using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.BaseDataModel
{
    public partial class FImpLine : FImpFormBase
    {
        public FImpLine()
        {
            InitializeComponent();
        }

        private void btnGenData_Click(object sender, EventArgs e)
        {
            FAutoTemplate f = new FAutoTemplate();
            f.ShowDialog();
            if (f.DialogResult != DialogResult.OK)
            {
                return;
            }
            string strPrefix = f.DataPrefix;
            int iDataSeqStart = f.DataSeqStart;
            int iDataSeqEnd = f.DataSeqEnd;
            int iDataSeqLen = f.DataSeqLen;
            List<string> listLine = new List<string>();
            for (int i = iDataSeqStart; i <= iDataSeqEnd; i++)
            {
                string strTmp = strPrefix;
                if (iDataSeqLen > 0)
                    strTmp += i.ToString().PadLeft(iDataSeqLen, '0');
                else
                    strTmp += i.ToString();
                listLine.Add(strTmp);
            }
            int iLine = 1;
            Excel.Worksheet sheet = (Excel.Worksheet)excelApp.ActiveSheet;
            while (true)
            {
                Excel.Range range = (Excel.Range)sheet.Cells[iLine, 1];
                if (range.Value2 == null || range.Value2.ToString() == "")
                    break;
                iLine++;
            }
            for (int i = 0; i < listLine.Count; i++)
            {
                Excel.Range range = (Excel.Range)sheet.Cells[iLine + i, 1];
                range.Value2 = listLine[i];
            }
        }

    }
}