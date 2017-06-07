using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.ReportView;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.ReportViewBase;
using System.Reflection;

namespace BenQGuru.eMES.ReportView
{
	/// <summary>
	/// ReportViewFacade 的摘要说明。
	/// 文件名:		ReportViewFacade.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
	/// 创建日期:	2007-5-9 11:25:53
	/// 修改人:
	/// 修改日期:
	/// 描 述:	
	/// 版 本:	
	/// </summary>
	public partial class ReportViewFacade
	{

		#region RptViewChartCategory
        public RptViewChartCategory[] GetRptViewChartCategoriesByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVCHARTCATE WHERE RPTID='" + reportId + "' ORDER BY CHARTSEQ,CATESEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewChartCategory), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewChartCategory[] categories = new RptViewChartCategory[objs.Length];
            objs.CopyTo(categories, 0);
            return categories;
        }
        #endregion

        #region RptViewChartData
        public RptViewChartData[] GetRptViewChartDatasByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVCHARTDATA WHERE RPTID='" + reportId + "' ORDER BY CHARTSEQ,DATASEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewChartData), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewChartData[] datas = new RptViewChartData[objs.Length];
            objs.CopyTo(datas, 0);
            return datas;
        }
        #endregion

        #region RptViewChartMain
        public RptViewChartMain[] GetRptViewChartMainsByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVCHARTMAIN WHERE RPTID='" + reportId + "' ORDER BY CHARTSEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewChartMain), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewChartMain[] charts = new RptViewChartMain[objs.Length];
            objs.CopyTo(charts, 0);
            return charts;
        }
        #endregion

        #region RptViewChartSeries
        public RptViewChartSeries[] GetRptViewChartSeriesByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVCHARTSER WHERE RPTID='" + reportId + "' ORDER BY CHARTSEQ,SERSEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewChartSeries), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewChartSeries[] series = new RptViewChartSeries[objs.Length];
            objs.CopyTo(series, 0);
            return series;
        }
        #endregion

        #region RptViewDataConnect
        /// <summary>
        /// ** 功能描述:	分页查询RptViewDataConnect
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftCode">_name，模糊查询</param>
        /// <param name="shiftTypeCode">_type，模糊查询</param> 
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> RptViewDataConnect数组</returns>
        public object[] QueryDBServer(string _name, string _type, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(RptViewDataConnect), new PagerCondition(string.Format("select {0} from TBLRPTVCONNECT where 1=1 and UPPER(CONNECTNAME) like '{1}%' and UPPER(SERVICENAME) like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataConnect)), _name, _type), "DATACONNECTID", inclusive, exclusive));
        }
        /// <summary>
        /// ** 功能描述:	查询RptViewDataConnect的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftCode">_name，模糊查询</param>
        /// <param name="shiftTypeCode">_type，模糊查询</param> 
        /// <returns> Shift的总记录数</returns>
        public int QueryDataConnectCount(string _name, string _type)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVCONNECT where 1=1 and UPPER(CONNECTNAME) like '{0}%' and UPPER(SERVICENAME) like '{1}%' ", _name, _type)));
        }
        /// <summary>
        /// ** 功能描述:	查询RptViewDataConnect的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>       
        /// <returns> Shift的总记录数</returns>
        public int GetRptViewDataConnectNextId()
        {
            int i = 1;
            string strSql = "select max(dataconnectid) dataconnectid from TBLRPTVCONNECT";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewDataConnect), new SQLCondition(strSql));
            if (objs != null && objs.Length > 0)
            {
                i = Convert.ToInt32(((RptViewDataConnect)objs[0]).DataConnectID) + 1;
            }
            return i;
        }
        #endregion

        #region RptViewDataFormat
        public RptViewDataFormat[] GetRptViewDataFormatsByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVDATAFMT WHERE FORMATID IN (SELECT FORMATID FROM TBLRPTVGRIDDATAFMT WHERE RPTID='" + reportId + "') OR FORMATID IN (SELECT FORMATID FROM TBLRPTVEXTTEXT WHERE RPTID='" + reportId + "') ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewDataFormat), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewDataFormat[] formats = new RptViewDataFormat[objs.Length];
            objs.CopyTo(formats, 0);
            return formats;
        }
        
        public RptViewDataFormat[] GetRptViewDataFormatChartByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVDATAFMT WHERE FORMATID IN (SELECT LABELFORMATID FROM TBLRPTVCHARTMAIN WHERE RPTID='" + reportId + "')";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewDataFormat), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewDataFormat[] formats = new RptViewDataFormat[objs.Length];
            objs.CopyTo(formats, 0);
            return formats;
        }

        public string BuildStyleValueFromDataFormat(RptViewDataFormat dataFormat)
        {
            string strVal = "";
            strVal += dataFormat.FontFamily + ";";
            strVal += dataFormat.FontSize.ToString() + ";";
            strVal += (dataFormat.FontWeight == "Bold" ? "true" : "false") + ";";
            strVal += (dataFormat.FontStyle == "Italic" ? "true" : "false") + ";";
            strVal += (dataFormat.TextDecoration == "Underline" ? "true" : "false") + ";";
            strVal += dataFormat.Color + ";";
            strVal += dataFormat.BackgroundColor + ";";
            if (dataFormat.TextAlign == "Center")
                strVal += BenQGuru.eMES.Web.Helper.TextAlign.Center + ";";
            else if (dataFormat.TextAlign == "Right")
                strVal += BenQGuru.eMES.Web.Helper.TextAlign.Right + ";";
            else
                strVal += BenQGuru.eMES.Web.Helper.TextAlign.Left + ";";
            if (dataFormat.VerticalAlign == "Top")
                strVal += BenQGuru.eMES.Web.Helper.VerticalAlign.Top + ";";
            else if (dataFormat.VerticalAlign == "Bottom")
                strVal += BenQGuru.eMES.Web.Helper.VerticalAlign.Bottom + ";";
            else
                strVal += BenQGuru.eMES.Web.Helper.VerticalAlign.Middle + ";";
            strVal += dataFormat.ColumnWidth.ToString() + ";";
            strVal += (dataFormat.BorderStyle == "1" ? "true" : "false") + ";";
            strVal += dataFormat.TextFormat + ";";
            strVal += dataFormat.TextExpress;
            return strVal;
        }
        public RptViewDataFormat BuildDataFormatByStyle(string styleValue)
        {
            RptViewDataFormat dataFormat = new RptViewDataFormat();
            dataFormat.FontFamily = "Arial";
            dataFormat.FontSize = 12;
            dataFormat.FontWeight = "Normal";
            dataFormat.FontStyle = "Normal";
            dataFormat.TextDecoration = "None";
            dataFormat.Color = "Black";
            dataFormat.BackgroundColor = "White";
            dataFormat.TextAlign = "Left";
            dataFormat.VerticalAlign = "Middle";
            dataFormat.ColumnWidth = 2.5M;
            dataFormat.BorderStyle = "1";
            string[] styleList = styleValue.Split(';');
            if (styleList.Length >= 10)
            {
                dataFormat.FontFamily = styleList[0];   // Font Family
                if (styleList[1] != "")                 // Font Size
                    dataFormat.FontSize = decimal.Parse(styleList[1]);
                if (styleList[2] == "true")             // Font Weight
                    dataFormat.FontWeight = "Bold";
                if (styleList[3] == "true")             // Font Style
                    dataFormat.FontStyle = "Italic";
                if (styleList[4] == "true")             // Font Decoration
                    dataFormat.TextDecoration = "Underline";
                if (styleList[5] != "")                 // Fore Color
                    dataFormat.Color = styleList[5];
                if (styleList[6] != "")                 // Back Color
                    dataFormat.BackgroundColor = styleList[6];
                if (styleList[7] == BenQGuru.eMES.Web.Helper.TextAlign.Center)        // Text Align
                    dataFormat.TextAlign = "Center";
                else if (styleList[7] == BenQGuru.eMES.Web.Helper.TextAlign.Right)
                    dataFormat.TextAlign = "Right";
                else
                    dataFormat.TextAlign = "Left";
                if (styleList[8] == BenQGuru.eMES.Web.Helper.VerticalAlign.Top)     // Vertical Align
                    dataFormat.VerticalAlign = "Top";
                else if (styleList[8] == BenQGuru.eMES.Web.Helper.VerticalAlign.Bottom)
                    dataFormat.VerticalAlign = "Bottom";
                else
                    dataFormat.VerticalAlign = "Middle";
                if (styleList[9] != "")                                     // Column Width
                    dataFormat.ColumnWidth = decimal.Parse(styleList[9]);
                if (styleList[10] == "false")                               // Border Visible
                    dataFormat.BorderStyle = "0";
                dataFormat.TextFormat = styleList[11];   // Text Format
                dataFormat.TextExpress = styleList[12];     // Text Expression
            }
            return dataFormat;
        }
        #endregion

		#region RptViewDataSource

        /// <summary>
        /// 直接执行数据源，获取DataSet
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="webRootPath">Web根目录物理路径</param>
        /// <returns></returns>
        public DataSet ExecuteDataSetFromSource(RptViewDataSource dataSource, string webRootPath)
        {
            try
            {
                RptViewDataConnect dataConnect = (RptViewDataConnect)this.GetRptViewDataConnect(dataSource.DataConnectID);
                DataSet ds = null;
                if (dataSource.SourceType == DataSourceType.SQL)    // SQL语句
                {
                    ReportViewBase.DatasourceBase dataBase = new BenQGuru.eMES.ReportViewBase.DatasourceBase();
                    dataBase.DataConnect = dataConnect;
                    string strSql = dataSource.SQL;

                    #region 32位系统最大支持2G内存
                    //Added by leon yuan,20090702,限定记录的条数
                    CheckOverMaxRecordCount(strSql, dataBase);
                    #endregion

                    ds = dataBase.ExecuteSql(strSql);
                }
                else if (dataSource.SourceType == DataSourceType.DLL)
                {
                    string[] classFullName = dataSource.DllFileName.Split(',');
                    if (classFullName.Length < 2)
                        return null;

                    System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(webRootPath + "\\bin\\" + classFullName[1]);
                    object obj = assembly.CreateInstance(classFullName[0]);
                    ReportViewBase.DatasourceBase dataBase = (ReportViewBase.DatasourceBase)obj;

                    ArrayList listParam = new ArrayList();
                    RptViewDataSourceParam[] sourceParames = this.GetRptViewDataSourceParamByDataSourceId(Convert.ToInt32(dataSource.DataSourceID));
                    for (int i = 0; sourceParames != null && i < sourceParames.Length; i++)
                    {
                        listParam.Add(sourceParames[i].DefaultValue);
                    }
                    object[] objsParam = new object[listParam.Count];
                    listParam.CopyTo(objsParam);

                    dataBase.DataConnect = dataConnect;
                    ds = dataBase.Execute(objsParam);
                    obj = null;
                    dataBase = null;
                }
                return ds;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is System.OutOfMemoryException)
                { throw new Exception("$WINDOWS_32X_2GMemory_Limit"); }
                else
                { throw ex; }
            }
        }

        /// <summary>
        /// 执行某个报表的DataSet
        /// </summary>
        /// <param name="reportId">Report ID</param>
        /// <param name="userCode">用户代码</param>
        /// <param name="viewerInput">用户输入数据</param>
        /// <param name="webRootPath">Web根目录物理路径</param>
        /// <returns></returns>
        public DataSet ExecuteDataSetFromSource(string reportId, RptViewUserSubscription[] viewerInput, string webRootPath)
        {
            try
            {
                RptViewDesignMain designMain = (RptViewDesignMain)this.GetRptViewDesignMain(reportId);
                RptViewDataSource dataSource = (RptViewDataSource)this.GetRptViewDataSource(designMain.DataSourceID);
                RptViewDataConnect dataConnect = (RptViewDataConnect)this.GetRptViewDataConnect(dataSource.DataConnectID);
                DataSet ds = null;
                if (dataSource.SourceType == DataSourceType.SQL)    // SQL语句
                {
                    ReportViewBase.DatasourceBase dataBase = new BenQGuru.eMES.ReportViewBase.DatasourceBase();
                    dataBase.DataConnect = dataConnect;
                    string strSql = dataSource.SQL;
                    // 过滤的SQL语句条件
                    RptViewGridFilter[] filters = this.GetRptViewGridFiltersByReportId(reportId);
                    string strFilter = " WHERE 1=1 ";
                    bool bExistFilter = false;
                    RptViewDataSourceColumn[] columnList = null;
                    if (filters != null && filters.Length > 0)
                    {
                        columnList = this.GetRptViewDataSourceColumnByDataSourceId(Convert.ToInt32(dataSource.DataSourceID));
                    }
                    for (int i = 0; filters != null && i < filters.Length; i++)
                    {
                        if (filters[i].ColumnName != "")
                        {
                            for (int n = 0; viewerInput != null && n < viewerInput.Length; n++)
                            {
                                if (viewerInput[n].InputType == ReportViewerInputType.SqlFilter &&
                                    viewerInput[n].InputName == filters[i].ColumnName && 
                                    viewerInput[n].SqlFilterSequence == filters[i].FilterSequence)
                                {
                                    filters[i].DefaultValue = viewerInput[n].InputValue;
                                }
                            }
                            bExistFilter = true;
                            bool bIsString = true;
                            bool bDate = false;
                            bool bNum = false;
                            if (columnList != null)
                            {
                                for (int n = 0; n < columnList.Length; n++)
                                {
                                    if (columnList[n].ColumnName == filters[i].ColumnName)
                                    {
                                        if (columnList[n].DataType == "日期" ||
                                            columnList[n].DataType == "数值"
                                            ||
                                            columnList[n].DataType == ReportDataType.Date
                                            ||
                                            columnList[n].DataType == ReportDataType.Numeric)
                                        {
                                            bIsString = false;
                                        }

                                        if (columnList[n].DataType == ReportDataType.Date || columnList[n].DataType == "日期")
                                        {
                                            bDate = true;
                                        }
                                        else
                                        {
                                            bDate = false;
                                        }

                                        if (columnList[n].DataType == ReportDataType.Numeric || columnList[n].DataType == "数值")
                                        {
                                            bNum = true;
                                        }
                                        else
                                        {
                                            bNum = false;
                                        }
                                        break;
                                    }
                                }
                            }

                            if (bIsString && filters[i].DefaultValue != string.Empty && filters[i].DefaultValue != "*")
                            {
                                strFilter += " AND " + this.BuildSqlFilterOperation(filters[i].ColumnName, filters[i].DefaultValue, filters[i].FilterOperation, bIsString) + " ";
                            }
                            else if (bDate && filters[i].DefaultValue != "0" && filters[i].DefaultValue != string.Empty)
                            {
                                strFilter += " AND " + this.BuildSqlFilterOperation(filters[i].ColumnName, filters[i].DefaultValue, filters[i].FilterOperation, bIsString) + " ";
                            }
                            else if (bNum && filters[i].DefaultValue != string.Empty && filters[i].DefaultValue !="0")
                            {
                                strFilter += " AND " + this.BuildSqlFilterOperation(filters[i].ColumnName, filters[i].DefaultValue, filters[i].FilterOperation, bIsString) + " ";
                            }

                        }
                    }
                    if (bExistFilter == true)
                    {
                        strSql = "SELECT * FROM (" + dataSource.SQL + ") " + strFilter;
                    }

                    #region 32位系统最大支持2G内存
                    //Added by leon yuan,20090702,限定记录的条数
                    CheckOverMaxRecordCount(strSql, dataBase);
                    #endregion

                    ds = dataBase.ExecuteSql(strSql);
                }
                else if (dataSource.SourceType == DataSourceType.DLL)
                {
                    string[] classFullName = dataSource.DllFileName.Split(',');
                    if (classFullName.Length < 2)
                        return null;

                    System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(webRootPath + "\\bin\\" + classFullName[1]);
                    object obj = assembly.CreateInstance(classFullName[0]);
                    ReportViewBase.DatasourceBase dataBase = (ReportViewBase.DatasourceBase)obj;

                    ArrayList listParam = new ArrayList();
                    RptViewDataSourceParam[] sourceParames = this.GetRptViewDataSourceParamByDataSourceId(Convert.ToInt32(dataSource.DataSourceID));
                    for (int i = 0; sourceParames != null && i < sourceParames.Length; i++)
                    {
                        if (viewerInput != null)
                        {
                            for (int n = 0; n < viewerInput.Length; n++)
                            {
                                if (viewerInput[n].InputType == ReportViewerInputType.DllParameter &&
                                    viewerInput[n].InputName == sourceParames[i].ParameterName)
                                {
                                    sourceParames[i].DefaultValue = viewerInput[n].InputValue;
                                }
                            }
                        }
                        listParam.Add(sourceParames[i].DefaultValue);
                    }
                    object[] objsParam = new object[listParam.Count];
                    listParam.CopyTo(objsParam);

                    dataBase.DataConnect = dataConnect;
                    ds = dataBase.Execute(objsParam);
                    obj = null;
                    dataBase = null;
                }
                return ds;
            }
            catch (Exception ex)
            {
                if (ex.InnerException is System.OutOfMemoryException)
                { throw new Exception("$WINDOWS_32X_2GMemory_Limit"); }
                else
                { throw ex; }
            }
        }
        private string BuildSqlFilterOperation(string columnName, string columnValue, string operation, bool isString)
        {
            string strRet = "";
            string strQuote = "";
            if (isString == true)
                strQuote = "'";
            if (operation == ReportFilterType.Equal)
            {
                if (columnValue.IndexOf(',') > 0)
                {
                    strRet = string.Format("{0} IN ({1}{2}{1})", columnName, strQuote, columnValue.Replace(",","','"));
                }
                else
                {
                    strRet = string.Format("{0}={1}{2}{1}", columnName, strQuote, columnValue);
                }
            }
            else if (operation == ReportFilterType.Greater)
                strRet = string.Format("{0}>{1}{2}{1}", columnName, strQuote, columnValue);
            else if (operation == ReportFilterType.GreaterEqual)
                strRet = string.Format("{0}>={1}{2}{1}", columnName, strQuote, columnValue);
            else if (operation == ReportFilterType.Lesser)
                strRet = string.Format("{0}<{1}{2}{1}", columnName, strQuote, columnValue);
            else if (operation == ReportFilterType.LesserEqual)
                strRet = string.Format("{0}<={1}{2}{1}", columnName, strQuote, columnValue);
            else if (operation == ReportFilterType.LeftMatch)
                strRet = string.Format("{0} LIKE '{1}%'", columnName, columnValue);
            else if (operation == ReportFilterType.RightMatch)
                strRet = string.Format("{0} LIKE '%{1}'", columnName, columnValue);
            return strRet;
        }
        /// <summary>
        /// ** 功能描述:	查询RptViewDataConnect的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>       
        /// <returns> Shift的总记录数</returns>
        public int GetRptViewDataSourceNextId()
        {
            int i = 1;
            string strSql = "select max(datasourceid) datasourceid from TBLRPTVDATASRC";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewDataSource), new SQLCondition(strSql));
            if (objs != null && objs.Length > 0)
            {
                i = Convert.ToInt32(((RptViewDataSource)objs[0]).DataSourceID) + 1;
            }
            return i;
        }

        //32位系统最大支持2G内存
        //Added by leon yuan,20090702,限定记录的条数
        private void CheckOverMaxRecordCount(string strSql, DatasourceBase dataBase)
        {
            Int64 maxCount = 0;
            if (System.Configuration.ConfigurationSettings.AppSettings["MaxRecordCount"] != null)
            {
                maxCount = Convert.ToInt64(System.Configuration.ConfigurationSettings.AppSettings["MaxRecordCount"]);

                string sql1 = "select count(*) from (" + strSql + ")";
                DataSet ds1 = dataBase.ExecuteSql(sql1);
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt64(ds1.Tables[0].Rows[0].ItemArray[0].ToString()) > maxCount)
                    {
                        throw new Exception("$WINDOWS_32X_2GMemory_Limit");
                    }
                }
            }

        } 
 
        /// <summary>
        /// ** 功能描述:	分页查询RptViewDataConnect
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftCode">_name，模糊查询</param>
        /// <param name="shiftTypeCode">_type，模糊查询</param> 
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> RptViewDataConnect数组</returns>
        public object[] QueryDataSource(string _name, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(RptViewDataSource), new PagerCondition(string.Format(("Select TBLRPTVDATASRC.DATASOURCEID,TBLRPTVDATASRC.Name,TBLRPTVDATASRC.DESCRIPTION,TBLRPTVCONNECT.DATACONNECTID,TBLRPTVCONNECT.SERVICENAME,TBLRPTVDATASRC.SOURCETYPE,(TBLRPTVDATASRC.SQL||TBLRPTVDATASRC.Dllfilename) AS SQL  FROM TBLRPTVCONNECT,TBLRPTVDATASRC Where TBLRPTVDATASRC.DATACONNECTID=TBLRPTVCONNECT.DATACONNECTID AND UPPER(NAME) LIKE '{0}%'"), _name), inclusive, exclusive));
        }
        /// <summary>
        /// ** 功能描述:	查询RptViewDataConnect的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftCode">_name，模糊查询</param>
        /// <returns> 总记录数</returns>
        public int QueryDataSourceCount(string _name)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("Select COUNT(*) CNT  FROM TBLRPTVDATASRC Where DATACONNECTID in (select DATACONNECTID from TBLRPTVCONNECT) and UPPER(NAME) LIKE '{0}%'", _name)));
        }

        public void DeleteDataSourceWithColumnParam(RptViewDataSource[] dataSource)
        {
            if (dataSource == null || dataSource.Length == 0)
                return;
            this.DataProvider.BeginTransaction();
            try
            {
                for (int i = 0; i < dataSource.Length; i++)
                {
                    string strSql = "DELETE FROM tblRptVDataSrcColumn WHERE DataSourceID=" + dataSource[i].DataSourceID.ToString();
                    this.DataProvider.CustomExecute(new SQLCondition(strSql));
                    strSql = "DELETE FROM tblRptVDataSrcParam WHERE DataSourceID=" + dataSource[i].DataSourceID.ToString();
                    this.DataProvider.CustomExecute(new SQLCondition(strSql));
                    this.DeleteRptViewDataSource(dataSource[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            finally
            {
            }
        }

        /// <summary>
        /// 通过DATASOURCEID 来查询dataSouce
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object[] QueryDataSourceById(string dataSourceId)
        {
            return this.DataProvider.CustomQuery(typeof(RptViewDataSource), new SQLCondition(string.Format("SELECT {0} FROM TBLRPTVDATASRC WHERE DATASOURCEID='{1}' order by DATASOURCEID",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataSource)), dataSourceId)));
        }

        #endregion

		#region RptViewDataSourceColumn
        public RptViewDataSourceColumn[] GetRptViewDataSourceColumnByDataSourceId(int dataSourceId)
        {
            string strSql = "SELECT * FROM TBLRPTVDATASRCCOLUMN WHERE DATASOURCEID=" + dataSourceId + " ORDER BY COLUMNSEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewDataSourceColumn), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewDataSourceColumn[] columns = new RptViewDataSourceColumn[objs.Length];
            objs.CopyTo(columns, 0);
            return columns;
        }
        public int GetRptViewDataSourceColumnCountByDataSourceId(int dataSourceId)
        {
            string strSql = "SELECT Count(*) FROM TBLRPTVDATASRCCOLUMN WHERE DATASOURCEID=" + dataSourceId + " ORDER BY COLUMNSEQ";
            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }
        public object[] GetRptViewDataSourceColumn(int dataSourceId, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(RptViewDataSourceColumn), new PagerCondition(string.Format("select {0} from TBLRPTVDATASRCCOLUMN where 1=1 and DATASOURCEID like '{1}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataSourceColumn)), dataSourceId), "COLUMNSEQ", inclusive, exclusive));

        }
        /// <summary>
        /// 根据数据源得到数据列
        /// </summary>
        /// <param name="webRootPath">物理路径</param>
        /// <param name="FullName">数据源</param>
        /// <param name="_id">datasourceid</param>
        public void SetColumn(string webRootPath, string FullName, decimal _id, string userCode)
        {
            DataSet ds = new DataSet();
            
            RptViewDataSource dataSource = (RptViewDataSource)this.GetRptViewDataSource(_id);
            if (dataSource.SourceType == DataSourceType.DLL)
            {
                string[] classFullName = dataSource.DllFileName.Split(',');
                if (classFullName.Length < 2)
                    return;

                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(webRootPath + "\\bin\\" + classFullName[1]);
                object obj = assembly.CreateInstance(classFullName[0]);
                ReportViewBase.DatasourceBase dataBase = (ReportViewBase.DatasourceBase)obj;

                dataBase.DataConnect = (RptViewDataConnect)this.GetRptViewDataConnect(dataSource.DataConnectID);
                ds = dataBase.QuerySchema(null);
            }
            else
            {
                DatasourceBase dsb = new DatasourceBase();
                RptViewDataConnect rpt = new RptViewDataConnect();
                rpt = (RptViewDataConnect)this.GetRptViewDataConnect(((RptViewDataSource)GetRptViewDataSource(_id)).DataConnectID);
                dsb.DataConnect = rpt;
                //ds = dsb.ExecuteSql(FullName);
                ds = dsb.QuerySchemaSql(dataSource.SQL);
            }
            try
            {
                RptViewDataSourceColumn[] columns = GetRptViewDataSourceColumnByDataSourceId(Convert.ToInt32(_id));
                Dictionary<string, RptViewDataSourceColumn> columnMap = new Dictionary<string, RptViewDataSourceColumn>();
                for (int i = 0; columns != null && i < columns.Length; i++)
                {
                    columnMap.Add(columns[i].ColumnName, columns[i]);
                }
                List<string> tblColumnName = new List<string>();
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    if (columnMap.ContainsKey(ds.Tables[0].Columns[i].ColumnName) == false)
                    {
                        RptViewDataSourceColumn rptcol = new RptViewDataSourceColumn();
                        rptcol.ColumnName = ds.Tables[0].Columns[i].ToString();
                        rptcol.ColumnSequence = Convert.ToDecimal(i + 1);
                        rptcol.Description = rptcol.ColumnName;
                        rptcol.Visible = "1";
                        if (ds.Tables[0].Columns[i].DataType == typeof(decimal))
                            rptcol.DataType = ReportDataType.Numeric;
                        else
                            rptcol.DataType = ReportDataType.String;
                        rptcol.DataSourceID = _id;
                        rptcol.MaintainUser = userCode;
                        this.AddRptViewDataSourceColumn(rptcol);
                    }
                    else
                    {
                        if (Convert.ToInt32(columnMap[ds.Tables[0].Columns[i].ColumnName].ColumnSequence) != i + 1)
                        {
                            string strSql = "update tblrptvdatasrccolumn set columnseq=" + (i + 1).ToString() + " where datasourceid=" + columnMap[ds.Tables[0].Columns[i].ColumnName].DataSourceID.ToString() + " and columnseq=" + columnMap[ds.Tables[0].Columns[i].ColumnName].ColumnSequence.ToString() + " and columnname='" + columnMap[ds.Tables[0].Columns[i].ColumnName].ColumnName + "' ";
                            this.DataProvider.CustomExecute(new SQLCondition(strSql));
                            columnMap[ds.Tables[0].Columns[i].ColumnName].ColumnSequence = i + 1;
                        }
                    }
                    tblColumnName.Add(ds.Tables[0].Columns[i].ColumnName);
                }
                for (int i = 0; columns != null && i < columns.Length; i++)
                {
                    if (tblColumnName.Contains(columns[i].ColumnName) == false)
                    {
                        this.DeleteRptViewDataSourceColumn(columns[i]);
                    }
                }
            }
            catch
            { }

        }
        /// <summary>
        /// ** 功能描述:	查询TBLRPTVDATASRCCOLUMN的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftCode">_name，模糊查询</param>
        /// <returns> 总记录数</returns>
        public int QueryAllCount()
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("Select count(*) FROM TBLRPTVDATASRCCOLUMN")));
        }
		#endregion

		#region RptViewDataSourceParam
        public RptViewDataSourceParam[] GetRptViewDataSourceParamByDataSourceId(int dataSourceId)
        {
            string strSql = "SELECT * FROM TBLRPTVDATASRCPARAM WHERE DATASOURCEID=" + dataSourceId + " ORDER BY PARAMSEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewDataSourceParam), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewDataSourceParam[] parames = new RptViewDataSourceParam[objs.Length];
            objs.CopyTo(parames, 0);
            return parames;
        }
        /// <summary>
        /// ** 功能描述:	查询RptViewDataConnect的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftCode">_name，模糊查询</param>
        /// <returns> 总记录数</returns>
        public int QueryDataSourceParamCount(decimal _id)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("Select count(*) FROM TBLRPTVDATASRCPARAM Where DATASOURCEID LIKE '{0}%'", _id)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询RptViewDataConnect
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftCode">_name，模糊查询</param>
        /// <param name="shiftTypeCode">_type，模糊查询</param> 
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> RptViewDataConnect数组</returns>
        public object[] QueryDataSourceParam(decimal _id, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(RptViewDataSourceParam), new PagerCondition(string.Format("select {0} from TBLRPTVDATASRCPARAM where 1=1 and DATASOURCEID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataSourceParam)), _id), "PARAMSEQ", inclusive, exclusive));
        }
        /// <summary>
        /// ** 功能描述:	分页查询RptViewDataConnect
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftCode">_name，模糊查询</param>
        /// <param name="shiftTypeCode">_type，模糊查询</param> 
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> RptViewDataConnect数组</returns>
        public object[] GetAllDataSourceParamByID(decimal _id)
        {
            return this.DataProvider.CustomQuery(typeof(RptViewDataSourceParam), new SQLCondition(string.Format("select {0} from TBLRPTVDATASRCPARAM where 1=1 and DATASOURCEID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewDataSourceParam)), _id)));
        }
        #endregion

		#region RptViewDesignMain
        /// <summary>
        /// 查询下一个Report ID
        /// </summary>
        /// <returns></returns>
        public int GetNextReportId()
        {
            string strSql = "SELECT MAX(TO_NUMBER(RPTID)) RPTID FROM TBLRPTVDESIGNMAIN ";
            object[] objsTmp = this.DataProvider.CustomQuery(typeof(RptViewDesignMain), new SQLCondition(strSql));
            int iMax = 1;
            if (objsTmp != null && objsTmp.Length > 0 && ((RptViewDesignMain)objsTmp[0]).ReportID != "")
            {
                iMax = Convert.ToInt32(((RptViewDesignMain)objsTmp[0]).ReportID);
                iMax++;
            }
            return iMax;
        }

        /// <summary>
        /// 根据Report ID查询相关数据
        /// </summary>
        public ReportDesignView BuildReportDesignViewByReportId(string reportId)
        {
            ReportDesignView designView = new ReportDesignView();
            designView.ReportID = reportId;
            RptViewDesignMain rptMain = (RptViewDesignMain)this.GetRptViewDesignMain(designView.ReportID);
            designView.DesignMain = rptMain;
            designView.GridColumns = this.GetRptViewGridColumnsByReportId(designView.ReportID);
            designView.GridGroups = this.GetRptViewGridGroupsByReportId(designView.ReportID);
            designView.GridGroupTotals = this.GetRptViewGridGroupTotalsByReportId(designView.ReportID);
            designView.GridFilters = this.GetRptViewGridFiltersByReportId(designView.ReportID);
            designView.GridDataFormats = this.GetRptViewGridFormatsByReportId(designView.ReportID);
            designView.DataFormats = this.GetRptViewDataFormatsByReportId(designView.ReportID);
            designView.ChartMains = this.GetRptViewChartMainsByReportId(designView.ReportID);
            designView.ChartSeries = this.GetRptViewChartSeriesByReportId(designView.ReportID);
            designView.ChartCategories = this.GetRptViewChartCategoriesByReportId(designView.ReportID);
            designView.ChartDatas = this.GetRptViewChartDatasByReportId(designView.ReportID);
            designView.ChartDataFormats = this.GetRptViewDataFormatChartByReportId(designView.ReportID);
            designView.FileParameters = this.GetRptViewFileParametersByReportId(designView.ReportID);
            designView.ReportSecurity = this.GetRptViewReportSecurityByReportId(designView.ReportID);

            RptViewGridDataStyle style = (RptViewGridDataStyle)this.GetRptViewGridDataStyle(designView.ReportID);
            if (style != null)
            {
                designView.DefinedReportStyle = (RptViewReportStyle)this.GetRptViewReportStyle(style.StyleID);
            }

            designView.ExtendText = this.GetRptViewExtendTextsByReportId(designView.ReportID);
            designView.FiltersUI = this.GetRptViewFilterUIByReportId(designView.ReportID);

            return designView;
        }

        /// <summary>
        /// 发布上传的报表文件
        /// </summary>
        /// <param name="designView">输入的报表数据</param>
        /// <param name="targetFileName">报表文件应保存的目录</param>
        public void PublishUploadedReportFile(ReportDesignView designView, string targetFileName, string userCode)
        {
            string strSql = "";
            DBDateTime dDate = FormatHelper.GetNowDBDateTime(this.DataProvider);

            bool bIsNew = true;
            if (designView.ReportID != "")
            {
                bIsNew = false;
            }
            else
            {
                bIsNew = true;
                // 查询最大的报表ID
                designView.ReportID = this.GetNextReportId().ToString();
            }

            // 更新RptViewDesignMain
            designView.DesignMain.ReportID = designView.ReportID;
            //designView.DesignMain.ReportName
            //designView.DesignMain.Description
            //designView.DesignMain.DataSourceID
            designView.DesignMain.ReportBuilder = ReportBuilder.OffLine;
            designView.DesignMain.DisplayType = ReportDisplayType.Grid;
            designView.DesignMain.Status = ReportDesignStatus.Publish;
            //designView.DesignMain.ReportFileName
            //designView.DesignMain.ParentReportFolder
            designView.DesignMain.DesignUser = userCode;
            designView.DesignMain.DesignDate = dDate.DBDate;
            designView.DesignMain.DesignTime = dDate.DBTime;
            designView.DesignMain.PublishUser = userCode;
            designView.DesignMain.PublishDate = dDate.DBDate;
            designView.DesignMain.PublishTime = dDate.DBTime;
            designView.DesignMain.MaintainUser = userCode;
            designView.DesignMain.MaintainDate = dDate.DBDate;
            designView.DesignMain.MaintainTime = dDate.DBTime;
            if (bIsNew == true)
            {
                this.DataProvider.Insert(designView.DesignMain);
            }
            else
            {
                this.DataProvider.Update(designView.DesignMain);
            }

            // 更新RptViewFileParameter
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVFILEPARAM WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            if (designView.FileParameters != null)
            {
                for (int i = 0; i < designView.FileParameters.Length; i++)
                {
                    designView.FileParameters[i].ReportID = designView.ReportID;
                    designView.FileParameters[i].Sequence = i + 1;
                    //designView.FileParameters[i].FileParameterName
                    //designView.FileParameters[i].Description
                    //designView.FileParameters[i].DataType
                    //designView.FileParameters[i].DefaultValue
                    //designView.FileParameters[i].ViewerInput
                    designView.FileParameters[i].MaintainUser = userCode;
                    designView.FileParameters[i].MaintainDate = dDate.DBDate;
                    designView.FileParameters[i].MaintainTime = dDate.DBTime;
                    this.DataProvider.Insert(designView.FileParameters[i]);
                }
            }

            // 更新RptViewGridFilter
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVGRIDFLT WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            if (designView.GridFilters != null)
            {
                for (int i = 0; i < designView.GridFilters.Length; i++)
                {
                    designView.GridFilters[i].ReportID = designView.ReportID;
                    designView.GridFilters[i].FilterSequence = i + 1;
                    designView.GridFilters[i].DataSourceID = designView.DesignMain.DataSourceID;
                    //designView.GridFilters[i].ColumnName
                    //designView.GridFilters[i].ParameterName
                    //designView.GridFilters[i].Description
                    //designView.GridFilters[i].FilterOperation
                    designView.GridFilters[i].MaintainUser = userCode;
                    designView.GridFilters[i].MaintainDate = dDate.DBDate;
                    designView.GridFilters[i].MaintainTime = dDate.DBTime;
                    this.DataProvider.Insert(designView.GridFilters[i]);
                }
            }

            // 更新FilterUI
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVFILTERUI WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.FiltersUI != null && i < designView.FiltersUI.Length; i++)
            {
                designView.FiltersUI[i].ReportID = designView.ReportID;
                designView.FiltersUI[i].Sequence = i + 1;
                designView.FiltersUI[i].MaintainUser = userCode;
                designView.FiltersUI[i].MaintainDate = dDate.DBDate;
                designView.FiltersUI[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.FiltersUI[i]);
            }

            // 更新RptViewReportSecurity
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVRPTSECURITY WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            if (designView.ReportSecurity != null)
            {
                for (int i = 0; i < designView.ReportSecurity.Length; i++)
                {
                    designView.ReportSecurity[i].ReportID = designView.ReportID;
                    designView.ReportSecurity[i].Sequence = i + 1;
                    //designView.ReportSecurity[i].UserGroupCode
                    //designView.ReportSecurity[i].RightAccess
                    this.DataProvider.Insert(designView.ReportSecurity[i]);
                }
            }

            // 更新RptViewEntry
            this.UpdateReportEntryPublish(designView.DesignMain, userCode);

            if (targetFileName != "")
            {
                // 拷贝文件
                if (System.IO.File.Exists(targetFileName) == true)
                {
                    string strTmpFile = targetFileName.Substring(0, targetFileName.LastIndexOf(".")) + "_" + dDate.DBDate.ToString() + dDate.DBTime.ToString().PadLeft(6, '0') + targetFileName.Substring(targetFileName.LastIndexOf("."));
                    System.IO.File.Move(targetFileName, strTmpFile);
                }
                System.IO.File.Move(designView.UploadFileName, targetFileName);
            }
        }

        /// <summary>
        /// 保存在线设计的报表数据
        /// </summary>
        /// <param name="designView"></param>
        /// <param name="userCode"></param>
        public void SaveDesignReportData(ReportDesignView designView, string userCode)
        {
            string strSql = "";
            DBDateTime dDate = FormatHelper.GetNowDBDateTime(this.DataProvider);

            bool bIsNew = true;
            if (designView.ReportID != "")
            {
                bIsNew = false;
            }
            else
            {
                bIsNew = true;
                // 查询最大的报表ID
                designView.ReportID = this.GetNextReportId().ToString();
            }

            // 更新RptViewDesignMain
            designView.DesignMain.ReportID = designView.ReportID;
            //designView.DesignMain.ReportName
            //designView.DesignMain.Description
            //designView.DesignMain.DataSourceID
            designView.DesignMain.ReportBuilder = ReportBuilder.OnLine;
            //designView.DesignMain.DisplayType
            designView.DesignMain.Status = ReportDesignStatus.Initial;
            //designView.DesignMain.ReportFileName
            //designView.DesignMain.ParentReportFolder
            designView.DesignMain.DesignUser = userCode;
            designView.DesignMain.DesignDate = dDate.DBDate;
            designView.DesignMain.DesignTime = dDate.DBTime;
            designView.DesignMain.PublishUser = userCode;
            designView.DesignMain.PublishDate = dDate.DBDate;
            designView.DesignMain.PublishTime = dDate.DBTime;
            designView.DesignMain.MaintainUser = userCode;
            designView.DesignMain.MaintainDate = dDate.DBDate;
            designView.DesignMain.MaintainTime = dDate.DBTime;
            if (bIsNew == true)
            {
                this.DataProvider.Insert(designView.DesignMain);
            }
            else
            {
                this.DataProvider.Update(designView.DesignMain);
            }

            // 更新RptViewGridColumn
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVGRIDCOLUMN WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.GridColumns != null && i < designView.GridColumns.Length; i++)
            {
                designView.GridColumns[i].ReportID = designView.ReportID;
                designView.GridColumns[i].DisplaySequence = i + 1;
                designView.GridColumns[i].DataSourceID = designView.DesignMain.DataSourceID;
                //designView.GridColumns[i].ColumnName
                designView.GridColumns[i].MaintainUser = userCode;
                designView.GridColumns[i].MaintainDate = dDate.DBDate;
                designView.GridColumns[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.GridColumns[i]);
            }

            // 更新RptViewGridGroup
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVGRIDGRP WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.GridGroups != null && i < designView.GridGroups.Length; i++)
            {
                designView.GridGroups[i].ReportID = designView.ReportID;
                //designView.GridGroups[i].GroupSequence;
                designView.GridGroups[i].DataSourceID = designView.DesignMain.DataSourceID;
                //designView.GridGroups[i].ColumnName
                designView.GridGroups[i].MaintainUser = userCode;
                designView.GridGroups[i].MaintainDate = dDate.DBDate;
                designView.GridGroups[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.GridGroups[i]);
            }

            // 更新RptViewGridGroupTotal
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVGRIDGRPTOTAL WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.GridGroupTotals != null && i < designView.GridGroupTotals.Length; i++)
            {
                designView.GridGroupTotals[i].ReportID = designView.ReportID;
                //designView.GridGroupTotals[i].GroupSequence
                //designView.GridGroupTotals[i].ColumnName
                designView.GridGroupTotals[i].DataSourceID = designView.DesignMain.DataSourceID;
                //designView.GridGroupTotals[i].TotalType
                designView.GridGroupTotals[i].MaintainUser = userCode;
                designView.GridGroupTotals[i].MaintainDate = dDate.DBDate;
                designView.GridGroupTotals[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.GridGroupTotals[i]);
            }

            // 更新RptViewGridFilter
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVGRIDFLT WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.GridFilters != null && i < designView.GridFilters.Length; i++)
            {
                designView.GridFilters[i].ReportID = designView.ReportID;
                designView.GridFilters[i].FilterSequence = i + 1;
                designView.GridFilters[i].DataSourceID = designView.DesignMain.DataSourceID;
                //designView.GridFilters[i].ColumnName
                //designView.GridFilters[i].ParameterName
                //designView.GridFilters[i].Description
                //designView.GridFilters[i].FilterOperation
                //designView.GridFilters[i].DefaultValue
                designView.GridFilters[i].MaintainUser = userCode;
                designView.GridFilters[i].MaintainDate = dDate.DBDate;
                designView.GridFilters[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.GridFilters[i]);
            }

            // 更新RptViewReportGridStyle
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVGRIDDATASTYLE WHERE RPTID='" + designView.ReportID + "'";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            if (designView.DefinedReportStyle != null)
            {
                RptViewGridDataStyle style = new RptViewGridDataStyle();
                style.ReportID = designView.ReportID;
                style.StyleID = designView.DefinedReportStyle.StyleID;
                style.MaintainUser = designView.DesignMain.MaintainUser;
                this.DataProvider.Insert(style);
            }

            // 更新RptViewDataFormat
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVDATAFMT WHERE FORMATID IN (SELECT FORMATID FROM TBLRPTVGRIDDATAFMT WHERE RPTID='" + designView.ReportID + "')";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.DataFormats != null && i < designView.DataFormats.Length; i++)
            {
                designView.DataFormats[i].MaintainUser = userCode;
                designView.DataFormats[i].MaintainDate = dDate.DBDate;
                designView.DataFormats[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.DataFormats[i]);
            }

            // 更新RptViewGridDataFormat
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVGRIDDATAFMT WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.GridDataFormats != null && i < designView.GridDataFormats.Length; i++)
            {
                designView.GridDataFormats[i].ReportID = designView.ReportID;
                //designView.GridDataFormats[i].ColumnName
                //designView.GridDataFormats[i].StyleType
                //designView.GridDataFormats[i].GroupSequence
                //designView.GridDataFormats[i].FormatID
                designView.GridDataFormats[i].MaintainUser = userCode;
                designView.GridDataFormats[i].MaintainDate = dDate.DBDate;
                designView.GridDataFormats[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.GridDataFormats[i]);
            }

            // 更新ExtendText
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVEXTTEXT WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.ExtendText != null && i < designView.ExtendText.Length; i++)
            {
                designView.ExtendText[i].ReportID = designView.ReportID;
                //designView.ExtendText[i].Sequence
                designView.ExtendText[i].MaintainUser = userCode;
                designView.ExtendText[i].MaintainDate = dDate.DBDate;
                designView.ExtendText[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.ExtendText[i]);
            }

            // 更新FilterUI
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVFILTERUI WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.FiltersUI != null && i < designView.FiltersUI.Length; i++)
            {
                designView.FiltersUI[i].ReportID = designView.ReportID;
                designView.FiltersUI[i].Sequence = i + 1;
                designView.FiltersUI[i].MaintainUser = userCode;
                designView.FiltersUI[i].MaintainDate = dDate.DBDate;
                designView.FiltersUI[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.FiltersUI[i]);
            }

            // 更新ChartMain
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVCHARTMAIN WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.ChartMains != null && i < designView.ChartMains.Length; i++)
            {
                designView.ChartMains[i].ReportID = designView.ReportID;
                //designView.ChartMains[i].ChartSequence
                //designView.ChartMains[i].DataSourceID 
                //designView.ChartMains[i].ChartType 
                //designView.ChartMains[i].ChartSubType 
                //designView.ChartMains[i].ShowLegend 
                //designView.ChartMains[i].ShowMarker 
                //designView.ChartMains[i].MarkerType 
                //designView.ChartMains[i].ShowLabel 
                //designView.ChartMains[i].LabelFormatID 
                designView.ChartMains[i].MaintainUser = userCode;
                designView.ChartMains[i].MaintainDate = dDate.DBDate;
                designView.ChartMains[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.ChartMains[i]);
            }

            // 更新ChartSeries
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVCHARTSER WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.ChartSeries != null && i < designView.ChartSeries.Length; i++)
            {
                designView.ChartSeries[i].ReportID = designView.ReportID;
                //designView.ChartSeries[i].ChartSequence 
                //designView.ChartSeries[i].SeriesSequence
                //designView.ChartSeries[i].DataSourceID
                //designView.ChartSeries[i].ColumnName
                //designView.ChartSeries[i].Description
                designView.ChartSeries[i].MaintainUser = userCode;
                designView.ChartSeries[i].MaintainDate = dDate.DBDate;
                designView.ChartSeries[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.ChartSeries[i]);
            }

            // 更新ChartCategory
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVCHARTCATE WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.ChartCategories != null && i < designView.ChartCategories.Length; i++)
            {
                designView.ChartCategories[i].ReportID = designView.ReportID;
                //designView.ChartCategories[i].ChartSequence
                //designView.ChartCategories[i].CategorySequence 
                //designView.ChartCategories[i].DataSourceID
                //designView.ChartCategories[i].ColumnName
                //designView.ChartCategories[i].Description
                designView.ChartCategories[i].MaintainUser = userCode;
                designView.ChartCategories[i].MaintainDate = dDate.DBDate;
                designView.ChartCategories[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.ChartCategories[i]);
            }

            // 更新ChartData
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVCHARTDATA WHERE RPTID='" + designView.ReportID + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.ChartDatas != null && i < designView.ChartDatas.Length; i++)
            {
                designView.ChartDatas[i].ReportID = designView.ReportID;
                //designView.ChartDatas[i].ChartSequence
                //designView.ChartDatas[i].DataSequence
                //designView.ChartDatas[i].DataSourceID
                //designView.ChartDatas[i].ColumnName
                //designView.ChartDatas[i].Description
                //designView.ChartDatas[i].TotalType
                designView.ChartDatas[i].MaintainUser = userCode;
                designView.ChartDatas[i].MaintainDate = dDate.DBDate;
                designView.ChartDatas[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.ChartDatas[i]);
            }

            // 更新RptViewDataFormat
            if (bIsNew == false)
            {
                strSql = "DELETE FROM TBLRPTVDATAFMT WHERE FORMATID IN (SELECT LABELFORMATID FROM TBLRPTVCHARTMAIN WHERE RPTID='" + designView.ReportID + "')";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
            }
            for (int i = 0; designView.ChartDataFormats != null && i < designView.ChartDataFormats.Length; i++)
            {
                designView.ChartDataFormats[i].MaintainUser = userCode;
                designView.ChartDataFormats[i].MaintainDate = dDate.DBDate;
                designView.ChartDataFormats[i].MaintainTime = dDate.DBTime;
                this.DataProvider.Insert(designView.ChartDataFormats[i]);
            }
        }

        /// <summary>
        /// 根据状态查询所有报表
        /// </summary>
        /// <param name="statusList"></param>
        /// <returns></returns>
        public RptViewDesignMain[] GetRptviewDesignMainByStatus(params string[] statusList)
        {
            string strSql = "SELECT * FROM TBLRPTVDESIGNMAIN WHERE 1=1 ";
            string strFilter = " 1=0 ";
            for (int i = 0; i < statusList.Length; i++)
            {
                strFilter += " OR STATUS='" + statusList[i] + "' ";
            }
            if (statusList.Length > 0)
                strSql += " AND (" + strFilter + ") ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewDesignMain), new SQLCondition(strSql));
            if (objs != null)
            {
                RptViewDesignMain[] list = new RptViewDesignMain[objs.Length];
                objs.CopyTo(list, 0);
                return list;
            }
            return null;
        }

        public RptViewDesignMain GetRptViewDesignMainByReportName(string name)
        {
            string strSql = "select * from tblrptvdesignmain where upper(rptname)='" + name + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewDesignMain), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
                return null;
            else
                return (RptViewDesignMain)objs[0];
        }
        #endregion

        #region RptViewEntry
        public object[] QueryRptViewEntryByParent(string parentCode, int inclusive, int exclusive)
        {
            string strSql = "";
            if (parentCode == "" || parentCode == "UNFOLDER")
                strSql = string.Format("SELECT {0} FROM TBLRPTVENTRY WHERE PENTRYCODE='' OR PENTRYCODE IS NULL ORDER BY SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewEntry)));
            else
                strSql = string.Format("SELECT {0} FROM TBLRPTVENTRY WHERE PENTRYCODE='" + parentCode + "' ORDER BY SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewEntry)));
            return this.DataProvider.CustomQuery(typeof(RptViewEntry), new PagerCondition(strSql, "SEQ", inclusive, exclusive));
        }
        public int QueryRptViewEntryByParentCount(string parentCode)
        {
            string strSql = "";
            if (parentCode == "" || parentCode == "UNFOLDER")
                strSql = "SELECT count(*) FROM TBLRPTVENTRY WHERE PENTRYCODE='' OR PENTRYCODE IS NULL";
            else
                strSql = "SELECT count(*) FROM TBLRPTVENTRY WHERE PENTRYCODE='" + parentCode + "'";
            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }

        public RptViewEntry[] GetRptViewEntryFolder()
        {
            string strSql = "SELECT * FROM TBLRPTVENTRY WHERE ENTRYTYPE='" + ReportEntryType.Folder + "' ORDER BY SEQ ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewEntry), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewEntry[] entryList = new RptViewEntry[objs.Length];
            objs.CopyTo(entryList, 0);
            return entryList;
        }

        public void UpdateReportEntryPublish(RptViewDesignMain designMain, string userCode)
        {
            string strSql = "SELECT * FROM TBLRPTVENTRY WHERE RPTID='" + designMain.ReportID + "' ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewEntry), new SQLCondition(strSql));
            RptViewEntry entry = null;
            if (objs != null && objs.Length > 0)
                entry = (RptViewEntry)objs[0];
            if (entry == null)
            {
                strSql = "SELECT MAX(SEQ) SEQ FROM TBLRPTVENTRY WHERE PENTRYCODE='" + designMain.ParentReportFolder + "'";
                object[] objsTmp = this.DataProvider.CustomQuery(typeof(RptViewEntry), new SQLCondition(strSql));
                int iSeq = 1;
                if (objsTmp != null && objsTmp.Length > 0)
                {
                    iSeq = Convert.ToInt32(((RptViewEntry)objsTmp[0]).Sequence) + 1;
                }
                entry = new RptViewEntry();
                entry.ReportID = designMain.ReportID;
                entry.Sequence = iSeq;
                string strEntryCode = "RPT-" + designMain.ReportID;
                if (this.GetRptViewEntry(strEntryCode) != null)
                    strEntryCode = "RPT-" + designMain.ReportID + "-" + System.Guid.NewGuid().ToString().Substring(0, 30);
                entry.EntryCode = strEntryCode;
                entry.EntryName = designMain.ReportName;
                entry.Description = designMain.Description;
                entry.EntryType = ReportEntryType.Report;
                entry.ParentEntryCode = designMain.ParentReportFolder;
                entry.Visible = FormatHelper.BooleanToString(true);
                entry.MaintainUser = userCode;
                this.DataProvider.Insert(entry);
            }
            else
            {
                entry.EntryType = ReportEntryType.Report;
                entry.ParentEntryCode = designMain.ParentReportFolder;
                entry.MaintainUser = userCode;
                this.DataProvider.Update(entry);
            }
        }

        /// <summary>
        /// 在报表结构中删除报表项时，同时删除对应的报表子项内容
        /// </summary>
        /// <param name="entryList"></param>
        public void DeleteRptViewEntryWithReport(RptViewEntry[] entryList)
        {
            if (entryList == null || entryList.Length == 0)
                return;
            this.DataProvider.BeginTransaction();
            try
            {
                for (int i = 0; i < entryList.Length; i++)
                {
                    if (entryList[i].EntryType == ReportEntryType.Report && string.IsNullOrEmpty(entryList[i].ReportID) == false)
                    {
                        string strRptId = entryList[i].ReportID;
                        string strFormat = "DELETE FROM {0} WHERE RPTID='" + strRptId + "'";
                        string strSql = string.Format(strFormat, "TBLRPTVGRIDCOLUMN");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVGRIDGRP");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVGRIDGRPTOTAL");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVGRIDFLT");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVCHARTMAIN");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVCHARTSER");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVCHARTCATE");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVCHARTDATA");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVFILEPARAM");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVGRIDDATAFMT");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVDESIGNMAIN");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVRPTSECURITY");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVUSERSUBSCR");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVGRIDDATASTYLE");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVEXTTEXT");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = string.Format(strFormat, "TBLRPTVFILTERUI");
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                        strSql = "DELETE FROM TBLRPTVUSERDFT WHERE DEFAULTRPTID='" + strRptId + "'";
                        this.DataProvider.CustomExecute(new SQLCondition(strSql));
                    }
                    this.DeleteRptViewEntry(entryList[i]);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            finally
            {
            }
        }
        #endregion

        #region RptViewExtendText
        public RptViewExtendText[] GetRptViewExtendTextsByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVEXTTEXT WHERE RPTID='" + reportId + "' ORDER BY SEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewExtendText), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewExtendText[] text = new RptViewExtendText[objs.Length];
            objs.CopyTo(text, 0);
            return text;
        }
        #endregion

        #region RptViewFileParameter
        public RptViewFileParameter[] GetRptViewFileParametersByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVFILEPARAM WHERE RPTID='" + reportId + "' ORDER BY SEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewFileParameter), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewFileParameter[] parames = new RptViewFileParameter[objs.Length];
            objs.CopyTo(parames, 0);
            return parames;
        }
        #endregion

        #region RptViewFilterUI
        public RptViewFilterUI[] GetRptViewFilterUIByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVFILTERUI WHERE RPTID='" + reportId + "' ORDER BY SEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewFilterUI), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewFilterUI[] filters = new RptViewFilterUI[objs.Length];
            objs.CopyTo(filters, 0);
            return filters;
        }
        //Add 2008/11/05
        public RptViewFilterUI[] GetRptViewFilterUIByReportIdAndSeq(string reportId, string strName,decimal seq)
        {
            string strSql = "SELECT * FROM TBLRPTVFILTERUI WHERE RPTID='" + reportId + "' AND INPUTNAME ='" + strName + "' AND SEQ="+seq+"  ORDER BY SEQ";

            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewFilterUI), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewFilterUI[] filters = new RptViewFilterUI[objs.Length];
            objs.CopyTo(filters, 0);
            return filters;
        }
        #endregion

        #region RptViewGridColumn
        public RptViewGridColumn[] GetRptViewGridColumnsByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVGRIDCOLUMN WHERE RPTID='" + reportId + "' ORDER BY DISPLAYSEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewGridColumn), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewGridColumn[] columns = new RptViewGridColumn[objs.Length];
            objs.CopyTo(columns, 0);
            return columns;
        }
		#endregion

		#region RptViewGridDataFormat
        public RptViewGridDataFormat[] GetRptViewGridFormatsByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVGRIDDATAFMT WHERE RPTID='" + reportId + "' ORDER BY STYLETYPE,COLUMNNAME";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewGridDataFormat), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewGridDataFormat[] formats = new RptViewGridDataFormat[objs.Length];
            objs.CopyTo(formats, 0);
            return formats;
        }
        #endregion

		#region RptViewGridFilter
        public RptViewGridFilter[] GetRptViewGridFiltersByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVGRIDFLT WHERE RPTID='" + reportId + "' ORDER BY FLTSEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewGridFilter), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewGridFilter[] filters = new RptViewGridFilter[objs.Length];
            objs.CopyTo(filters, 0);
            return filters;
        }
        //Modified 2008/11/11
        public RptViewGridFilter GetRptViewGridFiltersByReportIdAndName(string reportId,string columnName,decimal seq)
        {
            string strSql = "SELECT * FROM TBLRPTVGRIDFLT WHERE RPTID='" + reportId + "' AND COLUMNNAME ='"+ columnName+"' AND FLTSEQ="+seq+"  ORDER BY FLTSEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewGridFilter), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewGridFilter[] filters = new RptViewGridFilter[objs.Length];
            objs.CopyTo(filters, 0);
            if (filters.Length > 0)
            {
                return filters[0];
            }
            return null;
        }
        #endregion

		#region RptViewGridGroup
        public RptViewGridGroup[] GetRptViewGridGroupsByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVGRIDGRP WHERE RPTID='" + reportId + "' ORDER BY GRPSEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewGridGroup), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewGridGroup[] groups = new RptViewGridGroup[objs.Length];
            objs.CopyTo(groups, 0);
            return groups;
        }
        #endregion

		#region RptViewGridGroupTotal
        public RptViewGridGroupTotal[] GetRptViewGridGroupTotalsByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVGRIDGRPTOTAL WHERE RPTID='" + reportId + "' ORDER BY GRPSEQ,COLUMNNAME";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewGridGroupTotal), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewGridGroupTotal[] groups = new RptViewGridGroupTotal[objs.Length];
            objs.CopyTo(groups, 0);
            return groups;
        }
        public RptViewGridGroupTotal[] GetRptViewGridGroupTotalsByReportIdAndGroup(string reportId, int groupSeq)
        {
            string strSql = "SELECT * FROM TBLRPTVGRIDGRPTOTAL WHERE RPTID='" + reportId + "' AND GRPSEQ=" + groupSeq + " ORDER BY GRPSEQ,COLUMNNAME";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewGridGroupTotal), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewGridGroupTotal[] groups = new RptViewGridGroupTotal[objs.Length];
            objs.CopyTo(groups, 0);
            return groups;
        }
        #endregion

        #region RptViewReportSecurity
        public RptViewReportSecurity[] GetRptViewReportSecurityByReportId(string reportId)
        {
            string strSql = "SELECT * FROM TBLRPTVRPTSECURITY WHERE RPTID='" + reportId + "' ORDER BY SEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewReportSecurity), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewReportSecurity[] security = new RptViewReportSecurity[objs.Length];
            objs.CopyTo(security, 0);
            return security;
        }
        public void UpdateRptViewReportSecurity(string reportId, string[] userGroupList)
        {
            string strSql = "DELETE FROM TBLRPTVRPTSECURITY WHERE RPTID='" + reportId + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(strSql));
            strSql = "SELECT MAX(SEQ) SEQ FROM TBLRPTVRPTSECURITY";
            object[] objsTmp = this.DataProvider.CustomQuery(typeof(RptViewReportSecurity), new SQLCondition(strSql));
            int iSeq = 1;
            if (objsTmp != null && objsTmp.Length > 0)
            {
                iSeq = Convert.ToInt32(((RptViewReportSecurity)objsTmp[0]).Sequence) + 1;
            }
            for (int i = 0; i < userGroupList.Length; i++)
            {
                RptViewReportSecurity security = new RptViewReportSecurity();
                security.ReportID = reportId;
                security.Sequence = iSeq + i;
                ////Modified by allen on 20081104 for change security: functiongroup
                //security.UserGroupCode = userGroupList[i];
                security.FunctionGroupCode = userGroupList[i];
                ////End Modified by allen on 20081104 for change security: functiongroup
                security.RightAccess = "1";
                this.DataProvider.Insert(security);
            }
        }
        /// <summary>
        /// 查询用户是否有访问某个报表的权限(此处没有添加ADMIN的判断)
        /// </summary>
        /// <param name="reportId">Report ID</param>
        /// <param name="userCode">用户代码</param>
        /// <returns></returns>
        public bool CheckUserReportSecurity(string reportId, string userCode)
        {
            ////Modified by allen on 20081104 for change security: functiongroup
            //string strSql = "select * from tblrptvrptsecurity where rptid='" + reportId + "' and usergroupcode in (select usergroupcode from tblusergroup2user where usercode='" + userCode + "')";
            string strSql = "select * from tblrptvrptsecurity where rptid='" + reportId + "'  AND EXISTS (SELECT functiongroupcode FROM tblusergroup2functiongroup WHERE functiongroupcode = tblrptvrptsecurity.functiongroupcode AND usergroupcode IN(select usergroupcode from tblusergroup2user where usercode='" + userCode + "') )";
            ////End Modified by allen on 20081104 for change security: functiongroup
            object[] obj = this.DataProvider.CustomQuery(typeof(RptViewReportSecurity), new SQLCondition(strSql));
            if (obj != null && obj.Length > 0 && obj[0] != null)
                return true;
            return false;
        }
        #endregion

        #region RptViewReportStyle
        public int QueryRptViewReportStyleByNameCount(string name)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRPTVSTYLE where 1=1 and NAME like '{0}%' ", name)));
        }

        public object[] QueryRptViewReportStyleByName(string name, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(RptViewReportStyle), new PagerCondition(string.Format("select {0} from TBLRPTVSTYLE where 1=1 and NAME like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RptViewReportStyle)), name), "NAME", inclusive, exclusive));
        }

        public decimal GetNextStyleID()
        {
            decimal d = 1;
            string strSql = "select max(styleid) styleid from TBLRPTVSTYLE";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewReportStyle), new SQLCondition(strSql));
            if (objs != null && objs.Length > 0)
                d = ((RptViewReportStyle)objs[0]).StyleID + 1;
            return d;
        }
        #endregion

        #region RptViewReportStyleDetail
        public RptViewReportStyleDetail[] GetRptViewReportStyleDetailByStyleID(decimal styleId)
        {
            string strSql = "select * from TBLRPTVSTYLEDTL where STYLEID=" + styleId.ToString();
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewReportStyleDetail), new SQLCondition(strSql));
            if (objs == null)
                return null;
            else
            {
                RptViewReportStyleDetail[] dtl = new RptViewReportStyleDetail[objs.Length];
                objs.CopyTo(dtl, 0);
                return dtl;
            }
        }

        public void UpdateRptViewReportStyleDetail(decimal styleId, string styleType, RptViewDataFormat dataFormat)
        {
            string strSql = "select * from TBLRPTVSTYLEDTL where STYLEID=" + styleId.ToString() + " and STYLETYPE='" + styleType + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewReportStyleDetail), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
            {
                dataFormat.FormatID = System.Guid.NewGuid().ToString();
                this.DataProvider.Insert(dataFormat);
                RptViewReportStyleDetail style = new RptViewReportStyleDetail();
                style.StyleID = styleId;
                style.StyleType = styleType;
                style.FormatID = dataFormat.FormatID;
                style.MaintainUser = dataFormat.MaintainUser;
                this.DataProvider.Insert(style);
            }
            else
            {
                RptViewReportStyleDetail style = (RptViewReportStyleDetail)objs[0];
                dataFormat.FormatID = style.FormatID;
                this.DataProvider.Update(dataFormat);
            }
        }
        #endregion

        #region RptViewUserDefault
        #endregion

        #region RptViewUserSubscription
        public RptViewUserSubscription[] GetRptViewUserSubscriptionsByReportId(string reportId, string userCode)
        {
            string strSql = "SELECT * FROM TBLRPTVUSERSUBSCR WHERE RPTID='" + reportId + "' AND USERCODE='" + userCode + "' ORDER BY SEQ";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewUserSubscription), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewUserSubscription[] subs = new RptViewUserSubscription[objs.Length];
            objs.CopyTo(subs, 0);
            return subs;
        }
        /// <summary>
        /// 根据ReportID获取需要输入的内容
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public RptViewUserSubscription[] GetNeedInputByReportId(string reportId, string userCode)
        {
            RptViewGridFilter[] objs = this.GetRptViewGridFiltersByReportId(reportId);
            Dictionary<string, RptViewGridFilter> filters = new Dictionary<string, RptViewGridFilter>();
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    if (objs[i].ColumnName != "")
                    {
                        filters.Add(objs[i].ColumnName + ":" + objs[i].FilterSequence.ToString(), objs[i]);
                    }
                    else
                    {
                        filters.Add(objs[i].ParameterName + ":" + objs[i].FilterSequence.ToString(), objs[i]);
                    }
                }
            }
            RptViewFileParameter[] objs1 = this.GetRptViewFileParametersByReportId(reportId);
            Dictionary<string, RptViewFileParameter> parames = new Dictionary<string, RptViewFileParameter>();
            if (objs1 != null)
            {
                for (int i = 0; i < objs1.Length; i++)
                {
                    if (FormatHelper.StringToBoolean(objs1[i].ViewerInput) == true)
                    {
                        parames.Add(objs1[i].FileParameterName, objs1[i]);
                    }
                }
            }
            RptViewUserSubscription[] objs2 = this.GetRptViewUserSubscriptionsByReportId(reportId, userCode);
            List<RptViewUserSubscription> subs = new List<RptViewUserSubscription>();
            if (objs2 != null)
            {
                subs.AddRange(objs2);
            }

            // 更新描述
            for (int i = subs.Count - 1; i >= 0; i--)
            {
                if (subs[i].InputType == ReportViewerInputType.SqlFilter ||
                    subs[i].InputType == ReportViewerInputType.DllParameter)
                {
                    if (filters.ContainsKey(subs[i].InputName + ":" + subs[i].SqlFilterSequence.ToString()) == true)
                    {
                        subs[i].EAttribute1 = filters[subs[i].InputName + ":" + subs[i].SqlFilterSequence.ToString()].Description;
                        filters.Remove(subs[i].InputName + ":" + subs[i].SqlFilterSequence.ToString());
                    }
                    else
                    {
                        subs.RemoveAt(i);
                    }
                }
                else if (subs[i].InputType == ReportViewerInputType.FileParameter)
                {
                    if (parames.ContainsKey(subs[i].InputName) == true)
                    {
                        subs[i].EAttribute1 = parames[subs[i].InputName].Description;
                        parames.Remove(subs[i].InputName);
                    }
                    else
                    {
                        subs.RemoveAt(i);
                    }
                }
            }

            // 添加未设定值的数据
            if (filters.Count > 0 || parames.Count > 0)
            {
                foreach (RptViewGridFilter flt in filters.Values)
                {
                    RptViewUserSubscription s = new RptViewUserSubscription();
                    s.ReportID = reportId;
                    if (flt.ColumnName != "")
                    {
                        s.InputType = ReportViewerInputType.SqlFilter;
                        s.InputName = flt.ColumnName;
                        s.EAttribute1 = flt.Description;
                        s.InputValue = flt.DefaultValue;
                        s.SqlFilterSequence = flt.FilterSequence;
                    }
                    else if (flt.ParameterName != "")
                    {
                        s.InputType = ReportViewerInputType.DllParameter;
                        s.InputName = flt.ParameterName;
                        s.EAttribute1 = flt.Description;
                        s.InputValue = flt.DefaultValue;
                        s.SqlFilterSequence = flt.FilterSequence;
                    }
                    subs.Add(s);
                }
                foreach (RptViewFileParameter fp in parames.Values)
                {
                    RptViewUserSubscription s = new RptViewUserSubscription();
                    s.ReportID = reportId;
                    s.InputType = ReportViewerInputType.FileParameter;
                    s.InputName = fp.FileParameterName;
                    s.EAttribute1 = fp.Description;
                    s.InputValue = fp.DefaultValue;
                    subs.Add(s);
                }
            }
            RptViewUserSubscription[] retList = new RptViewUserSubscription[subs.Count];
            subs.CopyTo(retList);

            return retList;
        }
        public void UpdateRptViewUserSubscriptionByReportId(string reportId, string userCode, RptViewUserSubscription[] subscr, bool asDefault)
        {
            string strSql = "DELETE FROM TBLRPTVUSERSUBSCR WHERE RPTID='" + reportId + "' AND USERCODE='" + userCode + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(strSql));
            for (int i = 0; i < subscr.Length; i++)
            {
                this.DataProvider.Insert(subscr[i]);
            }
            RptViewUserDefault objDef = (RptViewUserDefault)this.GetRptViewUserDefault(userCode);
            
            if (asDefault == false && (objDef != null && objDef.DefaultReportID == reportId))
            {
                this.DeleteRptViewUserDefault(objDef);
            }
            if (asDefault == true && (objDef == null || objDef.DefaultReportID != reportId))
            {
                bool bIsNew = false;
                if (objDef == null)
                {
                    objDef = new RptViewUserDefault();
                    objDef.UserCode = userCode;
                    bIsNew = true;
                }
                objDef.DefaultReportID = reportId;
                objDef.MaintainUser = userCode;
                if (bIsNew == true)
                    this.AddRptViewUserDefault(objDef);
                else
                    this.UpdateRptViewUserDefault(objDef);
            }
        }
        #endregion

        #region RptViewDataConnect
        public RptViewDataConnect[] GetRptViewDataConnect()
        {
            string strSql = "SELECT * FROM TBLRPTVCONNECT ORDER BY connectname";
            object[] objs = this.DataProvider.CustomQuery(typeof(RptViewDataConnect), new SQLCondition(strSql));
            if (objs == null)
                return null;
            RptViewDataConnect[] groups = new RptViewDataConnect[objs.Length];
            objs.CopyTo(groups, 0);
            return groups;
        }
        #endregion

    }
}

