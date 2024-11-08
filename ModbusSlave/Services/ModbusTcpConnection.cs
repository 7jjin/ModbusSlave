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
            _connectionTimer = new System.Timers.Timer(2000);
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
                Console.WriteLine("modbus Connect?" , _masterClient.Connected);
                _connectionTimer.Start(); // 연결 성공 후 타이머 시작

                Console.WriteLine("Modbus Master connected.");
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
        private void CheckConnectionStatus(object sender, ElapsedEventArgs e)
        {
            if (!IsMasterConnected())
            {
                Console.WriteLine("Connection lost. Attempting to reconnect...");
                _isConnected = false;
                _connectionTimer.Stop(); // 재연결 시도 중 타이머 일시 정지
            }
        }

        /// <summary>
        /// Master와의 소켓 연결 상태 확인
        /// </summary>
        public bool IsMasterConnected()
        {
            try
            {
                // Master와 연결된 클라이언트 소켓의 상태를 확인
                if (_masterClient?.Client != null &&
                    _masterClient.Client.Poll(0, SelectMode.SelectRead) &&
                    _masterClient.Client.Available == 0)
                {
                    return false; // 연결 끊김
                }
                return true; // 연결 유지 중
            }
            catch
            {
                return false; // 소켓 상태 확인 불가, 연결 끊김으로 간주
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
