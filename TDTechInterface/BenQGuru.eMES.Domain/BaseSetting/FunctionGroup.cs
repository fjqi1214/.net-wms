using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.BaseSetting
{
    #region FunctionGroup
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLFUNCTIONGROUP", "FUNCTIONGROUPCODE")]
    public class FunctionGroup : DomainObject
    {
        public FunctionGroup()
        {
        }

        /// <summary>
        /// 功能组代码
        /// </summary>
        [FieldMapAttribute("FUNCTIONGROUPCODE", typeof(string), 40, true)]
        public string FunctionGroupCode;

        /// <summary>
        /// 功能组描述
        /// </summary>
        [FieldMapAttribute("FUNCTIONGROUPDESC", typeof(string), 40, false)]
        public string FunctionGroupDescription;

        /// <summary>
        /// 最后维护用户
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;
    }
    #endregion
}
