using ModbusSlave.Interfaces;
using ModbusSlave.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public DataViewService(ContextMenuService contextMenuService)
        {
            _contextMenuService = contextMenuService;
            _cellDataList = new List<CellData>();
        }

        public void InitializeDataView(DataGridView dataView)
        {
            _dataView = dataView;
            dataView.Columns.Add("Column1", "No");    // "No" 헤더
            dataView.Columns.Add("Column2", "00000"); // "00000" 헤더

            // 11개의 행 추가 (0~9번 인덱스, 총 10개)
            for (int i = 0; i < 9; i++)
            {
                dataView.Rows.Add();
                _cellDataList.Add(new CellData(i,DataType.Signed, 0));    //초기값 Signed, 0 으로 설정
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
            if (e.ColumnIndex == 1) // 예를 들어, 두 번째 열이 데이터 입력 열이라고 가정
            {
                Form2 dataInputForm = new Form2(_cellDataList[e.RowIndex].Type);
                if (dataInputForm.ShowDialog() == DialogResult.OK)
                {
                   _cellDataList[e.RowIndex].Value = dataInputForm.InputValue;

                    // DataGridView에 값 업데이트
                    _dataView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dataInputForm.InputValue;
                }
            }
        }

        /// <summary>
        /// 데이터 타입 변경에 따른 Cell값 변경
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="selectedType"></param>
        public void UpdateCellData(int rowIndex, int columnIndex, DataType selectedType)
        {
            if (columnIndex == 1)  // 두 번째 열만 처리
            {
                _cellDataList[rowIndex].Type = selectedType;  // 선택된 타입으로 업데이트
                var selectedCell = _dataView.Rows[rowIndex].Cells[1];
                Console.WriteLine(_cellDataList);

                if(selectedType == DataType.Signed)
                {
                    int  signedValue = ConvertToSigned(selectedCell.Value.ToString());
                    if(signedValue > 32767)
                    {
                        selectedCell.Value = signedValue - 65536;
                    }
                    else
                    {
                        selectedCell.Value = signedValue.ToString();
                    }
                }else if(selectedType == DataType.Unsigned)
                {
                    ushort unsignedValue = ConvertToUnsigned16(selectedCell.Value.ToString());
                    selectedCell.Value = unsignedValue.ToString();

                    

                }else if(selectedType == DataType.Hex)
                {
                    int value = ConvertToUnsigned16(selectedCell.Value.ToString());
                    selectedCell.Value = $"0x{value:X4}";
                }else if(selectedType == DataType.Binary)
                {
                    int binaryValue = ConvertToSigned(selectedCell.Value.ToString());
                    string binaryString = binaryValue < 0 ? Convert.ToString((ushort)binaryValue, 2).PadLeft(15, '0') : Convert.ToString(binaryValue, 2).PadLeft(16, '0');
                    selectedCell.Value = binaryString;
                }

                return;
                // 필요 시 추가 처리 (예: 값 변환)
                // _cellDataList[rowIndex].Value = 변환된 값;
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
            // 기본적으로 10진수로 처리
            return int.Parse(value);
        }

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

        private int ConvertToSignedFromUnsigned(ushort unsignedValue)
        {
            // Unsigned 값이 32768 이상이면 Signed로 변환 시 2의 보수를 적용
            if (unsignedValue >= 32768)
            {
                return unsignedValue - 65536;  // 65536(2^16)을 빼서 Signed 변환
            }
            return unsignedValue;  // 32767 이하인 경우 그대로 반환
        }
    }
}
