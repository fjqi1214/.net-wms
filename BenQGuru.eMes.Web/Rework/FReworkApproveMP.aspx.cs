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
    /// FReworkApproveMP 的摘要说明。
    /// </summary>
    public partial class FReworkApproveMP : BaseMPageMinus
    {

        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        //private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
        private BenQGuru.eMES.Web.Helper.ButtonHelper buttonHelper = null;

        private BenQGuru.eMES.Rework.ReworkFacade _facade;//= ReworkFacadeFactory.Create();
        //private BenQGuru.eMES.BaseSetting.UserFacade _userManager;// = ReworkFacadeFactory.CreateUserManager();



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
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {

            this.InitOnPostBack();

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.InitButton();
                this.InitWebGrid();

                this.txtReworkApproverQuery.Text = this.GetUserCode();
            }
        }
        private void InitViewPanel()
        {
            this.txtReworkApproverQuery.Text = GetUserCode();

            // ToDo: 这里应该是设成Readonly的,但是为了测试方便,没有这么做.
            // ToDo: 这里可以直接改用户名称,就不需要不停地登录了
            //this.txtReworkApproverQuery.ReadOnly = true;
        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

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

        protected void drpApproveStatusQuery_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {

                this.drpApproveStatusQuery.Items.Clear();

                this.drpApproveStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_WAITING_STRING), ApproveStatus.APPROVESTATUS_WAITING.ToString())
                    );

                this.drpApproveStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_WAITING_OTHERS_STRING), ApproveStatus.APPROVESTATUS_WAITING_OTHERS.ToString())
                    );

                this.drpApproveStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_PASSED_STRING), ApproveStatus.APPROVESTATUS_PASSED.ToString())
                    );

                this.drpApproveStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_NOPASSED_STRING), ApproveStatus.APPROVESTATUS_NOPASSED.ToString())
                    );

                new DropDownListBuilder(drpApproveStatusQuery).AddAllItem(this.languageComponent1);
            }
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ReworkSequence", "序号", null);
            this.gridHelper.AddColumn("ReworkUserCode", "用户代码", null);
            this.gridHelper.AddColumn("ReworkCode", "返工需求单代码", null);
            this.gridHelper.AddLinkColumn("ReworkRangeMNID", "返工范围", null);
            this.gridHelper.AddColumn("ReworkPassSequence", "签核层级", null);
            this.gridHelper.AddColumn("ReworkIsPass", "是否签核完成", null);
            this.gridHelper.AddLinkColumn("ReworkPassContent", "签核意见", null);
            this.gridHelper.AddColumn("ReworkStatus", "签核状态", null);
            this.gridHelper.AddColumn("ReworkPassUser", "签核人员", null);
            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.Grid.Columns.FromKey("ReworkUserCode").Hidden = true;
            this.gridHelper.Grid.Columns.FromKey("ReworkIsPass").Hidden = true;
            this.gridHelper.Grid.Columns.FromKey("ReworkSequence").Hidden = true;
            ((BoundDataField)this.gridHelper.Grid.Columns.FromKey("ReworkCode")).HtmlEncode = false;
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["ReworkSequence"] = ((ReworkPassEx)obj).Sequence.ToString();
            row["ReworkUserCode"] = ((ReworkPassEx)obj).UserCode.ToString();
            row["ReworkCode"] = GetNoLink(((ReworkPassEx)obj).ReworkCode.ToString());
            row["ReworkRangeMNID"] = "";
            row["ReworkPassSequence"] = ((ReworkPassEx)obj).PassSequence.ToString();
            row["ReworkIsPass"] = FormatHelper.DisplayBoolean(((ReworkPassEx)obj).IsPass.ToString(), this.languageComponent1);
            row["ReworkPassContent"] = "";
            row["ReworkStatus"] = this.GetPassStatusString(((ReworkPassEx)obj));
            row["ReworkPassUser"] = ((ReworkPassEx)obj).GetDisplayText("PassUser");
            return row;
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            SetEditObject(null);
            return this._facade.QueryReworkApprove(
                string.Empty,
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkApproverQuery.Text)),
                string.Empty,
                FormatHelper.CleanString(this.drpApproveStatusQuery.SelectedValue),
                string.Empty,
                false,
                true,
                inclusive, exclusive);
        }

        private int GetRowCount()
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            return this._facade.QueryReworkApproveCount(
                string.Empty,
                FormatHelper.CleanString(this.txtReworkApproverQuery.Text),
                string.Empty,
                FormatHelper.CleanString(this.drpApproveStatusQuery.SelectedValue),
                string.Empty,
                false,
                true
                );
        }

        #endregion

        #region Button
        protected void cmdPass_ServerClick(object sender, System.EventArgs e)
        {
            if (ValidateInput())
            {
                if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
                ArrayList array = this.gridHelper.GetCheckedRows();
                ArrayList reworkPassList = new ArrayList(array.Count);

                ReworkPass reworkPass = null;
                foreach (GridRecord row in array)
                {
                    reworkPass = (ReworkPass)this.GetEditObject(row);
                    reworkPass.PassContent = FormatHelper.CleanString(this.txtApproveContentEdit.Text, 100);
                    //added by jessie lee for CS0092,2005/10/13,P4.10
                    reworkPass.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    reworkPass.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    //end
                    reworkPassList.Add(reworkPass);
                }

                this._facade.PassReworkApprove((ReworkPass[])reworkPassList.ToArray(typeof(ReworkPass)));

                this.RequestData();

                /* 签核通过后给下一位签核人发送邮件通知 */
                //new MailHelper(base.DataProvider,this.GetUserMail()).SendMailToNext( (ReworkPass[])reworkPassList.ToArray( typeof(ReworkPass) ) );
            }
        }

        protected void cmdNoPass_ServerClick(object sender, System.EventArgs e)
        {
            if (ValidateInput())
            {
                if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
                ArrayList array = this.gridHelper.GetCheckedRows();
                ArrayList reworkPassList = new ArrayList(array.Count);

                ReworkPass reworkPass = null;
                foreach (GridRecord row in array)
                {
                    reworkPass = (ReworkPass)this.GetEditObject(row);
                    reworkPass.PassContent = FormatHelper.CleanString(this.txtApproveContentEdit.Text, 100);
                    //added by jessie lee for CS0092,2005/10/13,P4.10
                    reworkPass.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    reworkPass.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    //end
                    reworkPassList.Add(reworkPass);
                }

                this._facade.NOPassReworkApprove((ReworkPass[])reworkPassList.ToArray(typeof(ReworkPass)));

                this.RequestData();
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (this.ValidateInput())
            {
                if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
                this._facade.UpdateReworkPass((ReworkPass)this.GetEditObject(), false);

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
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

            
            if (obj != null)
            {
                if (commandName == "ReworkPassContent")
                {
                    Response.Redirect(this.MakeRedirectUrl("FReworkApproveResultMP.aspx", new string[] { "ReworkCode" }, new string[] { GetText(row.Items.FindItemByKey("ReworkCode").Text) }));
                }

                else if (commandName == "ReworkRangeMNID")
                {
                    if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
                    object reworksheet = _facade.GetReworkSheet(GetText(row.Items.FindItemByKey("ReworkCode").Text));
                    if (reworksheet != null)
                    {
                        if (((ReworkSheet)reworksheet).ReworkType == BenQGuru.eMES.Web.Helper.ReworkType.REWORKTYPE_REMO)
                        {
                            ExceptionManager.Raise(this.GetType(), "$Error_Rework_NORange_For_REMO_TYPE");
                        }

                        Response.Redirect(this.MakeRedirectUrl("FReworkRangeSP_New.aspx", new string[] { "ReworkSheetCode" }, new string[] { GetText(row.Items.FindItemByKey("ReworkCode").Text) }));
                    }
                }

                else if (commandName == "Edit")
                {
                    this.SetEditObject(obj);
                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }


        #endregion

        #region Object <--> Page
        private object GetEditObject(GridRecord row)
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            object obj = _facade.GetReworkPass(GetText(row.Items.FindItemByKey("ReworkCode").Value.ToString()), row.Items.FindItemByKey("ReworkSequence").Value.ToString());

            if (obj != null)
            {
                ((ReworkPass)obj).PassUser = GetUserCode();
                //((ReworkPass)obj).PassContent = FormatHelper.CleanString(this.txtApproveContentEdit.Text);
                return (ReworkPass)obj;
            }

            return null;
        }

        private object GetEditObject()
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            ReworkPass reworkPass = (ReworkPass)this._facade.GetReworkPass(ReworkCode, ReworkSequence.ToString());
            reworkPass.MaintainUser = this.GetUserCode();
            reworkPass.PassContent = FormatHelper.CleanString(this.txtApproveContentEdit.Text);
            return reworkPass;
        }

        private void SetEditObject(object obj)
        {

            if (obj == null)
            {
                this.txtApproveContentEdit.Text = string.Empty;
                return;
            }
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            object rs = this._facade.GetReworkSheet(((ReworkPass)obj).ReworkCode);
            if (rs == null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_ReworkSheet_Lost");
            }

            if (((ReworkSheet)rs).Status != ReworkStatus.REWORKSTATUS_WAITING)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_ReworkPass_Status_NotWaiting", string.Format("[$StatusShouldBe {0}]", "$" + ReworkStatus.REWORKSTATUS_WAITING));
            }

            if (((ReworkPass)obj).IsPass == IsPass.ISPASS_NOTACTION)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Rework_PassContent");
            }

            this.ReworkCode = ((ReworkPass)obj).ReworkCode;
            this.ReworkSequence = (int)(((ReworkPass)obj).Sequence);
            this.txtApproveContentEdit.Text = ((ReworkPass)obj).PassContent;
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblApproveContentEdit, txtApproveContentEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }



        #endregion

        #region attribute
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

        public int ReworkSequence
        {
            get
            {
                return Int32.Parse(this.ViewState["reworksequence"].ToString());
            }
            set
            {
                this.ViewState["reworksequence"] = value;
            }

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
                                   ((ReworkPassEx)obj).UserCode.ToString(),
                                   ((ReworkPassEx)obj).ReworkCode.ToString() ,
                                   ((ReworkPassEx)obj).PassSequence.ToString(),
                                   GetPassStatusString((ReworkPassEx)obj),
                                   //((ReworkPassEx)obj).PassUser.ToString()
                             ((ReworkPassEx)obj).GetDisplayText("PassUser")
                               }
                ;
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                                    "ReworkUserCode",
                                    "ReworkCode",
                                    "ReworkPassSequence",
                                    "ReworkStatus",
                                    "ReworkPassUser" };
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }
        #endregion

        private string GetPassStatusString(ReworkPassEx obj)
        {
            // 如果不通过,显示出来的始终是等待
            if (obj.ReworkStatus == ReworkStatus.REWORKSTATUS_NEW)
            {
                if (obj.Status == ApproveStatus.APPROVESTATUS_NOPASSED)
                {
                    return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_NOPASSED_STRING);
                }

                if (obj.Status == ApproveStatus.APPROVESTATUS_PASSED)
                {
                    return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_PASSED_STRING);
                }

                if (obj.Status == ApproveStatus.APPROVESTATUS_WAITING)
                {
                    return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_WAITING_STRING);
                }

                return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_WAITING_STRING);

            }
            else
            {
                // 共有下面几种可能
                // ISPASS           是否签核到当前层级      Status      应该显示
                // 0(未通过)        0(不是)                 0(等待)     0(等待其他人)
                // 0(未通过)        1(是)                   0(等待)     3(等待其他人)
                // 0(未通过)        1(是)                   1(通过)     3(等待其他人)
                // 1(已通过)        *                       1(通过)     1(已通过)
                // 0(已通过)        *                       2(不通过)   2(不通过)

                // 通过
                if (obj.ReworkStatus != ReworkStatus.REWORKSTATUS_WAITING)
                {
                    return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_PASSED_STRING);
                }


                // 等待其他人
                if (obj.CurrentPassSeq == obj.PassSequence && obj.ReworkStatus == ReworkStatus.REWORKSTATUS_WAITING)
                {
                    return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_WAITING_STRING);
                }

                return this.languageComponent1.GetString(ApproveStatus.APPROVESTATUS_WAITING_OTHERS_STRING);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }


        private string GetNoLink(string no)
        {
            return string.Format("<a href=FReworkSheetMP.aspx?approvesheetno={0}>{1}</a>", no, no);
        }

        private string GetText(string html)
        {
            int s = html.IndexOf(">", 0);
            int e = html.IndexOf("</a>", 0);

            string str = html.Substring(s + 1, e - s - 1);
            return str;
        }

    }
}
