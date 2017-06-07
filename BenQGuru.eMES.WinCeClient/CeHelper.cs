using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BenQGuru.eMES.WinCeClient
{
    public class CeHelper
    {
        /// <summary>
        /// json反序列化
        /// ——和BenQGuru.eMES.Web.WebService项目下的BaseService.cs中的StrsToJson方法配合使用 
        /// </summary>
        /// <param name="json">输入string型json</param>
        /// <returns>返回一维数组</returns>
        public string[] JsonToStrs(string json)
        {
            if (json != null&&json!="[]")
            {
                json = json.Replace("[\"", "").Replace("\"]", "");
                List<string> strs = new List<string>();
                string[] test = new string[2];
                for (int i = 0; i > -1; i++)
                {
                    int len = json.IndexOf("\",\"");
                    if (len >= 0)
                    {
                        test = SubStr(json, "\",\"", len);
                        strs.Add(test[0].Replace("\\\"", "\"").Replace("\\]", "]"));
                        json = test[1];
                    }
                    else
                    {
                        strs.Add(json.Replace("\\\"", "\"").Replace("\\]", "]"));
                        i = -2;
                    }
                }
                if (strs.Count > 0)
                {
                    return strs.ToArray();
                }
            }
            return null;
        }
        protected string[] SubStr(string str, string strsplit, int end)
        {
            string[] strs = new string[2];
            if (end >= 0)
            {
                strs[0] = str.Substring(0, end);
                strs[1] = str.Substring(end + strsplit.Length);
            }
            return strs;
        }
    }
}
