using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Config;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.Web.ReportCenter.UserControls;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.Web.ReportCenter
{
    public partial class FNewReportPerformanceManHourPerProductQP : BaseQPageNew
    {
        #region 页面初始化

        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private string preferredTable = "tblmaterial,tblmo,tblres,tblline2crew,tblitemclass,tbltimedimension,**";

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

        private void InitWhereControls()
        {
            this.UCWhereConditions1.SetControlPosition(0, 0, UCWhereConditions1.PanelGoodSemiGoodWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 1, UCWhereConditions1.PanelItemCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(1, 0, UCWhereConditions1.PanelBigSSCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 1, UCWhereConditions1.PanelSSCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(2, 0, UCWhereConditions1.PanelStartDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 1, UCWhereConditions1.PanelEndDateWhere.ID);
        }

        private void InitGroupControls()
        {
            this.UCGroupConditions1.ShowShift = true;
            this.UCGroupConditions1.ShowShiftDay = true;
            this.UCGroupConditions1.ShowWeek = true;
            this.UCGroupConditions1.ShowMonth = true;

            this.UCGroupConditions1.ShowByTimePanel = true;

            this.UCGroupConditions1.ShowFacCode = true;
            this.UCGroupConditions1.ShowBigSSCode = true;
            this.UCGroupConditions1.ShowSSCode = true;

            this.UCGroupConditions1.ShowItemCode = true;
            this.UCGroupConditions1.ShowCrewCode = true;

            this.UCGroupConditions1.ShowManHourPanel = true;
        }

        private void InitResultControls()
        {
            //this.gridWebGrid.Visible = false;
            //this.OWCChartSpace1.Display = false;
            this.cmdGridExport.Visible = false;
            //this.OWCPivotTable1.Display = false;
        }

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);

            //初始化控件的位置和可见性
            InitWhereControls();
            InitGroupControls();
            InitResultControls();
        }

        #endregion

        #region 公用属性和事件处理

        private void LoadDisplayControls()
        {
            if (!this.IsPostBack)
            {
                List<ListItem> displayList = new List<ListItem>();
                displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.Grid), NewReportDisplayType.Grid));
                displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.LineChart), NewReportDisplayType.LineChart));
                this.UCDisplayConditions1.DisplayList = displayList;

                if (this.Request.Params["Width"] != null)
                {
                    ViewState["Width"] = this.Request.Params["Width"];
                }

                if (this.Request.Params["Height"] != null)
                {
                    ViewState["Height"] = this.Request.Params["Height"];
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gridHelper = new GridHelperNew(gridWebGrid, DtSource);
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.UCWhereConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCGroupConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCDisplayConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                //this.OWCPivotTable1.LanguageComponent = this.languageComponent1;

                this.columnChart.Visible = false;
                this.lineChart.Visible = false;

                this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;
            }

            //加载控件的值
            LoadDisplayControls();

            //根据是否选择车间设定一些控件的可见性
            SetControlByFacCode(UCGroupConditions1.FacCodeChecked);
            this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
            this.gridWebGrid.Behaviors.Sorting.Enabled = false;

        }

        protected override void OnPreRender(EventArgs e)
        {
            ReportPageHelper.SetControlValue(this, this.Request.Params);
            ReportPageHelper.DoQueryForBSHome(this, this.Request.Params, this.DoQuery);

            if (this.AutoRefresh)
            {
                this.DoQuery();
            }

            base.OnPreRender(e);
        }

        private void SetControlByFacCode(bool facCodeChecked)
        {
            UCGroupConditions1.ShowIncludeIndirectManHour = facCodeChecked;

            UCGroupConditions1.ShowItemCode = !facCodeChecked;
            UCGroupConditions1.ShowBigSSCode = !facCodeChecked;
            UCGroupConditions1.ShowSSCode = !facCodeChecked;

            UCWhereConditions1.PanelGoodSemiGoodWhere.Visible = !facCodeChecked;
            UCWhereConditions1.PanelItemCodeWhere.Visible = !facCodeChecked;

            UCWhereConditions1.PanelBigSSCodeWhere.Visible = !facCodeChecked;
            UCWhereConditions1.PanelSSCodeWhere.Visible = !facCodeChecked;

            if (facCodeChecked)
            {
                preferredTable = "tbltimedimension,**,tblmaterial,tblmo,tblres,tblline2crew,tblitemclass";
            }
            else
            {
                preferredTable = "tblmaterial,tblmo,tblres,tblline2crew,tblitemclass,tbltimedimension,**";
            }
        }

        public bool AutoRefresh
        {
            get
            {
                if (this.ViewState["AutoRefresh"] != null)
                {
                    try
                    {
                        return bool.Parse(this.ViewState["AutoRefresh"].ToString());
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
                this.ViewState["AutoRefresh"] = value.ToString();

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

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.DoQuery();
            this.GridExport(this.gridWebGrid);
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.DoQuery();
        }

        protected void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
        {
            this.DoQuery();
        }

        #endregion

        #region 使用ReportSQLEngine相关的函数

        private object[] LoadDataSource()
        {
            int dateAdjust = 0;
            bool roundDate = false;

            string byTimeType = UCGroupConditions1.UserSelectByTimeType;

            string groupFieldsX = this.UCGroupConditions1.GetGroupFieldList(preferredTable, "X");
            string groupFieldsY = this.UCGroupConditions1.GetGroupFieldList(preferredTable, "Y");

            ReportSQLHelper sqlHelper = new ReportSQLHelper(this.DataProvider);
            string sql = sqlHelper.GetPerformanceReportSQL(
                UCGroupConditions1.FacCodeChecked,
                UCGroupConditions1.FacCodeChecked || !UCGroupConditions1.SSChecked,
                UCGroupConditions1.ExcludeReworkOutputChecked,
                UCGroupConditions1.ExcludeLostManHourChecked,
                UCGroupConditions1.ShowIncludeIndirectManHour && UCGroupConditions1.IncludeIndirectManHourChecked,
                UCWhereConditions1.GetWhereSQLStatement(preferredTable, byTimeType, roundDate, dateAdjust),
                groupFieldsX,
                groupFieldsY,
                sqlHelper.GetFormularForManHourPerProduct(UCGroupConditions1.ExcludeLostManHourChecked, UCGroupConditions1.ShowIncludeIndirectManHour && UCGroupConditions1.IncludeIndirectManHourChecked)
            );


            ReportSQLEngine engine = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            return engine.GetReportDataSource(sql, byTimeType, dateAdjust);
        }

        #endregion

        #region 相关的函数

        private string[] GetOWCSchema()
        {
            string[] rows = GetRows().ToArray();
            string[] columns = GetColumns().ToArray();

            ArrayList schemaList = new ArrayList();
            foreach (string row in rows)
            {
                schemaList.Add(row);
            }
            foreach (string column in columns)
            {
                schemaList.Add(column);
            }

            schemaList.Add("ManHourPerProduct");
            schemaList.Add("EAttribute1");


            return (string[])schemaList.ToArray(typeof(string));
        }

        private List<string> GetColumns()
        {
            List<string> returnValue = new List<string>();

            string rowString = UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "X");

            if (rowString.Trim().Length > 0)
            {

                returnValue.AddRange(rowString.Split(','));

                for (int i = 0; i < returnValue.Count; i++)
                {
                    returnValue[i] = DomainObjectUtility.GetPropertyNameByFieldName(typeof(NewReportDomainObject), returnValue[i]);
                }
            }

            return returnValue;
        }

        private List<string> GetRows()
        {
            List<string> returnValue = new List<string>();

            string columnString = UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "Y");

            if (columnString.Trim().Length > 0)
            {

                returnValue.AddRange(columnString.Split(','));

                for (int i = 0; i < returnValue.Count; i++)
                {
                    returnValue[i] = DomainObjectUtility.GetPropertyNameByFieldName(typeof(NewReportDomainObject), returnValue[i]);
                }
            }

            return returnValue;
        }

        #endregion

        private bool CheckBeforeQuery()
        {
            return true;
        }

        protected override void DoQuery()
        {
            base.DoQuery();

            if (this.CheckBeforeQuery())
            {
                this.AutoRefresh = this.chbRefreshAuto.Checked;

                string byTimeType = this.UCGroupConditions1.UserSelectByTimeType.Trim().ToLower();
                object[] dateSource = null;

                //一般数据
                dateSource = this.LoadDataSource();
                if (dateSource == null || dateSource.Length <= 0)
                {
                    this.gridWebGrid.Visible = false;
                    //this.OWCChartSpace1.Display = false;
                    this.cmdGridExport.Visible = false;

                    this.columnChart.Visible = false;
                    this.lineChart.Visible = false;

                    ReportPageHelper.SetPageScrollToBottom(this);
                    return;
                }

                //数据加载到Grid
                List<string> fixedColumnList = GetRows();
                List<ReportGridDim3Property> dim3PropertyList = new List<ReportGridDim3Property>();

                dim3PropertyList.Add(new ReportGridDim3Property("ManHourSum", "0.0000", "SUM", "SUM", false));
                dim3PropertyList.Add(new ReportGridDim3Property("StandardQty", "0.0000", "SUM", "SUM", false));
                if (this.UCGroupConditions1.ExcludeLostManHourChecked)
                {
                    dim3PropertyList.Add(new ReportGridDim3Property("InvalidManHour", "0.0000", "SUM", "SUM", false));
                }
                if (this.UCGroupConditions1.ShowIncludeIndirectManHour && this.UCGroupConditions1.IncludeIndirectManHourChecked)
                {
                    dim3PropertyList.Add(new ReportGridDim3Property("IndirectManHour", "0.0000", "SUM", "SUM", false));
                }
                dim3PropertyList.Add(new ReportGridDim3Property("ManHourPerProduct", "0.0000", "SUM", "SUM", false));

                ReportGridHelperNew reportGridHelper = new ReportGridHelperNew(this.DataProvider, this.languageComponent1, this.gridWebGrid,DtSource);

                reportGridHelper.DataSource = dateSource;
                reportGridHelper.DataSourceForCompare = null;
                reportGridHelper.Dim1PropertyList = fixedColumnList;
                reportGridHelper.Dim3PropertyList = dim3PropertyList;
                reportGridHelper.HasDim3PropertyNameRowColumn = true;
                reportGridHelper.ByTimeType = byTimeType;

                reportGridHelper.ShowGrid();
                base.InitWebGrid();

                this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
                this.gridWebGrid.Behaviors.Sorting.Enabled = false;


                //修饰汇总行和汇总列
                ModifySummaryItems();

                //获取表格和图示
                if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.Grid)
                {
                    this.gridWebGrid.Visible = true;
                    //this.OWCChartSpace1.Display = false;
                    this.cmdGridExport.Visible = true;

                    this.columnChart.Visible = false;
                    this.lineChart.Visible = false;
                }
                else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.LineChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.PieChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                {
                    List<string> rowPropertyList = GetColumns();
                    List<string> columnPropertyList = GetRows();
                    columnPropertyList.Add("EAttribute1");
                    List<string> dataPropertyList = new List<string>();
                    dataPropertyList.Add("ManHourPerProduct");

                    NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dateSource.Length];
                    dateSource.CopyTo(dateSourceForOWC, 0);
                   
                    string propertyName = this.languageComponent1.GetString(dataPropertyList[0]);
                    foreach (NewReportDomainObject domainObject in dateSourceForOWC)
                    {
                        domainObject.EAttribute1 = propertyName + domainObject.EAttribute1;
                    }

                    foreach (NewReportDomainObject obj in dateSourceForOWC)
                    {
                        obj.TempValue = obj.ManHourPerProduct.ToString();
                        //时段、班次、天、周、月、年
                        if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Period)
                        {
                            obj.PeriodCode = obj.PeriodCode.ToString();
                        }

                        if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Shift)
                        {
                            obj.PeriodCode = obj.ShiftCode.ToString();
                        }

                        if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.ShiftDay)
                        {
                            obj.PeriodCode = obj.ShiftDay.ToString();
                        }

                        if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Week)
                        {
                            obj.PeriodCode = obj.Week.ToString();
                        }

                        if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Month)
                        {
                            obj.PeriodCode = obj.Month.ToString();
                        }

                        if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Year)
                        {
                            obj.PeriodCode = obj.Year.ToString();
                        }
                    }

                    if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.LineChart)
                    {
                        this.columnChart.Visible = false;
                        this.lineChart.Visible = true;

                        lineChart.ChartGroupByString = UCGroupConditions1.GetCheckedColumnsString();

                        //设置首页报表的大小
                        if (ViewState["Width"] != null)
                        {
                            lineChart.Width = int.Parse(ViewState["Width"].ToString());
                        }

                        if (ViewState["Height"] != null)
                        {
                            lineChart.Height = int.Parse(ViewState["Height"].ToString());
                        }
                        //end

                        this.lineChart.ChartTextFormatString = "<DATA_VALUE:0.0000>";
                        this.lineChart.YLabelFormatString = "<DATA_VALUE:0.0000>";
                        this.lineChart.DataType = true;
                        this.lineChart.DataSource = dateSourceForOWC;
                        this.lineChart.DataBind();
                    }
                    else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                    {
                        this.columnChart.Visible = true;
                        this.lineChart.Visible = false;

                        columnChart.ChartGroupByString = UCGroupConditions1.GetCheckedColumnsString();

                        //设置首页报表的大小
                        if (ViewState["Width"] != null)
                        {
                            columnChart.Width = int.Parse(ViewState["Width"].ToString());
                        }

                        if (ViewState["Height"] != null)
                        {
                            columnChart.Height = int.Parse(ViewState["Height"].ToString());
                        }
                        //end

                        this.columnChart.ChartTextFormatString = "<DATA_VALUE:0.##%>";
                        this.columnChart.YLabelFormatString = "<DATA_VALUE:#0.##%>";
                        this.columnChart.DataType = true;
                        this.columnChart.DataSource = dateSourceForOWC;
                        this.columnChart.DataBind();
                    }
                    else
                    {
                        this.columnChart.Visible = false;
                        this.lineChart.Visible = false;
                    }

                    this.gridWebGrid.Visible = false;
                    this.cmdGridExport.Visible = false;

                    //List<string> rowPropertyList = GetColumns();
                    //List<string> columnPropertyList = GetRows();
                    //columnPropertyList.Add("EAttribute1");
                    //List<string> dataPropertyList = new List<string>();
                    //dataPropertyList.Add("ManHourPerProduct");

                    //NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dateSource.Length];
                    //dateSource.CopyTo(dateSourceForOWC, 0);

                    //string propertyName = this.languageComponent1.GetString(dataPropertyList[0]);
                    //foreach (NewReportDomainObject domainObject in dateSourceForOWC)
                    //{
                    //    domainObject.EAttribute1 = propertyName + domainObject.EAttribute1;
                    //}

                    //ReportChartHelper reportChartHelper = new ReportChartHelper(this.OWCPivotTable1, this.OWCChartSpace1, this.languageComponent1);
                    //reportChartHelper.DataSource = dateSourceForOWC;
                    //reportChartHelper.RowPropertyList = rowPropertyList.ToArray();
                    //reportChartHelper.ColumnPropertyList = columnPropertyList.ToArray();
                    //reportChartHelper.DataPropertyList = dataPropertyList.ToArray();
                    //reportChartHelper.DataPropertyFormatList = new string[] { "0.0000" };
                    //reportChartHelper.UserOWCChartType = UCDisplayConditions1.GetDisplayType().Trim().ToLower();

                    //reportChartHelper.LoadOWCChart();

                    //this.gridWebGrid.Visible = false;
                    //this.OWCChartSpace1.Display = true;
                    //this.cmdGridExport.Visible = false;
                }

                ReportPageHelper.SetPageScrollToBottom(this);
            }
            else
            {
                this.chbRefreshAuto.Checked = false;
                this.AutoRefresh = false;
            }
        }

        private void ModifySummaryItems()
        {
            int excludeInvalidCount = 0;
            int includeIndirectCount = 0;
            if (UCGroupConditions1.ExcludeLostManHourChecked)
            {
                excludeInvalidCount = 1;
            }
            if (UCGroupConditions1.ShowIncludeIndirectManHour && UCGroupConditions1.IncludeIndirectManHourChecked)
            {
                includeIndirectCount = 1;
            }

            for (int i = 0; i < gridWebGrid.Rows.Count - 1; i++)
            {
                if ((i + 1) % (3 + excludeInvalidCount + includeIndirectCount) == 0)
                {
                    CalcSummaryCell(i, gridWebGrid.Columns.Count - 1, excludeInvalidCount, includeIndirectCount);
                }
            }

            int pos = gridWebGrid.Columns.FromKey("Values").Index;
            for (int i = pos + 1; i < gridWebGrid.Columns.Count; i++)
            {
                CalcSummaryCell(gridWebGrid.Rows.Count - 1, i, excludeInvalidCount, includeIndirectCount);
            }
        }

        private void CalcSummaryCell(int rowIndex, int columnIndex, int excludeInvalidCount, int includeIndirectCount)
        {
            double a = 0;
            double b = 0;
            double c = 0;
            double d = 0;
            double result = 0;

            double.TryParse(gridWebGrid.Rows[rowIndex - 2 - excludeInvalidCount - includeIndirectCount].Items[columnIndex].Text, out a);
            double.TryParse(gridWebGrid.Rows[rowIndex - 1 - excludeInvalidCount - includeIndirectCount].Items[columnIndex].Text, out b);
            if (excludeInvalidCount > 0)
            {
                double.TryParse(gridWebGrid.Rows[rowIndex - 1 - includeIndirectCount].Items[columnIndex].Text, out c);
            }

            if (includeIndirectCount > 0)
            {
                double.TryParse(gridWebGrid.Rows[rowIndex - 1].Items[columnIndex].Text, out d);
            }

            result = b;
            if (result != 0)
            {
                result = (a - c + d) / result;
            }

            gridWebGrid.Rows[rowIndex].Items[columnIndex].Text = result.ToString("0.0000");
        }
    }
}
