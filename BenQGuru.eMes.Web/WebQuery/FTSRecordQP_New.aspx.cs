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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;
using BenQGuru.eMES.DataCollect;
using Infragistics.Web.UI.GridControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    public partial class FTSRecordQP_New : BaseQPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected ExcelExporter excelExporter = null;
        protected WebQueryHelperNew _helper = null;

       // protected GridHelper gridHelper = null;

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);

            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);

            this._helper = new WebQueryHelperNew(this.cmdQuery, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1,DtSource);
            this._helper.LoadGridDataSource += new EventHandler(_helper_LoadGridDataSource);
            this._helper.DomainObjectToGridRow += new EventHandler(_helper_DomainObjectToGridRow);
            this._helper.DomainObjectToExportRow += new EventHandler(_helper_DomainObjectToExportRow);
            this._helper.GetExportHeadText += new EventHandler(_helper_GetExportHeadText);
            //this._helper.GridCellClick += new EventHandler(_helper_GridCellClick);
            this.txtSegmentCodeQuery.TextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this._initialWebGrid();

                TSStatus tsstatus = new TSStatus();
                tsstatus.Items.Remove(TSStatus.TSStatus_Reflow);
                tsstatus.Items.Remove(TSStatus.TSStatus_RepeatNG);
                tsstatus.Items.Remove(TSStatus.TSStatus_OffMo);

                new CheckBoxListBuilder(tsstatus, this.chkTSStateList, this.languageComponent1).Build();

                this.dateStartDateQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(System.DateTime.Now));
                this.dateEndDateQuery.Text = this.dateStartDateQuery.Text;
            }
            CheckBoxListBuilder.FormatListControlStyle(this.chkTSStateList, 80);
            FormatHelper.SetSNRangeValue(txtStartSnQuery, txtEndSnQuery);

            this.InitQueryText();
        }

        void TextBox_TextChanged(object sender, EventArgs e)
        {
            this.txtStepSequence.Segment = this.txtSegmentCodeQuery.Text.Trim().ToUpper();
        }

        #region 初始化查询栏位
        private void InitQueryText() 
        {
            this.lblMaterialModelCodeQuery.Visible = false;
            this.txtMaterialModelCodeQuery.Visible = false;
            this.lblMaterialMachineTypeGroup.Visible = false;
            this.txtMaterialMachineTypeGroup.Visible = false;
        }
        #endregion

        private void _initialWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("Ts_SN", "序列号", null);
            this.gridHelper.AddColumn("Ts_RCard", "序列号", null);
            this.gridHelper.AddColumn("Ts_SNSeq", "序列号顺序", null);
            this.gridHelper.AddColumn("Ts_Count", "第N次不良", null);
            this.gridHelper.AddColumn("Ts_TsState", "维修状态", null);
            this.gridHelper.AddLinkColumn("Ts_Details", "维修信息", null);
            this.gridHelper.AddLinkColumn("Ts_ChangedItemDetails", "换料信息", null);
            this.gridHelper.AddLinkColumn("Ts_ChangedItemDetailsSMT", "SMT换料", null);
            this.gridHelper.AddColumn("Ts_ModelCode", "产品别", null);
            this.gridHelper.AddColumn("Ts_ItemCode", "产品", null);
            this.gridHelper.AddColumn("Ts_ItemDesc", "产品描述", null);
            // Add 2 columns named '整机机型'和'BOM版本'
            this.gridHelper.AddColumn("Ts_Model", "整机机型", null);
            this.gridHelper.AddColumn("Ts_BOMVersion", "BOM版本", null);

            this.gridHelper.AddColumn("Ts_MoCode", "工单", null);
            this.gridHelper.AddColumn("Ts_NGDate", "不良日期", null);
            this.gridHelper.AddColumn("Ts_NGTime", "不良时间", null);
            this.gridHelper.AddColumn("Ts_SourceResource", "来源站", null);
            this.gridHelper.AddColumn("Ts_FrmUser", "操作工", null);
            this.gridHelper.AddColumn("Ts_FrmMemo", "送修备注", null);
            this.gridHelper.AddColumn("Ts_ConfirmDate", "接收日期", null);
            this.gridHelper.AddColumn("Ts_ConfirmTime", "接收时间", null);
            this.gridHelper.AddColumn("Ts_ConfirmResource", "接收站", null);
            this.gridHelper.AddColumn("Ts_ConfirmUser", "接收人", null);
            this.gridHelper.AddColumn("Ts_SouceResourceDate", "维修日期", null);
            this.gridHelper.AddColumn("Ts_SouceResourceTime", "维修时间", null);
            this.gridHelper.AddColumn("Ts_RepaireResource", "维修站", null);
            this.gridHelper.AddColumn("Ts_TSUser", "维修工", null);
            this.gridHelper.AddColumn("Ts_DestOpCode", "去向工序", null);
            this.gridHelper.AddColumn("Ts_ScrapCause", "包含原因", null);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridWebGrid.Columns.FromKey("Ts_ItemDesc").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Ts_RCard").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Ts_SNSeq").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Ts_ScrapCause").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Ts_ChangedItemDetails").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Ts_ChangedItemDetailsSMT").Hidden = true;
            ((BoundDataField)this.gridWebGrid.Columns.FromKey("Ts_SN")).HtmlEncode = false;
        }


        protected override void Grid_ClickCell(GridRecord row, string command)
        {

            if (command == "Ts_Details")
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FTSRecordDetailsQP.aspx",
                    new string[]{
									"ModelCode",
									"ItemCode",
									"MoCode",
									"RunningCard",
									"RunningCardSeq",
									"TSState",
									"TSResourceCode",
									"TSDate",
									"TSTime",
									"BackUrl",
									"ScrapCause"
								},
                    new string[]{
									row.Items.FindItemByKey("Ts_ModelCode").Text,
									row.Items.FindItemByKey("Ts_ItemCode").Text,
									row.Items.FindItemByKey("Ts_MoCode").Text,
									row.Items.FindItemByKey("Ts_RCard").Text,
									row.Items.FindItemByKey("Ts_SNSeq").Text,
									row.Items.FindItemByKey("Ts_TsState").Text,
									row.Items.FindItemByKey("Ts_RepaireResource").Text,
									row.Items.FindItemByKey("Ts_NGDate").Text,
									row.Items.FindItemByKey("Ts_NGTime").Text,
									"FTSRecordQP_New.aspx",
									row.Items.FindItemByKey("Ts_ScrapCause").Text
								})
                    );
            }
            else if (command == "Ts_ChangedItemDetails")
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FTSChangedItemQP.aspx",
                    new string[]{
									"ModelCode",
									"ItemCode",
									"MoCode",
									"RunningCard",
									"TSState",
									"TSResourceCode",
									"TSDate",
									"TSTime",
									"BackUrl"
								},
                    new string[]{
									row.Items.FindItemByKey("Ts_ModelCode").Text,
									row.Items.FindItemByKey("Ts_ItemCode").Text,
									row.Items.FindItemByKey("Ts_MoCode").Text,
									row.Items.FindItemByKey("Ts_RCard").Text,
									row.Items.FindItemByKey("Ts_TsState").Text,
									row.Items.FindItemByKey("Ts_RepaireResource").Text,
									row.Items.FindItemByKey("Ts_NGDate").Text,
									row.Items.FindItemByKey("Ts_NGTime").Text,
									"FTSRecordQP_New.aspx"
								})
                    );
            }
            else if (command == "Ts_ChangedItemDetailsSMT")
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FTSChangedItemSMTQP.aspx",
                    new string[]{
									"ModelCode",
									"ItemCode",
									"MoCode",
									"RunningCard",
									"TSState",
									"TSResourceCode",
									"TSDate",
									"TSTime",
									"BackUrl"
								},
                    new string[]{
									row.Items.FindItemByKey("Ts_ModelCode").Text,
									row.Items.FindItemByKey("Ts_ItemCode").Text,
									row.Items.FindItemByKey("Ts_MoCode").Text,
									row.Items.FindItemByKey("Ts_RCard").Text,
									row.Items.FindItemByKey("Ts_TsState").Text,
									row.Items.FindItemByKey("Ts_RepaireResource").Text,
									row.Items.FindItemByKey("Ts_NGDate").Text,
									row.Items.FindItemByKey("Ts_NGTime").Text,
									"FTSRecordQP_New.aspx"
								})
                    );
            }
        }

        void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            if (chbRepairDetail.Checked)
            {
                (e as ExportHeadEventArgsNew).Heads =
                    new string[]{
									"Ts_SN",
									"Ts_Count",
									"Ts_TsState",
									"Ts_ModelCode",
									"Ts_ItemCode",
                                    //"Ts_ItemDesc",
                                    "Ts_Model",
                                    "Ts_BOMVersion",
									"Ts_MoCode",

									"Ts_NGDate",
									"Ts_NGTime",
									"Ts_SourceResource",
									"Ts_FrmUser",
                                    "Ts_FrmMemo",
									"Ts_ConfirmDate",
									"Ts_ConfirmTime",
									"Ts_ConfirmResource",
									"Ts_ConfirmUser",

									"ErrorCodeGroup",
									"ErrorCodeGroupDescription",
									"ErrorCode",
									"ErrorCodeDescription",
									"ErrorCauseCode",
									"ErrorCauseDescription",
									"ErrorLocation",
									"ErrorParts",
									"Solution",
									"SolutionDescription",
                                    "SolutionMemo",
									"Duty",
									"DutyDescription",

									"Memo",
									"Ts_SouceResourceDate",
									"Ts_SouceResourceTime",
									"Ts_RepaireResource",
									"Ts_TSUser",
									"Ts_DestOpCode"
									
								};
            }
            else
            {
                (e as ExportHeadEventArgsNew).Heads =
                    new string[]{
									"Ts_SN",
									"Ts_Count",
									"Ts_TsState",
									"Ts_ModelCode",
									"Ts_ItemCode",
                                    //"Ts_ItemDesc",
                                    "Ts_Model",
                                    "Ts_BOMVersion",
									"Ts_MoCode",
									"Ts_NGDate",
									"Ts_NGTime",
									"Ts_SourceResource",
									"Ts_FrmUser",
                                    "Ts_FrmMemo",
									"Ts_ConfirmDate",
									"Ts_ConfirmTime",
									"Ts_ConfirmResource",
									"Ts_ConfirmUser",
									"Ts_SouceResourceDate",
									"Ts_SouceResourceTime",
									"Ts_RepaireResource",
									"Ts_TSUser",
									"Ts_DestOpCode"
								};
            }
        }

        void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
            {
                if (chbRepairDetail.Checked)
                {
                    ExportQDOTSDetails1 obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as ExportQDOTSDetails1;
                    (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                        new string[]{
										obj.SN,
										obj.NGCount.ToString(),
										this.languageComponent1.GetString(obj.TsState),
										obj.ModelCode,
										obj.ItemCode,	
                                        //obj.ItemDesc,
							            obj.ModelType,
                                        obj.BOMVersion,					  
										obj.MoCode,

										FormatHelper.ToDateString(obj.SourceResourceDate),
										FormatHelper.ToTimeString(obj.SourceResourceTime),													  
										obj.SourceResource,
										obj.FrmUser,
                                        obj.TSMemo,
										FormatHelper.ToDateString(obj.ConfirmDate),
										FormatHelper.ToTimeString(obj.ConfiemTime),
										obj.ConfirmResource,
										obj.ConfirmUser,

										obj.ErrorCodeGroup,
										obj.ErrorCodeGroupDescription,
										obj.ErrorCode,
										obj.ErrorCodeDescription,
										obj.ErrorCauseCode,
										obj.ErrorCauseDescription,
										obj.ErrorLocation,
										obj.ErrorParts,
										obj.SolutionCode,
										obj.SolutionDescription,
                                        obj.Solution,
										obj.Duty,
										obj.DutyDescription,

										obj.Memo,
										FormatHelper.ToDateString(obj.RepaireDate),
										FormatHelper.ToTimeString(obj.RepaireTime),
										obj.RepaireResource,
										obj.TSUser,
										obj.DestOpCode
									};
                }
                else
                {
                    QDOTSRecord obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as QDOTSRecord;
                    (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                        new string[]{
										obj.SN,
										obj.NGCount.ToString(),
										this.languageComponent1.GetString(obj.TsState),
										obj.ModelCode,
										obj.ItemCode,
                                        //obj.ItemDesc,
                                        obj.ModelType,
                                        obj.BOMVersion,
										obj.MoCode,
										FormatHelper.ToDateString(obj.SourceResourceDate),
										FormatHelper.ToTimeString(obj.SourceResourceTime),													  
										obj.SourceResource,
										obj.FrmUser,
                                        obj.TSMemo,
										FormatHelper.ToDateString(obj.ConfirmDate),
										FormatHelper.ToTimeString(obj.ConfiemTime),
										obj.ConfirmResource,
										obj.ConfirmUser,
										FormatHelper.ToDateString(obj.RepaireDate),
										FormatHelper.ToTimeString(obj.RepaireTime),
										obj.RepaireResource,
										obj.TSUser,
										obj.DestOpCode
									};
                }
            }
        }

        void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                QDOTSRecord obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as QDOTSRecord;
                DataRow row = DtSource.NewRow();
                row["Ts_SN"] = this.GetRCardLink(obj.SN);
                row["Ts_RCard"] = obj.SN;
                row["Ts_SNSeq"] = obj.RunningCardSequence;
                row["Ts_Count"] = obj.NGCount.ToString();
                row["Ts_TsState"] = this.languageComponent1.GetString(obj.TsState);
                row["Ts_Details"] = "";
                row["Ts_ChangedItemDetails"] = "";
                row["Ts_ChangedItemDetailsSMT"] = "";
                row["Ts_ModelCode"] = obj.ModelCode;
                row["Ts_ItemCode"] = obj.ItemCode;
                row["Ts_ItemDesc"] = obj.ItemDesc;
                row["Ts_Model"] = obj.ModelType;
                row["Ts_BOMVersion"] = obj.BOMVersion;
                row["Ts_MoCode"] = obj.MoCode;
                row["Ts_NGDate"] = FormatHelper.ToDateString(obj.SourceResourceDate);
                row["Ts_NGTime"] = FormatHelper.ToTimeString(obj.SourceResourceTime);
                row["Ts_SourceResource"] = obj.SourceResource;
                row["Ts_FrmUser"] = obj.FrmUser;
                row["Ts_FrmMemo"] = obj.TSMemo;
                row["Ts_ConfirmDate"] = FormatHelper.ToDateString(obj.ConfirmDate);
                row["Ts_ConfirmTime"] = FormatHelper.ToTimeString(obj.ConfiemTime);
                row["Ts_ConfirmResource"] = obj.ConfirmResource;
                row["Ts_ConfirmUser"] = obj.ConfirmUser;
                row["Ts_SouceResourceDate"] = FormatHelper.ToDateString(obj.RepaireDate);
                row["Ts_SouceResourceTime"] = FormatHelper.ToTimeString(obj.RepaireTime);
                row["Ts_RepaireResource"] = obj.RepaireResource;
                row["Ts_TSUser"] = obj.TSUser;
                row["Ts_DestOpCode"] = obj.DestOpCode;
                row["Ts_ScrapCause"] = obj.ScrapCause;

                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                  
            }
        }

        void _helper_LoadGridDataSource(object sender, EventArgs e)
        {
            if ((sender is System.Web.UI.HtmlControls.HtmlInputButton) && ((System.Web.UI.HtmlControls.HtmlInputControl)(((System.Web.UI.HtmlControls.HtmlInputButton)(sender)))).Name == "cmdQuery")
            {
                //维修查询
                this.QueryEvent(sender, e);
            }
            else
            {
                //导出查询
                this.ExprotEvent(sender, e);
            }
        }

        private string GetRCardLink(string no)
        {
            return string.Format("<a href=FItemTracingQP.aspx?RCARDFROM={0}&RCARDTO={1}>{2}</a>", Server.UrlEncode(no), Server.UrlEncode(no), no);
        }

        private void ExprotEvent(object sender, EventArgs e)
        {
            if (chbRepairDetail.Checked)
            {
                //将序列号转换为SourceCode
                DataCollectFacade dataCollectfacade = new DataCollectFacade(this.DataProvider);
                //对于序列号的输入框，需要进行一下处理
                string startRCard = FormatHelper.CleanString(this.txtStartSnQuery.Text.Trim().ToUpper());
                string endRCard = FormatHelper.CleanString(this.txtEndSnQuery.Text.Trim().ToUpper());
                //转换成SourceCard
                string startSourceCard = dataCollectfacade.GetSourceCard(startRCard, string.Empty);
                string endSourceCard = dataCollectfacade.GetSourceCard(endRCard, string.Empty);
                //end

                int startDate = DefaultDateTime.DefaultToInt;
                int endDate = DefaultDateTime.DefaultToInt;

                startDate = FormatHelper.TODateInt(this.dateStartDateQuery.Text);
                endDate = FormatHelper.TODateInt(this.dateEndDateQuery.Text);

                FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
                (e as WebQueryEventArgsNew).GridDataSource =
                    facadeFactory.CreateQueryTSRecordFacade().QueryExportTSRecord_New(
                        this.drpFGorSemiFG.SelectedValue,
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtConditionItem.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtConditionMo.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(startSourceCard)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(endSourceCard)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkMo.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSegmentCodeQuery.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStepSequence.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeEdit.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroupQuery.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialModelCodeQuery.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialMachineTypeGroup.Text)),
                        startDate, endDate, CheckBoxListBuilder.GetCheckedList(this.chkTSStateList),
                        (e as WebQueryEventArgsNew).StartRow,
                        (e as WebQueryEventArgsNew).EndRow);
            }
            else
            {
                this.QueryEvent(sender, e);
            }
        }

        private void QueryEvent(object sender, EventArgs e)
        {
            //将序列号转换为SourceCode
            DataCollectFacade dataCollectfacade = new DataCollectFacade(this.DataProvider);
            //对于序列号的输入框，需要进行一下处理
            string startRCard = FormatHelper.CleanString(this.txtStartSnQuery.Text.Trim().ToUpper());
            string endRCard = FormatHelper.CleanString(this.txtEndSnQuery.Text.Trim().ToUpper());
            //转换成SourceCard
            string startSourceCard = dataCollectfacade.GetSourceCard(startRCard, string.Empty);
            string endSourceCard = dataCollectfacade.GetSourceCard(endRCard, string.Empty);
            //end

            int startDate = DefaultDateTime.DefaultToInt;
            int endDate = DefaultDateTime.DefaultToInt;

            startDate = FormatHelper.TODateInt(this.dateStartDateQuery.Text);
            endDate = FormatHelper.TODateInt(this.dateEndDateQuery.Text);

            FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
            (e as WebQueryEventArgsNew).GridDataSource =
                facadeFactory.CreateQueryTSRecordFacade().QueryTSRecord_New(
                        this.drpFGorSemiFG.SelectedValue,
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtConditionItem.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtConditionMo.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(startSourceCard)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(endSourceCard)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkMo.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSegmentCodeQuery.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStepSequence.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeEdit.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroupQuery.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialModelCodeQuery.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialMachineTypeGroup.Text)),
                        startDate, endDate, CheckBoxListBuilder.GetCheckedList(this.chkTSStateList),
                        (e as WebQueryEventArgsNew).StartRow,
                        (e as WebQueryEventArgsNew).EndRow);
            (e as WebQueryEventArgsNew).RowCount =
                facadeFactory.CreateQueryTSRecordFacade().QueryTSRecordCount_New(
                        this.drpFGorSemiFG.SelectedValue,
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtConditionItem.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtConditionMo.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(startSourceCard)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(endSourceCard)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReworkMo.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSegmentCodeQuery.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStepSequence.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeEdit.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroupQuery.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialModelCodeQuery.Text)),
                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialMachineTypeGroup.Text)),
                        startDate, endDate, CheckBoxListBuilder.GetCheckedList(this.chkTSStateList));
        }

        protected void drpFGorSemiFG_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.drpFGorSemiFG.Items.Clear();
                this.drpFGorSemiFG.Items.Add(new ListItem("", ""));
                this.drpFGorSemiFG.Items.Add(new ListItem(languageComponent1.GetString(ItemType.ITEMTYPE_FINISHEDPRODUCT), ItemType.ITEMTYPE_FINISHEDPRODUCT));
                this.drpFGorSemiFG.Items.Add(new ListItem(languageComponent1.GetString(ItemType.ITEMTYPE_SEMIMANUFACTURE), ItemType.ITEMTYPE_SEMIMANUFACTURE));
            }            
        }
    }
}
