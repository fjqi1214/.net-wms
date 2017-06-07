using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for User2Org
/// ** 作 者:		Created by Scott Gy
/// ** 日 期:		2008-06-25 10:36:31
/// ** 修 改:
/// ** 日 期:
/// </summary>

namespace BenQGuru.eMES.Domain.BaseSetting
{
    #region User2Org
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLUSER2ORG", "USERCODE,ORGID")]
    public class User2Org : DomainObject
    {
        public User2Org()
        {
        }

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 用户代码
        /// </summary>
        [FieldMapAttribute("USERCODE", typeof(string), 40, false)]
        public string UserCode;

        /// <summary>
        /// 组织编号
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;

        /// <summary>
        /// 是否为默认组织
        /// </summary>
        [FieldMapAttribute("DEFAULTORG", typeof(int), 1, false)]
        public int IsDefaultOrg;

    }
    #endregion
}
