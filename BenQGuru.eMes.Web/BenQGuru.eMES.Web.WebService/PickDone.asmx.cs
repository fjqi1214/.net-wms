using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.WebService
{
    /// <summary>
    /// PickDone 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class PickDone : System.Web.Services.WebService
    {
        WarehouseFacade facade = null;
        InventoryFacade _Invenfacade = null;
        public PickDone()
        {
            InitDbItems();
        }
        private string m_DbName;
        private IDomainDataProvider _domainDataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(m_DbName);
                }
                return _domainDataProvider;
            }
        }
        private void InitDbItems()
        {
            foreach (var item in BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.Settings)
            {
                if (item.Default)
                    m_DbName = item.Name;

            }
        }
        [WebMethod(EnableSession = true)]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession = true)]   //查询
        public DataTable PickNOQueryGrid(string PickNo)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }



            PickNo = PickNo.ToUpper();
            Pick pick = (Pick)facade.GetPick(PickNo);

            PickDetailEx[] details = null;
            if (pick.PickType == PickType.PickType_DNC || pick.PickType == PickType.PickType_DNZC)
                details = facade.QueryPickDetailForDN(PickNo);
            else
                details = facade.QueryPickDetailForNotDN(PickNo);

            DataTable dt = new DataTable("ExampleDataTable");

            dt.Columns.Add("Check", typeof(string));
            dt.Columns.Add("Merge", typeof(string));
            dt.Columns.Add("DQMCode", typeof(string));  //鼎桥物料编码
            dt.Columns.Add("CusMcode", typeof(string));  //华为物料编码
            dt.Columns.Add("RequireQty", typeof(string));  //需求数量
            dt.Columns.Add("PickedQTY", typeof(string));   //已捡熟料
            dt.Columns.Add("PickNo", typeof(string));
            dt.Columns.Add("PickLine", typeof(string));
            for (int i = 0; i < details.Length; i++)
            {
                PickDetailEx PikDetail = details[i] as PickDetailEx;
                dt.Rows.Add("", details[i].SapCount > 1 ? "是" : "否", PikDetail.DQMCode,
                    pick.PickType == PickType.PickType_UB ? PikDetail.CustMCode : PikDetail.VEnderItemCode,
                    PikDetail.QTY.ToString(),
                    PikDetail.SQTY.ToString(),
                    PikDetail.PickNo, PikDetail.PickLine);
            }


            return dt;
        }
        [WebMethod(EnableSession = true)]  //提交
        public bool SubmitButton(string PickNo, string CartonNo, string Number, string SN, string UserCode, bool IsAll, bool Check, out string message)
        {

            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            if (_Invenfacade == null)
            {
                _Invenfacade = new InventoryFacade(DataProvider);
            }
            PickNo = PickNo.ToUpper();
            CartonNo = CartonNo.ToUpper();
            SN = SN.ToUpper();
            try
            {
                //判断输入的是否是数字
                DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);



                if (IsAll)   //  选择整箱
                {
                    //if (string.IsNullOrEmpty(CartonNo))
                    //{
                    //    return "整箱操作箱号不能为空";
                    //}
                    //string result = CheckStorageCode(PickNo, CartonNo, IsAll, Check, Number, SN);
                    //if (result != "TRUE")
                    //{
                    //    return result;
                    //}

                    this.DataProvider.BeginTransaction();
                    try
                    {

                        object obj = _Invenfacade.GetStorageDetail(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo)));
                        if (obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "库存中没有该箱号";
                            return false;
                        }
                        StorageDetail sto = obj as StorageDetail;
                        object[] pickline_obj = facade.GetPickLine(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickNo)), sto.DQMCode);
                        if (pickline_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "拣货任务令中没有鼎桥物料号";
                            return false;
                        }
                        #region  更新表 pickdetail
                        PickDetail pikd = pickline_obj[0] as PickDetail;
                        pikd.SQTY += sto.AvailableQty;
                        pikd.MaintainDate = dbTime.DBDate;
                        pikd.MaintainTime = dbTime.DBTime;
                        pikd.MaintainUser = UserCode;
                        if (pikd.SQTY == pikd.QTY)
                        {
                            pikd.Status = PickDetail_STATUS.Status_ClosePick;
                        }
                        else
                        {
                            pikd.Status = PickDetail_STATUS.Status_Pick;
                        }
                        facade.UpdatePickdetail(pikd);
                        #endregion
                        #region
                        object obj_pik = _Invenfacade.GetPick(pikd.PickNo);
                        if (obj_pik == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "更新拣货任务令头错误";
                            return false;
                        }
                        else
                        {
                            Pick pi = obj_pik as Pick;
                            if (pi.Status == Pick_STATUS.Status_WaitPick)
                            {
                                pi.Status = Pick_STATUS.Status_Pick;

                                InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                                trans.CartonNO = string.Empty;
                                trans.DqMCode = sto.DQMCode;
                                trans.FacCode = string.Empty;
                                trans.FromFacCode = string.Empty;
                                trans.FromStorageCode = string.Empty;
                                trans.InvNO = (obj_pik as Pick).InvNo;//.InvNo;
                                trans.InvType = (obj_pik as Pick).PickType;
                                trans.LotNo = string.Empty;
                                trans.MaintainDate = dbTime.DBDate;
                                trans.MaintainTime = dbTime.DBTime;
                                trans.MaintainUser = UserCode;
                                trans.MCode = sto.MCode;
                                trans.ProductionDate = 0;
                                trans.Qty = sto.AvailableQty;
                                trans.Serial = 0;
                                trans.StorageAgeDate = 0;
                                trans.StorageCode = (obj_pik as Pick).StorageCode;
                                trans.SupplierLotNo = string.Empty;
                                trans.TransNO = (obj_pik as Pick).PickNo;
                                trans.TransType = "OUT";
                                trans.Unit = string.Empty;
                                trans.ProcessType = "PICK";
                                facade.AddInvInOutTrans(trans);


                                pi.MaintainDate = dbTime.DBDate;
                                pi.MaintainTime = dbTime.DBTime;
                                pi.MaintainUser = UserCode;
                                _Invenfacade.UpdatePick(pi);
                            }

                        }
                        #endregion
                        #region  新增一笔数据到tblPickDetailMaterial
                        Pickdetailmaterial pikm = facade.CreateNewPickdetailmaterial();
                        pikm.Cartonno = sto.CartonNo;
                        pikm.CDate = dbTime.DBDate;
                        pikm.CTime = dbTime.DBTime;
                        pikm.CUser = UserCode;
                        pikm.CustmCode = pikd.CustMCode;
                        pikm.DqmCode = pikd.DQMCode;
                        pikm.LocationCode = sto.LocationCode;
                        pikm.Lotno = sto.Lotno;
                        pikm.MaintainDate = dbTime.DBDate;
                        pikm.MaintainTime = dbTime.DBTime;
                        pikm.MaintainUser = UserCode;
                        pikm.MCode = pikd.MCode;
                        pikm.Pickline = pikd.PickLine;
                        pikm.Pickno = pikd.PickNo;
                        pikm.Production_Date = sto.ProductionDate;
                        //pikm.QcStatus = string.Empty;
                        pikm.Qty = sto.AvailableQty;
                        //pikm.Status = string.Empty;   ////xu yao xiu gai 
                        pikm.StorageageDate = sto.LastStorageAgeDate;
                        pikm.Supplier_lotno = sto.SupplierLotNo;
                        pikm.Unit = sto.Unit;
                        pikm.PQty = 0;

                        facade.AddPickdetailmaterial(pikm);
                        #endregion

                        #region  如果是单件管控，新增一笔数据到tblPickDetailMaterialSN
                        MOModel.ItemFacade itemFacade = new MOModel.ItemFacade(DataProvider);
                        object material_obj = itemFacade.GetMaterial(sto.MCode);
                        if ((material_obj as Domain.MOModel.Material).MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                        {
                            object[] stosn = facade.GetStorageDetailSnbyCartonNo(sto.CartonNo);
                            if (stosn.Length != sto.AvailableQty)
                            {
                                this.DataProvider.RollbackTransaction();

                                message = "箱号：" + sto.CartonNo + "可拣数量不等于该箱的可用数量";
                                return false;
                            }

                            for (int i = 0; i < stosn.Length; i++)
                            {
                                Pickdetailmaterialsn piksn = facade.CreateNewPickdetailmaterialsn();
                                StorageDetailSN stsn = stosn[i] as StorageDetailSN;
                                piksn.Cartonno = stsn.CartonNo;
                                piksn.MaintainDate = dbTime.DBDate;
                                piksn.MaintainTime = dbTime.DBTime;
                                piksn.MaintainUser = UserCode;
                                piksn.Pickline = pikd.PickLine;
                                piksn.Pickno = pikd.PickNo;
                                piksn.QcStatus = string.Empty;
                                piksn.Sn = stsn.SN;
                                facade.AddPickdetailmaterialsn(piksn);
                                #region   更新状态
                                stsn.PickBlock = "Y";
                                stsn.MaintainDate = dbTime.DBDate;
                                stsn.MaintainTime = dbTime.DBTime;
                                stsn.MaintainUser = UserCode;
                                _Invenfacade.UpdateStorageDetailSN(stsn);
                                #endregion
                            }

                        }
                        #endregion
                        #region  更新库存可用数量
                        sto.FreezeQty += sto.AvailableQty;
                        sto.AvailableQty = 0;
                        sto.MaintainDate = dbTime.DBDate;
                        sto.MaintainTime = dbTime.DBTime;
                        sto.MaintainUser = UserCode;
                        _Invenfacade.UpdateStorageDetail(sto);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "捡料出错";
                        return false;
                    }
                }
                else //选择拆箱
                {

                    object obj_pik = _Invenfacade.GetPick(PickNo);
                    if (obj_pik == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "更新拣货任务令头错误";
                        return false;
                    }
                    else
                    {
                        Pick pi = obj_pik as Pick;
                        if (pi.Status == Pick_STATUS.Status_WaitPick)
                        {
                            pi.Status = Pick_STATUS.Status_Pick;

                            InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = string.Empty;
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (obj_pik as Pick).InvNo;//.InvNo;
                            trans.InvType = (obj_pik as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = dbTime.DBDate;
                            trans.MaintainTime = dbTime.DBTime;
                            trans.MaintainUser = UserCode;
                            trans.MCode = string.Empty;
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = (obj_pik as Pick).StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = (obj_pik as Pick).PickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "PICK";
                            facade.AddInvInOutTrans(trans);

                            pi.MaintainDate = dbTime.DBDate;
                            pi.MaintainTime = dbTime.DBTime;
                            pi.MaintainUser = UserCode;

                            _Invenfacade.UpdatePick(pi);
                        }

                    }


                    #region 判断是单件管控还是批管控
                    string ControlType = string.Empty;
                    string CartonCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo));
                    if (!string.IsNullOrEmpty(CartonNo))  //有箱号
                    {

                        object obj = _Invenfacade.GetStorageDetail(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo)));
                        if (obj == null)
                        {

                            message = "库存中没有该箱号信息";
                            return false;
                        }
                        else
                        {
                            StorageDetail sto = obj as StorageDetail;
                            MOModel.ItemFacade itemFacade = new MOModel.ItemFacade(DataProvider);
                            object material_obj = itemFacade.GetMaterial(sto.MCode);
                            if (material_obj == null)
                            {
                                message = "改箱号对应的物料信息在物料表中不存在";
                                return false;
                            }
                            else
                            {
                                Domain.MOModel.Material matr = material_obj as Domain.MOModel.Material;
                                ControlType = matr.MCONTROLTYPE;
                                if (matr.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                                {
                                    if (string.IsNullOrEmpty(SN))
                                    {
                                        message = "单件管控料必须输入SN进行捡料";
                                        return false;
                                    }
                                    else
                                    {
                                        object obj1 = _Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN)));
                                        if (obj1 == null)
                                        {
                                            message = "库存中没有SN数据";
                                            return false;
                                        }
                                        else
                                        {
                                            object obj2 = _Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN)));
                                            if (obj2 == null)
                                            {
                                                message = "箱号或SN出错：没有SN信息";
                                                return false;
                                            }
                                            else
                                            {
                                                string car = (_Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN))) as StorageDetailSN).CartonNo;
                                                if (CartonCode != car)
                                                {
                                                    message = "输入的箱号与SN号不匹配，单件管控料可以只输入SN";
                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    else  // 没有输入箱号
                    {
                        if (!string.IsNullOrEmpty(SN))
                        {
                            object obj = _Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN)));
                            if (obj == null)
                            {
                                message = "库存中没有SN数据";
                                return false;
                            }
                            else
                            {

                                CartonCode = (obj as StorageDetailSN).CartonNo;

                                object obj3 = _Invenfacade.GetStorageDetail(CartonCode);
                                if (obj3 == null)
                                {
                                    message = "库存中没有该箱号信息";
                                    return false;
                                }
                                else
                                {
                                    StorageDetail sto = obj3 as StorageDetail;
                                    MOModel.ItemFacade itemFacade = new MOModel.ItemFacade(DataProvider);
                                    object material_obj = itemFacade.GetMaterial(sto.MCode);
                                    if (material_obj == null)
                                    {
                                        message = "改箱号对应的物料信息在物料表中不存在";
                                        return false;
                                    }
                                    else
                                    {
                                        Domain.MOModel.Material matr = material_obj as Domain.MOModel.Material;
                                        ControlType = matr.MCONTROLTYPE;
                                        if (matr.MCONTROLTYPE != SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                                        {
                                            message = "SN料号不是单件管控料，请重新输入";
                                            return false;
                                        }
                                    }
                                }
                            }


                        }

                    }
                    #endregion
                    //string result = CheckStorageCode(PickNo, CartonNo, IsAll, Check, Number, SN);
                    //if (result != "TRUE")
                    //{
                    //    return result;
                    //}
                    //#endregion
                    try
                    {
                        this.DataProvider.BeginTransaction();
                        //DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                        object sto_obj = null;
                        object[] pickline_obj = null;
                        StorageDetail stor = null;

                        if (string.IsNullOrEmpty(CartonCode))
                        {
                            CartonCode = (_Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN))) as StorageDetailSN).CartonNo;
                        }
                        if (ControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                        {
                            //单件管控
                            object sto_sn = _Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN)));
                            if (sto_sn == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                message = "库存中没有该序列号";
                                return false;
                            }
                            CartonCode = (sto_sn as StorageDetailSN).CartonNo;
                        }

                        sto_obj = _Invenfacade.GetStorageDetail(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonCode)));



                        if (sto_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "库存中没有该箱号";
                            return false;
                        }
                        stor = sto_obj as StorageDetail;


                        pickline_obj = facade.GetPickLine(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickNo)), stor.DQMCode);
                        if (pickline_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "拣货任务令中没有鼎桥物料号：" + stor.DQMCode;
                            return false;
                        }


                        #region  更新表 pickdetail
                        PickDetail pikd = pickline_obj[0] as PickDetail;
                        if (ControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                        {
                            pikd.SQTY += 1;
                        }
                        else
                        {
                            if ((stor.StorageQty - stor.FreezeQty) < decimal.Parse(Number.Trim()))
                            {
                                message = "输入的数量不能大于可用数量！";
                                return false;

                            }
                            pikd.SQTY += decimal.Parse(Number.Trim());
                        }
                        pikd.MaintainDate = dbTime.DBDate;
                        pikd.MaintainTime = dbTime.DBTime;
                        pikd.MaintainUser = UserCode;
                        if (pikd.SQTY == pikd.QTY)
                        {
                            pikd.Status = PickDetail_STATUS.Status_ClosePick;
                        }
                        else
                        {
                            pikd.Status = PickDetail_STATUS.Status_Pick;
                        }
                        facade.UpdatePickdetail(pikd);
                        #endregion

                        #region  新增一笔数据到tblPickDetailMaterial

                        object find_obj = facade.GetPickdetailmaterial(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickNo)), CartonCode);
                        if (find_obj == null)
                        {

                            Pickdetailmaterial pikm = facade.CreateNewPickdetailmaterial();
                            pikm.Cartonno = stor.CartonNo;
                            pikm.CDate = dbTime.DBDate;
                            pikm.CTime = dbTime.DBTime;
                            pikm.CUser = UserCode;
                            pikm.CustmCode = pikd.CustMCode;
                            pikm.DqmCode = pikd.DQMCode;
                            pikm.LocationCode = stor.LocationCode;
                            pikm.Lotno = stor.Lotno;
                            pikm.MaintainDate = dbTime.DBDate;
                            pikm.MaintainTime = dbTime.DBTime;
                            pikm.MaintainUser = UserCode;
                            pikm.MCode = pikd.MCode;
                            pikm.Pickline = pikd.PickLine;
                            pikm.Pickno = pikd.PickNo;
                            pikm.Production_Date = stor.ProductionDate;
                            //pikm.QcStatus = string.Empty;
                            if (ControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                            {
                                pikm.Qty = 1;
                            }
                            else
                            {
                                pikm.Qty = decimal.Parse(Number);
                            }
                            //pikm.Status = string.Empty;   ////xu yao xiu gai 
                            pikm.StorageageDate = stor.LastStorageAgeDate;
                            pikm.Supplier_lotno = stor.SupplierLotNo;
                            pikm.Unit = stor.Unit;
                            pikm.PQty = 0;
                            facade.AddPickdetailmaterial(pikm);
                        }
                        else
                        {
                            Pickdetailmaterial pikm = find_obj as Pickdetailmaterial;
                            if (ControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                            {
                                pikm.Qty += 1;
                            }
                            else
                            {
                                pikm.Qty += decimal.Parse(Number);
                            }
                            pikm.MaintainDate = dbTime.DBDate;
                            pikm.MaintainTime = dbTime.DBTime;
                            pikm.MaintainUser = UserCode;
                            facade.UpdatePickdetailmaterial(pikm);
                        }


                        #endregion

                        #region  如果是单件管控，新增一笔数据到tblPickDetailMaterialSN
                        if (ControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                        {

                            object stosn = _Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN)));
                            if (stosn == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                message = "SN：" + SN + "不存在";
                                return false;
                            }
                            if ((stosn as StorageDetailSN).PickBlock == "Y")
                            {
                                this.DataProvider.RollbackTransaction();
                                message = "SN：" + SN + "已经被冻结";
                                return false;
                            }

                            Pickdetailmaterialsn piksn = facade.CreateNewPickdetailmaterialsn();
                            StorageDetailSN stsn = stosn as StorageDetailSN;
                            piksn.Cartonno = stsn.CartonNo;
                            piksn.MaintainDate = dbTime.DBDate;
                            piksn.MaintainTime = dbTime.DBTime;
                            piksn.MaintainUser = UserCode;
                            piksn.Pickline = pikd.PickLine;
                            piksn.Pickno = pikd.PickNo;
                            piksn.QcStatus = string.Empty;
                            piksn.Sn = stsn.SN;
                            facade.AddPickdetailmaterialsn(piksn);

                            #region   更新状态
                            stsn.PickBlock = "Y";
                            stsn.MaintainDate = dbTime.DBDate;
                            stsn.MaintainTime = dbTime.DBTime;
                            stsn.MaintainUser = UserCode;
                            _Invenfacade.UpdateStorageDetailSN(stsn);
                            #endregion

                        }
                        #endregion
                        #region  更新库存可用数量
                        if (ControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                        {
                            stor.FreezeQty += 1;
                            // stor.AvailableQty =stor.StorageQty-stor.FreezeQty;
                        }
                        else
                        {
                            stor.FreezeQty += int.Parse(Number.Trim());
                            // stor.AvailableQty = stor.StorageQty-stor.FreezeQty;
                        }
                        if (stor.AvailableQty < 0)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "库存里的可用数量不能为负";
                            return false;
                        }
                        stor.AvailableQty = stor.StorageQty - stor.FreezeQty;
                        stor.MaintainDate = dbTime.DBDate;
                        stor.MaintainTime = dbTime.DBTime;
                        stor.MaintainUser = UserCode;
                        _Invenfacade.UpdateStorageDetail(stor);
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "捡料出错" + ex.Message;
                        return false;
                    }
                }
                #region 创建发货箱单  --更新拣货任务令头
                CARTONINVOICES CartonH = facade.CreateNewCartoninvoices();
                object[] pikdetail_obj = facade.GetAllLineByPickNo(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickNo)));
                if (pikdetail_obj == null)
                {
                    object obj_pik = _Invenfacade.GetPick(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickNo)));
                    if (obj_pik == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "拣货任务令错误";
                        return false;
                    }
                    else
                    {

                        InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = string.Empty;
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = (obj_pik as Pick).InvNo;//.InvNo;
                        trans.InvType = (obj_pik as Pick).PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = dbTime.DBDate;
                        trans.MaintainTime = dbTime.DBTime;
                        trans.MaintainUser = UserCode;
                        trans.MCode = string.Empty;
                        trans.ProductionDate = 0;
                        trans.Qty = 0;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = (obj_pik as Pick).StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = (obj_pik as Pick).PickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "ClosePick";
                        facade.AddInvInOutTrans(trans);

                        Pick pi = obj_pik as Pick;
                        pi.Status = Pick_STATUS.Status_MakePackingList;
                        pi.MaintainDate = dbTime.DBDate;
                        pi.MaintainTime = dbTime.DBTime;
                        pi.MaintainUser = UserCode;

                        _Invenfacade.UpdatePick(pi);
                    }
                    //
                    object objLot = null;
                    objLot = facade.GetNewLotNO("K", dbTime.DBDate.ToString().Substring(2, 6).ToString());
                    Serialbook serbook = facade.CreateNewSerialbook();
                    if (objLot == null)
                    {
                        CartonH.CARINVNO = "K" + dbTime.DBDate.ToString().Substring(2, 6).ToString() + "001";
                        CartonH.PICKNO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickNo));
                        CartonH.STATUS = CartonInvoices_STATUS.Status_Release;
                        CartonH.CDATE = dbTime.DBDate;
                        CartonH.CTIME = dbTime.DBTime;
                        CartonH.CUSER = UserCode;
                        CartonH.MDATE = dbTime.DBDate;
                        CartonH.MTIME = dbTime.DBTime;
                        CartonH.MUSER = UserCode;
                        facade.AddCartoninvoices(CartonH);

                        serbook.SNprefix = "K" + dbTime.DBDate.ToString().Substring(2, 6).ToString();
                        serbook.MAXSerial = "2";
                        serbook.MUser = UserCode;
                        serbook.MDate = dbTime.DBDate;
                        serbook.MTime = dbTime.DBTime;

                        facade.AddSerialbook(serbook);


                    }
                    else
                    {
                        string MAXNO = (objLot as Serialbook).MAXSerial;
                        string SNNO = (objLot as Serialbook).SNprefix;
                        CartonH.CARINVNO = SNNO + Convert.ToString(MAXNO).PadLeft(3, '0');
                        CartonH.PICKNO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickNo));
                        CartonH.STATUS = CartonInvoices_STATUS.Status_Release;
                        CartonH.CDATE = dbTime.DBDate;
                        CartonH.CTIME = dbTime.DBTime;
                        CartonH.CUSER = UserCode;
                        CartonH.MDATE = dbTime.DBDate;
                        CartonH.MTIME = dbTime.DBTime;
                        CartonH.MUSER = UserCode;
                        facade.AddCartoninvoices(CartonH);

                        //更新tblserialbook
                        serbook.SNprefix = SNNO;
                        serbook.MAXSerial = Convert.ToString((int.Parse(MAXNO) + 1));
                        serbook.MUser = UserCode;
                        serbook.MDate = dbTime.DBDate;
                        serbook.MTime = dbTime.DBTime;
                        facade.UpdateSerialbook(serbook);
                    }
                }
                #endregion
                this.DataProvider.CommitTransaction();
                //this.gridHelper.RequestData();
                message = "拣货成功！";
                return true;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();

                message = "捡料出错";
                return false;
            }


        }
        protected string CheckStorageCode(string PickNo, string CartonNo, bool IsAll, bool Check, string Number, string SN)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            //if (string.IsNullOrEmpty(CartonNo))
            //{
            //    CartonNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonNoEdite.Text));
            //}
            int result = facade.CheckStorageCode(CartonNo, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(PickNo)), Check, IsAll, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(Number)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN)));
            switch (result)
            {
                case 1:

                    return "库存中不存在该箱号";

                case 2:
                    //WebInfoPublish.Publish(this, "拣货任务令号：" + this.txtPickNoQuery.Text + "无拣货数据", this.languageComponent1);
                    return "拣货任务令号无拣货数据";

                case 3:
                    // WebInfoPublish.Publish(this, "箱号：" + this.txtCartonNoEdite.Text + "在库存中的库位与拣货任务令中的库位不同", this.languageComponent1);
                    return "箱号在库存中的库位与拣货任务令中的库位不同";
                case 4:
                    // WebInfoPublish.Publish(this, "箱号：" + this.txtCartonNoEdite.Text + "鼎桥物料编码不在拣货任务令中", this.languageComponent1);
                    return "箱号鼎桥物料编码不在拣货任务令中";
                case 5:
                    //WebInfoPublish.Publish(this, "箱号：" + this.txtCartonNoEdite.Text + "拣货数量超出所需数量", this.languageComponent1);
                    return "箱号拣货数量超出所需数量";
                case 6:

                    //WebInfoPublish.Publish(this, "箱号：" + this.txtCartonNoEdite.Text + "违反先进先出规则", this.languageComponent1);
                    return "箱号违反先进先出规则";
                case 7:
                    //WebInfoPublish.Publish(this, "箱号：" + this.txtCartonNoEdite.Text + "已经拣货啦", this.languageComponent1);
                    return "箱号已经拣货啦";
                case 8:
                    //WebInfoPublish.Publish(this, "数据采集不全", this.languageComponent1);
                    return "数据采集不全";
                case 9:
                    //WebInfoPublish.Publish(this, "库存中没有可用的SN", this.languageComponent1);
                    return "库存中没有可用的SN";
                case 10:
                    //WebInfoPublish.Publish(this, "批管控必须输入数量", this.languageComponent1);
                    return "批管控必须输入数量";
                case 11:
                    //WebInfoPublish.Publish(this, "物料表没有维护管控类型", this.languageComponent1);
                    return "物料表没有维护管控类型";
                case 12:
                    //WebInfoPublish.Publish(this, "没有可用的数量，此箱不能添加", this.languageComponent1);
                    return "没有可用的数量，此箱不能添加";
                default:
                    return "TRUE";

            }

        }
        [WebMethod(EnableSession = true)]  //申请欠料
        public string ApplyButton(string PickNo, string PickLine, string UserCode)
        {
            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            PickNo = PickNo.ToUpper();
            //判断箱号有没有跟其他入库单的行关联
            //
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);


            Pick pik = facade.GetPick(PickNo) as Pick;
            if (pik == null)
            {
                return "拣货任务令头不存在";
            }
            else
            {
                if (pik.GFFlag == "X")
                {
                    return "光伏类型不允许欠料发货";
                }
                if (pik.PickType == PickType.PickType_PRC)
                {
                    return "预留单不允许欠料发货";
                }
            }

            PickDetail PickDetail = facade.GetPickdetail(PickNo.ToUpper(), PickLine.ToUpper()) as PickDetail;
            if (PickDetail.QTY == PickDetail.SQTY)
            {
                return "已拣数量等于需求数量，不必申请欠料";
            }
            else
            {
                PickDetail.Status = PickDetail_STATUS.Status_Owe;
                PickDetail.MaintainDate = dbTime.DBDate;
                PickDetail.MaintainTime = dbTime.DBTime;
                PickDetail.MaintainUser = UserCode;

                facade.UpdatePickdetail(PickDetail);

                return "申请欠料成功！";
            }

        }
        [WebMethod(EnableSession = true)]  //先进先出
        public DataTable GetInOutRule(string DQMCode, string PickNo)
        {
            if (_Invenfacade == null)
            {
                _Invenfacade = new InventoryFacade(DataProvider);
            }
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            string StorageCode = (_Invenfacade.GetPick(PickNo.ToUpper()) as Pick).StorageCode;
            object[] objs = facade.QueryInOutInfoByDQMCode(StorageCode, DQMCode.ToUpper(), 1, 500);
            DataTable dt = new DataTable("ExampleDataTable");
            if (objs != null)
            {
                dt.Columns.Add("FirstStorageAgeDate", typeof(string));  //首次入库时间
                dt.Columns.Add("LocationCode", typeof(string));  //货位
                dt.Columns.Add("CartonNo", typeof(string));  //箱号
                dt.Columns.Add("QTY", typeof(string));   //数量
                dt.Columns.Add("LotNo", typeof(string));  //批次号


                for (int i = 0; i < objs.Length; i++)
                {
                    StorageDetail stoDetail = objs[i] as StorageDetail;
                    dt.Rows.Add(FormatHelper.ToDateString(stoDetail.StorageAgeDate), stoDetail.LocationCode, stoDetail.CartonNo, stoDetail.AvailableQty.ToString(), stoDetail.Lotno);
                }

            }
            return dt;
        }
        [WebMethod(EnableSession = true)]   //查看
        public DataTable PickedView(string PickNo, string DQMCode)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            PickNo = PickNo.ToUpper();
            DQMCode = DQMCode.ToUpper();
            DataTable dt1 = facade.PickedView(PickNo, DQMCode);
            DataTable dt = new DataTable("ExampleDataTable");
            if (dt1 != null)
            {
                dt.Columns.Add("CartonNo", typeof(string));  //箱号
                dt.Columns.Add("SN", typeof(string));  //货位
                dt.Columns.Add("USER", typeof(string));   //捡料人
                dt.Columns.Add("DATE", typeof(string));   //日期
                dt.Columns.Add("LotNo", typeof(string));  //批次号
                dt.Columns.Add("LocationCode", typeof(string));  //货位
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    dt.Rows.Add(dt1.Rows[i]["CartonNo"].ToString(), dt1.Rows[i]["SN"].ToString(), dt1.Rows[i]["USERNAME"].ToString(), FormatHelper.ToDateString(int.Parse(dt1.Rows[i]["MDATE"].ToString())),
                         dt1.Rows[i]["LotNo"].ToString(), dt1.Rows[i]["LocationCode"].ToString());
                }

            }
            return dt;
        }
        [WebMethod(EnableSession = true)]  //查拣货的拣货任务令
        public string[] QueryPickNo(string user)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(user);

            string[] str = facade.QueryPickNO(usergroupList);
            //DataTable dt = new DataTable("ExampleDataTable");
            //if (dt1 != null)
            //{
            //    dt.Columns.Add("PickNo", typeof(string));
            //    for (int i = 0; i < dt1.Rows.Count; i++)
            //    {

            //        dt.Rows.Add(dt1.Rows[i][0].ToString());
            //    }

            //}
            return str;
        }
        [WebMethod(EnableSession = true)]  //查是否是批管控
        public string GetKeyPartsInfo(string CartonNo)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            string ControlType = facade.GetKeyPartsInfo(CartonNo);
            if (string.IsNullOrEmpty(ControlType))
            {
                return "该箱号无法识别管控类型";
            }
            else
            {
                if (ControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                    return "TRUE";
                else
                    return "FALSE";
            }
        }
        [WebMethod(EnableSession = true)]
        public string CheckInOutRule(string PickNo, string CartonNo, string Number, string SN, bool IsAll, bool Check)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            if (_Invenfacade == null)
            {
                _Invenfacade = new InventoryFacade(DataProvider);
            }
            PickNo = PickNo.ToUpper();
            CartonNo = CartonNo.ToUpper();
            SN = SN.ToUpper();
            if (IsAll)   //  选择整箱
            {
                if (string.IsNullOrEmpty(CartonNo))
                {
                    return "整箱操作箱号不能为空";
                }
                string result = CheckStorageCode(PickNo, CartonNo, IsAll, Check, Number, SN);
                if (result != "TRUE")
                {
                    return result;
                }
            }
            else //选择拆箱
            {

                #region 检查逻辑
                if (string.IsNullOrEmpty(SN) && string.IsNullOrEmpty(CartonNo))
                {
                    return "需要填箱号或SN信息";
                }
                #region 判断是单件管控还是批管控
                string ControlType = string.Empty;
                string CartonCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo));
                if (!string.IsNullOrEmpty(CartonNo))  //有箱号
                {

                    object obj = _Invenfacade.GetStorageDetail(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo)));
                    if (obj == null)
                    {

                        return "库存中没有该箱号信息";
                    }
                    else
                    {
                        StorageDetail sto = obj as StorageDetail;
                        MOModel.ItemFacade itemFacade = new MOModel.ItemFacade(DataProvider);
                        object material_obj = itemFacade.GetMaterial(sto.MCode);
                        if (material_obj == null)
                        {
                            return "改箱号对应的物料信息在物料表中不存在";
                        }
                        else
                        {
                            Domain.MOModel.Material matr = material_obj as Domain.MOModel.Material;
                            ControlType = matr.MCONTROLTYPE;
                            if (matr.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                            {
                                if (string.IsNullOrEmpty(SN))
                                {
                                    return "单件管控料必须输入SN进行捡料";
                                }
                                else
                                {
                                    object obj1 = _Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN)));
                                    if (obj1 == null)
                                    {
                                        return "库存中没有SN数据";
                                    }
                                    else
                                    {
                                        object obj2 = _Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN)));
                                        if (obj2 == null)
                                        {
                                            return "箱号或SN出错：没有SN信息";
                                        }
                                        else
                                        {
                                            string car = (_Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN))) as StorageDetailSN).CartonNo;
                                            if (CartonCode != car)
                                            {
                                                return "输入的箱号与SN号不匹配，单件管控料可以只输入SN";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else  // 没有输入箱号
                {
                    if (!string.IsNullOrEmpty(SN))
                    {
                        object obj = _Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN)));
                        if (obj == null)
                        {
                            return "库存中没有SN数据";
                        }
                        else
                        {
                            object obj2 = _Invenfacade.GetStorageDetailSN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(SN)));
                            if (obj2 == null)
                            {
                                return "无SN信息";
                            }
                            else
                            {
                                CartonCode = (obj2 as StorageDetailSN).CartonNo;

                                object obj3 = _Invenfacade.GetStorageDetail(CartonCode);
                                if (obj3 == null)
                                {
                                    return "库存中没有该箱号信息";
                                }
                                else
                                {
                                    StorageDetail sto = obj3 as StorageDetail;
                                    MOModel.ItemFacade itemFacade = new MOModel.ItemFacade(DataProvider);
                                    object material_obj = itemFacade.GetMaterial(sto.MCode);
                                    if (material_obj == null)
                                    {
                                        return "改箱号对应的物料信息在物料表中不存在";
                                    }
                                    else
                                    {
                                        Domain.MOModel.Material matr = material_obj as Domain.MOModel.Material;
                                        ControlType = matr.MCONTROLTYPE;
                                        if (matr.MCONTROLTYPE != SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                                        {
                                            return "SN料号不是单件管控料，请重新输入";
                                        }
                                    }
                                }
                            }
                        }

                    }

                }
                #endregion
                string result = CheckStorageCode(PickNo, CartonNo, IsAll, Check, Number, SN);
                if (result != "TRUE")
                {
                    return result;
                }
                #endregion
            }
            return "OK";
        }

        //ClosePickButton
        [WebMethod(EnableSession = true)]//拣料完成
        public string ClosePickButton(string pickno, string UserCode)
        {
            facade = new WarehouseFacade(DataProvider);
            Pick pick1 = (Pick)facade.GetPick(pickno);
            if (pick1 == null)
                return "拣货任务令不存在!";
            if (pick1.Status != Pick_STATUS.Status_Pick)
                return "拣货任务令状态必须是拣料中！";
            object[] pikdetailObj = facade.GetPickLineByPickNoNotCancel(pickno);
            if (pikdetailObj == null)
            { return "拣货任务令明细不存在"; }
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (PickDetail pickDetail in pikdetailObj)
                {
                    if (pickDetail.QTY == pickDetail.SQTY + pickDetail.OweQTY)
                    {
                        pickDetail.Status = PickDetail_STATUS.Status_ClosePick;
                        pickDetail.MaintainUser = UserCode;
                        facade.UpdatePickdetail(pickDetail);
                    }
                    else
                    {

                        int total = facade.QueryPickMaterialTotal(pickDetail.PickNo, pickDetail.PickLine, pickDetail.DQMCode);

                        if (total + pickDetail.OweQTY == pickDetail.QTY)
                        {
                            pickDetail.Status = PickDetail_STATUS.Status_ClosePick;
                            pickDetail.SQTY = pickDetail.QTY;
                            facade.UpdatePickdetail(pickDetail);

                        }
                        else
                        {
                            this.DataProvider.RollbackTransaction();
                            return "拣料数量不够";

                        }

                    }
                }
                Pick pick = facade.GetPick(pickno) as Pick;
                if (pick != null)
                {

                    InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                    trans.CartonNO = string.Empty;
                    trans.DqMCode = string.Empty;
                    trans.FacCode = string.Empty;
                    trans.FromFacCode = string.Empty;
                    trans.FromStorageCode = string.Empty;
                    trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                    trans.InvType = (pick as Pick).PickType;
                    trans.LotNo = string.Empty;
                    trans.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    trans.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    trans.MaintainUser = UserCode;
                    trans.MCode = string.Empty;
                    trans.ProductionDate = 0;
                    trans.Qty = 0;
                    trans.Serial = 0;
                    trans.StorageAgeDate = 0;
                    trans.StorageCode = (pick as Pick).StorageCode;
                    trans.SupplierLotNo = string.Empty;
                    trans.TransNO = (pick as Pick).PickNo;
                    trans.TransType = "OUT";
                    trans.Unit = string.Empty;
                    trans.ProcessType = "ClosePick";
                    facade.AddInvInOutTrans(trans);

                    pick.Status = Pick_STATUS.Status_MakePackingList;
                    pick.MaintainUser = UserCode;
                    facade.UpdatePick(pick);
                }
                this.DataProvider.CommitTransaction();
                return "拣料完成";
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return ex.Message;
            }
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetInvoicesDetails(string pickNo, string dqmcode)
        {
            facade = new WarehouseFacade(DataProvider);
            Pick pick = (Pick)facade.GetPick(pickNo);
            InvoicesDetail[] invs = null;
            if (pick.PickType == PickType.PickType_DNC || pick.PickType == PickType.PickType_DNZC)
                invs = facade.QueryInvoicesDetailsForDN(pickNo, dqmcode);

            else
                invs = facade.QueryInvoicesDetailsForNotDN(pickNo, dqmcode);
            DataTable dt = new DataTable("ExampleDataTable");
            dt.Columns.Add("DQMCODE", typeof(string));  //首次入库时间
            dt.Columns.Add("PLANQTY", typeof(string));  //货位
            dt.Columns.Add("MENSHORTDESC", typeof(string));  //箱号

            if (invs != null && invs.Length > 0)
            {



                for (int i = 0; i < invs.Length; i++)
                {

                    dt.Rows.Add(invs[i].DQMCode, invs[i].PlanQty, invs[i].MenshortDesc);
                }

            }

            return dt;

        }
    }
}
