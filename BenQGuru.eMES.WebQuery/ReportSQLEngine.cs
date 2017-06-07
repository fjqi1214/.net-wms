using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Web.Helper;

using ControlLibrary.Web.Language;

namespace BenQGuru.eMES.WebQuery
{
    public class ReportSQLEngine
    {
        #region 初始化

        private IDomainDataProvider _DomainDataProvider = null;
        private LanguageComponent _LanguageComponent = null;

        private List<TableRelation> _TableRelationList = null;

        public ReportSQLEngine(IDomainDataProvider domainDataProvider, LanguageComponent languageComponent)
        {
            _DomainDataProvider = domainDataProvider;
            _LanguageComponent = languageComponent;

            //if (_DomainDataProvider == null)
            //{
            //    _DomainDataProvider = DomainDataProviderManager.DomainDataProvider();
            //}            
        }

        private void InitTableRelation()
        {
            _TableRelationList = new List<TableRelation>();

            TableRelation relation = null;

            //tblmaterial
            relation = new TableRelation();
            relation.Table = "tblmaterial";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("mcode", "**", "**", "itemcode"));
            _TableRelationList.Add(relation);

            //tblmesentitylist
            relation = new TableRelation();
            relation.Table = "tblmesentitylist";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("serial", "**", "**", "tblmesentitylist_serial"));
            _TableRelationList.Add(relation);

