#region System
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
#endregion

#region project
using Infragistics.WebUI.UltraWebGrid;

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
	/// FItemLocationMP 的摘要说明。
	/// </summary>
	public partial class FItem2CartonCFG : BaseMPage
	{
		protected System.Web.UI.WebControls.Label lblABEdit;
		protected System.Web.UI.WebControls.Label lblQtyEdit;
		protected System.Web.UI.WebControls.TextBox txtQtyEdit;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		//protected System.Web.UI.WebControls.Label lblTitle;

		protected System.Web.UI.WebControls.Label lblSegment;

//		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
//		private GridHelper gridHelper = null;
//		private ButtonHelper buttonHelper = null;

		private ItemFacade itemFacade ;//= new  FacadeFactory(base.DataProvider).CreateItemFacade();

	
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
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion

		#region form events
		protected void Page_Load(object sender, System.EventArgs e)
		{
//			InitOnPostBack();
			if (!IsPostBack)
			{
//				// 初始化页面语言
//				this.InitPageLanguage(this.languageComponent1, false);
//				// 初始化界面UI
//				this.InitUI();
				InitDropdownList();
//
			}

		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Update )
			{
				this.txtItemName.ReadOnly = true;
			}
			if ( pageAction == PageActionType.Save )
			{
				this.txtItemName.ReadOnly = false;
			}
			if(pageAction ==PageActionType.Cancel)
			{
				this.txtItemName.ReadOnly = false;
			}
		}

//		private void cmdQuery_ServerClick(object sender, System.EventArgs e)
//		{
//			RequestData();
//			this.buttonHelper.PageActionStatusHandle( PageActionType.Query );	
//		}

//		private void gridWebGrid_DblClick(object sender,Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
//		{
//			object obj = this.GetEditObject(e.Row);
//
//			if ( obj != null )
//			{
//				this.SetEditObject( obj );
//
//				this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
//			}
//		}

//		private void cmdAdd_ServerClick(object sender, System.EventArgs e)
//		{
//			PageCheckManager checkManager = new PageCheckManager();
////			checkManager.Add(new LengthCheck(lblItem2CartonCFGStatusEdit,drpItem2CartonCFGStatusEdit,Int32.MaxValue,true));
//
//			if( !checkManager.Check() )
//			{
//				WebInfoPublish.Publish(this,checkManager.CheckMessage,this.languageComponent1);
//				return;
//			}
//
//			object order = this.GetEditObject();
//			if(order != null)
//			{
//				if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
//				this.itemFacade.AddItem2CartonCFG(order as Item2CartonCFG);
//				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
//				this.RequestData();
//				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
//			}
//		}

//		private void cmdDelete_ServerClick(object sender, System.EventArgs e)
//		{
//			if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
//			ArrayList array = this.gridHelper.GetCheckedRows();
//			if( array.Count > 0 )
//			{
//
//				ArrayList orders = new ArrayList( array.Count );
//			
//				foreach (UltraGridRow row in array)
//				{
//					object obj = this.GetEditObject(row);
//					if( obj != null )
//					{
////						if( (order as Item2CartonCFG).Item2CartonCFGStatus == Item2CartonCFGStatus.Completed)
////						{
////							ExceptionManager.Raise(this.GetType().BaseType,"$Error_Item2CartonCFG_Completed_CANNOT_Delete");
////							return;
////						}
//						orders.Add( (Item2CartonCFG)obj );
//					}
//				}
//				
//				this.itemFacade.DeleteItem2CartonCFG( (Item2CartonCFG[])orders.ToArray( typeof(Item2CartonCFG) ) );
//				
//				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
//				this.RequestData();
//				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
//			}
//		}

//		private void cmdSave_ServerClick(object sender, System.EventArgs e)
//		{
//			PageCheckManager checkManager = new PageCheckManager();
////			checkManager.Add(new LengthCheck(lblItem2CartonCFGStatusEdit,drpItem2CartonCFGStatusEdit,Int32.MaxValue,true));
//
//			if( !checkManager.Check() )
//			{
//				WebInfoPublish.Publish(this,checkManager.CheckMessage,this.languageComponent1);
//				return;
//			}
//
//			if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
//			object order = this.GetEditObject();
//
//			if(order != null)
//			{
////				if( (order as Item2CartonCFG).Item2CartonCFGStatus == Item2CartonCFGStatus.Completed)
////				{
////					ExceptionManager.Raise(this.GetType().BaseType,"$Error_Item2CartonCFG_Completed_CANNOT_Update");
////					return;
////				}
//
//				this.itemFacade.UpdateItem2CartonCFG( order as Item2CartonCFG );;
//				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
//				this.RequestData();
//				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
//			}
//		}

