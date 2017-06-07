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
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion


namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FModelRouteSP 的摘要说明。
	/// </summary>
	public partial class FModelRouteOperationSP : BasePage
	{
		protected System.Web.UI.WebControls.CheckBox chbSelectAll;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;


		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		private ModelFacade _modelFacade ;//= new FacadeFactory(base.DataProvider).CreateModelFacade();
	
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
			this.excelExporter.LanguageComponent = null;
			this.excelExporter.Page = this;
			this.excelExporter.RowSplit = "\r\n";

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
				this.InitWebGrid();
				Initparamters();
				RequestData();
				SetEditObject(null);
			}
		}

		protected override void InitUI()
		{
			base.InitUI ();
			this.cmdSave.Disabled = true;
		}




		private void gridWebGrid_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
				this.ViewState["opid"] = e.Row.Cells[5].ToString();
				SetEditObject(this._modelFacade.GetModel2Operation(OPID,GlobalVariables.CurrentOrganizations.First().OrganizationID));
		}
		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FModelRouteEP.aspx"));
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			if(ValidateInput())
			{
				this._modelFacade.UpdateModel2Operation((Model2OP)GetEditObject());
				this.RequestData();
			}
		}
		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			if ( this.gridHelper.IsClickEditColumn(e) )
			{
				this.ViewState["opid"] = e.Cell.Row.Cells[5].ToString();
				SetEditObject(this._modelFacade.GetModel2Operation(OPID,GlobalVariables.CurrentOrganizations.First().OrganizationID));
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
			if(Request.Params["modelcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["modelcode"] = Request.Params["modelcode"].ToString();
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

		public string ModelCode
		{
			get
			{
				return (string)ViewState["modelcode"];
			}
		}

		public string RouteCode
		{
			get
			{
				return (string)ViewState["routecode"];
			}
		}

		public string OPID
		{
			get
			{
				return (string)ViewState["opid"];
			}
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "ModelCode", "产品别代码",	null);
			this.gridHelper.AddColumn( "Route", "途程代码",	null);
			this.gridHelper.AddColumn( "OPCode", "工序代码",	null);
			this.gridHelper.AddColumn( "OPSequence", "工序序号",	null);
			this.gridHelper.AddDefaultColumn( false, true );
			//this.gridHelper.ApplyDefaultStyle();

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private void RequestData()
		{
			// 2005-04-06
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}

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
		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._modelFacade.GetModel2Operations(
			FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ModelCode)),FormatHelper.PKCapitalFormat(FormatHelper.CleanString(RouteCode)),
                GlobalVariables.CurrentOrganizations.First().OrganizationID,
				inclusive, exclusive);
		}

		private string[] FormatExportRecord( object obj )
		{
			return new string[]{((Model2OP)obj).ModelCode.ToString(),
								   ((Model2OP)obj).RouteCode.ToString(),
								   ((Model2OP)obj).OPCode.ToString(),
								   ((Model2OP)obj).OPSequence.ToString()};
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"ModelCode",
									"Route",
									"OPCode",	
									"OPSequence"
									 };
		}

		private object[] LoadDataSource()
		{
			return this.LoadDataSource( 1, int.MaxValue );
		}

		private int GetRowCount()
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._modelFacade.GetModel2OperationsCounts(
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString(ModelCode)),
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString(RouteCode)), 
                GlobalVariables.CurrentOrganizations.First().OrganizationID);
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								((Model2OP)obj).ModelCode.ToString(),
								((Model2OP)obj).RouteCode.ToString(),
								((Model2OP)obj).OPCode.ToString(),
								((Model2OP)obj).OPSequence.ToString(),"",
				                ((Model2OP)obj).OPID.ToString()
							});
		}

		private void SetEditObject(object obj)
		{
			if(obj == null)
			{
				this.txtOperationsequenceEdit.Text = string.Empty;
				this.chbOperationCheckEdit.Checked = false;
				this.chbCompLoadingEdit.Checked = false;
				this.chbIDMergeEdit.Checked = false;
				this.chbStartOpEdit.Checked = false;
				this.chbEndOpEdit.Checked = false;
				this.chbPackEdit.Checked = false;
				this.chbEditSPC.Checked = false;
				this.chbRepairEdit.Checked = false;
				this.chbNGTestEdit.Checked = false;
				this.pnlMainEdit.Visible = false;
				this.cmdSave.Disabled = true;
			}
			else
			{
				Model2OP model2Operation = (Model2OP)obj;
				this.txtOperationsequenceEdit.Text = model2Operation.OPSequence.ToString();
				this.chbOperationCheckEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-9);
				this.chbCompLoadingEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-8);
				this.chbIDMergeEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-7);
				this.chbStartOpEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-6);
				this.chbEndOpEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-5);
				this.chbPackEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-4);
				this.chbEditSPC.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-3);
				this.chbRepairEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-2);
				this.chbNGTestEdit.Checked = FormatHelper.StringToBoolean(model2Operation.OPControl,model2Operation.OPControl.Length-1);
				if(this.chbIDMergeEdit.Checked)
				{
					this.pnlMainEdit.Visible = true;
					this.drpMergeTypeEdit.SelectedValue = model2Operation.IDMergeType;
					this.txtDenominatorEdit.Text =model2Operation.IDMergeRule.ToString();
				}
				else
				{
					this.pnlMainEdit.Visible = false;
				}
				this.cmdSave.Disabled = false;
			}
		}

		private object GetEditObject()
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			Model2OP model2Operation = (Model2OP)this._modelFacade.GetModel2Operation(OPID,GlobalVariables.CurrentOrganizations.First().OrganizationID);
			try
			{
				model2Operation.OPSequence= System.Int32.Parse(this.txtOperationsequenceEdit.Text.Trim());
			}
			catch
			{
				model2Operation.OPSequence = 0;
			}
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-9,this.chbOperationCheckEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-8,this.chbCompLoadingEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-7,this.chbIDMergeEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-6,this.chbStartOpEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-5,this.chbEndOpEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-4,this.chbPackEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-3,this.chbEditSPC.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-2,this.chbRepairEdit.Checked);
			model2Operation.OPControl = FormatHelper.BooleanToString(model2Operation.OPControl,model2Operation.OPControl.Length-1,this.chbNGTestEdit.Checked);
			model2Operation.IDMergeType = FormatHelper.CleanString(this.drpMergeTypeEdit.SelectedValue);
			model2Operation.IDMergeRule =System.Int32.Parse( FormatHelper.CleanString(this.txtDenominatorEdit.Text));
            model2Operation.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
			return model2Operation;
		}

		
		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new NumberCheck(lblCodeEdit, txtOperationsequenceEdit, true) );
			if(pnlMainEdit.Visible)
			{
				manager.Add( new NumberCheck(lblCodeEdit, txtOperationsequenceEdit,0,int.MaxValue, true) );
			}
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false ;
			}
			return true;
		}

		#endregion

		protected void cbIDMergeEdit_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chbIDMergeEdit.Checked)
			{
				pnlMainEdit.Visible = true;
			}
			else
			{
				pnlMainEdit.Visible = false;
			}
		}

		protected void drpMergeTypeEdit_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				this.drpMergeTypeEdit.Items.Clear();
				this.drpMergeTypeEdit.Items.Add(new ListItem("",""));
				this.drpMergeTypeEdit.Items.Add(new ListItem( this.languageComponent1.GetString(IDMergeType.IDMERGETYPE_IDMERGE),IDMergeType.IDMERGETYPE_IDMERGE));
				this.drpMergeTypeEdit.Items.Add(new ListItem( this.languageComponent1.GetString(IDMergeType.IDMERGETYPE_ROUTER),IDMergeType.IDMERGETYPE_ROUTER));
			}
		}

		
		
		
	}
}
