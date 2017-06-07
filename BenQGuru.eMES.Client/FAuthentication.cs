using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Security;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FAuthentication : BaseForm
    {
        public static bool m_isRightUser = false;
        public static string m_UserCode = string.Empty;
        private string paraGroup = string.Empty;
        public FAuthentication(string paraGroupRight)
        {
            m_isRightUser = false;
            m_UserCode = string.Empty;

            this.paraGroup = paraGroupRight;
            InitializeComponent();
        }

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        SystemSettingFacade systemSettingFacade = null;
        SecurityFacade _facade = null;
        private bool _isRightUsers = false;
        
        #region 属性

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }


        private bool IsRightUser
        {
            get
            {
                return _isRightUsers;
            }
        }


        #endregion

        protected void ShowMessage(Exception e)
        {
            MessageBox.Show(UserControl.MutiLanguages.ParserMessage(e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtusercode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.txtpwd.Focus();

            }
        }

        private void txtpwd_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnOK_Click(null, null);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //用户权限
                systemSettingFacade = new SystemSettingFacade(this.DataProvider);
                _facade = new SecurityFacade(this.DataProvider);
                if (this.txtusercode.Value.Trim() == string.Empty)
                {
                    this.lblInfo.Text = MutiLanguages.ParserString("$Error_User_Code_Empty");
                    txtusercode.TextFocus(false, true);
                    return;
                }

                // 未输入密码
                if (this.txtpwd.Value.Trim() == string.Empty)
                {
                    this.lblInfo.Text = MutiLanguages.ParserString("$Error_Password_Empty");
                    txtpwd.Focus();
                    return;
                }

                object COOPParameter = systemSettingFacade.GetParameter(this.txtusercode.Value.Trim().ToUpper(), paraGroup);
                if (COOPParameter == null)
                {
                    this.lblInfo.Text = MutiLanguages.ParserString("$CS_CurrentUser_Have_No_Right");
                    this.txtusercode.TextFocus(false, true);
                    return;
                }

                // 用户名不存在
                object[] objUserGroup = null;
                User user = _facade.LoginCheck(this.txtusercode.Value.Trim().ToUpper(), this.txtpwd.Value.Trim().ToUpper(), out objUserGroup);
                if (user == null)
                {
                    this.lblInfo.Text = MutiLanguages.ParserString("$Error_User_Not_Exist");
                    return;
                }
                m_isRightUser = true;
                m_UserCode = this.txtusercode.Value.Trim().ToUpper();
                this.Close();
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex);
                if (ex.Message == "$Error_Password_Not_Match")
                {

                    this.txtpwd.Focus();
                }
                else
                {
                    this.txtusercode.TextFocus(false, true);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FAuthentication_Load(object sender, EventArgs e)
        {
            this.txtusercode.Focus();
        }
    }
}
