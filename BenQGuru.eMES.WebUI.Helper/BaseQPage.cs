using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
//using BenQGuru.eMES.Web.Security;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// BaseQPage 的摘要说明。
	/// </summary>
	public class BaseQPage : BasePage
	{
		public BaseQPage() : base()
		{
		}

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{	
			this.Load +=new EventHandler(BaseQPage_Load);
			this.Unload +=new EventHandler(BaseQPage_Unload);
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


		private void BaseQPage_Unload(object sender, EventArgs e)
		{
		}

		private void BaseQPage_Load(object sender, EventArgs e)
		{
		}
	}
}
