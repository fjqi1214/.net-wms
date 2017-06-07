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
    public partial class FEquimentTypeMP : BaseMPageNew
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
			this.gridHelper.AddColumn( "EQPType", "设备类型",	null);
            this.gridHelper.AddColumn("EQPTypeDesc", "设备类型描述", null);
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
            //                    ((Domain.Equipment.EquipmentType)obj).Eqptype.ToString(),
            //                    ((Domain.Equipment.EquipmentType)obj).Eqptypedesc.ToString(),
            //                    ((Domain.Equipment.EquipmentType)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((Domain.Equipment.EquipmentType)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((Domain.Equipment.EquipmentType)obj).MaintainTime)
            //                    });
            DataRow row = this.DtSource.NewRow();
            row["EQPType"] = ((Domain.Equipment.EquipmentType)obj).Eqptype.ToString();
            row["EQPTypeDesc"] = ((Domain.Equipment.EquipmentType)obj).Eqptypedesc.ToString();
            row["MaintainUser"] = ((Domain.Equipment.EquipmentType)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Domain.Equipment.EquipmentType)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Domain.Equipment.EquipmentType)obj).MaintainTime);
            return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
			return this._facade.QueryEquipmenType( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPTypeQuery.Text)),
                FormatHelper.CleanString(this.txtEQPTypeDescQuery.Text),
				inclusive, exclusive );
		}


		protected override int GetRowCount()
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
			return this._facade.QueryEquipmenTypeCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPTypeQuery.Text)),
                FormatHelper.CleanString(this.txtEQPTypeDescQuery.Text));
		}

		#endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
            this._facade.AddEquipmentType((Domain.Equipment.EquipmentType)domainObject);
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
            foreach (Domain.Equipment.EquipmentType epqType in domainObjects)
            {
                int count = _facade.QueryEquipmentCountByType(epqType.Eqptype);
                if (count > 0)
                {
                    WebInfoPublish.PublishInfo(this, "$EQPTYPE_CONNOT_DELETE $EQPTYPE:"+epqType.Eqptype, this.languageComponent1);
                    return;
                }               
            }

            this._facade.DeleteEquipmentType((Domain.Equipment.EquipmentType[])domainObjects.ToArray(typeof(Domain.Equipment.EquipmentType)));
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
            this._facade.UpdateEquipmentType((Domain.Equipment.EquipmentType)domainObject);
		}


		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtEQPTypeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
                this.txtEQPTypeEdit.ReadOnly = true;
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
            Domain.Equipment.EquipmentType equipmentType = this._facade.CreateEquipmentType();
            equipmentType.Eqptype = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPTypeEdit.Text, 40));
            equipmentType.Eqptypedesc = FormatHelper.CleanString(this.txtEQPTypeDescEdit.Text, 100);            
            equipmentType.MaintainUser = this.GetUserCode();

            return equipmentType;
		}


		protected override object GetEditObject(GridRecord row)
		{
			if(_facade == null)
			{
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(this.DataProvider);
			}
            object obj = _facade.GetEquipmentType(row.Items.FindItemByKey("EQPType").Text.ToString());
			
			if (obj != null)
			{
                return (Domain.Equipment.EquipmentType)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtEQPTypeDescEdit.Text	= "";
				this.txtEQPTypeEdit.Text	= "";
				return;
			}

            this.txtEQPTypeDescEdit.Text = ((Domain.Equipment.EquipmentType)obj).Eqptypedesc.ToString();
            this.txtEQPTypeEdit.Text = ((Domain.Equipment.EquipmentType)obj).Eqptype.ToString();

		}

		
		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblEQPTypeEdit, this.txtEQPTypeEdit, 40, true) );				
			manager.Add( new LengthCheck(this.lblEQPTypeDescEdit, this.txtEQPTypeDescEdit, 100, false) );

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
            return new string[]{  ((Domain.Equipment.EquipmentType)obj).Eqptype.ToString(),
								   ((Domain.Equipment.EquipmentType)obj).Eqptypedesc.ToString(),								  
								   ((Domain.Equipment.EquipmentType)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((Domain.Equipment.EquipmentType)obj).MaintainDate), 
                                   FormatHelper.ToTimeString(((Domain.Equipment.EquipmentType)obj).MaintainTime)
                                };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"EQPType",
									"EQPTypeDesc",
									"MaintainUser",
									"MaintainDate","MaintainTime"};
		}

		#endregion
	}
}
