using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using BenQGuru.eMES.PDAClient.Service;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.PDAClient
{
    /// <summary>
    /// Form1 的摘要说明。
    /// 
    /// </summary>
    public class FMain : FormBase
    {
        private Infragistics.Win.UltraWinStatusBar.UltraStatusBar ultraStatusBar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        public Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ultraToolbarsManager;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _FMain_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _FMain_Toolbars_Dock_Area_Bottom;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _FMain_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _FMain_Toolbars_Dock_Area_Right;
        private System.Windows.Forms.Label labUser;
        private System.ComponentModel.IContainer components;
        private System.Timers.Timer timer1;
        private System.Windows.Forms.Label lblUpdateNow;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Label labelOrganization;
        private PictureBox pictureBox4;

        private System.Windows.Forms.MdiClient m_MdiClient;
        private TableLayoutPanel tableLayoutInfo;
        private Label labelChgPwd;
        private Label labelHelp;
        private Label labelLogOut;
        private Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager ultraTabbedMdiMain;
        private Panel panel1;
        public Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar ultraExplorerBar;
        private int iUpdaterAlertCount = 0;

        public FMain()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();            
            foreach (Control item in this.Controls)
            {
                if (item.GetType().ToString().Equals("System.Windows.Forms.MdiClient"))
                {
                    this.m_MdiClient = item as MdiClient;
                    break;
                }
            }
            this.m_MdiClient.BackColor = Color.FromArgb(244, 244, 244);            
            ApplicationService.Current().MainWindows = this;
            Infragistics.Win.UltraWinToolbars.UltraToolbar mainMenuBar = ultraToolbarsManager.Toolbars["MainMenuBar"];
            ApplicationService.Current().MenuService.MergeUltraWinMainMenu(mainMenuBar);            

            FLogin login = new FLogin(this);            
            login.Show();
            this.Visible = false;
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
            FMessageBox messageBox = new FMessageBox(UserControl.MutiLanguages.ParserMessage(message));
            messageBox.ShowDialog();
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
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup ultraExplorerBarGroup1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem1 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem2 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem ultraExplorerBarItem3 = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinToolbars.UltraToolbar ultraToolbar1 = new Infragistics.Win.UltraWinToolbars.UltraToolbar("MainMenuBar");
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            this.ultraStatusBar = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
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
            this._FMain_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._FMain_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._FMain_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.timer1 = new System.Timers.Timer();
            this.ultraTabbedMdiMain = new Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.ultraExplorerBar = new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBar();
            this.ultraToolbarsManager = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this.panel2.SuspendLayout();
            this.tableLayoutInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiMain)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraStatusBar
            // 
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            appearance1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(102)))), ((int)(((byte)(152)))));
            this.ultraStatusBar.Appearance = appearance1;
            this.ultraStatusBar.Location = new System.Drawing.Point(0, 244);
            this.ultraStatusBar.Name = "ultraStatusBar";
            ultraStatusPanel1.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel1.Text = "用户名:";
            ultraStatusPanel1.Key = "UserName";
            ultraStatusPanel2.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel3.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel3.Text = "资源信息:";
            ultraStatusPanel3.Key = "ResCode";
            ultraStatusPanel4.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel4.Width = 300;
            ultraStatusPanel5.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel5.Text = "当前版本:";
            ultraStatusPanel5.Key = "CurrentVer";
            ultraStatusPanel6.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel6.Width = 300;
            ultraStatusPanel7.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel7.Text = "登录时间:";
            ultraStatusPanel7.Key = "LoginTime";
            ultraStatusPanel8.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel8.Width = 300;
            ultraStatusPanel9.SizingMode = Infragistics.Win.UltraWinStatusBar.PanelSizingMode.Automatic;
            ultraStatusPanel9.Text = "数据库:";
            ultraStatusPanel9.Key = "DataBase";
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
            this.ultraStatusBar.Size = new System.Drawing.Size(294, 24);
            this.ultraStatusBar.TabIndex = 9;
            this.ultraStatusBar.Visible = false;
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
            this.panel2.Size = new System.Drawing.Size(294, 50);
            this.panel2.TabIndex = 41;
            this.panel2.Visible = false;
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
            this.tableLayoutInfo.Size = new System.Drawing.Size(0, 50);
            this.tableLayoutInfo.TabIndex = 49;
            this.tableLayoutInfo.Visible = false;
            // 
            // labelChgPwd
            // 
            this.labelChgPwd.AutoSize = true;
            this.labelChgPwd.BackColor = System.Drawing.Color.Transparent;
            this.labelChgPwd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelChgPwd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelChgPwd.ForeColor = System.Drawing.Color.White;
            this.labelChgPwd.Location = new System.Drawing.Point(-145, 22);
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
            this.labelHelp.Location = new System.Drawing.Point(-86, 22);
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
            this.labelLogOut.Location = new System.Drawing.Point(-51, 22);
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
            this.pictureBox4.Location = new System.Drawing.Point(204, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(90, 50);
            this.pictureBox4.TabIndex = 48;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Visible = false;
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
            this.pictureBox2.Visible = false;
            // 
            // lblUpdateNow
            // 
            this.lblUpdateNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUpdateNow.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdateNow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUpdateNow.Font = new System.Drawing.Font("宋体", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUpdateNow.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblUpdateNow.Location = new System.Drawing.Point(-122, 3);
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
            this.pictureBox1.Size = new System.Drawing.Size(294, 50);
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
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this._FMain_Toolbars_Dock_Area_Bottom);
            this.panel3.Controls.Add(this._FMain_Toolbars_Dock_Area_Left);
            this.panel3.Controls.Add(this._FMain_Toolbars_Dock_Area_Right);
            this.panel3.Controls.Add(this._FMain_Toolbars_Dock_Area_Top);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 50);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(2);
            this.panel3.Size = new System.Drawing.Size(294, 23);
            this.panel3.TabIndex = 42;
            // 
            // _FMain_Toolbars_Dock_Area_Bottom
            // 
            this._FMain_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FMain_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.Transparent;
            this._FMain_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._FMain_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.Color.White;
            this._FMain_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(2, 21);
            this._FMain_Toolbars_Dock_Area_Bottom.Name = "_FMain_Toolbars_Dock_Area_Bottom";
            this._FMain_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(290, 0);
            this._FMain_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ultraToolbarsManager;
            // 
            // _FMain_Toolbars_Dock_Area_Left
            // 
            this._FMain_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FMain_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.Transparent;
            this._FMain_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._FMain_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.Color.White;
            this._FMain_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(2, 19);
            this._FMain_Toolbars_Dock_Area_Left.Name = "_FMain_Toolbars_Dock_Area_Left";
            this._FMain_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(0, 2);
            this._FMain_Toolbars_Dock_Area_Left.ToolbarsManager = this.ultraToolbarsManager;
            // 
            // _FMain_Toolbars_Dock_Area_Right
            // 
            this._FMain_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FMain_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.Transparent;
            this._FMain_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._FMain_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.Color.White;
            this._FMain_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(292, 19);
            this._FMain_Toolbars_Dock_Area_Right.Name = "_FMain_Toolbars_Dock_Area_Right";
            this._FMain_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(0, 2);
            this._FMain_Toolbars_Dock_Area_Right.ToolbarsManager = this.ultraToolbarsManager;
            // 
            // _FMain_Toolbars_Dock_Area_Top
            // 
            this._FMain_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FMain_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.Transparent;
            this._FMain_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._FMain_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.Color.White;
            this._FMain_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(2, 2);
            this._FMain_Toolbars_Dock_Area_Top.Name = "_FMain_Toolbars_Dock_Area_Top";
            this._FMain_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(290, 17);
            this._FMain_Toolbars_Dock_Area_Top.ToolbarsManager = this.ultraToolbarsManager;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 15000;
            this.timer1.SynchronizingObject = this;
            this.timer1.Elapsed += new System.Timers.ElapsedEventHandler(this.timer1_Elapsed);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ultraExplorerBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 73);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(294, 244);
            this.panel1.TabIndex = 49;
            // 
            // ultraExplorerBar
            // 
            appearance3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            appearance3.BackColor2 = System.Drawing.Color.Transparent;
            appearance3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            appearance3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(61)))));
            this.ultraExplorerBar.Appearance = appearance3;
            this.ultraExplorerBar.Dock = System.Windows.Forms.DockStyle.Top;
            ultraExplorerBarItem1.Text = "New Item";
            ultraExplorerBarItem2.Text = "New Item";
            ultraExplorerBarItem3.Text = "New Item";
            ultraExplorerBarGroup1.Items.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarItem[] {
            ultraExplorerBarItem1,
            ultraExplorerBarItem2,
            ultraExplorerBarItem3});
            ultraExplorerBarGroup1.Settings.ItemAreaInnerMargins.Right = 47;
            ultraExplorerBarGroup1.Settings.ItemAreaOuterMargins.Right = 76;
            ultraExplorerBarGroup1.Text = "New Group";
            ultraExplorerBarGroup1.Visible = false;
            this.ultraExplorerBar.Groups.AddRange(new Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarGroup[] {
            ultraExplorerBarGroup1});
            this.ultraExplorerBar.GroupSettings.Style = Infragistics.Win.UltraWinExplorerBar.GroupStyle.SmallImagesWithText;
            appearance14.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            appearance14.Cursor = System.Windows.Forms.Cursors.Hand;
            appearance14.Image = ((object)(resources.GetObject("appearance14.Image")));
            this.ultraExplorerBar.ItemSettings.AppearancesSmall.Appearance = appearance14;
            this.ultraExplorerBar.ItemSettings.Style = Infragistics.Win.UltraWinExplorerBar.ItemStyle.Label;
            this.ultraExplorerBar.ItemSettings.UseDefaultImage = Infragistics.Win.DefaultableBoolean.True;
            this.ultraExplorerBar.Location = new System.Drawing.Point(2, 2);
            this.ultraExplorerBar.Name = "ultraExplorerBar";
            this.ultraExplorerBar.ShowDefaultContextMenu = false;
            this.ultraExplorerBar.Size = new System.Drawing.Size(290, 244);
            this.ultraExplorerBar.Style = Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarStyle.Listbar;
            this.ultraExplorerBar.TabIndex = 48;
            this.ultraExplorerBar.ViewStyle = Infragistics.Win.UltraWinExplorerBar.UltraExplorerBarViewStyle.Office2007;
            // 
            // ultraToolbarsManager
            // 
            this.ultraToolbarsManager.AlwaysShowMenusExpanded = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.Color.Transparent;
            appearance2.FontData.BoldAsString = "True";
            appearance2.ForeColor = System.Drawing.Color.White;
            appearance2.TextVAlignAsString = "Top";
            this.ultraToolbarsManager.Appearance = appearance2;
            this.ultraToolbarsManager.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ultraToolbarsManager.DesignerFlags = 1;
            this.ultraToolbarsManager.DockWithinContainer = this;
            this.ultraToolbarsManager.DockWithinContainerBaseType = typeof(System.Windows.Forms.Form);
            this.ultraToolbarsManager.LockToolbars = true;
            this.ultraToolbarsManager.ShowFullMenusDelay = 50;
            this.ultraToolbarsManager.ShowMenuShadows = Infragistics.Win.DefaultableBoolean.False;
            ultraToolbar1.DockedColumn = 0;
            ultraToolbar1.DockedRow = 0;
            ultraToolbar1.Settings.AllowCustomize = Infragistics.Win.DefaultableBoolean.False;
            ultraToolbar1.Settings.BorderStyleDocked = Infragistics.Win.UIElementBorderStyle.None;
            ultraToolbar1.Settings.PaddingLeft = 20;
            this.ultraToolbarsManager.Toolbars.AddRange(new Infragistics.Win.UltraWinToolbars.UltraToolbar[] {
            ultraToolbar1});
            appearance17.ImageBackground = ((System.Drawing.Image)(resources.GetObject("appearance17.ImageBackground")));
            this.ultraToolbarsManager.ToolbarSettings.HotTrackAppearance = appearance17;
            // 
            // FMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(294, 268);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ultraStatusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "FMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "明基逐鹿制造执行系统";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FMain_Paint);
            this.Closed += new System.EventHandler(this.FMain_Closed);
            this.Resize += new System.EventHandler(this.FMain_Resize);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutInfo.ResumeLayout(false);
            this.tableLayoutInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiMain)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraExplorerBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraToolbarsManager)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

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

            /* added
             * 禁掉menu右键菜单 */
            mainMenuBar.ShowInToolbarList = false;
            
            BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = new SystemSettingFacade(ApplicationService.Current().DataProvider);

            //Add Org ID
            if (GlobalVariables.CurrentOrganizations.GetOrganizationList().Count == 0)
            {
                this.labelOrganization.Visible = false;
            }
            else
            {
                this.labelOrganization.Text = UserControl.MutiLanguages.ParserString("$PageControl_Organization") + GlobalVariables.CurrentOrganizations.First().OrganizationDescription;
                this.labelOrganization.Visible = false;
            }

            //setting environment variable "NLS_Lang" in current process. 
            //To synchronize NLS_Lang between Oracle DB & CS Client
            Parameter param = (Parameter)sysFacade.GetParameter("NLS_LANG_Code", "NLS_LANG");
            if (param != null)
            { Environment.SetEnvironmentVariable("NLS_LANG", param.ParameterAlias); }            

            this.ultraTabbedMdiMain.MdiParent = this;
            this.ultraTabbedMdiMain.Enabled = true;
            //ultraExplorerBar.Height = Convert.ToInt32(panel1.Height * 0.6);            
            this.Refresh();            
            this.Show();
        }

        private void FMain_Closed(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        //新增	增加MessageForm高度
        private void FMain_Resize(object sender, System.EventArgs e)
        {
            //ultraExplorerBar.Height = Convert.ToInt32(panel1.Height * 0.6);
            //panel4.Height = panel1.Height - Convert.ToInt32(panel1.Height * 0.6);
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
                        }
                    }                    
                }
                if (iUpdaterAlertCount > 0)
                {
                    iUpdaterAlertCount -= Convert.ToInt32(1 * timer1.Interval);
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
            //修改	自动更新的功能完善
            object objUpdater = ApplicationService.CheckUpdate();
            if (objUpdater != null)
            {
                Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
                ApplicationService.AutoUpdate(upd.Location.Trim(), upd.LoginUser, upd.LoginPassword);
            }
            else
            {
                //ApplicationRun.GetInfoForm().Add(new UserControl.Message("$CS_LOCALVERSION_IS_NEW"));
            }
        }

        public System.Windows.Forms.Form ActiveWindow
        {
            get
            {
                return this.ActiveMdiChild;
            }
        }

        private void labelChgPwd_Click(object sender, EventArgs e)
        {            
            FChangePassword chgForm = new FChangePassword(ApplicationService.Current().UserCode);
            chgForm.ShowDialog();
        }

        private void labelHelp_Click(object sender, EventArgs e)
        {

        }

        private void labelLogOut_Click(object sender, EventArgs e)
        {            
            BenQGuru.eMES.PDAClient.Service.ApplicationService.Current().CloseAllMdiChildren();            
            //this.panel3.Visible = false;            
            this.ultraTabbedMdiMain.Enabled = false;
            FLogin login = new FLogin(this);
            login.Show();            
        }        

    }
}
