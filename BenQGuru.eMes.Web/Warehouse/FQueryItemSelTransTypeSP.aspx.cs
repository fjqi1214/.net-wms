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

using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
	/// <summary>
	/// FQueryItemSelTransTypeSP 的摘要说明。
	/// </summary>
	public partial class FQueryItemSelTransTypeSP : BasePage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				this.InitPage();
				this.SelectInitValue();
			}
		}

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

		}
		#endregion

		private void InitPage()
		{
			BenQGuru.eMES.Material.WarehouseFacade facade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
			object[] objs = facade.GetAllTransactionType();
			if (objs == null)
				return;
			listTransType.RepeatColumns = (objs.Length / 5) + 1;
			for (int i = 0; i < objs.Length; i++)
			{
				BenQGuru.eMES.Domain.Warehouse.TransactionType type = (BenQGuru.eMES.Domain.Warehouse.TransactionType)objs[i];
				listTransType.Items.Add(new ListItem(type.TransactionTypeName, type.TransactionTypeCode));
				type = null;
			}
			objs = null;
		}
		private void SelectInitValue()
		{
			string strSelected = this.Request.QueryString["selecteditem"];
			strSelected = "," + strSelected + ",";
			for (int i = 0; i < listTransType.Items.Count; i++)
			{
				if (strSelected.IndexOf("," + listTransType.Items[i].Value + ",") >= 0)
				{
					listTransType.Items[i].Selected = true;
				}
			}
		}

		protected void cmdSave_ServerClick(object sender, EventArgs e)
		{
			string strcode = "", strname = "";
			for (int i = 0; i < listTransType.Items.Count; i++)
			{
				if (listTransType.Items[i].Selected == true)
				{
					strcode += "," + listTransType.Items[i].Value;
					strname += "," + listTransType.Items[i].Text;
				}
			}
			if (strcode.Length > 0)
			{
				strcode = strcode.Substring(1);
				strname = strname.Substring(1);
			}
			this.txtSelectedItemCode.Value = strcode;
			this.txtSelectedItemName.Value = strname;
			this.Page.RegisterStartupScript("return to parent", "<script language='javascript'>ReturnValue()</script>");
		}
	}
}
