#region system
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
using Infragistics.WebUI.UltraWebGrid;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.WebQuery;
#endregion

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// ReportCenterViewConfigEP 的摘要说明。
	/// </summary>
	public partial class ReportCenterViewConfigEP : BasePage
	{
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		
		private BenQGuru.eMES.WebQuery.ReportViewConfigFacade viewFacade;


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

		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.IsPostBack == false)
			{
				this.InitList();
			}
		}

		private void InitList()
		{
			if (viewFacade == null)
				viewFacade = new ReportViewConfigFacade(this.DataProvider);
			this.txtSelected.Value = ";";
			this.lstSelected.Items.Clear();
			object[] objs = viewFacade.GetReportCenterViewByUser(this.GetUserCode());
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					ReportCenterView viewField = (ReportCenterView)objs[i];
					string strText = languageComponent1.GetString(viewField.ReportCode);
					lstSelected.Items.Add(new ListItem(strText, viewField.ReportCode));
					txtSelected.Value += viewField.ReportCode + ";";
				}
			}
			objs = viewFacade.GetReportCenterViewDefault();
			lstUnSelected.Items.Clear();
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					ReportCenterView viewField = (ReportCenterView)objs[i];
					if (this.txtSelected.Value.IndexOf(";" + viewField.ReportCode + ";") < 0)
					{
						string strText = languageComponent1.GetString(viewField.ReportCode);
						lstUnSelected.Items.Add(new ListItem(strText, viewField.ReportCode));
					}
				}
			}
		}

		protected void cmdSave_ServerClick(object sender, EventArgs e)
		{
			if (viewFacade == null)
				viewFacade = new ReportViewConfigFacade(this.DataProvider);
			viewFacade.UpdateReportCenterViewList(this.GetUserCode(), this.txtSelected.Value);
			this.Page.RegisterStartupScript("close_window", "<script>window.returnValue='OK';window.close();</script>");
		}
	}
}
