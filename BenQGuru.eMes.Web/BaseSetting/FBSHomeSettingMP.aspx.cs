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
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class FBSHomeSettingMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private SystemSettingFacade _SystemSettingFacade = null;

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);

            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

           // this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
        }

        #endregion

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
            _SystemSettingFacade = new SystemSettingFacade(this.DataProvider);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.BuildReportSeqList();
                this.BuildModuleCodeList();
                this.BuildChartTypeList();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        private void BuildReportSeqList()
        {
            this.ddlReportSeqEdit.Items.Add(new ListItem("", ""));
            this.ddlReportSeqEdit.Items.Add(new ListItem("1", "1"));
            this.ddlReportSeqEdit.Items.Add(new ListItem("2", "2"));
            this.ddlReportSeqEdit.Items.Add(new ListItem("3", "3"));
            this.ddlReportSeqEdit.Items.Add(new ListItem("4", "4"));

            this.ddlReportSeqEdit.SelectedIndex = 0;
        }

        private void BuildModuleCodeList()
        {
            this.ddlModuleEdit.Items.Add(new ListItem("", ""));

            object[] list = this._SystemSettingFacade.GetAllNewReportModules();
            if (list != null)
            {
                foreach (Domain.BaseSetting.Module module in list)
                {
                    this.ddlModuleEdit.Items.Add(new ListItem(FormatHelper.GetModuleTitle(this.languageComponent1, module.ModuleCode), module.ModuleCode));

                }
            }

            this.ddlModuleEdit.SelectedIndex = 0;
        }

        private void BuildChartTypeList()
        {
            this.ddlChartTypeEdit.Items.Add(new ListItem("", ""));
            this.ddlChartTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.LineChart), NewReportDisplayType.LineChart));
            this.ddlChartTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.HistogramChart), NewReportDisplayType.HistogramChart));
            this.ddlChartTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(NewReportDisplayType.PieChart), NewReportDisplayType.PieChart));

            this.ddlChartTypeEdit.SelectedIndex = 0;
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ReportSeq", "报表位置", null);
            this.gridHelper.AddColumn("Module", "模块", null);
            this.gridHelper.AddColumn("ChartType", "图表类型", null);
            this.gridHelper.AddLinkColumn("Detail", "详细", null);
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //        "false",
            //        ((BSHomeSetting)obj).ReportSeq.ToString(),
            //        FormatHelper.GetModuleTitle(this.languageComponent1, ((BSHomeSetting)obj).ModuleCode),
            //        this.languageComponent1.GetString(((BSHomeSetting)obj).ChartType),
            //        "",
            //        ""
            //    }
            //);
            DataRow row = this.DtSource.NewRow();
            row["ReportSeq"] = ((BSHomeSetting)obj).ReportSeq.ToString();
            row["Module"] = FormatHelper.GetModuleTitle(this.languageComponent1, ((BSHomeSetting)obj).ModuleCode);
            row["ChartType"] = this.languageComponent1.GetString(((BSHomeSetting)obj).ChartType);
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._SystemSettingFacade.QueryBSHomeSetting(0, inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            return this._SystemSettingFacade.QueryBSHomeSettingCount(0);
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName=="Detail")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FBSHomeSettingDetailMP.aspx", new string[] { "reportseq" }, new string[] { row.Items.FindItemByKey("ReportSeq").Text.Trim() }));
            }
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            this._SystemSettingFacade.AddBSHomeSetting((BSHomeSetting)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            this._SystemSettingFacade.DeleteBSHomeSetting((BSHomeSetting[])domainObjects.ToArray(typeof(BSHomeSetting)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            this._SystemSettingFacade.UpdateBSHomeSetting((BSHomeSetting)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.ddlReportSeqEdit.Enabled = true;
                this.ddlModuleEdit.Enabled = true;
                this.ddlChartTypeEdit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.ddlReportSeqEdit.Enabled = false;
                this.ddlModuleEdit.Enabled = true;
                this.ddlChartTypeEdit.Enabled = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            BSHomeSetting setting = this._SystemSettingFacade.CreateNewBSHomeSetting();

            setting.ReportSeq = int.Parse(this.ddlReportSeqEdit.SelectedValue);
            setting.ModuleCode = this.ddlModuleEdit.SelectedValue;
            setting.ChartType = this.ddlChartTypeEdit.SelectedValue;
            setting.MaintainUser = this.GetUserCode();

            return setting;
        }

        protected override object GetEditObject(GridRecord row)
        {

            object obj = _SystemSettingFacade.GetBSHomeSetting(int.Parse(row.Items.FindItemByKey("ReportSeq").Text));

            if (obj != null)
            {
                return (BSHomeSetting)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.ddlReportSeqEdit.SelectedIndex = 0;
                this.ddlModuleEdit.SelectedIndex = 0;
                this.ddlChartTypeEdit.SelectedIndex = 0;

                return;
            }

            try
            {
                this.ddlReportSeqEdit.SelectedValue = ((BSHomeSetting)obj).ReportSeq.ToString();
            }
            catch
            {
                this.ddlReportSeqEdit.SelectedIndex = 0;
            }

            try
            {
                this.ddlModuleEdit.SelectedValue = ((BSHomeSetting)obj).ModuleCode;

            }
            catch
            {
                this.ddlModuleEdit.SelectedIndex = 0;
            }

            try
            {
                this.ddlChartTypeEdit.SelectedValue = ((BSHomeSetting)obj).ChartType;
            }
            catch
            {
                this.ddlChartTypeEdit.SelectedIndex = 0;
            }
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new NumberCheck(this.lblReportSeqEdit, this.ddlReportSeqEdit, true));
            manager.Add(new LengthCheck(this.lblModuleEdit, this.ddlModuleEdit, 40, true));
            manager.Add(new LengthCheck(this.lblChartTypeEdit, this.ddlChartTypeEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                    ((BSHomeSetting)obj).ReportSeq.ToString(),
                    FormatHelper.GetModuleTitle(this.languageComponent1, ((BSHomeSetting)obj).ModuleCode),
                    this.languageComponent1.GetString(((BSHomeSetting)obj).ChartType)
  
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
                "ReportSeq",
                "Module",
                "ChartType"
            };
        }

        #endregion
    }
}
