namespace BenQGuru.eMES.WinCeClient
{
    partial class FIQCNGRecord
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
            this.CartonSeq = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DQMcode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.MControlType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTransNo = new System.Windows.Forms.Label();
            this.cmbNGType = new System.Windows.Forms.ComboBox();
            this.lblchujian = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbNGDesc = new System.Windows.Forms.ComboBox();
            this.btnPicUpload = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNGQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCartonNo = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(6, 27);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(230, 87);
            this.dataGrid1.TabIndex = 62;
            this.dataGrid1.TableStyles.Add(this.ExampleDataTable);
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            // 
            // ExampleDataTable
            // 
            this.ExampleDataTable.GridColumnStyles.Add(this.Check);
            this.ExampleDataTable.GridColumnStyles.Add(this.STLINE);
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
            this.btnReturn.Location = new System.Drawing.Point(189, 325);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(47, 20);
            this.btnReturn.TabIndex = 61;
            this.btnReturn.Text = "返回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(22, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(44, 20);
            this.btnSave.TabIndex = 55;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTransNo
            // 
            this.lblTransNo.Location = new System.Drawing.Point(4, 123);
            this.lblTransNo.Name = "lblTransNo";
            this.lblTransNo.Size = new System.Drawing.Size(77, 20);
            this.lblTransNo.Text = "缺陷类型";
            this.lblTransNo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbNGType
            // 
            this.cmbNGType.Location = new System.Drawing.Point(87, 120);
            this.cmbNGType.Name = "cmbNGType";
            this.cmbNGType.Size = new System.Drawing.Size(148, 23);
            this.cmbNGType.TabIndex = 50;
            // 
            // lblchujian
            // 
            this.lblchujian.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblchujian.Location = new System.Drawing.Point(5, 4);
            this.lblchujian.Name = "lblchujian";
            this.lblchujian.Size = new System.Drawing.Size(100, 20);
            this.lblchujian.Text = "IQC缺陷记录";
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(0, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.Text = "提示信息";
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(75, 288);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(160, 34);
            this.lblMessage.Text = "X";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 20);
            this.label1.Text = "SN";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(87, 167);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(148, 23);
            this.txtSN.TabIndex = 144;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.Text = "缺陷描述";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbNGDesc
            // 
            this.cmbNGDesc.Location = new System.Drawing.Point(87, 143);
            this.cmbNGDesc.Name = "cmbNGDesc";
            this.cmbNGDesc.Size = new System.Drawing.Size(148, 23);
            this.cmbNGDesc.TabIndex = 158;
            // 
            // btnPicUpload
            // 
            this.btnPicUpload.Location = new System.Drawing.Point(81, 265);
            this.btnPicUpload.Name = "btnPicUpload";
            this.btnPicUpload.Size = new System.Drawing.Size(81, 20);
            this.btnPicUpload.TabIndex = 172;
            this.btnPicUpload.Text = "图片上传";
            this.btnPicUpload.Click += new System.EventHandler(this.btnPicUpload_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 20);
            this.label4.Text = "不良数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtNGQty
            // 
            this.txtNGQty.Location = new System.Drawing.Point(87, 191);
            this.txtNGQty.Name = "txtNGQty";
            this.txtNGQty.Size = new System.Drawing.Size(148, 23);
            this.txtNGQty.TabIndex = 186;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.Text = "备注";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMemo
            // 
            this.txtMemo.Location = new System.Drawing.Point(87, 215);
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.Size = new System.Drawing.Size(148, 23);
            this.txtMemo.TabIndex = 189;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(4, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.Text = "箱号";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtCartonNo
            // 
            this.txtCartonNo.Enabled = false;
            this.txtCartonNo.Location = new System.Drawing.Point(87, 239);
            this.txtCartonNo.Name = "txtCartonNo";
            this.txtCartonNo.Size = new System.Drawing.Size(148, 23);
            this.txtCartonNo.TabIndex = 192;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(176, 265);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(44, 20);
            this.btnDelete.TabIndex = 194;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // FIQCNGRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCartonNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMemo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtNGQty);
            this.Controls.Add(this.btnPicUpload);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbNGDesc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblchujian);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTransNo);
            this.Controls.Add(this.cmbNGType);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.Name = "FIQCNGRecord";
            this.Size = new System.Drawing.Size(240, 345);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTransNo;
        private System.Windows.Forms.ComboBox cmbNGType;
        private System.Windows.Forms.Label lblchujian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.DataGridTableStyle ExampleDataTable;
        private System.Windows.Forms.DataGridTextBoxColumn Check;
        private System.Windows.Forms.DataGridTextBoxColumn STLINE;
        //private System.Windows.Forms.DataGridTextBoxColumn CartonNo;
        private System.Windows.Forms.DataGridTextBoxColumn CartonSeq;
        private System.Windows.Forms.DataGridTextBoxColumn DQMcode;
        private System.Windows.Forms.DataGridTextBoxColumn MControlType;
        private System.Windows.Forms.DataGridTextBoxColumn Qty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbNGDesc;
        private System.Windows.Forms.Button btnPicUpload;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNGQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMemo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCartonNo;
        private System.Windows.Forms.Button btnDelete;
    }
}
