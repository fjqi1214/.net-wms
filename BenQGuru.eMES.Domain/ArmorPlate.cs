using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for ArmorPlate
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
/// ** 日 期:		2006-7-25 11:37:01
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.ArmorPlate
{

	#region ArmorPlate
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBlARMORPLATE", "APID")]
	public class ArmorPlate : DomainObject
	{
		public ArmorPlate()
		{
		}
 
		/// <summary>
		/// [厂内编号]
		/// </summary>
		[FieldMapAttribute("APID", typeof(string), 40, true)]
		public string  ArmorPlateID;

		/// <summary>
		/// [基板料号]
		/// </summary>
		[FieldMapAttribute("BPCODE", typeof(string), 40, true)]
		public string  BasePlateCode;

		/// <summary>
		/// [版本]
		/// </summary>
		[FieldMapAttribute("VERSION", typeof(string), 40, true)]
		public string  Version;

		/// <summary>
		/// [厚度]
		/// </summary>
		[FieldMapAttribute("THICKNESS", typeof(decimal), 15, true)]
		public decimal  Thickness;

		/// <summary>
		/// [进厂日期]
		/// </summary>
		[FieldMapAttribute("INFACTORYDATE", typeof(int), 8, true)]
		public int InFactoryDate;

		/// <summary>
		/// [进厂时间]
		/// </summary>
		[FieldMapAttribute("INFACTORYTIME", typeof(int), 6, true)]
		public int InFactoryTime;

		/// <summary>
		/// [累计使用次数]
		/// </summary>
		[FieldMapAttribute("USEDTIMES", typeof(decimal), 10, true)]
		public decimal  UsedTimes;

		/// <summary>
		/// [当前状态：使用/为使用]
		/// </summary>
		[FieldMapAttribute("STATUS", typeof(string), 40, true)]
		public string  Status;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
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
		[FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
		public string  eAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TENSIONA", typeof(decimal), 15, true)]
		public decimal  TensionA;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TENSIONB", typeof(decimal), 15, true)]
		public decimal  TensionB;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TENSIONC", typeof(decimal), 15, true)]
		public decimal  TensionC;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TENSIOND", typeof(decimal), 15, true)]
		public decimal  TensionD;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TENSIONE", typeof(decimal), 15, true)]
		public decimal  TensionE;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 100, false)]
		public string  Memo;

		/// <summary>
		/// [厂商编号]
		/// </summary>
		[FieldMapAttribute("MANUFACTURERSN", typeof(string), 40, false)]
		public string  ManufacturerSN;

		/// <summary>
		/// [联板比例]
		/// </summary>
		[FieldMapAttribute("LBRATE", typeof(decimal), 10, true)]
		public decimal  LBRate;

		public string WithItems = string.Empty;
		public ArmorPlate2Item[] Items; 

	}
	#endregion

	#region ArmorPlate2Item
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLAP2ITEM", "ITEMCODE,APID")]
	public class ArmorPlate2Item : DomainObject
	{
		public ArmorPlate2Item()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
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
		[FieldMapAttribute("eArrtibute1", typeof(string), 40, false)]
		public string  eArrtibute1;

		/// <summary>
		/// [厂内编号]
		/// </summary>
		[FieldMapAttribute("APID", typeof(string), 40, true)]
		public string  ArmorPlateID;

	}
	#endregion

	#region ArmorPlateContol
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBlARMORPLATECONTROL", "OID")]
	public class ArmorPlateContol : DomainObject
	{
		public ArmorPlateContol()
		{
		}
 
		/// <summary>
		/// [累计使用次数]
		/// </summary>
		[FieldMapAttribute("USEDTIMES", typeof(decimal), 10, true)]
		public decimal  UsedTimes;

		/// <summary>
		/// [当前状态：使用/未使用]
		/// </summary>
		[FieldMapAttribute("STATUS", typeof(string), 40, true)]
		public string  Status;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
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
		[FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
		public string  eAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TENSIONA", typeof(decimal), 15, true)]
		public decimal  TensionA;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TENSIONB", typeof(decimal), 15, true)]
		public decimal  TensionB;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TENSIONC", typeof(decimal), 15, true)]
		public decimal  TensionC;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TENSIOND", typeof(decimal), 15, true)]
		public decimal  TensionD;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TENSIONE", typeof(decimal), 15, true)]
		public decimal  TensionE;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 100, false)]
		public string  Memo;

		/// <summary>
		/// [厂商编号]
		/// </summary>
		[FieldMapAttribute("MANUFACTURERSN", typeof(string), 40, false)]
		public string  ManufacturerSN;

		/// <summary>
		/// [联板比例]
		/// </summary>
		[FieldMapAttribute("LBRATE", typeof(decimal), 10, true)]
		public decimal  LBRate;

		/// <summary>
		/// [基板料号]
		/// </summary>
		[FieldMapAttribute("BPCODE", typeof(string), 40, true)]
		public string  BasePlateCode;

		/// <summary>
		/// [版本]
		/// </summary>
		[FieldMapAttribute("VERSION", typeof(string), 40, true)]
		public string  Version;

		/// <summary>
		/// [厚度]
		/// </summary>
		[FieldMapAttribute("THICKNESS", typeof(decimal), 15, true)]
		public decimal  Thickness;

		/// <summary>
		/// [GUID]
		/// </summary>
		[FieldMapAttribute("OID", typeof(string), 40, true)]
		public string  OID;

		/// <summary>
		/// [领用工单]
		/// </summary>
		[FieldMapAttribute("USEDMOCODE", typeof(string), 40, true)]
		public string  UsedMOCode;

		/// <summary>
		/// [领用产线]
		/// </summary>
		[FieldMapAttribute("USEDSSCODE", typeof(string), 40, true)]
		public string  UsedSSCode;

		/// <summary>
		/// [领用人员]
		/// </summary>
		[FieldMapAttribute("UUSER", typeof(string), 40, true)]
		public string  UsedUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("UDATE", typeof(int), 8, true)]
		public int  UsedDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("UTIME", typeof(int), 6, true)]
		public int  UsedTime;

		/// <summary>
		/// [钢板在当前工单的使用次数]
		/// </summary>
		[FieldMapAttribute("USEDTIMESINMO", typeof(decimal), 10, true)]
		public decimal  UsedTimesInMO;

		/// <summary>
		/// [退回人员]
		/// </summary>
		[FieldMapAttribute("RUSER", typeof(string), 40, false)]
		public string  ReturnUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RDATE", typeof(int), 8, false)]
		public int  ReturnDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RTIME", typeof(int), 6, false)]
		public int  ReturnTime;

		/// <summary>
		/// [厂内编号]
		/// </summary>
		[FieldMapAttribute("APID", typeof(string), 40, true)]
		public string  ArmorPlateID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("UNITPRINT", typeof(int), 22, true)]
        public int UnitPrint;


	}
	#endregion

	#region ArmorPlateStatusChangeList
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLAPSTATUSLIST", "OID")]
	public class ArmorPlateStatusChangeList : DomainObject
	{
		public ArmorPlateStatusChangeList()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("BPCODE", typeof(string), 40, true)]
		public string  BasePlateCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("STATUS", typeof(string), 40, true)]
		public string  Status;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OLDSTATUS", typeof(string), 40, true)]
		public string  OldStatus;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
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
		[FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
		public string  eAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 100, false)]
		public string  Memo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OID", typeof(string), 40, true)]
		public string  OID;

		/// <summary>
		/// [厂内编号]
		/// </summary>
		[FieldMapAttribute("APID", typeof(string), 40, true)]
		public string  ArmorPlateID;

	}
	#endregion

	#region ArmorPlateVersionChangeList
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLAPVCHANGELIST", "OID")]
	public class ArmorPlateVersionChangeList : DomainObject
	{
		public ArmorPlateVersionChangeList()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OID", typeof(string), 40, true)]
		public string  OID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("BPCODE", typeof(string), 40, true)]
		public string  BasePlateCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("Version", typeof(string), 40, true)]
		public string  Version;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OLDVERSION", typeof(string), 40, true)]
		public string  OldVersion;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
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
		[FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
		public string  eAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 100, false)]
		public string  Memo;

		/// <summary>
		/// [厂内编号]
		/// </summary>
		[FieldMapAttribute("APID", typeof(string), 40, true)]
		public string  ArmorPlateID;

	}
	#endregion

}

