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
using BenQGuru.eMES.Common   ;



namespace BenQGuru.eMES.Web.Rework
{
	/// <summary>
	/// FReworkSheet2CauseAP 的摘要说明。
	/// </summary>
	public partial class FReworkSheet2CauseAP : BaseAPage
	{


        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        private BenQGuru.eMES.Rework.ReworkFacade _facade ;//= ReworkFacadeFactory.Create();

        #region Stable
        protected void Page_Load(object sender, System.EventArgs e)
        {
			
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}

            if( this.GetRequestParam("ReworkSheetCode") == string.Empty)
            {
                ExceptionManager.Raise( this.GetType() , "$Error_RequestUrlParameter_Lost"); 
            }
            
            this.txtReworkCode.Text = this.GetRequestParam("ReworkSheetCode");
		}
		
		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			 return this.languageComponent1;
		}
        #endregion

        #region Not Stable
        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn( "ReworkCauseCode", "返工原因代码",	null);
			this.gridHelper.AddColumn( "ReworkCauseDesc", "返工原因描述",	null);

            this.gridHelper.AddDefaultColumn( true, false );
            
            this.gridHelper.ApplyLanguage( this.languageComponent1 );
        }

		protected override void AddDomainObject(ArrayList domainObject)
		{
			if(_facade==null)
			{
				_facade = new ReworkFacadeFactory(base.DataProvider).Create();
			}
			_facade.AddReworkSheet2Cause( (ReworkSheet2Cause[])domainObject.ToArray(typeof(ReworkSheet2Cause)));
		}

        protected override int GetRowCount()
        {			
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            return this._facade.GetUnselectedReworkCauseByReworkCodeCount( FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkCode.Text)) , FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkCauseCode.Text)));
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
                new object[]{"false",
                                ((ReworkCause)obj).ReworkCauseCode,
								((ReworkCause)obj).Description

            });
        }

        protected override object GetEditObject(UltraGridRow row)
        {
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            ReworkSheet2Cause relation = this._facade.CreateNewReworkSheet2Cause() ;
            relation.ReworkCode = this.txtReworkCode.Text.Trim() ;
            relation.ReworkCauseCode = row.Cells[1].Text ;
            relation.MaintainUser = this.GetUserCode() ;
            
            return relation ;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {			
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            return _facade.GetUnselectedReworkCauseByReworkCode( 
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReworkCode.Text )),
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReworkCauseCode.Text )),
                inclusive,exclusive);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
			this.Response.Redirect(this.MakeRedirectUrl("./FReworkSheet2CauseSP.aspx", new string[]{"ReworkSheetCode"}, new string[]{FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReworkCode.Text ))}) );
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


    }
}
