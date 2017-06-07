using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for ShiftModel
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2005-04-29 10:36:29
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.BaseSetting
{

	#region Shift
	/// <summary>
	/// 班次
	/// </summary>
	[Serializable, TableMap("TBLSHIFT", "SHIFTCODE")]
	public class Shift : DomainObject
	{
		public Shift()
		{
		}
 
		/// <summary>
		/// 班次描述
		/// </summary>
		[FieldMapAttribute("SHIFTDESC", typeof(string), 100, false)]
		public string  ShiftDescription;

		/// <summary>
		/// 班次的起始时间
		/// </summary>
		[FieldMapAttribute("SHIFTBTIME", typeof(int), 6, true)]
		public int  ShiftBeginTime;

		/// <summary>
		/// 班次的结束时间
		/// </summary>
		[FieldMapAttribute("SHIFTETIME", typeof(int), 6, true)]
		public int  ShiftEndTime;

		/// <summary>
		/// 是否是跨日期
		/// 1:跨
		/// 0:不跨
		/// </summary>
		[FieldMapAttribute("ISOVERDAY", typeof(string), 1, true)]
		public string  IsOverDate;

		/// <summary>
		/// 最后系统用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 班次顺序
		/// </summary>
		[FieldMapAttribute("SHIFTSEQ", typeof(decimal), 10, true)]
		public decimal  ShiftSequence;

		/// <summary>
		/// 班次代码
		/// </summary>
		[FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
		public string  ShiftCode;

		/// <summary>
		/// 班制代码
		/// </summary>
		[FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSHIFTTYPE", "SHIFTTYPECODE", "SHIFTTYPEDESC")]
		public string  ShiftTypeCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

	#region ShiftType
	/// <summary>
	/// 班制
	/// </summary>
	[Serializable, TableMap("TBLSHIFTTYPE", "SHIFTTYPECODE")]
	public class ShiftType : DomainObject
	{
		public ShiftType()
		{
		}
 
		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 最后维护用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 班制描述
		/// </summary>
		[FieldMapAttribute("SHIFTTYPEDESC", typeof(string), 100, false)]
		public string  ShiftTypeDescription;

		/// <summary>
		/// 生效日期
		/// </summary>
		[FieldMapAttribute("EFFDATE", typeof(decimal), 10, true)]
		public decimal  EffectiveDate;

		/// <summary>
		/// 失效日期
		/// </summary>
		[FieldMapAttribute("IVLDATE", typeof(int), 8, true)]
		public int  InvalidDate;

		/// <summary>
		/// 班制代码
		/// </summary>
		[FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
		public string  ShiftTypeCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

	#region TimePeriod
	/// <summary>
	/// 时间段
	/// </summary>
	[Serializable, TableMap("TBLTP", "TPCODE")]
	public class TimePeriod : DomainObject
	{
		public TimePeriod()
		{
		}
 
		/// <summary>
		/// 时间段的开始时间
		/// </summary>
		[FieldMapAttribute("TPBTIME", typeof(int), 6, true)]
		public int  TimePeriodBeginTime;

		/// <summary>
		/// 时间段的结束时间
		/// </summary>
		[FieldMapAttribute("TPETIME", typeof(int), 6, true)]
		public int  TimePeriodEndTime;

		/// <summary>
		/// 最后系统用户[LastMaintainUser]
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 最后维护日期[LastMaintainDate]
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 最后维护时间[LastMaintainTime]
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 是否是跨日期
		/// 1:跨
		/// 0:不跨
		/// </summary>
		[FieldMapAttribute("ISOVERDATE", typeof(string), 1, true)]
		public string  IsOverDate;

		/// <summary>
		/// 时间段序列
		/// </summary>
		[FieldMapAttribute("TPSEQ", typeof(int), 6, true)]
		public int  TimePeriodSequence;

		/// <summary>
		/// 时间段代码
		/// </summary>
		[FieldMapAttribute("TPCODE", typeof(string), 40, true)]
		public string  TimePeriodCode;

		/// <summary>
		/// 时间段描述
		/// </summary>
		[FieldMapAttribute("TPDESC", typeof(string), 100, false)]
		public string  TimePeriodDescription;

		/// <summary>
		/// Normal /Exception
		/// </summary>
		[FieldMapAttribute("TPTYPE", typeof(string), 40, true)]
		public string  TimePeriodType;

		/// <summary>
		/// 班次代码
		/// </summary>
		[FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSHIFT", "SHIFTCODE", "SHIFTDESC")]
		public string  ShiftCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
		public string  ShiftTypeCode;

	}
	#endregion

    #region ShiftCrew
    /// <summary>
    /// 班组
    /// </summary>
    [Serializable, TableMap("TBLCREW", "CREWCODE")]
    public class ShiftCrew : DomainObject
    {
        public ShiftCrew()
        {
        }

        /// <summary>
        /// 班组代码
        /// </summary>
        [FieldMapAttribute("CREWCODE", typeof(string), 40, false)]
        public string CrewCode;

        /// <summary>
        /// 班组描述
        /// </summary>
        [FieldMapAttribute("CREWDESC", typeof(string), 100, true)]
        public string CrewDesc;

        /// <summary>
        /// 最后系统用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

    }
    #endregion

}

