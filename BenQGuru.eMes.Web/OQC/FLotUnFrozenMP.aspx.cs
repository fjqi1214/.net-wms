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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.OQC
{
    /// <summary>
    /// FUnfrozenMP 的摘要说明。
    /// </summary>
    public partial class FLotUnfrozenMP : BaseMPageMinus
    {
        private System.ComponentModel.IContainer components;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        protected global::System.Web.UI.WebControls.TextBox txtDateFrom;
        protected global::System.Web.UI.WebControls.TextBox txtDateTo;

        //private GridHelper gridHelper = null;
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
            //this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            // 
            // languageComponent1
            // 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
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
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);

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

                DBDateTime currentDBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                foreach (GridRecord row in array)
                {
                    OQCLot OQClot = (OQCLot)this.GetEditObject(row);

                    if (OQClot != null)
                    {
                        //更新Lot
                        _OQCFacade.UpdateUnFrozenOnLot(((OQCLot)OQClot).LOTNO, unfrozenReason, currentDBDateTime.DBDate,
                                                     currentDBDateTime.DBTime, userCode, OQCFacade.Lot_Sequence_Default);
                        // 更新frozen
                        _OQCFacade.UnFreezeFrozen(((OQCLot)OQClot).LOTNO, unfrozenReason, currentDBDateTime.DBDate,
                                                     currentDBDateTime.DBTime, userCode, OQCFacade.Lot_Sequence_Default);
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
            this.gridHelper.AddColumn("OQCLotNo", "批号", null);
            this.gridHelper.AddColumn("stepsequence", "产线", null);
            this.gridHelper.AddColumn("FrosenCount", "隔离总数", null);
            this.gridHelper.AddColumn("MoCode", "工单", null);
            this.gridHelper.AddColumn("ItemCode", "产品", null);
            this.gridHelper.AddColumn("ItemDesc", "产品描述", null);
            this.gridHelper.AddColumn("MmodelCode", "机型", null);
            this.gridHelper.AddColumn("FrosenReason", "隔离原因", null);
            this.gridHelper.AddColumn("FrosenDate", "隔离日期", null);
            this.gridHelper.AddColumn("FrosenTime", "隔离时间", null);
            this.gridHelper.AddColumn("FrosenUser", "隔离人", null);
            this.gridHelper.AddLinkColumn("ItemCodeDetail", "产品明细", null);

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

        private object GetEditObject(GridRecord row)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(this.DataProvider);
            }
            object obj = this._OQCFacade.GetOQCLot(row.Items.FindItemByKey("OQCLotNo").Value.ToString(), OQCFacade.Lot_Sequence_Default);
            return obj;
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblUnfrozenCauseEdit, txtUnfrozenCauseEdit, 100, true));
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

        protected DataRow GetGridRow(object obj)
        {
            OQCLOTAndMaterial oqcLotAndMaterial = (OQCLOTAndMaterial)obj;

            DataRow row = this.DtSource.NewRow();
            row["OQCLotNo"] = oqcLotAndMaterial.LOTNO;
            row["stepsequence"] = oqcLotAndMaterial.SSCode;
            row["FrosenCount"] = oqcLotAndMaterial.LotSize;
            row["MoCode"] = oqcLotAndMaterial.MoCdoe;
            row["ItemCode"] = oqcLotAndMaterial.ItemCode;
            row["ItemDesc"] = oqcLotAndMaterial.MaterialDesc;
            row["MmodelCode"] = oqcLotAndMaterial.MmodelCode;
            row["FrosenReason"] = oqcLotAndMaterial.FrozenReason;
            row["FrosenDate"] = FormatHelper.ToDateString(oqcLotAndMaterial.FrozenDate);
            row["FrosenTime"] = FormatHelper.ToTimeString(oqcLotAndMaterial.FrozenTime);
            row["FrosenUser"] = ((OQCLOTAndMaterial)obj).GetDisplayText("FrozenBy");
            row["ItemCodeDetail"] = "";
            return row;
        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(this.DataProvider);
            }
            string LotNo = this.txtLotNoQuery.Text.ToString().Trim();
            string Stepsequence = this.txtStepsequence.Text.Trim();
            Stepsequence = Stepsequence.Replace(",", "','");
            string Mmodelcode = this.txtModelcode.Text.Trim();
            Mmodelcode = Mmodelcode.Replace(",", "','");
            string BigLine = this.txtBIGLine.Text.Trim();
            BigLine = BigLine.Replace(",", "','");
            int frozenDateStart = FormatHelper.TODateInt(this.txtDateFrom.Text);
            int frozenDateEnd = FormatHelper.TODateInt(this.txtDateTo.Text);

            object[] objs = this._OQCFacade.QueryFrozenAndMaterial(LotNo, Mmodelcode, Stepsequence, BigLine, frozenDateStart, frozenDateEnd, -1, -1, FrozenStatus.STATUS_FRONZEN, inclusive, exclusive);

            if (objs != null && objs.Length > 0)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    object[] MoObjs = _OQCFacade.QueryMoCodeINOLot2CardByLotNo(((OQCLOTAndMaterial)objs[i]).LOTNO);
                    if (MoObjs != null
                        && MoObjs.Length > 0)
                    {
                        for (int j = 0; j < MoObjs.Length; j++)
                        {
                            if (j == 0)
                            {
                                ((OQCLOTAndMaterial)objs[i]).MoCdoe = ((OQCLot2Card)MoObjs[j]).MOCode;
                            }
                            else
                            {
                                ((OQCLOTAndMaterial)objs[i]).MoCdoe = ((OQCLOTAndMaterial)objs[i]).MoCdoe + ", " + ((OQCLot2Card)MoObjs[j]).MOCode;
                            }
                        }
                    }
                }
            }
            return objs;
        }

        private int GetRowCount()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(this.DataProvider);
            }

            string LotNo = this.txtLotNoQuery.Text.ToString().Trim();
            string Stepsequence = this.txtStepsequence.Text.Trim();
            Stepsequence = Stepsequence.Replace(",", "','");
            string Mmodelcode = this.txtModelcode.Text.Trim();
            Mmodelcode = Mmodelcode.Replace(",", "','");
            string BigLine = this.txtBIGLine.Text.Trim();
            BigLine = BigLine.Replace(",", "','");
            int frozenDateStart = FormatHelper.TODateInt(this.txtDateFrom.Text);
            int frozenDateEnd = FormatHelper.TODateInt(this.txtDateTo.Text);
            return this._OQCFacade.QueryFrozenAndMaterialCount(LotNo, Mmodelcode, Stepsequence,
                                                            BigLine, frozenDateStart, frozenDateEnd, -1, -1, FrozenStatus.STATUS_FRONZEN);
        }

        private string[] FormatExportRecord(object obj)
        {
            OQCLOTAndMaterial frozen = (OQCLOTAndMaterial)obj;
            return new string[]{
                frozen.LOTNO,
                frozen.SSCode,
                frozen.LotSize.ToString(),
                frozen.MoCdoe,
                frozen.ItemCode,
                frozen.MaterialDesc,
                frozen.MmodelCode,                
                frozen.FrozenReason,
                FormatHelper.ToDateString(frozen.FrozenDate),
                FormatHelper.ToTimeString(frozen.FrozenTime),
                //frozen.FrozenBy
               ((OQCLOTAndMaterial)obj).GetDisplayText("FrozenBy")
            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                "OQCLotNo",
                "sscode",
                "FrozenCount",
                "MOCode",
                "ItemCode",
                "ItemDesc",
                "Modelcode",
                "FrosenReason",
                "FrosenDate",
                "FrosenTime",
                "FrosenUser"
            };
        }
        #endregion

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "ItemCodeDetail")
            {
                Response.Redirect(this.MakeRedirectUrl("FFrozenQP.aspx", new string[] { "lotno" }, new string[] { row.Items.FindItemByKey("OQCLotNo").Value.ToString() }));
            }
        }
    }
}
