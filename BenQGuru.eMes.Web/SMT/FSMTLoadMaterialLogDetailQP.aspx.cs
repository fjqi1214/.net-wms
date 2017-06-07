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
	public partial class FSMTLoadMaterialLogDetailQP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
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
			this.gridHelper.AddColumn( "Sequence", "项次", null);
			this.gridHelper.AddColumn( "ProductCode", "产品代码",	null);
			this.gridHelper.AddColumn( "sscode", "产线代码",	null);
			this.gridHelper.AddColumn( "MachineCode", "机台编码",	null);
			this.gridHelper.AddColumn( "MachineItemCode", "站位编码",	null);
			this.gridHelper.AddColumn( "SourceMaterialCode", "主物料号",	null);
			this.gridHelper.AddColumn( "MaterialCode", "替代物料号",	null);
			this.gridHelper.AddColumn( "FeederSpecCode", "Feeder规格",	null);
            this.gridHelper.AddColumn("Qty", "数量", HorizontalAlign.Right);
			this.gridHelper.AddColumn( "CheckResult", "比对结果", null);
			this.gridHelper.AddColumn( "CheckDescription", "比对描述", null);
			

			this.gridHelper.AddDefaultColumn( true, false );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
			
			if (this.IsPostBack == false)
			{
				this.txtItemCodeQuery.Text = this.GetRequestParam("itemcode");
				txtSSCodeQuery.Text = this.GetRequestParam("sscode");
				this.txtImportUserQuery.Text = this.GetRequestParam("importuser");
				this.txtImportDate.Text = FormatHelper.ToDateString(Convert.ToInt32(this.GetRequestParam("importdate")));
				this.txtImportTimeQuery.Text = FormatHelper.ToTimeString(Convert.ToInt32(this.GetRequestParam("importtime")));
				this.cmdQuery_Click(null, null);
			}
		}
		
		private Hashtable htMsg = new Hashtable();
		protected override DataRow GetGridRow(object obj)
		{
			SMTFeederMaterialImportLog item = (SMTFeederMaterialImportLog)obj;
			if (item.CheckDescription != null && item.CheckDescription != string.Empty && htMsg.ContainsKey(item.CheckDescription) == false)
			{
				htMsg.Add(item.CheckDescription, this.languageComponent1.GetString(item.CheckDescription));
			}
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{
            //                    "",
            //                    item.Sequence.ToString(),
            //                    item.ProductCode,
            //                    item.StepSequenceCode,
            //                    item.MachineCode,
            //                    item.MachineStationCode,
            //                    item.SourceMaterialCode,
            //                    item.MaterialCode,
            //                    item.FeederSpecCode,
            //                    item.Qty,
            //                    FormatHelper.StringToBoolean(item.CheckResult).ToString(),
            //                    (item.CheckDescription != null && item.CheckDescription != string.Empty ? htMsg[item.CheckDescription].ToString() : string.Empty)
            //                });
            DataRow row = this.DtSource.NewRow();
            row["Sequence"] = item.Sequence.ToString();
            row["ProductCode"] = item.ProductCode;
            row["sscode"] = item.StepSequenceCode;
            row["MachineCode"] = item.MachineCode;
            row["MachineItemCode"] = item.MachineStationCode;
            row["SourceMaterialCode"] = item.SourceMaterialCode;
            row["MaterialCode"] = item.MaterialCode;
            row["FeederSpecCode"] = item.FeederSpecCode;
            row["Qty"] = String.Format("{0:#,#}",item.Qty);
            row["CheckResult"] = FormatHelper.StringToBoolean(item.CheckResult).ToString();
            row["CheckDescription"] = (item.CheckDescription != null && item.CheckDescription != string.Empty ? htMsg[item.CheckDescription].ToString() : string.Empty);
            return row;


		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QuerySMTFeederMaterialImportLogDetail( 
				this.GetRequestParam("logno"),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QuerySMTFeederMaterialImportLogDetailCount( 
				this.GetRequestParam("logno"));
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			SMTFeederMaterialImportLog item = (SMTFeederMaterialImportLog)obj;
			if (item.CheckDescription != null && item.CheckDescription != string.Empty && htMsg.ContainsKey(item.CheckDescription) == false)
			{
				htMsg.Add(item.CheckDescription, this.languageComponent1.GetString(item.CheckDescription));
			}
			return new string[]{
								   item.Sequence.ToString(),
								   item.ProductCode,
								   item.StepSequenceCode,
								   item.MachineCode,
								   item.MachineStationCode,
								   item.SourceMaterialCode,
								   item.MaterialCode,
								   item.FeederSpecCode,
								   item.Qty.ToString(),
								   FormatHelper.StringToBoolean(item.CheckResult).ToString(),
								   (item.CheckDescription != null && item.CheckDescription != string.Empty ? htMsg[item.CheckDescription].ToString() : string.Empty)
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"Sequence",
									"ProductCode",
									"sscode",
									"MachineCode",
									"MachineItemCode",
									"SourceMaterialCode",
									"MaterialCode",
									"FeederSpecCode",
									"Qty",
									"CheckResult",
									"CheckDescription"
								};
		}
		#endregion

		protected void cmdReturn_ServerClick(object sender, EventArgs e)
		{
			this.Page.Response.Redirect("FSMTLoadMaterialLogQP.aspx");
		}
	}
}
