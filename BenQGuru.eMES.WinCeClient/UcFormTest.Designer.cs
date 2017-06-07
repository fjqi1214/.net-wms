namespace BenQGuru.eMES.WinCeClient
{
    partial class UcFormTest
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnGetUser = new System.Windows.Forms.Button();
            this.btnLoadGrid = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.btnHello = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 243);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(72, 23);
            this.textBox1.TabIndex = 9;
            // 
            // btnGetUser
            // 
            this.btnGetUser.Location = new System.Drawing.Point(132, 243);
            this.btnGetUser.Name = "btnGetUser";
            this.btnGetUser.Size = new System.Drawing.Size(72, 20);
            this.btnGetUser.TabIndex = 8;
            this.btnGetUser.Text = "Getuser";
            this.btnGetUser.Click += new System.EventHandler(this.btnGetUser_Click);
            // 
            // btnLoadGrid
            // 
            this.btnLoadGrid.Location = new System.Drawing.Point(132, 217);
            this.btnLoadGrid.Name = "btnLoadGrid";
            this.btnLoadGrid.Size = new System.Drawing.Size(72, 20);
            this.btnLoadGrid.TabIndex = 7;
            this.btnLoadGrid.Text = "LoadGrid";
            this.btnLoadGrid.Click += new System.EventHandler(this.btnLoadGrid_Click);
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(16, 12);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(209, 200);
            this.dataGrid1.TabIndex = 6;
            // 
            // btnHello
            // 
            this.btnHello.Location = new System.Drawing.Point(16, 218);
            this.btnHello.Name = "btnHello";
            this.btnHello.Size = new System.Drawing.Size(72, 20);
            this.btnHello.TabIndex = 5;
            this.btnHello.Text = "SayHello";
            this.btnHello.Click += new System.EventHandler(this.btnHello_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnGetUser);
            this.Controls.Add(this.btnLoadGrid);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.btnHello);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(240, 285);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnGetUser;
        private System.Windows.Forms.Button btnLoadGrid;
        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.Button btnHello;


    }
}
