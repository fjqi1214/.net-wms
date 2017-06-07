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
	/// FFeederMP 的摘要说明。
	/// </summary>
	public partial class FSMTLoadFeederQueryQP : BaseMPageNew
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
			this.gridHelper.AddColumn( "MachineCode", "机台编码",	null);
			this.gridHelper.AddColumn( "MachineStationCode", "机台编码",	null);
			this.gridHelper.AddColumn( "FeederSpecCode", "Feeder规格",	null);
			this.gridHelper.AddColumn( "FeederCode", "Feeder代码",	null);
			this.gridHelper.AddColumn( "ReelNo", "料卷编号",	null);
			this.gridHelper.AddColumn( "MaterialCode", "物料代码",	null);
			this.gridHelper.AddColumn( "LotNo", "批号",	null);
			this.gridHelper.AddColumn( "ResourceCode", "资源",	null);
			this.gridHelper.AddColumn( "StepSequenceCode", "产线",	null);
			this.gridHelper.AddColumn( "CheckResult", "比对结果",	null);
			this.gridHelper.AddColumn( "LoadType", "上料类型",	null);
			this.gridHelper.AddColumn( "LoadUser", "上料人员",	null);
			this.gridHelper.AddColumn( "LoadDate", "上料日期",	null);
			this.gridHelper.AddColumn( "LoadTime", "上料时间",	null);
			this.gridHelper.AddColumn( "UnLoadType", "下料类型",	null);
			this.gridHelper.AddColumn( "UnLoadUser", "下料人员",	null);
			this.gridHelper.AddColumn( "UnLoadDate", "下料日期",	null);
			this.gridHelper.AddColumn( "UnLoadTime", "下料时间",	null);
            this.gridHelper.AddColumn("ReelUsedQty", "料卷用量", HorizontalAlign.Right);
			this.gridHelper.AddColumn( "ExchangeFeederCode", "换下Feeder",	null);
			this.gridHelper.AddColumn( "ExchageReelNo", "换下料卷",	null);
			this.gridHelper.AddColumn( "FailReason", "失败原因",	null);
            this.gridHelper.AddColumn("RowCheckResult", "hhhhh", true);

            this.gridWebGrid.Columns.FromKey("RowCheckResult").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("ReelUsedQty").Format = "#,#";
            //this.gridWebGrid.Columns.FromKey("ReelUsedQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;

			this.gridHelper.AddDefaultColumn( true, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			for (int i = 1; i < this.gridWebGrid.Columns.Count; i++)
			{
				this.gridWebGrid.Columns[i].Width = Unit.Pixel(90);
			}
		}

        protected override void cmdQuery_Click(object sender, EventArgs e)
        {
            base.cmdQuery_Click(sender, e);
            foreach (GridRecord row  in this.gridWebGrid.Rows)
            {
                if (FormatHelper.StringToBoolean(row.Items.FindItemByKey("RowCheckResult").Value.ToString().Trim()) == false)
                {
                    row.CssClass = "ForeColorRed";
                }
            }
        }
		protected override DataRow GetGridRow(object obj)
		{
			MachineFeederLog item = (MachineFeederLog)obj;
                //new object[]{
                //                "",
                //                item.ProductCode,
                //                item.MOCode,
                //                item.MachineCode,
                //                item.MachineStationCode,
                //                item.FeederSpecCode,
                //                item.FeederCode,
                //                item.ReelNo,
                //                item.MaterialCode,
                //                item.LotNo,
                //                item.OpeResourceCode,
                //                item.OpeStepSequenceCode,
                //                FormatHelper.StringToBoolean(item.CheckResult).ToString(),
                //                item.OperationType,
                //                item.LoadUser,
                //                FormatHelper.ToDateString(item.LoadDate),
                //                FormatHelper.ToTimeString(item.LoadTime),
                //                item.UnLoadType,
                //                item.UnLoadUser,
                //                FormatHelper.ToDateString(item.UnLoadDate),
                //                FormatHelper.ToTimeString(item.UnLoadTime),
                //                item.ReelUsedQty,
                //                item.ExchangeFeederCode,
                //                item.ExchageReelNo,
                //                ParseFailReason(item.FailReason)
                //                });

            DataRow row = this.DtSource.NewRow();
            row["ProductCode"] = item.ProductCode;
            row["MOCode"] = item.MOCode;
            row["MachineCode"] = item.MachineCode;
            row["MachineStationCode"] = item.MachineStationCode;
            row["FeederSpecCode"] = item.FeederSpecCode;
            row["FeederCode"] = item.FeederCode;
            row["ReelNo"] = item.ReelNo;
            row["MaterialCode"] = item.MaterialCode;
            row["LotNo"] = item.LotNo;
            row["ResourceCode"] = item.OpeResourceCode;
            row["StepSequenceCode"] = item.OpeStepSequenceCode;
            row["CheckResult"] = FormatHelper.StringToBoolean(item.CheckResult).ToString();
            row["LoadType"] = item.OperationType;
            row["LoadUser"] = item.LoadUser;
            row["LoadDate"] = FormatHelper.ToDateString(item.LoadDate);
            row["LoadTime"] = FormatHelper.ToTimeString(item.LoadTime);
            row["UnLoadType"] = item.UnLoadType;
            row["UnLoadUser"] = item.UnLoadUser;
            row["UnLoadDate"] = FormatHelper.ToDateString(item.UnLoadDate);
            row["UnLoadTime"] = FormatHelper.ToTimeString(item.UnLoadTime);
            row["ReelUsedQty"] = String.Format("{0:#,#}",item.ReelUsedQty);
            row["ExchangeFeederCode"] = item.ExchangeFeederCode;
            row["ExchageReelNo"] = item.ExchageReelNo;
            row["FailReason"] = ParseFailReason(item.FailReason);
            row["RowCheckResult"] = item.CheckResult;
            //if (FormatHelper.StringToBoolean(item.CheckResult) == false)
            //{
            //    row.Style.ForeColor = Color.Red;
            //}
			return row;
		}
		private string ParseFailReason(string reason)
		{
			if (reason == string.Empty)
				return string.Empty;
			if (reason.IndexOf(" ") < 0)
				return this.languageComponent1.GetString(reason);
			else
				return this.languageComponent1.GetString(reason.Substring(0, reason.IndexOf(" "))) + reason.Substring(reason.IndexOf(" "));
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryMachineFeederLog(
				this.txtConditionItem.Text.Trim().ToUpper(),
				this.txtConditionMo.Text.Trim().ToUpper(),
				this.txtMaterialCodeQuery.Text.Trim().ToUpper(),
				this.txtLineCodeQuery.Text.Trim().ToUpper(),
				this.txtMachineCodeQuery.Text.Trim().ToUpper(),
				this.txtMachineStationCodeQuery.Text.Trim().ToUpper(),
				this.txtUserCodeQuery.Text.Trim().ToUpper(),
				this.txtMaterialLotNoQuery.Text.Trim(),
				this.txtReelNoQuery.Text.Trim().ToUpper(),
				this.ddlResultQuery.SelectedValue,
				FormatHelper.TODateInt(this.txtLoadBeginDate.Text).ToString(),
				FormatHelper.TODateInt(this.txtLoadEndDate.Text).ToString(),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryMachineFeederLogCount( 
				this.txtConditionItem.Text.Trim().ToUpper(),
				this.txtConditionMo.Text.Trim().ToUpper(),
				this.txtMaterialCodeQuery.Text.Trim().ToUpper(),
				this.txtLineCodeQuery.Text.Trim().ToUpper(),
				this.txtMachineCodeQuery.Text.Trim().ToUpper(),
				this.txtMachineStationCodeQuery.Text.Trim().ToUpper(),
				this.txtUserCodeQuery.Text.Trim().ToUpper(),
				this.txtMaterialLotNoQuery.Text.Trim().ToUpper(),
				this.txtReelNoQuery.Text.Trim().ToUpper(),
				this.ddlResultQuery.SelectedValue,
				FormatHelper.TODateInt(this.txtLoadBeginDate.Text).ToString(),
				FormatHelper.TODateInt(this.txtLoadEndDate.Text).ToString());
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			MachineFeederLog item = (MachineFeederLog)obj;
			return new string[]{
								   item.ProductCode,
								   item.MOCode,
								   item.MachineCode,
								   item.MachineStationCode,
								   item.FeederSpecCode,
								   item.FeederCode,
								   item.ReelNo,
								   item.MaterialCode,
								   item.LotNo,
								   item.OpeResourceCode,
								   item.OpeStepSequenceCode,
								   FormatHelper.StringToBoolean(item.CheckResult).ToString(),
								   item.OperationType,
								   item.LoadUser,
								   FormatHelper.ToDateString(item.LoadDate),
								   FormatHelper.ToTimeString(item.LoadTime),
								   item.UnLoadType,
								   item.UnLoadUser,
								   FormatHelper.ToDateString(item.UnLoadDate),
								   FormatHelper.ToTimeString(item.UnLoadTime),
								   item.ReelUsedQty.ToString(),
								   item.ExchangeFeederCode,
								   item.ExchageReelNo,
								   ParseFailReason(item.FailReason) };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"ItemCode",
									"MOCode",
									"MachineCode",
									"MachineStationCode",
									"FeederSpecCode",
									"FeederCode",
									"ReelNo",
									"MaterialCode",
									"LotNo",
									"ResourceCode",
									"StepSequenceCode",
									"CheckResult",
									"LoadType",
									"LoadUser",
									"LoadDate",
									"LoadTime",
									"UnLoadType",
									"UnLoadUser",
									"UnLoadDate",
									"UnLoadTime",
									"ReelUsedQty",
									"ExchangeFeederCode",
									"ExchageReelNo",
									"FailReason"};
		}
		#endregion


	}
}
