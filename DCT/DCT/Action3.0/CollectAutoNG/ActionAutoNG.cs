using System;
using System.Collections;
using UserControl;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;


namespace BenQGuru.eMES.Common.DCT.Action
{
    /// <summary>
    /// ActionCollectNG 的摘要说明。
    /// </summary>
    public class ActionAutoNG : BaseDCTAction
    {       
        public ActionAutoNG()
        {
            this.InitMessage = (new ActionHelper()).GetActionDesc(this);
            this.OutMesssage = new Message(MessageType.Normal, "$DCT_NG_Please_Input_NG_SN");
            this.LastPrompMesssage = new Message(MessageType.Normal, "$DCT_NG_Please_Input_NG_SN");	
        }

        private ProductInfo currentProductInfo = null;
        private MO moWillGo = null;
        public override Messages PreAction(object act)
        {
            Messages msg = new Messages();
            DataCollect.Action.ActionEventArgs args;
            if (currentProductInfo == null)
            {                             
                args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
                args.RunningCard = act.ToString().ToUpper().Trim();
                msg = this.CheckProduct(args.RunningCard, act);
                if (msg.IsSuccess() == false)
                {
                    msg.Add(new UserControl.Message(MessageType.Normal, "$DCT_NG_Please_Input_NG_SN"));
                    this.Status = ActionStatus.PrepareData;
                    this.FlowDirect = FlowDirect.WaitingInput;
                    return msg;
                }
                else
                {
                    this.ObjectState = args;
                }                               
            }
            if (this.Status == ActionStatus.PrepareData || this.Status == ActionStatus.Working)
            {
                msg = Action(act);
            }
            else if (this.Status == ActionStatus.Pass)
            {
                msg = AftAction(act);
            }         

            return msg;
        }

