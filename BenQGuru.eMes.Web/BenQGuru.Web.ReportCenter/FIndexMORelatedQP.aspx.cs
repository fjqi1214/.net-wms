using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Report;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    public partial class FIndexMORelatedQP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        protected global::System.Web.UI.WebControls.TextBox datStartDateQuery;
        protected global::System.Web.UI.WebControls.TextBox datEndDateQuery;

        private InventoryFacade _InventoryFacade = null;
        private MOFacade _MOFacade = null;

        #region Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);

            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            _InventoryFacade = new InventoryFacade(this.DataProvider);
            _MOFacade = new MOFacade(this.DataProvider);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                InitControls();

                this.columnChart.Visible = false;
                this.lineChart.Visible = false;
            }
            needVScroll = true;
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.GridExport(this.gridWebGrid);
        }

        private void InitControls()
        {
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            WarehouseFacade warehouseFacade = new WarehouseFacade(this.DataProvider);

            this.ddlGoodSemiGoodQuery.Items.Clear();
            this.ddlGoodSemiGoodQuery.Items.Add(new ListItem("", ""));
            this.ddlGoodSemiGoodQuery.Items.Add(new ListItem(this.languageComponent1.GetString(ItemType.ITEMTYPE_FINISHEDPRODUCT), ItemType.ITEMTYPE_FINISHEDPRODUCT));
            this.ddlGoodSemiGoodQuery.Items.Add(new ListItem(this.languageComponent1.GetString(ItemType.ITEMTYPE_SEMIMANUFACTURE), ItemType.ITEMTYPE_SEMIMANUFACTURE));

            this.rblByTimeTypeQuery.Items.Clear();
            this.rblByTimeTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(NewReportByTimeType.Week), NewReportByTimeType.Week));
            this.rblByTimeTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(NewReportByTimeType.Month), NewReportByTimeType.Month));
            this.rblByTimeTypeQuery.SelectedValue = NewReportByTimeType.Week;

            this.datStartDateQuery.Text = DateTime.Parse(DateTime.Now.Year.ToString() + "-01-01").ToString("yyyy-MM-dd");
            this.datEndDateQuery.Text = DateTime.Now.ToString("yyyy-MM-dd");

            //Gird and OWC Title
            //新grid没有这个属性
            //this.gridWebGrid.Caption = "MOCloseRate";
            //this.gridWebGrid2.Caption = "MOPeriodDistribute";

        }

        private void ProcessGridStyle()
        {
            //合并单元格、对齐、背景色
            //for (int i = 0; i < gridWebGrid.Rows.Count; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        gridWebGrid.Rows[i].Cells[0].RowSpan = 2;
            //        gridWebGrid.Rows[i].Cells[1].RowSpan = 2;

            //        gridWebGrid.Rows[i].Cells[1].Style.HorizontalAlign = HorizontalAlign.Right;

            //        for (int j = 2; j < gridWebGrid.Columns.Count; j++)
            //        {
            //            gridWebGrid.Rows[i].Cells[j].ColSpan = 2;
            //            gridWebGrid.Rows[i].Cells[j].Style.HorizontalAlign = HorizontalAlign.Center;
            //        }
            //    }
            //    else
            //    {
            //        for (int j = 2; j < gridWebGrid.Columns.Count; j++)
            //        {
            //            if (j % 2 == 1)
            //            {
            //                gridWebGrid.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.DarkCyan;
            //            }

            //            gridWebGrid.Rows[i].Cells[j].Style.HorizontalAlign = HorizontalAlign.Right;
            //        }
            //    }
            //}
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridWebGrid.Columns.Clear();

            this.gridHelper.AddColumn("TimeDimension", "时间维度", null);
            this.gridHelper.AddColumn("BeginningOpenMOCount", "期初未关工单累计", null);
            this.gridHelper.AddColumn("NewOpenMOCount", "新增开工数", null);
            this.gridHelper.AddColumn("NewCloseMOCount", "新增关单数", null);
            this.gridHelper.AddColumn("ClosingOpenMOCount", "期末未关工单累计", null);
            this.gridHelper.AddColumn("MOCloseRate", "关单率", null);

            this.gridHelper.AddDefaultColumn(false, false);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
            
            //this.gridWebGrid2.Columns.Clear();
            //this.gridWebGrid2.Columns.Add(string.Empty, string.Empty);

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            this.gridWebGrid.Height = 300;
            string sql = string.Empty;

            int startDate = 0;
            int endDate = 0;
            string timeField = "dweek";

            try
            {
                startDate = FormatHelper.TODateInt(this.datStartDateQuery.Text);
            }
            catch { }

            try
            {
                endDate = FormatHelper.TODateInt(this.datEndDateQuery.Text);
            }
            catch { }

            if (this.rblByTimeTypeQuery.SelectedValue == NewReportByTimeType.Week)
            {
                timeField = "dweek";
            }
            else
            {
                timeField = "dmonth";
            }

            //获取工单关单率
            sql = GetCloseRateSql(startDate, endDate, this.ddlGoodSemiGoodQuery.SelectedValue, timeField);
            object[] closeRateDataSource = this.DataProvider.CustomQuery(typeof(RptMOCloseRate), new PagerCondition(sql, "year," + timeField, inclusive, exclusive));


            ////获取工单账龄
            //sql = GetMOPeriodSql(startDate, endDate, this.ddlPeriodGroupQuery.SelectedValue);
            //object[] moPeriodDataSource = this.DataProvider.CustomQuery(typeof(ProductInvPeriod), new SQLCondition(sql));
            object[] moPeriodDataSource = null;

            //生成OWC图形
            ProcessCloseRateOWC(closeRateDataSource);
            ProcessMOPeriodGrid(moPeriodDataSource);
            ProcessMOPeriodOWC(moPeriodDataSource);

            return closeRateDataSource;
        }

        protected override int GetRowCount()
        {
            int startDate = 0;
            int endDate = 0;
            string timeField = "dweek";

            try
            {
                startDate = FormatHelper.TODateInt(this.datStartDateQuery.Text);
            }
            catch { }

            try
            {
                endDate = FormatHelper.TODateInt(this.datEndDateQuery.Text);
            }
            catch { }

            if (this.rblByTimeTypeQuery.SelectedValue == NewReportByTimeType.Week)
            {
                timeField = "dweek";
            }
            else
            {
                timeField = "dmonth";
            }

            return this.DataProvider.GetCount(new SQLCondition(GetCloseRateCountSql(startDate, endDate, this.ddlGoodSemiGoodQuery.SelectedValue, timeField)));
        }

        protected override DataRow GetGridRow(object obj)
        {
            RptMOCloseRate rptMOCloseRate = (RptMOCloseRate)obj;

            string dateString = rptMOCloseRate.Year.ToString() + "/";
            if (this.rblByTimeTypeQuery.SelectedValue == NewReportByTimeType.Week)
            {
                dateString += rptMOCloseRate.Week.ToString();
            }
            else
            {
                dateString += rptMOCloseRate.Month.ToString();
            }

            DataRow row = DtSource.NewRow();
            row["TimeDimension"] = dateString;
            row["BeginningOpenMOCount"] = rptMOCloseRate.FirstPQty.ToString();
            row["NewOpenMOCount"] = rptMOCloseRate.OpenQty.ToString();
            row["NewCloseMOCount"] = rptMOCloseRate.CloseQty.ToString();
            row["ClosingOpenMOCount"] = rptMOCloseRate.EndPQty.ToString();
            row["MOCloseRate"] = rptMOCloseRate.CloseRate.ToString("0.00%");
            return row;
        }

        #endregion

        #region SQL

        private string GetCloseRateSql(int startDate, int endDate, string goodSemiGood, string timeField)
        {
            string sql = string.Empty;
            int firstDate = 0;

            //获取查询起始时间的第一天，为了算期初未关单数量
            if (startDate > 0)
            {
                sql = "SELECT a.ddate FROM tbltimedimension a ";
                if (this.rblByTimeTypeQuery.SelectedValue == NewReportByTimeType.Week)
                {
                    sql += "WHERE EXISTS (SELECT * FROM tbltimedimension b WHERE b.ddate = " + startDate.ToString() + " AND a.year = b.year AND a.dweek = b.dweek ) ";
                }
                else
                {
                    sql += "WHERE EXISTS (SELECT * FROM tbltimedimension b WHERE b.ddate = " + startDate.ToString() + " AND a.year = b.year AND a.dmonth = b.dmonth ) ";
                }
                sql += "ORDER BY a.ddate ";

                object[] list = this.DataProvider.CustomQuery(typeof(TimeDimension), new SQLCondition(sql));
                if (list != null && list.Length > 0)
                {
                    firstDate = ((TimeDimension)list[0]).Date;
                }
            }

            //获取期初未关单数量
            int beginOpenCount = 0;
            if (firstDate > 0)
            {
                sql = "SELECT COUNT(*) FROM tblmo a, tblmaterial m ";
                sql += "WHERE a.moactstartdate < " + firstDate.ToString() + " ";
                sql += "AND (a.mostatus IN ('mostatus_open', 'mostatus_pending') OR (a.mostatus = 'mostatus_close' AND a.moactenddate >= " + firstDate.ToString() + ")) ";
                sql += "AND a.itemcode = m.mcode ";
                if (goodSemiGood.Trim().Length > 0)
                {
                    sql += "AND m.mtype = '" + goodSemiGood + "' ";
                }
                beginOpenCount = this.DataProvider.GetCount(new SQLCondition(sql));
            }

            sql = "SELECT year, " + timeField + ", firstpqty + " + beginOpenCount.ToString() + " AS firstpqty, openqty, closeqty, endpqty + " + beginOpenCount.ToString() + " AS endpqty, ";
            sql += "    ROUND(DECODE(openqty + firstpqty + " + beginOpenCount.ToString() + ", 0, 0, closeqty / (openqty + firstpqty + " + beginOpenCount.ToString() + ")), 4) AS closesorate ";
            sql += GetCloseRateSqlPart(startDate, endDate, goodSemiGood, timeField);

            return sql;
        }

        private string GetCloseRateCountSql(int startDate, int endDate, string goodSemiGood, string timeField)
        {
            string sql = "SELECT COUNT(*) ";
            sql += GetCloseRateSqlPart(startDate, endDate, goodSemiGood, timeField);

            return sql;
        }

        private string GetCloseRateSqlPart(int startDate, int endDate, string goodSemiGood, string timeField)
        {
            string sql = string.Empty;
            sql += "FROM ( ";
            sql += "    SELECT year, " + timeField + ", LAG(endpqty, 1, 0) OVER(ORDER BY year, " + timeField + ") AS firstpqty, openqty, closeqty, diffqty, endpqty ";
            sql += "    FROM ( ";
            sql += "        SELECT year, " + timeField + ", openqty, closeqty, diffqty, SUM(diffqty) OVER(ORDER BY year, " + timeField + " ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS endpqty ";
            sql += "        FROM ( ";
            sql += "            SELECT so.year, so." + timeField + ", SUM(so.openqty) openqty, SUM(so.closeqty) closeqty, ";
            sql += "                SUM(so.openqty) - SUM(so.closeqty) diffqty ";
            sql += "            FROM ( ";
            sql += "                SELECT NVL(openso.year, closeso.year) year, NVL(openso." + timeField + ", closeso." + timeField + ") " + timeField + ", ";
            sql += "                    NVL(openso.openqty, 0) openqty, NVL(closeso.closeqty, 0) closeqty ";
            sql += "                FROM ( ";
            sql += "                    SELECT COUNT(mocode) openqty, t.ddate, t.dweek, t.dmonth, t.year ";
            sql += "                    FROM tblmo a, tbltimedimension t, tblmaterial m";
            sql += "                    WHERE a.moactstartdate = t.ddate ";
            sql += "                    AND a.itemcode = m.mcode ";
            if (startDate > 0)
            {
                sql += "                    AND t.ddate >= " + startDate.ToString() + " ";
            }
            if (endDate > 0)
            {
                sql += "                    AND t.ddate <= " + endDate.ToString() + " ";
            }
            if (goodSemiGood.Trim().Length > 0)
            {
                sql += "                    AND m.mtype = '" + goodSemiGood + "' ";
            }
            sql += "                    AND a.mostatus IN ('" + MOManufactureStatus.MOSTATUS_OPEN + "', '" + MOManufactureStatus.MOSTATUS_PENDING + "', '" + MOManufactureStatus.MOSTATUS_CLOSE + "') ";
            sql += "                    GROUP BY t.ddate, t.dweek, t.dmonth, t.year ";
            sql += "                ) openso ";
            sql += "                FULL JOIN ( ";
            sql += "                    SELECT COUNT(mocode) closeqty, t.ddate, t.dweek, t.dmonth, t.year ";
            sql += "                    FROM tblmo a, tbltimedimension t, tblmaterial m";
            sql += "                    WHERE a.moactenddate = t.ddate ";
            sql += "                    AND a.itemcode = m.mcode ";
            if (startDate > 0)
            {
                sql += "                    AND t.ddate >= " + startDate.ToString() + " ";
            }
            if (endDate > 0)
            {
                sql += "                    AND t.ddate <= " + endDate.ToString() + " ";
            }
            if (goodSemiGood.Trim().Length > 0)
            {
                sql += "                    AND m.mtype = '" + goodSemiGood + "' ";
            }
            sql += "                    AND a.mostatus IN ('" + MOManufactureStatus.MOSTATUS_CLOSE + "') ";
            sql += "                    GROUP BY t.ddate, t.dweek, t.dmonth, t.year ";
            sql += "                ) closeso  ";
            sql += "                ON openso.ddate = closeso.ddate ";
            sql += "            ) so ";
            sql += "            GROUP BY so.year, so." + timeField + " ";
            sql += "        ) ";
            sql += "    ) ";
            sql += ") ";

            return sql;
        }

        private string GetMOPeriodSql(int startDate, int endDate, string periodGroup)
        {
            string sql = string.Empty;

            sql += "SELECT tblinvperiod.invperiodcode, tblinvperiod.datefrom, tblinvperiod.dateto, COUNT(*) AS productcount ";
            sql += "FROM ( ";
            sql += "    SELECT moactstartdate AS indate, FLOOR(SYSDATE - to_date(moactstartdate,'YYYYMMDD')) AS opendays ";
            sql += "    FROM tblmo ";
            sql += "    WHERE mostatus IN ('" + MOManufactureStatus.MOSTATUS_OPEN + "', '" + MOManufactureStatus.MOSTATUS_PENDING + "') ";
            sql += ") inv, tblinvperiod ";
            sql += "WHERE opendays BETWEEN tblinvperiod.datefrom AND tblinvperiod.dateto ";

            if (periodGroup.Trim().Length > 0)
            {
                sql += "AND tblinvperiod.peiodgroup = '" + periodGroup + "' ";
            }
            if (startDate > 0)
            {
                sql += "AND inv.indate >= " + startDate.ToString() + " ";
            }
            if (endDate > 0)
            {
                sql += "AND inv.indate <= " + endDate.ToString() + " ";
            }
            sql += "GROUP BY tblinvperiod.invperiodcode, tblinvperiod.datefrom, tblinvperiod.dateto ";
            sql += "ORDER BY tblinvperiod.datefrom, tblinvperiod.dateto ";

            return sql;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            RptMOCloseRate rptMOCloseRate = (RptMOCloseRate)obj;

            string dateString = rptMOCloseRate.Year.ToString() + "/";
            if (this.rblByTimeTypeQuery.SelectedValue == NewReportByTimeType.Week)
            {
                dateString += rptMOCloseRate.Week.ToString();
            }
            else
            {
                dateString += rptMOCloseRate.Month.ToString();
            }

            return new string[]
                {
                    dateString,
                    rptMOCloseRate.FirstPQty.ToString(),
                    rptMOCloseRate.OpenQty.ToString(),
                    rptMOCloseRate.CloseQty.ToString(),
                    rptMOCloseRate.EndPQty.ToString(),
                    rptMOCloseRate.CloseRate.ToString("0.00%")
                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[]
            {
                "TimeDimension",
                "BeginningOpenMOCount",
                "NewOpenMOCount",
                "NewCloseMOCount",
                "ClosingOpenMOCount",
                "MOCloseRate"
            };

        }

        #endregion

        #region OWC / Manual Grid

        private void ProcessCloseRateOWC(object[] dataSource)
        {
            if (dataSource != null)
            {
                this.lineChart.Visible = true;
                NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dataSource.Length];

                string propertyName = this.languageComponent1.GetString("MOCloseRate");
                NewReportDomainObject item;
                for (int i = 0; i < dataSource.Length; i++)
                {
                    item = new NewReportDomainObject();
                    item.EAttribute1 = propertyName;
                    if (this.rblByTimeTypeQuery.SelectedValue == NewReportByTimeType.Week)
                    {
                        item.PeriodCode = (dataSource[i] as RptMOCloseRate).Year.ToString() + "/" + (dataSource[i] as RptMOCloseRate).Week.ToString();
                    }
                    else
                    {
                        item.PeriodCode = (dataSource[i] as RptMOCloseRate).Year.ToString() + "/" + (dataSource[i] as RptMOCloseRate).Month.ToString();
                    }
                    item.TempValue = (dataSource[i] as RptMOCloseRate).CloseRate.ToString();
                    dateSourceForOWC[i] = item;
                }

                this.lineChart.ChartGroupByString = "";
                this.lineChart.ChartTextFormatString = "<DATA_VALUE:0.00%>";
                this.lineChart.YLabelFormatString = "<DATA_VALUE:##%>";
                this.lineChart.DataType = true;
                this.lineChart.DataSource = dateSourceForOWC;
                this.lineChart.DataBind();
            }
            else
            {
                this.lineChart.Visible = false;
            }
            //if (dataSource != null)
            //{
            //    string serieName = this.languageComponent1.GetString("MOCloseRate");

            //    List<string> categoryArray = new List<string>();
            //    List<object> valueArray = new List<object>();

            //    foreach (RptMOCloseRate moCloseRate in dataSource)
            //    {
            //        string currCategory = string.Empty;
            //        if (this.rblByTimeTypeQuery.SelectedValue == NewReportByTimeType.Week)
            //        {
            //            currCategory = moCloseRate.Year.ToString() + "/" + moCloseRate.Week.ToString();
            //        }
            //        else
            //        {
            //            currCategory = moCloseRate.Year.ToString() + "/" + moCloseRate.Month.ToString();
            //        }

            //        categoryArray.Add(currCategory);
            //        valueArray.Add(Convert.ToSingle(moCloseRate.CloseRate));
            //    }

            //    this.OWCChartSpace1.AddChart(true, serieName, categoryArray.ToArray(), valueArray.ToArray(), string.Empty);
            //    this.OWCChartSpace1.ChartType = OWCChartType.Line;
            //    this.OWCChartSpace1.AxesLeftNumberFormat = "0%";
            //    //this.OWCChartSpace1.ChartLeftMaximum = 1;
            //    this.OWCChartSpace1.Display = true;
            //}
        }

        private void ProcessMOPeriodOWC(object[] dataSource)
        {
            if (dataSource != null)
            {
                this.columnChart.Visible = true;
                NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dataSource.Length];

                string propertyName = this.languageComponent1.GetString("MOPeriod");
                NewReportDomainObject item;
                //int temp = (dataSource[0] as ProductInvPeriod).ProductCount;

                for (int i = 0; i < dataSource.Length; i++)
                {
                    item = new NewReportDomainObject();
                    item.EAttribute1 = propertyName;
                    item.PeriodCode = (dataSource[i] as ProductInvPeriod).DateFrom.ToString() + "~" + (dataSource[i] as ProductInvPeriod).DateTo.ToString();
                    item.TempValue = (dataSource[i] as ProductInvPeriod).ProductCount.ToString();
                    dateSourceForOWC[i] = item;
                    //if (temp < (dataSource[i] as ProductInvPeriod).ProductCount)
                    //{
                    //    temp = (dataSource[i] as ProductInvPeriod).ProductCount;
                    //}
                }

                //this.columnChart.YRangeMin = 0;
                //this.columnChart.YRangeMax = Convert.ToDouble(temp);

                this.columnChart.ChartGroupByString = "";
                this.columnChart.ChartTextFormatString = "<DATA_VALUE:0.##>";
                this.columnChart.YLabelFormatString = "<DATA_VALUE:0.##>";
                this.columnChart.DataType = true;
                this.columnChart.DataSource = dateSourceForOWC;
                this.columnChart.DataBind();
            }
            else
            {
                this.columnChart.Visible = false;
            }

            //this.OWCChartSpace2.ClearCharts();

            //if (dataSource != null)
            //{
            //    string serieName = this.languageComponent1.GetString("MOPeriod");


            //    List<string> categoryArray = new List<string>();
            //    List<object> valueArray = new List<object>();

            //    foreach (ProductInvPeriod moPeriod in dataSource)
            //    {
            //        categoryArray.Add(moPeriod.DateFrom.ToString() + " ~ " + moPeriod.DateTo.ToString());
            //        valueArray.Add(moPeriod.ProductCount);
            //    }

            //    this.OWCChartSpace2.AddChart(false, serieName, categoryArray.ToArray(), valueArray.ToArray(), string.Empty);
            //    this.OWCChartSpace2.ChartType = OWCChartType.ColumnClustered;
            //    this.OWCChartSpace2.AxesLeftNumberFormat = "0";
            //    this.OWCChartSpace2.Display = true;
            //}
        }

        private void ProcessMOPeriodGrid(object[] dataSource)
        {
            //this.gridWebGrid2.Rows.Clear();
            //this.gridWebGrid2.Columns.Clear();

            if (dataSource == null)
            {
                //this.gridWebGrid2.Columns.Add(string.Empty, string.Empty);
            }
            else
            {
                List<string> categoryArray = new List<string>();
                List<object> valueArray = new List<object>();

                foreach (ProductInvPeriod moPeriod in dataSource)
                {
                    categoryArray.Add(moPeriod.DateFrom.ToString() + " ~ " + moPeriod.DateTo.ToString());
                    valueArray.Add(moPeriod.ProductCount);
                }

                foreach (string category in categoryArray)
                {
                    //this.gridWebGrid2.Columns.Add(category, category);
                }

                //this.gridWebGrid2.Rows.Add(new UltraGridRow(valueArray.ToArray()));
            }
        }

        #endregion
    }
}
