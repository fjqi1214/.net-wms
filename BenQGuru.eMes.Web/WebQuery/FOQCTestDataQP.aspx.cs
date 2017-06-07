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
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FOQCTestDataQP 的摘要说明。
	/// </summary>
	public partial class FOQCTestDataQP : BaseQPage
	{

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected GridHelper _gridHelper = null;
		protected System.Web.UI.WebControls.Label lblStartSNQuery;
		protected System.Web.UI.WebControls.Label lblEndSNQuery;
		protected System.Web.UI.WebControls.Label lblSoftwareNameQuery;
		protected System.Web.UI.WebControls.TextBox txtSoftwareNameQuery;
		protected System.Web.UI.WebControls.Label lblSoftwareVersionQuery;
		protected System.Web.UI.WebControls.Label lblStepSequenceConditionQuery;
		protected BenQGuru.eMES.Web.SelectQuery.SelectableTextBox txtConditionStepSequence;
		protected System.Web.UI.WebControls.TextBox txtSoftwareVersionQuery;

		protected BenQGuru.eMES.Web.UserControl.eMESDate txtOQCBeginDate;
		protected BenQGuru.eMES.Web.UserControl.eMESTime txtOQCBeginTime;
		protected BenQGuru.eMES.Web.UserControl.eMESDate txtOQCEndDate;
		protected BenQGuru.eMES.Web.UserControl.eMESTime txtOQCEndTime;
	
		private QueryOQCFunctionFacade _facade = null ;

		protected GridHelper gridSNHelper ;
		protected WebQueryHelper _gridSNHelper = null;

		protected GridHelper gridFTHelper ;
		protected System.Web.UI.WebControls.Label Label1;
		protected WebQueryHelper _gridFTHelper ;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.gridSNHelper = new GridHelper(this.gridSN) ;
			this._gridSNHelper = new WebQueryHelper( this.cmdQuery,this.btnExpDimension,this.gridSN,this.Pagersizeselector2,this.Pagertoolbar2,this.languageComponent1 );
			this._gridSNHelper.LoadGridDataSource +=new EventHandler(LoadDataSourceSN);
			this._gridSNHelper.DomainObjectToGridRow +=new EventHandler(GetGridRowSN);
			this._gridSNHelper.GetExportHeadText += new EventHandler(_gridSNHelper_GetExportHeadText);
			this._gridSNHelper.DomainObjectToExportRow += new EventHandler(_gridSNHelper_DomainObjectToExportRow);

			this.gridFTHelper = new GridHelper(this.grdFT) ;
			this._gridFTHelper = new WebQueryHelper( this.cmdQuery,this.btnExportFT,this.grdFT,this.Pagersizeselector1,this.Pagertoolbar1,this.languageComponent1 );
			this._gridFTHelper.LoadGridDataSource +=new EventHandler(LoadDataSourceFT);
			this._gridFTHelper.DomainObjectToGridRow +=new EventHandler(GetGridRowFT);
			this._gridFTHelper.GetExportHeadText +=new EventHandler(_gridFTHelper_GetExportHeadText);
			this._gridFTHelper.DomainObjectToExportRow +=new EventHandler(_gridFTHelper_DomainObjectToExportRow);

			FormatHelper.SetSNRangeValue(txtStartSnQuery,txtEndSnQuery);

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				bool loaddata = false;
				txtOQCBeginDate.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				txtOQCEndDate.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );

				this.txtOQCBeginTime.Text = FormatHelper.ToTimeString( 0 ) ;
				this.txtOQCEndTime.Text = FormatHelper.ToTimeString(235959);

				if(Page.Request["reworkrcard"] != null && Page.Request["reworkrcard"] != string.Empty)
				{
					this.txtStartSnQuery.Text = Page.Request["reworkrcard"];
					this.txtEndSnQuery.Text = this.txtStartSnQuery.Text; 

					loaddata = true;
				}

				if(Page.Request["reworklotno"] != null && Page.Request["reworklotno"] != string.Empty)
				{
					this.txtOQCLotQuery.Text = Page.Request["reworklotno"];

					loaddata = true;
				}

				if(loaddata)
				{
					DateTime begin = new DateTime(DateTime.Now.Year-2,DateTime.Now.Month,DateTime.Now.Day);
					this.txtOQCBeginDate.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(begin) );
					this._helper.Query(null);
				}

				InitWebGridSN() ;

				InitWebGridFT() ;
			}

			
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
			this.grdFT.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.grdFT_ClickCellButton);
			this.gridSN.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridSN_ClickCellButton);
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

		private string rcardListInCarton = null;
		private void GetRcardByCarton()
		{
			if (rcardListInCarton == null)
			{
				if(_facade==null)
				{
					_facade = new QueryOQCFunctionFacade(this.DataProvider);
				}
				rcardListInCarton = _facade.GetRCardListInCarton(
					this.txtCartonNoQuery.Text.Trim().ToUpper(),
					this.txtStartSnQuery.Text.Trim().ToUpper(),
					this.txtEndSnQuery.Text.Trim().ToUpper());
			}
		}
		
		#region SNGrid
		protected void InitWebGridSN()
		{
			this.gridSNHelper.AddColumn("RCard","产品序列号",null) ;
			this.gridSNHelper.AddColumn( "RCardSeq", "RCardSeq",	null);
			this.gridSNHelper.AddColumn( "SSCode", "产线",	null);
			this.gridSNHelper.AddColumn( "ResCode", "资源",	null);
			this.gridSNHelper.AddColumn( "TestResult", "测试结果",	null);
			this.gridSNHelper.AddColumn( "TestDate", "测试日期",	null);
			this.gridSNHelper.AddColumn( "TestTime", "测试时间",	null);
			this.gridSNHelper.AddColumn( "TestUser", "测试人员",	null);
			this.gridSNHelper.AddLinkColumn( "TestData", "测试数据",	null);

			this.gridSNHelper.AddColumn( "LotNo", "LotNo",	null);
			this.gridSNHelper.AddColumn( "LotSeq", "LotSeq",	null);

			this.gridSNHelper.Grid.Columns.FromKey("LotNo").Hidden = true ;
			this.gridSNHelper.Grid.Columns.FromKey("LotSeq").Hidden = true ;
			this.gridSNHelper.Grid.Columns.FromKey("RCardSeq").Hidden = true ;

			//多语言
			this.gridSNHelper.ApplyLanguage( this.languageComponent1 );
		}

		private void _gridSNHelper_GetExportHeadText(object sender, EventArgs e)
		{
			( e as ExportHeadEventArgs ).Heads = new string[]{
																 "产品序列号",
																 "LotNo",
																"长度",
																"宽度",
																"板上高",
																"板厚",
																"总高",
																"左右孔距",
																"左孔到中间孔距",
																"右孔到中间孔距",
															 };
		}
		
		private object[] _DimentionValues = new object[]{};
		private object[] DimentionValues
		{
			get
			{
				return _DimentionValues;
			}
			set
			{
				if(value == null)
					_DimentionValues = new object[]{};
				else
					_DimentionValues = value;
			}
		}

		private ArrayList GetDimentionValues(OQCDimentionValue ov)
		{
			ArrayList list = new ArrayList();
			
			foreach(OQCDimentionValue v in this.DimentionValues)
			{
				if(ov.RunningCard == v.RunningCard && ov.LOTNO == v.LOTNO)
					list.Add(v);
			}

			return list;
		}

		private void _gridSNHelper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				string[] objs = new string[]{"","","","","","","","","","",};

				OQCDimentionValue ov = ( e as DomainObjectToExportRowEventArgs ).DomainObject as OQCDimentionValue;

				ArrayList list = GetDimentionValues(ov);
				foreach(OQCDimentionValue v in list)
				{
					int i = 2;
					objs[0] = v.RunningCard;
					objs[1] = v.LOTNO;

					if(v.ParamName.ToUpper() == DimentionParam.Length.ToUpper())
						i += 0;
					else if(v.ParamName.ToUpper() == DimentionParam.Width.ToUpper())
						i += 1;
					else if(v.ParamName.ToUpper() == DimentionParam.BoardHeight.ToUpper())
						i += 2;
					else if(v.ParamName.ToUpper() == DimentionParam.Height.ToUpper())
						i += 3;
					else if(v.ParamName.ToUpper() == DimentionParam.AllHeight.ToUpper())
						i += 4;
					else if(v.ParamName.ToUpper() == DimentionParam.Left2Right.ToUpper())
						i += 5;
					else if(v.ParamName.ToUpper() == DimentionParam.Left2Middle.ToUpper())
						i += 6;
					else if(v.ParamName.ToUpper() == DimentionParam.Right2Middle.ToUpper())
						i += 7;

					objs[i] = v.MinValue.ToString("0.00")+"/"+v.MaxValue.ToString("0.00")+"/"+v.ActualValue.ToString("0.00");
				}

				( e as DomainObjectToExportRowEventArgs ).ExportRow =  objs ;
			} 
		}

		private void GetGridRowSN(object sender, EventArgs e)
		//protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRowSN(object obj)
		{
			//OQCDimention oqc = obj as OQCDimention;

			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				OQCDimention oqc = ( e as DomainObjectToGridRowEventArgs ).DomainObject as OQCDimention;
				( e as DomainObjectToGridRowEventArgs ).GridRow = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
					new object[]{
									oqc.RunningCard,
									oqc.RunningCardSequence,
									oqc.StepSequenceCode,
									oqc.ResourceCode,
									oqc.TestResult,
									FormatHelper.ToDateString(oqc.TestDate),
									FormatHelper.ToTimeString(oqc.TestTime),
									oqc.TestUser,
									"",
									oqc.LOTNO,
									oqc.LotSequence,
				});
                
                
			}
		}

		private void LoadDataSourceSN(object sender, EventArgs e)
		//protected object[] LoadDataSourceSN(int inclusive, int exclusive)
		{
			int seq ;
			try
			{
				seq = int.Parse(this.txtSeq.Value) ;
			}
			catch
			{
				seq = -1 ;
			}

			int OQCBeginDate = FormatHelper.TODateInt(this.txtOQCBeginDate.Text);
			int OQCEndDate = FormatHelper.TODateInt(this.txtOQCEndDate.Text);

			int OQCBeginTime = FormatHelper.TOTimeInt(this.txtOQCBeginTime.Text);
			int OQCEndTime = FormatHelper.TOTimeInt(this.txtOQCEndTime.Text);


			if(_facade==null)
			{
				_facade = new QueryOQCFunctionFacade(this.DataProvider);
			}
			
			GetRcardByCarton();
			string rcardStart = this.txtStartSnQuery.Text.Trim().ToUpper();
			string rcardEnd = this.txtEndSnQuery.Text.Trim().ToUpper();
			if (this.rcardListInCarton != null && this.rcardListInCarton != string.Empty)
			{
				rcardStart = rcardListInCarton;
				rcardEnd = rcardListInCarton;
			}

			//导出
			if((sender is System.Web.UI.HtmlControls.HtmlInputButton ) && 
				((System.Web.UI.HtmlControls.HtmlInputControl)(((System.Web.UI.HtmlControls.HtmlInputButton)(sender)))).Name == "btnExpDimension")
			{
				object[] objs = _facade.QueryOQCDimentionValueForExport(
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtOQCLotQuery.Text),
					rcardStart,
					rcardEnd,
					FormatHelper.CleanString(this.txtSSCode.Text),
					OQCBeginDate,OQCBeginTime,
					OQCEndDate,OQCEndTime,
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);

				this.DimentionValues = objs;

				ArrayList list = new ArrayList();
				Hashtable table = new Hashtable();

				foreach(OQCDimentionValue v in DimentionValues)
				{
					if(!table.ContainsKey(v.RunningCard+v.LOTNO))
					{
						table.Add(v.RunningCard+v.LOTNO,v);
						list.Add(v);
					}
				}

				( e as WebQueryEventArgs ).GridDataSource = (object[])list.ToArray(typeof(object));
					

				if(( e as WebQueryEventArgs ).GridDataSource != null)
				{
					( e as WebQueryEventArgs ).RowCount = ( e as WebQueryEventArgs ).GridDataSource.Length;
				}
			}
			else
			{
				( e as WebQueryEventArgs ).GridDataSource = 
					_facade.QueryOQCDimention(
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtOQCLotQuery.Text),
					rcardStart,
					rcardEnd,
					FormatHelper.CleanString(this.txtSSCode.Text),
					OQCBeginDate,OQCBeginTime,
					OQCEndDate,OQCEndTime,
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);

				( e as WebQueryEventArgs ).RowCount = 
					this._facade.QueryOQCDimentionCount(
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtOQCLotQuery.Text),
					rcardStart,
					rcardEnd,
					FormatHelper.CleanString(this.txtSSCode.Text),
					OQCBeginDate,OQCBeginTime,
					OQCEndDate,OQCEndTime);
			}
		}

		protected int GetRowCountSN()
		{
			int seq ;
			try
			{
				seq = int.Parse(this.txtSeq.Value) ;
			}
			catch
			{
				seq = -1 ;
			}

			if(_facade==null)
			{
				_facade = new QueryOQCFunctionFacade(this.DataProvider);
			}


			int OQCBeginDate = FormatHelper.TODateInt(this.txtOQCBeginDate.Text);
			int OQCEndDate = FormatHelper.TODateInt(this.txtOQCEndDate.Text);

			int OQCBeginTime = FormatHelper.TOTimeInt(this.txtOQCBeginTime.Text);
			int OQCEndTime = FormatHelper.TOTimeInt(this.txtOQCEndTime.Text);

			return this._facade.QueryOQCDimentionCount(
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.txtOQCLotQuery.Text),
				FormatHelper.CleanString(this.txtStartSnQuery.Text),
				FormatHelper.CleanString(this.txtEndSnQuery.Text),
				FormatHelper.CleanString(this.txtSSCode.Text),
				OQCBeginDate,OQCBeginTime,
				OQCEndDate,OQCEndTime
				);


		}

		#endregion
	
		#region GridFT
		private void InitWebGridFT()
		{
			this.gridFTHelper.AddColumn("RCard","产品序列号",null) ;
			this.gridFTHelper.AddColumn( "RCardSeq", "RCardSeq",	null);
			this.gridFTHelper.AddColumn( "SSCode", "产线",	null);
			this.gridFTHelper.AddColumn( "ResCode", "资源",	null);
			this.gridFTHelper.AddColumn( "DutyRado", "Duty Rado",	null);
			this.gridFTHelper.AddColumn( "BurstMOFreq", "Burst MO频率",	null);
			this.gridFTHelper.AddColumn( "TestResult", "测试结果",	null);
			this.gridFTHelper.AddColumn( "TestDate", "测试日期",	null);
			this.gridFTHelper.AddColumn( "TestTime", "测试时间",	null);
			this.gridFTHelper.AddColumn( "TestUser", "测试人员",	null);
			this.gridFTHelper.AddLinkColumn( "TestData", "测试数据",	null);

			this.gridFTHelper.AddColumn( "LotNo", "LotNo",	null);
			this.gridFTHelper.AddColumn( "LotSeq", "LotSeq",	null);

			this.gridFTHelper.Grid.Columns.FromKey("LotNo").Hidden = true ;
			this.gridFTHelper.Grid.Columns.FromKey("LotSeq").Hidden = true ;

			this.gridFTHelper.Grid.Columns.FromKey("RCardSeq").Hidden = true ;

			//多语言
			this.gridFTHelper.ApplyLanguage( this.languageComponent1 );
		}

		private int GetElectricCount()
		{
			int OQCBeginDate = FormatHelper.TODateInt(this.txtOQCBeginDate.Text);
			int OQCEndDate = FormatHelper.TODateInt(this.txtOQCEndDate.Text);

			int OQCBeginTime = FormatHelper.TOTimeInt(this.txtOQCBeginTime.Text);
			int OQCEndTime = FormatHelper.TOTimeInt(this.txtOQCEndTime.Text);

			if(_facade==null)
			{
				_facade = new QueryOQCFunctionFacade(this.DataProvider);
			}

			return this._facade.QueryMaxOQCFuncTestValueDetailElectricCount(
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtOQCLotQuery.Text),
					FormatHelper.CleanString(this.txtStartSnQuery.Text),
					FormatHelper.CleanString(this.txtEndSnQuery.Text),
					FormatHelper.CleanString(this.txtSSCode.Text),
					OQCBeginDate,OQCBeginTime,
					OQCEndDate,OQCEndTime
				);
		}

		private void _gridFTHelper_GetExportHeadText(object sender, EventArgs e)
		{
			int electriccount = GetElectricCount();
			string[] heads = new string[electriccount + 7];
			heads[0] = "RunningCard";
			heads[1] = "LOTNO";
			heads[2] = "组别";
			heads[3] = "Duty Rado";
			heads[4] = "Burst MO频率";
			heads[5] = "频率";
			heads[6] = "电流标准值";

			for(int i = 1; i <= electriccount ; i++ )
				heads[i+6] = "电流值"+i.ToString();


			( e as ExportHeadEventArgs ).Heads = heads;
		}

		private object[] _EleDetails  = new object[]{};
		private object[] EleDetails
		{
			get
			{
				return _EleDetails;
			}
			set
			{
				if(value == null)
					_EleDetails = new object[]{};
				else
					_EleDetails = value;
			}
		}

		private object[] GetGroupEleDetails(OQCFuncTestValueDetail testValue)
		{
			ArrayList list = new ArrayList();

			foreach(OQCFuncTestValueEleDetail d in this.EleDetails)
			{
				if(d.RunningCard == testValue.RunningCard && d.RunningCardSequence == testValue.RunningCardSequence &&
					d.GroupSequence == testValue.GroupSequence)
				{
					list.Add(d);
				}
			}

			return list.ToArray();
		}

		private OQCFuncTestValue GetFTV(OQCFuncTestValueDetail testValue)
		{
			if(this.OQCFuncTestValues == null)
				return null;

			foreach(OQCFuncTestValue v in OQCFuncTestValues)
			{
				if(testValue.RunningCard == v.RunningCard && testValue.RunningCardSequence == v.RunningCardSequence &&
					testValue.LotNO == v.LotNO && testValue.LotSequence == v.LotSequence)
					return v;
			}

			return null;
		}

		private Hashtable htEleSeq = null;
		private void _gridFTHelper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				int electriccount = GetElectricCount();
				OQCFuncTestValueDetail testValue = ( e as DomainObjectToExportRowEventArgs ).DomainObject as OQCFuncTestValueDetail;

				string[] objs = new string[7+electriccount];

				OQCFuncTestValue FTV = GetFTV(testValue);
				
				objs[0] = testValue.RunningCard;
				objs[1] = testValue.LotNO;
				objs[2] = testValue.GroupSequence.ToString();
				if(FTV != null)
				{
					objs[3] = FTV.MinDutyRatoMin.ToString("0.00")+"/"+FTV.MinDutyRatoMax.ToString("0.00")+"/"+FTV.MinDutyRatoValue.ToString("0.00");
					objs[4] = FTV.BurstMdFreMin.ToString("0.00")+"/"+FTV.BurstMdFreMax.ToString("0.00")+"/"+FTV.BurstMdFreValue.ToString("0.00");
				}
				else
				{
					objs[3] = "";
					objs[4] = "";
				}

				objs[5] = testValue.FreMin.ToString("0.00")+"/"+testValue.FreMax.ToString("0.00")+"/"+testValue.FreValue.ToString("0.00");
				objs[6] = testValue.ElectricMin.ToString("0.00")+"/"+testValue.ElectricMax.ToString("0.00");//+"/"+testValue.ElectricValue.ToString("0.00");

				object[] details = GetGroupEleDetails(testValue);

				if (htEleSeq == null)
					htEleSeq = new Hashtable();
				string strKey = testValue.RunningCard + ":" + testValue.RunningCardSequence + ":" + testValue.GroupSequence.ToString();
				if (htEleSeq.ContainsKey(strKey) == false)
					htEleSeq.Add(strKey, 0);
				int iStartIdx = Convert.ToInt32(htEleSeq[strKey]);

				for(int i = 0 ; i < electriccount ; i++ )
				{
					if(i < details.Length)
					{
						OQCFuncTestValueEleDetail d = (OQCFuncTestValueEleDetail)details[i + iStartIdx];
						objs[i + 7] = /*d.ElectricMin.ToString("0.00")+"/"+d.ElectricMax.ToString("0.00")+"/"+*/ d.ElectricValue.ToString("0.00");
					}
					else
					{
						objs[i + 7] = "";
					}
				}
				htEleSeq[strKey] = iStartIdx + electriccount;

				( e as DomainObjectToExportRowEventArgs ).ExportRow =  objs ;
			}  
		}
		
		private void GetGridRowFT(object sender, EventArgs e)
		//private Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRowFT(object obj)
		{
//			OQCFuncTestValue testValue = obj as OQCFuncTestValue;
//
//			return 

			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				OQCFuncTestValue testValue = ( e as DomainObjectToGridRowEventArgs ).DomainObject as OQCFuncTestValue;
				( e as DomainObjectToGridRowEventArgs ).GridRow = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
					new object[]{
									testValue.RunningCard,
									testValue.RunningCardSequence,
									testValue.StepSequenceCode,
									testValue.ResourceCode,
									testValue.MinDutyRatoMin.ToString("0.00")+"/"+testValue.MinDutyRatoMax.ToString("0.00")+"/"+testValue.MinDutyRatoValue.ToString("0.00"),
									testValue.BurstMdFreMin.ToString("0.00")+"/"+testValue.BurstMdFreMax.ToString("0.00")+"/"+testValue.BurstMdFreValue.ToString("0.00"),
									testValue.ProductStatus,
									FormatHelper.ToDateString(testValue.MaintainDate),
									FormatHelper.ToTimeString(testValue.MaintainTime),
									testValue.MaintainUser,
									"",
									testValue.LotNO,
									testValue.LotSequence,
					});
				}  

		}

		private object[] OQCFuncTestValues;

		private void LoadDataSourceFT(object sender, EventArgs e)
		//private  object[] LoadDataSourceFT(int inclusive, int exclusive)
		{
			// Added by Icyer 2006/08/16
			if (this.txtConditionItem.Text.Trim() == string.Empty)
			{
				throw new Exception("$Error_ItemCode_NotCompare");
			}
			// Added end
			int seq ;
			try
			{
				seq = int.Parse(this.txtSeq.Value) ;
			}
			catch
			{
				seq = -1 ;
			}

			if(_facade==null)
			{
				_facade = new QueryOQCFunctionFacade(this.DataProvider);
			}

			int OQCBeginDate = FormatHelper.TODateInt(this.txtOQCBeginDate.Text);
			int OQCEndDate = FormatHelper.TODateInt(this.txtOQCEndDate.Text);

			int OQCBeginTime = FormatHelper.TOTimeInt(this.txtOQCBeginTime.Text);
			int OQCEndTime = FormatHelper.TOTimeInt(this.txtOQCEndTime.Text);
			
			GetRcardByCarton();
			string rcardStart = this.txtStartSnQuery.Text.Trim().ToUpper();
			string rcardEnd = this.txtEndSnQuery.Text.Trim().ToUpper();
			if (this.rcardListInCarton != null && this.rcardListInCarton != string.Empty)
			{
				rcardStart = rcardListInCarton;
				rcardEnd = rcardListInCarton;
			}

			//导出
			if((sender is System.Web.UI.HtmlControls.HtmlInputButton ) && 
				((System.Web.UI.HtmlControls.HtmlInputControl)(((System.Web.UI.HtmlControls.HtmlInputButton)(sender)))).Name == "btnExportFT")
			{

				this.EleDetails = _facade.QueryOQCFuncTestValueEleDetail(
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtOQCLotQuery.Text),
					rcardStart,
					rcardEnd,
					FormatHelper.CleanString(this.txtSSCode.Text),
					OQCBeginDate,OQCBeginTime,
					OQCEndDate,OQCEndTime);

				this.OQCFuncTestValues = _facade.QueryOQCFuncTestValue(
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtOQCLotQuery.Text),
					rcardStart,
					rcardEnd,
					FormatHelper.CleanString(this.txtSSCode.Text),
					OQCBeginDate,OQCBeginTime,
					OQCEndDate,OQCEndTime,
					0,
					int.MaxValue);

				( e as WebQueryEventArgs ).GridDataSource = 
					_facade.QueryOQCFuncTestValueDetailForExport(
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtOQCLotQuery.Text),
					rcardStart,
					rcardEnd,
					FormatHelper.CleanString(this.txtSSCode.Text),
					OQCBeginDate,OQCBeginTime,
					OQCEndDate,OQCEndTime,
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);

				if(( e as WebQueryEventArgs ).GridDataSource != null)
				{
					( e as WebQueryEventArgs ).RowCount = ( e as WebQueryEventArgs ).GridDataSource.Length;
				}
			}
			else
			{
				( e as WebQueryEventArgs ).GridDataSource = 
					_facade.QueryOQCFuncTestValue(
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtOQCLotQuery.Text),
					rcardStart,
					rcardEnd,
					FormatHelper.CleanString(this.txtSSCode.Text),
					OQCBeginDate,OQCBeginTime,
					OQCEndDate,OQCEndTime,
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);

				( e as WebQueryEventArgs ).RowCount = 
					this._facade.QueryOQCFuncTestValueCount(
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
					FormatHelper.CleanString(this.txtOQCLotQuery.Text),
					rcardStart,
					rcardEnd,
					FormatHelper.CleanString(this.txtSSCode.Text),
					OQCBeginDate,OQCBeginTime,
					OQCEndDate,OQCEndTime);
			}
		}


		private int GetRowCountFT()
		{
			int seq ;
			try
			{
				seq = int.Parse(this.txtSeq.Value) ;
			}
			catch
			{
				seq = -1 ;
			}

			if(_facade==null)
			{
				_facade = new QueryOQCFunctionFacade(this.DataProvider);
			}

			int OQCBeginDate = FormatHelper.TODateInt(this.txtOQCBeginDate.Text);
			int OQCEndDate = FormatHelper.TODateInt(this.txtOQCEndDate.Text);

			int OQCBeginTime = FormatHelper.TOTimeInt(this.txtOQCBeginTime.Text);
			int OQCEndTime = FormatHelper.TOTimeInt(this.txtOQCEndTime.Text);

			return this._facade.QueryOQCFuncTestValueCount(
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.txtOQCLotQuery.Text),
				FormatHelper.CleanString(this.txtStartSnQuery.Text),
				FormatHelper.CleanString(this.txtEndSnQuery.Text),
				FormatHelper.CleanString(this.txtSSCode.Text),
				OQCBeginDate,OQCBeginTime,
				OQCEndDate,OQCEndTime
				);
		}

		#endregion

		private void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			//this.gridFTHelper.RequestData();
			//this.gridSNHelper.RequestData();
		}

		private void grdFT_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if( e.Cell.Column.Key.ToUpper() == "TESTDATA" )
			{
				string url = this.MakeRedirectUrl(
					"FOQCFuncValueDataQP.aspx",
					new string[]{
									"LotNo",
									"LotSeq",
									"RCard",
									"RCardSeq",
									"BackUrl"
								},
					new string[]{
									e.Cell.Row.Cells.FromKey("LotNo").Text,//this.txtLotNo.Value,
									e.Cell.Row.Cells.FromKey("LotSeq").Text,//this.txtLotSeq.Value,
									e.Cell.Row.Cells.FromKey("RCard").Text,
									e.Cell.Row.Cells.FromKey("RCardSeq").Text,
									"FOQCTestDataQP.aspx"
								});

				this.Response.Redirect(url,true);

			}
		}

		private void gridSN_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if( e.Cell.Column.Key.ToUpper() == "TESTDATA" )
			{
				string url = this.MakeRedirectUrl(
					"FOQCFuncValueDataQP.aspx",
					new string[]{
									"LotNo",
									"LotSeq",
									"RCard",
									"RCardSeq",
									"BackUrl"
								},
					new string[]{
									e.Cell.Row.Cells.FromKey("LotNo").Text,//this.txtLotNo.Value,
									e.Cell.Row.Cells.FromKey("LotSeq").Text,//this.txtLotSeq.Value,
									e.Cell.Row.Cells.FromKey("RCard").Text,
									e.Cell.Row.Cells.FromKey("RCardSeq").Text,
									"FOQCTestDataQP.aspx"
								});

				this.Response.Redirect(url,true);

			}
		}

		
	}
}
