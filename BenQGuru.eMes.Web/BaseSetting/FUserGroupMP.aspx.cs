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
using BenQGuru.eMES.Security;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FUserGroupMP 的摘要说明。
	/// </summary>
	public partial class FUserGroupMP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private BenQGuru.eMES.BaseSetting.UserFacade _facade = null ; //new SystemSettingFacadeFactory().CreateUserFacade();
	
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
           // this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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
			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.InitialData();
			}
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		private void InitialData()
		{
			SystemParameterListBuilder builder = new SystemParameterListBuilder("usergrouptype",base.DataProvider);
			builder.Build( this.drpUserGroupTypeEdit );
			this.drpUserGroupTypeEdit.Items.Insert(0,"");
		}

		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn( "UserGroupCode", "用户组代码",	null);
			this.gridHelper.AddColumn( "UserGroupType", "用户组类别",	null);
			this.gridHelper.AddColumn( "UserGroupDescription", "用户组描述",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
            this.gridHelper.AddLinkColumn("SelectUser", "选择用户", null);
            this.gridHelper.AddLinkColumn("SelectVendor", "选择供应商", null);
			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override DataRow GetGridRow(object obj)
		{
           
            DataRow row = this.DtSource.NewRow();
            row["UserGroupCode"] = ((UserGroup)obj).UserGroupCode.ToString();
            row["UserGroupType"] = ((UserGroup)obj).UserGroupType.ToString();
            row["UserGroupDescription"] = ((UserGroup)obj).UserGroupDescription.ToString();
            row["MaintainUser"] = ((UserGroup)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((UserGroup)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((UserGroup)obj).MaintainTime);

            return row;

		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			return this._facade.QueryUserGroup( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
				inclusive, exclusive );
		}


		protected override int GetRowCount()
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			return this._facade.QueryUserGroupCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)));
		}

		#endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			this._facade.AddUserGroup((UserGroup)domainObject);
			//this.AddDefaultPageRight();
		}

		#region 默认权限处理

		//默认添加STARTPAGE 的权限
		private void AddDefaultPageRight()
		{
			UserGroup2Module relation = new UserGroup2Module();
			relation.UserGroupCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeEdit.Text, 40));
			relation.ModuleCode = "STARTPAGE";
			SecurityFacade securityFacade = new SecurityFacade(base.DataProvider);
			relation.ViewValue = securityFacade.SpellViewValueFromRights( new bool[]{true,true,true});
			relation.MaintainUser = this.GetUserCode();

			SystemSettingFacade facade = new SystemSettingFacade(base.DataProvider);
			facade.AddUserGroup2Module( relation);
		}
		//删除STARTPAGE 的权限
		private void DeleteDefaultPageRight(ArrayList domainObjects)
		{
			SystemSettingFacade facade = new SystemSettingFacade(base.DataProvider);
			foreach(UserGroup ug in domainObjects)
			{
				UserGroup2Module relation = new UserGroup2Module();
				relation.UserGroupCode = ug.UserGroupCode;
				relation.ModuleCode = "STARTPAGE";
				
				facade.DeleteUserGroup2Module( relation);
			}
		}
		#endregion

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			//this.DeleteDefaultPageRight(domainObjects);
			this._facade.DeleteUserGroup((UserGroup[])domainObjects.ToArray(typeof(UserGroup)));
		}
        
		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			this._facade.UpdateUserGroup((UserGroup)domainObject);
		}


		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtUserGroupCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtUserGroupCodeEdit.ReadOnly = true;
			}
		}

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName=="SelectUser")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FUserGroup2UserSP.aspx", new string[] { "usergroupcode" }, new string[] { row.Items.FindItemByKey("UserGroupCode").Text.Trim() }));
            }
            else if (commandName == "SelectVendor")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FUserGroup2VendorSP.aspx", new string[] { "usergroupcode" }, new string[] { row.Items.FindItemByKey("UserGroupCode").Text.Trim() }));
            }
        }

		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			//this.ValidateInput();

			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			UserGroup userGroup = this._facade.CreateNewUserGroup();

			userGroup.UserGroupDescription = FormatHelper.CleanString(this.txtUserGroupDescriptionEdit.Text, 100);
			userGroup.UserGroupCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeEdit.Text, 40));
			userGroup.UserGroupType = FormatHelper.CleanString(this.drpUserGroupTypeEdit.SelectedValue, 40);
			userGroup.MaintainUser = this.GetUserCode();

			return userGroup;
		}


		protected override object GetEditObject(GridRecord row)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
            object obj = _facade.GetUserGroup(row.Items.FindItemByKey("UserGroupCode").Text.ToString());
			
			if (obj != null)
			{
				return (UserGroup)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtUserGroupDescriptionEdit.Text	= "";
				this.txtUserGroupCodeEdit.Text	= "";
				this.drpUserGroupTypeEdit.SelectedIndex = 0;

				return;
			}

			this.txtUserGroupDescriptionEdit.Text	= ((UserGroup)obj).UserGroupDescription.ToString();
			this.txtUserGroupCodeEdit.Text	= ((UserGroup)obj).UserGroupCode.ToString();
			try
			{
				this.drpUserGroupTypeEdit.SelectedValue	= ((UserGroup)obj).UserGroupType.ToString();
			}
			catch
			{
				this.drpUserGroupTypeEdit.SelectedIndex = 0;
			}
		}

		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblUserGroupDescriptionEdit, this.txtUserGroupDescriptionEdit, 100, false) );			
			manager.Add( new LengthCheck(this.lblUserGroupCodeEdit, this.txtUserGroupCodeEdit, 40, true) );			
			manager.Add( new LengthCheck(this.lblUserGroupTypeEdit, this.drpUserGroupTypeEdit, 40, true) );
			//manager.Add( new LengthCheck(this.lblUserGroupTypeEdit, this.drpUserGroupTypeEdit, 40, true) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}

			return true;
		}

		#endregion]

		#region Export
		// 2005-04-06

		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{ ((UserGroup)obj).UserGroupCode.ToString(),
								   ((UserGroup)obj).UserGroupType.ToString(),
								   ((UserGroup)obj).UserGroupDescription.ToString(),
                                   //((UserGroup)obj).MaintainUser.ToString(),
                	                ((UserGroup)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((UserGroup)obj).MaintainDate),
								   FormatHelper.ToTimeString(((UserGroup)obj).MaintainTime) };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"用户组代码",
									"用户组类别",
									"用户组描述",
									"维护用户",	
									"维护日期",	
									"维护时间",
                                    "选择用户"};
		}

		#endregion
	}
}
