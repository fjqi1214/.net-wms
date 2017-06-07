using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for Package
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2006-5-27 11:08:17
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.Package
{

    #region CARTONINFO
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLCARTONINFO", "PKCARTONID")]
    public class CARTONINFO : DomainObject
    {
        public CARTONINFO()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PKCARTONID", typeof(string), 40, true)]
        public string PKCARTONID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
        public string CARTONNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CAPACITY", typeof(decimal), 10, true)]
        public decimal CAPACITY;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("COLLECTED", typeof(decimal), 10, true)]
        public decimal COLLECTED;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MUSER;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(decimal), 10, true)]
        public decimal MDATE;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(decimal), 10, true)]
        public decimal MTIME;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 100, false)]
        public string EATTRIBUTE1;

    }
    #endregion

    public class CartonCollection : CARTONINFO
    {
        /// <summary>
        /// 工单号
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 料品描述[ItemDesc]
        /// </summary>
        [FieldMapAttribute("ITEMDESC", typeof(string), 100, false)]
        public string ItemDescription;

        /// <summary>
        /// 料品名称[ItemName]
        /// </summary>
        [FieldMapAttribute("ITEMNAME", typeof(string), 100, false)]
        public string ItemName;
    }

    #region PKRuleStep
    /// <summary>
    /// 包装规则步骤
    /// </summary>
    [Serializable, TableMap("tblPKRuleStep", "PKRuleCode,Step")]
    public class PKRuleStep : DomainObject
    {
        public PKRuleStep()
        {
        }

        /// <summary>
        /// 包装规则代码
        /// </summary>
        [FieldMapAttribute("PKRuleCode", typeof(string), 40, true)]
        public string PKRuleCode;

        /// <summary>
        /// 包装步骤序号
        /// </summary>
        [FieldMapAttribute("Step", typeof(int), 10, true)]
        public int Step;

        /// <summary>
        /// 步骤代码
        /// </summary>
        [FieldMapAttribute("StepCode", typeof(string), 40, true)]
        public string StepCode;

        /// <summary>
        /// 步骤名称
        /// </summary>
        [FieldMapAttribute("StepName", typeof(string), 100, false)]
        public string StepName;

        /// <summary>
        ///  是否保存本步骤的条码
        /// </summary>
        [FieldMapAttribute("IsSaveRCard", typeof(string), 1, false)]
        public string IsSaveRCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUser", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDate", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTime", typeof(int), 6, true)]
        public int MaintainTime;

    }
    #endregion

    #region TBLPACKINGCHK

    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLPACKINGCHK", "RCARD")]
    public class PACKINGCHK : DomainObject
    {
        public PACKINGCHK()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string Rcard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CHKPRODUCTCODE", typeof(string), 40, true)]
        public string CheckProductCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CHKACCESSORY", typeof(string), 40, true)]
        public string CheckAccessory;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MUSER;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(decimal), 8, true)]
        public decimal MDATE;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(decimal), 6, true)]
        public decimal MTIME;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EATTRIBUTE1;

    }
    #endregion


    #region SKDCartonDetail
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLSKDCARTONDETAIL", "MOCODE,MCARD")]
    public class SKDCartonDetail : DomainObject
    {
        public SKDCartonDetail()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string moCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
        public string CartonNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40,true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SBITEMCODE", typeof(string), 40, true)]
        public string SBItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MCARD", typeof(string), 40, true)]
        public string MCard;

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

    }
    #endregion

    #region SKDCartonDetailWithCapity

    public class SKDCartonDetailWithCapity : SKDCartonDetail
    {
        [FieldMap("MNAME", typeof(String), 40, true)]
        public string MaterialName;

        [FieldMapAttribute("CARTONQTY", typeof(decimal), 10, true)]
        public decimal cartonQty;

        [FieldMapAttribute("MOQTY", typeof(decimal), 10, true)]
        public decimal moQty;

        [FieldMapAttribute("PLANQTY", typeof(decimal), 10, true)]
        public decimal planQty;
    }

    #endregion
}

