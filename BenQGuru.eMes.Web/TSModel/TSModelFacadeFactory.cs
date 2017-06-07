using System;

namespace BenQGuru.eMES.Web.TSModel
{
	/// <summary>
	/// TSModelFacadeFactory 的摘要说明。
	/// </summary>
	public class TSModelFacadeFactory
	{
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = null;

		public TSModelFacadeFactory(BenQGuru.eMES.Common.Domain.IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

        public  BenQGuru.eMES.TSModel.TSModelFacade CreateTSModelFacade()
        {
            return new BenQGuru.eMES.TSModel.TSModelFacade(_domainDataProvider) ;
        }
	}
}
