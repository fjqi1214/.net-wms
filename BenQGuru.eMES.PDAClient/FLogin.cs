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
using BenQGuru.eMES.PDAClient.Service;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.PDAClient
{
    /// <summary>
    /// FLogin 的摘要说明。
    /// </summary>
    public class FLogin : FormBase
    {
        private System.Windows.Forms.PictureBox pbDotnet;
        private System.Windows.Forms.PictureBox pictureBox1;
        private UserControl.UCButton btnLogin;
        private UserControl.UCButton btnExit;
        public UserControl.UCLabelEdit ucLEUserCode;
        private UserControl.UCLabelCombox ucLClanguage;
        public UserControl.UCLabelEdit ucLEResourceCode;
        private PictureBox pictureBox2;
        private FMain fmain;
        public UserControl.UCLabelEdit ucLEPassword;
        private UserControl.UCLabelCombox cboDatabase;

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FLogin(FMain fmain)
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();
            InitLanguageItems();
            InitDbItems();            
            this.fmain = fmain;
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
        
        private void InitLanguageItems()
        {
            this.ucLClanguage.ComboBoxData.Items.Add("中文简体");
            this.ucLClanguage.ComboBoxData.Items.Add("中文繁體");
            this.ucLClanguage.ComboBoxData.Items.Add("English");
            this.ucLClanguage.SelectedIndex = 0;
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.ucLEUserCode = new UserControl.UCLabelEdit();
            this.btnExit = new UserControl.UCButton();
            this.btnLogin = new UserControl.UCButton();
            this.ucLClanguage = new UserControl.UCLabelCombox();
            this.ucLEResourceCode = new UserControl.UCLabelEdit();
            this.ucLEPassword = new UserControl.UCLabelEdit();
            this.cboDatabase = new UserControl.UCLabelCombox();
            ((System.ComponentModel.ISupportInitialize)(this.pbDotnet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pbDotnet
            // 
            this.pbDotnet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pbDotnet.Image = ((System.Drawing.Image)(resources.GetObject("pbDotnet.Image")));
            this.pbDotnet.Location = new System.Drawing.Point(0, 148);
            this.pbDotnet.Name = "pbDotnet";
            this.pbDotnet.Size = new System.Drawing.Size(298, 150);
            this.pbDotnet.TabIndex = 25;
            this.pbDotnet.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(298, 129);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 129);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(298, 20);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 33;
            this.pictureBox2.TabStop = false;
            // 
            // ucLEUserCode
            // 
            this.ucLEUserCode.AllowEditOnlyChecked = true;
            this.ucLEUserCode.AutoSelectAll = false;
            this.ucLEUserCode.AutoUpper = true;
            this.ucLEUserCode.Caption = "用户名";
            this.ucLEUserCode.Checked = false;
            this.ucLEUserCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ucLEUserCode.EditType = UserControl.EditTypes.String;
            this.ucLEUserCode.Location = new System.Drawing.Point(109, 152);
            this.ucLEUserCode.MaxLength = 40;
            this.ucLEUserCode.Multiline = false;
            this.ucLEUserCode.Name = "ucLEUserCode";
            this.ucLEUserCode.PasswordChar = '\0';
            this.ucLEUserCode.ReadOnly = false;
            this.ucLEUserCode.ShowCheckBox = false;
            this.ucLEUserCode.Size = new System.Drawing.Size(179, 23);
            this.ucLEUserCode.TabIndex = 0;
            this.ucLEUserCode.TabNext = true;
            this.ucLEUserCode.Value = "";
            this.ucLEUserCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEUserCode.XAlign = 155;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.ButtonType = UserControl.ButtonTypes.Exit;
            this.btnExit.Caption = "退出";
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Location = new System.Drawing.Point(203, 271);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(88, 22);
            this.btnExit.TabIndex = 5;
            this.btnExit.Click += new System.EventHandler(this.ucBtnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.SystemColors.Control;
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
            this.btnLogin.ButtonType = UserControl.ButtonTypes.None;
            this.btnLogin.Caption = "登录";
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Location = new System.Drawing.Point(109, 271);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(88, 22);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Click += new System.EventHandler(this.ucBtnLogin_Click);
            // 
            // ucLClanguage
            // 
            this.ucLClanguage.AllowEditOnlyChecked = true;
            this.ucLClanguage.Caption = "语  言";
            this.ucLClanguage.Checked = false;
            this.ucLClanguage.Font = new System.Drawing.Font("宋体", 8.75F);
            this.ucLClanguage.Location = new System.Drawing.Point(109, 224);
            this.ucLClanguage.Name = "ucLClanguage";
            this.ucLClanguage.SelectedIndex = -1;
            this.ucLClanguage.ShowCheckBox = false;
            this.ucLClanguage.Size = new System.Drawing.Size(179, 22);
            this.ucLClanguage.TabIndex = 6;
            this.ucLClanguage.WidthType = UserControl.WidthTypes.Normal;
            this.ucLClanguage.XAlign = 155;
            this.ucLClanguage.SelectedIndexChanged += new System.EventHandler(this.ucLClanguage_SelectedIndexChanged);
            // 
            // ucLEResourceCode
            // 
            this.ucLEResourceCode.AllowEditOnlyChecked = true;
            this.ucLEResourceCode.AutoSelectAll = false;
            this.ucLEResourceCode.AutoUpper = true;
            this.ucLEResourceCode.Caption = "资  源";
            this.ucLEResourceCode.Checked = false;
            this.ucLEResourceCode.EditType = UserControl.EditTypes.String;
            this.ucLEResourceCode.Location = new System.Drawing.Point(109, 201);
            this.ucLEResourceCode.MaxLength = 40;
            this.ucLEResourceCode.Multiline = false;
            this.ucLEResourceCode.Name = "ucLEResourceCode";
            this.ucLEResourceCode.PasswordChar = '\0';
            this.ucLEResourceCode.ReadOnly = false;
            this.ucLEResourceCode.ShowCheckBox = false;
            this.ucLEResourceCode.Size = new System.Drawing.Size(179, 22);
            this.ucLEResourceCode.TabIndex = 2;
            this.ucLEResourceCode.TabNext = false;
            this.ucLEResourceCode.Value = "";
            this.ucLEResourceCode.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEResourceCode.XAlign = 155;
            this.ucLEResourceCode.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ucLEResourceCode_TxtboxKeyPress);
            // 
            // ucLEPassword
            // 
            this.ucLEPassword.AllowEditOnlyChecked = true;
            this.ucLEPassword.AutoSelectAll = false;
            this.ucLEPassword.AutoUpper = true;
            this.ucLEPassword.Caption = "密  码";
            this.ucLEPassword.Checked = false;
            this.ucLEPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ucLEPassword.EditType = UserControl.EditTypes.String;
            this.ucLEPassword.Location = new System.Drawing.Point(109, 177);
            this.ucLEPassword.MaxLength = 40;
            this.ucLEPassword.Multiline = false;
            this.ucLEPassword.Name = "ucLEPassword";
            this.ucLEPassword.PasswordChar = '*';
            this.ucLEPassword.ReadOnly = false;
            this.ucLEPassword.ShowCheckBox = false;
            this.ucLEPassword.Size = new System.Drawing.Size(179, 23);
            this.ucLEPassword.TabIndex = 1;
            this.ucLEPassword.TabNext = true;
            this.ucLEPassword.Value = "";
            this.ucLEPassword.WidthType = UserControl.WidthTypes.Normal;
            this.ucLEPassword.XAlign = 155;
            // 
            // cboDatabase
            // 
            this.cboDatabase.AllowEditOnlyChecked = true;
            this.cboDatabase.Caption = "数据库";
            this.cboDatabase.Checked = false;
            this.cboDatabase.Font = new System.Drawing.Font("宋体", 8.75F);
            this.cboDatabase.Location = new System.Drawing.Point(109, 247);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.SelectedIndex = -1;
            this.cboDatabase.ShowCheckBox = false;
            this.cboDatabase.Size = new System.Drawing.Size(179, 22);
            this.cboDatabase.TabIndex = 7;
            this.cboDatabase.WidthType = UserControl.WidthTypes.Normal;
            this.cboDatabase.XAlign = 155;
            // 
            // FLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(298, 298);
            this.ControlBox = false;
            this.Controls.Add(this.cboDatabase);
            this.Controls.Add(this.ucLEPassword);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.ucLEUserCode);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.ucLClanguage);
            this.Controls.Add(this.ucLEResourceCode);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbDotnet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.FLogin_Load);
            this.Closed += new System.EventHandler(this.FLogin_Closed);
            ((System.ComponentModel.ISupportInitialize)(this.pbDotnet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private IDomainDataProvider _domainDataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        protected void ShowMessage(string message)
        {
            FMessageBox messageBox = new FMessageBox(UserControl.MutiLanguages.ParserMessage(message));
            messageBox.ShowDialog();
        }

        protected void ShowMessage(Exception e)
        {
            FMessageBox messageBox = new FMessageBox(UserControl.MutiLanguages.ParserMessage(e.Message));
            messageBox.ShowDialog();
            //MessageBox.Show(UserControl.MutiLanguages.ParserMessage(e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            FMessageBox messageBox = new FMessageBox(UserControl.MutiLanguages.ParserMessage(message.Body));
            messageBox.ShowDialog();
            //MessageBox.Show(UserControl.MutiLanguages.ParserMessage(message.Body), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ucBtnLogin_Click(object sender, System.EventArgs e)
        {
            string dbName = (this.cboDatabase.ComboBoxData.SelectedItem as BenQGuru.eMES.Common.Config.PersistBrokerSetting).Name;
            MesEnviroment.LoginDB = dbName;
            _domainDataProvider = ApplicationService.Login(dbName).DataProvider;
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
                //ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
                return;
            }

            ApplicationService.Current().DataProvider = _basemodelFacade.DataProvider;
            //缓解性能问题
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

                // Add Org ID
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

                //自动更新的功能完善
                object objUpdater = ApplicationService.CheckUpdate();
                if (objUpdater != null)
                {
                    Updater upd = (BenQGuru.eMES.Web.Helper.Updater)objUpdater;
                    ApplicationService.AutoUpdate(upd.Location.Trim(), upd.LoginUser, upd.LoginPassword);
                }

                ApplicationService.Current().LoginInfo = new LoginInfo(user.UserCode, (Resource)obj, objUserGroup);

                //修改	显示用户名
                string strUserName = String.Empty;
                string strResource = String.Empty;
                if (user != null)
                {
                    strUserName = user.UserName.Trim();
                }                

                if (ApplicationService.Current().LoginInfo != null)
                {
                    strResource = string.Format("{0}/{1}/{2}",
                        ApplicationService.Current().LoginInfo.Resource.ResourceCode,
                        ApplicationService.Current().LoginInfo.Resource.StepSequenceCode,
                        ApplicationService.Current().LoginInfo.Resource.SegmentCode);
                }
                
                ApplicationService.Current().MainWindows = fmain;                
                ApplicationService.Current().MainWindows.LoginVersion = UserControl.FileLog.GetLocalCSVersion(UserControl.FileLog.VersionFileName);
                ApplicationService.Current().MainWindows.LoginDateTime = DateTime.Now;
                ApplicationService.Current().MainWindows.LoginUser = strUserName;
                ApplicationService.Current().MainWindows.LoginResource = strResource;
                ApplicationService.Current().MainWindows.LoginDB = (this.cboDatabase.ComboBoxData.SelectedItem as BenQGuru.eMES.Common.Config.PersistBrokerSetting).Text;
                fmain.Flash();

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
                //缓解性能问题
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
            this.InitPageLanguage();
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

        //焦点进入背景色变为浅绿，移出恢复正常
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

            this.InitPageLanguage();
            if (this.MdiParent != null)
            {
                (this.MdiParent as FMain).InitPageLanguage();
            }
        }

    }
}
