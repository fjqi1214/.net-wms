using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace BenQGuru.eMES.SAPDataTransfer
{
    internal static class SerializeUtil
    {
        internal static string SerializeFile(string fileName, Type type2Serialize, object obj2Serialize)
        {
            XmlSerializer serializer = new XmlSerializer(type2Serialize);
            string xmlFilePath = SerializeUtil.GetFileDirectory();
            string fileFullPath = xmlFilePath + fileName;
            StreamWriter inputWriter = File.CreateText(fileFullPath);
            serializer.Serialize(inputWriter, obj2Serialize);
            inputWriter.Close();
            serializer = null;

            return fileFullPath;
        }

        internal static string GetFileDirectory()
        {
            string xmlFilePath = InternalVariables.MS_XMLFilePath;
            if (!Directory.Exists(xmlFilePath))
            {
                Directory.CreateDirectory(xmlFilePath);
            }
            return xmlFilePath;
        }
    }
}
