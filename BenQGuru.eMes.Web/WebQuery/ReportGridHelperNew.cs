using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;
using Infragistics.Web.UI.GridControls;


namespace BenQGuru.eMES.Web.WebQuery
{
    public class ReportGridHelperNew
    {
        private const int columnWidth = 110;

        private IDomainDataProvider _DataProvider = null;
        private LanguageComponent _LanguageComponent = null;
        private WebDataGrid _Grid = null;
        private Hashtable _LanguageTable = new Hashtable();
        private DataTable dtSource = null;
        private GridHelperNew gridHelper = null;
        public ReportGridHelperNew(IDomainDataProvider dataProvider, LanguageComponent languageComponent, WebDataGrid grid, DataTable dt)
        {
            _DataProvider = dataProvider;
            _LanguageComponent = languageComponent;
            _Grid = grid;
            dtSource = dt;
        }

        #region 属性

        private object[] _DataSource = new object[0];
        private object[] _DataSourceForCompare = new object[0];
        private List<string> _Dim1PropertyList = new List<string>();
        private List<string> _Dim2PropertyList = new List<string>();
        private List<ReportGridDim3Property> _Dim3PropertyList = new List<ReportGridDim3Property>();
        private List<string> _Dim3DefaultValueList = new List<string>();

        private List<string> _FixedHeadDefaultValueList = new List<string>();
        private List<string> _FixedHeadDescDefaultValueList = new List<string>();

        private bool _HasDim3PropertyNameRowColumn = true;
        private bool _PutDim3PropertyNameAsColumn = true;
        private string _CompareType = string.Empty;
        private string _ByTimeType = string.Empty;

        public object[] DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        public object[] DataSourceForCompare
        {
            get { return _DataSourceForCompare; }
            set { _DataSourceForCompare = value; }
        }

        public List<string> Dim1PropertyList
        {
            get { return _Dim1PropertyList; }
            set { _Dim1PropertyList = value; }
        }

        public List<string> Dim2PropertyList
        {
            get { return _Dim2PropertyList; }
            set { _Dim2PropertyList = value; }
        }

        public List<ReportGridDim3Property> Dim3PropertyList
        {
            get { return _Dim3PropertyList; }
            set { _Dim3PropertyList = value; }
        }

        public List<string> Dim3DefaultValueList
        {
            get { return _Dim3DefaultValueList; }
            set { _Dim3DefaultValueList = value; }
        }

        public List<string> FixedHeadDescDefaultValueList
        {
            get { return _FixedHeadDescDefaultValueList; }
            set { _FixedHeadDescDefaultValueList = value; }
        }

        public List<string> FixedHeadDefaultValueList
        {
            get { return _FixedHeadDefaultValueList; }
            set { _FixedHeadDefaultValueList = value; }
        }

        public bool HasDim3PropertyNameRowColumn
        {
            get { return _HasDim3PropertyNameRowColumn; }
            set { _HasDim3PropertyNameRowColumn = value; }
        }

        public bool PutDim3PropertyNameAsColumn
        {
            get { return _PutDim3PropertyNameAsColumn; }
            set { _PutDim3PropertyNameAsColumn = value; }
        }

        public string CompareType
        {
            get { return _CompareType; }
            set { _CompareType = value; }
        }

        public string ByTimeType
        {
            get { return _ByTimeType; }
            set { _ByTimeType = value; }
        }

        #endregion

        #region 辅助函数

        public static string StringCalc(string op, string a, string b)
        {
            return StringCalc(op, a, b, "0");
        }

        public static string StringCalc(string op, string a, string b, string resultWhenError)
        {
            if (a == null || a.Trim().Length <= 0)
            {
                a = "0";
            }
            if (b == null || b.Trim().Length <= 0)
            {
                b = "0";
            }

            if (a.IndexOf(".") != a.LastIndexOf("."))
            {
                int pos = a.Substring(a.IndexOf(".") + 1).IndexOf(".");
                a = a.Substring(0, pos);
            }

            if (b.IndexOf(".") != b.LastIndexOf("."))
            {
                int pos = b.Substring(b.IndexOf(".") + 1).IndexOf(".");
                b = b.Substring(0, pos);
            }

            string returnValue = string.Empty;

            switch (op.ToUpper())
            {
                case "ADD":
                    if (a.IndexOf(".") >= 0 || b.IndexOf(".") >= 0 || a.IndexOf("%") >= 0 || b.IndexOf("%") >= 0)
                    {
                        double aReal = 0;
                        double bReal = 0;

                        if (a.IndexOf("%") >= 0)
                        {
                            aReal = double.Parse(a.Substring(0, a.IndexOf("%"))) / 100;
                        }
                        else
                        {
                            aReal = double.Parse(a);
                        }

                        if (b.IndexOf("%") >= 0)
                        {
                            bReal = double.Parse(b.Substring(0, b.IndexOf("%"))) / 100;
                        }
                        else
                        {
                            bReal = double.Parse(b);
                        }

                        returnValue = Convert.ToString(aReal + bReal);

                    }
                    else
                    {
                        returnValue = Convert.ToString(int.Parse(a) + int.Parse(b));
                    }
                    break;

                case "SUB":
                    if (a.IndexOf(".") >= 0 || b.IndexOf(".") >= 0 || a.IndexOf("%") >= 0 || b.IndexOf("%") >= 0)
                    {
                        double aReal = 0;
                        double bReal = 0;

                        if (a.IndexOf("%") >= 0)
                        {
                            aReal = double.Parse(a.Substring(0, a.IndexOf("%"))) / 100;
                        }
                        else
                        {
                            aReal = double.Parse(a);
                        }

                        if (b.IndexOf("%") >= 0)
                        {
                            bReal = double.Parse(b.Substring(0, b.IndexOf("%"))) / 100;
                        }
                        else
                        {
                            bReal = double.Parse(b);
                        }

                        returnValue = Convert.ToString(aReal - bReal);

                    }
                    else
                    {
                        returnValue = Convert.ToString(int.Parse(a) - int.Parse(b));
                    }
                    break;

                case "MAX":
                    if (a.IndexOf(".") >= 0 || b.IndexOf(".") >= 0 || a.IndexOf("%") >= 0 || b.IndexOf("%") >= 0)
                    {
                        double aReal = 0;
                        double bReal = 0;

                        if (a.IndexOf("%") >= 0)
                        {
                            aReal = double.Parse(a.Substring(0, a.IndexOf("%"))) / 100;
                        }
                        else
                        {
                            aReal = double.Parse(a);
                        }

                        if (b.IndexOf("%") >= 0)
                        {
                            bReal = double.Parse(b.Substring(0, b.IndexOf("%"))) / 100;
                        }
                        else
                        {
                            bReal = double.Parse(b);
                        }

                        returnValue = Convert.ToString(Math.Max(aReal, bReal));

                    }
                    else
                    {
                        returnValue = Convert.ToString(Math.Max(int.Parse(a), int.Parse(b)));
                    }
                    break;

                case "MUL":
                    if (a.IndexOf(".") >= 0 || b.IndexOf(".") >= 0 || a.IndexOf("%") >= 0 || b.IndexOf("%") >= 0)
                    {
                        double aReal = 0;
                        double bReal = 0;

                        if (a.IndexOf("%") >= 0)
                        {
                            aReal = double.Parse(a.Substring(0, a.IndexOf("%"))) / 100;
                        }
                        else
                        {
                            aReal = double.Parse(a);
                        }

                        if (b.IndexOf("%") >= 0)
                        {
                            bReal = double.Parse(b.Substring(0, b.IndexOf("%"))) / 100;
                        }
                        else
                        {
                            bReal = double.Parse(b);
                        }

                        returnValue = Convert.ToString(aReal * bReal);

                    }
                    else
                    {
                        returnValue = Convert.ToString(int.Parse(a) * int.Parse(b));
                    }
                    break;

                case "DIV":
                    if (a.IndexOf(".") >= 0 || b.IndexOf(".") >= 0 || a.IndexOf("%") >= 0 || b.IndexOf("%") >= 0)
                    {

                        double aReal = 0;
                        double bReal = 0;

                        if (a.IndexOf("%") >= 0)
                        {
                            aReal = double.Parse(a.Substring(0, a.IndexOf("%"))) / 100;
                        }
                        else
                        {
                            aReal = double.Parse(a);
                        }

                        if (b.IndexOf("%") >= 0)
                        {
                            bReal = double.Parse(b.Substring(0, b.IndexOf("%"))) / 100;
                        }
                        else
                        {
                            bReal = double.Parse(b);
                        }

                        if (bReal == 0)
                        {
                            returnValue = resultWhenError;
                        }
                        else
                        {
                            returnValue = Convert.ToString(aReal / bReal);
                        }
                    }
                    else
                    {
                        if (int.Parse(b) == 0)
                        {
                            returnValue = resultWhenError;
                        }
                        else
                        {
                            returnValue = Convert.ToString(int.Parse(a) / int.Parse(b));
                        }
                    }
                    break;

                default:
                    returnValue = a;
                    break;
            }

            return returnValue;
        }

