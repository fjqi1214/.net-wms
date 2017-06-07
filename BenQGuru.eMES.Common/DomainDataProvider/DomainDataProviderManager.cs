using System;
using BenQGuru.eMES.Common.Domain;   
using BenQGuru.eMES.Common.PersistBroker;

namespace BenQGuru.eMES.Common.DomainDataProvider
{
	/// <summary>
	/// DomainDataProviderManager 的摘要说明。
	/// </summary>
	public class DomainDataProviderManager
	{
		public DomainDataProviderManager()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static IDomainDataProvider  DomainDataProvider()
		{
			return DomainDataProviderManager.DomainDataProvider(null, new System.Globalization.CultureInfo("en-US", false)); 
		}

		public static IDomainDataProvider  DomainDataProvider(System.Globalization.CultureInfo  cultureInfo)
		{
			return DomainDataProviderManager.DomainDataProvider(null, cultureInfo); 
		}

		public static IDomainDataProvider  DomainDataProvider(IPersistBroker persistBroker, System.Globalization.CultureInfo  cultureInfo)
		{
			if (cultureInfo == null)
			{
				cultureInfo = new System.Globalization.CultureInfo("en-US", false);
			}
			if (persistBroker == null)
			{
				return new SQLDomainDataProvider(PersistBrokerManager.PersistBroker(cultureInfo), cultureInfo);
			}
			else
			{
				return new SQLDomainDataProvider(persistBroker, cultureInfo); 
			}
		}
		public static IDomainDataProvider  DomainDataProvider(BenQGuru.eMES.Common.Domain.DBName ConnectDB)
		{
            //modifed by carey.cheng on 2010-05-20 for muti db support
            //return new SQLDomainDataProvider(PersistBrokerManager.PersistBroker(ConnectDB), new System.Globalization.CultureInfo("en-US", false));
            return DomainDataProvider(ConnectDB.ToString());
            //end modifed by carey.cheng on 2010-05-20 for muti db support
		}

        //added by carey.cheng on 2010-05-20 for muti db support
        public static IDomainDataProvider DomainDataProvider(string ConnectDB)
        {
            return new SQLDomainDataProvider(PersistBrokerManager.PersistBroker(ConnectDB), new System.Globalization.CultureInfo("en-US", false));
        }
        //end added by carey.cheng on 2010-05-20 for muti db support
	}
}
