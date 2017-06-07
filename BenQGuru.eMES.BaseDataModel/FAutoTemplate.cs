using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.BaseDataModel
{
    public partial class FAutoTemplate : Form
    {
        public FAutoTemplate()
        {
            InitializeComponent();
        }

        public string DataPrefix = "";
        public int DataSeqStart = -1;
        public int DataSeqEnd = 0;
        public int DataSeqLen = 0;
        public void ShowInitData()
        {
            this.txtPrefix.Text = DataPrefix;
            this.txtStart.Text = DataSeqStart.ToString();
            this.txtEnd.Text = DataSeqEnd.ToString();
            this.txtSeqLength.Text = DataSeqLen.ToString();
            this.btnGenerate_Click(null, null);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int iStart = 0;
            int iEnd = 0;
            try
            {
                iStart = int.Parse(this.txtStart.Text);
            }
            catch
            {
                MessageBox.Show("起始序列错误");
                return;
            }
            try
            {
                iEnd = int.Parse(this.txtEnd.Text);
            }
            catch
            {
                MessageBox.Show("结束序列错误");
                return;
            }

            int iLen = 0;
            try
            {
                if (this.txtSeqLength.Text != "")
                    iLen = int.Parse(this.txtSeqLength.Text);
            }
            catch
            {
                MessageBox.Show("序列长度错误");
                return;
            }

            string strResult = "";
            for (int i = iStart; i <= iEnd; i++)
            {
                string strTmp = this.txtPrefix.Text.ToUpper().Trim();
                if (iLen > 0)
                    strTmp += i.ToString().PadLeft(iLen, '0');
                else
                    strTmp += i.ToString();
                strResult += strTmp + "\r\n";
            }
            this.txtResult.Text = strResult;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                DataSeqStart = int.Parse(this.txtStart.Text);
            }
            catch
            {
                MessageBox.Show("起始序列错误");
                return;
            }
            try
            {
                DataSeqEnd = int.Parse(this.txtEnd.Text);
            }
            catch
            {
                MessageBox.Show("结束序列错误");
                return;
            }

            DataSeqLen = 0;
            try
            {
                if (this.txtSeqLength.Text != "")
                    DataSeqLen = int.Parse(this.txtSeqLength.Text);
            }
            catch
            {
                MessageBox.Show("序列长度错误");
                return;
            }

            this.DataPrefix = this.txtPrefix.Text.ToUpper().Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}