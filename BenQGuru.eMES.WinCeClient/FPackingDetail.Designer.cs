namespace BenQGuru.eMES.WinCeClient
{
    partial class FPackingDetail
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
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblchujian = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblCarInvNO = new System.Windows.Forms.Label();
            this.lblPickNO = new System.Windows.Forms.Label();
            this.btnReturn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(5, 71);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(231, 223);
            this.dataGrid1.TabIndex = 62;
            this.dataGrid1.TableStyles.Add(this.ExampleDataTable);
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
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(19, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 20);
            this.label12.Text = "发货箱单号:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.Text = "拣货任务令号:";
            // 
            // lblchujian
            // 
            this.lblchujian.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblchujian.Location = new System.Drawing.Point(5, 4);
            this.lblchujian.Name = "lblchujian";
            this.lblchujian.Size = new System.Drawing.Size(100, 20);
            this.lblchujian.Text = "装箱详细";
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
            this.lblMessage.Location = new System.Drawing.Point(85, 297);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(100, 20);
            this.lblMessage.Text = "X";
            // 
            // lblCarInvNO
            // 
            this.lblCarInvNO.ForeColor = System.Drawing.Color.Red;
            this.lblCarInvNO.Location = new System.Drawing.Point(108, 48);
            this.lblCarInvNO.Name = "lblCarInvNO";
            this.lblCarInvNO.Size = new System.Drawing.Size(129, 20);
            // 
            // lblPickNO
            // 
            this.lblPickNO.ForeColor = System.Drawing.Color.Red;
            this.lblPickNO.Location = new System.Drawing.Point(106, 28);
            this.lblPickNO.Name = "lblPickNO";
            this.lblPickNO.Size = new System.Drawing.Size(130, 20);
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(98, 320);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(44, 20);
            this.btnReturn.TabIndex = 55;
            this.btnReturn.Text = "返回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // FPackingDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.Controls.Add(this.lblPickNO);
            this.Controls.Add(this.lblCarInvNO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblchujian);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnReturn);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.Name = "FPackingDetail";
            this.Size = new System.Drawing.Size(240, 345);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblchujian;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblCarInvNO;
        private System.Windows.Forms.DataGridTableStyle ExampleDataTable;
        private System.Windows.Forms.DataGridTextBoxColumn Check;
        private System.Windows.Forms.DataGridTextBoxColumn STLINE;
        //private System.Windows.Forms.DataGridTextBoxColumn CartonNo;
        private System.Windows.Forms.DataGridTextBoxColumn CartonSeq;
        private System.Windows.Forms.DataGridTextBoxColumn DQMcode;
        private System.Windows.Forms.DataGridTextBoxColumn MControlType;
        private System.Windows.Forms.DataGridTextBoxColumn Qty;
        private System.Windows.Forms.Label lblPickNO;
        private System.Windows.Forms.Button btnReturn;
    }
}
