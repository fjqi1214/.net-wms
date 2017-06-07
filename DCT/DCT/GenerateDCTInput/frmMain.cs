using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace GenerateDCTInput
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtInterval;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtInputFormat;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtCurrentInput;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.TextBox txtClientID;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Timer timerOutput;
		private System.Windows.Forms.ListBox lstOutput;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtInputLine;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Label label4;
		private System.ComponentModel.IContainer components;

		public frmMain()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.txtInterval = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtInputFormat = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtCurrentInput = new System.Windows.Forms.TextBox();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnPause = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.txtClientID = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.timerOutput = new System.Windows.Forms.Timer(this.components);
			this.lstOutput = new System.Windows.Forms.ListBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtInputLine = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(32, 320);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Interval";
			// 
			// txtInterval
			// 
			this.txtInterval.Location = new System.Drawing.Point(96, 320);
			this.txtInterval.Name = "txtInterval";
			this.txtInterval.Size = new System.Drawing.Size(48, 21);
			this.txtInterval.TabIndex = 1;
			this.txtInterval.Text = "2000";
			this.txtInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInterval_KeyPress);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 360);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "Input Format";
			// 
			// txtInputFormat
			// 
			this.txtInputFormat.Location = new System.Drawing.Point(96, 352);
			this.txtInputFormat.Multiline = true;
			this.txtInputFormat.Name = "txtInputFormat";
			this.txtInputFormat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtInputFormat.Size = new System.Drawing.Size(384, 88);
			this.txtInputFormat.TabIndex = 3;
			this.txtInputFormat.Text = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(0, 456);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 17);
			this.label3.TabIndex = 4;
			this.label3.Text = "Current Input";
			// 
			// txtCurrentInput
			// 
			this.txtCurrentInput.Location = new System.Drawing.Point(96, 448);
			this.txtCurrentInput.Multiline = true;
			this.txtCurrentInput.Name = "txtCurrentInput";
			this.txtCurrentInput.Size = new System.Drawing.Size(384, 80);
			this.txtCurrentInput.TabIndex = 5;
			this.txtCurrentInput.Text = "";
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(312, 320);
			this.btnStart.Name = "btnStart";
			this.btnStart.TabIndex = 6;
			this.btnStart.Text = "Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnPause
			// 
			this.btnPause.Location = new System.Drawing.Point(400, 320);
			this.btnPause.Name = "btnPause";
			this.btnPause.TabIndex = 7;
			this.btnPause.Text = "Pause";
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// txtClientID
			// 
			this.txtClientID.Location = new System.Drawing.Point(224, 320);
			this.txtClientID.Name = "txtClientID";
			this.txtClientID.Size = new System.Drawing.Size(64, 21);
			this.txtClientID.TabIndex = 11;
			this.txtClientID.Text = "0";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(152, 320);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 17);
			this.label5.TabIndex = 10;
			this.label5.Text = "Client ID";
			// 
			// timerOutput
			// 
			this.timerOutput.Tick += new System.EventHandler(this.timerOutput_Tick);
			// 
			// lstOutput
			// 
			this.lstOutput.ItemHeight = 12;
			this.lstOutput.Location = new System.Drawing.Point(96, 40);
			this.lstOutput.Name = "lstOutput";
			this.lstOutput.Size = new System.Drawing.Size(384, 208);
			this.lstOutput.TabIndex = 15;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(40, 40);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(42, 17);
			this.label6.TabIndex = 14;
			this.label6.Text = "Output";
			// 
			// txtInputLine
			// 
			this.txtInputLine.Location = new System.Drawing.Point(96, 256);
			this.txtInputLine.Name = "txtInputLine";
			this.txtInputLine.Size = new System.Drawing.Size(384, 21);
			this.txtInputLine.TabIndex = 17;
			this.txtInputLine.Text = "";
			this.txtInputLine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInputLine_KeyPress);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(56, 264);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(35, 17);
			this.label7.TabIndex = 16;
			this.label7.Text = "Input";
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(96, 8);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(384, 21);
			this.txtPath.TabIndex = 9;
			this.txtPath.Text = "";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(56, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 17);
			this.label4.TabIndex = 8;
			this.label4.Text = "Path";
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(488, 533);
			this.Controls.Add(this.txtInputLine);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.lstOutput);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtClientID);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtCurrentInput);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtInputFormat);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtInterval);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.btnStart);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DCT Input Simulation for RS485";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			GenerateData();
		}

		private int iStringInputIndex = 0;
		private void GenerateData()
		{
			if (iStringInputIndex == -1)
				return;
			if (iStringInputIndex < listStringInput.Count && listStringInput.Count > 0)
			{
				GenerateFile(this.txtClientID.Text + ":" + listStringInput[iStringInputIndex].ToString());
				iStringInputIndex++;
			}
			else
			{
				string strCurrentOutput = string.Empty;
				for (int iLoopInputIndex = 0; iLoopInputIndex < listLoopInput.Count; iLoopInputIndex++)
				{
					string strLine = listLoopInput[iLoopInputIndex].ToString();
					string[] strTmp = strLine.Split(':');
					string strPrefix = strTmp[1];
					int iLen = Convert.ToInt32(strTmp[2]);
					int iCurrent = Convert.ToInt32(htLoopIdx[iLoopInputIndex]);
					int iEnd = -1;
					if (strTmp.Length >= 5)
					{
						iEnd = Convert.ToInt32(strTmp[4]);
					}
					int iSleep = 0;
					if (strTmp.Length >= 6)
					{
						iSleep = Convert.ToInt32(strTmp[5]);
					}
				
					string strInput = strPrefix + iCurrent.ToString().PadLeft(iLen, '0');
					GenerateFileErrorTimes = 0;
					GenerateFile(this.txtClientID.Text + ":" + strInput);
					strCurrentOutput += strInput + "\r\n";
					iCurrent++;
					htLoopIdx[iLoopInputIndex] = iCurrent;

					if (iEnd > 0 && iCurrent >= iEnd)
					{
						btnPause_Click(null, null);
					}
					if (iSleep > 0)
					{
						System.Threading.Thread.Sleep(iSleep);
					}
				}
				this.txtCurrentInput.Text = strCurrentOutput;
			}
		}
		private int GenerateFileErrorTimes = 0;
		private void GenerateFile(string data)
		{
			try
			{
				string strFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + DateTime.Now.Millisecond.ToString().PadLeft(3, '0') + "-";
				strFileName = System.IO.Path.Combine(this.txtPath.Text, strFileName);
				string strExt = (new System.Random(DateTime.Now.Second * DateTime.Now.Millisecond).Next(999).ToString().PadLeft(3, '0')) + ".txt";
				while (System.IO.File.Exists(strFileName + strExt) == true)
				{
					strExt = (new System.Random(DateTime.Now.Second * DateTime.Now.Millisecond).Next(999).ToString().PadLeft(3, '0')) + ".txt";
				}
				strFileName = strFileName + strExt;
				System.IO.FileStream s = new System.IO.FileStream(strFileName, System.IO.FileMode.CreateNew);
				byte[] byteData = new byte[data.Length];
				byteData = System.Text.Encoding.UTF8.GetBytes(data);
				s.Write(byteData, 0, byteData.Length);
				s.Close();
			}
			catch (Exception ex)
			{
				GenerateFileErrorTimes++;
				if (GenerateFileErrorTimes >= 3)
				{
					MessageBox.Show(ex.Message);
					this.btnPause_Click(null, null);
				}
				else
				{
					GenerateFile(data);
				}
			}
		}

		private ArrayList listStringInput = null;	// 一次性输入
		private ArrayList listLoopInput = null;			// 循环输入部分
		private Hashtable htLoopIdx = null;			// 循环输入的当前序号
		private bool ParseInputFormat()
		{
			try
			{
				listStringInput = new ArrayList();
				listLoopInput = new ArrayList();
				htLoopIdx = new Hashtable();
				string[] strLines = this.txtInputFormat.Text.Split('\r', '\n');
				int iLoopStart = 0;
				// 读入一次性输入部分
				for (int i = 0; i < strLines.Length; i++)
				{
					if (strLines[i].Trim() != string.Empty)
					{
						if (strLines[i].StartsWith("LOOP") == true)
						{
							iLoopStart = i;
							break;
						}
						listStringInput.Add(strLines[i].Trim());
					}
				}
				// 读入循环输入部分
				for (int i = iLoopStart; i < strLines.Length; i++)
				{
					if (strLines[i].Trim() != string.Empty && strLines[i].Trim() != "LOOP")
					{
						listLoopInput.Add(strLines[i].Trim());
					}
				}
				// 读取循环部分的开始序号
				for (int i = 0; i < listLoopInput.Count; i++)
				{
					string strLine = listLoopInput[i].ToString();
					if (strLine.StartsWith("LOOP") == true)
					{
						string[] strTmp = strLine.Split(':');
						htLoopIdx.Add(i, strTmp[3]);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
			return true;
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			if (ParseInputFormat() == true)
			{
				
				iStringInputIndex = 0;
				timer1.Interval = int.Parse(this.txtInterval.Text);
				timer1.Start();
				this.btnStart.Enabled = false;
				this.btnPause.Enabled = true;

				this.timerOutput.Interval = 100;
				this.timerOutput.Start();
			}
		}

		private void txtInterval_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\n')
			{
				timer1.Interval = int.Parse(this.txtInterval.Text);
			}
		}

		private void btnPause_Click(object sender, System.EventArgs e)
		{
			timer1.Enabled = false;
			this.btnStart.Enabled = true;
			this.btnPause.Enabled = false;
			this.timerOutput.Enabled = false;
		}

		private void timerOutput_Tick(object sender, System.EventArgs e)
		{
			string strPath = System.IO.Path.Combine(this.txtPath.Text, "Output");
			string[] strFiles = System.IO.Directory.GetFiles(strPath, this.txtClientID.Text + "*.txt");
			if (strFiles.Length == 0)
				return;
			SortedList sortList = new SortedList();
			for (int i = 0; i < strFiles.Length; i++)
			{
				sortList.Add(strFiles[i], strFiles[i]);
			}
			foreach (object objFile in sortList.Keys)
			{
				try
				{
					System.IO.StreamReader reader = new System.IO.StreamReader(objFile.ToString(), System.Text.Encoding.GetEncoding("GB2312"));
					string strLine = reader.ReadLine();
					this.lstOutput.Items.Add(">> " + strLine);
					this.lstOutput.SelectedIndex = this.lstOutput.Items.Count - 1;
					reader.Close();
					System.IO.File.Delete(objFile.ToString());
				}
				catch
				{}
			}
		}

		private void txtInputLine_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				GenerateFile(this.txtClientID.Text + ":" + this.txtInputLine.Text.Trim().ToUpper());
				this.txtInputLine.Text = "";
				if (this.timerOutput.Enabled == false)
					this.timerOutput.Enabled = true;
			}
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			//
		}
	}
}
