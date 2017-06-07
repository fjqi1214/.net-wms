using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.WatchPanelNew
{
    public class ApplicationService
    {
        private static ApplicationService _applicationService = null;
        private static IDomainDataProvider _dataProvider;

        private ApplicationService(IDomainDataProvider _dataProvider)
        {
            if (_dataProvider != null) 
                _dataProvider = _dataProvider;
        }
        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_dataProvider == null)
                {

                    _dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();
                }

                return _dataProvider;
            }

            set
            {
                _dataProvider = value;
            }
        }

        public static ApplicationService Current()
        {
            if (_applicationService == null)
            {
                if (_dataProvider == null)
                {
                    _dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();
                }
                _applicationService = new ApplicationService(_dataProvider);
            }
            return _applicationService;
        }
    }
}
