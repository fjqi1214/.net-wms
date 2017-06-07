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

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class _StandardMaintainPage : BasePage
    {        
        private System.ComponentModel.IContainer components;

        private LanguageComponent _LanguageComponent1;
        private GridHelper _GridHelper;
        private ButtonHelper _ButtonHelper;
        private ExcelExporter _ExcelExporter;

        private ModelFacade _ModelFacade;

        #region Form Init

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this._LanguageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this._LanguageComponent1.Language = "CHS";
            this._LanguageComponent1.LanguagePackageDir = "";
            this._LanguageComponent1.RuntimePage = null;
            this._LanguageComponent1.RuntimeUserControl = null;
            this._LanguageComponent1.UserControlName = "";

            this._ExcelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this._ExcelExporter.FileExtension = "xls";
            this._ExcelExporter.LanguageComponent = this._LanguageComponent1;
            this._ExcelExporter.Page = this;
            this._ExcelExporter.RowSplit = "\r\n";

            this._ModelFacade = new ModelFacade(this.DataProvider);

            this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();

            if (!IsPostBack)
            {
                InitPageLanguage(this._LanguageComponent1, false);

                InitUI();
                InitButton();
                InitWebGrid();
                InitOrgList();
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        {
            if (e.Cell.Column.Key == "Edit")
            {
                object obj = this.GetEditObject(e.Cell.Row);

                if (obj != null)
                {
                    this.SetEditObject(obj);
                    this._ButtonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
            Model model = (Model)GetEditObject();
            if (model != null)
            {
                this._ModelFacade.AddModel((Model)model);
                this.RequestData();
                this._ButtonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            Model model = (Model)GetEditObject();
            if (model != null)
            {
                this._ModelFacade.UpdateModel(model);
                this.RequestData();

                this._ButtonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chbSelectAll.Checked)
            {
                this._GridHelper.CheckAllRows(CheckStatus.Checked);
            }
            else
            {
                this._GridHelper.CheckAllRows(CheckStatus.Unchecked);
            }
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList rowArray = this._GridHelper.GetCheckedRows();
            if (rowArray != null && rowArray.Count > 0)
            {
                ArrayList modelArray = new ArrayList(rowArray.Count);

                foreach (UltraGridRow row in rowArray)
                {
                    Model model = (Model)GetEditObject(row);

                    if (model != null)
                    {
                        modelArray.Add(model);
                    }
                }

                this._ModelFacade.DeleteModel((Model[])modelArray.ToArray(typeof(Model)));
                this.RequestData();
                this._ButtonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this._ExcelExporter.Export();
        }

        private void ButtonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Query)
            {
                this.ddlOrgEdit.Enabled = true;
                this.txtModelCodeEdit.ReadOnly = false;
            }
            else if (pageAction == PageActionType.Add)
            {
                this.ddlOrgEdit.Enabled = true;
                this.txtModelCodeEdit.ReadOnly = false;
            }
            else if (pageAction == PageActionType.Update)
            {
                this.ddlOrgEdit.Enabled = false;
                this.txtModelCodeEdit.ReadOnly = true;                
            }
            else if (pageAction == PageActionType.Save)
            {
                this.ddlOrgEdit.Enabled = true;
                this.txtModelCodeEdit.ReadOnly = false;

            }
            else if (pageAction == PageActionType.Cancel)
            {
                this.ddlOrgEdit.Enabled = true;
                this.txtModelCodeEdit.ReadOnly = false;                
            }
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this._GridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        #endregion

        #region LoadData

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this._GridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this._ModelFacade.QueryModels(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModelCodeQuery.Text)), inclusive, exclusive);
        }

        private int GetRowCount()
        {
            return this._ModelFacade.QueryModelsCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModelCodeQuery.Text)));
        }

        #endregion

        #region Init Functions

        private void InitHander()
        {
            this._GridHelper = new GridHelper(this.gridWebGrid);
            this._GridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
            this._GridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

            this._ButtonHelper = new ButtonHelper(this);
            this._ButtonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this._ButtonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.ButtonHelper_AfterPageStatusChangeHandle);

            this._ExcelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this._ExcelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this._ExcelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void InitButton()
        {
            this._ButtonHelper.AddDeleteConfirm();
            this._ButtonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        private void InitWebGrid()
        {
            this._GridHelper.Grid.Columns.Clear();

            this._GridHelper.AddColumn("OrganizationID", "组织编号", null);
            this._GridHelper.AddColumn("ModelCode", "产品别代码", null, 150);
            this._GridHelper.AddColumn("ModelDescription", "产品别描述", null);
            this._GridHelper.AddColumn("MaintainUser", "维护人员", null);

            this._GridHelper.Grid.Columns.FromKey("OrganizationID").Hidden = true;

            this._GridHelper.AddDefaultColumn(true, true);

            this._GridHelper.ApplyLanguage(this._LanguageComponent1);
        }

        private void InitOrgList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.ddlOrgEdit);
            builder.HandleGetObjectList = new GetObjectListDelegate((new BaseModelFacade(this.DataProvider)).GetCurrentOrgList);
            builder.Build("OrganizationDescription", "OrganizationID");
            this.ddlOrgEdit.Items.Insert(0, new ListItem("", ""));
            this.ddlOrgEdit.SelectedIndex = 0;
        }

        #endregion

        #region Get/Set Edit Object

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblOrgEdit, ddlOrgEdit, 8, true));
            manager.Add(new LengthCheck(lblModelCodeEdit, txtModelCodeEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, _LanguageComponent1);
                return false;
            }

            return true;
        }

        protected UltraGridRow GetGridRow(object obj)
        {
            Model model = (Model)obj;

            return new UltraGridRow(
                new object[]{
                    "false",
                    model.OrganizationID.ToString(),
                    model.ModelCode,
                    model.ModelDescription,
                    model.GetDisplayText("MaintainUser")
                });
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                Model model = this._ModelFacade.CreateNewModel();

                model.OrganizationID = int.Parse(this.ddlOrgEdit.SelectedValue);
                model.ModelCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModelCodeEdit.Text, 40));
                model.ModelDescription = FormatHelper.CleanString(this.txtModelDescriptionEdit.Text, 100);
                model.MaintainUser = this.GetUserCode();

                return model;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(UltraGridRow row)
        {
            return this._ModelFacade.GetModel(row.Cells.FromKey("ModelCode").Text, int.Parse(row.Cells.FromKey("OrganizationID").Text));
        }

        private void SetEditObject(object obj)
        {
            Model model = (Model)obj;

            if (model == null)
            {
                this.ddlOrgEdit.SelectedIndex = 0;
                this.txtModelCodeEdit.Text = string.Empty;
                this.txtModelDescriptionEdit.Text = string.Empty;
            }
            else
            {
                this.txtModelCodeEdit.Text = model.ModelCode;
                this.txtModelDescriptionEdit.Text = model.ModelDescription;

                try
                {
                    this.ddlOrgEdit.SelectedValue = model.OrganizationID.ToString();
                }
                catch
                {
                    this.ddlOrgEdit.SelectedIndex = 0;
                }
            }
        }

        #endregion

        #region Export

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "ModelCode",
                "ModelDescription",
                "MaintainUser"
            };
        }

        private string[] FormatExportRecord(object obj)
        {
            Model model = (Model)obj;

            return new string[]{
                model.ModelCode.ToString(),
                model.ModelDescription.ToString(),
                model.GetDisplayText("MaintainUser")
            };
        }

        #endregion
    }
}
