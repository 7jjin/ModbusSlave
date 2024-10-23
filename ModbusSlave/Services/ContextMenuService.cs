﻿using ModbusSlave.Interfaces;
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

            // 컨텍스트 메뉴 표시
            menu.Show(dataView, dataView.PointToClient(Cursor.Position));

            return selectedType;
        }

        private void OnTypeSelected(DataType type)
        {
            selectedType = type;
            MessageBox.Show($"Row {_rowIndex}, Column {_columnIndex} : 데이터 타입이 {type}으로 선택되었습니다.");

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
