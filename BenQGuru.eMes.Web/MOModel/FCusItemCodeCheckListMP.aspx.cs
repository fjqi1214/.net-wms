using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.RMA;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FUserMP 的摘要说明。
	/// </summary>
	public partial class FCusItemCodeCheckListMP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private RMAFacade _facade = null ; 

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
			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				InitialData();
			}
		}

	  	private void InitialData()
		{
			//SystemParameterListBuilder builder = new SystemParameterListBuilder("department","paramcode",base.DataProvider);
			//builder.Build( this.drpDepartmentEdit );
		}


		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
			this.gridHelper.AddColumn("ModelCode","产品别代码",null);
			this.gridHelper.AddColumn("ItemCode","产品代码",null);
			this.gridHelper.AddColumn("CustomerCode","客户代码",null);
			this.gridHelper.AddColumn("CusModelCode","客户机种",null);
			this.gridHelper.AddColumn("CusItemCode","客户料号",null);
			this.gridHelper.AddColumn("Character","特性",null);
			this.gridHelper.AddColumn("MUSER","维护人员",null);
			this.gridHelper.AddColumn("MDATE","维护时间",null);
			this.gridHelper.AddColumn("MTIME","维护日期",null);

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			CusItemCodeCheckList ciccl = obj as CusItemCodeCheckList;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								ciccl.ModelCode.ToString(),
								ciccl.ItemCode.ToString(),								
								ciccl.CustomerCode.ToString(),
								ciccl.CustomerModelCode.ToString(),
								ciccl.CustomerItemCode.ToString(),
								ciccl.Character.ToString(),
								ciccl.MaintainUser.ToString(),
								FormatHelper.ToDateString(ciccl.MaintainDate),
								FormatHelper.ToTimeString(ciccl.MaintainTime),
								""});
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null)
			{
				_facade = new RMAFacade(base.DataProvider);
			}
			return this._facade.QueryCusItemCodeCheckList( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
				"",
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCusCodeQuery.Text)),
				inclusive, exclusive );
		}


		protected override int GetRowCount()
		{
			if(_facade==null)
			{
				_facade = new RMAFacade(base.DataProvider);
			}
			return this._facade.QueryCusItemCodeCheckListCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
				"",
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCusCodeQuery.Text)));
		}

		#endregion

		#region Button

		/// <summary>
		/// 
		/// </summary>
		/// <param name="domainObject"></param>
		protected override void AddDomainObject(object domainObject)
		{
			ItemFacade itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();
            object obj = itemFacade.GetItem(((CusItemCodeCheckList)domainObject).ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
			if(obj==null)
			{
				WebInfoPublish.Publish(this, "$Error_ItemCode_NotCompare", this.languageComponent1);
				return;
			}

			ModelFacade modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();
			object[] obj2 = modelFacade.GetModel2ItemByItemCode(((CusItemCodeCheckList)domainObject).ItemCode);
			if( obj2==null )
			{
				WebInfoPublish.Publish(this, "$Error_ItemCode_NotMaintain_ModelCode", this.languageComponent1);
				return;
			}

			((CusItemCodeCheckList)domainObject).ModelCode = ((Model2Item)obj2[0]).ModelCode;

			if(_facade==null)
			{
				_facade = new RMAFacade(base.DataProvider);
			}
			this._facade.AddCusItemCodeCheckList((CusItemCodeCheckList)domainObject);
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null)
			{
				_facade = new RMAFacade(base.DataProvider);
			}
			this._facade.DeleteCusItemCodeCheckList((CusItemCodeCheckList[])domainObjects.ToArray(typeof(CusItemCodeCheckList)));
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null)
			{
				_facade = new RMAFacade(base.DataProvider);
			}	
			this._facade.UpdateCusItemCodeCheckList((CusItemCodeCheckList)domainObject);
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtModelCodeEdit.ReadOnly = false;
				this.txtItemCodeEdit.Enabled = true;
				this.txtItemCodeEdit.Readonly = false;
				this.txtCusCodeEdit.ReadOnly = false;
			}
			else if ( pageAction == PageActionType.Update )
			{
				this.txtModelCodeEdit.ReadOnly = true;
				this.txtItemCodeEdit.Enabled = false;
				this.txtItemCodeEdit.Readonly = true;
				this.txtCusCodeEdit.ReadOnly = true;
			}
		}

		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			if(_facade==null)
			{
				_facade = new RMAFacade(base.DataProvider);
			}
			CusItemCodeCheckList cicl = this._facade.CreateNewCusItemCodeCheckList();

			cicl.ModelCode		= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModelCodeEdit.Text, 40));
			cicl.ItemCode		= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeEdit.Text, 40));		
			cicl.CustomerCode	= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCusCodeEdit.Text, 40));
			cicl.CustomerModelCode		= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCusModelCodeEdit.Text, 40));
			cicl.CustomerItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCusItemCodeEdit.Text, 40));
			cicl.Character		= FormatHelper.CleanString(this.txtCharacterEdit.Text, 40);
			cicl.MaintainUser	= this.GetUserCode();

			return cicl;
		}


		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{
			if(_facade==null)
			{
				_facade = new RMAFacade(base.DataProvider);
			}
			object obj = _facade.GetCusItemCodeCheckList( 
				row.Cells.FromKey("ItemCode").Text.ToString() , 
				row.Cells.FromKey("ModelCode").Text.ToString(),
				row.Cells.FromKey("CustomerCode").Text.ToString());
			
			if (obj != null)
			{
				return (CusItemCodeCheckList)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtModelCodeEdit.Text = string.Empty;
                this.txtItemCodeEdit.Text = string.Empty;
				this.txtCusCodeEdit.Text = string.Empty;
				this.txtCusModelCodeEdit.Text = string.Empty;
				this.txtCusItemCodeEdit.Text = string.Empty;
				this.txtCharacterEdit.Text = string.Empty;

				return;
			}

			CusItemCodeCheckList ciccl = obj as CusItemCodeCheckList;

			this.txtModelCodeEdit.Text = ciccl.ModelCode.ToString();
			this.txtItemCodeEdit.Text = ciccl.ItemCode.ToString();
			this.txtCusCodeEdit.Text = ciccl.CustomerCode.ToString();
			this.txtCusModelCodeEdit.Text = ciccl.CustomerModelCode.ToString();
			this.txtCusItemCodeEdit.Text = ciccl.CustomerItemCode.ToString();
			this.txtCharacterEdit.Text = ciccl.Character.ToString();
		}

		
		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			//manager.Add( new LengthCheck(this.lblModelCodeEdit, this.txtModelCodeEdit, 40, true) );	
			manager.Add( new LengthCheck(this.lblItemCodeEdit, this.txtItemCodeEdit, 40, true) );	
			manager.Add( new LengthCheck(this.lblCusCodeEdit, this.txtCusCodeEdit, 40, true) );	
			manager.Add( new LengthCheck(this.lblCusModelCodeEdit, this.txtCusModelCodeEdit, 40, true) );	
			manager.Add( new LengthCheck(this.lblCusItemCodeEdit, this.txtCusItemCodeEdit, 40, true) );	
			manager.Add( new LengthCheck(this.lblCharacterEdit, this.txtCharacterEdit, 40, false) );	
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}

			return true;
		}

		#endregion

		#region Export

		protected override string[] FormatExportRecord( object obj )
		{
			CusItemCodeCheckList ciccl = obj as CusItemCodeCheckList;
			return new string[]{   ciccl.ModelCode.ToString(),
								   ciccl.ItemCode.ToString(),								
								   ciccl.CustomerCode.ToString(),
								   ciccl.CustomerModelCode.ToString(),
								   ciccl.CustomerItemCode.ToString(),
								   ciccl.Character.ToString(),
								   ciccl.MaintainUser.ToString(),
								   FormatHelper.ToDateString(ciccl.MaintainDate),
								   FormatHelper.ToTimeString(ciccl.MaintainTime)
			};
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"ModelCode",
									"ItemCode",	
									"CustomerCode",	
									"CusModelCode",
									"CusItemCode",	
									"Character",	
									"MUSER",	
									"MDATE",
									"MTIME"};
		}
		#endregion
	}
}
