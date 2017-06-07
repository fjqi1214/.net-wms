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
	/// FStockInDetailQP 的摘要说明。
	/// </summary>
	public partial class FStockInDetailQP : BaseQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.TextBox txtInTicketToQuery;

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
				dateInDateFromQuery.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				dateInDateToQuery.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );

				//状态下拉列表
				this.drpStatus.Items.Clear();
				this.drpStatus.Items.Add(new ListItem("所有",""));
				this.drpStatus.Items.Add(new ListItem("未出货",RCardStatus.Received));
				this.drpStatus.Items.Add(new ListItem("已出货",RCardStatus.Shipped));

				//加载入库类型
				this.LoadINVReceiveType();
			}
		}

		private string GetReceiveStausName(string code)
		{
			if(code == ReceiveStatus.Receiving)
				return "初始";
			else if(code == ReceiveStatus.Received)
				return "已完成";
			else
				return "";
		}
		private void _initialWebGrid()
		{
			this._gridHelper.AddColumn("ReceiveNo","入库单号",null);
			this._gridHelper.AddColumn("SEQ","SEQ",null);
			this._gridHelper.AddColumn("ReceiveType","入库单类型",null);
			this._gridHelper.AddColumn("ModelCode","产品别",null);
			this._gridHelper.AddColumn("ItemCode","产品代码",null);
			this._gridHelper.AddColumn("ItemDesc","产品描述",null);
			this._gridHelper.AddColumn("Status","入库单状态",null);
			this._gridHelper.AddColumn("Rev_PlanQty","计划入库数量",null);
			this._gridHelper.AddColumn("Rev_ActQty","实际采集数量",null);
			this._gridHelper.AddColumn("ReceiveDesc","备注信息",null);
			this._gridHelper.AddLinkColumn("RCardList","序列号列表",null);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );

			this._gridHelper.Grid.Columns.FromKey("Rev_PlanQty").DataType = typeof(System.Decimal).ToString();
			this._gridHelper.Grid.Columns.FromKey("Rev_ActQty").DataType = typeof(System.Decimal).ToString();

			this.gridWebGrid.Columns.FromKey("SEQ").Hidden = true;
			//this.gridWebGrid.Columns.FromKey("ActQty").Width = System.Web.UI.WebControls.Unit.Parse("60px");
			//this.gridWebGrid.Columns.FromKey("ReceiveType").Width = System.Web.UI.WebControls.Unit.Parse("80px");
			//this.gridWebGrid.Columns.FromKey("ModelCode").Width = System.Web.UI.WebControls.Unit.Parse("60px");
			this.gridWebGrid.Columns.FromKey("RCardList").Width = System.Web.UI.WebControls.Unit.Parse("8%");
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			if(!this.chbExpDetail.Checked)
			{
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"入库单号",
									"入库单类型",
									"产品别",
									"产品代码",
									"产品描述",
									"入库单状态",
									"计划数量",
									"实际采集数量",
									"备注信息"	
								};
			}
			else //导出二级界面
			{
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"入库单号",
									"入库单类型",
									"产品别",
									"产品代码",
									"产品描述",
									"入库单状态",
									"数量",
									"备注信息",
									"产品序列号",
									"产品序列号状态",
									"入库采集人员",
									"入库采集日期",
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
			
				manager.Add( new DateRangeCheck(this.lblInDateQuery, this.dateInDateFromQuery.Text, this.dateInDateToQuery.Text,false) );

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

					object[] dataSource = facade.QueryRecRCardWeb(
																	this.txtCartonNo.Text.Trim().ToUpper(),
																	this.txtReceivQuery.Text.Trim(),
																	this.drpStatus.SelectedValue,
																	this.dateInDateFromQuery.Text,
																	this.dateInDateToQuery.Text,
																	this.txtModel.Text.Trim(),
																	this.txtItemCode.Text.Trim(),
																	this.txtRCardFrom.Text.Trim(),
																	this.txtRCardTo.Text, 
																	this.drpRecType.SelectedValue,
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
			
				manager.Add( new DateRangeCheck(this.lblInDateQuery, this.dateInDateFromQuery.Text, this.dateInDateToQuery.Text,false) );

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

					object[] dataSource = facade.QueryInvReceiveWeb(
						this.txtCartonNo.Text.Trim().ToUpper(),
						this.txtReceivQuery.Text.Trim(),
						this.drpStatus.SelectedValue,
						this.dateInDateFromQuery.Text,
						this.dateInDateToQuery.Text,
						this.txtModel.Text.Trim(),
						this.txtItemCode.Text.Trim(),
						this.txtRCardFrom.Text.Trim(),
						this.txtRCardTo.Text, 
						this.drpRecType.SelectedValue,
						this.txtConditionMo.Text,
						( e as WebQueryEventArgs ).StartRow,
						( e as WebQueryEventArgs ).EndRow);

					( e as WebQueryEventArgs ).GridDataSource = dataSource;

					( e as WebQueryEventArgs ).RowCount = 
						facade.QueryInvReceiveWebCount(
						this.txtCartonNo.Text.Trim().ToUpper(),
						this.txtReceivQuery.Text.Trim(),
						this.drpStatus.SelectedValue,
						this.dateInDateFromQuery.Text,
						this.dateInDateToQuery.Text,
						this.txtModel.Text.Trim(),
						this.txtItemCode.Text.Trim(),
						this.txtRCardFrom.Text.Trim(),
						this.txtRCardTo.Text,
						this.drpRecType.SelectedValue,
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
					BenQGuru.eMES.Domain.Material.InvReceive obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as BenQGuru.eMES.Domain.Material.InvReceive;
					if(obj != null)
					{
						/*int sum = facade.QueryInvReceiveWebSum(obj.RecNo,
													obj.RecSeq,
													this.drpStatus.SelectedValue,
													this.dateInDateFromQuery.Text,
													this.dateInDateToQuery.Text,
													this.txtModel.Text.Trim(),
													this.txtItemCode.Text.Trim(),
													this.txtRCardFrom.Text.Trim(),
													this.txtRCardTo.Text
													);*/

						( e as DomainObjectToGridRowEventArgs ).GridRow = 
							new UltraGridRow( new object[]{
															  obj.RecNo,
															  obj.RecSeq,
															  obj.RecType,
															  obj.ModelCode,
															  obj.ItemCode,
															  obj.ItemDesc,
															  GetReceiveStausName(obj.RecStatus),
															  obj.PlanQty,
															  obj.ActQty,
															  obj.Description,
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
						BenQGuru.eMES.Domain.Material.InvReceive obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as BenQGuru.eMES.Domain.Material.InvReceive;

						/*int sum = facade.QueryInvReceiveWebSum(obj.RecNo,
							obj.RecSeq,
							this.drpStatus.SelectedValue,
							this.dateInDateFromQuery.Text,
							this.dateInDateToQuery.Text,
							this.txtModel.Text.Trim(),
							this.txtItemCode.Text.Trim(),
							this.txtRCardFrom.Text.Trim(),
							this.txtRCardTo.Text);*/

				
						( e as DomainObjectToExportRowEventArgs ).ExportRow = 
							new string[]{
											obj.RecNo,
											obj.RecType,
											obj.ModelCode,
											obj.ItemCode,
											obj.ItemDesc,
											GetReceiveStausName(obj.RecStatus),
											obj.PlanQty.ToString(),
											obj.ActQty.ToString(),
											obj.Description,
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
						BenQGuru.eMES.Domain.Material.ReceiveRCard obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as BenQGuru.eMES.Domain.Material.ReceiveRCard;
	
						( e as DomainObjectToExportRowEventArgs ).ExportRow = 
							new string[]{
											obj.RecNo,
											obj.RecType,
											obj.ModelCode,
											obj.ItemCode,
											obj.ItemDesc,
											GetReceiveStausName(obj.RecStatus),
											obj.ActQty.ToString(),
											obj.Description,
											obj.RunningCard,
											RCardStatus.GetName(obj.RCardStatus),
											obj.ReceiveUser,
											FormatHelper.ToDateString(obj.ReceiveDate),
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
				this.Session["FInvRCardQP_Type"] = "Receive";
				this.Session["FInvRCardQP_No"] = e.Cell.Row.Cells.FromKey("ReceiveNo").Text;
				this.Session["FInvRCardQP_Seq"] = e.Cell.Row.Cells.FromKey("SEQ").Text;
				this.Session["FInvRCardQP_Status"] = this.drpStatus.SelectedValue;
				this.Session["FInvRCardQP_From"] = this.txtRCardFrom.Text.Trim();
				this.Session["FInvRCardQP_To"] = this.txtRCardTo.Text.Trim();
				this.Session["FInvRCardQP_DateFrom"] = this.dateInDateFromQuery.Text;
				this.Session["FInvRCardQP_DateTo"] = this.dateInDateToQuery.Text;

				Response.Redirect(this.MakeRedirectUrl("FInvRCardQP.aspx"));
			}

		}

		//加载入库类型
		private void LoadINVReceiveType()
		{
			object[] recTypeParameters = this.GeINVReceiveTypes();
			if(recTypeParameters != null)
			{
				this.drpRecType.Items.Clear();
				this.drpRecType.Items.Add(new ListItem("所有",""));
                foreach (BenQGuru.eMES.Domain.BaseSetting.Parameter recType in recTypeParameters)
				{
					this.drpRecType.Items.Add(new ListItem(recType.ParameterCode,recType.ParameterCode));
				}
			}
		}

	    private object[] GeINVReceiveTypes()
		{
			BenQGuru.eMES.BaseSetting.SystemSettingFacade ssfacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(base.DataProvider);
			object[] objs = ssfacade.GetParametersByParameterGroup("INVRECEIVE");
			ssfacade = null;
			return objs;
		}
	}
}
