using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.DataCollect;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.Client
{
    public partial class FSoftVersionUpgrade_SEMI : Form
    {
        DataTable dtSoftWareVersion = new DataTable();

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }
        
        public FSoftVersionUpgrade_SEMI()
        {
            InitializeComponent();

            UserControl.UIStyleBuilder.FormUI(this);
            this.ultraGridUpgradeRecord.UpdateMode = UpdateMode.OnCellChange;
            UserControl.UIStyleBuilder.GridUI(this.ultraGridUpgradeRecord);
        }

        private void ucBtnQuery_Click(object sender, EventArgs e)
        {
            FSoftVersionSelector fSoftVersionSelector = new FSoftVersionSelector();
            fSoftVersionSelector.SoftVersionSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(Form_Event);
            fSoftVersionSelector.Owner = this;
            fSoftVersionSelector.ShowDialog();
            if (this.ucLabelSoftVersion.Value.Trim().Length > 0)
            {
                this.ucLabelRcard.TextFocus(true, true);
            }
            else
            {
                this.ucLabelSoftVersion.TextFocus(false, true);
            }
        }


        public void Form_Event(object sender, ParentChildRelateEventArgs<string> e)
        {
            string softWareVersion = e.CustomObject;

            this.ucLabelSoftVersion.Value = softWareVersion;
        }

        private void ultraGridUpgradeRecord_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["RunningCard"].Header.Caption = "产品序列号";
            e.Layout.Bands[0].Columns["MaterialDescription"].Header.Caption = "产品描述";
            e.Layout.Bands[0].Columns["SoftWareVersionOld"].Header.Caption = "原软件版本";
            e.Layout.Bands[0].Columns["SoftWareVesrsionNew"].Header.Caption = "最新软件版本";

            e.Layout.Bands[0].Columns["RunningCard"].Width = 200;
            e.Layout.Bands[0].Columns["MaterialDescription"].Width = 300;
            e.Layout.Bands[0].Columns["SoftWareVersionOld"].Width = 100;
            e.Layout.Bands[0].Columns["SoftWareVesrsionNew"].Width = 100;


            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["RunningCard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["MaterialDescription"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["SoftWareVersionOld"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["SoftWareVesrsionNew"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["RunningCard"].SortIndicator = SortIndicator.Ascending;
        }

        private void InitializeUltraGrid()
        {
            dtSoftWareVersion.Columns.Clear();

            dtSoftWareVersion.Columns.Add("RunningCard", typeof(string));
            dtSoftWareVersion.Columns.Add("MaterialDescription", typeof(string));
            dtSoftWareVersion.Columns.Add("SoftWareVersionOld", typeof(string));
            dtSoftWareVersion.Columns.Add("SoftWareVesrsionNew", typeof(string));

            dtSoftWareVersion.AcceptChanges();

            this.ultraGridUpgradeRecord.DataSource = dtSoftWareVersion;
        }


        private void FSoftVersionUpgrade_SEMI_Load(object sender, EventArgs e)
        {
            InitializeUltraGrid();
            this.InitUIControl();
        }

        private void InitUIControl()
        {
            this.ucLabelRcard.Value = "";
        }

        private void ucLabelRcard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLabelSoftVersion.Value.Trim().Length == 0)
                {
                    this.ucLabelSoftVersion.TextFocus(false, true);
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_PleaseChooseSoftVersion"));
                    return;
                }

                if (this.ucLabelRcard.Value == string.Empty)
                {
                    this.ucLabelRcard.TextFocus(false, true);
                    return;
                }
                string newVersion = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelSoftVersion.Value));
                string rcard = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelRcard.Value));
                Messages msg = new Messages();
                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
                ((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

                try
                {
                    DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                    string sourceRCard = dataCollectFacade.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);

                    // 获取要升级的产品序列号信息
                    OnWipSoftVer4Upgrade oldVersion = dataCollectFacade.GetOldSoftVersion(sourceRCard);

                    if (oldVersion == null)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Rcard_Or_NOSoftInfo"));
                        this.ucLabelRcard.TextFocus(false, true);
                        return;
                    }
                    // 判断软件版本是否相同，相同就不需要升级了
                    if (string.Compare(oldVersion.SoftwareVersion, newVersion, true) == 0)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_AlreadyUpgraded"));
                        this.ucLabelRcard.TextFocus(false, true);
                        return;
                    }

                    BenQGuru.eMES.BaseSetting.BaseModelFacade dataModel = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
                    Resource resource = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);
                    
                    string userCode = ApplicationService.Current().UserCode;

                    // 升级
                    msg.AddMessages(dataCollectFacade.UpgradeNewSoftVer(oldVersion, resource, userCode, newVersion));

                    if (!msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().Add(msg);
                        this.ucLabelRcard.TextFocus(false, true);
                        return;
                    }

                    dtSoftWareVersion.Rows.Add(new object[] { oldVersion.RunningCard, oldVersion.ItemDescription, 
                                                                oldVersion.SoftwareVersion, newVersion });
                    dtSoftWareVersion.AcceptChanges();
                    this.ucLabelEditCount.Value = this.dtSoftWareVersion.Rows.Count.ToString();

                    this.ucLabelRcard.TextFocus(false, true);
                }
                catch (Exception ex)
                {
                    ApplicationRun.GetInfoForm().Add(ex.Message);
                    this.ucLabelRcard.TextFocus(false, true);
                }
                finally
                {
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                    ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
                }
            }
        }

    }
}