using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSlave.Interfaces
{
    public interface IModbusDataStore
    {
        Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity);
    }
}
