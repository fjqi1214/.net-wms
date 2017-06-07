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
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion
namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FOPBOMOperationComponetLoadingMP 的摘要说明。
    /// </summary>
    public partial class FOPBOMOperationComponetLoadingMP : BaseMPageMinus
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        // private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private OPBOMFacade _opBOMFacade;// = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();
        //private ModelFacade _modelFacade = new FacadeFactory(base.DataProvider).CreateModelFacade();
        private ItemFacade _itemFacade;// = new FacadeFactory(base.DataProvider).CreateItemFacade();
        protected System.Web.UI.HtmlControls.HtmlInputButton cmdReturn;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        protected System.Web.UI.WebControls.Label lblBOMItemUOMEdit;
        protected System.Web.UI.WebControls.CheckBox cbSourceSBOMEdit;
        protected System.Web.UI.WebControls.Label lblMoCode;
        protected System.Web.UI.HtmlControls.HtmlInputButton cmdRelease;
        //protected Infragistics.WebUI.UltraWebGrid.UltraWebGrid gridWebGrid;


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
            // this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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

        #region page events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHanders();
            if (!IsPostBack)
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.OpenConnection();

                try
                {

                    // 初始化界面UI
                    this.InitUI();
                    Initparamters();
                    this.InitWebGrid();

                    // 初始化页面语言
                    this.InitPageLanguage(this.languageComponent1, false);

                    SetEditObject(null);
                    InitButtonState();

                    this.chkParseBarcode.Attributes["onclick"] = "onCheckChange(this)";
                    this.chkParsePrepare.Attributes["onclick"] = "onCheckChange(this)";
                    this.chkParseProduct.Attributes["onclick"] = "onCheckChange(this)";
                    this.chkSNLength.Attributes["onclick"] = "onCheckChange(this)";

                    this.TextboxOPBOMSourceItemDescEdit.ReadOnly = true;
                    LoadParseTypeName();
                    LoadCheckTypeName();

                    this.RequestData();
                    this.chkNeedVendor.Checked = false;
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.CloseConnection();
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.AutoCloseConnection = true;
                }
            }
            this.chkLinkBarcode.Checked = true;
            this.chkLinkBarcode.Enabled = false;
        }

        private void InitButtonState()
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
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
            if (chbSourceSBOMEdit.Checked)
            {
                Response.Redirect(this.MakeRedirectUrl("FOPBOMOperationItemSP.aspx",
                    new string[] { "itemcode", "opbomcode", "opbomversion", "routecode", "opid", "actiontype", "OrgID" },
                    new string[] { ItemCode, OPBOMCode, OPBOMVersion, RouteCode, OPID, this.Actiontype.ToString(), OrgID.ToString() }));
            }
            else
            {
                object opBOMDetail = this.GetEditObject();

                if (opBOMDetail != null)
                {
                    if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
                    if (_itemFacade.GetMaterial(((OPBOMDetail)opBOMDetail).OPBOMItemCode, ((OPBOMDetail)opBOMDetail).OrganizationID) == null)
                    {
                        ExceptionManager.Raise(this.GetType().BaseType, "$Error_Material_NotFound", lblBOMItemCodeEdit.Text + ": " + ((OPBOMDetail)opBOMDetail).OPBOMItemCode);
                    }

                    if (_itemFacade.GetMaterial(((OPBOMDetail)opBOMDetail).OPBOMSourceItemCode, ((OPBOMDetail)opBOMDetail).OrganizationID) == null)
                    {
                        ExceptionManager.Raise(this.GetType().BaseType, "$Error_Material_NotFound", lblSourceItemCode.Text + ": " + ((OPBOMDetail)opBOMDetail).OPBOMSourceItemCode);
                    }

                    if (_opBOMFacade == null) { _opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade(); }
                    DataProvider.BeginTransaction();
                    try
                    {
                        OPBOMDetail opBOPDTL = opBOMDetail as OPBOMDetail;
                        this._opBOMFacade.AddOPBOMItem(opBOPDTL);
                        //Laws Lu,2006/09/01
                        /*1,目前工序BOM建立逻辑不变，增加生效检查功能和失效功能，
                         * 初始建立的工序BOM资料处于失效状态,通过生效检查后处于生效状态，
                         * 此时不允许修改，只有失效状态的工序BOM才可以修改。
                         * 生效检查逻辑包括：完整的工序BOM包含的子阶物料（替代料）必须包含某工单所有的已发料物料代码，
                         * 比如，工单发料资料中包含5种物料，则工序BOM中的子阶物料必须也有这五种物料，
                         * 且首选料不能有这五种物料之外的其他物料。具体的工单由用户在界面指定。
                         * 举例如下：工单发料资料中有A,B,C,D四种物料*/
                        //Laws Lu,2006/12/15 取消默认为失效状态

                        MOFacade moFac = (new FacadeFactory(DataProvider)).CreateMOFacade();

                        object objOPBOM = moFac.GetOPBOM(opBOPDTL.ItemCode, opBOPDTL.OPBOMCode, opBOPDTL.OPBOMVersion, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                        //
                        if (objOPBOM != null)
                        {
                            OPBOM opBOM = objOPBOM as OPBOM;

                            opBOM.Avialable = 1;

                            moFac.UpdateOPBOM(opBOM);
                        }
                        // Added by Icyer 2005/08/16
                        // 同时将物料加入到物料主档中
                        BenQGuru.eMES.Material.WarehouseFacade wf = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
                        wf.AddWarehouseItem((OPBOMDetail)opBOMDetail);

                        DataProvider.CommitTransaction();
                        // Added end
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);

                        DataProvider.RollbackTransaction();
                        throw ex;
                    }
                    finally
                    {
                        ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    }

                    this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
                    this.pagerToolBar.RowCount = GetRowCount();
                    this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
                }
            }
            SetcbSourceSBOMEdit(true);
        }

        protected void DropdownlistSBOMVersionQuery_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.RequestData();
        }

        //设置来自标准bom是否可用
        private void SetcbSourceSBOMEdit(bool ifEnabled)
        {
            this.chbSourceSBOMEdit.Enabled = ifEnabled;
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                if (_opBOMFacade == null) { _opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade(); }
                ArrayList opBOMDetails = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    object obj = this.GetEditObject(row);
                    if (obj != null)
                    {
                        opBOMDetails.Add((OPBOMDetail)obj);
                    }
                }

                this._opBOMFacade.DeleteOPBOMItem((OPBOMDetail[])opBOMDetails.ToArray(typeof(OPBOMDetail)));

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (_opBOMFacade == null) { _opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade(); }
            object opBOMDetail = this.GetEditObject();
            if (opBOMDetail != null)
            {
                DataProvider.BeginTransaction();
                try
                {
                    OPBOMDetail opBOPDTL = opBOMDetail as OPBOMDetail;

                    //this._opBOMFacade.UpdateOPBOMItem( opBOPDTL );
                    this._opBOMFacade.DeleteOPBOMItem(opBOPDTL);
                    this._opBOMFacade.AddOPBOMItem(opBOPDTL);

                    //Laws Lu,2006/09/01
                    /*1,目前工序BOM建立逻辑不变，增加生效检查功能和失效功能，
                         * 初始建立的工序BOM资料处于失效状态,通过生效检查后处于生效状态，
                         * 此时不允许修改，只有失效状态的工序BOM才可以修改。
                         * 生效检查逻辑包括：完整的工序BOM包含的子阶物料（替代料）必须包含某工单所有的已发料物料代码，
                         * 比如，工单发料资料中包含5种物料，则工序BOM中的子阶物料必须也有这五种物料，
                         * 且首选料不能有这五种物料之外的其他物料。具体的工单由用户在界面指定。
                         * 举例如下：工单发料资料中有A,B,C,D四种物料*/
                    //Laws Lu,2006/12/15 取消默认为失效的设置
                    MOFacade moFac = (new FacadeFactory(DataProvider)).CreateMOFacade();

                    object objOPBOM = moFac.GetOPBOM(opBOPDTL.ItemCode, opBOPDTL.OPBOMCode, opBOPDTL.OPBOMVersion, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                    //
                    if (objOPBOM != null)
                    {
                        OPBOM opBOM = objOPBOM as OPBOM;

                        opBOM.Avialable = 1;

                        moFac.UpdateOPBOM(opBOM);
                    }

                    DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);

                    DataProvider.RollbackTransaction();
                    throw ex;
                }
                finally
                {
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                }

                this.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
            SetcbSourceSBOMEdit(true);

        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
            SetcbSourceSBOMEdit(true);
        }
        protected void drpItemControlTypeEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_opBOMFacade == null) { _opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade(); }
                this.drpItemControlTypeEdit.Items.Clear();
                string[] controls = this._opBOMFacade.GetItemControlTypes();
                ListItem item = new ListItem("", "");
                this.drpItemControlTypeEdit.Items.Add(item);
                for (int i = 0; i < controls.Length; i++)
                {
                    this.drpItemControlTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(controls[i]), controls[i]));
                }
            }
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

        private void LoadDropdownlistSBOMVersionQuery()
        {
            this.DropdownlistSBOMVersionQuery.Items.Clear();

            object[] controls = (new SBOMFacade(base.DataProvider)).GetAllSBOMVersion(this.ItemCode.ToUpper(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (controls == null) return;
            for (int i = 0; i < controls.Length; i++)
            {
                string version = ((SBOM)controls[i]).SBOMVersion;
                if (this.DropdownlistSBOMVersionQuery.Items.FindByText(version) == null)
                {
                    this.DropdownlistSBOMVersionQuery.Items.Add(version);
                }
            }

            DropdownlistSBOMVersionQuery.SelectedIndex = 0;
        }

        //private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        //{
        //    if (this.gridHelper.IsClickEditColumn(e))
        //    {
        //        object obj = this.GetEditObject(e.Cell.Row);

        //        if (obj != null)
        //        {
        //            this.SetEditObject(obj);

        //            this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
        //        }
        //        SetcbSourceSBOMEdit(false);
        //    }
        //    if (e.Cell.Column.Key == "itemrange")
        //    {
        //        Response.Redirect(this.MakeRedirectUrl("FOPBOMItemControlMP.aspx",
        //            new string[] { "itemcode", "opbomcode", "opbomversion", "opid", "opbomitemcode", "routecode", "OrgID" },
        //            new string[] { ItemCode, OPBOMCode, OPBOMVersion, OPID, e.Cell.Row.Cells[2].Text, RouteCode, OrgID.ToString() }));
        //    }
        //}

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
           
            else if (commandName == "itemrange")
            {
                Response.Redirect(this.MakeRedirectUrl("FOPBOMItemControlMP.aspx",
                    new string[] { "itemcode", "opbomcode", "opbomversion", "opid", "opbomitemcode", "routecode", "OrgID" },
                    new string[] { ItemCode, OPBOMCode, OPBOMVersion, OPID, row.Items.FindItemByKey("OPBOMItemCode").Value.ToString(), RouteCode, OrgID.ToString() }));
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        #endregion


        #region private method
        private void Initparamters()
        {
            if (Request.Params["itemcode"] == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_RequestUrlParameter_Lost");
            }
            else
            {
                this.ViewState["itemcode"] = Request.Params["itemcode"].ToString();
            }

            if (Request.Params["opbomcode"] == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_RequestUrlParameter_Lost");
            }
            else
            {
                this.ViewState["opbomcode"] = Request.Params["opbomcode"].ToString();
            }

            if (Request.Params["routecode"] == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_RequestUrlParameter_Lost");
            }
            else
            {
                this.ViewState["routecode"] = Request.Params["routecode"].ToString();
            }

            if (Request.Params["opid"] == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_RequestUrlParameter_Lost");
            }
            else
            {
                this.ViewState["opid"] = Request.Params["opid"].ToString();
            }

            if (Request.Params["actiontype"] == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_RequestUrlParameter_Lost");
            }
            else
            {
                this.ViewState["actiontype"] = Request.Params["actiontype"].ToString();
            }

            this.LoadDropdownlistSBOMVersionQuery();
            if (Request.Params["opbomversion"] == null)
            {
                //ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
                this.DropdownlistSBOMVersionQuery.SelectedIndex = 0;
            }
            else
            {
                //this.ViewState["opbomversion"] = Request.Params["opbomversion"].ToString();
                try
                {
                    this.DropdownlistSBOMVersionQuery.SelectedValue = Request.Params["opbomversion"].ToString();
                }
                catch
                {
                    this.DropdownlistSBOMVersionQuery.SelectedIndex = 0;
                }
            }

            if (Session["ReturnMessage"] != null)
            {
                string returnMessage = Session["ReturnMessage"].ToString();
                //returnMessage = this.languageComponent1.GetString(returnMessage);
                //Response.Write("<script language=javascript>alert('" + returnMessage + "');</script>");
                WebInfoPublish.PublishInfo(this, returnMessage, this.languageComponent1);

                Session.Remove("ReturnMessage");
            }

        }

        /// <summary>
        /// 上料0 下料1
        /// </summary>
        public int Actiontype
        {
            get
            {
                try
                {
                    //默认是上料
                    if (ViewState["actiontype"] != null)
                    { return Convert.ToInt32((string)ViewState["actiontype"]); }
                    else return 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        public string ItemCode
        {
            get
            {
                return (string)ViewState["itemcode"];
            }
        }

        public string OPBOMCode
        {
            get
            {
                return (string)ViewState["opbomcode"];
            }
        }

        public string OPBOMVersion
        {
            get
            {
                //return (string)ViewState["opbomversion"];
                return this.DropdownlistSBOMVersionQuery.SelectedValue;
            }
        }
        public string RouteCode
        {
            get
            {
                return (string)ViewState["routecode"];
            }
        }

        public string OPID
        {
            get
            {
                return (string)ViewState["opid"];
            }
        }

        public int OrgID
        {
            get
            {
                if (this.ViewState["OrgID"] == null)
                    return GlobalVariables.CurrentOrganizations.First().OrganizationID;
                else return int.Parse(this.ViewState["OrgID"].ToString());
            }
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("OPBOMItemSeq", "顺序", null);
            this.gridHelper.AddColumn("OPBOMItemCode", "子阶料料号", null);
            this.gridHelper.AddColumn("OPBOMItemName", "子阶料名称", null);
            this.gridHelper.AddColumn("OPBOMSourceItemCode", "首选料", null);
            this.gridHelper.AddColumn("OPBOMItemQty", "单机用量", null);
            this.gridHelper.AddColumn("OPBOMItemUOM", "计量单位", null);
            this.gridHelper.AddColumn("OPBOMItemEffectiveDate", "生效日期", null);
            this.gridHelper.AddColumn("OPBOMItemInvalidDate", "失效日期", null);
            this.gridHelper.AddColumn("OPBOMItemECN", "ECN号码", null);
            this.gridHelper.AddColumn("ItemControlType", "管控类型", null);
            this.gridHelper.AddColumn("OPBOMParseType", "解析方式", null);
            this.gridHelper.AddColumn("OPBOMCheckType", "检查类型", null);
            this.gridHelper.AddColumn("SNLength", "序列号长度", null);
            this.gridHelper.AddColumn("MaterialDesc", "物料描述", null);
            this.gridHelper.AddColumn("NeedVendor", "必须有供应商", null);
            this.gridHelper.AddDefaultColumn(true, true);
            this.gridWebGrid.Columns.FromKey("OPBOMItemECN").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void RequestData()
        {
            // 2005-04-06
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }


        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        private void InitHanders()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
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


        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                ((OPBOMDetail)obj).OPBOMItemSeq.ToString(),
                ((OPBOMDetail)obj).OPBOMItemCode.ToString(),
			    ((OPBOMDetail)obj).OPBOMItemName.ToString(),
			    ((OPBOMDetail)obj).OPBOMSourceItemCode.ToString(),
			    ((OPBOMDetail)obj).OPBOMItemQty.ToString(),
			    ((OPBOMDetail)obj).OPBOMItemUOM.ToString(),
			    FormatHelper.ToDateString(((OPBOMDetail)obj).OPBOMItemEffectiveDate),
			    FormatHelper.ToDateString(((OPBOMDetail)obj).OPBOMItemInvalidDate),
			    this.languageComponent1.GetString(((OPBOMDetail)obj).OPBOMItemControlType.ToString()),
                MOModelPublic.TranslateParseType(((OPBOMDetail)obj).OPBOMParseType.ToString(),this.languageComponent1),
                MOModelPublic.TranslateCheckType(((OPBOMDetail)obj).OPBOMCheckType.ToString(),this.languageComponent1),
                ((OPBOMDetail)obj).SerialNoLength.ToString(),
                ((OPBOMDetail)obj).EAttribute1.ToString(),
                this.languageComponent1.GetString(((OPBOMDetail)obj).NeedVendor.ToString())
            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "OPBOMItemSeq",
                "OPBOMItemCode",
				"OPBOMItemName",
				"OPBOMSourceItemCode",	
				"OPBOMItemQty",
				"OPBOMItemUOM",	
				"OPBOMItemEffectiveDate",
                "OPBOMItemInvalidDate",
                "ItemControlType",
                "OPBOMParseType",
                "OPBOMCheckType",
                "SNLength",
                "MaterialDesc",
                "NeedVendor"
            };
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_opBOMFacade == null) { _opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade(); }
            return this._opBOMFacade.QueryOPBOMDetail(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ItemCode)), OPID, OPBOMCode, OPBOMVersion, RouteCode, string.Empty, this.Actiontype, inclusive, exclusive, OrgID);
        }
        private int GetRowCount()
        {
            if (_opBOMFacade == null) { _opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade(); }
            return this._opBOMFacade.QueryOPBOMDetailCounts(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ItemCode)), OPID, OPBOMCode, OPBOMVersion, RouteCode, string.Empty, this.Actiontype, OrgID);
        }

        protected DataRow GetGridRow(object obj)
        {
            //this.gridHelper.AddColumn("OPBOMItemCode", "子阶料料号", null);
            //this.gridHelper.AddColumn("OPBOMItemName", "子阶料名称", null);
            //this.gridHelper.AddColumn("OPBOMSourceItemCode", "首选料", null);
            //this.gridHelper.AddColumn("OPBOMItemQty", "单机用量", null);
            //this.gridHelper.AddColumn("OPBOMItemUOM", "计量单位", null);
            //this.gridHelper.AddColumn("OPBOMItemEffectiveDate", "生效日期", null);
            //this.gridHelper.AddColumn("OPBOMItemInvalidDate", "失效日期", null);
            //this.gridHelper.AddColumn("OPBOMItemECN", "ECN号码", null);
            //this.gridHelper.AddColumn("ItemControlType", "管控类型", null);
            //this.gridHelper.AddColumn("OPBOMParseType", "解析方式", null);
            //this.gridHelper.AddColumn("OPBOMCheckType", "检查类型", null);
            //this.gridHelper.AddColumn("SNLength", "序列号长度", null);
            //this.gridHelper.AddColumn("MaterialDesc", "物料描述", null);
            //this.gridHelper.AddColumn("NeedVendor", "必须有供应商", null);

            //this.gridHelper.AddDefaultColumn(true, true);
            //this.gridWebGrid.Columns.FromKey("OPBOMItemECN").Hidden = true;

            ////多语言
            //this.gridHelper.ApplyLanguage(this.languageComponent1);

            DataRow row = this.DtSource.NewRow();
            row["OPBOMItemSeq"] = ((OPBOMDetail)obj).OPBOMItemSeq.ToString();
            row["OPBOMItemCode"] = ((OPBOMDetail)obj).OPBOMItemCode.ToString();
            row["OPBOMItemName"] = ((OPBOMDetail)obj).OPBOMItemName.ToString();
            row["OPBOMSourceItemCode"] = ((OPBOMDetail)obj).OPBOMSourceItemCode.ToString();
            row["OPBOMItemQty"] = ((OPBOMDetail)obj).OPBOMItemQty.ToString();
            row["OPBOMItemUOM"] = ((OPBOMDetail)obj).OPBOMItemUOM.ToString();
            row["OPBOMItemEffectiveDate"] = FormatHelper.ToDateString(((OPBOMDetail)obj).OPBOMItemEffectiveDate);
            row["OPBOMItemInvalidDate"] = FormatHelper.ToDateString(((OPBOMDetail)obj).OPBOMItemInvalidDate);
            row["OPBOMItemECN"] = ((OPBOMDetail)obj).OPBOMItemECN.ToString();
            row["ItemControlType"] = this.languageComponent1.GetString(((OPBOMDetail)obj).OPBOMItemControlType.ToString());
            row["OPBOMParseType"] = MOModelPublic.TranslateParseType(((OPBOMDetail)obj).OPBOMParseType.ToString(), this.languageComponent1);
            row["OPBOMCheckType"] = MOModelPublic.TranslateCheckType(((OPBOMDetail)obj).OPBOMCheckType.ToString(), this.languageComponent1);
            row["SNLength"] = ((OPBOMDetail)obj).SerialNoLength.ToString();
            row["MaterialDesc"] = ((OPBOMDetail)obj).EAttribute1.ToString();
            row["NeedVendor"] = this.languageComponent1.GetString(((OPBOMDetail)obj).NeedVendor.ToString());
            return row;

        }

        private object GetEditObject(GridRecord row)
        {
            if (_opBOMFacade == null) { _opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade(); }
            object obj = _opBOMFacade.GetOPBOMDetail(ItemCode, OPID, OPBOMCode, OPBOMVersion, row.Items.FindItemByKey("OPBOMItemCode").Value.ToString(), this.Actiontype.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (obj != null)
            {
                return (OPBOMDetail)obj;
            }

            return null;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.TextboxOPBOMItemSeqEdit.Text = "0";

                this.txtItemCodeEdit.Text = string.Empty;
                this.txtBOMItemNameEdit.Text = string.Empty;
                this.txtItemQtyEdit.Text = string.Empty;
                this.txtBOMItemUOMEdit.Text = string.Empty;
                this.drpItemControlTypeEdit.SelectedIndex = 0;
                this.txtSourceItemCodeEdit.Text = string.Empty;

                this.chkParseBarcode.Checked = false;
                this.chkParsePrepare.Checked = false;
                this.chkParseProduct.Checked = false;
                this.chkLinkBarcode.Checked = true;

                this.chkCompareItem.Checked = false;
                this.chkCheckStatus.Checked = false;
                this.chkSNLength.Checked = false;
                this.chkNeedVendor.Checked = false;

                this.txtSNLength.Text = string.Empty;

                this.chkValid.Checked = true;

                this.TextboxOPBOMSourceItemDescEdit.Text = "";

                return;
            }

            this.TextboxOPBOMItemSeqEdit.Text = ((OPBOMDetail)obj).OPBOMItemSeq.ToString();
            this.TextboxOPBOMItemSeqEdit.ReadOnly = false;
            this.txtItemCodeEdit.Text = ((OPBOMDetail)obj).OPBOMItemCode.ToString();
            this.txtItemCodeEdit.ReadOnly = true;
            this.txtBOMItemNameEdit.Text = ((OPBOMDetail)obj).OPBOMItemName;
            this.txtItemQtyEdit.Text = ((OPBOMDetail)obj).OPBOMItemQty.ToString();
            this.txtBOMItemUOMEdit.Text = ((OPBOMDetail)obj).OPBOMItemUOM;
            try
            {
                this.drpItemControlTypeEdit.SelectedValue = ((OPBOMDetail)obj).OPBOMItemControlType.ToString();
            }
            catch
            {
                this.drpItemControlTypeEdit.SelectedIndex = 0;
            }
            if (((OPBOMDetail)obj).OPBOMSourceItemCode != string.Empty)
            {
                this.txtSourceItemCodeEdit.Text = ((OPBOMDetail)obj).OPBOMSourceItemCode.ToString();
                this.txtSourceItemCodeEdit.ReadOnly = true;
            }
            else
            {
                this.txtSourceItemCodeEdit.Text = string.Empty;
            }

            this.chkParseBarcode.Checked = ((OPBOMDetail)obj).OPBOMParseType.IndexOf(OPBOMDetailParseType.PARSE_BARCODE) >= 0;
            this.chkParsePrepare.Checked = ((OPBOMDetail)obj).OPBOMParseType.IndexOf(OPBOMDetailParseType.PARSE_PREPARE) >= 0;
            this.chkParseProduct.Checked = ((OPBOMDetail)obj).OPBOMParseType.IndexOf(OPBOMDetailParseType.PARSE_PRODUCT) >= 0;
            this.chkCheckStatus.Checked = (((OPBOMDetail)obj).OPBOMParseType.IndexOf(OPBOMDetailParseType.PARSE_PRODUCT) >= 0 && ((OPBOMDetail)obj).CheckStatus == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING);

            this.chkLinkBarcode.Checked = ((OPBOMDetail)obj).OPBOMCheckType.IndexOf(OPBOMDetailCheckType.CHECK_LINKBARCODE) >= 0;
            // Added By Hi1/Venus.Feng on 20081127 for Hisense Version : Default checked the linkbarcode
            this.chkLinkBarcode.Checked = true;
            // ENd Added

            if (((OPBOMDetail)obj).NeedVendor != null)
            {
                this.chkNeedVendor.Checked = ((OPBOMDetail)obj).NeedVendor == NeedVendor.NeedVendor_Y;
            }

            this.chkCompareItem.Checked = ((OPBOMDetail)obj).OPBOMCheckType.IndexOf(OPBOMDetailCheckType.CHECK_COMPAREITEM) >= 0;
            this.chkSNLength.Checked = ((OPBOMDetail)obj).SerialNoLength > 0;
            this.txtSNLength.Text = ((OPBOMDetail)obj).SerialNoLength.ToString();

            this.chkValid.Checked = ((OPBOMDetail)obj).OPBOMValid.ToString() == BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING;

            this.TextboxOPBOMSourceItemDescEdit.Text = ((OPBOMDetail)obj).EAttribute1;

            if (this.judgeIfFromSBOM())
            {
                this.TextboxOPBOMItemSeqEdit.ReadOnly = false;
                this.txtItemCodeEdit.ReadOnly = true;
                this.txtSourceItemCodeEdit.ReadOnly = true;
                this.txtBOMItemNameEdit.ReadOnly = true;
                this.txtItemQtyEdit.ReadOnly = true;
                this.txtBOMItemUOMEdit.ReadOnly = true;
            }
        }

        //是否来源自标准BOM
        private bool judgeIfFromSBOM()
        {
            //bool result = false;
            //if (this.gridWebGrid.Rows != null)
            //{
            //    if (this.gridWebGrid.DisplayLayout.ActiveRow.Cells.FromKey(this.gridHelper.CheckColumnKey).ToString() == "true")
            //    {
            //        result = true;
            //    }
            //}
            //return result;
            return false;
        }

        private bool[] GetItemContrl(string itemControl)
        {
            bool[] values = new bool[itemControl.Length];

            for (int i = 0; i < itemControl.Length; i++)
            {
                values[i] = FormatHelper.StringToBoolean(itemControl.Substring(i, 1));
            }

            return values;
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (_itemFacade == null) { _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); }
                if (_opBOMFacade == null) { _opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade(); }

                OPBOMDetail opBOMDetail = this._opBOMFacade.CreateNewOPBOMDetail();

                opBOMDetail.OPBOMItemSeq = int.Parse(this.TextboxOPBOMItemSeqEdit.Text);
                opBOMDetail.IsItemCheck = OPBOMFacade.OPBOMISItemCheckValue_DEFAULT;
                opBOMDetail.ItemCheckValue = OPBOMFacade.OPBOMItemCheckValue_DEFAULT;
                opBOMDetail.ItemCode = ItemCode;
                opBOMDetail.MaintainUser = this.GetUserCode();
                opBOMDetail.OPBOMCode = OPBOMCode;
                opBOMDetail.OPBOMItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeEdit.Text));
                opBOMDetail.OPBOMItemControlType = FormatHelper.CleanString(this.drpItemControlTypeEdit.SelectedValue);
                opBOMDetail.OPBOMItemName = FormatHelper.CleanString(this.txtBOMItemNameEdit.Text);
                opBOMDetail.OPBOMItemQty = System.Decimal.Parse(this.txtItemQtyEdit.Text);
                opBOMDetail.OPBOMItemType = OPBOMFacade.OPBOMITEMTYPE_DEFAULT;
                opBOMDetail.OPBOMItemUOM = FormatHelper.CleanString(this.txtBOMItemUOMEdit.Text);
                opBOMDetail.OPBOMSourceItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSourceItemCodeEdit.Text));

                ItemRoute2OP itemRoute2OP = (ItemRoute2OP)_itemFacade.GetItemRoute2Op(OPID, GlobalVariables.CurrentOrganizations.First().OrganizationID);

                opBOMDetail.OPID = OPID;
                opBOMDetail.OPBOMVersion = this.DropdownlistSBOMVersionQuery.SelectedValue;
                opBOMDetail.OPCode = itemRoute2OP.OPCode;
                opBOMDetail.OPBOMItemEffectiveDate = 20051231;
                opBOMDetail.OPBOMItemEffectiveTime = 0;
                opBOMDetail.OPBOMItemInvalidDate = 21001231;
                opBOMDetail.OPBOMItemInvalidTime = 0;
                opBOMDetail.OPBOMItemVersion = this.DropdownlistSBOMVersionQuery.SelectedValue;
                opBOMDetail.ActionType = this.Actiontype;
                opBOMDetail.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

                if (chkValid.Checked)
                    opBOMDetail.OPBOMValid = int.Parse(BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING);
                else
                    opBOMDetail.OPBOMValid = int.Parse(BenQGuru.eMES.Web.Helper.FormatHelper.FALSE_STRING);

                //Parse Type
                opBOMDetail.OPBOMParseType = string.Empty;
                if (chkParseBarcode.Checked) opBOMDetail.OPBOMParseType += "," + OPBOMDetailParseType.PARSE_BARCODE;
                if (chkParsePrepare.Checked) opBOMDetail.OPBOMParseType += "," + OPBOMDetailParseType.PARSE_PREPARE;
                if (chkParseProduct.Checked) opBOMDetail.OPBOMParseType += "," + OPBOMDetailParseType.PARSE_PRODUCT;
                if (opBOMDetail.OPBOMParseType.Length > 0)
                    opBOMDetail.OPBOMParseType = opBOMDetail.OPBOMParseType.Substring(1);
                else
                    opBOMDetail.OPBOMParseType = " ";
                if (chkParseProduct.Checked && this.chkCheckStatus.Checked)
                    opBOMDetail.CheckStatus = BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING;
                else
                    opBOMDetail.CheckStatus = BenQGuru.eMES.Web.Helper.FormatHelper.FALSE_STRING;

                //Check Type
                opBOMDetail.OPBOMCheckType = string.Empty;
                if (chkLinkBarcode.Checked) opBOMDetail.OPBOMCheckType += "," + OPBOMDetailCheckType.CHECK_LINKBARCODE;
                if (chkCompareItem.Checked) opBOMDetail.OPBOMCheckType += "," + OPBOMDetailCheckType.CHECK_COMPAREITEM;
                if (opBOMDetail.OPBOMCheckType.Length > 0)
                    opBOMDetail.OPBOMCheckType = opBOMDetail.OPBOMCheckType.Substring(1);
                else
                    opBOMDetail.OPBOMCheckType = " ";

                if (chkSNLength.Checked)
                {
                    int snLength = int.Parse(this.txtSNLength.Text);
                    opBOMDetail.SerialNoLength = snLength;
                }
                else
                {
                    opBOMDetail.SerialNoLength = 0;
                }

                if (this.chkNeedVendor.Checked)
                {
                    opBOMDetail.NeedVendor = NeedVendor.NeedVendor_Y;
                }
                else
                {
                    opBOMDetail.NeedVendor = NeedVendor.NeedVendor_N;
                }

                return opBOMDetail;
            }
            else
            {
                return null;
            }
        }



        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblOPBOMItemSeqEdit, TextboxOPBOMItemSeqEdit, 8, true));
            manager.Add(new LengthCheck(lblBOMItemCodeEdit, txtItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblSourceItemCode, txtSourceItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblItemControlTypeEdit, drpItemControlTypeEdit, 40, true));
            manager.Add(new LengthCheck(lblItemUOMEdit, txtBOMItemUOMEdit, 40, true));
            manager.Add(new DecimalCheck(lblItemQtyEdit, txtItemQtyEdit, 0, int.MaxValue, true));
            if (chkSNLength.Checked)
            {
                manager.Add(new NumberCheck(this.lblSNLength, this.txtSNLength, 1, 99999999, false));
            }

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


        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.TextboxOPBOMItemSeqEdit.ReadOnly = false;
                this.txtItemCodeEdit.ReadOnly = false;
                this.txtSourceItemCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.TextboxOPBOMItemSeqEdit.ReadOnly = false;
                this.txtItemCodeEdit.ReadOnly = true;
                this.txtSourceItemCodeEdit.ReadOnly = true;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.TextboxOPBOMItemSeqEdit.ReadOnly = false;
                this.txtItemCodeEdit.ReadOnly = false;
                this.txtSourceItemCodeEdit.ReadOnly = false;
            }
        }

        private string GetItemControl()
        {
            string itemcontrol = "";

            return itemcontrol;
        }

        private object[] GetComponentLoadingOperations()
        {
            return new object[0];
        }


        private void Alert(string msg)
        {
            msg = msg.Replace("'", "");
            msg = msg.Replace("\r", "");
            msg = msg.Replace("\n", "");
            string _msg = string.Format("<script language='JavaScript'>  alert('{0}');</script>", msg);
            Page.RegisterStartupScript("", _msg);
        }
        #endregion

        private bool ExistERPBom()
        {
            /*1,目前工序BOM建立逻辑不变，增加生效检查功能和失效功能，
             * 初始建立的工序BOM资料处于失效状态,通过生效检查后处于生效状态，
             * 此时不允许修改，只有失效状态的工序BOM才可以修改。
             * 生效检查逻辑包括：完整的工序BOM包含的子阶物料（替代料）必须包含某工单所有的已发料物料代码，
             * 比如，工单发料资料中包含5种物料，则工序BOM中的子阶物料必须也有这五种物料，
             * 且首选料不能有这五种物料之外的其他物料。具体的工单由用户在界面指定。
             * 举例如下：工单发料资料中有A,B,C,D四种物料*/
            string moCode = txtMoCode.Text.ToUpper().Trim();
            string bitemCode = txtItemCodeEdit.Text.ToUpper().Trim();
            MOFacade moFac = (new FacadeFactory(base.DataProvider)).CreateMOFacade();
            //			object[] objs = moFac.QueryERPBOM(moCode);
            bool isExist = moFac.CheckERPBOM(OPID, moCode);

            //			bool isExist = false;
            //			if(objs != null && objs.Length > 0)
            //			{
            //				foreach(Domain.MOModel.ERPBOM erpBom in objs)
            //				{
            //					if(erpBom.BITEMCODE == bitemCode && moCode == erpBom.MOCODE)
            //					{
            //						isExist = true;
            //						break;
            //					}
            //				}
            //			}

            if (!isExist)
            {
                lblMessage.Text = languageComponent1.GetString("$CS_OPBOM_NOT_MATCH_ERPBOM");
            }
            else
            {
                lblMessage.Text = String.Empty;
            }

            return isExist;
        }

        private void cmdRelese_ServerClick(object sender, System.EventArgs e)
        {
            bool isExist = ExistERPBom();

            if (!isExist)
            {
                lblMessage.Text = languageComponent1.GetString("$CS_OPBOM_NOT_MATCH_ERPBOM");
                txtStatus.Text = "失效";
            }
            else
            {
                lblMessage.Text = String.Empty;
                txtStatus.Text = "生效";
            }

        }
        
    }
}
