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
        private ushort _startAddress;
        public Form2(DataType dataType, ushort address, IModbusConnection modbusConnection)
        {
            InitializeComponent();
            _dataType = dataType;
            _startAddress = address;
            this.Text = $"Enter {dataType}";
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
            ushort valueToSend = ConvertToUshort(_dataType, InputValue.ToString());
            _startAddress -= 40001;

            // 시작 주소 및 Modbus 연결을 위한 접근 (여기서는 startAddress를 e.RowIndex로 가정)
            await _modbusConnection.WriteHoldingRegistersAsync(_startAddress, new ushort[] { valueToSend });
        }

        private ushort ConvertToUshort(DataType dataType, string inputValue)
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

        // 확인 버튼 클릭 시 값 저장
        private void btnOk_Click(object sender, EventArgs e)
        {
            // 데이터 타입에 맞는 입력값 처리
            // 유효성 검사 후 InputValue에 저장
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TextBox_KeyPress_NumericOnly(object sender, KeyPressEventArgs e)
        {
            // Signed 값일 때는 "-"도 허용
            if (_dataType == DataType.Signed && e.KeyChar == '-' && txt_Value.Text.Length == 0)
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
