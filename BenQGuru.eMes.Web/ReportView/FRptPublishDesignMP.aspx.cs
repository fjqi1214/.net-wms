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
	/// FRptPublishDesignMP 的摘要说明。
	/// </summary>
	public partial class FRptPublishDesignMP : BasePage
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

            this.cmdPublish.ServerClick += new EventHandler(cmdPublish_ServerClick);
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
                InitReportList();

                string strRptId = this.GetRequestParam("reportid");

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
                    this.drpReportName.SelectedValue = strRptId;
                    this.drpReportName.Enabled = false;
                    if (rptMain != null && rptMain.ParentReportFolder != "")
                    {
                        this.drpReportFolder.SelectedValue = rptMain.ParentReportFolder;
                    }
                }
                if (this.drpReportFolder.Items.Count == 0 || this.drpReportName.Items.Count == 0)
                {
                    this.cmdPublish.Disabled = true;
                    this.cmdPublish.Attributes.Add("disabled", "disabled");
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

        private void InitReportList()
        {
            this.drpReportName.Items.Clear();
            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            object[] objs = rptFacade.GetRptviewDesignMainByStatus(ReportDesignStatus.Initial, ReportDesignStatus.ReDesign, ReportDesignStatus.Publish);
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    RptViewDesignMain rptMain = (RptViewDesignMain)objs[i];
                    if (rptMain.Status == ReportDesignStatus.Initial ||
                        rptMain.Status == ReportDesignStatus.ReDesign ||
                        rptMain.Status == ReportDesignStatus.Publish)
                    {
                        this.drpReportName.Items.Add(new ListItem(rptMain.ReportName, rptMain.ReportID));
                    }
                }
            }
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

        private void cmdPublish_ServerClick(object sender, EventArgs e)
        {
            string[] selectedUserGroup = this.rptSecuritySelect.SelectedUserGroup;
            if (selectedUserGroup.Length == 0)
                ////Modified by allen on 20081104 for change security: functiongroup
                //throw new Exception("$ReportDesign_Select_UserGroup");
                throw new Exception("$ReportDesign_Select_FunctionGroup");
                ////End Modified by allen on 20081104 for change security: functiongroup

            string strRptId = this.drpReportName.SelectedValue;
            string strFormatXml = Server.MapPath("ReportFormat.xml");
            ReportGenerater rptGenerater = new ReportGenerater(this.DataProvider);

            string strFileName = System.Web.HttpContext.Current.Server.MapPath("../ReportFiles");
            if (System.IO.Directory.Exists(strFileName) == false)
                System.IO.Directory.CreateDirectory(strFileName);
            if (strRptId != "")
                strFileName += "\\" + strRptId + ".rdlc";
            else
                strFileName += "\\" + System.Guid.NewGuid().ToString() + ".rdlc";

            rptGenerater.Generate(strRptId, strFormatXml, strFileName);
            string strRptFile = strFileName.Substring(strFileName.LastIndexOf("\\", strFileName.LastIndexOf("\\") - 1) + 1);

            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            DBDateTime dDate = FormatHelper.GetNowDBDateTime(this.DataProvider);
            RptViewDesignMain rptMain = (RptViewDesignMain)rptFacade.GetRptViewDesignMain(strRptId);
            rptMain.ReportFileName = strRptFile;
            rptMain.Status = ReportDesignStatus.Publish;
            rptMain.ParentReportFolder = this.drpReportFolder.SelectedValue;
            rptMain.PublishUser = this.GetUserCode();
            rptMain.PublishDate = dDate.DBDate;
            rptMain.PublishTime = dDate.DBTime;
            rptMain.MaintainUser = this.GetUserCode();
            rptMain.MaintainDate = dDate.DBDate;
            rptMain.MaintainTime = dDate.DBTime;

            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
            this.DataProvider.BeginTransaction();
            try
            {
                rptFacade.UpdateRptViewDesignMain(rptMain);

                rptFacade.UpdateReportEntryPublish(rptMain, this.GetUserCode());

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

            this.Session["PublishedReportId"] = strRptId;
            string alertInfo =
                string.Format("alert('{0}');", this.languageComponent1.GetString("$ReportDesign_Publish_Success"));
           // string.Format("<script language=javascript>alert('{0}');window.top.location.reload();</script>", this.languageComponent1.GetString("$ReportDesign_Publish_Success"));

            if (!this.ClientScript.IsClientScriptBlockRegistered("SaveSuccess"))
            {
                //this.ClientScript.RegisterClientScriptBlock(typeof(string), "SaveSuccess", alertInfo);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "SaveSuccess", alertInfo, true);
            }
            
        }

        void cmdCancel_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect("FRptMaintainStartPageMP.aspx");
        }


	}
}
