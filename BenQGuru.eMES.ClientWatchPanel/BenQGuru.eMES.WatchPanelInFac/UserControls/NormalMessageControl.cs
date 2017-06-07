using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class NormalMessageControl : System.Windows.Forms.UserControl
    {
        public NormalMessageControl()
        {
            InitializeComponent();
        }

        public string PlanQty
        {
            set { this.lblPlanQty.Text = value; }
        }

        public string OutPutQty
        {
            set { this.lblOutPutQty.Text = value; }
        }

        public string Crew
        {
            set { this.lblCrewList.Text = value; }
        }

        public string OnPostManCount
        {
            set { this.lblOnPostManCount.Text = value; }
        }

    }
}
