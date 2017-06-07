using System;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 

namespace  BenQGuru.eMES.SMT
{
	/// <summary>
	/// StationBOM 的摘要说明。
	/// 文件名:		QueryDomainObject.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		Simone Xu
	/// 创建日期:	2005/06/20
	/// 修改人:
	/// 修改日期:
	/// 描 述:		导入站表实体,用于比对,数据库无此表
	/// 版 本:	
	/// </summary>
	[Serializable]
	public class StationBOM : DomainObject
	{
		/// 所属工单[MOCODE]
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MOCode;

		/// <summary>
		/// 机台代码[RESCODE]
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string ResourceCode;

		/// <summary>
		/// 站位编号[StationCode]
		/// </summary>
		[FieldMapAttribute("StationCode", typeof(string), 40, true)]
		public string  StationCode;

		/// <summary>
		/// Feeder规格代码[FEEDERCODE]
		/// </summary>
		[FieldMapAttribute("FEEDERCODE", typeof(string), 40, true)]
		public string  FeederCode;

		/// <summary>
		/// 夏新物料码[OBITEMCODE]
		/// </summary>
		[FieldMapAttribute("OBITEMCODE", typeof(string), 40, true)]
		public string  OBItemCode;


		/// <summary>
		/// 比对结果[CompareResult]
		/// </summary>
		[FieldMapAttribute("CompareResult", typeof(string), 40, true)]
		public string  CompareResult;
	}

	/// <summary>
	/// MOItemBOM 的摘要说明。
	/// 文件名:		QueryDomainObject.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		Simone Xu
	/// 创建日期:	2005/06/20
	/// 修改人:
	/// 修改日期:
	/// 描 述:		导入工单物料清单实体,用于比对,数据库无此表
	/// 版 本:	
	/// </summary>
	[Serializable]
	public class MOItemBOM : DomainObject
	{

		/// 所属工单[MOCODE]
		/// </summary>
		[FieldMapAttribute("MOCODE", typeof(string), 40, true)]
		public string MOCode;

		/// <summary>
		/// 产品代码[ITEMCODE]
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 子阶料号[ITEMCODE]
		/// </summary>
		[FieldMapAttribute("OBITEMCODE", typeof(string), 40, true)]
		public string  OBItemCode;

		/// <summary>
		/// 子阶料名称[OBITEMNAME]
		/// </summary>
		[FieldMapAttribute("OBITEMNAME", typeof(string), 40, true)]
		public string  OBItemName;

		/// <summary>
		/// 单机用量[ITEMCODE]
		/// </summary>
		[FieldMapAttribute("OBITEMQTY", typeof(string), 40, true)]
		public string  OBItemQTY;

		/// <summary>
		/// 计量单位[OBITEMUNIT]
		/// </summary>
		[FieldMapAttribute("OBITEMUNIT", typeof(string), 40, true)]
		public string  OBItemUnit;

		/// <summary>
		/// 比对结果[CompareResult]
		/// </summary>
		[FieldMapAttribute("CompareResult", typeof(string), 40, true)]
		public string  CompareResult;
	}

	/// <summary>
	/// 导入错误信息 的摘要说明。
	/// 文件名:		QueryDomainObject.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		Simone Xu
	/// 创建日期:	2005/06/20
	/// 修改人:
	/// 修改日期:
	/// 描 述:		导入站表的批量错误信息
	/// 版 本:	
	/// </summary>
	[Serializable]
	public class ErrorMessage : DomainObject
	{
		/// 错误栏位[MOCODE]
		/// </summary>
		[FieldMapAttribute("Errorolumn", typeof(string), 40, true)]
		public string Errorolumn;

		/// <summary>
		/// 错误代码[ITEMCODE]
		/// </summary>
		[FieldMapAttribute("ErrorCode", typeof(string), 40, true)]
		public string  ErrorCode;

		/// <summary>
		/// 错误原因[ITEMCODE]
		/// </summary>
		[FieldMapAttribute("ErrorReason", typeof(string), 40, true)]
		public string  ErrorReason;

	}

	/// <summary>
	/// 工单产量
	/// </summary>
	[Serializable]
	public class SMTRptLineQtyMO : DomainObject
	{
		[FieldMapAttribute("ProductCode", typeof(string), 40, true)]
		public string ProductCode;
		
		[FieldMapAttribute("MOCode", typeof(string), 40, true)]
		public string MOCode;
		
