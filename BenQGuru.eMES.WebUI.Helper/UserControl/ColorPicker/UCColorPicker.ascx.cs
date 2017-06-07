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

namespace BenQGuru.eMES.Web.UserControl
{
    public partial class UCColorPicker : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string Enable
        {
            set
            {
                this.Attributes["isEnable"] = value;
            }
            get
            {
                if (this.Attributes["isEnable"] == null)
                {
                    return "true";
                }
                return this.Attributes["isEnable"];
            }
        }

        public string GetBaseUrl()
        {
            return this.VirtualHostRoot;
        }

        public string GetColorPickerJsFileUrl()
        {
            return GetBaseUrl() + "UserControl/ColorPicker/ColorPicker.js";
        }

        public string VirtualHostRoot
        {
            get
            {
                return string.Format("{0}{1}"
                    , this.Request.Url.Segments[0]
                    , this.Request.Url.Segments[1]);
            }
        }

        public string ColorSampleCellId
        {
            get
            {
                return this.tdColorSample.ClientID;
            }
        }
        public string ColorValueControlId
        {
            get
            {
                return this.hidColorPicker.ClientID;
            }
        }

        public string Value
        {
            set
            {
                this.tdColorSample.BgColor = value;
                this.hidColorPicker.Value = value;
            }
            get
            {
                return this.hidColorPicker.Value;
            }
        }

    }
}