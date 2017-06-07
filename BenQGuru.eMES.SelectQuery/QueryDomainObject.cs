using System;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Rework;

namespace BenQGuru.eMES.SelectQuery
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
	public class MO2Item : MO
	{
		/// <summary>
		/// 料品名称[ItemName]
		/// </summary>
		[FieldMapAttribute("ITEMNAME", typeof(string), 100, false)]
		public string  ItemName;
	}

	//返工单选择对象
	[Serializable]
	public class SelectReworkSheet : ReworkSheet
	{
		/// <summary>
		/// 料品名称[ItemName]
		/// </summary>
		[FieldMapAttribute("ITEMNAME", typeof(string), 100, false)]
		public string  ItemName;
	}

	[Serializable]
	public class ErrorGroup2CodeSelect : DomainObject
	{
		/// <summary>
		/// 不良代码组代码
		/// </summary>
		[FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
		public string  ErrorCodeGroup;

		/// <summary>
		/// 不良代码组描述
		/// </summary>
		[FieldMapAttribute("ECGDESC", typeof(string), 100, false)]
		public string  ErrorCodeGroupDescription;

		/// <summary>
		/// 不良代码代码
		/// </summary>
		[FieldMapAttribute("ECODE", typeof(string), 40, true)]
		public string  ErrorCode;

		/// <summary>
		/// 不良代码描述
		/// </summary>
		[FieldMapAttribute("ECDESC", typeof(string), 100, false)]
		public string  ErrorDescription;
	}
}
