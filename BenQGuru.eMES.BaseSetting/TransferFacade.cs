using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.BaseSetting
{
    public class TransferFacade : MarshalByRefObject
    {
        public const string MaterialTransferJobID = "MaterialTransfer";
        public const string MOHeaderTransferJobID = "MOHeaderTransfer";
        public const string MOMaterialTransferJobID = "MOMaterialTransfer";
        public const string StandardBOMTransferJobID = "StandardBOM";
        public const string MOCompleteTransferJobID = "MOComplete";
        public const string DNTransferJobID = "DNTransfer";
        public const string DNConfirmJobID = "DNConfirm";
        public const string InvertoryJobID = "InventoryTransfer";
        public const string MaterialPOJobID = "MaterialPOTransfer";
        public const string MaterialIssueJobID = "MaterialIssueTransfer";

        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public TransferFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        public TransferFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        public SAPDataTransferLog CreateNewSAPDataTransferLog()
        {
            return new SAPDataTransferLog();
        }

        public void AddSAPDataTransferLog(SAPDataTransferLog log)
        {
            this.DataProvider.Insert(log);
        }

        public void UpdateSAPDataTransferLog(SAPDataTransferLog log)
        {
            this.DataProvider.Update(log);
        }

        public void DeleteSAPDataTransferLog(SAPDataTransferLog log)
        {
            this.DataProvider.Delete(log);
        }

        public object GetSAPDataTransferLog(string transactionCode, int transactionSequence)
        {
            return this.DataProvider.CustomSearch(typeof(SAPDataTransferLog), new object[] { transactionCode, transactionSequence });
        }
    }
}
