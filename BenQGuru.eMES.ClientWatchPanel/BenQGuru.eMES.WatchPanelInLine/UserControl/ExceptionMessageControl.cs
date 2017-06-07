using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class ExceptionMessageControl : System.Windows.Forms.UserControl
    {
        public ExceptionMessageControl()
        {
            InitializeComponent();
        }

        public string ExpectionMessage
        {
            set { this.txtExpection.Text = value; }
            get { return this.txtExpection.Text; }
        }

        public void ClearMessage()
        {
            this.txtExpection.Clear();
        }
    }
}
