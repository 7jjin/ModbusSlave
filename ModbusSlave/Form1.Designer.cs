namespace ModbusSlave
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbx_readTest = new System.Windows.Forms.GroupBox();
            this.txt_ReadQuantity = new System.Windows.Forms.TextBox();
            this.lbl_ReadPlcAddress = new System.Windows.Forms.Label();
            this.txt_ReadAddress = new System.Windows.Forms.TextBox();
            this.lbl_ReadQuantity = new System.Windows.Forms.Label();
            this.btnReadData = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_ReadAddress = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslbl_status = new System.Windows.Forms.ToolStripStatusLabel();
            this.stlbl_statusCircle = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslbl_conectText = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbx_readTest.SuspendLayout();
            this.panel4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataView
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataView.Location = new System.Drawing.Point(0, 0);
            this.dataView.Margin = new System.Windows.Forms.Padding(0);
            this.dataView.MultiSelect = false;
            this.dataView.Name = "dataView";
            this.dataView.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("굴림", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataView.RowHeadersVisible = false;
            this.dataView.RowHeadersWidth = 51;
            this.dataView.RowTemplate.Height = 27;
            this.dataView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataView.ShowCellToolTips = false;
            this.dataView.Size = new System.Drawing.Size(488, 363);
            this.dataView.TabIndex = 25;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.38994F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.79245F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.974843F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(488, 663);
            this.tableLayoutPanel1.TabIndex = 47;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(482, 23);
            this.panel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.connectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(482, 23);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStripMenuItem1.Size = new System.Drawing.Size(36, 23);
            this.toolStripMenuItem1.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.AutoSize = false;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(300, 23);
            this.exitToolStripMenuItem.Text = "Exit        ";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // connectionToolStripMenuItem
            // 
            this.connectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.connectionToolStripMenuItem.Name = "connectionToolStripMenuItem";
            this.connectionToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            this.connectionToolStripMenuItem.Size = new System.Drawing.Size(92, 23);
            this.connectionToolStripMenuItem.Text = "Connection";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.AutoSize = false;
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(500, 23);
            this.connectToolStripMenuItem.Text = "Connect          ";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.AutoSize = false;
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(500, 23);
            this.disconnectToolStripMenuItem.Text = "Disconnect          ";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 29);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(488, 363);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbx_readTest);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 395);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(482, 226);
            this.panel3.TabIndex = 2;
            // 
            // gbx_readTest
            // 
            this.gbx_readTest.Controls.Add(this.txt_ReadQuantity);
            this.gbx_readTest.Controls.Add(this.lbl_ReadPlcAddress);
            this.gbx_readTest.Controls.Add(this.txt_ReadAddress);
            this.gbx_readTest.Controls.Add(this.lbl_ReadQuantity);
            this.gbx_readTest.Controls.Add(this.btnReadData);
            this.gbx_readTest.Controls.Add(this.label2);
            this.gbx_readTest.Controls.Add(this.lbl_ReadAddress);
            this.gbx_readTest.Location = new System.Drawing.Point(19, 16);
            this.gbx_readTest.Name = "gbx_readTest";
            this.gbx_readTest.Size = new System.Drawing.Size(444, 191);
            this.gbx_readTest.TabIndex = 0;
            this.gbx_readTest.TabStop = false;
            this.gbx_readTest.Text = "Read Test";
            // 
            // txt_ReadQuantity
            // 
            this.txt_ReadQuantity.Location = new System.Drawing.Point(96, 84);
            this.txt_ReadQuantity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ReadQuantity.Name = "txt_ReadQuantity";
            this.txt_ReadQuantity.Size = new System.Drawing.Size(166, 25);
            this.txt_ReadQuantity.TabIndex = 32;
            // 
            // lbl_ReadPlcAddress
            // 
            this.lbl_ReadPlcAddress.AutoSize = true;
            this.lbl_ReadPlcAddress.Location = new System.Drawing.Point(393, 45);
            this.lbl_ReadPlcAddress.Name = "lbl_ReadPlcAddress";
            this.lbl_ReadPlcAddress.Size = new System.Drawing.Size(47, 15);
            this.lbl_ReadPlcAddress.TabIndex = 41;
            this.lbl_ReadPlcAddress.Text = "40001";
            // 
            // txt_ReadAddress
            // 
            this.txt_ReadAddress.Location = new System.Drawing.Point(96, 42);
            this.txt_ReadAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ReadAddress.Name = "txt_ReadAddress";
            this.txt_ReadAddress.Size = new System.Drawing.Size(166, 25);
            this.txt_ReadAddress.TabIndex = 30;
            // 
            // lbl_ReadQuantity
            // 
            this.lbl_ReadQuantity.AutoSize = true;
            this.lbl_ReadQuantity.Location = new System.Drawing.Point(20, 87);
            this.lbl_ReadQuantity.Name = "lbl_ReadQuantity";
            this.lbl_ReadQuantity.Size = new System.Drawing.Size(62, 15);
            this.lbl_ReadQuantity.TabIndex = 31;
            this.lbl_ReadQuantity.Text = "Quantity";
            // 
            // btnReadData
            // 
            this.btnReadData.Location = new System.Drawing.Point(23, 131);
            this.btnReadData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadData.Name = "btnReadData";
            this.btnReadData.Size = new System.Drawing.Size(417, 34);
            this.btnReadData.TabIndex = 26;
            this.btnReadData.Text = "Read Register";
            this.btnReadData.UseVisualStyleBackColor = true;
            this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 15);
            this.label2.TabIndex = 40;
            this.label2.Text = "PLC address =";
            // 
            // lbl_ReadAddress
            // 
            this.lbl_ReadAddress.AutoSize = true;
            this.lbl_ReadAddress.Location = new System.Drawing.Point(20, 45);
            this.lbl_ReadAddress.Name = "lbl_ReadAddress";
            this.lbl_ReadAddress.Size = new System.Drawing.Size(60, 15);
            this.lbl_ReadAddress.TabIndex = 29;
            this.lbl_ReadAddress.Text = "Address";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.statusStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 627);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(482, 33);
            this.panel4.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbl_status,
            this.stlbl_statusCircle,
            this.tslbl_conectText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(482, 33);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslbl_status
            // 
            this.tslbl_status.AutoSize = false;
            this.tslbl_status.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tslbl_status.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tslbl_status.Name = "tslbl_status";
            this.tslbl_status.Size = new System.Drawing.Size(300, 27);
            this.tslbl_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stlbl_statusCircle
            // 
            this.stlbl_statusCircle.AutoSize = false;
            this.stlbl_statusCircle.BackColor = System.Drawing.Color.Transparent;
            this.stlbl_statusCircle.Font = new System.Drawing.Font("맑은 고딕", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.stlbl_statusCircle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stlbl_statusCircle.Margin = new System.Windows.Forms.Padding(5, 6, 0, 6);
            this.stlbl_statusCircle.Name = "stlbl_statusCircle";
            this.stlbl_statusCircle.Size = new System.Drawing.Size(16, 21);
            this.stlbl_statusCircle.Text = "      ";
            this.stlbl_statusCircle.Paint += new System.Windows.Forms.PaintEventHandler(this.StatusLabel_Paint);
            // 
            // tslbl_conectText
            // 
            this.tslbl_conectText.Margin = new System.Windows.Forms.Padding(6, 4, 0, 2);
            this.tslbl_conectText.Name = "tslbl_conectText";
            this.tslbl_conectText.Size = new System.Drawing.Size(88, 27);
            this.tslbl_conectText.Text = "Disconnected";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 663);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modbus Slave";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.gbx_readTest.ResumeLayout(false);
            this.gbx_readTest.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbx_readTest;
        private System.Windows.Forms.TextBox txt_ReadQuantity;
        private System.Windows.Forms.Label lbl_ReadPlcAddress;
        private System.Windows.Forms.TextBox txt_ReadAddress;
        private System.Windows.Forms.Label lbl_ReadQuantity;
        private System.Windows.Forms.Button btnReadData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_ReadAddress;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslbl_status;
        private System.Windows.Forms.ToolStripStatusLabel stlbl_statusCircle;
        private System.Windows.Forms.ToolStripStatusLabel tslbl_conectText;
    }
}

