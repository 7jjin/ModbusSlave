using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSlave.Models
{
    public class CellData
    {
        public int RowIndex { get; set; }
        public DataType Type { get; set; }
        public EndianType EndianType { get; set; }
        public object Value { get; set; }

        

        public CellData(int rowIndex, DataType type, object value, EndianType endianType)
        {
            Type = type;
            EndianType = endianType;
            Value = value;
            RowIndex = rowIndex;  
        }
    }
}
