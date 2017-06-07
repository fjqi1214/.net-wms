using System;
using System.Collections;
using System.Configuration;
using System.Data;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.SAPRFCService;
using BenQGuru.eMES.SAPRFCService.Domain;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FCreatePickCommand : BaseMPageNew
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        BenQGuru.eMES.BaseSetting.SystemSettingFacade _SystemSettingFacade = null;
        private BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = null;
        private BenQGuru.eMES.Material.InventoryFacade inventoryFacade = null;
        private BenQGuru.eMES.Material.InventoryFacade facade = null;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private Dictionary<string, string> dicStu = new Dictionary<string, string>();

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion



        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                txtPickNoQuery.Text = Request.QueryString["PickNo"];
                this.InitStorageList();
                //lblMOSelectMOViewField.Visible = false;
                #region dt
                dicStu.Add("", "");
                dicStu.Add("Release", "初始化");
                dicStu.Add("WaitPick", "待拣料");
                dicStu.Add("Pick", "拣料");
                dicStu.Add("MakePackingList", "制作箱单");
                dicStu.Add("Pack", "包装");
                dicStu.Add("OQC", "OQC检验");
                dicStu.Add("ClosePackingList", "箱单完成");
                dicStu.Add("Close", "已出库");
                dicStu.Add("Cancel", "取消");
                dicStu.Add("Block", "冻结");
                #endregion
                DrpPickTypeQueryLoad();
            }
            #region 注释
            //if (_SystemSettingFacade == null)
            //{
            //    _SystemSettingFacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(base.DataProvider);
            //}
            //if (_WarehouseFacade == null)
            //{
            //    _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            //}
            //BenQGuru.eMES.Domain.Warehouse.Storage[] storages = _WarehouseFacade.GetStorageCode();
            //this.drpStorageCodeQuery.Items.Add(new ListItem("", ""));
            //this.drpStorageCodeEdit.Items.Add(new ListItem("", ""));
            //foreach (BenQGuru.eMES.Domain.Warehouse.Storage p in storages)
            //{

            //    this.drpStorageCodeQuery.Items.Add(new ListItem(p.StorageName, p.StorageCode));
            //    if (!dicStu.ContainsKey(p.StorageCode))
            //        dicStu.Add(p.StorageCode, p.StorageName);
            //}


            //foreach (BenQGuru.eMES.Domain.Warehouse.Storage p in storages)
            //{

            //    this.drpStorageCodeEdit.Items.Add(new ListItem(p.StorageName, p.StorageCode));
            //}
            #endregion
            #region 注释
            //object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("PICKTYPE");
            //this.drpPickTypeQuery.Items.Add(new ListItem("", ""));
            //this.drpPickTypeEdit.Items.Add(new ListItem("", ""));
            //if (parameters != null)
            //{
            //    foreach (Domain.BaseSetting.Parameter parameter in parameters)
            //    {

            //        this.drpPickTypeQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
            //        if (!dicStu.ContainsKey(parameter.ParameterAlias))
            //            dicStu.Add(parameter.ParameterAlias, parameter.ParameterDescription);
            //    }

            //    foreach (Domain.BaseSetting.Parameter parameter in parameters)
            //    {

            //        this.drpPickTypeEdit.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
            //    }
            //}
            #endregion
        }

        private void DrpPickTypeQueryLoad()
        {
            #region  生产领料PickType_WWPOC

            drpPickTypeQuery.Items.Clear();
            drpPickTypeEdit.Items.Clear();
            this.drpPickTypeQuery.Items.Add(new ListItem("", ""));
            this.drpPickTypeQuery.Items.Add(new ListItem(GetPickTypeName(PickType.PickType_WWPOC), PickType.PickType_WWPOC));
            this.drpPickTypeQuery.SelectedIndex = 0;


            this.drpPickTypeEdit.Items.Add(new ListItem("", ""));
            this.drpPickTypeEdit.Items.Add(new ListItem(GetPickTypeName(PickType.PickType_WWPOC), PickType.PickType_WWPOC));
            this.drpPickTypeEdit.SelectedIndex = 0;
            #endregion
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion
        #region 下拉框
        private void InitStorageList()
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser(GetUserCode());//+TOSTORAGE
            this.drpStorageCodeQuery.Items.Add(new ListItem("", ""));
            this.drpStorageCodeEdit.Items.Add(new ListItem("", ""));
            object[] parameters = systemSettingFacade.GetDistinctParaInParameterGroup(usergroupList);
            if (parameters != null)
            {
                foreach (Domain.BaseSetting.Parameter parameter in parameters)
                {
                    drpStorageCodeQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterCode));
                    drpStorageCodeEdit.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterCode));
                    if (!dicStu.ContainsKey(parameter.ParameterCode))
                        dicStu.Add(parameter.ParameterCode, parameter.ParameterDescription);
                }
            }
            this.drpStorageCodeQuery.SelectedIndex = 0;
            this.drpStorageCodeEdit.SelectedIndex = 0;

        }
        #endregion

        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            object obj = facade.GetPick(row.Items.FindItemByKey("PICKNO").Text);
            if (obj != null)
            {
                return obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                txtPickNoEdit.Text = "";
                drpPickTypeEdit.SelectedValue = "";
                txtInvNoEdit.Text = "";
                drpStorageCodeEdit.SelectedValue = "";
                txtReceiverUserEdit.Text = "";
                txtReceiverAddrEdit.Text = "";
                txtPlanDateEdit.Text = "";
                txtREMARKEdit.Text = "";
                return;
            }
            BenQGuru.eMES.Domain.Warehouse.Pick pick = (BenQGuru.eMES.Domain.Warehouse.Pick)obj;
            txtPickNoEdit.Text = pick.PickNo;
            drpPickTypeEdit.SelectedValue = pick.PickType;
            txtInvNoEdit.Text = pick.InvNo;
            drpStorageCodeEdit.SelectedValue = pick.StorageCode;
            txtReceiverUserEdit.Text = pick.ReceiverUser;
            txtReceiverAddrEdit.Text = pick.Receiveraddr;
            txtPlanDateEdit.Text = FormatHelper.ToDateString(pick.PlanDate, "-");
            txtREMARKEdit.Text = pick.Remark1;

        }

        protected override object GetEditObject()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            BenQGuru.eMES.Domain.Warehouse.Pick pick = (BenQGuru.eMES.Domain.Warehouse.Pick)_WarehouseFacade.GetPick(txtPickNoEdit.Text);


            pick.PickNo = txtPickNoEdit.Text;
            pick.PickType = drpPickTypeEdit.SelectedValue;
            pick.InvNo = txtInvNoEdit.Text;
            pick.StorageCode = drpStorageCodeEdit.SelectedValue;
            pick.ReceiverUser = txtReceiverUserEdit.Text;
            pick.Receiveraddr = txtReceiverAddrEdit.Text;
            pick.PlanDate = FormatHelper.TODateInt(txtPlanDateEdit.Text);
            pick.Remark1 = txtREMARKEdit.Text;
            pick.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
            pick.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            pick.MaintainUser = this.GetUserCode();

            //pick.PickNo = txtPickNoEdit.Text;
            //pick.PickType = drpPickTypeEdit.SelectedValue;
            //pick.InvNo = txtInvNoEdit.Text;
            //pick.StorageCode = drpStorageCodeEdit.SelectedValue;
            //pick.ReceiverUser = txtReceiverUserEdit.Text;
            //pick.Receiveraddr = txtReceiverAddrEdit.Text;
            //pick.PlanDate = FormatHelper.TODateInt(txtPlanDateEdit.Text);
            //pick.Remark1 = txtREMARKEdit.Text;
            return pick;
        }

        #region 保存



        protected void cmdDelete_ServerClick(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;
            List<BenQGuru.eMES.Domain.Warehouse.Pick> ll = new List<BenQGuru.eMES.Domain.Warehouse.Pick>();
            facade = new InventoryFacade(this.DataProvider);

            foreach (GridRecord row in array)
            {
                BenQGuru.eMES.Domain.Warehouse.Pick obj = (BenQGuru.eMES.Domain.Warehouse.Pick)_WarehouseFacade.GetPick(row.Items.FindItemByKey("PICKNO").Text);

                if (obj.Status != PickHeadStatus.PickHeadStatus_Release)
                {
                    WebInfoPublish.Publish(this, obj.PickNo + "必须是初始化！", this.languageComponent1);
                    return;
                }
                if (facade.GetPickDetailCount(obj.PickNo) > 0)
                {
                    WebInfoPublish.Publish(this, obj.PickNo + "存在拣料行信息，不能删除！", this.languageComponent1);
                    return;
                }
                if (facade.GetPickDetailMaterialCount(obj.PickNo) > 0)
                {
                    WebInfoPublish.Publish(this, obj.PickNo + "存在已拣料信息，不能删除！", this.languageComponent1);
                    return;
                }
                ll.Add(obj);
            }


            try
            {
                this.DataProvider.BeginTransaction();

                foreach (BenQGuru.eMES.Domain.Warehouse.Pick p in ll)
                {
                    _WarehouseFacade.DeletePick(p);
                    _WarehouseFacade.DeleteDeteils(p.PickNo);
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }

        }

        protected void cmdRelease_ServerClick(object sender, EventArgs e)
        {
            GetServerClick("cmdRelease_ServerClick");
        }

        private void GetServerClick(string clickName)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            object obj = null;

            if (array.Count > 0)
            {
                ArrayList objList = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    obj = this.GetEditObject(row);

                    if (obj != null)
                    {
                        objList.Add(obj);
                    }
                }
                object[] asnList = ((Pick[])objList.ToArray(typeof(Pick)));
                if (clickName == "cmdRelease_ServerClick")
                {
                    this.CmdReleaseObjects(asnList);
                }

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }


        protected void CmdReleaseObjects(object[] pickList)
        {

            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (Pick pick in pickList)
                {
                    if (string.IsNullOrEmpty(pick.StorageCode))
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, pick.PickNo + "拣货任务令出库库位不能空", this.languageComponent1);
                        return;
                    }
                    PickDetail[] details = _WarehouseFacade.GetPickdetails(pick.PickNo);
                    foreach (PickDetail d in details)
                    {
                        if (d.QTY == 0)
                        {
                            WebInfoPublish.Publish(this, pick.PickNo + "拣货任务令行" + d.PickLine + "数量不能为0！", this.languageComponent1);
                            return;
                        }
                    }
                    bool isUpdate = facade.QueryPickDetailCount(pick.PickNo, PickDetail_STATUS.Status_Cancel) > 0;
                    if (!isUpdate)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "需求行为空时不可下发", this.languageComponent1);
                        return;
                    }
                    if (pick.Status == PickHeadStatus.PickHeadStatus_Release)
                    {
                        if (pick.PickType == PickType.PickType_DNZC)
                        {
                            #region DNZC
                            PickDetail[] pickmcodeList = facade.GetPickDetailMCodeByPickNo1(pick.PickNo);
                            string stno = GetStNo(pick.StNo);
                            ASNDetail[] asnmCodeList = facade.GetAsnDetailMCodeByStNo1(stno);

                            #region 需求物料和数量不一致

                            if (pickmcodeList.Length != asnmCodeList.Length)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "对应 ASN号和拣货任务令的需求物料和数量不一致", this.languageComponent1);
                                return;
                            }
                            foreach (PickDetail mCodeObj in pickmcodeList)
                            {
                                int asnqty = facade.GetAsnDetailSumQtyByMCode(stno, mCodeObj.MCode);//DQMCode
                                int pickqty = facade.GetPickDetailSumQtyByMCode(pick.PickNo, mCodeObj.MCode);//DQMCode
                                if (pickqty != asnqty)
                                {
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "对应 ASN号和拣货任务令的需求物料和数量是不一致", this.languageComponent1);
                                    return;
                                }
                            }
                            #endregion


                            object[] cartonNoList = facade.QueryAsnDetailCartonNo(stno);

                            Dictionary<string, List<CartonInvDetailMaterial>> cartonsGroupByDQMCode = new Dictionary<string, List<CartonInvDetailMaterial>>();
                            if (cartonNoList == null || cartonNoList.Length <= 0)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, pick.StNo + "对应的入库指令明细不存在！", this.languageComponent1);
                                return;
                            }


                            #region 注释
                            //CARTONINVOICES CartonH = new CARTONINVOICES();
                            //string date = dbTime.DBDate.ToString().Substring(2, 6);
                            //string serialNo = CreateSerialNo(date);
                            //string carinvno = "K" + date + serialNo;
                            //CartonH.CARINVNO = carinvno;
                            //CartonH.PICKNO = pick.PickNo;
                            //CartonH.STATUS = CartonInvoices_STATUS.Status_ClosePackingList;

                            //CartonH.FDATE = dbTime.DBDate;
                            //CartonH.FTIME = dbTime.DBTime;
                            //CartonH.CDATE = dbTime.DBDate;
                            //CartonH.CTIME = dbTime.DBTime;
                            //CartonH.CUSER = this.GetUserCode();
                            //CartonH.MDATE = dbTime.DBDate;
                            //CartonH.MTIME = dbTime.DBTime;
                            //CartonH.MUSER = this.GetUserCode();
                            //_WarehouseFacade.AddCartoninvoices(CartonH); 
                            #endregion

                            #region add by sam
                            object objLot = null;
                            CARTONINVOICES CartonH = _WarehouseFacade.CreateNewCartoninvoices();
                            objLot = _WarehouseFacade.GetNewLotNO("K", dbTime.DBDate.ToString().Substring(2, 6).ToString());
                            Serialbook serbook = _WarehouseFacade.CreateNewSerialbook();
                            if (objLot == null)
                            {
                                CartonH.CARINVNO = "K" + dbTime.DBDate.ToString().Substring(2, 6).ToString() + "001";
                                CartonH.PICKNO = pick.PickNo;
                                CartonH.STATUS = CartonInvoices_STATUS.Status_ClosePackingList;
                                CartonH.CDATE = dbTime.DBDate;
                                CartonH.CTIME = dbTime.DBTime;
                                CartonH.CUSER = this.GetUserCode();
                                CartonH.MDATE = dbTime.DBDate;
                                CartonH.MTIME = dbTime.DBTime;
                                CartonH.MUSER = this.GetUserCode();
                                _WarehouseFacade.AddCartoninvoices(CartonH);

                                serbook.SNprefix = "K" + dbTime.DBDate.ToString().Substring(2, 6).ToString();
                                serbook.MAXSerial = "2";
                                serbook.MUser = this.GetUserCode();
                                serbook.MDate = dbTime.DBDate;
                                serbook.MTime = dbTime.DBTime;

                                _WarehouseFacade.AddSerialbook(serbook);


                            }
                            else
                            {
                                string MAXNO = (objLot as Serialbook).MAXSerial;
                                string SNNO = (objLot as Serialbook).SNprefix;
                                CartonH.CARINVNO = SNNO + Convert.ToString(MAXNO).PadLeft(3, '0');
                                CartonH.PICKNO = pick.PickNo;
                                CartonH.STATUS = CartonInvoices_STATUS.Status_ClosePackingList;
                                CartonH.CDATE = dbTime.DBDate;
                                CartonH.CTIME = dbTime.DBTime;
                                CartonH.CUSER = this.GetUserCode();
                                CartonH.MDATE = dbTime.DBDate;
                                CartonH.MTIME = dbTime.DBTime;
                                CartonH.MUSER = this.GetUserCode();
                                _WarehouseFacade.AddCartoninvoices(CartonH);

                                //更新tblserialbook
                                serbook.SNprefix = SNNO;
                                serbook.MAXSerial = Convert.ToString((int.Parse(MAXNO) + 1));
                                serbook.MUser = this.GetUserCode();
                                serbook.MDate = dbTime.DBDate;
                                serbook.MTime = dbTime.DBTime;
                                _WarehouseFacade.UpdateSerialbook(serbook);
                            }
                            #endregion




                            foreach (Asndetail asnDetail in cartonNoList)
                            {
                                //如果一致，则将ASN中的物料SN条码 匹配到拣货任务令的已拣物料中。自动产生发货箱单，
                                #region 自动产生发货箱单，

                                BenQGuru.eMES.Domain.Warehouse.CartonInvDetail c2 = new BenQGuru.eMES.Domain.Warehouse.CartonInvDetail();
                                string caseNo = cmdCreateBarCode();
                                c2.CARINVNO = CartonH.CARINVNO;
                                c2.STATUS = "ClosePack";
                                c2.PICKNO = CartonH.PICKNO;
                                c2.CARTONNO = asnDetail.Cartonno;
                                c2.CUSER = GetUserCode();
                                c2.CDATE = FormatHelper.TODateInt(DateTime.Now);
                                c2.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                                c2.MDATE = FormatHelper.TODateInt(DateTime.Now);
                                c2.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                                c2.MUSER = GetUserCode();
                                _WarehouseFacade.AddCartonInvDetail(c2);

                                Pickdetailmaterial pikm = new Pickdetailmaterial();
                                pikm.Cartonno = asnDetail.Cartonno;
                                pikm.CDate = dbTime.DBDate;
                                pikm.CTime = dbTime.DBTime;
                                pikm.CUser = this.GetUserCode();
                                pikm.CustmCode = asnDetail.CustmCode;
                                pikm.DqmCode = asnDetail.DqmCode;
                                pikm.LocationCode = "";// stor.LocationCode;
                                pikm.Lotno = asnDetail.Lotno;
                                pikm.MaintainDate = dbTime.DBDate;
                                pikm.MaintainTime = dbTime.DBTime;
                                pikm.MaintainUser = this.GetUserCode();
                                pikm.MCode = asnDetail.MCode;
                                pikm.Pickline = asnDetail.Stline;
                                pikm.Pickno = pick.PickNo;
                                pikm.Production_Date = asnDetail.Production_Date;
                                StorageDetail sd = (StorageDetail)_WarehouseFacade.GetStorageDetail(asnDetail.Cartonno);
                                pikm.PQty = asnDetail.Qty;
                                pikm.LocationCode = sd.LocationCode;
                                //pikm.QcStatus = string.Empty;
                                pikm.Qty = asnDetail.Qty;
                                //pikm.Status = string.Empty;   ////xu yao xiu gai 
                                pikm.StorageageDate = asnDetail.StorageageDate;
                                pikm.Supplier_lotno = asnDetail.Supplier_lotno;
                                pikm.Unit = asnDetail.Unit;
                                _WarehouseFacade.AddPickdetailmaterial(pikm);

                                CartonInvDetailMaterial cm = new CartonInvDetailMaterial();
                                cm.CARINVNO = CartonH.CARINVNO;
                                cm.CARTONNO = asnDetail.Cartonno;
                                cm.CDATE = FormatHelper.TODateInt(DateTime.Now);
                                cm.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                                cm.CUSER = GetUserCode();
                                cm.DQMCODE = asnDetail.DqmCode;
                                cm.MCODE = asnDetail.MCode;
                                cm.MDATE = FormatHelper.TODateInt(DateTime.Now);
                                cm.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                                cm.MUSER = GetUserCode();
                                cm.PICKLINE = asnDetail.Stline;
                                cm.PICKNO = pick.PickNo;
                                cm.QTY = asnDetail.Qty;
                                cm.UNIT = asnDetail.Unit;
                                if (cartonsGroupByDQMCode.ContainsKey(cm.DQMCODE))
                                    cartonsGroupByDQMCode[cm.DQMCODE].Add(cm);
                                else
                                {
                                    cartonsGroupByDQMCode[cm.DQMCODE] = new List<CartonInvDetailMaterial>();
                                    cartonsGroupByDQMCode[cm.DQMCODE].Add(cm);
                                }

                                _WarehouseFacade.AddCartonInvDetailMaterial(cm);

                                Asndetailsn[] sns = _WarehouseFacade.GetAsnDetailSNs(asnDetail.Stno, asnDetail.Stline);

                                foreach (Asndetailsn snn in sns)
                                {
                                    CARTONINVDETAILSN sn = new CARTONINVDETAILSN();
                                    sn.CARINVNO = CartonH.CARINVNO;
                                    sn.CARTONNO = cm.CARTONNO;
                                    sn.MDATE = FormatHelper.TODateInt(DateTime.Now);
                                    sn.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                                    sn.MUSER = GetUserCode();
                                    sn.PICKNO = pick.PickNo;
                                    sn.PICKLINE = snn.Stline;
                                    sn.SN = snn.Sn;
                                    _WarehouseFacade.AddCARTONINVDETAILSN(sn);

                                    PickDetailMaterialSn pickmsn = new PickDetailMaterialSn();
                                    pickmsn.CartonNo = asnDetail.Cartonno;
                                    pickmsn.PickNo = pick.PickNo;
                                    pickmsn.PickLine = asnDetail.Stline;
                                    pickmsn.Sn = snn.Sn;

                                    // pickmsn.QcStatus = asnObj.QcStatus;
                                    pickmsn.MaintainUser = this.GetUserCode();
                                    pickmsn.MaintainDate = dbTime.DBDate;
                                    pickmsn.MaintainTime = dbTime.DBTime;
                                    facade.AddPickDetailMaterialSn(pickmsn);
                                }
                            }
                            foreach (string DQMCode in cartonsGroupByDQMCode.Keys)
                            {
                                PickDetail pd = _WarehouseFacade.GetPickDetail(pick.PickNo, DQMCode);
                                int PQTY = 0;
                                foreach (CartonInvDetailMaterial c in cartonsGroupByDQMCode[DQMCode])
                                {
                                    PQTY += (int)c.QTY;
                                }

                                pd.PQTY = PQTY;
                                pd.Status = "ClosePack";
                                _WarehouseFacade.UpdatePickDetailForDNCZ(pd);

                            }

                                #endregion
                            //拣货任务令头下发后为包装状态，拣货任务令行为包装完成状态
                            pick.Status = "ClosePackingList";
                            pick.SerialNumber = Convert.ToInt64(dbTime.DBDate.ToString() + dbTime.DBTime.ToString().PadLeft(6, '0'));

                            _WarehouseFacade.UpdatePickForDNCZ(pick, GetUserCode());
                            #endregion
                        }
                        else
                        {
                            //拣货任务令头下发后为包装状态，拣货任务令行为包装完成状态
                            pick.SerialNumber = Convert.ToInt64(dbTime.DBDate.ToString() + dbTime.DBTime.ToString().PadLeft(6, '0'));

                            facade.UpdatePickStatusByPickNo(PickHeadStatus.PickHeadStatus_WaitPick, pick.PickNo, pick.SerialNumber);
                            facade.UpdatePickDetailStatusByPickNo(PickHeadStatus.PickHeadStatus_WaitPick, pick.PickNo, PickHeadStatus.PickHeadStatus_Release);
                        }

                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = string.Empty;
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = pick.InvNo;//.InvNo;
                        trans.InvType = pick.PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = dbTime.DBDate;
                        trans.MaintainTime = dbTime.DBTime;
                        trans.MaintainUser = this.GetUserCode();
                        trans.MCode = string.Empty;
                        trans.ProductionDate = 0;
                        trans.Qty = 0;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = pick.StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = pick.PickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "ISSUE";
                        _WarehouseFacade.AddInvInOutTrans(trans);

                    }
                    else
                    {
                        this.DataProvider.RollbackTransaction();

                        WebInfoPublish.Publish(this, "非初始化的拣货任务令不可下发", this.languageComponent1);
                        return;
                    }
                }

                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "下发成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                Log.Error(ex.StackTrace);
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }

        }

        private string CreateSerialNo(int stno)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            }
            string maxserial = _WarehouseFacade.GetMaxSerial("CT" + stno);

            //如果已是最大值就返回为空
            if (maxserial == "999999999")
            {
                return "";
            }

            SERIALBOOK serialbook = new SERIALBOOK();
            if (maxserial == "")
            {
                serialbook.SNPrefix = "CT" + stno;
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _WarehouseFacade.AddSerialBook(serialbook);
                return string.Format("{0:000000000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = "CT" + stno;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _WarehouseFacade.UpdateSerialBook(serialbook);
                return string.Format("{0:000000000}", int.Parse(serialbook.MaxSerial));
            }
        }

        protected string cmdCreateBarCode()
        {

            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);

            //点击生成条码按钮，根据数量输入框输入的数量生成箱号数，显示在Grid中并保存在TBLBARCODE表中
            //4.	鼎桥箱号编码规则：CT+年月日+九位流水码：如：CT20160131000000001，流水码不归零(流水码创建对应的Sequences累加)

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int date = dbDateTime.DBDate;
            //try
            //{
            //    this.DataProvider.BeginTransaction();

            BarCode bar = new BarCode();
            string serialNo = CreateSerialNo(date);
            bar.BarCodeNo = "CT" + date + serialNo;
            bar.Type = "CARTONNO";
            bar.MCode = "";
            bar.EnCode = "";
            bar.SpanYear = date.ToString().Substring(0, 4);
            bar.SpanDate = date;
            if (!string.IsNullOrEmpty(serialNo))
            {
                bar.SerialNo = int.Parse(serialNo);
            }
            bar.PrintTimes = 0;
            bar.CUser = this.GetUserCode();	//	CUSER
            bar.CDate = dbDateTime.DBDate;	//	CDATE
            bar.CTime = dbDateTime.DBTime;//	CTIME
            bar.MaintainDate = dbDateTime.DBDate;	//	MDATE
            bar.MaintainTime = dbDateTime.DBTime;	//	MTIME
            bar.MaintainUser = this.GetUserCode();		//	MUSER
            _WarehouseFacade.AddBarCode(bar);
            return "CT" + date + serialNo;
            //    this.DataProvider.CommitTransaction();
            //}
            //catch (Exception ex)
            //{
            //    this.DataProvider.RollbackTransaction();
            //    WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            //}
        }

        protected override void cmdAdd_Click(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            string pono = FormatHelper.CleanString(txtInvNoEdit.Text);
            bool isVender = _WarehouseFacade.IsHasVenderCode(pono, GetUserCode()) > 0;
            if (!isVender)
            {
                WebInfoPublish.Publish(this, "SAP单据号对应的供应商是否和当前用户所属用户组对应的供应商不吻合", this.languageComponent1);
                this.DataProvider.RollbackTransaction();
                return;
            }
            //SAP接口
            #region GetWWPOList add by sam
            if (!string.IsNullOrEmpty(pono))
            {
                BenQGuru.eMES.SAPRFCService.WWPO2SAP wwpoToSap = new WWPO2SAP(this.DataProvider);
                List<WWPOComponentPara> list = new List<WWPOComponentPara>();
                WWPOComponentPara wwpocom = new WWPOComponentPara();
                wwpocom.PONO = pono;
                //wwpo.POLine = 1;
                list.Add(wwpocom);
                try
                {
                    this.DataProvider.BeginTransaction();

                    #region add wwpo

                    WWPOComponentResult coList = wwpoToSap.GetWWPOList(list);
                    if (coList != null)
                    {
                        _WarehouseFacade.DeleteWWPOByPoNo(pono);
                        SAPRfcReturn re = coList.RfcResult;
                        foreach (WWPOComponent co in coList.WWPOComponentList)
                        {
                            MesWWPO wwpo = new MesWWPO();
                            wwpo.PONO = co.PONO;
                            wwpo.POLine = co.POLine;
                            wwpo.SubLine = co.SubLine;
                            wwpo.MCode = co.MCode;
                            wwpo.HWMCode = co.HWMCode;
                            wwpo.Qty = co.Qty;
                            wwpo.Unit = co.Unit;
                            wwpo.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            wwpo.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            wwpo.MaintainUser = this.GetUserCode();
                            if (re != null)
                            {
                                wwpo.SapResult = re.Result;
                                wwpo.Message = re.Message;
                            }
                            _WarehouseFacade.AddMesWWPO(wwpo);
                        }
                    }

                    #endregion

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
                    return;
                }
            }

            #endregion


            Pick pick = new Pick();
            pick.PickNo = txtPickNoEdit.Text;
            pick.PickType = drpPickTypeEdit.SelectedValue;
            pick.InvNo = txtInvNoEdit.Text;
            pick.StorageCode = drpStorageCodeEdit.SelectedValue;
            pick.ReceiverUser = txtReceiverUserEdit.Text;
            pick.Receiveraddr = txtReceiverAddrEdit.Text;
            pick.PlanDate = FormatHelper.TODateInt(txtPlanDateEdit.Text);
            pick.Remark1 = txtREMARKEdit.Text;
            pick.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
            pick.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            pick.MaintainUser = this.GetUserCode();
            pick.Status = "Release";
            pick.CDate = FormatHelper.TODateInt(DateTime.Now);
            pick.CTime = FormatHelper.TOTimeInt(DateTime.Now);
            pick.CUser = this.GetUserCode();
            _WarehouseFacade.AddPick(pick);
            WebInfoPublish.Publish(this, "添加成功！", this.languageComponent1);
            this.gridHelper.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }




        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();

            #region
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                this.gridHelper.AddColumn(this.PickHeadViewFieldList[i].FieldName, /*this.languageComponent1.GetString(*/this.PickHeadViewFieldList[i].Description/*)*/, null);
            }

            //多语言
            #endregion

            #region 注释
            //this.gridHelper.AddColumn("PICKNO", "拣货任务令号", null);
            //this.gridHelper.AddColumn("INVNO", "SAP单据号", null);
            //this.gridHelper.AddColumn("PICKTYPE", "单据类型", null);
            //this.gridHelper.AddColumn("STATUS", "状态", null);
            //this.gridHelper.AddColumn("StorageCode", "出库库位", null);
            //this.gridHelper.AddColumn("ReceiverUser", "收货人信息", null);

            //this.gridHelper.AddColumn("ReceiverAddr", "收货地址", null);
            //this.gridHelper.AddColumn("Plan_Date", "计划发货日期", null);
            //this.gridHelper.AddColumn("PLANGIDATE", "计划交货日期", null);
            //this.gridHelper.AddColumn("Down_Date", "下发日期", null);
            //this.gridHelper.AddColumn("Down_User", "下发人", null);
            //this.gridHelper.AddColumn("Finish_Date", "拣料完成日期", null);
            //this.gridHelper.AddColumn("Finish_Time", "拣料完成时间", null);
            //this.gridHelper.AddColumn("OQC_Date", "OQC完成日期", null);
            //this.gridHelper.AddColumn("OQC_Time", "OQC完成时间", null);
            //this.gridHelper.AddColumn("Packing_List_Date", "箱单完成日期", null);
            //this.gridHelper.AddColumn("Packing_List_Time", "箱单完成时间", null);
            //this.gridHelper.AddColumn("Delivery_Date", "出库日期", null);
            //this.gridHelper.AddColumn("Delivery_Time", "出库时间", null);
            //this.gridHelper.AddColumn("CDATE", "创建日期", null);
            //this.gridHelper.AddColumn("CTIME", "创建时间", null);
            //this.gridHelper.AddColumn("CUSER", "创建人", null);
            //this.gridHelper.AddColumn("Remark", "备注", null);
            //this.gridHelper.AddColumn("MDATE", "维护日期", null);
            //this.gridHelper.AddColumn("MTIME", "维护时间", null);

            //this.gridHelper.AddColumn("Down_Time", "下发时间", null);
            //this.gridHelper.AddColumn("REMARK1", "备注", null);
            //this.gridHelper.AddColumn("MUSER", "维护人", null); 
            #endregion

            this.gridHelper.AddLinkColumn("LinkToCartonImport", "详细信息", null);


            this.gridHelper.AddDefaultColumn(true, true);

            if (!string.IsNullOrEmpty(this.txtPickNoQuery.Text))
                this.gridHelper.RequestData();

            //多语言
            //this.gridHelper.ApplyLanguage(this.languageComponent1);
        }



        public string DownloadPhysicalPath
        {
            get
            {
                return string.Format(@"{0}\{1}\", Request.PhysicalApplicationPath, _downloadPath.Trim('\\', '/').Replace('/', '\\'));
            }
        }
        private string PageVirtualHostRoot
        {
            get
            {
                return string.Format("{0}{1}"
                    , this.Page.Request.Url.Segments[0]
                    , this.Page.Request.Url.Segments[1]);
            }
        }

        private string _downloadPath = @"\upload\";


        protected override DataRow GetGridRow(object obj)
        {
            BenQGuru.eMES.Domain.Warehouse.Pick pick = (BenQGuru.eMES.Domain.Warehouse.Pick)obj;
            DataRow row = this.DtSource.NewRow();
            row["PICKNO"] = pick.PickNo;
            row["INVNO"] = pick.InvNo;
            row["PICKTYPE"] = pick.PickType;// dicStu.ContainsKey(pick.PickType) ? dicStu[pick.PickType] : string.Empty;
            if (pick.Status == PickHeadStatus.PickHeadStatus_Close)
            {
                row["STATUS"] = "已出库";
            }
            else
            {
                row["STATUS"] = languageComponent1.GetString(pick.Status); //dicStu.ContainsKey(pick.Status) ? dicStu[pick.Status] : string.Empty;
            }
            row["StorageCode"] = pick.StorageCode;// dicStu.ContainsKey(pick.StorageCode) ? dicStu[pick.StorageCode] : string.Empty;
            row["ReceiverUser"] = pick.ReceiverUser;
            row["ReceiverAddr"] = pick.Receiveraddr;
            row["Plan_Date"] = FormatHelper.ToDateString(pick.PlanDate);
            row["PLANGIDATE"] = pick.PlanGIDate;
            row["Down_Date"] = FormatHelper.ToDateString(pick.DownDate);
            row["Down_User"] = pick.DownUser;
            row["Finish_Date"] = FormatHelper.ToDateString(pick.FinishDate);
            row["Finish_Time"] = FormatHelper.ToTimeString(pick.FinishTime);
            row["OQC_Date"] = FormatHelper.ToDateString(pick.OQCDate);
            row["OQC_Time"] = FormatHelper.ToTimeString(pick.OQCTime);
            row["Packing_List_Date"] = FormatHelper.ToDateString(pick.PackingListDate);
            row["Packing_List_Time"] = FormatHelper.ToTimeString(pick.PackingListTime);
            row["Delivery_Date"] = FormatHelper.ToDateString(pick.DeliveryDate);
            row["Delivery_Time"] = FormatHelper.ToTimeString(pick.DeliveryTime);
            row["CDATE"] = FormatHelper.ToDateString(pick.CDate);
            row["CTIME"] = FormatHelper.ToTimeString(pick.CTime);
            row["CUSER"] = pick.CUser;
            row["MDATE"] = FormatHelper.ToDateString(pick.MaintainDate);
            row["MTIME"] = FormatHelper.ToTimeString(pick.MaintainTime);
            row["Remark"] = pick.Remark1;

            //row["REMARK1"] = pick.Remark1;
            //row["Down_Time"] = FormatHelper.ToTimeString(pick.DownTime);
            //row["MUSER"] = pick.MaintainUser;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            List<string> status = new List<string>();
            if (this.rdoRelease1.Checked)
                status.Add("Release");
            if (this.rdoWaitPick.Checked)
                status.Add("WaitPick");
            if (this.rdoPick.Checked)
                status.Add("Pick");
            if (this.rdoMakePackingList.Checked)
                status.Add("MakePackingList");
            if (this.rdoPack.Checked)
                status.Add("Pack");
            if (this.rdoOQC.Checked)
                status.Add("OQC");
            if (this.rdoClosePackingList.Checked)
                status.Add("ClosePackingList");
            if (this.rdoClose1.Checked)
                status.Add("Close");
            if (this.rdoCancel1.Checked)
                status.Add("Cancel");
            if (this.rdoBlock.Checked)
                status.Add("Block");
            if (this.rdoPackingListing.Checked)
                status.Add("PackingListing");
            return _WarehouseFacade.GetPicks(txtPickNoQuery.Text,
                                                drpPickTypeQuery.SelectedValue,
                                                drpStorageCodeQuery.SelectedValue,
                                                FormatHelper.CleanString(txtInvNoQuery.Text),
                                                status,
                                                FormatHelper.TODateInt(txtCBDateQuery.Text),
                                                FormatHelper.TODateInt(txtCEDateQuery.Text),
                                                GetUserCode(),
                                                inclusive,
                                                exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            List<string> status = new List<string>();
            if (this.rdoRelease1.Checked)
                status.Add("'Release'");
            if (this.rdoWaitPick.Checked)
                status.Add("'WaitPick'");
            if (this.rdoPick.Checked)
                status.Add("'Pick'");
            if (this.rdoMakePackingList.Checked)
                status.Add("'MakePackingList'");
            if (this.rdoPack.Checked)
                status.Add("'Pack'");
            if (this.rdoOQC.Checked)
                status.Add("'OQC'");
            if (this.rdoClosePackingList.Checked)
                status.Add("'ClosePackingList'");
            if (this.rdoClose1.Checked)
                status.Add("'Close'");
            if (this.rdoCancel1.Checked)
                status.Add("'Cancel'");
            if (this.rdoBlock.Checked)
                status.Add("'Block'");
            return _WarehouseFacade.GetPicksCount(txtPickNoQuery.Text, drpPickTypeQuery.SelectedValue,
                drpStorageCodeQuery.SelectedValue, FormatHelper.CleanString(txtInvNoQuery.Text), string.Join(",", status.ToArray()),
                FormatHelper.TODateInt(txtCBDateQuery.Text), FormatHelper.TODateInt(txtCEDateQuery.Text), GetUserCode());
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            _WarehouseFacade.UpdatePick((BenQGuru.eMES.Domain.Warehouse.Pick)domainObject);
        }


        protected override bool ValidateInput()
        {

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            string pono = FormatHelper.CleanString(txtInvNoEdit.Text);
            bool isVender = _WarehouseFacade.IsHasVenderCode(pono, GetUserCode()) > 0;
            if (!isVender)
            {
                WebInfoPublish.Publish(this, "SAP单据号对应的供应商是否和当前用户所属用户组对应的供应商不吻合", this.languageComponent1);
                this.DataProvider.RollbackTransaction();
                return false;
            }
            return true;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }


        protected void Gener_ServerClick(object sender, EventArgs e)
        {

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            string dateStr = DateTime.Now.ToString("yyyyMMdd");

            string perfix = "OU" + dateStr;
            BenQGuru.eMES.Domain.Warehouse.Serialbook s = (BenQGuru.eMES.Domain.Warehouse.Serialbook)_WarehouseFacade.GetSerialbook(perfix);
            int max = 0;
            if (s == null)
            {
                max = 1;
                s = new BenQGuru.eMES.Domain.Warehouse.Serialbook();
                s.MAXSerial = "1";
                s.MDate = FormatHelper.TODateInt(DateTime.Now);
                s.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                s.MUser = GetUserCode();
                s.SNprefix = perfix;
                _WarehouseFacade.AddSerialbook(s);
            }
            else
            {
                max = int.Parse(s.MAXSerial);
                max++;
                s = (BenQGuru.eMES.Domain.Warehouse.Serialbook)_WarehouseFacade.GetSerialbook(perfix);
                s.MAXSerial = max.ToString();
                s.MDate = FormatHelper.TODateInt(DateTime.Now);
                s.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                s.MUser = GetUserCode();
                _WarehouseFacade.UpdateSerialbook(s);

            }

            txtPickNoEdit.Text = perfix + max.ToString().PadLeft(4, '0');
        }


        public string DownloadPath
        {
            get
            {
                return string.Format(@"{0}{1}/", this.PageVirtualHostRoot, _downloadPath.Trim('\\', '/').Replace('\\', '/'));
            }
        }



        #endregion

        #region Button
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            string k = row.Items.FindItemByKey("PICKNO").Text.Trim();
            string invNo = row.Items.FindItemByKey("INVNO").Text.Trim();
            if (commandName == "LinkToCartonImport")
            {
                Response.Redirect(this.MakeRedirectUrl("FCreateCommandDemand.aspx",
                                    new string[] { "PICKNO", "INVNO" },
                                    new string[] { k, invNo }));
            }
        }




        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);
                if (obj != null)
                {
                    this.SetEditObject(obj);
                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
            else if (commandName == "LinkToCartonImport")
            {
                string k = row.Items.FindItemByKey("PICKNO").Text.Trim();
                string invNo = row.Items.FindItemByKey("INVNO").Text.Trim();
                Response.Redirect(this.MakeRedirectUrl("FCreateCommandDemand.aspx",
                                    new string[] { "PICKNO", "INVNO" },
                                    new string[] { k, invNo }));
            }
            //if (commandName == "Edit")
            //{
            //    object obj = _WarehouseFacade.GetPick(row.Items.FindItemByKey("PICKNO").Text);
            //    BenQGuru.eMES.Domain.Warehouse.Pick pick = (BenQGuru.eMES.Domain.Warehouse.Pick)obj;
            //    txtPickNoEdit.Text = pick.PickNo;
            //    drpPickTypeEdit.SelectedValue = pick.PickType;
            //    txtInvNoEdit.Text = pick.InvNo;
            //    drpStorageCodeEdit.SelectedValue = pick.StorageCode;
            //    txtReceiverUserEdit.Text = pick.ReceiverUser;
            //    txtReceiverAddrEdit.Text = pick.Receiveraddr;
            //    txtPlanDateEdit.Text = FormatHelper.ToDateString(pick.PlanDate, "-");
            //    txtREMARKEdit.Text = pick.Remark1;
            //}
        }



        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            Pick pick = obj as Pick;
            if (pick == null)
                return null;
            return new string[]
                {
                    pick.PickNo,
    pick.InvNo,
       pick.PickType,// dicStu.ContainsKey(pick.PickType) ? dicStu[pick.PickType] : string.Empty,
     languageComponent1.GetString(pick.Status), //dicStu.ContainsKey(pick.Status) ? dicStu[pick.Status] : string.Empty,
          pick.StorageCode,// dicStu.ContainsKey(pick.StorageCode) ? dicStu[pick.StorageCode] : string.Empty,
           pick.ReceiverUser,
           pick.Receiveraddr,
        FormatHelper.ToDateString(pick.PlanDate),
         pick.PlanGIDate,
        FormatHelper.ToDateString(pick.DownDate),
        pick.DownUser,
          FormatHelper.ToDateString(pick.FinishDate),
          FormatHelper.ToTimeString(pick.FinishTime),
       FormatHelper.ToDateString(pick.OQCDate),
       FormatHelper.ToTimeString(pick.OQCTime),
                FormatHelper.ToDateString(pick.PackingListDate),
                FormatHelper.ToTimeString(pick.PackingListTime),
            FormatHelper.ToDateString(pick.DeliveryDate),
            FormatHelper.ToTimeString(pick.DeliveryTime),
    FormatHelper.ToDateString(pick.CDate),
    FormatHelper.ToTimeString(pick.CTime),
    pick.CUser,
    FormatHelper.ToDateString(pick.MaintainDate),
    FormatHelper.ToTimeString(pick.MaintainTime),
     pick.Remark1,
                };
        }

        private string GetDate(int date)
        {
            return date.ToString();
        }
        protected override string[] GetColumnHeaderText()
        {
            #region 注释
            return new string[] {	       "PICKNO",                                
                                        "INVNO",                            
                                        "PICKTYPE",                         
                                        "STATUS",                           
                                        "StorageCode",                      
                                        "ReceiverUser",                     
                                        "ReceiverAddr",                     
                                        "Plan_Date",                        
                                        "PLANGIDATE",                       
                                        "Down_Date",                        
                                        "Down_User",                        
                                        "Finish_Date",                      
                                        "Finish_Time",                      
                                        "OQC_Date",                         
                                        "OQC_Time",                         
                                        "Packing_List_Date",                
                                        "Packing_List_Time",                
                                        "Delivery_Date",                    
                                        "Delivery_Time",                    
                                        "CDATE",                            
                                        "CTIME",                            
                                        "CUSER",                            
                                        "MDATE",                            
                                        "MTIME",                            
                                        "Remark"                         
                                  };
            #endregion
        }

        #endregion



        protected void drpStorageInTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        #region add by sam
        private ViewField[] viewFieldList = null;
        private ViewField[] PickHeadViewFieldList
        {
            get
            {
                if (viewFieldList == null)
                {
                    if (facade == null)
                    {
                        facade = new InventoryFacade(base.DataProvider);
                    }
                    object[] objs = facade.QueryViewFieldByUserCode(this.GetUserCode(), "TBLCPICK");
                    if (objs != null)
                    {
                        viewFieldList = new ViewField[objs.Length];
                        objs.CopyTo(viewFieldList, 0);
                    }
                    else
                    {
                        objs = facade.QueryViewFieldDefault("TBLCPICK_FIELD_LIST_SYSTEM_DEFAULT", "TBLCPICK");
                        if (objs != null)
                        {
                            ArrayList list = new ArrayList();
                            for (int i = 0; i < objs.Length; i++)
                            {
                                ViewField field = (ViewField)objs[i];
                                //if (FormatHelper.StringToBoolean(field.IsDefault) == true)
                                //{
                                list.Add(field);
                                //}
                            }
                            viewFieldList = new ViewField[list.Count];
                            list.CopyTo(viewFieldList, 0);
                        }
                    }
                    if (viewFieldList != null)
                    {
                        bool bExistPickNo = false;
                        for (int i = 0; i < viewFieldList.Length; i++)
                        {
                            if (viewFieldList[i].FieldName == "INVNO")
                            {
                                bExistPickNo = true;
                                break;
                            }
                        }
                        if (bExistPickNo == false)
                        {
                            ViewField field = new ViewField();
                            field.FieldName = "INVNO";
                            field.Description = "SAP单据号";
                            ArrayList list = new ArrayList();
                            list.Add(field);
                            list.AddRange(viewFieldList);
                            viewFieldList = new ViewField[list.Count];
                            list.CopyTo(viewFieldList, 0);
                        }
                    }
                }
                return viewFieldList;
            }
        }
        #endregion


    }
}
