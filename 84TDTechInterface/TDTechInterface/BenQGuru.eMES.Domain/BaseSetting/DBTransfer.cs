using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.BaseSetting
{
    #region TransferJob
    [Serializable, TableMap("TBLTRANSFERJOB", "SERIAL")]
    public class TransferJob : DomainObject
    {
        public TransferJob()
        {

        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        /// Name
        ///</summary>	
        [FieldMapAttribute("NAME", typeof(string), 40, false)]
        public string Name;

        ///<summary>
        /// Description
        ///</summary>	
        [FieldMapAttribute("DESCRIPTION", typeof(string), 100, true)]
        public string Description;

        ///<summary>
        /// TransactionSetSerial
        ///</summary>	
        [FieldMapAttribute("TRANSACTIONSETSERIAL", typeof(int), 38, false)]
        public int TransactionSetSerial;

        ///<summary>
        /// JobStatus
        ///</summary>	
        [FieldMapAttribute("JOBSTATUS", typeof(string), 40, false)]
        public string JobStatus;

        ///<summary>
        /// JobType
        ///</summary>	
        [FieldMapAttribute("JOBTYPE", typeof(string), 40, false)]
        public string JobType;

        ///<summary>
        /// KeepDays
        ///</summary>	
        [FieldMapAttribute("KEEPDAYS", typeof(int), 38, false)]
        public int KeepDays;

        ///<summary>
        /// TransactionCount
        ///</summary>	
        [FieldMapAttribute("TRANSACTIONCOUNT", typeof(int), 38, false)]
        public int TransactionCount;

        ///<summary>
        /// MasterTable
        ///</summary>	
        [FieldMapAttribute("MASTERTABLE", typeof(string), 40, true)]
        public string MasterTable;

        ///<summary>
        /// LastSuccessDate
        ///</summary>	
        [FieldMapAttribute("LASTSUCCESSDATE", typeof(int), 8, true)]
        public int LastSuccessDate;

        ///<summary>
        /// LastSuccessTime
        ///</summary>	
        [FieldMapAttribute("LASTSUCCESSTIME", typeof(int), 6, true)]
        public int LastSuccessTime;

        ///<summary>
        /// LastRunDate
        ///</summary>	
        [FieldMapAttribute("LASTRUNDATE", typeof(int), 8, true)]
        public int LastRunDate;

        ///<summary>
        /// LastRunTime
        ///</summary>	
        [FieldMapAttribute("LASTRUNTIME", typeof(int), 6, true)]
        public int LastRunTime;

        ///<summary>
        /// RunCount
        ///</summary>	
        [FieldMapAttribute("RUNCOUNT", typeof(int), 38, true)]
        public int RunCount;

        /// <summary>
        /// 最后系统用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        
    } 
    #endregion

    [Serializable, TableMap("TBLTRANSFERJOB", "SERIAL")]
    public class TransferJobExtend : TransferJob
    {
        public TransferJobExtend()
        {

        }

        ///<summary>
        /// Condition
        ///</summary>	
        [FieldMapAttribute("CONDITION", typeof(string), 2000, true)]
        public string Condition;
    }

    #region TransactionSet
    [Serializable, TableMap("TBLTRANSACTIONSET", "SERIAL")]
    public class TransactionSet : DomainObject
    {
        public TransactionSet()
        {

        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        /// Name
        ///</summary>	
        [FieldMapAttribute("NAME", typeof(string), 40, false)]
        public string Name;

        ///<summary>
        /// Description
        ///</summary>	
        [FieldMapAttribute("DESCRIPTION", typeof(string), 100, true)]
        public string Description;

        ///<summary>
        /// MasterTable
        ///</summary>	
        [FieldMapAttribute("MASTERTABLE", typeof(string), 40, true)]
        public string MasterTable;

        ///<summary>
        /// KeyFields
        ///</summary>	
        [FieldMapAttribute("KEYFIELD", typeof(string), 100, true)]
        public string KeyFields;

        ///<summary>
        /// Condition
        ///</summary>	
        [FieldMapAttribute("CONDITION", typeof(string), 2000, true)]
        public string Condition;

        ///<summary>
        /// TransactionMode        
        ///</summary>	
        [FieldMapAttribute("TRANSACTIONMODE", typeof(string), 40, false)]
        public string TransactionMode;

        ///<summary>
        /// SetType        
        ///</summary>	
        [FieldMapAttribute("SETTYPE", typeof(string), 40, false)]
        public string SetType;

        ///<summary>
        /// ParentSerial
        ///</summary>	
        [FieldMapAttribute("ParentSerial", typeof(int), 38, false)]
        public int ParentSerial;

        ///<summary>
        /// ChildFields        
        ///</summary>	
        [FieldMapAttribute("CHILDFIELDS", typeof(string), 100, false)]
        public string ChildFields;

        /// <summary>
        /// 最后系统用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;
    } 
    #endregion

    #region TransactionSetDetail
    [Serializable, TableMap("TBLTRANSACTIONSETDETAIL", "SERIAL")]
    public class TransactionSetDetail : DomainObject
    {
        public TransactionSetDetail()
        {

        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        /// TransactionSetSerial
        ///</summary>	
        [FieldMapAttribute("TRANSACTIONSETSERIAL", typeof(int), 38, false)]
        public int TransactionSetSerial;

        ///<summary>
        /// Sequence
        ///</summary>	
        [FieldMapAttribute("SEQUENCE", typeof(int), 38, false)]
        public int Sequence;

        ///<summary>
        /// TableName        
        ///</summary>	
        [FieldMapAttribute("TABLENAME", typeof(string), 40, false)]
        public string TableName;

        ///<summary>
        /// ForeignKeyFields        
        ///</summary>	
        [FieldMapAttribute("FOREIGNKEYFIELDS", typeof(string), 100, false)]
        public string ForeignKeyFields;

        ///<summary>
        /// Condition
        ///</summary>	
        [FieldMapAttribute("CONDITION", typeof(string), 2000, true)]
        public string Condition;

        ///<summary>
        /// CopyMethod
        ///</summary>	
        [FieldMapAttribute("COPYMETHOD", typeof(string), 40, true)]
        public string CopyMethod;

        /// <summary>
        /// 最后系统用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;
    } 
    #endregion
    
}
