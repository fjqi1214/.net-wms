using System;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.LotDataCollect.Action;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.LotDataCollect;
using BenQGuru.eMES.Web.Helper;


using UserControl;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Domain.TSModel;

namespace BenQGuru.eMES.LotDataCollect
{
    /// <summary>
    /// DataCollectOnLine 的摘要说明。
    /// </summary>
    public class ActionOnLineHelper
    {
        private IDomainDataProvider _domainDataProvider = null;

        public ActionOnLineHelper(IDomainDataProvider domainDataProvider)
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


        public const int StartSeq = 1;
        public const string StringNull = "";
        public const int StartNGTimes = 0;

        //得到ID的Simulation 信息
        public Messages GetIDInfo(string iD)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "GetIDInfo");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                DataCollectFacade dataCollect = new DataCollectFacade(this.DataProvider);
                ProductInfo product = new ProductInfo();
                object obj = dataCollect.GetLotSimulation(iD.ToUpper());
                if (obj == null)
                {
                    product.LastSimulation = null;

                    //					//Karron Qiu ,2005-11-2,没有生产信息的时候,添加一个信息
                    //					messages.Add(new Message(MessageType.Error,"$NoSimulationInfo"));
                }
                else
                {
                    product.LastSimulation = new ExtendSimulation((LotSimulation)obj);

                    //Laws Lu,2005/12/15	产品停线盘点
                    if (Convert.ToInt32(product.LastSimulation.IsHold) == (int)Web.Helper.CycleStatus.Hold)
                    {
                        messages.Add(new Message(MessageType.Error, "$CS_LINE_IS_HOLD"));
                    }
                }
                product.NowSimulation = new LotSimulation();

