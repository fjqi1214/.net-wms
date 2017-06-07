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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Web.UserControl;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FParameterGroupMP 的摘要说明。
	/// </summary>
	public partial class FParameterGroupMP : BaseMPageNew
	{
        protected System.Web.UI.WebControls.Label lblParameterGroupTitle;
    
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.BaseSetting.SystemSettingFacade _facade = null ;

		protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

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

		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		#endregion

		#region WebGrid
	     
		protected override void InitWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn( "ParameterGroupCode", "参数组代码",	null);
			this.gridHelper.AddColumn( "ParameterGroupType", "参数组类型",	null);
			this.gridHelper.AddColumn( "IsSystem", "是否为系统参数组",	null);
			this.gridHelper.AddColumn( "ParameterGroupDescription", "参数组描述",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		
		protected override  DataRow GetGridRow(object obj)
		{
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{"false",
            //                    ((ParameterGroup)obj).ParameterGroupCode.ToString(),
            //                    ((ParameterGroup)obj).ParameterGroupType.ToString(),
            //                    FormatHelper.DisplayBoolean(((ParameterGroup)obj).IsSystem, this.languageComponent1),
            //                    ((ParameterGroup)obj).ParameterGroupDescription.ToString(),
            //                    //((ParameterGroup)obj).MaintainUser.ToString(),
            //                   ((ParameterGroup)obj).GetDisplayText("MaintainUser"),


            //                    FormatHelper.ToDateString(((ParameterGroup)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((ParameterGroup)obj).MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["ParameterGroupCode"] = ((ParameterGroup)obj).ParameterGroupCode.ToString();
            row["ParameterGroupType"] = ((ParameterGroup)obj).ParameterGroupType.ToString();
            row["IsSystem"] = FormatHelper.DisplayBoolean(((ParameterGroup)obj).IsSystem, this.languageComponent1);
            row["ParameterGroupDescription"] = ((ParameterGroup)obj).ParameterGroupDescription.ToString();
            row["MaintainUser"] = ((ParameterGroup)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((ParameterGroup)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((ParameterGroup)obj).MaintainTime);
            return row;

		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if( _facade==null )
			{
				_facade = new SystemSettingFacade(base.DataProvider);
			}
			return this._facade.QueryParameterGroup( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtParameterGroupCodeQuery.Text)),
				FormatHelper.CleanString(this.txtParameterGroupTypeQuery.Text),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if( _facade==null )
			{
				_facade = new SystemSettingFacade(base.DataProvider);
			}
			return this._facade.QueryParameterGroupCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtParameterGroupCodeQuery.Text)),
				FormatHelper.CleanString(this.txtParameterGroupTypeQuery.Text));
		}


		#endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{
			if( _facade==null )
			{
				_facade = new SystemSettingFacade(base.DataProvider);
			}
			this._facade.AddParameterGroup( (ParameterGroup)domainObject );

		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if( _facade==null )
			{
				_facade = new SystemSettingFacade(base.DataProvider);
			}
			this._facade.UpdateParameterGroup( (ParameterGroup)domainObject );

		}
        
		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if( _facade==null )
			{
				_facade = new SystemSettingFacade(base.DataProvider);
			}
			this._facade.DeleteParameterGroup( (ParameterGroup[])domainObjects.ToArray( typeof(ParameterGroup) ) );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtParameterGroupCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtParameterGroupCodeEdit.ReadOnly = true;
			}
		}

		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			//this.ValidateInput();

			if( _facade==null )
			{
				_facade = new SystemSettingFacade(base.DataProvider);
			}
			ParameterGroup parameterGroup = this._facade.CreateNewParameterGroup();

			parameterGroup.ParameterGroupType		 = FormatHelper.CleanString(this.txtParameterGroupTypeEdit.Text, 40);
			parameterGroup.ParameterGroupDescription = FormatHelper.CleanString(this.txtParameterGroupDescriptionEdit.Text, 100);
			parameterGroup.IsSystem					 = FormatHelper.BooleanToString(this.chbIsSystemGroupEdit.Checked);
			parameterGroup.ParameterGroupCode		 = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtParameterGroupCodeEdit.Text, 40));
			parameterGroup.MaintainUser				 = this.GetUserCode();

			return parameterGroup;
		}


		protected override object GetEditObject(GridRecord row)
		{
			if( _facade==null )
			{
				_facade = new SystemSettingFacade(base.DataProvider);
			}
            object obj = _facade.GetParameterGroup(row.Items.FindItemByKey("ParameterGroupCode").Text.ToString());
			
			if (obj != null)
			{
				return (ParameterGroup)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtParameterGroupTypeEdit.Text			= "";
				this.txtParameterGroupDescriptionEdit.Text	= "";
				this.chbIsSystemGroupEdit.Checked				= false;
				this.txtParameterGroupCodeEdit.Text			= "";

				return;
			}

			this.txtParameterGroupTypeEdit.Text			= ((ParameterGroup)obj).ParameterGroupType.ToString();
			this.txtParameterGroupDescriptionEdit.Text	= ((ParameterGroup)obj).ParameterGroupDescription.ToString();
			this.chbIsSystemGroupEdit.Checked				= FormatHelper.StringToBoolean(((ParameterGroup)obj).IsSystem);
			this.txtParameterGroupCodeEdit.Text			= ((ParameterGroup)obj).ParameterGroupCode.ToString();
		}

		
		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblParameterGroupCodeEdit, this.txtParameterGroupCodeEdit, 40, true) );
			manager.Add( new LengthCheck(this.lblParameterGroupTypeEdit, this.txtParameterGroupTypeEdit, 40, true) );
			manager.Add( new LengthCheck(this.lblParameterGroupDescriptionEdit, this.txtParameterGroupDescriptionEdit, 100, false) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}

			return true;
		}

		#endregion

		#region Export
		// 2005-04-06

		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{  ((ParameterGroup)obj).ParameterGroupCode.ToString(),
								   ((ParameterGroup)obj).ParameterGroupType.ToString(),
								   FormatHelper.DisplayBoolean(((ParameterGroup)obj).IsSystem, this.languageComponent1),
								   ((ParameterGroup)obj).ParameterGroupDescription.ToString(),
                                   //((ParameterGroup)obj).MaintainUser.ToString(),
                                 ((ParameterGroup)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((ParameterGroup)obj).MaintainDate),
								   FormatHelper.ToTimeString(((ParameterGroup)obj).MaintainTime) };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"参数组代码",
									"参数组类型",	
									"是否为系统参数组",	
									"参数组描述",
									"维护用户",	
									"维护日期",	
									"维护时间" };
		}

		#endregion

	}
}
