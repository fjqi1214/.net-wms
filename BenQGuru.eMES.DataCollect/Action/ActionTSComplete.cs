#region system
using System;
using UserControl;
#endregion

#region project
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Domain.OQC;
#endregion

namespace BenQGuru.eMES.DataCollect.Action
{
    /// <summary>
    /// ActionTSComplete 的摘要说明。
    /// </summary>
    public class ActionTSComplete : IAction
    {
        private IDomainDataProvider _domainDataProvider = null;

        //		public ActionTSComplete()
        //		{
        //		}

        public ActionTSComplete(IDomainDataProvider domainDataProvider)
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

        #region IAction 成员

        /// <summary>
        /// 维修完成
        /// </summary>
        /// <param name="actionEventArgs"></param>
        /// <returns></returns>
        public Messages Execute(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //检查res在不在OPTS中
                // Modified By Hi1/Venus.Feng on 20080711 for Hisense Version : 对于自动作Reflow的动作来讲，不再Check Resource是否在回流的工序中
                if (((TSActionEventArgs)actionEventArgs).IgnoreResourceInOPTS != true)
                {
                    messages.AddMessages(dataCollect.CheckResourceInOperationTS(actionEventArgs));
                }
                // End Modified

                if (messages.IsSuccess())
                {
                    TSFacade tsFacade = new TSFacade(this.DataProvider);
                    //					if( !tsFacade.IsCardInTS(actionEventArgs.RunningCard))
                    //					{
                    //						messages.Add(new Message(MessageType.Error,"$CSError_Card_Not_In_TS"));
                    //					}
                    //					if(messages.IsSuccess())
                    //					{

                    //Laws Lu,2005/09/16,修改	逻辑调整P4.8
                    object obj = tsFacade.GetCardLastTSRecord(actionEventArgs.RunningCard);



                    if (obj == null)
                    {
                        messages.Add(new Message(MessageType.Error, "$CSError_Card_Not_In_TS"));
                        //messages.Add(new Message(MessageType.Error,"$CSError_Card_TSStatus_IsNot_TS"));
                    }
                    else
                    {
                        Domain.TS.TS ts = (Domain.TS.TS)obj;



                        if (ts.TSStatus == TSStatus.TSStatus_Scrap
                            || ts.TSStatus == TSStatus.TSStatus_Split
                            || ts.TSStatus == TSStatus.TSStatus_Reflow
                            || ts.TSStatus == TSStatus.TSStatus_Confirm)
                        {
                            messages.Add(new Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS $Current_Status $" + ts.TSStatus));
                        }


                        if (messages.IsSuccess())
                        {
                            //2006/11/17,Laws Lu add get DateTime from db Server
                            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                            //修改 Karron Qiu,2005-9-26
                            if (ts.FromInputType == TS.TSFacade.TSSource_OnWIP)//线上.必须是回流或者报废
                            {
                                if (((TSActionEventArgs)actionEventArgs).TSStatus == TSStatus.TSStatus_Complete)
                                {
                                    messages.Add(new Message(MessageType.Error, "$CSError_Please_Select_Reflow_OR_Scrap"));//请选择回流或者报废
                                }
                            }
                            else if (ts.FromInputType == TS.TSFacade.TSSource_TS)//离线. 不能回流
                            {
                                if (((TSActionEventArgs)actionEventArgs).TSStatus == TSStatus.TSStatus_Reflow)
                                {
                                    messages.Add(new Message(MessageType.Error, "$CSError_Offline_Cannot_Reflow"));//离线不能回流
                                }
                            }
                            else if (ts.FromInputType == TS.TSFacade.TSSource_RMA)//RMA. 不能回流
                            {
                                if (((TSActionEventArgs)actionEventArgs).TSStatus == TSStatus.TSStatus_Reflow)
                                {
                                    messages.Add(new Message(MessageType.Error, "$CSError_RMATS_Cannot_Reflow"));//RMA不能回流
                                }
                            }



                            //Laws Lu,2005/11/09,新增	记录ShiftDay
                            BaseSetting.BaseModelFacade dataModel = new BaseSetting.BaseModelFacade(this.DataProvider);
                            Domain.BaseSetting.Resource res = (Domain.BaseSetting.Resource)dataModel.GetResource(actionEventArgs.ResourceCode);
                            //onwip.SegmentCode				= productInfo.NowSimulationReport.SegmentCode;

                            BaseSetting.ShiftModelFacade shiftModel = new BaseSetting.ShiftModelFacade(this.DataProvider);
                            Domain.BaseSetting.TimePeriod period = (Domain.BaseSetting.TimePeriod)shiftModel.GetTimePeriod(res.ShiftTypeCode, Web.Helper.FormatHelper.TOTimeInt(dtNow));

                            int shiftDay = 0;
                            if (period == null)
                            {
                                throw new Exception("$OutOfPerid");
                            }

                            if (period.IsOverDate == Web.Helper.FormatHelper.TRUE_STRING)
                            {
                                if (period.TimePeriodBeginTime < period.TimePeriodEndTime)
                                {
                                    shiftDay = FormatHelper.TODateInt(dtNow.AddDays(-1));
                                }
                                else if (Web.Helper.FormatHelper.TOTimeInt(dtNow) < period.TimePeriodBeginTime)
                                {
                                    shiftDay = FormatHelper.TODateInt(dtNow.AddDays(-1));
                                }
                                else
                                {
                                    shiftDay = FormatHelper.TODateInt(dtNow);
                                }
                            }
                            else
                            {
                                shiftDay = FormatHelper.TODateInt(dtNow);
                            }

                            #region 报废
                            if (((TSActionEventArgs)actionEventArgs).TSStatus == TSStatus.TSStatus_Scrap)
                            {
                                if (ts.TSStatus == TSStatus.TSStatus_TS || ts.TSStatus == TSStatus.TSStatus_Confirm)
                                {
                                    ts.TSStatus = TSStatus.TSStatus_Scrap;
                                    ts.TSResourceCode = actionEventArgs.ResourceCode;
                                    ts.TSUser = actionEventArgs.UserCode;

                                    ts.TSDate = shiftDay;
                                    ts.TSTime = FormatHelper.TOTimeInt(dtNow);

                                    //added by jessie lee, 2005/11/24,
                                    //新增报废原因
                                    ts.ScrapCause = (actionEventArgs as TSActionEventArgs).ScrapCause;
                                    ts.MaintainUser = (actionEventArgs as TSActionEventArgs).MaintainUser;
                                    ts.MaintainDate = ts.TSDate;
                                    ts.MaintainTime = ts.TSTime;

                                    //TODO：Laws Lu,2005/11/09，需要优化
                                    tsFacade.UpdateTS(ts);

                                    //added by alex,2010/11/09
                                    BenQGuru.eMES.OQC.OQCFacade oqcFacade = new BenQGuru.eMES.OQC.OQCFacade(this.DataProvider);
                                    OQCLot2Card oqcLot2Card = oqcFacade.GetLastOQCLot2CardByRCard(actionEventArgs.RunningCard) as OQCLot2Card;
                                    if (oqcLot2Card != null)
                                    {
                                        oqcLot2Card.Status = "SCRAP";
                                        oqcFacade.UpdateOQCLot2Card(oqcLot2Card);
                                    }

                                    MOFacade moFAC = new MOFacade(this._domainDataProvider);

                                    if (ts.FromInputType == TSFacade.TSSource_OnWIP)
                                    {
                                        //Laws Lu,2005/08/19,新增
                                        //Laws Lu,2005/08/25,修改	处理报废时,更新工单的报废数量
                                        doAction(actionEventArgs);

                                    }
                                }
                                else
                                {
                                    messages.Add(new Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS_OR_Confirm"));
                                }

                            }
                            #endregion

                            #region 完成
                            if (((TSActionEventArgs)actionEventArgs).TSStatus == TSStatus.TSStatus_Complete)
                            {
                                if (ts.TSStatus == TSStatus.TSStatus_TS)
                                {
                                    ts.TSStatus = TSStatus.TSStatus_Complete;

                                    ts.TSTimes = 1;

                                    //added by jessie lee, 2005/11/24,
                                    ts.MaintainUser = (actionEventArgs as TSActionEventArgs).MaintainUser;
                                    ts.TSUser = actionEventArgs.UserCode;
                                    ts.MaintainDate = FormatHelper.TODateInt(dtNow);
                                    ts.MaintainTime = FormatHelper.TOTimeInt(dtNow);
                                    //Laws Lu,2006/04/28 add 添加维修shiftday和维修resourcecode
                                    ts.TSDate = shiftDay;
                                    ts.TSTime = FormatHelper.TOTimeInt(dtNow);
                                    ts.TSResourceCode = actionEventArgs.ResourceCode;


                                    tsFacade.UpdateTS(ts);

                                    // Added by Icyer 2006/11/07, KeyPart维修完成
                                    if (ts.CardType == CardType.CardType_Part && ts.FromInputType == TSFacade.TSSource_TS)
                                    {
                                        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                                        SimulationReport simRpt = dataCollectFacade.GetLastSimulationReport(ts.RunningCard);
                                        if (simRpt != null && simRpt.Status == ProductStatus.NG)
                                        {
                                            simRpt.Status = ProductStatus.GOOD;
                                            dataCollectFacade.UpdateSimulationReport(simRpt);
                                        }
                                    }
                                    // Added end
                                }
                                else
                                {
                                    messages.Add(new Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS $Current_Status $" + ts.TSStatus));
                                }
                            }
                            #endregion

                            #region 回流
                            if (((TSActionEventArgs)actionEventArgs).TSStatus == TSStatus.TSStatus_Reflow)
                            {
                                if (ts.TSStatus == TSStatus.TSStatus_TS)
                                {

                                    ts.TSStatus = TSStatus.TSStatus_Reflow;
                                    ts.ReflowMOCode = ((TSActionEventArgs)actionEventArgs).MOCode;
                                    //ts.ReflowResourceCode = ((TSActionEventArgs)actionEventArgs).ItemCode ;
                                    ts.ReflowRouteCode = ((TSActionEventArgs)actionEventArgs).RouteCode;
                                    ts.ReflowOPCode = ((TSActionEventArgs)actionEventArgs).OPCode;

                                    //added by jessie lee, 2005/11/24,
                                    //added by jessie lee, 2005/11/24,
                                    ts.MaintainUser = (actionEventArgs as TSActionEventArgs).MaintainUser;
                                    ts.TSUser = actionEventArgs.UserCode;
                                    ts.MaintainDate = FormatHelper.TODateInt(dtNow);
                                    ts.MaintainTime = FormatHelper.TOTimeInt(dtNow);

                                    //Laws Lu,2006/04/28 add 添加维修shiftday和维修resourcecode
                                    ts.TSDate = shiftDay;
                                    ts.TSTime = FormatHelper.TOTimeInt(dtNow);
                                    ts.TSResourceCode = actionEventArgs.ResourceCode;

                                    tsFacade.UpdateTSReflowStatus(ts);

                                }
                                else
                                {
                                    messages.Add(new Message(MessageType.Error, "$CSError_Card_TSStatus_IsNot_TS $Current_Status $" + ts.TSStatus));
                                }
                            }
                            #endregion
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
        //Laws Lu,2005/08/19,新增	完工逻辑
        //Laws Lu,2005/08/26,新增	更新工单报废数量
        private void doAction(ActionEventArgs e)
        {

            #region 填写Simulation
            //Laws Lu,2005/08/26,新增	更新工单中报废的数量

            DataCollectFacade fac = new DataCollectFacade(this.DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            Simulation sim = (Simulation)fac.GetSimulation(e.RunningCard.Trim());

            if (sim != null)
            {
                sim.IsComplete = "1";
                sim.EAttribute1 = "SCRAP";//TSStatus.TSStatus_Scrap 以Mail内容为准
                sim.ProductStatus = "SCRAP";
                sim.MaintainUser = e.UserCode.Trim();
                sim.MaintainDate = dbDateTime.DBDate;
                sim.MaintainTime = dbDateTime.DBTime;
            }

            fac.UpdateSimulation(sim);

            #endregion

            #region 填写SimulationReport

            SimulationReport simulationReport = new SimulationReport();
            simulationReport.RouteCode = sim.RouteCode;
            simulationReport.OPCode = sim.OPCode;
            simulationReport.CartonCode = sim.CartonCode;
            simulationReport.EAttribute1 = sim.EAttribute1;
            simulationReport.EAttribute2 = sim.EAttribute2;
            simulationReport.IDMergeRule = sim.IDMergeRule;
            simulationReport.IsComplete = sim.IsComplete;
            simulationReport.ItemCode = sim.ItemCode;
            simulationReport.LastAction = sim.LastAction;
            simulationReport.LOTNO = sim.LOTNO;
            simulationReport.MaintainDate = sim.MaintainDate;
            simulationReport.MaintainTime = sim.MaintainTime;
            simulationReport.MaintainUser = sim.MaintainUser;
            simulationReport.MOCode = sim.MOCode;
            simulationReport.ModelCode = sim.ModelCode;
            simulationReport.NGTimes = sim.NGTimes;
            simulationReport.PalletCode = sim.PalletCode;
            simulationReport.ResourceCode = sim.ResourceCode;
            simulationReport.RunningCard = sim.RunningCard;
            simulationReport.RunningCardSequence = sim.RunningCardSequence;
            simulationReport.Status = sim.ProductStatus;
            simulationReport.TranslateCard = sim.TranslateCard;
            simulationReport.TranslateCardSequence = sim.TranslateCardSequence;
            simulationReport.SourceCard = sim.SourceCard;
            simulationReport.SourceCardSequence = sim.SourceCardSequence;

            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            Resource resource = (Resource)dataModel.GetResource(sim.ResourceCode);
            simulationReport.SegmentCode = resource.SegmentCode;

            ShiftModelFacade shiftModel = new ShiftModelFacade(this.DataProvider);
            TimePeriod period = (TimePeriod)shiftModel.GetTimePeriod(resource.ShiftTypeCode, simulationReport.MaintainTime);
            if (period == null)
            {
                throw new Exception("$OutOfPerid");
            }

            // Modified by Jane Shu		Date:2005-07-26
            //			if ( period.IsOverDate == FormatHelper.TRUE_STRING )
            //			{
            //				if ( period.TimePeriodBeginTime < period.TimePeriodEndTime )
            //				{
            //					simulationReport.ShiftDay =	FormatHelper.TODateInt(DateTime.Now.AddDays(-1)) ;
            //				}
            //				else if ( sim.MaintainTime < period.TimePeriodBeginTime)
            //				{
            //					simulationReport.ShiftDay =	FormatHelper.TODateInt(DateTime.Now.AddDays(-1)) ;
            //				}
            //				else
            //				{
            //					simulationReport.ShiftDay = FormatHelper.TODateInt(DateTime.Now) ;
            //				}
            //			}
            //			else
            //			{
            //				simulationReport.ShiftDay = FormatHelper.TODateInt(DateTime.Now) ;
            //			}
            simulationReport.ShiftTypeCode = resource.ShiftTypeCode;
            simulationReport.ShiftCode = period.ShiftCode;
            simulationReport.TimePeriodCode = period.TimePeriodCode;
            simulationReport.StepSequenceCode = resource.StepSequenceCode;
            simulationReport.MOSeq = sim.MOSeq;     // Added by Icyer 2007/07/03

            fac.UpdateSimulationReport(simulationReport);

            #endregion

            #region 填写OnWIP
            //			OnWIP onwip	=	new OnWIP();
            //			onwip.Action	=	sim.LastAction;
            //			onwip.ActionResult	=	sim.ProductStatus;
            //			onwip.ItemCode		=	sim.ItemCode ;
            //			onwip.MaintainDate	=	FormatHelper.TODateInt(DateTime.Now) ;
            //			onwip.MaintainTime	=	FormatHelper.TOTimeInt(DateTime.Now) ;
            //			onwip.MaintainUser	=	sim.MaintainUser ;
            //			onwip.MOCode			=	sim.MOCode;
            //			onwip.ModelCode		=  sim.ModelCode;
            //			onwip.NGTimes			=  sim.NGTimes;
            //			onwip.OPCode			=  sim.OPCode;
            //			onwip.ResourceCode =  sim.ResourceCode;
            //			onwip.RouteCode		=  sim.RouteCode;
            //			onwip.RunningCard	=  sim.RunningCard;
            //			onwip.RunningCardSequence	= sim.RunningCardSequence ;
            //						
            ////			BaseModelFacade dataModel1 = new BaseModelFacade(this.DataProvider);
            ////			Resource resource1				= (Resource)dataModel1.GetResource(sim.ResourceCode);
            ////			onwip.SegmentCode				= resource1.SegmentCode ;
            ////						
            ////			ShiftModelFacade shiftModel1	= new ShiftModelFacade(this.DataProvider);
            ////			TimePeriod  period1				= (TimePeriod)shiftModel1.GetTimePeriod(resource1.ShiftTypeCode,onwip.MaintainTime);		
            ////			if (period1==null)
            ////			{
            ////				throw new Exception("$OutOfPerid");
            ////			}
            //							
            ////			if ( period1.IsOverDate == FormatHelper.TRUE_STRING )
            ////			{
            ////				if ( period1.TimePeriodBeginTime < period1.TimePeriodEndTime )
            ////				{
            ////					onwip.ShiftDay =	FormatHelper.TODateInt(DateTime.Now.AddDays(-1)) ;
            ////				}
            ////				else if ( sim.MaintainTime < period1.TimePeriodBeginTime)
            ////				{
            ////					onwip.ShiftDay =	FormatHelper.TODateInt(DateTime.Now.AddDays(-1)) ;
            ////				}
            ////				else
            ////				{
            ////					onwip.ShiftDay = FormatHelper.TODateInt(DateTime.Now) ;
            ////				}
            ////			}
            ////			else
            ////			{
            ////				onwip.ShiftDay = FormatHelper.TODateInt(DateTime.Now) ;
            ////			}
            ////			onwip.ShiftTypeCode	= resource1.ShiftTypeCode;
            ////			onwip.ShiftCode			= period1.ShiftCode;
            ////			onwip.TimePeriodCode	= period1.TimePeriodCode;
            //			
            //			onwip.SourceCard				  = sim.SourceCard;
            //			onwip.SourceCardSequence = sim.SourceCardSequence;
            ////			onwip.StepSequenceCode	  = resource1.StepSequenceCode;
            //			
            //			onwip.TranslateCard			= sim.TranslateCard;
            //			onwip.TranslateCardSequence = sim.TranslateCardSequence;
            //				
            //			fac.UpdateOnWIP(onwip);
            #endregion

            MOFacade moFAC = new MOFacade(_domainDataProvider);
            object objMO = null;
            if (e.CurrentMO != null)
            {
                objMO = e.CurrentMO;
            }
            else
            {
                objMO = moFAC.GetMO(sim.MOCode);
                e.CurrentMO = objMO as Domain.MOModel.MO;
            }



            if (objMO != null)
            {
                MO mo = (MO)objMO;

                //Laws Lu,2006/02/28,修改	报废数量
                mo.MOScrapQty = /*mo.MOScrapQty + */1 * sim.IDMergeRule;

                moFAC.UpdateMOScrapQty(mo);
            }
            //End Laws Lu

        }

        #endregion
    }
}
