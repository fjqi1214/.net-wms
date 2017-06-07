using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Diagnostics;

namespace UpdaterConfig
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Process[] procs = Process.GetProcessesByName("UpdaterConfig");
            if (procs.Length <= 1)
            {
                Application.Run(new frmUpdaterConfig());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
