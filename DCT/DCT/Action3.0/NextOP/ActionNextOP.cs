using System;
using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;

namespace BenQGuru.eMES.Common.DCT.Action
{
    /// <summary>
    /// ActionNextOP 的摘要说明。
    /// </summary>
    public class ActionNextOP : BaseDCTAction
    {
        public ActionNextOP()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
            this.LastPrompMesssage = new UserControl.Message(MessageType.Normal, "$CS_Please_Input_RunningCard");
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

            BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;

            DataCollect.Action.ActionEventArgs args;
            if (ObjectState == null)
            {
                args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
                args.RunningCard = act.ToString().Trim().ToUpper();
            }
            else
            {
                args = ObjectState as DataCollect.Action.ActionEventArgs;
            }

            //Laws Lu,2006/06/03	添加	获取已有连接
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

            if (msgs.IsSuccess())
            {

                //检查序列号
                ActionOnLineHelper _helper = new ActionOnLineHelper(domainProvider);

                msgs = _helper.GetIDInfo(args.RunningCard);

                if (msgs.IsSuccess())
                {
                    ProductInfo product = (ProductInfo)msgs.GetData().Values[0];

                    if (product == null || product.LastSimulation == null)
                    {
                        msgs.Add(new UserControl.Message(MessageType.Error, "$NoSimulation"));
                    }
                    else
                    {
                        msgs.ClearMessages();
                        if (product.LastSimulation.IsComplete == "1")
                        {
                            msgs.Add(new UserControl.Message(MessageType.Error, "$CS_ERROR_PRODUCT_ALREADY_COMPLETE"));
                        }
                        else
                        {
                            switch (product.LastSimulation.ProductStatus)
                            {
                                case ProductStatus.NG:
                                    BenQGuru.eMES.Domain.TS.TS ts = (BenQGuru.eMES.Domain.TS.TS)(new TSFacade(domainProvider)).GetCardLastTSRecord(args.RunningCard);
                                    if (ts != null &&
                                        (ts.TSStatus == TSStatus.TSStatus_Complete || ts.TSStatus == TSStatus.TSStatus_Reflow))
                                    {
                                        msgs.AddMessages(GetRunningCardOP(product, domainProvider));
                                    }
                                    else
                                    {
                                        msgs.Add(new UserControl.Message(MessageType.Error, "$CS_NG_PLEASE_SEND_TS"));
                                    }

                                    break;
                                case ProductStatus.OffMo:
                                    msgs.Add(new UserControl.Message(MessageType.Error, "$CS_RCRAD_ALREADY_OFF_MO"));
                                    break;
                                case ProductStatus.Scrap:
                                    msgs.Add(new UserControl.Message(MessageType.Error, "$CS_Error_Product_Already_Scrap"));
                                    break;
                                default:
                                    msgs.AddMessages(GetRunningCardOP(product, domainProvider));
                                    break;
                            }
                        }
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

        // 查询产品序列号应处的工序
        public Messages GetRunningCardOP(ProductInfo product, Common.DomainDataProvider.SQLDomainDataProvider domainProvider)
        {
            Messages msg = new Messages();
            DataCollectFacade dataCollect = new DataCollectFacade(domainProvider);
            try
            {
                ItemRoute2OP op = dataCollect.GetMORouteNextOP(product.LastSimulation.MOCode, product.LastSimulation.RouteCode, product.LastSimulation.OPCode);
                if (op != null)
                {
                    msg.Add(new UserControl.Message(MessageType.Success, "$DCT_RunningCard_In_OP " + op.OPCode));
                }
                else
                {
                    msg.Add(new Message(MessageType.Error, "$CS_Route_Failed_GetNotNextOP"));
                }
            }
            catch (Exception ex)
            {
                msg.Add(new Message(ex));
            }
            return msg;
        }

        public override Messages AftAction(object act)
        {
            base.AftAction(act);

            return null;
        }

    }
}
