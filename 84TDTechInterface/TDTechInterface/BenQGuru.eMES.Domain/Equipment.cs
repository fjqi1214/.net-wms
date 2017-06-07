using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.Equipment
{


    #region EQUIPMENTTYPE
    /// <summary>
    /// TBLEQUIPMENTTYPE
    /// </summary>
    [Serializable, TableMap("TBLEQUIPMENTTYPE", "EQPTYPE")]
    public class EquipmentType : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public EquipmentType()
        {
        }

        ///<summary>
        ///EQPTYPE
        ///</summary>
        [FieldMapAttribute("EQPTYPE", typeof(string), 80, false)]
        public string Eqptype;

        ///<summary>
        ///EQPTYPEDESC
        ///</summary>
        [FieldMapAttribute("EQPTYPEDESC", typeof(string), 200, true)]
        public string Eqptypedesc;

        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 200, true)]
        public string Eattribute1;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 80, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, true)]
        public int MaintainDate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, true)]
        public int MaintainTime;

    }
    #endregion

    #region Equipment
    [Serializable, TableMap("TBLEQUIPMENT", "EQPID")]
    public class Equipment : DomainObject
    {

        /// <summary>
        /// 设备ID
        /// </summary>
        [FieldMapAttribute("EQPID", typeof(string), 40, false)]
        public string EqpId;

        ///<summary>
        ///EQPNAME
        ///</summary>
        [FieldMapAttribute("EQPNAME", typeof(string), 100, true)]
        public string Eqpname;


        /// <summary>
        /// 品牌
        /// </summary>
        [FieldMapAttribute("Model", typeof(string), 40, true)]
        public string Model;

        /// <summary>
        /// 设备类型：系统参数维护
        /// </summary>
        [FieldMapAttribute("Type", typeof(string), 40, true)]
        public string Type;

        /// <summary>
        /// 设备描述
        /// </summary>
        [FieldMapAttribute("EQPDESC", typeof(string), 100, true)]
        public string EqpDesc;

        ///<summary>
        ///EQPTYPE
        ///</summary>
        [FieldMapAttribute("EQPTYPE", typeof(string), 100, true)]
        public string Eqptype;

        ///<summary>
        ///EQPCOMPANY
        ///</summary>
        [FieldMapAttribute("EQPCOMPANY", typeof(string), 100, true)]
        public string Eqpcompany;

        ///<summary>
        ///CONTACT
        ///</summary>
        [FieldMapAttribute("CONTACT", typeof(string), 100, true)]
        public string Contact;

        ///<summary>
        ///TELPHONE
        ///</summary>
        [FieldMapAttribute("TELPHONE", typeof(string), 100, true)]
        public string Telphone;

        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 100, true)]
        public string Eattribute1;

        ///<summary>
        ///EATTRIBUTE2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 100, true)]
        public string Eattribute2;


        ///<summary>
        ///EATTRIBUTE2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 100, true)]
        public string Eattribute3;


        ///<summary>
        ///EQPSTATUS
        ///</summary>
        [FieldMapAttribute("EQPSTATUS", typeof(string), 40, true)]
        public string Eqpstatus;


        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;
    }
    #endregion

    #region EQPLOG
    /// <summary>
    /// TBLEQPLOG
    /// </summary>
    [Serializable, TableMap("TBLEQPLOG", "SERIAL")]
    public class EQPLog : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public EQPLog()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///EQPID
        ///</summary>
        [FieldMapAttribute("EQPID", typeof(string), 40, false)]
        public string Eqpid;

        ///<summary>
        ///EQPSTATUS
        ///</summary>
        [FieldMapAttribute("EQPSTATUS", typeof(string), 40, true)]
        public string Eqpstatus;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 400, true)]
        public string Memo;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

    }
    #endregion

    #region EQUIPMENTTSTYPE
    /// <summary>
    /// TBLEQUIPMENTTSTYPE
    /// </summary>
    [Serializable, TableMap("TBLEQUIPMENTTSTYPE", "EQPTSTYPE")]
    public class EquipmentTsType : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public EquipmentTsType()
        {
        }

        ///<summary>
        ///EQPTSTYPE
        ///</summary>
        [FieldMapAttribute("EQPTSTYPE", typeof(string), 80, false)]
        public string Eqptstype;

        ///<summary>
        ///EQPTSTYPEDESC
        ///</summary>
        [FieldMapAttribute("EQPTSTYPEDESC", typeof(string), 200, true)]
        public string Eqptstypedesc;

        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 200, true)]
        public string Eattribute1;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 80, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, true)]
        public int MaintainDate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, true)]
        public int MaintainTime;

    }
    #endregion

    #region EQPTSLOG
    /// <summary>
    /// TBLEQPTSLOG
    /// </summary>
    [Serializable, TableMap("TBLEQPTSLOG", "SERIAL")]
    public class EQPTSLog : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public EQPTSLog()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        /// <summary>
        /// 维修时长(分钟)
        /// </summary>
        [FieldMapAttribute("DURATION", typeof(int), 8, false)]
        public int Duration;

        ///<summary>
        ///EQPID
        ///</summary>
        [FieldMapAttribute("EQPID", typeof(string), 40, false)]
        public string Eqpid;

        ///<summary>
        ///REASON
        ///</summary>
        [FieldMapAttribute("REASON", typeof(string), 400, true)]
        public string Reason;

        ///<summary>
        ///SOLUTION
        ///</summary>
        [FieldMapAttribute("SOLUTION", typeof(string), 400, true)]
        public string Solution;

        ///<summary>
        ///RESULT
        ///</summary>
        [FieldMapAttribute("RESULT", typeof(string), 400, true)]
        public string Result;

        ///<summary>
        ///TSTYPE
        ///</summary>
        [FieldMapAttribute("TSTYPE", typeof(string), 40, true)]
        public string Tstype;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 400, true)]
        public string Memo;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///FINDUSER
        ///</summary>
        [FieldMapAttribute("FINDUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string FindUser;

        ///<summary>
        ///FINDMDATE
        ///</summary>
        [FieldMapAttribute("FINDMDATE", typeof(int), 22, false)]
        public int FindMdate;

        ///<summary>
        ///FINDMTIME
        ///</summary>
        [FieldMapAttribute("FINDMTIME", typeof(int), 22, false)]
        public int FindMtime;

        ///<summary>
        ///TSINFO
        ///</summary>
        [FieldMapAttribute("TSINFO", typeof(string), 400, true)]
        public string TsInfo;

        ///<summary>
        ///STATUS
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;


    }
    #endregion

    #region EQPOEE
    /// <summary>
    /// TBLEQPOEE
    /// </summary>
    [Serializable, TableMap("TBLEQPOEE", "EQPID")]
    public class EQPOEE : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public EQPOEE()
        {
        }

        ///<summary>
        ///EQPID
        ///</summary>
        [FieldMapAttribute("EQPID", typeof(string), 40, false)]
        public string Eqpid;

        ///<summary>
        ///WORKTIME
        ///</summary>
        [FieldMapAttribute("WORKTIME", typeof(int), 22, false)]
        public int Worktime;

        ///<summary>
        ///SSCODE
        ///</summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string Sscode;

        ///<summary>
        ///OPCODE
        ///</summary>
        //[FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        //public string Opcode;

        ///<summary>
        ///RESCODE
        ///</summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        public string ResCode;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

    }
    #endregion

    #region EQPOEEForQuery
    public class EQPOEEForQuery : EQPOEE
    {
        public EQPOEEForQuery()
        { }

        [FieldMapAttribute("EQPDESC", typeof(string), 100, true)]
        public string EQPDESC;
    }
    #endregion

    #region EQPUSEINFO
    /// <summary>
    /// TBLEQPUSEINFO
    /// </summary>
    [Serializable, TableMap("TBLEQPUSEINFO", "EQPID,USEDATE")]
    public class EQPUseInfo : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public EQPUseInfo()
        {
        }

        ///<summary>
        ///EQPID
        ///</summary>
        [FieldMapAttribute("EQPID", typeof(string), 40, false)]
        public string Eqpid;

        ///<summary>
        ///USEDATE
        ///</summary>
        [FieldMapAttribute("USEDATE", typeof(int), 22, false)]
        public int Usedate;

        ///<summary>
        ///ONTIME
        ///</summary>
        [FieldMapAttribute("ONTIME", typeof(int), 22, false)]
        public int Ontime;

        ///<summary>
        ///OFFTIME
        ///</summary>
        [FieldMapAttribute("OFFTIME", typeof(int), 22, false)]
        public int Offtime;

        ///<summary>
        ///RUNURATION
        ///</summary>
        [FieldMapAttribute("RUNURATION", typeof(int), 22, false)]
        public int Runuration;

        ///<summary>
        ///STOPDURATION
        ///</summary>
        [FieldMapAttribute("STOPDURATION", typeof(int), 22, false)]
        public int Stopduration;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

    }
    #endregion

    #region EQPOEEForQuery
    public class EQPUseInfoForQuery : EQPUseInfo
    {
        public EQPUseInfoForQuery()
        { }

        [FieldMapAttribute("EQPDESC", typeof(string), 100, true)]
        public string EQPDESC;

        [FieldMapAttribute("WORKTIME", typeof(int), 22, false)]
        public int Worktime;
    }
    #endregion

    #region EQPMaintenance
    /// <summary>
    /// TBLEQPMaintenance
    /// </summary>
    [Serializable, TableMap("TBLEQPMaintenance", "EQPID,MaintainITEM,MaintainType")]
    public class EQPMaintenance : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public EQPMaintenance()
        {
        }

        ///<summary>
        ///EQPID
        ///</summary>
        [FieldMapAttribute("EQPID", typeof(string), 40, false)]
        public string Eqpid;


        ///<summary>
        ///MaintainITEM
        ///</summary>
        [FieldMapAttribute("MaintainITEM", typeof(string), 400, false)]
        public string MaintainITEM;

        ///<summary>
        ///MaintainITEM
        ///</summary>
        [FieldMapAttribute("MaintainType", typeof(string), 40, false)]
        public string MaintainType;

        ///<summary>
        ///CYCLETYPE
        ///</summary>
        [FieldMapAttribute("CYCLETYPE", typeof(string), 40, false)]
        public string CycleType;

        ///<summary>
        ///Frequency
        ///</summary>
        [FieldMapAttribute("Frequency", typeof(int), 6, false)]
        public int Frequency;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string Muser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;


    }
    #endregion

    #region EQPMaintenanceForQuery
    /// <summary>
    /// TBLEQPMaintenance
    /// </summary>

    public class EQPMaintenanceForQuery : EQPMaintenance
    {
        public EQPMaintenanceForQuery()
        {
        }


        [FieldMapAttribute("LASTMAINTENANCEDATE", typeof(int), 22, false)]
        public int LastMaintenanceDate;

        [FieldMapAttribute("LASTTIME", typeof(decimal), 22, false)]
        public decimal LastTime;

        [FieldMapAttribute("EQPNAME", typeof(string), 100, true)]
        public string Eqpname;

        [FieldMapAttribute("ACTDURATION", typeof(decimal), 22, false)]
        public decimal ActDuration;

        [FieldMapAttribute("PLANDURATION", typeof(int), 22, false)]
        public int PlanDuration;


    }
    #endregion

    
    #region EQPMaintenanceForQuery
    /// <summary>
    /// TBLEQPMaintenance
    /// </summary>

    public class EQPMaintenanceForEffective : EQPMaintenance
    {
        public EQPMaintenanceForEffective()
        {
        }


        [FieldMapAttribute("OEE", typeof(decimal), 22, false)]
        public decimal OEE;

        [FieldMapAttribute("ACTUSEDRATE", typeof(decimal), 22, false)]
        public decimal ActusedRate;

        [FieldMapAttribute("BXRATE", typeof(decimal), 22, false)]
        public decimal BXRate;

        [FieldMapAttribute("GOODRATE", typeof(decimal), 22, false)]
        public decimal GoodRate;

        [FieldMapAttribute("EQPNAME", typeof(string), 100, true)]
        public string Eqpname;

        [FieldMapAttribute("SSCODE", typeof(string), 22, false)]
        public string SSCode;

        //[FieldMapAttribute("OPCODE", typeof(string), 22, false)]
        //public string OpCode;

        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        public string ResCode;


    }
    #endregion
    


    #region EQPMaintainLog
    /// <summary>
    /// TBLEQPMaintainLog
    /// </summary>
    [Serializable, TableMap("TBLEQPMaintainLog", "SERIAL")]
    public class EQPMaintainLog : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public EQPMaintainLog()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///EQPID
        ///</summary>
        [FieldMapAttribute("EQPID", typeof(string), 40, false)]
        public string Eqpid;

        ///<summary>
        ///MaintainITEM
        ///</summary>
        [FieldMapAttribute("MaintainITEM", typeof(string), 400, false)]
        public string MaintainITEM;

        ///<summary>
        ///MaintainITEM
        ///</summary>
        [FieldMapAttribute("MaintainType", typeof(string), 400, false)]
        public string MaintainType;


        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 400, true)]
        public string MEMO;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string Muser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///RESULT
        ///</summary>
        [FieldMapAttribute("RESULT", typeof(string), 40, false)]
        public string Result;
    }
    #endregion

    #region EQPMaintenanceQuery
    public class EQPMaintenanceQuery : EQPMaintenance
    {
        public EQPMaintenanceQuery()
        {

        }

        /// <summary>
        /// 设备描述
        /// </summary>
        [FieldMapAttribute("EQPDESC", typeof(string), 100, true)]
        public string EqpDesc;

        ///<summary>
        ///LastMaintenanceDate
        ///</summary>
        [FieldMapAttribute("LastMaintenanceDate", typeof(int), 22, false)]
        public int LastMaintenanceDate;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 400, true)]
        public string MEMO;
    }
    #endregion

    #region EQPMaintainLogQuery
    public class EQPMaintainLogQuery : EQPMaintainLog
    {
        public EQPMaintainLogQuery()
        {

        }

        /// <summary>
        /// 设备描述
        /// </summary>
        [FieldMapAttribute("EQPDESC", typeof(string), 100, true)]
        public string EqpDesc;

        ///<summary>
        ///CYCLETYPE
        ///</summary>
        [FieldMapAttribute("CYCLETYPE", typeof(string), 40, false)]
        public string CycleType;
    }
    #endregion

}
