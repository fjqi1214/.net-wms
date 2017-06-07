using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

using ControlLibrary.Web.Language;

namespace BenQGuru.eMES.Web.ImportData
{
    [Serializable]
    public class ImportXMLHelper
    {
        #region 属性

        private string _XmlPath = string.Empty;
        private string _ImportType = string.Empty;

        public string XmlPath
        {
            get
            {
                return _XmlPath;
            }
            set
            {
                _XmlPath = value;
            }
        }

        public string ImportType
        {
            get
            {
                return _ImportType;
            }
            set
            {
                _ImportType = value;
            }
        }

        #endregion

        public ImportXMLHelper(string xmlPath)
        {
            this.XmlPath = xmlPath;
        }

        public ArrayList NotAllowNullField
        {
            get
            {
                return this.GetNotAllowNullField();//空的单元格不读出来
            }
        }



        private ArrayList GetImportType()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.XmlPath);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("datatype");

            ArrayList itArray = new ArrayList();

            if (nodeList.Count > 0)
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    string name = nodeList[i].Attributes["name"].InnerText;
                    string text = nodeList[i].Attributes["text"].InnerText;
                    DictionaryEntry de = new DictionaryEntry(name, text);
                    itArray.Add(de);
                }
            }

            return itArray;
        }

        //melo 添加于2006.12.4 用于多语言
        public ArrayList GetImportType(ControlLibrary.Web.Language.LanguageComponent languageComponent)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.XmlPath);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("datatype");

            ArrayList itArray = new ArrayList();

            if (nodeList.Count > 0)
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    string name = nodeList[i].Attributes["name"].InnerText;
                    string text = languageComponent.GetString("$Grid_" + name);
                    DictionaryEntry de = new DictionaryEntry(name, text);
                    itArray.Add(de);
                }
            }

            return itArray;
        }

        public Dictionary<string, string> GetGridColumns()
        {
            Dictionary<string, string> returnValue = new Dictionary<string, string>();

            XmlDocument document = new XmlDocument();
            document.Load(this.XmlPath);

            XmlNodeList dataTypeList = document.GetElementsByTagName("dataType");
            foreach (XmlNode dataType in dataTypeList)
            {
                if (string.Compare(dataType.Attributes["name"].Value, this.ImportType, true) == 0)
                {
                    foreach (XmlNode fieldList in dataType.ChildNodes)
                    {
                        if (string.Compare(fieldList.Name, "fieldList", true) == 0)
                        {
                            foreach (XmlNode field in fieldList.ChildNodes)
                            {
                                string key = field.Attributes["key"].Value;
                                string headerText = field.Attributes["headerText"].Value;
                                returnValue.Add(key, headerText);
                            }
                        }
                    }
                }
            }

            return returnValue;
        }

        private ArrayList GetNotAllowNullField()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.XmlPath);

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");

            ArrayList gbArray = new ArrayList();
            if (nodeList.Count > 0)
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    XmlNode node = nodeList[i];
                    if (node.ParentNode.Attributes["name"].InnerText.ToString() == this.ImportType)
                    {
                        if (node.ChildNodes.Count > 0)
                        {
                            for (int j = 0; j < node.ChildNodes.Count; j++)
                            {
                                if (bool.Parse(node.ChildNodes[j].Attributes["AllowNull"].InnerText.ToString())) continue;
                                string key = node.ChildNodes[j].Attributes["key"].InnerText.ToString();
                                string headerText = node.ChildNodes[j].Attributes["value"].InnerText.ToString();
                                DictionaryEntry de = new DictionaryEntry(key, headerText);
                                gbArray.Add(de);
                            }
                        }

                        break;
                    }
                }
            }
            return gbArray;
        }

        public bool NeedMatchCtoE(string key)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.XmlPath);

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");
            if (nodeList.Count > 0)
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    XmlNode node = nodeList[i];
                    if (node.ParentNode.Attributes["name"].InnerText.ToString() == this.ImportType)
                    {
                        if (node.ChildNodes.Count > 0)
                        {
                            for (int j = 0; j < node.ChildNodes.Count; j++)
                            {
                                if (string.Compare(key, node.ChildNodes[j].Attributes["key"].InnerText.ToString(), true) == 0)
                                {
                                    return bool.Parse(node.ChildNodes[j].Attributes["Match"].InnerText.ToString());
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 使用该方法前请现用NeedMatchCtoE验证
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string MatchType(string key)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.XmlPath);

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");
            if (nodeList.Count > 0)
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    XmlNode node = nodeList[i];
                    if (node.ParentNode.Attributes["name"].InnerText.ToString() == this.ImportType)
                    {
                        if (node.ChildNodes.Count > 0)
                        {
                            for (int j = 0; j < node.ChildNodes.Count; j++)
                            {
                                if (string.Compare(key, node.ChildNodes[j].Attributes["key"].InnerText.ToString(), true) == 0)
                                {
                                    return node.ChildNodes[j].Attributes["MatchType"].InnerText.ToString();
                                }
                            }
                        }

                        break;
                    }
                }
            }

            return string.Empty;
        }

        public string GetMatchKeyWord(string valueWord, string matchType)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string[] paths = this.XmlPath.Split('\\', '/');
            xmlDoc.Load(string.Join(@"\", paths, 0, paths.Length - 1) + @"\MatchType.xml");

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName(matchType);
            if (nodeList[0].ChildNodes.Count > 0)
            {
                for (int i = 0; i < nodeList[0].ChildNodes.Count; i++)
                {
                    XmlNode node = nodeList[0].ChildNodes[i];
                    if (string.Compare(valueWord, node.Attributes["value"].InnerText, true) == 0)
                    {
                        return node.Attributes["key"].InnerText;
                    }
                }
            }

            return string.Empty;
        }
    }
}
