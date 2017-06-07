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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Web.MOModel
{
    public partial class FItem2SPCTestMP : BaseMPage
    {		
        private System.ComponentModel.IContainer components;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        protected BenQGuru.eMES.MOModel.ItemFacade _facade ;//= new ItemFacade();
		private string _itemCode = "";
		private string _itemName = "";
	
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
			this.languageComponent1.LanguagePackageDir = "\\\\..";
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
			}

			SessionHelper ssh = SessionHelper.Current(this.Session);
			_itemCode = (string)ssh.LoadStoredObject("spc2itemcode");
			_itemName = (string)ssh.LoadStoredObject("spc2itemname");

			TextBoxEnableChg();
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		#endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
			this.gridHelper.AddColumn("OID","OID",null);
            this.gridHelper.AddColumn( "ItemName", "产品名称",	null);
            this.gridHelper.AddColumn( "TestName", "测试项",	null);
			this.gridHelper.AddColumn( "Seq", "序号",	null);
			this.gridHelper.AddColumn( "USL", "USL",	null);
			this.gridHelper.AddColumn( "LSL", "LSL",	null);
			this.gridHelper.AddColumn( "UCL", "UCL",	null);
			this.gridHelper.AddColumn( "LCL", "LCL",	null);
			this.gridHelper.AddColumn( "Desc", "备注",	null);
            this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);
            this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridWebGrid.Columns.FromKey("OID").Hidden = true;

			this.gridHelper.RequestData();
        }
		
        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
			Item2SPCTest spc = obj as Item2SPCTest;
			if(spc != null)
				return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
																		new object[]{"false",
																					spc.OID,
																					_itemName,
																					spc.TestName,
																					spc.Seq,
																					decimaltostr(spc.LowOnly,spc.USL),
																					decimaltostr(spc.UpOnly,spc.LSL),
																					decimaltostr(spc.AutoCL,spc.UCL),
																					decimaltostr(spc.AutoCL,spc.LCL),
																					spc.Description,
																					spc.MaintainUser.ToString(),
																					FormatHelper.ToDateString(spc.MaintainDate)
																					});
			else
				return null;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			if(_facade==null){_facade = new ItemFacade(base.DataProvider);}
            return this._facade.QueryItem2SPCTest( 
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(_itemCode)),
                inclusive, exclusive );
        }


        protected override int GetRowCount()
        {
			if(_facade==null){_facade = new ItemFacade(base.DataProvider);}
            return this._facade.QueryItem2SPCTestCount( 
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(_itemCode))
                );
        }

        #endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new ItemFacade(base.DataProvider);}
			this._facade.AddItem2SPCTest( (Item2SPCTest)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new ItemFacade(base.DataProvider);}
			this._facade.DeleteItem2SPCTTest( (Item2SPCTest[])domainObjects.ToArray( typeof(Item2SPCTest) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new ItemFacade(base.DataProvider);}
			this._facade.UpdateItem2SPCTest( (Item2SPCTest)domainObject);
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				//this.txtErrorCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				//this.txtErrorCodeEdit.ReadOnly = true;
			}
		}
		#endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
			if(_facade==null){_facade = new ItemFacade(base.DataProvider);}
			if(	this.ValidateInput())
			{
				Item2SPCTest spc = this._facade.CreateNewItem2SPCTest();
			
				spc.OID = this.txtOID.Text.Trim()!=String.Empty?this.txtOID.Text:Guid.NewGuid().ToString();
				spc.ItemCode = this._itemCode;
				spc.TestName = this.txtTestNameEdit.Text;
				spc.Seq = int.Parse(this.txtSeqEdit.Text);
				spc.AutoCL = this.chbAutoCLEdit.Checked?"Y":"N";
				spc.LowOnly = this.chbLowEdit.Checked?"Y":"N";
				spc.UpOnly = this.chbUpEdit.Checked?"Y":"N";
				spc.UCL = this.txtUCLEdit.Text==String.Empty?0:decimal.Parse(this.txtUCLEdit.Text);
				spc.LCL = this.txtLCLEdit.Text==String.Empty?0:decimal.Parse(this.txtLCLEdit.Text);
				spc.USL = this.txtUSLEdit.Text==String.Empty?0:decimal.Parse(this.txtUSLEdit.Text);
				spc.LSL = this.txtLSLEdit.Text==String.Empty?0:decimal.Parse(this.txtLSLEdit.Text);
				spc.Description = this.txtDescEdit.Text;
				spc.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
				spc.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
				spc.MaintainUser = this.GetUserCode();

				return spc;
			}
			else
				return null;
        }


        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {	
			if(_facade==null){_facade = new ItemFacade(base.DataProvider);}
            object obj = _facade.GetItem2SPCTest(row.Cells[1].Text.ToString());
            if (obj != null)
            {
                return (Item2SPCTest)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
			if (obj == null)
            {
				this.txtOID.Text			= String.Empty;
				this.txtItemCodeEdit.Text   = this._itemName;
                this.txtTestNameEdit.Text	= String.Empty;
				this.txtSeqEdit.Text		= String.Empty;
				this.txtDescEdit.Text       = String.Empty;
				this.txtUCLEdit.Text        = String.Empty;
				this.txtLCLEdit.Text		= String.Empty;
				this.txtUSLEdit.Text		= string.Empty;
				this.txtLSLEdit.Text		= string.Empty;
				this.chbUpEdit.Checked      = false;
				this.chbLowEdit.Checked		= false;
				this.chbAutoCLEdit.Checked		= false;
				TextBoxEnableChg();
                return;
            }

			Item2SPCTest spc = obj as Item2SPCTest;
			if(spc == null) return;

			this.txtOID.Text			= spc.OID;
			this.txtItemCodeEdit.Text   = this._itemName;
			this.txtTestNameEdit.Text	= spc.TestName;
			this.txtSeqEdit.Text		= spc.Seq.ToString();
			this.txtDescEdit.Text       = spc.Description;
			this.txtUCLEdit.Text        = decimaltostr(spc.AutoCL,spc.UCL);
			this.txtLCLEdit.Text		= decimaltostr(spc.AutoCL,spc.LCL);
			this.txtUSLEdit.Text		= decimaltostr(spc.LowOnly,spc.USL);
			this.txtLSLEdit.Text		= decimaltostr(spc.UpOnly,spc.LSL);
			this.chbAutoCLEdit.Checked		= spc.AutoCL=="Y";
			this.chbLowEdit.Checked		= spc.LowOnly=="Y";
			this.chbUpEdit.Checked		= spc.UpOnly=="Y";
			
			TextBoxEnableChg();

        }

		
        protected override bool ValidateInput()
        {
			if(_facade==null){_facade = new ItemFacade(base.DataProvider);}
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblItemNameEdit,this.txtTestNameEdit,40,true));			
			manager.Add( new NumberCheck(this.lblSeqEdit,this.txtSeqEdit,1,int.MaxValue,true));
			manager.Add(new TestNameUniqueCheck(this._facade,this._itemCode,this.txtTestNameEdit.Text,this.txtOID.Text));
			manager.Add(new SeqUniqueCheck(this.lblSeqEdit.Text,this._facade,this._itemCode,this.txtSeqEdit.Text,this.txtOID.Text));
			if(this.txtUSLEdit.Enabled)
				manager.Add(new DecimalCheck(this.lblUSLEdit,this.txtUSLEdit,true));
			if(this.txtLSLEdit.Enabled)
				manager.Add(new DecimalCheck(this.lblLSLEdit,this.txtLSLEdit,true));
			if(this.txtUCLEdit.Enabled)
				manager.Add(new DecimalCheck(this.lblUCLEdit,this.txtUCLEdit,true));
			if(this.txtLCLEdit.Enabled)
				manager.Add(new DecimalCheck(this.lblLCLEdit,this.txtLCLEdit,true));

			//manager.Add(new DateUniqueCheck(this._facade,
			//								_itemCode,
			//								FormatHelper.TODateInt(this.dateStartDateEdit.Text),
			//								FormatHelper.TODateInt(this.dateEndDateEdit.Text),
			//								this.txtOID.Text));
			//manager.Add(new TableNameCheck(this.txtTableNameEdit.Text));

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
				return false;
			}

			return true;
        }

        #endregion
        
        #region Export 

        protected override string[] FormatExportRecord( object obj )
        {
            return new string[]{
                                   _itemName,
                                   ((Item2SPCTest)obj).TestName,
								   ((Item2SPCTest)obj).Seq.ToString(),
								   ((Item2SPCTest)obj).USL.ToString(),
								   ((Item2SPCTest)obj).LSL.ToString(),
								   ((Item2SPCTest)obj).UCL.ToString(),
								   ((Item2SPCTest)obj).LCL.ToString(),
								   ((Item2SPCTest)obj).Description,
                                   ((Item2SPCTest)obj).MaintainUser.ToString(),
                                   FormatHelper.ToDateString(((Item2SPCTest)obj).MaintainDate)
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
           return new string[] {	"产品名称",
                                    "测试项",
                                    "序号",
                                    "USL",
									"LSL",
								    "UCL",
									"LCL",
									"备注",
									"维护人员",
									"维护日期"};
        }
        #endregion

		protected void chbAuto_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.chbAutoCLEdit.Checked)
			{
				this.txtUCLEdit.Text = string.Empty;
				this.txtLCLEdit.Text = string.Empty;
			}
			
			TextBoxEnableChg();
		}

		protected void chbLowOnly_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.chbLowEdit.Checked)
			{
				this.chbUpEdit.Checked = false;
				this.txtUSLEdit.Text = string.Empty;
				//this.txtUCLEdit.Text = string.Empty;
			}

			TextBoxEnableChg();
		}

		protected void chbUpOnly_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.chbUpEdit.Checked)
			{
				this.chbLowEdit.Checked = false;
				this.txtLSLEdit.Text = string.Empty;
				//this.txtLCLEdit.Text = string.Empty;
			}

			TextBoxEnableChg();		
		}

		private void TextBoxEnableChg()
		{
			this.txtLSLEdit.Enabled = !this.chbUpEdit.Checked;
			this.txtUSLEdit.Enabled = !this.chbLowEdit.Checked;
			this.txtUCLEdit.Enabled = !this.chbAutoCLEdit.Checked;
			this.txtLCLEdit.Enabled = !this.chbAutoCLEdit.Checked;
		}

		private string decimaltostr(string ifchecked,decimal d)
		{
			if(ifchecked == "Y" && d == 0)
				return "";
			else
				return d.ToString();
		}
    }

	/// <summary>
	/// 检查同一个产品是否有重复的测试项
	/// </summary>
	class TestNameUniqueCheck
		:IPageCheck
	{
		private string _itemCode;
		private string _testName;
		private ItemFacade _facade;
		private string _oldOID;
		public TestNameUniqueCheck(ItemFacade facade,string itemCode,string testName,string oldOID)
		{
			_itemCode = itemCode;
			_testName = testName;
			_facade = facade;
			_oldOID = oldOID;
		}
		#region IPageCheck 成员

		public string CheckMessage
		{
			get
			{
				return "$Error_SPC_Test_Repeat";
			}
		}

		#endregion

		#region ICheck 成员

		public bool Check()
		{
			object[] objs = _facade.GetItem2SPCTest(_itemCode,_testName);
			if(objs != null && objs.Length > 0 && ((Item2SPCTest)objs[0]).OID != _oldOID)
				return false;
			
			return true;
		}

		#endregion
	}

	/// <summary>
	/// 检查同一个产品是否有重复的序号
	/// </summary>
	class SeqUniqueCheck
		:IPageCheck
	{
		private string _itemCode;
		private int _seq;
		private ItemFacade _facade;
		private string _oldOID;
		private string _label;
		public SeqUniqueCheck(string label,ItemFacade facade,string itemCode,string seq,string oldOID)
		{
			_label = label;
			seq = seq.Trim();
			_itemCode = itemCode;
			try
			{
				_seq = seq==string.Empty?0:int.Parse(seq);
			}
			catch(FormatException)
			{
				_seq = -1;
			}
			_facade = facade;
			_oldOID = oldOID;
		}
		#region IPageCheck 成员

		public string CheckMessage
		{
			get
			{
				return "$Error_SPC_Seq_Repeat";
			}
		}

		#endregion

		#region ICheck 成员

		public bool Check()
		{
			object[] objs = _facade.GetItem2SPCTest(_itemCode,_seq);
			if(objs != null && objs.Length > 0 && ((Item2SPCTest)objs[0]).OID != _oldOID)
				return false;
			
			return true;
		}

		#endregion
	}
}