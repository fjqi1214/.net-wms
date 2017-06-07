using System;
using BenQGuru.eMES.Common.Domain ;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// BaseModelFacadeFactory 的摘要说明。
	/// </summary>
	public class BaseModelFacadeFactory
	{
		private IDomainDataProvider _dataProvider = null ;

		public BaseModelFacadeFactory(IDomainDataProvider dataProvider)
		{
			
			_dataProvider = dataProvider ;
		
		}

		public BenQGuru.eMES.BaseSetting.BaseModelFacade Create()
		{
			return new BenQGuru.eMES.BaseSetting.BaseModelFacade(_dataProvider);
		}
              
        public BenQGuru.eMES.BaseSetting.EsopPicsFacade CreateEsopPicsFacade()
		{
            return new BenQGuru.eMES.BaseSetting.EsopPicsFacade(_dataProvider);
		}
	}
}
