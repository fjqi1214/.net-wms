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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Common;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.Rework
{
    /// <summary>
    /// FReworkSheetMP 的摘要说明。
    /// </summary>
    public partial class FReworkSheetMP : BaseMPageMinus
    {

        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;


        protected System.Web.UI.WebControls.Label lblReworkSheetTitle;
        //private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
        private BenQGuru.eMES.Web.Helper.ButtonHelper buttonHelper = null;

        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.TextBox Textbox2;
        protected System.Web.UI.WebControls.TextBox Textbox3;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private BenQGuru.eMES.Rework.ReworkFacade _facade;//= ReworkFacadeFactory.Create();


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
            this.excelExporter.DownloadRelativePath = "\\upload_tmp\\";
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";

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

                this.buttonHelper.AddDeleteConfirm();
                this.InitWebGrid();
                this.InitDropDowns();
                this.drpReworkType_Load();

                this.txtMaintainBeginDate.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
                this.txtMaintainEndDate.Text = this.txtMaintainBeginDate.Text;


                if (Page.Request["approvesheetno"] != null && Page.Request["approvesheetno"] != string.Empty)
                {
                    this.txtReworkSheetCodeQuery.Text = Page.Request["approvesheetno"];
                    this.RequestData();
                }
            }
        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);

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
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            // TODO: 调整列的顺序及标题

            this.gridHelper.AddColumn("ReworkSheetCode", "返工需求单编号", null);
            this.gridHelper.AddColumn("ItemCode", "产品", null);
            this.gridHelper.AddLinkColumn("ReworkRangeMNID", "返工范围", null);
            //			this.gridHelper.AddLinkColumn( "ReworkCause", "返工原因",	null);
            this.gridHelper.AddLinkColumn("Approve", "签核流程", null);
            this.gridHelper.AddColumn("ReworkQty", "计划返工数量", null);
            this.gridHelper.AddColumn("RealQty", "实际返工数量", null);
            this.gridHelper.AddLinkColumn("SelectRoute", "返工途程", null);
            this.gridHelper.AddColumn("ReworkSheetStatus", "需求单状态", null);
            this.gridHelper.AddColumn("ReworkType", "返工类型", null);

            this.gridHelper.AddColumn("DutyCode", "责任别", null);
            this.gridHelper.AddColumn("MaterialModelCode", "机型", null);
            this.gridHelper.AddColumn("BigLine", "大线", null);
            this.gridHelper.AddColumn("TOReworkDate", "判退时间", null);

            this.gridHelper.AddColumn("ReworkReason", "情况说明", null);
            this.gridHelper.AddColumn("ReasonAnalyse", "不良原因分析", null);
            this.gridHelper.AddColumn("Soluation", "纠正措施", null);

            // Added By HI1/Venus.Feng on 20080727 for Hisense Version 
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            // End Added

            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ReworkSheetCode"] = ((ReworkSheetQuery)obj).ReworkCode.ToString();
            row["ItemCode"] = ((ReworkSheetQuery)obj).ReworkCode.ToString();
            row["ReworkRangeMNID"] = "";
            row["Approve"] = "";
            row["ReworkQty"] =((ReworkSheetQuery)obj).ReworkQty.ToString();
            row["RealQty"] = ((ReworkSheetQuery)obj).ReworkRealQty.ToString();
            row["SelectRoute"] = "";
            row["ReworkSheetStatus"] = this.languageComponent1.GetString(((ReworkSheetQuery)obj).Status);
            row["ReworkType"] = this.languageComponent1.GetString(((ReworkSheetQuery)obj).ReworkType);
            row["DutyCode"] = ((ReworkSheetQuery)obj).DutyCode;
            row["TOReworkDate"] = ((ReworkSheetQuery)obj).MaterialModelCode;
            row["ReworkReason"] = ((ReworkSheetQuery)obj).BigStepSequenceCode;
            row["ReasonAnalyse"] = FormatHelper.ToDateString(((ReworkSheetQuery)obj).DDATE);
            row["Soluation"] = ((ReworkSheetQuery)obj).ReworkReason.ToString();
            row["MaintainUser"] = ((ReworkSheetQuery)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((ReworkSheetQuery)obj).MaintainDate);
            return row;
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {

            // TODO: 使用主键之外的字段查询需更改后台Facade的Query方法
            if (_facade == null)
            {
                _facade = new ReworkFacadeFactory(base.DataProvider).Create();
            }

            int beginDate = FormatHelper.TODateInt(FormatHelper.CleanString(this.txtMaintainBeginDate.Text));
            int endDate = FormatHelper.TODateInt(FormatHelper.CleanString(this.txtMaintainEndDate.Text));
            int reworkDate = FormatHelper.TODateInt(FormatHelper.CleanString(this.txtReworkDateEdit.Text));

            return this._facade.QueryReworkSheet(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkSheetCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpModelQuery.SelectedValue)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
                FormatHelper.CleanString(this.drpReworkSheetStatusQuery.SelectedValue),
                beginDate, endDate, reworkDate,
                FormatHelper.CleanString(this.drpReworkType.SelectedValue.Trim()),
                FormatHelper.CleanString(this.txtBigSSCodeWhere.Text.Trim()),
                FormatHelper.CleanString(this.txtMaterialModelCodeWhere.Text.Trim()),
                FormatHelper.CleanString(this.txtDutyCodeQuery.Text.Trim()),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLotNO.Text)),
                inclusive, exclusive);
        }

        protected void drpModelQuery_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.drpModelQuery.Items.Clear();
                this.drpModelQuery.Items.Add(string.Empty);

                object[] models = new ReworkFacadeFactory(base.DataProvider).CreateModelFacade().GetAllModels();
                if (models != null)
                {
                    foreach (BenQGuru.eMES.Domain.MOModel.Model model in models)
                    {
                        this.drpModelQuery.Items.Add(model.ModelCode);
                    }
                }
            }

        }



        private int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new ReworkFacadeFactory(base.DataProvider).Create();
            }

            int beginDate = FormatHelper.TODateInt(FormatHelper.CleanString(this.txtMaintainBeginDate.Text));
            int endDate = FormatHelper.TODateInt(FormatHelper.CleanString(this.txtMaintainEndDate.Text));
            int reworkDate = FormatHelper.TODateInt(FormatHelper.CleanString(this.txtReworkDateEdit.Text));

            // TODO: 使用主键之外的字段查询需更改后台Facade的Query方法
            return this._facade.QueryReworkSheetCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkSheetCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpModelQuery.SelectedValue)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
                this.drpReworkSheetStatusQuery.SelectedValue,
                beginDate,
                endDate,
                reworkDate,
                FormatHelper.CleanString(this.drpReworkType.SelectedValue.Trim()),
                FormatHelper.CleanString(this.txtBigSSCodeWhere.Text.Trim()),
                FormatHelper.CleanString(this.txtMaterialModelCodeWhere.Text.Trim()),
                FormatHelper.CleanString(this.txtDutyCodeQuery.Text.Trim()),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLotNO.Text))
                );
        }

        #endregion

        #region Button

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FReworkSheetEP.aspx"));
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            ArrayList reworkSheets = new ArrayList(array.Count);

            foreach (GridRecord row in array)
            {
                ReworkSheet rSheet = (ReworkSheet)this.GetEditObject(row);
                if (rSheet.Status != ReworkStatus.REWORKSTATUS_NEW &&
                    !(rSheet.Status == ReworkStatus.REWORKSTATUS_RELEASE && rSheet.NeedCheck == "N"))
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Delete_ReworkSheet_NotNew");
                    return;
                }
                reworkSheets.Add(rSheet);
            }

            this._facade.DeleteReworkSheet((ReworkSheet[])reworkSheets.ToArray(typeof(ReworkSheet)));

            this.RequestData();
        }


        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.setReworkSheetStatus(BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_OPEN);
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
        }

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        protected override void Grid_ClickCell( GridRecord row, string commandName)
        {
            object obj = this.GetEditObject(row);

            if (commandName == "Edit")
            {
                if (obj != null)
                {
                    Response.Redirect(this.MakeRedirectUrl("FReworkSheetEP.aspx", new string[] { "ReworkSheetCode" }, new string[] { row.Items.FindItemByKey("ReworkSheetCode").Text }));
                }
            }
            else if (commandName =="Approve")
            {
                // 双击的是签核单元格
                if (obj != null)
                {
                    if (((ReworkSheet)obj).NeedCheck == "Y")
                    {
                        if (((ReworkSheet)obj).Status == BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_NEW)
                        {
                            Response.Redirect(this.MakeRedirectUrl("FReworkApproverMP.aspx", new string[] { "ReworkCode" }, new string[] { row.Items.FindItemByKey("ReworkSheetCode").Text }));
                        }
                        else
                        {
                            Response.Redirect(this.MakeRedirectUrl("FReworkApproverMP.aspx", new string[] { "hiddenEdit", "ReworkCode" }, new string[] { "1", row.Items.FindItemByKey("ReworkSheetCode").Text }));
                        }
                    }
                    else
                    {
                        ExceptionManager.Raise(this.GetType(), "$Error_DotNotNeedCheckFlow");
                    }
                }
            }

            else if (commandName =="ReworkCause")
            {
                // 双击的是返工原因
                if (obj != null)
                {
                    Response.Redirect(this.MakeRedirectUrl("FReworkSheet2CauseSP.aspx", new string[] { "ReworkSheetCode" }, new string[] { row.Items.FindItemByKey("ReworkSheetCode").Text }));
                }
            }

            else if (commandName =="ReworkRangeMNID")
            {
                // 双击的是MNID/PSN
                if (obj != null)
                {
                    //if( ((ReworkSheet)obj).ReworkType == BenQGuru.eMES.Web.Helper.ReworkType.REWORKTYPE_REMO)
                    //{
                    //    ExceptionManager.Raise(this.GetType() , "$Error_Rework_NORange_For_REMO_TYPE") ;
                    //}

                    Response.Redirect(this.MakeRedirectUrl("FReworkRangeSP_New.aspx", new string[] { "ReworkSheetCode" }, new string[] { row.Items.FindItemByKey("ReworkSheetCode").Text }));
                }
            }

            else if (commandName =="SelectRoute")
            {
                //if (((ReworkSheet)obj).ReworkType == BenQGuru.eMES.Web.Helper.ReworkType.REWORKTYPE_REMO)
                //{
                //    ExceptionManager.Raise(this.GetType(), "$Error_Rework_NORoute_For_REMO_TYPE");
                //}

                Response.Redirect(this.MakeRedirectUrl("FReworkRouteSP.aspx", new string[] { "ReworkSheetCode" }, new string[] { row.Items.FindItemByKey("ReworkSheetCode").Text }));

            }


        }

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            // TODO: 不可更改的字段置只读

            if (pageAction == PageActionType.Add)
            {
                this.txtReworkSheetCodeQuery.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtReworkSheetCodeQuery.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        private object GetEditObject(GridRecord row)
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            // TODO: 用主键列的Index的替换keyIndex
            object obj = _facade.GetReworkSheet(row.Items.FindItemByKey("ReworkSheetCode").Text.ToString());

            if (obj != null)
            {
                return (ReworkSheet)obj;
            }

            return null;
        }

        #endregion

        private void InitDropDowns()
        {
            if (!this.IsPostBack)
            {
                this.drpReworkSheetStatusQuery.Items.Clear();

                new DropDownListBuilder(drpReworkSheetStatusQuery).AddAllItem(this.languageComponent1);

                this.drpReworkSheetStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_NEW), BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_NEW));
                this.drpReworkSheetStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_WAITING), BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_WAITING));
                this.drpReworkSheetStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_RELEASE), BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_RELEASE));
                this.drpReworkSheetStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_OPEN), BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_OPEN));
                this.drpReworkSheetStatusQuery.Items.Add(
                    new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_CLOSE), BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_CLOSE));

            }
        }

        private void setReworkSheetStatus(string status)
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            ArrayList objs = new ArrayList(array.Count);

            foreach (GridRecord row in array)
            {
                objs.Add((ReworkSheet)this.GetEditObject(row));
            }

            ReworkSheet[] reworkSheets = (ReworkSheet[])(objs.ToArray(typeof(ReworkSheet)));


            if (status == BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_OPEN)
            {
                this._facade.OpenReworkSheets(reworkSheets);
            }


            if (status == BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_WAITING)
            {
                // Added By Hi1/Venus.Feng on 20080729 for HIsense Version
                foreach (ReworkSheet rs in reworkSheets)
                {
                    if (rs.NeedCheck == "N")
                    {
                        ExceptionManager.Raise(this.GetType(), "$Error_NeedNotToWait $Domain_ReworkSheet=" + rs.ReworkCode);
                    }
                }
                // End Added

                this._facade.WaitingReworkSheets(reworkSheets);

                /* 等待后给第一位签核人发送邮件通知 */
                // Marked By HI1/Venus.Feng on 20080808 for Hisense : not need to send mail
                //new MailHelper(base.DataProvider,this.GetUserMail()).SendMailToFirst( reworkSheets );
            }

            if (status == BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_CLOSE)
            {
                this._facade.CloseReworkSheets(reworkSheets, this.GetUserCode());
            }

            this.RequestData();
        }


        protected void cmdWait_ServerClick(object sender, System.EventArgs e)
        {
            this.setReworkSheetStatus(BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_WAITING);


        }

        protected void cmdMOClose_ServerClick(object sender, System.EventArgs e)
        {
            this.setReworkSheetStatus(BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_CLOSE);
        }

        protected void cmdOpen_ServerClick(object sender, System.EventArgs e)
        {
            this.setReworkSheetStatus(BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_OPEN);
        }


        #region Export
        // 2005-04-06
        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
								((ReworkSheetQuery)obj).ReworkCode.ToString(),
								((ReworkSheetQuery)obj).ItemCode.ToString(),
								((ReworkSheetQuery)obj).ReworkQty.ToString(),
                                ((ReworkSheetQuery)obj).ReworkRealQty.ToString(),
								this.languageComponent1.GetString( ((ReworkSheet)obj).Status ) ,
								this.languageComponent1.GetString( ((ReworkSheet)obj).ReworkType ),
                                ((ReworkSheetQuery)obj).DutyCode,
                                ((ReworkSheetQuery)obj).MaterialModelCode,
                                ((ReworkSheetQuery)obj).BigStepSequenceCode,
                                ((ReworkSheetQuery)obj).DDATE.ToString(),
                                ((ReworkSheetQuery)obj).ReworkReason.ToString(),
                                ((ReworkSheetQuery)obj).ReasonAnalyse.ToString(),
                                ((ReworkSheetQuery)obj).Soluation.ToString(),
                                //((ReworkSheetQuery)obj).MaintainUser.ToString(),
                                ((ReworkSheetQuery)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((ReworkSheetQuery)obj).MaintainDate),
							};

        }

        private string[] GetColumnHeaderText()
        {
            return new string[]{
                                   "ReworkSheetCode",
                                   "ItemCode",
                                   "ReworkQty",
                                   "RealQty",
                                   "ReworkSheetStatus",
                                   "ReworkType",
                                   "DutyCode",
                                   "MaterialModelCode",
                                   "BigSSCode",
                                   "ReworkDDate",
                                   "ReworkReason",
                                   "ReasonAnalyse",
                                   "Soluation",
                                   "MaintainUser",
                                   "MaintainDate",
                               };

        }


        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }
        #endregion

        protected void cmdInitial_ServerClick(object sender, EventArgs e)
        {
            if (_facade == null) { _facade = new ReworkFacadeFactory(base.DataProvider).Create(); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            ArrayList reworkSheets = new ArrayList(array.Count);

            foreach (GridRecord row in array)
            {
                ReworkSheet rSheet = (ReworkSheet)this.GetEditObject(row);
                if (string.Compare(rSheet.Status, ReworkStatus.REWORKSTATUS_OPEN, true) != 0)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_ReworkSheet_Status_Not_Open");
                    return;
                }
                if (string.Compare(rSheet.ReworkType, ReworkType.REWORKTYPE_ONLINE, true) != 0)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_ReworkSheet_ReworkType_Not_Online");
                    return;
                }
                reworkSheets.Add(rSheet);
            }

            this._facade.CancelOpenReworkSheet((ReworkSheet[])reworkSheets.ToArray(typeof(ReworkSheet)));

            this.RequestData();
        }

        public void drpReworkType_Load()
        {
            this.drpReworkType.Items.Clear();
            this.drpReworkType.Items.Add(new ListItem("", ""));
            this.drpReworkType.Items.Add(new ListItem(this.languageComponent1.GetString(ReworkType.REWORKTYPE_ONLINE), ReworkType.REWORKTYPE_ONLINE));
            this.drpReworkType.Items.Add(new ListItem(this.languageComponent1.GetString(ReworkType.REWORKTYPE_REMO), ReworkType.REWORKTYPE_REMO));
        }
    }
}
