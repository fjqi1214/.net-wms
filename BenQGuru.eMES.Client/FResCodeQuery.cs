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
    public partial class FResCodeQuery : BaseForm
    {
        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> ResCodeSelectedEvent;
        private DataTable m_ResCode = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private BaseModelFacade m_baseModelFacade;
        private string ssCode = "";

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
        public FResCodeQuery(string ssCode)
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridResCode.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridResCode.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridResCode.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridResCode.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridResCode.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridResCode.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridResCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridResCode.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridResCode.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridResCode.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");

            this.ssCode = ssCode;
        }

        private void FResCodeQuery_Load(object sender, EventArgs e)
        {
            this.InitialResCoderGrid();
            this.DoQuery();
            //this.InitPageLanguage();
        }

        private void txtResCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_ResCode.DefaultView.RowFilter = " ResCode like '" + Common.Helper.CommonHelper.Convert(this.txtResCodeQuery.Value.Trim().ToUpper()) + "%' and Resdesc like '" + Common.Helper.CommonHelper.Convert(this.txtResDESC.Value.Trim().ToUpper()) + "%'";
        }

        private void txtResDESC_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_ResCode.DefaultView.RowFilter = " ResCode like '" + Common.Helper.CommonHelper.Convert(this.txtResCodeQuery.Value.Trim().ToUpper()) + "%' and Resdesc like '" + Common.Helper.CommonHelper.Convert(this.txtResDESC.Value.Trim().ToUpper()) + "%'";
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridResCode.Rows.Count == 0)
            {
                return;
            }

            string resCode = string.Empty;
            for (int i = 0; i < ultraGridResCode.Rows.Count; i++)
            {
                if (ultraGridResCode.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    resCode += "," + ultraGridResCode.Rows[i].Cells[1].Text.Trim().ToUpper();
                }
            }

            if (resCode.Length > 0)
            {
                resCode = resCode.Substring(1);
            }
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(resCode);
            this.OnSoftVersionSelectedEvent(sender, args);
            this.Close();
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {

        }

        private void ultraGridResCode_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
            e.Layout.Bands[0].ScrollTipField = "ResCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["ResCode"].Header.Caption = "资源";
            e.Layout.Bands[0].Columns["ResDesc"].Header.Caption = "资源描述";


            e.Layout.Bands[0].Columns["Checked"].Width = 30;
            e.Layout.Bands[0].Columns["ResCode"].Width = 300;
            e.Layout.Bands[0].Columns["ResDesc"].Width = 300;

            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["ResCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["ResDesc"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["ResCode"].SortIndicator = SortIndicator.Ascending;
            e.Layout.Bands[0].Columns["ResDesc"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridResCode);
        }

        #endregion

        #region 自定义事件

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.ResCodeSelectedEvent != null)
            {
                ResCodeSelectedEvent(sender, e);
            }
        }

        private void InitialResCoderGrid()
        {
            this.m_ResCode = new DataTable();

            this.m_ResCode.Columns.Add("Checked", typeof(bool));
            this.m_ResCode.Columns.Add("ResCode", typeof(string));
            this.m_ResCode.Columns.Add("ResDesc", typeof(string));

            this.m_ResCode.AcceptChanges();

            this.ultraGridResCode.DataSource = this.m_ResCode;
        }

        private void DoQuery()
        {
            if (m_ResCode != null)
            {
                this.m_ResCode.Rows.Clear();
                this.m_ResCode.AcceptChanges();
            }

            m_baseModelFacade = new BaseModelFacade(this.DataProvider);
            object[] resCodeList;
            if (ssCode == string.Empty || ssCode.Length <= 0)
            {
                resCodeList = this.m_baseModelFacade.GetAllResource();
            }
            else
            {
                resCodeList = this.m_baseModelFacade.GetAllResource(ssCode);
            }
            
            if (resCodeList != null)
            {
                DataRow rowNew;
                foreach (Resource res in resCodeList)
                {
                    rowNew = this.m_ResCode.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["ResCode"] = res.ResourceCode;
                    rowNew["ResDesc"] = res.ResourceDescription;
                    this.m_ResCode.Rows.Add(rowNew);
                }
                this.m_ResCode.AcceptChanges();
            }
            this.ultraGridResCode.ActiveRow = null;
        }

        #endregion
    }
}
