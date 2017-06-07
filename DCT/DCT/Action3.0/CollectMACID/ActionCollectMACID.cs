using System;

using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;

using UserControl;

namespace BenQGuru.eMES.Common.DCT.Action
{
    /// <summary>
    /// ActionCollectGood 的摘要说明。
    /// </summary>
    public class ActionCollectMACID : BaseDCTAction
    {
        private string _rcard = string.Empty;

        public ActionCollectMACID()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$DCT_GOOD_Please_Input_GOOD_SN");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$DCT_GOOD_Please_Input_GOOD_SN");
        }



        public override Messages PreAction(object act)
        {
            Messages msgs = new Messages();

            if (_rcard == string.Empty)
            {
                Messages msg = CheckRcard(act);
                if (msg.IsSuccess() == false)
                {
                    ProcessBeforeReturn(this.Status, msg);
                    return msg;
                }
                _rcard = act.ToString().Trim().ToUpper();
            }

            base.PreAction(act);

            this.OutMesssage = new Message(MessageType.Normal, "$CS_Please_Input_MACID [" + _rcard + "]");

            msgs.Add(this.OutMesssage);

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

            DataCollect.Action.ActionEventArgs args;
            if (ObjectState == null)
            {
                args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
                args.RunningCard = act.ToString().ToUpper().Trim();
            }
            else
            {
                args = ObjectState as DataCollect.Action.ActionEventArgs;
            }

            if (msgs.IsSuccess())
            {
                ActionOnLineHelper _helper = new ActionOnLineHelper(domainProvider);

                msgs = _helper.GetIDInfo(args.RunningCard);

                if (msgs.IsSuccess())
                {
                    ProductInfo product = (ProductInfo)msgs.GetData().Values[0];

                    IAction dataCollectModule = new BenQGuru.eMES.DataCollect.Action.ActionFactory(domainProvider).CreateAction(ActionType.DataCollectAction_GOOD);

                    domainProvider.BeginTransaction();

                    try
                    {
                        IDCTClient client = act as IDCTClient;

                        //良品采集
                        if (msgs.IsSuccess())
                        {
                            msgs.AddMessages(((IActionWithStatus)dataCollectModule).Execute(
                                new ActionEventArgs(
                                ActionType.DataCollectAction_GOOD,
                                args.RunningCard,
                                client.LoginedUser,
                                client.ResourceCode,
                                product)));
                        }


                        if (msgs.IsSuccess())
                        {
                            domainProvider.CommitTransaction();
                            msgs.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_GOODSUCCESS")));
                        }
                        else
                        {
                            domainProvider.RollbackTransaction();
                        }
                    }
                    catch (Exception ex)
                    {
                        domainProvider.RollbackTransaction();
                        msgs.Add(new UserControl.Message(ex));
                    }
                    finally
                    {
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)domainProvider).PersistBroker.CloseConnection();
                    }
                }
            }

            if (msgs.IsSuccess())
            {
                base.Action(act);
            }

            ProcessBeforeReturn(this.Status, msgs);
            return msgs;
        }

        private Messages CheckRcard(object act)
        {

            string rcard=act.ToString().Trim().ToUpper();
            Messages msg = new Messages();

            BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;
            if ((act as IDCTClient).DBConnection != null)
            {
                domainProvider = (act as IDCTClient).DBConnection as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
            }
            else
            {
                domainProvider = Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider()
                    as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
                (act as IDCTClient).DBConnection = domainProvider;
            }

            ActionOnLineHelper actionOnLineHelper = new ActionOnLineHelper(domainProvider);

            msg = actionOnLineHelper.GetIDInfo(rcard);

            if (msg.IsSuccess())
            {
                ProductInfo productInfo = (ProductInfo)msg.GetData().Values[0];

                msg = actionOnLineHelper.CheckID(new ActionEventArgs(ActionType.DataCollectAction_GOOD,
                    rcard, ((IDCTClient)act).LoginedUser, ((IDCTClient)act).ResourceCode));
            }


            return msg;

        }

    }
}
