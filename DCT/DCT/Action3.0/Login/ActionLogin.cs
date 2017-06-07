using System;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using UserControl;


namespace BenQGuru.eMES.Common.DCT.Action
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionLogin : BaseDCTAction
    {
        private string _UserCode = string.Empty;
        private string _Password = string.Empty;
        private string _ResCode = string.Empty;

        private object _User = null;
        private object[] _UserGroup = null;

        public ActionLogin()
        {
            this.NeedAuthorized = false;
            this.InitMessage = null;
            this.OutMesssage = new Message(MessageType.Normal, "$DCT_PLEASE_LOGON");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$DCT_PLEASE_LOGON");
        }

        public override Messages InitAction(object act)
        {
            ((IDCTClient)act).Authorized = false;
            ((IDCTClient)act).LoginedUser = string.Empty;
            ((IDCTClient)act).LoginedPassword = string.Empty;
            ((IDCTClient)act).ResourceCode = string.Empty;
            ((IDCTClient)act).CachedAction = null;

            return base.InitAction(act);
        }

        public override Messages PreAction(object act)
        {
            Messages msgs = new Messages();

            if (act.ToString() == null)
            {
                return msgs;
            }

            SQLDomainDataProvider domainProvider = null;
            if ((act as IDCTClient).DBConnection != null)
            {
                domainProvider = (act as IDCTClient).DBConnection as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
            }
            else
            {
                domainProvider = DomainDataProviderManager.DomainDataProvider() as SQLDomainDataProvider;
                (act as IDCTClient).DBConnection = domainProvider;
            }

            string data = act.ToString().Trim().ToUpper();
            
            if (_UserCode.Trim().Length <= 0)
            {
                //输入用户名

                if (msgs.IsSuccess())
                {
                    if (data.Length <= 0)
                    {
                        msgs.Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_User_Code_Empty"));
                    }
                }

                if (msgs.IsSuccess())
                {
                    _User = (new BaseSetting.UserFacade(domainProvider)).GetUser(data);
                    if (_User == null)
                    {
                        msgs.Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_User_Not_Exist"));
                    }
                }

                if (msgs.IsSuccess())
                {
                    _UserCode = data;
                    msgs.Add(new UserControl.Message(UserControl.MessageType.Normal, "$DCT_PLEASE_INPUT_PASSWORD"));
                }
            }            
            else if (_Password.Trim().Length <= 0)
            {
                //输入密码

                if (msgs.IsSuccess())
                {
                    if (data.Length <= 0)
                    {
                        msgs.Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_Password_Empty"));
                    }
                }

                if (msgs.IsSuccess())
                {
                    try
                    {
                        _User = (new Security.SecurityFacade(domainProvider)).LoginCheck(_UserCode, data, out _UserGroup);
                    }
                    catch (Exception ex)
                    {
                        msgs.Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                    }
                }

                if (msgs.IsSuccess())
                {
                    _Password = data;
                    msgs.Add(new UserControl.Message(UserControl.MessageType.Normal, "$DCT_PLEASE_INPUT_RESOURCE"));

                    this.Status = ActionStatus.Working;
                }
            }

            ProcessBeforeReturn(this.Status, msgs);

            return msgs;
        }

        public override Messages Action(object act)
        {
            Messages msgs = new Messages();
            if (act == null)
            {
                return msgs;
            }

            BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;
            
            if ((act as IDCTClient).DBConnection != null)
            {
                domainProvider = (act as IDCTClient).DBConnection as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
            }
            else
            {
                domainProvider = DomainDataProviderManager.DomainDataProvider() as SQLDomainDataProvider;
                (act as IDCTClient).DBConnection = domainProvider;
            }

            string data = act.ToString().Trim().ToUpper();

            if (_ResCode.Trim().Length <= 0)
            {
                //输入资源

                if (msgs.IsSuccess())
                {
                    if (data.Length <= 0)
                    {
                        msgs.Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_Resource_Empty"));
                    }
                }

                if (msgs.IsSuccess())
                {
                    object resource = new BaseModelFacade(domainProvider).GetResource(data);
                    if (resource == null)
                    {
                        msgs.Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_Resource_Not_Exist"));
                    }
                }

                if (msgs.IsSuccess())
                {
                    _ResCode = data;
                }
            }

            if (msgs.IsSuccess())
            {
                bool bIsAdmin = false;
                if (_UserGroup != null)
                {
                    foreach (object o in _UserGroup)
                    {
                        if (((UserGroup)o).UserGroupType == "ADMIN")
                        {
                            bIsAdmin = true;
                            break;
                        }
                    }
                }

                if (!bIsAdmin)
                {
                    if (!(new Security.SecurityFacade(domainProvider)).CheckResourceRight(_UserCode, _ResCode))
                    {
                        msgs.Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_No_Resource_Right"));
                    }
                }
            }

            if (msgs.IsSuccess())
            {
                object res = new BaseModelFacade(domainProvider).GetResource(_ResCode);
                object org = (new BaseModelFacade(domainProvider)).GetOrg(((Resource)res).OrganizationID);
                if (org != null)
                {
                    GlobalVariables.CurrentOrganizations.Clear();
                    GlobalVariables.CurrentOrganizations.Add((Organization)org);
                }
                else
                {
                    msgs.Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_NoOrganizationOfTheResource"));
                }
            }

            if (msgs.IsSuccess())
            {
                msgs.Add(new UserControl.Message(UserControl.MessageType.Success, "$DCT_WELCOME"));

                if (act is IDCTClient)
                {
                    IDCTClient client = act as IDCTClient;
                    client.Authorized = true;
                    client.LoginedUser = _UserCode;
                    client.LoginedPassword = _Password;
                    client.ResourceCode = _ResCode;

                    Resource resource = (Resource)(new BaseModelFacade(domainProvider)).GetResource(data);
                    if (resource != null)
                    {
                        client.SegmentCode = resource.SegmentCode;
                        client.StepSequenceCode = resource.StepSequenceCode;
                        client.ShiftTypeCode = resource.ShiftTypeCode;                        
                    }
                }                
            }            

            if (msgs.IsSuccess())
            {
                this.Status = ActionStatus.Pass;
            }

            ProcessBeforeReturn(this.Status, msgs);

            return msgs;
        }

        public override Messages AftAction(object act)
        {
            Messages msgs = new Messages();
            return msgs;
        }
    }
}
