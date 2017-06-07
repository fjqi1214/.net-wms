using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.DBTransfer
{
    public class DBTransferFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public DBTransferFacade()
        {
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

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public DBTransferFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }


        #region TransferJob
        public TransferJob CreateNewTransferJob()
        {
            return new TransferJob();
        }

        public void AddTransferJob(TransferJob transferJob)
        {
            this._helper.AddDomainObject(transferJob);
        }

        public void UpdateTransferJob(TransferJob transferJob)
        {
            this.DataProvider.Update(transferJob);
        }

        public void DeleteTransferJob(TransferJob transferJob)
        {
            this._helper.DeleteDomainObject(transferJob);
        }

        public void DeleteTransferJob(TransferJob[] transferJob)
        {
            this._helper.DeleteDomainObject(transferJob);
        }

        public object GetTransferJob(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(TransferJob), new object[] { serial });
        }

        public object[] GetTransferJobs(string jobType)
        {
            string sql = "SELECT a.SERIAL, a.NAME, a.DESCRIPTION, a.TRANSACTIONSETSERIAL, a.JOBSTATUS,";
            sql += "             a.JOBTYPE, a.KEEPDAYS, a.TRANSACTIONCOUNT, a.LASTSUCCESSDATE,";
            sql += "             a.LASTSUCCESSTIME, a.LASTRUNDATE, a.LASTRUNTIME, a.RUNCOUNT, a.MUSER,";
            sql += "             a.MDATE, a.MTIME, b.MASTERTABLE, b.CONDITION";
            sql += "        FROM tbltransferjob a, tbltransactionset b";
            sql += "       WHERE a.transactionsetserial = b.serial";
            sql += "         AND a.jobstatus='Active'";
            sql += "         AND a.jobtype='" + jobType + "'";
            sql += "       ORDER BY a.serial";

            return this.DataProvider.CustomQuery(typeof(TransferJobExtend), new SQLCondition(sql));
        } 
        #endregion

        public TransactionSet CreateNewTransactionSet()
        {
            return new TransactionSet();
        }

        public void AddTransactionSet(TransactionSet transactionSet)
        {
            this._helper.AddDomainObject(transactionSet);
        }

        public void UpdateTransactionSet(TransactionSet transactionSet)
        {
            this.DataProvider.Update(transactionSet);
        }

        public void DeleteTransactionSet(TransactionSet transactionSet)
        {
            this._helper.DeleteDomainObject(transactionSet);
        }

        public void DeleteTransactionSet(TransactionSet[] transactionSet)
        {
            this._helper.DeleteDomainObject(transactionSet);
        }

        public object GetTransactionSet(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(TransactionSet), new object[] { serial });
        }

        public object[] GetChildTransactionSets(int parentSerial)
        {
            string sql = "SELECT {0} FROM tbltransactionset WHERE parentserial=" + parentSerial;

            return this.DataProvider.CustomQuery(typeof(TransactionSet),
                new SQLCondition(string.Format(sql,
                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(TransactionSet)))));
        }


        public TransactionSetDetail CreateNewTransactionSetDetail()
        {
            return new TransactionSetDetail();
        }

        public void AddTransactionSetDetail(TransactionSetDetail transactionSetDetail)
        {
            this._helper.AddDomainObject(transactionSetDetail);
        }

        public void UpdateTransactionSetDetail(TransactionSetDetail transactionSetDetail)
        {
            this.DataProvider.Update(transactionSetDetail);
        }

        public void DeleteTransactionSetDetail(TransactionSetDetail transactionSetDetail)
        {
            this._helper.DeleteDomainObject(transactionSetDetail);
        }

        public void DeleteTransactionSetDetail(TransactionSetDetail[] transactionSetDetail)
        {
            this._helper.DeleteDomainObject(transactionSetDetail);
        }

        public object GetTransactionSetDetail(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(TransactionSetDetail), new object[] { serial });
        }

        public object[] GetTransactionSetDetails(int transactionSetSerial)
        {
            string sql = "SELECT {0} FROM tbltransactionsetdetail WHERE TRANSACTIONSETSERIAL=" + transactionSetSerial + " ORDER BY SEQUENCE";

            return this.DataProvider.CustomQuery(typeof(TransactionSetDetail),
                new SQLCondition(string.Format(sql,
                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(TransactionSetDetail)))));
        }
    }
}
