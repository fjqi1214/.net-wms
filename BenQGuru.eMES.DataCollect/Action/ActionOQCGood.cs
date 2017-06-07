#region system;
using System;
using UserControl;
using System.Collections;
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
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.TS;
#endregion



namespace BenQGuru.eMES.DataCollect.Action
{
    /// <summary>
    /// 归属工单采集
    /// </summary>
    public class ActionOQCGood : IActionWithStatus
    {
        private IDomainDataProvider _domainDataProvider = null;

        //		public ActionOQCGood()
        //		{	
        //		}

        public ActionOQCGood(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                //				if (_domainDataProvider == null)
                //				{
                //					_domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                //				}

                return _domainDataProvider;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oqcGoodEventArgs"> </param> params (0,lotno)
        /// <returns></returns>
        public Messages Execute(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            OQCGoodEventArgs oqcGoodEventArgs = actionEventArgs as OQCGoodEventArgs;

            //added by hiro.chen 08/11/18 :判断是否已下地
            DataCollectFacade dcFacade = new DataCollectFacade(this.DataProvider);
            messages.AddMessages(dcFacade.CheckISDown(actionEventArgs.RunningCard));
            if (!messages.IsSuccess())
            {
                return messages;
            }
            //end 

            try
            {
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                ActionOQCHelper oqcHelper = new ActionOQCHelper(this.DataProvider);
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);

                // Added By Hi1/Venus.feng on 20080720 for Hisense Version : Change OQC Flow
                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                // Some Check
                #region CheckOQCLotStatus
                /*
                object objOQClot = oqcFacade.GetOQCLot(oqcGoodEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                if (objOQClot == null)
                {
                    throw new Exception("$Error_OQCLotNotExisted");
                }
                if (((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Initial)
                {
                    throw new Exception("$Error_OQCLotNO_Cannot_Initial");
                }
                if ((((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Reject) || ((((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Pass)))
                {
                    throw new Exception("$Error_OQCLotNO_HasComplete");
                }
                */
                #endregion

                // Update tbloqc.status
                #region 修改OQCLot状态
                OQCLot oqcLot = new OQCLot();
                oqcLot.LOTNO = oqcGoodEventArgs.OQCLotNO;
                oqcLot.LotSequence = OQCFacade.Lot_Sequence_Default;
                oqcLot.LOTStatus = Web.Helper.OQCLotStatus.OQCLotStatus_Examing;
                oqcFacade.UpdateOQCLotStatus(oqcLot);
                #endregion

                // Update tbloqccardlotcklist
                //Maybe need check????
                #region OQCLOTCardCheckList
                if (oqcGoodEventArgs.OQCLOTCardCheckLists != null)
                {
                    OQCLOTCardCheckList oqcLotCardCheckList;
                    for (int i = 0; i < oqcGoodEventArgs.OQCLOTCardCheckLists.Length; i++)
                    {
                        if (oqcGoodEventArgs.OQCLOTCardCheckLists[i] != null)
                        {
                            oqcLotCardCheckList = oqcGoodEventArgs.OQCLOTCardCheckLists[i] as OQCLOTCardCheckList;
                            oqcLotCardCheckList.MaintainDate = currentDateTime.DBDate;
                            oqcLotCardCheckList.MaintainTime = currentDateTime.DBTime;
                            oqcFacade.AddOQCLOTCardCheckList(oqcLotCardCheckList);
                        }
                    }
                }
                #endregion

                // Update tbloqclotcklist
                #region OQCLOTCheckList
                object obj = oqcFacade.GetOQCLOTCheckList(oqcGoodEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                if (obj == null)
                {
                    OQCLOTCheckList oqcCheckList = oqcFacade.CreateNewOQCLOTCheckList();
                    oqcCheckList.AGradeTimes = OQCFacade.Decimal_Default_value;
                    oqcCheckList.BGradeTimes = OQCFacade.Decimal_Default_value;
                    oqcCheckList.CGradeTimes = OQCFacade.Decimal_Default_value;
                    oqcCheckList.ZGradeTimes = OQCFacade.Decimal_Default_value;
                    oqcCheckList.LOTNO = oqcGoodEventArgs.OQCLotNO;
                    oqcCheckList.LotSequence = OQCFacade.Lot_Sequence_Default;
                    oqcCheckList.MaintainUser = oqcGoodEventArgs.UserCode;
                    oqcCheckList.MaintainDate = currentDateTime.DBDate;
                    oqcCheckList.MaintainTime = currentDateTime.DBTime;
                    oqcCheckList.Result = OQCLotStatus.OQCLotStatus_Examing;

                    oqcFacade.AddOQCLOTCheckList(GetDefectStatic(oqcGoodEventArgs.OQCLOTCardCheckLists, oqcCheckList));
                }
                else
                {
                    OQCLOTCheckList oqcCheckList = obj as OQCLOTCheckList;
                    oqcCheckList.MaintainUser = oqcGoodEventArgs.UserCode;

                    oqcFacade.UpdateOQCLOTCheckList(GetDefectStatic(oqcGoodEventArgs.OQCLOTCardCheckLists, oqcCheckList));
                }
                #endregion

                // Update tbllot2cardcheck
                #region OQCLot2CardCheck

                OQCLot2CardCheck oqcLot2CardCheck = oqcFacade.CreateNewOQCLot2CardCheck();
                oqcLot2CardCheck.ItemCode = oqcGoodEventArgs.ProductInfo.LastSimulation.ItemCode;
                oqcLot2CardCheck.LOTNO = oqcGoodEventArgs.OQCLotNO;
                oqcLot2CardCheck.MaintainUser = oqcGoodEventArgs.UserCode;
                oqcLot2CardCheck.MOCode = oqcGoodEventArgs.ProductInfo.LastSimulation.MOCode;
                oqcLot2CardCheck.ModelCode = oqcGoodEventArgs.ProductInfo.LastSimulation.ModelCode;
                oqcLot2CardCheck.OPCode = oqcGoodEventArgs.ProductInfo.LastSimulation.OPCode;
                oqcLot2CardCheck.ResourceCode = oqcGoodEventArgs.ResourceCode;
                oqcLot2CardCheck.RouteCode = oqcGoodEventArgs.ProductInfo.LastSimulation.RouteCode;
                oqcLot2CardCheck.RunningCard = oqcGoodEventArgs.ProductInfo.LastSimulation.RunningCard;
                oqcLot2CardCheck.RunningCardSequence = oqcGoodEventArgs.ProductInfo.LastSimulation.RunningCardSequence;

                BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
                Resource resource = (Resource)dataModel.GetResource(oqcGoodEventArgs.ResourceCode);

                oqcLot2CardCheck.SegmnetCode = resource.SegmentCode;

                TimePeriod period = oqcGoodEventArgs.ProductInfo.TimePeriod;
                if (period == null)
                {
                    ShiftModelFacade shiftModel = new ShiftModelFacade(this.DataProvider);
                    period = (TimePeriod)shiftModel.GetTimePeriod(resource.ShiftTypeCode, currentDateTime.DBTime);
                    if (period == null)
                    {
                        throw new Exception("$OutOfPerid");
                    }
                }
                oqcLot2CardCheck.ShiftCode = period.ShiftCode;
                oqcLot2CardCheck.ShiftTypeCode = resource.ShiftTypeCode;
                oqcLot2CardCheck.Status = ProductStatus.GOOD;
                oqcLot2CardCheck.StepSequenceCode = resource.StepSequenceCode;
                oqcLot2CardCheck.TimePeriodCode = period.TimePeriodCode;

                oqcLot2CardCheck.IsDataLink = oqcGoodEventArgs.IsDataLink;
                oqcLot2CardCheck.MaintainDate = currentDateTime.DBDate;
                oqcLot2CardCheck.MaintainTime = currentDateTime.DBTime;
                oqcLot2CardCheck.CheckSequence = oqcGoodEventArgs.CheckSequence;
                oqcLot2CardCheck.EAttribute1 = oqcGoodEventArgs.Memo;
                oqcFacade.AddOQCLot2CardCheck(oqcLot2CardCheck);

                #endregion

                // Good don't need update lot2errorcode or lotcard2errorcode
                ////// Update tbloqclot2errorcode
                ////// Update tbloqclotcard2errorcode

                messages.Add(new Message(MessageType.Success, "$CS_SampleConfirmOK"));
                // End Added

                #region Marked By Hi1/Venus.Feng on 20080720 for Hisense Version : Change OQC Flow
                /*
				//填写SIMULATION 检查工单、ID、途程、操作
				messages.AddMessages( dataCollect.CheckID(oqcGoodEventArgs));
				if (messages.IsSuccess())
				{				
					//
					if(oqcGoodEventArgs.ProductInfo.NowSimulation == null)
					{
						throw new Exception("$System_Error");
					}
					//check oqclotstatus
					#region CheckOQCLotStatus
//					object objOQClot = oqcFacade.GetOQCLot(oqcGoodEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default);
//					if(objOQClot == null)
//					{
//						throw new Exception("$Error_OQCLotNotExisted");
//					}
//					if( ((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Initial)
//					{
//						throw new Exception("$Error_OQCLotNO_Cannot_Initial");
//					}
//					if( (((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Reject)||((((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Pass) ) )
//					{
//						throw new Exception("$Error_OQCLotNO_HasComplete");
//					}
					#endregion
			

					messages.AddMessages( dataCollect.Execute(oqcGoodEventArgs));
					if (messages.IsSuccess())
					{
						//判断是否第一笔，如果是修改oqclot
						#region 修改OQCLot状态
						//						object[] objs = oqcFacade.ExtraQueryOQCLot2CardCheck(string.Empty,string.Empty,oqcGoodEventArgs.ProductInfo.NowSimulation.MOCode,oqcGoodEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default.ToString());
						//						if(objs == null)
						//						{
						//object objLot = oqcFacade.GetOQCLot(oqcGoodEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default);
						OQCLot oqcLot = new OQCLot();
						oqcLot.LOTNO = oqcGoodEventArgs.OQCLotNO;
						oqcLot.LotSequence = OQCFacade.Lot_Sequence_Default;
						oqcLot.LOTStatus = Web.Helper.OQCLotStatus.OQCLotStatus_Examing;
						oqcFacade.UpdateOQCLotStatus(oqcLot);
						//						}
						#endregion

						//add recrod to OQCLot2CardCheck
						#region OQCLot2CardCheck
						OQCLot2CardCheck oqcLot2CardCheck = oqcFacade.CreateNewOQCLot2CardCheck();
						oqcLot2CardCheck.ItemCode = oqcGoodEventArgs.ProductInfo.NowSimulation.ItemCode;
						oqcLot2CardCheck.LOTNO = oqcGoodEventArgs.OQCLotNO;
						oqcLot2CardCheck.MaintainUser = oqcGoodEventArgs.UserCode;
						oqcLot2CardCheck.MOCode = oqcGoodEventArgs.ProductInfo.NowSimulation.MOCode;
						oqcLot2CardCheck.ModelCode = oqcGoodEventArgs.ProductInfo.NowSimulation.ModelCode;
						oqcLot2CardCheck.OPCode = oqcGoodEventArgs.ProductInfo.NowSimulation.OPCode;
						oqcLot2CardCheck.ResourceCode = oqcGoodEventArgs.ProductInfo.NowSimulation.ResourceCode;
						oqcLot2CardCheck.RouteCode = oqcGoodEventArgs.ProductInfo.NowSimulation.RouteCode;
						oqcLot2CardCheck.RunningCard = oqcGoodEventArgs.ProductInfo.NowSimulation.RunningCard;
						oqcLot2CardCheck.RunningCardSequence = oqcGoodEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
						oqcLot2CardCheck.SegmnetCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
						oqcLot2CardCheck.ShiftCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftCode;
						oqcLot2CardCheck.ShiftTypeCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftTypeCode;
						oqcLot2CardCheck.Status =  ProductStatus.GOOD;
						oqcLot2CardCheck.StepSequenceCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
						oqcLot2CardCheck.TimePeriodCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode;
						
						oqcLot2CardCheck.IsDataLink = oqcGoodEventArgs.IsDataLink; //标识样本是不是来自数据连线 joe song 2006-06-08
						oqcLot2CardCheck.EAttribute1 = (oqcGoodEventArgs as OQCGoodEventArgs).Memo;//Laws Lu,2006/07/12 add memo field

						DBDateTime dbDateTime;
						//Laws Lu,2006/11/13 uniform system collect date
						if(actionEventArgs.ProductInfo.WorkDateTime != null)
						{
							dbDateTime = actionEventArgs.ProductInfo.WorkDateTime;
							
						}
						else
						{
							dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
							actionEventArgs.ProductInfo.WorkDateTime = dbDateTime;
						}

						oqcLot2CardCheck.MaintainDate = dbDateTime.DBDate;
						oqcLot2CardCheck.MaintainTime = dbDateTime.DBTime;
						oqcFacade.AddOQCLot2CardCheck(oqcLot2CardCheck);
						#endregion

						//OQCLOTCardCheckList

						#region OQCLOTCardCheckList
						if( oqcGoodEventArgs.OQCLOTCardCheckLists != null)
						{
							for(int i=0;i<oqcGoodEventArgs.OQCLOTCardCheckLists.Length;i++)
							{
								if( oqcGoodEventArgs.OQCLOTCardCheckLists[i] != null)
								{
									OQCLOTCardCheckList objCardCheck = oqcGoodEventArgs.OQCLOTCardCheckLists[i] as OQCLOTCardCheckList;
									objCardCheck.RunningCardSequence = oqcGoodEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
									oqcFacade.AddOQCLOTCardCheckList(objCardCheck);
								}
							}
						}
						#endregion

						//OQCLOTCheckList 统计
						#region OQCLOTCheckList
						
				
						object obj = oqcFacade.GetOQCLOTCheckList(oqcGoodEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default);
						if(obj == null)
						{
							OQCLOTCheckList oqcCheckList = oqcFacade.CreateNewOQCLOTCheckList();
							oqcCheckList.AGradeTimes = OQCFacade.Decimal_Default_value;
							oqcCheckList.BGradeTimes = OQCFacade.Decimal_Default_value;
							oqcCheckList.CGradeTimes = OQCFacade.Decimal_Default_value;
							oqcCheckList.LOTNO = oqcGoodEventArgs.OQCLotNO;
							oqcCheckList.LotSequence = OQCFacade.Lot_Sequence_Default;
							oqcCheckList.MaintainUser = oqcGoodEventArgs.UserCode;
							oqcCheckList.MaintainDate = dbDateTime.DBDate;
							oqcCheckList.MaintainTime = dbDateTime.DBTime;
							oqcCheckList.Result = OQCLotStatus.OQCLotStatus_Examing;

							oqcFacade.AddOQCLOTCheckList(GetDefectStatic(oqcGoodEventArgs.OQCLOTCardCheckLists,oqcCheckList));
						}
						else
						{
							OQCLOTCheckList oqcCheckList = obj as OQCLOTCheckList;
							oqcCheckList.MaintainUser = oqcGoodEventArgs.UserCode;

							oqcFacade.UpdateOQCLOTCheckList(GetDefectStatic(oqcGoodEventArgs.OQCLOTCardCheckLists,oqcCheckList));
						}
						#endregion

						//						#region this is for report add by crystal chu 2005/07/25
						//						ReportHelper reportCollect= new ReportHelper(this.DataProvider);
						//						messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider,oqcGoodEventArgs.ActionType,oqcGoodEventArgs.ProductInfo));
						//						#endregion

					
					}
				}
                */

                #endregion
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }
        public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            OQCGoodEventArgs oqcGoodEventArgs = actionEventArgs as OQCGoodEventArgs;

            //added by hiro.chen 08/11/18 :判断是否已下地
            DataCollectFacade dcFacade = new DataCollectFacade(this.DataProvider);
            messages.AddMessages(dcFacade.CheckISDown(actionEventArgs.RunningCard));
            if (!messages.IsSuccess())
            {
                return messages;
            }
            //end 

            try
            {
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                ActionOQCHelper oqcHelper = new ActionOQCHelper(this.DataProvider);
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);

                // Added By Hi1/Venus.feng on 20080720 for Hisense Version : Change OQC Flow
                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                // Some Check
                #region CheckOQCLotStatus
                object objOQClot = oqcFacade.GetOQCLot(oqcGoodEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                if (objOQClot == null)
                {
                    throw new Exception("$Error_OQCLotNotExisted");
                }
                if (((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Initial)
                {
                    throw new Exception("$Error_OQCLotNO_Cannot_Initial");
                }
                if ((((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Reject) || ((((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Pass)))
                {
                    throw new Exception("$Error_OQCLotNO_HasComplete");
                }
                #endregion

                // Update tbloqc.status
                #region 修改OQCLot状态
                OQCLot oqcLot = new OQCLot();
                oqcLot.LOTNO = oqcGoodEventArgs.OQCLotNO;
                oqcLot.LotSequence = OQCFacade.Lot_Sequence_Default;
                oqcLot.LOTStatus = Web.Helper.OQCLotStatus.OQCLotStatus_Examing;
                oqcFacade.UpdateOQCLotStatus(oqcLot);
                #endregion

                // Update tbloqccardlotcklist
                #region OQCLOTCardCheckList
                if (oqcGoodEventArgs.OQCLOTCardCheckLists != null)
                {
                    OQCLOTCardCheckList oqcLotCardCheckList;
                    for (int i = 0; i < oqcGoodEventArgs.OQCLOTCardCheckLists.Length; i++)
                    {
                        if (oqcGoodEventArgs.OQCLOTCardCheckLists[i] != null)
                        {
                            oqcLotCardCheckList = oqcGoodEventArgs.OQCLOTCardCheckLists[i] as OQCLOTCardCheckList;
                            oqcLotCardCheckList.MaintainDate = currentDateTime.DBDate;
                            oqcLotCardCheckList.MaintainTime = currentDateTime.DBTime;
                            oqcFacade.AddOQCLOTCardCheckList(oqcLotCardCheckList);
                        }
                    }
                }
                #endregion

                // Update tbloqclotcklist
                #region OQCLOTCheckList
                object obj = oqcFacade.GetOQCLOTCheckList(oqcGoodEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                if (obj == null)
                {
                    OQCLOTCheckList oqcCheckList = oqcFacade.CreateNewOQCLOTCheckList();
                    oqcCheckList.AGradeTimes = OQCFacade.Decimal_Default_value;
                    oqcCheckList.BGradeTimes = OQCFacade.Decimal_Default_value;
                    oqcCheckList.CGradeTimes = OQCFacade.Decimal_Default_value;
                    oqcCheckList.ZGradeTimes = OQCFacade.Decimal_Default_value;
                    oqcCheckList.LOTNO = oqcGoodEventArgs.OQCLotNO;
                    oqcCheckList.LotSequence = OQCFacade.Lot_Sequence_Default;
                    oqcCheckList.MaintainUser = oqcGoodEventArgs.UserCode;
                    oqcCheckList.MaintainDate = currentDateTime.DBDate;
                    oqcCheckList.MaintainTime = currentDateTime.DBTime;
                    oqcCheckList.Result = OQCLotStatus.OQCLotStatus_Examing;

                    oqcFacade.AddOQCLOTCheckList(GetDefectStatic(oqcGoodEventArgs.OQCLOTCardCheckLists, oqcCheckList));
                }
                else
                {
                    OQCLOTCheckList oqcCheckList = obj as OQCLOTCheckList;
                    oqcCheckList.MaintainUser = oqcGoodEventArgs.UserCode;

                    oqcFacade.UpdateOQCLOTCheckList(GetDefectStatic(oqcGoodEventArgs.OQCLOTCardCheckLists, oqcCheckList));
                }
                #endregion

                // Update tbllot2cardcheck
                #region OQCLot2CardCheck

                OQCLot2CardCheck oqcLot2CardCheck = oqcFacade.CreateNewOQCLot2CardCheck();
                oqcLot2CardCheck.ItemCode = oqcGoodEventArgs.ProductInfo.LastSimulation.ItemCode;
                oqcLot2CardCheck.LOTNO = oqcGoodEventArgs.OQCLotNO;
                oqcLot2CardCheck.MaintainUser = oqcGoodEventArgs.UserCode;
                oqcLot2CardCheck.MOCode = oqcGoodEventArgs.ProductInfo.LastSimulation.MOCode;
                oqcLot2CardCheck.ModelCode = oqcGoodEventArgs.ProductInfo.LastSimulation.ModelCode;
                oqcLot2CardCheck.OPCode = oqcGoodEventArgs.ProductInfo.LastSimulation.OPCode;
                oqcLot2CardCheck.ResourceCode = oqcGoodEventArgs.ResourceCode;
                oqcLot2CardCheck.RouteCode = oqcGoodEventArgs.ProductInfo.LastSimulation.RouteCode;
                oqcLot2CardCheck.RunningCard = oqcGoodEventArgs.ProductInfo.LastSimulation.RunningCard;
                oqcLot2CardCheck.RunningCardSequence = oqcGoodEventArgs.ProductInfo.LastSimulation.RunningCardSequence;

                BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
                Resource resource = (Resource)dataModel.GetResource(oqcGoodEventArgs.ResourceCode);

                oqcLot2CardCheck.SegmnetCode = resource.SegmentCode;

                TimePeriod period = oqcGoodEventArgs.ProductInfo.TimePeriod;
                if (period == null)
                {
                    ShiftModelFacade shiftModel = new ShiftModelFacade(this.DataProvider);
                    period = (TimePeriod)shiftModel.GetTimePeriod(resource.ShiftTypeCode, currentDateTime.DBTime);
                    if (period == null)
                    {
                        throw new Exception("$OutOfPerid");
                    }
                }
                oqcLot2CardCheck.ShiftCode = period.ShiftCode;
                oqcLot2CardCheck.ShiftTypeCode = resource.ShiftTypeCode;
                oqcLot2CardCheck.Status = ProductStatus.GOOD;
                oqcLot2CardCheck.StepSequenceCode = resource.StepSequenceCode;
                oqcLot2CardCheck.TimePeriodCode = period.TimePeriodCode;

                oqcLot2CardCheck.IsDataLink = oqcGoodEventArgs.IsDataLink;
                oqcLot2CardCheck.MaintainDate = currentDateTime.DBDate;
                oqcLot2CardCheck.MaintainTime = currentDateTime.DBTime;
                oqcLot2CardCheck.CheckSequence = oqcGoodEventArgs.CheckSequence;
                oqcLot2CardCheck.EAttribute1 = oqcGoodEventArgs.Memo;
                oqcFacade.AddOQCLot2CardCheck(oqcLot2CardCheck);

                #endregion

                // Good don't need update lot2errorcode or lotcard2errorcode
                ////// Update tbloqclot2errorcode
                ////// Update tbloqclotcard2errorcode

                messages.Add(new Message(MessageType.Success, "$CS_SampleConfirmOK"));
                // End Added

                #region Marked by hi1/Venus.Feng on 20080720 for Hisense Version : Change OQC Flow
                /*
				//填写SIMULATION 检查工单、ID、途程、操作
				messages.AddMessages( dataCollect.CheckID(oqcGoodEventArgs));
				if (messages.IsSuccess())
				{				
					//
					if(oqcGoodEventArgs.ProductInfo.NowSimulation == null)
					{
						throw new Exception("$System_Error");
					}
					//check oqclotstatus
					#region CheckOQCLotStatus
					object objOQClot = oqcFacade.GetOQCLot(oqcGoodEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default);
					if(objOQClot == null)
					{
						throw new Exception("$Error_OQCLotNotExisted");
					}
					if( ((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Initial)
					{
						throw new Exception("$Error_OQCLotNO_Cannot_Initial");
					}
					if( (((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Reject)||((((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Pass) ) )
					{
						throw new Exception("$Error_OQCLotNO_HasComplete");
					}
					#endregion
			

					if (actionCheckStatus.NeedUpdateSimulation == true)
					{
						messages.AddMessages( dataCollect.Execute(oqcGoodEventArgs));
					}
					if (messages.IsSuccess())
					{
						DBDateTime dbDateTime;
						//Laws Lu,2006/11/13 uniform system collect date
						if(actionEventArgs.ProductInfo.WorkDateTime != null)
						{
							dbDateTime = actionEventArgs.ProductInfo.WorkDateTime;
							
						}
						else
						{
							dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
							actionEventArgs.ProductInfo.WorkDateTime = dbDateTime;
						}

						//判断是否第一笔，如果是修改oqclot
						#region 修改OQCLot状态
						//						object[] objs = oqcFacade.ExtraQueryOQCLot2CardCheck(string.Empty,string.Empty,oqcGoodEventArgs.ProductInfo.NowSimulation.MOCode,oqcGoodEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default.ToString());
						//						if(objs == null)
						//						{
						//object objLot = oqcFacade.GetOQCLot(oqcGoodEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default);
						OQCLot oqcLot = new OQCLot();
						oqcLot.LOTNO = oqcGoodEventArgs.OQCLotNO;
						oqcLot.LotSequence = OQCFacade.Lot_Sequence_Default;
						oqcLot.LOTStatus = Web.Helper.OQCLotStatus.OQCLotStatus_Examing;
						oqcFacade.UpdateOQCLotStatus(oqcLot);
						//						}
						#endregion

						//add recrod to OQCLot2CardCheck
						#region OQCLot2CardCheck
						OQCLot2CardCheck oqcLot2CardCheck = oqcFacade.CreateNewOQCLot2CardCheck();
						oqcLot2CardCheck.ItemCode = oqcGoodEventArgs.ProductInfo.NowSimulation.ItemCode;
						oqcLot2CardCheck.LOTNO = oqcGoodEventArgs.OQCLotNO;
						oqcLot2CardCheck.MaintainUser = oqcGoodEventArgs.UserCode;
						oqcLot2CardCheck.MOCode = oqcGoodEventArgs.ProductInfo.NowSimulation.MOCode;
						oqcLot2CardCheck.ModelCode = oqcGoodEventArgs.ProductInfo.NowSimulation.ModelCode;
						oqcLot2CardCheck.OPCode = oqcGoodEventArgs.ProductInfo.NowSimulation.OPCode;
						oqcLot2CardCheck.ResourceCode = oqcGoodEventArgs.ProductInfo.NowSimulation.ResourceCode;
						oqcLot2CardCheck.RouteCode = oqcGoodEventArgs.ProductInfo.NowSimulation.RouteCode;
						oqcLot2CardCheck.RunningCard = oqcGoodEventArgs.ProductInfo.NowSimulation.RunningCard;
						oqcLot2CardCheck.RunningCardSequence = oqcGoodEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
						oqcLot2CardCheck.SegmnetCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
						oqcLot2CardCheck.ShiftCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftCode;
						oqcLot2CardCheck.ShiftTypeCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftTypeCode;
						oqcLot2CardCheck.Status =  ProductStatus.GOOD;
						oqcLot2CardCheck.StepSequenceCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
						oqcLot2CardCheck.TimePeriodCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode;

						oqcLot2CardCheck.IsDataLink = oqcGoodEventArgs.IsDataLink; //标识样本是不是来自数据连线 joe song 2006-06-08

						oqcLot2CardCheck.MaintainDate = dbDateTime.DBDate;
						oqcLot2CardCheck.MaintainTime = dbDateTime.DBTime;
						oqcFacade.AddOQCLot2CardCheck(oqcLot2CardCheck);
						#endregion

						//OQCLOTCardCheckList

						#region OQCLOTCardCheckList
						if( oqcGoodEventArgs.OQCLOTCardCheckLists != null)
						{
							for(int i=0;i<oqcGoodEventArgs.OQCLOTCardCheckLists.Length;i++)
							{
								if( oqcGoodEventArgs.OQCLOTCardCheckLists[i] != null)
								{
									oqcFacade.AddOQCLOTCardCheckList(oqcGoodEventArgs.OQCLOTCardCheckLists[i] as OQCLOTCardCheckList);
								}
							}
						}
						#endregion

						//OQCLOTCheckList 统计
						#region OQCLOTCheckList
						
				
						object obj = oqcFacade.GetOQCLOTCheckList(oqcGoodEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default);
						if(obj == null)
						{
							OQCLOTCheckList oqcCheckList = oqcFacade.CreateNewOQCLOTCheckList();
							oqcCheckList.AGradeTimes = OQCFacade.Decimal_Default_value;
							oqcCheckList.BGradeTimes = OQCFacade.Decimal_Default_value;
							oqcCheckList.CGradeTimes = OQCFacade.Decimal_Default_value;
							oqcCheckList.LOTNO = oqcGoodEventArgs.OQCLotNO;
							oqcCheckList.LotSequence = OQCFacade.Lot_Sequence_Default;
							oqcCheckList.MaintainUser = oqcGoodEventArgs.UserCode;
							oqcCheckList.MaintainDate = dbDateTime.DBDate;
							oqcCheckList.MaintainTime = dbDateTime.DBTime;
							oqcCheckList.Result = OQCLotStatus.OQCLotStatus_Examing;

							oqcFacade.AddOQCLOTCheckList(GetDefectStatic(oqcGoodEventArgs.OQCLOTCardCheckLists,oqcCheckList));
						}
						else
						{
							OQCLOTCheckList oqcCheckList = obj as OQCLOTCheckList;
							oqcCheckList.MaintainUser = oqcGoodEventArgs.UserCode;

							oqcFacade.UpdateOQCLOTCheckList(GetDefectStatic(oqcGoodEventArgs.OQCLOTCardCheckLists,oqcCheckList));
						}
						#endregion

						//						#region this is for report add by crystal chu 2005/07/25
						//						ReportHelper reportCollect= new ReportHelper(this.DataProvider);
						//						messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider,oqcGoodEventArgs.ActionType,oqcGoodEventArgs.ProductInfo));
						//						#endregion

					
					}
				}
                */

                #endregion

            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }



        #region private method
        private OQCLOTCheckList GetDefectStatic(object[] objsCheckItems, OQCLOTCheckList oqcCheckList)
        {
            if (objsCheckItems != null)
            {
                for (int i = 0; i < objsCheckItems.Length; i++)
                {
                    if (((OQCLOTCardCheckList)objsCheckItems[i]).Grade == OQCFacade.OQC_AGrade)
                    {
                        oqcCheckList.AGradeTimes = oqcCheckList.AGradeTimes + 1;
                    }
                    if (((OQCLOTCardCheckList)objsCheckItems[i]).Grade == OQCFacade.OQC_BGrade)
                    {
                        oqcCheckList.BGradeTimes = oqcCheckList.BGradeTimes + 1;
                    }
                    if (((OQCLOTCardCheckList)objsCheckItems[i]).Grade == OQCFacade.OQC_CGrade)
                    {
                        oqcCheckList.CGradeTimes = oqcCheckList.CGradeTimes + 1;
                    }
                    if (((OQCLOTCardCheckList)objsCheckItems[i]).Grade == OQCFacade.OQC_ZGrade)
                    {
                        oqcCheckList.ZGradeTimes = oqcCheckList.ZGradeTimes + 1;
                    }
                }
            }
            return oqcCheckList;
        }
        #endregion
    }
}
