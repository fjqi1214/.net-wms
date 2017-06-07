using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BenQGuru.eMES.BaseDataModel
{
    public partial class FSetting : Form
    {
        public FSetting()
        {
            InitializeComponent();
        }

        private void FSetting_Load(object sender, EventArgs e)
        {
            string strRepeat = Properties.Settings.Default.DataRepeat;
            if (strRepeat == ImportDataEngine.DataRepeatDeal.Update)
                this.rdoRepeatUpdate.Checked = true;
            else if (strRepeat == ImportDataEngine.DataRepeatDeal.Ignore)
                this.rdoRepeatIgnore.Checked = true;
            else if (strRepeat == ImportDataEngine.DataRepeatDeal.Cancel)
                this.rdoRepeatCancel.Checked = true;
            else
                this.rdoRepeatUpdate.Checked = true;

            string strErr = Properties.Settings.Default.DataError;
            if (strErr == ImportDataEngine.OnErrorDeal.Cancel)
                this.rdoErrorCancel.Checked = true;
            else
                this.rdoErrorIgnore.Checked = true;

            string strMap = Properties.Settings.Default.DataMappingType;
            if (strMap == ImportDataEngine.DataMappingType.ByIndex)
                this.rdoMappingByIndex.Checked = true;
            else
                this.rdoMappingByName.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strRepeat = "";
            if (this.rdoRepeatUpdate.Checked == true)
                strRepeat = ImportDataEngine.DataRepeatDeal.Update;
            else if (this.rdoRepeatIgnore.Checked == true)
                strRepeat = ImportDataEngine.DataRepeatDeal.Ignore;
            else if (this.rdoRepeatCancel.Checked == true)
                strRepeat = ImportDataEngine.DataRepeatDeal.Cancel;
            Properties.Settings.Default.DataRepeat = strRepeat;

            string strErr = "";
            if (this.rdoErrorIgnore.Checked == true)
                strErr = ImportDataEngine.OnErrorDeal.Ignore;
            else if (this.rdoErrorCancel.Checked == true)
                strErr = ImportDataEngine.OnErrorDeal.Cancel;
            Properties.Settings.Default.DataError = strErr;

            string strMap = "";
            if (this.rdoMappingByName.Checked == true)
                strMap = ImportDataEngine.DataMappingType.ByName;
            else
                strMap = ImportDataEngine.DataMappingType.ByIndex;
            Properties.Settings.Default.DataMappingType = strMap;

            XmlDocument doc = new XmlDocument();
            string strConfigFile = Application.StartupPath;
            if (strConfigFile.EndsWith("\\") == false)
                strConfigFile += "\\";
            strConfigFile += "BenQGuru.eMES.BaseDataModel.exe.config";
            doc.Load(strConfigFile);
            XmlNode node = doc.SelectSingleNode("//configuration/userSettings/BenQGuru.eMES.BaseDataModel.Properties.Settings/setting[@name='DataRepeat']");
            if (node != null && node.SelectSingleNode("value") != null)
                node.SelectSingleNode("value").FirstChild.Value = strRepeat;
            node = doc.SelectSingleNode("//configuration/userSettings/BenQGuru.eMES.BaseDataModel.Properties.Settings/setting[@name='DataError']");
            if (node != null && node.SelectSingleNode("value") != null)
                node.SelectSingleNode("value").FirstChild.Value = strErr;
            node = doc.SelectSingleNode("//configuration/userSettings/BenQGuru.eMES.BaseDataModel.Properties.Settings/setting[@name='DataMappingType']");
            if (node != null && node.SelectSingleNode("value") != null)
                node.SelectSingleNode("value").FirstChild.Value = strMap;
            doc.Save(strConfigFile);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}