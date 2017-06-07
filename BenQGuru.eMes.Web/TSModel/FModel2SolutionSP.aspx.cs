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

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.TSModel;

namespace BenQGuru.eMES.Web.TSModel
{
    /// <summary>
    /// FOperation2ResourceSP 的摘要说明。
    /// </summary>
    public partial class FModel2SolutionSP : BaseMPageNew
    {

        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblErrorGroupCode;

        private TSModelFacade _facade;//= TSModelFacadeFactory.CreateTSModelFacade();

        #region Stable
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //this.pagerSizeSelector.Readonly = true;

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtModelCodeQuery.Text = this.GetRequestParam("Modelcode");

            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region Not Stable
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("AssSolutionCode", "已关联解决方案代码", null);
            this.gridHelper.AddColumn("SolutionDescription", "解决方案描述", null);
            this.gridHelper.AddColumn("SolutionImprove", "解决方案改进", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            this.gridWebGrid.Columns.FromKey("SolutionImprove").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, false);

            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();

        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }
            _facade.DeleteModel2Solution((Model2Solution[])domainObjects.ToArray(typeof(Model2Solution)));
        }

        protected override int GetRowCount()
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }
            return this._facade.GetSelectedSolutionByModelCodeCount(
                FormatHelper.PKCapitalFormat(this.txtModelCodeQuery.Text.Trim()),
                FormatHelper.PKCapitalFormat(this.txtSolutionCodeQuery.Text.Trim()));
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((Solution)obj).SolutionCode.ToString(),
            //                    ((Solution)obj).SolutionDescription.ToString(),
            //                    ((Solution)obj).SolutionImprove.ToString(),
            //                    ((Solution)obj).MaintainUser.ToString(),
            //                    FormatHelper.ToDateString(((Solution)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((Solution)obj).MaintainTime)});
            DataRow row = this.DtSource.NewRow();
            row["AssSolutionCode"] = ((Solution)obj).SolutionCode.ToString();
            row["SolutionDescription"] = ((Solution)obj).SolutionDescription.ToString();
            row["SolutionImprove"] = ((Solution)obj).SolutionImprove.ToString();
            row["MaintainUser"] = ((Solution)obj).MaintainUser.ToString();
            row["MaintainDate"] = FormatHelper.ToDateString(((Solution)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Solution)obj).MaintainTime);
            return row;
        }

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((Solution)obj).SolutionCode.ToString(),
								   ((Solution)obj).SolutionDescription.ToString(),
								   ((Solution)obj).MaintainUser.ToString(),
								   FormatHelper.ToDateString(((Solution)obj).MaintainDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"AssSolutionCode",
									"SolutionDescription",
									"MaintainUser",
									"MaintainDate"};
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }
            Model2Solution relation = _facade.CreateNewModel2Solution();
            relation.ModelCode = this.txtModelCodeQuery.Text.Trim();
            relation.SolutionCode = row.Items.FindItemByKey("AssSolutionCode").Text;
            relation.MaintainUser = this.GetUserCode();

            return relation;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null) { _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade(); }
            return this._facade.GetSelectedSolutionByModelCode(
                FormatHelper.PKCapitalFormat(this.txtModelCodeQuery.Text.Trim()),
                FormatHelper.PKCapitalFormat(this.txtSolutionCodeQuery.Text.Trim()),
                inclusive, exclusive);
        }

        //protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
        //{
        //    this.Response.Redirect(this.MakeRedirectUrl("./FModel2SolutionAP.aspx", new string[]{"Modelcode"}, new string[]{this.txtModelCodeQuery.Text.Trim()}));
        //}

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("../momodel/FModelMP.aspx"));
        }

        #endregion

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

        protected void btnRefesh_Click(object sender, EventArgs e)
        {
            this.gridHelper.RequestData();
        }
    }
}
