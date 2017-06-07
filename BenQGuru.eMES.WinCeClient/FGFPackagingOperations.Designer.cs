namespace BenQGuru.eMES.WinCeClient
{
    partial class FGFPackagingOperations
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
            this.label8 = new System.Windows.Forms.Label();
            this.btnPackingDetail = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtCartonNO = new System.Windows.Forms.TextBox();
            this.lblCartonNO = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPickNO = new System.Windows.Forms.ComboBox();
            this.lblchujian = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblPQTY = new System.Windows.Forms.Label();
            this.lblCarInvNO = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSuiteQTY = new System.Windows.Forms.TextBox();
            this.cmbGFHWItemCode = new System.Windows.Forms.ComboBox();
            this.cmbGFPackingSEQ = new System.Windows.Forms.ComboBox();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbDQMCode = new System.Windows.Forms.ComboBox();
            this.txtQTY = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnPackFinish = new System.Windows.Forms.Button();
            this.btnApplyOQC = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.drpDQSMCodeEdit = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 268);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(231, 72);
            this.dataGrid1.TabIndex = 62;
            this.dataGrid1.TableStyles.Add(this.ExampleDataTable);
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
            this.btnReturn.Location = new System.Drawing.Point(194, 376);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(48, 25);
            this.btnReturn.TabIndex = 61;
            this.btnReturn.Text = "返回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(100, 245);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 20);
            this.label12.Text = "箱单号";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(-4, 245);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 20);
            this.label8.Text = "已包装箱";
            // 
            // btnPackingDetail
            // 
            this.btnPackingDetail.Location = new System.Drawing.Point(40, 376);
            this.btnPackingDetail.Name = "btnPackingDetail";
            this.btnPackingDetail.Size = new System.Drawing.Size(65, 25);
            this.btnPackingDetail.TabIndex = 60;
            this.btnPackingDetail.Text = "包装详细";
            this.btnPackingDetail.Click += new System.EventHandler(this.btnPackingDetail_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(-4, 376);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(44, 25);
            this.btnSubmit.TabIndex = 55;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Visible = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtCartonNO
            // 
            this.txtCartonNO.Location = new System.Drawing.Point(82, 167);
            this.txtCartonNO.Name = "txtCartonNO";
            this.txtCartonNO.Size = new System.Drawing.Size(145, 23);
            this.txtCartonNO.TabIndex = 53;
            this.txtCartonNO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCartonNO_KeyPress);
            // 
            // lblCartonNO
            // 
            this.lblCartonNO.Location = new System.Drawing.Point(-9, 40);
            this.lblCartonNO.Name = "lblCartonNO";
            this.lblCartonNO.Size = new System.Drawing.Size(100, 20);
            this.lblCartonNO.Text = "光伏华为编码";
            this.lblCartonNO.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(-7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "拣货任务令号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbPickNO
            // 
            this.cmbPickNO.Location = new System.Drawing.Point(97, 14);
            this.cmbPickNO.Name = "cmbPickNO";
            this.cmbPickNO.Size = new System.Drawing.Size(145, 23);
            this.cmbPickNO.TabIndex = 50;
            this.cmbPickNO.SelectedIndexChanged += new System.EventHandler(this.cmbPickNO_SelectedIndexChanged);
            // 
            // lblchujian
            // 
            this.lblchujian.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblchujian.Location = new System.Drawing.Point(0, 0);
            this.lblchujian.Name = "lblchujian";
            this.lblchujian.Size = new System.Drawing.Size(85, 20);
            this.lblchujian.Text = "包装作业";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(3, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.Text = "提示信息";
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(72, 343);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(156, 30);
            this.lblMessage.Text = "X";
            // 
            // lblPQTY
            // 
            this.lblPQTY.ForeColor = System.Drawing.Color.Red;
            this.lblPQTY.Location = new System.Drawing.Point(66, 245);
            this.lblPQTY.Name = "lblPQTY";
            this.lblPQTY.Size = new System.Drawing.Size(36, 20);
            this.lblPQTY.Text = "111";
            // 
            // lblCarInvNO
            // 
            this.lblCarInvNO.ForeColor = System.Drawing.Color.Red;
            this.lblCarInvNO.Location = new System.Drawing.Point(150, 245);
            this.lblCarInvNO.Name = "lblCarInvNO";
            this.lblCarInvNO.Size = new System.Drawing.Size(84, 20);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(-8, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "光伏包装序号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(-7, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "华为编码数量 ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 22);
            this.label5.Text = "包装箱号";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(-15, 219);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "SN号条码";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSuiteQTY
            // 
            this.txtSuiteQTY.Location = new System.Drawing.Point(92, 89);
            this.txtSuiteQTY.Name = "txtSuiteQTY";
            this.txtSuiteQTY.Size = new System.Drawing.Size(97, 23);
            this.txtSuiteQTY.TabIndex = 93;
            // 
            // cmbGFHWItemCode
            // 
            this.cmbGFHWItemCode.Location = new System.Drawing.Point(92, 37);
            this.cmbGFHWItemCode.Name = "cmbGFHWItemCode";
            this.cmbGFHWItemCode.Size = new System.Drawing.Size(156, 23);
            this.cmbGFHWItemCode.TabIndex = 94;
            this.cmbGFHWItemCode.SelectedIndexChanged += new System.EventHandler(this.cmbGFHWItemCode_SelectedIndexChanged);
            // 
            // cmbGFPackingSEQ
            // 
            this.cmbGFPackingSEQ.Location = new System.Drawing.Point(91, 63);
            this.cmbGFPackingSEQ.Name = "cmbGFPackingSEQ";
            this.cmbGFPackingSEQ.Size = new System.Drawing.Size(154, 23);
            this.cmbGFPackingSEQ.TabIndex = 95;
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(91, 219);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(124, 23);
            this.txtSN.TabIndex = 109;
            this.txtSN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSN_KeyPress);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(-8, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 20);
            this.label7.Text = "鼎桥物料编码";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbDQMCode
            // 
            this.cmbDQMCode.Location = new System.Drawing.Point(91, 112);
            this.cmbDQMCode.Name = "cmbDQMCode";
            this.cmbDQMCode.Size = new System.Drawing.Size(154, 23);
            this.cmbDQMCode.TabIndex = 124;
            this.cmbDQMCode.SelectedIndexChanged += new System.EventHandler(this.cmbDQMCode_SelectedIndexChanged);
            // 
            // txtQTY
            // 
            this.txtQTY.Location = new System.Drawing.Point(92, 192);
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.Size = new System.Drawing.Size(137, 23);
            this.txtQTY.TabIndex = 127;
            this.txtQTY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQTY_KeyPress);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 195);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 20);
            this.label9.Text = "数量";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnPackFinish
            // 
            this.btnPackFinish.Location = new System.Drawing.Point(99, 376);
            this.btnPackFinish.Name = "btnPackFinish";
            this.btnPackFinish.Size = new System.Drawing.Size(58, 25);
            this.btnPackFinish.TabIndex = 143;
            this.btnPackFinish.Text = "包装完成";
            this.btnPackFinish.Click += new System.EventHandler(this.btnPackFinish_Click);
            // 
            // btnApplyOQC
            // 
            this.btnApplyOQC.Location = new System.Drawing.Point(150, 376);
            this.btnApplyOQC.Name = "btnApplyOQC";
            this.btnApplyOQC.Size = new System.Drawing.Size(48, 25);
            this.btnApplyOQC.TabIndex = 159;
            this.btnApplyOQC.Text = "申请OQC";
            this.btnApplyOQC.Click += new System.EventHandler(this.btnApplyOQC_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(-35, 144);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 20);
            this.label10.Text = "鼎桥S编码";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // drpDQSMCodeEdit
            // 
            this.drpDQSMCodeEdit.Location = new System.Drawing.Point(66, 138);
            this.drpDQSMCodeEdit.Name = "drpDQSMCodeEdit";
            this.drpDQSMCodeEdit.Size = new System.Drawing.Size(179, 23);
            this.drpDQSMCodeEdit.TabIndex = 176;
            // 
            // FGFPackagingOperations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.drpDQSMCodeEdit);
            this.Controls.Add(this.btnApplyOQC);
            this.Controls.Add(this.btnPackFinish);
            this.Controls.Add(this.txtQTY);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbDQMCode);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.cmbGFPackingSEQ);
            this.Controls.Add(this.cmbGFHWItemCode);
            this.Controls.Add(this.txtSuiteQTY);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCarInvNO);
            this.Controls.Add(this.lblPQTY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblchujian);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnPackingDetail);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtCartonNO);
            this.Controls.Add(this.lblCartonNO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbPickNO);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.Name = "FGFPackagingOperations";
            this.Size = new System.Drawing.Size(248, 417);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnPackingDetail;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtCartonNO;
        private System.Windows.Forms.Label lblCartonNO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPickNO;
        private System.Windows.Forms.Label lblchujian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblPQTY;
        private System.Windows.Forms.Label lblCarInvNO;
        private System.Windows.Forms.DataGridTableStyle ExampleDataTable;
        private System.Windows.Forms.DataGridTextBoxColumn Check;
        private System.Windows.Forms.DataGridTextBoxColumn STLINE;
        private System.Windows.Forms.DataGridTextBoxColumn CartonNo;
        private System.Windows.Forms.DataGridTextBoxColumn CartonSeq;
        private System.Windows.Forms.DataGridTextBoxColumn DQMcode;
        private System.Windows.Forms.DataGridTextBoxColumn MControlType;
        private System.Windows.Forms.DataGridTextBoxColumn Qty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSuiteQTY;
        private System.Windows.Forms.ComboBox cmbGFHWItemCode;
        private System.Windows.Forms.ComboBox cmbGFPackingSEQ;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbDQMCode;
        private System.Windows.Forms.TextBox txtQTY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnPackFinish;
        private System.Windows.Forms.Button btnApplyOQC;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox drpDQSMCodeEdit;
    }
}
