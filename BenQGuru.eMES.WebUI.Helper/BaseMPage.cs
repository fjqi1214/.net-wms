using System;
using System.Web.UI;
using System.Collections;

using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// BaseMPage 的摘要说明。
	/// </summary>
	public class BaseMPage : BasePage
	{
		public BaseMPage() : base()
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

			this.components = new System.ComponentModel.Container();

			this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
			this.excelExporter.Page = this;
			this.excelExporter.LanguageComponent = this.languageComponent;

			this.Load += new System.EventHandler(this.Page_Load);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.InitOnPostBack();

			if (!IsPostBack)
			{
				this.InitPageLanguage(this.languageComponent, false);
				
				this.InitButtons();
				this.InitWebGrid();
			}
		}

		private void InitOnPostBack()
		{			
			#region ButtonHelper
			this.buttonHelper = new ButtonHelper(this);
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
			this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

			if ( this.buttonHelper.CmdAdd != null )
			{
				this.buttonHelper.CmdAdd.ServerClick += new EventHandler(cmdAdd_Click);
			}

			if ( this.buttonHelper.CmdSelect != null )
			{
				this.buttonHelper.CmdSelect.ServerClick += new EventHandler(cmdSelect_Click);
			}

			if ( this.buttonHelper.CmdDelete != null )
			{			
				this.buttonHelper.CmdDelete.ServerClick += new EventHandler(cmdDelete_Click);
			}
			
			if ( this.buttonHelper.CmdSave != null )
			{			
				this.buttonHelper.CmdSave.ServerClick += new EventHandler(cmdSave_Click);
			}
			
			if ( this.buttonHelper.CmdCancel != null )
			{			
				this.buttonHelper.CmdCancel.ServerClick += new EventHandler(cmdCancel_Click);
			}
			
			if ( this.buttonHelper.CmdQuery != null )
			{			
				this.buttonHelper.CmdQuery.ServerClick += new EventHandler(cmdQuery_Click);
			}
			
			if ( this.buttonHelper.CmdExport != null )
			{			
				this.buttonHelper.CmdExport.ServerClick += new EventHandler(cmdExport_Click);
			}
			#endregion

			#region GridHelper
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.GetRowCountHandle = new GetRowCountDelegate(this.GetRowCount);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);	

			if ( this.gridHelper.Grid != null )
			{
				//this.gridHelper.Grid.DblClick += new ClickEventHandler(Grid_DblClick);
				this.gridHelper.Grid.ClickCellButton += new ClickCellButtonEventHandler(Grid_ClickCellButton);
			}
			#endregion

			#region Exporter
			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);			
			#endregion
		}

		private void InitButtons()
		{	
			this.buttonHelper.PageActionStatusHandle( PageActionType.Add );	
		}
		#endregion

		#region WebGrid
		// 双击Grid调用
		protected virtual void Grid_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			object obj = this.GetEditObject(e.Row);

			if ( obj != null )
			{
				this.SetEditObject( obj );

				this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
			}
		}

		// 单击Grid的CellButton调用
		protected virtual void Grid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			this.Grid_ClickCell( e.Cell );
		}

		protected virtual void Grid_ClickCell(Infragistics.WebUI.UltraWebGrid.UltraGridCell cell)
		{
			if ( this.gridHelper.IsClickEditColumn(cell) )
			{
				object obj = this.GetEditObject(cell.Row);

				if ( obj != null )
				{
					this.SetEditObject( obj );

					this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
				}
			}
		}
		#endregion

		#region Button
		/// <summary>
		/// 点击新增按钮时调用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void cmdAdd_Click(object sender, System.EventArgs e)
		{					
			if ( this.ValidateInput() )
			{
				object obj = this.GetEditObject();

				if ( obj == null )
				{
					return;
				}
				
				this.AddDomainObject( obj );
				
				this.gridHelper.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			}
		}

		/// <summary>
		/// 点击选择按钮时调用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void cmdSelect_Click(object sender, System.EventArgs e)
		{					
			if ( this.ValidateInput() )
			{
				object obj = this.GetEditObject();

				if ( obj == null )
				{
					return;
				}
				
				this.AddDomainObject( obj );
				
				this.gridHelper.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Select );
			}
		}

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
		/// 点击保存按钮时调用
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void cmdSave_Click(object sender, System.EventArgs e)
		{
			if ( this.ValidateInput() )
			{
				object obj = this.GetEditObject();

				if ( obj == null )
				{
					return;
				}

				this.UpdateDomainObject( obj );

				this.gridHelper.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
			}
		}

		/// <summary>
		/// 点击清空按钮时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );		
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

		/// <summary>
		/// 点击查询按钮时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void cmdExport_Click(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}
	
		/// <summary>
		/// 获得导出数据
		/// </summary>
		/// <returns></returns>
		protected object[] LoadDataSource()
		{
			return this.LoadDataSource( 1, int.MaxValue );
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
		
		/// <summary>
		/// 获得查询所得的数据总行数，需重载
		/// </summary>
		/// <returns></returns>
		protected virtual int GetRowCount()
		{
			return 0;
		}

		/// <summary>
		/// 新增一个DomainObject入数据库，需重载
		/// </summary>
		/// <param name="domainObject"></param>
		protected virtual void AddDomainObject(object domainObject)
		{
		}

		/// <summary>
		/// 更新一个DomainObject入数据库，需重载
		/// </summary>
		/// <param name="domainObject"></param>
		protected virtual void UpdateDomainObject(object domainObject)
		{
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
		/// 处理新增和更新状态变化后的编辑区可编辑性，需重载
		/// </summary>
		/// <param name="pageAction"></param>
		protected virtual void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				//				this.txtSegmentCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				//				this.txtSegmentCodeEdit.ReadOnly = true;
			}
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

		#region Object <--> Page

		/// <summary>
		/// 从编辑区获得输入值，组成DomainObject，需重载
		/// </summary>
		/// <returns></returns>
		protected virtual object GetEditObject()
		{
			return null;
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
		/// 将DomainObject填入编辑区，如果为null则清空页面，需重载
		/// </summary>
		/// <param name="obj"></param>
		protected virtual void SetEditObject(object obj)
		{
		}
		
		/// <summary>
		/// 验证编辑区各输入值的有效性
		/// 如必填值是否为空，长度是否超出限制，输入格式是否正确...
		/// </summary>
		protected virtual bool ValidateInput()
		{
			return true;
		}

		#endregion	

		#endregion
	}
}
