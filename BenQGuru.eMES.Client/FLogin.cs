using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;
using System.Resources;
using System.Runtime.InteropServices;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.MutiLanguage;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Security;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Client
{
    /// <summary>
    /// FLogin 的摘要说明。
    /// </summary>
    public class FLogin : BaseForm
    {
        //		private const string MesAgent = "BenQGuru.eMES.Agent.exe";
        //		private const string DCTServer = "DCTControlPanel.exe";

        private System.Windows.Forms.PictureBox pbDotnet;
        private System.Windows.Forms.PictureBox pictureBox1;
        private UserControl.UCButton ucBtnLogin;
        private UserControl.UCButton ucBtnExit;
        public UserControl.UCLabelEdit ucLEUserCode;
        private UserControl.UCLabelCombox ucLClanguage;
        public UserControl.UCLabelEdit ucLEResourceCode;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private PictureBox pictureBox2;
        private UserControl.UCLabelCombox cboDatabase;
        public UserControl.UCLabelEdit ucLEPassword;

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FLogin()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            this.ucLClanguage.ComboBoxData.Items.Add("中文简体");
            this.ucLClanguage.ComboBoxData.Items.Add("中文繁體");
            this.ucLClanguage.ComboBoxData.Items.Add("English");
            this.ucLClanguage.SelectedIndex = 0;
            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //

            //added by carey.cheng on 2010-05-20 for muti db support
            InitDbItems();
            //end added by carey.cheng on 2010-05-20 for muti db support

        }

        private void InitDbItems()
        {
            this.cboDatabase.Clear();
            foreach (var item in BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.Settings)
            {
                this.cboDatabase.ComboBoxData.Items.Add(item);
                if (item.Default)
                    this.cboDatabase.ComboBoxData.SelectedItem = item;
                  
            }
            this.cboDatabase.ComboBoxData.ValueMember = "Name";
            this.cboDatabase.ComboBoxData.DisplayMember = "Text";
            

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLogin));
            this.pbDotnet = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ucLEUserCode = new UserControl.UCLabelEdit();
            this.ucBtnExit = new UserControl.UCButton();
            this.ucBtnLogin = new UserControl.UCButton();
            this.ucLClanguage = new UserControl.UCLabelCombox();
            this.ucLEResourceCode = new UserControl.UCLabelEdit();
            this.cboDatabase = new UserControl.UCLabelCombox();
            this.ucLEPassword = new UserControl.UCLabelEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDotnet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pbDotnet
            // 
            this.pbDotnet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbDotnet.Image = ((System.Drawing.Image)(resources.GetObject("pbDotnet.Image")));
            this.pbDotnet.Location = new System.Drawing.Point(0, 223);
            this.pbDotnet.Name = "pbDotnet";
            this.pbDotnet.Size = new System.Drawing.Size(570, 185);
            this.pbDotnet.TabIndex = 25;
            this.pbDotnet.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(570, 203);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(328, 231);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(41, 12);
            this.lblUser.TabIndex = 55;
            this.lblUser.Text = "用户名";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 280);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 31;
            this.label2.Text = "资  源";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 304);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 32;
            this.label3.Text = "语  言";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 203);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(570, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            // 
            // ucLEUserCode
            // 
            this.ucLEUserCode.AllowEditOnlyChecked = true;
            this.ucLEUserCode.AutoSelectAll = false;
            this.ucLEUserCode.AutoUpper = true;
            this.ucLEUserCode.Caption = "";
            this.ucLEUserCode.Checked = false;
            this.ucLEUserCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ucLEUserCode.EditType = UserControl.EditTypes.String;
            this.ucLEUserCode.Location = new System.Drawing.Point(367, 235);
            this.ucLEUserCode.MaxLength = 40;
            this.ucLEUserCode.Multiline = false;
            this.ucLEUserCode.Name = "ucLEUserCode";
            this.ucLEUserCode.PasswordChar = '\0';
            this.ucLEUserCode.ReadOnly = false;
            this.ucLEUserCode.ShowCheckBox = false;
            this.ucLEUserCode.Size = new System.Drawing.Size(141, 23);
            this.ucLEUserCode.TabIndex = 0;
            this.ucLEUserCode.TabNext = true;
            this.ucLEUserCode.Value = "";
            this.ucLEUserCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEUserCode.XAlign = 375;
            // 
            // ucBtnExit
            // 
            this.ucBtnExit.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnExit.BackgroundImage")));
            this.ucBtnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.ucBtnExit.Caption = "退出";
            this.ucBtnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnExit.Location = new System.Drawing.Point(424, 363);
            this.ucBtnExit.Name = "ucBtnExit";
            this.ucBtnExit.Size = new System.Drawing.Size(88, 22);
            this.ucBtnExit.TabIndex = 5;
            this.ucBtnExit.Click += new System.EventHandler(this.ucBtnExit_Click);
            // 
            // ucBtnLogin
            // 
            this.ucBtnLogin.BackColor = System.Drawing.SystemColors.Control;
            this.ucBtnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucBtnLogin.BackgroundImage")));
            this.ucBtnLogin.ButtonType = UserControl.ButtonTypes.None;
            this.ucBtnLogin.Caption = "登录";
            this.ucBtnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ucBtnLogin.Location = new System.Drawing.Point(330, 363);
            this.ucBtnLogin.Name = "ucBtnLogin";
            this.ucBtnLogin.Size = new System.Drawing.Size(88, 22);
            this.ucBtnLogin.TabIndex = 4;
            this.ucBtnLogin.Click += new System.EventHandler(this.ucBtnLogin_Click);
            // 
            // ucLClanguage
            // 
            this.ucLClanguage.AllowEditOnlyChecked = true;
            this.ucLClanguage.Caption = "";
            this.ucLClanguage.Checked = false;
            this.ucLClanguage.Location = new System.Drawing.Point(367, 307);
            this.ucLClanguage.Name = "ucLClanguage";
            this.ucLClanguage.SelectedIndex = -1;
            this.ucLClanguage.ShowCheckBox = false;
            this.ucLClanguage.Size = new System.Drawing.Size(141, 22);
            this.ucLClanguage.TabIndex = 3;
            this.ucLClanguage.WidthType = UserControl.WidthTypes.Normal;
            this.ucLClanguage.XAlign = 375;
            this.ucLClanguage.SelectedIndexChanged += new System.EventHandler(this.ucLClanguage_SelectedIndexChanged);
            // 
            // ucLEResourceCode
            // 
            this.ucLEResourceCode.AllowEditOnlyChecked = true;
            this.ucLEResourceCode.AutoSelectAll = false;
            this.ucLEResourceCode.AutoUpper = true;
            this.ucLEResourceCode.Caption = "";
            this.ucLEResourceCode.Checked = false;
            this.ucLEResourceCode.EditType = UserControl.EditTypes.String;
            this.ucLEResourceCode.Location = new System.Drawing.Point(367, 284);
            this.ucLEResourceCode.MaxLength = 40;
            this.ucLEResourceCode.Multiline = false;
            this.ucLEResourceCode.Name = "ucLEResourceCode";
            this.ucLEResourceCode.PasswordChar = '\0';
            this.ucLEResourceCode.ReadOnly = false;
            this.ucLEResourceCode.ShowCheckBox = false;
            this.ucLEResourceCode.Size = new System.Drawing.Size(141, 22);
            this.ucLEResourceCode.TabIndex = 2;
            this.ucLEResourceCode.TabNext = false;
            this.ucLEResourceCode.Value = "";
            this.ucLEResourceCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEResourceCode.XAlign = 375;
            this.ucLEResourceCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEResourceCode_TxtboxKeyPress);
            // 
            // cboDatabase
            // 
            this.cboDatabase.AllowEditOnlyChecked = true;
            this.cboDatabase.Caption = "";
            this.cboDatabase.Checked = false;
            this.cboDatabase.Location = new System.Drawing.Point(367, 332);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.SelectedIndex = -1;
            this.cboDatabase.ShowCheckBox = false;
            this.cboDatabase.Size = new System.Drawing.Size(141, 22);
            this.cboDatabase.TabIndex = 6;
            this.cboDatabase.WidthType = UserControl.WidthTypes.Normal;
            this.cboDatabase.XAlign = 375;
            // 
            // ucLEPassword
            // 
            this.ucLEPassword.AllowEditOnlyChecked = true;
            this.ucLEPassword.AutoSelectAll = false;
            this.ucLEPassword.AutoUpper = true;
            this.ucLEPassword.Caption = "";
            this.ucLEPassword.Checked = false;
            this.ucLEPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ucLEPassword.EditType = UserControl.EditTypes.String;
            this.ucLEPassword.Location = new System.Drawing.Point(367, 259);
            this.ucLEPassword.MaxLength = 40;
            this.ucLEPassword.Multiline = false;
            this.ucLEPassword.Name = "ucLEPassword";
            this.ucLEPassword.PasswordChar = '*';
            this.ucLEPassword.ReadOnly = false;
            this.ucLEPassword.ShowCheckBox = false;
            this.ucLEPassword.Size = new System.Drawing.Size(141, 23);
            this.ucLEPassword.TabIndex = 1;
            this.ucLEPassword.TabNext = true;
            this.ucLEPassword.Value = "";
            this.ucLEPassword.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEPassword.XAlign = 375;
            // 
            // FLogin
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(570, 408);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.ucLEPassword);
            this.Controls.Add(this.ucLEUserCode);
            this.Controls.Add(this.ucBtnExit);
            this.Controls.Add(this.ucBtnLogin);
            this.Controls.Add(this.cboDatabase);
            this.Controls.Add(this.ucLClanguage);
            this.Controls.Add(this.ucLEResourceCode);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbDotnet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Closed += new System.EventHandler(this.FLogin_Closed);
            this.Load += new System.EventHandler(this.FLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbDotnet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion



        private IDomainDataProvider _domainDataProvider;// = ApplicationService.Current().DataProvider;//marked by carey.cheng on 2010-05-20 for muti db support
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }


        protected void ShowMessage(string message)
        {
            ///lablastMsg.Text =message;
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(Exception e)
        {
            //			string message = string.Empty;
            //			
            ////			do
            ////			{
            //				message += e.Message;
            ////				e = e.InnerException;
            ////			}
            ////			while( e != null );

            //ApplicationRun.GetInfoForm().Add(new UserControl.Message(e));
            MessageBox.Show(UserControl.MutiLanguages.ParserMessage(e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_MouseEnter(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox4_MouseLeave(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.Default;

        }

        private void pictureBox5_MouseEnter(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void pictureBox5_MouseLeave(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }


        private void AlertLoginMessage(UserControl.Message message)
        {
            MessageBox.Show(UserControl.MutiLanguages.ParserMessage(message.Body), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ucBtnLogin_Click(object sender, System.EventArgs e)
        {
            //added by carey.cheng on 2010-05-18 for new login mode
            string dbName = (this.cboDatabase.ComboBoxData.SelectedItem as BenQGuru.eMES.Common.Config.PersistBrokerSetting).Name;
            MesEnviroment.LoginDB = dbName;
            _domainDataProvider = ApplicationService.Login(dbName).DataProvider;
            //end added by carey.cheng on 2010-05-18 for new login mode

            SecurityFacade _facade = null;
            BaseModelFacade _basemodelFacade = null;
            try
            {
                _facade = new SecurityFacade(DataProvider);
                _basemodelFacade = new BaseModelFacade(DataProvider);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
                return;
            }

            ApplicationService.Current().DataProvider = _basemodelFacade.DataProvider;
            //Laws Lu,2005/10/19,新增	缓解性能问题
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            try
            {
                #region 设置多国语言
                string language = this.ucLClanguage.ComboBoxData.SelectedItem.ToString();
                if (language == "中文简体")
                {
                    UserControl.MutiLanguages.Language = LanguageType.SimplifiedChinese;
                    ApplicationService.Current().Language = LanguageType.SimplifiedChinese;
                }
                else if (language == "中文繁體")
                {
                    UserControl.MutiLanguages.Language = LanguageType.TraditionalChinese;
                    ApplicationService.Current().Language = LanguageType.TraditionalChinese;
                }
                else if (language == "English")
                {
                    UserControl.MutiLanguages.Language = LanguageType.English;
                    ApplicationService.Current().Language = LanguageType.English;
                }

                #endregion

                if (this.ucLEUserCode.Value.Trim() == string.Empty)
                {
                    //ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_User_Code_Empty"));
                    AlertLoginMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_User_Code_Empty"));
                    ucLEUserCode.TextFocus(false, true);
                    return;
                }

                // 未输入密码
                if (this.ucLEPassword.Value.Trim() == string.Empty)
                {
                    AlertLoginMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_Password_Empty"));
                    ucLEPassword.Focus();
                    return;
                }

                // 未输入资源
                if (this.ucLEResourceCode.Value.Trim() == string.Empty)
                {
                    AlertLoginMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_Resource_Empty"));
                    ucLEResourceCode.TextFocus(false, true);
                    return;
                }

                object[] objUserGroup = null;
                User user = _facade.LoginCheck(this.ucLEUserCode.Value.Trim().ToUpper(), this.ucLEPassword.Value.Trim().ToUpper(), out objUserGroup);

                // 用户名不存在
                if (user == null)
                {

                    AlertLoginMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_User_Not_Exist"));
                    ucLEUserCode.TextFocus(false, true);
                    return;
                }

                object obj = _basemodelFacade.GetResource(this.ucLEResourceCode.Value.Trim().ToUpper());

                if (obj == null)
                {
                    AlertLoginMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_Resource_Not_Exist"));
                    ucLEResourceCode.TextFocus(false, true);
                    return;
                }

                // 检查Resource权限
                //Laws Lu,2005/08/29,修改	Admin组权限，允许Pass
                //				object[] objUserGroup = (new UserFacade(this._domainDataProvider)).GetUserGroupofUser(user.UserCode);

                bool bIsAdmin = false;
                if (objUserGroup != null)
                {
                    foreach (object o in objUserGroup)
                    {
                        if (((UserGroup)o).UserGroupType == "ADMIN")
                        {
                            bIsAdmin = true;
                            break;
                        }
                    }
                }

                if (!bIsAdmin)
                {
                    if (!_facade.CheckResourceRight(user.UserCode, this.ucLEResourceCode.Value.Trim().ToUpper()))
                    {
                        AlertLoginMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_No_Resource_Right"));
                        ucLEUserCode.TextFocus(false, true);
                        return;
                    }
                }

                // Added By Hi1/Venus.Feng on 20080629 for Hisense Version : Add Org ID
                object org = _basemodelFacade.GetOrg(((Resource)obj).OrganizationID);
                if (org != null)
                {
                    GlobalVariables.CurrentOrganizations.Clear();
                    GlobalVariables.CurrentOrganizations.Add((Organization)org);
                }
                else
                {
                    AlertLoginMessage(new UserControl.Message(UserControl.MessageType.Error, "$Error_NoOrganizationOfTheResource"));
                    ucLEResourceCode.TextFocus(false, true);
                    return;
                }
                // End

                //Laws Lu,2006/08/04 修改	自动更新的功能完善
                object objUpdater = ApplicationService.CheckUpdate();
                if (objUpdater != null)
                {
                    Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
                    ApplicationService.AutoUpdate(upd.Location.Trim(), upd.LoginUser, upd.LoginPassword);
                }
                //				//Laws Lu,2005/08/22,新增	版本更新
                //				object objUpdater = FormatHelper.GetCsVersion(_basemodelFacade.DataProvider);
                //
                //				string strVersion = UserControl.FileLog.GetLocalCSVersion(UserControl.FileLog.VersionFileName);
                //				if (objUpdater != null && strVersion != ((BenQGuru.eMES.Web.Helper.Updater)objUpdater) .CSVersion.Trim())
                //				{
                //					Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
                //					ApplicationService.AutoUpdate(upd.Location.Trim(),upd.LoginUser,upd.LoginPassword);
                //				}
                //End Laws Lu

                ApplicationService.Current().LoginInfo = new LoginInfo(user.UserCode, (Resource)obj, objUserGroup);

                //Laws Lu,2005/09/07,修改	显示用户名
                string strUserName = String.Empty;
                string strResource = String.Empty;

                //object objUser = new UserFacade(_domainDataProvider).GetUser(ApplicationService.Current().LoginInfo.UserCode);

                if (user != null)
                {
                    strUserName = user.UserName.Trim();
                }
                //marked by carey.cheng on 2010-05-20 for new login mode
                //strUserName = strUserName + "  " + UserControl.MutiLanguages.ParserString("menu_Wellcome");

                ApplicationRun.GetInfoForm().Add(strUserName + "  " + UserControl.MutiLanguages.ParserString("menu_Wellcome"));

                if (ApplicationService.Current().LoginInfo != null)
                {
                    strResource = string.Format("{0}/{1}/{2}",
                        ApplicationService.Current().LoginInfo.Resource.ResourceCode,
                        ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                        ApplicationService.Current().LoginInfo.Resource.SegmentCode);
                }
                //End Laws Lu
                //added by carey.cheng on 2010-05-20 for muti db support
                ApplicationService.Current().MainWindows = this.MdiParent as BenQGuru.eMES.Client.FMain;
                //end added by carey.cheng on 2010-05-20 for muti db support
                ApplicationService.Current().MainWindows.LoginVersion = UserControl.FileLog.GetLocalCSVersion(UserControl.FileLog.VersionFileName);
                ApplicationService.Current().MainWindows.LoginDateTime = DateTime.Now;

                ApplicationService.Current().MainWindows.LoginUser = strUserName;
                ApplicationService.Current().MainWindows.LoginResource = strResource;
                ApplicationService.Current().MainWindows.LoginDB = (this.cboDatabase.ComboBoxData.SelectedItem as BenQGuru.eMES.Common.Config.PersistBrokerSetting).Text;
                //ApplicationService.Current().MainWindows.BackgroundImage=
                ApplicationRun.GetQtyForm().Close();

                ((BenQGuru.eMES.Client.FMain)this.MdiParent).Flash();

                Log.ClearOldLogFiles(ConfigurationManager.AppSettings.Get("LogKeepDays"));
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
                if (ex.Message == "$Error_Password_Not_Match")
                {
                    ucLEPassword.Focus();
                }
                else
                {
                    ucLEUserCode.TextFocus(false, true);
                }

                return;
            }
            finally
            {
                //Laws Lu,2006/12/29,新增	缓解性能问题
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }

            this.Close();
        }

        private void ucLEResourceCode_TxtboxKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.ucBtnLogin_Click(sender, null);
            }
        }

        private void FLogin_Load(object sender, System.EventArgs e)
        {
            //this.InitPageLanguage();
        }

        private void FLogin_Closed(object sender, System.EventArgs e)
        {
            if (this.DataProvider != null)
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }
        }



        #region 修改系统时间

        [DllImport("kernel32.dll")]
        private static extern bool SetLocalTime(ref SYSTEMTIME time);

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public short year;
            public short month;
            public short dayOfWeek;
            public short day;
            public short hour;
            public short minute;
            public short second;
            public short milliseconds;
        }

        private static void SetDate(DateTime dt)
        {
            SYSTEMTIME st;

            st.year = (short)dt.Year;
            st.month = (short)dt.Month;
            st.dayOfWeek = (short)dt.DayOfWeek;
            st.day = (short)dt.Day;
            st.hour = (short)dt.Hour;
            st.minute = (short)dt.Minute;
            st.second = (short)dt.Second;
            st.milliseconds = (short)dt.Millisecond;

            SetLocalTime(ref st);
        }

        #endregion

        //Laws Lu,2006/12/25 焦点进入背景色变为浅绿，移出恢复正常
        private void ucLEPassword_Enter(object sender, System.EventArgs e)
        {
            this.ucLEPassword.BackColor = Color.GreenYellow;
        }

        private void ucLEPassword_Leave(object sender, System.EventArgs e)
        {
            this.ucLEPassword.BackColor = Color.White;
        }

        private void ucLEPassword_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Application.DoEvents();
                ucLEResourceCode.TextFocus(false, true);
            }
        }

        private void ucBtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ucLClanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region 设置多国语言
            string language = this.ucLClanguage.ComboBoxData.SelectedItem.ToString();
            if (language == "中文简体")
            {
                UserControl.MutiLanguages.Language = LanguageType.SimplifiedChinese;
                ApplicationService.Current().Language = LanguageType.SimplifiedChinese;
                Bitmap bmpBanner = new Bitmap(Properties.Resources.Login_banner_emes);//Banner
                this.pictureBox1.Image = bmpBanner;
                Bitmap bmpCopyRight = new Bitmap(Properties.Resources.Login_copyright_bg_text2);
                this.pictureBox2.Image = bmpCopyRight;
                Bitmap bmpLogo = new Bitmap(Properties.Resources.Login_logo);
                this.pbDotnet.Image = bmpLogo;
                //FMain fMain = new FMain();
                //this.ParentForm.BackgroundImage = Properties.Resources.MainBackGround;
                //fMain.pictureBox2.Image = Properties.Resources.MainCopyRight_Image;
                //fMain.pictureBox4.Image = Properties.Resources.MainLogo_Image;
                
            }
            else if (language == "中文繁體")
            {
                UserControl.MutiLanguages.Language = LanguageType.TraditionalChinese;
                ApplicationService.Current().Language = LanguageType.TraditionalChinese;
                Bitmap bmpBanner = new Bitmap(Properties.Resources.Login_banner_emes_CHT);//Banner
                this.pictureBox1.Image = bmpBanner;
                Bitmap bmpCopyRight = new Bitmap(Properties.Resources.Login_copyright_bg_text2_CHT);
                this.pictureBox2.Image = bmpCopyRight;
                Bitmap bmpLogo = new Bitmap(Properties.Resources.Login_logo);
                this.pbDotnet.Image = bmpLogo;
                //FMain fMain = new FMain();
                //this.ParentForm.BackgroundImage = Properties.Resources.BackgroundImage;
                //fMain.pictureBox2.Image = Properties.Resources.Login_banner_emes;
                //fMain.pictureBox4.Image = Properties.Resources.MainLogo_Image;
            }
            else if (language == "English")
            {
                UserControl.MutiLanguages.Language = LanguageType.English;
                ApplicationService.Current().Language = LanguageType.English;
                Bitmap bmpBanner = new Bitmap(Properties.Resources.Login_banner_emes_ENG);//Banner
                this.pictureBox1.Image = bmpBanner;
                Bitmap bmpCopyRight = new Bitmap(Properties.Resources.Login_copyright_bg_text2_ENG);
                this.pictureBox2.Image = bmpCopyRight;
                Bitmap bmpLogo = new Bitmap(Properties.Resources.Login_logo_ENG);
                this.pbDotnet.Image = bmpLogo;
                //FMain fMain = new FMain();
                //this.ParentForm.BackgroundImage = Properties.Resources.BackgroundImage_EN;
                //fMain.pictureBox2.Image = Properties.Resources.Login_banner_emes_EN;
                //fMain.pictureBox4.Image = Properties.Resources.Index_eMES_logo;
            }

            #endregion

            //this.InitPageLanguage();
            if (this.MdiParent != null)
            {
                if (language == "中文简体")
                {
                    (this.MdiParent as FMain).BackgroundImage = Properties.Resources.BackgroundImage_CHS;
                    (this.MdiParent as FMain).pictureBox2.Image = Properties.Resources.Index_eMES_banner;
                    (this.MdiParent as FMain).pictureBox4.Image = Properties.Resources.Index_eMES_logo;
                }
                else if (language == "中文繁體")
                {
                    (this.MdiParent as FMain).BackgroundImage = Properties.Resources.BackgroundImage_CHT;
                    (this.MdiParent as FMain).pictureBox2.Image = Properties.Resources.Index_eMES_banner_CHT;
                    (this.MdiParent as FMain).pictureBox4.Image = Properties.Resources.Index_eMES_logo;
                }
                else if (language == "English")
                {
                    (this.MdiParent as FMain).BackgroundImage = Properties.Resources.BackgroundImage_ENU;
                    (this.MdiParent as FMain).pictureBox2.Image = Properties.Resources.Index_eMES_banner_ENG;
                    (this.MdiParent as FMain).pictureBox4.Image = Properties.Resources.Index_eMES_logo_ENG;
                }
                (this.MdiParent as FMain).InitPageLanguage();

                
            }
        }



    }
}
