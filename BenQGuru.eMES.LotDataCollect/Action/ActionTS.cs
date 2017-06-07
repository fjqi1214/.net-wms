using System;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.LotDataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.LotDataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;

using UserControl;

namespace BenQGuru.eMES.LotDataCollect.Action
{
    /// <summary>
    /// 归属工单采集
    /// </summary>
    public class ActionTS : IActionWithStatus
    {
        private IDomainDataProvider _domainDataProvider = null;

        //		public ActionTS()
        //		{
        //			//
        //			// TODO: 在此处添加构造函数逻辑
        //			//
        //		}

        public ActionTS(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        /// <summary>
        /// 不良品采集
        /// </summary>
        /// <param name="domainDataProvider"></param>
        /// <param name="iD"></param>
        /// <param name="actionType"></param>
        /// <param name="resourceCode"></param>
        /// <param name="userCode"></param>
        /// <param name="product"></param>
        /// <param name="datas1"></param>
        /// <param name="datas2"></param>
        /// <returns></returns>
        public Messages Execute(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            Messages msgAutoGoMO = new Messages();		// Added by Icyer 2007/03/09
            try
            {
                // Added by Icyer 2007/03/09
                // 检测自动归属工单
                ActionGoToMO actionGoToMO = new ActionGoToMO(this.DataProvider);
                msgAutoGoMO = actionGoToMO.AutoGoMO(actionEventArgs);
                // Added end

                // Added by Icyer 2006/12/03
                // 自动做Undo
                //messages.AddMessages((new ActionUndoNG(this.DataProvider)).UndoNG(actionEventArgs));
                // Added end
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(actionEventArgs));
                if (messages.IsSuccess())
                {
                    //补充SIMULATION 不良信息
                    actionEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.NG;
                    actionEventArgs.ProductInfo.NowSimulation.NGTimes = actionEventArgs.ProductInfo.NowSimulation.NGTimes + 1;
                    actionEventArgs.ProductInfo.NowSimulation.GoodQty = 0;
                    actionEventArgs.ProductInfo.NowSimulation.NGQty = actionEventArgs.ProductInfo.NowSimulation.LotQty;
                    if (actionEventArgs.CurrentMO != null)
                    {
                        actionEventArgs.ProductInfo.NowSimulation.RMABillCode = actionEventArgs.CurrentMO.RMABillCode;
                    }
                    else
                    {
                        MO mo = (new MOFacade(DataProvider)).GetMO(actionEventArgs.ProductInfo.NowSimulation.MOCode) as MO;
                        actionEventArgs.CurrentMO = mo;
                        actionEventArgs.ProductInfo.NowSimulation.RMABillCode = mo.RMABillCode;
                    }

                    messages.AddMessages(dataCollect.Execute(actionEventArgs, null, false, false));
                    if (messages.IsSuccess())
                    {

                        //Laws Lu,2005/12/19,新增	获取ErrorGroup2ErrorCode
                        if (actionEventArgs.ActionType == ActionType.DataCollectAction_NG)
                        {
                            actionEventArgs.ProductInfo.ECG2ErrCodes = ParseECGECQty(((TSActionEventArgs)actionEventArgs).ErrorCodes, actionEventArgs.ActionType);
                        }
                        else
                        {
                            actionEventArgs.ProductInfo.ECG2ErrCodes = ParseECGECQty(((TSActionEventArgs)actionEventArgs).ErrorInfor, actionEventArgs.ActionType);
                        }

                        //填写测试不良数据
                        messages.AddMessages(dataCollect.CollectErrorInformation(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo, ((TSActionEventArgs)actionEventArgs).ErrorCodes, null, ((TSActionEventArgs)actionEventArgs).Memo));

                        // Added By Hi1/Venus.Feng on 20080711 for Hisense Version : Auto Set RCard Reflow Route and OP by ErrorCode
                        //if (messages.IsSuccess() == true)
                        //{
                        //    messages.AddMessages(this.SetRCardReflowByErrorCode(actionEventArgs));
                        //}
                    }

                    if (messages.IsSuccess())
                    {
                        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                        //dataCollectFacade.TryToDeleteRCardFromLot(actionEventArgs.LotCode);
                    }
                }
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            //return messages;
            if (msgAutoGoMO.Count() < 1 || (msgAutoGoMO.IsSuccess() == true && messages.IsSuccess() == false))
                return messages;
            else
            {
                msgAutoGoMO.IgnoreError();
                msgAutoGoMO.AddMessages(messages);
                return msgAutoGoMO;
            }
        }
        //Laws Lu,2006/01/10,获取不良代码组、不良代码
        public static string ParseECG2Errs(object[] errorInfo, string actType)
        {
            string strReturn = String.Empty;
            if (errorInfo == null)
            {
                return strReturn;
            }
            for (int i = 0; i < errorInfo.Length; i++)
            {
                if (actType == ActionType.DataCollectAction_NG || actType == ActionType.DataCollectAction_OutLineNG)
                {
                    ErrorCodeGroup2ErrorCode tsLoc = ((ErrorCodeGroup2ErrorCode)errorInfo[i]);
                    strReturn += tsLoc.ErrorCodeGroup + ":" + tsLoc.ErrorCode + ";";
                }
            }

            return strReturn;
        }

        //add by kathy @20130830，获取不良代码组、不良代码、不良数量
        public static string ParseECGECQty(object[] errorInfo, string actType)
        {
            string strReturn = String.Empty;
            if (errorInfo == null)
            {
                return strReturn;
            }
            for (int i = 0; i < errorInfo.Length; i++)
            {
                if (actType == ActionType.DataCollectAction_NG || actType == ActionType.DataCollectAction_OutLineNG)
                {
                    TSErrorCode tsLoc = ((TSErrorCode)errorInfo[i]);
                    strReturn += tsLoc.ErrorCodeGroup + ":" + tsLoc.ErrorCode + ":" + tsLoc.ErrorQty + ";";
                }
            }

            return strReturn;
        }
        /// <summary>
        /// 不良品采集
        /// </summary>
        /// <param name="domainDataProvider"></param>
        /// <param name="iD"></param>
        /// <param name="actionType"></param>
        /// <param name="resourceCode"></param>
        /// <param name="userCode"></param>
        /// <param name="product"></param>
        /// <param name="datas1"></param>
        /// <param name="datas2"></param>
        /// <returns></returns>
        public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            Messages msgAutoGoMO = new Messages();		// Added by Icyer 2007/03/09
            try
            {
                // Added by Icyer 2007/03/09
                // 检测自动归属工单
                ActionGoToMO actionGoToMO = new ActionGoToMO(this.DataProvider);
                msgAutoGoMO = actionGoToMO.AutoGoMO(actionEventArgs);
                // Added end

                // Added by Icyer 2006/12/03
                // 自动做Undo
                //messages.AddMessages((new ActionUndoNG(this.DataProvider)).UndoNG(actionEventArgs));
                // Added end
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(actionEventArgs, actionCheckStatus));
                if (messages.IsSuccess())
                {
                    //补充SIMULATION 不良信息
                    actionEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.NG;
                    actionEventArgs.ProductInfo.NowSimulation.NGTimes = actionEventArgs.ProductInfo.NowSimulation.NGTimes + 1;
                    actionEventArgs.ProductInfo.NowSimulation.GoodQty = 0;
                    actionEventArgs.ProductInfo.NowSimulation.NGQty = actionEventArgs.ProductInfo.NowSimulation.LotQty;


                    if (actionEventArgs.CurrentMO != null)
                    {
                        actionEventArgs.ProductInfo.NowSimulation.RMABillCode = actionEventArgs.CurrentMO.RMABillCode;
                    }
                    else
                    {
                        MO mo = (new MOFacade(DataProvider)).GetMO(actionEventArgs.ProductInfo.NowSimulation.MOCode) as MO;
                        actionEventArgs.CurrentMO = mo;
                        actionEventArgs.ProductInfo.NowSimulation.RMABillCode = mo.RMABillCode;
                    }

                    if (actionCheckStatus.NeedUpdateSimulation)
                    {
                        messages.AddMessages(dataCollect.Execute(actionEventArgs, null, false, false));
                    }
                    else
                    {
                        messages.AddMessages(dataCollect.Execute(actionEventArgs, actionCheckStatus, false, false));
                    }

                    if (messages.IsSuccess())
                    {
                        if (actionCheckStatus.NeedFillReport == true)
                        {
                            //Laws Lu,2005/12/19,新增	获取ErrorGroup2ErrorCode
                            if (actionEventArgs.ActionType == ActionType.DataCollectAction_NG)
                            {
                                actionEventArgs.ProductInfo.ECG2ErrCodes = ParseECGECQty(((TSActionEventArgs)actionEventArgs).ErrorCodes, actionEventArgs.ActionType);
                            }
                            else
                            {
                                actionEventArgs.ProductInfo.ECG2ErrCodes = ParseECGECQty(((TSActionEventArgs)actionEventArgs).ErrorInfor, actionEventArgs.ActionType);
                            }
                        }
                        //填写测试报表 TODO
                        messages.AddMessages(dataCollect.CollectErrorInformation(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo, ((TSActionEventArgs)actionEventArgs).ErrorCodes, null, ((TSActionEventArgs)actionEventArgs).Memo));
                        // Added By Hi1/Venus.Feng on 20080711 for Hisense Version : Auto Set RCard Reflow Route and OP by ErrorCode
                        //if (messages.IsSuccess() == true)
                        //{
                        //    messages.AddMessages(this.SetRCardReflowByErrorCode(actionEventArgs));
                        //}
                    }

                }
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            //return messages;
            if (msgAutoGoMO.Count() < 1 || (msgAutoGoMO.IsSuccess() == true && messages.IsSuccess() == false))
                return messages;
            else
            {
                msgAutoGoMO.IgnoreError();
                msgAutoGoMO.AddMessages(messages);
                return msgAutoGoMO;
            }
        }

