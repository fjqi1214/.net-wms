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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FINNOInfoQP 的摘要说明。
	/// </summary>
	public partial class FINNOInfoQP : BaseQPageNew
	{

		protected WebQueryHelperNew _helper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected GridHelperNew _gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{	
			//this.pagerSizeSelector.Readonly = true;
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
			this.txtINNOQuery.Text = this.GetRequestParam( "INNO" );
			this.txtINNO.Text = this.GetRequestParam("MCARD");
			this.txtOPCode.Text = this.GetRequestParam("OPCode");

			this._gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);

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
			}
		}

		private void _initialWebGrid()
		{
            base.InitWebGrid();
			this._gridHelper.AddColumn("MOCode","工单",null);
			this._gridHelper.AddColumn("RouteCode","生产途程",null);
			this._gridHelper.AddColumn("OPCode","工序",null);

            this._gridHelper.AddColumn("IT_PACKEDNO", "最小包装条码", null);
			this._gridHelper.AddColumn("ActionType","交易类型",null);
			this._gridHelper.AddColumn("InsideItemCode","料号",null);
			this._gridHelper.AddColumn("MItemName","物料名称",null);
			this._gridHelper.AddColumn("VendorCode","厂商",null);
			this._gridHelper.AddColumn("VendorItemCode","厂商料号",null);
			this._gridHelper.AddColumn("LotNo",			"生产批号",null);
			this._gridHelper.AddColumn("DateCode",		"生产日期",null);
			this._gridHelper.AddColumn("Version",		"物料版本",null);
			this._gridHelper.AddColumn("PCBAVersion",	"PCBA版本",null);
			this._gridHelper.AddColumn("BIOSVersion",	"BIOS版本",null);

			this._gridHelper.AddColumn( "IT_MaintainUser1", "采集人",	null);
			this._gridHelper.AddColumn( "IT_MaintainDate1", "采集日期",	null);
			this._gridHelper.AddColumn( "IT_MaintainTime1", "采集时间",	null);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );
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

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
			FacadeFactory facadeFactory = new FacadeFactory (base.DataProvider) ;
			( e as WebQueryEventArgsNew ).GridDataSource = 
				facadeFactory.CreateQueryFacade1().QueryINNOInfo2(
				FormatHelper.CleanString(this.txtINNOQuery.Text),
				this.GetRequestParam("InsideItemCode"),
				this.GetRequestParam("VendorCode"),
				this.GetRequestParam("VendorItemCode"),
				this.GetRequestParam("LotNo"),
				this.GetRequestParam("DateCode"),
				FormatHelper.CleanString(this.txtINNO.Text),
				FormatHelper.CleanString(this.txtOPCode.Text),
				( e as WebQueryEventArgsNew ).StartRow,
				( e as WebQueryEventArgsNew ).EndRow);

			( e as WebQueryEventArgsNew ).RowCount = 
				facadeFactory.CreateQueryFacade1().QueryINNOInfoCount2(
				FormatHelper.CleanString(this.txtINNOQuery.Text),
				this.GetRequestParam("InsideItemCode"),
				this.GetRequestParam("VendorCode"),
				this.GetRequestParam("VendorItemCode"),
				this.GetRequestParam("LotNo"),
				this.GetRequestParam("DateCode"),
				FormatHelper.CleanString(this.txtINNO.Text),
				FormatHelper.CleanString(this.txtOPCode.Text));
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgsNew ).DomainObject != null )
			{
				TracedMinno obj = ( e as DomainObjectToGridRowEventArgsNew ).DomainObject as TracedMinno;

                DataRow row = this.DtSource.NewRow();
                row["MOCode"] = obj.MOCode;
                row["RouteCode"] = obj.RouteCode;
                row["OPCode"] = obj.OPCode;
                row["IT_PACKEDNO"] = obj.INNO;
                row["ActionType"] = (obj.ActionType == 0) ? "上料" : "下料";
                row["InsideItemCode"] = obj.MItemCode;
                row["MItemName"] = obj.MItemName;
                row["VendorCode"] = obj.VendorCode;
                row["VendorItemCode"] = obj.VendorItemCode;
                row["LotNo"] = obj.LotNO;
                row["DateCode"] = obj.DateCode;
                row["Version"] = obj.Version;
                row["PCBAVersion"] = obj.PCBA;
                row["BIOSVersion"] = obj.BIOS;
                row["IT_MaintainUser1"] = obj.MaintainUser;
                row["IT_MaintainDate1"] = FormatHelper.ToDateString(obj.MaintainDate);
                row["IT_MaintainTime1"] = FormatHelper.ToTimeString(obj.MaintainTime);


                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                    //new UltraGridRow( new object[]{
                    //                                  obj.MOCode,
                    //                                  obj.RouteCode,
                    //                                  obj.OPCode,

                    //                                  obj.INNO,
                    //                                  (obj.ActionType == 0)? "上料":"下料",
                    //                                  obj.MItemCode,
                    //                                  obj.MItemName,
                    //                                  obj.VendorCode,
                    //                                  obj.VendorItemCode,
                    //                                  obj.LotNO,
                    //                                  obj.DateCode,
                    //                                  obj.Version,
                    //                                  obj.PCBA,
                    //                                  obj.BIOS,

                    //                                  obj.MaintainUser,
                    //                                  FormatHelper.ToDateString(obj.MaintainDate),
                    //                                  FormatHelper.ToTimeString(obj.MaintainTime)
                    //                              }
                    //);
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgsNew ).DomainObject != null )
			{
				TracedMinno obj = ( e as DomainObjectToExportRowEventArgsNew ).DomainObject as TracedMinno;
				( e as DomainObjectToExportRowEventArgsNew ).ExportRow = 
					new string[]{
									obj.MOCode,
									obj.RouteCode,
									obj.OPCode,

									obj.INNO,
									"上料",
									obj.MItemCode,
									obj.MItemName,
									obj.VendorCode,
									obj.VendorItemCode,
									obj.LotNO,
									obj.DateCode,
									obj.Version,
									obj.PCBA,
									obj.BIOS,

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
								"MOCode",
								"RouteCode",
								"OPCode",

								"IT_PACKEDNO",
								"ActionType",
								"InsideItemCode",
								"MItemName",
								"VendorCode",
								"VendorItemCode",
								"LOTNO",
								"DateCode",
								"Version",
								"PCBAVersion",
								"BIOSVersion",

								"IT_MaintainUser1",
								"IT_MaintainDate1",
								"IT_MaintainTime1"
							};
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			//this.Response.Redirect("./FComponentLoadingTrackingQP.aspx");

            //add by Jarvis
            string referedURL = this.GetRequestParam("REFEREDURL");
            if (referedURL == string.Empty)
            {
                referedURL = "FComponentLoadingTrackingQP.aspx";
            }
            else
            {
                referedURL = System.Web.HttpUtility.UrlDecode(referedURL);
            }
            Response.Redirect(referedURL);
		}
	}
}