//		private void cmdCancel_ServerClick(object sender, System.EventArgs e)
//		{
//			
//			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );
//		}

//		private void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
//		{
//			if ( this.chbSelectAll.Checked )
//			{
//				this.gridHelper.CheckAllRows( CheckStatus.Checked );
//			}
//			else
//			{
//				this.gridHelper.CheckAllRows( CheckStatus.Unchecked );
//			}
//		}

//		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
//		{
//			if(e.Cell.Column.Key =="Edit")
//			{
//				object obj = this.GetEditObject(e.Cell.Row);
//
//				if ( obj != null )
//				{
//					this.SetEditObject( obj );
//
//					this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
//				}
//			}
//		}

//		private void cmdGridExport_ServerClick(object sender, System.EventArgs e)
//		{
//			this.excelExporter.Export();
//		}

		
		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{	
			if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			this.itemFacade.AddItem2CartonCFG( (Item2CartonCFG)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			this.itemFacade.DeleteItem2CartonCFG( (Item2CartonCFG[])domainObjects.ToArray( typeof(Item2CartonCFG) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			this.itemFacade.UpdateItem2CartonCFG( (Item2CartonCFG)domainObject );
		}

		#endregion

		#region private method
//		private void RequestData()
//		{
//			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
//			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
//			this.pagerToolBar.RowCount = GetRowCount();
//			this.pagerToolBar.InitPager();
//		}
//
//		private void InitOnPostBack()
//		{		
//			this.buttonHelper = new ButtonHelper(this);
//			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
//			this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);
//
//			this.gridHelper = new GridHelper(this.gridWebGrid);
//			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
//			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);
//
//			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
//
//			// 2005-04-06
//			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
//			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
//			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
//		}

		protected override void InitWebGrid()
		{
			this.gridHelper.AddColumn( "ItemName",       "产品名称",	null);
			this.gridHelper.AddColumn( "PCSTYPE",  "板片类型",		null);
			this.gridHelper.AddColumn( "MPlate",   "M板",	null);
			this.gridHelper.AddColumn( "SPlate",   "S板",	null);
			this.gridHelper.AddColumn( "CartonItemCode",  "大箱料号",		null);
			this.gridHelper.AddColumn( "CartonItemCodeLen","大箱标签长度",		null);
			this.gridHelper.AddColumn( "StartPosition",   "起始位置",null);
			this.gridHelper.AddColumn( "EndPosition",   "结束位置",null);
			
			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private  void InitDropdownList()
		{
			this.drpPCSType.Items.Clear();
			//this.drpPCSType.Items.Add( new ListItem( "", "" ));
			this.drpPCSType.Items.Add( new ListItem( languageComponent1.GetString( PCSType.SingleSide.ToString()) , Convert.ToString((int)PCSType.SingleSide) ));
			this.drpPCSType.Items.Add( new ListItem( languageComponent1.GetString( PCSType.DoubleSide.ToString()) ,Convert.ToString((int) PCSType.DoubleSide) ));

//			this.drpItem2CartonCFGStatusQuery.Items.Clear();
//			this.drpItem2CartonCFGStatusQuery.Items.Add( new ListItem( "", "" ));
//			this.drpItem2CartonCFGStatusQuery.Items.Add( new ListItem( languageComponent1.GetString( Item2CartonCFGStatus.InProcess) , Item2CartonCFGStatus.InProcess) );
//			this.drpItem2CartonCFGStatusQuery.Items.Add( new ListItem( languageComponent1.GetString( Item2CartonCFGStatus.Completed), Item2CartonCFGStatus.Completed ) );
		}

		protected override  object GetEditObject()
		{
			if(	this.ValidateInput())
			{
				if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}

				Item2CartonCFG entity = this.itemFacade.CreateNewItem2CartonCFG();

				entity.ItemName = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemName.Text, 40));
				entity.PCSTYPE = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpPCSType.SelectedIndex.ToString(), 40));
				entity.CartonItemNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonItemCode.Text, 40));
				entity.MPlate = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMPlate.Text, 40));
				entity.SPlate = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSPlate.Text, 40));
				entity.CartonLabelLen = Convert.ToInt32(this.txtCartonCodeLen.Text.Trim());
				entity.StartPosition =  Convert.ToInt32(this.txtStartPosition.Text.Trim());
				entity.EndPosition =  Convert.ToInt32(this.txtEndPosition.Text.Trim());
				entity.MaintainUser = this.GetUserCode();
				
				return entity;
			}
			else
			{
				return null;
			}
		}

		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}

			object obj = this.itemFacade.GetItem2CartonCFG(row.Cells.FromKey("ItemName").Text.ToString());
			
			if (obj != null)
			{
				return (Item2CartonCFG)obj;
			}

			return null;
		}

		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
