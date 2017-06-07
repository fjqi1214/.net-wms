using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;


using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.GZip;

//using log4net.Appender;

namespace AutoUpdate
{
	public delegate void BeforeUpdaterHandler(object sender,System.EventArgs e);
	public delegate void AfterUpdaterHandler(object sender,System.EventArgs e);
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class frmAutoUpdater : System.Windows.Forms.Form
	{
		private const string prepare_update = "正在连接更新服务器...";
		private const string downloading_file = "正在下载，请稍候......";
		private const string on_update = "Guru eMes系统自动更新中，请稍候......";
		private const string system_restart = "正在重新启动MES系统......";

		private const string start_update = "\r\nStart update...";
		private const string start_load_file_from_server = "Start download file from server...";
		private const string process_ok = "Done...";
		private const string process_failure = "Failure...";
		private const string start_unzip_file_from_server = "Start unzip file...";
		private const string finish_update = "Update OK";
		//更新路径
		private string updaterUri = System.Configuration.ConfigurationSettings.AppSettings["Uri"];
		//主程序名称
		private string mainAssemblyName = System.Configuration.ConfigurationSettings.AppSettings["MainAssemblyName"];
        //DCT程序名称
        private string dctAssemblyName = System.Configuration.ConfigurationSettings.AppSettings["DCTAssemblyName"];
		//压缩文件名称
		private string fileName = System.Configuration.ConfigurationSettings.AppSettings["ZipFileName"];
		private System.Windows.Forms.ProgressBar pgbDownload;
        private bool m_NeedRunDCT = false;

		public event BeforeUpdaterHandler BeforeUpdater;
		public event AfterUpdaterHandler AfterUpdater;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmAutoUpdater()
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
		/// 使用外部参数启动
		/// </summary>
		/// <param name="args">参数</param>
		public frmAutoUpdater(string[] args)
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			if(args != null && args.Length > 0)
			{
				updaterUri = args[0];
				if(args.Length > 1)
				{
					mainAssemblyName = args[1];
				}
			}

			this.Text = prepare_update;

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
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmAutoUpdater));
			this.pgbDownload = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// pgbDownload
			// 
			this.pgbDownload.Location = new System.Drawing.Point(8, 8);
			this.pgbDownload.Name = "pgbDownload";
			this.pgbDownload.Size = new System.Drawing.Size(448, 16);
			this.pgbDownload.TabIndex = 1;
			// 
			// frmAutoUpdater
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(472, 56);
			this.ControlBox = false;
			this.Controls.Add(this.pgbDownload);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAutoUpdater";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Guru eMes系统自动更新中，请稍候......";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.frmAutoUpdater_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main(string[] args) 
		{

			Application.Run(new frmAutoUpdater(args));
		}

		//解压缩文件
		public void UnZipFile(string file)
		{
            string serverDir = Application.StartupPath + "\\";
            ZipInputStream s = new ZipInputStream(File.OpenRead(serverDir + file));
		
			ZipEntry theEntry;
			try
			{
				ArrayList alDBLog = new ArrayList();
				string version = String.Empty;
				//循环获取压缩实体
				while ((theEntry = s.GetNextEntry()) != null) 
				{
					
					string directoryName = Path.GetDirectoryName(theEntry.Name);
					string fileName  = Path.GetFileName(theEntry.Name);

                    if (directoryName != String.Empty && !Directory.Exists(serverDir + directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
					
					if (fileName != String.Empty 
						&& fileName.IndexOf("AutoUpdate") < 0
						&& fileName.IndexOf("SharpZipLib") < 0
						&& fileName.IndexOf("UpdateObject") < 0
						&& fileName.IndexOf("Client.log") < 0
						&& fileName.IndexOf("sqlnet.log") < 0
						&& fileName.IndexOf("UpdateLog.txt") < 0
						&& fileName.IndexOf("Log.") < 0
						&& fileName.IndexOf("_log") < 0) 
					{
						Tools.UpdateLog updateLog = new Tools.UpdateLog();
					
						updateLog.FileName = fileName;
						updateLog.UpdateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

						updateLog.Result = "TRUE";
			
						if(File.Exists(fileName))
						{
							try
							{
								File.SetAttributes(fileName,FileAttributes.Normal);
								//Laws Lu,2006/08/15 无需删除，只需要更改属性
//								File.Delete(fileName);
							}
							catch(Exception ex)
							{
								updateLog.Result = "FALSE";
							}

							FileInfo fi = new FileInfo(fileName);

							if(fi.LastWriteTime == theEntry.DateTime || fi.Length == 0)
							{
								continue;
							}
						}

						Tools.FileLog.FileLogContentOut(fileName);

						#region 写入文件
                        if (directoryName != String.Empty)
                            directoryName = directoryName + "\\";
                        FileStream streamWriter = File.Create(directoryName + fileName);
						try
						{
							int size = 2048;
							byte[] data = new byte[2048];
							while (true) 
							{
								size = s.Read(data, 0, data.Length);

								//get version
								if(fileName.IndexOf("Version.") >= 0)
								{
									version = System.Text.Encoding.Default.GetString(data).Replace("\0","");
								}

								if (size > 0) 
								{
									//写文件
									streamWriter.Write(data, 0, size);
								} 
								else 
								{
									break;
								}
							}

							Tools.FileLog.FileLogOut(process_ok);
						}
						catch(Exception ex)
						{
							Tools.FileLog.FileLogOut(process_failure + "\t" + ex.Message);
							updateLog.Result = "FALSE";
						}
						finally
						{
							streamWriter.Close();
						}
						#endregion

						alDBLog.Add(updateLog);

					}
					
					//进度条累加
					if(pgbDownload.Value < 100)
					{
						pgbDownload.Value += 1;


						pgbDownload.Refresh();
					}

					System.Threading.Thread.Sleep(50);

					
				}
				if(alDBLog.Count > 0)
				{
					Tools.UpdateLog[] logs = new Tools.UpdateLog[alDBLog.Count];
					for(int i = 0 ; i < alDBLog.Count ; i ++ )
					{
						(alDBLog[i] as Tools.UpdateLog) .Version = version;

						logs[i] = alDBLog[i] as Tools.UpdateLog;
					}

					
					Tools.FileLog.DBLogOut(logs);
				}
			}
			catch(Exception ex)
			{
				Tools.FileLog.FileLogOut(process_failure + "\t" + ex.Message);
			}
			finally
			{
				s.Close();
			}
		}
		//获取运行中的主进程
		public System.Diagnostics.Process GetRunningInstance() 
		{ 
			System.Diagnostics.Process mainProcess = null;

			try
			{
				//System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess(); 
				System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();//(mainAssemblyName); 
				//查找相同名称的进程 
				foreach (System.Diagnostics.Process process in processes) 
				{
                    try
                    {
                        string prcName = process.MainModule.ModuleName;
                        string prcStartPath = process.MainModule.FileName.Substring(0, process.MainModule.FileName.LastIndexOf(@"\") + 1);

                        //确认相同进程的程序运行位置是否一样. 
                        if (prcName == mainAssemblyName && prcStartPath == AppDomain.CurrentDomain.BaseDirectory)
                        {
                            mainProcess = process;
                            break;
                        }
                    }
                    catch 
                    {
                    }
				}
			}
			catch
			{}
			return mainProcess; 
		}

		//杀死主进程
		private void KillMainProcess()
		{
			System.Diagnostics.Process mainProcess = GetRunningInstance();
			if(mainProcess != null)
			{
				mainProcess.Kill();
			}
		}

        //获取DCT进程
        public System.Diagnostics.Process GetRunningDCTInstance()
        {
            System.Diagnostics.Process dctProcess = null;

            try
            {
                //System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
                //查找相同名称的进程 
                foreach (System.Diagnostics.Process process in processes)
                {
                    try
                    {
                        string prcName = process.MainModule.ModuleName;
                        string prcStartPath = process.MainModule.FileName.Substring(0, process.MainModule.FileName.LastIndexOf(@"\") + 1);

                        //确认相同进程的程序运行位置是否一样. 
                        if (prcName == dctAssemblyName && prcStartPath == AppDomain.CurrentDomain.BaseDirectory)
                        {
                            dctProcess = process;
                            break;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
            return dctProcess;
        }

        //杀死DCT进程
        private void KillDCTProcess()
        {
            System.Diagnostics.Process dctProcess = this.GetRunningDCTInstance();
            if (dctProcess != null)
            {
                this.m_NeedRunDCT = true;
                dctProcess.Kill();
            }
        }

		//完成更新
		private void CompleteMainProcess()
		{
			if(File.Exists(fileName))
			{
				File.Delete(fileName);
			}

			if(System.IO.File.Exists(mainAssemblyName))
			{
				System.Diagnostics.Process.Start(mainAssemblyName);
			}

            if (this.m_NeedRunDCT)
            {
                if (System.IO.File.Exists(dctAssemblyName))
                {
                    System.Diagnostics.Process.Start(dctAssemblyName);
                }
            }

			Application.ExitThread();
		}
		//开始更新
		private void StartUpdater()
		{
			//string fileName = System.Configuration.ConfigurationSettings.AppSettings["ZipFileName"];
			
			System.Net.WebClient wc = new System.Net.WebClient();
			try
			{
				if(System.IO.File.Exists(fileName))
				{
					System.IO.File.Delete(fileName);
				}
				//进度条累加
				for(int i = 1;i < 1300000;i ++)
				{
					pgbDownload.Value = Convert.ToInt32(i/30000);
				}

				pgbDownload.Refresh();
				
				Tools.FileLog.FileLogOut(start_load_file_from_server);

				this.Text = downloading_file;

				wc.DownloadFile(updaterUri + "/" + fileName + "?tmp=" 
					+ (new System.Random()).Next(1000000).ToString(),fileName);

				this.Text = on_update;

				System.Threading.Thread.Sleep(2000);
				//Log
				Tools.FileLog.FileLogOut(process_ok);

				Tools.FileLog.FileLogOut(start_unzip_file_from_server);

				UnZipFile(fileName);

				Tools.FileLog.FileLogOut(process_ok);

				//进度条累加
				for(int i = 600000;i < 10000000;i ++)
				{
					if(pgbDownload.Value == 100)
					{
						break;
					}
					pgbDownload.Value = Convert.ToInt32(i/30000);
				}

				if(pgbDownload.Value != 100)
				{
					pgbDownload.Value = 100;
				}

				pgbDownload.Refresh();

			}
			catch(Exception ex)
			{
				Tools.FileLog.FileLogOut(process_failure + "\t" +  ex.Message);
			}
			finally
			{
				Tools.FileLog.FileLogOut(finish_update);
				wc.Dispose();
			}
		}

		private void frmAutoUpdater_Load(object sender, System.EventArgs e)
		{
			System.Threading.Thread.Sleep(1000);
			this.Show();

			if(BeforeUpdater != null)
			{
				BeforeUpdater(sender,e);
			}
			//杀死主进程
			KillMainProcess();
            //杀死DCT进程
            KillDCTProcess();

			Tools.FileLog.FileLogOut(start_update);
			//开始更新
			StartUpdater();

			this.Text = system_restart;
			//完成更新
			CompleteMainProcess();

			if(AfterUpdater != null)
			{
				AfterUpdater(sender,e);
			}
		}
	}
}
