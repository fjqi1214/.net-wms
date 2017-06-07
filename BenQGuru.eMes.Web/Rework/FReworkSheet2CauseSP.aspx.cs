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
	/// FReworkSheet2CauseSP 的摘要说明。
	/// </summary>
	public partial class FReworkSheet2CauseSP : BaseSPage
	{
    

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private BenQGuru.eMES.Rework.ReworkFacade _facade ;//= ReworkFacadeFactory.Create();

        #region Stable
        protected void Page_Load(object sender, System.EventArgs e)
        {
			if(!Page.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
				if( this.GetRequestParam("ReworkSheetCode") == string.Empty)
				{
					ExceptionManager.Raise( this.GetType() , "$Error_RequestUrlParameter_Lost"); 
				}

				ReworkSheet rs = (ReworkSheet) this._facade.GetReworkSheet( this.GetRequestParam("ReworkSheetCode") ) ;
				if(rs == null)
				{
					ExceptionManager.Raise( this.GetType() , "$Error_ReworkCode_Invalid"); 
				}
				if(rs.Status != ReworkStatus.REWORKSTATUS_NEW)
				{
					cmdDelete.Disabled = true ;
					cmdAdd.Visible = false ;
				}
				else
				{
					cmdDelete.Disabled = false ;
					cmdAdd.Visible = true ;
				}


				this.txtReworkSheetCode.Text = this.GetRequestParam("ReworkSheetCode");
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
            this.gridHelper.AddColumn( "ReworkCauseCode", "返工原因代码",	null);
			this.gridHelper.AddColumn( "ReworkCauseDesc", "返工原因描述",	null);

            this.gridHelper.AddDefaultColumn( true, false );
            
            this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridHelper.RequestData();
        }

        protected override object GetEditObject(UltraGridRow row)
        {
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            ReworkSheet2Cause relation = this._facade.CreateNewReworkSheet2Cause() ;
            relation.ReworkCode = this.txtReworkSheetCode.Text.Trim() ;
            relation.ReworkCauseCode = row.Cells[1].Text ;
            relation.MaintainUser = this.GetUserCode() ;
            
            return relation ;
        }

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
			_facade.DeleteReworkSheet2Cause( (ReworkSheet2Cause[])domainObjects.ToArray(typeof(ReworkSheet2Cause)));
		}

        protected override int GetRowCount()
        {			
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            return this._facade.GetSelectedReworkCauseByReworkCodeCount( FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReworkSheetCode.Text )), FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReworkCauseCodeQuery.Text )) );
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
                new object[]{"false",
                                ((ReworkCause)obj).ReworkCauseCode,
								((ReworkCause)obj).Description
                            }
                );
         }

		#region Export
		// 2005-04-06
		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{  ((ReworkCause)obj).ReworkCauseCode,((ReworkCause)obj).Description };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"ReworkCauseCode","ReworkCauseDesc" };
		}

		#endregion

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {			
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            return this._facade.GetSelectedReworkCauseByReworkCode(
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReworkSheetCode.Text )) ,
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReworkCauseCodeQuery.Text ) ),
                inclusive,exclusive  );
        }

        protected void cmdSelect_ServerClick(object sender, System.EventArgs e) 
        {
			this.Response.Redirect(this.MakeRedirectUrl("./FReworkSheet2CauseAP.aspx", new string[]{"ReworkSheetCode"}, new string[]{FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkSheetCode.Text ))}));
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
    }
}
