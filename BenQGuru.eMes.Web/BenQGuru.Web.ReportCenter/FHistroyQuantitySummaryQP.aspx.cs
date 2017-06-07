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

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.Config;
using System.Collections.Generic;
using BenQGuru.Web.ReportCenter;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.Web.ReportCenter
{
    /// <summary>
    /// FHistroyQuantitySummaryQP 的摘要说明。
    /// </summary>
    public partial class FHistroyQuantitySummaryQP : BaseQPageNew
    {
        public BenQGuru.eMES.Web.UserControl.eMESDate dateStartDateQuery;
        public BenQGuru.eMES.Web.UserControl.eMESDate dateEndDateQuery;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        //private System.ComponentModel.IContainer components;

        protected void Page_Load(object sender, System.EventArgs e)
        {

            RadioButtonListBuilder builder1 = new RadioButtonListBuilder(
                new TimingType(), this.rblTimingType, this.languageComponent1);

            RadioButtonListBuilder builder2 = new RadioButtonListBuilder(
                new SummaryTarget(), this.rblSummaryTarget, this.languageComponent1);
  
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                //this.InitPageLanguage(this.languageComponent1, false);

                this.UCDisplayConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;

                //this.gridWebGrid.Visible = false;
                this.cmdGridExport.Visible = false;
                this.columnChart.Visible = false;
                this.lineChart.Visible = false;

                builder1.Build();
                builder2.Build();

                this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
                this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;


                this.rblSummaryTarget.Attributes.Add("onclick", "judgeSummaryTarget()");
            }

            RadioButtonListBuilder.FormatListControlStyle(this.rblTimingType, 50);
            RadioButtonListBuilder.FormatListControlStyle(this.rblSummaryTarget, 50);

            //加载控件的值
            LoadDisplayControls();
            //this.gridWebGrid.Behaviors.CreateBehavior<RowSelectors>().Enabled = false;
            this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
            this.gridWebGrid.Behaviors.Sorting.Enabled = false;
        }

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
            //this.components = new System.ComponentModel.Container();
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            //// 
            //// languageComponent1
            //// 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";

        }
        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            ReportPageHelper.SetControlValue(this, this.Request.Params);
            ReportPageHelper.DoQueryForBSHome(this, this.Request.Params, this._DoQuery);

            if (this.AutoRefresh)
            {
                this._DoQuery();
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

        private bool _checkRequireFields()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new DateRangeCheck(this.lblStartDateQuery, this.dateStartDateQuery.Text, this.lblEndDateQuery, this.dateEndDateQuery.Text, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return true;
            }
            return true;
        }

        private string V_SummaryTarget
        {
            get
            {
                try
                {
                    return this.ViewState["V_SummaryTarget"].ToString();
                }
                catch
                {
                    return SummaryTarget.Model;
                }
            }
            set
            {
                this.ViewState["V_SummaryTarget"] = value;
            }

        }

        private string[] getOWCSchema()
        {
            return new string[]{
								   "ModelCode", 
								   "ItemCode",
								   "MoCode",
								   "OperationCode",
								   "SegmentCode",
								   "StepSequenceCode",
								   "ResourceCode",
								   "ShiftDay",
								   "ShiftCode",
								   "TimePeriodCode",
								   "Quantity",		
								   "Week",
								   "Month"
							   };
        }

        private string[] getOWCSchema2()
        {
            string[] rows = TimingType.ParserAttributeTimingType(this.rblTimingType.SelectedValue);
            string[] columns = SummaryTarget.ParserAttributeSummaryTarget2(this.rblSummaryTarget.SelectedValue);

            ArrayList schemaList = new ArrayList();
            foreach (string row in rows)
            {
                schemaList.Add(row);
            }
            foreach (string column in columns)
            {
                schemaList.Add(column);
            }
            schemaList.Add("Quantity");
            schemaList.Add("InputQty");

            return (string[])schemaList.ToArray(typeof(string));
        }

        private NewReportDomainObject[] ToNewReportDomainObject(object[] dateSource)
        {
            NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dateSource.Length];
            NewReportDomainObject item;
            for (int i = 0; i < dateSource.Length; i++)
            {
                item = new NewReportDomainObject();
                //item.EAttribute1 = "Quantity";
                item.InputQty = (dateSource[i] as HistroyQuantitySummary).InputQty;
                item.ItemCode = (dateSource[i] as HistroyQuantitySummary).ItemCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).ItemCode.ToString();
                item.MOCode = (dateSource[i] as HistroyQuantitySummary).MoCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).MoCode.ToString();
                item.ModelCode = (dateSource[i] as HistroyQuantitySummary).ModelCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).ModelCode.ToString();
                item.Month = (dateSource[i] as HistroyQuantitySummary).Month == null ? "" : (dateSource[i] as HistroyQuantitySummary).Month.ToString();
                //(dateSource[i] as HistroyQuantitySummary).NatureDate.ToString();
                item.OPCode = (dateSource[i] as HistroyQuantitySummary).OperationCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).OperationCode.ToString();
                item.OutputQty = (dateSource[i] as HistroyQuantitySummary).Quantity;
                item.ResCode = (dateSource[i] as HistroyQuantitySummary).ResourceCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).ResourceCode.ToString();
                item.SegCode = (dateSource[i] as HistroyQuantitySummary).SegmentCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).SegmentCode.ToString();
                item.ShiftCode = (dateSource[i] as HistroyQuantitySummary).ShiftCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).ShiftCode.ToString();
                item.ShiftDay = (dateSource[i] as HistroyQuantitySummary).ShiftDay.ToString();
                item.SSCode = (dateSource[i] as HistroyQuantitySummary).StepSequenceCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).StepSequenceCode.ToString();
                item.PeriodCode = (dateSource[i] as HistroyQuantitySummary).TimePeriodCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).TimePeriodCode.ToString();
                item.Week = (dateSource[i] as HistroyQuantitySummary).Week == null ? "" : (dateSource[i] as HistroyQuantitySummary).Week.ToString();

                if (this.rblTimingType.SelectedValue.ToUpper() == TimingType.TimePeriod.ToUpper())
                {
                    item.TimeString = (dateSource[i] as HistroyQuantitySummary).TimePeriodCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).TimePeriodCode.ToString();
                }
                else if (this.rblTimingType.SelectedValue.ToUpper() == TimingType.Shift.ToUpper())
                {
                    item.TimeString = (dateSource[i] as HistroyQuantitySummary).ShiftCode == null ? "" : (dateSource[i] as HistroyQuantitySummary).ShiftCode.ToString();
                }
                else if (this.rblTimingType.SelectedValue.ToUpper() == TimingType.Day.ToUpper())
                {
                    item.TimeString = (dateSource[i] as HistroyQuantitySummary).ShiftDay.ToString();
                }
                else if (this.rblTimingType.SelectedValue.ToUpper() == TimingType.Week.ToUpper())
                {
                    item.TimeString = (dateSource[i] as HistroyQuantitySummary).Week.ToString();
                }
                else if (this.rblTimingType.SelectedValue.ToUpper() == TimingType.Month.ToUpper())
                {
                    item.TimeString = (dateSource[i] as HistroyQuantitySummary).Month.ToString();
                }

                dateSourceForOWC[i] = item;
            }
            return dateSourceForOWC;
        }

        private void _DoQuery()
        {
            DtSource = new DataTable();
            this.gridWebGrid.ClearDataSource();
            this.gridWebGrid.Columns.Clear();
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);

            if (this._checkRequireFields())
            {
                this.AutoRefresh = this.chbRefreshAuto.Checked;

                string byTimeType = TimingType.ParserAttributeTimingType3(this.rblTimingType.SelectedValue);


                //一般数据
                object[] dateSource = this._loadDataSource();

                if (dateSource == null || dateSource.Length <= 0)
                {
                    this.gridWebGrid.Visible = false;
                    this.cmdGridExport.Visible = false;
                    this.lineChart.Visible = false;
                    this.columnChart.Visible = false;

                    ReportPageHelper.SetPageScrollToBottom(this);
                    return;
                }

                NewReportDomainObject[] dateSourceForOWC = this.ToNewReportDomainObject(dateSource);
                object[] dateSourceCompare = new NewReportDomainObject[0] { };

                //数据加载到Grid
                List<string> fixedColumnList = new List<string>();
                string ColumnList = SummaryTarget.ParserAttributeSummaryTarget3(this.rblSummaryTarget.SelectedValue);
                fixedColumnList.Add(ColumnList);
                this.txtColumn.Text= ColumnList;

                List<ReportGridDim3Property> dim3PropertyList = new List<ReportGridDim3Property>();

                dim3PropertyList.Add(new ReportGridDim3Property("InputQty", "0", "SUM", "SUM", false));
                dim3PropertyList.Add(new ReportGridDim3Property("OutputQty", "0", "SUM", "SUM", false));


                ReportGridHelperNew reportGridHelper = new ReportGridHelperNew(this.DataProvider, this.languageComponent1, this.gridWebGrid,DtSource);

                reportGridHelper.DataSource = dateSourceForOWC;
                reportGridHelper.DataSourceForCompare = dateSourceCompare;
                reportGridHelper.Dim1PropertyList = fixedColumnList;
                reportGridHelper.Dim3PropertyList = dim3PropertyList;
                reportGridHelper.HasDim3PropertyNameRowColumn = true;
                reportGridHelper.CompareType = "";
                reportGridHelper.ByTimeType = byTimeType;

                reportGridHelper.ShowGrid();
                base.InitWebGrid();


                //获取表格和图示
                if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.Grid)
                {
                    this.gridWebGrid.Visible = true;
                    this.cmdGridExport.Visible = true;
                    this.lineChart.Visible = false;
                    this.columnChart.Visible = false;
                }
                else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.LineChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.PieChart
                    || UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                {




                    string propertyName = this.languageComponent1.GetString(dim3PropertyList[0].Name);
                    foreach (NewReportDomainObject domainObject in dateSourceForOWC)
                    {
                        domainObject.EAttribute1 = propertyName + domainObject.EAttribute1;
                    }

                    List<string> rowPropertyList = new List<string>();
                    string[] rows = TimingType.ParserAttributeTimingType2(this.rblTimingType.SelectedValue);
                    if (rows != null)
                    {
                        foreach (string row in rows)
                        {
                            rowPropertyList.Add(row);
                        }
                    }

                    List<string> columnPropertyList = new List<string>();
                    string[] columns = SummaryTarget.ParserAttributeSummaryTarget2(this.rblSummaryTarget.SelectedValue);
                    if (columns != null)
                    {
                        foreach (string column in columns)
                        {
                            columnPropertyList.Add(column);
                        }
                    }
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

                    foreach (NewReportDomainObject obj in dateSourceForOWC)
                    {
                        obj.TempValue = obj.InputQty.ToString();

                        //时段、班次、天、周、月、年
                        if (this.rblTimingType.SelectedValue == TimingType.TimePeriod.ToString())
                        {
                            obj.PeriodCode = obj.PeriodCode.ToString();
                        }

                        if (this.rblTimingType.SelectedValue == TimingType.Shift.ToString())
                        {
                            obj.PeriodCode = obj.ShiftCode.ToString();
                        }

                        if (this.rblTimingType.SelectedValue == TimingType.Day.ToString())
                        {
                            obj.PeriodCode = obj.ShiftDay.ToString();
                        }

                        if (this.rblTimingType.SelectedValue == TimingType.Week.ToString())
                        {
                            obj.PeriodCode = obj.Week.ToString();
                        }

                        if (this.rblTimingType.SelectedValue == TimingType.Month.ToString())
                        {
                            obj.PeriodCode = obj.Month.ToString();
                        }
                        //end
                    }

                    if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.LineChart)
                    {
                        this.columnChart.Visible = false;
                        this.lineChart.Visible = true;

                        lineChart.ChartGroupByString = ColumnList;

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

                        columnChart.ChartGroupByString = ColumnList;
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

                    this.cmdGridExport.Visible = false;
                }

                ReportPageHelper.SetPageScrollToBottom(this);
            }
            else
            {
                this.chbRefreshAuto.Checked = false;
                this.AutoRefresh = false;
            }
            this.gridWebGrid.Behaviors.RowSelectors.RowNumbering = false;
            this.gridWebGrid.Behaviors.Sorting.Enabled = false;
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.V_SummaryTarget = this.rblSummaryTarget.SelectedValue;

            this._DoQuery();
        }

        protected void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
        {
            this._DoQuery();
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this._DoQuery();
            this.GridExport(this.gridWebGrid);
        }

        private object[] _loadDataSource()
        {
          
            System.Collections.Specialized.NameValueCollection iNVC = new System.Collections.Specialized.NameValueCollection(3);
            iNVC.Add("iModelCode", FormatHelper.CleanString(this.txtModelQuery.Text));	//产品别
            iNVC.Add("iItemCode", FormatHelper.CleanString(this.txtItemQuery.Text));		//产品
            iNVC.Add("iMoCode", FormatHelper.CleanString(this.txtMoQuery.Text));			//工单

            QueryFacade1 _QueryFacade = new QueryFacade1(base.DataProvider);
            object[] returnObjs =
                _QueryFacade.QueryHistoryQuantitySummary(
                FormatHelper.CleanString(this.txtCondition.Text),
                FormatHelper.CleanString(this.txtCondition.Text),
                FormatHelper.CleanString(this.txtCondition.Text),
                FormatHelper.CleanString(this.txtCondition.Text),
                FormatHelper.CleanString(this.txtCondition.Text),
                FormatHelper.CleanString(this.txtCondition.Text),
                FormatHelper.CleanString(this.txtCondition.Text),
                iNVC,
                FormatHelper.TODateInt(this.dateStartDateQuery.Text),
                FormatHelper.TODateInt(this.dateEndDateQuery.Text),
                this.rblTimingType.SelectedValue,
                this.V_SummaryTarget,
                "",
                "");


            if (returnObjs != null)
            {
                foreach (HistroyQuantitySummary hyp in returnObjs)
                {
                    if (hyp.Week != null)
                    {
                        hyp.Week = string.Format("{0}W{1}", hyp.ShiftDay.ToString().Substring(2, 2), hyp.Week.PadLeft(2, '0'));
                    }
                    if (hyp.Month != null)
                    {
                        hyp.Month = string.Format("{0}M{1}", hyp.ShiftDay.ToString().Substring(2, 2), hyp.Month.PadLeft(2, '0'));
                    }
                }
            }

            return returnObjs;
        }

        protected void rblSummaryTarget_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string summaryTarget = this.rblSummaryTarget.SelectedValue;
            if (summaryTarget.ToUpper() == SummaryTarget.Model.ToUpper())
            {
                this.txtCondition.Type = "model";
            }
            if (summaryTarget.ToUpper() == SummaryTarget.Item.ToUpper())
            {
                this.txtCondition.Type = "item";
            }
            if (summaryTarget.ToUpper() == SummaryTarget.Mo.ToUpper())
            {
                this.txtCondition.Type = "mo";
            }
            if (summaryTarget.ToUpper() == SummaryTarget.Operation.ToUpper())
            {
                this.txtCondition.Type = "operation";
            }
            if (summaryTarget.ToUpper() == SummaryTarget.Segment.ToUpper())
            {
                this.txtCondition.Type = "segment";
            }
            if (summaryTarget.ToUpper() == SummaryTarget.StepSequence.ToUpper())
            {
                this.txtCondition.Type = "stepsequence";
            }
            if (summaryTarget.ToUpper() == SummaryTarget.Resource.ToUpper())
            {
                this.txtCondition.Type = "resource";
            }
            this.txtCondition.Text = "";
            string lblText = this.languageComponent1.GetString(SummaryTarget.ParserAttributeSummaryTarget(summaryTarget));
            if (lblText != "" && lblText != null)
            {
                this.lblModelCodeQuery.Text = lblText;
            }
        }


    }
}
