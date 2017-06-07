using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;

using UserControl;

namespace BenQGuru.eMES.Common.DCT.Action
{
    /// <summary>
    /// ActionCollectGood 的摘要说明。
    /// </summary>
    public class ActionCollectONPost : BaseDCTAction
    {

        List<string> _userList = new List<string>();

        public ActionCollectONPost()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$Messag_Please_Input_UserCode");
            //this.LastPrompMesssage = new Message(MessageType.Normal, "$Messag_Please_Input_UserCode");
        }

        public override Messages PreAction(object act)
        {
            return Action(act);
        }

        public override Messages Action(object act)
        {
            Messages msgs = new Messages();

            if (act == null)
            {
                return msgs;
            }

            SQLDomainDataProvider domainProvider = null;

            if ((act as IDCTClient).DBConnection != null)
            {
                domainProvider = (act as IDCTClient).DBConnection as SQLDomainDataProvider;
            }
            else
            {
                domainProvider = DomainDataProviderManager.DomainDataProvider() as SQLDomainDataProvider;
                (act as IDCTClient).DBConnection = domainProvider;
            }

            PerformanceFacade performanceFacade = new PerformanceFacade(domainProvider);

            string resCode = (act as IDCTClient).ResourceCode;
            string maintainUserCode = (act as IDCTClient).LoginedUser;
            string userCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(act.ToString()));

            if (act.ToString().ToUpper() == "CONFIRM")
            {

                if (_userList.Count <= 0)
                {
                    msgs.Add(new UserControl.Message(MessageType.Error, "$Message_AtLeastOneUser"));
                }

                if (msgs.IsSuccess())
                {
                    msgs = performanceFacade.GoOnPost(resCode, _userList, maintainUserCode);

                    if (msgs.IsSuccess())
                    {
                        msgs.ClearMessages();
                        msgs.Add(new UserControl.Message(MessageType.Success, "$Message_DCTGoOnPostSuccessfully ["+_userList.Count.ToString()+"] $Message_People"));                       
                    }
                }

                base.Action(act);
                _userList.Clear();
                ProcessBeforeReturn(this.Status, msgs);
                return msgs;
            }

            bool userIsExist = false;

            for (int i = 0; i < _userList.Count; i++)
            {
                if (_userList.Contains(userCode))
                {
                    userIsExist = true;
                }
            }

            if (userIsExist)
            {
                msgs.Add(new UserControl.Message(MessageType.Error, "$Message_User_Is_Exist"));
                msgs.Add(new UserControl.Message(MessageType.Normal, "$Messag_Please_Input_UserCode"));
                msgs.Add(new UserControl.Message(MessageType.Normal, "$Message_Confirm_GoOnPost"));
                ProcessBeforeReturn(this.Status, msgs);
                return msgs;
            }

            _userList.Add(userCode);

            msgs = performanceFacade.CheckBeforeGoOnPost(resCode, _userList);

            if (!msgs.IsSuccess())
            {
                _userList.Remove(userCode);
            }

            msgs.Add(new UserControl.Message(MessageType.Normal, "$Messag_Please_Input_UserCode"));
            msgs.Add(new UserControl.Message(MessageType.Normal, "$Message_Confirm_GoOnPost"));
            ProcessBeforeReturn(this.Status, msgs);
            return msgs;
        }
    }
}
