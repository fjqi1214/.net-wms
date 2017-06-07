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
using System.Collections.Generic;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Web.UserControl;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FTSPerformanceQP 的摘要说明。
    /// </summary>
    public partial class FRMADefectQP : BaseQPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected ExcelExporter excelExporter = null;
        protected WebQueryHelperNew _helper = null;
        //protected GridHelper gridHelper = null;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, DtSource);

            this._helper = new WebQueryHelperNew(this.cmdQuery, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1, DtSource);
            this._helper.LoadGridDataSource += new EventHandler(_helper_LoadGridDataSource);
            this._helper.DomainObjectToGridRow += new EventHandler(_helper_DomainObjectToGridRow);
            this._helper.DomainObjectToExportRow += new EventHandler(_helper_DomainObjectToExportRow);
            this._helper.GetExportHeadText += new EventHandler(_helper_GetExportHeadText);
            //this._helper.GridCellClick += new EventHandler(_helper_GridCellClick);

            RadioButtonListBuilder builder = new RadioButtonListBuilder(
                new RMATimeKide(), this.rblSummaryTargetQuery, this.languageComponent1);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this._initialWebGrid();
                UCColumnChartProcess1.Visible = false;

                builder.Build(RMATimeKide.Day);

                this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
                this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;
            }

            RadioButtonListBuilder.FormatListControlStyle(this.rblSummaryTargetQuery, 50);
        }

        private void _initialWebGrid()
        {
            this.gridHelper.Grid.Columns.Clear();
            base.InitWebGrid();
            switch (this.rblSummaryTargetQuery.SelectedValue)
            {
                case RMATimeKide.Day:
                    this.gridHelper.AddColumn("timingtype_day", "天", null);
                    break;
                case RMATimeKide.Week:
                    this.gridHelper.AddColumn("timingtype_week", "周", null);
                    break;
                case RMATimeKide.Month:
                    this.gridHelper.AddColumn("timingtype_month", "月", null);
                    break;
                default:
                    this.gridHelper.AddColumn("timingtype_day", "天", null);
                    break;
            }
            this.gridHelper.AddColumn("RMATotalQty", "总件数", null);
            this.gridHelper.AddColumn("RMAQuantity", "RMA数量", null);
            this.gridHelper.AddColumn("RMAClosedQty", "结案数", null);
            this.gridHelper.AddColumn("RMAUnClosedQty", "未结案数", null);
            this.gridHelper.AddColumn("RMAAchieved", "达成率", null);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.gridWebGrid.Height = new Unit(200);
            //			this.gridHelper.Grid.Bands[0].ColFootersVisible =ShowMarginInfo.Yes ;
            //			this.gridHelper.Grid.Columns[0].FooterText = this.languageComponent1.GetString("Summary") ;
            //			this.gridHelper.Grid.Columns.FromKey("RMATotalQty").FooterTotal = SummaryInfo.Sum ;
            //			this.gridHelper.Grid.Columns.FromKey("RMAQuantity").FooterTotal = SummaryInfo.Sum ;
            //			this.gridHelper.Grid.Columns.FromKey("RMAClosedQty").FooterTotal = SummaryInfo.Sum ;
            //			this.gridHelper.Grid.Columns.FromKey("RMAUnClosedQty").FooterTotal = SummaryInfo.Sum ;
            //			this.gridHelper.Grid.Columns.FromKey("RMAAchieved").FooterTotal = SummaryInfo.Avg ;
            //			this.gridHelper.Grid.Bands[0].FooterStyle = this.gridHelper.Grid.Bands[0].RowStyle ;
        }

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
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        private bool _checkRequireFields()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new DateRangeCheck(this.lblStartDateQuery, this.dateStartDateQuery.Text, this.lblEndDateQuery, this.dateEndDateQuery.Text, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }


        private void _helper_LoadGridDataSource(object sender, EventArgs e)
        {


            if (this._checkRequireFields())
            {
                _initialWebGrid();
                FacadeFactory facadeFactory = new FacadeFactory(this.DataProvider);
                object[] dataSource = facadeFactory.CreateQueryRMATSFacade().QueryRMADefect(
                    rblSummaryTargetQuery.SelectedValue,
                    FormatHelper.TODateInt(this.dateStartDateQuery.Text),
                    FormatHelper.TODateInt(this.dateEndDateQuery.Text),
                    (e as WebQueryEventArgsNew).StartRow,
                    (e as WebQueryEventArgsNew).EndRow
                    ); ;

                (e as WebQueryEventArgsNew).GridDataSource = dataSource;

                (e as WebQueryEventArgsNew).RowCount =
                    facadeFactory.CreateQueryRMATSFacade().QueryRMADefectCount(
                    rblSummaryTargetQuery.SelectedValue,
                    FormatHelper.TODateInt(this.dateStartDateQuery.Text),
                    FormatHelper.TODateInt(this.dateEndDateQuery.Text));

                this._processOWC(dataSource);
            }
        }

        private void _processOWC(object[] dataSource)
        {
            //this.OWCChartSpace1.ClearCharts();

            //if( dataSource != null )
            //{
            //    string[] categories = new string[ dataSource.Length ];
            //    object[] values = new object[ dataSource.Length ];
            //    object[] ParetoValues = new object[ dataSource.Length ];

            //    for(int i = 0;i<dataSource.Length;i++)
            //    {
            //        string timeType = "";
            //        switch( this.rblSummaryTargetQuery.SelectedValue )
            //        {
            //            case RMATimeKide.Day:
            //                timeType = FormatHelper.ToDateString((dataSource[i] as QDORMADefect).TimeType);
            //                break;
            //            case RMATimeKide.Week:
            //                timeType = (dataSource[i] as QDORMADefect).TimeType.ToString();
            //                break;
            //            case RMATimeKide.Month:
            //                timeType = (dataSource[i] as QDORMADefect).TimeType.ToString();
            //                break;
            //            default:
            //                timeType = "";
            //                break;
            //        }
            //        categories[i] = timeType;
            //        values[i] = (dataSource[i] as QDORMADefect).TotalQty;
            //        ParetoValues[i] = (dataSource[i] as QDORMADefect).RMAQuantity.ToString(); 
            //    }

            //    //this.OWCChartSpace1.ChartCombinationType = OWCChartCombinationType.OWCCombinationPareto;
            //    this.OWCChartSpace1.AddChart(this.languageComponent1.GetString("RMATotalQty"), categories, values );	
            //    this.OWCChartSpace1.AddChart(this.languageComponent1.GetString("RMAQuantity"), categories, ParetoValues, OWCChartType.LineMarkers, true);

            //    this.OWCChartSpace1.Display = true;
            //}

            if (dataSource != null)
            {

                string[] categories = new string[dataSource.Length];
                object[] values = new object[dataSource.Length];
                object[] ParetoValues = new object[dataSource.Length];

                for (int i = 0; i < dataSource.Length; i++)
                {
                    categories[i] = (dataSource[i] as QDORMADefect).TimeType.ToString();
                    values[i] = (dataSource[i] as QDORMADefect).TotalQty;
                    ParetoValues[i] = (dataSource[i] as QDORMADefect).RMAQuantity;
                }

                //设置首页报表的大小
                if (ViewState["Width"] != null)
                {
                    UCColumnChartProcess1.Width = int.Parse(ViewState["Width"].ToString());
                }

                if (ViewState["Height"] != null)
                {
                    UCColumnChartProcess1.Height = int.Parse(ViewState["Height"].ToString());
                }
                //end

                NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dataSource.Length];

                for (int i = 0; i < dateSourceForOWC.Length; i++)
                {
                    dateSourceForOWC[i] = new NewReportDomainObject();
                }

                for (int i = 0; i < dateSourceForOWC.Length; i++)
                {
                    int j = 0;
                    if (i < dateSourceForOWC.Length)
                    {

                        dateSourceForOWC[i].PeriodCode = (dataSource[i] as QDORMADefect).TimeType.ToString();
                        dateSourceForOWC[i].TempValue = (dataSource[i] as QDORMADefect).RMAQuantity.ToString();
                        dateSourceForOWC[i].PlanQty = (dataSource[i] as QDORMADefect).TotalQty;
                        dateSourceForOWC[i].EAttribute1 = this.languageComponent1.GetString("RMAQuantity");
                        dateSourceForOWC[i].TimeString = (dataSource[i] as QDORMADefect).TimeType.ToString();


                    }


                }



                switch (this.rblSummaryTargetQuery.SelectedValue)
                {
                    case RMATimeKide.Day:
                        this.UCColumnChartProcess1.ChartGroupByString = "shiftday";
                        break;
                    case RMATimeKide.Week:
                        this.UCColumnChartProcess1.ChartGroupByString = "dweek";
                        break;
                    case RMATimeKide.Month:
                        this.UCColumnChartProcess1.ChartGroupByString = "dmonth";
                        break;
                }


                this.UCColumnChartProcess1.DataType = true;
                this.UCColumnChartProcess1.YLabelFormatString = "<DATA_VALUE:0.##>";
                this.UCColumnChartProcess1.ChartTextFormatString = "<DATA_VALUE:0.##>";
                this.UCColumnChartProcess1.DataSource = dateSourceForOWC;
                this.UCColumnChartProcess1.DataBindQty(this.languageComponent1.GetString("TotalRMAQty"));
                this.UCColumnChartProcess1.Visible = true;
            }
            else
            {
                this.UCColumnChartProcess1.Visible = false;
            }

        }


        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                QDORMADefect obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as QDORMADefect;
                string timeType = "";
                DataRow row = DtSource.NewRow();

                switch (this.rblSummaryTargetQuery.SelectedValue)
                {
                    case RMATimeKide.Day:
                        row["timingtype_day"] = FormatHelper.ToDateString(obj.TimeType);
                        break;
                    case RMATimeKide.Week:
                        row["timingtype_week"] = obj.TimeType.ToString();
                        break;
                    case RMATimeKide.Month:
                        row["timingtype_month"] = obj.TimeType.ToString();
                        break;
                    default:
                        row["timingtype_day"] = "";
                        break;
                }
                int unColsedQTY = obj.TotalQty - obj.ClosedQty;
                decimal achieved = Convert.ToDecimal(obj.ClosedQty) / Convert.ToDecimal(obj.TotalQty);

                row["RMATotalQty"] = obj.TotalQty.ToString();
                row["RMAQuantity"] = obj.RMAQuantity.ToString();
                row["RMAClosedQty"] = obj.ClosedQty.ToString();
                row["RMAUnClosedQty"] = unColsedQTY.ToString();
                row["RMAAchieved"] = achieved.ToString("##.##%");

                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;

            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                QDORMADefect obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as QDORMADefect;
                string timeType = "";
                switch (this.rblSummaryTargetQuery.SelectedValue)
                {
                    case RMATimeKide.Day:
                        timeType = FormatHelper.ToDateString(obj.TimeType);
                        break;
                    case RMATimeKide.Week:
                        timeType = obj.TimeType.ToString();
                        break;
                    case RMATimeKide.Month:
                        timeType = obj.TimeType.ToString();
                        break;
                    default:
                        timeType = "";
                        break;
                }
                int unColsedQTY = obj.TotalQty - obj.ClosedQty;
                decimal achieved = Convert.ToDecimal(obj.ClosedQty) / Convert.ToDecimal(obj.TotalQty);
                (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                    new string[]{
									timeType,
									obj.TotalQty.ToString(),
									obj.RMAQuantity.ToString(),
									obj.ClosedQty.ToString(),
									unColsedQTY.ToString(),
									achieved.ToString("##.##%")	
								};
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            string timeType = RMATimeKide.Day;
            switch (this.rblSummaryTargetQuery.SelectedValue)
            {
                case RMATimeKide.Day:
                    timeType = RMATimeKide.Day;
                    break;
                case RMATimeKide.Week:
                    timeType = RMATimeKide.Week;
                    break;
                case RMATimeKide.Month:
                    timeType = RMATimeKide.Month;
                    break;
                default:
                    timeType = RMATimeKide.Day;
                    break;
            }
            (e as ExportHeadEventArgsNew).Heads =
                new string[]{
								timeType,
								"RMATotalQty",
								"RMAQuantity",
								"RMAClosedQty",
								"RMAUnClosedQty",
								"RMAAchieved"
							};
        }
        protected override void Grid_ClickCell(GridRecord row, string command)
        {
            if (command == "")
            {
                return;
            }
        }


    }
}
