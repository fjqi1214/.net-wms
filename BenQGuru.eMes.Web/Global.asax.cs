using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using BenQGuru.eMES.Web.Helper;
using System.IO;

namespace BenQuru.eMES.Web 
{
	/// <summary>
	/// Global 的摘要说明。
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
//			WebStatisical.Instance().AddStatisicalItem("User");

			//sammer kong 2005/04/21 register some variable
			InternalSystemVariable.Register(new MOManufactureStatus());
			InternalSystemVariable.Register(new BOMItemControlType());
			InternalSystemVariable.Register(new ItemControlType());
			InternalSystemVariable.Register(new ReworkStatus());
			InternalSystemVariable.Register(new ReworkType());
			InternalSystemVariable.Register(new ModuleStatus());
			InternalSystemVariable.Register(new ModuleType());
			InternalSystemVariable.Register(new MenuType());
			InternalSystemVariable.Register(new MOType());
			InternalSystemVariable.Register(new UserGroupType());
			InternalSystemVariable.Register(new ResourceType());
            InternalSystemVariable.Register(new RejectStatus());
			InternalSystemVariable.Register(new RouteType());
            InternalSystemVariable.Register(new MOProductType());

			/*added by jessie lee
			 * At the beginning of Application,clear the directory upload
			 */
			try
			{
				string pDir = Server.MapPath("upload") ;
				if(Directory.Exists(pDir))
				{
					Directory.Delete(pDir,true);
				}
				Directory.CreateDirectory(pDir);
			}
			catch
			{
			}
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

