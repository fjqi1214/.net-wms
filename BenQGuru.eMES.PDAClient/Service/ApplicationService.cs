using System;
using System.ComponentModel;
using System.Windows.Forms;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
//using Microsoft.Practices.EnterpriseLibrary;
//using Microsoft.Practices.EnterpriseLibrary.Caching;
//using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace BenQGuru.eMES.PDAClient.Service
{
	/// <summary>
	/// Service 的摘要说明。
	/// </summary>
	public class ApplicationService
	{
		public const string MesAgent = "BenQGuru.eMES.Agent.exe";
		public const string DCTServer = "DCTControlPanel.exe";

		private static ApplicationService _applicationService = null;
		private  BenQGuru.eMES.PDAClient.Service.MenuService  _menuSerivce = null;
		private FMain				_mainWindows = null;
		private static IDomainDataProvider _dataProvider;
//		private CacheManager	_cacheManager = null;

		private ApplicationService(IDomainDataProvider _dataProvider)
		{
            if(_dataProvider != null) //added by carey.cheng on 2010-05-20 for muti db support
                _menuSerivce = new MenuService(_dataProvider); ;
		}

		public IDomainDataProvider DataProvider
		{
			get
			{
				if(_dataProvider == null)
				{

					_dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();
				}

				return _dataProvider;
			}

			set
			{
				_dataProvider = value;
			}
		}
		
		public static ApplicationService Current()
		{
			if (_applicationService == null)
			{
				if(_dataProvider == null)
				{
					_dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();
				}
				_applicationService = new ApplicationService(_dataProvider); 
			}
			return _applicationService;
		}

        //added by carey.cheng on 2010-05-20 for muti db support
        public static ApplicationService Login(string ConnectDB)
        {
            _dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(ConnectDB);
            _applicationService = new ApplicationService(_dataProvider);
            return _applicationService;
        }
        //end added by carey.cheng on 2010-05-20 for muti db support

		public BenQGuru.eMES.PDAClient.Service.MenuService MenuService
		{
			get
			{
				return _menuSerivce;
			}
		}

		public FMain MainWindows
		{
			get
			{
				return _mainWindows;
			}
			set
			{
				_mainWindows = value;
			}
		}

		private LoginInfo loginInfo = null;
		public LoginInfo LoginInfo
		{
			get
			{
				return this.loginInfo;
			}
			set
			{
				this.loginInfo = value;

//				this.MainWindows.SetLoginInfo( this.loginInfo );
			}
		}

		public string UserCode
		{
			get
			{
				if ( LoginInfo == null )
				{
					return null;
				}

				return LoginInfo.UserCode;
			}
		}

		public string ResourceCode
		{
			get
			{
				if ( LoginInfo == null )
				{
					return null;
				}

				return LoginInfo.Resource.ResourceCode;
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

//		public CacheManager CacheManager
//		{
//			get
//			{
//				if (_cacheManager == null)
//				{
//					_cacheManager = CacheFactory.GetCacheManager();
//				}
//				return _cacheManager; 
//			}
//		}

		public void CloseAllMdiChildren()
		{
			foreach( System.Windows.Forms.Form form in this.MainWindows.MdiChildren )
			{
				form.Close();
			}
		}

		public static void AutoUpdate(string location,string user,string password)
		{
			System.Diagnostics.Process pr = new System.Diagnostics.Process();

			System.Diagnostics.ProcessStartInfo prStart = new System.Diagnostics.ProcessStartInfo();

			prStart.FileName = "AutoUpdate.exe";

			prStart.Arguments = location + " " + AppDomain.CurrentDomain.FriendlyName;

			pr.StartInfo = prStart;

			System.Diagnostics.Process prAgent = ApplicationRun.RunningInstance(MesAgent);

			//System.Diagnostics.Process prDCT = ApplicationRun.RunningInstance(DCTServer);

			pr.Start();

			if( prAgent != null)
			{
				prAgent.Kill();
			}

            // Marked By Hi1/Venus.Feng on 20080908 for Hisense Version : not need to kill DCT in CS
            // The Updater will Kill it
            //if( prDCT != null)
            //{
            //    prDCT.Kill();
            //}
            // End Marked
			Application.Exit();
		}

		public static object CheckUpdate()
		{
			try
			{
				//object objUpdater = null;
				//Laws Lu,2005/08/22,新增	版本更新
				object objUpdater = FormatHelper.GetCsVersion(ApplicationService.Current().DataProvider);

				string strVersion = UserControl.FileLog.GetLocalCSVersion(UserControl.FileLog.VersionFileName);
				int iErrorCount = 0;
				if(objUpdater != null)
				{
					iErrorCount = FormatHelper.GetUpdateErrorCount(ApplicationService.Current().DataProvider,((BenQGuru.eMES.Web.Helper.Updater)objUpdater) .CSVersion);
				}
				//Laws Lu,2006/08/14 修改	如果更新有错误也不允许继续操作
				if ((objUpdater != null 
					&& strVersion != ((BenQGuru.eMES.Web.Helper.Updater)objUpdater) .CSVersion.Trim())
					||  (objUpdater != null && iErrorCount != 0  
					&& strVersion == ((BenQGuru.eMES.Web.Helper.Updater)objUpdater) .CSVersion.Trim()))
				{
					return objUpdater;
					//				bResult = true;
					//				Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
					//				
					//				AutoUpdate(upd.Location.Trim(),upd.LoginUser,upd.LoginPassword);
				}
			}
			catch(Exception ex)
			{
				Common.Log.Error(ex.Message);
			}

			return null;
		}
	}
}
