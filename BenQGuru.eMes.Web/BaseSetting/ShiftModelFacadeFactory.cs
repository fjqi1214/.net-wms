using System;
using BenQGuru.eMES.Common.Domain ;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// ShiftModelFacadeFactory 的摘要说明。
	/// </summary>
	public class ShiftModelFacadeFactory
	{
		private IDomainDataProvider _dataProvider = null ;
		public ShiftModelFacadeFactory(IDomainDataProvider dataProvider)
		{
			_dataProvider = dataProvider ;
		
		}

		public BenQGuru.eMES.BaseSetting.ShiftModelFacade Create()
		{
			return new BenQGuru.eMES.BaseSetting.ShiftModelFacade(_dataProvider);
		}
	}
}
