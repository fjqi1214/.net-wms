using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Performance;
using BenQGuru.eMES.Performance;
using BenQGuru.eMES.Web.Helper;
using UserControl;


namespace BenQGuru.eMES.Client
{
    public partial class FOnOffPostByCrew : BaseForm
    {
        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;
        private PerformanceFacade _PerformanceFacade = null;

        private DataSet _OnPostList = null;
        private DataTable _OnPostListHead = null;
        private DataTable _OnPostListDetail = null;

        public FOnOffPostByCrew()
        {
            InitializeComponent();

            _PerformanceFacade = new PerformanceFacade(this.DataProvider);

            this.ultraGridOnPost.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.ultraGridOnPost.UpdateMode = UpdateMode.OnCellChange;
        }

        #region Properties And Common Functions

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _DomainDataProvider;
            }
        }

        protected void ShowMessage(string message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        protected void ShowMessage(UserControl.Messages messages)
        {
            ApplicationRun.GetInfoForm().Add(messages);
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        #endregion

        #region Events

        private void FOnOffPostByCrew_Load(object sender, EventArgs e)
        {
            RefreshControlValue();

            InitOnPostGrid();

            RefreshOnPostGrid();
            //this.InitPageLanguage();
        }

        private void ultraGridOnPost_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "SSCodeDisplay";
            e.Layout.Bands[1].ScrollTipField = "UserCodeDisplay";

            // 设置列宽和列名称，是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

            e.Layout.Bands[0].Columns["SSCode"].Hidden = true;

            e.Layout.Bands[0].Columns["SSCodeDisplay"].Header.Caption = "产线";
            e.Layout.Bands[0].Columns["SSCodeDisplay"].Width = 180;
            e.Layout.Bands[0].Columns["SSCodeDisplay"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["SSCodeDisplay"].SortIndicator = SortIndicator.Ascending;

            e.Layout.Bands[0].Columns["ManCount"].Header.Caption = "在岗人数";
            e.Layout.Bands[0].Columns["ManCount"].Width = 70;
            e.Layout.Bands[0].Columns["ManCount"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;


            e.Layout.Bands[1].Columns["UserCodeDisplay"].Header.Caption = "在岗人员";
            e.Layout.Bands[1].Columns["UserCodeDisplay"].Width = 120;
            e.Layout.Bands[1].Columns["UserCodeDisplay"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["UserCodeDisplay"].SortIndicator = SortIndicator.Ascending;

            e.Layout.Bands[1].Columns["OPCodeDisplay"].Header.Caption = "工序";
            e.Layout.Bands[1].Columns["OPCodeDisplay"].Width = 150;
            e.Layout.Bands[1].Columns["OPCodeDisplay"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["ResCodeDisplay"].Header.Caption = "资源";
            e.Layout.Bands[1].Columns["ResCodeDisplay"].Width = 150;
            e.Layout.Bands[1].Columns["ResCodeDisplay"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["StatusDisplay"].Header.Caption = "人员状态";
            e.Layout.Bands[1].Columns["StatusDisplay"].Width = 60;
            e.Layout.Bands[1].Columns["StatusDisplay"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["OnDate"].Header.Caption = "上岗日期";
            e.Layout.Bands[1].Columns["OnDate"].Width = 70;
            e.Layout.Bands[1].Columns["OnDate"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["OnTime"].Header.Caption = "上岗时间";
            e.Layout.Bands[1].Columns["OnTime"].Width = 70;
            e.Layout.Bands[1].Columns["OnTime"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["ShiftCode"].Header.Caption = "班次";
            e.Layout.Bands[1].Columns["ShiftCode"].Width = 150;
            e.Layout.Bands[1].Columns["ShiftCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["SSCode"].Hidden = true;
            e.Layout.Bands[1].Columns["UserCode"].Hidden = true;
            e.Layout.Bands[1].Columns["Status"].Hidden = true;
            //this.InitGridLanguage(ultraGridOnPost);
        }

        private void ucButtonRefresh_Click(object sender, EventArgs e)
        {
            RefreshOnPostGrid();
        }

        private void ucButtonGoOffPost_Click(object sender, EventArgs e)
        {
            List<string> ssCodeList = GetSelectedSSCode();

            if (ssCodeList.Count <= 0)
            {
                ShowMessage(new UserControl.Message(MessageType.Error, "$Message_AtLeastOneEffectiveRecord"));
                return;
            }

            Messages msgs = new Messages();
            foreach (string ssCode in ssCodeList)
            {
                msgs.AddMessages(_PerformanceFacade.GoOffPost(ssCode, ApplicationService.Current().UserCode));
            }

            ShowMessage(msgs);

            RefreshOnPostGrid();
        }

        private void ucButtonPause_Click(object sender, EventArgs e)
        {
            List<string> ssCodeList = GetSelectedSSCode();

            if (ssCodeList.Count <= 0)
            {
                ShowMessage(new UserControl.Message(MessageType.Error, "$Message_AtLeastOneEffectiveRecord"));
                return;
            }

            Messages msgs = new Messages();
            foreach (string ssCode in ssCodeList)
            {
                msgs.AddMessages(_PerformanceFacade.PauseUserOnPost(ssCode, ApplicationService.Current().UserCode));
            }

            ShowMessage(msgs);

            RefreshOnPostGrid();
        }

        private void ucButtonCancelPause_Click(object sender, EventArgs e)
        {
            List<string> ssCodeList = GetSelectedSSCode();

            if (ssCodeList.Count <= 0)
            {
                ShowMessage(new UserControl.Message(MessageType.Error, "$Message_AtLeastOneEffectiveRecord"));
                return;
            }

            Messages msgs = new Messages();
            foreach (string ssCode in ssCodeList)
            {
                msgs.AddMessages(_PerformanceFacade.CancelPauseUserOnPost(ssCode, ApplicationService.Current().UserCode));
            }

            ShowMessage(msgs);

            RefreshOnPostGrid();
        }

        #endregion

        #region On/Off Post functions

        private void RefreshControlValue()
        {
            DBDateTime nowDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            OnOffPostEnvirenment env = new OnOffPostEnvirenment();

            if (env.Init(this.DataProvider, ApplicationService.Current().ResourceCode, nowDateTime))
            {
                ucLabelEditDate.Value = FormatHelper.ToDateString(env.ShiftDate);
                ucLabelEditShiftCode.Value = env.Shift.ShiftCode;
                ucLabelEditBigSSCode.Value = env.StepSequence.BigStepSequenceCode;

                Line2Crew line2Crew = (Line2Crew)_PerformanceFacade.GetLine2Crew(env.ShiftDate, env.StepSequence.StepSequenceCode, env.Shift.ShiftCode);
                if (line2Crew != null)
                {
                    ucLabelEditCrewCode.Value = line2Crew.CrewCode;
                }
            }
        }

        private void InitOnPostGrid()
        {
            _OnPostList = new DataSet();
            _OnPostListHead = new DataTable("Head");
            _OnPostListDetail = new DataTable("Detail");

            _OnPostListHead.Columns.Add("Checked", typeof(string));
            _OnPostListHead.Columns.Add("SSCode", typeof(string));
            _OnPostListHead.Columns.Add("SSCodeDisplay", typeof(string));
            _OnPostListHead.Columns.Add("ManCount", typeof(string));

            _OnPostListDetail.Columns.Add("UserCode", typeof(string));
            _OnPostListDetail.Columns.Add("UserCodeDisplay", typeof(string));
            _OnPostListDetail.Columns.Add("OPCodeDisplay", typeof(string));
            _OnPostListDetail.Columns.Add("ResCodeDisplay", typeof(string));
            _OnPostListDetail.Columns.Add("Status", typeof(string));
            _OnPostListDetail.Columns.Add("StatusDisplay", typeof(string));
            _OnPostListDetail.Columns.Add("OnDate", typeof(string));
            _OnPostListDetail.Columns.Add("OnTime", typeof(string));
            _OnPostListDetail.Columns.Add("ShiftCode", typeof(string));
            _OnPostListDetail.Columns.Add("SSCode", typeof(string));


            _OnPostList.Tables.Add(_OnPostListHead);
            _OnPostList.Tables.Add(_OnPostListDetail);

            _OnPostList.Relations.Add(new DataRelation("Head2Detail",
                                                _OnPostList.Tables["Head"].Columns["SSCode"],
                                                _OnPostList.Tables["Detail"].Columns["SSCode"]));
            _OnPostList.AcceptChanges();
            ultraGridOnPost.DataSource = _OnPostList;
        }

        private void ClearOnPostGrid()
        {
            if (this._OnPostList == null)
            {
                return;
            }

            this._OnPostList.Tables["Detail"].Rows.Clear();
            this._OnPostList.Tables["Head"].Rows.Clear();

            this._OnPostList.Tables["Detail"].AcceptChanges();
            this._OnPostList.Tables["Head"].AcceptChanges();

            this._OnPostList.AcceptChanges();
        }

        private void LoadOnPostList()
        {
            try
            {
                ClearOnPostGrid();

                object[] ssOnPostList = _PerformanceFacade.QueryLineOnPostAndPauseList(string.Empty, -1, string.Empty, ucLabelEditBigSSCode.Value);

                if (ssOnPostList != null)
                {
                    List<string> ssCodeList = new List<string>();
                    foreach (StepSequence stepSequence in ssOnPostList)
                    {
                        DataRow newRow = _OnPostList.Tables["Head"].NewRow();
                        newRow["Checked"] = "false";
                        newRow["SSCode"] = stepSequence.StepSequenceCode;
                        newRow["SSCodeDisplay"] = stepSequence.GetDisplayText("StepSequenceCode");
                        newRow["ManCount"] = stepSequence.EAttribute1;
                        _OnPostList.Tables["Head"].Rows.Add(newRow);

                        if (!ssCodeList.Contains(stepSequence.StepSequenceCode))
                        {
                            ssCodeList.Add(stepSequence.StepSequenceCode);
                        }
                    }

                    if (ssCodeList.Count > 0)
                    {
                        foreach (string ssCode in ssCodeList)
                        {
                            object[] line2ManDetailList = _PerformanceFacade.QueryUserCurrentLine2ManDetailEx(string.Empty, ssCode, string.Empty, string.Empty, -1, string.Empty, string.Empty);

                            if (line2ManDetailList != null)
                            {
                                foreach (Line2ManDetailEx line2ManDetailEx in line2ManDetailList)
                                {
                                    if (line2ManDetailEx.Status == Line2ManDetailStatus.Line2ManDetailStatus_On
                                        || line2ManDetailEx.Status == Line2ManDetailStatus.Line2ManDetailStatus_AutoOn
                                        || line2ManDetailEx.Status == Line2ManDetailStatus.Line2ManDetailStatus_Pause)
                                    {

                                        DataRow row = _OnPostList.Tables["Detail"].NewRow();
                                        row["UserCode"] = line2ManDetailEx.UserCode;
                                        row["UserCodeDisplay"] = line2ManDetailEx.UserCode + " - " + line2ManDetailEx.UserName;
                                        row["OPCodeDisplay"] = line2ManDetailEx.OPCode + " - " + line2ManDetailEx.OPDesc;
                                        row["ResCodeDisplay"] = line2ManDetailEx.ResourceCode + " - " + line2ManDetailEx.ResDesc;
                                        row["Status"] = line2ManDetailEx.Status;
                                        row["StatusDisplay"] = UserControl.MutiLanguages.ParserString(line2ManDetailEx.Status);
                                        row["OnDate"] = FormatHelper.ToDateString(line2ManDetailEx.OnDate);
                                        row["OnTime"] = FormatHelper.ToTimeString(line2ManDetailEx.OnTime);
                                        row["ShiftCode"] = line2ManDetailEx.ShiftCode + " - " + line2ManDetailEx.ShiftDesc;
                                        row["SSCode"] = line2ManDetailEx.SSCode;
                                        _OnPostList.Tables["Detail"].Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }

                    _OnPostList.Tables["Head"].AcceptChanges();
                    _OnPostList.Tables["Detail"].AcceptChanges();
                    _OnPostList.AcceptChanges();

                    ultraGridOnPost.DataSource = _OnPostList;
                }
            }
            catch (Exception ex)
            {
                ShowMessage(new UserControl.Message(ex));
            }
        }

        private void RefreshOnPostGrid()
        {
            ClearOnPostGrid();
            LoadOnPostList();
        }

        private List<string> GetSelectedSSCode()
        {
            List<string> returnValue = new List<string>();
            for (int i = 0; i < ultraGridOnPost.Rows.Count; i++)
            {
                UltraGridRow row = ultraGridOnPost.Rows[i];

                if (Convert.ToBoolean(row.Cells["Checked"].Text))
                {
                    if (!returnValue.Contains(row.Cells["SSCode"].Text))
                    {
                        returnValue.Add(row.Cells["SSCode"].Text);
                    }
                }
            }

            return returnValue;
        }

        #endregion
    }
}