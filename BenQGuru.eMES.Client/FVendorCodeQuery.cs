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
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Client
{
    public partial class FVendorCodeQuery : BaseForm
    {
        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> BigSSCodeSelectedEvent;
        private DataTable m_VendorCode = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private ItemFacade m_ItemFacade;

        #endregion

        #region 属性

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }


        private ItemFacade baseModelFacade
        {
            get
            {
                if (this.m_ItemFacade == null)
                {
                    this.m_ItemFacade = new ItemFacade(this.DataProvider);
                }
                return m_ItemFacade;
            }
        }

        #endregion

        #region 页面事件

        public FVendorCodeQuery()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridBigSSCode.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridBigSSCode.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridBigSSCode.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridBigSSCode.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridBigSSCode.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridBigSSCode.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridBigSSCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridBigSSCode.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridBigSSCode.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridBigSSCode.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FBigSSCodeQuery_Load(object sender, EventArgs e)
        {
            this.InitialBigSSCoderGrid();
            this.DoQuery();
            //this.InitPageLanguage();
            //this.InitGridLanguage(ultraGridBigSSCode);
        }

        private void TxtBigSSCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_VendorCode.DefaultView.RowFilter = " VendorCode like '" + Common.Helper.CommonHelper.Convert(this.TxtVendorCode.Value.Trim().ToUpper()) + "%' AND VendorName Like '" + Common.Helper.CommonHelper.Convert(this.txtVendorName.Value.Trim().ToUpper()) + "%'";
        }

        private void TxtVendorName_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_VendorCode.DefaultView.RowFilter = " VendorCode like '" + Common.Helper.CommonHelper.Convert(this.TxtVendorCode.Value.Trim().ToUpper()) + "%' AND VendorName Like '" + Common.Helper.CommonHelper.Convert(this.txtVendorName.Value.Trim().ToUpper()) + "%'";
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridBigSSCode.Rows.Count == 0)
            {
                return;
            }

            string bigSSCode = string.Empty;
            for (int i = 0; i < ultraGridBigSSCode.Rows.Count; i++)
            {
                if (ultraGridBigSSCode.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    bigSSCode += "," + ultraGridBigSSCode.Rows[i].Cells[1].Text.Trim().ToUpper();
                }
            }

            if (bigSSCode.Length > 0)
            {
                bigSSCode = bigSSCode.Substring(1);
            }
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(bigSSCode);
            this.OnSoftVersionSelectedEvent(sender, args);
            this.Close();
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {

        }

        private void ultraGridBigSSCode_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
            e.Layout.Bands[0].ScrollTipField = "VendorCode";
            e.Layout.Bands[0].ScrollTipField = "VendorName";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["VendorCode"].Header.Caption = "供应商代码";
            e.Layout.Bands[0].Columns["VendorName"].Header.Caption = "供应商名称";

            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["VendorCode"].Width = 200;
            e.Layout.Bands[0].Columns["VendorName"].Width = 200;

            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["VendorCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["VendorName"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["VendorCode"].SortIndicator = SortIndicator.Ascending;
        }

        #endregion 

        #region 自定义事件

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.BigSSCodeSelectedEvent != null)
            {
                BigSSCodeSelectedEvent(sender, e);
            }
        }

        private void InitialBigSSCoderGrid()
        {
            this.m_VendorCode = new DataTable();

            this.m_VendorCode.Columns.Add("Checked", typeof(bool));
            this.m_VendorCode.Columns.Add("VendorCode", typeof(string));
            this.m_VendorCode.Columns.Add("VendorName", typeof(string));

            this.m_VendorCode.AcceptChanges();

            this.ultraGridBigSSCode.DataSource = this.m_VendorCode;
        }

        private void DoQuery()
        {
            if (m_VendorCode != null)
            {
                this.m_VendorCode.Rows.Clear();
                this.m_VendorCode.AcceptChanges();
            }

            m_ItemFacade = new ItemFacade(this.DataProvider);
            object[] bigSSCodeList = this.m_ItemFacade.GetAllVender();
            if (bigSSCodeList != null)
            {
                DataRow rowNew;
                foreach (Vendor ss in bigSSCodeList)
                {
                    rowNew = this.m_VendorCode.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["VendorCode"] = ss.VendorCode;
                    rowNew["VendorName"] = ss.VendorName;
                    this.m_VendorCode.Rows.Add(rowNew);
                }
                this.m_VendorCode.AcceptChanges();
            }
            this.ultraGridBigSSCode.ActiveRow = null;
        }

        #endregion 
    }
}