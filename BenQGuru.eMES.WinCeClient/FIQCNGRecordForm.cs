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
    public partial class FIQCNGRecordForm : Form
    {
        public FIQCNGRecordForm(string iqcNo, string cartonNo, string dqMCode)
        {
            InitializeComponent();
            FIQCNGRecord f = new FIQCNGRecord(iqcNo, cartonNo, dqMCode, this);
            this.Controls.Add(f);
            f.Dock = DockStyle.Fill;

            f.Visible = true;
        }
    }
}