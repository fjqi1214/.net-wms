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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.Alert
{
    public partial class FInvRCardQP : BaseMPage
    {
        private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		protected BenQGuru.eMES.BaseSetting.UserFacade _userfacade;

		string _type;
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
			if(this.Session["FInvRCardQP_Type"] != null)
				_type = this.Session["FInvRCardQP_Type"].ToString();	
			else
				_type = null;	

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		#endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
		    this.gridHelper.AddColumn( "RCard", "产品序列号",	null);
			this.gridHelper.AddColumn( "CartonNo", "Carton箱号",	null);
			this.gridHelper.AddColumn( "MOCode", "工单代码",	null);
			this.gridHelper.AddColumn( "MOPlanQty", "工单计划数量",	null);
			//this.gridHelper.AddColumn( "MO_Ship_ActQty", "工单已出货数量",	null);
			if(_type == "ship")
			{
				this.gridHelper.AddColumn( "MO_Ship_ActQty", "工单已出货数量",	null);
				this.gridHelper.AddColumn( "RecNo", "入库单号",	null);
				this.gridHelper.AddColumn( "ShipUser", "出货采集人员",	null);
				this.gridHelper.AddColumn( "ShipDate1", "出货采集日期",	null);
			}
			else
			{
				this.gridHelper.AddColumn( "MO_Rev_ActQty", "工单已入库数量",	null);
				this.gridHelper.AddColumn( "RCardStatus", "产品序列号状态",	null);
				this.gridHelper.AddColumn( "ReceiveUser", "入库采集人员",	null);
				this.gridHelper.AddColumn( "ReceiveDate", "入库采集日期",	null);
			}

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridHelper.RequestData();
        }
		
		
        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
			InvRCardForQuery inv = obj as InvRCardForQuery ;
			if(inv == null )
				return null;

			Infragistics.WebUI.UltraWebGrid.UltraGridRow ur = null;
			if(this._type == "ship")
				ur = new UltraGridRow(
									new object[]
												{
													inv.RunningCard,
													inv.CartonCode,
													inv.MOCode,
													inv.PlanQty,
													inv.ActQty,
													inv.RecNO,
													inv.ShipUser,
													FormatHelper.ToDateString(inv.ShipDate)
												}
									);
			else
				ur = new UltraGridRow(
									new object[]
												{
													inv.RunningCard,
													inv.CartonCode,
													inv.MOCode,
													inv.PlanQty,
													inv.ActQty,
													RCardStatus.GetName(inv.RCardStatus),
													inv.ReceiveUser,
													FormatHelper.ToDateString(inv.ReceiveDate)
												}
										);
				
			return ur;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			BenQGuru.eMES.Common.Domain.IDomainDataProvider provider = null;
			try
			{
				provider = base.DataProvider;
				BenQGuru.eMES.Material.InventoryFacade facade = new InventoryFacade(provider);
				string no = this.Session["FInvRCardQP_No"].ToString();
				string seq = this.Session["FInvRCardQP_Seq"].ToString();
				string status = this.Session["FInvRCardQP_Status"].ToString();
				string rcardfrom = this.Session["FInvRCardQP_From"].ToString();
				string rcardto =this.Session["FInvRCardQP_To"].ToString();
				string datefrom = this.Session["FInvRCardQP_DateFrom"].ToString();
				string dateto = this.Session["FInvRCardQP_DateTo"].ToString();
				
				if(_type == "ship")
					return facade.QueryInvShipRCardWeb(no,seq,datefrom,dateto,rcardfrom,rcardto,inclusive,exclusive);
				else
					return facade.QueryInvRCardWeb(no,seq,status,datefrom,dateto,rcardfrom,rcardto,inclusive,exclusive);
			}
			finally
			{
				//if(provider != null)
				//	((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)provider).PersistBroker.CloseConnection();
			}
        }

		protected override int GetRowCount()
		{
			BenQGuru.eMES.Common.Domain.IDomainDataProvider provider = null;
			try
			{
				provider = base.DataProvider;
				BenQGuru.eMES.Material.InventoryFacade facade = new InventoryFacade(provider);
				string no = this.Session["FInvRCardQP_No"].ToString();
				string seq = this.Session["FInvRCardQP_Seq"].ToString();
				string status = this.Session["FInvRCardQP_Status"].ToString();
				string rcardfrom = this.Session["FInvRCardQP_From"].ToString();
				string rcardto =this.Session["FInvRCardQP_To"].ToString();
				string datefrom = this.Session["FInvRCardQP_DateFrom"].ToString();
				string dateto = this.Session["FInvRCardQP_DateTo"].ToString();
				
				if(_type == "ship")
					return facade.QueryInvShipRCardWebCount(no,seq,datefrom,dateto,rcardfrom,rcardto);
				else
					return facade.QueryInvRCardWebCount(no,seq,status,datefrom,dateto,rcardfrom,rcardto);
			}
			finally
			{
				if(provider != null)
				{
					BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider p = provider as BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider;
					BenQGuru.eMES.Common.PersistBroker.OLEDBPersistBroker b = p.PersistBroker as BenQGuru.eMES.Common.PersistBroker.OLEDBPersistBroker;
					//b.CloseConnection();
					
				}
					//((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)provider).PersistBroker.CloseConnection();
			}
		}

        #endregion    

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			if(_type == "ship")
				Response.Redirect("FStockOutDetailQP.aspx");
			else
				Response.Redirect("FStockInDetailQP.aspx");
		}

      }

}