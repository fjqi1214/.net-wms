using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Security;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.MutiLanguage;
using BenQGuru.Public.LicenseController;

namespace BenQuru.eMES.Web
{
    /// <summary>
    /// FLoginNew 的摘要说明。
    /// </summary>
    public partial class FLoginNew :BasePage
    {
        private System.ComponentModel.IContainer components;

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        protected System.Web.UI.HtmlControls.HtmlInputButton cmdCancel;
        private SecurityFacade _facade;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //添加语言栏位初始化
                Language_Load();
                languageComponent1.Language = this.drpLanguageNew.SelectedValue.Trim();
                this.InitPageLanguage(this.languageComponent1, false);
                this.cmdSubmit.Text = languageComponent1.GetString("$PageControl_Submit");
                string word = languageComponent1.GetString("$" + this.GetType().BaseType.Name);
                if (word != string.Empty)
                {
                    this.Title = word;
                }
                //added by carey.cheng on 2010-05-19 for muti db support
                this.dprDatabase.Items.Clear();
                this.dprDatabase.DataSource = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.Settings;
                this.dprDatabase.DataTextField = "Text";
                this.dprDatabase.DataValueField = "Name";
                this.dprDatabase.DataBind();

                for (int i = 0; i < BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.Settings.Count; i++)
                {
                    if (BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.Settings[i].Default)
                    {
                        this.dprDatabase.SelectedIndex = i;
                        break;
                    }
                }
                //End added by carey.cheng on 2010-05-19 for muti db support

            }

