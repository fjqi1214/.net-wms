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
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Domain.TSModel;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.TSModel
{
    public partial class FErrorCodeGroupMP : BaseMPageNew
    {
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
		
        protected BenQGuru.eMES.TSModel.TSModelFacade _facade ;//= TSModelFacadeFactory.CreateTSModelFacade();
	
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
            this.gridHelper.AddColumn( "ErrorCodeGroupA", "不良代码组",	null);
            this.gridHelper.AddColumn( "ErrorCodeGroupDescription", "不良代码组描述",	null);            
            this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);
            this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
            this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
            this.gridHelper.AddLinkColumn( "SelectErrorCode","不良代码列表",null);

			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("SelectErrorCode").Width = 100;
            this.gridHelper.AddDefaultColumn( true, true );
            
            this.gridHelper.ApplyLanguage( this.languageComponent1 );
        }
		
        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ErrorCodeGroupA"] = ((ErrorCodeGroupA)obj).ErrorCodeGroup.ToString();
            row["ErrorCodeGroupDescription"] = ((ErrorCodeGroupA)obj).ErrorCodeGroupDescription.ToString();
            row["MaintainUser"] = ((ErrorCodeGroupA)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString((obj as ErrorCodeGroupA).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString((obj as ErrorCodeGroupA).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            return this._facade.QueryErrorCodeGroup( 
                FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtErrorCodeGroupQuery.Text)),
                inclusive, exclusive );
        }


        protected override int GetRowCount()
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            return this._facade.QueryErrorCodeGroupCount( 
                FormatHelper.CleanString(this.txtErrorCodeGroupQuery.Text));
        }

        #endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.AddErrorCodeGroup( (ErrorCodeGroupA)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.DeleteErrorCodeGroup( (ErrorCodeGroupA[])domainObjects.ToArray( typeof(ErrorCodeGroupA) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.UpdateErrorCodeGroup( (ErrorCodeGroupA)domainObject );

		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtErrorCodeGroupEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtErrorCodeGroupEdit.ReadOnly = true;
			}
		}

		#endregion        

        #region Object <--> Page

        protected override object GetEditObject()
        {
//            this.ValidateInput();
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            ErrorCodeGroupA ErrorCodeGroupA = this._facade.CreateNewErrorCodeGroup();

            ErrorCodeGroupA.ErrorCodeGroup = FormatHelper.CleanString(FormatHelper.PKCapitalFormat(this.txtErrorCodeGroupEdit.Text), 40);
            ErrorCodeGroupA.ErrorCodeGroupDescription = FormatHelper.CleanString(this.txtErrorCodeGroupDescriptionEdit.Text, 100);
            ErrorCodeGroupA.MaintainUser = this.GetUserCode();

            return ErrorCodeGroupA;
        }


        protected override object GetEditObject(GridRecord row)
        {	
            if (_facade == null)
            {
                _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("ErrorCodeGroupA").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetErrorCodeGroup(strCode);
            if (obj != null)
            {
                return (ErrorCodeGroupA)obj;
            }
            return null;

        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtErrorCodeGroupEdit.Text	= "";
                this.txtErrorCodeGroupDescriptionEdit.Text	= "";

                return;
            }

            this.txtErrorCodeGroupEdit.Text	= ((ErrorCodeGroupA)obj).ErrorCodeGroup.ToString();
            this.txtErrorCodeGroupDescriptionEdit.Text	= ((ErrorCodeGroupA)obj).ErrorCodeGroupDescription.ToString();
        }

		
        protected override bool ValidateInput()
        {
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblErrorCodeGroupEdit, this.txtErrorCodeGroupEdit, 40, true) );			
			manager.Add( new LengthCheck(this.lblErrorCodeGroupDescriptionEdit, this.txtErrorCodeGroupDescriptionEdit, 100, false) );			

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
				return false;
			}

			return true;
        }

        #endregion

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "SelectErrorCode")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FErrorCodeGroup2ErrorCodeSP.aspx", new string[] { "ErrorCodeGroup" }, new string[] { row.Items.FindItemByKey("ErrorCodeGroupA").Value.ToString() }));
            }
        }

        #region Export 	
        protected override string[] FormatExportRecord( object obj )
        {
            return new string[]{
                                   ((ErrorCodeGroupA)obj).ErrorCodeGroup.ToString(),
                                   ((ErrorCodeGroupA)obj).ErrorCodeGroupDescription.ToString(),
                                   ((ErrorCodeGroupA)obj).GetDisplayText("MaintainUser"),
                                   FormatHelper.ToDateString(((ErrorCodeGroupA)obj).MaintainDate)
                                   
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"不良代码组",
                                    "不良代码组描述",
                                    "维护用户",
                                    "维护日期"};
        }
        #endregion
    }


}