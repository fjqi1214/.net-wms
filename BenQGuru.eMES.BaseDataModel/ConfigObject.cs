using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BenQGuru.eMES.BaseDataModel
{
    
    public class FieldDataFrom
    {
        public static string ParentItem = "ParentItem";     // 来自父Object中某个栏位
        public static string ParentNode = "ParentNode";     // 来自父配置结点中某Node
        public static string Query = "Query";               // 来自SQL语句查询
        public static string UserCode = "UserCode";         // 执行时的UserCode
        public static string Date = "Date";                 // 当前日期
        public static string Time = "Time";                 // 当前时间
        public static string GUID = "GUID";                 // GUID
    }
    public class FieldCheckType
    {
        public static string Exist = "Exist";               // 检查数据是否存在
        public static string DataType = "DataType";         // 检查数据类型
        public static string Length = "Length";             // 长度，在表达式中写：{0}>=2表示长度至少是2
        public static string DataRange = "DataRange";       // 数据范围，在表达式中写：{0}>=0&&{0}<=9，表示从0到9
    }

    public class ConfigObject
    {
        public string Type = "";    // Class Type完整名称
        public string Name = "";    // 标识名称，用在NodeList中
        public string Text = "";    // 显示文本

        public string TemplateFileName = "";    // 模板文件名

        public List<ConfigField> FieldList = null;      // 字段列表
        public List<ConfigField> DefaultFieldList = null;   // 默认字段列表

        public static void LoadConfig(string xmlFile, out List<ConfigObject> outConfigObjList, out MatchType matchType)
        {
            outConfigObjList = null;
            matchType = null;
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFile);
            XmlNode nodeItemRoot = doc.SelectSingleNode("//ItemList");
            if (nodeItemRoot == null)
                return;
            XmlNodeList nodeItemList = nodeItemRoot.SelectNodes("Item");
            if (nodeItemList == null)
                return;
            outConfigObjList = new List<ConfigObject>();
            for (int i = 0; i < nodeItemList.Count; i++)
            {
                XmlNode nodeItem = nodeItemList[i];
                ConfigObject cfgObj = GetConfigObjectFromXmlNode(nodeItem);
                if (cfgObj != null)
                    outConfigObjList.Add(cfgObj);
            }
            XmlNode nodeMatchRoot = doc.SelectSingleNode("//MatchList");
            if (nodeMatchRoot != null)
            {
                matchType = new MatchType();
                XmlNodeList nodeMatchList = nodeMatchRoot.SelectNodes("Match");
                for (int i = 0; nodeMatchList != null && i < nodeMatchList.Count; i++)
                {
                    string strMatchName = GetNodeAttribute(nodeMatchList[i], "Name");
                    XmlNodeList nodeMatchDtl = nodeMatchList[i].SelectNodes("List");
                    for (int n = 0; nodeMatchDtl != null && n < nodeMatchDtl.Count; n++)
                    {
                        matchType.Add(strMatchName, GetNodeAttribute(nodeMatchDtl[n], "Value"), GetNodeAttribute(nodeMatchDtl[n], "Text"));
                    }
                }
            }
            return;
        }
        private static ConfigObject GetConfigObjectFromXmlNode(XmlNode nodeItem)
        {
            ConfigObject cfgObj = new ConfigObject();
            cfgObj.Type = GetNodeAttribute(nodeItem, "Type");
            cfgObj.Name = GetNodeAttribute(nodeItem, "Name");
            cfgObj.Text = GetNodeAttribute(nodeItem, "Text");
            cfgObj.TemplateFileName = GetNodeAttribute(nodeItem, "TemplateFileName");
            XmlNodeList nodeFieldList = nodeItem.SelectNodes("FieldList/Field");
            if (nodeFieldList == null)
                return null;
            cfgObj.FieldList = new List<ConfigField>();
            for (int n = 0; n < nodeFieldList.Count; n++)
            {
                XmlNode nodeField = nodeFieldList[n];
                cfgObj.FieldList.Add(GetConfigFieldFromXmlNode(nodeField));
            }
            nodeFieldList = nodeItem.SelectNodes("DefaultFieldList/Field");
            if (nodeFieldList != null)
            {
                cfgObj.DefaultFieldList = new List<ConfigField>();
                for (int n = 0; n < nodeFieldList.Count; n++)
                {
                    XmlNode nodeField = nodeFieldList[n];
                    cfgObj.DefaultFieldList.Add(GetConfigFieldFromXmlNode(nodeField));
                }
            }
            return cfgObj;
        }
        private static ConfigField GetConfigFieldFromXmlNode(XmlNode nodeField)
        {
            ConfigField cfgFld = new ConfigField();
            cfgFld.Name = GetNodeAttribute(nodeField, "Name");
            cfgFld.Text = GetNodeAttribute(nodeField, "Text");
            cfgFld.AllowNull = ConvertStringToBoolean(GetNodeAttribute(nodeField, "AllowNull", "true"));
            cfgFld.UniqueGroup = GetNodeAttribute(nodeField, "UniqueGroup");
            cfgFld.DefaultValue = GetNodeAttribute(nodeField, "DefaultValue");
            cfgFld.MatchType = GetNodeAttribute(nodeField, "MatchType");
            cfgFld.DataType = GetNodeAttribute(nodeField, "DataType", "string");
            cfgFld.DataFrom = GetNodeAttribute(nodeField, "DataFrom");
            cfgFld.ParentNodeField = GetNodeAttribute(nodeField, "ParentNodeField");
            cfgFld.IgnoreForeignWhenNull = ConvertStringToBoolean(GetNodeAttribute(nodeField, "IgnoreForeignWhenNull", "true"));
            cfgFld.DataQuerySql = GetNodeAttribute(nodeField, "DataQuerySql");
            cfgFld.DataQueryParam = GetNodeAttribute(nodeField, "DataQueryParam");
            cfgFld.IncludeItem = ConvertStringToBoolean(GetNodeAttribute(nodeField, "IncludeItem", "true"));
            cfgFld.ForeignItem = ConvertStringToBoolean(GetNodeAttribute(nodeField, "ForeignItem", "false"));
            if (cfgFld.ForeignItem == true)
            {
                XmlNode nodeTmp = nodeField.SelectSingleNode("ForeignItem");
                if (nodeTmp != null)
                {
                    cfgFld.ForeignObject = GetConfigObjectFromXmlNode(nodeTmp);
                }
            }
            XmlNodeList nodeChkList = nodeField.SelectNodes("CheckList/Check");
            if (nodeChkList != null)
            {
                List<ConfigCheck> chkList = new List<ConfigCheck>();
                for (int i = 0; i < nodeChkList.Count; i++)
                {
                    chkList.Add(GetConfigCheckFromXmlNode(nodeChkList[i]));
                }
                cfgFld.CheckList = chkList;
            }
            return cfgFld;
        }
        private static ConfigCheck GetConfigCheckFromXmlNode(XmlNode nodeCheck)
        {
            ConfigCheck chk = new ConfigCheck();
            chk.Type = GetNodeAttribute(nodeCheck, "Type");
            chk.ParentObjectType = GetNodeAttribute(nodeCheck, "ParentObjectType");
            chk.ParentObjectField = GetNodeAttribute(nodeCheck, "ParentObjectField");
            chk.CheckDataType = GetNodeAttribute(nodeCheck, "CheckDataType");
            chk.LengthExpression = GetNodeAttribute(nodeCheck, "LengthExpression");
            chk.DataRangeExpression = GetNodeAttribute(nodeCheck, "DataRangeExpression");
            chk.ExistCheckSql = GetNodeAttribute(nodeCheck, "ExistCheckSql");
            return chk;
        }
        private static string GetNodeAttribute(XmlNode node, string attributeName)
        {
            return GetNodeAttribute(node, attributeName, "");
        }
        private static string GetNodeAttribute(XmlNode node, string attributeName, string defaultValue)
        {
            if (node.Attributes[attributeName] == null)
                return defaultValue;
            else
                return node.Attributes[attributeName].Value;
        }
        private static bool ConvertStringToBoolean(string value)
        {
            return (value.ToUpper() == "TRUE" || value == "1");
        }
    }

    public class ConfigField
    {
        public string Name = "";        // 属性名称(Class的属性)
        public string Text = "";        // 描述
        public bool AllowNull = true;   // 是否为空：true/false
        public string UniqueGroup = ""; // 唯一性检查群组，UniqueGroup相同的栏位表示联合的唯一字段
        public string DefaultValue = "";    // 默认值，读取时直接将Attribute值赋值给栏位
        public string MatchType = "";       // 匹配类型
        public string DataType = "";        // 数据类型
        public bool IncludeItem = true;     // 是否包含在Class中：true/false
        public bool ForeignItem = false;    // 是否属于外部Class：true/false
        public bool IgnoreForeignWhenNull = true;   // 当数据为空时，不再处理外部记录
        public string DataFrom = "";        // 数据来源，参考FieldDataFrom列表
        public string ParentNodeField = ""; // 如果DataFrom=ParentObject或ParentItem，则此设置表示来自父对象的结点名或属性名
        public string DataQuerySql = "";    // 如果DataFrom=Query，则此设置表示查询的SQL语句，只返回一个字段，中间可以用{0}、{1}做格式参数
        public string DataQueryParam = "";  // 如果DataFrom=Query，则此设置表示传入的格式参数的Class属性名，多个属性用逗号隔开

        public ConfigObject ForeignObject = null;
        public List<ConfigCheck> CheckList = null;

        public object Value = null;
    }

    public class ConfigCheck
    {
        public string Type = "";                // 检查类型，清参考FieldCheckType
        public string ParentObjectType = "";    // 如果Type=Exist，则此设置表示检查外部表的对象类型
        public string ParentObjectField = "";   // 如果Type=Exist，则此设置表示检查外部表的对象的属性名
        public string CheckDataType = "";       // 如果Type=DataType，则此设置表示检查的数据类型：integer(整数)、numeric(数字)
        public string LengthExpression = "";    // 如果Type=Length，则此设置表示长度检查表达式，比如：{0}>=2、{0}>=2&&{0}<=5，其中{0}的带入值是数据实际长度
        public string DataRangeExpression = ""; // 如果Type=DataRange，则此设置表示范围检查表达式，比如：{0}>=2&&{0}<=5，{0}>={MinValue}，其中{0}的带入值是数据实际值，{MinValue}表示取Class的MinValue属性值
        public string ExistCheckSql = "";       // 如果Type=Exist，则此设置检查数据存在性的SQL语句，返回count(*)格式，SQL语句中可以包含{0}作为实际数据的占位格式
    }

    public class MatchType
    {
        private Dictionary<string, Dictionary<string, string>> list = new Dictionary<string, Dictionary<string, string>>();

        public void Add(string name, string value, string text)
        {
            name = name.ToUpper();
            text = text.ToUpper();
            Dictionary<string, string> dic = null;
            if (list.ContainsKey(name) == true)
                dic = list[name];
            else
            {
                dic = new Dictionary<string, string>();
                list.Add(name, dic);
            }
            if (dic.ContainsKey(text) == false)
                dic.Add(text,value);
            else
                dic[text] = value;
        }

        public string GetValue(string name, string text)
        {
            name = name.ToUpper();
            text = text.ToUpper();
            Dictionary<string, string> dic = list[name];
            if (dic == null)
                return "";

            string ret = null;
            try
            {
                ret = dic[text];
            }
            catch
            {
                ret = string.Empty;
            }
            if (ret == null)
                return string.Empty;

            return ret;
        }

    }
    
}
