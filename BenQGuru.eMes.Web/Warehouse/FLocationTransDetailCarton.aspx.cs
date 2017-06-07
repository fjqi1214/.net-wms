using System;
using System.Data;

using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FLocationTransDetailCarton : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

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
            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.txtLocationNoQuery.Text = Request.QueryString["LOCATIONNO"];
                this.txtLocationNoQuery.Enabled = false;
                txtPageEdit.Text = Request.QueryString["Page"];

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

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("LocStorTransNo", "货位移动单号", null);
            this.gridHelper.AddColumn("DQMcode", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("Mdesc", "物料描述", null);
            this.gridHelper.AddColumn("HWMcode", "华为物料编码", null);
            this.gridHelper.AddColumn("FLocationCode", "源货位", null);
            this.gridHelper.AddColumn("TLocationCode", "目标货位", null);
            this.gridHelper.AddColumn("Quantity_num", "数量", null);
            this.gridHelper.AddColumn("FCartonNo", "源箱号", null);
            this.gridHelper.AddColumn("TCartonNo", "目标箱号", null);
            this.gridHelper.AddLinkColumn("SNDetail", "SN详情", null);

            //this.gridWebGrid.Columns.FromKey("FCartonNo").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("TCartonNo").Hidden = true;
            
            this.gridHelper.AddDefaultColumn(false, false);
            
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();

            row["LocStorTransNo"] = ((StorloctransDetailCarton)obj).Transno;
            row["DQMcode"] = ((StorloctransDetailCarton)obj).DqmCode;
            row["Mdesc"] = ((StorloctransDetailCarton)obj).MDesc;
            row["HWMcode"] = ((StorloctransDetailCarton)obj).MCode;
            row["FLocationCode"] = ((StorloctransDetailCarton)obj).FromlocationCode;
            row["TLocationCode"] = ((StorloctransDetailCarton)obj).LocationCode;
            row["Quantity_num"] = ((StorloctransDetailCarton)obj).Qty.ToString("G0");
            row["FCartonNo"] = ((StorloctransDetailCarton)obj).Fromcartonno;
            row["TCartonNo"] = ((StorloctransDetailCarton)obj).Cartonno;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }

            return this._WarehouseFacade.QueryStorloctransDetailC(
           FormatHelper.CleanString(this.txtLocationNoQuery.Text),
           Request.QueryString["DQMcode"],
           inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.QueryStorloctransDetailCCount(
                  FormatHelper.CleanString(this.txtLocationNoQuery.Text), Request.QueryString["DQMcode"]
                  );
        }
        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            if (commandName == "SNDetail")
            {
                string transNo = row.Items.FindItemByKey("LocStorTransNo").Text.Trim();
                string FcartonNo = row.Items.FindItemByKey("FCartonNo").Text.Trim();
                string TcartonNo = row.Items.FindItemByKey("TCartonNo").Text.Trim();
                //PIS未看到具体页面（参照转储）
                Response.Redirect(this.MakeRedirectUrl("FLocationTransDetailSN.aspx", new string[] { "LOCATIONNO", "FCARTON", "TCARTON", "MCODE" }, new string[] { transNo, FcartonNo, TcartonNo, Request.QueryString["MCODE"] }));
            }
        }
        protected override void Grid_ClickCell(GridRecord row, string command)
        {
            base.Grid_ClickCell(row, command);
            if (command == "SNDetail")
            {
                string transNo = row.Items.FindItemByKey("LocStorTransNo").Text.Trim();
                string FcartonNo = row.Items.FindItemByKey("FCartonNo").Text.Trim();
                string TcartonNo = row.Items.FindItemByKey("TCartonNo").Text.Trim();
                //PIS未看到具体页面
                //Response.Redirect(this.MakeRedirectUrl("FStorLocTransDetailSN.aspx", new string[] { "LOCATIONNO", "FCARTON", "TCARTON" }, new string[] { transNo, FcartonNo, TcartonNo }));
            }
        }

        #endregion
        
        #region Button

        //返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPageEdit.Text))
            {
                Response.Redirect(this.MakeRedirectUrl("FLocationTransDetailMP.aspx", new string[] { "LOCATIONNO" }, new string[] { this.txtLocationNoQuery.Text }));

            }
            else
            {
                Response.Redirect(this.MakeRedirectUrl(txtPageEdit.Text, new string[] { "LOCATIONNO" }, new string[] { this.txtLocationNoQuery.Text }));
            }
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                ((StorloctransDetailCarton)obj).Transno,
                                ((StorloctransDetailEX)obj).DqmCode,
                                ((StorloctransDetailEX)obj).MDesc,
                                ((StorloctransDetailEX)obj).MCode,
                                ((StorloctransDetailCarton)obj).FromlocationCode,                                 
                                ((StorloctransDetailCarton)obj).LocationCode,
                                ((StorloctransDetailCarton)obj).Qty.ToString("G0"),
                                ((StorloctransDetailCarton)obj).Fromcartonno,
                                ((StorloctransDetailCarton)obj).Cartonno                                
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[]
                {
                                    "LocationNo",
                                    "DQMcode",
                                    "Mdesc",
                                    "HWMcode",
                                    "FLocationCode",
                                    "TLocationCode",
                                    "Quantity_num",
                                    "FCartonNo",
                                     "TCartonNo" 
                                   
                };
        }

        #endregion
    }
}
