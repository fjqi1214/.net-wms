using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// PagerSizeSelector 的摘要说明。
	/// sammer kong 2005/05/17 page size read only
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:PagerSizeSelector runat=server></{0}:PagerSizeSelector>")]
	public class PagerSizeSelector : System.Web.UI.WebControls.WebControl, INamingContainer 
	{

		// Added by Icyer 2005/09/07
		// 每页显示数改变时，需要刷新Grid
		public delegate void PagerSizeChangedHandle (object sender, int pagerSize);
		public event PagerSizeChangedHandle OnPagerSizeChanged;
		// Added end
		
		private string text = "每页显示行数";
	
		[Bindable(true), 
			Category("Appearance"), 
			DefaultValue("每页显示行数")] 
		public string Text 
		{
			get
			{
				return text;
			}

			set
			{
				text = value;
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
		
		protected System.Web.UI.WebControls.Label lblPageSize = new Label();
		protected System.Web.UI.WebControls.DropDownList drpPageSize = new DropDownList();
        protected System.Web.UI.WebControls.Button btnPostback = new Button();
		
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

			this.Load += new EventHandler( this.PagerSizeSelector_Load );
			//this.drpPageSize.SelectedIndexChanged +=new EventHandler(drpPageSize_SelectedIndexChanged);
            this.btnPostback.Click += new EventHandler(btnPostback_Click);
		}


		/* modified by jessie lee
		 * CS191 将默认改为50 */
		[Bindable(true), Browsable(false),
			Category("Appearance"), 
			DefaultValue("50")] 
		public int PageSize
		{
			get
			{
				this.EnsureChildControls();
				if( this.ViewState["{PageSize}"] == null )
				{
					this.ViewState["{PageSize}"] = "50";
				}
				return System.Int32.Parse( this.ViewState["{PageSize}"].ToString() );
			}
			set
			{			
				this.EnsureChildControls();
				int pageSize = value;
				if( pageSize <= 0 )
				{		
					pageSize = 50;
				}
				try
				{
					this.drpPageSize.SelectedValue = pageSize.ToString();
				}
				catch
				{
					this.drpPageSize.SelectedValue = "50";					
				}
				finally
				{
					this.ViewState["{PageSize}"] = this.drpPageSize.SelectedValue;
				}
			}
		}

		protected override void CreateChildControls()
		{			
			this.lblPageSize.Text = Text;

			this.drpPageSize.Items.Add( new ListItem("10", "10") );
			this.drpPageSize.Items.Add( new ListItem("20", "20") );
			this.drpPageSize.Items.Add( new ListItem("50", "50") );
			this.drpPageSize.Items.Add( new ListItem("100", "100") );
            
			this.drpPageSize.SelectedValue = this.PageSize.ToString();	
			this.drpPageSize.AutoPostBack = false;

            //mdify by jinger 20160224 解决页面出现多个PagerSizeSelector改变下拉框值不能触发事件问题
            //this.drpPageSize.Attributes.Add("onchange", "$('#pagerSizeSelector_btnPostback').click();");
            this.drpPageSize.Attributes.Add("onchange", "$('#" + this.ID + "_btnPostback').click();");
            //end mdify

            this.btnPostback.Style.Add("display","none");
            this.btnPostback.ID = "btnPostback";

			this.Controls.Add(new LiteralControl("<TABLE cellSpacing='0' cellPadding='0' border='0'>\n"));
			this.Controls.Add(new LiteralControl("	<TR>\n"));
			
			this.Controls.Add(new LiteralControl("		<TD class='fieldName'>"));
			this.Controls.Add( this.lblPageSize );
			this.Controls.Add(new LiteralControl("</TD>\n"));
			
			this.Controls.Add(new LiteralControl("		<TD>"));
			this.Controls.Add( this.drpPageSize );
            this.Controls.Add(this.btnPostback);
			this.Controls.Add(new LiteralControl(" </TD>\n"));
			
			this.Controls.Add(new LiteralControl("	</TR>\n"));
			this.Controls.Add(new LiteralControl("</TABLE>\n"));
		}

		private void PagerSizeSelector_Load(object sender, EventArgs e)
		{		
			this.drpPageSize.Width = new Unit(60);	

//			if ( !this.Page.IsPostBack )
//			{
//				this.PageSize = 20;

				try
				{
					this.lblPageSize.Text = this.languageComponent1.GetString("pagerSizeSelector");
				}
				catch
				{
					this.lblPageSize.Text = this.Text;
				}
//			}

			this.drpPageSize.Enabled = !this.Readonly;
		}

        void btnPostback_Click(object sender, EventArgs e)
        {
            try
            {
                this.PageSize = System.Int32.Parse(this.drpPageSize.SelectedValue);
            }
            catch
            {
                this.PageSize = 50;
            }

            if (OnPagerSizeChanged != null)
            {
                OnPagerSizeChanged(this, this.PageSize);
            }
        }

        private void drpPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.PageSize = System.Int32.Parse(this.drpPageSize.SelectedValue);
            }
            catch
            {
                this.PageSize = 50;
            }

            // Added by Icyer 2005/09/07
            // 每页显示数改变时，需要刷新Grid
            if (OnPagerSizeChanged != null)
            {
                OnPagerSizeChanged(this, this.PageSize);
            }

            // Added end
        }
	}
}
