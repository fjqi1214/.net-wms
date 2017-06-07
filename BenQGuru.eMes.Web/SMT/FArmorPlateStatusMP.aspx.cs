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
using BenQGuru.eMES.Domain.ArmorPlate;
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
	public partial class FArmorPlateStatusMP : BaseMPageMinus
	{
		protected System.Web.UI.WebControls.CheckBox chbSelectAll;
		private System.ComponentModel.IContainer components;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		private BenQGuru.eMES.SMT.ArmorPlateFacade _apFacade = null ;


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
			this.cmdStartEnd.ServerClick +=new EventHandler(cmdAdd_ServerClick);
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
				InitButtonHelp();
				SetEditObject(null);
				this.InitWebGrid();
				//this.txtAPIDEdit.Attributes.Add("onblur","return CheckEnter();");
		    }
		}
		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			RequestData();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Query );	
		}

		private void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
			object obj = this.GetEditObject();
			if(obj != null)
			{
				if(_apFacade==null){_apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade();}
				object ap = _apFacade.GetArmorPlate( (obj as ArmorPlateStatusChangeList ).ArmorPlateID );
				if(ap==null)
				{
					WebInfoPublish.Publish(this, string.Format( "$ArmorPlateID=[{0}], $Error_ArmorPlate_Not_Maintain", (obj as ArmorPlateStatusChangeList ).ArmorPlateID),languageComponent1);
					return ;
				}

				object apc = _apFacade.GetArmorPlateInUseContol((obj as ArmorPlateStatusChangeList ).ArmorPlateID );
				if(apc!=null)
				{
					WebInfoPublish.Publish(this, string.Format( "$ArmorPlateID=[{0}], $Error_ArmorPlate_Is_InUse", (obj as ArmorPlateStatusChangeList ).ArmorPlateID),languageComponent1);
					return ;
				}

				(obj as ArmorPlateStatusChangeList).BasePlateCode = ( ap as ArmorPlate).BasePlateCode;

				if( string.Compare(( ap as ArmorPlate).Status, ArmorPlateStatus.StartUsing,true)==0 )
				{
					(ap as ArmorPlate).Status = ArmorPlateStatus.EndUsing;
					(obj as ArmorPlateStatusChangeList).Status = ArmorPlateStatus.EndUsing;
					(obj as ArmorPlateStatusChangeList).OldStatus = ArmorPlateStatus.StartUsing;

				}
				else
				{
					(ap as ArmorPlate).Status = ArmorPlateStatus.StartUsing;
					(obj as ArmorPlateStatusChangeList).Status = ArmorPlateStatus.StartUsing;
					(obj as ArmorPlateStatusChangeList).OldStatus = ArmorPlateStatus.EndUsing;
				}
				( ap as ArmorPlate).MaintainUser = this.GetUserCode();
				this._apFacade.AddArmorPlateStatusChangeList(obj as ArmorPlateStatusChangeList);
				this._apFacade.UpdateArmorPlate( ap as ArmorPlate );
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
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
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "ArmorPlateID",			"厂内编号",		null);
			this.gridHelper.AddColumn( "BasePlateCode",			"基板料号",	null);
			this.gridHelper.AddColumn( "BeforeStatus",			"变更前状态",		null);
			this.gridHelper.AddColumn( "AfterStatus",			"变更后状态",		null);
			this.gridHelper.AddColumn( "Memo",					"备注",	null);
			
			this.gridHelper.AddColumn( "MUSER",   "维护人员",null);
			this.gridHelper.AddColumn( "MDATE",   "维护日期",null);
			this.gridHelper.AddColumn( "MTIME",   "维护时间",null);
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		private  object GetEditObject()
		{
			if(	this.ValidateInput())
			{
				if(_apFacade==null){_apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade();}

				ArmorPlateStatusChangeList obj = this._apFacade.CreateNewArmorPlateStatusChangeList();

				obj.OID = Guid.NewGuid().ToString();
				obj.ArmorPlateID = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAPIDEdit.Text, 40));
				obj.Memo = FormatHelper.CleanString(this.txtMenoEdit.Text, 100);
				
				obj.MaintainUser = this.GetUserCode();
				
				return obj;
			}
			else
			{
				return null;
			}
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblAPIDEdit, txtAPIDEdit, 40, true) );
			manager.Add( new LengthCheck(lblMemoEdit, txtMenoEdit, 100, false) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}

		private void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtAPIDEdit.Text = string.Empty ;
				this.txtCurStatusEdit.Text = string.Empty ;
				this.txtMenoEdit.Text = string.Empty ;

				return;
			}
		}

		protected DataRow GetGridRow(object obj)
		{
			ArmorPlateStatusChangeList apscl = obj as ArmorPlateStatusChangeList;
           
            DataRow row = this.DtSource.NewRow();
            row["ArmorPlateID"] = apscl.ArmorPlateID.ToString();
            row["BasePlateCode"] = apscl.BasePlateCode.ToString();
            row["BeforeStatus"] = languageComponent1.GetString(apscl.OldStatus.ToString());
            row["AfterStatus"] = languageComponent1.GetString(apscl.Status.ToString());
            row["Memo"] = apscl.Memo.ToString();
            row["MUSER"] = apscl.MaintainUser.ToString();
            row["MDATE"] = FormatHelper.ToDateString(apscl.MaintainDate);
            row["MTIME"] = FormatHelper.ToTimeString(apscl.MaintainTime);
            return row;

		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if( _apFacade==null )
			{
				_apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade();
			}

			return _apFacade.QueryArmorPlateStatusChangeList(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtAPIDQuery.Text)),
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtBPCodeQuery.Text)),
				inclusive, exclusive);
		}
	
		private int GetRowCount()
		{
			if(_apFacade==null){_apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade();}
			return _apFacade.QueryArmorPlateStatusChangeListCount(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtAPIDQuery.Text)),
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtBPCodeQuery.Text))
				);
		}

		private string[] FormatExportRecord( object obj )
		{
			ArmorPlateStatusChangeList apscl = obj as ArmorPlateStatusChangeList;
			return new string[]{   
								   apscl.ArmorPlateID.ToString(),
								   apscl.BasePlateCode.ToString(),
								   languageComponent1.GetString( apscl.OldStatus.ToString() ),
								   languageComponent1.GetString( apscl.Status.ToString() ),
								   apscl.Memo.ToString(),
								   apscl.MaintainUser.ToString(),
								   FormatHelper.ToDateString(apscl.MaintainDate),
								   FormatHelper.ToTimeString(apscl.MaintainTime)
							   };
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	"ArmorPlateID",
									"BasePlateCode",
									"BeforeStatus",	
									"AfterStatus",
									"Memo",
									"MUSER",	
									"MDATE",
									"MTIME"
			                        };
		}

		#endregion

		protected void cmdCheck_ServerClick(object sender, EventArgs e)
		{
			string apid = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtAPIDEdit.Text, 40));
			if(apid.Length>0)
			{
				if(_apFacade==null){_apFacade = new SMTFacadeFactory(base.DataProvider).CreateArmorPlateFacade();}
				object ap = _apFacade.GetArmorPlate( apid );
				if(ap==null)
				{
					//this.txtAPIDEdit.Text = string.Empty;
					this.txtCurStatusEdit.Text = string.Empty;
					WebInfoPublish.Publish(this, string.Format( "$ArmorPlateID=[{0}],$Error_ArmorPlate_Not_Maintain", apid),languageComponent1);
					return ;
				}

				this.txtCurStatusEdit.Text = languageComponent1.GetString( (ap as ArmorPlate).Status.ToString() );
			}
		}
	}
}
