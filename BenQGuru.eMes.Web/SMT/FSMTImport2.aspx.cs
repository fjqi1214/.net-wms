using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Web.UI.HtmlControls;

using Infragistics.WebUI.UltraWebGrid ;
using Infragistics.WebUI.UltraWebNavigator ;
using Infragistics.WebUI.Shared ;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.BaseSetting ;
using BenQGuru.eMES.Domain.MOModel ;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.WebQuery;

namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// FSMTLoadingMPSecond 的摘要说明。
	/// </summary>
	public class FSMTImport2  : BaseMPage
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdCopy;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid gridWebGrid;
		protected System.Web.UI.WebControls.Label lblItemCodeQuery;
		protected System.Web.UI.WebControls.Label lblMOCopySourceQuery;
		protected System.Web.UI.WebControls.Label lblStationEdit;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdGridExport;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdDelete;
		protected BenQGuru.eMES.Web.Helper.PagerSizeSelector pagerSizeSelector;
		protected BenQGuru.eMES.Web.Helper.PagerToolBar pagerToolBar;
		protected System.Web.UI.WebControls.Label lblSupplierItemEdit;
		protected System.Web.UI.WebControls.Label lblLotNOEdit;
		protected System.Web.UI.WebControls.Label lblSupplyCodeEdit;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdSave;
    
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.DropDownList drpMOCopySourceQuery;
		protected System.Web.UI.WebControls.Label lblItemCodeEdit;
		protected System.Web.UI.WebControls.TextBox txtSupplierItemEdit;
		protected System.Web.UI.WebControls.TextBox txtLotNOEdit;
		protected System.Web.UI.WebControls.TextBox txtSupplyCodeEdit;
		protected System.Web.UI.WebControls.Label lblDateCodeEdit;
		protected System.Web.UI.WebControls.TextBox txtDateCodeEdit;
		protected System.Web.UI.WebControls.Label lblPCBAEdit;
		protected System.Web.UI.WebControls.TextBox txtPCBAEdit;
		protected System.Web.UI.WebControls.Label lblBIOSEdit;
		protected System.Web.UI.WebControls.TextBox txtBIOSEdit;
		protected Infragistics.WebUI.UltraWebNavigator.UltraWebTree treeWebTree;
		protected System.Web.UI.WebControls.CheckBox chbSelectAll;
		protected System.Web.UI.WebControls.CheckBox chbifImportCheck;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chbSupplierItemEdit;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chbLotNOEdit;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chbSupplyCodeEdit;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chbDateCodeEdit;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chbPCBAEdit;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chbBIOSEdit;
		protected System.Web.UI.WebControls.Label lblVersionEdit;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chbVersionEdit;
		protected System.Web.UI.WebControls.TextBox txtVersionEdit;
		protected System.Web.UI.WebControls.DropDownList drpMOCode;
		protected System.Web.UI.WebControls.Label lblFeederEdit;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chbFeederEdit;
		protected System.Web.UI.WebControls.Label lblSapOpcode;
		protected System.Web.UI.WebControls.TextBox txtSapOPCode;

		private BenQGuru.eMES.SMT.SMTFacade _facade ;//= new SMTFacadeFactory().Create();
		protected System.Web.UI.WebControls.TextBox txtOperationCode;
		protected System.Web.UI.WebControls.TextBox txtResourceCode;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdCancel;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chbExportByRes;
		protected System.Web.UI.WebControls.TextBox txtItemCodeQuery;
		protected System.Web.UI.WebControls.Label lblTitle;
		protected System.Web.UI.WebControls.TextBox Textbox3;
		protected System.Web.UI.WebControls.TextBox Textbox5;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.HtmlControls.HtmlInputFile fileExcel;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox txtItemCode;
		protected System.Web.UI.HtmlControls.HtmlInputFile FileMOItem;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox CheckboxBOM;
		protected System.Web.UI.HtmlControls.HtmlInputButton btnImport;
		protected System.Web.UI.WebControls.TextBox txtItemCodeEdit;
		protected System.Web.UI.WebControls.TextBox txtStationEdit;
		protected System.Web.UI.WebControls.TextBox txtRouteCode;
		protected System.Web.UI.WebControls.TextBox txtFeederEdit;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.DropDownList drpResource;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdMOClose;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdImport;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidifImportUse;
		protected System.Web.UI.HtmlControls.HtmlInputCheckBox chbIfContainFeeder;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdReFlesh;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdRelease;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidtxtMOCode;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdchangeMO;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdFreshTree;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdCompare;
		protected System.Web.UI.WebControls.Label lblVisibleStyle;
		protected System.Web.UI.WebControls.RadioButtonList rblMOBOMSourceSelect;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidComparesource;
		protected System.Web.UI.WebControls.Label lblMoQuery;
		protected BenQGuru.eMES.Web.SelectQuery.SelectableTextBox txtMoQuery;

		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}


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
			this.drpMOCopySourceQuery.SelectedIndexChanged += new System.EventHandler(this.drpMOCopySourceQuery_SelectedIndexChanged);
			this.rblMOBOMSourceSelect.SelectedIndexChanged += new System.EventHandler(this.rblMOBOMSourceSelect_SelectedIndexChanged);
			this.treeWebTree.Load += new System.EventHandler(this.treeWebTree_Load);
			this.treeWebTree.NodeClicked += new Infragistics.WebUI.UltraWebNavigator.NodeClickedEventHandler(this.treeWebTree_NodeClicked);
			this.cmdCopy.ServerClick += new System.EventHandler(this.cmdCopy_ServerClick);
			this.cmdImport.ServerClick += new System.EventHandler(this.cmdImport_ServerClick);
			this.cmdCompare.ServerClick += new System.EventHandler(this.cmdCompare_ServerClick);
			this.cmdMOClose.ServerClick += new System.EventHandler(this.cmdMOClose_ServerClick);
			this.cmdRelease.ServerClick += new System.EventHandler(this.cmdRelease_ServerClick);
			this.cmdchangeMO.ServerClick += new System.EventHandler(this.cmdchangeMO_ServerClick);
			this.cmdFreshTree.ServerClick += new System.EventHandler(this.cmdFreshTree_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}


		#region Init
		private void Page_Load(object sender, System.EventArgs e)
		{
			string strMessage = this.languageComponent1.GetString("$Message_SMTLoading_DataOverwritten");

			this.cmdCopy.Attributes.Add("onclick","return CheckIfCopy();" ) ;
			this.cmdImport.Attributes.Add("onclick","return CheckImport();" ) ;
			this.cmdCompare.Attributes.Add("onclick","return CheckCompare();" ) ;
			string deleteWord = languageComponent1.GetString("deleteConfirm");
			//this.cmdPending.Attributes["onclick"] = "return popSelectrPage();";
			this.cmdRelease.Attributes["onclick"] = "{ return confirm('" + deleteWord + "'); }";
			this.txtMoQuery.Tag = "false";  //工单选择状态不受限制
			InitHanders();

			RadioButtonListBuilder builder1 = new RadioButtonListBuilder(new CompareSourceType(),this.rblMOBOMSourceSelect,this.languageComponent1);
			if( !this.IsPostBack )
			{
				builder1.Build();
			}

			RadioButtonListBuilder.FormatListControlStyle( this.rblMOBOMSourceSelect,60 );
		}

		private void InitHanders()
		{
			this.excelExporter.LoadExportDataNoPageHandle = new LoadExportDataDelegateNoPage( LoadExportData ) ;
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
			this.excelExporter.CellSplit = ",";
			this.excelExporter.FileExtension = "csv";
			this.excelExporter.RowSplit = "\r\n" ;
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		//获取选择的第一张工单
		private string GetFirstMOCode()
		{
			string returnMOCode = string.Empty;
			if(this.txtMoQuery.Text != string.Empty)
			{
				returnMOCode = this.txtMoQuery.Text.Split(',')[0].ToString();
			}
			return returnMOCode;
		}


		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
			this.gridHelper.AddColumn( "ResourceCode1", "机台",	null);
			this.gridHelper.AddColumn( "StationCode", "站位",	null);
			this.gridHelper.AddColumn( "FeederCode", "料架规格代码",	null);
			this.gridHelper.AddColumn( "MaterialItemCode", "料号",	null);
			this.gridHelper.AddColumn( "Version", "物料版本",	null);
			this.gridHelper.AddColumn( "LotNO", "生产批次",	null);
			this.gridHelper.AddColumn( "DateCode", "生产日期",	null);
			this.gridHelper.AddColumn( "BIOSVersion", "BIOS版本",	null);
			this.gridHelper.AddColumn( "PCBAVersion", "PCBA版本",	null);
			this.gridHelper.AddColumn( "VendorCode1", "厂商",	null);
			this.gridHelper.AddColumn( "VenderItemCode", "厂商料号",	null);
			
			this.gridHelper.AddColumn( "ItemCode", "产品代码",	null);
			this.gridHelper.AddColumn( "MOCode", "工单代码",	null);
			this.gridHelper.AddColumn( "RouteCode", "途程代码",	null);
			this.gridHelper.AddColumn( "OPCode", "工序代码",	null);

			this.gridHelper.AddDefaultColumn( true, true );

			//this.gridWebGrid.Columns.FromKey("ResourceCode1").Hidden = true ;	
			this.gridWebGrid.Columns.FromKey("ItemCode").Hidden = true ;	
			this.gridWebGrid.Columns.FromKey("MOCode").Hidden = true ;	
			this.gridWebGrid.Columns.FromKey("RouteCode").Hidden = true ;	
			this.gridWebGrid.Columns.FromKey("OPCode").Hidden = true ;	
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{   "false",
								((SMTResourceBOM)obj).ResourceCode.ToString(),
								((SMTResourceBOM)obj).StationCode.ToString(),
								((SMTResourceBOM)obj).FeederCode.ToString(),
								((SMTResourceBOM)obj).OPBOMItemCode.ToString(),
								((SMTResourceBOM)obj).Version.ToString(),
								((SMTResourceBOM)obj).LotNO.ToString(),
								((SMTResourceBOM)obj).DateCode.ToString(),
								((SMTResourceBOM)obj).BIOS.ToString(),
								((SMTResourceBOM)obj).PCBA.ToString(),
								((SMTResourceBOM)obj).VendorCode.ToString(),
								((SMTResourceBOM)obj).VenderItemCode.ToString(),
								((SMTResourceBOM)obj).ItemCode.ToString(),
								((SMTResourceBOM)obj).MOCode.ToString(),
								((SMTResourceBOM)obj).RouteCode.ToString(),
								((SMTResourceBOM)obj).OPCode.ToString(),
								""});
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			if(this.ViewState["Export"]!=null && this.ViewState["Export"].ToString()=="True")
			{
				this.ViewState["Export"] = "False";
				return this.LoadExportData();
			}
			if(this.treeWebTree.SelectedNode!=null && this.treeWebTree.SelectedNode.Text != string.Empty )
			{

				//通过当前维护工单和机台查询SMTResourceBOM (当前维护的工单为选择的第一张工单)
				return this._facade.QuerySMTResourceBOM(string.Empty , this.GetFirstMOCode() , string.Empty, string.Empty, this.treeWebTree.SelectedNode.Text , string.Empty,inclusive,exclusive) ;
			}
			else
			{
				return null ;
			}
		}

		private void cmdRelease_ServerClick(object sender, System.EventArgs e)
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
				this.cmdFreshTree_ServerClick(null,null);
			}
		}

		#region 选择机台导出

		//导出检查
		private void ExportCheck()
		{
			//指定机台导出，判断选择的TreeNode
			if(this.treeWebTree.CheckedNodes.Count == 0)
			{
				ExceptionManager.Raise( this.GetType() , "$Error_Resource_SelectedNULL") ;// "没有选择机台，请选择机台"
			}
			#region 注销的实现
//			if(this.chbExportByRes.Checked)
//			{
//				//指定机台导出，判断选择的TreeNode
//				if(this.treeWebTree.CheckedNodes.Count == 0)
//				{
//					ExceptionManager.Raise( this.GetType() , "$Error_Resource_SelectedNULL") ;// "没有选择机台，请选择机台"
//				}
//
//			}
//			else
//			{
//				//导出所有机台的ResourceBOM，判断是否有机台
//				if(!(this.treeWebTree.Nodes.Count>0))
//				{
//					ExceptionManager.Raise( this.GetType() , "$Error_Resource_NULL") ;	// "没有可以导出的机台"
//				}
//			}
			#endregion
		}

		//获取导出的机台集合
		private ArrayList GetExportResource()
		{
			ArrayList ResourceCode = new ArrayList();
			ArrayList selectNodes = new ArrayList(this.treeWebTree.Nodes.Count);
			//指定机台导出，判断选择的TreeNode
			selectNodes = this.treeWebTree.CheckedNodes;
			#region 注销的逻辑 
//			if(this.chbExportByRes.Checked)
//			{
//				//指定机台导出，判断选择的TreeNode
//				selectNodes = this.treeWebTree.CheckedNodes;
//			}
//			else
//			{
//				
//				selectNodes.AddRange(this.treeWebTree.Nodes);
//			}
			#endregion
			foreach(Node resourceNode in selectNodes)
			{
				ResourceCode.Add(resourceNode.Text);
			}
			return ResourceCode;
		}

		#endregion

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			if( this.GetFirstMOCode() != string.Empty )
			{
				return this._facade.QuerySMTResourceBOMCount(string.Empty , this.GetFirstMOCode() , string.Empty, string.Empty, this.treeWebTree.SelectedNode.Text , string.Empty) ;
			}
			else
			{
				return 0 ;
			}
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.AddSMTResourceBOM( (SMTResourceBOM)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.DeleteSMTResourceBOM( (SMTResourceBOM[])domainObjects.ToArray( typeof(SMTResourceBOM) ) );

			this.gridHelper.RequestData();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
			this.cmdFreshTree_ServerClick(null,null);
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.UpdateSMTResourceBOM( (SMTResourceBOM)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				//this.drpStationEdit.Enabled = true ;
				this.clearEditArea() ;

			}

			if ( pageAction == PageActionType.Update )
			{
				//this.drpStationEdit.Enabled = false ;
			}

			if( pageAction == PageActionType.Save )
			{
				this.clearEditArea() ;
			}
		}
		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			if( !this.ValidateInput() )
			{
				return null ;
			}

			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			SMTResourceBOM bom = this._facade.CreateNewSMTResourceBOM() ;
			if(this.chbBIOSEdit.Checked)
			{
				bom.BIOS = FormatHelper.CleanString(this.txtBIOSEdit.Text) ;
			}
			else
			{
				bom.BIOS = string.Empty ;
			}

			if(this.chbDateCodeEdit.Checked)
			{
				bom.DateCode = FormatHelper.CleanString(this.txtDateCodeEdit.Text) ;
			}

			bom.FeederCode = FormatHelper.CleanString(this.txtFeederEdit.Text) ;

			bom.ItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)) ;
            
			bom.OPBOMItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeEdit.Text)) ;

			if(this.chbLotNOEdit.Checked)
			{
				bom.LotNO = FormatHelper.CleanString(this.txtLotNOEdit.Text) ;
			}

			bom.MOCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.GetFirstMOCode())) ;
			//bom.MOCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpMOCode.SelectedValue)) ;

			bom.OPCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOperationCode.Text)) ;

			if(this.chbPCBAEdit.Checked)
			{
				bom.PCBA = FormatHelper.CleanString(this.txtPCBAEdit.Text) ;
			}

            
			bom.ResourceCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCode.Text)) ;
			bom.RouteCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRouteCode.Text)) ;
			bom.StationCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStationEdit.Text)) ;
            
			if(this.chbSupplierItemEdit.Checked)
			{
				bom.VenderItemCode  = FormatHelper.CleanString(this.txtSupplierItemEdit.Text) ; 
			}

			if(this.chbSupplyCodeEdit.Checked)
			{
				bom.VendorCode = FormatHelper.CleanString(this.txtSupplyCodeEdit.Text) ;
			}

			if(this.chbVersionEdit.Checked)
			{
				bom.Version = FormatHelper.CleanString(this.txtVersionEdit.Text) ;
			}

			BenQGuru.eMES.MOModel.MOFacade moFacade =  new SMTFacadeFactory(base.DataProvider).CreateMOFacade() ;
			MO2Route mo2route = (MO2Route) moFacade.GetMONormalRouteByMOCode(this.drpMOCopySourceQuery.SelectedValue) ;
			if(mo2route != null)
			{
				bom.OPBOMCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(mo2route.OPBOMCode)) ;
				bom.OPBOMVersion = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(mo2route.OPBOMVersion)) ;
			}
            
			bom.MaintainUser = this.GetUserCode() ;

			return bom;
		}


		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			#region  以前的方法
