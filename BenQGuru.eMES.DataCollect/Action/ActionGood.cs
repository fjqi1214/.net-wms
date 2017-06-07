using System;
using UserControl;
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
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Rework;

namespace BenQGuru.eMES.DataCollect.Action
{
    /// <summary>
    /// 归属工单采集
    /// </summary>
    public class ActionGood : IActionWithStatus
    {
        private IDomainDataProvider _domainDataProvider = null;

        //		public ActionGood()
        //		{	
        //		}

        public ActionGood(IDomainDataProvider domainDataProvider)
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
                messages.AddMessages((new ActionUndoNG(this.DataProvider)).UndoNG(actionEventArgs));
                // Added end
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(actionEventArgs));
                if (messages.IsSuccess())
                {
                    //Laws Lu,2005/08/15,新增	完工逻辑，在其他Check都通过的情况下，所有的RunningCard应该是GOOD状态
                    //暂时不考虑线外工序
                    DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                    if (actionEventArgs.ProductInfo.NowSimulation.RouteCode != "" && dataCollectFacade.OPIsMORouteLastOP(
                        actionEventArgs.ProductInfo.NowSimulation.MOCode
                        , actionEventArgs.ProductInfo.NowSimulation.RouteCode
                        , actionEventArgs.ProductInfo.NowSimulation.OPCode))
                    {
                        actionEventArgs.ProductInfo.NowSimulation.IsComplete = "1";
                        actionEventArgs.ProductInfo.NowSimulation.EAttribute1 = "GOOD";
                        //完工自动入库
                        dataCollectFacade.AutoInventory(actionEventArgs.ProductInfo.NowSimulation, actionEventArgs.UserCode);
                    }
                    //End Laws Lu                    
                    messages.AddMessages(dataCollect.Execute(actionEventArgs));
                    if (messages.IsSuccess())
                    {
                        // 自动产生送检批
                        messages.AddMessages(this.GenerateLot(actionEventArgs));
                        //if (messages.IsSuccess())
                        //{
                        //    //填写测试报表 TODO
                        //    ReportHelper reportCollect = new ReportHelper(this.DataProvider);
                        //    messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo));
                        //    messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo));
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
                messages.AddMessages((new ActionUndoNG(this.DataProvider)).UndoNG(actionEventArgs));
                // Added end
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(actionEventArgs, actionCheckStatus));

                if (messages.IsSuccess())
                {
                    //Laws Lu,2005/08/15,新增	完工逻辑，在其他Check都通过的情况下，所有的RunningCard应该是GOOD状态
                    //暂时不考虑线外工序
                    DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                    if (actionEventArgs.ProductInfo.NowSimulation.RouteCode != "" && dataCollectFacade.OPIsMORouteLastOP(
                        actionEventArgs.ProductInfo.NowSimulation.MOCode
                        , actionEventArgs.ProductInfo.NowSimulation.RouteCode
                        , actionEventArgs.ProductInfo.NowSimulation.OPCode))
                    {
                        actionEventArgs.ProductInfo.NowSimulation.IsComplete = "1";
                        actionEventArgs.ProductInfo.NowSimulation.EAttribute1 = "GOOD";
                        //完工自动入库
                        dataCollectFacade.AutoInventory(actionEventArgs.ProductInfo.NowSimulation, actionEventArgs.UserCode);
                    }
                    //End Laws Lu
                    if (actionCheckStatus.NeedUpdateSimulation == true)
                    {
                        messages.AddMessages(dataCollect.Execute(actionEventArgs));
                    }
                    else
                    {
                        messages.AddMessages(dataCollect.Execute(actionEventArgs, actionCheckStatus));
                    }
                    if (messages.IsSuccess())
                    {
                        // 自动产生送检批
                        messages.AddMessages(this.GenerateLot(actionEventArgs));
                        if (messages.IsSuccess())
                        {
                            //填写测试报表 TODO
                            //if (actionCheckStatus.NeedFillReport == true)
                            //{
                            //    ReportHelper reportCollect = new ReportHelper(this.DataProvider);
                            //    messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo, actionCheckStatus));
                            //    messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo, actionCheckStatus));
                            //}

                            //将Action加入列表
                            actionCheckStatus.ActionList.Add(actionEventArgs);
                        }
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

        public Messages GenerateLot(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);

            try
            {
                string itemCode = actionEventArgs.ProductInfo.NowSimulation.ItemCode;
                string rCard = actionEventArgs.ProductInfo.NowSimulation.RunningCard;
                string moCode = actionEventArgs.ProductInfo.NowSimulation.MOCode;

                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);

                object item = itemFacade.GetItem(itemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (item == null)
                {
                    messages.Add(new Message(MessageType.Error, "$Error_ItemCode_NotExist $Domain_ItemCode=" + itemCode));
                    return messages;
                }
                //if (((Item)item).CheckItemOP == null || ((Item)item).CheckItemOP.Trim().Length == 0)
                //{
                //    messages.Add(new Message(MessageType.Error, "$Error_NoItemGenerateLotOPCode $Domain_ItemCode=" + itemCode));
                //    return messages;
                //}
                
                DBDateTime currentDBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                OQCLot lot;

                // Resource auto generate lotno for ReworkSheet
                Resource res = actionEventArgs.ProductInfo.Resource;
                if (res == null)
                {
                    res = (Resource)baseModelFacade.GetResource(actionEventArgs.ResourceCode);
                    actionEventArgs.ProductInfo.Resource = res;
                }

                ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);
                ReworkRange reworkRange = null;
                OQCLot currentLot = (OQCLot)oqcFacade.GetLatestOQCLot(actionEventArgs.RunningCard);
                if (currentLot != null && currentLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject)
                {
                    reworkRange = (ReworkRange)reworkFacade.GetLatestReworkRange(actionEventArgs.RunningCard);
                }

                //产生新批的情况有两种
                //情况一：返工Res（Res.ReworkRouteCode不为空），RCard有当前的返工需求单，且此返工需求单要求自动产生新批（ReworkSheet.AutoLot等于Y）
                //情况二：其他情况，当前工序为Item的产生批工序（当前工序等于Item.CheckItemOP）
                //
                if (res != null && res.ReworkRouteCode != null && res.ReworkRouteCode.Trim().Length > 0
                    && reworkRange != null && reworkRange.ReworkCode != null && reworkRange.ReworkCode.Trim().Length > 0)
                {
                    object reworkSheet = reworkFacade.GetReworkSheet(reworkRange.ReworkCode);
                    ReworkSheet rs = reworkSheet as ReworkSheet;

                    if (rs.AutoLot != null && string.Compare(rs.AutoLot, "Y", true) == 0
                        && rs.LotList != null && rs.LotList.Trim().Length > 0)
                    {
                        //Get rework lot by oldlotno (reworkcode=oldlotno=lotlist)
                        object rLot = oqcFacade.GetOQCLotByOldLotNo(rs.LotList);
                        if (rLot == null)
                        {
                            lot = this.CreateNewLot(actionEventArgs, (Item)item, currentDBDateTime, true, rs.LotList);
                        }
                        else
                        {
                            lot = rLot as OQCLot;
                        }
                    }
                    else
                    {
                        return messages;
                    }                  
                }
                else
                {
                    if (string.Compare(((Item)item).CheckItemOP, actionEventArgs.ProductInfo.NowSimulation.OPCode, true) != 0)
                    {
                        return messages;
                    }

                    // GetLot2Card By RCard+LotStatus
                    if (oqcFacade.IsCardUsedByAnyLot(rCard, moCode, itemCode))
                    {
                        return messages;
                    }
                    else
                    {
                        lot = this.CreateNewLot(actionEventArgs, (Item)item, currentDBDateTime, false, "");
                    }
                }

                object oldLot2Card = oqcFacade.GetOQCLot2Card(actionEventArgs.ProductInfo.NowSimulation.RunningCard,
                    actionEventArgs.ProductInfo.NowSimulation.MOCode, lot.LOTNO, lot.LotSequence);
                if (oldLot2Card != null)
                {
                    //messages.Add(new Message(MessageType.Error, "$Error_IDHasExistedInOtherOQCLotNO $CS_LotNo=" + lot.LOTNO));
                    return messages;
                }

                actionEventArgs.ProductInfo.NowSimulation.LOTNO = lot.LOTNO;
                actionEventArgs.ProductInfo.NowSimulationReport.LOTNO = lot.LOTNO;
                DataCollectFacade dataCollect = new DataCollectFacade(this.DataProvider);
                dataCollect.UpdateSimulation(actionEventArgs.ProductInfo.NowSimulation);
                dataCollect.UpdateSimulationReport(actionEventArgs.ProductInfo.NowSimulationReport);

                OQCLot2Card oqcLot2Card = oqcFacade.CreateNewOQCLot2Card();
                oqcLot2Card.ItemCode = actionEventArgs.ProductInfo.NowSimulation.ItemCode;
                oqcLot2Card.CollectType = "pcs";
                oqcLot2Card.LOTNO = lot.LOTNO;
                oqcLot2Card.LotSequence = OQCFacade.Lot_Sequence_Default;
                oqcLot2Card.MaintainUser = actionEventArgs.UserCode;
                oqcLot2Card.MaintainDate = currentDBDateTime.DBDate;
                oqcLot2Card.MaintainTime = currentDBDateTime.DBTime;
                oqcLot2Card.MOCode = actionEventArgs.ProductInfo.NowSimulation.MOCode;
                oqcLot2Card.ModelCode = actionEventArgs.ProductInfo.NowSimulation.ModelCode;
                oqcLot2Card.OPCode = actionEventArgs.ProductInfo.NowSimulation.OPCode;
                oqcLot2Card.ResourceCode = actionEventArgs.ProductInfo.NowSimulation.ResourceCode;
                oqcLot2Card.RouteCode = actionEventArgs.ProductInfo.NowSimulation.RouteCode;
                oqcLot2Card.RunningCard = actionEventArgs.ProductInfo.NowSimulation.RunningCard;
                oqcLot2Card.RunningCardSequence = actionEventArgs.ProductInfo.NowSimulation.RunningCardSequence;
                oqcLot2Card.SegmnetCode = actionEventArgs.ProductInfo.NowSimulationReport.SegmentCode;
                oqcLot2Card.ShiftCode = actionEventArgs.ProductInfo.NowSimulationReport.ShiftCode;
                oqcLot2Card.ShiftTypeCode = actionEventArgs.ProductInfo.NowSimulationReport.ShiftTypeCode;
                oqcLot2Card.Status = actionEventArgs.ProductInfo.NowSimulationReport.Status;
                oqcLot2Card.StepSequenceCode = actionEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode;
                oqcLot2Card.TimePeriodCode = actionEventArgs.ProductInfo.NowSimulationReport.TimePeriodCode;
                //oqcLot2Card.EAttribute1 = actionEventArgs.ProductInfo.NowSimulation.CartonCode;
                oqcLot2Card.EAttribute1 = ""; //现在这里只能是空，后面Carton包装时候会更新为CartonCode
                oqcLot2Card.MOSeq = actionEventArgs.ProductInfo.NowSimulation.MOSeq;

                oqcFacade.AddOQCLot2Card(oqcLot2Card);

                // Added By Hi1/Venus.Feng on 20081027 for Hisense Version : Add Frozen by lot logic
                if (lot.FrozenStatus == FrozenStatus.STATUS_FRONZEN)
                {
                    Frozen frozen = new Frozen();
                    frozen.RCard = actionEventArgs.ProductInfo.NowSimulation.RunningCard;
                    frozen.EAttribute1 = "";
                    frozen.FrozenBy = lot.FrozenBy;
                    frozen.FrozenDate = lot.FrozenDate;
                    frozen.FrozenReason = lot.FrozenReason;

                    int seq = 0;
                    object[] oldFrozenRCard = oqcFacade.QueryFrozen(frozen.RCard, frozen.RCard,
                        string.Empty, string.Empty, string.Empty, string.Empty,
                        -1, -1, -1, -1, int.MinValue, int.MaxValue);

                    if (oldFrozenRCard != null)
                    {
                        foreach (Frozen f in oldFrozenRCard)
                        {
                            seq = Math.Max(seq, f.FrozenSequence);
                        }
                    }

                    frozen.FrozenSequence = seq + 1;
                    frozen.FrozenStatus = FrozenStatus.STATUS_FRONZEN;
                    frozen.FrozenTime = lot.FrozenTime;
                    frozen.ItemCode = actionEventArgs.ProductInfo.NowSimulation.ItemCode;
                    frozen.LotNo = lot.LOTNO;
                    frozen.LotSequence = Convert.ToInt32(lot.LotSequence);
                    frozen.MaintainDate = currentDBDateTime.DBDate;
                    frozen.MaintainTime = currentDBDateTime.DBTime;
                    frozen.MaintainUser = actionEventArgs.UserCode;
                    frozen.MOCode = actionEventArgs.ProductInfo.NowSimulation.MOCode;
                    frozen.ModelCode = actionEventArgs.ProductInfo.NowSimulation.ModelCode;
                    frozen.UnfrozenBy = "";
                    frozen.UnfrozenDate = 0;
                    frozen.UnfrozenReason = "";
                    frozen.UnfrozenTime = 0;

                    oqcFacade.AddFrozen(frozen);
                }
                // End Added

                //Update tbloqclot.lotsize++
                lot.LotSize = 1;
                oqcFacade.UpdateOQCLotSize(lot);
            }
            catch (Exception ex)
            {
                messages.Add(new Message(ex));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        private OQCLot CreateNewLot(ActionEventArgs actionEventArgs, Item item, DBDateTime currentDBDateTime, bool forRework, string oldLotNo)
        {
            string lotNoMarkCode = forRework ? "R" : "L";
            string lotType = forRework ? OQCLotType.OQCLotType_ReDO : OQCLotType.OQCLotType_Normal;
            string productionType = "";
            decimal lotSize = 0;

            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            TryFacade tryFacade = new TryFacade(this.DataProvider);
            OQCLot newOQCLot = null;
            OQCLot objLot = null;
            DateTime currentDateTime = FormatHelper.ToDateTime(currentDBDateTime.DBDate, currentDBDateTime.DBTime);
            object[] tryListOfRCard = null;
            object[] tryListOfOldLot = null;

            if (forRework)
            {
                OQCLot oldLot = oqcFacade.GetOQCLot(oldLotNo, OQCFacade.Lot_Sequence_Default) as OQCLot;

                productionType = oldLot.ProductionType;
                if (productionType == ProductionType.ProductionType_Try)
                {
                    tryListOfRCard = tryFacade.GetTryListOfRCard(actionEventArgs.ProductInfo.NowSimulation.RunningCard, item.ItemCode);
                }

                //Lot Size
                lotSize = oldLot.LotCapacity;

                tryListOfOldLot = tryFacade.GetTryListOfLotNo(oldLot.LOTNO);
            }
            else
            {
                #region 先获取产品序列号对应的工单的productionType（来自于tblmo.momemo）
                // 先获取产品序列号对应的工单的productionType（来自于tblmo.momemo）
                MO mo = (new MOFacade(DataProvider)).GetMO(actionEventArgs.ProductInfo.NowSimulation.MOCode) as MO;
                SystemSettingFacade ssf = new SystemSettingFacade(this.DataProvider);
                Parameter param;
                param = ssf.GetParameter(mo.MOType, BenQGuru.eMES.Web.Helper.MOType.GroupType) as Parameter;
                if (param != null)
                {
                    if (string.Compare(param.ParameterValue, BenQGuru.eMES.Web.Helper.MOType.MOTYPE_RMAREWORKMOTYPE, true) == 0)
                    {
                        productionType = ProductionType.ProductionType_Claim;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(mo.MOMemo))
                        {
                            productionType = ProductionType.ProductionType_Mass;
                            mo.MOMemo = MOProductType.MO_Product_Type_Mass.ToUpper();                            
                        }

                        // 根据Memo获取ProductionType
                        param = ssf.GetParameter(mo.MOMemo, MOProductType.GroupType) as Parameter;
                        //changed by hiro 08/11/10 判断param不空进行比较
                        if (param != null)
                        {
                            if (string.Compare(param.ParameterValue, MOProductType.MO_Product_Type_New, true) == 0)
                            {
                                productionType = ProductionType.ProductionType_New;
                            }
                            else if (string.Compare(param.ParameterValue, MOProductType.MO_Product_Type_Mass, true) == 0)
                            {
                                productionType = ProductionType.ProductionType_Mass;
                            }
                            else
                            {
                                throw new Exception("$Error_MOMemoNotConfig $Domain_Parameter=" + mo.MOMemo);
                            }

                            tryListOfRCard = tryFacade.GetTryListOfRCard(actionEventArgs.ProductInfo.NowSimulation.RunningCard, item.ItemCode);
                            if (tryListOfRCard != null && tryListOfRCard.Length > 0)
                            {
                                productionType = ProductionType.ProductionType_Try;
                            }
                        }
                        else
                        {
                            throw new Exception("$CS_System_Params_Losted $Domain_ParameterGroup='" + MOProductType.GroupType + "' $Domain_Parameter=" + mo.MOMemo);
                        }

                    }
                }
                else
                {
                    throw new Exception("$CS_System_Params_Losted $Domain_ParameterGroup='" + BenQGuru.eMES.Web.Helper.MOType.GroupType + "' $Domain_Parameter=" + mo.MOType);
                }
                #endregion
            }

            #region 获取ShiftDay和ShiftCode
            BaseSetting.BaseModelFacade dataModel = new BaseSetting.BaseModelFacade(this.DataProvider);
            Domain.BaseSetting.Resource res = (Domain.BaseSetting.Resource)dataModel.GetResource(actionEventArgs.ResourceCode);
            int shiftDay = 0;
            string shiftCode = string.Empty;

            BaseSetting.ShiftModelFacade shiftModel = new BaseSetting.ShiftModelFacade(this.DataProvider);
            Domain.BaseSetting.TimePeriod period = (Domain.BaseSetting.TimePeriod)shiftModel.GetTimePeriod(res.ShiftTypeCode, currentDBDateTime.DBTime);
            if (period == null)
            {
                throw new Exception("$OutOfPerid");
            }

            shiftCode = period.ShiftCode;

            if (period.IsOverDate == Web.Helper.FormatHelper.TRUE_STRING)
            {
                if (period.TimePeriodBeginTime < period.TimePeriodEndTime)
                {
                    shiftDay = FormatHelper.TODateInt(currentDateTime.AddDays(-1));
                }
                else if (Web.Helper.FormatHelper.TOTimeInt(currentDateTime) < period.TimePeriodBeginTime)
                {
                    shiftDay = FormatHelper.TODateInt(currentDateTime.AddDays(-1));
                }
                else
                {
                    shiftDay = FormatHelper.TODateInt(currentDateTime);
                }
            }
            else
            {
                shiftDay = FormatHelper.TODateInt(currentDateTime);
            }
            #endregion

            bool needCreateNewLot = false;
            bool needAddLotTryCode = false;

            // 获取Lot的主逻辑
            if (productionType == ProductionType.ProductionType_Try)  //试流批
            {
                object[] lotList = oqcFacade.GetLotList4TryCase(item.ItemCode, res.StepSequenceCode, lotType, productionType, lotNoMarkCode);
                if (lotList == null || lotList.Length == 0) // 找不到对应的任何试流批，则新建试流批
                {
                    needCreateNewLot = true;
                    needAddLotTryCode = true;
                }
                else // 找到多个，则遍历每一个，找其中试流批与RCard完全一致的批，若没有则新建试流批
                {
                    object[] tryListOfLot;

                    foreach (OQCLot lotTemp in lotList)
                    {
                        tryListOfLot = tryFacade.GetTryListOfLotNo(lotTemp.LOTNO);
                        if (tryListOfRCard != null && tryListOfRCard.Length != 0 &&
                            tryListOfLot != null && tryListOfLot.Length != 0 &&
                            this.CompareTryCodeList(tryListOfRCard, tryListOfLot) == true)
                        {
                            newOQCLot = oqcFacade.GetOQCLot(lotTemp.LOTNO, lotTemp.LotSequence) as OQCLot;
                            break;
                        }
                    }

                    if (newOQCLot == null)
                    {
                        needCreateNewLot = true;
                        needAddLotTryCode = true;
                    }
                }
            }
            else //新品批或量产批或理赔批
            {
                objLot = oqcFacade.GetMaxLot4NormalCase(item.ItemCode, res.StepSequenceCode, lotType, productionType, lotNoMarkCode);

                if (objLot == null || objLot.LOTNO == null || objLot.LOTNO == string.Empty)
                {
                    needCreateNewLot = true;
                    needAddLotTryCode = false;
                }
                else
                {
                    newOQCLot = oqcFacade.GetOQCLot(objLot.LOTNO, OQCFacade.Lot_Sequence_Default) as OQCLot;
                }
            }

            // Added By Hi1/Venus.Feng on 20081128 for Hisense Version
            // 增加了ForRework的这个条件
            // 如果是Rework，都应该产生新的批，因为调用该函数之前，就已经by老的lot获取新的lot了，但是没有获取到
            // 才会跑到该函数中，所以在该函数中对于Rework的Case，都应该产生新的批
            // End Added
            if (needCreateNewLot || forRework)
            {
                newOQCLot = oqcFacade.CreateNewOQCLot();

                objLot = oqcFacade.GetMaxLot(res.StepSequenceCode, shiftDay, lotNoMarkCode);

                if (objLot == null || objLot.LOTNO == null || objLot.LOTNO == string.Empty)
                {
                    newOQCLot.LOTNO = res.StepSequenceCode + shiftDay.ToString() + lotNoMarkCode + "001";
                }
                else
                {
                    string oldLotNO = objLot.LOTNO;
                    string newSequence = Convert.ToString((int.Parse(oldLotNO.Substring(oldLotNO.Length - 3, 3)) + 1));

                    newOQCLot.LOTNO = oldLotNO.Substring(0, oldLotNO.Length - 3) + newSequence.PadLeft(3, '0');
                }

                // Create New OQC Lot
                if (forRework)
                {
                    newOQCLot = oqcFacade.CreateNewOQCLot(newOQCLot.LOTNO, actionEventArgs.UserCode, currentDBDateTime,
                                        res.OrganizationID, lotSize, res.StepSequenceCode,
                                        item.ItemCode, oldLotNo, lotType,
                                        productionType, res.ResourceCode, shiftDay, shiftCode);
                }
                else
                {
                    newOQCLot = oqcFacade.CreateNewOQCLot(newOQCLot.LOTNO, actionEventArgs.UserCode, currentDBDateTime,
                                    res.OrganizationID, item.LotSize, res.StepSequenceCode,
                                    item.ItemCode, oldLotNo, lotType,
                                    productionType, res.ResourceCode, shiftDay, shiftCode);

                }

                oqcFacade.AddOQCLot(newOQCLot);

                if (needAddLotTryCode || forRework)
                {
                    Try2Lot try2Lot = null;
                    if (forRework)
                    {
                        if (tryListOfOldLot != null && tryListOfOldLot.Length > 0)
                        {
                            foreach (Try tempTry in tryListOfOldLot)
                            {
                                try2Lot = tryFacade.CreateNewTry2Lot();
                                try2Lot.EAttribute1 = "";
                                try2Lot.LotNo = newOQCLot.LOTNO;
                                try2Lot.MaintainDate = currentDBDateTime.DBDate;
                                try2Lot.MaintainTime = currentDBDateTime.DBTime;
                                try2Lot.MaintainUser = actionEventArgs.UserCode;
                                try2Lot.TryCode = tempTry.TryCode;

                                tryFacade.AddTry2Lot(try2Lot);
                            }
                        }
                    }
                    else
                    {
                        foreach (Try tempTry in tryListOfRCard)
                        {
                            try2Lot = tryFacade.CreateNewTry2Lot();
                            try2Lot.EAttribute1 = "";
                            try2Lot.LotNo = newOQCLot.LOTNO;
                            try2Lot.MaintainDate = currentDBDateTime.DBDate;
                            try2Lot.MaintainTime = currentDBDateTime.DBTime;
                            try2Lot.MaintainUser = actionEventArgs.UserCode;
                            try2Lot.TryCode = tempTry.TryCode;

                            tryFacade.AddTry2Lot(try2Lot);
                        }
                    }
                }
            }

            return newOQCLot;
        }

        private bool CompareTryCodeList(object[] listOfRCard, object[] listOfLot)
        {
            string tryListOfRCard = "";
            string tryListOfLot = "";

            foreach (Try a in listOfRCard)
            {
                tryListOfRCard += a.TryCode.Trim() + ",";
            }

            foreach (Try b in listOfLot)
            {
                tryListOfLot += b.TryCode.Trim() + ",";
            }

            if (string.Compare(tryListOfLot, tryListOfRCard, true) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
