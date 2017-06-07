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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.OQC
{
    /// <summary>
    /// FFrozenQP 的摘要说明。
    /// </summary>
    public partial class FLotFrozenQueryMP : BaseMPageMinus
    {
        private System.ComponentModel.IContainer components;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

        private OQCFacade _OQCFacade = null;


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
                Initparameters();
                // 初始化界面UI
                this.InitUI();
                InitButtonHelp();
                SetEditObject(null);
                this.InitWebGrid();
            }
        }

        private void Initparameters()
        {
            if (this.Request.Params["lotno"] == null)
            {
                this.txtLotNoQuery.Text = string.Empty;
                //ExceptionManager.Raise(this.GetType().BaseType, "$Error_RequestUrlParameter_Lost");
            }
            else
            {
                this.txtLotNoQuery.Text = this.Request.Params["lotno"];
            }
        }
        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            if (!ValidateQueryInput())
                return;

            RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
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
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("OQCLotNo", "批号", null);
            this.gridHelper.AddColumn("stepsequence", "产线", null);
            this.gridHelper.AddColumn("FrosenCount", "隔离总数", null);
            this.gridHelper.AddColumn("MoCode", "工单", null);
            this.gridHelper.AddColumn("ItemCode", "产品", null);
            this.gridHelper.AddColumn("MmodelCode", "机型", null);
            this.gridHelper.AddColumn("FrosenReason", "隔离原因", null);
            this.gridHelper.AddColumn("FrosenDate", "隔离日期", null);
            this.gridHelper.AddColumn("FrosenTime", "隔离时间", null);
            this.gridHelper.AddColumn("FrosenUser", "隔离人", null);
            this.gridHelper.AddColumn("UnfrosenReason", "取消隔离原因", null);
            this.gridHelper.AddColumn("UnfrosenDate", "取消隔离日期", null);
            this.gridHelper.AddColumn("UnfrosenTime", "取消隔离时间", null);
            this.gridHelper.AddColumn("UnfrosenUser", "取消隔离人", null);
            this.gridHelper.AddLinkColumn("ItemCodeDetail", "产品明细", null);

            this.gridHelper.AddDefaultColumn(false, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private object GetEditObject()
        {
            if (!this.ValidateInput())
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
            return null;
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

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

            DataRow row = DtSource.NewRow();
            row["OQCLotNo"] = oqcLotAndMaterial.LOTNO;
            row["stepsequence"] = oqcLotAndMaterial.GetDisplayText("SSCode");
            row["FrosenCount"] = oqcLotAndMaterial.LotSize;
            row["MoCode"] = oqcLotAndMaterial.MoCdoe;
            row["ItemCode"] = oqcLotAndMaterial.GetDisplayText("ItemCode");
            row["MmodelCode"] = oqcLotAndMaterial.MmodelCode;
            row["FrosenReason"] = oqcLotAndMaterial.FrozenReason;
            row["FrosenDate"] = oqcLotAndMaterial.FrozenDate > 0 ? FormatHelper.ToDateString(oqcLotAndMaterial.FrozenDate) : "";
            row["FrosenTime"] = oqcLotAndMaterial.FrozenDate > 0 ? FormatHelper.ToTimeString(oqcLotAndMaterial.FrozenTime) : "";
            row["FrosenUser"] = ((OQCLOTAndMaterial)obj).GetDisplayText("FrozenBy");
            row["UnfrosenReason"] = oqcLotAndMaterial.UnFrozenReason;
            row["UnfrosenDate"] = oqcLotAndMaterial.UnFrozenDate > 0 ? FormatHelper.ToDateString(oqcLotAndMaterial.UnFrozenDate) : "";
            row["UnfrosenTime"] = oqcLotAndMaterial.UnFrozenTime > 0 ? FormatHelper.ToTimeString(oqcLotAndMaterial.UnFrozenTime) : "";
            row["UnfrosenUser"] = ((OQCLOTAndMaterial)obj).GetDisplayText("UnFrozenBy");
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

            string frozenStatus = string.Empty;
            int frozenDateStart = FormatHelper.TODateInt(this.txtFrozenDateStartQuery.Text);
            int frozenDateEnd = FormatHelper.TODateInt(this.txtFrozenDateEndQuery.Text);
            int unfrozenDateStart = FormatHelper.TODateInt(this.txtUnfrozenDateStartQuery.Text);
            int unfrozenDateEnd = FormatHelper.TODateInt(this.txtUnfrozenDateEndQuery.Text);


            object[] objs = this._OQCFacade.QueryFrozenAndMaterial(LotNo, Mmodelcode, Stepsequence, BigLine,
                                frozenDateStart, frozenDateEnd, unfrozenDateStart, unfrozenDateEnd, FrozenStatus.STATUS_FRONZEN + "," + FrozenStatus.STATUS_UNFRONZEN, inclusive, exclusive);
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

            int frozenDateStart = FormatHelper.TODateInt(this.txtFrozenDateStartQuery.Text);
            int frozenDateEnd = FormatHelper.TODateInt(this.txtFrozenDateEndQuery.Text);
            int unfrozenDateStart = FormatHelper.TODateInt(this.txtUnfrozenDateStartQuery.Text);
            int unfrozenDateEnd = FormatHelper.TODateInt(this.txtUnfrozenDateEndQuery.Text);
            return this._OQCFacade.QueryFrozenAndMaterialCount(LotNo, Mmodelcode, Stepsequence,
                                                            BigLine, frozenDateStart, frozenDateEnd, unfrozenDateStart, unfrozenDateEnd, FrozenStatus.STATUS_FRONZEN + "," + FrozenStatus.STATUS_UNFRONZEN);
        }

        private string[] FormatExportRecord(object obj)
        {
            OQCLOTAndMaterial oqcLotAndMaterial = (OQCLOTAndMaterial)obj;
            return new string[]{                    
					oqcLotAndMaterial.LOTNO,
					oqcLotAndMaterial.GetDisplayText("SSCode"),
					oqcLotAndMaterial.LotSize.ToString().Trim(),
					oqcLotAndMaterial.MoCdoe,
					oqcLotAndMaterial.GetDisplayText("ItemCode"),
                    oqcLotAndMaterial.MmodelCode,
                    oqcLotAndMaterial.FrozenReason,
                    oqcLotAndMaterial.FrozenDate > 0 ? FormatHelper.ToDateString(oqcLotAndMaterial.FrozenDate) : "",
                    oqcLotAndMaterial.FrozenDate > 0 ? FormatHelper.ToTimeString(oqcLotAndMaterial.FrozenTime) : "",
                    oqcLotAndMaterial.FrozenBy,
                    oqcLotAndMaterial.UnFrozenReason,
                    oqcLotAndMaterial.UnFrozenDate > 0 ? FormatHelper.ToDateString(oqcLotAndMaterial.UnFrozenDate) : "",
                    oqcLotAndMaterial.UnFrozenTime > 0 ? FormatHelper.ToTimeString(oqcLotAndMaterial.UnFrozenTime) : "",
                    oqcLotAndMaterial.UnFrozenBy
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
                "Modelcode",
                "FrosenReason",
                "FrosenDate",
                "FrosenTime",
                "FrosenUser",
                "UnfrosenReason",
                "UnfrosenDate",
                "UnfrosenTime",
                "UnfrosenUser"
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
