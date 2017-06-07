using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using BenQGuru.eMES.Common.MutiLanguage;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// PagerToolbar 的摘要说明。
	/// </summary>
	[DefaultProperty("RowCount"), 
		ToolboxData("<{0}:PagerToolBar runat=server></{0}:PagerToolBar>")]
	public class PagerToolBar : System.Web.UI.WebControls.WebControl, INamingContainer 
	{
		protected System.Web.UI.WebControls.DropDownList listPageIndex	= new DropDownList();
		protected System.Web.UI.WebControls.ImageButton linkPagePrev	= new ImageButton();
		protected System.Web.UI.WebControls.ImageButton linkPageNext	= new ImageButton();
		protected System.Web.UI.WebControls.ImageButton linkPageFirst	= new ImageButton();
		protected System.Web.UI.WebControls.ImageButton linkPageLast	= new ImageButton();
		protected System.Web.UI.WebControls.ImageButton linkGo			= new ImageButton();

		protected System.Web.UI.LiteralControl ltPageIndex = new LiteralControl();
		protected System.Web.UI.LiteralControl ltPageCount = new LiteralControl();
		protected System.Web.UI.LiteralControl ltRowCount  = new LiteralControl();
		protected System.Web.UI.LiteralControl ltGo		   = new LiteralControl();

		protected System.Web.UI.WebControls.TextBox txtPageIndex = new TextBox();
		
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		public System.EventHandler OnPagerToolBarClick;

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
							
			this.listPageIndex.Width = new Unit(60);
			this.listPageIndex.AutoPostBack = true;
			this.txtPageIndex.Width = new Unit(40);
			this.linkGo.ID = "lnkGo";

			this.linkPageFirst.ImageUrl	= string.Format(@"{0}skin/image/PageFirst.gif", this.VirtualHostRoot);
			this.linkPagePrev.ImageUrl	= string.Format(@"{0}skin/image/PagePrev.gif", this.VirtualHostRoot);
			this.linkPageNext.ImageUrl	= string.Format(@"{0}skin/image/PageNext.gif", this.VirtualHostRoot);
			this.linkPageLast.ImageUrl	= string.Format(@"{0}skin/image/PageLast.gif", this.VirtualHostRoot);
			this.linkGo.ImageUrl		= string.Format(@"{0}skin/image/Go.gif", this.VirtualHostRoot);

			this.linkPageFirst.Click += new System.Web.UI.ImageClickEventHandler(this.linkPageFirst_Click);
			this.linkPagePrev.Click += new System.Web.UI.ImageClickEventHandler(this.linkPagePrev_Click);
			this.linkPageNext.Click += new System.Web.UI.ImageClickEventHandler(this.linkPageNext_Click);
			this.linkPageLast.Click += new System.Web.UI.ImageClickEventHandler(this.linkPageLast_Click);
			this.linkGo.Click		+= new ImageClickEventHandler(linkGo_Click);

//			this.listPageIndex.SelectedIndexChanged += new System.EventHandler(this.listPageIndex_SelectedIndexChanged);		
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new EventHandler(PagerToolBar_PreRender);
		}

		private string rowCountPrevText = "共";
		private string rowCountNextText = "笔";

		protected override void CreateChildControls()
		{	
			this.Controls.Add(new LiteralControl("<TABLE cellSpacing='0' cellPadding='0' border='0'>\n"));
			this.Controls.Add(new LiteralControl("	<TR style='FONT-WEIGHT: bold'>\n"));
			
			this.Controls.Add(new LiteralControl("		<TD align='right' noWrap width='80'>"));
			this.Controls.Add(ltPageIndex);
			this.Controls.Add(new LiteralControl(" / "));
			this.Controls.Add(ltPageCount);
			this.Controls.Add(new LiteralControl("</TD>	\n"));
			
			this.Controls.Add(new LiteralControl("		<TD align='center' noWrap width='80'>"));
			this.Controls.Add(ltRowCount);
			this.Controls.Add(new LiteralControl("</TD>\n"));
			
			this.Controls.Add(new LiteralControl("		<TD width='20' noWrap>"));
			this.Controls.Add(linkPageFirst);
			this.Controls.Add(new LiteralControl("</TD>\n"));
			
			this.Controls.Add(new LiteralControl("		<TD width='22' noWrap>"));
			this.Controls.Add(linkPagePrev);
			this.Controls.Add(new LiteralControl("</TD>\n"));			
			
			this.Controls.Add(new LiteralControl("		<TD width='20' noWrap>"));
			this.Controls.Add(linkPageNext);
			this.Controls.Add(new LiteralControl("</TD>\n"));			
			
			this.Controls.Add(new LiteralControl("		<TD width='20' noWrap>"));
			this.Controls.Add(linkPageLast);
			this.Controls.Add(new LiteralControl("</TD>\n"));
			
			this.Controls.Add(new LiteralControl("		<TD class='fieldName' noWrap>"));
			this.Controls.Add(ltGo);
			this.Controls.Add(new LiteralControl("</TD>\n"));
			
			this.Controls.Add(new LiteralControl("		<TD noWrap>"));
			//sammer kong hidden
			this.listPageIndex.Visible = false;
			this.Controls.Add(listPageIndex);
			this.Controls.Add(new LiteralControl("</TD>\n"));

			//sammer kong textbox
			this.Controls.Add(new LiteralControl("		<TD width='48' noWrap>"));
			this.txtPageIndex.BorderStyle = BorderStyle.Groove;
			this.Controls.Add(this.txtPageIndex);
			this.Controls.Add(new LiteralControl("</TD>\n"));

			//sammer kong go button
			this.Controls.Add(new LiteralControl("		<TD width='20' noWrap>"));
			this.Controls.Add(this.linkGo);
			this.Controls.Add(new LiteralControl("</TD>\n"));
			
			this.Controls.Add(new LiteralControl("	</TR>\n"));
			this.Controls.Add(new LiteralControl("</TABLE>\n"));
		}

		#region Property
		public int PageIndex
		{
			get
			{
				if (this.ViewState["$PageIndex"] == null)
				{
					return 1;
				}
				return (int)this.ViewState["$PageIndex"];
			}
			set
			{
				if( value <= 1 )
				{
					this.ViewState["$PageIndex"] = 1;
				}
				else if(  value >=  this.PageCount )
				{
					this.ViewState["$PageIndex"] = this.PageCount;
				}
				else
				{
					this.ViewState["$PageIndex"] = value;
				}

				this.listPageIndex.SelectedValue = this.ViewState["$PageIndex"].ToString();
				this.ltPageIndex.Text = PageIndex.ToString();
				this.txtPageIndex.Text = PageIndex.ToString();
			}
		}

		public int PageCount
		{
			get
			{
				if ( this.RowCount <= 0 )
				{
					return 1;
				}

				if ( this.RowCount % this.PageSize != 0 )
				{
					return this.RowCount/this.PageSize + 1;
				}
				else
				{
					return this.RowCount/this.PageSize;
				}
			}
		}

		public int RowCount
		{
			get
			{
				if (this.ViewState["$RowCount"] == null)
				{
					return 0;
				}
				return (int)this.ViewState["$RowCount"];
			}
			set
			{
				if (value <= 0)
				{
					// 防止除 零
					this.ViewState["$RowCount"] = 0;
					this.linkGo.Enabled = false;
					this.txtPageIndex.ReadOnly = true;
				}
				else
				{
					this.ViewState["$RowCount"] = value;
					this.linkGo.Enabled = true;
					this.txtPageIndex.ReadOnly = false;
				}

				this.ltRowCount.Text  = string.Format( "{0} {1} {2}", rowCountPrevText, RowCount.ToString(), rowCountNextText );
				this.ltPageCount.Text = PageCount.ToString();
			}
		}

		public int PageSize
		{
			get
			{
				if (this.ViewState["$PageSize"] == null)
				{
					return 20;
				}
				return (int)this.ViewState["$PageSize"];
			}
			set
			{
				if (value <=0)
				{
					// 防止除 零
					this.ViewState["$PageSize"] = 1;
				}
				else
				{
					this.ViewState["$PageSize"] = value;
				}
			}
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

		#endregion

		#region Function
		public void InitPager()
		{
			this.PageIndex = 1;

			//sammer kong marked
//			this.listPageIndex.Items.Clear(); 
//			ListItem item = null;
//			for(int i=0; i<this.PageCount; i ++)
//			{
//				item = new ListItem((i+1).ToString(),(i+1).ToString());
//				this.listPageIndex.Items.Add( item );  
//			}
//
//			this.listPageIndex.SelectedValue = this.PageIndex.ToString();
		}		
		
		private void EnableNext(bool enable)
		{
			this.linkPageNext.Enabled	= enable;
			this.linkPageLast.Enabled	= enable;
		}

		private void EnablePrev(bool enable)
		{		
			this.linkPagePrev.Enabled	= enable;
			this.linkPageFirst.Enabled	= enable;
		}
		#endregion

		#region Events
		private void Page_Load(object sender, System.EventArgs e)
		{
			if( !this.Page.IsPostBack )
			{				
				if ( RowCount == 0 )
				{
					this.EnablePrev(false);
					this.EnableNext(false);
					this.linkGo.Enabled = false;
					this.txtPageIndex.Text = "1";
					this.txtPageIndex.ReadOnly = true;
				}
			}

			try
			{
				rowCountPrevText = this.languageComponent1.GetString("lblPrevRowCount");
				rowCountNextText = this.languageComponent1.GetString("lblNextRowCount");

				this.ltRowCount.Text  = string.Format( "{0} {1} {2}", 
					rowCountPrevText,
					RowCount.ToString(), 
					rowCountNextText );
			}
			catch
			{
				this.ltRowCount.Text  = string.Format( "共 {0} 笔", RowCount.ToString() );
			}

			try
			{
				this.ltGo.Text = this.languageComponent1.GetString("lblGo");
			}
			catch
			{
				this.ltGo.Text = "转到";
			}

			this.ltPageIndex.Text = this.PageIndex.ToString();
			this.ltPageCount.Text = this.PageCount.ToString();

			this.txtPageIndex.Attributes["onkeydown"] = "if(event.keyCode==13){" +
															"document.getElementById('" + this.ID + "_lnkGo').click();" + 
															"event.cancelBubble = true;" + 
															"event.returnValue=false;}";

		}

		private void PagerToolBar_PreRender(object sender, EventArgs e)
		{		
			if ( this.PageCount == 1 )
			{
				this.linkGo.Enabled = false;
			}

			this.EnablePrev(true);
			this.EnableNext(true);

			if( this.PageIndex >=  this.PageCount )
			{
				this.EnableNext( false );
			}
			if( this.PageIndex <= 1 )
			{
				this.EnablePrev( false );	
			}
		}

		private void listPageIndex_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex = System.Int32.Parse( this.listPageIndex.SelectedValue );
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.GotoPage));
			}
		}

		private void linkPageNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex += 1;
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.PageNext));
			}		
		}

		private void linkPagePrev_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex -= 1;
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.PagePrev));
			}			
		}

		private void linkPageFirst_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex = 1;
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.PagePrev));
			}			
		}

		private void linkPageLast_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex = this.PageCount;
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.PagePrev));
			}		
		}

		private void linkGo_Click(object sender, ImageClickEventArgs e)
		{
			if (this.OnPagerToolBarClick != null)
			{
				//					this.PageIndex = System.Int32.Parse( this.listPageIndex.SelectedValue );
		
				int pageIndex = 0;
				try
				{
					pageIndex = System.Int32.Parse(this.txtPageIndex.Text);
				}
				catch
				{
					pageIndex = this.PageIndex;
					this.txtPageIndex.Text = this.PageIndex.ToString();

					return;
				}
					
				if ( this.PageIndex == pageIndex )
				{
					return;
				}

				this.PageIndex = pageIndex;
					
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.GotoPage));		
			}
		}
		#endregion

	}

	public enum ToolBarEnum
	{
		PageNext = 0,
		PagePrev = 1,
		GotoPage = 2
	}

	public class PagerToolBarEventArgs: System.EventArgs
	{
		public PagerToolBarEventArgs(ToolBarEnum toolBarEnum)
		{
			this.ToolBarEnum = toolBarEnum;
		}
		public ToolBarEnum ToolBarEnum;
	}
}
