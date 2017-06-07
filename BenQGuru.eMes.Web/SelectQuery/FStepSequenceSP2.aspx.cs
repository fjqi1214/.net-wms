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
using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Web.UserControl ;

namespace BenQGuru.eMES.Web.SelectQuery
{
	/// <summary>
	/// Selector 的摘要说明。
	/// </summary>
	public partial class FStepSequenceSP2 : BaseSelectorPageNew
	{

        private BenQGuru.eMES.SelectQuery.SPFacade facade ;//= FacadeFactory.CreateSPFacade() ;


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

		}
		#endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
        }

        #endregion

        #region WebGrid
        protected override DataRow GetSelectedGridRow(object obj)
        {
            DataRow row = DtSourceSelected.NewRow();
            row["Selector_SelectedCode"] = ((BenQGuru.eMES.Domain.BaseSetting.StepSequence)obj).StepSequenceCode;
            row["Selector_SelectedDesc"] = ((BenQGuru.eMES.Domain.BaseSetting.StepSequence)obj).StepSequenceDescription;
            return row;
        }

        protected override DataRow GetUnSelectedGridRow(object obj)
        {
            DataRow row = DtSourceUnSelected.NewRow();
            row["Selector_UnselectedCode"] = ((BenQGuru.eMES.Domain.BaseSetting.StepSequence)obj).StepSequenceCode;
            row["Selector_UnSelectedDesc"] = ((BenQGuru.eMES.Domain.BaseSetting.StepSequence)obj).StepSequenceDescription;
            return row;
        }

        protected override object[] LoadSelectedDataSource(int inclusive, int exclusive)
        {
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
            return this.facade.QuerySelectedStepSequence( this.GetSelectedCodes() ) ;
        }

        protected override object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
        {
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
            return this.facade.QueryUnSelectedStepSequenceInSegment( this.txtSegmentCodeQuery.Text ,this.txtStepSequenceCodeQuery.Text, this.GetSelectedCodes(),inclusive,exclusive ) ;
        }


        protected override int GetUnSelectedRowCount()
        {
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateSPFacade() ;}
            return this.facade.QueryUnSelectedStepSequenceInSegmentCount( this.txtSegmentCodeQuery.Text ,this.txtStepSequenceCodeQuery.Text, this.GetSelectedCodes() ) ;
        }

        
        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            this.writerOutted = true ;
            base.Render (writer);
        }

        protected override void cmdInit_ServerClick(object sender, System.EventArgs e)
        {
            this.gridSelectedHelper.RequestData();
            this.gridUnSelectedHelper.RequestData();
            SetSelectedCodes();
            txtSegmentCodeQuery.ReadOnly = (txtSegmentCodeQuery.Text != string.Empty);
        }

        protected override void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            base.cmdQuery_ServerClick(sender, e);
            txtSegmentCodeQuery.ReadOnly = (txtSegmentCodeQuery.Text != string.Empty);
        }


	}
}
