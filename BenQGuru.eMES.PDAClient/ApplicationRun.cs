using System;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;

namespace BenQGuru.eMES.PDAClient
{
    /// <summary>
    /// ApplicationRun 的摘要说明。
    /// Laws Lu,2005/08/01,修改GetInfoForm方法
    /// </summary>
    /// 
    public class ApplicationRun
    {
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(System.IntPtr hWnd);

        public ApplicationRun()
        {
        }
        public static ApplicationContext applicationContext;
        private static FSplashForm splashForm = new FSplashForm();

        public static string[] appArguments = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            appArguments = args;
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ApplicationRun.otherException);
            Application.ThreadExit += new System.EventHandler(ApplicationRun.Exit);

            applicationContext = new ApplicationContext();
            Application.Idle += new EventHandler(OnAppIdle);
            splashForm.Show();
            Application.Run(applicationContext);
            
        }

        private static void OnAppIdle(object sender, EventArgs e)
        {
            if (applicationContext.MainForm == null)
            {
                Application.Idle -= new EventHandler(OnAppIdle);

                // 初始化ApplicationService.Current
                Service.ApplicationService.Current();
                //System.Threading.Thread threadDb = new System.Threading.Thread(
                //    new System.Threading.ThreadStart(TryConnectDB));
                //threadDb.Start();
                
                FMain mainForm = new FMain();
                applicationContext.MainForm = mainForm;
                Service.ApplicationService.Current().MainWindows = mainForm;
                Application.DoEvents();               
                splashForm.Close();
                splashForm = null;
            }
        }

        public static System.Diagnostics.Process RunningInstance(string processName)
        {
            try
            {
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();// (processName); 
                //查找相同名称的进程 
                foreach (System.Diagnostics.Process process in processes)
                {                    
                    //确认相同进程的程序运行位置是否一样. 
                    string moduleFileName = null;
                    try
                    {
                        moduleFileName = process.MainModule.FileName;
                    }
                    catch { }
                    if (moduleFileName != null && moduleFileName.IndexOf(processName) >= 0)
                    {
                        return process;
                    }                    
                }
            }
            catch { }            
            return null;
        }

        private static void otherException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (e.Exception.Source.Trim() != "Infragistics.Win.UltraWinGrid.v3.2"
                && e.Exception.Source.Trim() != "Infragistics.Win.UltraWinExplorerBar.v3.2")
            {
                string errorInfo = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")//error date & time
                    + "\t" + e.Exception.Message //error message
                    + "\t" + e.Exception.Source;	//error object name;

                UserControl.FileLog.FileLogOut("Client.log", errorInfo);
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)
                    BenQGuru.eMES.PDAClient.Service.ApplicationService.Current().DataProvider).PersistBroker.CloseConnection();
            }
        }

        private static void Exit(object sender, System.EventArgs e)
        {
            try
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)
                    BenQGuru.eMES.PDAClient.Service.ApplicationService.Current().DataProvider).PersistBroker.CloseConnection();
            }
            catch { }
        }        
        
    }
}
