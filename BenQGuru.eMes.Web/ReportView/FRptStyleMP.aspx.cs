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
    /// FRptStyleMP 的摘要说明。
    /// </summary>
    public partial class FRptStyleMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.ReportView.ReportViewFacade _facade;

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
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("StyleID", "StyleID", null);
            this.gridHelper.AddColumn("StyleName", "样式名称", null);
            this.gridHelper.AddColumn("StyleDescription", "描述", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddLinkColumn("Detail", "样式", null);
            this.gridWebGrid.Columns.FromKey("StyleID").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, true);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            RptViewReportStyle style = (RptViewReportStyle)obj;
            DataRow row = DtSource.NewRow();
            row["StyleID"] = style.StyleID;
            row["StyleName"] = style.Name;
            row["StyleDescription"] = style.Description;
            row["MaintainUser"] = style.MaintainUser;
            row["MaintainDate"] = FormatHelper.ToDateString(style.MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(style.MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null) { _facade = new BenQGuru.eMES.ReportView.ReportViewFacade(this.DataProvider); }
            return this._facade.QueryRptViewReportStyleByName(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStyleNameQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null) { _facade = new BenQGuru.eMES.ReportView.ReportViewFacade(this.DataProvider); }
            return this._facade.QueryRptViewReportStyleByNameCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStyleNameQuery.Text)));
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Detail")
            {
                string strUrl = "FRptStyleDetailMP.aspx?styleid=" + row.Items.FindItemByKey("StyleID").Text + "&stylename=" + Server.UrlEncode(row.Items.FindItemByKey("StyleName").Text);
                this.Response.Redirect(strUrl);
            }

        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null) { _facade = new BenQGuru.eMES.ReportView.ReportViewFacade(this.DataProvider); }
            ((RptViewReportStyle)domainObject).StyleID = _facade.GetNextStyleID();
            this._facade.AddRptViewReportStyle((RptViewReportStyle)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null) { _facade = new BenQGuru.eMES.ReportView.ReportViewFacade(this.DataProvider); }
            this._facade.DeleteRptViewReportStyle((RptViewReportStyle[])domainObjects.ToArray(typeof(RptViewReportStyle)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null) { _facade = new BenQGuru.eMES.ReportView.ReportViewFacade(this.DataProvider); }
            this._facade.UpdateRptViewReportStyle((RptViewReportStyle)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null) { _facade = new BenQGuru.eMES.ReportView.ReportViewFacade(this.DataProvider); }
            RptViewReportStyle style = (RptViewReportStyle)_facade.GetRptViewReportStyle(decimal.Parse(this.txtStyleID.Text));
            if (style == null)
                style = this._facade.CreateNewRptViewReportStyle();

            style.Name = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStyleNameEdit.Text, 40));
            style.Description = this.txtStyleDescEdit.Text;
            style.MaintainUser = this.GetUserCode();
            style.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
            style.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

            return style;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null) { _facade = new BenQGuru.eMES.ReportView.ReportViewFacade(this.DataProvider); }
            object obj = _facade.GetRptViewReportStyle(decimal.Parse(row.Items.FindItemByKey("StyleID").Text.ToString()));

            if (obj != null)
            {
                return (RptViewReportStyle)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtStyleID.Text = "0";
                this.txtStyleNameEdit.Text = "";
                this.txtStyleDescEdit.Text = "";

                return;
            }

            RptViewReportStyle style = (RptViewReportStyle)obj;
            this.txtStyleID.Text = style.StyleID.ToString();
            this.txtStyleNameEdit.Text = style.Name;
            this.txtStyleDescEdit.Text = style.Description;
        }


        protected override bool ValidateInput()
        {

            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblStyleNameEdit, txtStyleNameEdit, 40, true));

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
            RptViewReportStyle style = (RptViewReportStyle)obj;
            return new string[]{  style.Name,
								   style.Description,
								   style.MaintainUser,
								   FormatHelper.ToDateString(style.MaintainDate),
								   FormatHelper.ToTimeString(style.MaintainTime) };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
									"StyleName",
									"StyleDescription",
									"MaintainUser",	
									"MaintainDate",
									"MaintainTime"};
        }
        #endregion
    }
}
