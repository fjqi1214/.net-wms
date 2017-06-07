using System;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.BaseSetting
{
    #region PrintTemplate
    /// <summary>
    /// 标签模板代码
    /// </summary>
    [Serializable, TableMap("TBLPRINTTEMPLATE", "TEMPLATENAME")]
    public class PrintTemplate : DomainObject
    {
        public PrintTemplate()
        {
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        [FieldMapAttribute("TEMPLATENAME", typeof(string), 40, false)]
        public string TemplateName;

        /// <summary>
        /// 模板描述
        /// </summary>
        [FieldMapAttribute("TEMPLATEDESC", typeof(string), 100, false)]
        public string TemplateDesc;


        /// <summary>
        /// 模板路径
        /// </summary>
        [FieldMapAttribute("TEMPLATEPATH", typeof(string), 300, false)]
        public string TemplatePath;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, false)]
        public string EAttribute1;

    }
    #endregion
}
