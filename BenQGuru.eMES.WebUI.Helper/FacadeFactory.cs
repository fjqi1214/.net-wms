using System;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// SecurityFacadeFactory 的摘要说明。
	/// </summary>
	public class FacadeFactory
	{
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = null;

		public FacadeFactory(BenQGuru.eMES.Common.Domain.IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public BenQGuru.eMES.Security.SecurityFacade CreateSecurityFacade()
		{
			return new BenQGuru.eMES.Security.SecurityFacade(_domainDataProvider);
		}

		public BenQGuru.eMES.BaseSetting.SystemSettingFacade CreateSystemSettingFacade()
		{
			return new BenQGuru.eMES.BaseSetting.SystemSettingFacade(_domainDataProvider);
		}
	}
}
