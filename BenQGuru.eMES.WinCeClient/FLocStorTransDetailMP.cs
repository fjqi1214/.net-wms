using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
//using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FLocStorTransDetailMP : UserControl
    {
        LocStorTrans.LocStorTrans LocStorTransService;
        public FLocStorTransMP Fm;
  
        public FLocStorTransDetailMP( )
         {
            InitializeComponent();
            LocStorTransService = new BenQGuru.eMES.WinCeClient.LocStorTrans.LocStorTrans();
         }

        public FLocStorTransDetailMP(string transNo, string dqmCode, string fromCartonno, string locationCode, string cartonno)
        {
            InitializeComponent();
            LocStorTransService = new BenQGuru.eMES.WinCeClient.LocStorTrans.LocStorTrans();
            LocStorTransService.Url = WebServiceFacade.GetWebServiceURL() + "LocStorTrans.asmx";
            txtLocationNoEdit.Text = transNo;
            DataTable dt = new DataTable();
            dt = LocStorTransService.LocStorTransView(transNo, dqmCode, fromCartonno, locationCode, cartonno);
            this.dataGrid1.DataSource = dt;
        
        }


        private void btnReturn_Click(object sender, EventArgs e)
        {
            FLocStorTransMP fLocStorTransMp = new FLocStorTransMP();
            this.Visible = false;
            this.Parent.Controls.Add(fLocStorTransMp);
            //this.Hide();
        }


        #region 自定义方法
        //this.dataGrid1.DisplayLayout.Bands[0].Columns["SerialNo"].Width = 205;
        //private void FLocStorTransDetailMpLoad()
        //{
        //    string transNos = Fm.Text;
        //    string transNo = txtLocationNoEdit.Text.Trim().ToUpper();
        //    string dqmCode = txtDQMoCodeEdit.Text.Trim().ToUpper();

        //    string locationCode = txtTLocationCodeEdit.Text;//目标货位
        //    string fromCartonno = txtOriginalCartonEdit.Text;//原箱号 FromCARTONNO 
        //    string cartonno =  txtTLocationCartonEdit.Text;//目标箱号
        //    DataTable dt = new DataTable();
        //    dt = LocStorTransService.LocStorTransView(transNo, dqmCode, fromCartonno, locationCode, cartonno);
        //    this.dataGrid1.DataSource = dt;
        //}
        #endregion
    }
}
