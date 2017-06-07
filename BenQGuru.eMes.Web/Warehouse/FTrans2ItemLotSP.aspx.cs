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

using Infragistics.WebUI.UltraWebGrid ;

using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Web.UserControl ;

using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
	/// <summary>
	/// FWHItemSP 的摘要说明。
	/// </summary>
	public partial class FTrans2ItemLotSP : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private BenQGuru.eMES.SelectQuery.SPFacade facade ;//= new BenQGuru.eMES.SelectQuery.SPFacade(base.DataProvider);


		// 分隔符
		protected const string DATA_SPLITER = "," ;
		protected bool writerOutted = false ;
		protected GridHelper gridSelectedHelper = null;
		protected GridHelper gridUnSelectedHelper = null;
		private Hashtable htItemQty = new Hashtable();

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
			this.gridSelected.UpdateGrid += new Infragistics.WebUI.UltraWebGrid.UpdateGridEventHandler(this.gridSelected_UpdateGrid);
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


		#region WebGrid
		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetSelectedGridRow(object obj)
		{
			WarehouseItem item = (WarehouseItem)obj;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{ 
								"",
								item.ItemCode ,
								item.ItemName,
								this.htItemQty[item.ItemCode].ToString()
							}

				);
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetUnSelectedGridRow(object obj)
		{
			WarehouseItem item = (WarehouseItem)obj;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{ 
								"",
								item.ItemCode ,
								item.ItemName
							}

				);
		}

		protected object[] LoadSelectedDataSource(int inclusive, int exclusive)
		{
			if(facade==null){facade = new BenQGuru.eMES.SelectQuery.SPFacade(base.DataProvider);}
			object[] objs = this.facade.QuerySelectedWarehouseItem( this.GetSelectedCodes() ) ;
			if (objs != null && objs.Length > 0)
				this.cmdSave.Disabled = false;
			else
				this.cmdSave.Disabled = true;
			return objs;
		}

		protected object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
		{
			if(facade==null){facade = new BenQGuru.eMES.SelectQuery.SPFacade(base.DataProvider);}
			return this.facade.QueryUnSelectedWarehouseItem(this.txtMOCodeEdit.Text, FormatHelper.PKCapitalFormat(this.txtItemCodeQuery.Text) ,this.txtItemNameQuery.Text, this.GetSelectedCodes(),inclusive,exclusive ) ;
		}


		protected int GetUnSelectedRowCount()
		{
			if(facade==null){facade = new BenQGuru.eMES.SelectQuery.SPFacade(base.DataProvider);}
			return this.facade.QueryUnSelectedWarehouseItemCount(this.txtMOCodeEdit.Text, FormatHelper.PKCapitalFormat(this.txtItemCodeQuery.Text) ,this.txtItemNameQuery.Text , this.GetSelectedCodes() ) ;
		}

        
		#endregion

		
		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			Control control ;
			control = this.FindControl("cmdUnSelect") ;
			if(control != null)
			{
				((System.Web.UI.WebControls.Button)control).Click += new System.EventHandler(this.cmdUnSelect_Click);
			}
			control = this.FindControl("cmdSelect") ;
			if(control != null)
			{
				((System.Web.UI.WebControls.Button)control).Click += new System.EventHandler(this.cmdSelect_Click);
			}
			control = this.FindControl("cmdQuery") ;
			if(control != null)
			{
				((System.Web.UI.HtmlControls.HtmlInputButton)control).ServerClick += new System.EventHandler(this.cmdQuery_ServerClick);
			}

			control = this.FindControl("chbSelected") ;
			if(control==null)
			{
				this.gridSelectedHelper = new GridHelper( this.GetGridSelected() ) ;
			}
			else
			{
				this.gridSelectedHelper = new GridHelper( this.GetGridSelected() ,(System.Web.UI.WebControls.CheckBox) control ) ;
			}

			control = this.FindControl("chbUnSelected") ;
			if(control==null)
			{
				this.gridUnSelectedHelper = new GridHelper( this.GetGridUnSelected()  ) ;
			}
			else
			{
				this.gridUnSelectedHelper = new GridHelper( this.GetGridUnSelected()  ,(System.Web.UI.WebControls.CheckBox) control ) ;
			}


			this.gridSelectedHelper.BuildGridRowhandle = new BuildGridRowDelegate( this.GetSelectedGridRow ) ;
			this.gridSelectedHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadSelectedDataSource) ;

			this.gridUnSelectedHelper.BuildGridRowhandle = new BuildGridRowDelegate( this.GetUnSelectedGridRow ) ;
			this.gridUnSelectedHelper.GetRowCountHandle = new GetRowCountDelegate( this.GetUnSelectedRowCount ) ;
			this.gridUnSelectedHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadUnSelectedDataSource) ;

			control = this.FindControl("cmdCancel") ;
			if(control!=null)
			{
				((System.Web.UI.HtmlControls.HtmlInputButton)control).Attributes.Add("OnClick","window.close();return false ;") ;
			}

			control = this.FindControl("cmdInit") ;
			if(control!=null)
			{
				((System.Web.UI.HtmlControls.HtmlInputButton)control).ServerClick +=  new System.EventHandler(this.cmdInit_ServerClick);
			}

			if(! this.IsPostBack )
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.InitWebGrid() ;
				if (this.GetRequestParam("mo") != "1")
				{
					this.txtMOCodeEdit.Enabled = false;
				}
				if (this.GetRequestParam("mocode") != null)
				{
					this.txtMOCodeEdit.Text = (string)this.GetRequestParam("mocode");
					this.txtMOCodeEdit.Enabled = false;
				}
				this.cmdSave.Disabled = true;
			}


			if (this.ViewState["SelectedItemQty"] != null)
			{
				htItemQty = (Hashtable)this.ViewState["SelectedItemQty"];
			}

		}

		#endregion

		#region WebGrid
		protected void InitWebGrid()
		{

			this.gridSelectedHelper.AddColumn( "Selector_SelectedCode", "已选择的项目",	null);
			this.gridSelectedHelper.AddColumn( "Selector_SelectedDesc", "描述",	null);
			this.gridSelected.Columns.Add(new UltraGridColumn("Selector_Qty", "数量", ColumnType.Custom, "0"));
			this.gridSelected.Columns[2].EditorControlID = "txtEditQty";
			this.gridSelected.Columns[2].AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
			this.gridSelected.Columns[2].CellStyle.BackColor = Color.FromArgb(255, 252, 240);
			this.gridSelectedHelper.AddDefaultColumn(true,false) ;
			this.gridSelectedHelper.ApplyLanguage( this.languageComponent1 );

			this.gridUnSelectedHelper.AddColumn( "Selector_UnselectedCode", "未选择的项目",	null);
			this.gridUnSelectedHelper.AddColumn( "Selector_UnSelectedDesc", "描述",	null);
			this.gridUnSelectedHelper.AddDefaultColumn(true,false) ;
			this.gridUnSelectedHelper.ApplyLanguage( this.languageComponent1 );
		}

		#endregion

		#region Misc
		protected void cmdUnSelect_Click(object sender, System.EventArgs e)
		{
			ArrayList rows = this.gridSelectedHelper.GetCheckedRows() ;
			foreach( UltraGridRow row in rows )
			{
				this.gridSelectedHelper.Grid.Rows.Remove( row ) ;
				this.htItemQty.Remove(row.Cells[1].Text);
			}
			this.ViewState["SelectedItemQty"] = htItemQty;


			this.RequestData() ;

		}

		protected void cmdSelect_Click(object sender, System.EventArgs e)
		{
			ArrayList rows = this.gridUnSelectedHelper.GetCheckedRows() ;
			foreach( UltraGridRow row in rows )
			{
				UltraGridRow newRow = new UltraGridRow(
					new object[]{"",row.Cells[1].Text,row.Cells[2].Text}
					) ;
				this.gridSelectedHelper.Grid.Rows.Add( newRow ) ;
				this.htItemQty.Add(row.Cells[1].Text, 0);
			}
			this.ViewState["SelectedItemQty"] = htItemQty;
            

			this.RequestData() ;
		}


		protected string[] GetSelectedCodes()
		{
			Control control = this.FindControl("txtSelected") ;
			if(control == null)
			{
				return new string[0] ;
			}

			else
			{
				if( ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value.Trim().Length ==0)
				{
					return new string[0] ;
				}
				return ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value.Split( DATA_SPLITER.ToCharArray() ) ;

			}
		}

		private void SetSelectedCodes()
		{
			string[] codes = new string[ this.gridSelectedHelper.Grid.Rows.Count ];
			for(int i=0 ; i<codes.Length ;i++)
			{
				codes[i] = this.gridSelectedHelper.Grid.Rows[i].Cells[1].Text ;
			}

			Control control = this.FindControl("txtSelected") ;
			if(control == null)
			{
				return  ;
			}

			else
			{
				((System.Web.UI.HtmlControls.HtmlTextArea)control).Value = string.Join(DATA_SPLITER,codes) ;
			}
		}

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			RequestData();
		}

		private void RequestData()
		{
			SetSelectedCodes() ;

			this.gridUnSelectedHelper.RequestData() ;
			this.gridSelectedHelper.RequestData();

		}

		protected virtual Infragistics.WebUI.UltraWebGrid.UltraWebGrid GetGridUnSelected()
		{
			Control control = this.FindControl("gridUnselected") ;
			if(control == null)
			{
				return null ;
			}
			return (Infragistics.WebUI.UltraWebGrid.UltraWebGrid) control ;
		}

		protected virtual Infragistics.WebUI.UltraWebGrid.UltraWebGrid GetGridSelected()
		{
			Control control = this.FindControl("gridSelected") ;
			if(control == null)
			{
				return null ;
			}
			return (Infragistics.WebUI.UltraWebGrid.UltraWebGrid) control ;
		}


		private void cmdInit_ServerClick(object sender, System.EventArgs e)
		{
			this.gridSelectedHelper.RequestData() ;
			SetSelectedCodes() ;

		}

		private void gridSelected_UpdateGrid(object sender, Infragistics.WebUI.UltraWebGrid.UpdateEventArgs e)
		{
			this.htItemQty = new Hashtable();
			for (int i = 0; i < e.Grid.Rows.Count; i++)
			{
				this.htItemQty.Add(e.Grid.Rows[i].Cells[1].Text, e.Grid.Rows[i].Cells[3].Text);
			}
			this.ViewState["SelectedItemQty"] = this.htItemQty;
		}

		protected void cmdSave_ServerClick(object sender, System.EventArgs e)
		{
			if (txtMOCodeEdit.Enabled == true && FormatHelper.CleanString(this.txtMOCodeEdit.Text) == string.Empty)
			{
				throw new Exception("$CS_MO_NotExit");
			}
		
			if(facade==null){facade = new BenQGuru.eMES.SelectQuery.SPFacade(base.DataProvider);}
			BenQGuru.eMES.Material.WarehouseFacade _facade = new BenQGuru.eMES.Material.WarehouseFacade(this.facade.DataProvider);
			ArrayList list = new ArrayList();
			for (int i = 0; i < this.gridSelected.Rows.Count; i++)
			{
				if (IsDecimal(this.gridSelected.Rows[i].Cells[3].Text) && decimal.Parse(this.gridSelected.Rows[i].Cells[3].Text) != 0)
				{
					WarehouseTicketDetail item = _facade.CreateNewWarehouseTicketDetail();
					item.TicketNo = this.GetRequestParam("ticketno");
					item.ItemCode = this.gridSelected.Rows[i].Cells[1].Text;
					item.ItemName = this.gridSelected.Rows[i].Cells[2].Text;
					item.MOCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeEdit.Text));
					item.Qty = decimal.Parse(this.gridSelected.Rows[i].Cells[3].Text);
					item.ActualQty = 0;
					item.MaintainUser = this.GetUserCode();
					list.Add(item);
				}
			}
			if (FormatHelper.CleanString(this.txtMOCodeEdit.Text) != string.Empty)
			{
				BenQGuru.eMES.MOModel.MOFacade mof = new BenQGuru.eMES.MOModel.MOFacade(this.facade.DataProvider);
				BenQGuru.eMES.Domain.MOModel.MO mo = (BenQGuru.eMES.Domain.MOModel.MO)mof.GetMO(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeEdit.Text)));
				if (mo == null || (mo.MOStatus != MOManufactureStatus.MOSTATUS_RELEASE && mo.MOStatus != MOManufactureStatus.MOSTATUS_OPEN && mo.MOStatus != MOManufactureStatus.MOSTATUS_PENDING))
				{
					throw new Exception("$CS_MO_NotExit");
				}
			}
			_facade.AddWarehouseTicketDetail(list);
			this.Page.RegisterStartupScript("close window", "<script language='javascript'>CloseWindow();</script>");
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

		#endregion


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

	}
}
