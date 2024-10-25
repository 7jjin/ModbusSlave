using ModbusSlave.Interfaces;
using ModbusSlave.Models;
using ModbusSlave.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusSlave
{
    public partial class Form1 : Form
    {
        private IModbusConnection _modbusConnection;
        private readonly IDataViewService _dataViewService;
        private readonly IContextMenuService _contextMenuService;
        public Form1(IModbusConnection modbusConnection, IDataViewService dataViewService, IContextMenuService contextMenuService)
        {
            InitializeComponent();
            
            this.Text = "Modbus Slave";
            _modbusConnection = modbusConnection;
            _dataViewService = dataViewService;
            _contextMenuService = contextMenuService;

            _modbusConnection.Connect();
            dataView.MouseDown += DataView_MouseDown;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            _dataViewService.InitializeDataView(dataView);
            _dataViewService.LoadData(dataView);

            txt_ReadAddress.Text = "0";
            txt_ReadQuantity.Text = "10";
            txt_WriteAddress.Text = "0";
            txt_WriteQuantity.Text = "10";
        }

        /// <summary>
        /// DataView 1열 클릭시 ContextMenu 생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dataView.HitTest(e.X, e.Y);
                if (hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex == 1)
                {
                    int rowIndex = hitTestInfo.RowIndex;
                    int columnIndex = hitTestInfo.ColumnIndex;  

                    _contextMenuService.ShowContextMenu(dataView, rowIndex,columnIndex);

                }
            }
            
        }

        private async void btnReadData_Click(object sender, EventArgs e)
        {
            try
            {
                ushort startAddress;
                if (!ushort.TryParse(txt_ReadAddress.Text, out startAddress))
                {
                    MessageBox.Show("올바른 주소 값을 입력해주세요.");
                    return;
                }
                ushort quantity;
                if (!ushort.TryParse(txt_ReadQuantity.Text, out quantity))
                {
                    MessageBox.Show("올바른 수량 값을 입력해주세요.");
                    return;
                }

                ushort[] holdingRegisters = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, quantity);

                for (int i = 0; i < holdingRegisters.Length; i++)
                {
                    // 16bit Signed값으로 변경 
                    if (i < dataView.Rows.Count) 
                    {
                        int signedValue = (short)holdingRegisters[i];
                        string displayValue;

                        if(signedValue < -32768 || signedValue > 32767)
                        {
                            displayValue = holdingRegisters[i].ToString();
                        }
                        else
                        {
                            displayValue = signedValue.ToString();
                        }
                        dataView.Rows[i].Cells[1].Value = displayValue;
                        dataView.AllowUserToAddRows = true;
                    }
                }

                MessageBox.Show("Successfully read data from Modbus Master.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to read data: {ex.Message}");
            }
        }


        private async void btnWriteData_Click_1(object sender, EventArgs e)
        {
            try
            {
                ushort startAddress;
                if (!ushort.TryParse(txt_WriteAddress.Text, out startAddress))
                {
                    MessageBox.Show("올바른 주소 값을 입력해주세요.");
                    return;
                }
                ushort quantity;
                if (!ushort.TryParse(txt_WriteQuantity.Text, out quantity))
                {
                    MessageBox.Show("올바른 수량 값을 입력해주세요.");
                    return;
                }

                ushort[] valuesToWrite = new ushort[quantity];
                for (int i = 0; i < quantity; i++)
                {
                    string cellValue = dataView.Rows[i].Cells[1].Value?.ToString();
                    if (string.IsNullOrEmpty(cellValue))
                    {
                        MessageBox.Show("입력한 수량의 모든 셀에 데이터를 입력해주세요.");
                        return;
                    }

                    ushort unsignedValue;
                    if (cellValue.StartsWith("0x"))
                    {
                        unsignedValue = ushort.Parse(cellValue.Substring(2), NumberStyles.HexNumber);
                    }
                    else if (short.TryParse(cellValue, out short signedValue))
                    {
                        unsignedValue = (ushort)signedValue;
                    }
                    else if (cellValue.All(c => c == '0' || c == '1') && (cellValue.Length == 8 || cellValue.Length == 16))
                    {
                        unsignedValue = Convert.ToUInt16(cellValue.Substring(2), 2);
                    }
                    else if (!ushort.TryParse(cellValue, out unsignedValue))
                    {
                        MessageBox.Show("데이터 형식이 올바르지 않습니다.");
                        return;
                    }


                    valuesToWrite[i] = unsignedValue;
                }
                await _modbusConnection.WriteHoldingRegistersAsync(startAddress, valuesToWrite);

                MessageBox.Show("데이터가 성공적으로 전송되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
