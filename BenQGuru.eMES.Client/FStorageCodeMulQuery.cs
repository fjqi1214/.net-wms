using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
    public partial class FStorageCodeMulQuery : BaseForm
    {
        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> StorageCodeSelectedEvent;
        private DataTable m_StroageDataTable = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private InventoryFacade m_InventoryFacade;     
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

        public FStorageCodeMulQuery()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridStorageCode.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridStorageCode.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridStorageCode.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridStorageCode.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridStorageCode.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridStorageCode.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridStorageCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridStorageCode.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridStorageCode.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridStorageCode.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FStorageCodeMulQuery_Load(object sender, EventArgs e)
        {
            this.InitialStorageCoderGrid();
            this.DoQuery();
            //this.InitPageLanguage();
        }

        private void TxtStorageCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_StroageDataTable.DefaultView.RowFilter = " StorageCodeForQuery like '" + Common.Helper.CommonHelper.Convert(this.TxtStorageCode.Value.Trim().ToUpper()) + "%' AND StorageNameForQuery Like '" + Common.Helper.CommonHelper.Convert(this.TxtStorageName.Value.Trim().ToUpper()) + "%'";
        }

        private void TxtStorageName_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_StroageDataTable.DefaultView.RowFilter = " StorageCodeForQuery like '" + Common.Helper.CommonHelper.Convert(this.TxtStorageCode.Value.Trim().ToUpper()) + "%' AND StorageNameForQuery Like '" + Common.Helper.CommonHelper.Convert(this.TxtStorageName.Value.Trim().ToUpper()) + "%'";
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridStorageCode.Rows.Count == 0)
            {
                return;
            }

            string storageCode = string.Empty;
            for (int i = 0; i < ultraGridStorageCode.Rows.Count; i++)
            {
                if (ultraGridStorageCode.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    storageCode += "," + ultraGridStorageCode.Rows[i].Cells[1].Text.Trim().ToUpper();
                }
            }

            if (storageCode.Length > 0)
            {
                storageCode = storageCode.Substring(1);
            }
            //else
            //{
            //    //返回空字符串，为了和点击取消区分
            //    storageCode = "$&Empty";
            //}
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(storageCode);
            this.OnSoftVersionSelectedEvent(sender, args);
            this.Close();
        }

        private void ultraGridStorageCode_InitializeLayout(object sender, InitializeLayoutEventArgs e)
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
            e.Layout.Bands[0].ScrollTipField = "StorageCodeForQuery";
            e.Layout.Bands[0].ScrollTipField = "StorageNameForQuery";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["StorageCodeForQuery"].Header.Caption = "库别代码";
            e.Layout.Bands[0].Columns["StorageNameForQuery"].Header.Caption = "库别名称";


            e.Layout.Bands[0].Columns["Checked"].Width = 30;
            e.Layout.Bands[0].Columns["StorageCodeForQuery"].Width = 200;
            e.Layout.Bands[0].Columns["StorageNameForQuery"].Width = 200;

            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["StorageCodeForQuery"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["StorageNameForQuery"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["StorageCodeForQuery"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridStorageCode);
        }
        #endregion

        #region 自定义事件

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.StorageCodeSelectedEvent != null)
            {
                StorageCodeSelectedEvent(sender, e);
            }
        }

        private void InitialStorageCoderGrid()
        {
            this.m_StroageDataTable = new DataTable();

            this.m_StroageDataTable.Columns.Add("Checked", typeof(bool));
            this.m_StroageDataTable.Columns.Add("StorageCodeForQuery", typeof(string));
            this.m_StroageDataTable.Columns.Add("StorageNameForQuery", typeof(string));
            this.m_StroageDataTable.AcceptChanges();

            this.ultraGridStorageCode.DataSource = this.m_StroageDataTable;
        }

        private void DoQuery()
        {
            if (m_StroageDataTable != null)
            {
                this.m_StroageDataTable.Rows.Clear();
                this.m_StroageDataTable.AcceptChanges();
            }

            m_InventoryFacade = new InventoryFacade(this.DataProvider);
            object[] storageCodeList = this.m_InventoryFacade.GetAllStorageCode();
            if (storageCodeList != null)
            {
                DataRow rowNew;
                foreach (Storage ss in storageCodeList)
                {
                    rowNew = this.m_StroageDataTable.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["StorageCodeForQuery"] = ss.StorageCode;
                    rowNew["StorageNameForQuery"] = ss.StorageName;
                    this.m_StroageDataTable.Rows.Add(rowNew);
                }
                this.m_StroageDataTable.AcceptChanges();
            }
            this.ultraGridStorageCode.ActiveRow = null;
        }

       
        #endregion 
    }
}
