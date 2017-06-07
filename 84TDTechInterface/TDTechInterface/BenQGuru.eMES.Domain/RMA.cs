using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for RMA
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jessie Lee
/// ** 日 期:		2006-7-7 9:44:49
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.RMA
{

	#region CusItemCodeCheckList
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLCITEMCODECL", "ITEMCODE,MODELCODE,CUSCODE")]
	public class CusItemCodeCheckList : DomainObject
	{
		public CusItemCodeCheckList()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CUSCODE", typeof(string), 40, true)]
		public string  CustomerCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CUSITEMCODE", typeof(string), 40, true)]
		public string  CustomerItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CUSMODELCODE", typeof(string), 40, true)]
		public string  CustomerModelCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("eAttribute1", typeof(string), 40, false)]
		public string  eAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHARCODE", typeof(string), 40, true)]
		public string  Character;

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

	}
	#endregion

	#region ErrorSymptom
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLES", "SymptomCode")]
	public class ErrorSymptom : DomainObject
	{
		public ErrorSymptom()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SymptomCode", typeof(string), 40, true)]
		public string  SymptomCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ESDesc", typeof(string), 100, false)]
		public string  Description;

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
		[FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
		public string  EAttribute1;

	}
	#endregion

	#region Model2ErrorSymptom
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLMODEL2ERRSYM", "ModelCode,SymptomCode")]
	public class Model2ErrorSymptom : DomainObject
	{
		public Model2ErrorSymptom()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ModelCode", typeof(string), 40, true)]
		public string  ModelCode;

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
		[FieldMapAttribute("SymptomCode", typeof(string), 40, true)]
		public string  SymptomCode;

	}
	#endregion

	#region RCard2RMABill
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("TBLRCARD2RMABill", "RMABILLCODE,ITEMCODE,CUSCODE,MODELCODE,RCARD")]
	public class RCard2RMABill : DomainObject
	{
		public RCard2RMABill()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RMABILLCODE", typeof(string), 40, true)]
		public string  RMABillCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
		public string  ItemCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CUSCODE", typeof(string), 40, true)]
		public string  CustomerCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
		public string  ModelCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RCARD", typeof(string), 40, true)]
		public string  RunningCard;

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

		public int RMATimes;

	}
	#endregion

    #region RMABill
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLRMABILL", "RMABILLCODE")]
    public class RMABill : DomainObject
    {
        public RMABill()
        {
        }

        /// <summary>
        /// RMA单号
        /// </summary>
        [FieldMapAttribute("RMABILLCODE", typeof(string), 40, true)]
        public string RMABillCode;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MEMO", typeof(string), 200, false)]
        public string Memo;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Status", typeof(string), 40, true)]
        public string Status;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
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
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, false)]
        public string eAttribute1;
 

    }
    #endregion

    #region RMADETIAL
    /// <summary>
    /// TBLRMADETIAL
    /// </summary>
    [Serializable, TableMap("TBLRMADETIAL", "RCARD,RMABILLCODE")]
    public class RMADetial : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public RMADetial()
        {
        }

        ///<summary>
        ///RMABILLCODE
        ///</summary>
        [FieldMapAttribute("RMABILLCODE", typeof(string), 40, false)]
        public string Rmabillcode;

        ///<summary>
        ///SERVERCODE
        ///</summary>
        [FieldMapAttribute("SERVERCODE", typeof(string), 40, false)]
        public string Servercode;

        ///<summary>
        ///MODELCODE
        ///</summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string Modelcode;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string Itemcode;

        ///<summary>
        ///RCARD
        ///</summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string Rcard;

        ///<summary>
        ///HANDELCODE
        ///</summary>
        [FieldMapAttribute("HANDELCODE", typeof(string), 40, false)]
        public string Handelcode;

        ///<summary>
        ///COMPISSUE
        ///</summary>
        [FieldMapAttribute("COMPISSUE", typeof(string), 200, false)]
        public string Compissue;

        ///<summary>
        ///CUSTOMCODE
        ///</summary>
        [FieldMapAttribute("CUSTOMCODE", typeof(string), 40, false)]
        public string Customcode;

        ///<summary>
        ///COMFROM
        ///</summary>
        [FieldMapAttribute("COMFROM", typeof(string), 100, false)]
        public string Comfrom;

        ///<summary>
        ///MAINTENANCE
        ///</summary>
        [FieldMapAttribute("MAINTENANCE", typeof(int), 22, false)]
        public int Maintenance;

        ///<summary>
        ///WHRECEIVEDATE
        ///</summary>
        [FieldMapAttribute("WHRECEIVEDATE", typeof(int), 22, true)]
        public int Whreceivedate;

        ///<summary>
        ///SUBCOMPANY
        ///</summary>
        [FieldMapAttribute("SUBCOMPANY", typeof(string), 40, true)]
        public string Subcompany;

        ///<summary>
        ///REMOCODE
        ///</summary>
        [FieldMapAttribute("REMOCODE", typeof(string), 40, true)]
        public string Remocode;

        ///<summary>
        ///ERRORCODE
        ///</summary>
        [FieldMapAttribute("ERRORCODE", typeof(string), 40, true)]
        public string Errorcode;

        ///<summary>
        ///ISINSHELFLIFE
        ///</summary>
        [FieldMapAttribute("ISINSHELFLIFE", typeof(string), 2, true)]
        public string Isinshelflife;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 200, true)]
        public string Memo;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

    }
    #endregion

	#region RMACodeRule
	/// <summary>
	/// 
	/// </summary>
	[Serializable, TableMap("tblrmacoderule", "BUCODE")]
	public class RMACodeRule : DomainObject
	{
		public RMACodeRule()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("BUCODE", typeof(string), 40, true)]
		public string  BUCode;

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
		[FieldMapAttribute("RMABillFL", typeof(string), 40, true)]
		public string  RMABillFirstLetter;

	}
	#endregion

}

