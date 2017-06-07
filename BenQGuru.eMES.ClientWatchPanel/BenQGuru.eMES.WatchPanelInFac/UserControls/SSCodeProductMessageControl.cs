using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.WebQuery;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class SSCodeProductMessageControl : System.Windows.Forms.UserControl
    {

        #region 变量

        private DataTable m_ProductDataTable = null;
        private DataTable m_PassRateTable = null;
        private DataTable m_OutPutQtyTable = null;
        private ArrayList _ShiftCodeList = new ArrayList();

        private string _BigLineCode = string.Empty;
        private string _CrewCodeList = string.Empty;
        private string _ExceptionMessageList = string.Empty;
        private string _HeaderLineMessage = string.Empty;

        private int _PlanQty = 0;
        private int _OnPostManCount = 0;
        private int _BigSSCodeOutPutQty = 0;

        private object[] _ProductGridDataSource = null;
        private object[] _RateLineTChartDataSource = null;
        private object[] _BarJoinTChartDataSource = null;


        public string BigLineCode
        {
            set { _BigLineCode = value; }
        }

        public string CrewCodeList
        {
            set { _CrewCodeList = value; }
        }

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

        public int OnPostManCount
        {
            set { _OnPostManCount = value; }
        }

        public int BigSSCodeOutPutQty
        {
            set { _BigSSCodeOutPutQty = value; }
        }

        public object[] ProductGridDataSource
        {
            set { _ProductGridDataSource = value; }
        }

        public object[] RateLineTChartDataSource
        {
            set { _RateLineTChartDataSource = value; }
        }

        public object[] BarJoinTChartDataSource
        {
            set { _BarJoinTChartDataSource = value; }
        }

        public ArrayList ShiftCodeList
        {
            set { _ShiftCodeList = value; }
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
        private void ultraProdcutDataGrid_InitializeLayout_1(object sender, InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = true;

            e.Layout.Override.RowSizing = RowSizing.Free;
            this.ultraProdcutDataGrid.DisplayLayout.Override.RowSizing = RowSizing.Free;
            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            //e.Layout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Inset;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["ItemCode"].Header.Caption = "产品代码";
            e.Layout.Bands[0].Columns["ItemName"].Header.Caption = "产品描述";//Added By Nettie Chen 2009/09/23
            e.Layout.Bands[0].Columns["MaterialModelCode"].Header.Caption = "机型";
            e.Layout.Bands[0].Columns["DayPlanQty"].Header.Caption = "日计划";

            for (int i = 1; i <= _ShiftCodeList.Count; i++)
            {
                string shiftCodeKey = "ShiftCode" + i.ToString();
                e.Layout.Bands[0].Columns[shiftCodeKey].Header.Caption = "班次" + i.ToString();
            }

            e.Layout.Bands[0].Columns["PerTimeOutPutQty"].Header.Caption = "本时段";
            e.Layout.Bands[0].Columns["PassRate"].Header.Caption = "直通率";
            e.Layout.Bands[0].Columns["OneNeedTime"].Header.Caption = "台耗工时";
            e.Layout.Bands[0].Columns["UPPH"].Header.Caption = "UPPH";

            e.Layout.Bands[0].Columns["ItemCode"].Width = 130;
            e.Layout.Bands[0].Columns["ItemName"].Width = 200;//Added By Nettie Chen 2009/09/23
            e.Layout.Bands[0].Columns["MaterialModelCode"].Width = 130;
            e.Layout.Bands[0].Columns["DayPlanQty"].Width = 100;
            e.Layout.Bands[0].Columns["DayPlanQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

            for (int i = 1; i <= _ShiftCodeList.Count; i++)
            {
                string shiftCodeKey = "ShiftCode" + i.ToString();
                //Modified By Nettie Chen 2009/09/23
                //e.Layout.Bands[0].Columns[shiftCodeKey].Width = 130;
                e.Layout.Bands[0].Columns[shiftCodeKey].Width = 100;
                //End Modified
                e.Layout.Bands[0].Columns[shiftCodeKey].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            }
            //Modified By Nettie Chen 2009/09/23
            //e.Layout.Bands[0].Columns["PerTimeOutPutQty"].Width = 120;
            e.Layout.Bands[0].Columns["PerTimeOutPutQty"].Width = 100;
            //End Modified
            
            e.Layout.Bands[0].Columns["PerTimeOutPutQty"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            e.Layout.Bands[0].Columns["PassRate"].Width = 120;
            e.Layout.Bands[0].Columns["PassRate"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            e.Layout.Bands[0].Columns["OneNeedTime"].Width = 120;
            e.Layout.Bands[0].Columns["OneNeedTime"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            e.Layout.Bands[0].Columns["UPPH"].Width = 110;
            e.Layout.Bands[0].Columns["UPPH"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["ItemCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["ItemName"].CellActivation = Activation.NoEdit;//Added By Nettie Chen 2009/09/23
            e.Layout.Bands[0].Columns["MaterialModelCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["DayPlanQty"].CellActivation = Activation.NoEdit;

            for (int i = 1; i <= _ShiftCodeList.Count; i++)
            {
                string shiftCodeKey = "ShiftCode" + i.ToString();
                e.Layout.Bands[0].Columns[shiftCodeKey].CellActivation = Activation.NoEdit;
            }
            e.Layout.Bands[0].Columns["PerTimeOutPutQty"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["PassRate"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["OneNeedTime"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["UPPH"].CellActivation = Activation.NoEdit;
        }

        #endregion

        #region 自定义函数

        public void SetControlsValue()
        {
            InitialultraProdcutDataGrid();

            SetOutPutJoinValue();
            SetRateLineValue();
            SetDataGirdValue();

            hearMessageControl.BigLine = _HeaderLineMessage;
            exceptionMessageControl.ExpectionMessage = _ExceptionMessageList;
            normalMessageControl.PlanQty = _PlanQty.ToString();
            normalMessageControl.OnPostManCount = _OnPostManCount.ToString();
            normalMessageControl.OutPutQty = _BigSSCodeOutPutQty.ToString();
            normalMessageControl.Crew = _CrewCodeList;

            hearMessageControl.Refresh();
            exceptionMessageControl.Refresh();
            normalMessageControl.Refresh();
        }

        //初始化ProductGrid
        private void InitialultraProdcutDataGrid()
        {
            this.m_ProductDataTable = new DataTable();

            this.m_ProductDataTable.Columns.Add("ItemCode", typeof(string));
            this.m_ProductDataTable.Columns.Add("ItemName", typeof(string));//Added By Nettie Chen 2009/09/23
            this.m_ProductDataTable.Columns.Add("MaterialModelCode", typeof(string));
            this.m_ProductDataTable.Columns.Add("DayPlanQty", typeof(int));

            for (int i = 1; i <= _ShiftCodeList.Count; i++)
            {
                string shiftCodeKey = "ShiftCode" + i.ToString();
                this.m_ProductDataTable.Columns.Add(shiftCodeKey, typeof(string));
            }

            this.m_ProductDataTable.Columns.Add("PerTimeOutPutQty", typeof(int));
            this.m_ProductDataTable.Columns.Add("PassRate", typeof(string));
            this.m_ProductDataTable.Columns.Add("OneNeedTime", typeof(double));
            this.m_ProductDataTable.Columns.Add("UPPH", typeof(double));

            this.m_ProductDataTable.AcceptChanges();

            this.ultraProdcutDataGrid.DataSource = this.m_ProductDataTable;
        }

        private void SetOutPutJoinValue()
        {
            this.m_OutPutQtyTable = new DataTable();
            this.m_OutPutQtyTable.Columns.Add("TimePeriodCode", typeof(string));
            this.m_OutPutQtyTable.Columns.Add("OutPutQty", typeof(int));

            if (_BarJoinTChartDataSource != null)
            {
                foreach (NewReportDomainObject obj in _BarJoinTChartDataSource)
                {
                    DataRow newRow = this.m_OutPutQtyTable.NewRow();

                    newRow["TimePeriodCode"] = obj.PeriodCode;
                    newRow["OutPutQty"] = obj.Output;

                    this.m_OutPutQtyTable.Rows.Add(newRow);
                }

                this.m_OutPutQtyTable.AcceptChanges();

                OutPutJoin.JoinPen.Color = Color.White;
                OutPutJoin.JoinPen.Width = 2;
                OutPutJoin.LabelMember = m_OutPutQtyTable.Columns["TimePeriodCode"].ToString();
                OutPutJoin.YValues.DataMember = m_OutPutQtyTable.Columns["OutPutQty"].ToString();
                OutPutJoin.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value;
                OutPutJoin.DataSource = m_OutPutQtyTable;
            }


            this.DataChart.Refresh();
        }

        private void SetRateLineValue()
        {
            this.m_PassRateTable = new DataTable();
            this.m_PassRateTable.Columns.Add("TimePeriodCode", typeof(string));
            this.m_PassRateTable.Columns.Add("PassRate", typeof(string));

            if (_RateLineTChartDataSource != null)
            {
                foreach (NewReportDomainObject obj in _RateLineTChartDataSource)
                {
                    DataRow newRow = this.m_PassRateTable.NewRow();

                    newRow["TimePeriodCode"] = obj.PeriodCode;
                    newRow["PassRate"] = Math.Round(obj.PassRcardRate * 100, 2);
                    this.m_PassRateTable.Rows.Add(newRow);
                }

                this.m_PassRateTable.AcceptChanges();

                RateLine.Color = Color.White;
                RateLine.YValues.DataMember = m_PassRateTable.Columns["PassRate"].ToString();
                RateLine.LabelMember = m_PassRateTable.Columns["TimePeriodCode"].ToString();

                RateLine.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value;
                RateLine.DataSource = m_PassRateTable;
            }

            this.DataChart.Refresh();
        }

        private void SetDataGirdValue()
        {
            if (_ProductGridDataSource != null)
            {
                foreach (watchPanelProductDate obj in _ProductGridDataSource)
                {
                    if (CheckValueIsZero(obj))
                    {
                        DataRow newRow = this.m_ProductDataTable.NewRow();

                        newRow["ItemCode"] = obj.ItemCode;
                        newRow["ItemName"] = obj.ItemName;//Added By Nettie Chen 2009/09/23
                        newRow["MaterialModelCode"] = obj.MaterialModelCode;
                        newRow["DayPlanQty"] = obj.DayPlanQty;

                        SetDataGirdShiftLineOutPutQtyValue(obj, ref newRow);

                        newRow["PerTimeOutPutQty"] = obj.PerTimeOutPutQty;
                        newRow["PassRate"] = obj.PassRate.ToString("0.00%");
                        newRow["OneNeedTime"] = Math.Round(obj.OneNeedTime, 4);
                        newRow["UPPH"] = Math.Round(obj.UPPH, 4);

                        this.m_ProductDataTable.Rows.Add(newRow);
                    }
                }
            }

            this.m_ProductDataTable.AcceptChanges();

            this.ultraProdcutDataGrid.ActiveRow = null;
        }

        //给班次下线赋值,班次不固定
        private void SetDataGirdShiftLineOutPutQtyValue(watchPanelProductDate obj, ref DataRow newRow)
        {
            for (int i = 1; i <= _ShiftCodeList.Count; i++)
            {
                string shiftCodeKey = "ShiftCode" + i.ToString();

                if (i == 1)
                {
                    newRow[shiftCodeKey] = obj.ShiftLineOutPut1;
                }

                if (i == 2)
                {
                    newRow[shiftCodeKey] = obj.ShiftLineOutPut2;
                }

                if (i == 3)
                {
                    newRow[shiftCodeKey] = obj.ShiftLineOutPut3;
                }

                if (i == 4)
                {
                    newRow[shiftCodeKey] = obj.ShiftLineOutPut4;
                }

                if (i == 5)
                {
                    newRow[shiftCodeKey] = obj.ShiftLineOutPut5;
                }

                if (i == 6)
                {
                    newRow[shiftCodeKey] = obj.ShiftLineOutPut6;
                }

                if (i == 7)
                {
                    newRow[shiftCodeKey] = obj.ShiftLineOutPut7;
                }

                if (i == 8)
                {
                    newRow[shiftCodeKey] = obj.ShiftLineOutPut8;
                }

                if (i == 9)
                {
                    newRow[shiftCodeKey] = obj.ShiftLineOutPut9;
                }

                if (i == 10)
                {
                    newRow[shiftCodeKey] = obj.ShiftLineOutPut10;
                }
            }
        }

        private bool CheckValueIsZero(watchPanelProductDate obj)
        {
            if (obj.DayPlanQty == 0 && obj.PerTimeOutPutQty == 0 && obj.PassRate == 0
                && obj.OneNeedTime == 0 && obj.UPPH == 0 && obj.ShiftLineOutPut1 == 0
                && obj.ShiftLineOutPut2 == 0 && obj.ShiftLineOutPut3 == 0 && obj.ShiftLineOutPut4 == 0
                && obj.ShiftLineOutPut5 == 0 && obj.ShiftLineOutPut6 == 0 && obj.ShiftLineOutPut7 == 0
                && obj.ShiftLineOutPut8 == 0 && obj.ShiftLineOutPut9 == 0 && obj.ShiftLineOutPut10 == 0)
            {
                return false;
            }
            return true;
        }
        #endregion


    }
}
