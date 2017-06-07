using System;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// ItemFacadeFactory 的摘要说明。
	/// </summary>
	public class FacadeFactory
	{
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = null;

		public FacadeFactory(BenQGuru.eMES.Common.Domain.IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public  ItemFacade CreateItemFacade()
		{
			return new ItemFacade(_domainDataProvider);
		}

		public  SBOMFacade CreateSBOMFacade()
		{
			return new SBOMFacade(_domainDataProvider);
		}

		public  MOFacade CreateMOFacade()
		{
			return new MOFacade(_domainDataProvider);
		}

		public  OPBOMFacade CreateOPBOMFacade()
		{
			return new OPBOMFacade(_domainDataProvider);
		}
		public  ModelFacade CreateModelFacade()
		{
			return new ModelFacade(_domainDataProvider);
		}
		public  OPItemControlFacade CreateOPItemControlFacade()
		{
			return new OPItemControlFacade(_domainDataProvider);
		}
		public  BaseModelFacade CreateBaseModelFacade()
		{
			return new BaseModelFacade(_domainDataProvider);
		}

		public  OrderFacade CreateOrderFacade()
		{
			return new OrderFacade(_domainDataProvider);
		}

		public  ShelfFacade CreateShelfFacade()
		{
			return new ShelfFacade(_domainDataProvider);
		}

		public RMAFacade CreateRMAFacade()
		{
			return new RMAFacade(_domainDataProvider);
		}

		public SPCFacade CreateSPCFacade()
		{
			return new SPCFacade(_domainDataProvider);
		}

		public OQCFacade CreateOQCFacade()
		{
			return new OQCFacade(_domainDataProvider);
		}
	}
}
