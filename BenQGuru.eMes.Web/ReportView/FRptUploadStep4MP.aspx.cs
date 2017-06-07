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
using System.Xml;
using System.Collections.Generic;

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Domain.ReportView;

namespace BenQGuru.eMES.Web.ReportView
{
    /// <summary>
    /// FRptUploadStep4MP 的摘要说明。
    /// </summary>
    public partial class FRptUploadStep4MP : ReportWizardBasePage
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
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                string strFileName = this.designView.UploadFileName;
                strFileName = strFileName.Substring(strFileName.LastIndexOf("\\") + 1);
                this.txtReportFile.Text = strFileName;
                this.txtReportName.Text = this.designView.DesignMain.ReportName;

                ////Modified by allen on 20081104 for change to FunctionGroup
                //BenQGuru.eMES.BaseSetting.UserFacade userFacade = new BenQGuru.eMES.BaseSetting.UserFacade(this.DataProvider);
                //object[] objGroup = userFacade.GetAllUserGroup();
                BenQGuru.eMES.BaseSetting.SystemSettingFacade systemSetFacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(this.DataProvider);
                object[] objGroup = systemSetFacade.GetAllFunctionGroup();
                ////End Modified by allen on 20081104 for change to FunctionGroup

                ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
                RptViewReportSecurity[] rptSecurity = null;
                if (this.designView.DesignMain.ReportID != "")
                {
                    rptSecurity = rptFacade.GetRptViewReportSecurityByReportId(this.designView.DesignMain.ReportID);
                }

                ////Modified by allen on 20081104 for change to FunctionGroup
                //this.rptSecuritySelect.InitData(objGroup, rptSecurity);
                this.rptSecuritySelect.InitFunctionGroupData(objGroup, rptSecurity);
                ////End Modified by allen on 20081104 for change to FunctionGroup

                object[] objTmps = rptFacade.GetRptViewEntryFolder();
                if (objTmps != null)
                {
                    RptViewEntry[] entityList = new RptViewEntry[objTmps.Length];
                    objTmps.CopyTo(entityList, 0);
                    InitReportFolderList(entityList);
                }
                RptViewDesignMain rptMain = this.designView.DesignMain;
                if (rptMain != null && rptMain.ParentReportFolder != "")
                {
                    this.drpReportFolder.SelectedValue = rptMain.ParentReportFolder;
                }

                if (this.drpReportFolder.Items.Count == 0)
                {
                    this.cmdPublish.Disabled = true;
                    this.cmdPublish.Attributes.Add("disabled", "disabled");
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

        private void cmdPublish_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInput() == false)
                return;
            UpdateReportDesignView();
            this.SaveDesignView(designView);

            string strFileName = "", strRptFile = "";
            if (string.IsNullOrEmpty(this.designView.DesignMain.ReportFileName) == true)
            {
                strFileName = this.designView.UploadFileName;
                strFileName = strFileName.Substring(strFileName.LastIndexOf("\\") + 1);
                strFileName = Server.MapPath("../ReportFiles") + "\\" + strFileName;

                strRptFile = strFileName.Substring(Server.MapPath("../").Length);
                this.designView.DesignMain.ReportFileName = strRptFile;
            }

            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
            this.DataProvider.BeginTransaction();
            try
            {
                rptFacade.PublishUploadedReportFile(this.designView, strFileName, this.GetUserCode());

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

            string[] strVal = new string[this.designView.ReportSecurity.Length];
            for (int i = 0; i < this.designView.ReportSecurity.Length; i++)
            {
                ////Modified by allen on 20081104 for change security: functiongroup
                //strVal[i] = this.designView.ReportSecurity[i].UserGroupCode;
                strVal[i] = this.designView.ReportSecurity[i].FunctionGroupCode;
                ////End Modified by allen on 20081104 for change security: functiongroup
            }
            this.rptSecuritySelect.SetSelectedItem(strVal);

            this.Session["PublishedReportId"] = this.designView.ReportID;

            string alertInfo =
                string.Format("<script language=javascript>alert('{0}');window.top.location.reload();</script>", this.languageComponent1.GetString("$ReportDesign_Publish_Success"));
            if (!this.ClientScript.IsClientScriptBlockRegistered("SaveSuccess"))
            {
                this.ClientScript.RegisterClientScriptBlock(typeof(string), "SaveSuccess", alertInfo);
            }
            this.cmdPublish.Disabled = true;
            this.cmdPublish.Attributes.Add("disabled", "disabled");
        }

        protected override void DisplayDesignData()
        {
            if (this.designView.ReportSecurity != null)
            {
                string[] strVal = new string[this.designView.ReportSecurity.Length];
                for (int i = 0; i < this.designView.ReportSecurity.Length; i++)
                {
                    ////Modified by allen on 20081104 for change security: functiongroup
                    //strVal[i] = this.designView.ReportSecurity[i].UserGroupCode;
                    strVal[i] = this.designView.ReportSecurity[i].FunctionGroupCode;
                    ////End Modified by allen on 20081104 for change security: functiongroup
                }
                this.rptSecuritySelect.SetSelectedItem(strVal);
            }
            if (this.designView.DesignMain.ParentReportFolder != "")
            {
                this.drpReportFolder.SelectedValue = this.designView.DesignMain.ParentReportFolder;
            }
        }

        protected override bool ValidateInput()
        {
            return true;
        }

        protected override void RedirectToBack()
        {
            this.Response.Redirect("FRptUploadStep3MP.aspx");
        }

        protected override void RedirectToNext()
        {
            //
        }

        protected override void UpdateReportDesignView()
        {
            string[] selectedUserGroup = this.rptSecuritySelect.SelectedUserGroup;
            if (selectedUserGroup.Length == 0)
                ////Modified by allen on 20081104 for change security: functiongroup
                //throw new Exception("$ReportDesign_Select_UserGroup");
                throw new Exception("$ReportDesign_Select_FunctionGroup");
                ////End Modified by allen on 20081104 for change security: functiongroup

            RptViewReportSecurity[] rptSecurity = new RptViewReportSecurity[selectedUserGroup.Length];
            for (int i = 0; i < selectedUserGroup.Length; i++)
            {
                RptViewReportSecurity security = new RptViewReportSecurity();
                ////Modified by allen on 20081104 for change security: functiongroup
                //security.UserGroupCode = selectedUserGroup[i];
                security.FunctionGroupCode = selectedUserGroup[i];
                ////End Modified by allen on 20081104 for change security: functiongroup
                rptSecurity[i] = security;
            }
            this.designView.ReportSecurity = rptSecurity;

            this.designView.DesignMain.ParentReportFolder = this.drpReportFolder.SelectedValue;
        }

        #endregion




    }
}
