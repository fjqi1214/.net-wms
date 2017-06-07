using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WebQuery
{
	public class QDOMultiMOMemo : DomainObject 
	{
		/// <summary>
		/// 产品序列号
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, false)]
		public string  RunningCard;

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
		/// 备注
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  Meno;

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
		/// 
		/// </summary>
		[FieldMapAttribute("MoCode", typeof(string), 40, false)]
		public string  MoCode;
	}
	
}
