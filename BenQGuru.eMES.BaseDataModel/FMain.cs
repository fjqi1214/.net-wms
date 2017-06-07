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
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.MutiLanguage;

namespace BenQGuru.eMES.BaseDataModel
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
        }

        private void InitTreeView()
        {
            this.trvMain.Nodes.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(ApplicationService.ConfigFile);
            XmlNode nodeTreeRoot = doc.SelectSingleNode("//NodeList");
            if (nodeTreeRoot == null)
                return;
            XmlNodeList nodeTreeList = nodeTreeRoot.SelectNodes("Node");
            for (int i = 0; i < nodeTreeList.Count; i++)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = nodeTreeList[i].Attributes["Text"].FirstChild.Value;
                treeNode.Tag = nodeTreeList[i];
                this.trvMain.Nodes.Add(treeNode);

                AddNodeToTree(treeNode, nodeTreeList[i]);
            }
        }
        private void AddNodeToTree(TreeNode parentNode, XmlNode nodeList)
        {
            XmlNodeList nodes = nodeList.SelectNodes("Node");
            for (int i = 0; i < nodes.Count; i++)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Text = nodes[i].Attributes["Text"].FirstChild.Value;
                treeNode.Tag = nodes[i];
                parentNode.Nodes.Add(treeNode);

                AddNodeToTree(treeNode, nodes[i]);
            }
        }

        private List<ConfigObject> configObjList = null;
        private MatchType configMatchType = null;
        private void FMain_Load(object sender, EventArgs e)
        {
            ApplicationService.LoginUserCode = "ImportTool";

            ConfigObject.LoadConfig(ApplicationService.ConfigFile, out configObjList, out configMatchType);

            InitTreeView();
        }

        private void trvMain_DoubleClick(object sender, EventArgs e)
        {
            TreeNode selectedNode = trvMain.SelectedNode;
            if (selectedNode == null)
                return;
            XmlNode nodeCfg = (XmlNode)selectedNode.Tag;
            if (nodeCfg == null)
                return;
            if (nodeCfg.Attributes["ImportItemName"] == null)
                return;

            string strImportName = nodeCfg.Attributes["ImportItemName"].FirstChild.Value;
            if (strImportName == "")
                return;
            ConfigObject cfgObj = null;
            for (int i = 0; i < this.configObjList.Count; i++)
            {
                if (this.configObjList[i].Name == strImportName)
                {
                    cfgObj = this.configObjList[i];
                    break;
                }
            }
            if (cfgObj == null)
                return;

            this.Cursor = Cursors.WaitCursor;
            for (int i = 0; i < this.panelMain.Controls.Count; i++)
            {
                if (this.panelMain.Controls[i] is FImpFormBase)
                {
                    ((FImpFormBase)this.panelMain.Controls[i]).Close();
                }
            }
            
            string strFile = ApplicationService.TemplateFolder + cfgObj.TemplateFileName;

            //2#条码的导入做特殊处理
            if (nodeCfg.Attributes["ImportItemName"].Value == "No2Seq")
            {
                string path = nodeCfg.Attributes["FromFile"].Value;
                if (path != null && path != string.Empty)
                {
                    try
                    {
                        System.IO.File.Copy(path, strFile, true);
                    }
                    catch { }
                }
            }

            FImpFormBase fImp = null;
            if (this.panelMain.Controls.Count > 0 && this.panelMain.Controls[0] is FImpFormBase)
            {
                fImp = (FImpFormBase)this.panelMain.Controls[0];
                //fImp.CloseExcelWorkbook();

                this.panelMain.Controls.Clear();
                Application.DoEvents();
                fImp = CreateImportForm(cfgObj);
                fImp.MainForm = this;
                fImp.TopLevel = false;
                fImp.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(fImp);
            }
            else
            {
                this.panelMain.Controls.Clear();
                fImp = CreateImportForm(cfgObj);
                fImp.MainForm = this;
                fImp.TopLevel = false;
                fImp.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(fImp);
            }
            fImp.CurrentImportName = cfgObj.Name;
            fImp.configMatchType = this.configMatchType;
            fImp.configObjList = this.configObjList;

            fImp.ShowExcel(strFile);
            fImp.Visible = true;

            this.Cursor = Cursors.Default;

            this.Activate();
        }
        private FImpFormBase CreateImportForm(ConfigObject cfgObj)
        {
            FImpFormBase fImp = null;
            switch (cfgObj.Name)
            {
                case "Operation":
                    fImp = new FImpOP();
                    break;
                case "Item2Route":
                    fImp = new FImpItemRoute();
                    break;
                default:
                    fImp = new FImpFormBase();
                    break;
            }
            return fImp;
        }

        private void FMain_Activated(object sender, EventArgs e)
        {
            
            if (this.panelMain.Controls.Count > 0 && this.panelMain.Controls[0] is FImpFormBase)
            {
                this.panelMain.Focus();
                FImpFormBase f = (FImpFormBase)this.panelMain.Controls[0];
                f.Activate();
                f.Focus();
                f.SetActive();
            }
            
        }

        private void menuFileExit_Click(object sender, EventArgs e)
        {
            if (this.panelMain.Controls.Count > 0 && this.panelMain.Controls[0] is FImpFormBase)
            {
                ((FImpFormBase)this.panelMain.Controls[0]).Close();
            }
            this.Close();
        }

        private void menuViewTree_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel1Collapsed = !this.splitContainer1.Panel1Collapsed;
            this.menuViewTree.Checked = !this.splitContainer1.Panel1Collapsed;
        }

        private void menuViewSetting_Click(object sender, EventArgs e)
        {
            FSetting f = new FSetting();
            f.ShowDialog();
        }

    }
}