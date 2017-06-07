using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using UserControl;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class FacMessageControl : System.Windows.Forms.UserControl
    {
        public FacMessageControl()
        {
            InitializeComponent();
        }

        private void FacMessageControl_Load(object sender, EventArgs e)
        {
            this.richTextMessage.Rtf = this.RTF;
        }

        private string m_RTF;

        public string RTF
        {
            get { return m_RTF; }
            set { m_RTF = value; }
        }

        public void ValueRefresh()
        {
            this.richTextMessage.Rtf = this.RTF;
        }

    }
}
