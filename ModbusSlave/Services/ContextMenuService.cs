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

        private void AddEndianSubMenu(ToolStripMenuItem parentItem)
        {
            var bigEndianItem = new ToolStripMenuItem("Big-endian", null, OnEndianTypeSelected);
            var littleEndianItem = new ToolStripMenuItem("Little-endian", null, OnEndianTypeSelected);
            var bigEndianByteSwapItem = new ToolStripMenuItem("Big-endian Byte Swap", null, OnEndianTypeSelected);
            var littleEndianByteSwapItem = new ToolStripMenuItem("Little-endian Byte Swap", null, OnEndianTypeSelected);

            // 서브 메뉴를 부모 메뉴에 추가
            parentItem.DropDownItems.Add(bigEndianItem);
            parentItem.DropDownItems.Add(littleEndianItem);
            parentItem.DropDownItems.Add(bigEndianByteSwapItem);
            parentItem.DropDownItems.Add(littleEndianByteSwapItem);
        }

        private void OnTypeSelected(DataType type)
        {
            selectedType = type;
            //MessageBox.Show($"Row {_rowIndex}, Column {_columnIndex} : 데이터 타입이 {type}으로 선택되었습니다.");

            // _dataViewService가 null이 아닌지 확인 후 호출
            if (_dataViewService != null)
            {
                _dataViewService.UpdateCellData(_rowIndex, _columnIndex, type, "none");
            }
            else
            {
                MessageBox.Show("DataViewService가 주입되지 않았습니다.");
            }
        }

        private void OnEndianTypeSelected(object sender, EventArgs e)
        {
            // 선택된 서브 메뉴 항목
            var selectedItem = sender as ToolStripMenuItem;

            // 부모 메뉴 항목
            var parentItem = selectedItem?.OwnerItem as ToolStripMenuItem;
            if (parentItem != null)
            {
                string parentMenuText = parentItem.Text; // 예: "32-bit Signed"
                string selectedEndianType = selectedItem.Text; // 예: "Big-endian"

                // 부모와 서브 메뉴 텍스트 정보를 UpdateCellData로 전달
                if (_dataViewService != null)
                {
                    if(parentMenuText == "32-bit Signed")
                    {
                        _dataViewService.UpdateCellData(_rowIndex, _columnIndex, DataType.Signed32, selectedEndianType);
                    }
                    else if(parentMenuText == "32-bit Unsigned")
                    {
                        _dataViewService.UpdateCellData(_rowIndex, _columnIndex, DataType.Unsigned32, selectedEndianType);
                    }else if(parentMenuText == "64-bit Signed")
                    {
                        _dataViewService.UpdateCellData(_rowIndex, _columnIndex, DataType.Signed64, selectedEndianType);
                    }
                    else if (parentMenuText == "64-bit Unsigned")
                    {
                        _dataViewService.UpdateCellData(_rowIndex, _columnIndex, DataType.Unsigned64, selectedEndianType);
                    }
                }
                else
                {
                    MessageBox.Show("DataViewService가 주입되지 않았습니다.");
                }
            }
        }

        private int ConvertToSigned(string value)
        {
            // 기본적으로 10진수로 처리
            return int.Parse(value);
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
