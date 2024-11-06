using ModbusSlave.Interfaces;
using NModbus;
using NModbus.Data;
using NModbus.Device;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusSlave.Services
{
    public class ModbusTcpConnection : IModbusConnection
    {
        private TcpListener _tcpListener;
        private IModbusSlaveNetwork _slaveNetwork;
        private IModbusSlave _slave;
        private DefaultSlaveDataStore _dataStore;
        private DataGridView _dataView;
        private TcpClient _tcpClient;

        public ModbusTcpConnection(){}

        /// <summary>
        /// Connect 함수 - Slave 연결 설정
        /// </summary>
        public void Connect(string ipAddress, int port, int slaveId)
        {
            try
            {
                _tcpListener = new TcpListener(System.Net.IPAddress.Parse(ipAddress), port);
                _tcpListener.Start();
                var factory = new ModbusFactory();

                // 기본 데이터 저장소 생성 (Coils, Inputs, Holding Registers, Input Registers)
                _dataStore = new DefaultSlaveDataStore();

                // Slave 생성 및 네트워크 설정
                _slave = factory.CreateSlave((byte)slaveId, _dataStore);
                _slaveNetwork = factory.CreateSlaveNetwork(_tcpListener);
                _slaveNetwork.AddSlave(_slave);

                // 비동기적으로 요청을 처리하도록 설정
                Task.Run(async () => await _slaveNetwork.ListenAsync());

                // Slave 연결 시작
                Console.WriteLine("Modbus Slave connected successfully.");
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

                // TcpListener 중지 - 연결된 클라이언트가 연결 끊김을 감지하게 됨
                if (_tcpListener != null)
                {
                    _tcpListener.Server.Close();
                    _tcpListener = null;
                }

                // SlaveNetwork와 Slave 리소스 정리
                _slaveNetwork.Dispose();
                _slave = null;

                MessageBox.Show("Slave disconnected successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error disconnecting: {ex.Message}");
            }
        }


        // 특정 Holding Register 값 업데이트
        public async Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity)
        {
            if (_slave == null)
            {
                MessageBox.Show("ModbusMaster is not connected.");
                return null;
            }
            else
            {
                // 기존 데이터 읽기 로직
                return await Task.FromResult(_dataStore.HoldingRegisters.ReadPoints(startAddress, quantity));
            }
        }

        public async Task WriteHoldingRegistersAsync(ushort startAddress, ushort[] values)
        {
            if (_slave == null)
            {
                MessageBox.Show("ModbusMaster is not connected.");
                return;
            }
            else
            {
                // 값들을 Holding Register에 쓰기
                await Task.Run(() => _dataStore.HoldingRegisters.WritePoints(startAddress, values));
            }
        }
    }
}
