using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using System.Web.Services;
using System.Text;

namespace BenQGuru.eMES.Web.WebService
{
    public class BaseService : System.Web.Services.WebService
    {
        protected string DbName
        { get; set; }

        private IDomainDataProvider _domainDataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(DbName);
                }
                return _domainDataProvider;
            }
        }

        public BaseService()
        {
            InitDbItems();
        }


        private void InitDbItems()
        {
            foreach (var item in BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.Settings)
            {
                if (item.Default)
                    DbName = item.Name;

            }
        }

        [WebMethod(Description = "获取用户组织id", EnableSession = true)]
        protected void GetOrgListByUserCode(string userCode)
        {
            try
            {
                string C_ORGNAME_IN_SESSION = "CurrentOrganization";
                if (HttpContext.Current.Session[C_ORGNAME_IN_SESSION] == null)
                {
                    // 只将User默认的Org放进Session中去
                    BaseModelFacade baseFacade = new BaseModelFacade(this.DataProvider);
                    object org = baseFacade.GetUserDefaultOrgByUser(userCode);
                    if (org == null)
                    {
                        throw new Exception("$Error_UserDefaultOrg_NotDefined");
                    }
                    HttpContext.Current.Session[C_ORGNAME_IN_SESSION] = new List<Organization>() { (Organization)org };
                }
            }
            catch
            {
                //抛出 由上层处理
                throw;
            }
        }
        /// <summary>
        /// json序列化
        /// ——精简版，仅限一维数组
        /// ——自带敏感字符过滤
        /// ——和BenQGuru.eMES.WinCeClient项目下的CeHelper.cs中的JsonToStrs方法配合使用
        /// </summary>
        /// <param name="strs">输入string[]</param>
        /// <returns>返回string型json</returns>
        public string StrsToJson(string[] strs)
        {
            StringBuilder jsonstring = new StringBuilder();
            if (strs != null && strs.Length > 0)
            {
                jsonstring.Append("[");
                for (int i = 0; i < strs.Length; i++)
                {
                    if (strs[i] == null)
                    {
                        strs[i] = string.Empty;
                    }
                    jsonstring.Append("\"" + strs[i].Replace("\"", "\\\"").Replace("]", "\\]") + "\"");//敏感字符过滤
                    if (i != strs.Length - 1)
                    {
                        jsonstring.Append(",");
                    }
                }
                jsonstring.Append("]");
                if (jsonstring.ToString() != "[]")
                {
                    return jsonstring.ToString();
                }
            }
            return null;
        }
    }
}
