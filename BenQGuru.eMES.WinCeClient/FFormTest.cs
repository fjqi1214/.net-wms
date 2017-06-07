using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.WinCeClient.TestService;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FFormTest : Form
    {
        TestService.TestService testService;
        public FFormTest()
        {
            InitializeComponent();
            testService = new BenQGuru.eMES.WinCeClient.TestService.TestService();
            testService.Url = WebServiceFacade.GetWebServiceURL()+"TestService.asmx";
        }

        private void btnHello_Click(object sender, EventArgs e)
        {
            MessageBox.Show(testService.HelloWorld());
        }

        private void btnLoadGrid_Click(object sender, EventArgs e)
        {
            dataGrid1.DataSource = testService.GetDataTable();
        }

        private void btnGetUser_Click(object sender, EventArgs e)
        {
            string userCode = this.textBox1.Text.Trim();
            if (string.IsNullOrEmpty(userCode))
            {
                MessageBox.Show("UserCode不能为空");
            }
            else
            {
                User user = testService.GetUserInfo(userCode);
                if (user!=null)
                {
                    MessageBox.Show("Name :"+user.UserName);
                }
                else
                {
                    MessageBox.Show("用户不存在");
                }
            }
        }
    }
}