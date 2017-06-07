using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.SAPDataTransferInterface;

namespace BenQGuru.eMES.SAPDataTransferConsole
{
    public partial class FMain : Form
    {
        private List<ServiceEntity> services = new List<ServiceEntity>();
        private ServiceEntity m_CurrentServiceEntity = null;
        private Assembly m_CurrentServiceAssembly = null;
        private Type m_CurrentServiceType = null;

        public FMain()
        {
            InitializeComponent();
        }

        private void FMain_Load(object sender, EventArgs e)
        {
            this.toolStripButtonRun.Enabled = false;
            this.propertyGridArguments.PropertySort = PropertySort.Categorized;

            ClearAllOldLogFiles();
        }

        private void listBoxServiceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxServiceList.SelectedIndex >= 0)
            {
                if (this.m_CurrentServiceEntity == listBoxServiceList.SelectedItem)
                {
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    this.serviceInfoPanelMain.SetServiceInfo(listBoxServiceList.SelectedItem as ServiceEntity);
                    this.LoadAssemblyInfo(listBoxServiceList.SelectedItem as ServiceEntity);
                    this.toolStripButtonRun.Enabled = true;
                    m_CurrentServiceEntity = listBoxServiceList.SelectedItem as ServiceEntity;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void LoadAssemblyInfo(ServiceEntity se)
        {
            if (se == null)
            {
                return;
            }

            m_CurrentServiceAssembly = Assembly.LoadFrom(se.AssemblyPath);
            m_CurrentServiceType = m_CurrentServiceAssembly.GetType(se.Type);
            using (ICommand command = (ICommand)Activator.CreateInstance(m_CurrentServiceType))
            {
                this.propertyGridArguments.SelectedObject = command.GetArguments();
            }
        }

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButtonFind_Click(object sender, EventArgs e)
        {
            this.listBoxServiceList.Items.Clear();
            this.serviceInfoPanelMain.ClearServiceInfo();
            this.propertyGridArguments.SelectedObject = null;

            services = ConfigurationManager.GetSection("ServiceEntities") as List<ServiceEntity>;
            foreach (ServiceEntity se in services)
            {
                this.listBoxServiceList.Items.Add(se);
            }
            this.listBoxServiceList.DisplayMember = "Description";

            this.listBoxServiceList.ClearSelected();
            this.toolStripButtonRun.Enabled = false;
        }

        private void toolStripButtonRun_Click(object sender, EventArgs e)
        {
            if (this.listBoxServiceList.Items.Count == 0)
            {
                return;
            }

            if (this.listBoxServiceList.SelectedIndex < 0)
            {
                return;
            }

            if (this.m_CurrentServiceAssembly == null)
            {
                return;
            }

            if (this.m_CurrentServiceType == null)
            {
                return;
            }

            using (ICommand command = (ICommand)Activator.CreateInstance(m_CurrentServiceType))
            {
                command.SetArguments(this.propertyGridArguments.SelectedObject);
                string message = "";
                if (!command.ArgumentValid(ref message))
                {
                    MessageBox.Show(this, message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                ServiceResult sr = command.Run(RunMethod.Manually);

                string transCode= string.Empty;
                if (sr.TransactionCode.Trim().Length > 0)
                {
                    transCode = ", TransactionCode=" + sr.TransactionCode;
                }
                
                if (sr.Result)
                {
                    MessageBox.Show(this, "执行成功" + transCode, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "执行失败" + transCode + Environment.NewLine + "错误信息: " + sr.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.propertyGridArguments.SelectedObject = command.NewTransactionCode();
                this.Cursor = Cursors.Default;
            }
        }

        private void toolStripButtonCleanOldLog_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ClearAllOldLogFiles();
            this.Cursor = Cursors.Arrow;
        }

        private void ClearAllOldLogFiles()
        {            
            ClearOldFiles(ConfigurationManager.AppSettings.Get("XMLPath"), ConfigurationManager.AppSettings.Get("LogKeepDays"));
            ClearOldFiles(ConfigurationManager.AppSettings.Get("WindowsServiceLogPath"), ConfigurationManager.AppSettings.Get("LogKeepDays"));
            ClearOldFiles(ConfigurationManager.AppSettings.Get("WebServiceLogPath"), ConfigurationManager.AppSettings.Get("LogKeepDays"));

            MessageBox.Show(this, "清除旧日志成功");
        }

        private void ClearOldFiles(string logPath, string keepDays)
        {
            int days = 0;
            int.TryParse(keepDays, out days);
            if (days <= 0)
            {
                return;
            }

            try
            {
                DateTime now = DateTime.Now;

                DirectoryInfo dir = new DirectoryInfo(logPath);
                foreach (FileInfo file in dir.GetFiles())
                {
                    if (file.LastWriteTime < now.AddDays(-days))
                    {
                        file.Delete();
                    }
                }
            }
            catch
            {
            }
        }
    }
}