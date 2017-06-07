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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Domain.DataLink;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FFTDetailQuery 的摘要说明。
	/// </summary>
	public partial class FFTDetailQuery : BaseQPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
		protected GridHelper _gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 初始化页面语言
			this.InitPageLanguage(this.languageComponent1, false);

			this.txtSNQuery.Text = this.GetRequestParam("Rcard");
			this.txtTestSeq.Text = this.GetRequestParam("TestSeq");

			this._gridHelper = new GridHelper(this.gridWebGrid);

			this._helper = new WebQueryHelper( null,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );

			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);

			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();

				this._helper.Query(sender);
			}
		}

		private void _initialWebGrid()
		{
			// 产品序列号        测试组       电压 电流1~20
			this._gridHelper.AddColumn("SN",			"产品序列号",null);
			this._gridHelper.AddColumn("TestGroup",		"测试组",null);

			this._gridHelper.AddColumn("FreqLowSpec",			"频率(下限)",null);
			this._gridHelper.AddColumn("FreqUpSpec",			"频率(上限)",null);
			this._gridHelper.AddColumn("Freq",			"频率",null);
	
			this._gridHelper.AddColumn("DutyLowSpec",			"Duty_RATO(下限)",null);
			this._gridHelper.AddColumn("DutyUpSpec",			"Duty_RATO(上限)",null);
			this._gridHelper.AddColumn("Duty_RT",			"Duty_RT",null);

			this._gridHelper.AddColumn("BurstLowSpec",			"Burst_MD(下限)",null);
			this._gridHelper.AddColumn("BurstUpSpec",			"Burst_MD(上限)",null);
			this._gridHelper.AddColumn("Burst_MD",			"Burst_MD",null);

			this._gridHelper.AddColumn("ACLowSpec",			"电流(下限)",null);
			this._gridHelper.AddColumn("ACUpSpec",			"电流(上限)",null);
			

			this._gridHelper.AddColumn("AC1",			"测试电流1",null);
			this._gridHelper.AddColumn("AC2",			"测试电流2",null);
			this._gridHelper.AddColumn("AC3",			"测试电流3",null);
			this._gridHelper.AddColumn("AC4",			"测试电流4",null);
			this._gridHelper.AddColumn("AC5",			"测试电流5",null);

			this._gridHelper.AddColumn("AC6",			"测试电流6",null);
			this._gridHelper.AddColumn("AC7",			"测试电流7",null);
			this._gridHelper.AddColumn("AC8",			"测试电流8",null);
			this._gridHelper.AddColumn("AC9",			"测试电流9",null);
			this._gridHelper.AddColumn("AC10",			"测试电流10",null);

			this._gridHelper.AddColumn("AC11",			"测试电流11",null);
			this._gridHelper.AddColumn("AC12",			"测试电流12",null);
			this._gridHelper.AddColumn("AC13",			"测试电流13",null);
			this._gridHelper.AddColumn("AC14",			"测试电流14",null);
			this._gridHelper.AddColumn("AC15",			"测试电流15",null);

			this._gridHelper.AddColumn("AC16",			"测试电流16",null);
			this._gridHelper.AddColumn("AC17",			"测试电流17",null);
			this._gridHelper.AddColumn("AC18",			"测试电流18",null);
			this._gridHelper.AddColumn("AC19",			"测试电流19",null);
			this._gridHelper.AddColumn("AC20",			"测试电流20",null);

			this._gridHelper.AddColumn("AC21",			"测试电流21",null);
			this._gridHelper.AddColumn("AC22",			"测试电流22",null);
			this._gridHelper.AddColumn("AC23",			"测试电流23",null);
			this._gridHelper.AddColumn("AC24",			"测试电流24",null);
			this._gridHelper.AddColumn("AC25",			"测试电流25",null);

			this._gridHelper.AddColumn("AC26",			"测试电流26",null);
			this._gridHelper.AddColumn("AC27",			"测试电流27",null);
			this._gridHelper.AddColumn("AC28",			"测试电流28",null);
			this._gridHelper.AddColumn("AC29",			"测试电流29",null);
			this._gridHelper.AddColumn("AC30",			"测试电流30",null);

			this._gridHelper.AddColumn("AC31",			"测试电流31",null);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );
		}

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
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion

		private bool _checkRequireFields()
		{			
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck(this.lblSN,this.txtSNQuery,System.Int32.MaxValue,true) );

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return false;
			}	
			return true;
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{	
			if( this._checkRequireFields() )
			{
				FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);

				( e as WebQueryEventArgs ).GridDataSource = 
					facadeFactory.CreateDataLinkFacade().QueryFTDetail(					
					this.GetRequestParam("ItemCode"),
					FormatHelper.CleanString(this.txtSNQuery.Text).ToUpper(),
					FormatHelper.CleanString(this.txtTestSeq.Text).ToUpper()
					);

				( e as WebQueryEventArgs ).RowCount =
					facadeFactory.CreateDataLinkFacade().QueryFTDetailCount(
					this.GetRequestParam("ItemCode"),
					FormatHelper.CleanString(this.txtSNQuery.Text).ToUpper(),
					FormatHelper.CleanString(this.txtTestSeq.Text).ToUpper());
					
			}
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				FTDetail obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as FTDetail;
				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.RCard,
													  obj.TGroup,

													  obj.FreqLowSpec.ToString(),
													  obj.FreqUpSpec.ToString(),
													  obj.Freq.ToString(),

													  obj.DutyLowSpec.ToString(),
													  obj.DutyUpSpec.ToString(),
													  obj.Duty_Rt.ToString(),

													  obj.BurstLowSpec.ToString(),
													  obj.BurstUpSpec.ToString(),
													  obj.Burst_Md.ToString(),

													  obj.ACLowSpec.ToString(),
													  obj.ACUpSpec.ToString(),
													  obj.AC1.ToString(),
													  obj.AC2.ToString(),
													  obj.AC3.ToString(),
													  obj.AC4.ToString(),
													  obj.AC5.ToString(),

													  obj.AC6.ToString(),
													  obj.AC7.ToString(),
													  obj.AC8.ToString(),
													  obj.AC9.ToString(),
													  obj.AC10.ToString(),

													  obj.AC11.ToString(),
													  obj.AC12.ToString(),
													  obj.AC13.ToString(),
													  obj.AC14.ToString(),
													  obj.AC15.ToString(),

													  obj.AC16.ToString(),
													  obj.AC17.ToString(),
													  obj.AC18.ToString(),
													  obj.AC19.ToString(),
													  obj.AC20.ToString(),

													  obj.AC21.ToString(),
													  obj.AC22.ToString(),
													  obj.AC23.ToString(),
													  obj.AC24.ToString(),
													  obj.AC25.ToString(),

													  obj.AC26.ToString(),
													  obj.AC27.ToString(),
												      obj.AC28.ToString(),
													  obj.AC29.ToString(),
													  obj.AC30.ToString(),
													  obj.AC31.ToString(),
												  }
					);
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				FTDetail obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as FTDetail;
				( e as DomainObjectToExportRowEventArgs ).ExportRow = 
					new string[]{
									obj.RCard,
									obj.TGroup.ToString(),

									obj.FreqLowSpec.ToString(),
									obj.FreqUpSpec.ToString(),
									obj.Freq.ToString(),

									obj.DutyLowSpec.ToString(),
									obj.DutyUpSpec.ToString(),
									obj.Duty_Rt.ToString(),

									obj.BurstLowSpec.ToString(),
									obj.BurstUpSpec.ToString(),
									obj.Burst_Md.ToString(),

									obj.ACLowSpec.ToString(),
									obj.ACUpSpec.ToString(),

									obj.AC1.ToString(),
									obj.AC2.ToString(),
									obj.AC3.ToString(),
									obj.AC4.ToString(),
									obj.AC5.ToString(),

									obj.AC6.ToString(),
									obj.AC7.ToString(),
									obj.AC8.ToString(),
									obj.AC9.ToString(),
									obj.AC10.ToString(),

									obj.AC11.ToString(),
									obj.AC12.ToString(),
									obj.AC13.ToString(),
									obj.AC14.ToString(),
									obj.AC15.ToString(),

									obj.AC16.ToString(),
									obj.AC17.ToString(),
									obj.AC18.ToString(),
									obj.AC19.ToString(),
									obj.AC20.ToString(),

									obj.AC21.ToString(),
									obj.AC22.ToString(),
									obj.AC23.ToString(),
									obj.AC24.ToString(),
									obj.AC25.ToString(),

									obj.AC26.ToString(),
									obj.AC27.ToString(),
									obj.AC28.ToString(),
									obj.AC29.ToString(),
									obj.AC30.ToString(),
									obj.AC31.ToString(),
								};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{

			( e as ExportHeadEventArgs ).Heads = 
				new string[]{
								"产品序列号",
								"测试组",
								"频率(下限)",
								"频率(上限)",
								"Freq",
								"DUTY_RT(下限)",
								"DUTY_RT(上限)",
								"Duty_Rt",

								"BURST_MD(下限)",
								"BURST_MD(上限)",
								"Burst_Md",

								"电流(下限)",
								"电流(上限)",
								
								"AC1",
								"AC2",
								"AC3",
								"AC4",
								"AC5",

								"AC6",
								"AC7",
								"AC8",
								"AC9",
								"AC10",

								"AC11",
								"AC12",
								"AC13",
								"AC14",
								"AC15",

								"AC16",
								"AC17",
								"AC18",
								"AC19",
								"AC20",

								"AC21",
								"AC22",
								"AC23",
								"AC24",
								"AC25",

								"AC26",
								"AC27",
								"AC28",
								"AC29",
								"AC30",
								"AC31",
							};
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect("FFTQuery.aspx");
		}		
	}
}
