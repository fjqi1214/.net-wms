using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.BaseSetting
{
    [Serializable, TableMap("TBLDATATRANSFERLOG", "TRANSACTIONCODE")]
    public class SAPDataTransferLog : DomainObject
    {
        [FieldMapAttribute("JOBID", typeof(string), 100, false)]
        public string JobID;

        [FieldMapAttribute("TRANSACTIONCODE", typeof(string), 100, false)]
        public string TransactionCode;

        [FieldMapAttribute("TRANSACTIONSEQUENCE", typeof(int), 8, false)]
        public int TransactionSequence;

        [FieldMapAttribute("REQUESTDATE", typeof(int), 8, false)]
        public int RequestDate;

        [FieldMapAttribute("REQUESTTIME", typeof(int), 6, false)]
        public int RequestTime;

        [FieldMapAttribute("REQUESTCONTENT", typeof(string), 500, false)]
        public string RequestContent;

        [FieldMapAttribute("RESPONSEDATE", typeof(int), 8, true)]
        public int ResponseDate;

        [FieldMapAttribute("RESPONSETIME", typeof(int), 6, true)]
        public int ResponseTime;

        [FieldMapAttribute("RESPONSECONTENT", typeof(string), 500, true)]
        public string ResponseContent;

        [FieldMapAttribute("FinishedDate", typeof(int), 8, true)]
        public int FinishedDate;

        [FieldMapAttribute("FINISHEDTIME", typeof(int), 6, true)]
        public int FinishedTime;

        [FieldMapAttribute("RESULT", typeof(string), 40, true)]
        public string Result;

        [FieldMapAttribute("ERRORMESSAGE", typeof(string), 2000, true)]
        public string ErrorMessage;

        [FieldMapAttribute("SENDRECORDCOUNT", typeof(int), 8, true)]
        public int SendRecordCount;

        [FieldMapAttribute("RECEIVEDRECORDCOUNT", typeof(int), 8, true)]
        public int ReceivedRecordCount;

        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
}