            //Add By Jarvis 20121212
            LicenseContinue.LicenseAdvanceDays = int.Parse(System.Configuration.ConfigurationManager.AppSettings["LicenseAdvanceDays"].ToString());
        }

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        protected void cmdOK_ServerClick(object sender, System.EventArgs e)
        {            
            //modified by carey.cheng on 2010-05-19 for muti db support
            MesEnviroment.DatabasePosition = this.dprDatabase.Value;

            //IDomainDataProvider DataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(this.dprDatabase.Value);
            //base.DataProvider = DataProvider;
            //end modified by carey.cheng on 2010-05-19 for muti db support

            SessionHelper sessionHelper = SessionHelper.Current(this.Session);
            _facade = new SecurityFacade(base.DataProvider);
            try
            {
                // 未输入用户名
                if (this.txtUserCode.Text.Trim() == string.Empty)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_User_Code_Empty");
                }
                //用户输入密码错误5次
                if (this.loguser.Value != this.txtUserCode.Text.Trim() && this.loguser.Value != string.Empty)
                {
                    this.logintimes.Value = "0";//登陆用户与上一次的用户不同且不是第一次登陆,输入密码错误次数清零
                }
                if (this.loguser.Value == this.txtUserCode.Text.Trim() || this.loguser.Value == string.Empty)//登陆用户第一次或者和上次登陆的相同
                {
                    this.loguser.Value = this.txtUserCode.Text.Trim();
                    int logtimes = Convert.ToInt32(this.logintimes.Value);
                    logtimes = logtimes + 1;
                    this.logintimes.Value = logtimes.ToString();
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                    DataProvider.BeginTransaction();
                    try
                    {
                        string login = "update tbluser set userstat ='L' where usercode ='" + txtUserCode.Text.Trim().ToUpper() + "'";
                        if (logtimes > 5)
                        {
                            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.Execute(login);//密码错误5次将userstat改为L,锁定账户
                            DataProvider.CommitTransaction();
                            this.logintimes.Value = "0";
                            return;
                        }
                    }
                    catch
                    {
                        DataProvider.RollbackTransaction();
                    }
                    finally
                    {
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    }
                }
                // 未输入密码
                if (this.txtPassword.Text.Trim() == string.Empty)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Password_Empty");
                }
                //验证码
                if (ConfigurationManager.AppSettings["NeedValidationCode"] == "Y")
                {

                    if (Session["ImageCode"] != null)
                    {
                        if (txtValidationCode.Text.Trim() != string.Empty)
                        {
                            if (Session["ImageCode"].ToString() != txtValidationCode.Text.Trim().ToString().ToUpper())
                            {
                                ExceptionManager.Raise(this.GetType(), "$Error_ValidationCode_Wrong");
                            }
                        }
                        else
                        {
                            ExceptionManager.Raise(this.GetType(), "$Error_ValidationCode_Empty");
                        }
                    }
                }

                BenQGuru.eMES.Domain.BaseSetting.User user = this._facade.LoginCheck(FormatHelper.CleanString(this.txtUserCode.Text.ToUpper()), FormatHelper.CleanString(this.txtPassword.Text.ToUpper()));

                // 用户名不存在
                if (user == null)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_User_Not_Exist");
                }
                //新用户必须更改密码,用户限制,锁定账户	
                string userstat = "select userstat from tbluser where usercode ='" + txtUserCode.Text.Trim().ToUpper() + "'";
                DataSet ds = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(userstat);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "C")//用户限制
                    {
                        ExceptionManager.Raise(this.GetType(), "$Error_User_Confined");
                        return;
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "L")//锁定账户
                    {
                        ExceptionManager.Raise(this.GetType(), "$Error_User_Locked");
                        return;
                    }
                    /*
					else if (ds.Tables[0].Rows[0][0].ToString() == "N")//新用户必须更改密码
					{
						ExceptionManager.Raise(this.GetType(), "$Error_User_New");
						return;
					}
                    */
                }

                // Added By Hi1/Venus.Feng on 20080627 for Hisense Version : Add OrgID
                // 只将User默认的Org放进Session中去
                BaseModelFacade baseFacade = new BaseModelFacade(this.DataProvider);
                object org = baseFacade.GetUserDefaultOrgByUser(user.UserCode);
                if (org == null)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_UserDefaultOrg_NotDefined");
                    return;
                }
                GlobalVariables.CurrentOrganizations.Clear();
                GlobalVariables.CurrentOrganizations.Add((Organization)org);
                // End Added

                sessionHelper.IsBelongToAdminGroup = this._facade.IsBelongToAdminGroup(this.txtUserCode.Text.ToUpper().Trim());
                sessionHelper.UserName = user.UserName;
                sessionHelper.UserCode = user.UserCode;
                sessionHelper.UserMail = user.UserEmail;
                //sessionHelper.Language = this.drpLanguage.Value; 
                sessionHelper.Language = this.drpLanguageNew.SelectedValue;

                Log.ClearOldLogFiles(ConfigurationManager.AppSettings.Get("LogKeepDays"));

                //this.Response.Redirect(this.MakeRedirectUrl("./FStartPage.aspx"), false);
                if (!this.CheckLicense())
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                languageComponent1.Language = this.drpLanguageNew.SelectedValue.Trim();
                this.InitPageLanguage(this.languageComponent1, false);
                this.lblMessage.Text = MessageCenter.ParserMessage(ex.Message, this.languageComponent1);
            }
        }

        /// <summary>
        /// 根据登录语言 更换背景图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpLanguageNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpLanguageNew.SelectedValue.Equals("CHS"))
            {
                this.ImageHead.Src = "Skin/Image/Login_banner_emes.jpg";
                this.ImageCenter.Src = "Skin/Image/Login_copyright_bg_text2.jpg";
                this.ImageLogoTD.Style.Value = "background-image:url(Skin/Image/Login_logo.jpg);";
                //this.ImageBody.Style.Value = "background-image:url(Skin/Image/BackgroundImage.jpg);";
            }

            if (this.drpLanguageNew.SelectedValue.Equals("CHT"))
            {
                this.ImageHead.Src = "Skin/Image/Login_banner_emes_CHT.jpg";
                this.ImageCenter.Src = "Skin/Image/Login_copyright_bg_text2_CHT.jpg";
                this.ImageLogoTD.Style.Value = "background-image:url(Skin/Image/Login_logo.jpg);";
                // this.ImageBody.Style.Value = "background-image:url(Skin/Image/BackgroundImage_CHT.jpg);";
            }

            if (this.drpLanguageNew.SelectedValue.Equals("ENU"))
            {
                this.ImageHead.Src = "Skin/Image/Login_banner_emes_ENG.jpg";
                this.ImageCenter.Src = "Skin/Image/Login_copyright_bg_text2_ENG.jpg";
                this.ImageLogoTD.Style.Value = "background-image:url(Skin/Image/Login_logo_ENG.jpg);";
                // this.ImageBody.Style.Value = "background-image:url(Skin/Image/BackgroundImage_ENG.jpg);";
            }
            languageComponent1.Language = this.drpLanguageNew.SelectedValue.Trim();
            this.InitPageLanguage(languageComponent1, false);
            this.cmdSubmit.Text = this.languageComponent1.GetString("$PageControl_Submit");
        }


        public void Language_Load()
        {
            this.drpLanguageNew.Items.Clear();
            this.drpLanguageNew.Items.Add(new ListItem("简体中文", "CHS"));
            this.drpLanguageNew.Items.Add(new ListItem("繁體中文", "CHT"));
            this.drpLanguageNew.Items.Add(new ListItem("English", "ENU"));
            this.drpLanguageNew.SelectedValue = "CHS";
        }

        private bool CheckLicense()
        {
            //Add by Jarvis 20121218 License
#if !DEBUG
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);
            ContextHelper.CurrentServerDate = dtNow;
            LicenseInfo license = ContextHelper.License;    
            if (license.Status != LicenseStatus.Valid)
            {
                Exception ex = new Exception("$License_Expired , $Contact_SysAdmin ！");
                ExceptionManager.Raise(this.GetType().BaseType, "$License_Expired", "", ex);
                return false;
            }
            else
            {
                DateTime exprieDate = license.ExpireDate;
                TimeSpan ts = exprieDate - dtNow;
                if (ts.Days <= LicenseContinue.LicenseAdvanceDays)
                {
                    languageComponent1.Language = this.drpLanguageNew.SelectedValue.Trim();
                    string message = this.languageComponent1.GetString("$License_Will_Expire_After") +" "+ ts.Days + " " + this.languageComponent1.GetString("$License_Will_Expire_Days") + "," + this.languageComponent1.GetString("$Contact_SysAdmin") + "!";
                    string scriptString = string.Format("<script language=\"javascript\">alert('{0}');location.href='{1}'</script>", message, this.MakeRedirectUrl("./FStartPage.aspx"));

                    RegisterClientScriptBlock("LicenseInfo", scriptString);
                }
                else
                {
                    this.Response.Redirect(this.MakeRedirectUrl("./FStartPage.aspx"), false);
                }

                //可设置在登录时/所有页面检查License
                LicenseContinue.LicenseGo = string.Empty;//"Continue";
            }
#endif

#if DEBUG
            this.Response.Redirect(this.MakeRedirectUrl("./FStartPage.aspx"), false);
#endif
            return true;
        }

    }
}
