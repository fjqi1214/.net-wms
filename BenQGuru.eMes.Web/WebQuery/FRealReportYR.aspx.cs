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
using BenQGuru.eMES.Web.Helper;  

namespace BenQGuru.eMES.Web.RealTimeReport
{
	/// <summary>
	/// FRealReportYR 的摘要说明。
	/// </summary>
	public class FRealReportYR : BasePage
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面

			PhysicalLayoutDataPreparationHelper pHelper = new PhysicalLayoutDataPreparationHelper(null, this.drpSegmentCodeQuery, this.drpStepSequenceCodeQuery, null, this.drpShiftCodeQuery);
			pHelper.Load();

			RouteDataPreparationHelper rHelper =new RouteDataPreparationHelper(null, this.drpModelQuery, this.drpItemCodeQuery, this.drpMOCodeQuery, null);
			rHelper.Load();

			dateStartDateQuery.Enable  = "false";
			dateStartDateQuery.Date_DateTime = System.DateTime.Now;  

			this.BuildHtmlContent(); 
		}

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
			this.Submit2.ServerClick += new System.EventHandler(this.Submit2_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblSegmentCodeQuery;
		protected System.Web.UI.WebControls.DropDownList drpSegmentCodeQuery;
		protected System.Web.UI.WebControls.Label lblShiftDayQuery;
		protected System.Web.UI.WebControls.Label lblShiftCodeQuery;
		protected System.Web.UI.WebControls.DropDownList drpShiftCodeQuery;
		protected System.Web.UI.WebControls.Label lblStepSequenceCodeQuery;
		protected System.Web.UI.WebControls.DropDownList drpStepSequenceCodeQuery;
		protected System.Web.UI.WebControls.Label lblModelQuery;
		protected System.Web.UI.WebControls.DropDownList drpModelQuery;
		protected System.Web.UI.WebControls.Label lblItemCodeQuery;
		protected System.Web.UI.WebControls.DropDownList drpItemCodeQuery;
		protected System.Web.UI.WebControls.Label lblMOCodeQuery;
		protected System.Web.UI.WebControls.DropDownList drpMOCodeQuery;
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit2;
		protected System.Web.UI.WebControls.RadioButton rbLine;
		protected System.Web.UI.WebControls.Label lblTargetYR;
		protected System.Web.UI.WebControls.TextBox txtTargetYR;
		protected BenQGuru.eMES.Web.UserControl.eMESDate dateStartDateQuery;

		
		private string _htmlContent = string.Empty;
 
		public string HTMLContent
		{
			get
			{
				return _htmlContent;
			}
			set
			{
				_htmlContent = value;
			}
		}

		private void BuildHtmlContent()
		{
			try
			{
				System.Text.StringBuilder  sb = new System.Text.StringBuilder();

				BenQGuru.eMES.BaseSetting.BaseModelFacade baseModelFacade= new FacadeFactory().CreateBaseModelFacade();  
				object[] stepSequences = baseModelFacade.GetStepSequenceBySegmentCode(this.drpShiftCodeQuery.SelectedValue); 
			

					sb.Append("<tr>");
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "产线"));
					if (stepSequences != null)
					{
						for(int i=0; i<stepSequences.Length;i++)
						{
							sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", ((BenQGuru.eMES.Domain.BaseSetting.StepSequence)stepSequences[i]).StepSequenceCode));
						}
					}
					else
					{
						sb.Append(string.Format("<td class='gridWebGrid-hc'  width='100%'>{0}</td>\n", "&nbsp;"));
					}
					sb.Append("</tr>\n");

					sb.Append("<tr>");
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "直通台数"));
					if (stepSequences != null)
					{
						for(int i=0; i<stepSequences.Length;i++)
						{
							sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", 1));
						}
					}
					else
					{
							sb.Append(string.Format("<td class='gridWebGrid-ic' >{0}</td>\n", "&nbsp;"));
					}
					sb.Append("</tr>\n");

					sb.Append("<tr>");
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "总通过台数"));
					if (stepSequences != null)
					{
						for(int i=0; i<stepSequences.Length;i++)
						{
							sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", 2));
						}
					}
					else
					{
						sb.Append(string.Format("<td class='gridWebGrid-ic' >{0}</td>\n", "&nbsp;"));
					}
					sb.Append("</tr>\n");

					sb.Append("<tr>");
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "产线直通率"));
					if (stepSequences != null)
					{
						for(int i=0; i<stepSequences.Length;i++)
						{
							sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", 3));
						}
					}
					else
					{
						sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", "&nbsp;"));
					}
					sb.Append("</tr>\n");

					sb.Append("<tr>");
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "工段直通率"));
					if (stepSequences != null)
					{
						for(int i=0; i<stepSequences.Length;i++)
						{
							sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", 4));
						}
					}
					else
					{
						sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", "&nbsp;"));
					}
					sb.Append("</tr>\n");
				
				this.HTMLContent = sb.ToString(); 
			}
			catch(Exception e)
			{
				BenQGuru.eMES.Common.ExceptionManager.Raise(null, e.Message, e);      
			}
			
		}

		private void Submit2_ServerClick(object sender, System.EventArgs e)
		{
			this.BuildHtmlContent(); 
		}
	}
}
