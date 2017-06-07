namespace BenQGuru.eMES.WinCeClient
{
    partial class FStorLocTransOperations
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
            this.txtFromCartonNo = new System.Windows.Forms.TextBox();
            this.lblTransNo = new System.Windows.Forms.Label();
            this.cmbTransNo = new System.Windows.Forms.ComboBox();
            this.lblchujian = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLocationCode = new System.Windows.Forms.TextBox();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.txtQTY = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTLocationCartonNo = new System.Windows.Forms.TextBox();
            this.rdbSplitCarton = new System.Windows.Forms.RadioButton();
            this.rdbAllCarton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(5, 204);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(231, 90);
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
            this.btnReturn.Location = new System.Drawing.Point(132, 320);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(40, 20);
            this.btnReturn.TabIndex = 61;
            this.btnReturn.Text = "返回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(77, 320);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(44, 20);
            this.btnSubmit.TabIndex = 55;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtFromCartonNo
            // 
            this.txtFromCartonNo.Location = new System.Drawing.Point(81, 102);
            this.txtFromCartonNo.Name = "txtFromCartonNo";
            this.txtFromCartonNo.Size = new System.Drawing.Size(155, 23);
            this.txtFromCartonNo.TabIndex = 53;
            this.txtFromCartonNo.TextChanged += new System.EventHandler(this.txtFromCartonNo_TextChanged);
            // 
            // lblTransNo
            // 
            this.lblTransNo.Location = new System.Drawing.Point(3, 29);
            this.lblTransNo.Name = "lblTransNo";
            this.lblTransNo.Size = new System.Drawing.Size(72, 20);
            this.lblTransNo.Text = "转储单";
            this.lblTransNo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbTransNo
            // 
            this.cmbTransNo.Location = new System.Drawing.Point(81, 26);
            this.cmbTransNo.Name = "cmbTransNo";
            this.cmbTransNo.Size = new System.Drawing.Size(155, 23);
            this.cmbTransNo.TabIndex = 50;
            this.cmbTransNo.SelectedIndexChanged += new System.EventHandler(this.cmbTransNo_SelectedIndexChanged);
            // 
            // lblchujian
            // 
            this.lblchujian.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblchujian.Location = new System.Drawing.Point(5, 4);
            this.lblchujian.Name = "lblchujian";
            this.lblchujian.Size = new System.Drawing.Size(100, 20);
            this.lblchujian.Text = "转储作业";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(5, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.Text = "提示信息";
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(80, 297);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(156, 20);
            this.lblMessage.Text = "X";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 20);
            this.label4.Text = "目标货位";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 20);
            this.label5.Text = "原箱号";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.Text = "SN";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLocationCode
            // 
            this.txtLocationCode.Location = new System.Drawing.Point(81, 77);
            this.txtLocationCode.Name = "txtLocationCode";
            this.txtLocationCode.Size = new System.Drawing.Size(155, 23);
            this.txtLocationCode.TabIndex = 93;
            this.txtLocationCode.TextChanged += new System.EventHandler(this.txtLocationCode_TextChanged);
            // 
            // txtSN
            // 
            this.txtSN.Enabled = false;
            this.txtSN.Location = new System.Drawing.Point(81, 152);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(155, 23);
            this.txtSN.TabIndex = 109;
            this.txtSN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSN_KeyPress);
            // 
            // txtQTY
            // 
            this.txtQTY.Enabled = false;
            this.txtQTY.Location = new System.Drawing.Point(81, 127);
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.Size = new System.Drawing.Size(155, 23);
            this.txtQTY.TabIndex = 127;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 20);
            this.label9.Text = "数量";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.Text = "目标箱号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtTLocationCartonNo
            // 
            this.txtTLocationCartonNo.Location = new System.Drawing.Point(81, 177);
            this.txtTLocationCartonNo.Name = "txtTLocationCartonNo";
            this.txtTLocationCartonNo.Size = new System.Drawing.Size(155, 23);
            this.txtTLocationCartonNo.TabIndex = 144;
            // 
            // rdbSplitCarton
            // 
            this.rdbSplitCarton.Location = new System.Drawing.Point(92, 55);
            this.rdbSplitCarton.Name = "rdbSplitCarton";
            this.rdbSplitCarton.Size = new System.Drawing.Size(54, 20);
            this.rdbSplitCarton.TabIndex = 146;
            this.rdbSplitCarton.TabStop = false;
            this.rdbSplitCarton.Text = "拆箱";
            this.rdbSplitCarton.CheckedChanged += new System.EventHandler(this.rdbSplitCarton_CheckedChanged);
            // 
            // rdbAllCarton
            // 
            this.rdbAllCarton.Checked = true;
            this.rdbAllCarton.Location = new System.Drawing.Point(162, 55);
            this.rdbAllCarton.Name = "rdbAllCarton";
            this.rdbAllCarton.Size = new System.Drawing.Size(54, 20);
            this.rdbAllCarton.TabIndex = 147;
            this.rdbAllCarton.Text = "整箱";
            // 
            // FStorLocTransOperations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.rdbAllCarton);
            this.Controls.Add(this.rdbSplitCarton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTLocationCartonNo);
            this.Controls.Add(this.txtQTY);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.txtLocationCode);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblchujian);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtFromCartonNo);
            this.Controls.Add(this.lblTransNo);
            this.Controls.Add(this.cmbTransNo);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.Name = "FStorLocTransOperations";
            this.Size = new System.Drawing.Size(240, 345);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtFromCartonNo;
        private System.Windows.Forms.Label lblTransNo;
        private System.Windows.Forms.ComboBox cmbTransNo;
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLocationCode;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.TextBox txtQTY;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTLocationCartonNo;
        private System.Windows.Forms.RadioButton rdbSplitCarton;
        private System.Windows.Forms.RadioButton rdbAllCarton;
    }
}
