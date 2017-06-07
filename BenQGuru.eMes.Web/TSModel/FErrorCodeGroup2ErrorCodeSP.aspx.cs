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
	public partial class FErrorCodeGroup2ErrorCodeSP : BaseMPageNew
	{
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Label lblErrorGroupCode;

		private TSModelFacade _facade ;//= TSModelFacadeFactory.CreateTSModelFacade();

		#region Stable
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			//this.pagerSizeSelector.Readonly = true;

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtErrorCodeGroupCodeQuery.Text = this.GetRequestParam("ErrorCodeGroup");
            }

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
            this.gridHelper.AddColumn("AssErrorCode", "不良代码", null);
            this.gridHelper.AddColumn("ErrorDescription", "不良代码描述", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();

        }

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			ErrorCodeGroup2ErrorCode[] ecg2ec = (ErrorCodeGroup2ErrorCode[])domainObjects.ToArray(typeof(ErrorCodeGroup2ErrorCode));

			if(_facade.CheckErrorCodeGroup2ErrorCodeIsUsed(ecg2ec))
			{
				BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType() , "$Error_ErrorCode2Group_Has_Used" ) ;
			}
			_facade.DeleteErrorCodeGroup2ErrorCode( ecg2ec );
		}

		protected override object GetEditObject(GridRecord row)
		{
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			ErrorCodeGroup2ErrorCode relation = _facade.CreateNewErrorCodeGroup2ErrorCode();
			relation.ErrorCodeGroup = this.txtErrorCodeGroupCodeQuery.Text.Trim();
            relation.ErrorCode = row.Items.FindItemByKey("AssErrorCode").Text;		
			relation.MaintainUser = this.GetUserCode();

			return relation;
		}

		protected override int GetRowCount()
		{			
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			return this._facade.GetSelectedErrorCodeByErrorCodeGroupCodeCount( 
				FormatHelper.PKCapitalFormat(this.txtErrorCodeGroupCodeQuery.Text.Trim()) ,
                FormatHelper.PKCapitalFormat(this.txtErrorCodeCodeQuery.Text.Trim()));
		}

		protected override DataRow GetGridRow(object obj)
		{
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{"false",
            //                    ((ErrorCodeA)obj).ErrorCode.ToString(),
            //                    ((ErrorCodeA)obj).ErrorDescription.ToString(),
            //                    ((ErrorCodeA)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((ErrorCodeA)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((ErrorCodeA)obj).MaintainTime)});
            DataRow row = this.DtSource.NewRow();
            row["AssErrorCode"] = ((ErrorCodeA)obj).ErrorCode.ToString();
            row["ErrorDescription"] = ((ErrorCodeA)obj).ErrorDescription.ToString();
            row["MaintainUser"] = ((ErrorCodeA)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((ErrorCodeA)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((ErrorCodeA)obj).MaintainTime);
            return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{			
			if(_facade==null){_facade = new TSModelFacadeFactory(base.DataProvider).CreateTSModelFacade();}
			return this._facade.GetSelectedErrorCodeByErrorCodeGroupCode(
                FormatHelper.PKCapitalFormat(this.txtErrorCodeGroupCodeQuery.Text.Trim()) ,
                FormatHelper.PKCapitalFormat(this.txtErrorCodeCodeQuery.Text.Trim()),
                inclusive,exclusive);
		}

		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{((ErrorCodeA)obj).ErrorCode.ToString(),
								   ((ErrorCodeA)obj).ErrorDescription.ToString(),
								   ((ErrorCodeA)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((ErrorCodeA)obj).MaintainDate)};
		}

		protected override string[] GetColumnHeaderText()
		{
            return new string[] {	"AssErrorCode",
									"ErrorDescription",
									"MaintainUser",
									"MaintainDate"};
		}

		protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("./FErrorCodeGroup2ErrorCodeAP.aspx", new string[]{"ErrorCodeGroup"}, new string[]{FormatHelper.CleanString( this.txtErrorCodeGroupCodeQuery.Text )}));
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("./FErrorCodeGroupMP.aspx"));
		}

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

        public void btnRefesh_Click(object sender, EventArgs e)
        {
            this.gridHelper.RequestData();
           
        }
		
	}
}
