namespace BenQGuru.eMES.WinCeClient
{
    partial class FIQCCommand
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lbllocationCode = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lblCartonNo = new System.Windows.Forms.Label();
            this.btnApplyIQC = new System.Windows.Forms.Button();
            this.btnFirstCheck = new System.Windows.Forms.Button();
            this.btnCancelDown = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInvNo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(94, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(138, 23);
            this.comboBox1.TabIndex = 33;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // lbllocationCode
            // 
            this.lbllocationCode.Location = new System.Drawing.Point(0, 9);
            this.lbllocationCode.Name = "lbllocationCode";
            this.lbllocationCode.Size = new System.Drawing.Size(91, 20);
            this.lbllocationCode.Text = "入库指令号";
            // 
            // comboBox2
            // 
            this.comboBox2.Location = new System.Drawing.Point(94, 42);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(138, 23);
            this.comboBox2.TabIndex = 36;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // lblCartonNo
            // 
            this.lblCartonNo.Location = new System.Drawing.Point(36, 45);
            this.lblCartonNo.Name = "lblCartonNo";
            this.lblCartonNo.Size = new System.Drawing.Size(39, 20);
            this.lblCartonNo.Text = "状态";
            // 
            // btnApplyIQC
            // 
            this.btnApplyIQC.Location = new System.Drawing.Point(160, 230);
            this.btnApplyIQC.Name = "btnApplyIQC";
            this.btnApplyIQC.Size = new System.Drawing.Size(72, 20);
            this.btnApplyIQC.TabIndex = 41;
            this.btnApplyIQC.Text = "申请IQC";
            this.btnApplyIQC.Click += new System.EventHandler(this.btnApplyIQC_Click);
            // 
            // btnFirstCheck
            // 
            this.btnFirstCheck.Location = new System.Drawing.Point(82, 230);
            this.btnFirstCheck.Name = "btnFirstCheck";
            this.btnFirstCheck.Size = new System.Drawing.Size(72, 20);
            this.btnFirstCheck.TabIndex = 40;
            this.btnFirstCheck.Text = "初检";
            this.btnFirstCheck.Click += new System.EventHandler(this.btnFirstCheck_Click);
            // 
            // btnCancelDown
            // 
            this.btnCancelDown.Location = new System.Drawing.Point(4, 230);
            this.btnCancelDown.Name = "btnCancelDown";
            this.btnCancelDown.Size = new System.Drawing.Size(72, 20);
            this.btnCancelDown.TabIndex = 39;
            this.btnCancelDown.Text = "取消下发";
            this.btnCancelDown.Click += new System.EventHandler(this.btnCancelDown_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 108);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeadersVisible = false;
            this.dataGrid1.Size = new System.Drawing.Size(232, 107);
            this.dataGrid1.TabIndex = 38;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.Text = "SAP单据号";
            // 
            // txtInvNo
            // 
            this.txtInvNo.Location = new System.Drawing.Point(94, 79);
            this.txtInvNo.Name = "txtInvNo";
            this.txtInvNo.Size = new System.Drawing.Size(121, 23);
            this.txtInvNo.TabIndex = 76;
            this.txtInvNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInvNo_KeyPress);
            // 
            // FIQCCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.txtInvNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnApplyIQC);
            this.Controls.Add(this.btnFirstCheck);
            this.Controls.Add(this.btnCancelDown);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.lblCartonNo);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lbllocationCode);
            this.Name = "FIQCCommand";
            this.Size = new System.Drawing.Size(237, 277);
            this.Click += new System.EventHandler(this.FIQCCommand_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lbllocationCode;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lblCartonNo;
        private System.Windows.Forms.Button btnApplyIQC;
        private System.Windows.Forms.Button btnFirstCheck;
        private System.Windows.Forms.Button btnCancelDown;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInvNo;
    }
}
