using System;
using System.Windows.Forms;

namespace BenQGuru.eMES.Agent
{
	/// <summary>
	/// ApplicationRun 的摘要说明。
	/// </summary>
	public class ApplicationRun
	{
		[System.Runtime.InteropServices.DllImport("User32.dll")]
		private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);
		[System.Runtime.InteropServices.DllImport("User32.dll")]
		private static extern bool SetForegroundWindow(System.IntPtr hWnd);

		public ApplicationRun()
		{
		}
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.ThreadException +=  new System.Threading.ThreadExceptionEventHandler(ApplicationRun.otherException); 
			Application.ThreadExit += new System.EventHandler(ApplicationRun.Exit);

			
			System.Threading.Mutex appSingleton = new System.Threading.Mutex(false,"Agent");
			if(appSingleton.WaitOne(0,false))
			{
				Application.Run(new CollectAgent());
			}
			else
			{
				MessageBox.Show("已经有一个Agent应用程序在运行。\n点击确认退出","Agent 运行警告");
			}
			appSingleton.Close();
			
		}

		public static System.Diagnostics.Process RunningInstance() 
		{ 
			//			System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess(); 
			//			System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName (current.ProcessName); 
			//			//查找相同名称的进程 
			//			foreach (System.Diagnostics.Process process in processes) 
			//			{ 
			//				//忽略当前进程 
			//				if (process.Id != current.Id) 
			//				{ 
			//					//string a = process.MainModule.FileName;
			//					//确认相同进程的程序运行位置是否一样. 
			//					if(process.MainModule.FileName == current.MainModule.FileName)
			//					{
			////					if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName) 
			////					{ 
			//						//Return the other process instance. 
			//						return process; 
			//					}
			//				} 
			//			} 
			//No other instance was found, return null. 
			return null; 
		}

		private static void HandleRunningInstance(System.Diagnostics.Process instance)
		{
			//MessageBox.Show("该应用系统已经在运行！","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Information);
			ShowWindowAsync(instance.MainWindowHandle,3); //调用api函数，最大化显示窗口
			SetForegroundWindow(instance.MainWindowHandle); //将窗口放置最前端。
		}
		


		private static void otherException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			if(e.Exception.Source.Trim() != "Infragistics.Win.UltraWinGrid.v3.2")
			{
				ApplicationRun.GetInfoForm().Add("$CS_System_Error:"+e.Exception.Message);

				UserControl.FileLog.FileLogOut("Client.log",e.Exception.Message);
			}
		}

		private static void Exit(object sender, System.EventArgs e)
		{
		}

		private static FInfoForm infoForm=null;
		public static FInfoForm GetInfoForm()
		{   
			if (infoForm==null)
			{

				infoForm = new FInfoForm();
				infoForm.Height = 200;
				infoForm.Dock = DockStyle.Fill;

				infoForm.Show();
			}
			else
			{
				if (!infoForm.Visible) 
				{
					infoForm=new FInfoForm();
					infoForm.Show();
				}
			}
			return infoForm;
		}
	}
}
