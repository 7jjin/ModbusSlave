﻿using ModbusSlave.Interfaces;
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
        public string LogMessage { get; set; }
        public bool IsConnected
        {
            get => _isConnected;
            set
            {
                _isConnected = value;
                stlbl_statusCircle.Invalidate();  // 상태 라벨 다시 그리기
            }
        }
        private bool _isConnected;
        public Form1(IModbusConnection modbusConnection, IDataViewService dataViewService, IContextMenuService contextMenuService)
        {
            InitializeComponent();
            
            this.Text = "Modbus Slave";
            _modbusConnection = modbusConnection;
            _dataViewService = dataViewService;
            _contextMenuService = contextMenuService;

            
            dataView.MouseDown += DataView_MouseDown;
            txt_ReadAddress.TextChanged += Txt_ReadAddress_TextChanged;
            stlbl_statusCircle.Paint += StatusLabel_Paint;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
           
            _dataViewService.InitializeDataView(dataView);
            _dataViewService.LoadData(dataView);

            txt_ReadAddress.Text = "0";
            txt_ReadQuantity.Text = "10";
            IsConnected = false;
            tslbl_conectText.Text = "Disconnected";
            LogMessage = "No connection";

            stlbl_statusCircle.Invalidate(); // 초기 상태를 반영하도록 강제로 다시 그리기
        }

        private void StatusLabel_Paint(object sender, PaintEventArgs e)
        {
            Color color = new Color();
            string currentTime = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]");
            if (!IsConnected)
            {
                if (IsConnected == false)
                {
                    Console.WriteLine("Slave의 연결이 해제되었습니다.");
                    color = Color.Red;
                    tslbl_conectText.Text = "Disconnected";
                    tslbl_status.Text = LogMessage ?? "No connection";
                }
            }
            else
            {
                Console.WriteLine("연결을 성공했습니다.");
                color = Color.Green;
                tslbl_conectText.Text = "Connected";
                tslbl_status.Text = LogMessage;

            }


            using (SolidBrush brush = new SolidBrush(color))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // 부드럽게 원 그리기
                e.Graphics.FillEllipse(brush, 0, 0, stlbl_statusCircle.Width - 1, stlbl_statusCircle.Height - 1); // 원 그리기
            }

        }

        /// <summary>
        /// ReadAddress 텍스트 박스의 입력값이 바뀔 때마다 PLC Label값도 바뀜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_ReadAddress_TextChanged(object sender, EventArgs e)
        {
            if (ushort.TryParse(txt_ReadAddress.Text, out ushort inputValue))
            {
                // 40001을 더한 값 계산
                int result = inputValue + 40001;
                

                // 계산 결과를 Label에 표시
                lbl_ReadPlcAddress.Text = result.ToString();
                dataView.Columns[1].HeaderText = $"{result}";
            }
            else
            {
                // 변환이 실패하면 에러 메시지 표시
                MessageBox.Show("올바른 숫자를 입력하세요.");
            }
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
                _dataViewService.SetCellsToSigned(holdingRegisters.Length - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to read data: {ex.Message}");
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionForm connectionForm = new ConnectionForm(_modbusConnection, this);
            connectionForm.ShowDialog();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
