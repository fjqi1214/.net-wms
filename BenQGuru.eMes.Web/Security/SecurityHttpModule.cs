using System;
using System.Web;
using System.Web.SessionState;

using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.Security
{
	/// <summary>
	/// SecurityHttpModule 的摘要说明。
	/// </summary>
	public class SecurityHttpModule : IHttpModule,IRequiresSessionState
	{
		public SecurityHttpModule()
		{
		}

		public void Init(HttpApplication application) 
		{			
			application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
			application.EndRequest += (new EventHandler(this.Application_EndRequest));
			application.AcquireRequestState +=new EventHandler(application_AcquireRequestState);
			application.Error +=new EventHandler(application_Error);
		}

		// Your BeginRequest event handler.
		private void Application_BeginRequest(Object source, EventArgs e) 
		{	
		}

		private string _getRequestUrl(HttpApplication application)
		{			
			HttpContext context = application.Context;			
			string requestUrl = context.Request.Path ;
			string appUrl = context.Request.ApplicationPath;

			if( appUrl.Length > 0 )
			{
				requestUrl = requestUrl.Remove(0,appUrl.Length);
			}
			if( requestUrl.StartsWith("/") )
			{
				requestUrl = requestUrl.Substring(1,requestUrl.Length-1);
			}

			return requestUrl;
		}

		

		#region Access Rights Check
		private void checkRights(string url,HttpSessionState session)
		{
		}

		private bool is_need_rights_check(string pageCode)
		{
			return true;
		}
		#endregion
    
		// Your EndRequest event handler.
		private void Application_EndRequest(Object source, EventArgs e) 
		{
		}        
    
		public void Dispose() 
		{
		}

		/// <summary>
		/// sammer kong20050408
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void application_AcquireRequestState(object sender, EventArgs e)
		{
			HttpApplication application = (HttpApplication)sender;
		}

		private void application_Error(object sender, EventArgs e)
		{
			HttpApplication application = (HttpApplication)sender;		
			
			Exception ex = application.Server.GetLastError();	
			if( ex is AccessCheckException )
			{
				string url = application.Request.ApplicationPath + "/module/ErrorPage.aspx" ;		
				string errMsg = application.Server.UrlPathEncode(ex.Message.Replace(Environment.NewLine, "$"));
				url += "?msg=" + errMsg;		
				application.Server.ClearError();
				application.Response.Redirect(url );
			}
			if( ex is SessionExpiredException )
			{
				string path = "default.aspx";
				if( this._getRequestUrl(application).ToUpper() != "SQCSTARTPAGE.ASPX")
				{
					path = "../../" + path;
				}			
				else
				{
					path = "./" + path;
				}
				application.Session.RemoveAll();
				application.Response.Write("<script language=javascript>window.top.location.href='" + path + "'</script>");		
				application.Response.End();
			}
		}
	}

	class AccessCheckException : Exception
	{
		public AccessCheckException(string msg) : base(msg)
		{
		}
	}

	class SessionExpiredException : Exception
	{
		public SessionExpiredException(string msg) : base(msg)
	{
	}
	}
}
