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
using BenQGuru.eMES.MOModel;
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
	public partial class FSolderPaste2ItemMP : BaseMPageMinus
	{
		private System.ComponentModel.IContainer components;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		protected System.Web.UI.WebControls.Label lblPartNOEdit;
		protected System.Web.UI.WebControls.TextBox txtPartNOEdit;
		protected System.Web.UI.WebControls.Label lblReturnTPEdit;
		protected System.Web.UI.WebControls.TextBox txtReturnTPEdit;
		protected System.Web.UI.WebControls.Label lblUnOpenTPEdit;
		protected System.Web.UI.WebControls.TextBox txtUnOpenTPEdit;
		protected System.Web.UI.WebControls.Label lblOpenTPEdit;
		protected System.Web.UI.WebControls.TextBox txtOpenTPEdit;
		protected System.Web.UI.WebControls.Label lblGuanPerEdit;
		protected System.Web.UI.WebControls.TextBox txtGuanPerEdit;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		
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
			object[] objs = this.GetEditObject();
			if(objs != null && objs.Length>0)
			{
				if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}

				ArrayList array = new ArrayList( objs ) ;
				this._solderPasteFacade.AddSolderPaste2ItemWithTransaction((SolderPaste2Item[])array.ToArray(typeof(SolderPaste2Item)));

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
						objs.Add( (SolderPaste2Item)obj );
					}
				}
				
				this._solderPasteFacade.DeleteSolderPaste2Item( (SolderPaste2Item[])objs.ToArray( typeof(SolderPaste2Item) ) );
				
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
			}
		}

		private void cmdCancel_ServerClick(object sender, System.EventArgs e)
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
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
		}

		private void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			this.txtItemEdit.Text = string.Empty;
			this.drpSPTypeEdit.SelectedIndex = 0;			
		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "SolderPasteType",			"锡膏类型",		null);
			this.gridHelper.AddColumn( "ItemCode",			"产品代码",	null);

			this.gridHelper.AddColumn( "MUSER",   "维护人员",null);
			this.gridHelper.AddColumn( "MDATE",   "维护日期",null);
			this.gridHelper.AddColumn( "MTIME",   "维护时间",null);
			
			this.gridHelper.AddDefaultColumn( true, false );
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
                    this.drpSPTypeEdit.Items.Add(new ListItem(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterDescription, ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterAlias));
                    this.drpSPTypeQuery.Items.Add(new ListItem(((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterDescription, ((BenQGuru.eMES.Domain.BaseSetting.Parameter)obj).ParameterAlias));
                }
            }
		}

		private  object[] GetEditObject()
		{
			if(	this.ValidateInput())
			{
				if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}

				string[] items = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemEdit.Text, int.MaxValue)).Split(',');
 
				if(items!=null && items.Length>0)
				{
					ItemFacade itemFacade = new SMTFacadeFactory(base.DataProvider).CreateItemFacade();
					object[] spis = new object[items.Length];

					for(int i=0; i<items.Length; i++)
					{
						SolderPaste2Item obj = this._solderPasteFacade.CreateNewSolderPaste2Item();

						obj.ItemCode = items[i];
                        object item = itemFacade.GetItem(obj.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
						if(item==null)
						{
							ExceptionManager.Raise( this.GetType(), string.Format("$Domain_ItemCode=[{0}], $Error_Not_Maintain", obj.ItemCode ) );
							return null;
						}

						object sp2Item = _solderPasteFacade.GetSolderPaste2Item( obj.ItemCode );
						if(sp2Item!=null)
						{
							ExceptionManager.Raise( this.GetType(), string.Format("$Domain_ItemCode=[{0}], $Error_SPType_Maintain${1}", 
								obj.ItemCode, (sp2Item as SolderPaste2Item).SolderPasteType ) );
							return null;
						}

                        ///* 3.1	无铅锡膏可以用于产品代码编码中第二码是字母的所有产品 */
                        //if( string.Compare( drpSPTypeEdit.SelectedValue, SolderPasteType.Pb_Free, true )==0 
                        //    && (!char.IsLetter( obj.ItemCode, 1 ))
                        //    && char.IsDigit( obj.ItemCode, 1 ) )
                        //{
                        //    ExceptionManager.Raise( this.GetType(), string.Format("$Domain_ItemCode=[{0}], $Error_SPType_Should_Maintain${1}", 
                        //        obj.ItemCode, SolderPasteType.Pb ) );
                        //    return null;
                        //}
                        ///* 3.2	有铅锡膏可以用于产品代码编码中第二码是数字的所有产品 */
                        //else if( string.Compare( drpSPTypeEdit.SelectedValue, SolderPasteType.Pb, true )==0 
                        //    && (!char.IsDigit( obj.ItemCode, 1 ))
                        //    && char.IsLetter( obj.ItemCode, 1 ))
                        //{
                        //    ExceptionManager.Raise( this.GetType(), string.Format("$Domain_ItemCode=[{0}], $Error_SPType_Should_Maintain${1}", 
                        //        obj.ItemCode, SolderPasteType.Pb_Free ) );
                        //    return null;
                        //}

						obj.SolderPasteType = drpSPTypeEdit.SelectedValue ;
						obj.MaintainUser = this.GetUserCode();

						spis[i] = obj;
					}
				
					return spis;
				}

				return null;
			}
			else
			{
				return null;
			}
		}

		private object GetEditObject(GridRecord row)
		{	
			if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}

			object obj = this._solderPasteFacade.GetSolderPaste2Item(row.Items.FindItemByKey("ItemCode").Text.ToString());
			
			if (obj != null)
			{
				return (SolderPaste2Item)obj;
			}

			return null;
		}

		private bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblSPTypeEdit, drpSPTypeEdit, 40, true) );
			manager.Add( new LengthCheck(lblItemCodeEdit, txtItemEdit, int.MaxValue, true) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,languageComponent1);
				return false;
			}
			return true;
		}

		protected DataRow GetGridRow(object obj)
		{
			SolderPaste2Item spi = obj as SolderPaste2Item;
          
            DataRow row = this.DtSource.NewRow();
            row["SolderPasteType"] = this.languageComponent1.GetString(spi.SolderPasteType);
            row["ItemCode"] = spi.ItemCode;
            row["MUSER"] = spi.MaintainUser.ToString();
            row["MDATE"] = FormatHelper.ToDateString(spi.MaintainDate);
            row["MTIME"] = FormatHelper.ToTimeString(spi.MaintainTime);
            return row;

		}

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if( _solderPasteFacade==null )
			{
				_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();
			}

			return _solderPasteFacade.QuerySolderPaste2Item(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtItemQuery.Text)),
				drpSPTypeQuery.SelectedValue,
				inclusive, exclusive);
		}
	
		private int GetRowCount()
		{
			if(_solderPasteFacade==null){_solderPasteFacade = new SMTFacadeFactory(base.DataProvider).CreateSolderPasteFacade();}
			return _solderPasteFacade.QuerySolderPaste2ItemCount(
				FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtItemQuery.Text)),
				drpSPTypeQuery.SelectedValue);
		}

		private string[] FormatExportRecord( object obj )
		{
			SolderPaste2Item spi = obj as SolderPaste2Item;
			return new string[]{   
									this.languageComponent1.GetString( spi.SolderPasteType ),
									spi.ItemCode,
									spi.MaintainUser.ToString(),
									FormatHelper.ToDateString(spi.MaintainDate),
									FormatHelper.ToTimeString(spi.MaintainTime)
								  
								};
		}

		private string[] GetColumnHeaderText()
		{
			return new string[] {	
									"SolderPasteType",
									"ItemCode",
									"MUSER",	
									"MDATE",
									"MTIME"
			                        };
		}

		#endregion

		

	}
}