        public override Messages Action(object act)
        {
            Messages msg = new Messages();
            BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider = null;

            if (act == null)
            {
                return msg;
            }

            if (currentProductInfo == null)
                return msg;

            DataCollect.Action.ActionEventArgs args;
            if (ObjectState == null)
            {
                args = new BenQGuru.eMES.DataCollect.Action.ActionEventArgs();
            }
            else
            {
                args = ObjectState as DataCollect.Action.ActionEventArgs;
            }

            string data = act.ToString().ToUpper().Trim();//Errorcode
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
                       
            //msg = CheckData(data, domainProvider);         


            if (msg.IsSuccess())
            {
                //检查序列号

                ActionOnLineHelper _helper = new ActionOnLineHelper(domainProvider);
                msg = _helper.GetIDInfo(args.RunningCard);

                if (msg.IsSuccess())
                {
                    ProductInfo product = (ProductInfo)msg.GetData().Values[0];

                    TSModelFacade tsmodelFacade = new TSModelFacade(domainProvider);                    

                    if (msg.IsSuccess())
                    {
                        SystemSettingFacade systemSettingFacade = new SystemSettingFacade(domainProvider);
                        object parameter = systemSettingFacade.GetParameter("DEFAULTERRORCODE", "NGCOLLECTDEFAULTERRORCODE");
                        if (parameter == null)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$Error_NoDefaultErrorCode"));
                            msg.Add(new UserControl.Message(MessageType.Normal, "$DCT_NG_Please_Input_NG_SN"));
                            this.Status = ActionStatus.PrepareData;
                            this.FlowDirect = FlowDirect.WaitingInput;
                            //base.Action(act);                            
                            return msg;
                        }
                        Parameter errorCodeParameter = parameter as Parameter;
                        object errorCode = tsmodelFacade.GetErrorCode(errorCodeParameter.ParameterAlias);
                        if (errorCode == null)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$ErrorCode_Not_Exist"));
                            msg.Add(new UserControl.Message(MessageType.Normal, "$DCT_NG_Please_Input_NG_SN"));
                            this.Status = ActionStatus.PrepareData;
                            this.FlowDirect = FlowDirect.WaitingInput;                        
                            return msg;
                        }

                        object[] ecgObjects = tsmodelFacade.GetErrorCodeGroupByErrorCodeCode(((ErrorCodeA)errorCode).ErrorCode);
                        if (ecgObjects == null || ecgObjects.Length == 0)
                        {
                            msg.Add(new UserControl.Message(MessageType.Error, "$Error_ErrorCodeNoErrorGroup"));
                            msg.Add(new UserControl.Message(MessageType.Normal, "$DCT_NG_Please_Input_NG_SN"));
                            this.Status = ActionStatus.PrepareData;
                            this.FlowDirect = FlowDirect.WaitingInput;                         
                            return msg;
                        }

                        ErrorCodeGroup2ErrorCode ecg2ec = (ErrorCodeGroup2ErrorCode)tsmodelFacade.GetErrorCodeGroup2ErrorCodeByecCode(((ErrorCodeA)errorCode).ErrorCode);

                        object[] errorcodes = new object[] { ecg2ec };
                        //if (msg.IsSuccess())
                        //{
                        //    string strModelCode = this.GetModelCodeFromProduct(product, this.moWillGo, domainProvider);
                        //    if (strModelCode != "")
                        //    {
                        //        errorcodes = tsmodelFacade.QueryECG2ECByECAndModelCode(new string[] { ((ErrorCodeA)errorCode).ErrorCode }, strModelCode);
                        //    }

                        //    if (errorcodes == null || errorcodes.Length == 0)
                        //    {
                        //        msg.Add(new UserControl.Message(UserControl.MessageType.Error, "$ErrorCode_Not_BelongTo_ModelCode"));
                        //        base.Action(act);
                        //        ActionRCard actRcard = new ActionRCard();
                        //        this.NextAction = actRcard;
                        //        return msg;
                        //    }
                        //}

                        if (msg.IsSuccess())
                        {
                            IAction dataCollectModule
                                = new BenQGuru.eMES.DataCollect.Action.ActionFactory(domainProvider).CreateAction(ActionType.DataCollectAction_NG);
                            domainProvider.BeginTransaction();
                            try
                            {
                                IDCTClient client = act as IDCTClient;

                                msg.AddMessages(((IActionWithStatus)dataCollectModule).Execute(
                                    new TSActionEventArgs(ActionType.DataCollectAction_NG,
                                    args.RunningCard,
                                    client.LoginedUser,
                                    client.ResourceCode,
                                    product,
                                    errorcodes,
                                    null,
                                    "")));

                                if (msg.IsSuccess())
                                {
                                    domainProvider.CommitTransaction();
                                    base.Action(act);
                                    msg.Add(new UserControl.Message(MessageType.Success, string.Format("$CS_NGSUCCESS")));                                    
                                    return msg;
                                    
                                }
                                else
                                {
                                    domainProvider.RollbackTransaction();
                                }
                            }
                            catch (Exception ex)
                            {
                                domainProvider.RollbackTransaction();
                                msg.Add(new UserControl.Message(ex));
                            }
                            finally
                            {

                                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)domainProvider).PersistBroker.CloseConnection();
                            }
                        }

                    }
                }
            }
            if (msg.IsSuccess())
            {
                base.Action(act);
            }
            else
            {
                msg.Add(new UserControl.Message(MessageType.Normal, "$DCT_NG_Please_Input_NG_SN"));
                this.FlowDirect = FlowDirect.WaitingInput;
            }
            return msg;
        }

        #region Check Data
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">ErrorCode</param>
        /// <returns></returns>
        public Messages CheckData(string data, Common.DomainDataProvider.SQLDomainDataProvider domainProvider)
        {
            Messages msg = new Messages();
            if (data == string.Empty)
            {
                msg.Add(new UserControl.Message(UserControl.MessageType.Error, "$DCT_PLEASE_INPUT_ErrorCode"));
            }
            else
            {

                TSModelFacade tsmodelFacade = new TSModelFacade(domainProvider);
                object obj = tsmodelFacade.GetErrorCode(data);

                if (obj == null)
                {
                    msg.Add(new UserControl.Message(UserControl.MessageType.Error, "$ErrorCode_Not_Exist"));
                    return msg;
                }

                if (currentProductInfo != null)
                {
                    //object[] errorcodes = tsmodelFacade.QueryECG2ECByECAndModelCode(new string[]{data}, currentProductInfo.LastSimulation.ModelCode);
                    object[] errorcodes = null;
                    string strModelCode = GetModelCodeFromProduct(currentProductInfo, this.moWillGo, domainProvider);
                    if (strModelCode != "")
                    {
                        errorcodes = tsmodelFacade.QueryECG2ECByECAndModelCode(new string[] { data }, strModelCode);
                    }

                    if (errorcodes == null || errorcodes.Length == 0)
                    {
                        msg.Add(new UserControl.Message(UserControl.MessageType.Error, "$ErrorCode_Not_BelongTo_ModelCode"));
                        return msg;
                    }
                }
            }

            return msg;
        }

        public Messages CheckProduct(string rcard, object act)
        {
            currentProductInfo = null;
            moWillGo = null;
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
            ActionOnLineHelper _helper = new ActionOnLineHelper(domainProvider);
            msg = _helper.GetIDInfo(rcard);
            if (msg.IsSuccess())
            {
                IDCTClient client = (IDCTClient)act;
                ProductInfo product = (ProductInfo)msg.GetData().Values[0];
                bool bNeedCheckMO = false;
                if (product == null || product.LastSimulation == null)
                {
                    /*	需要再检查是否归属工单
                    msg.Add(new UserControl.Message(UserControl.MessageType.Error,"$NoSimulation"));
                    return msg;
                    */
                    bNeedCheckMO = true;
                }
                else
                {
                    // 如果完工，并且当前资源不属于Simulation的当前工序，则需要检查归属工单
                    if (product.LastSimulation.IsComplete == "1")
                    {
                        BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(domainProvider);
                        if (dataModel.GetOperation2Resource(product.LastSimulation.OPCode, client.ResourceCode) == null)
                        {
                            bNeedCheckMO = true;
                        }
                    }
                }
                Messages msgChkErr = new Messages();
                if (bNeedCheckMO == true)
                {
                    ActionGoToMO actionGoMO = new ActionGoToMO(domainProvider);
                    Messages msgMo = actionGoMO.GetItemCodeFromGoMoRCard(client.ResourceCode, rcard);
                    if (msgMo.IsSuccess() == false)		// 如果有错误，表示需要归属工单，但是解析工单或查询工单发生错误
                    {
                        msgChkErr.AddMessages(msgMo);
                    }
                    else	// 返回成功，有两种情况：需要归属工单并且返回正确的工单信息；不需要归属工单
                    {
                        UserControl.Message msgMoData = msgMo.GetData();
                        if (msgMoData != null && msgMoData.Values.Length > 0)		// 有DATA数据，表示需要归属工单
                        {
                            this.moWillGo = (MO)msgMoData.Values[0];
                        }
                        else		// 如果没有DATA数据，表示不需要归属工单，则调用以前的代码：如果LastSimulation为空，则报没有序列号
                        {
                            if (product.LastSimulation == null)
                            {
                                msgChkErr.Add(new UserControl.Message(UserControl.MessageType.Error, "$NoSimulation"));
                            }
                        }
                    }
                }
                if (msgChkErr.IsSuccess() == false)
                {
                    return msgChkErr;
                }

                if (product.LastSimulation != null)		// 只有在序列号存在的情况下才检查途程
                {
                    msg = _helper.CheckID(new TSActionEventArgs(ActionType.DataCollectAction_NG,
                        rcard,
                        client.LoginedUser,
                        client.ResourceCode,
                        product,
                        new object[] { },
                        null,
                        ""));
                }
                if (product.LastSimulation == null || msg.IsSuccess() == true)
                {
                    currentProductInfo = product;
                }
                else if (product.LastSimulation.LastAction == ActionType.DataCollectAction_GOOD ||
                        product.LastSimulation.LastAction == ActionType.DataCollectAction_NG ||
                        product.LastSimulation.LastAction == ActionType.DataCollectAction_OutLineGood ||
                        product.LastSimulation.LastAction == ActionType.DataCollectAction_OutLineNG)	// 允许测试站重复采集
                {
                    BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(domainProvider);
                    if (dataModel.GetOperation2Resource(product.LastSimulation.OPCode, (act as IDCTClient).ResourceCode) != null)
                    {
                        msg.ClearMessages();
                        currentProductInfo = product;
                    }

                }
            }
            return msg;
        }


        public override Messages AftAction(object act)
        {
            base.AftAction(act);

            return null;
        }
        /// <summary>
        /// 查询产品别代码：如果mo不为null，则通过工单号查询产品别，否则通过product中的序列号查询
        /// </summary>
        /// <returns></returns>
        private string GetModelCodeFromProduct(ProductInfo product, MO mo, BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider domainProvider)
        {
            string strModelCode = string.Empty;
            if (mo != null)
            {
                BenQGuru.eMES.MOModel.ModelFacade modelFacade = new ModelFacade(domainProvider);
                object[] objsModel = modelFacade.GetModel2ItemByItemCode(mo.ItemCode);
                if (objsModel != null && objsModel.Length > 0)
                {
                    Model2Item modelItem = (Model2Item)objsModel[0];
                    strModelCode = modelItem.ModelCode;
                }
            }
            else if (product.LastSimulation != null)
                strModelCode = product.LastSimulation.ModelCode;
            return strModelCode;
        }
        #endregion

    }
}
