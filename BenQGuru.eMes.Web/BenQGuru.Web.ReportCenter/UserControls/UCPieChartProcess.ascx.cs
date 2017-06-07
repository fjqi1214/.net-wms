using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Infragistics.WebUI.UltraWebChart;
using BenQGuru.eMES.WebQuery;

namespace BenQGuru.Web.ReportCenter.UserControls
{
    public partial class UCPieChartProcess : System.Web.UI.UserControl
    {
        protected Infragistics.UltraChart.Data.ChartDemoData ChartData;
        protected void Page_Load(object sender, EventArgs e)
        {

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

        private bool m_DataType = false;
        public bool DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
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

            DataTable data = ConvertDataTable();
            pieChart.DataSource = data;
            pieChart.DataBind();
        }

        /// <summary>
        /// 当X轴的值等于EAttribute1的值时，绑定Chart图
        /// </summary>
        public void DataBindOQC()
        {
            DefineChartStyle();

            DataTable data = ConvertDataTableOQC();
            pieChart.DataSource = data;
            pieChart.DataBind();
        }

        /// <summary>
        /// 定义图表样式
        /// </summary>
        private void DefineChartStyle()
        {
            //定义X轴的Extent
            if (DataSource.Length > 0 && DataSource[DataSource.Length - 1].PeriodCode != null)
            {                
                this.pieChart.Axis.X.Extent = DataSource[DataSource.Length - 1].PeriodCode.Length ;
                this.pieChart.Axis.Y.Labels.ItemFormatString = YLabelFormatString;
                this.pieChart.PieChart.ChartText[0].ItemFormatString = ChartTextFormatString;
                if (m_Width != 0)
                {
                    pieChart.Width = m_Width;
                }

                if (m_Height != 0)
                {
                    pieChart.Height = m_Height;
                }
            }
        }

        /// <summary>
        /// 将数据转换为相对应X轴、Y轴的DataTable
        /// </summary>
        public DataTable ConvertDataTable()
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
                        dr[domainObject.PeriodCode] = domainObject.TempValue;
                    }
                }

                dt.Rows.Add(dr);
            }

            return dt;
        }

        /// <summary>
        /// 当X轴的值等于EAttribute1的值时，将数据转换为相对应X轴、Y轴的DataTable
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
                        name = name.Substring(0, name.Length-1);
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
    }
}