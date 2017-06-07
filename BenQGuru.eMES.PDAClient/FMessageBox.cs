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
    public partial class FMessageBox : Form
    {
        private int _X = 0;
        private int _Y = 0;
        public FMessageBox()
        {
            InitializeComponent();
        }

        public FMessageBox(string message)
        {
            InitializeComponent();
            _X = 5;
            _Y = 30;
            this.richTextBox1.Text = string.Empty;
            this.richTextBox1.Text = UserControl.MutiLanguages.ParserMessage(message);
        }

        public FMessageBox(string message, int X, int Y)
        {
            InitializeComponent();
            _X = X;
            _Y = Y;
            this.richTextBox1.Text = string.Empty;
            this.richTextBox1.Text = UserControl.MutiLanguages.ParserMessage(message);
        }

        private void FMessageBox_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(235, 240, 247);
            this.Font = new Font("Arial", 9);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            Point pt = new Point(_X, _Y);
            this.Location = pt;
            this.richTextBox1.BackColor = Color.FromArgb(235, 240, 247);

        }

        private void ultraButtonOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
