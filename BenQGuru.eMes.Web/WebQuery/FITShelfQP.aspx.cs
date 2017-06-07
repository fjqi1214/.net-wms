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
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Domain.MOModel ;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FITShelfQP 的摘要说明。
	/// </summary>
	public partial class FITShelfQP : BaseQPage
	{
		private System.ComponentModel.IContainer components;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		protected WebQueryHelper _helper = null;
		protected GridHelper gridHelper = null;

	
		//protected GridHelper gridBurnInHelper ;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面

//			this.gridBurnInHelper = new GridHelper(this.gridWebGrid) ;
//			this.gridBurnInHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSourceBurnIn);
//			this.gridBurnInHelper.GetRowCountHandle = new GetRowCountDelegate(this.GetRowCountBurnIn);
//			this.gridBurnInHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRowBurnIn);	
//
//			InitWebGridBurnIn();

			this.gridHelper = new GridHelper(this.gridWebGrid);

			if(!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				InitWebGrid();

				txtBurnInDateFrom.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				txtBurnInDateTo.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );

				txtBurnOutDateFrom.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
				txtBurnOutDateTo.Text = FormatHelper.ToDateString(  FormatHelper.TODateInt(DateTime.Today) );
			}

			this._helper = new WebQueryHelper( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				
			//this._helper.GridCellClick +=new EventHandler(_helper_GridCellClick);

			FormatHelper.SetSNRangeValue(txtStartSnQuery,txtEndSnQuery);
		}

		protected ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
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
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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


		#region BurnInGrid
		protected void InitWebGrid()
		{
			/* 车号，BurnIn日期、时间，BurnIn人员，装车数量，产品序列号明细 */
			this.gridHelper.AddColumn( "ShelfPK","ShelfPK",null) ;
			this.gridHelper.AddColumn( "ShelfNO","车号",null) ;
			this.gridHelper.AddColumn( "BurnInDate", "BurnIn日期",	null);
			this.gridHelper.AddColumn( "BurnInTime", "BurnIn时间",	null);
			this.gridHelper.AddColumn( "BurnInUser", "BurnIn人员",	null);
			this.gridHelper.AddColumn( "BurnInTP", "BurnIn时长(hour)",	null);
			this.gridHelper.AddColumn( "BurnOutDate", "BurnOut日期",	null);
			this.gridHelper.AddColumn( "BurnOutTime", "BurnOut时间",	null);
			this.gridHelper.AddColumn( "BurnOutUser", "BurnOut人员",	null);
			this.gridHelper.AddColumn( "BurnInOutVolumn", "装车数量",	null);
			this.gridHelper.AddColumn( "ResourceCode", "资源代码",	null);
			this.gridHelper.AddColumn( "StepSequenceCode", "产线代码",	null);
			this.gridHelper.AddLinkColumn( "RCardList", "产品序列号明细",	null);

			this.gridHelper.Grid.Columns.FromKey("ShelfPK").Hidden = true;

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToGridRowEventArgs ).DomainObject != null )
			{
				ShelfActionListForQuery shelfal = ( e as DomainObjectToGridRowEventArgs ).DomainObject as ShelfActionListForQuery;
				( e as DomainObjectToGridRowEventArgs ).GridRow = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								shelfal.PKID.ToString(),
								shelfal.ShelfNO.ToString(),
								FormatHelper.ToDateString( shelfal.BurnInDate ),
								FormatHelper.ToTimeString( shelfal.BurnInTime ),
								shelfal.BurnInUser.ToString(),
								shelfal.BurnInTimePeriod.ToString(),
								FormatHelper.ToDateString( shelfal.BurnOutDate ),
								FormatHelper.ToTimeString( shelfal.BurnOutTIme ),
								shelfal.BurnOutUser.ToString(),
								shelfal.eAttribute1.ToString(),
								shelfal.ResourceCode.ToString(),
								shelfal.StepSequenceCode.ToString(),
								""
							}
					);
				}
		}

//		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
//		{
//			ShelfActionList shelfal = obj as ShelfActionList;
//			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
//				new object[]{
//								shelfal.PKID.ToString(),
//								shelfal.ShelfNO.ToString(),
//								FormatHelper.ToDateString( shelfal.BurnInDate ),
//								FormatHelper.ToTimeString( shelfal.BurnInTime ),
//								shelfal.BurnInUser.ToString(),
//								shelfal.BurnInTimePeriod.ToString(),
//								FormatHelper.ToDateString( shelfal.BurnOutDate ),
//								FormatHelper.ToTimeString( shelfal.BurnOutTIme ),
//								shelfal.BurnOutUser.ToString(),
//								shelfal.eAttribute1.ToString(),
//								""
//							}
//                
//                
//				);
//		}

