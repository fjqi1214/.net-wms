using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for TSModel
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2005-04-29 10:36:47
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.TSModel
{

	#region Duty
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLDUTY", "DUTYCODE")]
	public class Duty : DomainObject
	{
		public Duty()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DUTYCODE", typeof(string), 40, true)]
		public string  DutyCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DUTYDESC", typeof(string), 100, false)]
		public string  DutyDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), -1, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), -1, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 10, true)]
		public string  EAttribute1;

	}
	#endregion

	#region ErrorCause
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLECS", "ECSCODE")]
	public class ErrorCause : DomainObject
	{
		public ErrorCause()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECSCODE", typeof(string), 40, true)]
		public string  ErrorCauseCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECSDESC", typeof(string), 100, false)]
		public string  ErrorCauseDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
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
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

	#region ErrorCauseGroup
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLECSG", "ECSGCODE")]
	public class ErrorCauseGroup : DomainObject
	{
		public ErrorCauseGroup()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECSGCODE", typeof(string), 40, true)]
		public string  ErrorCauseGroupCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECSGDESC", typeof(string), 100, false)]
		public string  ErrorCauseGroupDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
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
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

	#region ErrorCauseGroup2ErrorCause
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLECSG2ECS", "ECSGCODE,ECSCODE")]
	public class ErrorCauseGroup2ErrorCause : DomainObject
	{
		public ErrorCauseGroup2ErrorCause()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 8, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECSGCODE", typeof(string), 40, true)]
		public string  ErrorCauseGroupCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECSCODE", typeof(string), 40, true)]
		public string  ErrorCause;

	}
	#endregion

	#region ErrorCodeA
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLEC", "ECODE")]
	public class ErrorCodeA : DomainObject
	{
		public ErrorCodeA()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECODE", typeof(string), 40, true)]
		public string  ErrorCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECDESC", typeof(string), 100, false)]
		public string  ErrorDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
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
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

        public override string ToString()
        {
            return this.ErrorCode + " " + this.ErrorDescription;
        }

	}
	#endregion

    #region ErrorCodeItem2Route
    [Serializable, TableMap("TBLECITEM2ROUTE", "ECCODE,ITEMCODE,ORGID")]
    public class ErrorCodeItem2Route : DomainObject
    {
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECCODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, false)]
        public string ErrorCodeGroupCode;
    } 
    #endregion

	#region ErrorCodeGroup
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLECG", "ECGCODE")]
	public class ErrorCodeGroupA : DomainObject
	{
		public ErrorCodeGroupA()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
		public string  ErrorCodeGroup;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECGDESC", typeof(string), 100, false)]
		public string  ErrorCodeGroupDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 8, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ROUTECODE", typeof(string), 40, false)]
		public string  RouteCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, false)]
		public string  OPCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, false)]
		public string  ResourceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

	#region ErrorCodeGroup2ErrorCode
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLECG2EC", "ECGCODE,ECODE")]
	public class ErrorCodeGroup2ErrorCode : DomainObject
	{
		public ErrorCodeGroup2ErrorCode()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 8, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
		public string  ErrorCodeGroup;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECODE", typeof(string), 40, true)]
		public string  ErrorCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

    [Serializable, TableMap("TBLECG2EC", "ECGCODE,ECODE")]
    public class ErrorGrou2ErrorCode4OQC : ErrorCodeGroup2ErrorCode
    {
        [FieldMapAttribute("ECDESC", typeof(string), 100, true)]
        public string ErrorCodeDescription;
    }

	#region Model2ErrorCause
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLMODEL2ECS", "MODELCODE,ECSCODE")]
	public class Model2ErrorCause : DomainObject
	{
		public Model2ErrorCause()
		{
		}
 
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
		[FieldMapAttribute("MTIME", typeof(int), 8, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECSCODE", typeof(string), 40, true)]
		public string  ErrorCauseCode;

	}
	#endregion

	#region Model2ErrorCauseGroup
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLMODEL2ECSG", "MODELCODE,ECSGCODE")]
	public class Model2ErrorCauseGroup : DomainObject
	{
		public Model2ErrorCauseGroup()
		{
		}
 
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
		[FieldMapAttribute("MTIME", typeof(int), 8, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECSGCODE", typeof(string), 40, true)]
		public string  ErrorCauseGroupCode;

	}
	#endregion

	#region ItemRouteOp2ErrorCauseGroup
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLITEMROUTEOP2ECSG", "OpID,ECSGCode")]
	public class ItemRouteOp2ErrorCauseGroup : DomainObject
	{
		public ItemRouteOp2ErrorCauseGroup()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OpID", typeof(string), 40, true)]
		public string  OpID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECSGCode", typeof(string), 40, true)]
		public string  ErrorCauseGroupCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUser", typeof(string), 40, true)]
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
		[FieldMapAttribute("EAttribute1", typeof(string), 40, true)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ItemCode", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RouteCode", typeof(string), 40, true)]
		public string  RouteCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OpCode", typeof(string), 40, true)]
		public string  OpCode;

	}
	#endregion
    
	#region Model2ErrorCodeGroup
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLMODEL2ECG", "MODELCODE,ECGCODE")]
	public class Model2ErrorCodeGroup : DomainObject
	{
		public Model2ErrorCodeGroup()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ROUTECODE", typeof(string), 40, false)]
		public string  RouteCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("OPCODE", typeof(string), 40, false)]
		public string  OPCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RESCODE", typeof(string), 40, false)]
		public string  ResourceCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

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
		[FieldMapAttribute("MTIME", typeof(int), 8, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ECGCODE", typeof(string), 40, true)]
		public string  ErrorCodeGroup;

	}
	#endregion

	#region Model2Solution
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLMODEL2SOLUTION", "MODELCODE,SOLCODE")]
	public class Model2Solution : DomainObject
	{
		public Model2Solution()
		{
		}
 
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
		[FieldMapAttribute("MTIME", typeof(int), 8, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SOLCODE", typeof(string), 40, true)]
		public string  SolutionCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 10, true)]
		public string  EAttribute1;

	}
	#endregion

	#region Solution
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSOLUTION", "SOLCODE")]
	public class Solution : DomainObject
	{
		public Solution()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SOLCODE", typeof(string), 40, true)]
		public string  SolutionCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SOLDESC", typeof(string), 100, false)]
		public string  SolutionDescription;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SOLIMP", typeof(string), 100, false)]
		public string  SolutionImprove;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
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
		[FieldMapAttribute("EAttribute1", typeof(string), 10, true)]
		public string  EAttribute1;

	}
	#endregion

    #region TSSmartConfig
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLTSSMARTCFG", "SEQ")]
    public class TSSmartConfig : DomainObject
    {
        public TSSmartConfig()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, false)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECGCODE", typeof(string), 40, false)]
        public string ErrorGroupCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ENABLED", typeof(string), 1, true)]
        public string Enabled;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SORTBY", typeof(string), 40, false)]
        public string SortBy;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DATERANGE", typeof(decimal), 15, true)]
        public decimal DateRange;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("DATERANGETYPE", typeof(string), 40, false)]
        public string DateRangeType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SHOWITEMCOUNT", typeof(decimal), 10, true)]
        public decimal ShowItemCount;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
        public string EAttribute1;

    }
    #endregion

    #region ErrorCode2OPRework
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLEC2OPREWORK", "OPCODE,ECODE,ORGID")]
    public class ErrorCode2OPRework : DomainObject
    {
        public ErrorCode2OPRework()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, false)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TOOPCODE", typeof(string), 40, true)]
        public string ToOPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, true)]
        public int OrganizationID;

    }
    #endregion

    #region ErrorCode2OPReworkNew
    [Serializable]
    public class ErrorCode2OPReworkNew : DomainObject
    {
        [FieldMapAttribute("OPCode", typeof(string), 40, true)]
        public string OPCode;

        [FieldMapAttribute("ErrorCode", typeof(string), 40, true)]
        public string ErrorCode;

        [FieldMapAttribute("ErrorCodeDesc", typeof(string), 40, true)]
        public string ErrorCodeDesc;

        [FieldMapAttribute("RouteCode", typeof(string), 40, true)]
        public string RouteCode;

        [FieldMapAttribute("ToOPCode", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLOP", "OPCODE", "OPDESC")]
        public string ToOPCode;

    } 
    #endregion

}

