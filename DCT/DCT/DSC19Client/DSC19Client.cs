using System;

using System.Net;
using System.Net.Sockets;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DCT.ATop.DSC19.Library;

using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace BenQGuru.eMES.Common.DCT.ATop.DSC19
{
	/// <summary>
	/// DSC19Client 的摘要说明。
	/// </summary>
	public class DSC19Client:IDCTClient
	{
		
		private const string IN = "  IN  ";
		private const string OUT = "  OUT  ";

		private BaseDCTAction cachedAction = null;

		#region 变量

		//ip地址
		protected string client_ip;
		//端口
		protected int client_port;
		//描述
		protected string client_description = String.Empty;
		//ID
		protected int client_id;
		//状态
		protected DSC19ClientStatus client_status;
		//当前接受的字符串
		protected string client_recieve;
		//资源代码
		protected string client_rescode = String.Empty;
		//登陆用户
		protected string client_loginuser = String.Empty;
		//登陆用户
		protected string client_password = String.Empty;

		//通讯用的Socket
//		protected Socket socket;
		//是否登陆
		protected bool client_certificate;
		//DB数据提供对象
		protected object db_connection = null;
		//配置节点
		protected int node_id;
		#endregion

		#region 属性
		/// <summary>
		/// 端口
		/// </summary>
		public int ClientPort
		{
			set
			{
				client_port = value;
			}
			get
			{
				return client_port;
			}
		}
		/// <summary>
		/// DB 连接属性
		/// </summary>
		public object DBConnection
		{
			set
			{
				db_connection = value;
			}
			get
			{
				return db_connection;
			}
		}
		/// <summary>
		/// 是否为已经登陆
		/// </summary>
		public bool Authorized
		{
			get
			{
				return client_certificate;
			}
			set
			{
				client_certificate = value;
				
			}
		}
		/// <summary>
		/// 登陆用户
		/// </summary>
		public string LoginedUser
		{
			get
			{
				return client_loginuser;
			}
			set
			{
				client_loginuser = value;
			}
		}
		/// <summary>
		/// 登陆用户
		/// </summary>
		public string LoginedPassword
		{
			get
			{
				return client_password;
			}
			set
			{
				client_password = value;
			}
		}
		/// <summary>
		/// 资源代码
		/// </summary>
		public string ResourceCode
		{
			get
			{
				return client_rescode;
			}
			set
			{
				client_rescode = value;
				if(AfterLogin != null)
				{
					AfterLogin(this,new CommandEventArgs(value));
				}
			}
		}

        private string _SegmentCode = string.Empty;
        public string SegmentCode
        {
            get
            {
                return _SegmentCode;
            }
            set
            {
                _SegmentCode = value;
                if (AfterLogin != null)
                {
                    AfterLogin(this, new CommandEventArgs(value));
                }
            }
        }

        private string _StepSequenceCode = string.Empty;
        public string StepSequenceCode
        {
            get
            {
                return _StepSequenceCode;
            }
            set
            {
                _StepSequenceCode = value;
                if (AfterLogin != null)
                {
                    AfterLogin(this, new CommandEventArgs(value));
                }
            }
        }

        private string _ShiftTypeCode = string.Empty;
        public string ShiftTypeCode
        {
            get
            {
                return _ShiftTypeCode;
            }
            set
            {
                _ShiftTypeCode = value;
                if (AfterLogin != null)
                {
                    AfterLogin(this, new CommandEventArgs(value));
                }
            }
        }

		/// <summary>
		/// 接受的数据
		/// </summary>
		public string RecievedData
		{
			get
			{
				return client_recieve;
			}
			set
			{
				client_recieve = value;
			}
		}
		/// <output>类型</output>
		public DCTType Type
		{
			get
			{
				return DCTType.DSC19;
			}
		}
		/// <output>IP地址</output>
		public string ClientAddress
		{
			get
			{
				return client_ip;
			}
			set
			{
				client_ip = value;
			}
		}

		/// <output>ID</output>
		public int ClientID
		{
			get
			{
				return client_id;
			}
			set
			{
				client_id = value;
			}
		}

		/// <output>描述</output>
		public string ClientDesc
		{
			get
			{
				return client_description;
			}
			set
			{
				client_description = value;
			}
		}

		/// <output>NodeID</output>
		public int NodeID
		{
			get
			{
				return node_id;
			}
			set
			{
				node_id = value;
			}
		}

		/// <output>状态</output>
		public DSC19ClientStatus ClientStatus
		{
			get
			{
//				if(this.socket.Connected == false)
//				{
//					client_status = DSC19ClientStatus.Closed;
//				}
				return client_status;
			}
			set
			{
				client_status = value;
			}
		}

		public BaseDCTAction CachedAction
		{
			set
			{
				cachedAction = value;
			}
			get
			{
				return cachedAction ;
			}
		}

		#endregion

		public DSC19Client(string ip, int port)
		{
			try
			{
				client_status = DSC19ClientStatus.Closed;	// Added by Icyer 2006/12/04
				/*
				if(OnSendData == null)
				{
					OnSendData += new EventCommandHandler(DSC19Client_OnSendData);
				}
				*/
				client_ip = ip;
				client_port = port;

				InitialClient();
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public string DSC19Client_OnSendData(object sender,CommandEventArgs e)
		{
			return e.Message;
		}

		#region 方法

		public int Reset()
		{
			return 0;
		}

		public int ChangeStatus()
		{
			return 0;
		}

		public int ChangeIP()
		{
			return 0;
		}

		public int ChangePort()
		{
			return 0;
		}

		public void Open()
		{
			try
			{
				DSC19ClientStatus status = (DSC19ClientStatus)GW21API.AB_GW_Status(NodeID);
				
				if(status != DSC19ClientStatus.Connecting)
				{
					ClientStatus = (DSC19ClientStatus)GW21API.AB_GW_Open(NodeID);
					System.Threading.Thread.Sleep(200);
					status = (DSC19ClientStatus)GW21API.AB_GW_Status(NodeID);
				}
				ClientStatus = status;
			}
			catch(Exception ex)
			{
				if(OnError != null)
				{
					OnError(this,new CommandEventArgs(ex.Message));
				}
			}
		}
	

		public void Close()
		{
			DSC19ClientStatus status = (DSC19ClientStatus)GW21API.AB_GW_Status(NodeID);
				
			if(status == DSC19ClientStatus.Connecting)
			{
				int iReturn;
				iReturn = GW21API.AB_GW_Close(NodeID);
				if(iReturn > 0)
				{
					ClientStatus = DSC19ClientStatus.Closed;
				}
			}
		}

		public string SendCommand(DCTCommand command)
		{
			string strReturn = String.Empty;

			/*	Removed by Icyer 2006/12/04
			if(socket == null || socket.Connected == false)
			{
				Open();
			}
			*/
			// Added by Icyer 2006/12/04
			if (client_status != DSC19ClientStatus.Connecting)
			{
				Open();
			}
			// Added end

			DSC19CommandList comList = new DSC19CommandList();

			//socket.Send(comList.GetCommand(command));		// Removed by Icyer 2006/12/04
			// Added by Icyer 2006/12/04
			byte[] strCmd = comList.GetCommand(command);
			// 发送命令
			SendToClient(this.ClientID, strCmd);
			// Added end

			if(OnSendData != null)
			{
				OnSendData(this
					,new CommandEventArgs(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + OUT + command.ToString()));
			}

			return strReturn;
		}

        public string SendMessage(string msg,int lineno)
        {
            return string.Empty;
        }
		public string SendMessage(string msg)
		{
			string strReturn = String.Empty;
			// Added by Icyer 2006/12/13
			if (msg != null && msg != null)
			{
				if (msg.EndsWith("\r\n") == false)
					msg += "\r\n";
			}
			// Added end

			byte[] btOriginal = System.Text.Encoding.Default.GetBytes(msg);
			// Added by Icyer 2006/12/20 @ YHI
			// 增加语言转换
			if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHS")	// 简体中文
			{
				btOriginal = System.Text.Encoding.GetEncoding("GB2312").GetBytes(msg);
			}
			else if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHT")	// 繁体中文
			{
				btOriginal = System.Text.Encoding.GetEncoding("BIG5").GetBytes(msg);
			}
			// Added end
			//byte[] btFinal = (new DSC19MessageParser()).WrapMessage(btOriginal);
			byte[] btFinal = btOriginal;

			/* Removed by Icyer 2006/12/04
			if(socket == null || socket.Connected == false)
			{
				Open();
			}
			*/
			// Added by Icyer 2006/12/04
			if (client_status != DSC19ClientStatus.Connecting)
			{
				Open();
			}
			// Added end
			//Laws Lu,2006/06/20	清除DCT设备界面
			//socket.Send(new DSC19CommandList().GetCommand(DCTCommand.ClearText));

			//socket.Send(btFinal);		// Removed by Icyer 2006/12/04
			// Added by Icyer 2006/12/04
			// 发送命令
			SendToClient(this.ClientID, btFinal);
			// Added end

			//socket.Send(new DSC19CommandList().GetCommand(DCTCommand.ScrollDown));
			
			if(OnSendData != null)
			{
				OnSendData(this
					,new CommandEventArgs(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + OUT + msg));
			}

			btOriginal = null;
			btFinal = null;

			return strReturn;
		}

		// Added by Icyer 2006/12/18
		public string RecieveDataFromRoot(string message)
		{
			if(OnSendData != null)
			{
				OnSendData(this
					,new CommandEventArgs(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + IN + message));
			}
			return message;
		}
		// Added end
		
		public string RecieveData(ref int clientId)
		{
			string strReturn = String.Empty;

			byte[] btRecieve = new byte[512];
			//string strRecieve = String.Empty;
			/*	Removed by Icyer 2006/12/04
			if(socket == null || socket.Connected == false)
			{
				Open();
			}
			socket.Receive(btRecieve);
			*/
			// Added by Icyer 2006/12/04
			// 接收
			btRecieve = this.ReceiveFromClient(ref clientId);
			// Added end

			strReturn = System.Text.Encoding.Default.GetString(
				(new DSC19MessageParser()).RetrieveData(btRecieve));
			
			client_recieve = strReturn.ToUpper().Trim();

			if(OnSendData != null && client_recieve != string.Empty)
			{
				OnSendData(this
					,new CommandEventArgs(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + IN + strReturn));
			}
			btRecieve = null;

			return strReturn.ToUpper().Trim();

		}

		/// <summary>
		/// 初始化Client连接
		/// </summary>
		protected void InitialClient()
		{
			/*	Removed by Icyer 2006/12/04
			if(socket == null)
			{
				socket = new Socket(
					AddressFamily.InterNetwork
					,SocketType.Stream
					,ProtocolType.IP);

				socket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendTimeout,50);
			}
			*/
		}

		public override string ToString()
		{
			return this.RecievedData;
		}

		//2006/06/02 add ,loop ask for client status
		//2006/06/16 modify,confirm status use ping command
		//2006/07/01 modify,@ darfon
		public bool IsAlive()
		{
			bool bResult = true;
			try
			{
				// Removed by Icyer 2006/12/04
//				if( !socket.Connected || DSC19ClientStatus.Connecting != ClientStatus)
//				{
//					bResult = false;
//				}
//				else
//				{
////					if(socket.Poll(-1,SelectMode.SelectRead))
////					{
////						this.RecieveData();
////					}
//					if(socket.Poll(-1,SelectMode.SelectWrite)/* || socket.Poll(-1,SelectMode.SelectError) */)
//					{
//						//bResult = false;
//						this.SendCommand(DCTCommand.AutoReportingOn);
//					}
//				}
//				//this.socket.Poll(200,SelectMode.SelectWrite);
////				if(!PingClass.PingHost(client_ip) || !socket.Connected || (DSC19ClientStatus.Connecting != ClientStatus))
////				{
////					bResult = false;
////					//Laws Lu,2006/06/20 change facility status
////					ClientStatus = DSC19ClientStatus.TCPConnectError;
////				}
				bResult = (ClientStatus == DSC19ClientStatus.Connecting);
			}
			catch(Exception ex)
			{
				bResult = false;

				//Laws Lu,2006/06/20 change facility status
				ClientStatus = DSC19ClientStatus.TCPConnectError;

				Log.Error(ex.Message);

				InitialClient();
				
			}
				
			//System.Diagnostics.Debug.Assert(bResult,"OK");
			
			return bResult;
		}

		private void SendToClient(int clientId, byte[] command)
		{
			short iCmdLen = Convert.ToInt16(command.Length);
			if (System.Configuration.ConfigurationSettings.AppSettings["TestMode"] == "1")
				GW21API.AB_DCS_DspStrC(clientId, ref command, iCmdLen);
			else
				GW21API.AB_DCS_DspStrC(clientId, ref command[0], iCmdLen);
		}
		private byte[] ReceiveFromClient(ref int clientId)
		{
			short iSubCmd = -1;
			short iCmdType = -1;
			short iCmdLen = 512;
			byte[] data = new byte[iCmdLen];
			short ret = 0;
			if (System.Configuration.ConfigurationSettings.AppSettings["TestMode"] == "1")
				ret = GW21API.AB_Tag_RcvMsg(ref clientId, ref iSubCmd, ref iCmdType, ref data, ref iCmdLen);
			else
				ret = GW21API.AB_Tag_RcvMsg(ref clientId, ref iSubCmd, ref iCmdType, ref data[0], ref iCmdLen);
			if (ret <= 0)
				return new byte[0];
			if (iCmdLen > 1)
			{
				for (int i = 0; i < 2; i++)
				{
					byte ch = data[iCmdLen - 1];
					if (ch == 10 || ch == 13)
						iCmdLen--;
				}
			}
			if (iCmdLen > 0)
			{
				//this.ClientID = clientId;
			}
			string strRetMsg = System.Text.UTF8Encoding.UTF8.GetString(data, 0, iCmdLen);
			byte[] revData = System.Text.UTF8Encoding.UTF8.GetBytes(strRetMsg);
			return revData;
		}
		#endregion

        public void ClearRecievedData()
        {
            this.RecievedData = "";
        }

		public event EventCommandHandler OnSendData;

		public event EventCommandHandler OnError;

		public event EventCommandHandler AfterLogin;

		// Added by Icyer 2006/12/14
		public event EventCommandHandler OnTerminalConnect;
		public void TerminalConnect(BaseDCTDriver driver)
		{
			if (OnTerminalConnect != null)
			{
				OnTerminalConnect(driver, new CommandEventArgs(this.ClientID.ToString()));
			}
		}
		// Added end
	}

	/// <author>Laws Lu</author>
	/// <since>2006/04/12</since>
	/// <version>1.0.0</version>
	public enum DSC19ClientStatus
	{
		TCPCreateError = -1,
		ClientNotExist = -2,
		Closed = 0,
		WaitingForConnect = 6,
		Connecting = 7,
		TimeOut = 8,
		TCPConnectError = 9

	}
}
