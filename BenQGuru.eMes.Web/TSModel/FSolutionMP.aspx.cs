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
	/// FSolutionMP 的摘要说明。
	/// </summary>
	public partial class FSolutionMP : BaseMPageNew
	{		
		private System.ComponentModel.IContainer components;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

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
            this.gridHelper.AddColumn( "SolutionCode", "解决方案代码",	null);
            this.gridHelper.AddColumn( "SolutionDescription", "解决方案描述",	null);
            this.gridHelper.AddColumn( "SolutionImprove", "解决方案改善对策",	null);
            this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);
            this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
            this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

            this.gridHelper.AddDefaultColumn( true, true );
            
			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
			this.gridWebGrid.Columns.FromKey("SolutionImprove").Hidden = true;
            this.gridHelper.ApplyLanguage( this.languageComponent1 );
        }
		
        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["SolutionCode"] = ((Solution)obj).SolutionCode.ToString();
            row["SolutionDescription"] = ((Solution)obj).SolutionDescription.ToString();
            row["SolutionImprove"] = ((Solution)obj).SolutionImprove.ToString();
            row["MaintainUser"] = ((Solution)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString((obj as Solution).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString((obj as Solution).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            return this._facade.QuerySolution( 
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSolutionCodeQuery.Text)),
                inclusive, exclusive );
        }


        protected override int GetRowCount()
        {
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            return this._facade.QuerySolutionCount( 
                FormatHelper.CleanString(this.txtSolutionCodeQuery.Text));
        }

        #endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.AddSolution( (Solution)domainObject );

		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.DeleteSolution( (Solution[])domainObjects.ToArray( typeof(Solution) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			this._facade.UpdateSolution( (Solution)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtSolutionCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtSolutionCodeEdit.ReadOnly = true;
			}
		}
		#endregion
      

        #region Object <--> Page

        protected override object GetEditObject()
        {
//            this.ValidateInput();
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
            Solution solution = this._facade.CreateNewSolution();

            solution.SolutionCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSolutionCodeEdit.Text, 40));
            solution.SolutionDescription = FormatHelper.CleanString(this.txtSolutionDescriptionEdit.Text, 100);
            solution.SolutionImprove = FormatHelper.CleanString(this.txtSolutionImproveEdit.Text, 100);
            solution.MaintainUser = this.GetUserCode();

            return solution;
        }


        protected override object GetEditObject(GridRecord row)
        {	
            if (_facade == null)
            {
                _facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("SolutionCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetSolution(strCode);
            if (obj != null)
            {
                return (Solution)obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtSolutionCodeEdit.Text	= "";
                this.txtSolutionDescriptionEdit.Text	= "";
                this.txtSolutionImproveEdit.Text	= "";

                return;
            }

            this.txtSolutionCodeEdit.Text	= ((Solution)obj).SolutionCode.ToString();
            this.txtSolutionDescriptionEdit.Text	= ((Solution)obj).SolutionDescription.ToString();
            this.txtSolutionImproveEdit.Text	= ((Solution)obj).SolutionImprove.ToString();
        }

		
        protected override bool ValidateInput()
        {
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblSolutionCodeEdit, this.txtSolutionCodeEdit, 40, true) );			
			manager.Add( new LengthCheck(this.lblSolutionDescriptionEdit, this.txtSolutionDescriptionEdit, 100, false) );			
			manager.Add( new LengthCheck(this.lblSolutionImproveEdit, this.txtSolutionImproveEdit, 100, false) );			

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
            return  new string[]{
                                    ((Solution)obj).SolutionCode.ToString(),
                                    ((Solution)obj).SolutionDescription.ToString(),
                                    ((Solution)obj).GetDisplayText("MaintainUser"),
                                    FormatHelper.ToDateString(((Solution)obj).MaintainDate)
                                }
                ;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"解决方案代码",
                                    "解决方案描述",
                                    "维护人员",
                                    "维护日期" };
        }
        #endregion

	}
}
