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
    public partial class FacMessageControl : System.Windows.Forms.UserControl
    {
        //SetWindowLong函数：改变窗口属性
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern long SetWindowLong(
                IntPtr hWnd, //窗口句柄
                int nIndex, // 指定要设定的值的信息
                IntPtr dwNewLong // 新值
        );

        //SetWindowLong函数：获得窗口属性
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        const int GWL_EXSTYLE = (-20);
        const int WS_EX_TRANSPARENT = 0x20;

        public FacMessageControl()
        {
            InitializeComponent();
            this.pictureBoxBACK.ImageLocation = Application.StartupPath + @"\background.jpg";  
        }

        private void FacMessageControl_Load(object sender, EventArgs e)
        {
            this.pictureBoxBACK.ImageLocation = Application.StartupPath + @"\background.jpg";  
            SetWindowLong(this.richTextMessage.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(richTextMessage.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT));
            this.richTextMessage.Rtf = this.RTF;
        }

        private string m_RTF;

        public string RTF
        {
            get { return m_RTF; }
            set { m_RTF = value; }
        }

        public void ValueRefresh()
        {
            this.richTextMessage.Rtf = this.RTF;
        }

    }
}
