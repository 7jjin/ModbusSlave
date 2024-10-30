
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

        public ModbusTcpConnection()
        {
            _tcpListener = new TcpListener(System.Net.IPAddress.Parse("127.0.0.1"), 502);
            // 기본 데이터 저장소 생성 (Coils, Inputs, Holding Registers, Input Registers)
            _dataStore = new DefaultSlaveDataStore();
        }

        public void Connect(int slaveId)
        {
            try
            {
                _tcpListener.Start();
                var factory = new ModbusFactory();

                // Slave ID 1을 가진 Slave 생성
                _slave = factory.CreateSlave((byte)slaveId, _dataStore);

                // Modbus Slave 네트워크 생성 및 Slave 추가
                _slaveNetwork = factory.CreateSlaveNetwork(_tcpListener);
                _slaveNetwork.AddSlave(_slave);

                // 비동기적으로 요청을 처리하도록 설정
                Task.Run(async () => await _slaveNetwork.ListenAsync());

                MessageBox.Show("접속을 시작합니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // 특정 Holding Register 값 업데이트
        // 특정 Holding Register 값 업데이트
        public async Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity)
        {
            if (_slave == null)
            {
                throw new InvalidOperationException("ModbusMaster is not connected.");
            }

            // 기존 데이터 읽기 로직
            return await Task.FromResult(_dataStore.HoldingRegisters.ReadPoints(startAddress, quantity));
        }
        public async Task WriteHoldingRegistersAsync(ushort startAddress, ushort[] values)
        {
            if (_slave == null)
            {
                throw new InvalidOperationException("ModbusMaster is not connected.");
            }

            // 값들을 Holding Register에 쓰기
            await Task.Run(() => _dataStore.HoldingRegisters.WritePoints(startAddress, values));
        }

    }
}
