using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;

using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.Web.ReportCenter
{
    public partial class FSAPStorageQueryQP : BaseQPage
    {
        protected BenQGuru.eMES.Web.UserControl.eMESDate datAppDateFromQuery;
        protected BenQGuru.eMES.Web.UserControl.eMESDate datAppDateToQuery;

        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private ExcelExporter excelExporter;

        private InventoryFacade _InventoryFacade = null;
        private SAPMESStorageCompareFacade _SAPMESStorageCompareFacade = null;

        #region Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);

            // languageComponent1
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

            // excelExporter
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.InitOnPostBack();

            _InventoryFacade = new InventoryFacade(this.DataProvider);
            _SAPMESStorageCompareFacade = new SAPMESStorageCompareFacade(this.DataProvider);

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化界面UI
                this.InitUI();
                this.InitButtonHelp();
                this.InitWebGrid();
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdSAPStorageSync_ServerClick(object sender, System.EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            SAPStorageQuery query = _InventoryFacade.CreateNewSAPStorageQuery();

            query.Flag = FlagStatus.FlagStatus_MES;
            query.OrganizationID = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtFactoryQuery.Text));
            query.StorageID = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtStorageQuery.Text));
            query.ItemCode = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtItemCodeQuery.Text));
            query.MaintainUser = this.GetUserCode();

            _InventoryFacade.AddSAPStorageQuery(query);

            this.txtFactoryQuery.Text = string.Empty;
            this.txtStorageQuery.Text = string.Empty;
            this.txtItemCodeQuery.Text = string.Empty;
        }

        protected void cmdCompare_ServerClick(object sender, System.EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            SetInfo();
            RequestData();
        }

        protected void cmdRefresh_ServerClick(object sender, System.EventArgs e)
        {
            SetInfo();
        }

        private void SetInfo()
        {
            this.txtInfo.Text = string.Empty;

            SAPStorageQuery query = null;
            StringBuilder builder = new StringBuilder();

            object[] list = _InventoryFacade.QuerySAPStorageQuery(FlagStatus.FlagStatus_SAP, string.Empty);
            if (list != null)
            {
                query = (SAPStorageQuery)list[list.Length - 1];

                builder.Append(this.languageComponent1.GetString("LastSyncDateTime"));
                builder.Append(":");
                builder.Append(FormatHelper.ToDateString(query.MaintainDate, "-"));
                builder.Append(" ");
                builder.Append(FormatHelper.ToTimeString(query.MaintainTime, ":"));
                builder.Append(" ");
                builder.Append(this.languageComponent1.GetString("Factory"));
                builder.Append(":");
                builder.Append(query.OrganizationID);
                builder.Append(" ");
                builder.Append(this.languageComponent1.GetString("Storage"));
                builder.Append(":");
                builder.Append(query.StorageID);
                builder.Append(" ");
                builder.Append(this.languageComponent1.GetString("ItemCode"));
                builder.Append(":");
                builder.Append(query.ItemCode);

                this.txtInfo.Text = builder.ToString();
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        private bool ValidateInput()
        {

            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblFactoryQuery, txtFactoryQuery, int.MaxValue, true));
            manager.Add(new LengthCheck(lblStorageQuery, txtStorageQuery, int.MaxValue, true));

            if (!manager.Check())
            {
                WebInfoPublish.PublishInfo(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region For Page_Load

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);

            this.gridHelper = new GridHelper(this.gridWebGrid);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void InitButtonHelp()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        #endregion

        #region For Query Data

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(int.MinValue, int.MaxValue);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            return _SAPMESStorageCompareFacade.QuerySAPMESStorageCompare(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFactoryQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                inclusive,
                exclusive
                );
        }

        private int GetRowCount()
        {
            return _SAPMESStorageCompareFacade.QuerySAPMESStorageCompareCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFactoryQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text))
                );
        }

        #endregion

        #region For Grid And Edit

        private void InitWebGrid()
        {
            this.gridWebGrid.Columns.Clear();

            this.gridHelper.AddColumn("Factory", "工厂", null);
            this.gridHelper.AddColumn("Storage", "库别", null);
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ItemDesc", "产品描述", null);
            this.gridHelper.AddColumn("Grade", "等级", null);
            this.gridHelper.AddColumn("SAPQTY", "SAP非限制使用库存", null);
            this.gridHelper.AddColumn("SAPMQTY", "SAP货损库存", null);
            this.gridHelper.AddColumn("SAPOKQTY", "SAP完工数量", null);
            this.gridHelper.AddColumn("DifferenceQty", "差异数量", null);
            this.gridHelper.AddColumn("MESQTY", "MES在库数量", null);


            this.gridHelper.AddDefaultColumn(false, false);

            this.gridWebGrid.Columns.FromKey("SAPQTY").DataType = "System.Int32";
            this.gridWebGrid.Columns.FromKey("SAPMQTY").DataType = "System.Int32";
            this.gridWebGrid.Columns.FromKey("MESQTY").DataType = "System.Int32";
            this.gridWebGrid.Columns.FromKey("SAPOKQTY").DataType = "System.Int32";
            this.gridWebGrid.Columns.FromKey("DifferenceQty").DataType = "System.Int32";

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{
                    ((SAPMESStorageCompare)obj).OrganizationID.ToString(),
                    ((SAPMESStorageCompare)obj).StorageID,
                    ((SAPMESStorageCompare)obj).ItemCode,
                    ((SAPMESStorageCompare)obj).MaterialName,
                    ((SAPMESStorageCompare)obj).ItemGrade,
                    ((SAPMESStorageCompare)obj).SAPQTY.ToString(),
                    ((SAPMESStorageCompare)obj).SAPMQTY.ToString(),
                    ((SAPMESStorageCompare)obj).SAPOKQTY.ToString(),
                    Convert.ToString(((SAPMESStorageCompare)obj).SAPQTY+((SAPMESStorageCompare)obj).SAPMQTY-((SAPMESStorageCompare)obj).SAPOKQTY),
                    ((SAPMESStorageCompare)obj).MESQTY.ToString()
                });
        }

        #endregion

        #region For Export To Excel

        private string[] FormatExportRecord(object obj)
        {
            return (new string[]{
                    ((SAPMESStorageCompare)obj).OrganizationID.ToString(),
                    ((SAPMESStorageCompare)obj).StorageID,
                    ((SAPMESStorageCompare)obj).ItemCode,
                    ((SAPMESStorageCompare)obj).MaterialName,
                    ((SAPMESStorageCompare)obj).ItemGrade,
                    ((SAPMESStorageCompare)obj).SAPQTY.ToString(),
                    ((SAPMESStorageCompare)obj).SAPMQTY.ToString(),
                    ((SAPMESStorageCompare)obj).SAPOKQTY.ToString(),
                    Convert.ToString(((SAPMESStorageCompare)obj).SAPQTY+((SAPMESStorageCompare)obj).SAPMQTY-((SAPMESStorageCompare)obj).SAPOKQTY),
                    ((SAPMESStorageCompare)obj).MESQTY.ToString()
                });
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "Factory",
                "Storage",	
                "ItemCode",
                "ItemDesc",
                "Grade",
                "SAPQTY",
                "SAPMQTY",
                "SAPOKQTY",
                "DifferenceQty",
                "MESQTY"                                   
            };
        }

        #endregion
    }
}
