using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for ATE
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jessie Lee
/// ** 日 期:		2006-5-22 10:24:05
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.ATE
{

	#region ATETestInfo
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLATETESTINFO", "PKID")]
	public class ATETestInfo : DomainObject
	{
		public ATETestInfo()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PKID", typeof(string), 40, true)]
		public string  PKID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string  RunningCard;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TESTRESULT", typeof(string), 40, true)]
		public string  TestResult;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FAILCODE", typeof(string), 100, false)]
		public string  FailCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, true)]
		public string  ResCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LINECODE", typeof(string), 40, true)]
		public string  LineCode;

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

	}
	#endregion

}

