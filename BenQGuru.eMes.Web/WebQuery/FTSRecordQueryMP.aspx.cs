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
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Web.UserControl;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FTSRecordQueryMP 的摘要说明。
	/// </summary>
	public partial class FTSRecordQueryMP : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
		private BenQGuru.eMES.Web.Helper.ButtonHelper buttonHelper = null;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		protected BenQGuru.eMES.BaseSetting.BaseModelFacade BaseMode_Facade = null;// new BaseModelFacade();


		protected void Page_Load(object sender, System.EventArgs e)
		{
		    this.InitOnPostBack();

			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.InitWebGrid();
	            txtDateFrom.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Now));
				txtDateTo.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Now));
                object[] objs = new object[]{"false","No1111","TS","","Model","Item111","MO00111","2005/04/02","Res_AI","2005/04/06","2005/04/08","Station11",""};
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
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}

		#region(Init)

		private void InitOnPostBack()
		{		
			this.buttonHelper = new ButtonHelper(this);
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
//			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
//			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
//			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
//			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
//		   this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}

		private void InitButton()
		{	
			this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			this.buttonHelper.AddDeleteConfirm();
		}


//		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow()
//		{
//			// TODO: 调整字段值的顺序，使之与Grid的列对应
//		}


		/// <summary>
		/// angel zhu add 
		/// Column内的Key现不能确定，以后需要补充完整
		/// </summary>
		private void InitWebGrid()
		{   	
			this.gridHelper.AddColumn("a", "序列号",	null);
			this.gridHelper.AddColumn( "b", "维修状态",	null);
			this.gridHelper.AddLinkColumn("c","详细信息",null);
			this.gridHelper.AddColumn( "d", "产品别",	null);
			this.gridHelper.AddColumn( "e", "产品",	null);
			this.gridHelper.AddColumn( "f", "工单",	null);
			this.gridHelper.AddColumn( "g", "不良日期",	null);
			this.gridHelper.AddColumn( "h", "来源站",	null);
			this.gridHelper.AddColumn( "i", "进站日期",	null);
			this.gridHelper.AddColumn( "j", "出站日期",	null);
			this.gridHelper.AddColumn( "k", "去向站",	null);

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}


		protected void drpModel_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DropDownListBuilder builder = new DropDownListBuilder(this.drpModel);
				
				ModelFacade Model_Facade = new FacadeFactory(base.DataProvider).CreateModelFacade() ;
				builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(Model_Facade.GetAllModels);
				builder.Build("ModelCode", "ModelCode");
				this.drpModel.Items.Insert(0, "");
			}

		
		}

		protected void drpItem_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DropDownListBuilder builder = new DropDownListBuilder(this.drpItem);
				ItemFacade Item_Facade = new FacadeFactory(base.DataProvider).CreateItemFacade();
				builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(Item_Facade.GetAllItem);
				builder.Build("ItemCode", "ItemCode");
				this.drpItem.Items.Insert(0, "");
			}
		}

		protected void drpMo_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DropDownListBuilder builder = new DropDownListBuilder(this.drpMo);
				MOFacade Mo_Facade = new FacadeFactory(base.DataProvider).CreateMOFacade() ;
				builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(Mo_Facade.GetAllMO);
				builder.Build("MOCode", "MOCode");
				this.drpMo.Items.Insert(0, "");
			}
			
		}

		
		protected void drpOperation_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DropDownListBuilder builder = new DropDownListBuilder(this.drpOperation);
				if(BaseMode_Facade==null)
				{
					BaseMode_Facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade() ;
				}
				builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(BaseMode_Facade.GetAllOperation);
				builder.Build("OPCode", "OPCode");
				this.drpOperation.Items.Insert(0, "");
			}
		}

        
		protected void drpSegment_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				DropDownListBuilder builder = new DropDownListBuilder(this.drpSegment);
				if(BaseMode_Facade==null)
				{
					BaseMode_Facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade() ;
				}
				builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(BaseMode_Facade.GetAllSegment);
				builder.Build("SegmentCode", "SegmentCode");
				this.drpSegment.Items.Insert(0, "");
			}

		}

		protected void drpStepSequence_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{				
				DropDownListBuilder builder = new DropDownListBuilder(this.drpStepSequence);
				if(BaseMode_Facade==null)
				{
					BaseMode_Facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade() ;
				}
				builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(BaseMode_Facade.GetAllStepSequence);
				builder.Build("StepSequenceCode", "StepSequenceCode");
				this.drpStepSequence.Items.Insert(0, "");
			}
		}

		protected void drpResource_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{				
				DropDownListBuilder builder = new DropDownListBuilder(this.drpResource);
				if(BaseMode_Facade==null)
				{
					BaseMode_Facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade() ;
				}
				builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(BaseMode_Facade.GetAllResource);
				builder.Build("ResourceCode", "ResourceCode");
				this.drpResource.Items.Insert(0, "");
			}
		}

		#endregion

