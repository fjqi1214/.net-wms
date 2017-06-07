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
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// FSMTBOMCompareDif 的摘要说明。
	/// </summary>
	public class FSMTBOMCompareDif  : BasePage
	{
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid gridWebGrid;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid gridInMOBom;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid gridInStationBom;
		protected BenQGuru.eMES.Web.Helper.PagerSizeSelector pagerSizeSelector;
		//protected BenQGuru.eMES.Web.Helper.PagerToolBar pagerToolBar;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdReturn;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		private GridHelper gridHelper = null;
		private GridHelper gridInMOBomHelper = null;
		private GridHelper gridInStationBomHelper = null;
		private ExcelExporter excelExporter = null;
		private ButtonHelper buttonHelper = null;

		//private BenQGuru.eMES.SMT.SMTFacade _facade ;//= new SMTFacadeFactory().Create();
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdExport;
		private BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter1;
		private BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter2;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdMOOpen;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdMOClose;
		protected System.Web.UI.WebControls.Label lblTitles;
		//private BenQGuru.eMES.MOModel.MOFacade _modelFacade ;//= SMTFacadeFactory.CreateMOFacade();
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			InitHanders();
			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
				// 初始化界面UI
				this.InitUI();
				//InitParameters();		//接收参数方法
				this.InitWebGrid();
				this.RequestData();		//加载数据
			}
			
		}


		private void InitParameters()
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


		private void InitHanders()
		{
			//gridInMOBom					gridInMOBomHelper
			//gridInStationBom				gridInStationBomHelper
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.gridInMOBomHelper = new GridHelper(this.gridInMOBom);
			this.gridInMOBomHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadMOBOMDataSource);
			this.gridInMOBomHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridMOBOMRow);

			this.gridInStationBomHelper = new GridHelper(this.gridInStationBom);
			this.gridInStationBomHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadStationBOMDataSource);
			this.gridInStationBomHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridStationBOMRow);

			this.buttonHelper = new ButtonHelper(this);
			
			//导出只存在于站表中的数据
			this.excelExporter.LoadExportDataNoPageHandle = new LoadExportDataDelegateNoPage(this.GetInSationBOMobj);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);	

			//导出成功的数据 
			this.excelExporter1.LoadExportDataNoPageHandle = new LoadExportDataDelegateNoPage(this.GetSucessObj);
			this.excelExporter1.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatSucessExportRecord);
			this.excelExporter1.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetSucessColumnHeaderText);	

			//导出只存在于工单物料清单的数据
			this.excelExporter2.LoadExportDataHandle = new LoadExportDataDelegate(this.LoadMOBOMDataSource);
			this.excelExporter2.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatMOBOMExportRecord);
			this.excelExporter2.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetMOBOMColumnHeaderText);	

		}

		#region GetGridRow
		//比对成功
		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								((StationBOM)obj).MOCode.ToString(),
								((StationBOM)obj).ResourceCode.ToString(),
								((StationBOM)obj).StationCode.ToString(),
								((StationBOM)obj).FeederCode.ToString(),
								((StationBOM)obj).OBItemCode.ToString(),
								((StationBOM)obj).CompareResult.ToString()
							});
		}
	
		//只存在于站表中
		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridStationBOMRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								((StationBOM)obj).MOCode.ToString(),
								((StationBOM)obj).ResourceCode.ToString(),
								((StationBOM)obj).StationCode.ToString(),
								((StationBOM)obj).FeederCode.ToString(),
								((StationBOM)obj).OBItemCode.ToString(),
								((StationBOM)obj).CompareResult.ToString()
							});
		}

		//只存在于物料清单
		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridMOBOMRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								((MOItemBOM)obj).MOCode.ToString(),
								((MOItemBOM)obj).ItemCode.ToString(),
								((MOItemBOM)obj).OBItemCode.ToString(),
								((MOItemBOM)obj).OBItemName.ToString(),
								((MOItemBOM)obj).OBItemQTY.ToString(),
								((MOItemBOM)obj).OBItemUnit.ToString(),
								((MOItemBOM)obj).CompareResult.ToString()
							});
		}

		#endregion

		protected  void InitWebGrid()
		{
			//比对成功
			this.gridHelper.AddColumn( "MOCODE", "工单",	null);
			this.gridHelper.AddColumn( "RESCODE", "机台编码",	null);
			this.gridHelper.AddColumn( "StationCode", "站位编号",	null);
			this.gridHelper.AddColumn( "FEEDERCODE", "料架规格代码",	null);
			this.gridHelper.AddColumn( "OBITEMCODE", "料号",	null);
			this.gridHelper.AddColumn( "CompareResult", "比对结果",	null);
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			//只存在于站表中
			this.gridInStationBomHelper.AddColumn( "MOCODE", "工单",	null);
			this.gridInStationBomHelper.AddColumn( "RESCODE", "机台编码",	null);
			this.gridInStationBomHelper.AddColumn( "StationCode", "站位编号",	null);
			this.gridInStationBomHelper.AddColumn( "FEEDERCODE", "料架规格代码",	null);
			this.gridInStationBomHelper.AddColumn( "OBITEMCODE", "料号",	null);
			this.gridInStationBomHelper.AddColumn( "CompareResult", "比对结果",	null);
			this.gridInStationBom.Columns.FromKey("MOCODE").Hidden = true ;	
			this.gridInStationBomHelper.ApplyLanguage( this.languageComponent1 );

			//只存在于物料清单
			this.gridInMOBomHelper.AddColumn( "MOCODE", "工单",	null);
			this.gridInMOBomHelper.AddColumn( "ITEMCODE", "产品编码",	null);
			this.gridInMOBomHelper.AddColumn( "OBITEMCODE", "子阶料号",	null);
			this.gridInMOBomHelper.AddColumn( "OBITEMNAME", "子阶料名称",	null);
			this.gridInMOBomHelper.AddColumn( "OBITEMQTY", "单机用量",	null);
			this.gridInMOBomHelper.AddColumn( "OBITEMUNIT", "计量单位",	null);
			this.gridInMOBomHelper.AddColumn( "CompareResult", "比对结果",	null);
			this.gridInMOBomHelper.ApplyLanguage( this.languageComponent1 );

		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			return GetSucessObj();
		}

		private object[] LoadMOBOMDataSource(int inclusive, int exclusive)
		{
			return GetInMOBOMObj();
		}
		private object[] LoadStationBOMDataSource(int inclusive, int exclusive)
		{
			return GetInSationBOMobj();
		}

		private object[] LoadMOBOMDataSource()
		{
			return this. LoadMOBOMDataSource(1, 20);
		}

		#region LoadSourece
		//比对成功
		private object[] GetSucessObj()
		{
			Hashtable SessionCompareHT = (Hashtable)Session["SessionCompareHT"];
			if(SessionCompareHT!=null)
			{
				ArrayList returnObjList = (ArrayList)SessionCompareHT["SucessResult"];
				if(returnObjList.Count >0)
				return (StationBOM[])returnObjList.ToArray(typeof(StationBOM) ) ;
			}
			return new object[]{};
		}
	
		//只存在于站表中
		private object[] GetInSationBOMobj()
		{
			Hashtable SessionCompareHT = (Hashtable)Session["SessionCompareHT"];
			if(SessionCompareHT!=null)
			{
				ArrayList returnObjList = (ArrayList)SessionCompareHT["InStationResult"];
				if(returnObjList.Count >0)
					return (StationBOM[])returnObjList.ToArray(typeof(StationBOM) ) ;
			}
			return new object[]{};

			#region 测试数据
			//			ArrayList returnObjList = new ArrayList();
			//			BenQGuru.eMES.SMT.StationBOM stBom = new StationBOM();
			//			stBom.MOCode = "工单123";
			//			stBom.ResourceCode = "机台456";
			//			stBom.FeederCode = "Feeder哈哈哈";
			//			stBom.StationCode = "站位一一以";
			//			stBom.OBItemCode = "面板ddd";
			//			stBom.CompareResult = "比对成功";
			//
			//			returnObjList.Add(stBom);
			//			return (StationBOM[])returnObjList.ToArray(typeof(StationBOM) ) ;
			#endregion
		}
		//只存在于物料清单
		private object[] GetInMOBOMObj()
		{

			Hashtable SessionCompareHT = (Hashtable)Session["SessionCompareHT"];
			if(SessionCompareHT!=null)
			{
				ArrayList returnObjList = (ArrayList)SessionCompareHT["InMoBOMResult"];
				if(returnObjList.Count >0)
					return (MOItemBOM[])returnObjList.ToArray(typeof(MOItemBOM) ) ;
			}
			return new object[]{};


		}


		#endregion

	
		//获取数据
		private void RequestData()
		{
			this.gridHelper.GridBind(1, 20);
			this.gridInMOBomHelper.GridBind(1, 20);
			this.gridInStationBomHelper.GridBind(1, 20);
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
			this.excelExporter1 = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
			this.excelExporter2 = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
			this.cmdMOClose.ServerClick += new System.EventHandler(this.cmdSucessExport_ServerClick);
			this.cmdMOOpen.ServerClick += new System.EventHandler(this.cmdInMOBOMExport_ServerClick);
			this.cmdExport.ServerClick += new System.EventHandler(this.cmdGridExport_ServerClick);
			this.cmdReturn.ServerClick += new System.EventHandler(this.cmdReturn_ServerClick);
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
			this.excelExporter.CellSplit = ",";
			this.excelExporter.FileExtension = "csv";
			this.excelExporter.LanguageComponent = this.languageComponent1;
			this.excelExporter.Page = this;
			this.excelExporter.RowSplit = "";
			// 
			// excelExporter1
			// 
			this.excelExporter1.CellSplit = ",";
			this.excelExporter1.FileExtension = "csv";
			this.excelExporter1.LanguageComponent = this.languageComponent1;
			this.excelExporter1.Page = this;
			this.excelExporter1.RowSplit = "";
			// 
			// excelExporter2
			// 
			this.excelExporter2.CellSplit = ",";
			this.excelExporter2.FileExtension = "csv";
			this.excelExporter2.LanguageComponent = this.languageComponent1;
			this.excelExporter2.Page = this;
			this.excelExporter2.RowSplit = "";
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			this.RequestData();
		}


		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
