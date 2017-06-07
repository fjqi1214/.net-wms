using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.WebQuery
{
    public partial class FIndexAchievingRateQP : BaseQPage
    {
        private System.ComponentModel.IContainer components;

        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private GridHelper _GridHelper = null;
        private WebQueryHelper _WebQueryHelper = null;

        public BenQGuru.eMES.Web.UserControl.eMESDate datStartDateQuery;
        public BenQGuru.eMES.Web.UserControl.eMESDate datEndDateQuery;

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
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this._GridHelper = new GridHelper(this.gridWebGrid);

            this._WebQueryHelper = new WebQueryHelper(this.cmdQuery, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1);
            this._WebQueryHelper.LoadGridDataSource += new EventHandler(WebQueryHelper_LoadGridDataSource);
            this._WebQueryHelper.DomainObjectToGridRow += new EventHandler(WebQueryHelper_DomainObjectToGridRow);
            this._WebQueryHelper.CheckRequireFields += new EventHandler(WebQueryHelper_CheckRequireFields);
            this._WebQueryHelper.GetExportHeadText += new EventHandler(WebQueryHelper_GetExportHeadText);
            this._WebQueryHelper.DomainObjectToExportRow += new EventHandler(WebQueryHelper_DomainObjectToExportRow);

            if (!this.IsPostBack)
            {
                this.InitPageLanguage(this.languageComponent1, false);

                this.InitQueryCondtion();
                this.InitWebGrid();

                this.columnChart.Visible = false;
            }
        }

        private void WebQueryHelper_LoadGridDataSource(object sender, EventArgs e)
        {
            QueryFacade1 queryFacade1 = new QueryFacade1(this.DataProvider);

            WebQueryEventArgs args = (WebQueryEventArgs)e;

            FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);

            args.RowCount = queryFacade1.QueryAchievingRateCount(
                FormatHelper.TODateInt(this.datStartDateQuery.Date_DateTime),
                FormatHelper.TODateInt(this.datEndDateQuery.Date_DateTime),
                this.txtBigSSCodeQuery.Text,
                this.txtMOCodeQuery.Text,
                this.txtItemCodeQuery.Text,
                this.txtMaterialModelCodeQuery.Text,
                this.ddlGoodSemiGoodQuery.SelectedValue);

            args.GridDataSource = queryFacade1.QueryAchievingRate(
                FormatHelper.TODateInt(this.datStartDateQuery.Date_DateTime),
                FormatHelper.TODateInt(this.datEndDateQuery.Date_DateTime),
                this.txtBigSSCodeQuery.Text,
                this.txtMOCodeQuery.Text,
                this.txtItemCodeQuery.Text,
                this.txtMaterialModelCodeQuery.Text,
                this.ddlGoodSemiGoodQuery.SelectedValue,
                args.StartRow,
                args.EndRow);

            ProcessOWC(queryFacade1.QueryAchievingRate(
                FormatHelper.TODateInt(this.datStartDateQuery.Date_DateTime),
                FormatHelper.TODateInt(this.datEndDateQuery.Date_DateTime),
                this.txtBigSSCodeQuery.Text,
                this.txtMOCodeQuery.Text,
                this.txtItemCodeQuery.Text,
                this.txtMaterialModelCodeQuery.Text,
                this.ddlGoodSemiGoodQuery.SelectedValue,
                int.MinValue,
                int.MaxValue));
        }

        private void WebQueryHelper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            DomainObjectToGridRowEventArgs args = (DomainObjectToGridRowEventArgs)e;

            if (args.DomainObject != null)
            {
                RptAchievingRate obj = (RptAchievingRate)args.DomainObject;
                args.GridRow = new UltraGridRow(new object[]{
                    FormatHelper.ToDateString(obj.ShiftDay),
                    obj.AchievingQty.ToString("0.00"),
                    obj.PlanQty.ToString("0.00"),
                    obj.AchievingRate.ToString("0.00%")
                });
            }
        }

        private void WebQueryHelper_CheckRequireFields(object sender, EventArgs e)
        {
            int startDate = 0;
            int endDate = 0;

            try
            {
                startDate = FormatHelper.TODateInt(this.datStartDateQuery.Date_DateTime);
                endDate = FormatHelper.TODateInt(this.datEndDateQuery.Date_DateTime);
            }
            catch { }

            if (startDate <= 0 || endDate <= 0)
            {
                throw new Exception("$Error_PleaseInputStartEndDate");
            }
        }

        private void WebQueryHelper_GetExportHeadText(object sender, EventArgs e)
        {
            ExportHeadEventArgs args = (ExportHeadEventArgs)e;
            args.Heads = new string[]{
	            "Date",
	            "AchievingQty",
	            "PlanQty",
	            "AchievingRate"
            };
        }

        private void WebQueryHelper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            DomainObjectToExportRowEventArgs args = (DomainObjectToExportRowEventArgs)e;

            if (args.DomainObject != null)
            {
                RptAchievingRate obj = (RptAchievingRate)args.DomainObject;

                args.ExportRow = new string[]{
                    FormatHelper.ToDateString(obj.ShiftDay),
                    obj.AchievingQty.ToString("0"),
                    obj.PlanQty.ToString("0"),
                    obj.AchievingRate.ToString("0.00%")
				};
            }
        }

        private void InitQueryCondtion()
        {
            this.ddlGoodSemiGoodQuery.Items.Clear();
            this.ddlGoodSemiGoodQuery.Items.Add(new ListItem("", ""));
            this.ddlGoodSemiGoodQuery.Items.Add(new ListItem(this.languageComponent1.GetString(ItemType.ITEMTYPE_FINISHEDPRODUCT), ItemType.ITEMTYPE_FINISHEDPRODUCT));
            this.ddlGoodSemiGoodQuery.Items.Add(new ListItem(this.languageComponent1.GetString(ItemType.ITEMTYPE_SEMIMANUFACTURE), ItemType.ITEMTYPE_SEMIMANUFACTURE));

            this.datStartDateQuery.Date_DateTime = DateTime.Now;
            this.datEndDateQuery.Date_DateTime = DateTime.Now;
        }

        private void InitWebGrid()
        {
            this._GridHelper.AddColumn("Date", "日期", null);
            this._GridHelper.AddColumn("AchievingQty", "完工数量", null);
            this._GridHelper.AddColumn("PlanQty", "计划数量", null);
            this._GridHelper.AddColumn("AchievingRate", "达成率", null);

            //多语言
            this._GridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void ProcessOWC(object[] dataSource)
        {
            if (dataSource != null)
            {
                this.columnChart.Visible = true;
                NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dataSource.Length];

                string propertyName = this.languageComponent1.GetString("AchievingRate");
                NewReportDomainObject item;
                //decimal temp = (dataSource[0] as RptAchievingRate).AchievingRate;

                for (int i = 0; i < dataSource.Length; i++)
                {
                    item = new NewReportDomainObject();
                    item.EAttribute1 = propertyName;
                    item.PeriodCode = FormatHelper.ToDateString((dataSource[i] as RptAchievingRate).ShiftDay) + ".";
                    item.TempValue = (dataSource[i] as RptAchievingRate).AchievingRate.ToString();
                    dateSourceForOWC[i] = item;
                    //if (temp < (dataSource[i] as RptAchievingRate).AchievingRate)
                    //{
                    //    temp = (dataSource[i] as RptAchievingRate).AchievingRate;
                    //}
                }
                
                //this.columnChart.YRangeMin = 0;
                //this.columnChart.YRangeMax = Convert.ToDouble(temp);

                this.columnChart.ChartGroupByString = "";
                this.columnChart.ChartTextFormatString = "<DATA_VALUE:0.00%>";
                this.columnChart.YLabelFormatString = "<DATA_VALUE:0.##%>";
                this.columnChart.DataType = true;
                this.columnChart.DataSource = dateSourceForOWC;
                this.columnChart.DataBind();
            }
            else
            {
                this.columnChart.Visible = false;
            }

            //this.OWCChartSpace1.ClearCharts();

            //if (dataSource != null)
            //{
            //    string serieName = this.languageComponent1.GetString("AchievingRate");
            //    List<string> categoryArray = new List<string>();
            //    List<object> valueArray = new List<object>();

            //    foreach (RptAchievingRate rptAchievingRate in dataSource)
            //    {
            //        categoryArray.Add(FormatHelper.ToDateString(rptAchievingRate.ShiftDay) + ".");
            //        valueArray.Add(rptAchievingRate.AchievingRate);
            //    }

            //    this.OWCChartSpace1.AddChart(true, serieName, categoryArray.ToArray(), valueArray.ToArray(), string.Empty);

            //    this.OWCChartSpace1.ChartType = OWCChartType.ColumnClustered;
            //    this.OWCChartSpace1.AxesLeftNumberFormat = "0%";
            //    this.OWCChartSpace1.ChartLeftMaximum = 1;
            //    this.OWCChartSpace1.Display = true;
            //}
        }

        #endregion
    }
}
