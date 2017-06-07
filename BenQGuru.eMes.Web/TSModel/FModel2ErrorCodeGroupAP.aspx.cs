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

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.TSModel;

namespace BenQGuru.eMES.Web.TSModel
{
	/// <summary>
	/// FOperation2ResourceSP 的摘要说明。
	/// </summary>
	public partial class FModel2ErrorCodeGroupAP : BaseAPageNew
	{
	
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Label lblErrorGroupCode;
		protected System.Web.UI.WebControls.Label lblErrorCodeCodeQuery;
		protected System.Web.UI.WebControls.TextBox txtErrorCauseCodeQuery;

		private TSModelFacade _facade ;//= TSModelFacadeFactory.CreateTSModelFacade();

		#region Stable
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
			this.txtModelCodeQuery.Text = this.GetRequestParam("ModelCode");
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		#endregion

		#region Not Stable
		protected override void InitWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn( "UnAssErrorCodeGroup", "未关联不良代码组",	null);
			this.gridHelper.AddColumn( "ErrorCodeGroupDescription", "不良代码组描述",	null);
			this.gridHelper.AddColumn( "RouteCode", "途程代码",	null);
			this.gridHelper.AddColumn( "OPCode", "工序代码",	null);
			this.gridHelper.AddColumn( "ResourecCode", "资源代码",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);	
		
			this.gridWebGrid.Columns.FromKey("RouteCode").Hidden = true;
			this.gridWebGrid.Columns.FromKey("OPCode").Hidden = true;
			this.gridWebGrid.Columns.FromKey("ResourecCode").Hidden = true;

			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
			this.gridHelper.AddDefaultColumn( true, false );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			base.InitWebGrid();
		}

		protected override void AddDomainObject(ArrayList domainObject)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			_facade.AddModel2ErrorCodeGroup( (Model2ErrorCodeGroup[])domainObject.ToArray(typeof(Model2ErrorCodeGroup)));
		}

		protected override int GetRowCount()
		{			
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			return this._facade.GetUnselectedErrorCodeGroupByModelCodeCount( 
				FormatHelper.PKCapitalFormat( this.txtModelCodeQuery.Text.Trim() ), 
				FormatHelper.PKCapitalFormat(this.txtErrorCodeGroupCodeQuery.Text.Trim())
				);
		}

		protected override DataRow GetGridRow(object obj)
		{
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{"false",
            //                    ((ErrorCodeGroupA)obj).ErrorCodeGroup.ToString(),
            //                    ((ErrorCodeGroupA)obj).ErrorCodeGroupDescription.ToString(),
            //                    ((ErrorCodeGroupA)obj).RouteCode.ToString(),
            //                    ((ErrorCodeGroupA)obj).OPCode.ToString(),
            //                    ((ErrorCodeGroupA)obj).ResourceCode.ToString(),
            //                    ((ErrorCodeGroupA)obj).MaintainUser.ToString(),
            //                    FormatHelper.ToDateString(((ErrorCodeGroupA)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((ErrorCodeGroupA)obj).MaintainTime)
            //                    });
            DataRow row = this.DtSource.NewRow();
            row["UnAssErrorCodeGroup"] = ((ErrorCodeGroupA)obj).ErrorCodeGroup.ToString();
            row["ErrorCodeGroupDescription"] = ((ErrorCodeGroupA)obj).ErrorCodeGroupDescription.ToString();
            row["RouteCode"] = ((ErrorCodeGroupA)obj).RouteCode.ToString();
            row["OPCode"] = ((ErrorCodeGroupA)obj).OPCode.ToString();
            row["ResourecCode"] = ((ErrorCodeGroupA)obj).ResourceCode.ToString();
            row["MaintainUser"] = ((ErrorCodeGroupA)obj).MaintainUser.ToString();
            row["MaintainDate"] = FormatHelper.ToDateString(((ErrorCodeGroupA)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((ErrorCodeGroupA)obj).MaintainTime);
            return row;
		}

		protected override object GetEditObject(GridRecord row)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			Model2ErrorCodeGroup relation = _facade.CreateNewModel2ErrorCodeGroup();
			relation.ModelCode = this.txtModelCodeQuery.Text.Trim();
            relation.ErrorCodeGroup = row.Items.FindItemByKey("UnAssErrorCodeGroup").Text;
            relation.RouteCode = row.Items.FindItemByKey("RouteCode").Text.Trim();
            relation.OPCode = row.Items.FindItemByKey("OPCode").Text.Trim();
			relation.MaintainUser = this.GetUserCode();

			return relation;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{			
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			return this._facade.GetUnselectedErrorCodeGroupByModelCode( 
				FormatHelper.PKCapitalFormat( this.txtModelCodeQuery.Text.Trim() ), 
				FormatHelper.PKCapitalFormat(this.txtErrorCodeGroupCodeQuery.Text.Trim()),
				inclusive,exclusive);
		}

        //protected void cmdClose_ServerClick(object sender, System.EventArgs e)
        //{
        //    this.Response.Redirect(this.MakeRedirectUrl("./FModel2ErrorCodeGroupSP.aspx", new string[]{"modelcode"}, new string[]{this.txtModelCodeQuery.Text.Trim()}));
        //}

		#endregion

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
		
	}
}
