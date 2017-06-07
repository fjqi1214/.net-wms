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
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Client
{
    public partial class FStackCode : BaseForm
    {
        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> StackCodeSelectedEvent;
        private DataTable m_StackCode = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private InventoryFacade m_baseModelFacade;
        private string storageCode = string.Empty;

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
                if (this.m_baseModelFacade == null)
                {
                    this.m_baseModelFacade = new InventoryFacade(this.DataProvider);
                }
                return m_baseModelFacade;
            }
        }

        #endregion

        #region 页面事件
        public FStackCode()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridStackCode.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridStackCode.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridStackCode.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridStackCode.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridStackCode.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridStackCode.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridStackCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridStackCode.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridStackCode.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridStackCode.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        public FStackCode(string storageCode)
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridStackCode.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridStackCode.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridStackCode.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridStackCode.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridStackCode.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridStackCode.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridStackCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridStackCode.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridStackCode.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridStackCode.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

            this.storageCode = storageCode;
        }

        private void FStackCode_Load(object sender, EventArgs e)
        {
            this.InitialStackCodeGrid();
            this.DoQuery();

            //this.InitGridLanguage(ultraGridStackCode);
            //this.InitPageLanguage();
        }

        private void TxtStackCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_StackCode.DefaultView.RowFilter = " StackCode like '" + Common.Helper.CommonHelper.Convert(this.TxtStackCode.Value.Trim().ToUpper()) + "%'";
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridStackCode.Rows.Count == 0)
            {
                return;
            }

            string stackCode = string.Empty;
            for (int i = 0; i < ultraGridStackCode.Rows.Count; i++)
            {
                if (ultraGridStackCode.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    stackCode += "," + ultraGridStackCode.Rows[i].Cells[1].Text.Trim().ToUpper();
                }
            }

            if (stackCode.Length > 0)
            {
                stackCode = stackCode.Substring(1);
            }
            else
            {
                //返回空字符串，为了和点击取消区分
                stackCode = "$&Empty";
            }
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(stackCode);
            this.OnSoftVersionSelectedEvent(sender, args);
            this.Close();
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ultraGridStackCode_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
            e.Layout.Bands[0].ScrollTipField = "StackCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["StackCode"].Header.Caption = "库位";


            e.Layout.Bands[0].Columns["Checked"].Width = 30;
            e.Layout.Bands[0].Columns["StackCode"].Width = 300;

            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["StackCode"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["StackCode"].SortIndicator = SortIndicator.Ascending;
        }

        #endregion 

        #region 自定义事件

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.StackCodeSelectedEvent != null)
            {
                StackCodeSelectedEvent(sender, e);
            }
        }

        private void InitialStackCodeGrid()
        {
            this.m_StackCode = new DataTable();

            this.m_StackCode.Columns.Add("Checked", typeof(bool));
            this.m_StackCode.Columns.Add("StackCode", typeof(string));

            this.m_StackCode.AcceptChanges();

            this.ultraGridStackCode.DataSource = this.m_StackCode;
        }

        private void DoQuery()
        {
            if (m_StackCode != null)
            {
                this.m_StackCode.Rows.Clear();
                this.m_StackCode.AcceptChanges();
            }

            m_baseModelFacade = new InventoryFacade(this.DataProvider);
            object[] stackCodeList = this.m_baseModelFacade.GetStack(storageCode);
            if (stackCodeList != null)
            {
                DataRow rowNew;
                foreach (SStack ss in stackCodeList)
                {
                    rowNew = this.m_StackCode.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["StackCode"] = ss.StackCode;
                    this.m_StackCode.Rows.Add(rowNew);
                }
                this.m_StackCode.AcceptChanges();
            }
            this.ultraGridStackCode.ActiveRow = null;
        }

        #endregion 

        private void ultraGridStackCode_CellChange(object sender, CellEventArgs e)
        {
            if (e.Cell.Column.Key == "Checked")
            {
                if (e.Cell.Text == "True")
                {
                    foreach (UltraGridRow row in this.ultraGridStackCode.Rows)
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
