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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class FOracleJobMP : BaseMPageMinus
    {
        private System.ComponentModel.IContainer components;

        //private LanguageComponent languageComponent1;
        //private GridHelper gridHelper = null;
        private ButtonHelper _ButtonHelper = null;
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

           // this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
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
                InitPageLanguage(this.languageComponent1, false);

                InitUI();
                InitButton();
                InitWebGrid();
                InitDropDownList();

                cmdQuery_ServerClick(null, null);
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            UserSchedulerJob job = (UserSchedulerJob)GetEditObject();
            if (job != null)
            {
                this._JobFacade.AddUserSchedulerJob(job);
                this.RequestData();
                this._ButtonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            try
            {
                if (commandName == "Enable")
                {

                    UserSchedulerJob job = (UserSchedulerJob)this.GetEditObject(row);

                    if (job != null)
                    {
                        this._JobFacade.EnableUserSchedulerJob(job.JobName);
                        cmdQuery_ServerClick(null, null);
                    }

                }
                else if (commandName == "Disable")
                {
                    UserSchedulerJob job = (UserSchedulerJob)this.GetEditObject(row);

                    if (job != null)
                    {
                        this._JobFacade.DisableUserSchedulerJob(job.JobName);
                        cmdQuery_ServerClick(null, null);
                    }
                }
                else if (commandName == "Stop")
                {
                    UserSchedulerJob job = (UserSchedulerJob)this.GetEditObject(row);

                    if (job != null)
                    {
                        this._JobFacade.StopUserSchedulerJob(job.JobName);
                        cmdQuery_ServerClick(null, null);
                    }
                }
                else if (commandName == "RunImmediately")
                {
                    UserSchedulerJob job = (UserSchedulerJob)this.GetEditObject(row);

                    if (job != null)
                    {
                        this._JobFacade.RunUserSchedulerJob(job.JobName);
                        cmdQuery_ServerClick(null, null);
                    }
                }
                else if (commandName == "Log")
                {
                    this.Response.Redirect(this.MakeRedirectUrl("./FViewJobLog.aspx?jobid=" + row.Items.FindItemByKey("JobName").Text), false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Raise(this.GetType(), ex.Message, ex.InnerException);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        //protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        //{
        //    if (this.chbSelectAll.Checked)
        //    {
        //        this.gridHelper.CheckAllRows(CheckStatus.Checked);
        //    }
        //    else
        //    {
        //        this.gridHelper.CheckAllRows(CheckStatus.Unchecked);
        //    }
        //}

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList rowArray = this.gridHelper.GetCheckedRows();
            if (rowArray != null && rowArray.Count > 0)
            {
                ArrayList jobArray = new ArrayList(rowArray.Count);

                foreach (GridRecord row in rowArray)
                {
                    UserSchedulerJob job = (UserSchedulerJob)GetEditObject(row);

                    if (job != null)
                    {
                        jobArray.Add(job);
                    }
                }

                this._JobFacade.DeleteUserSchedulerJob((UserSchedulerJob[])jobArray.ToArray(typeof(UserSchedulerJob)));
                this.RequestData();
                this._ButtonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this._ExcelExporter.Export();
        }

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
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

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._JobFacade.QueryUserSchedulerJob(inclusive, exclusive);
        }

        private int GetRowCount()
        {
            return this._JobFacade.QueryUserSchedulerJobCount();
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
            this._ButtonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this._ExcelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this._ExcelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this._ExcelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void InitButton()
        {
            this._ButtonHelper.AddDeleteConfirm();
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        protected override  void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("JobName", "JobName", null);
            this.gridHelper.AddColumn("JobType", "JobType", null);
            this.gridHelper.AddColumn("JobAction", "JobAction", null);
            this.gridHelper.AddColumn("StartDateTime", "StartDateTime", null);
            this.gridHelper.AddColumn("RepeatInterval", "RepeatInterval", null);
            this.gridHelper.AddColumn("Enabled", "Enabled", null);
            this.gridHelper.AddColumn("State", "State", null);
            this.gridHelper.AddColumn("LastStartDateTime", "LastStartDateTime", null);
            this.gridHelper.AddColumn("LastRunDuration", "LastRunDuration", null);
            this.gridHelper.AddColumn("NextRunDateTime", "NextRunDateTime", null);
            this.gridHelper.AddColumn("Comments", "Comments", null);
            this.gridHelper.AddLinkColumn("Enable", "Enable", null);
            this.gridHelper.AddLinkColumn("Disable", "Disable", null);
            this.gridHelper.AddLinkColumn("RunImmediately", "RunImmediately", null);
            this.gridHelper.AddLinkColumn("Stop", "Stop", null);
            this.gridHelper.AddLinkColumn("Log", "Log", null);

            this.gridHelper.AddDefaultColumn(true, false);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void InitDropDownList()
        {
            ddlJobTypeEdit.Items.Add(new ListItem("STORED_PROCEDURE", "STORED_PROCEDURE"));
            ddlJobTypeEdit.Items.Add(new ListItem("PLSQL_BLOCK", "PLSQL_BLOCK"));
            ddlJobTypeEdit.SelectedIndex = 0;
        }

        #endregion

        #region Get/Set Edit Object

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblJobNameEdit, txtJobNameEdit, 30, true));
            manager.Add(new LengthCheck(lblJobTypeEdit, ddlJobTypeEdit, 16, true));
            manager.Add(new LengthCheck(lblJobActionEdit, txtJobActionEdit, 4000, true));
            manager.Add(new LengthCheck(lblRepeatIntervalEdit, txtRepeatIntervalEdit, 4000, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        protected DataRow GetGridRow(object obj)
        {
            UserSchedulerJob job = (UserSchedulerJob)obj;

            //return new UltraGridRow(
            //    new object[]{
            //        "false",
            //        job.JobName,
            //        job.JobType,
            //        job.JobAction,
            //        FormatHelper.TODateTimeString(FormatHelper.TODateInt(job.StartDate),FormatHelper.TOTimeInt(job.StartDate)),
            //        job.RepeatInterval,
            //        languageComponent1.GetString(string.Compare(job.Enabled, "True", true) == 0 ? "trueText" : "falseText"),
            //        job.State,
            //        FormatHelper.TODateTimeString(FormatHelper.TODateInt(job.LastStartDate),FormatHelper.TOTimeInt(job.LastStartDate)),
            //        job.LastRunDuration.ToString(),
            //        FormatHelper.TODateTimeString(FormatHelper.TODateInt(job.NextRunDate),FormatHelper.TOTimeInt(job.NextRunDate)),
            //        job.Comments,
            //        "",
            //        "",
            //        "",
            //        ""
            //    });
            DataRow row = this.DtSource.NewRow();
            row["JobName"] = job.JobName;
            row["JobType"] = job.JobType;
            row["JobAction"] = job.JobAction;
            row["StartDateTime"] = FormatHelper.TODateTimeString(FormatHelper.TODateInt(job.StartDate), FormatHelper.TOTimeInt(job.StartDate));
            row["RepeatInterval"] = job.RepeatInterval;
            row["Enabled"] = languageComponent1.GetString(string.Compare(job.Enabled, "True", true) == 0 ? "trueText" : "falseText");
            row["State"] = job.State;
            row["LastStartDateTime"] = FormatHelper.TODateTimeString(FormatHelper.TODateInt(job.LastStartDate), FormatHelper.TOTimeInt(job.LastStartDate));
            row["LastRunDuration"] =job.LastRunDuration.ToString() ;
            row["NextRunDateTime"] = FormatHelper.TODateTimeString(FormatHelper.TODateInt(job.NextRunDate), FormatHelper.TOTimeInt(job.NextRunDate));
            row["Comments"] = job.Comments;
            return row;

        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                UserSchedulerJob job = this._JobFacade.CreateNewUserSchedulerJob();

                job.JobName = this.txtJobNameEdit.Text;
                job.JobType = this.ddlJobTypeEdit.SelectedValue;
                job.JobAction = this.txtJobActionEdit.Text;
                job.RepeatInterval = this.txtRepeatIntervalEdit.Text;
                job.Comments = this.txtCommentsEdit.Text;

                return job;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {
            return this._JobFacade.GetUserSchedulerJob(row.Items.FindItemByKey("JobName").Text);
        }

        private void SetEditObject(object obj)
        {
            UserSchedulerJob job = (UserSchedulerJob)obj;

            if (job == null)
            {
                this.txtJobNameEdit.Text = string.Empty;
                this.ddlJobTypeEdit.SelectedIndex = 0;
                this.txtJobActionEdit.Text = string.Empty;
                this.txtRepeatIntervalEdit.Text = string.Empty;
                this.txtCommentsEdit.Text = string.Empty;
            }
            else
            {
                this.txtJobNameEdit.Text = job.JobName;

                try
                {
                    this.ddlJobTypeEdit.SelectedValue = job.JobType;
                }
                catch
                {
                    this.ddlJobTypeEdit.SelectedIndex = 0;
                }

                this.txtJobActionEdit.Text = job.JobAction;
                this.txtRepeatIntervalEdit.Text = job.RepeatInterval.ToString();
                this.txtCommentsEdit.Text = job.Comments;
            }
        }

        #endregion

        #region Export

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "JobName",
                "JobType",
                "JobAction",
                "StartDateTime",
                "RepeatInterval",
                "Enabled",
                "State",
                "LastStartDateTime",
                "LastRunDuration",
                "NextRunDateTime",
                "Comments"
            };
        }

        private string[] FormatExportRecord(object obj)
        {
            UserSchedulerJob job = (UserSchedulerJob)obj;

            return new string[]{
                job.JobName,
                job.JobType,
                job.JobAction,
                FormatHelper.TODateTimeString(FormatHelper.TODateInt(job.StartDate),FormatHelper.TOTimeInt(job.StartDate)),
                job.RepeatInterval,
                languageComponent1.GetString(string.Compare(job.Enabled, "True", true) == 0 ? "trueText" : "falseText"),
                job.State,
                FormatHelper.TODateTimeString(FormatHelper.TODateInt(job.LastStartDate),FormatHelper.TOTimeInt(job.LastStartDate)),
                job.LastRunDuration.ToString(),
                FormatHelper.TODateTimeString(FormatHelper.TODateInt(job.NextRunDate),FormatHelper.TOTimeInt(job.NextRunDate)),
                job.Comments
            };
        }

        #endregion
    }
}
