using System;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// ItemFacadeFactory 的摘要说明。
	/// </summary>
	public class ItemFacadeFactory
	{
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = null;

		public ItemFacadeFactory(BenQGuru.eMES.Common.Domain.IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public ItemFacade Create()
		{
			return new ItemFacade(_domainDataProvider);
		}

        public BenQGuru.eMES.BaseSetting.SystemSettingFacade CreateSystemSettingFacade()
        {
            return new BenQGuru.eMES.BaseSetting.SystemSettingFacade(_domainDataProvider);
        }
	}
}
