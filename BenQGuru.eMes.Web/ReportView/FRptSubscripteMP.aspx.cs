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
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Domain.ReportView;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.ReportView
{
    /// <summary>
    /// FRptSubscripteMP 的摘要说明。
    /// </summary>
    public partial class FRptSubscripteMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.ReportView.ReportViewFacade rptFacade;

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
            this.gridWebGrid.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid_InitializeRow);
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
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

            this.drpRptId.SelectedIndexChanged += new EventHandler(drpRptId_SelectedIndexChanged);
            this.cmdSave.ServerClick += new EventHandler(cmdSave_ServerClick);
            this.PreRender += new EventHandler(FRptSubscripteMP_PreRender);
        }

        void FRptSubscripteMP_PreRender(object sender, EventArgs e)
        {
            this.cmdSave.Disabled = false;
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
                object[] objTmps = rptFacade.GetAllRptViewEntry();
                if (objTmps != null)
                {
                    RptViewEntry[] entityList = new RptViewEntry[objTmps.Length];
                    objTmps.CopyTo(entityList, 0);
                    InitReportFolderList(entityList);
                }
            }
        }

        private void InitReportFolderList(RptViewEntry[] entityList)
        {
            this.drpRptId.Items.Clear();
            for (int i = 0; i < entityList.Length; i++)
            {
                if (entityList[i].ParentEntryCode == "")
                {
                    AppendParentParameter(entityList[i], entityList, "");
                }
            }
        }
        private void AppendParentParameter(RptViewEntry entity, RptViewEntry[] entityList, string prefix)
        {
            drpRptId.Items.Add(new ListItem(prefix + entity.EntryName, entity.EntryCode));
            for (int i = 0; i < entityList.Length; i++)
            {
                if (entityList[i].ParentEntryCode == entity.EntryCode)
                {
                    char nbsp = (char)0xA0;
                    AppendParentParameter(entityList[i], entityList, prefix + (new string(nbsp, 4)));
                }
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        private void drpRptId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strValue = this.drpRptId.SelectedValue;
            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            object objEntry = rptFacade.GetRptViewEntry(strValue);
            if (objEntry != null)
            {
                RptViewEntry entry = (RptViewEntry)objEntry;
                if (entry.EntryType == ReportEntryType.Report && entry.ReportID != "")
                {
                    this.gridHelper.RequestData();
                    return;
                }
            }
            this.gridWebGrid.Rows.Clear();
            this.pagerToolBar.RowCount = 0;
            this.chkReportAsDefault.Checked = false;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ReportID", "ReportID", null);
            this.gridHelper.AddColumn("Sequence", "Sequence", null);
            this.gridHelper.AddColumn("InputType", "输入类型", null);
            this.gridHelper.AddColumn("InputTypeDesc", "输入类型", null);
            this.gridHelper.AddColumn("InputName", "输入名称", null);
            this.gridHelper.AddColumn("InputNameDesc", "输入名称", null);
            this.gridHelper.AddColumn("InputValue", "输入值", null);
            this.gridHelper.AddColumn("SqlFilterSequence", "SQL参数次序", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridWebGrid.Columns.FromKey("ReportID").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Sequence").Hidden = true;
            this.gridWebGrid.Columns.FromKey("InputType").Hidden = true;
            this.gridWebGrid.Columns.FromKey("InputName").Hidden = true;
            this.gridWebGrid.Columns.FromKey("SqlFilterSequence").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("InputValue").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
            //this.gridWebGrid.Columns.FromKey("InputValue").CellStyle.BackColor = Color.FromArgb(255, 252, 240);//#fffcf0
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["InputValue"].ReadOnly = false;

            this.gridHelper.AddDefaultColumn(false, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        void gridWebGrid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
        {
            //e.Row.Items.FindItemByKey("RouteDescription").CssClass = "ForeColorRed CellBackColor";
            string strScript = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                            e.Row.Index, e.Row.Items.FindItemByKey("InputValue").Column.Index);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript, true);
        }

        protected override DataRow GetGridRow(object obj)
        {
            RptViewUserSubscription subs = (RptViewUserSubscription)obj;
            DataRow row = DtSource.NewRow();
            row["ReportID"] = subs.ReportID;
            row["Sequence"] = subs.Sequence;
            row["InputType"] = subs.InputType;
            row["InputTypeDesc"] = this.languageComponent1.GetString(subs.InputType);
            row["InputName"] = subs.InputName;
            row["InputNameDesc"] = subs.EAttribute1;
            row["InputValue"] = subs.InputValue;
            row["SqlFilterSequence"] = subs.SqlFilterSequence;
            row["MaintainUser"] = subs.MaintainUser;
            row["MaintainDate"] = FormatHelper.ToDateString(subs.MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(subs.MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            string strValue = this.drpRptId.SelectedValue;

            if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
            object objEntry = rptFacade.GetRptViewEntry(strValue);
            if (objEntry != null)
            {
                RptViewEntry entry = (RptViewEntry)objEntry;
                if (entry.EntryType == ReportEntryType.Report && entry.ReportID != "")
                {
                    RptViewUserDefault objDef = (RptViewUserDefault)rptFacade.GetRptViewUserDefault(this.GetUserCode());
                    this.chkReportAsDefault.Checked = false;
                    if (objDef != null && objDef.DefaultReportID == entry.ReportID)
                    {
                        this.chkReportAsDefault.Checked = true;
                    }

                    object[] objs = rptFacade.GetNeedInputByReportId(entry.ReportID, this.GetUserCode());
                    this.pagerToolBar.RowCount = objs.Length;
                    return objs;
                }
            }
            this.chkReportAsDefault.Checked = false;
            this.pagerToolBar.RowCount = 0;
            return null;
        }

        protected override int GetRowCount()
        {
            return 0;
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
        }

        protected override void UpdateDomainObject(object domainObject)
        {
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            return null;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (rptFacade == null)
            {
                rptFacade = new ReportViewFacade(this.DataProvider);
            }
            object obj = rptFacade.GetRptViewUserSubscription(this.GetUserCode(), row.Items.FindItemByKey("ReportID").Text, decimal.Parse(row.Items.FindItemByKey("Sequence").Value.ToString()));

            if (obj != null)
            {
                return obj as RptViewUserSubscription;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
        }


        protected override bool ValidateInput()
        {
            return true;

        }

        #endregion

        private void cmdSave_ServerClick(object sender, EventArgs e)
        {
            DBDateTime dDate = FormatHelper.GetNowDBDateTime(this.DataProvider);
            string strReportId = "";
            RptViewUserSubscription[] subs = new RptViewUserSubscription[this.gridWebGrid.Rows.Count];
            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                GridRecord row = this.gridWebGrid.Rows[i];
                RptViewUserSubscription s = new RptViewUserSubscription();
                s.UserCode = this.GetUserCode();
                s.ReportID = row.Items.FindItemByKey("ReportID").Text;
                s.Sequence = i + 1;
                s.InputType = row.Items.FindItemByKey("InputType").Text;
                s.InputName = row.Items.FindItemByKey("InputName").Text;
                s.InputValue = row.Items.FindItemByKey("InputValue").Text;
                s.SqlFilterSequence = decimal.Parse(row.Items.FindItemByKey("SqlFilterSequence").Text);
                s.MaintainUser = this.GetUserCode();
                s.MaintainDate = dDate.DBDate;
                s.MaintainTime = dDate.DBTime;
                subs[i] = s;
                strReportId = s.ReportID;
                this.gridWebGrid.Rows[i].Items.FindItemByKey("MaintainUser").Text = s.MaintainUser;
                this.gridWebGrid.Rows[i].Items.FindItemByKey("MaintainDate").Text = FormatHelper.ToDateString(s.MaintainDate);
                this.gridWebGrid.Rows[i].Items.FindItemByKey("MaintainTime").Text = FormatHelper.ToTimeString(s.MaintainTime);
            }

            if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
            if (strReportId == "")
            {
                string strValue = this.drpRptId.SelectedValue;
                object objEntry = rptFacade.GetRptViewEntry(strValue);
                strReportId = ((RptViewEntry)objEntry).ReportID;
            }

            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
            this.DataProvider.BeginTransaction();
            try
            {
                rptFacade.UpdateRptViewUserSubscriptionByReportId(strReportId, this.GetUserCode(), subs, this.chkReportAsDefault.Checked);

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }

            string alertInfo =
                string.Format("<script language=javascript>alert('{0}');</script>", this.languageComponent1.GetString("$CS_Save_Success"));
            if (!this.ClientScript.IsClientScriptBlockRegistered("SaveSuccess"))
            {
                this.ClientScript.RegisterClientScriptBlock(typeof(string), "SaveSuccess", alertInfo);
            }

        }

    }
}
