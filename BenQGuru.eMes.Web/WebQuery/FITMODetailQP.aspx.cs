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
	public partial class FITMODetailQP : BaseMPageNew
	{
    

        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private QueryFacade2 _facade = null ; //FacadeFactory.CreateQueryFacade2() ;



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
            this.txtSN.Value = ((ProductionProcess)obj).RCard ; 
            
			if(!Page.IsPostBack)
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
            base.InitWebGrid();
            this.gridHelper.AddLinkColumn("IT_ProductionProcess","生产过程",null) ;
            this.gridHelper.AddColumn( "IT_SN", "序列号",	null);
            this.gridHelper.AddColumn( "IT_ItemStatus", "产品状态",	null);
            this.gridHelper.AddColumn( "IT_OP", "工序",	null);
            this.gridHelper.AddColumn( "IT_OPType", "工序结果",	null);
            this.gridHelper.AddColumn( "IT_OPResult", "工序结果",	null);
            this.gridHelper.AddColumn( "IT_Segment", "工段",	null);
            this.gridHelper.AddColumn( "IT_Line", "生产线",	null);
            this.gridHelper.AddColumn( "IT_Resource", "资源",	null);
            this.gridHelper.AddColumn( "IT_MaintainDate", "日期",	null);
            this.gridHelper.AddColumn( "IT_MaintainTime", "时间",	null);
            this.gridHelper.AddColumn( "IT_MaintainUser", "操作工",	null);

            this.gridHelper.AddColumn( "IT_OPType_ORI", "工序结果",	null);
			this.gridHelper.AddColumn( "IT_TCARD", "转换前序列号",	null);

            ((BoundDataField)this.gridHelper.Grid.Columns.FromKey("IT_OPType")).HtmlEncode = false; 
            this.gridHelper.Grid.Columns.FromKey("IT_OPType_ORI").Hidden = true ;
            this.gridHelper.Grid.Columns.FromKey("IT_OPResult").Hidden = true ;
			this.gridHelper.Grid.Columns.FromKey("IT_TCARD").Hidden = true ;

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
 
            base.InitWebGrid();

            this.gridHelper.RequestData();
            this.cmdQuery.Visible = false ;

        }
		
        protected override DataRow GetGridRow(object obj)
        {
            decimal seq ;
            try
            {
                seq = decimal.Parse( this.GetRequestParam("RCARDSEQ") ) ;
            }
            catch
            {
                seq = -1 ;
            }


            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    string.Empty,
            //                    ((ItemTracing)obj).RCard,
            //                    this.languageComponent1.GetString( ((ItemTracing)obj).ItemStatus ),
            //                    ((ItemTracing)obj).OPCode.ToString(),
            //                    WebQueryHelper.GetOPResultLinkHtml2(
            //                        this.languageComponent1,
            //                        ((ItemTracing)obj).LastAction,
            //                        ((ItemTracing)obj).RCard,
            //                        ((ItemTracing)obj).RCardSeq ,
            //                        this.txtMO.Value,
            //                        string.Format(
            //                            "FITMODetailQP.aspx?RCARD={0}&RCARDSEQ={1}&MOCODE={2}",
            //                            ((ItemTracing)obj).RCard,
            //                            ((ItemTracing)obj).RCardSeq,
            //                            this.txtMO.Value
            //                        )
            //                    ),
            //                    "",
            //                    ((ItemTracing)obj).SegmentCode.ToString(),
            //                    ((ItemTracing)obj).LineCode.ToString(),
            //                    ((ItemTracing)obj).ResCode.ToString(),
            //                    FormatHelper.ToDateString(((ItemTracing)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((ItemTracing)obj).MaintainTime),
            //                    ((ItemTracing)obj).MaintainUser.ToString(),
            //                    ((ItemTracing)obj).OPType.ToString(),
            //                    ((ItemTracing)obj).TCard
            //                }
            //    );

            DataRow row = this.DtSource.NewRow();
            row["IT_ProductionProcess"] = string.Empty;
            row["IT_SN"] = ((ItemTracing)obj).RCard;
            row["IT_ItemStatus"] = this.languageComponent1.GetString(((ItemTracing)obj).ItemStatus);
            row["IT_OP"] = ((ItemTracing)obj).OPCode.ToString();
            row["IT_OPType"] = WebQueryHelper.GetOPResultLinkHtml2(
                                    this.languageComponent1,
                                    ((ItemTracing)obj).LastAction,
                                    ((ItemTracing)obj).RCard,
                                    ((ItemTracing)obj).RCardSeq,
                                    this.txtMO.Value,
                                    string.Format(
                                        "FITMODetailQP.aspx?RCARD={0}&RCARDSEQ={1}&MOCODE={2}",
                                        ((ItemTracing)obj).RCard,
                                        ((ItemTracing)obj).RCardSeq,
                                        this.txtMO.Value
                                    )
                                );
            row["IT_Segment"] = ((ItemTracing)obj).SegmentCode.ToString();
            row["IT_Line"] = ((ItemTracing)obj).LineCode.ToString();
            row["IT_Resource"] = ((ItemTracing)obj).ResCode.ToString();
            row["IT_MaintainDate"] = FormatHelper.ToDateString(((ItemTracing)obj).MaintainDate);
            row["IT_MaintainTime"] = FormatHelper.ToTimeString(((ItemTracing)obj).MaintainTime);
            row["IT_MaintainUser"] = ((ItemTracing)obj).MaintainUser.ToString();
            row["IT_OPType_ORI"] = ((ItemTracing)obj).OPType.ToString();
            row["IT_TCARD"] = ((ItemTracing)obj).TCard;
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
			}
            return this._facade.QueryItemTracing(
                string.Empty,
                string.Empty,
                this.txtMO.Value,
                inclusive, exclusive );
        }


        protected override int GetRowCount()
        {
			if(_facade==null)
			{
				_facade = new FacadeFactory(base.DataProvider).CreateQueryFacade2() ;
			}
            return this._facade.QueryItemTracingCount(
                string.Empty,
                string.Empty,
                this.txtMO.Value
                );
        }

        #endregion
        
        #region Export 	
        protected override string[] FormatExportRecord( object obj )
        {
            return  new string[]{
                                    ((ItemTracing)obj).RCard,
                                    this.languageComponent1.GetString( ((ItemTracing)obj).ItemStatus ),
                                    ((ItemTracing)obj).OPCode.ToString(),
                                    WebQueryHelper.GetOPResult(
                                        this.languageComponent1,
                                        ((ItemTracing)obj).LastAction),
                                    ((ItemTracing)obj).SegmentCode.ToString(),
                                    ((ItemTracing)obj).LineCode.ToString(),
                                    ((ItemTracing)obj).ResCode.ToString(),
                                    FormatHelper.ToDateString(((ItemTracing)obj).MaintainDate),
                                    FormatHelper.ToTimeString(((ItemTracing)obj).MaintainTime),
                                    ((ItemTracing)obj).MaintainUser.ToString(),
                                }
                ;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {
                                    "IT_SN", 
                                    "IT_ItemStatus", 
                                    "IT_OP", 
                                    "IT_OPType", 
                                    "IT_Segment", 
                                    "IT_Line", 
                                    "IT_Resource", 
                                    "IT_MaintainDate", 
                                    "IT_MaintainTime", 
                                    "IT_MaintainUser"
                                } ;
        }
        #endregion

        protected override void Grid_ClickCell(GridRecord row, string command)
        {
            string moCode = this.txtMO.Value ;
            string sn = row.Items.FindItemByKey("IT_SN").Text;
            string opType = row.Items.FindItemByKey("IT_OPType_ORI").Text;
            string tcard = row.Items.FindItemByKey("IT_TCARD").Text;

            if(command=="IT_ProductionProcess")
            {
                Response.Redirect(
				this.MakeRedirectUrl( "FITProductionProcessQP.aspx", 
					new string[]{"MOCODE","RCARD","TCARD"},
					new string[]{moCode,sn,tcard})); 
            }
            else if(command=="IT_OPResult")
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
                
                Response.Redirect( string.Format("{0}?RCARD={1}" , gotoURL ,  sn) );
            }

        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect("FITMOInfoQP.aspx?RCARD=" + this.txtSN.Value) ;
        }

        private void SetParam(string key,string pValue)
        {
            ViewState.Add(key,pValue) ;
        }

        private string GetParam(string key)
        {
            if(ViewState[key] == null)
            {
                return string.Empty ;
            }
            else
            {
                return ViewState[key].ToString() ;
            }
        }


	}
}
