using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.SMT
{

	#region Station
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSTATION", "STATIONCODE,RESCODE")]
	public class Station : DomainObject
	{
		public Station()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("STATIONCODE", typeof(string), 40, true)]
		public string  StationCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("STATIONDESC", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 资源代码
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string  ResourceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

	#region SMTResourceBOM
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSMTRESBOM", "ITEMCODE,MOCODE,RESCODE,StationCode,OBITEMCODE")]
	public class SMTResourceBOM : DomainObject
	{
		public SMTResourceBOM()
		{
		}
 
		/// <summary>
		/// 料品生产日期
		/// </summary>
		[FieldMapAttribute("DATECODE", typeof(string), 100, false)]
		public string  DateCode;

		/// <summary>
		/// 厂商代号
		/// </summary>
		[FieldMapAttribute("VENDORCODE", typeof(string), 100, false)]
		public string  VendorCode;

		/// <summary>
		/// 厂商料号
		/// </summary>
		[FieldMapAttribute("VENDERITEMCODE", typeof(string), 100, false)]
		public string  VenderItemCode;

		/// <summary>
		/// 料品规格
		/// </summary>
		[FieldMapAttribute("VERSION", typeof(string), 100, false)]
		public string  Version;

		/// <summary>
		/// BIOS版本
		/// </summary>
		[FieldMapAttribute("BIOS", typeof(string), 100, false)]
		public string  BIOS;

		/// <summary>
		/// PCBA版本
		/// </summary>
		[FieldMapAttribute("PCBA", typeof(string), 100, false)]
		public string  PCBA;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LOTNO", typeof(string), 40, true)]
		public string  LotNO;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OBITEMCODE", typeof(string), 40, false)]
		public string  OPBOMItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string  MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ROUTECODE", typeof(string), 40, false)]
		public string  RouteCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, false)]
		public string  OPCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string  ResourceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("StationCode", typeof(string), 40, true)]
		public string  StationCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, true)]
		public string  FeederCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPBOMCODE", typeof(string), 40, false)]
		public string  OPBOMCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPBOMVER", typeof(string), 40, false)]
		public string  OPBOMVersion;

	}
	#endregion

	#region CodeObject
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("NULL", "Code")]
	public class CodeObject : DomainObject
	{
		public CodeObject()
		{
		}
 
		[FieldMapAttribute("CODE", typeof(string), 40, false)]
		public string  Code;


	}
	#endregion



	#region Feeder
	/// <summary>
	/// Feeder主档
	/// </summary>
	[Serializable, TableMap("tblFEEDER", "FEEDERCODE")]
	public class Feeder : DomainObject
	{
		public Feeder()
		{
		}
 
		/// <summary>
		/// Feeder类型
		/// </summary>
		[FieldMapAttribute("FEEDERTYPE", typeof(string), 40, false)]
		public string  FeederType;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// Feeder代码
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, true)]
		public string  FeederCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// Feeder规格代码
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// 最大使用次数
		/// </summary>
		[FieldMapAttribute("MAXCOUNT", typeof(decimal), 10, true)]
		public decimal  MaxCount;

		/// <summary>
		/// 已使用次数
		/// </summary>
		[FieldMapAttribute("USEDCOUNT", typeof(decimal), 10, true)]
		public decimal  UsedCount;

		/// <summary>
		/// 预警使用次数
		/// </summary>
		[FieldMapAttribute("ALERTCOUNT", typeof(decimal), 10, true)]
		public decimal  AlertCount;

		/// <summary>
		/// 状态
		/// </summary>
		[FieldMapAttribute("STATUS", typeof(string), 40, false)]
		public string  Status;

		/// <summary>
		/// 状态更改原因
		/// </summary>
		[FieldMapAttribute("STATUSCHGREASON", typeof(string), 40, false)]
		public string  StatusChangedReason;

		/// <summary>
		/// 状态更改日期
		/// </summary>
		[FieldMapAttribute("STATUSCHGDATE", typeof(int), 8, true)]
		public int  StatusChangedDate;

		/// <summary>
		/// 状态更改时间
		/// </summary>
		[FieldMapAttribute("STATUSCHGTIME", typeof(int), 6, true)]
		public int  StatusChangedTime;

		/// <summary>
		/// 退回原因
		/// </summary>
		[FieldMapAttribute("RETREASON", typeof(string), 100, false)]
		public string  ReturnReason;

		/// <summary>
		/// 是否在使用
		/// </summary>
		[FieldMapAttribute("USEFLAG", typeof(string), 1, true)]
		public string  UseFlag;

		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 累计使用次数
		/// </summary>
		[FieldMapAttribute("TOTALCOUNT", typeof(decimal), 10, true)]
		public decimal  TotalCount;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CURRUNITQTY", typeof(decimal), 15, true)]
		public decimal  CurrentUnitQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPEUSER", typeof(string), 40, false)]
		public string  OperationUser;

        //Add by Terry 2010-11-03
        /// <summary>
        /// 保养期限
        /// </summary>
        [FieldMapAttribute("MAXMDAY", typeof(decimal), 22, true)]
        public decimal MaxMDay;

        /// <summary>
        /// 保养预警期限
        /// </summary>
        [FieldMapAttribute("ALTERMDAY", typeof(decimal), 22, true)]
        public decimal AlterMDay;

        // <summary>
        /// 保养日期
        /// </summary>
        [FieldMapAttribute("MAINTAINDATE", typeof(decimal), 22, false)]
        public decimal TheMaintainDate;

        /// <summary>
        /// 保养时间
        /// </summary>
        [FieldMapAttribute("MAINTAINTIME", typeof(decimal), 22, false)]
        public decimal TheMaintainTime;

        // <summary>
        /// 保养人员
        /// </summary>
        [FieldMapAttribute("MAINTAINUSER", typeof(string), 40, false)]
        public string TheMaintainUser;

	}
	#endregion

	#region FeederMaintain
	/// <summary>
	/// Feeder维护信息
	/// </summary>
	[Serializable, TableMap("TBLFEEDERMAINTAIN", "FEEDERCODE,SEQ")]
	public class FeederMaintain : DomainObject
	{
		public FeederMaintain()
		{
		}
 
		/// <summary>
		/// Feeder类型
		/// </summary>
		[FieldMapAttribute("FEEDERTYPE", typeof(string), 40, false)]
		public string  FeederType;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// Feeder代码
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, true)]
		public string  FeederCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 规格代码
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// 状态
		/// </summary>
		[FieldMapAttribute("STATUS", typeof(string), 40, false)]
		public string  Status;

		/// <summary>
		/// 退回原因
		/// </summary>
		[FieldMapAttribute("RETREASON", typeof(string), 100, false)]
		public string  ReturnReason;

		/// <summary>
		/// 分析原因
		/// </summary>
		[FieldMapAttribute("ANALYSEREASON", typeof(string), 100, false)]
		public string  AnalyseReason;

		/// <summary>
		/// 处理方法
		/// </summary>
		[FieldMapAttribute("OPERMESSAGE", typeof(string), 100, false)]
		public string  OperationMessage;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(string), 10, true)]
		public string  Seq;

		/// <summary>
		/// 维护类型
		/// </summary>
		[FieldMapAttribute("MAINTAINTYPE", typeof(string), 40, false)]
		public string  MaintainType;

		/// <summary>
		/// 报废标记
		/// </summary>
		[FieldMapAttribute("SCRAPFLAG", typeof(string), 1, true)]
		public string  ScrapFlag;

		/// <summary>
		/// 最大使用次数
		/// </summary>
		[FieldMapAttribute("MAXCOUNT", typeof(decimal), 10, true)]
		public decimal  MaxCount;

		/// <summary>
		/// 已使用次数
		/// </summary>
		[FieldMapAttribute("USEDCOUNT", typeof(decimal), 10, true)]
		public decimal  UsedCount;

		/// <summary>
		/// 累计使用次数
		/// </summary>
		[FieldMapAttribute("TOTALCOUNT", typeof(decimal), 10, true)]
		public decimal  TotalCount;

		/// <summary>
		/// 原状态
		/// </summary>
		[FieldMapAttribute("OLDSTATUS", typeof(string), 40, false)]
		public string  OldStatus;

	}
	#endregion

	#region FeederSpec
	/// <summary>
	/// Feeder规格
	/// </summary>
	[Serializable, TableMap("tblFEEDERSPEC", "FEEDERSPECCODE")]
	public class FeederSpec : DomainObject
	{
		public FeederSpec()
		{
		}
 
		/// <summary>
		/// 规格名称
		/// </summary>
		[FieldMapAttribute("NAME", typeof(string), 100, false)]
		public string  Name;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// Feeder规格代码
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, true)]
		public string  FeederSpecCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

	#region FeederStatusLog
	/// <summary>
	/// Feeder使用记录
	/// </summary>
	[Serializable, TableMap("TBLFEEDERSTATUSLOG", "FEEDERCODE,SEQ")]
	public class FeederStatusLog : DomainObject
	{
		public FeederStatusLog()
		{
		}
 
		/// <summary>
		/// Feeder类型
		/// </summary>
		[FieldMapAttribute("FEEDERTYPE", typeof(string), 40, false)]
		public string  FeederType;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// Feeder代码
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, true)]
		public string  FeederCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// Feeder规格代码
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// 状态
		/// </summary>
		[FieldMapAttribute("STATUS", typeof(string), 40, false)]
		public string  Status;

		/// <summary>
		/// 状态更改原因
		/// </summary>
		[FieldMapAttribute("STATUSCHGREASON", typeof(string), 40, false)]
		public string  StatusChangedReason;

		/// <summary>
		/// 装有更改日期
		/// </summary>
		[FieldMapAttribute("STATUSCHGDATE", typeof(int), 8, true)]
		public int  StatusChangedDate;

		/// <summary>
		/// 状态更改时间
		/// </summary>
		[FieldMapAttribute("STATUSCHGTIME", typeof(int), 6, true)]
		public int  StatusChangedTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
		public decimal  Seq;

		/// <summary>
		/// 旧有状态
		/// </summary>
		[FieldMapAttribute("OLDSTATUS", typeof(string), 40, false)]
		public string  OldStatus;

		/// <summary>
		/// 其他信息
		/// </summary>
		[FieldMapAttribute("OTHERMESSAGE", typeof(string), 100, false)]
		public string  OtherMessage;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPEUSER", typeof(string), 40, false)]
		public string  OperationUser;

	}
	#endregion

	#region MachineFeeder
	/// <summary>
	/// 机台上料记录
	/// </summary>
	[Serializable, TableMap("TBLMACHINEFEEDER", "MOCODE,MACHINECODE,MACHINESTATIONCODE,TBLGRP,SSCODE")]
	public class MachineFeeder : DomainObject
	{
		public MachineFeeder()
		{
		}
 
		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("PRODUCTCODE", typeof(string), 40, false)]
		public string  ProductCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string  MOCode;

		/// <summary>
		/// 机台代码
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, true)]
		public string  MachineCode;

		/// <summary>
		/// 站位代码
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, true)]
		public string  MachineStationCode;

		/// <summary>
		/// Feeder规格
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// Feeder代码
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, false)]
		public string  FeederCode;

		/// <summary>
		/// 料卷编号
		/// </summary>
		[FieldMapAttribute("REELNO", typeof(string), 40, false)]
		public string  ReelNo;

		/// <summary>
		/// 上料人员
		/// </summary>
		[FieldMapAttribute("LOADUSER", typeof(string), 40, false)]
		public string  LoadUser;

		/// <summary>
		/// 上料日期
		/// </summary>
		[FieldMapAttribute("LOADDATE", typeof(int), 8, true)]
		public int  LoadDate;

		/// <summary>
		/// 上料时间
		/// </summary>
		[FieldMapAttribute("LOADTIME", typeof(int), 6, true)]
		public int  LoadTime;

		/// <summary>
		/// 料卷物料代码
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, false)]
		public string  MaterialCode;

		/// <summary>
		/// 单位用量
		/// </summary>
		[FieldMapAttribute("UNITQTY", typeof(decimal), 15, true)]
		public decimal  UnitQty;

		/// <summary>
		/// 生产批号
		/// </summary>
		[FieldMapAttribute("LOTNO", typeof(string), 40, false)]
		public string  LotNo;

		/// <summary>
		/// DateCode
		/// </summary>
		[FieldMapAttribute("DATECODE", typeof(string), 40, false)]
		public string  DateCode;

		/// <summary>
		/// 检查结果
		/// </summary>
		[FieldMapAttribute("CHECKRESULT", typeof(string), 1, true)]
		public string  CheckResult;

		/// <summary>
		/// 失败原因
		/// </summary>
		[FieldMapAttribute("FAILREASON", typeof(string), 100, false)]
		public string  FailReason;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("NEXTREELNO", typeof(string), 40, false)]
		public string  NextReelNo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPERESCODE", typeof(string), 40, false)]
		public string  OpeResourceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPESSCODE", typeof(string), 40, false)]
		public string  OpeStepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ENABLED", typeof(string), 1, true)]
		public string  Enabled;

		/// <summary>
		/// 料卷是否停线
		/// </summary>
		[FieldMapAttribute("REELCEASEFLAG", typeof(string), 1, true)]
		public string  ReelCeaseFlag;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("STATIONENABLED", typeof(string), 1, true)]
		public string  StationEnabled;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TBLGRP", typeof(string), 40, true)]
		public string  TableGroup;

	}
	#endregion

	#region MachineFeederLog
	/// <summary>
	/// 机台上料记录
	/// </summary>
	[Serializable, TableMap("TBLMACHINEFEEDERLOG", "LOGNO")]
	public class MachineFeederLog : DomainObject
	{
		public MachineFeederLog()
		{
		}
 
		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("PRODUCTCODE", typeof(string), 40, false)]
		public string  ProductCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 机台代码
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, false)]
		public string  MachineCode;

		/// <summary>
		/// 站位代码
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, false)]
		public string  MachineStationCode;

		/// <summary>
		/// Feeder规格
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// Feeder代码
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, false)]
		public string  FeederCode;

		/// <summary>
		/// 料卷编号
		/// </summary>
		[FieldMapAttribute("REELNO", typeof(string), 40, false)]
		public string  ReelNo;

		/// <summary>
		/// 上料人员
		/// </summary>
		[FieldMapAttribute("LOADUSER", typeof(string), 40, false)]
		public string  LoadUser;

		/// <summary>
		/// 上料日期
		/// </summary>
		[FieldMapAttribute("LOADDATE", typeof(int), 8, true)]
		public int  LoadDate;

		/// <summary>
		/// 上料时间
		/// </summary>
		[FieldMapAttribute("LOADTIME", typeof(int), 6, true)]
		public int  LoadTime;

		/// <summary>
		/// 料卷物料代码
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, false)]
		public string  MaterialCode;

		/// <summary>
		/// 单位用量
		/// </summary>
		[FieldMapAttribute("UNITQTY", typeof(decimal), 15, true)]
		public decimal  UnitQty;

		/// <summary>
		/// 生产批号
		/// </summary>
		[FieldMapAttribute("LOTNO", typeof(string), 40, false)]
		public string  LotNo;

		/// <summary>
		/// DateCode
		/// </summary>
		[FieldMapAttribute("DATECODE", typeof(string), 40, false)]
		public string  DateCode;

		/// <summary>
		/// 检查结果
		/// </summary>
		[FieldMapAttribute("CHECKRESULT", typeof(string), 1, true)]
		public string  CheckResult;

		/// <summary>
		/// 失败原因
		/// </summary>
		[FieldMapAttribute("FAILREASON", typeof(string), 100, false)]
		public string  FailReason;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LOGNO", typeof(decimal), 10, true)]
		public decimal  LogNo;

		/// <summary>
		/// 操作类型
		/// </summary>
		[FieldMapAttribute("OPERATIONTYPE", typeof(string), 40, false)]
		public string  OperationType;

		/// <summary>
		/// 料卷使用数量
		/// </summary>
		[FieldMapAttribute("REELUSEDQTY", typeof(decimal), 15, true)]
		public decimal  ReelUsedQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPERESCODE", typeof(string), 40, false)]
		public string  OpeResourceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPESSCODE", typeof(string), 40, false)]
		public string  OpeStepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("UNLOADUSER", typeof(string), 40, false)]
		public string  UnLoadUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("UNLOADDATE", typeof(int), 8, true)]
		public int  UnLoadDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("UNLOADTIME", typeof(int), 6, true)]
		public int  UnLoadTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("UNLOADTYPE", typeof(string), 40, false)]
		public string  UnLoadType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EXCHGFEEDERCODE", typeof(string), 40, false)]
		public string  ExchangeFeederCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EXCHGREELNO", typeof(string), 40, false)]
		public string  ExchageReelNo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 料卷点料差异数量
		/// </summary>
		[FieldMapAttribute("REELCHKDIFFQTY", typeof(decimal), 15, true)]
		public decimal  ReelCheckDiffQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("STATIONENABLED", typeof(string), 1, true)]
		public string  StationEnabled;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TBLGRP", typeof(string), 40, false)]
		public string  TableGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

	}
	#endregion

	#region Reel
	/// <summary>
	/// 料卷资料
	/// </summary>
	[Serializable, TableMap("TBLREEL", "REELNO")]
	public class Reel : DomainObject
	{
		public Reel()
		{
		}
 
		/// <summary>
		/// 数量
		/// </summary>
		[FieldMapAttribute("QTY", typeof(decimal), 15, true)]
		public decimal  Qty;

		/// <summary>
		/// 物料代码
		/// </summary>
		[FieldMapAttribute("PARTNO", typeof(string), 40, false)]
		public string  PartNo;

		/// <summary>
		/// 生产批号
		/// </summary>
		[FieldMapAttribute("LOTNO", typeof(string), 100, false)]
		public string  LotNo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATECODE", typeof(string), 40, false)]
		public string  DateCode;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 料卷代号
		/// </summary>
		[FieldMapAttribute("REELNO", typeof(string), 40, true)]
		public string  ReelNo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 已使用数量
		/// </summary>
		[FieldMapAttribute("USEDQTY", typeof(decimal), 15, true)]
		public decimal  UsedQty;

		/// <summary>
		/// 是否被领用
		/// </summary>
		[FieldMapAttribute("USEDFLAG", typeof(string), 1, true)]
		public string  UsedFlag;

		/// <summary>
		/// 领用的工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 领用的产线代码
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 是否特采
		/// </summary>
		[FieldMapAttribute("ISSPECIAL", typeof(string), 1, true)]
		public string  IsSpecial;

		/// <summary>
		/// 备注
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 100, false)]
		public string  Memo;

		/// <summary>
		/// 点料差异
		/// </summary>
		[FieldMapAttribute("CHECKDIFFQTY", typeof(decimal), 15, true)]
		public decimal  CheckDiffQty;

	}
	#endregion

	#region ReelCheckedLog
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLREELCHKLOG", "CheckID")]
	public class ReelCheckedLog : DomainObject
	{
		public ReelCheckedLog()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CheckID", typeof(decimal), 10, true)]
		public decimal  CheckID;

		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 料卷编号
		/// </summary>
		[FieldMapAttribute("REELNO", typeof(string), 40, false)]
		public string  ReelNo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHECKUSER", typeof(string), 40, false)]
		public string  CheckUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHECKDATE", typeof(int), 8, true)]
		public int  CheckDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHECKTIME", typeof(int), 6, true)]
		public int  CheckTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 物料代码
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, false)]
		public string  MaterialCode;

		/// <summary>
		/// 料卷总数量
		/// </summary>
		[FieldMapAttribute("REELQTY", typeof(decimal), 15, true)]
		public decimal  ReelQty;

		/// <summary>
		/// 料卷点料时的剩余数量
		/// </summary>
		[FieldMapAttribute("REELLEFTQTY", typeof(decimal), 15, true)]
		public decimal  ReelLeftQty;

		/// <summary>
		/// 料卷实际数量
		/// </summary>
		[FieldMapAttribute("REELACTQTY", typeof(decimal), 15, true)]
		public decimal  ReelActualQty;

		/// <summary>
		/// 是否做点料
		/// </summary>
		[FieldMapAttribute("ISCHECKED", typeof(string), 1, true)]
		public string  IsChecked;

		/// <summary>
		/// 料卷领用时的数量
		/// </summary>
		[FieldMapAttribute("REELCURRQTY", typeof(decimal), 15, true)]
		public decimal  ReelCurrentQty;

		/// <summary>
		/// 是否特采
		/// </summary>
		[FieldMapAttribute("ISSPECIAL", typeof(string), 1, true)]
		public string  IsSpecial;

		/// <summary>
		/// 特采备注
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 100, false)]
		public string  Memo;

		/// <summary>
		/// 领用人员
		/// </summary>
		[FieldMapAttribute("GETOUTUSER", typeof(string), 40, false)]
		public string  GetOutUser;

		/// <summary>
		/// 领用日期
		/// </summary>
		[FieldMapAttribute("GETOUTDATE", typeof(int), 8, true)]
		public int  GetOutDate;

		/// <summary>
		/// 领用时间
		/// </summary>
		[FieldMapAttribute("GETOUTTIME", typeof(int), 6, true)]
		public int  GetOutTime;

	}
	#endregion

	#region ReelQty
	/// <summary>
	/// Feeder主档
	/// </summary>
	[Serializable, TableMap("TBLREELQTY", "REELNO,MOCODE")]
	public class ReelQty : DomainObject
	{
		public ReelQty()
		{
		}
 
		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 料卷编号
		/// </summary>
		[FieldMapAttribute("REELNO", typeof(string), 40, true)]
		public string  ReelNo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string  MOCode;

		/// <summary>
		/// 机台代码
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, false)]
		public string  MachineCode;

		/// <summary>
		/// 站位代码
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, false)]
		public string  MachineStationCode;

		/// <summary>
		/// Feeder规格
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// Feeder代码
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, false)]
		public string  FeederCode;

		/// <summary>
		/// 原有数量
		/// </summary>
		[FieldMapAttribute("QTY", typeof(decimal), 15, true)]
		public decimal  Qty;

		/// <summary>
		/// 使用数量
		/// </summary>
		[FieldMapAttribute("USEDQTY", typeof(decimal), 15, true)]
		public decimal  UsedQty;

		/// <summary>
		/// 已更新数量
		/// </summary>
		[FieldMapAttribute("UPDATEDQTY", typeof(decimal), 15, true)]
		public decimal  UpdatedQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ALERTQTY", typeof(decimal), 15, true)]
		public decimal  AlertQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("UNITQTY", typeof(decimal), 15, true)]
		public decimal  UnitQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MaterialCode", typeof(string), 40, false)]
		public string  MaterialCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

	}
	#endregion

	#region ReelValidity
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLREELVALIDITY", "MATERIALPREFIX")]
	public class ReelValidity : DomainObject
	{
		public ReelValidity()
		{
		}
 
		/// <summary>
		/// 物料代码前缀
		/// </summary>
		[FieldMapAttribute("MATERIALPREFIX", typeof(string), 40, true)]
		public string  MaterialPrefix;

		/// <summary>
		/// 有效期(月)
		/// </summary>
		[FieldMapAttribute("VALIDITYMONTH", typeof(decimal), 15, true)]
		public decimal  ValidityMonth;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
		public string  EAttribute1;

	}
	#endregion

	#region SMTAlert
	/// <summary>
	/// SMT预警
	/// </summary>
	[Serializable, TableMap("TBLSMTALERT", "ALERTSEQ")]
	public class SMTAlert : DomainObject
	{
		public SMTAlert()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ALERTSEQ", typeof(decimal), 10, true)]
		public decimal  AlertSeq;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ALERTTYPE", typeof(string), 40, false)]
		public string  AlertType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PRODUCTCODE", typeof(string), 40, false)]
		public string  ProductCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, false)]
		public string  MachineCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, false)]
		public string  MachineStationCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, false)]
		public string  FeederCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FEEDERMAXCOUNT", typeof(decimal), 10, true)]
		public decimal  FeederMaxCount;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FEEDERALERTCOUNT", typeof(decimal), 10, true)]
		public decimal  FeederAlertCount;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FEEDERUSEDCOUNT", typeof(decimal), 10, true)]
		public decimal  FeederUsedCount;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("REELNO", typeof(string), 40, false)]
		public string  ReelNo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("REELQTY", typeof(decimal), 15, true)]
		public decimal  ReelQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("REELUSEDQTY", typeof(decimal), 15, true)]
		public decimal  ReelUsedQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ALERTDATE", typeof(int), 8, true)]
		public int  AlertDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ALERTTIME", typeof(int), 6, true)]
		public int  AlertTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ALERTSTATUS", typeof(string), 40, false)]
		public string  AlertStatus;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ALERTLEVEL", typeof(string), 40, false)]
		public string  AlertLevel;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MAINTAINUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MAINTAINDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MAINTAINTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

	}
	#endregion

	#region SMTCheckMaterial
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("tblSMTCheckMaterial", "CheckID")]
	public class SMTCheckMaterial : DomainObject
	{
		public SMTCheckMaterial()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CheckID", typeof(decimal), 10, true)]
		public decimal  CheckID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHECKRESULT", typeof(string), 40, false)]
		public string  CheckResult;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHECKUSER", typeof(string), 40, false)]
		public string  CheckUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHECKDATE", typeof(int), 8, true)]
		public int  CheckDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHECKTIME", typeof(int), 6, true)]
		public int  CheckTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
		public string  ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

	}
	#endregion

	#region SMTCheckMaterialDetail
	/// <summary>
	/// SMT Feeder物料
	/// </summary>
	[Serializable, TableMap("tblSMTCheckMaterialDtl", "CheckID,SEQ")]
	public class SMTCheckMaterialDetail : DomainObject
	{
		public SMTCheckMaterialDetail()
		{
		}
 
		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("PRODUCTCODE", typeof(string), 40, false)]
		public string  ProductCode;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 机台代码
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, false)]
		public string  MachineCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 首选料
		/// </summary>
		[FieldMapAttribute("SOURCEMATERIALCODE", typeof(string), 40, false)]
		public string  SourceMaterialCode;

		/// <summary>
		/// 物料代码
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, false)]
		public string  MaterialCode;

		/// <summary>
		/// Feeder规格代码
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// SMT用量
		/// </summary>
		[FieldMapAttribute("SMTQTY", typeof(decimal), 15, true)]
		public decimal  SMTQty;

		/// <summary>
		/// 站位代码
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, false)]
		public string  MachineStationCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CheckID", typeof(decimal), 10, true)]
		public decimal  CheckID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
		public decimal  Sequence;

		/// <summary>
		/// 物料名称
		/// </summary>
		[FieldMapAttribute("MATERIALNAME", typeof(string), 100, false)]
		public string  MaterialName;

		/// <summary>
		/// 工单BOM单位用量
		/// </summary>
		[FieldMapAttribute("BOMQTY", typeof(decimal), 15, true)]
		public decimal  BOMQty;

		/// <summary>
		/// 比对结果
		/// </summary>
		[FieldMapAttribute("CHECKRESULT", typeof(string), 1, true)]
		public string  CheckResult;

		/// <summary>
		/// 比对描述
		/// </summary>
		[FieldMapAttribute("CHECKDESC", typeof(string), 100, false)]
		public string  CheckDescription;

		/// <summary>
		/// 单位
		/// </summary>
		[FieldMapAttribute("BOMUOM", typeof(string), 100, false)]
		public string  BOMUOM;

		/// <summary>
		/// SMT/MOBOM
		/// </summary>
		[FieldMapAttribute("TYPE", typeof(string), 40, false)]
		public string  Type;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

	}
	#endregion

	#region SMTFeederMaterial
	/// <summary>
	/// SMT Feeder物料
	/// </summary>
	[Serializable, TableMap("TBLSMTFEEDERMATERIAL", "PRODUCTCODE,MACHINECODE,MATERIALCODE,MACHINESTATIONCODE,SSCODE,TBLGRP")]
	public class SMTFeederMaterial : DomainObject
	{
		public SMTFeederMaterial()
		{
		}
 
		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("PRODUCTCODE", typeof(string), 40, true)]
		public string  ProductCode;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 机台代码
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, true)]
		public string  MachineCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

        /// <summary>
        /// add by kathy @20130814 新增页面隐藏栏位，记录导入信息是否合法
        /// </summary>
        public string EAttribute2;

		/// <summary>
		/// 首选料
		/// </summary>
		[FieldMapAttribute("SOURCEMATERIALCODE", typeof(string), 40, false)]
		public string  SourceMaterialCode;

		/// <summary>
		/// 物料代码
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, true)]
		public string  MaterialCode;

		/// <summary>
		/// Feeder规格代码
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// 数量
		/// </summary>
		[FieldMapAttribute("QTY", typeof(decimal), 15, true)]
		public decimal  Qty;

		/// <summary>
		/// 站位代码
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, true)]
		public string  MachineStationCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string  StepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TBLGRP", typeof(string), 40, true)]
		public string  TableGroup;

	}
	#endregion

	#region SMTFeederMaterialImportLog
	/// <summary>
	/// SMT Feeder物料
	/// </summary>
	[Serializable, TableMap("TBLSMTFEEDERMATERIALIMPLOG", "LOGNO,SEQ")]
	public class SMTFeederMaterialImportLog : DomainObject
	{
		public SMTFeederMaterialImportLog()
		{
		}
 
		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("PRODUCTCODE", typeof(string), 40, false)]
		public string  ProductCode;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 机台代码
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, false)]
		public string  MachineCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 首选料
		/// </summary>
		[FieldMapAttribute("SOURCEMATERIALCODE", typeof(string), 40, false)]
		public string  SourceMaterialCode;

		/// <summary>
		/// 物料代码
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, false)]
		public string  MaterialCode;

		/// <summary>
		/// Feeder规格代码
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// 数量
		/// </summary>
		[FieldMapAttribute("QTY", typeof(decimal), 15, true)]
		public decimal  Qty;

		/// <summary>
		/// 站位代码
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, false)]
		public string  MachineStationCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LOGNO", typeof(decimal), 10, true)]
		public decimal  LOGNO;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
		public decimal  Sequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("IMPUSER", typeof(string), 40, false)]
		public string  ImportUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("IMPDATE", typeof(int), 8, true)]
		public int  ImportDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("IMPTIME", typeof(int), 6, true)]
		public int  ImportTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHECKRESULT", typeof(string), 1, true)]
		public string  CheckResult;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHECKDESC", typeof(string), 100, false)]
		public string  CheckDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TBLGRP", typeof(string), 40, false)]
		public string  TableGroup;

	}
	#endregion

	#region SMTLineControlLog
	/// <summary>
	/// 机台上料记录
	/// </summary>
	[Serializable, TableMap("TBLSMTLINECTLLOG", "LOGID")]
	public class SMTLineControlLog : DomainObject
	{
		public SMTLineControlLog()
		{
		}
 
		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("PRODUCTCODE", typeof(string), 40, false)]
		public string  ProductCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 机台代码
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, false)]
		public string  MachineCode;

		/// <summary>
		/// 站位代码
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, false)]
		public string  MachineStationCode;

		/// <summary>
		/// Feeder规格
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// Feeder代码
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, false)]
		public string  FeederCode;

		/// <summary>
		/// 料卷编号
		/// </summary>
		[FieldMapAttribute("REELNO", typeof(string), 40, false)]
		public string  ReelNo;

		/// <summary>
		/// 料卷物料代码
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, false)]
		public string  MaterialCode;

		/// <summary>
		/// 单位用量
		/// </summary>
		[FieldMapAttribute("UNITQTY", typeof(decimal), 15, true)]
		public decimal  UnitQty;

		/// <summary>
		/// 生产批号
		/// </summary>
		[FieldMapAttribute("REELQTY", typeof(decimal), 15, true)]
		public decimal  ReelQty;

		/// <summary>
		/// DateCode
		/// </summary>
		[FieldMapAttribute("REELUSEDQTY", typeof(decimal), 15, true)]
		public decimal  ReelUsedQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("NEXTREELNO", typeof(string), 40, false)]
		public string  NextReelNo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPERESCODE", typeof(string), 40, false)]
		public string  OpeResourceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPESSCODE", typeof(string), 40, false)]
		public string  OpeStepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 工单是否可用
		/// </summary>
		[FieldMapAttribute("ENABLED", typeof(string), 1, true)]
		public string  Enabled;

		/// <summary>
		/// 料卷是否停线
		/// </summary>
		[FieldMapAttribute("REELCEASEFLAG", typeof(string), 1, true)]
		public string  ReelCeaseFlag;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LOGID", typeof(decimal), 10, true)]
		public decimal  LogID;

		/// <summary>
		/// 操作类型
		/// </summary>
		[FieldMapAttribute("OPERATIONTYPE", typeof(string), 40, false)]
		public string  OperationType;

		/// <summary>
		/// 产线原有状态
		/// </summary>
		[FieldMapAttribute("LINESTATUSOLD", typeof(string), 1, true)]
		public string  LineStatusOld;

		/// <summary>
		/// 产线最新状态
		/// </summary>
		[FieldMapAttribute("LINESTATUS", typeof(string), 1, true)]
		public string  LineStatus;

		/// <summary>
		/// 是否修改产线状态
		/// </summary>
		[FieldMapAttribute("CHGLINESTATUS", typeof(string), 1, true)]
		public string  ChangeLineStatus;

	}
	#endregion

	#region SMTMachineActiveInno
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSMTMACHINEACTIVEINNO", "MOCODE,SSCODE,MACHINECODE")]
	public class SMTMachineActiveInno : DomainObject
	{
		public SMTMachineActiveInno()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string  MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string  StepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("INNO", typeof(decimal), 10, true)]
		public decimal  INNO;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, true)]
		public string  MachineCode;

	}
	#endregion

	#region SMTMachineDiscard
	/// <summary>
	/// 设备抛料导入
	/// </summary>
	[Serializable, TableMap("TBLSMTMACHINEDISCARD", "MOCODE,SSCODE,MATERIALCODE,MACHINESTATIONCODE")]
	public class SMTMachineDiscard : DomainObject
	{
		public SMTMachineDiscard()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string  MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string  StepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, true)]
		public string  MaterialCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, true)]
		public string  MachineStationCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PICKUPCOUNT", typeof(decimal), 15, true)]
		public decimal  PickupCount;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("REJECTPARTS", typeof(decimal), 15, true)]
		public decimal  RejectParts;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("NOPICKUP", typeof(decimal), 15, true)]
		public decimal  NoPickup;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ERRORPARTS", typeof(decimal), 15, true)]
		public decimal  ErrorParts;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DISLODGEDPARTS", typeof(decimal), 15, true)]
		public decimal  DislodgedParts;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
		public string  EAttribute1;

	}
	#endregion

	#region SMTMachineInno
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSMTMACHINEINNO", "INNO,INNOSEQ")]
	public class SMTMachineInno : DomainObject
	{
		public SMTMachineInno()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("PRODUCTCODE", typeof(string), 40, false)]
		public string  ProductCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 机台代码
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, false)]
		public string  MachineCode;

		/// <summary>
		/// 站位代码
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, false)]
		public string  MachineStationCode;

		/// <summary>
		/// Feeder规格
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// Feeder代码
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, false)]
		public string  FeederCode;

		/// <summary>
		/// 料卷编号
		/// </summary>
		[FieldMapAttribute("REELNO", typeof(string), 40, false)]
		public string  ReelNo;

		/// <summary>
		/// 料卷物料代码
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, false)]
		public string  MaterialCode;

		/// <summary>
		/// 单位用量
		/// </summary>
		[FieldMapAttribute("UNITQTY", typeof(decimal), 15, true)]
		public decimal  UnitQty;

		/// <summary>
		/// 生产批号
		/// </summary>
		[FieldMapAttribute("LOTNO", typeof(string), 40, false)]
		public string  LotNo;

		/// <summary>
		/// DateCode
		/// </summary>
		[FieldMapAttribute("DATECODE", typeof(string), 40, false)]
		public string  DateCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("INNO", typeof(decimal), 10, true)]
		public decimal  INNO;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("INNOSEQ", typeof(decimal), 10, true)]
		public decimal  INNOSequence;

	}
	#endregion

	#region SMTRCardInno
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSMTRCARDINNO", "RCARD,RCARDSEQ,INNO")]
	public class SMTRCardInno : DomainObject
	{
		public SMTRCardInno()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string  RunningCard;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
		public decimal  RunningCardSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("INNO", typeof(decimal), 10, true)]
		public decimal  INNO;

	}
	#endregion

	#region SMTRCardMaterial
	/// <summary>
	/// SMT的RCard上料
	/// </summary>
	[Serializable, TableMap("TBLSMTRCARDMATERIAL", "RCARD,RCARDSEQ")]
	public class SMTRCardMaterial : DomainObject
	{
		public SMTRCardMaterial()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string  RunningCard;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RCARDSEQ", typeof(decimal), 15, true)]
		public decimal  RunningCardSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
		public string  ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
		public string  ModelCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, false)]
		public string  SegmentCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, false)]
		public string  ResourceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ROUTECODE", typeof(string), 40, false)]
		public string  RouteCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, false)]
		public string  OPCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, false)]
		public string  ShiftTypeCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
		public string  ShiftCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TPCODE", typeof(string), 40, false)]
		public string  TimePeriodCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PRODUCTSTATUS", typeof(string), 40, false)]
		public string  ProductStatus;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
		public string  EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

	}
	#endregion

	#region SMTRCardMaterialDetail
	/// <summary>
	/// SMT的RCard上料
	/// </summary>
	[Serializable, TableMap("TBLSMTRCARDMATERIALDTL", "RCARD,RCARDSEQ,REELSEQ")]
	public class SMTRCardMaterialDetail : DomainObject
	{
		public SMTRCardMaterialDetail()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string  RunningCard;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RCARDSEQ", typeof(decimal), 15, true)]
		public decimal  RunningCardSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("REELSEQ", typeof(decimal), 10, true)]
		public decimal  ReelSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MACHINECODE", typeof(string), 40, false)]
		public string  MachineCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MACHINESTATIONCODE", typeof(string), 40, false)]
		public string  MachineStationCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FEEDERSPECCODE", typeof(string), 40, false)]
		public string  FeederSpecCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, false)]
		public string  FeederCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("REELNO", typeof(string), 40, false)]
		public string  ReelNo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, false)]
		public string  MaterialCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("UNITQTY", typeof(decimal), 15, true)]
		public decimal  UnitQty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LOTNO", typeof(string), 40, false)]
		public string  LotNo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATECODE", typeof(string), 40, false)]
		public string  DateCode;

	}
	#endregion

	#region SMTSensorQty
	/// <summary>
	/// SMT预警
	/// </summary>
	[Serializable, TableMap("TBLSMTSENSORQTY", "PRODUCTCODE,MOCODE,SSCODE,SHIFTDAY,SHIFTTYPECODE,SHIFTCODE,TPCODE")]
	public class SMTSensorQty : DomainObject
	{
		public SMTSensorQty()
		{
		}
 
		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("PRODUCTCODE", typeof(string), 40, true)]
		public string  ProductCode;

		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string  MOCode;

		/// <summary>
		/// 累计数量
		/// </summary>
		[FieldMapAttribute("QTY", typeof(decimal), 15, true)]
		public decimal  Qty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MAINTAINUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MAINTAINDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MAINTAINTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 产线代码
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string  StepSequenceCode;

		/// <summary>
		/// 生产日期
		/// </summary>
		[FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
		public int  ShiftDay;

		/// <summary>
		/// 班制代码
		/// </summary>
		[FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
		public string  ShiftTypeCode;

		/// <summary>
		/// 班次代码
		/// </summary>
		[FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
		public string  ShiftCode;

		/// <summary>
		/// 时段代码
		/// </summary>
		[FieldMapAttribute("TPCODE", typeof(string), 40, true)]
		public string  TPCode;

		/// <summary>
		/// 时段开始日期
		/// </summary>
		[FieldMapAttribute("TPBTIME", typeof(int), 6, true)]
		public int  TPBeginTime;

		/// <summary>
		/// 时段结束日期
		/// </summary>
		[FieldMapAttribute("TPETIME", typeof(int), 6, true)]
		public int  TPEndTime;

		/// <summary>
		/// 时段次序
		/// </summary>
		[FieldMapAttribute("TPSEQ", typeof(decimal), 10, true)]
		public decimal  TPSequence;

		/// <summary>
		/// 工单开始生产日期
		/// </summary>
		[FieldMapAttribute("MOBDATE", typeof(int), 8, true)]
		public int  MOBeginDate;

		/// <summary>
		/// 工单开始生产时间
		/// </summary>
		[FieldMapAttribute("MOBTIME", typeof(int), 6, true)]
		public int  MOBeginTime;

		/// <summary>
		/// 工单结束生产日期
		/// </summary>
		[FieldMapAttribute("MOEDATE", typeof(int), 8, true)]
		public int  MOEndDate;

		/// <summary>
		/// 工单结束生产时间
		/// </summary>
		[FieldMapAttribute("MOETIME", typeof(int), 6, true)]
		public int  MOEndTime;

		/// <summary>
		/// 数量差异原因
		/// </summary>
		[FieldMapAttribute("DIFFREASON", typeof(string), 100, false)]
		public string  DifferenceReason;

		/// <summary>
		/// 数量差异维护人员
		/// </summary>
		[FieldMapAttribute("DIFFMUSER", typeof(string), 40, false)]
		public string  DifferenceMaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DIFFMDATE", typeof(int), 8, true)]
		public int  DifferenceMaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DIFFMTIME", typeof(int), 6, true)]
		public int  DifferenceMaintainTime;

	}
	#endregion

	#region SMTTargetQty
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSMTTARGETQTY", "MOCODE,SSCODE,TPCODE")]
	public class SMTTargetQty : DomainObject
	{
		public SMTTargetQty()
		{
		}
 
		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string  MOCode;

		/// <summary>
		/// 产线代码
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string  SSCode;

		/// <summary>
		/// 时段代码
		/// </summary>
		[FieldMapAttribute("TPCODE", typeof(string), 40, true)]
		public string  TPCode;

		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("PRODUCTCODE", typeof(string), 40, false)]
		public string  ProductCode;

		/// <summary>
		/// 班制代码
		/// </summary>
		[FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, false)]
		public string  ShiftTypeCode;

		/// <summary>
		/// 班次代码
		/// </summary>
		[FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
		public string  ShfitCode;

		/// <summary>
		/// 工段代码
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string  SegmentCode;

		/// <summary>
		/// 时段开始时间
		/// </summary>
		[FieldMapAttribute("TPBTIME", typeof(int), 6, true)]
		public int  TPBeginTime;

		/// <summary>
		/// 时段结束时间
		/// </summary>
		[FieldMapAttribute("TPETIME", typeof(int), 6, true)]
		public int  TPEndTime;

		/// <summary>
		/// 时段次序
		/// </summary>
		[FieldMapAttribute("TPSEQ", typeof(decimal), 10, true)]
		public decimal  TPSequence;

		/// <summary>
		/// 时段描述
		/// </summary>
		[FieldMapAttribute("TPDESC", typeof(string), 100, false)]
		public string  TPDescription;

		/// <summary>
		/// 时段目标产量
		/// </summary>
		[FieldMapAttribute("TPQTY", typeof(decimal), 15, true)]
		public decimal  TPQty;

		/// <summary>
		/// 每小时产量
		/// </summary>
		[FieldMapAttribute("QTYPERHOUR", typeof(decimal), 15, true)]
		public decimal  QtyPerHour;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
		public string  EAttribute1;

	}
	#endregion

    #region Smtrelationqty
    /// <summary>
    /// TBLSMTRELATIONQTY
    /// </summary>
    [Serializable, TableMap("TBLSMTRELATIONQTY", "RCARD,MOCODE")]
    public class Smtrelationqty : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Smtrelationqty()
        {
        }

        ///<summary>
        ///RCARD
        ///</summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string Rcard;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string Mocode;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string Itemcode;

        ///<summary>
        ///RELATIONQTRY
        ///</summary>
        [FieldMapAttribute("RELATIONQTRY", typeof(int), 22, false)]
        public int Relationqtry;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
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
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

        ///<summary>
        ///EATTRIBUTE2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
        public string Eattribute2;

        ///<summary>
        ///EATTRIBUTE3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 40, true)]
        public string Eattribute3;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 100, true)]
        public string Memo;

    }
    #endregion

    #region Splitboard
    /// <summary>
    /// TBLSPLITBOARD
    /// </summary>
    [Serializable, TableMap("TBLSPLITBOARD", "SEQ,MOCODE,RCARD")]
    public class Splitboard : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Splitboard()
        {
        }

        ///<summary>
        ///RCARD
        ///</summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string Rcard;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string Mocode;

        ///<summary>
        ///SEQ
        ///</summary>
        [FieldMapAttribute("SEQ", typeof(int), 22, false)]
        public int Seq;

        ///<summary>
        ///SCARD
        ///</summary>
        [FieldMapAttribute("SCARD", typeof(string), 40, false)]
        public string Scard;

        ///<summary>
        ///MODELCODE
        ///</summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string Modelcode;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string Itemcode;

        ///<summary>
        ///ROUTECODE
        ///</summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string Routecode;

        ///<summary>
        ///OPCODE
        ///</summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string Opcode;

        ///<summary>
        ///SEGCODE
        ///</summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string Segcode;

        ///<summary>
        ///SSCODE
        ///</summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string Sscode;

        ///<summary>
        ///RESCODE
        ///</summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string Rescode;

        ///<summary>
        ///SHIFTTYPECODE
        ///</summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, false)]
        public string Shifttypecode;

        ///<summary>
        ///SHIFTCODE
        ///</summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string Shiftcode;

        ///<summary>
        ///TPCODE
        ///</summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, false)]
        public string Tpcode;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(int), 22, false)]
        public int Muser;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

    }
    #endregion


}

