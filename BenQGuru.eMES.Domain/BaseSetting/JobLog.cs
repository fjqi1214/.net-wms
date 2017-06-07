using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.BaseSetting
{
    #region JobLog

    /// <summary>
    ///	JobLog
    /// </summary>
    [Serializable, TableMap("TBLJOBLOG", "SERIAL")]
    public class JobLog : DomainObject
    {
        public JobLog()
        {
        }

        ///<summary>
        ///JobID
        ///</summary>	
        [FieldMapAttribute("JOBID", typeof(string), 40, false)]
        public string JobID;

        ///<summary>
        ///StartDateTime
        ///</summary>	
        [FieldMapAttribute("STARTDATETIME", typeof(DateTime), 8, false)]
        public DateTime StartDateTime;

        ///<summary>
        ///EndDateTime
        ///</summary>	
        [FieldMapAttribute("ENDDATETIME", typeof(DateTime), 8, false)]
        public DateTime EndDateTime;

        ///<summary>
        ///UsedTime
        ///</summary>	
        [FieldMapAttribute("USEDTIME", typeof(int), 22, false)]
        public int UsedTime;

        ///<summary>
        ///ProcessCount
        ///</summary>	
        [FieldMapAttribute("PROCESSCOUNT", typeof(int), 22, false)]
        public int ProcessCount;

        ///<summary>
        ///Result
        ///</summary>	
        [FieldMapAttribute("RESULT", typeof(string), 40, false)]
        public string Result;

        ///<summary>
        ///ErrorMessage
        ///</summary>	
        [FieldMapAttribute("ERRORMSG", typeof(string), 500, true)]
        public string ErrorMessage;

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

    }

    #endregion
}
