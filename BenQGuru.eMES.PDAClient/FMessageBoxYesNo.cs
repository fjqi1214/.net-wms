using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.PDAClient
{
    public partial class FMessageBoxYesNo : Form
    {
        private int _X = 0;
        private int _Y = 0;        
        private bool _Flag = false;
        public bool Flag
        {
            get
            {
                return _Flag;
            }            
        }
        public FMessageBoxYesNo()
        {
            InitializeComponent();
        }

        public FMessageBoxYesNo(string message, int X, int Y)
        {
            InitializeComponent();
            _X = X;
            _Y = Y;
            this.label1.Text = string.Empty;
            this.label1.Text = UserControl.MutiLanguages.ParserMessage(message);
        }

        public FMessageBoxYesNo(string message)
        {
            InitializeComponent();
            _X = 5;
            _Y = 30;
            this.label1.Text = string.Empty;
            this.label1.Text = UserControl.MutiLanguages.ParserMessage(message);
        }

        private void FMessageBoxYesNo_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(235, 240, 247);
            this.Font = new Font("Arial", 9);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            Point pt = new Point(_X, _Y);
            this.Location = pt;
            this.ultraButtonNo.Focus();
        }

        private void ultraButtonYes_Click(object sender, EventArgs e)
        {
            _Flag = true;
            this.Close();
        }

        private void ultraButtonNo_Click(object sender, EventArgs e)
        {
            _Flag = false;
            this.Close();
        }
        
    }
}
