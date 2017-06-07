using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using BenQGuru.eMES.Web.Helper ;
namespace BenQGuru.eMES.Web.SelectQuery
{


    
    [DefaultProperty("Text"), 
    ToolboxData("<{0}:SelectableTextBox4SS runat=server></{0}:SelectableTextBox4SS>")]
    public class SelectableTextBox4SS : System.Web.UI.WebControls.WebControl, INamingContainer 
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
        DefaultValue("Segment代码")] 
        public string Segment 
        {
            get
            {
                if ( this.ViewState["{Segment}"] == null )
                {
                    this.ViewState["{Segment}"] = string.Empty ;
                }
                return this.ViewState["{Segment}"].ToString();
            }
            set
            {
                this.ViewState["{Segment}"] = value.ToString();
                this.cmdSelectCodes.Attributes.Add("DocumentSegment",value.ToString()) ;
            }
        }

        [Bindable(true),
        Category("Appearance"),
        DefaultValue("TextBox是否可输入")]
        public bool CanKeyIn
        {
            get
            {
                if (this.ViewState["{CanKeyIn}"] == null)
                {
                    this.ViewState["{CanKeyIn}"] = "false";
                }
                return System.Boolean.Parse(this.ViewState["{CanKeyIn}"].ToString());
            }
            set
            {
                this.ViewState["{CanKeyIn}"] = value.ToString();
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
        private string targetFile = "FStepSequenceSP2.aspx" ;
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




        public SelectableTextBox4SS()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
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

        public new string CssClass
        {
            get
            {
                return this.txtCodes.CssClass;
            }
            set
            {
                this.txtCodes.CssClass = value;
            }
        }
    }
}
