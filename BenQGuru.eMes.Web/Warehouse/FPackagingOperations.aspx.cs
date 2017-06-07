using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using System.Linq;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.MOModel;



namespace BenQGuru.eMES.Web.Warehouse
{
    public partial class FPackagingOperations : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private InventoryFacade _InventoryFacade = null;
        private WarehouseFacade _WarehouseFacade = null;

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

                string pickNo = Request.QueryString["PickNo"];
                if (!string.IsNullOrEmpty(pickNo))
                    txtPickNoQuery.Text = pickNo;
                string carinvno = Request.QueryString["CARINVNO"];
                if (!string.IsNullOrEmpty(carinvno))
                    txtCarInvNoQuery.Text = carinvno;
                if (Request.QueryString.AllKeys.Length > 0)
                    cmdReturn.Visible = true;


            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("CarInvNo", "发货箱单号", null);
            this.gridHelper.AddColumn("IT_CartonNo", "包装箱号", null);
            this.gridHelper.AddColumn("DQMCode", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("CusMCode", "客户物料编码", null);
            this.gridHelper.AddColumn("PickedQTY", "已拣数量", null);
            this.gridHelper.AddColumn("PackingQTY", "包装数量", null);

            this.gridHelper.AddDefaultColumn(true, false);
            this.gridHelper.AddLinkColumn("LinkToSN", "SN信息");

            this.gridWebGrid.Columns["CarInvNo"].Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            if (!string.IsNullOrEmpty(txtPickNoQuery.Text))
            {

                this.gridHelper.RequestData();
            }



        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["CarInvNo"] = ((PackagingOperations)obj).CARINVNO;
            row["IT_CartonNo"] = ((PackagingOperations)obj).CARTONNO;
            row["DQMCode"] = ((PackagingOperations)obj).DQMCODE;
            row["CusMCode"] = ((PackagingOperations)obj).CustMCode;
            row["PickedQTY"] = ((PackagingOperations)obj).SQTY;
            row["PackingQTY"] = ((PackagingOperations)obj).QTY;

            return row;

        }


        #region 删除
        protected override object GetEditObject(GridRecord row)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            object obj = _WarehouseFacade.QueryCartonInvDetailMaterial(row.Items.FindItemByKey("CarInvNo").Text,
                row.Items.FindItemByKey("IT_CartonNo").Text, row.Items.FindItemByKey("DQMCode").Text);
            if (obj != null)
            {
                return obj;
            }

