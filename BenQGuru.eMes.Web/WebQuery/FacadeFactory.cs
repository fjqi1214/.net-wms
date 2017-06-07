using System;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.DataLink;
using BenQGuru.eMES.Report;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FacadeFactory 的摘要说明。
	/// </summary>
	public class FacadeFactory
	{
		private IDomainDataProvider _dataProvider = null ;
		public FacadeFactory(IDomainDataProvider dataProvider)
		{
			_dataProvider = dataProvider ;
		}

		public BaseModelFacade CreateBaseModelFacade()
		{
			return new BaseModelFacade(_dataProvider); 
		}

		public ShiftModelFacade CreateShfitModelFacade()
		{
			return new ShiftModelFacade(_dataProvider);
		}

		public ModelFacade CreateModelFacade()
		{
			return new ModelFacade(_dataProvider);
		}

		public MOFacade CreateMOFacade()
		{
			return new MOFacade(_dataProvider);
		}

		public ItemFacade CreateItemFacade()
		{
			return new ItemFacade(_dataProvider);
		}

		public WarehouseFacade CreateWarehouseFacade()
		{
			return new WarehouseFacade(_dataProvider);
		}

		public InventoryFacade CreateInventoryFacade()
		{
			return new InventoryFacade(_dataProvider);
		}

		public  QueryFacade1 CreateQueryFacade1()
		{
			return new QueryFacade1(_dataProvider);
		}

        public  QueryFacade2 CreateQueryFacade2()
        {
            return new QueryFacade2(_dataProvider);
		}

		public  QueryFacade3 CreateQueryFacade3()
		{
			return new QueryFacade3(_dataProvider);
		}

		public  QuerySMTComponentLoadingFacade CreateQuerySMTComponentLoadingFacade()
		{
			return new QuerySMTComponentLoadingFacade(_dataProvider);
		}

		public  QueryOQCLotDetailsFacade CreateQueryOQCLotDetailsFacade()
		{
			return new QueryOQCLotDetailsFacade(_dataProvider);
		}

		public  QuerySoftwareVersionFacade CreateQuerySoftwareVersionFacade()
		{
			return new QuerySoftwareVersionFacade(_dataProvider);
		}

		public  QueryCardTransferFacade CreateQueryCardTransferFacade()
		{
			return new QueryCardTransferFacade(_dataProvider);
		}

		public  QuerySMTNGFacade CreateQuerySMTNGFacade()
		{
			return new QuerySMTNGFacade(_dataProvider);
		}

		public QuerySNNGFacade CreateQuerySNNGFacade()
		{
			return new QuerySNNGFacade(_dataProvider);
		}

		public QueryECNFacade CreateQueryECNFacade()
		{
			return new QueryECNFacade(_dataProvider);
		}

		public QueryTryNoFacade CreateQueryTryNoFacade()
		{
			return new QueryTryNoFacade(_dataProvider);
		}

		public QueryComponentLoadingFacade CreateQueryComponentLoadingFacade()
		{
			return new QueryComponentLoadingFacade(_dataProvider);
		}

		public QueryTryItemFacade CreateQueryTryItemFacade()
		{
			return new QueryTryItemFacade(_dataProvider);
		}

		public QueryStockFacade CreateQueryStockFacade()
		{
			return new QueryStockFacade(_dataProvider);
		}

		public QueryTSRecordFacade CreateQueryTSRecordFacade()
		{
			return new QueryTSRecordFacade(_dataProvider);
		}

		public QueryTSPerformanceFacade CreateQueryTSPerformanceFacade()
		{
			return new QueryTSPerformanceFacade(_dataProvider);
		}

		public QueryTSInfoFacade CreateQueryTSInfoFacade()
		{
			return new QueryTSInfoFacade(_dataProvider);
		}

		public QueryTSChangedPartsFacade CreateQueryTSChangedPartsFacade()
		{
			return new QueryTSChangedPartsFacade(_dataProvider);
		}

		public QueryTSDetailsFacade CreateQueryTSDetailsFacade()
		{
			return new QueryTSDetailsFacade(_dataProvider);
		}

		public QueryItemConfigrationFacade CreateQueryItemConfigrationFacade()
		{
			return new QueryItemConfigrationFacade(_dataProvider);
		}

		public QueryMultiMOMemoFacade CreateQueryMultiMOMemoFacade()
		{
			return new QueryMultiMOMemoFacade(_dataProvider);
		}

		public QueryRMATSFacade CreateQueryRMATSFacade()
		{
			return new QueryRMATSFacade(_dataProvider);
		}

		public QuerySolderPasteFacade CreateQuerySolderPasteFacade()
		{
			return new QuerySolderPasteFacade(_dataProvider);
		}

		public ArmorPlateFacade CreateArmorPlateFacade()
		{
			return new ArmorPlateFacade(_dataProvider);
		}

		public DataLinkFacade CreateDataLinkFacade()
		{
			return new DataLinkFacade(_dataProvider);
		}

		public ReportFacade CreateReportFacade()
		{
			return new ReportFacade(_dataProvider);
		}
    }
}
