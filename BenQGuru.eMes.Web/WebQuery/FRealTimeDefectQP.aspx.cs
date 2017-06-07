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
	/// FRealTimeDefectQP 的摘要说明。
	/// </summary>
	public partial class FRealTimeDefectQP : BaseQPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Timers.Timer timerRefresh;


		//protected GridHelper gridHelper = null;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{		
			this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);	

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.eMESDate1.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));

                this.txtSegmentCodeQuery.Readonly = false;
                this.txtSegmentCodeQuery.CanKeyIn = false;

				this._initialWebGrid( null );

				this.RefreshController1.Interval = ConfigSection.Current.DomainSetting.Interval;

				factory_load();
			}

			if( this.V_StartRefresh )
			{
				this._doQuery();
			}
		}

		private void _initialWebGrid(object[] source)
		{
			this.gridWebGrid.Columns.Clear();
            base.InitWebGrid();
			this.gridHelper.AddColumn("StepSequenceCode","生产线",null);
			//this.gridWebGrid.Bands[0].Columns.FromKey("StepSequenceCode").Width  =  new Unit(150);
			this.gridHelper.AddColumn("ItemCode","产品代码",null);
			#region		变为纵向显示	Test

			this.gridHelper.AddColumn("ErrorCodeGroup" ,this.languageComponent1.GetString("ErrorCodeGroupA")+":"+this.languageComponent1.GetString("ErrorCodeA"),null);
			this.gridHelper.AddColumn("ErrorCodeGroupQuantity",this.languageComponent1.GetString("DefectQuantity"),null);

			this.gridHelper.AddColumn("InputQuantity","投入总数",null);
			this.gridHelper.AddColumn("DefectYield","缺陷不良率",null);
			//this.gridHelper.Grid.Columns.FromKey("DefectYield").DataType = typeof(System.Decimal).ToString();
			//this.gridHelper.Grid.Columns.FromKey("DefectYield").Format = ".##%";
			//this.gridWebGrid.Bands[0].Columns.FromKey("StepSequenceCode").MergeCells = true;
			//this.gridWebGrid.Bands[0].Columns.FromKey("ItemCode").MergeCells = true;
			#endregion

			#region 根据排名显示不良代码组和缺陷数量
//			int num = this.V_Top;
//			if(num > 0)
//			{
//				for(int i=0;i<num;i++)
//				{
//					this.gridHelper.AddColumn("ErrorCodeGroup"+i ,this.languageComponent1.GetString("ErrorCodeGroupA")+":"+this.languageComponent1.GetString("ErrorCodeA"),null);
//					this.gridHelper.AddColumn("ErrorCodeGroupQuantity"+i,this.languageComponent1.GetString("DefectQuantity"),null);
//				}
//			}

			#endregion

			#region 根据来源数据的ErrorCode不同自动添加Column (已经实现和测试通过,但是暂时不用这样处理)
//			if( source != null )
//			{
//				foreach(RealTimeDefect real in source)
//				{	
//					if( !this._isColumnKeyUsed( real.ErrorCode + "-" + real.ErrorCodeGroup ) )
//					{
//						this.gridHelper.AddColumn(real.ErrorCode + "-" + real.ErrorCodeGroup ,this.languageComponent1.GetString("ErrorCodeGroupA")+":"+this.languageComponent1.GetString("ErrorCodeA"),null);
//					}
//					if( !this._isColumnKeyUsed( real.ErrorCode + "-" + real.ErrorCodeGroup + "-Quantity" ) )
//					{
//						this.gridHelper.AddColumn(real.ErrorCode + "-" + real.ErrorCodeGroup + "-Quantity",this.languageComponent1.GetString("DefectQuantity"),null);
//					}
//				}
//			}
			#endregion

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

        //private bool _isColumnKeyUsed(string key)
        //{
        //    foreach(UltraGridColumn col in this.gridWebGrid.Columns)
        //    {
        //        if( col.Key.ToUpper() == key.ToUpper() )
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

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
			this.timerRefresh = new System.Timers.Timer();
			((System.ComponentModel.ISupportInitialize)(this.timerRefresh)).BeginInit();

            this.txtSegmentCodeQuery.TextBox.TextChanged += new EventHandler(txtSegmentCodeQuery_TextChanged);

			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";
			// 
			// timerRefresh
			// 
			this.timerRefresh.Elapsed += new System.Timers.ElapsedEventHandler(this.timerRefresh_Elapsed);
			((System.ComponentModel.ISupportInitialize)(this.timerRefresh)).EndInit();

		}
		#endregion

        protected void txtSegmentCodeQuery_TextChanged(object sender, EventArgs e)
		{
			this.drpShiftQuery.Items.Clear();

            this.txtStepSequence.Segment = this.txtSegmentCodeQuery.Text;

            if (this.txtSegmentCodeQuery.Text != "")
			{
				BaseModelFacade facade = new FacadeFactory(base.DataProvider).CreateBaseModelFacade();
                Segment segment = facade.GetSegment(this.txtSegmentCodeQuery.Text) as Segment;
				if( segment != null )
				{
                    object[] shifts = new FacadeFactory(base.DataProvider).CreateShfitModelFacade().QueryShiftBySegment("", segment.SegmentCode, 0, System.Int32.MaxValue);
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


		private object[] _loadDataSource()
		{
			return new FacadeFactory(base.DataProvider).CreateQueryFacade1().QueryRealTimeDefect(
				this.V_SegmentCode,
				this.V_StepSequenceCode,
				this.V_ModelCode,
				this.V_ItemCode,
				this.V_MoCode,
				this.drpFactory.SelectedValue,
				this.V_ShiftCode,
				FormatHelper.TODateInt(this.V_Date),
				this.V_Top);
		}


		private void _processDataDourceToGrid(object[] source)
		{
//			this._initialWebGrid( source );
			this.gridWebGrid.Rows.Clear();
			int num = this.V_Top;

			if( source != null )
			{
				foreach(RealTimeDefect real in source)
				{

                    //UltraGridRow gridRow = null;
                    //object[] objs = new object[this.gridWebGrid.Columns.Count];
                    //gridRow = new UltraGridRow( objs );
                    GridRecord gridRow = new GridRecord();
					this.gridWebGrid.Rows.Add( gridRow );
					gridRow.Items.FindItemByKey("StepSequenceCode").Text = real.StepSequenceCode;
					gridRow.Items.FindItemByKey("ItemCode").Text = real.ItemCode;
					gridRow.Items.FindItemByKey("ErrorCodeGroup").Text = real.ErrorCodeGroup+" "+ real.ECGDESC + ":" + real.ErrorCode+" "+real.ECDESC;
					gridRow.Items.FindItemByKey("ErrorCodeGroupQuantity").Text = real.DefectQuantity.ToString();
					gridRow.Items.FindItemByKey("InputQuantity").Text = real.InputQty.ToString();
					if(real.InputQty!=0)
					{
						decimal defectYield = (decimal)real.DefectQuantity/(decimal)real.InputQty ;
						gridRow.Items.FindItemByKey("DefectYield").Value = defectYield ;
					}
				}
			}

		}


		private bool _checkRequireFields()
		{
			PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblSegment, this.txtSegmentCodeQuery, 40, true));
			manager.Add( new LengthCheck(this.lblShip,this.drpShiftQuery,40,true) );
			manager.Add( new DateCheck(this.lblDate,this.eMESDate1.Text,true) );
			manager.Add( new NumberCheck(this.lblTop,this.txtTop,0,System.Int32.MaxValue,true) );
			manager.Add( new DateRangeCheck(this.lblDate,this.eMESDate1.Text,this.lblToday,FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now)),0,ConfigSection.Current.DomainSetting.MaxDateRange,true));
			
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
                //Check Segment
                BaseModelFacade baseModel = new BaseModelFacade(this.DataProvider);
                object segment = baseModel.GetSegment(FormatHelper.PKCapitalFormat(this.txtSegmentCodeQuery.Text.Trim()));

                if (segment == null)
                {
                    throw new Exception("$Error_CS_Current_Segment_Not_Exist");
                }

				this._processDataDourceToGrid( this._loadDataSource() );

				this.V_StartRefresh = this.chbRefreshAuto.Checked;
			}
			else
			{
				this.chbRefreshAuto.Checked = false;
//				this.RefreshController1.Stop();
				this.V_StartRefresh = false;
			}


		}


		#region ViewState		

		private int V_Top
		{
			get
			{	
				try
				{
					return System.Int32.Parse( this.txtTop.Text );
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				this.ViewState["V_Top"] = value;
			}
		}

		
		private string V_SegmentCode
		{
			get
			{
                return this.txtSegmentCodeQuery.Text;
				//				if( this.ViewState["V_SegmentCode"] != null )
				//				{
				//					return this.ViewState["V_SegmentCode"].ToString();
				//				}
				//				else
				//				{
				//					return "";
				//				}
			}
			set
			{
				this.ViewState["V_SegmentCode"] = value;
			}
		}

		public bool V_StartRefresh
		{
			get
			{
				if( this.Session["V_StartRefresh"] != null )
				{
					try
					{
						return System.Boolean.Parse( this.Session["V_StartRefresh"].ToString() );
					}
					catch
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			set
			{
				this.Session["V_StartRefresh"] = value.ToString();
				if( value )
				{
					this.RefreshController1.Start();
				}
				else
				{
					this.RefreshController1.Stop();
				}
			}
		}

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
			this._doQuery() ;
			this.MergeCells() ;
		}


		private void timerRefresh_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			//			if( this.V_StartRefresh )
			//			{
			//				this.Response.AddHeader("Refresh","2");
			//			}
		}

		
		protected void chkRefreshAuto_CheckedChanged(object sender, System.EventArgs e)
		{
			this._doQuery();
		}
		private void factory_load()
		{
			if(!Page.IsPostBack)
			{
				WarehouseFacade whFacade = new WarehouseFacade(base.DataProvider);
				object[]  factorys  = whFacade.GetAllFactory();
				if( factorys != null )
				{
					foreach( BenQGuru.eMES.Domain.Warehouse.Factory _factory in factorys)
					{
						this.drpFactory.Items.Add( _factory.FactoryCode ) ;
					}
					new DropDownListBuilder( this.drpFactory ).AddAllItem( this.languageComponent1 ) ;
				}
			}
		}

		protected void cmdGridExport2_ServerClick(object sender, EventArgs e)
		{
			this.GridExport(this.gridWebGrid);
		}

		private void MergeCells()
		{
			if( this.gridWebGrid == null )
			{
				return ;
			}

			if( this.gridWebGrid.Rows.Count == 0 )
			{
				return ;
			}

			/* 首先合并产线 */
            //for( int i=0; i<this.gridWebGrid.Rows.Count-1; i++ )
            //{
            //    GridRecord row = this.gridWebGrid.Rows[i];
            //    for( int j=i+1; j<this.gridWebGrid.Rows.Count; j++ )
            //    {
            //        GridRecord row2 = this.gridWebGrid.Rows[j];
            //        if( string.Compare( row.Items.FindItemByKey("StepSequenceCode").Text,row2.Items.FindItemByKey("StepSequenceCode").Text,true )==0 )
            //        {
            //            //row.Items.FindItemByKey("StepSequenceCode").RowSpan += 1;
            //        }
            //        else
            //        {
            //            i=j-1;
            //            break;
            //        }
            //    }
            //}

			/* 根据产线的合并，合并产品 */
            //for( int i=0; i<this.gridWebGrid.Rows.Count; i++ )
            //{
            //    GridRecord row = this.gridWebGrid.Rows[i];
            //    int rowSpan = row.Items.FindItemByKey("StepSequenceCode").RowSpan ;
            //    for( int j=i+1; j<i+rowSpan; j++ )
            //    {
            //        GridRecord row2 = this.gridWebGrid.Rows[j];
            //        if( string.Compare( row.Items.FindItemByKey("ItemCode").Text,row2.Items.FindItemByKey("ItemCode").Text,true)==0 )
            //        {
            //            //row.Items.FindItemByKey("ItemCode").RowSpan += 1;
            //        }
            //        else
            //        {
            //            row = this.gridWebGrid.Rows[j];
            //        }
            //    }
            //    i = i+rowSpan-1;
            //}

		}
	}
}
