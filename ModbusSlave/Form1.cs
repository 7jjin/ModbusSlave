using ModbusSlave.Interfaces;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace ModbusSlave
{
    public partial class Form1 : Form
    {
        private readonly IModbusConnection _modbusConnection;
        private readonly IDataViewService _dataViewService;
        private readonly IContextMenuService _contextMenuService;
        private Timer _statusUpdateTimer;

       
        public Form1(IModbusConnection modbusConnection, IDataViewService dataViewService, IContextMenuService contextMenuService)
        {
            InitializeComponent();

            _modbusConnection = modbusConnection;
            _dataViewService = dataViewService;
            _contextMenuService = contextMenuService;

            InitializeDataView();
            InitializeStatusUpdateTimer();
        }

        private void InitializeDataView()
        {
            dataView.MouseDown += DataView_MouseDown;
            txt_ReadAddress.TextChanged += Txt_ReadAddress_TextChanged;
            stlbl_statusCircle.Paint += StatusLabel_Paint;
            stlbl_listenCircle.Paint += ListenLabel_Paint;

            _dataViewService.InitializeDataView(dataView);
            _dataViewService.LoadData(dataView);
        }

        private void InitializeStatusUpdateTimer()
        {
            _statusUpdateTimer = new Timer { Interval = 1000 }; // 1초마다 체크
            _statusUpdateTimer.Tick += async (sender, e) =>
            {
                _modbusConnection.IsConnected = await _modbusConnection.IsMasterConnected();
                Console.WriteLine($"isConnected {_modbusConnection.IsConnected}");
                stlbl_statusCircle.Invalidate(); // 상태 원이 다시 그려지도록 트리거
                stlbl_listenCircle.Invalidate(); // 상태 원이 다시 그려지도록 트리거
            };
            _statusUpdateTimer.Start();
        }

        private void StatusLabel_Paint(object sender, PaintEventArgs e)
        {
            PaintStatusCircle(e, stlbl_statusCircle, _modbusConnection.IsListened && _modbusConnection.IsConnected ? Color.Green : Color.Red);
            tslbl_conectText.Text = _modbusConnection.IsConnected ? "Connected" : "Disconnected";
            tslbl_status.Text = _modbusConnection.LogMessage ?? "No connection";
        }

        private void ListenLabel_Paint(object sender, PaintEventArgs e)
        {
            PaintStatusCircle(e, stlbl_listenCircle, _modbusConnection.IsListened ? Color.Green : Color.Red);
            tslbl_listenText.Text = _modbusConnection.IsListened ? "Listened" : "Not Listened";
        }

        private void PaintStatusCircle(PaintEventArgs e, ToolStripLabel control, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(brush, 0, 0, control.Width - 1, control.Height - 1);
            }
        }

        private void Txt_ReadAddress_TextChanged(object sender, EventArgs e)
        {
            if (ushort.TryParse(txt_ReadAddress.Text, out ushort inputValue))
            {
                int result = inputValue + 40001;
                lbl_ReadPlcAddress.Text = result.ToString();
                dataView.Columns[1].HeaderText = $"{result}";
            }
            else
            {
                MessageBox.Show("올바른 숫자를 입력하세요.");
            }
        }

        private void DataView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = dataView.HitTest(e.X, e.Y);
                if (hitTestInfo.Type == DataGridViewHitTestType.Cell && hitTestInfo.ColumnIndex == 1)
                {
                    _contextMenuService.ShowContextMenu(dataView, hitTestInfo.RowIndex, hitTestInfo.ColumnIndex);
                }
            }
        }

        private async void btnReadData_Click(object sender, EventArgs e)
        {
            try
            {
                ushort startAddress = GetAddressValue(txt_ReadAddress);
                ushort quantity = GetAddressValue(txt_ReadQuantity);
                var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10)); // 5초 타임아웃

                Console.WriteLine("Reading Holding Register...");
                ushort[] holdingRegisters = await _modbusConnection.ReadHoldingRegistersAsync(startAddress, quantity);
                Console.WriteLine($"Holding registers: {holdingRegisters}");
                if (holdingRegisters != null)
                {
                    for (int i = 0; i < holdingRegisters.Length; i++)
                    {
                        dataView.Rows[i].Cells[1].Value = ((short)holdingRegisters[i]).ToString();
                    }
                    _dataViewService.SetCellsToSigned(holdingRegisters.Length - 1);
                    _modbusConnection.LogMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Read {40001 + startAddress} ~ {40001 + startAddress + quantity} data ";
                    statusStrip1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to read data: {ex.Message}");
                _modbusConnection.LogMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Failed to read data";
            }
        }

        private ushort GetAddressValue(TextBox textBox)
        {
            if (!ushort.TryParse(textBox.Text, out ushort value))
                throw new Exception("올바른 숫자를 입력해주세요.");
            return value;
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionForm connectionForm = new ConnectionForm(_modbusConnection, this);
            try
            {
                connectionForm.ShowDialog();
                //_modbusConnection.IsMasterConnected();
                //IsListened = true;
                //LogMessage = "Slave is now listening for connections...";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start listening: {ex.Message}");
                _modbusConnection.LogMessage = "Failed to listen";
            }
            finally
            {
                connectionForm.Dispose();
            }
        }


        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _modbusConnection.Disconnect();
            _modbusConnection.IsConnected = false;
            _modbusConnection.IsListened = false;
            _modbusConnection.LogMessage = "No connection";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
