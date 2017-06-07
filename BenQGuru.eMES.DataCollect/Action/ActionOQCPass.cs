#region system;
using System;
using UserControl;
using System.Collections;
using System.Data.SqlTypes;
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
using BenQGuru.eMES.Domain.Report;
using BenQGuru.eMES.Report;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Rework;
#endregion



namespace BenQGuru.eMES.DataCollect.Action
{
    /// <summary>
    /// 归属工单采集
    /// </summary>
    public class ActionOQCPass : IActionWithStatus
    {
        private IDomainDataProvider _domainDataProvider = null;

        //		public ActionOQCPass()
        //		{	
        //		}

        public ActionOQCPass(IDomainDataProvider domainDataProvider)
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
            OQCPASSEventArgs oqcPassEventArgs = actionEventArgs as OQCPASSEventArgs;
            try
            {
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                ActionOQCHelper oqcHelper = new ActionOQCHelper(this.DataProvider);
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                TSFacade tsFacade = new TSFacade(this.DataProvider);

                object objLot = null;
                if (oqcPassEventArgs.Lot == null)
                {
                    objLot = oqcFacade.GetExamingOQCLot(oqcPassEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                }
                else
                {
                    objLot = oqcPassEventArgs.Lot;
                }
                if (objLot == null)
                {
                    throw new Exception("$Error_OQCLotNotExisted");
                }
                OQCLot oqcLot = objLot as OQCLot;

                if (!oqcPassEventArgs.IsForcePass)
                {
                    // Modified By Hi1/Venus.Feng on 20080916 for Hisense version
                    /*
                    //批中的实际检验产品数量应该不小于维护的抽检样本数量 joe song 2006-06-08
                    int act_sample_id = oqcFacade.ExactQueryOQCLot2CardCheckCount(oqcPassEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                    if (act_sample_id < oqcLot.SampleSize)
                        throw new Exception("$Error_OQC_Act_less_than_Sample");
                    */
                    object[] groupList = oqcFacade.GetCheckGroupList4LotCheck(oqcLot.LOTNO);
                    if (groupList != null && groupList.Length > 0)
                    {
                        string groupCodeList = "";
                        foreach (OQCLot2CheckGroup lot2checkgroup in groupList)
                        {
                            if (lot2checkgroup.CheckedCount < lot2checkgroup.NeedCheckCount)
                            {
                                groupCodeList += lot2checkgroup.CheckGroup + ",";
                            }
                        }
                        if (groupCodeList.Length > 0)
                        {
                            groupCodeList = groupCodeList.Substring(0, groupCodeList.Length - 1);
                            throw new Exception("$Error_OQC_Act_less_than_Sample $Domain_OQCCheckGroup：" + groupCodeList + " ");
                        }
                    }
                }

                object[] objsCard = null;

                if (oqcPassEventArgs.CardOfLot != null)
                {
                    objsCard = oqcPassEventArgs.CardOfLot;
                }
                else
                {
                    objsCard = oqcHelper.QueryCardOfLot(oqcPassEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                }

                if (objsCard == null)
                {
                    throw new Exception("$Error_OQCLot2CardNotExisted");
                }

                //如果机种中设定检查数据连线，批中的数据连线样本数量应该不小于产品别中设定的数值  joe song 2006-06-08
                if (objsCard != null && objsCard.Length > 0)
                {
                    Simulation sim = objsCard[0] as Simulation;
                    if (sim != null)
                    {
                        BenQGuru.eMES.Domain.MOModel.Model model = new BenQGuru.eMES.MOModel.ModelFacade(this.DataProvider).GetModel(sim.ModelCode, GlobalVariables.CurrentOrganizations.First().OrganizationID) as BenQGuru.eMES.Domain.MOModel.Model;
                        if (model != null && model.IsCheckDataLink == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING)
                        {
                            int datalinkcount = oqcFacade.GetLotDataLinkCardCount(oqcPassEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                            if (datalinkcount < model.DataLinkQty)
                            {
                                throw new Exception("$Error_OQC_Act_DataLink_less_than_Model");
                            }
                        }
                    }
                }
                //如果机种中设定检查尺寸，批中的尺寸数量应该不小于产品别中设定的数值  joe song 2006-06-08
                if (objsCard != null && objsCard.Length > 0)
                {
                    Simulation sim = objsCard[0] as Simulation;
                    if (sim != null)
                    {
                        BenQGuru.eMES.Domain.MOModel.Model model = new BenQGuru.eMES.MOModel.ModelFacade(this.DataProvider).GetModel(sim.ModelCode, GlobalVariables.CurrentOrganizations.First().OrganizationID) as BenQGuru.eMES.Domain.MOModel.Model;
                        if (model != null && model.IsDim == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING)
                        {
                            int dimcount = oqcFacade.GetLotDimentionCount(oqcPassEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                            if (dimcount < model.DimQty)
                            {
                                throw new Exception("$Error_OQC_Act_Dim_less_than_Model");
                            }
                        }
                    }
                }

                // Added By Hi1/venus.Feng on 20080801 for Hisense Version
                // Check Route and OP
                DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                ItemRoute2OP nextOP;
                // Get Current OP by Resource
                Operation2Resource op2Res = (new BaseModelFacade(this.DataProvider)).GetOperationByResource(oqcPassEventArgs.ResourceCode);

                // Check all rcard productstatus=Good 针对报废品不做检查
                foreach (Simulation si in objsCard)
                {
                    if (si.ProductStatus == ProductStatus.NG)
                    {
                        throw new Exception("$CS_RCardStatusIsNGCanNotPass $RCard=" + si.RunningCard);
                    }
                    if (si.ProductStatus == ProductStatus.Reject)
                    {
                        throw new Exception("$CS_IDisReject $RCard=" + si.RunningCard);
                    }
                }

                foreach (Simulation si in objsCard)
                {
                    // Get RCard Next OP
                    nextOP = dcf.GetMORouteNextOP(si.MOCode, si.RouteCode, si.OPCode);
                    if (nextOP != null && string.Compare(nextOP.OPCode, op2Res.OPCode, true) != 0)
                    {
                        throw new Exception("$CS_Route_Failed " + nextOP.OPCode + " $RCard=" + si.RunningCard);
                    }
                }

                // End Added

                //检查是否包含在批中的runningcard的最后一次检验的结果都为OK
                //add by crystal chu 2005/07/18,如果是抱费也是可以的
                #region 检查是否包含在批中的runningcard的最后一次检验的结果都为OK
                // Marked By Hi1/Venus.Feng on 20080801 for Hisense Version
                // Need not to check
                /*
                object[] objsCardCheckLastRecord = oqcFacade.QueryOQCLot2CardCheckLastRecord(string.Empty,string.Empty,string.Empty,oqcPassEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default.ToString(),ProductStatus.NG);
				if(objsCardCheckLastRecord != null)
				{
					//如果是其他则在TS进行查询，如果发现是报废，或者是拆解
					for(int i=0;i<objsCardCheckLastRecord.Length;i++)
					{
						//IsCardScrapeOrSplit]
						OQCLot2CardCheck cardCheckk = (OQCLot2CardCheck)objsCardCheckLastRecord[i];
						
						object[] objsTS = tsFacade.GetCardScrapeOrSplit(cardCheckk.RunningCard
							,cardCheckk.MOCode
							,cardCheckk.RunningCardSequence.ToString()
							);
						if(objsTS != null)
						{
							foreach(Domain.TS.TS ts in objsTS)
							{
								if(!oqcPassEventArgs.CardOfLotForDelete.Contains(ts.RunningCard))
								{
									oqcPassEventArgs.CardOfLotForDelete.Add(ts.RunningCard);
								}
							}
						}

						if((!tsFacade.IsCardScrapeOrSplit( 
							cardCheckk.RunningCard
							,cardCheckk.MOCode,cardCheckk.RunningCardSequence.ToString())) && cardCheckk.Status == ProductStatus.NG)
						{
							throw new Exception(String.Format("$Error_OQCLotNotAllGood"));
						}
					}
				}
				*/

                #endregion

                #region 检查是否包含在批中的runningcard的最后一次功能测试都为OK joe song 2006-06-10
                // Marked By Hi1/Venus.Feng on 20080801 for Hisense Version
                /*
                int funcCount = oqcFacade.QueryFuncTesCount(oqcPassEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default.ToString(),ProductStatus.NG);
				if(funcCount > 0)
					throw new Exception(String.Format("$Error_OQCLotNotAllGood"));
                */
                #endregion

                //object[] objsCard = oqcFacade.ExactQueryOQCLot2Card( oqcPassEventArgs.OQCLotNO,OQCFacade.Lot_Sequence_Default);

                //Laws Lu,2006/08/02 调整Update的先后顺序，将容易并发锁定的放在最后更新
                #region UpdateOQCLotCheckList Status
                object objLotCheckList = oqcFacade.GetOQCLOTCheckList(oqcPassEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default);
                if (objLotCheckList != null)
                {
                    OQCLOTCheckList oqcLotCheckList = objLotCheckList as OQCLOTCheckList;
                    oqcLotCheckList.Result = OQCLotStatus.OQCLotStatus_Pass;
                    oqcFacade.UpdateOQCLOTCheckList(oqcLotCheckList);
                }
                #endregion

                #region 整个lot中的ID全变成Good
                oqcFacade.UpdateOQCLot2CardByOQCResult(oqcPassEventArgs.OQCLotNO, OQCFacade.Lot_Sequence_Default.ToString(), string.Empty, string.Empty, ProductStatus.GOOD, false);
                #endregion

                #region 对每个扳子进行处理
                //对全部处理
                messages.AddMessages(SetIDOQCGooD(messages, objsCard, oqcPassEventArgs, dataCollect));
                #endregion

                #region updateOQCLotStatus
                //Laws Lu,2005/11/01,修改	改善性能

                DBDateTime dbDateTime;
                //Laws Lu,2006/11/13 uniform system collect date
                if (actionEventArgs.ProductInfo != null && actionEventArgs.ProductInfo.WorkDateTime != null)
                {
                    dbDateTime = actionEventArgs.ProductInfo.WorkDateTime;
                }
                else
                {
                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                    if (actionEventArgs.ProductInfo != null)
                    {
                        actionEventArgs.ProductInfo.WorkDateTime = dbDateTime;
                    }

                }

                oqcLot.LOTStatus = OQCLotStatus.OQCLotStatus_Pass;
                oqcLot.DealDate = dbDateTime.DBDate;
                oqcLot.DealTime = dbDateTime.DBTime;
                oqcLot.DealUser = actionEventArgs.UserCode;
                oqcLot.Memo = oqcPassEventArgs.Memo;

                oqcFacade.UpdateOQCLotStatus(oqcLot);
                #endregion

                //add by roger.xue 2008/10/27
                if (oqcPassEventArgs.IsUnFrozen)
                {
                    oqcFacade.UpdateUnFrozenOnLot(oqcPassEventArgs.OQCLotNO, oqcPassEventArgs.UnFrozenReason,
                        dbDateTime.DBDate, dbDateTime.DBTime, oqcPassEventArgs.UserCode, OQCFacade.Lot_Sequence_Default);

                    oqcFacade.UnFreezeFrozen(oqcPassEventArgs.OQCLotNO, oqcPassEventArgs.UnFrozenReason,
                        dbDateTime.DBDate, dbDateTime.DBTime, oqcPassEventArgs.UserCode, OQCFacade.Lot_Sequence_Default);
                }
                //end add
                //add 自动入库 by alex.hu 2010/11/10
                //完工会自动入库
                //dcf.AutoInventory(oqcLot, oqcPassEventArgs.UserCode);
                
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        private Resource Resource;

        /// <summary>
        /// Laws Lu,2005/10/10,修改	处理并发更新报表数量
        /// </summary>
        /// <param name="messages"></param>
        /// <param name="Simulations">已经按MOCODE,ROUTECODE,OPCODE,FROMROUTE,FROMOP,RESCODE,PRODUCTSTATUS,LACTION,ACTIONLIST,NGTIMES,ISCOM排序</param>
        /// <param name="actionEventArgs"></param>
        /// <param name="dataCollect"></param>
        /// <returns></returns>
        private Messages SetIDOQCGooD(Messages messages, object[] Simulations, OQCPASSEventArgs actionEventArgs, ActionOnLineHelper dataCollect)
        {
            DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
            //ReportFacade reportFacade = new ReportFacade(this.DataProvider);
            ReportHelper reportHelper = new ReportHelper(this.DataProvider);
            TSFacade tsFacade = new TSFacade(this.DataProvider);
            //MOFacade moFacade = new MOFacade(this.DataProvider);
            //ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this.DataProvider);
            //ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            //ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);
            Messages newMessages = new Messages();
            //ProductInfo preProduct =null;
            //			ReportRealtimeLineQty reportRealtimeLineQty = null;	
            //			ReportHistoryOPQty reportHistoryOPQty = null;
            //			ItemRoute2OP itemRoute2OP=null;
            //int moOutput=0;
            int i = 0;
            bool bFlag = false;
            bool NotNeedProcess = false;
            ArrayList arCardList = new ArrayList();

            Hashtable htSubstract = new Hashtable();

            //			bool isOpChanged=true;
            //			TimePeriod timePeriod=null;
            ActionCheckStatus actionCheckStatus = new ActionCheckStatus();

            if (Resource == null)
            {
                BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
                Resource = (Domain.BaseSetting.Resource)dataModel.GetResource(actionEventArgs.ResourceCode);
            }

            //ArrayList rptList = new ArrayList();
            Hashtable products = new Hashtable();
            while (i < Simulations.Length)
            {     //针对报废品，不过账
                if (((Simulation)Simulations[i]).ProductStatus != ProductStatus.Scrap)
                {
                    if (actionEventArgs.CardOfLotForDelete != null && actionEventArgs.CardOfLotForDelete.Contains(((Simulation)Simulations[i]).RunningCard))
                    {
                        Simulation sim = (Simulation)Simulations[i];
                        tsFacade.DeleteCardInTS(
                            sim.MOCode
                            , sim.RunningCard
                            , sim.RunningCardSequence.ToString());

                        //Laws Lu,2006/03/21 
                        if (htSubstract.ContainsKey((Simulations[i] as Simulation).MOCode))
                        {
                            htSubstract[sim.MOCode] = Convert.ToInt32(htSubstract[sim.MOCode]) + 1;
                        }
                        else
                        {
                            htSubstract.Add(sim.MOCode, 1);
                        }
                    }
                    else
                    {
                        #region 正常处理
                        ProductInfo productionInf = dataCollect.GetIDInfoBySimulation((Simulation)Simulations[i]);
                        //Laws Lu,2005/11/30,新增	产品信息缓存
                        Simulation sim = (Simulation)Simulations[i];

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

                        //bool needCheck = true;

                        OQCPASSEventArgs oqcPassEventArgs = new OQCPASSEventArgs(ActionType.DataCollectAction_OQCPass,
                            ((Simulation)Simulations[i]).RunningCard, actionEventArgs.UserCode, actionEventArgs.ResourceCode, actionEventArgs.OQCLotNO,
                            productionInf);


                        try
                        {
                            #region 保存WIP
                            //					if (preProduct != null)
                            //					{
                            //						if (dataCollect.CompareSimulationCheck(preProduct.LastSimulation,oqcPassEventArgs.ProductInfo.LastSimulation))
                            //						{
                            //							dataCollect.CopyProduct(preProduct,oqcPassEventArgs.ProductInfo);
                            //
                            //							dataCollectFacade.WriteSimulationCheckOnlineOP(oqcPassEventArgs.RunningCard,oqcPassEventArgs.ActionType,oqcPassEventArgs.ResourceCode,
                            //								oqcPassEventArgs.UserCode,oqcPassEventArgs.ProductInfo);
                            //
                            //							needCheck = false;
                            //						}
                            //					}
                            //					if (needCheck)		
                            //					{
                            oqcPassEventArgs.ProductInfo.NowSimulation.ProductStatus = ProductStatus.GOOD;

                            //					if (preProduct == null)
                            //					{

                            if (!NotNeedProcess)
                            {
                                newMessages.AddMessages(dataCollect.CheckID(oqcPassEventArgs, actionCheckStatus));
                                //					}
                                if (!newMessages.IsSuccess())
                                {
                                    return newMessages;
                                }
                            }

                            if (!bFlag)
                            {
                                actionEventArgs.isLastOp = dataCollectFacade.OPIsMORouteLastOP(
                                    oqcPassEventArgs.ProductInfo.NowSimulation.MOCode
                                    , oqcPassEventArgs.ProductInfo.NowSimulation.RouteCode
                                    , oqcPassEventArgs.ProductInfo.NowSimulation.OPCode);

                                bFlag = true;
                            }

                            //					}
                            //					else
                            //					{
                            //						if (!((oqcPassEventArgs.ProductInfo.NowSimulation.MOCode == preProduct.NowSimulation.MOCode)
                            //							&&(oqcPassEventArgs.ProductInfo.NowSimulation.RouteCode == preProduct.NowSimulation.RouteCode)
                            //							&&(oqcPassEventArgs.ProductInfo.NowSimulation.OPCode == preProduct.NowSimulation.OPCode)))
                            //						{
                            //							if(!bFlag)
                            //							{
                            //							actionEventArgs.isLastOp = dataCollectFacade.OPIsMORouteLastOP(
                            //								oqcPassEventArgs.ProductInfo.NowSimulation.MOCode
                            //								,oqcPassEventArgs.ProductInfo.NowSimulation.RouteCode
                            //								,oqcPassEventArgs.ProductInfo.NowSimulation.OPCode);
                            //
                            //								bFlag = true;
                            //							}
                            ////							isOpChanged = true;
                            //						}
                            ////						else
                            ////							isOpChanged = false;
                            //					}

                            //Laws Lu,2005/08/15,新增	完工逻辑，在其他Check都通过的情况下，所有的RunningCard应该是GOOD状态
                            //暂时不考虑线外工序
                            if (oqcPassEventArgs.ProductInfo.NowSimulation.RouteCode != "" && actionEventArgs.isLastOp)
                            {
                                oqcPassEventArgs.ProductInfo.NowSimulation.IsComplete = "1";
                                oqcPassEventArgs.ProductInfo.NowSimulation.EAttribute1 = "GOOD";
                                //完工自动入库
                                dataCollectFacade.AutoInventory((Simulation)Simulations[i], actionEventArgs.UserCode);
                            }
                            //End Laws Lu
                            if (!arCardList.Contains(oqcPassEventArgs.ProductInfo.LastSimulation.RunningCard))
                            {
                                arCardList.Add(oqcPassEventArgs.ProductInfo.LastSimulation.RunningCard);
                            }
                            if (actionCheckStatus.NeedUpdateSimulation == true && NotNeedProcess == false)
                            {
                                // Marked By Hi1/Venus.Feng on 20080806 for Hisense version
                                // 注释掉Execute是因为一个批里面的RCard，循环中的第一个RCard会插TBLONWIP两笔记录进去
                                // 不知道是否是原有的逻辑，所以暂时Mark起来了，如果因此导致了原有的逻辑丢失，请UnMark
                                //newMessages.AddMessages(dataCollect.Execute(oqcPassEventArgs));
                                // End Marked
                                NotNeedProcess = true;
                            }
                            // Marked By Hi1/Venus.Feng on 20080806 for Hisense version
                            // 注释掉Execute是因为一个批里面的RCard，循环中的第一个RCard会插TBLONWIP两笔记录进去
                            // 不知道是否是原有的逻辑，所以暂时Mark起来了，如果因此导致了原有的逻辑丢失，请UnMark
                            //else
                            //{
                            // End Marked
                            dataCollectFacade.WriteSimulation(
                                productionInf.LastSimulation.RunningCard
                                , productionInf.LastSimulation.LastAction
                                , productionInf.LastSimulation.ResourceCode
                                , actionEventArgs.UserCode, productionInf);

                            dataCollectFacade.AdjustRouteOPOnline(
                                productionInf.LastSimulation.RunningCard,
                                actionEventArgs.ActionType,
                                actionEventArgs.ResourceCode,
                                actionEventArgs.UserCode,
                                productionInf);

                            DBDateTime dbDateTime;
                            //Laws Lu,2006/11/13 uniform system collect date
                            if (actionEventArgs.ProductInfo != null && actionEventArgs.ProductInfo.WorkDateTime != null)
                            {
                                dbDateTime = actionEventArgs.ProductInfo.WorkDateTime;
                            }
                            else
                            {
                                dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                                if (actionEventArgs.ProductInfo != null)
                                {
                                    actionEventArgs.ProductInfo.WorkDateTime = dbDateTime;
                                }

                            }

                            productionInf.NowSimulation.MaintainUser = actionEventArgs.UserCode;

                            productionInf.NowSimulation.MaintainDate = dbDateTime.DBDate;
                            productionInf.NowSimulation.MaintainTime = dbDateTime.DBTime;

                            productionInf.NowSimulation.LastAction = actionEventArgs.ActionType;
                            productionInf.NowSimulation.ProductStatus = ProductStatus.GOOD;
                            productionInf.NowSimulation.ResourceCode = Resource.ResourceCode;

                            if (productionInf.NowSimulation.RouteCode != "" && actionEventArgs.isLastOp)
                            {
                                productionInf.NowSimulation.IsComplete = "1";
                                productionInf.NowSimulation.EAttribute1 = "GOOD";
                            }

                            Operation currrentOP = (new BaseModelFacade(DataProvider)).GetOperationByRouteAndResource(
                                ((ExtendSimulation)productionInf.LastSimulation).NextRouteCode,
                                Resource.ResourceCode) as Operation;

                            productionInf.NowSimulation.RouteCode = productionInf.LastSimulation.RouteCode;
                            productionInf.NowSimulation.OPCode = currrentOP.OPCode;

                            productionInf.NowSimulationReport = dataCollect.FillSimulationReport(productionInf);

                            dataCollect.FillOnWip(productionInf);
                            //}

                            bool isExist = false;
                            foreach (ProductInfo pr in products.Values)
                            {
                                if (pr.LastSimulation.RunningCard == sim.RunningCard)
                                {
                                    isExist = true;
                                    break;
                                }
                            }
                            if (false == isExist)
                            {
                                products.Add((Simulation)Simulations[i], productionInf);
                            }

                            if (!newMessages.IsSuccess())
                            {
                                return newMessages;
                            }


                            #endregion

                            //ReportHelper reportCollect = new ReportHelper(this.DataProvider);

                            #region Mark的报表逻辑,Laws Lu,2005/10/18 注释
                            //						if (moOutput >0 )
                            //						{
                            //							dataCollectFacade.UpdateMOOutPut(preProduct.NowSimulation.MOCode,moOutput);
                            //							moOutput=0;
                            //						}
                            //						if (reportRealtimeLineQty!=null)
                            //						{
                            //							if( (tsFacade.QueryTSCountByLine(
                            //								oqcGoodEventArgs.ProductInfo.NowSimulationReport.RunningCard,
                            //								oqcGoodEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode)!=0)
                            //								||(reworkFacade.IsInReject( oqcGoodEventArgs.ProductInfo.NowSimulationReport.RunningCard,
                            //								oqcGoodEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode)))
                            //							{
                            //							}
                            //							else
                            //							{
                            //								//Laws Lu,2005/10/09,修改	需要乘以分板比例
                            //								reportRealtimeLineQty.AllGoodQty += 1 * oqcGoodEventArgs.ProductInfo.NowSimulation.IDMergeRule;
                            //								allqty = Convert.ToInt32(1 * oqcGoodEventArgs.ProductInfo.NowSimulation.IDMergeRule);
                            //							}
                            //
                            //							reportFacade.ModifyReportRealtimeLineQty(reportRealtimeLineQty,1,0,allqty);
                            //							reportRealtimeLineQty=null;
                            //						}
                            //
                            //						//Laws  Lu,2005/10/13,新增	QtyFlag更改为主键
                            //						string flag = String.Empty;
                            //
                            //						if (isLastOp)//是否为最后工序
                            //							flag = "Y";
                            //						else
                            //							flag = "N";
                            //
                            //						object obj = reportFacade.GetReportRealtimeLineQty(
                            //							oqcGoodEventArgs.ProductInfo.NowSimulation.MOCode,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulationReport.SegmentCode,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulation.ItemCode,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftCode,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulationReport.ModelCode,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftDay,
                            //							flag);							
                            //						if(obj == null)
                            //						{
                            //							reportRealtimeLineQty = reportFacade.CreateNewReportRealtimeLineQty();
                            //							reportRealtimeLineQty.AllGoodQty=0;
                            //							reportRealtimeLineQty.Day=oqcGoodEventArgs.ProductInfo.NowSimulationReport.MaintainDate;
                            //							reportRealtimeLineQty.InputQty=0;
                            //							reportRealtimeLineQty.ItemCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ItemCode;
                            //							reportRealtimeLineQty.MOCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.MOCode;
                            //							reportRealtimeLineQty.ModelCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ModelCode;
                            //							reportRealtimeLineQty.NGTimes =0;
                            //							reportRealtimeLineQty.OuputQty =0;
                            //							reportRealtimeLineQty.ScrapQty =0;
                            //							reportRealtimeLineQty.SegmentCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
                            //							reportRealtimeLineQty.ShiftCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftCode;
                            //							reportRealtimeLineQty.ShiftDay =oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftDay;
                            //							reportRealtimeLineQty.StepSequenceCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
                            //							reportRealtimeLineQty.TimePeriodCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode;
                            //							if (timePeriod==null)
                            //							{										
                            //								timePeriod = (TimePeriod)shiftModelFacade.GetTimePeriod(oqcGoodEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode);
                            //								if(timePeriod == null)
                            //								{
                            //									throw new  Exception("$Error_TimePeriod_IS_Null");
                            //								}
                            //							}
                            //							reportRealtimeLineQty.TimePeriodBeginTime =   timePeriod.TimePeriodBeginTime;
                            //							reportRealtimeLineQty.TimePeriodEndTime= timePeriod.TimePeriodEndTime;
                            //							reportRealtimeLineQty.Week = reportHelper.WeekOfYear(reportRealtimeLineQty.ShiftDay.ToString());
                            //							//Laws Lu,2005/10/10,修改	最后工序,计算产量
                            //							//reportRealtimeLineQty.QtyFlag = "Y";
                            //							//							if (isLastOp)
                            //							//							{
                            //							//								reportRealtimeLineQty.QtyFlag = "Y";
                            //							//							}
                            //							//							else
                            //							//							{
                            //							//								reportRealtimeLineQty.QtyFlag = "N";
                            //							//							}
                            //							reportRealtimeLineQty.QtyFlag = flag;
                            //							reportRealtimeLineQty.Month = DateTime.Now.Month;
                            //
                            //							reportFacade.AddReportRealtimeLineQty(reportRealtimeLineQty);
                            //						}
                            //						else
                            //						{
                            //							reportRealtimeLineQty = obj as ReportRealtimeLineQty;
                            //						}
                            //						//是否是最后一站
                            //						//						if (isLastOp)
                            //						//						{
                            //						//							reportRealtimeLineQty.QtyFlag = "Y";
                            //						//						}
                            //						//						else
                            //						//						{
                            //						//							reportRealtimeLineQty.QtyFlag = "N";
                            //						//						}
                            //						reportRealtimeLineQty.QtyFlag = flag;
                            //
                            //						//是否是中间站						
                            //						itemRoute2OP = (ItemRoute2OP)itemFacade.GetItemRoute2Operation(oqcGoodEventArgs.ProductInfo.NowSimulation.ItemCode,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulation.RouteCode,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulation.OPCode);
                            //					}
                            //					if( isLastOp
                            //						|| FormatHelper.StringToBoolean( itemRoute2OP.OPControl,(int)OperationList.MidistOutput))
                            //					{
                            //						//Laws Lu,2005/10/09,修改	需要乘以分板比例
                            //						reportRealtimeLineQty.OuputQty += 1 * oqcGoodEventArgs.ProductInfo.NowSimulation.IDMergeRule;	
                            //						qty = Convert.ToInt32(1 * oqcGoodEventArgs.ProductInfo.NowSimulation.IDMergeRule);	
                            //					}
                            //					if( (tsFacade.QueryTSCountByLine(
                            //						oqcGoodEventArgs.ProductInfo.NowSimulationReport.RunningCard,
                            //						oqcGoodEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode)!=0)
                            //						||(reworkFacade.IsInReject( oqcGoodEventArgs.ProductInfo.NowSimulationReport.RunningCard,
                            //						oqcGoodEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode)))
                            //					{
                            //					}
                            //					else
                            //					{
                            //						//Laws Lu,2005/10/09,修改	需要乘以分板比例
                            //						reportRealtimeLineQty.AllGoodQty += 1 * oqcGoodEventArgs.ProductInfo.NowSimulation.IDMergeRule;
                            //						allqty = Convert.ToInt32(1 * oqcGoodEventArgs.ProductInfo.NowSimulation.IDMergeRule);
                            //					}
                            //					//if (isLastOp)
                            //					//	moOutput += 1;					
                            //					if (i==Simulations.Length -1)
                            //					{
                            //						//Laws Lu,2005/10/10,修改	处理并发问题
                            //						reportFacade.ModifyReportRealtimeLineQty(reportRealtimeLineQty,qty,ngtimes,allqty);
                            //						
                            //						reportRealtimeLineQty =null;
                            //						//						if (isLastOp)
                            //						//						{
                            //						//							(new MOFacade(this.DataProvider)).UpdateMOOutPutQty(oqcGoodEventArgs.ProductInfo.NowSimulationReport.MOCode,reportFacade.GetMOOutPutQty(oqcGoodEventArgs.ProductInfo.NowSimulationReport.MOCode));
                            //						//						}
                            //						//moOutput=0;
                            //					}
                            //
                            //					if (isOpChanged)
                            //					{
                            //						if (reportHistoryOPQty!=null)
                            //						{
                            //							reportFacade.ModifyReportHistoryOPQty(reportHistoryOPQty,qty,ngtimes);
                            //							reportHistoryOPQty=null;
                            //						}
                            //						
                            //						//Laws  Lu,2005/10/13,新增	QtyFlag更改为主键
                            //						string flag = String.Empty;
                            //
                            //						if (isLastOp)//是否为最后工序
                            //							flag = "Y";
                            //						else
                            //							flag = "N";
                            //
                            //
                            //						object obj = reportFacade.GetReportHistoryOPQty(oqcGoodEventArgs.ProductInfo.NowSimulationReport.ModelCode,oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftDay,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulationReport.MOCode,oqcGoodEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode,oqcGoodEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode,oqcGoodEventArgs.ProductInfo.NowSimulationReport.SegmentCode,
                            //							oqcGoodEventArgs.ProductInfo.NowSimulationReport.ItemCode,oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftCode,oqcGoodEventArgs.ProductInfo.NowSimulationReport.OPCode,oqcGoodEventArgs.ProductInfo.NowSimulationReport.ResourceCode,flag);
                            //						if(obj == null)
                            //						{
                            //							reportHistoryOPQty = reportFacade.CreateNewReportHistoryOPQty();
                            //						
                            //							reportHistoryOPQty.Day= oqcGoodEventArgs.ProductInfo.NowSimulationReport.MaintainDate;
                            //							reportHistoryOPQty.ItemCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ItemCode;
                            //							reportHistoryOPQty.MOCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.MOCode;
                            //							reportHistoryOPQty.ModelCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ModelCode;
                            //							reportHistoryOPQty.NGTimes =0;
                            //							reportHistoryOPQty.OuputQty =0;
                            //							//reportHistoryOPQty.QtyFlag = "N";
                            //							reportHistoryOPQty.ShiftCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftCode;
                            //							reportHistoryOPQty.ShiftDay =oqcGoodEventArgs.ProductInfo.NowSimulationReport.ShiftDay;
                            //							reportHistoryOPQty.StepSequenceCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
                            //							reportHistoryOPQty.TimePeriodCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode;
                            //							reportHistoryOPQty.AllGoodQty =0;
                            //							reportHistoryOPQty.ResourceCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.ResourceCode;
                            //							reportHistoryOPQty.SegmnetCode = oqcGoodEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
                            //							//Laws Lu,2005/09/26,修改	
                            //							reportHistoryOPQty.Week = reportHelper.WeekOfYear(reportHistoryOPQty.ShiftDay.ToString());
                            //							reportHistoryOPQty.OPCode =  oqcGoodEventArgs.ProductInfo.NowSimulationReport.OPCode;
                            //							reportHistoryOPQty.Month = DateTime.Now.Month;
                            //							reportHistoryOPQty.AllGoodQty =0;
                            //
                            //							reportHistoryOPQty.QtyFlag = flag;
                            //						
                            //							reportFacade.AddReportHistoryOPQty(reportHistoryOPQty);
                            //						}
                            //						else
                            //						{
                            //							reportHistoryOPQty = obj as ReportHistoryOPQty;
                            //						}
                            //
                            //						
                            //					}
                            //					if (oqcGoodEventArgs.ProductInfo.NowSimulation.ProductStatus == ProductStatus.GOOD)
                            //					{					
                            //						//Laws Lu,2005/10/09,修改	需要乘以分板比例
                            //						reportHistoryOPQty.OuputQty += 1 * oqcGoodEventArgs.ProductInfo.NowSimulation.IDMergeRule;
                            //						qty = Convert.ToInt32(1 * oqcGoodEventArgs.ProductInfo.NowSimulation.IDMergeRule);
                            //					}
                            //					else if (oqcGoodEventArgs.ProductInfo.NowSimulation.ProductStatus == ProductStatus.NG)
                            //					{
                            //						//Laws Lu,2005/10/09,修改	需要乘以分板比例
                            //						reportHistoryOPQty.NGTimes += 1 * oqcGoodEventArgs.ProductInfo.NowSimulation.IDMergeRule;
                            //						ngtimes = Convert.ToInt32(1 * oqcGoodEventArgs.ProductInfo.NowSimulation.IDMergeRule);
                            //					}
                            //					if (i==Simulations.Length -1)
                            //					{
                            //						//Laws Lu,2005/10/10,修改	处理并发问题
                            //						reportFacade.ModifyReportHistoryOPQty(reportHistoryOPQty,qty,ngtimes);
                            //						reportHistoryOPQty=null;
                            //					}


                            #endregion


                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        #endregion

                    }                  
                }
                i = i + 1;
            }

            #region FQC性能调优

            OQCPASSEventArgs args = null;
            if (products != null && products.Count > 0)
            {
                ProductInfo productionInf = null;
                foreach (ProductInfo product in products.Values)
                {
                    if (product != null)
                    {
                        productionInf = product;
                        break;
                    }
                }
                #region 填写必要信息

                //				args = new OQCPASSEventArgs(
                //					ActionType.DataCollectAction_OQCPass
                //					,productionInf.LastSimulation.RunningCard
                //					,actionEventArgs.UserCode
                //					,actionEventArgs.ResourceCode
                //					,actionEventArgs.OQCLotNO,
                //					productionInf);
                #endregion

                #region 拼接SQL语句
                //需要修改，没有支持多工单
                //ProductInfo productionInf = products.Keys as ProductInfo;
                string runningCards = "('" + String.Join("','", (string[])arCardList.ToArray(typeof(string))) + "')";
                string strUpdateSim = "UPDATE TBLSIMULATION SET "
                    + "ishold='" + productionInf.NowSimulation.IsHold
                    + "',mtime='" + productionInf.NowSimulation.MaintainTime
                    + "',mdate='" + productionInf.NowSimulation.MaintainDate + "',"
                    + "muser='" + productionInf.NowSimulation.MaintainUser
                    + "',actionlist='" + productionInf.NowSimulation.ActionList + ActionType.DataCollectAction_OQCPass + "',"
                    + "productstatus='" + productionInf.NowSimulation.ProductStatus
                    + "',lotno='" + productionInf.NowSimulation.LOTNO
                    + "',laction='" + productionInf.NowSimulation.LastAction
                    + "',opcode='" + productionInf.NowSimulation.OPCode + "',"
                    + "rcardseq= rcardseq + 1,rescode='" + productionInf.NowSimulation.ResourceCode
                    + "',iscom='" + productionInf.NowSimulation.IsComplete
                    + "',eattribute1='" + productionInf.NowSimulation.EAttribute1
                    + "' where rcard in " + runningCards + " and lotno='" + actionEventArgs.OQCLotNO.ToUpper() + "'";  //+ " and mocode='" + productionInf.NowSimulation.MOCode + "'";

                string strUpdateSimRpt = "UPDATE TBLSIMULATIONREPORT SET "
                    + "tpcode='" + productionInf.NowSimulationReport.TimePeriodCode
                    + "',shiftcode='" + productionInf.NowSimulationReport.ShiftCode
                    + "',shifttypecode='" + productionInf.NowSimulationReport.ShiftTypeCode
                    + "',status='" + productionInf.NowSimulation.ProductStatus
                    + "',shiftday=" + productionInf.NowSimulationReport.ShiftDay
                    + ",laction='" + productionInf.NowSimulation.LastAction + "'"
                    + ",lotno='" + productionInf.NowSimulation.LOTNO + "',rcardseq= rcardseq + 1"
                    + ",eattribute1='" + productionInf.NowSimulation.EAttribute1
                    + "',iscom='" + productionInf.NowSimulation.IsComplete + "',rescode='"
                    + productionInf.NowSimulation.ResourceCode
                    + "',segcode='" + productionInf.NowSimulationReport.SegmentCode
                    + "',sscode='" + productionInf.NowSimulationReport.StepSequenceCode
                    + "',itemcode='" + productionInf.NowSimulation.ItemCode + "',"
                    + "opcode='" + productionInf.NowSimulation.OPCode
                    + "',mtime=" + productionInf.NowSimulation.MaintainTime
                    + ",mdate=" + productionInf.NowSimulation.MaintainDate
                    + ",muser='" + productionInf.NowSimulation.MaintainUser + "',"
                    + "routecode='" + productionInf.NowSimulation.RouteCode + "'"
                    + " where rcard in " + runningCards + " and lotno='" + actionEventArgs.OQCLotNO.ToUpper() + "'"; //+ " and mocode='" + productionInf.NowSimulation.MOCode + "'";
                #endregion

                int iReturn = 0;
                //更新SimulaitionReport
                iReturn = (DataProvider as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider).PersistBroker.ExecuteWithReturn(strUpdateSim);
                if (iReturn <= 0)
                {
                    throw new Exception("$Error_Command_Execute");
                }

                //更新Simulaition
                iReturn = (DataProvider as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider).PersistBroker.ExecuteWithReturn(strUpdateSimRpt);
                if (iReturn <= 0)
                {
                    throw new Exception("$Error_Command_Execute");
                }

                //填写Onwip
                string sqlForOnWip = string.Empty;
                sqlForOnWip += "SELECT shifttypecode, shiftcode, tpcode, itemcode, status AS actionresult, laction AS action, ";
                sqlForOnWip += "scard, tcard, rcardseq, muser, mdate, ngtimes, ";
                sqlForOnWip += "mtime, scardseq, routecode, mocode, segcode, tcardseq, ";
                sqlForOnWip += "sscode, rcard, opcode, shiftday, rescode, modelcode, eattribute1, moseq ";
                sqlForOnWip += "FROM tblsimulationreport ";
                sqlForOnWip += "WHERE rcard IN " + runningCards + " ";
                sqlForOnWip += "AND lotno = '" + actionEventArgs.OQCLotNO.ToUpper() + "' ";

                object[] onWIPArray = this.DataProvider.CustomQuery(typeof(OnWIP), new SQLCondition(sqlForOnWip));
                if (onWIPArray == null || onWIPArray.Length <= 0)
                {
                    throw new Exception("$Error_Command_Execute");
                }

                foreach (OnWIP onWIP in onWIPArray)
                {
                    dataCollectFacade.AddOnWIP(onWIP);
                }
            }
            #endregion

            //填写产量报表
            //reportHelper.FqcPassQty(products, Simulations, actionEventArgs);

            //Laws Lu,2006/03/20 填写资源报表
            //Laws Lu,2006/03/21 支持混单
            Hashtable htOutput = new Hashtable();


            ArrayList objs = new ArrayList();

            //分工单存储
            foreach (ProductInfo productionInf in products.Values)
            {
                ProductInfo tmpPro = null;
                foreach (ProductInfo pro in htOutput.Keys)
                {
                    if (pro.LastSimulation.MOCode == productionInf.LastSimulation.MOCode)
                    {
                        tmpPro = pro;
                        break;
                    }
                }

                if (tmpPro != null)
                {

                    htOutput[tmpPro] = Convert.ToInt32(htOutput[tmpPro]) + 1;
                }
                else
                {
                    htOutput.Add(productionInf, 1);
                }

                //				pro = productionInf;
                //				break;
            }



            foreach (ProductInfo prod in htOutput.Keys)
            {
                ProductInfo product = null;
                Messages msgs = dataCollect.GetIDInfo(prod.LastSimulation.RunningCard);
                if (msgs.IsSuccess())
                {
                    product = msgs.GetData().Values[0] as ProductInfo;

                }

                dataCollectFacade.AdjustRouteOPOnline(
                    product.LastSimulation.RunningCard,
                    actionEventArgs.ActionType,
                    actionEventArgs.ResourceCode,
                    actionEventArgs.UserCode,
                    product);

                DBDateTime dbDateTime;
                //Laws Lu,2006/11/13 uniform system collect date
                if (prod.WorkDateTime != null)
                {
                    dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                    prod.WorkDateTime = dbDateTime;
                }
                else
                {
                    dbDateTime = prod.WorkDateTime;
                }

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
                product.NowSimulation.OPCode = product.LastSimulation.OPCode;

                product.NowSimulationReport = dataCollect.FillSimulationReport(product);
                //dataCollect.FillOnWip(pro);
                //计算实际产量
                int iInput = Convert.ToInt32(htOutput[prod])
                    + Convert.ToInt32((htSubstract[product.LastSimulation.MOCode] == null ? 0 : htSubstract[product.LastSimulation.MOCode]));
                //newMessages.AddMessages((new ReportHelper(DataProvider)).SetReportResQuanMaster(
                //    DataProvider
                //    , actionEventArgs.ActionType
                //    , product
                //    , Convert.ToInt32(htOutput[prod])
                //    , iInput
                //    , 1));
            }

            return newMessages;
        }
    }
}
