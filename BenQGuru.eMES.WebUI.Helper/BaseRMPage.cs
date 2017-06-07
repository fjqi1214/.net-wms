using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
//using BenQGuru.eMES.Web.Security;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// BaseRPage 的摘要说明。
	/// </summary>
	public class BaseRMPage : BaseMPage
	{
		public BaseRMPage() : base()
		{
		}

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{	
			this.Load +=new EventHandler(BaseRPage_Load);
			this.Unload +=new EventHandler(BaseRPage_Unload);
		}

		public override string StyleSheet
		{
			get
			{
				string reportStyle = "STYLE2.0.CSS";
				if(System.Configuration.ConfigurationSettings.AppSettings["ReportStyle"] != null
					&& System.Configuration.ConfigurationSettings.AppSettings["ReportStyle"].Trim() != String.Empty)
				{
					reportStyle = System.Configuration.ConfigurationSettings.AppSettings["ReportStyle"].Trim();
				}
				return string.Format("{0}{1}"
					, this.VirtualHostRoot
					, @"Skin/" + reportStyle);
			}
		}


//		protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
//		{
//#if DEBUG
//			ResponseStatisical.Instance().SetBeginRequestTime( this,SessionHelper.Current(this.Session).ModuleCode);
//#endif
//
//			base.RaisePostBackEvent (sourceControl, eventArgument);
//#if DEBUG
//			ResponseStatisical.Instance().SetEndRequestTime( this,SessionHelper.Current(this.Session).ModuleCode);
//
//			ResponseStatisical.Instance().Write();
//#endif
//
//		}


		private void BaseRPage_Unload(object sender, EventArgs e)
		{
		}

		private void BaseRPage_Load(object sender, EventArgs e)
		{
		}
	}
}
