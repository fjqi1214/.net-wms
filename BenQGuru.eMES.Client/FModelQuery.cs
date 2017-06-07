using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;

namespace BenQGuru.eMES.Client
{
    public partial class FModelQuery : BaseForm
    {       
        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> ModelCodeSelectedEvent;
        private DataTable m_ModelCode = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private ModelFacade m_ModelFacade;
        private string modelCode = string.Empty;

        #endregion

        #region 属性

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }


        private ModelFacade modelFacade
        {
            get
            {
                if (this.m_ModelFacade == null)
                {
                    this.m_ModelFacade = new ModelFacade(this.DataProvider);
                }
                return m_ModelFacade;
            }
        }

        #endregion

        #region 页面事件
        public FModelQuery()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridModelCode.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridModelCode.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridModelCode.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridModelCode.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridModelCode.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridModelCode.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridModelCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridModelCode.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridModelCode.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridModelCode.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FModelQuery_Load(object sender, EventArgs e)
        {
            this.InitialModelCodeGrid();
            this.DoQuery();
            //this.InitPageLanguage();
        }

        private void TxtModelCode_InnerTextChanged(object sender, EventArgs e)
        {
            this.m_ModelCode.DefaultView.RowFilter = " ModelCode like '" + Common.Helper.CommonHelper.Convert(this.txtItemModelCode.Value.Trim().ToUpper()) + "%'";
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridModelCode.Rows.Count == 0)
            {
                return;
            }

            string modelCode = string.Empty;
            for (int i = 0; i < ultraGridModelCode.Rows.Count; i++)
            {
                if (ultraGridModelCode.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    modelCode += "," + ultraGridModelCode.Rows[i].Cells[1].Text.Trim().ToUpper();
                }
            }

            if (modelCode.Length > 0)
            {
                modelCode = modelCode.Substring(1);
            }           
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(modelCode);
            this.OnSoftVersionSelectedEvent(sender, args);
            this.Close();
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ultraGridModelCode_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
            e.Layout.Bands[0].ScrollTipField = "ModelCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["ModelCode"].Header.Caption = "产品别";
            e.Layout.Bands[0].Columns["ModelDesc"].Header.Caption = "产品别描述";


            e.Layout.Bands[0].Columns["Checked"].Width = 30;
            e.Layout.Bands[0].Columns["ModelCode"].Width = 200;
            e.Layout.Bands[0].Columns["ModelDesc"].Width = 200;

            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["ModelCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["ModelDesc"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["ModelCode"].SortIndicator = SortIndicator.Ascending;
            //this.InitGridLanguage(ultraGridModelCode);
        }

        private void ultraGridModelCode_CellChange(object sender, CellEventArgs e)
        {
            //if (e.Cell.Column.Key == "Checked")
            //{
            //    if (e.Cell.Text == "True")
            //    {
            //        foreach (UltraGridRow row in this.ultraGridModelCode.Rows)
            //        {
            //            if (row.Cells["Checked"] != e.Cell)
            //            {
            //                row.Cells["Checked"].Value = false;
            //            }
            //        }
            //    }
            //}
        }

        #endregion 

        #region 自定义事件

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.ModelCodeSelectedEvent != null)
            {
                ModelCodeSelectedEvent(sender, e);
            }
        }

        private void InitialModelCodeGrid()
        {
            this.m_ModelCode = new DataTable();

            this.m_ModelCode.Columns.Add("Checked", typeof(bool));
            this.m_ModelCode.Columns.Add("ModelCode", typeof(string));
            this.m_ModelCode.Columns.Add("ModelDesc", typeof(string));

            this.m_ModelCode.AcceptChanges();

            this.ultraGridModelCode.DataSource = this.m_ModelCode;
        }

        private void DoQuery()
        {
            if (m_ModelCode != null)
            {
                this.m_ModelCode.Rows.Clear();
                this.m_ModelCode.AcceptChanges();
            }

            object[] modelCodeList = modelFacade.GetAllModels();
            if (modelCodeList != null)
            {
                DataRow rowNew;
                foreach (Model model in modelCodeList)
                {
                    rowNew = this.m_ModelCode.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["ModelCode"] = model.ModelCode;
                    rowNew["ModelDesc"] = model.ModelDescription;
                    this.m_ModelCode.Rows.Add(rowNew);
                }
                this.m_ModelCode.AcceptChanges();
            }
            this.ultraGridModelCode.ActiveRow = null;
        }

        #endregion 

       
    }
}
