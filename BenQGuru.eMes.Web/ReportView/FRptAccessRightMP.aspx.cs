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
	/// FRptAccessRightMP 的摘要说明。
	/// </summary>
	public partial class FRptAccessRightMP : BasePage
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

            this.cmdSave.ServerClick += new EventHandler(cmdSave_ServerClick);
            this.cmdCancel.ServerClick += new EventHandler(cmdCancel_ServerClick);

		}
		#endregion
		
		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (this.IsPostBack == false)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false); 
                string strRptId = this.GetRequestParam("reportid");
                if (strRptId == "")
                    throw new Exception("$Error_RequestUrlParameter_Lost");

                ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);

                InitSecurityList(strRptId);

                object[] objTmps = rptFacade.GetRptViewEntryFolder();
                if (objTmps != null)
                {
                    RptViewEntry[] entityList = new RptViewEntry[objTmps.Length];
                    objTmps.CopyTo(entityList, 0);
                    InitReportFolderList(entityList);
                }
                if (strRptId != "")
                {
                    RptViewDesignMain rptMain = (RptViewDesignMain)rptFacade.GetRptViewDesignMain(strRptId);
                    this.txtReportName.Text = rptMain.ReportName;
                    if (rptMain != null && rptMain.ParentReportFolder != "")
                    {
                        this.drpReportFolder.SelectedValue = rptMain.ParentReportFolder;
                    }
                }
            }
		}

        private void InitSecurityList(string strRptId)
        {
            ////Modified by allen on 20081104 for change to FunctionGroup
            //BenQGuru.eMES.BaseSetting.UserFacade userFacade = new BenQGuru.eMES.BaseSetting.UserFacade(this.DataProvider);
            //object[] objGroup = userFacade.GetAllUserGroup();

            BenQGuru.eMES.BaseSetting.SystemSettingFacade systemSetFacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(this.DataProvider);
            object[] objGroup = systemSetFacade.GetAllFunctionGroup();
            ////End Modified by allen on 20081104 for change to FunctionGroup

            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            RptViewReportSecurity[] rptSecurity = rptFacade.GetRptViewReportSecurityByReportId(strRptId);

            ////Modified by allen on 20081104 for change to FunctionGroup
            //this.rptSecuritySelect.InitData(objGroup, rptSecurity);
            this.rptSecuritySelect.InitFunctionGroupData(objGroup, rptSecurity);
            ////End Modified by allen on 20081104 for change to FunctionGroup
        }

        private void InitReportFolderList(RptViewEntry[] entityList)
        {
            this.drpReportFolder.Items.Clear();
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
            drpReportFolder.Items.Add(new ListItem(prefix + entity.EntryName, entity.EntryCode));
            for (int i = 0; i < entityList.Length; i++)
            {
                if (entityList[i].ParentEntryCode == entity.EntryCode)
                {
                    char nbsp = (char)0xA0;
                    AppendParentParameter(entityList[i], entityList, prefix + (new string(nbsp, 4)));
                }
            }
        }

		#endregion

        private void cmdSave_ServerClick(object sender, EventArgs e)
        {
            string[] selectedUserGroup = this.rptSecuritySelect.SelectedUserGroup;
            if (selectedUserGroup.Length == 0)
                ////Modified by allen on 20081104 for change security: functiongroup
                //throw new Exception("$ReportDesign_Select_UserGroup");
                throw new Exception("$ReportDesign_Select_FunctionGroup");
                ////End Modified by allen on 20081104 for change security: functiongroup

            string strRptId = this.GetRequestParam("reportid");
            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            RptViewDesignMain rptMain = (RptViewDesignMain)rptFacade.GetRptViewDesignMain(strRptId);

            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
            this.DataProvider.BeginTransaction();
            try
            {
                if (rptMain.ParentReportFolder != this.drpReportFolder.SelectedValue)
                {
                    rptMain.ParentReportFolder = this.drpReportFolder.SelectedValue;
                    rptFacade.UpdateRptViewDesignMain(rptMain);

                    rptFacade.UpdateReportEntryPublish(rptMain, this.GetUserCode());
                }

                rptFacade.UpdateRptViewReportSecurity(strRptId, selectedUserGroup);

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

            InitSecurityList(strRptId);

            string alertInfo =
                string.Format("<script language=javascript>alert('{0}');</script>", this.languageComponent1.GetString("$CS_Save_Success"));
            if (!this.ClientScript.IsClientScriptBlockRegistered("SaveSuccess"))
            {
                this.ClientScript.RegisterClientScriptBlock(typeof(string), "SaveSuccess", alertInfo);
            }
        }

        private void cmdCancel_ServerClick(object sender, EventArgs e)
        {
            string strBackUrl = this.Request.QueryString["backurl"];
            if (strBackUrl != "")
            {
                this.Response.Redirect(strBackUrl);
            }
        }


	}
}
