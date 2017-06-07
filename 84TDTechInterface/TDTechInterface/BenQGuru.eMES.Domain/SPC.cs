using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for SPC
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2006-10-20 15:52:16
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.SPC
{

	#region SPCConfig
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSPCCONFIG", "ParamName")]
	public class SPCConfig : DomainObject
	{
		public SPCConfig()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ParamName", typeof(string), 40, true)]
		public string  ParamName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ParamValue", typeof(string), 100, false)]
		public string  ParamValue;

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

	#region SPCDataStore
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSPCDATASTORE", "ID")]
	public class SPCDataStore : DomainObject
	{
		public SPCDataStore()
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
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
		public string  eAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ID", typeof(string), 40, true)]
		public string  ID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ItemCode", typeof(string), 40, false)]
		public string  ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DateFrom", typeof(int), 8, true)]
		public int  DateFrom;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DateTo", typeof(int), 8, true)]
		public int  DateTo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TableName", typeof(string), 40, true)]
		public string  TableName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ObjectCode", typeof(string), 40, false)]
		public string  ObjectCode;

	}
	#endregion

	#region SPCItemSpec
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSPCITEMSPEC", "ItemCode,GroupSeq,ObjectCode")]
	public class SPCItemSpec : DomainObject
	{
		public SPCItemSpec()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ConditionName", typeof(string), 40, true)]
		public string  ConditionName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("UCL", typeof(decimal), 15, true)]
		public decimal  UCL;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LimitUpOnly", typeof(string), 1, true)]
		public string  LimitUpOnly;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LimitLowOnly", typeof(string), 1, true)]
		public string  LimitLowOnly;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("AutoCL", typeof(string), 1, true)]
		public string  AutoCL;

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

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ItemCode", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("GroupSeq", typeof(decimal), 10, true)]
		public decimal  GroupSeq;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LCL", typeof(decimal), 15, true)]
		public decimal  LCL;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("USL", typeof(decimal), 15, true)]
		public decimal  USL;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LSL", typeof(decimal), 15, true)]
		public decimal  LSL;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("Memo", typeof(string), 100, false)]
		public string  Memo;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ObjectCode", typeof(string), 40, true)]
		public string  ObjectCode;


	}
	#endregion

	#region SPCObject
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSPCOBJECT", "ObjectCode")]
	public class SPCObject : DomainObject
	{
		public SPCObject()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ObjectCode", typeof(string), 40, true)]
		public string  ObjectCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ObjectName", typeof(string), 100, false)]
		public string  ObjectName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("GraphType", typeof(string), 40, true)]
		public string  GraphType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DateRange", typeof(string), 40, true)]
		public string  DateRange;

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

	#region SPCObjectStore
	/// <summary>
	/// 
	/// </summary>
    [Serializable, TableMap("TBLSPCOBJECTSTORE", "SERIAL")]
    public class SPCObjectStore : DomainObject
    {
        public SPCObjectStore()
        {
        }

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
        [FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
        public string eAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("GROUPSEQ", typeof(decimal), 10, true)]
        public decimal GroupSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ObjectCode", typeof(string), 40, true)]
        public string ObjectCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKGroup", typeof(string), 400, true)]
        public string CKGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKItemCode", typeof(string), 400, false)]
        public string CKItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

    }
	#endregion

	#region SPCDeterRule
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLSPCDETERRULE", "RULECODE")]
	public class SPCDeterRule : DomainObject
	{
		public SPCDeterRule()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RULECODE", typeof(string), 40, true)]
		public string  RuleCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RULENAME", typeof(string), 100, false)]
		public string  RuleName;

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

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RULETYPE", typeof(string), 40, false)]
		public string  RuleType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARAMETER1", typeof(string), 100, false)]
		public string  Parameter1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARAMETER2", typeof(string), 100, false)]
		public string  Parameter2;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ENABLED", typeof(string), 1, true)]
		public string  Enabled;

	}
	#endregion

}

