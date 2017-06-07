using System;
using System.Data;
using System.Configuration;
using System.Collections;
using BenQGuru.eMES.Common;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FPickInOutDetail : BaseMPageNew
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        private static string StorageCode = string.Empty;
        private static string DQMCode = string.Empty;
        private static string PickNo = string.Empty;

        private WarehouseFacade facade = null;
        private InventoryFacade _Invenfacade = null;

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
                StorageCode = this.GetRequestParam("StorageCode");
                DQMCode = this.GetRequestParam("DQMCode");
                PickNo = this.GetRequestParam("PickNo");
                this.txtDQMCodeQuery.Text = DQMCode;
                this.txtDQMCodeQuery.Enabled = false;
                this.InitPageLanguage(this.languageComponent1, false);
             }
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            InitWebGrid();
            this.cmdQuery_Click(null, null);
            this.RequestData();
        }

        #region 默认查询 add by sam
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

       
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            //this.gridHelper.AddColumn("LotNo", "批次号", null);
            this.gridHelper.AddColumn("FirstStorageAgeDate", "首次入库时间", null);
            this.gridHelper.AddColumn("LocationNO", "货位", null);
            this.gridHelper.AddColumn("BoxNo", "箱号", null);
            this.gridHelper.AddColumn("Qty", "数量", null);
            this.gridHelper.AddColumn("StorageCode", "库位", null);


            this.gridWebGrid.Columns.FromKey("StorageCode").Hidden = true;
          

            this.gridHelper.AddDefaultColumn(false, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            //this.gridHelper.RequestData();
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["FirstStorageAgeDate"] = FormatHelper.ToDateString(((StorageDetail)obj).StorageAgeDate);
            //row["LotNo"] = ((Pickdetailmaterial)obj).Lotno;
            row["LocationNO"] = ((StorageDetail)obj).LocationCode;
            row["BoxNo"] = ((StorageDetail)obj).CartonNo;
            row["Qty"] = ((StorageDetail)obj).AvailableQty.ToString();
            row["StorageCode"] = ((StorageDetail)obj).StorageCode;

            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryInOutInfoByDQMCode(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(StorageCode)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(DQMCode)),
                inclusive,
                exclusive
               
               );
        }
        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryInOutInfoByDQMCodeCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(StorageCode)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(DQMCode))
            );
        }

        #endregion

        #region Button
        protected void cmdReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FPickDoneMP.aspx",
                                   new string[] { "PickNo" },
                                   new string[] { PickNo }));
        }
       

        #endregion

        #region Object <--> Page

            

        #endregion

        #region For Export To Excel

       

        #endregion

       
    }
}
