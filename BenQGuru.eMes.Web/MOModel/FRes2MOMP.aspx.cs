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
	/// FRes2MOMP 的摘要说明。
	/// </summary>
	public partial class FRes2MOMP : BasePage
	{
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		protected System.Web.UI.WebControls.TextBox txtMOCode;
		protected BenQGuru.eMES.Web.UserControl.eMESDate dateStartDateEdit;
		protected BenQGuru.eMES.Web.UserControl.eMESTime dateStartTimeEdit;
		protected BenQGuru.eMES.Web.UserControl.eMESDate dateEndDateEdit;
		protected BenQGuru.eMES.Web.UserControl.eMESTime dateEndTimeEdit;
		protected BenQGuru.eMES.Web.UserControl.eMESDate txtDateFrom;
		protected BenQGuru.eMES.Web.UserControl.eMESDate txtDateTo;
		
		private BenQGuru.eMES.MOModel.MOFacade _moFacade = null ;


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
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

		}
		#endregion

		#region form events
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitOnPostBack();
		    if (!IsPostBack)
		    {	
				this.drpMOGetTypeEdit.Attributes.Add("onchange", "HideShowMOType();");
				
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
				// 初始化界面UI
				this.InitUI();
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

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
		    object res2mo = this.GetEditObject();
			if(res2mo != null && CheckResourceRepeat((Resource2MO)res2mo, true) == true)
			{
				if(_moFacade==null){_moFacade = new MOFacade(this.DataProvider); }
				this._moFacade.AddResource2MO(res2mo as Resource2MO);
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

				ArrayList res2moList = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					object res2mo = this.GetEditObject(row);
					if( res2mo != null )
					{
						res2moList.Add( (Resource2MO)res2mo );
					}
				}

				if(_moFacade==null){_moFacade = new MOFacade(this.DataProvider); }
				this._moFacade.DeleteResource2MO( (Resource2MO[])res2moList.ToArray( typeof(Resource2MO) ) );

				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
				
			}
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if(_moFacade==null){_moFacade = new MOFacade(this.DataProvider); }
			object res2mo = this.GetEditObject();

			if(res2mo != null && CheckResourceRepeat((Resource2MO)res2mo, false) == true)
			{
				this._moFacade.UpdateResource2MO(res2mo as Resource2MO);
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
			if(pageAction == PageActionType.Add)
			{
				this.txtResourceCodeEdit.ReadOnly = false;
			}
			else if ( pageAction == PageActionType.Update )
			{
				this.txtResourceCodeEdit.ReadOnly = true;
			}
			else if ( pageAction == PageActionType.Save )
			{
				this.txtResourceCodeEdit.ReadOnly = false;
			}
			else if(pageAction ==PageActionType.Cancel)
			{
				this.txtResourceCodeEdit.ReadOnly = false;
			}
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn("Sequence","序号",null);
			this.gridHelper.AddColumn("ResourceCode","资源代码",null);
			this.gridHelper.AddColumn("StartDate","起始日期",null);
			this.gridHelper.AddColumn("StartTime","起始时间",null);
			this.gridHelper.AddColumn("EndDate","结束日期",null);
			this.gridHelper.AddColumn("EndTime1","结束时间",null);
			this.gridHelper.AddColumn("MOGetType","工单获取方式",null);
			this.gridHelper.AddColumn("MOCode","工单代码",null);
			this.gridHelper.AddColumn("RCardPrefix","序列号前缀",null);
			this.gridHelper.AddColumn("RCardLength","序列号长度",null);
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
				if(_moFacade==null){_moFacade = new MOFacade(this.DataProvider); }

				Resource2MO res2mo = this._moFacade.CreateNewResource2MO();

				if (this.txtSequenceEdit.Text != "")
				{
					try
					{
						res2mo.Sequence = decimal.Parse(this.txtSequenceEdit.Text);
					}
					catch {}
				}
				res2mo.ResourceCode = this.txtResourceCodeEdit.Text.ToUpper().Trim();
				res2mo.StartDate = FormatHelper.TODateInt(this.dateStartDateEdit.Text);
				if (res2mo.StartDate > 0)
					res2mo.StartTime = FormatHelper.TOTimeInt(this.dateStartTimeEdit.Text);
				res2mo.EndDate = FormatHelper.TODateInt(this.dateEndDateEdit.Text);
				if (res2mo.EndDate > 0)
				{
					res2mo.EndTime = FormatHelper.TOTimeInt(this.dateEndTimeEdit.Text);
					if (res2mo.EndTime == 0)
						res2mo.EndTime = 235959;
				}
				res2mo.MOGetType = this.drpMOGetTypeEdit.SelectedValue;
				if (res2mo.MOGetType == Resource2MOGetType.Static)
					res2mo.StaticMOCode = this.txtMOCodeEdit.Text.ToUpper().Trim();
				else if (res2mo.MOGetType == Resource2MOGetType.GetFromRCard)
				{
					res2mo.MOCodeRunningCardStartIndex = decimal.Parse(this.txtMORCardStartIndexEdit.Text);
					res2mo.MOCodeLength = decimal.Parse(this.txtMOLendthEdit.Text);
					res2mo.MOCodePrefix = this.txtMOCodePrefix.Text.ToUpper().Trim();
					res2mo.MOCodePostfix = this.txtMOCodePostfix.Text.ToUpper().Trim();
				}
				res2mo.CheckRunningCardFormat = FormatHelper.BooleanToString(this.chkCheckRCardFormatEdit.Checked);
				if (this.chkCheckRCardFormatEdit.Checked == true)
				{
					res2mo.RunningCardPrefix = this.txtRCardPrefixEdit.Text.ToUpper().Trim();
					if (this.txtRCardLengthEdit.Text != "")
					{
						try
						{
							res2mo.RunningCardLength = decimal.Parse(this.txtRCardLengthEdit.Text);
						}
						catch {}
					}
				}
				
				res2mo.MaintainUser = this.GetUserCode();
				
				return res2mo;
			}
			else
			{
				return null;
			}
		}

		private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_moFacade==null){_moFacade = new MOFacade(this.DataProvider); }
			object obj = this._moFacade.GetResource2MO(decimal.Parse(row.Cells.FromKey("Sequence").Text.ToString()));
			
			if (obj != null)
			{
				return (Resource2MO)obj;
			}

			return null;
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();
			manager.Add( new LengthCheck( this.lblResourceCodeEdit, this.txtResourceCodeEdit, 40, true) );
			if (this.drpMOGetTypeEdit.SelectedValue == Resource2MOGetType.Static)
				manager.Add( new LengthCheck( this.lblMOCodeEdit, this.txtMOCodeEdit, 40, true) );
			else if (this.drpMOGetTypeEdit.SelectedValue == Resource2MOGetType.GetFromRCard)
			{
				manager.Add( new DecimalCheck(this.lblMORCardStartIndexEdit, this.txtMORCardStartIndexEdit, 1, decimal.MaxValue, true));
				manager.Add( new DecimalCheck(this.lblMOLendthRCardEdit, this.txtMOLendthEdit, 1, decimal.MaxValue, true));
			}


			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			// 检查资源正确性
			BenQGuru.eMES.BaseSetting.BaseModelFacade modelFacade = new BaseModelFacade(this.DataProvider);
			object objTmp = modelFacade.GetResource(this.txtResourceCodeEdit.Text.ToUpper().Trim());
			if (objTmp == null)
			{
				ExceptionManager.Raise(this.GetType(), "$Error_Resource_Not_Exist");
				return false;
			}
			// 检查工单正确性
			if (this.drpMOGetTypeEdit.SelectedValue == Resource2MOGetType.Static)
			{
				if(_moFacade==null){_moFacade = new MOFacade(this.DataProvider); }
				objTmp = _moFacade.GetMO(this.txtMOCodeEdit.Text.ToUpper().Trim());
				if (objTmp == null)
				{
					ExceptionManager.Raise(this.GetType(), "$Error_Input_MoCode");
					return false;
				}
			}
			return true;
		}
		private bool CheckResourceRepeat(Resource2MO res2mo, bool isNew)
		{
			// 检查时间范围是否有交叉
			object[] objsExist = _moFacade.QueryResource2MOByResourceDate(
				res2mo.ResourceCode,
				res2mo.StartDate,
				res2mo.StartTime,
				res2mo.EndDate,
				res2mo.EndTime
				);
			if (objsExist != null && objsExist.Length > 0)
			{
				bool bError = false;
				if (objsExist.Length > 1 || isNew == true)
					bError = true;
				else
				{
					Resource2MO res2mo1 = (Resource2MO)objsExist[0];
					if (Convert.ToInt32(res2mo.Sequence) != Convert.ToInt32(res2mo1.Sequence))
					{
						bError = true;
					}
				}
				if (bError == true)
				{
					ExceptionManager.Raise(this.GetType(), "$Error_Resource2MO_Date_Repeat");
					return false;
				}
			}
			return true;
		}

		private void SetEditObject(object obj)
		{
			if(_moFacade==null){_moFacade = new MOFacade(this.DataProvider); }
			if (obj == null)
			{
				this.txtSequenceEdit.Text = "";
				this.txtResourceCodeEdit.Text = "";
				this.dateStartDateEdit.Text = "";
				this.dateStartTimeEdit.Text = "0";
				this.dateEndDateEdit.Text = "";
				this.dateEndTimeEdit.Text = "0";
				this.drpMOGetTypeEdit.SelectedIndex = 0;
				this.txtMOCodeEdit.Text = "";
				this.txtMORCardStartIndexEdit.Text = "";
				this.txtMOLendthEdit.Text = "";
				this.txtMOCodePrefix.Text = "";
				this.txtMOCodePostfix.Text = "";
				this.chkCheckRCardFormatEdit.Checked = false;
				this.txtRCardPrefixEdit.Text = "";
				this.txtRCardLengthEdit.Text = "";
				return;
			}

			Resource2MO res2mo = (Resource2MO)obj;
			this.txtSequenceEdit.Text = res2mo.Sequence.ToString();
			this.txtResourceCodeEdit.Text = res2mo.ResourceCode;
			this.dateStartDateEdit.Text = FormatHelper.ToDateString(res2mo.StartDate);
			this.dateStartTimeEdit.Text = FormatHelper.ToTimeString(res2mo.StartTime);
			this.dateEndDateEdit.Text = FormatHelper.ToDateString(res2mo.EndDate);
			this.dateEndTimeEdit.Text = FormatHelper.ToTimeString(res2mo.EndTime);
			this.drpMOGetTypeEdit.SelectedValue = res2mo.MOGetType;
			this.txtMOCodeEdit.Text = res2mo.StaticMOCode;
			this.txtMORCardStartIndexEdit.Text = res2mo.MOCodeRunningCardStartIndex.ToString();
			this.txtMOLendthEdit.Text = res2mo.MOCodeLength.ToString();
			this.txtMOCodePrefix.Text = res2mo.MOCodePrefix;
			this.txtMOCodePostfix.Text = res2mo.MOCodePostfix;
			this.chkCheckRCardFormatEdit.Checked = FormatHelper.StringToBoolean(res2mo.CheckRunningCardFormat);
			this.txtRCardPrefixEdit.Text = res2mo.RunningCardPrefix;
			this.txtRCardLengthEdit.Text = res2mo.RunningCardLength.ToString();
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			Resource2MO res2mo = (Resource2MO)obj;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								"false",
								res2mo.Sequence,
								res2mo.ResourceCode,
								FormatHelper.ToDateString(res2mo.StartDate),
								FormatHelper.ToTimeString(res2mo.StartTime),
								FormatHelper.ToDateString(res2mo.EndDate),
								FormatHelper.ToTimeString(res2mo.EndTime),
								this.drpMOGetTypeEdit.Items.FindByValue(res2mo.MOGetType).Text,
								(res2mo.MOGetType == Resource2MOGetType.Static ? res2mo.StaticMOCode : res2mo.MOCodePrefix + (new string('_', Convert.ToInt32(res2mo.MOCodeLength))) + res2mo.MOCodePostfix),
								res2mo.RunningCardPrefix,
								res2mo.RunningCardLength,
                                //res2mo.MaintainUser,
                                 ((Resource2MO)obj).GetDisplayText("MaintainUser"),
								FormatHelper.ToDateString(res2mo.MaintainDate),
								""
							} );
		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_moFacade==null){_moFacade = new MOFacade(this.DataProvider); }
			return this._moFacade.QueryResource2MO(
				this.txtResourceCodeQuery.Text.Trim().ToUpper(), 
				FormatHelper.TODateInt(this.txtDateFrom.Text),
				FormatHelper.TODateInt(this.txtDateTo.Text),
				inclusive, exclusive);
			
		}

		private int GetRowCount()
		{
			if(_moFacade==null){_moFacade = new MOFacade(this.DataProvider); }
			return this._moFacade.QueryResource2MOCount(
				this.txtResourceCodeQuery.Text.Trim().ToUpper(), 
				FormatHelper.TODateInt(this.txtDateFrom.Text),
				FormatHelper.TODateInt(this.txtDateTo.Text)
				);
		}

		private string[] FormatExportRecord( object obj )
		{
			Resource2MO res2mo = (Resource2MO)obj;
			return new string[]{
								   res2mo.ResourceCode,
								   FormatHelper.ToDateString(res2mo.StartDate),
								   FormatHelper.ToTimeString(res2mo.StartTime),
								   FormatHelper.ToDateString(res2mo.EndDate),
								   FormatHelper.ToTimeString(res2mo.EndTime),
								   this.drpMOGetTypeEdit.Items.FindByValue(res2mo.MOGetType).Text,
								   (res2mo.MOGetType == Resource2MOGetType.Static ? res2mo.StaticMOCode : res2mo.MOCodePrefix + (new string('_', Convert.ToInt32(res2mo.MOCodeLength))) + res2mo.MOCodePostfix),
								   res2mo.RunningCardPrefix,
								   res2mo.RunningCardLength.ToString(),
                                   //res2mo.MaintainUser,
                    ((Resource2MO)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(res2mo.MaintainDate)
							   };
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"ResourceCode",
									"StartDate",
									"StartTime",
									"EndDate",
									"EndTime",
									"MOGetType",
									"MOCode",
									"RCardPrefix",
									"RCardLength",
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

	}
}
