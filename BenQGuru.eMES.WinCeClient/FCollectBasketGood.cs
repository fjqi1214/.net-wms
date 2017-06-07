using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.WinCeClient.CollectGoodService;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FCollectBasketGood : UserControl
    {
        CollectGoodService.CollectGoodService CollectGood;
        string usercode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();
        string rescode = ApplicationService.Current().LoginInfo.Resource.ToUpper();
        public FCollectBasketGood()
        {
            InitializeComponent();
            CollectGood = new BenQGuru.eMES.WinCeClient.CollectGoodService.CollectGoodService();
            CollectGood.Url = WebServiceFacade.GetWebServiceURL() + "CollectGoodService.asmx";
            
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtGoodCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                {
                    string message = string.Empty;
                    string GoodCode = string.Empty;
                    GoodCode = this.txtGoodCode.Text.Trim().ToUpper();

                    if (GoodCode == string.Empty)
                    {
                        this.txtmessage.Text = "请输入良品条码";
                        return;
                    }
                    message = CollectGood.CollectGood(GoodCode, usercode, rescode);
                    if (message == "OK")
                    {
                        this.txtmessage.Text = GoodCode+"良品采集OK";
                        this.txtGoodCode.Text = "";
                        this.txtGoodCode.Focus();
                    }
                    else { 
                           this.txtmessage.Text = message;
                           this.txtGoodCode.Text = "";
                           this.txtGoodCode.Focus();
                         }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Enums.WinCE_MsgBox_Title_Tips);
            }
        }

        private void FCollectBasketGood_Paint(object sender, PaintEventArgs e)
        {
            this.txtGoodCode.Focus();
            this.txtGoodCode.Text = "";
        }
    }
}
