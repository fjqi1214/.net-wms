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


namespace BenQGuru.eMES.Client
{
    public partial class FOPCodeQuery : BaseForm
    {
        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> OPCodeSelectedEvent;
        private DataTable m_OPCode = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private BaseModelFacade m_baseModelFacade;

        #endregion

        #region 属性

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }


        private BaseModelFacade baseModelFacade
        {
            get
            {
                if (this.m_baseModelFacade == null)
                {
                    this.m_baseModelFacade = new BaseModelFacade(this.DataProvider);
                }
                return m_baseModelFacade;
            }
        }

        #endregion

        #region 页面事件

        public FOPCodeQuery()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridOPCode.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridOPCode.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridOPCode.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridOPCode.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridOPCode.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridOPCode.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridOPCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridOPCode.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridOPCode.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridOPCode.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FOPCodeQuery_Load(object sender, EventArgs e)
        {
            this.InitialOPCoderGrid();
            this.DoQuery();
            //this.InitPageLanguage();
        }

        private void txtOPCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_OPCode.DefaultView.RowFilter = " OPCode like '" + Common.Helper.CommonHelper.Convert(this.txtOPCodeQuery.Value.Trim().ToUpper()) + "%' and OPdesc like '" + Common.Helper.CommonHelper.Convert(this.txtOPDESC.Value.Trim().ToUpper()) + "%'";
        }

        private void txtOPDESC_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_OPCode.DefaultView.RowFilter = " OPCode like '" + Common.Helper.CommonHelper.Convert(this.txtOPCodeQuery.Value.Trim().ToUpper()) + "%' and OPdesc like '" + Common.Helper.CommonHelper.Convert(this.txtOPDESC.Value.Trim().ToUpper()) + "%'";
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridOPCode.Rows.Count == 0)
            {
                return;
            }

            string opCode = string.Empty;
            for (int i = 0; i < ultraGridOPCode.Rows.Count; i++)
            {
                if (ultraGridOPCode.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    opCode += "," + ultraGridOPCode.Rows[i].Cells[1].Text.Trim().ToUpper();
                }
            }

            if (opCode.Length > 0)
            {
                opCode = opCode.Substring(1);
            }
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(opCode);
            this.OnSoftVersionSelectedEvent(sender, args);
            this.Close();
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {

        }

        private void ultraGridOPCode_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
            e.Layout.Bands[0].ScrollTipField = "OPCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["OPCode"].Header.Caption = "工序";
            e.Layout.Bands[0].Columns["OPDesc"].Header.Caption = "工序描述";


            e.Layout.Bands[0].Columns["Checked"].Width = 30;
            e.Layout.Bands[0].Columns["OPCode"].Width = 300;
            e.Layout.Bands[0].Columns["OPDesc"].Width = 300;

            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["OPCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["OPDesc"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["OPCode"].SortIndicator = SortIndicator.Ascending;
            e.Layout.Bands[0].Columns["OPDesc"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridOPCode);
        }

        #endregion

        #region 自定义事件

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.OPCodeSelectedEvent != null)
            {
                OPCodeSelectedEvent(sender, e);
            }
        }

        private void InitialOPCoderGrid()
        {
            this.m_OPCode = new DataTable();

            this.m_OPCode.Columns.Add("Checked", typeof(bool));
            this.m_OPCode.Columns.Add("OPCode", typeof(string));
            this.m_OPCode.Columns.Add("OPDesc", typeof(string));

            this.m_OPCode.AcceptChanges();

            this.ultraGridOPCode.DataSource = this.m_OPCode;
        }

        private void DoQuery()
        {
            if (m_OPCode != null)
            {
                this.m_OPCode.Rows.Clear();
                this.m_OPCode.AcceptChanges();
            }

            m_baseModelFacade = new BaseModelFacade(this.DataProvider);
            object[] opCodeList = this.m_baseModelFacade.GetAllOperation();
            if (opCodeList != null)
            {
                DataRow rowNew;
                foreach (Operation ss in opCodeList)
                {
                    rowNew = this.m_OPCode.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["OPCode"] = ss.OPCode;
                    rowNew["OPDesc"] = ss.OPDescription;
                    this.m_OPCode.Rows.Add(rowNew);
                }
                this.m_OPCode.AcceptChanges();
            }
            this.ultraGridOPCode.ActiveRow = null;
        }

        #endregion
    }
}