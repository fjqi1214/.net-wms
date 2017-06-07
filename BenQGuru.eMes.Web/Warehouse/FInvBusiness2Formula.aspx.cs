using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Material;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FInvBusiness2Formula : BaseAPage
    {
        protected System.Web.UI.WebControls.Label lblOperationSelectTitle;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private InventoryFacade facade = null;
        protected ExcelExporter excelExporter = null;

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

        #region Stable
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtBusinessCodepQuery.Text = this.GetRequestParam("businessCode");

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
            this.gridHelper.AddColumn("FormulaCode", "规则代码", null);
            this.gridHelper.AddColumn("FormulaDescription", "规则描述", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);


            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.AddDefaultColumn(true, false);
            base.InitWebGrid();

            this.gridHelper.RequestData();
        }

        protected override void AddDomainObject(ArrayList domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(this.DataProvider);
            }
            facade.AddInvBusiness2Formula((InvBusiness2Formula[])domainObject.ToArray(typeof(InvBusiness2Formula)));
        }

        protected override object GetEditObject(UltraGridRow row)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(this.DataProvider);
            }
            InvBusiness2Formula invBusiness2Formula = this.facade.CreateNewInvBusiness2Formula();
            
            invBusiness2Formula.BusinessCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBusinessCodepQuery.Text));
            invBusiness2Formula.FormulaCode = row.Cells[1].Text.Trim();
            invBusiness2Formula.MaintainUser = this.GetUserCode();
            invBusiness2Formula.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

            return invBusiness2Formula;
        }


        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(this.DataProvider);
            }
            return this.facade.QueryInvBusinessNotFormulaCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBusinessCodepQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFormulaCodeQuery.Text)), 
                FormatHelper.CleanString(this.txtFormulaDescriptionQuery.Text));
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
								((InvFormula)obj).FormulaCode.ToString(),
                                ((InvFormula)obj).FormulaDesc.ToString(),
                                //((InvFormula)obj).MaintainUser.ToString(),
                      ((InvFormula)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((InvFormula)obj).MaintainDate)
							});
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(this.DataProvider);
            }
            return facade.QueryInvBusinessNotFormulaQuery(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBusinessCodepQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFormulaCodeQuery.Text)), 
                    FormatHelper.CleanString(this.txtFormulaDescriptionQuery.Text),
                    inclusive, exclusive);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(
                this.MakeRedirectUrl(@"./FInvBusiness2FormulaQuery.aspx",
                                    new string[] { "businessCode" },
                                    new string[] { this.txtBusinessCodepQuery.Text }));
        }


        #endregion

    }
}
