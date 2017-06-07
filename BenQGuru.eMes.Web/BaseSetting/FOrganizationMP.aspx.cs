using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class FOrganizationMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private BenQGuru.eMES.BaseSetting.BaseModelFacade _facade = null;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPageLanguage(this.languageComponent1, false);
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #region Grid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("OrganizationCode", "组织编号", null);
            this.gridHelper.AddColumn("OrganizationName", "组织名称", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
           
            DataRow row = this.DtSource.NewRow();
            row["OrganizationCode"] = ((Organization)obj).OrganizationID.ToString();
            row["OrganizationName"] = ((Organization)obj).OrganizationDescription;
            row["MaintainUser"] = ((Organization)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Organization)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Organization)obj).MaintainTime);
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }

            return _facade.GetOrgList(this.txtOrganizationNameQuery.Text.Trim(), inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return _facade.GetOrgListCount(this.txtOrganizationNameQuery.Text.Trim()) ;
        }

        #endregion

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.AddOrg((Organization)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.DeleteOrg((Organization[])domainObjects.ToArray(typeof(Organization)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            this._facade.UpdateOrg((Organization)domainObject);
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtOrganizationCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtOrganizationCodeEdit.ReadOnly = true;
            }
        }

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            Organization org = new Organization();
            org.OrganizationID = int.Parse(this.txtOrganizationCodeEdit.Text.Trim());
            org.OrganizationDescription = this.txtOrganizationNameEdit.Text.Trim();
            org.MaintainUser = this.GetUserCode();

            return org;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            object obj = _facade.GetOrg(int.Parse(row.Items.FindItemByKey("OrganizationCode").Text.Trim()));

            if (obj != null)
            {
                return (Organization)obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtOrganizationCodeEdit.Text = "";
                this.txtOrganizationNameEdit.Text = "";
                return;
            }
            this.txtOrganizationCodeEdit.Text = ((Organization)obj).OrganizationID.ToString();
            this.txtOrganizationNameEdit.Text = ((Organization)obj).OrganizationDescription;
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new NumberCheck(this.lblOrganizationCodeEdit, this.txtOrganizationCodeEdit,true));
            manager.Add(new LengthCheck(this.lblOrganizationNameEdit, this.txtOrganizationNameEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #region Export
        // 2005-04-06

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((Organization)obj).OrganizationID.ToString(),
								   ((Organization)obj).OrganizationDescription,
                                   //((Organization)obj).MaintainUser,
                    ((Organization)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((Organization)obj).MaintainDate),
								   FormatHelper.ToTimeString(((Organization)obj).MaintainTime)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"OrganizationCode",
									"OrganizationName",	
									"MaintainUser",
									"MaintainDate",
                                    "MaintainTime"};
        }

        #endregion
    }
}