//			Model2Route model2Route = this._modelFacade.CreateNewModel2Route();
//			model2Route.ModelCode = ModelCode;
//			model2Route.RouteCode = row.Cells[1].Text;
//			model2Route.MaintainUser = this.GetUserCode();
//
//			return model2Route;
			return null;
		}

		private void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Session.Remove("SessionCompareHT");
			//返回 , 应该调用客户端脚本返回
			this.ExecuteClientFunction("Close","");
		}

		
		#region Export

		private void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.RowSplit = "\r\n" ;
			this.excelExporter.Export();
		}

		
		private void cmdSucessExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter1.RowSplit = "\r\n" ;
			this.excelExporter1.Export();
		}

		private void cmdInMOBOMExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter2.RowSplit = "\r\n" ;
			this.excelExporter2.Export();
		}

		#region 导出比对成功的数据

		protected  string[] FormatSucessExportRecord( object obj )
		{
			//导出数据 有Feeder栏位
			return new string[]{  
								   ((StationBOM)obj).MOCode.ToString(),
								   ((StationBOM)obj).ResourceCode.ToString(),
								   ((StationBOM)obj).StationCode.ToString(),
								   ((StationBOM)obj).FeederCode.ToString(),
								   ((StationBOM)obj).OBItemCode.ToString(),
			};
			
		
		}


		protected  string[] GetSucessColumnHeaderText()
		{
			//导出数据 有Feeder栏位
			return new string[] {   
									"MOCode",
									"ResourceCode1",
									"StationCode",
									"FeederCode",
									"MaterialItemCode"
								};
		}

		#endregion

		#region 导出站表的数据
		protected  string[] FormatExportRecord( object obj )
		{
			//导出数据 有Feeder栏位
			return new string[]{  
								   ((StationBOM)obj).ResourceCode.ToString(),
								   ((StationBOM)obj).StationCode.ToString(),
								   ((StationBOM)obj).FeederCode.ToString(),
								   ((StationBOM)obj).OBItemCode.ToString(),
								   ((StationBOM)obj).CompareResult.ToString()
							   };
			
		
		}


		protected  string[] GetColumnHeaderText()
		{
			//导出数据 有Feeder栏位
			return new string[] {   
									"ResourceCode1",
									"StationCode",
									"FeederCode",
									"MaterialItemCode",
									"CompareResult"
								};
		}
		#endregion

		#region 导出物料清单的数据
		protected  string[] FormatMOBOMExportRecord( object obj )
		{
			//导出数据 有Feeder栏位
			return new string[]{  
								   ((MOItemBOM)obj).MOCode.ToString(),
								   ((MOItemBOM)obj).ItemCode.ToString(),
								   ((MOItemBOM)obj).OBItemCode.ToString(),
								   ((MOItemBOM)obj).OBItemName.ToString(),
								   ((MOItemBOM)obj).OBItemQTY.ToString(),
								   ((MOItemBOM)obj).OBItemUnit.ToString()
			};
			
		
		}


		protected  string[] GetMOBOMColumnHeaderText()
		{
			//导出数据 有Feeder栏位
			return new string[] {   
									"MOCode",
									"ItemCode",
									"OBItemCode",
									"OBItemName",
									"OBItemQTY",
									"OBItemUnit"
								};
		}
		#endregion

		#endregion
		
		#region 私有方法

		/// <summary>
		/// 执行客户端的函数
		/// </summary>
		/// <param name="FunctionName">函数名</param>
		/// <param name="FunctionParam">参数</param>
		/// <param name="Page">当前页面的引用</param>
		public  void ExecuteClientFunction(string FunctionName,string FunctionParam)
		{
			try
			{
				string _msg = string.Empty;
				if(FunctionParam != string.Empty)
					_msg = string.Format("<script language='JavaScript'>  {0}('{1}');</script>",FunctionName,FunctionParam);
				else
					_msg = string.Format("<script language='JavaScript'>  {0}();</script>",FunctionName);

				//将Key值设为随机数,防止脚本重复
				Page.RegisterStartupScript(Guid.NewGuid().ToString(),_msg);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void Alert(string msg)
		{
			msg = msg.Replace("'","");
			msg = msg.Replace("\r","");
			msg = msg.Replace("\n","");
			string _msg = string.Format("<script language='JavaScript'>  alert('{0}');</script>",msg);
			Page.RegisterStartupScript("",_msg);
		}

		#endregion



	}
}
