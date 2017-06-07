namespace BenQGuru.eMES.WinCeClient
{
    partial class FSMTCheck
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMachineCode = new System.Windows.Forms.TextBox();
            this.txtMocode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.txtReelNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStationCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboStationTable = new System.Windows.Forms.ComboBox();
            this.txtmessage = new BenQGuru.eMES.WinCeClient.UcPlaySoundTextBox();
            this.SuspendLayout();
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.Location = new System.Drawing.Point(111, 26);
            this.txtMachineCode.MaxLength = 40;
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Size = new System.Drawing.Size(170, 23);
            this.txtMachineCode.TabIndex = 19;
            this.txtMachineCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMachineCode_KeyPress);
            // 
            // txtMocode
            // 
            this.txtMocode.Location = new System.Drawing.Point(111, 2);
            this.txtMocode.MaxLength = 40;
            this.txtMocode.Name = "txtMocode";
            this.txtMocode.Size = new System.Drawing.Size(170, 23);
            this.txtMocode.TabIndex = 16;
            this.txtMocode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMocode_KeyPress);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(21, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 20);
            this.label4.Text = "Table";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(21, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.Text = "机台编码";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.Text = "工单代码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btn_exit
            // 
            this.btn_exit.Location = new System.Drawing.Point(189, 188);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(72, 20);
            this.btn_exit.TabIndex = 15;
            this.btn_exit.Text = "退出";
            this.btn_exit.Visible = false;
            // 
            // btn_ok
            // 
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ok.Location = new System.Drawing.Point(111, 183);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(72, 20);
            this.btn_ok.TabIndex = 14;
            this.btn_ok.Text = "清空";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // txtReelNo
            // 
            this.txtReelNo.Location = new System.Drawing.Point(111, 98);
            this.txtReelNo.MaxLength = 40;
            this.txtReelNo.Name = "txtReelNo";
            this.txtReelNo.Size = new System.Drawing.Size(170, 23);
            this.txtReelNo.TabIndex = 24;
            this.txtReelNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReelNo_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(21, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.Text = "料卷号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtStationCode
            // 
            this.txtStationCode.Location = new System.Drawing.Point(111, 74);
            this.txtStationCode.MaxLength = 40;
            this.txtStationCode.Name = "txtStationCode";
            this.txtStationCode.Size = new System.Drawing.Size(170, 23);
            this.txtStationCode.TabIndex = 33;
            this.txtStationCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStationCode_KeyPress);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(21, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 20);
            this.label6.Text = "站位";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cboStationTable
            // 
            this.cboStationTable.Location = new System.Drawing.Point(111, 50);
            this.cboStationTable.Name = "cboStationTable";
            this.cboStationTable.Size = new System.Drawing.Size(170, 23);
            this.cboStationTable.TabIndex = 40;
            this.cboStationTable.SelectedIndexChanged += new System.EventHandler(this.cboStationTable_SelectedIndexChanged);
            this.cboStationTable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboStationTable_KeyPress);
            // 
            // txtmessage
            // 
            this.txtmessage.Location = new System.Drawing.Point(22, 122);
            this.txtmessage.Multiline = true;
            this.txtmessage.Name = "txtmessage";
            this.txtmessage.Size = new System.Drawing.Size(259, 60);
            this.txtmessage.TabIndex = 18;
            // 
            // FSMTCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.cboStationTable);
            this.Controls.Add(this.txtStationCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtReelNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMachineCode);
            this.Controls.Add(this.txtmessage);
            this.Controls.Add(this.txtMocode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_ok);
            this.Name = "FSMTCheck";
            this.Size = new System.Drawing.Size(309, 244);
            this.Resize += new System.EventHandler(this.FSMTCheck_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtMachineCode;
        private UcPlaySoundTextBox txtmessage;
        private System.Windows.Forms.TextBox txtMocode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.TextBox txtReelNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStationCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboStationTable;

    }
}
