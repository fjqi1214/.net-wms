using System;
using System.Collections;

using BenQGuru.eMES.AlertModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.DataCollect.Action
{
    /// <summary>
    /// 归属工单采集
    /// </summary>
    public class ActionOQCReject : IActionWithStatus
    {
        private IDomainDataProvider _domainDataProvider = null;

        //		public ActionOQCReject()
        //		{	
        //		}

        public ActionOQCReject(IDomainDataProvider domainDataProvider)
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

        public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            return new Messages();
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
            OQCRejectEventArgs oqcRejectEventArgs = actionEventArgs as OQCRejectEventArgs;
            try
            {
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                ActionOQCHelper oqcHelper = new ActionOQCHelper(this.DataProvider);
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);
                TSFacade tsFacade = new TSFacade(this.DataProvider);

                //check oqcStatus
                #region CheckOQCLotStatus
                //				object objOQClot = oqcFacade.GetOQCLot(oqcRejectEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default);
                //				if(objOQClot == null)
                //				{
                //					throw new Exception("$Error_OQCLotNotExisted");
                //				}
                //				if( ((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Initial)
                //				{
                //					throw new Exception("$Error_OQCLotNO_Cannot_Initial");
                //				}
                //				if( ((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_NoExame)
                //				{
                //					throw new Exception("$Error_OQCLotNO_Cannot_NoExame");
                //				}
                //				if( (((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Reject)||((((OQCLot)objOQClot).LOTStatus == OQCLotStatus.OQCLotStatus_Pass)  ))
                //				{
                //					throw new Exception("$Error_OQCLotNO_HasComplete");
                //				}
                #endregion

                if (!oqcRejectEventArgs.IsForceReject)
                {
                    //必须要有NG而且在送修状态的板子
                    if (tsFacade.HaveNewStatusCardInOQCLot(oqcRejectEventArgs.OQCLotNO, String.Empty))
                    {
                        throw new Exception("$Error_OQCLotNo_HasNoTS");
                    }
                }

                object objLot = null;
                if (oqcRejectEventArgs.Lot == null)
                {
                    objLot = oqcFacade.GetExamingOQCLot(oqcRejectEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                }
                else
                {
                    objLot = oqcRejectEventArgs.Lot;
                }

                //update each running card reject
                #region 取得每个板子进行批退,填充reject,Reject2ErrorCode信息
                //reject
                object[] RCards = null;

                if (oqcRejectEventArgs.CardOfLot != null)
                {
                    RCards = oqcRejectEventArgs.CardOfLot;
                }
                else
                {
                    RCards = oqcHelper.QueryCardOfLot(oqcRejectEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                }

                if (!oqcRejectEventArgs.IsForceReject)
                {
                    int iNGCount = 0;
                    for (int j = 0; j < RCards.Length; j++)
                    {
                        if (((Simulation)RCards[j]).ProductStatus == ProductStatus.NG)
                        {
                            iNGCount = 1;
                            break;
                        }
                    }
                    int funcNGCount = 0;
                    funcNGCount = oqcFacade.QueryFuncTesCount(oqcRejectEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default.ToString(), ProductStatus.NG);
                    if (iNGCount == 0 && funcNGCount == 0)
                    {
                        throw new Exception("$CS_LOT_NOT_EXIST_NG");
                    }
                }

                #region reject errorCodes
                object[] objs = oqcFacade.ExtraQueryOQCLotCard2ErrorCode(oqcRejectEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default.ToString());
                Reject2ErrorCode reject2ErrorCode = reworkFacade.CreateNewReject2ErrorCode();
                //Reject2ErrorCode
                if (objs != null)
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        bool needInsert = false;
                        foreach (Simulation sim in RCards)
                        {
                            if (sim.RunningCard == ((OQCLotCard2ErrorCode)objs[i]).RunningCard
                                && sim.RunningCardSequence == ((OQCLotCard2ErrorCode)objs[i]).RunningCardSequence)
                            {
                                needInsert = true;
                            }
                        }
                        if (needInsert)
                        {
                            reject2ErrorCode.ErrorCode = ((OQCLotCard2ErrorCode)objs[i]).ErrorCode;
                            reject2ErrorCode.ErrorCodeGroup = ((OQCLotCard2ErrorCode)objs[i]).ErrorCodeGroup;
                            reject2ErrorCode.MaintainUser = actionEventArgs.UserCode;
                            reject2ErrorCode.RunningCard = ((OQCLotCard2ErrorCode)objs[i]).RunningCard;
                            reject2ErrorCode.RunningCardSequence = ((OQCLotCard2ErrorCode)objs[i]).RunningCardSequence + 1;
                            reject2ErrorCode.MOCode = ((OQCLotCard2ErrorCode)objs[i]).MOCode;
                            reject2ErrorCode.LotNo = ((OQCRejectEventArgs)actionEventArgs).OQCLotNO;
                            reject2ErrorCode.MOSeq = ((OQCLotCard2ErrorCode)objs[i]).MOSeq;         // Added by Icyer 2007/07/03

                            if (reworkFacade.GetReject2ErrorCode(reject2ErrorCode.ErrorCode, reject2ErrorCode.RunningCard, reject2ErrorCode.RunningCardSequence, reject2ErrorCode.ErrorCodeGroup) == null)
                            {
                                reworkFacade.AddReject2ErrorCode(reject2ErrorCode);
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region OQCLotCheckList
                object objLotCheckList = oqcFacade.GetOQCLOTCheckList(oqcRejectEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                if (objLotCheckList != null)
                {
                    OQCLOTCheckList oqcLotCheckList = objLotCheckList as OQCLOTCheckList;
                    oqcLotCheckList.Result = OQCLotStatus.OQCLotStatus_Reject;
                    oqcFacade.UpdateOQCLOTCheckList(oqcLotCheckList);
                }
                #endregion

                //把整个lot中的ID全变成reject
                #region 整个lot中的ID全变成reject
                object[] objsInTS = oqcFacade.GetOQCLot2CardInTS(string.Empty, string.Empty, string.Empty, string.Empty, oqcRejectEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default.ToString());
                if (objsInTS == null)
                {
                    oqcFacade.UpdateOQCLot2CardByOQCResult(oqcRejectEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default.ToString(), string.Empty, string.Empty, ProductStatus.Reject, true);
                }
                else
                {
                    string tmpString = string.Empty;
                    for (int i = 0; i < objsInTS.Length; i++)
                    {
                        tmpString += ": " + ((OQCLot2Card)objsInTS[i]).RunningCard;
                    }
                    throw new Exception(String.Format("$Error_RunningCardInTS: {0}", tmpString));
                }
                #endregion

                messages.AddMessages(SetReworkInformation(messages, RCards, oqcRejectEventArgs, dataCollect, reworkFacade));

                #region updateOQCLot

                DBDateTime dbDateTime;

                dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                OQCLot oqcLot = objLot as OQCLot;
                oqcLot.LOTStatus = OQCLotStatus.OQCLotStatus_Reject;
                oqcLot.DealDate = dbDateTime.DBDate;
                oqcLot.DealTime = dbDateTime.DBTime;
                oqcLot.DealUser = actionEventArgs.UserCode;
                oqcLot.Memo = oqcRejectEventArgs.Memo;
                oqcFacade.UpdateOQCLotStatus(oqcLot);
                #endregion

                if (oqcRejectEventArgs.IsAutoGenerateReworkSheet)
                {
                    //ReworkSheet reworkSheet = reworkFacade.GetMaxReworkSheet(dbDateTime.DBDate);
                    //reworkSheet.ReworkCode = reworkSheet.ReworkCode.Substring(0, 9) + Convert.ToString(int.Parse(reworkSheet.ReworkCode.Substring(9, 3)) + 1).PadLeft(3, '0');
                    ReworkSheet reworkSheet = new ReworkSheet();
                    reworkSheet.ReworkCode = oqcLot.LOTNO;
                    reworkSheet.CreateDate = dbDateTime.DBDate;
                    reworkSheet.CreateTime = dbDateTime.DBTime;
                    reworkSheet.CreateUser = actionEventArgs.UserCode;
                    reworkSheet.Department = "";
                    reworkSheet.EAttribute1 = "";
                    reworkSheet.ItemCode = (RCards[0] as Simulation).ItemCode;
                    reworkSheet.LotList = oqcRejectEventArgs.OQCLotNO;
                    reworkSheet.MaintainDate = dbDateTime.DBDate;
                    reworkSheet.MaintainTime = dbDateTime.DBTime;
                    reworkSheet.MaintainUser = actionEventArgs.UserCode;
                    reworkSheet.MOCode = "";
                    reworkSheet.NeedCheck = "N";
                    reworkSheet.NewMOCode = "";
                    reworkSheet.NewOPBOMCode = "";
                    reworkSheet.NewOPBOMVersion = "";
                    reworkSheet.NewOPCode = "";
                    reworkSheet.NewRouteCode = "";
                    reworkSheet.ReasonAnalyse = "";
                    reworkSheet.ReworkContent = "";
                    reworkSheet.ReworkDate = 0;
                    reworkSheet.ReworkHC = 0;
                    reworkSheet.ReworkMaxQty = 0;
                    reworkSheet.ReworkQty = RCards.Length;
                    reworkSheet.ReworkRealQty = 0;
                    reworkSheet.ReworkReason = oqcRejectEventArgs.Memo;
                    reworkSheet.ReworkSourceCode = " ";
                    reworkSheet.ReworkTime = 0;
                    reworkSheet.ReworkType = ReworkType.REWORKTYPE_ONLINE;
                    reworkSheet.Soluation = "";
                    reworkSheet.Status = ReworkStatus.REWORKSTATUS_RELEASE;
                    reworkSheet.AutoLot = oqcRejectEventArgs.IsCreateNewLot ? "Y" : "";

                    reworkFacade.AddReworkSheetWithOutTrans(reworkSheet);
                }

                //add by roger.xue 2008/10/27
                if (oqcRejectEventArgs.IsUnFrozen)
                {
                    oqcFacade.UpdateUnFrozenOnLot(oqcRejectEventArgs.OQCLotNO, oqcRejectEventArgs.UnFrozenReason,
                        dbDateTime.DBDate, dbDateTime.DBTime, oqcRejectEventArgs.UserCode, OQCFacade.Lot_Sequence_Default);

                    oqcFacade.UnFreezeFrozen(oqcRejectEventArgs.OQCLotNO, oqcRejectEventArgs.UnFrozenReason,
                        dbDateTime.DBDate, dbDateTime.DBTime, oqcRejectEventArgs.UserCode, OQCFacade.Lot_Sequence_Default);
                }
                //end add

                AlertFacade alertFacade = new AlertFacade(this.DataProvider);
                alertFacade.AlertOQCReject(oqcLot.ItemCode, oqcLot.SSCode, oqcLot.LOTNO, oqcLot.Memo);
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        private Domain.BaseSetting.Resource Resource;

        private Messages SetReworkInformation(Messages messages, object[] Simulations,OQCRejectEventArgs actionEventArgs,ActionOnLineHelper dataCollect,ReworkFacade reworkFacade)
        {
            Messages newMessages = new Messages();
            BenQGuru.eMES.TS.TSFacade tsFacade = new BenQGuru.eMES.TS.TSFacade(this.DataProvider);
            DataCollectFacade dcf = new DataCollectFacade(this._domainDataProvider);
            //ProductInfo preProduct =null;
            int i = 0;

            ActionCheckStatus actionCheckStatus = new ActionCheckStatus();

            if (Resource == null)
            {
                BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(actionEventArgs.ResourceCode);

            }

            while (i < Simulations.Length)
            {
                try
                {
                    #region 保存WIP
                    ProductInfo productionInf = dataCollect.GetIDInfoBySimulation((Simulation)Simulations[i]);

                    if (actionEventArgs.listActionCheckStatus.ContainsKey(productionInf.LastSimulation.MOCode))
                    {
                        actionCheckStatus = (ActionCheckStatus)actionEventArgs.listActionCheckStatus[productionInf.LastSimulation.MOCode];

                        actionCheckStatus.ProductInfo = productionInf;

                        actionCheckStatus.ProductInfo.Resource = Resource;

                        //					lastSimulation =  productionInf.LastSimulation;
                        actionCheckStatus.ActionList = new ArrayList();
                    }
                    else
                    {
                        //actionCheckStatus.NeedUpdateSimulation = false;
                        //actionCheckStatus.NeedFillReport = true;
                        actionEventArgs.listActionCheckStatus.Add(productionInf.LastSimulation.MOCode, actionCheckStatus);
                    }

                    OQCRejectEventArgs oqcRejectEventArgs = new OQCRejectEventArgs(
                        ActionType.DataCollectAction_OQCReject, ((Simulation)Simulations[i]).RunningCard,
                        actionEventArgs.UserCode, actionEventArgs.ResourceCode, actionEventArgs.OQCLotNO, productionInf);

                    newMessages.AddMessages(dataCollect.CheckID(oqcRejectEventArgs, actionCheckStatus));
                    //					if (preProduct != null)
                    //					{
                    //						if (dataCollect.CompareSimulationCheck(preProduct.LastSimulation,oqcRejectEventArgs.ProductInfo.LastSimulation))
                    //						{
                    //							dataCollect.CopyProduct(preProduct,oqcRejectEventArgs.ProductInfo);
                    //							dcf.WriteSimulationCheckOnlineOP(oqcRejectEventArgs.RunningCard,oqcRejectEventArgs.ActionType,oqcRejectEventArgs.ResourceCode,
                    //								oqcRejectEventArgs.UserCode,oqcRejectEventArgs.ProductInfo);
                    //							needCheck=false;
                    //						}
                    //					}
                    //					if (needCheck)		
                    //						newMessages.AddMessages(dataCollect.CheckID(oqcRejectEventArgs));
                    if (!newMessages.IsSuccess())
                    {
                        return newMessages;
                    }
                    #region TS
                    //Laws Lu,需要修改	曾经走过TS处理,没有考虑多次TS的情况,MOCode和Sequence也应该作为关键参数,
                    BenQGuru.eMES.Domain.TS.TS ts = (BenQGuru.eMES.Domain.TS.TS)tsFacade.GetCardLastTSRecord(oqcRejectEventArgs.RunningCard);
                    if (ActionType.DataCollectAction_OQCReject == oqcRejectEventArgs.ActionType
                        && ts != null
                        && ts.FromInputType == TS.TSFacade.TSSource_OnWIP
                        )
                    {

                        //Laws Lu,2006/07/07 修复在OQC前工序NG回流后,SN后仍然留在PK站
                        dcf.AdjustRouteOPOnline(oqcRejectEventArgs.RunningCard
                            , ActionType.DataCollectAction_OQCReject
                            , oqcRejectEventArgs.ResourceCode
                            , oqcRejectEventArgs.UserCode
                            , oqcRejectEventArgs.ProductInfo);


                        //oqcRejectEventArgs.ProductInfo.NowSimulation.OPCode = op.OPCode;

                        dcf.WriteSimulation(
                            oqcRejectEventArgs.RunningCard
                            , ActionType.DataCollectAction_OQCReject
                            , oqcRejectEventArgs.ResourceCode
                            , oqcRejectEventArgs.UserCode
                            , oqcRejectEventArgs.ProductInfo);

                    }
                    #endregion
                    //Laws Lu,2006/07/07 fix op 
                    if (oqcRejectEventArgs.ProductInfo.CurrentItemRoute2OP != null)
                    {
                        oqcRejectEventArgs.ProductInfo.NowSimulation.OPCode = oqcRejectEventArgs.ProductInfo.CurrentItemRoute2OP.OPCode;
                    }
                    else
                    {
                        object objNextOP = (new BaseModelFacade(DataProvider)).GetNextOperationOfRoute(oqcRejectEventArgs.ProductInfo.LastSimulation.RouteCode, oqcRejectEventArgs.ProductInfo.LastSimulation.OPCode);
                        if (objNextOP != null)
                        {
                            oqcRejectEventArgs.ProductInfo.NowSimulation.OPCode = (objNextOP as Operation).OPCode;
                        }
                    }

                    oqcRejectEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.Reject;
                    if (actionCheckStatus.NeedUpdateSimulation == true)
                    {
                        //把资源暂时替换成onwip需要正确资源
                        string resCode = oqcRejectEventArgs.ResourceCode;
                        oqcRejectEventArgs.ResourceCode = actionEventArgs.rightResource;
                        newMessages.AddMessages(dataCollect.Execute(oqcRejectEventArgs));
                        //转换回原来的资源
                        oqcRejectEventArgs.ResourceCode = resCode;

                    }
                    #endregion
                    if (newMessages.IsSuccess())
                    {
                        if (oqcRejectEventArgs.ProductInfo.NowSimulation == null)
                        {
                            throw new Exception("$System_Error");
                        }


                        #region reject
                        Reject reject = reworkFacade.CreateNewReject();
                        reject.ItemCode = oqcRejectEventArgs.ProductInfo.NowSimulation.ItemCode;
                        reject.LOTNO = oqcRejectEventArgs.OQCLotNO;
                        reject.MaintainUser = oqcRejectEventArgs.UserCode;
                        reject.MOCode = oqcRejectEventArgs.ProductInfo.NowSimulation.MOCode;
                        reject.ModelCode = oqcRejectEventArgs.ProductInfo.NowSimulation.ModelCode;
                        reject.OPCode = oqcRejectEventArgs.ProductInfo.NowSimulation.OPCode;
                        reject.RejectDate = oqcRejectEventArgs.ProductInfo.NowSimulation.MaintainDate;
                        reject.RejectStatus = RejectStatus.Reject;
                        reject.RejectTime = oqcRejectEventArgs.ProductInfo.NowSimulation.MaintainTime;
                        reject.RejectUser = oqcRejectEventArgs.UserCode;
                        reject.ResourceCode = oqcRejectEventArgs.ResourceCode;
                        reject.RouteCode = oqcRejectEventArgs.ProductInfo.NowSimulation.RouteCode;
                        reject.RunningCard = oqcRejectEventArgs.RunningCard;
                        reject.RunningCardSequence = oqcRejectEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
                        reject.SegmentCode = oqcRejectEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
                        reject.SourceCard = oqcRejectEventArgs.ProductInfo.NowSimulation.SourceCard;
                        reject.SourceCardSequence = oqcRejectEventArgs.ProductInfo.NowSimulation.SourceCardSequence;
                        reject.StepSequenceCode = oqcRejectEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
                        reject.TranslateCard = oqcRejectEventArgs.ProductInfo.NowSimulation.TranslateCard;
                        reject.TranslateCardSequence = oqcRejectEventArgs.ProductInfo.NowSimulation.TranslateCardSequence;
                        reject.MOSeq = oqcRejectEventArgs.ProductInfo.NowSimulation.MOSeq;      // Added by Icyer 2007/07/02
                        reworkFacade.AddReject(reject);
                        #endregion
                    }

                    //Laws Lu,2005/08/25,新增
                    //样本如果有若干个已经经过“送修确认”转为“待修”状态（或者在维修中），那么批退不可以继续
                    //TSFacade tsFacade = new TSFacade(this._domainDataProvider);
                    //Laws Lu,2005/08/25,新增	删除TS中的记录
                    //Laws Lu,2005/08/31,修改	只有FQC工序送修才能够被删除
                    if (oqcRejectEventArgs.ActionType == ActionType.DataCollectAction_OQCReject
                        && ts != null
                        && ts.FromInputType == TS.TSFacade.TSSource_OnWIP
                        && ts.TSStatus == BenQGuru.eMES.Web.Helper.TSStatus.TSStatus_New)
                    {
                        if (ts.FromRouteCode == oqcRejectEventArgs.ProductInfo.LastSimulation.RouteCode &&
                            ts.FromOPCode == oqcRejectEventArgs.ProductInfo.LastSimulation.OPCode &&
                            ts.MOCode == oqcRejectEventArgs.ProductInfo.LastSimulation.MOCode &&
                            ts.RunningCardSequence == oqcRejectEventArgs.ProductInfo.LastSimulation.RunningCardSequence &&
                            ts.RunningCard == oqcRejectEventArgs.ProductInfo.LastSimulation.RunningCard)
                        {
                            tsFacade.DeleteCardInTS(
                                oqcRejectEventArgs.ProductInfo.LastSimulation.MOCode
                                , oqcRejectEventArgs.ProductInfo.LastSimulation.RunningCard
                                , oqcRejectEventArgs.ProductInfo.LastSimulation.RunningCardSequence.ToString());
                        }
                    }

                }
                catch (Exception ex)
                {
                    newMessages.Add(new Message(ex));
                }

                //preProduct=oqcRejectEventArgs.ProductInfo;
                i = i + 1;
            }

            //Laws Lu,2006/03/20 填写资源报表
            //Laws Lu,2006/03/21 支持混单
            Hashtable htOutput = new Hashtable();

            foreach (Simulation sim in Simulations)
            {
                Simulation tmpSim = null;
                foreach (Simulation x in htOutput.Keys)
                {
                    if (sim.MOCode == x.MOCode)
                    {
                        tmpSim = x;
                        break;
                    }
                }
                if (tmpSim != null)
                {
                    htOutput[tmpSim] = Convert.ToInt32(htOutput[tmpSim]) + 1;
                }
                else
                {
                    htOutput.Add(sim, 1);
                }
            }


            foreach (Simulation sim in htOutput.Keys)
            {
                ProductInfo product = null;

                Messages msgs = dataCollect.GetIDInfo(sim.RunningCard);
                if (msgs.IsSuccess())
                {
                    product = msgs.GetData().Values[0] as ProductInfo;

                }
                DBDateTime dbDateTime;

                dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                //				dcf.AdjustRouteOPOnline(
                //					product.LastSimulation.RunningCard,
                //					actionEventArgs.ActionType,
                //					actionEventArgs.ResourceCode,
                //					actionEventArgs.UserCode,
                //					product);

                product.NowSimulation.MaintainUser = actionEventArgs.UserCode;
                product.NowSimulation.MaintainDate = dbDateTime.DBDate;
                product.NowSimulation.MaintainTime = dbDateTime.DBTime;
                product.NowSimulation.LastAction = actionEventArgs.ActionType;
                product.NowSimulation.ProductStatus = ProductStatus.GOOD;
                product.NowSimulation.ResourceCode = Resource.ResourceCode;

                (new DataCollectFacade(DataProvider)).WriteSimulation(
                    product.LastSimulation.RunningCard
                    , ActionType.DataCollectAction_OQCPass
                    , actionEventArgs.ResourceCode
                    , actionEventArgs.UserCode
                    , product);


                product.NowSimulation.RouteCode = product.LastSimulation.RouteCode;
                product.NowSimulation.OPCode = product.LastSimulation.OPCode; ;

                product.NowSimulationReport = dataCollect.FillSimulationReport(product);
                //dataCollect.FillOnWip(pro);

                //newMessages.AddMessages((new ReportHelper(DataProvider)).SetReportResQuanMaster(
                //    DataProvider
                //    , actionEventArgs.ActionType
                //    , product
                //    , 0
                //    , Convert.ToInt32(htOutput[sim])
                //    , Convert.ToInt32(htOutput[sim])));
            }

            return newMessages;
        }
    }
}
