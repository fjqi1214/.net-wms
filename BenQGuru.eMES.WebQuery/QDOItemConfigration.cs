using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WebQuery
{
	/// <summary>
	/// QDOItemConfigration 的摘要说明。
	/// </summary>
	public class QDOItemConfigration : DomainObject 
	{
		/// <summary>
		/// 产品序列号
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, false)]
		public string  RunningCard;

		/// <summary>
		/// 序号
		/// </summary>
		[FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, false)]
		public decimal  RunningCardSequence;

		/// <summary>
		/// 员工工号
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 采集时间
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 采集日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 属性
		/// </summary>
		[FieldMapAttribute("CheckItemCode", typeof(string), 40, false)]
		public string  CheckItemCode;

		/// <summary>
		/// 配置大类
		/// </summary>
		[FieldMapAttribute("CatergoryCode", typeof(string), 40, false)]
		public string  CatergoryCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, true)]
		public string  EAttribute1;

		/// <summary>
		/// 时间段代码
		/// </summary>
		[FieldMapAttribute("TPCODE", typeof(string), 40, true)]
		public string  TimePeriodCode;

		/// <summary>
		/// 班次代码
		/// </summary>
		[FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
		public string  ShiftCode;

		/// <summary>
		/// 班制代码
		/// </summary>
		[FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
		public string  ShiftTypeCode;

		/// <summary>
		/// 采集资源
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string  ResourceCode;

		/// <summary>
		/// 采集工位
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, true)]
		public string  OPCode;

		/// <summary>
		/// 采集线别
		/// </summary>
		[FieldMapAttribute("SSCODE", typeof(string), 40, true)]
		public string  StepSequenceCode;

		/// <summary>
		/// 工段代码
		/// </summary>
		[FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
		public string  SegmnetCode;

		/// <summary>
		/// 生产途程代码
		/// </summary>
		[FieldMapAttribute("ROUTECODE", typeof(string), 40, false)]
		public string  RouteCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 标准值
		/// </summary>
		[FieldMapAttribute("CheckItemVlaue", typeof(string), 40, false)]
		public string  CheckItemVlaue;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MoCode", typeof(string), 40, false)]
		public string  MoCode;

		/// <summary>
		/// 实际值
		/// </summary>
		[FieldMapAttribute("ActValue", typeof(string), 40, true)]
		public string  ActValue;

		/// <summary>
		/// 配置码
		/// </summary>
		[FieldMapAttribute("ItemConfig", typeof(string), 40, true)]
		public string  ItemConfig;

	}
}
