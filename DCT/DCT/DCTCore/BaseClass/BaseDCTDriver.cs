using System;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Common.DCT.Core
{
    /// <summary>
    /// BaseDCTDriver 的摘要说明。
    /// </summary>
    public abstract class BaseDCTDriver
    {
        public const string CANCEL = "CANCEL";
        public const string LOGIN = "LOGIN";
        public const string EXIT = "EXIT";
        public const string CLS = "CLS";
        public const string FINERROR = "FINERROR";
        public const string KBATCH = "KBATCH";
        public const string MIX = "MIX";
        public const string COMPAPP = "COMPAPP";
        public const string NEXTOP = "NEXTOP";		// Added by Icyer 2006/12/31	查询序列号应处OP
        public const string GOWORK = "GOWORK";     //JOE 20070903 员工上岗指令
        public const string CLEARWORK = "CLEARWORK";     //JOE 20070903 全部
        public const string OK = "OK";
        public const string DELOPERATOR = "DELOPERATOR";
        public const string OQACHECK = "OQACHECK";
        public const string OQAATEPASS = "OQAATEPASS";
        public const string LOCK = "LOCK";
        public const string COMPPROCODE = "COMPPROCODE";
        public const string COMPTWO = "COMPTWO";
        public const string SMTNG = "SMTNG";
        public const string AUTONG = "AUTONG";
        public const string FGPACKING = "FGPACKING";
        public const string MACID = "MACID";
        public const string MACALL = "MACALL";
        public const string ONPOST = "ONPOST";

        public BaseDCTDriver()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        protected IDCTClient client;

        public event EventCommandHandler AfterLogout;

        public BaseActionStack stack = new BaseActionStack();

        public BaseDCTAction defaultDCTAction = null;	// Added by Icyer 2006/12/29 @ YHI	切换指令时执行的Action

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

        public virtual IDCTClient DCTClient
        {
            get
            {
                return client;
            }
            set
            {
                client = value/* as GW28Client*/;
            }
        }

        #endregion

        public virtual bool DealSuperCommand(string command)
        {
            bool deal = false;

            if (command == CANCEL)
            {
                Cancel();
                deal = true;
            }
            if (command == LOGIN)
            {
                Login();
                deal = true;
            }
            if (command == EXIT)
            {
                Exit();
                deal = true;
            }
            if (command == CLS)
            {
                Clear();
                deal = true;
            }

            return deal;
        }

        //Cancel current action
        protected virtual void Cancel()
        {
            stack.ResetAction();
        }
        //login out and close connection
        protected virtual void Exit()
        {
            if (client != null)
            {
                client.Authorized = false;
                stack.ResetAction();
                client.Close();
                client.CachedAction = null;

                if (AfterLogout != null)
                {
                    AfterLogout(this, new CommandEventArgs("OK"));
                }
            }
        }
        //Clear Screen
        protected virtual void Clear()
        {
            //sub class implement
        }

        protected virtual void Login()
        {
        }

        //Recieve Data from client
        public virtual void CycleRequest()
        {
            //sub class implement
        }
        //Handle Message
        protected virtual void HandleMessage(string strRec)
        {
            //sub class implement
        }
        //Send Message to client
        public virtual void CycleResponse()
        {
            if (stack.CurrentDirect == FlowDirect.WaitingOutput)
            {
                BaseDCTAction action = stack.GetNextAction();

                try
                {
                    stack.SendMessage(client, action.Do(client), string.Empty);
                }
                catch (Exception ex)
                {
                    UserControl.Messages msg = new UserControl.Messages();
                    msg.Add(new UserControl.Message(ex));
                    stack.SendMessage(client, msg, string.Empty);
                }
            }
        }

        public virtual void DCTListen(object obj)
        {
        }

        public virtual void DCTListen()
        {
            DCTListen(null);
        }

        public void RaiseAfterLogout()
        {
            if (AfterLogout != null)
            {
                AfterLogout(this, new CommandEventArgs("OK"));
            }
        }
    }
}
