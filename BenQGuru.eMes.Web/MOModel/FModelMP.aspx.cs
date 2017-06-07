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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
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
	/// FModelMP 的摘要说明。
	/// </summary>
    public partial class FModelMP : BaseMPageMinus
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;


		//private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		private ModelFacade _modelFacade ;//= FacadeFactory.CreateModelFacade();
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
			//this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
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

                BuildOrgList();
			}
		}
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);
			this.txtDataLinkQty.Enabled = this.chbIsDataLink.Checked;
			this.txtDimQty.Enabled = this.chbIsDim.Checked;
		}

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			this.RequestData();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Query );	
		}

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			object model = this.GetEditObject();
			if(model != null)
			{
				this._modelFacade.AddModel( (Model)model );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			}
		}

		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{
				ArrayList models = new ArrayList( array.Count );
			
				foreach (GridRecord row in array)
				{
					object model = this.GetEditObject(row);
					if( model != null )
					{
						models.Add( (Model)model );
					}
				}

				this._modelFacade.DeleteModel( (Model[])models.ToArray( typeof(Model) ) );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
			}
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			object model = this.GetEditObject();
			if(model != null)
			{
				this._modelFacade.UpdateModel((Model)model );
				this.RequestData();

				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
			}
		}

		protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
		{
			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );
		}

        protected override void Grid_ClickCell(GridRecord row, string commandName)
		{
            if (commandName == "AModelCodeTitle")
			{
				Response.Redirect(this.MakeRedirectUrl("FBarcodeRuleMP.aspx",new string[] {"modelcode"},new string[] { row.Items.FindItemByKey("ModelCode" ).Value.ToString()}));
			}
            else if (commandName == "ItemCodeList")
			{
				Response.Redirect(this.MakeRedirectUrl("FModelItemEP.aspx", new string[] {"modelcode"},new string[] { row.Items.FindItemByKey("ModelCode" ).Value.ToString()} ));
			}
            else if (commandName == "routealt")
			{
				Response.Redirect(this.MakeRedirectUrl("FModelRoutealtEP.aspx", new string[] {"modelcode"},new string[] { row.Items.FindItemByKey("ModelCode" ).Value.ToString()}));
			}
            else if (commandName == "route")
			{
				Response.Redirect(this.MakeRedirectUrl("FModelRouteEP.aspx",  new string[] {"modelcode"},new string[] { row.Items.FindItemByKey("ModelCode" ).Value.ToString()}));
			}
            else if (commandName == "Edit")
			{
				object obj = this.GetEditObject(row);

				if ( obj != null )
				{
					this.SetEditObject( obj );

					this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
				}
			}
            else if (commandName.ToUpper() == "SELECTERRORCODEGROUP")
			{
				this.Response.Redirect(this.MakeRedirectUrl("../tsmodel/FModel2ErrorCodeGroupSP.aspx", new string[] {"modelcode"},new string[] { row.Items.FindItemByKey("ModelCode" ).Value.ToString()}));
			}
            else if (commandName.ToUpper() == "SELECTERRORCAUSEGROUP")
			{
				this.Response.Redirect(this.MakeRedirectUrl("../tsmodel/FModel2ErrorCauseGroupSP.aspx",new string[] {"modelcode"},new string[] { row.Items.FindItemByKey("ModelCode" ).Value.ToString()}));
			}
            else if (commandName.ToUpper() == "ERRORSYMPTOMLIST")/* added by jessie lee, 2006/6/28, for RMA */
			{
				this.Response.Redirect(this.MakeRedirectUrl("FModel2ErrorSymptomSP.aspx",new string[] {"modelcode"},new string[] { row.Items.FindItemByKey("ModelCode" ).Value.ToString()}));
			}
            else if (commandName.ToUpper() == "SELECTSOLUTION")
			{
				this.Response.Redirect(this.MakeRedirectUrl("../tsmodel/FModel2SolutionSP.aspx",new string[] {"modelcode"},new string[] { row.Items.FindItemByKey("ModelCode" ).Value.ToString()}));
			}
		}

		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}

		#endregion

		#region private method

		private void RequestData()
		{
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}

		private int GetRowCount()
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._modelFacade.QueryModelsCount(FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtModelCodeQuery.Text)));
		}


		private void InitHander()
		{
			this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

			this.buttonHelper = new ButtonHelper(this);
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
			this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
		}



		private void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if(pageAction == PageActionType.Save)
			{
				this.txtModelCodeEdit.ReadOnly = false;
                this.DropDownListOrg.Enabled = true;
			}
			if(pageAction == PageActionType.Update)
			{
				this.txtModelCodeEdit.ReadOnly = true;
                this.DropDownListOrg.Enabled = false;
			}
			if ( pageAction == PageActionType.Cancel )
			{
				this.txtModelCodeEdit.ReadOnly = false;
                this.DropDownListOrg.Enabled = true;
			}
			
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
			this.gridHelper.AddColumn( "ModelCode", "产品别代码",	null);
			//this.gridWebGrid.Columns.FromKey("ModelCode").Width = new Unit(150);
			this.gridHelper.AddColumn( "ModelDescription",		 "产品别描述",		null);
			this.gridHelper.AddColumn( "IsInventory","是否可入库",null);
			this.gridHelper.AddColumn( "IsReflow","是否使用回流",null);
			this.gridHelper.AddColumn( "IsDim","是否检查尺寸量测数量",null);
			this.gridHelper.AddColumn( "DimQty","尺寸量测数量",null);
			this.gridHelper.AddColumn( "IsCheckDataLink","是否检查设备连线数量",null);
			this.gridHelper.AddColumn( "DataLinkQty","连线样本的数量",null);

			//this.gridWebGrid.Columns.FromKey("IsCheckDataLink").Width = new Unit(70);
			//this.gridWebGrid.Columns.FromKey("IsInventory").Width = new Unit(70);
			//this.gridWebGrid.Columns.FromKey("IsReflow").Width = new Unit(70);

			//this.gridWebGrid.Columns.FromKey("ModelDescription").Width = new Unit(150);
			this.gridHelper.AddLinkColumn("AModelCodeTitle","二维条码解析相关资料维护",new ColumnStyle() );
			this.gridHelper.AddLinkColumn("ItemCodeList","产品列表",new ColumnStyle() );
			this.gridHelper.AddLinkColumn("routealt","途程群组信息",new ColumnStyle() );
			this.gridHelper.AddLinkColumn("route","生产途程列表",new ColumnStyle() );
			this.gridHelper.AddLinkColumn("SelectErrorCodeGroup","不良代码组列表",null);
			this.gridHelper.AddLinkColumn("SelectErrorCauseGroup","不良原因组列表",null);
			this.gridHelper.AddLinkColumn("SelectSolution","解决方案列表",null);
			this.gridHelper.AddLinkColumn("ErrorSymptomList","不良现象列表",null);
			
			this.gridHelper.AddColumn("MaintainUser","维护人员",null);
			this.gridHelper.AddColumn("MaintainDate","维护日期",null);

            this.gridHelper.AddColumn("OrganizationID", "组织编号", null);

            this.gridWebGrid.Columns.FromKey("routealt").Hidden = true ;
			this.gridWebGrid.Columns.FromKey("route").Hidden = true ;
			this.gridWebGrid.Columns.FromKey("ItemCodeList").Hidden = true ;		//产品列表 InternalFeedback要求不在这里维护料品
            this.gridWebGrid.Columns.FromKey("OrganizationID").Hidden = true;

            /*
            Added By Hi1/Venus.Feng on 20080707 for Hisense:mark these columns             
            */
            this.gridWebGrid.Columns.FromKey("IsReflow").Hidden = true;
            this.gridWebGrid.Columns.FromKey("IsDim").Hidden = true;
            this.gridWebGrid.Columns.FromKey("IsCheckDataLink").Hidden = true;
            this.gridWebGrid.Columns.FromKey("DimQty").Hidden = true;
            this.gridWebGrid.Columns.FromKey("AModelCodeTitle").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ErrorSymptomList").Hidden = true;
            this.gridWebGrid.Columns.FromKey("DataLinkQty").Hidden = true;
            this.gridWebGrid.Columns.FromKey("IsInventory").Hidden = true;
            // End

            //this.gridWebGrid.Columns.FromKey("SelectErrorCodeGroup").Width = 80;
            //this.gridWebGrid.Columns.FromKey("SelectErrorCauseGroup").Width = 80;
            //this.gridWebGrid.Columns.FromKey("SelectSolution").Width = 80;

            this.gridHelper.AddDefaultColumn( true, true );
			//this.gridHelper.ApplyDefaultStyle();

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private  object GetEditObject()
		{
			if(this.ValidateInput())
			{
				if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
				Model model = this._modelFacade.CreateNewModel();

				model.ModelCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtModelCodeEdit.Text, 40));
				model.ModelDescription = FormatHelper.CleanString(this.txtModelDescriptionEdit.Text, 100);
				model.MaintainUser = this.GetUserCode();
				model.IsInventory = FormatHelper.BooleanToString(this.chbIsIn.Checked);
				model.IsReflow = FormatHelper.BooleanToString(this.chbIsReflow.Checked);
				model.IsCheckDataLink = FormatHelper.BooleanToString(this.chbIsDataLink.Checked);
				model.DataLinkQty = (this.chbIsDataLink.Checked?int.Parse(this.txtDataLinkQty.Text):0);
				model.IsDim =  FormatHelper.BooleanToString(this.chbIsDim.Checked);
				model.DimQty = (this.chbIsDim.Checked?int.Parse(this.txtDimQty.Text):0);
                model.OrganizationID = int.Parse(this.DropDownListOrg.SelectedValue);
				return model;
			}
			else
			{
				return null;
			}
		}

		private object GetEditObject(GridRecord row)
		{	
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
            object obj = this._modelFacade.GetModel(row.Items.FindItemByKey("ModelCode").Value.ToString(), int.Parse(row.Items.FindItemByKey("OrganizationID").Value.ToString()));
			
			if (obj != null)
			{
				return (Model)obj;
			}

			return null;
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck(lblModelCodeEdit, txtModelCodeEdit, 40, true) );
            manager.Add(new LengthCheck(lblOrgEdit, DropDownListOrg, 8, true));
			
			if(this.chbIsDataLink.Checked)
				manager.Add(new NumberCheck(this.chbIsDataLink,this.txtDataLinkQty,0,int.MaxValue,true));
			
			if(this.chbIsDim.Checked)
				manager.Add(new NumberCheck(this.chbIsDim,this.txtDimQty,0,int.MaxValue,true));
			

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}

		private void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtModelCodeEdit.Text=string.Empty;
				this.txtModelDescriptionEdit.Text=string.Empty;
				this.chbIsIn.Checked = false;
				this.chbIsReflow.Checked = false;
				this.chbIsDataLink.Checked = false;
				this.txtDataLinkQty.Text = string.Empty;
				this.chbIsDim.Checked = false;
				this.txtDimQty.Text = string.Empty;
                this.DropDownListOrg.SelectedIndex = 0;
				return;
			}

			this.txtModelCodeEdit.Text	= ((Model)obj).ModelCode.ToString();
			
			this.txtModelDescriptionEdit.Text	= ((Model)obj).ModelDescription.ToString();

			this.chbIsIn.Checked = (((Model)obj).IsInventory == FormatHelper.TRUE_STRING);
			this.chbIsReflow.Checked = (((Model)obj).IsReflow == FormatHelper.TRUE_STRING);
			this.chbIsDataLink.Checked = (((Model)obj).IsCheckDataLink == FormatHelper.TRUE_STRING);
			this.txtDataLinkQty.Text = ((Model)obj).DataLinkQty.ToString();

			this.chbIsDim.Checked = (((Model)obj).IsDim == FormatHelper.TRUE_STRING);
			this.txtDimQty.Text = ((Model)obj).DimQty.ToString();
            try
            {
                this.DropDownListOrg.SelectedValue = ((Model)obj).OrganizationID.ToString();
            }
            catch
            {
                this.DropDownListOrg.SelectedIndex = 0;
            }
		}

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ModelCode"] = ((Model)obj).ModelCode.ToString();
            row["ModelDescription"] = ((Model)obj).ModelDescription.ToString();
            row["IsInventory"] = FormatHelper.DisplayBoolean(((Model)obj).IsInventory, this.languageComponent1);
            row["IsReflow"] = FormatHelper.DisplayBoolean(((Model)obj).IsReflow, this.languageComponent1);
            row["IsDim"] = FormatHelper.DisplayBoolean(((Model)obj).IsDim, this.languageComponent1);
            row["DimQty"] = ((Model)obj).DimQty.ToString();
            row["IsCheckDataLink"] = FormatHelper.DisplayBoolean(((Model)obj).IsCheckDataLink, this.languageComponent1);
            row["DataLinkQty"] = ((Model)obj).DataLinkQty.ToString();
            row["AModelCodeTitle"] = "";
            row["ItemCodeList"] = "";
            row["routealt"] = "";
            row["route"] = "";
            row["SelectErrorCodeGroup"] = "";
            row["SelectErrorCauseGroup"] = "";
            row["SelectSolution"] = "";
            row["ErrorSymptomList"] = "";
            row["MaintainUser"] = ((Model)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Model)obj).MaintainDate);
            row["OrganizationID"] = ((Model)obj).OrganizationID.ToString();
            return row;
        }

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_modelFacade==null){_modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();}
			return this._modelFacade.QueryModels(
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModelCodeQuery.Text)),
				inclusive, exclusive );
		}

		private string[] FormatExportRecord( object obj )
		{
            /*
			return new string[]{((Model)obj).ModelCode.ToString(),
								   ((Model)obj).ModelDescription.ToString(),
								   FormatHelper.DisplayBoolean(((Model)obj).IsInventory,this.languageComponent1),
								   FormatHelper.DisplayBoolean(((Model)obj).IsReflow,this.languageComponent1),
								   FormatHelper.DisplayBoolean(((Model)obj).IsCheckDataLink,this.languageComponent1),
								   ((Model)obj).DataLinkQty.ToString(),
								   FormatHelper.DisplayBoolean(((Model)obj).IsDim,this.languageComponent1),
								   ((Model)obj).DimQty.ToString(),
                                   ((Model)obj).OrganizationID.ToString(),
				                   ((Model)obj).MaintainUser.ToString(),
								   FormatHelper.ToDateString(((Model)obj).MaintainDate)
							   };
            */

            /*
            Modified By Hi1/Venus.Feng on 20080707 for Hisense:mark these columns             
            */
            return new string[]{((Model)obj).ModelCode.ToString(),
								   ((Model)obj).ModelDescription.ToString(),
								   FormatHelper.DisplayBoolean(((Model)obj).IsInventory,this.languageComponent1),
				                   ((Model)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((Model)obj).MaintainDate)
							   };
            // End
		}

		private string[] GetColumnHeaderText()
		{
            /*
			return new string[] {	"ModelCode",
									"ModelDescription",
									"IsInventory",
									"IsReflow",
									"IsCheckDataLink",
									"DataLinkQty",
									"IsDim",
									"DimQty",
                                    "OrganizationID",
                                    "MaintainUser",
			                        "MaintainDate"
									};
            */
            /*
            Modified By Hi1/Venus.Feng on 20080707 for Hisense:mark these columns             
            */
            return new string[] {	"ModelCode",
									"ModelDescription",
									"IsInventory",
                                    "MaintainUser",
			                        "MaintainDate"
									};
            // End
		}

		private object[] LoadDataSource()
		{
			return this.LoadDataSource(1,int.MaxValue);
		}

        private void BuildOrgList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListOrg);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllOrg);
            builder.Build("OrganizationDescription", "OrganizationID");
            this.DropDownListOrg.Items.Insert(0, new ListItem("", ""));

            this.DropDownListOrg.SelectedIndex = 0;
        }

        private object[] GetAllOrg()
        {
            BaseModelFacade facadeBaseModel = new BaseModelFacade(base.DataProvider);
            return facadeBaseModel.GetCurrentOrgList();
        }

		#endregion

		
	}
}
