using System;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.RealTimeReport;

namespace BenQGuru.eMES.Web.RealTimeReport
{
	/// <summary>
	/// FacadeFactory 的摘要说明。
	/// </summary>
	public class FacadeFactory
	{
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _dataProvider = null;

		public BenQGuru.eMES.Common.Domain.IDomainDataProvider DataProvider
		{
			get
			{
				if (_dataProvider == null)
				{
					_dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();
				}
				return _dataProvider;
			}
		}

		public FacadeFactory()
		{
		}

		public FacadeFactory(BenQGuru.eMES.Common.Domain.IDomainDataProvider dataProvider)
		{
				_dataProvider = dataProvider;
		}

		public BaseModelFacade CreateBaseModelFacade()
		{
			if (_dataProvider != null)
			{
				return new BaseModelFacade(this.DataProvider);
			}
			else
			{
				return new BaseModelFacade();
			}
		}

		public ModelFacade CreateModelFacade()
		{
			
			if (_dataProvider != null)
			{
				return new ModelFacade(this.DataProvider);
			}
			else
			{
				return new ModelFacade();
			}
		}

		public MOFacade CreateMOFacade()
		{
			
			if (_dataProvider != null)
			{
				return new MOFacade(this.DataProvider);
			}
			else
			{
				return new MOFacade();
			}
		}

		public ItemFacade CreateItemFacade()
		{
			
			if (_dataProvider != null)
			{
				return new ItemFacade(this.DataProvider);
			}
			else
			{
				return new ItemFacade();
			}
		}

		public ShiftModelFacade CreateShiftModelFacade()
		{
			
			if (_dataProvider != null)
			{
				return new ShiftModelFacade(this.DataProvider);
			}
			else
			{
				return new ShiftModelFacade();
			}
		}

		
		public  ReportFacade CreateReportFacade()
		{
			
			if (_dataProvider != null)
			{
				return new ReportFacade(this.DataProvider);
			}
			else
			{
				return new ReportFacade();
			}
		}
	}
}
