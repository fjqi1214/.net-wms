namespace BenQGuru.eMES.WinCeClient
{
    partial class FLocStorTransDetailMP
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.Check = new System.Windows.Forms.DataGridTextBoxColumn();
            this.STNO = new System.Windows.Forms.DataGridTextBoxColumn();
            this.cartonno = new System.Windows.Forms.DataGridTextBoxColumn();
            this.relocaNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.locaNo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.DQMCode = new System.Windows.Forms.DataGridTextBoxColumn();
            this.stline = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnReturn = new System.Windows.Forms.Button();
            this.txtLocationNoEdit = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.Text = "货位移动单号";
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(87, 260);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(100, 20);
            this.lblMessage.Text = "X";
            // 
            // label10
            // 
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(7, 260);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 20);
            this.label10.Text = "消息提示：";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(0, 37);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(240, 209);
            this.dataGrid1.TabIndex = 13;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.GridColumnStyles.Add(this.Check);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.STNO);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.cartonno);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.relocaNo);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.locaNo);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.DQMCode);
            this.dataGridTableStyle1.GridColumnStyles.Add(this.stline);
            // 
            // Check
            // 
            this.Check.Format = "";
            this.Check.FormatInfo = null;
            this.Check.HeaderText = "选中";
            this.Check.Width = 30;
            // 
            // STNO
            // 
            this.STNO.Format = "";
            this.STNO.FormatInfo = null;
            this.STNO.HeaderText = "入库指令号";
            // 
            // cartonno
            // 
            this.cartonno.Format = "";
            this.cartonno.FormatInfo = null;
            this.cartonno.HeaderText = "箱号";
            // 
            // relocaNo
            // 
            this.relocaNo.Format = "";
            this.relocaNo.FormatInfo = null;
            this.relocaNo.HeaderText = "推荐货位";
            // 
            // locaNo
            // 
            this.locaNo.Format = "";
            this.locaNo.FormatInfo = null;
            this.locaNo.HeaderText = "货位";
            // 
            // DQMCode
            // 
            this.DQMCode.Format = "";
            this.DQMCode.FormatInfo = null;
            this.DQMCode.HeaderText = "鼎桥物料号";
            // 
            // stline
            // 
            this.stline.Format = "";
            this.stline.FormatInfo = null;
            this.stline.HeaderText = "行";
            this.stline.Width = 10;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(184, 283);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(48, 27);
            this.btnReturn.TabIndex = 15;
            this.btnReturn.Text = "返 回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // txtLocationNoEdit
            // 
            this.txtLocationNoEdit.ForeColor = System.Drawing.Color.Red;
            this.txtLocationNoEdit.Location = new System.Drawing.Point(102, 11);
            this.txtLocationNoEdit.Name = "txtLocationNoEdit";
            this.txtLocationNoEdit.Size = new System.Drawing.Size(130, 20);
            // 
            // FLocStorTransDetailMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.txtLocationNoEdit);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label1);
            this.Name = "FLocStorTransDetailMP";
            this.Size = new System.Drawing.Size(240, 320);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn Check;
        private System.Windows.Forms.DataGridTextBoxColumn STNO;
        private System.Windows.Forms.DataGridTextBoxColumn cartonno;
        private System.Windows.Forms.DataGridTextBoxColumn relocaNo;
        private System.Windows.Forms.DataGridTextBoxColumn locaNo;
        private System.Windows.Forms.DataGridTextBoxColumn DQMCode;
        private System.Windows.Forms.DataGridTextBoxColumn stline;
        private System.Windows.Forms.Label txtLocationNoEdit;
    }
}