//			manager.Add( new LengthCheck(lblItem2CartonCFGCodeEdit, txtItem2CartonCFGCodeEdit, 40, true) );
//			manager.Add( new DateCheck(lblPlanShipDateEdit, txtPlanShipDateEdit.Text, true) );
			object objItem = null;
			object[] objItems = null;
			try
			{
				if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
				objItems = itemFacade.QueryItem(null,txtItemName.Text.Trim().ToUpper(),null,null,null);
			}
			catch
			{}
			if(objItems == null ||  (objItems != null && objItems.Length < 1))
			{
				WebInfoPublish.Publish(this,"$Error_ItemCode_NotExist",languageComponent1);
				return false;
			}
			if(txtItemName.Text.Trim() == String.Empty)
			{
				WebInfoPublish.Publish(this,"$PageControl_ItemName $Error_Input_Empty",languageComponent1);
				return false;
			}
			if(txtMPlate.Text.Trim() == String.Empty)
			{
				WebInfoPublish.Publish(this,"$PageControl_Mplate $Error_Input_Empty",languageComponent1);
				return false;
			}
			if(txtCartonItemCode.Text.Trim() == String.Empty)
			{
				WebInfoPublish.Publish(this,"$PageControl_CartonItemCode $Error_Input_Empty",languageComponent1);
				return false;
			}

			if(drpPCSType.SelectedIndex == 1)
			{
				if(txtSPlate.Text.Trim() == String.Empty)
				{
					WebInfoPublish.Publish(this,"$PageControl_Splate $Error_Input_Empty",languageComponent1);
					return false;
				}
			}

			manager.Add( new NumberCheck(lblCartonCodeLen, txtCartonCodeLen, true) );
			manager.Add( new NumberCheck(lblStartPosition, txtStartPosition, true) );
			manager.Add( new NumberCheck(lblEndPosition, txtEndPosition, true) );
			manager.Add( new DecimalRangeCheck(lblStartPosition, txtStartPosition.Text,lblEndPosition,txtEndPosition.Text, false) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}

		protected override void SetEditObject(object obj)
		{
			if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			if (obj == null)
			{
				this.txtItemName.Text = String.Empty;
				this.drpPCSType.SelectedIndex = 0;
				this.txtCartonItemCode.Text = String.Empty;
				this.txtMPlate.Text = String.Empty;
				this.txtSPlate.Text = String.Empty;
				this.txtCartonCodeLen.Text = String.Empty;
				this.txtStartPosition.Text = String.Empty;
				this.txtEndPosition.Text =  String.Empty;
//				this.txtItem2CartonCFGCodeEdit.Text = string.Empty ;
//				this.txtPlanShipDateEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now ) );
//				this.txtActShipDateEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.MaxValue ) );
//				this.txtActShipDateEdit.Enable = "false";
				return;
			}
			
			Item2CartonCFG entity = (Item2CartonCFG)obj;
			this.txtItemName.Text = entity.ItemName;
			this.drpPCSType.SelectedIndex = Convert.ToInt32(entity.PCSTYPE);
			this.txtCartonItemCode.Text = entity.CartonItemNo;
			this.txtMPlate.Text = entity.MPlate;
			this.txtSPlate.Text = entity.SPlate;
			this.txtCartonCodeLen.Text = Convert.ToInt32(entity.CartonLabelLen).ToString();
			this.txtStartPosition.Text = Convert.ToInt32(entity.StartPosition).ToString();
			this.txtEndPosition.Text =  Convert.ToInt32(entity.EndPosition).ToString();
			
		}

		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			/*this.gridHelper.AddColumn( "ItemName",       "产品名称",	null);
			this.gridHelper.AddColumn( "CartonPCSType",  "板片类型",		null);
			this.gridHelper.AddColumn( "MPlate",   "M板",	null);
			this.gridHelper.AddColumn( "SPlate",   "S板",	null);
			this.gridHelper.AddColumn( "CartonItemCode",  "大箱料号",		null);
			this.gridHelper.AddColumn( "CartonItemCodeLen","大箱标签位数",		null);
			this.gridHelper.AddColumn( "StartPosition",   "起始位数",null);
			this.gridHelper.AddColumn( "EndPosition",   "结束位数",null);*/
			Item2CartonCFG entity = obj as Item2CartonCFG;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{   "false",
								entity.ItemName,
								entity.PCSTYPE.ToString()=="1"?languageComponent1.GetString(PCSType.DoubleSide.ToString()):languageComponent1.GetString(PCSType.SingleSide.ToString()),
								entity.MPlate,
								entity.SPlate,
								entity.CartonItemNo,
								entity.CartonLabelLen,
								entity.StartPosition,
								entity.EndPosition
							});
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if( itemFacade==null )
			{
				itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();
			}

			return itemFacade.QueryItem2CartonCFG(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtItemEdit.Text)),
				inclusive, exclusive);
		}
	
		protected override int GetRowCount()
		{
			if(itemFacade==null){itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			return itemFacade.QueryItem2CartonCFGCount(
			FormatHelper.CleanString(this.txtItemEdit.Text));

		}

		protected override string[] FormatExportRecord( object obj )
		{
			/*this.gridHelper.AddColumn( "ItemName",       "产品名称",	null);
			this.gridHelper.AddColumn( "CartonPCSType",  "板片类型",		null);
			this.gridHelper.AddColumn( "MPlate",   "M板",	null);
			this.gridHelper.AddColumn( "SPlate",   "S板",	null);
			this.gridHelper.AddColumn( "CartonItemCode",  "大箱料号",		null);
			this.gridHelper.AddColumn( "CartonItemCodeLen","大箱标签位数",		null);
			this.gridHelper.AddColumn( "StartPosition",   "起始位数",null);
			this.gridHelper.AddColumn( "EndPosition",   "结束位数",null);*/

			Item2CartonCFG entity = obj as Item2CartonCFG;
			return new string[]{ 
								entity.ItemName,
								entity.PCSTYPE.ToString()=="2"?languageComponent1.GetString(PCSType.DoubleSide.ToString()):languageComponent1.GetString(PCSType.SingleSide.ToString()),
								entity.MPlate,
								entity.SPlate,
								entity.CartonItemNo,
								Convert.ToInt32(entity.CartonLabelLen).ToString(),
								Convert.ToInt32(entity.StartPosition).ToString(),
								Convert.ToInt32(entity.EndPosition).ToString()
							};

//			Item2CartonCFG order = obj as Item2CartonCFG;
//			return new string[]{   order.Item2CartonCFGNumber,
//								   FormatHelper.ToDateString( order.PlanShipDate ),
//								   FormatHelper.ToDateString( order.ActShipDate ),
//								   languageComponent1.GetString( order.Item2CartonCFGStatus )};

		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[]{   
								"ItemName",
								"PCSTYPE",
								"MPlate",
								"SPlate",
								"CartonItemCode",
								"CartonItemCodeLen",
								"StartPosition",
								"EndPosition",
							};
//			return new string[] {	"Item2CartonCFGNO",
//									"PlanShipDate",
//									"ActShipDate",	
//									"Item2CartonCFGStatus"
//								};
		}

		#endregion

		protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
		{
			SetEditObject(null);
		}
		
	}
}
