#region  system
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
#endregion

#region project
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using System.Data;
using System.Collections.Generic;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
#endregion

namespace BenQGuru.eMES.Client
{
    public partial class FOQCQuery : BaseForm
    {

        private string m_LotNo = string.Empty;

        private DataSet m_SampleList = null;
        private DataTable m_CheckLOTNO = null;
        private DataTable m_CheckGrade = null;
        private DataTable m_CheckGroupOne = null;

        private DataSet m_CheckList = null;
        private DataTable m_CheckRcard = null;
        private DataTable m_CheckNGCode = null;


        private DataSet m_BaseList = null;
        private DataTable m_Checkgroup = null;
        private DataTable m_RcardAndResult = null;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }
        public FOQCQuery()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            this.ultraGridHeader.DisplayLayout.Appearance.BackColor = System.Drawing.Color.Gainsboro; ;
            this.ultraGridHeader.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            //this.ultraGridHeader.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridHeader.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridHeader.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridHeader.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridHeader.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridHeader.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridHeader.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridHeader.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

            this.ultraGridDetail.DisplayLayout.Appearance.BackColor = System.Drawing.Color.Gainsboro; ;
            this.ultraGridDetail.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            //this.ultraGridDetail.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridDetail.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridDetail.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridDetail.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridDetail.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridDetail.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridDetail.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridDetail.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

