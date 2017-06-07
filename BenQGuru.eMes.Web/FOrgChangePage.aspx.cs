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

using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;

namespace BenQuru.eMES.Web
{
    public partial class FOrgChangePage : BasePage
    {
        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private BaseModelFacade m_BaseFacade;

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            
            InitializeComponent();
            base.OnInit(e);
        }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            string word = languageComponent1.GetString("$OrgChangePageTitle");
            if (word != string.Empty)
            {
                this.Title = word;
            }

            if (!Page.IsPostBack)
            {
                this.InitPageLanguage(this.languageComponent1, false);
                this.LoadOrgList();
            }
        }

        private BaseModelFacade baseFacade
        {
            get
            {
                if (this.m_BaseFacade == null)
                {
                    this.m_BaseFacade = new BaseModelFacade(base.DataProvider);
                }
                return m_BaseFacade;
            }
        }

        private void LoadOrgList()
        {
            object[] orgList = baseFacade.GetAllOrgByUserCode(this.GetUserCode());
            this.RadioButtonListOrg.Items.Clear();
            foreach (Organization org in orgList)
            {
                this.RadioButtonListOrg.Items.Add(new ListItem(org.OrganizationDescription, org.OrganizationID.ToString()));
            }

            this.RadioButtonListOrg.SelectedIndex = 
                this.RadioButtonListOrg.Items.IndexOf(
                    this.RadioButtonListOrg.Items.FindByValue(GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString()));

            this.HiddenFieldReturnValue.Value = GlobalVariables.CurrentOrganizations.First().OrganizationDescription;
        }

        protected void RadioButtonListOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void cmdConfirm_ServerClick(object sender, EventArgs e)
        {
            object org = baseFacade.GetOrg(int.Parse(this.RadioButtonListOrg.SelectedItem.Value));

            GlobalVariables.CurrentOrganizations.Clear();
            GlobalVariables.CurrentOrganizations.Add((Organization)org);

            this.HiddenFieldReturnValue.Value = ((Organization)org).OrganizationDescription;
            Response.Write("<script language:javascript>javascript:window.returnValue = '" + ((Organization)org).OrganizationDescription + "';</script>");
            Response.Write("<script language:javascript>javascript:window.close();</script>");            
        }
    }
}
