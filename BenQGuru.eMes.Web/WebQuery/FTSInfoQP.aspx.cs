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
using BenQGuru.eMES.Web.UserControl;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using System.Collections.Generic;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FTSInfoQP 的摘要说明。
    /// </summary>
    public partial class FTSInfoQP : BaseQPageNew
    {
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit2;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        public BenQGuru.eMES.Web.UserControl.UCNumericUpDown upDown;
        protected ExcelExporter excelExporter = null;
        protected WebQueryHelperNew _helper = null;
       // protected GridHelper gridHelper = null;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);

            this._helper = new WebQueryHelperNew(this.cmdQuery, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1,DtSource);
            this._helper.LoadGridDataSource += new EventHandler(_helper_LoadGridDataSource);
            this._helper.DomainObjectToGridRow += new EventHandler(_helper_DomainObjectToGridRow);
            this._helper.DomainObjectToExportRow += new EventHandler(_helper_DomainObjectToExportRow);
            this._helper.GetExportHeadText += new EventHandler(_helper_GetExportHeadText);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                RadioButtonListBuilder builder = new RadioButtonListBuilder(
                    new TSInfoSummaryTarget(), this.rblSummaryTargetQuery, this.languageComponent1);

                builder.Build();

                this._initialWebGrid();

                this.txtErrorCodeGroup.Enabled = false;
                this.txtErrorCodeGroup.Text = string.Empty;

                this.upDown.Value = 5;

                this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
                this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;

                this.paretoChart.Visible = false;
                this.pie3DChart.Visible = false;
            }

            InitQueryText();

            RadioButtonListBuilder.FormatListControlStyle(this.rblSummaryTargetQuery, 80);
        }

        #region
        private void InitQueryText() 
        {
            this.lblFirstClassGroup.Visible = false;
            this.drpFirstClassQuery.Visible = false;
            this.lblSecondClassGroup.Visible = false;
            this.drpSecondClassQuery.Visible = false;
            this.lblThirdClassGroup.Visible = false;
            this.drpThirdClassQuery.Visible = false;
        }
        #endregion

        private void _initialWebGrid()
        {
           // this.gridWebGrid.Columns.Clear();

            if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCause)
            {
                this.gridHelper.AddColumn(TSInfoSummaryTarget.ErrorCause, "不良原因", null);
                this.gridHelper.AddColumn("ErrorCauseDescription", "不良原因描述", null);
            }
            else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCauseGroup)
            {
                this.gridHelper.AddColumn(TSInfoSummaryTarget.ErrorCauseGroup, "不良原因组", null);
                this.gridHelper.AddColumn("ErrorCauseGroupDescription", "不良原因组描述", null);
            }
            else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCodeGroup)
            {
                this.gridHelper.AddColumn(TSInfoSummaryTarget.ErrorCodeGroup, "不良代码组", null);
                this.gridHelper.AddColumn("ErrorCodeGroupDescription", "不良代码组描述", null);
            }
            else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorLocation)
            {
                this.gridHelper.AddColumn(TSInfoSummaryTarget.ErrorLocation, "不良位置", null);
            }
            else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.Errorcomponent)
            {
                this.gridHelper.AddColumn(TSInfoSummaryTarget.Errorcomponent, "不良组件", null);
            }
            else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.Duty)
            {
                this.gridHelper.AddColumn(TSInfoSummaryTarget.Duty, "责任别", null);
                this.gridHelper.AddColumn("ErrorDutyDescription", "责任别描述", null);
            }
            else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCode)
            {
                this.gridHelper.AddColumn(TSInfoSummaryTarget.ErrorCodeGroup, "不良代码组", null);
                this.gridHelper.AddColumn("ErrorCodeGroupDescription", "不良代码组描述", null);
                this.gridHelper.AddColumn(TSInfoSummaryTarget.ErrorCode, "不良代码", null);
                this.gridHelper.AddColumn("ErrorCodeDescription", "不良代码描述", null);
            }

            this.gridHelper.AddColumn("TsInfoQuantity", "数量", null);
            this.gridHelper.AddColumn("Percent", "百分比", null);
            this.gridHelper.AddLinkColumn("List", "详细信息", null);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.gridWebGrid.Height = 200;
            this.gridWebGrid.DataSource = DtSource;
            this.gridWebGrid.DataBind();
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
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this._helper_GridCellClick);

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

        //add by roger.xue on 2008/11/03: Load drp data
        #region load drp value
        protected void drpFinishSemimanuProduct_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpFinishSemimanuProductQuery.Items.Insert(0, new ListItem("", ""));

                this.drpFinishSemimanuProductQuery.Items.Insert(1, new ListItem(languageComponent1.GetString("itemtype_finishedproduct"), ItemType.ITEMTYPE_FINISHEDPRODUCT));

                this.drpFinishSemimanuProductQuery.Items.Insert(2, new ListItem(languageComponent1.GetString("itemtype_semimanufacture"), ItemType.ITEMTYPE_SEMIMANUFACTURE));
            }
        }


        protected void drpFirstClass_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);

                DropDownListBuilder builder = new DropDownListBuilder(this.drpFirstClassQuery);

                builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(itemFacade.GetItemFirstClass);

                builder.Build("FirstClass", "FirstClass");

                this.drpFirstClassQuery.Items.Insert(0, new ListItem("", ""));
            }
        }


        protected void drpFirstClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string firstClass = this.drpFirstClassQuery.SelectedValue;

            this.drpSecondClassQuery.Items.Clear();
            this.drpThirdClassQuery.Items.Clear();

            if (firstClass.Trim().Length > 0)
            {
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                object[] itemClassList = itemFacade.GetItemSecondClass(firstClass);
                if (itemClassList != null)
                {
                    foreach (ItemClass itemClass in itemClassList)
                    {
                        this.drpSecondClassQuery.Items.Add(new ListItem(itemClass.SecondClass, itemClass.SecondClass));
                    }
                }
            }

            this.drpSecondClassQuery.Items.Insert(0, new ListItem("", ""));
            ReloadDataSource();
            this.paretoChart.Visible = false;
            this.pie3DChart.Visible = false;

        }

        protected void drpSecondClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string firstClass = this.drpFirstClassQuery.SelectedValue;
            string secondClass = this.drpSecondClassQuery.SelectedValue;

            this.drpThirdClassQuery.Items.Clear();

            if (firstClass.Trim().Length > 0 && secondClass.Trim().Length > 0)
            {
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                object[] itemClassList = itemFacade.GetItemThirdClass(firstClass, secondClass);
                if (itemClassList != null)
                {
                    foreach (ItemClass itemClass in itemClassList)
                    {
                        this.drpThirdClassQuery.Items.Add(new ListItem(itemClass.ThirdClass, itemClass.ThirdClass));
                    }
                }
            }

            this.drpThirdClassQuery.Items.Insert(0, new ListItem("", ""));
            ReloadDataSource();
            this.paretoChart.Visible = false;
            this.pie3DChart.Visible = false;
        }

        #endregion

        //end add

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

        private void ReloadDataSource()
        {
            DtSource = new DataTable();
            this.gridWebGrid.ClearDataSource();
            this.gridWebGrid.Columns.Clear();
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);

            this._initialWebGrid();
            if (this._checkRequireFields())
            {
                int startRow = (this.gridHelper._pagerToolBar.PageCount - 1) * this.gridHelper._pagerToolBar.PageSize + 1;
                int endRow = this.gridHelper._pagerToolBar.RowCount;
                FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
                object[] dataSource =
                    facadeFactory.CreateQueryTSInfoFacade().QueryTSInfo(
                    FormatHelper.CleanString(this.txtErrorCodeGroup.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorCode.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorCause.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorCauseGroup.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorLocation.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorDuty.Text).ToUpper(),
                    FormatHelper.CleanString(this.drpFinishSemimanuProductQuery.SelectedValue),
                    FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtFromResource.Text).ToUpper(),
                    FormatHelper.CleanString(this.drpFirstClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpSecondClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpThirdClassQuery.SelectedValue),
                    FormatHelper.TODateInt(this.dateStartDateQuery.Text),
                    FormatHelper.TODateInt(this.dateEndDateQuery.Text),
                    this.rblSummaryTargetQuery.SelectedValue,
                    FormatHelper.CleanString(this.txtLotNo.Text).ToUpper(),
                    int.Parse(this.upDown.Control.Text),
                    FormatHelper.CleanString(this.txtErrorcomponentQuery.Text).ToUpper(),
                    startRow,
                    endRow);

                this.gridHelper.RefreshData();
                this._processOWC(dataSource);
            }
        }



        private void _helper_LoadGridDataSource(object sender, EventArgs e)
        {
            this._initialWebGrid();
            if (this._checkRequireFields())
            {
                FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
                object[] dataSource =
                    facadeFactory.CreateQueryTSInfoFacade().QueryTSInfo(
                    FormatHelper.CleanString(this.txtErrorCodeGroup.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorCode.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorCause.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorCauseGroup.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorLocation.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorDuty.Text).ToUpper(),
                    FormatHelper.CleanString(this.drpFinishSemimanuProductQuery.SelectedValue),
                    FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtFromResource.Text).ToUpper(),
                    FormatHelper.CleanString(this.drpFirstClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpSecondClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpThirdClassQuery.SelectedValue),
                    FormatHelper.TODateInt(this.dateStartDateQuery.Text),
                    FormatHelper.TODateInt(this.dateEndDateQuery.Text),
                    this.rblSummaryTargetQuery.SelectedValue,
                    FormatHelper.CleanString(this.txtLotNo.Text).ToUpper(),
                    this.upDown.Value,
                    FormatHelper.CleanString(this.txtErrorcomponentQuery.Text).ToUpper(),
                    (e as WebQueryEventArgsNew).StartRow,
                    (e as WebQueryEventArgsNew).EndRow);

                (e as WebQueryEventArgsNew).GridDataSource = dataSource;

                (e as WebQueryEventArgsNew).RowCount =
                    facadeFactory.CreateQueryTSInfoFacade().QueryTSInfoCount(
                    FormatHelper.CleanString(this.txtErrorCodeGroup.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorCode.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorCause.Text).ToUpper(),
                     FormatHelper.CleanString(this.txtErrorCauseGroup.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorLocation.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtErrorDuty.Text).ToUpper(),
                    FormatHelper.CleanString(this.drpFinishSemimanuProductQuery.SelectedValue),
                    FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtFromResource.Text).ToUpper(),
                    FormatHelper.CleanString(this.drpFirstClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpSecondClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpThirdClassQuery.SelectedValue),
                    FormatHelper.TODateInt(this.dateStartDateQuery.Text),
                    FormatHelper.TODateInt(this.dateEndDateQuery.Text),
                    this.rblSummaryTargetQuery.SelectedValue,
                    FormatHelper.CleanString(this.txtLotNo.Text).ToUpper(),
                    this.upDown.Value,
                    FormatHelper.CleanString(this.txtErrorcomponentQuery.Text).ToUpper());

                this._processOWC(dataSource);
            }
        }


        private NewReportDomainObject[] newreportdomanobject(object[] dataSource)
        {
            if (dataSource != null)
            {
                dataSource = this.AddOtherInfo(dataSource);

                List<NewReportDomainObject> list = new List<NewReportDomainObject>();

                foreach (QDOTSInfo obj in dataSource)
                {

                    NewReportDomainObject reportobj = new NewReportDomainObject();

                    reportobj.TempValue = obj.Percent.ToString();
                    reportobj.EAttribute1 = this.languageComponent1.GetString("Percent");

                    if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCause)
                    {
                        reportobj.PeriodCode = obj.ErrorCause.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }

                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCauseGroup)
                    {
                        reportobj.PeriodCode = obj.ErrorCauseGroup.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }
                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCodeGroup)
                    {
                        reportobj.PeriodCode = obj.ErrorCodeGroup.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }
                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorLocation)
                    {
                        reportobj.PeriodCode = obj.ErrorLocation.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }
                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.Duty)
                    {
                        reportobj.PeriodCode = obj.Duty.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }
                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCode)
                    {
                        reportobj.PeriodCode = obj.ErrorCode.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }
                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.Errorcomponent)
                    {
                        reportobj.PeriodCode = obj.ErrorComponent.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }

                    list.Add(reportobj);

                }
                return list.ToArray();
            }
            return null;
        }

        private NewReportDomainObject[] newreportdomanobject1(object[] dataSource)
        {
            if (dataSource != null)
            {
                dataSource = this.AddOtherInfo(dataSource);

                List<NewReportDomainObject> list = new List<NewReportDomainObject>();
                foreach (QDOTSInfo obj in dataSource)
                {

                    NewReportDomainObject reportobj = new NewReportDomainObject();

                    reportobj.TempValue = obj.Quantity.ToString();
                    reportobj.EAttribute1 = this.languageComponent1.GetString("QTY");

                    if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCause)
                    {
                        reportobj.PeriodCode = obj.ErrorCause.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }

                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCauseGroup)
                    {
                        reportobj.PeriodCode = obj.ErrorCauseGroup.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }
                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCodeGroup)
                    {
                        reportobj.PeriodCode = obj.ErrorCodeGroup.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }
                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorLocation)
                    {
                        reportobj.PeriodCode = obj.ErrorLocation.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }
                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.Duty)
                    {
                        reportobj.PeriodCode = obj.Duty.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }
                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCode)
                    {
                        reportobj.PeriodCode = obj.ErrorCode.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }
                    else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.Errorcomponent)
                    {
                        reportobj.PeriodCode = obj.ErrorComponent.ToString();
                        reportobj.DutyCode = this.rblSummaryTargetQuery.SelectedValue;
                    }

                    list.Add(reportobj);

                }
                return list.ToArray();
            }
            return null;
        }

        private void _processOWC(object[] dataSource)
        {
            NewReportDomainObject[] objs = this.newreportdomanobject(dataSource);
            NewReportDomainObject[] objs1 = this.newreportdomanobject1(dataSource);

            if (objs != null && objs1 != null)
            {
                paretoChart.Visible = true;
                pie3DChart.Visible = true;

                paretoChart.ChartGroupByString = "summaryTarget";
                this.paretoChart.LineLable = this.languageComponent1.GetString("Percent");
                this.paretoChart.YLabelFormatString = "<DATA_VALUE:0.##>";
                this.paretoChart.Y2LabelFormatString = "<DATA_VALUE:00.##>";
                this.paretoChart.DataType = true;
                this.paretoChart.DataSource = objs1;
                this.paretoChart.DataBind();

                pie3DChart.ChartGroupByString = "summaryTarget";
                this.pie3DChart.ChartTextFormatString = "<DATA_VALUE:#0.##%>";
                this.pie3DChart.YLabelFormatString = "<DATA_VALUE:#0.##%>";
                this.pie3DChart.DataType = true;
                this.pie3DChart.DataSource = objs1;
                this.pie3DChart.DataBind();
            }
            else
            {
                paretoChart.Visible = false;
                pie3DChart.Visible = false;
            }
        }

        //获取最小单元刻度
        private int GetMahorUnit(int quantity)
        {
            if (quantity < 10)
                return 1;

            return 0;
        }

        private int GetMahorUnit2(int quantity)
        {
            if (quantity < 10) return 1;
            if (quantity < 100) return 10;
            int length = quantity.ToString().Length;

            int ff = Convert.ToInt32(quantity.ToString().Substring(0, 1));
            if (ff < 2)
            {
                return 1 * Convert.ToInt32(Math.Pow(10, length - 2));
            }
            else if (ff > 5)
            {
                return 5 * Convert.ToInt32(Math.Pow(10, length - 2));
            }
            else
            {
                return 2 * Convert.ToInt32(Math.Pow(10, length - 2));
            }
        }

        //添加其它统计栏位
        private object[] AddOtherInfo(object[] dataSource)
        {
            if (dataSource != null && dataSource.Length > 0)
            {
                QDOTSInfo otherQDOTSInfo = new QDOTSInfo();
                string OtherCode = "其它";
                otherQDOTSInfo.ErrorCodeGroup = OtherCode;
                otherQDOTSInfo.ErrorCode = OtherCode;
                otherQDOTSInfo.ErrorCauseGroup = OtherCode;
                otherQDOTSInfo.ErrorCause = OtherCode;
                otherQDOTSInfo.ErrorLocation = OtherCode;
                otherQDOTSInfo.Duty = OtherCode;
                otherQDOTSInfo.ErrorComponent = OtherCode;
                otherQDOTSInfo.AllQuantity = ((QDOTSInfo)dataSource[0]).AllQuantity;
                otherQDOTSInfo.Quantity = otherQDOTSInfo.AllQuantity - (int)this.getMaxNum(dataSource);
                decimal percent = ((decimal)otherQDOTSInfo.Quantity) / ((decimal)otherQDOTSInfo.AllQuantity);
                otherQDOTSInfo.Percent = percent;

                object[] newDataSource = new object[dataSource.Length + 1];
                for (int i = 0; i < dataSource.Length; i++)
                {
                    newDataSource[i] = dataSource[i];
                }
                newDataSource[dataSource.Length] = otherQDOTSInfo;

                return newDataSource;
            }
            return dataSource;
        }

        private decimal getParetoValue(object[] dataSource, int count)
        {
            //柏拉图的value 是累计值,根据传入的累计数,统计累计值
            decimal returnValue = 0;

            for (int i = 0; i <= count; i++)
            {
                returnValue += (dataSource[i] as QDOTSInfo).Percent;
            }
            return returnValue;
        }
        private double getMaxNum(object[] dataSource)
        {
            //统计传入数据源的最大值,用来设置左边Y轴的最大值
            double returnValue = 0;

            for (int i = 0; i <= dataSource.Length - 1; i++)
            {
                returnValue += (dataSource[i] as QDOTSInfo).Quantity;
            }
            return returnValue;
        }

        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                QDOTSInfo obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as QDOTSInfo;
                DataRow row = DtSource.NewRow();
                if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCode.ToUpper())
                {
                    row[TSInfoSummaryTarget.ErrorCodeGroup] =obj.ErrorCodeGroup;
                    row["ErrorCodeGroupDescription"]=obj.ErrorCodeGroupDesc;
                    row[TSInfoSummaryTarget.ErrorCode]=obj.ErrorCode;
                    row["ErrorCodeDescription"] = obj.ErrorCodeDesc;
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCodeGroup.ToUpper())
                {
                    row[TSInfoSummaryTarget.ErrorCodeGroup] = obj.ErrorCodeGroup;
                    row["ErrorCodeGroupDescription"] = obj.ErrorCodeGroupDesc;
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCauseGroup.ToUpper())
                {
                    row[TSInfoSummaryTarget.ErrorCauseGroup] = obj.ErrorCauseGroup;
                    row["ErrorCauseGroupDescription"] = obj.ErrorCauseGroupDesc;
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCause.ToUpper())
                {
                    row[TSInfoSummaryTarget.ErrorCause] = obj.ErrorCause;
                    row["ErrorCauseDescription"] = obj.ErrorCauseDesc;
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorLocation.ToUpper())
                {
                    row[TSInfoSummaryTarget.ErrorLocation] = obj.ErrorLocation;
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.Duty.ToUpper())
                {
                    row[TSInfoSummaryTarget.Duty] = obj.Duty;
                    row["ErrorDutyDescription"] = obj.DutyDesc;
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.Errorcomponent.ToUpper())
                {
                    row[TSInfoSummaryTarget.Errorcomponent] = obj.ErrorComponent;
                }

                row["TsInfoQuantity"]=obj.Quantity;
                row["Percent"] = System.Decimal.Round(obj.Percent * 100, 2) + "%";
                row["List"] = "";

                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                //modified by jessie lee for CS0096, 2005/10/10
                QDOTSInfo obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as QDOTSInfo;

                ArrayList objList = new ArrayList();
                if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCode.ToUpper())
                {
                    objList.Add(obj.ErrorCodeGroup);
                    objList.Add(obj.ErrorCodeGroupDesc);
                    objList.Add(obj.ErrorCode);
                    objList.Add(obj.ErrorCodeDesc);
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCodeGroup.ToUpper())
                {
                    objList.Add(obj.ErrorCodeGroup);
                    objList.Add(obj.ErrorCodeGroupDesc);
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCauseGroup.ToUpper())
                {
                    objList.Add(obj.ErrorCauseGroup);
                    objList.Add(obj.ErrorCauseGroupDesc);
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCause.ToUpper())
                {
                    objList.Add(obj.ErrorCause);
                    objList.Add(obj.ErrorCauseDesc);
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorLocation.ToUpper())
                {
                    objList.Add(obj.ErrorLocation);
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.Duty.ToUpper())
                {
                    objList.Add(obj.Duty);
                    objList.Add(obj.DutyDesc);
                }
                else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.Errorcomponent.ToUpper())
                {
                    objList.Add(obj.ErrorComponent);
                }

                objList.Add(obj.Quantity.ToString());
                objList.Add(obj.Percent.ToString(".##%"));

                (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                                    (string[])objList.ToArray(typeof(string));
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            ArrayList objList = new ArrayList();
            if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCode.ToUpper())
            {
                objList.Add("ErrorCodeGroup");
                objList.Add("ErrorCodeGroupDescription");
                objList.Add("ErrorCode");
                objList.Add("ErrorCodeDescription");
            }
            else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCodeGroup.ToUpper())
            {
                objList.Add("ErrorCodeGroup");
                objList.Add("ErrorCodeGroupDescription");
            }
            else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCause.ToUpper())
            {
                objList.Add("ErrorCause");
                objList.Add("ErrorCauseDescription");
            }
            else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorCauseGroup.ToUpper())
            {
                objList.Add("ErrorCauseGroup");
                objList.Add("ErrorCauseGroupDescription");
            }
            else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.ErrorLocation.ToUpper())
            {
                objList.Add("ErrorLocation");
            }
            else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.Duty.ToUpper())
            {
                objList.Add("Duty");
                objList.Add("ErrorDutyDescription");
            }
            else if (this.rblSummaryTargetQuery.SelectedValue.ToUpper() == TSInfoSummaryTarget.Errorcomponent.ToUpper())
            {
                objList.Add("Errorcomponent");
            }
            objList.Add("TsInfoQuantity");
            objList.Add("Percent");

            (e as ExportHeadEventArgsNew).Heads = (string[])objList.ToArray(typeof(string))
                            ;
        }

        protected override void Grid_ClickCell(GridRecord row, string command)
        {

            if (command == "List")
            {
                object objTmp = row.Items.FindItemByKey(this.rblSummaryTargetQuery.SelectedValue);
                string summaryObject = row.Items.FindItemByKey(this.rblSummaryTargetQuery.SelectedValue).Text;
                string summaryObjectDesc = string.Empty;
                string summaryObject1 = string.Empty;
                string summaryObject1Desc = string.Empty;
                if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCause)
                {
                    summaryObjectDesc = row.Items.FindItemByKey("ErrorCauseDescription").Text;
                }
                else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCauseGroup)
                {
                    summaryObjectDesc = row.Items.FindItemByKey("ErrorCauseGroupDescription").Text;
                }
                else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCodeGroup)
                {
                    summaryObjectDesc = row.Items.FindItemByKey("ErrorCodeGroupDescription").Text;
                }
                else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.Duty)
                {
                    summaryObjectDesc = row.Items.FindItemByKey("ErrorDutyDescription").Text;
                }
                else if (this.rblSummaryTargetQuery.SelectedValue == TSInfoSummaryTarget.ErrorCode)
                {
                    summaryObjectDesc = row.Items.FindItemByKey("ErrorCodeDescription").Text;
                    summaryObject1 = row.Items.FindItemByKey(TSInfoSummaryTarget.ErrorCodeGroup).Text;
                    summaryObject1Desc = row.Items.FindItemByKey("ErrorCodeGroupDescription").Text;
                }


                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FTSInfoListQP.aspx",
                    new string[]{
									"12_ModelCode",
									"12_ItemCode",
									"12_MoCode",
									"12_FrmResCodes",
									"12_StartDate",
									"12_EndDate",
									"12_ECG",
									"12_EC",
									"12_ECS",
                                    "12_ECSG",
									"12_LOC",
									"12_DUTY",
									"12_SummaryTarget",
									"12_SummaryObject",
									"12_SummaryObjectDesc",
									"12_SummaryObject1",
									"12_SummaryObject1Desc",
									"12_LotNo",
                                    "12_Errorcomponent",
                                    "12_FirstClassGroup",
                                    "12_SecondClassGroup",
                                    "12_ThirdClassGroup"
								},
                    new string[]{
									FormatHelper.CleanString(this.drpFinishSemimanuProductQuery.SelectedValue),	
									FormatHelper.CleanString(this.txtConditionItem.Text),	
									FormatHelper.CleanString(this.txtConditionMo.Text),	
									FormatHelper.CleanString(this.txtFromResource.Text),	
									this.dateStartDateQuery.Text,
									this.dateEndDateQuery.Text,
									FormatHelper.CleanString(this.txtErrorCodeGroup.Text),
									FormatHelper.CleanString(this.txtErrorCode.Text),
									FormatHelper.CleanString(this.txtErrorCause.Text),
                                    FormatHelper.CleanString(this.txtErrorCauseGroup.Text),
									FormatHelper.CleanString(this.txtErrorLocation.Text),
									FormatHelper.CleanString(this.txtErrorDuty.Text),
									this.rblSummaryTargetQuery.SelectedValue,
									summaryObject,
									summaryObjectDesc,
									summaryObject1,
									summaryObject1Desc,
									this.txtLotNo.Text.Trim().ToUpper(),
                                    FormatHelper.CleanString(this.txtErrorcomponentQuery.Text),
                                    FormatHelper.CleanString(this.drpFirstClassQuery.SelectedValue),
                                    FormatHelper.CleanString(this.drpSecondClassQuery.SelectedValue),
                                    FormatHelper.CleanString(this.drpThirdClassQuery.SelectedValue)
								})
                    );
            }
        }

        //根据选择的统计对象的不同,显示的查询条件也不相同
        protected void rblSummaryTargetQuery_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.txtErrorCodeGroup.Enabled = true;
            this.txtErrorCode.Enabled = true;
            this.txtErrorCauseGroup.Enabled = true;
            this.txtErrorCause.Enabled = true;
            this.txtErrorLocation.Enabled = true;
            this.txtErrorDuty.Enabled = true;
            this.txtErrorcomponentQuery.Enabled = true;

            if (this.rblSummaryTargetQuery.SelectedIndex == 0)
            {
                this.txtErrorCodeGroup.Enabled = false;
                this.txtErrorCodeGroup.Text = string.Empty;
            }
            else if (this.rblSummaryTargetQuery.SelectedIndex == 1)
            {
                this.txtErrorCode.Enabled = false;
                this.txtErrorCode.Text = string.Empty;
            }
            else if (this.rblSummaryTargetQuery.SelectedIndex == 2)
            {
                this.txtErrorCauseGroup.Enabled = false;
                this.txtErrorCauseGroup.Text = string.Empty;
            }
            else if (this.rblSummaryTargetQuery.SelectedIndex == 3)
            {
                this.txtErrorCause.Enabled = false;
                this.txtErrorCause.Text = string.Empty;
            }
            else if (this.rblSummaryTargetQuery.SelectedIndex == 4)
            {
                this.txtErrorLocation.Enabled = false;
                this.txtErrorLocation.Text = string.Empty;
            }
            else if (this.rblSummaryTargetQuery.SelectedIndex == 5)
            {
                this.txtErrorDuty.Enabled = false;
                this.txtErrorDuty.Text = string.Empty;
            }
            else if (this.rblSummaryTargetQuery.SelectedIndex == 6)
            {
                this.txtErrorcomponentQuery.Enabled = false;
                this.txtErrorcomponentQuery.Text = string.Empty;
            }

            ReloadDataSource();
            this.paretoChart.Visible = false;
            this.pie3DChart.Visible = false;

        }
    }
}
