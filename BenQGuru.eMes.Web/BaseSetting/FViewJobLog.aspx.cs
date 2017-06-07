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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class FViewJobLog : BaseMPageMinus
    {
        private System.ComponentModel.IContainer components;

        //private LanguageComponent languageComponent1;
        //private GridHelper gridHelper;
        private ButtonHelper _ButtonHelper;
        private ExcelExporter _ExcelExporter;

        private JobFacade _JobFacade;

        #region Form Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";

            this._ExcelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this._ExcelExporter.FileExtension = "xls";
            this._ExcelExporter.LanguageComponent = this.languageComponent1;
            this._ExcelExporter.Page = this;
            this._ExcelExporter.RowSplit = "\r\n";

            this._JobFacade = new JobFacade(this.DataProvider);

           // this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();

            if (!IsPostBack)
            {
                this.InitPageLanguage(this.languageComponent1, false);

                InitUI();
                InitButton();
                InitWebGrid();

                this.txtJobIDQuery.Text = this.GetRequestParam("jobid");
                this.datDateQuery.Text = DateTime.Now.ToString("yyyy-MM-dd");

                cmdQuery_ServerClick(null, null);
            }            
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.SetEditObject(obj);
                    this._ButtonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }

        protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        {
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this._ExcelExporter.Export();
        }

        private void ButtonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FOracleJobMP.aspx"));
        }

        #endregion

        #region LoadData

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        protected object[] LoadDataSource(int inclusive, int exclusive)
        {
            return _JobFacade.QueryJobLog(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtJobIDQuery.Text)),
                FormatHelper.TODateInt(datDateQuery.Text),
                inclusive, exclusive);
        }

        protected int GetRowCount()
        {
            return this._JobFacade.QueryJobLogCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtJobIDQuery.Text)),
                FormatHelper.TODateInt(datDateQuery.Text));
        }

        #endregion

        #region Init Functions

        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this._ButtonHelper = new ButtonHelper(this);
            this._ButtonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this._ButtonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.ButtonHelper_AfterPageStatusChangeHandle);

            this._ExcelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this._ExcelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this._ExcelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void InitButton()
        {
            this._ButtonHelper.AddDeleteConfirm();
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        protected void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("JobID", "JobID", null);
            this.gridHelper.AddColumn("StartDateTime", "开始时间", null);
            this.gridHelper.AddColumn("EndDateTime", "结束时间", null);
            this.gridHelper.AddColumn("UsedTime", "所用时间", null);
            this.gridHelper.AddColumn("ProcessCount", "处理资料笔数", null);
            this.gridHelper.AddColumn("Result", "处理结果", null);
            this.gridHelper.AddColumn("ErrorMessage", "错误信息", null);
            this.gridHelper.AddColumn("Serial", "Serial", null);

            this.gridHelper.Grid.Columns.FromKey("Serial").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, false);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        #endregion

        #region Get/Set Edit Object

        private DataRow GetGridRow(object obj)
        {
            JobLog jobLog = (JobLog)obj;

            //return new UltraGridRow(
            //    new object[]{
            //        "false",
            //        jobLog.JobID,
            //        FormatHelper.TODateTimeString(FormatHelper.TODateInt(jobLog.StartDateTime), FormatHelper.TOTimeInt(jobLog.StartDateTime)),
            //        FormatHelper.TODateTimeString(FormatHelper.TODateInt(jobLog.EndDateTime), FormatHelper.TOTimeInt(jobLog.EndDateTime)),
            //        jobLog.UsedTime.ToString(),
            //        jobLog.ProcessCount.ToString(),
            //        jobLog.Result,
            //        jobLog.ErrorMessage,
            //        jobLog.Serial
            //    });
            DataRow row = this.DtSource.NewRow();
            row["JobID"] = jobLog.JobID;
            row["StartDateTime"] = FormatHelper.TODateTimeString(FormatHelper.TODateInt(jobLog.StartDateTime), FormatHelper.TOTimeInt(jobLog.StartDateTime));
            row["EndDateTime"] = FormatHelper.TODateTimeString(FormatHelper.TODateInt(jobLog.EndDateTime), FormatHelper.TOTimeInt(jobLog.EndDateTime));
            row["UsedTime"] = jobLog.UsedTime.ToString();
            row["ProcessCount"] = jobLog.ProcessCount.ToString();
            row["Result"] = jobLog.Result;
            row["ErrorMessage"] = jobLog.ErrorMessage;
            row["Serial"] = jobLog.Serial;
            return row;

        }

        private object GetEditObject()
        {
            return null;
        }

        private object GetEditObject(GridRecord row)
        {
            return null;
        }

        private void SetEditObject(object obj)
        {
        }

        #endregion

        #region Export

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "JobID",
                "StartDateTime",
                "EndDateTime",
                "UsedTime",
                "ProcessCount",
                "Result",
                "ErrorMessage"
            };
        }

        private string[] FormatExportRecord(object obj)
        {
            JobLog jobLog = (JobLog)obj;

            return new string[]{
                jobLog.JobID,
                FormatHelper.TODateTimeString(FormatHelper.TODateInt(jobLog.StartDateTime), FormatHelper.TOTimeInt(jobLog.StartDateTime)),
                FormatHelper.TODateTimeString(FormatHelper.TODateInt(jobLog.EndDateTime), FormatHelper.TOTimeInt(jobLog.EndDateTime)),
                jobLog.UsedTime.ToString(),
                jobLog.ProcessCount.ToString(),
                jobLog.Result,
                jobLog.ErrorMessage
            };
        }

        #endregion
    }
}
