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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FMaterialMP 的摘要说明。
    /// </summary>
    public partial class FMaterialMP : BaseMPageMinus
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private OPBOMFacade _opBOMFacade;

        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private ItemFacade _itemFacade;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

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
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            // 
            // languageComponent1
            // 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";
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
            InitHander();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.chkParseBarcode.Attributes["onclick"] = "onCheckChange(this)";
                this.chkParsePrepare.Attributes["onclick"] = "onCheckChange(this)";
                this.chkParseProduct.Attributes["onclick"] = "onCheckChange(this)";
                this.chkSNLength.Attributes["onclick"] = "onCheckChange(this)";

                // 初始化界面UI
                this.InitUI();
                this.InitButton();
                this.InitWebGrid();

                LoadParseTypeName();
                LoadCheckTypeName();
                this.chkNeedVendor.Checked = false;
            }

            this.chkLinkBarcode.Checked = true;
            this.chkLinkBarcode.Enabled = false;

        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            object material = this.GetEditObject();

            if (material != null)
            {
                if (!string.IsNullOrEmpty(((Domain.MOModel.Material)material).VendorCode))
                {
                    ItemFacade itemfacade = new ItemFacade(this.DataProvider);
                    object objvendor = itemfacade.GetVender((((Domain.MOModel.Material)material).VendorCode.Trim().ToUpper()));
                    if (objvendor == null)
                    {
                        WebInfoPublish.Publish(this, "$Vendor_isnot_exit", this.languageComponent1);
                        return;
                    }
                }
                this._itemFacade.UpdateMaterial((Domain.MOModel.Material)material);
                this.RequestData();

                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
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

        private int GetRowCount()
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            return this._itemFacade.QueryMaterialCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.TextBoxMaterialCodeQuery.Text)),
                                                       FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.TextBoxMaterialNameQuery.Text)),
                                                       FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpControlType.Text)),
                                                       FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.TextBoxItemNameEditSRM.Text)));
        }

        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }


        protected void drpControlType_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {

                if (_opBOMFacade == null) { _opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade(); }
                this.drpControlType.Items.Clear();
                string[] controls = this._opBOMFacade.GetItemControlTypes();
                ListItem item = new ListItem("", "");
                this.drpControlType.Items.Add(item);
                for (int i = 0; i < controls.Length; i++)
                {
                    this.drpControlType.Items.Add(new ListItem(this.languageComponent1.GetString(controls[i]), controls[i]));
                }
            }
        }

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            //			
        }


        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        public void InitButton()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("MaterialCode", "物料代码", null);
            this.gridHelper.AddColumn("MaterialName", "物料名称", null);
            this.gridHelper.AddColumn("MaterialControlType", "管控类型", null);
            this.gridHelper.AddColumn("ParseType", "解析方式", null);
            this.gridHelper.AddColumn("CheckType", "检查类型", null);
            this.gridHelper.AddColumn("CheckStatus", "生产防错", null);
            this.gridHelper.AddColumn("SNLength", "序列号长度", null);
            this.gridHelper.AddColumn("MaterialDesc", "物料描述", null);
            this.gridHelper.AddColumn("VerdorCode", "供应商代码", null);
            this.gridHelper.AddColumn("NeedVendor", "必须有供应商", null);
            this.gridHelper.AddColumn("IsSMTMaterial", "SMT料", null);
            this.gridHelper.AddColumn("OrganizationID", "组织编号", null);

            this.gridWebGrid.Columns.FromKey("OrganizationID").Hidden = true;

            this.gridHelper.AddDefaultColumn(false, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private object GetEditObject()
        {
            Domain.MOModel.Material material = null;

            if (this.ValidateInput())
            {
                if (_itemFacade == null)
                    _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();

                material = (Domain.MOModel.Material)_itemFacade.GetMaterial(this.TextBoxMaterialCodeEdit.Text, int.Parse(this.TextBoxOrgIDEdit.Text));

                if (material != null)
                {
                    //Parse Type
                    material.MaterialParseType = string.Empty;
                    if (chkParseBarcode.Checked) material.MaterialParseType += "," + OPBOMDetailParseType.PARSE_BARCODE;
                    if (chkParsePrepare.Checked) material.MaterialParseType += "," + OPBOMDetailParseType.PARSE_PREPARE;
                    if (chkParseProduct.Checked) material.MaterialParseType += "," + OPBOMDetailParseType.PARSE_PRODUCT;
                    if (material.MaterialParseType.Length > 0)
                        material.MaterialParseType = material.MaterialParseType.Substring(1);
                    else
                        material.MaterialParseType = " ";
                    if (chkParseProduct.Checked && chkCheckStatus.Checked)
                        material.CheckStatus = BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING;
                    else
                        material.CheckStatus = BenQGuru.eMES.Web.Helper.FormatHelper.FALSE_STRING;

                    //Check Type
                    material.MaterialCheckType = string.Empty;
                    if (chkLinkBarcode.Checked) material.MaterialCheckType += "," + OPBOMDetailCheckType.CHECK_LINKBARCODE;
                    if (chkCompareItem.Checked) material.MaterialCheckType += "," + OPBOMDetailCheckType.CHECK_COMPAREITEM;
                    if (material.MaterialCheckType.Length > 0)
                        material.MaterialCheckType = material.MaterialCheckType.Substring(1);
                    else
                        material.MaterialCheckType = " ";
                    if (chkNeedVendor.Checked)
                        material.NeedVendor = NeedVendor.NeedVendor_Y;
                    else
                        material.NeedVendor = NeedVendor.NeedVendor_N;



                    material.VendorCode = this.txtVendorCode.Text.Trim().ToUpper();

                    if (chkSNLength.Checked)
                    {
                        int snLength = int.Parse(this.txtSNLength.Text);
                        material.SerialNoLength = snLength;
                    }
                    else
                    {
                        material.SerialNoLength = 0;
                    }

                    //Add By Bernard @ 2010-10-28
                    material.MaterialControlType = (ddlControlTypeEdit.SelectedValue != "") ? ddlControlTypeEdit.SelectedValue : "";

                    //Add By  jack  2012-03-13 
                    if (chkIsSMT.Checked)
                        material.IsSMT = MaterialIsSMT.MaterialIsSMT_Y;
                    else
                        material.IsSMT = MaterialIsSMT.MaterialIsSMT_N;

                }
            }

            return material;
        }

        private object GetEditObject(GridRecord row)
        {
            if (_itemFacade == null)
            {
                _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();
            }
            object obj = this._itemFacade.GetMaterial(row.Items.FindItemByKey("MaterialCode").Value.ToString(), int.Parse(row.Items.FindItemByKey("OrganizationID").Value.ToString()));

            if (obj != null)
            {
                return (Domain.MOModel.Material)obj;
            }

            return null;
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            if (chkSNLength.Checked)
            {
                manager.Add(new NumberCheck(this.lblSNLength, this.txtSNLength, 1, 99999999, false));
            }

            manager.Add(new LengthCheck(this.lblControlTypeEdit, this.ddlControlTypeEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            if (this.chkCompareItem.Checked == true)
            {
                if (!this.chkParseBarcode.Checked && !this.chkParsePrepare.Checked && !this.chkParseProduct.Checked)
                {
                    WebInfoPublish.Publish(this, "$CheckCompareItem_Must_CheckOneParse", languageComponent1);
                    return false;
                }
            }

            return true;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.TextBoxMaterialCodeEdit.Text = string.Empty;
                this.TextBoxOrgIDEdit.Text = string.Empty;

                this.chkParseBarcode.Checked = false;
                this.chkParsePrepare.Checked = false;
                this.chkParseProduct.Checked = false;
                this.chkLinkBarcode.Checked = true;

                this.chkCompareItem.Checked = false;
                this.chkCheckStatus.Checked = false;
                this.chkSNLength.Checked = false;
                this.chkNeedVendor.Checked = false;
                this.txtVendorCode.Text = string.Empty;
                this.txtSNLength.Text = string.Empty;
                this.chkIsSMT.Checked = false;

                this.ddlControlTypeEdit.SelectedIndex = 0;

                return;
            }

            this.TextBoxMaterialCodeEdit.Text = ((Domain.MOModel.Material)obj).MaterialCode;
            this.TextBoxOrgIDEdit.Text = ((Domain.MOModel.Material)obj).OrganizationID.ToString();
            this.txtVendorCode.Text = ((Domain.MOModel.Material)obj).VendorCode.ToString();

            if (((Domain.MOModel.Material)obj).MaterialParseType == null)
                ((Domain.MOModel.Material)obj).MaterialParseType = "";

            this.chkParseBarcode.Checked = ((Domain.MOModel.Material)obj).MaterialParseType.IndexOf(OPBOMDetailParseType.PARSE_BARCODE) >= 0;
            this.chkParsePrepare.Checked = ((Domain.MOModel.Material)obj).MaterialParseType.IndexOf(OPBOMDetailParseType.PARSE_PREPARE) >= 0;
            this.chkParseProduct.Checked = ((Domain.MOModel.Material)obj).MaterialParseType.IndexOf(OPBOMDetailParseType.PARSE_PRODUCT) >= 0;
            this.chkCheckStatus.Checked = (((Domain.MOModel.Material)obj).MaterialParseType.IndexOf(OPBOMDetailParseType.PARSE_PRODUCT) >= 0 && ((Domain.MOModel.Material)obj).CheckStatus == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING);

            if (((Domain.MOModel.Material)obj).MaterialCheckType == null)
                ((Domain.MOModel.Material)obj).MaterialCheckType = "";

            this.chkLinkBarcode.Checked = ((Domain.MOModel.Material)obj).MaterialCheckType.IndexOf(OPBOMDetailCheckType.CHECK_LINKBARCODE) >= 0;
            this.chkCompareItem.Checked = ((Domain.MOModel.Material)obj).MaterialCheckType.IndexOf(OPBOMDetailCheckType.CHECK_COMPAREITEM) >= 0;
            this.chkSNLength.Checked = ((Domain.MOModel.Material)obj).SerialNoLength > 0;
            this.txtSNLength.Text = ((Domain.MOModel.Material)obj).SerialNoLength.ToString();

            if (((Domain.MOModel.Material)obj).NeedVendor != null)
            {
                this.chkNeedVendor.Checked = ((Domain.MOModel.Material)obj).NeedVendor == NeedVendor.NeedVendor_Y;
            }

            //   add by jack 2012-03-13  
            if (!string.IsNullOrEmpty(((Domain.MOModel.Material)obj).IsSMT))
            {
                this.chkIsSMT.Checked = ((Domain.MOModel.Material)obj).IsSMT.IndexOf(MaterialIsSMT.MaterialIsSMT_Y) >= 0;
            }
            else
            {
                this.chkIsSMT.Checked = false;
            }

            //Add By Bernard @ 2010-10-28
            try
            {
                this.ddlControlTypeEdit.SelectedValue = ((Domain.MOModel.Material)obj).MaterialControlType.Trim();
            }
            catch
            {
                this.ddlControlTypeEdit.SelectedIndex = 0;
            }

            this.chkLinkBarcode.Checked = true;
            return;
        }

        protected DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["MaterialCode"] = ((Domain.MOModel.Material)obj).MaterialCode.ToString();
            row["MaterialName"] = ((Domain.MOModel.Material)obj).MaterialName == null ? "" : ((Domain.MOModel.Material)obj).MaterialName;
            row["MaterialControlType"] = this.languageComponent1.GetString(((Domain.MOModel.Material)obj).MaterialControlType.ToString());
            row["ParseType"] = ((Domain.MOModel.Material)obj).MaterialParseType == null ? "" : MOModelPublic.TranslateParseType(((Domain.MOModel.Material)obj).MaterialParseType, this.languageComponent1);
            row["CheckType"] = ((Domain.MOModel.Material)obj).MaterialCheckType == null ? "" : MOModelPublic.TranslateCheckType(((Domain.MOModel.Material)obj).MaterialCheckType, this.languageComponent1);
            row["CheckStatus"] = FormatHelper.DisplayBoolean(((Domain.MOModel.Material)obj).CheckStatus, this.languageComponent1);
            row["SNLength"] = ((Domain.MOModel.Material)obj).SerialNoLength.ToString();
            row["MaterialDesc"] = ((Domain.MOModel.Material)obj).MaterialDescription == null ? "" : ((Domain.MOModel.Material)obj).MaterialDescription;
            row["VerdorCode"] = ((Domain.MOModel.Material)obj).GetDisplayText("VendorCode");
            row["NeedVendor"] = this.languageComponent1.GetString(((Domain.MOModel.Material)obj).NeedVendor.ToString());
            row["IsSMTMaterial"] = this.languageComponent1.GetString(((Domain.MOModel.Material)obj).IsSMT.ToString());
            row["OrganizationID"] = ((Domain.MOModel.Material)obj).OrganizationID.ToString();
            return row;
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
            return this._itemFacade.QueryMaterial(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.TextBoxMaterialCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.TextBoxMaterialNameQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpControlType.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.TextBoxItemNameEditSRM.Text)),
                inclusive, exclusive);
        }

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                ((Domain.MOModel.Material)obj).MaterialCode.ToString(),
                ((Domain.MOModel.Material)obj).MaterialName == null ? "" : ((Domain.MOModel.Material)obj).MaterialName,
                this.languageComponent1.GetString(((Domain.MOModel.Material)obj).MaterialControlType.ToString()),
                ((Domain.MOModel.Material)obj).MaterialParseType == null ? "" : MOModelPublic.TranslateParseType(((Domain.MOModel.Material)obj).MaterialParseType, this.languageComponent1),
                ((Domain.MOModel.Material)obj).MaterialCheckType == null ? "" : MOModelPublic.TranslateCheckType(((Domain.MOModel.Material)obj).MaterialCheckType, this.languageComponent1),			
                FormatHelper.DisplayBoolean(((Domain.MOModel.Material)obj).CheckStatus,this.languageComponent1),
                ((Domain.MOModel.Material)obj).SerialNoLength.ToString(),                
                ((Domain.MOModel.Material)obj).MaterialDescription == null ? "" : ((Domain.MOModel.Material)obj).MaterialDescription ,
                ((Domain.MOModel.Material)obj).GetDisplayText("VendorCode"),
                this.languageComponent1.GetString(((Domain.MOModel.Material)obj).NeedVendor.ToString()),
                this.languageComponent1.GetString(((Domain.MOModel.Material)obj).IsSMT.ToString()),
            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "MaterialCode",
                "MaterialName",
                "MaterialControlType",
                "ParseType",
                "CheckType",
                "CheckStatus",
                "SNLength",               
                "MaterialDesc",
                "VendorCode",
                "NeedVendor",
                "IsSMTMaterial"
            };

        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        private void LoadCheckTypeName()
        {
            chkLinkBarcode.Text = this.languageComponent1.GetString(OPBOMDetailCheckType.CHECK_LINKBARCODE);
            chkCompareItem.Text = this.languageComponent1.GetString(OPBOMDetailCheckType.CHECK_COMPAREITEM);
        }

        private void LoadParseTypeName()
        {
            chkParseBarcode.Text = this.languageComponent1.GetString(OPBOMDetailParseType.PARSE_BARCODE);
            chkParsePrepare.Text = this.languageComponent1.GetString(OPBOMDetailParseType.PARSE_PREPARE);
            chkParseProduct.Text = this.languageComponent1.GetString(OPBOMDetailParseType.PARSE_PRODUCT);
        }

        //Add By Bernard @ 2010-10-28
        protected void ddlControlTypeEdit_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_opBOMFacade == null)
                {
                    _opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();
                }
                this.ddlControlTypeEdit.Items.Clear();
                string[] controls = this._opBOMFacade.GetItemControlTypes();
                ddlControlTypeEdit.Items.Add(new ListItem("", ""));
                for (int i = 0; i < controls.Length; i++)
                {
                    this.ddlControlTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(controls[i]), controls[i]));
                }
            }
        }
        #endregion


    }
}
