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

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// ItemMP 的摘要说明。
	/// </summary>
	public partial class FOrderMP : BasePage
	{
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		public BenQGuru.eMES.Web.UserControl.eMESDate txtPlanShipDateEdit;
		public BenQGuru.eMES.Web.UserControl.eMESDate txtActShipDateEdit;
		
		private BenQGuru.eMES.MOModel.OrderFacade orderFacade = null ;


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
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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
				InitDropdownList();
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
		    PageCheckManager checkManager = new PageCheckManager();
			checkManager.Add(new LengthCheck(lblOrderStatusEdit,drpOrderStatusEdit,Int32.MaxValue,true));

			if( !checkManager.Check() )
			{
				WebInfoPublish.Publish(this,checkManager.CheckMessage,this.languageComponent1);
				return;
			}

			object order = this.GetEditObject();
			if(order != null)
			{
				if(orderFacade==null){orderFacade = new FacadeFactory(base.DataProvider).CreateOrderFacade();}
				this.orderFacade.AddOrder(order as Order);
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			}
		}

		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			if(orderFacade==null){orderFacade = new FacadeFactory(base.DataProvider).CreateOrderFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{

				ArrayList orders = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					object order = this.GetEditObject(row);
					if( order != null )
					{
						if( (order as Order).OrderStatus == OrderStatus.Completed)
						{
							ExceptionManager.Raise(this.GetType().BaseType,"$Error_Order_Completed_CANNOT_Delete");
							return;
						}
						orders.Add( (Order)order );
					}
				}
				
				this.orderFacade.DeleteOrder( (Order[])orders.ToArray( typeof(Order) ) );
				
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
			}
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			PageCheckManager checkManager = new PageCheckManager();
			checkManager.Add(new LengthCheck(lblOrderStatusEdit,drpOrderStatusEdit,Int32.MaxValue,true));

			if( !checkManager.Check() )
			{
				WebInfoPublish.Publish(this,checkManager.CheckMessage,this.languageComponent1);
				return;
			}

			if(orderFacade==null){orderFacade = new FacadeFactory(base.DataProvider).CreateOrderFacade();}
			object order = this.GetEditObject();

			if(order != null)
			{
				if( (order as Order).OrderStatus == OrderStatus.Completed)
				{
					ExceptionManager.Raise(this.GetType().BaseType,"$Error_Order_Completed_CANNOT_Update");
					return;
				}

				this.orderFacade.UpdateOrder( order as Order );;
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
			else if(e.Cell.Column.Key =="MO2Order")
			{
				//Response.Redirect(this.MakeRedirectUrl("../OQC/FItem2CheckListSP.aspx",new string[] {"itemcode"},new string[] {e.Cell.Row.Cells.FromKey("ItemCode").Text.Trim()}));
			}

			else if(e.Cell.Column.Key =="OrderDetail")
			{
				Response.Redirect(this.MakeRedirectUrl ("FOrderDetailMP.aspx",new string[] {"OrderNO"},new string[] {e.Cell.Row.Cells.FromKey("OrderNO").Text.Trim()}));
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
			if(pageAction == PageActionType.Add)
			{
				this.txtOrderCodeEdit.ReadOnly = false;
				this.drpOrderStatusEdit.SelectedValue = OrderStatus.InProcess;
				this.drpOrderStatusEdit.Enabled = false;
			}
			else if ( pageAction == PageActionType.Update )
			{
				this.txtOrderCodeEdit.ReadOnly = true;
			}
			else if ( pageAction == PageActionType.Save )
			{
				this.txtOrderCodeEdit.ReadOnly = false;
			}
			else if(pageAction ==PageActionType.Cancel)
			{
				this.txtOrderCodeEdit.ReadOnly = false;
				this.drpOrderStatusEdit.SelectedValue = OrderStatus.InProcess;
				this.drpOrderStatusEdit.Enabled = false;
			}
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "OrderNO",       "订单号",	null);
			this.gridHelper.AddColumn( "PlanShipDate",  "计划完成日期",		null);
			this.gridHelper.AddColumn( "ActShipDate",   "实际完成日期",	null);
			this.gridHelper.AddColumn( "OrderStatus",   "订单状态",	null);
			this.gridHelper.AddLinkColumn( "MO2Order",      "工单明细",		null);
			this.gridHelper.AddLinkColumn( "OrderDetail",   "订单详细信息",null);
			
			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private  void InitDropdownList()
		{
			this.drpOrderStatusEdit.Items.Clear();
			this.drpOrderStatusEdit.Items.Add( new ListItem( "", "" ));
			this.drpOrderStatusEdit.Items.Add( new ListItem( languageComponent1.GetString( OrderStatus.InProcess) , OrderStatus.InProcess) );
			this.drpOrderStatusEdit.Items.Add( new ListItem( languageComponent1.GetString( OrderStatus.Completed) , OrderStatus.Completed) );

			this.drpOrderStatusQuery.Items.Clear();
			this.drpOrderStatusQuery.Items.Add( new ListItem( "", "" ));
			this.drpOrderStatusQuery.Items.Add( new ListItem( languageComponent1.GetString( OrderStatus.InProcess) , OrderStatus.InProcess) );
			this.drpOrderStatusQuery.Items.Add( new ListItem( languageComponent1.GetString( OrderStatus.Completed), OrderStatus.Completed ) );
		}

		private  object GetEditObject()
		{
			if(	this.ValidateInput())
			{
				if(orderFacade==null){orderFacade = new FacadeFactory(base.DataProvider).CreateOrderFacade();}

				Order order = this.orderFacade.CreateNewOrder();

				order.OrderNumber = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOrderCodeEdit.Text, 40));
				order.PlanShipDate = FormatHelper.TODateInt( this.txtPlanShipDateEdit.Text );
				order.OrderStatus = drpOrderStatusEdit.SelectedValue ;
				order.ActShipDate = FormatHelper.TODateInt( this.txtActShipDateEdit.Text );
				order.MaintainUser = this.GetUserCode();
				
				return order;
			}
			else
			{
				return null;
			}
		}

		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(orderFacade==null){orderFacade = new FacadeFactory(base.DataProvider).CreateOrderFacade();}

			object obj = this.orderFacade.GetOrder(row.Cells.FromKey("OrderNO").Text.ToString());
			
			if (obj != null)
			{
				return (Order)obj;
			}

			return null;
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck(lblOrderCodeEdit, txtOrderCodeEdit, 40, true) );
			manager.Add( new DateCheck(lblPlanShipDateEdit, txtPlanShipDateEdit.Text, true) );
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}

		private void SetEditObject(object obj)
		{
			if(orderFacade==null){orderFacade = new FacadeFactory(base.DataProvider).CreateOrderFacade();}
			if (obj == null)
			{
				this.txtOrderCodeEdit.Text = string.Empty ;
				this.txtPlanShipDateEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now ) );
				this.txtActShipDateEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.MaxValue ) );
				this.txtActShipDateEdit.Enable = "false";
				return;
			}
			
			this.txtOrderCodeEdit.Text = ( obj as Order ).OrderNumber ;
			this.txtPlanShipDateEdit.Text = FormatHelper.ToDateString( ( obj as Order ).PlanShipDate );
			this.txtActShipDateEdit.Text = FormatHelper.ToDateString( ( obj as Order ).ActShipDate );
			this.txtActShipDateEdit.Enable = "false";
			this.drpOrderStatusEdit.SelectedValue = ( obj as Order ).OrderStatus;
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			Order order = obj as Order;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{   "false",
								order.OrderNumber,
								FormatHelper.ToDateString( order.PlanShipDate ),
								FormatHelper.ToDateString( order.ActShipDate ),
								languageComponent1.GetString( order.OrderStatus ),
								"","",""});
		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if( orderFacade==null )
			{
				orderFacade = new FacadeFactory(base.DataProvider).CreateOrderFacade();
			}

			return orderFacade.QueryORDER(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtOrderCodeQuery.Text)),
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtPartnerQuery.Text)),
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtConditionItem.Text)),
				drpOrderStatusQuery.SelectedValue,
				inclusive, exclusive);
		}
	
		private int GetRowCount()
		{
			if(orderFacade==null){orderFacade = new FacadeFactory(base.DataProvider).CreateOrderFacade();}
			return orderFacade.QueryORDERCount(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtOrderCodeQuery.Text)),
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtPartnerQuery.Text)),
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtConditionItem.Text)),
				drpOrderStatusQuery.SelectedValue);
		}

		private string[] FormatExportRecord( object obj )
		{
			Order order = obj as Order;
			return new string[]{   order.OrderNumber,
								   FormatHelper.ToDateString( order.PlanShipDate ),
								   FormatHelper.ToDateString( order.ActShipDate ),
								   languageComponent1.GetString( order.OrderStatus )};
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"OrderNO",
									"PlanShipDate",
									"ActShipDate",	
									"OrderStatus"
			                        };
		}

		#endregion

		

	}
}
