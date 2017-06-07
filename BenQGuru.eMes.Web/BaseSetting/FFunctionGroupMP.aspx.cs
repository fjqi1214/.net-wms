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
	/// FFunctionGroupMP 的摘要说明。
	/// </summary>
	public partial class FFunctionGroupMP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.BaseSetting.SystemSettingFacade _facade = null; 
	
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

           // this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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
            //
		}

		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn( "FunctionGroupCode", "功能组代码",	null);
            this.gridHelper.AddColumn("FunctionGroupDescription", "功能组描述", null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
			this.gridHelper.AddLinkColumn( "SelectModule","选择模块",null);
			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override DataRow GetGridRow(object obj)
		{
            DataRow row = this.DtSource.NewRow();
            row["FunctionGroupCode"] = ((FunctionGroup)obj).FunctionGroupCode.ToString();
            row["FunctionGroupDescription"] = ((FunctionGroup)obj).FunctionGroupDescription.ToString();
            row["MaintainUser"] = ((FunctionGroup)obj).MaintainUser;//.GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((FunctionGroup)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((FunctionGroup)obj).MaintainTime);
            
            return row;

		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade() ;
			}
			return this._facade.QueryFunctionGroup( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFunctionGroupCodeQuery.Text)),
				inclusive, exclusive );
		}


		protected override int GetRowCount()
		{
			if(_facade==null)
			{
                _facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade();
			}
			return this._facade.QueryFunctionGroupCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFunctionGroupCodeQuery.Text)));
		}

		#endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade==null)
			{
                _facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade();
			}
            this._facade.AddFunctionGroup((FunctionGroup)domainObject);
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade() ;
			}
            this._facade.DeleteFunctionGroup((FunctionGroup[])domainObjects.ToArray(typeof(FunctionGroup)));
		}
        
		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade() ;
			}
            this._facade.UpdateFunctionGroup((FunctionGroup)domainObject);
		}


		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtFunctionGroupCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtFunctionGroupCodeEdit.ReadOnly = true;
			}
		}

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName=="SelectModule")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FFunctionGroup2FunctionSP.aspx", new string[] { "functiongroupcode" }, new string[] { row.Items.FindItemByKey("FunctionGroupCode").Text.Trim() }));
            }
        }

		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			//this.ValidateInput();

			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade() ;
			}
            FunctionGroup functionGroup = this._facade.CreateNewFunctionGroup();

            functionGroup.FunctionGroupDescription = FormatHelper.CleanString(this.txtFunctionGroupDescriptionEdit.Text, 100);
            functionGroup.FunctionGroupCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFunctionGroupCodeEdit.Text, 40));
            functionGroup.MaintainUser = this.GetUserCode();

            return functionGroup;
		}


		protected override object GetEditObject(GridRecord row)
		{
			if(_facade==null)
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).CreateSystemSettingFacade() ;
			}
            object obj = _facade.GetFunctionGroup(row.Items.FindItemByKey("FunctionGroupCode").Text.ToString());
			
			if (obj != null)
			{
                return (FunctionGroup)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
                this.txtFunctionGroupDescriptionEdit.Text = "";
				this.txtFunctionGroupCodeEdit.Text	= "";

				return;
			}

            this.txtFunctionGroupDescriptionEdit.Text = ((FunctionGroup)obj).FunctionGroupDescription.ToString();
            this.txtFunctionGroupCodeEdit.Text = ((FunctionGroup)obj).FunctionGroupCode.ToString();
		}

		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblFunctionGroupDescriptionEdit, this.txtFunctionGroupDescriptionEdit, 100, false));
            manager.Add(new LengthCheck(this.lblFunctionGroupCodeEdit, this.txtFunctionGroupCodeEdit, 40, true));			

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
            return new string[]{ ((FunctionGroup)obj).FunctionGroupCode.ToString(),
								   ((FunctionGroup)obj).FunctionGroupDescription.ToString(),
                                    //((FunctionGroup)obj).MaintainUser.ToString(),
                             ((FunctionGroup)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((FunctionGroup)obj).MaintainDate),
								   FormatHelper.ToTimeString(((FunctionGroup)obj).MaintainTime) };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"FunctionGroupCode",
									"FunctionGroupDesc",
									"MaintainUser",	
									"MaintainDate",	
									"MaintainTime" };
        }

		#endregion
	}
}
