using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Client
{
    public partial class FFrozenReason : Form
    {
        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> Event;
        
        public FFrozenReason()
        {
            InitializeComponent();
        }

        public void OnEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (Event != null)
            {
                Event(sender, e);
            }
        }

        private void ucBtnConfirm_Click(object sender, EventArgs e)
        {
            string frozenReason = FormatHelper.CleanString(this.txtFrozenReason.Value.Trim(), 100);

            if (frozenReason != string.Empty)
            {
                this.OnEvent(null, new ParentChildRelateEventArgs<string>(frozenReason));
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ucBtnCancle_Click(object sender, EventArgs e)
        {
            ApplicationRun.GetInfoForm().Add("$CS_UnFrozenReason_NOT_Be_None");
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FFrozenReason_Load(object sender, EventArgs e)
        {
            this.txtFrozenReason.Value = this.Reason;
            this.txtFrozenReason.TextFocus(false, true);
        }
 
        private string m_Reason = string.Empty;

        public string Reason
        {
            get { return m_Reason; }
            set { m_Reason = value; }
        }

    }

}