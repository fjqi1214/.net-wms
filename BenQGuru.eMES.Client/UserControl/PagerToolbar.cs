using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace UserControl
{
	/// <summary>
	/// PagerToolbar 的摘要说明。
	/// </summary>
	public class PagerToolbar : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label lblPageIndex;
		private System.Windows.Forms.Label lblOf;
		private System.Windows.Forms.Label lblPrevRowCount;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Label lblFirstPage;
		private System.Windows.Forms.Label lblPrevPage;
		private System.Windows.Forms.Label lblNextPage;
		private System.Windows.Forms.Label lblLastPage;
		private System.Windows.Forms.Label lblGo;
		private System.Windows.Forms.Label lblRowCount;
		private System.Windows.Forms.Label lblNextRowCount;
		private System.Windows.Forms.Label lblPageCount;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TextBox txtPageIndex;

		public System.EventHandler OnPagerToolBarClick;

		public PagerToolbar()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private int _pageIndex = 1;
		private int _pageSize = 20;
		private int _rowCount = 0;

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PagerToolbar));
			this.lblPageIndex = new System.Windows.Forms.Label();
			this.lblOf = new System.Windows.Forms.Label();
			this.lblPageCount = new System.Windows.Forms.Label();
			this.lblPrevRowCount = new System.Windows.Forms.Label();
			this.lblRowCount = new System.Windows.Forms.Label();
			this.lblNextRowCount = new System.Windows.Forms.Label();
			this.lblFirstPage = new System.Windows.Forms.Label();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.lblPrevPage = new System.Windows.Forms.Label();
			this.lblNextPage = new System.Windows.Forms.Label();
			this.lblLastPage = new System.Windows.Forms.Label();
			this.lblGo = new System.Windows.Forms.Label();
			this.txtPageIndex = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// lblPageIndex
			// 
			this.lblPageIndex.Location = new System.Drawing.Point(8, 0);
			this.lblPageIndex.Name = "lblPageIndex";
			this.lblPageIndex.Size = new System.Drawing.Size(48, 24);
			this.lblPageIndex.TabIndex = 0;
			this.lblPageIndex.Text = "1";
			this.lblPageIndex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblOf
			// 
			this.lblOf.Location = new System.Drawing.Point(56, 0);
			this.lblOf.Name = "lblOf";
			this.lblOf.Size = new System.Drawing.Size(24, 24);
			this.lblOf.TabIndex = 1;
			this.lblOf.Text = "/";
			this.lblOf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPageCount
			// 
			this.lblPageCount.Location = new System.Drawing.Point(80, 0);
			this.lblPageCount.Name = "lblPageCount";
			this.lblPageCount.Size = new System.Drawing.Size(48, 24);
			this.lblPageCount.TabIndex = 2;
			this.lblPageCount.Text = "1";
			this.lblPageCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPrevRowCount
			// 
			this.lblPrevRowCount.Location = new System.Drawing.Point(112, 0);
			this.lblPrevRowCount.Name = "lblPrevRowCount";
			this.lblPrevRowCount.Size = new System.Drawing.Size(16, 24);
			this.lblPrevRowCount.TabIndex = 3;
			this.lblPrevRowCount.Text = "共";
			this.lblPrevRowCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblRowCount
			// 
			this.lblRowCount.Location = new System.Drawing.Point(128, 0);
			this.lblRowCount.Name = "lblRowCount";
			this.lblRowCount.Size = new System.Drawing.Size(48, 24);
			this.lblRowCount.TabIndex = 4;
			this.lblRowCount.Text = " 0";
			this.lblRowCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblNextRowCount
			// 
			this.lblNextRowCount.Location = new System.Drawing.Point(176, 0);
			this.lblNextRowCount.Name = "lblNextRowCount";
			this.lblNextRowCount.Size = new System.Drawing.Size(16, 24);
			this.lblNextRowCount.TabIndex = 5;
			this.lblNextRowCount.Text = "笔";
			this.lblNextRowCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblFirstPage
			// 
			this.lblFirstPage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblFirstPage.ImageIndex = 0;
			this.lblFirstPage.ImageList = this.imageList;
			this.lblFirstPage.Location = new System.Drawing.Point(240, 0);
			this.lblFirstPage.Name = "lblFirstPage";
			this.lblFirstPage.Size = new System.Drawing.Size(16, 24);
			this.lblFirstPage.TabIndex = 6;
			this.lblFirstPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblFirstPage.Click += new System.EventHandler(this.lblFirstPage_Click);
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
			this.imageList.ImageSize = new System.Drawing.Size(10, 13);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.White;
			// 
			// lblPrevPage
			// 
			this.lblPrevPage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblPrevPage.ImageIndex = 1;
			this.lblPrevPage.ImageList = this.imageList;
			this.lblPrevPage.Location = new System.Drawing.Point(264, 0);
			this.lblPrevPage.Name = "lblPrevPage";
			this.lblPrevPage.Size = new System.Drawing.Size(16, 24);
			this.lblPrevPage.TabIndex = 7;
			this.lblPrevPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblPrevPage.Click += new System.EventHandler(this.lblPrevPage_Click);
			// 
			// lblNextPage
			// 
			this.lblNextPage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblNextPage.ImageIndex = 2;
			this.lblNextPage.ImageList = this.imageList;
			this.lblNextPage.Location = new System.Drawing.Point(296, 0);
			this.lblNextPage.Name = "lblNextPage";
			this.lblNextPage.Size = new System.Drawing.Size(16, 24);
			this.lblNextPage.TabIndex = 8;
			this.lblNextPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblNextPage.Click += new System.EventHandler(this.lblNextPage_Click);
			// 
			// lblLastPage
			// 
			this.lblLastPage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblLastPage.ImageIndex = 3;
			this.lblLastPage.ImageList = this.imageList;
			this.lblLastPage.Location = new System.Drawing.Point(320, 0);
			this.lblLastPage.Name = "lblLastPage";
			this.lblLastPage.Size = new System.Drawing.Size(16, 24);
			this.lblLastPage.TabIndex = 9;
			this.lblLastPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblLastPage.Click += new System.EventHandler(this.lblLastPage_Click);
			// 
			// lblGo
			// 
			this.lblGo.Location = new System.Drawing.Point(360, 0);
			this.lblGo.Name = "lblGo";
			this.lblGo.Size = new System.Drawing.Size(32, 24);
			this.lblGo.TabIndex = 1;
			this.lblGo.Text = "转到";
			this.lblGo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtPageIndex
			// 
			this.txtPageIndex.Location = new System.Drawing.Point(392, 2);
			this.txtPageIndex.Name = "txtPageIndex";
			this.txtPageIndex.Size = new System.Drawing.Size(48, 21);
			this.txtPageIndex.TabIndex = 10;
			this.txtPageIndex.Text = "";
			this.txtPageIndex.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPageIndex_KeyPress);
			// 
			// PagerToolbar
			// 
			this.Controls.Add(this.txtPageIndex);
			this.Controls.Add(this.lblGo);
			this.Controls.Add(this.lblLastPage);
			this.Controls.Add(this.lblNextPage);
			this.Controls.Add(this.lblPrevPage);
			this.Controls.Add(this.lblFirstPage);
			this.Controls.Add(this.lblNextRowCount);
			this.Controls.Add(this.lblPrevRowCount);
			this.Controls.Add(this.lblPageCount);
			this.Controls.Add(this.lblOf);
			this.Controls.Add(this.lblPageIndex);
			this.Controls.Add(this.lblRowCount);
			this.Name = "PagerToolbar";
			this.Size = new System.Drawing.Size(456, 24);
			this.ResumeLayout(false);

		}
		#endregion

		#region Property
		public int PageIndex
		{
			get
			{				
				return this._pageIndex;
			}
			set
			{
				this.EnableNext( true );
				this.EnablePrev( true );
				this._pageIndex = value;

				if( value >=  this.PageCount )
				{
					this._pageIndex = this.PageCount;
					this.EnableNext( false );
				}
				if( value <= 1 )
				{
					this._pageIndex = 1;
					this.EnablePrev( false );	
				}

				this.lblPageIndex.Text = this.PageIndex.ToString();	
				this.txtPageIndex.Text = this.PageIndex.ToString();
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
				return this._rowCount;
			}
			set
			{
				if ( value <= 0 )
				{
					this._rowCount = 0;
				}
				else
				{
					this._rowCount = value;
				}

				this.lblRowCount.Text  = RowCount.ToString();
				this.lblPageCount.Text = PageCount.ToString();

				this.InitPager();

				if ( this._rowCount == 0 )
				{
					this.txtPageIndex.Enabled = false;
				}
				else
				{
					this.txtPageIndex.Enabled = true;
				}
			}
		}

		public int PageSize
		{
			get
			{
				return this._pageSize;
			}
			set
			{
				if (value <= 0 )
				{
					this._pageSize = 1;
				}
				else
				{
					this._pageSize = value;
				}
			}
		}

		public int Inclusive
		{
			get
			{
				return 	(this.PageIndex - 1 ) * this.PageSize + 1;

			}
		}

		public int Exclusive
		{
			get
			{
				return Inclusive + this.PageSize - 1;
			}
		}
		#endregion

		#region Function
		public void InitPager()
		{	
			this.PageIndex = 1;
		}
		
		private void EnableNext(bool enable)
		{
			this.lblNextPage.Enabled	= enable;
			this.lblLastPage.Enabled	= enable;
		}

		private void EnablePrev(bool enable)
		{		
			this.lblPrevPage.Enabled	= enable;
			this.lblFirstPage.Enabled	= enable;
		}
		#endregion

		#region Event
		private void lblFirstPage_Click(object sender, System.EventArgs e)
		{		
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex = 1;
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.PageFirst));
			}	
		}

		private void lblPrevPage_Click(object sender, System.EventArgs e)
		{	
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex -= 1;
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.PagePrev));
			}
		}

		private void lblNextPage_Click(object sender, System.EventArgs e)
		{	
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex += 1;
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.PageNext));
			}
		}

		private void lblLastPage_Click(object sender, System.EventArgs e)
		{	
			if (this.OnPagerToolBarClick != null)
			{
				this.PageIndex = this.PageCount;
				OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.PageLast));
			}
		}	

		private void txtPageIndex_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( (int)(e.KeyChar) == 13 )
			{
				if (this.OnPagerToolBarClick != null)
				{
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

					this.PageIndex =  pageIndex;
					this.txtPageIndex.Text = this.PageIndex.ToString();

					OnPagerToolBarClick(this, new PagerToolBarEventArgs(ToolBarEnum.GotoPage));
				}
			}
		}
		#endregion
	}	

	public enum ToolBarEnum
	{
		PageFirst = 0,
		PageNext = 1,
		PagePrev = 2,
		PageLast = 3,
		GotoPage = 4
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
