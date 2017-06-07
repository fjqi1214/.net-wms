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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FOnWipDistributingQP 的摘要说明。
	/// </summary>
	public partial class FOnWipDistributingQP : BaseQPageNew
	{

		protected WebQueryHelperNew _helper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		//protected GridHelper gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//this.pagerSizeSelector.Readonly = true;

			this.ViewState["Status"] = this.GetRequestParam("Status");
			this.txtItemCodeQuery.Text = this.GetRequestParam("ItemCode");
			this.txtMoCodeQuery.Text = this.GetRequestParam("MoCode");
			this.txtOperationCodeQuery.Text = this.GetRequestParam("OperationCode");
			this.txtResourceCodeQuery.Text = this.GetRequestParam("ResourceCode");
			this.ViewState["StartDate"] = this.GetRequestParam("StartDate"); 
			this.ViewState["EndDate"] = this.GetRequestParam("EndDate"); 
			this.ViewState["MoCodes"] = this.GetRequestParam("MoCodes");
			this.ViewState["ShiftDay"] = this.GetRequestParam("ShiftDay");
			this.ViewState["ShiftCode"] = this.GetRequestParam("ShiftCode");
			this.ViewState["IsFQC"] = this.GetRequestParam("IsFQC");

			this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);

			this._helper = new WebQueryHelperNew( null,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1,DtSource);
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);
			//this._helper.GridCellClick +=new EventHandler(_helper_GridCellClick);

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();

				this._helper.Query( sender );
			}
		}

		private void _initialWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddLinkColumn("WipHistroy","生产过程",null);
			this.gridHelper.AddColumn( "RunningCard","序列号",null);
			this.gridHelper.AddColumn( "ProductStatus","产品状态",null);
			this.gridHelper.AddColumn( "OperationResult","工序结果",null);
			this.gridHelper.AddColumn( "IDMergeRule","转换比例",null);
			this.gridHelper.AddColumn( "MaintainDate","日期",	null);
			this.gridHelper.AddColumn( "MaintainTime","时间",	null);
			this.gridHelper.AddColumn( "Operator","操作工",	null);
			this.gridHelper.AddColumn( "translateCard","转化前序列号",	null);

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridWebGrid.Columns.FromKey("translateCard").Hidden = true;
            ((BoundDataField)this.gridWebGrid.Columns.FromKey("OperationResult")).HtmlEncode = false;
			//this.gridWebGrid.Bands[0].Columns.FromKey("WipHistroy").Width = new Unit(60);
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
			FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
			( e as WebQueryEventArgsNew ).GridDataSource = 
				facadeFactory.CreateQueryFacade3().QueryOnWipInfoDistributing(
				this.ViewState["Status"].ToString(),
				this.txtItemCodeQuery.Text,
				this.ViewState["ShiftCode"].ToString(),
				this.txtOperationCodeQuery.Text,
				this.txtResourceCodeQuery.Text,
				this.txtMoCodeQuery.Text,
				FormatHelper.TODateInt( this.ViewState["ShiftDay"].ToString() ),
				FormatHelper.TODateInt( this.ViewState["ShiftDay"].ToString() ),
				( e as WebQueryEventArgsNew ).StartRow,
				( e as WebQueryEventArgsNew ).EndRow);

			( e as WebQueryEventArgsNew ).RowCount = 
				facadeFactory.CreateQueryFacade3().QueryOnWipInfoDistributingCount(
				this.ViewState["Status"].ToString(),
				this.txtItemCodeQuery.Text,
				this.ViewState["ShiftCode"].ToString(),
				this.txtOperationCodeQuery.Text,
				this.txtResourceCodeQuery.Text,
				this.txtMoCodeQuery.Text,
				FormatHelper.TODateInt( this.ViewState["ShiftDay"].ToString() ),
				FormatHelper.TODateInt( this.ViewState["ShiftDay"].ToString() ));
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                OnWipInfoDistributing obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as OnWipInfoDistributing;
                if (string.Compare(this.ViewState["Status"].ToString(), "NG", true) == 0)
                {
                    obj.ProductStatus = TSStatus.TSStatus_New;
                }
                DataRow row = DtSource.NewRow();
                row["WipHistroy"] = "";
                row["RunningCard"] = obj.RunningCard;
                row["ProductStatus"] = this.languageComponent1.GetString(obj.ProductStatus);
                row["OperationResult"] = WebQueryHelper.GetOPResultLinkHtml2(this.languageComponent1,
                                                                                            obj.Action,
                                                                                            obj.RunningCard,
                                                                                            obj.RunningCardSequence,
                                                                                            this.txtMoCodeQuery.Text,
                                                                                            this.Request.Url.PathAndQuery);
                row["IDMergeRule"] = obj.IDMergeRule;
                row["MaintainDate"] = FormatHelper.ToDateString(obj.MaintainDate);
                row["MaintainTime"] = FormatHelper.ToTimeString(obj.MaintainTime);
                row["Operator"] = obj.MaintainUser;
                row["translateCard"] = obj.TranslateCard;
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
            }
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgsNew ).DomainObject != null )
			{
				if( string.Compare(this.txtOperationCodeQuery.Text,"TS",true)!=0 )
				{
		
					OnWipInfoDistributing obj = ( e as DomainObjectToExportRowEventArgsNew ).DomainObject as OnWipInfoDistributing;
					( e as DomainObjectToExportRowEventArgsNew ).ExportRow = 
						new string[]{
										this.txtItemCodeQuery.Text,
										this.txtOperationCodeQuery.Text,
										this.txtResourceCodeQuery.Text,
										this.txtMoCodeQuery.Text,
										obj.RunningCard,
										this.languageComponent1.GetString(obj.ProductStatus),
										WebQueryHelper.GetOPResult(this.languageComponent1, obj.Action),
										FormatHelper.ToDateString(obj.MaintainDate),
										FormatHelper.ToTimeString(obj.MaintainTime),
										obj.MaintainUser
									};
				}
				else
				{
					OnWipInfoDistributing obj = ( e as DomainObjectToExportRowEventArgsNew ).DomainObject as OnWipInfoDistributing;
					( e as DomainObjectToExportRowEventArgsNew ).ExportRow = 
						new string[]{
										this.txtItemCodeQuery.Text,
										this.txtOperationCodeQuery.Text,
										this.txtResourceCodeQuery.Text,
										this.txtMoCodeQuery.Text,
										obj.RunningCard,
										this.languageComponent1.GetString(obj.ProductStatus),
										this.languageComponent1.GetString("ItemTracing_ts"),
										FormatHelper.ToDateString(obj.MaintainDate),
										FormatHelper.ToTimeString(obj.MaintainTime),
										obj.MaintainUser
									};
				}
			}
			
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgsNew ).Heads = 
				new string[]{
								"ItemCode",
								"OperationCode",
								"ResourceCode",
								"MOCode",
								"RunningCard",
								"ProductStatus",
								"OperationResult",
								"MaintainDate",
								"MaintainTime",
								"MaintainUser"
							};
		}

        protected override void Grid_ClickCell(GridRecord row, string command)
        {

            if (command == "WipHistroy")
			{			
				this.Response.Redirect( 
					this.MakeRedirectUrl(
					"FITProductionProcessQP.aspx",
					new string[]{
									"RCARD",
									"MOCODE",
									"TCARD"
								},
					new string[]{
									row.Items.FindItemByKey("RunningCard").Text,
									this.txtMoCodeQuery.Text,
									row.Items.FindItemByKey("translateCard").Text
								})
					);
			}
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect( 
				this.MakeRedirectUrl(
				"FOnWipResourceQP.aspx",
				new string[]{"ItemCode","MoCode","OperationCode","STARTDATE","ENDDATE","IsFQC"},
				new string[]{
								this.txtItemCodeQuery.Text,
								this.ViewState["MoCodes"].ToString(),
								this.txtOperationCodeQuery.Text,
								this.ViewState["StartDate"].ToString(),
								this.ViewState["EndDate"].ToString(),
								this.ViewState["IsFQC"].ToString()
			})
				);
		}
	}
}
