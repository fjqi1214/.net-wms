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
    /// FUserGroup2FunctionGroupSP 的摘要说明。
	/// </summary>
    public partial class FUserGroup2FunctionGroupSP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private SystemSettingFacade facade = null;//new SystemSettingFacadeFactory().CreateUserFacade();	

		#region Stable
		protected void Page_Load(object sender, System.EventArgs e)
		{					//this.pagerSizeSelector.Readonly = true;

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
            this.gridHelper.AddColumn("FunctionGroupCode", "功能组代码", null);
            this.gridHelper.AddColumn("FunctionGroupDescription", "功能组描述", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
			this.gridHelper.AddDefaultColumn( true, false );
            this.gridHelper.ApplyLanguage(this.languageComponent1);
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade() ;
			}
            facade.DeleteUserGroup2FunctionGroup((UserGroup2FunctionGroup[])domainObjects.ToArray(typeof(UserGroup2FunctionGroup)));
		}

		protected override int GetRowCount()
		{
			if(facade==null)
			{
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade();
			}
			return this.facade.GetSelectedFunctionGroupByUserGroupCodeCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFunctionGroupCodeQuery.Text)));
		}

		protected override DataRow GetGridRow(object obj)
		{
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((FunctionGroup)obj).FunctionGroupCode.ToString(),
            //                    ((FunctionGroup)obj).FunctionGroupDescription.ToString(),
            //                    //((FunctionGroup)obj).MaintainUser.ToString(),
            //                  ((FunctionGroup)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((FunctionGroup)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((FunctionGroup)obj).MaintainTime)
            //                    });
            DataRow row = this.DtSource.NewRow();
            row["FunctionGroupCode"] = ((FunctionGroup)obj).FunctionGroupCode.ToString();
            row["FunctionGroupDescription"] = ((FunctionGroup)obj).FunctionGroupDescription.ToString();
            row["MaintainUser"] = ((FunctionGroup)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((FunctionGroup)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((FunctionGroup)obj).MaintainTime);
            return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{	
			if(facade==null)
			{
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade();
			}
			return facade.GetSelectedFunctionGroupByUserGroupCode( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFunctionGroupCodeQuery.Text)),
				inclusive,exclusive);
		}

		protected override object GetEditObject(GridRecord row)
		{
			if(facade==null)
			{
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade();
			}
            UserGroup2FunctionGroup relation = facade.CreateNewUserGroup2FunctionGroup();

			relation.UserGroupCode = this.txtUserGroupCodeQuery.Text.Trim();
            relation.FunctionGroupCode = row.Items.FindItemByKey("FunctionGroupCode").Text;		
			relation.MaintainUser = this.GetUserCode();

			return relation;
		}

		protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("./FUserGroup2FunctionGroupAP.aspx", new string[]{"usergroupcode"}, new string[]{this.txtUserGroupCodeQuery.Text.Trim()}));
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
