using ModbusSlave.Interfaces;
using ModbusSlave.Models;
using System.Windows.Forms;

namespace ModbusSlave.Services
{
    public class ContextMenuService : IContextMenuService
    {
        private DataType? selectedType = null;
        private int _rowIndex;
        private int _columnIndex;
        private IDataViewService _dataViewService;  // DataViewService에 접근
        private ContextMenuStrip _contextMenuStrip;

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

            ContextMenuStrip menu = new ContextMenuStrip();
            
            menu.Items.Add("Signed", null, (s, e) => OnTypeSelected(DataType.Signed));
            menu.Items.Add("Unsigned", null, (s, e) => OnTypeSelected(DataType.Unsigned));
            menu.Items.Add("Hex", null, (s, e) => OnTypeSelected(DataType.Hex));
            menu.Items.Add("Binary", null, (s, e) => OnTypeSelected(DataType.Binary));
            //var signed32Item = new ToolStripMenuItem("32-bit Signed");
            //var unsigned32Item = new ToolStripMenuItem("32-bit Unsigned");
            //var signed64Item = new ToolStripMenuItem("64-bit Signed");
            //var unsigned64Item = new ToolStripMenuItem("64-bit Unsigned");

            //// 서브 메뉴를 각 항목에 추가
            //AddEndianSubMenu(signed32Item);
            //AddEndianSubMenu(unsigned32Item);
            //AddEndianSubMenu(signed64Item);
            //AddEndianSubMenu(unsigned64Item);

            //// ContextMenuStrip에 항목 추가
            //_contextMenuStrip.Items.Add(signed32Item);
            //_contextMenuStrip.Items.Add(unsigned32Item);
            //_contextMenuStrip.Items.Add(signed64Item);
            //_contextMenuStrip.Items.Add(unsigned64Item);

            // 컨텍스트 메뉴 표시
            menu.Show(dataView, dataView.PointToClient(Cursor.Position));

            return selectedType;
        }

        //private void AddEndianSubMenu(ToolStripMenuItem menuItem)
        //{
        //    var bigEndianItem = new ToolStripMenuItem("Big-endian");
        //    var littleEndianItem = new ToolStripMenuItem("Little-endian");
        //    var bigEndianByteSwapItem = new ToolStripMenuItem("Big-endian Byte Swap");
        //    var littleEndianByteSwapItem = new ToolStripMenuItem("Little-endian Byte Swap");

        //    // 서브 메뉴 클릭 이벤트
        //    bigEndianItem.Click += OnTypeSelected(DataType.Signed);
        //    littleEndianItem.Click += OnLittleEndianClick;
        //    bigEndianByteSwapItem.Click += OnBigEndianByteSwapClick;
        //    littleEndianByteSwapItem.Click += OnLittleEndianByteSwapClick;

        //    // 메뉴에 서브 메뉴 추가
        //    menuItem.DropDownItems.Add(bigEndianItem);
        //    menuItem.DropDownItems.Add(littleEndianItem);
        //    menuItem.DropDownItems.Add(bigEndianByteSwapItem);
        //    menuItem.DropDownItems.Add(littleEndianByteSwapItem);
        //}

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
    }
}
