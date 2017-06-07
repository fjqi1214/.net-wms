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

using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Web.UserControl ;
using BenQGuru.eMES.WebQuery ;
using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FOQCFuncValueDataQP 的摘要说明。
	/// </summary>
	public partial class FOQCFuncValueDataQP : BaseMPage
	{
	
		private System.ComponentModel.IContainer components;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private QueryOQCFunctionFacade _facade = null ;
		
		protected GridHelper gridSNHelper ;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.gridSNHelper = new GridHelper(this.gridSN) ;

			if(!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				string rcard = this.GetRequestParam("RCARD") ;
				this.txtSN.Value = rcard;
				this.txtSeq.Value = this.GetRequestParam("RCardSeq");	
				this.txtLotNo.Value = this.GetRequestParam("LotNo");
				this.txtLotSeq.Value = this.GetRequestParam("LotSeq");

				GetElectricCount();
				this.gridSNHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSourceSN);
				this.gridSNHelper.GetRowCountHandle = new GetRowCountDelegate(this.GetRowCountSN);
				this.gridSNHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRowSN);	

				InitWebGridSN() ;
			}
		}


		private int electriccount = 0;

		private int GetElectricCount()
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

			OQCFuncTestValue obj = (OQCFuncTestValue)this._facade.GetOQCFuncTestValue(this.txtSN.Value ,seq,
				this.txtLotNo.Value,this.txtLotSeq.Value);

			if(obj != null)
			{
				electriccount = Convert.ToInt32(obj.ElectricTestCount);
			}
			else
			{
				electriccount = 0;
			}

			return electriccount;
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
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

		#region WebGrid
		protected override void InitWebGrid()
		{
			//this.gridHelper.AddColumn("RCard","产品序列号",null) ;
			//this.gridHelper.AddColumn( "RcardSeq", "RcardSeq",	null);
			this.gridHelper.AddColumn( "GroupSeq1", "组别",	null);
			this.gridHelper.AddColumn( "Freq", "频率",	null);
			this.gridHelper.AddColumn( "Electric", "电流标准值",	null);

			for(int i = 1; i <= electriccount ; i++ )
				this.gridHelper.AddColumn( "ElectricValue"+i.ToString(), "电流值"+i.ToString(),	null);

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
 
			base.InitWebGrid();

			this.gridHelper.RequestData();

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

		private Hashtable htSpec = null;
		private Hashtable htEleSeq = null;
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			OQCFuncTestValueDetail testValue = obj as OQCFuncTestValueDetail;

			object[] objs = new object[3+this.electriccount];

			objs[0] = testValue.GroupSequence;
			objs[1] = testValue.FreMin.ToString("0.00")+"/"+testValue.FreMax.ToString("0.00")+"/"+testValue.FreValue.ToString("0.00");
			objs[2] = testValue.ElectricMin.ToString("0.00")+"/"+testValue.ElectricMax.ToString("0.00");//+"/"+testValue.ElectricValue.ToString("0.00");

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
					OQCFuncTestValueEleDetail d = (OQCFuncTestValueEleDetail)details[iStartIdx + i];
					objs[i + 3] = d.ElectricValue.ToString("0.00");
				}
				else
				{
					objs[i + 3] = "";
				}
			}
			htEleSeq[strKey] = iStartIdx + electriccount;
			/*
			BenQGuru.eMES.SPCDataCenter.DataEntry dataEntry = (BenQGuru.eMES.SPCDataCenter.DataEntry)obj;
			if (htSpec == null)
			{
				htSpec = new Hashtable();
				string strSql = "SELECT * FROM tblOQCFuncTestSpec WHERE ItemCode='" + dataEntry.ItemCode + "'";
				object[] objsSpec = this.DataProvider.CustomQuery(typeof(OQCFuncTestSpec), new BenQGuru.eMES.Common.Domain.SQLCondition(strSql));
				if (objsSpec != null)
				{
					for (int i = 0; i < objsSpec.Length; i++)
					{
						OQCFuncTestSpec spec = (OQCFuncTestSpec)objsSpec[i];
						if (htSpec.ContainsKey(Convert.ToInt32(spec.GroupSequence)) == false)
						{
							htSpec.Add(Convert.ToInt32(spec.GroupSequence), spec);
						}
					}
				}
			}
			int iGroupCount = 0;
			for (int i = 0; i < dataEntry.ListTestData.Count; i++)
			{
				BenQGuru.eMES.SPCDataCenter.DataEntryTestData testData = (BenQGuru.eMES.SPCDataCenter.DataEntryTestData)dataEntry.ListTestData[i];
				if (iGroupCount < testData.GroupSequence)
					iGroupCount = Convert.ToInt32(testData.GroupSequence);
			}
			this.pagerToolBar.RowCount = this.pagerToolBar.RowCount - 1 + iGroupCount;
			object[] objs = null;
			for (int i = 1; i <= iGroupCount; i++)
			{
				OQCFuncTestSpec spec = new OQCFuncTestSpec();
				if (htSpec.ContainsKey(i) == true)
					spec = (OQCFuncTestSpec)htSpec[i];
				objs = new object[3 + this.electriccount];
				objs[0] = i;
				objs[2] = spec.ElectricMin.ToString("0.00") + "/" + spec.ElectricMax.ToString("0.00");
				for (int n = 0; n < dataEntry.ListTestData.Count; n++)
				{
					BenQGuru.eMES.SPCDataCenter.DataEntryTestData testData = (BenQGuru.eMES.SPCDataCenter.DataEntryTestData)dataEntry.ListTestData[n];
					if (testData.ObjectCode == SPCObjectList.OQC_FT_FREQUENCY && testData.GroupSequence == i)
					{
						objs[1] = spec.FreMin.ToString("0.00") + "/" + spec.FreMax.ToString("0.00") + "/" + Convert.ToDecimal(testData.Data).ToString("0.00");
					}
					if (testData.ObjectCode == SPCObjectList.OQC_FT_ELECTRIC && testData.GroupSequence == i)
					{
						decimal[] dvalue = (decimal[])testData.Data;
						for (int x = 0; x < this.electriccount; x++)
						{
							objs[3 + x] = dvalue[x].ToString("0.00");
						}
					}
				}
				if (i < iGroupCount)
				{
					this.gridWebGrid.Rows.Add(new Infragistics.WebUI.UltraWebGrid.UltraGridRow( objs ));
				}
			}
			*/

			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( objs );
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

		protected override object[] LoadDataSource(int inclusive, int exclusive)
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

			this.EleDetails = this._facade.QueryOQCFuncTestValueEleDetail(this.txtSN.Value ,seq,
				this.txtLotNo.Value,this.txtLotSeq.Value);

			return this._facade.QueryOQCFuncTestValueDetail(
				this.txtSN.Value ,
				seq,this.txtLotNo.Value,this.txtLotSeq.Value,
				inclusive, exclusive );
		}


		protected override int GetRowCount()
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
			return this._facade.QueryOQCFuncTestValueDetailCount(
				this.txtSN.Value,
				seq,this.txtLotNo.Value,this.txtLotSeq.Value
				);
		}

		#endregion

		#region SNGrid
		protected void InitWebGridSN()
		{
//			public class DimentionParam
//			{
//				public static string Length = "Length";
//				public static string Width = "Width";
//				public static string Right2Middle = "Right2Middle";
//				public static string Left2Middle = "Left2Middle";
//				public static string Left2Right = "Left2Right";
//				public static string AllHeight = "AllHeight";
//				public static string BoardHeight = "BoardHeight";
//				public static string Height = "Height";
//			}

			this.gridSNHelper.AddColumn("Length","长度",null) ;
			this.gridSNHelper.AddColumn( "Width", "宽度",	null);
			this.gridSNHelper.AddColumn("BoardHeight","板上高",null) ;
			this.gridSNHelper.AddColumn( "Height", "板厚",	null);
			this.gridSNHelper.AddColumn( "AllHeight", "总高",	null);
			this.gridSNHelper.AddColumn("Left2Right","左右孔距",null) ;
			this.gridSNHelper.AddColumn( "Left2Middle", "左孔到中间孔距",	null);
			this.gridSNHelper.AddColumn("Right2Middle","右孔到中间孔距",null) ;
	
			this.gridSNHelper.RequestData();

			gridSN.Rows.Clear();

			if(this.DimentionValues.Length > 0)
			{
				object[] objs = new object[]{"","","","","","","","",};

				foreach(OQCDimentionValue v in this.DimentionValues)
				{
					int i = 0;

					if(v.ParamName.ToUpper() == DimentionParam.Length.ToUpper())
						i = 0;
					else if(v.ParamName.ToUpper() == DimentionParam.Width.ToUpper())
						i = 1;
					else if(v.ParamName.ToUpper() == DimentionParam.BoardHeight.ToUpper())
						i = 2;
					else if(v.ParamName.ToUpper() == DimentionParam.Height.ToUpper())
						i = 3;
					else if(v.ParamName.ToUpper() == DimentionParam.AllHeight.ToUpper())
						i = 4;
					else if(v.ParamName.ToUpper() == DimentionParam.Left2Right.ToUpper())
						i = 5;
					else if(v.ParamName.ToUpper() == DimentionParam.Left2Middle.ToUpper())
						i = 6;
					else if(v.ParamName.ToUpper() == DimentionParam.Right2Middle.ToUpper())
						i = 7;

					if (objs[i].ToString() != string.Empty)
					{
						this.gridSN.Rows.Add(new Infragistics.WebUI.UltraWebGrid.UltraGridRow(objs));
						objs = new object[]{"","","","","","","","",};
					}
					objs[i] = v.MinValue.ToString("0.00")+"/"+v.MaxValue.ToString("0.00")+"/"+v.ActualValue.ToString("0.00");
				}

				this.gridSN.Rows.Add(new Infragistics.WebUI.UltraWebGrid.UltraGridRow(objs));
			}

			this.gridSNHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRowSN(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								"","","","","","","","",
							}
                
                
				);
		}

		protected object[] LoadDataSourceSN(int inclusive, int exclusive)
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

			object[] objs =  this._facade.QueryOQCDimentionValue(
				this.txtSN.Value ,
				seq,this.txtLotNo.Value,this.txtLotSeq.Value);

			this.DimentionValues = objs;

			return objs;
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
			return this._facade.QueryOQCDimentionValueCount(
				this.txtSN.Value,
				seq,this.txtLotNo.Value,this.txtLotSeq.Value
				);
		}

		#endregion

		#region Export 	
		protected override string[] FormatExportRecord( object obj )
		{
			OQCFuncTestValueDetail testValue = obj as OQCFuncTestValueDetail;

			string[] objs = new string[3+this.electriccount];

			objs[0] = testValue.GroupSequence.ToString();
			objs[1] = testValue.FreMin.ToString("0.00")+"/"+testValue.FreMax.ToString("0.00")+"/"+testValue.FreValue.ToString("0.00");
			objs[2] = testValue.ElectricMin.ToString("0.00")+"/"+testValue.ElectricMax.ToString("0.00");//+"/"+testValue.ElectricValue.ToString("0.00");

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
					OQCFuncTestValueEleDetail d = (OQCFuncTestValueEleDetail)details[iStartIdx + i];
					objs[i + 3] = /*d.ElectricMin.ToString("0.00")+"/"+d.ElectricMax.ToString("0.00")+"/"+*/ d.ElectricValue.ToString("0.00");
				}
				else
				{
					objs[i + 3] = "";
				}
			}
			htEleSeq[strKey] = iStartIdx + electriccount;

			return objs;
		}

		protected override string[] GetColumnHeaderText()
		{
			this.gridHelper.AddColumn( "GroupSeq", "组别",	null);
			this.gridHelper.AddColumn( "Freq", "频率",	null);
			this.gridHelper.AddColumn( "Electric", "电流标准值",	null);

			for(int i = 1; i <= electriccount ; i++ )
				this.gridHelper.AddColumn( "ElectricValue"+i.ToString(), "电流值"+i.ToString(),	null);

			string[] strs = new string[3+this.electriccount];

			strs[0] = "GroupSeq";
			strs[1] = "Freq";
			strs[2] = "Electric";

			for(int i = 1; i <= electriccount ; i++ )
				strs[2+i] = "ElectricValue"+i.ToString();

			return strs;
		}
		#endregion

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
//			string url = this.MakeRedirectUrl(
//				"FOQCFuncValueQP.aspx",
//				new string[]{
//								"LotNo",
//								"LotSeq",
//								"RCard",
//								"RCardSeq",
//								"BackUrl"
//							},
//				new string[]{
//								this.txtLotNo.Value,
//								this.txtLotSeq.Value,
//								this.txtSN.Value,
//								this.txtSeq.Value,
//								"FOQCLotSampleQP.aspx"
//							});

			this.Response.Redirect(this.GetRequestParam("BackUrl"),true);
		}
	}
}
