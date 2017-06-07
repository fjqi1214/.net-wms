using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.IQC
{
    public partial class FShipToStockMP : BaseMPageMinus
    {


        private System.ComponentModel.IContainer components;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private ExcelExporter excelExporter;

        private IQCFacade _IQCFacade;

        #region Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

            // languageComponent1
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

            // excelExporter
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.InitOnPostBack();

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化界面UI
                this.InitUI();
                this.InitButtonHelp();
                this.SetEditObject(null);
                this.InitWebGrid();

                // 初始化控件
                this.BuildOrgList(this.drpOrgEdit);
                this.BuildOrgList(this.drpOrgQuery);
                this.chkActiveQuery.Checked = true;
            }

            _IQCFacade = new IQCFacadeFactory(base.DataProvider).CreateIQCFacade();

            this.SetButtonsForActive(this.chkActiveQuery.Checked);
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            object shipToStock = this.GetEditObject();

            if (shipToStock != null)
            {
                if (_IQCFacade.GetShipToStock(
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(((ShipToStock)shipToStock).MaterialCode)),
                    ((ShipToStock)shipToStock).OrganizationID,
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(((ShipToStock)shipToStock).VendorCode))) != null)
                {
                    WebInfoPublish.Publish(this, "$Error_ShipToStock_Exist", languageComponent1);
                    return;
                };

                this._IQCFacade.AddShipToStock((ShipToStock)shipToStock);

                this.SetButtonsForActive(true);
                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object shipToStock = this.GetEditObject(row);
                    if (shipToStock != null)
                    {
                        items.Add((ShipToStock)shipToStock);
                    }
                }

                this._IQCFacade.DeleteShipToStock((ShipToStock[])items.ToArray(typeof(ShipToStock)));

                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            object shipToStock = this.GetEditObject();

            if (shipToStock != null)
            {
                this._IQCFacade.UpdateShipToStock((ShipToStock)shipToStock);

                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.SetEditObject(obj);

                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Update)
            {
                this.txtDQMaterialNO.ReadOnly = true;
                this.drpOrgEdit.Enabled = false;
                this.txtVendorCodeEdit.Readonly = true;
            }
            if (pageAction == PageActionType.Save)
            {
                this.txtDQMaterialNO.ReadOnly = false;
                this.drpOrgEdit.Enabled = true;
                this.txtVendorCodeEdit.Readonly = false;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.txtDQMaterialNO.ReadOnly = false;
                this.drpOrgEdit.Enabled = true;
                this.txtVendorCodeEdit.Readonly = false;
            }
            if (pageAction == PageActionType.Add)
            {
                this.txtDQMaterialNO.ReadOnly = false;
                this.drpOrgEdit.Enabled = true;
                this.txtVendorCodeEdit.Readonly = false;
            }
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblDQMaterialNO, txtDQMaterialNO, 40, true));
            //  manager.Add(new LengthCheck(lblOrgEdit, drpOrgEdit, 2000, true));
            manager.Add(new LengthCheck(lblVendorCodeEdit, txtVendorCodeEdit, 40, true));
            manager.Add(new DateCheck(lblEffectDateEdit, datEffectDateEdit.Text, false));
            manager.Add(new DateCheck(lblInvalidDateEdit, datInvalidDateEdit.Text, false));
            manager.Add(new DateRangeCheck(this.lblEffectDateEdit, this.datEffectDateEdit.Text, this.lblInvalidDateEdit, this.datInvalidDateEdit.Text, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            //if (itemFacade.GetMaterial(
            //    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtMaterialCodeEdit.Text)),
            //    int.Parse(drpOrgEdit.SelectedValue)) == null)

            BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            if (_WarehouseFacade.GetMaterialInfoByQDMCode(
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtDQMaterialNO.Text)))==null)
            {
                WebInfoPublish.Publish(this, "$Error_Material_NotFound", languageComponent1);
                return false;
            }

            if (Convert.ToDateTime(this.datEffectDateEdit.Text) > Convert.ToDateTime(this.datInvalidDateEdit.Text))
            {
                WebInfoPublish.Publish(this, "$Error_EffectDateCannotBiggerThanInvalidDate", languageComponent1);
                return false;
            }

            return true;
        }

        private void SetButtonsForActive(bool active)
        {
            this.chkActiveQuery.Checked = active;
            this.gridWebGrid.Columns.FromKey("Edit").Hidden = !active;
            this.cmdDelete.Visible = active;
        }

        #endregion

        #region For Page_Load

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        public void InitButtonHelp()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
        }

        private void BuildOrgList(DropDownList dropDownListForOrg)
        {
            DropDownListBuilder builder = new DropDownListBuilder(dropDownListForOrg);
            builder.HandleGetObjectList = new GetObjectListDelegate((new BaseModelFacade(this.DataProvider)).GetAllOrg);
            builder.Build("OrganizationDescription", "OrganizationID");
            dropDownListForOrg.Items.Insert(0, new ListItem("", ""));

            dropDownListForOrg.SelectedIndex = 0;
        }

        private void BuildVendorCodeList(DropDownList dropDownListForVendorCode)
        {
            DropDownListBuilder builder = new DropDownListBuilder(dropDownListForVendorCode);
            builder.HandleGetObjectList = new GetObjectListDelegate((new ItemFacade(this.DataProvider)).GetAllVender);
            builder.Build("VendorCode", "VendorCode");
            dropDownListForVendorCode.Items.Insert(0, new ListItem("", ""));

            dropDownListForVendorCode.SelectedIndex = 0;
        }

        #endregion

        #region For Query Data

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(int.MinValue, int.MaxValue);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            int orgID = -1;
            if (this.drpOrgQuery.SelectedIndex > 0)
            {
                int.TryParse(this.drpOrgQuery.SelectedValue, out orgID);
            }

            return this._IQCFacade.QueryShipToStockEx(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDQMCodeQuery.Text)),
                orgID,
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeQuery.Text)),
                (this.chkActiveQuery.Checked ? "Y" : "N"),
                inclusive,
                exclusive);
        }

        private int GetRowCount()
        {
            int orgID = -1;
            if (this.drpOrgQuery.SelectedIndex > 0)
            {
                int.TryParse(this.drpOrgQuery.SelectedValue, out orgID);
            }

            return this._IQCFacade.QueryShipToStockCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDQMCodeQuery.Text)),
                orgID,
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeQuery.Text)),
                (this.chkActiveQuery.Checked ? "Y" : "N"));
        }

        #endregion

        #region For Grid And Edit

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("MaterialCode", "鼎桥物料代码", null);
            this.gridHelper.AddColumn("MaterialDesc", "物料描述", null);
            this.gridHelper.AddColumn("OrganizationID", "组织编号", null);
            this.gridHelper.AddColumn("OrganizationDesc", "组织", null);
            this.gridHelper.AddColumn("VendorCode", "供应商代码", null);
            this.gridHelper.AddColumn("VendorName", "供应商名称", null);
            this.gridHelper.AddColumn("EffectDate", "生效日期", null);
            this.gridHelper.AddColumn("InvalidDate", "失效日期", null);
            this.gridHelper.AddColumn("Active", "有效", null);
            this.gridHelper.AddColumn("MaintainUser", "维护人员", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridWebGrid.Columns.FromKey("OrganizationID").Hidden = true;
            this.gridWebGrid.Columns.FromKey("OrganizationDesc").Hidden = true;
            this.gridWebGrid.Columns.FromKey("VendorCode").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Active").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["MaterialCode"] = ((ShipToStockEx)obj).DQMCode;
            row["MaterialDesc"] = ((ShipToStockEx)obj).MaterialDesc;
            row["OrganizationID"] = ((ShipToStockEx)obj).OrganizationID.ToString();
            row["OrganizationDesc"] = ((ShipToStockEx)obj).OrganizationDesc;
            row["VendorCode"] = ((ShipToStockEx)obj).VendorCode;
            row["VendorName"] = ((ShipToStockEx)obj).VendorName;
            row["EffectDate"] = FormatHelper.ToDateString(((ShipToStockEx)obj).EffectDate);
            row["InvalidDate"] = FormatHelper.ToDateString(((ShipToStockEx)obj).InvalidDate);
            row["Active"] = ((ShipToStockEx)obj).Active;
            row["MaintainUser"] = ((ShipToStockEx)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((ShipToStockEx)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((ShipToStockEx)obj).MaintainTime);
            return row;
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                ShipToStock shipToStock = this._IQCFacade.CreateNewShipToStock();


                BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
                // shipToStock.OrganizationID = int.Parse(this.drpOrgEdit.SelectedValue);
                shipToStock.OrganizationID = 1;
                shipToStock.VendorCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVendorCodeEdit.Text));
                shipToStock.EffectDate = FormatHelper.TODateInt(this.datEffectDateEdit.Text);
                shipToStock.InvalidDate = FormatHelper.TODateInt(this.datInvalidDateEdit.Text);
                shipToStock.Active = "Y";
                shipToStock.MaintainUser = this.GetUserCode();
                shipToStock.DQMCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDQMaterialNO.Text, 40));
               
               BenQGuru.eMES.Domain.MOModel.Material m= _WarehouseFacade.GetMaterialFromDQMCode(shipToStock.DQMCode);
               if (m != null)
                   shipToStock.MaterialCode = m.MCode;


                return shipToStock;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {
            object obj = this._IQCFacade.GetShipToStock(row.Items.FindItemByKey("MaterialCode").Text, int.Parse(row.Items.FindItemByKey("OrganizationID").Text), row.Items.FindItemByKey("VendorCode").Text);

            if (obj != null)
            {
                return (ShipToStock)obj;
            }

            return null;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtDQMaterialNO.Text = string.Empty;
                //  this.drpOrgEdit.SelectedIndex = 0;
                this.txtVendorCodeEdit.Text = string.Empty;
                this.datEffectDateEdit.Text = System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                this.datInvalidDateEdit.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

                return;
            }

            this.txtDQMaterialNO.Text = ((ShipToStock)obj).DQMCode.ToString();
            this.datEffectDateEdit.Text = FormatHelper.ToDateString(((ShipToStock)obj).EffectDate);
            this.datInvalidDateEdit.Text = FormatHelper.ToDateString(((ShipToStock)obj).InvalidDate);
            this.txtVendorCodeEdit.Text = ((ShipToStock)obj).VendorCode;

            try
            {
                this.drpOrgEdit.SelectedValue = ((ShipToStock)obj).OrganizationID.ToString();
            }
            catch
            {
                this.drpOrgEdit.SelectedIndex = 0;
            }
        }

        #endregion

        #region For Export To Excel

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                ((ShipToStockEx)obj).DQMCode,
                ((ShipToStockEx)obj).MaterialDesc,
             //   ((ShipToStockEx)obj).OrganizationDesc,
                ((ShipToStockEx)obj).VendorName,
                FormatHelper.ToDateString(((ShipToStockEx)obj).EffectDate),
                FormatHelper.ToDateString(((ShipToStockEx)obj).InvalidDate),
                ((ShipToStockEx)obj).Active,
                //((ShipToStockEx)obj).MaintainUser,
                ((ShipToStockEx)obj).GetDisplayText("MaintainUser"),
                FormatHelper.ToDateString(((ShipToStockEx)obj).MaintainDate),
                FormatHelper.ToTimeString(((ShipToStockEx)obj).MaintainTime)
            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "DQMCode",
                "MaterialDesc",
              //  "OrganizationDesc",
                "VendorName",	
                "EffectDate",
                "InvalidDate",
                "Active",
                "MaintainUser",
                "MaintainDate",
                "MaintainTime"
            };
        }

        #endregion
    }
}
