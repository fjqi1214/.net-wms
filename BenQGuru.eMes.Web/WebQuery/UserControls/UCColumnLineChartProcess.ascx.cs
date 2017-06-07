using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BenQGuru.eMES.WebQuery;
using Infragistics.WebUI.UltraWebChart;


using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.UltraChart.Resources;
using Infragistics.UltraChart.Data.Series;
using Infragistics.UltraChart.Core.Layers;
using Infragistics.UltraChart.Core;
using Infragistics.UltraChart.Core.ColorModel;
using Infragistics.UltraChart.Data;
using Infragistics.UltraChart.Core.Primitives;


namespace BenQGuru.eMES.Web.WebQuery.UserControls
{
    public partial class UCColumnLineChartProcess : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private bool m_DataType = false;
        public bool DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
        }

        private int m_Width = 0;
        public int Width
        {
            get { return m_Width; }
            set { m_Width = value; }
        }

        private int m_Height = 0;
        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

        private string m_ChartTextFormatString = null;
        public string ChartTextFormatString
        {
            get { return m_ChartTextFormatString; }
            set { m_ChartTextFormatString = value; }
        }

        private string m_LineChartTextFormatString = null;
        public string LineChartTextFormatString
        {
            get { return m_LineChartTextFormatString; }
            set { m_LineChartTextFormatString = value; }
        }

        private string m_ChartGroupByString = null;
        public string ChartGroupByString
        {
            get { return m_ChartGroupByString; }
            set { m_ChartGroupByString = value; }
        }

        private string m_YLabelFormatString = null;
        public string YLabelFormatString
        {
            get { return m_YLabelFormatString; }
            set { m_YLabelFormatString = value; }
        }

        private string m_Y2LabelFormatString = null;
        public string Y2LabelFormatString
        {
            get { return m_Y2LabelFormatString; }
            set { m_Y2LabelFormatString = value; }
        }          

        private object m_ColumnDataSource;
        public object ColumnDataSource
        {
            get { return m_ColumnDataSource; }
            set { m_ColumnDataSource = value; }
        }

        private object m_LineDataSource;
        public object LineDataSource
        {
            get { return m_LineDataSource; }
            set { m_LineDataSource = value; }
        }

        private int m_Y2RangeMax = 100;
        public int Y2RangeMax
        {
            get { return m_Y2RangeMax; }
            set { m_Y2RangeMax = value; }
        }   

        private NewReportDomainObject[] m_DataSource;
        public NewReportDomainObject[] DataSource
        {
            get { return m_DataSource; }
            set { m_DataSource = value; }
        }

        /// <summary>
        /// 绑定Chart图
        /// </summary>
        public void DataBind()
        {
            DefineChartStyle();

            DataTable data = ConvertDataTableNew();
            columnLineChart.DataSource = data;
            columnLineChart.DataBind();
        }

        /// <summary>
        /// 绑定Chart图
        /// </summary>
        public void DataBindTable()
        {
            this.columnLineChart.ColumnLineChart.ColumnData.ZeroAligned = DataType;
            this.columnLineChart.ColumnLineChart.LineData.ZeroAligned = DataType;
            if (m_Width != 0)
            {
                columnLineChart.Width = m_Width;
            }

            if (m_Height != 0)
            {
                columnLineChart.Height = m_Height;
            }
            this.columnLineChart.Data.SwapRowsAndColumns = true;
            this.columnLineChart.Axis.Y2.RangeType = Infragistics.UltraChart.Shared.Styles.AxisRangeType.Custom;
            this.columnLineChart.Axis.Y2.RangeMax = Y2RangeMax;
            this.columnLineChart.Axis.Y.Visible = true;
            this.columnLineChart.Axis.Y2.Visible = true;

            this.columnLineChart.Axis.Y.Labels.ItemFormatString = YLabelFormatString;
            this.columnLineChart.Axis.Y2.Labels.ItemFormatString = Y2LabelFormatString;

            this.columnLineChart.ColumnLineChart.Column.ChartText[0].ItemFormatString = ChartTextFormatString;
            this.columnLineChart.ColumnLineChart.Line.ChartText[0].ItemFormatString = LineChartTextFormatString;

            this.columnLineChart.ColumnLineChart.LineData.DataSource = LineDataSource;            
            this.columnLineChart.ColumnLineChart.LineData.DataBind();                     
            this.columnLineChart.ColumnLineChart.ColumnData.DataSource  = ColumnDataSource;
            this.columnLineChart.ColumnLineChart.ColumnData.DataBind();  
            
        }


        /// <summary>
        /// 绑定Chart图
        /// </summary>
        public void DataBindQty(string planQtyName)
        {
            DefineChartStyle();

            DataTable data = ConvertDataTableNewQty(planQtyName);
            columnLineChart.ColumnLineChart.ColumnData.DataSource = data;
            columnLineChart.ColumnLineChart.LineData.DataSource = ConvertDataTableActiveRate(planQtyName);
            columnLineChart.DataBind();
        }

        /// <summary>
        /// 当X轴的值等于EAttribute1的值时，绑定Chart图
        /// </summary>
        public void DataBindOQC()
        {
            DefineChartStyle();

            DataTable data = ConvertDataTableOQC();
            columnLineChart.DataSource = data;
            columnLineChart.DataBind();
        }

        /// <summary>
        /// 定义图表样式
        /// </summary>
        private void DefineChartStyle()
        {
            //定义X轴的Extent
            if (DataSource.Length > 0 && DataSource[DataSource.Length - 1].PeriodCode != null)
            {
                //this.columnLineChart.Axis.X.Extent = DataSource[DataSource.Length - 1].PeriodCode.Length * 10;
                this.columnLineChart.Axis.X.Extent = System.Text.Encoding.GetEncoding("GB2312").GetByteCount(DataSource[DataSource.Length - 1].PeriodCode) * 5;
                //this.columnLineChart.Axis.Y.Extent = 32;

                this.columnLineChart.Axis.Y.Visible = true;
                this.columnLineChart.Axis.Y2.Visible = true;
             
                this.columnLineChart.Axis.Y.Labels.ItemFormatString = YLabelFormatString;
                this.columnLineChart.Axis.Y2.Labels.ItemFormatString = Y2LabelFormatString;
                
                this.columnLineChart.ColumnLineChart.Column.ChartText[0].ItemFormatString = ChartTextFormatString;
                this.columnLineChart.ColumnLineChart.Line.ChartText[0].ItemFormatString = LineChartTextFormatString;
                //this.columnLineChart.ColumnLineChart.ColumnData.DataSource 
                this.columnLineChart.Data.ZeroAligned = DataType;
                //this.columnLineChart.ColorModel.ColorBegin = System.Drawing.Color.Green;
                //this.columnLineChart.ColorModel.ColorEnd = System.Drawing.Color.Green;
                //this.columnLineChart.ColumnLineChart.Line.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dash;

                if (m_Width != 0)
                {
                    columnLineChart.Width = m_Width;
                }

                if (m_Height != 0)
                {
                    columnLineChart.Height = m_Height;
                }
            }
        }

        /// <summary>
        ///当X轴的值等于EAttribute1的值时，将数据转换为相对应X轴、Y轴的DataTable 
        /// </summary>
        public DataTable ConvertDataTableOQC()
        {
            DataTable dt = new DataTable();

            //此列用来显示有变描述
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            ArrayList columnName = new ArrayList();

            foreach (NewReportDomainObject domainObject in DataSource) //X轴
            {
                if (!columnName.Contains(domainObject.PeriodCode))
                {
                    columnName.Add(domainObject.PeriodCode);
                }
            }

            //排序名称
            columnName.Sort();

            for (int i = 0; i < columnName.Count; i++)
            {
                dt.Columns.Add(new DataColumn(columnName[i].ToString(), typeof(double)));
            }

            Hashtable nameColumn = new Hashtable();
            string name = string.Empty;

            #region //Y轴
            string[] groupColumns = m_ChartGroupByString.Split(',');
            ArrayList nameList = new ArrayList();
            foreach (NewReportDomainObject domainObject in DataSource)
            {
                name = string.Empty;
                if (groupColumns.Length > 0)
                {
                    for (int i = 0; i < groupColumns.Length; i++)
                    {
                        if (groupColumns[i].Trim().ToLower() == "sscode")
                        {
                            name += domainObject.SSCode + "-";
                        }

                        else if (groupColumns[i].Trim().ToLower() == "opcode")
                        {
                            name += domainObject.OPCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "rescode")
                        {
                            name += domainObject.ResCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "bigsscode")
                        {
                            name += domainObject.BigSSCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "segcode")
                        {
                            name += domainObject.SegCode + "-";
                        }

                        else if (groupColumns[i].Trim().ToLower() == "faccode")
                        {
                            name += domainObject.FacCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "momemo")
                        {
                            name += domainObject.MOMemo + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "newmass")
                        {
                            name += domainObject.NewMass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "itemcode")
                        {
                            name += domainObject.ItemCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mmodelcode")
                        {
                            name += domainObject.MaterialModelCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mmachinetype")
                        {
                            name += domainObject.MaterialMachineType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mexportimport")
                        {
                            name += domainObject.MaterialExportImport + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mocode")
                        {
                            name += domainObject.MOCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "firstclass")
                        {
                            name += domainObject.FirstClass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "secondclass")
                        {
                            name += domainObject.SecondClass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "thirdclass")
                        {
                            name += domainObject.SSCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "lotno")
                        {
                            name += domainObject.LotNo + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "productiontype")
                        {
                            name += domainObject.ProductionType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "oqclottype")
                        {
                            name += domainObject.OQCLotType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "inspectorandname")
                        {
                            name += domainObject.InspectorAndName + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "iqclineitemtype")
                        {
                            name += domainObject.IQCLineItemType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "iqcitemtype")
                        {
                            name += domainObject.IQCItemType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "rohs")
                        {
                            name += domainObject.Rohs + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "concessionstatus")
                        {
                            name += domainObject.ConcessionStatus + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "vendorcode")
                        {
                            name += domainObject.VendorCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "materialcode")
                        {
                            name += domainObject.MaterialCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "tpcode")
                        {
                            name += domainObject.PeriodCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToUpper() == "shiftcode")
                        {
                            name += domainObject.ShiftCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "shiftday")
                        {
                            name += domainObject.ShiftDay + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "dweek")
                        {
                            name += domainObject.Week + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "dmonth")
                        {
                            name += domainObject.Month + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "year")
                        {
                            name += domainObject.Year + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "goodsemigood")
                        {
                            name += domainObject.GoodSemiGood + "-";
                        }
                    }
                    if (name == string.Empty)
                    {
                        name += "系列";
                    }
                    else
                    {
                        name = name.Substring(0, name.Length - 1);
                    }
                }

                if (!nameColumn.Contains(name))
                {
                    nameColumn.Add(name, name);
                }

                nameList.Add(name);
            }

            #endregion

            foreach (System.Collections.DictionaryEntry objDE in nameColumn)
            {
                DataRow dr = dt.NewRow();
                dr[0] = objDE.Value.ToString();
                for (int i = 1; i < columnName.Count + 1; i++)
                {
                    dr[i] = 0;
                }
                for (int m = 0; m < DataSource.Length; m++)
                {
                    NewReportDomainObject domainObject = DataSource[m] as NewReportDomainObject;

                    if (objDE.Value.ToString() == nameList[m].ToString())
                    {
                        dr[domainObject.PeriodCode] = domainObject.TempValue;
                    }
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 将数据转换为相对应X轴、Y轴的DataTable
        /// </summary>
        public DataTable ConvertDataTableNew()
        {
            DataTable dt = new DataTable();

            //此列用来显示时间
            dt.Columns.Add(new DataColumn("Time", typeof(string)));
            ArrayList columnNameList = new ArrayList();

            #region //其他ColunmName
            string name = string.Empty;
            string[] groupColumns = m_ChartGroupByString.Split(',');
            ArrayList allNameList = new ArrayList();
            foreach (NewReportDomainObject domainObject in DataSource)
            {
                name = string.Empty;
                #region
                if (groupColumns.Length > 0)
                {
                    for (int i = 0; i < groupColumns.Length; i++)
                    {
                        if (groupColumns[i].Trim().ToLower() == "sscode")
                        {
                            name += domainObject.SSCode + "-";
                        }

                        else if (groupColumns[i].Trim().ToLower() == "opcode")
                        {
                            name += domainObject.OPCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "rescode")
                        {
                            name += domainObject.ResCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "bigsscode")
                        {
                            name += domainObject.BigSSCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "segcode")
                        {
                            name += domainObject.SegCode + "-";
                        }

                        else if (groupColumns[i].Trim().ToLower() == "faccode")
                        {
                            name += domainObject.FacCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "momemo")
                        {
                            name += domainObject.MOMemo + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "newmass")
                        {
                            name += domainObject.NewMass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "itemcode")
                        {
                            name += domainObject.ItemCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mmodelcode" || groupColumns[i].Trim().ToLower() == "materialmodelcode")
                        {
                            name += domainObject.MaterialModelCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mmachinetype" || groupColumns[i].Trim().ToLower() == "materialmachinetype")
                        {
                            name += domainObject.MaterialMachineType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mexportimport" || groupColumns[i].Trim().ToLower() == "materialexportimport")
                        {
                            name += domainObject.MaterialExportImport + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mocode")
                        {
                            name += domainObject.MOCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "firstclass")
                        {
                            name += domainObject.FirstClass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "secondclass")
                        {
                            name += domainObject.SecondClass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "thirdclass")
                        {
                            name += domainObject.SSCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "lotno")
                        {
                            name += domainObject.LotNo + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "productiontype")
                        {
                            name += domainObject.ProductionType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "oqclottype")
                        {
                            name += domainObject.OQCLotType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "inspectorandname")
                        {
                            name += domainObject.InspectorAndName + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "iqclineitemtype")
                        {
                            name += domainObject.IQCLineItemType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "iqcitemtype")
                        {
                            name += domainObject.IQCItemType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "rohs")
                        {
                            name += domainObject.Rohs + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "concessionstatus")
                        {
                            name += domainObject.ConcessionStatus + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "vendorcode")
                        {
                            name += domainObject.VendorCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "materialcode")
                        {
                            name += domainObject.MaterialCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "tpcode")
                        {
                            name += domainObject.PeriodCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToUpper() == "shiftcode")
                        {
                            name += domainObject.ShiftCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "shiftday")
                        {
                            name += domainObject.ShiftDay + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "dweek")
                        {
                            name += domainObject.Week + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "dmonth")
                        {
                            name += domainObject.Month + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "year")
                        {
                            name += domainObject.Year + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "goodsemigood")
                        {
                            name += domainObject.GoodSemiGood + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "crewcode")
                        {
                            name += domainObject.CrewCode + "-";
                        }
                    }
                #endregion
                    name += domainObject.EAttribute1;
                }

                if (!columnNameList.Contains(name))
                {
                    columnNameList.Add(name);
                }

                allNameList.Add(name);
            }

            #endregion

            //排序名称
            columnNameList.Sort();

            for (int i = 0; i < columnNameList.Count; i++)
            {
                dt.Columns.Add(new DataColumn(columnNameList[i].ToString(), typeof(double)));
            }

            ArrayList columnValueList = new ArrayList();
            foreach (NewReportDomainObject domainObject in DataSource) //Y轴
            {
                if (!columnValueList.Contains(domainObject.PeriodCode))
                {
                    columnValueList.Add(domainObject.PeriodCode);
                }
            }

            columnValueList.Sort();

            foreach (string value in columnValueList)
            {
                DataRow dr = dt.NewRow();
                dr[0] = value;
                for (int i = 1; i < columnNameList.Count + 1; i++)
                {
                    dr[i] = 0;
                }
                for (int m = 0; m < DataSource.Length; m++)
                {
                    NewReportDomainObject domainObject = DataSource[m] as NewReportDomainObject;

                    if (value == domainObject.PeriodCode)
                    {
                        dr[allNameList[m].ToString()] = domainObject.TempValue;

                    }
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }


        /// <summary>
        /// 将数据转换为相对应X轴、Y轴的DataTable
        /// </summary>
        public DataTable ConvertDataTableNewQty(string planQtyName)
        {
            DataTable dt = new DataTable();

            //此列用来显示时间
            dt.Columns.Add(new DataColumn("Time", typeof(string)));
            ArrayList columnNameList = new ArrayList();
            ArrayList ColumnQtyList = new ArrayList();

            #region //其他ColunmName
            string name = string.Empty;
            string qtyName = string.Empty;
            string[] groupColumns = m_ChartGroupByString.Split(',');
            ArrayList allNameList = new ArrayList();
            ArrayList allQtyNameList = new ArrayList();
            foreach (NewReportDomainObject domainObject in DataSource)
            {
                name = string.Empty;
                qtyName = string.Empty;
                #region
                if (groupColumns.Length > 0)
                {
                    for (int i = 0; i < groupColumns.Length; i++)
                    {
                        if (groupColumns[i].Trim().ToLower() == "sscode")
                        {
                            name += domainObject.SSCode + "-";
                            qtyName += domainObject.SSCode + "-";
                        }

                        else if (groupColumns[i].Trim().ToLower() == "opcode")
                        {
                            name += domainObject.OPCode + "-";
                            qtyName += domainObject.OPCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "rescode")
                        {
                            name += domainObject.ResCode + "-";
                            qtyName += domainObject.ResCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "bigsscode")
                        {
                            name += domainObject.BigSSCode + "-";
                            name += domainObject.BigSSCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "segcode")
                        {
                            name += domainObject.SegCode + "-";
                            qtyName += domainObject.SegCode + "-";
                        }

                        else if (groupColumns[i].Trim().ToLower() == "faccode")
                        {
                            name += domainObject.FacCode + "-";
                            qtyName += domainObject.FacCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "momemo")
                        {
                            name += domainObject.MOMemo + "-";
                            qtyName += domainObject.MOMemo + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "newmass")
                        {
                            name += domainObject.NewMass + "-";
                            qtyName += domainObject.NewMass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "itemcode")
                        {
                            name += domainObject.ItemCode + "-";
                            qtyName += domainObject.ItemCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mmodelcode" || groupColumns[i].Trim().ToLower() == "materialmodelcode")
                        {
                            name += domainObject.MaterialModelCode + "-";
                            qtyName += domainObject.MaterialModelCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mmachinetype" || groupColumns[i].Trim().ToLower() == "materialmachinetype")
                        {
                            name += domainObject.MaterialMachineType + "-";
                            qtyName += domainObject.MaterialMachineType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mexportimport" || groupColumns[i].Trim().ToLower() == "materialexportimport")
                        {
                            name += domainObject.MaterialExportImport + "-";
                            qtyName += domainObject.MaterialExportImport + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mocode")
                        {
                            name += domainObject.MOCode + "-";
                            qtyName += domainObject.MOCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "firstclass")
                        {
                            name += domainObject.FirstClass + "-";
                            qtyName += domainObject.FirstClass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "secondclass")
                        {
                            name += domainObject.SecondClass + "-";
                            qtyName += domainObject.SecondClass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "thirdclass")
                        {
                            name += domainObject.SSCode + "-";
                            qtyName += domainObject.SSCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "lotno")
                        {
                            name += domainObject.LotNo + "-";
                            qtyName += domainObject.LotNo + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "productiontype")
                        {
                            name += domainObject.ProductionType + "-";
                            qtyName += domainObject.ProductionType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "oqclottype")
                        {
                            name += domainObject.OQCLotType + "-";
                            qtyName += domainObject.OQCLotType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "inspectorandname")
                        {
                            name += domainObject.InspectorAndName + "-";
                            qtyName += domainObject.InspectorAndName + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "iqclineitemtype")
                        {
                            name += domainObject.IQCLineItemType + "-";
                            qtyName += domainObject.IQCLineItemType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "iqcitemtype")
                        {
                            name += domainObject.IQCItemType + "-";
                            qtyName += domainObject.IQCItemType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "rohs")
                        {
                            name += domainObject.Rohs + "-";
                            qtyName += domainObject.Rohs + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "concessionstatus")
                        {
                            name += domainObject.ConcessionStatus + "-";
                            qtyName += domainObject.ConcessionStatus + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "vendorcode")
                        {
                            name += domainObject.VendorCode + "-";
                            qtyName += domainObject.VendorCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "materialcode")
                        {
                            name += domainObject.MaterialCode + "-";
                            qtyName += domainObject.MaterialCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "tpcode")
                        {
                            name += domainObject.PeriodCode + "-";
                            qtyName += domainObject.PeriodCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToUpper() == "shiftcode")
                        {
                            name += domainObject.ShiftCode + "-";
                            qtyName += domainObject.ShiftCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "shiftday")
                        {
                            name += domainObject.ShiftDay + "-";
                            qtyName += domainObject.ShiftDay + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "dweek")
                        {
                            name += domainObject.Week + "-";
                            qtyName += domainObject.Week + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "dmonth")
                        {
                            name += domainObject.Month + "-";
                            qtyName += domainObject.Month + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "year")
                        {
                            name += domainObject.Year + "-";
                            qtyName += domainObject.Year + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "goodsemigood")
                        {
                            name += domainObject.GoodSemiGood + "-";
                            qtyName += domainObject.GoodSemiGood + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "crewcode")
                        {
                            name += domainObject.CrewCode + "-";
                            qtyName += domainObject.CrewCode + "-";
                        }

                        else if (groupColumns[i].Trim().ToLower() == "projectname")
                        {
                            name += domainObject.ProjectCode + "-";
                            qtyName += domainObject.ProjectCode + "-";
                        }
                    }
                #endregion
                    name += domainObject.EAttribute1;
                    qtyName += "计划";

                }

                if (!columnNameList.Contains(name))
                {
                    columnNameList.Add(name);
                }
                if (!ColumnQtyList.Contains(qtyName))
                {
                    ColumnQtyList.Add(qtyName);
                }



                allNameList.Add(name);
                allQtyNameList.Add(qtyName);
            }

            #endregion

            //排序名称
            columnNameList.Sort();
            ColumnQtyList.Sort();

            //dt.Columns.Add(new DataColumn(planQtyName, typeof(double)));

            for (int i = 0; i < ColumnQtyList.Count; i++)
            {
                dt.Columns.Add(new DataColumn(ColumnQtyList[i].ToString(), typeof(double)));
                dt.Columns.Add(new DataColumn(columnNameList[i].ToString(), typeof(double)));
            }
            
            //for (int i = 0; i < columnNameList.Count; i++)
            //{
            //    dt.Columns.Add(new DataColumn(columnNameList[i].ToString(), typeof(double)));
            //}
            ArrayList columnValueList = new ArrayList();
            foreach (NewReportDomainObject domainObject in DataSource) //Y轴
            {
                if (!columnValueList.Contains(domainObject.PeriodCode))
                {
                    columnValueList.Add(domainObject.PeriodCode);
                }
            }
            columnValueList.Sort();

            foreach (string value in columnValueList)
            {
                DataRow dr = dt.NewRow();
                dr[0] = value;
                for (int i = 1; i < columnNameList.Count + ColumnQtyList.Count +1; i++)
                {
                    dr[i] = 0;
                }
                for (int m = 0; m < DataSource.Length; m++)
                {
                    NewReportDomainObject domainObject = DataSource[m] as NewReportDomainObject;

                    if (value == domainObject.PeriodCode)
                    {
                        //dr[planQtyName] = domainObject.PlanQty;
                        dr[allQtyNameList[m].ToString()] = domainObject.PlanQty;
                        dr[allNameList[m].ToString()] = domainObject.TempValue;                                               
                    }
                }



                dt.Rows.Add(dr);
            }
            return dt;
        }


        /// <summary>
        /// 将数据转换为相对应X轴、Y轴的DataTable
        /// </summary>
        public DataTable ConvertDataTableActiveRate(string planQtyName)
        {
            DataTable dt = new DataTable();

            //此列用来显示有变描述
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            ArrayList columnName = new ArrayList();

            foreach (NewReportDomainObject domainObject in DataSource) //X轴
            {
                if (!columnName.Contains(domainObject.PeriodCode))
                {
                    columnName.Add(domainObject.PeriodCode);
                }
            }

            //排序名称
            columnName.Sort();

            for (int i = 0; i < columnName.Count; i++)
            {
                if (columnName[i].ToString().Trim() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn(" ", typeof(double)));
                }
                else
                {
                    dt.Columns.Add(new DataColumn(columnName[i].ToString(), typeof(double)));
                }
            }

            Hashtable nameColumn = new Hashtable();
            string name = string.Empty;
            //Y轴
            string[] groupColumns = m_ChartGroupByString.Split(',');
            ArrayList nameList = new ArrayList();
            foreach (NewReportDomainObject domainObject in DataSource)
            {
                name = string.Empty;
                if (groupColumns.Length > 0)
                {
                    for (int i = 0; i < groupColumns.Length; i++)
                    {
                        if (groupColumns[i].Trim().ToLower() == "sscode")
                        {
                            name += domainObject.SSCode + "-";
                        }

                        else if (groupColumns[i].Trim().ToLower() == "opcode")
                        {
                            name += domainObject.OPCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "rescode")
                        {
                            name += domainObject.ResCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "bigsscode")
                        {
                            name += domainObject.BigSSCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "segcode")
                        {
                            name += domainObject.SegCode + "-";
                        }

                        else if (groupColumns[i].Trim().ToLower() == "faccode")
                        {
                            name += domainObject.FacCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "momemo")
                        {
                            name += domainObject.MOMemo + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "newmass")
                        {
                            name += domainObject.NewMass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "itemcode")
                        {
                            name += domainObject.ItemCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "modelcode")
                        {
                            name += domainObject.ModelCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mmodelcode" || groupColumns[i].Trim().ToLower() == "materialmodelcode")
                        {
                            name += domainObject.MaterialModelCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mmachinetype" || groupColumns[i].Trim().ToLower() == "materialmachinetype")
                        {
                            name += domainObject.MaterialMachineType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mexportimport" || groupColumns[i].Trim().ToLower() == "materialexportimport")
                        {
                            name += domainObject.MaterialExportImport + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "mocode")
                        {
                            name += domainObject.MOCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "firstclass")
                        {
                            name += domainObject.FirstClass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "secondclass")
                        {
                            name += domainObject.SecondClass + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "thirdclass")
                        {
                            name += domainObject.SSCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "lotno")
                        {
                            name += domainObject.LotNo + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "productiontype")
                        {
                            name += domainObject.ProductionType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "oqclottype")
                        {
                            name += domainObject.OQCLotType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "inspectorandname")
                        {
                            name += domainObject.InspectorAndName + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "iqclineitemtype")
                        {
                            name += domainObject.IQCLineItemType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "iqcitemtype")
                        {
                            name += domainObject.IQCItemType + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "rohs")
                        {
                            name += domainObject.Rohs + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "concessionstatus")
                        {
                            name += domainObject.ConcessionStatus + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "vendorcode")
                        {
                            name += domainObject.VendorCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "materialcode")
                        {
                            name += domainObject.MaterialCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "tpcode")
                        {
                            name += domainObject.PeriodCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToUpper() == "shiftcode")
                        {
                            name += domainObject.ShiftCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "shiftday")
                        {
                            name += domainObject.ShiftDay + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "dweek")
                        {
                            name += domainObject.Week + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "dmonth")
                        {
                            name += domainObject.Month + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "year")
                        {
                            name += domainObject.Year + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "goodsemigood")
                        {
                            name += domainObject.GoodSemiGood + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "crewcode")
                        {
                            name += domainObject.CrewCode + "-";
                        }
                        else if (groupColumns[i].Trim().ToLower() == "projectname")
                        {
                            name += domainObject.ProjectCode + "-";
                        }
                    }
                    name += domainObject.EAttribute1;
                }

                if (!nameColumn.Contains(name))
                {
                    nameColumn.Add(name, name);
                }

                nameList.Add(name);
            }


            foreach (System.Collections.DictionaryEntry objDE in nameColumn)
            {
                DataRow dr = dt.NewRow();
                dr[0] = objDE.Value.ToString();

                for (int m = 0; m < DataSource.Length; m++)
                {
                    NewReportDomainObject domainObject = DataSource[m] as NewReportDomainObject;

                    if (objDE.Value.ToString() == nameList[m].ToString())
                    {
                        if (domainObject.PeriodCode.Trim() == string.Empty)
                        {
                            dr[" "] = domainObject.TempValue;
                        }
                        else
                        {
                            if (domainObject.PlanQty != 0)
                            {
                                dr[domainObject.PeriodCode] = decimal.Parse(domainObject.TempValue) / decimal.Parse(domainObject.PlanQty.ToString()); ;//domainObject.TempValue;
                            }
                        }
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}