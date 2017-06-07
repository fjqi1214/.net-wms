using System;
using System.Collections;
using System.Text;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Common.DCT.Core
{
    /// <summary>
    /// BaseActionStack 的摘要说明。
    /// </summary>
    public class BaseActionStack
    {
        private ArrayList _WorkingActionList = new ArrayList();

        private static int BeepInterval = 1500;
        private static int BeepCount = 3;
        private static DCTCommand BeepType;
        private static bool BeepSuccess = true;
        private static bool BeepError = true;

        static BaseActionStack()
        {
            #region 控制DCT 发声音的
            try
            {
                BeepSuccess = bool.Parse(System.Configuration.ConfigurationSettings.AppSettings["BeepSuccess"]);


            }
            catch
            {
                BeepSuccess = true;

            }
            try
            {
                BeepError = bool.Parse(System.Configuration.ConfigurationSettings.AppSettings["BeepError"]);
            }
            catch
            {
                BeepError = true;

            }
            try
            {
                BeepInterval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["BeepInterval"]);

            }
            catch
            {
                BeepInterval = 1500;

            }

            try
            {
                BeepCount = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["BeepCount"]);
            }
            catch
            {
                BeepCount = 3;
            }

            try
            {
                if (System.Configuration.ConfigurationSettings.AppSettings["BeepType"] == "short")
                {
                    BeepType = DCTCommand.SpeakerOn;
                }
                else
                {
                    BeepType = DCTCommand.SpeakerOff;
                }
            }
            catch
            {
                BeepType = DCTCommand.SpeakerOff;
            }
            #endregion

            if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHS")	// 简体中文
            {
                MutiLanguages.Language = "CHS";
            }
            else if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHT")	// 繁体中文
            {
                MutiLanguages.Language = "CHT";
            }
            else if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "ENU")	// 英文
            {
                MutiLanguages.Language = "ENU";
            }
        }

        public BaseActionStack()
        {
            if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHS")	// 简体中文
            {
                MutiLanguages.Language = "CHS";
            }
            else if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "CHT")	// 繁体中文
            {
                MutiLanguages.Language = "CHT";
            }
            else if (System.Configuration.ConfigurationSettings.AppSettings["Language"] == "ENU")	// 英文
            {
                MutiLanguages.Language = "ENU";
            }
        }

        public ArrayList WorkingActionList
        {
            get
            {
                return _WorkingActionList;
            }
            set
            {
                _WorkingActionList = value;
            }
        }

        public BaseDCTAction CachedAction = null;
        public bool CancelActionOutput = false;

        public virtual void Add(BaseDCTAction action)
        {
            if (_WorkingActionList.Count < 1)
            {
                this._WorkingActionList.Add(action);
            }
        }

        public virtual void Login()
        {
        }

        public virtual void ResetAction()
        {
            _WorkingActionList.Clear();
        }

        public virtual void ResetActionStack(BaseDCTAction action)
        {
            _WorkingActionList.Clear();
            _WorkingActionList.Add(action);
        }

        public virtual BaseDCTAction GetNextAction()
        {
            BaseDCTAction action = null;

            if (_WorkingActionList.Count > 0)
            {
                action = (BaseDCTAction)_WorkingActionList[0];

                while (action != null && action.Status == ActionStatus.Pass)
                {
                    _WorkingActionList.Clear();
                    _WorkingActionList.Add(action.NextAction);
                    action = null;
                    action = (BaseDCTAction)_WorkingActionList[0];
                }
            }

            return action;
        }

        public virtual FlowDirect CurrentDirect
        {
            get
            {
                if (_WorkingActionList.Count > 0)
                {
                    return ((BaseDCTAction)_WorkingActionList[0]).FlowDirect;
                }

                return FlowDirect.WaitingOutput;
            }
            //set 
            //{
            //    this._CurrentDirect = value;            
            //}
        }

        private string empty = "".PadLeft(90, ' ');
        //DCT每行的英文字符数
        //private int DCTEnLineLen = 30;

        private void SendMessageEx(IDCTClient client, string send_msg)
        {
            client.SendMessage(send_msg);
        }

        public virtual void SendMessage(object sender, Messages msgs, string command)
        {
            IDCTClient client = sender as IDCTClient;

            Message msg = new Message();
            for (int i = 0; i < msgs.Count(); i++)
            {
                if (msgs.Objects(i).Type != MessageType.Data)
                {
                    msg = msgs.Objects(i);

                    //处理一般的输出信息
                    if (msg.Body != String.Empty)
                    {
                        string send_msg = MutiLanguages.ParserMessage(msg.Body);

                        if (send_msg.IndexOf("$") < 0)
                        {
                            if (msg.Type == MessageType.DCTClear)
                            {
                                client.SendMessage(empty);
                            }
                            if (msg.Type == MessageType.DCTData)
                            {
                                SendMessageEx(client, send_msg);
                            }
                            else
                            {
                                SendMessageEx(client, send_msg);
                            }
                        }
                        else	// 将缺失的Message记录下来
                        {
                            try
                            {
                                string strFile = @"C:\DCT_EmptyMessage.txt";
                                System.IO.StreamWriter writer = new System.IO.StreamWriter(strFile, true);
                                writer.WriteLine(msg.Body);
                                writer.Close();
                            }
                            catch { }
                        }

                        if (msg.Type == MessageType.Normal && msg.Body.Trim().Length > 0)
                        {
                            this._LastPrompt = send_msg.Trim();
                        }
                    }

                    //处理异常
                    if (msg.Exception != null)
                    {
                        string send_msg = MutiLanguages.ParserMessage(msg.Exception.Message);
                        if (send_msg.IndexOf("$") < 0)
                        {
                            if (msg.Type == MessageType.DCTClear)
                            {
                                client.SendMessage(empty);
                            }

                            if (msg.Type == MessageType.DCTData)
                            {
                                SendMessageEx(client, send_msg);
                            }
                            else
                            {
                                SendMessageEx(client, send_msg);
                            }
                        }
                    }

                    //发出声音
                    if (msg != null)
                    {
                        switch (msg.Type)
                        {
                            case MessageType.Success:
                                {
                                    //if(CurrentDirect == FlowDirect.WaitingOutPut && BeepSuccess)
                                    if (BeepSuccess)
                                        client.SendCommand(DCTCommand.SpeakerOn);

                                    break;
                                }
                            case MessageType.Error:
                                {
                                    if (BeepError)
                                    {
                                        for (int j = 0; j < BeepCount; j++)
                                        {
                                            client.SendCommand(BeepType);
                                            System.Threading.Thread.Sleep(BeepInterval);
                                        }
                                    }
                                    break;
                                }
                            default:
                                {
                                    if (BeepSuccess)
                                        client.SendCommand(DCTCommand.SpeakerOn);

                                    break;
                                }
                        }
                    }

                    //记录WorkingError
                    if (msg.Type == MessageType.Error || msg.Exception != null)
                    {
                        try
                        {
                            SQLDomainDataProvider domainProvider = null;
                            if (client.DBConnection != null)
                            {
                                domainProvider = client.DBConnection as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
                            }
                            else
                            {
                                domainProvider = DomainDataProviderManager.DomainDataProvider() as SQLDomainDataProvider;
                                client.DBConnection = domainProvider;
                            }

                            DataCollectFacade dataCollectFacade = new DataCollectFacade(domainProvider);

                            string userCode = client.LoginedUser;
                            string resCode = client.ResourceCode;
                            string segCode = client.SegmentCode;
                            string ssCode = client.StepSequenceCode;
                            string shiftTypeCode = client.ShiftTypeCode;

                            string errorMessageCode = string.Empty;
                            string errorMessage = string.Empty;
                            if (msg.Type == MessageType.Error)
                            {
                                errorMessageCode = msg.Body;
                                errorMessage = MutiLanguages.ParserMessage(msg.Body);                                                                
                            }

                            if (msg.Exception != null)
                            {
                                errorMessageCode = msg.Exception.Message;
                                errorMessage = MutiLanguages.ParserMessage(msg.Exception.Message);                                   
                            }

                            dataCollectFacade.LogWorkingError(userCode, resCode, segCode, ssCode, shiftTypeCode,
                                WorkingErrorFunctionType.DCT, command, this._LastPrompt + ": " + client.ToString(), errorMessageCode, errorMessage);
                        }
                        catch(Exception ex)
                        {
                            //throw ex;
                        }
                    }
                }
            }
        }

        public int CurrentIndex = 0;

        private string _LastPrompt = string.Empty;
    }
}
