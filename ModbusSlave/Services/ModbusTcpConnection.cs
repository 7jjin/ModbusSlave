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
        private bool _isConnected = false;
        private TcpClient _masterClient;

        public ModbusTcpConnection()
        {
            _connectionTimer = new System.Timers.Timer(2000);  // 2초마다 연결 상태 점검
            _connectionTimer.Elapsed += CheckConnectionStatus;
            _connectionTimer.AutoReset = true;
        }

        /// <summary>
        /// Connect 함수 - Slave 연결 설정
        /// </summary>
        public async void Connect(string ipAddress, int port, int slaveId)
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
                _tcpListener.Start();
                var factory = new ModbusFactory();

                // 기본 데이터 저장소 생성
                _dataStore = new DefaultSlaveDataStore();
                _slave = factory.CreateSlave((byte)slaveId, _dataStore);
                _slaveNetwork = factory.CreateSlaveNetwork(_tcpListener);
                _slaveNetwork.AddSlave(_slave);

                Console.WriteLine("Modbus Slave is listening for connections.");

                // Master 연결 수락 대기 (비동기적)
                _masterClient = await _tcpListener.AcceptTcpClientAsync();
                Console.WriteLine("Modbus Master connected.");
                _isConnected = true;

                // 연결 성공 후 타이머 시작
                _connectionTimer.Start();
            }
            catch (SocketException ex) when (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
            {
                MessageBox.Show($"Port {port} is already in use. Choose a different port.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting: {ex.Message}");
            }
        }

        /// <summary>
        /// Disconnect 함수 - Slave 연결 종료
        /// </summary>
        public void Disconnect()
        {
            try
            {
                _connectionTimer.Stop();

                if (_tcpListener != null)
                {
                    _tcpListener.Stop();
                    _tcpListener = null;
                }

                if (_masterClient != null)
                {
                    _masterClient.Close();
                    _masterClient = null;
                }

                _slaveNetwork.Dispose();
                _slave = null;

                _isConnected = false;
                MessageBox.Show("Slave disconnected successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error disconnecting: {ex.Message}");
            }
        }

        /// <summary>
        /// Master와의 연결 상태를 주기적으로 확인하는 메서드
        /// </summary>
        private async void CheckConnectionStatus(object sender, ElapsedEventArgs e)
        {
            if (!await IsMasterConnected())
            {
                Console.WriteLine("Connection lost. Restarting listener for reconnection...");
                _isConnected = false;
                _connectionTimer.Stop();

                // Listener 재시작 로직 추가
                Disconnect(); // 현재 연결을 종료 후
                Task.Delay(1000).Wait(); // 잠시 대기 (옵션)
                Connect("127.0.0.1", 502, 1); // 필요에 따라 재연결 시도
            }
            else
            {
                _isConnected = true;
                Console.WriteLine("Connection is stable.");
            }
        }

        /// <summary>
        /// Master와의 소켓 연결 상태 확인
        /// </summary>
        public async Task<bool> IsMasterConnected()
        {
            try
            {
                if (_masterClient?.Connected == true)
                {
                    // Master의 연결을 확인하려면 데이터를 수신할 수 있어야 함.
                    NetworkStream stream = _masterClient.GetStream();
                    byte[] buffer = new byte[1];

                    // 데이터를 받기 전에 연결 상태를 확인
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    // 데이터가 0이면 연결이 끊어진 것으로 판단
                    if (bytesRead == 0)
                    {
                        return false;
                    }

                    return true; // 연결이 유지됨
                }
                return false;
            }
            catch (Exception ex)
            {
                // 예외 발생 시 연결 끊어졌다고 판단
                Console.WriteLine($"Error checking connection: {ex.Message}");
                return false;
            }
        }

        public async Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity)
        {
            if (!_isConnected)
            {
                MessageBox.Show("Modbus Master is not connected.");
                return null;
            }
            else
            {
                return await Task.FromResult(_dataStore.HoldingRegisters.ReadPoints(startAddress, quantity));
            }
        }

        public async Task WriteHoldingRegistersAsync(ushort startAddress, ushort[] values)
        {
            if (!_isConnected)
            {
                MessageBox.Show("Modbus Master is not connected.");
                return;
            }
            else
            {
                await Task.Run(() => _dataStore.HoldingRegisters.WritePoints(startAddress, values));
            }
        }
    }
}