            return null;
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(this.DataProvider);
            }



            CartonInvDetailMaterial[] cartonInvDetailMaterialList = ((CartonInvDetailMaterial[])domainObjects.ToArray(typeof(CartonInvDetailMaterial)));
            try
            {
                if (cartonInvDetailMaterialList == null || cartonInvDetailMaterialList.Length == 0)
                {
                    WebInfoPublish.Publish(this, "不存在要删除的数据！", this.languageComponent1);
                    return;

                }
                string carinvno = cartonInvDetailMaterialList[0].CARINVNO;
                CARTONINVOICES cartoninvoices = _WarehouseFacade.GetCartoninvoices(carinvno)
                   as CARTONINVOICES;
                if (cartoninvoices == null)
                {

                    WebInfoPublish.Publish(this, carinvno + "包装箱号不存在！", this.languageComponent1);
                    return;
                }
                if (cartoninvoices.STATUS == "ClosePack" ||
                  cartoninvoices.STATUS == "Close" ||
                  cartoninvoices.STATUS == "ClosePackingList")
                {
                    WebInfoPublish.Publish(this, "发货箱单状态不能为包装完成、箱单完成、已入库！", this.languageComponent1);
                    return;

                }



                //TBLCartonInvDetailMaterial
                //  1、输入拣货任务令查询到结果后才能删除。

                this.DataProvider.BeginTransaction();
                foreach (CartonInvDetailMaterial cartonInvDetailMaterial in cartonInvDetailMaterialList)
                {


                    #region delete
                    Pick pickHead = _InventoryFacade.GetPick(cartonInvDetailMaterial.PICKNO) as Pick;
                    PickDetail pickDetail = _InventoryFacade.GetPickDetail(cartonInvDetailMaterial.PICKNO, cartonInvDetailMaterial.PICKLINE) as PickDetail;
                    object[] objPickdetailmaterials = this._WarehouseFacade.QueryPICKDetailMaterialBydqMCode(cartonInvDetailMaterial.PICKNO, cartonInvDetailMaterial.DQMCODE);
                    //Pickdetailmaterial pickdetailmaterial = _WarehouseFacade.GetPickdetailmaterial(cartonInvDetailMaterial.PICKNO, cartonInvDetailMaterial.CARTONNO) as Pickdetailmaterial;
                    if (pickDetail == null || pickHead == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "拣货任务令或行信息不存在！", this.languageComponent1);
                        return;
                    }

                    //4、单行明细删除完毕后同时删除对应的tblcartoninvdetail。
                    _WarehouseFacade.DeleteCartonInvDetailMaterial(cartonInvDetailMaterial);
                    //2、删除箱号后同时删除掉箱中的所有SN信息。
                    _WarehouseFacade.DeleteCartoninvdetailsnByCartonNo(pickDetail.PickNo, cartonInvDetailMaterial.CARTONNO, cartonInvDetailMaterial.DQMCODE);
                    // CARINVNO,CARTONNO

                    object[] list = _WarehouseFacade.QueryCartonInvDetailMaterial(cartonInvDetailMaterial.CARINVNO);
                    if (list == null)
                    {
                        CartonInvDetail cartonInvDetail =
                     _WarehouseFacade.GetCartonInvDetail(cartonInvDetailMaterial.CARINVNO,
                                                         cartonInvDetailMaterial.CARTONNO) as CartonInvDetail;
                        if (cartonInvDetail != null)
                        {
                            _WarehouseFacade.DeleteCartonInvDetail(cartonInvDetail);
                        }


                        pickHead.Status = PickHeadStatus.PickHeadStatus_Pick;
                        _WarehouseFacade.UpdatePick(pickHead);
                    }


                    //5、拣货任务令头状态为pick或pack，且发货箱单头状态为Release或pack时才可以删除。
                    //    (对应的组合：a、tblpick.status=pick&&tblcartoninvoices.status=Release
                    //  b、tblpick.status=pack&&tblcartoninvoices.status=pack)
                    //if ((pickHead.Status == PickHeadStatus.PickHeadStatus_Pick && cartoninvoices.STATUS == PickDetail_STATUS.Status_Release)
                    //    || (pickHead.Status == PickHeadStatus.PickHeadStatus_Pack && cartoninvoices.STATUS == PickDetail_STATUS.Status_Pack)
                    //    || pickHead.Status == PickHeadStatus.PickHeadStatus_WaitPick)
                    //{
                    //    //3、明细删除完毕后将拣货任务令头更新为MakePackingList。
                    //    object[] list = _WarehouseFacade.QueryCartonInvDetailMaterial(cartonInvDetailMaterial.CARINVNO);
                    //    if (list == null)
                    //    {
                    //        CartonInvDetail cartonInvDetail =
                    //     _WarehouseFacade.GetCartonInvDetail(cartonInvDetailMaterial.CARINVNO,
                    //                                         cartonInvDetailMaterial.CARTONNO) as CartonInvDetail;
                    //        if (cartonInvDetail != null)
                    //        {
                    //            _WarehouseFacade.DeleteCartonInvDetail(cartonInvDetail);
                    //        }
                    //        pickHead.Status = PickHeadStatus.PickHeadStatus_MakePackingList;
                    //        _WarehouseFacade.UpdatePick(pickHead);
                    //    }
                    //}
                    //else
                    //{
                    //    this.DataProvider.RollbackTransaction();
                    //    WebInfoPublish.Publish(this, "拣货任务令头状态为pick发货箱单头状态为Release或者拣货任务令头状态为pack发货箱单头状态为pack时才可以删除", this.languageComponent1);
                    //    return;
                    //}

                    pickDetail.PQTY -= cartonInvDetailMaterial.QTY;
                    _InventoryFacade.UpdatePickDetail(pickDetail);

                    #region Pickdetailmaterial
                    decimal qTY = cartonInvDetailMaterial.QTY;

                    if (objPickdetailmaterials != null && objPickdetailmaterials.Length > 0)
                    {
                        foreach (Pickdetailmaterial _pickdetailmaterial in objPickdetailmaterials)
                        {
                            decimal num = _pickdetailmaterial.PQty;
                            if (num > 0)
                            {
                                if (qTY > num)
                                {
                                    _pickdetailmaterial.PQty -= _pickdetailmaterial.PQty;
                                    this._WarehouseFacade.UpdatePickdetailmaterial(_pickdetailmaterial);
                                    qTY -= num;
                                }
                                else
                                {
                                    _pickdetailmaterial.PQty -= qTY;
                                    this._WarehouseFacade.UpdatePickdetailmaterial(_pickdetailmaterial);
                                    qTY = 0;
                                    break;
                                }
                            }
                        }
                    }

                    #endregion
                    //pickdetailmaterial.PQty -= cartonInvDetailMaterial.QTY;
                    //_WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);
                    cartonInvDetailMaterial.QTY -= cartonInvDetailMaterial.QTY;
                    _WarehouseFacade.UpdateCartonInvDetailMaterial(cartonInvDetailMaterial);
                    #endregion
                }

                CartonInvDetailMaterial[] cas = _WarehouseFacade.GetCartonInvDetailMaterialByCa(txtCarInvNoQuery.Text);
                if (cas.Length == 0)
                {
                    CARTONINVOICES cartoninvoices1 = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoices(txtCarInvNoQuery.Text);
                    cartoninvoices1.STATUS = CartonInvoices_STATUS.Status_Release;
                    _WarehouseFacade.UpdateCartoninvoices(cartoninvoices1);
                }
                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "删除成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }



        }
        #endregion

        #region  add by sam 查询前检查
        protected override void cmdQuery_Click(object sender, System.EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            string pickNo = this.txtPickNoQuery.Text.Trim().ToUpper();
            Pick pick = (Pick)_WarehouseFacade.GetPick(pickNo);
            if (pick != null)
            {
                if (!string.IsNullOrEmpty(pick.GFFlag))
                {
                    WebInfoPublish.Publish(this, "请至光伏包装页面操作", this.languageComponent1);
                    return;
                }
            }
            this.gridHelper.RequestData();
            if (this.gridHelper2 != null)
                this.gridHelper2.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }
        #endregion
        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }

            string pickNo = FormatHelper.CleanString(this.txtPickNoQuery.Text.Trim().ToUpper());
            object obj = _WarehouseFacade.GetCartonInvoices(pickNo);
            if (obj == null)
            {
                this.txtCarInvNoQuery.Text = string.Empty;
            }
            else
            {
                this.txtCarInvNoQuery.Text = (obj as CARTONINVOICES).CARINVNO;
            }

            BindDQMaterialNO();
            this.txtSNEdit.Text = string.Empty;

            return this._WarehouseFacade.QueryPackagingOperations(pickNo, FormatHelper.CleanString(this.txtCARTONNOQuery.Text), inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.QueryPackagingOperationsCount(
                FormatHelper.CleanString(this.txtPickNoQuery.Text.Trim().ToUpper()), FormatHelper.CleanString(this.txtCARTONNOQuery.Text)
                );
        }

        #endregion

        #region Button

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "LinkToSN")
            {
                string carInvNo = row.Items.FindItemByKey("CarInvNo").Text.Trim();
                string cartonNo = row.Items.FindItemByKey("IT_CartonNo").Text.Trim();

                Response.Redirect(
                                    this.MakeRedirectUrl("FPackagingOperationsSN.aspx",
                                    new string[] { "CarInvNo", "CartonNo" },
                                    new string[] { carInvNo, cartonNo })
                                   );
            }
        }

        protected void cmdSaveIt_ServerClick(object sender, System.EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtCartonNo.Text))
            {
                WebInfoPublish.Publish(this, "必须输入箱号", this.languageComponent1);
                return;
            }
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            string pickNo = this.txtPickNoQuery.Text.Trim().ToUpper();
            string carInvNo = string.Empty;
            object obj = _WarehouseFacade.GetCartonInvoices(pickNo);
            if (obj != null)
            {
                carInvNo = (obj as CARTONINVOICES).CARINVNO;
            }

            string dqMaterialNO = this.ddlDQMaterialNO.SelectedValue;
            string cartonNo = this.txtCartonNo.Text.Trim().ToUpper();
            string qty = this.txtQTY.Text.Trim();
            string sn = this.txtSNEdit.Text.Trim().ToUpper();
            StorageDetail storageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(cartonNo);
            if (storageDetail != null)
            {
                if (storageDetail.AvailableQty > 0)
                {
                    WebInfoPublish.Publish(this, "请使用新箱包装", this.languageComponent1);
                    return;
                }
            }

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);
            string mUser = this.GetUserCode();
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            try
            {
                this.DataProvider.BeginTransaction();
                //更新状态
                object objPick = this._InventoryFacade.GetPick(pickNo);
                if (objPick != null)
                {
                    Pick pick = objPick as Pick;
                    pick.Status = "Pack";
                    pick.MaintainUser = mUser;
                    pick.MaintainDate = mDate;
                    pick.MaintainTime = mTime;
                    this._InventoryFacade.UpdatePick(pick);
                }

                object objPICKDetailMaterial = this._WarehouseFacade.QueryPICKDetailMaterial(pickNo, cartonNo);
                //拣料表中是否存在箱号？
                if (objPICKDetailMaterial == null)
                {
                    #region 非整箱包货
                    //检查箱号，鼎桥物料号，是够存在
                    if (string.IsNullOrEmpty(this.ddlDQMaterialNO.SelectedValue))
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "必须选择鼎桥物料号", this.languageComponent1);
                        return;
                    }

                    //不存在，要通过鼎桥物料号判断是取SN还是数量
                    //1，根据鼎桥物料号，先判断是单件管控还是非单件管控
                    object[] mar_objs = _WarehouseFacade.GetMaterialInfoByQDMCode(dqMaterialNO);
                    if (mar_objs == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "无效的鼎桥物料号", this.languageComponent1);
                        return;
                    }
                    Domain.MOModel.Material mar = mar_objs[0] as Domain.MOModel.Material;
                    if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)  //单件管控
                    {
                        #region 单件管控
                        if (string.IsNullOrEmpty(this.txtSNEdit.Text))
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "必须输入SN号码", this.languageComponent1);
                            this.txtSNEdit.Focus();
                            return;
                        }
                        //  取SN
                        //检查SN在tblpickdetailmaterialsn表里有数据吗，没有报错，有对表tblcartoninvoicesmaterial，tblcartoninvoicesmaterialsn操作
                        object objPickdetailmaterialsn = this._WarehouseFacade.GetPickdetailmaterialsn(pickNo, sn);
                        if (objPickdetailmaterialsn == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "刷入SN条码不存在", this.languageComponent1);
                            return;
                        }
                        Pickdetailmaterialsn pikdetailsn = objPickdetailmaterialsn as Pickdetailmaterialsn;
                        object objPickdetailmaterial = _WarehouseFacade.GetPickdetailmaterial(pikdetailsn.Pickno, pikdetailsn.Cartonno);
                        if (objPickdetailmaterial == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "输入的SN找不到箱号信息", this.languageComponent1);
                            return;
                        }
                        Pickdetailmaterial Pickdetailmaterial = objPickdetailmaterial as Pickdetailmaterial;
                        //2>	检查SN条码是否存在当前拣货任务令号对应发货箱单的发货箱单明细SN信息表(TBLCartonInvDetailSN)中，存在则报错提示刷入SN条码已包装过
                        object _obj = this._WarehouseFacade.GetCartoninvdetailsn(carInvNo, sn);
                        if (_obj != null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "刷入SN条码已包装过", this.languageComponent1);
                            return;
                        }
                        //输入的SN的鼎桥物料号是否与选中的相同

                        if (Pickdetailmaterial.DqmCode != dqMaterialNO)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "SN的号码与选择的鼎桥物料号不一致", this.languageComponent1);
                            return;
                        }
                        //5>	新增发货箱单明细SN信息表(TBLCartonInvDetailSN)数据
                        Pickdetailmaterialsn pickdetailmaterialsn = objPickdetailmaterialsn as Pickdetailmaterialsn;
                        CARTONINVDETAILSN _CARTONINVDETAILSN = new CARTONINVDETAILSN();
                        _CARTONINVDETAILSN.CARINVNO = carInvNo;
                        _CARTONINVDETAILSN.PICKNO = pickNo;
                        _CARTONINVDETAILSN.PICKLINE = pickdetailmaterialsn.Pickline;
                        _CARTONINVDETAILSN.CARTONNO = cartonNo;
                        _CARTONINVDETAILSN.SN = pickdetailmaterialsn.Sn;
                        _CARTONINVDETAILSN.MUSER = mUser;
                        _CARTONINVDETAILSN.MDATE = mDate;
                        _CARTONINVDETAILSN.MTIME = mTime;
                        this._WarehouseFacade.AddCARTONINVDETAILSN(_CARTONINVDETAILSN);

                        //3>	检查包装箱号是否存在当前拣货任务令号对应发货箱单的发货箱单明细信息表(TBLCartonInvDetail)中，不存在则新增发货箱单明细信息表(TBLCartonInvDetail)数据
                        object objCartonInvDetail = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        if (objCartonInvDetail == null)
                        {
                            //箱是否存在--不存在
                            CartonInvDetail cartonInvDetail = new CartonInvDetail();
                            cartonInvDetail.CARINVNO = carInvNo;
                            cartonInvDetail.PICKNO = pickNo;
                            cartonInvDetail.STATUS = "Pack";
                            cartonInvDetail.CARTONNO = cartonNo;
                            cartonInvDetail.PACKMCODE = "";
                            cartonInvDetail.PACKQTY = 1;
                            cartonInvDetail.CUSER = mUser;
                            cartonInvDetail.CDATE = mDate;
                            cartonInvDetail.CTIME = mTime;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            cartonInvDetail.MUSER = mUser;
                            this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);

                            //this.DataProvider.CommitTransaction();

                            //  插入 cartonInvDetailMaterial
                            CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                            cartonInvDetailMaterial.CARINVNO = carInvNo;
                            cartonInvDetailMaterial.CARTONNO = cartonNo;
                            cartonInvDetailMaterial.CDATE = mDate;
                            cartonInvDetailMaterial.CTIME = mTime;
                            cartonInvDetailMaterial.CUSER = mUser;
                            cartonInvDetailMaterial.DQMCODE = dqMaterialNO;
                            cartonInvDetailMaterial.MCODE = mar.MCode;
                            cartonInvDetailMaterial.MDATE = mDate;
                            cartonInvDetailMaterial.MTIME = mTime;
                            cartonInvDetailMaterial.MUSER = mUser;
                            cartonInvDetailMaterial.PICKLINE = pikdetailsn.Pickline;
                            cartonInvDetailMaterial.PICKNO = pikdetailsn.Pickno;
                            cartonInvDetailMaterial.QTY = 1;
                            cartonInvDetailMaterial.UNIT = Pickdetailmaterial.Unit;

                            _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);
                        }
                        else
                        {


                            object objCartonInvDetailMaterial = this._WarehouseFacade.QueryCartonInvDetailMaterial(carInvNo, cartonNo, dqMaterialNO);
                            if (objCartonInvDetailMaterial == null)
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                                cartonInvDetailMaterial.CARINVNO = carInvNo;
                                cartonInvDetailMaterial.CARTONNO = cartonNo;
                                cartonInvDetailMaterial.CDATE = mDate;
                                cartonInvDetailMaterial.CTIME = mTime;
                                cartonInvDetailMaterial.CUSER = mUser;
                                cartonInvDetailMaterial.DQMCODE = dqMaterialNO;
                                cartonInvDetailMaterial.MCODE = mar.MCode;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.PICKLINE = pikdetailsn.Pickline;
                                cartonInvDetailMaterial.PICKNO = pikdetailsn.Pickno;
                                cartonInvDetailMaterial.QTY = 1;
                                cartonInvDetailMaterial.UNIT = Pickdetailmaterial.Unit;

                                _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);
                            }
                            else
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = objCartonInvDetailMaterial as CartonInvDetailMaterial;
                                cartonInvDetailMaterial.QTY += 1;
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                this._WarehouseFacade.UpdateCartonInvDetailMaterial(cartonInvDetailMaterial);
                            }
                            // 更新表CartonInvDetail的以包数量
                            CartonInvDetail cartonInvDetail = objCartonInvDetail as CartonInvDetail;
                            object[] objs_cartondetailmaterial = _WarehouseFacade.GetCartonInvDetailMaterial(carInvNo, cartonNo);
                            if (objs_cartondetailmaterial == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "更新发货箱出错", this.languageComponent1);
                                return;
                            }
                            double ss = 0;
                            for (int i = 0; i < objs_cartondetailmaterial.Length; i++)
                            {
                                CartonInvDetailMaterial cartondetailmar = objs_cartondetailmaterial[i] as CartonInvDetailMaterial;
                                ss += (double)cartondetailmar.QTY;
                            }
                            cartonInvDetail.PACKQTY = ss;
                            cartonInvDetail.MUSER = mUser;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);



                        }
                        #endregion
                        #region //更新 pickdetailmaterial
                        Pickdetailmaterial.PQty += 1;
                        Pickdetailmaterial.MaintainDate = mDate;
                        Pickdetailmaterial.MaintainTime = mTime;
                        Pickdetailmaterial.MaintainUser = mUser;
                        _WarehouseFacade.UpdatePickdetailmaterial(Pickdetailmaterial);

                        #endregion
                        #region //更新 pickdetail
                        object[] pickdetailmaterial_obj = _WarehouseFacade.GetAllPickDetailMaterialByPickNoAndLine(pickNo, pikdetailsn.Pickline);
                        if (pickdetailmaterial_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "计算包装数量出错", this.languageComponent1);
                            return;
                        }
                        decimal sum = 0;
                        for (int i = 0; i < pickdetailmaterial_obj.Length; i++)
                        {
                            Pickdetailmaterial pickdetailmater = pickdetailmaterial_obj[i] as Pickdetailmaterial;
                            sum += pickdetailmater.PQty;
                        }
                        object pickdetail_obj = _WarehouseFacade.GetPickdetail(pickNo, pikdetailsn.Pickline);
                        if (pickdetail_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "更新拣货明细表出错", this.languageComponent1);
                            return;
                        }
                        PickDetail pickdetail = pickdetail_obj as PickDetail;
                        pickdetail.PQTY = sum;
                        if (pickdetail.PQTY == pickdetail.SQTY)
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_ClosePack;
                        }
                        else
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_Pack;
                        }
                        pickdetail.MaintainDate = mDate;
                        pickdetail.MaintainTime = mTime;
                        pickdetail.MaintainUser = mUser;

                        _WarehouseFacade.UpdatePickdetail(pickdetail);

                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = cartonNo;
                        trans.DqMCode = mar.DqmCode;
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = (objPick as Pick).InvNo;//.InvNo;
                        trans.InvType = (objPick as Pick).PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = mDate;
                        trans.MaintainTime = mTime;
                        trans.MaintainUser = mUser;
                        trans.MCode = mar.MCode;


                        trans.Qty = 1;
                        // stor.AvailableQty =stor.StorageQty-stor.FreezeQty;


                        trans.ProductionDate = 0;

                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = (objPick as Pick).StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = (objPick as Pick).PickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "PACK";
                        _WarehouseFacade.AddInvInOutTrans(trans);


                        #endregion

                        #region  箱包装完成，改变状态
                        object objCartonInvDetail1 = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        CartonInvDetail car = objCartonInvDetail1 as CartonInvDetail;
                        if (pickdetail.SQTY == (decimal)car.PACKQTY)
                        {
                            car.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                            car.MUSER = mUser;
                            car.MDATE = mDate;
                            car.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(car);
                        }
                        #endregion

                    }
                    else // 非单件管控
                    {
                        // 取页面上的数量
                        //检查一下，鼎桥物料号在这个表
                        // 有对表tblcartoninvoicesmaterial tblpickdetailmaterial 有数据。 
                        #region 非单件管控
                        if (string.IsNullOrEmpty(this.txtQTY.Text))
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "必须输入数量", this.languageComponent1);
                            this.txtQTY.Focus();
                            return;
                        }
                        //判断数量是否是数字格式
                        try
                        {
                            decimal _qty = decimal.Parse(qty);
                            if (_qty <= 0)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "数量必须为大于零的数字", this.languageComponent1);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "数量必须为大于零的数字", this.languageComponent1);
                            return;
                        }

                        object[] objPickdetail = _WarehouseFacade.GetAllPickDetailByPickNoAndDQMCode(pickNo, dqMaterialNO);
                        if (objPickdetail == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "拣货任务明细中没有该鼎桥物料号", this.languageComponent1);
                            return;
                        }
                        PickDetail Pickdetail = objPickdetail[0] as PickDetail;
                        object objCartonInvDetail = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        if (objCartonInvDetail == null)
                        {
                            CartonInvDetail cartonInvDetail = new CartonInvDetail();
                            cartonInvDetail.CARINVNO = carInvNo;
                            cartonInvDetail.PICKNO = pickNo;
                            cartonInvDetail.STATUS = "Pack";
                            cartonInvDetail.CARTONNO = cartonNo;
                            cartonInvDetail.PACKMCODE = "";
                            cartonInvDetail.PACKQTY = double.Parse(qty);
                            cartonInvDetail.CUSER = mUser;
                            cartonInvDetail.CDATE = mDate;
                            cartonInvDetail.CTIME = mTime;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            cartonInvDetail.MUSER = mUser;
                            this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);
                            // 
                            CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                            cartonInvDetailMaterial.CARINVNO = carInvNo;
                            cartonInvDetailMaterial.CARTONNO = cartonNo;
                            cartonInvDetailMaterial.CDATE = mDate;
                            cartonInvDetailMaterial.CTIME = mTime;
                            cartonInvDetailMaterial.CUSER = mUser;
                            cartonInvDetailMaterial.DQMCODE = dqMaterialNO;
                            cartonInvDetailMaterial.MCODE = mar.MCode;
                            cartonInvDetailMaterial.MDATE = mDate;
                            cartonInvDetailMaterial.MTIME = mTime;
                            cartonInvDetailMaterial.MUSER = mUser;
                            cartonInvDetailMaterial.PICKLINE = Pickdetail.PickLine;
                            cartonInvDetailMaterial.PICKNO = Pickdetail.PickNo;
                            cartonInvDetailMaterial.QTY = decimal.Parse(qty);
                            cartonInvDetailMaterial.UNIT = Pickdetail.Unit;

                            _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);

                        }
                        else
                        {

                            CartonInvDetail CartonInvDetail = objCartonInvDetail as CartonInvDetail;
                            object objCartonInvDetailMaterial = this._WarehouseFacade.QueryCartonInvDetailMaterial(carInvNo, cartonNo, dqMaterialNO);
                            if (objCartonInvDetailMaterial == null)
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                                cartonInvDetailMaterial.CARINVNO = carInvNo;
                                cartonInvDetailMaterial.CARTONNO = cartonNo;
                                cartonInvDetailMaterial.CDATE = mDate;
                                cartonInvDetailMaterial.CTIME = mTime;
                                cartonInvDetailMaterial.CUSER = mUser;
                                cartonInvDetailMaterial.DQMCODE = dqMaterialNO;
                                cartonInvDetailMaterial.MCODE = mar.MCode;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.PICKLINE = Pickdetail.PickLine;
                                cartonInvDetailMaterial.PICKNO = Pickdetail.PickNo;
                                cartonInvDetailMaterial.QTY = decimal.Parse(qty);
                                cartonInvDetailMaterial.UNIT = Pickdetail.Unit;

                                _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);

                            }
                            else
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = objCartonInvDetailMaterial as CartonInvDetailMaterial;
                                cartonInvDetailMaterial.QTY += decimal.Parse(qty);
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                this._WarehouseFacade.UpdateCartonInvDetailMaterial(cartonInvDetailMaterial);
                            }

                            // 更新表CartonInvDetail的以包数量
                            CartonInvDetail cartonInvDetail = objCartonInvDetail as CartonInvDetail;
                            object[] objs_cartondetailmaterial = _WarehouseFacade.GetCartonInvDetailMaterial(carInvNo, cartonNo);
                            if (objs_cartondetailmaterial == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "更新发货箱出错", this.languageComponent1);
                                return;
                            }
                            double ss = 0;
                            for (int i = 0; i < objs_cartondetailmaterial.Length; i++)
                            {
                                CartonInvDetailMaterial cartondetailmar = objs_cartondetailmaterial[i] as CartonInvDetailMaterial;
                                ss += (double)cartondetailmar.QTY;
                            }
                            cartonInvDetail.PACKQTY = ss;
                            cartonInvDetail.MUSER = mUser;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);

                        }
                        #endregion
                        #region //更新 pickdetailmaterial
                        //5>	更新已拣物料明细表(TBLPICKDetailMaterial)数据
                        decimal qTY = decimal.Parse(qty);
                        object[] objPickdetailmaterials = this._WarehouseFacade.QueryPICKDetailMaterials(pickNo, dqMaterialNO);
                        foreach (Pickdetailmaterial _pickdetailmaterial in objPickdetailmaterials)
                        {
                            decimal num = _pickdetailmaterial.Qty - _pickdetailmaterial.PQty;
                            if (num > 0)
                            {
                                if (qTY > num)
                                {
                                    _pickdetailmaterial.PQty = _pickdetailmaterial.Qty;
                                    _pickdetailmaterial.MaintainUser = mUser;
                                    _pickdetailmaterial.MaintainDate = mDate;
                                    _pickdetailmaterial.MaintainTime = mTime;
                                    this._WarehouseFacade.UpdatePickdetailmaterial(_pickdetailmaterial);

                                    qTY -= num;
                                }
                                else
                                {
                                    _pickdetailmaterial.PQty += qTY;
                                    _pickdetailmaterial.MaintainUser = mUser;
                                    _pickdetailmaterial.MaintainDate = mDate;
                                    _pickdetailmaterial.MaintainTime = mTime;
                                    this._WarehouseFacade.UpdatePickdetailmaterial(_pickdetailmaterial);

                                    qTY = 0;

                                    break;
                                }
                            }
                        }
                        if (qTY > 0)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "输入的包装数量过大", this.languageComponent1);
                            return;
                        }

                        #endregion
                        #region //更新 pickdetail
                        object[] pickdetailmaterial_obj = _WarehouseFacade.GetAllPickDetailMaterialByPickNoAndDQMCode(pickNo, dqMaterialNO);
                        if (pickdetailmaterial_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "计算包装数量出错", this.languageComponent1);
                            return;
                        }
                        decimal sum = 0;
                        for (int i = 0; i < pickdetailmaterial_obj.Length; i++)
                        {
                            Pickdetailmaterial pickdetailmater = pickdetailmaterial_obj[i] as Pickdetailmaterial;
                            sum += pickdetailmater.PQty;
                        }
                        object[] pickdetail_obj = _WarehouseFacade.GetAllPickDetailByPickNoAndDQMCode(pickNo, dqMaterialNO);
                        if (pickdetail_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "更新拣货明细表出错", this.languageComponent1);
                            return;
                        }
                        PickDetail pickdetail = pickdetail_obj[0] as PickDetail;
                        pickdetail.PQTY = sum;
                        if (pickdetail.PQTY == pickdetail.SQTY)
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_ClosePack;



                        }
                        else
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_Pack;
                        }
                        pickdetail.MaintainDate = mDate;
                        pickdetail.MaintainTime = mTime;
                        pickdetail.MaintainUser = mUser;

                        _WarehouseFacade.UpdatePickdetail(pickdetail);

                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = cartonNo;
                        trans.DqMCode = mar.DqmCode;
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = (objPick as Pick).InvNo;//.InvNo;
                        trans.InvType = (objPick as Pick).PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = mDate;
                        trans.MaintainTime = mTime;
                        trans.MaintainUser = mUser;
                        trans.MCode = mar.MCode;


                        trans.Qty = qTY;
                        // stor.AvailableQty =stor.StorageQty-stor.FreezeQty;


                        trans.ProductionDate = 0;

                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = (objPick as Pick).StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = (objPick as Pick).PickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "PACK";
                        _WarehouseFacade.AddInvInOutTrans(trans);


                        #endregion

                        #region  箱包装完成，改变状态
                        object objCartonInvDetail1 = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        CartonInvDetail car = objCartonInvDetail1 as CartonInvDetail;
                        if (pickdetail.SQTY == (decimal)car.PACKQTY)
                        {
                            car.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                            car.MUSER = mUser;
                            car.MDATE = mDate;
                            car.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(car);
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 整箱包货
                    //如果箱号是有
                    //1, cartonNo 在tblpickdetailmaterial中 以pickno为条件，查
                    //如果有，就将tblpickdetailmaterial where carton=‘cartonNo’ and pick=？ 搬到tblcartoninvoicesmaterial中
                    //如果这个表tblpickdetailmaterialsn中有数据，将这些数据搬到tblcartoninvoicesmaterialsn中
                    Pickdetailmaterial pickdetailmaterial = objPICKDetailMaterial as Pickdetailmaterial;
                    if (pickdetailmaterial.PQty != 0)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "该箱号已部分包装,不能再使用原箱号码", this.languageComponent1);
                        return;
                    }
                    else
                    {
                        //1>	新增发货箱单明细信息表(TBLCartonInvDetail)数据
                        CartonInvDetail cartonInvDetail = new CartonInvDetail();
                        cartonInvDetail.CARINVNO = carInvNo;
                        cartonInvDetail.PICKNO = pickNo;
                        cartonInvDetail.STATUS = "ClosePack";
                        cartonInvDetail.CARTONNO = cartonNo;
                        //cartonInvDetail.PACKMCODE = "";
                        //cartonInvDetail.PACKQTY = 0;
                        cartonInvDetail.CUSER = mUser;
                        cartonInvDetail.CDATE = mDate;
                        cartonInvDetail.CTIME = mTime;
                        cartonInvDetail.MDATE = mDate;
                        cartonInvDetail.MTIME = mTime;
                        cartonInvDetail.MUSER = mUser;
                        this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);

                        //  this.DataProvider.CommitTransaction();

                        //2>	新增发货箱单明细物料信息表(TBLCartonInvDetailMaterial)数据
                        //object _obj = this._WarehouseFacade.GetPickdetailmaterial(pickNo,cartonNo);
                        //if (_obj == null)
                        //{
                        //    this.DataProvider.RollbackTransaction();
                        //    WebInfoPublish.Publish(this, "查找已经拣料明细出错", this.languageComponent1);
                        //    return;
                        //}
                        //Pickdetailmaterial pickDetailmar = _obj as Pickdetailmaterial;
                        CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                        cartonInvDetailMaterial.CARINVNO = carInvNo;
                        cartonInvDetailMaterial.PICKNO = pickNo;
                        cartonInvDetailMaterial.PICKLINE = pickdetailmaterial.Pickline;
                        cartonInvDetailMaterial.CARTONNO = cartonNo;
                        cartonInvDetailMaterial.MCODE = pickdetailmaterial.MCode;
                        cartonInvDetailMaterial.DQMCODE = pickdetailmaterial.DqmCode;
                        cartonInvDetailMaterial.QTY = pickdetailmaterial.Qty;
                        cartonInvDetailMaterial.UNIT = pickdetailmaterial.Unit;
                        cartonInvDetailMaterial.GFHWITEMCODE = string.Empty;
                        cartonInvDetailMaterial.GFPACKINGSEQ = string.Empty;
                        cartonInvDetailMaterial.CUSER = mUser;
                        cartonInvDetailMaterial.CDATE = mDate;
                        cartonInvDetailMaterial.CTIME = mTime;
                        cartonInvDetailMaterial.MDATE = mDate;
                        cartonInvDetailMaterial.MTIME = mTime;
                        cartonInvDetailMaterial.MUSER = mUser;
                        this._WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);

                        //3>	新增发货箱单明细SN信息表(TBLCartonInvDetailSN)数据
                        object[] objs = this._WarehouseFacade.GetPickDetailMaterialSN(pickNo, cartonNo);
                        if (objs == null)
                        {
                            //this.DataProvider.RollbackTransaction();
                            //WebInfoPublish.Publish(this, "查找已拣物料SN出错", this.languageComponent1);
                            //return;
                        }
                        else
                        {
                            CARTONINVDETAILSN cartonINVDETAILSN = new CARTONINVDETAILSN();

                            foreach (Pickdetailmaterialsn pickdetailmaterialsn in objs)
                            {
                                cartonINVDETAILSN.CARINVNO = carInvNo;
                                cartonINVDETAILSN.PICKNO = pickNo;
                                cartonINVDETAILSN.PICKLINE = pickdetailmaterialsn.Pickline;
                                cartonINVDETAILSN.CARTONNO = cartonNo;
                                cartonINVDETAILSN.SN = pickdetailmaterialsn.Sn;
                                cartonINVDETAILSN.MUSER = mUser;
                                cartonINVDETAILSN.MDATE = mDate;
                                cartonINVDETAILSN.MTIME = mTime;
                                this._WarehouseFacade.AddCARTONINVDETAILSN(cartonINVDETAILSN);
                            }
                        }

                        ////6>	更新已拣物料明细表(TBLPICKDetailMaterial)数据
                        //pickdetailmaterial.PQty = pickdetailmaterial.Qty;
                        //pickdetailmaterial.MaintainUser = mUser;
                        //pickdetailmaterial.MaintainDate = mDate;
                        //pickdetailmaterial.MaintainTime = mTime;
                        //this._WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);

                        ////4>	更新拣拣货任务令头表(TBLPICK)数据
                        //object objPick = this._InventoryFacade.GetPick(pickNo);
                        //if (objPick != null)
                        //{
                        //    Pick pick = objPick as Pick;
                        //    pick.Status = "Pack";
                        //    pick.MaintainUser = mUser;
                        //    pick.MaintainDate = mDate;
                        //    pick.MaintainTime = mTime;
                        //    this._InventoryFacade.UpdatePick(pick);
                        //}

                        #region //更新 pickdetailmaterial
                        pickdetailmaterial.PQty = pickdetailmaterial.Qty;
                        pickdetailmaterial.MaintainDate = mDate;
                        pickdetailmaterial.MaintainTime = mTime;
                        pickdetailmaterial.MaintainUser = mUser;
                        _WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);
                        #endregion

                        #region //更新 pickdetail
                        object[] pickdetailmaterial_obj = _WarehouseFacade.GetAllPickDetailMaterialByPickNoAndLine(pickNo, pickdetailmaterial.Pickline);
                        if (pickdetailmaterial_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "计算包装数量出错", this.languageComponent1);
                            return;
                        }
                        decimal sum = 0;
                        for (int i = 0; i < pickdetailmaterial_obj.Length; i++)
                        {
                            Pickdetailmaterial pickdetailmater = pickdetailmaterial_obj[i] as Pickdetailmaterial;
                            sum += pickdetailmater.PQty;
                        }
                        object pickdetail_obj = _WarehouseFacade.GetPickdetail(pickNo, pickdetailmaterial.Pickline);
                        if (pickdetail_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "更新拣货明细表出错", this.languageComponent1);
                            return;
                        }
                        PickDetail pickdetail = pickdetail_obj as PickDetail;
                        pickdetail.PQTY = sum;
                        if (pickdetail.PQTY == pickdetail.SQTY)
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_ClosePack;
                        }
                        else
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_Pack;
                        }
                        pickdetail.MaintainDate = mDate;
                        pickdetail.MaintainTime = mTime;
                        pickdetail.MaintainUser = mUser;

                        _WarehouseFacade.UpdatePickdetail(pickdetail);



                        #endregion
                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = cartonNo;
                        trans.DqMCode = pickdetailmaterial.DqmCode; ;
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = (objPick as Pick).InvNo;//.InvNo;
                        trans.InvType = (objPick as Pick).PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = mDate;
                        trans.MaintainTime = mTime;
                        trans.MaintainUser = mUser;
                        trans.MCode = pickdetailmaterial.MCode;


                        trans.Qty = pickdetailmaterial.Qty;
                        // stor.AvailableQty =stor.StorageQty-stor.FreezeQty;


                        trans.ProductionDate = 0;

                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = (objPick as Pick).StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = (objPick as Pick).PickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "PACK";
                        _WarehouseFacade.AddInvInOutTrans(trans);



                    }
                    #endregion
                }

                object objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                if (objCARTONINVOICES == null)
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, "找不到拣货任务令号对应的发货箱单头信息", this.languageComponent1);
                    return;
                }
                CARTONINVOICES _CARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                if (_CARTONINVOICES.STATUS != CartonInvoices_STATUS.Status_Pack)
                {
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_Pack;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }

                //8>	检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] _objs = this._WarehouseFacade.QueryPickDetail(pickNo);
                if (_objs != null)
                {
                    bool isTrue = true;
                    foreach (PickDetail _pickDetail in _objs)
                    {
                        if (_pickDetail.SQTY != _pickDetail.PQTY)
                        {
                            isTrue = false;
                            break;
                        }
                    }
                    if (isTrue)
                    {
                        object _objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                        if (_objCARTONINVOICES != null)
                        {
                            CARTONINVOICES cARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                            cARTONINVOICES.STATUS = "ClosePack";
                            cARTONINVOICES.FDATE = mDate;
                            cARTONINVOICES.FTIME = mTime;
                            cARTONINVOICES.MUSER = mUser;
                            cARTONINVOICES.MDATE = mDate;
                            cARTONINVOICES.MTIME = mTime;
                            this._WarehouseFacade.UpdateTBLCartonInvoices(cARTONINVOICES);


                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = string.Empty;
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (objPick as Pick).InvNo;//.InvNo;
                            trans.InvType = (objPick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = this.GetUserCode();
                            trans.MCode = string.Empty;
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = (objPick as Pick).StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = (objPick as Pick).PickNo; // asnIqc.IqcNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);
                        }
                    }
                }

                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "保存成功", this.languageComponent1);
                this.gridHelper.RefreshData();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, "保存失败：" + ex.Message, this.languageComponent1);
            }
        }

        protected void cmdPackingFinished_ServerClick(object sender, System.EventArgs e)
        {
            //if (!ValidateInput())
            //{
            //    return;
            //}
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);
            string mUser = this.GetUserCode();
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            string pickNo = this.txtPickNoQuery.Text.Trim().ToUpper();
            object obj = _WarehouseFacade.GetCartonInvoices(pickNo);



            if (obj == null)
            {
                WebInfoPublish.Publish(this, "当前拣货任务令号没有对应的发货箱单信息", this.languageComponent1);
                return;
            }


            CARTONINVOICES carvoice = obj as CARTONINVOICES;
            if (carvoice.STATUS != "Release" && carvoice.STATUS != "Pack")
            {
                WebInfoPublish.Publish(this, "箱单状态必须是初始化或者包装中才能操作！", this.languageComponent1);
                return;
            }

            Pick pick = (Pick)_WarehouseFacade.GetPick(pickNo);
            //请先完成拣料
            if (pick != null)
            {
                if (!(pick.Status == PickHeadStatus.PickHeadStatus_Pack || pick.Status == PickHeadStatus.PickHeadStatus_MakePackingList))
                {
                    WebInfoPublish.Publish(this, "请先完成拣料", this.languageComponent1);
                    return;
                }
            }
            else
            {
                WebInfoPublish.Publish(this, "拣料任务令不存在", this.languageComponent1);
                return;
            }

            try
            {
                this.DataProvider.BeginTransaction();

                //1. 当前包装箱号在发货箱单明细信息表(TBLCartonInvDetail)中的状态更新为：ClosePack:包装完成
                _WarehouseFacade.UpdateCartoninvdetailByCARINVNO((obj as CARTONINVOICES).CARINVNO, CartonInvoices_STATUS.Status_ClosePack);
                //object _obj = this._WarehouseFacade.GetCartonInvDetail((obj as CARTONINVOICES).CARINVNO, (obj as CARTONINVOICES).CARINVNO);//this.txtCartonNo.Text.Trim().ToUpper()
                //if (_obj == null)
                //{
                //    WebInfoPublish.Publish(this, "当前包装箱号没有对应的发货箱单明细信息", this.languageComponent1);
                //    return;
                //}

                //CartonInvDetail cartonInvDetail = _obj as CartonInvDetail;
                //cartonInvDetail.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                //cartonInvDetail.MUSER = mUser;
                //cartonInvDetail.MDATE = mDate;
                //cartonInvDetail.MTIME = mTime;
                //this._WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);

                //2. 检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] objs = this._WarehouseFacade.GetPickLineByPickNoNotCancel(pickNo);
                if (objs == null)
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, "当前拣货任务令号没有对应的拣货任务令明细信息", this.languageComponent1);
                    return;
                }

                bool isTrue = true;
                foreach (PickDetail pickDetail in objs)
                {
                    if (pickDetail.SQTY != pickDetail.PQTY)
                    {
                        isTrue = false;
                        break;
                    }
                }
                if (isTrue)
                {
                    CARTONINVOICES _CARTONINVOICES = obj as CARTONINVOICES;
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                    _CARTONINVOICES.FDATE = mDate;
                    _CARTONINVOICES.FTIME = mTime;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }
                else
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, "当前发货箱单未包装完成", this.languageComponent1);
                    return;
                }


                #region 在invinouttrans表中增加一条数据
                WarehouseFacade facade = new WarehouseFacade(base.DataProvider);


                DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                foreach (PickDetail pickDetail in objs)
                {
                    InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                    trans.CartonNO = string.Empty;
                    trans.DqMCode = pickDetail.DQMCode;
                    trans.FacCode = string.Empty;
                    trans.FromFacCode = string.Empty;
                    trans.FromStorageCode = string.Empty;
                    trans.InvNO = pick.InvNo;//.InvNo;
                    trans.InvType = pick.PickType;
                    trans.LotNo = string.Empty;
                    trans.MaintainDate = dbTime1.DBDate;
                    trans.MaintainTime = dbTime1.DBTime;
                    trans.MaintainUser = this.GetUserCode();
                    trans.MCode = pickDetail.MCode;
                    trans.ProductionDate = 0;
                    trans.Qty = pickDetail.QTY;
                    trans.Serial = 0;
                    trans.StorageAgeDate = 0;
                    trans.StorageCode = pick.StorageCode;
                    trans.SupplierLotNo = string.Empty;
                    trans.TransNO = pickDetail.PickNo; // asnIqc.IqcNo;
                    trans.TransType = "OUT";
                    trans.Unit = string.Empty;
                    trans.ProcessType = "ClosePack";
                    facade.AddInvInOutTrans(trans);
                }

                #endregion

                foreach (PickDetail pickDetail in objs)
                {
                    pickDetail.Status = PickDetail_STATUS.Status_ClosePack;
                    pickDetail.MaintainUser = mUser;
                    pickDetail.MaintainDate = mDate;
                    pickDetail.MaintainTime = mTime;
                    _WarehouseFacade.UpdatePickdetail(pickDetail);
                }

                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "装箱成功", this.languageComponent1);
                this.gridHelper.RefreshData();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, "装箱失败：" + ex.Message, this.languageComponent1);
            }
        }

        #endregion

        #region Object <--> Page

        protected void BindDQMaterialNO()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            string pickNo = this.txtPickNoQuery.Text.Trim().ToUpper();
            //绑定鼎桥物料编码
            string[] str = _WarehouseFacade.QueryDQMaterialNO(pickNo);
            this.ddlDQMaterialNO.Items.Clear();
            if (str != null)
            {
                this.ddlDQMaterialNO.Items.Add(string.Empty);
                foreach (string s in str)
                {
                    this.ddlDQMaterialNO.Items.Add(s);
                }
            }
            else
            {
                this.ddlDQMaterialNO.Items.Add(string.Empty);
            }
            this.ddlDQMaterialNO.SelectedIndex = 0;
        }

        protected void ddlDQMaterialNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            string dqMCode = this.ddlDQMaterialNO.SelectedValue;
            object[] mar_objs = _WarehouseFacade.GetMaterialInfoByQDMCode(dqMCode);
            if (mar_objs == null)
            {
                WebInfoPublish.Publish(this, "无效的鼎桥物料号", this.languageComponent1);
                return;
            }
            Domain.MOModel.Material mar = mar_objs[0] as Domain.MOModel.Material;
            if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)  //单件管控
            {
                this.txtQTY.Enabled = false;
                this.txtQTY.Text = string.Empty;
                this.txtSNEdit.Enabled = true;
            }
            else
            {
                this.txtQTY.Enabled = true;
                this.txtSNEdit.Enabled = false;
                this.txtSNEdit.Text = string.Empty;
            }
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblPickNoQuery, this.txtPickNoQuery, 40, true));
            manager.Add(new LengthCheck(this.lblDQMaterialNO, this.ddlDQMaterialNO, 40, false));
            manager.Add(new LengthCheck(this.lblPackingCartonNo, this.txtCartonNo, 40, true));
            manager.Add(new LengthCheck(this.lblSNEdit, this.txtSNEdit, 40, false));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                ((PackagingOperations)obj).CARTONNO,
                ((PackagingOperations)obj).DQMCODE,
                ((PackagingOperations)obj).CustMCode,
                ((PackagingOperations)obj).SQTY.ToString(),
                ((PackagingOperations)obj).QTY.ToString()
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
                "IT_CartonNo",
                "DQMCode",
                "CusMCode",
                "PickedQTY",
                "PackingQTY"
            };
        }

        #endregion


        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {



            string page = Request.QueryString["Page"];
            Response.Redirect(this.MakeRedirectUrl(page,
                                 new string[] { "PickNo", "CARINVNO" },
                                 new string[] { txtPickNoQuery.Text.Trim().ToUpper(),
                                         txtCarInvNoQuery.Text.Trim().ToUpper()
                                        
                                    }));

        }




    }
}
