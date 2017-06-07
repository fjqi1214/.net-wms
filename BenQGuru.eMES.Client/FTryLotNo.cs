using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.BaseSetting;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.MOModel;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FTryLotNo : BaseForm
    {

        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> TrySelectedEvent;
        private DataTable m_TryLotNo = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private TryFacade m_tryFacade;

        #endregion

        #region 属性

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        private TryFacade tryFacade
        {
            get
            {
                if (this.m_tryFacade == null)
                {
                    this.m_tryFacade = new TryFacade(this.DataProvider);
                }
                return m_tryFacade;
            }
        }

        #endregion

        #region 页面事件

        private void FTryLotNo_Load(object sender, EventArgs e)
        {
            this.InitialTryLotNorGrid();
            this.DoQuery();
            //this.InitGridLanguage(ultraGridTry);
            //this.InitPageLanguage();
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridTry.Rows.Count == 0)
            {
                return;
            }

            string tryCode = string.Empty;
            for (int i = 0; i < ultraGridTry.Rows.Count; i++)
            {
                if (ultraGridTry.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    if (Convert.ToInt32(ultraGridTry.Rows[i].Cells["PlanQty"].Text.Trim()) <= Convert.ToInt32(ultraGridTry.Rows[i].Cells["ActualQty"].Text.Trim()))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Current_TryCode:" + ultraGridTry.Rows[i].Cells["TryCode"].Text +
                                                                                                    "   $CS_PlanQty:" + ultraGridTry.Rows[i].Cells["PlanQty"].Text +
                                                                                                    "   $CS_ActualQty:" + ultraGridTry.Rows[i].Cells["ActualQty"].Text));
                        return;
                    }

                    tryCode += "," + ultraGridTry.Rows[i].Cells["TryCode"].Text.Trim().ToUpper();
                }
            }

            if (tryCode.Length > 0)
            {
                tryCode = tryCode.Substring(1);
            }
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(tryCode);
            this.OnSoftVersionSelectedEvent(sender, args);
            this.Close();
        }

        private void ultraGridTry_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
            e.Layout.Bands[0].ScrollTipField = "TryCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["TryCode"].Header.Caption = "试流单号";
            e.Layout.Bands[0].Columns["MCode"].Header.Caption = "料号";
            e.Layout.Bands[0].Columns["MDesc"].Header.Caption = "料号描述";
            e.Layout.Bands[0].Columns["PlanQty"].Header.Caption = "计划数量";
            e.Layout.Bands[0].Columns["ActualQty"].Header.Caption = "实际数量";
            e.Layout.Bands[0].Columns["Memo"].Header.Caption = "备注";

            e.Layout.Bands[0].Columns["Checked"].Width = 50;
            e.Layout.Bands[0].Columns["TryCode"].Width = 110;
            e.Layout.Bands[0].Columns["MCode"].Width = 80;
            e.Layout.Bands[0].Columns["MDesc"].Width = 80;
            e.Layout.Bands[0].Columns["PlanQty"].Width = 60;
            e.Layout.Bands[0].Columns["ActualQty"].Width =60;
            e.Layout.Bands[0].Columns["Memo"].Width = 150;
            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["TryCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MCode"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MDesc"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["PlanQty"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["ActualQty"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["Memo"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["TryCode"].SortIndicator = SortIndicator.Ascending;


        }

        private void TxtTryCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string tryCode = FormatHelper.CleanString(this.TxtTryCode.Value.Trim().ToUpper());
                if (tryCode.Trim().Length == 0)
                {
                    this.TxtTryCode.TextFocus(false, true);
                    return;
                }

                int tryCodeLength = tryCode.Trim().Length;

                for (int i = 0; i < ultraGridTry.Rows.Count; i++)
                {
                    int girdTryCodeLength = ultraGridTry.Rows[i].Cells["TryCode"].Text.Trim().Length;
                    if (girdTryCodeLength >= tryCodeLength
                        && ultraGridTry.Rows[i].Cells["TryCode"].Text.Trim().ToUpper().Substring(0, tryCodeLength) == tryCode)
                    {
                        if (ultraGridTry.Rows[i].Cells[0].Text.ToLower() == "false")
                        {
                            ultraGridTry.Rows[i].Cells[0].Value = true;
                        }
                    }
                }

                this.ucButtonOK_Click(sender, e);
            }
        }

        private void TxtTryCode_InnerTextChanged(object sender, EventArgs e)
        {
            m_TryLotNo.DefaultView.RowFilter = "TryCode like '" + Common.Helper.CommonHelper.Convert(this.TxtTryCode.Value.Trim().ToUpper()) + "%'";
        }

        #endregion

        #region 自定义事件

        public  FTryLotNo()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridTry.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridTry.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridTry.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridTry.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridTry.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridTry.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridTry.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridTry.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridTry.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridTry.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }       

        private void DoQuery()
        {
            if (m_TryLotNo != null)
            {
                this.m_TryLotNo.Rows.Clear();
                this.m_TryLotNo.AcceptChanges();
            }

            m_tryFacade = new TryFacade(this.DataProvider);
            object[] TryLotList = this.m_tryFacade.GetTryWithStatus();
            if (TryLotList != null)
            {
                DataRow rowNew;
                foreach (TryAndItemDesc swv in TryLotList)
                {
                    rowNew = this.m_TryLotNo.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["TryCode"] = swv.TryCode;
                    rowNew["MCode"] = swv.ItemCode;
                    rowNew["MDesc"] = swv.ItemDescription;
                    rowNew["PlanQty"] = swv.PlanQty;
                    rowNew["ActualQty"] = swv.ActualQty;
                    rowNew["Memo"] = swv.Memo;
                    this.m_TryLotNo.Rows.Add(rowNew);
                }
                this.m_TryLotNo.AcceptChanges();
            }
            this.ultraGridTry.ActiveRow = null;
        }           

        private void InitialTryLotNorGrid()
        {
            this.m_TryLotNo = new DataTable();

            this.m_TryLotNo.Columns.Add("Checked", typeof(bool));
            this.m_TryLotNo.Columns.Add("TryCode", typeof(string));
            this.m_TryLotNo.Columns.Add("MCode", typeof(string));
            this.m_TryLotNo.Columns.Add("MDesc", typeof(string));            
            this.m_TryLotNo.Columns.Add("PlanQty", typeof(int));
            this.m_TryLotNo.Columns.Add("ActualQty", typeof(int));
            this.m_TryLotNo.Columns.Add("Memo", typeof(string));

            this.m_TryLotNo.AcceptChanges();

            this.ultraGridTry.DataSource = this.m_TryLotNo;
        }

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.TrySelectedEvent != null)
            {
                TrySelectedEvent(sender, e);
            }
        }

        #endregion

    }
}