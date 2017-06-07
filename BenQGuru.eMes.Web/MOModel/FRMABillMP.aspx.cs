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
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.RMA;
using BenQGuru.eMES.TS;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.TSModel;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FMOMP 的摘要说明。
    /// </summary>
    public partial class FRMABillMP :BaseMPageMinus
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        //private BenQGuru.eMES.Web.Helper.GridHelper gridHelper = null;
        private System.ComponentModel.IContainer components = null;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        protected global::System.Web.UI.WebControls.TextBox MDateFrom;
        protected global::System.Web.UI.WebControls.TextBox MDateTo;
        private ButtonHelper buttonHelper = null;
        /// <summary>
        /// /// <summary>
        /// 该facade与历史库连接，如果需要与生产库连接，需定义新的facade，不要重置
        /// </summary>
        /// </summary>
        private RMAFacade _facade = null;
        private TSFacade _TSFacade = null;
        private DataCollectFacade _DataCollectFacade = null;
        private MOFacade _MOFacade = null;
        private Report.ReportFacade _ReportFacade = null;
        private TSModelFacade _TSModelFacade =  null;

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

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.InitOnPostBack();

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitUI();
                this.InitWebGrid();
                InitButtonHelp();
            }

        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);


            this.gridHelper = new GridHelperNew(this.gridWebGrid,this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
            new ButtonHelper(this).AddDeleteConfirm();

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

        private string[] FormatExportRecord(object obj)
        {
            RMABill rb = obj as RMABill;


            return new string[]{
                rb.RMABillCode,
                this.languageComponent1.GetString(rb.Status),
                rb.Memo,
                rb.GetDisplayText("MaintainUser"),
                 FormatHelper.ToDateString(rb.MaintainDate),
                 FormatHelper.ToTimeString(rb.MaintainTime)
            };

        }

        private string[] GetColumnHeaderText()
        {
            return new string[] { "RMABill", "Status", "Memo", "MUSER", "MDATE", "MTIME" };
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }
        #endregion

        #region WebGrid
        private void InitWebGrid()
        {
            this.gridHelper.AddColumn("RMABillCode", "RMA单号", null);
            this.gridHelper.AddColumn("Status", "状态", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("MUSER", "维护用户", null);
            this.gridHelper.AddColumn("MDATE", "维护日期", null);
            this.gridHelper.AddColumn("MTIME", "维护时间", null);
            this.gridHelper.AddLinkColumn("Detail", "详细", null);
            this.gridHelper.AddDefaultColumn(true, true);



            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected DataRow GetGridRow(object obj)
        {
            RMABill rb = obj as RMABill;

            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //                    false,								
            //                    rb.RMABillCode.ToString(),
            //                    this.languageComponent1.GetString( rb.Status.ToString() ),
            //                    rb.Memo,
            //                    rb.GetDisplayText("MaintainUser"),							
            //                     FormatHelper.ToDateString(rb.MaintainDate),
            //                     FormatHelper.ToTimeString(rb.MaintainTime)
            //                });
            DataRow row = this.DtSource.NewRow();
            row["RMABillCode"] = rb.RMABillCode.ToString();
            row["Status"] = this.languageComponent1.GetString(rb.Status.ToString());
            row["Memo"] = rb.Memo;
            row["MUSER"] = rb.GetDisplayText("MaintainUser");
            row["MDATE"] = FormatHelper.ToDateString(rb.MaintainDate);
            row["MTIME"] = FormatHelper.ToTimeString(rb.MaintainTime);
            return row;

        }

        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new FacadeFactory(base.DataProvider).CreateRMAFacade();
            }
            return _facade.QueryRMABill(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRMABillQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemQuery.Text)),
                FormatHelper.CleanString(this.txtCusCodeQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModelQuery.Text)),
                FormatHelper.CleanString(this.txtSubsidiaryCompanyQuery.Text),
                FormatHelper.TODateInt(this.MDateFrom.Text),
                FormatHelper.TODateInt(this.MDateTo.Text),
                inclusive, exclusive);
        }

        private int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new FacadeFactory(base.DataProvider).CreateRMAFacade();
            }
            return _facade.QueryRMABillCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRMABillQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemQuery.Text)),
                FormatHelper.CleanString(this.txtCusCodeQuery.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModelQuery.Text)),
                FormatHelper.CleanString(this.txtSubsidiaryCompanyQuery.Text),
                FormatHelper.TODateInt(this.MDateFrom.Text),
                FormatHelper.TODateInt(this.MDateTo.Text));
        }

        #endregion

        #region Button


        protected void cmdDelete_ServerClick(object sender, System.EventArgs e)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            //已经关闭或结案的RMA不能新增行项目

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    RMABill rmaBill = (RMABill)this.GetEditObject(row);
                    if (rmaBill != null)
                    {
                        if (rmaBill.Status != RMABillStatus.Initial)
                        {
                            WebInfoPublish.Publish(this, "$BS_RMABillStatus_CannotDelete", this.languageComponent1);
                            this.RequestData();
                            return;
                        }
                        items.Add(rmaBill);
                    }
                }
                _facade.DeleteRMABill((RMABill[])items.ToArray(typeof(RMABill)));

            }

            this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
            this.RequestData();

        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected void cmdAdd_ServerClick(object sender, EventArgs e)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            //新增前唯一性检查

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            object obj = this.GetEditObject();
            if (obj != null)
            {
                ((RMABill)obj).Status = RMABillStatus.Initial;
                this._facade.AddRMABill((RMABill)obj);
            }
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        protected void cmdDistribution_ServerClick(object sender, EventArgs e)
        {

            /* 下发的时候需要 */
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    RMABill rmaBill = (RMABill)this.GetEditObject(row);
                    if (rmaBill != null)
                    {
                        if (rmaBill.Status != RMABillStatus.Initial)
                        {
                            WebInfoPublish.PublishInfo(this, "$BS_RMABillStatus_CannotOpened $RMABillCode:" + rmaBill.RMABillCode, this.languageComponent1);
                            return;
                        }

                        object[] objs = _facade.QueryRMADetail(rmaBill.RMABillCode);
                        if (objs == null || objs.Length == 0)
                        {
                            WebInfoPublish.PublishInfo(this, "$BS_RMABillDetail_NOT_EXIST $RMABillCode:" + rmaBill.RMABillCode, this.languageComponent1);
                            return;
                        }

                        items.Add(rmaBill);
                    }
                }

                if (_TSFacade == null)
                {
                    _TSFacade = new TSFacade(this.DataProvider);
                }
                //放在事务中处理
                this.DataProvider.BeginTransaction();

                foreach (RMABill rmabill in items)
                {
                    // Update RMA Status
                    rmabill.Status = RMABillStatus.Opened;
                    _facade.UpdateRMABill(rmabill);

                    object[] objs = _facade.QueryRMADetail(rmabill.RMABillCode);
                    if (objs != null || objs.Length != 0)
                    {
                        foreach (RMADetial detial in objs)
                        {
                            //如果该序列号的处理方式为维修的，需要往TBLTS和TBLTSERRORCODE插入数据
                            if (detial.Handelcode == "ts")
                            {
                                //Insert TS 
                                Domain.TS.TS ts = this.GetTS(detial);
                                _TSFacade.AddTS(ts);

                                Domain.TS.TSErrorCode tsErrorCode = this.GetTSErrorCode(detial,ts);
                                _TSFacade.AddTSErrorCode(tsErrorCode);

                            }
                        }
                    }
                }

                this.DataProvider.CommitTransaction();
            }
            //InitViewPanel();

            this.RequestData();
        }

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {

            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            //新增前唯一性检查

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            object obj = this.GetEditObject();
            if (obj != null)
            {
                //((RMABill)obj).Status = RMABillStatus.Initial;
                this._facade.UpdateRMABill((RMABill)obj);
            }
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Save);


        }

        protected void cmdClose_ServerClick(object sender, EventArgs e)
        {

            /* 下发的时候需要 */
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            if (_TSFacade == null)
            {
                _TSFacade = new TSFacade(this.DataProvider);
            }
            if(_DataCollectFacade == null)
            {
                _DataCollectFacade = new DataCollectFacade(this.DataProvider);
            }

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList items = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    RMABill rmaBill = (RMABill)this.GetEditObject(row);
                    if (rmaBill != null)
                    {
                        if (rmaBill.Status != RMABillStatus.Opened)
                        {
                            WebInfoPublish.PublishInfo(this, "$BS_RMABillStatus_CannotClose $RMABillCode:" + rmaBill.RMABillCode, this.languageComponent1);
                            return;
                        }

                        object[] objs = _facade.QueryRMADetail(rmaBill.RMABillCode);
                        if (objs == null || objs.Length == 0)
                        {
                            WebInfoPublish.PublishInfo(this, "$BS_RMABillDetail_NOT_EXIST $RMABillCode:" + rmaBill.RMABillCode, this.languageComponent1);
                            return;
                        }
                        foreach (RMADetial rmaDetial in objs)
                        {
                            if (rmaDetial.Handelcode == "ts")
                            {
                                object objTs = _TSFacade.QueryLastTSByRunningCard(rmaDetial.Rcard);
                                if(objTs != null)
                                {
                                    //处理方式为维修的，则序列号必须为维修完成(tblts.tsstatus_complete)或者报废（tblts.tsstatus_scrap）
                                    if (!((objTs as Domain.TS.TS).TSStatus == TSStatus.TSStatus_Complete || (objTs as Domain.TS.TS).TSStatus == TSStatus.TSStatus_Scrap))
                                    {
                                        WebInfoPublish.PublishInfo(this, "$RCAR_MUST_TSCOMPLETE_TSSCRAP $RCARD:" + rmaDetial.Rcard, this.languageComponent1);
                                        return;
                                    }
                                }
                            }
                            else if (rmaDetial.Handelcode == "rework")
                            {
                                object objsimRepot = _DataCollectFacade.GetLastSimulationReportByRMA(rmaDetial.Rcard, rmaDetial.Rmabillcode);
                                if (objsimRepot != null)
                                {
                                    if ((objsimRepot as Domain.DataCollect.SimulationReport).IsComplete != "1")
                                    {
                                        WebInfoPublish.PublishInfo(this, "$CS_RCARD_IS_NOT_FINISHED $RCARD:" + rmaDetial.Rcard, this.languageComponent1);
                                        return;
                                    }
                                }
                                else
                                {
                                    //还未做返工
                                    WebInfoPublish.PublishInfo(this, "$RCARD_NOT_DO_REWORK $RCARD:" + rmaDetial.Rcard, this.languageComponent1);
                                    return;
                                }
                            }
                        }

                        items.Add(rmaBill);
                    }
                }


                //放在事务中处理
                this.DataProvider.BeginTransaction();

                foreach (RMABill rmabill in items)
                {
                    // Update RMA Status
                    rmabill.Status = RMABillStatus.Closed;
                    _facade.UpdateRMABill(rmabill);

                }

                this.DataProvider.CommitTransaction();

                RequestData();
            }
        }

        private void RequestData()
        {
            // 2005-04-06
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        //protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
        //{
        //    if (this.chbSelectAll.Checked)
        //    {
        //        this.gridHelper.CheckAllRows(CheckStatus.Checked);
        //    }
        //    else
        //    {
        //        this.gridHelper.CheckAllRows(CheckStatus.Unchecked);
        //    }
        //}


        protected void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Update)
            {
                this.txtRMABillEidt.ReadOnly = true;
            }
            if (pageAction == PageActionType.Add)
            {
                this.txtRMABillEidt.ReadOnly = false;
            }
        }


        #endregion

        #region Object <--> Page


        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblRMABillCode, txtRMABillEidt, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.PublishInfo(this, manager.CheckMessage, languageComponent1);
                return false;
            }

            return true;
        }

        private object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            if (this.ValidateInput())
            {
                object obj = (RMABill)this._facade.GetRMABill(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRMABillEidt.Text)));
                RMABill rmaBill = (RMABill)obj;
                if (obj != null)
                {
                    rmaBill.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 200);
                    rmaBill.MaintainUser = this.GetUserCode();
                }
                else
                {
                    rmaBill = new RMABill();
                    rmaBill.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 200);
                    rmaBill.RMABillCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtRMABillEidt.Text,40));
                    rmaBill.MaintainUser = this.GetUserCode();
                }


                return rmaBill;

            }
            else
            {
                return null;
            }
        }


        private object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            object obj = this._facade.GetRMABill(row.Items.FindItemByKey("RMABillCode").Text);

            if (obj != null)
            {
                return (RMABill)obj;
            }

            return null;
        }

        private void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtRMABillEidt.Text = string.Empty;
                this.txtMemoEdit.Text = string.Empty;

                return;
            }

            this.txtRMABillEidt.Text = ((RMABill)obj).RMABillCode;
            this.txtMemoEdit.Text = ((RMABill)obj).Memo;

        }

        private BenQGuru.eMES.Domain.TS.TS GetTS(RMADetial rmadetial)
        {
            BenQGuru.eMES.Domain.TS.TS itemTs = null;

            itemTs = new BenQGuru.eMES.Domain.TS.TS();
            if (_DataCollectFacade == null)
            {
                _DataCollectFacade = new DataCollectFacade(this.DataProvider);
            }

            itemTs.MOCode = "";

            itemTs.RMABillCode = rmadetial.Rmabillcode;
            itemTs.RunningCard = rmadetial.Rcard;
            itemTs.RunningCardSequence = 0;
            itemTs.TSId = FormatHelper.GetUniqueID("", itemTs.RunningCard, itemTs.RunningCardSequence.ToString());
            itemTs.TranslateCard = rmadetial.Rcard;
            itemTs.TranslateCardSequence = 0;
            itemTs.CardType = CardType.CardType_Product;
            itemTs.TSStatus = TSStatus.TSStatus_New;
            itemTs.SourceCardSequence = 0;
            itemTs.MaintainUser = this.GetUserCode();
            itemTs.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
            itemTs.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            itemTs.TSDate = 0;
            itemTs.TSTime = 0;
            itemTs.FromInputType = TSFacade.TSSource_RMA;
            itemTs.FromUser = itemTs.MaintainUser;
            itemTs.FromDate = itemTs.MaintainDate;
            itemTs.FormTime = itemTs.MaintainTime;
            itemTs.FromOPCode = " ";
            itemTs.FromResourceCode = " ";
            itemTs.FromRouteCode = " ";
            itemTs.FromSegmentCode = " ";
            itemTs.FromShiftCode = " ";
            itemTs.FromShiftDay = FormatHelper.TODateInt(DateTime.Now);
            itemTs.FromShiftTypeCode = " ";
            itemTs.FromStepSequenceCode = " ";
            itemTs.FromTimePeriodCode = " ";
            itemTs.FromSegmentCode = " ";
            itemTs.TSTimes = _TSFacade.GetMaxTSTimes(rmadetial.Rcard) + 1;
            itemTs.ItemCode = rmadetial.Itemcode;
            itemTs.ModelCode = rmadetial.Modelcode;
            itemTs.TransactionStatus = " ";
          

            if (_ReportFacade == null)
            {
                _ReportFacade = new BenQGuru.eMES.Report.ReportFacade(this.DataProvider);
            }
            object obmTimeDimension = _ReportFacade.GetTimeDimension(itemTs.FromShiftDay);
            if (obmTimeDimension != null)
            {
                itemTs.Week = (obmTimeDimension as Domain.Report.TimeDimension).Week;
                itemTs.Month = (obmTimeDimension as Domain.Report.TimeDimension).Month;
            }
            else
            {
                itemTs.Week = 0;
                itemTs.Month = 0;
            }

            if(_MOFacade == null)
            {
                _MOFacade = new MOFacade(this.DataProvider);
            }
            object objmo = _MOFacade.GetMO(rmadetial.Remocode);
            if(objmo == null)
            {
                itemTs.MOSeq = 0;
            }
            else
            {
                itemTs.MOSeq = (objmo as MO).MOSeq;
            }
            return itemTs;
        }

        private BenQGuru.eMES.Domain.TS.TSErrorCode GetTSErrorCode(RMADetial rmadetial, Domain.TS.TS ts)
        {
            if(_TSModelFacade == null)
            {
                _TSModelFacade = new TSModelFacade(this.DataProvider);
            }
            object objEcg = _TSModelFacade.GetErrorCodeGroup2ErrorCodeByecCode(rmadetial.Errorcode);


            Domain.TS.TSErrorCode tsErrorCode = new Domain.TS.TSErrorCode();
            tsErrorCode.ErrorCode = rmadetial.Errorcode;
            if (objEcg != null)
            {
                tsErrorCode.ErrorCodeGroup = (objEcg as Domain.TSModel.ErrorCodeGroup2ErrorCode).ErrorCodeGroup;
            }
            else
            {
                tsErrorCode.ErrorCodeGroup = " ";
            }
            tsErrorCode.ItemCode = rmadetial.Itemcode;
            tsErrorCode.TSId = ts.TSId;
            tsErrorCode.RunningCard = rmadetial.Rcard;
            tsErrorCode.RunningCardSequence = ts.RunningCardSequence;
            tsErrorCode.ModelCode = ts.ModelCode;
            tsErrorCode.MOCode = "";
            tsErrorCode.MaintainUser = this.GetUserCode();
            tsErrorCode.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
            tsErrorCode.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
            return tsErrorCode;
        }

        #endregion

        #region form event
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.SetEditObject(obj);

                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                    buttonHelper_AfterPageStatusChangeHandle(PageActionType.Cancel);
                }
            }

            if (commandName == "Detail")
            {
                Response.Redirect(this.MakeRedirectUrl("FRMABillEP.aspx", new string[] { "RMABillCode" }, new string[] {row.Items.FindItemByKey("RMABillCode").Text.Trim() }));
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        #endregion

    }

}
