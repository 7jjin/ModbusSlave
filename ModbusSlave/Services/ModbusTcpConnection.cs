using ModbusSlave.Interfaces;
using NModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSlave.Services
{
    public class ModbusTcpConnection : IModbusConnection
    {
        private TcpListener _tcpListener;
        private IModbusSlaveNetwork _slaveNetwork;

        public void Connect()
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 502);
                _tcpListener.Start();

                var factory = new ModbusFactory();
                var slave = factory.CreateSlave(1);

                _slaveNetwork = factory.CreateSlaveNetwork(_tcpListener);
                _slaveNetwork.AddSlave(slave);

                Task.Run(() => ListenForRequests());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect: {ex.Message}");
                throw;
            }
        }

        private async Task ListenForRequests()
        {
            try
            {
                Console.WriteLine("Waiting for Modbus Master requests...");
                while (true)
                {
                    // Master의 연결 수락
                    var client = await _tcpListener.AcceptTcpClientAsync();
                    Console.WriteLine("Modbus Master connected.");

                    // 요청 수신 및 처리
                    await _slaveNetwork.ListenAsync();
                }
            } catch (Exception ex) { Console.WriteLine($"Error while listening for requests: {ex.Message}"); };
        }

    }
}
