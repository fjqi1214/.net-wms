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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Domain.Rework;


namespace BenQGuru.eMES.Web.Rework

{
	/// <summary>
	/// FReworkCauseMP 的摘要说明。
	/// </summary>
    public partial class FReworkCauseMP : BenQGuru.eMES.Web.Helper.BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.Rework.ReworkFacade _facade ;//= ReworkFacadeFactory.Create();



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
            // TODO: 调整列的顺序及标题

            this.gridHelper.AddColumn( "ReworkCauseCode", "返工原因代码",	null);
            this.gridHelper.AddColumn( "ReworkCauseDescription", "返工原因描述",	null);
            this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);
            this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
            this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

            this.gridHelper.AddDefaultColumn( true, true );
            
            this.gridHelper.ApplyLanguage( this.languageComponent1 );
			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
        }
		
        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
                new object[]{"false",
                                ((ReworkCause)obj).ReworkCauseCode.ToString(),
                                ((ReworkCause)obj).Description.ToString(),
                                //((ReworkCause)obj).MaintainUser.ToString(),

                                 ((ReworkCause)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((ReworkCause)obj).MaintainDate),
                                FormatHelper.ToTimeString(((ReworkCause)obj).MaintainTime),
                                ""});
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            return this._facade.QueryReworkCause( 
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtCauseCodeQuery.Text) ),
                inclusive, exclusive );
        }


        protected override int GetRowCount()
        {
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            return this._facade.QueryReworkCauseCount( 
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString( this.txtCauseCodeQuery.Text)));
        }

        #endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
			this._facade.AddReworkCause( (ReworkCause)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
			this._facade.DeleteReworkCause( (ReworkCause[])domainObjects.ToArray( typeof(ReworkCause) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
			this._facade.UpdateReworkCause( (ReworkCause)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtCauseCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtCauseCodeEdit.ReadOnly = true;
			}
		}

		#endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            ReworkCause reworkCause = this._facade.CreateNewReworkCause();

            reworkCause.ReworkCauseCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtCauseCodeEdit.Text, 40) );
            reworkCause.Description = FormatHelper.CleanString(this.txtDescriptionEdit.Text, 40);
            reworkCause.MaintainUser = this.GetUserCode();

            return reworkCause;
        }


        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {	
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            // TODO: 用主键列的Index的替换keyIndex
            object obj = _facade.GetReworkCause( row.Cells[1].Text.ToString() );
			
            if (obj != null)
            {
                return (ReworkCause)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            // TODO: 如果不使用TextBox则需修改

            if (obj == null)
            {
                this.txtCauseCodeEdit.Text	= "";
                this.txtDescriptionEdit.Text	= "";

                return;
            }

            this.txtCauseCodeEdit.Text	= ((ReworkCause)obj).ReworkCauseCode.ToString();
            this.txtDescriptionEdit.Text	= ((ReworkCause)obj).Description.ToString();
        }

		
        protected override bool ValidateInput()
        {


            PageCheckManager manager = new PageCheckManager();

            manager.Add( new LengthCheck(lblCauseCodeEdit, txtCauseCodeEdit, 40, true) );
            manager.Add( new LengthCheck(lblCauseDescriptionEdit,txtDescriptionEdit,100,false)) ;

            if ( !manager.Check() )
            {
                WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
                return false;
            }

            return true ;


        }

        #endregion


        #region Export 	
        protected override string[] FormatExportRecord( object obj )
        {
            return new string[]{
                                ((ReworkCause)obj).ReworkCauseCode.ToString(),
                                ((ReworkCause)obj).Description.ToString(),
                                //((ReworkCause)obj).MaintainUser.ToString(),
                               ((ReworkCause)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((ReworkCause)obj).MaintainDate)};        
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"ReworkCauseCode",
                                    "ReworkCauseDescription",
                                    "MaintainUser",
                                    "MaintainDate" };
        }
        #endregion
	}
}
