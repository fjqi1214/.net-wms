using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using BenQGuru.eMES.DBTransfer;
using BenQGuru.eMES.Domain.BaseSetting;
using System.Threading;

namespace BenQGuru.eMES.DBTransferTool
{
    public partial class FormMain : Form
    {
        private DataSet m_ResultList = null;
        private DataTable m_JobList = null;
        private DataTable m_TableList = null;
        private int m_InputDate = 0;
        private string[] m_SelectedJobNames = null;
        private int m_SelectedJobNamesCount = 0;
        
        private IDomainDataProvider _domainDataProvider = null;
        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        private DBTransferFacade m_TransferFacade = null;
        private DBTransferFacade TransferFacade
        {
            get
            {
                if (m_TransferFacade == null)
                {
                    m_TransferFacade = new DBTransferFacade(DataProvider);
                }

                return m_TransferFacade;
            }
        }

        public FormMain()
        {
            //CheckForIllegalCrossThreadCalls = false;

            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            this.ultraGridJobList.UpdateMode = UpdateMode.OnCellChange;
            this.ultraGridJobList.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            this.ultraGridJobList.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridJobList.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridJobList.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridJobList.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridJobList.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridJobList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridJobList.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridJobList.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridJobList.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.InitializeJobListGrid();

            this.comboBoxJobType.Items.Clear();
            this.comboBoxJobType.Items.Add("Clear");
            this.comboBoxJobType.Items.Add("Copy");
            this.comboBoxJobType.SelectedIndex = 0;

            this.textBoxDBLink.Text = "his2real";
            this.textBoxDate.Text = FormatHelper.TODateInt(System.DateTime.Now.Date).ToString();

            this.maskedTextBoxSecond.Text = "5";
            this.groupBoxRefresh.Enabled = false;
            this.progressBarMain.Visible = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.m_SelectedJobNamesCount != 0)
            {
                MessageBox.Show("还有任务在运行中，不能关闭！");
                e.Cancel = true;
            }
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            bool forClear = false;
            if (this.comboBoxJobType.SelectedItem.ToString() == "Clear")
            {
                forClear = true;
            }

            if (this.textBoxDBLink.Text.Trim() == "")
            {
                MessageBox.Show("请输入DBLink！");
                this.textBoxDBLink.Focus();
                return;
            }

            if (forClear && this.textBoxDate.Text.Trim() == "")
            {
                MessageBox.Show("请输入参考日期！");
                this.textBoxDate.Focus();
                return;
            }

            // 只打开一次数据库链接，且一直保持该链接，直到程序退出或终止
            ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

