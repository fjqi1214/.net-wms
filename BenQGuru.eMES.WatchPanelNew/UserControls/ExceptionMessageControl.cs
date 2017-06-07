using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace BenQGuru.eMES.WatchPanelNew
{
    public partial class ExceptionMessageControl : System.Windows.Forms.UserControl
    {
        public ExceptionMessageControl()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32")]
        public static extern int GetScrollPos(IntPtr hwnd, int nBar);
        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar,int nPos, bool bRedraw);

        public const int EM_LINESCROLL = 0xb6;

        public string ExpectionMessage
        {
            set { this.txtExpection.Text = value; }
            get { return this.txtExpection.Text; }
        }

        public void ClearMessage()
        {
            this.txtExpection.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i = GetScrollPos(this.txtExpection.Handle, 1);
            SendMessage(this.txtExpection.Handle, EM_LINESCROLL, 0, 1);
            if (i == GetScrollPos(this.txtExpection.Handle, 1))
            {
                //回到顶部，这里用SetScrollPos似乎有问题，滚动条和文字不是同步更新
                this.txtExpection.SelectionStart = 0;
                this.txtExpection.SelectionLength = 1;
                this.txtExpection.ScrollToCaret();
                this.txtExpection.SelectionLength = 0;
            }

        }
    }
}
