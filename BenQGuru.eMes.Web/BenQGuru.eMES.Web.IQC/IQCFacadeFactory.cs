using System;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.IQC;


namespace BenQGuru.eMES.Web.IQC
{
    public class IQCFacadeFactory
    {
        private IDomainDataProvider _domainDataProvider = null;

        public IQCFacadeFactory(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

        public IQCFacade CreateIQCFacade()
        {
            return new IQCFacade(_domainDataProvider);
        }
    }
}
