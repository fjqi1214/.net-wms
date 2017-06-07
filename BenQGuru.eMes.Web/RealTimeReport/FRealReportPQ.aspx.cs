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
using BenQGuru.eMES.RealTimeReport;


namespace BenQGuru.eMES.Web.RealTimeReport
{
	/// <summary>
	/// FRealReportPQ 的摘要说明。
	/// </summary>
	public partial class FRealReportPQ : BasePage
	{
		protected BenQGuru.eMES.Web.UserControl.eMESDate dateStartDateQuery;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			PhysicalLayoutDataPreparationHelper pHelper = new PhysicalLayoutDataPreparationHelper(null, this.drpSegmentCodeQuery, this.drpStepSequenceCodeQuery, null ,this.drpShiftCodeQuery);
			pHelper.Load();

			RouteDataPreparationHelper rHelper =new RouteDataPreparationHelper(null, this.drpModelQuery, this.drpItemCodeQuery, this.drpMOCodeQuery, null);
			rHelper.Load();

			dateStartDateQuery.Enable  = "false";
			dateStartDateQuery.Date_DateTime = System.DateTime.Now;   
     
			if(!this.IsPostBack)
			{
				this.BuildHtmlContent();
			}
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

		}
		#endregion

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

		protected void Submit1_ServerClick(object sender, System.EventArgs e)
		{
			this.BuildHtmlContent();
		}

		

		private void BuildHtmlContent()
		{
			try
			{
				System.Text.StringBuilder  sb = new System.Text.StringBuilder();

				ReportFacade reportFacade= new FacadeFactory(this.DataProvider).CreateReportFacade(); 
				BenQGuru.eMES.BaseSetting.ShiftModelFacade shiftModelFacade= new FacadeFactory(this.DataProvider).CreateShiftModelFacade();  
				object[] tps = shiftModelFacade.GetTimePeriodByShiftCode(this.drpShiftCodeQuery.SelectedValue); 
				object[] reports = reportFacade.QueryOPQty(this.drpSegmentCodeQuery.SelectedValue,
																			BenQGuru.eMES.Web.Helper.FormatHelper.TODateInt(this.dateStartDateQuery.Text), 
																			this.drpShiftCodeQuery.SelectedValue,
																			this.drpStepSequenceCodeQuery.SelectedValue,
																			this.drpItemCodeQuery.SelectedValue,
																			this.drpModelQuery.SelectedValue,
																			this.drpMOCodeQuery.SelectedValue); 
				if (tps == null)
				{
					sb.Append("<tr>");
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "机种"));
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "工单"));
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "产线"));
					sb.Append(string.Format("<td class='gridWebGrid-hc' colSpan='{0}' noWrap>{1}</td>\n", 1, "时段"));
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "汇总"));
					sb.Append("</tr>\n");
				}
				else
				{
					sb.Append("<tr>");
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "机种"));
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "工单"));
					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "产线"));
					//sb.Append(string.Format("<td class='gridWebGrid-hc' colSpan='{0}' noWrap>{1}</td>\n", tps.Length, "时段"));
					if (tps != null)
					{
						for(int i=0;i< tps.Length; i++)
						{
							sb.Append(string.Format("<td class='gridWebGrid-hc' noWrap>{0} ~ {1}</td>\n",
								BenQGuru.eMES.Web.Helper.FormatHelper.ToTimeString(((BenQGuru.eMES.Domain.BaseSetting.TimePeriod)tps[i]).TimePeriodBeginTime),
								BenQGuru.eMES.Web.Helper.FormatHelper.ToTimeString(((BenQGuru.eMES.Domain.BaseSetting.TimePeriod)tps[i]).TimePeriodEndTime)
								));
						}
					}

					sb.Append(string.Format("<td class='gridWebGrid-hc'>{0}</td>\n", "汇总"));
					sb.Append("</tr>\n");
				}

				sb.Append("<tr>");
				sb.Append(string.Format("<td class='gridWebGrid-hc' colSpan='{0}'>{1}</td>\n", 3, "汇总"));

				//输出产量数据 -B
			
				//如果没有数据
				if (reports == null)
				{
					if (tps != null)
					{
						for(int i=0;i< tps.Length; i++)
						{
							sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", 0));
						}
					}
					else
					{
						sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", 0));
					}
					sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", 0));
					sb.Append("</tr>\n");
				}
				//如果有数据
				else
				{
					//1. 按 Model 分组
					//2. 按 MO 分组
					//1. 按 Line 分组
					if (tps != null)
					{
						for(int i=0;i< tps.Length; i++)
						{
							sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", 0));
						}
					}
					else
					{
						sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", 0));
					}
					sb.Append(string.Format("<td class='gridWebGrid-ic'>{0}</td>\n", 0));
					sb.Append("</tr>\n");
				}

				//输出产量数据 -E
				
				this.HTMLContent = sb.ToString(); 
			}
			catch(Exception e)
			{
				BenQGuru.eMES.Common.ExceptionManager.Raise(null, e.Message, e);      
			}
			
		}
	}
}
