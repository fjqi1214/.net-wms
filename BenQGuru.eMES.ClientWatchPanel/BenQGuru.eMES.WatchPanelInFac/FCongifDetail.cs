using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class FCongifDetail : Form
    {
        #region  变量
        private string _configTypeName = string.Empty;
        private string _FacMessage = string.Empty;

        private IDomainDataProvider _dataProvider;
        public IDomainDataProvider DataProvider
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

        public FCongifDetail()
        {
            InitializeComponent();
            FacConfigMessage.CommonInfo = string.Empty;
        }

        public FCongifDetail(string configTypeName)
        {
            InitializeComponent();
            _configTypeName = configTypeName;
            this.txtConfigName.Text = configTypeName;
            InitControlsByXml();
            this.txtConfigName.Enabled = false;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btInit_Click(object sender, EventArgs e)
        {
            FFacMessageEdit frm = new FFacMessageEdit();
            frm.Owner = this;
            frm.ShowDialog(this);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtConfigName.Text))
            {
                if (MessageBox.Show("请输入当前配置项名称!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.txtConfigName.Focus();
                    this.txtConfigName.BackColor = Color.GreenYellow;
                    return;
                }
            }

            if (string.IsNullOrEmpty(this.txtAutoRefrsh.Value))
            {
                if (MessageBox.Show("请输入自动刷新频率!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.txtAutoRefrsh.TextFocus(false, true);
                    return;
                }
            }

            if (string.IsNullOrEmpty(this.txtWatchrRefrsh.Value))
            {
                if (MessageBox.Show("请输入屏幕切换频率!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.txtWatchrRefrsh.TextFocus(false, true);
                    return;
                }
            }

            if ((chkFacOutPutAndRate.Checked || chkBigLineMessage.Checked || chkFacWatchPanel.Checked) && string.IsNullOrEmpty(this.txtBigLineList.Text))
            {
                if (MessageBox.Show("请选择包含的产线!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.txtBigLineList.Focus();
                    this.txtBigLineList.BackColor = Color.GreenYellow;
                    return;
                }
            }

            string notExistLines = CheckBigLine();

            if (notExistLines.Length > 0)
            {
                if (MessageBox.Show("产线:" + notExistLines + " 不存在!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.txtBigLineList.Focus();
                    this.txtBigLineList.BackColor = Color.GreenYellow;
                    return;
                }
            }
            //Added By Nettie Chen 2009/09/22
            if (chkFinishedProduct.Checked==false && chkSemimanuFacture.Checked==false )
            {
                if (MessageBox.Show("请选择包含的成品类别!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    return;
                }
            }
            //End Added


            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            this.txtConfigName.Text = this.txtConfigName.Text.Replace("'", "");
            XmlNode NodeFormat = xmlDoc.SelectSingleNode("//FacWatchConfig[@Attribute='Fac']");

            XmlNodeList NodeList = NodeFormat.ChildNodes;

            foreach (XmlNode xd in NodeList)
            {
                XmlElement xe = (XmlElement)xd;
                if (xe.GetAttribute("ConfigName").ToString().ToUpper() == this.txtConfigName.Text.ToUpper())
                {
                    if (_configTypeName == string.Empty)
                    {
                        if (MessageBox.Show("该配置项已经存在!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                        {
                            this.txtConfigName.Focus();
                            this.txtConfigName.BackColor = Color.GreenYellow;
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

        private void btSelected_Click(object sender, EventArgs e)
        {
            FSelectBigLines fm = new FSelectBigLines();
            fm.Owner = this;
            fm.BiglineSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(bigLineSelector_BigLineSelectedEvent);
            fm.ShowDialog();
            fm = null;
        }
        #endregion

        #region 自定义事件

        private void SaveNewXmlNode(XmlDocument xmlDoc, string xmlPath)
        {
            XmlNode NodeFormat = xmlDoc.SelectSingleNode("//FacWatchConfig[@Attribute='Fac']");

            XmlElement configTypeElement = xmlDoc.CreateElement("TypeConfig");
            XmlText txtConfigType = xmlDoc.CreateTextNode("");
            configTypeElement.AppendChild(txtConfigType);
            configTypeElement.SetAttribute("ConfigName", this.txtConfigName.Text.ToUpper());
            NodeFormat.AppendChild(configTypeElement);

            XmlElement facSurveyElement = xmlDoc.CreateElement("FacSurvey");
            XmlText textFacSurvey = xmlDoc.CreateTextNode(this.chkFacMessage.Checked.ToString());
            facSurveyElement.AppendChild(textFacSurvey);
            NodeFormat.AppendChild(configTypeElement).AppendChild(facSurveyElement);

            XmlElement facMessageElement = xmlDoc.CreateElement("FacMessage");
            XmlText textFacMessage = xmlDoc.CreateTextNode(FacConfigMessage.CommonInfo);
            facMessageElement.AppendChild(textFacMessage);
            NodeFormat.AppendChild(configTypeElement).AppendChild(facMessageElement);

            XmlElement facWatchPanelElement = xmlDoc.CreateElement("FacWatchPanel");
            XmlText textFacWatchPanel = xmlDoc.CreateTextNode(this.chkFacWatchPanel.Checked.ToString());
            facWatchPanelElement.AppendChild(textFacWatchPanel);
            NodeFormat.AppendChild(configTypeElement).AppendChild(facWatchPanelElement);

            XmlElement facQtyAndRateElement = xmlDoc.CreateElement("FacQtyAndRate");
            XmlText textfacQtyAndRate = xmlDoc.CreateTextNode(this.chkFacOutPutAndRate.Checked.ToString());
            facQtyAndRateElement.AppendChild(textfacQtyAndRate);
            NodeFormat.AppendChild(configTypeElement).AppendChild(facQtyAndRateElement);

            XmlElement lineSurveyElement = xmlDoc.CreateElement("LineSurvey");
            XmlText textLineSurvey = xmlDoc.CreateTextNode(this.chkBigLineMessage.Checked.ToString());
            lineSurveyElement.AppendChild(textLineSurvey);
            NodeFormat.AppendChild(configTypeElement).AppendChild(lineSurveyElement);

            XmlElement autoRefreshElement = xmlDoc.CreateElement("AutoRefresh");
            XmlText textAutoRefresh = xmlDoc.CreateTextNode(this.txtAutoRefrsh.Value.ToString());
            autoRefreshElement.AppendChild(textAutoRefresh);
            NodeFormat.AppendChild(configTypeElement).AppendChild(autoRefreshElement);

            XmlElement watchRefreshElement = xmlDoc.CreateElement("WatchRefresh");
            XmlText textWatchRefresh = xmlDoc.CreateTextNode(this.txtWatchrRefrsh.Value.ToString());
            watchRefreshElement.AppendChild(textWatchRefresh);
            NodeFormat.AppendChild(configTypeElement).AppendChild(watchRefreshElement);

            XmlElement bigLineListElement = xmlDoc.CreateElement("BigLineList");
            XmlText textBigLineList = xmlDoc.CreateTextNode(this.txtBigLineList.Text.ToString());
            bigLineListElement.AppendChild(textBigLineList);
            NodeFormat.AppendChild(configTypeElement).AppendChild(bigLineListElement);

            XmlElement bigLineNumberElement = xmlDoc.CreateElement("BigLineNumber");
            XmlText textBigLineNumber = xmlDoc.CreateTextNode(GetSelectedLineNumber().ToString());
            bigLineNumberElement.AppendChild(textBigLineNumber);
            NodeFormat.AppendChild(configTypeElement).AppendChild(bigLineNumberElement);

            XmlElement timeStatElement = xmlDoc.CreateElement("TimeStat");
            XmlText textTimeStat = xmlDoc.CreateTextNode(GetTimeStat());
            timeStatElement.AppendChild(textTimeStat);
            NodeFormat.AppendChild(configTypeElement).AppendChild(timeStatElement);

            //Added By Nettie Chen 2009/09/22
            XmlElement facFinProductElement = xmlDoc.CreateElement("FinishedProduct");
            XmlText textFinishedProduct = xmlDoc.CreateTextNode(this.chkFinishedProduct.Checked.ToString());
            facFinProductElement.AppendChild(textFinishedProduct);
            NodeFormat.AppendChild(configTypeElement).AppendChild(facFinProductElement);

            XmlElement facSemFactureElement = xmlDoc.CreateElement("SemimanuFacture");
            XmlText textSemimanuFacture = xmlDoc.CreateTextNode(this.chkSemimanuFacture.Checked.ToString());
            facSemFactureElement.AppendChild(textSemimanuFacture);
            NodeFormat.AppendChild(configTypeElement).AppendChild(facSemFactureElement);
            //End Added

            xmlDoc.Save(xmlPath);
        }

        private void DeleteOldXmlNode(XmlDocument xmlDoc, string oldNodeName, string xmlPath)
        {
            XmlNodeList xmlNodeList = xmlDoc.SelectSingleNode("WatchConfig").ChildNodes;
            foreach (XmlNode node in xmlNodeList)
            {
                XmlElement xe = (XmlElement)node;
                if (xe.GetAttribute("ConfigName") == oldNodeName)
                {
                    xe.RemoveAll();
                }
            }

            xmlDoc.Save(xmlPath);
        }

        private void ChangeCuerrtXmlNode(XmlDocument xmlDoc, XmlNode newNodeFormat, string xmlPath)
        {
            XmlNodeList xmlNodeList = newNodeFormat.ChildNodes;
            foreach (XmlNode xn in xmlNodeList)
            {
                XmlElement xe = (XmlElement)xn;

                if (xe.Name == "FacSurvey")
                {
                    xe.InnerText = this.chkFacMessage.Checked.ToString();
                }

                if (xe.Name == "FacMessage")
                {
                    xe.InnerText = FacConfigMessage.CommonInfo;
                }

                if (xe.Name == "FacWatchPanel")
                {
                    xe.InnerText = this.chkFacWatchPanel.Checked.ToString();
                }

                if (xe.Name == "FacQtyAndRate")
                {
                    xe.InnerText = this.chkFacOutPutAndRate.Checked.ToString();
                }

                if (xe.Name == "LineSurvey")
                {
                    xe.InnerText = this.chkBigLineMessage.Checked.ToString();
                }

                if (xe.Name == "AutoRefresh")
                {
                    xe.InnerText = this.txtAutoRefrsh.Value.ToString();
                }

                if (xe.Name == "WatchRefresh")
                {
                    xe.InnerText = this.txtWatchrRefrsh.Value.ToString();
                }

                if (xe.Name == "BigLineList")
                {
                    xe.InnerText = this.txtBigLineList.Text.ToString();
                }

                if (xe.Name == "BigLineNumber")
                {
                    xe.InnerText = GetSelectedLineNumber().ToString();
                }
                if (xe.Name == "TimeStat")
                {
                    xe.InnerText = GetTimeStat();
                }
                //Added By Nettie Chen 2009/09/22
                if (xe.Name == "FinishedProduct")
                {
                    xe.InnerText = this.chkFinishedProduct.Checked.ToString();
                }
                if (xe.Name == "SemimanuFacture")
                {
                    xe.InnerText = this.chkSemimanuFacture.Checked.ToString();
                }
                //End Added
            }

            xmlDoc.Save(xmlPath);
        }

        private void InitControlsByXml()
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("//TypeConfig[@ConfigName='" + this.txtConfigName.Text + "']");

            if (xmlNode != null)
            {
                XmlNodeList xmlNodeList = xmlNode.ChildNodes;

                foreach (XmlNode xn in xmlNodeList)
                {
                    XmlElement xe = (XmlElement)xn;

                    if (xe.Name == "FacSurvey")
                    {
                        if (xe.InnerText.ToLower() == "true")
                        {
                            this.chkFacMessage.Checked = true;
                        }
                        else
                        {
                            this.chkFacMessage.Checked = false;
                        }
                    }

                    if (xe.Name == "FacMessage")
                    {
                        FacConfigMessage.CommonInfo = xe.InnerText;
                    }

                    if (xe.Name == "FacWatchPanel")
                    {
                        if (xe.InnerText.ToLower() == "true")
                        {
                            this.chkFacWatchPanel.Checked = true;
                        }
                        else
                        {
                            this.chkFacWatchPanel.Checked = false;
                        }
                    }
                    
                    if (xe.Name == "FacQtyAndRate")
                    {
                        if (xe.InnerText.ToLower() == "true")
                        {
                            this.chkFacOutPutAndRate.Checked = true;
                        }
                        else
                        {
                            this.chkFacOutPutAndRate.Checked = false;
                        }
                    }

                    if (xe.Name == "LineSurvey")
                    {
                        if (xe.InnerText.ToLower() == "true")
                        {
                            this.chkBigLineMessage.Checked = true;
                        }
                        else
                        {
                            this.chkBigLineMessage.Checked = false;
                        }
                    }

                    if (xe.Name == "AutoRefresh")
                    {
                        this.txtAutoRefrsh.Value = xe.InnerText;
                    }

                    if (xe.Name == "WatchRefresh")
                    {
                        this.txtWatchrRefrsh.Value = xe.InnerText;
                    }

                    if (xe.Name == "BigLineList")
                    {
                        this.txtBigLineList.Text = xe.InnerText;
                    }

                    if (xe.Name == "BigLineNumber")
                    {
                        if (xe.InnerText == "1")
                        {
                            this.rbtOneLine.Checked = true;
                        }                                               
                    }

                    if (xe.Name == "TimeStat")
                    {
                        if (xe.InnerText == "PerDay")
                        {
                            this.rbtPerDay.Checked = true;
                        }

                        if (xe.InnerText == "PerTime")
                        {
                            this.rbtPerTime.Checked = true;
                        }
                    }

                    //Added By Nettie Chen 2009/09/22
                    if (xe.Name == "FinishedProduct")
                    {
                        if (xe.InnerText.ToLower() == "true")
                        {
                            this.chkFinishedProduct.Checked = true;
                        }
                        else
                        {
                            this.chkFinishedProduct.Checked = false;
                        }
                    }
                    if (xe.Name == "SemimanuFacture")
                    {
                        if (xe.InnerText.ToLower() == "true")
                        {
                            this.chkSemimanuFacture.Checked = true;
                        }
                        else
                        {
                            this.chkSemimanuFacture.Checked = false;
                        }
                    }
                    //End Added
                }
            }

        }

        private int GetSelectedLineNumber()
        {
            int lineNum = 0;
            if (this.rbtOneLine.Checked)
            {
                lineNum = 1;
            }           

            return lineNum;
        }

        private string GetTimeStat()
        {
            string timeStat = string.Empty;
            if (this.rbtPerTime.Checked)
            {
                timeStat = "PerTime";
            }

            if (this.rbtPerDay.Checked)
            {
                timeStat = "PerDay";
            }

            return timeStat;
        }

        private bool InputValueCheck()
        {
            return true;
        }

        private void bigLineSelector_BigLineSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtBigLineList.Text = e.CustomObject;
        }

        private string CheckBigLine()
        {
            string returnValue = string.Empty;

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            object[] bigLineList = systemSettingFacade.GetAllBIGSSCODE();

            string[] textBigList = this.txtBigLineList.Text.ToUpper().Trim().Split(',');

            for (int i = 0; i < textBigList.Length; i++)
            {
                for (int j = 0; j < bigLineList.Length; j++)
                {
                    if (((Parameter)bigLineList[j]).ParameterAlias.Trim().ToUpper() == textBigList[i].Trim().ToUpper())
                    {
                        break;
                    }

                    if (j == bigLineList.Length - 1)
                    {
                        returnValue += textBigList[i].Trim().ToUpper() + ",";
                    }
                }
            }

            if (returnValue.Length > 0)
            {
                returnValue = returnValue.Substring(0, returnValue.Length - 1);
            }
            return returnValue;
        }
        #endregion

    }
}