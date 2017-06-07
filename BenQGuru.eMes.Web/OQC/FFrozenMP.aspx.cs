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
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;


namespace BenQGuru.eMES.Web.OQC
{
    /// <summary>
    /// FFrozenMP 的摘要说明。
    /// </summary>
    public partial class FFrozenMP : BasePage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private OQCFacade _OQCFacade = null;
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
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
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
            InitHander();

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化界面UI
                this.InitUI();
                this.InitButton();
                this.InitWebGrid();
            }
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();

            AutoSetEdit();
        }

        private void AutoSetEdit()
        {
            //自动设定Edit
            if (this.gridWebGrid.Rows.Count > 0)
            {
                object obj = this.GetEditObject(this.gridWebGrid.Rows[0]);
                if (obj != null)
                {
                    this.SetEditObject(obj);
                }

                this.buttonHelper.PageActionStatusHandle(PageActionType.Update);

                if (_OQCFacade == null)
                {
                    _OQCFacade = new OQCFacade(base.DataProvider);
                }

                int frozenRCardCount = _OQCFacade.QueryOQCLotFrozenCount(this.gridWebGrid.Rows[0].Cells[1].ToString());

                if (frozenRCardCount > 0)
                {
                    this.txtFrozenCauseEdit.ReadOnly = true;
                    this.chkFrozenLotEdit.Enabled = false;
                    this.cmdSave.Disabled = true;
                }
                else
                {
                    this.txtFrozenCauseEdit.ReadOnly = false;
                    this.chkFrozenLotEdit.Enabled = true;
                }
            }
            else
            {
                this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
            }
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if (!ValidateInput())
                return;

            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            int frozenRCardCount = _OQCFacade.QueryOQCLotFrozenCount(this.gridWebGrid.Rows[0].Cells[1].ToString());

            if (frozenRCardCount > 0)
            {
                WebInfoPublish.Publish(this, "$Message_LotIsFrozen", languageComponent1);
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
                return;
            }

            OQCLot lot = (OQCLot)this.GetEditObject(this.gridWebGrid.Rows[0]);

            if (lot != null)
            {
                try
                {
                    ((SQLDomainDataProvider)this.DataProvider).PersistBroker.AutoCloseConnection = false;
                    ((SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
                    this.DataProvider.BeginTransaction();

                    _OQCFacade.FreezeLot(lot,this.txtFrozenCauseEdit.Text.Trim().ToUpper(), this.chkFrozenLotEdit.Checked,this.GetUserCode());

                    this.DataProvider.CommitTransaction();

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

                cmdQuery_ServerClick(sender, e);
            }
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.txtLotNoQuery.Text = string.Empty;

            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
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

        private void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Query)
            {
                this.txtFrozenCauseEdit.ReadOnly = true;
                this.chkFrozenLotEdit.Enabled = false;
            }
            if (pageAction == PageActionType.Update)
            {
                this.txtFrozenCauseEdit.ReadOnly = false;
                this.chkFrozenLotEdit.Enabled = true;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.txtFrozenCauseEdit.ReadOnly = true;
                this.chkFrozenLotEdit.Enabled = false;
            }
            if (pageAction == PageActionType.Save)
            {
                this.txtFrozenCauseEdit.ReadOnly = true;
                this.chkFrozenLotEdit.Enabled = false;
            }
        }

        private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        {
        }

        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
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
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            object lot = this._OQCFacade.GetOQCLot(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLotNoQuery.Text)), OQCFacade.Lot_Sequence_Default);
            return lot == null ? 0 : 1;
        }


        private void InitHander()
        {
            this.gridHelper = new GridHelper(this.gridWebGrid);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        public void InitButton()
        {
            this.buttonHelper.AddDeleteConfirm();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("LotNo", "批号", null);
            this.gridHelper.AddDefaultColumn(true, true);

            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (_OQCFacade == null)
                {
                    _OQCFacade = new OQCFacade(base.DataProvider);
                }

                OQCLot lot = this._OQCFacade.CreateNewOQCLot();
                lot.LotFrozen = chkFrozenLotEdit.Checked ? "Y" : "N";

                return lot;
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
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            object obj = this._OQCFacade.GetOQCLot(row.Cells[1].Text.ToString(), OQCFacade.Lot_Sequence_Default);

            if (obj != null)
            {
                return (OQCLot)obj;
            }

            return null;
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblFrozenCauseEdit, txtFrozenCauseEdit, 100, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, languageComponent1);
                return false;
            }
            return true;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtFrozenCauseEdit.Text = string.Empty;
                this.chkFrozenLotEdit.Checked = false;

                return;
            }

            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            object[] frozenRCard = this._OQCFacade.QueryFrozenRCard(((OQCLot)obj).LOTNO);

            object objLot = this._OQCFacade.GetOQCLot(((OQCLot)obj).LOTNO, OQCFacade.Lot_Sequence_Default);

            if (frozenRCard == null)
            {
                if (objLot != null)
                {
                    this.txtFrozenCauseEdit.Text = ((OQCLot)objLot).FrozenReason.Trim();
                }
                else
                {
                    this.txtFrozenCauseEdit.Text = string.Empty;
                }
                this.chkFrozenLotEdit.Checked = true;
            }
            else
            {
                this.txtFrozenCauseEdit.Text = ((Frozen)frozenRCard[0]).FrozenReason.ToString();
                this.chkFrozenLotEdit.Checked = ((OQCLot)obj).LotFrozen == "Y";
            }
        }

        protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{
                    "false",
                    ((OQCLot)obj).LOTNO.ToString(),
                    ""	});
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            object lot = this._OQCFacade.GetOQCLot(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLotNoQuery.Text)), OQCFacade.Lot_Sequence_Default);

            if (lot == null)
                return null;
            else
                return new object[] { lot };
        }

        private string[] FormatExportRecord(object obj)
        {
            return new string[] { ((OQCLot)obj).LOTNO.ToString() };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] { "LotNo" };
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        #endregion
    }
}
