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
using BenQGuru.eMES.Domain.RMA;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FOperation2ResourceSP 的摘要说明。
	/// </summary>
	public partial class FModel2ErrorSymptomSP : BaseSPage
	{

		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Label lblErrorGroupCode;

		private RMAFacade _facade ;//= TSModelFacadeFactory.CreateTSModelFacade();

		#region Stable
		protected void Page_Load(object sender, System.EventArgs e)
		{		
			//this.pagerSizeSelector.Readonly = true;

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.txtModelCodeQuery.Text = this.GetRequestParam("Modelcode");				
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
			this.gridHelper.AddColumn( "ErrorSymptom", "解决方案代码",	null);
			this.gridHelper.AddColumn( "Description", "解决方案描述",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
			this.gridHelper.AddDefaultColumn( true, false );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridHelper.RequestData();
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			_facade.DeleteModel2ErrorSymptom( (Model2ErrorSymptom[])domainObjects.ToArray(typeof(Model2ErrorSymptom)));
		}

		protected override int GetRowCount()
		{			
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			return this._facade.GetSelectedErrorSymptomByModelCodeCount( 
				FormatHelper.PKCapitalFormat(this.txtModelCodeQuery.Text.Trim()),
				FormatHelper.PKCapitalFormat(this.txtSolutionCodeQuery.Text.Trim()));
		}

		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((ErrorSymptom)obj).SymptomCode.ToString(),
								((ErrorSymptom)obj).Description.ToString(),
								((ErrorSymptom)obj).MaintainUser.ToString(),
								FormatHelper.ToDateString(((ErrorSymptom)obj).MaintainDate),
								FormatHelper.ToTimeString(((ErrorSymptom)obj).MaintainTime)});
		}

		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{
								   ((ErrorSymptom)obj).SymptomCode.ToString(),
								   ((ErrorSymptom)obj).Description.ToString(),
								   ((ErrorSymptom)obj).MaintainUser.ToString(),
								   FormatHelper.ToDateString(((ErrorSymptom)obj).MaintainDate)
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"ErrorSymptom",
									"Description",
									"MaintainUser",
									"MaintainDate"};
		}

		protected override object GetEditObject(UltraGridRow row)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			Model2ErrorSymptom m2es = _facade.CreateNewModel2ErrorSymptom();
			m2es.ModelCode = this.txtModelCodeQuery.Text.Trim();
			m2es.SymptomCode = row.Cells.FromKey("ErrorSymptom").Text;		
			m2es.MaintainUser = this.GetUserCode();

			return m2es;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{			
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateRMAFacade();}
			return this._facade.GetSelectedErrorSymptomByModelCode( 
				FormatHelper.PKCapitalFormat(this.txtModelCodeQuery.Text.Trim()),
				FormatHelper.PKCapitalFormat(this.txtSolutionCodeQuery.Text.Trim()),
				inclusive,exclusive);
		}

		protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("FModel2ErrorSymptomAP.aspx", new string[]{"Modelcode"}, new string[]{this.txtModelCodeQuery.Text.Trim()}));
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("FModelMP.aspx"));
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
