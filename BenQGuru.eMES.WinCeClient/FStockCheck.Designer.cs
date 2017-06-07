namespace BenQGuru.eMES.WinCeClient
{
    partial class FStockCheck
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLocationCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCARTONNO = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.cboCheckNo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQTY = new System.Windows.Forms.TextBox();
            this.txtDQMCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDiffDesc = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "盘点单号";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "实际货位";
            // 
            // txtLocationCode
            // 
            this.txtLocationCode.Location = new System.Drawing.Point(103, 29);
            this.txtLocationCode.Name = "txtLocationCode";
            this.txtLocationCode.Size = new System.Drawing.Size(100, 23);
            this.txtLocationCode.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "箱号";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 20);
            this.label4.Text = "鼎桥物料编码";
            // 
            // txtCARTONNO
            // 
            this.txtCARTONNO.Location = new System.Drawing.Point(103, 53);
            this.txtCARTONNO.Name = "txtCARTONNO";
            this.txtCARTONNO.Size = new System.Drawing.Size(100, 23);
            this.txtCARTONNO.TabIndex = 2;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(145, 131);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(58, 20);
            this.btnSubmit.TabIndex = 19;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(1, 154);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(211, 147);
            this.dataGrid1.TabIndex = 21;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // cboCheckNo
            // 
            this.cboCheckNo.Location = new System.Drawing.Point(64, 3);
            this.cboCheckNo.Name = "cboCheckNo";
            this.cboCheckNo.Size = new System.Drawing.Size(148, 23);
            this.cboCheckNo.TabIndex = 26;
            this.cboCheckNo.SelectedIndexChanged += new System.EventHandler(this.cboCheckNo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.Text = "数量";
            // 
            // txtQTY
            // 
            this.txtQTY.Location = new System.Drawing.Point(42, 128);
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.Size = new System.Drawing.Size(55, 23);
            this.txtQTY.TabIndex = 2;
            // 
            // txtDQMCode
            // 
            this.txtDQMCode.Location = new System.Drawing.Point(103, 76);
            this.txtDQMCode.Name = "txtDQMCode";
            this.txtDQMCode.Size = new System.Drawing.Size(100, 23);
            this.txtDQMCode.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 20);
            this.label6.Text = "差异原因";
            // 
            // txtDiffDesc
            // 
            this.txtDiffDesc.Location = new System.Drawing.Point(103, 105);
            this.txtDiffDesc.Name = "txtDiffDesc";
            this.txtDiffDesc.Size = new System.Drawing.Size(100, 23);
            this.txtDiffDesc.TabIndex = 34;
            // 
            // FStockCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.txtDiffDesc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboCheckNo);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtCARTONNO);
            this.Controls.Add(this.txtDQMCode);
            this.Controls.Add(this.txtQTY);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLocationCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FStockCheck";
            this.Size = new System.Drawing.Size(215, 304);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLocationCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCARTONNO;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.ComboBox cboCheckNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQTY;
        private System.Windows.Forms.TextBox txtDQMCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDiffDesc;
    }
}
