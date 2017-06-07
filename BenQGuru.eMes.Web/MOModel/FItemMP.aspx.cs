#region system
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

#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// ItemMP 的摘要说明。
    /// </summary>
    public partial class FItemMP : BaseMPageMinus
    {
        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //private GridHelperNew gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private ItemFacade _itemFacade;// = FacadeFactory.CreateItemFacade();
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

        private BenQGuru.eMES.MOModel.ModelFacade _modelFacade;//= new FacadeFactory(base.DataProvider).CreateModelFacade();


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
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";

        }

        #endregion

        #region form events
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitOnPostBack();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化界面UI
                this.InitUI();
                InitButtonHelp();
                SetEditObject(null);
                this.InitWebGrid();
                BuildOrgList();
                txtPcbAcountEdit.Text = "1";
            }

        }
        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        //private void gridWebGrid_DblClick(object sender, Infragistics.WebUI.UltraWebGrid.ClickEventArgs e)
        //{
        //    object obj = this.GetEditObject(e.Row);

        //    if (obj != null)
        //    {
        //        this.SetEditObject(obj);

        //        this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
        //    }
        //}

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            PageCheckManager checkManager = new PageCheckManager();
            checkManager.Add(new LengthCheck(lblItemModelCodeEdit, drpModelEdit, Int32.MaxValue, true));
            checkManager.Add(new LengthCheck(lblOrgEdit, DropDownListOrg, 8, true));
            checkManager.Add(new NumberCheck(this.lblPcbAcountEdit, this.txtPcbAcountEdit, 0, int.MaxValue, false));

            if (!checkManager.Check())
            {
                WebInfoPublish.Publish(this, checkManager.CheckMessage, this.languageComponent1);
                return;
            }

            if (this.txtOPCodeEdit.Text.Trim().Length > 0)
            {
                WebInfoPublish.Publish(this, "$Error_NewItemNeedNotOP", this.languageComponent1);
                return;
            }

            object item = this.GetEditObject();
            if (item != null)
            {
                if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
                this._itemFacade.AddItem((Item)item, this.drpModelEdit.SelectedValue.ToUpper());

                object objMaterial = this._itemFacade.GetMaterial(((Item)item).ItemCode.Trim().ToUpper(), Convert.ToInt32(this.DropDownListOrg.SelectedValue.Trim()));
                if (objMaterial == null)
                {
                    Domain.MOModel.Material material = (Domain.MOModel.Material)(this.GetMaterialObject(((Item)item)));
                    this._itemFacade.AddMaterial(material);
                }
                else
                {
                    Domain.MOModel.Material material = (Domain.MOModel.Material)objMaterial;
                    material.MaterialType = ((Item)item).ItemType;
                    this._itemFacade.UpdateMaterial(material);
                }
                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
                this.drpModelEdit.SelectedIndex = 0;
            }
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {

                ArrayList items = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object item = this.GetEditObject(row);
                    if (item != null)
                    {
                        items.Add((Item)item);
                    }
                }

                this._itemFacade.DeleteItem((Item[])items.ToArray(typeof(Item)));

                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
                this.drpModelEdit.SelectedIndex = 0;
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            PageCheckManager checkManager = new PageCheckManager();
            checkManager.Add(new LengthCheck(lblItemModelCodeEdit, drpModelEdit, Int32.MaxValue, true));
            checkManager.Add(new LengthCheck(lblOrgEdit, DropDownListOrg, 8, true));

            //修改可以输入0  Terry 2010-10-29
            if (txtOPCodeEdit.Text.Trim() == string.Empty)
            {
                checkManager.Add(new NumberCheck(this.lblLotSizeEdit, this.txtLotSizeEdit, 0, int.MaxValue, true));
            }
            else
            {
                checkManager.Add(new NumberCheck(this.lblLotSizeEdit, this.txtLotSizeEdit, 1, int.MaxValue, true));
            }


            //可以为空 Terry 2010-10-29
            checkManager.Add(new LengthCheck(this.lblCheckOPCodeEdit, this.txtOPCodeEdit, 40, false));
            checkManager.Add(new LengthCheck(this.lblItemProductCodeEdit, this.TextboxItemProductCodeEdit, 100, false));
            checkManager.Add(new NumberCheck(this.lblPcbAcountEdit, this.txtPcbAcountEdit, 1, int.MaxValue, true));

            if (!checkManager.Check())
            {
                WebInfoPublish.Publish(this, checkManager.CheckMessage, this.languageComponent1);
                return;
            }

            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            object item = this.GetEditObject();

            if (item != null)
            {
                //Added by hi1/venus.feng on 20080717 for Hisense Version
                //Check OP
                //Modify by Terry 2010-10-29 (not check op)
                if (this.txtOPCodeEdit.Text.Trim() != string.Empty)
                {
                    BaseModelFacade baseModel = new BaseModelFacade(this.DataProvider);
                    object op = baseModel.GetOperation(FormatHelper.PKCapitalFormat(this.txtOPCodeEdit.Text.Trim()));

                    if (op == null)
                    {
                        throw new Exception("$Error_CS_Current_OP_Not_Exist");
                    }

                    //Check OP is in ItemRoute2OP
                    ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                    if (itemFacade.IsOPInItemOP(((Item)item).ItemCode, ((Item)item).OrganizationID, ((Item)item).CheckItemOP) == false)
                    {
                        throw new Exception("$Error_OPIsNotInItemOPList");
                    }
                }

                //Check Item MO in processing

                MOFacade moFacade = new MOFacade(this.DataProvider);
                if (moFacade.CheckItemCodeUsed(((Item)item).ItemCode) == true)
                {
                    throw new Exception("$ERROR_ITEM_USE");
                }
                // End added

                this._itemFacade.UpdateItem((Item)item, this.drpModelEdit.SelectedValue.ToUpper());
                this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
                this.drpModelEdit.SelectedIndex = 0;
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
            this.drpModelEdit.SelectedIndex = 0;
            setDropDownListEnabled(true);
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
            else if (commandName == "OQCList")
            {
                Response.Redirect(this.MakeRedirectUrl("../OQC/FItem2CheckListSP.aspx", new string[] { "itemcode" }, new string[] { row.Items.FindItemByKey("ItemCode").Text.Trim() }));
            }

            else if (commandName == "KepartsQty")
            {
                Response.Redirect(this.MakeRedirectUrl("FItemLocationMP.aspx", new string[] { "itemcode" }, new string[] { row.Items.FindItemByKey("ItemCode").Text.Trim() }));
            }

            /* 注释，jessie lee， 2006/8/15 */
            //			else if(e.Cell.Column.Key =="SPCM")
            //			{
            //				Response.Redirect(this.MakeRedirectUrl ("FItem2SPCTestItemMP.aspx",new string[] {"itemcode","itemname"},new string[] {e.Cell.Row.Cells.FromKey("ItemCode").Text.Trim(),e.Cell.Row.Cells.FromKey("ItemName").Text.Trim()}));
            //			}

            else if (commandName == "ConfigM")
            {
                if (row.Items.FindItemByKey("ItemConfig").Text.Trim().Length == 0) return;
                string itemcode = row.Items.FindItemByKey("ItemCode").Text.Trim();
                string itemconfig = row.Items.FindItemByKey("ItemConfig").Text.Trim();
                string orgID = row.Items.FindItemByKey("OrganizationID").Text.Trim();

                Response.Redirect(this.MakeRedirectUrl("FItem2Config.aspx", new string[] { "ITEMCODE", "ItemConfig", "OrgID" }, new string[] { itemcode, itemconfig, orgID }));
            }

            else if (commandName == "OQCFuncTest")	// Added by Icyer 2006/06/08
            {
                Response.Redirect(this.MakeRedirectUrl("../OQC/FOQCFuncTestMP.aspx", new string[] { "itemcode" }, new string[] { row.Items.FindItemByKey("ItemCode").Text.Trim() }));
            }
            //			else if(e.Cell.Column.Key =="Dimention")
            //			{
            //				Response.Redirect(this.MakeRedirectUrl ("FItem2DimentionMP.aspx",new string[] {"itemcode"},new string[] {e.Cell.Row.Cells.FromKey("ItemCode").Text.Trim()}));
            //			}
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }


        #endregion

        #region private method
        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            // 2005-04-06
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

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Update)
            {
                this.txtItemCodeEdit.ReadOnly = true;
                this.DropDownListOrg.Enabled = false;
            }
            if (pageAction == PageActionType.Save)
            {
                this.txtItemCodeEdit.ReadOnly = false;
                this.DropDownListOrg.Enabled = true;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.txtItemCodeEdit.ReadOnly = false;
                this.DropDownListOrg.Enabled = true;
            }
            if (pageAction == PageActionType.Add)
            {
                this.txtItemCodeEdit.ReadOnly = false;
                this.DropDownListOrg.Enabled = true;
            }
        }

        private void InitWebGrid()
        {
            //update by crystal chu 2005/04/26
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ItemName", "产品名称", null);
            this.gridHelper.AddColumn("ItemType", "产品类别", null);
            this.gridHelper.AddColumn("UOM", "计量单位", null);
            this.gridHelper.AddColumn("ItemDescription", "产品描述", null);
            this.gridHelper.AddColumn("ModelCode2", "所属产品别", null);
            this.gridHelper.AddColumn("ItemConfig", "配置码", null);
            this.gridHelper.AddColumn("BurnInOutVolumn", "装车数量", null);
            this.gridHelper.AddColumn("CartonQty", "装箱数量", null);

            this.gridHelper.AddColumn("OPCodeForLot", "产生送检批工序", null);
            this.gridHelper.AddColumn("LotSize", "批量", null);

            this.gridHelper.AddColumn("ElectricCurrentMinValue", "最小电流值", null);
            this.gridHelper.AddColumn("ElectricCurrentMaxValue", "最大电流值", null);
            this.gridHelper.AddColumn("ItemProdcutCode", "商品码", null);
            this.gridHelper.AddColumn("NeedCheckCarton", "比对箱号", null);
            this.gridHelper.AddColumn("CompareAccessory", "比对附件袋", null);
            this.gridHelper.AddColumn("BurnUseMinutes", "老化预计耗时", null);

            this.gridHelper.AddColumn("MUSER", "维护人员", null);
            this.gridHelper.AddColumn("MDATE", "维护日期", null);

            //Add by Terry 2010-10-29
            this.gridHelper.AddColumn("PCBACOUNT", "PCBA拼版数量", null);

            this.gridHelper.AddLinkColumn("ConfigM", "配置信息维护", null);
            this.gridHelper.AddLinkColumn("KepartsQty", "产品元件件数", null);
            //this.gridHelper.AddLinkColumn("SPCM","SPC维护",null);
            this.gridHelper.AddLinkColumn("OQCList", "检验清单", null);

            this.gridHelper.AddLinkColumn("OQCFuncTest", "功能测试标准", null);	// Added by Icyer 2006/06/08
            //this.gridHelper.AddLinkColumn("Dimention","尺寸量测标准",null);

            this.gridHelper.AddColumn("OrganizationID", "组织编号", null);
            this.gridWebGrid.Columns.FromKey("OrganizationID").Hidden = true;

            //Added By Hi1/Venus.Feng on 20080707 for Hisense:mark these columns
            this.gridWebGrid.Columns.FromKey("ItemConfig").Hidden = true;
            this.gridWebGrid.Columns.FromKey("BurnInOutVolumn").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ElectricCurrentMinValue").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ElectricCurrentMaxValue").Hidden = true;
            this.gridWebGrid.Columns.FromKey("OQCFuncTest").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ConfigM").Hidden = true;
            this.gridWebGrid.Columns.FromKey("OQCList").Hidden = true;
            this.gridWebGrid.Columns.FromKey("KepartsQty").Hidden = true;
            this.gridWebGrid.Columns.FromKey("OPCodeForLot").Hidden = true;
            //End Added

            // 2005-04-06
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private object GetMaterialObject(Item item)
        {
            Domain.MOModel.Material material = this._itemFacade.CreateNewMaterial();
            material.MaterialCode = ((Item)item).ItemCode;
            material.MaterialName = ((Item)item).ItemName;
            material.MaterialDescription = ((Item)item).ItemDescription;
            material.MaterialUOM = ((Item)item).ItemUOM;
            material.MaterialType = ((Item)item).ItemType;
            material.MaterialMachineType = " ";
            material.MaterialVolume = " ";
            material.MaterialModelCode = " ";
            material.MaterialExportImport = " ";
            material.MaterialModelGroup = " ";
            material.MaterialGroup = " ";
            material.MaterialGroupDescription = " ";
            material.MaterialControlType = " ";
            material.MaintainUser = this.GetUserCode();
            //material.MaintainDate = ;
            //material.MaintainTime =;
            material.EAttribute1 = " ";
            material.OrganizationID = ((Item)item).OrganizationID;
            material.MaterialParseType = "parse_prepare";    
            material.CheckStatus = " ";
            material.MaterialCheckType = "check_linkbarcode";
            material.SerialNoLength = 0;
            material.VendorCode = " ";
            material.ROHS = "N";
            material.NeedVendor = "N";
            material.MShelfLife = 0;

            return material;
        }
        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }

                Item item = this._itemFacade.CreateNewItem();

                item.ItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeEdit.Text, 40));
                item.ItemDescription = FormatHelper.CleanString(this.txtItemDescEdit.Text, 100);
                item.ItemType = FormatHelper.CleanString(this.drpItemTypeEdit.SelectedValue, 40);
                item.ItemName = FormatHelper.CleanString(this.txtItemNameEdit.Text, 100);
                item.ItemUOM = FormatHelper.CleanString(this.txtItemUOMEdit.Text, 40);
                item.ItemDate = FormatHelper.TODateInt(DateTime.Today.ToShortDateString());
                item.ItemUser = FormatHelper.CleanString(this.GetUserCode());
                item.ItemConfigration = FormatHelper.CleanString(this.txtConfig.Text, 40);
                //item.ItemBurnInQty = Convert.ToInt32( FormatHelper.CleanString(this.txtVolumnEdit.Text, 10) ) ;
                item.ItemCartonQty = Convert.ToInt32(FormatHelper.CleanString(this.txtCartonQty.Text, 10));
                item.MaintainUser = this.GetUserCode();
                item.ElectricCurrentMaxValue = Convert.ToDecimal(FormatHelper.CleanString(this.txtMaxElectricCurrent.Text, 18));
                item.ElectricCurrentMinValue = Convert.ToDecimal(FormatHelper.CleanString(this.txtMinElectricCurrent.Text, 18));
                item.CheckItemOP = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeEdit.Text, 40));
                item.LotSize = Convert.ToInt32(this.txtLotSizeEdit.Text.Trim());

                item.ItemProductCode = FormatHelper.CleanString(this.TextboxItemProductCodeEdit.Text, 100);
                item.NeedCheckCarton = FormatHelper.BooleanToString(this.CheckboxNeedCheckCartonEdit.Checked);
                item.NeedCheckAccessory = FormatHelper.BooleanToString(this.CheckboxNeedCheckComApp.Checked);
                item.OrganizationID = int.Parse(this.DropDownListOrg.SelectedValue);
                item.PcbaCount = Convert.ToInt32(this.txtPcbAcountEdit.Text.Trim());
                item.BurnUseMinutes = Convert.ToInt32(this.txtBurnUseMinutesEdit.Text.Trim());  //Add by sandy on 20140530

                return item;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(GridRecord row)
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            object obj = this._itemFacade.GetItem(row.Items.FindItemByKey("ItemCode").Text.ToString(), int.Parse(row.Items.FindItemByKey("OrganizationID").Text));

            if (obj != null)
            {
                return (Item)obj;
            }

            return null;
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblItemCodeEdit, txtItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblItemUOMEdit, txtItemUOMEdit, 40, true));
            manager.Add(new LengthCheck(lblItemTypeEdit, drpItemTypeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblItemModelCodeEdit, this.drpModelEdit, 40, true));
            //manager.Add( new NumberCheck (this.lblVolumnNumEdit, this.txtVolumnEdit, 0, int.MaxValue, false) );
            manager.Add(new NumberCheck(this.lblItemCartonQty, this.txtCartonQty, 0, int.MaxValue, false));
            manager.Add(new NumberCheck(this.lblLotSizeEdit, this.txtLotSizeEdit, 0, int.MaxValue, false));
            manager.Add(new LengthCheck(this.lblOrgEdit, this.DropDownListOrg, 8, true));
            manager.Add(new LengthCheck(this.lblItemProductCodeEdit, this.TextboxItemProductCodeEdit, 100, false));
            manager.Add(new NumberCheck(this.lblBurnUseMinutesEdit, this.txtBurnUseMinutesEdit, 0, int.MaxValue, false));

            //manager.Add( new DecimalCheck(this.lblMaxElectricCurrent, this.txtMaxElectricCurrent, decimal.MinValue, decimal.MaxValue, false) );
            //manager.Add( new DecimalCheck (this.lblMinElectricCurrent, this.txtMinElectricCurrent, decimal.MinValue, decimal.MaxValue, false) );

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }

        private void SetEditObject(object obj)
        {
            if (_modelFacade == null) { _modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade(); }
            if (obj == null)
            {
                this.txtItemCodeEdit.Text = string.Empty;
                this.txtItemDescEdit.Text = string.Empty;
                this.drpItemTypeEdit.SelectedIndex = 0;
                this.txtItemNameEdit.Text = string.Empty;
                //				this.txtItemVersionEdit.Text=string.Empty;
                this.txtItemUOMEdit.Text = string.Empty;
                this.txtConfig.Text = string.Empty;
                this.txtVolumnEdit.Text = String.Empty;
                this.txtCartonQty.Text = String.Empty;
                this.txtMaxElectricCurrent.Text = "0";
                this.txtMinElectricCurrent.Text = "0";
                txtVolumnEdit.Text = "0";
                this.DropDownListOrg.SelectedIndex = 0;
                this.txtOPCodeEdit.Text = "";
                this.txtLotSizeEdit.Text = "";
                this.txtPcbAcountEdit.Text = "";
                this.TextboxItemProductCodeEdit.Text = "";
                this.CheckboxNeedCheckCartonEdit.Checked = false;
                this.CheckboxNeedCheckComApp.Checked = false;
                txtPcbAcountEdit.Text = "1";
                this.txtBurnUseMinutesEdit.Text = "0";
                return;
            }

            this.txtItemCodeEdit.Text = ((Item)obj).ItemCode.ToString();
            this.txtItemDescEdit.Text = ((Item)obj).ItemDescription.ToString();
            try
            {
                this.drpItemTypeEdit.SelectedValue = ((Item)obj).ItemType.ToString();
            }
            catch
            {
                this.drpItemTypeEdit.SelectedIndex = 0;
            }
            this.txtItemNameEdit.Text = ((Item)obj).ItemName.ToString();
            //			this.txtItemVersionEdit.Text	= ((Item)obj).ItemVersion.ToString();
            this.txtItemUOMEdit.Text = ((Item)obj).ItemUOM.ToString();
            this.txtVolumnEdit.Text = ((Item)obj).ItemBurnInQty.ToString("##.##");
            this.txtCartonQty.Text = ((Item)obj).ItemCartonQty.ToString("##.##");
            this.txtConfig.Text = ((Item)obj).ItemConfigration;

            this.TextboxItemProductCodeEdit.Text = ((Item)obj).ItemProductCode;
            this.CheckboxNeedCheckCartonEdit.Checked = (((Item)obj).NeedCheckCarton == FormatHelper.TRUE_STRING);
            this.CheckboxNeedCheckComApp.Checked = (((Item)obj).NeedCheckAccessory == FormatHelper.TRUE_STRING);

            this.txtMaxElectricCurrent.Text = ((Item)obj).ElectricCurrentMaxValue.ToString();
            this.txtMinElectricCurrent.Text = ((Item)obj).ElectricCurrentMinValue.ToString();
            //Add by Terry  2010-10-29
            this.txtPcbAcountEdit.Text = ((Item)obj).PcbaCount.ToString();
            try
            {
                this.DropDownListOrg.SelectedValue = ((Item)obj).OrganizationID.ToString();
            }
            catch
            {
                this.DropDownListOrg.SelectedIndex = 0;
            }

            object[] oldItems = this._modelFacade.GetModel2ItemByItemCode(this.txtItemCodeEdit.Text);
            if (oldItems != null && oldItems.Length > 0)
                this.drpModelEdit.SelectedValue = ((Model2Item)oldItems[0]).ModelCode;
            MOFacade moFacade = new MOFacade(base.DataProvider);
            if (moFacade.CheckItemCodeUsed(((Item)obj).ItemCode))
            {
                setDropDownListEnabled(false);
            }
            else
            {
                setDropDownListEnabled(true);
            }
            this.txtOPCodeEdit.Text = ((Item)obj).CheckItemOP;
            this.txtLotSizeEdit.Text = ((Item)obj).LotSize.ToString();
            this.txtBurnUseMinutesEdit.Text = ((Item)obj).BurnUseMinutes.ToString();
        }

        private void setDropDownListEnabled(bool enabled)
        {
            this.drpItemTypeEdit.Enabled = enabled;
            this.drpModelEdit.Enabled = enabled;
        }

        //Modified by Terry 2010-10-29 (add PcbaCount)
        protected DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ItemCode"] = ((Item)obj).ItemCode;
            row["ItemName"] = ((Item)obj).ItemName;
            row["ItemType"] = this.languageComponent1.GetString(((Item)obj).ItemType);
            row["UOM"] = ((Item)obj).ItemUOM;
            row["ItemDescription"] = ((Item)obj).ItemDescription;
            row["ModelCode2"] = ((ItemOfModel)obj).ModelCode;
            row["ItemConfig"] = ((Item)obj).ItemConfigration;
            row["BurnInOutVolumn"] = ((Item)obj).ItemBurnInQty.ToString("##.##");
            row["CartonQty"] = ((Item)obj).ItemCartonQty.ToString("##.##");
            row["OPCodeForLot"] = ((Item)obj).GetDisplayText("CheckItemOP");
            row["LotSize"] = ((Item)obj).LotSize.ToString();
            row["ElectricCurrentMinValue"] = ((Item)obj).ElectricCurrentMinValue.ToString();
            row["ElectricCurrentMaxValue"] = ((Item)obj).ElectricCurrentMaxValue.ToString();
            row["ItemProdcutCode"] = ((Item)obj).ItemProductCode;
            row["NeedCheckCarton"] = FormatHelper.DisplayBoolean(((Item)obj).NeedCheckCarton, this.languageComponent1);
            row["CompareAccessory"] = FormatHelper.DisplayBoolean(((Item)obj).NeedCheckAccessory, this.languageComponent1);

            row["MUSER"] = ((Item)obj).GetDisplayText("MaintainUser");
            row["MDATE"] = FormatHelper.ToDateString(((Item)obj).MaintainDate);
            row["PCBACOUNT"] = ((Item)obj).PcbaCount.ToString();
            row["ConfigM"] = string.Empty;
            row["KepartsQty"] = string.Empty;
            row["OQCList"] = string.Empty;
            row["OQCFuncTest"] = string.Empty;
            row["OrganizationID"] = ((Item)obj).OrganizationID.ToString();
            row["BurnUseMinutes"] = ((Item)obj).BurnUseMinutes.ToString();
            return row;

            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //        "false",
            //        ((Item)obj).ItemCode ,
            //        ((Item)obj).ItemName ,
            //        this.languageComponent1.GetString( ((Item)obj).ItemType ),
            //        ((Item)obj).ItemUOM ,
            //        ((Item)obj).ItemDescription ,
            //        ((ItemOfModel)obj).ModelCode ,
            //        ((Item)obj).ItemConfigration ,
            //        ((Item)obj).ItemBurnInQty.ToString("##.##"),
            //        ((Item)obj).ItemCartonQty.ToString("##.##"),
            //        ((Item)obj).GetDisplayText("CheckItemOP"),
            //        ((Item)obj).LotSize.ToString(),
            //        ((Item)obj).ElectricCurrentMinValue.ToString(),
            //        ((Item)obj).ElectricCurrentMaxValue.ToString(),
            //        ((Item)obj).ItemProductCode,
            //        FormatHelper.DisplayBoolean(((Item)obj).NeedCheckCarton, this.languageComponent1),
            //        FormatHelper.DisplayBoolean(((Item)obj).NeedCheckAccessory, this.languageComponent1),
            //        ((Item)obj).GetDisplayText("MaintainUser"),
            //        FormatHelper.ToDateString(((Item)obj).MaintainDate),
            //        ((Item)obj).PcbaCount.ToString(),
            //        "",
            //        "",
            //        "",
            //        "",
            //        ((Item)obj).OrganizationID.ToString(),
            //        ""
            //    });
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            //			return this._itemFacade.QueryItemIllegibility(
            //				 FormatHelper.PKCapitalFormat (FormatHelper.CleanString(this.txtItemCodeQuery.Text)),FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemNameQuery.Text)),FormatHelper.CleanString(this.drpItemTypeQuery.SelectedValue),FormatHelper.CleanString(this.drpItemModelCode.SelectedValue),string.Empty,
            //				inclusive, exclusive );
            /* 如果新增字段，请去修改查询的方法 */
            return this._itemFacade.GetItemWithModelCode(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemNameQuery.Text)), FormatHelper.CleanString(this.drpItemTypeQuery.SelectedValue), FormatHelper.CleanString(this.txtModelCodeQuery.Text), string.Empty,
                inclusive, exclusive);
        }

        protected void drpItemTypeQuery_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpItemTypeQuery.Items.Clear();
                DropDownListBuilder _builder = new DropDownListBuilder(this.drpItemTypeQuery);
                _builder.AddAllItem(languageComponent1);
                this.drpItemTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(ItemType.ITEMTYPE_FINISHEDPRODUCT), ItemType.ITEMTYPE_FINISHEDPRODUCT));
                this.drpItemTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(ItemType.ITEMTYPE_SEMIMANUFACTURE), ItemType.ITEMTYPE_SEMIMANUFACTURE));
            }
        }
        //所属产品别
        protected void drpModelEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpModelEdit.Items.Clear();
                DropDownListBuilder builder = new DropDownListBuilder(this.drpModelEdit);
                builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this.GetModels);
                builder.Build("ModelCode", "ModelCode");

                //added by jessie lee,2005/10/20,for P4.12
                ListItem emptyItem = new ListItem("", "");
                this.drpModelEdit.Items.Insert(0, emptyItem);
            }

            //this.drpModelEdit.SelectedIndex=0;
        }

        //获取所有的Model(产品别)
        private object[] GetModels()
        {
            if (_modelFacade == null) { _modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade(); }
            return this._modelFacade.QueryModels("");
        }

        protected void drpItemTypeEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpItemTypeEdit.Items.Clear();

                this.drpItemTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(ItemType.ITEMTYPE_FINISHEDPRODUCT), ItemType.ITEMTYPE_FINISHEDPRODUCT));
                this.drpItemTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(ItemType.ITEMTYPE_SEMIMANUFACTURE), ItemType.ITEMTYPE_SEMIMANUFACTURE));
            }
        }

        private void drpRouteCodeEdit_Load(object sender, System.EventArgs e)
        {
            //			if(!IsPostBack)
            //			{
            //				this.drpRouteCodeEdit.Items.Clear();
            //				this.drpRouteCodeEdit.Items.Add(new ListItem("",""));
            //				this.drpRouteCodeEdit.Items.Add(new ListItem(ItemControlType.ITEMCONTROLTYPE_LOT,ItemControlType.ITEMCONTROLTYPE_LOT));
            //				this.drpRouteCodeEdit.Items.Add(new ListItem(ItemControlType.ITEMCONTROLTYPE_PICS,ItemControlType.ITEMCONTROLTYPE_PICS));
            //			}
        }

        private int GetRowCount()
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            //return this._itemFacade.QueryItemIllegibilityCount(
            //FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemNameQuery.Text)),FormatHelper.CleanString(this.drpItemTypeQuery.SelectedValue),string.Empty,string.Empty);

            return this._itemFacade.GetItemWithModelCodeCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemNameQuery.Text)), FormatHelper.CleanString(this.drpItemTypeQuery.SelectedValue), FormatHelper.CleanString(this.txtModelCodeQuery.Text), string.Empty);
        }

        private string[] FormatExportRecord(object obj)
        {
            /*
            return new string[]{((Item)obj).ItemCode ,
                                   ((Item)obj).ItemName ,
                                   this.languageComponent1.GetString( ((Item)obj).ItemType ),
                                   ((Item)obj).ItemUOM ,
                                   ((Item)obj).ItemDescription ,
                                   ((ItemOfModel)obj).ModelCode ,
                                   ((Item)obj).ItemConfigration ,
                                   ((Item)obj).ItemBurnInQty.ToString("##.##") ,
                                   ((Item)obj).ItemCartonQty.ToString("##.##"),
                                   ((Item)obj).MaintainUser ,
                                   FormatHelper.ToDateString(((Item)obj).MaintainDate),
                                   FormatHelper.ToDateString(((Item)obj).MaintainTime)};
            */
            // Modified By Hi1/Venus.Feng on 20080707 for Hisense : Mark these columns
            //Modified by Terry 2010-10-29 (add PcbaCount)
            return new string[]{
                ((Item)obj).ItemCode ,
                ((Item)obj).ItemName ,
                this.languageComponent1.GetString( ((Item)obj).ItemType ),
                ((Item)obj).ItemUOM ,
                ((Item)obj).ItemDescription ,
                ((ItemOfModel)obj).ModelCode ,
                ((Item)obj).ItemCartonQty.ToString("##.##"),
                ((Item)obj).GetDisplayText("CheckItemOP"),
                ((Item)obj).LotSize.ToString(),
                ((Item)obj).ItemProductCode,
                FormatHelper.DisplayBoolean(((Item)obj).NeedCheckCarton, this.languageComponent1),
                FormatHelper.DisplayBoolean(((Item)obj).NeedCheckAccessory, this.languageComponent1),
                ((Item)obj).BurnUseMinutes.ToString(),
                ((Item)obj).GetDisplayText("MaintainUser") ,
                FormatHelper.ToDateString(((Item)obj).MaintainDate),
                FormatHelper.ToTimeString(((Item)obj).MaintainTime),
                ((Item)obj).PcbaCount.ToString()  
            };

            // End
        }

        private string[] GetColumnHeaderText()
        {
            /*
			return new string[] {	"ItemCode",
									"ItemName",
									"ItemType",	
									"UOM",
									"Description",
									"ModelCode2",
									"ItemConfig",
									"ItemUser",	
									"ItemDate",
									"BurnInOutVolumn",
									"CartonQty",
                                    "OrganizationID"
			                        };
            */
            // Modified By Hi1/Venus.Feng on 20080707 for Hisense : Mark these columns
            return new string[] {	
                "ItemCode",
                "ItemName",
                "ItemType",	
                "UOM",
                "Description",
                "ModelCode2",
                "CartonQty",	
                "OPCodeForLot",
                "LotSize",
                "ItemProdcutCode",
                "NeedCheckCarton",
                "CompareAccessory",
                "BurnUseMinutes",
                "MaintainUser",
                "MaintainDate",
                "MaintainTime",
                "PcbaCount"
            };
            // End
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(
                1, int.MaxValue);
        }

        #endregion


        private void BuildOrgList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListOrg);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllOrg);
            builder.Build("OrganizationDescription", "OrganizationID");
            this.DropDownListOrg.Items.Insert(0, new ListItem("", ""));

            this.DropDownListOrg.SelectedIndex = 0;
        }

        private object[] GetAllOrg()
        {
            BaseModelFacade facadeBaseModel = new BaseModelFacade(base.DataProvider);
            return facadeBaseModel.GetCurrentOrgList();
        }

    }
}
