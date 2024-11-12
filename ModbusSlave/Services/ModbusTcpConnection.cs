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
        private string _ipAddress;
        private int _port;
        private int _slaveId;

        // IModbusConnection 인터페이스의 속성 구현
        public bool IsConnected { get;  set; }
        public bool IsListened { get;  set; }
        public string LogMessage {  get; set; }

        public ModbusTcpConnection()
        {
            _connectionTimer = new System.Timers.Timer(2000);
            _connectionTimer.Elapsed += CheckConnectionStatus;
            _connectionTimer.AutoReset = true;
        }

        /// <summary>
        /// Slave 연결 설정 및 Listen 상태로 진입
        /// </summary>
        public  void Connect(string ipAddress, int port, int slaveId)
        {
            try
            {
                // 1. TcpListener로 Master 연결 요청을 수락
                _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
                _tcpListener.Start();

                // Slave 설정 초기화
                _ipAddress = ipAddress;
                _port = port;
                _slaveId = slaveId;

                var factory = new ModbusFactory();
                _dataStore = new DefaultSlaveDataStore();
                _slave = factory.CreateSlave((byte)slaveId, _dataStore);
                _slaveNetwork = factory.CreateSlaveNetwork(_tcpListener);
                _slaveNetwork.AddSlave(_slave);

                IsListened = true;
                Console.WriteLine("Modbus Slave is ready to accept Master connections.");

                // 2. Master 연결 요청 수락
                Task.Run(async () =>
                {
                    _masterClient = await _tcpListener.AcceptTcpClientAsync();
                });
                //_masterClient = await _tcpListener.AcceptTcpClientAsync();

                IsConnected = true;
                //await _slaveNetwork.ListenAsync();
                Console.WriteLine("Modbus Master connected.");

                // 연결 수락 후 `ListenAsync`로 Modbus 통신 시작
                Task.Run(async () =>
                {
                    try
                    {
                        await _slaveNetwork.ListenAsync();
                        Console.WriteLine("Modbus communication is now active.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error in Modbus communication: {ex.Message}");
                        IsConnected = false;
                    }
                });

                // 연결 확인 타이머 시작
                _connectionTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Modbus connection setup: {ex.Message}");
                Disconnect();
            }
        }



        /// <summary>
        /// Slave 연결 종료
        /// </summary>
        public void Disconnect()
        {
            try
            {
                _connectionTimer?.Stop();

                _slaveNetwork?.Dispose();
                _slaveNetwork = null;

                _tcpListener?.Stop();
                _tcpListener = null;

                _masterClient?.Close();
                _masterClient = null;

                _slave = null; // 슬레이브 개체 해제

                IsConnected = false;
                IsListened = false;

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
                //await Connect()
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
                    var isConnected = _masterClient.Client.Poll(0, SelectMode.SelectRead) && _masterClient.Client.Available == 0;
                    if (isConnected)
                    {
                        Console.WriteLine("Connection with Modbus Master lost.");
                        return false; // 연결 끊김
                    }
                    return true; // 연결 유지 중
                }
                catch
                {
                    Console.WriteLine("Unable to verify connection with Modbus Master. Assuming disconnection.");
                    return false;
                }
            }
            return false;
        }

        private async Task ReconnectAsync()
        {
            while (!IsConnected)
            {
                try
                {
                    Console.WriteLine("Reconnecting to Modbus Master...");
                    Connect(_ipAddress, _port, _slaveId);
                    await Task.Delay(5000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Reconnection attempt failed: {ex.Message}");
                }
            }
            _connectionTimer.Start();
        }

        /// <summary>
        /// Holding Registers 읽기
        /// </summary>
        public async Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity)
        {
            //if (!IsConnected)
            //{
            //    MessageBox.Show("Modbus Master is not connected.");
            //    return null;
            //}
            return await Task.FromResult(_dataStore.HoldingRegisters.ReadPoints(startAddress, quantity));
        }

        /// <summary>
        /// Holding Registers 쓰기
        /// </summary>
        public async Task WriteHoldingRegistersAsync(ushort startAddress, ushort[] values)
        {
            //if (!IsConnected)
            //{
            //    MessageBox.Show("Modbus Master is not connected.");
            //    return;
            //}
            await Task.Run(() => _dataStore.HoldingRegisters.WritePoints(startAddress, values));
        }
    }
}