        private string FormatText(string text, string format)
        {
            string returnValue = text;

            if (format.Trim().Length > 0)
            {
                double result = 0;
                if (double.TryParse(text, out result))
                {
                    returnValue = result.ToString(format);
                }
            }

            return returnValue;
        }

        private string GetFomularName(string formular)
        {
            string returnValue = string.Empty;

            if (formular.IndexOf("(") >= 0)
            {
                returnValue = formular.Substring(0, formular.IndexOf("("));
            }
            else
            {
                returnValue = formular;
            }

            switch (returnValue.Trim().ToUpper())
            {
                case "SUM":
                    returnValue = "Summary";
                    break;

                case "PPM":
                    returnValue = "Summary";
                    break;

                case "MAX":
                    returnValue = "Max";
                    break;

                case "DIV":
                    returnValue = "Summary";
                    break;

                case "DIVS":
                    returnValue = "Summary";
                    break;

                case "AVG":
                    returnValue = "Avg";
                    break;

                case "AVGT":
                    returnValue = "Avg";
                    break;

                default:
                    break;
            }

            return returnValue;
        }

        private void ParseFomular(string formular, out string op, out List<int> paramRefList)
        {
            op = formular;
            paramRefList = new List<int>();

            try
            {

                if (formular.IndexOf("(") >= 0)
                {
                    op = formular.Substring(0, formular.IndexOf("("));

                    string paramString = formular.Substring(formular.IndexOf("(") + 1, formular.IndexOf(")") - formular.IndexOf("(") - 1);
                    string[] paramArray = paramString.Split(',');
                    if (paramArray != null)
                    {
                        foreach (string param in paramArray)
                        {
                            int posRef = 0;
                            int.TryParse(param.Substring(param.IndexOf("{") + 1, param.IndexOf("}") - param.IndexOf("{") - 1), out posRef);
                            if (posRef != 0)
                            {
                                paramRefList.Add(posRef);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private string Translate(string key)
        {
            string returnValue = key;

            object obj = _LanguageTable[key];
            if (obj == null)
            {
                if (_LanguageComponent != null)
                {
                    string lang = _LanguageComponent.GetString(key);
                    if (lang.Trim().Length > 0)
                    {
                        returnValue = lang;
                        _LanguageTable.Add(key, lang);
                    }
                }
            }
            else
            {
                returnValue = obj.ToString();
            }

            return returnValue;
        }

        public static string GetValueByPropertyName(NewReportDomainObject domainObject, string propertyName)
        {
            string returnValue = string.Empty;
            FieldInfo fieldInfo = typeof(NewReportDomainObject).GetField(propertyName);
            if (fieldInfo != null)
            {
                object val = fieldInfo.GetValue(domainObject);
                if (val != null)
                {
                    returnValue = val.ToString();
                }
            }

            return returnValue;
        }

        public static void SetValueByPropertyName(NewReportDomainObject domainObject, string propertyName, string propertyValue)
        {
            FieldInfo fieldInfo = typeof(NewReportDomainObject).GetField(propertyName);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(domainObject, propertyValue);
            }
        }

        #endregion

        #region Grid操作辅助函数

        private DataRow GetNewRow(List<string> dim1ValueList, int dim2ColumnCount, string dim3PropertyName, List<string> dim3FormatList)
        {
            DataRow returnValue = null;
            List<string> valueList = new List<string>();

            //前方的Dim1固定列
            valueList.AddRange(dim1ValueList);

            //中间的Dim3PropertyName列
            if (_PutDim3PropertyNameAsColumn && _HasDim3PropertyNameRowColumn && dim3PropertyName.Trim().Length > 0)
            {
                valueList.Add(dim3PropertyName);
            }

            //后面的数据区

            for (int i = 0; i < dim2ColumnCount; i++)
            {
                for (int j = 0; j < dim3FormatList.Count; j++)
                {
                    string defaultValue = "0";
                    if (_Dim3DefaultValueList.Count > 0)
                    {
                        defaultValue = _Dim3DefaultValueList[j];
                    }
                    valueList.Add(FormatText(defaultValue, dim3FormatList[j]));
                }
            }
            returnValue = this.dtSource.NewRow();
            int colnum = 1;
            foreach (string str in (string[])valueList.ToArray())
            {
                if (colnum < dtSource.Columns.Count)
                {
                    returnValue[colnum] = str;
                    colnum++;
                }

            }
            if (returnValue.Table.Columns.Contains("GUID"))
            {
                returnValue["GUID"] = Guid.NewGuid().ToString();
            }
            return returnValue;
        }


        private string GetColumnString(NewReportDomainObject domainObject, List<string> propertyList, string splitter)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < propertyList.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(splitter);
                }
                builder.Append(GetValueByPropertyName(domainObject, propertyList[i]));
            }

            return builder.ToString();
        }

        private string GetColumnString(GridRecord row, List<string> columnKeyList, string splitter)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < columnKeyList.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(splitter);
                }
                builder.Append(row.Items.FindItemByKey(columnKeyList[i]).Text);
            }

