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
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Alert;
using BenQGuru.eMES.AlertModel;
#endregion

namespace BenQGuru.eMES.Web.Alert
{
	/// <summary>
	/// ItemMP 的摘要说明。
	/// </summary>
	public partial class FAlertMP : BaseMPage
	{
		#region 变量声明
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdSave;
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		private BenQGuru.eMES.AlertModel.AlertFacade m_alertFacade;
		private AlertConst _alertConst;
		protected BenQGuru.eMES.Web.UserControl.eMESDate dateFrom;
		protected BenQGuru.eMES.Web.UserControl.eMESDate dateTo;
		protected System.Web.UI.WebControls.CheckBox CheckBox1;
		#endregion

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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			_alertConst = new AlertConst(this.languageComponent1);
			this.cmdGridExport.Attributes["onclick"] = "document.all.hidAction.value='exp'";
			this.cmdQuery.Attributes["onclick"] = "document.all.hidAction.value='query'";
			#region auto refresh
			if(this.chkRefreshAuto.Checked)
			{
				int mi = 5;
				try
				{
					mi = int.Parse(this.txtMi.Text);
				}
				catch
				{
					this.txtMi.Text = AutoRefreshConst.defaultMI.ToString();
				}
				this.refreshCtrl.Interval= AutoRefreshConst.GetMMInterval(mi);
				this.refreshCtrl.Start();
				//this.gr
			}
			else
			{
				this.refreshCtrl.Stop();
			}
			#endregion

			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				#region Init Control's Value List
				this.dateFrom.Text = DateTime.Now.ToShortDateString();
				this.dateTo.Text = DateTime.Now.ToShortDateString();
				
				AlertTypeBuilder.Buid(this.drpAlertType.Items,this._alertConst);
				this.drpAlertType.Items.Insert(0,new ListItem(_alertConst.GetName("*"),"*"));
				this.drpAlertType.Items.Add(new ListItem(_alertConst.GetName(AlertType_Old.Manual),AlertType_Old.Manual));

				AlertItemBuilder.Build(this.drpAlertType.SelectedValue,this.drpAlertItem.Items,this._alertConst);
				this.drpAlertItem.Items.Insert(0,new ListItem(_alertConst.GetName("*"),"*"));

				AlertStatusBuilder.Build(this.chlAlertStatus.Items,this._alertConst);
				this.chlAlertStatus.Items.FindByValue(AlertStatus_Old.Unhandled).Selected = true;
				this.chlAlertStatus.Items.FindByValue(AlertStatus_Old.Observing).Selected = true;
				this.chlAlertStatus.Items.FindByValue(AlertStatus_Old.Handling).Selected = true;

