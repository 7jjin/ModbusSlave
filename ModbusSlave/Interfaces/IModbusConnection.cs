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
        void Connect(string ipAddress, int port, int slaveId);

        void Disconnect();
        Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity);
        Task WriteHoldingRegistersAsync(ushort startAddress, ushort[] values);
    }
}
