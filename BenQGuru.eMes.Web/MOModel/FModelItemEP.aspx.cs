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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FModelItemEP 的摘要说明。
	/// </summary>
	public partial class FModelItemEP : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		//private ItemFacade _itemFacade = FacadeFactory.CreateItemFacade();
		private ModelFacade _modelFacade;// = new FacadeFactory(base.DataProvider).CreateModelFacade();
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
	
		

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
			this.excelExporter.LanguageComponent = this.languageComponent1;
			this.excelExporter.Page = this;

		}
		#endregion

		#region page events
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitHanders();
			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				// 初始化界面UI
				this.InitUI();
				this.InitParameters();
				this.InitWebGrid();
				this.InitButton();
				this.RequestData();
				//this.pagerSizeSelector.Readonly = true;
			}
		}

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FModelItemSP.aspx" ,new string[] {"modelcode"},new string[] { FormatHelper.CleanString(this.txtModelCodeQuery.Text.Trim())}));
		}

		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{
				ArrayList items = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					object item = this.GetEditObject(row);
					if( item != null )
					{
						items.Add( (Model2Item)item );
					}
				}

				this._modelFacade.RemoveItemsFromModel( (Model2Item[])items.ToArray( typeof(Model2Item) ) );

				this.RequestData();
			}
		}

		private void RequestData()
		{
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}

		protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( this.chbSelectAll.Checked )
			{
				this.gridHelper.CheckAllRows( CheckStatus.Checked );
			}
			else
			{
				this.gridHelper.CheckAllRows( CheckStatus.Unchecked );
			}
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FModelMP.aspx"));
		}
		#endregion

		#region private method
		private void InitHanders()
		{
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.buttonHelper = new ButtonHelper(this);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}
		private void InitParameters()
		{
			if(this.Request.Params["modelcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				//sammer kong 20050411
				if( this.Request.Params["modelcode"] != null )
				{
					this.txtModelCodeQuery.Text = this.Request.Params["modelcode"].ToString();
				}
			}
		}

		public void InitButton()
		{	
			this.buttonHelper.AddDeleteConfirm();
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "ItemCode", "产品代码",	null);
			this.gridHelper.AddColumn( "ItemName",		 "产品名称",		null);
			this.gridHelper.AddColumn( "ItemType", "产品类别",	null);
//			this.gridHelper.AddColumn( "Model",		 "产品别代码",	null);
//			this.gridWebGrid.Columns.FromKey("Model").Hidden = true;
			this.gridHelper.AddColumn( "UOM",  "计量单位",	null);
//			this.gridHelper.AddColumn( "ItemVersion",		 "产品版本",	null);
//			this.gridHelper.AddColumn( "ItemControlType",		 "管控方式",	null);
//			this.gridHelper.AddColumn( "ItemUser",	 "开立人员",	null);
//			this.gridHelper.AddColumn( "ItemDate",    "开立日期",		null);
			this.gridHelper.AddColumn( "Description",    "产品描述",		null);
			this.gridHelper.AddColumn("MUSER","维护人员",null);
			this.gridHelper.AddColumn("MDATE","维护日期",null);

			this.gridHelper.AddDefaultColumn( true, false );
			//this.gridHelper.ApplyDefaultStyle();

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((Item)obj).ItemCode.ToString(),
								((Item)obj).ItemName.ToString(),
								this.languageComponent1.GetString( ((Item)obj).ItemType.ToString()),
								//((Item)obj).ModelCode.ToString(),
								((Item)obj).ItemUOM.ToString(),
//								((Item)obj).ItemVersion.ToString(),
//								((Item)obj).ItemControlType.ToString(),
//								((Item)obj).ItemUser.ToString(),
//								FormatHelper.ToDateString(((Item)obj).ItemDate),
								((Item)obj).ItemDescription.ToString(),
								((Item)obj).MaintainUser.ToString(),
								FormatHelper.ToDateString(((Item)obj).MaintainDate)

								});
		}

		private string[] FormatExportRecord( object obj )
		{
			return new string[]{
								   ((Item)obj).ItemCode.ToString(),
								   ((Item)obj).ItemName.ToString(),
								   this.languageComponent1.GetString( ((Item)obj).ItemType.ToString()),
								   ((Item)obj).ItemUOM.ToString(),
								   ((Item)obj).ItemDescription.ToString(),
								   ((Item)obj).MaintainUser.ToString(),
								   FormatHelper.ToDateString(((Item)obj).MaintainDate)
							   };
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"ItemCode",
									"ItemName",
				                    "ItemType",
				                    "UOM",
				                    "Description",
				"MUSER","MDATE"
								};
		}

		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			Model2Item model2Item = this._modelFacade.CreateModel2Item();
			model2Item.ModelCode = this.txtModelCodeQuery.Text;
			model2Item.ItemCode = row.Cells[1].Text;
			model2Item.MaintainUser = this.GetUserCode();
            model2Item.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
			return model2Item;
		}



		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._modelFacade.GetSelectedItems(
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModelCodeQuery.Text)),
				string.Empty,
				inclusive, exclusive );
		}

		private object[] LoadDataSource()
		{
			return this.LoadDataSource(1,int.MaxValue);
		}

		private void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			this.RequestData();
		}
	
		private int GetRowCount()
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._modelFacade.GetSelectedItemsCounts(
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModelCodeQuery.Text)),
				string.Empty
				);
		}
		#endregion		

		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}
	}
}
