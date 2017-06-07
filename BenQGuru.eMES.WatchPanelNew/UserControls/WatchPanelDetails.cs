using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;

namespace BenQGuru.eMES.WatchPanelNew
{
    public partial class WatchPanelDetails : System.Windows.Forms.UserControl
    {
        private DataTable m_ProductDataTable = null;
        public WatchPanelDetails()
        {
            InitializeComponent();
        }

        #region 变量
        public string _sscode
        {
            get;
            set;
        }

        public string _workShopCode
        {
            get;
            set;
        }

        public double _maxLineCount
        {
            get;
            set;
        }

        public double _pageScrolling
        {
            get;
            set;
        }

        public object[] panlDetailsDataSource
        {
            get;
            set;
        }

        private int _loadedCount = 0;  //记录Grid数据加载了多少条

        #endregion

        #region  初始化看板信息
        public void InitControlsValue()
        {
            InitTimer();
            if (!string.IsNullOrEmpty(_sscode))
            {
                //产线看板
                this.hearMessageControl1.SetTitle = string.Format("产线{0}电子看板\n Production Line Overview", _sscode);
            }
            else
            {
                //车间看板
                this.hearMessageControl1.SetTitle = string.Format("车间{0}电子看板\n WorkShop Overview", _workShopCode);
            }
            InitialDataGridViewProdcut();

            SetDataGirdValue(_maxLineCount);
        }

        private void WatchPanelDetails_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Grid初始化及数据加载
        //初始化ProductGrid
        private void InitialDataGridViewProdcut()
        {
            this.m_ProductDataTable = new DataTable();

            this.m_ProductDataTable.Columns.Add("ShiftDay", typeof(string));
            this.m_ProductDataTable.Columns.Add("ssCode", typeof(string));
            this.m_ProductDataTable.Columns.Add("OrderNO", typeof(string));
            this.m_ProductDataTable.Columns.Add("MoCode", typeof(string));
            this.m_ProductDataTable.Columns.Add("PlanQty", typeof(double));  //计划数量
            this.m_ProductDataTable.Columns.Add("InputQty", typeof(double)); //投入数量
            this.m_ProductDataTable.Columns.Add("OutQty", typeof(double));  //产出数量

            this.m_ProductDataTable.Columns.Add("AchievementRate", typeof(string));  //工单达成率
            this.m_ProductDataTable.Columns.Add("PassYield", typeof(string));   //直通率

            this.m_ProductDataTable.AcceptChanges();

            this.dataGridViewProdcut.DataSource = this.m_ProductDataTable;
            this.dataGridViewProdcut.Columns[0].FillWeight = 11;
            this.dataGridViewProdcut.Columns[1].FillWeight = 12;
            this.dataGridViewProdcut.Columns[2].FillWeight = 12;
            this.dataGridViewProdcut.Columns[3].FillWeight = 15;
            this.dataGridViewProdcut.Columns[4].FillWeight = 10;
            this.dataGridViewProdcut.Columns[5].FillWeight = 10;
            this.dataGridViewProdcut.Columns[6].FillWeight = 10;
            this.dataGridViewProdcut.Columns[7].FillWeight = 10;
            this.dataGridViewProdcut.Columns[8].FillWeight = 10;

            this.dataGridViewProdcut.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewProdcut.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewProdcut.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewProdcut.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewProdcut.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewProdcut.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewProdcut.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewProdcut.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewProdcut.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            this.dataGridViewProdcut.Columns[0].HeaderText = "生产日期";
            this.dataGridViewProdcut.Columns[1].HeaderText = "产线代码";
            this.dataGridViewProdcut.Columns[2].HeaderText = "销售订单";
            this.dataGridViewProdcut.Columns[3].HeaderText = "工单代码";
            this.dataGridViewProdcut.Columns[4].HeaderText = "计划数量";
            this.dataGridViewProdcut.Columns[5].HeaderText = "投入数量";
            this.dataGridViewProdcut.Columns[6].HeaderText = "完工数量";
            this.dataGridViewProdcut.Columns[7].HeaderText = "工单达成率";
            this.dataGridViewProdcut.Columns[8].HeaderText = "直通率";

        }

        //加载数据
        public void SetDataGirdValue(double maxLoadCount)
        {
            if (panlDetailsDataSource != null)
            {
                string returnValue = string.Empty;
                int loopCount = 0;   //记录当前循环是第几次循环
                int addCount = 0;    //记录当前往m_ProductDataTable中添加了几笔数据
                InitialDataGridViewProdcut();
                //重新循环一次
                if (_loadedCount >= panlDetailsDataSource.Length)
                {
                    _loadedCount = 0;
                }
                foreach (PanelDetailsData obj in panlDetailsDataSource)
                {
                    loopCount++;
                    if (loopCount <= _loadedCount)
                    {
                        continue;
                    }
                    if (addCount == maxLoadCount)
                    {
                        break;
                    }
                    DataRow newRow = this.m_ProductDataTable.NewRow();

                    newRow["ShiftDay"] = obj.ShiftDay;
                    newRow["ssCode"] = obj.SSCode;
                    newRow["OrderNO"] = obj.OrderNo;
                    newRow["MoCode"] = obj.MoCode;
                    newRow["PlanQty"] = obj.MoPlanQty;
                    newRow["InputQty"] = obj.MoInputOty;
                    newRow["OutQty"] = obj.MoOutQty;
                    newRow["AchievementRate"] = obj.AchievementRate.ToString("0.##%");
                    newRow["PassYield"] = obj.PassYield.ToString("0.##%");
                    this.m_ProductDataTable.Rows.Add(newRow);
                    _loadedCount++;
                    addCount++;
                }
            }

            this.m_ProductDataTable.AcceptChanges();
        }
        #endregion


        private void GridViewTimer_Tick(object sender, EventArgs e)
        {
            SetDataGirdValue(_maxLineCount);
        }

        #region 辅助函数
        private void InitTimer()
        {
            this.GridViewTimer.Enabled = true;
            this.GridViewTimer.Interval = Convert.ToInt32(_pageScrolling * 1000);
        }
        #endregion
    }
}
