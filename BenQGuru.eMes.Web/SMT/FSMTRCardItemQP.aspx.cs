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
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// FSMTRCardItemQP 的摘要说明。
	/// </summary>
	public partial class FSMTRCardItemQP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
        protected global::System.Web.UI.WebControls.TextBox txtLoadBeginDate;
        protected global::System.Web.UI.WebControls.TextBox txtLoadEndDate;
		
		private BenQGuru.eMES.SMT.SMTFacade _facade ;//= new SMTFacadeFactory().Create();
	
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
   
  
			//this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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
			if (!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.txtLoadBeginDate.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));
				this.txtLoadEndDate.Text = this.txtLoadBeginDate.Text;
			}
			if (this.txtStartSN.Text == string.Empty && this.txtEndSN.Text != string.Empty)
				this.txtStartSN.Text = this.txtEndSN.Text;
			else if (this.txtEndSN.Text == string.Empty && this.txtStartSN.Text != string.Empty)
				this.txtEndSN.Text = this.txtStartSN.Text;
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
			this.gridHelper.AddColumn( "ProductCode", "产品代码",	null);
			this.gridHelper.AddColumn( "MOCode", "工单代码",	null);
			this.gridHelper.AddColumn( "RCard", "产品序列号",	null);
			this.gridHelper.AddColumn( "LoadResource", "上料资源",	null);
			this.gridHelper.AddColumn( "LoadStepSequenceCode", "上料产线",	null);
			this.gridHelper.AddColumn( "LoadUser", "上料人员",	null);
			this.gridHelper.AddColumn( "LoadDate", "上料日期",	null);
			this.gridHelper.AddColumn( "LoadTime", "上料时间",	null);
			this.gridHelper.AddLinkColumn( "MaterialDetail", "物料明细", null);

			this.gridHelper.AddDefaultColumn( false, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

		}
		
		protected override DataRow GetGridRow(object obj)
		{
			SMTRCardMaterial item = (SMTRCardMaterial)obj;
          
            DataRow row = this.DtSource.NewRow();
            row["ProductCode"] = item.ItemCode;
            row["MOCode"] = item.MOCode;
            row["RCard"] = item.RunningCard;
            row["LoadResource"] = item.ResourceCode;
            row["LoadStepSequenceCode"] = item.StepSequenceCode;
            row["LoadUser"] = item.MaintainUser;
            row["LoadDate"] = FormatHelper.ToDateString(item.MaintainDate);
            row["LoadTime"] = FormatHelper.ToTimeString(item.MaintainTime);

			return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QuerySMTRCardMaterial(
				this.txtConditionItem.Text.Trim().ToUpper(),
				this.txtConditionMo.Text.Trim().ToUpper(),
				FormatHelper.TODateInt(this.txtLoadBeginDate.Text),
				FormatHelper.TODateInt(this.txtLoadEndDate.Text),
				this.txtStartSN.Text.Trim().ToUpper(),
				this.txtEndSN.Text.Trim().ToUpper(),
				this.txtResourceCode.Text.Trim().ToUpper(),
				this.txtSSCode.Text.Trim().ToUpper(),
				this.txtMaterialCode.Text.Trim().ToUpper(),
				this.txtReelNoQuery.Text.Trim().ToUpper(),
				this.txtMaterialLotNoQuery.Text.Trim().ToUpper(),
				this.txtDateCodeQuery.Text.Trim().ToUpper(),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QuerySMTRCardMaterialCount( 
				this.txtConditionItem.Text.Trim().ToUpper(),
				this.txtConditionMo.Text.Trim().ToUpper(),
				FormatHelper.TODateInt(this.txtLoadBeginDate.Text),
				FormatHelper.TODateInt(this.txtLoadEndDate.Text),
				this.txtStartSN.Text.Trim().ToUpper(),
				this.txtEndSN.Text.Trim().ToUpper(),
				this.txtResourceCode.Text.Trim().ToUpper(),
				this.txtSSCode.Text.Trim().ToUpper(),
				this.txtMaterialCode.Text.Trim().ToUpper(),
				this.txtReelNoQuery.Text.Trim().ToUpper(),
				this.txtMaterialLotNoQuery.Text.Trim().ToUpper(),
				this.txtDateCodeQuery.Text.Trim().ToUpper());
		}

        protected override void Grid_ClickCell(GridRecord row, string commandName)
		{
            if (commandName == "MaterialDetail")
			{
				string strUrl = this.MakeRedirectUrl("FSMTRCardItemDetailQP.aspx", 
					new string[]{"mocode", "rcard"},
					new string[]{row.Items.FindItemByKey("MOCode").Text.ToString(), row.Items.FindItemByKey("RCard").Text.ToString()}
					);
				Response.Redirect(strUrl);
			}
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			SMTRCardMaterial item = (SMTRCardMaterial)obj;
			return new string[]{
								   item.ItemCode,
								   item.MOCode,
								   item.RunningCard,
								   item.ResourceCode,
								   item.StepSequenceCode,
								   item.MaintainUser,
								   FormatHelper.ToDateString(item.MaintainDate),
								   FormatHelper.ToTimeString(item.MaintainTime) };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"ItemCode",
									"MOCode",
									"RCard",
									"ResourceCode",
									"sscode",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"};
		}
		#endregion
	}
}
