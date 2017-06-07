namespace BenQGuru.eMES.WinCeClient
{
	partial class FASNRECEIVEMP
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

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.cmbASNNO = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSN = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.SuspendLayout();
            // 
            // cmbASNNO
            // 
            this.cmbASNNO.Location = new System.Drawing.Point(92, 2);
            this.cmbASNNO.Name = "cmbASNNO";
            this.cmbASNNO.Size = new System.Drawing.Size(117, 23);
            this.cmbASNNO.TabIndex = 0;
            this.cmbASNNO.SelectedIndexChanged += new System.EventHandler(this.cmbASNNO_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.Text = "入库指令号:";
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(168, 31);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(67, 20);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "急料";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.Text = "SN号:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.Text = "箱号编码:";
            // 
            // txtSN
            // 
            this.txtSN.Location = new System.Drawing.Point(44, 27);
            this.txtSN.Name = "txtSN";
            this.txtSN.Size = new System.Drawing.Size(106, 23);
            this.txtSN.TabIndex = 6;
            this.txtSN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSN_KeyPress);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(72, 51);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(107, 23);
            this.textBox2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 20);
            this.label4.Text = "华为料号编码:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(1, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.Text = "拒收箱数:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(72, 153);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(50, 23);
            this.textBox3.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 20);
            this.button1.TabIndex = 11;
            this.button1.Text = "拒收";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(180, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(43, 20);
            this.button2.TabIndex = 12;
            this.button2.Text = "接收";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(102, 231);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 20);
            this.button3.TabIndex = 13;
            this.button3.Text = "让步接收";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(1, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 20);
            this.label6.Text = "拒收原因:";
            // 
            // comboBox2
            // 
            this.comboBox2.Location = new System.Drawing.Point(72, 177);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(100, 23);
            this.comboBox2.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(1, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 20);
            this.label7.Text = "让步接收问题:";
            // 
            // comboBox3
            // 
            this.comboBox3.Location = new System.Drawing.Point(102, 202);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(100, 23);
            this.comboBox3.TabIndex = 17;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(3, 231);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 20);
            this.button4.TabIndex = 18;
            this.button4.Text = "图片上传";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(0, 254);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 20);
            this.label8.Text = "箱数:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(59, 254);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 20);
            this.label9.Text = "拒收:";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(129, 254);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 20);
            this.label10.Text = "应收:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(1, 274);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 20);
            this.label11.Text = "已收:";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(69, 274);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 20);
            this.label12.Text = "让步接收:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(168, 272);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(40, 20);
            this.button5.TabIndex = 24;
            this.button5.Text = "返回";
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGrid1.Location = new System.Drawing.Point(4, 77);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(231, 53);
            this.dataGrid1.TabIndex = 37;
            this.dataGrid1.CurrentCellChanged += new System.EventHandler(this.dataGrid1_CurrentCellChanged);
            this.dataGrid1.GotFocus += new System.EventHandler(this.dataGrid1_GotFocus);
            // 
            // FASNRECEIVEMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.txtSN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbASNNO);
            this.Name = "FASNRECEIVEMP";
            this.Text = "初检验收";
            this.Load += new System.EventHandler(this.FASNRECEIVEMP_Load);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ComboBox cmbASNNO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSN;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGrid dataGrid1;
	}
}