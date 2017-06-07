using System;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Domain.MOModel;

namespace  BenQGuru.eMES.MOModel
{
	/// <summary>
	/// ItemOfModel 的摘要说明。
	/// 文件名:		QueryDomainObject.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		Simone Xu
	/// 创建日期:	2005/06/20
	/// 修改人:
	/// 修改日期:
	/// 描 述:		继承自Item，包含Model的ModelCode信息
	/// 版 本:	
	/// </summary>
	[Serializable]
	public class ItemOfModel : Item
	{
		/// <summary>
		/// 所属机种[MODELCODE]
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;
	}

	/// <summary>
	/// 产品途程工序的工序序号
	/// </summary>
	[Serializable]
	public class OPSeqsence : DomainObject
	{
		/// <summary>
		/// MaxSeq	最大序号
		/// </summary>
		[FieldMapAttribute("MAXSEQ", typeof(decimal), 10, true)]
		public decimal  MaxSeq;

		/// <summary>
		/// MinSeq	最小序号
		/// </summary>
		[FieldMapAttribute("MINSEQ", typeof(decimal), 10, true)]
		public decimal  MinSeq;
	}
	
}
