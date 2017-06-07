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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FStorageMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;


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
            _SystemSettingFacade = new SystemSettingFacade(this.DataProvider);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitVirtualFlagList();
                this.InitSourceFlagList();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        //初始化虚拟库位下拉框
        /// <summary>
        /// 初始化虚拟库位
        /// </summary>
        private void InitVirtualFlagList()
        {
            this.drpVirtualFlagQuery.Items.Add(new ListItem("", ""));
            this.drpVirtualFlagQuery.Items.Add(new ListItem("是", "Y"));
            this.drpVirtualFlagQuery.Items.Add(new ListItem("否", "N"));
            this.drpVirtualFlagQuery.SelectedIndex = 0;
        }


        //初始化库位来源下拉框
        /// <summary>
        /// 初始化库位来源
        /// </summary>
        private void InitSourceFlagList()
        {
            this.drpSourceFlagQuery.Items.Add(new ListItem("", ""));
            this.drpSourceFlagQuery.Items.Add(new ListItem("SAP", "SAP"));
            this.drpSourceFlagQuery.Items.Add(new ListItem("MES", "MES"));
            this.drpSourceFlagQuery.SelectedIndex = 1;
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("StorageCode", "库位代码", null);
            this.gridHelper.AddColumn("StorageName", "库位名称", null);
            this.gridHelper.AddColumn("SProperty", "库位属性", null);
            this.gridHelper.AddColumn("VirtualFalg", "虚拟库位", null);
            this.gridHelper.AddColumn("SourceFalg", "库位来源", null);
            this.gridHelper.AddColumn("Address1", "地址1", null);
            this.gridHelper.AddColumn("ContactUser1", "联系人1", null);
            this.gridHelper.AddColumn("Address2", "地址2", null);
            this.gridHelper.AddColumn("ContactUser2", "联系人2", null);
            this.gridHelper.AddColumn("Address3", "地址3", null);
            this.gridHelper.AddColumn("ContactUser3", "联系人3", null);
            this.gridHelper.AddColumn("Address4", "地址4", null);
            this.gridHelper.AddColumn("ContactUser4", "联系人4", null);
            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["StorageCode"] = ((Storage)obj).StorageCode;
            row["StorageName"] = ((Storage)obj).StorageName;
            row["SProperty"] = ((Storage)obj).SProperty;
            row["VirtualFalg"] = ((Storage)obj).VirtualFlag;
            row["SourceFalg"] = ((Storage)obj).SourceFlag;
            row["Address1"] = ((Storage)obj).Address1;
            row["ContactUser1"] = ((Storage)obj).ContactUser1;
            row["Address2"] = ((Storage)obj).Address2;
            row["ContactUser2"] = ((Storage)obj).ContactUser2;
            row["Address3"] = ((Storage)obj).Address3;
            row["ContactUser3"] = ((Storage)obj).ContactUser3;
            row["Address4"] = ((Storage)obj).Address4;
            row["ContactUser4"] = ((Storage)obj).ContactUser4;
            row["MaintainUser"] = ((Storage)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Storage)obj).MaintainDate);
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryStorage(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageCodeQuery.Text)),
                FormatHelper.CleanString(this.txtStorageNameQuery.Text),
                FormatHelper.CleanString(this.drpVirtualFlagQuery.SelectedValue),
                FormatHelper.CleanString(this.drpSourceFlagQuery.SelectedValue),
                GlobalVariables.CurrentOrganizations.First().OrganizationID,
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryStorageCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageCodeQuery.Text)), 
                FormatHelper.CleanString(this.txtStorageNameQuery.Text),
                FormatHelper.CleanString(this.drpVirtualFlagQuery.SelectedValue),
                FormatHelper.CleanString(this.drpSourceFlagQuery.SelectedValue),
                GlobalVariables.CurrentOrganizations.First().OrganizationID);
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            int orgId = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            object obj = _InventoryFacade.GetStorage(orgId, this.txtStorageCodeEdit.Text);

            if (obj != null)
            {
                WebInfoPublish.Publish(this, "库位代码已存在", this.languageComponent1);
                return;
            }
            ((Storage)domainObject).SourceFlag = "MES";
            this._InventoryFacade.AddStorage((Storage)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            //不能删除SAP,如果库存明细中有该库位或者该库位已经相应的入库单或出库单产生，则不能删除
            foreach (Storage storage in domainObjects.ToArray())
            {
                if (storage.SourceFlag == "SAP")
                {
                    WebInfoPublish.Publish(this, "SAP库位不能删除", this.languageComponent1);
                    return;
                }
                bool isUsed = _InventoryFacade.CheckStorageIsUsed(storage.StorageCode);
                if (isUsed)
                {
                    WebInfoPublish.Publish(this, "库位已使用，不能删除", this.languageComponent1);
                    return;
                }
            }
          
            this._InventoryFacade.DeleteStorage((Storage[])domainObjects.ToArray(typeof(Storage)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            this._InventoryFacade.UpdateStorage((Storage)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtStorageCodeEdit.Enabled = true;
                this.txtStorageNameEdit.Enabled = true;
                this.txtSPropertyEdit.Enabled = true;
                this.cbxVirtualFlagEdit.Enabled = true;
                this.txtAddress1Edit.Enabled = true;
                this.txtAddress2Edit.Enabled = true;
                this.txtAddress3Edit.Enabled = true;
                this.txtAddress4Edit.Enabled = true;
                this.txtContactUser1Edit.Enabled = true;
                this.txtContactUser2Edit.Enabled = true;
                this.txtContactUser3Edit.Enabled = true;
                this.txtContactUser4Edit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtStorageCodeEdit.Enabled = false;
                string sourceFlag = Session["SourceFlag"].ToString();
                switch (sourceFlag)
                {
                    case "SAP":
                        this.txtStorageCodeEdit.Enabled = false;
                        this.txtStorageNameEdit.Enabled = false;
                        this.txtSPropertyEdit.Enabled = false;
                        this.cbxVirtualFlagEdit.Enabled = true;
                        this.txtAddress1Edit.Enabled = false;
                        this.txtAddress2Edit.Enabled = false;
                        this.txtAddress3Edit.Enabled = false;
                        this.txtAddress4Edit.Enabled = false;
                        this.txtContactUser1Edit.Enabled = false;
                        this.txtContactUser2Edit.Enabled = false;
                        this.txtContactUser3Edit.Enabled = false;
                        this.txtContactUser4Edit.Enabled = false;
                        break;
                    case "MES":
                        this.txtStorageCodeEdit.Enabled = false;
                        this.txtStorageNameEdit.Enabled = true;
                        this.txtSPropertyEdit.Enabled = true;
                        this.cbxVirtualFlagEdit.Enabled = true;
                        this.txtAddress1Edit.Enabled = true;
                        this.txtAddress2Edit.Enabled = true;
                        this.txtAddress3Edit.Enabled = true;
                        this.txtAddress4Edit.Enabled = true;
                        this.txtContactUser1Edit.Enabled = true;
                        this.txtContactUser2Edit.Enabled = true;
                        this.txtContactUser3Edit.Enabled = true;
                        this.txtContactUser4Edit.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            Storage storage = this._InventoryFacade.CreateStorage();

            storage.StorageCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageCodeEdit.Text, 40));
            storage.StorageName = FormatHelper.CleanString(this.txtStorageNameEdit.Text, 100);
            storage.SProperty = FormatHelper.CleanString(this.txtSPropertyEdit.Text, 40);
            storage.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            storage.Address1 = FormatHelper.CleanString(this.txtAddress1Edit.Text, 40);
            storage.Address2 = FormatHelper.CleanString(this.txtAddress2Edit.Text, 40);
            storage.Address3 = FormatHelper.CleanString(this.txtAddress3Edit.Text, 40);
            storage.Address4 = FormatHelper.CleanString(this.txtAddress4Edit.Text, 40);
            storage.ContactUser1 = FormatHelper.CleanString(this.txtContactUser1Edit.Text, 40);
            storage.ContactUser2 = FormatHelper.CleanString(this.txtContactUser2Edit.Text, 40);
            storage.ContactUser3 = FormatHelper.CleanString(this.txtContactUser3Edit.Text, 40);
            storage.ContactUser4 = FormatHelper.CleanString(this.txtContactUser4Edit.Text, 40);
            storage.VirtualFlag = this.cbxVirtualFlagEdit.Checked ? "Y" : "N";
            storage.MaintainUser = this.GetUserCode();
            if (Session["SourceFlag"] != null)
            {
                storage.SourceFlag = Session["SourceFlag"].ToString();
            }
            return storage;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            int orgId = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            object obj = _InventoryFacade.GetStorage(orgId, row.Items.FindItemByKey("StorageCode").Value.ToString());

            if (obj != null)
            {
                return (Storage)obj;
            }

            return null;
        }
        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtStorageCodeEdit.Text = string.Empty;
                this.txtStorageNameEdit.Text = string.Empty;
                this.txtSPropertyEdit.Text = string.Empty;
                this.cbxVirtualFlagEdit.Checked = false;
                this.txtAddress1Edit.Text = string.Empty;
                this.txtAddress2Edit.Text = string.Empty;
                this.txtAddress3Edit.Text = string.Empty;
                this.txtAddress4Edit.Text = string.Empty;
                this.txtContactUser1Edit.Text = string.Empty;
                this.txtContactUser2Edit.Text = string.Empty;
                this.txtContactUser3Edit.Text = string.Empty;
                this.txtContactUser4Edit.Text = string.Empty;

                return;
            }
            this.txtStorageCodeEdit.Text = ((Storage)obj).StorageCode;
            this.txtStorageNameEdit.Text = ((Storage)obj).StorageName;
            this.txtSPropertyEdit.Text = ((Storage)obj).SProperty;
            this.cbxVirtualFlagEdit.Checked = ((Storage)obj).VirtualFlag == "Y" ? true : false;
            this.txtAddress1Edit.Text = ((Storage)obj).Address1;
            this.txtAddress2Edit.Text = ((Storage)obj).Address2;
            this.txtAddress3Edit.Text = ((Storage)obj).Address3;
            this.txtAddress4Edit.Text = ((Storage)obj).Address4;
            this.txtContactUser1Edit.Text = ((Storage)obj).ContactUser1;
            this.txtContactUser2Edit.Text = ((Storage)obj).ContactUser2;
            this.txtContactUser3Edit.Text = ((Storage)obj).ContactUser3;
            this.txtContactUser4Edit.Text = ((Storage)obj).ContactUser4;
            Session["SourceFlag"] = ((Storage)obj).SourceFlag;
                     
        }

        protected override bool ValidateInput()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblStorageCodeEdit, this.txtStorageCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblStorageNameEdit, this.txtStorageNameEdit, 100, false));
            manager.Add(new LengthCheck(this.lblSPropertyEdit, this.txtSPropertyEdit, 40, false));
            manager.Add(new LengthCheck(this.lblAddress1Edit, this.txtAddress1Edit, 200, false));
            manager.Add(new LengthCheck(this.lblAddress2Edit, this.txtAddress2Edit, 200, false));
            manager.Add(new LengthCheck(this.lblAddress3Edit, this.txtAddress3Edit, 200, false));
            manager.Add(new LengthCheck(this.lblAddress4Edit, this.txtAddress4Edit, 200, false));
            manager.Add(new LengthCheck(this.lblContactUser1Edit, this.txtContactUser1Edit, 40, false));
            manager.Add(new LengthCheck(this.lblContactUser2Edit, this.txtContactUser2Edit, 40, false));
            manager.Add(new LengthCheck(this.lblContactUser3Edit, this.txtContactUser3Edit, 40, false));
            manager.Add(new LengthCheck(this.lblContactUser4Edit, this.txtContactUser4Edit, 40, false));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((Storage)obj).StorageCode,
                                ((Storage)obj).StorageName,
                                ((Storage)obj).SProperty,
                                ((Storage)obj).VirtualFlag,
                                ((Storage)obj).SourceFlag,
                                ((Storage)obj).Address1,
                                ((Storage)obj).ContactUser1,
                                ((Storage)obj).Address2,
                                ((Storage)obj).ContactUser2,
                                ((Storage)obj).Address3,
                                ((Storage)obj).ContactUser3,
                                 ((Storage)obj).Address4,
                                ((Storage)obj).ContactUser4,
                                ((Storage)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((Storage)obj).MaintainDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"StorageCode",
                                    "StorageName",
                                    "SProperty",
                                    "VirtualFalg",
                                    "SourceFalg",
                                    "Address1",	
                                    "ContactUser1",
                                    "Address2",	
                                    "ContactUser2",
                                    "Address3",	
                                    "ContactUser3",
                                    "Address4",	
                                    "ContactUser4",
                                    "MaintainUser",	
                                    "MaintainDate"};
        }

        #endregion

    }
}
