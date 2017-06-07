using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.ReportView;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.ReportView
{
    public class ReportGenerater
    {
        private IDomainDataProvider _domainDataProvider = null;
        public ReportGenerater(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        private Dictionary<string, decimal> dicColumnWidth = null;

        /// <summary>
        /// 生成报表文件
        /// </summary>
        /// <param name="reportId">报表ID</param>
        /// <param name="formatFile">报表格式模板</param>
        /// <param name="reportFile">生成的报表文件名称</param>
        /// <returns>报表文件路径</returns>
        public string Generate(string reportId, string formatFile, string reportFile)
        {
            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            ReportDesignView designView = rptFacade.BuildReportDesignViewByReportId(reportId);
            return this.Generate(designView, formatFile, reportFile);
        }

        private decimal dRptExtendTextHeight = 0;

        /// <summary>
        /// 生成报表文件
        /// </summary>
        /// <param name="designView">报表设计数据</param>
        /// <param name="formatFile">报表格式模板</param>
        /// <returns>报表文件路径</returns>
        public string Generate(ReportDesignView designView, string formatFile, string reportFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(formatFile);
            StringBuilder sbRpt = new StringBuilder();
            sbRpt.Append(xmlDoc.SelectSingleNode("//MainBody").FirstChild.Value);

            designView.DataSourceColumns.Clear();
            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            RptViewDataSourceColumn[] objColumns = rptFacade.GetRptViewDataSourceColumnByDataSourceId(Convert.ToInt32(designView.DesignMain.DataSourceID));
            for (int i = 0; objColumns != null && i < objColumns.Length; i++)
            {
                designView.DataSourceColumns.Add(objColumns[i].ColumnName, objColumns[i]);
            }

            sbRpt.Replace("<%ReportID%>", System.Guid.NewGuid().ToString().ToLower());

            string strTable = "";
            string strChart = "";
            decimal iRptHeight = 0;
            decimal iRptWidth = 0;
            decimal iTableHeight = 0;
            if (designView.DesignMain.DisplayType == ReportDisplayType.Grid ||
                designView.DesignMain.DisplayType == ReportDisplayType.GridChart)
            {
                strTable = this.BuildTable(designView, xmlDoc);
                iRptHeight = ((designView.GridGroups == null ? 0 : designView.GridGroups.Length) + 2) * 0.5M;
                foreach (decimal dWidth in dicColumnWidth.Values)
                {
                    iRptWidth += dWidth;
                }
                iTableHeight = iRptHeight;
            }
            if (designView.DesignMain.DisplayType == ReportDisplayType.Chart ||
                designView.DesignMain.DisplayType == ReportDisplayType.GridChart)
            {
                strChart = this.BuildChart(designView, xmlDoc);
                iRptHeight = iRptHeight + 9;
                if (iRptWidth == 0)
                {
                    iRptWidth = 22;
                }
            }
            string strExtendText = BuildExtendText(designView, xmlDoc);

            strTable = strTable.Replace("<%Top%>", dRptExtendTextHeight.ToString());
            strChart = strChart.Replace("<%Top%>", (iTableHeight + dRptExtendTextHeight).ToString());
            iRptHeight = iRptHeight + dRptExtendTextHeight;

            sbRpt.Replace("<%ExtendText%>", strExtendText);
            sbRpt.Replace("<%Table%>", strTable);
            sbRpt.Replace("<%Chart%>", strChart);
            sbRpt.Replace("<%ReportHeight%>", iRptHeight.ToString() + "cm");
            sbRpt.Replace("<%ReportWidth%>", iRptWidth.ToString() + "cm");
            
            sbRpt.Replace("<%DataSet%>", this.BuildDataSet(designView, xmlDoc));

            string strFileName = reportFile;
            System.IO.StreamWriter writer = new System.IO.StreamWriter(strFileName, false, Encoding.UTF8);
            writer.Write(sbRpt.ToString());
            writer.Close();

            return strFileName;
        }

        /// <summary>
        /// 构建Table部分
        /// </summary>
        private string BuildTable(ReportDesignView designView, XmlDocument formatXml)
        {
            StringBuilder sbRptTable = new StringBuilder();
            sbRptTable.Append(formatXml.SelectSingleNode("//Table").FirstChild.Value);

            this.dicColumnWidth = new Dictionary<string, decimal>();

            sbRptTable.Replace("<%HeaderCellList%>", this.BuildHeader(designView, formatXml));
            sbRptTable.Replace("<%TableGroups%>", this.BuildTableGroups(designView, formatXml));
            sbRptTable.Replace("<%TableCellList%>", this.BuildTableCells(designView, formatXml));
            sbRptTable.Replace("<%TableDetailVisibility%>", this.BuildTableDetailVisible(designView, formatXml));
            sbRptTable.Replace("<%TableColumnList%>", this.BuildTableColumn(designView, formatXml));

            return sbRptTable.ToString();
        }

        private string BuildExtendText(ReportDesignView designView, XmlDocument formatXml)
        {
            dRptExtendTextHeight = 0;
            if (designView.ExtendText == null || designView.ExtendText.Length == 0 || 
                designView.DataFormats == null || designView.DataFormats.Length == 0)
                return "";
            decimal dRptWidth = 0;
            foreach (decimal dWidthTmp in dicColumnWidth.Values)
                dRptWidth += dWidthTmp;
            decimal dTop = 0;
            StringBuilder sbText = new StringBuilder();
            string strTextFormat = formatXml.SelectSingleNode("//ExtendText").FirstChild.Value;
            for (int i = 0; i < designView.ExtendText.Length; i++)
            {
                RptViewDataFormat dataFormat = null;
                for (int n = 0; n < designView.DataFormats.Length; n++)
                {
                    if (designView.DataFormats[n].FormatID == designView.ExtendText[i].FormatID)
                    {
                        dataFormat = designView.DataFormats[n];
                        break;
                    }
                }
                if (dataFormat == null)
                    continue;
                decimal dHeight = 0.6M + 0.1M * (dataFormat.FontSize - 10);
                decimal dLeft = 0;
                if (dataFormat.TextAlign == "Center")
                    dLeft = (dRptWidth - dataFormat.ColumnWidth) / 2;
                else if (dataFormat.TextAlign == "Right")
                    dLeft = dRptWidth - dataFormat.ColumnWidth;
                string strTextTmp = strTextFormat;
                strTextTmp = strTextTmp.Replace("<%TextBoxName%>", "RptExtText_" + designView.ExtendText[i].Sequence.ToString());
                strTextTmp = strTextTmp.Replace("<%Left%>", dLeft.ToString());
                strTextTmp = strTextTmp.Replace("<%Top%>", dTop.ToString());
                strTextTmp = strTextTmp.Replace("<%Height%>", dHeight.ToString());
                strTextTmp = strTextTmp.Replace("<%Width%>", dataFormat.ColumnWidth.ToString());
                strTextTmp = this.ApplyFontStyle(strTextTmp, designView, dataFormat.FormatID, formatXml, out dataFormat);
                strTextTmp = strTextTmp.Replace("<%TextValue%>", dataFormat.TextExpress);

                sbText.Append(strTextTmp);
                dTop += dHeight;
            }
            dRptExtendTextHeight = dTop;

            return sbText.ToString();
        }

        /// <summary>
        /// 构建Header部分
        /// </summary>
        private string BuildHeader(ReportDesignView designView, XmlDocument formatXml)
        {
            string strHeaderCells = "";
            string strHeaderCell = formatXml.SelectSingleNode("//HeaderCell").FirstChild.Value;
            for (int i = 0; designView.GridColumns != null && i < designView.GridColumns.Length; i++)
            {
                string strColumnName = designView.GridColumns[i].ColumnName;
                string strTmp = strHeaderCell.Replace("<%TextBoxName%>", "table_header_field_" + strColumnName);
                string strGridDataFormatId = "";
                for (int n = 0; designView.GridDataFormats != null && n < designView.GridDataFormats.Length; n++)
                {
                    if (designView.GridDataFormats[n].StyleType == ReportStyleType.Header &&
                        designView.GridDataFormats[n].ColumnName == strColumnName)
                    {
                        strGridDataFormatId = designView.GridDataFormats[n].FormatID;
                        break;
                    }
                }
                RptViewDataFormat dataFormat = null;
                strTmp = this.ApplyFontStyle(strTmp, designView, strGridDataFormatId, formatXml, out dataFormat);
                SaveColumnWidth(strColumnName, dataFormat);
                string strHeaderText = "";
                if (dataFormat == null || dataFormat.TextExpress == null || dataFormat.TextExpress == "")
                    strHeaderText = (designView.DataSourceColumns[strColumnName] != null ? designView.DataSourceColumns[strColumnName].Description : strColumnName);
                else
                    strHeaderText = this.BuildCellTextExpression(designView, dataFormat.TextExpress);
                strTmp = strTmp.Replace("<%HeaderText%>", strHeaderText);
                strTmp = strTmp.Replace("<%SortColumnName%>", "=Fields!" + strColumnName + ".Value");
                strHeaderCells = strHeaderCells + strTmp;
            }
            return strHeaderCells;
        }

        private void SaveColumnWidth(string columnName, RptViewDataFormat dataFormat)
        {
            if (dataFormat != null)
            {
                decimal dWidth = dataFormat.ColumnWidth;
                if (this.dicColumnWidth.ContainsKey(columnName) == false)
                    this.dicColumnWidth.Add(columnName, dWidth);
                else if (this.dicColumnWidth[columnName] < dWidth)
                    this.dicColumnWidth[columnName] = dWidth;
            }
        }

        private string BuildCellTextExpression(ReportDesignView designView, string textExpression)
        {
            if (textExpression == null || textExpression.IndexOf("{") < 0 || textExpression.IndexOf("}") < 0)
                return textExpression;
            if (designView.GridColumns == null)
                return textExpression;

            string strValue = "";
            string[] splitedExpress = textExpression.Split('"');
            for (int i = 0; i < splitedExpress.Length; i++)
            {
                if (splitedExpress[i] == "" && i == splitedExpress.Length - 1)
                    continue;
                if (i % 2 == 0)
                {
                    string strNeedRep = splitedExpress[i];
                    int iPos = 0;
                    while (strNeedRep.IndexOf("{", iPos) >= 0)
                    {
                        int iFrom = strNeedRep.IndexOf("{", iPos);
                        if (iFrom < 0)
                            break;
                        int iTo = strNeedRep.IndexOf("}", iFrom);
                        if (iTo < 0)
                            break;
                        string strColName = strNeedRep.Substring(iFrom + 1, iTo - iFrom - 1);
                        bool bFlag = false;
                        for (int n = 0; n < designView.GridColumns.Length; n++)
                        {
                            if (designView.DataSourceColumns[designView.GridColumns[n].ColumnName].Description == strColName)
                            {
                                strColName = "Fields!" + designView.GridColumns[n].ColumnName;
                                bFlag = true;
                                break;
                            }
                        }
                        if (bFlag == true)
                            strNeedRep = strNeedRep.Substring(0, iFrom) + strColName + ".Value" + strNeedRep.Substring(iTo + 1);
                        
                        iPos = iTo;
                    }
                    strValue += strNeedRep;
                }
                else
                {
                    strValue += "\"" + splitedExpress[i] + "\"";
                }
            }
            return strValue;
        }

        /// <summary>
        /// 将字体格式设置应用到内容
        /// </summary>
        private string ApplyFontStyle(string content, ReportDesignView designView, string formatId, XmlDocument formatXml, out RptViewDataFormat outDataFormat)
        {
            RptViewDataFormat format = null;
            for (int i = 0; designView.DataFormats != null && i < designView.DataFormats.Length; i++)
            {
                if (designView.DataFormats[i].FormatID == formatId)
                {
                    format = designView.DataFormats[i];
                    break;
                }
            }
            if (format == null)
            {
                for (int i = 0; designView.ChartDataFormats != null && i < designView.ChartDataFormats.Length; i++)
                {
                    if (designView.ChartDataFormats[i].FormatID == formatId)
                    {
                        format = designView.ChartDataFormats[i];
                        break;
                    }
                }
            }
            outDataFormat = format;
            if (format == null || formatId == null)
            {
                string strBorderTemp = formatXml.SelectSingleNode("//CellBorderStyle").FirstChild.Value;
                strBorderTemp = strBorderTemp.Replace("<%BorderStyle%>", "Solid");
                content = content.Replace("<%TableCellStyle%>", strBorderTemp);
                return content;
            }

            string strStyle = formatXml.SelectSingleNode("//TableCellStyle").FirstChild.Value;

            strStyle = strStyle.Replace("<%FontFamily%>", format.FontFamily);
            strStyle = strStyle.Replace("<%FontSize%>", Convert.ToInt32(format.FontSize).ToString() + "pt");
            strStyle = strStyle.Replace("<%FontWeight%>", format.FontWeight == "Bold" ? "700" : "400");
            strStyle = strStyle.Replace("<%TextDecoration%>", format.TextDecoration);
            strStyle = strStyle.Replace("<%FontStyle%>", format.FontStyle);
            strStyle = strStyle.Replace("<%BackgroundColor%>", format.BackgroundColor);
            strStyle = strStyle.Replace("<%Color%>", format.Color);
            strStyle = strStyle.Replace("<%Format%>", format.TextFormat);
            string strBorderStyle = "";
            if (format.BorderStyle != "0")
            {
                strBorderStyle = formatXml.SelectSingleNode("//CellBorderStyle").FirstChild.Value;
                strBorderStyle = strBorderStyle.Replace("<%BorderStyle%>", "Solid");
            }
            strStyle = strStyle.Replace("<%BorderStyle%>", strBorderStyle);
            strStyle = strStyle.Replace("<%TextAlign%>", format.TextAlign);
            strStyle = strStyle.Replace("<%VerticalAlign%>", format.VerticalAlign);

            content = content.Replace("<%TableCellStyle%>", strStyle);

            return content;
        }

        /// <summary>
        /// 构建TableGroups部分
        /// </summary>
        private string BuildTableGroups(ReportDesignView designView, XmlDocument formatXml)
        {
            if (designView.GridGroups == null ||
                designView.GridGroups.Length == 0)
                return "";
            else
            {
                string strGroups = formatXml.SelectSingleNode("//TableGroups").FirstChild.Value;
                StringBuilder sbGroup = new StringBuilder();
                for (int i = 0; i < designView.GridGroups.Length; i++)
                {
                    sbGroup.Append(this.BuildTableGroup(designView, i, formatXml));
                }
                strGroups = strGroups.Replace("<%TableGroup%>", sbGroup.ToString());
                return strGroups;
            }
        }

        /// <summary>
        /// 构建单个TableGroup部分
        /// </summary>
        private string BuildTableGroup(ReportDesignView designView, int groupIndex, XmlDocument formatXml)
        {
            RptViewGridGroup group = designView.GridGroups[groupIndex];
            Dictionary<string, RptViewGridGroupTotal> subTotal = designView.GetSubTotalByGroup(group);

            string strGroup = formatXml.SelectSingleNode("//TableGroup").FirstChild.Value;
            StringBuilder sbGroupCells = new StringBuilder();
            string strGroupCell = formatXml.SelectSingleNode("//GroupCell").FirstChild.Value;
            for (int i = 0; designView.GridColumns != null && i < designView.GridColumns.Length; i++)
            {
                string strColumnName = designView.GridColumns[i].ColumnName;
                string strTmp = strGroupCell.Replace("<%TextBoxName%>", "group_field_" + group.ColumnName + "_" + strColumnName);
                string strGridDataFormatId = "";
                for (int n = 0; designView.GridDataFormats != null && n < designView.GridDataFormats.Length; n++)
                {
                    if (designView.GridDataFormats[n].StyleType == ReportStyleType.SubTotal &&
                        designView.GridDataFormats[n].ColumnName == strColumnName &&
                        Convert.ToInt32(designView.GridDataFormats[n].GroupSequence) == group.GroupSequence)
                    {
                        strGridDataFormatId = designView.GridDataFormats[n].FormatID;
                        break;
                    }
                }
                RptViewDataFormat dataFormat = null;
                strTmp = this.ApplyFontStyle(strTmp, designView, strGridDataFormatId, formatXml, out dataFormat);
                SaveColumnWidth(strColumnName, dataFormat);

                if (dataFormat == null || dataFormat.TextExpress == null || dataFormat.TextExpress == "")
                {
                    if (strColumnName == group.ColumnName)
                    {
                        strTmp = strTmp.Replace("<%CellText%>", "=Fields!" + strColumnName + ".Value");
                    }
                    else
                    {
                        string strTotalType = ReportTotalType.Empty;
                        if (subTotal.ContainsKey(strColumnName) == true)
                            strTotalType = subTotal[strColumnName].TotalType;
                        if (strTotalType == ReportTotalType.Sum)
                            strTmp = strTmp.Replace("<%CellText%>", "=Sum(Fields!" + strColumnName + ".Value)");
                        else if (strTotalType == ReportTotalType.Avg)
                            strTmp = strTmp.Replace("<%CellText%>", "=Avg(Fields!" + strColumnName + ".Value)");
                        else if (strTotalType == ReportTotalType.Count)
                            strTmp = strTmp.Replace("<%CellText%>", "=Count(Fields!" + strColumnName + ".Value)");
                        else if (strTotalType == ReportTotalType.Empty)
                            strTmp = strTmp.Replace("<%CellText%>", "");
                        else if (strTotalType == ReportTotalType.Max)
                            strTmp = strTmp.Replace("<%CellText%>", "=Max(Fields!" + strColumnName + ".Value)");
                    }
                }
                else
                {
                    string strCellText = this.BuildCellTextExpression(designView, dataFormat.TextExpress);
                    strTmp = strTmp.Replace("<%CellText%>", strCellText);
                }

                sbGroupCells.Append(strTmp);
            }
            strGroup = strGroup.Replace("<%GroupCellList%>", sbGroupCells.ToString());
            strGroup = strGroup.Replace("<%GroupSortExpression%>", "=Fields!" + group.ColumnName + ".Value");
            strGroup = strGroup.Replace("<%GroupName%>", "group_" + group.ColumnName);
            strGroup = strGroup.Replace("<%GroupExpression%>", "=Fields!" + group.ColumnName + ".Value");
            if (groupIndex == 0)
            {
                strGroup = strGroup.Replace("<%Visibility%>", "");
            }
            else
            {
                string strVisible = formatXml.SelectSingleNode("//Visibility").FirstChild.Value;
                strVisible = strVisible.Replace("<%ToggleItem%>", "group_field_" + designView.GridGroups[groupIndex - 1].ColumnName + "_" + designView.GridGroups[groupIndex - 1].ColumnName);
                strGroup = strGroup.Replace("<%Visibility%>", strVisible);
            }

            return strGroup;
        }

        /// <summary>
        /// 构建明细数据部分
        /// </summary>
        private string BuildTableCells(ReportDesignView designView, XmlDocument formatXml)
        {
            StringBuilder sbCells = new StringBuilder();
            string strTableCellFormat = formatXml.SelectSingleNode("//TableCell").FirstChild.Value;
            for (int i = 0; designView.GridColumns != null && i < designView.GridColumns.Length; i++)
            {
                string strColumnName = designView.GridColumns[i].ColumnName;
                string strTmp = strTableCellFormat;
                strTmp = strTmp.Replace("<%TextBoxName%>", "table_cell_" + strColumnName);
                bool bIsGroupField = false;
                for (int n = 0; designView.GridGroups != null && n < designView.GridGroups.Length; n++)
                {
                    if (designView.GridGroups[n].ColumnName == strColumnName)
                    {
                        bIsGroupField = true;
                        break;
                    }
                }
                if (bIsGroupField == false)
                {
                    string strGridDataFormatId = "";
                    for (int n = 0; designView.GridDataFormats != null && n < designView.GridDataFormats.Length; n++)
                    {
                        if (designView.GridDataFormats[n].StyleType == ReportStyleType.Item &&
                            designView.GridDataFormats[n].ColumnName == strColumnName)
                        {
                            strGridDataFormatId = designView.GridDataFormats[n].FormatID;
                            break;
                        }
                    }
                    RptViewDataFormat dataFormat = null;
                    strTmp = this.ApplyFontStyle(strTmp, designView, strGridDataFormatId, formatXml, out dataFormat);
                    SaveColumnWidth(strColumnName, dataFormat);

                    if (dataFormat == null || dataFormat.TextExpress == null || dataFormat.TextExpress == "")
                    {
                        if (strColumnName.ToLower().Equals("rownum"))
                        {
                            strTmp = strTmp.Replace("<%TextBoxValue%>", "=RowNumber(Nothing)");
                        }
                        else
                        {
                            strTmp = strTmp.Replace("<%TextBoxValue%>", "=Fields!" + strColumnName + ".Value");
                        }
                    }
                    else
                        strTmp = strTmp.Replace("<%TextBoxValue%>", this.BuildCellTextExpression(designView, dataFormat.TextExpress));
                }
                else
                {
                    strTmp = strTmp.Replace("<%TextBoxValue%>", "");
                    RptViewDataFormat dataFormat = null;
                    strTmp = this.ApplyFontStyle(strTmp, designView, null, formatXml, out dataFormat);
                    SaveColumnWidth(strColumnName, dataFormat);
                }
            
                sbCells.Append(strTmp);
            }
            return sbCells.ToString();
        }

        /// <summary>
        /// 构建明细数据隐藏部分
        /// </summary>
        private string BuildTableDetailVisible(ReportDesignView designView, XmlDocument formatXml)
        {
            if (designView.GridGroups == null ||
                designView.GridGroups.Length == 0)
                return "";
            else
            {
                string strVisible = formatXml.SelectSingleNode("//Visibility").FirstChild.Value;
                strVisible = strVisible.Replace("<%ToggleItem%>", "group_field_" + designView.GridGroups[designView.GridGroups.Length - 1].ColumnName + "_" + designView.GridGroups[designView.GridGroups.Length - 1].ColumnName);
                return strVisible;
            }
        }

        /// <summary>
        /// 构建Column设置
        /// </summary>
        private string BuildTableColumn(ReportDesignView designView, XmlDocument formatXml)
        {
            string strTableColumns = "";
            string strTableColumn = formatXml.SelectSingleNode("//TableColumn").FirstChild.Value;
            for (int i = 0; designView.GridColumns != null && i < designView.GridColumns.Length; i++)
            {
                string strTmp = strTableColumn;
                if (this.dicColumnWidth.ContainsKey(designView.GridColumns[i].ColumnName) == true)
                    strTmp = strTmp.Replace("<%Width%>", this.dicColumnWidth[designView.GridColumns[i].ColumnName].ToString());
                else
                    strTmp = strTmp.Replace("<%Width%>", "2.5");
                strTableColumns = strTableColumns + strTmp;
            }
            return strTableColumns;
        }

        /// <summary>
        /// 构建DataSet
        /// </summary>
        private string BuildDataSet(ReportDesignView designView, XmlDocument formatXml)
        {
            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            RptViewDataSourceColumn[] columnObjs = rptFacade.GetRptViewDataSourceColumnByDataSourceId(Convert.ToInt32(designView.DesignMain.DataSourceID));
            Dictionary<string, RptViewDataSourceColumn> columns = new Dictionary<string, RptViewDataSourceColumn>();
            for (int i = 0; i < columnObjs.Length; i++)
            {
                columns.Add(columnObjs[i].ColumnName, columnObjs[i]);
            }

            string strDataSet = formatXml.SelectSingleNode("//DataSet").FirstChild.Value;
            string strFields = "";
            string strField = formatXml.SelectSingleNode("//DataSetField").FirstChild.Value;
            for (int i = 0; designView.GridColumns != null && i < designView.GridColumns.Length; i++)
            {
                string strTmp = strField.Replace("<%Name%>", designView.GridColumns[i].ColumnName);
                strTmp = strTmp.Replace("<%Type%>", columns[designView.GridColumns[i].ColumnName].DataType);
                strFields = strFields + strTmp;
            }
            if (designView.GridColumns == null || designView.GridColumns.Length == 0)
            {
                for (int i = 0; i < columnObjs.Length; i++)
                {
                    string strTmp = strField.Replace("<%Name%>", columnObjs[i].ColumnName);
                    strTmp = strTmp.Replace("<%Type%>", columnObjs[i].DataType);
                    strFields = strFields + strTmp;
                }
            }
            strDataSet = strDataSet.Replace("<%FieldList%>", strFields);
            strDataSet = strDataSet.Replace("<%SQLQuery%>", "");

            return strDataSet;
        }

        /// <summary>
        /// 构建Chart
        /// </summary>
        private string BuildChart(ReportDesignView designView, XmlDocument formatXml)
        {
            StringBuilder sbChartTotal = new StringBuilder();
            for (int i = 0; designView.ChartMains != null && i < designView.ChartMains.Length; i++)
            {
                RptViewChartMain chartMain = designView.ChartMains[i];
                StringBuilder sbChart = new StringBuilder();
                sbChart.Append(formatXml.SelectSingleNode("//Chart").FirstChild.Value);
                sbChart.Replace("<%ChartName%>", "CHART" + chartMain.ChartSequence.ToString());
                sbChart.Replace("<%ChartType%>", chartMain.ChartType);
                sbChart.Replace("<%ShowLegend%>", FormatHelper.StringToBoolean(chartMain.ShowLegend).ToString().ToLower());
                sbChart.Replace("<%SeriesGroup%>", this.BuildChartSeries(designView, chartMain, formatXml));
                sbChart.Replace("<%CategoryGroupingList%>", this.BuildChartCategory(designView, chartMain, formatXml));
                sbChart.Replace("<%ChartData%>", this.BuildChartData(designView, chartMain, formatXml));

                sbChartTotal.Append(sbChart.ToString());
            }

            return sbChartTotal.ToString();
        }

        /// <summary>
        /// 构建Chart Series
        /// </summary>
        private string BuildChartSeries(ReportDesignView designView, RptViewChartMain chartMain, XmlDocument formatXml)
        {
            StringBuilder sbSeries = new StringBuilder();
            // Dynamic Series
            string strSeriesDyanmic = formatXml.SelectSingleNode("//SeriesGroupDynamic").FirstChild.Value;
            for (int i = 0; designView.ChartSeries != null && i < designView.ChartSeries.Length; i++)
            {
                if (designView.ChartSeries[i].ChartSequence == chartMain.ChartSequence)
                {
                    string strColumnName = designView.ChartSeries[i].ColumnName;
                    string strTmp = strSeriesDyanmic;
                    strTmp = strTmp.Replace("<%SeriesName%>", "chart_" + chartMain.ChartSequence.ToString() + "_series_" + strColumnName);
                    strTmp = strTmp.Replace("<%GroupExpression%>", string.Format("=Fields!{0}.Value", strColumnName));
                    strTmp = strTmp.Replace("<%Label%>", string.Format("=Fields!{0}.Value", strColumnName));
                    sbSeries.Append(strTmp);
                }
            }
            if (designView.ChartSeries == null || designView.ChartSeries.Length == 0)
            {
                // Static Series
                string strSeriesStaticMemberList = "";
                string strSeriesStaticMember = formatXml.SelectSingleNode("//SeriesGroupStaticMember").FirstChild.Value;
                for (int i = 0; designView.ChartDatas != null && i < designView.ChartDatas.Length; i++)
                {
                    if (designView.ChartDatas[i].ChartSequence == chartMain.ChartSequence)
                    {
                        string strTmp = strSeriesStaticMember;
                        strTmp = strTmp.Replace("<%Label%>", (designView.DataSourceColumns[designView.ChartDatas[i].ColumnName] != null ? designView.DataSourceColumns[designView.ChartDatas[i].ColumnName].Description : designView.ChartDatas[i].ColumnName));
                        strSeriesStaticMemberList = strSeriesStaticMemberList + strTmp;
                    }
                }
                string strSeriesStatic = formatXml.SelectSingleNode("//SeriesGroupStatic").FirstChild.Value;
                strSeriesStatic = strSeriesStatic.Replace("<%SeriesGroupStaticMember%>", strSeriesStaticMemberList);

                sbSeries.Append(strSeriesStatic);
            }

            return sbSeries.ToString();
        }

        /// <summary>
        /// 构建Chart Category
        /// </summary>
        private string BuildChartCategory(ReportDesignView designView, RptViewChartMain chartMain, XmlDocument formatXml)
        {
            StringBuilder sbCategory = new StringBuilder();
            string strCategoryFormat = formatXml.SelectSingleNode("//CategoryGrouping").FirstChild.Value;
            for (int i = 0; designView.ChartCategories != null && i < designView.ChartCategories.Length; i++)
            {
                if (designView.ChartCategories[i].ChartSequence == chartMain.ChartSequence)
                {
                    string strColumnName = designView.ChartCategories[i].ColumnName;
                    string strTmp = strCategoryFormat;
                    strTmp = strTmp.Replace("<%CategoryName%>", "chart_" + chartMain.ChartSequence.ToString() + "_category_" + strColumnName);
                    strTmp = strTmp.Replace("<%GroupExpression%>", "=Fields!" + strColumnName + ".Value");
                    strTmp = strTmp.Replace("<%Label%>", "=Fields!" + strColumnName + ".Value");
                    sbCategory.Append(strTmp);
                }
            }
            if (sbCategory.Length == 0)
            {
                return "";
            }
            string strCategoryListFormat = formatXml.SelectSingleNode("//CategoryGroupingList").FirstChild.Value;
            strCategoryListFormat = strCategoryListFormat.Replace("<%CategoryGrouping%>", sbCategory.ToString());
            return strCategoryListFormat;
        }

        /// <summary>
        /// 构建Chart Data
        /// </summary>
        private string BuildChartData(ReportDesignView designView, RptViewChartMain chartMain, XmlDocument formatXml)
        {
            StringBuilder sbData = new StringBuilder();
            string strDataFormat = formatXml.SelectSingleNode("//ChartData").FirstChild.Value;
            for (int i = 0; designView.ChartDatas != null && i < designView.ChartDatas.Length; i++)
            {
                if (designView.ChartDatas[i].ChartSequence == chartMain.ChartSequence)
                {
                    RptViewChartData chartData = designView.ChartDatas[i];
                    string strColumnName = designView.ChartDatas[i].ColumnName;
                    string strTmp = strDataFormat;
                    string strValue = "";
                    if (chartData.TotalType == ReportTotalType.Sum)
                    {
                        strValue = "=Sum(Fields!" + strColumnName + ".Value)";
                    }
                    else if (chartData.TotalType == ReportTotalType.Avg)
                    {
                        strValue = "=Avg(Fields!" + strColumnName + ".Value)";
                    }
                    else if (chartData.TotalType == ReportTotalType.Count)
                    {
                        strValue = "=Count(Fields!" + strColumnName + ".Value)";
                    }
                    strTmp = strTmp.Replace("<%Value%>", strValue);

                    string strLabel = "";
                    if (FormatHelper.StringToBoolean(chartMain.ShowLabel) == true)
                    {
                        strLabel = formatXml.SelectSingleNode("//ChartDataLabelStyle").FirstChild.Value;
                        RptViewDataFormat dataFormat = null;
                        strLabel = this.ApplyFontStyle(strLabel, designView, chartMain.LabelFormatID, formatXml, out dataFormat);
                    }
                    strTmp = strTmp.Replace("<%DataLabelStyle%>", strLabel);

                    string strMarker = "";
                    if (FormatHelper.StringToBoolean(chartMain.ShowMarker) == true)
                    {
                        strMarker = formatXml.SelectSingleNode("//ChartDataMarkerType").FirstChild.Value;
                        strMarker = strMarker.Replace("<%MarkerType%>", chartMain.MarkerType);
                    }
                    strTmp = strTmp.Replace("<%MarkerType%>", strMarker);
                    sbData.Append(strTmp);
                }
            }
            return sbData.ToString();
        }

    }
}
