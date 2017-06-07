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
    public partial class FPicture : Form
    {
        public FPicture(string asn)
        {
            InitializeComponent();
            FUploadPicture f = new FUploadPicture(asn, this);
            this.Controls.Add(f);
            f.Dock = DockStyle.Fill;

            f.Visible = true;


        }

        public void Asn(string asn)
        {
            this.fUploadPicture1.STNo = asn;
        }
    }
}