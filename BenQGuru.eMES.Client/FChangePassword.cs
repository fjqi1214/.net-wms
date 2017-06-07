using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Security;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Client
{
    public partial class FChangePassword : BaseForm
    {
        private string m_UserCode;
        public FChangePassword(string userCode)
        {
            this.m_UserCode = userCode;
            InitializeComponent();
        }

        private void ucConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.m_UserCode))
                {
                    MessageBox.Show(UserControl.MutiLanguages.ParserString("$Error_Please_Login"), UserControl.MutiLanguages.ParserString("$CS_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //MessageBox.Show(new UserControl.Message(UserControl.MessageType.Input, "").ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(this.ucOldPwd.InnerTextBox.Text.Trim()))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_User_Origin_PassWord_Empty"));
                    this.ucOldPwd.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.ucNewPwd.InnerTextBox.Text.Trim()))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_User_New_PassWord_Empty"));
                    this.ucNewPwd.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(this.ucNewPwdConfirm.InnerTextBox.Text.Trim()))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_User_ConfirmPassWord_Error"));
                    this.ucNewPwdConfirm.Focus();
                    return;
                }
                if (!this.ucNewPwd.InnerTextBox.Text.Trim().Equals(this.ucNewPwdConfirm.InnerTextBox.Text.Trim()))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, "$Error_User_ConfirmPassWord_Error"));
                    this.ucNewPwdConfirm.Focus();
                    return;
                }

                SecurityFacade _facade = null;
                try
                {
                    _facade = new SecurityFacade(ApplicationService.Current().DataProvider);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);                 
                    return;
                }
                try
                {
                    _facade.ModifyPassword(m_UserCode, this.ucOldPwd.InnerTextBox.Text.Trim(), this.ucNewPwd.InnerTextBox.Text.Trim());
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Normal, "$Message_Modify_Password_Successful"));
                }
                catch (Exception ex)
                {
                    if (ex.Message == "$Error_Password_Not_Match")
                    {
                        this.ucOldPwd.Focus();
                    }
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(UserControl.MessageType.Error, ex.Message));
                }
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(ex.Message);
            }
        }

        private void ucCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
