using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FInventoryPeriodStandardMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;

        #region Web 窗体设计器生成的代码

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);

            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

            this.ddlStorageAttributeEdit.SelectedIndexChanged += new EventHandler(ddlStorageAttributeEdit_SelectedIndexChanged);
            this.ddlPeriodGroupEdit.SelectedIndexChanged += new EventHandler(ddlPeriodGroupEdit_SelectedIndexChanged);
        }
        #endregion

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
            _InventoryFacade = new InventoryFacade(this.DataProvider);
            _SystemSettingFacade = new SystemSettingFacade(this.DataProvider);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.BuildStorageAttributeList();                
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        private void BuildStorageAttributeList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.ddlStorageAttributeEdit);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllStorageAttribute);
            builder.Build("ParameterDescription", "ParameterCode");
            this.ddlStorageAttributeEdit.Items.Insert(0, new ListItem("", ""));

            this.ddlStorageAttributeEdit.SelectedIndex = 0;
        }

        private void BuildPeriodGroupList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.ddlPeriodGroupEdit);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllPeriodGroup);
            builder.Build("PeiodGroup", "PeiodGroup");
            this.ddlPeriodGroupEdit.Items.Insert(0, new ListItem("", ""));

            this.ddlPeriodGroupEdit.SelectedIndex = 0;
        }

        private void BuildPeriodCodeList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.ddlPeriodCodeEdit);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllPeriodCode);
            builder.Build("InvPeriodCode", "InvPeriodCode");
            this.ddlPeriodCodeEdit.Items.Insert(0, new ListItem("", ""));

            this.ddlPeriodCodeEdit.SelectedIndex = 0;
        }

        private void ddlStorageAttributeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {            
            BuildPeriodGroupList();
        }

        private void ddlPeriodGroupEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildPeriodCodeList();
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("StorageAttributeCode", "库存属性", null);
            this.gridHelper.AddColumn("StorageAttribute", "库存属性", null);
            this.gridHelper.AddColumn("PeriodGroup", "账龄组", null);
            this.gridHelper.AddColumn("InvPeriodCode", "账龄代码", null);
            this.gridHelper.AddColumn("IndexValue", "指标值", null);
            this.gridHelper.AddDefaultColumn(true, true);

            this.gridWebGrid.Columns.FromKey("StorageAttributeCode").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            string inventoryType = ((InventoryPeriodStandard)obj).InventoryType;
            Domain.BaseSetting.Parameter parameter = (Domain.BaseSetting.Parameter)_SystemSettingFacade.GetParameter(inventoryType, "STORAGEATTRIBUTE");
            if (parameter != null)
            {
                inventoryType = parameter.ParameterDescription;
            }

            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //        "false",
            //        ((InventoryPeriodStandard)obj).InventoryType.ToString(),
            //        inventoryType,
            //        ((InventoryPeriodStandard)obj).PeriodGroup.ToString(),
            //        ((InventoryPeriodStandard)obj).InventoryPeriodCode.ToString(),
            //        ((InventoryPeriodStandard)obj).PercentageStandard.ToString("0.00") ,
            //        ""
            //    }
            //);

            DataRow row = this.DtSource.NewRow();
            row["StorageAttributeCode"] = ((InventoryPeriodStandard)obj).InventoryType.ToString();
            row["StorageAttribute"] = inventoryType;
            row["PeriodGroup"] = ((InventoryPeriodStandard)obj).PeriodGroup.ToString();
            row["InvPeriodCode"] = ((InventoryPeriodStandard)obj).InventoryPeriodCode.ToString();
            row["IndexValue"] = ((InventoryPeriodStandard)obj).PercentageStandard.ToString("0.00");
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._InventoryFacade.QueryInventoryPeriodStandard(string.Empty, inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            return this._InventoryFacade.QueryInventoryPeriodStandardCount(string.Empty);
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            this._InventoryFacade.AddInventoryPeriodStandard((InventoryPeriodStandard)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            this._InventoryFacade.DeleteInventoryPeriodStandard((InventoryPeriodStandard[])domainObjects.ToArray(typeof(InventoryPeriodStandard)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            this._InventoryFacade.UpdateInventoryPeriodStandard((InventoryPeriodStandard)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.ddlStorageAttributeEdit.Enabled = true;
                this.ddlPeriodGroupEdit.Enabled = true;
                this.ddlPeriodCodeEdit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.ddlStorageAttributeEdit.Enabled = false;
                this.ddlPeriodGroupEdit.Enabled = false;
                this.ddlPeriodCodeEdit.Enabled = false;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            InventoryPeriodStandard standard = this._InventoryFacade.CreateNewInventoryPeriodStandard();

            standard.InventoryType = this.ddlStorageAttributeEdit.SelectedValue;
            standard.PeriodGroup = this.ddlPeriodGroupEdit.SelectedValue;
            standard.InventoryPeriodCode = this.ddlPeriodCodeEdit.SelectedValue;
            standard.PercentageStandard = decimal.Parse(this.txtIndexValueEdit.Text);
            standard.MaintainUser = this.GetUserCode();

            return standard;
        }

        protected override object GetEditObject(GridRecord row)
        {

            object obj = _InventoryFacade.GetInventoryPeriodStandard(
                row.Items.FindItemByKey("StorageAttributeCode").Text.ToString(),
                row.Items.FindItemByKey("PeriodGroup").Text.ToString(),
                row.Items.FindItemByKey("InvPeriodCode").Text.ToString()
                );

            if (obj != null)
            {
                return (InventoryPeriodStandard)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.ddlStorageAttributeEdit.SelectedIndex = 0;
                ddlPeriodGroupEdit.Items.Clear();
                ddlPeriodCodeEdit.Items.Clear();
                this.txtIndexValueEdit.Text = string.Empty;

                return;
            }

            try
            {
                this.ddlStorageAttributeEdit.SelectedValue = ((InventoryPeriodStandard)obj).InventoryType;
                ddlStorageAttributeEdit_SelectedIndexChanged(null, null);
            }
            catch
            {
                this.ddlStorageAttributeEdit.SelectedIndex = 0;
            }

            try
            {
                this.ddlPeriodGroupEdit.SelectedValue = ((InventoryPeriodStandard)obj).PeriodGroup;
                ddlPeriodGroupEdit_SelectedIndexChanged(null, null);
            }
            catch
            {
                this.ddlPeriodGroupEdit.SelectedIndex = 0;
            }

            try
            {
                this.ddlPeriodCodeEdit.SelectedValue = ((InventoryPeriodStandard)obj).InventoryPeriodCode;
            }
            catch
            {
                this.ddlPeriodCodeEdit.SelectedIndex = 0;
            }

            this.txtIndexValueEdit.Text = ((InventoryPeriodStandard)obj).PercentageStandard.ToString("0.00");
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblStorageAttributeEdit, this.ddlStorageAttributeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblPeriodGroupEdit, this.ddlPeriodGroupEdit, 40, true));
            manager.Add(new LengthCheck(this.lblPeriodCodeEdit, this.ddlPeriodCodeEdit, 40, true));            
            manager.Add(new DecimalCheck(this.lblIndexValueEdit, this.txtIndexValueEdit, Convert.ToDecimal(0.00), Convert.ToDecimal(1.00), true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (!_InventoryFacade.CheckInventoryPeriodStandard(ddlStorageAttributeEdit.SelectedValue, ddlPeriodGroupEdit.SelectedValue, ddlPeriodCodeEdit.SelectedValue, decimal.Parse(txtIndexValueEdit.Text)))
            {
                WebInfoPublish.Publish(this, "$Error_CheckInventoryPeriodStandard", this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            string inventoryType = ((InventoryPeriodStandard)obj).InventoryType;
            Domain.BaseSetting.Parameter parameter = (Domain.BaseSetting.Parameter)_SystemSettingFacade.GetParameter(inventoryType, "STORAGEATTRIBUTE");
            if (parameter != null)
            {
                inventoryType = parameter.ParameterDescription;
            }

            return new string[]{
                inventoryType,
                ((InventoryPeriodStandard)obj).PeriodGroup.ToString(),
                ((InventoryPeriodStandard)obj).InventoryPeriodCode.ToString(),
                ((InventoryPeriodStandard)obj).PercentageStandard.ToString("0.00")                
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
                "StorageAttribute",
                "PeriodGroup",
                "InvPeriodCode",
                "IndexValue"
            };
        }

        #endregion

        #region DropDownList

        private object[] GetAllPeriodGroup()
        {
            if (this.ddlStorageAttributeEdit.SelectedValue != null && this.ddlStorageAttributeEdit.SelectedValue.Trim().Length > 0)
            {
                return _InventoryFacade.QueryPeriodGroupByStorageAttribute(this.ddlStorageAttributeEdit.SelectedValue);
            }
            else
            {
                return null;
            }
        }

        private object[] GetAllPeriodCode()
        {
            if (this.ddlPeriodGroupEdit.SelectedValue != null && this.ddlPeriodGroupEdit.SelectedValue.Trim().Length > 0)
            {
                return _InventoryFacade.QueryInvPeriod(this.ddlPeriodGroupEdit.SelectedValue, int.MinValue, int.MaxValue);
            }
            else
            {
                return null;
            }
        }

        private object[] GetAllStorageAttribute()
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(base.DataProvider);
            return systemSettingFacade.GetAllParametersOrderByEattribute1("STORAGEATTRIBUTE");
        }

        #endregion
    }
}
