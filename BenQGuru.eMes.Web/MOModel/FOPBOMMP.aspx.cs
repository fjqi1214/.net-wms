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
using BenQGuru.eMES.BaseSetting;
#endregion


namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FOPBOMMP 的摘要说明。
	/// </summary>
	public partial class FOPBOMMP : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private OPBOMFacade _opBOMFacade;// = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();
		private ItemFacade _itemFacade;// = new FacadeFactory(base.DataProvider).CreateItemFacade();
		private BaseModelFacade _baseModelFacade;// =new FacadeFactory(base.DataProvider).CreateBaseModelFacade();
		
		private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
		private BenQGuru.eMES.Web.Helper.ButtonHelper buttonHelper = null;
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
			this.excelExporter.LanguageComponent = null;
			this.excelExporter.Page = null;
			this.excelExporter.RowSplit = "\r\n";

		}
		#endregion

		#region  page events
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.InitOnPostBack();

			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.InitUI();	

				this.InitButton();
				this.InitWebGrid();

				//this.pagerSizeSelector.Readonly = true;
			}
		}
		protected void drpItemCodeEdit_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
				DropDownListBuilder builder =new DropDownListBuilder(this.drpItemCodeEdit);
				builder.HandleGetObjectList = new GetObjectListDelegate(this._itemFacade.GetAllItem);
				builder.Build("ItemCode","ItemCode");
				ListItem item  = new ListItem("",string.Empty);
				this.drpItemCodeEdit.Items.Insert(0,item);
			}
		
		}

		protected void drpRouteCodeEdit_Load(object sender, System.EventArgs e)
		{
			if(_baseModelFacade==null){_baseModelFacade =new FacadeFactory(base.DataProvider).CreateBaseModelFacade();}
			if(!IsPostBack)
			{
				DropDownListBuilder builder =new DropDownListBuilder(this.drpRouteCodeEdit);
				builder.HandleGetObjectList = new GetObjectListDelegate(this._baseModelFacade.GetAllRoute);
				builder.Build("RouteCode","RouteCode");
				ListItem item  = new ListItem("",string.Empty);
				this.drpRouteCodeEdit.Items.Insert(0,item);
			}
		
		}
		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{	
			if(_opBOMFacade==null){_opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();}
			object opbom = this.GetEditObject();
			if(opbom != null)
			{
				this._opBOMFacade.AddOPBOM( (OPBOM)opbom);

				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			}
		}

		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			if(_opBOMFacade==null){_opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{
				ArrayList oPBOMs = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					object obj = this.GetEditObject(row);
					if( obj != null )
					{
						oPBOMs.Add( (OPBOM)obj );
					}
				}

				this._opBOMFacade.DeleteOPBOM( (OPBOM[])oPBOMs.ToArray( typeof(OPBOM) ) );

				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
			}
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_opBOMFacade==null){_opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();}
			object opbom = this.GetEditObject();
			if(opbom != null)
			{
				this._opBOMFacade.UpdateOPBOM( (OPBOM)opbom );

				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
			}
		}

		protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
		{
			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );		
		}

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			this.RequestData();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Query );		
		}

		private void RequestData()
		{
			// 2005-04-06
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}

		private void gridWebGrid_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			object obj = this.GetEditObject(e.Row);
			
			if ( obj != null )
			{
				this.SetEditObject( obj );
				this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
			}

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

		private void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.drpItemCodeEdit.Enabled = true;
				this.txtOPBOMCodeEdit.Text = string.Empty;
				this.drpRouteCodeEdit.Enabled = true;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.drpItemCodeEdit.Enabled = false;
				this.txtOPBOMCodeEdit.ReadOnly = true;
				this.drpRouteCodeEdit.Enabled = false;
			}
		}

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if ( this.gridHelper.IsClickEditColumn(e) )
			{
				object obj = this.GetEditObject(e.Cell.Row);

				if ( obj != null )
				{
					this.SetEditObject( obj );

					this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
				}
			}
			if(e.Cell.Column.Key == "detail")
			{
				Response.Redirect(this.MakeRedirectUrl("FOPBOMOperationListMP.aspx",
					new string[]{"itemcode","opbomcode","opbomversion","routecode"},
					new string[]{e.Cell.Row.Cells[1].Text, e.Cell.Row.Cells[2].Text, e.Cell.Row.Cells[7].Text, e.Cell.Row.Cells[3].Text}));  
			}
		}

		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}

		
		#endregion

		#region private method
		private void InitOnPostBack()
		{		
			this.buttonHelper = new ButtonHelper(this);
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
			this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
			
			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
		}

		private string[] FormatExportRecord( object obj )
		{
			return new string[]{((OPBOM)obj).ItemCode.ToString(),
								   ((OPBOM)obj).OPBOMCode.ToString(),
								   ((OPBOM)obj).OPBOMRoute.ToString(),
								   ((OPBOM)obj).OPBOMDescription.ToString() };
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {"ItemCode",
									"OPBOMCode",
									"OPBOMRoute",	
									"OPBOMDescription"
									 };
		}

		private object[] LoadDataSource()
		{
			return this.LoadDataSource( 1, int.MaxValue );
		}

		private void InitButton()
		{	
			this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			this.buttonHelper.AddDeleteConfirm();
		}
		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "ItemCode", "产品代码",	null);
			this.gridHelper.AddColumn( "OPBOMCode", "工序物料清单代码",	null);
			this.gridHelper.AddColumn( "OPBOMRoute", "途程代码",	null);
			this.gridHelper.AddColumn( "OPBOMDescription", "描述",	null);
			this.gridHelper.AddLinkColumn("detail","详细信息",null);
			

			this.gridHelper.AddDefaultColumn( true, true );
			//this.gridHelper.ApplyDefaultStyle();

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((OPBOM)obj).ItemCode.ToString(),
								((OPBOM)obj).OPBOMCode.ToString(),
								((OPBOM)obj).OPBOMRoute.ToString(),
								((OPBOM)obj).OPBOMDescription.ToString(),
								"","",
			                    ((OPBOM)obj).OPBOMVersion.ToString()});
		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_opBOMFacade==null){_opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();}
			return this._opBOMFacade.QueryOPBOM( 
			FormatHelper.PKCapitalFormat(	FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
				FormatHelper.CleanString(this.txtBOMCodeQuery.Text),
				string.Empty,
				inclusive, exclusive,GlobalVariables.CurrentOrganizations.First().OrganizationID );
		}


		private int GetRowCount()
		{
			if(_opBOMFacade==null){_opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();}
			return this._opBOMFacade.QueryOPBOMCount(
			FormatHelper.PKCapitalFormat(	FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
			FormatHelper.PKCapitalFormat(	FormatHelper.CleanString(this.txtBOMCodeQuery.Text)),string.Empty, GlobalVariables.CurrentOrganizations.First().OrganizationID);										
		}


		private object GetEditObject()
		{
			if(_opBOMFacade==null){_opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();}
			if( this.ValidateInput())
			{
				OPBOM oPBOM = this._opBOMFacade.CreateNewOPBOM();

				oPBOM.ItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.drpItemCodeEdit.SelectedValue, 40));
				oPBOM.OPBOMCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPBOMCodeEdit.Text, 40));
				oPBOM.OPBOMDescription = FormatHelper.CleanString(this.txtDescriptionEdit.Text, 100);
				oPBOM.OPBOMRoute = FormatHelper.CleanString(this.drpRouteCodeEdit.SelectedValue, 40);
				//oPBOM.OPBOMVersion = FormatHelper.PKCapitalFormat(Const.BOM_VERSION);
				oPBOM.MaintainUser = this.GetUserCode();
                oPBOM.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
				return oPBOM;
			}
			else
			{
				return null;
			}
		}


		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_opBOMFacade==null){_opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();}
			object obj = _opBOMFacade.GetOPBOM( row.Cells[1].Text,row.Cells[2].Text,row.Cells[7].Text,GlobalVariables.CurrentOrganizations.First().OrganizationID );
			
			if (obj != null)
			{
				return (OPBOM)obj;
			}

			return null;
		}

		private void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.drpItemCodeEdit.SelectedIndex =0;
				this.txtOPBOMCodeEdit.Text = string.Empty;
				this.drpRouteCodeEdit.SelectedIndex =0;
				this.txtDescriptionEdit.Text = string.Empty;
				return;
			}

			try
			{
				this.drpItemCodeEdit.SelectedValue = ((OPBOM)obj).ItemCode.ToString();
			}
			catch
			{
				this.drpItemCodeEdit.SelectedIndex =0;
			}
			this.txtOPBOMCodeEdit.Text = ((OPBOM)obj).OPBOMCode;
			this.drpRouteCodeEdit.SelectedValue = ((OPBOM)obj).OPBOMRoute;
			this.txtDescriptionEdit.Text = ((OPBOM)obj).OPBOMDescription;

		}

		
		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck(lblItemCodeEdit, drpItemCodeEdit, 40, true) );
			manager.Add( new LengthCheck(lblOPBOMCodeEdit, txtOPBOMCodeEdit, 40, true) );
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}


		#endregion

	}
}
