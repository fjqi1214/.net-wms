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
	public class FModel2ErrorSymptomMP : BaseMPage
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid gridWebGrid;
		protected System.Web.UI.WebControls.CheckBox chbSelectAll;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdGridExport;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdDelete;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdAdd;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdSave;
		protected BenQGuru.eMES.Web.Helper.PagerSizeSelector pagerSizeSelector;
		protected BenQGuru.eMES.Web.Helper.PagerToolBar pagerToolBar;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdCancel;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdReturn;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.Label lblModelCodeQuery;
		protected System.Web.UI.WebControls.Label lblSymptomEdit;
		protected System.Web.UI.WebControls.TextBox txtSymptomEdit;
		protected System.Web.UI.WebControls.Label lblDescEdit;
		protected System.Web.UI.WebControls.TextBox txtDescEdit;
		protected System.Web.UI.WebControls.TextBox txtModelCodeQuery;

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
			this.cmdGridExport.ServerClick += new System.EventHandler(this.cmdGridExport_ServerClick);
			this.cmdReturn.ServerClick += new System.EventHandler(this.cmdReturn_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		#region Init
		private void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				Initparameters();
				InitViewPanel();
			}
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		private void InitViewPanel()
		{
			this.txtModelCodeQuery.Text = ModelCode;
			this.txtModelCodeQuery.ReadOnly = true;
		}
		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
			this.gridHelper.AddColumn( "ModelCode", "产品别代码",	null);
			this.gridHelper.AddColumn( "ErrorSymptom", "不良症状",	null);
			this.gridHelper.AddColumn( "Description", "描述",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

			this.gridWebGrid.Columns.FromKey("ModelCode").Hidden = true;

			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridHelper.RequestData();
		}

		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			Model2ErrorSymptom m2es = obj as Model2ErrorSymptom;	
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								m2es.ModelCode.ToString(),
								m2es.SymptomCode.ToString(),
								m2es.Description.ToString(),
								m2es.MaintainUser.ToString(),
								FormatHelper.ToDateString(m2es.MaintainDate),
								FormatHelper.ToTimeString(m2es.MaintainTime),
								""});
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			return this._facade.QueryModel2ErrorSymptom( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ModelCode)),"",
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			return this._facade.QueryModel2ErrorSymptomCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ModelCode)),"");
		}

		#endregion

		#region Button
		private void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FModelMP.aspx"));
		}

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			
			this._facade.AddModel2ErrorSymptom( (Model2ErrorSymptom)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			this._facade.DeleteModel2ErrorSymptom( (Model2ErrorSymptom[])domainObjects.ToArray( typeof(Model2ErrorSymptom) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			this._facade.UpdateModel2ErrorSymptom( (Model2ErrorSymptom)domainObject );
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
			Model2ErrorSymptom m2es = this._facade.CreateNewModel2ErrorSymptom();

			m2es.ModelCode	= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ModelCode, 40));
			m2es.SymptomCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSymptomEdit.Text,40));
			m2es.Description	= FormatHelper.CleanString( this.txtDescEdit.Text, 200);
			m2es.MaintainUser			= this.GetUserCode();


			return m2es;
		}

		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			object obj = _facade.GetModel2ErrorSymptom(ModelCode, row.Cells.FromKey("ErrorSymptom").Text.ToString() );
			
			if (obj != null)
			{
				return (Model2ErrorSymptom)obj;
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

			Model2ErrorSymptom m2es = obj as Model2ErrorSymptom;
			this.txtSymptomEdit.Text = m2es.SymptomCode.ToString();
			this.txtDescEdit.Text = m2es.Description.ToString();
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
			Model2ErrorSymptom m2es = obj as Model2ErrorSymptom;	
			return new string[]{  m2es.ModelCode.ToString(),
								   m2es.SymptomCode.ToString(),
								   m2es.Description.ToString(),
								   m2es.MaintainUser.ToString(),
								   FormatHelper.ToDateString(m2es.MaintainDate),
								   FormatHelper.ToTimeString(m2es.MaintainTime)
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"ModelCode",
									"ErrorSymptom",
									"Description",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"
									};
		}
		#endregion

		#region property
		private void Initparameters()
		{
			if(this.Request.Params["modelcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["modelcode"] = this.Request.Params["modelcode"];
			}
		}

		private void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}


	
		public string ModelCode
		{
			get
			{
				return (string) this.ViewState["modelcode"];
			}
		}
		#endregion

		
	}
}
