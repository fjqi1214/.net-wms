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
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Client
{
    public partial class FStorageCodeQuery : Form
    {
        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> BigSSCodeSelectedEvent;
        private DataTable m_StroageDataTable = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private InventoryFacade m_InventoryFacade;
        private int orgID = -1;
        #endregion

        #region 属性

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }


        private InventoryFacade baseModelFacade
        {
            get
            {
                if (this.m_InventoryFacade == null)
                {
                    this.m_InventoryFacade = new InventoryFacade(this.DataProvider);
                }
                return m_InventoryFacade;
            }
        }

        #endregion

        #region 页面事件

        public FStorageCodeQuery()
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

        public FStorageCodeQuery(int orgid)
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
            this.orgID = orgid;
        }

        private void FStorageCodeQuery_Load(object sender, EventArgs e)
        {
            this.InitialBigSSCoderGrid();
            if (orgID > -1)
            {
                this.DoQueryForOrgid();
            }
            else
            {
                this.DoQuery();
            }
        }

        private void TxtBigSSCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_StroageDataTable.DefaultView.RowFilter = " StorageCode like '" + Common.Helper.CommonHelper.Convert(this.TxtBigSSCode.Value.Trim().ToUpper()) + "%' AND StorageName Like '" + Common.Helper.CommonHelper.Convert(this.TxtStorageName.Value.Trim().ToUpper()) + "%'";
        }

        private void TxtStorageName_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_StroageDataTable.DefaultView.RowFilter = " StorageCode like '" + Common.Helper.CommonHelper.Convert(this.TxtBigSSCode.Value.Trim().ToUpper()) + "%' AND StorageName Like '" + Common.Helper.CommonHelper.Convert(this.TxtStorageName.Value.Trim().ToUpper()) + "%'";
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
            else
            {
                //返回空字符串，为了和点击取消区分
                //bigSSCode = "$&Empty";
                bigSSCode = "";
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
            e.Layout.Bands[0].ScrollTipField = "StorageCode";
            e.Layout.Bands[0].ScrollTipField = "StorageName";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["StorageCode"].Header.Caption = "库别代码";
            e.Layout.Bands[0].Columns["StorageName"].Header.Caption = "库别名称";


            e.Layout.Bands[0].Columns["Checked"].Width = 30;
            e.Layout.Bands[0].Columns["StorageCode"].Width = 200;
            e.Layout.Bands[0].Columns["StorageName"].Width = 200;

            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["StorageCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["StorageName"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["StorageCode"].SortIndicator = SortIndicator.Ascending;
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
            this.m_StroageDataTable = new DataTable();

            this.m_StroageDataTable.Columns.Add("Checked", typeof(bool));
            this.m_StroageDataTable.Columns.Add("StorageCode", typeof(string));
            this.m_StroageDataTable.Columns.Add("StorageName", typeof(string));
            this.m_StroageDataTable.AcceptChanges();

            this.ultraGridBigSSCode.DataSource = this.m_StroageDataTable;
        }

        private void DoQuery()
        {
            if (m_StroageDataTable != null)
            {
                this.m_StroageDataTable.Rows.Clear();
                this.m_StroageDataTable.AcceptChanges();
            }

            m_InventoryFacade = new InventoryFacade(this.DataProvider);
            object[] bigSSCodeList = this.m_InventoryFacade.GetAllStorageCode();
            if (bigSSCodeList != null)
            {
                DataRow rowNew;
                foreach (Storage ss in bigSSCodeList)
                {
                    rowNew = this.m_StroageDataTable.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["StorageCode"] = ss.StorageCode;
                    rowNew["StorageName"] = ss.StorageName;
                    this.m_StroageDataTable.Rows.Add(rowNew);
                }
                this.m_StroageDataTable.AcceptChanges();
            }
            this.ultraGridBigSSCode.ActiveRow = null;
        }

        private void DoQueryForOrgid()
        {
            if (m_StroageDataTable != null)
            {
                this.m_StroageDataTable.Rows.Clear();
                this.m_StroageDataTable.AcceptChanges();
            }

            m_InventoryFacade = new InventoryFacade(this.DataProvider);
            object[] StorageList = this.m_InventoryFacade.QueryStorageByOrgId(orgID);
            if (StorageList != null)
            {
                DataRow rowNew;
                foreach (Storage ss in StorageList)
                {
                    rowNew = this.m_StroageDataTable.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["StorageCode"] = ss.StorageCode;
                    rowNew["StorageName"] = ss.StorageName;
                    this.m_StroageDataTable.Rows.Add(rowNew);
                }
                this.m_StroageDataTable.AcceptChanges();
            }
            this.ultraGridBigSSCode.ActiveRow = null;
        }
        #endregion 

        private void ultraGridBigSSCode_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "Checked")
            {
                if (e.Cell.Text == "True")
                {
                    foreach (UltraGridRow row in this.ultraGridBigSSCode.Rows)
                    {
                        if (row.Cells["Checked"] != e.Cell)
                        {
                            row.Cells["Checked"].Value = false;
                        }
                    }
                }
            }
        }
    }
}