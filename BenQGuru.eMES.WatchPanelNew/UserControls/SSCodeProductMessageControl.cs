using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; 


namespace BenQGuru.eMES.WatchPanelNew
{
    public partial class SSCodeProductMessageControl : System.Windows.Forms.UserControl
    {

        #region 变量

        private DataTable m_ProductDataTable = null; 
        private string _ExceptionMessageList = string.Empty;
        private string _HeaderLineMessage = string.Empty;
        private string _OnPostMan = string.Empty;

        private int _PlanQty = 0;       
        private int _SSCodeOutPutQty = 0;

        private object[] _ProductGridDataSource = null;        
        
        
        public string ExceptionMessageList
        {
            set { _ExceptionMessageList = value; }
        }

        public string HeaderLineMessage
        {
            set { _HeaderLineMessage = value; }
        }

        public int PlanQty
        {
            set { _PlanQty = value; }
        }

        public string OnPostMan
        {
            set { _OnPostMan = value; }
        }

        public int SSCodeOutPutQty
        {
            set { _SSCodeOutPutQty = value; }
        }

        public object[] ProductGridDataSource
        {
            set { _ProductGridDataSource = value; }
        }   

        public object[] PassQtyDataSource
        {
            get;
            set;
        }

        public object[] ProductQtyDataSource
        {
            get;
            set;
        }

        public string YearTargetValue
        {
            get;
            set;
        }

        #endregion

        #region 事件

        public SSCodeProductMessageControl()
        {
            InitializeComponent();
        }

        private void SSCodeProductMessageControl_Load(object sender, EventArgs e)
        {
            
        }      

        #endregion

        #region 自定义函数

        public void SetControlsValue()
        {
            InitialDataGridViewProdcut();            
            SetDataChartValue();
            SetDataGirdValue();            
            hearMessageControl.BigLine = _HeaderLineMessage;
            exceptionMessageControl.ExpectionMessage = _ExceptionMessageList;
            normalMessageControl.PlanQty = _PlanQty.ToString();
            normalMessageControl.OnPostMan = _OnPostMan;
            normalMessageControl.OutPutQty = _SSCodeOutPutQty.ToString();
            if (_PlanQty != 0)
            {
                normalMessageControl.PlanPassRate = (Math.Round(((double)_SSCodeOutPutQty / _PlanQty),4)).ToString("0.##%");
            }
            else
            {
                normalMessageControl.PlanPassRate = "0%";
            }

            hearMessageControl.Refresh();
            exceptionMessageControl.Refresh();
            normalMessageControl.Refresh();            
        }

        //初始化ProductGrid
        private void InitialDataGridViewProdcut()
        {
            this.m_ProductDataTable = new DataTable();

            this.m_ProductDataTable.Columns.Add("ItemCode", typeof(string));
            this.m_ProductDataTable.Columns.Add("ItemName", typeof(string));
            this.m_ProductDataTable.Columns.Add("DayPlanQty", typeof(int));
            this.m_ProductDataTable.Columns.Add("PerTimeOutPutQty", typeof(int));
            this.m_ProductDataTable.Columns.Add("PassRate", typeof(string));
            this.m_ProductDataTable.Columns.Add("OneNeedTime", typeof(double));
            this.m_ProductDataTable.Columns.Add("UPPH", typeof(double));

            this.m_ProductDataTable.AcceptChanges();

            this.dataGridViewProdcut.DataSource = this.m_ProductDataTable;
            this.dataGridViewProdcut.Columns[0].FillWeight = 15;
            this.dataGridViewProdcut.Columns[1].FillWeight = 25;
            this.dataGridViewProdcut.Columns[2].FillWeight = 10;
            this.dataGridViewProdcut.Columns[3].FillWeight = 10;
            this.dataGridViewProdcut.Columns[4].FillWeight = 10;
            this.dataGridViewProdcut.Columns[5].FillWeight = 10;
            this.dataGridViewProdcut.Columns[6].FillWeight = 10;

            this.dataGridViewProdcut.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewProdcut.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewProdcut.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewProdcut.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewProdcut.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dataGridViewProdcut.Columns[0].HeaderText = "产品代码";
            this.dataGridViewProdcut.Columns[1].HeaderText = "产品描述";
            this.dataGridViewProdcut.Columns[2].HeaderText = "日计划产量";
            this.dataGridViewProdcut.Columns[3].HeaderText = "下线数量";
            this.dataGridViewProdcut.Columns[4].HeaderText = "直通率";
            this.dataGridViewProdcut.Columns[5].HeaderText = "台耗工时";
            this.dataGridViewProdcut.Columns[6].HeaderText = "UPPH";            
        }

