using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// Form1 的摘要说明。
    /// Laws Lu,2005/08/01,修改	MessageForm移动到主窗体
    /// </summary>
    public class FMain : BaseForm
    {
        private Infragistics.Win.UltraWinStatusBar.UltraStatusBar ultraStatusBar;
        private Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager ultraTabbedMdiMain;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        public Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _FMain_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _FMain_Toolbars_Dock_Area_Bottom;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _FMain_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _FMain_Toolbars_Dock_Area_Right;
        private System.Windows.Forms.Panel panel1;
        //Amoi,Laws Lu,2005/08/01,添加	MessageForm移动到主窗体
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labUser;
        private System.ComponentModel.IContainer components;
        private System.Timers.Timer timer1;
        private System.Windows.Forms.Label lblUpdateNow;
        public  System.Windows.Forms.PictureBox pictureBox2;
        private Label labelOrganization;
        public  PictureBox pictureBox4;

        private System.Windows.Forms.MdiClient m_MdiClient;
        private TableLayoutPanel tableLayoutInfo;
        private Label labelChgPwd;
        private Label labelHelp;
        private Label labelLogOut;
        private Splitter splitter1;
        public Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar ultraExplorerBar;



        int iUpdaterAlertCount = 0;
        //		private IDomainDataProvider _domainDataProvider =ApplicationService.Current().DataProvider;
        //		public IDomainDataProvider DataProvider
        //		{
        //			get
        //			{
        //				return _domainDataProvider;
        //			}
        //		}

        public FMain()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            //added by carey.cheng on 2010-05-17 for modify the mdi form backcolor
            foreach (Control item in this.Controls)
            {
                if (item.GetType().ToString().Equals("System.Windows.Forms.MdiClient"))
                {
                    this.m_MdiClient = item as MdiClient;
                    break;
                }
            }
            this.m_MdiClient.BackColor = Color.FromArgb(244, 244, 244);
            //End added by carey.cheng on 2010-05-17 for modify the mdi form backcolor


            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //

            //ApplicationService.Current().MenuService.MergeMainMenu(this.mainMenu); 
            ApplicationService.Current().MainWindows = this;


            Infragistics.Win.UltraWinToolbars.UltraToolbar mainMenuBar = ultraToolbarsManager.Toolbars["MainMenuBar"];
            ApplicationService.Current().MenuService.MergeUltraWinMainMenu(mainMenuBar);
            //ApplicationService.Current().MenuService.MergeMainMenu(mainMenu);

            //Amoi,Laws Lu,2005/08/01,添加	确定infoForm的位置，初始化InfoForm
            ultraExplorerBar.Height = Convert.ToInt32(panel1.Height * 0.6);
            panel4.Height = panel1.Height - Convert.ToInt32(panel1.Height * 0.6);
            ApplicationRun.GetInfoForm();

            /*
            // Added By Hi1/Venus.Feng on 20080629 for Hisense Version : Add Org ID
            if (GlobalVariables.CurrentOrganizations.GetOrganizationList().Count == 0)
            {
                this.labelOrganization.Visible = false;
            }
            else
            {
                this.labelOrganization.Text = UserControl.MutiLanguages.ParserString("$PageControl_Organization") + GlobalVariables.CurrentOrganizations.First().OrganizationDescription;
                
                //modified by carey.cheng on 2010-05-12 for KN
                //to mark organization
                //this.labelOrganization.Visible = true;
                this.labelOrganization.Visible = false;
                //end modified

            }
            // End


            //			if(ApplicationService.CheckUpdate() != null)
            //			{
            //				ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error,
            //					"$CS_NEW_VERSION_AVIALABLE_PLS_LOGOUT "));
            //			}
            //EndAmoi

            //FLogin fl=new FLogin();
            //fl.ShowDialog();
             */

            //Added by carey.cheng on 2010-05-18 for new login mode
            this.panel1.Visible = false;
            this.panel3.Visible = false;
            this.panel4.Visible = false;

            FLogin login = new FLogin();
            login.MdiParent = this;
            login.Show();
            //End Added by carey.cheng on 2010-05-18 for new login mode

            this.InitPageLanguage();

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

        protected void ShowMessage(string message)
        {
            ///lablastMsg.Text =message;
            ApplicationRun.GetInfoForm().Add(message);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel6 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel7 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel8 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel9 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel10 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("MainMenuBar");
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            this.ultraStatusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
            this.ultraTabbedMdiMain = new Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutInfo = new System.Windows.Forms.TableLayoutPanel();
            this.labelChgPwd = new System.Windows.Forms.Label();
            this.labelHelp = new System.Windows.Forms.Label();
            this.labelLogOut = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblUpdateNow = new System.Windows.Forms.Label();
            this.labUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelOrganization = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this._FMain_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.ultraToolbarsManager = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._FMain_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._FMain_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._FMain_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ultraExplorerBar = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.timer1 = new System.Timers.Timer();
            this.splitter1 = new System.Windows.Forms.Splitter();
            ((System.ComponentModel.ISupportInitialize)(this.ultraStatusBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiMain)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraStatusBar
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            appearance1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(102)))), ((int)(((byte)(152)))));
            this.ultraStatusBar.Appearance = appearance1;
            this.ultraStatusBar.Location = new System.Drawing.Point(0, 451);
            this.ultraStatusBar.Name = "ultraStatusBar";
            ultraStatusPanel1.Key = "UserName";
            ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel1.Text = "用户名:";
            ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel3.Key = "ResCode";
            ultraStatusPanel3.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel3.Text = "资源信息:";
            ultraStatusPanel4.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel4.Width = 300;
            ultraStatusPanel5.Key = "CurrentVer";
            ultraStatusPanel5.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel5.Text = "当前版本:";
            ultraStatusPanel6.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel6.Width = 300;
            ultraStatusPanel7.Key = "LoginTime";
            ultraStatusPanel7.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel7.Text = "登陆时间:";
            ultraStatusPanel8.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel8.Width = 300;
            ultraStatusPanel9.Key = "DataBase";
            ultraStatusPanel9.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel9.Text = "数据库:";
            ultraStatusPanel10.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            this.ultraStatusBar.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[] {
            ultraStatusPanel1,
            ultraStatusPanel2,
            ultraStatusPanel3,
            ultraStatusPanel4,
            ultraStatusPanel5,
            ultraStatusPanel6,
            ultraStatusPanel7,
            ultraStatusPanel8,
            ultraStatusPanel9,
            ultraStatusPanel10});
            this.ultraStatusBar.Size = new System.Drawing.Size(652, 24);
            this.ultraStatusBar.TabIndex = 9;
            this.ultraStatusBar.Click += new System.EventHandler(this.ultraStatusBar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutInfo);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.lblUpdateNow);
            this.panel2.Controls.Add(this.labUser);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.labelOrganization);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(652, 50);
            this.panel2.TabIndex = 41;
            // 
            // tableLayoutInfo
            // 
            this.tableLayoutInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutInfo.BackgroundImage")));
            this.tableLayoutInfo.ColumnCount = 5;
            this.tableLayoutInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutInfo.Controls.Add(this.labelChgPwd, 1, 1);
            this.tableLayoutInfo.Controls.Add(this.labelHelp, 2, 1);
            this.tableLayoutInfo.Controls.Add(this.labelLogOut, 3, 1);
            this.tableLayoutInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutInfo.Location = new System.Drawing.Point(340, 0);
            this.tableLayoutInfo.Name = "tableLayoutInfo";
            this.tableLayoutInfo.RowCount = 3;
            this.tableLayoutInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutInfo.Size = new System.Drawing.Size(222, 50);
            this.tableLayoutInfo.TabIndex = 49;
            // 
            // labelChgPwd
            // 
            this.labelChgPwd.AutoSize = true;
            this.labelChgPwd.BackColor = System.Drawing.Color.Transparent;
            this.labelChgPwd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelChgPwd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelChgPwd.ForeColor = System.Drawing.Color.White;
            this.labelChgPwd.Location = new System.Drawing.Point(76, 22);
            this.labelChgPwd.Name = "labelChgPwd";
            this.labelChgPwd.Size = new System.Drawing.Size(53, 12);
            this.labelChgPwd.TabIndex = 0;
            this.labelChgPwd.Text = "更改密码";
            this.labelChgPwd.Click += new System.EventHandler(this.labelChgPwd_Click);
            // 
            // labelHelp
            // 
            this.labelHelp.AutoSize = true;
            this.labelHelp.BackColor = System.Drawing.Color.Transparent;
            this.labelHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelHelp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelHelp.ForeColor = System.Drawing.Color.White;
            this.labelHelp.Location = new System.Drawing.Point(135, 22);
            this.labelHelp.Name = "labelHelp";
            this.labelHelp.Size = new System.Drawing.Size(29, 12);
            this.labelHelp.TabIndex = 1;
            this.labelHelp.Text = "帮助";
            this.labelHelp.Click += new System.EventHandler(this.labelHelp_Click);
            // 
            // labelLogOut
            // 
            this.labelLogOut.AutoSize = true;
            this.labelLogOut.BackColor = System.Drawing.Color.Transparent;
            this.labelLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelLogOut.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLogOut.ForeColor = System.Drawing.Color.White;
            this.labelLogOut.Location = new System.Drawing.Point(170, 22);
            this.labelLogOut.Name = "labelLogOut";
            this.labelLogOut.Size = new System.Drawing.Size(29, 12);
            this.labelLogOut.TabIndex = 2;
            this.labelLogOut.Text = "登出";
            this.labelLogOut.Click += new System.EventHandler(this.labelLogOut_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(562, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(90, 50);
            this.pictureBox4.TabIndex = 48;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(340, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // lblUpdateNow
            // 
            this.lblUpdateNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUpdateNow.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdateNow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUpdateNow.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUpdateNow.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblUpdateNow.Location = new System.Drawing.Point(236, 3);
            this.lblUpdateNow.Name = "lblUpdateNow";
            this.lblUpdateNow.Size = new System.Drawing.Size(72, 23);
            this.lblUpdateNow.TabIndex = 3;
            this.lblUpdateNow.Text = "现在更新";
            this.lblUpdateNow.Visible = false;
            this.lblUpdateNow.Click += new System.EventHandler(this.lblUpdateNow_Click);
            // 
            // labUser
            // 
            this.labUser.AutoSize = true;
            this.labUser.BackColor = System.Drawing.Color.Transparent;
            this.labUser.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labUser.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labUser.Location = new System.Drawing.Point(235, 3);
            this.labUser.Name = "labUser";
            this.labUser.Size = new System.Drawing.Size(0, 14);
            this.labUser.TabIndex = 2;
            this.labUser.Visible = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(2, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "MES";
            this.label1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(652, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // labelOrganization
            // 
            this.labelOrganization.AutoSize = true;
            this.labelOrganization.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(52)))), ((int)(((byte)(156)))));
            this.labelOrganization.ForeColor = System.Drawing.Color.White;
            this.labelOrganization.Location = new System.Drawing.Point(187, 5);
            this.labelOrganization.Name = "labelOrganization";
            this.labelOrganization.Size = new System.Drawing.Size(77, 12);
            this.labelOrganization.TabIndex = 47;
            this.labelOrganization.Text = "您的组织为：";
            this.labelOrganization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelOrganization.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(215)))), ((int)(((byte)(237)))));
            this.panel3.Controls.Add(this._FMain_Toolbars_Dock_Area_Bottom);
            this.panel3.Controls.Add(this._FMain_Toolbars_Dock_Area_Left);
            this.panel3.Controls.Add(this._FMain_Toolbars_Dock_Area_Right);
            this.panel3.Controls.Add(this._FMain_Toolbars_Dock_Area_Top);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 50);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(652, 24);
            this.panel3.TabIndex = 42;
            // 
            // _FMain_Toolbars_Dock_Area_Bottom
            // 
            this._FMain_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FMain_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(215)))), ((int)(((byte)(237)))));
            this._FMain_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._FMain_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.Color.Black;
            this._FMain_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 24);
            this._FMain_Toolbars_Dock_Area_Bottom.Name = "_FMain_Toolbars_Dock_Area_Bottom";
            this._FMain_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(652, 0);
            this._FMain_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager;
            // 
            // ultraToolbarsManager
            // 
            appearance2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(215)))), ((int)(((byte)(237)))));
            appearance2.FontData.BoldAsString = "False";
            appearance2.ForeColor = System.Drawing.Color.Black;
            appearance2.TextVAlignAsString = "Top";
            this.ultraToolbarsManager.Appearance = appearance2;
            this.ultraToolbarsManager.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ultraToolbarsManager.DesignerFlags = 1;
            this.ultraToolbarsManager.DockWithinContainer = this;
            this.ultraToolbarsManager.DockWithinContainerBaseType = typeof(System.Windows.Forms.Form);
            this.ultraToolbarsManager.LockToolbars = true;
            this.ultraToolbarsManager.ShowFullMenusDelay = 500;
            this.ultraToolbarsManager.ShowMenuShadows = Infragistics.Win.DefaultableBoolean.False;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
            ultraToolbar1.Settings.BorderStyleDocked = Infragistics.Win.UIElementBorderStyle.None;
            appearance20.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance20.ImageBackground")));
            ultraToolbar1.Settings.FloatingAppearance = appearance20;
            appearance18.BorderColor = System.Drawing.Color.Transparent;
            appearance18.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance18.ImageBackground")));
            ultraToolbar1.Settings.HotTrackAppearance = appearance18;
            ultraToolbar1.Settings.PaddingLeft = 20;
            appearance21.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance21.ImageBackground")));
            ultraToolbar1.Settings.PressedAppearance = appearance21;
            this.ultraToolbarsManager.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            appearance15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(215)))), ((int)(((byte)(237)))));
            this.ultraToolbarsManager.ToolbarSettings.Appearance = appearance15;
            this.ultraToolbarsManager.ToolbarSettings.HotTrackAppearance = appearance2;
            // 
            // _FMain_Toolbars_Dock_Area_Left
            // 
            this._FMain_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FMain_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(215)))), ((int)(((byte)(237)))));
            this._FMain_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._FMain_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.Color.Black;
            this._FMain_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 17);
            this._FMain_Toolbars_Dock_Area_Left.Name = "_FMain_Toolbars_Dock_Area_Left";
            this._FMain_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 7);
            this._FMain_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager;
            // 
            // _FMain_Toolbars_Dock_Area_Right
            // 
            this._FMain_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FMain_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(215)))), ((int)(((byte)(237)))));
            this._FMain_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._FMain_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.Color.Black;
            this._FMain_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(652, 17);
            this._FMain_Toolbars_Dock_Area_Right.Name = "_FMain_Toolbars_Dock_Area_Right";
            this._FMain_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 7);
            this._FMain_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager;
            // 
            // _FMain_Toolbars_Dock_Area_Top
            // 
            this._FMain_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FMain_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(215)))), ((int)(((byte)(237)))));
            this._FMain_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._FMain_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.Color.Black;
            this._FMain_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._FMain_Toolbars_Dock_Area_Top.Name = "_FMain_Toolbars_Dock_Area_Top";
            this._FMain_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(652, 17);
            this._FMain_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.ultraExplorerBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 74);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(202, 377);
            this.panel1.TabIndex = 43;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(2, 279);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(198, 96);
            this.panel4.TabIndex = 13;
            // 
            // ultraExplorerBar
            // 
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            appearance3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            appearance3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(198)))), ((int)(((byte)(235)))));
            appearance3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.ultraExplorerBar.Appearance = appearance3;
            this.ultraExplorerBar.BorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            this.ultraExplorerBar.Dock = System.Windows.Forms.DockStyle.Top;
            ultraExplorerBarItem1.Text = "New Item";
            ultraExplorerBarItem2.Text = "New Item";
            ultraExplorerBarItem3.Text = "New Item";
            ultraExplorerBarGroup1.Items.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem[] {
            ultraExplorerBarItem1,
            ultraExplorerBarItem2,
            ultraExplorerBarItem3});
            ultraExplorerBarGroup1.ItemSettings.HotTrackBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
            ultraExplorerBarGroup1.Text = "New Group";
            this.ultraExplorerBar.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1});
            this.ultraExplorerBar.GroupSettings.HeaderVisible = Infragistics.Win.DefaultableBoolean.True;
            this.ultraExplorerBar.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.SmallImagesWithText;
            this.ultraExplorerBar.ImageSizeSmall = new System.Drawing.Size(0, 0);
            appearance14.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(198)))), ((int)(((byte)(235)))));
            appearance14.Cursor = System.Windows.Forms.Cursors.Hand;
            appearance14.Image = ((object)(resources.GetObject("appearance14.Image")));
            this.ultraExplorerBar.ItemSettings.AppearancesSmall.Appearance = appearance14;
            appearance17.BackColor = System.Drawing.Color.Blue;
            this.ultraExplorerBar.ItemSettings.AppearancesSmall.CheckedAppearance = appearance17;
            appearance16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(186)))), ((int)(((byte)(241)))));
            appearance16.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(186)))), ((int)(((byte)(241)))));
            appearance16.BorderColor = System.Drawing.Color.Transparent;
            appearance16.ForeColor = System.Drawing.Color.White;
            this.ultraExplorerBar.ItemSettings.AppearancesSmall.HotTrackAppearance = appearance16;
            this.ultraExplorerBar.ItemSettings.HotTracking = Infragistics.Win.DefaultableBoolean.True;
            this.ultraExplorerBar.ItemSettings.HotTrackStyle = Infragistics.Win.UltraWinExplorerBar.ItemHotTrackStyle.HighlightEntireItem;
            this.ultraExplorerBar.ItemSettings.Style = Infragistics.Win.UltraWinExplorerBar.ItemStyle.Label;
            this.ultraExplorerBar.ItemSettings.UseDefaultImage = Infragistics.Win.DefaultableBoolean.True;
            this.ultraExplorerBar.Location = new System.Drawing.Point(2, 2);
            this.ultraExplorerBar.Name = "ultraExplorerBar";
            this.ultraExplorerBar.ShowDefaultContextMenu = false;
            this.ultraExplorerBar.Size = new System.Drawing.Size(198, 277);
            this.ultraExplorerBar.Style = Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarStyle.Listbar;
            this.ultraExplorerBar.TabIndex = 12;
            this.ultraExplorerBar.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.ultraExplorerBar.ViewStyle = Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarViewStyle.Office2007;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 15000D;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.splitter1.Location = new System.Drawing.Point(202, 74);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 377);
            this.splitter1.TabIndex = 45;
            this.splitter1.TabStop = false;
            // 
            // FMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(652, 475);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ultraStatusBar);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "明基逐鹿制造运营管理解决方案";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closed += new System.EventHandler(this.FMain_Closed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FMain_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FMain_Paint);
            this.Resize += new System.EventHandler(this.FMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ultraStatusBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiMain)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutInfo.ResumeLayout(false);
            this.tableLayoutInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        //		public void SetLoginInfo( LoginInfo loginInfo )
        //		{
        //		}

        public string LoginUser
        {
            set
            {
                this.labUser.Text = value;
                this.ultraStatusBar.Panels[1].Text = value;
            }
        }

        public string LoginResource
        {
            set
            {
                this.ultraStatusBar.Panels[3].Text = value;
            }
        }

        public string LoginVersion
        {
            set
            {
                this.ultraStatusBar.Panels[5].Text = value;
            }
        }

        public DateTime LoginDateTime
        {
            set
            {
                this.ultraStatusBar.Panels[7].Text = FormatHelper.TODateTimeString(
                    FormatHelper.TODateInt(value),
                    FormatHelper.TOTimeInt(value));
            }
        }

        public string LoginDB
        {
            set
            {
                this.ultraStatusBar.Panels[9].Text = value;
            }
        }

        private void FMain_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        }

        private void ultraExplorerBar_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
        {

        }

        public void Flash()
        {
            ApplicationService.Current().MainWindows = this;

            Infragistics.Win.UltraWinToolbars.UltraToolbar mainMenuBar = ultraToolbarsManager.Toolbars["MainMenuBar"];
            mainMenuBar.ToolbarsManager.Tools.Clear();
            mainMenuBar.Tools.Clear();

            ApplicationService.Current().MenuService.ClearMenu();
            ApplicationService.Current().MenuService.MergeUltraWinMainMenu(mainMenuBar, true);

            /* added by jessie lee, 2006-6-14
             * 禁掉menu右键菜单 */
            mainMenuBar.ShowInToolbarList = false;

            // Added by Icyer 2006-07-31
            // SMT上料资源登录时自动打开料卷监控界面
            // 判断SMT上料资源，以SMT开头的资源
            if (FSMTFeederReelWatch.Current != null)
                FSMTFeederReelWatch.Current.CloseForce();
            string strSmtResPrefix = "SMT";
            BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = new SystemSettingFacade(ApplicationService.Current().DataProvider);
            object obj = sysFacade.GetParameter("SMT_RESOURCE_PREFIX", "SMT_LINE_MACHINE");
            if (obj != null)
                strSmtResPrefix = ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterAlias;
            if (ApplicationService.Current().LoginInfo.Resource.ResourceCode.StartsWith(strSmtResPrefix) == true)
            {
                ApplicationService.Current().MenuService.DispathToolbarOnClick("BenQGuru.eMES.Client.FSMTFeederReelWatch");
            }
            // Added end

            // Added By Hi1/Venus.Feng on 20080629 for Hisense Version : Add Org ID
            if (GlobalVariables.CurrentOrganizations.GetOrganizationList().Count == 0)
            {
                this.labelOrganization.Visible = false;
            }
            else
            {
                this.labelOrganization.Text = UserControl.MutiLanguages.ParserString("$PageControl_Organization") + GlobalVariables.CurrentOrganizations.First().OrganizationDescription;
                //modified by carey.cheng on 2010-05-12 for KN
                //to mark organization
                //this.labelOrganization.Visible = true;
                this.labelOrganization.Visible = false;
                //end modified
            }
            // End

            //added by hi1/leon.yuan 2009/1/8 for setting environment variable "NLS_Lang" in current process. 
            //To synchronize NLS_Lang between Oracle DB & CS Client
            Parameter param = (Parameter)sysFacade.GetParameter("NLS_LANG_Code", "NLS_LANG");
            if (param != null)
            { Environment.SetEnvironmentVariable("NLS_LANG", param.ParameterAlias); }
            //end added by hi1/leon.yuan 2009/1/8

            //Added by carey.cheng on 2010-05-18 for new login mode
            this.ultraTabbedMdiMain.MdiParent = this;
            this.ultraTabbedMdiMain.Enabled = true;
            this.panel1.Visible = true;
            this.panel3.Visible = true;
            this.panel4.Visible = true;
            ultraExplorerBar.Height = Convert.ToInt32(panel1.Height * 0.6);
            panel4.Height = panel1.Height - Convert.ToInt32(panel1.Height * 0.6);
            ApplicationRun.GetInfoForm();
            this.Refresh();
            //End added by carey.cheng on 2010-05-18 for new login mode
        }

        private void FMain_Closed(object sender, System.EventArgs e)
        {
        }

        private void FMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FSMTFeederReelWatch.Current != null)
                FSMTFeederReelWatch.Current.CloseForce();
            Application.Exit();
        }

        //Laws Lu,2005/09/14,新增	增加MessageForm高度
        private void FMain_Resize(object sender, System.EventArgs e)
        {
            ultraExplorerBar.Height = Convert.ToInt32(panel1.Height * 0.6);
            panel4.Height = panel1.Height - Convert.ToInt32(panel1.Height * 0.6);
        }

        private void ultraStatusBar_Click(object sender, System.EventArgs e)
        {

        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            

            if (ApplicationService.Current().UserCode != null)
            {
                if ((System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"] != null
                    && System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"].Trim() != String.Empty))
                {
                    string configHour = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"].Trim().Substring(0, 2)).ToString();
                    string configTime = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["CheckUpdateDateTime"].Trim().Substring(3, 2)).ToString();

                    if (configHour == DateTime.Now.Hour.ToString() &&
                        configTime == DateTime.Now.Minute.ToString() && iUpdaterAlertCount == 0)
                    {
                        object objUpdater = ApplicationService.CheckUpdate();
                        if (objUpdater != null)
                        {
                            iUpdaterAlertCount = Convert.ToInt32(4 * timer1.Interval);

                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error,
                                "$CS_NEW_VERSION_AVIALABLE_PLS_LOGOUT " + Convert.ToInt32(iUpdaterAlertCount / timer1.Interval)));


                            //iUpdaterAlertCount = 60;
                            //							System.Threading.Thread.Sleep(30000);
                            //							//超出预警次数，自动退出系统
                            //						
                            //							this.Close();
                            //							Application.Exit();
                        }
                    }



                    //this.
                    //				Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
                    //				ApplicationService.AutoUpdate(upd.Location.Trim(),upd.LoginUser,upd.LoginPassword);
                }
                if (iUpdaterAlertCount > 0)
                {
                    iUpdaterAlertCount -= Convert.ToInt32(1 * timer1.Interval);
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error,
                        "$CS_NEW_VERSION_AVIALABLE_PLS_LOGOUT " + Convert.ToInt32(iUpdaterAlertCount / timer1.Interval)));

                    if (iUpdaterAlertCount == 0)
                    {
                        this.Close();
                        Application.Exit();
                    }

                }

            }
        }

        private void lblUpdateNow_Click(object sender, System.EventArgs e)
        {
            //Laws Lu,2006/08/04 修改	自动更新的功能完善
            object objUpdater = ApplicationService.CheckUpdate();
            if (objUpdater != null)
            {
                Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
                ApplicationService.AutoUpdate(upd.Location.Trim(), upd.LoginUser, upd.LoginPassword);
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message("$CS_LOCALVERSION_IS_NEW"));
            }
        }



        public System.Windows.Forms.Form ActiveWindow
        {
            get
            {
                return this.ActiveMdiChild;
            }
        }
        //Amoi,Laws Lu,2005/08/01,新增	允许外部访问panel4
        public Panel MessageForm
        {
            get
            {
                return this.panel4;
            }
        }

        //EndAmoi


        private void labelChgPwd_Click(object sender, EventArgs e)
        {
            //FChangePassword chgForm = new FChangePassword(this.labUser.Text.Trim());
            FChangePassword chgForm = new FChangePassword(ApplicationService.Current().UserCode);
            chgForm.ShowDialog();
        }

        private void labelHelp_Click(object sender, EventArgs e)
        {

        }

        private void labelLogOut_Click(object sender, EventArgs e)
        {
            //Added by carey.cheng on 2010-05-18 for new login mode
            BenQGuru.eMES.Client.Service.ApplicationService.Current().CloseAllMdiChildren();
            ApplicationRun.GetInfoForm().Clear();
            if (FSMTFeederReelWatch.Current != null)
                FSMTFeederReelWatch.Current.CloseForce();
            this.panel1.Visible = false;
            this.panel3.Visible = false;
            this.panel4.Visible = false;
            this.ultraTabbedMdiMain.Enabled = false;
            this.BackgroundImage = Properties.Resources.BackgroundImage_CHS;
            this.pictureBox2.Image = Properties.Resources.Index_eMES_banner;
            this.pictureBox4.Image = Properties.Resources.Index_eMES_logo;
            FLogin login = new FLogin();
            login.MdiParent = this;

            login.Show();
            
            this.InitPageLanguage();
            //End Added by carey.cheng on 2010-05-18 for new login mode
        }

    }
}
