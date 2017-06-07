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

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.OQC
{
    /// <summary>
    /// FUnfrozenMP 的摘要说明。
    /// </summary>
    public partial class FUnfrozenMP : BasePage
    {
        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

        private BenQGuru.eMES.OQC.OQCFacade _OQCFacade = null;

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
            this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

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

                this.cmdSave.Value = this.languageComponent1.GetString("Unfrosen");
            }

            //this.cmdSave.Disabled = false;
        }
        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            if (!ValidateQueryInput())
                return;

            RequestData();
        }

        protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
        {
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null || array.Count <= 0)
                return;

            if (!ValidateInput())
                return;

            Frozen frozen = null;
            string unfrozenReason = this.txtUnfrozenCauseEdit.Text.Trim();
            string userCode = this.GetUserCode();            

            try
            {
                ((SQLDomainDataProvider)this.DataProvider).PersistBroker.AutoCloseConnection = false;
                ((SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
                this.DataProvider.BeginTransaction();

                if (_OQCFacade == null)
                {
                    _OQCFacade = new OQCFacade(this.DataProvider);
                }

                foreach (UltraGridRow row in array)
                {
                    frozen = (Frozen)this.GetEditObject(row);

                    if (frozen != null)
                    {
                        _OQCFacade.UnfreezeRCard(frozen, unfrozenReason, userCode);
                    }
                }

                this.DataProvider.CommitTransaction();

                this.txtUnfrozenCauseEdit.Text = string.Empty;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            finally
            {
                ((SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
                ((SQLDomainDataProvider)this.DataProvider).PersistBroker.AutoCloseConnection = true;
            }

            this.gridHelper.RequestData();
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
        }

        private void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chbSelectAll.Checked)
            {
                this.gridHelper.CheckAllRows(CheckStatus.Checked);
            }
            else
            {
                this.gridHelper.CheckAllRows(CheckStatus.Unchecked);
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

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelper(this.gridWebGrid);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        public void InitButtonHelp()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
        }

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("RunningCard", "产品序列号", null);
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("MOCode", "工单代码", null);
            this.gridHelper.AddColumn("OQCLotNo", "批号", null);
            this.gridHelper.AddColumn("LotSequence", "子批号", null);
            this.gridHelper.AddColumn("FrosenReason", "隔离原因", null);
            this.gridHelper.AddColumn("FrosenDate", "隔离日期", null);
            this.gridHelper.AddColumn("FrosenTime", "隔离时间", null);
            this.gridHelper.AddColumn("FrosenUser", "隔离人", null);
            this.gridHelper.AddColumn("FrosenSequence", "隔离次数", null);

            this.gridWebGrid.Columns.FromKey("LotSequence").Hidden = true;
            this.gridWebGrid.Columns.FromKey("FrosenSequence").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                return null;
            }
            else
            {
                return null;
            }
        }

        private object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(this.DataProvider);
            }

            return _OQCFacade.GetFrozen(row.Cells[1].Text, row.Cells[4].Text, int.Parse(row.Cells[5].Text), row.Cells[3].Text, row.Cells[2].Text, int.Parse(row.Cells[10].Text));
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblUnfrozenCauseEdit, this.txtUnfrozenCauseEdit, 100, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        private bool ValidateQueryInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new DateCheck(this.lblStartDateQuery, this.txtDateFrom.Text, false));
            manager.Add(new DateCheck(this.lblEndDateQuery, this.txtDateTo.Text, false));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        private void SetEditObject(object obj)
        {
        }

        protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            Frozen frozen = (Frozen)obj;
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{
								"false",
								frozen.RCard,
								frozen.ItemCode,
								frozen.MOCode,
								frozen.LotNo,
								frozen.LotSequence.ToString(),
								frozen.FrozenReason,
                                FormatHelper.ToDateString(frozen.FrozenDate),
                                FormatHelper.ToTimeString(frozen.FrozenTime),
								frozen.FrozenBy,
								frozen.FrozenSequence.ToString()
							});
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(this.DataProvider);
            }

            string rCardStart = string.Empty;
            string rCardEnd = string.Empty;
            string lotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLotNoQuery.Text));
            string moCode = string.Empty;
            string itemCode = string.Empty;

            string frozenStatus = FrozenStatus.STATUS_FRONZEN;
            int frozenDateStart = FormatHelper.TODateInt(this.txtDateFrom.Text);
            int frozenDateEnd = FormatHelper.TODateInt(this.txtDateTo.Text);
            int unfrozenDateStart = -1;
            int unfrozenDateEnd = -1;

            return this._OQCFacade.QueryFrozen(
                rCardStart, rCardEnd,
                lotNo, moCode, itemCode,
                frozenStatus, frozenDateStart, frozenDateEnd, unfrozenDateStart, unfrozenDateEnd,
                inclusive, exclusive);
        }

        private int GetRowCount()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(this.DataProvider);
            }

            string rCardStart = string.Empty;
            string rCardEnd = string.Empty;
            string lotNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLotNoQuery.Text));
            string moCode = string.Empty;
            string itemCode = string.Empty;

            string frozenStatus = FrozenStatus.STATUS_FRONZEN;
            int frozenDateStart = FormatHelper.TODateInt(this.txtDateFrom.Text);
            int frozenDateEnd = FormatHelper.TODateInt(this.txtDateTo.Text);
            int unfrozenDateStart = -1;
            int unfrozenDateEnd = -1;

            return this._OQCFacade.QueryFrozenCount(
                rCardStart, rCardEnd,
                lotNo, moCode, itemCode,
                frozenStatus, frozenDateStart, frozenDateEnd, unfrozenDateStart, unfrozenDateEnd);
        }

        private string[] FormatExportRecord(object obj)
        {
            Frozen frozen = (Frozen)obj;
            return new string[]{
                frozen.RCard,
                frozen.ItemCode,
                frozen.MOCode,
                frozen.LotNo,
                frozen.FrozenReason,
                FormatHelper.ToDateString(frozen.FrozenDate),
                FormatHelper.ToTimeString(frozen.FrozenTime),
                frozen.FrozenBy
            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "RunningCard",
                "ItemCode",
                "MOCode",
                "OQCLotNo",
                "FrosenReason",
                "FrosenDate",
                "FrosenTime",
                "FrosenUser"
            };
        }
        #endregion

        private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        {
        }

    }
}
