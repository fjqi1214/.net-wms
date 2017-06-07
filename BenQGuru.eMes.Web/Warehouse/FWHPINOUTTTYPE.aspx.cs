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

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FWHPINOUTTTYPE : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        #region Facade
        private WarehouseFacade m_WarehouseFacade = null;
        public WarehouseFacade WarehouseFacade
        {
            get
            {
                if (m_WarehouseFacade == null)
                {
                    return new WarehouseFacade(base.DataProvider);
                }
                else
                {
                    return m_WarehouseFacade;
                }
            }
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

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化控件
                this.BindBusinessType();
                this.BuildOrgList(this.drpOrgEdit);
                this.BuildSAPCode();
            }
        }

        private void BindBusinessType()
        {
            //绑定业务类型
            RadioButtonListBuilder builder = new RadioButtonListBuilder(new BussinessType(), this.rblBussinessType, this.languageComponent1);
            builder.Build();
        }
        private void BuildOrgList(DropDownList dropDownListForOrg)
        {
            DropDownListBuilder builder = new DropDownListBuilder(dropDownListForOrg);
            builder.HandleGetObjectList = new GetObjectListDelegate((new BaseModelFacade(this.DataProvider)).GetAllOrg);
            builder.Build("OrganizationDescription", "OrganizationID");
            dropDownListForOrg.Items.Insert(0, new ListItem("", ""));

            dropDownListForOrg.SelectedIndex = 0;
        }

        private void BuildSAPCode()
        {
            SystemSettingFacade systemFacade = new SystemSettingFacade(this.DataProvider);
            object[] objList = systemFacade.GetParametersByParameterGroup("SAPINTERFACECODE");
            if (objList != null)
            {
                this.drpSAPCode.Items.Clear();
                foreach (BenQGuru.eMES.Domain.BaseSetting.Parameter para in objList)
                {
                    this.drpSAPCode.Items.Add(new ListItem(para.ParameterDescription, para.ParameterAlias));
                }
                drpSAPCode.Items.Insert(0, new ListItem("", ""));
                drpSAPCode.SelectedIndex = 0;
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected void ddlBusinessTypeQuery_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlBusinessTypeQuery.Items.Insert(0, new ListItem("", ""));
                this.ddlBusinessTypeQuery.Items.Insert(1, new ListItem(languageComponent1.GetString(BussinessType.type_in), BussinessType.type_in));
                this.ddlBusinessTypeQuery.Items.Insert(2, new ListItem(languageComponent1.GetString(BussinessType.type_out), BussinessType.type_out));
            }
        }

        #endregion

        #region WebGrid
        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this.WarehouseFacade.QueryMaterialBusiness(
                     FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialBusinessCodeQuery.Text)), FormatHelper.CleanString(this.txtMaterialBusinessDescQuery.Text), FormatHelper.CleanString(this.ddlBusinessTypeQuery.SelectedValue), inclusive, exclusive);
        }
        protected override int GetRowCount()
        {
            int rowCount = this.WarehouseFacade.QueryMaterialBusinessCount(
                                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialBusinessCodeQuery.Text)), FormatHelper.CleanString(this.txtMaterialBusinessDescQuery.Text), FormatHelper.CleanString(this.ddlBusinessTypeQuery.SelectedValue));
            return rowCount;
        }
        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("MaterialBusinessCode", "业务代码", null);
            this.gridHelper.AddColumn("MaterialBusinessDesc", "业务描述", null);
            this.gridHelper.AddColumn("BusinessType", "业务类型", null);
            this.gridHelper.AddColumn("OrganizationDesc", "组织", null);
            this.gridHelper.AddColumn("SAPCode", "SAP业务代码", null);
            this.gridHelper.AddColumn("ISFIFO", "默认FIFO检查", null);

            this.gridHelper.AddDefaultColumn(true, true);
            this.gridHelper.ApplyLanguage(this.languageComponent1);

        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            MaterialBusiness materialBusiness = (MaterialBusiness)obj;
            object org = new BaseModelFacade(base.DataProvider).GetOrg(materialBusiness.OrgID);
            string orgDesc = string.Empty;
            if (org != null)
            {
                orgDesc = ((Organization)org).OrganizationDescription;
            }

            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
                                materialBusiness.BusinessCode,
                                materialBusiness.BusinessDesc,
                                languageComponent1.GetString(materialBusiness.BusinessType),
                                orgDesc,
                                materialBusiness.SAPCODE,
                                materialBusiness.ISFIFO});
        }
        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            this.WarehouseFacade.AddMaterialBusiness((MaterialBusiness)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            this.WarehouseFacade.DeleteMaterialBusiness((MaterialBusiness[])domainObjects.ToArray(typeof(MaterialBusiness)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            this.WarehouseFacade.UpdateMaterialBusiness((MaterialBusiness)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtMaterialBusinessCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtMaterialBusinessCodeEdit.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            MaterialBusiness materialBusiness = this.WarehouseFacade.CreateNewMaterialBusiness();
            materialBusiness.BusinessCode = txtMaterialBusinessCodeEdit.Text.Trim().ToUpper();
            materialBusiness.BusinessDesc = txtMaterialBusinessDescEdit.Text.Trim();
            if (this.rblBussinessType.SelectedIndex == 0)
            {
                //入库
                materialBusiness.BusinessType = BussinessType.type_in;
                if (chbFIFO.Checked == true)
                {
                    materialBusiness.ISFIFO = "Y";
                }
                else
                {
                    materialBusiness.ISFIFO = "N";
                }
            }
            else
            {
                materialBusiness.ISFIFO = "";
                materialBusiness.BusinessType = BussinessType.type_out;
            }
            materialBusiness.OrgID = int.Parse(drpOrgEdit.SelectedValue);
            materialBusiness.SAPCODE = this.drpSAPCode.SelectedValue;
            materialBusiness.MaintainDate = FormatHelper.TODateInt(DateTime.Now.ToShortDateString());
            materialBusiness.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            materialBusiness.MaintainUser = this.GetUserCode();

            return materialBusiness;
        }

        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            object obj = this.WarehouseFacade.GetMaterialBusiness(row.Cells[1].Text.ToString());

            if (obj != null)
            {
                return (MaterialBusiness)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                txtMaterialBusinessCodeEdit.Text = "";
                txtMaterialBusinessDescEdit.Text = "";
                drpOrgEdit.SelectedIndex = 0;
                this.rblBussinessType.SelectedIndex = 0;
                chbFIFO.Enabled = true;
                drpSAPCode.SelectedIndex = 0;
                chbFIFO.Checked = false;
                return;
            }

            MaterialBusiness materialBusiness = (MaterialBusiness)obj;
            txtMaterialBusinessCodeEdit.Text = materialBusiness.BusinessCode;
            txtMaterialBusinessDescEdit.Text = materialBusiness.BusinessDesc;
            try
            {
                this.drpOrgEdit.SelectedValue = materialBusiness.OrgID.ToString();
            }
            catch
            {
                this.drpOrgEdit.SelectedIndex = 0;
            }

            if (materialBusiness.BusinessType == BussinessType.type_in)
            {
                this.rblBussinessType.SelectedIndex = 0;
                chbFIFO.Enabled = true;
            }
            else
            {
                chbFIFO.Enabled = false;
                this.rblBussinessType.SelectedIndex = 1;
            }
            try
            {
                this.drpSAPCode.SelectedValue = materialBusiness.SAPCODE;
            }
            catch
            {
                this.drpSAPCode.SelectedIndex = 0;
            }
            if (materialBusiness.ISFIFO == "Y")
            {
                chbFIFO.Checked = true;
            }
            else
            {
                chbFIFO.Checked = false;
            }

        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblMaterialBusinessCodeEdit, this.txtMaterialBusinessCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblMaterialBusinessDescEdit, this.txtMaterialBusinessDescEdit, 100, true));
            manager.Add(new LengthCheck(this.lblOrgEdit, this.drpOrgEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            if (this.txtMaterialBusinessCodeEdit.ReadOnly == false)
            {
                //当是新增状态
                object obj = this.WarehouseFacade.GetMaterialBusiness(this.txtMaterialBusinessCodeEdit.Text.Trim().ToUpper());
                if (obj != null)
                {
                    WebInfoPublish.Publish(this, "$Error_Primary_Key_Overlap", this.languageComponent1);
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            MaterialBusiness materialBusiness = (MaterialBusiness)obj;
            object org = new BaseModelFacade(base.DataProvider).GetOrg(materialBusiness.OrgID);
            string orgDesc = string.Empty;
            if (org != null)
            {
                orgDesc = ((Organization)org).OrganizationDescription;
            }

            return new string[]{materialBusiness.BusinessCode,
                                materialBusiness.BusinessDesc,
                                languageComponent1.GetString(materialBusiness.BusinessType),
                                orgDesc,
                                materialBusiness.SAPCODE,
                                materialBusiness.ISFIFO};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"MaterialBusinessCode",
                                    "MaterialBusinessDesc",
                                    "BusinessType",
                                    "OrganizationDesc",
                                    "SAPCode",
                                    "ISFIFO"};
        }

        #endregion

        #region Event
        protected void rblBussinessType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.rblBussinessType.SelectedIndex == 0)
            {
                chbFIFO.Enabled = true;
            }
            else
            {
                chbFIFO.Checked = false;
                chbFIFO.Enabled = false;
            }
        }
        #endregion
    }
}
