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
	public partial class FStepSeq2WarehouseSP : BaseMPage
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdQuery;
		protected System.Web.UI.WebControls.Label lblShiftTitle;
		protected System.Web.UI.WebControls.TextBox txtShiftCodeQuery;
		protected System.Web.UI.WebControls.TextBox txtShiftCodeEdit;
		protected System.Web.UI.WebControls.Label lblShiftTypeCodeEdit;
		protected System.Web.UI.WebControls.DropDownList drpShiftTypeCodeEdit;
		protected System.Web.UI.WebControls.CheckBox chbIsOverDateEdit;
		protected System.Web.UI.WebControls.Label lblShiftCodeQuery;
		protected System.Web.UI.WebControls.Label lblShiftTypeCodeQuery;
		protected System.Web.UI.WebControls.TextBox txtShiftTypeCodeQuery;
		protected System.Web.UI.WebControls.Label lblShiftSequenceEdit;
		protected System.Web.UI.WebControls.TextBox txtShiftSequenceEdit;
		protected System.Web.UI.WebControls.Label lblShiftDescriptionEdit;
		protected System.Web.UI.WebControls.TextBox txtShiftDescriptionEdit;
		protected System.Web.UI.WebControls.Label lblShiftBeginTimeEdit;
		protected System.Web.UI.WebControls.Label lblShiftEndTimeEdit;
		protected BenQGuru.eMES.Web.UserControl.eMESTime timeShiftBeginTimeEdit;
		protected BenQGuru.eMES.Web.UserControl.eMESTime timeShiftEndTimeEdit;

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected BenQGuru.eMES.Web.Helper.PagerToolBar pagerToolBar;
		protected BenQGuru.eMES.Web.Helper.PagerSizeSelector pagerSizeSelector;
		
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

				//this.txtSegmentCodeQuery.Text = this.GetRequestParam("segcode");
				this.txtStepSequenceCodeQuery.Text = this.GetRequestParam("sscode");
				System.Collections.Specialized.NameValueCollection list = this.Request.QueryString;
				if (/* this.txtSegmentCodeQuery.Text == "" || */ this.txtStepSequenceCodeQuery.Text == "")
				{
					BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType(), "$Error_StepSeq2Warehouse_NoParam");
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
			this.gridHelper.AddColumn( "FactoryCode", "工厂代码",	null);
			//this.gridHelper.AddColumn( "SegmentCode", "工段代码",	null);
			this.gridHelper.AddColumn( "WarehouseCode", "仓库代码",	null);

			this.gridHelper.AddDefaultColumn(true, false);
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.cmdQuery_Click(null, null);
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			Warehouse2StepSequence w2ss = (Warehouse2StepSequence)obj;
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								w2ss.FactoryCode,
								//w2ss.SegmentCode,
								w2ss.WarehouseCode,
								""});
			w2ss = null;
			return row;
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouse2StepSequenceBySS(this.GetRequestParam("sscode"), inclusive, exclusive);
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouse2StepSequenceBySSCount(this.GetRequestParam("sscode"));
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.AddWarehouse2StepSequence( (Warehouse2StepSequence)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.DeleteWarehouse2StepSequence( (Warehouse2StepSequence[])domainObjects.ToArray( typeof(Warehouse2StepSequence) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.UpdateWarehouse2StepSequence( (Warehouse2StepSequence)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.drpFactoryCodeEdit.Enabled = true;
			}
			
			if ( pageAction == PageActionType.Update )
			{
				this.drpFactoryCodeEdit.Enabled = false;
			}
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("../BASESETTING/FSTEPSEQUENCEMP.ASPX"));
		}
		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			Warehouse2StepSequence w2ss = this._facade.CreateNewWarehouse2StepSequence();

			w2ss.FactoryCode		= this.drpFactoryCodeEdit.SelectedValue;
			//w2ss.SegmentCode		= this.Request.QueryString["segcode"];
			w2ss.WarehouseCode		= this.drpWarehouseCodeEdit.SelectedValue;
			w2ss.StepSequenceCode	= this.Request.QueryString["sscode"];

			w2ss.MaintainUser		= this.GetUserCode();

			return w2ss;
		}


		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object obj = _facade.GetWarehouse2StepSequence(this.Request.QueryString["sscode"],
															row.Cells.FromKey("WarehouseCode").Text,
															row.Cells.FromKey("FactoryCode").Text);
			
			if (obj != null)
			{
				return obj as Warehouse2StepSequence;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.drpFactoryCodeEdit.SelectedIndex = -1;
				this.drpWarehouseCodeEdit.SelectedIndex = -1;

				return;
			}

			Warehouse2StepSequence w2ss = (Warehouse2StepSequence)obj;
			try
			{
				this.drpFactoryCodeEdit.SelectedValue	= w2ss.FactoryCode;
			}
			catch
			{
				this.drpFactoryCodeEdit.SelectedIndex = 0;
			}
			try
			{
				this.drpWarehouseCodeEdit.SelectedValue	= w2ss.WarehouseCode;
			}
			catch
			{
				this.drpWarehouseCodeEdit.SelectedIndex = 0;
			}
		}
		
		protected override bool ValidateInput()
		{
			return true;
		}

		#endregion

		#region 数据初始化
		protected void drpFactoryCodeEdit_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
				DropDownListBuilder builder = new DropDownListBuilder(this.drpFactoryCodeEdit);
				builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this._facade.GetAllFactory);
				builder.Build("FactoryCode", "FactoryCode");
				BenQGuru.eMES.Common.MutiLanguage.LanguageWord lword  = languageComponent1.GetLanguage("listItemAll");
				this.drpFactoryCodeEdit.Items.Insert(0, new ListItem(lword.ControlText, ""));
				lword = null;

				this.drpFactoryCodeEdit_SelectedIndexChanged(null, null);
				builder = null;
			}	
		}

		protected void drpFactoryCodeEdit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object[] objs;
			if (this.drpFactoryCodeEdit.SelectedValue != string.Empty)
				objs = this._facade.QueryWarehouse(string.Empty, /*this.txtSegmentCodeQuery.Text,*/ this.drpFactoryCodeEdit.SelectedValue, 1, int.MaxValue);
			else
				objs = this._facade.GetAllDistinctWarehouse();
			this.drpWarehouseCodeEdit.Items.Clear();
			if (objs == null)
				return;
			foreach (object obj in objs)
			{
				this.drpWarehouseCodeEdit.Items.Add(((Warehouse)obj).WarehouseCode);
			}
			objs = null;
		}
		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			Warehouse2StepSequence w2ss = (Warehouse2StepSequence)obj;
			string[] strValue = new string[]{
									w2ss.FactoryCode,
								   //w2ss.SegmentCode,
								   w2ss.WarehouseCode
								   };
			w2ss = null;
			return strValue;
		}

		protected override string[] GetColumnHeaderText()
		{		
			// TODO: 调整字段值的顺序，使之与Grid的列对应
			return new string[] {	
									"FactoryCode",
									//"SegmentCode",
									"WarehouseCode"
									};
		}
		#endregion

	}
}
