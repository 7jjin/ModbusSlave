using ModbusSlave.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSlave.Services
{
    public class ModbusDataStore : IModbusDataStore
    {
        private readonly ushort[] _holdingRegisters;

        public ModbusDataStore()
        {
            _holdingRegisters = new ushort[100];
        }

        public ushort[] GetHoldingRegisters()
        {
            return _holdingRegisters;
        }

        public void SetHoldingRegisters(ushort index, ushort value)
        {
            if(index >=0 && index < _holdingRegisters.Length)
            {
                _holdingRegisters[index] = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public Task<ushort[]> ReadHoldingRegistersAsync(ushort startAddress, ushort quantity)
        {
            var result = new ushort[quantity];
            Array.Copy(_holdingRegisters, startAddress, result, 0, quantity);
            return Task.FromResult(result);
        }

        public Task WriteHoldingRegistersAsync(ushort startAddress, ushort[] values)
        {
            Array.Copy(values, 0, _holdingRegisters, startAddress, values.Length);
            return Task.CompletedTask;
        }
    }
}