            return builder.ToString();
        }

        private string GetDim2ColumnKey(NewReportDomainObject obj)
        {
            string returnValue = string.Empty;

            if (obj != null)
            {
                if (_Dim2PropertyList == null || _Dim2PropertyList.Count <= 0)
                {
                    if (obj.TimeString != null)
                    {
                        returnValue = obj.TimeString;
                    }
                }
                else
                {
                    foreach (string property in _Dim2PropertyList)
                    {
                        returnValue += GetValueByPropertyName(obj, property) + ",";
                    }
                }

                if (returnValue.Length > 0 && returnValue.Substring(returnValue.Length - 1) == ",")
                {
                    returnValue = returnValue.Substring(0, returnValue.Length - 1);
                }
            }

            return returnValue.Trim();
        }

        #endregion

        #region ShowGrid步骤

        private void InitGrid()
        {
            _Grid.Columns.Clear();
            dtSource.Columns.Clear();
            if (_DataSource == null)
            {
                return;
            }

            gridHelper = new GridHelperNew(_Grid, dtSource);

            //前方固定列
            foreach (string field in _Dim1PropertyList)
            {
                gridHelper.AddColumn(field, Translate(field), columnWidth);
            }

            //数据属性名称列
            if (_HasDim3PropertyNameRowColumn)
            {
                gridHelper.AddColumn("Values", "", columnWidth);
            }

            //时间维度列
            List<string> sortedColumnList = new List<string>();
            foreach (NewReportDomainObject obj in _DataSource)
            {
                if (!sortedColumnList.Contains(GetDim2ColumnKey(obj).Trim()))
                {
                    sortedColumnList.Add(GetDim2ColumnKey(obj).Trim());
                }
            }
            sortedColumnList.Sort();
            foreach (string column in sortedColumnList)
            {
                gridHelper.AddColumn(column, column, columnWidth);
            }
            _Grid.DataSource = dtSource;
            _Grid.DataBind();
        }

        private void InitGridFixOPHead()
        {
            _Grid.Columns.Clear();
            dtSource.Columns.Clear();
            if (_DataSource == null)
            {
                return;
            }

            GridHelperNew gridHelper = new GridHelperNew(_Grid, dtSource);

            //前方固定列
            foreach (string field in _Dim1PropertyList)
            {
                gridHelper.AddColumn(field, Translate(field), columnWidth);
            }

            int opCount = 0;
            foreach (string Field in _FixedHeadDescDefaultValueList)
            {
                opCount += 1;
                string OPKeyColunm = "OP" + Convert.ToString(opCount);
                gridHelper.AddColumn(OPKeyColunm, Field, columnWidth);
            }
            _Grid.DataSource = dtSource;
            _Grid.DataBind();
        }

        private void InitNormalGrid()
        {
            _Grid.Columns.Clear();
            dtSource.Columns.Clear();
            if (_DataSource == null)
            {
                return;
            }

            GridHelperNew gridHelper = new GridHelperNew(_Grid, dtSource);
            //前方固定列
            foreach (string field in _Dim1PropertyList)
            {
                gridHelper.AddColumn(field, Translate(field), columnWidth);
            }
            _Grid.DataSource = dtSource;
            _Grid.DataBind();
        }

