using System;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain; 
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.BaseSetting
{
	/// <summary>
	/// RouteOfRouteAlt 的摘要说明。
	/// 文件名:		QueryDomainObject.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		sammer kong
	/// 创建日期:	2005/04/05
	/// 修改人:
	/// 修改日期:
	/// 描 述:		
	/// 版 本:	
	/// </summary>
	[Serializable]
	public class ModuleWithViewValue : Module
	{
		[FieldMapAttribute("VIEWVALUE", typeof(string), 40, true)]
		public string ViewValue;
		
	}

	/// <summary>
	/// RouteOfRouteAlt 的摘要说明。
	/// 文件名:		QueryDomainObject.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		Jane Shu
	/// 创建日期:	2005/03/18
	/// 修改人:
	/// 修改日期:
	/// 描 述:		继承自Route，包含Route的RouteAlt及RouteSequence信息
	/// 版 本:	
	/// </summary>
	[Serializable]
	public class RouteOfRouteAlt : Route
	{
		/// <summary>
		/// 工作途程群组名称
		/// </summary>
		[FieldMapAttribute("ROUTEALTCODE", typeof(string), 40, true)]
		public string  RouteAltCode;	
	
		/// <summary>
		/// 生产途程顺序
		/// </summary>
		[FieldMapAttribute("ROUTESEQ", typeof(decimal), 10, true)]
		public decimal  RouteSequence;
	}

	/// <summary>
	/// ResourceOfOperation 的摘要说明。
	/// 文件名:		QueryDomainObject.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		Jane Shu
	/// 创建日期:	2005/03/18
	/// 修改人:
	/// 修改日期:
	/// 描 述:		继承自Resource，包含Resource的OperationCode及ResourceSequence信息
	/// 版 本:	
	/// </summary>
	[Serializable]
	public class ResourceOfOperation : Resource
	{		
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RESSEQ", typeof(decimal), 10, true)]
		public decimal  ResourceSequence;

		/// <summary>
		/// 工序代码
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, true)]
		public string  OPCode;
	}

	/// <summary>
	/// OperationOfRoute 的摘要说明。
	/// 文件名:		QueryDomainObject.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		Jane Shu
	/// 创建日期:	2005/03/18
	/// 修改人:
	/// 修改日期:
	/// 描 述:		继承自Operation，包含Operation的RouteCode及OPSequence信息
	/// 版 本:	
	/// </summary>
	[Serializable]
	public class OperationOfRoute : Operation
	{
		
		/// <summary>
		/// 生产途程代码
		/// </summary>
		[FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
		public string  RouteCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPSEQ", typeof(decimal), 10, true)]
		public decimal  OPSequence;
	}



	/// <summary>
	/// Right 的摘要说明。
	/// 文件名:		QueryDomainObject.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		Jane Shu
	/// 创建日期:	2005/03/18
	/// 修改人:
	/// 修改日期:
	/// 描 述:		继承自Operation，包含Operation的RouteCode及OPSequence信息
	/// 版 本:	
	/// </summary>
	[Serializable]
	public class FormRight : DomainObject
	{		
		/// <summary>
		/// 生产途程代码
		/// </summary>
		[FieldMapAttribute("MDLCODE", typeof(string), 40, true)]
		public string  ModuleCode;

		/// <summary>
		///
		/// </summary>
		[FieldMapAttribute("VIEWVALUE", typeof(string), 40, false)]
		public string ViewValue;
	}

	/// <summary>
	/// Right 的摘要说明。
	/// 文件名:		QueryDomainObject.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		Jane Shu
	/// 创建日期:	2005/03/18
	/// 修改人:
	/// 修改日期:
	/// 描 述:		继承自Operation，包含Operation的RouteCode及OPSequence信息
	/// 版 本:	
	/// </summary>
	[Serializable]
	public class MenuWithUrl : Menu
	{
		/// <summary>
		/// 页面Url
		/// </summary>
		[FieldMapAttribute("FORMURL", typeof(string), 100, false)]
		public string  FormUrl;
	}
}
