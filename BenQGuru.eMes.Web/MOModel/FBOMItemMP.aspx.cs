#region system
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
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// MBOMItem 的摘要说明。
    /// </summary>
    public partial class MBOMItem : BaseMPageMinus
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private ItemFacade _itemFacade;//= FacadeFactory.CreateItemFacade();
        private SBOMFacade _sbomFacade;//= FacadeFactory.CreateSBOMFacade();
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;


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
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;

        }
        #endregion

        #region private method
        private void InitViewPanel()
        {
            if (Request.Params["itemcode"] != null)
            {
                if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
                //				ItemCode = Request.Params["itemcode"].ToString();
                object item = _itemFacade.GetItem(Request.Params["itemcode"].ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID);

                if (item != null)
                {
                    txtItemCode.Text = ((Item)item).ItemCode;
                    txtItemName.Text = ((Item)item).ItemName;
                    txtItemType.Text = this.languageComponent1.GetString(((Item)item).ItemType.ToString());
                    txtItemCode.ReadOnly = true;
                    txtItemName.ReadOnly = true;
                    txtItemType.ReadOnly = true;
                }
                else
                {
                    txtItemCode.Text = string.Empty;
                    txtItemName.Text = string.Empty;
                    txtItemType.Text = string.Empty;
                    txtItemCode.ReadOnly = true;
                    txtItemName.ReadOnly = true;
                    txtItemType.ReadOnly = true;
                }
            }
        }



        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_sbomFacade == null) { _sbomFacade = new FacadeFactory(base.DataProvider).CreateSBOMFacade(); }

            string version = string.Empty;
            if (this.DropdownlistSBOMVersionQuery.SelectedIndex >= 0)
            {
                version = DropdownlistSBOMVersionQuery.SelectedValue;
            }

            return this._sbomFacade.GetSBOM(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCode.Text.Trim())), version, inclusive, exclusive);
        }
        private int GetRowCount()
        {
            if (_sbomFacade == null) { _sbomFacade = new FacadeFactory(base.DataProvider).CreateSBOMFacade(); }

            string version = string.Empty;
            if (this.DropdownlistSBOMVersionQuery.SelectedIndex >= 0)
            {
                version = DropdownlistSBOMVersionQuery.SelectedValue;
            }

            return this._sbomFacade.GetSBOMCounts(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCode.Text.Trim())), version);
        }
        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            // 2005-04-06
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
								   ((SBOM)obj).SBOMItemCode.ToString(),
								   ((SBOM)obj).EAttribute1.ToString(),
								   ((SBOM)obj).SBOMSourceItemCode.ToString(),
								   ((SBOM)obj).SBOMItemQty.ToString(),
								   ((SBOM)obj).SBOMItemUOM == null ? "" : ((SBOM)obj).SBOMItemUOM,
								   ((SBOM)obj).Location.ToString(),
                                   ((SBOM)obj).SBOMVersion.ToString(),
								   FormatHelper.ToDateString(((SBOM)obj).SBOMItemEffectiveDate),
								   FormatHelper.ToDateString(((SBOM)obj).SBOMItemInvalidDate) };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
									"SBOMItemCode",
									"SBOMItemName",	
									"SBOMSourceItemCode",
									"SBOMItemQty",
									"SBOMItemUOM",
									"SBOMItemLocation",
                                    "BOMVersion",
									"EffectiveDate",	
									"IneffectiveDate"
								 };
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("SBOMItemCode", "子阶料料号", null);
            this.gridHelper.AddColumn("SBOMItemName", "子阶料名称", null);
            this.gridHelper.AddColumn("SBOMSourceItemCode", "首选料", null);
            this.gridHelper.AddColumn("SBOMItemQty", "单机用量", null);
            this.gridHelper.AddColumn("SBOMItemUOM", "计量单位", null);
            this.gridHelper.AddColumn("SBOMItemLocation", "位号", null);
            this.gridHelper.AddColumn("ECNNO", "ECN号码", null);
            this.gridHelper.AddColumn("BOMVersion", "BOM版本", null);
            this.gridHelper.AddColumn("EffectiveDate", "生效日期", null);
            this.gridHelper.AddColumn("IneffectiveDate", "失效日期", null);

            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridWebGrid.Columns.FromKey("ItemCode").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ECNNO").Hidden = true;
        }

        #endregion

        #region page events
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.OpenConnection();

                try
                {
                    // 初始化界面UI
                    this.InitUI();
                    InitViewPanel();
                    this.InitWebGrid();
                    this.LoadDropdownlistSBOMVersionQuery();
                    this.RequestData();
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.CloseConnection();
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.AutoCloseConnection = true;
                }

                //this.pagerSizeSelector.Readonly = true;
            }
        }
        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FBOMP.aspx"));
        }

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_sbomFacade == null) { _sbomFacade = new FacadeFactory(base.DataProvider).CreateSBOMFacade(); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object item = this.GetEditObject(row);
                    if (item != null)
                    {
                        items.Add((SBOM)item);
                    }
                }

                this._sbomFacade.DeleteSBOM((SBOM[])items.ToArray(typeof(SBOM)));

                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void DropdownlistSBOMVersionQuery_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.RequestData();
        }

        private void LoadDropdownlistSBOMVersionQuery()
        {
            this.DropdownlistSBOMVersionQuery.Items.Clear();

            if (_sbomFacade == null) { _sbomFacade = new FacadeFactory(base.DataProvider).CreateSBOMFacade(); }
            //object[] controls = _sbomFacade.GetSBOM(Request.Params["itemcode"].ToString(), 0, int.MaxValue);
            object[] controls = _sbomFacade.GetAllSBOMVersion(Request.Params["itemcode"].ToString().ToUpper(), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (controls == null || controls.Length == 0) return;
            for (int i = 0; i < controls.Length; i++)
            {
                string version = ((SBOM)controls[i]).SBOMVersion;
                if (this.DropdownlistSBOMVersionQuery.Items.FindByText(version) == null)
                {
                    this.DropdownlistSBOMVersionQuery.Items.Add(version);
                }
            }

            DropdownlistSBOMVersionQuery.SelectedIndex = 0;
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ItemCode"] = ((SBOM)obj).ItemCode.ToString();
            row["SBOMItemCode"] = ((SBOM)obj).SBOMItemCode.ToString();
            row["SBOMItemName"] = ((SBOM)obj).EAttribute1.ToString();
            row["SBOMSourceItemCode"] = ((SBOM)obj).SBOMSourceItemCode.ToString();
            row["SBOMItemQty"] = ((SBOM)obj).SBOMItemQty.ToString();
            row["SBOMItemUOM"] = (((SBOM)obj).SBOMItemUOM == null ? "" : ((SBOM)obj).SBOMItemUOM);
            row["SBOMItemLocation"] = ((SBOM)obj).Location.ToString();
            row["ECNNO"] = ((SBOM)obj).SBOMItemECN.ToString();
            row["BOMVersion"] = ((SBOM)obj).SBOMVersion.ToString();
            row["EffectiveDate"] = FormatHelper.ToDateString(((SBOM)obj).SBOMItemEffectiveDate);
            row["IneffectiveDate"] = FormatHelper.ToDateString(((SBOM)obj).SBOMItemInvalidDate);
            return row;
        }

        private object GetEditObject(GridRecord row)
        {
            if (_sbomFacade == null) { _sbomFacade = new FacadeFactory(base.DataProvider).CreateSBOMFacade(); }
            //object obj = this._sbomFacade.GetSBOM(row.Cells.FromKey("ItemCode").Text,row.Cells.FromKey("SBOMItemCode").Text,row.Cells.FromKey("SBOMSourceItemCode").Text,row.Cells.FromKey("SBOMItemQty").Text);
            DateTime effDate = DateTime.Parse(row.Items.FindItemByKey("EffectiveDate").Text);

            string version = string.Empty;
            if (this.DropdownlistSBOMVersionQuery.SelectedIndex >= 0)
            {
                version = DropdownlistSBOMVersionQuery.SelectedValue;
            }

            object obj = this._sbomFacade.GetSBOM(row.Items.FindItemByKey("ItemCode").Value.ToString(), row.Items.FindItemByKey("SBOMItemCode").Value.ToString(), row.Items.FindItemByKey("SBOMSourceItemCode").Value.ToString(), row.Items.FindItemByKey("SBOMItemQty").Value.ToString(), FormatHelper.TODateInt(effDate.Date).ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID, version);
            if (obj != null)
            {
                return (SBOM)obj;
            }

            return null;
        }
        #endregion
    }
}
