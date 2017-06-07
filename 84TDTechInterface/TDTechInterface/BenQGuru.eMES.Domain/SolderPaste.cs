using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for SolderPaste
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
/// ** 日 期:		2006-7-17 10:24:39
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.SolderPaste
{

	#region SolderPaste
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSOLDERPASTE", "SPID")]
	public class SolderPaste : DomainObject
	{
		public SolderPaste()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SPID", typeof(string), 40, true)]
		public string  SolderPasteID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LOTNO", typeof(string), 40, true)]
		public string  LotNO;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PRODATE", typeof(int), 8, true)]
		public int  ProductionDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EXDATE", typeof(int), 8, true)]
		public int  ExpiringDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("Status", typeof(string), 40, true)]
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
		[FieldMapAttribute("PARTNO", typeof(string), 40, true)]
		public string  PartNO;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("USED", typeof(string), 1, true)]
		public string  Used;

	}
	#endregion

	#region SolderPaste2Item
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSOLDERPASTE2ITEM", "ITEMCODE")]
	public class SolderPaste2Item : DomainObject
	{
		public SolderPaste2Item()
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
		[FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
		public string  eAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SPTYPE", typeof(string), 40, true)]
		public string  SolderPasteType;

	}
	#endregion

	#region SolderPasteControl
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSOLDERPASTECONTROL", "PARTNO")]
	public class SolderPasteControl : DomainObject
	{
		public SolderPasteControl()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARTNO", typeof(string), 40, true)]
		public string  PartNO;

		/// <summary>
		/// [类型：有铅、无铅、高温、锡锑]
		/// </summary>
		[FieldMapAttribute("TYPE", typeof(string), 40, true)]
		public string  Type;

		/// <summary>
		/// [开封时长]
		/// </summary>
		[FieldMapAttribute("OPENTS", typeof(decimal), 15, true)]
		public decimal  OpenTimeSpan;

		/// <summary>
		/// [未开封时长]
		/// </summary>
		[FieldMapAttribute("UNOPENTS", typeof(decimal), 15, true)]
		public decimal  UnOpenTimeSpan;

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
		/// [回温时长]
		/// </summary>
		[FieldMapAttribute("ReturnTimeSpan", typeof(decimal), 15, true)]
		public decimal  ReturnTimeSpan;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("GUARANTEEPERIOD", typeof(int), 10, true)]
		public int  GuaranteePeriod;

	}
	#endregion

	#region SOLDERPASTEPRO
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSOLDERPASTEPRO", "SPPKID")]
	public class SOLDERPASTEPRO : DomainObject
	{
		public SOLDERPASTEPRO()
		{
		}
 
		/// <summary>
		/// 使用次数
		/// </summary>
		[FieldMapAttribute("SEQUENCE", typeof(int), 6, true)]
		public int  SEQUENCE;

		/// <summary>
		/// 领用人员
		/// </summary>
		[FieldMapAttribute("OPENUSER", typeof(string), 40, false)]
		public string  OPENUSER;

		/// <summary>
		/// 领用时间
		/// </summary>
		[FieldMapAttribute("OPENTIME", typeof(int), 6, false)]
		public int  OPENTIME;

		/// <summary>
		/// 回存人员
		/// </summary>
		[FieldMapAttribute("RESAVEUSER", typeof(string), 40, false)]
		public string  RESAVEUSER;

		/// <summary>
		/// 回存日期
		/// </summary>
		[FieldMapAttribute("RESAVEDATE", typeof(int), 8, false)]
		public int  RESAVEDATE;

		/// <summary>
		/// 锡膏ID
		/// </summary>
		[FieldMapAttribute("SOLDERPASTEID", typeof(string), 40, false)]
		public string  SOLDERPASTEID;

		/// <summary>
		/// 备注
		/// </summary>
		[FieldMapAttribute("EATTRIBUTE1", typeof(string), 100, false)]
		public string  EATTRIBUTE1;

		/// <summary>
		/// 维护用户
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MUSER;

		/// <summary>
		/// 维护日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MDATE;

		/// <summary>
		/// 维护时间
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MTIME;

		/// <summary>
		/// 作废人员
		/// </summary>
		[FieldMapAttribute("UNAVIALUSER", typeof(string), 40, false)]
		public string  UNAVIALUSER;

		/// <summary>
		/// 作废日期
		/// </summary>
		[FieldMapAttribute("UNAVIALDATE", typeof(int), 8, false)]
		public int  UNAVIALDATE;

		/// <summary>
		/// 回存时间
		/// </summary>
		[FieldMapAttribute("RESAVETIME", typeof(int), 6, false)]
		public int  RESAVETIME;

		/// <summary>
		/// 作废时间
		/// </summary>
		[FieldMapAttribute("UNAVIALTIME", typeof(int), 6, false)]
		public int  UNAVIALTIME;

		/// <summary>
		/// 产线代码
		/// </summary>
		[FieldMapAttribute("LINECODE", typeof(string), 40, false)]
		public string  LINECODE;

		/// <summary>
		/// 工单代码
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCODE;

		/// <summary>
		/// 领用日期
		/// </summary>
		[FieldMapAttribute("OPENDATE", typeof(int), 8, false)]
		public int  OPENDATE;

		/// <summary>
		/// 状态：
		/// 正常/限制使用/报废/用完/回存
		/// </summary>
		[FieldMapAttribute("STATUS", typeof(string), 40, false)]
		public string  STATUS;

		/// <summary>
		/// 回温时长
		/// </summary>
		[FieldMapAttribute("RETURNTIMESPAN", typeof(decimal), 10, false)]
		public decimal  RETURNTIMESPAN;

		/// <summary>
		/// 取出未开封时长
		/// </summary>
		[FieldMapAttribute("VEILTIMESPAN", typeof(decimal), 10, false)]
		public decimal  VEILTIMESPAN;

		/// <summary>
		/// 开封时长
		/// </summary>
		[FieldMapAttribute("UNVEILTIMESPAN", typeof(decimal), 10, false)]
		public decimal  UNVEILTIMESPAN;

		/// <summary>
		/// 实效日期
		/// </summary>
		[FieldMapAttribute("EXPIREDDATE", typeof(int), 8, true)]
		public int  EXPIREDDATE;

		/// <summary>
		/// 备注
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 100, false)]
		public string  MEMO;

		/// <summary>
		/// 搅拌人员
		/// </summary>
		[FieldMapAttribute("AGITAEUSER", typeof(string), 40, false)]
		public string  AGITAEUSER;

		/// <summary>
		/// 搅拌日期
		/// </summary>
		[FieldMapAttribute("AGITATEDATE", typeof(int), 8, false)]
		public int  AGITATEDATE;

		/// <summary>
		/// 搅拌时间
		/// </summary>
		[FieldMapAttribute("AGITATETIME", typeof(int), 6, false)]
		public int  AGITATETIME;

		/// <summary>
		/// 开封人员
		/// </summary>
		[FieldMapAttribute("UNVEILUSER", typeof(string), 40, false)]
		public string  UNVEILUSER;

		/// <summary>
		/// 开封日期
		/// </summary>
		[FieldMapAttribute("UNVEILMDATE", typeof(int), 8, false)]
		public int  UNVEILMDATE;

		/// <summary>
		/// 开封时间
		/// </summary>
		[FieldMapAttribute("UNVEILTIME", typeof(int), 6, false)]
		public int  UNVEILTIME;

		/// <summary>
		/// 锡膏类型
		/// </summary>
		[FieldMapAttribute("SPTYPE", typeof(string), 40, false)]
		public string  SPTYPE;

		/// <summary>
		/// 料号
		/// </summary>
		[FieldMapAttribute("LOTNO", typeof(string), 40, true)]
		public string  LOTNO;

		/// <summary>
		/// 回温计时
		/// </summary>
		[FieldMapAttribute("RETURNCOUNTTIME", typeof(decimal), 15, false)]
		public decimal  RETURNCOUNTTIME;

		/// <summary>
		/// 开封计时
		/// </summary>
		[FieldMapAttribute("UNVEILCOUNTTIME", typeof(decimal), 15, false)]
		public decimal  UNVEILCOUNTTIME;

		/// <summary>
		/// 未开封计时
		/// </summary>
		[FieldMapAttribute("VEILCOUNTTIME", typeof(decimal), 15, false)]
		public decimal  VEILCOUNTTIME;

		/// <summary>
		/// 主键
		/// </summary>
		[FieldMapAttribute("SPPKID", typeof(string), 40, true)]
		public string  SPPKID;

		/// <summary>
		/// 回温人员
		/// </summary>
		[FieldMapAttribute("RETRUNUSER", typeof(string), 40, false)]
		public string  RETRUNUSER;

		/// <summary>
		/// 回温日期
		/// </summary>
		[FieldMapAttribute("RETURNDATE", typeof(int), 8, false)]
		public int  RETURNDATE;

		/// <summary>
		/// 回温时间
		/// </summary>
		[FieldMapAttribute("RETURNTIME", typeof(int), 6, false)]
		public int  RETURNTIME;

	}
	#endregion

}

