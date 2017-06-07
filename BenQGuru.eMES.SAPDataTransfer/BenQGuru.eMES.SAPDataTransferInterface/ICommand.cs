using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.SAPDataTransferInterface
{
    public enum RunMethod
    {
        Auto,
        Manually
    }

    public class ServiceResult
    {
        public ServiceResult(bool result, string message, string transactionCode)
        {
            this.m_Result = result;
            this.m_Message = message;
            this.m_TransactionCode = transactionCode;
        }
        private bool m_Result;

        public bool Result
        {
            get { return m_Result; }
            set { m_Result = value; }
        }

        private string m_Message;

        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }

        private string m_TransactionCode;

        public string TransactionCode
        {
            get { return m_TransactionCode; }
            set { m_TransactionCode = value; }
        }

    }

    public interface ICommand : IDisposable
    {
        ServiceResult Run(RunMethod runMethod);

        object GetArguments();
        
        void SetArguments(object arguments);

        bool ArgumentValid(ref string returnMessage);

        object NewTransactionCode();
    }
}
