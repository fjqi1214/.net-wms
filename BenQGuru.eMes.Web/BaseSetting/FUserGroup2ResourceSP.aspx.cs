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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;


namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FUserGroup2ResourceSP 的摘要说明。
	/// </summary>
	public partial class FUserGroup2ResourceSP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private UserFacade facade = null ; //new SystemSettingFacadeFactory().CreateUserFacade();
	
		#region Stable
		protected void Page_Load(object sender, System.EventArgs e)
		{		
			//this.pagerSizeSelector.Readonly = true;

			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.txtUserGroupCodeQuery.Text = this.GetRequestParam("usergroupcode");				
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
            base.InitWebGrid();
            this.gridHelper.AddColumn("ResourceCode", "资源代码", null);
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            if (!IsPostBack)
            {
                this.gridHelper.RequestData();
            }
        }

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			facade.DeleteUserGroup2Resource((UserGroup2Resource[])domainObjects.ToArray(typeof(UserGroup2Resource)));
		}

		protected override int GetRowCount()
		{
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			return this.facade.GetSelectedResourceByUserGroupCodeCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeQuery.Text)));
		}

		protected override DataRow GetGridRow(object obj)
		{
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{"false",((Resource)obj).ResourceCode.ToString()});
            DataRow row = this.DtSource.NewRow();
            row["ResourceCode"] = ((Resource)obj).ResourceCode.ToString();
            return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			return facade.GetSelectedResourceByUserGroupCode( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeQuery.Text)),
				inclusive,exclusive);
		}

		protected override object GetEditObject(GridRecord row)
		{
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			UserGroup2Resource relation = facade.CreateNewUserGroup2Resource();

			relation.UserGroupCode = this.txtUserGroupCodeQuery.Text.Trim();
            relation.ResourceCode = row.Items.FindItemByKey("ResourceCode").Text;		
			relation.MaintainUser = this.GetUserCode();

			return relation;
		}

		protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("./FUserGroup2ResourceAP.aspx", new string[]{"usergroupcode"}, new string[]{this.txtUserGroupCodeQuery.Text.Trim()}));
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("./FSecurityMP.aspx"));
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
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion
	}
}