            //tbltimedimension
            relation = new TableRelation();
            relation.Table = "tbltimedimension";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("ddate", "**", "**", "shiftday"));
            _TableRelationList.Add(relation);

            //tblmo
            relation = new TableRelation();
            relation.Table = "tblmo";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("mocode", "**", "**", "mocode"));
            _TableRelationList.Add(relation);

            //tbllot
            relation = new TableRelation();
            relation.Table = "tbllot";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("lotno", "**", "**", "lotno"));
            _TableRelationList.Add(relation);

            //tblshift 1
            relation = new TableRelation();
            relation.Table = "tblshift";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("shiftcode", "**", "**", "shiftcode"));
            _TableRelationList.Add(relation);

            //tblshift 2
            relation = new TableRelation();
            relation.Table = "tblshift";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("shiftcode", "tblmesentitylist", "tblmesentitylist", "shiftcode"));
            _TableRelationList.Add(relation);

            //tbltp 1
            relation = new TableRelation();
            relation.Table = "tbltp";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("tpcode", "**", "**", "tpcode"));
            _TableRelationList.Add(relation);

            //tbltp 2
            relation = new TableRelation();
            relation.Table = "tbltp";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("tpcode", "tblmesentitylist", "tblmesentitylist", "tpcode"));
            _TableRelationList.Add(relation);

            //tblseg 1
            relation = new TableRelation();
            relation.Table = "tblseg";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("segcode", "**", "**", "segcode"));
            _TableRelationList.Add(relation);

            //tblseg 2
            relation = new TableRelation();
            relation.Table = "tblseg";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("segcode", "tblmesentitylist", "tblmesentitylist", "segcode"));
            _TableRelationList.Add(relation);

            //tblss 1
            relation = new TableRelation();
            relation.Table = "tblss";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("sscode", "**", "**", "sscode"));
            _TableRelationList.Add(relation);

            //tblss 2
            relation = new TableRelation();
            relation.Table = "tblss";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("sscode", "tblmesentitylist", "tblmesentitylist", "sscode"));
            _TableRelationList.Add(relation);

            //tblop 1
            relation = new TableRelation();
            relation.Table = "tblop";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("opcode", "**", "**", "opcode"));
            _TableRelationList.Add(relation);

            //tblop 2
            relation = new TableRelation();
            relation.Table = "tblop";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("opcode", "tblmesentitylist", "tblmesentitylist", "opcode"));
            _TableRelationList.Add(relation);

            //tblres 1
            relation = new TableRelation();
            relation.Table = "tblres";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("rescode", "**", "**", "rescode"));
            _TableRelationList.Add(relation);

            //tblres 2
            relation = new TableRelation();
            relation.Table = "tblres";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("rescode", "tblmesentitylist", "tblmesentitylist", "rescode"));
            _TableRelationList.Add(relation);

            //tblline2crew 1
            relation = new TableRelation();
            relation.Table = "tblline2crew";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("shiftdate", "**", "**", "shiftday"));
            relation.TableLinkList.Add(new TableLink("sscode", "**", "**", "sscode"));
            relation.TableLinkList.Add(new TableLink("shiftcode", "**", "**", "shiftcode"));
            _TableRelationList.Add(relation);

            //tblline2crew 2
            relation = new TableRelation();
            relation.Table = "tblline2crew";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("sscode", "tblmesentitylist", "tblmesentitylist", "sscode"));
            relation.TableLinkList.Add(new TableLink("shiftcode", "tblmesentitylist", "tblmesentitylist", "shiftcode"));
            relation.TableLinkList.Add(new TableLink("shiftdate", "**", "**", "shiftday"));
            _TableRelationList.Add(relation);

            //tblcrew 1 
            relation = new TableRelation();
            relation.Table = "tblcrew";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("crewcode", "**", "**", "crewcode"));
            _TableRelationList.Add(relation);

            //tblcrew 2
            relation = new TableRelation();
            relation.Table = "tblcrew";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("crewcode", "tblline2crew", "tblline2crew", "crewcode"));
            _TableRelationList.Add(relation);

            //tblfactory 1 
            relation = new TableRelation();
            relation.Table = "tblfactory";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("faccode", "**", "**", "faccode"));
            _TableRelationList.Add(relation);

            //tblfactory 2
            relation = new TableRelation();
            relation.Table = "tblfactory";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("faccode", "tblmesentitylist", "tblmesentitylist", "faccode"));
            _TableRelationList.Add(relation);

            //tblitemclass 1
            relation = new TableRelation();
            relation.Table = "tblitemclass";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("firstclass", "**", "**", "firstclass"));
            _TableRelationList.Add(relation);

            //tblitemclass 2
            relation = new TableRelation();
            relation.Table = "tblitemclass";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("itemgroup", "tblmaterial", "tblmaterial", "mgroup"));
            _TableRelationList.Add(relation);

            //tblsysparam
            relation = new TableRelation();
            relation.Table = "tblsysparam";
            relation.TableAlias = "tblsysparam_momemo";
            relation.TableLinkList.Add(new TableLink("paramcode", "tblmo", "tblmo", "momemo"));
            relation.TableLinkList.Add(new TableLink("paramgroupcode", "tblmo", "tblmo", "'MO_PRODUCT_TYPE'"));
            _TableRelationList.Add(relation);

            //tbluser 2
            relation = new TableRelation();
            relation.Table = "tbluser";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("usercode", "**", "**", "INSPECTOR"));
            _TableRelationList.Add(relation);

            //tblvendor 1
            relation = new TableRelation();
            relation.Table = "tblvendor";
            relation.TableAlias = relation.Table;
            relation.TableLinkList.Add(new TableLink("vendorcode", "**", "**", "vendorcode"));
            _TableRelationList.Add(relation);
        }

        #endregion

        #region 用于查询数据的一些输入属性

        private string _CoreTableAlias = "ct";
        private string _DetailedCoreTable = string.Empty;
        private string _Formular = string.Empty;
        private string _WhereCondition = string.Empty;
        private string _HavingCondition = string.Empty;
        private string _GroupFieldsX = string.Empty;
        private string _GroupFieldsY = string.Empty;
        private string _OrderFields = string.Empty;

        public string CoreTableAlias
        {
            get { return _CoreTableAlias; }
            set { _CoreTableAlias = value; }
        }

        public string DetailedCoreTable
        {
            get { return _DetailedCoreTable; }
            set { _DetailedCoreTable = value; }
        }

        public string Formular
        {
            get { return _Formular; }
            set { _Formular = value; }
        }

        public string WhereCondition
        {
            get { return _WhereCondition; }
            set { _WhereCondition = value; }
        }

        public string HavingCondition
        {
            get { return _HavingCondition; }
            set { _HavingCondition = value; }
        }

        public string GroupFieldsX
        {
            get
            {
                return _GroupFieldsX;
            }
            set
            {
                //用于格式化周和月（几如果需要使用周和月，则年也是必须带出来的）
                _GroupFieldsX = AddYearField(value);
            }
        }

        public string GroupFieldsY
        {
            get { return _GroupFieldsY; }
            set { _GroupFieldsY = value; }
        }

        public string OrderFields
        {
            get { return _OrderFields; }
            set { _OrderFields = value; }
        }

        #endregion

        #region 用于获取SQL的一些辅助函数

        private string GetGroupFieldSQL()
        {
            string returnValue = "";

            string y = _GroupFieldsY.Trim();
            if (y.Length > 0)
            {
                if (returnValue.Trim().Length > 0)
                {
                    returnValue += ",";
                }
                returnValue += y;
            }

            string x = _GroupFieldsX.Trim();
            if (x.Length > 0)
            {
                if (returnValue.Trim().Length > 0)
                {
                    returnValue += ",";
                }
                returnValue += x;
            }

            return returnValue;
        }

        private string GetOrderFieldSQL()
        {
            string returnValue = "";

            if (_OrderFields.Trim().Length > 0)
            {
                returnValue = _OrderFields;
            }
            else
            {
                returnValue = GetGroupFieldSQL();
            }

            return returnValue;
        }

        private string GetSelectFieldSQL()
        {
            string returnValue = "";

            string group = GetGroupFieldSQL().Trim();
            if (group.Length > 0)
            {
                if (returnValue.Trim().Length > 0)
                {
                    returnValue += ",";
                }
                returnValue += group;
            }

            string form = _Formular.Trim();
            if (form.Length > 0)
            {
                if (returnValue.Trim().Length > 0)
                {
                    returnValue += ",";
                }
                returnValue += form;
            }

            return returnValue;
        }

        private string GetTableSQL()
        {
            InitTableRelation();

            string coreTableTemplate = "**";

            string returnValue = coreTableTemplate;
            string tableIncluded = "," + coreTableTemplate + ",";
            List<string> tableList = GetTables();

            foreach (string table in tableList)
            {
                LinkTableSQL(table, ref tableIncluded, ref returnValue);
            }

            if (_DetailedCoreTable.Trim().Length > 0 && returnValue.Trim().IndexOf(coreTableTemplate) == 0)
            {
                returnValue = " (" + _DetailedCoreTable + ") " + _CoreTableAlias + " " + returnValue.Substring(coreTableTemplate.Length);
            }

            return returnValue;
        }

        private bool TableIncluded(string newTable, string tableIncludedString)
        {
            tableIncludedString = "," + tableIncludedString.Trim().ToLower() + ",";
            if (tableIncludedString.IndexOf("," + newTable.Trim().ToLower() + ",") >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LinkTableSQL(string newTable, ref string tableIncluded, ref string tableSQL)
        {
            if (TableIncluded(newTable, tableIncluded))
            {
                return;
            }

            TableRelation relationUsed = null;

            //第一次扫描确定有没有可以直接使用的TableRelation
            foreach (TableRelation relation in _TableRelationList)
            {
                string tableAlias = relation.TableAlias.Trim().ToLower();

                if (tableAlias == newTable.Trim().ToLower())
                {
                    if (relationUsed == null)
                    {
                        bool canUse = true;
                        foreach (TableLink link in relation.TableLinkList)
                        {
                            if (_DetailedCoreTable.Trim().Length > 0 && link.ParentTable == "**" && _DetailedCoreTable.ToLower().IndexOf(link.ParentTableField.ToLower()) < 0)
                            {
                                canUse = false;
                                break;
                            }
                        }

                        if (canUse)
                        {
                            relationUsed = relation;
                        }
                    }

                    //如果所有的ParentTable中不包括**、并且都已经出现，则优先使用这个Relation
                    bool allParentTableIncluded = true;
                    foreach (TableLink link in relation.TableLinkList)
                    {
                        if (link.ParentTable == "**" || !TableIncluded(link.ParentTable, tableIncluded))
                        {
                            allParentTableIncluded = false;
                            break;
                        }
                    }

                    if (allParentTableIncluded)
                    {
                        relationUsed = relation;
                        break;
                    }
                }
            }

            //使用第一个可用的TableRelation
            if (relationUsed != null)
            {
                string table = relationUsed.Table.Trim().ToLower();
                string tableAlias = relationUsed.TableAlias.Trim().ToLower();

                for (int i = 0; i < relationUsed.TableLinkList.Count; i++)
                {
                    string parentTableAlias = relationUsed.TableLinkList[i].ParentTableAlias.Trim().ToLower();
                    if (!TableIncluded(relationUsed.TableLinkList[i].ParentTableAlias, tableIncluded))
                    {
                        LinkTableSQL(parentTableAlias, ref tableIncluded, ref tableSQL);
                    }

                    if (i == 0)
                    {
                        tableSQL += " LEFT OUTER JOIN " + table + " " + tableAlias + " ";
                    }

                    if (i == 0)
                    {
                        tableSQL += "ON ";
                    }
                    else
                    {
                        tableSQL += "AND ";
                    }

                    tableSQL += GetPrefixAndField(tableAlias, relationUsed.TableLinkList[i].TableField) + " = " + GetPrefixAndField(parentTableAlias, relationUsed.TableLinkList[i].ParentTableField) + " ";

                }

                tableIncluded += tableAlias + ",";
                return;

            }
        }

        private List<string> GetTables()
        {
            List<string> returnValue = new List<string>();

            returnValue.AddRange(ExtractTables(_GroupFieldsX));
            returnValue.AddRange(ExtractTables(_GroupFieldsY));
            returnValue.AddRange(ExtractTables(_Formular));
            returnValue.AddRange(ExtractTables(_WhereCondition));

            return returnValue;
        }

        private List<string> ExtractTables(string inputString)
        {
            string tableLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_0123456789*";
            List<string> returnValue = new List<string>();

            try
            {
                inputString = inputString.Replace("''", " ");

                string clearString = string.Empty;
                bool instr = false;

                while (inputString.IndexOf("'") >= 0)
                {
                    if (instr)
                    {
                        inputString = inputString.Substring(inputString.IndexOf("'") + 1);
                        instr = false;
                    }
                    else
                    {
                        clearString += inputString.Substring(0, inputString.IndexOf("'")) + " ";
                        inputString = inputString.Substring(inputString.IndexOf("'") + 1);
                        instr = true;
                    }
                }

                if (inputString.Length > 0)
                {
                    clearString += inputString;
                }

                string tableName = string.Empty;
                while (clearString.IndexOf(".") >= 0)
                {
                    tableName = clearString.Substring(0, clearString.IndexOf("."));
                    clearString = clearString.Substring(clearString.IndexOf(".") + 1);

                    int pos = -1;
                    for (int i = tableName.Length - 1; i >= 0; i--)
                    {
                        if (tableLetters.IndexOf(tableName[i]) < 0)
                        {
                            pos = i;
                            break;
                        }
                    }
                    if (pos >= 0)
                    {
                        returnValue.Add(tableName.Substring(pos + 1).Trim().ToLower());
                    }
                    else
                    {
                        returnValue.Add(tableName.Trim().ToLower());
                    }
                }
            }
            catch
            {
            }

            return returnValue;
        }

        private Hashtable _LanguageTable = new Hashtable();

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

        private string DeleteAlias(string fieldList)
        {
            StringBuilder returnValue = new StringBuilder();
            string temp = fieldList;

            bool inConstant = false;
            bool inAlias = false;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp.Substring(i, 1) == "'")
                {
                    inConstant = !inConstant;
                }

                if (!inConstant)
                {
                    if (i <= temp.Length - 4 && temp.Substring(i, 4).ToUpper() == " AS ")
                    {
                        inAlias = true;
                    }

                    if (temp.Substring(i, 1) == ",")
                    {
                        inAlias = false;
                    }
                }

                if (inConstant || !inAlias)
                {
                    returnValue.Append(temp[i]);
                }
            }

            return returnValue.ToString();
        }

        private string AddYearField(string groupField)
        {
            string returnValue = string.Empty;

            string temp = " " + groupField.ToLower() + " ";
            bool needYear = false;

            if (temp.IndexOf("dweek") >= 0)
            {
                string part = temp.Substring(temp.IndexOf("dweek") - 1, 7);
                if ((part[0] == ' ' || part[0] == '.' || part[0] == ',')
                    && (part[6] == ' ' || part[6] == '.' || part[6] == ','))
                {
                    needYear = true;
                }
            }

            if (temp.IndexOf("dmonth") >= 0)
            {
                string part = temp.Substring(temp.IndexOf("dmonth") - 1, 8);
                if ((part[0] == ' ' || part[0] == '.' || part[0] == ',')
                    && (part[7] == ' ' || part[7] == '.' || part[7] == ','))
                {
                    needYear = true;
                }
            }

            if (needYear)
            {
                if (temp.IndexOf("tbltimedimension") >= 0)
                {
                    if (temp.IndexOf("tbltimedimension.year") < 0)
                    {
                        if (temp.Trim().Length > 0)
                        {
                            temp += ",";
                        }
                        temp += "tbltimedimension.year";
                    }
                }
                else if (temp.IndexOf("**") >= 0)
                {
                    if (temp.IndexOf("**.year") < 0)
                    {
                        if (temp.Trim().Length > 0)
                        {
                            temp += ",";
                        }
                        temp += "**.year";
                    }
                }
                else
                {
                    if (temp.IndexOf(" year") < 0)
                    {
                        if (temp.Trim().Length > 0)
                        {
                            temp += ",";
                        }
                        temp += "year";
                    }
                }
            }

            returnValue = temp;
            return returnValue;
        }

        private string GetPrefixAndField(string tableAlias, string field)
        {
            string returnValue = string.Empty;

            float f = 0;
            if (field.IndexOf("'") >= 0 || float.TryParse(field, out f))
            {
                tableAlias = string.Empty;
            }

            if (tableAlias.Trim().Length > 0)
            {
                returnValue = tableAlias.Trim() + ".";
            }

            returnValue += field.Trim();

            return returnValue;
        }

        #endregion

        public string GetReportSQL()
        {
            string returnValue = string.Empty;

            returnValue += "SELECT " + GetSelectFieldSQL() + " ";
            returnValue += "FROM " + GetTableSQL() + " ";
            returnValue += "WHERE " + (_WhereCondition.Trim().Length > 0 ? _WhereCondition : " 1 = 1 ") + " ";

            string groupSQL = GetGroupFieldSQL();
            if (groupSQL.Trim().Length > 0)
            {
                returnValue += "GROUP BY " + DeleteAlias(groupSQL) + " ";

                if (_HavingCondition.Trim().Length > 0)
                {
                    returnValue += "HAVING " + _HavingCondition + " ";
                }
            }

            //排序时，必须把非时间性字段放在前面，以便后面在页面中的行列转置
            string orderSQL = GetOrderFieldSQL();
            if (orderSQL.Trim().Length > 0)
            {
                returnValue += "ORDER BY " + DeleteAlias(orderSQL) + " ";
            }

            returnValue = returnValue.Replace("**", _CoreTableAlias);

            return returnValue;
        }

        public object[] GetReportData()
        {
            string sql = GetReportSQL();
            return _DomainDataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));
        }

        public object[] GetReportDataSource(string byTimeType, int dateAdjust)
        {
            object[] returnValue = null;

            ReportHelpher reportHelpher = new ReportHelpher(_DomainDataProvider);

            string sql = GetReportSQL();
            returnValue = _DomainDataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));

            if (returnValue != null)
            {
                foreach (NewReportDomainObject domainObject in returnValue)
                {
                    //修正环比/同期比的属性值，已经对domainObject.GridColumn赋值
                    if (dateAdjust != 0)
                    {
                        switch (byTimeType)
                        {
                            case NewReportByTimeType.Year:
                                reportHelpher.GetAdjustYear(-dateAdjust, ref domainObject.Year);
                                break;

                            case NewReportByTimeType.Month:
                                reportHelpher.GetAdjustMonth(-dateAdjust, ref domainObject.Year, ref domainObject.Month);
                                break;

                            case NewReportByTimeType.Week:
                                reportHelpher.GetAdjustWeek(-dateAdjust, ref domainObject.Year, ref domainObject.Week);
                                break;

                            case NewReportByTimeType.ShiftDay:
                                reportHelpher.GetAdjustDay(-dateAdjust, ref domainObject.ShiftDay);
                                break;

                            default:
                                break;
                        }
                    }

                    //ShiftDay格式
                    if (domainObject.ShiftDay != null)
                    {
                        string s = domainObject.ShiftDay.ToString().Trim();
                        domainObject.ShiftDay = string.Format("{0}/{1}/{2}", s.Substring(0, 4), s.Substring(4, 2), s.Substring(6, 2));
                    }

                    //Week格式
                    if (domainObject.Week != null && domainObject.Year != null)
                    {
                        domainObject.Week = domainObject.Year + "/" + domainObject.Week.PadLeft(2, '0');
                    }

                    //Month格式
                    if (domainObject.Month != null && domainObject.Year != null)
                    {
                        domainObject.Month = domainObject.Year + "/" + domainObject.Month.PadLeft(2, '0');
                    }

                    //成品半成品翻译
                    if (domainObject.GoodSemiGood != null)
                    {
                        domainObject.GoodSemiGood = Translate(domainObject.GoodSemiGood);
                    }

                    //新品量产品翻译
                    if (domainObject.NewMass != null)
                    {
                        domainObject.NewMass = Translate(domainObject.NewMass);
                    }

                    //内销出口翻译
                    if (domainObject.MaterialExportImport != null)
                    {
                        domainObject.MaterialExportImport = Translate(domainObject.MaterialExportImport);
                    }

                    //OQCLotType翻译
                    if (domainObject.OQCLotType != null)
                    {
                        domainObject.OQCLotType = Translate(domainObject.OQCLotType);
                    }

                    //ProductionType翻译
                    if (domainObject.ProductionType != null)
                    {
                        domainObject.ProductionType = Translate(domainObject.ProductionType);
                    }

                    //EAttribute1翻译
                    if (domainObject.EAttribute1 != null)
                    {
                        domainObject.EAttribute1 = Translate(domainObject.EAttribute1);
                    }

                    //IQCLINEITEMTYPE翻译
                    if (domainObject.IQCLineItemType != null)
                    {
                        domainObject.IQCLineItemType = Translate(domainObject.IQCLineItemType);
                    }

                    //IQCITEMTYPE翻译
                    if (domainObject.IQCItemType != null)
                    {
                        domainObject.IQCItemType = Translate(domainObject.IQCItemType);
                    }

                    //ROHS翻译
                    if (domainObject.Rohs != null)
                    {
                        domainObject.Rohs = Translate(domainObject.Rohs);
                    }

                    //CONCESSIONSTATUS翻译
                    if (domainObject.ConcessionStatus != null)
                    {
                        domainObject.ConcessionStatus = Translate(domainObject.ConcessionStatus);
                    }

                    //ExceptionCode翻译
                    //if (domainObject.ExceptionCode != null)
                    //{
                    //    domainObject.ExceptionCode = Translate(domainObject.ExceptionCode);
                    //}


                    switch (byTimeType)
                    {
                        case NewReportByTimeType.Year:
                            domainObject.TimeString = domainObject.Year;
                            break;

                        case NewReportByTimeType.Month:
                            domainObject.TimeString = domainObject.Month;
                            break;

                        case NewReportByTimeType.Week:
                            domainObject.TimeString = domainObject.Week;
                            break;

                        case NewReportByTimeType.ShiftDay:
                            domainObject.TimeString = domainObject.ShiftDay;
                            break;

                        case NewReportByTimeType.Shift:
                            domainObject.TimeString = domainObject.ShiftCode;
                            break;

                        case NewReportByTimeType.Period:
                            domainObject.TimeString = domainObject.PeriodCode;
                            break;

                        default:
                            break;
                    }
                }
            }

            return returnValue;
        }

        public object[] GetReportDataSource(string sql, string byTimeType, int dateAdjust)
        {
            object[] returnValue = null;

            ReportHelpher reportHelpher = new ReportHelpher(_DomainDataProvider);

            returnValue = _DomainDataProvider.CustomQuery(typeof(NewReportDomainObject), new SQLCondition(sql));

            if (returnValue != null)
            {
                foreach (NewReportDomainObject domainObject in returnValue)
                {
                    //修正环比/同期比的属性值，已经对domainObject.TimeString赋值
                    if (dateAdjust != 0)
                    {
                        switch (byTimeType)
                        {
                            case NewReportByTimeType.Year:
                                reportHelpher.GetAdjustYear(-dateAdjust, ref domainObject.Year);
                                break;

                            case NewReportByTimeType.Month:
                                reportHelpher.GetAdjustMonth(-dateAdjust, ref domainObject.Year, ref domainObject.Month);
                                break;

                            case NewReportByTimeType.Week:
                                reportHelpher.GetAdjustWeek(-dateAdjust, ref domainObject.Year, ref domainObject.Week);
                                break;

                            case NewReportByTimeType.ShiftDay:
                                reportHelpher.GetAdjustDay(-dateAdjust, ref domainObject.ShiftDay);
                                break;

                            default:
                                break;
                        }
                    }

                    //ShiftDay格式
                    if (domainObject.ShiftDay != null)
                    {
                        string s = domainObject.ShiftDay.ToString().Trim();
                        domainObject.ShiftDay = string.Format("{0}/{1}/{2}", s.Substring(0, 4), s.Substring(4, 2), s.Substring(6, 2));
                    }

                    //Week格式
                    if (domainObject.Week != null && domainObject.Year != null)
                    {
                        domainObject.Week = domainObject.Year + "/" + domainObject.Week.PadLeft(2, '0');
                    }

                    //Month格式
                    if (domainObject.Month != null && domainObject.Year != null)
                    {
                        domainObject.Month = domainObject.Year + "/" + domainObject.Month.PadLeft(2, '0');
                    }

                    //成品半成品翻译
                    if (domainObject.GoodSemiGood != null)
                    {
                        domainObject.GoodSemiGood = Translate(domainObject.GoodSemiGood);
                    }

                    //新品量产品翻译
                    if (domainObject.NewMass != null)
                    {
                        domainObject.NewMass = Translate(domainObject.NewMass);
                    }

                    //内销出口翻译
                    if (domainObject.MaterialExportImport != null)
                    {
                        domainObject.MaterialExportImport = Translate(domainObject.MaterialExportImport);
                    }

                    //OQCLotType翻译
                    if (domainObject.OQCLotType != null)
                    {
                        domainObject.OQCLotType = Translate(domainObject.OQCLotType);
                    }

                    //ProductionType翻译
                    if (domainObject.ProductionType != null)
                    {
                        domainObject.ProductionType = Translate(domainObject.ProductionType);
                    }

                    //EAttribute1翻译
                    if (domainObject.EAttribute1 != null)
                    {
                        domainObject.EAttribute1 = Translate(domainObject.EAttribute1);
                    }

                    switch (byTimeType)
                    {
                        case NewReportByTimeType.Year:
                            domainObject.TimeString = domainObject.Year;
                            break;

                        case NewReportByTimeType.Month:
                            domainObject.TimeString = domainObject.Month;
                            break;

                        case NewReportByTimeType.Week:
                            domainObject.TimeString = domainObject.Week;
                            break;

                        case NewReportByTimeType.ShiftDay:
                            domainObject.TimeString = domainObject.ShiftDay;
                            break;

                        case NewReportByTimeType.Shift:
                            domainObject.TimeString = domainObject.ShiftCode;
                            break;

                        case NewReportByTimeType.Period:
                            domainObject.TimeString = domainObject.PeriodCode;
                            break;

                        default:
                            break;
                    }
                }
            }

            return returnValue;
        }

        public static string GetDetailedCoreForRptSoQty()
        {
            string sql = "SELECT mocode, shiftday, itemcode, tblmesentitylist_serial, moinputcount, mooutputcount, molineoutputcount, mowhitecardcount, mooutputwhitecardcount, lineinputcount, lineoutputcount, opcount, opwhitecardcount, eattribute1 ";
            sql += "FROM tblrptsoqty";

            return sql;
        }

        public static string GetDetailedCoreForRptOpQty()
        {
            string sql = "SELECT mocode, shiftday, itemcode, tblmesentitylist_serial, inputtimes, outputtimes, ngtimes, eattribute1 ";
            sql += "FROM tblrptopqty";

            return sql;
        }

        public static string GetDetailedCoreForRptLineQty()
        {
            string sql = "SELECT mocode, shiftday, itemcode, tblmesentitylist_serial, linewhitecardcount, reswhitecardcount, eattribute1 ";
            sql += "FROM tblrptlineqty";

            return sql;
        }
    }

    public class TableRelation
    {
        private string _Table = string.Empty;
        private string _TableAlias = string.Empty;
        private List<TableLink> _TableLinkList = new List<TableLink>();

        public string Table
        {
            get { return _Table; }
            set { _Table = value; }
        }

        public string TableAlias
        {
            get { return _TableAlias; }
            set { _TableAlias = value; }
        }

        public List<TableLink> TableLinkList
        {
            get { return _TableLinkList; }
            set { _TableLinkList = value; }
        }
    }

    public class TableLink
    {
        private string _TableField = string.Empty;

        private string _ParentTable = string.Empty;
        private string _ParentTableAlias = string.Empty;
        private string _ParentTableField = string.Empty;

        public TableLink(string tableField, string parentTable, string parentTableAlias, string parentTableField)
        {
            _TableField = tableField;
            _ParentTable = parentTable;
            _ParentTableAlias = parentTableAlias;
            _ParentTableField = parentTableField;
        }

        public string TableField
        {
            get { return _TableField; }
            set { _TableField = value; }
        }

        public string ParentTable
        {
            get { return _ParentTable; }
            set { _ParentTable = value; }
        }

        public string ParentTableAlias
        {
            get { return _ParentTableAlias; }
            set { _ParentTableAlias = value; }
        }

        public string ParentTableField
        {
            get { return _ParentTableField; }
            set { _ParentTableField = value; }
        }
    }

}
