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
using System.IO;
using System.Text;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.Common.Config;
using BenQGuru.eMES.Material;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FRealTimeQuantitySummaryQP 的摘要说明。
	/// </summary>
	public partial class FRealTimeResECQP : BaseQPageNew
	{
		protected System.Web.UI.WebControls.Label lblStartDateQuery;
		protected System.Web.UI.WebControls.Label lblEndDateQuery;

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		/// <summary>
		/// 生产线
		/// </summary>
		protected BenQGuru.eMES.Web.SelectQuery.SelectableTextBox txtStepSequence;


		//protected GridHelper gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{		
			this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.eMESDate1.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));				

				this._initialWebGrid();			
			}	
		}


		private void _initialWebGrid()
		{
			this.gridWebGrid.Columns.Clear();
            base.InitWebGrid();
			this.gridHelper.AddColumn("Sequence","序号",100);
			this.gridHelper.AddColumn("ErrorSymptom","不良现象",100);
			
			string selected = "";
			if( this.drpShiftQuery.SelectedValue == "" )
			{
				this.gridHelper.AddColumn("TPCode","时段代码",null);
			}
			else
			{	
				object[] tps = new FacadeFactory(base.DataProvider).CreateShfitModelFacade().GetTimePeriodByShiftCode(this.drpShiftQuery.SelectedValue);
				if( tps != null )
				{
					int now = FormatHelper.TOTimeInt(System.DateTime.Now);

					int count = tps.Length;
					//foreach(TimePeriod tp in tps)
					for( int i=0; i<count; i++ )
					{
						TimePeriod tp = tps[i] as TimePeriod;
						string key = tp.TimePeriodCode;

						this.gridHelper.AddColumn(key,FormatHelper.ToShortTimeString(tp.TimePeriodBeginTime) + "<br>" + FormatHelper.ToShortTimeString(tp.TimePeriodEndTime),null);

						if( tp.TimePeriodBeginTime <= now &&
							tp.TimePeriodEndTime >= now )
						{
							selected = tp.TimePeriodCode;
						}						
					}						
				}
				else
				{
					this.gridHelper.AddColumn("TPCode","时段代码",null);
				}					
			}

            //GridItemStyle blueBack = new GridItemStyle(true);
            //blueBack.BackColor = Color.SkyBlue;
            //if( this.gridWebGrid.Columns.FromKey( selected ) != null )
            //{
            //    this.gridWebGrid.Columns.FromKey(selected).HeaderStyle.BackColor = Color.SkyBlue;
            //}

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
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

		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRescode2EC(
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtResCode.Text)),
				FormatHelper.TODateInt( this.eMESDate1.Text),
				FormatHelper.PKCapitalFormat(this.drpShiftQuery.SelectedValue),
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtStepSequence.Text)),
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtModelQuery.Text)),
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtItemQuery.Text)),
				FormatHelper.PKCapitalFormat( FormatHelper.CleanString(this.txtMoQuery.Text)));
		}


		private void _processDataDourceToGrid(object[] source)
		{
			this._initialWebGrid();

			this.gridWebGrid.Rows.Clear();

			if( source != null )
			{				
				int i=1;
				foreach(QDORes2EC obj in source)
				{
					bool isAdded = false;
					GridRecord gridRow = null;

                    foreach (GridRecord row in this.gridWebGrid.Rows)
					{
						if( string.Compare( row.Items.FindItemByKey("ErrorSymptom").Text, obj.ErrorCodeDesc, true)==0 )
						{
							isAdded = true;
							gridRow = row;
						}
						 
						if(gridRow!=null)
							break;
					}

					if( !isAdded || gridRow == null )
					{
                        //object[] objs = new object[this.gridWebGrid.Columns.Count];
                        //gridRow = new UltraGridRow( objs );
                        gridRow = new GridRecord();
						this.gridWebGrid.Rows.Add( gridRow );
						
						gridRow.Items.FindItemByKey("Sequence").Text = i.ToString();
						gridRow.Items.FindItemByKey("ErrorSymptom").Text = obj.ErrorCodeDesc;
						i++;
					}

					gridRow.Items.FindItemByKey(obj.TPCode).Text = obj.NGTimes.ToString() ;				
				}
			}

			this._processGridStyle();
		}

		private void _processGridStyle()
		{
			try
			{
                //GridItemStyle style = new GridItemStyle(true);
                //style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
                //for(int col=1;col < this.gridWebGrid.Columns.Count-1;col++)
                //{			
                //    for(int row=0;row<this.gridWebGrid.Rows.Count-1;row++)
                //    {
                //        this.gridWebGrid.Rows[row].Cells[col].Style.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
                //    }
                //}
			}
			catch
			{
			}
		}


		private bool _checkRequireFields()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblShip,this.drpShiftQuery,40,true) );
			manager.Add( new DateCheck(this.lblDate,this.eMESDate1.Text,true) );
			
			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);

				return false;
			}
			else
			{
				return true;
			}
		}


		private void _doQuery()
		{
			if( this._checkRequireFields() )
			{
				this._initialWebGrid();

				this._processDataDourceToGrid( this._loadDataSource() );
			}
		}


		#region ViewState
	
		private string V_StepSequenceCode
		{
			get
			{	
				return this.txtStepSequence.Text;
//				if( this.ViewState["V_StepSequenceCode"] != null )
//				{
//					return this.ViewState["V_StepSequenceCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_StepSequenceCode"] = value;
			}
		}

		private string V_ShiftCode
		{
			get
			{	
				return this.drpShiftQuery.SelectedValue;
//				if( this.ViewState["V_ShiftCode"] != null )
//				{
//					return this.ViewState["V_ShiftCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_ShiftCode"] = value;
			}
		}

		private string V_Date
		{
			get
			{	
				return this.eMESDate1.Text;
//				if( this.ViewState["V_Date"] != null )
//				{
//					return this.ViewState["V_Date"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_Date"] = value;
			}
		}

		private string V_ModelCode
		{
			get
			{	
				return this.txtModelQuery.Text;
//				if( this.ViewState["V_ModelCode"] != null )
//				{
//					return this.ViewState["V_ModelCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_ModelCode"] = value;
			}
		}

		private string V_ItemCode
		{
			get
			{		
				return this.txtItemQuery.Text;
//				if( this.ViewState["V_ItemCode"] != null )
//				{
//					return this.ViewState["V_ItemCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_ItemCode"] = value;
			}
		}

		private string V_MoCode
		{
			get
			{	
				return this.txtMoQuery.Text;
//				if( this.ViewState["V_MoCode"] != null )
//				{
//					return this.ViewState["V_MoCode"].ToString();
//				}
//				else
//				{
//					return "";
//				}
			}
			set
			{
				this.ViewState["V_MoCode"] = value;
			}
		}
		#endregion

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{	
			this._doQuery();
		}


		
		private void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
		{
//			this.V_StartRefresh = this.chkRefreshAuto.Checked;	
			this._doQuery();
		}
		

		protected void cmdGridExport2_ServerClick(object sender, EventArgs e)
		{
			this.GridExport(this.gridWebGrid);
		}

		protected void drpShiftQuery_Load(object sender, EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				this.drpShiftQuery.Items.Clear();

				BaseModelFacade facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade();	
				
				object[] shifts = new FacadeFactory(base.DataProvider).CreateShfitModelFacade().QueryShift("","",0,System.Int32.MaxValue);
				if( shifts != null )
				{
					string selected = "";
					int now = FormatHelper.TODateInt(System.DateTime.Now);

					foreach(Shift shift in shifts)
					{
						if( shift.ShiftBeginTime <= now &&
							shift.ShiftEndTime >= now )
						{
							selected = shift.ShiftCode;
						}
						this.drpShiftQuery.Items.Add( shift.ShiftCode );
					}
					try
					{
						this.drpShiftQuery.SelectedValue = selected;
					}
					catch
					{
						this.drpShiftQuery.SelectedIndex = 0;
					}
				}
				
			}
		}
	}
}
