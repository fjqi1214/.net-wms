using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.WatchPanel;
using BenQGuru.eMES.WebQuery;
using Steema.TeeChart.Styles;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class FLIneWatchPanel : Form
    {
        #region 变量 属性

        private DataTable m_ProductDataTable = null;
        private DataTable m_PassRateTable = null;
        private DataTable m_OutPutQtyTable = null;
        private IDomainDataProvider _dataProvider;
        private int _NowDBDate = 0;
        public string _bigLine = string.Empty;
        private ArrayList _ShiftCodeList = new ArrayList();

        private IDomainDataProvider DataProvider
        {
            get
            {
                if (_dataProvider == null)
                {

                    _dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();
                }
                return _dataProvider;
            }

            set
            {
                _dataProvider = value;
            }
        }

        public string BigLine
        {
            get { return _bigLine; }
            set { _bigLine = value; }
        }

        private int NowDBDate
        {
            get
            {
                return _NowDBDate;
            }

            set
            {
                _NowDBDate = value;
            }
        }

        #endregion

        #region 页面事件

        public FLIneWatchPanel()
        {
            InitializeComponent();
        }

        public FLIneWatchPanel(string bigLineCode)
        {
            InitializeComponent();
            this.BigLine = bigLineCode;
        }

        private void FLIneWatchPanel_Load(object sender, EventArgs e)
        {
            //初始化信息     
            GetShiftCodeMessageByBigSSCode();
            InitialultraProdcutDataGrid();
            InitialHeaderMessage();
            InitTimer();

            RefreshExceptionMessage();
            RefreshNormalProductMessage();
            RefreshNormalDataGridMessage();
            RefreshPassRateTChart();
            RefreshOutPutQtyTChart();
        }

        //Exit
        private void FLIneWatchPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.Close();
            }
        }

        //初始化Ｇｒｉｄ
        private void ultraProdcutDataGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = true;
            //e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            //e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.Override.DefaultRowHeight = 100;
            
            e.Layout.Override.RowSizing = RowSizing.Free;
            this.ultraProdcutDataGrid.DisplayLayout.Override.RowSizing = RowSizing.Free;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

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

        private void timerException_Tick(object sender, EventArgs e)
        {
            try
            {
                RefreshExceptionMessage();
            }
            catch
            { }
        }

        private void timerThreeArea_Tick(object sender, EventArgs e)
        {
            try
            {
                RefreshNormalProductMessage();
                RefreshNormalDataGridMessage();
                RefreshPassRateTChart();
                RefreshOutPutQtyTChart();
            }
            catch
            { }
        }

        #endregion

        #region  自定义事件

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

        //刷新Header信息
        private void InitialHeaderMessage()
        {
            this.userControlHearderMessqge.BigLine = _bigLine;
        }

        //刷新异常信息
        private void RefreshExceptionMessage()
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);

            object[] exceptionList = watchPanelFacade.QueryExceptionList(this.BigLine, this.NowDBDate);
            if (exceptionList != null && exceptionList.Length > 0)
            {
                string exceptionMessageList = string.Empty;
                string cuerrtExecptionMessage = string.Empty;
                this.userControlExceptionMessage.ClearMessage();
                int showNumber = 1;
                foreach (ExceptionEventWithDescription obj in exceptionList)
                {
                    cuerrtExecptionMessage = obj.ItemCode + "-" + obj.Description + "-" + obj.Memo + "(" + FormatHelper.ToTimeString(obj.BeginTime, ":") + "-" + FormatHelper.ToTimeString(obj.EndTime, ":") + ")" + "\r\n";

                    exceptionMessageList += showNumber.ToString() + ": " + cuerrtExecptionMessage;
                    showNumber += 1;
                }

                this.userControlExceptionMessage.ExpectionMessage = exceptionMessageList;
                this.userControlExceptionMessage.Refresh();
            }
        }

        //刷新常规信息
        private void RefreshNormalProductMessage()
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);

            int PlanQty = watchPanelFacade.GetWorkPlanQty(this.BigLine, this.NowDBDate);
            int bigSSCodeOutPutQty = watchPanelFacade.GetBigSSCodeOutPutQty(this.BigLine, this.NowDBDate);
            int onPostManCount = watchPanelFacade.GetOnPostManCount(this.BigLine, this.NowDBDate);

            string stringCrewList = string.Empty;
            object[] crewList = watchPanelFacade.QueryCrewList(this.BigLine, this.NowDBDate);
            if (crewList != null)
            {
                foreach (Line2Crew obj in crewList)
                {
                    stringCrewList += "," + obj.CrewCode;
                }
            }

            if (stringCrewList.Trim().Length > 1)
            {
                stringCrewList = stringCrewList.Substring(1);
            }


            this.userControlNormalMessage.PlanQty = PlanQty.ToString();
            this.userControlNormalMessage.OnPostManCount = onPostManCount.ToString();
            this.userControlNormalMessage.OutPutQty = bigSSCodeOutPutQty.ToString();
            this.userControlNormalMessage.Crew = stringCrewList;
            this.userControlNormalMessage.Refresh();

        }

        //刷新生产数据信息
        private void RefreshNormalDataGridMessage()
        {
            //获取标准机型工时
            //double standardWorkingTime = 0;
            //SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            //double.TryParse(systemSettingFacade.GetParameterAlias("PERFORMANCEREPORT", "STANDARDWORKINGTIME"), out standardWorkingTime);
            //if (standardWorkingTime == 0)
            //{
            //    standardWorkingTime = 1;
            //}


            this.m_ProductDataTable.Clear();
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            string tpCode = string.Empty;
            TimePeriod timePeriod = (TimePeriod)watchPanelFacade.GettimePeriod(this.BigLine);
            if (timePeriod != null)
            {
                tpCode = timePeriod.TimePeriodCode;
            }

            object[] productDataList = watchPanelFacade.QueryProductData(this.BigLine, this.NowDBDate, _ShiftCodeList, tpCode);

            if (productDataList != null)
            {
                foreach (watchPanelProductDate obj in productDataList)
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
                this.m_ProductDataTable.AcceptChanges();
            }
            this.ultraProdcutDataGrid.ActiveRow = null;
        }

        //刷新直通率
        private void RefreshPassRateTChart()
        {
            this.m_PassRateTable = new DataTable();
            this.m_PassRateTable.Columns.Add("TimePeriodCode", typeof(string));
            this.m_PassRateTable.Columns.Add("PassRate", typeof(string));

            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            object[] RateDataList = watchPanelFacade.QueryPassRateData(this.BigLine, this.NowDBDate);

            if (RateDataList != null)
            {
                DataRow newRow;
                foreach (NewReportDomainObject obj in RateDataList)
                {
                    newRow = this.m_PassRateTable.NewRow();

                    newRow["TimePeriodCode"] = obj.PeriodCode;
                    newRow["PassRate"] = Math.Round(obj.PassRcardRate * 100, 2);
                    this.m_PassRateTable.Rows.Add(newRow);
                }                
            }

            this.m_PassRateTable.AcceptChanges();

            RateLine.Color = Color.White;
            RateLine.LinePen.Width = 2;
            RateLine.YValues.DataMember = m_PassRateTable.Columns["PassRate"].ToString();
            RateLine.LabelMember = m_PassRateTable.Columns["TimePeriodCode"].ToString();

            RateLine.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value;
            RateLine.DataSource = m_PassRateTable;

            this.DataChart.Refresh();
        }

        //刷新产量信息
        private void RefreshOutPutQtyTChart()
        {
            this.m_OutPutQtyTable = new DataTable();
            this.m_OutPutQtyTable.Columns.Add("TimePeriodCode", typeof(string));
            this.m_OutPutQtyTable.Columns.Add("OutPutQty", typeof(int));

            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            object[] RateDataList = watchPanelFacade.QueryOutPutQtyData(this.BigLine, this.NowDBDate);

            if (RateDataList != null)
            {
                DataRow newRow;
                foreach (NewReportDomainObject obj in RateDataList)
                {
                    newRow = this.m_OutPutQtyTable.NewRow();

                    newRow["TimePeriodCode"] = obj.PeriodCode;
                    newRow["OutPutQty"] = obj.Output;

                    this.m_OutPutQtyTable.Rows.Add(newRow);
                }               
            }

            this.m_OutPutQtyTable.AcceptChanges();

            OutPutJoin.JoinPen.Color = Color.White;
            OutPutJoin.JoinPen.Width = 2;
            OutPutJoin.LabelMember = m_OutPutQtyTable.Columns["TimePeriodCode"].ToString();
            OutPutJoin.YValues.DataMember = m_OutPutQtyTable.Columns["OutPutQty"].ToString();
            OutPutJoin.Marks.Style = Steema.TeeChart.Styles.MarksStyles.Value;
            OutPutJoin.DataSource = m_OutPutQtyTable;

            this.DataChart.Refresh();
        }

        private void GetShiftCodeMessageByBigSSCode()
        {
            //获取当前产线的所有班次
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            object[] shiftCodeList = watchPanelFacade.QueryShiftCode(this.BigLine,string.Empty);
            //end

            if (shiftCodeList != null && shiftCodeList.Length > 0)
            {
                foreach (Shift obj in shiftCodeList)
                {
                    _ShiftCodeList.Add(obj);
                }
            }
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

        private void InitTimer()
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("//LineTypeConfig[@ConfigName='TimerConfig']");

            if (xmlNode != null)
            {
                XmlNodeList xmlNodeList = xmlNode.ChildNodes;

                foreach (XmlNode xn in xmlNodeList)
                {
                    XmlElement xe = (XmlElement)xn;
                    if (xe.Name == "timerExceptionInterval")
                    {
                        this.timerException.Interval = int.Parse(xe.InnerText);
                    }

                    if (xe.Name == "timerThreeAreaInterval")
                    {
                        this.timerThreeArea.Interval = int.Parse(xe.InnerText);
                    }

                    if (xe.Name == "CuerrtDay")
                    {
                        if (string.IsNullOrEmpty(xe.InnerText))
                        {
                            _NowDBDate = FormatHelper.GetNowDBDateTime(this.DataProvider).DBDate;
                        }
                        else
                        {
                            this.NowDBDate = int.Parse(xe.InnerText);
                        }
                    }
                }
            }

            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            string shiftTypeCode = string.Empty;
            TimePeriod timePeriod = (TimePeriod)watchPanelFacade.GettimePeriod(this.BigLine);
            if (timePeriod != null)
            {
                shiftTypeCode = timePeriod.ShiftTypeCode;
            }

            _NowDBDate = watchPanelFacade.GetShiftDay(shiftTypeCode, _NowDBDate,FormatHelper.GetNowDBDateTime(this.DataProvider).DBTime);

            this.timerException.Enabled = true;
            this.timerException.Enabled = true;
        }

        private bool CheckValueIsZero(watchPanelProductDate obj)
        {
            if (obj.DayPlanQty==0 && obj.PerTimeOutPutQty==0 && obj.PassRate==0
                && obj.OneNeedTime==0 && obj.UPPH==0 && obj.ShiftLineOutPut1==0
                && obj.ShiftLineOutPut2==0 && obj.ShiftLineOutPut3==0 && obj.ShiftLineOutPut4==0
                && obj.ShiftLineOutPut5==0 && obj.ShiftLineOutPut6==0 && obj.ShiftLineOutPut7==0
                && obj.ShiftLineOutPut8==0 && obj.ShiftLineOutPut9==0 && obj.ShiftLineOutPut10==0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}