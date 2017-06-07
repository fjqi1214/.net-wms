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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.SolderPaste;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// ItemMP 的摘要说明。
	/// </summary>
	public partial class FSolderPasteMP : BaseMPageMinus
	{
		private System.ComponentModel.IContainer components;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        protected global::System.Web.UI.WebControls.TextBox txtFromProDateQuery;
        protected global::System.Web.UI.WebControls.TextBox txtToProDateQuery;
        protected global::System.Web.UI.WebControls.TextBox txtProDateEdit;
		private BenQGuru.eMES.SMT.SolderPasteFacade _solderPasteFacade = null ;


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
			//this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
			this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
			//this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
			// 
			// languageComponent1
			// 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";
			// 
			// excelExporter
			// 
			this.excelExporter.FileExtension = "xls";
			this.excelExporter.LanguageComponent = this.languageComponent1;
			this.excelExporter.Page = this;
			this.excelExporter.RowSplit = "\r\n";

		}
		#endregion

		#region form events
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitOnPostBack();
		    if (!IsPostBack)
		    {	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
				// 初始化界面UI
				this.InitUI();
				InitQueryPanel();
				InitButtonHelp();
				SetEditObject(null);
				this.InitWebGrid();
		    }
		}
		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			RequestData();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Query );	
		}

        //private void gridWebGrid_DblClick(object sender,Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
        //{
        //    object obj = this.GetEditObject(e.Row);

        //    if ( obj != null )
        //    {
        //        this.SetEditObject( obj );

        //        this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
        //    }
        //}

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            object obj = this.GetEditObject();
            if (obj != null)
            {
                if (_solderPasteFacade == null) { _solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade(); }
                this._solderPasteFacade.AddSolderPaste(obj as SolderPaste);
                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{

				ArrayList objs = new ArrayList( array.Count );
			
				foreach (GridRecord row in array)
				{
					object obj = this.GetEditObject(row);
					if( obj != null )
					{
						objs.Add( (SolderPaste)obj );
					}
				}
				
				this._solderPasteFacade.DeleteSolderPaste( (SolderPaste[])objs.ToArray( typeof(SolderPaste) ) );
				
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
			}
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}
				object obj = this.GetEditObject();

				if(obj != null)
				{
					this._solderPasteFacade.UpdateSolderPaste( obj as SolderPaste );;
					this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
					this.RequestData();
					this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
				}
		
		}

		protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
		{
			
			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );
		}

        //private void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        //{
        //    if ( this.chbSelectAll.Checked )
        //    {
        //        this.gridHelper.CheckAllRows( CheckStatus.Checked );
        //    }
        //    else
        //    {
        //        this.gridHelper.CheckAllRows( CheckStatus.Unchecked );
        //    }
        //}

        protected override void Grid_ClickCell(GridRecord row, string commandName)
		{
            if (commandName == "Edit")
			{
				object obj = this.GetEditObject(row);

				if ( obj != null )
				{
					this.SetEditObject( obj );

					this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
				}
			}
		}

		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}

		
		#endregion

		#region private method
		private void RequestData()
		{
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}

		private void InitOnPostBack()
		{		
			this.buttonHelper = new ButtonHelper(this);
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
			this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

			this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

			// 2005-04-06
			this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
			this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
			this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
		}

		public void InitButtonHelp()
		{	
			this.buttonHelper.AddDeleteConfirm();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
		}

		private void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if(pageAction == PageActionType.Add)
			{
				this.txtSPIDEdit.ReadOnly = false;
			}
			else if ( pageAction == PageActionType.Update )
			{
				this.txtSPIDEdit.ReadOnly = true;
			}
			else if ( pageAction == PageActionType.Save )
			{
				this.txtSPIDEdit.ReadOnly = false;
			}
			else if(pageAction ==PageActionType.Cancel)
			{
				this.txtSPIDEdit.ReadOnly = false;
			}
		}

		protected override void InitWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn( "SolderPasteID",				"锡膏编码",		null);
			this.gridHelper.AddColumn( "SolderPastePartNO",			"锡膏物料号",	null);
			this.gridHelper.AddColumn( "SolderPasteLotNO",			"锡膏批号",		null);
			this.gridHelper.AddColumn( "DateCode",					"生产日期",	null);
			this.gridHelper.AddColumn( "InvalidDate",				"失效日期",	null);
			this.gridHelper.AddColumn( "Status",					"状态",	null);

			this.gridHelper.AddColumn( "MUSER",   "维护人员",null);
			this.gridHelper.AddColumn( "MDATE",   "维护日期",null);
			this.gridHelper.AddColumn( "MTIME",   "维护时间",null);
			
			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private  void InitQueryPanel()
		{			
			this.drpSPStatusQuery.Items.Clear();
			this.drpSPStatusQuery.Items.Add( new ListItem( "", "" ));
			new DropDownListBuilder2( new SolderPasteStatus(), this.drpSPStatusQuery, this.languageComponent1 ).Build();

			this.txtFromProDateQuery.Text = FormatHelper.ToDateString( FormatHelper.TODateInt(DateTime.Now) );
			this.txtToProDateQuery.Text = this.txtFromProDateQuery.Text;
			
		}

		private  object GetEditObject()
		{
			if(	this.ValidateInput())
			{
				if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}

				SolderPaste obj = this._solderPasteFacade.CreateNewSolderPaste();

				obj.SolderPasteID = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSPIDEdit.Text, 40));
				obj.PartNO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPartNOEdit.Text, 40));

				object spc = _solderPasteFacade.GetSolderPasteControl( obj.PartNO );
				if(spc==null)
				{
					WebInfoPublish.Publish(this, "$Error_SolderPastePartNO_Not_Maintain",languageComponent1);
					return null;
				}

				obj.LotNO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLotNOEdit.Text, 40));
				obj.ProductionDate = FormatHelper.TODateInt( this.txtProDateEdit.Text );

				DateTime proDate = DateTime.ParseExact(this.txtProDateEdit.Text,"yyyy-MM-dd",null);
				DateTime exDate = proDate.AddMonths( (spc as SolderPasteControl).GuaranteePeriod );
				obj.ExpiringDate = FormatHelper.TODateInt(exDate);

				obj.Used = FormatHelper.FALSE_STRING;
				obj.Status = SolderPasteStatus.Normal ;
				obj.MaintainUser = this.GetUserCode();
				
				return obj;
			}
			else
			{
				return null;
			}
		}

		private object GetEditObject(GridRecord row)
		{	
			if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}

			object obj = this._solderPasteFacade.GetSolderPaste(row.Items.FindItemByKey("SolderPasteID").Text.ToString());
			
			if (obj != null)
			{
				return (SolderPaste)obj;
			}

			return null;
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblSPIDEdit, txtSPIDEdit, 40, true) );
			manager.Add( new LengthCheck(lblPartSNEdit, txtPartNOEdit, 40, true) );
			manager.Add( new LengthCheck(lblLotSNEdit, txtLotNOEdit, 40, true) );
			manager.Add( new DateCheck(lblDateCode, txtProDateEdit.Text, true) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}

		private void SetEditObject(object obj)
		{
			if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}
			if (obj == null)
			{
				this.txtSPIDEdit.Text = string.Empty ;
				this.txtPartNOEdit.Text = string.Empty ;
				this.txtLotNOEdit.Text = string.Empty ;
				this.txtProDateEdit.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now )) ;

				return;
			}
			
			SolderPaste solderPaste = obj as SolderPaste;

			this.txtSPIDEdit.Text = solderPaste.SolderPasteID.ToString() ;
			this.txtPartNOEdit.Text = solderPaste.PartNO.ToString() ;
			this.txtLotNOEdit.Text = solderPaste.LotNO.ToString() ;
			this.txtProDateEdit.Text = FormatHelper.ToDateString(solderPaste.ProductionDate) ;
		}

		protected DataRow GetGridRow(object obj)
		{
			SolderPaste solderPaste = obj as SolderPaste;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{   "false",
            //                    solderPaste.SolderPasteID.ToString(),
            //                    solderPaste.PartNO.ToString(),
            //                    solderPaste.LotNO.ToString(),
            //                    FormatHelper.ToDateString(solderPaste.ProductionDate),
            //                    FormatHelper.ToDateString(solderPaste.ExpiringDate),
            //                    this.languageComponent1.GetString(solderPaste.Status),
            //                    solderPaste.MaintainUser.ToString(),
            //                    FormatHelper.ToDateString(solderPaste.MaintainDate),
            //                    FormatHelper.ToTimeString(solderPaste.MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["SolderPasteID"] = solderPaste.SolderPasteID.ToString();
            row["SolderPastePartNO"] = solderPaste.PartNO.ToString();
            row["SolderPasteLotNO"] = solderPaste.LotNO.ToString();
            row["DateCode"] = FormatHelper.ToDateString(solderPaste.ProductionDate);
            row["InvalidDate"] = FormatHelper.ToDateString(solderPaste.ExpiringDate);
            row["Status"] = this.languageComponent1.GetString(solderPaste.Status);
            row["MUSER"] = solderPaste.MaintainUser.ToString();
            row["MDATE"] = FormatHelper.ToDateString(solderPaste.MaintainDate);
            row["MTIME"] = FormatHelper.ToTimeString(solderPaste.MaintainTime);
            return row;

		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if( _solderPasteFacade==null )
			{
				_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();
			}

			return _solderPasteFacade.QuerySolderPaste(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtSPIDQuery.Text)),
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtPartNOQuery.Text)),
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtLotNOQuery.Text)),
				drpSPStatusQuery.SelectedValue, 
				FormatHelper.TODateInt(FormatHelper.CleanString(this.txtFromProDateQuery.Text)),
				FormatHelper.TODateInt(FormatHelper.CleanString(this.txtToProDateQuery.Text)),
				inclusive, exclusive);
		}
	
		private int GetRowCount()
		{
			if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}
			return _solderPasteFacade.QuerySolderPasteCount(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtSPIDQuery.Text)),
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtPartNOQuery.Text)),
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtLotNOQuery.Text)),
				drpSPStatusQuery.SelectedValue, 
				FormatHelper.TODateInt(FormatHelper.CleanString(this.txtFromProDateQuery.Text)),
				FormatHelper.TODateInt(FormatHelper.CleanString(this.txtToProDateQuery.Text)));
		}

		private string[] FormatExportRecord( object obj )
		{
			SolderPaste solderPaste = obj as SolderPaste;
			return new string[]{   solderPaste.SolderPasteID.ToString(),
								   solderPaste.PartNO.ToString(),
								   solderPaste.LotNO.ToString(),
								   FormatHelper.ToDateString(solderPaste.ProductionDate),
								   FormatHelper.ToDateString(solderPaste.ExpiringDate),
								   this.languageComponent1.GetString(solderPaste.Status),
								   solderPaste.MaintainUser.ToString(),
								   FormatHelper.ToDateString(solderPaste.MaintainDate),
								   FormatHelper.ToTimeString(solderPaste.MaintainTime)
								  
			};
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"SolderPasteID",
									"SolderPastePartNO",
									"SolderPasteLotNO",	
									"DateCode",
									"InvalidDate",
									"Status",
									"MUSER",	
									"MDATE",
									"MTIME"
			                        };
		}

		#endregion

		

	}
}
