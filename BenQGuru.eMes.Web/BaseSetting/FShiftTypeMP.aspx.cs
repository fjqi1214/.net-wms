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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FShiftTypeMP 的摘要说明。
	/// </summary>
	public partial class FShiftTypeMP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Label lblShiftTypeTitle;

		public BenQGuru.eMES.Web.UserControl.eMESDate dateEffectiveDateEdit;
		public BenQGuru.eMES.Web.UserControl.eMESDate dateInvalidDateEdit;

		private BenQGuru.eMES.BaseSetting.ShiftModelFacade _facade = null ;//	new ShiftModelFacadeFactory().Create();

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
			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
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
            base.InitWebGrid();
			this.gridHelper.AddColumn( "ShiftTypeCode", "班制代码",	null);
			this.gridHelper.AddColumn( "EffectiveDate", "生效日期",	null);
			this.gridHelper.AddColumn( "InvalidDate", "失效日期",	null);
			this.gridHelper.AddColumn( "ShiftTypeDescription", "班制描述",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
			this.gridWebGrid.Columns.FromKey("EffectiveDate").Hidden = true;
			this.gridWebGrid.Columns.FromKey("InvalidDate").Hidden = true;

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		
		protected override DataRow GetGridRow(object obj)
		{
            DataRow row = this.DtSource.NewRow();
            row["ShiftTypeCode"] = ((ShiftType)obj).ShiftTypeCode.ToString();
            row["EffectiveDate"] = FormatHelper.ToDateString(System.Int32.Parse(((ShiftType)obj).EffectiveDate.ToString()));
            row["InvalidDate"] = FormatHelper.ToDateString(((ShiftType)obj).InvalidDate);
            row["ShiftTypeDescription"] = ((ShiftType)obj).ShiftTypeDescription.ToString();
            row["MaintainUser"] = ((ShiftType)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((ShiftType)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((ShiftType)obj).MaintainTime);
            return row;
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade == null)
			{
				_facade = new ShiftModelFacadeFactory(base.DataProvider).Create() ;
			}
			return this._facade.QueryShiftType( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftTypeCodeQuery.Text)),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade == null)
			{
				_facade = new ShiftModelFacadeFactory(base.DataProvider).Create() ;
			}
			return this._facade.QueryShiftTypeCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftTypeCodeQuery.Text)));
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{
			if(_facade == null)
			{
				_facade = new ShiftModelFacadeFactory(base.DataProvider).Create() ;
			}
			this._facade.AddShiftType( (ShiftType)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade == null)
			{
				_facade = new ShiftModelFacadeFactory(base.DataProvider).Create() ;
			}
			this._facade.DeleteShiftType( (ShiftType[])domainObjects.ToArray( typeof(ShiftType) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade == null)
			{
				_facade = new ShiftModelFacadeFactory(base.DataProvider).Create() ;
			}
			this._facade.UpdateShiftType( (ShiftType)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtShiftTypeCodeEdit.ReadOnly = false;
			}
			
			if ( pageAction == PageActionType.Update )
			{
				this.txtShiftTypeCodeEdit.ReadOnly = true;
			}
		}
		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			if(_facade == null)
			{
				_facade = new ShiftModelFacadeFactory(base.DataProvider).Create() ;
			}
			ShiftType shiftType = this._facade.CreateNewShiftType();

			shiftType.ShiftTypeDescription	= FormatHelper.CleanString(this.txtShiftTypeDescriptionEdit.Text, 100);
			shiftType.EffectiveDate			= 0;//FormatHelper.TODateInt(this.dateEffectiveDateEdit.Text);
			shiftType.InvalidDate			= 0;//FormatHelper.TODateInt(this.dateInvalidDateEdit.Text);
			shiftType.ShiftTypeCode			= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtShiftTypeCodeEdit.Text, 40));
			shiftType.MaintainUser			= this.GetUserCode();

			return shiftType;
		}

		protected override object GetEditObject(GridRecord row)
		{
            if (_facade == null)
            {
                _facade = new ShiftModelFacadeFactory(base.DataProvider).Create();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("ShiftTypeCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetShiftType(strCode);
            if (obj != null)
            {
                return (ShiftType)obj;
            }
            return null;

		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtShiftTypeDescriptionEdit.Text	= "";
//				this.dateEffectiveDateEdit.Text	= "";
//				this.dateInvalidDateEdit.Text	= "";
				this.txtShiftTypeCodeEdit.Text	= "";

				return;
			}

			this.txtShiftTypeDescriptionEdit.Text	= ((ShiftType)obj).ShiftTypeDescription.ToString();
//			this.dateEffectiveDateEdit.Text	= FormatHelper.ToDateString(((ShiftType)obj).EffectiveDate);
//			this.dateInvalidDateEdit.Text	= FormatHelper.ToDateString(((ShiftType)obj).InvalidDate);
			this.txtShiftTypeCodeEdit.Text	= ((ShiftType)obj).ShiftTypeCode.ToString();
		}

		
		protected override bool ValidateInput()
		{			
			PageCheckManager manager = new PageCheckManager();
			
			manager.Add( new LengthCheck(lblShiftTypeCodeEdit, txtShiftTypeCodeEdit, 40, true) );
//			manager.Add( new DateRangeCheck(lblEffectiveDateEdit, this.dateEffectiveDateEdit.Text,this.dateInvalidDateEdit.Text, true) );
			manager.Add( new LengthCheck(lblShiftTypeDescriptionEdit, txtShiftTypeDescriptionEdit, 100, false) );

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
			return new string[]{  ((ShiftType)obj).ShiftTypeCode.ToString(),
								   ((ShiftType)obj).ShiftTypeDescription.ToString(),
								   ((ShiftType)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((ShiftType)obj).MaintainDate) };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"ShiftTypeCode",
									"ShiftTypeDescription",
									"MaintainUser",
									"MaintainDate"};
		}
		#endregion
	}
}
