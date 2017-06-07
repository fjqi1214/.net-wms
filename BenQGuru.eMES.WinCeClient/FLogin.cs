using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FLogin : Form
    {
        
        WebServiceFacade facade;


        private bool m_Authenticated = false;

        public bool Authenticated
        {
            get { return m_Authenticated; }
        }

        public WebServiceFacade WebServiceFacade
        {
            get
            {
                if (facade == null)
                {
                    facade = new WebServiceFacade();
                }
                return facade;
            }
        }

        public FLogin()
        {
            InitializeComponent();
            //Banner
            Bitmap bmpBanner = new Bitmap(Properties.Resources.Login_banner_emes);
            this.pictureBox1.Image = bmpBanner;
            //Copyright //daniel 2015/3/6
            this.picCopyright.Image = new Bitmap(Properties.Resources.Login_CopyRight_Image);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //#if DEBUG
            //{
            //    m_Authenticated = true;
            //    //this.DialogResult = DialogResult.Yes;
            //    this.Close();
            //}
            //#endif
            if (string.IsNullOrEmpty(txtUserCode.Text.Trim()))
	        {
                 //txtUserCode.Focus();
                 //return;
	        }
            if (string.IsNullOrEmpty(txtPassWord.Text.Trim()))
	        {
                 //txtPassWord.Focus();
                 //return;
	        }
            //if (string.IsNullOrEmpty(txtResCode.Text.Trim()))
            //{
            //     txtResCode.Focus();
            //     return;
            //}

            try 
	        {	        
                if (facade==null)
                {
                    facade = new WebServiceFacade();
                } 
                string userCode=txtUserCode.Text.Trim().ToUpper();
                string passWord=txtPassWord.Text.Trim().ToUpper();
                //string resCode=txtResCode.Text.Trim().ToUpper();
                string resCode = string.Empty;
                string serviceResult = facade.Login(userCode, passWord, resCode);
                //string serviceResult = "WebService_Op_Success";
                if (serviceResult==Enums.WebService_Op_Success)
                {
                    //MessageBox.Show("登陆成功");
                    ApplicationService.Current().LoginInfo = new LoginInfo(userCode, resCode, null);
                    //this.DialogResult = DialogResult.Yes;
                    m_Authenticated = true;
                    this.Close();

                }
                else
                {
                    MessageBox.Show(serviceResult);
                }
	        }
	        catch (Exception ex)
	        {
                MessageBox.Show(ex.Message,Enums.WinCE_MsgBox_Title_Tips);
	        }
            
        }

        private void FLogin_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.None;

            txtUserCode.Focus();
        }

        //实现回车 跳转到输入密码
        private void txtUserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.txtUserCode.Text.Trim() == string.Empty)
                {
                    this.txtUserCode.Focus();
                    return;
                }

                this.txtPassWord.Focus();
            }
        }

        //实现回车 跳转
        private void txtPassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' )
            {
                if (this.txtUserCode.Text.Trim() == string.Empty)
                {
                    this.txtUserCode.Focus();
                    return;
                }

                if (this.txtPassWord.Text.Trim() == string.Empty)
                {
                    this.txtPassWord.Focus();
                    return;
                }

                this.txtResCode.Focus();
            }
        }

        //实现回车 跳转
        private void txtResCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnLogin_Click(btnLogin, null);
            }
        }

        private void FLogin_Paint(object sender, PaintEventArgs e)
        {
            //获取窗体handle
            //IntPtr hwnd = WinAPI.FindWindow(null, this.Text);

            ////隐藏OK button 和 X button
            //WinAPI.HideOKButton(hwnd);
            //WinAPI.HideXButton(hwnd);

        }
    }

    class WinAPI
    {

        [DllImport("aygshell.dll")]
        private static extern bool SHDoneButton(IntPtr hWnd, UInt32 dwState);
        [DllImport("coredll.dll")]
        public static extern UInt32 SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);
        [DllImport("coredll.dll")]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("coredll.dll")]
        public static extern IntPtr GetCapture();

        [DllImport("coredll.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        public const UInt32 SHDB_SHOW = 0x0001; 
        public const UInt32 SHDB_HIDE = 0x0002; 
        public const int GWL_STYLE = -16; 
        public const UInt32 WS_NONAVDONEBUTTON = 0x00010000; 

        public static void HideOKButton(IntPtr hWnd) 
        { 
            SHDoneButton(hWnd, SHDB_HIDE); 
        }

        public static void HideXButton(IntPtr hWnd) 
        { 
            UInt32 dwStyle = GetWindowLong(hWnd, GWL_STYLE); 
            if ((dwStyle & WS_NONAVDONEBUTTON) == 0) 
                SetWindowLong(hWnd, GWL_STYLE, dwStyle | WS_NONAVDONEBUTTON); 
        }


        /// <summary>
        /// 显示/隐藏软键盘
        /// </summary>
        /// <param name="SIP_STATUS"></param>
        /// <returns></returns>
        [DllImport("coredll", EntryPoint = "SipShowIM")]
        private static extern bool SipShowIM(IntPtr SIP_STATUS);
        private static readonly IntPtr SIPF_OFF = (IntPtr)0x0;
        private static readonly IntPtr SIPF_ON = (IntPtr)0x1;

        /// <summary>
        /// 显示/隐藏软键盘
        /// </summary>
        /// <param name="visible">是否显示</param>
        /// <returns></returns>
        public static bool SipShowIM(bool isShow)
        {
            return SipShowIM(isShow ? SIPF_ON : SIPF_OFF);
        }
    } 
}