using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSlave.Interfaces
{
    public interface IModbusConnection
    {
        void Connect();
        Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity);
        Task WriteHoldingRegistersAsync(ushort startAddress, ushort[] values);
    }
}