				this.lblPrimaryColor.ForeColor = ColorHelper.GetColor(AlertLevel_Old.Primary);
				this.lblImportantColor.ForeColor = ColorHelper.GetColor(AlertLevel_Old.Important);
				this.lblSeverityColor.ForeColor = ColorHelper.GetColor(AlertLevel_Old.Severity);
				#endregion region
				this.stbItem.Enabled = !(this.drpAlertItem.SelectedValue == "*");
			}
		}

		BenQGuru.eMES.AlertModel.AlertFacade _alertFacade
		{
			get
			{
				if(m_alertFacade == null)
					m_alertFacade = new BenQGuru.eMES.AlertModel.AlertFacade(DataProvider);

				return m_alertFacade;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);

			if(Session["ss_action"] != null && Session["ss_action"].ToString() == PageActionType.Update)
			{
				if(Session["ss_alerttype"] != null) 
				{
					this.drpAlertType.SelectedValue = Session["ss_alerttype"].ToString();
					drpAlertType_SelectedIndexChanged(null,null);
				}

				if(Session["ss_alertitem"] != null) 
				{
						this.drpAlertItem.SelectedValue = Session["ss_alertitem"].ToString();
						drpAlertItem_SelectedIndexChanged(null,null);
				}
				if(Session["ss_itemcode"] != null) this.stbItem.Text = Session["ss_itemcode"].ToString();
				if(Session["ss_auto"] != null) this.chkRefreshAuto.Checked = bool.Parse(Session["ss_auto"].ToString());
				if(Session["ss_mi"] != null) this.txtMi.Text = Session["ss_mi"].ToString();
				if(Session["ss_datefrom"] != null) this.dateFrom.Text = Session["ss_datefrom"].ToString();
				if(Session["ss_dateto"] != null) this.dateTo.Text = Session["ss_dateto"].ToString();
				if(Session["ss_status"] != null)
				{
					for(int i=0;i<this.chlAlertStatus.Items.Count;i++)
					{
						this.chlAlertStatus.Items[i].Selected = false;
					}
					string status = Session["ss_status"].ToString();
					string [] statusArr = status.Split(',');
					for(int i=0;i<statusArr.Length;i++)
					{
						if(statusArr[i] != null && statusArr[i] != string.Empty)
						{
							this.chlAlertStatus.Items.FindByValue(statusArr[i]).Selected = true;
						}
					}
				}

				this.gridHelper.RequestData();

				Session["ss_action"] = null;
			}
			//如果自动刷新,则执行查询
			else if(this.chkRefreshAuto.Checked)
			{
				this.gridHelper.RequestData();
			}
		}

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload (e);
			//_dataProvider.
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		

		#region WebGrid
		protected override void Grid_ClickCell(UltraGridCell cell)
		{
			base.Grid_ClickCell (cell);
			if(cell.Column.Key =="HandleLog")
			{
				SaveQuery();
				Response.Redirect(this.MakeRedirectUrl ("FHandleLogQP.aspx",new string[] {"alertid"},new string[] {cell.Row.Cells[1].Value.ToString()}));
			}
			if(this.gridHelper.IsClickEditColumn(cell))
			{
				SaveQuery();
				Response.Redirect(this.MakeRedirectUrl ("FAlertEP.aspx",new string[] {"alertid"},new string[] {cell.Row.Cells[1].Value.ToString()}));
			}
		}

		private void SaveQuery()
		{
			Session["ss_action"] = PageActionType.Update;
			Session["ss_alerttype"] = this.drpAlertType.SelectedValue;
			Session["ss_alertitem"] = this.drpAlertItem.SelectedValue;
			Session["ss_itemcode"] = this.stbItem.Text;
			Session["ss_auto"] = this.chkRefreshAuto.Checked.ToString();
			Session["ss_mi"] = this.txtMi.Text;
			Session["ss_datefrom"] = this.dateFrom.Text;
			Session["ss_dateto"] = this.dateTo.Text;

			int selectcount = 0;
			for(int i = 0;i<this.chlAlertStatus.Items.Count;i++)
			{
				if(this.chlAlertStatus.Items[i].Selected )
				{
					selectcount ++ ;
				}
			}

			string[] statusArr = new string[selectcount];
			int j = 0;
			for(int i = 0;i<this.chlAlertStatus.Items.Count;i++)
			{
				if(this.chlAlertStatus.Items[i].Selected )
				{
					statusArr[j] = chlAlertStatus.Items[i].Value;
					j ++;
				}
			}
			Session["ss_status"] = String.Join(",",statusArr);

		}
		protected override void Grid_DblClick(object sender, ClickEventArgs e)
		{
			Infragistics.WebUI.UltraWebGrid.UltraGridRow Row = e.Row;
			if(Row != null)
			{
				SaveQuery();
				Response.Redirect(this.MakeRedirectUrl ("FAlertEP.aspx",new string[] {"alertid"},new string[] {Row.Cells[1].Value.ToString()}));
			}
		}

		protected override void InitWebGrid()
		{
			this.gridHelper.AddColumn("AlertID",       "AlertID",   null);
			this.gridHelper.AddColumn( "AlertMsg",     "事件描述",	null);
			this.gridHelper.AddColumn( "SSCode",     "产线",	null);
			this.gridHelper.AddColumn( "ProductCode",     "产品",	null);
			this.gridHelper.AddColumn( "AlertStatus",  "预警状态",	null);
			this.gridHelper.AddColumn("AlertType",     "预警类别",  null);
			this.gridHelper.AddColumn("AlertItem",     "预警项次",  null);
			this.gridHelper.AddLinkColumn("HandleLog", "处理记录",  null);
			this.gridHelper.AddColumn("SendUser",      "发出人",    null);
			this.gridHelper.AddColumn("AlertDate",      "预警日期",    null);
			this.gridHelper.AddColumn("AlertTime",      "预警时间",    null);
			this.gridHelper.AddColumn("MaintainUser",      "修改人",    null);
			this.gridHelper.AddColumn("MaintainDate",      "修改日期",    null);
			this.gridHelper.AddColumn("MaintainTime",      "修改时间",    null);

			this.gridHelper.AddDefaultColumn( true, true );
            
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
			
			this.GetWebGrid().Columns.FromKey("AlertMsg").Width = System.Web.UI.WebControls.Unit.Parse("250px");
			this.GetWebGrid().Columns.FromKey("AlertID").Hidden = true;

			if(this.chkRefreshAuto.Checked || Request.QueryString["autosearch"] =="1")
			{
				this.gridHelper.RequestData();
			}
			else if(!Page.IsPostBack)
			{
				this.gridHelper.RequestData();//第一次查数据
			}
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			BenQGuru.eMES.Domain.Alert.Alert alert = obj as BenQGuru.eMES.Domain.Alert.Alert;
			if(alert != null)
			{
				Infragistics.WebUI.UltraWebGrid.UltraGridRow ur =  new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
																										new object[]{"false",
																										alert.AlertID,
																										alert.AlertMsg,
																										alert.SSCode,
																										alert.ProductCode,
																										_alertConst.GetName(alert.AlertStatus),
																										_alertConst.GetName(alert.AlertType),
																										_alertConst.GetName(alert.AlertItem),
																										"",
																										alert.SendUser,
																										FormatHelper.ToDateString(alert.AlertDate),
																										FormatHelper.ToTimeString(alert.AlertTime),
																										alert.MaintainUser,
																										FormatHelper.ToDateString(alert.MaintainDate),
																										FormatHelper.ToTimeString(alert.MaintainTime)
																										});
				ur.Cells[2].Style.ForeColor = ColorHelper.GetColor(alert.AlertLevel);
				ur.Cells[5].Style.ForeColor = ColorHelper.GetColor(alert.AlertStatus);
				return ur;
			}
				
			else
				return null;
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			this._alertFacade.DeleteAlertAndRelation(domainObjects);			
		}

		protected override object GetEditObject(UltraGridRow row)
		{
			return 	this._alertFacade.GetAlert(decimal.Parse(row.Cells[1].Value.ToString()));
		}

		#region 生成查询条件的值
		//取预警项值列表
		private string GetItems()
		{
			string items = string.Empty;
			string[] itemArr = stbItem.Text.Split(',');
			if(itemArr != null && itemArr.Length > 0)
			{
				items = FormatHelper.ProcessQueryValues(itemArr);
			}
			if(items == "''")
				items = string.Empty;

			return items;
		}

		//取状态列表
		private string GetStatus()
		{
			int selectcount = 0;
			for(int i = 0;i<this.chlAlertStatus.Items.Count;i++)
			{
				if(this.chlAlertStatus.Items[i].Selected )
				{
					selectcount ++ ;
				}
			}

			string[] statusArr = new string[selectcount];
			int j = 0;
			for(int i = 0;i<this.chlAlertStatus.Items.Count;i++)
			{
				if(this.chlAlertStatus.Items[i].Selected )
				{
					statusArr[j] = chlAlertStatus.Items[i].Value;
					j ++;
				}
			}

			return FormatHelper.ProcessQueryValues(statusArr);
		}

		//取产品代码列表
		private string GetProducts()
		{
			string products = string.Empty;
			string[] productArr = this.txtProduct.Text.Split(',');
			if(productArr != null && productArr.Length > 0)
			{
				products = FormatHelper.ProcessQueryValues(productArr);
			}
			if(products == "''")
				products = string.Empty;

			return products;
		}

		//取产线列表
		private string GetSSCodes()
		{
			string sscodes = string.Empty;
			string[] sscodeArr = this.txtSSCode.Text.Split(',');
			if(sscodeArr != null && sscodeArr.Length > 0)
			{
				sscodes = FormatHelper.ProcessQueryValues(sscodeArr);
			}
			if(sscodes == "''")
				sscodes = string.Empty;

			return sscodes;
		}

		#endregion

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			string items = string.Empty;
			string status = string.Empty;
			string products = string.Empty;
			string sscodes = string.Empty;

			items = this.GetItems();
			status = this.GetStatus();
			products = this.GetProducts();
			sscodes = this.GetSSCodes();

			if(this.chbExpDetails.Checked && this.hidAction.Value == "exp")
			{
				#region 导出处理记录
				return 	this._alertFacade.QueryAlert2Handle(this.drpAlertType.SelectedValue,
															this.drpAlertItem.SelectedValue,
															items,
															FormatHelper.TODateInt(this.dateFrom.Text),
															FormatHelper.TODateInt(this.dateTo.Text),
															status,
															products,
															sscodes,
															this.txtAlertMsg.Text.Trim()
															);
				#endregion
			}
			else
			{
				#region 不导出处理记录
				return 	this._alertFacade.QueryAlert(this.drpAlertType.SelectedValue,
													this.drpAlertItem.SelectedValue,
													items,
													FormatHelper.TODateInt(this.dateFrom.Text),
													FormatHelper.TODateInt(this.dateTo.Text),
													status,
													products,
													sscodes,
													this.txtAlertMsg.Text.Trim(),
													inclusive,exclusive);
				#endregion
			}
		}

		protected override int GetRowCount()
		{
			string items = string.Empty;
			string status = string.Empty;
			string products = string.Empty;
			string sscodes = string.Empty;

			items = this.GetItems();
			status = this.GetStatus();
			products = this.GetProducts();
			sscodes = this.GetSSCodes();

			return 	this._alertFacade.QueryAlertCount(this.drpAlertType.SelectedValue,
													this.drpAlertItem.SelectedValue,
													items,
													FormatHelper.TODateInt(this.dateFrom.Text),
													FormatHelper.TODateInt(this.dateTo.Text),
													status,
													products,
													sscodes,
													this.txtAlertMsg.Text.Trim(),
													0,int.MaxValue);
		}

		#endregion

        
		#region Export 

		protected override string[] FormatExportRecord( object obj )
		{
			if(this.chbExpDetails.Checked) //导出处理记录
			{
				BenQGuru.eMES.Domain.Alert.Alert2Handle alert = obj as BenQGuru.eMES.Domain.Alert.Alert2Handle;
				if(alert != null)
				{
				
					return new string[]{
										   alert.AlertMsg,
										   _alertConst.GetName(alert.HandlAlertStatus),
										   _alertConst.GetName(alert.AlertType),
										   _alertConst.GetName(alert.AlertItem),
										   alert.SendUser,
										   FormatHelper.ToDateString(alert.AlertDate),
										   FormatHelper.ToTimeString(alert.AlertTime),
										   alert.MaintainUser,
										   FormatHelper.ToDateString(alert.MaintainDate),
										   FormatHelper.ToTimeString(alert.MaintainTime),
											alert.HandleMsg,alert.HandleUser,alert.UserEmail,
										   _alertConst.GetName(alert.AlertLevel),
										   FormatHelper.ToDateString(alert.HandleDate),
										   FormatHelper.ToTimeString(alert.HandleTime)
									   };
				}
				else
					return null;	
			}
			else
			{
				BenQGuru.eMES.Domain.Alert.Alert alert = obj as BenQGuru.eMES.Domain.Alert.Alert;
				if(alert != null)
				{
				
					return new string[]{
										   alert.AlertMsg,
										   _alertConst.GetName(alert.AlertStatus),
										   _alertConst.GetName(alert.AlertType),
										   _alertConst.GetName(alert.AlertItem),
										   alert.SendUser,
										   FormatHelper.ToDateString(alert.AlertDate),
										   FormatHelper.ToTimeString(alert.AlertTime),
										   alert.MaintainUser,
										   FormatHelper.ToDateString(alert.MaintainDate),
										   FormatHelper.ToTimeString(alert.MaintainTime)
									   };
				}
				else
					return null;
			}
		}

		protected override string[] GetColumnHeaderText()
		{
			if(this.chbExpDetails.Checked)
			{
				return new string[] {
									"事件描述","预警状态","预警类别","预警项次","发出人","预警日期","预警时间","修改人","修改日期","修改时间",
									"处理记录","处理人","电子信箱","预警级别","处理日期","处理时间"
									};
			}
			else
			{
				return new string[] {"事件描述","预警状态","预警类别","预警项次","发出人","预警日期","预警时间","修改人","修改日期","修改时间"};
			}
		}
		#endregion

		protected void drpAlertType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			AlertItemBuilder.Build(this.drpAlertType.SelectedValue,this.drpAlertItem.Items,this._alertConst);

			this.drpAlertItem.Items.Insert(0,new ListItem(_alertConst.GetName("*"),"*"));
			
			drpAlertItem_SelectedIndexChanged(null,null);
		}

		protected void drpAlertItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.drpAlertItem.SelectedValue == "*")
			{
				this.stbItem.Enabled = false;
			}
			else
			{
				this.stbItem.Enabled = true;
			}

			AlertItemValueHelper.SetItemValue(this.stbItem,this.drpAlertType.SelectedValue,this.drpAlertItem.SelectedValue);
		}

	}
}
