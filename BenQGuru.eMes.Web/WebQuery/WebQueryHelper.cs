using System;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting ;

namespace BenQGuru.eMES.Web.WebQuery
{	
	/// <summary>
	/// WebQueryHelper 的摘要说明。
	/// </summary>	
	public class WebQueryHelper
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdQuery;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdGridExport;
		protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid gridWebGrid;
		protected BenQGuru.eMES.Web.Helper.PagerSizeSelector pagerSizeSelector;
		protected BenQGuru.eMES.Web.Helper.PagerToolBar pagerToolBar;
		public ExcelExporter excelExporter = null;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		public object[] MergeColumnIndexList = null;

		public event EventHandler LoadGridDataSource;
		public event EventHandler DomainObjectToGridRow;
		public event EventHandler DomainObjectToExportRow;
		public event EventHandler GetExportHeadText;
		public event EventHandler GridCellClick;
		public event EventHandler CheckRequireFields;

		public bool IsInExport = false;		// Added by Icyer 2006/12/26 @ YHI	标识是否导出操作，如果是导出则不用计算总数
		
		public WebQueryHelper(
			HtmlInputButton queryButton,
			HtmlInputButton exportButton,
			UltraWebGrid grid,
			PagerSizeSelector selector,
			PagerToolBar toolBar,
			ControlLibrary.Web.Language.LanguageComponent languageComponent)
		{
			//variable
			this.cmdQuery = queryButton;
			this.cmdGridExport = exportButton;
			this.gridWebGrid = grid;
			this.pagerSizeSelector = selector;
			this.pagerToolBar = toolBar;
			this.languageComponent1 = languageComponent;
			//export
			this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter();
			this.excelExporter.Page = this.gridWebGrid.Page;
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(this.FormatExportRecord);
			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(this.LoadDataSource);
			this.excelExporter.LanguageComponent = this.languageComponent1;	
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);			
			//register event
			if( this.cmdQuery != null )
			{
				this.cmdQuery.ServerClick +=new EventHandler(cmdQuery_ServerClick);
			}
			if( this.cmdGridExport != null )
			{
				this.cmdGridExport.ServerClick +=new EventHandler(cmdGridExport_ServerClick);
			}
			if( this.gridWebGrid != null )
			{
				this.gridWebGrid.ClickCellButton +=new ClickCellButtonEventHandler(gridWebGrid_ClickCellButton);
			}
			if( this.pagerToolBar != null )
			{
				this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
			}
			if(this.pagerSizeSelector != null)
			{
				this.pagerSizeSelector.OnPagerSizeChanged +=new BenQGuru.eMES.Web.Helper.PagerSizeSelector.PagerSizeChangedHandle(pagerSizeSelector_OnPagerSizeChanged);
			}
		}

		#region Query

		private void _processDataSource(object sender,int inclusive,int exclusive,int pageSize)
		{
			WebQueryEventArgs args = new WebQueryEventArgs(inclusive,exclusive);
            this.LoadGridDataSource( sender,args );

			if( args.GridDataSource != null )
			{
				DomainObjectToGridRowEventArgs args1 = new DomainObjectToGridRowEventArgs();
				Infragistics.WebUI.UltraWebGrid.UltraGridRow lastRow = null;

				foreach( DomainObject obj in args.GridDataSource )
				{
					args1.DomainObject = obj;
					this.DomainObjectToGridRow( sender, args1 );

					if( args1.GridRow != null )
					{					
						this.gridWebGrid.Rows.Add( this.buildMergedGridRow( args1.GridRow, lastRow ) );

						lastRow = args1.GridRow;
					}
				}
			}

			if( this.pagerSizeSelector != null )
			{
				this.pagerToolBar.PageSize = pageSize;
			}

			if( this.pagerToolBar != null )
			{
				this.pagerToolBar.RowCount = args.RowCount;				
			}
			
		}

		public void Query(object sender)
		{
			if( this.gridWebGrid != null &&
				this.LoadGridDataSource != null &&
				this.DomainObjectToGridRow != null )
			{
				this.gridWebGrid.Rows.Clear();

				if( this.CheckRequireFields != null )
				{
					this.CheckRequireFields(sender,null);
				}

				this._processDataSource(sender,1,this.pagerSizeSelector.PageSize,this.pagerSizeSelector.PageSize);

				this.pagerToolBar.InitPager();
			}
		}

        private void cmdQuery_ServerClick(object sender, EventArgs e)
		{
			IsInExport = false;		// Added by Icyer 2006/12/25 @ YHI	查询数据操作
			this.Query( sender );
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridWebGrid.Rows.Clear();

			int inclusive = ( this.pagerToolBar.PageIndex - 1 ) * this.pagerToolBar.PageSize + 1;
			int exclusive = inclusive + this.pagerToolBar.PageSize - 1;

			this._processDataSource(sender,inclusive, exclusive,this.pagerToolBar.PageSize);
		}
		#endregion

		#region Export
		private string[] GetColumnHeaderText()
		{
			ExportHeadEventArgs headArgs = new ExportHeadEventArgs();
			this.GetExportHeadText( null, headArgs );

			return headArgs.Heads;
		}

		/// <summary>
		/// 格式化object的各字段成字符串，用于导出数据，需重载
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		private string[] FormatExportRecord( object obj )
		{
			DomainObjectToExportRowEventArgs args = new DomainObjectToExportRowEventArgs();
			args.DomainObject = obj as DomainObject;
			this.DomainObjectToExportRow(this.excelExporter.Page,args);

			return args.ExportRow;
		}

		protected object[] LoadDataSource(int start,int end)
		{
			WebQueryEventArgs args = new WebQueryEventArgs();
			args.StartRow = start;
			args.EndRow = end;

			//this.LoadGridDataSource( this.excelExporter.Page,args );

			this.LoadGridDataSource( this.cmdGridExport,args );

			

			return args.GridDataSource;
		}

		private void cmdGridExport_ServerClick(object sender, EventArgs e)
		{
			if( this.gridWebGrid != null &&
				this.LoadGridDataSource != null &&
				this.GetExportHeadText != null )
			{
				this.IsInExport = true;			// Added by Icyer 2006/12/26 @ YHI	导出操作
				this.excelExporter.Export();
				this.IsInExport = false;		// Added by Icyer 2006/12/26 @ YHI
			}
		}

		#endregion

		#region Grid Click
		private void gridWebGrid_ClickCellButton(object sender, CellEventArgs e)
		{
			if( this.GridCellClick != null )
			{
				GridCellClickEventArgs cellArgs = new GridCellClickEventArgs();			
				cellArgs.Cell = e.Cell;

				this.GridCellClick( sender, cellArgs );
			}
		}	
		#endregion

		/// <summary>
		/// 根据LastAction显示工序结果
		/// </summary>
		/// <param name="languageComponent"></param>
		/// <param name="lastAction"></param>
		/// <param name="runningCard"></param>
		/// <param name="runningCardSequence"></param>
		/// <param name="moCode"></param>
		/// <param name="referedURL"></param>
		/// <returns></returns>
		public static string GetOPResultLinkHtml2(ControlLibrary.Web.Language.ILanguageComponent languageComponent,string lastAction,string runningCard,decimal runningCardSequence,string moCode,string referedURL)
		{
			string html = "" ;
			string tpl = string.Format("<a href=\"{{0}}?REFEREDURL={3}&RCARD={0}&RCARDSEQ={1}&MOCODE={2}&TYPE={{2}}\">{{1}}</a>&nbsp;",runningCard,runningCardSequence,moCode,System.Web.HttpUtility.UrlEncode(referedURL) ) ;
			string tplnoref = string.Format("<a >{{0}}</a>&nbsp;") ;
			try
			{
				string moduleName = ActionType.GetOperationResultModule(lastAction);
				switch(moduleName)
				{
					case "Testing":
						html += string.Format( tpl,"FITOPResultTestingQP.aspx", languageComponent.GetString("ItemTracing_testing"),moduleName );
						break;
					case "ComponentLoading":
						html += string.Format( tpl,"FITOPResultComploadingQP.aspx", languageComponent.GetString("ItemTracing_componentloading") ,moduleName);
						break;
					case "IDTranslation":
						html += string.Format( tpl,"FITOPResultSNQP.aspx", languageComponent.GetString("ItemTracing_sn") ,moduleName);
						break;
					case "Packing":
						html += string.Format( tpl,"FITOPResultPackingQP.aspx", languageComponent.GetString("ItemTracing_packing") ,"ItemTracing_packing");
						break;
					case "UnPacking":
						html += string.Format( tpl,"FITOPResultPackingQP.aspx", languageComponent.GetString("ItemTracing_unpacking") ,"ItemTracing_unpacking");
						break;
					case "TS":
						html += string.Format( tpl,"FITOPResultTSQP.aspx", languageComponent.GetString("ItemTracing_ts") ,moduleName);
						break;
					case "OQC":
						html += string.Format( tpl,"FITOPResultFQCQP.aspx", languageComponent.GetString("ItemTracing_oqc") ,moduleName);
						break;
					//以下非工序结果 "Reject", "GoMO","ECN","SoftINFO","TRY"
					case "Reject":
						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_Reject") );
						break;
					case "GoMO":
						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_GoMO") );
						break;
					case "ECN":
						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_ECN") );
						break;
					case "SoftINFO":
						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_SoftINFO") );
						break;
					case "TRY":
						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_TRY") );
						break;
					case "OffMo":
						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_OFFMO") );
						break;
					case "DropMaterial":
						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_DropMaterial") );
						break;
					case "BurnIn":
						//html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_BurnIn") );
						html += string.Format( tpl,"FITOPResultBurnInQP.aspx", languageComponent.GetString("ItemTracing_BurnIn") ,moduleName);
						break;
					case "BurnOut":
						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_BurnOut") );
						break;
						//暂时没有以下工序结果
//					case "OutsideRoute":
//						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_OutsideRoute") );
//						break;
//					case "SMT":
//						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_SMT") );
//						break;
//					case "SPC":
//						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_SPC") );
//						break;
//					case "DeductBOMItem":
//						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_DeductBOMItem") );
//						break;
//					case "MidistOutput":
//						html += string.Format( tplnoref, languageComponent.GetString("ItemTracing_MidistOutput") );
//						break;
					default:
						html = string.Empty;
						break;
				}

				return html;
			}
			catch
			{

			}

			return html ;

		}

		/// <summary>
		/// 根据LastAction显示工序结果
		/// </summary>
		/// <param name="languageComponent"></param>
		/// <param name="lastAction"></param>
		/// <param name="runningCard"></param>
		/// <param name="runningCardSequence"></param>
		/// <param name="moCode"></param>
		/// <param name="referedURL"></param>
		/// <returns></returns>
		public static string GetOPResult(ControlLibrary.Web.Language.ILanguageComponent languageComponent,string lastAction)
		{
			string opResult = string.Empty ;
			try
			{
				string moduleName = ActionType.GetOperationResultModule(lastAction);
				switch(moduleName)
				{
					case "Testing":
						opResult = languageComponent.GetString("ItemTracing_testing") ;
						break;
					case "ComponentLoading":
						opResult = languageComponent.GetString("ItemTracing_componentloading") ;
						break;
					case "IDTranslation":
						opResult = languageComponent.GetString("ItemTracing_sn") ;
						break;
					case "Packing":
						opResult = languageComponent.GetString("ItemTracing_packing");
						break;
					case "UnPacking":
						opResult = languageComponent.GetString("ItemTracing_unpacking");
						break;
					case "TS":
						opResult = languageComponent.GetString("ItemTracing_ts");
						break;
					case "OQC":
						opResult = languageComponent.GetString("ItemTracing_oqc") ;
						break;
						//以下非工序结果 "Reject", "GoMO","ECN","SoftINFO","TRY"
					case "Reject":
						opResult = languageComponent.GetString("ItemTracing_Reject");
						break;
					case "GoMO":
						opResult = languageComponent.GetString("ItemTracing_GoMO") ;
						break;
					case "ECN":
						opResult = languageComponent.GetString("ItemTracing_ECN") ;
						break;
					case "SoftINFO":
						opResult = languageComponent.GetString("ItemTracing_SoftINFO") ;
						break;
					case "TRY":
						opResult = languageComponent.GetString("ItemTracing_TRY") ;
						break;
					case "OutsideRoute":
						opResult = languageComponent.GetString("ItemTracing_OutsideRoute") ;
						break;
					case "SMT":
						opResult = languageComponent.GetString("ItemTracing_SMT") ;
						break;
					case "SPC":
						opResult = languageComponent.GetString("ItemTracing_SPC") ;
						break;
					case "DeductBOMItem":
						opResult = languageComponent.GetString("ItemTracing_DeductBOMItem") ;
						break;
					case "MidistOutput":
						opResult = languageComponent.GetString("ItemTracing_MidistOutput") ;
						break;
					case "BurnIn":
						opResult = languageComponent.GetString("ItemTracing_BurnIn") ;
						break;
					case "BurnOut":
						opResult = languageComponent.GetString("ItemTracing_BurnOut") ;
						break;
					default:
						opResult  = string.Empty;
						break;
				}

				return opResult ;
			}
			catch
			{

			}

			return opResult  ;

		}

		private void GetOPByLastAction(string lastAction)
		{
		
		}

        public static string GetOPResultLinkHtml(ControlLibrary.Web.Language.ILanguageComponent languageComponent,string opCtrl,string runningCard,decimal runningCardSequence,string moCode,string referedURL)
        {
            string html = "" ;
            string tpl = string.Format("<a href=\"{{0}}?REFEREDURL={3}&RCARD={0}&RCARDSEQ={1}&MOCODE={2}\">{{1}}</a>&nbsp;",runningCard,runningCardSequence,moCode,System.Web.HttpUtility.UrlEncode(referedURL) ) ;

            try
            {
                if( opCtrl[ (int)OperationList.Testing ].ToString() == FormatHelper.TRUE_STRING )
                {
                    html += string.Format( tpl,"FITOPResultTestingQP.aspx", languageComponent.GetString("ItemTracing_testing") );
                }

                if( opCtrl[ (int)OperationList.ComponentLoading ].ToString() == FormatHelper.TRUE_STRING )
                {
                    html += string.Format( tpl,"FITOPResultComploadingQP.aspx", languageComponent.GetString("ItemTracing_componentloading") );
                }

                if( opCtrl[ (int)OperationList.IDTranslation ].ToString() == FormatHelper.TRUE_STRING )
                {
                    html += string.Format( tpl,"FITOPResultSNQP.aspx", languageComponent.GetString("ItemTracing_sn") );
                }

                if( opCtrl[ (int)OperationList.Packing ].ToString() == FormatHelper.TRUE_STRING )
                {
                    html += string.Format( tpl,"FITOPResultPackingQP.aspx", languageComponent.GetString("ItemTracing_packing") );
                }

                if( opCtrl[ (int)OperationList.TS ].ToString() == FormatHelper.TRUE_STRING )
                {
                    html += string.Format( tpl,"FITOPResultTSQP.aspx", languageComponent.GetString("ItemTracing_ts") );
                }

                if( opCtrl[ (int)OperationList.OQC ].ToString() == FormatHelper.TRUE_STRING )
                {
                    html += string.Format( tpl,"FITOPResultFQCQP.aspx", languageComponent.GetString("ItemTracing_oqc") );
                }
            }
            catch
            {

            }

            return html ;

        }

		
		
		/// <summary>
		///获取超链接html
		/// </summary>
		/// <param name="languageComponent">语言包</param>
		/// <param name="cellValue">显示的Value</param>
		/// <param name="postURLParms">超链接的地址和参数(传入前已经拼好)</param>
		/// <returns></returns>
		public static string GetLinkHtml(ControlLibrary.Web.Language.ILanguageComponent languageComponent,string cellValue,string postURLParms)
		{
			string html = string.Format("<a href=\"{0}\" style='color:blue;'>{1}</a>&nbsp;",postURLParms,cellValue);

			return html ;

		}

        public static string GetOPResultLinkText(ControlLibrary.Web.Language.ILanguageComponent languageComponent,string opCtrl)
        {
            string text = "" ;

            try
            {
                if( opCtrl[ (int)OperationList.Testing ].ToString() == FormatHelper.TRUE_STRING )
                {
                    text += languageComponent.GetString("ItemTracing_testing") + " ";
                }

                if( opCtrl[ (int)OperationList.ComponentLoading ].ToString() == FormatHelper.TRUE_STRING )
                {
                    text += languageComponent.GetString("ItemTracing_componentloading") ;
                }

                if( opCtrl[ (int)OperationList.IDTranslation ].ToString() == FormatHelper.TRUE_STRING )
                {
                    text += languageComponent.GetString("ItemTracing_sn") + " ";
                }

                if( opCtrl[ (int)OperationList.Packing ].ToString() == FormatHelper.TRUE_STRING )
                {
                    text += languageComponent.GetString("ItemTracing_packing") + " ";
                }

                if( opCtrl[ (int)OperationList.TS ].ToString() == FormatHelper.TRUE_STRING )
                {
                    text += languageComponent.GetString("ItemTracing_ts") + " ";
                }

                if( opCtrl[ (int)OperationList.OQC ].ToString() == FormatHelper.TRUE_STRING )
                {
                    text += languageComponent.GetString("ItemTracing_oqc") + " ";
                }
            }
            catch
            {

            }

            return text ;

        }

        public static string GetItemTracingHtml(string rcard,bool addLink)
        {
            if(addLink)
            {
                return string.Format(
                    "<a href='FItemTracingQP.aspx?RCARDFROM={0}&RCARDTO={0}'>{0}</a>",
                    rcard) ;
            }
            else
            {
                return rcard ;
            }
        }

        public static string GetProductionProcessHtml(string rcard,int rcardseq,string moCode,string referedURL,bool addLink)
        {
            if(addLink)
            {
                return string.Format(
                    "<a href='FITProductionProcessQP.aspx?RCARD={0}&RCARDSEQ={1}&MOCODE={2}&REFEREDURL={3}'>{0}</a>",
                    rcard,
                    rcardseq,
                    moCode,
                    System.Web.HttpUtility.UrlEncode(referedURL)
                ) ;
            }
            else
            {
                return rcard ;
            }
        }

		#region 合并单元格
		private UltraGridRow buildMergedGridRow( UltraGridRow row, UltraGridRow lastRow )
		{
			if ( this.MergeColumnIndexList == null )
			{
				return row;
			}

			ArrayList array = new ArrayList();

			foreach ( UltraGridCell cell in row.Cells )
			{
				array.Add( cell.Text );
			}

			if ( lastRow != null )
			{
				bool equal = false;

				foreach ( int[] indices in this.MergeColumnIndexList )
				{
					if ( indices == null )
					{
						continue;
					}

					equal = true;
					foreach ( int index in indices )
					{
						if ( row.Cells[index].Text != lastRow.Cells[index].Text )
						{
							equal = false;
							break;
						}
					}

					if ( equal )
					{
						foreach ( int index in indices )
						{
							array[index] = "";
						}
					}
					else
					{
						break;
					}
				}
			}

			return new UltraGridRow( array.ToArray() );
		}
		#endregion

		private void pagerSizeSelector_OnPagerSizeChanged(object sender, int pageSize)
		{
			if ( this.pagerSizeSelector != null )
			{
				if ( this.pagerToolBar!= null )
				{
					this.pagerToolBar.PageSize = pageSize;
					int iPageCount = 0;
					if (this.pagerToolBar.RowCount % pageSize == 0)
						iPageCount = this.pagerToolBar.RowCount / pageSize;
					else
						iPageCount = this.pagerToolBar.RowCount / pageSize + 1;
					if (this.pagerToolBar.PageIndex >= iPageCount)
					{
						this.pagerToolBar.PageIndex = 0;
					}
				
					this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
					
					this.pagerToolBar.InitPager();

					_processDataSource(this,1,this.pagerSizeSelector.PageSize,	this.pagerSizeSelector.PageSize);
					
				}
			}
				
		}
	}

	#region Arguments
	public class WebQueryEventArgs : EventArgs
	{
		public WebQueryEventArgs() : this(0,0)
		{
		}

		public WebQueryEventArgs(int startRow, int endRow)
		{
			this.StartRow = startRow;
			this.EndRow = endRow;
		}

		public int StartRow = 0;

		public int EndRow = 0;

		public int RowCount = 0;

		public object[] GridDataSource;
	}

	public class DomainObjectToGridRowEventArgs : EventArgs
	{
		public DomainObjectToGridRowEventArgs()
		{
		}

		public DomainObject DomainObject;

		public UltraGridRow GridRow;
	}

	public class DomainObjectToExportRowEventArgs : EventArgs
	{
		public DomainObjectToExportRowEventArgs()
		{
		}

		public DomainObject DomainObject;

		public string[] ExportRow;
	}

	public class ExportHeadEventArgs : EventArgs
	{
		public string[] Heads;
	}

	public class GridCellClickEventArgs : EventArgs
	{
		public UltraGridCell Cell;
	}
	#endregion
}
