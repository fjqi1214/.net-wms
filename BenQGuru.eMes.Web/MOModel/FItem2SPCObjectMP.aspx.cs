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
using BenQGuru.eMES.Domain.SPC;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FModelMP 的摘要说明。
	/// </summary>
	public partial class FItem2SPCDataStoreMP : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;


		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		private SPCFacade _spcFacade ;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		protected BenQGuru.eMES.Web.UserControl.eMESDate txtDateFromEdit;
		protected BenQGuru.eMES.Web.UserControl.eMESDate txtDateToEdit;

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
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
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
			InitHander();
			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				// 初始化界面UI
				this.InitUI();
				this.InitButton();
				this.InitWebGrid();
				this.InitPanel();
			}
		}

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			this.RequestData();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Query );	
		}

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
			if(_spcFacade==null){_spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade();}
			object obj = this.GetEditObject();
			if(obj != null)
			{
				this._spcFacade.AddSPCDataStore( (SPCDataStore)obj );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			}
		}

		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			if(_spcFacade==null){_spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{
				ArrayList objs = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					object obj = this.GetEditObject(row);
					if( obj != null )
					{
						objs.Add( (SPCDataStore)obj );
					}
				}

				this._spcFacade.DeleteSPCDataStore( (SPCDataStore[])objs.ToArray( typeof(SPCDataStore) ) );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
			}
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_spcFacade==null){_spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade();}
			object obj = this.GetEditObject();
			if(obj != null)
			{
				this._spcFacade.UpdateSPCDataStore((SPCDataStore)obj );
				this.RequestData();

				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
			}
		}

		protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
		{
			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );
		}

		protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
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

		private void gridWebGrid_ClickCellButton(object sender, CellEventArgs e)
		{
			object obj = this.GetEditObject(e.Cell.Row);

			if ( obj != null )
			{
				this.SetEditObject( obj );

				this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
			}
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

		private int GetRowCount()
		{
			if(_spcFacade==null){_spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade();}
			return this._spcFacade.QuerySPCDataStoreCount( 
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
				FormatHelper.CleanString(this.drpObjectCodeQuery.SelectedValue));
		}


		private void InitHander()
		{
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.buttonHelper = new ButtonHelper(this);
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
			this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
		}



		private void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if(pageAction == PageActionType.Save)
			{
				//this.txtObjectCodeEdit.ReadOnly = false;
			}
			if(pageAction == PageActionType.Update)
			{
				//this.txtObjectCodeEdit.ReadOnly = true;
			}
			if ( pageAction == PageActionType.Cancel )
			{
				//this.txtObjectCodeEdit.ReadOnly = false;
			}
			
		}


		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}

		public void InitButton()
		{	
			this.buttonHelper.AddDeleteConfirm();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "ID",			"ID",	null);
			this.gridHelper.AddColumn( "ItemCode",		"产品代码",	null);
			this.gridHelper.AddColumn( "ObjectCode",	"管控项目",	null);
			this.gridHelper.AddColumn( "SPCTableName",	"数据库表名",	null);
			this.gridHelper.AddColumn( "DateFrom",		"开始日期",	null);
			this.gridHelper.AddColumn( "DateTo",		"结束日期",	null);

			this.gridHelper.AddColumn( "MUSER", "维护用户",	null);
			this.gridHelper.AddColumn( "MDATE", "维护日期",	null);
			this.gridHelper.AddColumn( "MTIME", "维护时间",	null);

			this.gridHelper.Grid.Columns.FromKey("ID").Hidden = true;

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private void InitPanel()
		{
			this.txtDateFromEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now ));
			this.txtDateToEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now ));

			this.drpObjectCodeQuery.Items.Clear();
			this.drpObjectCodeEdit.Items.Clear();

			if(_spcFacade==null){_spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade();}
			object[] objs=_spcFacade.GetAllSPCObject();
			
			if( objs!=null && objs.Length>0 )
			{
				this.drpObjectCodeQuery.Items.Add( new ListItem( "", "" ) );
				this.drpObjectCodeEdit.Items.Add( new ListItem( "", "" ) );

				for( int i=0; i<objs.Length; i++ )
				{
					this.drpObjectCodeQuery.Items.Add( new ListItem( (objs[i] as SPCObject).ObjectCode, (objs[i] as SPCObject).ObjectCode ) );
					this.drpObjectCodeEdit.Items.Add( new ListItem( (objs[i] as SPCObject).ObjectCode, (objs[i] as SPCObject).ObjectCode ) );
				}
			}
		}

		private  object GetEditObject()
		{
			if(this.ValidateInput())
			{
				if(_spcFacade==null){_spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade();}
				SPCDataStore obj = this._spcFacade.CreateNewSPCDataStore();

				if(this.txtIDEdit.Value==string.Empty)
				{
					obj.ID = Guid.NewGuid().ToString();
				}
				else
				{
					obj.ID = this.txtIDEdit.Value ;
				}
				obj.ItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtItemCodeEdit.Text ) );
				obj.ObjectCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.drpObjectCodeEdit.SelectedValue ) );
				obj.TableName = FormatHelper.CleanString( this.txtTableNameEdit.Text ) ;
				obj.DateFrom = FormatHelper.TODateInt( this.txtDateFromEdit.Text );
				obj.DateTo = FormatHelper.TODateInt( this.txtDateToEdit.Text );

				obj.MaintainUser = this.GetUserCode();

				return obj;
			}
			else
			{
				return null;
			}
		}

		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_spcFacade==null){_spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade();}
			object obj = this._spcFacade.GetSPCDataStore(row.Cells.FromKey("ID").Text.ToString());
			
			if (obj != null)
			{
				return (SPCDataStore)obj;
			}

			return null;
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck( this.lblItemCodeEdit, this.txtItemCodeEdit, 40, false) );
			//manager.Add( new LengthCheck( this.lblObjectCodeEdit, this.drpObjectCodeEdit, 40, false) );
			manager.Add( new LengthCheck( this.lblTableNameEdit, this.txtTableNameEdit, 40, true) );
			manager.Add( new DateRangeCheck( this.lblDateFromEdit, this.txtDateFromEdit.Text, this.lblDateToEdit, this.txtDateToEdit.Text, true));
			
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}

		private void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtIDEdit.Value = string.Empty;
				this.txtItemCodeEdit.Text=string.Empty;
				this.drpObjectCodeEdit.SelectedIndex = -1;
				this.txtTableNameEdit.Text = string.Empty ;
				this.txtDateFromEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now ));
				this.txtDateToEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now ));
				return;
			}

			SPCDataStore spcObj = obj as SPCDataStore ;

			this.txtIDEdit.Value = spcObj.ID.ToString();
			this.txtItemCodeEdit.Text = spcObj.ItemCode.ToString();
			this.drpObjectCodeEdit.SelectedValue = spcObj.ObjectCode.ToString();
			this.txtTableNameEdit.Text = spcObj.TableName.ToString() ;
			this.txtDateFromEdit.Text = FormatHelper.ToDateString( spcObj.DateFrom);
			this.txtDateToEdit.Text = FormatHelper.ToDateString( spcObj.DateTo );
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			SPCDataStore spcObj = obj as SPCDataStore ;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
				new object[]{
								false,
								spcObj.ID.ToString(),
								spcObj.ItemCode.ToString(),
								spcObj.ObjectCode.ToString(),
								spcObj.TableName.ToString(),
								FormatHelper.ToDateString( spcObj.DateFrom),
								FormatHelper.ToDateString( spcObj.DateTo ),
								spcObj.MaintainUser.ToString(),
								FormatHelper.ToDateString(spcObj.MaintainDate),
								FormatHelper.ToTimeString(spcObj.MaintainTime),
								""
							});
		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_spcFacade==null){_spcFacade = new FacadeFactory(base.DataProvider).CreateSPCFacade();}
			return this._spcFacade.QuerySPCDataStore(
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
				FormatHelper.CleanString(this.drpObjectCodeQuery.SelectedValue),
				inclusive, exclusive );
		}

		private string[] FormatExportRecord( object obj )
		{
			SPCDataStore spcObj = obj as SPCDataStore ;
			return new string[]{
								   spcObj.ItemCode.ToString(),
								   spcObj.ObjectCode.ToString(),
								   spcObj.TableName.ToString(),
								   FormatHelper.ToDateString( spcObj.DateFrom),
								   FormatHelper.ToDateString( spcObj.DateTo ),
								   spcObj.MaintainUser.ToString(),
								   FormatHelper.ToDateString(spcObj.MaintainDate),
								   FormatHelper.ToTimeString(spcObj.MaintainTime)	
							   };
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	
									"ItemCode",
									"ObjectCode",
									"SPCTableName",
									"DateFrom",
									"DateTo",
									"MUSER",
									"MDATE",
									"MTIME"
								};
		}

		#endregion

		
	}
}