//			object obj = _facade.GetSMTResourceBOM(
//				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("ItemCode").Text)),
//				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("MOCode").Text)) ,
//				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("RouteCode").Text)),
//				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("OPCode").Text)),
//				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("ResourceCode1").Text)) ,
//				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("StationCode").Text)),
//				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("MaterialItemCode").Text)));
			#endregion
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			object obj = _facade.GetSMTResourceBOM(
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("ItemCode").Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("MOCode").Text)) ,
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("ResourceCode1").Text)) ,
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("StationCode").Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(row.Cells.FromKey("MaterialItemCode").Text)));

			if (obj != null)
			{
				return (SMTResourceBOM)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if(obj != null)
			{

				this.txtBIOSEdit.Text = ((SMTResourceBOM)obj).BIOS ;
				this.txtDateCodeEdit.Text = ((SMTResourceBOM)obj).DateCode ;
				this.txtLotNOEdit.Text = ((SMTResourceBOM)obj).LotNO ;
				this.txtPCBAEdit.Text = ((SMTResourceBOM)obj).PCBA ;
				this.txtSupplierItemEdit.Text = ((SMTResourceBOM)obj).VenderItemCode ;
				this.txtSupplyCodeEdit.Text = ((SMTResourceBOM)obj).VendorCode ;
				this.txtVersionEdit.Text = ((SMTResourceBOM)obj).Version ;

				this.txtStationEdit.Text = ((SMTResourceBOM)obj).StationCode ;
				this.txtItemCodeEdit.Text = ((SMTResourceBOM)obj).OPBOMItemCode ;
				this.txtFeederEdit.Text = ((SMTResourceBOM)obj).FeederCode ;
				
				this.chbBIOSEdit.Checked = !(this.txtBIOSEdit.Text == "" ) ;                
				this.chbDateCodeEdit.Checked = !(this.txtDateCodeEdit.Text == "" ) ;
				this.chbLotNOEdit.Checked = !(this.txtLotNOEdit.Text == "" ) ;
				this.chbPCBAEdit.Checked = !(this.txtPCBAEdit.Text == "") ;
				this.chbSupplierItemEdit.Checked = !(this.txtSupplierItemEdit.Text == "") ;
				this.chbSupplyCodeEdit.Checked = !(this.txtSupplyCodeEdit.Text == "") ;
				this.chbVersionEdit.Checked = !(this.txtVersionEdit.Text == "") ;
				this.chbFeederEdit.Checked = !(this.txtFeederEdit.Text == "") ;

				this.txtOperationCode.Text = ((SMTResourceBOM)obj).OPCode ;
				this.txtResourceCode.Text = ((SMTResourceBOM)obj).ResourceCode ;
				this.txtItemCodeQuery.Text = ((SMTResourceBOM)obj).ItemCode ;
				this.txtRouteCode.Text = ((SMTResourceBOM)obj).RouteCode ;

			}
		}

		
		protected override bool ValidateInput()
		{

			PageCheckManager manager = new PageCheckManager();


			if(this.chbBIOSEdit.Checked)
			{
				manager.Add( new LengthCheck(lblBIOSEdit, txtBIOSEdit, 40, true) );
			}

			if(this.chbDateCodeEdit.Checked)
			{
				manager.Add( new LengthCheck(lblDateCodeEdit, txtDateCodeEdit, 40, true) );
			}

			if(this.chbFeederEdit.Checked)
			{

			}
            
			if(this.chbLotNOEdit.Checked)
			{
				manager.Add( new LengthCheck(lblLotNOEdit, txtLotNOEdit, 40, true) );
			}

			if(this.chbPCBAEdit.Checked)
			{
				manager.Add( new LengthCheck(lblPCBAEdit, txtPCBAEdit, 40, true) );
			}

			if(this.chbSupplierItemEdit.Checked)
			{
				manager.Add( new LengthCheck(lblSupplierItemEdit, txtSupplierItemEdit, 40, true) );
			}

			if(this.chbSupplyCodeEdit.Checked)
			{
				manager.Add( new LengthCheck(lblSupplyCodeEdit, txtSupplyCodeEdit, 40, true) );
			}

			if(this.chbVersionEdit.Checked)
			{
				manager.Add( new LengthCheck(lblVersionEdit, txtVersionEdit, 40, true) );
			}

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
				return false;
			}

			return true ;


		}

		#endregion

		#region Export

		protected override string[] FormatExportRecord( object obj )
		{
			if(this.chbIfContainFeeder.Checked)
			{
				//this.drpMOCode.SelectedValue,
				//导出数据 有Feeder栏位
				return new string[]{  
									   this.GetFirstMOCode(),
									   ((SMTResourceBOM)obj).ResourceCode.ToString(),
									   ((SMTResourceBOM)obj).StationCode.ToString(),
									   ((SMTResourceBOM)obj).FeederCode.ToString(),
									   ((SMTResourceBOM)obj).OPBOMItemCode.ToString()
								   };
			}
			else
			{
				//this.drpMOCode.SelectedValue,
				//导出数据 没有Feeder栏位
				return new string[]{   this.GetFirstMOCode(),
									   ((SMTResourceBOM)obj).ResourceCode.ToString(),
									   ((SMTResourceBOM)obj).StationCode.ToString(),
									   ((SMTResourceBOM)obj).OPBOMItemCode.ToString()
								   };
			}
		}


		protected override string[] GetColumnHeaderText()
		{
			if(this.chbIfContainFeeder.Checked)
			{
				//导出数据 有Feeder栏位
				return new string[] {   "MOCode",
										"ResourceCode1",
										"StationCode",
										"FeederCode",
										"MaterialItemCode"
									};
			}
			else
			{
				//导出数据 没有Feeder栏位
				return new string[] {   "MOCode",
										"ResourceCode1",
										"StationCode",
										"MaterialItemCode"
									};
			}
		}


		#endregion

		#region 控件初始化事件

		private void drpMOCode_Load(object sender, System.EventArgs e)
		{
			if(! this.IsPostBack)
			{
				DropDownListBuilder builder = new DropDownListBuilder(this.drpMOCode);
				builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate( this.GetMOByStatus );

				builder.Build("MOCode", "MOCode");

				this.drpMOCode.Items.Insert(0, "" );
			}
		}


		//来源工单加载（注意： 不应该包涵当前的工单）
		private void drpMOCopySourceQuery_Load(object sender, System.EventArgs e)
		{
			if(! this.IsPostBack) //此处在当前工单改变的时候重新加载，否则不加载，此加载方法应该独立出来供调用
			{
				this.P_drpMOCopySourceQuery_Load();
			}
		}

		//加载来源工单（不包含当前待维护工单,和来源工单对应的产品料号相同）
		private void P_drpMOCopySourceQuery_Load()
		{
			this.drpMOCopySourceQuery.Items.Clear();		//加载前清除
			DropDownListBuilder builder = new DropDownListBuilder(this.drpMOCopySourceQuery);
			builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate( this.GetSourceItemCode);

			builder.Build("MOCode", "MOCode");

			this.drpMOCopySourceQuery.Items.Insert(0, "" );
//			if(this.drpMOCode.SelectedValue != "")
//			{
//				this.drpMOCopySourceQuery.Items.Remove(this.drpMOCode.SelectedValue);	//移除当前的工单(参数为当前的工单Code)
//			}
			if(this.GetFirstMOCode() != "")
			{
				this.drpMOCopySourceQuery.Items.Remove(this.GetFirstMOCode());	//移除当前的工单(参数为当前的工单Code)
			}
			this.F_drpResourceQuery_Load();
		}


		//机台树形结构加载
		public void treeWebTree_Load(object sender, System.EventArgs e)
		{
			//根据待维护工单加载机台（资源）
			//if(this.drpMOCode.SelectedValue != string.Empty)
			if(this.GetFirstMOCode() != string.Empty)
			{
				this.treeWebTree.CheckBoxes = true;
			}
		}

			
		private void drpResourceQuery_Load(object sender, System.EventArgs e)
		{
			if( ! this.IsPostBack )		
			{
				this.F_drpResourceQuery_Load();
			}
		}


		private void F_drpResourceQuery_Load()
		{
			//机台应该根据来源工单加载，并添加所有机台的选项
			this.drpResource.Items.Clear() ;
			if(this.drpMOCopySourceQuery.SelectedValue == string.Empty)return;
			object[]  MOResources  = new SMTFacadeFactory(base.DataProvider).CreateBaseModelFacadeFacade().GetSMTResourceByMoCode(this.drpMOCopySourceQuery.SelectedValue);
			if( MOResources != null )
			{
				foreach( Resource jitai in MOResources)
				{
					this.drpResource.Items.Add( jitai.ResourceCode ) ;
				}

				new DropDownListBuilder( this.drpResource ).AddAllItem( this.languageComponent1 ) ;
			}
		}


		#region	私有方法
		private object[] GetSourceItemCode()
		{
			return new SMTFacadeFactory(base.DataProvider).CreateMOFacade().GetMoByItemCode(this.txtItemCode.Text,new string[]{ MOManufactureStatus.MOSTATUS_RELEASE, MOManufactureStatus.MOSTATUS_OPEN ,MOManufactureStatus.MOSTATUS_CLOSE});
		}
		
		private object[] GetMOByStatus()
		{
			return new SMTFacadeFactory(base.DataProvider).CreateMOFacade().GetMOByStatus(this.GetMOStatusList());
		}

		//获取工单的筛选条件，release，open
		private string[] GetMOStatusList()
		{
			return new string[]{ MOManufactureStatus.MOSTATUS_RELEASE, MOManufactureStatus.MOSTATUS_OPEN };
			
		}

		#endregion

		#endregion

		#region 控件改变事件,控件点击事件

		private void cmdReFlesh_ServerClick(object sender, System.EventArgs e)
		{
			//刷新页面
			Session.Remove("HT");
			//重新加载机台树形控件
			this.BuildRouteTree() ;
			this.Alert("复制完成");
		}

		private void treeWebTree_NodeClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
		{
			//根据机台加载Grid明细
			DoTreeNodeClick(true) ;
		}


		private void DoTreeNodeClick(bool isRealClick)
		{
			Node node = this.treeWebTree.SelectedNode ;
			if( node != null )
			{
				// 列出当前mo的BOM信息
				this.gridHelper.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Query );

				this.clearEditArea() ;
			}
		}


		//待维护工单选择改变
		private void drpMOCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			//设置产品显示
			MO currentMO = (new SMTFacadeFactory(base.DataProvider).CreateMOFacade().GetMO(this.GetFirstMOCode()) as MO);
			if(currentMO!=null)
			{
				this.hidtxtMOCode.Value = currentMO.MOCode; //做同步
				this.txtItemCode.Text = currentMO.ItemCode;
				
				//重新加载来源工单
				P_drpMOCopySourceQuery_Load();

				//重新加载机台树形控件
				this.BuildRouteTree() ;	
			}
			else
			{
				this.ExecuteClientFunction("popSelectrPage","");
			}
		}


		//来源单据改变事件
		private void drpMOCopySourceQuery_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//根据来源工单重新加载对应机台
			this.F_drpResourceQuery_Load();
		}


		private void BuildRouteTree()
		{
			this.gridHelper.Grid.Rows.Clear() ;
			string moCode = this.GetFirstMOCode() ;

			if(moCode != string.Empty)
			{
				object[]  MOResources  = new SMTFacadeFactory(base.DataProvider).CreateBaseModelFacadeFacade().GetSMTResourceByMoCode(moCode);
				this.treeWebTree.Nodes.Clear() ;
				if(MOResources!=null)
				{
					Node node=null;
					foreach(object jitai in MOResources)
					{
						node = new Node();
						node.Text = (jitai as Resource).ResourceCode;
						node.Tag = (jitai as Resource).ResourceCode;
						this.treeWebTree.Nodes.Add( node );
					}
				}

				this.treeWebTree.ExpandAll() ;
				foreach(Node node in treeWebTree.Nodes)
				{
					node.Checked = true;
				}
			}
			else
			{
				this.treeWebTree.Nodes.Clear() ;
				this.txtItemCode.Text = string.Empty ;
				this.txtResourceCode.Text = string.Empty ;
				this.txtItemCodeQuery.Text = string.Empty ;
			}

			this.clearEditArea() ;
			this.ClickDefaultNode();
		}


		private void ClickDefaultNode()
		{
			//获取机台树第一个结点，执行nodeClick事件
			if(this.treeWebTree.Nodes.Count >0 )
			{
				this.treeWebTree.SelectedNode  = this.treeWebTree.Nodes[0];
				this.treeWebTree_NodeClicked(null,null);
			}
		}


		private void clearEditArea()
		{
			this.txtBIOSEdit.Text = "" ;
			this.txtDateCodeEdit.Text = "" ;
			this.txtLotNOEdit.Text = "" ;
			this.txtPCBAEdit.Text = "" ;
			this.txtSupplierItemEdit.Text = "" ;
			this.txtSupplyCodeEdit.Text = "" ;
			this.txtVersionEdit.Text = "" ;

			this.txtStationEdit.Text = "" ;
			this.txtItemCodeEdit.Text = "" ;
			this.txtFeederEdit.Text = "";

			this.chbBIOSEdit.Checked = false ;
			this.chbDateCodeEdit.Checked = false ;
			this.chbLotNOEdit.Checked = false ;
			this.chbPCBAEdit.Checked = false ;
			this.chbSupplierItemEdit.Checked = false ;
			this.chbSupplyCodeEdit.Checked = false ;
			this.chbVersionEdit.Checked = false ;
			this.chbFeederEdit.Checked = false ;
		}


		//复制操作
		//需要根据选择的机台进行复制,注意：机台可以是全部（即来源工单的所有机台的防呆资料）
		private void cmdCopy_ServerClick(object sender, System.EventArgs e)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			if(this.GetFirstMOCode()== string.Empty)
			{
				this.Alert(this.languageComponent1.GetString("$Error_TARGET_MOCODE_EMPTY"));
				return;
			}

			if(this.drpMOCopySourceQuery.SelectedValue == string.Empty)
			{
				//ExceptionManager.Raise(this.GetType(), "$Error_SOURCE_MOCODE_EMPTY");
				this.Alert(this.languageComponent1.GetString("$Error_SOURCE_MOCODE_EMPTY"));
				return;
			}
			//对来源机台的检查
			if(this.drpResource.Items.Count == 0 || (this.drpResource.Items.Count== 1 && this.drpResource.Items[0].Value =="全部"))
			{
				//ExceptionManager.Raise(this.GetType(), "$Error_SOURCE_RESOURCE_EMPTY");
				this.Alert(this.languageComponent1.GetString("$Error_SOURCE_RESOURCE_EMPTY"));
				return;
			}

			//获取用户选择的机台
			ArrayList selectResCodeList = this.GetFromResource();

			//获取相同的机台
			//Hashtable returnResCodeHT = _facade.CheckSameResource(this.drpMOCode.SelectedValue,selectResCodeList);
			Hashtable returnResCodeHT = _facade.CheckSameResource(this.GetFirstMOCode(),selectResCodeList);

			ArrayList sameResCodeList = (ArrayList)returnResCodeHT["SameResCodes"] ;	//相同的机台,需要用户选择是否覆盖的机台
			ArrayList diffResCodeList = (ArrayList)returnResCodeHT["DifferentResCodes"] ;	//只在来源工单中有的机台
			if(sameResCodeList.Count >0 )
			{
				Hashtable sessionHT = new Hashtable();
				sessionHT["SameResourceCode"] =  sameResCodeList;
				sessionHT["DiffResourceCode"] =  diffResCodeList;
				//sessionHT["ToMOCode"] = this.drpMOCode.SelectedValue;
				sessionHT["ToMOCode"] = this.GetFirstMOCode();
				sessionHT["FromMOCode"] = this.drpMOCopySourceQuery.SelectedValue;
				Session["HT"] = sessionHT;
				//如果有相同的机台,弹出相同机台的选择框,选择可以覆盖的机台后,执行复制程序,提示复制成功后关闭弹出窗口
				this.ExecuteClientFunction("popCopyPage","");
			}
			else
			{
				//如果没有相同的机台,直接执行复制程序,最后提示复制成功
				if(diffResCodeList.Count!=0)
				{
					//_facade.CopySMTResourceBOM(this.drpMOCopySourceQuery.SelectedValue,this.drpMOCode.SelectedValue,diffResCodeList);
					_facade.CopySMTResourceBOM(this.drpMOCopySourceQuery.SelectedValue,this.GetFirstMOCode(),diffResCodeList);
					BuildRouteTree();
					this.Alert("复制完成");
				}
			}

		}

		#region

		//获取当前选择的机台
		private ArrayList GetFromResource()
		{
			ArrayList returnList = new ArrayList();
			if(this.drpResource.SelectedValue == string.Empty)
			{
				foreach(ListItem itemRecCode in this.drpResource.Items)
				{
					if(itemRecCode.Value !=string.Empty)
						returnList.Add(itemRecCode.Text);
				}
			}
			else
			{
				returnList.Add(this.drpResource.SelectedValue);
			}
			return returnList;
		}


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

		#endregion

		private void cmdImport_ServerClick(object sender, System.EventArgs e)
		{
			#region
			//站表导入
			//逐行取得文件中每一条资料，分别判断该条资料的机台、站位、Feeder资料是否与基础设置中相符
			//取得相符的资料进行导入判断

			//不符合的资料显示给客户

			#endregion

			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			if(this.GetFirstMOCode() == string.Empty)
			{
				ExceptionManager.Raise(this.GetType(), "$Error_TARGET_MOCODE_EMPTY");
			}

			//上传文件到服务器
			string fileName = FileLoadProcess.UplodFile2ServerUploadFolder(this.Page,this.fileExcel,null);
			if(fileName == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileIsEmpty");
			}
			this.ViewState.Add("UploadedFileName",fileName);
			//取得导入数据
			object[] smtboms = this.GetImportStationItemBOM();

			//批量检查,显示所有错误信息
			//任何错误都不会执行导入
			object[] errorMessages = _facade.ValudateImportSMTBOM( smtboms);
			if(errorMessages!=null && errorMessages.Length>0)
			{
				Session["ErrorSMTBOM"] = errorMessages;
				this.ExecuteClientFunction("popErrorPage","");
			}
			else
			{
				//导入数据
				foreach(SMTResourceBOM smtbom in smtboms)
				{
					_facade.DeleteSMTResourceBOM(smtbom);
					_facade.AddSMTResourceBOM(smtbom);
					continue ;
				}

				//重新加载机台树形控件
				this.BuildRouteTree() ;
			}
		}


		private void cmdCompare_ServerClick(object sender, System.EventArgs e)
		{
			#region
			//比对BOM逻辑
			//比对,显示异常

			Session.Remove("HT");
			Session.Remove("SessionCompareHT");
			#endregion

			object[] moboms = new object[]{};
			if(this.rblMOBOMSourceSelect.SelectedValue == CompareSourceType.DB)
			{
				#region	获取工单MOBOM, 可以根据OPCode查询
				MOFacade mofacade = new MOFacade(base.DataProvider);

				//TODO ForSimone 工单需要改为多选，去多选后的工单代码
				object[] dbmoboms = mofacade.GetMOBOM(this.txtMoQuery.Text,this.txtSapOPCode.Text); //根据工单和OPCode获取MOBOM

				//比对MOBOMITEM
				moboms = this.MapperMOBOMITEM( dbmoboms );

				#endregion
			}
			else if(this.rblMOBOMSourceSelect.SelectedValue == CompareSourceType.Excel)
			{
				#region 获取MOBom导入实体 从Excel
				//			//上传文件到服务器
				string fileName2 = FileLoadProcess.UplodFile2ServerUploadFolder(this.Page,this.FileMOItem,null);
				if(fileName2 == null)
				{
					ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileIsEmpty");
				}
				this.ViewState.Add("UploadedFileName2",fileName2);
				//取得数据(工单物料清单)
				moboms = this.GetUploadMOBOM();

				if(!this.CheckMO(moboms))
				{
					this.Alert("物料清单并不属于当前工单,请重新选择工单物料清单文件!");return;
				}
				#endregion
			}

			#region 获取站表导入实体
			string fileName = FileLoadProcess.UplodFile2ServerUploadFolder(this.Page,this.fileExcel,null);
			if(fileName == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileIsEmpty");
			}
			this.ViewState.Add("UploadedFileName",fileName);
			//取得导入数据
			object[] stboms = this.GetUploadStationItemBOM();

			#endregion

			
			//比对操作,用Session传递比对结果
			Hashtable SessionCompareHT = this.GetCompareResult(ClearBlankStationRow(stboms),ClearBlankMOItemBOMRow(moboms));
			Session["SessionCompareHT"] = SessionCompareHT;
			this.ExecuteClientFunction("popDifferentBomPage","");
		}

		private object[] MapperMOBOMITEM(object[] dbmoboms )
		{
			ArrayList returnMOBOMITEMList = new ArrayList();
			if(dbmoboms!=null)
			{
				foreach(MOBOM _mobom in dbmoboms)
				{
					MOItemBOM _moItemBom = new MOItemBOM();
					_moItemBom.MOCode = _mobom.MOCode;
					_moItemBom.ItemCode = _mobom.ItemCode;
					_moItemBom.OBItemCode = _mobom.MOBOMItemCode;
					_moItemBom.OBItemName = _mobom.MOBOMItemName;
					_moItemBom.OBItemQTY = _mobom.MOBOMItemQty.ToString();
					_moItemBom.OBItemUnit = _mobom.MOBOMItemUOM;

					returnMOBOMITEMList.Add(_moItemBom);
				}
			}
			return (object[])returnMOBOMITEMList.ToArray(typeof(MOItemBOM));;
		}

		private bool CheckMO(object[] moboms)
		{
			MOItemBOM moitem = (MOItemBOM)moboms[0];
			if(moitem.MOCode == this.hidtxtMOCode.Value)
			{
				return true;
			}
			return false;
		}

		
		//去除空行
		private object[] ClearBlankStationRow(object[] stationboms)
		{
			if(stationboms ==null || stationboms.Length==0)return stationboms;
			ArrayList returnList = new ArrayList();
			foreach(StationBOM stbom in stationboms)
			{
				if(stbom.OBItemCode!=string.Empty)
				{
					returnList.Add(stbom);
				}
			}
			return returnList.ToArray();
		}

		//去除空行
		private object[] ClearBlankMOItemBOMRow(object[] moboms)
		{
			if(moboms ==null || moboms.Length==0)return moboms;
			ArrayList returnList = new ArrayList();
			foreach(MOItemBOM item in moboms)
			{
				if(item.OBItemCode!=string.Empty)
				{
					returnList.Add(item);
				}
			}
			return returnList.ToArray();
		}

		private void cmdchangeMO_ServerClick(object sender, System.EventArgs e)
		{
			//当前待维护工单改变
			this.drpMOCode_SelectedIndexChanged(null,null);
		}

		private void cmdFreshTree_ServerClick(object sender, System.EventArgs e)
		{
			this.BuildRouteTree(); //刷新树结构
		}

		private void cmdMOClose_ServerClick(object sender, System.EventArgs e)
		{
			this.ExportCheck();
			this.ViewState["Export"] = "True";
			this.excelExporter.RowSplit = "\r\n" ;
			this.excelExporter.Export();
		}

		#region 导入私有方法

		private object[] GetImportStationItemBOM()
		{
			string fileName = string.Empty ;

			fileName = this.ViewState["UploadedFileName"].ToString() ;
            
			string configFile = this.getParseConfigFileName() ;

			DataFileParser parser = new DataFileParser();
			parser.FormatName = "SMTResourceBOM" ;
			parser.ConfigFile = configFile ;
			//parser.CheckValidHandle = new CheckValid( this.SMTResourceBOMDownloadCheck );			//检查逻辑
			object[] smtboms = parser.Parse(fileName) ;

			object[] filterboms = _facade.FilterImportData(this.GetFirstMOCode(),smtboms);	//比对检查,返回可以导入的数据

			foreach(SMTResourceBOM SMTrBOM in filterboms)
			{
				//映射数据以及数据格式
				//SMTrBOM.MOCode = FormatHelper.PKCapitalFormat(this.drpMOCode.SelectedValue);
				SMTrBOM.MOCode = FormatHelper.PKCapitalFormat(this.GetFirstMOCode());
				SMTrBOM.ItemCode = FormatHelper.PKCapitalFormat(this.txtItemCode.Text);
				SMTrBOM.ResourceCode = FormatHelper.PKCapitalFormat(SMTrBOM.ResourceCode);
				SMTrBOM.StationCode = FormatHelper.PKCapitalFormat(SMTrBOM.StationCode);
				SMTrBOM.FeederCode = FormatHelper.PKCapitalFormat(SMTrBOM.FeederCode);
				SMTrBOM.OPBOMItemCode = FormatHelper.PKCapitalFormat(SMTrBOM.OPBOMItemCode);

				SMTrBOM.MaintainUser        = this.GetUserCode();
				SMTrBOM.MaintainDate        = FormatHelper.TODateInt(DateTime.Today);
				SMTrBOM.MaintainTime        = FormatHelper.TOTimeInt(DateTime.Now);
			}

			return filterboms ;

		}
		//去除空行
		private object[] ClearBlankSMTResourceBOMRow(object[] smtboms)
		{
			if(smtboms ==null || smtboms.Length==0)return smtboms;
			ArrayList returnList = new ArrayList();
			foreach(SMTResourceBOM SMTrBOM in smtboms)
			{
				if(SMTrBOM.OPBOMItemCode!=string.Empty)
				{
					returnList.Add(SMTrBOM);
				}
			}
			return returnList.ToArray();
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

		//导入数据检查
		private bool SMTResourceBOMDownloadCheck(object obj)
		{
			#region 导入逻辑检查逻辑

			//逐行取得文件中每一条资料，分别判断该条资料的机台、站位、Feeder资料是否与基础设置中相符
			//应该检查所有SMTResourceBOM的主键

			#endregion

			if(!this.chbifImportCheck.Checked){return true;}		//用控件标识是否执行检查逻辑,默认是检查
			SMTResourceBOM sbom = obj as SMTResourceBOM;

			// 检查机台 ResourceCode
			BenQGuru.eMES.BaseSetting.BaseModelFacade baseModelFacade =new SMTFacadeFactory(base.DataProvider).CreateBaseModelFacadeFacade() ;
			object resource = baseModelFacade.GetResource(FormatHelper.PKCapitalFormat(FormatHelper.CleanString( sbom.ResourceCode ))) ;
			if( resource == null )
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_ResourceCode_NotExist");
			}

			// 检查站位 StationCode
			object station = _facade.GetStation(FormatHelper.PKCapitalFormat(FormatHelper.CleanString( sbom.ResourceCode )),FormatHelper.PKCapitalFormat(FormatHelper.CleanString( sbom.StationCode ))) ;
			if( station == null )
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_StationCode_NotExist");
			}

			// 检查 FeederCode
			if(sbom.FeederCode != string.Empty)
			{
				object Feeder = _facade.GetFeeder(FormatHelper.PKCapitalFormat(FormatHelper.CleanString( sbom.FeederCode ))) ;
				if( Feeder == null )
				{
					ExceptionManager.Raise(this.GetType().BaseType,"$Error_FeederCode_NotExist");
				}
			}

			return true;
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
        
		protected object[] LoadExportData()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			ArrayList ResourceCodeList = this.GetExportResource();
			//查询条件，工单，机台下对应的所有SMTResourceBOM
			object[] boms = this._facade.QuerySMTResourceBOMExport(this.GetFirstMOCode(), ResourceCodeList) ;
			//object[] boms = this._facade.QuerySMTResourceBOMExport(this.drpMOCode.SelectedValue, ResourceCodeList) ;
			return boms ;
		}


		#endregion

		#region 站表比对BOM

		#region 比对BOM私有方法

		//获取比对结果
		private Hashtable GetCompareResult(object[] StationObjs,object[] MoBomObjs)
		{
			Hashtable returnHT = new Hashtable();
			ArrayList SucessResult = new ArrayList();			//比对成功
			ArrayList InStationResult = new ArrayList();		//只在站表中
			ArrayList InMoBOMResult = new ArrayList();			//只在物料清单

			//只比对料号是否相同
			Hashtable stationHT = new Hashtable();		//station料号集合
			Hashtable moBomHT = new Hashtable();		//工单料号集合

			string strMOCode = this.hidtxtMOCode.Value;//((MOItemBOM)MoBomObjs[0]).MOCode;	//工单单号

			#region 初始化比对Hashtable
			foreach(object stObj in StationObjs)
			{
				StationBOM stbom = (stObj as StationBOM);
				if(stbom!=null)
				{
					if(!stationHT.Contains(stbom.OBItemCode)){stationHT.Add(stbom.OBItemCode,stbom.OBItemCode);}
				}
			}

			foreach(object moitemObj in MoBomObjs)
			{
				MOItemBOM moitem = (moitemObj as MOItemBOM);
				if(moitem!=null)
				{
					if(!moBomHT.Contains(moitem.OBItemCode)){moBomHT.Add(moitem.OBItemCode,moitem.OBItemCode);}
				}
			}

			#endregion
			//以站表为基础比对
			foreach(object stObj in StationObjs)
			{
				StationBOM stbom = (stObj as StationBOM);
				if(moBomHT.Contains(stbom.OBItemCode))
				{
					stbom.MOCode = strMOCode;
					stbom.CompareResult = "一致";
					SucessResult.Add(stbom);		//如果在moitembom有,比对成功
				}
				else
				{
					stbom.MOCode = strMOCode;
					stbom.CompareResult = "只存在于站表中";
					InStationResult.Add(stbom);		//否则表示只在站表中有
				}
			}

			foreach(object moitemObj in MoBomObjs)
			{
				MOItemBOM moitem = (moitemObj as MOItemBOM);
				if(!stationHT.Contains(moitem.OBItemCode))
				{
					moitem.CompareResult = "只存在于物料清单";
					InMoBOMResult.Add(moitem);		//如果站表中没有,表示只在moitembom中有
				}
			}

			//判断替代料
			#region 判断替代料

			Hashtable sucessResStation = new Hashtable();
			foreach(StationBOM sbom in SucessResult)
			{
				string strResStation = sbom.ResourceCode.Trim() + sbom.StationCode.Trim();
				if(!sucessResStation.Contains(strResStation))
					sucessResStation.Add(strResStation,strResStation);
			}
			foreach(StationBOM sbom in InStationResult)
			{
				string strResStation = sbom.ResourceCode.Trim() + sbom.StationCode.Trim();
				if(sucessResStation.Contains(strResStation))
				{
					sbom.CompareResult = "替代料";
				}
			}

			#endregion

			returnHT["SucessResult"] = SucessResult;
			returnHT["InStationResult"] = InStationResult;
			returnHT["InMoBOMResult"] = InMoBOMResult;
		
			return returnHT;
		}


		//获取站表导入实体
		private object[] GetUploadStationItemBOM()
		{
			string fileName = string.Empty ;

			fileName = this.ViewState["UploadedFileName"].ToString() ;
            
			string configFile = this.getParseConfigFileName() ;

			DataFileParser parser = new DataFileParser();
			parser.FormatName = "StationBOM" ;
			parser.ConfigFile = configFile ;
			object[] stboms = parser.Parse(fileName) ;

			return stboms ;

		}
		//取得(工单物料清单)导入实体
		private object[] GetUploadMOBOM()
		{
			string fileName = string.Empty ;
			fileName = this.ViewState["UploadedFileName2"].ToString() ;
            
			string configFile = this.getParseConfigFileName() ;

			DataFileParser parser = new DataFileParser();
			parser.FormatName = "MOItemBOM" ;
			parser.ConfigFile = configFile ;
			object[] moboms = parser.Parse(fileName) ;

			return moboms ;
		}

		#endregion

		private void Submit1_ServerClick(object sender, System.EventArgs e)
		{
			this.ExecuteClientFunction("popErrorPage","");
		}

		#endregion

		private void rblMOBOMSourceSelect_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.hidComparesource.Value = rblMOBOMSourceSelect.SelectedValue;
			this.ExecuteClientFunction("OnMOBOMSourceChange","");
			this.ExecuteClientFunction("OnMOBOMSourceChange","");
		}

		

	}
	
}
