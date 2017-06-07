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

using BenQGuru.eMES.Dashboard;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.Web.Dashboard
{
	/// <summary>
	/// WebForm1 的摘要说明。
	/// </summary>
	public partial class MTTR : Page
	{
		private DashboardFacade dbFAC = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				GetMTTR();
			}
		}

		#region GetMTTR
		public void GetMTTR()
		{
			string modelCode = String.Empty;
			string itemCode = String.Empty;
			string orderCode = String.Empty;
			string frmDate = String.Empty;
			string toDate = String.Empty;
			string frmMonth = String.Empty;
			string toMonth = String.Empty;
			string frmWeek = String.Empty;
			string toWeek = String.Empty;
			string statisticlatitude = String.Empty;
			string filterfield = String.Empty;
						

			if(this.Request.QueryString["modelcode"] != null)
			{
				modelCode = this.Request.QueryString["modelcode"];
			}

						
			if(this.Request.QueryString["itemcode"] != null)
			{
				itemCode = this.Request.QueryString["itemcode"];
			}

						
			if(this.Request.QueryString["ordercode"] != null)
			{
				orderCode = this.Request.QueryString["ordercode"];
			}

						
			if(this.Request.QueryString["fromdate"] != null)
			{
				frmDate = this.Request.QueryString["fromdate"];
			}

						
			if(this.Request.QueryString["todate"] != null)
			{
				toDate = this.Request.QueryString["todate"];
			}

						
			if(this.Request.QueryString["frommonth"] != null)
			{
				frmMonth = this.Request.QueryString["frommonth"];
			}

			if(this.Request.QueryString["tomonth"] != null)
			{
				toMonth = this.Request.QueryString["tomonth"];
			}

			if(this.Request.QueryString["fromweek"] != null)
			{
				frmWeek = this.Request.QueryString["fromweek"];
			}

			if(this.Request.QueryString["toweek"] != null)
			{
				toWeek = this.Request.QueryString["toweek"];
			}

			if(this.Request.QueryString["statisticlatitude"] != null)
			{
				statisticlatitude = this.Request.QueryString["statisticlatitude"];
			}

			if(this.Request.QueryString["filterfield"] != null)
			{
				filterfield = this.Request.QueryString["filterfield"];
			}

			if(dbFAC == null)
			{
				dbFAC = (new FacadeFactory()).CreateDashboardFacade();;
			}
			string xmlContent = dbFAC.getMTTR(
				modelCode
				,itemCode
				,orderCode
				,frmDate
				,toDate
				,frmMonth
				,toMonth
				,frmWeek
				,toWeek
				,statisticlatitude
				,filterfield);

			this.Response.Clear();
			this.Response.ClearContent();
			this.Response.ClearHeaders();

			this.Response.Write(xmlContent);
		}
		#endregion

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
		}
		#endregion
	}
}
