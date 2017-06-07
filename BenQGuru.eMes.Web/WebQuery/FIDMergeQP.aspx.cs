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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FQueryIDMergeQP 的摘要说明。
	/// </summary>
	public partial class FIDMergeQP : BaseQPageNew
	{
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        //private System.ComponentModel.IContainer components;

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelperNew _helper = null;
		protected GridHelperNew _gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.gridHelper=new GridHelperNew(this.gridWebGrid,this.DtSource);
			this._gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);

			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();

				this.txtDateFrom.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				this.txtDateTo.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
			}

			this._helper = new WebQueryHelperNew( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1,this.DtSource );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);		
			FormatHelper.SetSNRangeValue(txtStartSnQuery,txtEndSnQuery);
            this.chbFrmDate.Visible = false;
		}

		private void _initialWebGrid()
		{
            base.InitWebGrid();
			this._gridHelper.AddColumn("MOCode",				"工单",null);
			this._gridHelper.AddColumn("ItemCode",			"产品代码",null);
			this._gridHelper.AddColumn("RunningCardBeforeTranslation",			"转换前转换序列号",null);
			this._gridHelper.AddColumn("CollectionOperationCode",			"采集工位",null);
			this._gridHelper.AddColumn("CollectionStepSequenceCode",			"采集线别",null);
			this._gridHelper.AddColumn("CollectionResourceCode",				"采集资源",null);
			this._gridHelper.AddColumn("RunningCardAfterTranslation",			"转换后转换序列号",null);
			this._gridHelper.AddColumn( "EmployeeNo",			"员工工号",	null);
			this._gridHelper.AddColumn( "CollectionDate",		"采集日期",	null);
			this._gridHelper.AddColumn( "CollectionTime",		"采集时间",	null);	

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

		private bool _checkRequireFields()
		{			
			PageCheckManager manager = new PageCheckManager();
			//manager.Add( new LengthCheck(this.lblMoConditionQuery,this.txtConditionMo.TextBox,System.Int32.MaxValue,true) );

			manager.Add( new DateRangeCheck(this.chbFrmDate,this.txtDateFrom.Text,lblFrmDateT,txtDateTo.Text,0,7,true));

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return false;
			}	
			return true;
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
			string beginDate = String.Empty;
			string endDate = String.Empty;

            //if(chbFrmDate.Checked == true)
            //{
            //    if(!_checkRequireFields())
            //    {
            //        return;
            //    }
				beginDate = FormatHelper.CleanString(FormatHelper.TODateInt(this.txtDateFrom.Text).ToString()).ToUpper();
				endDate = FormatHelper.CleanString(FormatHelper.TODateInt(this.txtDateTo.Text).ToString()).ToUpper();
            //}

			FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider) ;
			( e as WebQueryEventArgsNew ).GridDataSource = 
				facadeFactory.CreateQueryCardTransferFacade().QueryCardTransfer(
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartSnQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtEndSnQuery.Text).ToUpper(),
				beginDate,
				endDate,
				( e as WebQueryEventArgsNew ).StartRow,
				( e as WebQueryEventArgsNew ).EndRow);

			( e as WebQueryEventArgsNew ).RowCount = 
				facadeFactory.CreateQueryCardTransferFacade().QueryCardTransferCount(
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.txtStartSnQuery.Text).ToUpper(),
				FormatHelper.CleanString(this.txtEndSnQuery.Text).ToUpper(),
				beginDate,
				endDate);
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgsNew ).DomainObject != null )
			{
				OnWIPCardTransfer obj = ( e as DomainObjectToGridRowEventArgsNew ).DomainObject as OnWIPCardTransfer;

                DataRow row = this.DtSource.NewRow();
                row["MOCode"] = obj.MOCode;
                row["ItemCode"] = obj.ItemCode;
                row["RunningCardBeforeTranslation"] = obj.TranslateCard;
                row["CollectionOperationCode"] = obj.OPCode;
                row["CollectionStepSequenceCode"] = obj.StepSequenceCode;
                row["CollectionResourceCode"] = obj.ResourceCode;
                row["RunningCardAfterTranslation"] = obj.RunningCard;
                row["EmployeeNo"] = obj.MaintainUser;
                row["CollectionDate"] = FormatHelper.ToDateString(obj.MaintainDate);
                row["CollectionTime"] = FormatHelper.ToTimeString(obj.MaintainTime);


                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                //    new UltraGridRow( new object[]{
                //                                      obj.MOCode,
                //                                      obj.ItemCode,
                //                                      obj.TranslateCard,
                //                                      obj.OPCode,													  
                //                                      obj.StepSequenceCode,
                //                                      obj.ResourceCode,
                //                                      obj.RunningCard,
                //                                      obj.MaintainUser,
                //                                      FormatHelper.ToDateString(obj.MaintainDate),
                //                                      FormatHelper.ToTimeString(obj.MaintainTime)
                //                                  }
                //    );
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgsNew ).DomainObject != null )
			{
				OnWIPCardTransfer obj = ( e as DomainObjectToExportRowEventArgsNew ).DomainObject as OnWIPCardTransfer;
				( e as DomainObjectToExportRowEventArgsNew ).ExportRow = 
					new string[]{
									obj.MOCode,
									obj.ItemCode,
									obj.TranslateCard,
									obj.OPCode,													  
									obj.StepSequenceCode,
									obj.ResourceCode,
									obj.RunningCard,
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
								"ItemCode",
								"RunningCardBeforeTranslation",
								"CollectionOperationCode",
								"CollectionStepSequenceCode",
								"CollectionResourceCode",
								"RunningCardAfterTranslation",
								"EmployeeNo",
								"CollectionDate",
								"CollectionTime"
							};
		}		
	}
}
