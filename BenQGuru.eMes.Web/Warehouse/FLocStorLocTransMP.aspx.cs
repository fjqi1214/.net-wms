using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using Infragistics.WebUI.UltraWebGrid;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FLocStorLocTransMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private WarehouseFacade _WarehouseFacade = null;
        private WarehouseFacade _TransferFacade;
        SystemSettingFacade _SystemSettingFacade = null;
        private InventoryFacade facade = null;
        private UserFacade _UserFacade = null;
        private InventoryFacade _InventoryFacade = null;
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
            //this.gridWebGrid.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid_InitializeRow);
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
            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                LoadStatusList();
                InitWebGrid();
                //this.cmdQuery_Click(null, null);
                //this.RequestData();


                if (!string.IsNullOrEmpty(Request.QueryString["LOCATIONNO"]))
                {
                    this.txtLocationNoQuery.Text = Request.QueryString["LOCATIONNO"];
                    this.cmdQuery_Click(null, null);
                    this.RequestData();
                }

            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region 默认查询
        private void RequestData()
        {

            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }
        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);


            #region Exporter
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this.excelExporter.Page = this;
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
            #endregion

        }
        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        #endregion

        #region 下拉框
        private void LoadStatusList()
        {
            StatusList.Items.Clear();
            StatusList.Items.Add(new ListItem("拆箱", "SplitCarton"));
            StatusList.Items.Add(new ListItem("整箱", "AllCarton"));
            #region
            //PickHeadStatus status = new PickHeadStatus();
            //生成枚举下维护的值的单选
            //foreach (string item in status.Items)
            //{
            //    if (item == PickHeadStatus.PickHeadStatus_Close)
            //    {
            //        StatusList.Items.Add(new ListItem(this.languageComponent1.GetString("PickHeadStatus_Close"), item));
            //    }
            //    else
            //    {
            //        StatusList.Items.Add(new ListItem(this.languageComponent1.GetString(item), item)); //值为枚举值，显示文本从多语言文件获取
            //    }
            //}
            #endregion 设定默认选择
            StatusList.SelectedIndex = 0;
        }

        protected void RadioButtonStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 需求数量：输入框，选中整箱单选按钮时该输入框不可用
            //SN：输入框，选中整箱单选按钮时该输入框不可用
            if (StatusList.SelectedValue == "AllCarton")
            {
                txtNumEdit.Text = "";
                txtSNEdit.Text = "";
                txtNumEdit.ReadOnly = true;
                txtSNEdit.ReadOnly = true;
            }
            else
            {
                txtNumEdit.ReadOnly = false;
                txtSNEdit.ReadOnly = false;
            }
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("LocationTransNo", "货位移动单号", null);
            this.gridHelper.AddColumn("DQMcode", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("InvNo", "SAP单据号", null);
            this.gridHelper.AddColumn("HasReMoveQty", "已移动数量", null);
            this.gridHelper.AddColumn("Unit", "单位", null);
            this.gridHelper.AddColumn("TransUser", "拣料人", null);
            this.gridHelper.AddColumn("TransTime", "拣料时间", null);

            this.gridHelper.AddColumn("FromLocation", "原货位", null);
            this.gridHelper.AddColumn("Location", "目标货位", null);
            this.gridHelper.AddColumn("FCartonNo", "原箱号条码", null);
            this.gridHelper.AddColumn("Cartonno", "目标箱号条码", null);

            this.gridHelper.AddColumn("TransNo", "货位移动单号", null);

            this.gridHelper.AddColumn("HWMcode", "华为物料编码", null);
            this.gridHelper.AddLinkColumn("SNDetail", "SN详情", null);

            this.gridHelper.AddLinkColumn("CartonInfo", "箱号信息", null);
            this.gridWebGrid.Columns.FromKey("LocationTransNo").Hidden = true;


            this.gridWebGrid.Columns.FromKey("TransNo").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("FCartonNo").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("Cartonno").Hidden = true;
            this.gridWebGrid.Columns.FromKey("HWMcode").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, false);
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["LocationTransNo"] = ((StorLocTransOperations)obj).Transno;
            row["DQMcode"] = ((StorLocTransOperations)obj).DqmCode;
            row["InvNo"] = ((StorLocTransOperations)obj).InvNo;
            row["HasReMoveQty"] = ((StorLocTransOperations)obj).Qty;
            row["Unit"] = ((StorLocTransOperations)obj).Unit;
            row["TransUser"] = ((StorLocTransOperations)obj).CUser;
            row["TransTime"] = FormatHelper.ToTimeString(((StorLocTransOperations)obj).CTime);

            row["FromLocation"] = ((StorLocTransOperations)obj).FromlocationCode;

            row["Location"] = ((StorLocTransOperations)obj).LocationCode;
            row["FCartonNo"] = ((StorLocTransOperations)obj).Fromcartonno;
            row["Cartonno"] = ((StorLocTransOperations)obj).Cartonno;
            //row["TransDate"] = FormatHelper.ToDateString(((Storloctrans)obj).CDate);
            row["TransNo"] = ((StorLocTransOperations)obj).Transno;

            row["HWMcode"] = ((StorLocTransOperations)obj).MCode;

            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            return this.facade.QueryLocStorTransQueryDetail(
           FormatHelper.CleanString(this.txtLocationNoQuery.Text),
            FormatHelper.CleanString(this.txtInvNoQuery.Text),
           inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            return this.facade.QueryLocStorTransDetailCount(
                  FormatHelper.CleanString(this.txtLocationNoQuery.Text),
              FormatHelper.CleanString(this.txtInvNoQuery.Text)
                  );
        }

        #endregion

        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            string transNo = row.Items.FindItemByKey("TransNo").Text.Trim();
            string fcartonNo = row.Items.FindItemByKey("FCartonNo").Text.Trim();
            string tcartonNo = row.Items.FindItemByKey("Cartonno").Text.Trim();
            string dqMcode = row.Items.FindItemByKey("DQMcode").Text.Trim();
            string mcode = row.Items.FindItemByKey("HWMcode").Text.Trim();
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);
                if (obj != null)
                {
                    this.SetEditObject(obj);
                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
            else if (commandName == "CartonInfo")
            {
                Response.Redirect(this.MakeRedirectUrl("FLocationTransDetailMP.aspx", new string[] { "LOCATIONNO", "Page" }, new string[] { transNo, "FLocStorLocTransMP.aspx" }));
            }
            else if (commandName == "SNDetail")
            {


                //PIS未看到具体页面（参照转储）
                Response.Redirect(this.MakeRedirectUrl("FLocationTransDetailSN.aspx", new string[] { "LOCATIONNO", "FCARTON", "TCARTON", "Page" }, new string[] { transNo, fcartonNo, tcartonNo, "FLocStorLocTransMP.aspx" }));


                ;
            }
            // 箱号信息：点击跳入图6.13.2：（1）页面
            //SN信息：点击跳转到图6.13.2：（2）页面
        }
        private bool IsInt(string obj)
        {
            try
            {
                int.Parse(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool IsDecimal(string obj)
        {
            try
            {
                decimal.Parse(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #region Button
        //提交
        protected void cmdCommit_Click2(object sender, EventArgs e)
        {
            AddStorLoc();
            #region 转储功能
            /* 一 根据箱号到TBLStorageDetail查找mcode，根据（转单号，mcode）查找TBLStorLocTransDetail 中数据，如果没有则报错：转储单中没有对应的SAP物料号
             二  判断（CartonNO对应的MCode），（TransNo）在TBLStorLocTransDetail中的状态是否为Close:完成，如果是提示该料转储已经完成，如果是Release（初始化），更新状态为（Pick，拣料中） 。
              【整箱】：
            1，	检查TBLStorageDetail.FreezeQTY是否为零？不为零，检查是否TBLStorageDetail.FreezeQTY= TBLStorageDetail. STORAGEQTY?如果是：提示此箱在拣料中；
             * 如果不是提示此箱SN部分拣料中，请拆箱拣料。
            2，	更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. STORAGEQTY，TBLStorageDetail. AvailableQTY=0。
            3，	更新TBLStorageDetailSN. PICKBLOCK=Y。
            4，	向TBLStorLocTransDetailCarton插入一笔数据。*/
            // 提交按钮：（参考转储功能）
            //5，	其他点请参考转储功能
            #endregion

            #region facade
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            }
            ItemFacade itemFacade = new ItemFacade();
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            #endregion

            #region check
            string transNo = FormatHelper.CleanString(txtLocationNoEdit.Text);

            string tLocationCode = FormatHelper.CleanString(txtTLocationCodeEdit.Text);//目标货位
            string fromCarton = FormatHelper.CleanString(txtOriginalCartonEdit.Text);//原箱号 FromCARTONNO 
            string tcarton = FormatHelper.CleanString(txtTLocationCartonEdit.Text);//目标箱号
            string inputsn = FormatHelper.CleanString(txtSNEdit.Text);//SN
            #endregion

            #region check
            if (string.IsNullOrEmpty(transNo))
            {
                WebInfoPublish.Publish(this, "移转单号不能为空", this.languageComponent1);
                return;
            }
            StorageDetail storageCarton = (StorageDetail)facade.GetStorageDetail(fromCarton);
            if (storageCarton == null)
            {  //A 根据原箱号和数量操作。根据原箱号（cartonno）到TBLStorageDetail中查找数据。没有报错。
                WebInfoPublish.PublishInfo(this, "转储单中没有对应的SAP物料号", this.languageComponent1);
                return;
            }
            string dqmCode = storageCarton.DQMCode;
            if (string.IsNullOrEmpty(dqmCode))
            {
                WebInfoPublish.Publish(this, "鼎桥物料编码不能为空", this.languageComponent1);
                return;
            }

            if (string.IsNullOrEmpty(tLocationCode))
            {
                WebInfoPublish.Publish(this, "目标货位不能为空", this.languageComponent1);
                return;
            }
            if (string.IsNullOrEmpty(tcarton))
            {
                WebInfoPublish.Publish(this, "目标箱号不能为空", this.languageComponent1);
                return;
            }

            Storloctrans storloctrans = (Storloctrans)_WarehouseFacade.GetStorloctrans(transNo);
            if (storloctrans == null)
            {
                WebInfoPublish.Publish(this, "移转单号不存在", this.languageComponent1);
                return;
            }
            #endregion

            try
            {
                this.DataProvider.BeginTransaction();
                #region Check
                //1，	检查TBLStorLocTrans表目标库位是否为空，如果是，从TBLStorageDetail取库位更新；
                StorageDetail oldstorageDetail = (StorageDetail)facade.GetStorageDetail(tcarton);
                if (oldstorageDetail == null)
                {
                    //this.DataProvider.RollbackTransaction();
                    //WebInfoPublish.Publish(this, "目标箱号不存在", this.languageComponent1);
                    //return;
                    StorageDetail newstorageCarton = new StorageDetail();
                    newstorageCarton.LocationCode = storageCarton.LocationCode;
                    newstorageCarton.CartonNo = storageCarton.LocationCode;
                    newstorageCarton.FreezeQty = storageCarton.FreezeQty;
                    newstorageCarton.AvailableQty = storageCarton.AvailableQty;
                    newstorageCarton.StorageQty = storageCarton.StorageQty;

                    oldstorageDetail = storageCarton;
                    oldstorageDetail.LocationCode = tLocationCode;
                    oldstorageDetail.CartonNo = tcarton;
                    oldstorageDetail.FreezeQty = 0;
                    oldstorageDetail.AvailableQty = 0;
                    oldstorageDetail.StorageQty = 0;
                    _WarehouseFacade.AddStorageDetail(oldstorageDetail);

                    storageCarton.LocationCode = newstorageCarton.LocationCode;
                    storageCarton.CartonNo = newstorageCarton.CartonNo;
                    storageCarton.FreezeQty = newstorageCarton.FreezeQty;
                    storageCarton.AvailableQty = newstorageCarton.AvailableQty;
                    storageCarton.StorageQty = newstorageCarton.StorageQty;
                }
                StorageDetail storageDetail = (StorageDetail)facade.GetStorageDetail(tcarton);
                if (string.IsNullOrEmpty(storloctrans.StorageCode))
                {
                    if (storageDetail != null)
                    {
                        storloctrans.StorageCode = storageDetail.StorageCode;
                    }
                    _WarehouseFacade.UpdateStorloctrans(storloctrans);
                }

                #endregion

                #region【整箱】：
                if (StatusList.SelectedValue == CartonType.CartonType_AllCarton)
                {
                    //StorloctransDetail storLocTransDetail=new StorloctransDetail();
                    // 1.检查TBLStorageDetail.FreezeQTY是否为零？不为零，检查是否TBLStorageDetail.FreezeQTY= TBLStorageDetail. STORAGEQTY?
                    //  如果是：提示此箱在拣料中；如果不是提示此箱SN部分拣料中，请拆箱拣料  
                    if (storageDetail.FreezeQty != 0)
                    {
                        if (storageDetail.FreezeQty == storageDetail.StorageQty)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "提示此箱在拣料中", this.languageComponent1);
                            return;
                        }
                        else
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "此箱SN部分拣料中，请拆箱拣料。", this.languageComponent1);
                            return;
                        }
                    }
                    //2，	更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. STORAGEQTY，TBLStorageDetail. AvailableQTY=0。
                    storageDetail.AvailableQty = 0;
                    storageDetail.LocationCode = tLocationCode;//edit by sam
                    facade.UpdateStorageDetail(storageDetail);
                    //4，	每次提交都更新表TBLStorLocTransDetail中的需求数量。
                    StorloctransDetail storloctransDetail = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail(transNo, storageDetail.MCode);
                    storloctransDetail.Qty += storageDetail.AvailableQty;
                    _WarehouseFacade.UpdateStorloctransdetail(storloctransDetail);

                    // StorloctransDetailCarton storObj = (StorloctransDetailCarton)_WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton, tcarton);

                    #region 4，	向TBLStorLocTransDetailCarton插入一笔数据。
                    StorloctransDetailCarton newstorlocDetailCarton = new StorloctransDetailCarton();
                    newstorlocDetailCarton.Transno = transNo;   //storlocDetailCarton.Transno = StorLocTransDetail.TransNo;
                    newstorlocDetailCarton.MCode = storageDetail.MCode;//MCODE：TBLStorLocTransDetail.MCODE
                    newstorlocDetailCarton.DqmCode = dqmCode;
                    newstorlocDetailCarton.FacCode = "10Y2";
                    newstorlocDetailCarton.Qty = storageDetail.StorageQty;//QTY: TBLStorageDetail. storageQTY
                    newstorlocDetailCarton.LocationCode = tLocationCode;//LocationCode: TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                    newstorlocDetailCarton.Cartonno = tcarton;//CARTONNO：PDA页面的目标箱号 txtTLocationCartonEdit
                    newstorlocDetailCarton.FromlocationCode = storageDetail.LocationCode;  //FromLocationCode：TBLStorageDetail.LocationCode
                    newstorlocDetailCarton.Fromcartonno = storageDetail.CartonNo; //FromCARTONNO：TBLStorageDetail. CARTONNO
                    newstorlocDetailCarton.Lotno = storageDetail.Lotno; //LotNo：TBLStorageDetail. LotNo
                    newstorlocDetailCarton.CUser = this.GetUserCode();	//	CUSER
                    newstorlocDetailCarton.CDate = dbDateTime.DBDate;	//	CDATE
                    newstorlocDetailCarton.CTime = dbDateTime.DBTime;//	CTIME
                    newstorlocDetailCarton.MaintainDate = dbDateTime.DBDate;	//	MDATE
                    newstorlocDetailCarton.MaintainTime = dbDateTime.DBTime;	//	MTIME
                    newstorlocDetailCarton.MaintainUser = this.GetUserCode();
                    _WarehouseFacade.AddStorloctransdetailcarton(newstorlocDetailCarton);
                    #endregion
                    //5，	如果原箱号在TBLStorageDetailSN有SN信息，将SN信息插入到TBLStorLocTransDetailSN表
                    object[] storageDetailSnlist = facade.GetStorageDetailSnbyCarton(fromCarton);
                    if (storageDetailSnlist != null)
                    {
                        foreach (StorageDetailSN newStorageDetailSn in storageDetailSnlist)
                        {
                            #region add TBLStorageDetailSN
                            StorloctransDetailSN newStorloctransDetailSn = new StorloctransDetailSN();
                            newStorloctransDetailSn.Sn = newStorageDetailSn.SN;//SN：TBLStorageDetailSN.SN
                            newStorloctransDetailSn.Transno = newStorageDetailSn.SN;    // TBLStorLocTransDetail. TransNo
                            newStorloctransDetailSn.Fromcartonno = storageDetail.LocationCode;//FromCARTONNO：TBLStorageDetail.LocationCode
                            newStorloctransDetailSn.Cartonno = tcarton;//FormatHelper.CleanString(txtTLocationCodeEdit.Text);//CARTONNO：TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                            newStorloctransDetailSn.MaintainDate = dbDateTime.DBDate;	//	MDATE
                            newStorloctransDetailSn.MaintainTime = dbDateTime.DBTime;	//	MTIME
                            newStorloctransDetailSn.MaintainUser = this.GetUserCode();
                            _WarehouseFacade.AddStorloctransdetailsn(newStorloctransDetailSn);
                            #endregion
                        }
                    }
                }
                #endregion

                #region【拆箱】：
                else if (StatusList.SelectedValue == CartonType.CartonType_SplitCarton)
                {
                    //根据原箱号和数量操作。根据原箱号（cartonno）到TBLStorageDetail中查找数据。
                    //2.检查录入的箱号或SN号对应的库位是否与TBLStorLocTrans表中的库位一致，
                    //不一致提示错误：源箱号所在库位与货位移动单号目标库位不一致(TBLStorageDetailSN )

                    if (storloctrans.StorageCode != storageCarton.StorageCode)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "源箱号所在库位与货位移动单号目标库位不一致", this.languageComponent1);
                        return;
                    }

                    //1，	无论输入是【原箱号】，【SN】，首先判断管控类型，如果是单件管控，已输入SN为条件进行录入；如果是批管控或不管控则以【原箱号】和【数量】为录入条件。
                    Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(storageCarton.MCode);
                    if (material != null)
                    {
                        //2单件管控IsInt
                        if (material.MCONTROLTYPE == BOMItemControlType.ITEM_CONTROL_KEYPARTS)
                        {
                            #region	单件管控：
                            StorageDetailSN storageDetailSn = (StorageDetailSN)facade.GetStorageDetailSN(inputsn);
                            fromCarton = storageDetailSn.CartonNo;
                            StorageDetail storageCartonSn = (StorageDetail)facade.GetStorageDetail(fromCarton);
                            if (storloctrans.StorageCode != storageCartonSn.StorageCode)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "源箱号所在库位与货位移动单号目标库位不一致", this.languageComponent1);
                                return;
                            }

                            #region add by sam 2016年4月27日
                            storageDetailSn.CartonNo = tcarton;
                            facade.UpdateStorageDetailSN(storageDetailSn);

                            storageCartonSn.StorageQty -= 1;
                            storageCartonSn.AvailableQty -= 1;
                            facade.UpdateStorageDetail(storageCartonSn);
                            storageDetail.StorageQty += 1;
                            storageDetail.AvailableQty += 1;
                            facade.UpdateStorageDetail(storageDetail);
                            #endregion
                            //B 根据SN在TBLStorageDetail中查找mcode信息，再根据（mcode+TransNo）信息在TBLStorLocTransDetail中是否有数据，
                            //如果没有则报错：转储单中没有对应的SAP物料号。
                            StorloctransDetail storloctransdetailObj = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail(transNo, storageCartonSn.MCode);
                            if (storloctransdetailObj == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "转储单中没有对应的SAP物料号", this.languageComponent1);
                                return;
                            }
                            //storageDetailSn.PickBlock = "Y";
                            //D 更新TBLStorageDetailSN. PICKBLOCK=Y。
                            //E  需要输入目标箱号，根据SN信息在TBLStorageDetailSN中查找原箱号信息；
                            //根据（目标箱号+TransNo+FromCARTONNO原箱号）在TBLStorLocTransDetailCarton是否有记录。 inputCarton
                            //storageDetailSn.CartonNo
                            StorloctransDetailCarton storloctransDetailCarton =
                                (StorloctransDetailCarton)
                                _WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton,
                                                                             storageDetailSn.CartonNo);
                            if (storloctransDetailCarton != null)
                            {// 如果有： 更新TBLStorLocTransDetailCarton.QTY+ PDA页面填的数量，MDate，MTime，MUser。
                                storloctransDetailCarton.Qty += 1;
                                storloctransDetailCarton.MaintainDate = dbDateTime.DBDate;
                                storloctransDetailCarton.MaintainTime = dbDateTime.DBTime;
                                storloctransDetailCarton.MaintainUser = this.GetUserCode();
                                _WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                            }
                            else
                            {
                                #region 4，	向TBLStorLocTransDetailCarton插入一笔数据。
                                StorloctransDetailCarton newstorlocDetailCarton = new StorloctransDetailCarton();
                                newstorlocDetailCarton.Transno = storloctransdetailObj.Transno;   //storlocDetailCarton.Transno = StorLocTransDetail.TransNo;
                                newstorlocDetailCarton.DqmCode = storloctransdetailObj.DqmCode;
                                newstorlocDetailCarton.FacCode = "10Y2";
                                newstorlocDetailCarton.MCode = storloctransdetailObj.MCode;//MCODE：TBLStorLocTransDetail.MCODE
                                newstorlocDetailCarton.Qty = 1;//   //QTY: 1
                                newstorlocDetailCarton.LocationCode = tLocationCode;// FormatHelper.CleanString(txtTLocationCodeEdit.Text);//LocationCode: TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                                newstorlocDetailCarton.Cartonno = tcarton;// FormatHelper.CleanString(txtTLocationCartonEdit.Text);//CARTONNO：PDA页面的目标箱号 txtTLocationCartonEdit
                                newstorlocDetailCarton.FromlocationCode = storageCartonSn.LocationCode;  //FromLocationCode：TBLStorageDetail.LocationCode
                                newstorlocDetailCarton.Fromcartonno = fromCarton;// rageDetail.CartonNo; //FromCARTONNO：TBLStorageDetail. CARTONNO
                                newstorlocDetailCarton.Lotno = storageDetail.Lotno; //LotNo：TBLStorageDetail. LotNo
                                newstorlocDetailCarton.CUser = this.GetUserCode();	//	CUSER
                                newstorlocDetailCarton.CDate = dbDateTime.DBDate;	//	CDATE
                                newstorlocDetailCarton.CTime = dbDateTime.DBTime;//	CTIME
                                newstorlocDetailCarton.MaintainDate = dbDateTime.DBDate;	//	MDATE
                                newstorlocDetailCarton.MaintainTime = dbDateTime.DBTime;	//	MTIME
                                newstorlocDetailCarton.MaintainUser = this.GetUserCode();
                                _WarehouseFacade.AddStorloctransdetailcarton(newstorlocDetailCarton);
                                #endregion

                                //说明：表中没有的字段来自TBLStorageDetail中对应字段
                                //F  向表TBLStorLocTransDetailSN插入一条数据。
                                object[] storageDetailSnlist = facade.GetStorageDetailSnbyCarton(storageDetail.CartonNo);

                                #region add TBLStorageDetailSN
                                StorloctransDetailSN newStorageDetailSn =
                                    storageDetailSnlist[0] as StorloctransDetailSN;
                                if (newStorageDetailSn != null)
                                {
                                    StorloctransDetailSN newStorloctransDetailSn = new StorloctransDetailSN();
                                    newStorloctransDetailSn.Sn = newStorageDetailSn.Sn;//SN：TBLStorageDetailSN.SN
                                    newStorloctransDetailSn.Transno = storloctransdetailObj.Transno; // TBLStorLocTransDetail. TransNo
                                    newStorloctransDetailSn.Fromcartonno = storageDetail.LocationCode;//FromCARTONNO：TBLStorageDetail.LocationCode
                                    newStorloctransDetailSn.Cartonno = tcarton;// FormatHelper.CleanString(txtTLocationCodeEdit.Text);//CARTONNO：TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                                    newStorloctransDetailSn.MaintainDate = dbDateTime.DBDate;	//	MDATE
                                    newStorloctransDetailSn.MaintainTime = dbDateTime.DBTime;	//	MTIME
                                    newStorloctransDetailSn.MaintainUser = this.GetUserCode();
                                    _WarehouseFacade.AddStorloctransdetailsn(newStorloctransDetailSn);
                                }
                                #endregion

                            }
                            #endregion
                        }
                        else
                        {
                            #region 和非单件管控
                            //StorloctransDetail storloctransdetailObj = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail
                            //   (storageCarton.MCode, transNo);
                            //B 检查TBLStorageDetail. AvailableQTY>PDA页面填的数量。如果否，提示：输入的数量大于库存可用数量。
                            if (!IsInt(txtNumEdit.Text))
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "输入的数量必须为数值型", this.languageComponent1);
                                return;
                            }
                            int qty = int.Parse(txtNumEdit.Text);//数量
                            if (storageCarton.AvailableQty <= qty)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "输入的数量大于库存可用数量SAP物料号", this.languageComponent1);
                                return;
                            }
                            //C 更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. FreezeQTY+ PDA页面填的数量，
                            //TBLStorageDetail. AvailableQTY= TBLStorageDetail.STORAGEQTY-TBLStorageDetail. FreezeQTY。
                            //storageCarton.CartonNo = tcarton;
                            //storageCarton.FreezeQty += qty;
                            // storageCarton.AvailableQty = storageCarton.StorageQty - storageCarton.FreezeQty;
                            //storageCarton.LocationCode = tLocationCode;

                            storageCarton.AvailableQty -= qty;
                            storageCarton.StorageQty -= qty;
                            facade.UpdateStorageDetail(storageCarton);

                            storageDetail.StorageQty += qty;
                            storageDetail.AvailableQty += qty;
                            storageDetail.LocationCode = tLocationCode;
                            facade.UpdateStorageDetail(storageDetail);

                            StorloctransDetailCarton storloctransDetailCarton =
                            (StorloctransDetailCarton)
                            _WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton,
                                                                           tcarton);
                            if (storloctransDetailCarton != null)
                            {// 如果有： 更新TBLStorLocTransDetailCarton.QTY+ PDA页面填的数量，MDate，MTime，MUser。
                                storloctransDetailCarton.Qty += qty;// int.Parse(txtNumEdit.Text);
                                storloctransDetailCarton.MaintainDate = dbDateTime.DBDate;
                                storloctransDetailCarton.MaintainTime = dbDateTime.DBTime;
                                storloctransDetailCarton.MaintainUser = this.GetUserCode();
                                _WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                            }
                            else
                            {
                                #region 如果没有：插入一条数据 	向TBLStorLocTransDetailCarton插入一笔数据。
                                StorloctransDetailCarton newstorlocDetailCarton = new StorloctransDetailCarton();
                                newstorlocDetailCarton.Transno = transNo;// storloctransdetailObj.Transno;
                                newstorlocDetailCarton.DqmCode = dqmCode;// storloctransdetailObj.DqmCode;
                                newstorlocDetailCarton.FacCode = "10Y2";
                                newstorlocDetailCarton.MCode = storageCarton.MCode;// storloctransdetailObj.MCode;//MCODE：TBLStorLocTransDetail.MCODE
                                newstorlocDetailCarton.Qty = qty; //QTY: 1
                                newstorlocDetailCarton.LocationCode = tLocationCode;//FormatHelper.CleanString(txtTLocationCodeEdit.Text);//LocationCode: TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                                newstorlocDetailCarton.Cartonno = tcarton;// FormatHelper.CleanString(txtTLocationCartonEdit.Text);//CARTONNO：PDA页面的目标箱号 txtTLocationCartonEdit
                                newstorlocDetailCarton.FromlocationCode = storageCarton.LocationCode;  //FromLocationCode：TBLStorageDetail.LocationCode
                                newstorlocDetailCarton.Fromcartonno = fromCarton;// storageDetail.CartonNo; //FromCARTONNO：TBLStorageDetail. CARTONNO
                                newstorlocDetailCarton.Lotno = storageCarton.Lotno; //LotNo：TBLStorageDetail. LotNo
                                newstorlocDetailCarton.CUser = this.GetUserCode();	//	CUSER
                                newstorlocDetailCarton.CDate = dbDateTime.DBDate;	//	CDATE
                                newstorlocDetailCarton.CTime = dbDateTime.DBTime;//	CTIME
                                newstorlocDetailCarton.MaintainDate = dbDateTime.DBDate;	//	MDATE
                                newstorlocDetailCarton.MaintainTime = dbDateTime.DBTime;	//	MTIME
                                newstorlocDetailCarton.MaintainUser = this.GetUserCode();
                                _WarehouseFacade.AddStorloctransdetailcarton(newstorlocDetailCarton);
                                #endregion
                            }
                            #endregion
                        }

                    }
                }
                #endregion

                //货位移动单据号
                string date = dbDateTime.DBDate.ToString().Substring(2, 6);
                string documentno = CreateAutoDocmentsNo(date);
                SaveDocmentsNo(documentno);
                txtLocationNoEdit.Text = documentno;

                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "提交成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }

        //提交
        protected void cmdCommit_Click(object sender, EventArgs e)
        {
            AddStorLoc();

            #region facade
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            }
            ItemFacade itemFacade = new ItemFacade();
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            #endregion

            #region check
            string transNo = FormatHelper.CleanString(txtLocationNoEdit.Text);

            string tLocationCode = " ";// FormatHelper.CleanString(txtTLocationCodeEdit.Text);//目标货位
            string fromCarton = FormatHelper.CleanString(txtOriginalCartonEdit.Text);//原箱号 FromCARTONNO 
            string tcarton = "Move";// FormatHelper.CleanString(txtTLocationCartonEdit.Text);//目标箱号
            string inputsn = FormatHelper.CleanString(txtSNEdit.Text);//SN
            #endregion

            string inputqty = txtNumEdit.Text.Trim();
            #region check
            if (string.IsNullOrEmpty(transNo))
            {
                WebInfoPublish.Publish(this, "移转单号不能为空", this.languageComponent1);
                return;
            }
            StorageDetail storageCarton = (StorageDetail)facade.GetStorageDetail(fromCarton);
            if (storageCarton == null)
            {  //A 根据原箱号和数量操作。根据原箱号（cartonno）到TBLStorageDetail中查找数据。没有报错。
                WebInfoPublish.PublishInfo(this, "原箱号不存在", this.languageComponent1);
                return;
            }
            string dqmCode = storageCarton.DQMCode;
            if (string.IsNullOrEmpty(dqmCode))
            {
                WebInfoPublish.Publish(this, "鼎桥物料编码不能为空", this.languageComponent1);
                return;
            }
            Storloctrans storloctrans = (Storloctrans)_WarehouseFacade.GetStorloctrans(transNo);
            if (storloctrans == null)
            {
                WebInfoPublish.Publish(this, "移转单号不存在", this.languageComponent1);
                return;
            }
            #endregion

            try
            {
                this.DataProvider.BeginTransaction();



                #region【整箱】：
                if (StatusList.SelectedValue == CartonType.CartonType_AllCarton)
                {
                    // 1.检查TBLStorageDetail.FreezeQTY是否为零？不为零，检查是否TBLStorageDetail.FreezeQTY= TBLStorageDetail. STORAGEQTY?
                    //  如果是：提示此箱在拣料中；如果不是提示此箱SN部分拣料中，请拆箱拣料  
                    if (storageCarton.FreezeQty != 0)
                    {

                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "提示此箱在拣料中", this.languageComponent1);
                        return;

                    }
                    if (_WarehouseFacade.GetStorageDetailSNPickBlockCount(fromCarton) > 0)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "提示此箱在拣料中", this.languageComponent1);
                        return;
                    }


                    //4，	每次提交都更新表TBLStorLocTransDetail中的需求数量。
                    StorloctransDetail storloctransDetail = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail(transNo, storageCarton.MCode);
                    storloctransDetail.Qty += storageCarton.AvailableQty;
                    _WarehouseFacade.UpdateStorloctransdetail(storloctransDetail);


                    // StorloctransDetailCarton storObj = (StorloctransDetailCarton)_WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton, tcarton);

                    #region 4，	向TBLStorLocTransDetailCarton插入一笔数据。
                    StorloctransDetailCarton newstorlocDetailCarton = new StorloctransDetailCarton();
                    newstorlocDetailCarton.Transno = transNo;   //storlocDetailCarton.Transno = StorLocTransDetail.TransNo;
                    newstorlocDetailCarton.MCode = storageCarton.MCode;//MCODE：TBLStorLocTransDetail.MCODE
                    newstorlocDetailCarton.DqmCode = dqmCode;
                    newstorlocDetailCarton.FacCode = "10Y2";
                    newstorlocDetailCarton.Qty = storageCarton.StorageQty;//QTY: TBLStorageDetail. storageQTY
                    newstorlocDetailCarton.LocationCode = tLocationCode;//LocationCode: TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                    newstorlocDetailCarton.Cartonno = tcarton;//CARTONNO：PDA页面的目标箱号 txtTLocationCartonEdit
                    newstorlocDetailCarton.FromlocationCode = storageCarton.LocationCode;  //FromLocationCode：TBLStorageDetail.LocationCode
                    newstorlocDetailCarton.Fromcartonno = storageCarton.CartonNo; //FromCARTONNO：TBLStorageDetail. CARTONNO
                    newstorlocDetailCarton.Lotno = storageCarton.Lotno; //LotNo：TBLStorageDetail. LotNo
                    newstorlocDetailCarton.CUser = this.GetUserCode();	//	CUSER
                    newstorlocDetailCarton.CDate = dbDateTime.DBDate;	//	CDATE
                    newstorlocDetailCarton.CTime = dbDateTime.DBTime;//	CTIME
                    newstorlocDetailCarton.MaintainDate = dbDateTime.DBDate;	//	MDATE
                    newstorlocDetailCarton.MaintainTime = dbDateTime.DBTime;	//	MTIME
                    newstorlocDetailCarton.MaintainUser = this.GetUserCode();
                    _WarehouseFacade.AddStorloctransdetailcarton(newstorlocDetailCarton);
                    #endregion
                    //5，	如果原箱号在TBLStorageDetailSN有SN信息，将SN信息插入到TBLStorLocTransDetailSN表
                    object[] storageDetailSnlist = facade.GetStorageDetailSnbyCarton(fromCarton);
                    if (storageDetailSnlist != null)
                    {
                        foreach (StorageDetailSN newStorageDetailSn in storageDetailSnlist)
                        {
                            #region add TBLStorageDetailSN
                            StorloctransDetailSN newStorloctransDetailSn = new StorloctransDetailSN();
                            newStorloctransDetailSn.Sn = newStorageDetailSn.SN;//SN：TBLStorageDetailSN.SN
                            newStorloctransDetailSn.Transno = transNo;    // TBLStorLocTransDetail. TransNo
                            newStorloctransDetailSn.Fromcartonno = fromCarton;//storageDetail.LocationCode;//FromCARTONNO：TBLStorageDetail.LocationCode
                            newStorloctransDetailSn.Cartonno = tcarton;//FormatHelper.CleanString(txtTLocationCodeEdit.Text);//CARTONNO：TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                            newStorloctransDetailSn.MaintainDate = dbDateTime.DBDate;	//	MDATE
                            newStorloctransDetailSn.MaintainTime = dbDateTime.DBTime;	//	MTIME
                            newStorloctransDetailSn.MaintainUser = this.GetUserCode();
                            _WarehouseFacade.AddStorloctransdetailsn(newStorloctransDetailSn);
                            #endregion
                        }
                    }

                    storageCarton.StorageQty = 0;
                    //2，	更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. STORAGEQTY，TBLStorageDetail. AvailableQTY=0。
                    storageCarton.AvailableQty = 0;
                    //storageCarton.LocationCode = tLocationCode;//edit by sam
                    facade.UpdateStorageDetail(storageCarton);
                }
                #endregion

                #region【拆箱】：
                else if (StatusList.SelectedValue == CartonType.CartonType_SplitCarton)
                {

                    //if (storloctrans.StorageCode != storageCarton.StorageCode)
                    //{
                    //    this.DataProvider.RollbackTransaction();
                    //    WebInfoPublish.Publish(this, "源箱号所在库位与货位移动单号目标库位不一致", this.languageComponent1);
                    //    return;
                    //}

                    //1，	无论输入是【原箱号】，【SN】，首先判断管控类型，如果是单件管控，已输入SN为条件进行录入；如果是批管控或不管控则以【原箱号】和【数量】为录入条件。
                    Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(storageCarton.MCode);
                    if (material != null)
                    {
                        //2单件管控IsInt
                        if (material.MCONTROLTYPE == BOMItemControlType.ITEM_CONTROL_KEYPARTS)
                        {
                            #region	单件管控：
                            StorageDetailSN storageDetailSn = (StorageDetailSN)facade.GetStorageDetailSN(inputsn);
                            fromCarton = storageDetailSn.CartonNo;
                            StorageDetail storageCartonSn = (StorageDetail)facade.GetStorageDetail(fromCarton);
                            if (storloctrans.StorageCode != storageCartonSn.StorageCode)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "源箱号所在库位与货位移动单号目标库位不一致", this.languageComponent1);
                                return;
                            }

                            #region add by sam 2016年4月27日
                            storageDetailSn.CartonNo = tcarton;
                            facade.UpdateStorageDetailSN(storageDetailSn);

                            storageCartonSn.StorageQty -= 1;
                            storageCartonSn.AvailableQty -= 1;

                            facade.UpdateStorageDetail(storageCartonSn);
                            //storageCarton.StorageQty -= 1;
                            //storageCarton.AvailableQty -= 1;
                            //facade.UpdateStorageDetail(storageCarton);
                            #endregion
                            //B 根据SN在TBLStorageDetail中查找mcode信息，再根据（mcode+TransNo）信息在TBLStorLocTransDetail中是否有数据，
                            //如果没有则报错：转储单中没有对应的SAP物料号。
                            StorloctransDetail storloctransdetailObj = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail(transNo, storageCartonSn.MCode);
                            if (storloctransdetailObj == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "转储单中没有对应的SAP物料号", this.languageComponent1);
                                return;
                            }

                            StorloctransDetailCarton storloctransDetailCarton =
                                (StorloctransDetailCarton)
                                _WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton,
                                                                             storageDetailSn.CartonNo);
                            if (storloctransDetailCarton != null)
                            {// 如果有： 更新TBLStorLocTransDetailCarton.QTY+ PDA页面填的数量，MDate，MTime，MUser。
                                storloctransDetailCarton.Qty += 1;
                                storloctransDetailCarton.MaintainDate = dbDateTime.DBDate;
                                storloctransDetailCarton.MaintainTime = dbDateTime.DBTime;
                                storloctransDetailCarton.MaintainUser = this.GetUserCode();
                                _WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                            }
                            else
                            {
                                #region 4，	向TBLStorLocTransDetailCarton插入一笔数据。
                                StorloctransDetailCarton newstorlocDetailCarton = new StorloctransDetailCarton();
                                newstorlocDetailCarton.Transno = storloctransdetailObj.Transno;   //storlocDetailCarton.Transno = StorLocTransDetail.TransNo;
                                newstorlocDetailCarton.DqmCode = storloctransdetailObj.DqmCode;
                                newstorlocDetailCarton.FacCode = "10Y2";
                                newstorlocDetailCarton.MCode = storloctransdetailObj.MCode;//MCODE：TBLStorLocTransDetail.MCODE
                                newstorlocDetailCarton.Qty = 1;//   //QTY: 1
                                newstorlocDetailCarton.LocationCode = tLocationCode;// FormatHelper.CleanString(txtTLocationCodeEdit.Text);//LocationCode: TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                                newstorlocDetailCarton.Cartonno = tcarton;// FormatHelper.CleanString(txtTLocationCartonEdit.Text);//CARTONNO：PDA页面的目标箱号 txtTLocationCartonEdit
                                newstorlocDetailCarton.FromlocationCode = storageCartonSn.LocationCode;  //FromLocationCode：TBLStorageDetail.LocationCode
                                newstorlocDetailCarton.Fromcartonno = fromCarton;// rageDetail.CartonNo; //FromCARTONNO：TBLStorageDetail. CARTONNO
                                newstorlocDetailCarton.Lotno = storageCartonSn.Lotno; //LotNo：TBLStorageDetail. LotNo
                                newstorlocDetailCarton.CUser = this.GetUserCode();	//	CUSER
                                newstorlocDetailCarton.CDate = dbDateTime.DBDate;	//	CDATE
                                newstorlocDetailCarton.CTime = dbDateTime.DBTime;//	CTIME
                                newstorlocDetailCarton.MaintainDate = dbDateTime.DBDate;	//	MDATE
                                newstorlocDetailCarton.MaintainTime = dbDateTime.DBTime;	//	MTIME
                                newstorlocDetailCarton.MaintainUser = this.GetUserCode();
                                _WarehouseFacade.AddStorloctransdetailcarton(newstorlocDetailCarton);
                                #endregion

                                //说明：表中没有的字段来自TBLStorageDetail中对应字段
                                //F  向表TBLStorLocTransDetailSN插入一条数据。
                                object[] storageDetailSnlist = facade.GetStorageDetailSnbyCarton(storageCartonSn.CartonNo);

                                #region add TBLStorageDetailSN
                                StorloctransDetailSN newStorageDetailSn =
                                    storageDetailSnlist[0] as StorloctransDetailSN;
                                if (newStorageDetailSn != null)
                                {
                                    StorloctransDetailSN newStorloctransDetailSn = new StorloctransDetailSN();
                                    newStorloctransDetailSn.Sn = newStorageDetailSn.Sn;//SN：TBLStorageDetailSN.SN
                                    newStorloctransDetailSn.Transno = storloctransdetailObj.Transno; // TBLStorLocTransDetail. TransNo
                                    newStorloctransDetailSn.Fromcartonno = storageCartonSn.CartonNo;//FromCARTONNO：TBLStorageDetail.LocationCode
                                    newStorloctransDetailSn.Cartonno = tcarton;// FormatHelper.CleanString(txtTLocationCodeEdit.Text);//CARTONNO：TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                                    newStorloctransDetailSn.MaintainDate = dbDateTime.DBDate;	//	MDATE
                                    newStorloctransDetailSn.MaintainTime = dbDateTime.DBTime;	//	MTIME
                                    newStorloctransDetailSn.MaintainUser = this.GetUserCode();
                                    _WarehouseFacade.AddStorloctransdetailsn(newStorloctransDetailSn);
                                }
                                #endregion

                            }
                            #endregion
                        }
                        else
                        {
                            #region 和非单件管控
                            if (!IsInt(inputqty))
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "输入的数量必须为数值型", this.languageComponent1);
                                return;
                            }
                            int qty = int.Parse(inputqty);//数量
                            if (storageCarton.AvailableQty <= qty)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "输入的数量大于库存可用数量SAP物料号", this.languageComponent1);
                                return;
                            }
                            //C 更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. FreezeQTY+ PDA页面填的数量，
                            //TBLStorageDetail. AvailableQTY= TBLStorageDetail.STORAGEQTY-TBLStorageDetail. FreezeQTY。
                            //storageCarton.CartonNo = tcarton;
                            //storageCarton.FreezeQty += qty;
                            // storageCarton.AvailableQty = storageCarton.StorageQty - storageCarton.FreezeQty;
                            //storageCarton.LocationCode = tLocationCode;

                            storageCarton.AvailableQty -= qty;
                            storageCarton.StorageQty -= qty;
                            facade.UpdateStorageDetail(storageCarton);

                            //storageDetail.StorageQty += qty;
                            //storageDetail.AvailableQty += qty;
                            //storageDetail.LocationCode = tLocationCode;
                            //facade.UpdateStorageDetail(storageDetail);

                            #region StorloctransDetailCarton
                            StorloctransDetailCarton storloctransDetailCarton =
                                              (StorloctransDetailCarton)
                                              _WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton,
                                                                                             tcarton);
                            if (storloctransDetailCarton != null)
                            {// 如果有： 更新TBLStorLocTransDetailCarton.QTY+ PDA页面填的数量，MDate，MTime，MUser。
                                storloctransDetailCarton.Qty += qty;// int.Parse(txtNumEdit.Text);
                                storloctransDetailCarton.MaintainDate = dbDateTime.DBDate;
                                storloctransDetailCarton.MaintainTime = dbDateTime.DBTime;
                                storloctransDetailCarton.MaintainUser = this.GetUserCode();
                                _WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                            }
                            else
                            {
                                #region 如果没有：插入一条数据 	向TBLStorLocTransDetailCarton插入一笔数据。
                                StorloctransDetailCarton newstorlocDetailCarton = new StorloctransDetailCarton();
                                newstorlocDetailCarton.Transno = transNo;// storloctransdetailObj.Transno;
                                newstorlocDetailCarton.DqmCode = dqmCode;// storloctransdetailObj.DqmCode;
                                newstorlocDetailCarton.FacCode = "10Y2";
                                newstorlocDetailCarton.MCode = storageCarton.MCode;// storloctransdetailObj.MCode;//MCODE：TBLStorLocTransDetail.MCODE
                                newstorlocDetailCarton.Qty = qty; //QTY: 1
                                newstorlocDetailCarton.LocationCode = tLocationCode;//FormatHelper.CleanString(txtTLocationCodeEdit.Text);//LocationCode: TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                                newstorlocDetailCarton.Cartonno = tcarton;// FormatHelper.CleanString(txtTLocationCartonEdit.Text);//CARTONNO：PDA页面的目标箱号 txtTLocationCartonEdit
                                newstorlocDetailCarton.FromlocationCode = storageCarton.LocationCode;  //FromLocationCode：TBLStorageDetail.LocationCode
                                newstorlocDetailCarton.Fromcartonno = fromCarton;// storageDetail.CartonNo; //FromCARTONNO：TBLStorageDetail. CARTONNO
                                newstorlocDetailCarton.Lotno = storageCarton.Lotno; //LotNo：TBLStorageDetail. LotNo
                                newstorlocDetailCarton.CUser = this.GetUserCode();	//	CUSER
                                newstorlocDetailCarton.CDate = dbDateTime.DBDate;	//	CDATE
                                newstorlocDetailCarton.CTime = dbDateTime.DBTime;//	CTIME
                                newstorlocDetailCarton.MaintainDate = dbDateTime.DBDate;	//	MDATE
                                newstorlocDetailCarton.MaintainTime = dbDateTime.DBTime;	//	MTIME
                                newstorlocDetailCarton.MaintainUser = this.GetUserCode();
                                _WarehouseFacade.AddStorloctransdetailcarton(newstorlocDetailCarton);
                                #endregion
                            }
                            #endregion

                            #endregion
                        }

                    }
                }
                #endregion

                //货位移动单据号
                string date = dbDateTime.DBDate.ToString().Substring(2, 6);
                string documentno = CreateAutoDocmentsNo(date);
                SaveDocmentsNo(documentno);
                txtLocationNoEdit.Text = documentno;

                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "提交成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }



        //上架
        protected void cmdShelves_ServerClick(object sender, EventArgs e)
        {

            #region 勾选一条
            if (gridWebGrid.Rows.Count <= 0)
            {
                WebInfoPublish.Publish(this, "Gird数据不能为空", this.languageComponent1);
                return;
            }
            ArrayList array = this.gridHelper.GetCheckedRows();

            if (array.Count != 1)
            {
                WebInfoPublish.Publish(this, "只能选择一条数据", this.languageComponent1);
                return;
            }
            string transNo = "";
            string fromCarton = "";
            string inputqty = "";
            string TCartonno = "";
            foreach (GridRecord row in array)
            {
                transNo = row.Items.FindItemByKey("TransNo").Text;
                fromCarton = row.Items.FindItemByKey("FCartonNo").Text;
                inputqty = row.Items.FindItemByKey("HasReMoveQty").Text;
                TCartonno = row.Items.FindItemByKey("Cartonno").Text;
            }
            int qty = Convert.ToInt32(Convert.ToDecimal(inputqty));

            #endregion

            if (TCartonno != "Move")
            {
                WebInfoPublish.Publish(this, "该货位移动数据已上架", this.languageComponent1);
                return;
            }
            #region facade
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            }
            ItemFacade itemFacade = new ItemFacade();
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            #endregion

            #region check
            string tLocationCode = FormatHelper.CleanString(txtTLocationCodeEdit.Text);//目标货位
            string tcarton = FormatHelper.CleanString(txtTLocationCartonEdit.Text); //目标箱号
            #endregion


            #region check
            if (string.IsNullOrEmpty(transNo))
            {
                WebInfoPublish.Publish(this, "移转单号不能为空", this.languageComponent1);
                return;
            }
            StorageDetail storageCarton = (StorageDetail)facade.GetStorageDetail(fromCarton);
            if (storageCarton == null)
            {  //A 根据原箱号和数量操作。根据原箱号（cartonno）到TBLStorageDetail中查找数据。没有报错。
                WebInfoPublish.PublishInfo(this, "转储单中没有对应的SAP物料号", this.languageComponent1);
                return;
            }
            if (string.IsNullOrEmpty(tLocationCode))
            {
                WebInfoPublish.Publish(this, "目标货位不能为空", this.languageComponent1);
                return;
            }
            if (string.IsNullOrEmpty(tcarton))
            {
                WebInfoPublish.Publish(this, "目标箱号不能为空", this.languageComponent1);
                return;
            }
            Storloctrans storloctrans = (Storloctrans)_WarehouseFacade.GetStorloctrans(transNo);
            if (storloctrans == null)
            {
                WebInfoPublish.Publish(this, "移转单号不存在", this.languageComponent1);
                return;
            }
            #endregion

            try
            {
                this.DataProvider.BeginTransaction();
                StorloctransDetailCarton oldStorloctransDetailCarton = _WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton, "Move") as StorloctransDetailCarton;
                if (oldStorloctransDetailCarton == null)
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, "该来源箱号未提交", this.languageComponent1);
                    return;
                }
                StorloctransDetailCarton storloctransDetailCarton = _WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton, tcarton) as StorloctransDetailCarton;
                if (storloctransDetailCarton == null)
                {
                    storloctransDetailCarton = new StorloctransDetailCarton();
                    storloctransDetailCarton = oldStorloctransDetailCarton;
                    storloctransDetailCarton.LocationCode = tLocationCode;
                    storloctransDetailCarton.Cartonno = tcarton;
                    _WarehouseFacade.AddStorloctransdetailcarton(storloctransDetailCarton);
                    _WarehouseFacade.DeleteStorloctransdetailcarton(oldStorloctransDetailCarton);
                }
                else
                {
                    storloctransDetailCarton.Qty += oldStorloctransDetailCarton.Qty;
                    _WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                }
                _WarehouseFacade.UpdateStorloctransdetailcarton(transNo, fromCarton, tcarton, tLocationCode);
                _WarehouseFacade.UpdateStorageDetailSN(tcarton, fromCarton);
                _WarehouseFacade.UpdateStorloctransDetailSN(transNo, tcarton, fromCarton);

                #region Check
                //1，	检查TBLStorLocTrans表目标库位是否为空，如果是，从TBLStorageDetail取库位更新；
                StorageDetail oldstorageDetail = (StorageDetail)facade.GetStorageDetail(tcarton);

                if (oldstorageDetail == null)
                {



                    oldstorageDetail = storageCarton;
                    oldstorageDetail.CDate = FormatHelper.TODateInt(DateTime.Now);
                    oldstorageDetail.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                    oldstorageDetail.CUser = GetUserCode();
                    oldstorageDetail.LocationCode = tLocationCode;
                    oldstorageDetail.CartonNo = tcarton;
                    oldstorageDetail.FreezeQty = 0;
                    oldstorageDetail.AvailableQty = qty;
                    oldstorageDetail.StorageQty = qty;

                    _WarehouseFacade.AddStorageDetail(oldstorageDetail);
                }
                else
                {
                    oldstorageDetail.AvailableQty += qty;
                    oldstorageDetail.StorageQty += qty;
                    _WarehouseFacade.UpdateStorageDetail(oldstorageDetail);
                }
                StorageDetail storageDetail = (StorageDetail)facade.GetStorageDetail(tcarton);
                if (string.IsNullOrEmpty(storloctrans.StorageCode))
                {
                    if (storageDetail != null)
                    {
                        storloctrans.StorageCode = storageDetail.StorageCode;
                    }
                    _WarehouseFacade.UpdateStorloctrans(storloctrans);
                }

                #endregion
                StorageDetail fromStorageDetail = (StorageDetail)facade.GetStorageDetail(fromCarton);
                if (fromStorageDetail.StorageQty == 0)
                    _WarehouseFacade.DeleteStorageDetail(fromStorageDetail);
                StorageDetail tranStorageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(tcarton);
                InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                trans.CartonNO = tcarton;
                trans.DqMCode = tranStorageDetail.DQMCode;
                trans.FacCode = tranStorageDetail.FacCode;
                trans.FromFacCode = tranStorageDetail.FacCode;
                trans.FromStorageCode = tranStorageDetail.StorageCode;
                trans.InvNO = " ";
                trans.InvType = " ";
                trans.LotNo = " ";
                trans.MaintainDate = dbDateTime.DBDate;
                trans.MaintainTime = dbDateTime.DBTime;
                trans.MaintainUser = GetUserCode();
                trans.MCode = tranStorageDetail.MCode;
                trans.ProductionDate = tranStorageDetail.ProductionDate;
                trans.Qty = tranStorageDetail.StorageQty;
                trans.Serial = 0;
                trans.StorageAgeDate = tranStorageDetail.StorageAgeDate;
                trans.StorageCode = tranStorageDetail.StorageCode;
                trans.SupplierLotNo = tranStorageDetail.SupplierLotNo;
                trans.TransNO = tranStorageDetail.CartonNo;
                trans.TransType = "IN";
                trans.ProcessType = "LocationTrans";
                trans.Unit = tranStorageDetail.Unit;
                _WarehouseFacade.AddInvInOutTrans(trans);




                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "上架成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }

        //创建按钮：点击创建生成一新货位移动单号，并填入货位移动单号输入框
        protected void cmdCreate_Click(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            }
            ItemFacade itemFacade = new ItemFacade(base.DataProvider);
            // 1.点击创建按钮，创建新的移转单号，规则：M+年+月+日+三位流水号。
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            string date = dbTime.DBDate.ToString().Substring(2, 6);
            string newIqcNo = this.CreateAutoDocmentsNo(date);
            txtLocationNoEdit.Text = newIqcNo;
        }

        //新增
        private void AddStorLoc()
        {
            #region check
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            }
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            ItemFacade itemFacade = new ItemFacade(base.DataProvider);
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            string newIqcNo = FormatHelper.CleanString(txtLocationNoEdit.Text);
            string fromCarton = FormatHelper.CleanString(txtOriginalCartonEdit.Text);
            StorageDetail storageCarton = (StorageDetail)facade.GetStorageDetail(fromCarton);
            if (storageCarton == null)
            {  //A 根据原箱号和数量操作。根据原箱号（cartonno）到TBLStorageDetail中查找数据。没有报错。
                WebInfoPublish.PublishInfo(this, "转储单中没有对应的SAP物料号", this.languageComponent1);
                return;
            }
            string dqmCode = storageCarton.DQMCode;// FormatHelper.CleanString(txtDQMoCodeEdit.Text);
            BenQGuru.eMES.Domain.MOModel.Material material = (BenQGuru.eMES.Domain.MOModel.Material)itemFacade.GetMaterialByDQMCode(dqmCode);
            if (string.IsNullOrEmpty(dqmCode))
            {
                WebInfoPublish.Publish(this, "鼎桥物料编码不能为空", this.languageComponent1);
                return;
            }
            if (material == null)
            {
                WebInfoPublish.Publish(this, "鼎桥物料编码不存在", this.languageComponent1);
                return;
            }
            if (string.IsNullOrEmpty(newIqcNo))
            {
                WebInfoPublish.Publish(this, "移转单号不能为空", this.languageComponent1);
                return;
            }
            #endregion
            try
            {
                this.DataProvider.BeginTransaction();
                Storloctrans oldStorloctrans = (Storloctrans)_WarehouseFacade.GetStorloctrans(newIqcNo);
                if (oldStorloctrans == null)
                {
                    #region 货位移动单信息TBLStorLocTrans表增加一笔数据
                    Storloctrans storloctrans = new Storloctrans();
                    storloctrans.Transno = newIqcNo;
                    storloctrans.TransType = TransType.TransType_Move;//类型(Transfer:转储；Move:货位移动)
                    storloctrans.Status = Pick_STATUS.Status_Release;
                    storloctrans.Invno = " ";
                    storloctrans.StorageCode = "";
                    storloctrans.CDate = dbTime.DBDate;
                    storloctrans.CTime = dbTime.DBTime;
                    storloctrans.CUser = this.GetUserCode();
                    storloctrans.MaintainDate = dbTime.DBDate;
                    storloctrans.MaintainTime = dbTime.DBTime;
                    storloctrans.MaintainUser = this.GetUserCode();
                    _WarehouseFacade.AddStorloctrans(storloctrans);
                    #endregion
                }
                StorloctransDetail oldstorloctransDetail = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail(newIqcNo, material.MCode);
                if (oldstorloctransDetail != null)
                {
                    //检查移转单下表TBLStorLocTransDetail是否存在，如果存在提示已经包含物料信息。
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, "移转单号已经包含物料信息", this.languageComponent1);
                    return;
                }
                else
                {
                    #region 货位移动单信息StorloctransDetail表增加一笔数据
                    StorloctransDetail storloctransDetail = new StorloctransDetail();
                    storloctransDetail.Transno = newIqcNo;
                    storloctransDetail.Status = Pick_STATUS.Status_Release;
                    storloctransDetail.DqmCode = dqmCode;
                    storloctransDetail.MCode = material.MCode;
                    storloctransDetail.MDesc = material.MenlongDesc;
                    storloctransDetail.CustmCode = "";//  
                    storloctransDetail.Unit = "";// 
                    storloctransDetail.Qty = 0;
                    storloctransDetail.CDate = dbTime.DBDate;
                    storloctransDetail.CTime = dbTime.DBTime;
                    storloctransDetail.CUser = this.GetUserCode();
                    storloctransDetail.MaintainDate = dbTime.DBDate;
                    storloctransDetail.MaintainTime = dbTime.DBTime;
                    storloctransDetail.MaintainUser = this.GetUserCode();
                    _WarehouseFacade.AddStorloctransdetail(storloctransDetail);
                    #endregion
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }
        #endregion

        #region 编辑

        protected override object GetEditObject(GridRecord row)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            string locationTransNo = row.Items.FindItemByKey("LocationTransNo").Text;
            string fromcartonno = row.Items.FindItemByKey("Fromcartonno").Text;
            string cartonno = row.Items.FindItemByKey("Cartonno").Text;
            object obj = _WarehouseFacade.GetStorloctransdetailcarton(locationTransNo, fromcartonno, cartonno);
            if (obj != null)
            {
                return obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            StorloctransDetailCarton detail = obj as StorloctransDetailCarton;
            if (detail == null)
            {
                this.txtTLocationCodeEdit.Text = "";
                txtOriginalCartonEdit.Text = "";//Fromcartonno;
                txtNumEdit.Text = ""; //  需求数量：输入框，选中整箱单选按钮时该输入框不可用
                this.txtSNEdit.Text = ""; //SN：输入框，选中整箱单选按钮时该输入框不可用
                this.txtTLocationCartonEdit.Text = "";//Cartonno;

                this.txtLocationNoEdit.Text = "";//Transno;//货位移动单号
                //this.txtDQMoCodeEdit.Text = "";//DqmCode;//鼎桥物料编码
                return;
            }
            //this.txtTLocationCodeEdit.Text = detail.LocationCode;
            //txtOriginalCartonEdit.Text = detail.Fromcartonno;
            //txtNumEdit.Text = detail.Qty.ToString();//  需求数量：输入框，选中整箱单选按钮时该输入框不可用
            ////this.txtSNEdit.Text =  SN：输入框，选中整箱单选按钮时该输入框不可用
            //this.txtTLocationCartonEdit.Text = detail.Cartonno;

            //this.txtLocationNoEdit.Text = detail.Transno;//货位移动单号
            //this.txtDQMoCodeEdit.Text = detail.DqmCode;//鼎桥物料编码

        }
        #endregion

        #region GetServerClick

        private object[] GetCheckedRowsObjects()
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
                object[] asnList = ((StorloctransDetailCarton[])objList.ToArray(typeof(StorloctransDetailCarton)));
                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
                return asnList;
            }
            return null;
        }
        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                          ((StorloctransDetailCarton)obj).DqmCode,
                          ((StorloctransDetailCarton)obj).Qty.ToString(),
                          ((StorloctransDetailCarton)obj).Unit,
                          ((StorloctransDetailCarton)obj).CUser,
               FormatHelper.ToTimeString(((StorloctransDetailCarton)obj).CTime)
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[]
                {
                     "DQMcode",     
                     "HasReMoveQty",  
                     "Unit",                  
                     "TransUser",         
                     "TransTime"                                         
                };
        }
        #endregion

        #region Load
        private string CreateAutoDocmentsNo(string stno)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(DataProvider);
            string prefix = "M" + stno;
            string maxserial = _WarehouseFacade.GetMaxSerial(prefix);//通过前缀获取最大流水号

            //如果已是最大值就返回为空
            if (maxserial == "999")
            {
                return "";
            }

            SERIALBOOK serialbook = new SERIALBOOK();

            if (maxserial == "")
            {
                serialbook.SNPrefix = prefix; /// 序列号前缀 物料代码
                serialbook.MaxSerial = "001";     /// 序列号最大Serial号码

                //序列号(3位)
                return prefix + string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = prefix;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                //序列号(3位)
                return prefix + string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }


        }

        private void SaveDocmentsNo(string newKZCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            _WarehouseFacade = new WarehouseFacade(DataProvider);

            SERIALBOOK serialbook = new SERIALBOOK();
            serialbook.SNPrefix = newKZCode.Substring(0, 7);
            serialbook.MaxSerial = newKZCode.Substring(7, 3);
            serialbook.MDate = dbDateTime.DBDate;//当前日期 
            serialbook.MTime = dbDateTime.DBTime;//当前时间
            serialbook.MUser = GetUserCode();//维护人=登录用户

            string oldserial = _WarehouseFacade.GetMaxSerial(serialbook.SNPrefix);//通过前缀获取流水号
            if (oldserial == "")
            {
                _WarehouseFacade.AddSerialBook(serialbook);//新增到serialbook表
            }
            else
            {
                _WarehouseFacade.UpdateSerialBook(serialbook);//更新到serialbook表
            }
        }

        private string CreateNewIqcNo(string stno)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (_TransferFacade == null)
            {
                _TransferFacade = new WarehouseFacade(this.DataProvider);
            }
            string maxserial = _TransferFacade.GetMaxSerial("M" + stno);

            //如果已是最大值就返回为空
            if (maxserial == "999")
            {
                return "";
            }

            SERIALBOOK serialbook = new SERIALBOOK();

            if (maxserial == "")
            {
                serialbook.SNPrefix = "M" + stno;
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _TransferFacade.AddSerialBook(serialbook);
                return "M" + stno + string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = "M" + stno;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _TransferFacade.UpdateSerialBook(serialbook);
                return "M" + stno + string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }
        }

        #endregion

    }
}
