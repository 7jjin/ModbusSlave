using ModbusSlave.Interfaces;
using ModbusSlave.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ModbusSlave
{
    public partial class Form2 : Form
    {
        private IModbusConnection _modbusConnection;
        public object InputValue { get; private set; }
        private DataType _dataType;
        private EndianType _endianType;
        private ushort _startAddress;
        public Form2(DataType dataType, ushort address, IModbusConnection modbusConnection,EndianType endianType)
        {
            InitializeComponent();
            _dataType = dataType;
            _endianType = endianType;
            _startAddress = address;
            this.Text = $"Enter {dataType} {endianType}";
            _modbusConnection = modbusConnection;
           
            btnOk.Click += async (s, e) =>
            {
                if (ValidateInput(dataType))
                {
                    InputValue = txt_Value.Text;
                    this.DialogResult = DialogResult.OK;
                    await SendDataToModbusMasterAsync();
                    this.Close();
                }
            };
            btnCancel.Click += (s, e) =>
            {
                this.Close();
            };

        }


        // 데이터 전송 메서드
        private async Task SendDataToModbusMasterAsync()
        {
            // DataType에 따라 데이터를 ushort로 변환
            ushort[] valuesToSend = ConvertToUshortArray(_dataType, InputValue.ToString(), _endianType);
            _startAddress -= 40001;


            await _modbusConnection.WriteHoldingRegistersAsync(_startAddress, valuesToSend);
        }

        /// <summary>
        /// DataType과 EndianType에 맞춰 배열을 만들어 전달하는 함수
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="inputValue"></param>
        /// <param name="endianType"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        // 32비트 및 64비트 데이터를 ushort 배열로 변환
        private ushort[] ConvertToUshortArray(DataType dataType, string inputValue, EndianType endianType)
        {
            switch (dataType)
            {
                case DataType.Signed32:
                    int signed32Value = Int32.Parse(inputValue);
                    return ConvertSigned32BitToUshortArray(signed32Value, endianType);

                case DataType.Unsigned32:
                    uint unsigned32Value = UInt32.Parse(inputValue);
                    return ConvertUnsigned32BitToUshortArray(unsigned32Value, endianType);

                case DataType.Signed64:
                    long signed64Value = long.Parse(inputValue);
                    return ConvertSigned64BitToUshortArray(signed64Value, endianType);

                case DataType.Unsigned64:
                    ulong unsigned64Value = ulong.Parse(inputValue);
                    return ConvertUnsigned64BitToUshortArray(unsigned64Value, endianType);

                default:
                    return new[] { ConvertToUshort(dataType, inputValue, endianType) };
            }
        }

        /// <summary>
        /// 32 bit Signed
        /// </summary>
        /// <param name="value"></param>
        /// <param name="endianType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private ushort[] ConvertSigned32BitToUshortArray(int value, EndianType endianType)
        {
            switch (endianType)
            {
                case EndianType.BigEndian:
                    // 32bit 정수를 Big-endian 형식의 바이트 배열로 변환
                    byte[] bigEndian = BitConverter.GetBytes(value);

                    // 시스템이 Little-endian인 경우 Big-endian 순서를 맞추기 위해 바이트 순서를 뒤집음
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(bigEndian);
                    }

                    // 상위 16비트와 하위 16비트를 그대로 추출
                    ushort[] result = new ushort[2];
                    result[0] = (ushort)((bigEndian[0] << 8) | bigEndian[1]); // 상위 16비트
                    result[1] = (ushort)((bigEndian[2] << 8) | bigEndian[3]); // 하위 16비트

                    return result;
                case EndianType.LittleEndian:
                    byte[] littleEndian = BitConverter.GetBytes(value);

                    // 상위 16비트와 하위 16비트를 Little-endian 형식으로 추출
                    return new ushort[]
                    {
                (ushort)((littleEndian[0] << 8) | littleEndian[1]), // 하위 16비트
                (ushort)((littleEndian[2] << 8) | littleEndian[3])  // 상위 16비트
                    };
                case EndianType.BigEndianByteSwap:
                    byte[] bigEndianByteSwap = BitConverter.GetBytes(value);

                    // 상위 16비트와 하위 16비트를 Little-endian 형식으로 추출
                    return new ushort[]
                    {
                (ushort)((bigEndianByteSwap[2] << 8) | bigEndianByteSwap[3]), // 하위 16비트
                (ushort)((bigEndianByteSwap[0] << 8) | bigEndianByteSwap[1])  // 상위 16비트
                    };
                case EndianType.LittleEndianByteSwap:
                    // value를 32비트 unsigned로 처리
                    uint swappedValue = unchecked((uint)value);

                    // 상위 16비트와 하위 16비트를 추출
                    ushort upperValue = (ushort)(swappedValue & 0xFFFF);           // 하위 16비트
                    ushort lowerValue = (ushort)((swappedValue >> 16) & 0xFFFF);   // 상위 16비트

                    // Little-endian Byte Swap 유지
                    return new ushort[] { upperValue, lowerValue };
                default:
                    throw new ArgumentException("Unsupported EndianType");
            }
        }

        /// <summary>
        /// 32bit Unsigned
        /// </summary>
        /// <param name="value"></param>
        /// <param name="endianType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private ushort[] ConvertUnsigned32BitToUshortArray(uint value, EndianType endianType)
        {
            switch (endianType)
            {
                case EndianType.BigEndian:
                    // 32비트 unsigned 정수를 Big-endian 형식의 바이트 배열로 변환
                    byte[] bigEndian = BitConverter.GetBytes(value);

                    // 시스템이 Little-endian인 경우 Big-endian 순서를 맞추기 위해 바이트 순서를 뒤집음
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(bigEndian);
                    }

                    // 상위 16비트와 하위 16비트를 그대로 추출
                    return new ushort[]
                    {
                (ushort)((bigEndian[0] << 8) | bigEndian[1]), // 상위 16비트
                (ushort)((bigEndian[2] << 8) | bigEndian[3])  // 하위 16비트
                    };

                case EndianType.LittleEndian:
                    byte[] littleEndian = BitConverter.GetBytes(value);

                    // 상위 16비트와 하위 16비트를 Little-endian 형식으로 추출
                    return new ushort[]
                    {
                (ushort)((littleEndian[0] << 8) | littleEndian[1]), // 하위 16비트
                (ushort)((littleEndian[2] << 8) | littleEndian[3])  // 상위 16비트
                    };

                case EndianType.BigEndianByteSwap:
                    byte[] bigEndianByteSwap = BitConverter.GetBytes(value);

                    // Big-endian Byte Swap 적용
                    return new ushort[]
                    {
                (ushort)((bigEndianByteSwap[2] << 8) | bigEndianByteSwap[3]), // 하위 16비트 (스왑 적용)
                (ushort)((bigEndianByteSwap[0] << 8) | bigEndianByteSwap[1])  // 상위 16비트 (스왑 적용)
                    };

                case EndianType.LittleEndianByteSwap:
                    // Little-endian Byte Swap 유지
                    return new ushort[]
                    {
                (ushort)(value & 0xFFFF),          // 하위 16비트
                (ushort)((value >> 16) & 0xFFFF)   // 상위 16비트
                    };

                default:
                    throw new ArgumentException("Unsupported EndianType");
            }
        }

        /// <summary>
        /// 64bit Signed
        /// </summary>
        /// <param name="value"></param>
        /// <param name="endianType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private ushort[] ConvertSigned64BitToUshortArray(long value, EndianType endianType)
        {
            switch (endianType)
            {
                case EndianType.BigEndian:
                    // 64비트 정수를 Big-endian 형식의 바이트 배열로 변환
                    byte[] bigEndian = BitConverter.GetBytes(value);

                    // 시스템이 Little-endian인 경우 Big-endian 순서를 맞추기 위해 바이트 순서를 뒤집음
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(bigEndian);
                    }

                    // 각 16비트 값으로 분할
                    return new ushort[]
                    {
                (ushort)((bigEndian[0] << 8) | bigEndian[1]), // 최상위 16비트
                (ushort)((bigEndian[2] << 8) | bigEndian[3]), // 두 번째 상위 16비트
                (ushort)((bigEndian[4] << 8) | bigEndian[5]), // 두 번째 하위 16비트
                (ushort)((bigEndian[6] << 8) | bigEndian[7])  // 최하위 16비트
                    };

                case EndianType.LittleEndian:
                    byte[] littleEndian = BitConverter.GetBytes(value);

                    // 각 16비트 값을 Little-endian 형식으로 추출
                    return new ushort[]
                    {
                (ushort)((littleEndian[0] << 8) | littleEndian[1]), // 최하위 16비트
                (ushort)((littleEndian[2] << 8) | littleEndian[3]), // 두 번째 하위 16비트
                (ushort)((littleEndian[4] << 8) | littleEndian[5]), // 두 번째 상위 16비트
                (ushort)((littleEndian[6] << 8) | littleEndian[7])  // 최상위 16비트
                    };

                case EndianType.BigEndianByteSwap:
                    byte[] bigEndianByteSwap = BitConverter.GetBytes(value);

                    // Big-endian에서 바이트 스왑 적용
                    return new ushort[]
                    {
                (ushort)((bigEndianByteSwap[6] << 8) | bigEndianByteSwap[7]), // 최하위 16비트
                (ushort)((bigEndianByteSwap[4] << 8) | bigEndianByteSwap[5]), // 두 번째 하위 16비트
                (ushort)((bigEndianByteSwap[2] << 8) | bigEndianByteSwap[3]), // 두 번째 상위 16비트
                (ushort)((bigEndianByteSwap[0] << 8) | bigEndianByteSwap[1])  // 최상위 16비트
                    };

                case EndianType.LittleEndianByteSwap:
                    // 64비트 값을 Little-endian Byte Swap 형식으로 변환
                    ulong swappedValue = unchecked((ulong)value);

                    // 각 16비트 값을 Little-endian Byte Swap 구조로 분할
                    return new ushort[]
                    {
                (ushort)(swappedValue & 0xFFFF),                    // 최하위 16비트
                (ushort)((swappedValue >> 16) & 0xFFFF),            // 두 번째 하위 16비트
                (ushort)((swappedValue >> 32) & 0xFFFF),            // 두 번째 상위 16비트
                (ushort)((swappedValue >> 48) & 0xFFFF)             // 최상위 16비트
                    };

                default:
                    throw new ArgumentException("Unsupported EndianType");
            }
        }
        /// <summary>
        /// 64bit Unsigned
        /// </summary>
        /// <param name="value"></param>
        /// <param name="endianType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private ushort[] ConvertUnsigned64BitToUshortArray(ulong value, EndianType endianType)
        {
            switch (endianType)
            {
                case EndianType.BigEndian:
                    // 64비트 unsigned 정수를 Big-endian 형식의 바이트 배열로 변환
                    byte[] bigEndian = BitConverter.GetBytes(value);

                    // 시스템이 Little-endian인 경우 Big-endian 순서를 맞추기 위해 바이트 순서를 뒤집음
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(bigEndian);
                    }

                    // 각 16비트 값으로 분할
                    return new ushort[]
                    {
                (ushort)((bigEndian[0] << 8) | bigEndian[1]), // 최상위 16비트
                (ushort)((bigEndian[2] << 8) | bigEndian[3]), // 두 번째 상위 16비트
                (ushort)((bigEndian[4] << 8) | bigEndian[5]), // 두 번째 하위 16비트
                (ushort)((bigEndian[6] << 8) | bigEndian[7])  // 최하위 16비트
                    };

                case EndianType.LittleEndian:
                    byte[] littleEndian = BitConverter.GetBytes(value);

                    // 각 16비트 값을 Little-endian 형식으로 추출
                    return new ushort[]
                    {
                (ushort)((littleEndian[0] << 8) | littleEndian[1]), // 최하위 16비트
                (ushort)((littleEndian[2] << 8) | littleEndian[3]), // 두 번째 하위 16비트
                (ushort)((littleEndian[4] << 8) | littleEndian[5]), // 두 번째 상위 16비트
                (ushort)((littleEndian[6] << 8) | littleEndian[7])  // 최상위 16비트
                    };

                case EndianType.BigEndianByteSwap:
                    byte[] bigEndianByteSwap = BitConverter.GetBytes(value);

                    // Big-endian에서 바이트 스왑 적용
                    return new ushort[]
                    {
                (ushort)((bigEndianByteSwap[6] << 8) | bigEndianByteSwap[7]), // 최하위 16비트
                (ushort)((bigEndianByteSwap[4] << 8) | bigEndianByteSwap[5]), // 두 번째 하위 16비트
                (ushort)((bigEndianByteSwap[2] << 8) | bigEndianByteSwap[3]), // 두 번째 상위 16비트
                (ushort)((bigEndianByteSwap[0] << 8) | bigEndianByteSwap[1])  // 최상위 16비트
                    };

                case EndianType.LittleEndianByteSwap:
                    // 64비트 값을 Little-endian Byte Swap 형식으로 변환
                    // unsigned로 처리하므로 ulong로 변환
                    ulong swappedValue = unchecked((ulong)value);

                    // 각 16비트 값을 Little-endian Byte Swap 구조로 분할
                    return new ushort[]
                    {
                (ushort)(swappedValue & 0xFFFF),                    // 최하위 16비트
                (ushort)((swappedValue >> 16) & 0xFFFF),            // 두 번째 하위 16비트
                (ushort)((swappedValue >> 32) & 0xFFFF),            // 두 번째 상위 16비트
                (ushort)((swappedValue >> 48) & 0xFFFF)             // 최상위 16비트
                    };

                default:
                    throw new ArgumentException("Unsupported EndianType");
            }
        }

        private ushort[] ConvertSigned32BitToBigEndianParts(int value)
        {
            // 32bit 정수를 Big-endian 형식의 바이트 배열로 변환
            byte[] bytes = BitConverter.GetBytes(value);

            // 시스템이 Little-endian인 경우 Big-endian 순서를 맞추기 위해 바이트 순서를 뒤집음
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            // 상위 16비트와 하위 16비트를 그대로 추출
            ushort[] result = new ushort[2];
            result[0] = (ushort)((bytes[0] << 8) | bytes[1]); // 상위 16비트
            result[1] = (ushort)((bytes[2] << 8) | bytes[3]); // 하위 16비트

            return result;
        }

        // Long 값을 Endian 방식에 따라 ushort 배열로 변환
        private ushort[] ConvertLongToUshortArray(long value, int byteSize, EndianType endianType)
        {
            byte[] bytes = BitConverter.GetBytes(value);

            if ((BitConverter.IsLittleEndian && endianType == EndianType.BigEndian) ||
                (!BitConverter.IsLittleEndian && endianType == EndianType.LittleEndian))
            {
                Array.Reverse(bytes);
            }

            // ushort로 2바이트씩 분리
            ushort[] result = new ushort[bytes.Length/ 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = BitConverter.ToUInt16(bytes, i * 2);
            }

            return result;
        }

        private ushort ConvertToUshort(DataType dataType, string inputValue,EndianType endianType)
        {
            ushort result;
            switch (dataType)
            {
                case DataType.Hex:
                    result = ushort.Parse(inputValue.Substring(2), System.Globalization.NumberStyles.HexNumber);
                    break;
                case DataType.Binary:
                    result = Convert.ToUInt16(inputValue, 2);
                    break;
                case DataType.Signed:
                    result = (ushort)short.Parse(inputValue);
                    break;
                case DataType.Unsigned:
                    result = ushort.Parse(inputValue);
                    break;
                default:
                    throw new InvalidOperationException("지원되지 않는 데이터 타입입니다.");
            }
            return result;
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {
            txt_Value.KeyPress -= TextBox_KeyPress_NumericOnly;
            if (_dataType == DataType.Signed || _dataType == DataType.Unsigned || _dataType == DataType.Signed32 || _dataType == DataType.Unsigned32 || _dataType == DataType.Signed64 || _dataType == DataType.Unsigned64)
            {
                txt_Value.KeyPress += TextBox_KeyPress_NumericOnly;
            }else if(_dataType == DataType.Hex)
            {
                txt_Value.KeyPress += TextBox_KeyPress_HexOnly;
            }else if(_dataType == DataType.Binary)
            {
                txt_Value.KeyPress += TextBox_KeyPress_BinaryOnly;
            }

            // KeyDown 이벤트 추가
            txt_Value.KeyDown += Txt_Value_KeyDown;
        }

        // Enter 키 눌림 처리
        private void Txt_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick(); // btnOk 클릭 이벤트 호출
                e.Handled = true; // 이벤트 처리 완료
            }
        }

        /// <summary>
        /// 데이터 타입에 따른 유효성 검사 확인 함수
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private bool ValidateInput(DataType dataType)
        {
            int value;
            long longValue;
            ulong ulongValue;
            switch (_dataType)
            {
                case DataType.Signed:
                    
                    // Signed 입력 처리
                    if (!int.TryParse(txt_Value.Text, out value) || value < -32768 || value > 32767)
                    {
                        MessageBox.Show("Signed 값은 -32768 ~ 32767 사이여야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
                case DataType.Unsigned:
                    // Unsigned 입력 처리
                    if (!int.TryParse(txt_Value.Text, out value) || value <= 0 || value > 65535)
                    {
                        MessageBox.Show("Unsigned 값은 0 ~ 65535 사이여야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
                case DataType.Signed32:
                    // Signed 32-bit 입력 처리
                    if (!long.TryParse(txt_Value.Text, out longValue) || longValue < -2147483648L || longValue > 2147483647L)
                    {
                        MessageBox.Show("Signed32 값은 -2147483648 ~ 2147483647 사이여야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;

                case DataType.Unsigned32:
                    // Unsigned 32-bit 입력 처리
                    if (!ulong.TryParse(txt_Value.Text, out ulongValue) || ulongValue > 4294967295UL)
                    {
                        MessageBox.Show("Unsigned32 값은 0 ~ 4294967295 사이여야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;

                case DataType.Signed64:
                    // Signed 64-bit 입력 처리
                    if (!long.TryParse(txt_Value.Text, out longValue) || longValue < -9223372036854775808L || longValue > 9223372036854775807L)
                    {
                        MessageBox.Show("Signed64 값은 -9223372036854775808 ~ 9223372036854775807 사이여야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;

                case DataType.Unsigned64:
                    // Unsigned 64-bit 입력 처리
                    if (!ulong.TryParse(txt_Value.Text, out ulongValue) || ulongValue > 18446744073709551615UL)
                    {
                        MessageBox.Show("Unsigned64 값은 0 ~ 18446744073709551615 사이여야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
                case DataType.Hex:
                    // Hex 입력 처리
                    string hexValue = txt_Value.Text;

                    // '0x'로 시작하는지 확인하고, 16진수 값의 범위를 확인
                    if (!hexValue.StartsWith("0x"))
                    {
                        MessageBox.Show("Hex 형식은 '0x'로 시작해야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        // 16진수로 변환 및 범위 검사
                        if (int.TryParse(hexValue.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out value))
                        {
                            if (value < 0x0000 || value > 0xFFFF)
                            {
                                MessageBox.Show("Hex 값은 0x0000 ~ 0xFFFF 사이여야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("유효하지 않은 Hex 값입니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    break;
                case DataType.Binary:
                    // Binary 입력 처리
                    // Binary 형식 검사 (0과 1만 허용) 및 길이 제한 (예: 16비트 이하)
                    string binaryValue = txt_Value.Text.Replace(" ", "");
                    if (!Regex.IsMatch(binaryValue, "^[01]+$"))
                    {
                        MessageBox.Show("Binary 형식은 0과 1로만 구성되어야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // 16비트 이하로 제한 (최대 16자리)
                    if (binaryValue.Length > 16)
                    {
                        MessageBox.Show("Binary 값은 최대 16비트여야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
                default:
                    MessageBox.Show("알 수 없는 데이터 타입입니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
            }
            return true;
        }


        private void TextBox_KeyPress_NumericOnly(object sender, KeyPressEventArgs e)
        {
            // Signed 값일 때는 "-"도 허용
            if ((_dataType == DataType.Signed || _dataType == DataType.Signed32 || _dataType == DataType.Signed64) && e.KeyChar == '-' && txt_Value.Text.Length == 0)
            {
                // 첫 번째 문자로만 "-" 허용
                e.Handled = false;
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TextBox_KeyPress_HexOnly(object sender, KeyPressEventArgs e)
        {
            // 현재 입력된 텍스트
            string currentText = txt_Value.Text;

            // 첫 번째 문자는 반드시 '0'이어야 함
            if (currentText.Length == 0 && e.KeyChar != '0' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // 두 번째 문자는 반드시 'x'이어야 함
            if (currentText.Length == 1 && e.KeyChar != 'x' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // 세 번째 문자 이후부터는 최대 4개의 16진수 문자만 입력 가능
            if (currentText.Length >= 2)
            {
                // 0x 뒤로 최대 4개의 값만 허용
                if (currentText.Length > 5 && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }

                // 16진수 문자인지 확인하고, 제어 문자는 허용
                if (!Uri.IsHexDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }


        private void TextBox_KeyPress_BinaryOnly(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '0' && e.KeyChar != '1' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


    }
}
