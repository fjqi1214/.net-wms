using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BenQGuru.eMES.WebQuery;
using Infragistics.WebUI.UltraWebChart;

namespace BenQGuru.Web.ReportCenter.UserControls
{
    public partial class UCColumnChartProcess : System.Web.UI.UserControl
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
            columnChart.DataSource = data;
            columnChart.DataBind();
        }


        /// <summary>
        /// 绑定Chart图
        /// </summary>
        public void DataBindQty(string planQtyName)
        {
            DefineChartStyle();

            DataTable data = ConvertDataTableNewQty(planQtyName);
            columnChart.DataSource = data;
            columnChart.DataBind();
        }

        /// <summary>
        /// 当X轴的值等于EAttribute1的值时，绑定Chart图
        /// </summary>
        public void DataBindOQC()
        {
            DefineChartStyle();

            DataTable data = ConvertDataTableOQC();
            columnChart.DataSource = data;
            columnChart.DataBind();
        }


        /// <summary>
        /// 绑定Chart图 产成品库龄
        /// </summary>
        public void DataBindProductInvPeriod()
        {
            this.columnChart.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.StackColumnChart;
            DefineChartStyle();

            DataTable data = ConvertDataTableNew();
            columnChart.DataSource = data;
            columnChart.DataBind();
        }

        /// <summary>
        /// 定义图表样式
        /// </summary>
        private void DefineChartStyle()
        {
            //定义X轴的Extent
            if (DataSource.Length > 0 && DataSource[DataSource.Length - 1].PeriodCode != null)
            {
                this.columnChart.Axis.X.Extent = System.Text.Encoding.GetEncoding("GB2312").GetByteCount(DataSource[DataSource.Length - 1].PeriodCode) * 5;
                this.columnChart.Axis.Y.Extent = 32;
                this.columnChart.Axis.Y.Labels.ItemFormatString = YLabelFormatString;
                this.columnChart.ColumnChart.ChartText[0].ItemFormatString = ChartTextFormatString;
                this.columnChart.Data.ZeroAligned = DataType;
                this.columnChart.ColumnChart.ColumnSpacing = 0;
                this.columnChart.Axis.X.Extent = 100;                
                if (m_Width != 0)
                {
                    columnChart.Width = m_Width;
                }

                if (m_Height != 0)
                {
                    columnChart.Height = m_Height;
                }
            }
        }

        /// <summary>
        /// 将数据转换为相对应X轴、Y轴的DataTable
        /// </summary>
        //public DataTable ConvertDataTable()
        //{
        //    DataTable dt = new DataTable();

        //    //此列用来显示有变描述
        //    dt.Columns.Add(new DataColumn("Name", typeof(string)));
        //    ArrayList columnName = new ArrayList();

        //    foreach (NewReportDomainObject domainObject in DataSource) //X轴
        //    {
        //        if (!columnName.Contains(domainObject.PeriodCode))
        //        {
        //            columnName.Add(domainObject.PeriodCode);
        //        }
        //    }

        //    //排序名称
        //    columnName.Sort();

        //    for (int i = 0; i < columnName.Count; i++)
        //    {
        //        dt.Columns.Add(new DataColumn(columnName[i].ToString(), typeof(double)));
        //    }

        //    Hashtable nameColumn = new Hashtable();
        //    string name = string.Empty;

        //    #region //Y轴
        //    string[] groupColumns = m_ChartGroupByString.Split(',');
        //    ArrayList nameList = new ArrayList();
        //    foreach (NewReportDomainObject domainObject in DataSource)
        //    {
        //        name = string.Empty;
        //        if (groupColumns.Length > 0)
        //        {
        //            for (int i = 0; i < groupColumns.Length; i++)
        //            {
        //                if (groupColumns[i].Trim().ToLower() == "sscode")
        //                {
        //                    name += domainObject.SSCode + "-";
        //                }

        //                else if (groupColumns[i].Trim().ToLower() == "opcode")
        //                {
        //                    name += domainObject.OPCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "rescode")
        //                {
        //                    name += domainObject.ResCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "bigsscode")
        //                {
        //                    name += domainObject.BigSSCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "segcode")
        //                {
        //                    name += domainObject.SegCode + "-";
        //                }

        //                else if (groupColumns[i].Trim().ToLower() == "faccode")
        //                {
        //                    name += domainObject.FacCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "momemo")
        //                {
        //                    name += domainObject.MOMemo + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "newmass")
        //                {
        //                    name += domainObject.NewMass + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "itemcode")
        //                {
        //                    name += domainObject.ItemCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "mmodelcode" || groupColumns[i].Trim().ToLower() == "materialmodelcode")
        //                {
        //                    name += domainObject.MaterialModelCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "mmachinetype" || groupColumns[i].Trim().ToLower() == "materialmachinetype")
        //                {
        //                    name += domainObject.MaterialMachineType + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "mexportimport" || groupColumns[i].Trim().ToLower() == "materialexportimport")
        //                {
        //                    name += domainObject.MaterialExportImport + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "mocode")
        //                {
        //                    name += domainObject.MOCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "firstclass")
        //                {
        //                    name += domainObject.FirstClass + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "secondclass")
        //                {
        //                    name += domainObject.SecondClass + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "thirdclass")
        //                {
        //                    name += domainObject.SSCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "lotno")
        //                {
        //                    name += domainObject.LotNo + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "productiontype")
        //                {
        //                    name += domainObject.ProductionType + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "oqclottype")
        //                {
        //                    name += domainObject.OQCLotType + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "inspectorandname")
        //                {
        //                    name += domainObject.InspectorAndName + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "iqclineitemtype")
        //                {
        //                    name += domainObject.IQCLineItemType + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "iqcitemtype")
        //                {
        //                    name += domainObject.IQCItemType + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "rohs")
        //                {
        //                    name += domainObject.Rohs + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "concessionstatus")
        //                {
        //                    name += domainObject.ConcessionStatus + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "vendorcode")
        //                {
        //                    name += domainObject.VendorCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "materialcode")
        //                {
        //                    name += domainObject.MaterialCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "tpcode")
        //                {
        //                    name += domainObject.PeriodCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToUpper() == "shiftcode")
        //                {
        //                    name += domainObject.ShiftCode + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "shiftday")
        //                {
        //                    name += domainObject.ShiftDay + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "dweek")
        //                {
        //                    name += domainObject.Week + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "dmonth")
        //                {
        //                    name += domainObject.Month + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "year")
        //                {
        //                    name += domainObject.Year + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "goodsemigood")
        //                {
        //                    name += domainObject.GoodSemiGood + "-";
        //                }
        //                else if (groupColumns[i].Trim().ToLower() == "crewcode")
        //                {
        //                    name += domainObject.CrewCode + "-";
        //                }
        //            }
        //            name += domainObject.EAttribute1;
        //        }

        //        if (!nameColumn.Contains(name))
        //        {
        //            nameColumn.Add(name, name);
        //        }

        //        nameList.Add(name);
        //    }

        //    #endregion

        //    foreach (System.Collections.DictionaryEntry objDE in nameColumn)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dr[0] = objDE.Value.ToString();
        //        for (int i = 1; i < columnName.Count + 1; i++)
        //        {
        //            dr[i] = 0;
        //        }
        //        for (int m = 0; m < DataSource.Length; m++)
        //        {
        //            NewReportDomainObject domainObject = DataSource[m] as NewReportDomainObject;

        //            if (objDE.Value.ToString() == nameList[m].ToString())
        //            {
        //                dr[domainObject.PeriodCode] = domainObject.TempValue;
        //            }
        //        }

        //        dt.Rows.Add(dr);
        //    }

        //    return dt;
        //}

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
                        else if (groupColumns[i].Trim().ToLower() == "dutydesc")
                        {
                            name += domainObject.DutyDesc + "-";
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
                        else if (groupColumns[i].Trim().ToLower() == "modelcode")
                        {
                            name += domainObject.ModelCode + "-";
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


            dt.Columns.Add(new DataColumn(planQtyName, typeof(double)));
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
                dr[1] = 0;
                for (int i = 2; i < columnNameList.Count + 2; i++)
                {
                    dr[i] = 0;
                }
                for (int m = 0; m < DataSource.Length; m++)
                {
                    NewReportDomainObject domainObject = DataSource[m] as NewReportDomainObject;

                    if (value == domainObject.PeriodCode)
                    {

                        dr[planQtyName] = domainObject.PlanQty;

                        
                        dr[allNameList[m].ToString()] = domainObject.TempValue;                                                
                    }
                }

                dt.Rows.Add(dr);
            }



            return dt;
        }
    }
}