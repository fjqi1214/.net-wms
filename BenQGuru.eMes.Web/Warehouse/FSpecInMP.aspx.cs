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
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.MOModel;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FSpecInMP : BaseMPageNew
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
                this.InitStorageInList();
                this.InitLoationInList();
                InitEditTextBox(false);
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        //初始化入库库位下拉框
        /// <summary>
        /// 初始化入库库位
        /// </summary>
        private void InitStorageInList()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            object[] objStorage = _InventoryFacade.GetStorage("MES");
            if (objStorage != null && objStorage.Length > 0)
            {
                foreach (Storage storage in objStorage)
                {
                    this.drpStorageInEdit.Items.Add(new ListItem(storage.StorageName, storage.StorageCode));
                }
                this.drpStorageInEdit.SelectedIndex = 0;
            }

        }


        ////初始化入库货位下拉框
        ///// <summary>
        ///// 初始化入库货位
        ///// </summary>
        private void InitLoationInList()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            string storageCode = this.drpStorageInEdit.SelectedValue;
            if (!string.IsNullOrEmpty(storageCode))
            {
                object[] objLocation = _InventoryFacade.GetLocation("MES", storageCode);
                this.drpLoationInEdit.Items.Clear();
                if (objLocation != null && objLocation.Length > 0)
                {
                    foreach (Location location in objLocation)
                    {
                        this.drpLoationInEdit.Items.Add(new ListItem(location.LocationName, location.LocationCode));
                    }
                    this.drpLoationInEdit.SelectedIndex = 0;
                }
            }

        }

        //初始化编辑区域文本框启用状态
        /// <summary>
        /// 初始化编辑区域文本框启用状态
        /// </summary>
        /// <param name="b">true/false</param>
        private void InitEditTextBox(bool b)
        {
            this.txtMaterialENDesc.Enabled = b;
            this.txtMaterialCHDesc.Enabled = b;
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("MaterialNo", "物料编码", null);
            this.gridHelper.AddColumn("DQMCode", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("ENSDesc", "英文短描述", null);
            this.gridHelper.AddColumn("ENLDesc", "英文长描述", null);
            this.gridHelper.AddColumn("CHSDesc", "中文短描述", null);
            this.gridHelper.AddColumn("CHLDesc", "中文长描述", null);
            this.gridHelper.AddColumn("MESMaterialDesc", "MES物料描述", null);
            this.gridHelper.AddColumn("StorageIn", "入库库位", null);
            this.gridHelper.AddColumn("LoationIn", "入库货位", null);
            this.gridHelper.AddColumn("MUOM", "单位", null);
            this.gridHelper.AddColumn("QTY", "数量", null);
            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddDefaultColumn(false, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["MaterialNo"] = ((SpecInOutWithMaterial)obj).MCode;
            row["DQMCode"] = ((SpecInOutWithMaterial)obj).DqmCode;
            row["ENSDesc"] = ((SpecInOutWithMaterial)obj).MenshortDesc;
            row["ENLDesc"] = ((SpecInOutWithMaterial)obj).MenlongDesc;
            row["CHSDesc"] = ((SpecInOutWithMaterial)obj).MchshortDesc;
            row["CHLDesc"] = ((SpecInOutWithMaterial)obj).MchlongDesc;
            row["MESMaterialDesc"] = ((SpecInOutWithMaterial)obj).InOutDesc;
            row["StorageIn"] = ((SpecInOutWithMaterial)obj).StorageName;
            row["LoationIn"] = ((SpecInOutWithMaterial)obj).LocationName;
            row["MUOM"] = ((SpecInOutWithMaterial)obj).Muom;
            row["QTY"] = ((SpecInOutWithMaterial)obj).Qty;
            row["MaintainUser"] = ((SpecInOutWithMaterial)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((SpecInOutWithMaterial)obj).MaintainDate);

            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return this._InventoryFacade.QuerySpecInOut(
                FormatHelper.CleanString(this.txtMaterialNOQuery.Text),
                FormatHelper.CleanString(this.txtMaterialDescQuery.Text),
                FormatHelper.TODateInt(this.bDate.Text),
                FormatHelper.TODateInt(this.eDate.Text),
                "I",//入库状态
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QuerySpecInOutCount(
                FormatHelper.CleanString(this.txtMaterialNOQuery.Text),
                FormatHelper.CleanString(this.txtMaterialDescQuery.Text),
                FormatHelper.TODateInt(this.bDate.Text),
                FormatHelper.TODateInt(this.eDate.Text),
                "I"//入库状态
                );
        }

        #endregion

        #region Button
        //新增
        protected override void AddDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            string dQMCode = FormatHelper.CleanString(this.txtMaterialNO.Text);
            Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterialByDQMCode(dQMCode);
            Domain.MOModel.Material newMaterial = null;
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            try
            {
                this.DataProvider.BeginTransaction();
                //如果该物料编码不存在物料主表，新增一笔数据
                if (material == null)
                {
                    newMaterial = new Domain.MOModel.Material();
                    newMaterial.MCode = FormatHelper.CleanString(this.txtMaterialNO.Text, 40);
                    newMaterial.DqmCode = FormatHelper.CleanString(this.txtMaterialNO.Text, 40);
                    newMaterial.MspecialDesc = FormatHelper.CleanString(this.txtSpecialDescEdit.Text, 200);
                    newMaterial.Muom = FormatHelper.CleanString(this.txtUnitEdit.Text, 40);
                    newMaterial.MType = "itemtype_finishedproduct";
                    newMaterial.Sourceflag = "MES";
                    newMaterial.CUser = this.GetUserCode();
                    newMaterial.CDate = dbDateTime.DBDate;
                    newMaterial.CTime = dbDateTime.DBTime;
                    newMaterial.MaintainUser = this.GetUserCode();
                }
  
                if (newMaterial != null)
                {
                    itemFacade.AddMaterial(newMaterial);
                }
                //同时更新库存
                SpecStorageInfo specStorageInfo = (SpecStorageInfo)_InventoryFacade.GetSpecStorageInfo(((SpecInOut)domainObject).StorageCode,
                                                                                                        ((SpecInOut)domainObject).MCode,
                                                                                                        ((SpecInOut)domainObject).LocationCode);
                if (specStorageInfo != null)
                {
                    specStorageInfo.StorageQty = specStorageInfo.StorageQty + Convert.ToInt32(this.txtQTY.Text.Trim());
                    this._InventoryFacade.UpdateSpecStorageInfo(specStorageInfo);
                }
                else
                {
                    specStorageInfo = this._InventoryFacade.CreateNewSpecStorageInfo();
                    specStorageInfo.MCode = ((SpecInOut)domainObject).MCode;
                    specStorageInfo.DQMCode = ((SpecInOut)domainObject).DQMCode;
                    specStorageInfo.Muom = ((SpecInOut)domainObject).Muom;
                    specStorageInfo.StorageCode = ((SpecInOut)domainObject).StorageCode;
                    specStorageInfo.LocationCode = ((SpecInOut)domainObject).LocationCode;
                    specStorageInfo.StorageQty = ((SpecInOut)domainObject).Qty;
                    specStorageInfo.CUser = this.GetUserCode();
                    specStorageInfo.CDate = dbDateTime.DBDate;
                    specStorageInfo.CTime = dbDateTime.DBTime;
                    specStorageInfo.MaintainUser = this.GetUserCode();

                    this._InventoryFacade.AddSpecStorageInfo(specStorageInfo);
                }

                this._InventoryFacade.AddSpecInOut((SpecInOut)domainObject);
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {

                WebInfoPublish.Publish(this, "新增失败：" + ex.Message, this.languageComponent1);
                this.DataProvider.RollbackTransaction();
            }



        }
        //确定
        protected void cmdOK_ServerClick(object sender, System.EventArgs e)
        {
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            string dQMCode = FormatHelper.CleanString(this.txtMaterialNO.Text);
            this.txtSpecialDescEdit.Text = "";
            this.txtMaterialENDesc.Text = "";
            this.txtMaterialCHDesc.Text = "";
            this.txtUnitEdit.Text = "";
            if (string.IsNullOrEmpty(dQMCode))
            {
                WebInfoPublish.Publish(this, "物料编码为空", this.languageComponent1);
                this.txtMaterialNO.Focus();
                return;
            }
            Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterialByDQMCode(dQMCode);
            if (material == null)
            {
                WebInfoPublish.Publish(this, "物料主数据没有该物料编码： " + dQMCode, this.languageComponent1);
                this.txtMaterialNO.Focus();
                return;
            }
            this.txtMaterialENDesc.Text = material.MenshortDesc;
            this.txtMaterialCHDesc.Text = material.MchshortDesc;
            this.txtUnitEdit.Text = material.Muom;

        }
        //新建
        protected void cmdNew_ServerClick(object sender, System.EventArgs e)
        {
            WarehouseFacade warehouseFacade = new WarehouseFacade(this.DataProvider);
            string preFix = "MES";
            object objSerialBook = warehouseFacade.GetSerialBook(preFix);
            if (objSerialBook == null)
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                SERIALBOOK serialBook = new SERIALBOOK();
                serialBook.SNPrefix = preFix;
                serialBook.MaxSerial = "1";
                serialBook.MUser = this.GetUserCode();
                serialBook.MDate = dbDateTime.DBDate;
                serialBook.MTime = dbDateTime.DBTime;
                try
                {
                    warehouseFacade.AddSerialBook(serialBook);
                    this.txtMaterialNO.Text = "MES000001";
                }
                catch (Exception ex)
                {

                    WebInfoPublish.Publish(this, "新建失败：" + ex.Message, this.languageComponent1);
                }
            }
            else
            {
                SERIALBOOK serialBook = (SERIALBOOK)objSerialBook;
                if (serialBook.MaxSerial == "999999")
                {
                    WebInfoPublish.Publish(this, "物料编码已用完！", this.languageComponent1);
                    return;
                }
                serialBook.MaxSerial = (Convert.ToInt32(serialBook.MaxSerial) + 1).ToString();
                try
                {
                    warehouseFacade.UpdateSerialBook(serialBook);
                    this.txtMaterialNO.Text = serialBook.SNPrefix + serialBook.MaxSerial.PadLeft(6, '0');
                }
                catch (Exception ex)
                {

                    WebInfoPublish.Publish(this, "新建失败：" + ex.Message, this.languageComponent1);
                }
            }
        }
        //库位下拉框索引改变
        protected void drpStorageInEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            string storageCode = this.drpStorageInEdit.SelectedValue;
            object[] objLocation = _InventoryFacade.GetLocation("MES", storageCode);
            this.drpLoationInEdit.Items.Clear();
            if (objLocation != null && objLocation.Length > 0)
            {
                foreach (Location location in objLocation)
                {
                    this.drpLoationInEdit.Items.Add(new ListItem(location.LocationName, location.LocationCode));
                }
                this.drpLoationInEdit.SelectedIndex = 0;
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

            SpecInOut specInOut = this._InventoryFacade.CreateNewSpecinout();
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            string dQMCode = FormatHelper.CleanString(this.txtMaterialNO.Text);
            Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterialByDQMCode(dQMCode);
            if (material == null)
            {
                specInOut.MCode = FormatHelper.CleanString(this.txtMaterialNO.Text, 40);
            }
            else
            {
                specInOut.MCode = material.MCode;
            }
            specInOut.DQMCode = FormatHelper.CleanString(this.txtMaterialNO.Text, 40);
            specInOut.MoveType = "I";
            specInOut.StorageCode = FormatHelper.CleanString(this.drpStorageInEdit.SelectedValue, 40);
            specInOut.LocationCode = FormatHelper.CleanString(this.drpLoationInEdit.SelectedValue, 40);
            specInOut.Qty = Convert.ToInt32(FormatHelper.CleanString(this.txtQTY.Text, 40));
            specInOut.MaintainUser = this.GetUserCode();
            specInOut.InOutDesc = txtSpecialDescEdit.Text;
            return specInOut;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtMaterialNO.Text = "";
                this.txtSpecialDescEdit.Text = "";
                this.txtMaterialENDesc.Text = "";
                this.txtMaterialCHDesc.Text = "";
                this.txtQTY.Text = "";
                this.txtUnitEdit.Text = "";
                return;
            }
        }

        protected override bool ValidateInput()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblMaterialNO, this.txtMaterialNO, 40, true));
            manager.Add(new LengthCheck(this.lblSpecialDescEdit, this.txtSpecialDescEdit, 200, true));
            manager.Add(new LengthCheck(this.lblStorageInEdit, this.drpStorageInEdit, 40, true));
            manager.Add(new LengthCheck(this.lblLoationInEdit, this.drpLoationInEdit, 40, true));
            manager.Add(new NumberCheck(this.lblQTY, this.txtQTY, true));

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
            return new string[]{((SpecInOutWithMaterial)obj).MCode,
                                ((SpecInOutWithMaterial)obj).DqmCode,
                                ((SpecInOutWithMaterial)obj).MenshortDesc,
                                ((SpecInOutWithMaterial)obj).MenlongDesc,
                                ((SpecInOutWithMaterial)obj).MchshortDesc,
                                ((SpecInOutWithMaterial)obj).MchlongDesc,
                                ((SpecInOutWithMaterial)obj).MspecialDesc,
                                ((SpecInOutWithMaterial)obj).StorageName,
                                ((SpecInOutWithMaterial)obj).LocationName,
                                ((SpecInOutWithMaterial)obj).Muom,
                                ((SpecInOutWithMaterial)obj).Qty.ToString(),
                                ((SpecInOutWithMaterial)obj).GetDisplayText("MaintainUser"),
                                FormatHelper.ToDateString(((SpecInOutWithMaterial)obj).MaintainDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"MaterialNo",
                                    "DQMCode",
                                    "ENSDesc",
                                    "ENLDesc",
                                    "CHSDesc",
                                    "CHLDesc",	
                                    "MESMaterialDesc",
                                    "StorageIn",	
                                    "LoationIn",
                                    "MUOM",	
                                    "QTY",
                                    "MaintainUser",	
                                    "MaintainDate"};
        }

        #endregion

    }
}
