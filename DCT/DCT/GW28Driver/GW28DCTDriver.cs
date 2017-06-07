using System;

using BenQGuru.eMES.Common.DCT.Action;
using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.GW28
{
    public class GW28DCTDriver : BaseDCTDriver, IDCTDriver
    {
        //		private GW28Client client;
        //
        //		public event EventCommandHandler AfterLogout;
        //		
        //public override BaseActionStack stack = new GW28WorkingStack();


        #region IDCTDriver 成员
        //
        //		BenQGuru.eMES.Action.IAction BenQGuru.Common.DCT.Core.IDCTDriver.MesAction
        //		{
        //			get
        //			{
        //				// TODO:  添加 GW28DCTDrive.BenQGuru.Common.DCT.Core.IDCTDriver.MesAction getter 实现
        //				return null;
        //			}
        //			set
        //			{
        //				// TODO:  添加 GW28DCTDrive.BenQGuru.Common.DCT.Core.IDCTDriver.MesAction setter 实现
        //			}
        //		}

        public override IDCTClient DCTClient
        {
            get
            {
                return client;
            }
            set
            {
                client = value as GW28Client;
            }
        }

        #endregion

        //		public void SuperCommand(string command)
        //		{
        //			if(command == "CANCEL")
        //			{
        //				stack.ResetAction();
        //			}
        //			if(command == "LOGIN")
        //			{
        //				stack.Login();
        //			}
        //			if(command == "EXIT")
        //			{
        //				if(client != null)
        //				{
        //					client.Authorized = false;
        //					stack.ResetAction();
        //					client.Close();
        //
        //					if(AfterLogout != null)
        //					{
        //						AfterLogout(this,new CommandEventArgs("OK"));
        //					}
        //				}
        //			}
        //			if(command == "CLS")
        //			{
        //				if(client != null)
        //				{
        //					client.SendCommand(GW28Command.ClearText);
        //					stack.ResetAction();
        //				}
        //			}
        //		
        //		}

        protected override void Clear()
        {
            if (client != null)
            {
                (client as GW28Client).SendCommand(DCTCommand.ClearText);
                stack.ResetAction();
            }
        }

        //login out and close connection
        protected override void Exit()
        {
            if (client != null)
            {
                client.Authorized = false;
                stack.ResetAction();
                client.CachedAction = null;

                base.RaiseAfterLogout();
            }
        }

        private bool IsInputedLoginCmd = false;
        //Recieve Data from client
        public override void CycleRequest()
        {
            if (stack.CurrentDirect == FlowDirect.WaitingInput)
            {
                string strRec = null;

                strRec = (client as GW28Client).RecieveData();

                BaseDCTAction action = null;
                if (strRec != null)
                {
                    if (strRec.IndexOf("ALL RIGHTS") < 0 && strRec != String.Empty)
                    {
                        try
                        {
                            if (IsInputedLoginCmd == false && strRec == "10")
                                return;
                            string receivedData = strRec;
                            if (receivedData == EXIT)
                            {
                                IsInputedLoginCmd = false;
                            }
                            else if (receivedData == LOGIN && IsInputedLoginCmd == true)		// 输入LOGIN，要先退出
                            {
                                receivedData = EXIT;
                                IsInputedLoginCmd = false;
                            }
                            if (receivedData == EXIT || IsInputedLoginCmd == false)
                            {
                                stack = new BaseActionStack();
                                //stack.CurrentDirect = FlowDirect.WaitingOutPut;
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

                                /* joe 20070903 GW28 输入和输出在一个界面，不需要输出原始输入
                                // 输出原始输入
                                UserControl.Messages inputMsg = new UserControl.Messages();
                                string strInput = receivedData;
                                if (action is Action.ActionPassword)
                                {
                                    strInput = new string('*', receivedData.Length);
                                }
                                inputMsg.Add(new UserControl.Message(strInput));
                                stack.SendMessage(client, inputMsg);
                                */

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
                            SuperCommand(strRec);

                            action = stack.GetNextAction();
                            stack.SendMessage(client,action.Do(client));
                            stack.CurrentDirect = action.FlowDirect;
                            */
                        }
                        catch (Exception ex)
                        {
                            UserControl.FileLog.FileLogOut("DCTControlPanel.log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.StackTrace);
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
            }
        }
        //public override 
        //Send Message to client
        public override void CycleResponse()
        {
            if (stack.CurrentDirect == FlowDirect.WaitingOutput)
            {
                BaseDCTAction action = stack.GetNextAction();
                if (action == null)
                {
                    this.Login();
                    return;
                }

                try
                {
                    stack.SendMessage(client, action.Do(client), "");

                    //stack.CurrentDirect = action.FlowDirect;
                }
                catch (Exception ex)
                {
                    UserControl.FileLog.FileLogOut("DCTControlPanel.log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.StackTrace);
                    UserControl.Messages msg = new UserControl.Messages();
                    msg.Add(new UserControl.Message(ex));
                    stack.SendMessage(client, msg, "");
                }
            }
        }

        private bool bIsInitLoginMessage = true;
        //logon mes 
        protected override void Login()
        {
            BaseDCTAction action = stack.GetNextAction();
            //			if(action == null)
            //			{
            //			action = new ActionUser();
            //			stack.Add(action);
            //			}

            if (stack.CurrentDirect == FlowDirect.WaitingOutput && action == null)
            {
                action = new ActionUser();
                if (bIsInitLoginMessage == true)
                {
                    action.OutMesssage = new UserControl.Message(UserControl.MessageType.Normal, "$DCT_LOGIN");
                    bIsInitLoginMessage = false;
                }
                stack.Add(action);


                try
                {

                    stack.SendMessage(client, action.Do(client), "");

                    //stack.CurrentDirect = action.FlowDirect;
                }
                catch (Exception ex)
                {
                    UserControl.FileLog.FileLogOut("DCTControlPanel.log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.StackTrace);
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

            while ((client as GW28Client).ClientStatus == GW28ClientStatus.Connecting)
            {
                try
                {
                    /*if(obj != null )
                    {
                        client = obj as GW28Client;
                    }
                    else */
                    //					if(client == null)
                    //					{
                    //						client = new GW28Client("10.89.58.200",55962);
                    //
                    //						client.ClientAddress = "10.89.58.200";
                    //						client.ClientID = 1;
                    //						client.ClientPort  = 55962;
                    //						client.ClientDesc  = "测试";
                    //					}

                    client.Open();

                    if (!client.Authorized)
                    {
                        Login();
                    }

                    //								else
                    //								{
                    CycleResponse();
                    //				}

                    CycleRequest();

                }
                catch (Exception ex)
                {
                    UserControl.FileLog.FileLogOut("DCTControlPanel.log", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.StackTrace);
                    Log.Error(ex.Message);
                    //break;
                }
                //				catch(Exception ex)
                //				{
                //					throw ex;
                //					
                //					//Log.Error(ex.Message);
                //					//break;
                //				}
            }
        }

        //		public override void DCTListen()
        //		{
        //			DCTListen(null);
        //		}


    }
}
