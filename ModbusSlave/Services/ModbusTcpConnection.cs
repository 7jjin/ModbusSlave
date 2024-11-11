using ModbusSlave.Interfaces;
using NModbus;
using NModbus.Data;
using NModbus.Device;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ModbusSlave.Services
{
    public class ModbusTcpConnection : IModbusConnection
    {
        private TcpListener _tcpListener;
        private IModbusSlaveNetwork _slaveNetwork;
        private IModbusSlave _slave;
        private DefaultSlaveDataStore _dataStore;
        private System.Timers.Timer _connectionTimer;
        private TcpClient _masterClient;

        // IModbusConnection 인터페이스의 속성 구현
        public bool IsConnected { get; private set; }
        public bool IsListening { get; private set; }

        public ModbusTcpConnection()
        {
            _connectionTimer = new System.Timers.Timer(2000);
            _connectionTimer.Elapsed += CheckConnectionStatus;
            _connectionTimer.AutoReset = true;
        }

        /// <summary>
        /// Slave 연결 설정 및 Listen 상태로 진입
        /// </summary>
        public async void Connect(string ipAddress, int port, int slaveId)
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
                _tcpListener.Start();

                var factory = new ModbusFactory();
                _dataStore = new DefaultSlaveDataStore();
                _slave = factory.CreateSlave((byte)slaveId, _dataStore);
                _slaveNetwork = factory.CreateSlaveNetwork(_tcpListener);
                _slaveNetwork.AddSlave(_slave);
                IsListening = true;

                Console.WriteLine("Modbus Slave is listening for connections.");

                _masterClient = await _tcpListener.AcceptTcpClientAsync();
                IsConnected = true;
                await _slaveNetwork.ListenAsync();
                _connectionTimer.Start();
                Console.WriteLine("Modbus Master connected.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting: {ex.Message}");
            }
        }

        /// <summary>
        /// Slave 연결 종료
        /// </summary>
        public void Disconnect()
        {
            try
            {
                _connectionTimer.Stop();

                _tcpListener?.Stop();
                _tcpListener = null;

                _masterClient?.Close();
                _masterClient = null;

                _slaveNetwork?.Dispose();
                _slave = null;

                IsConnected = false;
                IsListening = false;
                MessageBox.Show("Slave disconnected successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error disconnecting: {ex.Message}");
            }
        }

        /// <summary>
        /// Master와의 연결 상태를 주기적으로 확인
        /// </summary>
        private async void CheckConnectionStatus(object sender, ElapsedEventArgs e)
        {
            if (!await IsMasterConnected())
            {
                Console.WriteLine("Connection lost. Attempting to reconnect...");
                IsConnected = false;
                _connectionTimer.Stop();
            }
        }

        /// <summary>
        /// Master와의 연결 여부 확인
        /// </summary>
        public async Task<bool> IsMasterConnected()
        {
            if (_masterClient != null)
            {
                try
                {
                    // 연결 상태를 확인하기 위해 0 바이트 전송을 시도
                    if (_masterClient.Client.Poll(0, SelectMode.SelectRead) && _masterClient.Available == 0)
                    {
                        // 연결이 끊어진 것으로 간주
                        IsConnected = false;
                    }
                    else
                    {
                        // 연결이 유지되고 있는 것으로 간주
                        IsConnected = true;
                        return true;
                    }
                }
                catch (SocketException)
                {
                    // 연결 오류 발생 시 연결이 끊어졌다고 판단
                    IsConnected = false;
                }
            }

            return false;
        }

        /// <summary>
        /// Holding Registers 읽기
        /// </summary>
        public async Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity)
        {
            if (!IsConnected)
            {
                MessageBox.Show("Modbus Master is not connected.");
                return null;
            }
            return await Task.FromResult(_dataStore.HoldingRegisters.ReadPoints(startAddress, quantity));
        }

        /// <summary>
        /// Holding Registers 쓰기
        /// </summary>
        public async Task WriteHoldingRegistersAsync(ushort startAddress, ushort[] values)
        {
            if (!IsConnected)
            {
                MessageBox.Show("Modbus Master is not connected.");
                return;
            }
            await Task.Run(() => _dataStore.HoldingRegisters.WritePoints(startAddress, values));
        }
    }
}
