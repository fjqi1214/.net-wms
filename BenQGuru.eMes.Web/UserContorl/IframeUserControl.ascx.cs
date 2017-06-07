using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace BenQuru.eMES.Web.UserContorl
{
    public partial class IframeUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetIframeA(string name, string url)
        {
            iframe1.Attributes.Add("src", url);
            lblTitle1.Text = name;
        }

        public void SetIframeB(string name, string url)
        {
            iframe2.Attributes.Add("src", url);
            lblTitle2.Text = name;
        }

        public void SetIframeC(string name, string url)
        {
            iframe3.Attributes.Add("src", url);
            lblTitle3.Text = name;
        }

        public void SetIframeD(string name, string url)
        {
            iframe4.Attributes.Add("src", url);
            lblTitle4.Text = name;
        }

    }
}