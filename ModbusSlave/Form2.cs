using ModbusSlave.Models;
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
    public partial class Form2 : Form
    {
        public object InputValue { get; private set; }
        private DataType _dataType;
        public Form2(DataType dataType)
        {
            InitializeComponent();
            _dataType = dataType;
            this.Text = $"Enter {dataType}";
            InitializeInputControls();
        }



        private void InitializeInputControls()
        {
            switch (_dataType)
            {
                case DataType.Signed:
                    // Signed 입력 처리
                    break;
                case DataType.Unsigned:
                    // Unsigned 입력 처리
                    break;
                case DataType.Hex:
                    // Hex 입력 처리
                    break;
                case DataType.Binary:
                    // Binary 입력 처리
                    break;

            }
        }

        // 확인 버튼 클릭 시 값 저장
        private void btnOk_Click(object sender, EventArgs e)
        {
            // 데이터 타입에 맞는 입력값 처리
            // 유효성 검사 후 InputValue에 저장
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
