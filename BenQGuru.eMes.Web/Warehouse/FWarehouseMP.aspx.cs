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

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
	/// <summary>
	/// FFactory 的摘要说明。
	/// </summary>
	public partial class FWarehouseMP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
	
		private ButtonHelper buttonHelper = null;
		private WarehouseFacade _facade ;//= new WarehouseFacade();


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

				InitDropDownList();
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
			this.gridHelper.AddColumn( "FactoryCode", "工厂代码",	null);
			//this.gridHelper.AddColumn( "SegmentCode", "工段代码",	null);
			this.gridHelper.AddColumn( "WarehouseCode", "仓库名称",	null);
			this.gridHelper.AddColumn( "WarehouseDescription", "仓库描述",	null);
			this.gridHelper.AddColumn( "WarehouseTypeLabel", "仓库类型",	null);
			this.gridHelper.AddColumn( "WarehouseStatusLabel", "仓库状态",	null);
			this.gridHelper.AddColumn( "FactoryCode", "工厂代码",	null);
			this.gridHelper.AddColumn( "WarehouseType", "仓库类型",	null);
			this.gridWebGrid.Columns.Add( new UltraGridColumn("WarehouseIsControl", "是否管控", ColumnType.CheckBox, null));
			this.gridHelper.AddColumn( "WarehouseStatus", "仓库状态",	null);

			this.gridWebGrid.Columns.FromKey("WarehouseType").Hidden = true;
			this.gridWebGrid.Columns.FromKey("WarehouseStatus").Hidden = true;

			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			Warehouse warehouse = (Warehouse)obj;
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = 
				new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
				new object[]{"false",
								warehouse.FactoryCode.ToString(),
								//warehouse.SegmentCode.ToString(),
								warehouse.WarehouseCode.ToString(),
								warehouse.WarehouseDescription.ToString(),
								this.drpWarehouseTypeEdit.Items.FindByValue(warehouse.WarehouseType).Text,
								this.drpWarehouseStatusEdit.Items.FindByValue(warehouse.WarehouseStatus).Text,
								warehouse.WarehouseType,
								FormatHelper.StringToBoolean(warehouse.IsControl),
								warehouse.WarehouseStatus,
								""});
			warehouse = null;
			return row;
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouse( 
				this.drpWarehouseCodeQuery.SelectedValue,this.drpFactoryCodeQuery.SelectedValue,
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouseCount(this.drpWarehouseCodeQuery.SelectedValue,this.drpFactoryCodeQuery.SelectedValue);
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.AddWarehouse( (Warehouse)domainObject );
			InitWarehouseCodeQueryList();
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.DeleteWarehouse( (Warehouse[])domainObjects.ToArray( typeof(Warehouse) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.UpdateWarehouse( (Warehouse)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.drpWarehouseStatusEdit.SelectedIndex = 0;
				SetEditObjEnabled(true);
			}
			
			if ( pageAction == PageActionType.Update )
			{
				SetEditObjEnabled(false);
			}
		}

		private void SetEditObjEnabled(bool enabled)
		{
			this.drpFactoryCodeEdit.Enabled = enabled;
			//this.drpSegmentCodeEdit.Enabled = enabled;
			this.txtWarehouseCodeEdit.ReadOnly = !enabled;
			this.drpWarehouseStatusEdit.Enabled = !enabled;
		}
		#endregion

		#region 数据初始化

		private void InitDropDownList()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			BenQGuru.eMES.Common.MutiLanguage.LanguageWord lword  = languageComponent1.GetLanguage("listItemAll");
			DropDownListBuilder builder = new DropDownListBuilder(this.drpFactoryCodeQuery);
			builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllFactory);
			builder.Build("FactoryCode","FactoryCode");
			this.drpFactoryCodeQuery.Items.Insert(0, new ListItem(lword.ControlText, ""));

			BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(base.DataProvider);
//			builder = new DropDownListBuilder(this.drpSegmentCodeQuery);
//			builder.HandleGetObjectList = new GetObjectListDelegate(bmFacade.GetAllSegment);
//			builder.Build("SegmentCode","SegmentCode");
//			this.drpSegmentCodeQuery.Items.Insert(0, new ListItem(lword.ControlText, ""));

			builder = new DropDownListBuilder(this.drpFactoryCodeEdit);
			builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllFactory);
			builder.Build("FactoryCode","FactoryCode");
			this.drpFactoryCodeEdit.Items.Insert(0, new ListItem(lword.ControlText, ""));

