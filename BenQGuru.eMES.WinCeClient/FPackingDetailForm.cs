using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FPackingDetailForm : Form
    {
        public FPackingDetailForm(string pickNo, string carInvNo)
        {
            InitializeComponent();
            FPackingDetail f = new FPackingDetail(pickNo, carInvNo, this);
            this.Controls.Add(f);
            f.Dock = DockStyle.Fill;

            f.Visible = true;
        }
    }
}