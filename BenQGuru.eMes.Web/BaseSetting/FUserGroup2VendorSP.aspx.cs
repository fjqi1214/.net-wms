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

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FOperation2ResourceSP 的摘要说明。
    /// </summary>
    public partial class FUserGroup2VendorSP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private UserFacade facade = null;

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

                this.txtUserGroupCodeQuery.Text = this.GetRequestParam("usergroupcode");
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
            this.gridHelper.AddColumn("VendorCodeN", "供应商代码", null);
            this.gridHelper.AddColumn("VendorNameN", "供应商名称", null);
            this.gridHelper.AddColumn("VendorALIAS", "供应商别名", null);
            this.gridHelper.AddColumn("VendorUser", "联系人", null);
            this.gridHelper.AddColumn("VendorAddres", "地址", null);
            this.gridHelper.AddColumn("VendorFax", "传真", null);
            this.gridHelper.AddColumn("VendorTelephone", "移动电话", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddDefaultColumn(true, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.gridHelper.RequestData();
        }

       
        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade();
            }
            return facade.GetSelectedVendorByUserGroupCode(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade();
            }
            return this.facade.GetSelectedVendorByUserGroupCodeCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeQuery.Text)));
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
            row["MaintainUser"] = ((Vendor)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Vendor)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Vendor)obj).MaintainTime);
            return row;

        }

        #endregion

        #region Button

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade();
            }
            facade.DeleteUserGroup2Vendor((UserGroup2Vendor[])domainObjects.ToArray(typeof(UserGroup2Vendor)));
        }

        protected void cmdSelect_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FUserGroup2VendorAP.aspx", new string[] { "usergroupcode" }, new string[] { this.txtUserGroupCodeQuery.Text.Trim() }));
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FUserGroupMP.aspx"));
        }

        //刷新页面使用
        protected void btnRefesh_Click(object sender, EventArgs e)
        {
            this.gridHelper.RequestData();
        } 

        #endregion

        #region Object <--> Page
        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade();
            }
            UserGroup2Vendor relation = facade.CreateNewUserGroup2Vendor();

            relation.UserGroupCode = this.txtUserGroupCodeQuery.Text.Trim();
            relation.VendorCode = row.Items.FindItemByKey("VendorCodeN").Text;
            relation.MaintainUser = this.GetUserCode();

            return relation;
        } 
        #endregion
    }
}
