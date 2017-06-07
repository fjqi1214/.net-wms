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
using BenQGuru.eMES.MOModel;



namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FSpecOutMP : BaseMPageNew
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

        //初始化出库库位下拉框
        /// <summary>
        /// 初始化出库库位
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
                    this.drpStorageOutEdit.Items.Add(new ListItem(storage.StorageName, storage.StorageCode));
                }
                this.drpStorageOutEdit.SelectedIndex = 0;
            }

        }


        //初始化出库货位下拉框
        /// <summary>
        /// 初始化出库货位
        /// </summary>
        private void InitLoationInList()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            string storageCode = this.drpStorageOutEdit.SelectedValue;
            if (!string.IsNullOrEmpty(storageCode))
            {
                object[] objLocation = _InventoryFacade.GetLocation("MES", storageCode);
                this.drpLoationOutEdit.Items.Clear();
                if (objLocation != null && objLocation.Length > 0)
                {
                    foreach (Location location in objLocation)
                    {
                        this.drpLoationOutEdit.Items.Add(new ListItem(location.LocationName, location.LocationCode));
                    }
                    this.drpLoationOutEdit.SelectedIndex = 0;
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
            this.txtSpecialDescEdit.Enabled = b;
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
            this.gridHelper.AddColumn("SpecialDesc", "特殊物料描述", null);
            this.gridHelper.AddColumn("StorageOut", "出库库位", null);
            this.gridHelper.AddColumn("LoationOut", "出库货位", null);
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
            row["SpecialDesc"] = ((SpecInOutWithMaterial)obj).InOutDesc;
            row["StorageOut"] = ((SpecInOutWithMaterial)obj).StorageName;
            row["LoationOut"] = ((SpecInOutWithMaterial)obj).LocationName;
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
                "O",//出库状态
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
                "O"//出库状态
                );
        }

        #endregion

        #region Button

        //物料编码回车
        //protected void btnMaterialNOEnter_Click(object sender, EventArgs e)
        //{
        //    ItemFacade itemFacade = new ItemFacade(this.DataProvider);
        //    string dQMCode = FormatHelper.CleanString(this.txtMaterialNO.Text);
        //    this.txtSpecialDescEdit.Text = "";
        //    this.txtMaterialENDesc.Text = "";
        //    this.txtMaterialCHDesc.Text = "";
        //    this.txtUnitEdit.Text = "";
        //    if (string.IsNullOrEmpty(dQMCode))
        //    {
        //        WebInfoPublish.Publish(this, "物料编码为空", this.languageComponent1);
        //        this.txtMaterialNO.Focus();
        //        return;
        //    }
        //    Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterialByDQMCode(dQMCode);
        //    if (material == null)
        //    {
        //        WebInfoPublish.Publish(this, "物料主数据没有该物料编码： " + dQMCode, this.languageComponent1);
        //        this.txtMaterialNO.Focus();
        //        return;
        //    }
        //    this.txtSpecialDescEdit.Text = material.MspecialDesc;
        //    this.txtMaterialENDesc.Text = material.MenshortDesc;
        //    this.txtMaterialCHDesc.Text = material.MchshortDesc;
        //    this.txtUnitEdit.Text = material.Muom;

        //}

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
            if (string.IsNullOrEmpty(drpStorageOutEdit.SelectedValue))
            {
                WebInfoPublish.Publish(this, "库位编码不能为空！", this.languageComponent1);
                return;
            }
            if (string.IsNullOrEmpty(drpLoationOutEdit.SelectedValue))
            {
                WebInfoPublish.Publish(this, "货位编码不能为空！", this.languageComponent1);
                return;
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);
            SpecInOut inOut = _InventoryFacade.QuerySpecInOuts(dQMCode, drpStorageOutEdit.SelectedValue, drpLoationOutEdit.SelectedValue);

            Domain.MOModel.Material material = (Domain.MOModel.Material)_InventoryFacade.GetMaterialByDQMCode(dQMCode);
            if (material == null)
            {
                WebInfoPublish.Publish(this, "物料主数据没有该物料编码！ " + dQMCode, this.languageComponent1);
                this.txtMaterialNO.Focus();
                return;
            }
            if (inOut == null)
            {
                WebInfoPublish.Publish(this, "没有对应的入库记录！", this.languageComponent1);
                this.txtMaterialNO.Focus();

            }
            this.txtSpecialDescEdit.Text = inOut.InOutDesc;
            this.txtMaterialENDesc.Text = material.MenshortDesc;
            this.txtMaterialCHDesc.Text = material.MchshortDesc;
            this.txtUnitEdit.Text = material.Muom;

        }

        //新增
        protected override void AddDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            //扣减库存
            string mCode = ((SpecInOut)domainObject).MCode;

            try
            {
                this.DataProvider.BeginTransaction();
                int qty = ((SpecInOut)domainObject).Qty;//需要扣减的库存
                SpecStorageInfo s = (SpecStorageInfo)_InventoryFacade.GetSpecStorageInfo(drpStorageOutEdit.SelectedValue, mCode, drpLoationOutEdit.SelectedValue);
                if (qty > s.StorageQty)
                {
                    WebInfoPublish.Publish(this, "输入数量大于库存数量", this.languageComponent1);//add by sam
                    return;
                }
                if (qty == s.StorageQty)
                    _InventoryFacade.DeleteSpecStorageInfo(s);
                else
                {
                    s.StorageQty = s.StorageQty - qty;
                    this._InventoryFacade.UpdateSpecStorageInfo(s);

                }
                this._InventoryFacade.AddSpecInOut((SpecInOut)domainObject);
                this.DataProvider.CommitTransaction();

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;

            }

        }

        //库位下拉框索引改变
        protected void drpStorageOutEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            string storageCode = this.drpStorageOutEdit.SelectedValue;
            object[] objLocation = _InventoryFacade.GetLocation("MES", storageCode);
            this.drpLoationOutEdit.Items.Clear();
            if (objLocation != null && objLocation.Length > 0)
            {
                foreach (Location location in objLocation)
                {
                    this.drpLoationOutEdit.Items.Add(new ListItem(location.LocationName, location.LocationCode));
                }
                this.drpLoationOutEdit.SelectedIndex = 0;
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
            specInOut.InOutDesc = txtSpecialDescEdit.Text;

            specInOut.DQMCode = FormatHelper.CleanString(this.txtMaterialNO.Text, 40);
            specInOut.Muom = FormatHelper.CleanString(this.txtUnitEdit.Text, 40);
            specInOut.MoveType = "O";
            specInOut.StorageCode = FormatHelper.CleanString(this.drpStorageOutEdit.SelectedValue, 40);
            specInOut.LocationCode = FormatHelper.CleanString(this.drpLoationOutEdit.SelectedValue, 40);
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
            manager.Add(new LengthCheck(this.lblStorageOutEdit, this.drpStorageOutEdit, 40, true));
            manager.Add(new LengthCheck(this.lblLoationOutEdit, this.drpLoationOutEdit, 40, true));
            manager.Add(new NumberCheck(this.lblQTY, this.txtQTY, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            //检查：库存数量>=出库数量
            string mCode = FormatHelper.CleanString(this.txtMaterialNO.Text);
            int qty = Convert.ToInt32(this.txtQTY.Text.Trim());//出库数量

            object[] objSpecStorageInfo = _InventoryFacade.GetSpecStorageInfo(mCode);
            if (objSpecStorageInfo != null)
            {
                bool isAllZero = true;
                foreach (SpecStorageInfo specStorageInfo in objSpecStorageInfo)
                {
                    if (specStorageInfo.StorageQty != 0)
                    {
                        isAllZero = false;
                        break;
                    }
                }
                if (isAllZero)
                {
                    WebInfoPublish.Publish(this, "物料没有库存", this.languageComponent1);
                    return false;
                }
                int storageQty = _InventoryFacade.GetSpecStorageInfoQty(mCode);
                if (storageQty < qty)
                {
                    WebInfoPublish.Publish(this, "库存数量少于出库数量", this.languageComponent1);
                    return false;
                }
            }
            else
            {
                WebInfoPublish.Publish(this, "物料没有库存", this.languageComponent1);
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
                                    "SpecialDesc",
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
