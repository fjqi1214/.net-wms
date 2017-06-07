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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FStockOutDetailQP 的摘要说明。
	/// </summary>
	public partial class FStockOutDetailQP : BaseQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.TextBox txtInTicketToQuery;
		protected System.Web.UI.WebControls.Label lblReceivNoQuery;
		protected System.Web.UI.WebControls.Label lblStatusQuery;
		protected System.Web.UI.WebControls.DropDownList drpStatus;


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
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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

		protected GridHelper _gridHelper = null;
		protected WebQueryHelper _helper = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.cmdQuery.Attributes["onclick"] ="document.all.hidAction.value='query'";
			this.cmdGridExport.Attributes["onclick"] = "document.all.hidAction.value='exp'";
			this._gridHelper = new GridHelper(this.gridWebGrid);

			this._helper = new WebQueryHelper( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);
			this._helper.MergeColumnIndexList = new object[]{ new int[]{0,1}, new int[]{2,3} };

			if(!Page.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();
				this.txtPrintDateTo.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				this.txtPrintDateFrom.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );

				//加载出库类型
				this.LoadINVReceiveType();
			}
		}

		private string GetReceiveStausName(string code)
		{
			if(code == ShipStatus.Shipping)
				return "初始";
			else if(code == ShipStatus.Shipped)
				return "已完成";
			else
				return "";
		}
		private void _initialWebGrid()
		{
			this._gridHelper.AddColumn("ShipNo","出货单号",null);
			this._gridHelper.AddColumn("SEQ","SEQ",null);
			this._gridHelper.AddColumn("ShipType","出货单类型",null);
			this._gridHelper.AddColumn("Partner","客户代码",null);
			this._gridHelper.AddColumn("PartnerDesc","客户名称",null);
			this._gridHelper.AddColumn("ModelCode","产品别",null);
			this._gridHelper.AddColumn("ItemCode","产品代码",null);
			this._gridHelper.AddColumn("ItemDesc","产品描述",null);
			this._gridHelper.AddColumn("Status","出货单状态",null);
			this._gridHelper.AddColumn("ShipDate","发货日期",null);
			this._gridHelper.AddColumn("Ship_PlanQty","计划出库数量",null);
			this._gridHelper.AddColumn("ActQty","实际采集数量",null);
			this._gridHelper.AddColumn("ReceiveDesc","备注信息",null);
			this._gridHelper.AddLinkColumn("RCardList","序列号列表",null);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );

			this._gridHelper.Grid.Columns.FromKey("Ship_PlanQty").DataType = typeof(System.Decimal).ToString();
			this._gridHelper.Grid.Columns.FromKey("ActQty").DataType = typeof(System.Decimal).ToString();

			this.gridWebGrid.Columns.FromKey("SEQ").Hidden = true;
			this.gridWebGrid.Columns.FromKey("RCardList").Width = System.Web.UI.WebControls.Unit.Parse("8%");
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			if(!this.chbExpDetail.Checked)
			{
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"出货单号",
									"出货单类型",
									"客户代码",
									"客户名称",
									"产品别",
									"产品代码",
									"产品描述",
									"出货单状态",
									"发货日期",
									"计划数量",
									"实际采集数量",
									"备注信息"	
								};
			}
			else//导出二级界面
			{
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"出货单号",
									"出货单类型",
									"客户代码",
									"客户名称",
									"产品别",
									"产品代码",
									"产品描述",
									"出货单状态",
									"发货日期",
									"数量",
									"备注信息",
									"产品序列号",
									"入库单号",
									"出货采集人员",
									"出货采集日期",
									"CartonNo"
								};	
			}
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
			if(this.chbExpDetail.Checked && this.hidAction.Value == "exp")
			{
				#region 导出二级界面
				PageCheckManager manager = new PageCheckManager();
			
				manager.Add( new DateRangeCheck(this.lblOutDateFromQuery, this.dateInDateFromQuery.Text, this.dateInDateToQuery.Text,false) );

				manager.Add( new DateRangeCheck(this.lblPrintDate, this.txtPrintDateFrom.Text, this.txtPrintDateFrom.Text,false) );

				if( !manager.Check() )
				{
					WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
					return;
				}

				BenQGuru.eMES.Common.Domain.IDomainDataProvider provider = null;
				try
				{
					provider = base.DataProvider;
					BenQGuru.eMES.Material.InventoryFacade facade = new InventoryFacade(provider);

					object[] dataSource = facade.QueryShipRCardWeb(
						this.txtCartonNo.Text.Trim().ToUpper(),
						this.txtShipNoQuery.Text.Trim(),
						this.txtPartner.Text.Trim(),
						this.dateInDateFromQuery.Text,
						this.dateInDateToQuery.Text,
						this.txtModel.Text.Trim(),
						this.txtItemCode.Text.Trim(),
						this.txtRCardFrom.Text.Trim(),
						this.txtRCardTo.Text, 
						this.txtPrintDateFrom.Text,
						this.txtPrintDateTo.Text,
						this.drpShipype.SelectedValue,
						( e as WebQueryEventArgs ).StartRow,
						( e as WebQueryEventArgs ).EndRow);

					( e as WebQueryEventArgs ).GridDataSource = dataSource;

				}
				finally
				{
					if(provider != null)
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)provider).PersistBroker.CloseConnection();
				}
				#endregion
			}
			else
			{
				#region 不导出二级界面
				PageCheckManager manager = new PageCheckManager();
			
				manager.Add( new DateRangeCheck(this.lblOutDateFromQuery, this.dateInDateFromQuery.Text, this.dateInDateToQuery.Text,false) );

				manager.Add( new DateRangeCheck(this.lblPrintDate, this.txtPrintDateFrom.Text, this.txtPrintDateFrom.Text,false) );

				if( !manager.Check() )
				{
					WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
					return;
				}

				BenQGuru.eMES.Common.Domain.IDomainDataProvider provider = null;
				try
				{
					provider = base.DataProvider;
					BenQGuru.eMES.Material.InventoryFacade facade = new InventoryFacade(provider);

					object[] dataSource = facade.QueryInvShipWeb(
						this.txtCartonNo.Text.Trim().ToUpper(),
						this.txtShipNoQuery.Text.Trim(),
						this.txtPartner.Text.Trim(),
						this.dateInDateFromQuery.Text,
						this.dateInDateToQuery.Text,
						this.txtModel.Text.Trim(),
						this.txtItemCode.Text.Trim(),
						this.txtRCardFrom.Text.Trim(),
						this.txtRCardTo.Text,
						this.txtPrintDateFrom.Text,
						this.txtPrintDateTo.Text,
						this.drpShipype.SelectedValue,
						this.txtConditionMo.Text,
						( e as WebQueryEventArgs ).StartRow,
						( e as WebQueryEventArgs ).EndRow);

					( e as WebQueryEventArgs ).GridDataSource = dataSource;

					( e as WebQueryEventArgs ).RowCount = 
						facade.QueryInvShipWebCount(
						this.txtCartonNo.Text.Trim().ToUpper(),
						this.txtShipNoQuery.Text.Trim(),
						this.txtPartner.Text.Trim(),
						this.dateInDateFromQuery.Text,
						this.dateInDateToQuery.Text,
						this.txtModel.Text.Trim(),
						this.txtItemCode.Text.Trim(),
						this.txtRCardFrom.Text.Trim(),
						this.txtRCardTo.Text,
						this.txtPrintDateFrom.Text,
						this.txtPrintDateTo.Text,
						this.drpShipype.SelectedValue,
						this.txtConditionMo.Text);

				}
				finally
				{
					if(provider != null)
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)provider).PersistBroker.CloseConnection();
				}
				#endregion
			}
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			BenQGuru.eMES.Common.Domain.IDomainDataProvider provider = null;
			try
			{
				provider = base.DataProvider;
				BenQGuru.eMES.Material.InventoryFacade facade = new InventoryFacade(provider);

				if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
				{
					BenQGuru.eMES.Domain.Material.InvShip obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as BenQGuru.eMES.Domain.Material.InvShip;
					if(obj != null)
					{
						/*int sum = facade.QueryInvShipWebSum(obj.ShipNo,
													obj.ShipSeq,
													this.txtPartner.Text.Trim(),
													this.dateInDateFromQuery.Text,
													this.dateInDateToQuery.Text,
													this.txtModel.Text.Trim(),
													this.txtItemCode.Text.Trim(),
													this.txtRCardFrom.Text.Trim(),
													this.txtRCardTo.Text,
													this.txtPrintDateFrom.Text,
													this.txtPrintDateTo.Text
													);*/

						( e as DomainObjectToGridRowEventArgs ).GridRow = 
							new UltraGridRow( new object[]{
															  obj.ShipNo,
															  obj.ShipSeq,
															  obj.ShipType,
															  obj.PartnerCode,
															  obj.PartnerDesc,
															  obj.ModelCode,
															  obj.ItemCode,
															  obj.ItemDesc,
															  GetReceiveStausName(obj.ShipStatus),
															  FormatHelper.ToDateString(obj.ShipDate),
															  obj.PlanQty,
															  obj.ActQty,
															  obj.ShipDesc,
															  string.Empty
														  }
															);
					}
				}
			}
			finally
			{
				if(provider != null)
					((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)provider).PersistBroker.CloseConnection();
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if(!this.chbExpDetail.Checked)
			{
				#region 不导出二级界面
				BenQGuru.eMES.Common.Domain.IDomainDataProvider provider = null;
				try
				{
					provider = base.DataProvider;
					BenQGuru.eMES.Material.InventoryFacade facade = new InventoryFacade(provider);
					if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
					{
						BenQGuru.eMES.Domain.Material.InvShip obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as BenQGuru.eMES.Domain.Material.InvShip;

						/*int sum = facade.QueryInvShipWebSum(obj.ShipNo,
							obj.ShipSeq,
							this.txtPartner.Text.Trim(),
							this.dateInDateFromQuery.Text,
							this.dateInDateToQuery.Text,
							this.txtModel.Text.Trim(),
							this.txtItemCode.Text.Trim(),
							this.txtRCardFrom.Text.Trim(),
							this.txtRCardTo.Text,
							this.txtPrintDateFrom.Text,
							this.txtPrintDateTo.Text);*/

						( e as DomainObjectToExportRowEventArgs ).ExportRow = 
							new string[]{
											obj.ShipNo,
											obj.ShipType,
											obj.PartnerCode,
											obj.PartnerDesc,
											obj.ModelCode,
											obj.ItemCode,
											obj.ItemDesc,
											GetReceiveStausName(obj.ShipStatus),
											FormatHelper.ToDateString(obj.ShipDate),
											obj.PlanQty.ToString(),
											obj.ActQty.ToString(),
											obj.ShipDesc,
											string.Empty
										};
					}
				}
				finally
				{
					if(provider != null)
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)provider).PersistBroker.CloseConnection();
				}
				#endregion
			}
			else
			{
				#region 导出二级界面

				BenQGuru.eMES.Common.Domain.IDomainDataProvider provider = null;
				try
				{
					provider = base.DataProvider;
					BenQGuru.eMES.Material.InventoryFacade facade = new InventoryFacade(provider);
					if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
					{
						BenQGuru.eMES.Domain.Material.ShipRCard obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as BenQGuru.eMES.Domain.Material.ShipRCard;

						( e as DomainObjectToExportRowEventArgs ).ExportRow = 
																	new string[]{
																				obj.ShipNo,
																				obj.ShipType,
																				obj.PartnerCode,
																				obj.PartnerDesc,
																				obj.ModelCode,
																				obj.ItemCode,
																				obj.ItemDesc,
																				GetReceiveStausName(obj.ShipStatus),
																				FormatHelper.ToDateString(obj.ShipDate),
																				obj.ActQty.ToString(),
																				obj.ShipDesc,
																				obj.RunningCard,
																				obj.RecNO,
																				obj.RCardShipUser,
																				FormatHelper.ToDateString(obj.RCardShipDate),
																				obj.CartonCode
																				};
					}
				}
				finally
				{
					if(provider != null)
						((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)provider).PersistBroker.CloseConnection();
				}

				#endregion
			}
		}

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if(e.Cell.Column.Key =="RCardList")
			{
				this.Session["FInvRCardQP_No"] = e.Cell.Row.Cells.FromKey("ShipNo").Text;
				this.Session["FInvRCardQP_Seq"] = e.Cell.Row.Cells.FromKey("SEQ").Text;
				this.Session["FInvRCardQP_From"] = this.txtRCardFrom.Text.Trim();
				this.Session["FInvRCardQP_To"] = this.txtRCardTo.Text.Trim();
				this.Session["FInvRCardQP_DateFrom"] = this.dateInDateFromQuery.Text;
				this.Session["FInvRCardQP_DateTo"] = this.dateInDateToQuery.Text;
				this.Session["FInvRCardQP_Status"] = string.Empty;
				this.Session["FInvRCardQP_Type"] = "ship";

				Response.Redirect(this.MakeRedirectUrl("FInvRCardQP.aspx"));
			}

		}

		//加载入库类型
		private void LoadINVReceiveType()
		{
			object[] shipTypeParameters = this.GeINVShipTypes();
			if(shipTypeParameters != null)
			{
				this.drpShipype.Items.Clear();
				this.drpShipype.Items.Add(new ListItem("所有",""));
				foreach( BenQGuru.eMES.Domain.BaseSetting.Parameter shipType in shipTypeParameters )
				{
					this.drpShipype.Items.Add(new ListItem(shipType.ParameterCode,shipType.ParameterCode));
				}
			}
		}

		private object[] GeINVShipTypes()
		{
			BenQGuru.eMES.BaseSetting.SystemSettingFacade ssfacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(base.DataProvider);
			object[] objs = ssfacade.GetParametersByParameterGroup("INVSHIP");
			ssfacade = null;
			return objs;
		}
	}
}
