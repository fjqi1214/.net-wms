using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.BaseSetting
{
    #region FunctionGroup2Function
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLFUNCTIONGROUP2FUNCTION", "MDLCODE,FUNCTIONGROUPCODE")]
    public class FunctionGroup2Function : DomainObject
    {
        public FunctionGroup2Function()
        {
        }

        /// <summary>
        /// 模块代码
        /// </summary>
        [FieldMapAttribute("MDLCODE", typeof(string), 40, true)]
        public string ModuleCode;

        /// <summary>
        /// 用户组代码
        /// </summary>
        [FieldMapAttribute("FUNCTIONGROUPCODE", typeof(string), 40, true)]
        public string FunctionGroupCode;

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
        [FieldMapAttribute("VIEWVALUE", typeof(string), 40, false)]
        public string ViewValue;

    }
    #endregion
}
