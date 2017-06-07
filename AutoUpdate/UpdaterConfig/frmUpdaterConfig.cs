using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using Tools;

namespace UpdaterConfig
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class frmUpdaterConfig : System.Windows.Forms.Form
	{
		private string savedFilePath = "Save.Dat";
		private string zipFileName = "Updater.zip";
		private bool allowClose = false;

		private IDomainDataProvider _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
		public IDomainDataProvider DataProvider
		{
			get
			{
				return _domainDataProvider;
			}
		}

		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.IO.FileSystemWatcher fileSystemWatcher1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnAllowUpdate;
		private System.Windows.Forms.Button btnForbidenUpdate;
		private System.Windows.Forms.Label lblCurrentVersion;
		private System.Windows.Forms.Label lblNewVersion;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox chkAutoUpdater;
		private System.Windows.Forms.Label lblPhysicalPath;
		private System.Windows.Forms.Button btnParsePath;
		private System.Windows.Forms.TextBox txtPhysicalPath;
		private System.Windows.Forms.TextBox txtTargetDIR;
		private System.Windows.Forms.TextBox txtSourceDIR;
		private System.Windows.Forms.Button btnSourceDIR;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.Button btnCreateVerNo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radDay;
		private System.Windows.Forms.RadioButton radWeek;
		private System.Windows.Forms.ComboBox cmbWeek;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.DateTimePicker dateTimePicker2;
		private System.Windows.Forms.RadioButton radMonth;
		private System.Windows.Forms.ComboBox cmdDay;
		private System.Windows.Forms.DateTimePicker dateTimePicker3;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

		public frmUpdaterConfig()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			dateTimePicker1.Value = DateTime.Now;
			dateTimePicker2.Value = DateTime.Now;
			dateTimePicker3.Value = DateTime.Now;
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
				if (components != null) 
				{
					components.Dispose();
				}
			}
			
			
			base.Dispose( disposing );
			notifyIcon1.Dispose();
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmUpdaterConfig));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.txtTargetDIR = new System.Windows.Forms.TextBox();
			this.btnParsePath = new System.Windows.Forms.Button();
			this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
			this.btnSourceDIR = new System.Windows.Forms.Button();
			this.txtSourceDIR = new System.Windows.Forms.TextBox();
			this.btnAllowUpdate = new System.Windows.Forms.Button();
			this.btnForbidenUpdate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblCurrentVersion = new System.Windows.Forms.Label();
			this.lblNewVersion = new System.Windows.Forms.Label();
			this.chkAutoUpdater = new System.Windows.Forms.CheckBox();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this.label4 = new System.Windows.Forms.Label();
			this.lblPhysicalPath = new System.Windows.Forms.Label();
			this.txtPhysicalPath = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.btnCreateVerNo = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
			this.cmdDay = new System.Windows.Forms.ComboBox();
			this.radMonth = new System.Windows.Forms.RadioButton();
			this.label7 = new System.Windows.Forms.Label();
			this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.cmbWeek = new System.Windows.Forms.ComboBox();
			this.radWeek = new System.Windows.Forms.RadioButton();
			this.radDay = new System.Windows.Forms.RadioButton();
			this.btnSave = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "Guru eMes自动升级服务器";
			this.notifyIcon1.Visible = true;
			this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "退出";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// txtTargetDIR
			// 
			this.txtTargetDIR.Location = new System.Drawing.Point(16, 32);
			this.txtTargetDIR.Name = "txtTargetDIR";
			this.txtTargetDIR.Size = new System.Drawing.Size(288, 21);
			this.txtTargetDIR.TabIndex = 0;
			this.txtTargetDIR.Text = "";
			// 
			// btnParsePath
			// 
			this.btnParsePath.Location = new System.Drawing.Point(312, 32);
			this.btnParsePath.Name = "btnParsePath";
			this.btnParsePath.Size = new System.Drawing.Size(104, 23);
			this.btnParsePath.TabIndex = 0;
			this.btnParsePath.Text = "解析物理路径";
			this.btnParsePath.Click += new System.EventHandler(this.btnParsePath_Click);
			// 
			// fileSystemWatcher1
			// 
			this.fileSystemWatcher1.EnableRaisingEvents = true;
			this.fileSystemWatcher1.NotifyFilter = ((System.IO.NotifyFilters)((System.IO.NotifyFilters.LastWrite | System.IO.NotifyFilters.LastAccess)));
			this.fileSystemWatcher1.SynchronizingObject = this;
			this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
			this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
			this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
			// 
			// btnSourceDIR
			// 
			this.btnSourceDIR.Location = new System.Drawing.Point(488, 120);
			this.btnSourceDIR.Name = "btnSourceDIR";
			this.btnSourceDIR.Size = new System.Drawing.Size(32, 23);
			this.btnSourceDIR.TabIndex = 0;
			this.btnSourceDIR.Text = "...";
			this.btnSourceDIR.Click += new System.EventHandler(this.btnSourceDIR_Click);
			// 
			// txtSourceDIR
			// 
			this.txtSourceDIR.Location = new System.Drawing.Point(16, 120);
			this.txtSourceDIR.Name = "txtSourceDIR";
			this.txtSourceDIR.Size = new System.Drawing.Size(472, 21);
			this.txtSourceDIR.TabIndex = 0;
			this.txtSourceDIR.Text = "";
			// 
			// btnAllowUpdate
			// 
			this.btnAllowUpdate.Location = new System.Drawing.Point(416, 360);
			this.btnAllowUpdate.Name = "btnAllowUpdate";
			this.btnAllowUpdate.Size = new System.Drawing.Size(112, 23);
			this.btnAllowUpdate.TabIndex = 0;
			this.btnAllowUpdate.Text = "立即更新版本";
			this.btnAllowUpdate.Click += new System.EventHandler(this.btnAllowUpdate_Click);
			// 
			// btnForbidenUpdate
			// 
			this.btnForbidenUpdate.Location = new System.Drawing.Point(424, 32);
			this.btnForbidenUpdate.Name = "btnForbidenUpdate";
			this.btnForbidenUpdate.Size = new System.Drawing.Size(112, 23);
			this.btnForbidenUpdate.TabIndex = 0;
			this.btnForbidenUpdate.Text = "停止手动更新";
			this.btnForbidenUpdate.Visible = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 23);
			this.label1.TabIndex = 6;
			this.label1.Text = "发布更新的Web目录";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 96);
			this.label2.Name = "label2";
			this.label2.TabIndex = 7;
			this.label2.Text = "新版本目录";
			// 
			// lblCurrentVersion
			// 
			this.lblCurrentVersion.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblCurrentVersion.ForeColor = System.Drawing.Color.Blue;
			this.lblCurrentVersion.Location = new System.Drawing.Point(80, 152);
			this.lblCurrentVersion.Name = "lblCurrentVersion";
			this.lblCurrentVersion.Size = new System.Drawing.Size(400, 23);
			this.lblCurrentVersion.TabIndex = 9;
			// 
			// lblNewVersion
			// 
			this.lblNewVersion.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblNewVersion.ForeColor = System.Drawing.Color.Blue;
			this.lblNewVersion.Location = new System.Drawing.Point(64, 184);
			this.lblNewVersion.Name = "lblNewVersion";
			this.lblNewVersion.Size = new System.Drawing.Size(424, 23);
			this.lblNewVersion.TabIndex = 10;
			// 
			// chkAutoUpdater
			// 
			this.chkAutoUpdater.Location = new System.Drawing.Point(0, 0);
			this.chkAutoUpdater.Name = "chkAutoUpdater";
			this.chkAutoUpdater.Size = new System.Drawing.Size(88, 24);
			this.chkAutoUpdater.TabIndex = 0;
			this.chkAutoUpdater.Text = "自动更新";
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker1.Location = new System.Drawing.Point(144, 24);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.ShowUpDown = true;
			this.dateTimePicker1.Size = new System.Drawing.Size(80, 21);
			this.dateTimePicker1.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(304, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 23);
			this.label4.TabIndex = 14;
			this.label4.Text = "时";
			// 
			// lblPhysicalPath
			// 
			this.lblPhysicalPath.Location = new System.Drawing.Point(16, 64);
			this.lblPhysicalPath.Name = "lblPhysicalPath";
			this.lblPhysicalPath.Size = new System.Drawing.Size(56, 23);
			this.lblPhysicalPath.TabIndex = 15;
			this.lblPhysicalPath.Text = "物理路径";
			// 
			// txtPhysicalPath
			// 
			this.txtPhysicalPath.Enabled = false;
			this.txtPhysicalPath.Location = new System.Drawing.Point(80, 64);
			this.txtPhysicalPath.Name = "txtPhysicalPath";
			this.txtPhysicalPath.Size = new System.Drawing.Size(440, 21);
			this.txtPhysicalPath.TabIndex = 0;
			this.txtPhysicalPath.Text = "";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(60, 17);
			this.label5.TabIndex = 16;
			this.label5.Text = "目前版本:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 184);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 17);
			this.label6.TabIndex = 17;
			this.label6.Text = "新版本:";
			// 
			// btnCreateVerNo
			// 
			this.btnCreateVerNo.Location = new System.Drawing.Point(304, 360);
			this.btnCreateVerNo.Name = "btnCreateVerNo";
			this.btnCreateVerNo.Size = new System.Drawing.Size(112, 23);
			this.btnCreateVerNo.TabIndex = 18;
			this.btnCreateVerNo.Text = "创建版本号";
			this.btnCreateVerNo.Click += new System.EventHandler(this.btnCreateVerNo_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dateTimePicker3);
			this.groupBox1.Controls.Add(this.cmdDay);
			this.groupBox1.Controls.Add(this.radMonth);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.dateTimePicker2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.cmbWeek);
			this.groupBox1.Controls.Add(this.radWeek);
			this.groupBox1.Controls.Add(this.radDay);
			this.groupBox1.Controls.Add(this.chkAutoUpdater);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.dateTimePicker1);
			this.groupBox1.Controls.Add(this.btnSave);
			this.groupBox1.Location = new System.Drawing.Point(16, 216);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(504, 120);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// dateTimePicker3
			// 
			this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker3.Location = new System.Drawing.Point(224, 88);
			this.dateTimePicker3.Name = "dateTimePicker3";
			this.dateTimePicker3.ShowUpDown = true;
			this.dateTimePicker3.Size = new System.Drawing.Size(80, 21);
			this.dateTimePicker3.TabIndex = 23;
			// 
			// cmdDay
			// 
			this.cmdDay.Items.AddRange(new object[] {
														"1",
														"2",
														"3",
														"4",
														"5",
														"6",
														"7",
														"8",
														"9",
														"10",
														"11",
														"12",
														"13",
														"14",
														"15",
														"16",
														"17",
														"18",
														"19",
														"20",
														"21",
														"22",
														"23",
														"24",
														"25",
														"26",
														"27",
														"28",
														"29",
														"30",
														"31"});
			this.cmdDay.Location = new System.Drawing.Point(144, 88);
			this.cmdDay.Name = "cmdDay";
			this.cmdDay.Size = new System.Drawing.Size(80, 20);
			this.cmdDay.TabIndex = 22;
			// 
			// radMonth
			// 
			this.radMonth.Location = new System.Drawing.Point(80, 88);
			this.radMonth.Name = "radMonth";
			this.radMonth.Size = new System.Drawing.Size(56, 24);
			this.radMonth.TabIndex = 21;
			this.radMonth.Tag = "UpdateType";
			this.radMonth.Text = "每月";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(304, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(24, 23);
			this.label7.TabIndex = 20;
			this.label7.Text = "时";
			// 
			// dateTimePicker2
			// 
			this.dateTimePicker2.CustomFormat = "";
			this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dateTimePicker2.Location = new System.Drawing.Point(224, 56);
			this.dateTimePicker2.Name = "dateTimePicker2";
			this.dateTimePicker2.ShowUpDown = true;
			this.dateTimePicker2.Size = new System.Drawing.Size(80, 21);
			this.dateTimePicker2.TabIndex = 19;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(224, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(24, 23);
			this.label3.TabIndex = 18;
			this.label3.Text = "时";
			// 
			// cmbWeek
			// 
			this.cmbWeek.Items.AddRange(new object[] {
														 "星期一",
														 "星期二",
														 "星期三",
														 "星期四",
														 "星期五",
														 "星期六",
														 "星期日"});
			this.cmbWeek.Location = new System.Drawing.Point(144, 56);
			this.cmbWeek.Name = "cmbWeek";
			this.cmbWeek.Size = new System.Drawing.Size(80, 20);
			this.cmbWeek.TabIndex = 17;
			// 
			// radWeek
			// 
			this.radWeek.Checked = true;
			this.radWeek.Location = new System.Drawing.Point(80, 56);
			this.radWeek.Name = "radWeek";
			this.radWeek.Size = new System.Drawing.Size(56, 24);
			this.radWeek.TabIndex = 16;
			this.radWeek.TabStop = true;
			this.radWeek.Tag = "UpdateType";
			this.radWeek.Text = "每周";
			// 
			// radDay
			// 
			this.radDay.Location = new System.Drawing.Point(80, 24);
			this.radDay.Name = "radDay";
			this.radDay.Size = new System.Drawing.Size(56, 24);
			this.radDay.TabIndex = 15;
			this.radDay.Tag = "UpdateType";
			this.radDay.Text = "每天";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(424, 24);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(64, 23);
			this.btnSave.TabIndex = 20;
			this.btnSave.Text = "保存";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Elapsed);
			// 
			// frmUpdaterConfig
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(570, 399);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCreateVerNo);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtPhysicalPath);
			this.Controls.Add(this.txtSourceDIR);
			this.Controls.Add(this.txtTargetDIR);
			this.Controls.Add(this.lblPhysicalPath);
			this.Controls.Add(this.lblNewVersion);
			this.Controls.Add(this.lblCurrentVersion);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnForbidenUpdate);
			this.Controls.Add(this.btnAllowUpdate);
			this.Controls.Add(this.btnSourceDIR);
			this.Controls.Add(this.btnParsePath);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmUpdaterConfig";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Guru eMes自动升级服务器端配置";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmUpdaterConfig_Closing);
			this.Load += new System.EventHandler(this.frmUpdaterConfig_Load);
			this.Activated += new System.EventHandler(this.frmUpdaterConfig_Activated);
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.ThreadException +=new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);


			Application.Run(new frmUpdaterConfig());
		}

		private void btnSourceDIR_Click(object sender, System.EventArgs e)
		{
			if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				this.txtSourceDIR.Text = folderBrowserDialog1.SelectedPath;
				SaveInfo();
			}
		}
		//刷新版本信息
		private void RefreshVersion()
		{
			Updater updater = getServerVersion();
			if(updater !=  null)
			{
				lblCurrentVersion.Text = updater.CSVersion;
			}
			
			lblNewVersion.Text = getLocalVersion();

			if(lblCurrentVersion.Text.Trim() == lblNewVersion.Text.Trim())
			{
				lblNewVersion.Text = "当前已经是最新版本，无需更新";
			}
		}
		//获取服务器的版本
		private Updater getServerVersion()
		{
			(_domainDataProvider as SQLDomainDataProvider).PersistBroker.CloseConnection();

			object[] objs= _domainDataProvider.CustomQuery(typeof(Updater),
				new SQLCondition("select CSVERSION,LOCATION,LOGINUSER,LOGINPASSWORD,ISAVIABLE from TBLCSUPDATER where  ISAVIABLE = 1"));
			if (objs == null)
				return null;	
			return (Updater)objs[0];
		}
		//更新服务器的版本
		private void UpdateServerVersion()
		{
			if(txtSourceDIR.Text.Trim() == String.Empty
				|| txtTargetDIR.Text.Trim() == String.Empty)
			{
				return;
			}
			object obj = getServerVersion();
			if(lblNewVersion.Text.IndexOf("无需更新") < 0)
			{
				string sql = String.Empty;
				if(obj == null)
				{
					sql = "insert into TBLCSUPDATER (CSVERSION,LOCATION,ISAVIABLE) values ('"
						+ lblNewVersion.Text.Trim() + "','"
						+ txtTargetDIR.Text.Trim() + "',1)";
				}
				else
				{
					sql = "update TBLCSUPDATER set CSVERSION = '" + lblNewVersion.Text.Trim() 
						+ "',LOCATION ='" + txtTargetDIR.Text.Trim() + "',ISAVIABLE=1 where ISAVIABLE = 1";
				}
		
				_domainDataProvider.BeginTransaction();
				try
				{
					_domainDataProvider.CustomExecute(
						new SQLCondition(sql));

					_domainDataProvider.CommitTransaction();

					FileLog.FileLogOut(lblNewVersion.Text + "\tOK");
				
				}
				catch(Exception ex)
				{
					Log.Error(ex.Message);
					_domainDataProvider.RollbackTransaction();
					throw ex;
				}
				finally
				{
					(_domainDataProvider as SQLDomainDataProvider).PersistBroker.CloseConnection();
				}
			}
		}

		//获取本地版本
		private string getLocalVersion()
		{
			string fileName = txtSourceDIR.Text + "\\" + "Version.txt";
			string strVersion = string.Empty;

			if(System.IO.File.Exists(fileName))
			{
				FileStream sr = File.OpenRead(fileName);

				byte[] content = new byte[512];

				sr.Read(content,0,content.Length);

				sr.Close();

				strVersion = System.Text.Encoding.Default.GetString(content);
			}

			return strVersion;
		}

		private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
		{
			RefreshVersion();
		}

		private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
		{
			RefreshVersion();
		}

		private void fileSystemWatcher1_Deleted(object sender, System.IO.FileSystemEventArgs e)
		{
			RefreshVersion();
		}

		private void btnRefresh_Click(object sender, System.EventArgs e)
		{
			RefreshVersion();
		}

		private void btnParsePath_Click(object sender, System.EventArgs e)
		{
			string phyPath = String.Empty;
			if(txtTargetDIR.Text.Trim() != String.Empty)
			{
				try
				{
					if(txtTargetDIR.Text.Trim().ToUpper().IndexOf("LOCALHOST") > -1
						|| txtTargetDIR.Text.Trim().ToUpper().IndexOf("127.0.0.1") > -1)
					{
						MessageBox.Show("必须输入网络机器名或者IP地址");
					}
					else
					{
						string iisVDName = txtTargetDIR.Text.Trim().Substring(txtTargetDIR.Text.Trim().LastIndexOf("/") + 1
							,txtTargetDIR.Text.Trim().Length - txtTargetDIR.Text.Trim().LastIndexOf("/") -1);
						IISManagement im = new IISManagement();

						foreach(IISWebServer iws in  im.WebServers)
						{
							foreach(IISWebVirtualDir iwvd in  iws.WebVirtualDirs)
							{
								if(iwvd.Name.ToUpper() == iisVDName.ToUpper())
								{
									phyPath = iwvd.Path;
									break;
								}
							}
						}
					}
				}
				catch(Exception ex)
				{
					Log.Error(ex.Message);
				}
				
			}
			txtPhysicalPath.Text  = phyPath;
			SaveInfo();
		}

		private void frmUpdaterConfig_Activated(object sender, System.EventArgs e)
		{
			//GetSavedInfo();
			RefreshVersion();

		}
		//获取保存的信息
		private void GetSavedInfo()
		{
//			
//			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = 
//				new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
//
//			FileStream fs = null;
			try
			{
//				fs = File.OpenRead(savedFilePath);
//				bf.FilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
//
//				UpdaerConfigInfo uci = 
//					bf.Deserialize(fs) as UpdaerConfigInfo;
				UpdaerConfigInfo uci = new  UpdaerConfigInfo();
				uci.LoadFromFile(savedFilePath);
				if(uci != null)
				{
					txtSourceDIR.Text = uci.SourcePath;
					txtTargetDIR.Text = uci.TargetVIRPath;
					txtPhysicalPath.Text = uci.TargetPath;

					if(uci.NeedAutoUpdate == true)
					{
						this.chkAutoUpdater.Checked = true;
						if(uci.Day != 0)
						{
							radMonth.Checked = true;
						}
						if(uci.Week != 0)
						{
							radWeek.Checked = true;
						}
						if(radDay.Checked)
						{
							this.dateTimePicker1.Value = uci.UpdateTime;
						}
						if(radWeek.Checked)
						{
							this.dateTimePicker2.Value = uci.UpdateTime;
							cmbWeek.SelectedIndex = (int)uci.Week;
							
						}
						if(radMonth.Checked)
						{
							this.dateTimePicker3.Value = uci.UpdateTime;
							cmdDay.SelectedValue = uci.Day;
						}
					}
				}
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
			}
			finally
			{
//				if(fs != null)
//				{
//					fs.Close();
//				}
			}
		}

		private void btnAllowUpdate_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			if(txtTargetDIR.Text.Trim().ToUpper().IndexOf("LOCALHOST") > -1
				|| txtTargetDIR.Text.Trim().ToUpper().IndexOf("127.0.0.1") > -1
				|| txtPhysicalPath.Text.Trim() == String.Empty)
			{
				Log.Info("必须输入网络机器名或者IP地址");
				this.Cursor = Cursors.Default;
				return;
			}
			Application.DoEvents();
			SaveInfo();

			///TODO:开始升级
			///
			try
			{
				//解析Web服务器的物理地址
				btnParsePath_Click(sender,e);

				ZipAndCopyFile();

				UpdateServerVersion();

				this.Cursor = Cursors.Default;

				//FileLog.FileLogOut(lblNewVersion.Text + "\t" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "\tOK");


				Log.Info(lblNewVersion.Text + "，更新成功");
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);

