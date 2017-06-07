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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.OQC;
using System.Text;
using BenQGuru.eMES.Domain.OQC;


namespace BenQGuru.eMES.Web.OQC
{
    public partial class FOQCCheckListQuery : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;
        private OQCFacade _OQCFacade = null;

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
            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitPickNoList();
                this.InitOQCStatusList();

                InitStorageList();
                InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
            }
        }

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

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        //初始拣货任务号下拉框
        /// <summary>
        /// 初始拣货任务号下拉框
        /// </summary>
        private void InitPickNoList()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            //object[] objPick = _InventoryFacade.GetPickByStatus(PickHeadStatus.PickHeadStatus_OQC);
            //edit by sam
            object[] objPick = _InventoryFacade.GetPickByPickStatus(PickHeadStatus.PickHeadStatus_OQC);//,OQCStatus.OQCStatus_Release
            this.drpPickNoQuery.Items.Add(new ListItem("", ""));
            if (objPick != null)
            {
                foreach (Pick asn in objPick)
                {
                    this.drpPickNoQuery.Items.Add(new ListItem(asn.PickNo, asn.PickNo));
                }
            }
            this.drpPickNoQuery.SelectedIndex = 0;
        }

        //初始OQC检验单状态下拉框
        /// <summary>
        /// 初始OQC检验单状态下拉框
        /// </summary>
        private void InitOQCStatusList()
        {
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("OQCSTATUS");
            this.drpOQCStatusQuery.Items.Add(new ListItem("", ""));
            if (parameters != null)
            {
                foreach (Domain.BaseSetting.Parameter parameter in parameters)
                {
                    this.drpOQCStatusQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
                }
            }

            this.drpOQCStatusQuery.SelectedIndex = 0;
        }


        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("OQCNo", "OQC检验单号", null);
            this.gridHelper.AddColumn("PickNo", "拣货任务令号", null);
            this.gridHelper.AddColumn("StorageOutType", "出库类型", null);
            this.gridHelper.AddDataColumn("PickType", "出库类型代码", true);
            this.gridHelper.AddColumn("SAPInvNo", "SAP单据号", null);
            this.gridHelper.AddColumn("STORAGECode123", "出库库位", null);
            this.gridHelper.AddColumn("Status", "状态", null);
            this.gridHelper.AddDataColumn("OQCStatus", "状态", true);//隐藏列
            this.gridHelper.AddColumn("GfFlag", "光伏标识", null);
            this.gridHelper.AddColumn("OQCType", "检验方式", null);
            this.gridHelper.AddColumn("QCStatus", "检验结果", null);
            this.gridHelper.AddColumn("AppQty", "送检数量", null);
            this.gridHelper.AddColumn("NGQty", "缺陷品数", null);
            this.gridHelper.AddColumn("ReturnQty", "退换货数量", null);
            this.gridHelper.AddColumn("AppDate", "送检日期", null);
            this.gridHelper.AddColumn("AppTime", "送检时间", null);
            this.gridHelper.AddColumn("AppUser", "送检人", null);
            this.gridHelper.AddEditColumn("btnInspect", "检验");
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            row["OQCNo"] = ((OQCExt1)obj).OqcNo;
            row["PickNo"] = ((OQCExt1)obj).PickNo;
            row["StorageOutType"] = this.GetPickTypeName(((OQCExt1)obj).PickType);//出库类型（单据类型）
            row["PickType"] = ((OQCExt1)obj).PickType;//出库类型
            row["SAPInvNo"] = ((OQCExt1)obj).InvNo;


            row["STORAGECode123"] = ((OQCExt1)obj).storagecode;

            row["Status"] = this.GetStatusName(((OQCExt1)obj).Status);
            row["OQCStatus"] = ((OQCExt1)obj).Status;
            row["GfFlag"] = ((OQCExt1)obj).GFFlag;
            row["OQCType"] = FormatHelper.GetChName(((OQCExt1)obj).OqcType);
            row["QCStatus"] = FormatHelper.GetChName(((OQCExt1)obj).QcStatus);
            row["AppQty"] = ((OQCExt1)obj).AppQty;
            row["NGQty"] = ((OQCExt1)obj).NgQty;
            row["ReturnQty"] = ((OQCExt1)obj).ReturnQty;
            row["AppDate"] = FormatHelper.ToDateString(((OQCExt1)obj).AppDate, "/");
            row["AppTime"] = FormatHelper.ToTimeString(((OQCExt1)obj).AppTime, ":");
            row["AppUser"] = ((OQCExt1)obj).CUser;

            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(GetUserCode());
            return this._OQCFacade.QueryOQC2(usergroupList,
                FormatHelper.CleanString(this.drpPickNoQuery.SelectedValue),
                FormatHelper.CleanString(this.txtOQCNoQuery.Text),
                FormatHelper.CleanString(this.drpOQCStatusQuery.SelectedValue),
                FormatHelper.TODateInt(this.txtAppCDateQuery.Text),
                FormatHelper.TODateInt(this.txtAppEDateQuery.Text),
                txtCartonCode.Text.ToUpper(),
                txtSNQuery.Text.ToUpper(),
                FormatHelper.CleanString(this.txtInvNoQuery.Text),
                 FormatHelper.CleanString(this.txtDQMCodeQuery.Text),
                 FormatHelper.CleanString(this.txtCusMCodeQuery.Text),
                 FormatHelper.CleanString(this.txtGFHWItemCodeQuery.Text), drpStorageQuery.SelectedValue,
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(GetUserCode());
            return this._OQCFacade.QueryOQC2Count(usergroupList,
                FormatHelper.CleanString(this.drpPickNoQuery.SelectedValue),
                FormatHelper.CleanString(this.txtOQCNoQuery.Text),
                FormatHelper.CleanString(this.drpOQCStatusQuery.SelectedValue),
                FormatHelper.TODateInt(this.txtAppCDateQuery.Text),
                FormatHelper.TODateInt(this.txtAppEDateQuery.Text), txtCartonCode.Text.ToUpper(), txtSNQuery.Text.ToUpper(), FormatHelper.CleanString(this.txtInvNoQuery.Text),
                FormatHelper.CleanString(this.txtDQMCodeQuery.Text), FormatHelper.CleanString(this.txtCusMCodeQuery.Text), FormatHelper.CleanString(this.txtGFHWItemCodeQuery.Text), drpStorageQuery.SelectedValue);
        }

        #endregion

        #region Button

        //Grid中点击按钮
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }




            if (commandName == "btnInspect")
            {
                string oqcStatus = row.Items.FindItemByKey("OQCStatus").Text.Trim();
                string oqcNo = row.Items.FindItemByKey("OQCNo").Text.Trim();
                string pickNo = row.Items.FindItemByKey("PickNo").Text.Trim();
                string pickType = row.Items.FindItemByKey("PickType").Text.Trim();
                //if (oqcStatus == OQCStatus.OQCStatus_Release)
                //{
                //    //更新检验单状态为WaitCheck
                //    Domain.OQC.OQC oqc = (Domain.OQC.OQC)_OQCFacade.GetOQC(oqcNo);
                //    oqc.Status = OQCStatus.OQCStatus_WaitCheck;
                //    _OQCFacade.UpdateOQC(oqc);
                //}
                Response.Redirect(this.MakeRedirectUrl("FOQCCheckResultMP.aspx",
                                                            new string[] { "OQCNo", "PickNo", "PickType", "OQCStatus" },
                                                            new string[] { oqcNo, pickNo, pickType, oqcStatus }));
            }
        }
        #endregion

        protected void cmdStatusSTS_ServerClick(object sender, EventArgs e)
        {

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count == 0)
            {
                WebInfoPublish.Publish(this, "必须至少选择一条数据！", this.languageComponent1);
                return;
            }
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            foreach (GridRecord row in array)
            {
                string oqcNo = row.Items.FindItemByKey("OQCNo").Value.ToString();
                Domain.OQC.OQC oqc = (Domain.OQC.OQC)_OQCFacade.GetOQC(oqcNo);
                if (oqc.Status != IQCStatus.IQCStatus_Release)
                {
                    //OQC检验单号: {0} 状态不是初始化
                    WebInfoPublish.Publish(this, string.Format("OQC检验单号: {0} 状态不是初始化，不能免检 ", oqcNo), this.languageComponent1);
                    return;
                }
            }

            try
            {
                this.DataProvider.BeginTransaction();
                foreach (GridRecord row in array)
                {
                    string oqcNo = row.Items.FindItemByKey("OQCNo").Value.ToString();

                    Domain.OQC.OQC oqc = (Domain.OQC.OQC)_OQCFacade.GetOQC(oqcNo);

                    ToSTS(oqcNo);

                }
                this.DataProvider.CommitTransaction();
                this.gridHelper.RequestData();//刷新页面
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, "免检失败：" + ex.Message, this.languageComponent1);
                return;


            }
        }


        private void ToSTS(string oqcNo)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            //1、更新OQC单表(TBLOQC)
            _OQCFacade.UpdateOQC(OQCType.OQCType_ExemptCheck, OQCStatus.OQCStatus_OQCClose, "Y", oqcNo);

            //2、更新OQC单明细表(TBLOQCDETAIL)
            _OQCFacade.UpdateOQCDetail("Y", oqcNo);

            //3、更新OQC单明细SN信息表(TBLOQCDETAILSN)
            _OQCFacade.UpdateOQCDetailSN("Y", oqcNo);

            //4、更新发货箱单头信息表(TBLCartonInvoices)
            Pick pick = (Pick)_InventoryFacade.GetPickByOqcNo(oqcNo);
            if (pick != null)
            {
                //if (pick.GFFlag == "X")
                //{
                //    if (CheckAllOQCStatusIsOQCClose(oqcNo))
                //    {
                //        CARTONINVOICES cartonInvoices = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoicesByOqcNo(oqcNo);
                //        if (cartonInvoices != null)
                //        {
                //            cartonInvoices.STATUS = CartonInvoices_STATUS.Status_OQCClose;
                //            _WarehouseFacade.UpdateCartoninvoices(cartonInvoices);
                //        }
                //    }
                //    else
                //    {
                //        throw new Exception("OQC单号" + oqcNo + "没有全部检验完成");
                //    }
                //}
                //else
                //{


                if (_OQCFacade.IsOQCFinish(pick.PickNo))
                {
                    pick.Status = PickHeadStatus.PickHeadStatus_PackingListing;
                    _WarehouseFacade.UpdatePick(pick);
                }
                CARTONINVOICES cartonInvoices = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoicesByOqcNo(oqcNo);
                if (cartonInvoices != null)
                {
                    cartonInvoices.STATUS = CartonInvoices_STATUS.Status_OQCClose;
                    _WarehouseFacade.UpdateCartoninvoices(cartonInvoices);
                }

                //}

                #region 在invinouttrans表中增加一条数据
                WarehouseFacade facade = new WarehouseFacade(base.DataProvider);
                DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);

                InvInOutTrans trans1 = facade.CreateNewInvInOutTrans();
                trans1.CartonNO = string.Empty;
                trans1.DqMCode = string.Empty;
                trans1.FacCode = string.Empty;
                trans1.FromFacCode = string.Empty;
                trans1.FromStorageCode = string.Empty;
                trans1.InvNO = pick.InvNo;//.InvNo;
                trans1.InvType = pick.PickType;
                trans1.LotNo = string.Empty;
                trans1.MaintainDate = dbTime1.DBDate;
                trans1.MaintainTime = dbTime1.DBTime;
                trans1.MaintainUser = this.GetUserCode();
                trans1.MCode = string.Empty;
                trans1.ProductionDate = 0;
                trans1.Qty = 0;
                trans1.Serial = 0;
                trans1.StorageAgeDate = 0;
                trans1.StorageCode = string.Empty;
                trans1.SupplierLotNo = string.Empty;
                trans1.TransNO = pick.PickNo;// asnIqc.IqcNo;
                trans1.TransType = "OUT";
                trans1.Unit = string.Empty;
                trans1.ProcessType = "OQC";
                facade.AddInvInOutTrans(trans1);


                Domain.OQC.OQC asnIqcHead = (Domain.OQC.OQC)_OQCFacade.GetOQC(oqcNo);
                object[] objs_oqcDetail = _OQCFacade.GetOQCDetailByOqcNo(oqcNo);
                if (objs_oqcDetail != null)
                {
                    foreach (OQCDetail asnIqc in objs_oqcDetail)
                    {

                        InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = asnIqc.DQMCode;
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = asnIqc.CarInvNo;//.InvNo;
                        trans.InvType = asnIqcHead.OqcType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = dbTime1.DBDate;
                        trans.MaintainTime = dbTime1.DBTime;
                        trans.MaintainUser = this.GetUserCode();
                        trans.MCode = asnIqc.MCode;
                        trans.ProductionDate = 0;
                        trans.Qty = asnIqc.Qty;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = string.Empty;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = oqcNo;// asnIqc.IqcNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "OQC";
                        facade.AddInvInOutTrans(trans);
                    }
                }
                #endregion


            }
        }

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((OQCExt1)obj).OqcNo,
                                ((OQCExt1)obj).PickNo,
                                this.GetInvInName(((OQCExt1)obj).PickType),
                                ((OQCExt1)obj).InvNo,
                                this.GetStatusName(((OQCExt1)obj).Status),
                                ((OQCExt1)obj).GFFlag,
                                FormatHelper.GetChName(((OQCExt1)obj).OqcType),
                                FormatHelper.GetChName(((OQCExt1)obj).QcStatus),
                                ((OQCExt1)obj).AppQty.ToString(),
                                ((OQCExt1)obj).NgQty.ToString(),
                                FormatHelper.ToDateString(((OQCExt1)obj).AppDate,"/"),
                                FormatHelper.ToTimeString(((OQCExt1)obj).AppTime, ":")
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"OQCNo",
                                    "PickNo",
                                    "StorageOutType",
                                    "SAPInvNo",
                                    "Status",
	                                "GfFlag",
                                    "OQCType",
                                    "QCStatus",
                                    "AppQty",
                                    "NGQty",
                                    "AppDate",
                                    "AppTime",
                                    "AppUser"};
        }

        #endregion



        private void InitStorageList()
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser(GetUserCode());//+TOSTORAGE
            this.drpStorageQuery.Items.Add(new ListItem("", ""));

            object[] parameters = systemSettingFacade.GetDistinctParaInParameterGroup(usergroupList);
            if (parameters != null)
            {
                foreach (Domain.BaseSetting.Parameter parameter in parameters)
                {
                    drpStorageQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterCode));

                }
            }
            this.drpStorageQuery.SelectedIndex = 0;

        }
    }
}
