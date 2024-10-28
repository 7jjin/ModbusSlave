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
            this.lbl_WritePlcAddress = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_ReadPlcAddress = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_WriteAddress = new System.Windows.Forms.TextBox();
            this.lbl_WriteAddress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_WriteTest = new System.Windows.Forms.Label();
            this.txt_ReadQuantity = new System.Windows.Forms.TextBox();
            this.lbl_ReadQuantity = new System.Windows.Forms.Label();
            this.txt_ReadAddress = new System.Windows.Forms.TextBox();
            this.lbl_ReadAddress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_ReadTest = new System.Windows.Forms.Label();
            this.btnReadData = new System.Windows.Forms.Button();
            this.dataView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_WritePlcAddress
            // 
            this.lbl_WritePlcAddress.AutoSize = true;
            this.lbl_WritePlcAddress.Location = new System.Drawing.Point(830, 287);
            this.lbl_WritePlcAddress.Name = "lbl_WritePlcAddress";
            this.lbl_WritePlcAddress.Size = new System.Drawing.Size(47, 15);
            this.lbl_WritePlcAddress.TabIndex = 43;
            this.lbl_WritePlcAddress.Text = "40001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(719, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 15);
            this.label5.TabIndex = 42;
            this.label5.Text = "PLC address =";
            // 
            // lbl_ReadPlcAddress
            // 
            this.lbl_ReadPlcAddress.AutoSize = true;
            this.lbl_ReadPlcAddress.Location = new System.Drawing.Point(830, 104);
            this.lbl_ReadPlcAddress.Name = "lbl_ReadPlcAddress";
            this.lbl_ReadPlcAddress.Size = new System.Drawing.Size(47, 15);
            this.lbl_ReadPlcAddress.TabIndex = 41;
            this.lbl_ReadPlcAddress.Text = "40001";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(719, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 15);
            this.label2.TabIndex = 40;
            this.label2.Text = "PLC address =";
            // 
            // txt_WriteAddress
            // 
            this.txt_WriteAddress.Location = new System.Drawing.Point(521, 284);
            this.txt_WriteAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_WriteAddress.Name = "txt_WriteAddress";
            this.txt_WriteAddress.Size = new System.Drawing.Size(178, 25);
            this.txt_WriteAddress.TabIndex = 37;
            // 
            // lbl_WriteAddress
            // 
            this.lbl_WriteAddress.AutoSize = true;
            this.lbl_WriteAddress.Location = new System.Drawing.Point(445, 287);
            this.lbl_WriteAddress.Name = "lbl_WriteAddress";
            this.lbl_WriteAddress.Size = new System.Drawing.Size(60, 15);
            this.lbl_WriteAddress.TabIndex = 36;
            this.lbl_WriteAddress.Text = "Address";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(434, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(523, 3);
            this.label4.TabIndex = 35;
            // 
            // lbl_WriteTest
            // 
            this.lbl_WriteTest.AutoSize = true;
            this.lbl_WriteTest.Location = new System.Drawing.Point(431, 223);
            this.lbl_WriteTest.Name = "lbl_WriteTest";
            this.lbl_WriteTest.Size = new System.Drawing.Size(73, 15);
            this.lbl_WriteTest.TabIndex = 34;
            this.lbl_WriteTest.Text = "Write Test";
            // 
            // txt_ReadQuantity
            // 
            this.txt_ReadQuantity.Location = new System.Drawing.Point(521, 143);
            this.txt_ReadQuantity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ReadQuantity.Name = "txt_ReadQuantity";
            this.txt_ReadQuantity.Size = new System.Drawing.Size(436, 25);
            this.txt_ReadQuantity.TabIndex = 32;
            // 
            // lbl_ReadQuantity
            // 
            this.lbl_ReadQuantity.AutoSize = true;
            this.lbl_ReadQuantity.Location = new System.Drawing.Point(445, 146);
            this.lbl_ReadQuantity.Name = "lbl_ReadQuantity";
            this.lbl_ReadQuantity.Size = new System.Drawing.Size(62, 15);
            this.lbl_ReadQuantity.TabIndex = 31;
            this.lbl_ReadQuantity.Text = "Quantity";
            // 
            // txt_ReadAddress
            // 
            this.txt_ReadAddress.Location = new System.Drawing.Point(521, 101);
            this.txt_ReadAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ReadAddress.Name = "txt_ReadAddress";
            this.txt_ReadAddress.Size = new System.Drawing.Size(178, 25);
            this.txt_ReadAddress.TabIndex = 30;
            // 
            // lbl_ReadAddress
            // 
            this.lbl_ReadAddress.AutoSize = true;
            this.lbl_ReadAddress.Location = new System.Drawing.Point(445, 104);
            this.lbl_ReadAddress.Name = "lbl_ReadAddress";
            this.lbl_ReadAddress.Size = new System.Drawing.Size(60, 15);
            this.lbl_ReadAddress.TabIndex = 29;
            this.lbl_ReadAddress.Text = "Address";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(434, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(523, 3);
            this.label1.TabIndex = 28;
            // 
            // lbl_ReadTest
            // 
            this.lbl_ReadTest.AutoSize = true;
            this.lbl_ReadTest.Location = new System.Drawing.Point(431, 40);
            this.lbl_ReadTest.Name = "lbl_ReadTest";
            this.lbl_ReadTest.Size = new System.Drawing.Size(74, 15);
            this.lbl_ReadTest.TabIndex = 27;
            this.lbl_ReadTest.Text = "Read Test";
            // 
            // btnReadData
            // 
            this.btnReadData.Location = new System.Drawing.Point(875, 188);
            this.btnReadData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReadData.Name = "btnReadData";
            this.btnReadData.Size = new System.Drawing.Size(82, 28);
            this.btnReadData.TabIndex = 26;
            this.btnReadData.Text = "Read";
            this.btnReadData.UseVisualStyleBackColor = true;
            this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
            // 
            // dataView
            // 
            this.dataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataView.Location = new System.Drawing.Point(9, 41);
            this.dataView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataView.Name = "dataView";
            this.dataView.ReadOnly = true;
            this.dataView.RowHeadersVisible = false;
            this.dataView.RowHeadersWidth = 51;
            this.dataView.RowTemplate.Height = 27;
            this.dataView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataView.ShowCellToolTips = false;
            this.dataView.Size = new System.Drawing.Size(343, 365);
            this.dataView.TabIndex = 25;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 441);
            this.Controls.Add(this.lbl_WritePlcAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbl_ReadPlcAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_WriteAddress);
            this.Controls.Add(this.lbl_WriteAddress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_WriteTest);
            this.Controls.Add(this.txt_ReadQuantity);
            this.Controls.Add(this.lbl_ReadQuantity);
            this.Controls.Add(this.txt_ReadAddress);
            this.Controls.Add(this.lbl_ReadAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_ReadTest);
            this.Controls.Add(this.btnReadData);
            this.Controls.Add(this.dataView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_WritePlcAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_ReadPlcAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_WriteAddress;
        private System.Windows.Forms.Label lbl_WriteAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_WriteTest;
        private System.Windows.Forms.TextBox txt_ReadQuantity;
        private System.Windows.Forms.Label lbl_ReadQuantity;
        private System.Windows.Forms.TextBox txt_ReadAddress;
        private System.Windows.Forms.Label lbl_ReadAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_ReadTest;
        private System.Windows.Forms.Button btnReadData;
        private System.Windows.Forms.DataGridView dataView;
    }
}

