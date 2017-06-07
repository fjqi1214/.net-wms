using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.DT4000
{
    /// <summary>
    /// DT4000Client 的摘要说明。
    /// </summary>
    public class DT4000Client : IDCTClient
    {
        private const string IN = "  IN  ";
        private const string OUT = "  OUT  ";

        private BaseDCTAction cachedAction = null;

        #region 变量

        //ip地址
        protected string _ClientAddress = String.Empty;
        //端口
        protected int _ClientPort = 55962;
        //描述
        protected string _ClientDesc = String.Empty;
        //ID
        protected int _ClientID;
        //状态
        protected DT4000ClientStatus _ClientStatus;
        //当前接受的字符串
        protected string _RecievedData;
        //资源代码
        protected string _ResourceCode = String.Empty;
        protected string _SegmentCode = String.Empty;
        protected string _StepSequenceCode = String.Empty;
        protected string _ShiftTypeCode = String.Empty;
        //登陆用户
        protected string _LoginUser = String.Empty;
        //登陆用户
        protected string _Password = String.Empty;
        //通讯用的Socket
        protected Socket socket;
        //是否登陆
        protected bool _Authorized;
        //DB数据提供对象
        protected object db_connection = null;

        //设备最后一次报告是否Live的时间
        private DateTime _LastReportTime = DateTime.Now;
        //是否活动的
        private bool _IsAlive = true;

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
                return _Authorized;
            }
            set
            {
                _Authorized = value;

            }
        }
        /// <summary>
        /// 登陆用户
        /// </summary>
        public string LoginedUser
        {
            get
            {
                //返回当前上岗的人员列表,如果人员列表是空的,
                //则返回当前登录的人员
                //BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()
                //    as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;

                string operlist = string.Empty;
                //BenQGuru.eMES.HourCounting.HourCountingFacade facade = new BenQGuru.eMES.HourCounting.HourCountingFacade(domainProvider);
                //object[] objs = facade.GetHourCountingOperaterWorkOnByResource(this.ResourceCode);
                //if (objs != null && objs.Length > 0)
                //{
                //    foreach (BenQGuru.eMES.Domain.HourCounting.HourCountingOperaterWorkOn wo in objs)
                //    {
                //        operlist = operlist + wo.OperaterNo + ",";
                //    }

                //    if (operlist.Length > 0)
                //    {
                //        operlist = operlist.Substring(0, operlist.Length - 1);
                //    }
                //}

                if (operlist != string.Empty)
                    return operlist;
                else
                    return _LoginUser;
            }
            set
            {
                _LoginUser = value;
            }
        }
        /// <summary>
        /// 登陆用户
        /// </summary>
        public string LoginedPassword
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
        /// <summary>
        /// 资源代码
        /// </summary>
        public string ResourceCode
        {
            get
            {
                return _ResourceCode;
            }
            set
            {
                _ResourceCode = value;
                if (AfterLogin != null)
                {
                    AfterLogin(this, new CommandEventArgs(value));
                }
            }
        }
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
                return _RecievedData;
            }
            set
            {
                _RecievedData = value;
            }
        }
        /// <output>类型</output>
        public DCTType Type
        {
            get
            {
                return DCTType.DT4000;
            }
        }
        /// <output>IP地址</output>
        public string ClientAddress
        {
            get
            {
                return _ClientAddress;
            }
            set
            {
                _ClientAddress = value;
            }
        }

        /// <output>端口</output>
        public int ClientPort
        {
            get
            {
                return _ClientPort;
            }
            set
            {
                _ClientPort = value;
            }
        }

        /// <output>ID</output>
        public int ClientID
        {
            get
            {
                return _ClientID;
            }
            set
            {
                _ClientID = value;
            }
        }

        /// <output>描述</output>
        public string ClientDesc
        {
            get
            {
                return _ClientDesc;
            }
            set
            {
                _ClientDesc = value;
            }
        }

        /// <output>状态</output>
        public DT4000ClientStatus ClientStatus
        {
            get
            {
                if (this.socket.Connected == false)
                {
                    _ClientStatus = DT4000ClientStatus.Closed;
                }
                return _ClientStatus;
            }
            set
            {
                _ClientStatus = value;
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
                return cachedAction;
            }
        }
        #endregion

        public DT4000Client(string ip, int port)
        {
            try
            {
                /*
                if(OnSendData == null)
                {
                    OnSendData += new EventCommandHandler(DT4000Client_OnSendData);
                }
                */
                _ClientAddress = ip;
                _ClientPort = port;

                InitialClient();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DT4000Client_OnSendData(object sender, CommandEventArgs e)
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

        public void Open()
        {
            try
            {
                _LastReportTime = DateTime.Now;

                if (socket == null)
                {
                    InitialClient();
                }

                if (socket.Connected == false || socket.Poll(200, SelectMode.SelectError))
                {
                    IPAddress ipAdd = IPAddress.Parse(_ClientAddress);
                    IPEndPoint ipEnd = new IPEndPoint(ipAdd, _ClientPort);

                    socket.Connect(ipEnd);

                    this.SendCommand(DCTCommand.ClearText);

                    this._ClientStatus = DT4000ClientStatus.Connecting;

                    this.SendCommand(DCTCommand.SpeakerOff);//关掉beeper
                    this.SendCommand(DCTCommand.AutoReportingOff);//关掉自动报告
                    this.SendCommand(DCTCommand.AutoReportingOn);//自动报告是否live
                    this.SendCommand(DCTCommand.HostReportSetting);
                    this.SendCommand(DCTCommand.ClearText);
                    this.SendCommand(DCTCommand.ClearGraphic);
                }
            }
            catch (Exception ex)
            {
                if (OnError != null)
                {
                    OnError(this, new CommandEventArgs(ex.Message));
                }
                socket = null;
                InitialClient();
                //throw ex;
            }
        }


        public void Close()
        {
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            //Add by vivan.sun 2011-6-3
            if (this.DBConnection != null)
            {
                BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = this.DBConnection as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
                domainProvider.PersistBroker.CloseConnection();
            }
            //end add
        }

        public string SendCommand(DCTCommand command)
        {
            string strReturn = String.Empty;

            if (socket == null || socket.Connected == false)
            {
                Open();
            }

            DT4000CommandList comList = new DT4000CommandList();

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

        public string SendMessage(string msg, int line)
        {
            string strReturn = String.Empty;

            //btSetCursor
            byte[] btSetCursor = new byte[DT4000MessageParser.MessageSetCursor1.Length];
            DT4000MessageParser.MessageSetCursor1.CopyTo(btSetCursor, 0);
            if (line < 1)
                btSetCursor[11] = 1;
            else if (line > 4)
                btSetCursor[11] = 4;
            else
                btSetCursor[11] = (byte)line;

            //btBackToOutput          
            byte[] btBackToOutput = new byte[DT4000MessageParser.MessagePrefix.Length];
            DT4000MessageParser.MessagePrefix.CopyTo(btBackToOutput, 0);

            //btOriginal
            byte[] btOriginal = System.Text.Encoding.Default.GetBytes(msg);
            if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHS")	// 简体中文
            {
                btOriginal = System.Text.Encoding.GetEncoding("GB2312").GetBytes(msg);
            }
            else if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHT")	// 繁体中文
            {
                btOriginal = System.Text.Encoding.GetEncoding("BIG5").GetBytes(msg);
            }

            byte[] btOriginal2 = new byte[_DCTEnLineLen - 1];

            for (int i = 0; i < _DCTEnLineLen - 1; i++)
            {
                if (i < btOriginal.Length)
                    btOriginal2[i] = btOriginal[i];
                else
                    btOriginal2[i] = 32;
            }

            byte[] btFinal = (new DT4000MessageParser()).WrapMessageNew(btOriginal2);


            //Open
            if (socket == null || socket.Connected == false)
            {
                Open();
            }

            //返回到输出界面
            socket.Send(btBackToOutput);

            //设置光标位置
            socket.Send(btSetCursor);

            //输出信息
            //socket.Send(DT4000MessageParser.MessageClearText);
            //socket.Send(btSetCursor);
            socket.Send(btFinal);

            //重置光标位置到第一行
            socket.Send(new byte[] { 0x0c, 0x00, 0x40, 0x00, 0x00, 0x00, 0x65, 0x00, 0x00, 0x01, 0x00, 0x01 });

            //清空用户输入界面
            socket.Send(new DT4000CommandList().GetCommand(DCTCommand.ClearUserInput));

            if (OnSendData != null)
            {
                OnSendData(this
                    , new CommandEventArgs(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + OUT + msg));
            }

            btOriginal = null;
            btFinal = null;

            return strReturn;
        }

        int _DCTEnLineLen = 30;
        bool _Cut = false;
        byte[] _OldMessageByteArray = null;

        public string SendMessage(string msg)
		{
			string strReturn = String.Empty;

            if (msg == null || msg.Trim().Length <= 0)
                return strReturn;

            //获取编码方式
            System.Text.Encoding currEncoding = System.Text.Encoding.Default;
            if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHS")	// 简体中文
            {
                currEncoding = System.Text.Encoding.GetEncoding("GB2312");
            }
            else if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHT")	// 繁体中文
            {
                currEncoding = System.Text.Encoding.GetEncoding("BIG5");
            }

            //输入信息分行
            string sendTemp = string.Empty;
            ArrayList msgList = new ArrayList();

            while (currEncoding.GetByteCount(msg) > _DCTEnLineLen)
            {
                for (int i = 1; i < currEncoding.GetByteCount(msg); i++)
                {
                    if (currEncoding.GetByteCount(msg.Substring(0, i)) > _DCTEnLineLen)
                    {
                        sendTemp = msg.Substring(0, i - 1);
                        msg = msg.Substring(i - 1);
                        break;
                    }
                }

                if (!_Cut)
                    msgList.Add(sendTemp);
            }

            //处理最后一行
            if (msg.Length > 0)
            {
                if (currEncoding.GetByteCount(msg) < _DCTEnLineLen)
                {
                    msgList.Add(msg);
                }
                else
                {
                    for (int i = 1; i < currEncoding.GetByteCount(msg); i++)
                    {
                        if (currEncoding.GetByteCount(msg.Substring(0, i)) >= _DCTEnLineLen)
                        {
                            sendTemp = msg.Substring(0, i - 1);
                            msg = msg.Substring(i - 1);
                            break;
                        }
                    }
                    if (!_Cut)
                        msgList.Add(sendTemp);

                    if (msg.Length > 0)
                        msgList.Add(msg);
                }
            }

            //每行末尾补充空格
            ArrayList lineByteList = new ArrayList();
            int byteCount = 0;
            for (int i = 0; i < msgList.Count; i++)
            {
                byte[] btOriginal = new byte[_DCTEnLineLen];
                for (int j = 0; j < btOriginal.Length; j++)
                    btOriginal[j] = 32;

                currEncoding.GetBytes((string)msgList[i]).CopyTo(btOriginal, 0);

                lineByteList.Add(btOriginal);
                byteCount += btOriginal.Length;
            }

            //产生屏幕显示需要的字节数组
            if (_OldMessageByteArray == null)
            {
                _OldMessageByteArray = new byte[3 * _DCTEnLineLen];
                for (int i = 0; i < _OldMessageByteArray.Length; i++)
                    _OldMessageByteArray[i] = 32;
            }

            byte[] newMessageByteArray = new byte[_OldMessageByteArray.Length + byteCount];
            for (int i = 0; i < newMessageByteArray.Length; i++)
                newMessageByteArray[i] = 32;
            _OldMessageByteArray.CopyTo(newMessageByteArray, 0);

            byteCount = 0;
            for (int i = 0; i < lineByteList.Count; i++)
            {
                ((byte[])lineByteList[i]).CopyTo(newMessageByteArray, _OldMessageByteArray.Length + byteCount);
                byteCount += ((byte[])lineByteList[i]).Length;
            }

            //更新记录显示信息的字节数组
            if (newMessageByteArray.Length <= 3 * _DCTEnLineLen)
                _OldMessageByteArray = new byte[newMessageByteArray.Length];
            else
                _OldMessageByteArray = new byte[3 * _DCTEnLineLen];

            for (int i = 0; i < _OldMessageByteArray.Length; i++)
            {
                _OldMessageByteArray[i] = newMessageByteArray[newMessageByteArray.Length - _OldMessageByteArray.Length + i];
            }

            //获得发送给DCT的命令
            byte[] btFinal = (new DT4000MessageParser()).WrapMessageNew(_OldMessageByteArray);


            //显示
            if (socket == null || socket.Connected == false)
            {
                Open();
            }
            socket.Send(DT4000MessageParser.MessageClearText);
            socket.Send(DT4000MessageParser.MessageSetCursor2);
            socket.Send(btFinal);
            socket.Send(DT4000MessageParser.MessageSetCursor1);
            socket.Send(new DT4000CommandList().GetCommand(DCTCommand.ClearUserInput));

            if (OnSendData != null)
            {
                OnSendData(this
                    , new CommandEventArgs(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + OUT + msg));
            }

            btFinal = null;

            return strReturn;
		}

        public string RecieveData()
        {
            string strReturn = String.Empty;

            byte[] btRecieve = new byte[512];
            //string strRecieve = String.Empty;
            if (socket == null || socket.Connected == false)
            {
                Open();
            }
            socket.Receive(btRecieve);

            if (btRecieve.Length >= 7)
            {
                //判断是不是自动report指令
                if (btRecieve[0] == (byte)0x8
                    &&
                    btRecieve[1] == (byte)0x0
                    &&
                    btRecieve[2] == (byte)0x40
                    &&
                    btRecieve[3] == (byte)0x0
                    &&
                    btRecieve[4] == (byte)0x0
                    &&
                    btRecieve[5] == (byte)0x0
                    &&
                    btRecieve[6] == (byte)0x11
                    )
                {
                    this._LastReportTime = DateTime.Now;
                    this.SendCommand(DCTCommand.HostReportPackage);
                    return string.Empty;
                }
            }
            strReturn = System.Text.Encoding.Default.GetString(
                (new DT4000MessageParser()).RetrieveData(btRecieve));

            _RecievedData = strReturn.ToUpper().Trim();

            if (OnSendData != null)
            {
                OnSendData(this
                    , new CommandEventArgs(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + IN + strReturn));
            }
            btRecieve = null;

            return strReturn.ToUpper().Trim();

        }

        /// <summary>
        /// 初始化Client连接
        /// </summary>
        protected void InitialClient()
        {
            if (socket == null)
            {
                socket = new Socket(
                    AddressFamily.InterNetwork
                    , SocketType.Stream
                    , ProtocolType.IP);

                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 500);
            }
        }

        public override string ToString()
        {
            return this.RecievedData;
        }

        public bool IsAlive()
        {
            _IsAlive = true;

            if (_LastReportTime.AddSeconds(CommandEventArgs.DeadInterval) < DateTime.Now)
            {
                _IsAlive = false;
            }
            else
            {
                _IsAlive = true;
            }

            return _IsAlive;
        }

        public void ClearRecievedData()
        {
            this.RecievedData = "";
        }

        public void ClearScreen()
        {
            this.SendCommand(DCTCommand.ClearText);
            this.SendCommand(DCTCommand.ClearUserInput);
            this._OldMessageByteArray = null;
        }

        #endregion

        public event EventCommandHandler OnSendData;

        public event EventCommandHandler OnError;

        public event EventCommandHandler AfterLogin;

    }

    /// <author>Laws Lu</author>
    /// <since>2006/04/12</since>
    /// <version>1.0.0</version>
    public enum DT4000ClientStatus
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
