using System;
using BenQGuru.eMES.Common.Domain ;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// UserManagerFactory 的摘要说明。
	/// </summary>
	public class UserManagerFactory
	{
		private IDomainDataProvider _dataProvider = null ;

		public UserManagerFactory(IDomainDataProvider dataProvider)
		{
			_dataProvider = dataProvider ;
			
		}

		public UserFacade CreateUserManager()
		{
			return new UserFacade( _dataProvider );
		}
	}
}
