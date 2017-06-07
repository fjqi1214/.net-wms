using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using BenQGuru.eMES.Web.Helper ;
namespace BenQGuru.eMES.Web.SelectQuery
{


    
    [DefaultProperty("Text"), 
    ToolboxData("<{0}:SelectableTextBox4MO runat=server></{0}:SelectableTextBox4MO>")]
    public class SelectableTextBox4MO : System.Web.UI.WebControls.WebControl, INamingContainer 
    {
	
        [Bindable(true), 
        Category("Appearance")] 
        public string Text 
        {
            get
            {
                return this.txtCodes.Text;
            }

            set
            {
                this.txtCodes.Text = value;
            }
        }

        /// <summary>
        /// sammmer kong
        /// </summary>
        [Bindable(true), 
        Category("Appearance"), 
        DefaultValue("是否可用")] 
        public bool Readonly 
        {
            get
            {
                if ( this.ViewState["{ReadOnly}"] == null )
                {
                    this.ViewState["{ReadOnly}"] = "false";
                }
                return System.Boolean.Parse(this.ViewState["{ReadOnly}"].ToString());
            }
            set
            {
                this.ViewState["{ReadOnly}"] = value.ToString();			
                //				this.drpPageSize.Enabled = !value;
            }
        }
		


        /// <summary>
        /// vizo
        /// </summary>
        [Bindable(true), 
        Category("Appearance"), 
        DefaultValue("起始时间")] 
        public int StartTime
        {
            get
            {
                if ( this.ViewState["{StartTime}"] == null )
                {
                    this.ViewState["{StartTime}"] = FormatHelper.TODateInt( DateTime.MinValue ) ;
                }
                return FormatHelper.TODateInt( this.ViewState["{StartTime}"].ToString() );
            }
            set
            {
                this.ViewState["{StartTime}"] = value.ToString();
                this.cmdSelectCodes.Attributes.Add("DocumentStartTime",value.ToString()) ;
            }
        }
		


        /// <summary>
        /// vizo
        /// </summary>
        [Bindable(true), 
        Category("Appearance"), 
        DefaultValue("结束时间")] 
        public int EndTime
        {
            get
            {
                if ( this.ViewState["{EndTime}"] == null )
                {
                    this.ViewState["{EndTime}"] = FormatHelper.TODateInt( DateTime.MinValue ) ;
                }
                return FormatHelper.TODateInt( this.ViewState["{EndTime}"].ToString() );
            }
            set
            {
                this.ViewState["{EndTime}"] = value.ToString();
                this.cmdSelectCodes.Attributes.Add("DocumentEndTime",value.ToString()) ;
            }
        }
		


        [Bindable(true), 
        Category("Appearance"), 
        DefaultValue("类型")] 
        public string Type 
        {
            get
            {
                if ( this.ViewState["{Type}"] == null )
                {
                    this.ViewState["{Type}"] = SelectableTextBoxType.Model;
                }
                return this.ViewState["{Type}"].ToString();
            }
            set
            {
                this.ViewState["{Type}"] = value.ToString();

                this.cmdSelectCodes.Attributes.Add("DocumentType",value.ToString()) ;
            }
        }
		
        [Bindable(true), 
        Category("Appearance"), 
        DefaultValue("类型")] 
        public string Target 
        {
            get
            {
                if ( this.ViewState["{Target}"] == null )
                {
                    this.ViewState["{Target}"] = SelectableTextBoxType.Model;
                }
                return this.ViewState["{Target}"].ToString();
            }
            set
            {
                this.ViewState["{Target}"] = value.ToString();

                this.cmdSelectCodes.Attributes.Add("Target",value.ToString()) ;
            }
        }

        protected System.Web.UI.WebControls.TextBox txtCodes = new System.Web.UI.WebControls.TextBox();
        protected System.Web.UI.HtmlControls.HtmlInputButton cmdSelectCodes = new System.Web.UI.HtmlControls.HtmlInputButton();
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private string targetFile = "FMOSP2.aspx" ;
        
        
        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();

            base.OnInit (e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.languageComponent1.Language = SessionHelper.Current(this.Page.Session).Language; 
            this.Load += new EventHandler(SelectableTextBox_Load);

        }

        protected override void CreateChildControls()
        {			

            this.txtCodes.Attributes.Add("class","textbox");
            // 设置打开哪个文件
            this.cmdSelectCodes.Attributes.Add("TargetFile",targetFile) ;
            this.cmdSelectCodes.Attributes.Add("style",@"BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(" + this.VirtualHostRoot + @"SKIN\\Image\\right.gif); BORDER-LEFT: 0px; WIDTH: 24px; CURSOR: pointer; BORDER-BOTTOM: 0px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 24px; BACKGROUND-COLOR: transparent") ;
            this.cmdSelectCodes.Attributes.Add("onclick","DoSelectableTextBoxClick(this);event.returnValue=false;event.cancelBubble=true;") ;

            this.Controls.Add(new LiteralControl("<TABLE cellSpacing='0' cellPadding='0' border='0'>\n"));
            this.Controls.Add(new LiteralControl("	<TR>\n"));
			
            this.Controls.Add(new LiteralControl("		<TD class='fieldName'>"));
            this.Controls.Add( this.txtCodes );
            this.Controls.Add(new LiteralControl("</TD>\n"));
			
            this.Controls.Add(new LiteralControl("		<TD>"));
            this.Controls.Add( this.cmdSelectCodes );
            this.Controls.Add(new LiteralControl(" </TD>\n"));
			
            this.Controls.Add(new LiteralControl("	</TR>\n"));
            this.Controls.Add(new LiteralControl("</TABLE>\n"));
        }


        private void SelectableTextBox_Load(object sender, EventArgs e)
        {
            
            this.cmdSelectCodes.Disabled = this.Readonly ;
            this.txtCodes.Enabled = !this.Readonly ;

            if(!this.Page.IsStartupScriptRegistered("SelectableTextBox_Startup_js"))
            {
                string scriptString = string.Format("<script>var STB_Virtual_Path = \"{0}\";</script><script src='{0}SelectQuery/selectableTextBox.js'></script>",this.VirtualHostRoot ) ;
                
                this.Page.RegisterStartupScript("SelectableTextBox_Startup_js", scriptString);
            }
            this.Page.ClientScript.RegisterClientScriptInclude("Jquery", this.VirtualHostRoot + "Scripts/jquery-1.9.1.js");
        }

        private string VirtualHostRoot
        {
            get
            {
                //				return string.Format("http://{0}{1}{2}"
                //					, this.Request.Url.Host 
                //					, this.Request.Url.Segments[0]
                //					, this.Request.Url.Segments[1]);

                return string.Format("{0}{1}"
                    , this.Page.Request.Url.Segments[0]
                    , this.Page.Request.Url.Segments[1]);
            }
        }

        public System.Web.UI.WebControls.TextBox TextBox
        {
            get
            {
                return this.txtCodes;
            }
        }
    }
}
