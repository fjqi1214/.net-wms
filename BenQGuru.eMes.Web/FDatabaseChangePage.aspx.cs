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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQuru.eMES.Web
{
    public partial class FDatabaseChangePage : BasePage
    {
        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        override protected void OnInit(EventArgs e)
        {          
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string word = languageComponent1.GetString("$FDatabaseChangePage");
            if (word != string.Empty)
            {
                this.Title = word;
            }

            if (!Page.IsPostBack)
            {
                this.InitPageLanguage(this.languageComponent1, false);
                this.LoadDatabaseList();
            }
        }

        private void LoadDatabaseList()
        {
            this.RadioButtonListDatabase.Items.Clear();

            this.RadioButtonListDatabase.Items.Add(new ListItem(DBName.MES.ToString(), DBName.MES.ToString()));
            this.RadioButtonListDatabase.Items.Add(new ListItem(DBName.HIS.ToString(), DBName.HIS.ToString()));


            this.RadioButtonListDatabase.SelectedIndex = 
                this.RadioButtonListDatabase.Items.IndexOf(
                    this.RadioButtonListDatabase.Items.FindByValue(MesEnviroment.DatabasePosition.ToString()));

            this.HiddenFieldReturnValue.Value = MesEnviroment.DatabasePosition.ToString();
        }


        protected void cmdConfirm_ServerClick(object sender, EventArgs e)
        {
            MesEnviroment.DatabasePosition = ((DBName)Enum.Parse(typeof(DBName), this.RadioButtonListDatabase.SelectedValue.ToString())).ToString();

            this.HiddenFieldReturnValue.Value = this.RadioButtonListDatabase.SelectedItem.Value;
            Response.Write("<script language:javascript>javascript:window.returnValue = '" + this.RadioButtonListDatabase.SelectedItem.Value + "';</script>");
            Response.Write("<script language:javascript>javascript:window.close();</script>");            
        }
    }
}
