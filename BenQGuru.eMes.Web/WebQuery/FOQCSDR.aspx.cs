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


namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FOQCSDR 的摘要说明。
	/// </summary>
	public partial class FOQCSDR : BaseQPage
	{
		protected System.Web.UI.WebControls.Label lblOQCLotQuery;
		protected System.Web.UI.WebControls.TextBox txtOQCLotQuery;

		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected GridHelper _gridHelper = null;
		protected System.Web.UI.WebControls.Label lblStartSNQuery;
		protected System.Web.UI.WebControls.TextBox txtStartSNQuery;
		protected System.Web.UI.WebControls.Label lblEndSNQuery;
		protected System.Web.UI.WebControls.TextBox txtEndSNQuery;
		protected System.Web.UI.WebControls.Label lblSoftwareNameQuery;
		protected System.Web.UI.WebControls.TextBox txtSoftwareNameQuery;
		protected System.Web.UI.WebControls.Label lblSoftwareVersionQuery;
		protected System.Web.UI.WebControls.Label lblStepSequenceConditionQuery;
		protected BenQGuru.eMES.Web.SelectQuery.SelectableTextBox txtConditionStepSequence;
		protected System.Web.UI.WebControls.Label lblStartSnQuery;
		protected System.Web.UI.WebControls.TextBox txtStartSnQuery;
		protected System.Web.UI.WebControls.Label lblEndSnQuery;
		protected System.Web.UI.WebControls.TextBox txtEndSnQuery;
		protected System.Web.UI.WebControls.TextBox txtSoftwareVersionQuery;

		protected BenQGuru.eMES.Web.UserControl.eMESTime txtOQCBeginTime;
		protected BenQGuru.eMES.Web.UserControl.eMESTime txtOQCEndTime;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{

			this._gridHelper = new GridHelper(this.gridWebGrid);

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();

				txtOQCBeginDate.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				txtOQCEndDate.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );

				this.txtOQCBeginTime.Text = FormatHelper.ToTimeString( 0 ) ;
				this.txtOQCEndTime.Text = FormatHelper.ToTimeString(235959);
			}

			this._helper = new WebQueryHelper( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);		

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

		private void _initialWebGrid()
		{
			//C----OQC 检验样品数中的不良产品数量 , D----OQC 检验的样品数 , SDR=C/D*1000000
			this._gridHelper.AddColumn("MODELCODE",			"产品别代码",null);
			this._gridHelper.AddColumn("ITEMCODE",			"产品代码",null);
			this._gridHelper.AddColumn("MOCODE",			"工单代码",null);
//			if(this.drpDateGroup.SelectedValue == "MDATE")
//			{
//				this._gridHelper.AddColumn("DATEGROUPDay",			"天",null);
//			}
//			else if(this.drpDateGroup.SelectedValue == "WEEK")
//			{
//				this._gridHelper.AddColumn("DATEGROUPWeek",			"周",null);
//			}
//			else if(this.drpDateGroup.SelectedValue == "MONTH")
//			{
//				this._gridHelper.AddColumn("DATEGROUPMonth",			"月",null);
//			}
//			else
//			{
				this._gridHelper.AddColumn("DATEGROUP",			"日期",null);
//			}
			this._gridHelper.AddColumn("SAMPLECOUNT",		"样品总数",null);
			this._gridHelper.AddColumn("SAMPLENGCOUNT",		"样品不良总数",null);
			this._gridHelper.AddColumn("SDR",				"SDR",null);

			//多语言
			this._gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new DateRangeCheck(this.lblOQCBegindate, this.txtOQCBeginDate.Text, this.txtOQCEndDate.Text, false) );

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return;
			}

			this.QueryEvent(sender,e);
		}

		#region 查询事件

		private void QueryEvent(object sender, EventArgs e)
		{
			int OQCBeginDate = FormatHelper.TODateInt(this.txtOQCBeginDate.Text);
			int OQCEndDate = FormatHelper.TODateInt(this.txtOQCEndDate.Text);

			int OQCBeginTime = FormatHelper.TOTimeInt(this.txtOQCBeginTime.Text);
			int OQCEndTime = FormatHelper.TOTimeInt(this.txtOQCEndTime.Text);

			BenQGuru.eMES.WebQuery.QueryFacade2 qfacade = new BenQGuru.eMES.WebQuery.QueryFacade2(base.DataProvider);

			object[] dataSource = qfacade.QueryOQCSDR(
				FormatHelper.CleanString(this.txtConditionModel.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
				FormatHelper.CleanString(this.drpDateGroup.SelectedValue),
				FormatHelper.CleanString(this.txtSSCode.Text).ToUpper(),
				OQCBeginDate,OQCBeginTime,
				OQCEndDate,OQCEndTime);

			( e as WebQueryEventArgs ).GridDataSource = dataSource;				

			( e as WebQueryEventArgs ).RowCount = 0;
			if(( e as WebQueryEventArgs ).GridDataSource != null)
			{
				( e as WebQueryEventArgs ).RowCount =  ( e as WebQueryEventArgs ).GridDataSource.Length;
			}

			this._processOWC( dataSource );
		}

		//导出事件
		private void ExportQueryEvent(object sender, EventArgs e)
		{
			this.QueryEvent(sender,e);
		}

		private void _processOWC(object[] dataSource)
		{
            //update by Seven  2011-01-12
            if (dataSource != null)
            {
                this.lineChart.Visible = true;

                NewReportDomainObject[] dateSourceForOWC = new NewReportDomainObject[dataSource.Length];
                NewReportDomainObject item;
                for (int i = 0; i < dataSource.Length; i++)
                {
                    item = new NewReportDomainObject();
                    item.EAttribute1 = "SDR";
                    item.ItemCode = (dataSource[i] as OQCSDR).ItemCode.ToString();
                    item.PeriodCode = (dataSource[i] as OQCSDR).DateGroup.ToString();
                    item.TempValue = (dataSource[i] as OQCSDR).SDR.ToString();
                    dateSourceForOWC[i] = item;
                }

                this.lineChart.ChartGroupByString = "itemcode";
                this.lineChart.ChartTextFormatString = "<DATA_VALUE:0>";
                this.lineChart.YLabelFormatString = "<DATA_VALUE:0>";
                this.lineChart.DataType = true;
                this.lineChart.DataSource = dateSourceForOWC;
                this.lineChart.DataBind();
            }
            else
            {
                this.lineChart.Visible = false;
            }
            //end
            
		}


		#endregion


		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				OQCSDR obj = ( e as DomainObjectToGridRowEventArgs ).DomainObject as OQCSDR;

				string dataGroup = "";
				if(this.drpDateGroup.SelectedValue == "MDATE")
				{
					dataGroup = FormatHelper.ToDateString( obj.DateGroup );
				}
				else if(this.drpDateGroup.SelectedValue == "WEEK")
				{
					dataGroup = obj.DateGroup.ToString().Insert(4, "W");
				}
				else if(this.drpDateGroup.SelectedValue == "MONTH")
				{
					dataGroup = obj.DateGroup.ToString().Insert(4, "M");
				}
				else
				{
					dataGroup = FormatHelper.ToDateString( obj.DateGroup );
				}

				( e as DomainObjectToGridRowEventArgs ).GridRow = 
					new UltraGridRow( new object[]{
													  obj.ModelCode,
													  obj.ItemCode,
													  obj.MOCode,
													  dataGroup,
													  obj.SampleCount.ToString(),
													  obj.SampleNGCount.ToString(),
													  obj.SDR.ToString()
												  }
					);
			}
		}

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				OQCSDR obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as OQCSDR;

				string dataGroup = "";
				if(this.drpDateGroup.SelectedValue == "MDATE")
				{
					dataGroup = FormatHelper.ToDateString( obj.DateGroup );
				}
				else if(this.drpDateGroup.SelectedValue == "WEEK")
				{
					dataGroup = obj.DateGroup.ToString().Insert(4, "W");
				}
				else if(this.drpDateGroup.SelectedValue == "MONTH")
				{
					dataGroup = obj.DateGroup.ToString().Insert(4, "M");
				}
				else
				{
					dataGroup = FormatHelper.ToDateString( obj.DateGroup );
				}

				( e as DomainObjectToExportRowEventArgs ).ExportRow = 
					new string[]{
									obj.ModelCode,
									obj.ItemCode,
									obj.MOCode,
									dataGroup,
									obj.SampleCount.ToString(),
									obj.SampleNGCount.ToString(),
									obj.SDR.ToString()
								};
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			if(this.drpDateGroup.SelectedValue == "MDATE")
			{
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"MODELCODE",
									"ITEMCODE",
									"MOCODE",
									"天",
									"样品总数",
									"样品不良总数",
									"SDR"
								};
			}
			else if(this.drpDateGroup.SelectedValue == "WEEK")
			{
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"MODELCODE",
									"ITEMCODE",
									"MOCODE",
									"周",
									"样品总数",
									"样品不良总数",
									"SDR"
								};
			}
			else if(this.drpDateGroup.SelectedValue == "MONTH")
			{
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"MODELCODE",
									"ITEMCODE",
									"MOCODE",
									"月",
									"样品总数",
									"样品不良总数",
									"SDR"
								};
			}
			else
			{
				( e as ExportHeadEventArgs ).Heads = 
					new string[]{
									"MODELCODE",
									"ITEMCODE",
									"MOCODE",
									"日期",
									"样品总数",
									"样品不良总数",
									"SDR"
								};
			}
		}			
	}
}
