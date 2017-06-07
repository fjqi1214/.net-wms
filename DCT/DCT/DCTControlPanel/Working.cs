using System;
using System.Collections;
using System.Threading;
using System.Net;

using UserControl;
using BenQGuru.eMES.Common.DCT.ATop;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DCT.ATop.GW28;
using System.Windows.Forms;


namespace BenQGuru.eMES.Common.DCT
{
	/// <summary>
	/// WorkingStack 的摘要说明。
	/// </summary>
	public class Working
	{
		[System.Runtime.InteropServices.DllImport("User32.dll")]
		private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);
		[System.Runtime.InteropServices.DllImport("User32.dll")]
		private static extern bool SetForegroundWindow(System.IntPtr hWnd);

		public static string dctServer = "BenQGuruDCTServer";
		public static void Main()
		{
//			Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",true);
//			//ArrayList alRun = new ArrayList();
//			//alRun.AddRange(regKey.GetValueNames());
//			//if(!alRun.Contains(dctServer))
//			//{
//			regKey.SetValue(dctServer,Application.ExecutablePath);
//			regKey.Flush();
//			//}
//
//			regKey.Close();

			Application.ThreadException +=  new System.Threading.ThreadExceptionEventHandler(Working.otherException); 
			Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

			System.Diagnostics.Process pr = RunningInstance();
			if(pr == null)
			{
				Application.Run(new frmDCTServer());
			}
			else
			{
				HandleRunningInstance(pr);
			}
			
		}

		private static void HandleRunningInstance(System.Diagnostics.Process instance)
		{
			//MessageBox.Show("该应用系统已经在运行！","提示信息",MessageBoxButtons.OK,MessageBoxIcon.Information);
			try
			{
				ShowWindowAsync(instance.MainWindowHandle,3); //调用api函数，最大化显示窗口
				SetForegroundWindow(instance.MainWindowHandle); //将窗口放置最前端。
			}
			catch{}
		}

		public static System.Diagnostics.Process RunningInstance() 
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
				if (process.Id != current.Id) 
				{ 
					//string a = process.MainModule.FileName;
					//确认相同进程的程序运行位置是否一样.
					string moduleFileName = null;
					try
					{
						moduleFileName = process.MainModule.FileName;
					}
					catch{}
					if(moduleFileName != null && moduleFileName == current.MainModule.FileName)
					{
						//					if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName) 
						//					{ 
						//Return the other process instance. 
						return process; 
					}
				} 
			} 
			//No other instance was found, return null. 
			return null; 
		}

		private static void otherException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			FileLog.FileLogOut("DCTControlPanel.log",e.Exception.Message);
		}

		private static void Application_ApplicationExit(object sender, EventArgs e)
		{
			System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess(); 
			System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName (current.ProcessName); 
			//查找相同名称的进程 
			foreach (System.Diagnostics.Process process in processes) 
			{ 
				//忽略当前进程 
//				if (process.Id != current.Id) 
//				{ 
					//string a = process.MainModule.FileName;
					//确认相同进程的程序运行位置是否一样. 
					if(process.MainModule.FileName == current.MainModule.FileName)
					{
//					if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName) 
//					{ 
						//Return the other process instance. 

						try
						{
							process.Kill(); 
						}
						catch
						{}

						break;
					}
//				} 
			} 
		}
	}
}

