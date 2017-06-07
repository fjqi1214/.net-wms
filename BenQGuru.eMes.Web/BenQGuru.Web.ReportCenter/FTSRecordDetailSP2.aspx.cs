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
using BenQGuru.eMES.Web.Helper;
using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FTSRecordDetailSP 的摘要说明。
	/// </summary>
	public partial class FTSRecordDetailSP2 : BasePage
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltraWebGrid1;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.CheckBox Checkbox1;
		protected BenQGuru.eMES.Web.Helper.PagerToolBar Pagertoolbar1;
		private BenQGuru.eMES.Web.Helper.GridHelperForRPT gridHelper = null;
		private BenQGuru.eMES.Web.Helper.ButtonHelper buttonHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.InitOnPostBack();

			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.InitWebGrid();
				object[] objs = new object[]{"false","ErrGroup001","Err001","Model001","OP1","MO-002","TS002","Res_AI","Route110","TSStation1","TSWorker","2004/05/09"};
				UltraGridRow Row = new UltraGridRow(objs);
				this.gridWebGrid.Rows.Add(Row);
				
			}
			// 在此处放置用户代码以初始化页面
		}

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
			this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";
			// 
			// excelExporter
			// 
			this.excelExporter.FileExtension = "xls";
			this.excelExporter.LanguageComponent = null;
			this.excelExporter.Page = null;
			this.excelExporter.RowSplit = "\r\n";

		}

		#region(Init)
		
		#endregion
		private void InitOnPostBack()
		{		
			this.buttonHelper = new ButtonHelper(this);
//			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

			this.gridHelper = new GridHelperForRPT(this.gridWebGrid);
//			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
//			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

//			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
			//			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			//			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			//			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
//			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}

		private void InitButton()
		{	
			this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			this.buttonHelper.AddDeleteConfirm();
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			// TODO: 调整字段值的顺序，使之与Grid的列对应

			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false","TS","","Model","Item111","MO00111","2005/04/02","Res_AI","2005/04/06","2005/04/08","Station11",""
							});
		}


		/// <summary>
		/// angel zhu add 
		/// Column内的Key现不能确定，以后需要补充完整
		/// </summary>
		private void InitWebGrid()
		{   
			this.gridHelper.GridHelper.AddColumn("a", "不良代码组",	null);
			this.gridHelper.GridHelper.AddColumn( "b", "不良代码",	null);
			this.gridHelper.GridHelper.AddColumn("c","不良原因",null);
			this.gridHelper.GridHelper.AddColumn( "d", "责任别",	null);
			this.gridHelper.GridHelper.AddColumn( "e", "解决方案",	null);
			this.gridHelper.GridHelper.AddColumn( "f", "不良位置",	null);
			this.gridHelper.GridHelper.AddColumn( "g", "故障元件",	null);
			this.gridHelper.GridHelper.AddColumn( "h", "更换元件",	null);
			this.gridHelper.GridHelper.AddColumn( "i", "维修站",	null);
			this.gridHelper.GridHelper.AddColumn( "j", "维修工",	null);
			this.gridHelper.GridHelper.AddColumn( "k", "维修日期",	null);
			this.gridHelper.GridHelper.AddDefaultColumn(true, false);

			//多语言
			this.gridHelper.GridHelper.ApplyLanguage( this.languageComponent1 );
		}

		#endregion
	}
}
