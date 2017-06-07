using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BenQGuru.eMES.CodeSoftPrint
{
    /// <summary>
    /// 对codesoft 打印软件的简单封装(ActiveX控件的封装)
    /// </summary>
    public class CodeSoftFacade
    {
        private string _printerName = string.Empty;
        private string _templatePath = string.Empty;
        LabelManager2.IApplication _app;
        LabelManager2.Document _doc;

        //为测试时系统中没有安装CodeSoft使用
        private static string NoCodeSoft = System.Configuration.ConfigurationSettings.AppSettings["NoCodeSoft"];

        public CodeSoftFacade(string printername, string path)
            : this()
        {
            if (NoCodeSoft != "1")
                OpenTemplate(printername, path);
        }

        public void OpenTemplate(string printername, string path)
        {

                if (NoCodeSoft != "1")
                {
                    if (this._templatePath != path)
                    {
                        this._printerName = printername;
                        this._templatePath = path;

                        if (this._templatePath == null || this._templatePath == string.Empty)
                        {
                            throw new Exception("$ERROR_Label_template_empty");
                        }

                        if (_app == null)
                            throw new Exception("$ERROR_Open_CodeSoft !");

                        /// 设定标签模板文件路径
                        _doc = _app.Documents.Open(_templatePath, true);

                        if (_doc == null)
                        {
                            throw new Exception("$ERROR_Label_Open");
                        }

                        /// 设定标签打印时用的打印机
                        if (this._printerName != null && this._printerName != string.Empty)
                        {
                            if (_doc.Printer == null)
                                throw new Exception("$ERROR_Set_Printer_ERROR !");

                            _doc.Printer.SwitchTo(this._printerName, string.Empty, false);

                        }
                    }
                }
            

                

        }

        public void OpenTemplateMaterialLot(string printername, string fileName, string varName)
        {
            if (NoCodeSoft != "1")
            {
                string path = string.Empty;
                path = System.Environment.CurrentDirectory + "\\" + fileName;

                if (this._templatePath != path)
                {
                    this._printerName = printername;
                    this._templatePath = path;

                    if (string.IsNullOrEmpty(varName))
                    {
                        throw new Exception("$Print_VarName_ISempty");
                    }

                    if (this._templatePath == null || this._templatePath == string.Empty)
                    {
                        throw new Exception("$ERROR_Label_template_empty");
                    }

                    if (_app == null)
                        throw new Exception("$ERROR_Open_CodeSoft !");

                    /// 设定标签模板文件路径
                    _doc = _app.Documents.Open(_templatePath, true);

                    if (_doc == null)
                    {
                        throw new Exception("$ERROR_Label_Open： " + fileName);

                    }

                    /// 设定标签打印时用的打印机
                    if (this._printerName != null && this._printerName != string.Empty)
                    {
                        if (_doc.Printer == null)
                            throw new Exception("$ERROR_Set_Printer_ERROR !");

                        _doc.Printer.SwitchTo(this._printerName, string.Empty, false);

                    }
                }
            }
        }
        public CodeSoftFacade()
        {
            if (NoCodeSoft != "1")
            {
                _app = new LabelManager2.Application();

                if (_app == null)
                    throw new Exception("$ERROR_Open_CodeSoft !");

                _app.Visible = false;
            }
        }

        ~CodeSoftFacade()
        {
            this.ReleaseCom();
        }
        //added by leon yuan 2008/05/28, 允许一次打印多张标签
        private LabelPrintVars _labelPrintVars = new LabelPrintVars();
        public LabelPrintVars LabelPrintVars
        {
            get
            {
                return _labelPrintVars;
            }
            set
            {
                _labelPrintVars = value;
            }
        }

        //将相应的variable传给模板文件进行打印
        public void Print(string[] vars)
        {
            try
            {
                if (NoCodeSoft != "1")
                {
                    if (_doc == null)
                    {
                        throw new Exception("$ERROR_Label_Open 2");
                    }

                    if (_doc.Variables == null)
                        throw new Exception("$ERROR_Open_Label_Variable 1!");

                    if (vars == null)
                        throw new Exception("$ERROR_Pass_Params 2!");

                    if (_doc.Variables.Count < vars.Length)
                        throw new Exception("$ERROR_Lable_Vars_Count");

                    if (vars != null && vars.Length > 0)
                    {
                        for (int i = 0; i < vars.Length; i++)
                        {
                            LabelManager2.Variable var = (LabelManager2.Variable)_doc.Variables.Item("var" + i.ToString());

                            if (var == null)
                                throw new Exception("$ERROR_Open_Label_Variable 3! " + i.ToString());

                            var.Value = vars[i];
                        }

                        //added by leon yuan 2008/05/28, 允许一次打印多张标签
                        //process #3
                        for (int i = 0; i < _labelPrintVars.LabelVars_No3.Length; i++)
                        {
                            LabelManager2.Variable var = (LabelManager2.Variable)_doc.Variables.Item(_labelPrintVars.LabelVars_No3[i]);

                            if (_labelPrintVars.LabelValues_No3[i].Trim().Length > 0)
                            {

                                if (var == null)
                                    throw new Exception("$ERROR_Open_Label_Variable 4! " + i.ToString());

                                var.Value = _labelPrintVars.LabelValues_No3[i].Trim();
                            }
                            else
                            {
                                if (var != null)
                                {

                                    var.Value = "";
                                }
                            }
                        }
                        //Process #2
                        for (int i = 0; i < _labelPrintVars.LabelVars_No2.Length; i++)
                        {
                            LabelManager2.Variable var = (LabelManager2.Variable)_doc.Variables.Item(_labelPrintVars.LabelVars_No2[i]);

                            if (_labelPrintVars.LabelValues_No2[i].Trim().Length > 0)
                            {
                                if (var == null)
                                    throw new Exception("$ERROR_Open_Label_Variable 4! " + i.ToString());

                                var.Value = _labelPrintVars.LabelValues_No2[i].Trim();
                            }
                            else
                            {
                                if (var != null)
                                {

                                    var.Value = "";
                                }
                            }
                        }

                        _doc.PrintDocument(1);
                    }
                }
            }
            finally
            {
                ReleaseCom();
            }
        }


        //bighai 20090306
        public void Print(string[] vars, string varName, string fileName)
        {
            

            if (NoCodeSoft != "1")
            {
                if (_doc == null)
                {
                    throw new Exception("$ERROR_Label_Open：" + fileName);
                }

                if (_doc.Variables == null)
                    //throw new Exception("打开模板变量出错 !");
                    throw new Exception("$ERROR_Open_Label_Variable ：" + varName);

                if (vars == null)
                    throw new Exception("$ERROR_Pass_Params !");

                if (_doc.Variables.Count < vars.Length)
                    throw new Exception("$ERROR_Lable_Vars_Count");

                if (vars != null && vars.Length > 0)
                {
                    for (int i = 0; i < vars.Length; i++)
                    {
                        LabelManager2.Variable var = (LabelManager2.Variable)_doc.Variables.Item(varName);

                        if (var == null)
                            throw new Exception("$ERROR_Open_Label_Variable ：" + varName);

                        var.Value = vars[i];
                       
                    }

                    _doc.PrintDocument(1);
                   
                }
            }
        }

        public void Print(string strDataFile, string strDescFile)
        {
            try
            {
                if (NoCodeSoft != "1")
                {
                    if (_doc == null)
                    {
                        throw new Exception("$ERROR_Label_Open 2");
                    }

                    if (_doc.Variables == null)
                        throw new Exception("$ERROR_Open_Label_Variable !");

                    if (!System.IO.File.Exists(strDescFile))
                        throw new Exception("$ERROR_DescFile_Not_Exists !");

                    if (!System.IO.File.Exists(strDataFile))
                        throw new Exception("$ERROR_DescFile_Not_Exists !");

                    _doc.Database.OpenASCII(strDataFile, strDescFile);

                    _doc.Merge(1, 1, 1, 1, 1, "");

                }
            }
            finally
            {
                ReleaseCom();
            }
        }

        /// <summary>
        /// 释放对code soft activeX 控件的引用
        /// </summary>
        public void ReleaseCom()
        {
            if (_app != null)
            {
                try
                {
                    _app.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
                    _app = null;
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 打印时，不再此方法中释放com口，用来多标签打印,在外层方法中手动释放com口,增加批量打印功能
        /// </summary>
        /// <param name="valueList"></param>
        public void PrintWithOutReleaseCom(List<System.Collections.Specialized.StringDictionary> valueLists)
        {
            try
            {
                if (NoCodeSoft != "1")
                {
                    if (_doc == null)
                    {
                        throw new Exception("$ERROR_Label_Open 2");
                    }

                    if (_doc.Variables == null)
                        throw new Exception("$ERROR_Open_Label_Variable  1!");

                    if (valueLists == null || valueLists.Count <= 0)
                    {
                        throw new Exception("$ERROR_Pass_Params 2!");
                    }

                    foreach (System.Collections.Specialized.StringDictionary valueList in valueLists)
                    {
                        if (valueList == null)
                            throw new Exception("$ERROR_Pass_Params 2!");

                        //if (_doc.Variables.Count < valueList.Count)
                        //    throw new Exception("$ERROR_Lable_Vars_Count");

                        if (valueList != null && valueList.Count > 0 && _doc.Variables.Count > 0)
                        {
                            LabelManager2.Variable var = null;
                            foreach (string itemKey in valueList.Keys)
                            {
                                if (_doc.Variables.Item(itemKey) != null)
                                {
                                    var = _doc.Variables.Item(itemKey);
                                    var.Value = valueList[itemKey];
                                }
                                else if (_doc.Variables.Item(itemKey + "1") != null)
                                {
                                    var = _doc.Variables.Item(itemKey + "1");//打印模板参数输出值应为变量名+流水号，因只查到一个值，传过来的打印值未产生流水号，在此补足
                                    var.Value = valueList[itemKey];
                                }

                            }

                            _doc.PrintDocument(1);
                        }
                    }
                }
            }
            catch { }
        }

        public void Print(List<System.Collections.Specialized.StringDictionary> valueLists)
        {
            try
            {
                if (NoCodeSoft != "1")
                {
                    if (_doc == null)
                    {
                        throw new Exception("$ERROR_Label_Open 2");
                    }

                    if (_doc.Variables == null)
                        throw new Exception("$ERROR_Open_Label_Variable  1!");

                    if (valueLists == null || valueLists.Count <= 0)
                    {
                        throw new Exception("$ERROR_Pass_Params 2!");
                    }

                    foreach (System.Collections.Specialized.StringDictionary valueList in valueLists)
                    {
                        if (valueList == null)
                            throw new Exception("$ERROR_Pass_Params 2!");

                        //if (_doc.Variables.Count < valueList.Count)
                        //    throw new Exception("$ERROR_Lable_Vars_Count");

                        if (valueList != null && valueList.Count > 0 && _doc.Variables.Count > 0)
                        {
                            LabelManager2.Variable var = null;
                            foreach (string itemKey in valueList.Keys)
                            {
                                if (_doc.Variables.Item(itemKey) != null)
                                {
                                    var = _doc.Variables.Item(itemKey);
                                    var.Value = valueList[itemKey];
                                }
                                else if (_doc.Variables.Item(itemKey + "1") != null)
                                {
                                    var = _doc.Variables.Item(itemKey + "1");//打印模板参数输出值应为变量名+流水号，因只查到一个值，传过来的打印值未产生流水号，在此补足
                                    var.Value = valueList[itemKey];
                                }

                            }

                            _doc.PrintDocument(1);
                        }
                    }
                }
            }
            finally
            {
                ReleaseCom();
            }
        }
    }
}
