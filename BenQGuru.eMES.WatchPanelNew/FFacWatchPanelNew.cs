using System;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.Runtime.InteropServices;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WatchPanelNew
{
    public partial class FFacWatchPanelNew : Form
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
        private string _panelType = string.Empty;  //用来区分是产线看板还是车间看板

        private double _autoRefresh = 0;
        private double _screenRefresh = 0;
        private double _maxLineCount = 0;
        private double _pageScrolling = 0;

        private string _workShopCode = string.Empty;  //车间代码
        private string _ssCode = string.Empty;   //产线代码
        private object[] _ssCodeListInWorkShop = null;
        private int _loadedCount = 0;  //记录加载了多少条数据
        private string _ssCodeInWrokShopCode = string.Empty;

        private bool _dividedHoursByDay = false;
        private bool _dividedHoursByWeek = false;
        private bool _dividedHoursByMonth = false;
        private bool _comprehensivePanel = false;
        private bool _panelDetails = false;
        private bool _standbyContent = false;
        private  bool isResetCount = false;

        private int _CuerrtDay = 0;
        private int _ShowScreensNumber = 1;

        //显示画面的序号
        private int _UserControlID = 0;

        private IDomainDataProvider _dataProvider=ApplicationService.Current().DataProvider;
        private WatchPanelFacade _WatchPanelFacade;
        private IDomainDataProvider DataProvider
        {
            get
            {
                if (_dataProvider == null)
                {
                    _dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(Program.DBName);
                }
                return _dataProvider;
            }

            set
            {
                _dataProvider = value;
            }
        }
        private WatchPanelFacade m_WatchPanelFacade
        {
            get
            {
                if (_WatchPanelFacade == null)
                {
                    _WatchPanelFacade = new WatchPanelFacade(this.DataProvider);
                }
                return _WatchPanelFacade;
            }
        }
        #endregion

        #region 页面事件

        public FFacWatchPanelNew()
        {
            InitializeComponent();
        }

        public FFacWatchPanelNew(string configTypeName, string panelType)
        {
            InitializeComponent();
            _configTypeName = configTypeName;
            _panelType = panelType;
            InitValues();
            InitTimer();
            InitShowWatch();
        }

        public FFacWatchPanelNew(string workShopCode, string ssCode, string panelType, string configTypeName)
        {
            InitializeComponent();
            _configTypeName = configTypeName;
            _panelType = panelType;
            _workShopCode = workShopCode;
            _ssCode = ssCode;
            InitValues();
            InitTimer();
            InitShowWatch();
        }

        private void FFacWatchPanelNew_Load(object sender, EventArgs e)
        {

        }

        private void FFacWatchPanelNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.AutoTimer.Enabled = false;
                this.WatchTimer.Enabled = false;
                this.WorkShopTimer.Enabled = false;
                this.Close();
            }
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

        //数据的Load
        private void AutoTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                //显示待机内容
                if (_standbyContent && _UserControlID == 1)
                {
                    this.GetFacSurevyFromXML();
                    FacMessageControl control = (FacMessageControl)this.mainLayout.GetControlFromPosition(0, 0);
                    control.RTF = FacConfigMessage.CommonInfo;
                    control.ValueRefresh();
                }

                //显示综合看板
                if (_comprehensivePanel && _UserControlID == 2)
                {
                    FacProductMessageControlNew FacProductMessageControl = (FacProductMessageControlNew)this.mainLayout.GetControlFromPosition(0, 0);
                    SetFacProductMessageControlValue(FacProductMessageControl);
                    FacProductMessageControl.Dock = DockStyle.Fill;
                }

                //显示看板明细
                if (_panelDetails && _UserControlID == 3)
                {
                    WatchPanelDetails watchPanelDetail = (WatchPanelDetails)this.mainLayout.GetControlFromPosition(0, 0);
                    SetPanelDetailsControlValue(watchPanelDetail);
                    watchPanelDetail.Dock = DockStyle.Fill;
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
                #region 综合看板和看板明细之间的切换
                if (_UserControlID == 1)
                {
                    if (_standbyContent)
                    {
                        if (!this.mainLayout.GetControlFromPosition(0, 0).Name.Equals("FacMessageControl"))
                        {
                            this.GetFacSurevyFromXML();
                            this.mainLayout.Controls.Clear();
                            FacMessageControl control = new FacMessageControl();
                            control.RTF = FacConfigMessage.CommonInfo;
                            this.mainLayout.Controls.Add(control, 0, 0);
                            control.Dock = DockStyle.Fill;
                            control.ValueRefresh();
                            _UserControlID = 3;
                            return;
                        }
                    }
                        _UserControlID = 3;
                }

                if (_UserControlID == 3)
                {
                    if (_comprehensivePanel)
                    {
                        //if (!this.mainLayout.GetControlFromPosition(0, 0).Name.Equals("FacProductMessageControlNew"))
                        //{
                            FacProductMessageControlNew FacProductMessageControl = new FacProductMessageControlNew();
                            SetFacProductMessageControlValue(FacProductMessageControl);
                            RefreshTableLayout();
                            this.mainLayout.Controls.Add(FacProductMessageControl, 0, 0);
                            FacProductMessageControl.Dock = DockStyle.Fill;
                            _UserControlID = 2;
                            return;
                        //}
                    }
                    _UserControlID = 2;
                    if (string.IsNullOrEmpty(_ssCode)&& _ssCodeInWrokShopCode.Equals((_ssCodeListInWorkShop[_ssCodeListInWorkShop.Length-1] as StepSequence).StepSequenceCode))
                    {
                        AutoTimer_Tick(sender, e);
                    }
                }

                #endregion

                if (_UserControlID == 2)
                {
                    if (_panelDetails)
                    {
                        //if (!this.mainLayout.GetControlFromPosition(0, 0).Name.Equals("WatchPanelDetails"))
                        //{
                            WatchPanelDetails watchPanelDetail = new WatchPanelDetails();
                            SetPanelDetailsControlValue(watchPanelDetail);
                            RefreshTableLayout();
                            this.mainLayout.Controls.Add(watchPanelDetail, 0, 0);
                            watchPanelDetail.Dock = DockStyle.Fill;
                            _UserControlID = 1;
                            return;
                      //  }
                    }
                    _UserControlID = 1;
                }

                if (_UserControlID == 1 && this.mainLayout.GetControlFromPosition(0, 0).Name.Equals("FacProductMessageControlNew") && _standbyContent)
                {
                    this.GetFacSurevyFromXML();
                    this.mainLayout.Controls.Clear();
                    FacMessageControl control = new FacMessageControl();
                    control.RTF = FacConfigMessage.CommonInfo;
                    this.mainLayout.Controls.Add(control, 0, 0);
                    control.Dock = DockStyle.Fill;
                    control.ValueRefresh();
                    _UserControlID = 3;
                    return;
                }
            }
            catch
            { }
        }

        #endregion

        #region 自定义事件

        private void RefreshTableLayout()
        {
            if (mainLayout.Controls.Count >= 1) { mainLayout.Controls[0].Dispose(); }
            mainLayout.Controls.Clear();
            GC.Collect();

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
                    if (xe.Name == "FacMessage")
                    {
                        FacConfigMessage.CommonInfo = xe.InnerText.Trim();
                    }
                    if (xe.Name == "StandbyContent")
                    {
                        _standbyContent = Convert.ToBoolean(xe.InnerText.Trim());
                    }
                    if (xe.Name.Equals("ComprehensivePanel"))
                    {
                        _comprehensivePanel = Convert.ToBoolean(xe.InnerText.Trim());
                    }
                    if (xe.Name.Equals("PanelDetails"))
                    {
                        _panelDetails = Convert.ToBoolean(xe.InnerText.Trim());
                    }
                    if (xe.Name.Equals("AutoRefresh"))
                    {
                        if (!string.IsNullOrEmpty(xe.InnerText))
                        {
                            _autoRefresh = Convert.ToDouble(xe.InnerText.Trim());
                        }
                    }
                    if (xe.Name.Equals("ScreenRefresh"))
                    {
                        if (!string.IsNullOrEmpty(xe.InnerText))
                        {
                            _screenRefresh = Convert.ToDouble(xe.InnerText.Trim());
                        }
                    }
                    if (xe.Name.Equals("MaxLineCount"))
                    {
                        if (!string.IsNullOrEmpty(xe.InnerText))
                        {
                            _maxLineCount = Convert.ToDouble(xe.InnerText.Trim());
                        }
                    }
                    if (xe.Name.Equals("PageScrolling"))
                    {
                        if (!string.IsNullOrEmpty(xe.InnerText))
                        {
                            _pageScrolling = Convert.ToDouble(xe.InnerText.Trim());
                        }
                    }
                    if (xe.Name.Equals("DividedHoursByDay"))
                    {
                        _dividedHoursByDay = Convert.ToBoolean(xe.InnerText.Trim());
                    }
                    if (xe.Name.Equals("DividedHoursByMonth"))
                    {
                        _dividedHoursByMonth = Convert.ToBoolean(xe.InnerText.Trim());
                    }
                    if (xe.Name.Equals("DividedHoursByWeek"))
                    {
                        _dividedHoursByWeek = Convert.ToBoolean(xe.InnerText.Trim());
                    }
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
                            _CuerrtDay = WatchPanelFacade.GetNowDBDateTime(this.DataProvider).DBDate;
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
                _CuerrtDay = WatchPanelFacade.GetNowDBDateTime(this.DataProvider).DBDate;
            }

            //加载车间下的所有产线，只在车间看板使用
            if (string.IsNullOrEmpty(_ssCode))
            {
                _ssCodeListInWorkShop = m_WatchPanelFacade.GetStepSequenceBySeg(_workShopCode);
            }
        }

        //初始化WatchAndAutoTimer
        private void InitTimer()
        {
            this.WatchTimer.Interval = Convert.ToInt32(_screenRefresh * 1000);
            this.WatchTimer.Enabled = true;

          //如果是车间看板才使用该Timer
            if (string.IsNullOrEmpty(_ssCode) && _ssCodeListInWorkShop != null)
            {
                InitWorkShopTimer();
            }

            this.AutoTimer.Interval = Convert.ToInt32(_autoRefresh * 1000);
            this.AutoTimer.Enabled = true;

            this.ShiftDayTimer.Interval = Convert.ToInt32(60 * 1000);
            this.ShiftDayTimer.Enabled = true;
        }

        private void InitWorkShopTimer()
        {
                this.WorkShopTimer.Enabled = true;
                double interval = (double)(this.WatchTimer.Interval / _ssCodeListInWorkShop.Length);
                this.WorkShopTimer.Interval = (int)Math.Floor(interval);
        }

        //初始化看板显示
        private void InitShowWatch()
        {
            //双屏显示时，选择第二屏显示 
            if (_ShowScreensNumber == 2)
            {
                //判断是否有第二屏
                Screen[] allScreens = Screen.AllScreens;
                Screen currentScreen = Screen.FromRectangle(this.DisplayRectangle);
                bool isSecondScreen = false;
                if (allScreens.Length == 2)
                {
                    isSecondScreen = true;
                }

                if (isSecondScreen)
                {
                    this.DesktopLocation = Screen.AllScreens[1].Bounds.Location;
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.DesktopBounds = Screen.AllScreens[1].Bounds;
                }
            }

            //是否要显示待机信息
            if (_standbyContent)
            {
                FacMessageControl user = new FacMessageControl();
                user.RTF = FacConfigMessage.CommonInfo;
                RefreshTableLayout();
                this.mainLayout.Controls.Add(user, 0, 0);
                user.Dock = DockStyle.Fill;
                user.ValueRefresh();
                _UserControlID = 1;
                return;
            }

            //显示综合看板
            if (_comprehensivePanel)
            {
                FacProductMessageControlNew FacProductMessageControl = new FacProductMessageControlNew();
                SetFacProductMessageControlValue(FacProductMessageControl);
                RefreshTableLayout();
                this.mainLayout.Controls.Add(FacProductMessageControl, 0, 0);
                FacProductMessageControl.Dock = DockStyle.Fill;
                _UserControlID = 2;
                return;
            }

            //显示明细看板
            if (_panelDetails)
            {
                WatchPanelDetails watchPanelDetail = new WatchPanelDetails();
                SetPanelDetailsControlValue(watchPanelDetail);
                RefreshTableLayout();
                this.mainLayout.Controls.Add(watchPanelDetail, 0, 0);
                watchPanelDetail.Dock = DockStyle.Fill;
                _UserControlID = 3;
                return;
            }
        }

        #region 产线看板
        //当前日
        private int GetShiftDay(string ssCode)
        {
            string shiftTypeCode = string.Empty;
            TimePeriod timePeriod = (TimePeriod)m_WatchPanelFacade.GettimePeriod(ssCode);
            if (timePeriod != null)
            {
                shiftTypeCode = timePeriod.ShiftTypeCode;
            }

            int shiftDay = m_WatchPanelFacade.GetShiftDay(shiftTypeCode, _CuerrtDay, WatchPanelFacade.GetNowDBDateTime(this.DataProvider).DBTime);
            return shiftDay;
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

        //综合看板和看板明细
        private void SetFacProductMessageControlValue(FacProductMessageControlNew facProductMessageControlNew)
        {
            try
            {
                #region  画面切换设置
                //如果是车间看板 
                if (string.IsNullOrEmpty(_ssCode))
                {
                    if (!this.WorkShopTimer.Enabled && !this.AutoTimer.Enabled)
                    {
                        this.WorkShopTimer.Enabled = true;
                        this.AutoTimer.Enabled = true;
                        this._loadedCount = 0;
                        _ssCodeInWrokShopCode = string.Empty;
                    }
                }
                else
                {
                    //产线看板 
                    this.AutoTimer.Enabled = true;
                }

                #endregion 

                #region 空间维度: 车间/产线  时间维度：日，周，月来统计数据，
                //时间维度为日时，x轴用各个时段；为周时，x轴用该周下的每一天来表示；为月时，x周用该周下的每一周来显示
                //当空间维度是车间时，直通率和投入产出图要分别刷显示车间下的每条产线
                string ssCode = string.Empty;
                DBTimeDimension timeObj = (DBTimeDimension)m_WatchPanelFacade.GetDBTimeDimension(_CuerrtDay);
                if (timeObj == null)
                {
                    MessageBox.Show("数据库中缺少当前日期的时间维度,请检查Tbltimedimension表");
                    return;
                }

                if (string.IsNullOrEmpty(_ssCode) && _ssCodeListInWorkShop != null)
                {
                    facProductMessageControlNew.IsWorkShopPanel = true;
                    if (string.IsNullOrEmpty(_ssCodeInWrokShopCode))
                    {
                        _ssCodeInWrokShopCode = (_ssCodeListInWorkShop[0] as StepSequence).StepSequenceCode;
                    }
                    ssCode = _ssCodeInWrokShopCode;
                }
                else
                {
                    facProductMessageControlNew.IsWorkShopPanel = false;
                    ssCode = _ssCode;
                }

                #region  不良原因
                object[] peiDataSource = null;
                if (_dividedHoursByDay)
                {
                     peiDataSource = m_WatchPanelFacade.QueryErrorCasueTopFiveBySSCode(_CuerrtDay, _ssCode, _workShopCode);
                }
                if (_dividedHoursByWeek)
                {
                    peiDataSource = m_WatchPanelFacade.QueryErrorCasueTopFiveByWeek(_ssCode, _workShopCode, timeObj);
                }
                if (_dividedHoursByMonth)
                {
                    peiDataSource = m_WatchPanelFacade.QueryErrorCasueTopFiveByMonth(_ssCode, _workShopCode, timeObj);
                }
                #endregion

                #region 投入产出柱状图
                int seq = m_WatchPanelFacade.GetSEQForLineChar(ssCode);
                object[] todayQtyDataSource = null;
                if (_dividedHoursByDay)
                {
                    todayQtyDataSource = m_WatchPanelFacade.QueryLineCharDataSource(ssCode, _CuerrtDay, seq);
                }
                else
                {
                    if (timeObj == null)
                    {
                        MessageBox.Show("数据库中缺少当前日期的时间维度,请检查Tbltimedimension表");
                        return;
                    }
                    if (_dividedHoursByMonth)
                    {
                        //得到当前CurrentDay对应的月数传入
                        todayQtyDataSource = m_WatchPanelFacade.QueryInputOutputByShiftDay(ssCode, _workShopCode, _CuerrtDay, TimeDimension.Month, timeObj);
                    }
                    if (_dividedHoursByWeek)
                    {
                        //得到当前CurrentDay对应的周数传入
                        todayQtyDataSource = m_WatchPanelFacade.QueryInputOutputByShiftDay(ssCode, _workShopCode, _CuerrtDay, TimeDimension.Week, timeObj);
                    }
                }
                #endregion 

                #region  直通率,按照不同的时间维度统计
                object[] weekQtyDataSource=null;
                if (_dividedHoursByDay)
                {
                    weekQtyDataSource = m_WatchPanelFacade.QueryFPYBySSCodeForPeriod(ssCode, _CuerrtDay, seq);
                }
                else
                {
                    if (_dividedHoursByMonth)
                    {
                        //得到当前CurrentDay对应的月数传入
                        weekQtyDataSource = m_WatchPanelFacade.QueryFPYBySSCodeForMonth(ssCode, _CuerrtDay, _workShopCode, timeObj);
                    }

                    if (_dividedHoursByWeek)
                    {
                        //得到当前CurrentDay对应的周数传入
                        weekQtyDataSource = m_WatchPanelFacade.QueryFPYBySSCodeForWeek(ssCode,  _CuerrtDay,_workShopCode, timeObj);
                    }
                }
                #endregion 

                #region  总投入和总产出
                int inputOutPutShiftday = 0;
                string divideTyep = string.Empty;
                if (_dividedHoursByDay)
                {
                    inputOutPutShiftday = _CuerrtDay;
                    divideTyep = TimeDimension.Day;
                }
                else 
                {
                    inputOutPutShiftday = -1;
                    if (_dividedHoursByMonth)
                    {
                        divideTyep = TimeDimension.Month;
                    }
                    else
                    {
                        divideTyep = TimeDimension.Week;
                    }
                }
                object inputOutputByShifDay = m_WatchPanelFacade.GetInputOutputQtyByShiftDay(inputOutPutShiftday, _ssCode.Trim(), _workShopCode.Trim(),timeObj,divideTyep);
                #endregion  

                #region 上岗人数 
                 int opValue =m_WatchPanelFacade.GetOnPostManCount(_CuerrtDay, _ssCode, _workShopCode,timeObj,divideTyep);     //上岗人数
                #endregion 

                #endregion

                 #region 传参数到综合看板
                 facProductMessageControlNew.PeiDataSource = peiDataSource;
                facProductMessageControlNew.InputQutputQty = inputOutputByShifDay;
                facProductMessageControlNew.TodayQtyDataSource = todayQtyDataSource;
                facProductMessageControlNew.WeekQtyDataSource = weekQtyDataSource;
                facProductMessageControlNew.OpValue = opValue.ToString();
                facProductMessageControlNew.Sscode = ssCode;

                facProductMessageControlNew.WorkShopCode = _workShopCode;
                if (_dividedHoursByDay)
                {
                    facProductMessageControlNew.Dimension = TimeDimension.Day;
                }
                if (_dividedHoursByWeek)
                {
                    facProductMessageControlNew.Dimension = TimeDimension.Week;
                }
                if (_dividedHoursByMonth)
                {
                    facProductMessageControlNew.Dimension = TimeDimension.Month;
                }
                facProductMessageControlNew.InitControlsValue();
                #endregion 
            }
            catch
            {
            }
        }

        private string GetParameter(string parameterCode)
        {
            Parameter obj = m_WatchPanelFacade.GetParameter(parameterCode, "WATCHPANNEL") as Parameter;

            if (obj != null)
            {
                return obj.ParameterAlias;
            }
            return string.Empty;
        }

        #endregion

        //看板明细设置
        private void SetPanelDetailsControlValue(WatchPanelDetails panelDetail)
        {
            panelDetail._sscode = _ssCode;
            panelDetail._workShopCode = _workShopCode;
            panelDetail._maxLineCount = _maxLineCount;
            panelDetail._pageScrolling = _pageScrolling;
            DBTimeDimension timeObj = (DBTimeDimension)m_WatchPanelFacade.GetDBTimeDimension(_CuerrtDay);
            if (timeObj == null)
            {
                MessageBox.Show("数据库中缺少当前日期的时间维度,请检查Tbltimedimension表");
                return;
            }
            string divideTyep = string.Empty;
            if (_dividedHoursByDay)
            {
                divideTyep = TimeDimension.Day;
            }
            if (_dividedHoursByMonth)
            {
                divideTyep = TimeDimension.Month;
            }
            if (_dividedHoursByWeek)
            {
                divideTyep = TimeDimension.Week;
            }
            object[] panelDetailDataSource = this.m_WatchPanelFacade.QueryPanelDetailsData(_ssCode, _CuerrtDay, _workShopCode,timeObj,divideTyep);
            panelDetail.panlDetailsDataSource = panelDetailDataSource;

            this.WorkShopTimer.Enabled = false;
            this.AutoTimer.Enabled = false;

            panelDetail.InitControlsValue();

        }

        #region 同步看板取到XML中日期时间和实际的时间,间隔60S
        private void ShiftDayTimer_Tick(object sender, EventArgs e)
        {
            int newShiftDay = 0;
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

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
                            newShiftDay = WatchPanelFacade.GetNowDBDateTime(this.DataProvider).DBDate;
                        }
                        else
                        {
                            newShiftDay = Convert.ToInt32(xe.InnerText);
                        }
                    }
                }
            }

            if (newShiftDay == 0)
            {
                newShiftDay = WatchPanelFacade.GetNowDBDateTime(this.DataProvider).DBDate;
            }
            //将新值同步到全局变量中
            _CuerrtDay = newShiftDay;
        }
        #endregion


        //车间看板： 将车间下的所有产线在屏幕切换时间内 轮流显示，
        //直通率和投入/产出

        private void WorkShopTimer_Tick(object sender, EventArgs e)
        {
            if (_ssCodeListInWorkShop != null)
            {
                int loopCount = 0;   //记录当前循环是第几次循环
                //重头重新循环一次
                if (_loadedCount >= _ssCodeListInWorkShop.Length - 1)
                {
                    if (!isResetCount)
                    {
                        isResetCount = true;
                        _loadedCount = 0;
                    }
                }
                if (_loadedCount >= _ssCodeListInWorkShop.Length && isResetCount)
                {
                    _loadedCount = 0;
                }
                foreach (StepSequence obj in _ssCodeListInWorkShop)
                {
                    loopCount++;
                    if (loopCount <= _loadedCount)
                    {
                        continue;
                    }
                    if (_ssCodeInWrokShopCode.Equals(obj.StepSequenceCode))
                    {
                        continue;
                    }
                    _ssCodeInWrokShopCode = obj.StepSequenceCode;
                    _loadedCount++;
                    break;
                }
            }

            AutoTimer_Tick(sender, e);
            if (_ssCodeInWrokShopCode.Equals((_ssCodeListInWorkShop[_ssCodeListInWorkShop.Length - 1] as StepSequence).StepSequenceCode) && string.IsNullOrEmpty(_ssCode) && !_panelDetails)
            {
                WatchTimer_Tick(sender, e);
            }
        }
        #endregion

    }
}
