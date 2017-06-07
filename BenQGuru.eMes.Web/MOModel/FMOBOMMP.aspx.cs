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

#region project
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Domain.MOModel;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FMOBOMMP 的摘要说明。
	/// </summary>
	public partial class FMOBOMMP : BasePage
	{

		private object[] moboms = null ;
		private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		private BenQGuru.eMES.MOModel.MOFacade facade ;//= new FacadeFactory(base.DataProvider).CreateMOFacade();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.InitOnPostBack();

			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.InitUI();	
				InitParameters();
				this.InitWebGrid();
			}
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

		protected void cmdView_ServerClick(object sender, System.EventArgs e)
		{
            string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page, this.DownLoadPathMO, null);

			if(fileName == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileIsEmpty");
			}
			//add by crystal chu 2005/07/15
			if(fileName.LastIndexOf(".csv") == -1)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileTypeError");
			}

			this.ViewState.Add("UploadedMOBOMFileName",fileName);
			this.RequestData();
		}


		private void InitParameters()
		{
			if(Request.Params["itemcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["itemcode"] = Request.Params["itemcode"].ToString();
			}
			if(Request.Params["mocode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["mocode"] = Request.Params["mocode"].ToString();
			}
			if(Request.Params["routecode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["routecode"] = Request.Params["routecode"].ToString();
			}
		}

		public string MOCode
		{
			get
			{
				return (string)this.ViewState["mocode"];
			}
		}

		public string ItemCode
		{
			get
			{
				return (string)this.ViewState["itemcode"];
			}
		}

		public string RouteCode
		{
			get
			{
				return (string)this.ViewState["routecode"];
			}
		}

		private void InitOnPostBack()
		{			

			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

		}


		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "MOCode", "工单",	null);
			this.gridHelper.AddColumn( "ItemCode", "料品",	null);
			this.gridHelper.AddColumn( "MOBOMItemCode", "子阶料料号",	null);
			this.gridHelper.AddColumn( "MOBOMItemName", "子阶料名称",	null);
			this.gridHelper.AddColumn( "MOBOMItemQty", "单机用量",	null);
			this.gridHelper.AddColumn( "MOBOMItemUOM", "计量单位",	null);
			this.gridHelper.AddColumn( "MOBOMException", "异常现象",	null);
			this.gridWebGrid.Columns.FromKey("MOBOMException").Width = new Unit("30%");		//modify by Simone
			this.gridHelper.AddDefaultColumn( true, false );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			ArrayList newMOBoms = new ArrayList() ;
			if(moboms == null)
			{
				this.GetAllMOBOM();
			}
			for(int i=1;i<=moboms.Length;i++)
			{
				if(i>=inclusive && i<=exclusive)
				{
					newMOBoms.Add( moboms[i-1] );
				}
			}

			return newMOBoms.ToArray() ;
		}


		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((MOBOM)obj).MOCode.ToString(),
								((MOBOM)obj).ItemCode.ToString(),
								((MOBOM)obj).MOBOMItemCode.ToString(),
								((MOBOM)obj).MOBOMItemName.ToString(),
								((MOBOM)obj).MOBOMItemQty.ToString(),
								((MOBOM)obj).MOBOMItemUOM.ToString(),
								this.languageComponent1.GetString(((MOBOM)obj).MOBOMException.ToString()),
                             ""
							});
		}
		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( 1, int.MaxValue );
		}



		private object[] GetAllMOBOM()
		{
			string fileName = string.Empty ;

	
			fileName = this.ViewState["UploadedMOBOMFileName"].ToString() ;
            
			string configFile = this.getParseConfigFileName() ;

			DataFileParser parser = new DataFileParser();
			parser.FormatName = "MOBOM" ;
			parser.ConfigFile = configFile ;
			parser.CheckValidHandle = new CheckValid( this.MOBOMDownloadCheck );
			moboms = parser.Parse(fileName) ;

		

			foreach(MOBOM mobom in moboms)
			{
				mobom.MOCode   = FormatHelper.PKCapitalFormat(mobom.MOCode);
				mobom.ItemCode   = FormatHelper.PKCapitalFormat(mobom.ItemCode);

				mobom.MaintainUser        = this.GetUserCode();
				mobom.MaintainDate        = FormatHelper.TODateInt(DateTime.Today);
				mobom.MaintainTime        = FormatHelper.TOTimeInt(DateTime.Now);

			}

			return moboms ;

		}


		private void RequestData()
		{

			if(moboms==null)
			{
				moboms = GetAllMOBOM() ;
			}


			this.gridHelper.Grid.DisplayLayout.Pager.AllowPaging = true ;
			this.gridHelper.Grid.DisplayLayout.Pager.CurrentPageIndex = 1 ;
			this.gridHelper.Grid.DisplayLayout.Pager.PageSize = int.MaxValue ;
			this.gridHelper.GridBind(1,int.MaxValue);
		}


		private string getParseConfigFileName()
		{
			string configFile = this.Server.MapPath(this.TemplateSourceDirectory )  ;
			if(configFile[ configFile.Length - 1 ] != '\\')
			{
				configFile += "\\" ;
			}
			configFile += "DataFileParser.xml" ;
			return configFile ;
		}

		/// <summary>
		/// 判断导入的MO是否有效
		/// </summary>
		/// <param name="obj">导入的MO</param>
		/// <returns>如果有效,返回true,否则,返回false;返回false时,这个MO将不会被导入</returns>
		private bool MOBOMDownloadCheck(object obj)
		{
			MOBOM mobom = (MOBOM) obj ;

			if ( mobom.MOCode.Trim().ToUpper() != MOCode )
			{
				ExceptionManager.Raise(this.GetType(),"$Error_MOCode_NotCompare",string.Format("[$MOCode='{0}']",MOCode));
			}

			if ( mobom.ItemCode.Trim().ToUpper() != ItemCode )
			{
				ExceptionManager.Raise(this.GetType(),"$Error_ItemCode_NotCompare",string.Format("[$ItemCode='{0}']",ItemCode));
			}

			return true ;
		}

		private MOBOM GetSelectMOBOM(UltraGridRow row)
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateMOFacade();}
			MOBOM mobom = this.facade.CreateNewMOBOM();


			mobom.MOCode = row.Cells[1].Text.ToString();
			mobom.ItemCode = row.Cells[2].Text.ToString();
			mobom.MOBOMItemCode = row.Cells[3].Text.ToString();
			mobom.MOBOMItemName = row.Cells[4].Text.ToString();
			mobom.MOBOMItemQty = System.Decimal.Parse( row.Cells[5].Text.ToString());
			mobom.MOBOMItemUOM = row.Cells[6].Text.ToString();
			mobom.MOBOMException = row.Cells[7].Text.ToString();

			return mobom;
		}

		//比对bom
		protected void cmdDownload_ServerClick(object sender, System.EventArgs e)
		{
			if(facade==null){facade = new FacadeFactory(base.DataProvider).CreateMOFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{
				MOBOM[] moboms1 = new MOBOM[array.Count];

				if(moboms1.Length >0)
				{
					for(int i=0;i<array.Count;i++)
					{
						moboms1[i] = GetSelectMOBOM((UltraGridRow)array[i]);
					}

					moboms = this.facade.CompareMOBOMOPBOM( moboms1,ItemCode,MOCode,RouteCode);
					this.RequestData();
				}
			}
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FMOEP.aspx",new string[] {"ACT","MOCode"} ,new string[] {"EDIT",MOCode}));
		}
	}
}
