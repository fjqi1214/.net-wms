using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.BaseSetting;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Client
{
    public partial class FSoftVersionSelector : BaseForm
    {
        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>> SoftVersionSelectedEvent;
        
        private DataTable m_SoftVerList = null;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }
        private BaseModelFacade m_baseModelFacade;
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
        
        public FSoftVersionSelector()
        {
            InitializeComponent();

            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(ultraGridSoftVersion);
            this.ultraGridSoftVersion.UpdateMode = UpdateMode.OnCellChange;            
        }

        private void InitialSoftVerGrid()
        {
            this.m_SoftVerList = new DataTable();
            this.m_SoftVerList.Columns.Add("VersionName", typeof(string));
            this.m_SoftVerList.Columns.Add("EffDate", typeof(int));
            this.m_SoftVerList.Columns.Add("DisDate", typeof(int));
            this.m_SoftVerList.Columns.Add("MaintainUser", typeof(string));

            this.m_SoftVerList.AcceptChanges();

            this.ultraGridSoftVersion.DataSource = this.m_SoftVerList;
        }

        private void ucButtonQuery_Click(object sender, EventArgs e)
        {
            this.DoQuery();
        }

        private void ucButtonOK_Click(object sender, EventArgs e)
        {
            if (this.ultraGridSoftVersion.Rows.Count == 0)
            {
                return;
            }

            if (this.ultraGridSoftVersion.ActiveRow != null)
            {
                string versionName = this.ultraGridSoftVersion.ActiveRow.Cells["VersionName"].Value.ToString();
                ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(versionName);
                this.OnSoftVersionSelectedEvent(sender, args);

                this.Close();
            }
        }

        private void ultraGridSoftVersion_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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
            e.Layout.Bands[0].ScrollTipField = "VersionName";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["VersionName"].Header.Caption = "软件版本";
            e.Layout.Bands[0].Columns["EffDate"].Header.Caption = "生效日期";
            e.Layout.Bands[0].Columns["DisDate"].Header.Caption = "失效日期";
            e.Layout.Bands[0].Columns["MaintainUser"].Header.Caption = "维护人员";
            e.Layout.Bands[0].Columns["VersionName"].Width = 180;
            e.Layout.Bands[0].Columns["EffDate"].Width = 100;
            e.Layout.Bands[0].Columns["DisDate"].Width = 100;
            e.Layout.Bands[0].Columns["MaintainUser"].Width = 100;
            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["VersionName"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["EffDate"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["DisDate"].CellActivation = Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaintainUser"].CellActivation = Activation.NoEdit;

            e.Layout.Bands[0].Columns["VersionName"].SortIndicator = SortIndicator.Ascending;
        }

        private void ucLabelEditVersion_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.DoQuery();
            }
        }

        private void DoQuery()
        {
            this.m_SoftVerList.Rows.Clear();
            this.m_SoftVerList.AcceptChanges();

            string versionCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditVersion.Value));
            object[] softverList = this.baseModelFacade.QuerySoftWareVersionForSelector(versionCode);
            if (softverList != null)
            {
                DataRow rowNew;
                foreach (SoftWareVersion swv in softverList)
                {
                    rowNew = this.m_SoftVerList.NewRow();
                    rowNew["VersionName"] = swv.VersionCode;
                    rowNew["EffDate"] = swv.EffectiveDate;
                    rowNew["DisDate"] = swv.InvalidDate;
                    rowNew["MaintainUser"] = swv.MaintainUser;
                    this.m_SoftVerList.Rows.Add(rowNew);
                }
                this.m_SoftVerList.AcceptChanges();
            }
            this.ultraGridSoftVersion.ActiveRow = null;
        }

        public void OnSoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            if (this.SoftVersionSelectedEvent != null)
            {
                SoftVersionSelectedEvent(sender, e);
            }
        }

        private void ultraGridSoftVersion_DoubleClick(object sender, EventArgs e)
        {
            if (this.ultraGridSoftVersion.Rows.Count == 0)
            {
                return;
            }
            if (this.ultraGridSoftVersion.ActiveRow == null)
            {
                return;
            }

            string versionName = this.ultraGridSoftVersion.ActiveRow.Cells["VersionName"].Value.ToString();
            ParentChildRelateEventArgs<string> args = new ParentChildRelateEventArgs<string>(versionName);
            this.OnSoftVersionSelectedEvent(sender, args);

            this.Close();
        }

        private void FSoftVersionSelector_Load(object sender, EventArgs e)
        {
            this.InitialSoftVerGrid();
            
        }
    }
}