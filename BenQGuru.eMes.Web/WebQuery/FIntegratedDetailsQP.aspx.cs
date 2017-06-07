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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.DataCollect;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FIntegratedDetailsQP 的摘要说明。
	/// </summary>
	public partial class FIntegratedDetailsQP : BaseQPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelperNew _helper = null;
		//protected GridHelper gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//this.pagerSizeSelector.Readonly = true;

            this.txtPackedNoQuery.Text = this.GetRequestParam("PackedNo");
			

			gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);

			this._helper = new WebQueryHelperNew( null,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1,this.DtSource );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();

				this._helper.Query(sender);
				this.displayLable();
			}
		}
		private void displayLable()
		{
			string referedURL = this.GetRequestParam("RETURNPAGEURL") ;
			if( referedURL == "FIntegratedComponentLoadingQP.aspx")
			{
                this.lblTitle.Text = this.languageComponent1.GetString("$PageControl_DetialTitle");
			}
			else if(referedURL == "FIntegratedComponentDownQP.aspx")
			{
                this.lblTitle.Text = this.languageComponent1.GetString("$PageControl_Title");
			}

		}

		private void _initialWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn("MItemCode",				"料号",null);
			this.gridHelper.AddColumn("MOCode",			"工单",null);
			this.gridHelper.AddColumn("ItemCode",			"产品",null);
			this.gridHelper.AddColumn("RouteCode",			"途程",null);
			this.gridHelper.AddColumn("OPCode",				"工序",null);
			this.gridHelper.AddColumn("ResourceCode",			"资源",null);
			this.gridHelper.AddColumn("VendorCode",				"厂商代码",null);
			this.gridHelper.AddColumn("VendorItemCode",			"厂商料号",null);
			this.gridHelper.AddColumn("LotNO",			"批号",null);
			this.gridHelper.AddColumn("DateCode",			"生产日期",null);
			this.gridHelper.AddColumn("Version",				"版本号",null);
			this.gridHelper.AddColumn("PCBA",			"PCBA",null);
			this.gridHelper.AddColumn("BIOS",			"BIOS",null);			
			this.gridHelper.AddColumn("Qty",			"数量",null);
			this.gridHelper.AddColumn("MaintainUser",			"采集人员",null);
			this.gridHelper.AddColumn( "MaintainDate",		"维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime",		"维护时间",	null);	

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );	
		}

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

		private bool _checkRequireFields()
		{			
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck(this.lblPackedNoQuery,this.txtPackedNoQuery,System.Int32.MaxValue,true) );

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return false;
			}	
			return true;
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{	
			if( this._checkRequireFields() )
			{
				FacadeFactory facadeFactory = new FacadeFactory (base.DataProvider) ;
				( e as WebQueryEventArgsNew ).GridDataSource = 
					facadeFactory.CreateQueryComponentLoadingFacade().QueryLotDetails(						
					FormatHelper.CleanString(this.txtPackedNoQuery.Text).ToUpper(),
					( e as WebQueryEventArgsNew ).StartRow,
					( e as WebQueryEventArgsNew ).EndRow);

				( e as WebQueryEventArgsNew ).RowCount = 
					facadeFactory.CreateQueryComponentLoadingFacade().QueryLotDetailsCount(						
					FormatHelper.CleanString(this.txtPackedNoQuery.Text).ToUpper());
			}
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgsNew ).DomainObject != null )
			{
                OnWIPItem obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as OnWIPItem;
                DataRow row = DtSource.NewRow();
                row["MItemCode"] = obj.MItemCode;
                row["MOCode"] = obj.MOCode;
                row["ItemCode"] = obj.ItemCode;
                row["RouteCode"] = obj.RouteCode;
                row["OPCode"] = obj.OPCode;
                row["ResourceCode"] = obj.ResourceCode;
                row["VendorCode"] = obj.VendorCode;
                row["VendorItemCode"] = obj.VendorItemCode;
                row["LotNO"] = obj.LotNO;
                row["DateCode"] = obj.DateCode;
                row["Version"] = obj.Version;
                row["PCBA"] = obj.PCBA;
                row["BIOS"] = obj.BIOS;
                row["Qty"] = obj.Qty;
                row["MaintainUser"] = obj.MaintainUser;
                row["MaintainDate"] = FormatHelper.ToDateString(obj.MaintainDate);
                row["MaintainTime"] = FormatHelper.ToTimeString(obj.MaintainTime);
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgsNew ).DomainObject != null )
			{
                OnWIPItem obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as OnWIPItem;
				( e as DomainObjectToExportRowEventArgsNew ).ExportRow = 
					new string[]{
									obj.MItemCode,
									obj.MOCode,
									obj.ItemCode,
									obj.RouteCode,
									obj.OPCode,
									obj.ResourceCode,
									obj.VendorCode,
									obj.VendorItemCode,
									obj.LotNO,
									obj.DateCode,
									obj.Version,
									obj.PCBA,
									obj.BIOS,
									obj.Qty.ToString(),
									obj.MaintainUser,
									FormatHelper.ToDateString(obj.MaintainDate),
									FormatHelper.ToTimeString(obj.MaintainTime)
								};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgsNew ).Heads = 
				new string[]{
								"MItemCode",
								"MOCode",
								"ItemCode",
								"RouteCode",
								"OPCode",
								"ResourceCode",
								"VendorCode",
								"VendorItemCode",
								"LotNO",
								"DateCode",
								"Version",
								"PCBA",
								"BIOS",
								"Qty",
								"MaintainUser",
								"MaintainDate",
								"MaintainTime"
							};
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			string referedURL = this.GetRequestParam("RETURNPAGEURL") ;
			if( referedURL == string.Empty)
			{
				referedURL = "FIntegratedComponentLoadingQP.aspx" ;
			}
			else
			{
				referedURL = System.Web.HttpUtility.UrlDecode(referedURL) ;
			}
			Response.Redirect( referedURL ) ;
		}		
	}
}
