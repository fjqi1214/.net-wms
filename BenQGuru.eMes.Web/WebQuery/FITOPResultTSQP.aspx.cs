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
	public partial class FITOPResultTSQP : BaseMPageNew
	{
    

        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private QueryFacade2 _facade = null ;
		private QueryTSDetailsFacade _tsFacade = null ;
        //protected GridHelper gridTSHelper ;
        //protected GridHelper gridHLHelper ;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid Ultrawebgrid1;

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
            this.gridHelper2 = new GridHelperNew(this.gridWebGrid2,this.DtSource2);
            this.gridHelper3 = new GridHelperNew(this.gridWebGrid3, this.DtSource3);
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
			}

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

                //this.gridTSHelper = new GridHelper(this.gridTS) ;
                //this.gridTSHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSourceTS);
                //this.gridTSHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRowTS);	
                //InitWebGridTS() ;

                //this.gridHLHelper = new GridHelper(this.gridHL) ;
                //this.gridHLHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSourceHL);
                //this.gridHLHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRowHL);	
                //InitWebGridHL() ;	
            }
			//this.pagerSizeSelector.Readonly = true ;
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
            this.gridHelper.AddColumn( "IT_Route","生产途程",null) ;
            this.gridHelper.AddColumn( "IT_OP", "工序",	null);
            this.gridHelper.AddColumn( "IT_ItemStatus", "产品状态",	null);
            this.gridHelper.AddColumn( "IT_OPType", "工序结果",	null);
            this.gridHelper.AddColumn( "IT_Segment", "工段",	null);
            this.gridHelper.AddColumn( "IT_Line", "生产线",	null);
            this.gridHelper.AddColumn( "IT_Resource", "资源",	null);
            this.gridHelper.AddColumn( "IT_MaintainDate", "日期",	null);
            this.gridHelper.AddColumn( "IT_MaintainTime", "时间",	null);
			this.gridHelper.AddColumn( "IT_MaintainUser", "操作工",	null);

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

            this.gridHelper.RequestData();

        }
		
        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    ((TSOPResult)obj).RouteCode.ToString(),
            //                    ((TSOPResult)obj).OPCode.ToString(),
            //                    this.languageComponent1.GetString(((TSOPResult)obj).ItemStatus.ToString()),
            //                    this.languageComponent1.GetString( ((TSOPResult)obj).OPType.ToString()),
            //                    ((TSOPResult)obj).SegmentCode.ToString(),
            //                    ((TSOPResult)obj).LineCode.ToString(),
            //                    ((TSOPResult)obj).ResCode.ToString(),
            //                    FormatHelper.ToDateString(((TSOPResult)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((TSOPResult)obj).MaintainTime),
            //                    ((TSOPResult)obj).GetDisplayText("MaintainUser"),
                               
            //                }
            //);
            DataRow row = this.DtSource.NewRow();
            row["IT_Route"] = ((TSOPResult)obj).RouteCode.ToString();
            row["IT_OP"] = ((TSOPResult)obj).OPCode.ToString();
            row["IT_ItemStatus"] = this.languageComponent1.GetString(((TSOPResult)obj).ItemStatus.ToString());
            row["IT_OPType"] = this.languageComponent1.GetString(((TSOPResult)obj).OPType.ToString());
            row["IT_Segment"] = ((TSOPResult)obj).SegmentCode.ToString();
            row["IT_Line"] = ((TSOPResult)obj).LineCode.ToString();
            row["IT_Resource"] = ((TSOPResult)obj).ResCode.ToString();
            row["IT_MaintainDate"] = FormatHelper.ToDateString(((TSOPResult)obj).MaintainDate);
            row["IT_MaintainTime"] = FormatHelper.ToTimeString(((TSOPResult)obj).MaintainTime);
            row["IT_MaintainUser"] = ((TSOPResult)obj).GetDisplayText("MaintainUser");
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
	        return this._facade.QueryTSOPResult(
	            this.txtSN.Value ,
	            seq,
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
            return this._facade.QueryTSOPResultCount(
                this.txtSN.Value,
                seq
                );
        }

        #endregion
        
        #region TSGrid
        protected override void InitWebGrid2()
        {
            base.InitWebGrid2();
            this.gridHelper2.AddColumn( "IT_ErrorGroup", "不良代码组",	null);
            this.gridHelper2.AddColumn("IT_ErrorCode", "不良代码", null);
            this.gridHelper2.AddColumn("IT_ErrorCause", "不良原因", null);
            this.gridHelper2.AddColumn("IT_ErrorLocation", "不良位置", null);
            this.gridHelper2.AddColumn("IT_ErrorPart", "不良零件", null);
            this.gridHelper2.AddColumn("IT_Solution", "解决方案", null);
            this.gridHelper2.AddColumn("IT_Duty", "责任别", null);
            this.gridHelper2.AddColumn("IT_MEMO", "补充说明", null);
            this.gridHelper2.AddColumn("IT_TSUser", "维修工", null);
            this.gridHelper2.AddColumn("IT_TSDate", "维修日期", null);
            this.gridHelper2.AddColumn("IT_TSTime", "维修时间", null);

            //this.gridHelper2.Grid.Bands[0].Columns[0].MergeCells = true; //不良代码组
            //this.gridHelper2.Grid.Bands[0].Columns[1].MergeCells = true; //不良代码组
            //this.gridHelper2.Grid.Bands[0].Columns[2].MergeCells = true; //不良原因

            this.gridHelper2.ApplyLanguage(this.languageComponent1);

            this.gridHelper2.RequestData();

        }

        protected override DataRow GetGridRow2(object obj)
        {
			QDOTSDetails1 tsInfo = obj as QDOTSDetails1 ;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    tsInfo.ErrorCodeGroupDescription ,
            //                    tsInfo.ErrorCodeDescription ,
            //                    tsInfo.ErrorCauseDescription ,
            //                    tsInfo.ErrorLocation ,
            //                    tsInfo.ErrorParts ,
            //                    tsInfo.SolutionDescription ,
            //                    tsInfo.DutyDescription ,
            //                    tsInfo.Memo ,
            //                    tsInfo.GetDisplayText("TSOperator") ,
            //                    FormatHelper.ToDateString( tsInfo.TSDate ),
            //                    FormatHelper.ToTimeString( tsInfo.TSTime )
            //                }
            //    );
            DataRow row = this.DtSource2.NewRow();
            row["IT_ErrorGroup"] = tsInfo.ErrorCodeGroupDescription;
            row["IT_ErrorCode"] = tsInfo.ErrorCodeDescription;
            row["IT_ErrorCause"] = tsInfo.ErrorCauseDescription;
            row["IT_ErrorLocation"] = tsInfo.ErrorLocation;
            row["IT_ErrorPart"] = tsInfo.ErrorParts;
            row["IT_Solution"] = tsInfo.SolutionDescription;
            row["IT_Duty"] = tsInfo.DutyDescription;
            row["IT_MEMO"] = tsInfo.Memo;
            row["IT_TSUser"] = tsInfo.GetDisplayText("TSOperator");
            row["IT_TSDate"] = FormatHelper.ToDateString(tsInfo.TSDate);
            row["IT_TSTime"] = FormatHelper.ToTimeString(tsInfo.TSTime);
            return row;
        }

        protected override object[] LoadDataSource2(int inclusive, int exclusive)
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
			if(_tsFacade==null)
			{
				_tsFacade = new QueryTSDetailsFacade(base.DataProvider);
			}
			return this._tsFacade.QueryTSDetails(
				this.txtSN.Value ,
				seq,
				1, int.MaxValue );
        }

        #endregion

		#region HLGrid
		protected override void InitWebGrid3()
		{
            base.InitWebGrid3();
            this.gridHelper3.AddColumn("MItemCode1", "新物料料号", null);
            this.gridHelper3.AddColumn("MCard1", "新物料序列号", null);
            this.gridHelper3.AddColumn("SItemCode1", "原物料料号", null);
            this.gridHelper3.AddColumn("MSCard1", "原物料序列号", null);
            this.gridHelper3.AddColumn("Location1", "零件位置", null);
            this.gridHelper3.AddColumn("LotNO2", "批号", null);
            this.gridHelper3.AddColumn("VendorCode", "厂商", null);
            this.gridHelper3.AddColumn("VendorItemCode", "厂商料号", null);
            this.gridHelper3.AddColumn("DateCode", "生产日期", null);
            this.gridHelper3.AddColumn("Version", "料品版本", null);
            this.gridHelper3.AddColumn("PCBA", "PCBA版本", null);
            this.gridHelper3.AddColumn("BIOS", "BIOS版本", null);
            this.gridHelper3.AddColumn("Memo", "补充说明", null);

            this.gridHelper3.ApplyLanguage(this.languageComponent1);

            this.gridHelper3.RequestData();

		}
		
		protected override DataRow GetGridRow3(object obj)
		{
			HLInfo hlInfo = obj as HLInfo ;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    hlInfo.MItemCode ,
            //                    hlInfo.MCard ,
            //                    hlInfo.SourceItemCode ,
            //                    hlInfo.MSourceCard ,
            //                    hlInfo.Location ,
            //                    hlInfo.LotNO ,
            //                    hlInfo.VendorCode ,
            //                    hlInfo.VendorItemCode ,
            //                    hlInfo.DateCode ,
            //                    hlInfo.Version ,
            //                    hlInfo.PCBA ,
            //                    hlInfo.BIOS ,
            //                    hlInfo.Memo 
            //                }
            //    );
            DataRow row = this.DtSource3.NewRow();
            row["MItemCode1"] = hlInfo.MItemCode;
            row["MCard1"] = hlInfo.MCard;
            row["SItemCode1"] = hlInfo.SourceItemCode;
            row["MSCard1"] = hlInfo.MSourceCard;
            row["Location1"] = hlInfo.Location;
            row["LotNO2"] = hlInfo.LotNO;
            row["VendorCode"] = hlInfo.VendorCode;
            row["VendorItemCode"] = hlInfo.VendorItemCode;
            row["DateCode"] = hlInfo.DateCode;
            row["Version"] = hlInfo.Version;
            row["PCBA"] = hlInfo.PCBA;
            row["BIOS"] = hlInfo.BIOS;
            row["Memo"] = hlInfo.Memo;
            return row;
		}

		protected override object[] LoadDataSource3(int inclusive, int exclusive)
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
			return this._facade.QueryHLInfo(
				this.txtSN.Value ,
				seq,
				1, int.MaxValue );
		}

		#endregion

        #region Export 	
        protected override string[] FormatExportRecord( object obj )
        {
            return  new string[]{
                                    ((TSOPResult)obj).RouteCode.ToString(),
                                    ((TSOPResult)obj).OPCode.ToString(),
                                    this.languageComponent1.GetString(((TSOPResult)obj).ItemStatus.ToString()),
                                    this.languageComponent1.GetString( ((TSOPResult)obj).OPType.ToString()),
                                    ((TSOPResult)obj).SegmentCode.ToString(),
                                    ((TSOPResult)obj).LineCode.ToString(),
                                    ((TSOPResult)obj).ResCode.ToString(),
                                    FormatHelper.ToDateString(((TSOPResult)obj).MaintainDate),
                                    FormatHelper.ToTimeString(((TSOPResult)obj).MaintainTime),
                                    ((TSOPResult)obj).GetDisplayText("MaintainUser"),
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

            //string opType = e.Cell.Row.Cells[11].Text ;
            string opType = row.Items.FindItemByKey(" IT_OPType").Text;
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
            Response.Redirect("FItemTracingQP.aspx") ;
        }
	}
}
