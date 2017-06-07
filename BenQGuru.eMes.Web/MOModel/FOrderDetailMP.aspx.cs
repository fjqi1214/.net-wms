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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FItemLocationMP 的摘要说明。
	/// </summary>
	public partial class FOrderDetailMP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		public BenQGuru.eMES.Web.UserControl.eMESDate txtPlanDateEdit;
		public BenQGuru.eMES.Web.UserControl.eMESDate txtActDateEdit;

		private OrderFacade _orderFacade ;

	
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


		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				Initparameters();
				InitViewPanel();
			}
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		private void InitViewPanel()
		{
			this.txtOrderCodeQuery.Text = OrderNO;
			this.txtOrderCodeQuery.ReadOnly = true;

			this.txtOrderEdit.Text = OrderNO;
			this.txtOrderEdit.ReadOnly = true;

			this.drpOrderStatusEdit.Items.Clear();
			this.drpOrderStatusEdit.Items.Add( new ListItem( "", "" ));
			this.drpOrderStatusEdit.Items.Add( new ListItem( languageComponent1.GetString( OrderStatus.InProcess) , OrderStatus.InProcess) );
			this.drpOrderStatusEdit.Items.Add( new ListItem( languageComponent1.GetString( OrderStatus.Completed) , OrderStatus.Completed) );
			if(_orderFacade==null){_orderFacade = new  FacadeFactory(base.DataProvider).CreateOrderFacade();}
			Object obj = _orderFacade.GetOrder( OrderNO );
			this.drpOrderStatusEdit.SelectedValue = ( obj as Order ).OrderStatus;
			this.drpOrderStatusEdit.Enabled = false;

			this.txtPlanDateEdit.Text = FormatHelper.ToDateString( ( obj as Order ).PlanShipDate );
			this.txtPlanDateEdit.Enable = "false";

			this.txtActDateEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.MaxValue ) );
		}

		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
			this.gridHelper.AddColumn( "OrderNO",       "订单号",	null);
			this.gridHelper.AddColumn( "PartnerCode",   "客户代码",	null);
			this.gridHelper.AddColumn( "PartnerName",   "客户名称",	null);
			this.gridHelper.AddColumn( "ItemCode",      "产品代码",	null);
			this.gridHelper.AddColumn( "ItemName",      "产品名称",	null);
			this.gridHelper.AddColumn( "PlanQTY",       "计划数量",	null);
			this.gridHelper.AddColumn( "PlanDate",      "计划完成日期",	null);
			this.gridHelper.AddColumn( "ActQTY",        "实际数量",	null);
			this.gridHelper.AddColumn( "ActDate",       "实际完成日期",	null);
			this.gridHelper.AddLinkColumn( "MO2Order",  "工单明细",		null);

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridHelper.RequestData();
		}

		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			OrderDetail orderdetail = obj as  OrderDetail;
				return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
					new object[]{"false",
									orderdetail.OrderNumber,
									orderdetail.PartnerCode,
									orderdetail.PartnerDesc,
									orderdetail.ItemCode,
									orderdetail.ItemName,
									orderdetail.PlanQTY.ToString("##.##"),
									this.txtPlanDateEdit.Text,
									orderdetail.ActQTY.ToString("##.##"),
									FormatHelper.ToDateString( orderdetail.ActDate ),
									"",
									""});
		
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_orderFacade==null){_orderFacade = new  FacadeFactory(base.DataProvider).CreateOrderFacade();}
			return this._orderFacade.QueryOrderDetail(
				"","", 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(OrderNO)),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_orderFacade==null){_orderFacade = new  FacadeFactory(base.DataProvider).CreateOrderFacade();}
			return _orderFacade.QueryOrderDetailCount( 
				"","", FormatHelper.PKCapitalFormat(FormatHelper.CleanString(OrderNO)));
		}

		#endregion

		#region Button
		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FOrderMP.aspx"));
		}

		protected override void AddDomainObject(object domainObject)
		{
			if( this.drpOrderStatusEdit.SelectedValue == OrderStatus.Completed)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_Order_Completed_Detail_CANNOT_Add");
				return;
			}

			if(_orderFacade==null){_orderFacade = new  FacadeFactory(base.DataProvider).CreateOrderFacade();}
			this._orderFacade.AddOrderDetail( (OrderDetail)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if( this.drpOrderStatusEdit.SelectedValue == OrderStatus.Completed)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_Order_Completed_Detail_CANNOT_Delete");
				return;
			}
			if(_orderFacade==null){_orderFacade = new  FacadeFactory(base.DataProvider).CreateOrderFacade();}
			this._orderFacade.DeleteOrderDetail( (OrderDetail[])domainObjects.ToArray( typeof(OrderDetail) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if( this.drpOrderStatusEdit.SelectedValue == OrderStatus.Completed)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_Order_Completed_Detail_CANNOT_Update");
				return;
			}
			if(_orderFacade==null){_orderFacade = new  FacadeFactory(base.DataProvider).CreateOrderFacade();}
			this._orderFacade.UpdateOrderDetail( (OrderDetail)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtActQTYEdit.ReadOnly = true;
				this.txtActDateEdit.Enable = "false";
			}
			
			if ( pageAction == PageActionType.Update )
			{
				this.txtActQTYEdit.ReadOnly = true;
				this.txtActDateEdit.Enable = "false";
			}

			if(pageAction == PageActionType.Cancel)
			{
				this.txtActQTYEdit.ReadOnly = true;
				this.txtActDateEdit.Enable = "false";
			}
		}
		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			if(_orderFacade==null){_orderFacade = new  FacadeFactory(base.DataProvider).CreateOrderFacade();}
			OrderDetail orderDetail = this._orderFacade.CreateNewOrderDetail();

			orderDetail.OrderNumber = this.txtOrderEdit.Text;
			orderDetail.PartnerCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPartnerCodeEdit.Text));
			orderDetail.ItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemEdit.Text));

			ItemFacade itemfacade = new  FacadeFactory(base.DataProvider).CreateItemFacade();
            Object item = itemfacade.GetItem(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemEdit.Text)), GlobalVariables.CurrentOrganizations.First().OrganizationID);
			orderDetail.ItemName = ( item as Item ).ItemName;

			orderDetail.PlanDate = FormatHelper.TODateInt( this.txtPlanDateEdit.Text );
			orderDetail.PlanQTY = Convert.ToDecimal( this.txtPlanQTYEdit.Text );
			orderDetail.ActDate = FormatHelper.TODateInt( this.txtActDateEdit.Text.Trim() );
			orderDetail.ActQTY = Convert.ToDecimal( this.txtActQTYEdit.Text.Trim() );
			orderDetail.MaintainUser = this.GetUserCode();

			return orderDetail;
		}

		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_orderFacade==null){_orderFacade = new  FacadeFactory(base.DataProvider).CreateOrderFacade();}
			object obj = _orderFacade.GetOrderDetail(
				row.Cells.FromKey("PartnerCode").Text.ToString(), 
				row.Cells.FromKey("ItemCode").Text.ToString(),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(OrderNO)));
			
			if (obj != null)
			{
				return (OrderDetail)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtPartnerCodeEdit.Text = String.Empty;
				this.txtItemEdit.Text = string.Empty;
				this.txtPlanQTYEdit.Text = "0";
				this.txtActQTYEdit.Text = "0";
				this.txtActDateEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.MaxValue ) );
				return;
			}
			
			this.txtPartnerCodeEdit.Text = ( obj as OrderDetail ).PartnerCode;
			this.txtItemEdit.Text = ( obj as OrderDetail ).ItemCode;
			this.txtPlanQTYEdit.Text = ( obj as OrderDetail ).PlanQTY.ToString("##.##");
			this.txtActQTYEdit.Text = ( obj as OrderDetail ).ActQTY.ToString("##.##");
			this.txtPlanDateEdit.Text = FormatHelper.ToDateString( ( obj as OrderDetail ).PlanDate );
			this.txtActDateEdit.Text = FormatHelper.ToDateString( ( obj as OrderDetail ).ActDate );
		}

		
		protected override bool ValidateInput()
		{			
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblCusCodeEdit,txtPartnerCodeEdit,40,true) );
			manager.Add( new LengthCheck(lblItemCodeEdit,txtItemEdit,40,true) );
			manager.Add( new LengthCheck(lblPlanQTYEdit, txtPlanQTYEdit, 10, true) );
			manager.Add( new NumberCheck(lblPlanQTYEdit, txtPlanQTYEdit, 0,int.MaxValue, true) );
			manager.Add( new NumberCheck(lblActQTYEdit, txtActQTYEdit, 0,int.MaxValue, true) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);

				return false;
			}

			return true;
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			OrderDetail orderdetail = obj as OrderDetail ;
			return new string[]{  
				orderdetail.OrderNumber,
				orderdetail.PartnerCode,
				orderdetail.PartnerDesc,
				orderdetail.ItemCode,
				orderdetail.ItemName,
				orderdetail.PlanQTY.ToString("##.##"),
				this.txtPlanDateEdit.Text,
				orderdetail.ActQTY.ToString("##.##"),
				FormatHelper.ToDateString( orderdetail.ActDate )
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"OrderNO",
									"PartnerCode",
									"PartnerName",
									"ItemCode",
									"ItemName",
									"PlanQTY",
									"PlanDate",
									"ActQTY",
									"ActDate"
									};
		}
		#endregion

		#region property
		private void Initparameters()
		{
			if(this.Request.Params["OrderNO"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["OrderNO"] = this.Request.Params["OrderNO"];
			}
		}

		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}


	
		public string OrderNO
		{
			get
			{
				return (string) this.ViewState["OrderNO"];
			}
		}
		#endregion

		
	}
}
