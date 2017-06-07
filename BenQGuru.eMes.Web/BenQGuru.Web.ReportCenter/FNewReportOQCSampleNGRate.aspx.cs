using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    public partial class FNewReportOQCSampleNGRate : BaseQPageNew
    {
        #region 页面初始化

        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private const string preferredTable = "tblmaterial,tblitemclass,tbltimedimension,**,tblmesentitylist,tblmo,tblres,tblline2crew,";

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
            this.UCWhereConditions1.SetControlPosition(0, 1, UCWhereConditions1.PanelItemCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 2, UCWhereConditions1.PanelMaterialModelCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(1, 0, UCWhereConditions1.PanelMaterialMachineTypeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 1, UCWhereConditions1.PanelLotNoWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 2, UCWhereConditions1.PanelBigSSCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(2, 0, UCWhereConditions1.PanelSSCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 1, UCWhereConditions1.PanelMaterialExportImportWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 2, UCWhereConditions1.PanelCrewCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(3, 0, UCWhereConditions1.PanelStartDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(3, 1, UCWhereConditions1.PanelEndDateWhere.ID);

            this.UCWhereConditions1.SetControlPosition(4, 0, UCWhereConditions1.PanelFirstClassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(4, 1, UCWhereConditions1.PanelSecondClassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(4, 2, UCWhereConditions1.PanelThirdClassWhere.ID);

            this.UCWhereConditions1.SetControlPosition(5, 0, UCWhereConditions1.PanelProductionTypeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(5, 1, UCWhereConditions1.PanelOQCLotTypeWhere.ID);
        }

        private void InitGroupControls()
        {
            this.UCGroupConditions1.ShowShiftDay = true;
            this.UCGroupConditions1.ShowWeek = true;
            this.UCGroupConditions1.ShowMonth = true;
            this.UCGroupConditions1.ShowYear = true;

            this.UCGroupConditions1.ShowByTimePanel = true;
            this.UCGroupConditions1.ShowCompareTypePanel = true;

            this.UCGroupConditions1.ShowBigSSCode = true;
            this.UCGroupConditions1.ShowSSCode = true;

            this.UCGroupConditions1.ShowGoodSemiGood = true;
            this.UCGroupConditions1.ShowItemCode = true;
            this.UCGroupConditions1.ShowMaterialModelCode = true;
            this.UCGroupConditions1.ShowMaterialMachineType = true;
            this.UCGroupConditions1.ShowMaterialExportImport = true;
            this.UCGroupConditions1.ShowLotNo = true;
            this.UCGroupConditions1.ShowProductionType = true;
            this.UCGroupConditions1.ShowOQCLotType = true;
            this.UCGroupConditions1.ShowCrewCode = true;

            this.UCGroupConditions1.ShowFirstClass = true;
            this.UCGroupConditions1.ShowSecondClass = true;
            this.UCGroupConditions1.ShowThirdClass = true;

            this.UCGroupConditions1.ShowSp2 = true;
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
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.UCWhereConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCGroupConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCDisplayConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
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
            engine.DetailedCoreTable = GetCoreTableDetail();
            engine.Formular = GetFormular(compareType);
            engine.WhereCondition = this.UCWhereConditions1.GetWhereSQLStatement(preferredTable, byTimeType, roundDate, dateAdjust);
            engine.GroupFieldsX = groupFieldsX;
            engine.GroupFieldsY = groupFieldsY;

            return engine.GetReportDataSource(byTimeType, dateAdjust); ;
        }

        private string GetCoreTableDetail()
        {
            string returnValue = string.Empty;

            returnValue += "SELECT tbllot.lotno, tbllot.itemcode, tbllot.shiftday, " + "\r\n";
            returnValue += "tblss.bigsscode, b.sscode, b.rescode, tblline2crew.crewcode, " + "\r\n";
            returnValue += "b.ssize, c.ngcount " + "\r\n";
            returnValue += "FROM tbllot " + "\r\n";
            returnValue += "INNER JOIN " + "\r\n";
            returnValue += "(SELECT lotno, lotseq, sscode, rescode, COUNT(rcard) ssize " + "\r\n";
            returnValue += "FROM tbllot2cardcheck " + "\r\n";
            returnValue += "GROUP BY lotno, lotseq, sscode, rescode) b " + "\r\n";
            returnValue += "ON tbllot.lotno = b.lotno " + "\r\n";
            returnValue += "AND tbllot.lotseq = b.lotseq " + "\r\n";
            returnValue += "LEFT OUTER JOIN " + "\r\n";
            returnValue += "(SELECT lotno, lotseq, sscode, rescode, COUNT(rcard) ngcount " + "\r\n";
            returnValue += "FROM tbllot2cardcheck " + "\r\n";
            returnValue += "WHERE status = 'NG' " + "\r\n";
            returnValue += "GROUP BY lotno, lotseq, sscode, rescode) c " + "\r\n";
            returnValue += "ON tbllot.lotno = c.lotno " + "\r\n";
            returnValue += "AND tbllot.lotseq = c.lotseq " + "\r\n";
            returnValue += "AND b.sscode = c.sscode " + "\r\n";
            returnValue += "AND b.rescode = c.rescode " + "\r\n";
            returnValue += "LEFT OUTER JOIN tblss " + "\r\n";
            returnValue += "ON b.sscode = tblss.sscode " + "\r\n";
            returnValue += "LEFT OUTER JOIN tblline2crew " + "\r\n";
            returnValue += "ON tbllot.shiftday = tblline2crew.shiftdate " + "\r\n";
            returnValue += "AND tbllot.shiftcode = tblline2crew.shiftcode " + "\r\n";
            returnValue += "AND b.sscode = tblline2crew.sscode " + "\r\n";            

            return returnValue;
        }

        private string GetFormular(string compareType)
        {
            string returnValue = string.Empty;

            returnValue = "SUM(NVL(**.ngcount,0)) AS oqcngcount, SUM(NVL(**.ssize,0)) AS oqcssize, SUM(NVL(**.ngcount,0))/SUM(NVL(**.ssize,0))*1000000 AS oqcsamplengrateppm";

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

            schemaList.Add("OQCSampleNGRatePPM");
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

                dim3PropertyList.Add(new ReportGridDim3Property("OQCSampleSize", "0", "SUM", "SUM", false));
                dim3PropertyList.Add(new ReportGridDim3Property("OQCNGCount", "0", "SUM", "SUM", false));
                dim3PropertyList.Add(new ReportGridDim3Property("OQCSampleNGRatePPM", "0.00", "PPM({-1},{-2})", "PPM({-1},{-2})", false));

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
                    List<string> rowPropertyList = GetColumns();
                    List<string> columnPropertyList = GetRows();
                    columnPropertyList.Add("EAttribute1");
                    List<string> dataPropertyList = new List<string>();
                    dataPropertyList.Add("OQCSampleNGRatePPM");

                    NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dateSource.Length + dateSourceCompare.Length];
                    dateSource.CopyTo(dateSourceForOWC, 0);
                    for (int i = 0; i < dateSourceCompare.Length; i++)
                    {
                        dateSourceForOWC[dateSource.Length + i] = (NewReportDomainObject)dateSourceCompare[i];
                    }
                    string propertyName = this.languageComponent1.GetString(dataPropertyList[0]);
                    foreach (NewReportDomainObject domainObject in dateSourceForOWC)
                    {
                        domainObject.EAttribute1 = propertyName + domainObject.EAttribute1;
                    }

                    //ReportChartHelper reportChartHelper = new ReportChartHelper(this.OWCPivotTable1, this.OWCChartSpace1, this.languageComponent1);
                    //reportChartHelper.DataSource = dateSourceForOWC;
                    //reportChartHelper.RowPropertyList = rowPropertyList.ToArray();
                    //reportChartHelper.ColumnPropertyList = columnPropertyList.ToArray();
                    //reportChartHelper.DataPropertyList = dataPropertyList.ToArray();
                    //reportChartHelper.UserOWCChartType = UCDisplayConditions1.GetDisplayType().Trim().ToLower();
                    //reportChartHelper.IsPercent = false;

                    //reportChartHelper.LoadOWCChart();

                    foreach (NewReportDomainObject obj in dateSourceForOWC)
                    {
                        obj.TempValue = obj.OQCSampleNGRatePPM.ToString();
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

                        this.columnChart.ChartTextFormatString = "<DATA_VALUE:#0.##>";
                        this.columnChart.YLabelFormatString = "<DATA_VALUE:#0.##>";
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
                this.chbRefreshAuto.Checked = false;
                this.AutoRefresh = false;
            }
        }
    }
}
