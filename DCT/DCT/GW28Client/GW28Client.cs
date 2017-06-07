using System;
using System.Net;
using System.Net.Sockets;
using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.GW28
{
	/// <summary>
	/// DCTClient 的摘要说明。
	/// </summary>
	/// <author>Laws Lu</author>
	/// <since>2006/04/12</since>
	/// <version>1.0.0</version>
	public class GW28Client : IDCTClient
	{
		private const string IN = "  IN  ";
		private const string OUT = "  OUT  ";

		private BaseDCTAction cachedAction = null;

		
		#region 变量
		//ip地址
		protected string client_ip = String.Empty;
		//端口
		protected int client_port = 55962;
		//描述
		protected string client_description = String.Empty;
		//ID
		protected int client_id;
		//状态
		protected GW28ClientStatus client_status;
		//当前接受的字符串
		protected string client_recieve;
		//资源代码
		protected string client_rescode = String.Empty;
		//登陆用户
		protected string client_loginuser = String.Empty;
		//登陆用户
		protected string client_password = String.Empty;

		//通讯用的Socket
		protected Socket socket;
		//是否登陆
		protected bool client_certificate;
		//DB数据提供对象
		protected object db_connection = null;
		#endregion

		#region 属性
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
				return DCTType.GW28;
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

		/// <output>端口</output>
		public int ClientPort
		{
			get
			{
				return client_port;
			}
			set
			{
				client_port = value;
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

		/// <output>状态</output>
		public GW28ClientStatus ClientStatus
		{
			get
			{
				if(this.socket.Connected == false)
				{
					client_status = GW28ClientStatus.Closed;
				}
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
		
		public GW28Client(string ip,int port)
		{
			try
			{
				/*
				if(OnSendData == null)
				{
					OnSendData += new EventCommandHandler(GW28Client_OnSendData);
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

		public string GW28Client_OnSendData(object sender,CommandEventArgs e)
		{
			return null;
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
        //joe 20070903 设备最后一次报告是否Live的时间
        private DateTime LastReportTime = DateTime.Now;

		public void Open()
		{
			try
			{
                LastReportTime = DateTime.Now;

				if(socket == null)
				{
					InitialClient();
				}
				if(socket.Poll(200,SelectMode.SelectError)  || socket.Connected  == false)
				{
					IPAddress ipAdd = IPAddress.Parse(client_ip);
					IPEndPoint ipEnd = new IPEndPoint(ipAdd,client_port);

					socket.Connect(ipEnd);

					this.SendCommand(DCTCommand.ClearText);

					this.client_status = GW28ClientStatus.Connecting;
					
					this.SendCommand(DCTCommand.SpeakerOff);//关掉beeper
					this.SendCommand(DCTCommand.AutoReportingOff);//关掉自动报告
                    this.SendCommand(DCTCommand.AutoReportingOn);//自动报告是否live
				}
				

				//this.SendCommand(DCTCommand.SpeakerOn);
				//System.Threading.Thread.Sleep(50);
				//this.SendCommand(DCTCommand.SpeakerOff);
				
			}
			catch(Exception ex)
			{
				if(OnError != null)
				{
					OnError(this,new CommandEventArgs(ex.Message));
				}
				socket = null;
				InitialClient();
				//throw ex;
			}
		}
	

		public void Close()
		{
			if(socket != null)
			{
				socket.Shutdown(SocketShutdown.Both);
				socket.Close();
			}
		}

		public string SendCommand(DCTCommand command)
		{
			string strReturn = String.Empty;

			if(socket == null || socket.Connected == false)
			{
				Open();
			}

			GW28CommandList comList =new GW28CommandList();

			socket.Send(comList.GetCommand(command));

			/*
			if(OnSendData != null)
			{
				OnSendData(this
					,new CommandEventArgs(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + OUT + command.ToString()));
			}
			*/

			return strReturn;
		}

        public string SendMessage(string msg, int lineno)
        {
            return string.Empty;
        }

		public string SendMessage(string msg)
		{
			string strReturn = String.Empty;

			byte[] btOriginal = System.Text.Encoding.Default.GetBytes(msg);
            // Added by Joe 2007/9/2 @ LM
            // 增加语言转换
            if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHS")	// 简体中文
            {
                btOriginal = System.Text.Encoding.GetEncoding("GB2312").GetBytes(msg);
            }
            else if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHT")	// 繁体中文
            {
                btOriginal = System.Text.Encoding.GetEncoding("BIG5").GetBytes(msg);
            }
            //end Joe
			byte[] btFinal = (new GW28MessageParser()).WrapMessage(btOriginal);

			if(socket == null || socket.Connected == false)
			{
				Open();
			}
			socket.Send(btFinal);
			
			if(OnSendData != null)
			{
				OnSendData(this
					,new CommandEventArgs(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + OUT + msg));
			}

			btOriginal = null;
			btFinal = null;

			return strReturn;
		}

		public string RecieveData()
		{
			string strReturn = String.Empty;

			byte[] btRecieve = new byte[512];
			//string strRecieve = String.Empty;
			if(socket == null || socket.Connected == false)
			{
				Open();
			}
			socket.Receive(btRecieve);

            if (btRecieve[0] == (byte)27)//joe 20070903 如果读出的数据是以 27开关的，则说明数据是auto report发来的
            {
                this.LastReportTime = DateTime.Now;
                return string.Empty;
            }
			strReturn = System.Text.Encoding.Default.GetString(
				(new GW28MessageParser()).RetrieveData(btRecieve));
			
			client_recieve = strReturn.ToUpper().Trim();

			if(OnSendData != null)
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
			if(socket == null)
			{
				socket = new Socket(
					AddressFamily.InterNetwork
					,SocketType.Stream
					,ProtocolType.IP);

				socket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendTimeout,500);
			}
		}

		public override string ToString()
		{
			return this.RecievedData;
		}
		
		//2006/06/02 add ,loop ask for client status
		//2006/06/16 modify,confirm status use ping command
		private bool _alive = true;
		public bool IsAlive()
		{
            _alive = true;
      
            if ((DateTime.Now - LastReportTime).Seconds > CommandEventArgs.DeadInterval)
            {
                _alive = false;
            }
            else 
            {
                _alive = true;
            }
        
            return _alive;
        }
		#endregion

        public void ClearRecievedData()
        {
            this.RecievedData = "";
        }

		public event EventCommandHandler OnSendData;

		public event EventCommandHandler OnError;

		public event EventCommandHandler AfterLogin;
	}
	
	
	/// <author>Laws Lu</author>
	/// <since>2006/04/12</since>
	/// <version>1.0.0</version>
	public enum GW28ClientStatus
	{
		TCPCreateError = -1,
		GWNotExist = -2,
		Closed = 0,
		WaitingForConnect = 6,
		Connecting = 7,
		TimeOut = 8,
		TCPConnectError = 9

	}
}
