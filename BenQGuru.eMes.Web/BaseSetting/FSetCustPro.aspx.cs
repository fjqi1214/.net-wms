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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Security;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FUserGroupMP 的摘要说明。
	/// </summary>
	public partial class FSetCustPro : BaseMPage
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
				this.txtUserGroupCodeQuery.Text=Request.QueryString["CustCode"].ToString();
				this.InitialData();
			}
			
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		private void InitialData()
		{
			//SystemParameterListBuilder builder = new SystemParameterListBuilder("usergrouptype",base.DataProvider);
			//builder.Build( this.drpUserGroupTypeEdit );
			//this.drpUserGroupTypeEdit.Items.Insert(0,"");
		}

		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
			this.gridHelper.AddColumn( "CGroupCode", "客户组代码",	null);
			this.gridHelper.AddColumn( "ProGroup", "产品代码",	null);
		
			this.gridHelper.AddColumn( "User", "维护用户",	null);
			this.gridHelper.AddColumn( "Date", "维护日期",	null);
			this.gridHelper.AddColumn( "Time", "维护时间",	null);
			this.gridHelper.AddColumn( "desc", "描述",	null);
			//			this.gridHelper.AddLinkColumn( "SelectUser","选择用户",null);
			//			this.gridHelper.AddLinkColumn( "SelectModule","选择模块",null);
			this.gridHelper.AddDefaultColumn( true, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((USERGROUP2ITEM)obj).USERGROUPCODE.ToString(),
								((USERGROUP2ITEM)obj).ITEMCODE.ToString(),
								((USERGROUP2ITEM)obj).MUSER.ToString(),
								FormatHelper.ToDateString(((USERGROUP2ITEM)obj).MDATE),
								FormatHelper.ToTimeString(((USERGROUP2ITEM)obj).MTIME),
								((USERGROUP2ITEM)obj).EATTRIBUTE1.ToString(),
								});
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			object[] objs = this._facade.CSQueryUserGroup( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
				inclusive, exclusive );
			return objs;
		}


		protected override int GetRowCount()
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			return this._facade.CSQueryUserGroupCount( 
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
			this._facade.AddUSERGROUP2ITEM((USERGROUP2ITEM)domainObject);
		}

	

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			this._facade.DeleteUSERGROUP2ITEM((USERGROUP2ITEM[])domainObjects.ToArray(typeof(USERGROUP2ITEM)));
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

		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			//this.ValidateInput();
           
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			USERGROUP2ITEM userGroup = this._facade.CreateNewUSERGROUP2ITEM();

			userGroup.EATTRIBUTE1 = FormatHelper.CleanString(this.txtUserGroupDescriptionEdit.Text, 100);
			userGroup.USERGROUPCODE = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text, 40));
			userGroup.ITEMCODE = FormatHelper.CleanString(this.citemtype.Text, 40);
			userGroup.MDATE=FormatHelper.TODateInt(DateTime.Now);
            userGroup.MTIME=FormatHelper.TOTimeInt(DateTime.Now);
			userGroup.MUSER = this.GetUserCode();

			return userGroup;
		}


		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade() ;
			}
			object obj = _facade.GetUSERGROUP2ITEM( row.Cells[1].Text.ToString(),row.Cells[2].Text.ToString());
			
			if (obj != null)
			{
				return (USERGROUP2ITEM)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtUserGroupDescriptionEdit.Text	= "";
				this.txtUserGroupCodeEdit.Text	= "";
				this.citemtype.Text="";

				return;
			}

			this.txtUserGroupDescriptionEdit.Text	= ((USERGROUP2ITEM)obj).EATTRIBUTE1.ToString();
			this.txtUserGroupCodeEdit.Text	= ((USERGROUP2ITEM)obj).USERGROUPCODE.ToString();
			try
			{
				this.citemtype.Text	= ((USERGROUP2ITEM)obj).ITEMCODE.ToString();
			}
			catch
			{
				citemtype.Text="";
			}
		}

		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblItemTypeEdit, this.citemtype, 40, true) );			
			manager.Add( new LengthCheck(this.lblCustomGroupCodeEdit, this.txtUserGroupCodeEdit, 40, true) );			

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
			return new string[]{ ((USERGROUP2ITEM)obj).USERGROUPCODE.ToString(),
								   ((USERGROUP2ITEM)obj).ITEMCODE.ToString(),
								   ((USERGROUP2ITEM)obj).EATTRIBUTE1.ToString(),
								   ((USERGROUP2ITEM)obj).MUSER.ToString(),
								   FormatHelper.ToDateString(((USERGROUP2ITEM)obj).MDATE),
								   FormatHelper.ToTimeString(((USERGROUP2ITEM)obj).MTIME) };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"客户组代码",
									"产品类别",
									"维护用户",	
									 "描述",
									"维护日期",	
									"维护时间" };
		}

		#endregion
	}
}