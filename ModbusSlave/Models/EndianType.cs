using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSlave.Models
{
    public enum EndianType
    {
        none,
        BigEndian,
        LittleEndian,
        BigEndianByteSwap,
        LittleEndianByteSwap
    }
}
