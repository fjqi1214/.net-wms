using System;
using System.Web.UI;
using System.Collections;

using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// BaseSPage 的摘要说明。
	/// </summary>
	public class BaseSPage : BasePage
	{
		public BaseSPage() : base()
		{
		}
		
		protected GridHelper gridHelper = null;
		protected ButtonHelper buttonHelper = null;
		protected ExcelExporter excelExporter = null;

		private Infragistics.WebUI.UltraWebGrid.UltraWebGrid gridWebGrid;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent;
		private System.ComponentModel.IContainer components;

		#region Init
		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{	
			this.gridWebGrid = this.GetWebGrid();
			this.languageComponent = this.GetLanguageComponent();

			this.Load += new System.EventHandler(this.Page_Load);

			this.components = new System.ComponentModel.Container();
			this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
			this.excelExporter.Page = this;
			this.excelExporter.LanguageComponent = this.languageComponent;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.InitOnPostBack();

			if (!IsPostBack)
			{
				this.InitPageLanguage(this.languageComponent, false);

				this.InitWebGrid();
			}
		}

		private void InitOnPostBack()
		{			
			this.buttonHelper = new ButtonHelper(this);

			if ( this.buttonHelper.CmdDelete != null )
			{			
				this.buttonHelper.CmdDelete.ServerClick += new EventHandler(cmdDelete_Click);
			}
			
			if ( this.buttonHelper.CmdQuery != null )
			{			
				this.buttonHelper.CmdQuery.ServerClick += new EventHandler(cmdQuery_Click);
			}
			if ( this.buttonHelper.CmdExport != null )
			{			
				this.buttonHelper.CmdExport.ServerClick += new EventHandler(cmdExport_Click);
			}

			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.GetRowCountHandle = new GetRowCountDelegate(this.GetRowCount);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			#region Exporter
			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);			
			#endregion			
		}
		#endregion

		#region Button
		/// <summary>
		/// 点击删除按钮时调用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void cmdDelete_Click(object sender, System.EventArgs e)
		{
			ArrayList array = this.gridHelper.GetCheckedRows();
			object obj = null;

			if ( array.Count > 0 )
			{
				ArrayList objList = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					obj = this.GetEditObject(row);

					if ( obj != null )
					{
						objList.Add( obj );
					}
				}

				this.DeleteDomainObjects( objList );

				this.gridHelper.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
			}
		}

		/// <summary>
		/// 点击查询按钮时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void cmdQuery_Click(object sender, System.EventArgs e)
		{
			this.gridHelper.RequestData();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Query );
		}

		protected virtual void cmdExport_Click(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}
		#endregion

		#region override

		#region Control
		/// <summary>
		/// 返回UltraWebGrid，如果Grid名称为gridWebGrid，不用重载
		/// </summary>
		/// <returns></returns>
		protected virtual Infragistics.WebUI.UltraWebGrid.UltraWebGrid GetWebGrid()
		{
			Control ctrl = this.FindControl("gridWebGrid");

			if ( ctrl != null )
			{
				return (UltraWebGrid)ctrl;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 返回LanguageComponent，需重载
		/// </summary>
		/// <returns></returns>
		protected virtual ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return null;
		}
		#endregion
		
		#region CRUD
		/// <summary>
		/// 获得查询所得的分页数据，需重载
		/// </summary>
		/// <param name="inclusive"></param>
		/// <param name="exclusive"></param>
		/// <returns></returns>
		protected virtual object[] LoadDataSource(int inclusive, int exclusive)
		{
			return null;
		}	

		protected virtual object[] LoadDataSource()
		{
			return this.LoadDataSource(1,System.Int32.MaxValue);
		}
				
		/// <summary>
		/// 获得查询所得的数据总行数，需重载
		/// </summary>
		/// <returns></returns>
		protected virtual int GetRowCount()
		{
			return 0;
		}

		/// <summary>
		/// 从数据库删除多个DomainObject，需重载
		/// </summary>
		/// <param name="domainObject"></param>
		protected virtual void DeleteDomainObjects(ArrayList domainObjects)
		{
		}

		#endregion

		#region Format Data
		/// <summary>
		/// 初始化WebGrid，需重载，并在函数最后调用base.InitWebGrid();
		/// </summary>
		protected virtual void InitWebGrid()
		{
			this.gridHelper.ApplyLanguage( this.languageComponent );
		}
		
		/// <summary>
		/// 将object各字段组成UltraGridRow，需重载
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		protected virtual Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow();
		}

		/// <summary>
		/// 从UltraGridRow获得输入值，组成DomainObject，需重载
		/// </summary>
		/// <returns></returns>
		protected virtual object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			return null;
		}

		/// <summary>
		/// 格式化object的各字段成字符串，用于导出数据，需重载
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		protected virtual string[] FormatExportRecord( object obj )
		{
			return null;
		}

		/// <summary>
		/// 输出object各字段的名称，作为导出数据列的标题，需重载
		/// </summary>
		/// <returns></returns>
		protected virtual string[] GetColumnHeaderText()
		{
			return null;
		}
		#endregion

		#endregion
	}
}
