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
    public partial class FNewReportErrorCauseGroupAnalysisQP : BaseQPageNew
    {
        #region 页面初始化

        //private System.ComponentModel.IContainer components;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

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
            this.UCWhereConditions1.SetControlPosition(0, 1, UCWhereConditions1.PanelItemCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 2, UCWhereConditions1.PanelMaterialModelCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(1, 0, UCWhereConditions1.PanelMOCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 1, UCWhereConditions1.PanelMaterialMachineTypeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 2, UCWhereConditions1.PanelMOBOMVersionWhere.ID);

            this.UCWhereConditions1.SetControlPosition(2, 0, UCWhereConditions1.PanelBigSSCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 1, UCWhereConditions1.PanelSegCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 2, UCWhereConditions1.PanelSSCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(3, 0, UCWhereConditions1.PanelOPCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(3, 1, UCWhereConditions1.PanelShiftCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(4, 0, UCWhereConditions1.PanelStartDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(4, 1, UCWhereConditions1.PanelEndDateWhere.ID);

            this.UCWhereConditions1.SetControlPosition(5, 0, UCWhereConditions1.PanelFirstClassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(5, 1, UCWhereConditions1.PanelSecondClassWhere.ID);
            this.UCWhereConditions1.SetControlPosition(5, 2, UCWhereConditions1.PanelThirdClassWhere.ID);

            this.UCWhereConditions1.SetControlPosition(6, 0, UCWhereConditions1.PanelMOMemoWhere.ID);
            this.UCWhereConditions1.SetControlPosition(6, 1, UCWhereConditions1.PanelNewMassWhere.ID);
        }

        private void InitGroupControls()
        {
            this.UCGroupConditions1.ShowByTimePanel = true;

            this.UCGroupConditions1.ShowBigSSCode = true;
            this.UCGroupConditions1.ShowSegCode = true;
            this.UCGroupConditions1.ShowSSCode = true;

            this.UCGroupConditions1.ShowGoodSemiGood = true;
            this.UCGroupConditions1.ShowItemCode = true;
            this.UCGroupConditions1.ShowMaterialModelCode = true;
            this.UCGroupConditions1.ShowMaterialMachineType = true;
            this.UCGroupConditions1.ShowMaterialExportImport = true;

            this.UCGroupConditions1.ShowMOCode = true;
            this.UCGroupConditions1.ShowMOMemo = true;
            this.UCGroupConditions1.ShowNewMass = true;
            this.UCGroupConditions1.ShowCrewCode = true;

            this.UCGroupConditions1.ShowFirstClass = true;
            this.UCGroupConditions1.ShowSecondClass = true;
            this.UCGroupConditions1.ShowThirdClass = true;

            this.UCGroupConditions1.ShowSp1 = true;
            this.UCGroupConditions1.ShowSp2 = true;
        }

        private void InitResultControls()
        {
            //this.gridWebGrid.Visible = false;
            this.cmdGridExport.Visible = false;
            this.pieChart.Visible = false;
            this.columnChart.Visible = false;
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
            PostBackTrigger triggerCmdView = new PostBackTrigger();
            triggerCmdView.ControlID = "chbRefreshAuto";
            (this.Form.FindControl("up1") as UpdatePanel).Triggers.Add(triggerCmdView);
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.UCWhereConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCGroupConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCDisplayConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCQueryDataType1.InitUserControl(this.languageComponent1, this.DataProvider);

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
            string inputOutput = UCWhereConditions1.UserSelectInputOutput;
            string byTimeType = UCGroupConditions1.UserSelectByTimeType;
            string compareType = UCGroupConditions1.UserSelectCompareType;
            string completeType = UCGroupConditions1.UserSelectCompleteType;

            if (!isForCompare)
            {
                compareType = string.Empty;
            }

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

            bool bigSSChecked = UCGroupConditions1.BigSSChecked;
            bool opResChecked = UCGroupConditions1.OPChecked || UCGroupConditions1.ResChecked;
            bool segSSChecked = UCGroupConditions1.SegChecked || UCGroupConditions1.SSChecked;


            //内部SQL
            string groupFieldsYInner = this.UCGroupConditions1.GetGroupFieldList(preferredTableInner, "Y");
            if (groupFieldsYInner.Trim().Length > 0)
            {
                groupFieldsYInner += ",";
            }
            groupFieldsYInner += "**.ecsgcode";

            ReportSQLEngine engineInner = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engineInner.DetailedCoreTable = GetCoreTableDetail();
            engineInner.Formular = GetFormular(inputOutput, compareType, completeType, bigSSChecked, opResChecked, segSSChecked);
            engineInner.WhereCondition = this.UCWhereConditions1.GetWhereSQLStatement(preferredTableInner, byTimeType, roundDate, dateAdjust);
            engineInner.GroupFieldsX = string.Empty; ;
            engineInner.GroupFieldsY = groupFieldsYInner;


            //外部SQL
            string groupFields = string.Empty;
            groupFields += this.UCGroupConditions1.GetGroupFieldAliasList(preferredTable, "Y");
            if (groupFields.Trim().Length > 0)
            {
                groupFields += ",";
            }
            groupFields += "**.ecsgcode";

            ReportSQLEngine engine = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engine.DetailedCoreTable = engineInner.GetReportSQL();
            engine.Formular = groupFields + ",**.errorcount, **.errorcount / SUM(**.errorcount) OVER() AS errorpercent";
            engine.WhereCondition = string.Empty;
            engine.GroupFieldsX = string.Empty;
            engine.GroupFieldsY = string.Empty;
            engine.OrderFields = groupFields;

            return engine.GetReportDataSource(byTimeType, dateAdjust); ;
        }

        private string GetCoreTableDetail()
        {
            string returnValue = string.Empty;

            returnValue += "SELECT tbltserrorcause.ecsgcode || ' - ' ||tblecsg.ecsgdesc AS ecsgcode, tblts.itemcode, tblts.mocode, tblts.shiftday, " + "\r\n";
            returnValue += "tblss.bigsscode, tblts.modelcode, tblts.frmopcode as opcode, tblts.frmsegcode as segcode," + "\r\n";
            returnValue += "tblts.frmsscode as sscode, tblts.frmrescode as rescode, " + "\r\n";
            returnValue += "tblts.shifttypecode, tblts.shiftcode, tblts.tpcode " + "\r\n";
            returnValue += "FROM tblts " + "\r\n";
            returnValue += "INNER JOIN tbltserrorcause " + "\r\n";
            returnValue += "ON tblts.tsid = tbltserrorcause.tsid " + "\r\n";
            returnValue += "LEFT OUTER JOIN tblss " + "\r\n";
            returnValue += "ON tblts.frmsscode = tblss.sscode " + "\r\n";
            returnValue += "LEFT OUTER JOIN tblecsg " + "\r\n";
            returnValue += "ON tbltserrorcause.ecsgcode = tblecsg.ecsgcode " + "\r\n";

            return returnValue;
        }

        private string GetFormular(string inputOutput, string compareType, string completeType, bool bigSSChecked, bool opResChecked, bool segSSChecked)
        {
            string returnValue = string.Empty;

            returnValue += " count(**.ecsgcode) as errorcount ";

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
                this.AutoRefresh = this.chbRefreshAuto.Checked;

                string compareType = this.UCGroupConditions1.UserSelectCompareType.Trim().ToLower();
                string byTimeType = this.UCGroupConditions1.UserSelectByTimeType.Trim().ToLower();
                string inputOutput = this.UCWhereConditions1.UserSelectInputOutput.Trim().ToLower();
                object[] dateSource = null;
                object[] dateSourceCompare = null;



                //一般数据
                dateSource = this.LoadDataSource(false, compareType.Trim().Length > 0);
                if (dateSource == null || dateSource.Length <= 0)
                {
                    this.gridWebGrid.Visible = false;
                    this.cmdGridExport.Visible = false;

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
                List<string> columnPropertyListForGrid = new List<string>();
                List<ReportGridDim3Property> dim3PropertyList = new List<ReportGridDim3Property>();

                columnPropertyListForGrid.Add("ErrorCauseGroupCode");

                if (UCQueryDataType1.GetQueryDataType() == NewReportQueryDataType.DataNumber)
                {
                    dim3PropertyList.Add(new ReportGridDim3Property("ErrorCount", "0", "SUM", "SUM", false));
                }
                else
                {
                    dim3PropertyList.Add(new ReportGridDim3Property("ErrorPercent", "0.00%", "SUM", "SUM", false));
                }

                ReportGridHelperNew reportGridHelper = new ReportGridHelperNew(this.DataProvider, this.languageComponent1, this.gridWebGrid,this.DtSource);

                reportGridHelper.DataSource = dateSource;
                reportGridHelper.DataSourceForCompare = dateSourceCompare;
                reportGridHelper.Dim1PropertyList = fixedColumnList;
                reportGridHelper.Dim2PropertyList = columnPropertyListForGrid;
                reportGridHelper.Dim3PropertyList = dim3PropertyList;
                reportGridHelper.HasDim3PropertyNameRowColumn = true;
                reportGridHelper.CompareType = compareType;
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
                    this.cmdGridExport.Visible = true;
                    this.pieChart.Visible = false;
                    this.columnChart.Visible = false;
                }
                else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.LineChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.PieChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                {
                    List<string> rowPropertyList = GetColumns();
                    rowPropertyList.Add("ErrorCauseGroupCode");
                    List<string> columnPropertyList = GetRows();
                    columnPropertyList.Add("EAttribute1");
                    List<string> dataPropertyList = new List<string>();                    
                    dataPropertyList.Add("ErrorPercent");

                    NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dateSource.Length + dateSourceCompare.Length];
                    dateSource.CopyTo(dateSourceForOWC, 0);
                    for (int i = 0; i < dateSourceCompare.Length; i++)
                    {
                        dateSourceForOWC[dateSource.Length + i] = (NewReportDomainObject)dateSourceCompare[i];
                    }

                    //针对于饼图，数据源只使用最下面的汇总行的数据
                    if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.PieChart)
                    {
                        int count = this.gridWebGrid.Columns.Count - fixedColumnList.Count - 2;
                        dateSourceForOWC = new NewReportDomainObject[count];

                        float totalErrorCount = GetFloatFromGrid(this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Items[this.gridWebGrid.Columns.Count - 1].Text);
                        for (int i = fixedColumnList.Count + 1; i < this.gridWebGrid.Columns.Count - 1; i++)
                        {
                            NewReportDomainObject domainObject = new NewReportDomainObject();

                            domainObject.ErrorCauseGroupCode = this.gridWebGrid.Columns[i].Header.Text;
                            float errorCount = GetFloatFromGrid(this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Items[i].Text);
                            domainObject.ErrorPercent = errorCount / totalErrorCount;

                            dateSourceForOWC[i - fixedColumnList.Count - 1] = domainObject;
                        }

                        columnPropertyList.Clear();
                        rowPropertyList.Clear();
                        rowPropertyList.Add("ErrorCauseGroupCode");
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
                                NewReportDomainObject domainObject = new NewReportDomainObject();

                                for (int k = 0; k < fixedColumnList.Count; k++)
                                {
                                    t.GetField(fixedColumnList[k]).SetValue(domainObject, this.gridWebGrid.Rows[i].Items[k].Text);
                                }
                                domainObject.ErrorCauseGroupCode = this.gridWebGrid.Columns[j].Header.Text;

                                float errorCount = GetFloatFromGrid(this.gridWebGrid.Rows[i].Items[j].Text);
                                domainObject.ErrorPercent = errorCount / totalErrorCount;

                                dateSourceForOWC[i * countColumn + (j - fixedColumnList.Count - 1)] = domainObject;
                            }
                        }

                    }

                    string propertyName = this.languageComponent1.GetString(dataPropertyList[0]);
                    foreach (NewReportDomainObject domainObject in dateSourceForOWC)
                    {
                        domainObject.EAttribute1 = propertyName + domainObject.EAttribute1;
                    }

                    if (dateSourceForOWC != null)
                    {
                        foreach (NewReportDomainObject obj in dateSourceForOWC)
                        {
                            obj.TempValue = obj.ErrorPercent.ToString();
                            obj.PeriodCode = obj.ErrorCauseGroupCode;

                        }

                        if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
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

                            this.columnChart.ChartTextFormatString = "<DATA_VALUE:#00.00%>";
                            this.columnChart.YLabelFormatString = "<DATA_VALUE:#00.00%>";
                            this.columnChart.DataType = true;
                            this.columnChart.DataSource = dateSourceForOWC;
                            this.columnChart.DataBind();
                        }
                        else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.PieChart)
                        {
                            this.pieChart.Visible = true;
                            this.columnChart.Visible = false;

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

                            this.pieChart.ChartTextFormatString = "<DATA_VALUE:#0.##%>";
                            this.pieChart.YLabelFormatString = "<DATA_VALUE:#0.##%>";
                            this.pieChart.DataType = true;
                            this.pieChart.DataSource = dateSourceForOWC;
                            this.pieChart.DataBind();
                        }
                    }
                    this.gridWebGrid.Visible = false;
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
