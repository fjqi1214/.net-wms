using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for DeviceInterface
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2006-5-30 09:13:33 上午
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.DeviceInterface
{

	#region PreTestValue
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("tblPreTestValue", "ID")]
	public class PreTestValue : DomainObject
	{
		public PreTestValue()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RCard", typeof(string), 40, true)]
		public string  RCard;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MOCode", typeof(string), 40, false)]
		public string  MOCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("Value", typeof(decimal), 15, true)]
		public decimal  Value;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MaxValue", typeof(decimal), 15, true)]
		public decimal  MaxValue;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MinValue", typeof(decimal), 15, true)]
		public decimal  MinValue;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ID", typeof(string), 40, true)]
		public string  ID;


		//****************************
		//以下字段数据库中暂时没有记录 Simone
		//****************************

		/// <summary>
		/// 产品代码
		/// </summary>
		[FieldMapAttribute("ItemCode", typeof(string), 40, false)]
		public string  ItemCode;

		/// <summary>
		/// 产线代码
		/// </summary>
		[FieldMapAttribute("SSCode", typeof(string), 40, false)]
		public string  SSCode;

		/// <summary>
		/// 资源代码
		/// </summary>
		[FieldMapAttribute("ResCode", typeof(string), 40, false)]
		public string  ResCode;

		/// <summary>
		/// 采集人员
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
		public string  MaintainUser;

		/// <summary>
		/// 采集日期
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 采集时间
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 测试结果
		/// </summary>
		[FieldMapAttribute("TestResult", typeof(string), 10, true)]
		public string  TestResult;

	}
	#endregion

}

