using System;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain ;   

namespace BenQGuru.eMES.Web.OQC
{
	/// <summary>
	/// OQCFacadeFactory 的摘要说明。
	/// </summary>
	public class OQCFacadeFactory
	{
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = null;

		public OQCFacadeFactory(BenQGuru.eMES.Common.Domain.IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

        public  BenQGuru.eMES.OQC.OQCFacade CreateOQCFacade()
        {
            return new BenQGuru.eMES.OQC.OQCFacade(_domainDataProvider) ;
        }
	}
}
