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

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FOperation2ResourceSP 的摘要说明。
    /// </summary>
    public partial class FUserGroup2UserAP : BaseAPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblUserSelectTitle;

        private UserFacade facade = null;//new SystemSettingFacadeFactory().CreateUserFacade();


        #region Stable
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

        #region Not Stable

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            //UserGridColumnBuilder builder = new UserGridColumnBuilder(this.gridWebGrid);
            //builder.BuildForSelectPage();

            this.gridHelper.AddColumn("UserCode", "用户代码", null);
            this.gridHelper.AddColumn("UserName", "用户名", null);
            this.gridHelper.AddColumn("PhoneNumber", "电话号码", null);
            this.gridHelper.AddColumn("Email", "电子邮箱", null);
            this.gridHelper.AddColumn("Department", "部门", null);
            this.gridHelper.AddColumn("Organization", "默认组织", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.AddDefaultColumn(true, false);
        }

        protected override void AddDomainObject(ArrayList domainObject)
        {
            if (facade == null)
            {
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade();
            }
            facade.AddUserGroup2User((UserGroup2User[])domainObject.ToArray(typeof(UserGroup2User)));
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade();
            }
            UserGroup2User relation = facade.CreateUserGroup2User();

            relation.UserGroupCode = this.txtUserGroupCodeQuery.Text.Trim();
            relation.UserCode = row.Items.FindItemByKey("UserCode").Text;
            relation.MaintainUser = this.GetUserCode();

            return relation;
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade();
            }
            return this.facade.GetUnselectedUserByUserGroupCodeCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserCodeQuery.Text)));
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",((UserEx)obj).UserCode.ToString(),
            //                    ((UserEx)obj).UserName.ToString(),
            //                    ((UserEx)obj).UserTelephone.ToString(),
            //                    ((UserEx)obj).UserEmail.ToString(),
            //                    ((UserEx)obj).UserDepartment.ToString(),
            //                    ((UserEx)obj).DefaultOrgDesc.ToString(),
            //                    //((UserEx)obj).MaintainUser.ToString(),
            //               ((UserEx)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((UserEx)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((UserEx)obj).MaintainTime)
            //                    });
            DataRow row = this.DtSource.NewRow();
            row["UserCode"] = ((UserEx)obj).UserCode.ToString();
            row["UserName"] = ((UserEx)obj).UserName.ToString();
            row["PhoneNumber"] = ((UserEx)obj).UserTelephone.ToString();
            row["Email"] = ((UserEx)obj).UserEmail.ToString();
            row["Department"] = ((UserEx)obj).UserDepartment.ToString();
            row["Organization"] = ((UserEx)obj).DefaultOrgDesc.ToString();
            row["MaintainUser"] = ((UserEx)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((UserEx)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((UserEx)obj).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new SystemSettingFacadeFactory(base.DataProvider).CreateUserFacade();
            }
            return this.facade.GetUnselectedUserAndOrgIDByUserGroupCode(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserCodeQuery.Text)),
                inclusive, exclusive);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FUserGroup2UserSP.aspx", new string[] { "usergroupcode" }, new string[] { this.txtUserGroupCodeQuery.Text }));
        }
        #endregion

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
    }
}
