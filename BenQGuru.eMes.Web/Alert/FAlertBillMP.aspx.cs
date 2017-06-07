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
	public partial class FAlertBillMP : BaseMPage
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdSave;
		protected System.Web.UI.WebControls.Label lblItemCodeQuery;
		protected System.Web.UI.WebControls.Label lblItemTypeQuery;
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdCancel;

		protected System.Web.UI.WebControls.Label lblItemNameQuery;

		private BenQGuru.eMES.AlertModel.AlertBillFacade m_alertBillFacade;
		private AlertConst _alertConst;
		/// <summary>
		/// 预警类别是否是资源不良数
		/// </summary>
		private bool isResourceNG = false;	

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

			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.stbItem.Enabled = !(this.drpAlertItem.SelectedValue == "*");
			}
			if(this.drpAlertType.SelectedValue == AlertType_Old.ResourceNG)
			{
				isResourceNG = true;
			}
		}

		private void LoadAlertType()
		{
			AlertTypeBuilder.Buid(this.drpAlertType.Items,this._alertConst);
			this.drpAlertType.Items.Insert(0,new ListItem(_alertConst.GetName("*"),"*"));
			this.drpAlertType_SelectedIndexChanged(null,null);
		}

		public BenQGuru.eMES.AlertModel.AlertBillFacade _alertBillFacade
		{
			get
			{
				if(m_alertBillFacade == null)
					m_alertBillFacade = new BenQGuru.eMES.AlertModel.AlertBillFacade(DataProvider);

				return m_alertBillFacade;
			}
		}

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload (e);
			//_dataProvider.
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			if(!Page.IsPostBack)
				LoadAlertType();

			if(Session["ss_action"] != null
				&&
				Session["ss_action"].ToString() == PageActionType.Add)
			{
				this.gridHelper.RequestData();
			}
			else if(Session["ss_action"] != null
				&&
				Session["ss_action"].ToString() == PageActionType.Update)
			{
				if(SessionHelper.Current(this.Session).LoadStoredObject("ss_alerttype") != null)
				{
					this.drpAlertType.SelectedValue =SessionHelper.Current(this.Session).LoadStoredObject("ss_alerttype").ToString();
					this.drpAlertType_SelectedIndexChanged(null,null);
				}

				if(SessionHelper.Current(this.Session).LoadStoredObject("ss_alertitem") != null)
				{
					this.drpAlertItem.SelectedValue = SessionHelper.Current(this.Session).LoadStoredObject("ss_alertitem").ToString();
					this.drpAlertItem_SelectedIndexChanged(null,null);
				}

				if(SessionHelper.Current(this.Session).LoadStoredObject("ss_itemcode") != null)
					this.stbItem.Text = SessionHelper.Current(this.Session).LoadStoredObject("ss_itemcode").ToString();

				this.gridHelper.RequestData();
			}

			//
			Session["ss_action"] = string.Empty;
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		

		#region WebGrid
		protected override void Grid_ClickCell(UltraGridCell cell)
		{

			base.Grid_ClickCell (cell);
			if(isResourceNG)
			{
				string billid = cell.Row.Cells[1].Text.Trim();
				string itemcode = cell.Row.Cells.FromKey("AlertItemValue").Text.Trim();
				string alerttype = AlertType_Old.ResourceNG;
				string alertitem = AlertMsg.GetAlertCode(cell.Row.Cells.FromKey("AlertItem").Text.Trim(),this.languageComponent1);
				string rescode = (cell.Row.Cells.FromKey("AlertRes") !=null) ? cell.Row.Cells.FromKey("AlertRes").Text.Trim() : string.Empty;
				string ecg2ec = (cell.Row.Cells.FromKey("ECG2EC") !=null) ? cell.Row.Cells.FromKey("ECG2EC").Text.Trim() : string.Empty;
				if(cell.Column.Key =="Notifier")
				{
					SaveQuery();
					Response.Redirect(this.MakeRedirectUrl ("FNotifierQP.aspx",new string[] {"billid","itemcode","alerttype","alertitem","rescode","ecg2ec"},new string[] {billid,itemcode,alerttype,alertitem,rescode,ecg2ec}));
				}
				if(this.gridHelper.IsClickEditColumn(cell))
				{
					SaveQuery();
					Response.Redirect(this.MakeRedirectUrl ("FAlertBillEP.aspx",new string[] {"billid","itemcode","alerttype","alertitem","rescode","ecg2ec"},new string[] {billid,itemcode,alerttype,alertitem,rescode,ecg2ec}));
				}
			}
			else
			{
				if(cell.Column.Key =="Notifier")
				{
					SaveQuery();
					Response.Redirect(this.MakeRedirectUrl ("FNotifierQP.aspx",new string[] {"billid","alerttype"},new string[] {cell.Row.Cells[1].Text.Trim(),_alertConst.GetCode(cell.Row.Cells.FromKey("AlertType").Text.Trim())}));
				}
				if(this.gridHelper.IsClickEditColumn(cell))
				{
					SaveQuery();
					Response.Redirect(this.MakeRedirectUrl ("FAlertBillEP.aspx",new string[] {"billid","alerttype"},new string[] {cell.Row.Cells[1].Text.Trim(),_alertConst.GetCode(cell.Row.Cells.FromKey("AlertType").Text.Trim())}));
				}
			}
		}

		private void SaveQuery()
		{
			Session["ss_action"] = PageActionType.Update;
			SessionHelper.Current(this.Session).AddStoredObject("ss_itemcode",this.stbItem.Text,true);
			SessionHelper.Current(this.Session).AddStoredObject("ss_alerttype",this.drpAlertType.SelectedValue,true);
			SessionHelper.Current(this.Session).AddStoredObject("ss_alertitem",this.drpAlertItem.SelectedValue,true);
		}
		protected override void Grid_DblClick(object sender, ClickEventArgs e)
		{
			Infragistics.WebUI.UltraWebGrid.UltraGridRow Row = e.Row;
			if(Row != null)
			{
				SaveQuery();
				Response.Redirect(this.MakeRedirectUrl ("FAlertBillEP.aspx",new string[] {"billid","alerttype"},new string[] {Row.Cells[1].Text.Trim(),this._alertConst.GetCode(Row.Cells.FromKey("AlertType").Text.Trim())}));
			}
		}

		protected override void InitWebGrid()
		{
			this.gridWebGrid.Columns.Clear();
			this.gridHelper.AddColumn("BillId","BillId",null);
			this.gridHelper.AddColumn( "AlertItemValue", "预警项值",	null);
			this.gridHelper.AddColumn( "AlertType", "预警类别",	null);
			this.gridHelper.AddColumn( "AlertItem", "预警项次",	null);
			this.gridHelper.AddColumn( "ItemCode", "产品代码",	null);
			if(isResourceNG)
			{
				this.gridHelper.AddColumn( "AlertRes", "预警资源",	null);
				this.gridHelper.AddColumn( "ECG2EC", "不良代码组：不良代码",	null);
			}
			this.gridHelper.AddColumn( "StartNum", "起点数",	null);
			this.gridHelper.AddColumn( "AlertCondition", "预警条件",	null);
			this.gridHelper.AddColumn( "ValidDate", "生效日期",	null);
			this.gridHelper.AddColumn( "MailNotified", "是否邮件通知",	null);
			this.gridHelper.AddLinkColumn( "Notifier", "接收人",	null);
			this.gridHelper.AddDefaultColumn( true, true );
            
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridWebGrid.Columns[1].Hidden = true; //将Bill ID字段隐藏
			//this.gridHelper.RequestData();
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
				Infragistics.WebUI.UltraWebGrid.UltraGridRow ur;
				if(isResourceNG)
				{
					BenQGuru.eMES.Domain.Alert.AlertResBill alert = obj as BenQGuru.eMES.Domain.Alert.AlertResBill;
					if(alert == null ){return null;}
					ur =  new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
						new object[]{"false",
										alert.BillId.ToString(),
										alert.ItemCode,
										AlertMsg.GetAlertName(alert.AlertType,this.languageComponent1),
										AlertMsg.GetAlertName(alert.AlertItem,this.languageComponent1),
										alert.ResourceCode,
										alert.ErrorGroup2Code,
										alert.StartNum.ToString(),
										GetCondition(alert),
										FormatHelper.ToDateString(alert.ValidDate),
										alert.MailNotify=="Y"?"是":"否",
										""
									});
				}
				else
				{
					BenQGuru.eMES.Domain.Alert.AlertBill alert = obj as BenQGuru.eMES.Domain.Alert.AlertBill;
					if(alert == null ){return null;}
					ur =  new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
						new object[]{"false",
										alert.BillId.ToString(),
										alert.ItemCode,
										AlertMsg.GetAlertName(alert.AlertType,this.languageComponent1),
										AlertMsg.GetAlertName(alert.AlertItem,this.languageComponent1),
										alert.ProductCode,
										alert.StartNum.ToString(),
										GetCondition(alert),
										FormatHelper.ToDateString(alert.ValidDate),
										alert.MailNotify=="Y"?"是":"否",
										""
									});
				}

				return ur;
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			try
			{
				this.DataProvider.BeginTransaction();

				for(int i=0;i<domainObjects.Count;i++)
				{
					AlertBill alert = domainObjects[i] as AlertBill;
					if(alert != null)
					{
						object[] objs = _alertBillFacade.QueryAlertNotifier(alert.BillId);
						if(objs != null)
						{
							foreach(object obj in objs)
							{
								AlertNotifier an = obj as AlertNotifier;
								if(an != null)
									_alertBillFacade.DeleteAlertNotifier(an);
							}
						}
					}
					if(alert.AlertType == AlertType_Old.ResourceNG)
					{
						_alertBillFacade.DeleteAlertResBill((AlertResBill)alert);
					}
					else
					{
						_alertBillFacade.DeleteAlertBill(alert);
					}
				}
				
				this.DataProvider.CommitTransaction();
			}
			catch(System.Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				throw ex;
			}
		}

		protected override object GetEditObject(UltraGridRow row)
		{
			if(isResourceNG)
			{
				return  this._alertBillFacade.GetAlertResBill(int.Parse(row.Cells.FromKey("BillId").Value.ToString()));
			}
			else
				return  this._alertBillFacade.GetAlertBill(int.Parse(row.Cells.FromKey("BillId").Value.ToString()));
		}

		private string GetCondition(BenQGuru.eMES.Domain.Alert.AlertBill alert)
		{
			if(alert.Operator==Operator_Old.BW)
				return string.Format(_alertConst.GetName(Operator_Old.BW),NumberHelper.TrimZero(alert.LowValue),NumberHelper.TrimZero(alert.UpValue));
			else 
				return _alertConst.GetName(alert.Operator) + NumberHelper.TrimZero(alert.LowValue);
		}
		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
