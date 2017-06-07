using System;
using System.Windows.Forms;
using System.Xml;

namespace BenQGuru.eMES.WatchPanelNew
{
    public partial class FPanelConfigDetails : Form
    {
        #region  变量
        private string _ssCode;
        private string _workShopCode;
        private string _configTypeName;

        public string SsCode
        {
            get { return _ssCode; }
            set { _ssCode = value; }
        }

        public string WorkShopCode
        {
            get { return _workShopCode; }
            set { _workShopCode = value; }
        }
        #endregion

        public FPanelConfigDetails()
        {
            InitializeComponent();
        }

        private void FPanelConfigDetails_Load(object sender, EventArgs e)
        {
            this.ucSSCode.Value = this._ssCode;
            this.ucWorkShopCode.Value = this._workShopCode;
           // this.choSynthesized.Checked = true;  //综合看板应该配置，给出默认
            InitControlsByXml();

            if (!this.choStandBy.Checked)
            {
                this.ButtonSet.Enabled = false;
            }
            //if (!this.choPanelDetails.Checked)
            //{
            //    this.ucPageScrolling.Enabled = false;
            //    this.ucMaxLineCount.Enabled = false;
            //}
            //if (!this.choSynthesized.Checked)
            //{
            //    this.ucAutoRefresh.Enabled = false;
            //    this.ucScreenRefresh.Enabled = false;
            //}
            this.ucAutoRefresh.TextFocus(false, false);
        }

        public FPanelConfigDetails(string ssCode, string workShopCode)
        {
            this._ssCode = ssCode;
            this._workShopCode = workShopCode;
            if (string.IsNullOrEmpty(ssCode))
            {
                _configTypeName = workShopCode.ToUpper();
            }
            else
            {
                _configTypeName = (workShopCode + "_" + ssCode).ToUpper();
            }
            InitializeComponent();
        }

        #region Button
        //设置待机内容
        private void ButtonSet_Click(object sender, EventArgs e)
        {
            FFacMessageEdit messageEdit = new FFacMessageEdit();
            messageEdit.Owner = this;
            messageEdit.Show(this);
        }

        //保存配置信息
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (InputVerification() && CheckInputValue())
            {
                //产看配置了那个看板，不能三个都不选
                if (!this.choPanelDetails.Checked && (!this.choStandBy.Checked) && (!this.choSynthesized.Checked))
                {
                    MessageBox.Show("请至少选择一种看板类型！");
                    return;
                }

                string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);

                XmlNode NodeFormat = xmlDoc.SelectSingleNode("//FacWatchConfig[@Attribute='Fac']");

                XmlNodeList NodeList = NodeFormat.ChildNodes;

                foreach (XmlNode xd in NodeList)
                {
                    XmlElement xe = (XmlElement)xd;
                    if (xe.GetAttribute("ConfigName").ToString().ToUpper().Equals(_configTypeName))
                    {
                        if (_configTypeName == string.Empty)
                        {
                            if (MessageBox.Show("该配置项已经存在!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                            {
                                return;
                            }
                        }
                        else
                        {
                            ChangeCuerrtXmlNode(xmlDoc, xd, xmlPath);
                            this.Close();
                            return;
                        }
                    }
                }

                SaveNewXmlNode(xmlDoc, xmlPath);
                this.Close();
                return;
            }
        }

        //取消编辑信息
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.ucAutoRefresh.TextFocus(true, false);
            this.ucMaxLineCount.Value = string.Empty;
            this.ucPageScrolling.Value = string.Empty;
            this.ucScreenRefresh.Value = string.Empty;

            this.choPanelDetails.Checked = false;
            this.choStandBy.Checked = false;
            this.choSynthesized.Checked = true;

            this.rdoDay.Checked = true;
            this.rdoMonth.Checked = false;
            this.rdoWeek.Checked = false;

