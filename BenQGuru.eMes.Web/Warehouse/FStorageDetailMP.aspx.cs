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
    public partial class FStorageDetailMP : BaseMPageNew
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
                this.cmdSave.Disabled = true;
                InitTextBox();
                InitStorageList();
            }
        }
        #region  单据类型下拉框
        private void InitStorageList()
        {

            //InventoryFacade facade = new InventoryFacade(base.DataProvider);
            //object[] objStorage = facade.GetAllStorage();
            //drpStorageQuery.Items.Clear();
            //drpStorageQuery.Items.Add(new ListItem("", ""));
            //if (objStorage != null && objStorage.Length > 0)
            //{
            //    foreach (Storage storage in objStorage)
            //    {

            //        this.drpStorageQuery.Items.Add(new ListItem(
            //             storage.StorageCode + "-" + storage.StorageName, storage.StorageCode)
            //            );
            //    }
            //}
            //this.drpStorageQuery.SelectedIndex = 0;

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser(GetUserCode());//+TOSTORAGE
            this.drpStorageQuery.Items.Add(new ListItem("", ""));
            object[] parameters = systemSettingFacade.GetDistinctParaInParameterGroup(usergroupList);
            if (parameters != null)
            {
                foreach (Domain.BaseSetting.Parameter parameter in parameters)
                {
                    drpStorageQuery.Items.Add(new ListItem(parameter.ParameterCode + "-" + parameter.ParameterDescription, parameter.ParameterCode));
                }
            }
            this.drpStorageQuery.SelectedIndex = 0;

        }
        #endregion
        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("MaterialNo", "物料编码", null);
            this.gridHelper.AddColumn("DQMCode", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("MDesc", "物料名称", null);
            this.gridHelper.AddColumn("StorageCode", "库位代码", null);
            this.gridHelper.AddColumn("StorageName", "库位名称", null);
            this.gridHelper.AddColumn("LocationCode", "货位代码", null);
            this.gridHelper.AddColumn("LocationName", "货位名称", null);
            this.gridHelper.AddColumn("CartonNo", "箱号", null);
            this.gridHelper.AddColumn("Unit", "单位", null);
            this.gridHelper.AddColumn("StorageQty", "库存数量", null);
            this.gridHelper.AddColumn("FreezeQty", "冻结数量", null);
            this.gridHelper.AddColumn("FirstInDate", "首次入库时间", null);
            this.gridHelper.AddColumn("StorageAgeDate", "有效期起算时间", null);
            this.gridHelper.AddColumn("StorageAgeEndDate", "有效期截止时间", null);
            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            this.gridHelper.AddColumn("SN", "SN", null);
            this.gridWebGrid.Columns["SN"].Hidden = true;

            this.gridHelper.AddDefaultColumn(false, true);
            this.gridHelper.AddLinkColumn("LinkToSN", "查看SN");



            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();

            row["MaterialNo"] = ((StorageDetailExt)obj).MCode;
            row["DQMCode"] = ((StorageDetailExt)obj).DQMCode;
            row["MDesc"] = ((StorageDetailExt)obj).MDesc;
            row["StorageCode"] = ((StorageDetailExt)obj).StorageCode;
            row["StorageName"] = ((StorageDetailExt)obj).StorageName;
            row["LocationCode"] = ((StorageDetailExt)obj).LocationCode;
            row["LocationName"] = ((StorageDetailExt)obj).LocationName;
            row["CartonNo"] = ((StorageDetailExt)obj).CartonNo;
            row["Unit"] = ((StorageDetailExt)obj).Unit;
            row["StorageQty"] = ((StorageDetailExt)obj).StorageQty;
            row["FreezeQty"] = ((StorageDetailExt)obj).FreezeQty;
            _InventoryFacade = new InventoryFacade(base.DataProvider);

            row["FirstInDate"] = FormatHelper.ToDateString(_InventoryFacade.FirstInDate(((StorageDetailExt)obj).CartonNo,
                                                              ((StorageDetailExt)obj).MCode));
            row["StorageAgeDate"] = FormatHelper.ToDateString(((StorageDetailExt)obj).StorageAgeDate);
            int storageAgeEndDate = 0;
            if (((StorageDetailExt)obj).StorageAgeDate != 0)
            {
                storageAgeEndDate =
FormatHelper.TODateInt(FormatHelper.ToDateTime(((StorageDetailExt)obj).StorageAgeDate)
              .AddDays(((StorageDetailExt)obj).Validity));

            }
            row["StorageAgeEndDate"] = FormatHelper.ToDateString(storageAgeEndDate);// ((StorageDetailExt)obj).Validity == 0 ? ((StorageDetailExt)obj).StorageAgeDate.ToString() :storageAgeEndDate;

            row["MaintainUser"] = ((StorageDetailExt)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((StorageDetailExt)obj).MaintainDate);
            row["SN"] = ((StorageDetailExt)obj).SN;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return this._InventoryFacade.QueryStorageDetail(
                FormatHelper.CleanString(this.txtMCodeQuery.Text),
                FormatHelper.CleanString(this.txtDQMoCodeQuery.Text),
                FormatHelper.CleanString(this.drpStorageQuery.SelectedValue),
                FormatHelper.CleanString(this.txtLocationCodeQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonNoQuery.Text)),
                FormatHelper.CleanString(this.txtSNQuery.Text), GetUserCode(),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryStorageDetailCount(
                FormatHelper.CleanString(this.txtMCodeQuery.Text),
                FormatHelper.CleanString(this.txtDQMoCodeQuery.Text),
                FormatHelper.CleanString(drpStorageQuery.SelectedValue),
                FormatHelper.CleanString(this.txtLocationCodeQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonNoQuery.Text)),
                FormatHelper.CleanString(this.txtSNQuery.Text), GetUserCode());
        }

        #endregion

        #region Button
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "LinkToSN")
            {
                string mCode = row.Items.FindItemByKey("MaterialNo").Text.Trim();
                string dQMCode = row.Items.FindItemByKey("DQMCode").Text.Trim();
                string storageCode = row.Items.FindItemByKey("StorageCode").Text.Trim();
                string locationCode = row.Items.FindItemByKey("LocationCode").Text.Trim();
                string cartonNo = row.Items.FindItemByKey("CartonNo").Text.Trim();
                string sn = row.Items.FindItemByKey("SN").Text.Trim();

                Response.Redirect(
                                    this.MakeRedirectUrl("FStorageDetailSN.aspx",
                                    new string[] { "MCode", "DQMCode", "StorageCode", "LocationCode", "CartonNo", "SN" },
                                    new string[] { mCode, dQMCode, storageCode, locationCode, cartonNo, sn })
                                   );
            }
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            this._InventoryFacade.UpdateStorageDetail((StorageDetail)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                InitTextBox();
            }

            if (pageAction == PageActionType.Update)
            {
                InitTextBox();
            }
        }
        private void InitTextBox()
        {
            this.txtMCodeEdit.Enabled = false;
            this.txtDQMoCodeEdit.Enabled = false;
            this.txtStorageCodeEdit.Enabled = false;
            this.txtLocationCodeEdit.Enabled = false;
            this.txtCartonNoEdit.Enabled = false;
        }
        #endregion

        #region Object <--> Page
        protected override object GetEditObject()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            StorageDetail storageDetail = (StorageDetail)Session["storageDetail"];
            storageDetail.StorageAgeDate = FormatHelper.TODateInt(this.StorageAgeDateEdit.Text);
            return storageDetail;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            int orgId = GlobalVariables.CurrentOrganizations.First().OrganizationID;

            object obj = _InventoryFacade.GetStorageDetail(row.Items.FindItemByKey("CartonNo").Value.ToString());

            if (obj != null)
            {
                Session["storageDetail"] = obj;
                return (StorageDetail)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtMCodeEdit.Text = string.Empty;
                this.txtDQMoCodeEdit.Text = string.Empty;
                this.txtStorageCodeEdit.Text = string.Empty;
                this.txtLocationCodeEdit.Text = string.Empty;
                this.txtCartonNoEdit.Text = string.Empty;
                this.StorageAgeDateEdit.Text = string.Empty;
                return;
            }
            this.txtMCodeEdit.Text = ((StorageDetail)obj).MCode;
            this.txtDQMoCodeEdit.Text = ((StorageDetail)obj).DQMCode;
            this.txtStorageCodeEdit.Text = ((StorageDetail)obj).StorageCode;
            this.txtLocationCodeEdit.Text = ((StorageDetail)obj).LocationCode;
            this.txtCartonNoEdit.Text = ((StorageDetail)obj).CartonNo;
            this.StorageAgeDateEdit.Text = FormatHelper.ToDateString(((StorageDetail)obj).StorageAgeDate, "-");
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblMCodeEdit, this.txtMCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblDQMoCodeEdit, this.txtDQMoCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblStorageCodeEdit, this.txtStorageCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblLocationCodeEdit, this.txtLocationCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblCartonNoEdit, this.txtCartonNoEdit, 40, true));
            manager.Add(new LengthCheck(this.lblStorageAgeDateEdit, this.StorageAgeDateEdit, 22, true));

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
            try
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
                int storageAgeEndDate = 0;
                if (((StorageDetailExt)obj).StorageAgeDate != 0)
                {
                    storageAgeEndDate =
    FormatHelper.TODateInt(
            FormatHelper.ToDateTime(((StorageDetailExt)obj).StorageAgeDate)
                  .AddDays(((StorageDetailExt)obj).Validity));

                }
                return new string[]{((StorageDetailExt)obj).MCode,
                                ((StorageDetailExt)obj).DQMCode,
                                ((StorageDetailExt)obj).MDesc,
                                ((StorageDetailExt)obj).StorageCode,
                                ((StorageDetailExt)obj).StorageName,
                                ((StorageDetailExt)obj).LocationCode,
                                ((StorageDetailExt)obj).LocationName,
                                ((StorageDetailExt)obj).CartonNo,
                                ((StorageDetailExt)obj).Unit,
                                ((StorageDetailExt)obj).StorageQty.ToString(),
                                ((StorageDetailExt)obj).FreezeQty.ToString(),
                                FormatHelper.ToDateString(_InventoryFacade.FirstInDate(((StorageDetailExt)obj).CartonNo,
                                                              ((StorageDetailExt)obj).MCode)),
                                FormatHelper.ToDateString(((StorageDetailExt)obj).StorageAgeDate),
                                FormatHelper.ToDateString(storageAgeEndDate),
                                ((StorageDetailExt)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((StorageDetailExt)obj).MaintainDate)
                                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"MaterialNo",
                                    "DQMCode",
                                    "MDesc",
                                    "StorageCode",
                                    "StorageName",
                                    "LocationCode",	
                                    "LocationName",
                                    "CartonNo",	
                                    "Unit",
                                    "StorageQty",	
                                    "FreezeQty",
                                    "FirstInDate",
                                    "StorageAgeDate",
                                    "StorageAgeEndDate",	
                                    "MaintainUser",
                                    "MaintainDate",
                                    
            };
        }

        #endregion

    }
}
