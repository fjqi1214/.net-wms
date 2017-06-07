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
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Domain.MOModel ;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FItemTracingQP 的摘要说明。
	/// </summary>
	public partial class FITOPResultBurnInQP : BaseMPage
	{
    

        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private QueryFacade2 _facade = null ;// FacadeFactory.CreateQueryFacade2() ;

        protected GridHelper gridBurnInHelper ;

        private string[] caredActions = new string[]
            {
                ActionType.DataCollectAction_BurnIn,
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
			this.gridBurnIn.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridBurnIn_ClickCellButton);
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


            this.gridBurnInHelper = new GridHelper(this.gridBurnIn) ;
            this.gridBurnInHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSourceBurnIn);
            //this.gridBurnInHelper.GetRowCountHandle = new GetRowCountDelegate(this.GetRowCountBurnIn);
            this.gridBurnInHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRowBurnIn);	
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("IT_Route","生产途程",null) ;
            this.gridHelper.AddColumn( "IT_OP", "工序",	null);
            this.gridHelper.AddColumn( "IT_ItemStatus", "产品状态",	null);
            this.gridHelper.AddColumn( "IT_OPType", "工序结果",	null);
            this.gridHelper.AddColumn( "IT_Segment", "工段",	null);
            this.gridHelper.AddColumn( "IT_Line", "生产线",	null);
            this.gridHelper.AddColumn( "IT_Resource", "资源",	null);
            this.gridHelper.AddColumn( "IT_MaintainDate", "日期",	null);
            this.gridHelper.AddColumn( "IT_MaintainTime", "时间",	null);
            this.gridHelper.AddColumn( "IT_MaintainUser", "操作工",	null);
			this.gridHelper.AddColumn( "ShelfPK", "ShelfPK",	null);
			this.gridHelper.Grid.Columns.FromKey("ShelfPK").Hidden = true;

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
 
            base.InitWebGrid();

            this.gridHelper.RequestData();

			InitWebGridBurnIn() ;

        }
		
        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
                new object[]{
                                ((OPResult)obj).RouteCode.ToString(),
                                ((OPResult)obj).OPCode.ToString(),
                                this.languageComponent1.GetString( ((OPResult)obj).ItemStatus ),
								this.languageComponent1.GetString(this.GetRequestParam("TYPE").ToString()),
                                ((OPResult)obj).SegmentCode.ToString(),
                                ((OPResult)obj).LineCode.ToString(),
                                ((OPResult)obj).ResCode.ToString(),
                                FormatHelper.ToDateString(((OPResult)obj).MaintainDate),
                                FormatHelper.ToTimeString(((OPResult)obj).MaintainTime),
                                ((OPResult)obj).MaintainUser.ToString(),
								((OPResult)obj).ShelfPK.ToString()
                            }
                
                
                );
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

        #endregion
        

		#region BurnInGrid
        protected void InitWebGridBurnIn()
        {
			/* 车号，BurnIn日期、时间，BurnIn人员，装车数量，产品序列号明细 */
			this.gridBurnInHelper.AddColumn( "ShelfPK","ShelfPK",null) ;
            this.gridBurnInHelper.AddColumn( "ShelfNO","车号",null) ;
            this.gridBurnInHelper.AddColumn( "BurnInDate", "BurnIn日期",	null);
			this.gridBurnInHelper.AddColumn( "BurnInTime", "BurnIn时间",	null);
			this.gridBurnInHelper.AddColumn( "BurnInUser", "BurnIn人员",	null);
			this.gridBurnInHelper.AddColumn( "BurnInTP", "BurnIn时长(hour)",	null);
			this.gridBurnInHelper.AddColumn( "BurnOutDate", "BurnOut日期",	null);
			this.gridBurnInHelper.AddColumn( "BurnOutTime", "BurnOut时间",	null);
			this.gridBurnInHelper.AddColumn( "BurnOutUser", "BurnOut人员",	null);
			this.gridBurnInHelper.AddColumn( "BurnInOutVolumn", "装车数量",	null);
			this.gridBurnInHelper.AddLinkColumn( "RCardList", "产品序列号明细",	null);

			this.gridBurnInHelper.Grid.Columns.FromKey("ShelfPK").Hidden = true;

            this.gridBurnInHelper.ApplyLanguage( this.languageComponent1 );
 
            this.gridBurnInHelper.RequestData();

        }
		
        protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRowBurnIn(object obj)
        {
			ShelfActionList shelfal = obj as ShelfActionList;
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
                new object[]{
								shelfal.PKID.ToString(),
                                shelfal.ShelfNO.ToString(),
								FormatHelper.ToDateString( shelfal.BurnInDate ),
								FormatHelper.ToTimeString( shelfal.BurnInTime ),
								shelfal.BurnInUser.ToString(),
								shelfal.BurnInTimePeriod.ToString(),
								FormatHelper.ToDateString( shelfal.BurnOutDate ),
								FormatHelper.ToTimeString( shelfal.BurnOutTIme ),
								shelfal.BurnOutUser.ToString(),
								shelfal.eAttribute1.ToString(),
								""
                            }
                
                
                );
        }

        protected object[] LoadDataSourceBurnIn(int inclusive, int exclusive)
        {
			ShelfFacade shelfFacade = new ShelfFacade( base.DataProvider );
			
			string shelfpk = this.gridHelper.Grid.Rows[0].Cells.FromKey("ShelfPK").Value.ToString();

			object[] objs = null;
			object obj = shelfFacade.GetShelfActionList( shelfpk );
			if(obj!=null)
			{
				objs = new object[]{obj};
			}

			return objs;
        }

        #endregion


        #region Export 	
        protected override string[] FormatExportRecord( object obj )
        {
            return  new string[]{
                                    ((OPResult)obj).RouteCode.ToString(),
                                    ((OPResult)obj).OPCode.ToString(),
                                    this.languageComponent1.GetString( ((OPResult)obj).ItemStatus ),
									this.languageComponent1.GetString(this.GetRequestParam("TYPE").ToString()),
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

		private void gridBurnIn_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if(this.gridBurnInHelper.IsClickColumn("RCardList",e.Cell))
			{
				string shelfpk = e.Cell.Row.Cells.FromKey("ShelfPK").Value.ToString().Trim();
				if(shelfpk.Length>0)
				{
					int length = this.Request.QueryString.AllKeys.Length;
					string[] keys = this.Request.QueryString.AllKeys;
					string[] values = new string[length];

					for(int i=0;i<length;i++)
					{
						values[i] = System.Web.HttpUtility.UrlEncode(this.Request.QueryString[i].ToString());
					}
					string referUrl = this.MakeRedirectUrl("FITOPResultBurnInQP.aspx",keys,values);

					string url = this.MakeRedirectUrl("FITShelfDetailQP.aspx",new string[]{"ShelfPK","REFEREDURL"},new string[]{shelfpk, referUrl});
					Response.Redirect(url);
				}
			}
		}
	}
}
