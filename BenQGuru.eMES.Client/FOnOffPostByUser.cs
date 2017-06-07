using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FOnOffPostByUser : BaseForm
    {
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private PerformanceFacade _PerformanceFacade = null;

        public FOnOffPostByUser()
        {
            InitializeComponent();

            _PerformanceFacade = new PerformanceFacade(this.DataProvider);
        }

        #region Properties And Common Functions

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _DomainDataProvider;
            }
        }

        protected void ShowMessage(string message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(UserControl.Messages messages)
        {
            ApplicationRun.GetInfoForm().Add(messages);
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        #endregion

        #region Events

        private void FOnOffPostByUser_Activated(object sender, EventArgs e)
        {
            ucLabelEditUserCodeInput.TextFocus(true, false);
        }

        private void ucLabelEditUserCodeInput_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
            {
                return;
            }

            if (this.ucLabelEditUserCodeInput.Value.Trim()==string.Empty)
            {
                this.ucLabelEditUserCodeInput.TextFocus(true, true);
                return;
            }

            string userCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ucLabelEditUserCodeInput.Value));
            List<string> userCodeList = new List<string>();
            userCodeList.Add(userCode);

            Messages msgs = new Messages();
            if (ultraOptionSetOnOff.CheckedIndex == 0)
            {
                msgs = _PerformanceFacade.CheckBeforeGoOnPost(ApplicationService.Current().ResourceCode, userCodeList);
            }
            else
            {
                msgs = _PerformanceFacade.CheckBeforeGoOffPost(userCodeList);
            }

            if (!msgs.IsSuccess())
            {
                ShowMessage(msgs);

                ucLabelEditUserCodeInput.TextFocus(false, true);
            }
            else
            {
                ultraOptionSetOnOff.Enabled = false;

                if (listBoxUserCodeList.Items.Contains(userCode))
                {
                    if (MessageBox.Show(UserControl.MutiLanguages.ParserString("$Message_RemoveUserCodeConfirm"), this.Text, MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        listBoxUserCodeList.Items.Remove(userCode);
                    }
                }
                else
                {
                    listBoxUserCodeList.Items.Add(userCode);
                }

                ucLabelEditUserCodeInput.TextFocus(true, false);
            }
        }

        private void ucButtonConfirm_Click(object sender, EventArgs e)
        {
            string resCode = ApplicationService.Current().ResourceCode;
            string maintainUserCode = ApplicationService.Current().UserCode;
            List<string> userCodeList = new List<string>();
            foreach (string userCode in listBoxUserCodeList.Items)
            {
                if (!userCodeList.Contains(userCode))
                {
                    userCodeList.Add(userCode);
                }
            }

            if (userCodeList.Count <= 0)
            {
                ShowMessage(new UserControl.Message(MessageType.Error, "$Message_AtLeastOneUser"));
                ucLabelEditUserCodeInput.TextFocus(true, false);
                return;
            }

            Messages msgs = new Messages();
            if (ultraOptionSetOnOff.CheckedIndex == 0)
            {
                msgs = _PerformanceFacade.GoOnPost(resCode, userCodeList, maintainUserCode);
            }
            else
            {
                msgs = _PerformanceFacade.GoOffPost(userCodeList, maintainUserCode);
            }

            ShowMessage(msgs);
            ClearControls();
        }

        private void ucButtonCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        #endregion

        #region On/Off Post functions

        private void ClearControls()
        {
            ultraOptionSetOnOff.Enabled = true;
            listBoxUserCodeList.Items.Clear();
            ucLabelEditUserCodeInput.TextFocus(true, false);
        }

        #endregion

        private void FOnOffPostByUser_Load(object sender, EventArgs e)
        {
            //this.InitPageLanguage();
        }
    }
}