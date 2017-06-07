using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Collections;
using System.IO;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.SATOPrint
{
    public class SATOPrintHelper : MarshalByRefObject
    {
        public SATOPrintHelper()
        {
            
        }        

        public static ArrayList ParsePrintFile(string fileName, StringDictionary ValueList)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            ArrayList printValueList = new ArrayList();//用来存打印的值

            FileStream textReader = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            string lineValue = string.Empty;
            StreamReader streamReader = new StreamReader(textReader);
            lineValue = streamReader.ReadLine().Trim();
            //替换Esc
            byte[] byteArray = new byte[] { (byte)27 };
            string Esc = asciiEncoding.GetString(byteArray);

            while (lineValue != null)
            {
                lineValue=lineValue.Replace("[Esc]", Esc);
                while(lineValue.IndexOf("[$") != -1)
                {
                    string variablePre = string.Empty;  //[$Variable] 之前的字符串
                    string variableBehind = string.Empty;  //[$Variable] 之后的字符串
                    string variable = string.Empty;  //[$Variable] 
                    int variableEndIndex = 0;
                    string variableKey = string.Empty;

                    variablePre = lineValue.Substring(0,lineValue.IndexOf("[$"));
                    variableEndIndex = lineValue.Substring(variablePre.Length).IndexOf("]");
                    variableBehind = lineValue.Substring(variablePre.Length+variableEndIndex+1);
                    variable = lineValue.Substring(variablePre.Length,lineValue.Length-variablePre.Length);
                    variable = variable.Substring(0,variable.IndexOf("]")+1);

                    variableKey = variable.TrimStart('[').TrimEnd(']').TrimStart('$');
                    if (ValueList.ContainsKey(variableKey))
                    {
                        variable = ValueList[variableKey];
                    }
                    else
                    {
                        variable = string.Empty;
                    }                    
                    lineValue = variablePre + variable + variableBehind;
                }
                printValueList.Add(lineValue);
                lineValue = streamReader.ReadLine();
            }
            streamReader.Close();
            if (printValueList.Count == 0)
            {
                return null;
            }
            return printValueList;
        }

        public static StringDictionary InitPrintDictionary()
        {
            StringDictionary stringDictionary = new StringDictionary();
            //SystemSettingFacade systemSettingFacade = new SystemSettingFacade();
            //object[] allKey = systemSettingFacade.GetParametersByParameterGroup("PRINTKEYGROUP");
            //if (allKey != null)
            //{
            //    foreach (Parameter parameter in allKey)
            //    {
            //        stringDictionary.Add(parameter.ParameterCode, string.Empty);
            //    }
            //}

            return stringDictionary;
        }


    }
}