        private void SetDataChartValue()
        {            
            this.DataChart.ChartAreas[0].AxisX.Interval = 1;
            this.DataChart.ChartAreas[0].AxisX.IntervalOffset = 1;
            this.DataChart.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            this.DataChart.Series[0].Points.Clear();
            this.DataChart.Series[1].Points.Clear();
            #region 一周内产线产量图例

            if (ProductQtyDataSource != null)
            {
                foreach (NewReportDomainObject obj in ProductQtyDataSource)
                {
                    this.DataChart.Series[0].Points.AddXY(obj.ShiftDay, obj.Output);
                }
                this.DataChart.Series[0].IsValueShownAsLabel = true;
                this.DataChart.Series[0].IsVisibleInLegend = true;
                this.DataChart.Series[0].Name = "日产量";
            }

            #endregion

            #region 一周内产线直通率图例

            if (PassQtyDataSource != null)
            {
                foreach (NewReportDomainObject obj in PassQtyDataSource)
                {
                    this.DataChart.Series[1].Points.AddXY(obj.ShiftDay, Math.Round(obj.PassRcardRate, 4));
                }
                this.DataChart.Series[1].IsValueShownAsLabel = true;
                this.DataChart.Series[1].IsVisibleInLegend = true;
                this.DataChart.Series[1].Name = "直通率";
            }

            #endregion

            #region 直通率年度指标
            if (YearTargetValue != string.Empty)
            {
                StripLine sl1 = new StripLine();
                sl1.BackColor = System.Drawing.Color.Red;
                sl1.IntervalOffset = Double.Parse(YearTargetValue.ToString());
                sl1.StripWidth = 0.001;
                sl1.Text = "直通率年度指标" + string.Format("{0:0%}", Double.Parse(YearTargetValue));
                sl1.TextAlignment = StringAlignment.Near;
                sl1.Font = new Font("新宋体", 10, FontStyle.Bold); 
                DataChart.ChartAreas[0].AxisY.StripLines.Add(sl1);
            }
            #endregion            

            this.DataChart.Refresh();
        }              

        private void SetDataGirdValue()
        {
            if (_ProductGridDataSource != null)
            {
                foreach (WatchPanelProductDate obj in _ProductGridDataSource)
                {
                    if (CheckValueIsZero(obj))
                    {
                        DataRow newRow = this.m_ProductDataTable.NewRow();

                        newRow["ItemCode"] = obj.ItemCode;
                        newRow["ItemName"] = obj.ItemName;
                        newRow["DayPlanQty"] = obj.DayPlanQty;
                        newRow["PerTimeOutPutQty"] = obj.PerTimeOutPutQty;
                        newRow["PassRate"] = obj.PassRate.ToString("0.##%");
                        newRow["OneNeedTime"] = Math.Round(obj.OneNeedTime, 4);
                        newRow["UPPH"] = Math.Round(obj.UPPH, 4);

                        this.m_ProductDataTable.Rows.Add(newRow);
                    }
                }
            }

            this.m_ProductDataTable.AcceptChanges();           
        }       

        private bool CheckValueIsZero(WatchPanelProductDate obj)
        {
            if (obj.DayPlanQty == 0 && obj.PerTimeOutPutQty == 0 && obj.PassRate == 0
                && obj.OneNeedTime == 0 && obj.UPPH == 0)
            {
                return false;
            }
            return true;
        }
        #endregion

        


    }
}
