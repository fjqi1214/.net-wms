using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace BenQGuru.eMES.DataExp
{
    /// <summary>
    /// 导出数据的配置类
    /// </summary>
    public class DataExpSchema
    {
        private string ConfigFileName = "BenQGuru.eMES.DataExp.xml";
        List<DataExpObject> ObjectList = new List<DataExpObject>();

        //public BenQGuru.eMES.Web.Helper.ConsoleLog Log = null; 

        public DataExpSchema()
        {
            //Log = new BenQGuru.eMES.Web.Helper.ConsoleLog();
            string ConfigFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, this.ConfigFileName);
 
            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigFilePath);

            XmlNodeList nodes= doc.SelectNodes("//object");
            if (nodes != null && nodes.Count > 0)
            {
                foreach (XmlNode node in nodes)
                {
                    DataExpObject expObject = new DataExpObject();
                    expObject.Type = node.Attributes["Type"].Value;

                    if (node.Attributes["Name"] != null)
                        expObject.Name = node.Attributes["Name"].Value;
                    else
                        continue;

                    if (node.Attributes["Type"] != null)
                        expObject.Type = node.Attributes["Type"].Value;
                   
                    if (node.Attributes["NeedTitle"] != null)
                        expObject.NeedTitle = node.Attributes["NeedTitle"].Value=="1";

                    if (node.Attributes["FilePath"] != null)
                    {
                        expObject.FilePath = node.Attributes["FilePath"].Value;
                    }

                    if (node.Attributes["SeparateChar"] != null)
                    {
                        expObject.SeparateChar = node.Attributes["SeparateChar"].Value;
                    }

                    this.ObjectList.Add(expObject);
                    this.ParseFileName(node,expObject);
                    this.ParseFieldList(node, expObject); 
                }
            }
        }

        /// <summary>
        /// 分析文件名部分
        /// </summary>
        /// <param name="node"></param>
        /// <param name="expObject"></param>
        private void ParseFileName(XmlNode node, DataExpObject expObject)
        {
            XmlNode fileNode = node.SelectSingleNode("./FileName");
            if (fileNode != null)
            {
                DataExpFileName fileName = new DataExpFileName();
                expObject.FileName = fileName;
                fileName.Format = fileNode.Attributes["Format"].Value;

                XmlNodeList childnodes = fileNode.SelectSingleNode("./FormatList").SelectNodes("./Format");
                if (childnodes != null && childnodes.Count > 0)
                {
                    foreach (XmlNode childnode in childnodes)
                    {
                        DataExpFormat format = new DataExpFormat();
                        fileName.FormatList.Add(format);
                        format.Type = childnode.Attributes["Type"].Value;
                        format.Value = childnode.Attributes["Value"].Value;

                        XmlNodeList paramterList = childnode.SelectNodes(".//Parameter");
                        if (paramterList != null && paramterList.Count > 0)
                        {
                            foreach (XmlNode paramNode in paramterList)
                            {
                                DataExpParameter param = new DataExpParameter();
                                format.ParameterList.Add(param);
                                param.Seq = int.Parse(paramNode.Attributes["Seq"].Value);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 分析字段列表部分
        /// </summary>
        /// <param name="node"></param>
        /// <param name="expObject"></param>
        private void ParseFieldList(XmlNode node, DataExpObject expObject)
        {
            XmlNodeList childnodes = node.SelectSingleNode("./FieldList").SelectNodes("./Field");
            if (childnodes != null && childnodes.Count > 0)
            {
                foreach (XmlNode childnode in childnodes)
                {
                    DataExpField field = new DataExpField();
                    field.Name = childnode.Attributes["Name"].Value;

                    if (childnode.Attributes["RightPadLen"] != null)
                    {
                        field.RightPadLen = int.Parse(childnode.Attributes["RightPadLen"].Value);
                    }

                    if (childnode.Attributes["LeftPadLen"] != null)
                    {
                        field.LeftPadLen = int.Parse(childnode.Attributes["LeftPadLen"].Value);
                    }

                    if (childnode.Attributes["ConstChar"] != null)
                    {
                        field.ConstChar = childnode.Attributes["ConstChar"].Value;
                    }

                    if (childnode.Attributes["PadChar"] != null)
                    {
                        field.PadChar = childnode.Attributes["PadChar"].Value;
                    }

                    expObject.FieldList.Add(field);
                }
            }
        }

        public DataExpObject GetDataExpObject(string name)
        {
            foreach (DataExpObject obj in this.ObjectList)
            {
                if (obj.Name.ToUpper() == name.ToUpper())
                {
                    return obj;
                }
            }

            return null;
        }
    }
}
