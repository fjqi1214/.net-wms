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

using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Web.UserControl ;
using BenQGuru.eMES.MOModel ;
using BenQGuru.eMES.BaseSetting ;

using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FItemTracingQP 的摘要说明。
	/// </summary>
	public partial class FItemCheck : BaseMPage
	{
    

        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private  ItemFacade  _facade = null ; //FacadeFactory.CreateQueryFacade2() ;



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
			if( !this.IsPostBack )
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
			this.gridHelper.AddColumn( "ItemCode", "产品代码",	null);
			this.gridHelper.AddColumn( "ModelCode2", "所属产品别",	null);
			this.gridHelper.AddColumn( "BarCode", "二维条码",	null);
			this.gridHelper.AddColumn( "ErrorCodeGroup", "不良代码组",	null);
			this.gridHelper.AddColumn( "ErrorCauseGroup", "不良原因组",	null);
			this.gridHelper.AddColumn( "Solution", "解决方案",	null);
			this.gridHelper.AddColumn( "IT_Route", "生产途程",	null);
			this.gridHelper.AddColumn( "OQCList", "检验清单",	null);
			this.gridHelper.AddColumn( "ItemSBOM", "产品生产BOM",	null);
			this.gridHelper.AddColumn( "OPBOM", "产品工序BOM",	null);
			this.gridHelper.AddColumn( "RouteCode", "生产途程代码",	null);
			this.gridHelper.AddColumn( "Keyparts", "Keyparts",	null);
			this.gridHelper.AddColumn( "Lot", "Lot",	null);

//          this.gridHelper.Grid.Columns.FromKey("ItemCode").MergeCells = true;
//			this.gridHelper.Grid.Columns.FromKey("ModelCode2").MergeCells = true;
//			this.gridHelper.Grid.Columns.FromKey("BarCode").MergeCells = true;
//			this.gridHelper.Grid.Columns.FromKey("ErrorCodeGroup").MergeCells = true;
//			this.gridHelper.Grid.Columns.FromKey("ErrorCause").MergeCells = true;
//			this.gridHelper.Grid.Columns.FromKey("Solution").MergeCells = true;
//			this.gridHelper.Grid.Columns.FromKey("IT_Route").MergeCells = true;
//			this.gridHelper.Grid.Columns.FromKey("OQCList").MergeCells = true;
//			this.gridHelper.Grid.Columns.FromKey("ItemSBOM").MergeCells = true;
//			this.gridHelper.Grid.Columns.FromKey("OPBOM").MergeCells = true;
//			this.gridHelper.Grid.Columns.FromKey("RouteCode").MergeCells = true;

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
        }
		
        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
			QDOItemCheck itemCheck = obj as QDOItemCheck ;
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
                new object[]{
                                itemCheck.ItemCode,
								itemCheck.ModelCode,
								itemCheck.BarCode,
								itemCheck.ErrorCodeGroup,
								itemCheck.ErrorCause,
								itemCheck.Solution,
								itemCheck.RouteCode,
								itemCheck.OQCCheck,
								itemCheck.SBOM,
								itemCheck.OPBOM,
								itemCheck.RouteCode2,
								itemCheck.Keyparts.ToString(),
								itemCheck.Lot.ToString()
                                }
                
                
            );
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			if( _facade == null )
			{
				_facade =  new ItemFacade(base.DataProvider);
			}
			return _facade.QueryItemCheck( FormatHelper.CleanString( FormatHelper.PKCapitalFormat( this.txtItemQuery.Text ) ),inclusive, exclusive ); 
        }


        protected override int GetRowCount()
        {
			if( _facade == null )
			{
				_facade =  new ItemFacade(base.DataProvider);
			}
			return _facade.QueryItemCheckCount(  FormatHelper.CleanString( FormatHelper.PKCapitalFormat( this.txtItemQuery.Text ) ) ) ;
        }

        #endregion

		protected override void cmdQuery_Click(object sender, EventArgs e)
		{
			if( !( new NullCheck().Check(lblItemCodeQuery,txtItemQuery.Text ) ) )
			{
				//return;
			}

			base.cmdQuery_Click (sender, e);

			this.MergeCells();
		}

        
        #region Export 	
        protected override string[] FormatExportRecord( object obj )
        {
			QDOItemCheck itemCheck = obj as QDOItemCheck ;
            return  new string[]{
									itemCheck.ItemCode,
									itemCheck.ModelCode,
									itemCheck.BarCode,
									itemCheck.ErrorCodeGroup,
									itemCheck.ErrorCause,
									itemCheck.Solution,
									itemCheck.RouteCode,
									itemCheck.OQCCheck,
									itemCheck.SBOM.ToString(),
									itemCheck.OPBOM,
									itemCheck.RouteCode2,
									itemCheck.Keyparts.ToString(),
									itemCheck.Lot.ToString()
                                }
                ;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {   "ItemCode", 
                                    "ModelCode2", 
                                    "BarCode", 
                                    "ErrorCodeGroup", 
                                    "ErrorCauseGroup", 
                                    "Solution", 
									"IT_Route",
                                    "OQCList", 
                                    "ItemSBOM", 
                                    "OPBOM", 
									"RouteCode",
                                    "Keyparts", 
                                    "Lot"
                                };
        }
		#endregion

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

			for( int i=0; i<this.gridWebGrid.Rows.Count-1; i++ )
			{
				UltraGridRow row = this.gridWebGrid.Rows[i];
				for( int j=i+1; j<this.gridWebGrid.Rows.Count; j++ )
				{
					UltraGridRow row2 = this.gridWebGrid.Rows[j];
					if( string.Compare( row.Cells.FromKey("ItemCode").Text,row2.Cells.FromKey("ItemCode").Text,true )==0 )
					{
						row.Cells.FromKey("ItemCode").RowSpan += 1;
						row.Cells.FromKey("ModelCode2").RowSpan += 1;
						row.Cells.FromKey("BarCode").RowSpan += 1;
						row.Cells.FromKey("ErrorCodeGroup").RowSpan += 1;
						row.Cells.FromKey("ErrorCauseGroup").RowSpan += 1;
						row.Cells.FromKey("Solution").RowSpan += 1;
						row.Cells.FromKey("IT_Route").RowSpan += 1;
						row.Cells.FromKey("OQCList").RowSpan += 1;
						row.Cells.FromKey("ItemSBOM").RowSpan += 1;
						row.Cells.FromKey("OPBOM").RowSpan += 1;
					}
					else
					{
						i=j-1;
						break;
					}
				}
			}
		}
	}
}
