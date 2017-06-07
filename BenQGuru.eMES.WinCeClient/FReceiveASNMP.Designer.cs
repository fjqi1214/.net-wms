namespace BenQGuru.eMES.WinCeClient
{
    partial class FReceiveASNMP
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
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnUpPic = new System.Windows.Forms.Button();
            this.cmbGiveinResult = new System.Windows.Forms.ComboBox();
            this.lblGivenResult = new System.Windows.Forms.Label();
            this.cmbRejectResult = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGivenin = new System.Windows.Forms.Button();
            this.btnReceive = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.txtRejectCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCarton = new System.Windows.Forms.TextBox();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.lblCarton = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEmergency = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCartonNum = new System.Windows.Forms.Label();
            this.lblRejectNum = new System.Windows.Forms.Label();
            this.lblActNum = new System.Windows.Forms.Label();
            this.lblReceiveNum = new System.Windows.Forms.Label();
            this.lblGiveinNum = new System.Windows.Forms.Label();
            this.txtASNCode = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnCancelCartonno = new System.Windows.Forms.Button();
            this.chktrailcase = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 74);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(198, 104);
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
            this.btnReturn.Location = new System.Drawing.Point(110, 351);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(40, 20);
            this.btnReturn.TabIndex = 61;
            this.btnReturn.Text = "返回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(75, 328);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 20);
            this.label12.Text = "让步接收:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(1, 328);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 20);
            this.label11.Text = "已收:";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(149, 308);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 20);
            this.label10.Text = "应收:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(78, 308);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 20);
            this.label9.Text = "拒收:";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(2, 308);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 20);
            this.label8.Text = "箱数:";
            // 
            // btnUpPic
            // 
            this.btnUpPic.Location = new System.Drawing.Point(127, 256);
            this.btnUpPic.Name = "btnUpPic";
            this.btnUpPic.Size = new System.Drawing.Size(72, 20);
            this.btnUpPic.TabIndex = 60;
            this.btnUpPic.Text = "图片上传";
            this.btnUpPic.Click += new System.EventHandler(this.btnUpPic_Click);
            // 
            // cmbGiveinResult
            // 
            this.cmbGiveinResult.Location = new System.Drawing.Point(96, 282);
            this.cmbGiveinResult.Name = "cmbGiveinResult";
            this.cmbGiveinResult.Size = new System.Drawing.Size(92, 23);
            this.cmbGiveinResult.TabIndex = 59;
            this.cmbGiveinResult.Visible = false;
            // 
            // lblGivenResult
            // 
            this.lblGivenResult.Location = new System.Drawing.Point(-5, 284);
            this.lblGivenResult.Name = "lblGivenResult";
            this.lblGivenResult.Size = new System.Drawing.Size(118, 20);
            this.lblGivenResult.Text = "让步接收问题:";
            this.lblGivenResult.Visible = false;
            // 
            // cmbRejectResult
            // 
            this.cmbRejectResult.Location = new System.Drawing.Point(75, 227);
            this.cmbRejectResult.Name = "cmbRejectResult";
            this.cmbRejectResult.Size = new System.Drawing.Size(100, 23);
            this.cmbRejectResult.TabIndex = 58;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 20);
            this.label6.Text = "拒收原因:";
            // 
            // btnGivenin
            // 
            this.btnGivenin.Location = new System.Drawing.Point(49, 256);
            this.btnGivenin.Name = "btnGivenin";
            this.btnGivenin.Size = new System.Drawing.Size(72, 20);
            this.btnGivenin.TabIndex = 57;
            this.btnGivenin.Text = "让步接收";
            this.btnGivenin.Visible = false;
            this.btnGivenin.Click += new System.EventHandler(this.btnGivenin_Click);
            // 
            // btnReceive
            // 
            this.btnReceive.Location = new System.Drawing.Point(0, 256);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(43, 20);
            this.btnReceive.TabIndex = 56;
            this.btnReceive.Text = "接收";
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(119, 204);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(48, 20);
            this.btnReject.TabIndex = 55;
            this.btnReject.Text = "拒收";
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // txtRejectCount
            // 
            this.txtRejectCount.Location = new System.Drawing.Point(68, 201);
            this.txtRejectCount.Name = "txtRejectCount";
            this.txtRejectCount.Size = new System.Drawing.Size(45, 23);
            this.txtRejectCount.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(-1, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.Text = "拒收箱数:";
            // 
            // txtCarton
            // 
            this.txtCarton.Location = new System.Drawing.Point(75, 48);
            this.txtCarton.Name = "txtCarton";
            this.txtCarton.Size = new System.Drawing.Size(107, 23);
            this.txtCarton.TabIndex = 53;
            this.txtCarton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCarton_KeyPress);
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(82, 25);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(106, 23);
            this.txtSN.TabIndex = 52;
            this.txtSN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSN_KeyPress);
            // 
            // lblCarton
            // 
            this.lblCarton.Location = new System.Drawing.Point(0, 51);
            this.lblCarton.Name = "lblCarton";
            this.lblCarton.Size = new System.Drawing.Size(71, 20);
            this.lblCarton.Text = "箱号编码:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 20);
            this.label2.Text = "SN/物料号:";
            // 
            // chkEmergency
            // 
            this.chkEmergency.Checked = true;
            this.chkEmergency.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEmergency.ForeColor = System.Drawing.Color.Red;
            this.chkEmergency.Location = new System.Drawing.Point(7, 178);
            this.chkEmergency.Name = "chkEmergency";
            this.chkEmergency.Size = new System.Drawing.Size(64, 20);
            this.chkEmergency.TabIndex = 51;
            this.chkEmergency.Text = "急料";
            this.chkEmergency.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.Text = "入库指令号:";
            // 
            // lblCartonNum
            // 
            this.lblCartonNum.ForeColor = System.Drawing.Color.Red;
            this.lblCartonNum.Location = new System.Drawing.Point(46, 308);
            this.lblCartonNum.Name = "lblCartonNum";
            this.lblCartonNum.Size = new System.Drawing.Size(26, 20);
            // 
            // lblRejectNum
            // 
            this.lblRejectNum.ForeColor = System.Drawing.Color.Red;
            this.lblRejectNum.Location = new System.Drawing.Point(119, 308);
            this.lblRejectNum.Name = "lblRejectNum";
            this.lblRejectNum.Size = new System.Drawing.Size(26, 20);
            // 
            // lblActNum
            // 
            this.lblActNum.ForeColor = System.Drawing.Color.Red;
            this.lblActNum.Location = new System.Drawing.Point(181, 308);
            this.lblActNum.Name = "lblActNum";
            this.lblActNum.Size = new System.Drawing.Size(26, 20);
            // 
            // lblReceiveNum
            // 
            this.lblReceiveNum.ForeColor = System.Drawing.Color.Red;
            this.lblReceiveNum.Location = new System.Drawing.Point(43, 328);
            this.lblReceiveNum.Name = "lblReceiveNum";
            this.lblReceiveNum.Size = new System.Drawing.Size(26, 20);
            // 
            // lblGiveinNum
            // 
            this.lblGiveinNum.ForeColor = System.Drawing.Color.Red;
            this.lblGiveinNum.Location = new System.Drawing.Point(149, 328);
            this.lblGiveinNum.Name = "lblGiveinNum";
            this.lblGiveinNum.Size = new System.Drawing.Size(26, 20);
            // 
            // txtASNCode
            // 
            this.txtASNCode.Location = new System.Drawing.Point(82, 0);
            this.txtASNCode.Name = "txtASNCode";
            this.txtASNCode.Size = new System.Drawing.Size(106, 23);
            this.txtASNCode.TabIndex = 75;
            this.txtASNCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtASNCode_KeyPress);
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(-15, -15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(100, 20);
            this.checkBox1.TabIndex = 95;
            this.checkBox1.Text = "checkBox1";
            // 
            // btnCancelCartonno
            // 
            this.btnCancelCartonno.Location = new System.Drawing.Point(133, 178);
            this.btnCancelCartonno.Name = "btnCancelCartonno";
            this.btnCancelCartonno.Size = new System.Drawing.Size(68, 20);
            this.btnCancelCartonno.TabIndex = 133;
            this.btnCancelCartonno.Text = "取消箱号";
            this.btnCancelCartonno.Click += new System.EventHandler(this.btnCancelCartonno_Click);
            // 
            // chktrailcase
            // 
            this.chktrailcase.Location = new System.Drawing.Point(67, 178);
            this.chktrailcase.Name = "chktrailcase";
            this.chktrailcase.Size = new System.Drawing.Size(64, 20);
            this.chktrailcase.TabIndex = 151;
            this.chktrailcase.Text = "尾箱";
            this.chktrailcase.CheckStateChanged += new System.EventHandler(this.chktrailcase_CheckStateChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 354);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 20);
            this.label4.Text = "华为料号编码:";
            // 
            // FReceiveASNMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.txtCarton);
            this.Controls.Add(this.chktrailcase);
            this.Controls.Add(this.btnCancelCartonno);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.txtASNCode);
            this.Controls.Add(this.lblGiveinNum);
            this.Controls.Add(this.lblReceiveNum);
            this.Controls.Add(this.lblActNum);
            this.Controls.Add(this.lblRejectNum);
            this.Controls.Add(this.lblCartonNum);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnUpPic);
            this.Controls.Add(this.cmbGiveinResult);
            this.Controls.Add(this.lblGivenResult);
            this.Controls.Add(this.cmbRejectResult);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnGivenin);
            this.Controls.Add(this.btnReceive);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.txtRejectCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.lblCarton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkEmergency);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.Name = "FReceiveASNMP";
            this.Size = new System.Drawing.Size(189, 332);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnUpPic;
        private System.Windows.Forms.ComboBox cmbGiveinResult;
        private System.Windows.Forms.Label lblGivenResult;
        private System.Windows.Forms.ComboBox cmbRejectResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGivenin;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.TextBox txtRejectCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCarton;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label lblCarton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEmergency;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCartonNum;
        private System.Windows.Forms.Label lblRejectNum;
        private System.Windows.Forms.Label lblActNum;
        private System.Windows.Forms.Label lblReceiveNum;
        private System.Windows.Forms.Label lblGiveinNum;
        private System.Windows.Forms.DataGridTableStyle ExampleDataTable;
        private System.Windows.Forms.DataGridTextBoxColumn Check;
        private System.Windows.Forms.DataGridTextBoxColumn STLINE;
        private System.Windows.Forms.DataGridTextBoxColumn CartonNo;
        private System.Windows.Forms.DataGridTextBoxColumn CartonSeq;
        private System.Windows.Forms.DataGridTextBoxColumn DQMcode;
        private System.Windows.Forms.DataGridTextBoxColumn MControlType;
        private System.Windows.Forms.DataGridTextBoxColumn Qty;
        private System.Windows.Forms.TextBox txtASNCode;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnCancelCartonno;
        private System.Windows.Forms.CheckBox chktrailcase;
        private System.Windows.Forms.Label label4;
    }
}