            try
            {
                this.Cursor = Cursors.WaitCursor;

                m_InputDate = int.Parse(this.textBoxDate.Text);
                DateTime dateInput = FormatHelper.ToDateTime(m_InputDate, 1);
                int actualDate = 0;
                string dbLink = this.textBoxDBLink.Text.Trim();

                this.ClearJobList();

                object[] jobs = TransferFacade.GetTransferJobs(this.comboBoxJobType.SelectedItem.ToString());

                if (jobs != null)
                {
                    DataRow rowJob;
                    List<DataRow> rows;

                    foreach (TransferJobExtend job in jobs)
                    {
                        rowJob = this.m_ResultList.Tables["JobList"].NewRow();
                        rowJob["Checked"] = "false";
                        rowJob["Serial"] = job.Serial;
                        rowJob["TransactionSetSerial"] = job.TransactionSetSerial;
                        rowJob["Name"] = job.Name;
                        rowJob["Description"] = job.Description;
                        rowJob["MasterTable"] = job.MasterTable;
                        rowJob["Condition"] = job.Condition;
                        rowJob["KeepDays"] = job.KeepDays;
                        if (forClear)
                        {
                            actualDate = FormatHelper.TODateInt(dateInput.AddDays(-1 * job.KeepDays));
                        }
                        else
                        {
                            actualDate = m_InputDate;
                        }
                        rowJob["ActualDate"] = actualDate;
                        rowJob["RecordCount"] = this.GetRecordCount(job.MasterTable, job.Condition, actualDate, dbLink);
                        rowJob["LastRunDate"] = job.LastRunDate == 0 ? "0" : FormatHelper.TODateTimeString(job.LastRunDate, job.LastRunTime);
                        rowJob["LastSuccessDate"] = job.LastSuccessDate == 0 ? "0" : FormatHelper.TODateTimeString(job.LastSuccessDate, job.LastSuccessTime);
                        rowJob["RunStatus"] = "未运行";
                        this.m_ResultList.Tables["JobList"].Rows.Add(rowJob);

                        // Load Tables
                        rows = new List<DataRow>();
                        this.GetTables(rows, job.TransactionSetSerial, job.Serial);

                        foreach (DataRow rowTable in rows)
                        {
                            this.m_ResultList.Tables["TableList"].Rows.Add(rowTable);
                        }
                    }

                    this.m_ResultList.Tables["JobList"].AcceptChanges();
                    this.m_ResultList.Tables["TableList"].AcceptChanges();
                    this.m_ResultList.AcceptChanges();

                    this.ultraGridJobList.DataSource = this.m_ResultList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private int GetRecordCount(string masterTable, string condition,int date, string dbLink)
        {
            if (string.IsNullOrEmpty(masterTable.Trim()))
            {
                return 0;
            }
            string sql = "SELECT COUNT(*) FROM " + masterTable + "@" + dbLink + " WHERE 1=1 " + condition.Replace("$Date", date.ToString());
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private void GetTables(List<DataRow> rows, int transactionSetSerial, int transferJobSerial)
        {
            object[] details = TransferFacade.GetTransactionSetDetails(transactionSetSerial);
            DataRow rowTable;

            foreach (TransactionSetDetail detail in details)
            {
                rowTable = this.m_ResultList.Tables["TableList"].NewRow();
                rowTable["JobSerial"] = transferJobSerial;
                rowTable["Serial"] = detail.Serial;
                rowTable["TransactionSetSerial"] = detail.TransactionSetSerial;
                rowTable["Sequence"] = detail.Sequence;
                rowTable["TableName"] = detail.TableName;
                rowTable["Condition"] = detail.Condition;
                rowTable["ForeignKey"] = detail.ForeignKeyFields;
                
                rows.Add(rowTable);
            }
            
            object[] childTransactionSets = TransferFacade.GetChildTransactionSets(transactionSetSerial);
            if (childTransactionSets == null || childTransactionSets.Length == 0)
            {
                return;
            }
            else
            {
                foreach (TransactionSet transactionSet in childTransactionSets)
                {
                    this.GetTables(rows, transactionSet.Serial, transferJobSerial);
                }
            }
        }

        private void ClearJobList()
        {
            if (this.m_ResultList == null)
            {
                return;
            }
            this.m_ResultList.Tables["TableList"].Rows.Clear();
            this.m_ResultList.Tables["JobList"].Rows.Clear();
            this.m_ResultList.Tables["TableList"].AcceptChanges();
            this.m_ResultList.Tables["JobList"].AcceptChanges();            
            this.m_ResultList.AcceptChanges();
        }

        private void InitializeJobListGrid()
        {
            this.m_ResultList = new DataSet();
            this.m_JobList = new DataTable("JobList");
            this.m_TableList = new DataTable("TableList");

            this.m_JobList.Columns.Add("Checked", typeof(string));
            this.m_JobList.Columns.Add("Serial", typeof(int));
            this.m_JobList.Columns.Add("TransactionSetSerial", typeof(int));
            this.m_JobList.Columns.Add("Name", typeof(string));
            this.m_JobList.Columns.Add("Description", typeof(string));
            this.m_JobList.Columns.Add("MasterTable", typeof(string));
            this.m_JobList.Columns.Add("Condition", typeof(string));
            this.m_JobList.Columns.Add("KeepDays", typeof(int));
            this.m_JobList.Columns.Add("ActualDate", typeof(int));
            this.m_JobList.Columns.Add("LastRunDate", typeof(string));
            this.m_JobList.Columns.Add("LastSuccessDate", typeof(string));
            this.m_JobList.Columns.Add("RecordCount", typeof(int));
            this.m_JobList.Columns.Add("RunStatus", typeof(string));
            
            this.m_TableList.Columns.Add("JobSerial", typeof(int));
            this.m_TableList.Columns.Add("Serial", typeof(int));
            this.m_TableList.Columns.Add("TransactionSetSerial", typeof(int));
            this.m_TableList.Columns.Add("Sequence", typeof(int));
            this.m_TableList.Columns.Add("TableName", typeof(string));
            this.m_TableList.Columns.Add("ForeignKey", typeof(string));
            this.m_TableList.Columns.Add("Condition", typeof(string));

            this.m_ResultList.Tables.Add(this.m_JobList);
            this.m_ResultList.Tables.Add(this.m_TableList);

            this.m_ResultList.Relations.Add(new DataRelation("JobAndTable",
                                                this.m_ResultList.Tables["JobList"].Columns["Serial"],
                                                this.m_ResultList.Tables["TableList"].Columns["JobSerial"]));
            this.m_ResultList.AcceptChanges();
            this.ultraGridJobList.DataSource = this.m_ResultList;
        }

        private void ultraGridJobList_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 隐藏栏位
            e.Layout.Bands[0].Columns["Serial"].Hidden = true;
            e.Layout.Bands[0].Columns["TransactionSetSerial"].Hidden = true;
            e.Layout.Bands[0].Columns["Condition"].Hidden = true;

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["Name"].Header.Caption = "任务名称";
            e.Layout.Bands[0].Columns["Description"].Header.Caption = "任务描述";
            e.Layout.Bands[0].Columns["MasterTable"].Header.Caption = "主表";
            e.Layout.Bands[0].Columns["KeepDays"].Header.Caption = "保留天数";
            e.Layout.Bands[0].Columns["ActualDate"].Header.Caption = "数据保留日期";
            e.Layout.Bands[0].Columns["LastRunDate"].Header.Caption = "上次执行时间";
            e.Layout.Bands[0].Columns["LastSuccessDate"].Header.Caption = "上次执行成功时间";
            e.Layout.Bands[0].Columns["RecordCount"].Header.Caption = "处理数据笔数";
            e.Layout.Bands[0].Columns["RunStatus"].Header.Caption = "执行状态";

            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["Name"].Width = 100;
            e.Layout.Bands[0].Columns["Description"].Width = 150;
            e.Layout.Bands[0].Columns["MasterTable"].Width = 80;
            e.Layout.Bands[0].Columns["KeepDays"].Width = 60;
            e.Layout.Bands[0].Columns["ActualDate"].Width = 80;
            e.Layout.Bands[0].Columns["LastRunDate"].Width = 120;
            e.Layout.Bands[0].Columns["LastSuccessDate"].Width = 120;
            e.Layout.Bands[0].Columns["RecordCount"].Width = 80;
            e.Layout.Bands[0].Columns["RunStatus"].Width = 80;

            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["Name"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["Description"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MasterTable"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["KeepDays"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ActualDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["LastRunDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["LastSuccessDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["RecordCount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["RunStatus"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            // 允许冻结，且Checked栏位始终处于冻结状态，不可更改
            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["Name"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["Name"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            e.Layout.Bands[0].Columns["Name"].SortIndicator = SortIndicator.Ascending;

            // Table List   
            // JobSerial | Serial | Sequence | TableName
            e.Layout.Bands[1].Columns["JobSerial"].Hidden = true;
            e.Layout.Bands[1].Columns["Serial"].Hidden = true;
            e.Layout.Bands[1].Columns["TransactionSetSerial"].Hidden = true;

            e.Layout.Bands[1].Columns["Sequence"].Header.Caption = "序号";
            e.Layout.Bands[1].Columns["TableName"].Header.Caption = "表名";
            e.Layout.Bands[1].Columns["ForeignKey"].Header.Caption = "外键栏位";
            e.Layout.Bands[1].Columns["Condition"].Header.Caption = "其他条件";
            e.Layout.Bands[1].Columns["Sequence"].Width = 80;
            e.Layout.Bands[1].Columns["TableName"].Width = 250;
            e.Layout.Bands[1].Columns["ForeignKey"].Width = 100;
            e.Layout.Bands[1].Columns["Condition"].Width = 300;
            e.Layout.Bands[1].Columns["Sequence"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["TableName"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["ForeignKey"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["Condition"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["TransactionSetSerial"].SortIndicator = SortIndicator.Ascending;
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            if (this.ultraGridJobList.Rows.Count == 0)
            {
                return;
            }
            
            bool hasChecked = false;
            string selectedJobName = "";
            for (int i = 0; i < this.ultraGridJobList.Rows.Count; i++)
            {
                if (this.ultraGridJobList.Rows[i].Cells["Checked"].Value.ToString() == "True")
                {
                    hasChecked = true;
                    selectedJobName += this.ultraGridJobList.Rows[i].Cells["Name"].Value.ToString() + ",";
                }
            }

            if (hasChecked == false)
            {
                MessageBox.Show("请先选择一笔或几笔任务！");
                return;
            }           

            selectedJobName = selectedJobName.Substring(0, selectedJobName.Length - 1);
            m_SelectedJobNames = selectedJobName.Split(',');
            this.m_SelectedJobNamesCount = m_SelectedJobNames.Length;

            this.groupBoxMain.Enabled = false;
            this.checkBoxAutoRefresh.Checked = false;
            this.checkBoxAutoRefresh_CheckedChanged(this.checkBoxAutoRefresh, null);
            this.groupBoxRefresh.Enabled = true;

            this.buttonExecute.Enabled = false;
            this.ultraGridJobList.DisplayLayout.Bands[0].Columns["Checked"].CellActivation = Activation.Disabled;
            this.progressBarMain.Maximum = m_SelectedJobNamesCount;
            this.progressBarMain.Minimum = 0;
            this.progressBarMain.Value = 0;
            this.progressBarMain.Step = 1;
            this.progressBarMain.Visible = true;
            
            foreach (string jobName in m_SelectedJobNames)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(this.RunSP), jobName);
            }
        }

        private void RunSP(object jobName)
        {
            try
            {
                DataRow rowOfJob = this.FindRow(jobName.ToString());
                rowOfJob["RunStatus"] = "运行中";

                ProcedureParameter pp1 = new ProcedureParameter("v_jobList", typeof(string), 40, DirectionType.Input, jobName.ToString());
                ProcedureParameter pp2 = new ProcedureParameter("v_date", typeof(int), 8, DirectionType.Input, m_InputDate);

                ProcedureCondition pc = new ProcedureCondition("PKG_DBTRANSFER.Transfer", new ProcedureParameter[] { pp1, pp2 });

                // 多线程Run每一个任务，每一个任务自己创建自己的数据库链接
                IDomainDataProvider newProvider = DomainDataProviderManager.DomainDataProvider();
                newProvider.CustomProcedure(ref pc);

                Thread.Sleep(10000);

                rowOfJob["RunStatus"] = "完成";

                TransferJob job = TransferFacade.GetTransferJob(Convert.ToInt32(rowOfJob["Serial"])) as TransferJob;
                rowOfJob["LastRunDate"] = job.LastRunDate == 0 ? "0" : FormatHelper.TODateTimeString(job.LastRunDate, job.LastRunTime);
                rowOfJob["LastSuccessDate"] = job.LastSuccessDate == 0 ? "0" : FormatHelper.TODateTimeString(job.LastSuccessDate, job.LastSuccessTime);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Invoke(new Action<int>(this.SetFlag), 1);
            }
        }

        private void SetFlag(int count)
        {
            this.m_SelectedJobNamesCount = this.m_SelectedJobNamesCount - count;
            this.progressBarMain.Value = this.progressBarMain.Value + count;

            if (this.m_SelectedJobNamesCount == 0)
            {
                this.groupBoxMain.Enabled = true;

                this.timerRefresh.Enabled = false;
                this.groupBoxRefresh.Enabled = false;
                this.buttonExecute.Enabled = true;

                this.RefreshGridData();

                this.ultraGridJobList.DisplayLayout.Bands[0].Columns["Checked"].CellActivation = Activation.AllowEdit;
                this.progressBarMain.Visible = false;
            }
        }

        private DataRow FindRow(string jobName)
        {
            foreach (DataRow row in this.m_ResultList.Tables["JobList"].Rows)
            {
                if (string.Compare(row["Name"].ToString(), jobName, true) == 0)
                {
                    return row;
                }
            }
            return null;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if (this.m_SelectedJobNamesCount == 0)
            {
                this.timerRefresh.Enabled = false;
                return;
            }

            if (this.maskedTextBoxSecond.Text.Trim() == "")
            {
                this.maskedTextBoxSecond.Text = "5";
            }
            this.timerRefresh.Interval = int.Parse(this.maskedTextBoxSecond.Text) * 1000;

            this.RefreshGridData();
        }

        private void buttonManualRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshGridData();
        }

        private void RefreshGridData()
        {
            for (int i = 0; i < this.m_SelectedJobNames.Length; i++)
            {
                DataRow row = this.FindRow(this.m_SelectedJobNames[i]);
                row["RecordCount"] = this.GetRecordCount(row["MasterTable"].ToString(), row["Condition"].ToString(), Convert.ToInt32(row["ActualDate"].ToString()), this.textBoxDBLink.Text.Trim());
            }
        }

        private void checkBoxAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxAutoRefresh.Checked == true)
            {
                this.maskedTextBoxSecond.Enabled = true;
                this.timerRefresh.Interval = int.Parse(this.maskedTextBoxSecond.Text) * 1000;
                this.timerRefresh.Enabled = true;
                this.timerRefresh_Tick(this.timerRefresh, null);

                this.buttonManualRefresh.Enabled = false;
            }
            else
            {
                this.maskedTextBoxSecond.Enabled = false;
                this.timerRefresh.Enabled = false;

                this.buttonManualRefresh.Enabled = true;
            }
        }

        private void comboBoxJobType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearJobList();
            if (this.comboBoxJobType.SelectedItem.ToString() == "Clear")
            {
                this.ultraGridJobList.DisplayLayout.Bands[0].Columns["ActualDate"].Hidden = false;
            }
            else
            {
                this.ultraGridJobList.DisplayLayout.Bands[0].Columns["ActualDate"].Hidden = true;
            }
        }
    }
}