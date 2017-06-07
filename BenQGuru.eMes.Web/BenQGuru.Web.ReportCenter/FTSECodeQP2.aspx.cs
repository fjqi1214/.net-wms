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
using BenQGuru.eMES.Web.UserControl;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// FTSECodeQP 的摘要说明。
	/// </summary>
	public partial class FTSECodeQP2 : BaseRQPage
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit2;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		public BenQGuru.eMES.Web.UserControl.eMESDate dateStartDateQuery;
		public BenQGuru.eMES.Web.UserControl.eMESDate dateEndDateQuery;
		protected ExcelExporter excelExporter = null;
		protected WebQueryHelper _helper = null;
		protected GridHelperForRPT _gridHelper = null;	
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this._gridHelper = new GridHelperForRPT(this.gridWebGrid);

			this._helper = new WebQueryHelper( this.cmdQuery,this.cmdGridExport,this.gridWebGrid,this.pagerSizeSelector,this.pagerToolBar,this.languageComponent1 );
			this._helper.LoadGridDataSource +=new EventHandler(_helper_LoadGridDataSource);
			this._helper.DomainObjectToGridRow +=new EventHandler(_helper_DomainObjectToGridRow);
			this._helper.DomainObjectToExportRow +=new EventHandler(_helper_DomainObjectToExportRow);
			this._helper.GetExportHeadText +=new EventHandler(_helper_GetExportHeadText);				
			this._helper.GridCellClick +=new EventHandler(_helper_GridCellClick);

			if( !this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this._initialWebGrid();

				this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
				this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;
			}

		}

		private void _initialWebGrid()
		{
			this.gridWebGrid.Columns.Clear();

			this._gridHelper.GridHelper.AddColumn("ErrorCode",				"不良代码",null);
			this._gridHelper.GridHelper.AddColumn("ModelCode",				"产品别",null);
			this._gridHelper.GridHelper.AddColumn("DPPM",				"DPPM(K)",null);

			//多语言
			this._gridHelper.GridHelper.ApplyLanguage( this.languageComponent1 );
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

		private Hashtable _htOutputQty = null;
		private bool _checkRequireFields()
		{
			PageCheckManager manager = new PageCheckManager();
			manager.Add(new DateRangeCheck(this.lblStartDateQuery,this.dateStartDateQuery.Text,this.lblEndDateQuery,this.dateEndDateQuery.Text,true));

			if( !manager.Check() )
			{
				WebInfoPublish.Publish(this,manager.CheckMessage,this.languageComponent1);
				return false;
			}	
			return true;
		}


		private void _helper_LoadGridDataSource(object sender, EventArgs e)
		{	
			this._initialWebGrid();
			if( this._checkRequireFields() )
			{
				FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
				object[] output = facadeFactory.CreateQueryTSInfoFacade().QueryTSECodeAnalyseOutput(
					FormatHelper.CleanString(this.txtConditionModel.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionSSCode.Text).ToUpper(),
					FormatHelper.CleanString(this.txtErrorCodeGroup.Text).ToUpper(),
					FormatHelper.CleanString(this.txtErrorCode.Text).ToUpper(),
					FormatHelper.TODateInt(this.dateStartDateQuery.Text),
					FormatHelper.TODateInt(this.dateEndDateQuery.Text),
					this.drpDateGroup.SelectedValue
					);
				object[] dataSource = 
					facadeFactory.CreateQueryTSInfoFacade().QueryTSECodeAnalyse(
					FormatHelper.CleanString(this.txtConditionModel.Text).ToUpper(),
					FormatHelper.CleanString(this.txtConditionSSCode.Text).ToUpper(),
					FormatHelper.CleanString(this.txtErrorCodeGroup.Text).ToUpper(),
					FormatHelper.CleanString(this.txtErrorCode.Text).ToUpper(),
					FormatHelper.TODateInt(this.dateStartDateQuery.Text),
					FormatHelper.TODateInt(this.dateEndDateQuery.Text),
					this.drpDateGroup.SelectedValue,
					( e as WebQueryEventArgs ).StartRow,
					( e as WebQueryEventArgs ).EndRow);

				( e as WebQueryEventArgs ).GridDataSource = dataSource;

				this.gridWebGrid.Rows.Clear();
				if (dataSource != null)
				{
					this.SetGridColumn(output, dataSource);
					
					AddDateGroupRow();
					AddOutputRow(output);

					DomainObjectToGridRow(dataSource);
				}
				( e as WebQueryEventArgs ).RowCount = this.gridWebGrid.Rows.Count;
			}
		}

		private Hashtable _htDateItemIdx = null;
		private void SetGridColumn(object[] output, object[] dataSource)
		{
			this.gridWebGrid.Columns.Clear();
			this._gridHelper.GridHelper.AddColumn("ErrorCode",	"不良代码", null);
			ArrayList list = new ArrayList();
			_htDateItemIdx = new Hashtable();
			SortedList sortList = new SortedList();
			if (output != null)
			{
				for (int i = 0; i < output.Length; i++)
				{
					QDOTSErrorCodeAnalyse item = (QDOTSErrorCodeAnalyse)output[i];
					if (list.Contains(item.DateGroup.ToString() + ":" + item.ModelCode) == false)
					{
						sortList.Add(item.DateGroup.ToString() + ":" + item.ModelCode, item);
						list.Add(item.DateGroup.ToString() + ":" + item.ModelCode);
					}
				}
			}
			for (int i = 0; i < dataSource.Length; i++)
			{
				QDOTSErrorCodeAnalyse item = (QDOTSErrorCodeAnalyse)dataSource[i];
				if (list.Contains(item.DateGroup.ToString() + ":" + item.ModelCode) == false)
				{
					sortList.Add(item.DateGroup.ToString() + ":" + item.ModelCode, item);
					list.Add(item.DateGroup.ToString() + ":" + item.ModelCode);
				}
			}
			foreach (object objTmp in sortList.Keys)
			{
				QDOTSErrorCodeAnalyse item = (QDOTSErrorCodeAnalyse)sortList[objTmp];
				this._gridHelper.GridHelper.AddColumn(item.DateGroup.ToString() + ":" + item.ModelCode + ":ErrorQty", item.ModelCode);
				this._gridHelper.GridHelper.AddColumn(item.DateGroup.ToString() + ":" + item.ModelCode + ":DPPM", "DPPM(K)");
				_htDateItemIdx.Add(this.gridWebGrid.Columns.Count - 2, new string[]{item.DateGroup.ToString(), item.ModelCode});
			}
			if (this.gridWebGrid.Columns.Count > 15)
			{
				this.gridWebGrid.Columns[0].Width = Unit.Pixel(120);
				for (int i = 1; i < this.gridWebGrid.Columns.Count; i++)
				{
					this.gridWebGrid.Columns[i].Width = Unit.Pixel(60);
				}
			}
		}

		// 加入产量数据
		private void AddOutputRow(object[] output)
		{
			// 加入产量数据
			object[] objRow = new object[this.gridWebGrid.Columns.Count];
			objRow[0] = "Output";
			_htOutputQty = new Hashtable();
			if (output != null)
			{
				for (int i = 0; i < output.Length; i++)
				{
					QDOTSErrorCodeAnalyse item = (QDOTSErrorCodeAnalyse)output[i];
					_htOutputQty.Add(item.DateGroup.ToString() + ":" + item.ModelCode, item.Quantity);
					UltraGridColumn column = this.gridWebGrid.Columns.FromKey(item.DateGroup.ToString() + ":" + item.ModelCode + ":ErrorQty");
					if (column != null)
						objRow[column.Index] = item.Quantity;
				}
			}
			this.gridWebGrid.Rows.Add(new UltraGridRow(objRow));
			this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.ForeColor = Color.Blue;
		}

		private void AddDateGroupRow()
		{
			object[] objRow = new object[this.gridWebGrid.Columns.Count];
			objRow[0] = "日期";
			foreach (object objIdx in _htDateItemIdx.Keys)
			{
				string[] strDateItem = (string[])_htDateItemIdx[objIdx];
				objRow[Convert.ToInt32(objIdx)] = strDateItem[0];
			}
			string strPrevDate = string.Empty;
			for (int i = 1; i < objRow.Length; i++)
			{
				if (objRow[i] != null)
				{
					if (objRow[i].ToString() == strPrevDate)
					{
						objRow[i] = string.Empty;
					}
					else
					{
						strPrevDate = objRow[i].ToString();
					}
				}
			}
			this.gridWebGrid.Rows.Add(new UltraGridRow(objRow));
			this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.ForeColor = Color.Blue;
			
			strPrevDate = string.Empty;
			int iPrevIdx = -1;
			int iLastRow = this.gridWebGrid.Rows.Count - 1;
			for (int i = 1; i < this.gridWebGrid.Columns.Count; i++)
			{
				if (this.gridWebGrid.Rows[iLastRow].Cells[i].Text != null && 
					this.gridWebGrid.Rows[iLastRow].Cells[i].Text != string.Empty && 
					this.gridWebGrid.Rows[iLastRow].Cells[i].Text != strPrevDate)
				{
					if (strPrevDate != string.Empty)
					{
						this.gridWebGrid.Rows[iLastRow].Cells[iPrevIdx].ColSpan = i - iPrevIdx;
						this.gridWebGrid.Rows[iLastRow].Cells[iPrevIdx].Style.HorizontalAlign = HorizontalAlign.Center;
						this.gridWebGrid.Rows[iLastRow].Cells[iPrevIdx].Style.Font.Bold = true;
					}
					strPrevDate = this.gridWebGrid.Rows[iLastRow].Cells[i].Text;
					iPrevIdx = i;
				}
			}
			this.gridWebGrid.Rows[iLastRow].Cells[iPrevIdx].ColSpan = this.gridWebGrid.Columns.Count - iPrevIdx;
			this.gridWebGrid.Rows[iLastRow].Cells[iPrevIdx].Style.HorizontalAlign = HorizontalAlign.Center;
			this.gridWebGrid.Rows[iLastRow].Cells[iPrevIdx].Style.Font.Bold = true;
		}

		private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
		{}
		private void DomainObjectToGridRow(object[] dataSource)
		{
			string strPrevErrorGroupCode = string.Empty;
			string strPrevErrorCode = string.Empty;
			object[] objRow = null;
			Hashtable htQtyTotal = new Hashtable();
			Hashtable htDppmTotal = new Hashtable();
			QDOTSErrorCodeAnalyse item = null;
			for (int i = 0; i < dataSource.Length; i++)
			{
				item = (QDOTSErrorCodeAnalyse)dataSource[i];
				if (item.ErrorCodeGroup != strPrevErrorGroupCode || 
					item.ErrorCode != strPrevErrorCode)
				{
					if (objRow != null)
					{
						this.gridWebGrid.Rows.Add(new UltraGridRow(objRow));
					}
					
					if (item.ErrorCodeGroup != strPrevErrorGroupCode)
					{
						if (strPrevErrorGroupCode != string.Empty)
						{
							AddGroupTotalDataRow(htQtyTotal, htDppmTotal, strPrevErrorGroupCode);
						}
						// 添加不良代码组
						objRow = new object[this.gridWebGrid.Columns.Count];
						objRow[0] = item.ErrorCodeGroupDesc;
						this.gridWebGrid.Rows.Add(new UltraGridRow(objRow));
						this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.BackColor = Color.Linen;
						this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.Font.Bold = true;
						
						htQtyTotal = new Hashtable();
						htDppmTotal = new Hashtable();
					}
					//
					objRow = new object[this.gridWebGrid.Columns.Count];
					objRow[0] = item.ErrorCodeDesc;

					strPrevErrorGroupCode = item.ErrorCodeGroup;
					strPrevErrorCode = item.ErrorCode;
				}
				UltraGridColumn column = this.gridWebGrid.Columns.FromKey(item.DateGroup.ToString() + ":" + item.ModelCode + ":ErrorQty");
				if (column != null)
				{
					objRow[column.Index] = item.Quantity;
					if (htQtyTotal.ContainsKey(column.Index) == false)
						htQtyTotal.Add(column.Index, item.Quantity);
					else
						htQtyTotal[column.Index] = Convert.ToInt32(htQtyTotal[column.Index]) + item.Quantity;
					if (this._htOutputQty.Contains(item.DateGroup.ToString() + ":" + item.ModelCode) &&
						Convert.ToInt32(this._htOutputQty[item.DateGroup.ToString() + ":" + item.ModelCode]) != 0)
					{
						objRow[column.Index + 1] = item.Quantity * 1000 / Convert.ToInt32(this._htOutputQty[item.DateGroup.ToString() + ":" + item.ModelCode]);
						if (htDppmTotal.ContainsKey(column.Index + 1) == false)
							htDppmTotal.Add(column.Index + 1, objRow[column.Index + 1]);
						else
							htDppmTotal[column.Index + 1] = Convert.ToInt32(htDppmTotal[column.Index + 1]) + Convert.ToInt32(objRow[column.Index + 1]);
					}
				}
			}
			// 添加最后一行
			this.gridWebGrid.Rows.Add(new UltraGridRow(objRow));
			
			AddGroupTotalDataRow(htQtyTotal, htDppmTotal, strPrevErrorGroupCode);

			AddGlobalTotalDataRow();
		}
		private Hashtable _htQtyTotalAll = null;
		private void AddGroupTotalDataRow(Hashtable htQtyTotal, Hashtable htDppmTotal, string strPrevErrorGroupCode)
		{
			// 添加汇总行
			object[] objRow = new object[this.gridWebGrid.Columns.Count];
			objRow[0] = "Total";
			foreach (object objIdx in htQtyTotal.Keys)
			{
				objRow[Convert.ToInt32(objIdx)] = Convert.ToInt32(htQtyTotal[objIdx]);
			}
			this.gridWebGrid.Rows.Add(new UltraGridRow(objRow));
			this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.ForeColor = Color.Crimson;
			this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.Font.Bold = true;
			// 添加汇总的DPPM
			objRow = new object[this.gridWebGrid.Columns.Count];
			objRow[0] = "DPPM";
			foreach (object objIdx in htDppmTotal.Keys)
			{
				objRow[Convert.ToInt32(objIdx)] = Convert.ToInt32(htDppmTotal[objIdx]);
			}
			this.gridWebGrid.Rows.Add(new UltraGridRow(objRow));
			this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.ForeColor = Color.Green;
			this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.Font.Bold = true;
			// 添加不良代码组汇总不良率
			objRow = new object[this.gridWebGrid.Columns.Count];
			objRow[0] = strPrevErrorGroupCode + " Y/R";
			foreach (object objIdx in htQtyTotal.Keys)
			{
				string[] strDateItem = (string[])this._htDateItemIdx[objIdx];
				if (strDateItem == null)
					continue;
				string strDateGroup = strDateItem[0];
				string strModelCode = strDateItem[1];
				if (this._htOutputQty.Contains(strDateGroup + ":" + strModelCode) &&
					Convert.ToInt32(this._htOutputQty[strDateGroup + ":" + strModelCode]) != 0)
				{
					objRow[Convert.ToInt32(objIdx)] = (Convert.ToInt32(this._htOutputQty[strDateGroup + ":" + strModelCode]) - Convert.ToInt32(htQtyTotal[objIdx])) * 100 / Convert.ToInt32(this._htOutputQty[strDateGroup + ":" + strModelCode]);
					objRow[Convert.ToInt32(objIdx)] = Math.Round(Convert.ToDecimal(objRow[Convert.ToInt32(objIdx)]), 1).ToString() + "%";
				}
			}
			this.gridWebGrid.Rows.Add(new UltraGridRow(objRow));
			this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.ForeColor = Color.Red;
			this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.Font.Bold = true;

			if (_htQtyTotalAll == null) _htQtyTotalAll = new Hashtable();
			foreach (object objIdx in htQtyTotal.Keys)
			{
				if (_htQtyTotalAll.ContainsKey(objIdx) == false)
					_htQtyTotalAll.Add(objIdx, htQtyTotal[objIdx]);
				else
					_htQtyTotalAll[objIdx] = Convert.ToInt32(_htQtyTotalAll[objIdx]) + Convert.ToInt32(htQtyTotal[objIdx]);
			}
			
		}
		private void AddGlobalTotalDataRow()
		{
			object[] objRow = new object[this.gridWebGrid.Columns.Count];
			objRow[0] = "Total Y/R";
			foreach (object objIdx in _htQtyTotalAll.Keys)
			{
				string[] strDateItem = (string[])this._htDateItemIdx[objIdx];
				if (strDateItem == null)
					continue;
				string strDateGroup = strDateItem[0];
				string strModelCode = strDateItem[1];
				if (this._htOutputQty.Contains(strDateGroup + ":" + strModelCode) &&
					Convert.ToInt32(this._htOutputQty[strDateGroup + ":" + strModelCode]) != 0)
				{
					objRow[Convert.ToInt32(objIdx)] = (Convert.ToInt32(this._htOutputQty[strDateGroup + ":" + strModelCode]) - Convert.ToInt32(_htQtyTotalAll[objIdx])) * 100 / Convert.ToInt32(this._htOutputQty[strDateGroup + ":" + strModelCode]);
					objRow[Convert.ToInt32(objIdx)] = Math.Round(Convert.ToDecimal(objRow[Convert.ToInt32(objIdx)]), 1).ToString() + "%";
				}
			}
			this.gridWebGrid.Rows.Add(new UltraGridRow(objRow));
			this.gridWebGrid.Rows[this.gridWebGrid.Rows.Count - 1].Style.ForeColor = Color.Red;
		}

		private bool bExportFlag = false;
		private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
		{
			if( ( e as DomainObjectToExportRowEventArgs ).DomainObject != null )
			{
				if (bExportFlag == true)
				{
					( e as DomainObjectToExportRowEventArgs ).ExportRow = null;
					return;
				}
				for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
				{
					string[] strRow = new string[this.gridWebGrid.Columns.Count];
					for (int n = 0; n < this.gridWebGrid.Columns.Count; n++)
					{
						strRow[n] = this.gridWebGrid.Rows[i].Cells[n].Text;
					}
					this._helper.excelExporter.AppendRow(strRow);
				}
				bExportFlag = true;
				( e as DomainObjectToExportRowEventArgs ).ExportRow = null;
			}
		}

		private void _helper_GetExportHeadText(object sender, EventArgs e)
		{
			string[] strHeader = new string[this.gridWebGrid.Columns.Count];
			for (int i = 0; i < strHeader.Length; i++)
			{
				strHeader[i] = this.gridWebGrid.Columns[i].HeaderText;
			}
			( e as ExportHeadEventArgs ).Heads = strHeader;
		}	
	
		private void _helper_GridCellClick(object sender, EventArgs e)
		{			
		}
	}
}
