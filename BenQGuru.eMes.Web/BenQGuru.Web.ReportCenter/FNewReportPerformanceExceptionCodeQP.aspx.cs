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
    public partial class FNewReportPerformanceExceptionCodeQP : BaseQPageNew
    {
        #region 页面初始化

        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private const string preferredTableInner = "tblmaterial,tblmo,tblitemclass,tbltimedimension,tblres,tblline2crew,**,tblmesentitylist";
        private const string preferredTable = "tblmaterial,tblmo,tblitemclass,tbltimedimension,**,tblres,tblline2crew";

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);

            //初始化控件的位置和可见性
            InitWhereControls();
            InitGroupControls();
            InitResultControls();
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

        private void InitWhereControls()
        {
            this.UCWhereConditions1.SetControlPosition(0, 0, UCWhereConditions1.PanelGoodSemiGoodWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 1, UCWhereConditions1.PanelBigSSCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(1, 0, UCWhereConditions1.PanelStartDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 1, UCWhereConditions1.PanelEndDateWhere.ID);

            this.UCWhereConditions1.SetControlPosition(2, 0, UCWhereConditions1.PanelDutyCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 1, UCWhereConditions1.PanelExceptionCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 2, UCWhereConditions1.PanelExceptionFlagWhere.ID);
        }

        private void InitGroupControls()
        {
            this.UCGroupConditions1.ShowShiftDay = true;
            this.UCGroupConditions1.ShowWeek = true;
            this.UCGroupConditions1.ShowMonth = true;
            this.UCGroupConditions1.ShowByTimePanel = true;

            this.UCGroupConditions1.ShowGoodSemiGood = true;
            this.UCGroupConditions1.ShowBigSSCode = true;
            this.UCGroupConditions1.ShowExceptionOrDuty = true;

        }

        private void InitResultControls()
        {
            //this.gridWebGrid.Visible = false;
            //this.OWCChartSpace1.Display = false;
            this.columnChart.Visible = false;
            this.pieChart.Visible = false;
            this.cmdGridExport.Visible = false;
            //this.OWCPivotTable1.Display = false;
            
        }

        #endregion

        #region 公用属性和事件处理

        private void LoadDisplayControls()
        {
            if (!this.IsPostBack)
            {
                List<ListItem> displayList = new List<ListItem>();
                displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.Grid), NewReportDisplayType.Grid));
                displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.PieChart), NewReportDisplayType.PieChart));
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
            gridHelper = new GridHelperNew(gridWebGrid, DtSource);
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.UCWhereConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCGroupConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCDisplayConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCQueryDataType1.InitUserControl(this.languageComponent1, this.DataProvider);
                //this.OWCPivotTable1.LanguageComponent = this.languageComponent1;

                this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;
            }

            LoadDisplayControls();
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
            string byTimeType = UCGroupConditions1.UserSelectByTimeType;

            string exceptionOrDuty = UCGroupConditions1.UserExceptionOrDuty;
            string dataField = "ExceptionCode";
            if (exceptionOrDuty == NewReportExceptionOrDuty.Exception)
            {
                dataField = "ExceptionDesc";
            }
            else if (exceptionOrDuty == NewReportExceptionOrDuty.Duty)
            {
                dataField = "DutyDesc";
            }

            //内部SQL
            ReportSQLEngine engineInner = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engineInner.DetailedCoreTable = GetCoreTableDetail();
            engineInner.Formular = "SUM(lostmanhour) AS lostmanhour";
            engineInner.WhereCondition = this.UCWhereConditions1.GetWhereSQLStatement(preferredTableInner, byTimeType, true, 0);
            engineInner.GroupFieldsX = this.UCGroupConditions1.GetGroupFieldList(preferredTableInner, "X");
            engineInner.GroupFieldsY = this.UCGroupConditions1.GetGroupFieldList(preferredTableInner, "Y");
            if (engineInner.GroupFieldsY.Trim().Length > 0)
            {
                engineInner.GroupFieldsY += ",";
            }
            engineInner.GroupFieldsY += dataField;


            //外部SQL
            string groupFields = string.Empty;
            groupFields += this.UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "X");
            if (groupFields.IndexOf("dweek") == 0 || groupFields.IndexOf("dmonth") == 0)
            {
                groupFields += ",year";
            }
            groupFields += "," + dataField;
            string groupFieldsY = this.UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "Y");
            if (groupFieldsY.Trim().Length > 0)
            {
                groupFields += "," + groupFieldsY;
            }            

            ReportSQLEngine engine = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engine.DetailedCoreTable = engineInner.GetReportSQL();
            engine.Formular = groupFields + ",**.lostmanhour, **.lostmanhour / SUM(**.lostmanhour) OVER() AS lostmanhourpercent";
            engine.WhereCondition = string.Empty;
            engine.GroupFieldsX = string.Empty;
            engine.GroupFieldsY = string.Empty;
            engine.OrderFields = groupFields;

            return engine.GetReportDataSource(byTimeType, 0); ;
        }

        private string GetCoreTableDetail()
        {
            string returnValue = string.Empty;

            returnValue += "SELECT itemcode, bigsscode, tbllostmanhourdetail.dutycode,tblduty.dutydesc, tbllostmanhourdetail.exceptioncode, tblexceptioncode.exceptiondesc, exceptionflag, shiftdate AS shiftday, SUM(lostmanhour) / 3600.0 AS lostmanhour ";
            returnValue += "FROM tbllostmanhourdetail ";
            returnValue += "LEFT OUTER JOIN tblexceptioncode ";
            returnValue += "ON tblexceptioncode.exceptioncode = tbllostmanhourdetail.exceptioncode ";
            returnValue += "LEFT OUTER JOIN tblss ";
            returnValue += "ON tblss.sscode = tbllostmanhourdetail.sscode ";
            returnValue += "LEFT OUTER JOIN TBLDUTY ";
            returnValue += "ON TBLLOSTMANHOURDETAIL.DUTYCODE =TBLDUTY.DUTYCODE ";
            returnValue += "GROUP BY itemcode, bigsscode, tbllostmanhourdetail.dutycode,tblduty.dutydesc, tbllostmanhourdetail.exceptioncode, tblexceptioncode.exceptiondesc, exceptionflag, shiftdate ";

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

            schemaList.Add("Input");
            schemaList.Add("Output");
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
                string exceptionOrDuty = UCGroupConditions1.UserExceptionOrDuty;
                string dataField = "ExceptionCode";
                if (exceptionOrDuty == NewReportExceptionOrDuty.Exception)
                {
                    dataField = "ExceptionDesc";
                }
                else if (exceptionOrDuty == NewReportExceptionOrDuty.Duty)
                {
                    dataField = "DutyDesc";
                }

                object[] dateSource = null;

                //一般数据
                dateSource = this.LoadDataSource();
                if (dateSource == null || dateSource.Length <= 0)
                {
                    this.gridWebGrid.Visible = false;
                    //this.OWCChartSpace1.Display = false;
                    this.columnChart.Visible = false;
                    this.pieChart.Visible = false;
                    this.cmdGridExport.Visible = false;

                    ReportPageHelper.SetPageScrollToBottom(this);
                    return;
                }

                //数据加载到Grid
                List<string> fixedColumnList = GetRows();
                fixedColumnList.AddRange(GetColumns());

                List<string> columnPropertyListForGrid = new List<string>();
                List<ReportGridDim3Property> dim3PropertyList = new List<ReportGridDim3Property>();

                columnPropertyListForGrid.Add(dataField);

                if (UCQueryDataType1.GetQueryDataType() == NewReportQueryDataType.DataNumber)
                {
                    dim3PropertyList.Add(new ReportGridDim3Property("LostManHour", "0.0000", "SUM", "SUM", false));
                }
                else
                {
                    dim3PropertyList.Add(new ReportGridDim3Property("LostManHourPercent", "0.00%", "SUM", "SUM", false));
                }

                ReportGridHelperNew reportGridHelper = new ReportGridHelperNew(this.DataProvider, this.languageComponent1, this.gridWebGrid,DtSource);

                reportGridHelper.DataSource = dateSource;
                reportGridHelper.Dim1PropertyList = fixedColumnList;
                reportGridHelper.Dim2PropertyList = columnPropertyListForGrid;
                reportGridHelper.Dim3PropertyList = dim3PropertyList;
                reportGridHelper.HasDim3PropertyNameRowColumn = true;
                reportGridHelper.ByTimeType = byTimeType;

                reportGridHelper.ShowGrid();
                reportGridHelper.AddPercentByRow(false, "0.00%");
                base.InitWebGrid();

                this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
                this.gridWebGrid.Behaviors.Sorting.Enabled = false;

                //修正最后的汇总列里面数据（接近100修改为100）
                if (UCQueryDataType1.GetQueryDataType() == NewReportQueryDataType.DataRate)
                {
                    if (this.gridWebGrid.Rows.Count > 0 && this.gridWebGrid.Columns.Count > 0)
                    {
                        string sumCellText = this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Items[this.gridWebGrid.Columns.Count - 1].Text.Trim();
                        if (sumCellText.Length > 0 && sumCellText.IndexOf("%") == sumCellText.Length - 1)
                        {
                            this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Items[this.gridWebGrid.Columns.Count - 1].Text = "100.00%";
                        }
                    }

                    if (this.gridWebGrid.Rows.Count == 2 && this.gridWebGrid.Columns.Count > 0)
                    {
                        string sumCellText = this.gridWebGrid.Rows[0].Items[this.gridWebGrid.Columns.Count - 1].Text.Trim();
                        if (sumCellText.Length > 0 && sumCellText.IndexOf("%") == sumCellText.Length - 1)
                        {
                            this.gridWebGrid.Rows[0].Items[this.gridWebGrid.Columns.Count - 1].Text = "100.00%";
                        }
                    }

                }

                //获取表格和图示
                if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.Grid)
                {
                    this.gridWebGrid.Visible = true;
                    //this.OWCChartSpace1.Display = false;
                    this.pieChart.Visible = false;
                    this.columnChart.Visible = false;
                    this.cmdGridExport.Visible = true;
                }
                else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.LineChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.PieChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                {
                    List<string> rowPropertyList = new List<string>();
                    rowPropertyList.Add(dataField);

                    List<string> columnPropertyList = GetRows();
                    columnPropertyList.AddRange(GetColumns());

                    List<string> dataPropertyList = new List<string>();
                    dataPropertyList.Add("LostManHourPercent");

                    NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dateSource.Length];
                    dateSource.CopyTo(dateSourceForOWC, 0);


 
                    //针对于饼图，数据源只使用最下面的汇总行的数据
                    if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.PieChart)
                    {
                        int count = this.gridWebGrid.Columns.Count - fixedColumnList.Count - 2;
                        dateSourceForOWC = new NewReportDomainObject[count];

                        float totalErrorCount = GetFloatFromGrid(this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Items[this.gridWebGrid.Columns.Count - 1].Text);
                        for (int i = fixedColumnList.Count + 1; i < this.gridWebGrid.Columns.Count - 1; i++)
                        {
                            NewReportDomainObject domainObject = new NewReportDomainObject();

                            if (exceptionOrDuty == NewReportExceptionOrDuty.Exception)
                            {
                                domainObject.ExceptionDesc = this.gridWebGrid.Columns[i].Header.Text;
                            }
                            else if (exceptionOrDuty == NewReportExceptionOrDuty.Duty)
                            {
                                domainObject.DutyDesc = this.gridWebGrid.Columns[i].Header.Text;
                            }

                            if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Period)
                            {
                                domainObject.PeriodCode = this.gridWebGrid.Columns[i].Header.Text; ;
                            }

                            if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Shift)
                            {
                                domainObject.ShiftCode = this.gridWebGrid.Columns[i].Header.Text; ;
                            }

                            if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.ShiftDay)
                            {
                                domainObject.ShiftDay = this.gridWebGrid.Columns[i].Header.Text; ;
                            }

                            if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Week)
                            {
                                domainObject.Week = this.gridWebGrid.Columns[i].Header.Text; ;
                            }

                            if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Month)
                            {
                                domainObject.Month = this.gridWebGrid.Columns[i].Header.Text; ;
                            }

                            if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Year)
                            {
                                domainObject.Year = this.gridWebGrid.Columns[i].Header.Text; ;
                            }

                            float errorCount = GetFloatFromGrid(this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Items[i].Text);
                            domainObject.LostManHourPercent = errorCount / totalErrorCount;
                            
                            dateSourceForOWC[i - fixedColumnList.Count - 1] = domainObject;
                        }

                        columnPropertyList.Clear();
                        rowPropertyList.Clear();
                        rowPropertyList.Add(dataField);
                    }
                    else
                    {
                        int countRow = this.gridWebGrid.Rows.Count - 1;
                        int countColumn = this.gridWebGrid.Columns.Count - fixedColumnList.Count - 2;
                        dateSourceForOWC = new NewReportDomainObject[countRow * countColumn];

                        Type t = typeof(NewReportDomainObject);

                        for (int i = 0; i < this.gridWebGrid.Rows.Count - 1; i++)
                        {
                            float totalErrorCount = GetFloatFromGrid(this.gridWebGrid.Rows[i].Items[this.gridWebGrid.Columns.Count - 1].Text);

                            for (int j = fixedColumnList.Count + 1; j < this.gridWebGrid.Columns.Count - 1; j++)
                            {
                                //NewReportDomainObject domainObject = (NewReportDomainObject)dateSourceForOWC[i * countColumn + (j - fixedColumnList.Count - 1)];//new NewReportDomainObject();

                                NewReportDomainObject domainObject = new NewReportDomainObject();

                                for (int k = 0; k < fixedColumnList.Count; k++)
                                {
                                    t.GetField(fixedColumnList[k]).SetValue(domainObject, this.gridWebGrid.Rows[i].Items[k].Text);
                                }

                                if (exceptionOrDuty == NewReportExceptionOrDuty.Exception)
                                {
                                    domainObject.ExceptionDesc = this.gridWebGrid.Columns[j].Header.Text;
                                }
                                else if (exceptionOrDuty == NewReportExceptionOrDuty.Duty)
                                {
                                    domainObject.DutyDesc = this.gridWebGrid.Columns[j].Header.Text;
                                }


                                if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Period)
                                {
                                    domainObject.PeriodCode = this.gridWebGrid.Columns[j].Header.Text; ;
                                }

                                if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Shift)
                                {
                                    domainObject.ShiftCode = this.gridWebGrid.Columns[j].Header.Text; ;
                                }

                                if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.ShiftDay)
                                {
                                    domainObject.ShiftDay = this.gridWebGrid.Columns[j].Header.Text; ;
                                }

                                if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Week)
                                {
                                    domainObject.Week = this.gridWebGrid.Columns[j].Header.Text; ;
                                }

                                if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Month)
                                {
                                    domainObject.Month = this.gridWebGrid.Columns[j].Header.Text; ;
                                }

                                if (UCGroupConditions1.UserSelectByTimeType == NewReportByTimeType.Year)
                                {
                                    domainObject.Year = this.gridWebGrid.Columns[j].Header.Text; ;
                                }


                                float errorCount = GetFloatFromGrid(this.gridWebGrid.Rows[i].Items[j].Text);
                                domainObject.LostManHourPercent = errorCount / totalErrorCount;

                                //dateSourceForOWC[i - fixedColumnList.Count - 1] = domainObject;
                                dateSourceForOWC[i * countColumn + (j - fixedColumnList.Count - 1)] = domainObject;

                                
                            }
                        }

                    }

                    string propertyName = this.languageComponent1.GetString(dataPropertyList[0]);
                    foreach (NewReportDomainObject domainObject in dateSourceForOWC)
                    {
                        domainObject.EAttribute1 = propertyName + domainObject.EAttribute1;
                    }

                    foreach (NewReportDomainObject obj in dateSourceForOWC)
                    {
                        obj.TempValue = obj.LostManHourPercent.ToString();

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



                    if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.PieChart)
                    {
                        this.columnChart.Visible = false;
                        this.pieChart.Visible = true;

                        pieChart.ChartGroupByString = UCGroupConditions1.GetCheckedColumnsString();

                        //设置首页报表的大小
                        if (ViewState["Width"] != null)
                        {
                            pieChart.Width = int.Parse(ViewState["Width"].ToString());
                        }

                        if (ViewState["Height"] != null)
                        {
                            pieChart.Height = int.Parse(ViewState["Height"].ToString());
                        }
                        //end

                        this.pieChart.ChartTextFormatString = "<DATA_VALUE:0.00%>";
                        this.pieChart.YLabelFormatString = "<DATA_VALUE:0.##%>";
                        this.pieChart.DataType = true;
                        this.pieChart.DataSource = dateSourceForOWC;
                        this.pieChart.DataBind();
                    }
                    else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                    {
                        this.columnChart.Visible = true;
                        this.pieChart.Visible = false;

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

                        this.columnChart.ChartTextFormatString = "<DATA_VALUE:#0.##%>";
                        this.columnChart.YLabelFormatString = "<DATA_VALUE:#0.##%>";
                        this.columnChart.DataType = true;
                        this.columnChart.DataSource = dateSourceForOWC;
                        this.columnChart.DataBind();
                    }
                    else
                    {
                        this.columnChart.Visible = false;
                        this.pieChart.Visible = false;
                    }

                    this.gridWebGrid.Visible = false;
                    //this.OWCChartSpace1.Display = true;
                    
                    this.cmdGridExport.Visible = false;
                }

                ReportPageHelper.SetPageScrollToBottom(this);
            }
            else
            {
                this.chbRefreshAuto.Checked = false;
                this.AutoRefresh = false;
            }
        }

        private float GetFloatFromGrid(string text)
        {
            float returnValue = 0;

            if (text.IndexOf("-") >= 0)
            {
                text = text.Substring(0, text.IndexOf("-"));
            }

            float.TryParse(text.Trim(), out returnValue);

            return returnValue;
        }
    }
}
