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
using System.Collections;
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.DataCollect.Action
{


    /// <summary>
    /// 归属工单采集
    /// </summary>
    public class ActionDropMaterial : IActionWithStatus
    {
        private IDomainDataProvider _domainDataProvider = null;

        //		public ActionGood()
        //		{	
        //		}

        public ActionDropMaterial(IDomainDataProvider domainDataProvider)
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
        /// 根据OnWIPItem的上料记录，反推上料的BOM，然后建立对应的下料BOM
        /// </summary>
        private Messages BuildOPBomKeyPartsByOnWIPItem(string rcard, ProductInfo product, OnWIPItem[] wipItems)
        {
            Messages msg = new Messages();

            MO mo;
            MOFacade moFacade = new MOFacade(DataProvider);

            if (product != null && product.CurrentMO != null)
            {
                mo = product.CurrentMO;
            }
            else
            {
                mo = (MO)moFacade.GetMO(product.LastSimulation.MOCode);
            }

            OPBOMFacade opBOMFacade = new OPBOMFacade(DataProvider);
            object[] objOpBoms = opBOMFacade.GetOPBOMDetails(
                product.LastSimulation.MOCode
                , wipItems[0].RouteCode
                , wipItems[0].OPCode);
            for (int i = 0; objOpBoms != null && i < objOpBoms.Length; i++)
            {
                ((OPBOMDetail)objOpBoms[i]).ActionType = (int)MaterialType.DropMaterial;
            }
            opBomKeyparts = new OPBomKeyparts(objOpBoms, Convert.ToInt32(mo.IDMergeRule), this.DataProvider);

            if (opBomKeyparts.Count == 0)
            {
                msg.Add(new UserControl.Message("$CS_NOOPBomInfo $CS_Param_MOCode =" + product.LastSimulation.MOCode
                    + " $CS_Param_RouteCode =" + wipItems[0].RouteCode
                    + " $CS_Param_OPCode =" + wipItems[0].OPCode));
            }

            return msg;
        }
        /// <summary>
        /// 缓存的批号
        /// </summary>
        private ArrayList Innos = new ArrayList();

        private void GetKeyParts()
        {
            int n = opBomKeyparts.GetbomKeypartCount();
            for (int i = 0; i < n; i++)
            {
                Domain.Material.MKeyPart keypart = opBomKeyparts.GetKeypart(i);
                InnoObject io = new InnoObject();

                io.MCard = keypart.Keypart;
                io.MCardType = MCardType.MCardType_Keyparts;
                io.ItemIndex = i;
                io.MItemCode = keypart.MItemCode;
                io.Qty = 1;

                Innos.Add(io);
            }
        }

        private string _usercode;
        private string _rescode;

        private Messages CollectInfo(ActionOnLineHelper onLine, ProductInfo Product)
        {
            Messages msg = new Messages();

            GetKeyParts();

            DropMaterialEventArgs Args = new DropMaterialEventArgs(ActionType.DataCollectAction_DropMaterial, Product.LastSimulation.RunningCard,
                 _usercode, _rescode,
                 Product);
            Args.OnwipItems = Innos.ToArray();

            msg.AddMessages(onLine.ActionWithTransaction(Args));


            return msg;
        }

        private OnWIPItem[] LastRCardOnWIPItem = null;

        private Messages CollectKeyPartTS(ProductInfo Product)
        {
            Messages msg = new Messages();
            // 根据联美兰达的需求，不良品如果到TS，则自动将KeyPart采到TS
            BenQGuru.eMES.TS.TSFacade tsFacade = new BenQGuru.eMES.TS.TSFacade(this.DataProvider);
            BenQGuru.eMES.Domain.TS.TS ts = (BenQGuru.eMES.Domain.TS.TS)tsFacade.GetCardLastTSRecord(Product.LastSimulation.RunningCard);
            if (ts != null && (ts.TSStatus == TSStatus.TSStatus_New || ts.TSStatus == TSStatus.TSStatus_Confirm || ts.TSStatus == TSStatus.TSStatus_TS))
            { }
            else
            {
                return msg;
            }
            if (this.LastRCardOnWIPItem == null || this.LastRCardOnWIPItem.Length == 0)
                return msg;

            Domain.TS.TSErrorCode2Location[] errorInfo = null;
            MaterialFacade mfacade = new MaterialFacade(this.DataProvider);


            ActionDropMaterial actionDrop = new ActionDropMaterial(this.DataProvider);
            for (int i = 0; i < this.LastRCardOnWIPItem.Length; i++)
            {
                msg.AddMessages(actionDrop.CollectKeyPartNG(this.LastRCardOnWIPItem[i].MCARD, errorInfo, _usercode, _rescode));
                if (msg.IsSuccess() == true)
                    msg.Add(new UserControl.Message(MessageType.Success, "$KeyPart_NG_Collect_Success"));
                msg.Add(new UserControl.Message(MessageType.Normal, "$PageControl_Keyparts: " + this.LastRCardOnWIPItem[i].MCARD));
            }
            return msg;
        }

        /// <summary>
        /// 业务处理
        /// </summary>
        /// <returns></returns>
        private Messages DoAction(ProductInfo Product)
        {
            Messages msg = new Messages();
            ActionOnLineHelper onLine = new ActionOnLineHelper(DataProvider);
            try
            {
                msg.AddMessages(CollectInfo(onLine, Product));

                if (msg.IsSuccess())
                {
                    Messages msgTmp = CollectKeyPartTS(Product);
                    if (msgTmp.IsSuccess() == true)
                        msg.AddMessages(msgTmp);
                    else
                        msg = msgTmp;
                }

                if (msg.IsSuccess())
                {
                    DataProvider.CommitTransaction();
                }
                else
                {
                    DataProvider.RollbackTransaction();
                }
            }
            catch (Exception e)
            {
                DataProvider.RollbackTransaction();
                msg.Add(new UserControl.Message(e));
            }


            return msg;
        }

        private OPBomKeyparts opBomKeyparts;
        public Messages DropRCard(ProductInfo Product, string usercode, string rescode)
        {
            Messages msg = new Messages();

            this._rescode = rescode;
            this._usercode = usercode;

            if (Product != null && Product.LastSimulation != null)
            {
                if (Product.LastSimulation.ProductStatus != ProductStatus.NG)
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$DropMaterial_Need_NG"));
                    return msg;
                }

                MaterialFacade mfacade = new MaterialFacade(this.DataProvider);
                OnWIPItem[] items = mfacade.QueryLoadedKeyPartByRCard(Product.LastSimulation.RunningCard, Product.LastSimulation.MOCode);
                if (items == null || items.Length == 0)
                {
                    //msg.Add(new UserControl.Message(MessageType.Error, "$DropMaterial_No_KeyPart"));
                    return msg;
                }
                this.LastRCardOnWIPItem = items;

                msg.AddMessages(BuildOPBomKeyPartsByOnWIPItem(Product.LastSimulation.RunningCard, Product, items));

                if (msg.IsSuccess())
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        string keyPart = items[i].MCARD;
                        //增加KeyParts
                        msg.AddMessages(AddKeyPart(keyPart, Product.LastSimulation.MOCode, Product.LastSimulation.RunningCard));
                        if (msg.IsSuccess() == false)
                        {
                            return msg;
                        }
                    }
                    msg.AddMessages(DoAction(Product));

                    if (!msg.IsSuccess())
                    {
                        return msg;
                    }
                    else
                    {
                        return msg;
                    }
                }
                else
                {
                    msg.Add(new UserControl.Message(MessageType.Error, "$NoSimulation"));
                }

                return msg;
            }

            return msg;
        }

        //添加KeyParts
        private Messages AddKeyPart(string keyPart, string moCode, string runningCard)
        {
            Messages msg = new Messages();

            try
            {
                msg.AddMessages(opBomKeyparts.AddKeyparts(keyPart, moCode, runningCard));
            }
            catch (Exception e)
            {
                msg.Add(new UserControl.Message(e));
            }

            return msg;
        }

        public Messages Execute(ActionEventArgs actionEventArgs)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                // 联美兰达的下料不检查途程
                //messages.AddMessages( dataCollect.CheckID(actionEventArgs));
                if (messages.IsSuccess())
                {
                    // 联美兰达下料不更新Simulation
                    //messages.AddMessages( dataCollect.Execute(actionEventArgs));
                    if (messages.IsSuccess())
                    {
                        /*	由于界面上会自动调用GOOD采集，因此不用写报表
                        //填写测试报表
                        ReportHelper reportCollect= new ReportHelper(this.DataProvider);
                        messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo));
                        messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider,actionEventArgs.ActionType,actionEventArgs.ProductInfo));
                        */

                        //Laws Lu,2005/10/20,新增	使用配置文件来控制物料模块是否使用
                        if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                        {
                            BenQGuru.eMES.Material.CastDownHelper castHelper = new BenQGuru.eMES.Material.CastDownHelper(DataProvider);
                            ArrayList arRcard = new ArrayList();


                            castHelper.GetAllRCardByMo(ref arRcard, actionEventArgs.ProductInfo.NowSimulation.RunningCard, actionEventArgs.ProductInfo.NowSimulation.MOCode);
                            if (arRcard.Count == 0)
                            {
                                arRcard.Add(actionEventArgs.RunningCard);
                            }
                            string runningCards = "('" + String.Join("','", (string[])arRcard.ToArray(typeof(string))) + "')";

                            object[] objs = (actionEventArgs as DropMaterialEventArgs).OnwipItems;
                            //下料并归还库房
                            BenQGuru.eMES.Material.WarehouseFacade wfacade = new BenQGuru.eMES.Material.WarehouseFacade(this.DataProvider);
                            wfacade.RemoveWarehouse(
                                objs
                                , runningCards
                                , actionEventArgs.ProductInfo.NowSimulation.MOCode
                                , actionEventArgs.UserCode
                                , actionEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode
                                , actionEventArgs.ProductInfo.NowSimulation.OPCode);
                        }
                        else	// Added by Icyer 2006/11/16
                        {
                            DropMaterialEventArgs arg = (DropMaterialEventArgs)actionEventArgs;
                            if (arg.OnwipItems != null)
                            {
                                // 查询需要管控的KeyPart
                                ArrayList listKeyPart = new ArrayList();
                                ArrayList listAllKeyPart = new ArrayList();

                                //ArrayList partsToUnload = new ArrayList();
                                //ArrayList partsToReplace = new ArrayList();

                                //for (int i = 0; i < arg.OnwipItems.Length; i++)
                                //{
                                //    BenQGuru.eMES.Material.InnoObject io = (BenQGuru.eMES.Material.InnoObject)arg.OnwipItems[i];

                                //    if (io.NewBarcode.Trim().Length > 0)
                                //        partsToReplace.Add(io);
                                //    else
                                //        partsToUnload.Add(io);
                                //}

                                string sql = string.Empty;
                                CastDownHelper castDownHelper = new CastDownHelper(DataProvider);
                                BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
                                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                                ShiftModelFacade shiftModelFacade = new ShiftModelFacade(this.DataProvider);

                                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                                foreach (InnoObject io in arg.OnwipItems)
                                {
                                    //获取arRcard                                    
                                    ArrayList arRcard = new ArrayList();
                                    castDownHelper.GetAllRCard(ref arRcard, actionEventArgs.ProductInfo.NowSimulation.RunningCard);
                                    if (arRcard.Count == 0)
                                    {
                                        arRcard.Add(actionEventArgs.RunningCard);
                                    }

                                    //获取dropop
                                    Operation2Resource objOP = baseModelFacade.GetOperationByResource(arg.ResourceCode);
                                    string dropop = string.Empty;
                                    if (objOP != null)
                                    {
                                        dropop = objOP.OPCode;
                                    }

                                    #region 针对旧料所作的动作

                                    string runningCards = "('" + String.Join("','", (string[])arRcard.ToArray(typeof(string))) + "')";
                                    sql = string.Format("update TBLONWIPITEM set TRANSSTATUS='{0}',ActionType=" + (int)MaterialType.DropMaterial +
                                        ",DropOP = '" + dropop + "'" +
                                        ",DropUser='" + arg.UserCode + "'" +
                                        ",DropDate=" + dbDateTime.DBDate +
                                        ",DropTime=" + dbDateTime.DBTime +
                                        " where RCARD in {1} and ActionType='{2}'" +
                                        " and MCARD in ('" + io.MCard.Trim().ToUpper() + "')"
                                        , TransactionStatus.TransactionStatus_YES
                                        , runningCards
                                        , (int)Web.Helper.MaterialType.CollectMaterial);
                                    this.DataProvider.CustomExecute(new SQLCondition(sql));

                                    if (io.MCardType == MCardType.MCardType_Keyparts)
                                    {
                                        sql = "update TBLSIMULATIONREPORT set IsLoadedPart='0',LoadedRCard='' " +
                                            " where RCARD in ('" + io.MCard.Trim().ToUpper() + "')";
                                        this.DataProvider.CustomExecute(new SQLCondition(sql));

                                        sql = "update TBLITEMLOTDETAIL set SERIALSTATUS='STORAGE' " +
                                            " where SERIALNO in ('" + io.MCard.Trim().ToUpper() + "')";
                                        this.DataProvider.CustomExecute(new SQLCondition(sql));
                                    }

                                    #endregion

                                    #region 针对新料所做的动作

                                    if (io.NewBarcode.Trim().Length > 0)
                                    {
                                        object[] oldOnWIPItemList = dataCollectFacade.QueryOnWIPItem(io.MCard, runningCards, ((int)Web.Helper.MaterialType.DropMaterial).ToString());

                                        if (oldOnWIPItemList != null && oldOnWIPItemList.Length > 0)
                                        {
                                            OnWIPItem oldOnWIPItem = (OnWIPItem)oldOnWIPItemList[0];
                                            OnWIPItem newOnWIPItem = dataCollectFacade.CreateNewOnWIPItem();

                                            newOnWIPItem.RunningCard = oldOnWIPItem.RunningCard;
                                            newOnWIPItem.MSequence = oldOnWIPItem.MSequence;
                                            newOnWIPItem.MOCode = oldOnWIPItem.MOCode;
                                            newOnWIPItem.ModelCode = oldOnWIPItem.ModelCode;
                                            newOnWIPItem.ItemCode = oldOnWIPItem.ItemCode;
                                            newOnWIPItem.MItemCode = oldOnWIPItem.MItemCode;
                                            newOnWIPItem.MCardType = oldOnWIPItem.MCardType;
                                            newOnWIPItem.Qty = oldOnWIPItem.Qty;
                                            newOnWIPItem.RouteCode = oldOnWIPItem.RouteCode;
                                            newOnWIPItem.OPCode = oldOnWIPItem.OPCode;
                                            newOnWIPItem.ResourceCode = oldOnWIPItem.ResourceCode;
                                            newOnWIPItem.SegmentCode = oldOnWIPItem.SegmentCode;
                                            newOnWIPItem.StepSequenceCode = oldOnWIPItem.StepSequenceCode;
                                            newOnWIPItem.EAttribute1 = oldOnWIPItem.EAttribute1;
                                            newOnWIPItem.MOSeq = oldOnWIPItem.MOSeq;

                                            newOnWIPItem.MCARD = io.NewBarcode;
                                            newOnWIPItem.LotNO = io.NewLotNo;
                                            newOnWIPItem.PCBA = io.NewPCBA;
                                            newOnWIPItem.BIOS = io.NewBIOS;
                                            newOnWIPItem.Version = io.NewVersion;
                                            newOnWIPItem.VendorItemCode = io.NewVendorItemCode;
                                            newOnWIPItem.VendorCode = io.NewVendorCode;
                                            newOnWIPItem.DateCode = io.NewDateCode;

                                            newOnWIPItem.TransactionStatus = TransactionStatus.TransactionStatus_NO;
                                            newOnWIPItem.ActionType = (int)Web.Helper.MaterialType.CollectMaterial;
                                            newOnWIPItem.MaintainUser = actionEventArgs.UserCode;   
                                            newOnWIPItem.MaintainDate = dbDateTime.DBDate;
                                            newOnWIPItem.MaintainTime = dbDateTime.DBTime;

                                            //RunningCardSequence取当前RCard的最小再减一
                                            newOnWIPItem.RunningCardSequence = dataCollectFacade.GetMinRCardSequenceFromOnWipItem(runningCards);

                                            //重新抓取ShiftTypeCode、ShiftCode、TimePeriodCode
                                            StepSequence stepSequence = (StepSequence)baseModelFacade.GetStepSequence(newOnWIPItem.StepSequenceCode);
                                            if (stepSequence == null)
                                            {
                                                newOnWIPItem.ShiftTypeCode = stepSequence.ShiftTypeCode;
                                            }
                                            else
                                            {
                                                newOnWIPItem.ShiftTypeCode = oldOnWIPItem.ShiftTypeCode;
                                            }

                                            TimePeriod timePeriod = (TimePeriod)shiftModelFacade.GetTimePeriod(newOnWIPItem.ShiftTypeCode, dbDateTime.DBTime);
                                            if (timePeriod != null)
                                            {
                                                newOnWIPItem.TimePeriodCode = timePeriod.TimePeriodCode;
                                                newOnWIPItem.ShiftCode = timePeriod.ShiftCode;
                                            }

                                            dataCollectFacade.AddOnWIPItem(newOnWIPItem);
                                        }

                                        if (io.MCardType == MCardType.MCardType_Keyparts)
                                        {
                                            sql = "update TBLSIMULATIONREPORT set IsLoadedPart='1',LoadedRCard='" + String.Join(",", (string[])arRcard.ToArray(typeof(string))) + "' " +
                                               " where RCARD in ('" + io.NewBarcode.Trim().ToUpper() + "')";
                                            this.DataProvider.CustomExecute(new SQLCondition(sql));

                                            sql = "update TBLITEMLOTDETAIL set SERIALSTATUS='UNSTORAGE' " +
                                            " where SERIALNO in ('" + io.NewBarcode.Trim().ToUpper() + "')";
                                            this.DataProvider.CustomExecute(new SQLCondition(sql));
                                        }

                                        // Added By Hi1/Venus.feng on 20081114 for Hisense Version : Add Remove Pallet and Carton
                                        // 拆箱和拆Pallet
                                        DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
  
                                        if (messages.IsSuccess())
                                        {
                                            messages.AddMessages(dcf.RemoveFromPallet(io.NewBarcode.Trim().ToUpper(), actionEventArgs.UserCode,true));
                                            if (messages.IsSuccess())
                                            {
                                                messages.ClearMessages();
                                            }
                                        }

                                        if (messages.IsSuccess())
                                        {
                                            messages.AddMessages(dcf.RemoveFromCarton(io.NewBarcode.Trim().ToUpper(), actionEventArgs.UserCode));
                                            if (messages.IsSuccess())
                                            {
                                                messages.ClearMessages();
                                            }
                                        }
                                        // End Added
                                    }

                                    #endregion
                                }
                            }
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

        //Laws Lu,2006/01/04，此方法已经放弃使用
        public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus)
        {
            Messages messages = new Messages();
            DataCollectDebug dataCollectDebug = new DataCollectDebug(this.GetType().ToString() + "Collect");
            dataCollectDebug.WhenFunctionIn(messages);
            try
            {
                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(actionEventArgs));
                if (messages.IsSuccess())
                {
                    messages.AddMessages(dataCollect.Execute(actionEventArgs));
                    if (messages.IsSuccess())
                    {
                        //填写测试报表
                        //ReportHelper reportCollect = new ReportHelper(this.DataProvider);
                        //messages.AddMessages(reportCollect.ReportLineQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo));
                        //messages.AddMessages(reportCollect.ReportResQuanMaster(this.DataProvider, actionEventArgs.ActionType, actionEventArgs.ProductInfo));

                        //Laws Lu,2005/10/20,新增	使用配置文件来控制物料模块是否使用
                        if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                        {
                            BenQGuru.eMES.Material.CastDownHelper castHelper = new BenQGuru.eMES.Material.CastDownHelper(DataProvider);
                            ArrayList arRcard = new ArrayList();


                            castHelper.GetAllRCardByMo(ref arRcard, actionEventArgs.ProductInfo.NowSimulation.RunningCard, actionEventArgs.ProductInfo.NowSimulation.MOCode);
                            if (arRcard.Count == 0)
                            {
                                arRcard.Add(actionEventArgs.RunningCard);
                            }
                            string runningCards = "('" + String.Join("','", (string[])arRcard.ToArray(typeof(string))) + "')";

                            object[] objs = (actionEventArgs as DropMaterialEventArgs).OnwipItems;
                            //下料并归还库房
                            BenQGuru.eMES.Material.WarehouseFacade wfacade = new BenQGuru.eMES.Material.WarehouseFacade(this.DataProvider);
                            wfacade.RemoveWarehouse(
                                objs
                                , runningCards
                                , actionEventArgs.ProductInfo.NowSimulation.MOCode
                                , actionEventArgs.UserCode
                                , actionEventArgs.ProductInfo.NowSimulationReport.StepSequenceCode
                                , actionEventArgs.ProductInfo.NowSimulation.OPCode);
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

        /// <summary>
        /// 采集KeyPart不良
        /// </summary>
        public Messages CollectKeyPartNG(string keyPartNo, TSErrorCode2Location[] errorInfo, string userCode, string resourceCode)
        {
            Messages msg = new Messages();

            string strSql = "SELECT * FROM tblOnWIPItem WHERE MCard='" + keyPartNo + "' ORDER BY MDate DESC,MTime DESC ";
            object[] objs = this._domainDataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.OnWIPItem), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
            {
                msg.Add(new Message(MessageType.Error, "$KeyPart_NG_ErrorKeyPart"));
                return msg;
            }
            BenQGuru.eMES.Domain.DataCollect.OnWIPItem item = (BenQGuru.eMES.Domain.DataCollect.OnWIPItem)objs[0];

            TS.TSFacade tsFacade = new BenQGuru.eMES.TS.TSFacade(this.DataProvider);
            BenQGuru.eMES.Domain.TS.TS itemTs = new BenQGuru.eMES.Domain.TS.TS();

            object objTs = tsFacade.GetCardLastTSRecord(keyPartNo);
            if (objTs != null)
            {
                Domain.TS.TS ts = (Domain.TS.TS)objTs;
                if (ts.TSStatus == TSStatus.TSStatus_New ||
                    ts.TSStatus == TSStatus.TSStatus_Confirm ||
                    ts.TSStatus == TSStatus.TSStatus_TS)
                {
                    msg.Add(new Message(MessageType.Error, "$KeyPart_NG_In_TS"));
                    return msg;
                }
            }

            #region ItemTS
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            itemTs.MOCode = item.MOCode;
            itemTs.RunningCard = keyPartNo;
            itemTs.RunningCardSequence = tsFacade.GetUniqueTSRunningCardSequence(keyPartNo);
            itemTs.TSId = FormatHelper.GetUniqueID(item.MOCode, itemTs.RunningCard, itemTs.RunningCardSequence.ToString());
            itemTs.TranslateCard = item.RunningCard;
            itemTs.TranslateCardSequence = item.RunningCardSequence;
            itemTs.CardType = CardType.CardType_Part;
            itemTs.TSStatus = TSStatus.TSStatus_New;
            itemTs.MaintainUser = userCode;
            itemTs.MaintainDate = dbDateTime.DBDate;
            itemTs.MaintainTime = dbDateTime.DBTime;
            itemTs.TSDate = itemTs.ConfirmDate;
            itemTs.TSTime = itemTs.ConfirmTime;
            itemTs.FromInputType = BenQGuru.eMES.TS.TSFacade.TSSource_TS;
            itemTs.FromUser = userCode;
            itemTs.FromDate = itemTs.MaintainDate;
            itemTs.FormTime = itemTs.MaintainTime;
            itemTs.FromOPCode = "TS";
            itemTs.FromResourceCode = resourceCode;
            itemTs.FromRouteCode = item.RouteCode;
            itemTs.FromSegmentCode = item.SegmentCode;
            itemTs.FromShiftCode = item.ShiftCode;
            itemTs.FromShiftDay = item.MaintainDate;
            itemTs.FromShiftTypeCode = item.ShiftTypeCode;
            itemTs.FromStepSequenceCode = item.StepSequenceCode;
            itemTs.FromTimePeriodCode = item.TimePeriodCode;
            itemTs.ItemCode = item.MItemCode;
            itemTs.ModelCode = item.ModelCode;
            itemTs.TransactionStatus = TransactionStatus.TransactionStatus_NO;
            itemTs.TSDate = 0;
            itemTs.TSTime = 0;
            itemTs.MOSeq = item.MOSeq;
            itemTs.TSTimes = 1;
            tsFacade.AddTS(itemTs);
            #endregion

            #region Error Info
            for (int i = 0; errorInfo != null && i < errorInfo.Length; i++)
            {
                TSErrorCode tsErrorCode = new TSErrorCode();
                TSErrorCode2Location tsErrorCode2Location = new TSErrorCode2Location();
                int j = tsFacade.QueryTSErrorCodeCount(((TSErrorCode2Location)errorInfo[i]).ErrorCodeGroup,
                    ((TSErrorCode2Location)errorInfo[i]).ErrorCode, itemTs.TSId);
                if (j == 0)
                {
                    tsErrorCode.ErrorCode = ((TSErrorCode2Location)errorInfo[i]).ErrorCode;
                    tsErrorCode.ErrorCodeGroup = ((TSErrorCode2Location)errorInfo[i]).ErrorCodeGroup;
                    tsErrorCode.ItemCode = itemTs.ItemCode;
                    tsErrorCode.MaintainDate = itemTs.MaintainDate;
                    tsErrorCode.MaintainTime = itemTs.MaintainTime;
                    tsErrorCode.MaintainUser = itemTs.MaintainUser;
                    tsErrorCode.MOCode = itemTs.MOCode;
                    tsErrorCode.ModelCode = itemTs.ModelCode;
                    tsErrorCode.RunningCard = itemTs.RunningCard;
                    tsErrorCode.RunningCardSequence = itemTs.RunningCardSequence;
                    tsErrorCode.TSId = itemTs.TSId;
                    tsErrorCode.MOSeq = itemTs.MOSeq;
                    tsFacade.AddTSErrorCode(tsErrorCode);
                }
                if (((TSErrorCode2Location)errorInfo[i]).ErrorLocation.Trim() != string.Empty)
                {
                    tsErrorCode2Location.AB = ((TSErrorCode2Location)errorInfo[i]).AB;
                    tsErrorCode2Location.ErrorLocation = ((TSErrorCode2Location)errorInfo[i]).ErrorLocation;
                    tsErrorCode2Location.ErrorCode = ((TSErrorCode2Location)errorInfo[i]).ErrorCode;
                    tsErrorCode2Location.ErrorCodeGroup = ((TSErrorCode2Location)errorInfo[i]).ErrorCodeGroup;
                    tsErrorCode2Location.ItemCode = tsErrorCode.ItemCode;
                    tsErrorCode2Location.MaintainDate = tsErrorCode.MaintainDate;
                    tsErrorCode2Location.MaintainTime = tsErrorCode.MaintainTime;
                    tsErrorCode2Location.MaintainUser = tsErrorCode.MaintainUser;
                    tsErrorCode2Location.MEMO = "";
                    tsErrorCode2Location.MOCode = tsErrorCode.MOCode;
                    tsErrorCode2Location.ModelCode = tsErrorCode.ModelCode;
                    tsErrorCode2Location.RunningCard = tsErrorCode.RunningCard;
                    tsErrorCode2Location.RunningCardSequence = tsErrorCode.RunningCardSequence;
                    if (tsErrorCode2Location.ErrorLocation.IndexOf(".") < 0)
                        tsErrorCode2Location.SubErrorLocation = tsErrorCode2Location.ErrorLocation;
                    else
                        tsErrorCode2Location.SubErrorLocation = tsErrorCode2Location.ErrorLocation.Substring(
                            0, tsErrorCode2Location.ErrorLocation.IndexOf("."));
                    tsErrorCode2Location.TSId = tsErrorCode.TSId;

                    tsErrorCode2Location.ShiftDay = tsErrorCode2Location.MaintainDate;
                    tsErrorCode2Location.MOSeq = tsErrorCode.MOSeq;
                    tsFacade.AddTSErrorCode2Location(tsErrorCode2Location);
                }
            }
            #endregion

            return msg;
        }

    }
}
