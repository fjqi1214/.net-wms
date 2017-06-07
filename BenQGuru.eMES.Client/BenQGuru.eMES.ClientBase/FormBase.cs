using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.ClientBase
{
    public class FormBase : Form
    {
        public FormBase()
        {
        }

        private static IDomainDataProvider _dataProvider;
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

    }
}
