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
    public partial class FRMACustomerQP : BaseQPageNew
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

            CheckBoxListBuilder builder = new CheckBoxListBuilder(
                new RMAVisibleStyle2(), this.CheckBoxList1, this.languageComponent1);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this._initialWebGrid();

                builder.Build();

                this.tbOWC1.Visible = false;
                this.tbOWC2.Visible = false;

                foreach (ListItem listItem in CheckBoxList1.Items)
                {
                    listItem.Selected = true;
                }

                this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
                this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;
            }
        }

        private void _initialWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("CustomerCode", "客户代码", null);
            this.gridHelper.AddColumn("RMACount", "RMA件数", null);
            this.gridHelper.AddColumn("RMAQuantity", "RMA数量", null);
            this.gridHelper.AddColumn("OwnRate", "所占比例", null);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
          
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
            this.gridWebGrid.Height = new Unit(200);
            if (this._checkRequireFields())
            {
                FacadeFactory facadeFactory = new FacadeFactory(this.DataProvider);
                object[] dataSource = facadeFactory.CreateQueryRMATSFacade().QueryRMACustomerDis(
                    FormatHelper.TODateInt(this.dateStartDateQuery.Text),
                    FormatHelper.TODateInt(this.dateEndDateQuery.Text),
                    (e as WebQueryEventArgsNew).StartRow,
                    (e as WebQueryEventArgsNew).EndRow
                    ); ;

                (e as WebQueryEventArgsNew).GridDataSource = dataSource;

                (e as WebQueryEventArgsNew).RowCount =
                    facadeFactory.CreateQueryRMATSFacade().QueryRMACustomerDisCount(
                    FormatHelper.TODateInt(this.dateStartDateQuery.Text),
                    FormatHelper.TODateInt(this.dateEndDateQuery.Text));

                this._processOWC(dataSource);
            }
        }


        private NewReportDomainObject[] newreportdomanobject(object[] dataSource)
        {
            if (dataSource != null)
            {
                //dataSource = this.AddOtherInfo(dataSource);

                List<NewReportDomainObject> list = new List<NewReportDomainObject>();

                foreach (QDORMACustomerDis obj in dataSource)
                {

                    NewReportDomainObject reportobj = new NewReportDomainObject();

                    reportobj.TempValue = obj.Count.ToString();
                    reportobj.EAttribute1 = this.languageComponent1.GetString("QTY");
                    reportobj.PeriodCode = obj.CustomerCode;


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
                //dataSource = this.AddOtherInfo(dataSource);

                List<NewReportDomainObject> list = new List<NewReportDomainObject>();
                foreach (QDORMACustomerDis obj in dataSource)
                {

                    NewReportDomainObject reportobj = new NewReportDomainObject();

                    reportobj.TempValue = obj.Quantity.ToString();
                    reportobj.EAttribute1 = this.languageComponent1.GetString("QTY");
                    reportobj.PeriodCode = obj.CustomerCode;


                    list.Add(reportobj);

                }
                return list.ToArray();
            }
            return null;
        }

        private void _processOWC(object[] dataSource)
        {
            //this.OWCChartSpace1.ClearCharts();
            //this.OWCChartSpace2.ClearCharts();

            if (this.FindControl("tbOWC1") != null)
            {
                this.FindControl("tbOWC1").Visible = false;
            }

            if (this.FindControl("tbOWC2") != null)
            {
                this.FindControl("tbOWC2").Visible = false;
            }

            if (dataSource != null)
            {
                foreach (ListItem listItem in CheckBoxList1.Items)
                {
                    if (listItem.Selected)
                    {
                        if (string.Compare(RMAVisibleStyle2.Pie, listItem.Value, true) == 0)
                        {
                            if (this.FindControl("tbOWC1") != null)
                            {
                                this.FindControl("tbOWC1").Visible = true;
                            }

                            NewReportDomainObject[] objs = this.newreportdomanobject(dataSource);

                            if (objs != null)
                            {
                                pie3DChart.Visible = true;
                                pie3DChart.ChartGroupByString = "summaryTarget";
                                this.pie3DChart.ChartTextFormatString = "<DATA_VALUE:#0.##%>";
                                this.pie3DChart.YLabelFormatString = "<DATA_VALUE:#0.##%>";
                                this.pie3DChart.DataType = true;
                                this.pie3DChart.DataSource = objs;
                                this.pie3DChart.DataBind();
                            }
                            else
                            {

                                pie3DChart.Visible = false;
                            }
                        }
                        else if (string.Compare(RMAVisibleStyle2.Clusterd, listItem.Value, true) == 0)
                        {
                            if (this.FindControl("tbOWC2") != null)
                            {
                                this.FindControl("tbOWC2").Visible = true;
                            }
                            NewReportDomainObject[] objs1 = this.newreportdomanobject1(dataSource);

                            if (objs1 != null)
                            {
                                this.columnChart.Visible = true;
                                this.columnChart.ChartGroupByString = "CustomerCode";
                                this.columnChart.DataType = true;
                                this.columnChart.YLabelFormatString = "<DATA_VALUE:0.##>";
                                this.columnChart.ChartTextFormatString = "<DATA_VALUE:0.##>";
                                this.columnChart.DataSource = objs1;
                                this.columnChart.DataBind();

                            }
                            else
                            {
                                this.columnChart.Visible = false;
                            }


                        }
                    }
                }
            }
        }


        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                QDORMACustomerDis obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as QDORMACustomerDis;
                decimal rate = obj.Quantity / obj.TotalQty;
                DataRow row = DtSource.NewRow();
                row["CustomerCode"] = obj.CustomerCode.ToString();
                row["RMACount"] = obj.Count.ToString();
                row["RMAQuantity"] = obj.Quantity.ToString();
                row["OwnRate"] = rate.ToString("##.##%");
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;

            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                QDORMACustomerDis obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as QDORMACustomerDis;
                decimal rate = obj.Quantity / obj.TotalQty;
                (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                    new string[]{
									obj.CustomerCode.ToString(),
									obj.Count.ToString(),
									obj.Quantity.ToString(),
									rate.ToString("##.##%")
								};
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            (e as ExportHeadEventArgsNew).Heads =
                new string[]{
								"CustomerCode",
								"RMACount",
								"RMAQuantity",
								"OwnRate"
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