        private void InsertGrid()
        {
            _Grid.Rows.Clear();
            dtSource.Rows.Clear();
            _CompareType = _CompareType.Trim().ToLower();
            int compareCount = (_CompareType.Trim().Length <= 0 ? 1 : 2);

            if (_DataSource != null && _DataSource.Length > 0)
            {
                //初始化rowGroupCaptionList
                List<string> rowGroupCaptionList = new List<string>();
                foreach (ReportGridDim3Property property in _Dim3PropertyList)
                {
                    rowGroupCaptionList.Add(Translate(property.Name));
                    if (_CompareType.Length > 0)
                    {
                        rowGroupCaptionList.Add(Translate(property.Name) + Translate(_CompareType));
                    }
                }

                //正式填充表格
                List<DataRow> rowGroupList = new List<DataRow>();
                string lastFixedColumn = string.Empty;
                string fixedColumn = string.Empty;
                for (int index = 0; index < _DataSource.Length; index++)
                {
                    NewReportDomainObject domainObject = (NewReportDomainObject)_DataSource[index];
                    rowGroupList.Clear();

                    //一般数据
                    fixedColumn = GetColumnString(domainObject, _Dim1PropertyList, "\t");

                    //查找到rowGroupList或者创建新行
                    if (index != 0 && string.Compare(lastFixedColumn, fixedColumn, true) == 0)
                    {
                        for (int i = 0; i < rowGroupCaptionList.Count; i++)
                        {
                            rowGroupList.Add(dtSource.Rows[_Grid.Rows.Count - rowGroupCaptionList.Count + i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < rowGroupCaptionList.Count; i++)
                        {
                            List<string> valueString = new List<string>();
                            if (fixedColumn.Trim(' ').Length > 0)
                            {
                                valueString.AddRange(fixedColumn.Split('\t'));
                            }
                            else if (_Dim1PropertyList.Count > 0)
                            {
                                valueString.Add(string.Empty);
                            }

                            List<string> dim3FormatList = new List<string>();
                            if (_PutDim3PropertyNameAsColumn)
                            {
                                dim3FormatList.Add(_Dim3PropertyList[i / compareCount].Format);
                            }
                            else
                            {
                                for (int j = 0; j < _Dim3PropertyList.Count; j++)
                                {
                                    dim3FormatList.Add(_Dim3PropertyList[j].Format);
                                }
                            }

                            DataRow row = GetNewRow(valueString, _Grid.Columns.Count, rowGroupCaptionList[i], dim3FormatList);
                            dtSource.Rows.Add(row);
                            rowGroupList.Add(row);
                        }
                    }

                    //添加数据区
                    for (int i = 0; i < rowGroupCaptionList.Count; i++)
                    {
                        for (int j = 0; j < _Dim3PropertyList.Count; j++)
                        {
                            string property = _Dim3PropertyList[j].Name;
                            string format = _Dim3PropertyList[j].Format;
                            if (string.Compare(rowGroupCaptionList[i], Translate(property), true) == 0)
                            {
                                rowGroupList[i][GetDim2ColumnKey(domainObject)] = FormatText(GetValueByPropertyName(domainObject, property), format);
                                //rowGroupList[i].Items.FindItemByKey(GetDim2ColumnKey(domainObject)).Text = FormatText(GetValueByPropertyName(domainObject, property), format);
                            }
                        }
                    }
                    lastFixedColumn = fixedColumn;
                }

                _Grid.DataSource = dtSource;
                _Grid.DataBind();
            }
        }

        private void InsertGridOnFixedOPHead()
        {
            _Grid.Rows.Clear();
            dtSource.Rows.Clear();
            int fixedCount = _Dim1PropertyList.Count;
            List<string> sortedColumnList = new List<string>();
            if (_DataSource != null && _DataSource.Length > 0)
            {
                //填充表格             
                string lastFixedColumn = string.Empty;
                string fixedColumn = string.Empty;

                //构造一行数据
                string[] colnmnValue = new string[fixedCount + _FixedHeadDescDefaultValueList.Count];

                //初始化都是空
                for (int i = 0; i < colnmnValue.Length; i++)
                {
                    colnmnValue[i] = string.Empty;
                }

                for (int index = 0; index < _DataSource.Length; index++)
                {
                    NewReportDomainObject domainObject = (NewReportDomainObject)_DataSource[index];

                    if (index == 0)
                    {
                        lastFixedColumn = GetColumnString(domainObject, _Dim1PropertyList, ",");
                    }
                    else
                    {
                        NewReportDomainObject domainObjectLast = (NewReportDomainObject)_DataSource[index - 1];
                        lastFixedColumn = GetColumnString(domainObjectLast, _Dim1PropertyList, ",");
                    }
                    fixedColumn = GetColumnString(domainObject, _Dim1PropertyList, ",");

                    if (string.Compare(fixedColumn, lastFixedColumn, true) != 0)
                    {
                        DataRow row = dtSource.NewRow();
                        int colnum = 0;
                        foreach (object columnvalue in colnmnValue)
                        {
                            row[colnum] = columnvalue;
                            colnum++;
                        }
                        dtSource.Rows.Add(row);
                        //_Grid.Rows.Add(row);

                        //初始化都是空
                        for (int i = 0; i < colnmnValue.Length; i++)
                        {
                            colnmnValue[i] = string.Empty;
                        }
                    }

                    string[] fixedlist = fixedColumn.Split(',');
                    for (int i = 0; i < fixedlist.Length; i++)
                    {
                        colnmnValue[i] = fixedlist[i];
                    }

                    int findColumnNO = this.GetColumnNumber(domainObject, fixedCount);//获取插入数据的顺序号
                    colnmnValue[findColumnNO] = stringFormat(domainObject.PassRcardRate);

                    if (index == _DataSource.Length - 1)
                    {
                        DataRow row = dtSource.NewRow();
                        int colnum = 0;
                        foreach (object columnvalue in colnmnValue)
                        {
                            row[colnum] = columnvalue;
                            colnum++;
                        }
                        dtSource.Rows.Add(row);
                    }
                }
                _Grid.DataSource = dtSource;
                _Grid.DataBind();
            }

            //数据汇总
            Double[] lineNumber = new Double[_FixedHeadDescDefaultValueList.Count];

            if (_Grid.Rows.Count > 0)
            {
                for (int rowCount = 0; rowCount < _Grid.Rows.Count; rowCount++)
                {
                    int columnCount = fixedCount;
                    Double PassRcardRate = 1;

                    for (int i = 0; i < lineNumber.Length; i++)
                    {
                        string columnString = _Grid.Rows[rowCount].Items[columnCount].Text.ToString().Trim();
                        if (columnString.IndexOf('%') > 0)
                        {
                            columnString = columnString.Substring(0, columnString.Length - 1);
                        }

                        lineNumber[i] = columnString == string.Empty ? 1 : Convert.ToDouble(columnString) / 100;
                        PassRcardRate *= lineNumber[i];
                        columnCount += 1;
                    }

                    _Grid.Rows[rowCount].Items[fixedCount + _FixedHeadDescDefaultValueList.Count - 1].Text = (Math.Round(PassRcardRate * 100, 2) / 100).ToString("0.00%");
                }
            }
        }

        private void InsertNormalGrid()
        {
            _Grid.Rows.Clear();
            dtSource.Rows.Clear();
            if (_DataSource != null && _DataSource.Length > 0)
            {
                foreach (NewReportDomainObject obj in _DataSource)
                {
                    object[] rowObject = new object[_Dim1PropertyList.Count];
                    for (int i = 0; i < rowObject.Length; i++)
                    {
                        rowObject[i] = GetCuerrtColunmValue(obj, _Dim1PropertyList[i]);
                    }
                    DataRow row = dtSource.NewRow();
                    int colnum = 0;
                    foreach (object rowobject in rowObject)
                    {
                        row[colnum] = rowobject;
                        colnum++;
                    }
                    dtSource.Rows.Add(row);
                }
                _Grid.DataSource = dtSource;
                _Grid.DataBind();
            }
        }

        private void UpdateGridForCompare()
        {
            _CompareType = _CompareType.Trim().ToLower();
            int compareCount = (_CompareType.Trim().Length <= 0 ? 1 : 2);

            if (_CompareType.Length <= 0)
            {
                return;
            }

            //初始化rowGroupCaptionList
            List<string> rowGroupCaptionList = new List<string>();
            foreach (ReportGridDim3Property property in _Dim3PropertyList)
            {
                rowGroupCaptionList.Add(Translate(property.Name) + Translate(_CompareType));
            }

            //正式填充表格 
            if (_DataSourceForCompare != null && _DataSourceForCompare.Length > 0)
            {
                int rowGroupCount = _Dim3PropertyList.Count * 2;

                List<GridRecord> rowGroupList = new List<GridRecord>();

                string fixedColumnFromObject = string.Empty;
                string fixedColumnFromGrid = string.Empty;

                foreach (NewReportDomainObject domainObject in _DataSourceForCompare)
                {
                    for (int rowIndex = 0; rowIndex < _Grid.Rows.Count; rowIndex += rowGroupCount)
                    {
                        fixedColumnFromObject = GetColumnString(domainObject, _Dim1PropertyList, "\t");
                        fixedColumnFromGrid = GetColumnString(_Grid.Rows[rowIndex], _Dim1PropertyList, "\t");

                        if (string.Compare(fixedColumnFromObject, fixedColumnFromGrid, true) != 0)
                        {
                            continue;
                        }

                        //Grid和Object可以匹配了（时间列可以找到）
                        if (_Grid.Rows[0].Items.FindItemByKey(GetDim2ColumnKey(domainObject)) != null)
                        {
                            rowGroupList.Clear();
                            for (int i = 0; i < rowGroupCount; i++)
                            {
                                rowGroupList.Add(_Grid.Rows[rowIndex + i]);
                            }

                            for (int i = 0; i < rowGroupCount; i++)
                            {
                                string valueType = string.Empty;
                                if (_HasDim3PropertyNameRowColumn)
                                {
                                    valueType = rowGroupList[i].Items.FindItemByKey("Values").Text;
                                }

                                for (int j = 0; j < _Dim3PropertyList.Count; j++)
                                {
                                    string property = _Dim3PropertyList[j].Name;
                                    string format = _Dim3PropertyList[j].Format;
                                    if (_HasDim3PropertyNameRowColumn && string.Compare(rowGroupCaptionList[j], valueType, true) == 0)
                                    {
                                        rowGroupList[i].Items.FindItemByKey(GetDim2ColumnKey(domainObject)).Text = FormatText(GetValueByPropertyName(domainObject, property), format);
                                    }
                                    else if (!_HasDim3PropertyNameRowColumn && i == j * compareCount + 1)
                                    {
                                        rowGroupList[i].Items.FindItemByKey(GetDim2ColumnKey(domainObject)).Text = FormatText(GetValueByPropertyName(domainObject, property), format);
                                    }
                                }
                            }
                        }
                    }

                    continue;
                }
            }
        }

        private void SetSummaryRowColumn()
        {
            gridHelper = new GridHelperNew(_Grid, dtSource);
            int columnWidth = 110;

            int compareCount = (_CompareType.Trim().Length <= 0 ? 1 : 2);
            int groupCount = _Dim3PropertyList.Count * compareCount;

            //尾列的汇总
            if (_Dim3PropertyList.Count > 0)
            {
                bool found = false;
                foreach (ReportGridDim3Property property in _Dim3PropertyList)
                {
                    if (property.EndColumnFomular.Trim().Length > 0)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    //添加列
                    string endColumnName = string.Empty;
                    for (int i = 0; i < _Dim3PropertyList.Count; i++)
                    {
                        if (!_Dim3PropertyList[i].Hidden)
                        {
                            endColumnName = GetFomularName(_Dim3PropertyList[i].EndColumnFomular);
                            break;
                        }
                    }
                    gridHelper.AddColumn(endColumnName, _LanguageComponent.GetString(endColumnName), columnWidth);
                    dtSource = gridHelper.dtSource;
                    this._Grid.ClearDataSource();
                    this._Grid.DataBind();
                    _Grid.DataSource = dtSource;
                    _Grid.DataBind();

                    //写入数据
                    for (int i = 0; i < _Grid.Rows.Count / groupCount; i++)
                    {
                        for (int j = 0; j < _Dim3PropertyList.Count; j++)
                        {
                            for (int k = 0; k < compareCount; k++)
                            {
                                SetEndColumnText(i * groupCount + j * compareCount + k, _Dim3PropertyList[j].EndColumnFomular, _Dim3PropertyList[j].Format);
                            }
                        }
                    }
                }
            }

            //底行的汇总
            if (_Dim3PropertyList.Count > 0)
            {
                bool found = false;
                foreach (ReportGridDim3Property property in _Dim3PropertyList)
                {
                    if (property.BottemRowFomular.Trim().Length > 0)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    int firstColumnIndex = _Dim1PropertyList.Count + (_HasDim3PropertyNameRowColumn ? 1 : 0);
                    int lastColumnIndex = _Grid.Columns.Count - 1;

                    for (int i = 0; i < _Dim3PropertyList.Count; i++)
                    {
                        for (int j = 0; j < compareCount; j++)
                        {
                            //添加新行
                            List<string> valueString = new List<string>();
                            for (int k = 0; k < _Dim1PropertyList.Count; k++)
                            {
                                valueString.Add("");
                            }
                            string caption = Translate(_Dim3PropertyList[i].Name);
                            if (j % 2 == 1)
                            {
                                caption += Translate(_CompareType);
                            }
                            caption += Translate(GetFomularName(_Dim3PropertyList[i].BottemRowFomular));

                            List<string> dim3FormatList = new List<string>();
                            if (_PutDim3PropertyNameAsColumn)
                            {
                                dim3FormatList.Add(_Dim3PropertyList[i].Format);
                            }
                            else
                            {
                                for (int k = 0; k < _Dim3PropertyList.Count; k++)
                                {
                                    dim3FormatList.Add(_Dim3PropertyList[k].Format);
                                }
                            }

                            DataRow row = GetNewRow(valueString, _Grid.Columns.Count, caption, dim3FormatList);
                            dtSource.Rows.Add(row);

                            this._Grid.ClearDataSource();
                            this._Grid.DataBind();
                            //this._Grid.Rows.Add(row);
                            this._Grid.DataSource = dtSource;
                            this._Grid.DataBind();
                            for (int k = firstColumnIndex; k < lastColumnIndex + 1; k++)
                            {
                                SetBottomRowText(k, i * compareCount + j, groupCount, _Dim3PropertyList[i].EndColumnFomular, _Dim3PropertyList[i].Format);
                            }

                        }
                    }
                }
            }
        }

        private void SetEndColumnText(int rowIndex, string formular, string format)
        {
            GridRecord row = _Grid.Rows[rowIndex];
            DataRow dataRow = dtSource.Rows[rowIndex];
            int columnCount = _Grid.Columns.Count;
            int compareCount = (_CompareType.Trim().Length <= 0 ? 1 : 2);

            int firstColumnIndex = _Dim1PropertyList.Count + (_HasDim3PropertyNameRowColumn ? 1 : 0);
            int lastColumnIndex = columnCount - 2;

            string fomularOP = string.Empty;
            List<int> paramRefList = new List<int>();
            ParseFomular(formular, out fomularOP, out paramRefList);

            string result = "0";
            string totalCount = "0";
            string totalQty = "0";

            switch (fomularOP.ToUpper())
            {
                case "SUM":
                    for (int i = firstColumnIndex; i < lastColumnIndex + 1; i++)
                    {
                        result = StringCalc("ADD", result.ToString(), row.Items[i].Text);
                    }
                    row.Items[columnCount - 1].Text = FormatText(result, format);
                    dataRow[columnCount] = FormatText(result, format);
                    break;

                case "MAX":
                    for (int i = firstColumnIndex; i < lastColumnIndex + 1; i++)
                    {
                        result = StringCalc("MAX", result.ToString(), row.Items[i].Text);
                    }
                    row.Items[columnCount - 1].Text = FormatText(result, format);
                    dataRow[columnCount] = FormatText(result, format);
                    break;

                case "DIV":
                    if (paramRefList.Count > 0)
                    {
                        result = StringCalc("DIV", _Grid.Rows[rowIndex + paramRefList[0] * compareCount].Items[columnCount - 1].Text.Trim() + ".0", _Grid.Rows[rowIndex + paramRefList[1] * compareCount].Items[columnCount - 1].Text);
                        row.Items[columnCount - 1].Text = FormatText(result, format);
                        dataRow[columnCount] = FormatText(result, format);
                    }
                    else
                    {
                        result = StringCalc("DIV", _Grid.Rows[rowIndex - 1 * compareCount].Items[columnCount - 1].Text.Trim() + ".0", _Grid.Rows[rowIndex - 2 * compareCount].Items[columnCount - 1].Text);
                        row.Items[columnCount - 1].Text = FormatText(result, format);
                        dataRow[columnCount] = FormatText(result, format);
                    }
                    break;

                case "DIVS":
                    result = StringCalc("SUB", _Grid.Rows[rowIndex - 2 * compareCount].Items[columnCount - 1].Text.Trim() + ".0", _Grid.Rows[rowIndex - 1 * compareCount].Items[columnCount - 1].Text.Trim() + ".0");
                    if (float.Parse(result) < 0)
                    {
                        result = "0";
                    }
                    else
                    {
                        result = StringCalc("DIV", result, _Grid.Rows[rowIndex - 2 * compareCount].Items[columnCount - 1].Text.Trim() + ".0", "1");
                    }
                    row.Items[columnCount - 1].Text = FormatText(result, format);
                    dataRow[columnCount] = FormatText(result, format);
                    break;

                case "PPM":
                    result = StringCalc("DIV", _Grid.Rows[rowIndex - 1 * compareCount].Items[columnCount - 1].Text.Trim() + ".0", _Grid.Rows[rowIndex - 2 * compareCount].Items[columnCount - 1].Text);
                    result = StringCalc("MUL", result, "1000000");
                    row.Items[columnCount - 1].Text = FormatText(result, format);
                    dataRow[columnCount] = FormatText(result, format);
                    break;

                case "AVGT":
                    for (int i = firstColumnIndex; i < lastColumnIndex + 1; i++)
                    {
                        totalCount = StringCalc("ADD", totalCount, _Grid.Rows[rowIndex - 1].Items[i].Text);
                        totalQty = StringCalc("ADD", totalQty, StringCalc("MUL", _Grid.Rows[rowIndex].Items[i].Text, _Grid.Rows[rowIndex - 1].Items[i].Text));
                    }
                    result = StringCalc("DIV", totalQty, totalCount);
                    row.Items[columnCount - 1].Text = FormatText(result, format);
                    dataRow[columnCount] = FormatText(result, format);
                    break;

                case "AVG":
                    for (int i = firstColumnIndex; i < lastColumnIndex + 1; i++)
                    {
                        totalCount = StringCalc("ADD", totalCount, "1");
                        totalQty = StringCalc("ADD", totalQty, StringCalc("MUL", _Grid.Rows[rowIndex].Items[i].Text, _Grid.Rows[rowIndex - 1].Items[i].Text));
                    }
                    result = StringCalc("DIV", totalQty, totalCount);
                    row.Items[columnCount - 1].Text = FormatText(result, format);
                    dataRow[columnCount] = FormatText(result, format);
                    break;
                case "IQCPPM":
                    result = StringCalc("DIV", _Grid.Rows[rowIndex - 3 * compareCount].Items[columnCount - 1].Text, _Grid.Rows[rowIndex - 1 * compareCount].Items[columnCount - 1].Text.Trim() + ".0");
                    result = StringCalc("MUL", result, "1000000");
                    row.Items[columnCount - 1].Text = FormatText(result, format);
                    dataRow[columnCount] = FormatText(result, format);
                    break;
                default:
                    break;
            }

        }

        private void SetBottomRowText(int columnIndex, int firstRowIndex, int groupCount, string formular, string format)
        {
            string fomularOP = string.Empty;
            List<int> paramRefList = new List<int>();
            ParseFomular(formular, out fomularOP, out paramRefList);

            string result = "0";
            string totalCount = "0";
            string totalQty = "0";

            int compareCount = (_CompareType.Trim().Length <= 0 ? 1 : 2);
            int bottomRowIndex = _Grid.Rows.Count - 1;

            switch (fomularOP.ToUpper())
            {
                case "SUM":
                    for (int i = firstRowIndex; i < _Grid.Rows.Count - 1; i += groupCount)
                    {
                        result = StringCalc("ADD", result.ToString(), _Grid.Rows[i].Items[columnIndex].Text);
                    }
                    _Grid.Rows[_Grid.Rows.Count - 1].Items[columnIndex].Text = FormatText(result, format);
                    dtSource.Rows[_Grid.Rows.Count - 1][columnIndex + 1] = FormatText(result, format);
                    break;

                case "MAX":
                    for (int i = firstRowIndex; i < _Grid.Rows.Count - 1; i += groupCount)
                    {
                        result = StringCalc("MAX", result.ToString(), _Grid.Rows[i].Items[columnIndex].Text);
                    }
                    _Grid.Rows[_Grid.Rows.Count - 1].Items[columnIndex].Text = FormatText(result, format);
                    dtSource.Rows[_Grid.Rows.Count - 1][columnIndex + 1] = FormatText(result, format);
                    break;

                case "DIV":
                    if (paramRefList.Count > 0)
                    {
                        result = StringCalc("DIV", _Grid.Rows[bottomRowIndex + paramRefList[0] * compareCount].Items[columnIndex].Text.Trim() + ".0", _Grid.Rows[bottomRowIndex + paramRefList[1] * compareCount].Items[columnIndex].Text);
                        _Grid.Rows[bottomRowIndex].Items[columnIndex].Text = FormatText(result, format);
                        dtSource.Rows[_Grid.Rows.Count - 1][columnIndex + 1] = FormatText(result, format);
                    }
                    else
                    {

                        result = StringCalc("DIV", _Grid.Rows[bottomRowIndex - 1 * compareCount].Items[columnIndex].Text.Trim() + ".0", _Grid.Rows[bottomRowIndex - 2 * compareCount].Items[columnIndex].Text);
                        _Grid.Rows[bottomRowIndex].Items[columnIndex].Text = FormatText(result, format);
                        dtSource.Rows[_Grid.Rows.Count - 1][columnIndex + 1] = FormatText(result, format);
                    }
                    break;

                case "DIVS":
                    result = StringCalc("SUB", _Grid.Rows[bottomRowIndex - 2 * compareCount].Items[columnIndex].Text.Trim() + ".0", _Grid.Rows[bottomRowIndex - 1 * compareCount].Items[columnIndex].Text.Trim() + ".0");
                    if (float.Parse(result) < 0)
                    {
                        result = "0";
                    }
                    else
                    {
                        result = StringCalc("DIV", result, _Grid.Rows[bottomRowIndex - 2 * compareCount].Items[columnIndex].Text.Trim() + ".0", "1");
                    }

                    _Grid.Rows[bottomRowIndex].Items[columnIndex].Text = FormatText(result, format);
                    dtSource.Rows[_Grid.Rows.Count - 1][columnIndex + 1] = FormatText(result, format);
                    break;

                case "PPM":
                    result = StringCalc("DIV", _Grid.Rows[bottomRowIndex - 1 * compareCount].Items[columnIndex].Text.Trim() + ".0", _Grid.Rows[bottomRowIndex - 2 * compareCount].Items[columnIndex].Text);
                    result = StringCalc("MUL", result, "1000000");
                    _Grid.Rows[bottomRowIndex].Items[columnIndex].Text = FormatText(result, format);
                    dtSource.Rows[_Grid.Rows.Count - 1][columnIndex + 1] = FormatText(result, format);
                    break;

                case "AVGT":
                    for (int i = firstRowIndex; i < _Grid.Rows.Count - 1; i += groupCount)
                    {
                        totalCount = StringCalc("ADD", totalCount, _Grid.Rows[i - compareCount].Items[columnIndex].Text);
                        totalQty = StringCalc("ADD", totalQty, StringCalc("MUL", _Grid.Rows[i].Items[columnIndex].Text, _Grid.Rows[i - compareCount].Items[columnIndex].Text));
                    }
                    result = StringCalc("DIV", totalQty, totalCount);
                    _Grid.Rows[_Grid.Rows.Count - 1].Items[columnIndex].Text = FormatText(result, format);
                    dtSource.Rows[_Grid.Rows.Count - 1][columnIndex + 1] = FormatText(result, format);
                    break;

                case "AVG":
                    for (int i = firstRowIndex; i < _Grid.Rows.Count - 1; i += groupCount)
                    {
                        totalCount = StringCalc("ADD", totalCount, "1");
                        totalQty = StringCalc("ADD", totalQty, StringCalc("MUL", _Grid.Rows[i].Items[columnIndex].Text, _Grid.Rows[i - compareCount].Items[columnIndex].Text));
                    }
                    result = StringCalc("DIV", totalQty, totalCount);
                    _Grid.Rows[_Grid.Rows.Count - 1].Items[columnIndex].Text = FormatText(result, format);
                    dtSource.Rows[_Grid.Rows.Count - 1][columnIndex + 1] = FormatText(result, format);
                    break;
                case "IQCPPM":
                    result = StringCalc("DIV", _Grid.Rows[bottomRowIndex - 3 * compareCount].Items[columnIndex].Text.Trim() + ".0", _Grid.Rows[bottomRowIndex - 1 * compareCount].Items[columnIndex].Text);
                    result = StringCalc("MUL", result, "1000000");
                    _Grid.Rows[bottomRowIndex].Items[columnIndex].Text = FormatText(result, format);
                    dtSource.Rows[_Grid.Rows.Count - 1][columnIndex + 1] = FormatText(result, format);
                    break;
                default:
                    break;
            }

        }

        private void SetGridStyle()
        {
            GridHelperNew gridHelper = new GridHelperNew(_Grid, dtSource);
            gridHelper.ApplyDefaultStyle();

            int compareCount = (_CompareType.Trim().Length <= 0 ? 1 : 2);
            int rowGroupCount = _Dim3PropertyList.Count * compareCount;

            int valueNameColumnCount = 0;
            if (_HasDim3PropertyNameRowColumn)
            {
                valueNameColumnCount = 1;
            }

            //列标题
            ShiftModel shiftModel = new ShiftModel(_DataProvider);
            if (_ByTimeType == NewReportByTimeType.Period)
            {
                TimePeriod timePeriod = null;
                for (int i = _Dim1PropertyList.Count + valueNameColumnCount; i < _Grid.Columns.Count; i++)
                {
                    timePeriod = (TimePeriod)shiftModel.GetTimePeriod(_Grid.Columns[i].Header.Text);
                    if (timePeriod != null)
                        _Grid.Columns[i].Header.Text = timePeriod.TimePeriodDescription;
                }
            }
            else if (_ByTimeType == NewReportByTimeType.Shift)
            {
                Shift shift = null;
                for (int i = _Dim1PropertyList.Count + valueNameColumnCount; i < _Grid.Columns.Count; i++)
                {
                    shift = (Shift)shiftModel.GetShift(_Grid.Columns[i].Header.Text);
                    if (shift != null)
                        _Grid.Columns[i].Header.Text = shift.ShiftDescription;
                }
            }

            //数字右对齐
            for (int i = 0; i < _Grid.Rows.Count; i++)
            {
                for (int j = _Dim1PropertyList.Count + valueNameColumnCount; j < _Grid.Columns.Count; j++)
                {
                    _Grid.Rows[i].Items[j].CssClass = HorizontalAlign.Right.ToString();

                    //_Grid.Rows[i].Items[j].Style.HorizontalAlign = HorizontalAlign.Right;

                }
            }

            //投入产出列背景色
            if (_HasDim3PropertyNameRowColumn)
            {
                for (int i = 0; i < _Grid.Rows.Count; i++)
                {
                    //_Grid.Rows[i].Items[_Dim1PropertyList.Count].Style.BackColor = _Grid.DisplayLayout.HeaderStyleDefault.BackColor;
                    //to do
                }
            }

            //最下方汇总行背景色
            if (_Dim3PropertyList.Count > 0)
            {
                bool found = false;
                foreach (ReportGridDim3Property property in _Dim3PropertyList)
                {
                    if (property.BottemRowFomular.Trim().Length > 0)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    for (int i = _Grid.Rows.Count - rowGroupCount; i < _Grid.Rows.Count; i++)
                    {
                        for (int j = 0; j < _Grid.Columns.Count; j++)
                        {
                            //_Grid.Rows[i].Items[j].Style.BackColor = _Grid.DisplayLayout.HeaderStyleDefault.BackColor;
                            //to do
                        }
                    }
                }
            }

            //隐藏行
            if (_Dim3PropertyList.Count > 0)
            {
                for (int i = 0; i < rowGroupCount; i++)
                {
                    for (int j = 0; j < _Grid.Rows.Count; j++)
                    {
                        if (j % rowGroupCount == i && _Dim3PropertyList[i / compareCount].Hidden)
                        {
                            //_Grid.Rows[j].Hidden = true;
                            //to do
                        }
                    }
                }
            }

            //合并单元格
            if (_Dim3PropertyList.Count > 0)
            {
                int visibleRowGroupCount = rowGroupCount;
                int firstVisibleRowIndex = -1;
                for (int i = 0; i < _Dim3PropertyList.Count; i++)
                {
                    if (_Dim3PropertyList[i].Hidden)
                    {
                        visibleRowGroupCount -= compareCount;
                    }
                    else if (firstVisibleRowIndex < 0)
                    {
                        firstVisibleRowIndex = i * compareCount;
                    }
                }

                for (int i = firstVisibleRowIndex; i < _Grid.Rows.Count; i += rowGroupCount)
                {
                    for (int j = 0; j < _Dim1PropertyList.Count; j++)
                    {

                        // GridViewRow objgridviewrow = new GridViewRow
                        //         (1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                        // TableCell objtablecell = (TableCell)_Grid.Rows[i].Items[j];
                        // objtablecell.RowSpan = visibleRowGroupCount;

                        // _Grid.Rows[i].Cells[j].RowSpan = visibleRowGroupCount;
                        //to do
                    }
                }
            }
        }

        public void ShowGrid()
        {
            InitGrid();
            InsertGrid();
            UpdateGridForCompare();
            SetSummaryRowColumn();
            SetGridStyle();
        }

        public void ShowGridWithFixedOPHead()
        {
            InitGridFixOPHead();
            InsertGridOnFixedOPHead();
        }

        public void ShowNormalGrid()
        {
            InitNormalGrid();
            InsertNormalGrid();
        }

        public void AddPercentByRow(bool addTotalPercent, string format)
        {
            int valueNameColumnCount = 0;
            if (_HasDim3PropertyNameRowColumn)
            {
                valueNameColumnCount = 1;
            }

            int totalColumnCount = 0;
            if (!addTotalPercent)
            {
                totalColumnCount = 1;
            }

            for (int i = 0; i < _Grid.Rows.Count; i++)
            {
                double total = double.Parse(_Grid.Rows[i].Items[_Grid.Columns.Count - 1].Text);
                for (int j = _Dim1PropertyList.Count + valueNameColumnCount; j < _Grid.Columns.Count - totalColumnCount; j++)
                {
                    double qty = double.Parse(_Grid.Rows[i].Items[j].Text);
                    double percent = qty / total;
                    _Grid.Rows[i].Items[j].Text += " - " + percent.ToString(format);
                    //_Grid.Rows[i].Items[j].Style.HorizontalAlign = HorizontalAlign.Center;
                    //to do
                }
            }
        }

        private int GetColumnNumber(NewReportDomainObject domainObject, int fixedCount)
        {
            int returnNumber = 0;

            for (int i = 0; i < _FixedHeadDefaultValueList.Count; i++)
            {
                int lastNumber = domainObject.OPCode.LastIndexOf("-");
                string compareOP = domainObject.OPCode.Substring(0, lastNumber);
                if (_FixedHeadDefaultValueList[i].ToString() == compareOP)
                {
                    returnNumber = i;
                    break;
                }
            }
            returnNumber = returnNumber + fixedCount;
            return returnNumber;
        }

        private string stringFormat(float inputNumber)
        {

            string returnString = string.Empty;


            if (inputNumber <= 0)
            {
                returnString = "0.00%";
            }

            if (inputNumber >= 1)
            {
                returnString = "100.00%";
            }

            if (inputNumber > 0 && inputNumber < 1)
            {
                returnString = inputNumber.ToString("0.00%");
            }

            return returnString;
        }

        private string GetCuerrtColunmValue(NewReportDomainObject domainObject, string ColunmName)
        {
            string returnValue = string.Empty;
            if (ColunmName.ToUpper() == "WEEK")
            {
                returnValue = domainObject.Week;
            }

            if (ColunmName.ToUpper() == "SHIFTDAY")
            {
                returnValue = domainObject.ShiftDay;
            }

            if (ColunmName.ToUpper() == "MONTH")
            {
                returnValue = domainObject.Month;
            }

            if (ColunmName.ToUpper() == "YEAR")
            {
                returnValue = domainObject.Year;
            }

            if (ColunmName.ToUpper() == "FIRSTCLASS")
            {
                returnValue = domainObject.FirstClass;
            }

            if (ColunmName.ToUpper() == "SECONDCLASS")
            {
                returnValue = domainObject.SecondClass;
            }

            if (ColunmName.ToUpper() == "QUALIFIEDQTY")
            {
                returnValue = domainObject.QualifiedQty.ToString();
            }

            if (ColunmName.ToUpper() == "UNQUALIFIEDQTY")
            {
                returnValue = domainObject.UNQualifiedQty.ToString();
            }

            if (ColunmName.ToUpper() == "ALLQTY")
            {
                returnValue = domainObject.AllQty.ToString();
            }

            if (ColunmName.ToUpper() == "INSPECTORANDNAME")
            {
                returnValue = domainObject.InspectorAndName;
            }

            if (ColunmName.ToUpper() == "RPTGOODQTY")
            {
                returnValue = domainObject.RptGoodQty.ToString();
            }

            if (ColunmName.ToUpper() == "RPTNGQTY")
            {
                returnValue = domainObject.RptNGQty.ToString();
            }

            if (ColunmName.ToUpper() == "ALLIQCQTY")
            {
                returnValue = domainObject.AllIQCQty.ToString();
            }

            if (ColunmName.ToUpper() == "ITEMCODE")
            {
                returnValue = domainObject.ItemCode;
            }

            if (ColunmName.ToUpper() == "BIGSSCODE")
            {
                returnValue = domainObject.BigSSCode;
            }

            if (ColunmName.ToUpper() == "MATERIALMODELCODE")
            {
                returnValue = domainObject.MaterialModelCode;
            }

            return returnValue;

        }
        #endregion
    }
}
