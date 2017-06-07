using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.WatchPanel;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class FFacWatchPanel : Form
    {
        #region  变量
        //隐藏光标
        [DllImport("user32.dll")]
        static extern Int32 CreateCaret(Int32 hwnd, Int32 hBitmap, Int32 nWidth, Int32 nHeight);
        [DllImport("user32.dll")]
        static extern Int32 ShowCaret(Int32 hwnd);
        [DllImport("user32.dll")]
        static extern Int32 DestroyCaret();

        //配置文件名称以及各个配置值
        private string _configTypeName = string.Empty;
        private string _BigLineList = string.Empty;
        private string _GourpBy = string.Empty;
        private bool _IsShowFacSurvey = true;
        private bool _IsShowFacWatchPanel = true;
        private bool _IsShowFacQtyAndRate = true;
        private bool _IsShowLineSurvey = true;
        //Added By Nettie Chen 2009/09/23
        private bool _IsShowFinishedProduct = true;
        private bool _IsShowSemimanuProduct = true;
        //End Added
        private double _AutoRefresh = 10000;
        private double _WatchRefresh = 10000;
        private int _SetShowBigLineNumber = 0;
        private int _LineWatchPaneNunmer = 0;
        private int _LineWatchPaneControlID = 0;
        private int _CuerrtDay = 0;

        //显示画面的序号
        private int _UserControlID = 0;

        private IDomainDataProvider _dataProvider;
        private WatchPanelFacade _WatchPanelFacade;
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
        #endregion

        #region 页面事件

        public FFacWatchPanel()
        {
            InitializeComponent();
        }

        public FFacWatchPanel(string configTypeName)
        {
            InitializeComponent();
            _configTypeName = configTypeName;
            InitValues();
            InitShowWatch();
            InitTimer();
        }

        private void FFacWatchPanel_Load(object sender, EventArgs e)
        {

        }

        private void FFacWatchPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.AutoTimer.Enabled = false;
                this.WatchTimer.Enabled = false;
                this.Close();
            }
        }

        //数据的Load
        private void AutoTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _WatchPanelFacade = new WatchPanelFacade(this.DataProvider);
                if (_IsShowFacSurvey && _UserControlID == 1)
                {
                    this.GetFacSurevyFromXML();
                    FacMessageControl control = (FacMessageControl)this.mainLayout.GetControlFromPosition(0, 0);
                    control.RTF = FacConfigMessage.CommonInfo;
                    control.ValueRefresh();
                }

                if (_IsShowFacWatchPanel && _UserControlID == 2)
                {
                    FacProductMessageControl FacProductMessageControl = (FacProductMessageControl)this.mainLayout.GetControlFromPosition(0, 0);
                    SetFacProductMessageControlValue(FacProductMessageControl);
                    FacProductMessageControl.Dock = DockStyle.Fill;
                }

                if (_IsShowFacQtyAndRate && _UserControlID == 3)
                {
                    TChartControl tChartControl = (TChartControl)mainLayout.GetControlFromPosition(0, 0);
                    SetFacTChartControlValue(tChartControl, false);
                    tChartControl.Refresh();
                }

                if (_IsShowLineSurvey && _UserControlID == 4)
                {
                    string[] bigsscode = _BigLineList.Split(',');

                    SSCodeProductMessageControl tChartControl = (SSCodeProductMessageControl)mainLayout.GetControlFromPosition(0, 0);

                    if (_LineWatchPaneControlID == bigsscode.Length)
                    {
                        SetSSCodeProductMessageControlValue(tChartControl, bigsscode[_LineWatchPaneControlID - 1].ToString());
                    }
                }

                this.mainLayout.Refresh();
            }
            catch
            { }
        }

        //画面的切换
        private void WatchTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                #region 显示车间概况时，切换界面

                if (_UserControlID == 1)
                {
                    if (_IsShowFacWatchPanel)
                    {
                        FacProductMessageControl FacProductMessageControl = new FacProductMessageControl();
                        SetFacProductMessageControlValue(FacProductMessageControl);
                        RefreshTableLayout();
                        this.mainLayout.Controls.Add(FacProductMessageControl, 0, 0);
                        FacProductMessageControl.Dock = DockStyle.Fill;
                        _UserControlID = 2;
                        return;
                    }

                    if (_IsShowFacQtyAndRate)
                    {
                        TChartControl tChart = new TChartControl();
                        SetFacTChartControlValue(tChart, true);
                        RefreshTableLayout();
                        this.mainLayout.Controls.Add(tChart, 0, 0);
                        tChart.Dock = DockStyle.Fill;
                        _UserControlID = 3;
                        return;
                    }

                    if (_IsShowLineSurvey)
                    {
                        string[] bigline = _BigLineList.Split(',');

                        if (_BigLineList.Length > 0)
                        {
                            SSCodeProductMessageControl tChart = new SSCodeProductMessageControl();
                            SetSSCodeProductMessageControlValue(tChart, bigline[_LineWatchPaneControlID].ToString());
                            RefreshTableLayout();
                            mainLayout.Controls.Add(tChart, 0, 0);
                            tChart.Dock = DockStyle.Fill;
                            _LineWatchPaneControlID += 1;
                            _UserControlID = 4;
                            return;
                        }
                    }
                }

                #endregion

                #region 显示车间产量及直通率走势图时，切换界面
                if (_UserControlID == 2)
                {

                    if (_IsShowFacQtyAndRate)
                    {
                        TChartControl tChart = new TChartControl();
                        SetFacTChartControlValue(tChart, true);
                        RefreshTableLayout();
                        this.mainLayout.Controls.Add(tChart, 0, 0);
                        tChart.Dock = DockStyle.Fill;
                        _UserControlID = 3;
                        return;
                    }

                    if (_IsShowLineSurvey)
                    {
                        string[] bigline = _BigLineList.Split(',');

                        if (_BigLineList.Length > 0)
                        {
                            SSCodeProductMessageControl tChart = new SSCodeProductMessageControl();
                            SetSSCodeProductMessageControlValue(tChart, bigline[_LineWatchPaneControlID].ToString());
                            RefreshTableLayout();
                            mainLayout.Controls.Add(tChart, 0, 0);
                            tChart.Dock = DockStyle.Fill;
                            _LineWatchPaneControlID += 1;
                            _UserControlID = 4;
                            return;
                        }
                    }

                    if (_IsShowFacSurvey)
                    {
                        FacMessageControl facMessageControl = new FacMessageControl();
                        this.GetFacSurevyFromXML();
                        facMessageControl.RTF = FacConfigMessage.CommonInfo;
                        RefreshTableLayout();
                        mainLayout.Controls.Add(facMessageControl, 0, 0);
                        facMessageControl.Dock = DockStyle.Fill;
                        _UserControlID = 1;
                        return;
                    }
                }

                #endregion

                #region 显示产线产量及直通率走势图时，切换界面
                if (_UserControlID == 3)
                {
                    if (_IsShowLineSurvey)
                    {
                        string[] bigline = _BigLineList.Split(',');

                        if (_BigLineList.Length > 0)
                        {
                            SSCodeProductMessageControl tChart = new SSCodeProductMessageControl();
                            SetSSCodeProductMessageControlValue(tChart, bigline[_LineWatchPaneControlID].ToString());
                            RefreshTableLayout();
                            mainLayout.Controls.Add(tChart, 0, 0);
                            tChart.Dock = DockStyle.Fill;
                            _LineWatchPaneControlID += 1;
                            _UserControlID = 4;
                            return;
                        }
                    }

                    if (_IsShowFacSurvey)
                    {
                        FacMessageControl facMessageControl = new FacMessageControl();
                        this.GetFacSurevyFromXML();
                        facMessageControl.RTF = FacConfigMessage.CommonInfo;
                        RefreshTableLayout();
                        mainLayout.Controls.Add(facMessageControl, 0, 0);
                        facMessageControl.Dock = DockStyle.Fill;
                        _UserControlID = 1;
                        return;
                    }

                    if (_IsShowFacWatchPanel)
                    {
                        FacProductMessageControl FacProductMessageControl = new FacProductMessageControl();
                        SetFacProductMessageControlValue(FacProductMessageControl);
                        RefreshTableLayout();
                        this.mainLayout.Controls.Add(FacProductMessageControl, 0, 0);
                        FacProductMessageControl.Dock = DockStyle.Fill;
                        _UserControlID = 2;
                        return;
                    }

                }
                #endregion

                #region 显示产线产量及直通率走势图时，切换界面
                if (_UserControlID == 4)
                {
                    //车间显示产线信息时的翻页动作
                    if (_LineWatchPaneNunmer > _LineWatchPaneControlID)
                    {
                        string[] bigline = _BigLineList.Split(',');
                        SSCodeProductMessageControl tChartControl = new SSCodeProductMessageControl();
                        SetSSCodeProductMessageControlValue(tChartControl, bigline[_LineWatchPaneControlID].ToString());  
                        RefreshTableLayout();
                        mainLayout.Controls.Add(tChartControl, 0, 0);
                        tChartControl.Dock = DockStyle.Fill;
                        _LineWatchPaneControlID += 1;
                        return;
                    }
                    //end

                    _LineWatchPaneControlID = 0;

                    if (_IsShowFacSurvey)
                    {
                        FacMessageControl facMessageControl = new FacMessageControl();
                        this.GetFacSurevyFromXML();
                        facMessageControl.RTF = FacConfigMessage.CommonInfo;
                        RefreshTableLayout();
                        mainLayout.Controls.Add(facMessageControl, 0, 0);
                        facMessageControl.Dock = DockStyle.Fill;
                        _UserControlID = 1;
                        return;
                    }

                    if (_IsShowFacWatchPanel)
                    {
                        FacProductMessageControl FacProductMessageControl = new FacProductMessageControl();
                        SetFacProductMessageControlValue(FacProductMessageControl);
                        RefreshTableLayout();
                        this.mainLayout.Controls.Add(FacProductMessageControl, 0, 0);
                        FacProductMessageControl.Dock = DockStyle.Fill;
                        _UserControlID = 2;
                        return;
                    }

                    if (_IsShowFacQtyAndRate)
                    {
                        TChartControl tChart = new TChartControl();
                        SetFacTChartControlValue(tChart, true);
                        RefreshTableLayout();
                        this.mainLayout.Controls.Add(tChart, 0, 0);
                        tChart.Dock = DockStyle.Fill;
                        _UserControlID = 3;
                        return;
                    }
                }

                #endregion
            }
            catch
            { }
        }

        private void mainLayout_Enter(object sender, EventArgs e)
        {
            System.Drawing.Bitmap img = new System.Drawing.Bitmap(2, 12);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
            g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.FromArgb(0, 255, 255)), 0, 0, 3, 12);
            this.mainLayout.Focus();
            DestroyCaret();
            CreateCaret(this.mainLayout.Handle.ToInt32(), img.GetHbitmap().ToInt32(), img.Width, img.Height);
            ShowCaret(this.mainLayout.Handle.ToInt32());
        }
        #endregion

        #region 自定义事件

        private void RefreshTableLayout()
        {
            if (mainLayout.Controls.Count >= 1) { mainLayout.Controls[0].Dispose(); }//Added By Nettie Chen on 2009/10/24
            mainLayout.Controls.Clear();
            GC.Collect();//Added By Nettie Chen on 2009/10/24

            this.mainLayout.RowCount = 1;
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.Refresh();

            
        }
        //初始化常量
        private void InitValues()
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("//TypeConfig[@ConfigName='" + _configTypeName + "']");

            if (xmlNode != null)
            {
                XmlNodeList xmlNodeList = xmlNode.ChildNodes;

                foreach (XmlNode xn in xmlNodeList)
                {
                    XmlElement xe = (XmlElement)xn;

                    if (xe.Name == "FacSurvey")
                    {
                        _IsShowFacSurvey = Convert.ToBoolean(xe.InnerText);
                    }

                    if (xe.Name == "FacMessage")
                    {
                        FacConfigMessage.CommonInfo = xe.InnerText;
                    }

                    if (xe.Name == "FacWatchPanel")
                    {
                        _IsShowFacWatchPanel = Convert.ToBoolean(xe.InnerText);
                    }

                    if (xe.Name == "FacQtyAndRate")
                    {
                        _IsShowFacQtyAndRate = Convert.ToBoolean(xe.InnerText);
                    }

                    if (xe.Name == "LineSurvey")
                    {
                        _IsShowLineSurvey = Convert.ToBoolean(xe.InnerText);
                    }

                    if (xe.Name == "AutoRefresh")
                    {
                        _AutoRefresh = Convert.ToDouble(xe.InnerText);
                    }

                    if (xe.Name == "WatchRefresh")
                    {
                        _WatchRefresh = Convert.ToDouble(xe.InnerText);
                    }

                    if (xe.Name == "BigLineList")
                    {
                        _BigLineList = xe.InnerText;
                    }

                    if (xe.Name == "BigLineNumber")
                    {
                        _SetShowBigLineNumber = int.Parse(xe.InnerText);
                    }

                    if (xe.Name == "TimeStat")
                    {
                        _GourpBy = xe.InnerText;
                    }
                    //Added By Nettie Chen 2009/09/23
                    if (xe.Name == "FinishedProduct")
                    {
                        _IsShowFinishedProduct = Convert.ToBoolean(xe.InnerText);
                    }
                    if (xe.Name == "SemimanuFacture")
                    {
                        _IsShowSemimanuProduct = Convert.ToBoolean(xe.InnerText);
                    }
                    //End Added
                }

                if (_IsShowLineSurvey == true)
                {
                    string[] biglineList = _BigLineList.Split(',');
                    _LineWatchPaneNunmer = biglineList.Length;
                }
            }

            //配置文件当前日期
            XmlNode xmlNode1 = xmlDoc.SelectSingleNode("//LineTypeConfig[@ConfigName='TimerConfig']");
            if (xmlNode1 != null)
            {
                XmlNodeList xmlNodeList = xmlNode1.ChildNodes;
                foreach (XmlNode xn in xmlNodeList)
                {
                    XmlElement xe = (XmlElement)xn;

                    if (xe.Name == "CuerrtDay")
                    {
                        if (xe.InnerText == null || xe.InnerText == string.Empty)
                        {
                            _CuerrtDay = FormatHelper.GetNowDBDateTime(this.DataProvider).DBDate;
                        }
                        else
                        {
                            _CuerrtDay = Convert.ToInt32(xe.InnerText);
                        }
                    }
                }
            }


            if (_CuerrtDay == 0)
            {
                _CuerrtDay = FormatHelper.GetNowDBDateTime(this.DataProvider).DBDate;
            }
            //end
        }

        //初始化WatchAndAutoTimer
        private void InitTimer()
        {
            this.WatchTimer.Interval = Convert.ToInt32(_WatchRefresh * 60000);
            this.WatchTimer.Enabled = true;

            this.AutoTimer.Interval = Convert.ToInt32(_AutoRefresh * 60000);
            this.AutoTimer.Enabled = true;
        }

        //初始化看板显示
        private void InitShowWatch()
        {
            _WatchPanelFacade = new WatchPanelFacade(this.DataProvider);

            if (_IsShowFacSurvey)
            {
                FacMessageControl user = new FacMessageControl();
                user.RTF = FacConfigMessage.CommonInfo;
                RefreshTableLayout();
                this.mainLayout.Controls.Add(user, 0, 0);
                user.Dock = DockStyle.Fill; 
                _UserControlID = 1;
                return;
            }

            if (_IsShowFacWatchPanel)
            {
                FacProductMessageControl FacProductMessageControl = new FacProductMessageControl();
                SetFacProductMessageControlValue(FacProductMessageControl);
                RefreshTableLayout();
                this.mainLayout.Controls.Add(FacProductMessageControl, 0, 0);
                FacProductMessageControl.Dock = DockStyle.Fill;
                _UserControlID = 2;
                return;
            }

            if (_IsShowFacQtyAndRate)
            {
                TChartControl tChart = new TChartControl();
                SetFacTChartControlValue(tChart, true);
                RefreshTableLayout();
                this.mainLayout.Controls.Add(tChart, 0, 0);
                tChart.Dock = DockStyle.Fill;
                _UserControlID = 3;
                return;
            }

            if (_IsShowLineSurvey)
            {
                string[] bigLineList = _BigLineList.Split(',');
                SSCodeProductMessageControl ssCodeProductMessageControl = new SSCodeProductMessageControl();

                if (bigLineList.Length > 0)
                {
                    SetSSCodeProductMessageControlValue(ssCodeProductMessageControl, bigLineList[0].ToString());
                }
                RefreshTableLayout();
                this.mainLayout.Controls.Add(ssCodeProductMessageControl, 0, 0);//Added By Nettie Chen 2009/09/23
                ssCodeProductMessageControl.Dock = DockStyle.Fill;
                _UserControlID = 4;
                return;
            }
        }

        //获取当前要显示的大线
        private void GetCuerrtBigLines()
        {
            //    string[] allBigLineList = _BigLineList.Split(',');

            //    int findNum = -1;
            //    for (int i = 0; i < allBigLineList.Length; i++)
            //    {
            //        if (_CuerrtShowBigLineList.Count == 0)
            //        {
            //            break;
            //        }

            //        if (allBigLineList[i] == _CuerrtShowBigLineList[_CuerrtShowBigLineList.Count - 1])
            //        {
            //            _CuerrtShowBigLineList.Clear();
            //            findNum = i;
            //            break;
            //        }
            //    }

            //    _CuerrtShowBigLineList.Clear();

            //    if (findNum >= 0 && findNum < allBigLineList.Length - 1)
            //    {
            //        for (int i = findNum + 1; i < findNum + _SetShowBigLineNumber + 1; i++)
            //        {
            //            if (i < allBigLineList.Length)
            //            {
            //                _CuerrtShowBigLineList.Add(allBigLineList[i]);
            //            }
            //        }
            //    }

            //    if (findNum == allBigLineList.Length - 1 || findNum == -1)
            //    {
            //        for (int i = 0; i < _SetShowBigLineNumber; i++)
            //        {
            //            if (i < allBigLineList.Length)
            //            {
            //                _CuerrtShowBigLineList.Add(allBigLineList[i]);
            //            }
            //        }
            //    }
        }

        #region 产线看板
        //设定产线看板
        private void SetSSCodeProductMessageControlValue(SSCodeProductMessageControl ssCodeProductMessageControl, string bigSSCode)
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);

            int PlanQty = watchPanelFacade.GetWorkPlanQty(bigSSCode, GetShiftDay(bigSSCode));
            int bigSSCodeOutPutQty = watchPanelFacade.GetBigSSCodeOutPutQty(bigSSCode, GetShiftDay(bigSSCode));
            int onPostManCount = watchPanelFacade.GetOnPostManCount(bigSSCode, GetShiftDay(bigSSCode));

            ssCodeProductMessageControl.BigLineCode = bigSSCode;
            ssCodeProductMessageControl.ExceptionMessageList = GetExceptionMessageList(bigSSCode);
            ssCodeProductMessageControl.HeaderLineMessage = bigSSCode;

            ssCodeProductMessageControl.CrewCodeList = GetCrewCodeList(bigSSCode);
            ssCodeProductMessageControl.PlanQty = PlanQty;
            ssCodeProductMessageControl.OnPostManCount = onPostManCount;
            ssCodeProductMessageControl.BigSSCodeOutPutQty = bigSSCodeOutPutQty;
            ssCodeProductMessageControl.ShiftCodeList = GetShiftCodeList(bigSSCode);

            ssCodeProductMessageControl.ProductGridDataSource = GetProductGridDataSource(bigSSCode);
            ssCodeProductMessageControl.RateLineTChartDataSource = GetRateLineTChartDataSource(bigSSCode);
            ssCodeProductMessageControl.BarJoinTChartDataSource = GetBarJoinTChartDataSource(bigSSCode);

            ssCodeProductMessageControl.SetControlsValue();

        }

        //当前日
        private int GetShiftDay(string bigSSCode)
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            string shiftTypeCode = string.Empty;
            TimePeriod timePeriod = (TimePeriod)watchPanelFacade.GettimePeriod(bigSSCode);
            if (timePeriod != null)
            {
                shiftTypeCode = timePeriod.ShiftTypeCode;
            }

            int shiftDay = watchPanelFacade.GetShiftDay(shiftTypeCode, _CuerrtDay, FormatHelper.GetNowDBDateTime(this.DataProvider).DBTime);
            
            return shiftDay;
        }

        //获取该大线的所有班组
        private string GetCrewCodeList(string bigSSCode)
        {
            string crewList = string.Empty;
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);

            object[] crew = watchPanelFacade.QueryCrewList(bigSSCode, GetShiftDay(bigSSCode));

            if (crew != null)
            {
                foreach (Line2Crew obj in crew)
                {
                    crewList += "," + obj.CrewCode;
                }
            }

            if (crewList.Length > 0)
            {
                crewList = crewList.Substring(1);
            }

            return crewList;
        }

        //异常信息
        private string GetExceptionMessageList(string bigSSCode)
        {
            string exceptionMessageList = string.Empty;
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);

            object[] exceptionList = watchPanelFacade.QueryExceptionList(bigSSCode, GetShiftDay(bigSSCode));

            if (exceptionList != null && exceptionList.Length > 0)
            {
                string cuerrtExecptionMessage = string.Empty;
                int showNumber = 1;
                foreach (ExceptionEventWithDescription obj in exceptionList)
                {
                    cuerrtExecptionMessage = obj.ItemCode + "-" + obj.Description + "-" + obj.Memo + "(" + FormatHelper.ToTimeString(obj.BeginTime, ":") + "-" + FormatHelper.ToTimeString(obj.EndTime, ":") + ")" + "\r\n";

                    exceptionMessageList += showNumber.ToString() + ": " + cuerrtExecptionMessage;
                    showNumber += 1;
                }
            }

            return exceptionMessageList;
        }

        //获取当前产线的所有班次
        private ArrayList GetShiftCodeList(string bigSSCode)
        {
            ArrayList shiftCodeList = new ArrayList();
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            object[] shiftCode = watchPanelFacade.QueryShiftCode(bigSSCode, string.Empty);

            if (shiftCode != null && shiftCode.Length > 0)
            {
                foreach (Shift obj in shiftCode)
                {
                    shiftCodeList.Add(obj);
                }
            }

            return shiftCodeList;
        }

        //产线看板Grid的数据源
        private object[] GetProductGridDataSource(string bigSSCode)
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            string tpCode = string.Empty;
            TimePeriod timePeriod = (TimePeriod)watchPanelFacade.GettimePeriod(bigSSCode);
            if (timePeriod != null)
            {
                tpCode = timePeriod.TimePeriodCode;
            }
            object[] productDataList = null;

            if (watchPanelFacade.CheckBigLineCodeIsHaveSSCode(bigSSCode))
            {
                productDataList = watchPanelFacade.QueryProductData(bigSSCode, GetShiftDay(bigSSCode), GetShiftCodeList(bigSSCode), tpCode);
            }

            return productDataList;
        }

        //产线看板直通率的数据源
        private object[] GetRateLineTChartDataSource(string bigSSCode)
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            object[] RateDataList = watchPanelFacade.QueryPassRateData(bigSSCode, GetShiftDay(bigSSCode));

            return RateDataList;
        }

        //产线看板产量的数据源
        private object[] GetBarJoinTChartDataSource(string bigSSCode)
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            object[] RateDataList = watchPanelFacade.QueryOutPutQtyData(bigSSCode, GetShiftDay(bigSSCode));

            return RateDataList;
        }

        #endregion

        #region 车间质量/产量趋势图设定
        //车间质量/产量趋势图设定
        private void SetFacTChartControlValue(TChartControl tChart, bool isFristLoad)
        {
            //Modified By Nettie Chen 2009/09/23
            //object[] finishedRateLineDataSource = _WatchPanelFacade.QueryRateByGourpConditin(_BigLineList, ItemType.ITEMTYPE_FINISHEDPRODUCT, _GourpBy, _CuerrtDay);
            //object[] semimanuRateLineDataSource = _WatchPanelFacade.QueryRateByGourpConditin(_BigLineList, ItemType.ITEMTYPE_SEMIMANUFACTURE, _GourpBy, _CuerrtDay);
            //object[] finishedProductDateLineDataSource = _WatchPanelFacade.QueryOutPutQtyByGourpConditin(_BigLineList, ItemType.ITEMTYPE_FINISHEDPRODUCT, _GourpBy, false, _CuerrtDay);
            //object[] semimanuProductDateLineDataSource = _WatchPanelFacade.QueryOutPutQtyByGourpConditin(_BigLineList, ItemType.ITEMTYPE_SEMIMANUFACTURE, _GourpBy, false, _CuerrtDay);
            //object[] finishedBarDataSource = _WatchPanelFacade.QueryOutPutQtyByGourpConditin(_BigLineList, ItemType.ITEMTYPE_FINISHEDPRODUCT, _GourpBy, true, _CuerrtDay);
            //object[] semimanuBarDataSource = _WatchPanelFacade.QueryOutPutQtyByGourpConditin(_BigLineList, ItemType.ITEMTYPE_SEMIMANUFACTURE, _GourpBy, true, _CuerrtDay);
            //object[] TPCodeList = _WatchPanelFacade.QueryOutPutQtyByGourpConditin(_BigLineList, string.Empty, _GourpBy, false, _CuerrtDay);
            //tChart.SetDataChartValue(finishedRateLineDataSource, semimanuRateLineDataSource,
            //                         finishedProductDateLineDataSource, semimanuProductDateLineDataSource,
            //                         finishedBarDataSource, semimanuBarDataSource, isFristLoad, TPCodeList);
            object[] finishedRateLineDataSource = null;
            object[] finishedProductDateLineDataSource = null;
            object[] finishedBarDataSource = null;
            object[] semimanuRateLineDataSource = null;
            object[] semimanuProductDateLineDataSource = null;
            object[] semimanuBarDataSource = null;

            if (_IsShowFinishedProduct == true)
            {
                finishedRateLineDataSource = _WatchPanelFacade.QueryRateByGourpConditin(_BigLineList, ItemType.ITEMTYPE_FINISHEDPRODUCT, _GourpBy, _CuerrtDay);
                finishedProductDateLineDataSource = _WatchPanelFacade.QueryOutPutQtyByGourpConditin(_BigLineList, ItemType.ITEMTYPE_FINISHEDPRODUCT, _GourpBy, false, _CuerrtDay);
                finishedBarDataSource = _WatchPanelFacade.QueryOutPutQtyByGourpConditin(_BigLineList, ItemType.ITEMTYPE_FINISHEDPRODUCT, _GourpBy, true, _CuerrtDay);
            }
            if (_IsShowSemimanuProduct == true)
            {
                semimanuRateLineDataSource = _WatchPanelFacade.QueryRateByGourpConditin(_BigLineList, ItemType.ITEMTYPE_SEMIMANUFACTURE, _GourpBy, _CuerrtDay);
                semimanuProductDateLineDataSource = _WatchPanelFacade.QueryOutPutQtyByGourpConditin(_BigLineList, ItemType.ITEMTYPE_SEMIMANUFACTURE, _GourpBy, false, _CuerrtDay);
                semimanuBarDataSource = _WatchPanelFacade.QueryOutPutQtyByGourpConditin(_BigLineList, ItemType.ITEMTYPE_SEMIMANUFACTURE, _GourpBy, true, _CuerrtDay);
            }
            object[] TPCodeList = _WatchPanelFacade.QueryOutPutQtyByGourpConditin(_BigLineList, string.Empty, _GourpBy, false, _CuerrtDay);
            
            tChart.SetDataChartValue(finishedRateLineDataSource, semimanuRateLineDataSource,
                                     finishedProductDateLineDataSource, semimanuProductDateLineDataSource,
                                     finishedBarDataSource, semimanuBarDataSource, isFristLoad, TPCodeList, _IsShowFinishedProduct, _IsShowSemimanuProduct);
            //End Modified
        }

        #endregion

        #region 车间概况电子看板

        //获取车间概况
        private void GetFacSurevyFromXML()
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("//TypeConfig[@ConfigName='" + _configTypeName + "']");

            if (xmlNode != null)
            {
                XmlNodeList xmlNodeList = xmlNode.ChildNodes;

                foreach (XmlNode xn in xmlNodeList)
                {
                    XmlElement xe = (XmlElement)xn;
                    if (xe.Name == "FacMessage")
                    {
                        FacConfigMessage.CommonInfo = xe.InnerText;
                    }
                }
            }
        }
        //车间概况电子看板设定
        private void SetFacProductMessageControlValue(FacProductMessageControl facProductMessageControl)
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);

            object[] outPut = watchPanelFacade.QueryOutPutQtyGroupByItemType(_BigLineList, _CuerrtDay, false);
            int finshItemQty = 0;
            int semimanuFactureQty = 0;

            if (outPut != null)
            {
                foreach (watchPanelProductDate obj in outPut)
                {
                    if (obj.Mtype == ItemType.ITEMTYPE_FINISHEDPRODUCT)
                    {
                        finshItemQty = obj.MonthProductQty;
                    }

                    if (obj.Mtype == ItemType.ITEMTYPE_SEMIMANUFACTURE)
                    {
                        semimanuFactureQty = obj.MonthProductQty;
                    }
                }
            }

            object[] gridDataSource = _WatchPanelFacade.QueryProudctDataByDateAndSSCodeList(_CuerrtDay, _BigLineList);
            object[] barJoinDataSource = _WatchPanelFacade.QueryOQCLotPassRate(_CuerrtDay, _BigLineList);
            object[] peiDataSource = _WatchPanelFacade.QueryErrorCasueTopFive(_CuerrtDay, _BigLineList);

            facProductMessageControl.FinshItemQty = finshItemQty;
            facProductMessageControl.SemimanuFactureQty = semimanuFactureQty;
            facProductMessageControl.BigLineListInProduct = GetBigLineListInProduct(true);
            facProductMessageControl.BigLineListOutProduct = GetBigLineListInProduct(false);
            facProductMessageControl.GridDataSource = gridDataSource;
            facProductMessageControl.BarJoinDataSource = barJoinDataSource;
            facProductMessageControl.PeiDataSource = peiDataSource;
            //Added By Nettie Chen 2009/09/23
            facProductMessageControl.IsShowFinishedProduct = _IsShowFinishedProduct;
            facProductMessageControl.IsShowSemimanuProduct = _IsShowSemimanuProduct;
            //End Added
            facProductMessageControl.InitControlsValue();
        }

        //根据是否生产获取大线
        private string GetBigLineListInProduct(bool isInProduct)
        {
            WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
            string returnString = string.Empty;
            string[] bigLine = _BigLineList.Trim().ToUpper().Split(',');

            DBDateTime now = FormatHelper.GetNowDBDateTime(this.DataProvider);

            for (int i = 0; i < bigLine.Length; i++)
            {
                if (isInProduct && watchPanelFacade.CheckLineIsProduct(bigLine[i].ToString(), now.DBDate, now.DBTime))
                {
                    returnString += "," + bigLine[i].Trim().ToUpper().ToString();
                }

                if (!isInProduct && !watchPanelFacade.CheckLineIsProduct(bigLine[i].ToString(), now.DBDate, now.DBTime))
                {
                    returnString += "," + bigLine[i].Trim().ToUpper().ToString();
                }
            }

            if (returnString.Length > 0)
            {
                returnString = returnString.Substring(1);
            }

            return returnString;
        }

        #endregion
        #endregion


    }
}