                Message message = new Message(MessageType.Data, "", new object[] { product });
                messages.Add(message);

            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        public Messages GetIDInfoByMoCodeAndId(string moCode, string iD)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "GetIDInfo");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                DataCollectFacade dataCollect = new DataCollectFacade(this.DataProvider);
                ProductInfo product = new ProductInfo();
                object obj = dataCollect.GetLotSimulation(moCode, iD.ToUpper());
                if (obj == null)
                {
                    product.LastSimulation = null;

                    //					//Karron Qiu ,2005-11-2,没有生产信息的时候,添加一个信息
                    //					messages.Add(new Message(MessageType.Error,"$NoSimulationInfo"));
                }
                else
                {
                    product.LastSimulation = new ExtendSimulation((LotSimulation)obj);

                    //Laws Lu,2005/12/15	产品停线盘点
                    if (Convert.ToInt32(product.LastSimulation.IsHold) == (int)Web.Helper.CycleStatus.Hold)
                    {
                        messages.Add(new Message(MessageType.Error, "$CS_LINE_IS_HOLD"));
                    }
                }
                product.NowSimulation = new LotSimulation();

                Message message = new Message(MessageType.Data, "", new object[] { product });
                messages.Add(message);

            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        public ProductInfo GetIDInfoByLotSimulation(LotSimulation simulation)
        {
            ProductInfo product = new ProductInfo();
            product.LastSimulation = new ExtendSimulation(simulation);
            product.NowSimulation = new LotSimulation();
            //Laws Lu,2005/12/15	产品停线盘点
            if (Convert.ToInt32(product.LastSimulation.IsHold) == (int)Web.Helper.CycleStatus.Hold)
            {
                throw new Exception("$CS_LINE_IS_HOLD");
            }

            return product;

        }

        /// <summary>
        /// 比较两个SIMULATION在推途程上是否条件一致
        /// </summary>
        /// <param name="simulation1"></param>
        /// <param name="simulation2"></param>
        /// <returns></returns>
        public bool CompareSimulationCheck(LotSimulation simulation1, LotSimulation simulation2)
        {
            if (simulation1 == null)
                return false;
            if (simulation2 == null)
                return false;
            if ((simulation1.MOCode == simulation2.MOCode)
                && (simulation1.RouteCode == simulation2.RouteCode)
                && (simulation1.OPCode == simulation2.OPCode)
                && (simulation1.ResCode == simulation2.ResCode)
                && (simulation1.ProductStatus == simulation2.ProductStatus)
                && (simulation1.ActionList == simulation2.ActionList)
                && (simulation1.LastAction == simulation2.LastAction)
                && (simulation1.IsComplete == simulation2.IsComplete)
                )
                return true;
            return false;
        }
        public void CopyProduct(ProductInfo sourceProduct, ProductInfo toProduct)
        {
            ((ExtendSimulation)toProduct.LastSimulation).AdjustProductStatus = ((ExtendSimulation)sourceProduct.LastSimulation).AdjustProductStatus;
            ((ExtendSimulation)toProduct.LastSimulation).NextOPCode = ((ExtendSimulation)sourceProduct.LastSimulation).NextOPCode;
            ((ExtendSimulation)toProduct.LastSimulation).NextRouteCode = ((ExtendSimulation)sourceProduct.LastSimulation).NextRouteCode;
        }
        //Check Route 的基本信息  Check 正确会在NewSimulation 里填上新的途程 
        public Messages CheckID(ActionEventArgs actionEventArgs)
        {
            return CheckID(actionEventArgs, null);
        }
        // Added by Icyer 2005/10/28
        // 扩展一个带ActionCheckStatus的方法
        public Messages CheckID(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "CheckID");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                if (actionEventArgs.ActionType != ActionType.DataCollectAction_GoMO)
                {
                    if (actionEventArgs.ProductInfo.LastSimulation == null)
                    {
                        throw new Exception("$NoSimulationInfo");
                    }
                    else
                    {



                        DataCollectFacade dcFacade = new DataCollectFacade(this.DataProvider);

                        messages.AddMessages(dcFacade.CheckID(actionEventArgs.LotCode, actionEventArgs.ActionType, actionEventArgs.ResourceCode, actionEventArgs.UserCode, actionEventArgs.ProductInfo, actionCheckStatus));
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

        public Messages CheckIDOutline(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "CheckID");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                if (actionEventArgs.ActionType != ActionType.DataCollectAction_GoMO)
                {
                    if (actionEventArgs.ProductInfo.LastSimulation == null)
                    {
                        throw new Exception("$NoSimulationInfo");
                    }
                    else
                    {
                        DataCollectFacade dcFacade = new DataCollectFacade(this.DataProvider);
                        messages.AddMessages(dcFacade.CheckIDOutline(actionEventArgs.LotCode, actionEventArgs.ActionType, actionEventArgs.ResourceCode, ((OutLineActionEventArgs)actionEventArgs).OPCode, actionEventArgs.UserCode, actionEventArgs.ProductInfo));
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

        // 处理Simulation 的填写
        public Messages Execute(ActionEventArgs actionEventArgs)
        {
            return Execute(actionEventArgs, null);
        }

        // Added by Icyer 2005/10/31
        // 扩展一个带ActionCheckStatus参数的方法
        public Messages Execute(ActionEventArgs actionEventArgs, Action.ActionCheckStatus actionCheckStatus)
        {
            return Execute(actionEventArgs, actionCheckStatus, false, true);
        }


        public Messages Execute(ActionEventArgs actionEventArgs, Action.ActionCheckStatus actionCheckStatus, bool updateSimulation, bool addWip)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                DataCollectFacade dataCollect = new DataCollectFacade(this.DataProvider);
                //Laws Lu,2006/11/13 uniform system collect date
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
                actionEventArgs.ProductInfo.NowSimulation.EndDate = 0;
                actionEventArgs.ProductInfo.NowSimulation.EndTime = 0;

                if (actionEventArgs.ActionType == ActionType.DataCollectAction_NG)
                {
                    actionEventArgs.ProductInfo.NowSimulation.BeginDate = actionEventArgs.ProductInfo.LastSimulation.BeginDate;
                    actionEventArgs.ProductInfo.NowSimulation.BeginTime = actionEventArgs.ProductInfo.LastSimulation.BeginTime;
                }
                else
                {
                    actionEventArgs.ProductInfo.NowSimulation.BeginDate = dbDateTime.DBDate;
                    actionEventArgs.ProductInfo.NowSimulation.BeginTime = dbDateTime.DBTime;
                }
                actionEventArgs.ProductInfo.NowSimulation.MaintainUser = actionEventArgs.UserCode;
                actionEventArgs.ProductInfo.NowSimulationReport = this.FillLotSimulationReport(actionEventArgs.ProductInfo);

                // 如果actionCheckStatus不为空，则更新actionCheckStatus
                if (actionCheckStatus != null)
                {
                    actionCheckStatus.ProductInfo = actionEventArgs.ProductInfo;
                }

                //如果需要更新Simulation
                if (actionCheckStatus == null || actionCheckStatus.NeedUpdateSimulation == true || updateSimulation == true)
                {
                    //为了替换真确的资源
                    if (actionEventArgs.ActionType == ActionType.DataCollectAction_OQCNG
                        || actionEventArgs.ActionType == ActionType.DataCollectAction_OQCReject)
                    {
                        actionEventArgs.ProductInfo.NowSimulation.ResCode = actionEventArgs.ResourceCode;
                        actionEventArgs.ProductInfo.NowSimulationReport.ResCode = actionEventArgs.ResourceCode;
                    }

                    // LastSimulation !=null 的情况在 工单投入 处理
                    if (actionEventArgs.ProductInfo.LastSimulation != null)
                    {
                        if ((actionEventArgs.ProductInfo.LastSimulation.LotCode != actionEventArgs.ProductInfo.NowSimulation.LotCode)
                            || (actionEventArgs.ProductInfo.LastSimulation.MOCode != actionEventArgs.ProductInfo.NowSimulation.MOCode))
                        {

                            //Laws Lu,2006/03/20 报废后重新归属其他工单
                            if (actionEventArgs.ProductInfo.LastSimulation.MOCode != actionEventArgs.ProductInfo.NowSimulation.MOCode)
                            {
                                dataCollect.AddLotSimulation(actionEventArgs.ProductInfo.NowSimulation);
                                dataCollect.AddLotSimulationReport(actionEventArgs.ProductInfo.NowSimulationReport);
                            }
                            else
                            {
                                dataCollect.DeleteLotSimulation(actionEventArgs.ProductInfo.LastSimulation);
                                dataCollect.AddLotSimulation(actionEventArgs.ProductInfo.NowSimulation);

                                dataCollect.DeleteLotSimulationReport(actionEventArgs.ProductInfo.LastSimulation);
                                dataCollect.AddLotSimulationReport(actionEventArgs.ProductInfo.NowSimulationReport);
                            }

                        }
                        else
                        {
                            dataCollect.UpdateLotSimulation(actionEventArgs.ProductInfo.NowSimulation);
                            dataCollect.UpdateLotSimulationReport(actionEventArgs.ProductInfo.NowSimulationReport);
                        }
                    }
                    else
                    {
                        dataCollect.AddLotSimulation(actionEventArgs.ProductInfo.NowSimulation);
                        dataCollect.AddLotSimulationReport(actionEventArgs.ProductInfo.NowSimulationReport);
                    }
                }

                //如果需要更新Wip
                if (actionCheckStatus == null || actionCheckStatus.NeedUpdateSimulation == true)
                {
                    if (actionEventArgs.ActionType == ActionType.DataCollectAction_OQCNG)
                    {
                        dataCollect.AddLotOnWip(this.FillLotOnWip(actionEventArgs.ProductInfo, actionEventArgs));
                    }
                    else
                    {
                        dataCollect.AddLotOnWip(this.FillLotOnWip(actionEventArgs.ProductInfo));
                    }
                }
                else if (addWip == true)
                {
                    if (actionEventArgs.ActionType == ActionType.DataCollectAction_OQCNG)
                    {
                        dataCollect.AddLotOnWip(this.FillLotOnWip(actionEventArgs.ProductInfo, actionEventArgs));
                    }
                    else
                    {
                        actionEventArgs.OnWIP.Add(this.FillLotOnWip(actionEventArgs.ProductInfo));
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


        //填写SimulationReport ID,  途程,时间,基本模型,用户信息 依赖于 ProductInfo.NewSimulation
        public LotSimulationReport FillLotSimulationReport(ProductInfo productInfo)
        {
            LotSimulationReport simulationReport = new LotSimulationReport();
            simulationReport.RouteCode = productInfo.NowSimulation.RouteCode;
            simulationReport.OPCode = productInfo.NowSimulation.OPCode;
            simulationReport.CartonCode = productInfo.NowSimulation.CartonCode;
            simulationReport.EAttribute1 = productInfo.NowSimulation.EAttribute1;
            simulationReport.EAttribute2 = productInfo.NowSimulation.EAttribute2;
            simulationReport.IsComplete = productInfo.NowSimulation.IsComplete;
            simulationReport.ItemCode = productInfo.NowSimulation.ItemCode;
            simulationReport.LastAction = productInfo.NowSimulation.LastAction;
            simulationReport.LotNo = productInfo.NowSimulation.LotNo;
            simulationReport.BeginDate = productInfo.NowSimulation.BeginDate;
            simulationReport.BeginTime = productInfo.NowSimulation.BeginTime;
            simulationReport.MaintainUser = productInfo.NowSimulation.MaintainUser;
            simulationReport.MOCode = productInfo.NowSimulation.MOCode;
            simulationReport.ModelCode = productInfo.NowSimulation.ModelCode;
            simulationReport.NGTimes = productInfo.NowSimulation.NGTimes;
            simulationReport.PalletCode = productInfo.NowSimulation.PalletCode;
            simulationReport.ResCode = productInfo.NowSimulation.ResCode;
            simulationReport.LotCode = productInfo.NowSimulation.LotCode;
            simulationReport.LotSeq = productInfo.NowSimulation.LotSeq;
            simulationReport.Status = productInfo.NowSimulation.ProductStatus;
            simulationReport.LotStatus = productInfo.NowSimulation.LotStatus;
            simulationReport.RMABillCode = productInfo.NowSimulation.RMABillCode;
            simulationReport.CollectStatus = productInfo.NowSimulation.CollectStatus;
            simulationReport.LotQty = productInfo.NowSimulation.LotQty;
            simulationReport.GoodQty = productInfo.NowSimulation.GoodQty;
            simulationReport.NGQty = productInfo.NowSimulation.NGQty;

            //added by jessie, 2006-5-29, 添加车号
            simulationReport.ShelfNo = productInfo.NowSimulation.ShelfNo;
            //add by Laws,2006/05/31,Carton number
            simulationReport.CartonCode = productInfo.NowSimulation.CartonCode;

            // Changed by Icyer 2005/10/28
            /*
            BaseModelFacade dataModel=new BaseModelFacade(this.DataProvider);
            Resource resource				= (Resource)dataModel.GetResource(productInfo.NowSimulation.ResourceCode);
            */
            Resource resource = productInfo.Resource;
            if (resource == null)
            {
                BaseModelFacade dataModel = new BaseModelFacade(this.DataProvider);
                resource = (Resource)dataModel.GetResource(productInfo.NowSimulation.ResCode);
            }
            // Changed end

            simulationReport.SegmentCode = resource.SegmentCode;

            TimePeriod period = productInfo.TimePeriod;
            if (period == null)
            {
                ShiftModelFacade shiftModel = new ShiftModelFacade(this.DataProvider);
                period = (TimePeriod)shiftModel.GetTimePeriod(resource.ShiftTypeCode, simulationReport.BeginTime);
                if (period == null)
                {
                    throw new Exception("$OutOfPerid");
                }
                productInfo.TimePeriod = period;
            }

            // Modified by Jane Shu		Date:2005-07-26

            //Laws Lu,2006/11/13 uniform system collect date
            DBDateTime dbDateTime;
            if (productInfo.WorkDateTime == null)
            {
                dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                productInfo.WorkDateTime = dbDateTime;
            }
            else
            {
                dbDateTime = productInfo.WorkDateTime;
            }

            DateTime dtWorkDateTime = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);
            if (period.IsOverDate == FormatHelper.TRUE_STRING)
            {
                if (period.TimePeriodBeginTime < period.TimePeriodEndTime)
                {
                    simulationReport.BeginShiftDay = FormatHelper.TODateInt(dtWorkDateTime.AddDays(-1));//Laws Lu,2006/11/13 uniform system collect date
                }
                else if (productInfo.NowSimulation.BeginTime < period.TimePeriodBeginTime)
                {
                    simulationReport.BeginShiftDay = FormatHelper.TODateInt(dtWorkDateTime.AddDays(-1));//Laws Lu,2006/11/13 uniform system collect date
                }
                else
                {
                    simulationReport.BeginShiftDay = simulationReport.BeginDate;
                }
            }
            else
            {
                simulationReport.BeginShiftDay = FormatHelper.TODateInt(dtWorkDateTime);//Laws Lu,2006/11/13 uniform system collect date
            }
            simulationReport.ShiftTypeCode = resource.ShiftTypeCode;
            simulationReport.BeginShiftCode = period.ShiftCode;
            simulationReport.BeginTimePeriodCode = period.TimePeriodCode;
            simulationReport.StepSequenceCode = resource.StepSequenceCode;
            simulationReport.MOSeq = productInfo.NowSimulation.MOSeq;   // Added by Icyer 2007/07/03
            simulationReport.EndDate = 0;
            simulationReport.EndTime = 0;
            simulationReport.EndShiftCode = "";
            simulationReport.EndShiftDay = 0;
            return simulationReport;
        }

        //填写OnWip  途程,时间,基本模型,用户信息 依赖于 ProductInfo.NewSimulation
        public LotOnWip FillLotOnWip(ProductInfo productInfo)
        {
            return FillLotOnWip(productInfo, null);
        }

        public LotOnWip FillLotOnWip(ProductInfo productInfo, ActionEventArgs actionEventArgs)
        {
            LotOnWip onwip = new LotOnWip();
            onwip.Action = productInfo.NowSimulation.LastAction;
            onwip.ActionResult = productInfo.NowSimulation.ProductStatus;
            onwip.ItemCode = productInfo.NowSimulation.ItemCode;
            //Laws Lu,2006/11/13 MDate,MTime from DataBase
            DBDateTime dbDateTime = Web.Helper.FormatHelper.GetNowDBDateTime(DataProvider);
            onwip.BeginDate = dbDateTime.DBDate;
            onwip.BeginTime = dbDateTime.DBTime;
            onwip.MaintainUser = productInfo.NowSimulation.MaintainUser;
            onwip.MOCode = productInfo.NowSimulation.MOCode;
            onwip.ModelCode = productInfo.NowSimulation.ModelCode;
            onwip.NGTimes = productInfo.NowSimulation.NGTimes;
            onwip.OPCode = productInfo.NowSimulation.OPCode;
            onwip.LotQty = productInfo.NowSimulation.LotQty;
            onwip.GoodQty = productInfo.NowSimulation.GoodQty;
            onwip.NGQty = productInfo.NowSimulation.NGQty;
            if (actionEventArgs == null)
            {
                onwip.ResCode = productInfo.NowSimulation.ResCode;
            }
            else
            {
                onwip.ResCode = actionEventArgs.ResourceCode;
            }
            onwip.RouteCode = productInfo.NowSimulation.RouteCode;
            onwip.LotCode = productInfo.NowSimulation.LotCode;
            onwip.LotSeq = productInfo.NowSimulation.LotSeq;
            onwip.SegmentCode = productInfo.NowSimulationReport.SegmentCode;
            onwip.BeginShiftDay = productInfo.NowSimulationReport.BeginShiftDay;
            onwip.ShiftTypeCode = productInfo.NowSimulationReport.ShiftTypeCode;
            onwip.BeginShiftCode = productInfo.NowSimulationReport.BeginShiftCode;
            onwip.BeginTimePeriodCode = productInfo.NowSimulationReport.BeginTimePeriodCode;
            onwip.StepSequenceCode = productInfo.NowSimulationReport.StepSequenceCode;
            onwip.CollectStatus = productInfo.NowSimulation.CollectStatus;
            onwip.ShelfNo = productInfo.NowSimulation.ShelfNo;
            //Laws Lu,2006/07/05 support RMA
            onwip.RMABillCode = productInfo.NowSimulation.RMABillCode;
            onwip.Eattribute1 = productInfo.NowSimulation.EAttribute1;

            onwip.MoSeq = productInfo.NowSimulation.MOSeq;  // Added by Icyer 2007/07/02
            return onwip;
        }

        public Messages Action(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + ".Action." + actionEventArgs.ActionType);
            dataCollectDebug.WhenFunctionIn(messages);
            //IDomainDataProvider domainDataProvider = DomainDataProviderManager.DomainDataProvider();
            this.DataProvider.BeginTransaction();
            try
            {

                IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(actionEventArgs.ActionType);

                messages.AddMessages(dataCollectModule.Execute(actionEventArgs));
                if (messages.IsSuccess())
                    this.DataProvider.CommitTransaction();
                else
                    this.DataProvider.RollbackTransaction();
            }
            catch (Exception e)
            {
                this.DataProvider.RollbackTransaction();
                messages.Add(new Message(e));
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }

            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        public Messages ActionWithTransaction(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + ".Action." + actionEventArgs.ActionType);
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {

                IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(actionEventArgs.ActionType);

                messages.AddMessages(dataCollectModule.Execute(actionEventArgs));
            }
            catch (Exception e)
            {
                messages.Add(new Message(e));
            }
            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }

        // Added by Icyer 2005/10/28
        // 重载上面的函数，增加一个参数ActionCheckStatus
        public Messages Action(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + ".Action." + actionEventArgs.ActionType);
            dataCollectDebug.WhenFunctionIn(messages);
            this.DataProvider.BeginTransaction();
            try
            {

                IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(actionEventArgs.ActionType);

                // 如果是IActionWithStatus类型，则执行带ActionCheckStatus的方法
                if (dataCollectModule is IActionWithStatus)
                {
                    messages.AddMessages(((IActionWithStatus)dataCollectModule).Execute(actionEventArgs, actionCheckStatus));
                }
                else
                {
                    messages.AddMessages(dataCollectModule.Execute(actionEventArgs));
                }
                if (messages.IsSuccess())
                    this.DataProvider.CommitTransaction();
                else
                    this.DataProvider.RollbackTransaction();
            }
            catch (Exception e)
            {
                this.DataProvider.RollbackTransaction();
                messages.Add(new Message(e));
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            }

            dataCollectDebug.WhenFunctionOut(messages);
            return messages;
        }
        public Messages ActionWithTransaction(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + ".Action." + actionEventArgs.ActionType + "(WithCheck)");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(actionEventArgs.ActionType);

                // 如果是IActionWithStatus类型，则执行带ActionCheckStatus的方法
                if (dataCollectModule is IActionWithStatus)
                {
                    messages.AddMessages(((IActionWithStatus)dataCollectModule).Execute(actionEventArgs, actionCheckStatus));
                }
                else
                {
                    messages.AddMessages(dataCollectModule.Execute(actionEventArgs));
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

        //add by hiro 2008/07/23
        public Messages ActionWithTransaction(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus, object[] OPBOMDetail)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + ".Action." + actionEventArgs.ActionType + "(WithCheck)");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                IAction dataCollectModule = new ActionFactory(this.DataProvider).CreateAction(actionEventArgs.ActionType);

                // 如果是IActionWithStatus类型，则执行带ActionCheckStatus的方法
                if (dataCollectModule is IActionWithStatus)
                {
                    messages.AddMessages(((IActionWithStatusNew)dataCollectModule).Execute(actionEventArgs, actionCheckStatus, OPBOMDetail));
                }
                else
                {
                    messages.AddMessages(dataCollectModule.Execute(actionEventArgs));
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


        public Messages CollectErrorInformation(IDomainDataProvider domainDataProvider, string actionType, ProductInfo product, object[] datas1, object[] datas2, string memo)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug("CollectErrorInformation");
            dataCollectDebug.WhenFunctionIn(messages);
            TSFacade tsFacade = new TSFacade(domainDataProvider);
            BenQGuru.eMES.Domain.TS.TS tS = new BenQGuru.eMES.Domain.TS.TS();
            TSErrorCode tsErrorCode = new TSErrorCode();
            try
            {
                //Laws Lu,2005/08/17,修改
                tS.CardType = CardType.CardType_Product; ;
                //End　Laws Lu
                tS.FormTime = product.NowSimulation.BeginTime;
                tS.FromDate = product.NowSimulation.BeginDate;
                //Laws Lu,2005/08/17,修改
                //tS.FromInputType = "ONWIP";
                tS.FromInputType = TSFacade.TSSource_OnWIP;
                //End Laws Lu
                //Laws Lu,2005/08/23,修改 只是“发现时间”“发现人员”的信息没有填写。
                tS.FromUser = product.NowSimulation.MaintainUser;
                //End Laws Lu
                tS.FromMemo = memo;
                //Laws Lu,2006/07/05 add support RMA
                tS.RMABillCode = product.NowSimulation.RMABillCode;
                tS.FromOPCode = product.NowSimulation.OPCode;
                tS.FromResourceCode = product.NowSimulation.ResCode;
                tS.FromRouteCode = product.NowSimulation.RouteCode;
                tS.FromSegmentCode = product.NowSimulationReport.SegmentCode;
                tS.FromShiftCode = product.NowSimulationReport.BeginShiftCode;/*Laws Lu,2006/03/11 修正班次填写有误*/
                tS.FromShiftDay = product.NowSimulationReport.BeginShiftDay;
                tS.FromShiftTypeCode = product.NowSimulationReport.ShiftTypeCode;
                tS.FromStepSequenceCode = product.NowSimulationReport.StepSequenceCode;
                tS.FromTimePeriodCode = product.NowSimulationReport.BeginTimePeriodCode;
                tS.FromUser = product.NowSimulation.MaintainUser;
                tS.ItemCode = product.NowSimulation.ItemCode;
                tS.MaintainDate = product.NowSimulation.BeginDate;
                tS.MaintainTime = product.NowSimulation.BeginTime;
                tS.MaintainUser = product.NowSimulation.MaintainUser;
                tS.MOCode = product.NowSimulation.MOCode;
                tS.ModelCode = product.NowSimulation.ModelCode;
                tS.RunningCard = product.NowSimulation.LotCode;
                tS.RunningCardSequence = product.NowSimulation.LotSeq;
                tS.FromOutLineRouteCode = product.LastSimulation.RouteCode;
                tS.TransactionStatus = TSFacade.TransactionStatus_None;

                tS.TSId = FormatHelper.GetUniqueID(product.NowSimulation.MOCode
                    , tS.RunningCard, tS.RunningCardSequence.ToString());
                tS.TSStatus = TSStatus.TSStatus_New;
                //Laws Lu,2006/03/11,新增	保存送修的星期和月份
                //tS.Week = (new ReportHelper(DataProvider)).WeekOfYear(tS.FromShiftDay.ToString());
                tS.Month = int.Parse(tS.FromShiftDay.ToString().Substring(4, 2));

                //End Laws Lu
                tS.TSTimes = product.NowSimulation.NGTimes;
                tS.MOSeq = product.NowSimulation.MOSeq;     // Added by Icyer 2007/07/03
                tsFacade.AddTS(tS);

                if (datas1 != null)
                {
                    for (int i = 0; i < datas1.Length; i++)
                    {
                        tsErrorCode.ErrorCode = ((TSErrorCode)datas1[i]).ErrorCode;
                        tsErrorCode.ErrorCodeGroup = ((TSErrorCode)datas1[i]).ErrorCodeGroup;
                        tsErrorCode.ErrorQty = ((TSErrorCode)datas1[i]).ErrorQty;//add by kathy @20130830 不良数量
                        tsErrorCode.ItemCode = tS.ItemCode;
                        tsErrorCode.MaintainDate = tS.MaintainDate;
                        tsErrorCode.MaintainTime = tS.MaintainTime;
                        tsErrorCode.MaintainUser = tS.MaintainUser;
                        tsErrorCode.MOCode = tS.MOCode;
                        tsErrorCode.ModelCode = tS.ModelCode;
                        tsErrorCode.RunningCard = tS.RunningCard;
                        tsErrorCode.RunningCardSequence = tS.RunningCardSequence;
                        tsErrorCode.TSId = tS.TSId;
                        tsErrorCode.MOSeq = tS.MOSeq;   // Added by Icyer 2007/7/03
                        
                        tsFacade.AddTSErrorCode(tsErrorCode);
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

        //public Messages CollectErrorInfor(IDomainDataProvider domainDataProvider, string actionType, ProductInfo product, object[] errorinfo, string memo)
        //{
        //    Messages messages = new Messages();
        //    DataCollectDebug dataCollectDebug = new DataCollectDebug("CollectErrorInfor");
        //    dataCollectDebug.WhenFunctionIn(messages);
        //    TSFacade tsFacade = new TSFacade(domainDataProvider);
        //    BenQGuru.eMES.Domain.TS.TS tS = new BenQGuru.eMES.Domain.TS.TS();
        //    TSErrorCode tsErrorCode = new TSErrorCode();
        //    TSErrorCode2Location tsErrorCode2Location = new TSErrorCode2Location();
        //    try
        //    {
        //        //Laws Lu,2005/08/17,修改
        //        //tS.CardType = "成品";
        //        tS.CardType = CardType.CardType_Product; ;
        //        //End　Laws Lu
        //        tS.FormTime = product.NowSimulation.MaintainTime;
        //        tS.FromDate = product.NowSimulation.MaintainDate;
        //        //Laws Lu,2005/08/17,修改
        //        //tS.FromInputType = "ONWIP";
        //        tS.FromInputType = TSFacade.TSSource_OnWIP;
        //        //End Laws Lu
        //        //Laws Lu,2005/08/23,修改 只是“发现时间”“发现人员”的信息没有填写。
        //        tS.FromUser = product.NowSimulation.MaintainUser;
        //        //End Laws Lu
        //        tS.FromMemo = memo;
        //        //Laws Lu,2006/07/05 add support RMA
        //        tS.RMABillCode = product.NowSimulation.RMABillCode;
        //        tS.FromOPCode = product.NowSimulation.OPCode;
        //        tS.FromResourceCode = product.NowSimulation.ResourceCode;
        //        tS.FromRouteCode = product.NowSimulation.RouteCode;
        //        tS.FromSegmentCode = product.NowSimulationReport.SegmentCode;
        //        tS.FromShiftCode = product.NowSimulationReport.ShiftCode; ;/*Laws Lu,2006/03/11 修正班次填写有误*/
        //        tS.FromShiftDay = product.NowSimulationReport.ShiftDay;
        //        tS.FromShiftTypeCode = product.NowSimulationReport.ShiftTypeCode;
        //        tS.FromStepSequenceCode = product.NowSimulationReport.StepSequenceCode;
        //        tS.FromTimePeriodCode = product.NowSimulationReport.TimePeriodCode;
        //        tS.FromUser = product.NowSimulation.MaintainUser;
        //        tS.ItemCode = product.NowSimulation.ItemCode;
        //        tS.MaintainDate = product.NowSimulation.MaintainDate;
        //        tS.MaintainTime = product.NowSimulation.MaintainTime;
        //        tS.MaintainUser = product.NowSimulation.MaintainUser;
        //        tS.MOCode = product.NowSimulation.MOCode;
        //        tS.ModelCode = product.NowSimulation.ModelCode;
        //        tS.RunningCard = product.NowSimulation.RunningCard;
        //        tS.RunningCardSequence = product.NowSimulation.RunningCardSequence;
        //        tS.SourceCard = product.NowSimulation.SourceCard;
        //        tS.SourceCardSequence = product.NowSimulation.SourceCardSequence;
        //        //Laws Lu，2005/08/17，修改
        //        //tS.TransactionStatus = "NO";
        //        tS.TransactionStatus = TSFacade.TransactionStatus_None;
        //        //End Laws Lu
        //        tS.TranslateCard = product.NowSimulation.TranslateCard;
        //        tS.TranslateCardSequence = product.NowSimulation.TranslateCardSequence;
        //        //Laws Lu,2005/08/24,修改
        //        //tS.TSId= tS.RunningCard+tS.RunningCardSequence.ToString();
        //        tS.TSId = FormatHelper.GetUniqueID(product.NowSimulation.MOCode
        //            , tS.RunningCard, tS.RunningCardSequence.ToString());
        //        //End Laws Lu
        //        //Laws Lu,2005/08/17,新增	TS的状态
        //        tS.TSStatus = TSStatus.TSStatus_New;
        //        //Laws Lu,2006/03/11,新增	保存送修的星期和月份
        //        tS.Week = (new ReportHelper(DataProvider)).WeekOfYear(tS.FromShiftDay.ToString());
        //        tS.Month = int.Parse(tS.FromShiftDay.ToString().Substring(4, 2));

        //        //End Laws Lu
        //        tS.TSTimes = product.NowSimulation.NGTimes;
        //        //Laws Lu,2006/07/06 support RMA
        //        tS.RMABillCode = product.NowSimulation.RMABillCode;
        //        //modified by jessie lee ,when the status is new ,there is not tsuser
        //        //tS.TSUser = product.NowSimulation.MaintainUser;
        //        tS.MOSeq = product.NowSimulation.MOSeq;     // Added by Icyer 2007/07/03
        //        tsFacade.AddTS(tS);

        //        if (errorinfo != null)
        //        {
        //            for (int i = 0; i < errorinfo.Length; i++)
        //            {
        //                int j = tsFacade.QueryTSErrorCodeCount(((TSErrorCode2Location)errorinfo[i]).ErrorCodeGroup,
        //                    ((TSErrorCode2Location)errorinfo[i]).ErrorCode, tS.TSId);
        //                if (j == 0)
        //                {
        //                    tsErrorCode.ErrorCode = ((TSErrorCode2Location)errorinfo[i]).ErrorCode;
        //                    tsErrorCode.ErrorCodeGroup = ((TSErrorCode2Location)errorinfo[i]).ErrorCodeGroup;
        //                    tsErrorCode.ItemCode = tS.ItemCode;
        //                    tsErrorCode.MaintainDate = tS.MaintainDate;
        //                    tsErrorCode.MaintainTime = tS.MaintainTime;
        //                    tsErrorCode.MaintainUser = tS.MaintainUser;
        //                    tsErrorCode.MOCode = tS.MOCode;
        //                    tsErrorCode.ModelCode = tS.ModelCode;
        //                    tsErrorCode.RunningCard = tS.RunningCard;
        //                    tsErrorCode.RunningCardSequence = tS.RunningCardSequence;
        //                    tsErrorCode.TSId = tS.TSId;
        //                    tsErrorCode.MOSeq = tS.MOSeq;   // Added by Icyer 2007/07/03
        //                    tsFacade.AddTSErrorCode(tsErrorCode);
        //                }
        //                if (((TSErrorCode2Location)errorinfo[i]).ErrorLocation.Trim() != string.Empty)
        //                {
        //                    tsErrorCode2Location.AB = ((TSErrorCode2Location)errorinfo[i]).AB;
        //                    tsErrorCode2Location.ErrorLocation = ((TSErrorCode2Location)errorinfo[i]).ErrorLocation;
        //                    //Laws Lu,2005/09/09,修改	bug
        //                    tsErrorCode2Location.ErrorCode = ((TSErrorCode2Location)errorinfo[i]).ErrorCode;
        //                    tsErrorCode2Location.ErrorCodeGroup = ((TSErrorCode2Location)errorinfo[i]).ErrorCodeGroup;

        //                    tsErrorCode2Location.ItemCode = tsErrorCode.ItemCode;
        //                    tsErrorCode2Location.MaintainDate = tsErrorCode.MaintainDate;
        //                    tsErrorCode2Location.MaintainTime = tsErrorCode.MaintainTime;
        //                    tsErrorCode2Location.MaintainUser = tsErrorCode.MaintainUser;
        //                    tsErrorCode2Location.MEMO = "";
        //                    tsErrorCode2Location.MOCode = tsErrorCode.MOCode;
        //                    tsErrorCode2Location.ModelCode = tsErrorCode.ModelCode;
        //                    tsErrorCode2Location.RunningCard = tsErrorCode.RunningCard;
        //                    tsErrorCode2Location.RunningCardSequence = tsErrorCode.RunningCardSequence;
        //                    if (tsErrorCode2Location.ErrorLocation.IndexOf(".") < 0)
        //                        tsErrorCode2Location.SubErrorLocation = tsErrorCode2Location.ErrorLocation;
        //                    else
        //                        tsErrorCode2Location.SubErrorLocation = tsErrorCode2Location.ErrorLocation.Substring(
        //                            0, tsErrorCode2Location.ErrorLocation.IndexOf("."));
        //                    tsErrorCode2Location.TSId = tsErrorCode.TSId;

        //                    tsErrorCode2Location.ShiftDay = product.NowSimulationReport.ShiftDay;
        //                    tsErrorCode2Location.MOSeq = tsErrorCode.MOSeq;     // Added by Icyer 2007/07/03
        //                    tsFacade.AddTSErrorCode2Location(tsErrorCode2Location);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        messages.Add(new Message(e));
        //    }
        //    dataCollectDebug.WhenFunctionOut(messages);
        //    return messages;
        //}


        //public void CollectErrorInfor(LotSimulationReport sim, LotSimulationReport simReport, string LOTNO)
        //{
        //    TSFacade tsFacade = new TSFacade(_domainDataProvider);
        //    BenQGuru.eMES.Domain.TS.TS tS = new BenQGuru.eMES.Domain.TS.TS();
        //    TSErrorCode tsErrorCode = new TSErrorCode();
        //    //TSErrorCode2Location tsErrorCode2Location = new TSErrorCode2Location();

        //    tS.CardType = CardType.CardType_Product; ;
        //    tS.FormTime = sim.MaintainTime;
        //    tS.FromDate = sim.MaintainDate;
        //    tS.FromInputType = TSFacade.TSSource_OnWIP;
        //    tS.FromUser = sim.MaintainUser;
        //    //tS.FromMemo = memo;
        //    tS.FromOPCode = sim.OPCode;
        //    tS.FromResourceCode = sim.ResourceCode;
        //    tS.FromRouteCode = sim.RouteCode;
        //    tS.FromSegmentCode = simReport.SegmentCode;
        //    tS.FromShiftCode = simReport.ShiftCode; ;/*Laws Lu,2006/03/11 修正班次填写有误*/
        //    tS.FromShiftDay = simReport.ShiftDay;
        //    tS.FromShiftTypeCode = simReport.ShiftTypeCode;
        //    tS.FromStepSequenceCode = simReport.StepSequenceCode;
        //    tS.FromTimePeriodCode = simReport.TimePeriodCode;
        //    tS.FromUser = sim.MaintainUser;
        //    tS.ItemCode = sim.ItemCode;
        //    tS.MaintainDate = sim.MaintainDate;
        //    tS.MaintainTime = sim.MaintainTime;
        //    tS.MaintainUser = sim.MaintainUser;
        //    tS.MOCode = sim.MOCode;
        //    tS.ModelCode = sim.ModelCode;
        //    tS.RunningCard = sim.RunningCard;
        //    tS.RunningCardSequence = sim.RunningCardSequence;
        //    tS.SourceCard = sim.SourceCard;
        //    tS.SourceCardSequence = sim.SourceCardSequence;
        //    tS.TransactionStatus = TSFacade.TransactionStatus_None;
        //    tS.TranslateCard = sim.TranslateCard;
        //    tS.TranslateCardSequence = sim.TranslateCardSequence;
        //    tS.TSId = FormatHelper.GetUniqueID(sim.MOCode
        //        , tS.RunningCard, tS.RunningCardSequence.ToString());
        //    tS.TSStatus = TSStatus.TSStatus_New;
        //    tS.TSTimes = sim.NGTimes;
        //    //modified by jessie lee ,when the status is new ,there is not tsuser
        //    //tS.TSUser = sim.MaintainUser;
        //    tS.MOSeq = sim.MOSeq;       // Added by Icyer 2007/07/03
        //    tsFacade.AddTS(tS);

        //    object[] errorinfo = this.DataProvider.CustomQuery(typeof(Domain.OQC.OQCLotCard2ErrorCode), new SQLParamCondition(
        //        string.Format("select * from tbloqclotcard2errorcode where rcard=$RCARD and rcardseq=$RCARDSEQ and lotno=$LOTNO"),
        //        new SQLParameter[]
        //        {
        //            new SQLParameter("RCARD",typeof(string),sim.RunningCard),
        //            new SQLParameter("RCARDSEQ",typeof(string),sim.RunningCardSequence),
        //            new SQLParameter("LOTNO",typeof(string),LOTNO)
        //        }));
        //    if (errorinfo != null)
        //    {
        //        for (int i = 0; i < errorinfo.Length; i++)
        //        {
        //            int j = tsFacade.QueryTSErrorCodeCount(((Domain.OQC.OQCLotCard2ErrorCode)errorinfo[i]).ErrorCodeGroup,
        //                ((Domain.OQC.OQCLotCard2ErrorCode)errorinfo[i]).ErrorCode, tS.TSId);
        //            if (j == 0)
        //            {
        //                tsErrorCode.ErrorCode = ((Domain.OQC.OQCLotCard2ErrorCode)errorinfo[i]).ErrorCode;
        //                tsErrorCode.ErrorCodeGroup = ((Domain.OQC.OQCLotCard2ErrorCode)errorinfo[i]).ErrorCodeGroup;
        //                tsErrorCode.ItemCode = tS.ItemCode;
        //                tsErrorCode.MaintainDate = tS.MaintainDate;
        //                tsErrorCode.MaintainTime = tS.MaintainTime;
        //                tsErrorCode.MaintainUser = tS.MaintainUser;
        //                tsErrorCode.MOCode = tS.MOCode;
        //                tsErrorCode.ModelCode = tS.ModelCode;
        //                tsErrorCode.RunningCard = tS.RunningCard;
        //                tsErrorCode.RunningCardSequence = tS.RunningCardSequence;
        //                tsErrorCode.TSId = tS.TSId;
        //                tsErrorCode.MOSeq = tS.MOSeq;   // Added by Icyer 2007/07/03
        //                tsFacade.AddTSErrorCode(tsErrorCode);
        //            }

        //        }
        //    }
        //}


        /// <summary>
        /// added by jessie lee
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public bool CheckBelongToLot(string rcard)
        {
            string sql = string.Format(
@"select count(lotno)
  from tbllot
 where lotstatus not in ('{0}', '{1}')
   and lotno in (select lotno from tbllot2card where rcard = '{2}')", OQCLotStatus.OQCLotStatus_Pass, OQCLotStatus.OQCLotStatus_Reject, rcard.ToUpper());
            int count = this.DataProvider.GetCount(new SQLCondition(sql));

            if (count > 0) return true;
            return false;

        }
    }

    public class DataCollectDebug
    {
        //		private string FunctionName;
        //		private DateTime d;
        public DataCollectDebug(string functionName)
        {
            //FunctionName=functionName;
        }
        public void WhenFunctionIn(Messages message)
        {
            //d=DateTime.Now;
            //message.Add(new Message(MessageType.Debug,FunctionName+" IN"));

        }
        public void WhenFunctionOut(Messages message)
        {
            //message.Add(new Message(MessageType.Debug,FunctionName+" OUT"));
            //TimeSpan t= DateTime.Now.Subtract(d);
            //message.Add(new Message(MessageType.Performance,FunctionName+":"+t.TotalMilliseconds.ToString(),new object[]{t.TotalMilliseconds})); 
        }
        public void DebugPoint(Messages message, string point)
        {
            //message.Add(new Message(MessageType.Debug,FunctionName+" "+point));
        }
    }
}
