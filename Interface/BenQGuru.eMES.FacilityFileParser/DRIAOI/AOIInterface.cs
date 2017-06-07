using System;
using System.Collections;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.FacilityFileParser
{
	/// <summary>
	/// AOI DATA 的摘要说明。
	/// </summary>
	public class AOIData
	{
		public AOIData()
		{
		}

		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ITEMCODE;

		/// <summary>
		/// 产品序列号
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string  RCARD;

		/// <summary>
		/// 资源或者产线代码
		/// </summary>
		[FieldMapAttribute("RESOURCE", typeof(string), 40, true)]
		public string  RESOURCE;

		/// <summary>
		/// 出货单
		/// </summary>
		[FieldMapAttribute("SHIP", typeof(string), 40, true)]
		public string  SHIP;

		/// <summary>
		/// 用户
		/// </summary>
		[FieldMapAttribute("USER", typeof(string), 40, true)]
		public string USER;

		/// <summary>
		/// 日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public string MDATE;

		/// <summary>
		/// 时间
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 8, true)]
		public string MTIME;

		/// <summary>
		/// 不良代码统计
		/// </summary>
		[FieldMapAttribute("ERRORCOUNT", typeof(int), 8, true)]
		public string ERRORCOUNT;

		/// <summary>
		/// 不良代码
		/// </summary>
		[FieldMapAttribute("ERRORCODES", typeof(int), 1000, true)]
		public string ERRORCODES;
	}
}
