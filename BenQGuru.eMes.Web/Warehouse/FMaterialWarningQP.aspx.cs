using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Config;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FMaterialWarningQP : BasePage
    {
        protected BenQGuru.eMES.Web.UserControl.eMESDate datDateQuery;

        private System.ComponentModel.IContainer components;

        private LanguageComponent _LanguageComponent1;
        private GridHelper _GridHelper;
        private ExcelExporter _ExcelExporter;

        private MaterialFacade _MaterialFacade;

        public bool AutoRefresh
        {
            get
            {
                if (this.Session["AutoRefresh"] != null)
                {
                    try
                    {
                        return bool.Parse(this.Session["AutoRefresh"].ToString());
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            set
            {
                this.Session["AutoRefresh"] = value.ToString();

                if (value)
                {
                    this.RefreshController1.Start();
                }
                else
                {
                    this.RefreshController1.Stop();
                }
            }
        }

        #region Form Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this._LanguageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this._LanguageComponent1.Language = "CHS";
            this._LanguageComponent1.LanguagePackageDir = "";
            this._LanguageComponent1.RuntimePage = null;
            this._LanguageComponent1.RuntimeUserControl = null;
            this._LanguageComponent1.UserControlName = "";

            this._ExcelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this._ExcelExporter.FileExtension = "xls";
            this._ExcelExporter.LanguageComponent = this._LanguageComponent1;
            this._ExcelExporter.Page = this;
            this._ExcelExporter.RowSplit = "\r\n";

            this._MaterialFacade = new MaterialFacade(this.DataProvider);

            this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();

            if (!IsPostBack)
            {
                InitPageLanguage(this._LanguageComponent1, false);

                InitUI();
                InitStatus();
                InitWebGrid();
                this.datDateQuery.Date_DateTime = DateTime.Now;

                this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;
            }

            if (this.AutoRefresh)
            {
                this.RequestData();
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
        }

        protected void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        {
            if (e.Cell.Column.Key == "Edit")
            {
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this._ExcelExporter.Export();
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this._GridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        protected void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
        {
            this.RequestData();
        }

        #endregion

        #region LoadData

        private void RequestData()
        {
            this.AutoRefresh = this.chbRefreshAuto.Checked;

            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this._GridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._MaterialFacade.QueryMaterialReqInfo(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeQuery.Text)),
                FormatHelper.TODateInt(datDateQuery.Date_DateTime),
                this.ddlStatusQuery.SelectedValue,
                inclusive, exclusive);
        }

        private int GetRowCount()
        {
            return this._MaterialFacade.QueryMaterialReqInfoCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeQuery.Text)),
                FormatHelper.TODateInt(datDateQuery.Date_DateTime),
                this.ddlStatusQuery.SelectedValue);
        }

        #endregion

        #region Init Functions

        private void InitHander()
        {
            this._GridHelper = new GridHelper(this.gridWebGrid);
            this._GridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
            this._GridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

            this._ExcelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this._ExcelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this._ExcelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void InitWebGrid()
        {
            this.gridWebGrid.Columns.Clear();

            this._GridHelper.AddColumn("BigSSCode", "BigSSCode", null);
            this._GridHelper.AddColumn("PlanDate", "PlanDate", null);
            this._GridHelper.AddColumn("PlanSeq", "PlanSeq", null);
            this._GridHelper.AddColumn("MOCode", "MOCode", null);
            this._GridHelper.AddColumn("MOSeq", "MOSeq", null);
            this._GridHelper.AddColumn("MaterialCode", "MaterialCode", null);
            this._GridHelper.AddColumn("MModelCode", "MModelCode", null);
            this._GridHelper.AddColumn("RequestQty", "RequestQty", null);
            this._GridHelper.AddColumn("MayBeQty", "MayBeQty", null);
            this._GridHelper.AddColumn("Status", "Status", null);
            this._GridHelper.AddColumn("ReqType", "ReqType", null);

            //this._GridHelper.AddDefaultColumn(false, false);

            this._GridHelper.ApplyLanguage(this._LanguageComponent1);
        }

        private void InitStatus()
        {
            this.ddlStatusQuery.Items.Add(new ListItem(string.Empty, string.Empty));
            this.ddlStatusQuery.Items.Add(new ListItem(this._LanguageComponent1.GetString(MaterialReqStatus.MaterialReqStatus_Requesting), MaterialReqStatus.MaterialReqStatus_Requesting));
            this.ddlStatusQuery.Items.Add(new ListItem(this._LanguageComponent1.GetString(MaterialReqStatus.MaterialReqStatus_Responsed), MaterialReqStatus.MaterialReqStatus_Responsed));

            this.ddlStatusQuery.SelectedIndex = 1;
        }

        #endregion

        #region Get/Set Edit Object

        protected UltraGridRow GetGridRow(object obj)
        {
            MaterialReqInfoWithMessage materialReqInfoWithMessage = (MaterialReqInfoWithMessage)obj;

            return new UltraGridRow(
                new object[]{
                    materialReqInfoWithMessage.BigSSCode,
                    FormatHelper.ToDateString(materialReqInfoWithMessage.PlanDate),
                    materialReqInfoWithMessage.PlanSeq.ToString(),
                    materialReqInfoWithMessage.MoCode,
                    materialReqInfoWithMessage.MoSeq.ToString(),
                    materialReqInfoWithMessage.GetDisplayText("ItemCode"),
                    materialReqInfoWithMessage.MaterialModelCode,
                    materialReqInfoWithMessage.RequestQTY.ToString(),
                    materialReqInfoWithMessage.MayBeQTY.ToString(),
                    this._LanguageComponent1.GetString(materialReqInfoWithMessage.Status),
                    this._LanguageComponent1.GetString(materialReqInfoWithMessage.ReqType)
                });
        }

        #endregion

        #region Export

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "BigSSCode",
                "PlanDate",
                "PlanSeq",
                "MOCode",
                "MOSeq",
                "MaterialCode",
                "MModelCode",
                "RequestQty",
                "MayBeQty",
                "Status",
                "ReqType"
            };
        }

        private string[] FormatExportRecord(object obj)
        {
            MaterialReqInfoWithMessage materialReqInfoWithMessage = (MaterialReqInfoWithMessage)obj;

            return new string[]{
                materialReqInfoWithMessage.BigSSCode,
                FormatHelper.ToDateString(materialReqInfoWithMessage.PlanDate),
                materialReqInfoWithMessage.PlanSeq.ToString(),
                materialReqInfoWithMessage.MoCode,
                materialReqInfoWithMessage.MoSeq.ToString(),
                materialReqInfoWithMessage.GetDisplayText("ItemCode"),
                materialReqInfoWithMessage.MaterialModelCode,
                materialReqInfoWithMessage.RequestQTY.ToString(),
                materialReqInfoWithMessage.MayBeQTY.ToString(),
                this._LanguageComponent1.GetString(materialReqInfoWithMessage.Status),
                this._LanguageComponent1.GetString(materialReqInfoWithMessage.ReqType)
            };
        }

        #endregion
    }
}
