using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Data.OleDb;

namespace BenQGuru.eMES.CSSetupConfiger
{
    public partial class FMain : Form
    {
        private string CurrentPath
        {
            get
            {
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
        }
        private string m_DomainXmlFileName = "Domain.xml";
        private int m_ModifyIndex = -1;
        private string m_ConnectionString32 = "Provider=MSDAORA.1;Password={0};OLE DB Services=-1;User ID={1};Data Source={2};Persist Security Info=True";
        private string m_ConnectionString64 = "Provider=OraOLEDB.Oracle.1;Password={0};OLE DB Services=-1;User ID={1};Data Source={2};Persist Security Info=True";
 
        public FMain()
        {
            InitializeComponent();
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            try
            {
                this.BindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BindGrid()
        {
            string filename = Path.Combine(this.CurrentPath, this.m_DomainXmlFileName);
            XmlDocument document = new XmlDocument();
            document.Load(filename);
            XmlNodeList nodes = document.SelectNodes("/DomainSetting/PersistBrokers/PersistBroker");

            DataTable dtSource = new DataTable();
            dtSource.Columns.Add("colDbName");
            dtSource.Columns.Add("colDataSource");
            dtSource.Columns.Add("colUserName");
            dtSource.Columns.Add("colPassword");
            dtSource.Columns.Add("colProvider");            
            dtSource.Columns.Add("colChar");
            dtSource.Columns.Add("colDefault");

            foreach (XmlNode item in nodes)
            {
                DataRow dr = dtSource.NewRow();
                dr["colDbName"] = item.Attributes["Name"].Value;
                string connectString = BenQGuru.Palau.Common.CommonFunction.DESDeCrypt(item.Attributes["ConnectString"].Value);
                string[] items = connectString.Split(';');
                foreach (string info in items)
                {
                    if (info.StartsWith("Data Source"))
                    {
                        dr["colDataSource"] = info.Replace("Data Source", "").Replace("=", "").Trim();
                    }
                    if (info.StartsWith("User ID"))
                    {
                        dr["colUserName"] = info.Replace("User ID", "").Replace("=", "").Trim();
                    }
                    if (info.StartsWith("Password"))
                    {
                        dr["colPassword"] = info.Replace("Password", "").Replace("=", "").Trim();
                    }
                    if (info.StartsWith("Provider"))
                    {
                        dr["colProvider"] = info.Replace("Provider", "").Replace("=", "").Trim();
                    }
                }
                dr["colChar"] = item.Attributes["NLS"].Value;
                dr["colDefault"] = item.Attributes["Default"].Value;

                dtSource.Rows.Add(dr);
            }
            this.gridDb.DataSource = dtSource;
        }

        private void gridDb_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewColumn column = this.gridDb.Columns[e.ColumnIndex];
                    if (column is DataGridViewButtonColumn)
                    {
                        DataGridViewButtonCell cell = this.gridDb.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCell;
                        if (cell.ColumnIndex == 0) //edit
                        {
                            //MessageBox.Show("edit");
                            this.txtDbName.Text = this.gridDb.Rows[e.RowIndex].Cells[2].Value.ToString();
                            this.txtDataSource.Text = this.gridDb.Rows[e.RowIndex].Cells[3].Value.ToString();
                            this.txtUserName.Text = this.gridDb.Rows[e.RowIndex].Cells[4].Value.ToString();
                            this.txtPassword.Text = this.gridDb.Rows[e.RowIndex].Cells[5].Value.ToString();
                            this.txtChar.Text = this.gridDb.Rows[e.RowIndex].Cells[7].Value.ToString();
                            this.chkDefault.Checked = bool.Parse(this.gridDb.Rows[e.RowIndex].Cells[8].Value.ToString());
                            this.m_ModifyIndex = e.RowIndex;
                            this.txtDbName.Enabled = false;
                            this.comboBoxDriver.SelectedItem = this.gridDb.Rows[e.RowIndex].Cells[6].Value.ToString();
                        }
                        else //delete
                        {
                            if (MessageBox.Show("确定删除此笔记录?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //delete by name
                                string filename = Path.Combine(this.CurrentPath, this.m_DomainXmlFileName);
                                XmlDocument document = new XmlDocument();
                                document.Load(filename);
                                XmlNode parent = document.SelectSingleNode("/DomainSetting/PersistBrokers");

                                XmlNode node = document.SelectSingleNode("/DomainSetting/PersistBrokers/PersistBroker[@Name='" + this.gridDb.Rows[e.RowIndex].Cells[2].Value.ToString() + "']");
                                parent.RemoveChild(node);
                                document.Save(filename);
                                btnReset_Click(null, null);
                                this.BindGrid();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private XmlNode GetNewNode(XmlDocument document)
        {
            XmlNode child = document.CreateNode(XmlNodeType.Element, "PersistBroker", "");
            XmlAttribute attr = document.CreateAttribute("Text");
            attr.Value = this.txtDbName.Text.Trim();
            child.Attributes.Append(attr);

            attr = document.CreateAttribute("Name");
            attr.Value = this.txtDbName.Text.Trim();
            child.Attributes.Append(attr);

            attr = document.CreateAttribute("Default");
            attr.Value = this.chkDefault.Checked.ToString();
            child.Attributes.Append(attr);

            attr = document.CreateAttribute("Type");
            attr.Value = "OLEDBPersistBroker";
            child.Attributes.Append(attr);

            attr = document.CreateAttribute("NLS");
            attr.Value = this.txtChar.Text.Trim();
            child.Attributes.Append(attr);

            attr = document.CreateAttribute("ConnectString");
            if (this.comboBoxDriver.SelectedItem.ToString() == "OraOLEDB.Oracle.1")
            {
                attr.Value = BenQGuru.Palau.Common.CommonFunction.DESEnCrypt(string.Format(this.m_ConnectionString64, this.txtPassword.Text, this.txtUserName.Text, this.txtDataSource.Text));
            }
            else
            {
                attr.Value = BenQGuru.Palau.Common.CommonFunction.DESEnCrypt(string.Format(this.m_ConnectionString32, this.txtPassword.Text, this.txtUserName.Text, this.txtDataSource.Text));
            }
            
            child.Attributes.Append(attr);

            return child;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtDbName.Text.Trim()))
                {
                    MessageBox.Show("请输入数据库名称!");
                    this.txtDbName.Focus();
                    return;
                }
                if (!TestConnection())
                {
                    MessageBox.Show("数据库测试失败!请重新输入!");
                }
                else
                {
                    string filename = Path.Combine(this.CurrentPath, this.m_DomainXmlFileName);
                    XmlDocument document = new XmlDocument();
                    document.Load(filename);
                    XmlNode parent = document.SelectSingleNode("/DomainSetting/PersistBrokers");
                    if (this.chkDefault.Checked) //默认
                    {
                        XmlNodeList nodes = document.SelectNodes("/DomainSetting/PersistBrokers/PersistBroker");
                        foreach (XmlNode item in nodes)
                        {
                            item.Attributes["Default"].Value = "False";
                        }
                    }
                    if (m_ModifyIndex != -1) //modify
                    {
                        XmlNode oldNode = document.SelectSingleNode("/DomainSetting/PersistBrokers/PersistBroker[@Name='" + this.txtDbName.Text.Trim() + "']");
                        parent.ReplaceChild(GetNewNode(document), oldNode);
                    }
                    else //new
                    {
                        XmlNode child = GetNewNode(document);
                        parent.AppendChild(child);
                    }
                    document.Save(filename);
                    MessageBox.Show("配置数据库成功!");
                    this.BindGrid();
                }

                btnReset_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.m_ModifyIndex != -1)
                {
                    if (MessageBox.Show("修改尚未保存,确定退出?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        this.Close();
                    else return;
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool TestConnection()
        {
            if (string.IsNullOrEmpty(this.txtDataSource.Text.Trim()))
            {
                throw new Exception("请输入服务名!");
            }
            if (string.IsNullOrEmpty(this.txtUserName.Text.Trim()))
            {
                throw new Exception("请输入用户名!");
            }
            if (string.IsNullOrEmpty(this.txtPassword.Text.Trim()))
            {
                throw new Exception("请输入密码!");
            }
            
            IDbConnection connection = null;
            if (this.comboBoxDriver.SelectedItem.ToString()=="OraOLEDB.Oracle.1")
            {
                connection = new OleDbConnection(string.Format(this.m_ConnectionString64, this.txtPassword.Text, this.txtUserName.Text, this.txtDataSource.Text));
            }
            else
            {
                connection = new OleDbConnection(string.Format(this.m_ConnectionString32, this.txtPassword.Text, this.txtUserName.Text, this.txtDataSource.Text));
            }

            
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.TestConnection())
                {
                    MessageBox.Show("测试成功!");
                }
                else
                {
                    MessageBox.Show("测试失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_ModifyIndex = -1;
                this.txtDataSource.Clear();
                this.txtChar.Clear();
                this.txtDbName.Clear();
                this.txtPassword.Clear();
                this.txtUserName.Clear();
                this.chkDefault.Checked = false;
                this.txtDbName.Enabled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
