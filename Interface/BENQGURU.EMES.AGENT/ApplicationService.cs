using System;

namespace BenQGuru.eMES.Agent
{
	/// <summary>
	/// ApplicationService 的摘要说明。
	/// </summary>
	/// <summary>
	/// Service 的摘要说明。
	/// </summary>
	public class ApplicationService
	{
		private static ApplicationService _applicationService = null;
		private CollectAgent				_agentWindows = null;
		
		private ApplicationService()
		{
			
		}
		
		public static ApplicationService Current()
		{
			if (_applicationService == null)
			{
				_applicationService = new ApplicationService(); 
			}
			return _applicationService;
		}


		public CollectAgent AgentWindows
		{
			get
			{
				return _agentWindows;
			}
			set
			{
				_agentWindows = value;
			}
		}

		
		public string Language
		{
			get
			{
				return _language;
			}
			set
			{
				if("CHS"==value || "CHT"==value || "ENU"==value)
				{
					_language = value; 
				}
			}
		}
		private string _language = "CHS";

		public void CloseAllMdiChildren()
		{
			foreach( System.Windows.Forms.Form form in this.AgentWindows.MdiChildren )
			{
				form.Close();
			}
		}
	}
}
