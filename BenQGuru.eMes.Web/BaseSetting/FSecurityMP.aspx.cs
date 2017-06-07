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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FSecurityMP 的摘要说明。
	/// </summary>
	public partial class FSecurityMP : BaseMPageMinus
	{
		protected System.Web.UI.WebControls.CheckBox chbSelectAll;
        //private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Label lblSecurityTitle;

		private BenQGuru.eMES.BaseSetting.UserFacade _facade = null ; //new SystemSettingFacadeFactory().CreateUserFacade();
	
		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.InitOnPostBack();

			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.InitWebGrid();
			}
		}

		private void InitOnPostBack()
		{		
			this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}
		#endregion

		#region WebGrid
		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "UserGroupCode", "用户组代码",	null);
			this.gridHelper.AddColumn( "UserGroupType", "用户组类别",	null);
			this.gridHelper.AddColumn( "UserGroupDescription", "用户组描述",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
			this.gridHelper.AddLinkColumn( "SelectFunctionGroup","选择功能组",null);
			//this.gridHelper.AddLinkColumn( "SelectResource","选择资源",null);

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected DataRow GetGridRow(object obj)
		{
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    ((UserGroup)obj).UserGroupCode.ToString(),
            //                    ((UserGroup)obj).UserGroupType.ToString(),
            //                    ((UserGroup)obj).UserGroupDescription.ToString(),
            //                    //((UserGroup)obj).MaintainUser.ToString(),
            //          ((UserGroup)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((UserGroup)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((UserGroup)obj).MaintainTime),
            //                    "",""});
            DataRow row = this.DtSource.NewRow();
            row["UserGroupCode"] = ((UserGroup)obj).UserGroupCode.ToString();
            row["UserGroupType"] = ((UserGroup)obj).UserGroupType.ToString();
            row["UserGroupDescription"] = ((UserGroup)obj).UserGroupDescription.ToString();
            row["MaintainUser"] = ((UserGroup)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((UserGroup)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((UserGroup)obj).MaintainTime);
            return row;

		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade == null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			return this._facade.QueryUserGroup( 
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
					inclusive, exclusive );
		}


		private int GetRowCount()
		{
			if(_facade == null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			return this._facade.QueryUserGroupCount( 
					FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)));
		}

		#endregion

		#region Button

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			this.RequestData();	
		}

		private void RequestData()
		{
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}

        protected override void Grid_ClickCell(GridRecord row, string commandName)
		{
            if (commandName=="SelectFunctionGroup")
			{
				if( string.Compare( row.Items.FindItemByKey("UserGroupType").Text,"ADMIN",true)==0 )
				{
					return ;
				}
                this.Response.Redirect(this.MakeRedirectUrl("./FUserGroup2FunctionGroupSP.aspx", new string[] { "usergroupcode" }, new string[] { row.Items.FindItemByKey("UserGroupCode").Text.Trim() }));

			}
			if( commandName== "SelectResource" )
			{
                if (string.Compare(row.Items.FindItemByKey("UserGroupType").Text, "ADMIN", true) == 0)
				{
					return ;
				}
                this.Response.Redirect(this.MakeRedirectUrl("./FUserGroup2ResourceSP.aspx", new string[] { "usergroupcode" }, new string[] { row.Items.FindItemByKey("UserGroupCode").Text.Trim() }));
			}
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
	}
}
