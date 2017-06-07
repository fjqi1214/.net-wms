using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;

using BenQGuru.eMES.Client.FAutoTestAction;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Client
{
	/// <summary>
	/// FAutoTestConfig 的摘要说明。
	/// </summary>
	public class FAutoTestConfig : System.Windows.Forms.Form
	{
        public event System.EventHandler CollectTick = null;

		public System.Windows.Forms.Button btnAutoTest;
        public System.Windows.Forms.Button btnHide;
        private Timer timerCollect;
        private IContainer components;

        private DataRow rowPerfProj = null;
        private DataRow rowPerfRes = null;
        private DataRowCollection rowPerfKeyPart = null;
        private Timer timerRefreshStatus;
        private DataRowCollection rowPerfSplitSn = null;

		public FAutoTestConfig()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.btnAutoTest = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.timerCollect = new System.Windows.Forms.Timer(this.components);
            this.timerRefreshStatus = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnAutoTest
            // 
            this.btnAutoTest.Location = new System.Drawing.Point(40, 16);
            this.btnAutoTest.Name = "btnAutoTest";
            this.btnAutoTest.Size = new System.Drawing.Size(75, 23);
            this.btnAutoTest.TabIndex = 0;
            this.btnAutoTest.Text = "Auto Test";
            this.btnAutoTest.Click += new System.EventHandler(this.btnAutoTest_Click);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(40, 48);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(75, 23);
            this.btnHide.TabIndex = 1;
            this.btnHide.Text = "Hide/Show";
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // timerCollect
            // 
            this.timerCollect.Interval = 1000;
            this.timerCollect.Tick += new System.EventHandler(this.timerCollect_Tick);
            // 
            // timerRefreshStatus
            // 
            this.timerRefreshStatus.Interval = 5000;
            this.timerRefreshStatus.Tick += new System.EventHandler(this.timerRefreshStatus_Tick);
            // 
            // FAutoTestConfig
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(144, 87);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnAutoTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FAutoTestConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.FAutoTestConfig_Activated);
            this.Load += new System.EventHandler(this.FAutoTestConfig_Load);
            this.ResumeLayout(false);

		}
		#endregion

        private IDomainDataProvider _domainDataProvider = Service.ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        FAutoTestActionBase actionBase = null;
        public void StartTestForm(Form form)
        {
            if (form is FCollectionGDNG)
            {
                actionBase = new FAutoTestActionTest((FCollectionGDNG)form, this);
            }
            else if (form is FBurnIn)
            {
                actionBase = new FAutoTestActionBurnIn((FBurnIn)form, this);
            }
            else if (form is FBurnOut)
            {
                actionBase = new FAutoTestActionBurnOut((FBurnOut)form, this);
            }
            else if (form is FCollectionIDMerge)
            {
                actionBase = new FAutoTestActionIDMerge((FCollectionIDMerge)form, this);
            }
            else if (form is FCollectionMetrial)
            {
                actionBase = new FAutoTestActionMaterial((FCollectionMetrial)form, this);
            }
            else if (form is FCollectionOQC)
            {
                actionBase = new FAutoTestActionOQC((FCollectionOQC)form, this);
            }
            else if (form is FGenLotIDMerge)
            {
                actionBase = new FAutoTestActionPack((FGenLotIDMerge)form, this);
            }
        }

		private void FAutoTestConfig_Load(object sender, System.EventArgs e)
		{
			//Service.ApplicationService.Current().MainWindows.Activated += new EventHandler(MianWindows_Activated);
			try
			{
                Service.ApplicationService.Current().MainWindows.FormBorderStyle = FormBorderStyle.Sizable;
				this.Text = Service.ApplicationService.Current().MainWindows.ActiveMdiChild.Text;
			}
			catch{}
		}

		private void FAutoTestConfig_Activated(object sender, System.EventArgs e)
		{
			try
			{
				this.Text = Service.ApplicationService.Current().MainWindows.ActiveMdiChild.Text;
			}
			catch{}
		}

		private void btnHide_Click(object sender, System.EventArgs e)
		{
			if (Service.ApplicationService.Current().MainWindows.WindowState == FormWindowState.Minimized)
			{
				Service.ApplicationService.Current().MainWindows.WindowState = FormWindowState.Normal;
				this.WindowState = FormWindowState.Normal;
			}
			else
			{
				Service.ApplicationService.Current().MainWindows.WindowState = FormWindowState.Minimized;
				this.WindowState = FormWindowState.Minimized;
			}
		}

		private void MianWindows_Activated(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
				return;
			this.WindowState = FormWindowState.Normal;
		}

		public string GetAppConfig(string key)
		{
			System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
			xmlDoc.Load(Application.ExecutablePath + ".perfconfig");
			System.Xml.XmlNode node = xmlDoc.SelectSingleNode("//add[@key='" + key + "']");
			if (node != null)
				return node.Attributes["value"].Value;
			else
				return string.Empty;
		}
		
        /// <summary>
        /// 记录采集时间
        /// </summary>
        /// <param name="dt"></param>
		public void LogCostTime(DateTime dt, string sn)
		{
            if (rowPerfProj == null)
                return;
			TimeSpan ts = DateTime.Now - dt;
			double iCost = ts.TotalMilliseconds;
            string strSql = "insert into TBLPERFPROJRUNLOG (guid,rescode,collecttime,sn,costtime) values ('" + rowPerfProj["guid"].ToString() + "','" + Service.ApplicationService.Current().ResourceCode + "'," + DateTime.Now.ToString("yyyyMMddhhmmss") + ",'" + sn + "'," + ts.TotalMilliseconds.ToString() + ")";
            this.DataProvider.CustomExecute(new SQLCondition(strSql));
		}
        /// <summary>
        /// 更新最后采集的序列号
        /// </summary>
        public void UpdateLastSn(string sn)
        {
            if (rowPerfProj == null)
                return;
            sn = GetLastSequence(sn, rowPerfProj["SNSeqLen"]);
            string strSql = "update tblPerfProj2Res set SNSeqLast=" + sn + " where guid='" + rowPerfProj["guid"].ToString() + "' and rescode='" + Service.ApplicationService.Current().ResourceCode + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(strSql));
        }
        /// <summary>
        /// 更新最后采集的KeyPart序列号
        /// </summary>
        public void UpdateLastKeyPart(int sequence, string keyPart)
        {
            if (rowPerfProj == null)
                return;
            if (rowPerfKeyPart == null || rowPerfKeyPart.Count < sequence)
                return;
            string strMItemCode = rowPerfKeyPart[sequence - 1]["MItemCode"].ToString();
            keyPart = GetLastSequence(keyPart, rowPerfKeyPart[sequence - 1]["KeyPartSeqLen"]);
            string strSql = "update tblPerfProjRes2KeyPart set KeyPartSeqLast=" + keyPart + " where guid='" + rowPerfProj["guid"].ToString() + "' and rescode='" + Service.ApplicationService.Current().ResourceCode + "' and mitemcode='" + strMItemCode + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(strSql));
        }
        /// <summary>
        /// 更新最后采集的转换后序列号
        /// </summary>
        public void UpdateLastSplitSn(int sequence, string sn)
        {
            if (rowPerfProj == null)
                return;
            if (rowPerfSplitSn == null || rowPerfSplitSn.Count < sequence)
                return;
            sn = GetLastSequence(sn, rowPerfSplitSn[sequence - 1]["SplitSnSeqLen"]);
            string strSql = "update tblPerfProjRes2SplitSn set SplitSnSeqLast=" + sn + " where guid='" + rowPerfProj["guid"].ToString() + "' and rescode='" + Service.ApplicationService.Current().ResourceCode + "' and splitsequence=" + (sequence - 1).ToString() + " ";
            this.DataProvider.CustomExecute(new SQLCondition(strSql));
        }
        /// <summary>
        /// 更新最后采集的Carton序列号
        /// </summary>
        public void UpdateLastCarton(string cartonNo)
        {
            if (rowPerfProj == null)
                return;
            cartonNo = GetLastSequence(cartonNo, rowPerfRes["CartonSeqLen"]);
            string strSql = "update tblPerfProj2Res set RunCartonSeqLast=" + cartonNo + " where guid='" + rowPerfProj["guid"].ToString() + "' and rescode='" + Service.ApplicationService.Current().ResourceCode + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(strSql));
        }
        private string GetLastSequence(string value, object sequenceLen)
        {
            int iSeqLen = value.Length;
            try
            {
                iSeqLen = Convert.ToInt32(sequenceLen);
            }
            catch
            { }

            string strRet = "";
            for (int i = value.Length - 1; i >= value.Length - iSeqLen; i--)
            {
                if (value[i] >= '0' && value[i] <= '9')
                {
                    strRet = value[i].ToString() + strRet;
                }
                else
                {
                    break;
                }
            }
            if (strRet == "")
                return "0";
            else
                return strRet;
        }

        private bool bIsAutoCollect = false;
        /// <summary>
        /// 加载自动连续采集的配置文件
        /// </summary>
        public void LoadAutoCollectConfig()
        {
            string strPath = Application.StartupPath;
            if (strPath.EndsWith("\\") == false)
                strPath += "\\";
            string strFile = strPath + "PerformanceAutoCollect.xml";
            if (System.IO.File.Exists(strFile) == false)
                return;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strFile);
            XmlNode nodeCurr = xmlDoc.DocumentElement.SelectSingleNode("//CurrentIndex");
            string strCurrentIndex = "";
            if (nodeCurr != null)
                strCurrentIndex = nodeCurr.FirstChild.Value;
            else
                throw new Exception("找不到结点：CurrentIndex");

            XmlNode nodeLoad = xmlDoc.DocumentElement.SelectSingleNode("//CollectList/Collect[@Index='" + strCurrentIndex + "']");
            if (nodeLoad == null)
            {
                throw new Exception("找不到配置结点：" + strCurrentIndex);
                return;
            }
            string strConfigFile = nodeLoad.SelectSingleNode("ConfigFileName").FirstChild.Value;
            System.IO.File.Copy(strPath + strConfigFile, strPath + "BenQGuru.eMES.Client.exe.perfconfig", true);

            // 登录
            FLogin flogin = new FLogin();
            flogin.MdiParent = Service.ApplicationService.Current().MainWindows;
            flogin.ucLEUserCode.Value = nodeLoad.SelectSingleNode("UserName").FirstChild.Value;
            flogin.ucLEPassword.Text = nodeLoad.SelectSingleNode("Password").FirstChild.Value;
            flogin.ucLEResourceCode.Value = nodeLoad.SelectSingleNode("Resource").FirstChild.Value;
            flogin.ucBtnLogin_Click(null, EventArgs.Empty);

            // 启动界面
            Form f = this.CreateFormByCollectType(nodeLoad.SelectSingleNode("CollectType").FirstChild.Value);
            if (f == null)
                return;
            this.ShowCollectForm(f);

            bIsAutoCollect = true;

            nodeCurr.FirstChild.Value = (int.Parse(strCurrentIndex) + 1).ToString();
            xmlDoc.Save(strFile);
        }

        private bool bAutoStartTimer = false;
        /// <summary>
        /// 检查是否自动启动测试界面
        /// </summary>
        public void CheckAutoLaunchTest()
        {
            if (ApplicationRun.appArguments == null && ApplicationRun.appArguments.Length < 5)
            {
                return;
            }
            string strProjGuid = ApplicationRun.appArguments[4];
            string strSql = "select * from tblPerfProj where guid='" + strProjGuid + "'";
            DataSet ds = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)Service.ApplicationService.Current().DataProvider).PersistBroker.Query(strSql);
            if (ds.Tables[0].Rows.Count == 0)
                return;
            rowPerfProj = ds.Tables[0].Rows[0];

            strSql = "select * from tblPerfProj2Res where guid='" + strProjGuid + "' and rescode='" + Service.ApplicationService.Current().ResourceCode + "'";
            ds = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)Service.ApplicationService.Current().DataProvider).PersistBroker.Query(strSql);
            if (ds.Tables[0].Rows.Count == 0)
                return;
            rowPerfRes = ds.Tables[0].Rows[0];

            strSql = "select * from tblPerfProjRes2KeyPart where guid='" + strProjGuid + "' and rescode='" + Service.ApplicationService.Current().ResourceCode + "'";
            ds = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)Service.ApplicationService.Current().DataProvider).PersistBroker.Query(strSql);
            rowPerfKeyPart = ds.Tables[0].Rows;

            strSql = "select * from tblPerfProjRes2SplitSn where guid='" + strProjGuid + "' and rescode='" + Service.ApplicationService.Current().ResourceCode + "'";
            ds = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)Service.ApplicationService.Current().DataProvider).PersistBroker.Query(strSql);
            rowPerfSplitSn = ds.Tables[0].Rows;

            strSql = "select * from tblitemroute2op where itemcode='" + rowPerfProj["itemcode"].ToString() + "' and routecode='" + rowPerfProj["routecode"].ToString() + "' and opcode='" + rowPerfRes["opcode"].ToString() + "'" + GlobalVariables.CurrentOrganizations.GetSQLCondition();
            object[] objsTmp = Service.ApplicationService.Current().DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.ItemRoute2OP), new BenQGuru.eMES.Common.Domain.SQLCondition(strSql));
            if (objsTmp == null || objsTmp.Length == 0)
                return;
            BenQGuru.eMES.Domain.MOModel.ItemRoute2OP op = (BenQGuru.eMES.Domain.MOModel.ItemRoute2OP)objsTmp[0];
            string strOpControl = op.OPControl;

            Form f = CreateFormByOPControl(strOpControl);
            if (f == null)
                return;
            ShowCollectForm(f);
        }
        private void ShowCollectForm(Form f)
        {
            f.MdiParent = Service.ApplicationService.Current().MainWindows;
            f.Show();

            if (System.Configuration.ConfigurationManager.AppSettings["EnabledAutoTest"] == "1" &&
                FAutoTestAction.FAutoTestActionBase.CheckIsTestForm(f) == true)
            {
                this.StartTestForm(f);
            }

            bAutoStartTimer = true;
            this.btnAutoTest_Click(btnAutoTest, EventArgs.Empty);
            bAutoStartTimer = false;

            if (rowPerfProj != null)
            {
                this.timerRefreshStatus.Enabled = true;
            }
        }
        private Form CreateFormByOPControl(string opControl)
        {
            Form f = null;
            if (opControl[(int)BenQGuru.eMES.BaseSetting.OperationList.ComponentLoading] == '1')
                f = new FCollectionMetrial();
            else if (opControl[(int)BenQGuru.eMES.BaseSetting.OperationList.BurnIn] == '1')
                f = new FBurnIn();
            else if (opControl[(int)BenQGuru.eMES.BaseSetting.OperationList.BurnOut] == '1')
                f = new FBurnOut();
            else if (opControl[(int)BenQGuru.eMES.BaseSetting.OperationList.Packing] == '1')
                f = new FGenLotIDMerge();
            else if (opControl[(int)BenQGuru.eMES.BaseSetting.OperationList.IDTranslation] == '1')
                f = new FCollectionIDMerge();
            else if (opControl[(int)BenQGuru.eMES.BaseSetting.OperationList.OQC] == '1')
                f = new FCollectionOQC();
            else if (opControl[(int)BenQGuru.eMES.BaseSetting.OperationList.Testing] == '1')
                f = new FCollectionGDNG();
            return f;
        }
        private Form CreateFormByCollectType(string collectType)
        {
            collectType = collectType.ToUpper();
            Form f = null;
            if (collectType == "ComponentLoading".ToUpper())
                f = new FCollectionMetrial();
            else if (collectType == "BurnIn".ToUpper())
                f = new FBurnIn();
            else if (collectType == "BurnOut".ToUpper())
                f = new FBurnOut();
            else if (collectType == "Packing".ToUpper())
                f = new FGenLotIDMerge();
            else if (collectType == "IDTranslation".ToUpper())
                f = new FCollectionIDMerge();
            else if (collectType == "OQC".ToUpper())
                f = new FCollectionOQC();
            else if (collectType == "Testing".ToUpper())
                f = new FCollectionGDNG();
            return f;
        }

        private void btnAutoTest_Click(object sender, EventArgs e)
        {
            if (timerCollect.Enabled == true)
            {
                timerCollect.Enabled = false;
                if (bAutoStartTimer == true ||
                    MessageBox.Show("Pause testing?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    timerCollect.Enabled = false;
                    btnAutoTest.Text = "Start";
                }
                else
                {
                    timerCollect.Enabled = true;
                }
            }
            else
            {
                if (bAutoStartTimer == true ||
                    MessageBox.Show("Start testing?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    timerCollect.Interval = int.Parse(GetAppConfig("AutoTestInterval"));
                    timerCollect.Enabled = true;
                    btnAutoTest.Text = "Pause";
                }
            }
        }

        bool bInTimerRunning = false;
        private void timerCollect_Tick(object sender, EventArgs e)
        {
            if (bInTimerRunning == true)
                return;
            bInTimerRunning = true;
            try
            {
                if (this.CollectTick != null)
                {
                    this.CollectTick(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
            }
            finally
            {
                bInTimerRunning = false;
            }
        }

        public void RCardTestEnd()
        {
            timerCollect.Enabled = false;
            btnAutoTest.Text = "Start";

            // 如果是自动连续测试，则关闭本窗口，再做下一步
            if (bIsAutoCollect == true)
            {
                foreach (Form f in Service.ApplicationService.Current().MainWindows.MdiChildren)
                    f.Close();
                Application.DoEvents();
                FAutoTestConfig fcfg = new FAutoTestConfig();
                fcfg.LoadAutoCollectConfig();
                Application.DoEvents();
                this.Close();
            }
        }

        private void timerRefreshStatus_Tick(object sender, EventArgs e)
        {
            string strSql = "select * from tblperfproj2res where guid='" + rowPerfRes["guid"].ToString() + "' and rescode='" + rowPerfRes["rescode"].ToString() + "' ";
            DataSet ds = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)Service.ApplicationService.Current().DataProvider).PersistBroker.Query(strSql);
            if (ds.Tables[0].Rows.Count == 0)
                return;
            rowPerfRes = ds.Tables[0].Rows[0];

            if (rowPerfRes["status"].ToString() == "pause")
            {
                int iPauseSecond = 0;
                string strPauseFrom = "";
                try
                {
                    iPauseSecond = Convert.ToInt32(rowPerfRes["PauseSecond"]);
                    strPauseFrom = rowPerfRes["PauseFrom"].ToString();
                }
                catch
                {
                }
                if (iPauseSecond > 0 && strPauseFrom.Length == 14)
                {
                    DateTime dtFrom = new DateTime(
                                        int.Parse(strPauseFrom.Substring(0, 4)),
                                        int.Parse(strPauseFrom.Substring(4, 2)),
                                        int.Parse(strPauseFrom.Substring(6, 2)),
                                        int.Parse(strPauseFrom.Substring(8, 2)),
                                        int.Parse(strPauseFrom.Substring(10, 2)),
                                        int.Parse(strPauseFrom.Substring(12, 2))
                                        );
                    dtFrom = dtFrom.AddSeconds(iPauseSecond);
                    if (dtFrom <= DateTime.Now)
                    {
                        string strRun = "running";
                        strSql = "update tblPerfProj2Res set status='" + strRun + "',PauseSecond=0,PauseFrom=0 where guid='" + rowPerfRes["guid"].ToString() + "' and rescode='" + rowPerfRes["rescode"].ToString() + "' ";
                        Service.ApplicationService.Current().DataProvider.CustomExecute(new BenQGuru.eMES.Common.Domain.SQLCondition(strSql));
                        rowPerfRes["status"] = strRun;
                    }
                }
            }
            string strResStatus = rowPerfRes["status"].ToString();
            if ((strResStatus == "running" && this.timerCollect.Enabled == false) ||
                (strResStatus == "pause" && this.timerCollect.Enabled == true))
            {
                bAutoStartTimer = true;
                this.btnAutoTest_Click(null, null);
                bAutoStartTimer = false;
            }
        }

	}
}
