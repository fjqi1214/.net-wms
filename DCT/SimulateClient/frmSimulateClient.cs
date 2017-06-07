using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.IO;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DCTSimulateClient
{
	/// <summary>
	/// frmSimulateClient 的摘要说明。
	/// </summary>
	public class frmSimulateClient : System.Windows.Forms.Form
	{
		static void Main(string[] args)
		{
			
			Application.Run(new frmSimulateClient());
			
		}

		private delegate void InsertTextHandler(int index,object obj);


		private Socket socket = null;

		private Socket sourceSocket = null;

		Thread th = null;

		//private TcpListener lisServer = new TcpListener(IPAddress.Parse("127.0.0.1"),55962);

		private System.Windows.Forms.ListBox lstMessage;
		private System.Windows.Forms.TextBox txtSendedData;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.CheckBox chkPause;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSimulateClient()
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
				if(components != null)
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
			this.lstMessage = new System.Windows.Forms.ListBox();
			this.txtSendedData = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.chkPause = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lstMessage
			// 
			this.lstMessage.ItemHeight = 12;
			this.lstMessage.Location = new System.Drawing.Point(8, 8);
			this.lstMessage.Name = "lstMessage";
			this.lstMessage.Size = new System.Drawing.Size(520, 280);
			this.lstMessage.TabIndex = 0;
			// 
			// txtSendedData
			// 
			this.txtSendedData.Location = new System.Drawing.Point(8, 304);
			this.txtSendedData.Name = "txtSendedData";
			this.txtSendedData.Size = new System.Drawing.Size(424, 21);
			this.txtSendedData.TabIndex = 1;
			this.txtSendedData.Text = "";
			this.txtSendedData.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSendedData_KeyUp);
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(448, 304);
			this.btnSend.Name = "btnSend";
			this.btnSend.TabIndex = 2;
			this.btnSend.Text = "Send";
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// chkPause
			// 
			this.chkPause.Location = new System.Drawing.Point(448, 336);
			this.chkPause.Name = "chkPause";
			this.chkPause.Size = new System.Drawing.Size(56, 24);
			this.chkPause.TabIndex = 3;
			this.chkPause.Text = "暂停";
			// 
			// frmSimulateClient
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(536, 373);
			this.Controls.Add(this.chkPause);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.txtSendedData);
			this.Controls.Add(this.lstMessage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSimulateClient";
			this.Text = "frmSimulateClient";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmSimulateClient_Closing);
			this.Load += new System.EventHandler(this.frmSimulateClient_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private SimulationCommand simulationCmd = null;
		private log4net.ILog log = null;
		private string localIP = string.Empty;
		private DateTime dtEndTime = DateTime.MinValue;
		private bool updatedLogFile = false;
		private void frmSimulateClient_Load(object sender, System.EventArgs e)
		{
//			string strEndTime = System.Configuration.ConfigurationSettings.AppSettings["END_TIME"];
//			if (strEndTime != null && strEndTime != string.Empty)
//			{
//				dtEndTime = Convert.ToDateTime(strEndTime);
//			}
//			localIP = GetLocalIP();
//			if (localIP == string.Empty)
//			{
//				MessageBox.Show("无法找到当前IP", "DCT Client");
//				Application.Exit();
//			}
			
			/*
			string strPath = Application.StartupPath;
			if (!strPath.EndsWith("\\"))
				strPath += "\\";
			string strCmdFile = strPath + "Command.xml";
			if (!System.IO.File.Exists(strCmdFile))
			{
				strCmdFile = strPath + "..\\..\\Command.xml";
			}
			if (System.IO.File.Exists(strCmdFile))
			{
				simulationCmd = new SimulationCommand(strCmdFile);
			}
//			string[] ip = localIP.Split('.');
//			int iIP = Convert.ToInt32(ip[ip.Length - 1]);
//			simulationCmd.RCardPrefix = simulationCmd.RCardPrefix + iIP.ToString().PadLeft(3, '0');
			
			log4net.Config.DOMConfigurator.ConfigureAndWatch(new System.IO.FileInfo(strPath + "log4net.dll.log4net"));
			log = log4net.LogManager.GetLogger(typeof(frmSimulateClient));
			timeWriter = new StreamWriter(strPath + "TimeCost.log", true);
			*/
			
			InitialServer();
			ThreadStart ts = new ThreadStart(ThreadProc);
			th = new Thread(ts);
			th.IsBackground = true;
			th.Start();
			//lisServer.Start();
		}

		private void InitialServer()
		{
			try
			{
				sourceSocket = new Socket(AddressFamily.InterNetwork
					,SocketType.Stream
					,ProtocolType.IP);

				sourceSocket.Bind(new IPEndPoint(IPAddress.Parse(
					System.Configuration.ConfigurationSettings.AppSettings["IP"].Trim())
					,Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["PORT"].Trim())));
				/*
				sourceSocket.Bind(new IPEndPoint(IPAddress.Parse(
					localIP)
					,Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["PORT"].Trim())));
				*/

				sourceSocket.Listen(0);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnSend_Click(object sender, System.EventArgs e)
		{
//			if(lstMessage.Items.Count > 0)
//			{
//				if(lstMessage.Items[lstMessage.Items.Count - 1].ToString() == "EXIT")
//				{
//					InitialServer();
//				}
//			}
			if(!socket.Connected || socket.Poll(100,SelectMode.SelectRead) )
			{
				try
				{
					socket = sourceSocket.Accept();
					socket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReceiveTimeout,100);
				}
				catch(Exception ex)
				{
				}
			}
			if(socket.Connected)
			{
				try
				{
					
					socket.Send(System.Text.Encoding.Default.GetBytes((new string(' ', 8)) + txtSendedData.Text.Trim().ToUpper()));

					lstMessage.Items.Insert(0,">> " + txtSendedData.Text.Trim().ToUpper());
					//lstMessage.
					txtSendedData.Text = String.Empty;
				}
				catch
				{}
			}
		}

		public void ThreadProc()
		{
			if(sourceSocket != null)
			{
				while (true) 
				{
					try 
					{
						if(socket == null || /*sourceSocket.Poll(200,SelectMode.SelectError)  ||  */!socket.Connected)
						{
							socket = sourceSocket.Accept();
							socket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReceiveTimeout,100);
						}
						if(socket.Connected)
						{
							string rec = String.Empty;
							byte[] btMSG = new byte[512];
							
							socket.Receive(btMSG);

							rec = System.Text.Encoding.GetEncoding("GB2312").GetString(RetrieveData(btMSG));
							InsertTextHandler mi = new InsertTextHandler(this.InsertText);
							//在窗体上调用 BeginInvoke
							this.Invoke(mi,new object[]{0,rec});
							//Thread.Sleep(200) ;
						}
					}
					catch (Exception ex) 
					{
					}
				}
			}
		}

		private bool bInCmd = false;
		public void InsertText(int index,object obj)
		{
			this.lstMessage.Items.Insert(index,obj);
			if (simulationCmd == null)
				return;
			if (bInCmd == true)
				return;
			bInCmd = true;
			if (obj.ToString().IndexOf("Failure") >= 0)
			{
				log.Debug("Failure\t" + this.currentRCard);
				Sleep();
				this.txtSendedData.Text = "CANCEL";
				this.btnSend_Click(null, null);
			}
			else if (obj.ToString().IndexOf("请") >= 0)
			{
				Sleep();
				SimulateSendData(obj.ToString());
			}
			bInCmd = false;
		}
		private void Sleep()
		{
			Application.DoEvents();
			return;
			DateTime dt = DateTime.Now.AddMilliseconds(500);
			while (true)
			{
				Application.DoEvents();
				if (DateTime.Now.CompareTo(dt) >= 0 && this.chkPause.Checked == false || this.Visible == false)
				{
					break;
				}
			}
		}

		public byte[] RetrieveData(byte[] btMsg)
		{
			byte[] btReturn = null;
			ArrayList al = new ArrayList();

			//btReturn = new byte[al.Count];
			for(int i = 0;i < btMsg.Length ; i++)
			{
				if(btMsg[i] != (byte)13 && (int)btMsg[i] != 0)
				{
					al.Add(btMsg[i]);
				}
			}

			btReturn = new byte[al.Count];

			for(int i = 0 ; i < al.Count ; i ++ )
			{
				btReturn[i] = Convert.ToByte(Convert.ToInt32(al[i]));
			}

			al = null;
			btMsg = null;

			return btReturn;
		}

		private void txtSendedData_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				btnSend_Click(this,null);
			}
		}

		private void frmSimulateClient_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//			if(socket != null)
			//			{
			try
			{
				//th = null;
				
				if (timeWriter != null)
				{
					timeWriter.Close();
				}
				
				if(socket != null)
				{
					socket.Shutdown(SocketShutdown.Both);
					socket.Close();
				}

				if(sourceSocket != null)
				{
					sourceSocket.Shutdown(SocketShutdown.Both);
					sourceSocket.Close();
				}
				th.Abort();
				
			}
			catch
			{}
			finally
			{
				//Thread.Sleep(5000);
				//socket.
				sourceSocket = null;
				socket = null;
				th = null;
					
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();

				if (updatedLogFile == false)
				{
					this.UpdateLogFile();
				}

				Application.Exit();
			}
			//			}
		}

		private bool inputedUserCode = false;
		private bool inputedPassword = false;
		private bool inputedResource = false;
		private int iCurrentCommandLine = -1;
		private string currentRCard = string.Empty;
		private bool rcardInputComplete = false;
		private DateTime startRCardTime = DateTime.Now;
		private void SimulateSendData(string message)
		{
			if (inputedUserCode == false)	// 输入用户名
			{
				this.txtSendedData.Text = simulationCmd.UserCode;
				inputedUserCode = true;
			}
			else if (inputedPassword == false)	//输入密码
			{
				this.txtSendedData.Text = simulationCmd.Password;
				inputedPassword = true;
			}
			else if (inputedResource == false)	//输入资源
			{
				this.txtSendedData.Text = simulationCmd.ResourceCode;
				inputedResource = true;
			}
			else if (message.IndexOf("請輸入產品序列號") >= 0)
			{
				if (rcardInputComplete == true)
				{
					WriteCompleteLog();
				}

				// 空闲2秒
				DateTime dtSleepTo = DateTime.Now.AddSeconds(2);
				while (DateTime.Now.CompareTo(dtSleepTo) < 0)
				{
					Application.DoEvents();
				}
				
				if (dtEndTime != DateTime.MinValue)
				{
					if (DateTime.Now.CompareTo(dtEndTime) >= 0)
					{
						this.UpdateLogFile();
						Application.Exit();
					}
				}
				
				simulationCmd.RCardCurrentSeq++;
				if (simulationCmd.RCardCurrentSeq > simulationCmd.RCardSeqCount)
					return;
				simulationCmd.SaveCurrentSeq(simulationCmd.RCardCurrentSeq);
				string strSeq = simulationCmd.RCardCurrentSeq.ToString();
				strSeq = strSeq.PadLeft(simulationCmd.RCardSeqLength, '0');
				string rcard = simulationCmd.RCardPrefix + strSeq;
				iCurrentCommandLine = 0;
				rcardInputComplete = false;
				startRCardTime = DateTime.Now;
				currentRCard = rcard;
				this.txtSendedData.Text = rcard;
			}
			else	//输入实际命令
			{
				if (iCurrentCommandLine == -2)
				{
					this.txtSendedData.Text = "CANCEL";
					iCurrentCommandLine = -1;
				}
				else
				{
					string strLine = simulationCmd.CommandLine[iCurrentCommandLine];
					strLine = strLine.Replace("[RCARD]", currentRCard);
					this.txtSendedData.Text = strLine;
					iCurrentCommandLine++;
					if (iCurrentCommandLine == simulationCmd.CommandLine.Length)
					{
						rcardInputComplete = true;
						iCurrentCommandLine = -2;
					}
				}
			}
			this.btnSend_Click(null, null);
		}
		
		private int iTotalTimeCount = 0;
		private int iTotalTimeValue = 0;
		private StreamWriter timeWriter = null;
		private int iMaxCost = 0;
		private int iRCardCount = 0;
		private void WriteCompleteLog()
		{
			iRCardCount++;
			DateTime endTime = DateTime.Now;
			TimeSpan ts = endTime - startRCardTime;
			iTotalTimeValue += Convert.ToInt32(ts.TotalMilliseconds);
			string strLog = this.currentRCard + "\t" + simulationCmd.RCardCurrentSeq.ToString() + "\t" + ts.TotalMilliseconds.ToString();
			if (iRCardCount > 10)
			{
				if (ts.TotalMilliseconds > iMaxCost)
				{
					strLog += "\tMaxCost:" + ts.TotalMilliseconds.ToString();
					iMaxCost = Convert.ToInt32(ts.TotalMilliseconds);
				}
			}
			log.Debug(strLog);
			iTotalTimeCount++;
			if (iTotalTimeCount >= 100)
			{
				iTotalTimeValue = iTotalTimeValue / iTotalTimeCount;
				timeWriter.WriteLine(iTotalTimeValue.ToString() + "\t" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
				timeWriter.Flush();
				iTotalTimeValue = 0;
				iTotalTimeCount = 0;
			}
		}
		
		private string GetLocalIP()
		{
			System.Net.IPAddress[] addressList = Dns.GetHostByName(Dns.GetHostName()).AddressList;
			if (addressList != null && addressList.Length > 0)
			{
				return addressList[0].ToString();
			}
			return string.Empty;
		}

		private void UpdateLogFile()
		{
			/*
			string strPath = Application.StartupPath;
			if (! strPath.EndsWith("\\"))
				strPath += "\\";
			System.IO.File.Copy(strPath + "log-file.txt", @"\\grd1-icyer-yang\AutoTestLog$\log-file" + localIP + "-" + DateTime.Now.ToString("HHmmss") + ".txt");
			System.IO.File.Copy(strPath + "TimeCost.log", @"\\grd1-icyer-yang\AutoTestLog$\TimeCost" + localIP + "-" + DateTime.Now.ToString("HHmmss") + ".log");
			updatedLogFile = true;
			*/
		}

	}

	public class SimulationCommand
	{
		public string UserCode = string.Empty;
		public string Password = string.Empty;
		public string ResourceCode = string.Empty;
		public string RCardPrefix = string.Empty;
		public int RCardSeqLength = 0;
		public int RCardSeqStart = 0;
		public int RCardSeqCount = 0;
		public int RCardCurrentSeq = 0;
		public string[] CommandLine = null;

		private string FileName = string.Empty;
		private XmlDocument xmlDoc = null;
		private XmlNode nodeCurrentSeq = null;
		public SimulationCommand(string fileName)
		{
			this.FileName = fileName;
			xmlDoc = new XmlDocument();
			xmlDoc.Load(fileName);
			XmlNode node = xmlDoc.SelectSingleNode("//usercode");
			this.UserCode = node.FirstChild.Value;
			this.Password = xmlDoc.SelectSingleNode("//password").FirstChild.Value;
			this.ResourceCode = xmlDoc.SelectSingleNode("//resource").FirstChild.Value;
			node = xmlDoc.SelectSingleNode("//rcard");
			this.RCardPrefix = node.Attributes["prefix"].Value;
			this.RCardSeqLength = Convert.ToInt32(node.Attributes["seqlen"].Value);
			this.RCardSeqStart = Convert.ToInt32(node.Attributes["seqstart"].Value);
			this.RCardSeqCount = Convert.ToInt32(node.Attributes["seqcount"].Value);
			node = xmlDoc.SelectSingleNode("//commandline");
			string[] strTmp = node.FirstChild.Value.Split('\r');
			ArrayList list = new ArrayList();
			for (int i = 0; i < strTmp.Length; i++)
			{
				strTmp[i] = strTmp[i].Replace("\n", "").Replace("\t", "").Trim();
				if (strTmp[i] != string.Empty)
					list.Add(strTmp[i]);
			}
			CommandLine = new string[list.Count];
			list.CopyTo(CommandLine);
			
			this.RCardCurrentSeq = Convert.ToInt32(xmlDoc.SelectSingleNode("//rcardcurrentseq").FirstChild.Value);
			if (this.RCardCurrentSeq < this.RCardSeqStart)
				this.RCardCurrentSeq = this.RCardSeqStart;
		}

		public void SaveCurrentSeq(int currentSeq)
		{
			nodeCurrentSeq = xmlDoc.SelectSingleNode("//rcardcurrentseq");
			nodeCurrentSeq.FirstChild.Value = currentSeq.ToString();
			xmlDoc.Save(this.FileName);
		}

	}

}
