#region system
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
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// ItemMP 的摘要说明。
	/// </summary>
	public partial class FMORCardRangeMP : BasePage
	{
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		//private ItemFacade _itemFacade;// = FacadeFactory.CreateItemFacade();
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		
		private BenQGuru.eMES.MOModel.MORunningCardFacade _morcardFacade = null ;


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
			this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";
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
				this.txtMOCode.Text = this.GetRequestParam("mocode");
				if (this.txtMOCode.Text.Trim() == string.Empty)
				{
					throw new Exception("$Error_MOCode_NULL");
				}
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
				// 初始化界面UI
				this.InitUI();
				InitButtonHelp();
				SetEditObject(null);
				this.InitWebGrid();
				this.cmdQuery_ServerClick(null, null);
		    }
		}
		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			RequestData();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Query );	
		}

		private void gridWebGrid_DblClick(object sender,Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
		{
			object obj = this.GetEditObject(e.Row);

			if ( obj != null )
			{
				this.SetEditObject( obj );

				this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
			}
		}

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
		    object rcardRange = this.GetEditObject();
			if(rcardRange != null)
			{
				MORunningCardRange objRange = (MORunningCardRange)rcardRange;
				if (objRange.MORunningCardStart.Length != objRange.MORunningCardEnd.Length)
				{
					throw new Exception("$CS_RunningCard_Range_Should_be_Equal_Length");
				}
				if(_morcardFacade==null){_morcardFacade = new MORunningCardFacade(this.DataProvider); }
				this._morcardFacade.AddMORunningCardRange(rcardRange as MORunningCardRange);
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			}
		}

		protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
		{
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{

				ArrayList morcardRangeList = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					object morcardRange = this.GetEditObject(row);
					if( morcardRange != null )
					{
						morcardRangeList.Add( (MORunningCardRange)morcardRange );
					}
				}

				if(_morcardFacade==null){_morcardFacade = new MORunningCardFacade(this.DataProvider); }
				this._morcardFacade.DeleteMORunningCardRange( (MORunningCardRange[])morcardRangeList.ToArray( typeof(MORunningCardRange) ) );

				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
				
			}
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_morcardFacade==null){_morcardFacade = new MORunningCardFacade(this.DataProvider); }
			object morcardRange = this.GetEditObject();

			if(morcardRange != null)
			{
				MORunningCardRange objRange = (MORunningCardRange)morcardRange;
				if (objRange.MORunningCardStart.Length != objRange.MORunningCardEnd.Length)
				{
					throw new Exception("$CS_RunningCard_Range_Should_be_Equal_Length");
				}
				this._morcardFacade.UpdateMORunningCardRange(morcardRange as MORunningCardRange);
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
			}
		}

		protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
		{
			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );
		}

		private void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( this.chbSelectAll.Checked )
			{
				this.gridHelper.CheckAllRows( CheckStatus.Checked );
			}
			else
			{
				this.gridHelper.CheckAllRows( CheckStatus.Unchecked );
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

			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

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
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn("MOCode","工单",null);
			this.gridHelper.AddColumn("Sequence","次序",null);
			this.gridHelper.AddColumn("MORunningCardType","类型",null);
			this.gridHelper.AddColumn("MORunningCardStart","起始序列号",null);
			this.gridHelper.AddColumn("MORunningCardEnd","结束序列号",null);
			this.gridHelper.AddColumn("MUSER","维护人员",null);
			this.gridHelper.AddColumn("MDATE","维护日期",null);		

			this.gridWebGrid.Columns.FromKey("Sequence").Hidden = true;

			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private  object GetEditObject()
		{
			if(	this.ValidateInput())
			{
				if(_morcardFacade==null){_morcardFacade = new MORunningCardFacade(this.DataProvider); }

				MORunningCardRange rcardRange = this._morcardFacade.CreateNewMORunningCardRange();

				rcardRange.MOCode = this.txtMOCode.Text.Trim().ToUpper();
				try
				{
					rcardRange.Sequence = decimal.Parse(this.txtSeq.Text);
				}
				catch {}
				rcardRange.RunningCardType = this.drpRCardTypeEdit.SelectedValue;
				rcardRange.MORunningCardStart = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRCardStartEdit.Text, 40));
				rcardRange.MORunningCardEnd = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRCardEndEdit.Text, 40));
				
				rcardRange.MaintainUser = this.GetUserCode();
				
				return rcardRange;
			}
			else
			{
				return null;
			}
		}

		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_morcardFacade==null){_morcardFacade = new MORunningCardFacade(this.DataProvider); }
			object obj = this._morcardFacade.GetMORunningCardRange(decimal.Parse(row.Cells.FromKey("Sequence").Text.ToString()), row.Cells.FromKey("MOCode").Text.ToString());
			
			if (obj != null)
			{
				return (MORunningCardRange)obj;
			}

			return null;
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck( lblMORCardStartEdit, txtRCardStartEdit, 40, true) );
			manager.Add( new LengthCheck( lblMORCardEndEdit, txtRCardEndEdit, 40, true) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}

		private void SetEditObject(object obj)
		{
			if(_morcardFacade==null){_morcardFacade = new MORunningCardFacade(this.DataProvider); }
			if (obj == null)
			{
				this.txtSeq.Text = "0";
				this.drpRCardTypeEdit.SelectedIndex = 0;
				this.txtRCardStartEdit.Text = string.Empty;
				this.txtRCardEndEdit.Text = string.Empty;
				return;
			}

			MORunningCardRange rcardRange = (MORunningCardRange)obj;
			this.txtSeq.Text = rcardRange.Sequence.ToString();
			this.drpRCardTypeEdit.SelectedIndex = this.drpRCardTypeEdit.Items.IndexOf(this.drpRCardTypeEdit.Items.FindByValue(rcardRange.RunningCardType));
			this.txtRCardStartEdit.Text = rcardRange.MORunningCardStart;
			this.txtRCardEndEdit.Text = rcardRange.MORunningCardEnd;
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			MORunningCardRange rcardRange = (MORunningCardRange)obj;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								"false",
								rcardRange.MOCode,
								rcardRange.Sequence,
								this.drpRCardTypeEdit.Items.FindByValue(rcardRange.RunningCardType).Text,
								rcardRange.MORunningCardStart,
								rcardRange.MORunningCardEnd,
								rcardRange.MaintainUser,
								FormatHelper.ToDateString(rcardRange.MaintainDate),
								""});
		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_morcardFacade==null){_morcardFacade = new MORunningCardFacade(this.DataProvider); }
			return this._morcardFacade.QueryMORunningCardRange(this.txtMOCode.Text.Trim().ToUpper(), this.drpRCardTypeQuery.SelectedValue, inclusive, exclusive);
			
		}

		private int GetRowCount()
		{
			if(_morcardFacade==null){_morcardFacade = new MORunningCardFacade(this.DataProvider); }
			return this._morcardFacade.QueryMORunningCardRangeCount(this.txtMOCode.Text.Trim().ToUpper(), this.drpRCardTypeQuery.SelectedValue);
		}

		private string[] FormatExportRecord( object obj )
		{
			MORunningCardRange rcardRange = (MORunningCardRange)obj;
			return new string[]{
								   rcardRange.MOCode,
								   rcardRange.Sequence.ToString(),
								   this.drpRCardTypeEdit.Items.FindByValue(rcardRange.RunningCardType).Text,
								   rcardRange.MORunningCardStart,
								   rcardRange.MORunningCardEnd,
								   rcardRange.MaintainUser,
								   FormatHelper.ToDateString(rcardRange.MaintainDate)
							   };
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"MOCode",
									"Sequence",
									"MORunningCardType",
									"MORunningCardStart",
									"MORunningCardEnd",
									"MUSER",	
									"MDATE"
			                        };
		}
		#endregion

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if(e.Cell.Column.Key =="Edit")
			{
				object obj = this.GetEditObject(e.Cell.Row);

				if ( obj != null )
				{
					this.SetEditObject( obj );

					this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
				}
			}
		}

		protected void cmdReturn_ServerClick(object sender, EventArgs e)
		{
			if (this.GetRequestParam("backurl") != string.Empty)
			{
				string strUrl = this.GetRequestParam("backurl");
				Response.Redirect(strUrl);
			}
		}
	}
}