		[FieldMapAttribute("PlanQty", typeof(decimal), 40, true)]
		public decimal PlanQty;
		
		[FieldMapAttribute("PlanManHour", typeof(decimal), 40, true)]
		public decimal PlanManHour;
		
		[FieldMapAttribute("CurrentQty", typeof(decimal), 40, true)]
		public decimal CurrentQty;
		
		[FieldMapAttribute("ActualManHour", typeof(decimal), 40, true)]
		public decimal ActualManHour;
				
		[FieldMapAttribute("ActualQty", typeof(decimal), 40, true)]
		public decimal ActualQty;
		
		[FieldMapAttribute("DifferenceQty", typeof(decimal), 40, true)]
		public decimal DifferenceQty;
		
		[FieldMapAttribute("MOComPassRate", typeof(decimal), 40, true)]
		public decimal MOComPassRate;

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

	}

	/// <summary>
	/// 工单时段产量
	/// </summary>
	[Serializable]
	public class SMTRptLineQtyTimePeriod : DomainObject
	{
		[FieldMapAttribute("ProductCode", typeof(string), 40, true)]
		public string ProductCode;
		
		[FieldMapAttribute("MOCode", typeof(string), 40, true)]
		public string MOCode;
		
		[FieldMapAttribute("ShiftDay", typeof(decimal), 40, true)]
		public decimal ShiftDay;
		
		[FieldMapAttribute("TPCode", typeof(string), 40, true)]
		public string TPCode;
		
		[FieldMapAttribute("TPDesc", typeof(string), 40, true)]
		public string TPDescription;
		
		[FieldMapAttribute("CurrentQty", typeof(decimal), 40, true)]
		public decimal CurrentQty;
		
		[FieldMapAttribute("ActualQty", typeof(decimal), 40, true)]
		public decimal ActualQty;
		
		[FieldMapAttribute("DifferenceQty", typeof(decimal), 40, true)]
		public decimal DifferenceQty;
		
		[FieldMapAttribute("TPComPassRate", typeof(decimal), 40, true)]
		public decimal TPComPassRate;
		
		[FieldMapAttribute("DIFFREASON", typeof(string), 100, true)]
		public string MaintainReason;

		[FieldMapAttribute("DIFFMUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		[FieldMapAttribute("DIFFMDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		[FieldMapAttribute("DIFFMTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		[FieldMapAttribute("DAY", typeof(int), 8, true)]
		public int  Day;

		[FieldMapAttribute("TPBTIME", typeof(int), 6, true)]
		public int  TPBeginTime;

		[FieldMapAttribute("TPETIME", typeof(int), 6, true)]
		public int  TPEndTime;

	}

	/// <summary>
	/// 工单用料消耗
	/// </summary>
	[Serializable]
	public class SMTRptMOMaterial : DomainObject
	{
		[FieldMapAttribute("ProductCode", typeof(string), 40, true)]
		public string ProductCode;
		
		[FieldMapAttribute("MOCode", typeof(string), 40, true)]
		public string MOCode;
		
		[FieldMapAttribute("SSCode", typeof(string), 40, true)]
		public string StepSequenceCode;
		
		[FieldMapAttribute("MaterialCode", typeof(string), 40, true)]
		public string MaterialCode;
		
		[FieldMapAttribute("MachineCode", typeof(string), 40, true)]
		public string MachineCode;
		
		[FieldMapAttribute("MachineStationCode", typeof(string), 40, true)]
		public string MachineStationCode;
		
		[FieldMapAttribute("LogicalUsedQty", typeof(decimal), 40, true)]
		public decimal LogicalUsedQty;
		
		[FieldMapAttribute("ActualUsedQty", typeof(decimal), 40, true)]
		public decimal ActualUsedQty;
		
		[FieldMapAttribute("MachineDiscardRate", typeof(decimal), 40, true)]
		public decimal MachineDiscardRate;
		
		[FieldMapAttribute("MachineDiscardQty", typeof(decimal), 100, true)]
		public decimal MachineDiscardQty;

		[FieldMapAttribute("ManualDiscardRate", typeof(decimal), 40, false)]
		public decimal  ManualDiscardRate;

		[FieldMapAttribute("ManualDiscardQty", typeof(decimal), 8, true)]
		public decimal  ManualDiscardQty;
	}
	
}
