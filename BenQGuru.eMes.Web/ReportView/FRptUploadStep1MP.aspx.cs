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
	/// FRptUploadStep1MP 的摘要说明。
	/// </summary>
    public partial class FRptUploadStep1MP : ReportWizardBasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
	
		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
            this.FirstStepPage = "FRptUploadStep1MP.aspx";

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
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
                object[] objsDS = rptFacade.GetAllRptViewDataSource();
                this.drpDataSource.Items.Clear();
                if (objsDS != null)
                {
                    for (int i = 0; i < objsDS.Length; i++)
                    {
                        RptViewDataSource dataSource = (RptViewDataSource)objsDS[i];
                        this.drpDataSource.Items.Add(new ListItem(dataSource.Name, dataSource.DataSourceID.ToString()));
                    }
                }
            }
		}

        protected override void DisplayDesignData()
        {
            if (this.designView.DesignMain != null)
            {
                this.drpDataSource.SelectedValue = this.designView.DesignMain.DataSourceID.ToString();
                this.txtUploadRptName.Text = this.designView.DesignMain.ReportName;
                this.txtUploadRptDesc.Text = this.designView.DesignMain.Description;
            }
        }

        protected override bool ValidateInput()
        {
            if (fileRpt.PostedFile == null || fileRpt.PostedFile.FileName == "")
            {
                if (this.designView.DesignMain != null &&
                    (string.IsNullOrEmpty(this.designView.DesignMain.ReportFileName) == false || string.IsNullOrEmpty(this.designView.UploadFileName) == false))
                {
                    return true;
                }
                throw new Exception("$Error_UploadFileIsEmpty");
            }
            string strUploadFileName = fileRpt.PostedFile.FileName;
            string strExt = strUploadFileName.Substring(strUploadFileName.LastIndexOf("."));
            if (fileRpt.PostedFile.FileName.ToLower().EndsWith(".rdl") == false &&
                fileRpt.PostedFile.FileName.ToLower().EndsWith(".rdlc") == false)
            {
                throw new Exception("$ReportUpload_Invalid_FileName");
            }

            if (this.txtUploadRptName.Text.Trim() == "")
            {
                WebInfoPublish.Publish(this, this.lblUploadRptName.Text + " $Error_Input_Empty", this.languageComponent1);
                return false;
            }
            ReportViewFacade rptFacde = new ReportViewFacade(this.DataProvider);
            RptViewDesignMain rptMain = rptFacde.GetRptViewDesignMainByReportName(this.txtUploadRptName.Text.Trim());
            if (rptMain != null)
            {
                bool bError = false;
                if (string.IsNullOrEmpty(this.GetRequestParam("requestid")) == false && rptMain.ReportID != this.GetRequestParam("reportid"))
                {
                    bError = true;
                }
                if (this.designView == null || this.designView.DesignMain == null)
                {
                    bError = true;
                }
                if (this.designView != null && this.designView.DesignMain != null && rptMain.ReportID != this.designView.DesignMain.ReportID)
                {
                    bError = true;
                }
                if (bError == true)
                {
                    WebInfoPublish.Publish(this, "$ReportDesign_ReportName_Exist [" + this.txtUploadRptName.Text + "]", this.languageComponent1);
                    return false;
                }
            }
            return true;
        }

        protected override void RedirectToBack()
        {
            //
        }

        protected override void RedirectToNext()
        {
            this.Response.Redirect("FRptUploadStep2MP.aspx");
        }

        protected override void UpdateReportDesignView()
        {
            if (fileRpt.PostedFile != null && fileRpt.PostedFile.FileName != "")
            {
                string strUploadFileName = fileRpt.PostedFile.FileName;
                string strExt = strUploadFileName.Substring(strUploadFileName.LastIndexOf("\\") + 1);
                if (fileRpt.PostedFile.FileName.ToLower().EndsWith(".rdl") == false &&
                    fileRpt.PostedFile.FileName.ToLower().EndsWith(".rdlc") == false)
                {
                    throw new Exception("$ReportUpload_Invalid_FileName");
                }
                string strFolder = Server.MapPath("../") + "\\ReportFiles\\temp\\" + FormatHelper.TODateInt(DateTime.Today);
                if (System.IO.Directory.Exists(strFolder) == false)
                    System.IO.Directory.CreateDirectory(strFolder);
                string strFileName = strFolder + "\\" + strExt;
                fileRpt.PostedFile.SaveAs(strFileName);

                try
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.Load(strFileName);
                }
                catch (Exception ex)
                {
                    throw new Exception("$ReportUpload_Invalid_XMLFormat [" + ex.Message + "]");
                }

                this.designView.UploadFileName = strFileName;
            }
            else if (this.designView.DesignMain != null && 
                (string.IsNullOrEmpty(this.designView.DesignMain.ReportFileName) == false || string.IsNullOrEmpty(this.designView.UploadFileName) == false))
            {
                if (string.IsNullOrEmpty(this.designView.UploadFileName) == true)
                {
                    string strUploadFileName = Server.MapPath("..") + "\\" + this.designView.DesignMain.ReportFileName;
                    this.designView.UploadFileName = strUploadFileName;
                }
            }
            RptViewDesignMain main = new RptViewDesignMain();
            if (this.designView.DesignMain != null)
                main = this.designView.DesignMain;
            main.ReportName = this.txtUploadRptName.Text.Trim().ToUpper();
            main.Description = this.txtUploadRptDesc.Text;
            main.DataSourceID = Convert.ToDecimal(this.drpDataSource.SelectedValue);
            this.designView.DesignMain = main;
        }

		#endregion

        


	}
}
