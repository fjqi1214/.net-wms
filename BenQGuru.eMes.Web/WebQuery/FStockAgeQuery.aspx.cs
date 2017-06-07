using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.WebQuery
{
    public partial class FStockAgeQuery : BaseQPage
    {
        private System.ComponentModel.IContainer components;
        private System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private GridHelper _GridHelper = null;
        private WebQueryHelper _WebQueryHelper = null;
        private PauseFacade _PauseFacade = null;
        private WarehouseFacade _WarehouseFacade = null;

        private object[] _AllInvPeriods;

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
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
            _GridHelper = new GridHelper(this.gridWebGrid);
            _PauseFacade = new PauseFacade(this.DataProvider);
            _WarehouseFacade = new WarehouseFacade(this.DataProvider);

            if (!this.IsPostBack)
            {
                this.InitPageLanguage(this.languageComponent1, false);
                InitDropDownList_PeiodGroup();
                InitDropDownList_FirstClass();

                InitWebGrid();
                this.chbSAPCompelete.Checked = true;
            }

            this._WebQueryHelper = new WebQueryHelper(this.cmdQuery, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1);
            this._WebQueryHelper.LoadGridDataSource += new EventHandler(WebQueryHelper_LoadGridDataSource);
            this._WebQueryHelper.DomainObjectToGridRow += new EventHandler(WebQueryHelper_DomainObjectToGridRow);
            this._WebQueryHelper.DomainObjectToExportRow += new EventHandler(WebQueryHelper_DomainObjectToExportRow);
        }

        private void InitDropDownList_PeiodGroup()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.drpPeiodGroupEdit);
            builder.HandleGetObjectList = new GetObjectListDelegate(this._WarehouseFacade.GetAllinvperiodGroup);
            builder.Build("PeiodGroup", "PeiodGroup");
            this.drpPeiodGroupEdit.Items.Insert(0, new ListItem("", ""));
        }

        private void InitDropDownList_FirstClass()
        {
            ItemFacade itemFacade = new ItemFacade(base.DataProvider);
            DropDownListBuilder builder = new DropDownListBuilder(this.drpFirstClassGroup);
            builder.HandleGetObjectList += new GetObjectListDelegate(itemFacade.GetItemFirstClass);
            builder.Build("FirstClass", "FirstClass");
            this.drpFirstClassGroup.Items.Insert(0, new ListItem("", ""));
        }

        #endregion

        #region WebGrid

        private void InitWebGrid()
        {
            this.gridWebGrid.Columns.Clear();

            _AllInvPeriods = _PauseFacade.QueryInvPeriodcode(drpPeiodGroupEdit.SelectedValue);

            _GridHelper.AddColumn("Company", "公司别", null);
            _GridHelper.AddColumn("Storage", "库别", null);
            _GridHelper.AddColumn("FirstClass", "一级分类", null);
            _GridHelper.AddColumn("CusOrderNo", "合同号", null); //合同号
            _GridHelper.AddColumn("MName", "出口国家", null);
            _GridHelper.AddColumn("MModelCode", "机型", null);
            _GridHelper.AddColumn("ItemCode", "产品代码", null);
            _GridHelper.AddColumn("ItemGrade", "产品档次", null);
            _GridHelper.AddColumn("CusItemCode", "客户料号", null);

            if (_AllInvPeriods != null)
            {
                foreach (InvPeriod invPeriod in _AllInvPeriods)
                {
                    this._GridHelper.AddColumn(invPeriod.InvPeriodCode, invPeriod.InvPeriodCode, null);
                    this.gridWebGrid.Columns.FromKey(invPeriod.InvPeriodCode).DataType = "System.Int32";
                }
            }

            _GridHelper.AddColumn("cnt", "总计", null);
            this.gridWebGrid.Columns.FromKey("cnt").DataType = "System.Int32";

            _GridHelper.AddDefaultColumn(false, false);
            _GridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void WebQueryHelper_LoadGridDataSource(object sender, EventArgs e)
        {
            InitWebGrid();

            if (CheckRequireFields())
            {
                //该页面不需要分页,做法是将页面元素注释了
                FacadeFactory facadeFactory = new FacadeFactory(this.DataProvider);

                object[] dataSource = facadeFactory.CreateQueryFacade1().QueryStockAge(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageTypeEdit.Text)),
                    this.drpPeiodGroupEdit.SelectedValue,
                    this.txtItemCodeEdit.Text,
                    FormatHelper.CleanString(this.txtOrderNoEdit.Text),
                    this.txtMmodelcode.Text,
                    this.drpFirstClassGroup.SelectedValue,
                    this.chbSAPCompelete.Checked,
                    1,//(e as WebQueryEventArgs).StartRow,
                    int.MaxValue //(e as WebQueryEventArgs).EndRow
                    );

                //(e as WebQueryEventArgs).RowCount = facadeFactory.CreateQueryFacade1().QueryStockAgeCount(
                //    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageTypeEdit.Text)),
                //    this.drpPeiodGroupEdit.SelectedValue,
                //    this.txtItemCodeEdit.Text,
                //    FormatHelper.CleanString(this.txtOrderNoEdit.Text),
                //    this.txtMmodelcode.Text,
                //    this.drpFirstClassGroup.SelectedValue,
                //    this.chbSAPCompelete.Checked
                //    );

                this.ProcessDataDourceToGrid(dataSource);
            }
        }

        private void WebQueryHelper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            StockAge stockAge = (StockAge)(e as DomainObjectToGridRowEventArgs).DomainObject;

            (e as DomainObjectToGridRowEventArgs).GridRow = new UltraGridRow(
                new object[]{
                    stockAge.Company,
                    stockAge.StorageCode,
                    stockAge.Firstclass,
                    stockAge.Cusorderno,
                    stockAge.MaterialName,
                    stockAge.MaterialModelCode,
                    stockAge.MaterialCode,
                    stockAge.Itemgrade,
                    stockAge.CustomerItemCode,
                    stockAge.Invperiodcode,
                    stockAge.Datefrom.ToString(),
                    stockAge.dateto.ToString(),
                    stockAge.CNT
                });
        }

        private void WebQueryHelper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            StockAge stockAge = (StockAge)(e as DomainObjectToExportRowEventArgs).DomainObject;

            (e as DomainObjectToExportRowEventArgs).ExportRow =
                new string[]{
                    stockAge.Company,
                    stockAge.StorageCode,
                    stockAge.Firstclass,
                    stockAge.Cusorderno,
                    stockAge.MaterialName,
                    stockAge.MaterialModelCode,
                    stockAge.MaterialCode,
                    stockAge.Itemgrade,
                    stockAge.CustomerItemCode, 
                    stockAge.Invperiodcode,
                    stockAge.Datefrom.ToString(),
                    stockAge.dateto.ToString(),
                    stockAge.CNT.ToString()
                };
        }

        private void ProcessDataDourceToGrid(object[] source)
        {
            InitWebGrid();
            this.gridWebGrid.Rows.Clear();

            if (source != null)
            {
                foreach (StockAge stockAge in source)
                {
                    UltraGridRow gridRow = GetRowToInput(stockAge);
                    int totalCount = GetCellInt(gridRow.Cells.FromKey("cnt"));

                    if (_AllInvPeriods != null)
                    {
                        foreach (InvPeriod invPeriodStandard in _AllInvPeriods)
                        {
                            int count = GetCellInt(gridRow.Cells.FromKey(invPeriodStandard.InvPeriodCode));

                            if (invPeriodStandard.InvPeriodCode == stockAge.Invperiodcode)
                            {
                                count += stockAge.CNT;
                                totalCount += stockAge.CNT;
                                gridRow.Cells.FromKey(invPeriodStandard.InvPeriodCode).Text = count.ToString();
                            }
                            else if (count == 0)
                            {
                                gridRow.Cells.FromKey(invPeriodStandard.InvPeriodCode).Text = count.ToString();
                            }
                        }
                    }

                    gridRow.Cells.FromKey("cnt").Text = totalCount.ToString();
                }
            }

            this.ProcessGridStyle();
        }

        private UltraGridRow GetRowToInput(StockAge stockAge)
        {
            UltraGridRow returnValue = null;

            foreach (UltraGridRow row in this.gridWebGrid.Rows)
            {
                if (row.Cells.FromKey("Company").Text.Trim().ToUpper() == stockAge.Company.Trim().ToUpper()
                    && row.Cells.FromKey("Storage").Text.Trim().ToUpper() == stockAge.StorageCode.Trim().ToUpper()
                    && row.Cells.FromKey("FirstClass").Text.Trim().ToUpper() == stockAge.Firstclass.Trim().ToUpper()
                    && row.Cells.FromKey("CusOrderNo").Text.Trim().ToUpper() == stockAge.Cusorderno.Trim().ToUpper()
                    && row.Cells.FromKey("MName").Text.Trim().ToUpper() == stockAge.MaterialName.Trim().ToUpper()
                    && row.Cells.FromKey("MModelCode").Text.Trim().ToUpper() == stockAge.MaterialModelCode.Trim().ToUpper()
                    && row.Cells.FromKey("ItemCode").Text.Trim().ToUpper() == stockAge.MaterialCode.Trim().ToUpper()
                    && row.Cells.FromKey("ItemGrade").Text.Trim().ToUpper() == stockAge.Itemgrade.Trim().ToUpper()
                    && row.Cells.FromKey("CusItemCode").Text.Trim().ToUpper() == stockAge.CustomerItemCode.Trim().ToUpper())
                {
                    returnValue = row;
                    break;
                }
            }

            if (returnValue == null)
            {
                returnValue = new UltraGridRow(new object[this.gridWebGrid.Columns.Count]);
                this.gridWebGrid.Rows.Add(returnValue);

                returnValue.Cells.FromKey("Company").Text = stockAge.Company;
                returnValue.Cells.FromKey("Storage").Text = stockAge.StorageCode;
                returnValue.Cells.FromKey("FirstClass").Text = stockAge.Firstclass;
                returnValue.Cells.FromKey("CusOrderNo").Text = stockAge.Cusorderno;
                returnValue.Cells.FromKey("MName").Text = stockAge.MaterialName.ToString();
                returnValue.Cells.FromKey("MModelCode").Text = stockAge.MaterialModelCode.ToString();
                returnValue.Cells.FromKey("ItemCode").Text = stockAge.MaterialCode.ToString();
                returnValue.Cells.FromKey("ItemGrade").Text = stockAge.Itemgrade.ToString();
                returnValue.Cells.FromKey("CusItemCode").Text = stockAge.CustomerItemCode.ToString();
            }

            return returnValue;
        }

        private int GetCellInt(UltraGridCell cell)
        {
            if (cell.Text == null || cell.Text.Trim().Length <= 0)
            {
                return 0;
            }

            int returnValue = 0;
            int.TryParse(cell.Text, out returnValue);
            return returnValue;
        }

        private void ProcessGridStyle()
        {
            try
            {
                GridItemStyle style = new GridItemStyle(true);
                style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
                for (int col = 1; col < this.gridWebGrid.Columns.Count - 1; col++)
                {
                    for (int row = 0; row < this.gridWebGrid.Rows.Count - 1; row++)
                    {
                        this.gridWebGrid.Rows[row].Cells[col].Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
                    }
                }
            }
            catch
            {
            }
        }

        private bool CheckRequireFields()
        {
            PageCheckManager manager = new PageCheckManager();

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Export

        protected void cmdGridExport_ServerClick(object sender, EventArgs e)
        {
            this.GridExport(this.gridWebGrid);
        }

        #endregion
    }
}
