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
using UserControl;
namespace BenQGuru.eMES.Client
{
    public partial class FRcardImprotByMo : Form
    {
        #region  变量

        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> RCardSelectedEvent;
        private DataTable m_RCard = null;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        MOFacade moFacade;

        public string ItemCode;

        #endregion

        #region 属性

        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        #endregion

        #region 页面事件

        public FRcardImprotByMo()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);

            ultraGridRCard.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White; ;
            ultraGridRCard.DisplayLayout.CaptionAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridRCard.DisplayLayout.Appearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridRCard.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
            ultraGridRCard.DisplayLayout.Override.RowAppearance.BackColor = Color.FromArgb(230, 234, 245);
            ultraGridRCard.DisplayLayout.Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            ultraGridRCard.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            ultraGridRCard.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
            ultraGridRCard.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
            ultraGridRCard.DisplayLayout.ScrollBarLook.Appearance.BackColor = Color.FromName("LightGray");
        }

        private void FRCardQuery_Load(object sender, EventArgs e)
        {
            this.InitialRCardGrid();
            //this.DoQuery();
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridRCard.Rows.Count == 0)
            {
                return;
            }

            string rCard = string.Empty;
            for (int i = 0; i < ultraGridRCard.Rows.Count; i++)
            {
                if (ultraGridRCard.Rows[i].Cells[0].Text.ToLower() == "true")
                {
                    rCard += "," + ultraGridRCard.Rows[i].Cells[1].Text.Trim().ToUpper();
                }
            }

            if (rCard.Length > 0)
            {
                rCard = rCard.Substring(1);
            }
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(rCard);
            this.OnSoftVersionSelectedEvent(sender, args);
            this.Close();
        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {

        }

        private void ultraGridRCard_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
            e.Layout.Bands[0].ScrollTipField = "RCard";


            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["RCard"].Header.Caption = "产品序列号";


            e.Layout.Bands[0].Columns["Checked"].Width = 60;
            e.Layout.Bands[0].Columns["RCard"].Width = 300;


            // 设置栏位是否允许编辑，及栏位的显示形式

            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["RCard"].CellActivation = Activation.NoEdit;


            e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            e.Layout.Bands[0].Columns["RCard"].SortIndicator = SortIndicator.Ascending;
        }

        #endregion 

        #region 自定义事件

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.RCardSelectedEvent != null)
            {
                RCardSelectedEvent(sender, e);
            }
        }

        private void InitialRCardGrid()
        {
            this.m_RCard = new DataTable();
            this.m_RCard.Columns.Add("Checked", typeof(bool));
            this.m_RCard.Columns.Add("RCard", typeof(string));
            this.m_RCard.AcceptChanges();
            this.ultraGridRCard.DataSource = this.m_RCard;
        }

        private void DoQuery()
        {
            if (m_RCard != null)
            {
                this.m_RCard.Rows.Clear();
                this.m_RCard.AcceptChanges();
            }


            if (moFacade == null)
            {
                moFacade = new MOFacade(this.DataProvider);
            }

            string[] rCardList = moFacade.GetRcardsByMoAndItem(this.TxtMoCode.Value.Trim(),this.ItemCode);

            if (rCardList == null || rCardList.Length <= 0)
            {
                return;
            }
            if (rCardList != null)
            {
                DataRow rowNew;
                foreach (string rCard in rCardList)
                {
                    rowNew = this.m_RCard.NewRow();
                    rowNew["Checked"] = false;
                    rowNew["RCard"] = rCard;
                    this.m_RCard.Rows.Add(rowNew);
                }
                this.m_RCard.AcceptChanges();
            }
            this.ultraGridRCard.DataSource = m_RCard;
            this.ultraGridRCard.UpdateData();

            if (this.checkBoxSelectedAll.Checked)
            {
                for (int i = 0; i < ultraGridRCard.Rows.Count; i++)
                {
                    this.ultraGridRCard.Rows[i].Cells[0].Value = this.checkBoxSelectedAll.Checked;
                }

                this.ultraGridRCard.UpdateData();
            }

            this.ultraGridRCard.ActiveRow = null;
        }

        #endregion 

        private void ucButtonQuery_Click(object sender, EventArgs e)
        {
            if (this.TxtMoCode.Value.Trim() == "")
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_MoCode_Empty1"));
                return;
            }
            this.DoQuery();
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        private void checkBoxSelectedAll_CheckedChanged(object sender, EventArgs e)
        {

                for (int i = 0; i < ultraGridRCard.Rows.Count; i++)
                {
                    this.ultraGridRCard.Rows[i].Cells[0].Value = this.checkBoxSelectedAll.Checked;
                }

                this.ultraGridRCard.UpdateData();

        }

        private void TxtMoCode_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.TxtMoCode.Value.Trim() == "")
                {
                    this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_RCard_Empty"));
                    return;
                }
                this.DoQuery();
            }
        }

        
    }
}