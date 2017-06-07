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

namespace BenQGuru.eMES.Web.ReportView
{
	/// <summary>
	/// FRptTextFormatMP 的摘要说明。
	/// </summary>
    public partial class FRptTextFormatMP : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
	
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
            if (this.IsPostBack == false)
            {
                this.InitPageLanguage(this.languageComponent1, false);
                this.drpFontName.Items.Clear();
                foreach (System.Drawing.FontFamily family in System.Drawing.FontFamily.Families)
                {
                    this.drpFontName.Items.Add(new ListItem(family.Name, family.Name));
                }
                this.drpTextAlign.Items.Clear();
                this.drpTextAlign.Items.Add(new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.TextAlign.Left), BenQGuru.eMES.Web.Helper.TextAlign.Left));
                this.drpTextAlign.Items.Add(new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.TextAlign.Center), BenQGuru.eMES.Web.Helper.TextAlign.Center));
                this.drpTextAlign.Items.Add(new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.TextAlign.Right), BenQGuru.eMES.Web.Helper.TextAlign.Right));
                this.drpVerticalAlign.Items.Clear();
                this.drpVerticalAlign.Items.Add(new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.VerticalAlign.Top), BenQGuru.eMES.Web.Helper.VerticalAlign.Top));
                this.drpVerticalAlign.Items.Add(new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.VerticalAlign.Middle), BenQGuru.eMES.Web.Helper.VerticalAlign.Middle));
                this.drpVerticalAlign.Items.Add(new ListItem(this.languageComponent1.GetString(BenQGuru.eMES.Web.Helper.VerticalAlign.Bottom), BenQGuru.eMES.Web.Helper.VerticalAlign.Bottom));
                this.lblTextFormatTitle.Text = this.languageComponent1.GetString("$PageControl_TextFormatTitle");
                string strExistValue = Request["existvalue"];
                if (strExistValue != null && strExistValue != "")
                {
                    string[] existValue = strExistValue.Split(';');
                    if (existValue.Length >= 10)
                    {
                        if (existValue[0] != "")
                            this.drpFontName.SelectedValue = existValue[0];
                        this.txtFontSize.Text = existValue[1];
                        this.chkFontWeight.Checked = (existValue[2] == "true");
                        this.chkFontItalic.Checked = (existValue[3] == "true");
                        this.chkFontDecoration.Checked = (existValue[4] == "true");
                        this.txtFontColor.Value = existValue[5];
                        this.txtBackColor.Value = existValue[6];
                        this.drpTextAlign.SelectedValue = existValue[7];
                        this.drpVerticalAlign.SelectedValue = existValue[8];
                        this.txtColumnWidth.Text = existValue[9];
                        this.chkBorderVisible.Checked = (existValue[10] == "true");
                        this.txtTextFormat.Text = existValue[11];
                        this.txtValueContent.Text = existValue[12];
                    }
                }
                if (this.GetRequestParam("showdelete") == "1")
                {
                    this.cmdDeleteItem.Visible = true;
                }
                else
                    this.cmdDeleteItem.Visible = false;

            }
		}

		#endregion

        


	}
}
