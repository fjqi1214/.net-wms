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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
	/// <summary>
	/// FTransMP 的摘要说明。
	/// </summary>
	public partial class FTransMP : BaseMPage
	{
		protected System.Web.UI.WebControls.Label lblShiftTitle;

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Label lblInitFileQuery;
		protected System.Web.UI.HtmlControls.HtmlInputFile fileInit;
		protected System.Web.UI.WebControls.TextBox txtTransNameEdit;
		public BenQGuru.eMES.Web.UserControl.eMESDate txtDateFromQuery;
		public BenQGuru.eMES.Web.UserControl.eMESDate txtDateToQuery;
		
		private BenQGuru.eMES.Material.WarehouseFacade _facade ;//= new BenQGuru.eMES.Material.WarehouseFacade();

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
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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
			if (!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				FillDropDownList();
				txtDateToQuery.Text = DateTime.Today.ToString("yyyy-MM-dd");
				this.drpFactoryFromEdit.Attributes["onchange"] = "ChangeToDropDownList('drpFactoryFromEdit', 'drpFactoryToEdit');";
				//this.drpSegFromEdit.Attributes["onchange"] = "ChangeToDropDownList('drpSegFromEdit', 'drpSegToEdit');";
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
			this.gridHelper.AddColumn( "TransactionCode", "单据号",	null);
			this.gridHelper.AddColumn( "ReferenceCode", "参考号",	null);
			this.gridHelper.AddColumn( "TransactionName", "单据名称",	null);
			this.gridHelper.AddColumn( "MOCode", "工单号",	null);
			this.gridHelper.AddColumn( "FactoryFrom", "来源工厂",	null);
			//this.gridHelper.AddColumn( "SegmentFrom", "来源工段",	null);
			this.gridHelper.AddColumn( "WarehouseFrom", "来源仓库",	null);
			this.gridHelper.AddColumn( "FactoryTo", "目标工厂",	null);
			//this.gridHelper.AddColumn( "SegmentTo", "目标工段",	null);
			this.gridHelper.AddColumn( "WarehouseTo", "目标仓库",	null);
			this.gridHelper.AddColumn( "TransactionStatus", "状态",	null);
			this.gridHelper.AddColumn( "MaintainUser", "制单人",	null);
			this.gridHelper.AddColumn( "MaintainDate", "制单日期",	null);
			this.gridHelper.AddLinkColumn( "SelectItem", "单据物料", null);
			this.gridHelper.AddLinkColumn( "Transaction", "交易", null);
			
			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			WarehouseTicket ticket = (WarehouseTicket)obj;
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								ticket.TicketNo,
								ticket.ReferenceCode,
								GetTransTypeName(ticket.TransactionTypeCode),
								ticket.MOCode,
								ticket.FactoryCode,
								//ticket.SegmentCode,
								ticket.WarehouseCode,
								ticket.TOFactoryCode,
								//ticket.TOSegmentCode,
								ticket.TOWarehouseCode,
								GetTicketStatus(ticket.TransactionStatus),
								ticket.MaintainUser,
								FormatHelper.ToDateString(ticket.MaintainDate),
								"","", ""});
			ticket = null;
			return row;
		}
		private string GetTicketStatus(string status)
		{
			if (this.drpTransStatusQuery.Items.FindByValue(status) == null)
				return "";
			else
				return this.drpTransStatusQuery.Items.FindByValue(status).Text;
		}
		private Hashtable m_htTransType = null;
		private string GetTransTypeName(string type)
		{
			if (m_htTransType == null)
			{
				m_htTransType = new Hashtable();
				object[] objs = this._facade.GetAllTransactionType();
				if (objs != null)
				{
					for (int i = 0; i < objs.Length; i++)
					{
						TransactionType trans = (TransactionType)objs[i];
						m_htTransType.Add(trans.TransactionTypeCode, trans.TransactionTypeName);
					}
				}
			}
			if (m_htTransType.ContainsKey(type))
				return m_htTransType[type].ToString();
			else
				return "";
		}
		private string DisplayRefCode(string refCode, string ticketNo)
		{
			if (refCode == "")
				return "";
			else
			{
				string strUrl = this.MakeRedirectUrl("./FTrans2ActualTransSP.aspx");
				strUrl += "?ticketno=" + ticketNo;
				return string.Format("<a href='{0}'>{1}</a>", strUrl, refCode);
			}
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouseTicket( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTransCodeQuery.Text)),
				"",
				this.txtRefCodeQuery.Text,
				this.drpTransStatusQuery.SelectedValue,
				txtDateFromQuery.Text,
				txtDateToQuery.Text,
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserCodeQuery.Text)),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouseTicketCount(  
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTransCodeQuery.Text)),
				"",
				this.txtRefCodeQuery.Text,
				this.drpTransStatusQuery.SelectedValue,
				txtDateFromQuery.Text,
				txtDateToQuery.Text,
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMoCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserCodeQuery.Text)));
		}

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			string ticketno = e.Cell.Row.Cells.FromKey("TransactionCode").Text;
			string mocode = e.Cell.Row.Cells.FromKey("MOCode").Text;
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			if( this.gridHelper.IsClickColumn( "SelectItem",e ) )
			{
				//this.Response.Redirect( this.MakeRedirectUrl("./FTrans2ItemSP.aspx", new string[]{"ticketno"}, new string[]{e.Cell.Row.Cells[1].Text.Trim()}));
				this.Response.Redirect( this.MakeRedirectUrl("./FTrans2ItemSP.aspx", new string[]{"ticketno","mocode"}, new string[]{ticketno,mocode}));
			}
			if( this.gridHelper.IsClickColumn( "Transaction",e ) )
			{
				if (this._facade.QueryWarehouseTicketDetailCount(string.Empty, e.Cell.Row.Cells[1].Text.Trim()) > 0)
				{
					this.Response.Redirect( this.MakeRedirectUrl("./FTrans2ActualTransSP.aspx", new string[]{"ticketno"}, new string[]{e.Cell.Row.Cells[1].Text.Trim()}));
				}
				else
				{
					throw new Exception("$Error_DoWarehouseTransaction_WithoutWarehouseItem");
				}
			}
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.AddWarehouseTicket( (WarehouseTicket)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.DeleteWarehouseTicket( (WarehouseTicket[])domainObjects.ToArray( typeof(WarehouseTicket) ), this.GetUserCode() );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.UpdateWarehouseTicket( (WarehouseTicket)domainObject, this.GetUserCode() );
			string strText = "$Error_TicketUpdate_SuccessMessage";
			string alertInfo = 
				string.Format("<script language=javascript>alert('{0}');</script>", MessageCenter.ParserMessage(strText, languageComponent1).Replace("\n", "\\n" ));
			if( !this.IsClientScriptBlockRegistered("ExceptionAlert") )
			{
				this.RegisterClientScriptBlock("ExceptionAlert", alertInfo);	
			}
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtTransCodeEdit.ReadOnly = true;
			}
			
			if ( pageAction == PageActionType.Update )
			{
				this.txtTransCodeEdit.ReadOnly = true;
			}
		}

		#region 单据类型检查,是否受工单管控

		private bool IsByMOControl()
		{
			if(this.drpTransTypeEdit.SelectedValue == string.Empty)return false;

			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			string transtypecode = this.drpTransTypeEdit.SelectedValue.Substring(0, this.drpTransTypeEdit.SelectedValue.LastIndexOf(":"));
			TransactionType tranceTypeObj = (TransactionType)this._facade.GetTransactionType(transtypecode);
			if(tranceTypeObj != null && (FormatHelper.StringToBoolean(((TransactionType)tranceTypeObj).IsByMOControl) == true))
			{
				return true;
			}
			
//			if(transtypecode == "541" || transtypecode == "551" || transtypecode == "262" || transtypecode == "801" )
//			{
//				return true;
//			}
			return false;
		}

		#endregion

		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			WarehouseTicket ticket = this._facade.CreateNewWarehouseTicket();

			ticket.TicketNo		= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTransCodeEdit.Text, 50));
			string str = this.drpTransTypeEdit.SelectedValue;
			str = str.Substring(0, str.LastIndexOf(":"));
			ticket.TransactionTypeCode	= str;
			ticket.MOCode		= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeEdit.Text));
			ticket.FactoryCode	= this.drpFactoryFromEdit.SelectedValue;
			//ticket.SegmentCode	= this.drpSegFromEdit.SelectedValue;
			ticket.WarehouseCode	= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWHFromEdit.Text));
			ticket.TOFactoryCode	= this.drpFactoryToEdit.SelectedValue;
			//ticket.TOSegmentCode	= this.drpSegToEdit.SelectedValue;
			ticket.TOWarehouseCode	= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWHToEdit.Text));
			ticket.MaintainUser		= this.GetUserCode();
			ticket.ReferenceCode	= this.txtRefCodeEdit.Text;

			return ticket;
		}

		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object obj = _facade.GetWarehouseTicket( row.Cells[1].Text.ToString() );
			
			if (obj != null)
			{
				return obj as WarehouseTicket;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtTransCodeEdit.Text = "";
				this.txtMOCodeEdit.Text = "";
				this.drpTransTypeEdit.SelectedIndex = -1;
				this.drpFactoryFromEdit.SelectedIndex = -1;
				//this.drpSegFromEdit.SelectedIndex = -1;
				this.drpWHFromEdit.Text = "";
				this.drpFactoryToEdit.SelectedIndex = -1;
				//this.drpSegToEdit.SelectedIndex = -1;
				this.drpWHToEdit.Text = "";
				this.drpTransTypeEdit.Enabled = true;
				this.txtRefCodeEdit.Text = "";

				return;
			}

			WarehouseTicket ticket = (WarehouseTicket)obj;
			this.txtTransCodeEdit.Text	= ticket.TicketNo;
			this.txtMOCodeEdit.Text = ticket.MOCode;
			SetDropDownValue(this.drpTransTypeEdit, ticket.TransactionTypeCode + ":" + ticket.TicketNo.Substring(0, 1));
			SetDropDownValue(this.drpFactoryFromEdit, ticket.FactoryCode);
			//SetDropDownValue(this.drpSegFromEdit, ticket.SegmentCode);
			this.drpWHFromEdit.Text = ticket.WarehouseCode;
			SetDropDownValue(this.drpFactoryToEdit, ticket.TOFactoryCode);
			//SetDropDownValue(this.drpSegToEdit, ticket.TOSegmentCode);
			this.drpWHToEdit.Text = ticket.TOWarehouseCode;
			this.txtRefCodeEdit.Text = ticket.ReferenceCode;
			this.drpTransTypeEdit.Enabled = false;
		}
		private void SetDropDownValue(DropDownList drp, string value)
		{
			try
			{
				drp.SelectedValue = value;
			}
			catch
			{
				drp.SelectedIndex = -1;
			}
		}
		
		protected override bool ValidateInput()
		{
			if(this.IsByMOControl() && (this.txtMOCodeEdit.Text.Trim() == string.Empty))
			{
				WebInfoPublish.Publish(this, "$Error_Input_IsByMOControl_Empty", this.languageComponent1);
				return false;
			}

			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblTransNameEdit, txtTransCodeEdit, 40, true) );
			manager.Add( new LengthCheck(lblRefCodeEdit, txtRefCodeEdit, 40, false) );

			if ( ! manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}
			if ( ! ValidateInput2() )
			{
				WebInfoPublish.Publish(this, "$Error_Input_Empty", this.languageComponent1);
				return false;
			}
			if(this.txtMOCodeEdit.Text.Trim() != string.Empty)
			{
				if( JudgeMoByCode(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeEdit.Text))) == false)
				{
					WebInfoPublish.Publish(this, "$Error_Input_MoCode", this.languageComponent1);
					return false;
				}
			}
			

			return true;
		}

		private bool JudgeMoByCode(string mocode)
		{
			BenQGuru.eMES.MOModel.MOFacade _mofacade = new BenQGuru.eMES.MOModel.MOFacade(base.DataProvider);
			if(_mofacade.GetMO(mocode) != null)
			{
				return true;
			}
			return false;
		}

		private bool ValidateInput2()
		{
			if (this.drpTransTypeEdit.SelectedValue == string.Empty)
				return false;
			if (this.drpFactoryFromEdit.SelectedValue != string.Empty && ( /* this.drpSegFromEdit.SelectedValue == string.Empty || */ this.drpWHFromEdit.Text == string.Empty))
				return false;
			if (this.drpFactoryToEdit.SelectedValue != string.Empty && ( /* this.drpSegToEdit.SelectedValue == string.Empty || */ this.drpWHToEdit.Text == string.Empty))
				return false;
			if (this.drpFactoryFromEdit.SelectedValue == string.Empty)
			{
				//this.drpSegFromEdit.SelectedValue = string.Empty;
				this.drpWHFromEdit.Text = "";
			}
			if (this.drpFactoryToEdit.SelectedValue == string.Empty)
			{
				//this.drpSegToEdit.SelectedValue = string.Empty;
				this.drpWHToEdit.Text = "";
			}
			if (this.drpWHFromEdit.Text == string.Empty && this.drpWHToEdit.Text == string.Empty)
				return false;

			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			if (this.drpWHFromEdit.Text != string.Empty)
			{
				if (this._facade.QueryWarehouseCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWHFromEdit.Text)), /* this.drpSegFromEdit.SelectedValue,*/ this.drpFactoryFromEdit.SelectedValue) <= 0)
				{
					WebInfoPublish.Publish(this, "$Error_Input_WHFrom", this.languageComponent1);
					return false;
				}
			}
			if (this.drpWHToEdit.Text != string.Empty)
			{
				if (this._facade.QueryWarehouseCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpWHToEdit.Text)), /* this.drpSegToEdit.SelectedValue, */ this.drpFactoryToEdit.SelectedValue) <= 0)
				{
					WebInfoPublish.Publish(this, "$Error_Input_WHTo", this.languageComponent1);
					return false;
				}
			}

			return true;
		}

		#endregion

		#region 数据初始化
		private void FillDropDownList()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			BenQGuru.eMES.Common.MutiLanguage.LanguageWord lword  = languageComponent1.GetLanguage("listItemAll");

			this.drpTransStatusQuery.Items.Clear();
			this.drpTransStatusQuery.Items.Add(new ListItem(lword.ControlText, string.Empty));
			this.drpTransStatusQuery.Items.Add(new ListItem(languageComponent1.GetLanguage("WarehouseTransactionStatusPending").ControlText, WarehouseTicket.TransactionStatusEnum.Pending.ToString()));
			this.drpTransStatusQuery.Items.Add(new ListItem(languageComponent1.GetLanguage("WarehouseTransactionStatusTransaction").ControlText, WarehouseTicket.TransactionStatusEnum.Transaction.ToString()));
			this.drpTransStatusQuery.Items.Add(new ListItem(languageComponent1.GetLanguage("WarehouseTransactionStatusClosed").ControlText, WarehouseTicket.TransactionStatusEnum.Closed.ToString()));

			//初始化交易类型列表
			object[] objs = this._facade.GetAllTransactionType();
			this.drpTransTypeEdit.Items.Clear();
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					TransactionType type = (TransactionType)objs[i];
					this.drpTransTypeEdit.Items.Add(new ListItem(type.TransactionTypeName, type.TransactionTypeCode + ":" + type.TransactionPrefix));
					type = null;
				}
			}
			this.drpTransTypeEdit.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			//初始化工厂列表
			objs = this._facade.GetAllFactory();
			this.drpFactoryFromEdit.Items.Clear();
			this.drpFactoryToEdit.Items.Clear();
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					Factory f = (Factory)objs[i];
					this.drpFactoryFromEdit.Items.Add(new ListItem(f.FactoryCode, f.FactoryCode));
					this.drpFactoryToEdit.Items.Add(new ListItem(f.FactoryCode, f.FactoryCode));
					f = null;
				}
			}
			this.drpFactoryFromEdit.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			this.drpFactoryToEdit.Items.Insert(0, new ListItem(string.Empty, string.Empty));

			//初始化工段列表
			BenQGuru.eMES.BaseSetting.BaseModelFacade bmFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this._facade.DataProvider);
			objs = bmFacade.GetAllSegment();
			//this.drpSegFromEdit.Items.Clear();
			//this.drpSegToEdit.Items.Clear();
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					BenQGuru.eMES.Domain.BaseSetting.Segment seg = (BenQGuru.eMES.Domain.BaseSetting.Segment)objs[i];
					//this.drpSegFromEdit.Items.Add(new ListItem(seg.SegmentCode, seg.SegmentCode));
					//this.drpSegToEdit.Items.Add(new ListItem(seg.SegmentCode, seg.SegmentCode));
					seg = null;
				}
			}
			bmFacade = null;
			//this.drpSegFromEdit.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			//this.drpSegToEdit.Items.Insert(0, new ListItem(string.Empty, string.Empty));
			lword = null;

			this.txtUserCodeQuery.Text = this.GetUserCode();
		}

		//选择交易单据，自动生产单据号
		protected void drpTransTypeEdit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			if (this.drpTransTypeEdit.SelectedValue != string.Empty)
			{
				string[] str = this.drpTransTypeEdit.SelectedValue.Split(':');
				this.txtTransCodeEdit.Text = this._facade.GetTicketSeq(str[str.Length - 1]);
			}
			else
				this.txtTransCodeEdit.Text = "";
		}

		//选择来源工厂、工段，查询所有仓库
		/*
		private void FillWarehouseFrom(object sender, System.EventArgs e)
		{
			this.drpWHFromEdit.Items.Clear();
			if (this.drpFactoryFromEdit.SelectedValue == string.Empty)
			{
				this.drpSegFromEdit.SelectedValue = string.Empty;
			}
			else
			{
				this.FillWarehouse(this.drpFactoryFromEdit, this.drpSegFromEdit, this.drpWHFromEdit);
				if (sender != null)
				{
					if (((DropDownList)sender).ID == this.drpFactoryFromEdit.ID)
						this.drpFactoryToEdit.SelectedIndex = this.drpFactoryFromEdit.SelectedIndex;
					if (((DropDownList)sender).ID == this.drpSegFromEdit.ID)
						this.drpSegToEdit.SelectedIndex = this.drpSegFromEdit.SelectedIndex;
					FillWarehouseTo(null, null);
				}
			}
		}
		private void FillWarehouseTo(object sender, System.EventArgs e)
		{
			this.drpWHToEdit.Items.Clear();
			if (this.drpFactoryToEdit.SelectedValue == string.Empty)
			{
				this.drpSegToEdit.SelectedValue = string.Empty;
			}
			else
				this.FillWarehouse(this.drpFactoryToEdit, this.drpSegToEdit, this.drpWHToEdit);
		}
		private void FillWarehouse(DropDownList drpFactory, DropDownList drpSeg, DropDownList drp)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			string factoryCode = drpFactory.SelectedValue;
			string segCode = drpSeg.SelectedValue;
			object[] objs = this._facade.GetWarehouseByFactorySeg(segCode, factoryCode);
			drp.Items.Clear();
			drp.Items.Add("");
			if (objs != null)
			{
				for (int i = 0; i < objs.Length; i++)
				{
					Warehouse wh = (Warehouse)objs[i];
					drp.Items.Add(new ListItem(wh.WarehouseCode, wh.WarehouseCode));
					wh = null;
				}
			}
			objs = null;
		}
		*/
		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			WarehouseTicket ticket = (WarehouseTicket)obj;
			string[] strValue = new string[]{  ticket.TicketNo,
												ticket.ReferenceCode,
												GetTransTypeName(ticket.TransactionTypeCode),
												ticket.MOCode,
												ticket.FactoryCode,
												//ticket.SegmentCode,
												ticket.WarehouseCode,
												ticket.TOFactoryCode,
												//ticket.TOSegmentCode,
												ticket.TOWarehouseCode,
												GetTicketStatus(ticket.TransactionStatus),
								   
												ticket.MaintainUser.ToString(),
												FormatHelper.ToDateString(ticket.MaintainDate)
											};
			ticket = null;
			return strValue;
		}

		protected override string[] GetColumnHeaderText()
		{		
			// TODO: 调整字段值的顺序，使之与Grid的列对应
			return new string[] {	
									"TicketNo",
									"ReferenceCode",
									"TransactionTypeCode",
									"MOCode",
									"FromFactoryCode",
									//"FromSegmentCode",
									"FromWarehouseCode",
									"TOFactoryCode",
									//"TOSegmentCode",
									"TOWarehouseCode",
									"TransactionStatus",
									"MaintainUser",
									"MaintainDate"};
		}
		#endregion

	}
}
