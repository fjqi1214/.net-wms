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
using BenQGuru.eMES.Domain.BaseSetting;
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
	public partial class FSolderPasteCTRLMP : BaseMPageMinus
	{
		private System.ComponentModel.IContainer components;
		//protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		//private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		
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
				InitDropdownList();
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
			if(obj != null)
			{
				if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}
				this._solderPasteFacade.AddSolderPasteControl(obj as SolderPasteControl);
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
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
						objs.Add( (SolderPasteControl)obj );
					}
				}
				
				this._solderPasteFacade.DeleteSolderPasteControl( (SolderPasteControl[])objs.ToArray( typeof(SolderPasteControl) ) );
				
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
				this._solderPasteFacade.UpdateSolderPasteControl( obj as SolderPasteControl );;
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
			}
		}

		protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
		{
			
			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );
		}

		
        
		//private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
            protected override void Grid_ClickCell( GridRecord row, string commandName)
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
            else if (commandName == "MO2Order")
			{
				//Response.Redirect(this.MakeRedirectUrl("../OQC/FItem2CheckListSP.aspx",new string[] {"itemcode"},new string[] {e.Cell.Row.Cells.FromKey("ItemCode").Text.Trim()}));
			}

            else if (commandName == "OrderDetail")
			{
				Response.Redirect(this.MakeRedirectUrl ("FOrderDetailMP.aspx",new string[] {"OrderNO"},new string[] {row.Items.FindItemByKey("OrderNO").Text.Trim()}));
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
				this.txtPartNOEdit.ReadOnly = false;
				this.drpSPTypeEdit.SelectedIndex = 0;
			}
			else if ( pageAction == PageActionType.Update )
			{
				this.txtPartNOEdit.ReadOnly = true;
			}
			else if ( pageAction == PageActionType.Save )
			{
				this.txtPartNOEdit.ReadOnly = false;
			}
			else if(pageAction ==PageActionType.Cancel)
			{
				this.txtPartNOEdit.ReadOnly = false;
				this.drpSPTypeEdit.SelectedIndex = 0;
			}
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "SolderPastePartNO",			"锡膏物料号",	null);
			this.gridHelper.AddColumn( "SolderPasteType",			"锡膏类型",		null);
			this.gridHelper.AddColumn( "ReturnTimeSpan",			"回温时长",	null);
			this.gridHelper.AddColumn( "UnOpenTimeSpan",			"未开封时长",	null);
			this.gridHelper.AddColumn( "OpenTimeSpan",				"开封时长",	null);
			this.gridHelper.AddColumn( "GuaranteePeriod",			"保质期",	null);

			this.gridHelper.AddColumn( "MUSER",   "维护人员",null);
			this.gridHelper.AddColumn( "MDATE",   "维护日期",null);
			this.gridHelper.AddColumn( "MTIME",   "维护时间",null);
			
			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private  void InitDropdownList()
		{                        
			this.drpSPTypeEdit.Items.Clear();
			this.drpSPTypeEdit.Items.Add( new ListItem( "", "" ));
			
			this.drpSPTypeQuery.Items.Clear();
			this.drpSPTypeQuery.Items.Add( new ListItem( "", "" ));

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade();
            object[] objects = systemSettingFacade.GetParametersByParameterGroup("SMT_SOLDER_TYPE");
            if (objects != null)
            {
                foreach (object obj in objects)
                {
                    this.drpSPTypeEdit.Items.Add( new ListItem( ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterDescription, ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterAlias ));
                    this.drpSPTypeQuery.Items.Add(new ListItem(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterDescription, ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterAlias));
                }
            }
		}

		private  object GetEditObject()
		{
			if(	this.ValidateInput())
			{
				if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}

				SolderPasteControl spc = this._solderPasteFacade.CreateNewSolderPasteControl();

				spc.PartNO = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPartNOEdit.Text, 40));
				spc.Type = drpSPTypeEdit.SelectedValue ;
				spc.ReturnTimeSpan = Convert.ToDecimal( FormatHelper.CleanString(this.txtReturnTPEdit.Text) ); 
				spc.UnOpenTimeSpan = Convert.ToDecimal( FormatHelper.CleanString(this.txtUnOpenTPEdit.Text) );
				spc.OpenTimeSpan = Convert.ToDecimal( FormatHelper.CleanString(this.txtOpenTPEdit.Text) );
				spc.GuaranteePeriod = Convert.ToInt32( FormatHelper.CleanString(this.txtGuanPerEdit.Text) );
				spc.MaintainUser = this.GetUserCode();
				
				return spc;
			}
			else
			{
				return null;
			}
		}

		private object GetEditObject(GridRecord row)
		{	
			if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}

			object obj = this._solderPasteFacade.GetSolderPasteControl(row.Items.FindItemByKey("SolderPastePartNO").Text.ToString());
			
			if (obj != null)
			{
				return (SolderPasteControl)obj;
			}

			return null;
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblPartSNEdit, txtPartNOEdit, 40, true) );
			manager.Add( new LengthCheck(lblSPTypeEdit, drpSPTypeEdit, 40, true) );
			manager.Add( new DecimalCheck(lblReturnTPEdit, txtReturnTPEdit, 0, Decimal.MaxValue, true) );
			manager.Add( new DecimalCheck(lblUnOpenTPEdit, txtUnOpenTPEdit, 0, Decimal.MaxValue, true) );
			manager.Add( new DecimalCheck(lblOpenTPEdit, txtOpenTPEdit, 0, Decimal.MaxValue, true) );
			manager.Add( new DecimalCheck(lblGuanPerEdit, txtGuanPerEdit, 0, Decimal.MaxValue, true) );

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
				this.txtPartNOEdit.Text = string.Empty ;
				this.drpSPTypeEdit.SelectedIndex = 0 ;
				this.txtReturnTPEdit.Text = string.Empty ;
				this.txtOpenTPEdit.Text = string.Empty ;
				this.txtUnOpenTPEdit.Text = string.Empty ;
				this.txtGuanPerEdit.Text = string.Empty ;

				return;
			}
			
			SolderPasteControl spc = obj as SolderPasteControl;

			this.txtPartNOEdit.Text = spc.PartNO.ToString() ;
			this.drpSPTypeEdit.SelectedValue = spc.Type.ToString() ;
			this.txtReturnTPEdit.Text = spc.ReturnTimeSpan.ToString("##.##") ;
			this.txtOpenTPEdit.Text = spc.OpenTimeSpan.ToString("##.##") ;
			this.txtUnOpenTPEdit.Text = spc.UnOpenTimeSpan.ToString("##.##") ;
			this.txtGuanPerEdit.Text = spc.GuaranteePeriod.ToString("##.##") ;
		}

		protected DataRow GetGridRow(object obj)
		{
			SolderPasteControl spc = obj as SolderPasteControl;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{   "false",
            //                    spc.PartNO.ToString(),
            //                    this.languageComponent1.GetString( spc.Type ),
            //                    spc.ReturnTimeSpan.ToString("##.##")+this.languageComponent1.GetString("Hour"),
            //                    spc.UnOpenTimeSpan.ToString("##.##")+this.languageComponent1.GetString("Hour"),
            //                    spc.OpenTimeSpan.ToString("##.##")+this.languageComponent1.GetString("Hour"),
            //                    spc.GuaranteePeriod.ToString("##.##")+this.languageComponent1.GetString("Month"),
            //                    spc.MaintainUser.ToString(),
            //                    FormatHelper.ToDateString(spc.MaintainDate),
            //                    FormatHelper.ToTimeString(spc.MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["SolderPastePartNO"] = spc.PartNO.ToString();
            row["SolderPasteType"] = this.languageComponent1.GetString(spc.Type);
            row["ReturnTimeSpan"] = spc.ReturnTimeSpan.ToString("##.##") + this.languageComponent1.GetString("Hour");
            row["UnOpenTimeSpan"] = spc.UnOpenTimeSpan.ToString("##.##") + this.languageComponent1.GetString("Hour");
            row["OpenTimeSpan"] = spc.OpenTimeSpan.ToString("##.##") + this.languageComponent1.GetString("Hour");
            row["GuaranteePeriod"] = spc.GuaranteePeriod.ToString("##.##") + this.languageComponent1.GetString("Month");
            row["MUSER"] = spc.MaintainUser.ToString();
            row["MDATE"] = FormatHelper.ToDateString(spc.MaintainDate);
            row["MTIME"] = FormatHelper.ToTimeString(spc.MaintainTime);
            return row;

		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if( _solderPasteFacade==null )
			{
				_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();
			}

			return _solderPasteFacade.QuerySolderPasteControl(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtPartNOQuery.Text)),
				drpSPTypeQuery.SelectedValue,
				inclusive, exclusive);
		}
	
		private int GetRowCount()
		{
			if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}
			return _solderPasteFacade.QuerySolderPasteControlCount(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtPartNOQuery.Text)),
				drpSPTypeQuery.SelectedValue);
		}

		private string[] FormatExportRecord( object obj )
		{
			SolderPasteControl spc = obj as SolderPasteControl;
			return new string[]{   spc.PartNO.ToString(),
								   this.languageComponent1.GetString( spc.Type ),
								   spc.ReturnTimeSpan.ToString("##.##")+this.languageComponent1.GetString("Hour"),
								   spc.UnOpenTimeSpan.ToString("##.##")+this.languageComponent1.GetString("Hour"),
								   spc.OpenTimeSpan.ToString("##.##")+this.languageComponent1.GetString("Hour"),
								   spc.GuaranteePeriod.ToString("##.##")+this.languageComponent1.GetString("Month"),
								   spc.MaintainUser.ToString(),
									FormatHelper.ToDateString(spc.MaintainDate),
									FormatHelper.ToTimeString(spc.MaintainTime)
								  
			};
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"SolderPastePartNO",
									"SolderPasteType",
									"ReturnTimeSpan",	
									"UnOpenTimeSpan",
									"OpenTimeSpan",
									"GuaranteePeriod",
									"MUSER",	
									"MDATE",
									"MTIME"
			                        };
		}

		#endregion

		

	}
}
