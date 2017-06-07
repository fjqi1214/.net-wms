using System;
using UserControl;
using System.Collections.Generic;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.DataCollect;

namespace BenQGuru.eMES.DataCollect.Action
{
    /// <summary>
    /// 归属工单采集
    /// </summary>
    public class ActionECNTRY : IActionWithStatus
    {

        private IDomainDataProvider _domainDataProvider = null;

        //		public ActionECNTRY()
        //		{	
        //		}

        public ActionECNTRY(IDomainDataProvider domainDataProvider)
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

        public Messages Execute(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);

            try
            {
                DataCollectFacade facade = new DataCollectFacade(this.DataProvider);
                ActionOnLineHelper helper = new ActionOnLineHelper(this.DataProvider);

                if (actionEventArgs.ProductInfo == null)
                {
                    // 获得LastSimulation
                    messages.AddMessages(helper.GetIDInfo(actionEventArgs.RunningCard));

                    if (messages.IsSuccess())
                    {
                        actionEventArgs.ProductInfo = (ProductInfo)messages.GetData().Values[0];
                    }
                }

                if (actionEventArgs.ProductInfo == null)
                {
                    throw new Exception("$NoProductInfo");
                }

                if (actionEventArgs.ProductInfo.LastSimulation == null)
                {
                    throw new Exception("$NoSimulationInfo");
                }

                if (messages.IsSuccess())
                {
                    facade.CheckMO(actionEventArgs.ProductInfo.LastSimulation.MOCode, actionEventArgs.ProductInfo);
                    facade.GetRouteOPOnline(actionEventArgs.RunningCard, actionEventArgs.ActionType, actionEventArgs.ResourceCode, actionEventArgs.UserCode, actionEventArgs.ProductInfo);

                    if (messages.IsSuccess())
                    {
                        actionEventArgs.ProductInfo.NowSimulation.MaintainUser = actionEventArgs.UserCode;

                        DBDateTime dbDateTime;
                        //Laws Lu,2006/11/13 uniform system collect date
                        if (actionEventArgs.ProductInfo.WorkDateTime != null)
                        {
                            dbDateTime = actionEventArgs.ProductInfo.WorkDateTime;

                        }
                        else
                        {
                            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                            actionEventArgs.ProductInfo.WorkDateTime = dbDateTime;
                        }

                        actionEventArgs.ProductInfo.NowSimulation.MaintainDate = dbDateTime.DBDate;
                        actionEventArgs.ProductInfo.NowSimulation.MaintainTime = dbDateTime.DBTime;

                        // 填写NowSimulationReport
                        actionEventArgs.ProductInfo.NowSimulationReport = helper.FillSimulationReport(actionEventArgs.ProductInfo);

                        //填写ECN\TRY报表
                        if (((EcnTryActionEventArgs)actionEventArgs).ECNNo != "")
                        {
                            this.WriteECNInfo((EcnTryActionEventArgs)actionEventArgs, facade);
                        }

                        if (((EcnTryActionEventArgs)actionEventArgs).TryNo != "")
                        {
                            this.WriteTryInfo((EcnTryActionEventArgs)actionEventArgs, facade);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }

            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        //Add by Icyer 2005/10/28
        //扩展一个带ActionCheckStatus参数的方法
        public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);

            try
            {
                DataCollectFacade facade = new DataCollectFacade(this.DataProvider);
                ActionOnLineHelper helper = new ActionOnLineHelper(this.DataProvider);

                if (actionEventArgs.ProductInfo == null)
                {
                    actionEventArgs.ProductInfo = actionCheckStatus.ProductInfo;
                    if (actionEventArgs.ProductInfo == null)
                    {
                        // 获得LastSimulation
                        messages.AddMessages(helper.GetIDInfo(actionEventArgs.RunningCard));

                        if (messages.IsSuccess())
                        {
                            actionEventArgs.ProductInfo = (ProductInfo)messages.GetData().Values[0];
                            actionCheckStatus.ProductInfo = (ProductInfo)messages.GetData().Values[0];
                        }
                    }
                }

                if (actionEventArgs.ProductInfo == null)
                {
                    throw new Exception("$NoProductInfo");
                }

                if (actionEventArgs.ProductInfo.LastSimulation == null)
                {
                    throw new Exception("$NoSimulationInfo");
                }

                if (messages.IsSuccess())
                {
                    //如果没有检查过工单
                    if (actionCheckStatus.CheckedMO == false)
                    {
                        facade.CheckMO(actionEventArgs.ProductInfo.LastSimulation.MOCode, actionEventArgs.ProductInfo);
                        actionCheckStatus.CheckedMO = true;
                    }
                    facade.GetRouteOPOnline(actionEventArgs.RunningCard, actionEventArgs.ActionType, actionEventArgs.ResourceCode, actionEventArgs.UserCode, actionEventArgs.ProductInfo, actionCheckStatus);

                    if (messages.IsSuccess())
                    {
                        actionEventArgs.ProductInfo.NowSimulation.MaintainUser = actionEventArgs.UserCode;

                        DBDateTime dbDateTime;
                        //Laws Lu,2006/11/13 uniform system collect date
                        if (actionEventArgs.ProductInfo.WorkDateTime != null)
                        {
                            dbDateTime = actionEventArgs.ProductInfo.WorkDateTime;

                        }
                        else
                        {
                            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                            actionEventArgs.ProductInfo.WorkDateTime = dbDateTime;
                        }

                        actionEventArgs.ProductInfo.NowSimulation.MaintainDate = dbDateTime.DBDate;
                        actionEventArgs.ProductInfo.NowSimulation.MaintainTime = dbDateTime.DBTime;

                        actionEventArgs.ProductInfo.NowSimulationReport = helper.FillSimulationReport(actionEventArgs.ProductInfo);

                        //填写ECN\TRY报表
                        if (((EcnTryActionEventArgs)actionEventArgs).ECNNo != "")
                        {
                            this.WriteECNInfo((EcnTryActionEventArgs)actionEventArgs, facade, actionCheckStatus);
                        }

                        if (((EcnTryActionEventArgs)actionEventArgs).TryNo != "")
                        {
                            this.WriteTryInfo((EcnTryActionEventArgs)actionEventArgs, facade, actionCheckStatus);
                        }

                        //将Action加入列表
                        actionCheckStatus.ActionList.Add(actionEventArgs);
                    }
                }
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }

            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }
        // Added end

        private void WriteECNInfo(EcnTryActionEventArgs actionEventArgs, DataCollectFacade facade)
        {
            WriteECNInfo(actionEventArgs, facade, null);
        }
        private void WriteECNInfo(EcnTryActionEventArgs actionEventArgs, DataCollectFacade facade, ActionCheckStatus actionCheckStatus)
        {
            SimulationReport report = actionEventArgs.ProductInfo.NowSimulationReport;

            OnWIPECN onWIPECN = facade.CreateNewOnWIPECN();

            onWIPECN.ECNNO = actionEventArgs.ECNNo;
            onWIPECN.RunningCard = report.RunningCard;
            onWIPECN.RunningCardSequence = report.RunningCardSequence;
            onWIPECN.MOCode = report.MOCode;
            onWIPECN.ItemCode = report.ItemCode;
            onWIPECN.ResourceCode = report.ResourceCode;
            onWIPECN.OPCode = report.OPCode;
            onWIPECN.RouteCode = report.RouteCode;
            onWIPECN.ModelCode = report.ModelCode;
            onWIPECN.MaintainDate = report.MaintainDate;
            onWIPECN.MaintainTime = report.MaintainTime;
            onWIPECN.MaintainUser = report.MaintainUser;
            onWIPECN.ShiftTypeCode = report.ShiftTypeCode;
            onWIPECN.StepSequenceCode = report.StepSequenceCode;
            onWIPECN.SegmnetCode = report.SegmentCode;
            onWIPECN.ShiftCode = report.ShiftCode;
            onWIPECN.TimePeriodCode = report.TimePeriodCode;
            onWIPECN.MOSeq = report.MOSeq;  // Added by Icyer 2007/07/02

            if (actionCheckStatus == null || actionCheckStatus.NeedUpdateSimulation == true)
            {
                facade.AddOnWIPECN(onWIPECN);
            }
            else
            {
                actionEventArgs.OnWIP.Add(onWIPECN);
            }
        }

        private void WriteTryInfo(EcnTryActionEventArgs actionEventArgs, DataCollectFacade facade)
        {
            WriteTryInfo(actionEventArgs, facade, null);
        }
        private void WriteTryInfo(EcnTryActionEventArgs actionEventArgs, DataCollectFacade facade, ActionCheckStatus actionCheckStatus)
        {
            SimulationReport report = actionEventArgs.ProductInfo.NowSimulationReport;

            List<string> TryList = new List<string>();

            if (actionEventArgs.TryNo.Trim().Length > 0)
            {
                string TryCode = actionEventArgs.TryNo.Trim();
                if (TryCode.IndexOf(",") >= 0)
                {
                    string[] TryCodeList = TryCode.Split(',');
                    for (int i = 0; i < TryCodeList.Length; i++)
                    {
                        TryList.Add(TryCodeList[i]);
                    }
                }
                else
                {
                    TryList.Add(TryCode);
                }
            }

            if (TryList.Count > 0)
            {
                for (int i = 0; i < TryList.Count; i++)
                {
                    OnWIPTRY onWIPTRY = facade.CreateNewOnWIPTRY();

                    onWIPTRY.TRYNO = TryList[i].ToString().ToUpper();
                    onWIPTRY.RunningCard = report.RunningCard;
                    onWIPTRY.RunningCardSequence = report.RunningCardSequence;
                    onWIPTRY.MOCode = report.MOCode;
                    onWIPTRY.ItemCode = report.ItemCode;
                    onWIPTRY.ResourceCode = report.ResourceCode;
                    onWIPTRY.OPCode = report.OPCode;
                    onWIPTRY.RouteCode = report.RouteCode;
                    onWIPTRY.ModelCode = report.ModelCode;
                    onWIPTRY.MaintainDate = report.MaintainDate;
                    onWIPTRY.MaintainTime = report.MaintainTime;
                    onWIPTRY.MaintainUser = report.MaintainUser;
                    onWIPTRY.ShiftTypeCode = report.ShiftTypeCode;
                    onWIPTRY.StepSequenceCode = report.StepSequenceCode;
                    onWIPTRY.SegmnetCode = report.SegmentCode;
                    onWIPTRY.ShiftCode = report.ShiftCode;
                    onWIPTRY.TimePeriodCode = report.TimePeriodCode;
                    onWIPTRY.MOSeq = report.MOSeq;  // Added by Icyer 2007/07/02

                    if (actionCheckStatus == null || actionCheckStatus.NeedUpdateSimulation == true)
                    {
                        facade.AddOnWIPTRY(onWIPTRY);
                    }
                    else
                    {
                        actionEventArgs.OnWIP.Add(onWIPTRY);
                    }
                }
            }
        }

    }
}
