using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BenQGuru.eMES.WinCeClient
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //FLogin form = new FLogin();
            //if (form.ShowDialog()== DialogResult.Yes)
            //{
            //    Application.Run(new FMenuTest());
            //}


            Application.Run(new FMenuTest());
        }
    }
}