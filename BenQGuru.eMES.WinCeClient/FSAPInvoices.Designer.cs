namespace BenQGuru.eMES.WinCeClient
{
    partial class FSAPInvoices
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
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.btnReturn = new System.Windows.Forms.Button();
            this.txtPickNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(3, 44);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(233, 146);
            this.dataGrid1.TabIndex = 4;
            this.dataGrid1.TableStyles.Add(this.dataGridTableStyle1);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(82, 196);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(74, 22);
            this.btnReturn.TabIndex = 5;
            this.btnReturn.Text = "返回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // txtPickNo
            // 
            this.txtPickNo.Enabled = false;
            this.txtPickNo.Location = new System.Drawing.Point(92, 9);
            this.txtPickNo.Name = "txtPickNo";
            this.txtPickNo.Size = new System.Drawing.Size(127, 23);
            this.txtPickNo.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.Text = "拣货任务令：";
            // 
            // FSAPInvoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPickNo);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.dataGrid1);
            this.Name = "FSAPInvoices";
            this.Size = new System.Drawing.Size(239, 300);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.TextBox txtPickNo;
        private System.Windows.Forms.Label label1;
    }
}
