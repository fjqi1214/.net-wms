using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.WatchPanelDisToLine
{
    public partial class ExceptionMessageControl : System.Windows.Forms.UserControl
    {
        public ExceptionMessageControl()
        {
            InitializeComponent();
        }

        public string  ExceptionMessage
        {
            set { this.txtExpection.Text = value; }
            get { return this.txtExpection.Text; }
        }

        public Color MessageColor
        {
            set { this.txtExpection.ForeColor = value; }
            get { return this.txtExpection.ForeColor; }
 
        }

        public void ClearMessage()
        {
            this.txtExpection.Text=string.Empty;
        }
    }
}
