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
    public partial class FNewReportIQCUnQualified : BaseQPageNew
    {
        #region 页面初始化

        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private const string preferredTable = "tblmaterial,tblitemclass,tblvendor";

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
            this.UCWhereConditions1.SetControlPosition(0, 0, UCWhereConditions1.PanelMaterialCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 1, UCWhereConditions1.PanelIQCItemTypeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 2, UCWhereConditions1.PanelIQCLineItemTypeWhere.ID);


            this.UCWhereConditions1.SetControlPosition(1, 0, UCWhereConditions1.PanelStartDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 1, UCWhereConditions1.PanelEndDateWhere.ID);

            this.UCWhereConditions1.SetControlPosition(2, 0, UCWhereConditions1.PanelFirstClassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 1, UCWhereConditions1.PanelSecondClassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 2, UCWhereConditions1.PanelThirdClassWhere.ID);

            this.UCWhereConditions1.SetControlPosition(3, 0, UCWhereConditions1.PanelVendorCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(3, 1, UCWhereConditions1.PanelConcessionWhere.ID);
            this.UCWhereConditions1.SetControlPosition(3, 2, UCWhereConditions1.PanelRoHSWhere.ID);
        }

        private void InitGroupControls()
        {
            this.UCGroupConditions1.ShowShiftDay = true;
            this.UCGroupConditions1.ShowWeek = true;
            this.UCGroupConditions1.ShowMonth = true;
            this.UCGroupConditions1.ShowYear = true;

            this.UCGroupConditions1.ShowByTimePanel = true;
            this.UCGroupConditions1.ShowCompareTypePanel = true;

            this.UCGroupConditions1.ShowFirstClass = true;
            this.UCGroupConditions1.ShowSecondClass = true;
            this.UCGroupConditions1.ShowThirdClass = true;

            this.UCGroupConditions1.ShowIQCItemType = true;
            this.UCGroupConditions1.ShowIQCLineItemType = true;
            this.UCGroupConditions1.ShowRoHS = true;
            this.UCGroupConditions1.ShowVendorCode = true;

            this.UCGroupConditions1.ShowSp3 = true;
        }

        private void InitResultControls()
        {
            //this.gridWebGrid.Visible = false;
            //this.OWCChartSpace1.Display = false;
            this.cmdGridExport.Visible = false;
            //this.OWCPivotTable1.Display = false;
            this.columnChart.Visible = false;
            this.lineChart.Visible = false;
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
                displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.HistogramChart), NewReportDisplayType.HistogramChart));
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
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.UCWhereConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCGroupConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCDisplayConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                //this.OWCPivotTable1.LanguageComponent = this.languageComponent1;
            }

            //加载控件的值
            LoadDisplayControls();
            this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
            this.gridWebGrid.Behaviors.Sorting.Enabled = false;
            this.gridWebGrid.ClientEvents.Initialize = "Grid_InitializeMergeCells";

        }

        protected override void OnPreRender(EventArgs e)
        {
            ReportPageHelper.SetControlValue(this, this.Request.Params);
            ReportPageHelper.DoQueryForBSHome(this, this.Request.Params, this.DoQuery);

            base.OnPreRender(e);
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

        private object[] LoadDataSource(bool isForCompare, bool roundDate)
        {
            string byTimeType = UCGroupConditions1.UserSelectByTimeType;
            string compareType = UCGroupConditions1.UserSelectCompareType;

            if (!isForCompare)
            {
                compareType = string.Empty;
            }

            string groupFieldsX = this.UCGroupConditions1.GetGroupFieldList(preferredTable, "X");
            string groupFieldsY = this.UCGroupConditions1.GetGroupFieldList(preferredTable, "Y");

            //用于环比同期比时的修改时间过滤条件
            int dateAdjust = 0;
            if (string.Compare(compareType, NewReportCompareType.LastYear, true) == 0)
            {
                dateAdjust = -12;
            }
            else if (string.Compare(compareType, NewReportCompareType.Previous, true) == 0)
            {
                dateAdjust = -1;
            }

            ReportSQLEngine engine = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engine.DetailedCoreTable = GetDetailedCoreTable();
            engine.Formular = GetFormular(compareType);
            engine.WhereCondition = this.UCWhereConditions1.GetWhereSQLStatement(preferredTable, byTimeType, roundDate, dateAdjust);
            engine.GroupFieldsX = groupFieldsX;
            engine.GroupFieldsY = groupFieldsY;

            return engine.GetReportDataSource(byTimeType, dateAdjust); ;
        }

        private string GetDetailedCoreTable()
        {
            string returnValue = string.Empty;

            returnValue += " SELECT tbliqcdetail.iqcno, tbliqcdetail.stline, tbliqcdetail.itemcode, tbliqcdetail.mdate AS shiftday, ";
            returnValue += " tbliqcdetail.attribute AS iqclineitemtype, tblasniqc.attribute AS iqcitemtype, tbliqcdetail.concessionstatus,";
            returnValue += " tblasn.vendorcode, ";
            returnValue += " SUM(1) AS allqty, ";
            returnValue += " SUM(DECODE(tblasniqc.sts, 'Y', 1, 0)) AS stslotqty, ";
            returnValue += " SUM(DECODE(tbliqcdetail.checkstatus, '"+IQCCheckStatus.IQCCheckStatus_Qualified+"', 1, 0)) AS qualifiedlotqty, ";
            returnValue += " SUM(DECODE(tbliqcdetail.checkstatus, '" + IQCCheckStatus .IQCCheckStatus_UnQualified+ "', 1, 0)) AS unqualifiedqty ";
            returnValue += " FROM tblasn, tblasniqc, tbliqcdetail";
            returnValue += " WHERE tblasn.stno = tblasniqc.stno";
            returnValue += " AND tblasniqc.iqcno = tbliqcdetail.iqcno";
            returnValue += " GROUP BY tbliqcdetail.iqcno, tbliqcdetail.stline, tbliqcdetail.itemcode, tbliqcdetail.mdate, ";
            returnValue += " tbliqcdetail.attribute, tblasniqc.attribute, tbliqcdetail.concessionstatus, tblasn.vendorcode";

            return returnValue;
        }

        private string GetFormular(string compareType)
        {
            string returnValue = string.Empty;

            returnValue = "NVL(SUM(ALLQTY),0) AS ALLQTY,NVL(SUM(STSLOTQTY),0) AS STSLOTQTY ,NVL(SUM(QUALIFIEDLOTQTY),0) AS QUALIFIEDLOTQTY,NVL(SUM(UNQUALIFIEDQTY),0) AS UNQUALIFIEDQTY , NVL(SUM(UNQUALIFIEDQTY),0)/nvl(SUM(ALLQTY),0)*1000000 AS UNQUALIFIEDRATEPPM";

            //EATTRIBUTE1用于标记投入/产出+比较类型
            if (compareType.Trim().Length > 0)
            {
                if (returnValue.Trim().Length > 0)
                {
                    returnValue += ",";
                }
                returnValue += "'" + compareType.Trim() + "'" + " AS EATTRIBUTE1 ";
            }
            return returnValue;
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

            schemaList.Add("UNQUALIFIEDRATEPPM");
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
            //同期比只能用于月
            if (UCGroupConditions1.UserSelectCompareType == NewReportCompareType.LastYear
                && UCGroupConditions1.UserSelectByTimeType != NewReportByTimeType.Month)
            {
                WebInfoPublish.Publish(this, "$Report_LastYearOnlyForMonth", this.languageComponent1);
                return false;
            }

            //环比只能用于周、月、年
            if (UCGroupConditions1.UserSelectCompareType == NewReportCompareType.Previous
                && UCGroupConditions1.UserSelectByTimeType != NewReportByTimeType.Week
                && UCGroupConditions1.UserSelectByTimeType != NewReportByTimeType.Month
                && UCGroupConditions1.UserSelectByTimeType != NewReportByTimeType.Year)
            {
                WebInfoPublish.Publish(this, "$Report_PreviousOnlyForWeekMonthYear", this.languageComponent1);
                return false;
            }

            return true;
        }

        protected override void DoQuery()
        {
            base.DoQuery();

            if (this.CheckBeforeQuery())
            {
                string compareType = this.UCGroupConditions1.UserSelectCompareType.Trim().ToLower();
                string byTimeType = this.UCGroupConditions1.UserSelectByTimeType.Trim().ToLower();
                object[] dateSource = null;
                object[] dateSourceCompare = null;

                //一般数据
                dateSource = this.LoadDataSource(false, compareType.Trim().Length > 0);
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

                //环比/同期比数据
                if (compareType.Trim().Length > 0)
                {
                    dateSourceCompare = this.LoadDataSource(true, true);
                }
                if (dateSourceCompare == null)
                {
                    dateSourceCompare = new NewReportDomainObject[0] { };
                }

                //数据加载到Grid
                List<string> fixedColumnList = GetRows();
                List<ReportGridDim3Property> dim3PropertyList = new List<ReportGridDim3Property>();

                dim3PropertyList.Add(new ReportGridDim3Property("QualifiedLotQTY", "0", "SUM", "SUM", false));
                dim3PropertyList.Add(new ReportGridDim3Property("UNQualifiedQty", "0", "SUM", "SUM", false));
                dim3PropertyList.Add(new ReportGridDim3Property("STSLotQTY", "0", "SUM", "SUM", false));
                dim3PropertyList.Add(new ReportGridDim3Property("AllQty", "0", "SUM", "SUM", false));
                dim3PropertyList.Add(new ReportGridDim3Property("UnqualifiedRatePPM", "0.00", "IQCPPM({-},{-2})", "IQCPPM({-1},{-2})", false));

                ReportGridHelperNew reportGridHelper = new ReportGridHelperNew(this.DataProvider, this.languageComponent1, this.gridWebGrid,this.DtSource);

                reportGridHelper.DataSource = dateSource;
                reportGridHelper.DataSourceForCompare = dateSourceCompare;
                reportGridHelper.Dim1PropertyList = fixedColumnList;
                reportGridHelper.Dim3PropertyList = dim3PropertyList;
                reportGridHelper.HasDim3PropertyNameRowColumn = true;
                reportGridHelper.CompareType = compareType;
                reportGridHelper.ByTimeType = byTimeType;

                reportGridHelper.ShowGrid();
                base.InitWebGrid();

                this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
                this.gridWebGrid.Behaviors.Sorting.Enabled = false;


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

                    NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dateSource.Length + dateSourceCompare.Length];
                    dateSource.CopyTo(dateSourceForOWC, 0);
                    for (int i = 0; i < dateSourceCompare.Length; i++)
                    {
                        dateSourceForOWC[dateSource.Length + i] = (NewReportDomainObject)dateSourceCompare[i];
                    }
                    string propertyName = this.languageComponent1.GetString(dim3PropertyList[4].Name);
                    foreach (NewReportDomainObject domainObject in dateSourceForOWC)
                    {
                        domainObject.EAttribute1 = propertyName + domainObject.EAttribute1;
                    }

                    List<string> rowPropertyList = GetColumns();
                    List<string> columnPropertyList = GetRows();
                    columnPropertyList.Add("EAttribute1");
                    List<string> valuePropertyList = new List<string>();

                    valuePropertyList.Add("UnqualifiedRatePPM");
                    foreach (ReportGridDim3Property property in dim3PropertyList)
                    {
                        if (!property.Hidden && !valuePropertyList.Contains(property.Name))
                        {
                            valuePropertyList.Add(property.Name);
                        }
                    }
                    List<string> dataPropertyList = valuePropertyList;

                    //ReportChartHelper reportChartHelper = new ReportChartHelper(this.OWCPivotTable1, this.OWCChartSpace1, this.languageComponent1);
                    //reportChartHelper.DataSource = dateSourceForOWC;
                    //reportChartHelper.RowPropertyList = rowPropertyList.ToArray();
                    //reportChartHelper.ColumnPropertyList = columnPropertyList.ToArray();
                    //reportChartHelper.DataPropertyList = dataPropertyList.ToArray();
                    //reportChartHelper.UserOWCChartType = UCDisplayConditions1.GetDisplayType().Trim().ToLower();

                    //reportChartHelper.LoadOWCChart();

                    foreach (NewReportDomainObject obj in dateSourceForOWC)
                    {
                        obj.TempValue = obj.UnqualifiedRatePPM.ToString();
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

                        this.lineChart.ChartTextFormatString = "<DATA_VALUE:0.##>";
                        this.lineChart.YLabelFormatString = "<DATA_VALUE:0.##>";
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

                        this.columnChart.ChartTextFormatString = "<DATA_VALUE:0.##>";
                        this.columnChart.YLabelFormatString = "<DATA_VALUE:0.##>";
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
                    //this.OWCChartSpace1.Display = true;
                    this.cmdGridExport.Visible = false;
                }

                ReportPageHelper.SetPageScrollToBottom(this);
            }
            else
            {

            }
        }
    }
}
