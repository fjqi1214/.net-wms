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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.UserControl;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FSegmentMP 的摘要说明。
	/// </summary>
    public partial class FSegmentMP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
	
		private BenQGuru.eMES.BaseSetting.BaseModelFacade _facade = null ;//new BaseModelFacadeFactory().Create();

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
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\BenQGuru.eMES.Web\\";
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

                BuildOrgList();
                this.BuildFactoryList();
			}
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		#endregion

		#region Format Data
		protected override void InitWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn( "SegmentCode",		"工段代码",	null);
			this.gridHelper.AddColumn( "SegmentDescription", "工段描述",	null);
			this.gridHelper.AddColumn( "ShiftTypeCode", "所用班制",	null);
            this.gridHelper.AddColumn( "FactoryCode", "工厂", null);
			this.gridHelper.AddColumn( "MaintainUser",		"维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate",		"维护日期",	null);

			this.gridHelper.AddColumn( "MaintainTime",		"维护时间",	null);
			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

        protected override DataRow GetGridRow(object obj)
		{
            DataRow row = this.DtSource.NewRow();
            row["SegmentCode"] = ((Segment)obj).SegmentCode.ToString();
            row["SegmentDescription"] = ((Segment)obj).SegmentDescription.ToString();
            row["ShiftTypeCode"] = ((Segment)obj).GetDisplayText("ShiftTypeCode");
            row["FactoryCode"] = ((Segment)obj).GetDisplayText("FactoryCode");
            row["MaintainUser"] = ((Segment)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Segment)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Segment)obj).MaintainTime);
            return row;
		}
		
		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{  ((Segment)obj).SegmentCode.ToString(),
								   ((Segment)obj).SegmentDescription.ToString(),
								   ((Segment)obj).GetDisplayText("ShiftTypeCode"),
                                   ((Segment)obj).GetDisplayText("FactoryCode"), 
                                   ((Segment)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((Segment)obj).MaintainDate) };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"SegmentCode",
									"SegmentDescription",	
									"ShiftTypeCode",
                                    "FactoryCode",
									"MaintainUser",	
									"MaintainDate" };
		}
		#endregion

		#region CRUD
		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			}
			return this._facade.QuerySegment( 
									FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSegmentCodeQuery.Text)),
									inclusive, exclusive );
		}	
		
		protected override int GetRowCount()
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			}
			return this._facade.QuerySegmentCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSegmentCodeQuery.Text)) );
		}

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			}
			this._facade.AddSegment( (Segment)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			}
			this._facade.DeleteSegment( (Segment[])domainObjects.ToArray( typeof(Segment) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			} 
			this._facade.UpdateSegment((Segment)domainObject);
            Segment newSegment = (Segment)domainObject;
            object[] segquenceObj = this._facade.GetStepSequenceBySegmentCode(newSegment.SegmentCode);

            if (segquenceObj != null)
            {
                for (int i = 0; i < segquenceObj.Length; i++)
                {
                    StepSequence newStepSequence = (StepSequence)segquenceObj[i];
                    if (newStepSequence.ShiftTypeCode == "")
                    {
                        newStepSequence.ShiftTypeCode = newSegment.ShiftTypeCode;
                        this._facade.UpdateStepSequence(newStepSequence);
                    }
                }
            }
           
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtSegmentCodeEdit.ReadOnly = false;
                this.DropDownListOrg.Enabled = true;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtSegmentCodeEdit.ReadOnly = true;
                this.DropDownListOrg.Enabled = false;
			}
		}
		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			} 
			Segment segment = this._facade.CreateNewSegment();

			segment.SegmentDescription	= FormatHelper.CleanString(this.txtSegmentDescriptionEdit.Text, 100);
			segment.SegmentCode			= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSegmentCodeEdit.Text, 40));
			segment.ShiftTypeCode		= this.drpShiftTypeCodeEdit.SelectedValue;
			segment.MaintainUser		= this.GetUserCode();
            segment.OrganizationID      = int.Parse(this.DropDownListOrg.SelectedValue);
            segment.FactoryCode         = this.DropDownListFactoryEdit.SelectedValue;
			return segment;
		}

        protected override object GetEditObject(GridRecord row)
		{
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("SegmentCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetSegment(strCode);
            if (obj != null)
            {
                return (Segment)obj;
            }
            return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtSegmentDescriptionEdit.Text	= "";
				this.txtSegmentCodeEdit.Text		= "";
				this.drpShiftTypeCodeEdit.SelectedIndex	= 0;
                this.DropDownListOrg.SelectedIndex = 0;
                this.DropDownListFactoryEdit.SelectedIndex = 0;
				return;
			}

			this.txtSegmentDescriptionEdit.Text	= ((Segment)obj).SegmentDescription ;
			this.txtSegmentCodeEdit.Text		= ((Segment)obj).SegmentCode ;
			
			try
			{
				this.drpShiftTypeCodeEdit.SelectedValue 	= ((Segment)obj).ShiftTypeCode.ToString();
			}
			catch
			{
				this.drpShiftTypeCodeEdit.SelectedIndex	= 0;
			}

            try
            {
                this.DropDownListOrg.SelectedValue = ((Segment)obj).OrganizationID.ToString();
            }
            catch
            {
                this.DropDownListOrg.SelectedIndex = 0;
            }
            try
            {
                this.DropDownListFactoryEdit.SelectedValue = ((Segment)obj).FactoryCode.ToString();
            }
            catch
            {
                this.DropDownListFactoryEdit.SelectedIndex = 0;
            }
		}
		
		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
			
			manager.Add( new LengthCheck(lblSegmentCodeEdit, txtSegmentCodeEdit, 40, true) );
			manager.Add( new LengthCheck(lblShiftTypeCodeEdit, drpShiftTypeCodeEdit, 40, true) );
			manager.Add( new LengthCheck(lblSegmentDescriptionEdit, txtSegmentDescriptionEdit, 100, false) );
            manager.Add(new LengthCheck(lblOrgEdit, DropDownListOrg, 8, true));
            manager.Add(new LengthCheck(lblFactoryCodeEdit, DropDownListFactoryEdit, 40, true));

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);

				return false;
			}

			return true;
		}

		#endregion

		#region 数据初始化

		protected void drpShiftTypeCodeEdit_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				DropDownListBuilder builder = new DropDownListBuilder(this.drpShiftTypeCodeEdit);
				if(_facade==null)
				{
					_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
				} 
				builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate( new ShiftModelFacadeFactory(base.DataProvider).Create().GetAllShiftType);

                builder.Build("ShiftTypeDescription", "ShiftTypeCode");
                this.drpShiftTypeCodeEdit.Items.Insert(0, new ListItem("", ""));
			}
		}

        private void BuildFactoryList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListFactoryEdit);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllFactory);
            builder.Build("FactoryDescription", "FactoryCode");
            this.DropDownListFactoryEdit.Items.Insert(0, new ListItem("", ""));

            this.DropDownListFactoryEdit.SelectedIndex = 0;
        }

        private object[] GetAllFactory()
        {
            Material.WarehouseFacade wareHouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(this.DataProvider);
            return wareHouseFacade.GetAllFactory();
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
