using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class HearMessageControl : System.Windows.Forms.UserControl
    {
        public HearMessageControl()
        {
            InitializeComponent();
        }

        private void HearMessageControl_Load(object sender, EventArgs e)
        {
            this.lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
            //this.labelBigLine.Text = "TD线电子看板";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new System.Globalization.CultureInfo("en-US"));
            this.lblDate.Refresh();
        }

        public string BigLine
        {
            set { this.labelBigLine.Text = value+"线电子看板"; }
        }

        public string SetTitle
        {
            set { this.labelBigLine.Text = value; }
        }
    }
}
