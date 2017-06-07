using System;
using BenQGuru.eMES.Rework;

namespace BenQGuru.eMES.Web.Rework
{
	/// <summary>
	/// DataModelFacadeFactory 的摘要说明。
	/// </summary>
    public class ReworkFacadeFactory
    {
		private BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = null;

		public ReworkFacadeFactory(BenQGuru.eMES.Common.Domain.IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

        public  BenQGuru.eMES.Rework.ReworkFacade Create()
        {
            return new BenQGuru.eMES.Rework.ReworkFacade(_domainDataProvider);
        }

        public  BenQGuru.eMES.BaseSetting.UserFacade CreateUserManager()
        {
            return new BenQGuru.eMES.BaseSetting.UserFacade(_domainDataProvider);
        }


        public  BenQGuru.eMES.BaseSetting.SystemSettingFacade CreateSystemSettingFacade()
        {
            return new BenQGuru.eMES.BaseSetting.SystemSettingFacade(_domainDataProvider);
        }

        public  BenQGuru.eMES.MOModel.MOFacade CreateMOFacade()
        {
            return new BenQGuru.eMES.MOModel.MOFacade(_domainDataProvider);
        }

        public  BenQGuru.eMES.MOModel.OPBOMFacade CreateOPBOMFacade()
        {
            return new BenQGuru.eMES.MOModel.OPBOMFacade(_domainDataProvider) ;
        }

        public  BenQGuru.eMES.BaseSetting.BaseModelFacade CreateBaseModelFacade()
        {
            return new BenQGuru.eMES.BaseSetting.BaseModelFacade (_domainDataProvider);
        }

        public  BenQGuru.eMES.MOModel.ItemFacade CreateItemFacade()
        {
            return new BenQGuru.eMES.MOModel.ItemFacade (_domainDataProvider) ;
        }

        public  BenQGuru.eMES.MOModel.ModelFacade CreateModelFacade()
        {
            return new BenQGuru.eMES.MOModel.ModelFacade (_domainDataProvider) ;
        }

    }
}
