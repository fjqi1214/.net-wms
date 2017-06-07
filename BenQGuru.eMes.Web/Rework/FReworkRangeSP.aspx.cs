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

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Common ;


namespace BenQGuru.eMES.Web.Rework
{
	/// <summary>
	/// FReworkRangeSP 的摘要说明。
	/// </summary>
	public partial class FReworkRangeSP : BaseSPage
	{

        public BenQGuru.eMES.Web.UserControl.eMESDate dateFrom ;
        public BenQGuru.eMES.Web.UserControl.eMESTime timeFrom ;

        public BenQGuru.eMES.Web.UserControl.eMESDate dateTo ;
        public BenQGuru.eMES.Web.UserControl.eMESTime timeTo ;

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private BenQGuru.eMES.Rework.ReworkFacade _facade ;//= ReworkFacadeFactory.Create();

        #region Stable
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if( this.GetRequestParam("ReworkSheetCode") == string.Empty)
            {
                ExceptionManager.Raise( this.GetType() , "$Error_RequestUrlParameter_Lost"); 
            }
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            ReworkSheet rs = (ReworkSheet) this._facade.GetReworkSheet( this.GetRequestParam("ReworkSheetCode") ) ;
            if(rs == null)
            {
                ExceptionManager.Raise( this.GetType() , "$Error_ReworkCode_Invalid"); 
            }
            if(rs.Status != ReworkStatus.REWORKSTATUS_NEW)
            {
                cmdDelete.Disabled = true ;
                cmdAppend.Visible = false ;
            }
            else
            {
                cmdDelete.Disabled = false ;
                cmdAppend.Visible = true ;
            }



            if( ! this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

                this.txtReworkSheetCode.Text = this.GetRequestParam("ReworkSheetCode");
                //this.dateFrom.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now )) ;
                //this.dateTo.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now )) ;
                this.timeFrom.Text = FormatHelper.ToTimeString( 0 ) ;
                this.timeTo.Text = FormatHelper.ToTimeString( FormatHelper.TOTimeInt( DateTime.Now ) ) ;
            }
            

        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region Not Stable
        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn( "RunningCard" , "序列号" , null ) ;
            this.gridHelper.AddColumn( "RunningCardSequence", "顺序号",	null);
            this.gridHelper.AddColumn( "LOTNO", "批号",	null);
            this.gridHelper.AddColumn( "MOCode", "工单",	null);
            this.gridHelper.AddColumn( "ItemCode", "产品",	null);
            this.gridHelper.AddColumn( "ModelCode", "产品别",	null);
            this.gridHelper.AddColumn( "OPCode", "工位",	null);
            this.gridHelper.AddColumn( "RejectDate", "日期",	null);
            this.gridHelper.AddColumn( "RejectTime", "时间",	null);
            this.gridHelper.AddColumn( "RejectUser", "人员",	null);
            this.gridHelper.AddColumn( "ErrorCode", "不良信息",	null);

            this.gridHelper.AddDefaultColumn( true, false );
            
            this.gridHelper.ApplyLanguage( this.languageComponent1 );
            this.gridHelper.Grid.Columns.FromKey("RunningCardSequence").Hidden = true ;

			this.gridHelper.RequestData();

        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            _facade.DeleteReworkRange( (ReworkRange[])domainObjects.ToArray(typeof(ReworkRange)));
        }

        protected override int GetRowCount()
        {			
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            return this._facade.GetSelectedRejectByReworkSheetCount(
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReworkSheetCode.Text )),
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtMOCodeQuery.Text )) , 
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtLotNoQuery.Text )) ,
                this.drpOPCodeQuery.SelectedValue ,
                this.dateFrom.Text ,
                this.timeFrom.Text ,
                this.dateTo.Text , 
                this.timeTo.Text );
        }

        protected override object GetEditObject(UltraGridRow row)
        {
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            ReworkRange reworkRange = this._facade.CreateNewReworkRange() ;
            reworkRange.ReworkCode = this.txtReworkSheetCode.Text ;
            reworkRange.RunningCard = GetText(row.Cells[1].Text) ;
            reworkRange.MaintainUser = this.GetUserCode() ;
            try
            {
                reworkRange.RunningCardSequence = decimal.Parse( row.Cells[2].Text ) ;
            }
            catch
            {
                ExceptionManager.Raise(this.GetType() , "$Error_Format") ;
//                throw new Exception(ErrorCenter.GetErrorUserDescription( this.GetType().BaseType, string.Format(ErrorCenter.ERROR_FORMAT, "RunningCardSequence") ) ); 
            }
            return reworkRange;
        }



        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {


            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
                new object[]{"false",
                                GetRCardLink(((BenQGuru.eMES.Rework.RejectEx)obj).RunningCard.ToString()) ,
                                ((RejectEx)obj).RunningCardSequence.ToString() ,
                                GetLotNoLink(((RejectEx)obj).LOTNO) ,
                                ((RejectEx)obj).MOCode ,
                                ((RejectEx)obj).ItemCode ,
                                ((RejectEx)obj).ModelCode ,
                                ((RejectEx)obj).OPCode ,
                                FormatHelper.ToDateString(((RejectEx)obj).RejectDate) ,
                                FormatHelper.ToTimeString(((RejectEx)obj).RejectTime) ,
                                ((RejectEx)obj).RejectUser ,
                                ((RejectEx)obj).ErrorCode        
                            }
                );
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {			
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            return this._facade.GetSelectedRejectExByReworkSheet(
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReworkSheetCode.Text )),
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtMOCodeQuery.Text )) , 
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtLotNoQuery.Text )) ,
                this.drpOPCodeQuery.SelectedValue ,
                this.dateFrom.Text ,
                this.timeFrom.Text ,
                this.dateTo.Text , 
                this.timeTo.Text ,
                inclusive,exclusive  );
        }

        protected void cmdSelect_ServerClick(object sender, System.EventArgs e) 
        {
			this.Response.Redirect(this.MakeRedirectUrl("./FReworkRangeAP.aspx", new string[]{"ReworkSheetCode"}, new string[]{FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkSheetCode.Text ))}));
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FReworkSheetMP.aspx"));
        }

        #endregion

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


        protected void drpOpCodeQuery_Load(object sender , System.EventArgs e)
        {
            if(!this.IsPostBack)
            {
                object[] ops = new ReworkFacadeFactory(base.DataProvider).CreateBaseModelFacade().GetAllOperation() ;
                foreach(BenQGuru.eMES.Domain.BaseSetting.Operation op in ops)
                {
                    this.drpOPCodeQuery.Items.Add( op.OPCode ) ;
                }

                new DropDownListBuilder( this.drpOPCodeQuery ).AddAllItem( this.languageComponent1 ) ;
            }
        }

		private string GetRCardLink(string no)
		{
			return string.Format("<a href=../WebQuery/FOQCLotSampleQP.aspx?reworkrcard={0}>{1}</a>",no,no ) ;
		}
		private string GetLotNoLink(string no)
		{
			return string.Format("<a href=../WebQuery/FOQCSampleNGDetailQP.aspx?LotNo={0}&BackUrl=../Rework/FReworkRangeSP.aspx?ReworkSheetCode={2}>{1}</a>",no,no,this.GetRequestParam("ReworkSheetCode") ) ;
		}

		private string GetText(string html)
		{
			int s = html.IndexOf(">",0);
			int e = html.IndexOf("</a>",0);

			string str = html.Substring(s + 1,e-s-1);
			return str;
		}
	}
}
