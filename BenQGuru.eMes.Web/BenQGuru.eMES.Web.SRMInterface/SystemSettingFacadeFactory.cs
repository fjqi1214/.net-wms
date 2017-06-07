using System;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Security;
using BenQGuru.eMES.Common.Domain ;

namespace BenQGuru.eMES.Web.SRMInterface
{
	/// <summary>
	/// SystemSettingFacadeFactory 的摘要说明。
	/// </summary>
	public class SystemSettingFacadeFactory
	{
		private IDomainDataProvider _dataProvider = null ;

		public SystemSettingFacadeFactory(IDomainDataProvider dataProvider)
		{
			_dataProvider = dataProvider ;
			
		}

		public SystemSettingFacade Create()
		{
			return new SystemSettingFacade(_dataProvider);
		}

		public UserFacade CreateUserFacade()
		{
			return new UserFacade(_dataProvider);
		}

		public SecurityFacade CreateSecurityFacade()
		{
			return new SecurityFacade(_dataProvider);
		}

        public SystemSettingFacade CreateSystemSettingFacade()
        {
            return new SystemSettingFacade(_dataProvider);
        }
	}
}
