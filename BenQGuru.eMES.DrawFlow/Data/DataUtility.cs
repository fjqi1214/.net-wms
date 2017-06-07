using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenQGuru.eMES.DrawFlow.Data
{
    public sealed class DataUtility
    {
        /// <summary>
        /// </summary>
        public static bool IsEnterChar(char c)
        {
            if (c == '\r')
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 从Value[Key]结构的字符串中返回Key
        /// </summary>
        /// <returns></returns>
        /// </summary>
        public static string GetKeyFromString(string valueKeyString)
        {
            if (valueKeyString == null || valueKeyString.IndexOf("[") == -1 || valueKeyString.IndexOf("]") == -1)
            {
                return string.Empty;
            }
            valueKeyString = valueKeyString.Trim();
            valueKeyString = valueKeyString.Remove(0, valueKeyString.IndexOf("[") + 1);
            valueKeyString = valueKeyString.Substring(0, valueKeyString.Length - 1);
            return valueKeyString;
        }

        /// <summary>
        /// 从Value[Key]结构的字符串中返回Value
        /// </summary>
        /// <returns></returns>
        /// </summary>
        public static string GetValueFromString(string valueKeyString)
        {
            if (valueKeyString == null || valueKeyString.IndexOf("[") == -1 || valueKeyString.IndexOf("]") == -1)
            {
                return string.Empty;
            }
            valueKeyString = valueKeyString.Trim();
            valueKeyString = valueKeyString.Substring(0, valueKeyString.IndexOf("["));
            return valueKeyString;
        }

        public static string GenerateRandomString(int length)
        {
            char[] charSource = new char[] { '0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f','g','h','i','j',
                                            'k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','B','C',
                                            'D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','X','Y','Z'};
            StringBuilder rs = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                long tick = DateTime.Now.Ticks;
                Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                rs.Append(charSource[ran.Next(0, 62)]);
            }
            return rs.ToString();
        }
    }
}
