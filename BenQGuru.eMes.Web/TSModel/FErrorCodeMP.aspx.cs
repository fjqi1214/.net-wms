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
    public partial class FErrorCodeMP : BaseMPageNew
    {		
        private System.ComponentModel.IContainer components;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		
        protected BenQGuru.eMES.TSModel.TSModelFacade _facade;// = TSModelFacadeFactory.CreateTSModelFacade();
	
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
            this.gridHelper.AddColumn( "ErrorCodeA", "不良代码",	null);
            this.gridHelper.AddColumn( "ErrorDescription", "不良描述",	null);
            this.gridHelper.AddLinkColumn("ReworkRouteCode", "返工途程", null);
            this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);
            this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
            this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ReworkRouteCode").Hidden = true;
            this.gridHelper.AddDefaultColumn( true, true );
            
            this.gridHelper.ApplyLanguage( this.languageComponent1 );
        }
		
        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ErrorCodeA"] = ((ErrorCodeA)obj).ErrorCode.ToString();
            row["ErrorDescription"] = ((ErrorCodeA)obj).ErrorDescription.ToString();
            row["ReworkRouteCode"] = "";
            row["MaintainUser"] = ((ErrorCodeA)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString((obj as ErrorCodeA).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString((obj as ErrorCodeA).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            return this._facade.QueryErrorCode( 
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeQuery.Text)),
                inclusive, exclusive );
        }


        protected override int GetRowCount()
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            return this._facade.QueryErrorCodeCount( 
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtErrorCodeQuery.Text))
                );
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "ReworkRouteCode")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FErrorCodeItem2RouteMP.aspx", new string[] { "ErrorCode" }, new string[] { row.Items.FindItemByKey("ErrorCodeA").Value.ToString() }));
            }
           
        }

        #endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.AddErrorCode( (ErrorCodeA)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.DeleteErrorCode( (ErrorCodeA[])domainObjects.ToArray( typeof(ErrorCodeA) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.UpdateErrorCode( (ErrorCodeA)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtErrorCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtErrorCodeEdit.ReadOnly = true;
			}
		}
		#endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
//            this.ValidateInput();
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            ErrorCodeA ErrorCodeA = this._facade.CreateNewErrorCodeA();

            ErrorCodeA.ErrorCode = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtErrorCodeEdit.Text), 40);
            ErrorCodeA.ErrorDescription = FormatHelper.CleanString(this.txtErrorDescriptionEdit.Text, 100);
            ErrorCodeA.MaintainUser = this.GetUserCode();

            return ErrorCodeA;
        }


        protected override object GetEditObject(GridRecord row)
        {	
            if (_facade == null)
            {
                _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("ErrorCodeA").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetErrorCode(strCode);
            if (obj != null)
            {
                return (ErrorCodeA)obj;
            }
            return null;

        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtErrorCodeEdit.Text	= "";
                this.txtErrorDescriptionEdit.Text	= "";

                return;
            }

            this.txtErrorCodeEdit.Text	= ((ErrorCodeA)obj).ErrorCode.ToString();
            this.txtErrorDescriptionEdit.Text	= ((ErrorCodeA)obj).ErrorDescription.ToString();
        }

		
        protected override bool ValidateInput()
        {
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblNGCodeEdit, this.txtErrorCodeEdit, 40, true) );			
			manager.Add( new LengthCheck(this.lblErrorDescriptionEdit, this.txtErrorDescriptionEdit, 100, false) );			

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
                                   ((ErrorCodeA)obj).ErrorCode.ToString(),
                                   ((ErrorCodeA)obj).ErrorDescription.ToString(),
                                   ((ErrorCodeA)obj).GetDisplayText("MaintainUser"),
                                   FormatHelper.ToDateString(((ErrorCodeA)obj).MaintainDate)
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"不良代码",
                                    "不良描述",
                                    "维护用户",
                                    "维护日期" };
        }
        #endregion
    }


}