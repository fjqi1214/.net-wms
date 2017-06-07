using System;
using System.Web;
using System.Web.SessionState;

using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.Security
{
	/// <summary>
	/// StatisicalHttpModule 的摘要说明。
	/// </summary>
	public class StatisicalHttpModule : IHttpModule,IRequiresSessionState
	{
		public StatisicalHttpModule()
		{
		}

		public void Init(HttpApplication application) 
		{	
			application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
			application.EndRequest += (new EventHandler(this.Application_EndRequest));
		}

		private void Application_BeginRequest(Object source, EventArgs e) 
		{	
			ResponseStatisical.Instance().SetBeginRequestTime( source as HttpApplication );
		}

		private void Application_EndRequest(Object source, EventArgs e) 
		{
			ResponseStatisical.Instance().SetEndRequestTime( source as HttpApplication );
		}   
	
		public void Dispose() 
		{
		}
	}
}
