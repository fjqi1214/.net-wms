using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using BenQGuru.eMES.Web.Helper ;

namespace BenQGuru.eMES.Web.SelectQuery
{

	[DefaultProperty("Text"), 
	ToolboxData("<{0}:SelectSingletableTextBox runat=server></{0}:SelectSingletableTextBox>")]
	public class SelectSingletableTextBox : System.Web.UI.WebControls.WebControl, INamingContainer 
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

			this.txtCodes.ID = this.ID + "_01";
			this.cmdSelectCodes.ID = this.ID + "_02";

			this.txtCodes.Attributes.Add("class","textbox");
			this.cmdSelectCodes.Attributes.Add("style",@"BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(" + this.VirtualHostRoot + @"SKIN\\Image\\right.gif); BORDER-LEFT: 0px; WIDTH: 24px; CURSOR: pointer; BORDER-BOTTOM: 0px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 24px; BACKGROUND-COLOR: transparent") ;
			this.cmdSelectCodes.Attributes.Add("onclick","DoSelectableTextBoxClick(this);event.returnValue=false;event.cancelBubble=true;") ;

			this.Controls.Add( this.txtCodes );
			this.Controls.Add( this.cmdSelectCodes );

		}

		protected override void Render(HtmlTextWriter writer)
		{
            //this.txtCodes.ReadOnly = this.Readonly;
			//this.cmdSelectCodes.Disabled = this.Readonly ;
            SetTextBoxStatus(this.Enabled && !this.Readonly && this.CanKeyIn);
            SetSelectButtonStatus(this.Enabled && !this.Readonly);			

			writer.Write("<TABLE cellSpacing='0' cellPadding='0' border='0'>\n");
			writer.Write("	<TR>\n");
			
			writer.Write("		<TD class='fieldName'>");
			this.txtCodes.RenderControl(writer) ;
			writer.Write("</TD>\n");
			
			writer.Write("		<TD>");
			this.cmdSelectCodes.RenderControl(writer) ;
			writer.Write(" </TD>\n");
			
			writer.Write("	</TR>\n");
			writer.Write("</TABLE>\n");
		}

		public SelectSingletableTextBox()
		{
		}

		private void SelectableTextBox_Load(object sender, EventArgs e)
		{
            SetTextBoxStatus(this.Enabled && !this.Readonly && this.CanKeyIn);
            SetSelectButtonStatus(this.Enabled && !this.Readonly);			

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

        private void SetTextBoxStatus(bool editable)
        {
            if (editable)
            {
                this.txtCodes.Attributes.Remove("readonly");
            }
            else
            {
                this.txtCodes.Attributes.Remove("readonly");
                this.txtCodes.Attributes.Add("readonly", "readonly");
            }
        }

        private void SetSelectButtonStatus(bool enabled)
        {
            if (enabled)
            {
                this.cmdSelectCodes.Attributes.Remove("style");
                this.cmdSelectCodes.Attributes.Add("style", @"BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(" + this.VirtualHostRoot + @"SKIN\\Image\\right.gif); BORDER-LEFT: 0px; WIDTH: 24px; CURSOR: pointer; BORDER-BOTTOM: 0px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 24px; BACKGROUND-COLOR: transparent");
                this.cmdSelectCodes.Attributes.Add("onclick", "DoSelectableTextBoxClick(this);event.returnValue=false;event.cancelBubble=true;");
            }
            else
            {
                this.cmdSelectCodes.Attributes.Remove("style");
                this.cmdSelectCodes.Attributes.Add("style", @"BORDER-RIGHT: 0px; BORDER-TOP: 0px; FONT-SIZE: 12px; BACKGROUND-IMAGE: url(" + this.VirtualHostRoot + @"SKIN\\Image\\right.gif); BORDER-LEFT: 0px; WIDTH: 24px; CURSOR: none; BORDER-BOTTOM: 0px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 24px; BACKGROUND-COLOR: transparent");
                this.cmdSelectCodes.Attributes.Remove("onclick");
            }
        }
	}
}
