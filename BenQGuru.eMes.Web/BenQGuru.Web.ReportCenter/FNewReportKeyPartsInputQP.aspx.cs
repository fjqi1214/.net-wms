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
    public partial class FNewReportKeyPartsInputQP : BaseQPageNew
    {
        #region 页面初始化

        //private System.ComponentModel.IContainer components;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private const string preferredTable = "tbltimedimension,tblmaterial,tblmo,tblitemclass,**";

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
            this.UCWhereConditions1.SetControlPosition(0, 1, UCWhereConditions1.PanelMaterialMachineTypeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 2, UCWhereConditions1.PanelMaterialModelCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(1, 0, UCWhereConditions1.PanelVendorCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 1, UCWhereConditions1.PanelMOCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 2, UCWhereConditions1.PanelBigSSCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(2, 0, UCWhereConditions1.PanelStartDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 1, UCWhereConditions1.PanelEndDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(2, 2, UCWhereConditions1.PanelIncludeDroppedMaterialWhere.ID);

        }

        private void InitGroupControls()
        {
            this.UCGroupConditions1.ShowByTimePanel = true;
            this.UCGroupConditions1.ShowShiftDay = true;
            this.UCGroupConditions1.ShowWeek = true;
            this.UCGroupConditions1.ShowMonth = true;
            this.UCGroupConditions1.ShowYear = true;

            this.UCGroupConditions1.ShowVendorCode = true;
            this.UCGroupConditions1.ShowMaterialModelCode = true;
            this.UCGroupConditions1.ShowMaterialMachineType = true;
            this.UCGroupConditions1.ShowMOCode = true;
            this.UCGroupConditions1.ShowBigSSCode = true;
            this.UCGroupConditions1.ShowMaterialCode = true;
        }

        private void InitResultControls()
        {
            //this.gridWebGrid.Visible = false;
            //this.OWCChartSpace1.Display = false;
            this.cmdGridExport.Visible = false;
            //this.OWCPivotTable1.Display = false;
            this.alineCharta.Visible = false;
            this.columnChart.Visible = false;
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

            //加载控件的值
            LoadDisplayControls();
            this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
            this.gridWebGrid.Behaviors.Sorting.Enabled = false;

        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.UCWhereConditions1.UserSelectGoodSemiGood = ItemType.ITEMTYPE_FINISHEDPRODUCT;
            }

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

        private object[] LoadDataSource()
        {
            string byTimeType = UCGroupConditions1.UserSelectByTimeType;

            string groupFieldsX = this.UCGroupConditions1.GetGroupFieldList(preferredTable, "X");
            string groupFieldsY = this.UCGroupConditions1.GetGroupFieldList(preferredTable, "Y");

            ReportSQLEngine engine = new ReportSQLEngine(this.DataProvider, this.languageComponent1);
            engine.DetailedCoreTable = GetDetailedCoreTable();
            engine.Formular = GetFormular();
            engine.WhereCondition = this.UCWhereConditions1.GetWhereSQLStatement(preferredTable, byTimeType, false, 0);
            engine.GroupFieldsX = groupFieldsX;
            engine.GroupFieldsY = groupFieldsY;

            return engine.GetReportDataSource(byTimeType, 0); ;
        }

        private string GetDetailedCoreTable()
        {
            string returnValue = string.Empty;

            returnValue += "SELECT i.itemcode, i.mitemcode || ' - ' || m2.mdesc AS materialcode, i.mdate AS shiftday, i.mocode, m2.vendorcode, i.actiontype, s.bigsscode, COUNT(*) as keypartsinput ";
            returnValue += "FROM tblonwipitem i, tblmaterial m1, tblmaterial m2, tblvendor v, tblss s, tblmo mo ";
            returnValue += "WHERE 1 = 1 ";
            returnValue += "AND i.mcardtype = 0 ";
            returnValue += "AND i.itemcode = m1.mcode ";
            returnValue += "AND i.mitemcode = m2.mcode ";
            returnValue += "AND i.mocode = mo.mocode ";
            returnValue += "AND i.sscode = s.sscode ";
            returnValue += "AND m2.vendorcode = v.vendorcode ";
            returnValue += "GROUP BY i.itemcode, i.mitemcode || ' - ' || m2.mdesc, i.mdate, i.mocode, m2.vendorcode, i.actiontype, s.bigsscode ";

            return returnValue;
        }

        private string GetFormular()
        {
            string returnValue = string.Empty;

            returnValue = "SUM(**.keypartsinput) AS keypartsinput";

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

            schemaList.Add("KeyPartsInput");

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
            int startDate = 0;
            startDate = FormatHelper.TODateInt(UCWhereConditions1.UserSelectStartDate);
            if (startDate <= 0)
            {
                WebInfoPublish.Publish(this, "$Error_NeedStartDate", this.languageComponent1);
                return false;
            }

            int endDate = 0;
            endDate = FormatHelper.TODateInt(UCWhereConditions1.UserSelectEndDate);
            if (endDate <= 0)
            {
                WebInfoPublish.Publish(this, "$Error_NeedEndDate", this.languageComponent1);
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

                string byTimeType = this.UCGroupConditions1.UserSelectByTimeType.Trim().ToLower();
                object[] dateSource = null;

                //一般数据
                dateSource = this.LoadDataSource();
                if (dateSource == null || dateSource.Length <= 0)
                {
                    this.gridWebGrid.Visible = false;
                    //this.OWCChartSpace1.Display = false;
                    this.cmdGridExport.Visible = false;
                    this.alineCharta.Visible = false;
                    this.columnChart.Visible = false;

                    ReportPageHelper.SetPageScrollToBottom(this);
                    return;
                }

                //数据加载到Grid
                List<string> fixedColumnList = GetRows();
                List<ReportGridDim3Property> dim3PropertyList = new List<ReportGridDim3Property>();

                dim3PropertyList.Add(new ReportGridDim3Property("KeyPartsInput", "0", "SUM", "SUM", false));

                ReportGridHelperNew reportGridHelper = new ReportGridHelperNew(this.DataProvider, this.languageComponent1, this.gridWebGrid,this.DtSource);

                reportGridHelper.DataSource = dateSource;
                reportGridHelper.Dim1PropertyList = fixedColumnList;
                reportGridHelper.Dim3PropertyList = dim3PropertyList;
                reportGridHelper.HasDim3PropertyNameRowColumn = true;
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
                    this.alineCharta.Visible = false;
                    this.columnChart.Visible = false;
                }
                else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.LineChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.PieChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                {

                    NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dateSource.Length];
                    dateSource.CopyTo(dateSourceForOWC, 0);

                    string propertyName = this.languageComponent1.GetString(dim3PropertyList[0].Name);
                    foreach (NewReportDomainObject domainObject in dateSourceForOWC)
                    {
                        domainObject.EAttribute1 = propertyName + domainObject.EAttribute1;
                    }

                    List<string> rowPropertyList = GetColumns();
                    List<string> columnPropertyList = GetRows();
                    columnPropertyList.Add("EAttribute1");
                    List<string> valuePropertyList = new List<string>();
                    foreach (ReportGridDim3Property property in dim3PropertyList)
                    {
                        if (!property.Hidden)
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
                        obj.TempValue = obj.KeyPartsInput.ToString();
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
                        this.alineCharta.Visible = true;
                        alineCharta.ChartGroupByString = UCGroupConditions1.GetCheckedColumnsString();

                        //设置首页报表的大小
                        if (ViewState["Width"] != null)
                        {
                            alineCharta.Width = int.Parse(ViewState["Width"].ToString());
                        }

                        if (ViewState["Height"] != null)
                        {
                            alineCharta.Height = int.Parse(ViewState["Height"].ToString());
                        }
                        //end

                        this.alineCharta.ChartTextFormatString = "<DATA_VALUE:0.##>";
                        this.alineCharta.YLabelFormatString = "<DATA_VALUE:0.##>";
                        this.alineCharta.DataType = true;
                        this.alineCharta.DataSource = dateSourceForOWC;
                        this.alineCharta.DataBind();
                    }
                    else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                    {
                        this.columnChart.Visible = true;
                        this.alineCharta.Visible = false;

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
                        this.alineCharta.Visible = false;
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
