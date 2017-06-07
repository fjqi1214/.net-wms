using System;
using System.Collections;
using System.Reflection;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

using UserControl;

namespace BenQGuru.eMES.Common.DCT.Action
{
    public class ActionHelper
    {
        private Hashtable _CommandToAction = new Hashtable();

        public ActionHelper()
        {
            _CommandToAction.Add(ActionType.DataCollectAction_GoMO, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionGotoMO"));
            _CommandToAction.Add(ActionType.DataCollectAction_GOOD, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionCollectGood"));
            _CommandToAction.Add(ActionType.DataCollectAction_NG, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionCollectNG"));
            _CommandToAction.Add(ActionType.DataCollectAction_AutoNG, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionAutoNG"));
            _CommandToAction.Add(ActionType.DataCollectAction_KBatch, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionBatchWithKeypaerts"));

            _CommandToAction.Add(ActionType.DataCollectAction_Mix, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionCollectMix"));
            _CommandToAction.Add(ActionType.DataCollectAction_Split, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionCollectIDMerge"));
            _CommandToAction.Add(BaseDCTDriver.NEXTOP, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionNextOP"));
            _CommandToAction.Add(ActionType.DataCollectAction_CompareAppendix, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionCompareApp"));
            _CommandToAction.Add(ActionType.DataCollectAction_CompareTwo, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionCompareAppAndProductCode"));

            _CommandToAction.Add(ActionType.DataCollectAction_SMTNG, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionCollectSMTNG"));
            _CommandToAction.Add(ActionType.DataCollectAction_FGPacking, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionFGPacking"));
            _CommandToAction.Add(ActionType.DataCollectAction_CompareProductCode, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionCompareProductCode"));
            _CommandToAction.Add(ActionType.DataCollectAction_ONPost, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionCollectONPost"));
            _CommandToAction.Add(ActionType.DataCollectAction_OffPost, Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionCollectOffPost"));
            _CommandToAction.Add("LOGIN", Type.GetType("BenQGuru.eMES.Common.DCT.Action.ActionLogin"));
        }

        public BaseDCTAction GetActionByCommand(string command)
        {
            BaseDCTAction action = null;

            Type actionType = (Type)_CommandToAction[command.Trim().ToUpper()];

            if (actionType == null)
            {
                action = null;
            }
            else
            {
                action = (BaseDCTAction)actionType.Assembly.CreateInstance(actionType.FullName);
            }

            if (action != null)
            {
                action.Status = ActionStatus.Init;
                action.FlowDirect = FlowDirect.WaitingOutput;
                action.NextAction = null;
            }

            return action;
        }

        public string GetCommandByAction(BaseDCTAction action)
        {
            string command = string.Empty;

            foreach (string cmd in _CommandToAction.Keys)
            {
                if ((Type)_CommandToAction[cmd] == action.GetType())
                {
                    command = cmd.Trim().ToUpper();
                    break;
                }
            }

            return command;
        }

        public Message GetActionDesc(BaseDCTAction action)
        {
            Message msg = null;
            string command = GetCommandByAction(action);

            if (command.Length > 0)
            {
                BaseModelFacade facade = new BaseModelFacade((SQLDomainDataProvider)DomainDataProviderManager.DomainDataProvider());

                Dct dctAction = (Dct)facade.GetDCT(command);

                if (dctAction != null)
                {
                    msg = new Message("$DCT_CurrentDCTCommand " + dctAction.Dctdesc);
                }
            }

            return msg;
        }

        public BaseDCTAction GetEndNodeAction(IDCTClient client)
        {
            BaseDCTAction action = null;

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

            BaseModelFacade baseModelFacade = new BaseModelFacade(domainProvider);

            if (client.ResourceCode != null && client.ResourceCode.Trim().Length > 0)
            {
                Resource res = (Resource)baseModelFacade.GetResource(client.ResourceCode.Trim().ToUpper());

                if (res != null && res.DctCode != null & res.DctCode.Trim().Length > 0)
                {
                    action = (new ActionHelper()).GetActionByCommand(res.DctCode.Trim().ToUpper());
                }
            }

            if (action == null)
            {
                action = new ActionIdle();
            }

            return action;
        }
    }
}
