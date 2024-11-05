namespace ModbusSlave
{
    partial class ConnectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_Port = new System.Windows.Forms.TextBox();
            this.lbl_Port = new System.Windows.Forms.Label();
            this.txt_IpAddress = new System.Windows.Forms.TextBox();
            this.lbl_IpAddress = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lbl_FunctionCode = new System.Windows.Forms.Label();
            this.txt_SlaveId = new System.Windows.Forms.TextBox();
            this.lbl_SlaveId = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_Port);
            this.groupBox3.Controls.Add(this.lbl_Port);
            this.groupBox3.Controls.Add(this.txt_IpAddress);
            this.groupBox3.Controls.Add(this.lbl_IpAddress);
            this.groupBox3.Location = new System.Drawing.Point(12, 253);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(411, 107);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TCP/IP Server";
            // 
            // txt_Port
            // 
            this.txt_Port.Location = new System.Drawing.Point(291, 54);
            this.txt_Port.Name = "txt_Port";
            this.txt_Port.Size = new System.Drawing.Size(79, 25);
            this.txt_Port.TabIndex = 3;
            this.txt_Port.Text = "502";
            // 
            // lbl_Port
            // 
            this.lbl_Port.AutoSize = true;
            this.lbl_Port.Location = new System.Drawing.Point(288, 32);
            this.lbl_Port.Name = "lbl_Port";
            this.lbl_Port.Size = new System.Drawing.Size(34, 15);
            this.lbl_Port.TabIndex = 2;
            this.lbl_Port.Text = "Port";
            // 
            // txt_IpAddress
            // 
            this.txt_IpAddress.Location = new System.Drawing.Point(18, 54);
            this.txt_IpAddress.Name = "txt_IpAddress";
            this.txt_IpAddress.Size = new System.Drawing.Size(235, 25);
            this.txt_IpAddress.TabIndex = 1;
            this.txt_IpAddress.Text = "127.0.0.1";
            // 
            // lbl_IpAddress
            // 
            this.lbl_IpAddress.AutoSize = true;
            this.lbl_IpAddress.Location = new System.Drawing.Point(15, 32);
            this.lbl_IpAddress.Name = "lbl_IpAddress";
            this.lbl_IpAddress.Size = new System.Drawing.Size(78, 15);
            this.lbl_IpAddress.TabIndex = 0;
            this.lbl_IpAddress.Text = "IP Address";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.lbl_FunctionCode);
            this.groupBox2.Controls.Add(this.txt_SlaveId);
            this.groupBox2.Controls.Add(this.lbl_SlaveId);
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 135);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Slave Definition";
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "03 Holding Register (4x)"});
            this.comboBox2.Location = new System.Drawing.Point(110, 88);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(258, 23);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.Text = "03 Holding Register (4x)";
            // 
            // lbl_FunctionCode
            // 
            this.lbl_FunctionCode.AutoSize = true;
            this.lbl_FunctionCode.Location = new System.Drawing.Point(26, 91);
            this.lbl_FunctionCode.Name = "lbl_FunctionCode";
            this.lbl_FunctionCode.Size = new System.Drawing.Size(63, 15);
            this.lbl_FunctionCode.TabIndex = 2;
            this.lbl_FunctionCode.Text = "Function";
            // 
            // txt_SlaveId
            // 
            this.txt_SlaveId.Location = new System.Drawing.Point(110, 37);
            this.txt_SlaveId.Name = "txt_SlaveId";
            this.txt_SlaveId.Size = new System.Drawing.Size(96, 25);
            this.txt_SlaveId.TabIndex = 1;
            this.txt_SlaveId.Text = "1";
            // 
            // lbl_SlaveId
            // 
            this.lbl_SlaveId.AutoSize = true;
            this.lbl_SlaveId.Location = new System.Drawing.Point(26, 43);
            this.lbl_SlaveId.Name = "lbl_SlaveId";
            this.lbl_SlaveId.Size = new System.Drawing.Size(62, 15);
            this.lbl_SlaveId.TabIndex = 0;
            this.lbl_SlaveId.Text = "Slave ID";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(348, 57);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 7;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(348, 23);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(75, 23);
            this.btn_Connect.TabIndex = 6;
            this.btn_Connect.Text = "OK";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 68);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Modbus TCP/IP"});
            this.comboBox1.Location = new System.Drawing.Point(18, 26);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(261, 23);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Modbus TCP/IP";
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 375);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConnectionForm";
            this.Text = "ConnectionForm";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_Port;
        private System.Windows.Forms.Label lbl_Port;
        private System.Windows.Forms.TextBox txt_IpAddress;
        private System.Windows.Forms.Label lbl_IpAddress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lbl_FunctionCode;
        private System.Windows.Forms.TextBox txt_SlaveId;
        private System.Windows.Forms.Label lbl_SlaveId;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}