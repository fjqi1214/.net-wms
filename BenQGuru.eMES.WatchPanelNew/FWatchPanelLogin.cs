using System;
using System.Windows.Forms;
using BenQGuru.eMES.Common.Domain;
using System.Xml;

namespace BenQGuru.eMES.WatchPanelNew
{
    public partial class FWatchPanelLogin : Form
    {
        WatchPanelFacade _watchFacade = null;

        private IDomainDataProvider _dataProvider=ApplicationService.Current().DataProvider;
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

        public FWatchPanelLogin()
        {
            InitializeComponent();
        }

        //页面初始化
        private void FWatchPanelLogin_Load(object sender, EventArgs e)
        {
            ucWorkshop_Load(null, null);
            ucWorkShopBySSCode_Load(null, null);
            ucProductLine_Load(null, null);

            this.ucWorkShopBySSCode.Enabled = false;
            this.ucProductLine.Enabled = false;
        }

        #region RadioButton的控制
        private void rdoWorkShop_Click(object sender, EventArgs e)
        {
            this.rdoWorkShop.Checked = true;
            this.rdoSSPanel.Checked = false;

            this.ucWorkshop.SelectedIndex = 0;

            this.ucWorkshop.Enabled = true;
            this.ucWorkShopBySSCode.Enabled = false;
            this.ucProductLine.Enabled = false;
            this.ucProductLine.SelectedIndex = 0;
            this.ucWorkShopBySSCode.SelectedIndex = 0;
        }

        private void rdoSSPanel_Click(object sender, EventArgs e)
        {
            this.rdoSSPanel.Checked = true;
            this.rdoWorkShop.Checked = false;

            this.ucProductLine.SelectedIndex = 0;
            this.ucWorkShopBySSCode.SelectedIndex = 0;

            this.ucWorkShopBySSCode.Enabled = true;
            this.ucProductLine.Enabled = true;
            this.ucWorkshop.Enabled = false;
            this.ucWorkshop.SelectedIndex = 0;
        }
        #endregion

        #region Button
        private void btConfig_Click(object sender, EventArgs e)
        {
            if (ClickBeforeCheck())
            {
                FPanelConfigDetails panelDetails = null;
                if (this.rdoSSPanel.Checked)
                {
                    panelDetails = new FPanelConfigDetails(this.ucProductLine.SelectedItemValue.ToString(), this.ucWorkShopBySSCode.SelectedItemValue.ToString());
                }
                else
                {
                    panelDetails = new FPanelConfigDetails(string.Empty, this.ucWorkshop.SelectedItemValue.ToString());
                }
                panelDetails.Owner = this;
                panelDetails.ShowDialog(this);
            }
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            //运行前检查WatchConfig.xml中需要的数据是否已经配置齐全
            if (ClickBeforeCheck())
            {
                string coinfigName = string.Empty;
                if (rdoWorkShop.Checked)
                {
                    coinfigName = this.ucWorkshop.SelectedItemValue.ToString().ToUpper();
                }
                if (rdoSSPanel.Checked)
                {
                    coinfigName = this.ucWorkShopBySSCode.SelectedItemValue.ToString().ToUpper() + "_" + this.ucProductLine.SelectedItemValue.ToString().ToUpper();
                }
                if (HasBeenConfigured(coinfigName))
                {
                    FFacWatchPanelNew watchPanel = null;
                    if (this.rdoSSPanel.Checked)
                    {
                        watchPanel = new FFacWatchPanelNew(this.ucWorkShopBySSCode.SelectedItemValue.ToString().ToUpper(), this.ucProductLine.SelectedItemValue.ToString().ToUpper(), WatchPanelType.ProductLine, coinfigName);
                    }
                    if (this.rdoWorkShop.Checked)
                    {
                        watchPanel = new FFacWatchPanelNew(this.ucWorkshop.SelectedItemValue.ToString().ToUpper(), string.Empty, WatchPanelType.WorkShop, coinfigName);
                    }
                    watchPanel.Owner = this;
                    watchPanel.ShowDialog(this);
                }
            }
        }
        #endregion

        #region  数据初始化
        //车间代码
        private void ucWorkshop_Load(object sender, EventArgs e)
        {
            this.ucWorkshop.Clear();
            this.ucWorkshop.AddItem("", "");
            if (_watchFacade == null)
            {
                _watchFacade = new WatchPanelFacade(_dataProvider);
            }
            object[] segmentList = _watchFacade.GetAllWorkShop();
            if (segmentList != null && segmentList.Length != 0)
            {
                foreach (Segment seg in segmentList)
                {
                    ucWorkshop.AddItem(seg.SegmentCode + "-" + seg.SegmentDescription, seg.SegmentCode);
                }
            }
            this.ucWorkshop.SelectedIndex = 0;
        }
        //车间代码
        private void ucWorkShopBySSCode_Load(object sender, EventArgs e)
        {
            this.ucWorkShopBySSCode.Clear();
            this.ucWorkShopBySSCode.AddItem("", "");
            if (_watchFacade == null)
            {
                _watchFacade = new WatchPanelFacade(_dataProvider);
            }
            object[] segmentList = _watchFacade.GetAllWorkShop();
            if (segmentList != null && segmentList.Length != 0)
            {
                foreach (Segment seg in segmentList)
                {
                    ucWorkShopBySSCode.AddItem(seg.SegmentCode + "-" + seg.SegmentDescription, seg.SegmentCode);
                }
            }
            this.ucWorkShopBySSCode.SelectedIndex = 0;
        }

