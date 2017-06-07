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

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.WebQuery;

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.DataCollect;

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// FOQCLotDetailsQP 的摘要说明。
    /// </summary>
    public partial class FOQCLotDetailsQP : BaseQPageNew
    {

        protected ExcelExporter excelExporter = null;
        protected WebQueryHelperNew _helper = null;
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        //private System.ComponentModel.IContainer components;
        //protected GridHelperNew gridHelper = null;
        protected System.Web.UI.WebControls.Label lblStartSNQuery;
        protected System.Web.UI.WebControls.Label lblEndSNQuery;
        protected System.Web.UI.WebControls.Label lblSoftwareNameQuery;
        protected System.Web.UI.WebControls.TextBox txtSoftwareNameQuery;
        protected System.Web.UI.WebControls.Label lblSoftwareVersionQuery;
        protected System.Web.UI.WebControls.Label lblStepSequenceConditionQuery;
        protected BenQGuru.eMES.Web.SelectQuery.SelectableTextBox txtConditionStepSequence;
        protected System.Web.UI.WebControls.TextBox txtSoftwareVersionQuery;

        //protected BenQGuru.eMES.Web.UserControl.eMESTime txtOQCBeginTime;
        //protected BenQGuru.eMES.Web.UserControl.eMESTime txtOQCEndTime;

        protected BenQGuru.eMES.Web.UserControl.eMESTime txtPackedBeginTime;
        protected BenQGuru.eMES.Web.UserControl.eMESTime txtPackedEndTime;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this._initialWebGrid();

                this._initialOQCStatus();
                this.ddlMaterialExportImportWhere_Load();

                dateInDateFromQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));
                dateInDateToQuery.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));

                txtPackedBeginDate.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));
                txtPackedEndDate.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));

                txtOQCBeginDate.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));
                txtOQCEndDate.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Today));

                //this.txtOQCBeginTime.Text = FormatHelper.ToTimeString( 0 ) ;
                //this.txtOQCEndTime.Text = FormatHelper.ToTimeString(235959);

                this.txtPackedBeginTime.Text = FormatHelper.ToTimeString(0);
                this.txtPackedEndTime.Text = FormatHelper.ToTimeString(235959);

                this.chbItemDetail.Attributes["onclick"] = "onCheckBoxChange(this)";
                this.chbRepairDetail.Attributes["onclick"] = "onCheckBoxChange(this)";

                this.rdbPackedDate.Attributes["onclick"] = "onRadioCheckChange(this)";
                this.chbOQCDate.Attributes["onclick"] = "onCheckBoxChange(this)";

                //this.rdbPackedDate.Checked = true;
                this.rdbPackedDate.Checked = false;
            }

            this._helper = new WebQueryHelperNew(this.cmdQuery, this.cmdGridExport, this.gridWebGrid, this.pagerSizeSelector, this.pagerToolBar, this.languageComponent1,this.DtSource);
            this._helper.LoadGridDataSource += new EventHandler(_helper_LoadGridDataSource);
            this._helper.DomainObjectToGridRow += new EventHandler(_helper_DomainObjectToGridRow);
            this._helper.DomainObjectToExportRow += new EventHandler(_helper_DomainObjectToExportRow);
            this._helper.GetExportHeadText += new EventHandler(_helper_GetExportHeadText);
            //this._helper.GridCellClick += new EventHandler(_helper_GridCellClick);

            FormatHelper.SetSNRangeValue(txtStartSnQuery, txtEndSnQuery);

            this.txtOQCLotQuery.Attributes.Add("onkeyup", "OnOQCLotKeyUp();");
        }

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
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        private void _initialOQCStatus()
        {
            this.drpOQCStateQuery.Items.Clear();

            foreach (string key in new OQCLotStatus().Items)
            {
                this.drpOQCStateQuery.Items.Add(new ListItem(this.languageComponent1.GetString(key), key));
            }
            this.drpOQCStateQuery.Items.Insert(0, "");
        }

        private void _initialWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("ItemDesc", "产品描述", null);
            this.gridHelper.AddColumn("MaterialModelCode", "整机机型", null);
            this.gridHelper.AddColumn("SSCode", "产线代码", null);

            this.gridHelper.AddColumn("CrewCode", "班组", null);
            this.gridHelper.AddColumn("BigLine", "大线", null);
            this.gridHelper.AddColumn("MaterialMachineType", "整机机芯", null);
            this.gridHelper.AddColumn("MaterialExportImport", "内销/出口", null);
            this.gridHelper.AddColumn("ReworkCode", "返工需求单号", null);
            this.gridHelper.AddColumn("OldLotNo", "母批", null);
            this.gridHelper.AddColumn("IsLotFrozen", "是否隔离", null);

            this.gridHelper.AddColumn("LOTNO", "送检批号", null);
            this.gridHelper.AddColumn("LotSize", "送检批量", null);
            this.gridHelper.AddColumn("OQCLotType", "类型", null);
            this.gridHelper.AddColumn("LOTStatus", "当前状态", null);
            this.gridHelper.AddColumn("ProductType", "生产类型", null);

            this.gridHelper.AddColumn("SampleCount", "样本数", null);
            this.gridHelper.AddColumn("NGCount", "样本不良数", null);
            //this.gridHelper.AddColumn("FirstGoodCount", "一交合格数", null);
            this.gridHelper.AddColumn("SafeCount", "A等级", null);
            this.gridHelper.AddColumn("SerialCount", "B等级", null);
            this.gridHelper.AddColumn("SoftCount", "C等级", null);
            this.gridHelper.AddColumn("ZCount", "Z等级", null);

            this.gridHelper.AddColumn("PackedMan", "包装人员", null);
            this.gridHelper.AddColumn("PackeDate", "包装日期", null);
            this.gridHelper.AddColumn("PackeTime", "包装时间", null);

            this.gridHelper.AddColumn("DUser", "判定人员", null);
            this.gridHelper.AddColumn("DDate", "判定日期", null);
            this.gridHelper.AddColumn("DTime", "判定时间", null);

            this.gridHelper.AddColumn("Memo", "备注", null);

            this.gridHelper.AddLinkColumn("SampleDetails", "样本不良明细", null);
            this.gridHelper.AddLinkColumn("Details", "产品明细", null);

            // Added By Hi1/Venus.Feng on 20080801 for Hisense Version
            this.gridWebGrid.Columns.FromKey("ItemDesc").Hidden = true;
            this.gridWebGrid.Columns.FromKey("PackedMan").Hidden = true;
            this.gridWebGrid.Columns.FromKey("PackeDate").Hidden = true;
            this.gridWebGrid.Columns.FromKey("PackeTime").Hidden = true;
            
            // End Added

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void _helper_LoadGridDataSource(object sender, EventArgs e)
        {
            PageCheckManager manager = new PageCheckManager();
            //manager.Add( new DateRangeCheck(this.lblInDateFromQuery, this.dateInDateFromQuery.Text, this.dateInDateToQuery.Text, false) );

            //			manager.Add( new DateRangeCheck(this.lblPackedBegindate, this.txtPackedBeginDate.Text, this.txtPackedEndDate.Text, false) );
            //
            //			if(cbOQC.Checked)
            //			{
            //				manager.Add( new DateRangeCheck(this.lblOQCBegdate, this.txtOQCBeginDate.Text, this.txtOQCEndDate.Text, false) );
            //			}
            if (this.chbOQCDate.Checked)
            {
                manager.Add(new DateCheck(this.lblGenerateLotBegdate, this.txtOQCBeginDate.Text, false));
                manager.Add(new DateCheck(this.lblGenerateLotEnddate, this.txtOQCEndDate.Text, false));
                manager.Add(new DateRangeCheck(this.lblGenerateLotBegdate, this.txtOQCBeginDate.Text, this.txtOQCEndDate.Text, false));
            }
            if (this.rdbPackedDate.Checked)
            {
                manager.Add(new DateRangeCheck(this.lblPackingBeginDate, this.txtPackedBeginDate.Text, this.txtPackedEndDate.Text, false));
            }
            manager.Add(new DateCheck(this.lblDecideBegdate,this.txtDecideBegdateQuery.Text,false));
            manager.Add(new DateCheck(this.lblDecideEnddate,this.txtDecideEnddateQuery.Text,false));

            manager.Add(new DateRangeCheck(this.lblDecidedate, this.txtDecideBegdateQuery.Text, this.txtDecideEnddateQuery.Text, false));



            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return;
            }

            if ((sender is System.Web.UI.HtmlControls.HtmlInputButton) && ((System.Web.UI.HtmlControls.HtmlInputControl)(((System.Web.UI.HtmlControls.HtmlInputButton)(sender)))).Name == "cmdGridExport")
            {
                //TODO ForSimone
                this.ExportQueryEvent(sender, e);
            }
            else
            {
                this.QueryEvent(sender, e);
            }
        }

        #region 查询事件

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

            int PackedBeginDate = DefaultDateTime.DefaultToInt;
            int PackedBeginTime = FormatHelper.TOTimeInt(this.txtPackedBeginTime.Text);
            int PackedEndDate = DefaultDateTime.DefaultToInt;
            int PackedEndTime = FormatHelper.TOTimeInt(this.txtPackedEndTime.Text);
            int OQCBeginDate = DefaultDateTime.DefaultToInt;
            int OQCBeginTime = 0;
            //FormatHelper.TOTimeInt(this.txtOQCBeginTime.Text);
            int OQCEndDate = DefaultDateTime.DefaultToInt;
            int OQCEndTime = 0;

            int reworkBeginDate = FormatHelper.TODateInt(this.txtDecideBegdateQuery.Text);
            int reworkEndDate = FormatHelper.TODateInt(this.txtDecideEnddateQuery.Text);
            //FormatHelper.TOTimeInt(this.txtOQCEndTime.Text);

            //			PackedBeginDate = FormatHelper.TODateInt(this.txtPackedBeginDate.Text);
            //			PackedEndDate = FormatHelper.TODateInt(this.txtPackedEndDate.Text);
            //
            //			if(cbOQC.Checked)
            //			{
            //				OQCBeginDate = FormatHelper.TODateInt(this.txtOQCBeginDate.Text);
            //				OQCEndDate = FormatHelper.TODateInt(this.txtOQCEndDate.Text);
            //			}

            if (this.chbOQCDate.Checked)
            {
                OQCBeginDate = FormatHelper.TODateInt(this.txtOQCBeginDate.Text);
                OQCEndDate = FormatHelper.TODateInt(this.txtOQCEndDate.Text);
            }
            if (this.rdbPackedDate.Checked)
            {
                PackedBeginDate = FormatHelper.TODateInt(this.txtPackedBeginDate.Text);
                PackedEndDate = FormatHelper.TODateInt(this.txtPackedEndDate.Text);
            }

            FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
            (e as WebQueryEventArgsNew).GridDataSource =
                facadeFactory.CreateQueryOQCLotDetailsFacade().QueryOQCLotDetails(
                FormatHelper.CleanString(this.txtProductionTypeQuery.Text),
                FormatHelper.CleanString(this.txtStepSequence.Text).ToUpper(),
                FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
                FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
                FormatHelper.CleanString(this.txtOQCLotQuery.Text),
                FormatHelper.CleanString(this.drpOQCStateQuery.SelectedValue),
                FormatHelper.CleanString(startSourceCard),
                FormatHelper.CleanString(endSourceCard),
                FormatHelper.CleanString(this.drpFirstClassQuery.SelectedValue),
                FormatHelper.CleanString(this.drpSecondClassQuery.SelectedValue),
                FormatHelper.CleanString(this.drpThirdClassQuery.SelectedValue),
                FormatHelper.CleanString(this.drpCrewCodeQuery.SelectedValue),
                FormatHelper.CleanString(this.txtBigSSCodeWhere.Text),
                FormatHelper.CleanString(this.txtMaterialModelCodeWhere.Text),
                FormatHelper.CleanString(this.txtMaterialMachineTypeWhere.Text),
                FormatHelper.CleanString(this.drpMaterialExportImportWhere.SelectedValue),
                FormatHelper.CleanString(this.txtDecideManQuery.Text),
                FormatHelper.CleanString(this.txtOQCLotTypeQuery.Text),
                FormatHelper.CleanString(this.txtReWorkMOQuery.Text),
                reworkBeginDate, reworkEndDate,
                PackedBeginDate, PackedBeginTime,
                PackedEndDate, PackedEndTime,
                OQCBeginDate, OQCBeginTime,
                OQCEndDate, OQCEndTime,
                this.chbQueryHistory.Checked,
                (e as WebQueryEventArgsNew).StartRow,
                (e as WebQueryEventArgsNew).EndRow);

            //if(this.chbQueryHistory.Checked)
            //{
            //    if(( e as WebQueryEventArgs ).GridDataSource != null)
            //    {
            //        ( e as WebQueryEventArgs ).RowCount = ( e as WebQueryEventArgs ).GridDataSource.Length;
            //    }
            //    else
            //    {
            //        ( e as WebQueryEventArgs ).RowCount = 0;
            //    }
            //}
            //else
            //{
            (e as WebQueryEventArgsNew).RowCount =
                facadeFactory.CreateQueryOQCLotDetailsFacade().QueryOQCLotDetailsCount(
                FormatHelper.CleanString(this.txtProductionTypeQuery.Text),
                FormatHelper.CleanString(this.txtStepSequence.Text).ToUpper(),
                FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
                FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
                FormatHelper.CleanString(this.txtOQCLotQuery.Text),
                FormatHelper.CleanString(this.drpOQCStateQuery.SelectedValue),
                FormatHelper.CleanString(startSourceCard),
                FormatHelper.CleanString(endSourceCard),
                FormatHelper.CleanString(this.drpFirstClassQuery.SelectedValue),
                FormatHelper.CleanString(this.drpSecondClassQuery.SelectedValue),
                FormatHelper.CleanString(this.drpThirdClassQuery.SelectedValue),
                FormatHelper.CleanString(this.drpCrewCodeQuery.SelectedValue),
                FormatHelper.CleanString(this.txtBigSSCodeWhere.Text),
                FormatHelper.CleanString(this.txtMaterialModelCodeWhere.Text),
                FormatHelper.CleanString(this.txtMaterialMachineTypeWhere.Text),
                FormatHelper.CleanString(this.drpMaterialExportImportWhere.SelectedValue),
                FormatHelper.CleanString(this.txtDecideManQuery.Text),
                FormatHelper.CleanString(this.txtOQCLotTypeQuery.Text),
                FormatHelper.CleanString(this.txtReWorkMOQuery.Text),
                reworkBeginDate, reworkEndDate,
                PackedBeginDate, PackedBeginTime,
                PackedEndDate, PackedEndTime,
                OQCBeginDate, OQCBeginTime,
                OQCEndDate, OQCEndTime);
            //}
        }

        //导出事件
        private void ExportQueryEvent(object sender, EventArgs e)
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

            int PackedBeginDate = DefaultDateTime.DefaultToInt;
            int PackedBeginTime = FormatHelper.TOTimeInt(this.txtPackedBeginTime.Text);
            int PackedEndDate = DefaultDateTime.DefaultToInt;
            int PackedEndTime = FormatHelper.TOTimeInt(this.txtPackedEndTime.Text);
            int OQCBeginDate = DefaultDateTime.DefaultToInt;
            int OQCBeginTime = 0;
            //FormatHelper.TOTimeInt(this.txtOQCBeginTime.Text);
            int OQCEndDate = DefaultDateTime.DefaultToInt;
            int OQCEndTime = 0;
            int reworkBeginDate = FormatHelper.TODateInt(this.txtDecideBegdateQuery.Text);
            int reworkEndDate = FormatHelper.TODateInt(this.txtDecideEnddateQuery.Text);
            //FormatHelper.TOTimeInt(this.txtOQCEndTime.Text);

            //			PackedBeginDate = FormatHelper.TODateInt(this.txtPackedBeginDate.Text);
            //			PackedEndDate = FormatHelper.TODateInt(this.txtPackedEndDate.Text);
            //
            //			if(cbOQC.Checked)
            //			{
            //				OQCBeginDate = FormatHelper.TODateInt(this.txtOQCBeginDate.Text);
            //				OQCEndDate = FormatHelper.TODateInt(this.txtOQCEndDate.Text);
            //			}

            if (this.chbOQCDate.Checked)
            {
                OQCBeginDate = FormatHelper.TODateInt(this.txtOQCBeginDate.Text);
                OQCEndDate = FormatHelper.TODateInt(this.txtOQCEndDate.Text);
            }
            if (this.rdbPackedDate.Checked)
            {
                PackedBeginDate = FormatHelper.TODateInt(this.txtPackedBeginDate.Text);
                PackedEndDate = FormatHelper.TODateInt(this.txtPackedEndDate.Text);
            }

            if (chbItemDetail.Checked)
            {
                //TODO ForSimone
                #region 产品明细导出
                FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
                (e as WebQueryEventArgs).GridDataSource =
                    facadeFactory.CreateQueryOQCLotDetailsFacade().ExportQueryOQCLotDetails(
                    FormatHelper.CleanString(this.txtProductionTypeQuery.Text),
                    FormatHelper.CleanString(this.txtStepSequence.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtOQCLotQuery.Text),
                    FormatHelper.CleanString(this.drpOQCStateQuery.SelectedValue),
                    FormatHelper.CleanString(startSourceCard),
                    FormatHelper.CleanString(endSourceCard),
                    FormatHelper.CleanString(this.drpFirstClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpSecondClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpThirdClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpCrewCodeQuery.SelectedValue),
                    FormatHelper.CleanString(this.txtBigSSCodeWhere.Text),
                    FormatHelper.CleanString(this.txtMaterialModelCodeWhere.Text),
                    FormatHelper.CleanString(this.txtMaterialMachineTypeWhere.Text),
                    FormatHelper.CleanString(this.drpMaterialExportImportWhere.SelectedValue),
                    FormatHelper.CleanString(this.txtDecideManQuery.Text),
                    FormatHelper.CleanString(this.txtOQCLotTypeQuery.Text),
                    FormatHelper.CleanString(this.txtReWorkMOQuery.Text),
                reworkBeginDate, reworkEndDate,
                    PackedBeginDate, PackedBeginTime,
                    PackedEndDate, PackedEndTime,
                    OQCBeginDate, OQCBeginTime,
                    OQCEndDate, OQCEndTime,
                    this.chbQueryHistory.Checked,
                    (e as WebQueryEventArgs).StartRow,
                    (e as WebQueryEventArgs).EndRow);

                #endregion

            }
            else if (chbRepairDetail.Checked)
            {
                //TODO ForSimone

                #region 样本不良明细导出

                FacadeFactory facadeFactory = new FacadeFactory(base.DataProvider);
                (e as WebQueryEventArgs).GridDataSource =
                    facadeFactory.CreateQueryOQCLotDetailsFacade().ExportQueryOQCLotSampleDetails(
                    FormatHelper.CleanString(this.txtProductionTypeQuery.Text),
                    FormatHelper.CleanString(this.txtStepSequence.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtConditionItem.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtConditionMo.Text).ToUpper(),
                    FormatHelper.CleanString(this.txtOQCLotQuery.Text),
                    FormatHelper.CleanString(this.drpOQCStateQuery.SelectedValue),
                    FormatHelper.CleanString(startSourceCard),
                    FormatHelper.CleanString(endSourceCard),
                    FormatHelper.CleanString(this.drpFirstClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpSecondClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpThirdClassQuery.SelectedValue),
                    FormatHelper.CleanString(this.drpCrewCodeQuery.SelectedValue),
                    FormatHelper.CleanString(this.txtBigSSCodeWhere.Text),
                    FormatHelper.CleanString(this.txtMaterialModelCodeWhere.Text),
                    FormatHelper.CleanString(this.txtMaterialMachineTypeWhere.Text),
                    FormatHelper.CleanString(this.drpMaterialExportImportWhere.SelectedValue),
                    FormatHelper.CleanString(this.txtDecideManQuery.Text),
                    FormatHelper.CleanString(this.txtOQCLotTypeQuery.Text),
                    FormatHelper.CleanString(this.txtReWorkMOQuery.Text),
                reworkBeginDate, reworkEndDate,
                    PackedBeginDate, PackedBeginTime,
                    PackedEndDate, PackedEndTime,
                    OQCBeginDate, OQCBeginTime,
                    OQCEndDate, OQCEndTime,
                    this.chbQueryHistory.Checked,
                    (e as WebQueryEventArgs).StartRow,
                    (e as WebQueryEventArgs).EndRow);

                #endregion

            }
            else
            {
                this.QueryEvent(sender, e);
            }
        }

        #endregion


        private void _helper_DomainObjectToGridRow(object sender, EventArgs e)
        {
            if ((e as DomainObjectToGridRowEventArgsNew).DomainObject != null)
            {
                QueryOQCLot obj = (e as DomainObjectToGridRowEventArgsNew).DomainObject as QueryOQCLot;
                DataRow row = DtSource.NewRow();
                row["ItemCode"] = obj.ItemCode;
                row["ItemDesc"] = obj.ItemDesc;
                row["MaterialModelCode"] = obj.MaterialModelCode;
                row["SSCode"] = obj.SSCode;
                row["CrewCode"] = obj.CrewCode;
                row["BigLine"] = obj.BigStepSequenceCode;
                row["MaterialMachineType"] = obj.MaterialMachineType;
                row["MaterialExportImport"] = this.languageComponent1.GetString(obj.MaterialExportImport);
                row["ReworkCode"] = obj.ReworkCode;
                row["OldLotNo"] = obj.OldLotNo;

                row["IsLotFrozen"] = obj.LotFrozen;
                row["LOTNO"] = obj.LOTNO;
                row["LotSize"] = obj.LotSize;
                row["OQCLotType"] = this.languageComponent1.GetString(obj.OQCLotType);
                row["LOTStatus"] = this.languageComponent1.GetString(obj.LOTStatus);
                row["ProductType"] = this.languageComponent1.GetString(obj.ProductionType);

                row["SampleCount"] = obj.SampleCount;
                row["NGCount"] = obj.SampleNGCount;
                row["SafeCount"] = obj.AGradeTimes;
                row["SerialCount"] = obj.BGradeTimes;
                row["SoftCount"] = obj.CGradeTimes;
                row["ZCount"] = obj.ZGradeTimes;

                row["PackedMan"] = obj.MaintainUser;
                row["PackeDate"] = FormatHelper.ToDateString(obj.MaintainDate);
                row["PackeTime"] = FormatHelper.ToTimeString(obj.MaintainTime);
                row["DUser"] = obj.DealUser;
                row["DDate"] = FormatHelper.ToDateString(obj.DealDate);
                row["DTime"] = FormatHelper.ToTimeString(obj.DealTime);
                row["Memo"] = obj.Memo;
                
                (e as DomainObjectToGridRowEventArgsNew).GridRow = row;
                    //new UltraGridRow(new object[]{
                    //    obj.ItemCode,
                    //    obj.ItemDesc,
                    //    obj.MaterialModelCode,
                    //    obj.SSCode,
                    //    obj.CrewCode,
                    //    obj.BigStepSequenceCode,
                    //    obj.MaterialMachineType,
                    //     this.languageComponent1.GetString(obj.MaterialExportImport),
                    //    obj.ReworkCode,
                    //    obj.OldLotNo,
                    //    obj.LotFrozen,
                    //    obj.LOTNO,
                    //    obj.LotSize,
                    //    this.languageComponent1.GetString(obj.OQCLotType),
                    //    this.languageComponent1.GetString(obj.LOTStatus),	
                    //    this.languageComponent1.GetString(obj.ProductionType),
                    //    obj.SampleCount,
                    //    obj.SampleNGCount,
                    //    //obj.FirstGoodCount,
                    //    obj.AGradeTimes,
                    //    obj.BGradeTimes,
                    //    obj.CGradeTimes,
                    //    obj.ZGradeTimes,
                    //    obj.MaintainUser,
                    //    FormatHelper.ToDateString(obj.MaintainDate),
                    //    FormatHelper.ToTimeString(obj.MaintainTime),
                    //    obj.DealUser,
                    //    FormatHelper.ToDateString(obj.DealDate),
                    //    FormatHelper.ToTimeString(obj.DealTime),
                    //    obj.Memo,
                    //    "",
                    //    ""
                    //}
                    //);
            }
        }

        private void _helper_DomainObjectToExportRow(object sender, EventArgs e)
        {
            if (chbItemDetail.Checked)
            {
                //TODO ForSimone
                #region 产品明细导出

                if ((e as DomainObjectToExportRowEventArgs).DomainObject != null)
                {
                    ExportQueryOQCLotDetails obj = (e as DomainObjectToExportRowEventArgs).DomainObject as ExportQueryOQCLotDetails;
                    (e as DomainObjectToExportRowEventArgs).ExportRow =
                        new string[]{
                            obj.ItemCode,
                            //obj.ItemDesc,
                            obj.MaterialModelCode,
                            obj.SSCode,

                            obj.BigStepSequenceCode,
                            obj.MaterialMachineType,
                             this.languageComponent1.GetString(obj.MaterialExportImport),
                            obj.ReworkCode,
                            obj.OldLotNo,
                            obj.LotFrozen,

                            obj.LOTNO,
                            obj.LotSize.ToString(),
                            this.languageComponent1.GetString(obj.OQCLotType),
                            this.languageComponent1.GetString(obj.LOTStatus),	
                            
                            obj.SampleCount.ToString(),
                            obj.SampleNGCount.ToString(),
                            //obj.FirstGoodCount.ToString(),
                            obj.AGradeTimes.ToString(),
                            obj.BGradeTimes.ToString(),
                            obj.CGradeTimes.ToString(),
                            obj.ZGradeTimes.ToString(),
                            
                            obj.MaintainUser,
                            FormatHelper.ToDateString(obj.MaintainDate),
                            FormatHelper.ToTimeString(obj.MaintainTime),
                            obj.DealUser,
                            FormatHelper.ToDateString(obj.DealDate),
                            FormatHelper.ToTimeString(obj.DealTime),
                            obj.Memo,
                            obj.RunningCard
                        };
                }

                #endregion

            }
            else if (chbRepairDetail.Checked)
            {
                #region 样本不良明细导出

                if ((e as DomainObjectToExportRowEventArgs).DomainObject != null)
                {
                    ExportQueryOQCLotSampleDetails obj = (e as DomainObjectToExportRowEventArgs).DomainObject as ExportQueryOQCLotSampleDetails;
                    (e as DomainObjectToExportRowEventArgs).ExportRow =
                        new string[]{
                            obj.ItemCode,
                            //obj.ItemDesc,
                            obj.MaterialModelCode,
                            obj.SSCode,
                            obj.CrewCode,
                            obj.BigStepSequenceCode,
                            obj.MaterialMachineType,
                            this.languageComponent1.GetString(obj.MaterialExportImport),
                            obj.ReworkCode,
                            obj.OldLotNo,
                            obj.LotFrozen,

                            obj.LOTNO,
                            obj.LotSize.ToString(),
                            this.languageComponent1.GetString(obj.OQCLotType),
                            this.languageComponent1.GetString(obj.LOTStatus),	
                            
                            obj.SampleCount.ToString(),
                            obj.SampleNGCount.ToString(),
                            //obj.FirstGoodCount.ToString(),
							obj.AGradeTimes.ToString(),
							obj.BGradeTimes.ToString(),
							obj.CGradeTimes.ToString(),
                            obj.ZGradeTimes.ToString(),

							obj.MaintainUser,
							FormatHelper.ToDateString(obj.MaintainDate),
							FormatHelper.ToTimeString(obj.MaintainTime),
							obj.DealUser,
							FormatHelper.ToDateString(obj.DealDate),
							FormatHelper.ToTimeString(obj.DealTime),

							obj.RunningCard ,
							obj.ErrorCodeGroupDescription,
							obj.ErrorCodeDescription,
							obj.ErrorCauseDescription,
							obj.ErrorLocation,
							obj.ErrorParts,
							obj.SolutionDescription,
							obj.DutyDescription,													  
							obj.Memo,
							obj.TSOperator,
							FormatHelper.ToDateString(obj.TSDate),
							FormatHelper.ToTimeString(obj.TSTime)
						};
                }

                #endregion
            }
            else
            {
                if ((e as DomainObjectToExportRowEventArgsNew).DomainObject != null)
                {
                    QueryOQCLot obj = (e as DomainObjectToExportRowEventArgsNew).DomainObject as QueryOQCLot;
                    (e as DomainObjectToExportRowEventArgsNew).ExportRow =
                        new string[]{
							obj.ItemCode,
                            //obj.ItemDesc,
                            obj.MaterialModelCode,
							obj.SSCode,
                            obj.CrewCode,
                            obj.BigStepSequenceCode,
                            obj.MaterialMachineType,
                            this.languageComponent1.GetString(obj.MaterialExportImport),
                            obj.ReworkCode,
                            obj.OldLotNo,
                            obj.LotFrozen,

							obj.LOTNO,
							obj.LotSize.ToString(),
							this.languageComponent1.GetString(obj.OQCLotType),
							this.languageComponent1.GetString(obj.LOTStatus),	

							obj.SampleCount.ToString(),
							obj.SampleNGCount.ToString(),
							//obj.FirstGoodCount.ToString(),
							obj.AGradeTimes.ToString(),
							obj.BGradeTimes.ToString(),
							obj.CGradeTimes.ToString(),
                            obj.ZGradeTimes.ToString(),

							obj.MaintainUser,
							FormatHelper.ToDateString(obj.MaintainDate),
							FormatHelper.ToTimeString(obj.MaintainTime),
							obj.DealUser,
							FormatHelper.ToDateString(obj.DealDate),
							FormatHelper.ToTimeString(obj.DealTime),
                            obj.Memo
						};
                }
            }
        }

        private void _helper_GetExportHeadText(object sender, EventArgs e)
        {
            if (chbItemDetail.Checked)
            {
                (e as ExportHeadEventArgsNew).Heads =
                    new string[]{
						"ItemCode",
                        //"ItemDesc",
                        "MaterialModelCode",
						"SSCode",
                        "CrewCode",
                        "BigLine",
                        "MaterialMachineType",
                        "MaterialExportImport",
                        "ReworkCode",
                        "OldLotNo",
                        "IsLotFrozen",

						"LOTNO",
						"LotSize",
						"OQCLotType",
						"LOTStatus",

                        "SampleCount",
                        "NGCount",
                        //"样本数",
                        //"样本不良数",
						//"一交合格数",
						"SafeCount",
						"SerialCount",
						"SoftCount",
                        "ZCount",
					
						"MaintainUser",
						"MaintainDate",
						"MaintainTime",
						"DUser",
						"DDate",
						"DTime",
                        "Memo",
						"RunningCard"
					};

            }
            else if (this.chbRepairDetail.Checked)
            {
                (e as ExportHeadEventArgsNew).Heads =
                    new string[]{
						"ItemCode",
                        //"ItemDesc",
                        "MaterialModelCode",
						"SSCode",
                        "CrewCode",
                         "BigLine",
                        "MaterialMachineType",
                        "MaterialExportImport",
                        "ReworkCode",
                        "OldLotNo",
                        "IsLotFrozen",

						"LOTNO",
						"LotSize",
						"OQCLotType",
						"LOTStatus",

                         "SampleCount",
                        "NGCount",

                        //"样本数",
                        //"样本不良数",
						//"一交合格数",
						"SafeCount",
						"SerialCount",
						"SoftCount",
                        "ZCount",
					
						"MaintainUser",
						"MaintainDate",
						"MaintainTime",
						"DUser",
						"DDate",
						"DTime",

						"RunningCard",
						"ErrorCodeGroup",
						"ErrorCode",
						"ErrorCause",
						"ErrorLocation",
						"ErrorParts",
						"Solution",
						"Duty",
						"Memo",
						"TsOperator",
						"MDate",
						"MTime"
					};
            }
            else
            {
                (e as ExportHeadEventArgsNew).Heads =
                    new string[]{
						"ItemCode",
                        //"ItemDesc",
                        "MaterialModelCode",
						"SSCode",
                        "CrewCode",
                        "BigLine",
                        "MaterialMachineType",
                        "MaterialExportImport",
                        "ReworkCode",
                        "OldLotNo",
                        "IsLotFrozen",

						"LOTNO",
						"LotSize",
						"OQCLotType",
						"LOTStatus",

                         "SampleCount",
                        "NGCount",

                        //"样本数",
                        //"样本不良数",
						//"一交合格数",
						"SafeCount",
						"SerialCount",
						"SoftCount",
                        "ZCount",
					
						"MaintainUser",
						"MaintainDate",
						"MaintainTime",
						"DUser",
						"DDate",
						"DTime",
                        "Memo"
					};
            }
        }

        protected override void Grid_ClickCell(GridRecord row, string command)
        {
            if (command.ToUpper() == "Details".ToUpper())
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FOQCLotDetailsListInfoQP.aspx",
                    new string[]{
									"oqclot"
								},
                    new string[]{
									row.Items.FindItemByKey("LOTNO").Text
								})
                    );
            }
            else if (command.ToUpper() == "SampleDetails".ToUpper())
            {
                this.Response.Redirect(
                    this.MakeRedirectUrl(
                    "FOQCSampleNGDetailQP.aspx",
                    new string[]{
									"LotNo",
									"BackUrl"
								},
                    new string[]{
									row.Items.FindItemByKey("LOTNO").Text,
									"FOQCLotDetailsQP.aspx"
								})
                    );
            }
        }

        protected void drpFirstClass_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);

                DropDownListBuilder builder = new DropDownListBuilder(this.drpFirstClassQuery);

                builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(itemFacade.GetItemFirstClass);

                builder.Build("FirstClass", "FirstClass");

                this.drpFirstClassQuery.Items.Insert(0, new ListItem("", ""));
            }
        }

        protected void drpFirstClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string firstClass = this.drpFirstClassQuery.SelectedValue;

            this.drpSecondClassQuery.Items.Clear();
            this.drpThirdClassQuery.Items.Clear();

            if (firstClass.Trim().Length > 0)
            {
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                object[] itemClassList = itemFacade.GetItemSecondClass(firstClass);
                if (itemClassList != null)
                {
                    foreach (ItemClass itemClass in itemClassList)
                    {
                        this.drpSecondClassQuery.Items.Add(new ListItem(itemClass.SecondClass, itemClass.SecondClass));
                    }
                }
            }

            this.drpSecondClassQuery.Items.Insert(0, new ListItem("", ""));
        }

        protected void drpSecondClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string firstClass = this.drpFirstClassQuery.SelectedValue;
            string secondClass = this.drpSecondClassQuery.SelectedValue;

            this.drpThirdClassQuery.Items.Clear();

            if (firstClass.Trim().Length > 0 && secondClass.Trim().Length > 0)
            {
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                object[] itemClassList = itemFacade.GetItemThirdClass(firstClass, secondClass);
                if (itemClassList != null)
                {
                    foreach (ItemClass itemClass in itemClassList)
                    {
                        this.drpThirdClassQuery.Items.Add(new ListItem(itemClass.ThirdClass, itemClass.ThirdClass));
                    }
                }
            }

            this.drpThirdClassQuery.Items.Insert(0, new ListItem("", ""));
        }

        protected void drpCrewCodeQuery_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                ShiftModel shiftModel = new ShiftModel(this.DataProvider);

                DropDownListBuilder builder = new DropDownListBuilder(this.drpCrewCodeQuery);
                builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(shiftModel.GetAllShiftCrew);
                builder.Build("CrewCode", "CrewCode");
                this.drpCrewCodeQuery.Items.Insert(0, new ListItem("", ""));
            }
        }

        private void ddlMaterialExportImportWhere_Load()
        {
            this.drpMaterialExportImportWhere.Items.Clear();
            this.drpMaterialExportImportWhere.Items.Add(new ListItem("", ""));
            this.drpMaterialExportImportWhere.Items.Add(new ListItem(this.languageComponent1.GetString("materialexportimport_import"), "IMPORT"));
            this.drpMaterialExportImportWhere.Items.Add(new ListItem(this.languageComponent1.GetString("materialexportimport_export"), "EXPORT"));
        }
    }
}
