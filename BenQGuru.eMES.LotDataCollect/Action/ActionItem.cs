using System;
using System.Collections;
using UserControl;
using BenQGuru.eMES.Domain.LotDataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.LotDataCollect.Action
{
    /// <summary>
    /// 文件名:		ActionItem.cs
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		Mark Lee
    /// 创建日期:	2005-05-17 11:23:20
    /// 修改人:
    /// 修改日期:
    /// 描 述:	上料采集
    /// 版 本:	
    /// </summary>
    public class ActionItem : IActionWithStatus, IActionWithStatusNew
    {

        private IDomainDataProvider _domainDataProvider = null;
        private int seqForDeductQty = 0;//扣料时记录扣料顺序
        //		public ActionItem()
        //		{	
        //		}

        public ActionItem(IDomainDataProvider domainDataProvider)
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
        //Laws Lu,2005/12/23,新增INNO上料记录
        //Laws Lu,2006/01/06,允许记录Lot料的明细记录 
        public void InsertINNOOnWipItem(ActionEventArgs actionEventArgs, DataCollectFacade dataCollectFacade, object[] OPBOMDetail)
        {
            string iNNO = ((CINNOActionEventArgs)actionEventArgs).INNO;
            LotSimulation simulation = ((CINNOActionEventArgs)actionEventArgs).ProductInfo.NowSimulation;
            LotSimulationReport simulationReport = ((CINNOActionEventArgs)actionEventArgs).ProductInfo.NowSimulationReport;
            MaterialFacade material = new MaterialFacade(_domainDataProvider);
            //object[] mINNOs = material.GetLastMINNOs(iNNO);
            object[] mINNOs = OPBOMDetail;
            int i = 0;
            if (mINNOs == null)
            {
                throw new Exception("$CS_INNO_NOT_EXIST");
            }
            foreach (MINNO mINNO in mINNOs)
            {
                if (mINNO == null)
                    throw new Exception("$CS_INNOnotExist");
                if (mINNO.MOCode != simulation.MOCode)
                    throw new Exception("$CS_INNOnotForMO $CS_Param_MOCode=" + mINNO.MOCode);
                if (mINNO.RouteCode != simulation.RouteCode)
                    throw new Exception("$CS_INNOnotForRoute $CS_Param_RouteCode=" + mINNO.RouteCode);
                if (mINNO.OPCode != simulation.OPCode)
                    throw new Exception("$CS_INNOnotForOP $CS_Param_OPCode =" + mINNO.OPCode);
                if (mINNO.ResourceCode != simulation.ResCode)
                    throw new Exception("$CS_INNOnotForResource $CS_Param_ResourceCode=" + mINNO.ResourceCode);

                LotOnWipItem wipItem = new LotOnWipItem();
                wipItem.DateCode = mINNO.DateCode;
                wipItem.LOTNO = mINNO.LotNO;/*ActionOnLineHelper.StringNull;*/
                wipItem.MItemCode = mINNO.MItemCode;/*ActionOnLineHelper.StringNull;*/
                wipItem.VendorCode = mINNO.VendorCode;
                wipItem.VendorItemCode = mINNO.VendorItemCode;
                wipItem.Version = mINNO.Version;
                wipItem.Eattribute1 = simulation.EAttribute1;
                wipItem.ItemCode = simulation.ItemCode;
                wipItem.ResCode = simulation.ResCode;
                wipItem.RouteCode = simulation.RouteCode;
                wipItem.LotCode = simulation.LotCode;
                wipItem.LotSeq = simulation.LotSeq;
                wipItem.SegmentCode = simulationReport.SegmentCode;
                wipItem.BeginShiftCode = simulationReport.BeginShiftCode;
                wipItem.ShiftTypeCode = simulationReport.ShiftTypeCode;
                wipItem.StepSequenceCode = simulationReport.StepSequenceCode;
                wipItem.BeginTimePeriodCode = simulationReport.BeginTimePeriodCode;
                wipItem.MOCode = simulation.MOCode;
                wipItem.ModelCode = simulation.ModelCode;
                wipItem.OPCode = simulation.OPCode;
                wipItem.CollectStatus = simulation.CollectStatus;
                wipItem.BeginDate= simulation.BeginDate;
                wipItem.BeginTime = simulation.BeginTime;
                wipItem.MaintainUser = simulation.MaintainUser;

                wipItem.Qty = (int)mINNO.Qty * simulation.LotQty;
                wipItem.MCardType = MCardType.MCardType_INNO;
                //Laws Lu,2005/12/20,新增	采集类型
                wipItem.ActionType = (int)MaterialType.CollectMaterial;
                wipItem.MOSeq = simulation.MOSeq;   // Added by Icyer 2007/07/02

                dataCollectFacade.AddLotOnWIPItem(wipItem);

                i++;
            }
        }

        #region 扣料
        //add by Jarvis Chen 20120315

        public void UpdateStorageQty(StorageLotInfo lotInfo, InventoryFacade inventoryFacade, decimal qty)
        {
            object obj = inventoryFacade.GetStorageInfo(lotInfo.Storageid, lotInfo.Mcode, lotInfo.Stackcode);
            if (obj != null)
            {
                (obj as StorageInfo).Storageqty = (obj as StorageInfo).Storageqty - qty;
                //完成扣料：剩余数量为0删除记录，>0时更新数量 Jarvis 20120316
                if ((obj as StorageInfo).Storageqty == 0)
                {
                    inventoryFacade.DeleteStorageInfo((obj as StorageInfo));
                }
                else
                {
                    inventoryFacade.UpdateStorageInfo(obj as StorageInfo);
                }
            }
        }

        public Messages DeductQty(ActionEventArgs actionEventArgs, DataCollectFacade dataCollectFacade, MINNO minno)
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            MaterialFacade materialFacade = new MaterialFacade(this.DataProvider);
            MOFacade moFacade = new MOFacade(this.DataProvider);
            OPBOMFacade opbomFacade = new OPBOMFacade(this.DataProvider);
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            InventoryFacade inventoryFacade = new InventoryFacade(this.DataProvider);
            LotSimulation simulation = ((CINNOActionEventArgs)actionEventArgs).ProductInfo.NowSimulation;
            LotSimulationReport simulationReport = ((CINNOActionEventArgs)actionEventArgs).ProductInfo.NowSimulationReport;
            Messages returnValue = new Messages();
            string lotNoList = string.Empty;// add by Jarvis For onWipItem                       

            ProductInfo productionInfo = actionEventArgs.ProductInfo;
            LotSimulation sim = actionEventArgs.ProductInfo.NowSimulation;
            int orgid = actionEventArgs.ProductInfo.Resource.OrganizationID;
            MO mo = actionEventArgs.ProductInfo.CurrentMO;

            if (mo == null)
            {
                mo = moFacade.GetMO(sim.MOCode) as MO;
            }

            //获取当前工单号
            string moCode = productionInfo.NowSimulation.MOCode;
            //获取当前工序号
            string opCode = productionInfo.NowSimulation.OPCode;
            //获取当前产品号
            string itemCode = productionInfo.NowSimulation.ItemCode;
            //获取途程代码 
            string routeCode = productionInfo.NowSimulation.RouteCode;
            string resCode = productionInfo.Resource.ResourceCode;
            //获取ORGID
            int orgID = productionInfo.Resource.OrganizationID;
            string moBomVer = string.Empty;

            object objMo = moFacade.GetMO(moCode);

            if (objMo != null)
            {
                moBomVer = (objMo as MO).BOMVersion;
            }

            //获取物料名称
            string MItemName = string.Empty;
            Domain.MOModel.Material material = ((Domain.MOModel.Material)itemFacade.GetMaterial(minno.MItemCode, orgID));
            if (material != null)
            {
                MItemName = material.MaterialName;
            }

            //添加产品已上料扣料判断  tblonwip
            string lotNo = productionInfo.NowSimulation.LotCode;
            decimal seq = productionInfo.NowSimulation.LotSeq;
            //object[] objOnWip = dataCollectFacade.QueryLotOnWIP(lotNo, moCode, opCode, "CINNO");

            //if (objOnWip != null && objOnWip.Length > 0)
            //{
            //    return returnValue;
            //}

            //remove by Jarvis 不检查工单BOM 20120321
            //object[] objMoBoms = moFacade.QueryMoBom(sim.ItemCode, minno.MItemCode, sim.MOCode);//检查工单BOM是否有该首选料, Jarvis 20120319
            //if (objMoBoms == null)
            //{
            //    throw new Exception("$CS_ItemCode[" + minno.MItemCode + "]" + "$Error_NotExistInMoBOM" + String.Format("[$MOCode='{0}']", sim.MOCode));
            //}

            object[] opbomObjs = opbomFacade.QueryOPBOMDetail(sim.ItemCode, minno.MItemCode, string.Empty, string.Empty, string.Empty, sim.RouteCode, opCode, (int)MaterialType.CollectMaterial, int.MinValue, int.MaxValue, orgid, true);
            if (opbomObjs == null)
            {
                throw new Exception("$CS_ItemCode[" + minno.MItemCode + "]" + "$Error_NotExistInOPBOM" + String.Format("[$ItemCode='{0}']", sim.ItemCode));
            }

            object[] moRouteObjs = moFacade.QueryMORoutes(sim.MOCode, sim.RouteCode);
            if (moRouteObjs == null)
            {
                throw new Exception("$Error_MORouteNOExist");
            }

            bool iflag = false;

            decimal iOPBOMItemQty = 0;
            //需要比对的子阶料料号 比对成功的 只在工单生产BOM 中
            if (opbomObjs == null)//去掉检查工单BOM是否为空
            {
                return returnValue;
            }

            //以工单标准BOM为基准,扣减当前工单的倒冲库存地（tblmo. EATTRIBUTE2）中相对应的库存信息
            //for (int n = 0; n < objMoBoms.Length; n++)//去掉工单BOM 20120321 Jarvis
            //{
            //求得opbom中对应的料品
            string TempMOBOMItemCode = minno.MItemCode;
            iflag = false;
            iOPBOMItemQty = 0;
            for (int j = 0; j < opbomObjs.Length; j++)
            {
                if (TempMOBOMItemCode.ToUpper() == ((OPBOMDetail)opbomObjs[j]).OPBOMItemCode.ToUpper()
                    || TempMOBOMItemCode.ToUpper() == ((OPBOMDetail)opbomObjs[j]).OPBOMSourceItemCode.ToUpper())
                {   //子阶料存在或有替代料可用
                    iflag = true;
                    //TempMOBOMItemCode = ((OPBOMDetail)opbomObjs[j]).OPBOMItemCode;//remove by Jarvis 20120316
                    //For 替代料，记录首选料号,Jarvis 20120316
                    //TempMOBOMItemCode = ((MOBOM)objMoBoms[n]).MOBOMItemCode;

                        iOPBOMItemQty = (decimal)((OPBOMDetail)opbomObjs[j]).OPBOMItemQty;
                            iOPBOMItemQty *= sim.LotQty;
                        
                    break;
                }
            }

            //比对成功：子阶料料号一致
            if (iflag)//子阶料不存在， 即只在工单标准bom中
            {
                //object[] objInfos = inventoryFacade.QueryStorageInfoByIDAndMCode(mo.EAttribute2, TempMOBOMItemCode.ToUpper());
                object[] objInfos = inventoryFacade.QueryStorageInfoByIDAndMCode(TempMOBOMItemCode.ToUpper());
                if (objInfos == null)
                {
                    throw new Exception("$CS_ItemCode[" + minno.MItemCode + "]" + "$CS_StorageQty_ERROR");
                }

                //获取物料的总库存数，Jarvis 20120316
                //decimal total = inventoryFacade.GetStorageQty(mo.EAttribute2, TempMOBOMItemCode.ToUpper());
                decimal total = inventoryFacade.GetStorageQty(TempMOBOMItemCode.ToUpper());

                if (total < iOPBOMItemQty)
                {
                    throw new Exception("$CS_ItemCode[" + minno.MItemCode + "]" + "$CS_StorageQty_ERROR");
                }

                #region //先判断备料批号中的数据量，不够扣减就退出
                decimal temlotQty = 0;
                object[] objStorageLotInfo = null;
                ArrayList StorageLotInfoList = new ArrayList();
                
                    //获取子阶料的备料信息，Jarvis 20120316，按Seq排序
                    object[] minnoss = materialFacade.QueryMINNO_New(moCode, routeCode, opCode, resCode, moBomVer, minno.MSourceItemCode);//获取同一首选料的备料信息，Jarvis 20120321

                    //获取备料信息中可扣减数，Jarvis 20120316
                    foreach (MINNO temp in minnoss)
                    {
                        //objStorageLotInfo = inventoryFacade.QueryStorageLot(temp.LotNO, mo.EAttribute2, temp.MItemCode);
                        objStorageLotInfo = inventoryFacade.QueryStorageLot(temp.LotNO, temp.MItemCode);
                        if (objStorageLotInfo != null)
                        {
                            foreach (StorageLotInfo lotInfo in objStorageLotInfo)
                            {
                                temlotQty += lotInfo.Lotqty;
                                if (lotInfo.Lotqty <= 0)//如果批数量为0不记录该批
                                {
                                    continue;
                                }
                                StorageLotInfoList.Add(lotInfo);
                            }
                        }
                    }
                

                if (temlotQty < iOPBOMItemQty)
                {
                    throw new Exception("$CS_ItemCode[" + minno.MItemCode + "-" + MItemName + "]" + "$CS_DeductQty_ERROR");
                }
                #endregion

                #region 数量可以扣减

                foreach (StorageLotInfo lotInfo in StorageLotInfoList)
                {

                    if (iOPBOMItemQty > lotInfo.Lotqty)
                    {
                        iOPBOMItemQty = iOPBOMItemQty - lotInfo.Lotqty;
                        inventoryFacade.DeleteStorageLotInfo(lotInfo);
                        this.UpdateStorageQty(lotInfo, inventoryFacade, lotInfo.Lotqty);
                        lotNoList += ("," + lotInfo.Lotno + ",");

                        #region 记录过账信息
                        LotOnWipItem wipItem = new LotOnWipItem();
                        MINNO minnoTemp = null;
                        object[] minnoTemps = materialFacade.QueryMINNO(moCode, routeCode, opCode, resCode, moBomVer, lotInfo.Mcode, minno.MSourceItemCode, lotInfo.Lotno);
                        if (minnoTemps != null)
                        {
                            minnoTemp = (MINNO)minnoTemps[0];
                        }
                        wipItem.DateCode = minnoTemp.DateCode;
                        wipItem.LOTNO = minnoTemp.LotNO;
                        wipItem.MItemCode = minnoTemp.MItemCode;
                        wipItem.VendorCode = minnoTemp.VendorCode;
                        wipItem.VendorItemCode = minnoTemp.VendorItemCode;
                        wipItem.Version = minnoTemp.Version;
                        wipItem.MSeq = seqForDeductQty;
                        wipItem.MCardType = minno.EAttribute1;

                        wipItem.Eattribute1 = simulation.EAttribute1;
                        wipItem.ItemCode = simulation.ItemCode;
                        wipItem.ResCode = simulation.ResCode;
                        wipItem.RouteCode = simulation.RouteCode;
                        wipItem.LotCode = simulation.LotCode;
                        wipItem.LotSeq = simulation.LotSeq;
                        wipItem.SegmentCode = simulationReport.SegmentCode;
                        wipItem.BeginShiftCode = simulationReport.BeginShiftCode;
                        wipItem.ShiftTypeCode = simulationReport.ShiftTypeCode;
                        wipItem.StepSequenceCode = simulationReport.StepSequenceCode;
                        wipItem.BeginTimePeriodCode = simulationReport.BeginTimePeriodCode;
                        wipItem.MOCode = simulation.MOCode;
                        wipItem.ModelCode = simulation.ModelCode;
                        wipItem.OPCode = simulation.OPCode;
                        wipItem.CollectStatus = simulation.CollectStatus;
                        wipItem.BeginDate = simulation.BeginDate;
                        wipItem.BeginTime = simulation.BeginTime;
                        wipItem.MaintainUser = simulation.MaintainUser;
                        wipItem.TransStatus = TransactionStatus.TransactionStatus_YES;
                        wipItem.Qty = lotInfo.Lotqty;
                        
                        wipItem.ActionType = (int)MaterialType.CollectMaterial;
                        wipItem.MOSeq = simulation.MOSeq; 

                        dataCollectFacade.AddLotOnWIPItem(wipItem);

                        LotSimulationReport simulationRpt = dataCollectFacade.GetLastLotSimulationReport(wipItem.LotCode);
                        if (simulationRpt != null)
                        {
                            dataCollectFacade.UpdateLotSimulationReport(simulationRpt);
                                                        
                        }
                        seqForDeductQty++;
                        #endregion
                    }
                    else
                    {
                        lotInfo.Lotqty = lotInfo.Lotqty - iOPBOMItemQty;
                        inventoryFacade.UpdateStorageLotInfo(lotInfo);
                        this.UpdateStorageQty(lotInfo, inventoryFacade, iOPBOMItemQty);

                        #region 记录过账信息
                        LotOnWipItem wipItem = new LotOnWipItem();
                        MINNO minnoTemp = null;
                        object[] minnoTemps = materialFacade.QueryMINNO(moCode, routeCode, opCode, resCode, moBomVer, lotInfo.Mcode, minno.MSourceItemCode, lotInfo.Lotno);
                        if (minnoTemps != null)
                        {
                            minnoTemp = (MINNO)minnoTemps[0];
                        }
                        wipItem.DateCode = minnoTemp.DateCode;
                        wipItem.LOTNO = minnoTemp.LotNO;
                        wipItem.MItemCode = minnoTemp.MItemCode;
                        wipItem.VendorCode = minnoTemp.VendorCode;
                        wipItem.VendorItemCode = minnoTemp.VendorItemCode;
                        wipItem.Version = minnoTemp.Version;
                        wipItem.MSeq = seqForDeductQty;
                        wipItem.MCardType = minno.EAttribute1;

                        wipItem.Eattribute1 = simulation.EAttribute1;
                        wipItem.ItemCode = simulation.ItemCode;
                        wipItem.ResCode = simulation.ResCode;
                        wipItem.RouteCode = simulation.RouteCode;
                        wipItem.LotCode = simulation.LotCode;
                        wipItem.LotSeq = simulation.LotSeq;
                        wipItem.SegmentCode = simulationReport.SegmentCode;
                        wipItem.BeginShiftCode = simulationReport.BeginShiftCode;
                        wipItem.ShiftTypeCode = simulationReport.ShiftTypeCode;
                        wipItem.StepSequenceCode = simulationReport.StepSequenceCode;
                        wipItem.BeginTimePeriodCode = simulationReport.BeginTimePeriodCode;
                        wipItem.MOCode = simulation.MOCode;
                        wipItem.ModelCode = simulation.ModelCode;
                        wipItem.OPCode = simulation.OPCode;
                        wipItem.CollectStatus = simulation.CollectStatus;
                        wipItem.BeginDate = simulation.BeginDate;
                        wipItem.BeginTime = simulation.BeginTime;
                        wipItem.MaintainUser = simulation.MaintainUser;
                        wipItem.TransStatus = TransactionStatus.TransactionStatus_YES;
                        wipItem.Qty = iOPBOMItemQty;

                        wipItem.ActionType = (int)MaterialType.CollectMaterial;
                        wipItem.MOSeq = simulation.MOSeq;

                        dataCollectFacade.AddLotOnWIPItem(wipItem);

                        LotSimulationReport simulationRpt = dataCollectFacade.GetLastLotSimulationReport(wipItem.LotCode);
                        if (simulationRpt != null)
                        {
                            dataCollectFacade.UpdateLotSimulationReport(simulationRpt);
                                                        
                        }
                        seqForDeductQty++;
                        #endregion
                        iOPBOMItemQty = 0;
                        lotNoList += ("," + lotInfo.Lotno + ",");
                        break;
                    }

                }

                #endregion
            }
            //}                
            return returnValue;
        }

        #endregion

        public Messages InsertLotOnWipItem(ActionEventArgs actionEventArgs, DataCollectFacade dataCollectFacade, object[] OPBOMDetail)
        {
            Messages returnValue = new Messages();

            string iNNO = ((CINNOActionEventArgs)actionEventArgs).INNO;
            LotSimulation simulation = ((CINNOActionEventArgs)actionEventArgs).ProductInfo.NowSimulation;
            LotSimulationReport simulationReport = ((CINNOActionEventArgs)actionEventArgs).ProductInfo.NowSimulationReport;
            MaterialFacade material = new MaterialFacade(_domainDataProvider);
            //object[] mINNOs = material.GetLastMINNOs(iNNO);
            object[] mINNOs = OPBOMDetail;
            int i = 0;
            if (mINNOs == null)
            {
                throw new Exception("$CS_INNO_NOT_EXIST");
            }

            //确定是否需要扣料
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            Parameter objParameter = (Parameter)systemSettingFacade.GetParameter("DEDUCTQTY", "DEDUCTMATERIAL");
            bool isDeductQty = true;
            if (objParameter == null || objParameter.ParameterAlias != "Y")
            {
                isDeductQty = false;
            }

            foreach (MINNO mINNO in mINNOs)
            {
                if (mINNO == null)
                    throw new Exception("$CS_INNOnotExist");
                if (!isDeductQty)//如果不扣料只记录同一首选料的一笔过账
                {
                    //原有过账记录，挪到此处
                    LotOnWipItem wipItem = new LotOnWipItem();

                    wipItem.DateCode = mINNO.DateCode;
                    wipItem.LOTNO = mINNO.LotNO;
                    wipItem.MItemCode = mINNO.MItemCode;
                    wipItem.VendorCode = mINNO.VendorCode;
                    wipItem.VendorItemCode = mINNO.VendorItemCode;
                    wipItem.Version = mINNO.Version;
                    wipItem.MSeq = i;
                    wipItem.MCardType = mINNO.EAttribute1;

                    wipItem.Eattribute1 = simulation.EAttribute1;
                    wipItem.ItemCode = simulation.ItemCode;
                    wipItem.ResCode = simulation.ResCode;
                    wipItem.RouteCode = simulation.RouteCode;
                    wipItem.LotCode = simulation.LotCode;
                    wipItem.LotSeq = simulation.LotSeq;
                    wipItem.SegmentCode = simulationReport.SegmentCode;
                    wipItem.BeginShiftCode = simulationReport.BeginShiftCode;
                    wipItem.ShiftTypeCode = simulationReport.ShiftTypeCode;
                    wipItem.StepSequenceCode = simulationReport.StepSequenceCode;
                    wipItem.BeginTimePeriodCode = simulationReport.BeginTimePeriodCode;
                    wipItem.MOCode = simulation.MOCode;
                    wipItem.ModelCode = simulation.ModelCode;
                    wipItem.OPCode = simulation.OPCode;
                    wipItem.CollectStatus = simulation.CollectStatus;
                    wipItem.BeginDate = simulation.BeginDate;
                    wipItem.BeginTime = simulation.BeginTime;
                    wipItem.MaintainUser = simulation.MaintainUser;
                    wipItem.TransStatus = TransactionStatus.TransactionStatus_YES;

                    if (mINNO.Qty.ToString() != string.Empty && Convert.ToInt32(mINNO.Qty) != 0)
                    {
                        wipItem.Qty = mINNO.Qty * simulation.LotQty;
                    }
                    else
                    {
                        wipItem.Qty = simulation.LotQty;
                    }

                    //Laws Lu,2005/12/20,新增	采集类型
                    wipItem.ActionType = (int)MaterialType.CollectMaterial;
                    wipItem.MOSeq = simulation.MOSeq;   // Added by Icyer 2007/07/02

                    dataCollectFacade.AddLotOnWIPItem(wipItem);

                    LotSimulationReport simulationRpt = dataCollectFacade.GetLastLotSimulationReport(wipItem.LotCode);
                    if (simulationRpt != null)
                    {
                        dataCollectFacade.UpdateLotSimulationReport(simulationRpt);

                        // End Added
                    }
                    i++;
                }
                else
                {
                    //add by Jarvis 20120316 For 扣料
                    DeductQty(actionEventArgs, dataCollectFacade, mINNO);
                }
                
            }
            seqForDeductQty = 0;
            return returnValue;
        }


        //Laws Lu,2005/12/23,新增INNO上料记录
        //Laws Lu,2006/01/06,允许记录Lot料的明细记录
        public void InsertINNOOnWipItem(ActionEventArgs actionEventArgs, DataCollectFacade dataCollectFacade)
        {
            string iNNO = ((CINNOActionEventArgs)actionEventArgs).INNO;
            LotSimulation simulation = ((CINNOActionEventArgs)actionEventArgs).ProductInfo.NowSimulation;
            LotSimulationReport simulationReport = ((CINNOActionEventArgs)actionEventArgs).ProductInfo.NowSimulationReport;
            MaterialFacade material = new MaterialFacade(_domainDataProvider);
            object[] mINNOs = material.GetLastMINNOs(iNNO);
            int i = 0;
            if (mINNOs == null)
            {
                throw new Exception("$CS_INNO_NOT_EXIST");
            }
            foreach (MINNO mINNO in mINNOs)
            {
                if (mINNO == null)
                    throw new Exception("$CS_INNOnotExist");
                if (mINNO.MOCode != simulation.MOCode)
                    throw new Exception("$CS_INNOnotForMO $CS_Param_MOCode=" + mINNO.MOCode);
                if (mINNO.RouteCode != simulation.RouteCode)
                    throw new Exception("$CS_INNOnotForRoute $CS_Param_RouteCode=" + mINNO.RouteCode);
                if (mINNO.OPCode != simulation.OPCode)
                    throw new Exception("$CS_INNOnotForOP $CS_Param_OPCode =" + mINNO.OPCode);
                if (mINNO.ResourceCode != simulation.ResCode)
                    throw new Exception("$CS_INNOnotForResource $CS_Param_ResourceCode=" + mINNO.ResourceCode);

                LotOnWipItem wipItem = new LotOnWipItem();


                wipItem.DateCode = mINNO.DateCode;
                wipItem.LOTNO = mINNO.MItemPackedNo;//.LotNO;
                wipItem.MItemCode = mINNO.MItemCode;/*ActionOnLineHelper.StringNull;*/
                wipItem.MSeq = i;
                wipItem.VendorCode = mINNO.VendorCode;
                wipItem.VendorItemCode = mINNO.VendorItemCode;
                wipItem.Version = mINNO.Version;
                wipItem.Eattribute1 = simulation.EAttribute1;
                wipItem.ItemCode = simulation.ItemCode;
                wipItem.ResCode = simulation.ResCode;
                wipItem.RouteCode = simulation.RouteCode;
                wipItem.LotCode = simulation.LotCode;
                wipItem.LotSeq = simulation.LotSeq;
                wipItem.SegmentCode = simulationReport.SegmentCode;
                wipItem.BeginShiftCode = simulationReport.BeginShiftCode;
                wipItem.ShiftTypeCode = simulationReport.ShiftTypeCode;
                wipItem.StepSequenceCode = simulationReport.StepSequenceCode;
                wipItem.BeginTimePeriodCode = simulationReport.BeginTimePeriodCode;
                wipItem.MOCode = simulation.MOCode;
                wipItem.ModelCode = simulation.ModelCode;
                wipItem.OPCode = simulation.OPCode;
                wipItem.CollectStatus = simulation.CollectStatus;
                wipItem.BeginDate = simulation.BeginDate;
                wipItem.BeginTime = simulation.BeginTime;
                wipItem.MaintainUser = simulation.MaintainUser;

                wipItem.Qty = mINNO.Qty * simulation.LotQty;
                wipItem.MCardType = MCardType.MCardType_INNO;
                //Laws Lu,2005/12/20,新增	采集类型
                wipItem.ActionType = (int)MaterialType.CollectMaterial;
                wipItem.MOSeq = simulation.MOSeq;   // Added by Icyer 2007/07/02

                dataCollectFacade.AddLotOnWIPItem(wipItem);

                i++;
            }
        }


        /// <summary>
        /// ** 功能描述:	上料采集，包括INNO和KEYPARTS上料
        ///							集成上料号，必须存在且对应于RESOURCE
        ///							KEYPARTS料的检查在前台已经做完
        ///							事务必须在外面处理
        /// ** 作 者:		Mark Lee
        /// ** 日 期:		2005-06-01
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="actionEventArgs"></param>
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

                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(actionEventArgs));
                if (messages.IsSuccess())
                {
                    //上料检查  分INNO、KEYPARTS TODO 
                    //if (actionEventArgs.ActionType ==ActionType.DataCollectAction_CollectINNO)


                    messages.AddMessages(dataCollect.Execute(actionEventArgs));
                    if (messages.IsSuccess())
                    {
                        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);

                        #region 填写上料信息表  分INNO、KEYPARTS TODO
                        if (actionEventArgs.ActionType == ActionType.DataCollectAction_CollectINNO)
                        {

                            InsertINNOOnWipItem(actionEventArgs, dataCollectFacade);

                        }
                        #endregion

                        // Added by Icyer 2005/08/16
                        // 上料扣库存
                        // 暂时屏蔽 FOR 夏新P3版本 MARK LEE 2005/08/22
                        // 取消屏蔽 Icyer 2005/08/23
                        //Laws Lu,2005/10/20,新增	使用配置文件来控制物料模块是否使用
                        if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                        {
                            BenQGuru.eMES.Material.WarehouseFacade wfacade = new WarehouseFacade(this.DataProvider);
                            wfacade.CollectMaterialStock(actionEventArgs.ProductInfo.NowSimulation.LotCode, actionEventArgs.ProductInfo.NowSimulation.LotSeq.ToString(), actionEventArgs.ProductInfo.NowSimulation.MOCode);
                        }
                        // Added end

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

        // Added by Icyer 2005/10/28
        //扩展一个带ActionCheckStatus参数的方法
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
                msgAutoGoMO = actionGoToMO.AutoGoMO(actionEventArgs, actionCheckStatus);
                // Added end

                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(actionEventArgs, actionCheckStatus));

                if (messages.IsSuccess())
                {
                    if (actionCheckStatus.NeedUpdateSimulation == true)
                    {
                        messages.AddMessages(dataCollect.Execute(actionEventArgs));
                    }
                    else
                    {
                        messages.AddMessages(dataCollect.Execute(actionEventArgs, actionCheckStatus));
                    }

                    BenQGuru.eMES.Material.WarehouseFacade wfacade = null;
                    if (messages.IsSuccess())
                    {
                        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                        #region 填写上料信息表  分INNO
                        if (actionEventArgs.ActionType == ActionType.DataCollectAction_CollectINNO)
                        {
                            if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                            {
                                wfacade = ((CINNOActionEventArgs)actionEventArgs).Warehouse;
                            }

                            InsertINNOOnWipItem(actionEventArgs, dataCollectFacade);

                        }

                        #endregion
                    }
                    // Added by Icyer 2005/08/16
                    // 上料扣库存
                    // 暂时屏蔽 FOR 夏新P3版本 MARK LEE 2005/08/22
                    // 取消屏蔽 Icyer 2005/08/23
                    //Laws Lu,2005/10/20,新增	使用配置文件来控制物料模块是否使用
                    if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                    {
                        //BenQGuru.eMES.Material.WarehouseFacade wfacade = new WarehouseFacade(this.DataProvider);
                        if (wfacade != null)
                            wfacade.CollectMaterialStock(actionEventArgs.ProductInfo.NowSimulation.LotCode, actionEventArgs.ProductInfo.NowSimulation.LotSeq.ToString(), actionEventArgs.ProductInfo.NowSimulation.MOCode);
                    }
                    // Added end

                    //将Action加入列表
                    actionCheckStatus.ActionList.Add(actionEventArgs);
                    //AMOI  MARK  END

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
        // Added end

        //扩展一个带ActionCheckStatus和OPBOMDetail参数的方法
        public Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus, object[] OPBOMDetail)
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
                msgAutoGoMO = actionGoToMO.AutoGoMO(actionEventArgs, actionCheckStatus);
                // Added end

                ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
                //填写SIMULATION 检查工单、ID、途程、操作
                messages.AddMessages(dataCollect.CheckID(actionEventArgs, actionCheckStatus));

                if (messages.IsSuccess())
                {
                    //上料检查  分INNO、KEYPARTS TODO 
                    //if (actionEventArgs.ActionType ==ActionType.DataCollectAction_CollectINNO)


                    if (actionCheckStatus.NeedUpdateSimulation == true)
                    {
                        messages.AddMessages(dataCollect.Execute(actionEventArgs));
                    }
                    else
                    {
                        messages.AddMessages(dataCollect.Execute(actionEventArgs, actionCheckStatus));
                    }

                    BenQGuru.eMES.Material.WarehouseFacade wfacade = null;
                    if (messages.IsSuccess())
                    {
                        DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                        #region 填写上料信息表  分INNO、KEYPARTS
                        if (actionEventArgs.ActionType == ActionType.DataCollectAction_CollectINNO)
                        {
                            if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                            {
                                wfacade = ((CINNOActionEventArgs)actionEventArgs).Warehouse;
                            }

                            messages.AddMessages(InsertLotOnWipItem(actionEventArgs, dataCollectFacade, OPBOMDetail));
                            if (!messages.IsSuccess())
                            {
                                return messages;
                            }
                        }
 
                        #endregion
                    }
                    // Added by Icyer 2005/08/16
                    // 上料扣库存
                    // 暂时屏蔽 FOR 夏新P3版本 MARK LEE 2005/08/22
                    // 取消屏蔽 Icyer 2005/08/23
                    //Laws Lu,2005/10/20,新增	使用配置文件来控制物料模块是否使用
                    if (System.Configuration.ConfigurationSettings.AppSettings["NeedMaterialModule"].Trim() == "1")
                    {
                        //BenQGuru.eMES.Material.WarehouseFacade wfacade = new WarehouseFacade(this.DataProvider);
                        if (wfacade != null)
                            wfacade.CollectMaterialStock(actionEventArgs.ProductInfo.NowSimulation.LotCode, actionEventArgs.ProductInfo.NowSimulation.LotSeq.ToString(), actionEventArgs.ProductInfo.NowSimulation.MOCode);
                    }
                    // Added end


                    //将Action加入列表
                    actionCheckStatus.ActionList.Add(actionEventArgs);


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

        public const string MCardType_INNO = "1";

    }


}