        //产线代码
        private void ucProductLine_Load(object sender, EventArgs e)
        {
            this.ucProductLine.Clear();
            if (_watchFacade == null)
            {
                _watchFacade = new WatchPanelFacade(_dataProvider);
            }
            this.ucProductLine.AddItem("", "");
            object[] ssCodeList = _watchFacade.GetStepSequenceBySeg(this.ucWorkShopBySSCode.SelectedItemValue.ToString().Trim());
            if (ssCodeList != null && ssCodeList.Length != 0)
            {
                foreach (StepSequence sscode in ssCodeList)
                {
                    this.ucProductLine.AddItem(sscode.StepSequenceCode + "-" + sscode.StepSequenceDescription, sscode.StepSequenceCode);
                }
            }
            this.ucProductLine.SelectedIndex = 0;
        }
        #endregion

        //产线随着车间不同而级联变动
        private void ucWorkShopBySSCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucProductLine_Load(null, null);
        }

        private bool HasBeenConfigured(string configName)
        {
            bool isDetailConfigured = false;
            string result = InitControlsByXml(configName, out isDetailConfigured);
            //没有该项目的配置信息
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("请先点击配置维护看板必要的参数");
                return false;
            }
            else
            {
                if (isDetailConfigured)
                {
                    if (MessageBox.Show(result, "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (result.Equals(true.ToString()))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(result);
                        return false;
                    }
                }
            }
        }

        private bool ClickBeforeCheck()
        {
            //如果是选中的是产线，必须同时在车间和产线中选中值才可以点击按钮
            if (this.rdoWorkShop.Checked)
            {
                if (string.IsNullOrEmpty(this.ucWorkshop.SelectedItemValue.ToString().Trim()))
                {
                    MessageBox.Show(this.ucWorkshop.Caption + " 请选择非空的值");
                    return false;
                }
            }
            else
            {
                string errorMessage = string.Empty;
                if (string.IsNullOrEmpty(this.ucWorkShopBySSCode.SelectedItemValue.ToString().Trim()))
                {
                    errorMessage = this.ucWorkShopBySSCode.Caption + " 请选择非空的值" + "\r\n";
                }
                if (string.IsNullOrEmpty(this.ucProductLine.SelectedItemValue.ToString().Trim()))
                {
                    errorMessage += this.ucProductLine.Caption + " 请选择非空的值";
                }
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(errorMessage);
                    this.ucWorkShopBySSCode.Focus();
                    return false;
                }
            }
            return true;
        }

        //从XML文件中读取已经保存的配置信息
        private string InitControlsByXml(string configName, out bool isDetailConfigured)
        {
            isDetailConfigured = false;
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(xmlPath);
            }
            catch (Exception)
            {
                return "请先点击'配置'维护看板必要的参数";
            }

            XmlNode xmlNode = xmlDoc.SelectSingleNode("//TypeConfig[@ConfigName='" + configName + "']");
            if (xmlNode != null)
            {
                XmlNodeList xmlNodeList = xmlNode.ChildNodes;
                foreach (XmlNode xn in xmlNodeList)
                {
                    XmlElement xe = (XmlElement)xn;

                    if (xe.Name.Equals("ComprehensivePanel"))
                    {
                        if (xe.InnerText.ToUpper().Equals(false.ToString().ToUpper()))
                        {
                            isDetailConfigured = true;
                            return "综合看板配置项没有维护，确认要运行吗？";
                        }
                    }
                    if (xe.Name.Equals("PanelDetails"))
                    {
                        if (xe.InnerText.ToUpper().Equals(false.ToString().ToUpper()))
                        {
                            //明细看板没有配置，提示确认后继续
                            isDetailConfigured = true;
                            return "明细看板配置项没有维护，确认要运行吗？";
                        }
                    }
                }
                return  true.ToString();
            }
            return string.Empty;
        }
    }

    public class WatchPanelType
    {
        private static string workShop;
        private static string productLine;

        public static string WorkShop
        {
            get { return "WorkShopPanel"; }
        }

        public static string ProductLine
        {
            get { return "ProductLinePanel"; }
        }
    }
}
