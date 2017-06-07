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
    public partial class FItemCodeQuery : BaseForm
    {
        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> ItemCodeSelectedEvent;
        private DataTable m_ItemCode = null;
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

        public FItemCodeQuery()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridItem.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridItem.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridItem.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridItem.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridItem.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridItem.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridItem.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridItem.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridItem.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FMCodeQuery_Load(object sender, EventArgs e)
        {
            this.InitialBigSSCoderGrid();
            this.DoQuery();
            //this.InitPageLanguage();
        }

        private void TxtBigSSCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_ItemCode.DefaultView.RowFilter = " ItemCode like '" + Common.Helper.CommonHelper.Convert(this.TxtItemCode.Value.Trim().ToUpper()) + "%' AND ItemDesc Like '" + Common.Helper.CommonHelper.Convert(this.txtItemDesc.Value.Trim().ToUpper()) + "%'";
        }

        private void TxtVendorName_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_ItemCode.DefaultView.RowFilter = " ItemCode like '" + Common.Helper.CommonHelper.Convert(this.TxtItemCode.Value.Trim().ToUpper()) + "%' AND ItemDesc Like '" + Common.Helper.CommonHelper.Convert(this.txtItemDesc.Value.Trim().ToUpper()) + "%'";
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridItem.Rows.Count == 0)
            {
                return;
            }

            string itemCode = string.Empty;
            for (int i = 0; i < ultraGridItem.Rows.Count; i++)
            {
                if (ultraGridItem.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    itemCode += "," + ultraGridItem.Rows[i].Cells[1].Text.Trim().ToUpper();
                }
            }

            if (itemCode.Length > 0)
            {
                itemCode = itemCode.Substring(1);
            }
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(itemCode);
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
            e.Layout.Bands[0].ScrollTipField = "ItemCode";
            e.Layout.Bands[0].ScrollTipField = "ItemDesc";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["ItemCode"].Header.Caption = "产品代码";
            e.Layout.Bands[0].Columns["ItemDesc"].Header.Caption = "产品描述";

            e.Layout.Bands[0].Columns["Checked"].Width = 30;
            e.Layout.Bands[0].Columns["ItemCode"].Width = 200;
            e.Layout.Bands[0].Columns["ItemDesc"].Width = 200;

            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["ItemCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["ItemDesc"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["ItemCode"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridItem);
        }

        #endregion 

        #region 自定义事件

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.ItemCodeSelectedEvent != null)
            {
                ItemCodeSelectedEvent(sender, e);
            }
        }

        private void InitialBigSSCoderGrid()
        {
            this.m_ItemCode = new DataTable();

            this.m_ItemCode.Columns.Add("Checked", typeof(bool));
            this.m_ItemCode.Columns.Add("ItemCode", typeof(string));
            this.m_ItemCode.Columns.Add("ItemDesc", typeof(string));

            this.m_ItemCode.AcceptChanges();

            this.ultraGridItem.DataSource = this.m_ItemCode;
        }

        private void DoQuery()
        {
            if (m_ItemCode != null)
            {
                this.m_ItemCode.Rows.Clear();
                this.m_ItemCode.AcceptChanges();
            }

            m_ItemFacade = new ItemFacade(this.DataProvider);
            object[] MCodeList = this.m_ItemFacade.GetAllItem();// this.m_MoFacade.get
            if (MCodeList != null)
            {
                DataRow rowNew;
                foreach (Domain.MOModel.Item m in MCodeList)
                {
                    rowNew = this.m_ItemCode.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["ItemCode"] = m.ItemCode;
                    rowNew["ItemDesc"] = m.ItemDescription;
                    this.m_ItemCode.Rows.Add(rowNew);
                }
                this.m_ItemCode.AcceptChanges();
            }
            this.ultraGridItem.ActiveRow = null;
        }

        #endregion 
    }
}