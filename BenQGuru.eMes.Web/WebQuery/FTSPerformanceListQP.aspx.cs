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
	/// FTSPerformanceListQP 的摘要说明。
	/// </summary>
	public partial class FTSPerformanceListQP : BaseQPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelperNew _helper = null;

		#region View State
		private string ModelCode
		{
			get
			{
				if( this.ViewState["ModelCode"] != null )
				{
					return this.ViewState["ModelCode"].ToString();
				}
				return "";
			}
			set
			{
				this.ViewState["ModelCode"] = value;
			}
		}

		private string ItemCode
		{
			get
			{
				if( this.ViewState["ItemCode"] != null )
				{
					return this.ViewState["ItemCode"].ToString();
				}
				return "";
			}
			set
			{
				this.ViewState["ItemCode"] = value;
			}
		}

		private string MoCode
		{
			get
			{
				if( this.ViewState["MoCode"] != null )
				{
					return this.ViewState["MoCode"].ToString();
				}
				return "";
			}
			set
			{
				this.ViewState["MoCode"] = value;
			}
		}

		private string TsResource
		{
			get
			{
				if( this.ViewState["TsResource"] != null )
				{
					return this.ViewState["TsResource"].ToString();
				}
				return "";
			}
			set
			{
				this.ViewState["TsResource"] = value;
			}
		}

		private string TsOperator
		{
			get
			{
				if( this.ViewState["TsOperator"] != null )
				{
					return this.ViewState["TsOperator"].ToString();
				}
				return "";
			}
			set
			{
				this.ViewState["TsOperator"] = value;
			}
		}

		private int StartDate
		{
			get
			{
				if( this.ViewState["StartDate"] != null )
				{
					try
					{
						return System.Int32.Parse(this.ViewState["StartDate"].ToString());
					}
					catch
					{
						return 0;
					}
				}
				return 0;
			}
			set
			{
				this.ViewState["StartDate"] = value;
			}
		}

		private int EndDate
		{
			get
			{
				if( this.ViewState["EndDate"] != null )
				{
					try
					{
						return System.Int32.Parse(this.ViewState["EndDate"].ToString());
					}
					catch
					{
						return 0;
					}
				}
				return 0;
			}
			set
			{
				this.ViewState["EndDate"] = value;
			}
		}
		#endregion
	
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			this._initialParamter();

			this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);

			this._helper = new WebQueryHelperNew( null,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1,DtSource );
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
				this._helper.Query(sender);
			}
		}

		private void _initialParamter()
		{
			this.ModelCode = this.GetRequestParam("12_ModelCode");
			this.ItemCode = this.GetRequestParam("12_ItemCode");
			this.MoCode = this.GetRequestParam("12_MoCode");
			this.TsResource = this.GetRequestParam("12_TSResourceCode");
			this.TsOperator = this.GetRequestParam("12_TSOperator");
			this.txtOperatorQuery.Text = this.TsOperator;
			if( this.GetRequestParam("12_StartDate") != null )
			{
				try
				{
					this.StartDate = FormatHelper.TODateInt( this.GetRequestParam("12_StartDate") );
				}
				catch
				{
					this.StartDate = 0;
				}
			}
			if( this.GetRequestParam("12_EndDate") != null )
			{
				try
				{
					this.EndDate = FormatHelper.TODateInt( this.GetRequestParam("12_EndDate") );
				}
				catch
				{
					this.EndDate = 0;
				}
			}
		}

		private void _initialWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn("SN",				"序列号",null);
			this.gridHelper.AddColumn("SNSeq",				"序列号",null);
			this.gridHelper.AddColumn("TsState",			"维修状态",null);
			this.gridHelper.AddLinkColumn("TSDetails",	"维修信息",null);
			this.gridHelper.AddLinkColumn("ChangedItemDetails",	"换料信息",null);
			this.gridHelper.AddColumn("ModelCode",			"产品别",null);
			this.gridHelper.AddColumn("ItemCode",			"产品",null);
			this.gridHelper.AddColumn("MoCode",			"工单",null);
			this.gridHelper.AddColumn("NGDate",				"不良日期",null);
			this.gridHelper.AddColumn("NGTime",			"不良时间",null);
			this.gridHelper.AddColumn("SourceResource",		"来源站",null);	
			this.gridHelper.AddColumn( "SouceResourceDate",			"维修日期",	null);
			this.gridHelper.AddColumn( "SouceResourceTime",		"维修时间",	null);
			this.gridHelper.AddColumn( "RepaireResource",		"维修站",	null);
            this.gridHelper.AddColumn("Ts_DestOpCode", "去向站", null);	

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridWebGrid.Columns.FromKey("SNSeq").Hidden = true;
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
			manager.Add( new LengthCheck(this.lblOperatorQuery,this.txtOperatorQuery,System.Int32.MaxValue,true) );
			
			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return true;
			}	
			return true;
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{		
			if( this._checkRequireFields() )
			{
				if((sender is System.Web.UI.HtmlControls.HtmlInputButton ) && ((System.Web.UI.HtmlControls.HtmlInputControl)(((System.Web.UI.HtmlControls.HtmlInputButton)(sender)))).Name == "cmdGridExport")
				{
					//TODO ForSimone
					this.ExportQueryEvent(sender,e);
				}
				else
				{
					this.QueryEvent(sender,e);
				}
				
			}
		}

		//查询事件
		private void QueryEvent(object sender, EventArgs e)
		{
			//added by jessie lee,2005/9/20
			//限制计入统计的维修状态
			ArrayList array = new ArrayList();
			array.Add( TSStatus.TSStatus_Complete );
			array.Add( TSStatus.TSStatus_Reflow );
			FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
			( e as WebQueryEventArgsNew ).GridDataSource = 
				facadeFactory.CreateQueryTSRecordFacade().QueryTSPerformanceRecord(						
				FormatHelper.CleanString(this.ModelCode).ToUpper(),
				FormatHelper.CleanString(this.ItemCode).ToUpper(),
				FormatHelper.CleanString(this.MoCode).ToUpper(),
				"","",					
				this.StartDate,
				this.EndDate,
				array,this.TsOperator,this.TsResource,
				( e as WebQueryEventArgsNew ).StartRow,
				( e as WebQueryEventArgsNew ).EndRow);

			( e as WebQueryEventArgsNew ).RowCount = 
				facadeFactory.CreateQueryTSRecordFacade().QueryTSPerformanceRecordCount(						
				FormatHelper.CleanString(this.ModelCode).ToUpper(),
				FormatHelper.CleanString(this.ItemCode).ToUpper(),
				FormatHelper.CleanString(this.MoCode).ToUpper(),
				"","",					
				this.StartDate,
				this.EndDate,
				array,this.TsOperator,this.TsResource);
		}

		//导出事件
		private void ExportQueryEvent(object sender, EventArgs e)
		{
			if(chbRepairDetail.Checked)
			{
				//TODO ForSimone
				//added by jessie lee,2005/9/20
				//限制计入统计的维修状态
				ArrayList array = new ArrayList();
				array.Add( TSStatus.TSStatus_Complete );
				array.Add( TSStatus.TSStatus_Reflow );
				FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
				( e as WebQueryEventArgsNew ).GridDataSource = 
					facadeFactory.CreateQueryTSRecordFacade().ExportQueryTSPerformanceRecord(						
					FormatHelper.CleanString(this.ModelCode).ToUpper(),
					FormatHelper.CleanString(this.ItemCode).ToUpper(),
					FormatHelper.CleanString(this.MoCode).ToUpper(),
					"","",					
					this.StartDate,
					this.EndDate,
					array,this.TsOperator,this.TsResource,
					( e as WebQueryEventArgsNew ).StartRow,
					( e as WebQueryEventArgsNew ).EndRow);
			}
			else
			{
				this.QueryEvent(sender,e);
			}
			
		}



		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgsNew ).DomainObject != null )
			{
				QDOTSRecord obj = ( e as DomainObjectToGridRowEventArgsNew ).DomainObject as QDOTSRecord;
                DataRow row = DtSource.NewRow();
                row["SN"] = obj.SN;
                row["SNSeq"] = obj.RunningCardSequence;
                    row["TsState"] = this.languageComponent1.GetString(obj.TsState);
                    row["TSDetails"] = "";
                    row["ChangedItemDetails"] = "";
                    row["ModelCode"] =   obj.ModelCode;
                    row["ItemCode"] = obj.ItemCode;
                    row["MoCode"] = obj.MoCode;
                    row["NGDate"] = FormatHelper.ToDateString(obj.SourceResourceDate);
                    row["NGTime"] = FormatHelper.ToTimeString(obj.SourceResourceTime);
                    row["SourceResource"] = obj.SourceResource;
                    row["SouceResourceDate"] = FormatHelper.ToDateString(obj.RepaireDate);
                    row["SouceResourceTime"] = FormatHelper.ToTimeString(obj.RepaireTime);
                    row["RepaireResource"] =obj.RepaireResource;
                    row["Ts_DestOpCode"] = obj.DestOpCode;
				( e as DomainObjectToGridRowEventArgsNew ).GridRow = row;
					
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgsNew ).DomainObject != null )
			{
				
				if(chbRepairDetail.Checked)
				{
					ExportQDOTSDetails1 obj = ( e as DomainObjectToExportRowEventArgsNew ).DomainObject as ExportQDOTSDetails1;
					( e as DomainObjectToExportRowEventArgsNew ).ExportRow = 
						new string[]{
										obj.SN,
										obj.NGCount.ToString(),
										this.languageComponent1.GetString(obj.TsState),
										obj.ModelCode,
										obj.ItemCode,													  
										obj.MoCode,

										obj.ErrorCodeGroup,
										obj.ErrorCodeGroupDescription,
										obj.ErrorCode,
										obj.ErrorCodeDescription,
										obj.ErrorCauseCode,
										obj.ErrorCauseDescription,
										obj.ErrorLocation,
										obj.ErrorParts,
										obj.Solution,
										obj.SolutionDescription,
										obj.Duty,
										obj.DutyDescription,

										obj.Memo,
										FormatHelper.ToDateString(obj.RepaireDate),
										FormatHelper.ToTimeString(obj.RepaireTime),
										obj.RepaireResource,
										obj.TSUser,
										obj.DestOpCode
									};
				}
				else
				{
					QDOTSRecord obj = ( e as DomainObjectToExportRowEventArgsNew ).DomainObject as QDOTSRecord;
					( e as DomainObjectToExportRowEventArgsNew ).ExportRow = 
						new string[]{
										obj.SN,
										this.languageComponent1.GetString(obj.TsState),
										obj.ModelCode,
										obj.ItemCode,													  
										obj.MoCode,
										FormatHelper.ToDateString(obj.SourceResourceDate),
										FormatHelper.ToTimeString(obj.SourceResourceTime),													  
										obj.SourceResource,
										FormatHelper.ToDateString(obj.RepaireDate),
										FormatHelper.ToTimeString(obj.RepaireTime),
										obj.RepaireResource,
										obj.DestOpCode
									};
				}
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			if(chbRepairDetail.Checked)
			{
				( e as ExportHeadEventArgsNew ).Heads = 
					new string[]{
									"Ts_SN",
									"Ts_Count",
									"Ts_TsState",
									"Ts_ModelCode",
									"Ts_ItemCode",
									"Ts_MoCode",

									"ErrorCodeGroup",
									"ErrorCodeGroupDescription",
									"ErrorCode",
									"ErrorCodeDescription",
									"ErrorCauseCode",
									"ErrorCauseDescription",
									"ErrorLocation",
									"ErrorParts",
									"Solution",
									"SolutionDescription",
									"Duty",
									"DutyDescription",

									"Memo",
									"Ts_SouceResourceDate",
									"Ts_SouceResourceTime",
									"Ts_RepaireResource",
									"Ts_TSUser",
									"Ts_DestOpCode"
									
								};
			}
			else
			{
				( e as ExportHeadEventArgsNew ).Heads = 
					new string[]{
									"SN",
									"TsState",
									"ModelCode",
									"ItemCode",
									"MoCode",
									"NGDate",
									"NGTime",
									"SourceResource",
									"SouceResourceDate",
									"SouceResourceTime",
									"RepaireResource",
									"Ts_DestOpCode"
								};
			}
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect("FTSPerformanceQP.aspx");
		}
		
		protected override void Grid_ClickCell( GridRecord row, string commandName)
        {
			string[] keys = new string[13 + this.Request.QueryString.AllKeys.Length];
			string[] values = new string[13 + this.Request.QueryString.AllKeys.Length];

			int length = this.Request.QueryString.AllKeys.Length;
			for(int i=0;i<length;i++)
			{
				keys[i] = this.Request.QueryString.AllKeys.GetValue(i).ToString();
				values[i] = this.Request.QueryString[i].ToString();
			}
			keys[length+0] = "ModelCode";
			keys[length+1] = "ItemCode";
			keys[length+2] = "MoCode";
			keys[length+3] = "RunningCard";
			keys[length+4] = "TSState";
			keys[length+5] = "TSResourceCode";
			keys[length+6] = "TSOperator";
			keys[length+7] = "TSDate";
			keys[length+8] = "TSTime";
			keys[length+9] = "StartDate";
			keys[length+10] = "EndDate";
			keys[length+11] = "BackUrl";
			keys[length+12] = "RunningCardSeq";

			values[length+0] = row.Items.FindItemByKey("ModelCode").Text;
			values[length+1] = row.Items.FindItemByKey("ItemCode").Text;
			values[length+2] = row.Items.FindItemByKey("MoCode").Text;
			values[length+3] = row.Items.FindItemByKey("SN").Text;
			values[length+4] = row.Items.FindItemByKey("TsState").Text;
			values[length+5] = row.Items.FindItemByKey("RepaireResource").Text;
			values[length+6] = this.TsOperator;
			values[length+7] = row.Items.FindItemByKey("NGDate").Text;
			values[length+8] = row.Items.FindItemByKey("NGTime").Text;
			values[length+9] = this.GetRequestParam("StartDate");
			values[length+10] = this.GetRequestParam("EndDate");
			values[length+11] = "FTSPerformanceListQP.aspx";
			values[length+12] = row.Items.FindItemByKey("SNSeq").Text;

			if(commandName.ToUpper() == "TSDetails".ToUpper() )
			{
				this.Response.Redirect( 
					this.MakeRedirectUrl(
					"FTSRecordDetailsQP.aspx",keys,values)
//					new string[]{
//									"ModelCode",
//									"ItemCode",
//									"MoCode",
//									"RunningCard",
//									"TSState",
//									"TSResourceCode",
//									"TSOperator",
//									"TSDate",
//									"TSTime",
//									"StartDate",
//									"EndDate",
//									"BackUrl"
//								},
//					new string[]{
//									row.Items.FindItemByKey("ModelCode").Text,
//									row.Items.FindItemByKey("ItemCode").Text,
//									row.Items.FindItemByKey("MoCode").Text,
//									row.Items.FindItemByKey("SN").Text,
//									row.Items.FindItemByKey("TsState").Text,
//									this.TsResource,
//									this.GetRequestParam("TSOperator"),
//									row.Items.FindItemByKey("SouceResourceDate").Text,
//									row.Items.FindItemByKey("SouceResourceTime").Text,
//									this.GetRequestParam("StartDate"),
//									this.GetRequestParam("EndDate"),
//									"FTSPerformanceListQP.aspx"
//								}					
					);
			}
			if(commandName.ToUpper() == "ChangedItemDetails".ToUpper() )
			{
				this.Response.Redirect( 
					this.MakeRedirectUrl(
					"FTSChangedItemQP.aspx",keys,values)
//					new string[]{
//									"ModelCode",
//									"ItemCode",
//									"MoCode",
//									"RunningCard",
//									"TSState",
//									"TSResourceCode",
//									"TSOperator",
//									"TSDate",
//									"TSTime",
//									"StartDate",
//									"EndDate",
//									"BackUrl"
//								},
//					new string[]{
//									row.Items.FindItemByKey("ModelCode").Text,
//									row.Items.FindItemByKey("ItemCode").Text,
//									row.Items.FindItemByKey("MoCode").Text,
//									row.Items.FindItemByKey("SN").Text,
//									row.Items.FindItemByKey("TsState").Text,
//									this.TsResource,
//									this.GetRequestParam("TSOperator"),
//									row.Items.FindItemByKey("SouceResourceDate").Text,
//									row.Items.FindItemByKey("SouceResourceTime").Text,
//									this.GetRequestParam("StartDate"),
//									this.GetRequestParam("EndDate"),
//									"FTSPerformanceListQP.aspx"
//								})
					);
			}
		}
	}
}
