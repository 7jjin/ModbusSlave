using ModbusSlave.Interfaces;
using ModbusSlave.Models;
using System;
using System.Collections.Generic;
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

        public void UpdateCellData(int rowIndex, int columnIndex, DataType selectedType)
        {
            if (columnIndex == 1)  // 두 번째 열만 처리
            {
                _cellDataList[rowIndex].Type = selectedType;  // 선택된 타입으로 업데이트
                return;
                // 필요 시 추가 처리 (예: 값 변환)
                // _cellDataList[rowIndex].Value = 변환된 값;
            }
        }
    }
}
