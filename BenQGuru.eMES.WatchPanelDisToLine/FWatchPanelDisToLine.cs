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
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WatchPanelDisToLine
{
    public partial class FWatchPanelDisToLine : Form
    {
        //配置文件名称以及各个配置值
        private double _AutoRefresh = 1;
        private int _DisToLineRowNum = 15;//每页显示行数
        private double _DisToLinePageRefresh = 4000;//翻页时间
        private double _MessageRefresh = 4000;//预警自动刷新时间
        private IDomainDataProvider _dataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
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
        #region 页面事件
        public FWatchPanelDisToLine()
        {
            InitializeComponent();
            InitValues();
            InitShowWatch();
            InitTimer();
        }

        //初始化看板显示
        private void InitShowWatch()
        {
            DisToLineGridShow disToLineGridShow = new DisToLineGridShow(_DisToLinePageRefresh, _DisToLineRowNum, _MessageRefresh);
            SetOnlineResInfoControlValue(disToLineGridShow);
            RefreshTableLayout();
            this.MainLayout.Controls.Add(disToLineGridShow, 0, 0);
            disToLineGridShow.Dock = DockStyle.Fill;
            return;
        }

        //初始化WatchAndtimerChange
        private void InitTimer()
        {
            this.Autotimer.Interval = Convert.ToInt32(_AutoRefresh * 60000);
            this.Autotimer.Enabled = true;
        }

        private void FWatchPanelMaterial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                this.Autotimer.Enabled = false;
                this.Close();
            }
        }

        private void Autotimer_Tick(object sender, EventArgs e)
        {
            try
            {
                DisToLineGridShow disToLineGridShow = new DisToLineGridShow(_DisToLinePageRefresh, _DisToLineRowNum, _MessageRefresh);
                SetOnlineResInfoControlValue(disToLineGridShow);
                this.MainLayout.Refresh();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //初始化常量
        private void InitValues()
        {
            //string xmlPath = "../BenQGuru.eMES.WatchPanelDisToLine/WatchDisToLineConfig.xml";
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchDisToLineConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("//DisToLineWatchConfig[@ConfigName='TimerCofig']");

            //物料送检明细翻页频率
            if (xmlNode != null)
            {
                XmlNodeList xmlNodeList = xmlNode.ChildNodes;
                //获取物料送检明细翻页时间及每页显示数量
                foreach (XmlNode xn in xmlNodeList)
                {
                    if (xn.NodeType == XmlNodeType.Comment)
                        continue;
                    XmlElement xe = (XmlElement)xn;
                    if (xe.Name == "PanelRefresh")//自动刷新
                    {
                        _AutoRefresh = Convert.ToDouble(xe.InnerText);
                    }
                    if (xe.Name == "PageChangeTimer")//翻页时间
                    {
                        _DisToLinePageRefresh = Convert.ToDouble(xe.InnerText);
                    }
                    if (xe.Name == "PagerRowCount")//每页行数
                    {
                        _DisToLineRowNum = Convert.ToInt32(xe.InnerText);
                    }
                    if (xe.Name == "MessageRefresh")//预警自动刷新时间
                    {
                        _MessageRefresh = Convert.ToDouble(xe.InnerText);
                    }
                }
            }
        }

        //物料明细资源信息
        private void SetOnlineResInfoControlValue(DisToLineGridShow disToLineGridShow)
        {
            disToLineGridShow.InitControlsValue();
        }

        private void RefreshTableLayout()
        {
            if (MainLayout.Controls.Count >= 1) { MainLayout.Controls[0].Dispose(); }
            MainLayout.Controls.Clear();
            GC.Collect();

            this.MainLayout.RowCount = 1;
            this.MainLayout.ColumnCount = 1;
            this.MainLayout.Refresh();
        }

        #endregion


    }
}