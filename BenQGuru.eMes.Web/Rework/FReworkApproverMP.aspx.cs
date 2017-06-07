#region system;
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
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.Rework
{
    /// <summary>
    /// FReworkApproverMP 的摘要说明。
    /// </summary>
    public partial class FReworkApproverMP : BaseMPageMinus
    {
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        //private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
        private BenQGuru.eMES.Web.Helper.ButtonHelper buttonHelper = null;

        private BenQGuru.eMES.Rework.ReworkFacade _facade;//= ReworkFacadeFactory.Create();
        private BenQGuru.eMES.BaseSetting.UserFacade _userManager;//= new ReworkFacadeFactory(base.DataProvider).CreateUserManager();

        private string reworkStatus;


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
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            // 
            // languageComponent1
            // 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {

            try
            {
                if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
                object reworkSheet = this._facade.GetReworkSheet(this.GetRequestParam("reworkcode"));
                if (reworkSheet != null)
                {
                    this.reworkStatus = ((ReworkSheet)reworkSheet).Status;
                }
            }
            catch
            {
                ExceptionManager.Raise(this.GetType(), "$Error_RequestUrlParameter_Lost");
            }


            this.InitOnPostBack();

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.InitButton();
                this.InitWebGrid();
                InitParamterts();
                InitViewPanel();

                if (this.GetRequestParam("hiddenEdit") != string.Empty)
                {
                    HiddenEdit();
                }

            }

        }

        private void HiddenEdit()
        {
            this.tdEdit.Visible = false;
            this.tdDelete.Visible = false;
            this.cmdAdd.Visible = false;
            this.cmdSave.Visible = false;
            this.cmdCancel.Visible = false;
            //this.chbSelectAll.Visible = false ;
            this.gridHelper.Grid.Columns.FromKey(this.gridHelper.EditColumnKey).Hidden = true;

            this.gridHelper.Grid.Columns.FromKey(this.gridHelper.CheckColumnKey).Hidden = true;
        }
        private void InitParamterts()
        {
            if (this.Request.Params["reworkcode"] == null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_RequestUrlParameter_Lost");
                //                throw new Exception(ErrorCenter.GetErrorUserDescription( this.GetType().BaseType, string.Format(ErrorCenter.ERROR_WITHOUTINPUT, "ReworkCode") ) ); 
            }
            this.ReworkCode = Request.Params["reworkcode"];

        }
        private void InitViewPanel()
        {
            this.txtReworkCodeQuery.Text = ReworkCode;
            this.txtReworkCodeQuery.ReadOnly = true;
            this.RequestData();
        }

        public string ReworkCode
        {
            get
            {
                return (string)this.ViewState["reworkcode"];
            }
            set
            {
                this.ViewState["reworkcode"] = value;
            }
        }

        private int ReworkSeq
        {
            get
            {
                try
                {
                    return int.Parse(ViewState["reworkSeq"].ToString());
                }
                catch
                {
                    return 0;
                }
            }

            set
            {
                ViewState["reworkSeq"] = value;
            }

        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        private void InitButton()
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            this.buttonHelper.AddDeleteConfirm();
        }
        protected void drpApproverEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_userManager == null) { _userManager = new ReworkFacadeFactory(base.DataProvider).CreateUserManager(); }
                DropDownListBuilder builder = new DropDownListBuilder(this.drpApproverEdit);
                builder.HandleGetObjectList = new GetObjectListDelegate(this._userManager.GetAllUser);
                builder.Build("UserCode", "UserCode");
                ListItem item = new ListItem("", string.Empty);
                this.drpApproverEdit.Items.Insert(0, item);
            }
        }
        protected void drpApproveStatusQuery_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpApproveStatusQuery.Items.Clear();

                this.drpApproveStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetLanguage("ApproveStatus_Waiting").ControlText, ApproveStatus.APPROVESTATUS_WAITING.ToString())
                    );

                this.drpApproveStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetLanguage("ApproveStatus_NoPassed").ControlText, ApproveStatus.APPROVESTATUS_NOPASSED.ToString())
                    );

                this.drpApproveStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetLanguage("ApproveStatus_Passed").ControlText, ApproveStatus.APPROVESTATUS_PASSED.ToString())
                    );

                new DropDownListBuilder(drpApproveStatusQuery).AddAllItem(this.languageComponent1);

            }
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ReworkPassSequence", "签核层级", null);
            this.gridHelper.AddColumn("PassContent", "签核意见", null);
            this.gridHelper.AddColumn("ReworkStatus", "签核状态", null);
            this.gridHelper.AddColumn("ReworkUserCode", "签核人", null);
            this.gridHelper.AddColumn("ReworkDepartment", "签核部门", null);
            this.gridHelper.AddColumn("MaintainDate", "日期", null);
            this.gridHelper.AddColumn("MaintainTime", "时间", null);
            this.gridHelper.AddColumn("ReworkCode", "需求单代码", null);
            this.gridHelper.AddColumn("ReworkSequence", "签核顺序号", null);

            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.Grid.Columns.FromKey("ReworkCode").Hidden = true;
            ((BoundDataField)this.gridHelper.Grid.Columns.FromKey("ReworkCode")).HtmlEncode = true;
            this.gridHelper.Grid.Columns.FromKey("ReworkSequence").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
        }

        protected DataRow GetGridRow(object obj)
        {
            // TODO: 调整字段值的顺序，使之与Grid的列对应
            DataRow row = this.DtSource.NewRow();
            row["ReworkPassSequence"] = ((ReworkPassEx)obj).PassSequence.ToString();
            row["PassContent"] = ((ReworkPassEx)obj).PassContent.ToString();
            row["ReworkStatus"] = this.GetPassStatusString(((ReworkPassEx)obj));
            row["ReworkUserCode"] = ((ReworkPassEx)obj).GetDisplayText("UserCode");
            row["ReworkDepartment"] = ((ReworkPassEx)obj).UserDepartment.ToString();
            row["MaintainDate"] = FormatHelper.ToDateString(((ReworkPassEx)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((ReworkPassEx)obj).MaintainTime);
            row["ReworkCode"] = ((ReworkPassEx)obj).ReworkCode.ToString();
            row["ReworkSequence"] = ((ReworkPassEx)obj).Sequence.ToString();
            return row;
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            // TODO: 使用主键之外的字段查询需更改后台Facade的Query方法

            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            // showNotApproved 定义了已签层级是否显示未签核的人
            // 对于已经不通过和未签过的需求单,要显示未签核的人
            // 其他都只显示已签核的人
            bool showNotApproved;
            if (this.reworkStatus == ReworkStatus.REWORKSTATUS_NEW)
            {
                showNotApproved = true;
            }
            else
            {
                showNotApproved = false;
            }

            return this._facade.QueryReworkApprover(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkCodeQuery.Text)),
                string.Empty,
                FormatHelper.CleanString(this.txtSequenceQuery.Text),
                FormatHelper.CleanString(this.drpApproveStatusQuery.SelectedValue),
                string.Empty,
                true,
                showNotApproved,
                inclusive, exclusive);
        }


        private int GetRowCount()
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            // TODO: 使用主键之外的字段查询需更改后台Facade的Query方法
            // showNotApproved 定义了已签层级是否显示未签核的人
            // 对于已经不通过和未签过的需求单,要显示未签核的人
            // 其他都只显示已签核的人
            bool showNotApproved;
            if (this.reworkStatus == ReworkStatus.REWORKSTATUS_NEW)
            {
                showNotApproved = true;
            }
            else
            {
                showNotApproved = false;
            }

            return this._facade.QueryReworkApproverCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkCodeQuery.Text)),
                string.Empty,
                FormatHelper.CleanString(this.txtSequenceQuery.Text),
                FormatHelper.CleanString(this.drpApproveStatusQuery.SelectedValue),
                string.Empty,
                true,
                showNotApproved);
        }

        #endregion

        #region Button
        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            if (!this.ValidateInput())
            {
                return;
            }
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            this._facade.AddReworkPass((ReworkPass)this.GetEditObject());

            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            ArrayList reworkPasss = new ArrayList(array.Count);

            foreach (GridRecord row in array)
            {
                reworkPasss.Add((ReworkPass)this.GetEditObject(row));
            }

            this._facade.DeleteReworkPass((ReworkPass[])reworkPasss.ToArray(typeof(ReworkPass)));

            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (!this.ValidateInput())
            {
                return;
            }
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            this._facade.UpdateReworkPass((ReworkPass)this.GetEditObject());

            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }



        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            object obj = this.GetEditObject(row);

            if (commandName == "Edit")
            {
                if (obj != null)
                {
                    this.SetEditObject(obj);

                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }


        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            // TODO: 不可更改的字段置只读

            if (pageAction == PageActionType.Add)
            {
                this.drpApproverEdit.Enabled = true;
                this.txtPassSequenceEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.drpApproverEdit.Enabled = false;
            }
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            if (this.GetRequestParam("backto").ToString() == "ReworkApprove")
            {
                Response.Redirect(this.MakeRedirectUrl("FReworkApproveMP.aspx"));
            }
            else
            {
                Response.Redirect(this.MakeRedirectUrl("FReworkSheetMP.aspx"));
            }
        }
        #endregion

        #region Object <--> Page

        private object GetEditObject()
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            ReworkPass reworkPass = this._facade.CreateNewReworkPass();
            reworkPass.ReworkCode = ReworkCode;
            //sammer kong
            reworkPass.IsPass = IsPass.ISPASS_NOPASS;
            reworkPass.MaintainUser = this.GetUserCode();
            reworkPass.PassSequence = Int32.Parse(FormatHelper.CleanString(this.txtPassSequenceEdit.Text));
            //sammer kong
            reworkPass.Status = ApproveStatus.APPROVESTATUS_WAITING;
            reworkPass.UserCode = FormatHelper.CleanString(drpApproverEdit.SelectedValue);
            reworkPass.Sequence = this.ReworkSeq;
            return reworkPass;
        }


        private object GetEditObject(GridRecord row)
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            object obj = _facade.GetReworkPass(row.Items.FindItemByKey("ReworkCode").Value.ToString(), row.Items.FindItemByKey("ReworkSequence").Value.ToString());

            if (obj != null)
            {
                return (ReworkPass)obj;
            }

            return null;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.drpApproverEdit.SelectedIndex = 0;
                this.txtPassSequenceEdit.Text = string.Empty;
                return;
            }
            try
            {
                this.drpApproverEdit.SelectedValue = ((ReworkPass)obj).UserCode;
            }
            catch
            {
                this.drpApproverEdit.SelectedIndex = 0;
            }
            this.txtPassSequenceEdit.Text = ((ReworkPass)obj).PassSequence.ToString();

            this.ReworkSeq = (int)(((ReworkPass)obj).Sequence);
        }


        private bool ValidateInput()
        {


            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblApproverEdit, drpApproverEdit, 40, true));
            manager.Add(new NumberCheck(lblPassSequenceEdit, txtPassSequenceEdit, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;


        }

        #endregion

        #region Export
        // 2005-04-06
        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                   ((ReworkPassEx)obj).PassSequence.ToString(),
                                   ((ReworkPassEx)obj).PassContent.ToString(),
                                   GetPassStatusString((ReworkPassEx)obj),
                                   //((ReworkPassEx)obj).UserCode.ToString(),
                                ((ReworkPassEx)obj).GetDisplayText("UserCode"),
                                   ((ReworkPassEx)obj).UserDepartment.ToString(),
                                   FormatHelper.ToDateString(((ReworkPassEx)obj).MaintainDate)
                               };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	"ReworkPassSequence",
                                    "PassContent",
                                    "ReworkStatus",
                                    "ReworkUserCode",
                                    "ReworkDepartment",
                                    "MaintainDate"};
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }
        #endregion

        private string GetPassStatusString(ReworkPassEx obj)
        {
            if (obj.ReworkStatus == ReworkStatus.REWORKSTATUS_NEW)
            {
                return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_WAITING_STRING);
            }

            if (obj.Status == ApproveStatus.APPROVESTATUS_PASSED)
            {
                return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_PASSED_STRING);
            }
            else if (obj.Status == ApproveStatus.APPROVESTATUS_NOPASSED)
            {
                return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_NOPASSED_STRING);
            }
            return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_WAITING_STRING);
        }

    }
}
