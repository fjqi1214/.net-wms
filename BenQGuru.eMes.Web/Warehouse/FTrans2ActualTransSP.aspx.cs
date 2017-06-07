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

namespace BenQGuru.eMES.Web.WarehouseWeb
{
	/// <summary>
	/// FShiftMP 的摘要说明。
	/// </summary>
	public partial class FTrans2ActualTransSP : BaseMPage
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdQuery;
		protected System.Web.UI.WebControls.Label lblShiftTitle;

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdImport;
		protected System.Web.UI.WebControls.Label lblInitFileQuery;
		protected System.Web.UI.HtmlControls.HtmlInputFile fileInit;
		protected System.Web.UI.WebControls.Label lblItemCodeQuery;
		protected System.Web.UI.WebControls.TextBox txtItemCodeQuery;
		protected System.Web.UI.WebControls.Label lblItemNameQuery;
		protected System.Web.UI.WebControls.TextBox txtItemNameQuery;
		protected System.Web.UI.WebControls.Label lblItemCodeEdit;
		protected System.Web.UI.WebControls.TextBox txtItemCodeEdit;
		protected System.Web.UI.WebControls.Label lblItemNameEdit;
		protected System.Web.UI.WebControls.TextBox txtItemNameEdit;
		protected System.Web.UI.WebControls.Label lblItemUOMEdit;
		protected System.Web.UI.WebControls.TextBox txtItemUOMEdit;
		protected System.Web.UI.WebControls.Label lblItemControlTypeEdit;
		protected System.Web.UI.WebControls.DropDownList drpItemControlTypeEdit;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdDelete;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdAdd;
		protected System.Web.UI.HtmlControls.HtmlInputButton cmdSave;
		
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
			this.chbFLSL.Attributes.Add("onclick","UpdateGrid();");
			if (!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				if (this.GetRequestParam("ticketno") == "")
				{
					throw new Exception("");
				}
				this.txtTicketNoQuery.Text = this.GetRequestParam("ticketno");
				this.lblTicketNoLink.InnerHtml = string.Format("<a href='FQueryTransDtlSP.aspx?ticketno={0}&returnurl={1}'>{0}</a>", this.txtTicketNoQuery.Text,"FTrans2ActualTransSP.aspx");
				this.ViewState["IsSendItem"] = IsSendItem();
			}
		}

		private bool IsSendItem()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object obj = this._facade.GetWarehouseTicket(this.txtTicketNoQuery.Text);
			if (obj == null)
				return false;
			WarehouseTicket tkt = (WarehouseTicket)obj;
			string strPath = this.MapPath("TransTypeMoStock.xml");
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.Load(strPath);
			System.Xml.XmlElement eleDoc = doc.DocumentElement;
			System.Xml.XmlNode elemap = eleDoc.SelectSingleNode("//TransTypeMapping/SendItem");
			string strCode = "";
			if (elemap != null && elemap.Attributes["Code"] != null)
				strCode = elemap.Attributes["Code"].Value;
			if (tkt.TransactionTypeCode == strCode)
				return true;
			else
				return false;
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
			this.gridHelper.Grid.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No ;
			this.gridHelper.AddColumn( "TicketSeq", "序号",	null);
			this.gridHelper.AddColumn( "WarehouseItemCode", "物料代码",	null);
			this.gridHelper.AddColumn( "WarehouseItemName", "物料名称",	null);
			this.gridHelper.AddColumn( "TransMOCode", "工单号",	null);
			this.gridHelper.AddColumn( "TransQty", "单据数量",	null);
			
			this.gridHelper.AddColumn( "OweQty", "尚欠数量",	null);
			this.gridHelper.AddColumn( "ActualQty2", "发料数量",	null);

			this.gridHelper.CheckAllBox.Visible = false;

			this.gridWebGrid.Columns.FromKey("ActualQty2").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
			this.gridWebGrid.Columns.FromKey("ActualQty2").CellStyle.BackColor = Color.FromArgb(255, 252, 240);

			this.gridHelper.ApplyLanguage( this.languageComponent1 );



			this.cmdQuery_Click(null, null);
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			string strCurrQty = "0";
			string strKey = this.txtTicketNoQuery.Text + ":" + item.Sequence.ToString();
			decimal deLeft = item.Qty - item.ActualQty;
			string strReturnQty = "0";
 			string strReturnScrapQty = "0";
			Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								item.Sequence,
								item.ItemCode,
								item.ItemName,
								item.MOCode,
								Math.Round(item.Qty, 2),
								Math.Round(deLeft, 2),
								Math.Round(Convert.ToDecimal(strCurrQty), 2),
								Math.Round(Convert.ToDecimal(strReturnQty), 2),
								Math.Round(Convert.ToDecimal(strReturnScrapQty), 2),
								""});
			item = null;
			return row;
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouseTicketDetail( 
				string.Empty,
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTicketNoQuery.Text)),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			return this._facade.QueryWarehouseTicketDetailCount( 
				string.Empty,
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtTicketNoQuery.Text)));
		}

		#endregion

		#region Button
		/*
		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.AddWarehouseTicketDetail( (WarehouseTicketDetail)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.DeleteWarehouseTicketDetail( (WarehouseTicketDetail[])domainObjects.ToArray( typeof(WarehouseTicketDetail) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.UpdateWarehouseTicketDetail( (WarehouseTicketDetail)domainObject );
		}

		private void cmdUpdate_Click(object sender, EventArgs e)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			string strseq = this.txtItemSeqEdit.Value;
			string strno = this.txtTicketNoQuery.Text;
			if (ValidateInput())
			{
				decimal d = decimal.Parse(this.txtActualQtyEdit.Text);
				//this._facade.UpdateWarehouseTicketDetailActualQty(strseq, strno, d, this.GetUserCode());
				m_htCurrentTransQty = (Hashtable)this.ViewState["CurrentTransQty"];
				this.m_htCurrentTransQty[this.txtTicketNoQuery.Text + ":" + strseq] = d;
				this.ViewState["CurrentTransQty"] = m_htCurrentTransQty;
			}
			this.cmdQuery_Click(null, null);
			this.txtItemSeqEdit.Value = "";
			this.txtActualQtyEdit.Text = "";
		}
		*/

		protected void cmdReturn_ServerClick(object sender, EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("../Warehouse/FTransMP.aspx"));
		}

		protected void cmdTrans_ServerClick(object sender, EventArgs e)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this.ParseTransactionTypeMoStock();

			this._facade.WarehouseTicketDoTrans(this.txtTicketNoQuery.Text, GetHashtable(), this.GetUserCode());
			
			//this._facade.DoReturnItemFromSend(this.txtTicketNoQuery.Text, this.m_htReturnQty, this.m_htReturnScrapQty, this.GetUserCode());
			
			BenQGuru.eMES.Common.MutiLanguage.LanguageWord lword  = languageComponent1.GetLanguage("$Warehouse_Ticket_Transaction_Success");
			Page.RegisterStartupScript("success message", string.Format("<script language='javascript'>alert('{0}');</script>", lword.ControlText));
			cmdTrans.Disabled = true;
			this.ViewState["TransactionOK"] = "Yes";
			this.cmdQuery_Click(null, null);
		}
		private void ParseTransactionTypeMoStock()
		{
			if (TransactionType.TRANSACTIONTYPE_MOSTOCK == null)
			{
				try
				{
					string strPath = this.MapPath("TransTypeMoStock.xml");
					System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
					doc.Load(strPath);
					System.Xml.XmlElement eleDoc = doc.DocumentElement;
					int ilen = eleDoc.ChildNodes.Count;
					TransactionType.TRANSACTIONTYPE_MOSTOCK = new TransactionType.TransactionTypeMoStock[ilen];
					for (int i = 0; i < ilen; i++)
					{
						System.Xml.XmlNode node = eleDoc.ChildNodes[i];
						if (node.Name == "TransType")
						{
							TransactionType.TransactionTypeMoStock mostock = new TransactionType.TransactionTypeMoStock();
							mostock.TransactionTypeCode = node.Attributes["TransTypeCode"].Value;
							mostock.AttributeName = node.Attributes["MoStockAttribute"].Value;
							mostock.Operation = node.Attributes["Operation"].Value;
							if (node.Attributes["ToWarehouse"] != null)
							{
								mostock.ToWarehouse = node.Attributes["ToWarehouse"].Value;
							}
							else
								mostock.ToWarehouse = "";
							TransactionType.TRANSACTIONTYPE_MOSTOCK[i] = mostock;
						}
					}
				}
				catch
				{
					TransactionType.TRANSACTIONTYPE_MOSTOCK = null;
				}
			}
		}
		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			return null;
		}


		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			/*
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object obj = _facade.GetWarehouseTicketDetail( row.Cells[0].Text.ToString(), this.txtTicketNoQuery.Text );
			
			if (obj != null)
			{
				return obj as WarehouseTicketDetail;
			}
			*/

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			/*
			if (obj == null || (this.ViewState["TransactionOK"] != null && this.ViewState["TransactionOK"].ToString() == "Yes"))
			{
				this.txtActualQtyEdit.Text = "";
				this.txtItemSeqEdit.Value = "";

				return;
			}

			m_htCurrentTransQty = (Hashtable)this.ViewState["CurrentTransQty"];
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			this.txtActualQtyEdit.Text =m_htCurrentTransQty[this.txtTicketNoQuery.Text + ":" + item.Sequence.ToString()].ToString();
			this.txtItemSeqEdit.Value = item.Sequence.ToString();

			this.txtActualQtyEdit.ReadOnly = false;
			this.cmdUpdate.Enabled = true;
			*/
		}
		
		protected override bool ValidateInput()
		{
			/*
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new DecimalCheck(lblActualQtyEdit, txtActualQtyEdit, 0, decimal.MaxValue, true));

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}
			*/
			return true;
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			string strCurrQty = "0";
			decimal deLeft = item.Qty - item.ActualQty;
			string[] strValue = new string[]{  item.Sequence.ToString(), item.ItemCode,
												item.ItemName,
												item.MOCode,
												item.Qty.ToString(),
												deLeft.ToString(),
												strCurrQty
											};
			item = null;
			return strValue;
		}

		protected override string[] GetColumnHeaderText()
		{		
			return new string[] {	
									"WarehouseItemSequence",
									"WarehouseItemCode",
									"WarehouseItemName",
									"MOCode",
									"TicketQty",
									"ActualQty"};
		}
		#endregion

		private bool IsDecimal(string str)
		{
			try
			{
				decimal d = decimal.Parse(str);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private void FTrans2ActualTransSP_PreRender(object sender, System.EventArgs e)
		{
			this.gridHelper.Grid.DblClick -= new ClickEventHandler(Grid_DblClick);
		}


		private Hashtable GetHashtable()
		{
			Hashtable htCurrentTransQty = new Hashtable();
			for (int i = 0; i < this.gridHelper.Grid.Rows.Count; i++)
			{
				UltraGridRow row = this.gridHelper.Grid.Rows[i];
				string strKey = this.txtTicketNoQuery.Text + ":" + row.Cells.FromKey("TicketSeq").Text;
				if (IsDecimal(row.Cells.FromKey("ActualQty2").Text))
				{
					if (htCurrentTransQty.ContainsKey(strKey))
					{
						htCurrentTransQty[strKey] = row.Cells.FromKey("ActualQty2").Text;
					}
					else
					{
						htCurrentTransQty.Add(strKey, row.Cells.FromKey("ActualQty2").Text);
					}
				}
			}

			return htCurrentTransQty;
		}
	}
}
