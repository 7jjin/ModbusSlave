using ModbusSlave.Interfaces;
using ModbusSlave.Models;
using System.Data;
using System;
using System.Windows.Forms;
using System.Globalization;
using System.Linq;

namespace ModbusSlave.Services
{
    public class ContextMenuService : IContextMenuService
    {
        private DataType? selectedType = null;
        private int _rowIndex;
        private int _columnIndex;
        private IDataViewService _dataViewService;  // DataViewService에 접근
        private ContextMenuStrip _contextMenuStrip;
        private ToolStripMenuItem _menuItem;
        private DataGridView _dataView;

        // 생성자에서 주입 대신 setter로 주입
        public void SetDataViewService(IDataViewService dataViewService)
        {
            _dataViewService = dataViewService;
        }

        public DataType? ShowContextMenu(DataGridView dataView, int rowIndex, int columnIndex)
        {
            // RowIndex와 ColumnIndex를 저장
            _rowIndex = rowIndex;
            _columnIndex = columnIndex;
            _dataView = dataView;
            _contextMenuStrip = new ContextMenuStrip();



            // 각 항목 추가
            var signedItem = _contextMenuStrip.Items.Add("Signed", null, (s, e) => OnTypeSelected(DataType.Signed));
            var unsignedItem = _contextMenuStrip.Items.Add("Unsigned", null, (s, e) => OnTypeSelected(DataType.Unsigned));
            var hexItem = _contextMenuStrip.Items.Add("Hex", null, (s, e) => OnTypeSelected(DataType.Hex));
            var binaryItem = _contextMenuStrip.Items.Add("Binary", null, (s, e) => OnTypeSelected(DataType.Binary));
            var signed32Item = new ToolStripMenuItem("32-bit Signed");
            var unsigned32Item = new ToolStripMenuItem("32-bit Unsigned");
            var signed64Item = new ToolStripMenuItem("64-bit Signed");
            var unsigned64Item = new ToolStripMenuItem("64-bit Unsigned");

            // 서브 메뉴를 각 항목에 추가
            AddEndianSubMenu(signed32Item);
            AddEndianSubMenu(unsigned32Item);
            AddEndianSubMenu(signed64Item);
            AddEndianSubMenu(unsigned64Item);

            // ContextMenuStrip에 항목 추가
            _contextMenuStrip.Items.Add(signed32Item);
            _contextMenuStrip.Items.Add(unsigned32Item);
            _contextMenuStrip.Items.Add(signed64Item);
            _contextMenuStrip.Items.Add(unsigned64Item);


            // 컨텍스트 메뉴 표시
            _contextMenuStrip.Show(dataView, dataView.PointToClient(Cursor.Position));

            return selectedType;
        }

        private void AddEndianSubMenu(ToolStripMenuItem menuItem)
        {
            var bigEndianItem = new ToolStripMenuItem("Big-endian");
            var littleEndianItem = new ToolStripMenuItem("Little-endian");
            var bigEndianByteSwapItem = new ToolStripMenuItem("Big-endian Byte Swap");
            var littleEndianByteSwapItem = new ToolStripMenuItem("Little-endian Byte Swap");

            // 메뉴에 서브 메뉴 추가
            menuItem.DropDownItems.Add("bigEndianItem", null, (s, e) => OnTypeSelected(DataType.Signed));
            menuItem.DropDownItems.Add("littleEndianItem", null, (s, e) => OnTypeSelected(DataType.Signed));
            menuItem.DropDownItems.Add("bigEndianByteSwapItem", null, (s, e) => OnTypeSelected(DataType.Signed));
            menuItem.DropDownItems.Add("littleEndianByteSwapItem", null, (s, e) => OnTypeSelected(DataType.Signed));
        }

        private void OnTypeSelected(DataType type)
        {
            selectedType = type;
            //MessageBox.Show($"Row {_rowIndex}, Column {_columnIndex} : 데이터 타입이 {type}으로 선택되었습니다.");

            // _dataViewService가 null이 아닌지 확인 후 호출
            if (_dataViewService != null)
            {
                _dataViewService.UpdateCellData(_rowIndex, _columnIndex, type);
            }
            else
            {
                MessageBox.Show("DataViewService가 주입되지 않았습니다.");
            }
        }

        private int ConvertToSigned(string value)
        {
            // 기본적으로 10진수로 처리
            return int.Parse(value);
        }