//		protected override int GetRowCount()
//		{
//			ShelfFacade _facade = new ShelfFacade(base.DataProvider) ;
//
//			return _facade.QueryShelfActionListCountByShelfNo( this.txtShelf.Value.Trim() );
//
//		}

//		protected override object[] LoadDataSource(int inclusive, int exclusive)
//		{
//			ShelfFacade shelfFacade = new ShelfFacade( base.DataProvider );
//
//			object[] objs = (object[])shelfFacade.GetShelfActionListByShelfNo( this.txtShelf.Value.Trim() ); //ByShelfNo
//			if(objs ==null)
//			{
//				objs = new object[]{};
//			}
//
//			return objs;
//		}

		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{
			if((sender is System.Web.UI.HtmlControls.HtmlInputButton ) && ((System.Web.UI.HtmlControls.HtmlInputControl)(((System.Web.UI.HtmlControls.HtmlInputButton)(sender)))).Name == "cmdGridExport")
			{
				this.ExportQueryEvent(sender,e);
			}
			else
			{
				this.QueryEvent(sender,e);
			}
		}


		private void QueryEvent(object sender, EventArgs e)
		{
			int BurnInBeginDate = DefaultDateTime.DefaultToInt;
			int BurnInEndDate = DefaultDateTime.DefaultToInt;
			int BurnOutBeginDate = DefaultDateTime.DefaultToInt;
			int BurnOutEndDate = DefaultDateTime.DefaultToInt;

			if(rdbBurnIN.Checked)
			{
				BurnInBeginDate = FormatHelper.TODateInt(this.txtBurnInDateFrom.Text);
				BurnInEndDate = FormatHelper.TODateInt(this.txtBurnInDateTo.Text);
			}

			if(rdbBurnOut.Checked)
			{
				BurnOutBeginDate = FormatHelper.TODateInt(this.txtBurnOutDateFrom.Text);
				BurnOutEndDate = FormatHelper.TODateInt(this.txtBurnOutDateTo.Text);
			}

			ShelfFacade shelfFacade = new ShelfFacade( base.DataProvider );

			( e as WebQueryEventArgs ).GridDataSource = (object[])shelfFacade.GetShelfActionListByShelfNo( 
				FormatHelper.CleanString(this.txtShelf.Text.Trim()).ToUpper(),
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(), 
				FormatHelper.CleanString(this.txtStartSnQuery.Text),
				FormatHelper.CleanString(this.txtEndSnQuery.Text),
				BurnInBeginDate,BurnInEndDate, BurnOutBeginDate,BurnOutEndDate,
				( e as WebQueryEventArgs ).StartRow,
				( e as WebQueryEventArgs ).EndRow); //ByShelfNo

			( e as WebQueryEventArgs ).RowCount = shelfFacade.QueryShelfActionListCountByShelfNo( 
				FormatHelper.CleanString(this.txtShelf.Text.Trim()).ToUpper(),
				FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
				FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(), 
				FormatHelper.CleanString(this.txtStartSnQuery.Text),
				FormatHelper.CleanString(this.txtEndSnQuery.Text),
				BurnInBeginDate,BurnInEndDate, BurnOutBeginDate,BurnOutEndDate);
		}

		private void ExportQueryEvent(object sender, EventArgs e)
		{
			if(chbItemDetail.Checked)
			{
				int BurnInBeginDate = DefaultDateTime.DefaultToInt;
				int BurnInEndDate = DefaultDateTime.DefaultToInt;
				int BurnOutBeginDate = DefaultDateTime.DefaultToInt;
				int BurnOutEndDate = DefaultDateTime.DefaultToInt;

				if(rdbBurnIN.Checked)
				{
					BurnInBeginDate = FormatHelper.TODateInt(this.txtBurnInDateFrom.Text);
					BurnInEndDate = FormatHelper.TODateInt(this.txtBurnInDateTo.Text);
				}

				if(rdbBurnOut.Checked)
				{
					BurnOutBeginDate = FormatHelper.TODateInt(this.txtBurnOutDateFrom.Text);
					BurnOutEndDate = FormatHelper.TODateInt(this.txtBurnOutDateTo.Text);
				}

				ShelfFacade shelfFacade = new ShelfFacade( base.DataProvider );

				( e as WebQueryEventArgs ).GridDataSource = 
					shelfFacade.QueryShelf2RCard(
					FormatHelper.CleanString(this.txtShelf.Text.Trim()).ToUpper(),
					FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(), 
					FormatHelper.CleanString(this.txtStartSnQuery.Text),
					FormatHelper.CleanString(this.txtEndSnQuery.Text),
					BurnInBeginDate,BurnInEndDate, BurnOutBeginDate,BurnOutEndDate,
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);
			}
			else
			{
				this.QueryEvent(sender,e);
			}
		}

		#endregion


		#region Export 	

		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				if(chbItemDetail.Checked)
				{
					ItemTracingForShelf obj = ( e as DomainObjectToExportRowEventArgs ).DomainObject as ItemTracingForShelf;
					( e as DomainObjectToExportRowEventArgs ).ExportRow = 
						new string[]{
										obj.MaintainUser,//车号
										obj.RCard,
										obj.MOCode,
										obj.ItemCode,
										this.languageComponent1.GetString(obj.ItemStatus),
									
					};

				}
				else
				{
					ShelfActionListForQuery shelfal = ( e as DomainObjectToExportRowEventArgs ).DomainObject as ShelfActionListForQuery;
					( e as DomainObjectToExportRowEventArgs ).ExportRow = 
						new string[]{
										shelfal.ShelfNO.ToString(),
										FormatHelper.ToDateString( shelfal.BurnInDate ),
										FormatHelper.ToTimeString( shelfal.BurnInTime ),
										shelfal.BurnInUser.ToString(),
										shelfal.BurnInTimePeriod.ToString(),
										FormatHelper.ToDateString( shelfal.BurnOutDate ),
										FormatHelper.ToTimeString( shelfal.BurnOutTIme ),
										shelfal.BurnOutUser.ToString(),
										shelfal.eAttribute1.ToString(),
										shelfal.ResourceCode.ToString(),
										shelfal.StepSequenceCode.ToString(),
									
					};
				}
			}
		}

