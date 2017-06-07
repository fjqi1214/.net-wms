using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Web.UI.WebControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class FSelectConfig : Form
    {

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

        public FSelectConfig()
        {
            InitializeComponent();
        }

        private void FSelectConfig_Load(object sender, EventArgs e)
        {
            InitChbConfigList();
        }

        private void InitChbConfigList()
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("//FacWatchConfig[@Attribute='Fac']");
            XmlNodeList nodeList = xmlNode.ChildNodes;

            this.DrpConfigList.Clear();
            List<string> drpList = new List<string>();

            drpList.Add(string.Empty);
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                if (!string.IsNullOrEmpty(xe.GetAttribute("ConfigName")))
                {
                    drpList.Add(xe.GetAttribute("ConfigName"));
                }
            }

            drpList.Sort();
            for (int i = 0; i < drpList.Count; i++)
            {
                this.DrpConfigList.AddItem(drpList[i], drpList[i]);
            }

            this.DrpConfigList.SelectedIndex = 0;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            FCongifDetail frm = new FCongifDetail();
            frm.Owner = this;
            frm.ShowDialog(this);
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.DrpConfigList.SelectedItemValue.ToString()))
            {
                this.DrpConfigList.Focus();
                return;
            }

            if (!CheckConfig())
            {
                return;
            }

            FFacWatchPanel main = new FFacWatchPanel(this.DrpConfigList.SelectedItemValue.ToString());
            main.Owner = this;
            main.ShowDialog(this);
        }

        private void btChange_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DrpConfigList.SelectedItemValue.ToString()))
            {
                FCongifDetail frm = new FCongifDetail(this.DrpConfigList.SelectedItemValue.ToString());
                frm.Owner = this;
                frm.ShowDialog(this);
            }
        }

        private void DrpConfigList_SelectBox_DropDown(object sender, EventArgs e)
        {
            this.InitChbConfigList();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.DrpConfigList.SelectedItemValue.ToString()))
            {
                return;
            }

            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            XmlNode xmlNode = xmlDoc.SelectSingleNode("//FacWatchConfig[@Attribute='Fac']");
            XmlNodeList nodeList = xmlNode.ChildNodes;

            foreach (XmlNode node in nodeList)
            {
                XmlElement xe = (XmlElement)node;
                if (xe.GetAttribute("ConfigName") == this.DrpConfigList.SelectedItemValue.ToString())
                {
                    xmlNode.RemoveChild(xe);
                }
            }

            xmlDoc.Save(xmlPath);

            InitChbConfigList();
        }

        private bool CheckConfig()
        {
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + "WatchConfig.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            XmlNode NodeFormat = xmlDoc.SelectSingleNode("//FacWatchConfig[@Attribute='Fac']");

            XmlNodeList NodeList = NodeFormat.ChildNodes;

            foreach (XmlNode xd in NodeList)
            {
                XmlElement xe = (XmlElement)xd;
                if (xe.GetAttribute("ConfigName").ToString().ToUpper() == this.DrpConfigList.SelectedItemValue.ToString().ToUpper())
                {
                    XmlNodeList xmlNodeList = xd.ChildNodes;
                    foreach (XmlNode xn in xmlNodeList)
                    {
                        XmlElement xe1 = (XmlElement)xn;
                        if (xe1.Name == "BigLineList")
                        {
                            string bigLineList = xe1.InnerText.Trim().ToUpper();
                            string notExistLines = CheckBigLine(bigLineList);
                            if (notExistLines.Length > 0)
                            {
                                if (MessageBox.Show("配置项中产线:" + notExistLines + " 不存在!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                                {
                                    this.DrpConfigList.Focus();
                                    return false;
                                }
                            }
                        }
                    }
                }
            }


            return true;
        }

        private string CheckBigLine(string selectedBigLineList)
        {
            string returnValue = string.Empty;

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            object[] bigLineList = systemSettingFacade.GetAllBIGSSCODE();

            string[] textBigList = selectedBigLineList.ToUpper().Trim().Split(',');

            for (int i = 0; i < textBigList.Length; i++)
            {
                for (int j = 0; j < bigLineList.Length; j++)
                {
                    if (((Domain.BaseSetting.Parameter)bigLineList[j]).ParameterAlias.Trim().ToUpper() == textBigList[i].Trim().ToUpper())
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
    }

}