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
	/// FRptStyleDetailMP 的摘要说明。
	/// </summary>
	public partial class FRptStyleDetailMP : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.ReportView.ReportViewFacade _facade;
	
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
                this.InitPageLanguage(this.languageComponent1, false);
                this.txtStyleNameQuery.Text = Server.UrlDecode(this.Request.QueryString["stylename"]);
                InitStyleValue();
            }
		}

        private void InitStyleValue()
        {
            string styleId = this.GetRequestParam("styleid");
            _facade = new ReportViewFacade(this.DataProvider);
            RptViewReportStyleDetail[] styleDtls = _facade.GetRptViewReportStyleDetailByStyleID(decimal.Parse(styleId));
            if (styleDtls == null || styleDtls.Length == 0)
                return;
            for (int i = 0; i < styleDtls.Length; i++)
            {
                if (styleDtls[i].StyleType == ReportStyleType.Header)
                {
                    RptViewDataFormat format = (RptViewDataFormat)_facade.GetRptViewDataFormat(styleDtls[i].FormatID);
                    if (format != null)
                    {
                        this.hidHeaderRow.Value = _facade.BuildStyleValueFromDataFormat(format);
                        this.hidHeader_0.Value = this.hidHeaderRow.Value;
                        this.hidHeader_1.Value = this.hidHeaderRow.Value;
                        this.hidHeader_2.Value = this.hidHeaderRow.Value;
                    }
                }
                else if (styleDtls[i].StyleType == ReportStyleType.SubTotal)
                {
                    RptViewDataFormat format = (RptViewDataFormat)_facade.GetRptViewDataFormat(styleDtls[i].FormatID);
                    if (format != null)
                    {
                        this.hidGroup_0.Value = _facade.BuildStyleValueFromDataFormat(format);
                        this.hidGroupData_0_0.Value = this.hidGroup_0.Value;
                        this.hidGroupData_0_1.Value = this.hidGroup_0.Value;
                        this.hidGroupData_0_2.Value = this.hidGroup_0.Value;
                    }
                }
                else if (styleDtls[i].StyleType == ReportStyleType.Item)
                {
                    RptViewDataFormat format = (RptViewDataFormat)_facade.GetRptViewDataFormat(styleDtls[i].FormatID);
                    if (format != null)
                    {
                        this.hidItemHeader.Value = _facade.BuildStyleValueFromDataFormat(format);
                        this.hidItemData_0.Value = this.hidItemHeader.Value;
                        this.hidItemData_1.Value = this.hidItemHeader.Value;
                        this.hidItemData_2.Value = this.hidItemHeader.Value;
                    }
                }
            }
            for (int i = 0; i < styleDtls.Length; i++)
            {
                if (styleDtls[i].StyleType == ReportStyleType.SubTotalGroupField)
                {
                    RptViewDataFormat format = (RptViewDataFormat)_facade.GetRptViewDataFormat(styleDtls[i].FormatID);
                    if (format != null)
                    {
                        this.hidGroupData_0_0.Value = _facade.BuildStyleValueFromDataFormat(format);
                    }
                }
                else if (styleDtls[i].StyleType == ReportStyleType.SubTotalNonCalField)
                {
                    RptViewDataFormat format = (RptViewDataFormat)_facade.GetRptViewDataFormat(styleDtls[i].FormatID);
                    if (format != null)
                    {
                        this.hidGroupData_0_1.Value = _facade.BuildStyleValueFromDataFormat(format);
                    }
                }
                else if (styleDtls[i].StyleType == ReportStyleType.SubTotalCalField)
                {
                    RptViewDataFormat format = (RptViewDataFormat)_facade.GetRptViewDataFormat(styleDtls[i].FormatID);
                    if (format != null)
                    {
                        this.hidGroupData_0_2.Value = _facade.BuildStyleValueFromDataFormat(format);
                    }
                }
            }
        }
        #endregion

        void cmdSave_ServerClick(object sender, EventArgs e)
        {
            _facade = new ReportViewFacade(this.DataProvider);
            this.DataProvider.BeginTransaction();
            try
            {
                if (this.hidHeaderRow.Value != "")
                {
                    RptViewDataFormat format = _facade.BuildDataFormatByStyle(this.hidHeaderRow.Value);
                    format.MaintainUser = this.GetUserCode();
                    _facade.UpdateRptViewReportStyleDetail(decimal.Parse(this.GetRequestParam("styleid")), ReportStyleType.Header, format);
                }
                if (this.hidGroup_0.Value != "")
                {
                    RptViewDataFormat format = _facade.BuildDataFormatByStyle(this.hidGroup_0.Value);
                    format.MaintainUser = this.GetUserCode();
                    _facade.UpdateRptViewReportStyleDetail(decimal.Parse(this.GetRequestParam("styleid")), ReportStyleType.SubTotal, format);
                }
                if (this.hidItemHeader.Value != "")
                {
                    RptViewDataFormat format = _facade.BuildDataFormatByStyle(this.hidItemHeader.Value);
                    format.MaintainUser = this.GetUserCode();
                    _facade.UpdateRptViewReportStyleDetail(decimal.Parse(this.GetRequestParam("styleid")), ReportStyleType.Item, format);
                }
                if (this.hidGroupData_0_0.Value != "")
                {
                    RptViewDataFormat format = _facade.BuildDataFormatByStyle(this.hidGroupData_0_0.Value);
                    format.MaintainUser = this.GetUserCode();
                    _facade.UpdateRptViewReportStyleDetail(decimal.Parse(this.GetRequestParam("styleid")), ReportStyleType.SubTotalGroupField, format);
                }
                if (this.hidGroupData_0_1.Value != "")
                {
                    RptViewDataFormat format = _facade.BuildDataFormatByStyle(this.hidGroupData_0_1.Value);
                    format.MaintainUser = this.GetUserCode();
                    _facade.UpdateRptViewReportStyleDetail(decimal.Parse(this.GetRequestParam("styleid")), ReportStyleType.SubTotalNonCalField, format);
                }
                if (this.hidGroupData_0_2.Value != "")
                {
                    RptViewDataFormat format = _facade.BuildDataFormatByStyle(this.hidGroupData_0_2.Value);
                    format.MaintainUser = this.GetUserCode();
                    _facade.UpdateRptViewReportStyleDetail(decimal.Parse(this.GetRequestParam("styleid")), ReportStyleType.SubTotalCalField, format);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            finally
            {
                
            }
        }

        void cmdCancel_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect("FRptStyleMP.aspx");
        }

	}
}
