using ModbusSlave.Interfaces;
using ModbusSlave.Models;
using ModbusSlave.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            dataView.MouseDown += DataView_MouseDown;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _modbusConnection.Connect();
            _dataViewService.InitializeDataView(dataView);
            _dataViewService.LoadData(dataView);
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
    }
}
