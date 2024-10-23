using ModbusSlave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ModbusSlave.Interfaces
{
    public interface IDataViewService
    {
        void InitializeDataView(DataGridView dataView);
        void LoadData(DataGridView dataGridView);

        void UpdateCellData(int rowIndex, int columnIndex, DataType selectedType);
    }
}
