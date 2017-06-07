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
using BenQGuru.eMES.Domain.Warehouse;
using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FInvBusiness2FormulaQuery : BaseMPage
    {
        protected System.Web.UI.WebControls.Label lblOperationSelectTitle;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private InventoryFacade facade = null;

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

                this.txtBusinessCodeQuery.Text = this.GetRequestParam("businessCode");

                if (facade == null)
                {
                    facade = new InventoryFacade(this.DataProvider);
                }
                InvBusiness business = (InvBusiness)facade.GetInvBusiness(this.GetRequestParam("businessCode"), GlobalVariables.CurrentOrganizations.First().OrganizationID);
                this.txtBusinessDescriptionQuery.Text = business.BusinessDescription;

            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region NotStable
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

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(this.DataProvider);
            }
            this.facade.DeleteInvBusiness2Formula((InvBusiness2Formula[])domainObjects.ToArray(typeof(InvBusiness2Formula)));
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(this.DataProvider);
            }
            return this.facade.QueryInvBusinessFormulaCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBusinessCodeQuery.Text)));
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
            return facade.QueryInvBusinessFormulaQuery(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBusinessCodeQuery.Text)),
                inclusive, exclusive);
        }

        protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(
                    this.MakeRedirectUrl("./FInvBusiness2Formula.aspx",
                                            new string[] { "businessCode" },
                                            new string[] { this.txtBusinessCodeQuery.Text.Trim() }));
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FInvBusinessMP.aspx"));
        }

        protected override object GetEditObject(UltraGridRow row)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(this.DataProvider);
            }
            object obj = this.facade.GetInvBusiness2Formula(this.txtBusinessCodeQuery.Text.Trim(), row.Cells[1].Text.Trim(), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (obj != null)
            {
                return (InvBusiness2Formula)obj;
            }

            return null;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((InvFormula)obj).FormulaCode.ToString(),
                                ((InvFormula)obj).FormulaDesc.ToString(),
                                //((InvFormula)obj).MaintainUser.ToString(),
                                ((InvFormula)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((InvFormula)obj).MaintainDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"FormulaCode",
                                    "FormulaDescription",
                                    "MaintainUser",	
                                    "MaintainDate"};
        }

        #endregion
    }
}
