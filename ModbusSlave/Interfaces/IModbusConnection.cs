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
        bool IsConnected { get; }        // 연결 상태를 반환하는 속성
        bool IsListening { get; }        // Listen 상태를 반환하는 속성
        void Connect(string ipAddress, int port, int slaveId);

        void Disconnect();

        //Task<bool> IsMasterConnected();

        Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity);
        Task WriteHoldingRegistersAsync(ushort startAddress, ushort[] values);
    }
}
