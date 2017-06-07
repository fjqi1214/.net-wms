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
    public class ActionOQCNG : IActionWithStatus
    {
        private IDomainDataProvider _domainDataProvider = null;

        public ActionOQCNG(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionEventArgs"> </param> params (0,lotno)
        /// <returns></returns>
        public Messages Execute(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            OQCNGEventArgs oqcNGEventArgs = actionEventArgs as OQCNGEventArgs;

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
                TSFacade tsFacade = new TSFacade(this.DataProvider);
                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                //check oqclotstatus
                #region CheckOQCLotStatus
                object objOQClot = oqcFacade.GetOQCLot(oqcNGEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
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

                // Added By hi1/venus.Feng on 20080720 for Hisense Version : Change OQC Flow
                /// 最后一次过账的信息如果是NG，则判断其TS状态
                if (actionEventArgs.ProductInfo.LastSimulation.ProductStatus == ProductStatus.NG)
                {
                    // Check TS Status                    
                    object objTs = tsFacade.GetCardLastTSRecord(actionEventArgs.ProductInfo.LastSimulation.RunningCard);
                    if (objTs != null)
                    {
                        Domain.TS.TS ts = objTs as Domain.TS.TS;
                        if (ts.TSStatus == TSStatus.TSStatus_Complete
                            || ts.TSStatus == TSStatus.TSStatus_Reflow)
                        {
                            // 过账
                            messages.AddMessages(dataCollect.CheckID(oqcNGEventArgs));
                            if (messages.IsSuccess())
                            {
                                //
                                if (actionEventArgs.ProductInfo.NowSimulation == null)
                                {
                                    throw new Exception("$System_Error");
                                }
                                // Change the op to FQC OP
                                Operation2Resource op2Res = (new BaseModelFacade(this.DataProvider)).GetOperationByResource(actionEventArgs.ProductInfo.NowSimulation.ResourceCode);
                                actionEventArgs.ProductInfo.NowSimulation.OPCode = op2Res.OPCode;

                                actionEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.NG;
                                actionEventArgs.ProductInfo.NowSimulation.NGTimes = oqcNGEventArgs.ProductInfo.NowSimulation.NGTimes + 1;

                                messages.AddMessages(dataCollect.Execute(oqcNGEventArgs));
                                if (messages.IsSuccess())
                                {
                                    // 产生TS资料 和 TSErrorCode资料等
                                    this.GenerateNewTSInfo(oqcNGEventArgs);

                                    // 更新相关Lot的表
                                    this.UpdateOQCLotStatus(oqcNGEventArgs);
                                    this.UpdateLotCheckList(oqcNGEventArgs, currentDateTime);
                                    this.UpdateOQCLot2CardCheck(oqcNGEventArgs, currentDateTime, true);
                                    //this.UpdateOQCLot2CheckGroup(oqcNGEventArgs, currentDateTime);
                                    //this.UpdateOQCLot2ErrorCode(oqcNGEventArgs, currentDateTime);
                                    this.UpdateOQCLotCard2ErrorCode(oqcNGEventArgs, currentDateTime, true);
                                    this.UpdateOQCLotCardCheckList(oqcNGEventArgs, currentDateTime, true);

                                    // Report
                                    messages.AddMessages(this.GenerateReport(oqcNGEventArgs));
                                }
                            }
                        }
                        else
                        {
                            if (ts.TSStatus == TSStatus.TSStatus_New)
                            {
                                // Add new TSErrorCode to tbltserrorcode
                                BenQGuru.eMES.Domain.TS.TSErrorCode tsErrorCode = tsFacade.CreateNewTSErrorCode();
                                if (oqcNGEventArgs.ErrorCodeInformations != null)
                                {
                                    for (int i = 0; i < oqcNGEventArgs.ErrorCodeInformations.Length; i++)
                                    {
                                        tsErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCode;
                                        tsErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCodeGroup;
                                        tsErrorCode.ItemCode = ts.ItemCode;
                                        tsErrorCode.MaintainUser = oqcNGEventArgs.UserCode;
                                        tsErrorCode.MOCode = ts.MOCode;
                                        tsErrorCode.ModelCode = ts.ModelCode;
                                        tsErrorCode.RunningCard = oqcNGEventArgs.RunningCard;
                                        tsErrorCode.RunningCardSequence = ts.RunningCardSequence;
                                        tsErrorCode.TSId = ts.TSId;
                                        tsErrorCode.MOSeq = ts.MOSeq;
                                        tsErrorCode.MaintainDate = currentDateTime.DBDate;
                                        tsErrorCode.MaintainTime = currentDateTime.DBTime;
                                        if (tsFacade.GetTSErrorCode(tsErrorCode.ErrorCodeGroup, tsErrorCode.ErrorCode, tsErrorCode.TSId) == null)
                                        {
                                            tsFacade.AddTSErrorCode(tsErrorCode);
                                        }                                        
                                    }
                                }

                                // 更新相关Lot的表
                                this.UpdateOQCLotStatus(oqcNGEventArgs);
                                this.UpdateLotCheckList(oqcNGEventArgs, currentDateTime);
                                this.UpdateOQCLot2CardCheck(oqcNGEventArgs, currentDateTime, false);
                                //this.UpdateOQCLot2CheckGroup(oqcNGEventArgs, currentDateTime);
                                //this.UpdateOQCLot2ErrorCode(oqcNGEventArgs, currentDateTime);
                                this.UpdateOQCLotCard2ErrorCode(oqcNGEventArgs, currentDateTime, false);
                                this.UpdateOQCLotCardCheckList(oqcNGEventArgs, currentDateTime, false);
                            }
                            else
                            {
                                throw new Exception("$CS_NG_PLEASE_SEND_TS");
                            }
                        }
                    }
                }
                else if (actionEventArgs.ProductInfo.LastSimulation.ProductStatus == ProductStatus.GOOD)
                {
                    // 过账
                    messages.AddMessages(dataCollect.CheckID(oqcNGEventArgs));
                    if (messages.IsSuccess())
                    {
                        //
                        if (actionEventArgs.ProductInfo.NowSimulation == null)
                        {
                            throw new Exception("$System_Error");
                        }

                        // Change the op to FQC OP
                        Operation2Resource op2Res = (new BaseModelFacade(this.DataProvider)).GetOperationByResource(actionEventArgs.ProductInfo.NowSimulation.ResourceCode);
                        actionEventArgs.ProductInfo.NowSimulation.OPCode = op2Res.OPCode;

                        actionEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.NG;
                        actionEventArgs.ProductInfo.NowSimulation.NGTimes = oqcNGEventArgs.ProductInfo.NowSimulation.NGTimes + 1;

                        messages.AddMessages(dataCollect.Execute(oqcNGEventArgs));
                        if (messages.IsSuccess())
                        {
                            // 产生TS资料 和 TSErrorCode资料等
                            this.GenerateNewTSInfo(oqcNGEventArgs);

                            // 更新相关Lot的表
                            this.UpdateOQCLotStatus(oqcNGEventArgs);
                            this.UpdateLotCheckList(oqcNGEventArgs, currentDateTime);
                            this.UpdateOQCLot2CardCheck(oqcNGEventArgs, currentDateTime, true);
                            //this.UpdateOQCLot2CheckGroup(oqcNGEventArgs, currentDateTime);
                            //this.UpdateOQCLot2ErrorCode(oqcNGEventArgs, currentDateTime);
                            this.UpdateOQCLotCard2ErrorCode(oqcNGEventArgs, currentDateTime, true);
                            this.UpdateOQCLotCardCheckList(oqcNGEventArgs, currentDateTime, true);

                            // Report
                            messages.AddMessages(this.GenerateReport(oqcNGEventArgs));
                        }
                    }
                }
                else
                {
                    throw new Exception("$CS_ProductStatusError $CS_Param_ProductStatus" +MutiLanguages.ParserString(actionEventArgs.ProductInfo.LastSimulation.ProductStatus));
                }
                // End Added

                messages.Add(new Message(MessageType.Success, "$CS_SampleConfirmOK"));

                #region Marked By Hi1/Venus.Feng on 20080720 for Hisense Version
                /*
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(oqcNGEventArgs));
                if (messages.IsSuccess())
                {
                    //
                    if (actionEventArgs.ProductInfo.NowSimulation == null)
                    {
                        throw new Exception("$System_Error");
                    }


                    actionEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.NG;
                    actionEventArgs.ProductInfo.NowSimulation.NGTimes = oqcNGEventArgs.ProductInfo.NowSimulation.NGTimes + 1;

                    messages.AddMessages(dataCollect.Execute(oqcNGEventArgs));
                    if (messages.IsSuccess())
                    {
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
                        //判断是否第一笔，如果是修改oqclot
                        #region 修改OQCLot状态
                        //Laws Lu,2005/10/18,新增	更新Lot状态
                        OQCLot oqcLot = new OQCLot();
                        oqcLot.LOTNO = oqcNGEventArgs.OQCLotNO;
                        oqcLot.LotSequence = OQCFacade.Lot_Sequence_Default;
                        oqcLot.LOTStatus = Web.Helper.OQCLotStatus.OQCLotStatus_Examing;
                        oqcFacade.UpdateOQCLotStatus(oqcLot);
                        //						object[] objs = oqcFacade.ExtraQueryOQCLot2CardCheck(string.Empty,string.Empty,oqcNGEventArgs.ProductInfo.NowSimulation.MOCode,oqcNGEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default.ToString());
                        //						if(objs == null)
                        //						{
                        //							object objLot = oqcFacade.GetOQCLot(oqcNGEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default);
                        //							OQCLot oqcLot = objLot as OQCLot;
                        //							oqcLot.LOTStatus = OQCLotStatus.OQCLotStatus_Examing;
                        //							oqcFacade.UpdateOQCLot(oqcLot);
                        //						}
                        #endregion

                        //add recrod to OQCLot2CardCheck
                        #region OQCLot2CardCheck
                        OQCLot2CardCheck oqcLot2CardCheck = oqcFacade.CreateNewOQCLot2CardCheck();
                        oqcLot2CardCheck.ItemCode = oqcNGEventArgs.ProductInfo.NowSimulation.ItemCode;
                        oqcLot2CardCheck.LOTNO = oqcNGEventArgs.OQCLotNO;
                        oqcLot2CardCheck.MaintainUser = oqcNGEventArgs.UserCode;
                        oqcLot2CardCheck.MOCode = oqcNGEventArgs.ProductInfo.NowSimulation.MOCode;
                        oqcLot2CardCheck.ModelCode = oqcNGEventArgs.ProductInfo.NowSimulation.ModelCode;
                        oqcLot2CardCheck.OPCode = oqcNGEventArgs.ProductInfo.NowSimulation.OPCode;
                        oqcLot2CardCheck.ResourceCode = oqcNGEventArgs.ProductInfo.NowSimulation.ResourceCode;
                        oqcLot2CardCheck.RouteCode = oqcNGEventArgs.ProductInfo.NowSimulation.RouteCode;
                        oqcLot2CardCheck.RunningCard = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCard;
                        oqcLot2CardCheck.RunningCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
                        oqcLot2CardCheck.SegmnetCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
                        oqcLot2CardCheck.ShiftCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.ShiftCode;
                        oqcLot2CardCheck.ShiftTypeCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.ShiftTypeCode;
                        oqcLot2CardCheck.Status = ProductStatus.NG;
                        oqcLot2CardCheck.StepSequenceCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
                        oqcLot2CardCheck.TimePeriodCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode;

                        oqcLot2CardCheck.IsDataLink = oqcNGEventArgs.IsDataLink; //标识样本是不是来自数据连线 joe song 2006-06-08
                        oqcLot2CardCheck.EAttribute1 = (oqcNGEventArgs as OQCNGEventArgs).Memo;//Laws Lu,2006/07/12 add memo field

                        oqcFacade.AddOQCLot2CardCheck(oqcLot2CardCheck);
                        #endregion

                        //arraylist  OQCLOTCardCheckList
                        #region OQCLOTCardCheckList
                        if (oqcNGEventArgs.OQCLOTCardCheckLists != null)
                        {
                            for (int i = 0; i < oqcNGEventArgs.OQCLOTCardCheckLists.Length; i++)
                            {
                                if (oqcNGEventArgs.OQCLOTCardCheckLists[i] != null)
                                {
                                    oqcFacade.AddOQCLOTCardCheckList(oqcNGEventArgs.OQCLOTCardCheckLists[i] as OQCLOTCardCheckList);
                                }
                            }
                        }
                        #endregion

                        //OQCLOTCheckList 统计
                        #region OQCLOTCheckList


                        object obj = oqcFacade.GetOQCLOTCheckList(oqcNGEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                        if (obj == null)
                        {
                            OQCLOTCheckList oqcCheckList = oqcFacade.CreateNewOQCLOTCheckList();
                            oqcCheckList.AGradeTimes = OQCFacade.Decimal_Default_value;
                            oqcCheckList.BGradeTimes = OQCFacade.Decimal_Default_value;
                            oqcCheckList.CGradeTimes = OQCFacade.Decimal_Default_value;
                            oqcCheckList.LOTNO = oqcNGEventArgs.OQCLotNO;
                            oqcCheckList.LotSequence = OQCFacade.Lot_Sequence_Default;
                            oqcCheckList.MaintainUser = oqcNGEventArgs.UserCode;
                            oqcCheckList.Result = OQCLotStatus.OQCLotStatus_Examing;

                            oqcFacade.AddOQCLOTCheckList(GetDefectStatic(oqcNGEventArgs.OQCLOTCardCheckLists, oqcCheckList));
                        }
                        else
                        {
                            OQCLOTCheckList oqcCheckList = obj as OQCLOTCheckList;
                            oqcCheckList.MaintainUser = oqcNGEventArgs.UserCode;

                            oqcFacade.UpdateOQCLOTCheckList(GetDefectStatic(oqcNGEventArgs.OQCLOTCardCheckLists, oqcCheckList));
                        }
                        #endregion

                        //arraylist ErrorCodeGroupErrorCode
                        #region OQCLotCard2ErrorCode
                        if (oqcNGEventArgs.ErrorCodeInformations != null)
                        {
                            for (int i = 0; i < oqcNGEventArgs.ErrorCodeInformations.Length; i++)
                            {
                                OQCLotCard2ErrorCode oqcLotCard2ErrorCode = oqcFacade.CreateNewOQCLotCard2ErrorCode();
                                oqcLotCard2ErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCode;
                                oqcLotCard2ErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCodeGroup;
                                oqcLotCard2ErrorCode.LOTNO = oqcNGEventArgs.OQCLotNO;
                                oqcLotCard2ErrorCode.LotSequence = OQCFacade.Lot_Sequence_Default;
                                oqcLotCard2ErrorCode.MaintainUser = oqcNGEventArgs.UserCode;
                                oqcLotCard2ErrorCode.MOCode = oqcNGEventArgs.ProductInfo.NowSimulation.MOCode;
                                oqcLotCard2ErrorCode.RunningCard = oqcNGEventArgs.RunningCard;
                                oqcLotCard2ErrorCode.RunningCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
                                oqcLotCard2ErrorCode.MOSeq = oqcNGEventArgs.ProductInfo.NowSimulation.MOSeq;    // Added by Icyer 2007/07/02
                                oqcFacade.AddOQCLotCard2ErrorCode(oqcLotCard2ErrorCode);
                            }
                        }
                        #endregion

                        #region OQCLot2ErrorCode Static
                        if (oqcNGEventArgs.ErrorCodeInformations != null)
                        {
                            for (int i = 0; i < oqcNGEventArgs.ErrorCodeInformations.Length; i++)
                            {
                                obj = oqcFacade.GetOQCLot2ErrorCode(((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCodeGroup, ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCode, oqcNGEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                                if (obj == null)
                                {
                                    OQCLot2ErrorCode oqcLot2ErrorCode = oqcFacade.CreateNewOQCLot2ErrorCode();
                                    oqcLot2ErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCode;
                                    oqcLot2ErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCodeGroup;
                                    oqcLot2ErrorCode.LOTNO = oqcNGEventArgs.OQCLotNO;
                                    oqcLot2ErrorCode.LotSequence = OQCFacade.Lot_Sequence_Default;
                                    oqcLot2ErrorCode.MaintainUser = oqcNGEventArgs.UserCode;
                                    oqcLot2ErrorCode.Times = OQCFacade.Decimal_Default_value;

                                    oqcFacade.AddOQCLot2ErrorCode(GetErrorCodeStatic(oqcNGEventArgs.ErrorCodeInformations, oqcLot2ErrorCode));
                                }
                                else
                                {
                                    OQCLot2ErrorCode oqcLot2ErrorCode = obj as OQCLot2ErrorCode;
                                    oqcLot2ErrorCode.MaintainUser = oqcNGEventArgs.UserCode;

                                    oqcFacade.UpdateOQCLot2ErrorCode(GetErrorCodeStatic(oqcNGEventArgs.ErrorCodeInformations, oqcLot2ErrorCode));
                                }
                            }
                        }

                        #endregion

                        #region this is for report add by crystal chu 2005/07/25
                        ReportHelper reportCollect = new ReportHelper(this.DataProvider);
                        messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, oqcNGEventArgs.ActionType, oqcNGEventArgs.ProductInfo));
                        #endregion

                        #region TSInformation
                        BenQGuru.eMES.Domain.TS.TS tS = tsFacade.CreateNewTS();


                        tS.CardType = CardType.CardType_Product;
                        tS.FormTime = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainTime;
                        tS.FromDate = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainDate;
                        tS.FromInputType = TSFacade.TSSource_OnWIP;
                        tS.FromOPCode = oqcNGEventArgs.ProductInfo.NowSimulation.OPCode;
                        tS.FromResourceCode = oqcNGEventArgs.ProductInfo.NowSimulation.ResourceCode;
                        tS.FromRouteCode = oqcNGEventArgs.ProductInfo.NowSimulation.RouteCode;
                        tS.FromSegmentCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
                        tS.FromShiftCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.ShiftTypeCode;
                        tS.FromShiftDay = oqcNGEventArgs.ProductInfo.NowSimulationReport.ShiftDay;
                        tS.FromShiftTypeCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.ShiftTypeCode;
                        tS.FromStepSequenceCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
                        tS.FromTimePeriodCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode;
                        tS.FromUser = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainUser;
                        tS.ItemCode = oqcNGEventArgs.ProductInfo.NowSimulation.ItemCode;
                        tS.MaintainDate = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainDate;
                        tS.MaintainTime = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainTime;
                        tS.MaintainUser = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainUser;
                        tS.MOCode = oqcNGEventArgs.ProductInfo.NowSimulation.MOCode;
                        tS.ModelCode = oqcNGEventArgs.ProductInfo.NowSimulation.ModelCode;
                        tS.RunningCard = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCard;
                        tS.RunningCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
                        tS.SourceCard = oqcNGEventArgs.ProductInfo.NowSimulation.SourceCard;
                        tS.SourceCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.SourceCardSequence;
                        tS.TransactionStatus = TSFacade.TransactionStatus_None;
                        tS.TranslateCard = oqcNGEventArgs.ProductInfo.NowSimulation.TranslateCard;
                        tS.TranslateCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.TranslateCardSequence;
                        //Laws Lu,2005/08/24,修改
                        //tS.TSId= tS.RunningCard+tS.RunningCardSequence.ToString();
                        tS.TSId = FormatHelper.GetUniqueID(oqcNGEventArgs.ProductInfo.NowSimulation.MOCode
                            , tS.RunningCard, tS.RunningCardSequence.ToString());
                        //End Laws Lu
                        tS.TSStatus = TSStatus.TSStatus_New;
                        //modified by jessie lee ,when the status is new ,there is not tsuser
                        //tS.TSUser = oqcNGEventArgs.UserCode;
                        tS.TSTimes = oqcNGEventArgs.ProductInfo.NowSimulation.NGTimes;
                        tS.FromMemo = (oqcNGEventArgs as OQCNGEventArgs).Memo;//Laws Lu,2006/07/12 add memo field
                        tS.MOSeq = oqcNGEventArgs.ProductInfo.NowSimulation.MOSeq;  // Added by Icyer 2007/07/03
                        tsFacade.AddTS(tS);


                        BenQGuru.eMES.Domain.TS.TSErrorCode tsErrorCode = tsFacade.CreateNewTSErrorCode();
                        if (oqcNGEventArgs.ErrorCodeInformations != null)
                        {
                            for (int i = 0; i < oqcNGEventArgs.ErrorCodeInformations.Length; i++)
                            {
                                tsErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCode;
                                tsErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCodeGroup;
                                tsErrorCode.ItemCode = oqcNGEventArgs.ProductInfo.NowSimulation.ItemCode;
                                tsErrorCode.MaintainUser = oqcNGEventArgs.UserCode;
                                tsErrorCode.MOCode = oqcNGEventArgs.ProductInfo.NowSimulation.MOCode;
                                tsErrorCode.ModelCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.ModelCode;
                                tsErrorCode.RunningCard = oqcNGEventArgs.RunningCard;
                                tsErrorCode.RunningCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
                                tsErrorCode.TSId = tS.TSId;
                                tsErrorCode.MOSeq = oqcNGEventArgs.ProductInfo.NowSimulation.MOSeq;     // Added by Icyer 2007/07/03
                                // Added by Icyer 2006/09/06
                                tsErrorCode.MaintainDate = dbDateTime.DBDate;
                                tsErrorCode.MaintainTime = dbDateTime.DBTime;
                                // Added end
                                tsFacade.AddTSErrorCode(tsErrorCode);
                            }
                        }
                        #endregion

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionEventArgs"> </param> params (0,lotno)
        /// <returns></returns>
        public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            OQCNGEventArgs oqcNGEventArgs = actionEventArgs as OQCNGEventArgs;

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
                TSFacade tsFacade = new TSFacade(this.DataProvider);
                DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                //check oqclotstatus
                #region CheckOQCLotStatus
                object objOQClot = oqcFacade.GetOQCLot(oqcNGEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
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

                // Added By hi1/venus.Feng on 20080720 for Hisense Version : Change OQC Flow
                /// 最后一次过账的信息如果是NG，则判断其TS状态
                if (actionEventArgs.ProductInfo.LastSimulation.ProductStatus == ProductStatus.NG)
                {
                    // Check TS Status                    
                    object objTs = tsFacade.GetCardLastTSRecord(actionEventArgs.ProductInfo.LastSimulation.RunningCard);
                    if (objTs != null)
                    {
                        Domain.TS.TS ts = objTs as Domain.TS.TS;
                        if (ts.TSStatus == TSStatus.TSStatus_Complete
                            || ts.TSStatus == TSStatus.TSStatus_Reflow)
                        {
                            // 过账
                            messages.AddMessages(dataCollect.CheckID(oqcNGEventArgs, actionCheckStatus));
                            if (messages.IsSuccess())
                            {
                                //
                                if (actionEventArgs.ProductInfo.NowSimulation == null)
                                {
                                    throw new Exception("$System_Error");
                                }
                                // Change the op to FQC OP
                                Operation2Resource op2Res = (new BaseModelFacade(this.DataProvider)).GetOperationByResource(actionEventArgs.ProductInfo.NowSimulation.ResourceCode);
                                actionEventArgs.ProductInfo.NowSimulation.OPCode = op2Res.OPCode;

                                actionEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.NG;
                                actionEventArgs.ProductInfo.NowSimulation.NGTimes = oqcNGEventArgs.ProductInfo.NowSimulation.NGTimes + 1;

                                if (actionCheckStatus.NeedUpdateSimulation == true)
                                {
                                    //把资源暂时替换成onwip需要正确资源
                                    string resCode = oqcNGEventArgs.ResourceCode;
                                    oqcNGEventArgs.ResourceCode = oqcNGEventArgs.rightResource;
                                    messages.AddMessages(dataCollect.Execute(oqcNGEventArgs));
                                    //转换回原来的资源
                                    oqcNGEventArgs.ResourceCode = resCode;
                                }
                                if (messages.IsSuccess())
                                {
                                    // 产生TS资料 和 TSErrorCode资料等
                                    this.GenerateNewTSInfo(oqcNGEventArgs);

                                    // 更新相关Lot的表
                                    this.UpdateOQCLotStatus(oqcNGEventArgs);
                                    this.UpdateLotCheckList(oqcNGEventArgs, currentDateTime);
                                    this.UpdateOQCLot2CardCheck(oqcNGEventArgs, currentDateTime, true);
                                    //this.UpdateOQCLot2CheckGroup(oqcNGEventArgs, currentDateTime);
                                    //this.UpdateOQCLot2ErrorCode(oqcNGEventArgs, currentDateTime);
                                    this.UpdateOQCLotCard2ErrorCode(oqcNGEventArgs, currentDateTime, true);
                                    this.UpdateOQCLotCardCheckList(oqcNGEventArgs, currentDateTime, true);

                                    // Report
                                    messages.AddMessages(this.GenerateReport(oqcNGEventArgs, actionCheckStatus));
                                }
                            }
                        }
                        else
                        {
                            if (ts.TSStatus == TSStatus.TSStatus_New)
                            {
                                // Add new TSErrorCode to tbltserrorcode
                                BenQGuru.eMES.Domain.TS.TSErrorCode tsErrorCode = tsFacade.CreateNewTSErrorCode();
                                if (oqcNGEventArgs.ErrorCodeInformations != null)
                                {
                                    for (int i = 0; i < oqcNGEventArgs.ErrorCodeInformations.Length; i++)
                                    {
                                        tsErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCode;
                                        tsErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCodeGroup;
                                        tsErrorCode.ItemCode = ts.ItemCode;
                                        tsErrorCode.MaintainUser = oqcNGEventArgs.UserCode;
                                        tsErrorCode.MOCode = ts.MOCode;
                                        tsErrorCode.ModelCode = ts.ModelCode;
                                        tsErrorCode.RunningCard = oqcNGEventArgs.RunningCard;
                                        tsErrorCode.RunningCardSequence = ts.RunningCardSequence;
                                        tsErrorCode.TSId = ts.TSId;
                                        tsErrorCode.MOSeq = ts.MOSeq;
                                        tsErrorCode.MaintainDate = currentDateTime.DBDate;
                                        tsErrorCode.MaintainTime = currentDateTime.DBTime;
                                        if (tsFacade.GetTSErrorCode(tsErrorCode.ErrorCodeGroup, tsErrorCode.ErrorCode, tsErrorCode.TSId) == null)
                                        {
                                            tsFacade.AddTSErrorCode(tsErrorCode);
                                        }
                                    }
                                }

                                // 更新相关Lot的表
                                this.UpdateOQCLotStatus(oqcNGEventArgs);
                                this.UpdateLotCheckList(oqcNGEventArgs, currentDateTime);
                                this.UpdateOQCLot2CardCheck(oqcNGEventArgs, currentDateTime, false);
                                //this.UpdateOQCLot2CheckGroup(oqcNGEventArgs, currentDateTime);
                                //this.UpdateOQCLot2ErrorCode(oqcNGEventArgs, currentDateTime);
                                this.UpdateOQCLotCard2ErrorCode(oqcNGEventArgs, currentDateTime, false);
                                this.UpdateOQCLotCardCheckList(oqcNGEventArgs, currentDateTime, false);
                            }
                            else
                            {
                                throw new Exception("$CS_NG_PLEASE_SEND_TS");
                            }
                        }
                    }
                }
                else if (actionEventArgs.ProductInfo.LastSimulation.ProductStatus == ProductStatus.GOOD)
                {
                    // 过账
                    messages.AddMessages(dataCollect.CheckID(oqcNGEventArgs, actionCheckStatus));

                    if (messages.IsSuccess())
                    {
                        //
                        if (actionEventArgs.ProductInfo.NowSimulation == null)
                        {
                            throw new Exception("$System_Error");
                        }
                        // Change the op to FQC OP
                        Operation2Resource op2Res = (new BaseModelFacade(this.DataProvider)).GetOperationByResource(actionEventArgs.ProductInfo.NowSimulation.ResourceCode);
                        actionEventArgs.ProductInfo.NowSimulation.OPCode = op2Res.OPCode;

                        actionEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.NG;
                        actionEventArgs.ProductInfo.NowSimulation.NGTimes = oqcNGEventArgs.ProductInfo.NowSimulation.NGTimes + 1;

                        if (actionCheckStatus.NeedUpdateSimulation == true)
                        {
                            //把资源暂时替换成onwip需要正确资源
                            string resCode = oqcNGEventArgs.ResourceCode;
                            oqcNGEventArgs.ResourceCode = oqcNGEventArgs.rightResource;
                            messages.AddMessages(dataCollect.Execute(oqcNGEventArgs));
                            //转换回原来的资源
                            oqcNGEventArgs.ResourceCode = resCode;
                        }
                        
                        if (messages.IsSuccess())
                        {
                            // 产生TS资料 和 TSErrorCode资料等
                            this.GenerateNewTSInfo(oqcNGEventArgs);

                            // 更新相关Lot的表
                            this.UpdateOQCLotStatus(oqcNGEventArgs);
                            this.UpdateLotCheckList(oqcNGEventArgs, currentDateTime);
                            this.UpdateOQCLot2CardCheck(oqcNGEventArgs, currentDateTime, true);
                            //this.UpdateOQCLot2CheckGroup(oqcNGEventArgs, currentDateTime);
                            //this.UpdateOQCLot2ErrorCode(oqcNGEventArgs, currentDateTime);
                            this.UpdateOQCLotCard2ErrorCode(oqcNGEventArgs, currentDateTime, true);
                            this.UpdateOQCLotCardCheckList(oqcNGEventArgs, currentDateTime, true);

                            // Report
                            messages.AddMessages(this.GenerateReport(oqcNGEventArgs, actionCheckStatus));
                        }
                    }
                }
                else
                {
                    throw new Exception("$CS_ProductStatusError $CS_Param_ProductStatus" +MutiLanguages.ParserString(actionEventArgs.ProductInfo.LastSimulation.ProductStatus));
                }
                // End Added
                if (messages.IsSuccess())
                {
                    messages.Add(new Message(MessageType.Success, "$CS_SampleConfirmOK"));
                }

            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        #region private method

        private void UpdateOQCLotStatus(OQCNGEventArgs oqcNGEventArgs)
        {
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            OQCLot oqcLot = new OQCLot();
            oqcLot.LOTNO = oqcNGEventArgs.OQCLotNO;
            oqcLot.LotSequence = OQCFacade.Lot_Sequence_Default;
            oqcLot.LOTStatus = Web.Helper.OQCLotStatus.OQCLotStatus_Examing;
            oqcFacade.UpdateOQCLotStatus(oqcLot);
        }

        private void UpdateOQCLot2CardCheck(OQCNGEventArgs oqcNGEventArgs, DBDateTime currentDateTime, bool IsNowSimulation)
        {
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            OQCLot2CardCheck oqcLot2CardCheck = oqcFacade.CreateNewOQCLot2CardCheck();

            if (IsNowSimulation)
            {
                oqcLot2CardCheck.ItemCode = oqcNGEventArgs.ProductInfo.NowSimulation.ItemCode;
                oqcLot2CardCheck.LOTNO = oqcNGEventArgs.OQCLotNO;
                oqcLot2CardCheck.MaintainUser = oqcNGEventArgs.UserCode;
                oqcLot2CardCheck.MOCode = oqcNGEventArgs.ProductInfo.NowSimulation.MOCode;
                oqcLot2CardCheck.ModelCode = oqcNGEventArgs.ProductInfo.NowSimulation.ModelCode;
                oqcLot2CardCheck.OPCode = oqcNGEventArgs.ProductInfo.NowSimulation.OPCode;
                oqcLot2CardCheck.ResourceCode = oqcNGEventArgs.ResourceCode;
                oqcLot2CardCheck.RouteCode = oqcNGEventArgs.ProductInfo.NowSimulation.RouteCode;
                oqcLot2CardCheck.RunningCard = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCard;
                oqcLot2CardCheck.RunningCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
            }
            else
            {
                oqcLot2CardCheck.ItemCode = oqcNGEventArgs.ProductInfo.LastSimulation.ItemCode;
                oqcLot2CardCheck.LOTNO = oqcNGEventArgs.OQCLotNO;
                oqcLot2CardCheck.MaintainUser = oqcNGEventArgs.UserCode;
                oqcLot2CardCheck.MOCode = oqcNGEventArgs.ProductInfo.LastSimulation.MOCode;
                oqcLot2CardCheck.ModelCode = oqcNGEventArgs.ProductInfo.LastSimulation.ModelCode;
                oqcLot2CardCheck.OPCode = oqcNGEventArgs.ProductInfo.LastSimulation.OPCode;
                oqcLot2CardCheck.ResourceCode = oqcNGEventArgs.ResourceCode;
                oqcLot2CardCheck.RouteCode = oqcNGEventArgs.ProductInfo.LastSimulation.RouteCode;
                oqcLot2CardCheck.RunningCard = oqcNGEventArgs.ProductInfo.LastSimulation.RunningCard;
                oqcLot2CardCheck.RunningCardSequence = oqcNGEventArgs.ProductInfo.LastSimulation.RunningCardSequence;
            }           

            BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
            Resource resource = (Resource)dataModel.GetResource(oqcNGEventArgs.ResourceCode);

            oqcLot2CardCheck.SegmnetCode = resource.SegmentCode;

            TimePeriod period = oqcNGEventArgs.ProductInfo.TimePeriod;
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
            oqcLot2CardCheck.Status = ProductStatus.NG;
            oqcLot2CardCheck.StepSequenceCode = resource.StepSequenceCode;
            oqcLot2CardCheck.TimePeriodCode = period.TimePeriodCode;
            oqcLot2CardCheck.IsDataLink = oqcNGEventArgs.IsDataLink; //标识样本是不是来自数据连线 joe song 2006-06-08
            oqcLot2CardCheck.EAttribute1 = (oqcNGEventArgs as OQCNGEventArgs).Memo;//Laws Lu,2006/07/12 add memo field
            oqcLot2CardCheck.CheckSequence = oqcNGEventArgs.CheckSequence;
            oqcLot2CardCheck.MaintainDate = currentDateTime.DBDate;
            oqcLot2CardCheck.MaintainTime = currentDateTime.DBTime;
            oqcFacade.AddOQCLot2CardCheck(oqcLot2CardCheck);
        }

        private void UpdateOQCLotCardCheckList(OQCNGEventArgs oqcNGEventArgs, DBDateTime currentDateTime, bool IsNowSimulation)
        {
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            if (oqcNGEventArgs.OQCLOTCardCheckLists != null)
            {
                OQCLOTCardCheckList oqcLotCardCheckList;
                for (int i = 0; i < oqcNGEventArgs.OQCLOTCardCheckLists.Length; i++)
                {
                    if (oqcNGEventArgs.OQCLOTCardCheckLists[i] != null)
                    {
                        oqcLotCardCheckList = oqcNGEventArgs.OQCLOTCardCheckLists[i] as OQCLOTCardCheckList;
                        oqcLotCardCheckList.MaintainDate = currentDateTime.DBDate;
                        oqcLotCardCheckList.MaintainTime = currentDateTime.DBTime;
                        if (IsNowSimulation)
                        {
                            oqcLotCardCheckList.RunningCardSequence++;
                        }

                        oqcFacade.AddOQCLOTCardCheckList(oqcLotCardCheckList);
                    }
                }
            }
        }

        private void UpdateLotCheckList(OQCNGEventArgs oqcNGEventArgs, DBDateTime currentDateTime)
        {
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            object obj = oqcFacade.GetOQCLOTCheckList(oqcNGEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
            if (obj == null)
            {
                OQCLOTCheckList oqcCheckList = oqcFacade.CreateNewOQCLOTCheckList();
                oqcCheckList.AGradeTimes = OQCFacade.Decimal_Default_value;
                oqcCheckList.BGradeTimes = OQCFacade.Decimal_Default_value;
                oqcCheckList.CGradeTimes = OQCFacade.Decimal_Default_value;
                oqcCheckList.ZGradeTimes = OQCFacade.Decimal_Default_value;
                oqcCheckList.LOTNO = oqcNGEventArgs.OQCLotNO;
                oqcCheckList.LotSequence = OQCFacade.Lot_Sequence_Default;
                oqcCheckList.MaintainUser = oqcNGEventArgs.UserCode;
                oqcCheckList.Result = OQCLotStatus.OQCLotStatus_Examing;
                oqcCheckList.MaintainDate = currentDateTime.DBDate;
                oqcCheckList.MaintainTime = currentDateTime.DBTime;
                oqcFacade.AddOQCLOTCheckList(GetDefectStatic(oqcNGEventArgs.OQCLOTCardCheckLists, oqcCheckList));
            }
            else
            {
                OQCLOTCheckList oqcCheckList = obj as OQCLOTCheckList;
                oqcCheckList.MaintainUser = oqcNGEventArgs.UserCode;
                oqcCheckList.MaintainDate = currentDateTime.DBDate;
                oqcCheckList.MaintainTime = currentDateTime.DBTime;
                oqcFacade.UpdateOQCLOTCheckList(GetDefectStatic(oqcNGEventArgs.OQCLOTCardCheckLists, oqcCheckList));
            }
        }

        private void UpdateOQCLot2CheckGroup(OQCNGEventArgs oqcNGEventArgs, DBDateTime currentDateTime)
        {
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            if (oqcNGEventArgs.OQCLot2CheckGroupList != null)
            {
                OQCLot2CheckGroup oqcLot2CheckGroup;
                for (int i = 0; i < oqcNGEventArgs.OQCLot2CheckGroupList.Length; i++)
                {
                    if (oqcNGEventArgs.OQCLot2CheckGroupList[i] != null)
                    {
                        oqcLot2CheckGroup = oqcNGEventArgs.OQCLot2CheckGroupList[i] as OQCLot2CheckGroup;
                        oqcLot2CheckGroup.MaintainDate = currentDateTime.DBDate;
                        oqcLot2CheckGroup.MaintainTime = currentDateTime.DBTime;
                        if (oqcFacade.GetOQCLot2CheckGroup(oqcLot2CheckGroup.LOTNO, oqcLot2CheckGroup.LotSequence, oqcLot2CheckGroup.CheckGroup) == null)
                        {
                            oqcFacade.AddOQCLot2CheckGroup(oqcLot2CheckGroup);
                        }
                        else
                        {
                            oqcFacade.UpdateOQCLot2CheckGroup(oqcLot2CheckGroup);
                        }
                    }
                }
            }
        }

        private void UpdateOQCLotCard2ErrorCode(OQCNGEventArgs oqcNGEventArgs, DBDateTime currentDateTime, bool IsNowSimulation)
        {
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            if (oqcNGEventArgs.ErrorCodeInformations != null)
            {
                for (int i = 0; i < oqcNGEventArgs.ErrorCodeInformations.Length; i++)
                {
                    OQCLotCard2ErrorCode oqcLotCard2ErrorCode = oqcFacade.CreateNewOQCLotCard2ErrorCode();
                    oqcLotCard2ErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCode;
                    oqcLotCard2ErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCodeGroup;
                    oqcLotCard2ErrorCode.LOTNO = oqcNGEventArgs.OQCLotNO;
                    oqcLotCard2ErrorCode.LotSequence = OQCFacade.Lot_Sequence_Default;
                    oqcLotCard2ErrorCode.MaintainUser = oqcNGEventArgs.UserCode;
                    oqcLotCard2ErrorCode.RunningCard = oqcNGEventArgs.RunningCard;
                    if (IsNowSimulation)
                    {
                        oqcLotCard2ErrorCode.MOCode = oqcNGEventArgs.ProductInfo.NowSimulation.MOCode;
                        oqcLotCard2ErrorCode.RunningCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
                        oqcLotCard2ErrorCode.MOSeq = oqcNGEventArgs.ProductInfo.NowSimulation.MOSeq;
                    }
                    else
                    {
                        oqcLotCard2ErrorCode.MOCode = oqcNGEventArgs.ProductInfo.LastSimulation.MOCode;
                        oqcLotCard2ErrorCode.RunningCardSequence = oqcNGEventArgs.ProductInfo.LastSimulation.RunningCardSequence;
                        oqcLotCard2ErrorCode.MOSeq = oqcNGEventArgs.ProductInfo.LastSimulation.MOSeq;
                    }

                    oqcLotCard2ErrorCode.MaintainDate = currentDateTime.DBDate;
                    oqcLotCard2ErrorCode.MaintainTime = currentDateTime.DBTime;
                    oqcLotCard2ErrorCode.CheckSequence = oqcNGEventArgs.CheckSequence;
                    oqcFacade.AddOQCLotCard2ErrorCode(oqcLotCard2ErrorCode);
                }
            }
        }

        private void UpdateOQCLot2ErrorCode(OQCNGEventArgs oqcNGEventArgs, DBDateTime currentDateTime)
        {
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            if (oqcNGEventArgs.ErrorCodeInformations != null)
            {
                object obj;
                for (int i = 0; i < oqcNGEventArgs.ErrorCodeInformations.Length; i++)
                {
                    obj = oqcFacade.GetOQCLot2ErrorCode(((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCodeGroup, ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCode, oqcNGEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                    if (obj == null)
                    {
                        OQCLot2ErrorCode oqcLot2ErrorCode = oqcFacade.CreateNewOQCLot2ErrorCode();
                        oqcLot2ErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCode;
                        oqcLot2ErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCodeGroup;
                        oqcLot2ErrorCode.LOTNO = oqcNGEventArgs.OQCLotNO;
                        oqcLot2ErrorCode.LotSequence = OQCFacade.Lot_Sequence_Default;
                        oqcLot2ErrorCode.MaintainUser = oqcNGEventArgs.UserCode;
                        oqcLot2ErrorCode.Times = OQCFacade.Decimal_Default_value;
                        oqcLot2ErrorCode.MaintainDate = currentDateTime.DBDate;
                        oqcLot2ErrorCode.MaintainTime = currentDateTime.DBTime;
                        oqcFacade.AddOQCLot2ErrorCode(GetErrorCodeStatic(oqcNGEventArgs.ErrorCodeInformations, oqcLot2ErrorCode));
                    }
                    else
                    {
                        OQCLot2ErrorCode oqcLot2ErrorCode = obj as OQCLot2ErrorCode;
                        oqcLot2ErrorCode.MaintainUser = oqcNGEventArgs.UserCode;
                        oqcLot2ErrorCode.MaintainDate = currentDateTime.DBDate;
                        oqcLot2ErrorCode.MaintainTime = currentDateTime.DBTime;

                        oqcFacade.UpdateOQCLot2ErrorCode(GetErrorCodeStatic(oqcNGEventArgs.ErrorCodeInformations, oqcLot2ErrorCode));
                    }
                }
            }
        }

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
        private OQCLot2ErrorCode GetErrorCodeStatic(object[] objsErrorCodes, OQCLot2ErrorCode oqcLot2ErrorCode)
        {
            if (objsErrorCodes != null)
            {
                for (int i = 0; i < objsErrorCodes.Length; i++)
                {
                    if ((((ErrorCodeGroup2ErrorCode)objsErrorCodes[i]).ErrorCodeGroup == oqcLot2ErrorCode.ErrorCodeGroup) && (((ErrorCodeGroup2ErrorCode)objsErrorCodes[i]).ErrorCode == oqcLot2ErrorCode.ErrorCode))
                    {
                        oqcLot2ErrorCode.Times = oqcLot2ErrorCode.Times + 1;
                    }
                }
            }
            return oqcLot2ErrorCode;
        }

        private Messages GenerateReport(OQCNGEventArgs oqcNGEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            //ReportHelper reportCollect = new ReportHelper(this.DataProvider);
            //if (actionCheckStatus.NeedFillReport == true)
            //{
            //    messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, oqcNGEventArgs.ActionType, oqcNGEventArgs.ProductInfo));
            //    //Laws Lu,2005/11/05,新增	需要填写实时缺陷报表
            //    messages.AddMessages(reportCollect.ReportLineECOQuanMaster(this.DataProvider, oqcNGEventArgs.ActionType, oqcNGEventArgs.ProductInfo, oqcNGEventArgs.ErrorCodeInformations));
            //}
            return messages;
        }

        private Messages GenerateReport(OQCNGEventArgs oqcNGEventArgs)
        {
            Messages messages = new Messages();
            //ReportHelper reportCollect = new ReportHelper(this.DataProvider);
            //messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, oqcNGEventArgs.ActionType, oqcNGEventArgs.ProductInfo));
            return messages;
        }

        private void GenerateNewTSInfo(OQCNGEventArgs oqcNGEventArgs)
        {
            TSFacade tsFacade = new TSFacade(this.DataProvider);
            BenQGuru.eMES.Domain.TS.TS tS = tsFacade.CreateNewTS();

            tS.CardType = CardType.CardType_Product;
            tS.FormTime = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainTime;
            tS.FromDate = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainDate;
            tS.FromInputType = TSFacade.TSSource_OnWIP;
            tS.FromOPCode = oqcNGEventArgs.ProductInfo.NowSimulation.OPCode;
            tS.FromResourceCode = oqcNGEventArgs.rightResource;// oqcNGEventArgs.ProductInfo.NowSimulation.ResourceCode;
            tS.FromRouteCode = oqcNGEventArgs.ProductInfo.NowSimulation.RouteCode;
            tS.FromSegmentCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
            tS.FromShiftCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.ShiftTypeCode;
            tS.FromShiftDay = oqcNGEventArgs.ProductInfo.NowSimulationReport.ShiftDay;
            tS.FromShiftTypeCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.ShiftTypeCode;
            tS.FromStepSequenceCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
            tS.FromTimePeriodCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode;
            tS.FromUser = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainUser;
            tS.ItemCode = oqcNGEventArgs.ProductInfo.NowSimulation.ItemCode;
            tS.MaintainDate = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainDate;
            tS.MaintainTime = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainTime;
            tS.MaintainUser = oqcNGEventArgs.ProductInfo.NowSimulation.MaintainUser;
            tS.MOCode = oqcNGEventArgs.ProductInfo.NowSimulation.MOCode;
            tS.ModelCode = oqcNGEventArgs.ProductInfo.NowSimulation.ModelCode;
            tS.RunningCard = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCard;
            tS.RunningCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
            tS.SourceCard = oqcNGEventArgs.ProductInfo.NowSimulation.SourceCard;
            tS.SourceCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.SourceCardSequence;
            tS.TransactionStatus = TSFacade.TransactionStatus_None;
            tS.TranslateCard = oqcNGEventArgs.ProductInfo.NowSimulation.TranslateCard;
            tS.TranslateCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.TranslateCardSequence;
            tS.TSId = FormatHelper.GetUniqueID(oqcNGEventArgs.ProductInfo.NowSimulation.MOCode
                , tS.RunningCard, tS.RunningCardSequence.ToString());
            tS.TSStatus = TSStatus.TSStatus_New;
            tS.TSTimes = oqcNGEventArgs.ProductInfo.NowSimulation.NGTimes;
            tS.RMABillCode = oqcNGEventArgs.ProductInfo.NowSimulation.RMABillCode;
            tS.FromMemo = (oqcNGEventArgs as OQCNGEventArgs).Memo;
            tS.MOSeq = oqcNGEventArgs.ProductInfo.NowSimulation.MOSeq;      // Added by Icyer 2007/07/03
            tsFacade.AddTS(tS);

            BenQGuru.eMES.Domain.TS.TSErrorCode tsErrorCode = tsFacade.CreateNewTSErrorCode();
            if (oqcNGEventArgs.ErrorCodeInformations != null)
            {
                for (int i = 0; i < oqcNGEventArgs.ErrorCodeInformations.Length; i++)
                {
                    tsErrorCode.ErrorCode = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCode;
                    tsErrorCode.ErrorCodeGroup = ((ErrorCodeGroup2ErrorCode)oqcNGEventArgs.ErrorCodeInformations[i]).ErrorCodeGroup;
                    tsErrorCode.ItemCode = oqcNGEventArgs.ProductInfo.NowSimulation.ItemCode;
                    tsErrorCode.MaintainUser = oqcNGEventArgs.UserCode;
                    tsErrorCode.MOCode = oqcNGEventArgs.ProductInfo.NowSimulation.MOCode;
                    tsErrorCode.ModelCode = oqcNGEventArgs.ProductInfo.NowSimulationReport.ModelCode;
                    tsErrorCode.RunningCard = oqcNGEventArgs.RunningCard;
                    tsErrorCode.RunningCardSequence = oqcNGEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
                    tsErrorCode.TSId = tS.TSId;
                    tsErrorCode.MOSeq = oqcNGEventArgs.ProductInfo.NowSimulation.MOSeq;     // Added by Icyer 2007/07/03
                    tsFacade.AddTSErrorCode(tsErrorCode);
                }
            }
        }

        #endregion
    }
}
