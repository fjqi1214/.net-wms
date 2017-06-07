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
	public partial class FItemMP : BaseMPage
	{
		protected System.Web.UI.WebControls.Label lblShiftTitle;

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Label lblInitFileQuery;
		protected System.Web.UI.HtmlControls.HtmlInputFile fileInit;
		
		private BenQGuru.eMES.Material.WarehouseFacade _facade ;//= new BenQGuru.eMES.Material.WarehouseFacade();

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

				if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
				if (this._facade.QueryWarehouseItemCount(string.Empty, string.Empty) > 0)
				{
					cmdInitialize.Disabled = true;
				}
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
			this.gridHelper.AddColumn( "WarehouseItemCode", "物料代码",	null);
			this.gridHelper.AddColumn( "WarehouseItemName", "物料名称",	null);
			this.gridHelper.AddColumn( "WarehouseItemUOM", "计量单位",	null);
			this.gridHelper.AddColumn( "WarehouseItemControlType", "管制类型",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridHelper.Grid.Columns.FromKey("WarehouseItemControlType").Hidden = true ;
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			WarehouseItem item = (WarehouseItem)obj;
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								item.ItemCode,
								item.ItemName,
								GetUOM(item.ItemUOM),
								this.drpItemControlTypeEdit.Items.FindByValue(item.ItemControlType).Text,
								item.MaintainUser,
								FormatHelper.ToDateString(item.MaintainDate),
								FormatHelper.ToTimeString(item.MaintainTime),
								""});
			item = null;
			return row;
		}
		private string GetUOM(string strUOM)
		{
			if (this.drpItemUOMEdit.Items.FindByValue(strUOM) == null)
				return "";
			else
				return this.drpItemUOMEdit.Items.FindByValue(strUOM).Text;
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouseItem( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
				FormatHelper.CleanString(this.txtItemNameQuery.Text),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouseItemCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
				FormatHelper.CleanString(this.txtItemNameQuery.Text));
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.AddWarehouseItem( (WarehouseItem)domainObject );
			cmdInitialize.Disabled = true;
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.DeleteWarehouseItem( (WarehouseItem[])domainObjects.ToArray( typeof(WarehouseItem) ) );
			if (this._facade.QueryWarehouseItemCount(string.Empty, string.Empty) == 0)
			{
				cmdInitialize.Disabled = false;
			}
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.UpdateWarehouseItem( (WarehouseItem)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtItemCodeEdit.ReadOnly = false;
			}
			
			if ( pageAction == PageActionType.Update )
			{
				this.txtItemCodeEdit.ReadOnly = true;
			}
		}

		protected void cmdImport_ServerClick(object sender, System.EventArgs e)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.ImportAllItem(this.GetUserCode());
			this.cmdQuery_Click(null, null);
			cmdInitialize.Disabled = true;
		}
		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			WarehouseItem item = this._facade.CreateNewWarehouseItem();

			item.ItemCode		= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeEdit.Text, 50));
			item.ItemName		= FormatHelper.CleanString(this.txtItemNameEdit.Text, 100);
			item.ItemUOM		= this.drpItemUOMEdit.SelectedValue;
			item.ItemControlType	= this.drpItemControlTypeEdit.SelectedValue;
			item.MaintainUser		= this.GetUserCode();

			return item;
		}


		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object obj = _facade.GetWarehouseItem( row.Cells[1].Text.ToString() );
			
			if (obj != null)
			{
				return obj as WarehouseItem;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtItemCodeEdit.Text	= "";
				this.txtItemNameEdit.Text	= "";
				this.drpItemUOMEdit.SelectedIndex = -1;
				this.drpItemControlTypeEdit.SelectedIndex = 0;

				return;
			}

			WarehouseItem item = (WarehouseItem)obj;
			this.txtItemCodeEdit.Text	= item.ItemCode;
			this.txtItemNameEdit.Text	= item.ItemName;
			try
			{
				this.drpItemUOMEdit.SelectedValue	= item.ItemUOM;
			}
			catch
			{
				this.drpItemUOMEdit.SelectedIndex	= -1;
			}
			try
			{
				this.drpItemControlTypeEdit.SelectedValue	= item.ItemControlType;
			}
			catch
			{
				this.drpItemControlTypeEdit.SelectedIndex	= 0;
			}
		}
		
		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblItemIDEdit, txtItemCodeEdit, 40, true) );
			manager.Add( new LengthCheck(lblItemNEdit, txtItemNameEdit, 100, true) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}
			return true;
		}

		#endregion

		#region 数据初始化
		protected void drpItemControlTypeEdit_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				this.drpItemControlTypeEdit.Items.Clear();
				BenQGuru.eMES.Common.MutiLanguage.LanguageWord lword  = languageComponent1.GetLanguage(WarehouseItem.WarehouseItemControlType_Lot);
				this.drpItemControlTypeEdit.Items.Add(new ListItem(lword.ControlText, WarehouseItem.WarehouseItemControlType_Lot));
				lword  = languageComponent1.GetLanguage(WarehouseItem.WarehouseItemControlType_Single);
				this.drpItemControlTypeEdit.Items.Add(new ListItem(lword.ControlText, WarehouseItem.WarehouseItemControlType_Single));
				lword = null;
				this.drpItemControlTypeEdit.SelectedIndex = 0;
				
				this.drpItemUOMEdit.Items.Clear();
				DropDownListBuilder builder = new DropDownListBuilder(this.drpItemUOMEdit);
				builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetWarehouseItemUOM);
				builder.Build("ParameterAlias","ParameterCode");
			}
		}
		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			WarehouseItem item = (WarehouseItem)obj;
			string[] strValue = new string[]{  item.ItemCode,
												item.ItemName,
												GetUOM(item.ItemUOM),
												this.drpItemControlTypeEdit.Items.FindByValue(item.ItemControlType).Text,
								   
												item.MaintainUser.ToString(),
												FormatHelper.ToDateString(item.MaintainDate),
												FormatHelper.ToTimeString(item.MaintainTime)
											};
			item = null;
			return strValue;
		}

		protected override string[] GetColumnHeaderText()
		{		
			// TODO: 调整字段值的顺序，使之与Grid的列对应
			return new string[] {	
									"WarehouseItemCode",
									"WarehouseItemName",
									"WarehouseItemUOM",
									"WarehouseItemControlType",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"};
		}
		#endregion

	}
}
