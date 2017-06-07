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
    public partial class FMCodeQuery : BaseForm
    {
        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> MCodeSelectedEvent;
        private DataTable m_MCode = null;
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

        //add by li 2011.1.27 
        private bool m_IsSingle = false;
        public bool IsSingle
        {
            get { return m_IsSingle; }
            set { m_IsSingle = value; }
        }

        #endregion

        #region 页面事件

        public FMCodeQuery()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridMaterial.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridMaterial.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridMaterial.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridMaterial.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridMaterial.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridMaterial.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridMaterial.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridMaterial.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridMaterial.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridMaterial.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FMCodeQuery_Load(object sender, EventArgs e)
        {
            this.InitialBigSSCoderGrid();
            this.DoQuery();
            //this.InitPageLanguage();
        }

        private void TxtBigSSCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_MCode.DefaultView.RowFilter = " MCode like '" + Common.Helper.CommonHelper.Convert(this.TxtMCode.Value.Trim().ToUpper()) + "%' AND MDesc Like '" + Common.Helper.CommonHelper.Convert(this.txtMDesc.Value.Trim().ToUpper() )+ "%'";
        }

        private void TxtVendorName_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_MCode.DefaultView.RowFilter = " MCode like '" + Common.Helper.CommonHelper.Convert(this.TxtMCode.Value.Trim().ToUpper()) + "%' AND MDesc Like '" + Common.Helper.CommonHelper.Convert(this.txtMDesc.Value.Trim().ToUpper()) + "%'";
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridMaterial.Rows.Count == 0)
            {
                return;
            }

            string mCode = string.Empty;
            for (int i = 0; i < ultraGridMaterial.Rows.Count; i++)
            {
                if (ultraGridMaterial.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    mCode += "," + ultraGridMaterial.Rows[i].Cells[1].Text.Trim().ToUpper();
                }
            }

            if (mCode.Length > 0)
            {
                mCode = mCode.Substring(1);
            }
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(mCode);
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
            e.Layout.Bands[0].ScrollTipField = "MCode";
            e.Layout.Bands[0].ScrollTipField = "MDesc";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["MCode"].Header.Caption = "物料代码";
            e.Layout.Bands[0].Columns["MDesc"].Header.Caption = "物料描述";

            e.Layout.Bands[0].Columns["Checked"].Width = 30;
            e.Layout.Bands[0].Columns["MCode"].Width = 200;
            e.Layout.Bands[0].Columns["MDesc"].Width = 200;

            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["MCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MDesc"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["MCode"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridMaterial);
        }

        #endregion 

        #region 自定义事件

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.MCodeSelectedEvent != null)
            {
                MCodeSelectedEvent(sender, e);
            }
        }

        private void InitialBigSSCoderGrid()
        {
            this.m_MCode = new DataTable();

            this.m_MCode.Columns.Add("Checked", typeof(bool));
            this.m_MCode.Columns.Add("MCode", typeof(string));
            this.m_MCode.Columns.Add("MDesc", typeof(string));

            this.m_MCode.AcceptChanges();

            this.ultraGridMaterial.DataSource = this.m_MCode;
        }

        private void DoQuery()
        {
            if (m_MCode != null)
            {
                this.m_MCode.Rows.Clear();
                this.m_MCode.AcceptChanges();
            }

            m_ItemFacade = new ItemFacade(this.DataProvider);
            object[] MCodeList = this.m_ItemFacade.GetAllMaterial();// this.m_MoFacade.get
            if (MCodeList != null)
            {
                DataRow rowNew;
                foreach (Domain.MOModel.Material m in MCodeList)
                {
                    rowNew = this.m_MCode.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["MCode"] = m.MaterialCode;
                    rowNew["MDesc"] = m.MaterialDescription;
                    this.m_MCode.Rows.Add(rowNew);
                }
                this.m_MCode.AcceptChanges();
            }
            this.ultraGridMaterial.ActiveRow = null;
        }

       
        #endregion 

        private void ultraGridMaterial_CellChange(object sender, CellEventArgs e)
        {
            if (m_IsSingle)
            {
                if (e.Cell.Column.Key == "Checked")
                {
                    if (e.Cell.Text == "True")
                    {
                        foreach (UltraGridRow row in this.ultraGridMaterial.Rows)
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
}