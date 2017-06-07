using System;
using System.Collections;

using BenQGuru.eMES.Common.DCT.Action;
using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.DSC19
{
    public class DSC19DCTDriver : BaseDCTDriver, IDCTDriver
    {
        //		private DT4000Client client;
        //
        //		public event EventCommandHandler AfterLogout;
        //		
        //public override BaseActionStack stack = new DT4000WorkingStack();


        #region IDCTDriver 成员
        //
        //		BenQGuru.eMES.Action.IAction BenQGuru.Common.DCT.Core.IDCTDriver.MesAction
        //		{
        //			get
        //			{
        //				// TODO:  添加 DT4000DCTDrive.BenQGuru.Common.DCT.Core.IDCTDriver.MesAction getter 实现
        //				return null;
        //			}
        //			set
        //			{
        //				// TODO:  添加 DT4000DCTDrive.BenQGuru.Common.DCT.Core.IDCTDriver.MesAction setter 实现
        //			}
        //		}

        public delegate void AsyncDelegate();
        public override IDCTClient DCTClient
        {
            get
            {
                return client;
            }
            set
            {
                client = value as DSC19Client;
            }
        }

        #endregion

        // Added by Icyer 2006/12/11
        // Driver的列表，已ClientID作为Key
        private Hashtable listDriver = new Hashtable();
        public DSC19DCTDriver GetDriver(int clientId)
        {
            DSC19DCTDriver driver = (DSC19DCTDriver)listDriver[clientId];
            if (driver == null)
            {
                driver = new DSC19DCTDriver();
                driver.IsRootDriver = false;
                DSC19Client c1 = new DSC19Client(client.ClientAddress, client.ClientPort);
                c1.ClientID = clientId;
                c1.ClientStatus = DSC19ClientStatus.Connecting;
                driver.client = c1;
                driver.ClientID = clientId;
                ((DSC19Client)client).TerminalConnect(driver);
                listDriver.Add(clientId, driver);
                // Added by Icyer 2006/12/29 @ YHI	设置切换指令时的Action
                BenQGuru.eMES.Common.DCT.Action.ActionRCard arCard = new BenQGuru.eMES.Common.DCT.Action.ActionRCard();
                arCard.Status = ActionStatus.Working;
                driver.defaultDCTAction = arCard;
                // Added end
            }
            return driver;
        }
        public bool IsRootDriver = true;	// 是否种子Driver
        public int ClientID = 0;			// 终端ID
        // Added end

        protected override void Clear()
        {
            if (client != null)
            {
                (client as DSC19Client).SendCommand(DCTCommand.ClearText);
                stack.ResetAction();
            }
        }


        // Added by Icyer 2006/12/13
        private System.Threading.Timer timer = null;
        private System.Threading.Thread threadResponse = null;
        private bool IsInputedLoginCmd = false;	// 是否已输入登录命令
        private bool bInRequestExecute = false;
        public void RequestInputFromRoot(string receivedData)
        {
            BaseDCTAction action = null;
            if (receivedData != null)
            {
                ((DSC19Client)client).RecieveDataFromRoot(receivedData);
                if (receivedData.IndexOf("ALL RIGHTS") < 0 && receivedData != String.Empty)
                {
                    client.RecievedData = receivedData;
                    try
                    {

                        if (receivedData.IndexOf("DCS-19 SELF-RESET OK") >= 0)	// 终端启动
                        {
                            receivedData = "LOGIN";
                            UserControl.Messages loginMsg = new UserControl.Messages();
                            loginMsg.Add(new UserControl.Message("$DCT_LOGIN"));
                            stack.SendMessage(client, loginMsg, "");
                            /*
                            if (timer == null)
                            {
                                timer = new System.Threading.Timer(new System.Threading.TimerCallback(this.ResponseTimerCallback), this, 0, System.Threading.Timeout.Infinite);
                            }
                            */
                            if (threadResponse == null)
                            {
                                System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(this.ResponseTimerCallback);
                                threadResponse = new System.Threading.Thread(threadStart);
                                threadResponse.Start();
                            }
                            return;
                        }
                        bInRequestExecute = true;
                        if (receivedData == EXIT)
                        {
                            IsInputedLoginCmd = false;
                        }
                        else if (receivedData == LOGIN && IsInputedLoginCmd == true)		// 输入LOGIN，要先退出
                        {
                            receivedData = EXIT;
                            IsInputedLoginCmd = false;
                        }
                        if (receivedData == EXIT)
                        {
                            this.RaiseAfterLogout();
                            /*
                            if (timer != null)
                            {
                                timer = null;
                                System.Threading.Thread.Sleep(20);
                                System.Windows.Forms.Application.DoEvents();
                            }
                            */
                            if (threadResponse != null)
                            {
                                threadResponse.Abort();
                                System.Threading.Thread.Sleep(20);
                                System.Windows.Forms.Application.DoEvents();
                                threadResponse = null;
                            }
                            stack = new BaseActionStack();
                        }

                        DealSuperCommand(receivedData);

                        if (!client.Authorized)
                        {
                            Login();
                        }

                        bool bIsLoginCmd = false;
                        if (IsInputedLoginCmd == false && !client.Authorized)	// 如果还没有输入登录命令，而且客户端没有登录
                        {
                            bIsLoginCmd = true;
                            IsInputedLoginCmd = true;
                        }
                        if (client.Authorized || bIsLoginCmd == false)	// 如果是登录命令，则不用再次执行Action，以免将命令当做用户名
                        {
                            // 获取现在的Action
                            action = stack.GetNextAction();

                            // 输出原始输入
                            UserControl.Messages inputMsg = new UserControl.Messages();
                            string strInput = receivedData;
                            if (action is Action.ActionPassword)
                            {
                                strInput = new string('*', receivedData.Length);
                            }
                            inputMsg.Add(new UserControl.Message(strInput));
                            stack.SendMessage(client, inputMsg, "");

                            // 执行Action
                            if (stack.CancelActionOutput == false)
                            {
                                stack.SendMessage(client, action.Do(client), "");
                            }
                            else
                            {
                                action.Do(client);
                                stack.CancelActionOutput = false;
                            }
                            //stack.CurrentDirect = action.FlowDirect;
                        }

                        /*
                        if (timer == null)
                        {
                            timer = new System.Threading.Timer(new System.Threading.TimerCallback(this.ResponseTimerCallback), this, 0, System.Threading.Timeout.Infinite);
                        }
                        */
                        if (threadResponse == null)
                        {
                            System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(this.ResponseTimerCallback);
                            threadResponse = new System.Threading.Thread(threadStart);
                            threadResponse.Start();
                        }
                    }
                    catch (Exception ex)
                    {
                        UserControl.Messages msg = new UserControl.Messages();
                        msg.Add(new UserControl.Message(ex));
                        stack.SendMessage(client, msg, "");
                        // Added by Icyer 2006/12/14
                        if (action != null && action.NeedCancel == true)
                        {
                            action.NeedCancel = false;
                            //this.Cancel();
                            UserControl.Messages msgTmp = new UserControl.Messages();
                            msgTmp.Add(new UserControl.Message(UserControl.MessageType.Normal, "$DCT_Please_Input_SN_OR_Directive"));
                            stack.SendMessage(client, msgTmp, "");
                        }
                        // Added end
                        if (action != null)
                        {
                            //stack.CurrentDirect = action.FlowDirect;
                        }
                    }
                    bInRequestExecute = false;
                }
            }
        }
        //private void ResponseTimerCallback(object state)
        private void ResponseTimerCallback()
        {
            while (true)
            {
                /*
                if (timer == null)
                    return;
                */
                if (threadResponse == null)
                    return;
                if (bInRequestExecute == false)
                {
                    this.CycleResponse();
                }
                System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(10);
            }
        }
        // Added end

        //Recieve Data from client
        public override void CycleRequest()
        {
            if (this.IsRootDriver == true || stack.CurrentDirect == FlowDirect.WaitingInput)
            {
                string strRec = null;

                int clientId = 0;
                strRec = (client as DSC19Client).RecieveData(ref clientId);

                BaseDCTAction action = null;
                if (strRec != null && strRec != string.Empty)
                {
                    if (this.IsRootDriver == false)		// 如果不是种子Driver，则执行Action操作
                    {
                        if (strRec.IndexOf("ALL RIGHTS") < 0 && strRec != String.Empty && strRec.IndexOf("DCS-19 SELF-RESET") < 0)
                        {
                            try
                            {
                                DealSuperCommand(strRec);

                                action = stack.GetNextAction();
                                stack.SendMessage(client, action.Do(client), "");
                                //stack.CurrentDirect = action.FlowDirect;
                            }
                            catch (Exception ex)
                            {
                                UserControl.Messages msg = new UserControl.Messages();
                                msg.Add(new UserControl.Message(ex));
                                stack.SendMessage(client, msg, "");
                                if (action != null)
                                {
                                    //stack.CurrentDirect = action.FlowDirect;
                                }
                            }
                        }
                    }
                    else		// 如果是种子Driver，则将数据发送到真实的Driver上
                    {
                        DSC19DCTDriver driver = this.GetDriver(clientId);
                        this.AsyncTransferData(driver, strRec);

                        // 继续向下读
                        if (strRec != null && strRec != string.Empty)
                        {
                            CycleRequest();
                        }
                    }
                }
            }
        }
        //public override 
        //Send Message to client
        public override void CycleResponse()
        {
            if (stack.CurrentDirect == FlowDirect.WaitingOutput)
            {
                BaseDCTAction action = stack.GetNextAction();
                // Added by Icyer 2006/12/14
                if (action == null)
                    return;
                // Added end

                try
                {
                    stack.SendMessage(client, action.Do(client), "");

                    //stack.CurrentDirect = action.FlowDirect;
                }
                catch (Exception ex)
                {
                    UserControl.Messages msg = new UserControl.Messages();
                    msg.Add(new UserControl.Message(ex));
                    stack.SendMessage(client, msg, "");
                }
            }
        }

        //logon mes 
        protected override void Login()
        {
            BaseDCTAction action = stack.GetNextAction();

            if (stack.CurrentDirect == FlowDirect.WaitingOutput)
            {
                if (action == null)
                {
                    action = new ActionUser();
                    stack.Add(action);
                }


                try
                {

                    stack.SendMessage(client, action.Do(client), "");

                    //stack.CurrentDirect = action.FlowDirect;
                }
                catch (Exception ex)
                {
                    UserControl.Messages msg = new UserControl.Messages();
                    msg.Add(new UserControl.Message(ex));
                    stack.SendMessage(client, msg, "");
                }
            }
        }

        public override void DCTListen(object obj)
        {
            try
            {
                client.Open();
            }
            catch { }

            // 种子Client操作，不断读取终端数据，然后抛到ClientID对应的Driver
            while ((client as DSC19Client).ClientStatus == DSC19ClientStatus.Connecting ||
                System.Configuration.ConfigurationSettings.AppSettings["TestMode"] == "1")
            {
                try
                {
                    client.Open();

                    /*	Removed by Icyer 2006/12/13
                    if(!client.Authorized)
                    {
                        Login();
                    }
				
                    CycleResponse();
                    */

                    CycleRequest();

                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    //break;
                }
            }

        }

        //		public override void DCTListen()
        //		{
        //			DCTListen(null);
        //		}

        private void AsyncTransferData(DSC19DCTDriver driver, string receiveData)
        {
            AsyncTransferDriver transfer = new AsyncTransferDriver();
            transfer.driver = driver;
            transfer.ReceiveData = receiveData;
            AsyncDelegate d = new AsyncDelegate(transfer.DoAction);
            System.AsyncCallback callback = new AsyncCallback(AsyncCallback);
            d.BeginInvoke(callback, d);
        }
        private void AsyncCallback(System.IAsyncResult result)
        {
            AsyncDelegate d = (AsyncDelegate)result.AsyncState;
            d.EndInvoke(null);
        }
    }

    public class AsyncTransferDriver
    {
        public DSC19DCTDriver driver = null;
        public string ReceiveData = string.Empty;
        public void DoAction()
        {
            driver.RequestInputFromRoot(ReceiveData);
        }
    }

}