//		protected override string[] FormatExportRecord( object obj )
//		{
//			ShelfActionList shelfal = obj as ShelfActionList;
//			return  new string[]{
//									//shelfal.PKID.ToString(),
//									shelfal.ShelfNO.ToString(),
//									FormatHelper.ToDateString( shelfal.BurnInDate ),
//									FormatHelper.ToTimeString( shelfal.BurnInTime ),
//									shelfal.BurnInUser.ToString(),
//									shelfal.BurnInTimePeriod.ToString(),
//									FormatHelper.ToDateString( shelfal.BurnOutDate ),
//									FormatHelper.ToTimeString( shelfal.BurnOutTIme ),
//									shelfal.BurnOutUser.ToString(),
//									shelfal.eAttribute1.ToString(),
//								}
//				;
//		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			if(chbItemDetail.Checked)
			{
				( e as ExportHeadEventArgs ).Heads = new string[] {
																	  "ShelfNO",
																	  "SN", 
																	  "MOCode", 
																	  "ItemCode", 
																	  "Status",
				} ;
			}
			else
			{
				( e as ExportHeadEventArgs ).Heads = new string[] {
																	  "ShelfNO", 
																	  "BurnInDate", 
																	  "BurnInTime", 
																	  "BurnInUser", 
																	  "BurnInTP", 
																	  "BurnOutDate", 
																	  "BurnOutTime", 
																	  "BurnOutUser", 
																	  "BurnInOutVolumn", 
																	  "ResourceCode",
																	  "StepSequenceCode",
				};
			}
		}

//		protected override string[] GetColumnHeaderText()
//		{
//			return new string[] {
//									"ShelfNO", 
//									"BurnInDate", 
//									"BurnInTime", 
//									"BurnInUser", 
//									"BurnInTP", 
//									"BurnOutDate", 
//									"BurnOutTime", 
//									"BurnOutUser", 
//									"BurnInOutVolumn", 
//			} ;
//		}
		#endregion

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			//this._helper.Query(sender);
		}

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if(e.Cell.Key != "RCardList")
				return;

			string ShelfPK = e.Cell.Row.Cells.FromKey("ShelfPK").Value.ToString();

			this.Response.Redirect(string.Format("FITShelfDetailQP.aspx?ShelfPK={0}&REFEREDURL=FITShelfQP.aspx",ShelfPK),true);
		}
	}
}
