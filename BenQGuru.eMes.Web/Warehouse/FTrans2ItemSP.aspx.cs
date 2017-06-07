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
	/// FTrans2ItemSP 的摘要说明。
	/// </summary>
	public partial class FTrans2ItemSP : BaseMPage
	{
		protected System.Web.UI.WebControls.Label lblShiftTitle;

		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Label lblInitFileQuery;
		protected System.Web.UI.HtmlControls.HtmlInputFile fileInit;
		
		private BenQGuru.eMES.Material.WarehouseFacade _facade ;
//= new BenQGuru.eMES.Material.WarehouseFacade();
		//private Hashtable m_htItemQty = new Hashtable();

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
			//txtFLTS.Attributes.Add("onblur","UpdateGrid();");

			if(!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				InitParam();
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
			this.gridHelper.Grid.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.No ;

			this.gridHelper.AddColumn( "TicketSeq", "序号",	null);
			this.gridHelper.AddColumn( "WarehouseItemCode", "物料代码",	null);
			this.gridHelper.AddColumn( "WarehouseItemName", "物料名称",	null);
			this.gridHelper.AddColumn( "MOCode", "工单号",	null);
			this.gridHelper.AddColumn( "SingleQTY", "单机用量",	null);
			this.gridHelper.AddColumn( "ItemQty", "单据数量",	null);
			this.gridHelper.AddColumn( "ItemQtyOld", "单据数量",	null);
			this.gridHelper.AddColumn( "ActualQty", "实际数量",	null);

			this.gridHelper.AddDefaultColumn( true, false );
			this.gridWebGrid.Columns.FromKey("ItemQtyOld").Hidden = true;
			this.gridWebGrid.Columns.FromKey("ActualQty").Hidden = true;
			this.gridWebGrid.Columns.FromKey("ItemQty").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
			this.gridWebGrid.Columns.FromKey("ItemQty").CellStyle.BackColor = Color.FromArgb(255, 252, 240);
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			if (!this.IsPostBack)
			{
				if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
				if (this.GetRequestParam("ticketno") == "")
				{
					throw new Exception("");
				}
				txtTicketNo.Value = this.GetRequestParam("ticketno");
				if (this.GetRequestParam("ticketno") != null && this.GetRequestParam("ticketno")!=string.Empty)
				{
					txtMOCodeEdit.Text = this.GetRequestParam("mocode");
				}
				this.cmdQuery_Click(null, null);
				object obj = this._facade.GetWarehouseTicket(this.GetRequestParam("ticketno"));
				if (obj == null)
				{
					throw new Exception("");
				}
				this.txtMOCodeEdit.ReadOnly = true;
				this.cmdTogetherBOM.Disabled = true;
				this.cmdSelectMO.Disabled = true;
				string str = ((WarehouseTicket)obj).TransactionTypeCode;
				obj = this._facade.GetTransactionType(str);
				if (FormatHelper.StringToBoolean(((TransactionType)obj).IsByMOControl) == true)
				{
					this.txtMOCodeEdit.ReadOnly = false;
					this.txtMOCodeEdit.Enabled = false;
					if (this.gridWebGrid.Rows.Count == 0)
					{
						this.cmdTogetherBOM.Disabled = false;
						this.cmdSelectMO.Disabled = false;
						this.txtMOCodeEdit.Enabled = true;
					}
					this.ViewState["IsByMOControl"] = "Yes";
				}
				obj = null;
				this.cmdSave.Disabled = false;
			}
		}
		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			if(this.IsFTicket)
			{
				WarehouseTicketDetail2 item = (WarehouseTicketDetail2)obj;
				return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
					new object[]{"false",
									item.Sequence,
									item.ItemCode,
									item.ItemName,
									item.MOCode,
									Math.Round(item.SingleQTY, 2).ToString() ,
									Math.Round(item.Qty, 2).ToString(),
									Math.Round(item.Qty, 2).ToString(),
									Math.Round(item.ActualQty, 2).ToString()
								});
				
				
			}

			WarehouseTicketDetail item2 = (WarehouseTicketDetail)obj;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
					new object[]{"false",
									item2.Sequence,
									item2.ItemCode,
									item2.ItemName,
									item2.MOCode,
									"" ,
									Math.Round(item2.Qty, 2).ToString(),
									Math.Round(item2.Qty, 2).ToString(),
									Math.Round(item2.ActualQty, 2).ToString()
								});;
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object[] objs =null;
			if( this.IsFTicket )
			{
				objs = this._facade.QueryWarehouseTicketDetail2( 
					string.Empty,
					FormatHelper.CleanString(this.GetRequestParam("ticketno")),
					inclusive, exclusive );
			}
			else
			{
				objs = this._facade.QueryWarehouseTicketDetail( 
					string.Empty,
					FormatHelper.CleanString(this.GetRequestParam("ticketno")),
					inclusive, exclusive );
			}
			
			if ((objs == null || (objs != null && objs.Length == 0)) && (this.ViewState["IsByMOControl"] != null && this.ViewState["IsByMOControl"].ToString() == "Yes"))
			{
				this.cmdTogetherBOM.Disabled = false;
				this.cmdSelectMO.Disabled = false;
				this.txtMOCodeEdit.Enabled = true;
			}
			else
			{
				this.cmdTogetherBOM.Disabled = true;
				this.cmdSelectMO.Disabled = true;
				this.txtMOCodeEdit.Enabled = false;
			}
			if(objs == null || objs.Length == 0){this.ExecuteClientFunction("divSelectMODisplay",string.Empty);}
			return objs;
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object[] objs =null;
			if( this.IsFTicket )
			{
				return this._facade.QueryWarehouseTicketDetailCount2( 
					string.Empty,
					FormatHelper.CleanString(this.GetRequestParam("ticketno")));
			}
			else
			{
				return this._facade.QueryWarehouseTicketDetailCount( 
					string.Empty,
					FormatHelper.CleanString(this.GetRequestParam("ticketno")));
			}
			
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.AddWarehouseTicketDetail( (WarehouseTicketDetail)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			if (domainObjects != null && domainObjects.Count > 0)
			{
				for (int i = 0; i < domainObjects.Count; i++)
				{
					WarehouseTicketDetail item = (WarehouseTicketDetail)domainObjects[i];
					if (item.ActualQty > 0)
					{
						throw new Exception("$Add_Ticket_Detail_StatusTransaction [$WarehouseItemCode=" + item.ItemCode + "]");
					}
				}
			}
			this._facade.DeleteWarehouseTicketDetail( (WarehouseTicketDetail[])domainObjects.ToArray( typeof(WarehouseTicketDetail) ) );
			this.ViewState["ItemQty"] = null;
			this.ExecuteClientFunction("CmdSaveEnable",string.Empty);
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			this._facade.UpdateWarehouseTicketDetail( (WarehouseTicketDetail)domainObject );
		}

		protected void cmdOPBOM_ServerClick(object sender, EventArgs e)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			if (this.txtMOCodeEdit.Text == "")
				return;
			this._facade.ImportItemFromOPBOM(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeEdit.Text)), this.GetRequestParam("ticketno"), this.GetUserCode());
			this.cmdQuery_Click(null, null);
			if (this.gridWebGrid.Rows.Count > 0)
			{
				this.cmdTogetherBOM.Disabled = true;
				this.cmdSelectMO.Disabled = true;
				this.txtMOCodeEdit.Enabled = false;
				this.cmdSave.Disabled = false;
			}
		}

		protected void cmdReturn_ServerClick(object sender, EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("../Warehouse/FTransMP.aspx"));
		}

		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			return null;
		}


		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			object obj = _facade.GetWarehouseTicketDetail( row.Cells[1].Text.ToString(), this.GetRequestParam("ticketno") );
			
			if (obj != null)
			{
				return obj as WarehouseTicketDetail;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
		}
		
		protected override bool ValidateInput()
		{
			return true;
		}

		#endregion


		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			WarehouseTicketDetail item = (WarehouseTicketDetail)obj;
			string strQty = item.Qty.ToString();
			string[] strValue = 
				new string[]{
								item.Sequence.ToString(),
								item.ItemCode,
								item.ItemName,
								item.MOCode,
								//strQty,
								Math.Round(item.ActualQty, 2).ToString()
							};
			item = null;
			return strValue;
		}

		protected override string[] GetColumnHeaderText()
		{		
			return new string[] {	
									"TicketSeq",
									"WarehouseItemCode",
									"WarehouseItemName",
									"MOCode",
									//"TicketQty",
									"ActualQty"
								};
		}
		#endregion

		#region custom update in grid
		protected override void cmdQuery_Click(object sender, EventArgs e)
		{
			base.cmdQuery_Click (sender, e);
		}

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

		protected void cmdSave_ServerClick(object sender, EventArgs e)
		{
			if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
			if(ValidateInput())
			{
				for( int i=0; i<this.gridHelper.Grid.Rows.Count; i++ )
				{
					UltraGridRow row = this.gridHelper.Grid.Rows[i];
					WarehouseTicketDetail item = 
						(WarehouseTicketDetail)this._facade.GetWarehouseTicketDetail(row.Cells.FromKey("TicketSeq").Value.ToString(), this.txtTicketNo.Value);
					if (item != null && item.ActualQty <= 0)
					{
						item.Qty = Convert.ToDecimal( row.Cells.FromKey("ItemQty").Value );
						this._facade.UpdateWarehouseTicketDetail(item);
					}
				}
			}

			this.cmdQuery_Click(null, null);
		}
		#endregion

		protected void cmdTrans_ServerClick(object sender, EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("../Warehouse/FTrans2ActualTransSP.aspx?ticketno=" + this.txtTicketNo.Value));
		}

		/// <summary>
		/// 执行客户端的函数
		/// </summary>
		/// <param name="FunctionName">函数名</param>
		/// <param name="FunctionParam">参数</param>
		/// <param name="Page">当前页面的引用</param>
		public  void ExecuteClientFunction(string FunctionName,string FunctionParam)
		{
			try
			{
				string _msg = string.Empty;
				if(FunctionParam != string.Empty)
					_msg = string.Format("<script language='JavaScript'>  {0}('{1}');</script>",FunctionName,FunctionParam);
				else
					_msg = string.Format("<script language='JavaScript'>  {0}();</script>",FunctionName);

				//将Key值设为随机数,防止脚本重复
				Page.RegisterStartupScript(Guid.NewGuid().ToString(),_msg);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		private void InitParam()
		{

		}

		protected void txtFLTS_TextChanged(object sender, System.EventArgs e)
		{
			this.cmdSave.Disabled = false;
			if( this.txtFLTS.Text.Trim() == string.Empty )return;

			

			decimal fQTY = -1;
			try
			{
				fQTY = decimal.Parse(this.txtFLTS.Text.Trim());
			}
			catch
			{
			}
			if( fQTY<0 )return;

			for(int i=0; i< this.gridWebGrid.Rows.Count; i++ )
			{
				UltraGridRow row = this.gridWebGrid.Rows[i];
				if(row.Cells.FromKey("SingleQTY").Value !=null 
					&& row.Cells.FromKey("SingleQTY").Text.Trim().Length > 0 )
				{
					row.Cells.FromKey("ItemQty").Value = fQTY *  decimal.Parse( row.Cells.FromKey("SingleQTY").Value.ToString());
				}

			}
		}

		private bool IsFTicket
		{
			get
			{
				string tktNo = this.GetRequestParam("ticketno");
				if(_facade==null){_facade = new WarehouseFacade(base.DataProvider);}
				object tktObj =  _facade.GetWarehouseTicket( tktNo );
				if( tktObj!=null )
				{
					if( ( tktObj as WarehouseTicket ).TransactionTypeCode == "541" )
					{
						//541 发料单
						return true;
					}
				}
				return false;
			}
		}
	}
}
