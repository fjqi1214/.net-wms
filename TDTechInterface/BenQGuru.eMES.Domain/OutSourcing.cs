using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for OutSourcing
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2006-7-21 10:29:02
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.OutSourcing
{

	#region OutMO
	/// <summary>
	/// 外包工单主档
	/// </summary>
	[Serializable, TableMap("TBLOUTMO", "RndID")]
	public class OutMO : DomainObject
	{
		public OutMO()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RndID", typeof(string), 40, true)]
		public string  RndID;

		/// <summary>
		/// 工单
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 料号
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
		public string  ItemCode;

		/// <summary>
		/// 数量
		/// </summary>
		[FieldMapAttribute("QTY", typeof(decimal), 15, true)]
		public decimal  Qty;

		/// <summary>
		/// 外包厂
		/// </summary>
		[FieldMapAttribute("OUTFACTORY", typeof(string), 40, false)]
		public string  OutFactory;

		/// <summary>
		/// 完工日期
		/// </summary>
		[FieldMapAttribute("COMPLETEDATE", typeof(int), 8, true)]
		public int  CompleteDate;

		/// <summary>
		/// 开始序列号
		/// </summary>
		[FieldMapAttribute("STARTSN", typeof(string), 40, false)]
		public string  StartSN;

		/// <summary>
		/// 结束序列号
		/// </summary>
		[FieldMapAttribute("ENDSN", typeof(string), 40, false)]
		public string  EndSN;

		/// <summary>
		/// 备注
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 100, false)]
		public string  Memo;

		/// <summary>
		/// 导入人员
		/// </summary>
		[FieldMapAttribute("IMPORTUSER", typeof(string), 40, false)]
		public string  ImportUser;

		/// <summary>
		/// 导入日期
		/// </summary>
		[FieldMapAttribute("IMPORTDATE", typeof(int), 8, true)]
		public int  ImportDate;

		/// <summary>
		/// 导入时间
		/// </summary>
		[FieldMapAttribute("IMPORTTIME", typeof(int), 6, true)]
		public int  ImportTime;

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
		/// 类型：LOT、PCS
		/// </summary>
		[FieldMapAttribute("TYPE", typeof(string), 40, false)]
		public string  Type;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PLANQTY", typeof(decimal), 15, true)]
		public decimal  PlanQty;

	}
	#endregion

	#region OutWIP
	/// <summary>
	/// 外包工单主档
	/// </summary>
	[Serializable, TableMap("TBLOUTWIP", "RndID")]
	public class OutWIP : DomainObject
	{
		public OutWIP()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RndID", typeof(string), 40, true)]
		public string  RndID;

		/// <summary>
		/// 工单
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 结束序列号
		/// </summary>
		[FieldMapAttribute("ENDSN", typeof(string), 40, false)]
		public string  EndSN;

		/// <summary>
		/// 备注
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 100, false)]
		public string  Memo;

		/// <summary>
		/// 导入人员
		/// </summary>
		[FieldMapAttribute("IMPORTUSER", typeof(string), 40, false)]
		public string  ImportUser;

		/// <summary>
		/// 导入日期
		/// </summary>
		[FieldMapAttribute("IMPORTDATE", typeof(int), 8, true)]
		public int  ImportDate;

		/// <summary>
		/// 导入时间
		/// </summary>
		[FieldMapAttribute("IMPORTTIME", typeof(int), 6, true)]
		public int  ImportTime;

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
		/// 类型：LOT、PCS
		/// </summary>
		[FieldMapAttribute("TYPE", typeof(string), 40, false)]
		public string  Type;

		/// <summary>
		/// 开始序列号
		/// </summary>
		[FieldMapAttribute("STARTSN", typeof(string), 40, false)]
		public string  StartSN;

		/// <summary>
		/// 工序
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, false)]
		public string  OPCode;

		/// <summary>
		/// 产品状态：GOOD/NG
		/// </summary>
		[FieldMapAttribute("PRODUCTSTATUS", typeof(string), 40, false)]
		public string  ProductStatus;

		/// <summary>
		/// 不良描述
		/// </summary>
		[FieldMapAttribute("ERRORDESC", typeof(string), 100, false)]
		public string  ErrorDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("QTY", typeof(decimal), 10, true)]
		public decimal  Qty;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, false)]
		public string  StepSequenceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SHIFTDATE", typeof(string), 40, false)]
		public string  ShiftDate;

	}
	#endregion

	#region OutWIPMaterial
	/// <summary>
	/// 外包工单主档
	/// </summary>
	[Serializable, TableMap("TBLOUTWIPMATERIAL", "RndID")]
	public class OutWIPMaterial : DomainObject
	{
		public OutWIPMaterial()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RndID", typeof(string), 40, true)]
		public string  RndID;

		/// <summary>
		/// 工单
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 结束序列号
		/// </summary>
		[FieldMapAttribute("ENDSN", typeof(string), 40, false)]
		public string  EndSN;

		/// <summary>
		/// 备注
		/// </summary>
		[FieldMapAttribute("MEMO", typeof(string), 100, false)]
		public string  Memo;

		/// <summary>
		/// 导入人员
		/// </summary>
		[FieldMapAttribute("IMPORTUSER", typeof(string), 40, false)]
		public string  ImportUser;

		/// <summary>
		/// 导入日期
		/// </summary>
		[FieldMapAttribute("IMPORTDATE", typeof(int), 8, true)]
		public int  ImportDate;

		/// <summary>
		/// 导入时间
		/// </summary>
		[FieldMapAttribute("IMPORTTIME", typeof(int), 6, true)]
		public int  ImportTime;

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
		/// 类型：LOT、PCS
		/// </summary>
		[FieldMapAttribute("TYPE", typeof(string), 40, false)]
		public string  Type;

		/// <summary>
		/// 开始序列号
		/// </summary>
		[FieldMapAttribute("STARTSN", typeof(string), 40, false)]
		public string  StartSN;

		/// <summary>
		/// 物料名称
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, false)]
		public string  MaterialCode;

		/// <summary>
		/// Date Code
		/// </summary>
		[FieldMapAttribute("DATECODE", typeof(string), 40, false)]
		public string  DateCode;

		/// <summary>
		/// 供应商
		/// </summary>
		[FieldMapAttribute("SUPPLIER", typeof(string), 40, false)]
		public string  Supplier;

	}
	#endregion

	#region OutMaterial
	/// <summary>
	/// 外包工单发料主档
	/// </summary>
	[Serializable, TableMap("TBLOUTMATERIAL", "REELNO")]
	public class OutMaterial : DomainObject
	{
		public OutMaterial()
		{
		}
 
		/// <summary>
		/// 工单
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string  MOCode;

		/// <summary>
		/// 物料代码
		/// </summary>
		[FieldMapAttribute("MATERIALCODE", typeof(string), 40, true)]
		public string  MaterialCode;

		/// <summary>
		/// 料卷编号
		/// </summary>
		[FieldMapAttribute("REELNO", typeof(string), 40, true)]
		public string  ReelNo;

		/// <summary>
		/// 数量
		/// </summary>
		[FieldMapAttribute("QTY", typeof(decimal), 15, false)]
		public decimal  Qty;

		/// <summary>
		/// 是否特采
		/// </summary>
		[FieldMapAttribute("ISSPECIAL", typeof(string), 1, false)]
		public string  IsSpecial;

		/// <summary>
		/// 是否外包发料
		/// </summary>
		[FieldMapAttribute("ISOUTMATERIAL", typeof(string), 1, false)]
		public string  IsOutMaterial;

		/// <summary>
		/// 维护用户
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 维护日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(decimal), 8, false)]
		public decimal  MaintainDate;
	}
	#endregion

}

