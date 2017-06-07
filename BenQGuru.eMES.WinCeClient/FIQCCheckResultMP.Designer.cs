namespace BenQGuru.eMES.WinCeClient
{
    partial class FIQCCheckResultMP
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
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.ExampleDataTable = new System.Windows.Forms.DataGridTableStyle();
            this.Check = new System.Windows.Forms.DataGridTextBoxColumn();
            this.STLINE = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CartonNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.CartonSeq = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DQMcode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MControlType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblTransNo = new System.Windows.Forms.Label();
            this.cmbIQCNo = new System.Windows.Forms.ComboBox();
            this.lblchujian = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.rdbSpotCheck = new System.Windows.Forms.RadioButton();
            this.rdbFullCheck = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAQLStandard = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAQLStandardDescInfo = new System.Windows.Forms.Label();
            this.lblSamplesNum = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRejectionNum = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRecordNG = new System.Windows.Forms.Button();
            this.txtCartonNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCartonnoCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(6, 186);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(226, 109);
            this.dataGrid1.TabIndex = 62;
            this.dataGrid1.TableStyles.Add(this.ExampleDataTable);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // ExampleDataTable
            // 
            this.ExampleDataTable.GridColumnStyles.Add(this.Check);
            this.ExampleDataTable.GridColumnStyles.Add(this.STLINE);
            this.ExampleDataTable.GridColumnStyles.Add(this.CartonNo);
            this.ExampleDataTable.GridColumnStyles.Add(this.CartonSeq);
            this.ExampleDataTable.GridColumnStyles.Add(this.DQMcode);
            this.ExampleDataTable.GridColumnStyles.Add(this.MControlType);
            this.ExampleDataTable.GridColumnStyles.Add(this.Qty);
            // 
            // Check
            // 
            this.Check.Format = "";
            this.Check.FormatInfo = null;
            this.Check.HeaderText = "选择";
            this.Check.MappingName = "选择";
            // 
            // STLINE
            // 
            this.STLINE.Format = "";
            this.STLINE.FormatInfo = null;
            this.STLINE.HeaderText = "STLINE";
            this.STLINE.MappingName = "STLINE";
            // 
            // CartonNo
            // 
            this.CartonNo.Format = "";
            this.CartonNo.FormatInfo = null;
            this.CartonNo.HeaderText = "箱号编码";
            this.CartonNo.MappingName = "箱号编码";
            // 
            // CartonSeq
            // 
            this.CartonSeq.Format = "";
            this.CartonSeq.FormatInfo = null;
            this.CartonSeq.HeaderText = "小箱号";
            this.CartonSeq.MappingName = "小箱号";
            // 
            // DQMcode
            // 
            this.DQMcode.Format = "";
            this.DQMcode.FormatInfo = null;
            this.DQMcode.HeaderText = "鼎桥物料";
            this.DQMcode.MappingName = "鼎桥物料";
            // 
            // MControlType
            // 
            this.MControlType.Format = "";
            this.MControlType.FormatInfo = null;
            this.MControlType.HeaderText = "管控类型";
            this.MControlType.MappingName = "管控类型";
            // 
            // Qty
            // 
            this.Qty.Format = "";
            this.Qty.FormatInfo = null;
            this.Qty.HeaderText = "来料数量";
            this.Qty.MappingName = "来料数量";
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(164, 344);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(49, 20);
            this.btnReturn.TabIndex = 61;
            this.btnReturn.Text = "返回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(38, 298);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(44, 20);
            this.btnSubmit.TabIndex = 55;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblTransNo
            // 
            this.lblTransNo.Location = new System.Drawing.Point(-6, 49);
            this.lblTransNo.Name = "lblTransNo";
            this.lblTransNo.Size = new System.Drawing.Size(77, 20);
            this.lblTransNo.Text = "IQC检验单";
            this.lblTransNo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbIQCNo
            // 
            this.cmbIQCNo.Location = new System.Drawing.Point(76, 46);
            this.cmbIQCNo.Name = "cmbIQCNo";
            this.cmbIQCNo.Size = new System.Drawing.Size(156, 23);
            this.cmbIQCNo.TabIndex = 50;
            this.cmbIQCNo.SelectedIndexChanged += new System.EventHandler(this.cmbIQCNo_SelectedIndexChanged);
            // 
            // lblchujian
            // 
            this.lblchujian.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblchujian.Location = new System.Drawing.Point(5, 0);
            this.lblchujian.Name = "lblchujian";
            this.lblchujian.Size = new System.Drawing.Size(100, 20);
            this.lblchujian.Text = "IQC检验结果维护";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(2, 321);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.Text = "提示信息";
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(76, 321);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(137, 20);
            this.lblMessage.Text = "X";
            // 
            // rdbSpotCheck
            // 
            this.rdbSpotCheck.Checked = true;
            this.rdbSpotCheck.Location = new System.Drawing.Point(28, 97);
            this.rdbSpotCheck.Name = "rdbSpotCheck";
            this.rdbSpotCheck.Size = new System.Drawing.Size(54, 20);
            this.rdbSpotCheck.TabIndex = 146;
            this.rdbSpotCheck.Text = "抽检";
            // 
            // rdbFullCheck
            // 
            this.rdbFullCheck.Location = new System.Drawing.Point(100, 97);
            this.rdbFullCheck.Name = "rdbFullCheck";
            this.rdbFullCheck.Size = new System.Drawing.Size(84, 20);
            this.rdbFullCheck.TabIndex = 147;
            this.rdbFullCheck.TabStop = false;
            this.rdbFullCheck.Text = "加严全检";
            this.rdbFullCheck.CheckedChanged += new System.EventHandler(this.rdbFullCheck_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(-6, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.Text = "AQL标准";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbAQLStandard
            // 
            this.cmbAQLStandard.Location = new System.Drawing.Point(77, 117);
            this.cmbAQLStandard.Name = "cmbAQLStandard";
            this.cmbAQLStandard.Size = new System.Drawing.Size(155, 23);
            this.cmbAQLStandard.TabIndex = 158;
            this.cmbAQLStandard.SelectedIndexChanged += new System.EventHandler(this.cmbAQLStandard_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 20);
            this.label4.Text = "AQL标准描述";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAQLStandardDescInfo
            // 
            this.lblAQLStandardDescInfo.Location = new System.Drawing.Point(100, 143);
            this.lblAQLStandardDescInfo.Name = "lblAQLStandardDescInfo";
            this.lblAQLStandardDescInfo.Size = new System.Drawing.Size(96, 20);
            this.lblAQLStandardDescInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSamplesNum
            // 
            this.lblSamplesNum.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblSamplesNum.ForeColor = System.Drawing.Color.Red;
            this.lblSamplesNum.Location = new System.Drawing.Point(52, 163);
            this.lblSamplesNum.Name = "lblSamplesNum";
            this.lblSamplesNum.Size = new System.Drawing.Size(23, 20);
            this.lblSamplesNum.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(-2, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 20);
            this.label6.Text = "样本数";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblRejectionNum
            // 
            this.lblRejectionNum.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblRejectionNum.ForeColor = System.Drawing.Color.Red;
            this.lblRejectionNum.Location = new System.Drawing.Point(129, 162);
            this.lblRejectionNum.Name = "lblRejectionNum";
            this.lblRejectionNum.Size = new System.Drawing.Size(21, 20);
            this.lblRejectionNum.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label8.Location = new System.Drawing.Point(81, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 20);
            this.label8.Text = "判退数";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnRecordNG
            // 
            this.btnRecordNG.Location = new System.Drawing.Point(115, 298);
            this.btnRecordNG.Name = "btnRecordNG";
            this.btnRecordNG.Size = new System.Drawing.Size(81, 20);
            this.btnRecordNG.TabIndex = 172;
            this.btnRecordNG.Text = "记录缺陷";
            this.btnRecordNG.Click += new System.EventHandler(this.btnRecordNG_Click);
            // 
            // txtCartonNo
            // 
            this.txtCartonNo.Location = new System.Drawing.Point(76, 71);
            this.txtCartonNo.Name = "txtCartonNo";
            this.txtCartonNo.Size = new System.Drawing.Size(156, 23);
            this.txtCartonNo.TabIndex = 144;
            this.txtCartonNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCartonNo_KeyPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(-6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.Text = "箱号检索";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCartonnoCode
            // 
            this.txtCartonnoCode.Location = new System.Drawing.Point(76, 23);
            this.txtCartonnoCode.Name = "txtCartonnoCode";
            this.txtCartonnoCode.Size = new System.Drawing.Size(156, 23);
            this.txtCartonnoCode.TabIndex = 189;
            this.txtCartonnoCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCartonnoCode_KeyPress);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(-7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.Text = "箱号编码";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(152, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 20);
            this.label7.Text = "送检数";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTotal.Location = new System.Drawing.Point(201, 162);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(20, 20);
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FIQCCheckResultMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCartonnoCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnRecordNG);
            this.Controls.Add(this.lblRejectionNum);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblSamplesNum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAQLStandardDescInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbAQLStandard);
            this.Controls.Add(this.rdbFullCheck);
            this.Controls.Add(this.rdbSpotCheck);
            this.Controls.Add(this.txtCartonNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblchujian);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.lblTransNo);
            this.Controls.Add(this.cmbIQCNo);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.Name = "FIQCCheckResultMP";
            this.Size = new System.Drawing.Size(235, 367);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblTransNo;
        private System.Windows.Forms.ComboBox cmbIQCNo;
        private System.Windows.Forms.Label lblchujian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DataGridTableStyle ExampleDataTable;
        private System.Windows.Forms.DataGridTextBoxColumn Check;
        private System.Windows.Forms.DataGridTextBoxColumn STLINE;
        private System.Windows.Forms.DataGridTextBoxColumn CartonNo;
        private System.Windows.Forms.DataGridTextBoxColumn CartonSeq;
        private System.Windows.Forms.DataGridTextBoxColumn DQMcode;
        private System.Windows.Forms.DataGridTextBoxColumn MControlType;
        private System.Windows.Forms.DataGridTextBoxColumn Qty;
        private System.Windows.Forms.RadioButton rdbSpotCheck;
        private System.Windows.Forms.RadioButton rdbFullCheck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbAQLStandard;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAQLStandardDescInfo;
        private System.Windows.Forms.Label lblSamplesNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRejectionNum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnRecordNG;
        private System.Windows.Forms.TextBox txtCartonNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCartonnoCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotal;
    }
}