//			builder = new DropDownListBuilder(this.drpSegmentCodeEdit);
//			builder.HandleGetObjectList = new GetObjectListDelegate(bmFacade.GetAllSegment);
//			builder.Build("SegmentCode","SegmentCode");
//			this.drpSegmentCodeEdit.Items.Insert(0, new ListItem(lword.ControlText, ""));

			builder = new DropDownListBuilder(this.drpWarehouseTypeEdit);
			builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetWarehouseTypes);
			builder.Build("ParameterAlias","ParameterCode");

			this.drpWarehouseStatusEdit.Items.Clear();
			this.drpWarehouseStatusEdit.Items.Add(new ListItem( this.languageComponent1.GetString(Warehouse.WarehouseStatus_Normal),Warehouse.WarehouseStatus_Normal));
			this.drpWarehouseStatusEdit.Items.Add(new ListItem( this.languageComponent1.GetString(Warehouse.WarehouseStatus_Cycle),Warehouse.WarehouseStatus_Cycle));
			

			InitWarehouseCodeQueryList();

			bmFacade = null;
			lword = null;
			builder = null;

			this.chbIsControl.Checked = true;
		}
		private void InitWarehouseCodeQueryList()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			BenQGuru.eMES.Common.MutiLanguage.LanguageWord lword  = languageComponent1.GetLanguage("listItemAll");
			DropDownListBuilder builder = new DropDownListBuilder(this.drpWarehouseCodeQuery);
			builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllDistinctWarehouse);
			builder.Build("WarehouseCode","WarehouseCode");
			this.drpWarehouseCodeQuery.Items.Insert(0, new ListItem(lword.ControlText, ""));
		}


		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			Warehouse warehouse = this._facade.CreateNewWarehouse();

			warehouse.FactoryCode				= this.drpFactoryCodeEdit.SelectedValue;
			//warehouse.SegmentCode				= this.drpSegmentCodeEdit.SelectedValue;
			warehouse.WarehouseCode				= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtWarehouseCodeEdit.Text, 50));
			warehouse.WarehouseDescription		= FormatHelper.CleanString(this.txtWarehouseDescEdit.Text, 100);
			warehouse.WarehouseType 			= this.drpWarehouseTypeEdit.SelectedValue;
			warehouse.WarehouseStatus			= this.drpWarehouseStatusEdit.SelectedValue;
			warehouse.IsControl					= FormatHelper.BooleanToString(this.chbIsControl.Checked);
			warehouse.MaintainUser			= this.GetUserCode();

			return warehouse;
		}

		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object obj = _facade.GetWarehouse( 
				row.Cells.FromKey("WarehouseCode").Text.ToString(), 
				row.Cells.FromKey("FactoryCode").Text.ToString());
			
			if (obj != null)
			{
				return (Warehouse)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.drpFactoryCodeEdit.SelectedIndex = -1;
				///this.drpSegmentCodeEdit.SelectedIndex = -1;
				this.txtWarehouseCodeEdit.Text = "";
				this.txtWarehouseDescEdit.Text = "";
				this.drpWarehouseTypeEdit.SelectedIndex = -1;
				this.drpWarehouseStatusEdit.SelectedIndex = -1;
				this.chbIsControl.Checked = true;

				return;
			}

			Warehouse warehouse = (Warehouse)obj;
			SetDropDownValue(this.drpFactoryCodeEdit, warehouse.FactoryCode);
			//SetDropDownValue(this.drpSegmentCodeEdit, warehouse.SegmentCode);
			this.txtWarehouseCodeEdit.Text = warehouse.WarehouseCode;
			this.txtWarehouseDescEdit.Text = warehouse.WarehouseDescription;
			SetDropDownValue(this.drpWarehouseTypeEdit, warehouse.WarehouseType);
			SetDropDownValue(this.drpWarehouseStatusEdit, warehouse.WarehouseStatus);
			this.chbIsControl.Checked = FormatHelper.StringToBoolean(warehouse.IsControl);
			warehouse = null;
		}
		private void SetDropDownValue(DropDownList drp, string value)
		{
			try
			{
				drp.SelectedValue = value;
			}
			catch
			{
				drp.SelectedIndex = -1;
			}
		}

		
		protected override bool ValidateInput()
		{			
			PageCheckManager manager = new PageCheckManager();
			
			manager.Add( new LengthCheck(lblWarehouseCodeEdit, txtWarehouseCodeEdit, 40, true) );
			manager.Add( new LengthCheck(lblWarehouseNameEdit, txtWarehouseDescEdit, 100, false) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);

				return false;
			}

			return true;
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			Warehouse warehouse = (Warehouse)obj;
			string[] strArr = 
				new string[]{	warehouse.FactoryCode,
								//warehouse.SegmentCode,
								warehouse.WarehouseCode,
								warehouse.WarehouseDescription,
								this.drpWarehouseTypeEdit.Items.FindByValue(warehouse.WarehouseType).Text,
								this.drpWarehouseStatusEdit.Items.FindByValue(warehouse.WarehouseStatus).Text
								};
			warehouse = null;
			return strArr;
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"FactoryCode",
									//"SegmentCode",
									"WarehouseCode",
									"WarehouseDescription",
									"WarehouseType",
									"WarehouseStatus"
									};
		}
		#endregion


	
	}
}
