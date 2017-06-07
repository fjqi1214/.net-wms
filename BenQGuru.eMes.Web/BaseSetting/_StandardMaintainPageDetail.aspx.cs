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
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

namespace BenQGuru.eMES.Web.BaseSetting
{
    public partial class _StandardMaintainPageDetail : BasePage
    {
        private System.ComponentModel.IContainer components;

        private LanguageComponent _LanguageComponent1;
        private GridHelper _GridHelper;
        private ButtonHelper _ButtonHelper;
        private ExcelExporter _ExcelExporter;

        private UserFacade _UserFacade;

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

            this._UserFacade = new UserFacade(this.DataProvider);

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
                this.InitPageLanguage(this._LanguageComponent1, false);

                InitUI();
                InitButton();
                InitWebGrid();

                this.txtUserGroupCodeQuery.Text = this.GetRequestParam("usergroupcode");

                cmdQuery_ServerClick(null, null);
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
                ArrayList userGroup2UserArray = new ArrayList(rowArray.Count);

                foreach (UltraGridRow row in rowArray)
                {
                    UserGroup2User userGroup2User = (UserGroup2User)GetEditObject(row);

                    if (userGroup2User != null)
                    {
                        userGroup2UserArray.Add(userGroup2User);
                    }
                }

                this._UserFacade.DeleteUserGroup2User((UserGroup2User[])userGroup2UserArray.ToArray(typeof(UserGroup2User)));
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
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this._GridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("./FUserGroupMP.aspx"));
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

        protected object[] LoadDataSource(int inclusive, int exclusive)
        {
            return _UserFacade.QueryUserGroup2User(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
                string.Empty,
                inclusive, exclusive);
        }

        protected int GetRowCount()
        {
            return this._UserFacade.QueryUserGroup2UserCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserGroupCodeQuery.Text)),
                string.Empty);
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

        protected void InitWebGrid()
        {
            this._GridHelper.Grid.Columns.Clear();

            this._GridHelper.AddColumn("UserCode", "用户代码", null);
            this._GridHelper.AddColumn("UserGroupCode", "用户组代码", null);
            this._GridHelper.AddColumn("MaintainUser", "维护用户", null);
            this._GridHelper.AddColumn("MaintainDate", "维护日期", null);
            this._GridHelper.AddColumn("MaintainTime", "维护时间", null);

            this._GridHelper.AddDefaultColumn(true, false);

            this._GridHelper.ApplyLanguage(this._LanguageComponent1);
        }

        #endregion

        #region Get/Set Edit Object

        private UltraGridRow GetGridRow(object obj)
        {
            UserGroup2User userGroup2User = (UserGroup2User)obj;

            return new UltraGridRow(
                new object[]{
                    "false",
                    userGroup2User.UserCode,
                    userGroup2User.UserGroupCode,
                    userGroup2User.GetDisplayText("MaintainUser"),
                    FormatHelper.ToDateString(userGroup2User.MaintainDate),
                    FormatHelper.ToTimeString(userGroup2User.MaintainTime)
                });
        }

        private object GetEditObject()
        {
            return null;
        }

        private object GetEditObject(UltraGridRow row)
        {
            UserGroup2User userGroup2User = this._UserFacade.CreateUserGroup2User();

            userGroup2User.UserGroupCode = row.Cells.FromKey("UserGroupCode").Text;
            userGroup2User.UserCode = row.Cells.FromKey("UserCode").Text;
            userGroup2User.MaintainUser = this.GetUserCode();

            return userGroup2User;
        }

        private void SetEditObject(object obj)
        {
        }

        #endregion

        #region Export

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "UserCode",
                "UserGroupCode",
                "MaintainUser",
                "MaintainDate",
                "MaintainTime"
            };
        }

        private string[] FormatExportRecord(object obj)
        {
            UserGroup2User userGroup2User = (UserGroup2User)obj;

            return new string[]{
                userGroup2User.UserCode,
                userGroup2User.UserGroupCode,
                userGroup2User.GetDisplayText("MaintainUser"),
                FormatHelper.ToDateString(userGroup2User.MaintainDate),
                FormatHelper.ToTimeString(userGroup2User.MaintainTime)
            };
        }

        #endregion
    }
}
