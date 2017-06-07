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

using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Web.UserControl ;
using BenQGuru.eMES.WebQuery ;
using BenQGuru.eMES.Common ;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FItemTracingQP 的摘要说明。
	/// </summary>
	public partial class FITOPResultTestingQP : BaseMPageNew
	{
    

        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private QueryFacade2 _facade = null ;
		private QueryTSDetailsFacade _TSfacade = null ;
 // FacadeFactory.CreateQueryFacade2() ;

        private string[] caredActions = new string[]
            {
                ActionType.DataCollectAction_GOOD ,
                ActionType.DataCollectAction_NG ,
                ActionType.DataCollectAction_SMTGOOD ,
                ActionType.DataCollectAction_SMTNG 
            } ;


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
			//this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion


        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelper2 = new GridHelperNew(this.gridWebGrid2, this.DtSource2);
            if(!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

                string rcard = this.GetRequestParam("RCARD") ;
                int rcardseq ;
                try
                {
                    rcardseq = int.Parse( this.GetRequestParam("RCARDSEQ") );
                }
                catch
                {
                    rcardseq = -1 ;
                }
                string moCode = this.GetRequestParam("MOCODE") ;

				if(_facade==null)
				{
					_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
				}
                object obj = this._facade.GetProductionProcess( moCode,rcard,rcardseq ) ;
                if(obj == null)
                {
                    ExceptionManager.Raise(this.GetType() , "$Error_ItemTracing_not_exist") ;
                }

                this.txtItem.Value = ((ProductionProcess)obj).ItemCode ;
                this.txtMO.Value = ((ProductionProcess)obj).MOCode ; 
                this.txtModel.Value = ((ProductionProcess)obj).ModelCode ; 
                this.txtSN.Value = ((ProductionProcess)obj).RCard ; 
                this.txtSeq.Value = ((ProductionProcess)obj).RCardSequence.ToString() ;


            }

            //this.gridHelper2 = new GridHelperNew(this.gridWebGrid2, this.DtSource2);
            //this.gridHelper2.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource2);
            //this.gridHelper2.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount2);
            //this.gridHelper2.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow2);

            //InitWebGrid2();

        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("IT_Route","生产途程",null) ;
            this.gridHelper.AddColumn( "IT_OP", "工序",	null);
            this.gridHelper.AddColumn( "IT_ItemStatus", "产品状态",	null);
            this.gridHelper.AddColumn( "IT_OPType", "工序结果",	null);
            this.gridHelper.AddLinkColumn( "IT_OPResult", "工序结果",	null);
            this.gridHelper.AddColumn( "IT_Segment", "工段",	null);
            this.gridHelper.AddColumn( "IT_Line", "生产线",	null);
            this.gridHelper.AddColumn( "IT_Resource", "资源",	null);
            this.gridHelper.AddColumn( "IT_MaintainDate", "日期",	null);
            this.gridHelper.AddColumn( "IT_MaintainTime", "时间",	null);
            this.gridHelper.AddColumn( "IT_MaintainUser", "操作工",	null);

            this.gridHelper.AddColumn( "IT_OPType_ORI", "工序结果",	null);

            this.gridHelper.Grid.Columns.FromKey("IT_OPType_ORI").Hidden = true ;
			this.gridHelper.Grid.Columns.FromKey("IT_OPResult").Hidden = true ;

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
 
            this.gridHelper.RequestData();

        }

		
		
        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    ((OPResult)obj).RouteCode.ToString(),
            //                    ((OPResult)obj).OPCode.ToString(),
            //                    this.languageComponent1.GetString( ((OPResult)obj).ItemStatus ),
            //                    //WebQueryHelper.GetOPResultLinkText(this.languageComponent1,((OPResult)obj).OPType),
            //                    this.languageComponent1.GetString("ItemTracing_testing"),
            //                    "",
            //                    ((OPResult)obj).SegmentCode.ToString(),
            //                    ((OPResult)obj).LineCode.ToString(),
            //                    ((OPResult)obj).ResCode.ToString(),
            //                    FormatHelper.ToDateString(((OPResult)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((OPResult)obj).MaintainTime),
            //                    ((OPResult)obj).MaintainUser.ToString(),
            //                    ((OPResult)obj).OPType.ToString()
            //                }
            //    );

            DataRow row = this.DtSource.NewRow();
            row["IT_Route"] = ((OPResult)obj).RouteCode.ToString();
            row["IT_OP"] = ((OPResult)obj).OPCode.ToString();
            row["IT_ItemStatus"] = this.languageComponent1.GetString(((OPResult)obj).ItemStatus);
            row["IT_OPType"] = this.languageComponent1.GetString("ItemTracing_testing");
            row["IT_Segment"] = ((OPResult)obj).SegmentCode.ToString();
            row["IT_Line"] = ((OPResult)obj).LineCode.ToString();
            row["IT_Resource"] = ((OPResult)obj).ResCode.ToString();
            row["IT_MaintainDate"] = FormatHelper.ToDateString(((OPResult)obj).MaintainDate);
            row["IT_MaintainTime"] = FormatHelper.ToTimeString(((OPResult)obj).MaintainTime);
            row["IT_MaintainUser"] = ((OPResult)obj).MaintainUser.ToString();
            row["IT_OPType_ORI"] = ((OPResult)obj).OPType.ToString();
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {

            int seq ;
            try
            {
                seq = int.Parse(this.txtSeq.Value) ;
            }
            catch
            {
                seq = -1 ;
            }
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
			}
            return this._facade.QueryOPResult(
                this.txtMO.Value,
                this.txtSN.Value,
                seq,
                caredActions,
                inclusive, exclusive );
        }


        protected override int GetRowCount()
        {

            int seq ;
            try
            {
                seq = int.Parse(this.txtSeq.Value) ;
            }
            catch
            {
                seq = -1 ;
            }
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
			}
            return this._facade.QueryOPResultCount(
                this.txtMO.Value,
                this.txtSN.Value,
                seq,
                caredActions
                );
        }


		#region GridNG

		protected override void InitWebGrid2()
		{
            //this.gridHelper2 = new GridHelperNew(this.gridWebGrid2, this.DtSource2);
            base.InitWebGrid2();
			this.gridHelper2.AddColumn("ErrorCodeGroup",	"不良代码组",null);
			this.gridHelper2.AddColumn("ErrorCode",			"不良代码",null);
			this.gridHelper2.AddColumn("ErrorCause",			"不良原因",null);
			this.gridHelper2.AddColumn( "ErrorLocation",		"不良位置",	null);
			this.gridHelper2.AddColumn( "ErrorParts",		"不良元件",	null);
			this.gridHelper2.AddColumn("Solution",			"解决方案",null);
			this.gridHelper2.AddColumn("Duty",			"责任别",null);		
			this.gridHelper2.AddColumn( "Memo",		"补充说明",	null);
			this.gridHelper2.AddColumn( "TsOperator",		"维修工",	null);
			this.gridHelper2.AddColumn( "MaintainDate",		"维修日期",	null);
			this.gridHelper2.AddColumn( "MaintainTime",		"维修时间",	null);

            //this.gridHelper2.Grid.Columns.FromKey("ErrorCodeGroup").MergeCells = true;
            //this.gridHelper2.Grid.Columns.FromKey("ErrorCode").MergeCells = true;

			this.gridHelper2.ApplyLanguage( this.languageComponent1 );
 
			this.gridHelper2.RequestData();

		}

		protected override DataRow GetGridRow2(object obj)
		{
			QDOTSDetails1 returnObj = obj as QDOTSDetails1;
            if (returnObj != null)
            {
                DataRow row = this.DtSource2.NewRow();
                row["ErrorCodeGroup"] = returnObj.ErrorCodeGroupDescription;
                row["ErrorCode"] = returnObj.ErrorCodeDescription;
                row["ErrorCause"] = returnObj.ErrorCauseDescription;
                row["ErrorLocation"] = returnObj.ErrorLocation;
                row["ErrorParts"] = returnObj.ErrorParts;
                row["Solution"] = returnObj.SolutionDescription;
                row["Duty"] = returnObj.DutyDescription;
                row["Memo"] = returnObj.Memo;
                row["TsOperator"] = returnObj.TSOperator;
                row["MaintainDate"] = FormatHelper.ToDateString(returnObj.TSDate);
                row["MaintainTime"] = FormatHelper.ToTimeString(returnObj.TSTime);
                return row;
            }
            else
            {
              
                DataRow row = this.DtSource.NewRow();
                row["ErrorCodeGroup"] = "";
                row["ErrorCode"] = "";
                row["ErrorCause"] = "";
                row["ErrorLocation"] = "";
                row["ErrorParts"] = "";
                row["Solution"] = "";
                row["Duty"] = "";
                row["Memo"] = "";
                row["TsOperator"] = "";
                row["MaintainDate"] = "";
                row["MaintainTime"] = "";
                return row;
            }
		}

		protected override object[] LoadDataSource2(int inclusive, int exclusive)
		{
			if(_TSfacade==null)
			{
				_TSfacade =  new QueryTSDetailsFacade(base.DataProvider);
			}
            return this._TSfacade.QueryTSDetails(						
				FormatHelper.CleanString(this.txtMO.Value).ToUpper(),
				FormatHelper.CleanString(this.txtSN.Value).ToUpper(),
				int.Parse(this.txtSeq.Value),
				1, int.MaxValue );
		}

		protected override int GetRowCount2()
		{
			if(_TSfacade==null)
			{
				_TSfacade =  new QueryTSDetailsFacade(base.DataProvider);
			}
			return this._TSfacade.QueryTSDetailsCount(						
				FormatHelper.CleanString(this.txtMO.Value).ToUpper(),
				FormatHelper.CleanString(this.txtSN.Value).ToUpper(),
				int.Parse(this.txtSeq.Value)
				);
		}

		#endregion

        #endregion
        
        #region Export 	
        protected override string[] FormatExportRecord( object obj )
        {
            return  new string[]{
                                    ((OPResult)obj).RouteCode.ToString(),
                                    ((OPResult)obj).OPCode.ToString(),
                                    this.languageComponent1.GetString( ((OPResult)obj).ItemStatus ),
									//WebQueryHelper.GetOPResultLinkText(this.languageComponent1,((OPResult)obj).OPType),
									this.languageComponent1.GetString("ItemTracing_testing"),
                                    ((OPResult)obj).SegmentCode.ToString(),
                                    ((OPResult)obj).LineCode.ToString(),
                                    ((OPResult)obj).ResCode.ToString(),
                                    FormatHelper.ToDateString(((OPResult)obj).MaintainDate),
                                    FormatHelper.ToTimeString(((OPResult)obj).MaintainTime),
                                    ((OPResult)obj).MaintainUser.ToString(),
            }
            ;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {
                                    "IT_Route", 
                                    "IT_OP", 
                                    "IT_ItemStatus", 
                                    "IT_OPType", 
                                    "IT_Segment", 
                                    "IT_Line", 
                                    "IT_Resource", 
                                    "IT_MaintainDate", 
                                    "IT_MaintainTime", 
                                    "IT_MaintainUser"
                                };
        }
        #endregion

        protected override void Grid_ClickCell(GridRecord row, string command)
        {

            string opType = row.Items.FindItemByKey("IT_OPType_ORI").Text;

            if(command=="IT_OPResult")
            {
                string gotoURL = string.Empty ;

                if(opType == BenQGuru.eMES.Web.Helper.OPType.COMPLOADING)
                {
                    gotoURL = "FITOPResultComploadingQP.aspx" ;
                }

                if(opType == BenQGuru.eMES.Web.Helper.OPType.PACKING)
                {
                    gotoURL = "FITOPResultPackingQP.aspx" ;
                }

                if(opType == BenQGuru.eMES.Web.Helper.OPType.SN)
                {
                    gotoURL = "FITOPResultSNQP.aspx" ;
                }

                if(opType == BenQGuru.eMES.Web.Helper.OPType.TESTING)
                {
                    gotoURL = "FITOPResultTestingQP.aspx" ;
                }

                if(opType == BenQGuru.eMES.Web.Helper.OPType.TS)
                {
                    gotoURL = "FITOPResultTSQP.aspx" ;
                }

                Response.Redirect(string.Format("{0}?RCARD={1}", gotoURL, row.Items.FindItemByKey("IT_OP").Text));
            }

        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            string referedURL = this.GetRequestParam("REFEREDURL") ;
            if( referedURL == string.Empty)
            {
                referedURL = "FItemTracingQP.aspx" ;
            }
            else
            {
                referedURL = System.Web.HttpUtility.UrlDecode(referedURL) ;
            }
            Response.Redirect( referedURL ) ;
        }


	}
}
