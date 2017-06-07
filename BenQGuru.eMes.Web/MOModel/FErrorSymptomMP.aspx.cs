#region System
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
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.RMA;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FItemLocationMP 的摘要说明。
	/// </summary>
	public partial class FErrorSymptomMP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private RMAFacade _facade ;

	
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
			if (!IsPostBack)
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
			this.gridHelper.AddColumn( "ErrorSymptom", "不良现象",	null);
			this.gridHelper.AddColumn( "Description", "描述",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridHelper.RequestData();
		}

		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			ErrorSymptom es = obj as ErrorSymptom;	
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								es.SymptomCode.ToString(),
								es.Description.ToString(),
								es.MaintainUser.ToString(),
								FormatHelper.ToDateString(es.MaintainDate),
								FormatHelper.ToTimeString(es.MaintainTime),
								""});
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			return this._facade.QueryErrorSymptom( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSymptomCodeQuery.Text)),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			return this._facade.QueryErrorSymptomCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSymptomCodeQuery.Text)));
		}

		#endregion

		#region Button
		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FModelMP.aspx"));
		}

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			
			this._facade.AddErrorSymptom( (ErrorSymptom)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			this._facade.DeleteErrorSymptom( (ErrorSymptom[])domainObjects.ToArray( typeof(ErrorSymptom) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			this._facade.UpdateErrorSymptom( (ErrorSymptom)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtSymptomEdit.ReadOnly = false;
			}
			
			else if ( pageAction == PageActionType.Update )
			{
				this.txtSymptomEdit.ReadOnly = true;
			}

			else if(pageAction == PageActionType.Cancel)
			{
				this.txtSymptomEdit.ReadOnly = false;
			}
		}
		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			ErrorSymptom es = this._facade.CreateNewErrorSymptom();
			es.SymptomCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSymptomEdit.Text,40));
			es.Description	= FormatHelper.CleanString( this.txtDescEdit.Text, 200);
			es.MaintainUser			= this.GetUserCode();

			return es;
		}

		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			object obj = _facade.GetErrorSymptom( row.Cells.FromKey("ErrorSymptom").Text.ToString() );
			
			if (obj != null)
			{
				return (ErrorSymptom)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtSymptomEdit.Text = string.Empty;
				this.txtDescEdit.Text = string.Empty;
				return;
			}

			ErrorSymptom es = obj as ErrorSymptom;
			this.txtSymptomEdit.Text = es.SymptomCode.ToString();
			this.txtDescEdit.Text = es.Description.ToString();
		}

		
		protected override bool ValidateInput()
		{			
			PageCheckManager manager = new PageCheckManager();
			
			manager.Add( new LengthCheck(lblSymptomEdit, txtSymptomEdit, 40, true) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);

				return false;
			}

			return true;
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			ErrorSymptom es = obj as ErrorSymptom;	
			return new string[]{  
								   es.SymptomCode.ToString(),
								   es.Description.ToString(),
								   es.MaintainUser.ToString(),
								   FormatHelper.ToDateString(es.MaintainDate),
								   FormatHelper.ToTimeString(es.MaintainTime)
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"ErrorSymptom",
									"Description",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"
									};
		}
		#endregion

		#region property
	
		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}
		#endregion

		
	}
}
