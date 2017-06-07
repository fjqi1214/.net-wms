using System;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// SMTFacadeFactory 的摘要说明。
	/// </summary>
	public class SMTFacadeFactory
	{
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = null;

		public SMTFacadeFactory(BenQGuru.eMES.Common.Domain.IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}
		
		public SMTFacade Create()
		{
			return new SMTFacade(_domainDataProvider);
		}
		
		public  BenQGuru.eMES.BaseSetting.BaseModelFacade CreateBaseModelFacadeFacade()
		{
			return new BenQGuru.eMES.BaseSetting.BaseModelFacade(_domainDataProvider);
		}

        public  BenQGuru.eMES.MOModel.MOFacade CreateMOFacade()
        {
            return new BenQGuru.eMES.MOModel.MOFacade(_domainDataProvider) ;
        }

        public  BenQGuru.eMES.MOModel.ItemFacade CreateItemFacade()
        {
            return new BenQGuru.eMES.MOModel.ItemFacade(_domainDataProvider) ;
        }

		public  BenQGuru.eMES.MOModel.OPBOMFacade CreateOPBOMFacade()
		{
			return new BenQGuru.eMES.MOModel.OPBOMFacade(_domainDataProvider) ;
		}
		
		public  BenQGuru.eMES.SMT.SolderPasteFacade CreateSolderPasteFacade()
		{
			return new BenQGuru.eMES.SMT.SolderPasteFacade(_domainDataProvider) ;
		}

		public  BenQGuru.eMES.SMT.ArmorPlateFacade CreateArmorPlateFacade()
		{
			return new BenQGuru.eMES.SMT.ArmorPlateFacade(_domainDataProvider) ;
		}
	}
}
