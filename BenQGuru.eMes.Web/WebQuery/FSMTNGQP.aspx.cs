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
using BenQGuru.eMES.Domain.TS;
using BenQGuru.eMES.DataCollect;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FSMTNGQP 的摘要说明。
	/// </summary>
	public partial class FSMTNGQP : BaseQPageNew
	{

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelperNew _helper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		//protected GridHelper gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);

			this._helper = new WebQueryHelperNew( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1,DtSource );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				
			//this._helper.GridCellClick +=new EventHandler(_helper_GridCellClick);

			FormatHelper.SetSNRangeValue(txtStartSnQuery,txtEndSnQuery);			

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
				this._initialWebGrid();
			}
		}

		private void _initialWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn("MOCode",				"工单",null);
			this.gridHelper.AddColumn("ItemCode",			"产品代码",null);
			this.gridHelper.AddColumn("RunningCard",			"产品序列号",null);
			this.gridHelper.AddColumn("CollectionOperationCode",			"采集工位",null);
			this.gridHelper.AddColumn("CollectionStepSequenceCode",			"采集线别",null);
			this.gridHelper.AddColumn("CollectionResourceCode",				"采集资源",null);
			this.gridHelper.AddColumn( "EmployeeNo",			"员工工号",	null);
			this.gridHelper.AddColumn( "CollectionDate",		"采集日期",	null);
			this.gridHelper.AddColumn( "CollectionTime",		"采集时间",	null);	
			this.gridHelper.AddColumn("TSStatus",				"当前状态",null);
			this.gridHelper.AddLinkColumn("NGDetails","不良明细",null);

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
			manager.Add( new LengthCheck(this.lblMOIDQuery,this.txtConditionMo.TextBox,System.Int32.MaxValue,true) );

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return false;
			}	
			return true;
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
            //将序列号转换为SourceCode
            DataCollectFacade dataCollectfacade = new DataCollectFacade(this.DataProvider);
            //对于序列号的输入框，需要进行一下处理
            string startRCard = FormatHelper.CleanString(this.txtStartSnQuery.Text.Trim().ToUpper());
            string endRCard = FormatHelper.CleanString(this.txtEndSnQuery.Text.Trim().ToUpper());
            //转换成SourceCard
            string startSourceCard = dataCollectfacade.GetSourceCard(startRCard, string.Empty);
            string endSourceCard = dataCollectfacade.GetSourceCard(endRCard, string.Empty);
            //end

			FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
			( e as WebQueryEventArgsNew ).GridDataSource = 
				facadeFactory.CreateQuerySMTNGFacade().QuerySMTNG(
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionStepSequence.Text).ToUpper(),
                FormatHelper.CleanString(startSourceCard),
                FormatHelper.CleanString(endSourceCard),
				( e as WebQueryEventArgsNew ).StartRow,
				( e as WebQueryEventArgsNew ).EndRow);

			( e as WebQueryEventArgsNew ).RowCount = 
				facadeFactory.CreateQuerySMTNGFacade().QuerySMTNGCount(
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionStepSequence.Text).ToUpper(),
                FormatHelper.CleanString(startSourceCard),
                FormatHelper.CleanString(endSourceCard));
		
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgsNew ).DomainObject != null )
			{
				BenQGuru.eMES.Domain.TS.TS obj = ( e as DomainObjectToGridRowEventArgsNew ).DomainObject as BenQGuru.eMES.Domain.TS.TS;
                DataRow row = DtSource.NewRow();
                row["MOCode"] = obj.MOCode;
                row["ItemCode"] = obj.ItemCode;
                row["RunningCard"] = obj.RunningCard;
                row["CollectionOperationCode"] = obj.FromOPCode;
                row["CollectionStepSequenceCode"] = obj.FromStepSequenceCode;
                row["CollectionResourceCode"] = obj.FromResourceCode;
                row["EmployeeNo"] = obj.MaintainUser;
                row["CollectionDate"] = FormatHelper.ToDateString(obj.MaintainDate);
                row["CollectionTime"] = FormatHelper.ToTimeString(obj.MaintainTime);
                row["TSStatus"] = this.languageComponent1.GetString(obj.TSStatus);
                row["NGDetails"] = "";
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
					
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgsNew ).DomainObject != null )
			{
				BenQGuru.eMES.Domain.TS.TS obj = ( e as DomainObjectToExportRowEventArgsNew ).DomainObject as BenQGuru.eMES.Domain.TS.TS;
				( e as DomainObjectToExportRowEventArgsNew ).ExportRow = 
					new string[]{
									obj.MOCode,
									obj.ItemCode,
									obj.RunningCard,
									obj.FromOPCode,													  
									obj.FromStepSequenceCode,
									obj.FromResourceCode,
									obj.MaintainUser,
									FormatHelper.ToDateString(obj.MaintainDate),
													  FormatHelper.ToTimeString(obj.MaintainTime),
									this.languageComponent1.GetString(obj.TSStatus)													  
				};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgsNew ).Heads = 
				new string[]{
								"MOCode",
								"ItemCode",
								"RunningCard",
								"CollectionOperationCode",
								"CollectionStepSequenceCode",
								"CollectionResourceCode",
								"EmployeeNo",
								"CollectionDate",
								"CollectionTime",
								"TSStatus"
							};
		}

        protected override void Grid_ClickCell(GridRecord row, string command)
        {

            if (command == "NGDetails")
			{
				this.Response.Redirect( 
					this.MakeRedirectUrl(
					"FSMTNGListQP.aspx",
					new string[]{
									"RunningCard"
								},
					new string[]{
									row.Items.FindItemByKey("RunningCard").Text
								})
					);
			}
		}			
	}
}
