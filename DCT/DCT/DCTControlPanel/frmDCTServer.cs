using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DCT.ATop;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DCT.ATop.GW28;
using BenQGuru.eMES.Common.DCT.ATop.DT4000;
using BenQGuru.eMES.Common.DCT.ATop.DCN500;
using BenQGuru.eMES.Web.Helper;

//added by hi1/leon.yuan 2009/1/8 for setting environment variable "NLS_Lang" in current process. 
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
//end added by hi1/leon.yuan 2009/1/8

namespace BenQGuru.eMES.Common.DCT
{
    /// <summary>
    /// frmDCTServer 的摘要说明。
    /// </summary>
    public class frmDCTServer : System.Windows.Forms.Form
    {
        private IDomainDataProvider _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public System.Diagnostics.Process RunningInstance()
        {
            try
            {
                System.Diagnostics.Process current = System.Diagnostics.Process.GetCurrentProcess();
                System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcesses();
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


                            if (moduleFileName != null && moduleFileName == current.MainModule.FileName)
                            {
                                //					if (System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName) 
                                //					{ 
                                //Return the other process instance. 
                                return process;
                            }
                        }
                    }
                    catch { }
                }
            }
            catch { }
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
                    catch { }
                    if (moduleFileName != null && moduleFileName.IndexOf(processName) >= 0)
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
            catch { }
            //No other instance was found, return null. 
            return null;
        }

        //Laws Lu,2006/06/03	添加	使用一个静态的连接

        private bool allowClose = false;
        private DateTime _StartDateTime = DateTime.Now;
        Hashtable _ClientList = new Hashtable();
        Hashtable _ThreadList = new Hashtable();
        private static ILog Logger;
        private static string configFile = "log4net.dll.dct-log4net";

        private System.Windows.Forms.ListView lstDCTLient;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Button btnViewDCT;
        private System.Windows.Forms.Button btnStartDCT;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtRes;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblRes;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenu contextMenu1;
        //private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.CheckBox chkAutoRun;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnRefresh;
        private System.ComponentModel.IContainer components;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.Timer timer2;
        private MenuItem menuItem2;

        //Laws Lu，2007/01/16 次数
        int iUpdaterAlertCount = 0;

        public frmDCTServer()
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
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDCTServer));
            this.lstDCTLient = new System.Windows.Forms.ListView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.btnViewDCT = new System.Windows.Forms.Button();
            this.btnStartDCT = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtRes = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblRes = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.chkAutoRun = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstDCTLient
            // 
            this.lstDCTLient.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lstDCTLient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDCTLient.ForeColor = System.Drawing.SystemColors.Info;
            this.lstDCTLient.LargeImageList = this.imgList;
            this.lstDCTLient.Location = new System.Drawing.Point(0, 0);
            this.lstDCTLient.MultiSelect = false;
            this.lstDCTLient.Name = "lstDCTLient";
            this.lstDCTLient.Size = new System.Drawing.Size(690, 503);
            this.lstDCTLient.TabIndex = 0;
            this.lstDCTLient.UseCompatibleStateImageBehavior = false;
            this.lstDCTLient.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstDCTLient_MouseDown);
            this.lstDCTLient.Click += new System.EventHandler(this.lstDCTLient_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "");
            this.imgList.Images.SetKeyName(1, "");
            this.imgList.Images.SetKeyName(2, "");
            // 
            // btnViewDCT
            // 
            this.btnViewDCT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewDCT.Location = new System.Drawing.Point(608, 464);
            this.btnViewDCT.Name = "btnViewDCT";
            this.btnViewDCT.Size = new System.Drawing.Size(75, 23);
            this.btnViewDCT.TabIndex = 2;
            this.btnViewDCT.Text = "监控设备";
            this.btnViewDCT.Visible = false;
            this.btnViewDCT.Click += new System.EventHandler(this.btnViewDCT_Click);
            // 
            // btnStartDCT
            // 
            this.btnStartDCT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartDCT.Location = new System.Drawing.Point(80, 456);
            this.btnStartDCT.Name = "btnStartDCT";
            this.btnStartDCT.Size = new System.Drawing.Size(75, 23);
            this.btnStartDCT.TabIndex = 3;
            this.btnStartDCT.Text = "连接设备";
            this.btnStartDCT.Visible = false;
            this.btnStartDCT.Click += new System.EventHandler(this.btnStartDCT_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(528, 464);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "查找设备";
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(192, 464);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(100, 20);
            this.txtIP.TabIndex = 6;
            this.txtIP.Visible = false;
            // 
            // txtRes
            // 
            this.txtRes.Location = new System.Drawing.Point(352, 464);
            this.txtRes.Name = "txtRes";
            this.txtRes.Size = new System.Drawing.Size(100, 20);
            this.txtRes.TabIndex = 7;
            this.txtRes.Visible = false;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(144, 464);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(41, 13);
            this.lblIP.TabIndex = 8;
            this.lblIP.Text = "IP地址";
            this.lblIP.Visible = false;
            // 
            // lblRes
            // 
            this.lblRes.AutoSize = true;
            this.lblRes.Location = new System.Drawing.Point(296, 464);
            this.lblRes.Name = "lblRes";
            this.lblRes.Size = new System.Drawing.Size(55, 13);
            this.lblRes.TabIndex = 9;
            this.lblRes.Text = "资源代码";
            this.lblRes.Visible = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenu = this.contextMenu1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Guru Mes DCT SERVER";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenu1
            // 
            this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "重新连接";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // chkAutoRun
            // 
            this.chkAutoRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAutoRun.Location = new System.Drawing.Point(0, 480);
            this.chkAutoRun.Name = "chkAutoRun";
            this.chkAutoRun.Size = new System.Drawing.Size(136, 24);
            this.chkAutoRun.TabIndex = 10;
            this.chkAutoRun.Text = "系统启动时自动启动";
            this.chkAutoRun.Visible = false;
            this.chkAutoRun.CheckedChanged += new System.EventHandler(this.chkAutoRun_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(0, 456);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Visible = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 480);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(690, 23);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(147, 18);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(147, 18);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(147, 18);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // frmDCTServer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(690, 503);
            this.ControlBox = false;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.chkAutoRun);
            this.Controls.Add(this.lblRes);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.txtRes);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnStartDCT);
            this.Controls.Add(this.btnViewDCT);
            this.Controls.Add(this.lstDCTLient);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDCTServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DCT Server Version 1.0";
            this.Load += new System.EventHandler(this.frmDCTServer_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmDCTServer_Closing);
            this.Resize += new System.EventHandler(this.frmDCTServer_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public static bool IsRS485
        {
            get
            {
                return (System.Configuration.ConfigurationSettings.AppSettings["RS485"] == "1");
            }
        }

        private void frmDCTServer_Load(object sender, System.EventArgs e)
        {
            //added by hi1/leon.yuan 2009/1/8 for setting environment variable "NLS_Lang" in current process. 
            //To synchronize NLS_Lang between Oracle DB & CS Client
            SystemSettingFacade sysFacade = new SystemSettingFacade(this.DataProvider);
            Parameter param = (Parameter)sysFacade.GetParameter("NLS_LANG_Code", "NLS_LANG");
            if (param != null)
            { Environment.SetEnvironmentVariable("NLS_LANG", param.ParameterAlias); }
            //end added by hi1/leon.yuan 2009/1/8

            allowClose = true;
            timer2_Tick(sender, e);

            int interval = 30 * 1000;
            try
            {
                interval = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["AuotRefreshInterval"]) * 1000;
            }
            catch { }

            this.timer1.Interval = interval;

            //Add by Scott Gu 
            //For error "Cross-thread operation not valid: Control 'lstDCTLient' accessed from a thread other than the thread it was created on."
            Control.CheckForIllegalCrossThreadCalls = false;

            if (IsRS485 == true)
                LoadDCT_RS485(null);
            else
                LoadDCT(null);
            this.Hide();

            // Added by Icyer 2006/12/18
            string Log4NetConfigFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), configFile);
            if (File.Exists(Log4NetConfigFile))
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    DOMConfigurator.ConfigureAndWatch(new FileInfo(Log4NetConfigFile));
                }
                else
                {
                    DOMConfigurator.Configure(new FileInfo(Log4NetConfigFile));
                }
            }
            else
            {
                BasicConfigurator.Configure();
            }
            Logger = LogManager.GetLogger(this.GetType());
            // Added end
        }

        private void LoadDCT(object[] objs)
        {
            try
            {
                //根据配置文件，抓取DCT范围
                if (System.Configuration.ConfigurationSettings.AppSettings["AddressRage"] != null)
                {
                    //获取IP地址
                    System.Net.IPAddress ipStart = null;
                    System.Net.IPAddress ipEnd = null;
                    System.Net.IPAddress ipCurrent = null;
                    int iPort = 55962;

                    string ipConfig = System.Configuration.ConfigurationSettings.AppSettings["AddressRage"].Trim();
                    string[] ipRanges = ipConfig.Split(new char[] { ';' });

                    foreach (string ipRange in ipRanges)
                    {
                        string[] ipCovers = ipRange.Split(new char[] { '~' });

                        //获取开始结束IP范围
                        if (ipCovers.Length > 1)
                        {
                            ipStart = IPAddress.Parse(ipCovers[0].Split(new char[] { ':' })[0]);
                            ipEnd = IPAddress.Parse(ipCovers[1].Split(new char[] { ':' })[0]);
                            if (ipCovers[1].Split(new char[] { ':' }).Length > 1)
                            {
                                iPort = Convert.ToInt32(ipCovers[1].Split(new char[] { ':' })[1]);
                            }
                        }
                        else
                        {
                            ipStart = IPAddress.Parse(ipCovers[0].Split(new char[] { ':' })[0]);
                            ipEnd = IPAddress.Parse(ipCovers[0].Split(new char[] { ':' })[0]);
                            if (ipCovers[0].Split(new char[] { ':' }).Length > 1)
                            {
                                iPort = Convert.ToInt32(ipCovers[0].Split(new char[] { ':' })[1]);
                            }
                        }

                        byte[] btIPStart = ipStart.GetAddressBytes();
                        byte[] btIPEnd = ipEnd.GetAddressBytes();
                        byte[] btIPCurrent = new byte[4];
                        btIPStart.CopyTo(btIPCurrent, 0);

                        //循环启动DCT Client
                        for (int i = (int)btIPStart[3] - 1; i < (int)btIPEnd[3]; i++)
                        {
                            //生成新的IP
                            int ip = i + 1;
                            btIPCurrent[3] = (byte)ip;
                            string curIP = ipStart.ToString();
                            int iIP = ipStart.ToString().LastIndexOf(".");
                            string newIP = curIP.Substring(0, iIP);
                            newIP = newIP + "." + ip.ToString();
                            ipCurrent = IPAddress.Parse(newIP);

                            //启动DCT Client
                            IDCTClient client = null;
                            BaseDCTDriver driver = null;
                            if (objs != null && objs.Length > 0)
                            {
                                StartDCT(driver, client, newIP, iPort, objs);
                            }
                            else
                            {
                                StartDCT(driver, client, newIP, iPort, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        // Added by Icyer 2006/12/11
        private void LoadDCT_RS485(object[] objs)
        {
            try
            {
                short ret = ATop.DSC19.Library.GW21API.AB_API_Open();
                if (ret < 0)
                    return;
                short iNodeCount = ATop.DSC19.Library.GW21API.AB_GW_Cnt();
                for (short i = 0; i < iNodeCount; i++)
                {
                    int gateway_Id = 0, port = 0;
                    byte[] ip = new byte[20];
                    if (System.Configuration.ConfigurationSettings.AppSettings["TestMode"] != "1" || objs == null)
                    {
                        ret = ATop.DSC19.Library.GW21API.AB_GW_Conf(i, ref gateway_Id, ref ip[0], ref port);
                    }
                    else
                    {
                        ret = -1;
                    }
                    if (ret >= 0)
                    {
                        string newIP = System.Text.ASCIIEncoding.ASCII.GetString(ip);
                        newIP = newIP.Replace("\0", "");
                        IDCTClient client = null;
                        BaseDCTDriver driver = null;
                        if (objs != null && objs.Length > 0)
                        {
                            StartDCT(driver, client, newIP, port, objs, gateway_Id);
                        }
                        else
                        {
                            StartDCT(driver, client, newIP, port, null, gateway_Id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        // Added end

        private void frmDCTServer_Resize(object sender, System.EventArgs e)
        {
            lstDCTLient.Height = Convert.ToInt32(this.Height * 0.85);
        }

        private void StartDCT(BaseDCTDriver driver, IDCTClient client, string newIP, int port, object[] obj)
        {
            StartDCT(driver, client, newIP, port, obj, -1);
        }

        private void StartDCT(BaseDCTDriver driver, IDCTClient client, string newIP, int port, object[] obj, int clientId)
        {
            try
            {
                //Create DCT Client
                client = DCTFactory.CreateDCTClient(newIP, port);
                if (client is ATop.DSC19.DSC19Client)
                {
                    ((ATop.DSC19.DSC19Client)client).NodeID = clientId;
                }

                //Create DCT Driver
                driver = DCTFactory.CreateDCTDriver(newIP, port);

                if (client != null)
                {
                    //判断DCT Client是否已经存在
                    bool bExist = false;
                    BaseDCTDriver tmpDriver = null;
                    foreach (IDCTClient cl in _ClientList.Keys)
                    {
                        if (cl.ClientAddress == client.ClientAddress && cl.ClientPort == client.ClientPort && cl.ClientID == client.ClientID)
                        {
                            tmpDriver = _ClientList[cl] as BaseDCTDriver;
                            bExist = true;
                            break;
                        }
                    }

                    //新发现的DCT Client
                    if (!bExist)
                    {
                        driver.DCTClient = client;

                        //添加到Form上的显示
                        ListViewItem lvi = new ListViewItem(client.ClientAddress, 2);
                        lvi.Tag = client;
                        lock (lstDCTLient)
                        {
                            lstDCTLient.Items.Add(lvi);
                        }

                        //添加到后台的纪录ArrayList
                        lock (_ClientList)
                        {
                            _ClientList.Add(client, driver);
                        }

                        if (client is ATop.DSC19.DSC19Client)
                        {
                        }
                        else
                        {
                            client.OnSendData += new EventCommandHandler(client_OnSendData);
                        }
                    }

                    //已有的DCT Client
                    else
                    {
                        try
                        {
                            if (tmpDriver.DCTClient.IsAlive() && obj != null && obj.Length > 0)
                            {
                                return;
                            }
                            else
                            {
                                if (tmpDriver.DCTClient != null)
                                {
                                    try
                                    {
                                        tmpDriver.DCTClient.Close();
                                    }
                                    catch
                                    {
                                    }
                                    _ClientList.Remove(tmpDriver.DCTClient);
                                }
                                driver.DCTClient = client;
                            }
                        }
                        catch
                        {
                            driver.DCTClient = client;
                        }

                        lock (_ClientList)
                        {
                            _ClientList[client] = driver;
                        }
                    }

                    //添加事件处理
                    (client as IDCTClient).OnError += new EventCommandHandler(frmDCTServer_OnError);
                    (client as IDCTClient).AfterLogin += new EventCommandHandler(frmDCTServer_AfterLogin);
                    driver.AfterLogout += new EventCommandHandler(driver_AfterLogout);
                    if (client is ATop.DSC19.DSC19Client)
                    {
                        ((ATop.DSC19.DSC19Client)client).OnTerminalConnect += new EventCommandHandler(frmDCTServer_OnTerminalConnect);
                    }

                    //启动DCT Client，Driver的DCTListen为入口
                    if (!bExist)
                    {
                        ThreadStart ts = new ThreadStart(driver.DCTListen);
                        Thread th = new Thread(ts);
                        th.IsBackground = true;
                        th.Priority = ThreadPriority.AboveNormal;

                        th.Start();

                        lock (_ThreadList)
                        {
                            _ThreadList.Add(client.ClientAddress, th);
                        }
                    }
                    else
                    {
                        ThreadStart ts = new ThreadStart(driver.DCTListen);
                        Thread th = new Thread(ts);
                        th.Start();

                        lock (_ThreadList)
                        {
                            if (_ThreadList[client.ClientAddress] == null)
                                _ThreadList.Add(client.ClientAddress, th);
                            else
                                _ThreadList[client.ClientAddress] = th;
                        }

                        ListViewItem lvi = null;
                        if (client.Authorized == true)
                        {
                            lvi = new ListViewItem(client.ClientAddress, 0);
                            lvi.Tag = client;
                            lvi.Text = client.ResourceCode;
                        }
                        else
                        {
                            lvi = new ListViewItem(client.ClientAddress, 2);
                            lvi.Tag = client;
                            lvi.Text = client.ClientAddress;
                        }

                        for (int i = 0; i < lstDCTLient.Items.Count; i++)
                        {
                            if (((lstDCTLient.Items[i].Tag as IDCTClient).ClientAddress == client.ClientAddress
                                && (lstDCTLient.Items[i].Tag as IDCTClient).ClientPort == client.ClientPort
                                && (lstDCTLient.Items[i].Tag as IDCTClient).ClientID == client.ClientID))
                            {
                                lock (lstDCTLient)
                                {
                                    lstDCTLient.Items.RemoveAt(i);
                                }
                                break;
                            }
                        }

                        lock (lstDCTLient)
                        {
                            lstDCTLient.Items.Add(lvi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ListViewItem lvi = new ListViewItem(client.ClientAddress, 1);
                lvi.Tag = client;

                lock (lstDCTLient)
                {
                    lstDCTLient.Items.Add(lvi);
                }

                Log.Error(ex.Message);
            }
        }

        private string frmDCTServer_OnError(object sender, CommandEventArgs args)
        {
            if (sender != null)
            {
                foreach (ListViewItem lvi in lstDCTLient.Items)
                {
                    if (lvi.Text == (sender as IDCTClient).ClientAddress
                        || lvi.Text == (sender as IDCTClient).ResourceCode)
                    {
                        lvi.ImageIndex = 1;

                        break;
                    }
                }
                if (_ThreadList.ContainsKey((sender as IDCTClient).ClientAddress))
                {
                    try
                    {
                        (_ThreadList[(sender as IDCTClient).ClientAddress] as Thread).Abort();
                    }
                    catch
                    {
                    }
                }
            }
            return null;
        }

        private string frmDCTServer_AfterLogin(object sender, CommandEventArgs args)
        {
            if (sender != null)
            {
                //通过BeginInvke的方式供后台线程调用	
                lstDCTLient.BeginInvoke(new EventHandler(ListViewUpdateUI_AfterLogin), sender, args);

                /*Joe 2007-09-03 在后台线程调用时会报错，将其移到方法 ListViewUpdateUI_AfterLogin 中
                foreach (ListViewItem lvi in lstDCTLient.Items)
                {
                    
                    if ((lvi.Tag as IDCTClient).ClientAddress == (sender as IDCTClient).ClientAddress
                        && (lvi.Tag as IDCTClient).ClientPort == (sender as IDCTClient).ClientPort
                        && (lvi.Tag as IDCTClient).ClientID == (sender as IDCTClient).ClientID)
                    {
                        lvi.ImageIndex = 0;

                        lvi.Text = (sender as IDCTClient).ResourceCode;
                    }
                }*/

            }
            return null;
        }

        private void ListViewUpdateUI_AfterLogin(object sender, System.EventArgs e)
        {
            foreach (ListViewItem lvi in lstDCTLient.Items)
            {
                if ((lvi.Tag as IDCTClient).ClientAddress == (sender as IDCTClient).ClientAddress
                    && (lvi.Tag as IDCTClient).ClientPort == (sender as IDCTClient).ClientPort
                    && (lvi.Tag as IDCTClient).ClientID == (sender as IDCTClient).ClientID)
                {
                    lvi.ImageIndex = 0;

                    lvi.Text = (sender as IDCTClient).ResourceCode;
                }
            }
        }

        private string driver_AfterLogout(object sender, CommandEventArgs args)
        {
            if (sender != null)
            {
                foreach (ListViewItem lvi in lstDCTLient.Items)
                {
                    if (lvi.Tag == (sender as IDCTDriver).DCTClient)
                    {
                        lvi.ImageIndex = 1;

                        lvi.Text = (sender as IDCTDriver).DCTClient.ClientAddress;
                        break;
                    }
                }
            }
            return null;
        }

        // Added by Icyer 2006/12/14
        // 当连上终端时的事件
        private string frmDCTServer_OnTerminalConnect(object sender, CommandEventArgs args)
        {
            if (sender is ATop.DSC19.DSC19DCTDriver)
            {
                ATop.DSC19.DSC19Client client = ((ATop.DSC19.DSC19DCTDriver)sender).DCTClient as ATop.DSC19.DSC19Client;
                // 在ListView中添加图标
                ListViewItem lvi = null;
                if (client.Authorized == true)
                {
                    lvi = new ListViewItem(client.ClientAddress, 0);
                    lvi.Tag = client;
                    lvi.Text = client.ResourceCode;
                }
                else
                {
                    lvi = new ListViewItem(client.ClientAddress, 2);
                    lvi.Tag = client;
                    lvi.Text = client.ClientAddress;
                }
                for (int i = 0; i < lstDCTLient.Items.Count; i++)
                {
                    if (((lstDCTLient.Items[i].Tag as IDCTClient).ClientAddress == client.ClientAddress
                        && (lstDCTLient.Items[i].Tag as IDCTClient).ClientPort == client.ClientPort
                        && (lstDCTLient.Items[i].Tag as IDCTClient).ClientID == client.ClientID))
                    {
                        lock (lstDCTLient)
                        {
                            lstDCTLient.Items.RemoveAt(i);
                        }
                        break;
                    }
                }
                lock (lstDCTLient)
                {
                    lstDCTLient.Items.Add(lvi);
                }

                // 添加事件
                client.OnError += new EventCommandHandler(frmDCTServer_OnError);
                client.AfterLogin += new EventCommandHandler(frmDCTServer_AfterLogin);
                client.OnSendData += new EventCommandHandler(client_OnSendData);

                ((ATop.DSC19.DSC19DCTDriver)sender).AfterLogout += new EventCommandHandler(driver_AfterLogout);
            }
            return string.Empty;
        }

        private string LogOutput = System.Configuration.ConfigurationSettings.AppSettings["LogOutput"];
        private Hashtable htPrevLogTime = null;

        private string client_OnSendData(object sender, CommandEventArgs args)
        {
            try
            {
                if (LogOutput == "1")
                {
                    string strMsg = args.Message;
                    if (strMsg.EndsWith("\r\n") == true)
                        strMsg = strMsg.TrimEnd('\r', '\n');
                    IDCTClient client = sender as IDCTClient;
                    if (htPrevLogTime == null)
                        htPrevLogTime = new Hashtable();
                    string strKey = client.ClientAddress + ":" + client.ClientPort.ToString() + "-" + client.ClientID.ToString() + "\t" + client.ResourceCode;
                    if (htPrevLogTime.ContainsKey(strKey) == false)
                    {
                        htPrevLogTime.Add(strKey, new object[] { "", DateTime.MinValue });
                    }
                    string strCost = "0";
                    if (args.Message.IndexOf(" IN ") > 0)	// 用户输入
                    {
                        object[] objTmp = (object[])htPrevLogTime[strKey];
                        objTmp[0] = "IN";
                        objTmp[1] = DateTime.Now;
                        htPrevLogTime[strKey] = objTmp;
                    }
                    else if (args.Message.IndexOf(" OUT ") > 0)		// 系统输出
                    {
                        object[] objTmp = (object[])htPrevLogTime[strKey];
                        DateTime dt = (DateTime)objTmp[1];
                        if (dt != DateTime.MinValue)
                        {
                            TimeSpan ts = DateTime.Now - dt;
                            if (ts.TotalMilliseconds > 0)
                            {
                                strCost = ts.TotalMilliseconds.ToString();
                            }
                        }
                    }
                    strMsg = strKey + "\t" + strCost + "\t" + strMsg;
                    Logger.Debug(strMsg);
                }
                return args.Message;
            }
            catch
            {
                if (args != null)
                    return args.Message;
                return string.Empty;
            }
        }
        // Added end

        #region 窗体按钮事件
        private void btnStartDCT_Click(object sender, System.EventArgs e)
        {
            foreach (IDCTClient client in _ClientList.Keys)
            {
                if (lstDCTLient.SelectedItems.Count < 0)
                {
                    break;
                }
                if (client.ClientAddress == lstDCTLient.SelectedItems[0].Text/*
					|| client.ResourceCode == lstDCTLient.SelectedItems[0].Text*/
                                                                                 )
                {
                    (client as IDCTClient).OnError -= new EventCommandHandler(frmDCTServer_OnError);
                    (client as IDCTClient).AfterLogin -= new EventCommandHandler(frmDCTServer_AfterLogin);
                    (_ClientList[client] as IDCTDriver).AfterLogout -= new EventCommandHandler(driver_AfterLogout);

                    BaseDCTDriver driver = null;
                    if (client.Type == DCTType.GW28)
                    {
                        driver = new GW28DCTDriver();
                        driver.DCTClient = new GW28Client(client.ClientAddress, client.ClientPort);

                        StartDCT(driver, driver.DCTClient, client.ClientAddress, client.ClientPort, null);


                        break;
                    }

                    if (client.Type == DCTType.DT4000)
                    {
                        driver = new DT4000DCTDriver();
                        driver.DCTClient = new DT4000Client(client.ClientAddress, client.ClientPort);

                        StartDCT(driver, driver.DCTClient, client.ClientAddress, client.ClientPort, null);


                        break;
                    }
                    if (client.Type == DCTType.DCN500)
                    {
                        driver = new DCN500DCTDriver();
                        driver.DCTClient = new DCN500Client(client.ClientAddress, client.ClientPort);

                        StartDCT(driver, driver.DCTClient, client.ClientAddress, client.ClientPort, null);


                        break;
                    }

                }
            }
        }
        #endregion

        private void btnViewDCT_Click(object sender, System.EventArgs e)
        {
            foreach (IDCTClient client in _ClientList.Keys)
            {
                if (lstDCTLient.SelectedItems.Count > 0)
                {
                    if (lstDCTLient.SelectedItems[0].Tag == client)
                    {
                        frmDCTMonitor monitor = new frmDCTMonitor(client);
                        monitor.Text = client.ClientAddress + "\t" + client.ResourceCode;

                        client.OnSendData += new EventCommandHandler(monitor.SendData);

                        monitor.ShowDialog();
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem lvi in lstDCTLient.Items)
            {
                if (lvi.Text.Trim() == txtIP.Text.ToUpper().Trim()
                    || lvi.Text.Trim() == txtRes.Text.ToUpper().Trim())
                {
                    lvi.Selected = true;
                    //lvi.
                    break;
                }
            }
        }

        private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void frmDCTServer_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (allowClose != true)
            {
                e.Cancel = true;

                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        private void menuItem1_Click(object sender, System.EventArgs e)
        {
            this.notifyIcon1.Visible = false;
            allowClose = true;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            ShutDownThread();
            this.Close();
            Application.ExitThread();
        }

        private void ShutDownThread()
        {
            foreach (System.Threading.Thread th in _ThreadList.Values)
            {
                try
                {
                    th.Abort();
                }
                catch { }

            }
        }

        private void chkAutoRun_CheckedChanged(object sender, System.EventArgs e)
        {
            //			this.Cursor = Cursors.WaitCursor;
            //			if(chkAutoRun.Checked == true)
            //			{
            //				Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",true);
            //				//ArrayList alRun = new ArrayList();
            //				//alRun.AddRange(regKey.GetValueNames());
            //				//if(!alRun.Contains(dctServer))
            //				//{
            //				regKey.SetValue(Working.dctServer,Application.ExecutablePath);
            //				regKey.Flush();
            //				//}
            //
            //				regKey.Close();
            //			}
            //			else
            //			{
            //				Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",true);
            //				
            //				regKey.DeleteSubKey(Working.dctServer);
            //				regKey.Flush();
            //
            //				regKey.Close();
            //			}
            //			this.Cursor = Cursors.Default;
        }

        public static object CheckUpdate()
        {
            BenQGuru.eMES.Common.Domain.IDomainDataProvider _dataProvider =
                BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();

            try
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_dataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_dataProvider).PersistBroker.OpenConnection();
                //object objUpdater = null;
                //Laws Lu,2005/08/22,新增	版本更新
                object objUpdater = FormatHelper.GetCsVersion(_dataProvider);

                string strVersion = UserControl.FileLog.GetLocalCSVersion(UserControl.FileLog.VersionFileName);
                int iErrorCount = 0;
                if (objUpdater != null)
                {
                    iErrorCount = FormatHelper.GetUpdateErrorCount(_dataProvider, ((BenQGuru.eMES.Web.Helper.Updater)objUpdater).CSVersion);
                }
                //Laws Lu,2006/08/14 修改	如果更新有错误也不允许继续操作
                if ((objUpdater != null
                    && strVersion != ((BenQGuru.eMES.Web.Helper.Updater)objUpdater).CSVersion.Trim())
                    || (objUpdater != null && iErrorCount != 0
                    && strVersion == ((BenQGuru.eMES.Web.Helper.Updater)objUpdater).CSVersion.Trim()))
                {
                    return objUpdater;
                    //				bResult = true;
                    //				Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
                    //				
                    //				AutoUpdate(upd.Location.Trim(),upd.LoginUser,upd.LoginPassword);
                }
            }
            catch (Exception ex)
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

        object objUpdater = null;

        private void AutoUpdate()
        {
            if ((System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"] != null
                && System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"].Trim() != String.Empty))
            {
                string configHour = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"].Trim().Substring(0, 2)).ToString();
                string configTime = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"].Trim().Substring(3, 2)).ToString();

                if (configHour == DateTime.Now.Hour.ToString() &&
                    configTime == DateTime.Now.Minute.ToString() && iUpdaterAlertCount == 0)
                {
                    objUpdater = CheckUpdate();
                    if (objUpdater != null)
                    {
                        iUpdaterAlertCount = Convert.ToInt32(4 * timer1.Interval);

                        //						ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error,
                        //							"$CS_NEW_VERSION_AVIALABLE_PLS_LOGOUT " + Convert.ToInt32(iUpdaterAlertCount/timer1.Interval)));


                        //iUpdaterAlertCount = 60;
                        //							System.Threading.Thread.Sleep(30000);
                        //							//超出预警次数，自动退出系统
                        //						
                        //							this.Close();
                        //							Application.Exit();
                    }
                }

                //this.//				Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
                //				ApplicationService.AutoUpdate(upd.Location.Trim(),upd.LoginUser,upd.LoginPassword);
            }
            if (iUpdaterAlertCount > 0)
            {
                iUpdaterAlertCount -= Convert.ToInt32(1 * timer1.Interval);
                //				ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error,
                //					"$CS_NEW_VERSION_AVIALABLE_PLS_LOGOUT " + Convert.ToInt32(iUpdaterAlertCount/timer1.Interval)));

                if (iUpdaterAlertCount == 0)
                {
                    if (objUpdater != null)
                    {
                        Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
                        AutoUpdate(upd.Location.Trim(), upd.LoginUser, upd.LoginPassword);
                    }
                    //					this.Close();
                    //					Application.Exit();
                }

            }
        }

        public const string MesAgent = "BenQGuru.eMES.Agent.exe";
        public const string DCTServer = "DCTControlPanel.exe";

        public void AutoUpdate(string location, string user, string password)
        {
            System.Diagnostics.Process pr = new System.Diagnostics.Process();

            System.Diagnostics.ProcessStartInfo prStart = new System.Diagnostics.ProcessStartInfo();

            prStart.FileName = "AutoUpdate.exe";

            prStart.Arguments = location + " " + AppDomain.CurrentDomain.FriendlyName;

            pr.StartInfo = prStart;

            System.Diagnostics.Process prAgent = RunningInstance(MesAgent);

            System.Diagnostics.Process prDCT = RunningInstance(DCTServer);

            pr.Start();

            if (prAgent != null)
            {
                prAgent.Kill();
            }

            if (prDCT != null)
            {
                prDCT.Kill();
            }
            Application.Exit();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            AutoUpdate();

            if (IsRS485 == true)
                LoadDCT_RS485(new object[] { "OK" });
            else
                LoadDCT(new object[] { "OK" });
            //LoadDCT(new object[]{"OK"});
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            if (IsRS485 == true)
                LoadDCT_RS485(new object[] { "OK" });
            else
                LoadDCT(new object[] { "OK" });
            //LoadDCT(new object[]{"OK"});
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime nowTime = DateTime.Now;
            toolStripStatusLabel1.Text = "当前时间 " + nowTime.ToString("yyyy/MM/dd hh:mm");
            toolStripStatusLabel2.Text = "启动时间 " + _StartDateTime.ToString("yyyy/MM/dd hh:mm");

            TimeSpan runTime = nowTime - _StartDateTime;
            toolStripStatusLabel3.Text = "已经运行 " + string.Format("{0}天{1}小时{2}分", runTime.Days, runTime.Hours, runTime.Minutes);
        }

        private void lstDCTLient_Click(object sender, EventArgs e)
        {
            if (lstDCTLient.SelectedItems != null)
            {
                lstDCTLient.ContextMenu = this.contextMenu1;
            }
        }

        private void lstDCTLient_MouseDown(object sender, MouseEventArgs e)
        {
            lstDCTLient.ContextMenu = null;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (lstDCTLient.SelectedItems != null)
            {
                foreach (ListViewItem item in lstDCTLient.SelectedItems)
                {
                    if (item.Tag != null)
                        ReconnectDCTCliet(item);
                }
            }

        }

        private void ReconnectDCTCliet(ListViewItem item)
        {
            IDCTClient oldClient = (IDCTClient)item.Tag;

            if (oldClient == null)
                return;

            try
            {
                //Create DCT Client
                IDCTClient client = DCTFactory.CreateDCTClient(oldClient.ClientAddress, oldClient.ClientPort);
                if (client is ATop.DSC19.DSC19Client)
                {
                    ((ATop.DSC19.DSC19Client)client).NodeID = oldClient.ClientID;
                }

                //Create DCT Driver
                BaseDCTDriver driver = DCTFactory.CreateDCTDriver(oldClient.ClientAddress, oldClient.ClientPort);
                driver.DCTClient = client;

                //添加事件处理
                if (client is ATop.DSC19.DSC19Client)
                {
                    ((ATop.DSC19.DSC19Client)client).OnTerminalConnect += new EventCommandHandler(frmDCTServer_OnTerminalConnect);
                }
                else
                {
                    client.OnSendData += new EventCommandHandler(client_OnSendData);
                }
                client.OnError += new EventCommandHandler(frmDCTServer_OnError);
                client.AfterLogin += new EventCommandHandler(frmDCTServer_AfterLogin);
                driver.AfterLogout += new EventCommandHandler(driver_AfterLogout);

                //清理旧线程和几个全局变量
                try
                {
                    oldClient.Close();
                }
                catch { }
                lock (_ThreadList.SyncRoot)
                {
                    if ((Thread)_ThreadList[oldClient.ClientAddress] != null)
                    {
                        ((Thread)_ThreadList[oldClient.ClientAddress]).Abort();
                        _ThreadList.Remove(oldClient.ClientAddress);
                    }
                }
                lock (_ClientList.SyncRoot)
                {
                    foreach (IDCTClient cl in _ClientList.Keys)
                    {
                        if (cl.ClientAddress == oldClient.ClientAddress
                            && cl.ClientPort == oldClient.ClientPort
                            && cl.ClientID == oldClient.ClientID)
                        {
                            _ClientList.Remove(cl);
                            break;
                        }
                    }
                }

                //启动线程
                ThreadStart ts = new ThreadStart(driver.DCTListen);
                Thread th = new Thread(ts);
                th.IsBackground = true;
                th.Priority = ThreadPriority.AboveNormal;
                th.Start();

                //记录到几个全局变量
                lock (_ClientList.SyncRoot)
                {
                    _ClientList.Add(client, driver);
                }

                lock (_ThreadList.SyncRoot)
                {
                    _ThreadList.Add(client.ClientAddress, th);
                }

                //重置ListView的显示
                item.ImageIndex = 2;
                item.Tag = client;
            }

            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
