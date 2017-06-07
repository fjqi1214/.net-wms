#region system
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.WebUI.UltraWebGrid;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// ItemMP 的摘要说明。
	/// </summary>
	public partial class FShelfMP : BasePage
	{
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		//private ItemFacade _itemFacade;// = FacadeFactory.CreateItemFacade();
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		
		private BenQGuru.eMES.MOModel.ShelfFacade _shelfFacade = null ;//= new FacadeFactory(base.DataProvider).CreateModelFacade();


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
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
			this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";
			// 
			// excelExporter
			// 
			this.excelExporter.FileExtension = "xls";
			this.excelExporter.LanguageComponent = this.languageComponent1;
			this.excelExporter.Page = this;
			this.excelExporter.RowSplit = "\r\n";
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

		}
		#endregion

		#region form events
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitOnPostBack();
		    if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				// 初始化界面UI
				this.InitUI();
				InitButtonHelp();
				SetEditObject(null);
				this.InitWebGrid();
		    }
		}
		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			RequestData();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Query );	
		}

		private void gridWebGrid_DblClick(object sender,Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			object obj = this.GetEditObject(e.Row);

			if ( obj != null )
			{
				this.SetEditObject( obj );

				this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
			}
		}

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
		    object shelf = this.GetEditObject();
			if(shelf != null)
			{
				if(_shelfFacade==null){_shelfFacade = new FacadeFactory(base.DataProvider).CreateShelfFacade();}
				this._shelfFacade.AddShelf( shelf as Shelf );
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			}
		}

		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{

				ArrayList shelfs = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					object shelf = this.GetEditObject(row);
					if( shelf != null )
					{
						shelfs.Add( (Shelf)shelf );
					}
				}

				if(_shelfFacade==null){_shelfFacade = new FacadeFactory(base.DataProvider).CreateShelfFacade();}
				int iUsed = 0;
				foreach(Shelf sh in shelfs)
				{
					iUsed += this._shelfFacade.ShelfIsUsed(sh.ShelfNO);
				}

				if(iUsed == 0)
				{
					this._shelfFacade.DeleteShelf( (Shelf[])shelfs.ToArray( typeof(Shelf) ) );

					this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
					this.RequestData();
					this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
				}
				else
				{
					WebInfoPublish.Publish(this,"$CS_SHELF_IS_USED",languageComponent1);
				}
				
			}
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_shelfFacade==null){_shelfFacade = new FacadeFactory(base.DataProvider).CreateShelfFacade();}
			object shelf = this.GetEditObject();

			if(shelf != null)
			{
				this._shelfFacade.UpdateShelfMemo(shelf as Shelf);
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
			}
		}

		protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
		{
			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );
		}

		private void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( this.chbSelectAll.Checked )
			{
				this.gridHelper.CheckAllRows( CheckStatus.Checked );
			}
			else
			{
				this.gridHelper.CheckAllRows( CheckStatus.Unchecked );
			}
		}

		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}

		
		#endregion

		#region private method
		private void RequestData()
		{
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}

		private void InitOnPostBack()
		{		
			this.buttonHelper = new ButtonHelper(this);
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
			this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

			// 2005-04-06
			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
		}

		public void InitButtonHelp()
		{	
			this.buttonHelper.AddDeleteConfirm();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
		}

		private void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Update )
			{
				this.txtShelfNOEdit.ReadOnly = true;
			}
			if ( pageAction == PageActionType.Save )
			{
				this.txtShelfNOEdit.ReadOnly = false;
			}
			if(pageAction ==PageActionType.Cancel)
			{
				this.txtShelfNOEdit.ReadOnly = false;
			}
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn("ShelfNO","车号",null);
			this.gridHelper.AddColumn("Memo","备注",null);
			this.gridHelper.AddColumn("MUSER","维护人员",null);
			this.gridHelper.AddColumn("MDATE","维护日期",null);		

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private  object GetEditObject()
		{
			if(	this.ValidateInput())
			{
				if(_shelfFacade==null){_shelfFacade = new FacadeFactory(base.DataProvider).CreateShelfFacade();}

				Shelf shelf = this._shelfFacade.CreateNewShelf();

				shelf.ShelfNO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShelfNOEdit.Text, 6));
				
				shelf.Status = ShelfStatus.BurnOut;

				shelf.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text,100);
				
				shelf.MaintainUser = this.GetUserCode();
				
				return shelf;
			}
			else
			{
				return null;
			}
		}

		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_shelfFacade==null){_shelfFacade = new FacadeFactory(base.DataProvider).CreateShelfFacade();}
			object obj = this._shelfFacade.GetShelf(row.Cells.FromKey("ShelfNO").Text.ToString());
			
			if (obj != null)
			{
				return (Shelf)obj;
			}

			return null;
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck( lblShelfNOEdit, txtShelfNOEdit, 6, true) );
			manager.Add( new LengthCheck( lblMemoEdit, txtMemoEdit, 100, false) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}

		private void SetEditObject(object obj)
		{
			if(_shelfFacade==null){_shelfFacade = new FacadeFactory(base.DataProvider).CreateShelfFacade();}
			if (obj == null)
			{
				this.txtShelfNOEdit.Text = String.Empty;
				this.txtMemoEdit.Text = String.Empty;
				return;
			}

			this.txtShelfNOEdit.Text = ( obj as Shelf ).ShelfNO;
			this.txtMemoEdit.Text = ( obj as Shelf ).Memo;
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			Shelf shelf = obj as Shelf;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								"false",
								shelf.ShelfNO ,
								shelf.Memo,
								shelf.MaintainUser,
								FormatHelper.ToDateString(shelf.MaintainDate),
								""});
		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_shelfFacade==null){_shelfFacade = new FacadeFactory(base.DataProvider).CreateShelfFacade();}
			return this._shelfFacade.QueryShelf(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtShelfNOQuery.Text)), inclusive, exclusive );
		}

		private int GetRowCount()
		{
			if(_shelfFacade==null){_shelfFacade = new FacadeFactory(base.DataProvider).CreateShelfFacade();}
			return this._shelfFacade.QueryShelfCount( FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtShelfNOQuery.Text)) );
		}

		private string[] FormatExportRecord( object obj )
		{
			Shelf shelf = obj as Shelf;
			return new string[]{
								   shelf.ShelfNO ,
								   shelf.Memo,
								   shelf.MaintainUser,
								   FormatHelper.ToDateString(shelf.MaintainDate)};
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"ShelfNO",
									"Memo",
									"MUSER",	
									"MDATE"
			                        };
		}
		#endregion

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if(e.Cell.Column.Key =="Edit")
			{
				object obj = this.GetEditObject(e.Cell.Row);

				if ( obj != null )
				{
					this.SetEditObject( obj );

					this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
				}
			}
		}

	}
}
