using ModbusSlave.Interfaces;
using ModbusSlave.Services;
using System;
using System.Windows.Forms;

namespace ModbusSlave
{
    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Modbus Tcp 연결 클래스 인스턴스 생성
            IModbusConnection modbusConnection = new ModbusTcpConnection();

            // ContextMenuService를 먼저 생성
            IContextMenuService contextMenuService = new ContextMenuService();

            // DataViewService 생성
            IDataViewService dataViewService = new DataViewService((ContextMenuService)contextMenuService,modbusConnection);

            // DataViewService가 생성된 이후, ContextMenuService에 설정
            ((ContextMenuService)contextMenuService).SetDataViewService(dataViewService);

            // Form1 생성 시 의존성 주입
            Form1 form1 = new Form1(modbusConnection, dataViewService, contextMenuService);

            Application.Run(form1);
        }
    }
}
