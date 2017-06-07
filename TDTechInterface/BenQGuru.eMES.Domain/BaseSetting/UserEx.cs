using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.BaseSetting
{
    #region User Ex
    /// <summary>
    /// User 扩充
    /// </summary>
    public class UserEx : User
    {
        public UserEx()
        {
        }

        /// <summary>
        /// User2Org列表
        /// </summary>
        public User2Org[] user2OrgList;

        /// <summary>
        /// 默认组织Desc
        /// </summary>
        [FieldMapAttribute("ORGDESC", typeof(string), 40, false)]
        public string DefaultOrgDesc;
    }
    #endregion
}
