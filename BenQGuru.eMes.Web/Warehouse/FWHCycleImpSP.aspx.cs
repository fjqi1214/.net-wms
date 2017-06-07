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

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
	/// <summary>
	/// FShiftMP 的摘要说明。
	/// </summary>
	public partial class FWHCycleImpSP : BaseMPage
	{

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
		private BenQGuru.eMES.Material.WarehouseFacade _facade;// = new BenQGuru.eMES.Material.WarehouseFacade();

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
			
		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.cmdEnter.Disabled = true;
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
			this.gridHelper.AddColumn( "FactoryCode", "工厂",	null);
			//this.gridHelper.AddColumn( "SegmentCode1", "工段",	null);
			this.gridHelper.AddColumn( "WarehouseCode", "仓库",	null);
			this.gridHelper.AddColumn( "WarehouseItemCode", "物料代码",	null);
			this.gridHelper.AddColumn( "WarehouseItemName", "物料名称",	null);
			this.gridHelper.AddColumn( "WarehouseItemQty1", "离散数量",	null);
			this.gridHelper.AddColumn( "LineItemQty", "在制品虚拆数量",	null);
			this.gridHelper.AddColumn( "WarehouseStockQty", "帐面数",	null);
			this.gridHelper.AddColumn( "ActualQty", "实盘数",	null);
			this.gridHelper.AddColumn( "IsAllowed", "合法性",	null);

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		#endregion

		#region Import
		private object[] items;
		protected void cmdView_ServerClick(object sender, System.EventArgs e)
		{
            string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page, this.fileInit, null);
			if(fileName == null)
			{
				BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileIsEmpty");
			}
			if(!fileName.ToLower().EndsWith(".csv"))
			{
				BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileTypeError");
			}

			this.ViewState.Add("UploadedFileName",fileName);
			this.cmdQuery_Click(null, null);
			if (this.gridWebGrid.Rows.Count > 0)
			{
				this.cmdEnter.Disabled = false;
			}
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			ArrayList objs = new ArrayList() ;
			if(items == null)
			{
				this.GetAllItem();
			}
			for(int i=1;i<=items.Length;i++)
			{
				if(i>=inclusive && i<=exclusive)
				{
					objs.Add( items[i-1] );
				}
			}

			return objs.ToArray() ;

		}
		protected override int GetRowCount()
		{
			if(items == null)
			{
				this.GetAllItem();
			}
			return items.Length;
		}

		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			WarehouseCycleCountDetail item = (WarehouseCycleCountDetail)obj;
//			try
//			{
//				int iCode = int.Parse(item.WarehouseCode);
//				item.WarehouseCode = new string('0', 4 - item.WarehouseCode.Length) + item.WarehouseCode;
//			}
//			catch
//			{}
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								item.FactoryCode,
								//item.SegmentCode,
								item.WarehouseCode,
								item.ItemCode,
								item.EAttribute1,
								Math.Round(Convert.ToDecimal(item.Qty), 2).ToString(),			//离散数量	
								Math.Round(Convert.ToDecimal(item.LineQty), 2).ToString(),		//在制品虚拆数量
								Math.Round(item.Warehouse2LineQty, 2),
								Math.Round(item.PhysicalQty, 2),
								IsAllowed(item)
							});
		}
		private Hashtable htItems;
		private Hashtable htWarehouse = new Hashtable();
		private string strBoolYes = "";
		private string strBoolNo = "";
		private string IsAllowed(WarehouseCycleCountDetail dtl)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			if (htItems == null)
			{
				//将所有物料资料取出
				htItems = new Hashtable();
				object[] objs = this._facade.GetAllWarehouseItem();
				if (objs != null)
				{
					for (int i = 0; i < objs.Length; i++)
					{
						WarehouseItem item = (WarehouseItem)objs[i];
						htItems.Add(item.ItemCode, item.ItemName);
						item = null;
					}
				}
				objs = null;

				//取出Boolean的描述
				strBoolYes = FormatHelper.DisplayBoolean("1", this.languageComponent1);
				strBoolNo = FormatHelper.DisplayBoolean("0", this.languageComponent1);
			}
			
//			try
//			{
//				int iCode = int.Parse(dtl.WarehouseCode);
//				dtl.WarehouseCode = new string('0', 4 - dtl.WarehouseCode.Length) + dtl.WarehouseCode;
//			}
//			catch
//			{}
			//获取仓库状态
			if (!htWarehouse.ContainsKey(string.Format("{0}-{1}", dtl.FactoryCode, /*dtl.SegmentCode,*/ dtl.WarehouseCode)))
			{
				object obj = this._facade.GetWarehouse(dtl.WarehouseCode, /*dtl.SegmentCode,*/ dtl.FactoryCode);
				if (obj != null)
				{
					Warehouse wh = (Warehouse)obj;
					htWarehouse.Add(string.Format("{0}-{1}", dtl.FactoryCode, /*dtl.SegmentCode,*/ dtl.WarehouseCode), wh.WarehouseStatus);
				}
				else
				{
					htWarehouse.Add(string.Format("{0}-{1}", dtl.FactoryCode, /*dtl.SegmentCode,*/ dtl.WarehouseCode), Warehouse.WarehouseStatus_Closed);
				}
				obj = null;
			}
			
			dtl.ItemCode = dtl.ItemCode.ToUpper();
			if (dtl.FactoryCode != string.Empty && /* dtl.SegmentCode != string.Empty && */ dtl.WarehouseCode != string.Empty &&
				dtl.ItemCode != string.Empty && dtl.PhysicalQty >= 0 && 
				htItems.ContainsKey(dtl.ItemCode) && 
				htWarehouse[string.Format("{0}-{1}", dtl.FactoryCode, /*dtl.SegmentCode,*/ dtl.WarehouseCode)].ToString() == Warehouse.WarehouseStatus_Cycle)
			{
				return strBoolYes;
			}
			else
			{
				return strBoolNo;
			}
		}
    
		private object[] GetAllItem()
		{
			try
			{
				string fileName = string.Empty ;

				fileName = this.ViewState["UploadedFileName"].ToString() ;
	            
				string configFile = this.getParseConfigFileName() ;

				BenQGuru.eMES.Web.Helper.DataFileParser parser = new BenQGuru.eMES.Web.Helper.DataFileParser();
				parser.FormatName = "WarehouseCycleCountDetail" ;
				parser.ConfigFile = configFile ;
				items = parser.Parse(fileName) ;
			}
			catch
			{}

			return items ;

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

		protected void cmdImport_ServerClick(object sender, System.EventArgs e)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			if(items==null)
			{
				items = GetAllItem() ;
				if (items == null)
					return;
			}
			int iRet = this._facade.ImportWarehouseCycleCountDetail(items, this.GetUserCode());
			string strMessage = "";
			if (iRet > 0)
			{
				strMessage = languageComponent1.GetLanguage("$CycleImport_Success").ControlText;
				this.cmdEnter.Disabled = true;
			}
			else
			{
				strMessage = languageComponent1.GetLanguage("$CycleImport_Error").ControlText;
			}
			string alertInfo = 
				string.Format("<script language=javascript>alert('{0}');</script>", strMessage);
			if( !this.IsClientScriptBlockRegistered("ImportAlert") )
			{
				this.RegisterClientScriptBlock("ImportAlert", alertInfo);	
			}
			items = null;
		}

		protected void cmdReturn_ServerClick(object sender, EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("../Warehouse/FWHCycleMP.aspx"));
		}

		#endregion
	}
}