//		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
//		{
//			// TODO: 调整字段值的顺序，使之与Grid的列对应
//		}

		private int GetRowCount()
		{
			return 0 ;
		}

		
		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			return null;
		}

		private void RequestData()
		{
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}
		#endregion

		#region Object <--> Page
		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			
			return null;
		}

		private object GetEditObject()
		{

		    return null;
		}

		private void SetEditObject(object obj)
		{
			
		}

		private object[] GetColumnHeaderText()
		{
			return new object[] {	"序列号",
									"维修状态",
									"详细信息",
									"产品别",
									"产品",
									"工单",
									"不良日期",
									"来源站",
			                        "进站日期",
			                        "出站日期",
			                        "去向站"};
		}
	
	  	#endregion

       
		#region(DrpDownList Change Event)

		protected void drpModel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			drpItem.Items.Clear();	
							
			DropDownListBuilder builder = new DropDownListBuilder(this.drpItem);
			builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(GetItemByModel);
			builder.Build("ItemCode", "ItemCode");
			this.drpItem.Items.Insert(0, "");
		
		}

		private object[] GetItemByModel()
		{
			return null;
//              return(Item_Facade.QueryItem("","",drpModel.SelectedValue,"",1,int.MaxValue));
		}

		protected void drpItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			drpMo.Items.Clear();
							
			DropDownListBuilder builder = new DropDownListBuilder(this.drpMo);
			builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(GetMOByItem);
			builder.Build("MOCode", "MOCode");
			this.drpMo.Items.Insert(0, "");
				
		}

		private object[] GetMOByItem()
		{
			return null;
//            return(this.Mo_Facade.QueryMO("",drpItem.SelectedValue,"","","",1,int.MaxValue));
		}

		protected void drpMo_SelectedIndexChanged(object sender, System.EventArgs e)
		{ 
			drpOperation.Items.Clear();

			DropDownListBuilder builder = new DropDownListBuilder(this.drpOperation);
			builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(GetMOByOperation);
			builder.Build("OPCode", "OPCode");
			this.drpOperation.Items.Insert(0, "");
			
					
		}

		private object[] GetMOByOperation()
		{
			MOFacade Mo_Facade = new FacadeFactory(base.DataProvider).CreateMOFacade() ;
			object[] Routes = Mo_Facade.QueryMORoutes(drpMo.SelectedValue,"");
			object[] Operation = null;
			if(Routes==null)
			{
				return null;
			}
			if(BaseMode_Facade==null)
			{
				BaseMode_Facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade() ;
			}
			foreach(BenQGuru.eMES.Domain.BaseSetting.Route routes in Routes)
			{
				Operation = this.BaseMode_Facade.GetOperationByRouteCode(routes.RouteCode);
			}
            
			return Operation;
		}

		protected void drpSegment_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		    drpStepSequence.Items.Clear();
							
			DropDownListBuilder builder = new DropDownListBuilder(this.drpStepSequence);
			builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(GetStepSequenceBySegment);
			builder.Build("StepSequenceCode", "StepSequenceCode");
			this.drpSegment.Items.Insert(0, "");
			
					
		}

		private object[] GetStepSequenceBySegment()
		{
			if(BaseMode_Facade==null)
			{
				BaseMode_Facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade() ;
			}
			return(BaseMode_Facade.QueryStepSequence("",drpSegment.SelectedValue,1,int.MaxValue));
		}


		protected void drpStepSequence_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		    drpResource.Items.Clear();
							
			DropDownListBuilder builder = new DropDownListBuilder(this.drpResource);
			builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(GetResourceByStepSequence);
			builder.Build("ResourceCode", "ResourceCode");
			this.drpResource.Items.Insert(0, "");
			
			
		}

		private object[]  GetResourceByStepSequence()
		{
			if(BaseMode_Facade==null)
			{
				BaseMode_Facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade() ;
			}
			return(BaseMode_Facade.GetResourceByStepSequenceCode(drpStepSequence.SelectedValue));
		}

		#endregion	

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if(e.Cell.Column.HeaderText == "详细信息")
			{
				Response.Redirect("FTSRecordDetailSP.aspx");
			}
		}



	}
}
