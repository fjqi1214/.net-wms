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
    public partial class FITOPResultComploadingQP : BaseMPageNew
	{
    

        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private QueryFacade2 _facade = null ; //FacadeFactory.CreateQueryFacade2() ;

        protected GridHelper gridLotHelper ;
        protected GridHelper gridSingleHelper ;

        private string[] caredActions = new string[]
            {
                ActionType.DataCollectAction_CollectINNO,
                ActionType.DataCollectAction_CollectKeyParts
            } ;


        string rawURL = string.Empty ;

        


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
            //this.gridSingleItem.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridSingleItem_ClickCellButton);
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
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper2 = new GridHelperNew(this.gridWebGrid2, this.DtSource2);
            this.gridHelper3 = new GridHelperNew(this.gridWebGrid3, this.DtSource3);

            try
            {
                string[] urls = this.Request.RawUrl.Split( new char[]{'/'} ) ;
                this.rawURL = urls[ urls.Length-1 ] ;
            }
            catch
            {
                this.rawURL = string.Empty  ;
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

            //this.gridSingleHelper = new GridHelper(this.gridSingleItem) ;
            //this.gridSingleHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSourceSingle);
            //this.gridSingleHelper.GetRowCountHandle = new GetRowCountDelegate(this.GetRowCountSingle);
            //this.gridSingleHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRowSingle);	

            //this.gridLotHelper = new GridHelper(this.gridLot) ;
            //this.gridLotHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSourceLot);
            //this.gridLotHelper.GetRowCountHandle = new GetRowCountDelegate(this.GetRowCountLot);
            //this.gridLotHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRowLot);	

            //InitWebGridLot() ;
            //InitWebGridSingle() ;

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
 
            base.InitWebGrid();

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
            //                    this.languageComponent1.GetString("ItemTracing_componentloading"),
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
            row["IT_OPType"] = this.languageComponent1.GetString("ItemTracing_componentloading");
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
                this.txtSN.Value ,
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

        #endregion

        #region Grid2
        protected override void InitWebGrid2()
        {
            base.InitWebGrid2();
            this.gridHelper2.AddColumn("IT_PACKEDNO", "最小包装条码", null);
            this.gridHelper2.AddColumn("IT_ItemInFactory", "料号", null);
            this.gridHelper2.AddColumn("IT_Customer", "厂商", null);
            this.gridHelper2.AddColumn("IT_CustomerItem", "厂商料号", null);
            this.gridHelper2.AddColumn("IT_ProductLot", "生产批号", null);
            this.gridHelper2.AddColumn("IT_ProductDate", "生产日期", null);
            this.gridHelper2.AddColumn("IT_SPEC", "物料版本", null);
            this.gridHelper2.AddColumn("IT_PCBAVer", "PCBA版本", null);
            this.gridHelper2.AddColumn("IT_BIOSVer", "BIOS版本", null);

            this.gridHelper2.ApplyLanguage(this.languageComponent1);

            this.gridHelper2.RequestData();

        }
		
        protected override DataRow GetGridRow2(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    ((LotItemInfo)obj).MCard.ToString(),
            //                    ((LotItemInfo)obj).ItemCode.ToString(),
            //                    ((LotItemInfo)obj).Customer.ToString(),
            //                    ((LotItemInfo)obj).CustomerItem.ToString(),
            //                    ((LotItemInfo)obj).LotNO.ToString(),
            //                    ((LotItemInfo)obj).DateCode,
            //                    ((LotItemInfo)obj).Version.ToString(),
            //                    ((LotItemInfo)obj).PCBA.ToString(),
            //                    ((LotItemInfo)obj).BIOS.ToString()
            //                }
            //    );
            DataRow row = this.DtSource2.NewRow();
            row["IT_PACKEDNO"] = ((LotItemInfo)obj).MCard.ToString();
            row["IT_ItemInFactory"] = ((LotItemInfo)obj).ItemCode.ToString();
            row["IT_Customer"] = ((LotItemInfo)obj).Customer.ToString();
            row["IT_CustomerItem"] = ((LotItemInfo)obj).CustomerItem.ToString();
            row["IT_ProductLot"] = ((LotItemInfo)obj).LotNO.ToString();
            row["IT_ProductDate"] = ((LotItemInfo)obj).DateCode;
            row["IT_SPEC"] = ((LotItemInfo)obj).Version.ToString();
            row["IT_PCBAVer"] = ((LotItemInfo)obj).PCBA.ToString();
            row["IT_BIOSVer"] = ((LotItemInfo)obj).BIOS.ToString();
            return row;
        }

        protected override object[] LoadDataSource2(int inclusive, int exclusive)
        {
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
			}
            return this._facade.QueryLotItemInfo(
                this.txtMO.Value,
                this.txtSN.Value ,
                int.Parse( this.txtSeq.Value) ,
                1, int.MaxValue );
        }

        protected override int GetRowCount2()
        {
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
			}
            return this._facade.QueryLotItemInfoCount(
                this.txtMO.Value,
                this.txtSN.Value
                );
        }

        #endregion

        #region Grid3
        protected override void InitWebGrid3()
        {
            base.InitWebGrid3();
            this.gridHelper3.AddColumn("IT_ItemSN","原料序列号",null) ;
            this.gridHelper3.AddColumn("IT_TracingAble", "可追溯", null);
            this.gridHelper3.AddLinkColumn("IT_Tracing", "追溯", null);
            this.gridHelper3.AddColumn("IT_ItemInFactory", "料号", null);
            this.gridHelper3.AddColumn("IT_Customer", "厂商", null);
            this.gridHelper3.AddColumn("IT_CustomerItem", "厂商料号", null);
            this.gridHelper3.AddColumn("IT_ProductLot", "生产批号", null);
            this.gridHelper3.AddColumn("IT_ProductDate", "生产日期", null);
            this.gridHelper3.AddColumn("IT_SPEC", "物料版本", null);
            this.gridHelper3.AddColumn("IT_PCBAVer", "PCBA版本", null);
            this.gridHelper3.AddColumn("IT_BIOSVer", "BIOS版本", null);


            this.gridHelper3.Grid.Columns.FromKey("IT_TracingAble").Hidden = true;
            this.gridHelper3.Grid.Columns.FromKey("IT_Tracing").Hidden = true;

            this.gridHelper3.ApplyLanguage(this.languageComponent1);

            this.gridHelper3.RequestData();

        }
		
        protected override DataRow GetGridRow3(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    WebQueryHelper.GetProductionProcessHtml(
            //                        ((RunningItemInfo)obj).MCARD.ToString(),
            //                        -1,
            //                        string.Empty,
            //                        this.rawURL ,
            //                        ((RunningItemInfo)obj).CanTracing
            //                    ) ,
            //                    "",
            //                    "",
            //                    ((RunningItemInfo)obj).ItemCode.ToString(),
            //                    ((RunningItemInfo)obj).Customer.ToString(),
            //                    ((RunningItemInfo)obj).CustomerItem.ToString(),
            //                    ((RunningItemInfo)obj).LotNO.ToString(),
            //                    ((RunningItemInfo)obj).DateCode,
            //                    ((RunningItemInfo)obj).Version.ToString(),
            //                    ((RunningItemInfo)obj).PCBA.ToString(),
            //                    ((RunningItemInfo)obj).BIOS.ToString()
            //                }
            //    );
            DataRow row = this.DtSource3.NewRow();
            row["IT_ItemSN"] = WebQueryHelper.GetProductionProcessHtml(
                                    ((RunningItemInfo)obj).MCARD.ToString(),
                                    -1,
                                    string.Empty,
                                    this.rawURL,
                                    ((RunningItemInfo)obj).CanTracing
                                );
            row["IT_TracingAble"] = "";
            row["IT_ItemInFactory"] = ((RunningItemInfo)obj).ItemCode.ToString();
            row["IT_Customer"] = ((RunningItemInfo)obj).Customer.ToString();
            row["IT_CustomerItem"] = ((RunningItemInfo)obj).CustomerItem.ToString();
            row["IT_ProductLot"] = ((RunningItemInfo)obj).LotNO.ToString();
            row["IT_ProductDate"] = ((RunningItemInfo)obj).DateCode;
            row["IT_SPEC"] = ((RunningItemInfo)obj).Version.ToString();
            row["IT_PCBAVer"] = ((RunningItemInfo)obj).PCBA.ToString();
            row["IT_BIOSVer"] = ((RunningItemInfo)obj).BIOS.ToString();
            return row;
        }

        protected override object[] LoadDataSource3(int inclusive, int exclusive)
        {
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
			}
            return this._facade.QueryRunningItemInfo(
                this.txtMO.Value,
                this.txtSN.Value ,
                int.Parse( this.txtSeq.Value) ,
                1, int.MaxValue );
        }

        protected override int GetRowCount3()
        {
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
			}
            return this._facade.QueryRunningItemInfoCount(
                this.txtMO.Value,
                this.txtSN.Value
                );
        }

        #endregion
        
        #region Export 	
        protected override string[] FormatExportRecord( object obj )
        {
            return  new string[]{
                                    ((OPResult)obj).RouteCode.ToString(),
                                    ((OPResult)obj).OPCode.ToString(),
                                    this.languageComponent1.GetString( ((OPResult)obj).ItemStatus ),
                                    //WebQueryHelper.GetOPResultLinkText(this.languageComponent1,((OPResult)obj).OPType),
									this.languageComponent1.GetString("ItemTracing_componentloading"),
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

            //string opType = e.Cell.Row.Cells[11].Text ;
            string opType = row.Items.FindItemByKey(" IT_OPType_ORI").Text;
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

        private void gridSingleItem_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        {
            if( this.gridSingleHelper.IsClickColumn("IT_Tracing",e) )
            {
                Response.Redirect("FItemTracingQP.aspx?RCARDFROM=" + this.txtSN.Value + "&RCARDTO=" + this.txtSN.Value) ;
            }
        }


	}
}