//			this.InitWebGrid();
			if(isResourceNG)
			{
				return _alertBillFacade.QueryAlertResBill2(this.stbItem.Text.ToUpper(),this.drpAlertType.SelectedValue,this.drpAlertItem.SelectedValue,string.Empty,string.Empty,inclusive,exclusive);
			}
			return _alertBillFacade.QueryAlertBill2(this.stbItem.Text.ToUpper(),this.drpAlertType.SelectedValue,this.drpAlertItem.SelectedValue,inclusive,exclusive);
		}


		protected override int GetRowCount()
		{
			string alerttype = string.Empty;
			if(this.drpAlertType.SelectedValue != "*")
				alerttype = this.drpAlertType.SelectedValue;

			string alertItem = string.Empty;
			if(this.drpAlertItem.SelectedValue != "*")
				alertItem = this.drpAlertItem.SelectedValue;

			if(isResourceNG)
			{
				return _alertBillFacade.QueryAlertResBillCount(this.stbItem.Text,alerttype,alertItem,string.Empty,string.Empty);
			}
			return _alertBillFacade.QueryAlertBillCount(this.stbItem.Text,alerttype,alertItem);
		}

		#endregion

        
		#region Export 

		protected override string[] FormatExportRecord( object obj )
		{
			BenQGuru.eMES.Domain.Alert.AlertBill alert = obj as BenQGuru.eMES.Domain.Alert.AlertBill;
			if(alert != null)
			{
				
				return new string[]{
									   alert.ItemCode,
									   AlertMsg.GetAlertName(alert.AlertType,this.languageComponent1),
									   alert.StartNum.ToString(),
									   GetCondition(alert),
									   FormatHelper.ToDateString(alert.ValidDate),
									   alert.MailNotify=="Y"?"是":"否"
								   };
			}
			else
				return null;
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {this.gridWebGrid.Columns[1].HeaderText,"预警类别","起点数","预警条件","生效日期","是否邮件通知",};
		}
		#endregion

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
			Session["ss_action"] = PageActionType.Add;
			this.Response.Redirect("FAlertBillAP.aspx");
		}

		protected void drpAlertType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			AlertItemBuilder.Build(this.drpAlertType.SelectedValue,this.drpAlertItem.Items,this._alertConst);

			this.drpAlertItem.Items.Insert(0,new ListItem(_alertConst.GetName("*"),"*"));
			
			drpAlertItem_SelectedIndexChanged(null,null);
		}

		protected void drpAlertItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			AlertItemValueHelper.SetItemValue(this.stbItem,this.drpAlertType.SelectedValue,this.drpAlertItem.SelectedValue);
			if(this.drpAlertItem.SelectedValue == "*")
			{
				this.stbItem.Enabled = false;
				gridWebGrid.Bands[0].Columns.FromKey("AlertItemValue").HeaderText = lblAlertItemCodeQuery.Text;
			}
			else
			{
				gridWebGrid.Bands[0].Columns.FromKey("AlertItemValue").HeaderText = drpAlertItem.SelectedItem.Text;
				this.stbItem.Enabled = true;
			}
		}

	}
}
