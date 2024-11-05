using ModbusSlave.Interfaces;
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
    public partial class ConnectionForm : Form
    {
        private IModbusConnection _modbusConnection;
        private Form1 _form1;

        public ConnectionForm(IModbusConnection modbusConnection, Form1 form1)
        {
            InitializeComponent();
            _modbusConnection = modbusConnection;
            _form1 = form1;
        }

        /// <summary>
        /// Connection Function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Connect_Click_1(object sender, EventArgs e)
        {
            string ipAddress = txt_IpAddress.Text;
            int port = int.Parse(txt_Port.Text);
            int slaveId = int.Parse(txt_SlaveId.Text);
            string currentTime = DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]");
            try
            {
                _modbusConnection.Connect(ipAddress, port, slaveId);
                _form1.IsConnected = true;
                _form1.LogMessage = $"{currentTime} connected slaveId - {slaveId} {ipAddress} {port}";

                this.Close();
            }
            catch (Exception ex)
            {
                _form1.IsConnected = false;
                _form1.LogMessage = $"{currentTime} Slave Connection Failed. {ipAddress}:{port}";
            }
        }

        /// <summary>
        /// 창 닫는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
