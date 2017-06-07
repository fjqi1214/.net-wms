using System;

using BenQGuru.eMES.Dashboard;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.Web.Dashboard
{
	/// <summary>
	/// ItemFacadeFactory 的摘要说明。
	/// </summary>
	public class FacadeFactory
	{
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = null;

		public FacadeFactory()
		{
			if(_domainDataProvider == null)
			{
				_domainDataProvider = DomainDataProviderManager.DomainDataProvider();
			}
		}

		public DashboardFacade CreateDashboardFacade()
		{
			return new DashboardFacade(_domainDataProvider);
		}

		public ModelFacade CreateModelFacade()
		{
			return new ModelFacade(_domainDataProvider);
		}
	}
}
