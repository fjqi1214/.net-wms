namespace BenQGuru.eMES.WinCeClient
{
    partial class FPickDone
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
            this.cboPickNo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rdoAllCarton = new System.Windows.Forms.RadioButton();
            this.rdoSplitCarton = new System.Windows.Forms.RadioButton();
            this.txtCartonNo = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnInOut = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnClosePick = new System.Windows.Forms.Button();
            this.btnSAP = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboPickNo
            // 
            this.cboPickNo.Location = new System.Drawing.Point(83, 13);
            this.cboPickNo.Name = "cboPickNo";
            this.cboPickNo.Size = new System.Drawing.Size(136, 23);
            this.cboPickNo.TabIndex = 0;
            this.cboPickNo.TextChanged += new System.EventHandler(this.cboPickNo_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.Text = "拣货任务令";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "箱号";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "数量";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 20);
            this.label4.Text = "SN";
            // 
            // rdoAllCarton
            // 
            this.rdoAllCarton.Location = new System.Drawing.Point(21, 42);
            this.rdoAllCarton.Name = "rdoAllCarton";
            this.rdoAllCarton.Size = new System.Drawing.Size(100, 20);
            this.rdoAllCarton.TabIndex = 1;
            this.rdoAllCarton.Text = "整箱";
            this.rdoAllCarton.CheckedChanged += new System.EventHandler(this.rdoAllCarton_CheckedChanged);
            // 
            // rdoSplitCarton
            // 
            this.rdoSplitCarton.Location = new System.Drawing.Point(127, 42);
            this.rdoSplitCarton.Name = "rdoSplitCarton";
            this.rdoSplitCarton.Size = new System.Drawing.Size(81, 20);
            this.rdoSplitCarton.TabIndex = 2;
            this.rdoSplitCarton.Text = "拆箱";
            this.rdoSplitCarton.CheckedChanged += new System.EventHandler(this.rdoSplitCarton_CheckedChanged);
            // 
            // txtCartonNo
            // 
            this.txtCartonNo.Location = new System.Drawing.Point(49, 68);
            this.txtCartonNo.Name = "txtCartonNo";
            this.txtCartonNo.Size = new System.Drawing.Size(159, 23);
            this.txtCartonNo.TabIndex = 3;
            this.txtCartonNo.TextChanged += new System.EventHandler(this.txtCartonNo_TextChanged);
            this.txtCartonNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCartonNo_KeyPress);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(49, 95);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(99, 23);
            this.txtNumber.TabIndex = 4;
            this.txtNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumber_KeyPress);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(155, 97);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(53, 20);
            this.btnSubmit.TabIndex = 6;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(45, 125);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(159, 23);
            this.txtSN.TabIndex = 5;
            this.txtSN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSN_KeyPress);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(1, 155);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(215, 131);
            this.dataGrid1.TabIndex = 14;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged_1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn1);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn2);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn3);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn4);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn5);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn6);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.dataGridTextBoxColumn7);
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "选择";
            this.dataGridTextBoxColumn1.MappingName = "Check";
            this.dataGridTextBoxColumn1.Width = 20;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "鼎桥物料号";
            this.dataGridTextBoxColumn2.MappingName = "DQMCode";
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "华为物料号";
            this.dataGridTextBoxColumn3.MappingName = "CusMcode";
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "需求数量";
            this.dataGridTextBoxColumn4.MappingName = "RequireQty";
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "已拣数量";
            this.dataGridTextBoxColumn5.MappingName = "PickedQTY";
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "拣货任务令";
            this.dataGridTextBoxColumn6.MappingName = "PickNo";
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "行";
            this.dataGridTextBoxColumn7.MappingName = "PickLine";
            // 
            // btnInOut
            // 
            this.btnInOut.Location = new System.Drawing.Point(3, 333);
            this.btnInOut.Name = "btnInOut";
            this.btnInOut.Size = new System.Drawing.Size(69, 31);
            this.btnInOut.TabIndex = 7;
            this.btnInOut.Text = "先进先出";
            this.btnInOut.Click += new System.EventHandler(this.btnInOut_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(68, 333);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(68, 31);
            this.btnView.TabIndex = 8;
            this.btnView.Text = "已拣明细";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(140, 333);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(68, 31);
            this.btnApply.TabIndex = 9;
            this.btnApply.Text = "申请欠料";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(1, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 30);
            this.label5.Text = "提示信息：";
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(80, 289);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(136, 45);
            this.lblMessage.Text = "XXX";
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(154, 370);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(50, 31);
            this.btnReturn.TabIndex = 10;
            this.btnReturn.Text = "返回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnClosePick
            // 
            this.btnClosePick.Location = new System.Drawing.Point(3, 370);
            this.btnClosePick.Name = "btnClosePick";
            this.btnClosePick.Size = new System.Drawing.Size(68, 31);
            this.btnClosePick.TabIndex = 19;
            this.btnClosePick.Text = "拣料完成";
            this.btnClosePick.Click += new System.EventHandler(this.btnClosePick_Click);
            // 
            // btnSAP
            // 
            this.btnSAP.Location = new System.Drawing.Point(76, 370);
            this.btnSAP.Name = "btnSAP";
            this.btnSAP.Size = new System.Drawing.Size(72, 31);
            this.btnSAP.TabIndex = 26;
            this.btnSAP.Text = "SAP明细";
            this.btnSAP.Click += new System.EventHandler(this.btnSAP_Click);
            // 
            // FPickDone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.btnSAP);
            this.Controls.Add(this.btnClosePick);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnInOut);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.txtCartonNo);
            this.Controls.Add(this.rdoSplitCarton);
            this.Controls.Add(this.rdoAllCarton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboPickNo);
            this.Name = "FPickDone";
            this.Size = new System.Drawing.Size(219, 408);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPickNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoAllCarton;
        private System.Windows.Forms.RadioButton rdoSplitCarton;
        private System.Windows.Forms.TextBox txtCartonNo;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnInOut;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
        private System.Windows.Forms.Button btnClosePick;
        private System.Windows.Forms.Button btnSAP;
    }
}
