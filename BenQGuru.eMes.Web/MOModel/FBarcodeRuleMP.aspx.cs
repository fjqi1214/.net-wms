#region system
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
#endregion

#region project
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FBarcodeRuleMP 的摘要说明。
	/// </summary>
	public partial class FBarcodeRuleMP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private ModelFacade _facade ;//= new FacadeFactory(base.DataProvider).CreateModelFacade();
		
	
	

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

		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !this.IsPostBack )
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
			this.gridHelper.AddColumn( "ModelCode", "产品别代码",	null);
			this.gridHelper.AddColumn( "AModelCode", "缩略编码",	null);
			this.gridHelper.AddColumn( "Description", "含义",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);

			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
			this.gridWebGrid.Columns.FromKey("MaintainUser").Hidden = true;
			this.gridWebGrid.Columns.FromKey("MaintainDate").Hidden = true;
			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((BarcodeRule)obj).ModelCode.ToString(),
								((BarcodeRule)obj).AModelCode.ToString(),
								((BarcodeRule)obj).Description.ToString(),
								((BarcodeRule)obj).MaintainUser.ToString(),
								FormatHelper.ToDateString(((BarcodeRule)obj).MaintainDate),
								FormatHelper.ToTimeString(((BarcodeRule)obj).MaintainTime),
								""});
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._facade.QueryIllegibility( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpModelCodeQuery.SelectedValue)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAModelCodeQuery.Text)),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._facade.QueryIllegibilityCounts( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpModelCodeQuery.SelectedValue)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAModelCodeQuery.Text)));
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{	
			if(_facade==null){_facade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			this._facade.AddBarcodeRule( (BarcodeRule)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			this._facade.DeleteBarcodeRule( (BarcodeRule[])domainObjects.ToArray( typeof(BarcodeRule) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			this._facade.UpdateBarcodeRule( (BarcodeRule)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.drpModelCodeEdit.Enabled = true;
				this.txtAModelCodeEdit.Enabled = true;
			}
			
			if ( pageAction == PageActionType.Update )
			{
				this.drpModelCodeEdit.Enabled = false;
				this.txtAModelCodeEdit.Enabled = false;
			}
		}
		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			if(_facade==null){_facade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			BarcodeRule barcodeRule = this._facade.CreateNewBarcodeRule();

			barcodeRule.ModelCode	= FormatHelper.CleanString(this.drpModelCodeEdit.SelectedValue, 40);
			barcodeRule.AModelCode	= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAModelCodeEdit.Text,40));
			barcodeRule.Description		= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDescriptionEdit.Text,100));
			barcodeRule.MaintainUser		= this.GetUserCode();

			return barcodeRule;
		}


		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_facade==null){_facade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			object obj = _facade.GetBarcodeRule( row.Cells[1].Text.ToString(), row.Cells[2].Text.ToString());
			
			if (obj != null)
			{
				return obj as BarcodeRule;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.drpModelCodeEdit.SelectedIndex	= 0;
				this.txtAModelCodeEdit.Text	= "";
				this.txtDescriptionEdit.Text	= "";

				return;
			}

			try
			{
				this.drpModelCodeEdit.SelectedValue	= ((BarcodeRule)obj).ModelCode.ToString();
			}
			catch
			{
				this.drpModelCodeEdit.SelectedIndex =0;
			}
			this.txtAModelCodeEdit.Text	= ((BarcodeRule)obj).AModelCode;
			this.txtDescriptionEdit.Text	= ((BarcodeRule)obj).Description.ToString();
		}
		
		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblModelCodeEdit, drpModelCodeEdit, 40, true) );
			manager.Add( new ExtraLengthChek(lblAModelCodeEdit, txtAModelCodeEdit, 2, true) );
			manager.Add( new ExtraLengthChek(lblDescriptionEdit, txtDescriptionEdit, 8, true) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}
			return true;
		}

		#endregion

		#region 数据初始化
		protected void drpModelCodeQuery_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				if(_facade==null){_facade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
				DropDownListBuilder builder = new DropDownListBuilder(this.drpModelCodeQuery);
				builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this._facade.GetAllModels);
				builder.Build("ModelCode", "ModelCode");
				builder.AddAllItem(this.languageComponent1);
			}	
		
		}
		protected void drpModelCodeEdit_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				if(_facade==null){_facade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
				DropDownListBuilder builder = new DropDownListBuilder(this.drpModelCodeEdit);
				builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this._facade.GetAllModels);

				builder.Build("ModelCode", "ModelCode");
			}	
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{  ((BarcodeRule)obj).ModelCode.ToString(),
								   ((BarcodeRule)obj).AModelCode.ToString(),
								   ((BarcodeRule)obj).Description.ToString() };
		}

		protected override string[] GetColumnHeaderText()
		{		
			// TODO: 调整字段值的顺序，使之与Grid的列对应
			return new string[] {	
									"ModelCode",
									"AModelCode",
									"Description"
									};
		}
		#endregion

		
		
	}
}
