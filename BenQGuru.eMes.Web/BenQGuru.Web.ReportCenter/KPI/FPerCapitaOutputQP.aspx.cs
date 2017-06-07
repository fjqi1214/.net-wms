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
using BenQGuru.eMES.Web.SelectQuery;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Web.UserControl;
using BenQGuru.eMES.Domain.KPI;

namespace BenQGuru.Web.ReportCenter
{
    public partial class FPerCapitaOutputQP : BaseQPageNew
    {
        #region 页面初始化
        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private BenQGuru.eMES.WebQuery.KPIQueryFacade KPIFacade;
        private const string preferredTable = "tblmesentitylist,tblmaterial,tblmo,tblres,tblline2crew,tblitemclass,tbltimedimension";
        private static object[] dateSource = null;
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
            this.UCWhereConditions1.SetControlPosition(0, 0, UCWhereConditions1.PanelItemCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 1, UCWhereConditions1.PanelSSCodeWhere.ID);
            this.UCWhereConditions1.SetControlPosition(0, 2, UCWhereConditions1.PanelShiftCodeWhere.ID);

            this.UCWhereConditions1.SetControlPosition(1, 0, UCWhereConditions1.PanelStartDateWhere.ID);
            this.UCWhereConditions1.SetControlPosition(1, 1, UCWhereConditions1.PanelEndDateWhere.ID);
        }

        private void InitGroupControls()
        {
            this.UCGroupConditions1.ShowSSCode = true;
            this.UCGroupConditions1.ShowItemCode = true;
            this.UCGroupConditions1.ShowSp1 = true;
        }

        private void InitResultControls()
        {
            this.gridWebGrid.Visible = false;
            this.cmdGridExport.Visible = false;
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
                //displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.LineChart), NewReportDisplayType.LineChart));
                //displayList.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.HistogramChart), NewReportDisplayType.HistogramChart));
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
            this.InitOnPostBack();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.UCWhereConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCGroupConditions1.InitUserControl(this.languageComponent1, this.DataProvider);
                this.UCDisplayConditions1.InitUserControl(this.languageComponent1, this.DataProvider);

                this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;

                this.columnChart.Visible = false;
                this.lineChart.Visible = false;
            }

            //加载控件的值
            LoadDisplayControls();
        }

        private void InitOnPostBack()
        {

            this.gridHelper = new GridHelperNew(this.gridWebGrid, DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

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

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            bool ssCodeChecked = UCGroupConditions1.SSChecked;
            bool itemCodeChecked = UCGroupConditions1.ItemCodeChecked;
            string groupFields = string.Empty;
            if (ssCodeChecked || itemCodeChecked)
            {
                groupFields = (ssCodeChecked == true ? "**.SSCODE," : "") + (itemCodeChecked == true ? "**.ITEMCODE," : "");
            }
            if (KPIFacade == null) { KPIFacade = new KPIQueryFacade(base.DataProvider); }
            dateSource = KPIFacade.QueryPerCapita(((SelectableTextBox)UCWhereConditions1.PanelItemCodeWhere.Controls[3]).Text.Trim(), ((SelectableTextBox4SS)UCWhereConditions1.PanelSSCodeWhere.Controls[3]).Text.Trim(), ((DropDownList)UCWhereConditions1.PanelShiftCodeWhere.Controls[3]).SelectedValue,
                ((HtmlInputText)UCWhereConditions1.PanelStartDateWhere.Controls[3]).Value.Replace("-", ""),
                ((HtmlInputText)UCWhereConditions1.PanelEndDateWhere.Controls[3]).Value.Replace("-", ""),
                groupFields
                );
            return dateSource;
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.Grid.Columns.Clear();
            if (UCGroupConditions1.SSChecked)
            {
                this.gridHelper.AddColumn("SSCODE", "产线代码", null);
            }
            if (UCGroupConditions1.ItemCodeChecked)
            {
                this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            }
            this.gridHelper.AddColumn("ACQ", "标准工时", null);
            this.gridHelper.AddColumn("ACT", "作业工时", null);
            this.gridHelper.AddColumn("WOR", "工作时长", null);
            this.gridHelper.AddColumn("DATETIME", "机型标准工时", null);
            this.gridHelper.AddColumn("PER", "人均生产数", null);
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            
            if (UCGroupConditions1.SSChecked && UCGroupConditions1.ItemCodeChecked)
            {
                row["SSCODE"] = ((PerCapitaOutput)obj).sscode.ToString();
                row["ItemCode"] = ((PerCapitaOutput)obj).itemcode.ToString();
                row["ACQ"] = ((PerCapitaOutput)obj).acq.ToString();
                row["ACT"] = ((PerCapitaOutput)obj).act.ToString();
                row["WOR"] = ((PerCapitaOutput)obj).wor.ToString();
                row["DATETIME"] = ((PerCapitaOutput)obj).datetime.ToString();
                row["PER"] = ((PerCapitaOutput)obj).per == null ? "" : ((PerCapitaOutput)obj).per.ToString();
            }
            else if (UCGroupConditions1.SSChecked || UCGroupConditions1.ItemCodeChecked)
            {
                if (UCGroupConditions1.SSChecked)
                {
                    row["SSCODE"] = ((PerCapitaOutput)obj).sscode.ToString();
                }
                else
                {
                    row["ItemCode"] = ((PerCapitaOutput)obj).itemcode.ToString();
                }
                row["ACQ"] = ((PerCapitaOutput)obj).acq.ToString();
                row["ACT"] = ((PerCapitaOutput)obj).act.ToString();
                row["WOR"] = ((PerCapitaOutput)obj).wor.ToString();
                row["DATETIME"] = ((PerCapitaOutput)obj).datetime.ToString();
                row["PER"] = ((PerCapitaOutput)obj).per == null ? "" : ((PerCapitaOutput)obj).per.ToString();
            }
            else
            {
                
                row["ACQ"] = ((PerCapitaOutput)obj).acq.ToString();
                row["ACT"] = ((PerCapitaOutput)obj).act.ToString();
                row["WOR"] = ((PerCapitaOutput)obj).wor.ToString();
                row["DATETIME"] = ((PerCapitaOutput)obj).datetime.ToString();
                row["PER"] = ((PerCapitaOutput)obj).per == null ? "" : ((PerCapitaOutput)obj).per.ToString();
               
            }
            return row;
        }

        #endregion

        private void DoQuery()
        {
            if (true)
            {
                this.AutoRefresh = this.chbRefreshAuto.Checked;
                InitWebGrid();
                this.gridHelper.GridBind(int.MaxValue, int.MaxValue);
                if (dateSource == null || dateSource.Length <= 0)
                {
                    this.gridWebGrid.Visible = false;
                    this.cmdGridExport.Visible = false;
                    this.lineChart.Visible = false;
                    this.columnChart.Visible = false;
                    ReportPageHelper.SetPageScrollToBottom(this);
                    return;
                }
                //获取表格和图示
                if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.Grid)
                {
                    this.gridWebGrid.Visible = true;
                    this.cmdGridExport.Visible = true;
                    this.lineChart.Visible = false;
                    this.columnChart.Visible = false;
                }
                else if (UCDisplayConditions1.GetDisplayType().Trim().ToLower() == NewReportDisplayType.HistogramChart)
                {
                    this.columnChart.Visible = true;
                    this.lineChart.Visible = false;
                    this.gridWebGrid.Visible = false;
                    this.cmdGridExport.Visible = false;
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
                    this.columnChart.DataSource = null;
                    this.columnChart.DataBind();
                }
                else
                {
                    this.columnChart.Visible = false;
                    this.lineChart.Visible = false;
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