//				if(e != null)
//				{
//					MessageBox.Show(ex.Message);
//				}
			}
			finally
			{
				this.Cursor = Cursors.Default;
			}
			
		}

		//压缩文件
		private void ZipAndCopyFile()
		{
			if(txtSourceDIR.Text.Trim() != String.Empty)
			{
				ZipFile.ZipFiles(txtSourceDIR.Text.Trim(),zipFileName,9);
				
				File.Copy(zipFileName
					,txtPhysicalPath.Text.Trim() + "\\" + zipFileName,true);

			}
		}

		//保存的信息
		private void SaveInfo()
		{
//			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = 
//				new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
//
//			bf.FilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

//			FileStream fs = null;
			try
			{
				//fs = File.OpenWrite(savedFilePath);
				
				UpdaerConfigInfo uci = new UpdaerConfigInfo();
				
				uci.SourcePath = txtSourceDIR.Text.Trim();
				uci.TargetVIRPath = txtTargetDIR.Text.Trim();
				uci.TargetPath = txtPhysicalPath.Text.Trim();

				if(this.chkAutoUpdater.Checked == true)
				{
					uci.NeedAutoUpdate = true;
					//Laws Lu,2006/08/15 修改
					if(radDay.Checked)
					{
						uci.UpdateTime = this.dateTimePicker1.Value;
					}
					if(radWeek.Checked)
					{
						uci.Week = (DayOfWeek)cmbWeek.SelectedIndex;
						uci.UpdateTime = this.dateTimePicker2.Value;
					}
					if(radMonth.Checked)
					{
						uci.Day = Convert.ToInt32(cmdDay.SelectedValue);
						uci.UpdateTime = this.dateTimePicker3.Value;
					}
				}

				uci.SaveToFile(savedFilePath);
//				bf.Serialize(fs,uci);
			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
			}
			finally
			{
//				if(fs != null)
//				{
//					fs.Close();
//				}
			}
		}

		private void frmUpdaterConfig_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		
			if(allowClose != true)
			{
				e.Cancel = true;

				this.WindowState = FormWindowState.Minimized;
				this.Hide();
			}	
			else
			{
					SaveInfo();
			}
		}

		private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.WindowState == FormWindowState.Minimized)
			{
				this.Show();
				this.WindowState = FormWindowState.Normal;
			}
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			this.notifyIcon1.Visible = false;
			allowClose = true;
			this.Close();
			Application.ExitThread();
		}

		private void btnCreateVerNo_Click(object sender, System.EventArgs e)
		{
			string filePath = txtSourceDIR.Text.Trim() + "\\" + "Version.txt";
			if(File.Exists(filePath))
			{
				FileStream fs = File.OpenWrite(filePath);

				try
				{
					byte[] by = System.Text.Encoding.Default.GetBytes(DateTime.Now.ToString("yyyyMMddhhmmss"));
					fs.Write(by,0,by.Length);
				}
				catch(Exception ex)
				{
					throw ex;
				}
				finally
				{
					fs.Flush();

					fs.Close();
				}


			}
		}

		private void frmUpdaterConfig_Load(object sender, System.EventArgs e)
		{
			cmbWeek.SelectedIndex = 0; 
			cmdDay.SelectedIndex = 0;

			GetSavedInfo();

		}

		private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			Log.Error(e.Exception.Message);
		}

		#region 窗体事件
		private void radWeek_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radWeek.Checked == true)
			{
				SaveInfo();
			}
		}

		private void radMonth_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radMonth.Checked == true)
			{
				SaveInfo();
			}
		}

		private void radDay_CheckedChanged(object sender, System.EventArgs e)
		{
			if(radDay.Checked == true)
			{
				SaveInfo();
			}
		}

		private void cmbWeek_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(radWeek.Checked == true)
			{
				SaveInfo();
			}
		}

		private void cmdDay_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(radMonth.Checked == true)
			{
				SaveInfo();
			}
		}

		private void dateTimePicker2_ValueChanged(object sender, System.EventArgs e)
		{
			if(radWeek.Checked == true)
			{
				SaveInfo();
			}
		}

		private void dateTimePicker3_ValueChanged(object sender, System.EventArgs e)
		{
			if(radMonth.Checked == true)
			{
				SaveInfo();
			}
		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SaveInfo();
		}

		private void timer1_Elapsed(object sender, System.EventArgs e)
		{
			if(chkAutoUpdater.Checked == true)
			{
				
				int nowHour = DateTime.Now.Hour;
				int nowSecond = DateTime.Now.Second;
				int nowMinute = DateTime.Now.Minute;

				//每天更新
				if(radDay.Checked == true)
				{
					int hour = dateTimePicker1.Value.Hour;
					int second = dateTimePicker1.Value.Second;
					int minute = dateTimePicker1.Value.Minute;

					if( hour == nowHour
						&& second == nowSecond
						&& minute == nowMinute)
					{
						btnAllowUpdate_Click(null,null);
					}
				}
				else if(radWeek.Checked == true)//每周更新
				{
					int hour = dateTimePicker2.Value.Hour;
					int second = dateTimePicker2.Value.Second;
					int minute = dateTimePicker2.Value.Minute;

					if(Convert.ToInt32(DateTime.Now.DayOfWeek) == (cmbWeek.SelectedIndex + 1) 
						&& hour == nowHour
						&& second == nowSecond
						&& minute == nowMinute)
					{
						btnAllowUpdate_Click(null,null);
					}
				
			
				}
				else if(radMonth.Checked == true)	//每月更新
				{
					int hour = dateTimePicker3.Value.Hour;
					int second = dateTimePicker3.Value.Second;
					int minute = dateTimePicker3.Value.Minute;

					if(DateTime.Now.Day == (cmdDay.SelectedIndex + 1)
						&& hour == nowHour
						&& second == nowSecond
						&& minute == nowMinute)
					{
						btnAllowUpdate_Click(null,null);
					}
				}
			}

			RefreshVersion();
		}
	}
}
