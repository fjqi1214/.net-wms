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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Security;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

using System.Collections.Generic;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FUserMP 的摘要说明。
	/// </summary>
    public partial class FVendorQuery : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

        private WarehouseFacade _facade = null;
       
	
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
			//this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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
			
            base.InitWebGrid();
//			this.gridWebGrid.Columns.FromKey("UserPassword").Hidden = true;
            this.gridHelper.AddColumn("VendorCodeN", "供应商代码", null);
            this.gridHelper.AddColumn("VendorNameN", "供应商名称", null);
            this.gridHelper.AddColumn("VendorALIAS", "供应商别名", null);
            this.gridHelper.AddColumn("VendorUser", "联系人", null);
            this.gridHelper.AddColumn("VendorAddres", "地址", null);
            this.gridHelper.AddColumn("VendorFax", "传真", null);
            this.gridHelper.AddColumn("VendorTelephone", "移动电话", null);
           
           
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
          
            this.gridHelper.AddDefaultColumn(false, false);
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override DataRow GetGridRow(object obj)
		{
       
            DataRow row = this.DtSource.NewRow();
            row["VendorCodeN"] = ((Vendor)obj).VendorCode;
            row["VendorNameN"] = ((Vendor)obj).VendorName;
            row["VendorALIAS"] = ((Vendor)obj).ALIAS;
            row["VendorUser"] = ((Vendor)obj).VENDORUSER;
            row["VendorAddres"] = ((Vendor)obj).VENDORADDR;
            row["VendorFax"] = ((Vendor)obj).FAXNO;
            row["VendorTelephone"] = ((Vendor)obj).MOBILENO;
            //row["MaintainUser"] = ((Vendor)obj).GetDisplayText("MaintainUser");
            row["MaintainUser"] = ((Vendor)obj).MaintainUser;
            row["MaintainDate"] = FormatHelper.ToDateString(((Vendor)obj).MaintainDate);
            
            return row;

		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null)
			{
                _facade = new WarehouseFacade(base.DataProvider);
			}
            return this._facade.GetVendor(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserNameQuery.Text)),
				inclusive, exclusive );
		}

        protected override int GetRowCount()
		{
			if(_facade==null)
			{
                _facade = new WarehouseFacade(base.DataProvider);
			}
            return this._facade.GetVendorCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserNameQuery.Text))
                );
		}
	
       

		#endregion


	}
}
