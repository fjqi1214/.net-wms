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
	/// FOPBOMItemControlMP 的摘要说明。
	/// </summary>
	public partial class FOPBOMItemControlMP : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		private OPItemControlFacade _opItemControlFacade ;//= new FacadeFactory(base.DataProvider).CreateOPItemControlFacade();
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
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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
			this.excelExporter.LanguageComponent = this.languageComponent1;
			this.excelExporter.Page = this;
			this.excelExporter.RowSplit = "\r\n";

		}
		#endregion

		#region form events
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitHander();
			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				// 初始化界面UI
				this.InitUI();
				this.InitButton();
				this.InitWebGrid();
				Initparamters();
				RequestData();
				//this.pagerSizeSelector.Readonly = true;
			}
		}
		private void gridWebGrid_DblClick(object sender,Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			object obj = this.GetEditObject(e.Row);

			if ( obj != null )
			{
				this.SetEditObject( obj );

				this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
			}
		}

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
			if(_opItemControlFacade==null){_opItemControlFacade = new FacadeFactory(base.DataProvider).CreateOPItemControlFacade();}
			object opItemControl = this.GetEditObject();
			if(opItemControl != null)
			{
				this._opItemControlFacade.AddOPItemControl( (OPItemControl)opItemControl);

				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			}
		}

		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			if(_opItemControlFacade==null){_opItemControlFacade = new FacadeFactory(base.DataProvider).CreateOPItemControlFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{
				ArrayList opItemControls = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					object obj = this.GetEditObject(row);
					if( obj != null )
					{
						opItemControls.Add( (OPItemControl)obj );
					}
				}

				this._opItemControlFacade.DeleteItemControl( (OPItemControl[])opItemControls.ToArray( typeof(OPItemControl) ) );

				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
			}
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_opItemControlFacade==null){_opItemControlFacade = new FacadeFactory(base.DataProvider).CreateOPItemControlFacade();}
			object opItemControl = this.GetEditObject();
			if(opItemControl != null)
			{
				this._opItemControlFacade.UpdateItemControl((OPItemControl)opItemControl );

				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
			}
		}

		protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
		{
			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );
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
			Response.Redirect(this.MakeRedirectUrl("FOPBOMOperationComponetLoadingMP.aspx", 
				new string[]{"itemcode","opbomcode","opbomversion","routecode","opid","OrgID"},
                new string[] { ItemCode, OPBOMCode, OPBOMVersion, RouteCode, OPID, OrgID.ToString()}));
		}

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if(e.Cell.Column.Key=="Edit")
			{
				object obj = this.GetEditObject(e.Cell.Row);

				if ( obj != null )
				{
					this.SetEditObject( obj );

					this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
				}
			}
		}

		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}
		#endregion

		#region private method

		private void Initparamters()
		{
			if(Request.Params["itemcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["itemcode"] = Request.Params["itemcode"].ToString();
			}
			if(Request.Params["opbomcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["opbomcode"] = Request.Params["opbomcode"].ToString();
			}
			if(Request.Params["opbomversion"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["opbomversion"] = Request.Params["opbomversion"].ToString();
			}
			if(Request.Params["opid"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["opid"] = Request.Params["opid"].ToString();
			}
			if(Request.Params["opbomitemcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["opbomitemcode"] = Request.Params["opbomitemcode"].ToString();
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

		public string ItemCode
		{
			get
			{
				return (string)ViewState["itemcode"];
			}
		}

		public string OPBOMCode
		{
			get
			{
				return (string)ViewState["opbomcode"];
			}
		}

		public string OPBOMVersion
		{
			get
			{
				return (string)ViewState["opbomversion"];
			}
		}
		
		public string OPID
		{
			get
			{
				return (string)ViewState["opid"];
			}
		}

		public string OPBOMItemCode
		{
			get
			{
				return (string)ViewState["opbomitemcode"];
			}
		}

		public string RouteCode
		{
			get
			{
				return (string)ViewState["routecode"];
			}
		}

        public int OrgID
        {
            get
            {
                if (this.ViewState["OrgID"] == null)
                    return GlobalVariables.CurrentOrganizations.First().OrganizationID;
                else return int.Parse(this.ViewState["OrgID"].ToString());
            }
        }

		private void RequestData()
		{
			// 2005-04-06
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}
		private int GetRowCount()
		{
			if(_opItemControlFacade==null){_opItemControlFacade = new FacadeFactory(base.DataProvider).CreateOPItemControlFacade();}
			return this._opItemControlFacade.GetOPBOMItemControlCounts(
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString( ItemCode)),OPID,OPBOMItemCode,OPBOMCode,OPBOMVersion
				);
		}

		private void InitHander()
		{
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.buttonHelper = new ButtonHelper(this);
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
			// 2005-04-06
			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
		}


		private string[] FormatExportRecord( object obj )
		{
			return new string[]{((OPItemControl)obj).Sequence.ToString(),
								   ((OPItemControl)obj).VendorCode.ToString(),
								   ((OPItemControl)obj).VendorItemCode.ToString(),
								   ((OPItemControl)obj).PCBAVersion.ToString(),
								   ((OPItemControl)obj).BIOSVersion.ToString(),
								   ((OPItemControl)obj).CardStart.ToString(),
								   ((OPItemControl)obj).CardEnd.ToString(),
								   ((OPItemControl)obj).DateCodeStart.ToString(),
								   ((OPItemControl)obj).DateCodeEnd.ToString(),
								   ((OPItemControl)obj).ItemVersion.ToString(),
								   ((OPItemControl)obj).MEMO.ToString()};
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"Sequence",
									"VenderCode",
									"VenderItemCode",	
									"PCBAVersion",
									"BIOSVersion",	
									"CardStart",	
									"CardEnd",
									"DateCodeStart","DateCodeEnd","ItemVersion","Memo"};
		}

		private object[] LoadDataSource()
		{
			return this.LoadDataSource( 1, int.MaxValue );
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}

		public void InitButton()
		{	
			this.buttonHelper.AddDeleteConfirm();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "Sequence", "序号",	null);
			this.gridHelper.AddColumn( "VenderCode",		 "厂商代码",		null);
			this.gridHelper.AddColumn("VenderItemCode","厂商料号",null );
			this.gridHelper.AddColumn("PCBAVersion","PCBA版本",null );
			this.gridHelper.AddColumn("BIOSVersion","BIOS版本",null );
			this.gridHelper.AddColumn("CardStart","流程卡范围起始",null );
			this.gridHelper.AddColumn("CardEnd","流程卡范围结束",null );
			this.gridHelper.AddColumn("DateCodeStart","生产日期范围起始",null );
			this.gridHelper.AddColumn("DateCodeEnd","生产日期范围结束",null );
			this.gridHelper.AddColumn("ItemVersion","版本信息",new ColumnStyle() );
			this.gridHelper.AddColumn("Memo","备注",new ColumnStyle() );
			this.gridHelper.AddDefaultColumn( true, true );
			//this.gridHelper.ApplyDefaultStyle();

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private  object GetEditObject()
		{
			if(_opItemControlFacade==null){_opItemControlFacade = new FacadeFactory(base.DataProvider).CreateOPItemControlFacade();}
			if(this.ValidateInput())
			{

				OPItemControl opItemControl = (OPItemControl)this._opItemControlFacade.CreateNewOPItemControl();

				opItemControl.ItemCode = ItemCode;
				opItemControl.BIOSVersion = FormatHelper.CleanString(txtBIOSVersionEdit.Text.ToString(),100);
				opItemControl.CardEnd = FormatHelper.CleanString(this.txtCardEndEdit.Text, 40);
				opItemControl.CardStart = FormatHelper.CleanString(this.txtCardStartEdit.Text, 40);
				opItemControl.DateCodeEnd = FormatHelper.CleanString(this.txtDateCodeEndEdit.Text, 40);
				opItemControl.DateCodeStart = FormatHelper.CleanString(this.txtDataCodeStartEdit.Text, 40);
				opItemControl.ItemCode = ItemCode;
				opItemControl.ItemVersion = FormatHelper.CleanString(this.txtItemVersion.Text, 100);
				opItemControl.MEMO = FormatHelper.CleanString(this.txtMemoEdit.Text, 100);
				opItemControl.OPBOMCode = OPBOMCode;
				opItemControl.OPBOMVersion = OPBOMVersion;
				opItemControl.OPID = OPID;
				opItemControl.OPBOMItemCode = OPBOMItemCode;
				opItemControl.PCBAVersion = FormatHelper.CleanString(this.txtPCBAVersionEdit.Text, 100);
				opItemControl.VendorCode = FormatHelper.CleanString(this.txtSupplierCodeEdit.Text, 100);
				opItemControl.VendorItemCode = FormatHelper.CleanString(this.txtSupplierCodeEdit.Text, 100);
				opItemControl.MaintainUser = this.GetUserCode();

				return opItemControl;
			}
			else
			{
				return null;
			}
		}

		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_opItemControlFacade==null){_opItemControlFacade = new FacadeFactory(base.DataProvider).CreateOPItemControlFacade();}
			int sequence = 0;
			try
			{
				sequence = Int32.Parse(row.Cells[1].Text);
			}
			catch
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_System_Error");
			}
			object obj = this._opItemControlFacade.GetOPBOMItemControl(ItemCode,OPID,OPBOMItemCode,OPBOMCode,OPBOMVersion,sequence);
			
			if (obj != null)
			{
				return (OPItemControl)obj;
			}

			return null;
		}

		private bool ValidateInput()
		{

			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck(lblSupplierCodeEdit, txtSupplierCodeEdit, 40, true) );
			manager.Add( new LengthCheck(lblSupplierItemEdit, txtSupplierItemEdit, 40, true) );
			manager.Add( new LengthCheck(lblPCBAVersionDescriptionEdit, txtPCBAVersionEdit, 40, true) );
			manager.Add( new LengthCheck(lblBIOSVersionDescriptionEdit, txtBIOSVersionEdit, 40, true) );
			manager.Add( new LengthCheck(lblCardStartEdit, txtCardStartEdit, 40, true) );
			manager.Add( new LengthCheck(lblCardEndEdit, txtCardEndEdit, 40, true) );
			manager.Add( new LengthCheck(lblDataCodeStartEdit, txtDataCodeStartEdit, 40, true) );
			manager.Add( new LengthCheck(lblDateCodeEndEdit, txtDateCodeEndEdit, 40, true) );
			manager.Add( new LengthCheck(lblItemVersion, txtItemVersion, 40, true) );
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
			//manager.Add( new LengthCheck(lblItemVersion, txtItemVersion, 40, true) );

			//			if((this.txtCardStartEdit.Text.Trim().Length != this.txtCardEndEdit.Text.Trim().Length))
			//			{
			//				throw new Exception(ErrorCenter.GetErrorUserDescription( GetType().BaseType, string.Format(ErrorCenter.ERROR_WITHOUTINPUT, "流程卡范围起始和结束必须等长！") ) );
			//			}
			//			if((this.txtDataCodeStartEdit.Text.Trim().Length != this.txtDateCodeEndEdit.Text.Trim().Length))
			//			{
			//				throw new Exception(ErrorCenter.GetErrorUserDescription( GetType().BaseType, string.Format(ErrorCenter.ERROR_WITHOUTINPUT, "生产日期范围起始和结束必须等长！") ) );
			//			}
		}

		private void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtSupplierCodeEdit.Text= string.Empty;
				this.txtSupplierItemEdit.Text= string.Empty;
				this.txtPCBAVersionEdit.Text = string.Empty;
				this.txtBIOSVersionEdit.Text = string.Empty;
				this.txtCardStartEdit.Text   = string.Empty;
				this.txtCardEndEdit.Text     = string.Empty;
				this.txtDataCodeStartEdit.Text=string.Empty;
				this.txtDateCodeEndEdit.Text = string.Empty;
				this.txtMemoEdit.Text        = string.Empty;
				this.txtItemVersion.Text     = string.Empty;
				return;
			}

			this.txtSupplierCodeEdit.Text= ((OPItemControl)obj).VendorCode.ToString();
			this.txtSupplierItemEdit.Text= ((OPItemControl)obj).VendorItemCode.ToString();
			this.txtPCBAVersionEdit.Text = ((OPItemControl)obj).PCBAVersion.ToString();
			this.txtBIOSVersionEdit.Text = ((OPItemControl)obj).BIOSVersion.ToString();
			this.txtCardStartEdit.Text   = ((OPItemControl)obj).CardStart.ToString();
			this.txtCardEndEdit.Text     = ((OPItemControl)obj).CardEnd.ToString();
			this.txtDataCodeStartEdit.Text=((OPItemControl)obj).DateCodeStart.ToString();
			this.txtDateCodeEndEdit.Text = ((OPItemControl)obj).DateCodeEnd.ToString();
			this.txtItemVersion.Text     = ((OPItemControl)obj).ItemVersion.ToString();
			this.txtMemoEdit.Text        = ((OPItemControl)obj).MEMO.ToString();
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((OPItemControl)obj).Sequence.ToString(),
								((OPItemControl)obj).VendorCode.ToString(),
								((OPItemControl)obj).VendorItemCode.ToString(),
								((OPItemControl)obj).PCBAVersion.ToString(),
								((OPItemControl)obj).BIOSVersion.ToString(),
								((OPItemControl)obj).CardStart.ToString(),
								((OPItemControl)obj).CardEnd.ToString(),
								((OPItemControl)obj).DateCodeStart.ToString(),
								((OPItemControl)obj).DateCodeEnd.ToString(),
								((OPItemControl)obj).ItemVersion.ToString(),
								((OPItemControl)obj).MEMO.ToString(),
								""});
		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_opItemControlFacade==null){_opItemControlFacade = new FacadeFactory(base.DataProvider).CreateOPItemControlFacade();}
			return this._opItemControlFacade.GetOPBOMItemControl(
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString( ItemCode)),OPID,OPBOMItemCode,OPBOMCode,OPBOMVersion,
				inclusive, exclusive );
		}
		#endregion

		
	}
}
