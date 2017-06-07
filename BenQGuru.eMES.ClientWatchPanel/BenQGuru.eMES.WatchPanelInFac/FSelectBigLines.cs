using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class FSelectBigLines : Form
    {
        #region  变量 属性
        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> BiglineSelectedEvent;
        private DataTable m_BigLine = null;

        private IDomainDataProvider _dataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_dataProvider == null)
                {

                    _dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();
                }

                return _dataProvider;
            }

            set
            {
                _dataProvider = value;
            }
        }

        #endregion

        #region 页面事件

        public FSelectBigLines()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridBigLines.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridBigLines.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridBigLines.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridBigLines.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridBigLines.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridBigLines.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridBigLines.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridBigLines.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridBigLines.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridBigLines.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");           
        }

        private void FSelectBigLines_Load(object sender, EventArgs e)
        {
            InitialTryLotNorGrid();
            DoQuery();
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridBigLines.Rows.Count == 0)
            {
                return;
            }

            string bigLineList = string.Empty;
            for (int i = 0; i < ultraGridBigLines.Rows.Count; i++)
            {
                if (ultraGridBigLines.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    bigLineList += "," + ultraGridBigLines.Rows[i].Cells["BigLine"].Text.Trim().ToUpper();
                }
            }

            if (bigLineList.Length > 0)
            {
                bigLineList = bigLineList.Substring(1);
            }

            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(bigLineList);
            this.OnBigLineSelectedEvent(sender, args);
            this.Close();
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {

        }

        private void ultraGridBigLines_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 排序
            e.Layout.Override.HeaderClickAction = HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "BigLine";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["BigLine"].Header.Caption = "大线";
            e.Layout.Bands[0].Columns["BigLineDesc"].Header.Caption = "大线描述";

            e.Layout.Bands[0].Columns["Checked"].Width = 50;
            e.Layout.Bands[0].Columns["BigLine"].Width = 150;
            e.Layout.Bands[0].Columns["BigLineDesc"].Width =200;

            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["BigLine"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["BigLineDesc"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["BigLine"].SortIndicator = SortIndicator.Ascending;
        }

        private void TxtBigLine_InnerTextChanged(object sender, EventArgs e)
        {
            m_BigLine.DefaultView.RowFilter = "BigLine like '" + this.TxtBigLine.Value.Trim().ToUpper() + "%'";
        }

        #endregion

        #region 自定义事件
        private void InitialTryLotNorGrid()
        {
            this.m_BigLine = new DataTable();

            this.m_BigLine.Columns.Add("Checked", typeof(bool));
            this.m_BigLine.Columns.Add("BigLine", typeof(string));
            this.m_BigLine.Columns.Add("BigLineDesc", typeof(string));

            this.m_BigLine.AcceptChanges();

            this.ultraGridBigLines.DataSource = this.m_BigLine;
        }

        public void OnBigLineSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.BiglineSelectedEvent != null)
            {
                BiglineSelectedEvent(sender, e);
            }
        }

        private void DoQuery()
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            object[] bigLineList = systemSettingFacade.GetAllBIGSSCODE();
            if (bigLineList != null)
            {
                DataRow rowNew;
                foreach (Parameter pm in bigLineList)
                {
                    rowNew = this.m_BigLine.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["BigLine"] = pm.ParameterAlias;
                    rowNew["BigLineDesc"] = pm.ParameterDescription;
                    this.m_BigLine.Rows.Add(rowNew);
                }
                this.m_BigLine.AcceptChanges();
            }
            this.ultraGridBigLines.ActiveRow = null;
        }

        #endregion

        
    }
}