        private Messages SetRCardReflowByErrorCode(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            string routeCode = "";
            string opCode = "";

            System.Collections.Generic.List<string> listErrorCode = new System.Collections.Generic.List<string>();

            if (((TSActionEventArgs)actionEventArgs).ErrorCodes != null)
            {
                ErrorCodeGroup2ErrorCode[] error = new ErrorCodeGroup2ErrorCode[((TSActionEventArgs)actionEventArgs).ErrorCodes.Length];
                ((TSActionEventArgs)actionEventArgs).ErrorCodes.CopyTo(error, 0);
                for (int i = 0; i < error.Length; i++)
                {
                    if (error[i].ErrorCode != "" && listErrorCode.Contains(error[i].ErrorCode) == false)
                    {
                        listErrorCode.Add(error[i].ErrorCode);
                    }
                }
            }
            if (listErrorCode.Count == 0)
                return messages;

            // 查询不良代码和产品对应的返工途程
            TSModel.TSModelFacade tsmodelFacade = new BenQGuru.eMES.TSModel.TSModelFacade(this.DataProvider);
            object objErrorCode2OPRework = tsmodelFacade.GetErrorCode2OPRework(actionEventArgs.ProductInfo.NowSimulation.OPCode,
                listErrorCode[0], GlobalVariables.CurrentOrganizations.First().OrganizationID);

            object tempOPRework;
            ErrorCode2OPRework tempErrorCode2OPRework;
            if (objErrorCode2OPRework == null)
            {
                for (int i = 1; i < listErrorCode.Count; i++)
                {
                    tempOPRework = tsmodelFacade.GetErrorCode2OPRework(actionEventArgs.ProductInfo.NowSimulation.OPCode,
                           listErrorCode[i], GlobalVariables.CurrentOrganizations.First().OrganizationID);
                    if (tempOPRework != null)
                    {
                        messages.Add(new Message(MessageType.Error, "$Error_ErrorCodeHaveMoreThanOneRoute"));
                        return messages;
                    }
                }
                return messages;
            }
            else
            {
                ErrorCode2OPRework firstErrorCode2OPRework = objErrorCode2OPRework as ErrorCode2OPRework;
                for (int i = 1; i < listErrorCode.Count; i++)
                {
                    tempOPRework = tsmodelFacade.GetErrorCode2OPRework(actionEventArgs.ProductInfo.NowSimulation.OPCode,
                           listErrorCode[i], GlobalVariables.CurrentOrganizations.First().OrganizationID);

                    if (tempOPRework == null)
                    {
                        messages.Add(new Message(MessageType.Error, "$Error_ErrorCodeHaveMoreThanOneRoute"));
                        return messages;
                    }
                    else
                    {
                        tempErrorCode2OPRework = tempOPRework as ErrorCode2OPRework;
                        if (tempErrorCode2OPRework.RouteCode != firstErrorCode2OPRework.RouteCode
                            || tempErrorCode2OPRework.ToOPCode != firstErrorCode2OPRework.ToOPCode)
                        {
                            messages.Add(new Message(MessageType.Error, "$Error_ErrorCodeHaveMoreThanOneRoute"));
                            return messages;
                        }
                    }
                }

                opCode = firstErrorCode2OPRework.ToOPCode;
                routeCode = firstErrorCode2OPRework.RouteCode;
                if (string.Compare(opCode, "TS", true) == 0)
                {
                    return messages;
                }
            }

            if (routeCode.Trim().Length == 0)  // 如果途程等于空，则将当前序列号的途程作为返工途程 
            {
                routeCode = actionEventArgs.ProductInfo.NowSimulation.RouteCode;
            }

            // 检查产品与途程的对应
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            ItemRoute2OP op = (ItemRoute2OP)itemFacade.GetItemRoute2Operation(actionEventArgs.ProductInfo.NowSimulation.ItemCode, routeCode, opCode);
            if (op == null)
            {
                messages.Add(new Message(MessageType.Error, "$Error_ReworkRouteNotBelongToItem [" + routeCode + "]"));
                return messages;
            }

            // 将TS的状态改成维修中
            TSFacade tsFacade = new TSFacade(this.DataProvider);
            object obj = tsFacade.GetCardLastTSRecord(actionEventArgs.LotCode);
            if (obj != null)
            {
                Domain.TS.TS ts = (Domain.TS.TS)obj;
                ts.ConfirmResourceCode = actionEventArgs.ResourceCode;
                ts.ConfirmUser = actionEventArgs.UserCode;
                ts.ConfirmDate = actionEventArgs.ProductInfo.NowSimulation.BeginDate;
                ts.ConfirmTime = actionEventArgs.ProductInfo.NowSimulation.BeginTime;
                ts.TSStatus = TSStatus.TSStatus_TS;
                tsFacade.UpdateTS(ts);
            }
            else
                return messages;

            // 开始设置回流
            ActionFactory actionFactory = new ActionFactory(this.DataProvider);
            IAction actionTSComplete = actionFactory.CreateAction(ActionType.DataCollectAction_TSComplete);
            TSActionEventArgs tsactionEventArgs = new TSActionEventArgs(
                ActionType.DataCollectAction_TSComplete,
                actionEventArgs.LotCode,
                actionEventArgs.UserCode,
                actionEventArgs.ResourceCode,
                TSStatus.TSStatus_Reflow,
                actionEventArgs.ProductInfo.NowSimulation.MOCode,
                actionEventArgs.ProductInfo.NowSimulation.ItemCode,
                routeCode,
                op.OPCode,
                actionEventArgs.UserCode,
                null);
            tsactionEventArgs.RouteCode = routeCode;
            tsactionEventArgs.IgnoreResourceInOPTS = true;

            messages.AddMessages(actionTSComplete.Execute(tsactionEventArgs));

            return messages;
        }
    }
}