        /// <summary>
        /// 어떤 타입의 Big-Endian을 클릭했는지 확인하는 방법
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void OnBigEndianClick(DataType type)
        //{
        //    var selectedCell = _dataView.SelectedCells[1].Value.ToString();
        //    int currentRowIndex = _dataView.SelectedCells[1].RowIndex;
        //    int currentColumnIndex = _dataView.SelectedCells[1].ColumnIndex;

        //    if (currentRowIndex + 1 >= _dataView.Rows.Count)
        //    {
        //        MessageBox.Show("다음 행이 존재하지 않습니다.");
        //        return;
        //    }

        //    var nextCell = _dataView.Rows[currentRowIndex + 1].Cells[currentColumnIndex].Value.ToString();
        //    // sender를 ToolStripMenuItem으로 캐스팅
        //    if (sender is ToolStripMenuItem item)
        //    {
        //        // OwnerItem을 통해 상위 메뉴 아이템 확인
        //        if (item.OwnerItem is ToolStripMenuItem parentItem)
        //        {
        //            string parentText = parentItem.Text;

        //            // 32bit-signed, 32bit-Unsigned, 64bit-signed, 64bit-Unsigned 확인
        //            if (parentText == "32-bit Signed")
        //            {
        //                string signedBigEndian32Bit = Convert.ToString(Convert32bitToBigEndian(selectedCell, nextCell));
        //                _dataView.SelectedCells[1].Value = signedBigEndian32Bit;
        //                _dataView.Rows[currentRowIndex + 1].Cells[currentColumnIndex].Value = "-";
        //                DataType
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 32bit Signed Big-endian
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private long Convert32bitToBigEndian(string firstCellData, string secondCellData)
         {


            // 두 셀의 값을 16-bit로 읽어와 결합
            ushort upperValue = ConvertToUnsigned16(firstCellData);
            ushort lowerValue = ConvertToUnsigned16(secondCellData);

            // Big-endian으로 변환
            uint bigEndianValue = ((uint)upperValue << 16) | lowerValue;

            // 32bit signed로 변환 (부호 있는 값을 처리)
            int result = unchecked((int)bigEndianValue);
            return result;
        }

        private void OnLittleEndianClick(object sender, EventArgs e)
        {
            // sender를 ToolStripMenuItem으로 캐스팅
            if (sender is ToolStripMenuItem item)
            {
                // OwnerItem을 통해 상위 메뉴 아이템 확인
                if (item.OwnerItem is ToolStripMenuItem parentItem)
                {
                    string parentText = parentItem.Text;

                    // 32bit-signed, 32bit-Unsigned, 64bit-signed, 64bit-Unsigned 확인
                    if (parentText == "32-bit Signed" || parentText == "32-bit Unsigned" ||
                        parentText == "64-bit Signed" || parentText == "64-bit Unsigned")
                    {
                        MessageBox.Show($"{parentText}의 Big-endian이 선택되었습니다.");
                    }
                }
            }
        }

        private void OnBigEndianByteSwapClick(object sender, EventArgs e)
        {
            // sender를 ToolStripMenuItem으로 캐스팅
            if (sender is ToolStripMenuItem item)
            {
                // OwnerItem을 통해 상위 메뉴 아이템 확인
                if (item.OwnerItem is ToolStripMenuItem parentItem)
                {
                    string parentText = parentItem.Text;

                    // 32bit-signed, 32bit-Unsigned, 64bit-signed, 64bit-Unsigned 확인
                    if (parentText == "32-bit Signed" || parentText == "32-bit Unsigned" ||
                        parentText == "64-bit Signed" || parentText == "64-bit Unsigned")
                    {
                        MessageBox.Show($"{parentText}의 Big-endian이 선택되었습니다.");
                    }
                }
            }
        }

        private void OnLittleEndianByteSwapClick(Object sender, EventArgs e)
        {
            // sender를 ToolStripMenuItem으로 캐스팅
            if (sender is ToolStripMenuItem item)
            {
                // OwnerItem을 통해 상위 메뉴 아이템 확인
                if (item.OwnerItem is ToolStripMenuItem parentItem)
                {
                    string parentText = parentItem.Text;

                    // 32bit-signed, 32bit-Unsigned, 64bit-signed, 64bit-Unsigned 확인
                    if (parentText == "32-bit Signed" || parentText == "32-bit Unsigned" ||
                        parentText == "64-bit Signed" || parentText == "64-bit Unsigned")
                    {
                        MessageBox.Show($"{parentText}의 Big-endian이 선택되었습니다.");
                    }
                }
            }
        }



        //--------------------------------------------32bit 정의시 필요한 함수---------------------------------------------------------//
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
    }
}