            this.ucAutoRefresh.TextFocus(false, false);
        }

        //关闭页面
        private void ButtonClosed_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 辅助方法
        private bool InputVerification()
        {
            string errorMessage = string.Empty;

            if (this.choPanelDetails.Checked)
            {
                if (string.IsNullOrEmpty(this.ucMaxLineCount.Value.Trim()))
                {
                    errorMessage += this.ucMaxLineCount.Caption + " 缺少输入" + "\r\n";
                }
                if (string.IsNullOrEmpty(this.ucPageScrolling.Value.Trim()))
                {
                    errorMessage += this.ucPageScrolling.Caption + " 缺少输入" + "\r\n";
                }
            }

            if (string.IsNullOrEmpty(this.ucAutoRefresh.Value.Trim()))
            {
                errorMessage = this.ucAutoRefresh.Caption + " 缺少输入" + "\r\n";
            }

            if (string.IsNullOrEmpty(this.ucScreenRefresh.Value.Trim()))
            {
                errorMessage += this.ucScreenRefresh.Caption + " 缺少输入" + "\r\n";
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage);
                this.ucAutoRefresh.TextFocus(false, false);
                return false;
            }

            //检查每页行数在5~20 中间
            if (!string.IsNullOrEmpty(this.ucMaxLineCount.Value.Trim()))
            {
                if ((Convert.ToInt32(this.ucMaxLineCount.Value.Trim()) < 5 || Convert.ToInt32(this.ucMaxLineCount.Value.Trim()) > 50))
                {
                    MessageBox.Show(this.ucMaxLineCount.Caption + "输入值必须为5~50中的正整数");
                    this.ucMaxLineCount.TextFocus(false, true);
                    return false;
                }
            }
            return true;
        }

        public bool CheckInputValue()
        {
            if (this.ucMaxLineCount.Value.Trim().IndexOf('-') != -1 || this.ucMaxLineCount.Value.Trim().Equals("0"))
            {
                MessageBox.Show(this.ucMaxLineCount.Caption + "输入值必须为大于0正整数");
                this.ucMaxLineCount.TextFocus(false, true);
                return false;
            }
            if (this.ucPageScrolling.Value.Trim().IndexOf('-') != -1 || this.ucPageScrolling.Value.Trim().Equals("0"))
            {
                MessageBox.Show(this.ucPageScrolling.Caption + "输入值必须为大于0正整数");
                this.ucPageScrolling.TextFocus(false, true);
                return false;
            }
            if (this.ucAutoRefresh.Value.Trim().IndexOf('-') != -1 || this.ucAutoRefresh.Value.Trim().Equals("0"))
            {
                MessageBox.Show(this.ucAutoRefresh.Caption + "输入值必须为大于0正整数");
                this.ucAutoRefresh.TextFocus(false, true);
                return false;
            }
            if (this.ucScreenRefresh.Value.Trim().IndexOf('-') != -1 || this.ucScreenRefresh.Value.Trim().Equals("0"))
            {
                MessageBox.Show(this.ucScreenRefresh.Caption + "输入值必须为大于0正整数");
                this.ucScreenRefresh.TextFocus(false, true);
                return false;
            }
            return true;
        }
        #endregion

        #region XML操作
        //对已经存在的配置项信息修改
        private void ChangeCuerrtXmlNode(XmlDocument xmlDoc, XmlNode newNodeFormat, string xmlPath)
        {
            XmlNodeList xmlNodeList = newNodeFormat.ChildNodes;
            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement xe = (XmlElement)xn;

                //待机显示内容
                if (xe.Name.Equals("FacMessage"))
                {
                    xe.InnerText = FacConfigMessage.CommonInfo;
                }
                //CheckBox待机内容
                if (xe.Name.Equals("StandbyContent"))
                {
                    if (this.choStandBy.Checked)
                    {
                        xe.InnerText = true.ToString().ToUpper();
                    }
                    else
                    {
                        xe.InnerText = false.ToString().ToUpper();
                    }
                }
                //CheckBox综合看板
                if (xe.Name.Equals("ComprehensivePanel"))
                {
                    if (this.choSynthesized.Checked)
                    {
                        xe.InnerText = true.ToString().ToUpper();
                    }
                    else
                    {
                        xe.InnerText = false.ToString().ToUpper();
                    }
                }
                //CheckBox看板明细
                if (xe.Name.Equals("PanelDetails"))
                {
                    if (this.choPanelDetails.Checked)
                    {
                        xe.InnerText = true.ToString().ToUpper();
                    }
                    else
                    {
                        xe.InnerText = false.ToString().ToUpper();
                    }
                }

                //自动刷新频率
                if (xe.Name.Equals("AutoRefresh"))
                {
                    xe.InnerText = this.ucAutoRefresh.Value.Trim();
                }

                //屏幕刷新频率
                if (xe.Name.Equals("ScreenRefresh"))
                {
                    xe.InnerText = this.ucScreenRefresh.Value.Trim();
                }

                //每页显示行数
                if (xe.Name.Equals("MaxLineCount"))
                {
                    xe.InnerText = this.ucMaxLineCount.Value.Trim();
                }
                //页面滚动频率
                if (xe.Name.Equals("PageScrolling"))
                {
                    xe.InnerText = this.ucPageScrolling.Value.Trim();
                }
                //按天分时段
                if (xe.Name.Equals("DividedHoursByDay"))
                {
                    if (this.rdoDay.Checked)
                    {
                        xe.InnerText = true.ToString().ToUpper();
                    }
                    else
                    {
                        xe.InnerText = false.ToString().ToUpper();
                    }
                }
                //按周分时段
                if (xe.Name.Equals("DividedHoursByWeek"))
                {
                    if (this.rdoWeek.Checked)
                    {
                        xe.InnerText = true.ToString().ToUpper();
                    }
                    else
                    {
                        xe.InnerText = false.ToString().ToUpper();
                    }
                }
                //按月分时段
                if (xe.Name.Equals("DividedHoursByMonth"))
                {
                    if (this.rdoMonth.Checked)
                    {
                        xe.InnerText = true.ToString().ToUpper();
                    }
                    else
                    {
                        xe.InnerText = false.ToString().ToUpper();
                    }
                }
            }
            xmlDoc.Save(xmlPath);
            MessageBox.Show("修改配置项成功!", "提示", MessageBoxButtons.OK);
        }

        //从XML文件中读取已经保存的配置信息
        private void InitControlsByXml()
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("//TypeConfig[@ConfigName='" + this._configTypeName + "']");
            if (xmlNode != null)
            {
                XmlNodeList xmlNodeList = xmlNode.ChildNodes;
                foreach (XmlNode xn in xmlNodeList)
                {
                    XmlElement xe = (XmlElement)xn;
                    if (xe.Name.Equals("StandbyContent"))
                    {
                        if (xe.InnerText.ToUpper().Equals(true.ToString().ToUpper()))
                        {
                            this.choStandBy.Checked = true;
                        }
                        else
                        {
                            this.choStandBy.Checked = false;
                        }
                    }
                    if (xe.Name.Equals("FacMessage"))
                    {
                        FacConfigMessage.CommonInfo = xe.InnerText;
                    }
                    if (xe.Name.Equals("ComprehensivePanel"))
                    {
                        if (xe.InnerText.ToUpper().Equals(true.ToString().ToUpper()))
                        {
                            this.choSynthesized.Checked = true;
                        }
                        else
                        {
                            this.choSynthesized.Checked = false;
                        }
                    }
                    if (xe.Name.Equals("PanelDetails"))
                    {
                        if (xe.InnerText.ToUpper().Equals(true.ToString().ToUpper()))
                        {
                            this.choPanelDetails.Checked = true;
                        }
                        else
                        {
                            this.choPanelDetails.Checked = false;
                        }
                    }
                    if (xe.Name.Equals("AutoRefresh"))
                    {
                        this.ucAutoRefresh.Value = xe.InnerText;
                    }
                    if (xe.Name.Equals("ScreenRefresh"))
                    {
                        this.ucScreenRefresh.Value = xe.InnerText;
                    }
                    if (xe.Name.Equals("MaxLineCount"))
                    {
                        this.ucMaxLineCount.Value = xe.InnerText;
                    }
                    if (xe.Name.Equals("PageScrolling"))
                    {
                        this.ucPageScrolling.Value = xe.InnerText;
                    }
                    if (xe.Name.Equals("DividedHoursByDay"))
                    {
                        if (xe.InnerText.ToUpper().Equals(true.ToString().ToUpper()))
                        {
                            this.rdoDay.Checked = true;
                        }
                        else
                        {
                            this.rdoDay.Checked = false;
                        }
                    }
                    if (xe.Name.Equals("DividedHoursByWeek"))
                    {
                        if (xe.InnerText.ToUpper().Equals(true.ToString().ToUpper()))
                        {
                            this.rdoWeek.Checked = true;
                        }
                        else
                        {
                            this.rdoWeek.Checked = false;
                        }
                    }
                    if (xe.Name.Equals("DividedHoursByMonth"))
                    {
                        if (xe.InnerText.ToUpper().Equals(true.ToString().ToUpper()))
                        {
                            this.rdoMonth.Checked = true;
                        }
                        else
                        {
                            this.rdoMonth.Checked = false;
                        }
                    }
                }
            }
        }

        //创建新的配置项
        private void SaveNewXmlNode(XmlDocument xmlDoc, string xmlPath)
        {
            XmlNode NodeFormat = xmlDoc.SelectSingleNode("//FacWatchConfig[@Attribute='Fac']");

            XmlElement configTypeElement = xmlDoc.CreateElement("TypeConfig");
            XmlText txtConfigType = xmlDoc.CreateTextNode("");
            configTypeElement.AppendChild(txtConfigType);
            configTypeElement.SetAttribute("ConfigName", this._configTypeName.ToUpper());
            NodeFormat.AppendChild(configTypeElement);

            XmlElement facMessageElement = xmlDoc.CreateElement("FacMessage");
            XmlText textFacMessage = xmlDoc.CreateTextNode(FacConfigMessage.CommonInfo);
            facMessageElement.AppendChild(textFacMessage);
            NodeFormat.AppendChild(configTypeElement).AppendChild(facMessageElement);

            XmlElement standbyContent = xmlDoc.CreateElement("StandbyContent");
            XmlText txtStandbyContent = xmlDoc.CreateTextNode(this.choStandBy.Checked.ToString().ToUpper());
            standbyContent.AppendChild(txtStandbyContent);
            NodeFormat.AppendChild(configTypeElement).AppendChild(standbyContent);

            XmlElement comprehensivePanel = xmlDoc.CreateElement("ComprehensivePanel");
            XmlText txtComprehensivePanel = xmlDoc.CreateTextNode(this.choSynthesized.Checked.ToString().ToUpper());
            comprehensivePanel.AppendChild(txtComprehensivePanel);
            NodeFormat.AppendChild(configTypeElement).AppendChild(comprehensivePanel);

            XmlElement panelDetails = xmlDoc.CreateElement("PanelDetails");
            XmlText txtPanelDetails = xmlDoc.CreateTextNode(this.choPanelDetails.Checked.ToString().ToUpper());
            panelDetails.AppendChild(txtPanelDetails);
            NodeFormat.AppendChild(configTypeElement).AppendChild(panelDetails);

            XmlElement autoRefresh = xmlDoc.CreateElement("AutoRefresh");
            XmlText txtAutoRefresh = xmlDoc.CreateTextNode(this.ucAutoRefresh.Value.Trim());
            autoRefresh.AppendChild(txtAutoRefresh);
            NodeFormat.AppendChild(configTypeElement).AppendChild(autoRefresh);

            XmlElement screenRefresh = xmlDoc.CreateElement("ScreenRefresh");
            XmlText txtScreenRefresh = xmlDoc.CreateTextNode(this.ucScreenRefresh.Value.Trim());
            screenRefresh.AppendChild(txtScreenRefresh);
            NodeFormat.AppendChild(configTypeElement).AppendChild(screenRefresh);

            XmlElement maxLineCount = xmlDoc.CreateElement("MaxLineCount");
            XmlText txtMaxLineCount = xmlDoc.CreateTextNode(this.ucMaxLineCount.Value.Trim());
            maxLineCount.AppendChild(txtMaxLineCount);
            NodeFormat.AppendChild(configTypeElement).AppendChild(maxLineCount);

            XmlElement pageScrolling = xmlDoc.CreateElement("PageScrolling");
            XmlText txtPageScrolling = xmlDoc.CreateTextNode(this.ucPageScrolling.Value.Trim());
            pageScrolling.AppendChild(txtPageScrolling);
            NodeFormat.AppendChild(configTypeElement).AppendChild(pageScrolling);

            XmlElement dividedHoursByDay = xmlDoc.CreateElement("DividedHoursByDay");
            XmlText txtDividedHoursByDay = xmlDoc.CreateTextNode(this.rdoDay.Checked.ToString().ToUpper());
            dividedHoursByDay.AppendChild(txtDividedHoursByDay);
            NodeFormat.AppendChild(configTypeElement).AppendChild(dividedHoursByDay);

            XmlElement dividedHoursByWeek = xmlDoc.CreateElement("DividedHoursByWeek");
            XmlText txtDividedHoursByWeek = xmlDoc.CreateTextNode(this.rdoWeek.Checked.ToString().ToUpper());
            dividedHoursByWeek.AppendChild(txtDividedHoursByWeek);
            NodeFormat.AppendChild(configTypeElement).AppendChild(dividedHoursByWeek);

            XmlElement dividedHoursByMonth = xmlDoc.CreateElement("DividedHoursByMonth");
            XmlText txtDividedHoursByMonth = xmlDoc.CreateTextNode(this.rdoMonth.Checked.ToString().ToUpper());
            dividedHoursByMonth.AppendChild(txtDividedHoursByMonth);
            NodeFormat.AppendChild(configTypeElement).AppendChild(dividedHoursByMonth);

            xmlDoc.Save(xmlPath);
            MessageBox.Show("新增配置项信息成功!", "提示", MessageBoxButtons.OK);
        }
        #endregion

        //控制
        private void choSynthesized_Click(object sender, EventArgs e)
        {
            //if (!this.choSynthesized.Checked)
            //{
            //    this.ucAutoRefresh.Enabled = false;
            //    this.ucScreenRefresh.Enabled = false;
            //}
            //else
            //{
            //    this.ucAutoRefresh.Enabled = true;
            //    this.ucScreenRefresh.Enabled = true;
            //}
            this.ucAutoRefresh.TextFocus(false, true);
        }

        private void choStandBy_Click(object sender, EventArgs e)
        {
            if (this.choStandBy.Checked)
            {
                this.ButtonSet.Enabled = true;
            }
            else
            {
                this.ButtonSet.Enabled = false;
            }
        }

        private void choPanelDetails_Click(object sender, EventArgs e)
        {
            if (this.choPanelDetails.Checked)
            {
                this.ucMaxLineCount.Enabled = true;
                this.ucPageScrolling.Enabled = true;
            }
            else
            {
                this.ucMaxLineCount.Enabled = false;
                this.ucPageScrolling.Enabled = false;
            }
            this.ucMaxLineCount.TextFocus(false, true);
        }
        private void choPanelDetails_CheckedChanged(object sender, EventArgs e)
        {
            this.ucMaxLineCount.Value = string.Empty; ;
            this.ucPageScrolling.Value = string.Empty; ;
        }
    }

    public class TimeDimension
    {
        private static string _week;
        private static string _day;
        private static string _month;

        public static string Week
        {
            get { return "Week"; }
        }

        public static string Day
        {
            get { return "Day"; }
        }

        public static string Month
        {
            get { return "Month"; }
        }
    }
}
