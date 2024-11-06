using ModbusSlave.Interfaces;
using ModbusSlave.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusSlave.Services
{
    public class DataViewService : IDataViewService
    {
        private DataGridView _dataView;
        private readonly ContextMenuService _contextMenuService;
        private List<CellData> _cellDataList;
        private IModbusConnection _modbusConnection;


        public DataViewService(ContextMenuService contextMenuService, IModbusConnection modbusConnection)
        {
            _contextMenuService = contextMenuService;
            _cellDataList = new List<CellData>();
            _modbusConnection = modbusConnection;
        }

        public void InitializeDataView(DataGridView dataView)
        {
            _dataView = dataView;
            dataView.Columns.Add("Column1", "No");    // "No" 헤더
            dataView.Columns.Add("Column2", "00000"); // "00000" 헤더
            dataView.EnableHeadersVisualStyles = false;

            dataView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            dataView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightGray;

            // 11개의 행 추가 (0~9번 인덱스, 총 10개)
            for (int i = 0; i <= 9; i++)
            {
                dataView.Rows.Add();
                _cellDataList.Add(new CellData(i,DataType.Signed, 0,EndianType.none));    //초기값 Signed, 0 으로 설정
            }
            dataView.CellDoubleClick += dataGridView_CellDoubleClick;
        }

        public void LoadData(DataGridView dataView)
        {
            // 2~11행에 첫 번째 열에 0~9 값을 입력
            for (int i = 0; i < 10; i++)
            {
                dataView.Rows[i].Cells[0].Value = i;  // 첫 번째 열에 0부터 9까지 입력
                dataView.Rows[i].Cells[1].Value = 0;  // 연결 즉시 모두 0으로 초기화
            }
        }

        /// <summary>
        /// Cell 더블 클릭 시 발생하는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // e.RowIndex가 유효한지 확인
            if (e.RowIndex < 0) return;

            // HeaderText에서 ushort 값 가져오기
            if (ushort.TryParse(_dataView.Columns[1].HeaderText, out ushort headerValue))
            {
                // 클릭한 셀의 0열 값 가져오기
                // 0열에서 현재 행의 값을 가져오고 ushort로 변환
                if (ushort.TryParse(_dataView.Rows[e.RowIndex].Cells[0].Value.ToString(), out ushort cellValue))
                {
                    // startAddress 계산
                    ushort startAddress = (ushort)(headerValue + cellValue);

                    // 두 번째 열이 클릭된 경우에만 Form2를 연다.
                    if (e.ColumnIndex == 1) 
                    {
                        Form2 dataInputForm = new Form2(_cellDataList[e.RowIndex].Type, startAddress, _modbusConnection, _cellDataList[e.RowIndex].EndianType, e.RowIndex);
                        if (dataInputForm.ShowDialog() == DialogResult.OK)
                        {
                            _cellDataList[e.RowIndex].Value = dataInputForm.InputValue;

                            // DataGridView에 값 업데이트
                            _dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dataInputForm.InputValue;
                        }
                    }
                }
                else
                {
                    // 셀의 값이 ushort로 변환 불가할 경우 처리
                    MessageBox.Show("0열의 값을 읽을 수 없습니다.");
                }
            }
            else
            {
                // HeaderText가 ushort로 변환 불가할 경우 처리
                MessageBox.Show("헤더 텍스트를 읽을 수 없습니다.");
            }
        }


        /// <summary>
        /// 지정한 수량(quantity)만큼 CellData의 데이터 타입을 Signed로 변경
        /// </summary>
        /// <param name="quantity">변경할 셀의 수량</param>
        public void SetCellsToSigned(int quantity)
        {
            // quantity가 cellDataList의 크기를 초과하지 않도록 제한
            //int cellsToChange = Math.Min(quantity, _cellDataList.Count);

            for (int i = 0; i < quantity; i++)
            {
                // DataType을 Signed로 변경
                _cellDataList[i].Type = DataType.Signed;
            }
        }

        /// <summary>
        /// 데이터 타입 변경에 따른 Cell값 변경
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="selectedType"></param>
        public async void UpdateCellData(int rowIndex, int columnIndex, DataType selectedType, EndianType endianType)
        {
            if (columnIndex == 1)  // 두 번째 열만 처리
            {
                _cellDataList[rowIndex].Type = selectedType;  // 선택된 타입으로 업데이트
                _cellDataList[rowIndex].EndianType = endianType;
                var firstCellData = _dataView.Rows[rowIndex].Cells[1];

                ushort startAddress = ushort.Parse(_dataView.Rows[rowIndex].Cells[0].Value.ToString());
                Console.WriteLine(_cellDataList);

                // selectedType = 클릭 한 타입
                if(endianType == EndianType.none)
                {
                    // 레지스트리에서 값을 가져온다
                    ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 1);
                    // 16bit Signed 선택
                    if (selectedType == DataType.Signed)
                    {
                        int signedValue = ConvertToSigned(registerValues[0].ToString());
                        if (signedValue > 32767)
                        {
                            firstCellData.Value = signedValue - 65536;
                        }
                        else
                        {
                            firstCellData.Value = signedValue.ToString();
                        }
                    }
                    // 16it Unsigned 선택
                    else if(selectedType == DataType.Unsigned)
                    {
                        ushort unsignedValue = ConvertToUnsigned16(registerValues[0].ToString());
                        firstCellData.Value = unsignedValue.ToString();
                    }
                     // 16bit Hex 선택
                    else if(selectedType == DataType.Hex)
                    {
                        int value = ConvertToUnsigned16(registerValues[0].ToString());
                        firstCellData.Value = $"0x{value:X4}";
                    }
                    // 16bit Binary 선택
                    else if(selectedType == DataType.Binary)
                    {
                        int binaryValue = ConvertToSigned(registerValues[0].ToString());
                        string binaryString = binaryValue < 0 ? Convert.ToString((ushort)binaryValue, 2).PadLeft(15, '0') : Convert.ToString(binaryValue, 2).PadLeft(16, '0');
                        firstCellData.Value = binaryString;
                    }
                }
                // 32bit, 64bit Big-Endian을 선택
                else if(endianType == EndianType.BigEndian)
                {
                        
                    // 32bit Signed Big-endian을 선택
                    if(selectedType == DataType.Signed32)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 2);

                        ushort upperValue = ConvertToUnsigned16(registerValues[0].ToString());
                        ushort lowerValue = ConvertToUnsigned16(registerValues[1].ToString());

                        
                        uint bigEndianValue = ((uint)upperValue << 16) | lowerValue;

                        // 32bit signed로 변환 (부호 있는 값을 처리)
                        int result = unchecked((int)bigEndianValue);
                        firstCellData.Value = result.ToString();
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                    }
                    // 32bit Unsigned Big-endian을 선택
                    else if (selectedType == DataType.Unsigned32)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 2);
                        
                        ushort upperValue = ConvertToUnsigned16(registerValues[0].ToString());
                        ushort lowerValue = ConvertToUnsigned16(registerValues[1].ToString());

                        // Big-endian으로 변환
                        ulong bigEndianValue = ((ulong)upperValue << 16) | lowerValue;
                        firstCellData.Value = bigEndianValue.ToString();
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                    }
                    // 64bit Signed Big-endian을 선택
                    else if(selectedType == DataType.Signed64)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 4);
                        uint upperValue = ConvertTo32BitBigEndian(registerValues[0].ToString(), registerValues[1].ToString(), registerValues[2].ToString(), registerValues[3].ToString());
                        uint lowerValue = ConvertTo32BitBigEndian(registerValues[2].ToString(), registerValues[3].ToString(), registerValues[0].ToString(), registerValues[1].ToString());

                        ulong bigEndianValue = ((ulong)upperValue << 32) | lowerValue;

                        // Signed 처리
                        if ((bigEndianValue & 0x8000000000000000) != 0)
                        {
                            firstCellData.Value =  unchecked((long)bigEndianValue);
                        }

                        firstCellData.Value = (long)bigEndianValue;
                        if(rowIndex >= 7)
                        {
                            for(int i=rowIndex+1;i<10; i++)
                            {
                                _dataView.Rows[i].Cells[1].Value = "-";
                            }
                        }
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 2].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 3].Cells[1].Value = "-";
                    }
                    // 64bit Unsigned Big-endian을 선택
                    else if(selectedType == DataType.Unsigned64)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 4);
                        uint upperValue = ConvertTo32BitBigEndianUnsigned(registerValues[0].ToString(), registerValues[1].ToString(), registerValues[2].ToString(), registerValues[3].ToString());
                        uint lowerValue = ConvertTo32BitBigEndianUnsigned(registerValues[2].ToString(), registerValues[3].ToString(), registerValues[0].ToString(), registerValues[1].ToString());

                        firstCellData.Value = ((ulong)upperValue << 32) | lowerValue;
                        if (rowIndex >= 7)
                        {
                            for (int i = rowIndex + 1; i < 10; i++)
                            {
                                _dataView.Rows[i].Cells[1].Value = "-";
                            }
                        }
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 2].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 3].Cells[1].Value = "-";
                    }
                }
                // 32bit, 64bit Little-Endian을 선택
                else if(endianType == EndianType.LittleEndian)
                {
                    // 32bit Signed Little-endian을 선택
                    if (selectedType == DataType.Signed32)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 2);
                        ushort upperValue = ConvertToUnsigned16(registerValues[0].ToString());
                        ushort lowerValue = ConvertToUnsigned16(registerValues[1].ToString());

                        // 각 16-bit 값을 little-endian 방식으로 바이트 순서를 바꿈
                        ushort reversedUpper = (ushort)((upperValue >> 8) | (upperValue << 8));
                        ushort reversedLower = (ushort)((lowerValue >> 8) | (lowerValue << 8));

                        // Little-endian으로 변환 (값 순서 반대로)
                        uint littleEndianValue = ((uint)reversedLower << 16) | reversedUpper;

                        // 32bit signed로 변환 (부호 있는 값을 처리)
                        int result = unchecked((int)littleEndianValue);
                        firstCellData.Value = result.ToString();
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                    }
                    // 32bit Unsigned Little-endian을 선택
                    else if(selectedType == DataType.Unsigned32)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 2);
                        ushort upperValue = ConvertToUnsigned16(registerValues[0].ToString());
                        ushort lowerValue = ConvertToUnsigned16(registerValues[1].ToString());

                        // 각 16-bit 값을 little-endian 방식으로 바이트 순서를 바꿈
                        ushort reversedUpper = (ushort)((upperValue >> 8) | (upperValue << 8));
                        ushort reversedLower = (ushort)((lowerValue >> 8) | (lowerValue << 8));

                        // Little-endian으로 변환 (값 순서 반대로)
                        ulong littleEndianValue = ((ulong)reversedLower << 16) | reversedUpper;
                        firstCellData.Value = littleEndianValue.ToString();
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                    }
                    // 64bit Signed Little-endian을 선택
                    else if(selectedType == DataType.Signed64)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 4);
                        uint upperValue = ConvertTo32BitLittleEndian(registerValues[0].ToString(), registerValues[1].ToString(), registerValues[2].ToString(), registerValues[3].ToString());
                        uint lowerValue = ConvertTo32BitLittleEndian(registerValues[2].ToString(), registerValues[3].ToString(), registerValues[0].ToString(), registerValues[1].ToString());
                        ulong littleEndianValue = ((ulong)lowerValue << 32) | upperValue;

                        // Signed 처리
                        if ((littleEndianValue & 0x8000000000000000) != 0)
                        {
                            firstCellData.Value = unchecked((long)littleEndianValue);
                        }

                        firstCellData.Value =  (long)littleEndianValue;
                        if (rowIndex >= 7)
                        {
                            for (int i = rowIndex + 1; i < 10; i++)
                            {
                                _dataView.Rows[i].Cells[1].Value = "-";
                            }
                        }
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 2].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 3].Cells[1].Value = "-";
                    }
                    // 64bit Unsigned Little-endian을 선택
                    else if(selectedType == DataType.Unsigned64)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 4);
                        uint upperValue = ConvertTo32BitLittleEndian(registerValues[0].ToString(), registerValues[1].ToString(), registerValues[2].ToString(), registerValues[3].ToString());
                        uint lowerValue = ConvertTo32BitLittleEndian(registerValues[2].ToString(), registerValues[3].ToString(), registerValues[0].ToString(), registerValues[1].ToString());
                        ulong littleEndianValue = ((ulong)lowerValue << 32) | upperValue;

                        firstCellData.Value = littleEndianValue;
                        if (rowIndex >= 7)
                        {
                            for (int i = rowIndex + 1; i < 10; i++)
                            {
                                _dataView.Rows[i].Cells[1].Value = "-";
                            }
                        }
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 2].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 3].Cells[1].Value = "-";
                    }
                }
                // 32bit, 64bit Big-Endian Byte Swap을 선택
                else if(endianType == EndianType.BigEndianByteSwap)
                {
                    // 32bit Signed Big-endian Byte Swap을 선택
                    if(selectedType == DataType.Signed32)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 2);
                        ushort upperValue = ConvertToUnsigned16(registerValues[0].ToString());
                        ushort lowerValue = ConvertToUnsigned16(registerValues[1].ToString());

                        // Big-endian으로 결합: upperValue가 상위 비트로, lowerValue가 하위 비트로 결합
                        uint bigEndianValue = ((uint)upperValue << 16) | lowerValue; // 0x0457 08AE

                        // Byte Swap 수행: 각 바이트의 순서를 뒤집음
                        uint swappedValue = ((bigEndianValue & 0xFF000000) >> 8) |  // 상위 8비트 -> 두 번째 상위 8비트로 이동
                                    ((bigEndianValue & 0x00FF0000) << 8) |  // 두 번째 상위 8비트 -> 상위 8비트로 이동
                                    ((bigEndianValue & 0x0000FF00) >> 8) |  // 세 번째 상위 8비트 -> 하위 8비트로 이동
                                    ((bigEndianValue & 0x000000FF) << 8);    // 하위 8비트 -> 세 번째 상위 8비트로 이동


                        int result = unchecked((int)swappedValue);
                        firstCellData.Value = result;
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                    }
                    // 32bit Unsigned Big-endian Byte Swap을 선택
                    else if(selectedType == DataType.Unsigned32)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 2);
                        ushort upperValue = ConvertToUnsigned16(registerValues[0].ToString());
                        ushort lowerValue = ConvertToUnsigned16(registerValues[1].ToString());

                        // Big-endian으로 결합: upperValue가 상위 비트로, lowerValue가 하위 비트로 결합
                        uint bigEndianValue = ((uint)upperValue << 16) | lowerValue; // 0x0457 08AE

                        // Byte Swap 수행: 각 바이트의 순서를 뒤집음
                        uint swappedValue = ((bigEndianValue & 0xFF000000) >> 8) |  // 상위 8비트 -> 두 번째 상위 8비트로 이동
                                            ((bigEndianValue & 0x00FF0000) << 8) |  // 두 번째 상위 8비트 -> 상위 8비트로 이동
                                            ((bigEndianValue & 0x0000FF00) >> 8) |  // 세 번째 상위 8비트 -> 하위 8비트로 이동
                                            ((bigEndianValue & 0x000000FF) << 8);    // 하위 8비트 -> 세 번째 상위 8비트로 이동

                        // Unsigned 값으로 반환
                        firstCellData.Value =  (ulong)swappedValue;  // 결과 값 반환
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                    }
                    // 64bit Signed Big-endian Byte Swap을 선택
                    else if(selectedType == DataType.Signed64)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 4);
                        uint upperValue = ConvertTo32BitBigEndian(registerValues[0].ToString(), registerValues[1].ToString(), registerValues[2].ToString(), registerValues[3].ToString());
                        uint lowerValue = ConvertTo32BitBigEndian(registerValues[2].ToString(), registerValues[3].ToString(), registerValues[0].ToString(), registerValues[1].ToString());
                        ulong bigEndianValue = ((ulong)upperValue << 32) | lowerValue;

                        // Byte Swap
                        ulong swappedValue = ((bigEndianValue & 0xFF00000000000000) >> 8) |
                                             ((bigEndianValue & 0x00FF000000000000) << 8) |
                                             ((bigEndianValue & 0x0000FF0000000000) >> 8) |
                                             ((bigEndianValue & 0x000000FF00000000) << 8) |
                                             ((bigEndianValue & 0x00000000FF000000) >> 8) |
                                             ((bigEndianValue & 0x0000000000FF0000) << 8) |
                                             ((bigEndianValue & 0x000000000000FF00) >> 8) |
                                             ((bigEndianValue & 0x00000000000000FF) << 8);

                        // Signed 처리
                        if ((swappedValue & 0x8000000000000000) != 0)
                        {
                            firstCellData.Value = unchecked((long)swappedValue);
                        }

                        firstCellData.Value = (long)swappedValue;
                        if (rowIndex >= 7)
                        {
                            for (int i = rowIndex + 1; i < 10; i++)
                            {
                                _dataView.Rows[i].Cells[1].Value = "-";
                            }
                        }
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 2].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 3].Cells[1].Value = "-";
                    }
                    // 64bit Unsigned Big-endian Byte Swap을 선택
                    else if(selectedType == DataType.Unsigned64)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 4);
                        uint upperValue = ConvertTo32BitBigEndian(registerValues[0].ToString(), registerValues[1].ToString(), registerValues[2].ToString(), registerValues[3].ToString());
                        uint lowerValue = ConvertTo32BitBigEndian(registerValues[2].ToString(), registerValues[3].ToString(), registerValues[0].ToString(), registerValues[1].ToString());
                        ulong bigEndianValue = ((ulong)upperValue << 32) | lowerValue;

                        // Byte Swap
                        ulong swappedValue = ((bigEndianValue & 0xFF00000000000000) >> 8) |
                                             ((bigEndianValue & 0x00FF000000000000) << 8) |
                                             ((bigEndianValue & 0x0000FF0000000000) >> 8) |
                                             ((bigEndianValue & 0x000000FF00000000) << 8) |
                                             ((bigEndianValue & 0x00000000FF000000) >> 8) |
                                             ((bigEndianValue & 0x0000000000FF0000) << 8) |
                                             ((bigEndianValue & 0x000000000000FF00) >> 8) |
                                             ((bigEndianValue & 0x00000000000000FF) << 8);

                        firstCellData.Value = swappedValue;
                        if (rowIndex >= 7)
                        {
                            for (int i = rowIndex + 1; i < 10; i++)
                            {
                                _dataView.Rows[i].Cells[1].Value = "-";
                            }
                        }
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 2].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 3].Cells[1].Value = "-";
                    }
                }
                // 32bit, 64bit Little-Endian Byte Swap을 선택
                else if(endianType == EndianType.LittleEndianByteSwap)
                {
                    // 32bit Signed Little-endian Byte Swap을 선택
                    if(selectedType == DataType.Signed32)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 2);
                        ushort upperValue = ConvertToUnsigned16(registerValues[0].ToString());
                        ushort lowerValue = ConvertToUnsigned16(registerValues[1].ToString());
                        // 각 16-bit 값을 little-endian 방식으로 바이트 순서를 바꿈
                        ushort reversedUpper = (ushort)((upperValue >> 8) | (upperValue << 8));
                        ushort reversedLower = (ushort)((lowerValue >> 8) | (lowerValue << 8));

                        // Little-endian으로 변환 (값 순서 반대로)
                        uint littleEndianValue = ((uint)reversedLower << 16) | reversedUpper;

                        uint swappedValue = ((littleEndianValue & 0xFF000000) >> 8) |  // 상위 8비트 -> 두 번째 상위 8비트로 이동
                                     ((littleEndianValue & 0x00FF0000) << 8) |  // 두 번째 상위 8비트 -> 상위 8비트로 이동
                                     ((littleEndianValue & 0x0000FF00) >> 8) |  // 세 번째 상위 8비트 -> 하위 8비트로 이동
                                     ((littleEndianValue & 0x000000FF) << 8);    // 하위 8비트 -> 세 번째 상위 8비트로 이동

                        int result = unchecked((int)swappedValue);
                        firstCellData.Value = result;  // 결과 값 반환
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                    }
                    // 32bit Unsigned Little-endian Byte Swap을 선택
                    else if(selectedType == DataType.Unsigned32)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 2);
                        ushort upperValue = ConvertToUnsigned16(registerValues[0].ToString());
                        ushort lowerValue = ConvertToUnsigned16(registerValues[1].ToString());
                        // 각 16-bit 값을 little-endian 방식으로 바이트 순서를 바꿈
                        ushort reversedUpper = (ushort)((upperValue >> 8) | (upperValue << 8));
                        ushort reversedLower = (ushort)((lowerValue >> 8) | (lowerValue << 8));

                        // Little-endian으로 변환 (값 순서 반대로)
                        uint littleEndianValue = ((uint)reversedLower << 16) | reversedUpper;

                        uint swappedValue = ((littleEndianValue & 0xFF000000) >> 8) |  // 상위 8비트 -> 두 번째 상위 8비트로 이동
                                     ((littleEndianValue & 0x00FF0000) << 8) |  // 두 번째 상위 8비트 -> 상위 8비트로 이동
                                     ((littleEndianValue & 0x0000FF00) >> 8) |  // 세 번째 상위 8비트 -> 하위 8비트로 이동
                                     ((littleEndianValue & 0x000000FF) << 8);    // 하위 8비트 -> 세 번째 상위 8비트로 이동
                        firstCellData.Value = (ulong)swappedValue;
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                    }
                    // 64bit Signed Little-endian Byte Swap을 선택
                    else if(selectedType == DataType.Signed64)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 4);
                        uint upperValue = ConvertTo32BitLittleEndian(registerValues[0].ToString(), registerValues[1].ToString(), registerValues[2].ToString(), registerValues[3].ToString());
                        uint lowerValue = ConvertTo32BitLittleEndian(registerValues[2].ToString(), registerValues[3].ToString(), registerValues[0].ToString(), registerValues[1].ToString());
                        ulong littleEndianValue = ((ulong)lowerValue << 32) | upperValue;

                        // Byte Swap
                        ulong swappedValue = ((littleEndianValue & 0xFF00000000000000) >> 8) |
                                             ((littleEndianValue & 0x00FF000000000000) << 8) |
                                             ((littleEndianValue & 0x0000FF0000000000) >> 8) |
                                             ((littleEndianValue & 0x000000FF00000000) << 8) |
                                             ((littleEndianValue & 0x00000000FF000000) >> 8) |
                                             ((littleEndianValue & 0x0000000000FF0000) << 8) |
                                             ((littleEndianValue & 0x000000000000FF00) >> 8) |
                                             ((littleEndianValue & 0x00000000000000FF) << 8);

                        // Signed 처리
                        if ((swappedValue & 0x8000000000000000) != 0)
                        {
                            firstCellData.Value = unchecked((long)swappedValue);
                        }

                        firstCellData.Value = (long)swappedValue;
                        if (rowIndex >= 7)
                        {
                            for (int i = rowIndex + 1; i < 10; i++)
                            {
                                _dataView.Rows[i].Cells[1].Value = "-";
                            }
                        }
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 2].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 3].Cells[1].Value = "-";
                    }
                    // 64bit Unsigned Little-endian Byte Swap을 선택
                    else if(selectedType == DataType.Unsigned64)
                    {
                        ushort[] registerValues = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, 4);
                        uint upperValue = ConvertTo32BitLittleEndian(registerValues[0].ToString(), registerValues[1].ToString(), registerValues[2].ToString(), registerValues[3].ToString());
                        uint lowerValue = ConvertTo32BitLittleEndian(registerValues[2].ToString(), registerValues[3].ToString(), registerValues[0].ToString(), registerValues[1].ToString());
                        ulong littleEndianValue = ((ulong)lowerValue << 32) | upperValue;
                        // Byte Swap
                        ulong swappedValue = ((littleEndianValue & 0xFF00000000000000) >> 8) |
                                             ((littleEndianValue & 0x00FF000000000000) << 8) |
                                             ((littleEndianValue & 0x0000FF0000000000) >> 8) |
                                             ((littleEndianValue & 0x000000FF00000000) << 8) |
                                             ((littleEndianValue & 0x00000000FF000000) >> 8) |
                                             ((littleEndianValue & 0x0000000000FF0000) << 8) |
                                             ((littleEndianValue & 0x000000000000FF00) >> 8) |
                                             ((littleEndianValue & 0x00000000000000FF) << 8);

                        firstCellData.Value = swappedValue;
                        if (rowIndex >= 7)
                        {
                            for (int i = rowIndex + 1; i < 10; i++)
                            {
                                _dataView.Rows[i].Cells[1].Value = "-";
                            }
                        }
                        _dataView.Rows[rowIndex + 1].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 2].Cells[1].Value = "-";
                        _dataView.Rows[rowIndex + 3].Cells[1].Value = "-";
                    }
                }

                return;
             
            }
        }

        /// <summary>
        /// 형식에 따라 10진수로 변환
        /// </summary>
        private int ConvertToSigned(string value)
        {
            // 16진수 처리
            if (value.StartsWith("0x"))
            {
                return int.Parse(value.Substring(2), NumberStyles.HexNumber);
            }
            // 이진수 처리
            else if (value.All(c => c == '0' || c == '1') && (value.Length == 8 || value.Length == 16))
            {
                return Convert.ToInt32(value, 2);
            }
            // ASCII 문자 처리
            else if (value.Length == 1 && !char.IsDigit(value[0]) && char.IsLetterOrDigit(value[0]))
            {
                return (int)value[0]; // 문자 -> ASCII 코드 변환
            }
            else
            {

            }
            // 기본적으로 10진수로 처리
            return int.Parse(value);
        }




        //------------------------------------------------------------32bit 정의 시 필요 함수-----------------------------------------------------------------//

        /// <summary>
        /// Signed 값으로 변경 함수(16bit)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="OverflowException"></exception>
        private ushort ConvertToUnsigned16(string value)
        {
            int signedValue = ConvertToSigned(value);

            // 부호 있는 값을 16-bit unsigned로 변환
            if (signedValue < 0)
            {
                return (ushort)(signedValue + (1 << 16)); // 16-bit 2's complement 변환
            }

            // 16-bit 범위를 벗어나는 경우 처리
            if (signedValue > ushort.MaxValue)
            {
                throw new OverflowException("값이 16-bit 범위를 초과했습니다.");
            }

            return (ushort)signedValue;
        }

        //------------------------------------------------------------64bit 정의 시 필요 함수---------------------------------------------------------------------//
        private uint ConvertTo32BitBigEndian(string cell0, string cell1, string cell2, string cell3)
        {
            ushort upperValue = ConvertToUnsigned16(cell0); // 상위 16비트
            ushort lowerValue = ConvertToUnsigned16(cell1); // 하위 16비트

            // Big-endian으로 변환
            return ((uint)upperValue << 16) | lowerValue;
        }

        /// <summary>
        /// 64bit big-endian, little-endian 정의 할 때 필요한 함수
        /// </summary>
        /// <param name="cell2"></param>
        /// <param name="cell3"></param>
        /// <param name="cell0"></param>
        /// <param name="cell1"></param>
        /// <returns></returns>
        private uint ConvertTo32BitLittleEndian(string cell2, string cell3, string cell0, string cell1)
        {
            ushort upperValue = ConvertToUnsigned16(cell2); // 상위 16비트
            ushort lowerValue = ConvertToUnsigned16(cell3); // 하위 16비트

            // 각 16-bit 값을 little-endian 방식으로 바이트 순서를 바꿈
            ushort reversedUpper = (ushort)((upperValue >> 8) | (upperValue << 8));
            ushort reversedLower = (ushort)((lowerValue >> 8) | (lowerValue << 8));

            // Little-endian으로 변환
            return ((uint)reversedLower << 16) | reversedUpper;
        }
        private uint ConvertTo32BitBigEndianUnsigned(string cell0, string cell1, string cell2, string cell3)
        {
            ushort upperValue = ConvertToUnsigned16(cell0); // 상위 16비트
            ushort lowerValue = ConvertToUnsigned16(cell1); // 하위 16비트

            // Big-endian으로 변환
            return ((uint)upperValue << 16) | lowerValue;
        }

        private uint ConvertTo32BitLittleEndianUnsigned(string cell2, string cell3, string cell0, string cell1)
        {
            ushort upperValue = ConvertToUnsigned16(cell2); // 상위 16비트
            ushort lowerValue = ConvertToUnsigned16(cell3); // 하위 16비트
                                                            // 각 16-bit 값을 little-endian 방식으로 바이트 순서를 바꿈
            ushort reversedUpper = (ushort)((upperValue >> 8) | (upperValue << 8));
            ushort reversedLower = (ushort)((lowerValue >> 8) | (lowerValue << 8));

            // Little-endian으로 변환
            return ((uint)reversedLower << 16) | reversedUpper;

        }
    }
}