            this.ultraGridBase.DisplayLayout.Appearance.BackColor = System.Drawing.Color.Gainsboro; ;
            this.ultraGridBase.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            //this.ultraGridDetail.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridBase.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            this.ultraGridBase.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            this.ultraGridBase.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.ultraGridBase.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridBase.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            this.ultraGridBase.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            this.ultraGridBase.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FCollectionOQCQuery_Load(object sender, EventArgs e)
        {
            this.InitializeSampleListGrid();
            this.InitializeCheckListGrid();
            this.InitializeBaseListGrid();
            //this.InitPageLanguage();
        }
        private void ucLabelEditRCard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.GetLotNo();
            }
        }

        private void ucLabelEditLotNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                // Get CheckGroup Info
                Messages msg = this.LoadLotInfo();
                if (!msg.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().Add(msg);
                    this.m_LotNo = string.Empty;
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLabelEditSizeAndCapacity.Value = "";
                }
                else
                {
                    this.m_LotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
                    this.LoadSampleList(m_LotNo);
                    this.LoadCheckList(m_LotNo);
                    this.LoadBaseList(m_LotNo);
                    this.ucLabelEditLotNo.TextFocus(false, true);
                }
            }

        }

        private void ucButtonGetLot_Click(object sender, EventArgs e)
        {
            this.GetLotNo();

        }

        private void ultraGridHeader_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.MaxBandDepth = 1;
            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "CheckLOTNO";
            e.Layout.Bands[1].ScrollTipField = "CheckGrade";
            e.Layout.Bands[2].ScrollTipField = "CheckGroup";


            // 设置列宽和列名称
            //e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["CheckLOTNO"].Header.Caption = "批号";
            e.Layout.Bands[0].Columns["NGSampleCount"].Header.Caption = "不良样品数";
            e.Layout.Bands[0].Columns["SampleCount"].Header.Caption = "样本数";
            e.Layout.Bands[0].Columns["NGSampleRate"].Header.Caption = "抽样不合格率";

            e.Layout.Bands[0].Columns["CheckLOTNO"].Width = 300;
            e.Layout.Bands[0].Columns["NGSampleCount"].Width = 100;
            e.Layout.Bands[0].Columns["SampleCount"].Width = 100;
            e.Layout.Bands[0].Columns["NGSampleRate"].Width = 100;
            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["CheckLOTNO"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["NGSampleCount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["SampleCount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["NGSampleRate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            // 允许筛选
            e.Layout.Bands[0].Columns["CheckLOTNO"].AllowRowFiltering = DefaultableBoolean.True;
            // 允许冻结，且Checked栏位始终处于冻结状态，不可更改          
            e.Layout.Bands[0].Columns["CheckLOTNO"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["CheckLOTNO"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;

            e.Layout.Bands[0].Columns["CheckLOTNO"].SortIndicator = SortIndicator.Ascending;

            // CheckGrade
            e.Layout.Bands[1].Columns["CheckLOTNO"].Hidden = true;
            e.Layout.Bands[1].Columns["CheckGrade"].Header.Caption = "缺陷等级";
            e.Layout.Bands[1].Columns["DefectCount"].Header.Caption = "缺陷个数";
            e.Layout.Bands[1].Columns["DefeactRate"].Header.Caption = "缺陷率";
            e.Layout.Bands[1].Columns["CheckGrade"].Width = 300;
            e.Layout.Bands[1].Columns["DefectCount"].Width = 60;
            e.Layout.Bands[1].Columns["DefeactRate"].Width = 100;

            e.Layout.Bands[1].Columns["CheckGrade"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["DefectCount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["DefeactRate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["CheckGrade"].Header.Fixed = true;
            e.Layout.Bands[1].Columns["CheckGrade"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            e.Layout.Bands[1].Columns["CheckGrade"].AllowRowFiltering = DefaultableBoolean.True;

            e.Layout.Bands[1].Columns["CheckGrade"].SortIndicator = SortIndicator.Ascending;

            //CheckGroup
            e.Layout.Bands[2].Columns["CheckLOTNO"].Hidden = true;
            e.Layout.Bands[2].Columns["CheckGrade"].Hidden = true;
            e.Layout.Bands[2].Columns["CheckGroup"].Header.Caption = "检验类型";
            e.Layout.Bands[2].Columns["CheckItem"].Header.Caption = "检验项目";
            e.Layout.Bands[2].Columns["Count"].Header.Caption = "数量";
            e.Layout.Bands[2].Columns["CheckGroup"].Width = 300;
            e.Layout.Bands[2].Columns["CheckItem"].Width = 100;
            e.Layout.Bands[2].Columns["Count"].Width = 100;

            e.Layout.Bands[2].Columns["CheckGroup"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[2].Columns["CheckItem"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[2].Columns["Count"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[2].Columns["CheckGroup"].Header.Fixed = true;
            e.Layout.Bands[2].Columns["CheckGroup"].Header.Fixed = true;
            e.Layout.Bands[2].Columns["CheckGroup"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;

            e.Layout.Bands[2].Columns["CheckGroup"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridHeader);
        }

        private void ultraGridDetail_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.MaxBandDepth = 1;
            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "CheckRCard";
            e.Layout.Bands[1].ScrollTipField = "CheckNGCode";

            // CheckRCard
            e.Layout.Bands[0].Columns["CheckRCard"].Header.Caption = "序列号";
            e.Layout.Bands[0].Columns["CheckRCard"].Width = 100;
            e.Layout.Bands[0].Columns["CheckRCard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["CheckRCard"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["CheckRCard"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            e.Layout.Bands[0].Columns["CheckRCard"].AllowRowFiltering = DefaultableBoolean.True;

            e.Layout.Bands[0].Columns["CheckRCard"].SortIndicator = SortIndicator.Ascending;

            //CheckNGCode
            e.Layout.Bands[1].Columns["CheckRCard"].Hidden = true;
            e.Layout.Bands[1].Columns["CheckNGCode"].Header.Caption = "不良代码";
            e.Layout.Bands[1].Columns["CheckNGDESC"].Header.Caption = "不良代码描述";
            e.Layout.Bands[1].Columns["CheckDate"].Header.Caption = "检验日期";
            e.Layout.Bands[1].Columns["CheckTime"].Header.Caption = "检验时间";
            e.Layout.Bands[1].Columns["CheckUser"].Header.Caption = "检验人";

            e.Layout.Bands[1].Columns["CheckNGCode"].Width = 100;
            e.Layout.Bands[1].Columns["CheckNGDESC"].Width = 300;
            e.Layout.Bands[1].Columns["CheckDate"].Width = 100;
            e.Layout.Bands[1].Columns["CheckTime"].Width = 100;
            e.Layout.Bands[1].Columns["CheckUser"].Width = 100;

            e.Layout.Bands[1].Columns["CheckNGCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["CheckNGDESC"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["CheckDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["CheckTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["CheckUser"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["CheckNGCode"].Header.Fixed = true;
            e.Layout.Bands[1].Columns["CheckNGCode"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;

            e.Layout.Bands[1].Columns["CheckNGCode"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridDetail);
        }

        private void ultraGridBase_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.MaxBandDepth = 1;
            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "CheckGroup";
            e.Layout.Bands[1].ScrollTipField = "CheckRCard";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["CheckGroup"].Header.Caption = "检验类型";
            e.Layout.Bands[0].Columns["CheckedCount"].Header.Caption = "已检验数";
            e.Layout.Bands[0].Columns["NeedCheckCount"].Header.Caption = "应检验数";

            e.Layout.Bands[0].Columns["CheckGroup"].Width = 300;
            e.Layout.Bands[0].Columns["CheckedCount"].Width = 100;
            e.Layout.Bands[0].Columns["NeedCheckCount"].Width = 100;
            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["CheckGroup"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["CheckedCount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["NeedCheckCount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            // 允许筛选
            e.Layout.Bands[0].Columns["CheckGroup"].AllowRowFiltering = DefaultableBoolean.True;
            // 允许冻结，且Checked栏位始终处于冻结状态，不可更改
            e.Layout.Bands[0].Columns["CheckGroup"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["CheckGroup"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            // 默認排序
            e.Layout.Bands[0].Columns["CheckGroup"].SortIndicator = SortIndicator.Ascending;

            // CheckItem
            e.Layout.Bands[1].Columns["CheckGroup"].Hidden = true;
            e.Layout.Bands[1].Columns["CheckRCard"].Header.Caption = "序列号";
            e.Layout.Bands[1].Columns["CheckResult"].Header.Caption = "检验结果";
            e.Layout.Bands[1].Columns["CheckDate"].Header.Caption = "检验日期";
            e.Layout.Bands[1].Columns["CheckTime"].Header.Caption = "检验时间";
            e.Layout.Bands[1].Columns["CheckUser"].Header.Caption = "检验人";

            e.Layout.Bands[1].Columns["CheckRCard"].Width = 300;
            e.Layout.Bands[1].Columns["CheckResult"].Width = 60;
            e.Layout.Bands[1].Columns["CheckDate"].Width = 100;
            e.Layout.Bands[1].Columns["CheckTime"].Width = 100;
            e.Layout.Bands[1].Columns["CheckUser"].Width = 100;

            e.Layout.Bands[1].Columns["CheckRCard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["CheckResult"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["CheckDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["CheckTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["CheckUser"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["CheckRCard"].Header.Fixed = true;
            e.Layout.Bands[1].Columns["CheckRCard"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            e.Layout.Bands[1].Columns["CheckRCard"].AllowRowFiltering = DefaultableBoolean.True;

            e.Layout.Bands[1].Columns["CheckRCard"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridBase);
        }

        private void GetLotNo()
        {
            DataCollect.DataCollectFacade dcf = new BenQGuru.eMES.DataCollect.DataCollectFacade(DataProvider);
            string rCard = this.ucLabelEditRCard.Value.Trim().ToUpper();
            string cartonCode = txtCartonCode.Value.ToUpper().Trim();

            string sourceRCard = dcf.GetSourceCard(rCard, string.Empty);
            if (rCard.Length > 0)
            {
                object objSimulationReport = dcf.GetLastSimulationReport(sourceRCard);
                if (objSimulationReport != null)
                {
                    Domain.DataCollect.SimulationReport simulationReport = objSimulationReport as Domain.DataCollect.SimulationReport;
                    this.ucLabelEditLotNo.Value = simulationReport.LOTNO;
                    if (simulationReport.LOTNO == string.Empty)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SN_NOT_EXIST_LOT"));
                        this.ucLabelEditRCard.TextFocus(false, true);
                        return;
                    }

                    Messages msg = this.LoadLotInfo();
                    if (!msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().Add(msg);
                        this.m_LotNo = string.Empty;
                        this.ucLabelEditSizeAndCapacity.Value = "";
                        this.ucLabelEditItemCode.Value = "";
                        this.labelItemDescription.Text = "";
                    }
                    else
                    {
                        this.m_LotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
                        this.LoadSampleList(m_LotNo);
                        this.LoadCheckList(m_LotNo);
                        this.LoadBaseList(m_LotNo);
                        this.ucLabelEditLotNo.TextFocus(false, true);
                    }
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$NoSimulationInfo"));                    
                    this.m_LotNo = string.Empty;
                    this.ucLabelEditSizeAndCapacity.Value = "";
                    this.ucLabelEditItemCode.Value = "";
                    this.labelItemDescription.Text = "";
                    this.ucLabelEditRCard.TextFocus(false, true);
                }
            }
            //add by alex 2010.11.19
            else if (cartonCode != String.Empty)
            {
                OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
                object obj = oqcFacade.GetLot2CartonByCartonNo(cartonCode);
                if (obj != null)
                {
                    Lot2Carton lot2Carton = obj as Lot2Carton;

                    this.ucLabelEditLotNo.Value = lot2Carton.OQCLot;
                    if (lot2Carton.OQCLot == string.Empty)
                    {
                        Messages messages = new Messages();
                        messages.Add(new UserControl.Message(MessageType.Error, "$CS_CARTON_NOT_EXIST_LOT"));
                        ApplicationRun.GetInfoForm().Add(messages);
                        this.txtCartonCode.TextFocus(false, true);
                        return;
                    }
                    Messages msg = this.LoadLotInfo();
                    if (!msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().Add(msg);
                        this.m_LotNo = string.Empty;
                        this.ucLabelEditSizeAndCapacity.Value = "";
                        this.ucLabelEditItemCode.Value = "";
                        this.labelItemDescription.Text = "";
                    }
                    else
                    {
                        this.m_LotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
                        this.LoadSampleList(m_LotNo);
                        this.LoadCheckList(m_LotNo);
                        this.LoadBaseList(m_LotNo);
                        this.ucLabelEditLotNo.TextFocus(false, true);
                    }
                }
                else
                {
                    Messages messages = new Messages();
                    messages.Add(new UserControl.Message(MessageType.Error, "$NoLol2CartonInfo"));
                    ApplicationRun.GetInfoForm().Add(messages);
                    this.txtCartonCode.TextFocus(false, true);
                }
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_RunningCard"));
                this.ucLabelEditSizeAndCapacity.Value = "";
                this.m_LotNo = string.Empty;
                this.ucLabelEditItemCode.Value = "";
                this.labelItemDescription.Text = "";
                this.ucLabelEditRCard.TextFocus(false, true);
            }
        }

        private Messages LoadLotInfo()
        {
            Messages msg = new Messages();

            string lotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
            if (lotNo.Length > 0)
            {
                try
                {
                    OQCFacade oqcFacade = new OQCFacade(DataProvider);
                    object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(lotNo), OQCFacade.Lot_Sequence_Default);
                    if (obj == null)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                        this.ucLabelEditLotNo.TextFocus(false, true);
                        return msg;
                    }

                    object[] objs = oqcFacade.GetOQCLot2CardByLotNoAndSeq(FormatHelper.PKCapitalFormat(lotNo), OQCFacade.Lot_Sequence_Default);
                    if (objs == null || objs.Length == 0)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$Error_LotNoRCard"));
                        ucLabelEditLotNo.TextFocus(false, true);
                        return msg;
                    }

                    OQCLot lot = obj as OQCLot;

                    string itemCode = (objs[0] as OQCLot2Card).ItemCode;
                    ItemFacade itemFacade = new ItemFacade(this.DataProvider);

                    object item = itemFacade.GetItem(itemCode, lot.OrganizationID);
                    if (item == null)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$Error_ItemCode_NotExist $ItemCode=" + itemCode));
                        ucLabelEditLotNo.TextFocus(false, true);
                        return msg;
                    }

                    this.ucLabelEditItemCode.Value = (item as Item).ItemCode;
                    this.labelItemDescription.Text = (item as Item).ItemDescription;
                    this.ucLabelEditSizeAndCapacity.Value = lot.LotSize.ToString();// +"/" + lot.LotCapacity.ToString();
                }
                catch (Exception ex)
                {
                    msg.Add(new UserControl.Message(ex));
                    ucLabelEditLotNo.TextFocus(false, true);
                }
            }
            else
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"));
                this.ucLabelEditLotNo.TextFocus(false, true);
            }
            return msg;
        }

        private void InitializeSampleListGrid()
        {
            this.m_SampleList = new DataSet();
            this.m_CheckLOTNO = new DataTable("CheckLOTNO");
            this.m_CheckGrade = new DataTable("CheckGrade");
            this.m_CheckGroupOne = new DataTable("CheckGroup");

            this.m_CheckLOTNO.Columns.Add("CheckLOTNO", typeof(string));
            this.m_CheckLOTNO.Columns.Add("NGSampleCount", typeof(decimal));
            this.m_CheckLOTNO.Columns.Add("SampleCount", typeof(decimal));
            this.m_CheckLOTNO.Columns.Add("NGSampleRate", typeof(decimal));

            this.m_CheckGrade.Columns.Add("CheckGrade", typeof(string));
            this.m_CheckGrade.Columns.Add("DefectCount", typeof(decimal));
            this.m_CheckGrade.Columns.Add("DefeactRate", typeof(decimal));
            this.m_CheckGrade.Columns.Add("CheckLOTNO", typeof(string));

            this.m_CheckGroupOne.Columns.Add("CheckGroup", typeof(string));
            this.m_CheckGroupOne.Columns.Add("CheckItem", typeof(string));
            this.m_CheckGroupOne.Columns.Add("Count", typeof(decimal));
            this.m_CheckGroupOne.Columns.Add("CheckLOTNO", typeof(string));
            this.m_CheckGroupOne.Columns.Add("CheckGrade", typeof(string));

            this.m_SampleList.Tables.Add(this.m_CheckLOTNO);
            this.m_SampleList.Tables.Add(this.m_CheckGrade);
            this.m_SampleList.Tables.Add(this.m_CheckGroupOne);

            this.m_SampleList.Relations.Add(new DataRelation("SampleGroupAll",
                                                this.m_SampleList.Tables["CheckLOTNO"].Columns["CheckLOTNO"],
                                                this.m_SampleList.Tables["CheckGrade"].Columns["CheckLOTNO"]));

            this.m_SampleList.Relations.Add(new DataRelation("SampleGroupAllList",
                                              this.m_SampleList.Tables["CheckGrade"].Columns["CheckGrade"],
                                              this.m_SampleList.Tables["CheckGroup"].Columns["CheckGrade"]));

            this.m_SampleList.AcceptChanges();
            this.ultraGridHeader.DataSource = this.m_SampleList;
        }

        private void InitializeCheckListGrid()
        {
            this.m_CheckList = new DataSet();
            this.m_CheckRcard = new DataTable("CheckRCard");
            this.m_CheckNGCode = new DataTable("CheckNGCode");

            this.m_CheckRcard.Columns.Add("CheckRCard", typeof(string));

            this.m_CheckNGCode.Columns.Add("CheckNGCode", typeof(string));
            this.m_CheckNGCode.Columns.Add("CheckNGDESC", typeof(string));
            this.m_CheckNGCode.Columns.Add("CheckRCard", typeof(string));
            this.m_CheckNGCode.Columns.Add("CheckDate", typeof(int));
            this.m_CheckNGCode.Columns.Add("CheckTime", typeof(int));
            this.m_CheckNGCode.Columns.Add("CheckUser", typeof(string));

            this.m_CheckList.Tables.Add(this.m_CheckRcard);
            this.m_CheckList.Tables.Add(this.m_CheckNGCode);

            this.m_CheckList.Relations.Add("CheckRcardAndNGCode",
                this.m_CheckList.Tables["CheckRCard"].Columns["CheckRCard"],
                this.m_CheckList.Tables["CheckNGCode"].Columns["CheckRCard"]);

            this.m_CheckList.AcceptChanges();
            this.ultraGridDetail.DataSource = this.m_CheckList;
        }

        private void InitializeBaseListGrid()
        {
            this.m_BaseList = new DataSet();
            this.m_Checkgroup = new DataTable("CheckGroup");
            this.m_RcardAndResult = new DataTable("CheckRCard");

            this.m_Checkgroup.Columns.Add("CheckGroup", typeof(string));
            this.m_Checkgroup.Columns.Add("CheckedCount", typeof(string));
            this.m_Checkgroup.Columns.Add("NeedCheckCount", typeof(string));

            this.m_RcardAndResult.Columns.Add("CheckGroup", typeof(string));
            this.m_RcardAndResult.Columns.Add("CheckRCard", typeof(string));
            this.m_RcardAndResult.Columns.Add("CheckResult", typeof(string));
            this.m_RcardAndResult.Columns.Add("CheckDate", typeof(int));
            this.m_RcardAndResult.Columns.Add("CheckTime", typeof(int));
            this.m_RcardAndResult.Columns.Add("CheckUser", typeof(string));

            this.m_BaseList.Tables.Add(this.m_Checkgroup);
            this.m_BaseList.Tables.Add(this.m_RcardAndResult);

            this.m_BaseList.Relations.Add("CheckGroupAndRacrd",
                this.m_BaseList.Tables["CheckGroup"].Columns["CheckGroup"],
                this.m_BaseList.Tables["CheckRCard"].Columns["CheckGroup"]);

            this.m_BaseList.AcceptChanges();
            this.ultraGridBase.DataSource = this.m_BaseList;
        }

        private void ClearSampleList()
        {
            if (this.m_SampleList == null)
            {
                return;
            }

            this.m_SampleList.Tables["CheckGroup"].Rows.Clear();
            this.m_SampleList.Tables["CheckGrade"].Rows.Clear();
            this.m_SampleList.Tables["CheckLOTNO"].Rows.Clear();


            this.m_SampleList.Tables["CheckLOTNO"].AcceptChanges();
            this.m_SampleList.Tables["CheckGrade"].AcceptChanges();
            this.m_SampleList.Tables["CheckGroup"].AcceptChanges();
            this.m_SampleList.AcceptChanges();
        }
        private void ClearCheckList()
        {
            if (this.m_CheckList == null)
            {
                return;
            }

            this.m_CheckList.Tables["CheckNGCode"].Rows.Clear();
            this.m_CheckList.Tables["CheckRCard"].Rows.Clear();

            this.m_CheckList.Tables["CheckRCard"].AcceptChanges();
            this.m_CheckList.Tables["CheckNGCode"].AcceptChanges();
            this.m_CheckList.AcceptChanges();
        }

        private void ClearBaseList()
        {
            if (this.m_CheckList == null)
            {
                return;
            }

            this.m_BaseList.Tables["CheckRCard"].Rows.Clear();
            this.m_BaseList.Tables["CheckGroup"].Rows.Clear();

            this.m_BaseList.Tables["CheckRCard"].AcceptChanges();
            this.m_BaseList.Tables["CheckGroup"].AcceptChanges();
            this.m_BaseList.AcceptChanges();
        }

        private Messages LoadSampleList(string lotNo)
        {
            Messages msg = new Messages();
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            try
            {
                this.ClearSampleList();

                object CheckLOTNO = oqcFacade.Gettbllot2cardcheckInOQCQuery(lotNo);
                if (CheckLOTNO != null)
                {
                    DataRow rowLotNo;
                    rowLotNo = this.m_SampleList.Tables["CheckLOTNO"].NewRow();
                    rowLotNo["CheckLOTNO"] = this.ucLabelEditLotNo.Value.ToString();
                    rowLotNo["NGSampleCount"] = ((OQCCheckSample)CheckLOTNO).NGSampleCount;
                    rowLotNo["SampleCount"] = ((OQCCheckSample)CheckLOTNO).SampleCount;
                    rowLotNo["NGSampleRate"] = (((OQCCheckSample)CheckLOTNO).NGSampleCount * 1000000 / ((OQCCheckSample)CheckLOTNO).SampleCount);
                    this.m_SampleList.Tables["CheckLOTNO"].Rows.Add(rowLotNo);

                    object[] CheckGrade = oqcFacade.GetGradeAndNGCountINOQCQuery(lotNo);
                    object[] CheckGroup = oqcFacade.GetOQCcardLotcklistINOQCQuery(lotNo);
                    if (CheckGrade != null)
                    {
                        DataRow rowGrade;
                        foreach (OQCCheckGradeAndCount oqcCheckGradeAndCount in CheckGrade)
                        {
                            if (oqcCheckGradeAndCount.Grade == string.Empty)
                            {
                                continue;
                            }
                            rowGrade = this.m_SampleList.Tables["CheckGrade"].NewRow();
                            rowGrade["CheckGrade"] = oqcCheckGradeAndCount.Grade;
                            rowGrade["DefectCount"] = oqcCheckGradeAndCount.NGCount;
                            rowGrade["DefeactRate"] = (oqcCheckGradeAndCount.NGCount * 1000000 / ((OQCCheckSample)CheckLOTNO).SampleCount) ;
                            rowGrade["CheckLOTNO"] = this.ucLabelEditLotNo.Value.ToString();
                            this.m_SampleList.Tables["CheckGrade"].Rows.Add(rowGrade);
                        }
                        if (CheckGroup != null)
                        {
                            DataRow rowCheckgroup;
                            foreach (OQCCheckListAndCount oqcCheckListAndCount in CheckGroup)
                            {
                                rowCheckgroup = this.m_SampleList.Tables["CheckGroup"].NewRow();
                                rowCheckgroup["CheckGroup"] = oqcCheckListAndCount.CheckGroup;
                                rowCheckgroup["CheckItem"] = oqcCheckListAndCount.CheckItemCode;
                                rowCheckgroup["Count"] = oqcCheckListAndCount.NGCount;
                                rowCheckgroup["CheckGrade"] = oqcCheckListAndCount.Grade;
                                rowCheckgroup["CheckLOTNO"] = this.ucLabelEditLotNo.Value.ToString();
                                this.m_SampleList.Tables["CheckGroup"].Rows.Add(rowCheckgroup);
                            }
                        }
                    }
                    if (CheckLOTNO != null)
                        this.m_SampleList.Tables["CheckGroup"].AcceptChanges();
                    if (CheckGrade != null)
                        this.m_SampleList.Tables["CheckGrade"].AcceptChanges();
                    if (CheckGroup != null)
                        this.m_SampleList.Tables["CheckGroup"].AcceptChanges();
                    this.m_SampleList.AcceptChanges();
                    this.ultraGridHeader.DataSource = this.m_SampleList;
                    //this.ultraGridHeader.Rows.ExpandAll(true);
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }
            return msg;
        }

        private Messages LoadCheckList(string lotNo)
        {
            Messages msg = new Messages();
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            try
            {
                this.ClearCheckList();
                object[] checkRcard = oqcFacade.GetRcardFromOQCLotCard2ErrorCodeByLotNo(lotNo);
                if (checkRcard != null)
                {
                    DataRow rowRcard;
                    foreach (OQCLotCard2ErrorCode lot2CheckGroup in checkRcard)
                    {
                        rowRcard = this.m_CheckList.Tables["CheckRCard"].NewRow();
                        rowRcard["CheckRCard"] = lot2CheckGroup.RunningCard;
                        this.m_CheckList.Tables["CheckRCard"].Rows.Add(rowRcard);
                    }

                    object[] CheckError = oqcFacade.GetErrorCodeFromTblecAndtbloqclotcard2errorcodeByLotNo(lotNo);
                    if (CheckError != null)
                    {
                        DataRow rowErrorCode;
                        foreach (OQCLotCard2ErrorCodeAndECDESC oqcLot2CardCheckAndCheckGroup in CheckError)
                        {
                            rowErrorCode = this.m_CheckList.Tables["CheckNGCode"].NewRow();
                            rowErrorCode["CheckRCard"] = oqcLot2CardCheckAndCheckGroup.RunningCard;
                            rowErrorCode["CheckNGCode"] = oqcLot2CardCheckAndCheckGroup.ErrorCode;
                            rowErrorCode["CheckDate"] = oqcLot2CardCheckAndCheckGroup.MaintainDate;
                            rowErrorCode["CheckTime"] = oqcLot2CardCheckAndCheckGroup.MaintainTime;
                            rowErrorCode["CheckUser"] = oqcLot2CardCheckAndCheckGroup.MaintainUser;
                            rowErrorCode["CheckNGDESC"] = oqcLot2CardCheckAndCheckGroup.ECDESC;
                            this.m_CheckList.Tables["CheckNGCode"].Rows.Add(rowErrorCode);
                        }                       
                    }

                    if (checkRcard != null)
                        this.m_CheckList.Tables["CheckRCard"].AcceptChanges();
                    if (CheckError != null)
                        this.m_CheckList.Tables["CheckNGCode"].AcceptChanges();
                    this.m_CheckList.AcceptChanges();
                    this.ultraGridDetail.DataSource = this.m_CheckList;
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }
            return msg;
        }


        private Messages LoadBaseList(string lotNo)
        {
            Messages msg = new Messages();
            OQCFacade oqcFacade = new OQCFacade(this.DataProvider);
            try
            {
                this.ClearBaseList();

                object[] checkGroups = oqcFacade.GetCheckGroupListInOQCQuery(lotNo);
                if (checkGroups != null)
                {
                    DataRow rowGroup;
                    foreach (OQCLot2CheckGroup lot2CheckGroup in checkGroups)
                    {
                        rowGroup = this.m_BaseList.Tables["CheckGroup"].NewRow();
                        rowGroup["CheckGroup"] = lot2CheckGroup.CheckGroup;
                        rowGroup["CheckedCount"] = lot2CheckGroup.CheckedCount;
                        rowGroup["NeedCheckCount"] = lot2CheckGroup.NeedCheckCount;
                        this.m_BaseList.Tables["CheckGroup"].Rows.Add(rowGroup);
                    }

                    object[] CheckRCards = oqcFacade.GetCheckGroupAndRcard(lotNo);
                    if (CheckRCards != null)
                    {
                        DataRow rowRCards;
                        foreach (OQCLot2CardCheckAndCheckGroup oqcLot2CardCheckAndCheckGroup in CheckRCards)
                        {
                            bool CheckAdd = true;
                            foreach (DataRow rowRCard in this.m_BaseList.Tables["CheckRCard"].Rows)
                            {

                                if (rowRCard["CheckRCard"].ToString().ToUpper()== oqcLot2CardCheckAndCheckGroup.RunningCard.ToString().ToUpper()
                                    && rowRCard["CheckGroup"].ToString().ToUpper() == oqcLot2CardCheckAndCheckGroup.CheckGroup.ToString().ToUpper()
                                    && rowRCard["CheckResult"].ToString().ToUpper() == "GOOD"
                                    && oqcLot2CardCheckAndCheckGroup.Result.ToString().ToUpper()=="NG")
                                {
                                    rowRCard["CheckResult"] = "NG";
                                    CheckAdd = false;
                                }
                                else if (rowRCard["CheckRCard"].ToString().ToUpper() == oqcLot2CardCheckAndCheckGroup.RunningCard.ToString().ToUpper()
                                         && rowRCard["CheckGroup"].ToString().ToUpper() == oqcLot2CardCheckAndCheckGroup.CheckGroup.ToString().ToUpper()
                                         && rowRCard["CheckResult"].ToString().ToUpper() == "GOOD"
                                        && oqcLot2CardCheckAndCheckGroup.Result.ToString().ToUpper() == "GOOD")
                                {
                                    CheckAdd = false;
                                }
                                else if (rowRCard["CheckRCard"].ToString().ToUpper() == oqcLot2CardCheckAndCheckGroup.RunningCard.ToString().ToUpper()
                                         && rowRCard["CheckGroup"].ToString().ToUpper() == oqcLot2CardCheckAndCheckGroup.CheckGroup.ToString().ToUpper()
                                         && rowRCard["CheckResult"].ToString().ToUpper() == "NG"
                                         && oqcLot2CardCheckAndCheckGroup.Result.ToString().ToUpper() == "NG")
                                {
                                    CheckAdd = false;
                                }
                                else if (rowRCard["CheckRCard"].ToString().ToUpper() == oqcLot2CardCheckAndCheckGroup.RunningCard.ToString().ToUpper()
                                        && rowRCard["CheckGroup"].ToString().ToUpper() == oqcLot2CardCheckAndCheckGroup.CheckGroup.ToString().ToUpper()
                                         && rowRCard["CheckResult"].ToString().ToUpper() == "NG"
                                         && oqcLot2CardCheckAndCheckGroup.Result.ToString().ToUpper() == "GOOD")
                                {
                                    CheckAdd = false;
                                }     
                            }

                            if (CheckAdd == true)
                            {
                                rowRCards = this.m_BaseList.Tables["CheckRCard"].NewRow();
                                rowRCards["CheckGroup"] = oqcLot2CardCheckAndCheckGroup.CheckGroup;
                                rowRCards["CheckRCard"] = oqcLot2CardCheckAndCheckGroup.RunningCard;
                                rowRCards["CheckResult"] = oqcLot2CardCheckAndCheckGroup.Result;
                                rowRCards["CheckDate"] = oqcLot2CardCheckAndCheckGroup.MaintainDate;
                                rowRCards["CheckTime"] = oqcLot2CardCheckAndCheckGroup.MaintainTime;
                                rowRCards["CheckUser"] = oqcLot2CardCheckAndCheckGroup.MaintainUser;
                                //rowRCards["CheckSeq"] = oqcLot2CardCheckAndCheckGroup.CheckSequence;
                                //rowRCards["CheckRcardSeq"] = oqcLot2CardCheckAndCheckGroup.RunningCardSequence;
                                this.m_BaseList.Tables["CheckRCard"].Rows.Add(rowRCards);
                            }
                        }
                    }

                    if (checkGroups != null)
                        this.m_BaseList.Tables["CheckGroup"].AcceptChanges();
                    if (CheckRCards != null)
                        this.m_BaseList.Tables["CheckRCard"].AcceptChanges();

                    this.m_BaseList.AcceptChanges();
                    this.ultraGridBase.DataSource = this.m_BaseList;
                }
            }
            catch (Exception ex)
            {
                msg.Add(new UserControl.Message(ex));
            }
            return msg;
        }

        private void ultraGridHeader_BeforeRowFilterDropDownPopulate(object sender, BeforeRowFilterDropDownPopulateEventArgs e)
        {
            e.ValueList.ValueListItems.RemoveAt(1);
            e.ValueList.ValueListItems.RemoveAt(1);
            e.ValueList.ValueListItems.RemoveAt(1);
        }

        private void ultraGridDetail_BeforeRowFilterDropDownPopulate(object sender, BeforeRowFilterDropDownPopulateEventArgs e)
        {
            e.ValueList.ValueListItems.RemoveAt(1);
            e.ValueList.ValueListItems.RemoveAt(1);       
        }

        private void ultraGridBase_BeforeRowFilterDropDownPopulate(object sender, BeforeRowFilterDropDownPopulateEventArgs e)
        {
            e.ValueList.ValueListItems.RemoveAt(1);
            e.ValueList.ValueListItems.RemoveAt(1);     
        }

        private void txtCartonCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.GetLotNo();
            }
        }

      
    }
}