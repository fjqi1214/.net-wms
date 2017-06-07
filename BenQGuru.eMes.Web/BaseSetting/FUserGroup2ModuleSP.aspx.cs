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
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Security;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FOperation2ResourceSP 的摘要说明。
	/// </summary>
	public partial class FUserGroup2ModuleSP : BaseMPage
	{
		protected System.Web.UI.WebControls.Label lblModuleSelectTitle;
		protected System.Web.UI.WebControls.Label lblModuleCodeQuery;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.TextBox txtOperationCodeQuery;
		protected System.Web.UI.WebControls.Label lblRouteCodeEdit;
		protected System.Web.UI.WebControls.TextBox txtRouteCodeEdit;
		protected System.Web.UI.WebControls.Label lblRouteSequenceEdit;
		protected System.Web.UI.WebControls.TextBox txtRouteSequenceEdit;
		private SystemSettingFacade facade = null;//	new SystemSettingFacadeFactory().Create();
		private SecurityFacade securityFacade = null ;// new SystemSettingFacadeFactory().CreateSecurityFacade();

		#region Stable
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.pagerSizeSelector.Readonly = true;

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
			this.gridHelper.AddColumn("ModuleSequence",	"模块顺序",		null);
			this.gridHelper.AddColumn("ModuleCode",		"模块代码",		null);
			this.gridHelper.AddColumn("ParentModuleCode",		"父模块代码",		null);
			this.gridHelper.AddColumn("ModuleVersion",	"模块版本",		null);
			this.gridHelper.AddColumn("ModuleType",		"模块类型",		null);
			this.gridHelper.AddColumn("ModuleStatus",	"模块状态",		null);
			this.gridHelper.AddColumn("HelpFileName",	"帮助文件",		null);
			this.gridHelper.AddColumn("IsSystem",		"是否系统模块",	null);
			this.gridHelper.AddColumn("IsActive",		"是否可用",		null);
			this.gridHelper.AddColumn("MDLDescription",	"模块描述",		null);
			this.gridHelper.AddColumn("FormUrl",		"页面URL",		null);
			
			this.gridHelper.AddCheckBoxColumn("Export",		"导出",	false,	null);  //Add by Anco
			this.gridHelper.AddCheckBoxColumn("Read",		"读",	false,	null);
			this.gridHelper.AddCheckBoxColumn("Write",		"写",	false,	null);
			this.gridHelper.AddCheckBoxColumn("Delete",		"删",	false,	null);

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}
			facade.DeleteUserGroup2Module( (UserGroup2Module[])domainObjects.ToArray(typeof(UserGroup2Module)));
		}

		protected override int GetRowCount()
		{
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}
			return this.facade.GetSelectedModuleByUserGroupCodeCount( 
			FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpModuleTypeEdit.SelectedValue)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModuleCodeQuery.Text)),
				this.txtModuleDescEdit.Text);
		}

		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			if(securityFacade==null)
			{
				securityFacade = new SystemSettingFacadeFactory(base.DataProvider).CreateSecurityFacade();
			}
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((ModuleWithViewValue)obj).ModuleSequence,
								((ModuleWithViewValue)obj).ModuleCode,
								((ModuleWithViewValue)obj).ParentModuleCode,
								((ModuleWithViewValue)obj).ModuleVersion,
								((ModuleWithViewValue)obj).ModuleType,
								((ModuleWithViewValue)obj).ModuleStatus,
								((ModuleWithViewValue)obj).ModuleHelpFileName,
								FormatHelper.DisplayBoolean(((ModuleWithViewValue)obj).IsSystem, this.languageComponent1),
								FormatHelper.DisplayBoolean(((ModuleWithViewValue)obj).IsActive, this.languageComponent1),
								((ModuleWithViewValue)obj).ModuleDescription,
								((ModuleWithViewValue)obj).FormUrl,
								securityFacade.HasRight( ((ModuleWithViewValue)obj).ViewValue, RightType.Export, false ).ToString(),
								securityFacade.HasRight( ((ModuleWithViewValue)obj).ViewValue, RightType.Read, false ).ToString(),
								securityFacade.HasRight( ((ModuleWithViewValue)obj).ViewValue, RightType.Write, false ).ToString(),
								securityFacade.HasRight( ((ModuleWithViewValue)obj).ViewValue, RightType.Delete, false ).ToString()
								});
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtModuleCodeEdit.Text = "";
				foreach ( ListItem item in this.chbRight.Items)
				{
					item.Selected = false;
				}

				return;
			}

			this.txtModuleCodeEdit.Text = ((UserGroup2Module)obj).ModuleCode;
			if(securityFacade==null)
			{
				securityFacade = new SystemSettingFacadeFactory(base.DataProvider).CreateSecurityFacade();
			}
			this.chbRight.Items[0].Selected = securityFacade.HasRight( ((UserGroup2Module)obj).ViewValue, RightType.Export, false );
			this.chbRight.Items[1].Selected = securityFacade.HasRight( ((UserGroup2Module)obj).ViewValue, RightType.Read, false );
			this.chbRight.Items[2].Selected = securityFacade.HasRight( ((UserGroup2Module)obj).ViewValue, RightType.Write, false );
			this.chbRight.Items[3].Selected = securityFacade.HasRight( ((UserGroup2Module)obj).ViewValue, RightType.Delete, false );
		}

		protected override object GetEditObject(UltraGridRow row)
		{
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}
			object obj = this.facade.GetUserGroup2Module( this.txtUserGroupCodeQuery.Text, row.Cells[2].Text.Trim());
			
			if (obj != null)
			{
				return (UserGroup2Module)obj;
			}

			return null;
		}

		protected override object GetEditObject()
		{
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}
			UserGroup2Module relation = facade.CreateNewUserGroup2Module();

			relation.ModuleCode = this.txtModuleCodeEdit.Text;
			relation.UserGroupCode = this.txtUserGroupCodeQuery.Text;

			if(securityFacade==null)
			{
				securityFacade = new SystemSettingFacadeFactory(base.DataProvider).CreateSecurityFacade();
			}
			relation.ViewValue = securityFacade.SpellViewValueFromRights( new bool[]{
																						this.chbRight.Items[0].Selected,
																						this.chbRight.Items[1].Selected,				
																						this.chbRight.Items[2].Selected,
																						this.chbRight.Items[3].Selected});

			relation.MaintainUser = this.GetUserCode();

			return relation;
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}
			this.facade.UpdateUserGroup2Module((UserGroup2Module)domainObject );
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{	
			if(facade==null)
			{
				facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}
			return facade.GetSelectedModuleWithViewValueByUserGroupCode( 				
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpModuleTypeEdit.SelectedValue)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModuleCodeQuery.Text)),
				this.txtModuleDescEdit.Text,
				inclusive,exclusive);
		}

		protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("./FUserGroup2ModuleAP.aspx", new string[]{"usergroupcode"}, new string[]{this.txtUserGroupCodeQuery.Text.Trim()}));
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
		

		protected void drpModuleTypeEdit_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				this.drpModuleTypeEdit.Items.Clear();

				if( InternalSystemVariable.Lookup("ModuleType") == null )
				{
					return;
				}
				
				foreach (string _Items in (InternalSystemVariable.Lookup("ModuleType").Items))
				{
					drpModuleTypeEdit.Items.Add(_Items);
				}
																							
			}
		}
	}
}
