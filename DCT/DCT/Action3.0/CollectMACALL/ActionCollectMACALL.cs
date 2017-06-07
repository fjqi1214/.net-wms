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
    public class ActionCollectMACALL : BaseDCTAction
    {
        private bool _NeedGoMO = false;
        private string _NeedGoMOCode = string.Empty;

        public ActionCollectMACALL()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$DCT_GOOD_Please_Input_GOOD_SN");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$DCT_GOOD_Please_Input_GOOD_SN");
        }

        // 是否需要同时做归属工单
        public bool NeedGoMO
        {
            get
            {
                return _NeedGoMO;
            }
            set
            {
                _NeedGoMO = value;
            }
        }

        // 同时做归属工单的工单代码
        public string NeedGoMOCode
        {
            get
            {
                return _NeedGoMOCode;
            }
            set
            {
                _NeedGoMOCode = value;
            }
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

                        // 同时做归属工单
                        if (this.NeedGoMO == true && this.NeedGoMOCode != string.Empty)
                        {
                            ActionGotoMO actionGoMO = new ActionGotoMO();
                            msgs.AddMessages(actionGoMO.CheckSNFormat(args));

                            if (msgs.IsSuccess())
                            {
                                IAction dataCollectModuleGoMO = new BenQGuru.eMES.DataCollect.Action.ActionFactory(domainProvider).CreateAction(ActionType.DataCollectAction_GoMO);
                                msgs.AddMessages(((IActionWithStatus)dataCollectModuleGoMO).Execute(
                                    new GoToMOActionEventArgs(
                                    ActionType.DataCollectAction_GoMO,
                                    args.RunningCard,
                                    client.LoginedUser,
                                    client.ResourceCode,
                                    product,
                                    this.NeedGoMOCode)));
                            }

                            // 做归属工单后，重新获取ProductInfo
                            if (msgs.IsSuccess())
                            {
                                msgs = _helper.GetIDInfo(args.RunningCard);
                                product = (ProductInfo)msgs.GetData().Values[0];
                            }
                        }

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

                        // 做SMT物料关联
                        if (msgs.IsSuccess())
                        {
                            if (System.Configuration.ConfigurationSettings.AppSettings["PartConn"] != null &&
                                System.Configuration.ConfigurationSettings.AppSettings["PartConn"].ToUpper().Trim() == "TRUE")
                            {
                                msgs.AddMessages(this.SMTLoadItem(args.RunningCard.ToUpper().Trim(), client.ResourceCode.ToUpper(), client.LoginedUser.ToUpper(), domainProvider));
                            }
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

        private Messages SMTLoadItem(string rcard, string resourceCode, string userCode, BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider)
        {
            Messages msg = new Messages();
            BenQGuru.eMES.SMT.SMTFacade smtFacade = new BenQGuru.eMES.SMT.SMTFacade(domainProvider);
            msg = smtFacade.LoadMaterialForRCard(rcard, resourceCode, userCode);
            return msg;
        }
    }
}
