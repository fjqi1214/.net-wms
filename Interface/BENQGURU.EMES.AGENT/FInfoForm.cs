using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using UserControl;

using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Agent
{
	/// <summary>
	/// FInfoForm 的摘要说明。
	/// </summary>
	public class FInfoForm : System.Windows.Forms.Form
	{
		private UserControl.UCMessage memInfor;
		private UserControl.UCButton ucButton1;
		private UserControl.UCButton btnExit;
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

		int iUpdaterAlertCount = 0;

		public FInfoForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//

			if(AgentInfo.Module != null && AgentInfo.Module != string.Empty)
			{
				this.Text = string.Format("MES {0} Agent 采集信息",AgentInfo.Module);
			}
			
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FInfoForm));
			this.memInfor = new UserControl.UCMessage();
			this.ucButton1 = new UserControl.UCButton();
			this.btnExit = new UserControl.UCButton();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// memInfor
			// 
			this.memInfor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.memInfor.AutoScroll = true;
			this.memInfor.BackColor = System.Drawing.Color.Gainsboro;
			this.memInfor.ButtonVisible = false;
			this.memInfor.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.memInfor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.memInfor.Location = new System.Drawing.Point(0, 0);
			this.memInfor.Name = "memInfor";
			this.memInfor.Size = new System.Drawing.Size(496, 256);
			this.memInfor.TabIndex = 2;
			// 
			// ucButton1
			// 
			this.ucButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.ucButton1.BackColor = System.Drawing.SystemColors.Control;
			this.ucButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucButton1.BackgroundImage")));
			this.ucButton1.ButtonType = UserControl.ButtonTypes.Refresh;
			this.ucButton1.Caption = "清除";
			this.ucButton1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ucButton1.Location = new System.Drawing.Point(400, 256);
			this.ucButton1.Name = "ucButton1";
			this.ucButton1.Size = new System.Drawing.Size(88, 99);
			this.ucButton1.TabIndex = 3;
			this.ucButton1.Click += new System.EventHandler(this.ucButton1_Click);
			// 
			// btnExit
			// 
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExit.BackColor = System.Drawing.SystemColors.Control;
			this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
			this.btnExit.ButtonType = UserControl.ButtonTypes.None;
			this.btnExit.Caption = "退出";
			this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnExit.Location = new System.Drawing.Point(312, 256);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(88, 99);
			this.btnExit.TabIndex = 4;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			this.btnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnExit_KeyDown);
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 30000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// FInfoForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(218)), ((System.Byte)(235)), ((System.Byte)(243)));
			this.ClientSize = new System.Drawing.Size(496, 280);
			this.ControlBox = false;
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.ucButton1);
			this.Controls.Add(this.memInfor);
			this.KeyPreview = true;
			this.MinimizeBox = false;
			this.Name = "FInfoForm";
			this.Opacity = 0.75;
			this.Text = "MES Agent 采集信息";
			this.TopMost = true;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FInfoForm_KeyDown);
			this.Load += new System.EventHandler(this.FInfoForm_Load);
			this.ResumeLayout(false);

		}
		#endregion
		//		public void Add(UserControl.MessageEventArgsUnit messages)
		//		{
		//				memInfor.Add(messages);
		//		}

		public void Add(string text)
		{
			memInfor.Add(text);
		}

		public void Add(UserControl.Message message)
		{
			UserControl.Messages messages = new UserControl.Messages();
			messages.Add(message);
			this.Add(messages);
		}

		public void Add(UserControl.Messages messages)
		{
			memInfor.Add(messages);
		}

		public void Clear()
		{
			memInfor.Clear();
		}

		private void FInfoForm_Load(object sender, System.EventArgs e)
		{
		
			this.Width=256;
			this.Left=Screen.PrimaryScreen.Bounds.Width-this.Width;
			this.Top=Screen.PrimaryScreen.WorkingArea.Height-this.Height;
		}
		//Amoi,Laws Lu,2005/08/01,添加	清除信息框
		private void ucButton1_Click(object sender, System.EventArgs e)
		{
			memInfor.Clear();
		}

		private void btnExit_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.Alt && e.KeyCode == Keys.F4)
			{
				e.Handled = true;
			}
		}

		private void FInfoForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.Alt && e.KeyCode == Keys.F4)
			{
				e.Handled = true;
			}
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			if(DialogResult.OK == 
				MessageBox.Show("确认要退出吗?","Confrim"
				,MessageBoxButtons.OKCancel
				,MessageBoxIcon.Asterisk))
			{
				this.Close();
				Application.Exit();
			}
		}

		public object CheckUpdate()
		{
			BenQGuru.eMES.Common.Domain.IDomainDataProvider _dataProvider = 
				BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();

			try
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_dataProvider).PersistBroker.AutoCloseConnection = false;
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_dataProvider).PersistBroker.OpenConnection();
				//object objUpdater = null;
				//Laws Lu,2005/08/22,新增	版本更新
				object objUpdater = Web.Helper.FormatHelper.GetCsVersion(_dataProvider);

				string strVersion = UserControl.FileLog.GetLocalCSVersion(UserControl.FileLog.VersionFileName);
				int iErrorCount = 0;
				if(objUpdater != null)
				{
					iErrorCount = Web.Helper.FormatHelper.GetUpdateErrorCount(_dataProvider,((BenQGuru.eMES.Web.Helper.Updater)objUpdater) .CSVersion);
				}
				//Laws Lu,2006/08/14 修改	如果更新有错误也不允许继续操作
				if ((objUpdater != null 
					&& strVersion != ((BenQGuru.eMES.Web.Helper.Updater)objUpdater) .CSVersion.Trim())
					||  (objUpdater != null && iErrorCount != 0  
					&& strVersion == ((BenQGuru.eMES.Web.Helper.Updater)objUpdater) .CSVersion.Trim()))
				{
					return objUpdater;
					//				bResult = true;
					//				Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
					//				
					//				AutoUpdate(upd.Location.Trim(),upd.LoginUser,upd.LoginPassword);
				}
			}
			catch(Exception ex)
			{
				Common.Log.Error(ex.Message);
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_dataProvider).PersistBroker.CloseConnection();
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_dataProvider).PersistBroker.AutoCloseConnection = true;
			}

			return null;
		}

		public System.Diagnostics.Process RunningInstance() 
		{ 
			try
			{
				System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess(); 
				System.Diagnostics.Process[] processes =  System.Diagnostics.Process.GetProcesses();
				//			try
				//			{
				//				System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName (current.ProcessName); 
				//			}
				//			catch{}
				//查找相同名称的进程 
				foreach (System.Diagnostics.Process process in processes) 
				{ 
					//忽略当前进程 
					try
					{
						if (process.Id != current.Id) 
						{ 
							//string a = process.MainModule.FileName;
							//确认相同进程的程序运行位置是否一样.
							string moduleFileName = null;
					
							moduleFileName = process.MainModule.FileName;
					
					
							if(moduleFileName != null && moduleFileName == current.MainModule.FileName)
							{
								//					if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName) 
								//					{ 
								//Return the other process instance. 
								return process; 
							}
						}
					}
					catch{}
				} 
			}
			catch{}
			//No other instance was found, return null. 
			return null; 
		}

		public System.Diagnostics.Process RunningInstance(string processName) 
		{ 
			try
			{
				//System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess(); 
				System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();// (processName); 
				//查找相同名称的进程 
				foreach (System.Diagnostics.Process process in processes) 
				{ 
					//忽略当前进程 
					//				if (process.Id != current.Id) 
					//				{ 
					//string a = process.MainModule.FileName;
					//确认相同进程的程序运行位置是否一样. 
					string moduleFileName = null;
					try
					{
						moduleFileName = process.MainModule.FileName;
					}
					catch{}
					if(moduleFileName != null && moduleFileName.IndexOf(processName) >= 0)
					{
						//					if (process.MainModule.FileName.Location.Replace("/", "\\") == current.MainModule.FileName) 
						//					{ 
						//Return the other process instance. 
						//			if(processes.Length > 0)
						//			{
						return process; 
					}
					//					}
					//				} 
				} 
			}
			catch{}
			//No other instance was found, return null. 
			return null; 
		}


		object objUpdater = null;
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			if((System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"] != null
				&& System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"].Trim() != String.Empty))
			{
				string configHour = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"].Trim().Substring(0,2)).ToString();
				string configTime = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"].Trim().Substring(3,2)).ToString();

				if(configHour == DateTime.Now.Hour.ToString() &&
					configTime == DateTime.Now.Minute.ToString() && iUpdaterAlertCount == 0)
				{
					objUpdater = CheckUpdate();
					if(objUpdater != null)
					{
						iUpdaterAlertCount = Convert.ToInt32(4 * timer1.Interval);

						memInfor.Add(new UserControl.Message(UserControl.MessageType.Error,
							"$CS_NEW_VERSION_AVIALABLE_PLS_LOGOUT " + Convert.ToInt32(iUpdaterAlertCount/timer1.Interval)));

							
						//iUpdaterAlertCount = 60;
						//							System.Threading.Thread.Sleep(30000);
						//							//超出预警次数，自动退出系统
						//						
						//							this.Close();
						//							Application.Exit();
					}
				}
						
				//this.
				//				Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
				//				ApplicationService.AutoUpdate(upd.Location.Trim(),upd.LoginUser,upd.LoginPassword);
			}
			if(iUpdaterAlertCount > 0)
			{
				iUpdaterAlertCount -=  Convert.ToInt32(1 * timer1.Interval);
				memInfor.Add(new UserControl.Message(UserControl.MessageType.Error,
					"$CS_NEW_VERSION_AVIALABLE_PLS_LOGOUT " + Convert.ToInt32(iUpdaterAlertCount/timer1.Interval)));

				if(iUpdaterAlertCount == 0)
				{
					if(objUpdater != null)
					{
						Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
						AutoUpdate(upd.Location.Trim(),upd.LoginUser,upd.LoginPassword);
					}
				}

			}
				
		}

		public const string MesAgent = "BenQGuru.eMES.Agent.exe";
		public const string DCTServer = "DCTControlPanel.exe";

		public void AutoUpdate(string location,string user,string password)
		{
			System.Diagnostics.Process pr = new System.Diagnostics.Process();

			System.Diagnostics.ProcessStartInfo prStart = new System.Diagnostics.ProcessStartInfo();

			prStart.FileName = "AutoUpdate.exe";

			prStart.Arguments = location + " " + AppDomain.CurrentDomain.FriendlyName;

			pr.StartInfo = prStart;

			System.Diagnostics.Process prAgent = RunningInstance(MesAgent);

			System.Diagnostics.Process prDCT = RunningInstance(DCTServer);

			pr.Start();

			if( prAgent != null)
			{
				prAgent.Kill();
			}

			if( prDCT != null)
			{
				prDCT.Kill();
			}
			Application.Exit();
		}
		//EndAmoi

	}
}
