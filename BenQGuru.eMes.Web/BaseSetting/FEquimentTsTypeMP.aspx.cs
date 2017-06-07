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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FRouteMP 的摘要说明。
	/// </summary>
    public partial class FEquimentTsTypeMP : BaseMPageNew
	{
		protected System.Web.UI.WebControls.Label lblRouteTitle;

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
		private BenQGuru.eMES.Material.EquipmentFacade _facade =	null ;//	new BaseModelFacadeFactory().Create();

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
			if(!IsPostBack)
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
			this.gridHelper.AddColumn( "EQPTsType", "设备类型",	null);
            this.gridHelper.AddColumn( "EQPTsTypeDesc", "设备类型描述", null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override DataRow GetGridRow(object obj)
		{
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{"false",
            //                    ((Domain.Equipment.EquipmentTsType)obj).Eqptstype.ToString(),
            //                    ((Domain.Equipment.EquipmentTsType)obj).Eqptstypedesc.ToString(),
            //                    ((Domain.Equipment.EquipmentTsType)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((Domain.Equipment.EquipmentTsType)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((Domain.Equipment.EquipmentTsType)obj).MaintainTime)
            //                    });
            DataRow row = this.DtSource.NewRow();
            row["EQPTsType"] = ((Domain.Equipment.EquipmentTsType)obj).Eqptstype.ToString();
            row["EQPTsTypeDesc"] = ((Domain.Equipment.EquipmentTsType)obj).Eqptstypedesc.ToString();
            row["MaintainUser"] = ((Domain.Equipment.EquipmentTsType)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Domain.Equipment.EquipmentTsType)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Domain.Equipment.EquipmentTsType)obj).MaintainTime);
            return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
			return this._facade.QueryEquipmenTsType( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPTsTypeQuery.Text)),
                FormatHelper.CleanString(this.txtEQPTsTypeDescQuery.Text),
				inclusive, exclusive );
		}


		protected override int GetRowCount()
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
			return this._facade.QueryEquipmenTsTypeCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPTsTypeQuery.Text)),
                FormatHelper.CleanString(this.txtEQPTsTypeDescQuery.Text));
		}

		#endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
            this._facade.AddEquipmentTsType((Domain.Equipment.EquipmentTsType)domainObject);
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}

            foreach (Domain.Equipment.EquipmentTsType epqTsType in domainObjects)
            {
                int count = _facade.GetEQPTSLog(epqTsType.Eqptstype, EquipmentTSLogStatus.EquipmentTSLogStatus_New);
                if (count > 0)
                {
                    WebInfoPublish.PublishInfo(this, "$EQPTSTYPE_CONNOT_DELETE $EQPTSTYPE:" + epqTsType.Eqptstype, this.languageComponent1);
                    return;
                }
            }


            this._facade.DeleteEquipmentTsType((Domain.Equipment.EquipmentTsType[])domainObjects.ToArray(typeof(Domain.Equipment.EquipmentTsType)));
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
            this._facade.UpdateEquipmentTsType((Domain.Equipment.EquipmentTsType)domainObject);
		}


		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtEQPTsTypeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
                this.txtEQPTsTypeEdit.ReadOnly = true;
			}
		}
		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
            Domain.Equipment.EquipmentTsType equipmentType = this._facade.CreateEquipmentTsType();
            equipmentType.Eqptstype = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPTsTypeEdit.Text, 40));
            equipmentType.Eqptstypedesc = FormatHelper.CleanString(this.txtEQPTsTypeDescEdit.Text, 100);            
            equipmentType.MaintainUser = this.GetUserCode();

            return equipmentType;
		}


		protected override object GetEditObject(GridRecord row)
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
            object obj = _facade.GetEquipmentTsType(row.Items.FindItemByKey("EQPTsType").Text.ToString());
			
			if (obj != null)
			{
                return (Domain.Equipment.EquipmentTsType)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtEQPTsTypeDescEdit.Text	= "";
				this.txtEQPTsTypeEdit.Text	= "";
				return;
			}

            this.txtEQPTsTypeDescEdit.Text = ((Domain.Equipment.EquipmentTsType)obj).Eqptstypedesc.ToString();
            this.txtEQPTsTypeEdit.Text = ((Domain.Equipment.EquipmentTsType)obj).Eqptstype.ToString();

		}

		
		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblEQPTsTypeEdit, this.txtEQPTsTypeEdit, 40, true) );				
			manager.Add( new LengthCheck(this.lblEQPTsTypeDescEdit, this.txtEQPTsTypeDescEdit, 100, false) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}

			return true;
		}

		#endregion

		#region Export
		// 2005-04-06
		protected override string[] FormatExportRecord( object obj )
		{
            return new string[]{  ((Domain.Equipment.EquipmentTsType)obj).Eqptstype.ToString(),
								   ((Domain.Equipment.EquipmentTsType)obj).Eqptstypedesc.ToString(),								  
								   ((Domain.Equipment.EquipmentTsType)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((Domain.Equipment.EquipmentTsType)obj).MaintainDate), 
                                   FormatHelper.ToTimeString(((Domain.Equipment.EquipmentTsType)obj).MaintainTime)
                                };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"EQPTsType",
									"EQPTsTypeDesc",
									"MaintainUser",
									"MaintainDate","MaintainTime"};
		}

		#endregion
	}
}
