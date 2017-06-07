using System;
using System.Collections;

using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.Helper
{
	public delegate object[] LoadDataSourceDelegate( int inclusive, int exclusive );
	public delegate int GetRowCountDelegate();
	public delegate Infragistics.WebUI.UltraWebGrid.UltraGridRow BuildGridRowDelegate( object obj );

	public class GridHelperForRPT
	{
		public GridHelper GridHelper;
		public GridHelperForRPT(UltraWebGrid grid)
		{
			GridHelper = new GridHelper(grid);
			ApplyDefaultStyle();
		}

		public GridHelperForRPT(UltraWebGrid grid, System.Web.UI.WebControls.CheckBox chbSelectAll)
		{
			GridHelper = new GridHelper(grid,chbSelectAll);
			ApplyDefaultStyle();
		}

		public void ApplyDefaultStyle()
		{
			this.GridHelper.Grid.DisplayLayout.CellClickActionDefault = CellClickAction.RowSelect;

			this.GridHelper.Grid.DisplayLayout.HeaderStyleDefault.Font.Bold = true;
			this.GridHelper.Grid.DisplayLayout.HeaderStyleDefault.Height = 20;
			this.GridHelper.Grid.DisplayLayout.HeaderStyleDefault.BackColor = System.Drawing.Color.FromName("#FFFFFF");
			this.GridHelper.Grid.DisplayLayout.HeaderStyleDefault.BackgroundImage = "../Skin/image/bg_col.gif";
			this.GridHelper.Grid.DisplayLayout.HeaderStyleDefault.ForeColor = System.Drawing.Color.White;
			this.GridHelper.Grid.DisplayLayout.HeaderStyleDefault.BorderColor = System.Drawing.Color.FromName("#FFFFFF");
			this.GridHelper.Grid.DisplayLayout.HeaderStyleDefault.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;

			this.GridHelper.Grid.DisplayLayout.HeaderStyleDefault.BorderDetails.ColorLeft = System.Drawing.Color.FromName("#34297B");
			this.GridHelper.Grid.DisplayLayout.HeaderStyleDefault.BorderDetails.ColorRight = System.Drawing.Color.FromName("#34297B");
			this.GridHelper.Grid.DisplayLayout.HeaderStyleDefault.BorderDetails.ColorTop = System.Drawing.Color.FromName("#34297B");

			this.GridHelper.Grid.DisplayLayout.RowStyleDefault.BorderDetails.WidthLeft = 1;
			this.GridHelper.Grid.DisplayLayout.RowStyleDefault.BorderDetails.ColorRight = System.Drawing.Color.FromName("#34297B");
			this.GridHelper.Grid.DisplayLayout.RowStyleDefault.BorderDetails.ColorTop = System.Drawing.Color.FromName("#34297B");
			this.GridHelper.Grid.DisplayLayout.RowStyleDefault.BorderDetails.ColorBottom = System.Drawing.Color.FromName("#34297B");

			this.GridHelper.Grid.DisplayLayout.RowAlternateStyleDefault.BackColor = System.Drawing.Color.FromName("#c0cbdf");
			this.GridHelper.Grid.DisplayLayout.RowStyleDefault.BackColor = System.Drawing.Color.White;

			this.GridHelper.Grid.DisplayLayout.AllowUpdateDefault = AllowUpdate.Yes ;
		}
	}

	/// <summary>
	/// GridHelper 的摘要说明。
	/// </summary>	
	public class GridHelper
	{
		public LoadDataSourceDelegate LoadDataSourceHandle  = null;
		public BuildGridRowDelegate BuildGridRowhandle		= null;
		public GetRowCountDelegate GetRowCountHandle		= null;

		public string CheckColumnKey = "Check";
		public string CheckColumnText = "*";
		public string EditColumnKey = "Edit";
		public string EditColumnText = "编辑";

		public Infragistics.WebUI.UltraWebGrid.UltraWebGrid _grid = null;
		public System.Web.UI.WebControls.CheckBox _chbSelectAll = null;
		public PagerToolBar _pagerToolBar = null;
		public PagerSizeSelector _pagerSizeSelector = null;

		public static string _warningNoRow = "请选择至少一条记录！";

		public Infragistics.WebUI.UltraWebGrid.UltraWebGrid Grid
		{
			get
			{
				return this._grid;
			}
			set
			{
				this._grid = value;
			}
		}

		public System.Web.UI.WebControls.CheckBox CheckAllBox
		{
			get
			{
				return this._chbSelectAll;
			}
			set
			{
				this._chbSelectAll = value;
			}
		}

		public PagerToolBar PagerToolBar
		{
			get
			{
				return this._pagerToolBar;
			}
			set
			{
				this._pagerToolBar = value;
			}
		}

		public PagerSizeSelector PagerSizeSelector
		{
			get
			{
				return this._pagerSizeSelector;
			}
			set
			{
				this._pagerSizeSelector = value;
			}
		}

		public GridHelper(UltraWebGrid grid)
		{
			this._grid = grid;

			this.InitGridHelper();
		}

		public GridHelper(UltraWebGrid grid, System.Web.UI.WebControls.CheckBox chbSelectAll)
		{
			this._grid = grid;
			this._chbSelectAll = chbSelectAll;
	
			this.InitGridHelper();
		}

		public void InitGridHelper()
		{
			this.FindControls();

			if ( this.PagerToolBar!= null )
			{
				this.PagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
			}

			if ( this.CheckAllBox != null )
			{
				this.CheckAllBox.CheckedChanged += new EventHandler(CheckAllBox_CheckedChanged);
			}

			// Added by Icyer 2005/09/07
			if (this._pagerSizeSelector != null)
			{
				this._pagerSizeSelector.OnPagerSizeChanged += new PagerSizeSelector.PagerSizeChangedHandle(this.pagerSizeSelector_OnPageSizeChanged);
			}
			// Added end

			this.ApplyDefaultStyle();
		}
		
		// Added by Icyer 2005/09/07
		public void pagerSizeSelector_OnPageSizeChanged(object sender, int pageSize)
		{
			this.PagerToolBar.PageSize = pageSize;
			this.PagerToolBar.RowCount = this.PagerToolBar.RowCount;
			int iPageCount = 0;
			if (this.PagerToolBar.RowCount % pageSize == 0)
				iPageCount = this.PagerToolBar.RowCount / pageSize;
			else
				iPageCount = this.PagerToolBar.RowCount / pageSize + 1;
			if (this.PagerToolBar.PageIndex >= iPageCount)
			{
				this.PagerToolBar.PageIndex = 0;
			}
			RequestData();
		}
		// Added end
		
		public void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.RefreshData();
		}

		public void FindControls()
		{

			System.Web.UI.Control ctrl = this.Grid.Page.FindControl("chbSelectAll");

			if ( ctrl is System.Web.UI.WebControls.CheckBox )
			{
				this._chbSelectAll = (System.Web.UI.WebControls.CheckBox)ctrl;
			}

			ctrl = this.Grid.Page.FindControl("pagerToolBar");

			if ( ctrl is PagerToolBar )
			{
				this._pagerToolBar = (PagerToolBar)ctrl;
			}

			ctrl = this.Grid.Page.FindControl("pagerSizeSelector");

			if ( ctrl is PagerSizeSelector )
			{
				this._pagerSizeSelector = (PagerSizeSelector)ctrl;
			}
		}

        public void AddDefaultColumn(bool hasCheckColumn, bool hasEditColumn)
        {
            if (hasCheckColumn)
            {
                this.AddCheckBoxColumn(CheckColumnKey, CheckColumnText, true, null);
                //				this.Grid.Bands[0].Columns.FromKey(CheckColumnKey).HeaderText="<input type=checkbox onclick=document.Form1.chbSelectAll.checked=checked> language=javascript />";
                //				this.Grid.Bands[0].Columns.FromKey(CheckColumnKey).HeaderText="<input type=checkbox onclick=DoSelectAll('" + this.Grid.ID + "') language=javascript />";
                this.Grid.Bands[0].Columns.FromKey(CheckColumnKey).Move(0);
                this.Grid.Bands[0].Columns.FromKey(CheckColumnKey).Width = new System.Web.UI.WebControls.Unit(30);
                this.Grid.Bands[0].Columns.FromKey(CheckColumnKey).HeaderClickAction = HeaderClickAction.Select;
            }

            if (hasEditColumn)
            {
                this.AddLinkColumn(EditColumnKey, EditColumnText, null);
                this.Grid.Bands[0].Columns.FromKey(EditColumnKey).Move(this.Grid.DisplayLayout.Grid.Bands[0].Columns.Count - 1);
                //this.Grid.Bands[0].Columns.FromKey(EditColumnKey).CellButtonStyle.BackgroundImage	= ((BasePage)this.Grid.Page).VirtualHostRoot + "skin/image/edit16.gif";
                this.Grid.Bands[0].Columns.FromKey(EditColumnKey).CellStyle.BackgroundImage = ((BasePage)this.Grid.Page).VirtualHostRoot + "skin/image/edit16.gif";
            }
        }
		public void AddCheckBoxColumn(string key, string text, bool allowUpdate, ColumnStyle style)
		{
			this.Grid.Bands[0].Columns.Add(key);
			this.Grid.Bands[0].Columns.FromKey(key).HeaderText					= text;
			this.Grid.Bands[0].Columns.FromKey(key).AllowUpdate					= allowUpdate ? Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes : Infragistics.WebUI.UltraWebGrid.AllowUpdate.No;
			this.Grid.Bands[0].Columns.FromKey(key).Type						= Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
			this.Grid.Bands[0].Columns.FromKey(key).CellStyle.HorizontalAlign	= System.Web.UI.WebControls.HorizontalAlign.Center;
		}

        public void AddLinkColumn(string key, string text, ColumnStyle style)
        {
            this.Grid.Bands[0].Columns.Add(key);
            this.Grid.Bands[0].Columns.FromKey(key).HeaderText = text;
            this.Grid.Bands[0].Columns.FromKey(key).Width = new System.Web.UI.WebControls.Unit(50);
            this.Grid.Bands[0].Columns.FromKey(key).Type = Infragistics.WebUI.UltraWebGrid.ColumnType.Button;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.BackColor = System.Drawing.Color.Transparent;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
            //this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.CustomRules		="BACKGROUND-POSITION: center center;Background-repeat:no-repeat";
            //this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.BackgroundImage	= ((BasePage)this.Grid.Page).VirtualHostRoot + "skin/image/detail16.gif";
            this.Grid.Bands[0].Columns.FromKey(key).CellStyle.CustomRules = "BACKGROUND-POSITION: center center;Background-repeat:no-repeat";
            this.Grid.Bands[0].Columns.FromKey(key).CellStyle.BackgroundImage = ((BasePage)this.Grid.Page).VirtualHostRoot + "skin/image/detail16.gif";
        }

        public void AddLinkFileColumn(string key, string text, ColumnStyle style)
        {
            this.Grid.Bands[0].Columns.Add(key);
            this.Grid.Bands[0].Columns.FromKey(key).HeaderText = text;
            this.Grid.Bands[0].Columns.FromKey(key).Width = new System.Web.UI.WebControls.Unit(50);
            this.Grid.Bands[0].Columns.FromKey(key).Type = Infragistics.WebUI.UltraWebGrid.ColumnType.Button;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.BackColor = System.Drawing.Color.Transparent;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.None;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonDisplay = CellButtonDisplay.Always;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.Font.Underline = true;
            this.Grid.Bands[0].Columns.FromKey(key).CellStyle.CustomRules = "BACKGROUND-POSITION: center center;Background-repeat:no-repeat";
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.ForeColor = System.Drawing.Color.Blue;
            this.Grid.Bands[0].Columns.FromKey(key).CellStyle.Padding.Bottom = 0;
            this.Grid.Bands[0].Columns.FromKey(key).CellStyle.Padding.Left = 0;
            this.Grid.Bands[0].Columns.FromKey(key).CellStyle.Padding.Right = 0;
            this.Grid.Bands[0].Columns.FromKey(key).CellStyle.Padding.Top = 0;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.Margin.Top = 0;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.Margin.Bottom = 0;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.Margin.Left = 0;
            this.Grid.Bands[0].Columns.FromKey(key).CellButtonStyle.Margin.Right = 0;
            
        }


		public void ApplyLanguage(ControlLibrary.Web.Language.LanguageComponent languageComponent)
		{
			if ( languageComponent != null )
			{
				BenQGuru.eMES.Common.MutiLanguage.LanguageWord word = null;

				foreach ( UltraGridColumn column in this.Grid.Columns )
				{
					word = languageComponent.GetLanguage( column.Key );

					if ( column.Key.ToString() == "Edit")
					{
						if ( languageComponent.Language.ToString() == "CHS" )
						{
							column.HeaderText = "编辑";
						}
						else if( languageComponent.Language.ToString() == "CHT" )
						{
							column.HeaderText = "編輯";
						}
						else
						{
							column.HeaderText = "Edit";
						}
					}
					
					if ( word == null )
					{
						continue;
					}

					if ( word.ControlText.Trim() == string.Empty )
					{
//						continue;
						column.HeaderText = column.Key.ToString();
					}
					else
					{
						column.HeaderText = word.ControlText;
					}
				}
	
				word = languageComponent.GetLanguage("$warningNoRow");

				if ( word != null )
				{
                    GridHelper._warningNoRow = word.ControlText; ;
				}
			}
		}

		public void AddColumn(string key, string headerText, ColumnStyle style)
		{
			this.Grid.Bands[0].Columns.Add(key);
			this.Grid.Bands[0].Columns.FromKey(key).HeaderText = headerText;		
		}

		public void AddColumn(string key, string headerText)
		{
			this.Grid.Bands[0].Columns.Add(key);
			this.Grid.Bands[0].Columns.FromKey(key).HeaderText = headerText;		
		}

		public void AddColumn(string key, string headerText,ColumnStyle style,int width )
		{
			this.Grid.Bands[0].Columns.Add(key);
			this.Grid.Bands[0].Columns.FromKey(key).HeaderText = headerText;
			this.Grid.Bands[0].Columns.FromKey(key).Width = new System.Web.UI.WebControls.Unit(width);
		}

		public virtual void ApplyDefaultStyle()
		{
			this.Grid.DisplayLayout.CellClickActionDefault				 = CellClickAction.RowSelect;
			this.Grid.DisplayLayout.RowAlternateStyleDefault.BorderColor = System.Drawing.Color.FromArgb(0x78d7d8d9);
			this.Grid.DisplayLayout.RowAlternateStyleDefault.BackColor	 = System.Drawing.Color.FromArgb(0x78ebebeb);

			/* 允许cell edit */
			//this.Grid.DisplayLayout.CellClickActionDefault = CellClickAction. ;
			this.Grid.DisplayLayout.AllowUpdateDefault = AllowUpdate.Yes ;
			//this.Grid.Attributes.Add("AfterCellUpdateHandler","");
			//this.Grid.DisplayLayout.ClientSideEvents.AfterCellUpdateHandler = "";
		}

		public void CheckAllRows(CheckStatus status)
		{
			if ( this.Grid.Columns.FromKey(CheckColumnKey).AllowUpdate == AllowUpdate.Yes )
			{
				if ( status == CheckStatus.Checked )
				{
					foreach ( UltraGridRow row in this.Grid.Rows )
					{
						if ( row.Cells.FromKey(CheckColumnKey).AllowEditing != AllowEditing.No )
						{
							row.Cells.FromKey(CheckColumnKey).Text = "true";
						}
					}
				}

				if ( status == CheckStatus.Unchecked )
				{
					foreach ( UltraGridRow row in this.Grid.Rows )
					{
						if ( row.Cells.FromKey(CheckColumnKey).AllowEditing != AllowEditing.No )
						{
							row.Cells.FromKey(CheckColumnKey).Text = "false";
						}
					}
				}
			}
		}

		public ArrayList GetCheckedRows()
		{
			ArrayList array = new ArrayList();

			foreach(Infragistics.WebUI.UltraWebGrid.UltraGridRow row in this.Grid.Rows)
			{
				if (row.Cells.FromKey(CheckColumnKey).ToString() == "true")
				{
					array.Add( row );
				}
			}

            if (array.Count == 0)
            {
                this.Grid.Page.Response.Write("<script language=javascript>alert('" + GridHelper._warningNoRow + "');</script>");

            }

			return array;
		}

		public void GridBind(int pageIndex, int pageSize)
		{
			this.Grid.Rows.Clear();

			if ( this.CheckAllBox != null )
			{
				this.CheckAllBox.Checked = false;
			}

			if ( LoadDataSourceHandle == null || BuildGridRowhandle == null )
			{
				return;
			}

			if (pageIndex == PageGridBunding.None)
			{
				return;
			}

			int inclusive = ( pageIndex - 1 ) * pageSize + 1;
			int exclusive = inclusive + pageSize - 1;

			object[] objs = this.LoadDataSourceHandle(inclusive, exclusive);

			if ( objs == null)
			{
//				this.Grid.Page.Response.Write( @"<script language=javascript>alert('没有查询到符合条件的记录！')</script>");
				return;
			}

			foreach ( object obj in objs )
			{
				this.Grid.Rows.Add( this.BuildGridRowhandle(obj) );
			}
		}

		public bool IsClickEditColumn(Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			return IsClickEditColumn(e.Cell);
		}

		public bool IsClickEditColumn(Infragistics.WebUI.UltraWebGrid.UltraGridCell cell)
		{
			if ( cell.Column.Key == EditColumnKey )
			{
				return true;
			}

			return false;
		}

		public bool IsClickColumn(string columnKey, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			return IsClickColumn(columnKey, e.Cell);
		}

		public bool IsClickColumn(string columnKey, Infragistics.WebUI.UltraWebGrid.UltraGridCell cell)
		{
			if ( cell.Column.Key == columnKey )
			{
				return true;
			}

			return false;
		}

		public void CheckAllBox_CheckedChanged(object sender, EventArgs e)
		{			
			if ( this.CheckAllBox.Checked )
			{
				this.CheckAllRows( CheckStatus.Checked );
			}
			else
			{
				this.CheckAllRows( CheckStatus.Unchecked );
			}
		}
		
		public void RequestData()
		{		
			if ( this.PagerToolBar != null )
			{
				if ( this.PagerSizeSelector != null )
				{
					this.PagerToolBar.PageSize = this.PagerSizeSelector.PageSize;
				}
			
				this.PagerToolBar.InitPager();
				
			}

			this.RefreshData();
		}

		public void RefreshData()
		{	
			if ( this.PagerToolBar != null )
			{
				if ( this.GetRowCountHandle != null )
				{
					this.PagerToolBar.RowCount = this.GetRowCountHandle();
				}

				this.GridBind(this.PagerToolBar.PageIndex, this.PagerToolBar.PageSize);
			}
			else
			{
				this.GridBind(1, int.MaxValue);
			}
		}
	}

	public class ColumnStyle
	{
		public ColumnStyle()
		{
		}
	}
	
	public enum CheckStatus 
	{
		Checked, Unchecked 
	}
	
	public class PageGridBunding
	{
		public static int None = 0;
		public static int Page = 1;
	}
}
