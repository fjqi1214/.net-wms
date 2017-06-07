namespace BenQGuru.eMES.Web.UserControl
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		PagerToolBar 的摘要说明。
	/// </summary>
	public class PagerToolBar : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if( !this.IsPostBack )
			{
				this.linkPageNext.Enabled = false;
				this.linkPagePrev.Enabled = false;
			}
		}

		protected System.Web.UI.WebControls.LinkButton linkPageNext;
		protected System.Web.UI.WebControls.DropDownList listPageIndex;
		protected System.Web.UI.WebControls.LinkButton linkPagePrev;

		public System.EventHandler OnPagerToolBarClick;

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
				this.linkPageNext.Enabled = true;
				this.linkPagePrev.Enabled = true;

				if (value <=0)
				{
					// 防止除 零
					this.ViewState["$PageIndex"] = 1;
				}
				else
				{
					if( value >=  this.PageCount )
					{			
						this.linkPageNext.Enabled = false;
						this.ViewState["$PageIndex"] = this.PageCount;
					}
					else if( value <=1 )
					{
						this.linkPagePrev.Enabled = false;
						this.ViewState["$PageIndex"] = 1;
					}
					else
					{
						this.ViewState["$PageIndex"] = value;
					}
					this.listPageIndex.SelectedValue = this.ViewState["$PageIndex"].ToString();
				}
			}
		}

		public int GotoPageIndex
		{
			get
			{
				if (this.listPageIndex.SelectedItem == null)
				{
					return 0;
				}

				if (this.listPageIndex.SelectedItem.Text.Trim() == "")
				{
					return 0;
				}

				return System.Int32.Parse(this.listPageIndex.SelectedItem.Text.Trim());
			}
			set
			{
				if (value <=0)
				{
					// 防止除 零
					this.ViewState["$PageIndex"] = 0;
				}
				else
				{
					this.ViewState["$PageIndex"] = value;
				}
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
				if (value <=0)
				{
					// 防止除 零
					this.ViewState["$RowCount"] = 0;
				}
				else
				{
					this.ViewState["$RowCount"] = value;
				}
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


		public void InitPager()
		{
			this.PageIndex = 1;

			if (this.PageIndex <= 1)
			{
				this.linkPagePrev.Enabled = false;
			}
			else
			{
				this.linkPagePrev.Enabled = true;
			}

			if (this.PageIndex >= this.PageCount)
			{
				linkPageNext.Enabled = false;
			}
			else
			{
				linkPageNext.Enabled = true;
			}


//			if (this.PageIndex == 0)
//			{
//				linkPagePrev.Enabled = false;
//			}
//			else if (this.PageIndex == 1)
//			{
//				linkPagePrev.Enabled = false;
//			}
//			else
//			{
//				linkPagePrev.Enabled = true;
//			}
			
			this.listPageIndex.Items.Clear(); 
			ListItem item = null;
			for(int i=0; i<this.PageCount; i ++)
			{
				item = new ListItem((i+1).ToString(),(i+1).ToString());
				this.listPageIndex.Items.Add( item );  
			}

			this.listPageIndex.SelectedValue = this.PageIndex.ToString();
		}

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.linkPagePrev.Click += new System.EventHandler(this.linkPagePrev_Click);
			this.linkPageNext.Click += new System.EventHandler(this.linkPageNext_Click);
			this.listPageIndex.SelectedIndexChanged += new System.EventHandler(this.listPageIndex_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void linkPagePrev_Click(object sender, System.EventArgs e)
		{
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex -= 1;
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.PagePrev));
			}
		}

		private void linkPageNext_Click(object sender, System.EventArgs e)
		{
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex += 1;
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.PageNext));
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
