using System;
using System.Data;

using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FStorLocTransDetailMP : BaseMPageNew
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
                this.txtTransNoQuery.Text = Request.QueryString["TRANSNO"];
                this.txtTransNoQuery.Enabled = false;

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
            this.gridHelper.AddColumn("TransNo", "转储单号", null);
            this.gridHelper.AddColumn("DQMcode", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("CUSTMCODE", "华为物料号", null);
            this.gridHelper.AddColumn("Mdesc", "物料描述", null);

            this.gridHelper.AddColumn("RequireQty", "需求数量", null);
            this.gridHelper.AddColumn("Trans_Qty", "已转数量", null);
            this.gridHelper.AddColumn("MCode", "物料号", null);
            this.gridHelper.AddLinkColumn("CartonDetail", "箱号详情", null);

            this.gridWebGrid.Columns.FromKey("MCode").Hidden = true;
            this.gridHelper.AddDefaultColumn(false, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();

            row["TransNo"] = ((StorloctransDetailEX)obj).Transno;
            row["DQMcode"] = ((StorloctransDetailEX)obj).DqmCode;
            row["CUSTMCODE"] = ((StorloctransDetailEX)obj).CustmCode;
            row["Mdesc"] = ((StorloctransDetailEX)obj).MDesc;
            row["RequireQty"] = ((StorloctransDetailEX)obj).Qty.ToString("G0");
            row["Trans_Qty"] = ((StorloctransDetailEX)obj).TransQty.ToString("G0");
            row["MCode"] = ((StorloctransDetailEX)obj).MCode;

            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }

            return this._WarehouseFacade.QueryStorloctransDetail(
           FormatHelper.CleanString(this.txtTransNoQuery.Text),
           inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.QueryStorloctransDetailCount(
                  FormatHelper.CleanString(this.txtTransNoQuery.Text)
                  );
        }

        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            if (commandName == "CartonDetail")
            {
                string transNo = row.Items.FindItemByKey("TransNo").Text.Trim();
                string mcode = row.Items.FindItemByKey("MCode").Text.Trim();
                string dqMcode = row.Items.FindItemByKey("DQMcode").Text.Trim();
                Response.Redirect(this.MakeRedirectUrl("FStorLocTransDetailCarton.aspx", new string[] { "TRANSNO", "MCODE", "DQMCODE" }, new string[] { transNo, mcode, dqMcode }));
            }
        }

        protected override void Grid_ClickCell(GridRecord row, string command)
        {
            base.Grid_ClickCell(row, command);
            if (command == "CartonDetail")
            {
                string transNo = row.Items.FindItemByKey("TransNo").Text.Trim();
                string mcode = row.Items.FindItemByKey("MCode").Text.Trim();
                string dqMcode = row.Items.FindItemByKey("DQMcode").Text.Trim();
                Response.Redirect(this.MakeRedirectUrl("FStorLocTransDetailCarton.aspx", new string[] { "TRANSNO", "MCODE", "DQMCODE" }, new string[] { transNo, mcode, dqMcode }));
            }
        }

        #endregion

        #region Button

        //返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {


            Response.Redirect(this.MakeRedirectUrl("FStorLocTransMP.aspx", new string[] { "TRANSNO" }, new string[] { txtTransNoQuery.Text }));
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                ((StorloctransDetailEX)obj).Transno,
                                ((StorloctransDetailEX)obj).DqmCode,
                                 ((StorloctransDetailEX)obj).MDesc,
                                ((StorloctransDetailEX)obj).Qty.ToString("G0"),
                                ((StorloctransDetailEX)obj).TransQty.ToString("G0")
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[]
                {
                                    "TransNo",
                                    "DQMcode",
                                    "Mdesc",
                                    "RequireQty",
                                    "Trans_Qty"                                    
                                   
                };
        }

        #endregion
    }
}
