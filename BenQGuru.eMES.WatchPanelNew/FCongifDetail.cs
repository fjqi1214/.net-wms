using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;



namespace BenQGuru.eMES.WatchPanelNew
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

                    _dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(Program.DBName);
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

            if ((chkFacOutPutAndRate.Checked || chkBigLineMessage.Checked || chkFacWatchPanel.Checked) && string.IsNullOrEmpty(this.txtSSCodeList.Text))
            {
                if (MessageBox.Show("请选择包含的产线!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.txtSSCodeList.Focus();
                    this.txtSSCodeList.BackColor = Color.GreenYellow;
                    return;
                }
            }

            if ((chkFacOutPutAndRate.Checked || chkFacWatchPanel.Checked) && string.IsNullOrEmpty(this.txtSSCodeListForFactory.Text))
            {
                if (MessageBox.Show("请选择车间包含的产线!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.txtSSCodeListForFactory.Focus();
                    this.txtSSCodeListForFactory.BackColor = Color.GreenYellow;
                    return;
                }
            }

            if (string.IsNullOrEmpty(this.ucLabelEditScreens.Value))
            {
                if (MessageBox.Show("请输入选择显示屏幕!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.ucLabelEditScreens.TextFocus(false, true);
                    return;
                }
            }

            string notExistLines = CheckSSCode();

            if (notExistLines.Length > 0)
            {
                if (MessageBox.Show("产线:" + notExistLines + " 不存在!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.txtSSCodeList.Focus();
                    this.txtSSCodeList.BackColor = Color.GreenYellow;
                    return;
                }
            }

            string notExistSSCode = CheckSSCodeForFactory();

            if (notExistSSCode.Length > 0)
            {
                if (MessageBox.Show("车间包含的产线:" + notExistSSCode + " 不存在!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.txtSSCodeListForFactory.Focus();
                    this.txtSSCodeListForFactory.BackColor = Color.GreenYellow;
                    return;
                }
            }

            if (string.IsNullOrEmpty(this.txtEQPID.Text))
            {
                if (MessageBox.Show("请选择设备ID!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.txtEQPID.Focus();
                    this.txtEQPID.BackColor = Color.GreenYellow;
                    return;
                }
            }
           
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

        private void btSelectSSCode_Click(object sender, EventArgs e)
        {
            FSelectSSCode fm = new FSelectSSCode();
            fm.Owner = this;
            fm.BiglineSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(ssCodeSelector_SSCodeSelectedEvent);
            fm.ShowDialog();
            fm = null;
        }

        private void btSelectEQPID_Click(object sender, EventArgs e)
        {
            FSelectEquipmentId fm = new FSelectEquipmentId();
            fm.Owner = this;
            fm.EQPIDSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(eqpIDSelector_EqpIDSelectedEvent);
            fm.ShowDialog();
            fm = null;
        }

        private void btSelectSSCodeForFactory_Click(object sender, EventArgs e)
        {
            FSelectSSCode fm = new FSelectSSCode();
            fm.Owner = this;
            fm.BiglineSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(ssCodeSelectorForFactory_SSCodeSelectedEvent);
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
            XmlText textBigLineList = xmlDoc.CreateTextNode(this.txtSSCodeList.Text.ToString());
            bigLineListElement.AppendChild(textBigLineList);
            NodeFormat.AppendChild(configTypeElement).AppendChild(bigLineListElement);

            XmlElement eqpIdListElement = xmlDoc.CreateElement("EqpIdList");
            XmlText txtEQPIDList = xmlDoc.CreateTextNode(this.txtEQPID.Text.ToString());
            eqpIdListElement.AppendChild(txtEQPIDList);
            NodeFormat.AppendChild(configTypeElement).AppendChild(eqpIdListElement);

            XmlElement sscodeForFactoryElement = xmlDoc.CreateElement("SSCodeForFactory");
            XmlText textSSCodeForFactory = xmlDoc.CreateTextNode(this.txtSSCodeListForFactory.Text.ToString());
            sscodeForFactoryElement.AppendChild(textSSCodeForFactory);
            NodeFormat.AppendChild(configTypeElement).AppendChild(sscodeForFactoryElement);

            XmlElement showRightAreaElement = xmlDoc.CreateElement("ShowRightArea");
            XmlText txtShowRightAreaList = xmlDoc.CreateTextNode(this.chkShowRightArea.Checked.ToString());
            showRightAreaElement.AppendChild(txtShowRightAreaList);
            NodeFormat.AppendChild(configTypeElement).AppendChild(showRightAreaElement);

            XmlElement bigLineNumberElement = xmlDoc.CreateElement("BigLineNumber");
            XmlText textBigLineNumber = xmlDoc.CreateTextNode(GetSelectedLineNumber().ToString());
            bigLineNumberElement.AppendChild(textBigLineNumber);
            NodeFormat.AppendChild(configTypeElement).AppendChild(bigLineNumberElement);
                                    
            XmlElement showScreensNumber = xmlDoc.CreateElement("ShowScreensNumber");
            XmlText textShowScreensNumber = xmlDoc.CreateTextNode(this.ucLabelEditScreens.Value.ToString());
            showScreensNumber.AppendChild(textShowScreensNumber);
            NodeFormat.AppendChild(configTypeElement).AppendChild(showScreensNumber);
            
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
                    xe.InnerText = this.txtSSCodeList.Text.ToString();
                }
               
                if (xe.Name == "EqpIdList")
                {
                    xe.InnerText = this.txtEQPID.Text.ToString();
                }

                if (xe.Name == "SSCodeForFactory")
                {
                    xe.InnerText = this.txtSSCodeListForFactory.Text.ToString();
                }

                if (xe.Name == "ShowRightArea")
                {
                    xe.InnerText = this.chkShowRightArea.Checked.ToString();
                }                

                if (xe.Name == "BigLineNumber")
                {
                    xe.InnerText = GetSelectedLineNumber().ToString();
                }
               
                if (xe.Name == "ShowScreensNumber")
                {
                    xe.InnerText = this.ucLabelEditScreens.Value.ToString();
                }                
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
                        this.txtSSCodeList.Text = xe.InnerText;
                    }

                    if (xe.Name == "EqpIdList")
                    {
                        this.txtEQPID.Text = xe.InnerText;
                    }

                    if (xe.Name == "SSCodeForFactory")
                    {
                        this.txtSSCodeListForFactory.Text = xe.InnerText;
                    }

                    if (xe.Name == "ShowRightArea")
                    {
                        if (xe.InnerText.ToLower() == "true")
                        {
                            this.chkShowRightArea.Checked = true;
                        }
                        else
                        {
                            this.chkShowRightArea.Checked = false;
                        }
                    }                 
                   
                    if (xe.Name == "BigLineNumber")
                    {
                        if (xe.InnerText == "1")
                        {
                            this.rbtOneLine.Checked = true;
                        }
                    }                                      

                    if (xe.Name == "ShowScreensNumber")
                    {
                        this.ucLabelEditScreens.Value = xe.InnerText;
                    }
                    
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

        private bool InputValueCheck()
        {
            return true;
        }

        private void ssCodeSelector_SSCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtSSCodeList.Text = e.CustomObject;
        }

        private void eqpIDSelector_EqpIDSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtEQPID.Text = e.CustomObject;
        }

        private void ssCodeSelectorForFactory_SSCodeSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.txtSSCodeListForFactory.Text = e.CustomObject;
        }

        private string CheckSSCode()
        {
            string returnValue = string.Empty;
            try
            {

                WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
                object[] ssCodeList = watchPanelFacade.GetAllStepSequence();

                string[] textSSCodeList = this.txtSSCodeList.Text.ToUpper().Trim().Split(',');

                for (int i = 0; i < textSSCodeList.Length; i++)
                {
                    for (int j = 0; j < ssCodeList.Length; j++)
                    {
                        if (((StepSequence)ssCodeList[j]).StepSequenceCode.Trim().ToUpper() == textSSCodeList[i].Trim().ToUpper())
                        {
                            break;
                        }

                        if (j == ssCodeList.Length - 1)
                        {
                            returnValue += textSSCodeList[i].Trim().ToUpper() + ",";
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                if (ex.Message == "$Error_Command_Execute")
                {
                    MessageBox.Show("数据库连接错误，请检查配置！");
                }
            }

            if (returnValue.Length > 0)
            {
                returnValue = returnValue.Substring(0, returnValue.Length - 1);
            }
            return returnValue;
        }

        private string CheckSSCodeForFactory()
        {
            string returnValue = string.Empty;
            try
            {

                WatchPanelFacade watchPanelFacade = new WatchPanelFacade(this.DataProvider);
                object[] ssCodeList = watchPanelFacade.GetAllStepSequence();

                string[] txtSSCodeListForFactory = this.txtSSCodeListForFactory.Text.ToUpper().Trim().Split(',');

                for (int i = 0; i < txtSSCodeListForFactory.Length; i++)
                {
                    for (int j = 0; j < ssCodeList.Length; j++)
                    {
                        if (((StepSequence)ssCodeList[j]).StepSequenceCode.Trim().ToUpper() == txtSSCodeListForFactory[i].Trim().ToUpper())
                        {
                            break;
                        }

                        if (j == ssCodeList.Length - 1)
                        {
                            returnValue += txtSSCodeListForFactory[i].Trim().ToUpper() + ",";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.Message == "$Error_Command_Execute")
                {
                    MessageBox.Show("数据库连接错误，请检查配置！");
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