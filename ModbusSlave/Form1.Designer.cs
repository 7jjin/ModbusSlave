﻿namespace ModbusSlave
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
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.gbx_readTest.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataView
            // 
            this.dataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataView.Location = new System.Drawing.Point(0, 0);
            this.dataView.Margin = new System.Windows.Forms.Padding(0);
            this.dataView.Name = "dataView";
            this.dataView.ReadOnly = true;
            this.dataView.RowHeadersVisible = false;
            this.dataView.RowHeadersWidth = 51;
            this.dataView.RowTemplate.Height = 27;
            this.dataView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataView.ShowCellToolTips = false;
            this.dataView.Size = new System.Drawing.Size(490, 364);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.38994F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.00629F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.761006F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(490, 665);
            this.tableLayoutPanel1.TabIndex = 47;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 23);
            this.panel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.connectionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(484, 23);
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
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.exitToolStripMenuItem.Text = "Exit";
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
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 29);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(490, 364);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbx_readTest);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 396);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(484, 222);
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
            this.gbx_readTest.Location = new System.Drawing.Point(19, 13);
            this.gbx_readTest.Name = "gbx_readTest";
            this.gbx_readTest.Size = new System.Drawing.Size(447, 196);
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
            this.panel4.Location = new System.Drawing.Point(3, 624);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(484, 38);
            this.panel4.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(484, 38);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 663);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.gbx_readTest.ResumeLayout(false);
            this.gbx_readTest.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
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
    }
}

