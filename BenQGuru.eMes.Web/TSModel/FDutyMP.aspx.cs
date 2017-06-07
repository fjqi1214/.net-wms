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
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.TSModel
{
	/// <summary>
	/// FDutyMP 的摘要说明。
	/// </summary>
	public partial class FDutyMP : BaseMPageNew
	{


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

        private System.ComponentModel.IContainer components;
		
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		
        protected BenQGuru.eMES.TSModel.TSModelFacade _facade;// = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();
		
        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
		{
			if( !this.IsPostBack )
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
            this.gridHelper.AddColumn( "DutyCode", "责任别代码",	null);
            this.gridHelper.AddColumn( "DutyDescription", "责任别描述",	null);
            this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);
            this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
            this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

            this.gridHelper.AddDefaultColumn( true, true );
			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            
            this.gridHelper.ApplyLanguage( this.languageComponent1 );
        }
		
        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["DutyCode"] = ((Duty)obj).DutyCode.ToString();
            row["DutyDescription"] = ((Duty)obj).DutyDescription.ToString();
            row["MaintainUser"] = ((Duty)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Duty)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Duty)obj).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            return this._facade.QueryDuty( 
                FormatHelper.CleanString( FormatHelper.PKCapitalFormat( this.txtDutyCodeQuery.Text ) ),
                inclusive, exclusive );
        }


        protected override int GetRowCount()
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            return this._facade.QueryDutyCount( 
                FormatHelper.CleanString( FormatHelper.PKCapitalFormat( this.txtDutyCodeQuery.Text ) )
				);
        }

        #endregion

        #region Button

		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.AddDuty( (Duty)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			
			Duty[] dutys = (Duty[])domainObjects.ToArray( typeof(Duty) );
			if(_facade.CheckDutyCodeIsUsed(dutys))
			{
				BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType() , "$Error_DutyCode_Has_Used" ) ;
			}
			this._facade.DeleteDuty( dutys );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.UpdateDuty( (Duty)domainObject );
		}

        protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
        {
            if ( pageAction == PageActionType.Add )
            {
                this.txtDutyCodeEdit.ReadOnly = false;
            }

            if ( pageAction == PageActionType.Update )
            {
                this.txtDutyCodeEdit.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
//            this.ValidateInput();
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            Duty duty = this._facade.CreateNewDuty();

            duty.DutyCode = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtDutyCodeEdit.Text), 40);
            duty.DutyDescription = FormatHelper.CleanString(this.txtDutyDescriptionEdit.Text, 100);
            duty.MaintainUser = this.GetUserCode();

            return duty;
        }


        protected override object GetEditObject(GridRecord row)
        {	
            if (_facade == null)
            {
                _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("DutyCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetDuty(strCode);
            if (obj != null)
            {
                return (Duty)obj;
            }
            return null;

        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtDutyCodeEdit.Text	= "";
                this.txtDutyDescriptionEdit.Text	= "";

                return;
            }

            this.txtDutyCodeEdit.Text	= ((Duty)obj).DutyCode.ToString();
            this.txtDutyDescriptionEdit.Text	= ((Duty)obj).DutyDescription.ToString();
        }

		
        protected override bool ValidateInput()
        {
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblDutyCodeEdit, this.txtDutyCodeEdit, 40, true) );			
			manager.Add( new LengthCheck(this.lblDutyDescriptionEdit, this.txtDutyDescriptionEdit, 100, false) );			

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
				return false;
			}

			return true;
        }

        #endregion

        #region Export 		
       
        protected override string[] FormatExportRecord( object obj )
        {
            return new string[]{
                                   ((Duty)obj).DutyCode.ToString(),
                                   ((Duty)obj).DutyDescription.ToString(),
                                   ((Duty)obj).GetDisplayText("MaintainUser"),
                                   FormatHelper.ToDateString(((Duty)obj).MaintainDate)
                               }
                ;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"DutyCode",
                                    "DutyDescription",
                                    "MaintainUser",
                                    "MaintainDate" };
        }
        #endregion

	}
}
