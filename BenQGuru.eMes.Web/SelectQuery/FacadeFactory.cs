using System;

namespace BenQGuru.eMES.Web.SelectQuery
{
	/// <summary>
	/// FacadeFactory 的摘要说明。
	/// </summary>
	public class FacadeFactory
	{
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = null;

		public FacadeFactory(BenQGuru.eMES.Common.Domain.IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

        public  BenQGuru.eMES.SelectQuery.SPFacade CreateSPFacade()
        {
            return new BenQGuru.eMES.SelectQuery.SPFacade(_domainDataProvider) ;
        }
	}
}
