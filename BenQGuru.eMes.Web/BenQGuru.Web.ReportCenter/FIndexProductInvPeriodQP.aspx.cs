using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.MOModel;
using Infragistics.WebUI.UltraWebGrid;
using System.IO;
using BenQGuru.eMES.Common;
using System.Text;
using BenQGuru.eMES.WebQuery;

namespace BenQGuru.eMES.Web.WebQuery
{
    public partial class FIndexProductInvPeriodQP : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        public BenQGuru.eMES.Web.UserControl.eMESDate datDateQuery;

        private InventoryFacade _InventoryFacade = null;
        private int _AddColumnCount = 0;

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

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.datDateQuery.Date_DateTime = DateTime.Now;

                this.columnChart.Visible = false;
            }
        }

        protected override void cmdQuery_Click(object sender, System.EventArgs e)
        {
            base.cmdQuery_Click(sender, e);
            this.ProcessGridStyle();
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.GridExport(this.gridWebGrid);
        }

        private void ProcessGridStyle()
        {
            //合并单元格、对齐、背景色
            for (int i = 0; i < gridWebGrid.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    gridWebGrid.Rows[i].Cells[0].RowSpan = 2;
                    gridWebGrid.Rows[i].Cells[1].RowSpan = 2;

                    gridWebGrid.Rows[i].Cells[1].Style.HorizontalAlign = HorizontalAlign.Right;

                    for (int j = 2; j < gridWebGrid.Columns.Count; j++)
                    {
                        gridWebGrid.Rows[i].Cells[j].ColSpan = 2;
                        gridWebGrid.Rows[i].Cells[j].Style.HorizontalAlign = HorizontalAlign.Center;
                    }
                }
                else
                {
                    for (int j = 2; j < gridWebGrid.Columns.Count; j++)
                    {
                        if (j % 2 == 1)
                        {
                            gridWebGrid.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.DarkCyan;
                        }

                        gridWebGrid.Rows[i].Cells[j].Style.HorizontalAlign = HorizontalAlign.Right;
                    }
                }
            }
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            this.gridWebGrid.Columns.Clear();

            this.gridHelper.AddColumn("StorageAttribute", "库存属性", null);
            this.gridHelper.AddColumn("InvTotal", "总库存", null);

            for (int i = 0; i < _AddColumnCount * 2; i++)
            {
                this.gridHelper.AddColumn("AddColumnCount" + i.ToString("000"), "", null);
            }

            this.gridHelper.AddDefaultColumn(false, false);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            int date = 0;
            try
            {
                date = FormatHelper.TODateInt(this.datDateQuery.Date_DateTime);
            }
            catch { }

            object[] dataSource = _InventoryFacade.QueryProductInvPeriod(
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageAttributeQuery.Text)),
               date,
               inclusive,
               exclusive);

            ProcessDataDourceToGrid(ref dataSource);
            ProcessOWC(dataSource);

            return dataSource;
        }

        protected override int GetRowCount()
        {
            int date = 0;
            try
            {
                date = FormatHelper.TODateInt(this.datDateQuery.Date_DateTime);
            }
            catch { }

            return _InventoryFacade.QueryProductInvPeriodCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageAttributeQuery.Text)),
                date);
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(GetRowData((ProductInvPeriod)obj));
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return GetRowData((ProductInvPeriod)obj);
        }

        protected override string[] GetColumnHeaderText()
        {
            string[] headerText = new string[2 + _AddColumnCount * 2];

            headerText[0] = "StorageAttribute";
            headerText[1] = "InvTotal";

            for (int i = 0; i < _AddColumnCount * 2; i++)
            {
                headerText[2 + i] = "";
            }

            return headerText;
        }

        #endregion

        private void ProcessDataDourceToGrid(ref object[] dataSource)
        {
            this.gridWebGrid.Rows.Clear();
            _AddColumnCount = 0;

            List<ProductInvPeriod> newDataSource = new List<ProductInvPeriod>();

            if (dataSource != null)
            {
                ProductInvPeriod lastProductInvPeriod = null;
                List<ProductInvPeriod> productInvPeriodList = new List<ProductInvPeriod>();

                foreach (ProductInvPeriod productInvPeriod in dataSource)
                {
                    if (lastProductInvPeriod == null || productInvPeriod.InventoryType == lastProductInvPeriod.InventoryType)
                    {
                        productInvPeriodList.Add(productInvPeriod);
                    }
                    else
                    {
                        newDataSource.AddRange(GetDataToGrid(productInvPeriodList));

                        productInvPeriodList.Clear();
                        productInvPeriodList.Add(productInvPeriod);
                    }

                    lastProductInvPeriod = productInvPeriod;
                }

                //处理结尾
                newDataSource.AddRange(GetDataToGrid(productInvPeriodList));

                dataSource = newDataSource.ToArray();
                InitWebGrid();
            }
        }

        private void ProcessOWC(object[] dataSource)
        {
            if (dataSource != null)
            {
                List<string> serieNameList = new List<string>();

                this.columnChart.Visible = true;
                //NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dataSource.Length + dataSource.Length];
                //NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dataSource.Length * 2];

                NewReportDomainObject item;
                NewReportDomainObject item1;
                int i = 0;
                foreach (ProductInvPeriod productInvPeriod in dataSource)
                {
                    if (productInvPeriod.IsForTitle)
                    {
                        continue;
                    }

                    foreach (ProductInvPeriod data in productInvPeriod.DataToGrid)
                    {
                        string serieName = data.DateFrom.ToString() + " ~ " + data.DateTo.ToString();
                        if (!serieNameList.Contains(serieName))
                        {
                            serieNameList.Add(serieName);
                        }
                    }
                }

                int count = 0;
                foreach (string serieName in serieNameList)
                {
                    List<string> categoryArray = new List<string>();
                    List<object> valueArray = new List<object>();

                    foreach (ProductInvPeriod productInvPeriod in dataSource)
                    {
                        if (productInvPeriod.IsForTitle)
                        {
                            continue;
                        }

                        foreach (ProductInvPeriod data in productInvPeriod.DataToGrid)
                        {
                            string currSerieName = data.DateFrom.ToString() + " ~ " + data.DateTo.ToString();
                            double rate = 0;
                            if (productInvPeriod.ProductCount > 0)
                            {
                                rate = data.ProductCount * 1.0 / productInvPeriod.ProductCount;
                            }

                            if (serieName == currSerieName)
                            {
                                count += 2;
                            }
                        }
                    }
                }

                if (count > 0)
                {
                    NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[count];

                    foreach (string serieName in serieNameList)
                    {
                        foreach (ProductInvPeriod productInvPeriod in dataSource)
                        {
                            if (productInvPeriod.IsForTitle)
                            {
                                continue;
                            }

                            foreach (ProductInvPeriod data in productInvPeriod.DataToGrid)
                            {
                                string currSerieName = data.DateFrom.ToString() + " ~ " + data.DateTo.ToString();
                                double rate = 0;
                                if (productInvPeriod.ProductCount > 0)
                                {
                                    rate = data.ProductCount * 1.0 / productInvPeriod.ProductCount;
                                }

                                if (serieName == currSerieName)
                                {
                                    item = new NewReportDomainObject();
                                    item1 = new NewReportDomainObject();
                                    item.EAttribute1 = serieName;
                                    item.PeriodCode = data.InventoryType;
                                    item.TempValue = Convert.ToSingle(rate).ToString();
                                    dateSourceForOWC[i++] = item;

                                    item1.EAttribute1 = serieName;
                                    item1.PeriodCode = data.InventoryType + this.languageComponent1.GetString("Norm");
                                    item1.TempValue = Convert.ToSingle(data.PercentageStandard).ToString();
                                    dateSourceForOWC[i++] = item1;
                                }
                            }
                        }

                    }
                    this.columnChart.ChartGroupByString = "";
                    this.columnChart.ChartTextFormatString = "<DATA_VALUE:0.00%>";
                    this.columnChart.YLabelFormatString = "<DATA_VALUE:0.##%>";
                    this.columnChart.DataType = true;
                    this.columnChart.DataSource = dateSourceForOWC;
                    this.columnChart.DataBindProductInvPeriod();
                }
                else
                {
                    this.columnChart.Visible = false;
                }

            }
            else
            {
                this.columnChart.Visible = false;
            }
            //this.OWCChartSpace1.ClearCharts();

            //if (dataSource != null)
            //{
            //    List<string> serieNameList = new List<string>();

            //    foreach (ProductInvPeriod productInvPeriod in dataSource)
            //    {
            //        if (productInvPeriod.IsForTitle)
            //        {
            //            continue;
            //        }

            //        foreach (ProductInvPeriod data in productInvPeriod.DataToGrid)
            //        {
            //            string serieName = data.DateFrom.ToString() + " ~ " + data.DateTo.ToString();
            //            if (!serieNameList.Contains(serieName))
            //            {
            //                serieNameList.Add(serieName);
            //            }
            //        }
            //    }

            //    foreach (string serieName in serieNameList)
            //    {
            //        List<string> categoryArray = new List<string>();
            //        List<object> valueArray = new List<object>();

            //        foreach (ProductInvPeriod productInvPeriod in dataSource)
            //        {
            //            if (productInvPeriod.IsForTitle)
            //            {
            //                continue;
            //            }

            //            foreach (ProductInvPeriod data in productInvPeriod.DataToGrid)
            //            {
            //                string currSerieName = data.DateFrom.ToString() + " ~ " + data.DateTo.ToString();
            //                double rate = 0;
            //                if (productInvPeriod.ProductCount > 0)
            //                {
            //                    rate = data.ProductCount * 1.0 / productInvPeriod.ProductCount;
            //                }

            //                if (serieName == currSerieName)
            //                {
            //                    categoryArray.Add(data.InventoryType);
            //                    valueArray.Add(Convert.ToSingle(rate));

            //                    categoryArray.Add(data.InventoryType + this.languageComponent1.GetString("Norm"));
            //                    valueArray.Add(Convert.ToSingle(data.PercentageStandard));
            //                }
            //            }
            //        }
                    

            //        this.OWCChartSpace1.AddChart(true, serieName, categoryArray.ToArray(), valueArray.ToArray(), string.Empty);
            //    }

            //    this.OWCChartSpace1.ChartType = OWCChartType.ColumnStacked;
            //    this.OWCChartSpace1.AxesLeftNumberFormat = "0%";
            //    this.OWCChartSpace1.ChartLeftMaximum = 1;
            //    this.OWCChartSpace1.Display = true;
            //}
        }

        private List<ProductInvPeriod> GetDataToGrid(List<ProductInvPeriod> productInvPeriodList)
        {
            List<ProductInvPeriod> returnValue = new List<ProductInvPeriod>();

            if (productInvPeriodList.Count >= 0)
            {
                //新增一个Title
                ProductInvPeriod title = new ProductInvPeriod();
                title.InventoryType = productInvPeriodList[0].InventoryType;
                title.IsForTitle = true;
                title.ProductCount = 0;
                foreach (ProductInvPeriod item in productInvPeriodList)
                {
                    title.DataToGrid.Add(item);
                    title.ProductCount += item.ProductCount;
                }
                
                returnValue.Add(title);

                //新增一个Title
                ProductInvPeriod data = new ProductInvPeriod();
                data.InventoryType = productInvPeriodList[0].InventoryType;
                data.IsForTitle = false;
                data.ProductCount = 0;
                foreach (ProductInvPeriod item in productInvPeriodList)
                {
                    data.DataToGrid.Add(item);
                    data.ProductCount += item.ProductCount;
                }
                returnValue.Add(data);

                if (title.DataToGrid.Count > _AddColumnCount)
                {
                    _AddColumnCount = title.DataToGrid.Count;
                }
            }

            return returnValue;
        }

        private string[] GetRowData(ProductInvPeriod productInvPeriod)
        {
            string[] rowData = new string[2 + productInvPeriod.DataToGrid.Count * 2];

            rowData[0] = productInvPeriod.InventoryType;
            rowData[1] = productInvPeriod.ProductCount.ToString();

            for (int i = 0; i < productInvPeriod.DataToGrid.Count; i++)
            {
                ProductInvPeriod data = productInvPeriod.DataToGrid[i];

                if (productInvPeriod.IsForTitle)
                {
                    rowData[2 + i * 2] = data.DateFrom.ToString() + " ~ " + data.DateTo.ToString();
                    rowData[2 + i * 2 + 1] = data.DateFrom.ToString() + " ~ " + data.DateTo.ToString();
                }
                else
                {
                    double rate = 0;
                    if (productInvPeriod.ProductCount > 0)
                    {
                        rate = data.ProductCount * 1.0 / productInvPeriod.ProductCount;
                        rowData[2 + i * 2] = rate.ToString("0.00%");
                    }
                    rowData[2 + i * 2 + 1] = data.PercentageStandard.ToString("0.00%");
                }
            }

            return rowData;
        }
    }
}
