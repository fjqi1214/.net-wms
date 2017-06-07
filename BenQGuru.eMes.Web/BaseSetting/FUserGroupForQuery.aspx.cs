using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Security;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FUserGroupForQuery 的摘要说明。
	/// </summary>
	public partial class FUserGroupForQuery : BaseMPage
	{
		protected BenQGuru.eMES.Web.Helper.PagerSizeSelector pagerSizeSelector;
		protected BenQGuru.eMES.Web.Helper.PagerToolBar pagerToolBar;

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		private BenQGuru.eMES.BaseSetting.UserFacade _facade = null ;
		protected GridHelper _gridHelper = null;
		protected string userCode = "";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			userCode = this.GetRequestParam("UserCode");
			this.txtUserCode.Text = userCode;
			this.txtUserCode.Enabled = false;

			this._gridHelper = new GridHelper(this.gridWebGrid);

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialGrid();
				this._processDataDourceToGrid( this.LoadDataSource() );
			}
		}

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
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion

		private void _initialGrid()
		{
			this.gridWebGrid.Columns.Clear();

			this._gridHelper.AddColumn( "UserGroupCode", "用户组代码", null);
			this._gridHelper.AddColumn( "MaintainDate", "维护日期", null);
			this._gridHelper.AddColumn( "MaintainTime", "维护时间", null);
			this._gridHelper.AddColumn( "UserGroupDescription", "用户组描述", null);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if( _facade==null )
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade();
			}
			return this._facade.GetAllUserGroup( this.txtUserCode.Text );
		}

		private void _processDataDourceToGrid(object[] source)
		{
			this._initialGrid();

			this.gridWebGrid.Rows.Clear();

			if( source != null )
			{
				foreach(UserGroup real in source)
				{
					UltraGridRow gridRow = null;
					object[] objs = new object[this.gridWebGrid.Columns.Count];
					gridRow = new UltraGridRow( objs );
					this.gridWebGrid.Rows.Add( gridRow );

					gridRow.Cells.FromKey("UserGroupCode").Text = real.UserGroupCode;
					gridRow.Cells.FromKey("MaintainDate").Text = real.MaintainDate.ToString();
					gridRow.Cells.FromKey("MaintainTime").Text = real.MaintainTime.ToString();
					gridRow.Cells.FromKey("UserGroupDescription").Text = real.UserGroupDescription;
				}
			}
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect("FUserMP.aspx");
		}

		#region Export

		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{ ((UserGroup)obj).UserGroupCode.ToString(),
								   ((UserGroup)obj).MaintainDate.ToString(),
								   ((UserGroup)obj).MaintainTime.ToString(),
								   ((UserGroup)obj).UserGroupDescription.ToString() };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	this.languageComponent1.GetString("UserGroupCode"),
									this.languageComponent1.GetString("MaintainDate"),
									this.languageComponent1.GetString("MaintainTime"),
									this.languageComponent1.GetString("UserGroupDescription") };
		}

		#endregion
	}
}
