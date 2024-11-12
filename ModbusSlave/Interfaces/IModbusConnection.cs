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
        Task<bool> IsMasterConnected();  // 기존 연결 확인 메서드
        bool IsConnected { get; set; }        // 연결 상태를 반환하는 속성
        bool IsListened { get; set; }        // Listen 상태를 반환하는 속성
        string LogMessage { get; set; }
        void Connect(string ipAddress, int port, int slaveId);

        void Disconnect();

        //Task<bool> IsMasterConnected();

        Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity);
        Task WriteHoldingRegistersAsync(ushort startAddress, ushort[] values);
    }
}
