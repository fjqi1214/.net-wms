using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.DataCollect;

namespace BenQGuru.eMES.Client
{
    public partial class FSoftVersionUpgrade_FG : Form
    {
        private ProductInfo product = null;
        private DataTable m_MaterialList = null;
        private DataTable m_UpgradeLog = null;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        private IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FSoftVersionUpgrade_FG()
        {
            InitializeComponent();

            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(this.ultraGridMaterialList);
            UserControl.UIStyleBuilder.GridUI(this.ultraGridUpgradeLog);
            this.ultraGridMaterialList.UpdateMode = UpdateMode.OnCellChange;
            this.ultraGridMaterialList.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
            this.ultraGridUpgradeLog.UpdateMode = UpdateMode.OnCellChange;
        }

        private void InitialUltraGridDataSource()
        {
            this.m_MaterialList = new DataTable();
            this.m_MaterialList.Columns.Add("Checked", typeof(string));
            this.m_MaterialList.Columns.Add("ItemCode", typeof(string));
            this.m_MaterialList.Columns.Add("ItemDescription", typeof(string));
            this.m_MaterialList.Columns.Add("RCard", typeof(string));
            this.m_MaterialList.Columns.Add("Version", typeof(string));
            this.m_MaterialList.AcceptChanges();

            this.m_UpgradeLog = new DataTable();
            this.m_UpgradeLog.Columns.Add("RCard", typeof(string));
            this.m_UpgradeLog.Columns.Add("ItemDescription", typeof(string));
            this.m_UpgradeLog.Columns.Add("SemiGoodRCard", typeof(string));
            this.m_UpgradeLog.Columns.Add("OldVersion", typeof(string));
            this.m_UpgradeLog.Columns.Add("NewVersion", typeof(string));
            this.m_UpgradeLog.AcceptChanges();

            this.ultraGridMaterialList.DataSource = this.m_MaterialList;
            this.ultraGridUpgradeLog.DataSource = this.m_UpgradeLog;
        }

        private void ucButtonQuery_Click(object sender, EventArgs e)
        {
            FSoftVersionSelector selector = new FSoftVersionSelector();
            selector.Owner = this;
            selector.SoftVersionSelectedEvent += new ParentChildRelateEventHandler<ParentChildRelateEventArgs<string>>(selector_SoftVersionSelectedEvent);
            selector.ShowDialog();
            selector = null;

            if (this.ucLabelEditSoftVersion.Value.Trim().Length > 0)
            {
                this.ucLabelEditRCardFG.TextFocus(false, true);
            }
            else
            {
                this.ucLabelEditSoftVersion.TextFocus(false, true);
            }
        }

        void selector_SoftVersionSelectedEvent(object sender, ParentChildRelateEventArgs<string> e)
        {
            this.ucLabelEditSoftVersion.Value = e.CustomObject;
        }

        private void ucLabelEditRCardFG_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.LoadItemListByFGRCard();
            }
        }

        private void LoadItemListByFGRCard()
        {
            if (this.ucLabelEditRCardFG.Value.Trim().Length == 0)
            {
                this.InitialItemListOfFGRCard();
                this.ucLabelEditRCardFG.TextFocus(false, true);
                return;
            }

            string rcard = FormatHelper.PKCapitalFormat(this.ucLabelEditRCardFG.Value.Trim());

            Messages messages = new Messages();
            ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

            try
            {
                // Get Product Info
                messages = this.GetProduct(rcard);
                if (!messages.IsSuccess() || product == null || product.LastSimulation == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$NoProductInfo"));
                    this.InitialItemListOfFGRCard();
                    this.ucLabelEditRCardFG.TextFocus(false, true);
                    return;
                }

                // Get Material by ItemCode
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                object material = itemFacade.GetMaterial(product.LastSimulation.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (material == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CanNotFindItemInfo $ALERT_Item=" + product.LastSimulation.ItemCode));
                    this.InitialItemListOfFGRCard();
                    this.ucLabelEditRCardFG.TextFocus(false, true);
                    return;
                }
                // 设置Description
                this.labelItemDesc.Text = (material as BenQGuru.eMES.Domain.MOModel.Material).MaterialDescription;

                DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                object[] versionList = dcf.GetOnWipSoftVersionList(rcard, product.LastSimulation.MOCode, product.LastSimulation.ItemCode);
                if (versionList == null || versionList.Length == 0)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CanNotFindItemSoftVer_FG"));
                    this.InitialItemListOfFGRCard();
                    this.ucLabelEditRCardFG.TextFocus(false, true);
                    return;
                }

                OnWipSoftVer4Upgrade version;
                DataRow rowNew;
                this.m_MaterialList.Rows.Clear();
                this.m_MaterialList.AcceptChanges();
                foreach (OnWIPSoftVersion ver in versionList)
                {
                    version = dcf.GetOnWipSoftVersion(ver.RunningCard, ver.ItemCode);
                    if (version != null)
                    {
                        rowNew = this.m_MaterialList.NewRow();
                        rowNew["Checked"] = "false";
                        rowNew["ItemCode"] = version.ItemCode;
                        rowNew["ItemDescription"] = version.ItemDescription;
                        rowNew["RCard"] = version.RunningCard;
                        rowNew["Version"] = version.SoftwareVersion;

                        this.m_MaterialList.Rows.Add(rowNew);
                    }
                }
                this.m_MaterialList.AcceptChanges();

                if (this.m_MaterialList.Rows.Count > 0)
                {
                    //this.ultraGridMaterialList.DataSource = this.m_MaterialList;
                    this.ultraGridMaterialList.Rows[0].Cells["Checked"].Value = "true";
                    this.ultraGridMaterialList.UpdateData();
                    this.ucLabelEditRCard.TextFocus(true, true);
                }
                else
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CanNotFindItemSoftVer_FG"));
                    this.InitialItemListOfFGRCard();
                    this.ucLabelEditRCardFG.TextFocus(false, true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(ex.Message);
                this.InitialItemListOfFGRCard();
                this.ucLabelEditRCardFG.TextFocus(false, true);
            }
            finally
            {
                ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }
        }

        private Messages GetProduct(string rcard)
        {
            Messages productmessages = new Messages();
            product = null;
            ActionOnLineHelper dataCollect = new ActionOnLineHelper(this.DataProvider);
            productmessages.AddMessages(dataCollect.GetIDInfo(rcard.Trim().ToUpper()));
            if (productmessages.IsSuccess())
            {
                product = (ProductInfo)productmessages.GetData().Values[0];
            }
            dataCollect = null;
            return productmessages;
        }

        private void InitialItemListOfFGRCard()
        {
            this.labelItemDesc.Text = "";
            this.m_MaterialList.Rows.Clear();
            this.m_MaterialList.AcceptChanges();
        }

        private void ultraGridMaterialList_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "ItemCode";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["ItemCode"].Header.Caption = "料号";
            e.Layout.Bands[0].Columns["ItemDescription"].Header.Caption = "产品描述";
            e.Layout.Bands[0].Columns["RCard"].Header.Caption = "产品序列号";
            e.Layout.Bands[0].Columns["Version"].Header.Caption = "软件版本";
            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["ItemCode"].Width = 150;
            e.Layout.Bands[0].Columns["ItemDescription"].Width = 200;
            e.Layout.Bands[0].Columns["RCard"].Width = 150;
            e.Layout.Bands[0].Columns["Version"].Width = 100;
            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["ItemCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ItemDescription"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["RCard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["Version"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
        }

        private void ultraGridUpgradeLog_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
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

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "RCard";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["RCard"].Header.Caption = "产品序列号";
            e.Layout.Bands[0].Columns["ItemDescription"].Header.Caption = "产品描述";
            e.Layout.Bands[0].Columns["SemiGoodRCard"].Header.Caption = "半成品序列号";
            e.Layout.Bands[0].Columns["OldVersion"].Header.Caption = "原软件版本";
            e.Layout.Bands[0].Columns["NewVersion"].Header.Caption = "最新软件版本";

            e.Layout.Bands[0].Columns["RCard"].Width = 150;
            e.Layout.Bands[0].Columns["ItemDescription"].Width = 200;
            e.Layout.Bands[0].Columns["SemiGoodRCard"].Width = 150;
            e.Layout.Bands[0].Columns["OldVersion"].Width = 100;
            e.Layout.Bands[0].Columns["NewVersion"].Width = 100;
            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["RCard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["ItemDescription"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["SemiGoodRCard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["OldVersion"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["NewVersion"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[0].Columns["RCard"].SortIndicator = SortIndicator.Ascending;
        }

        private void ucLabelEditRCard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.DoUpgrade();
            }
        }

        private void DoUpgrade()
        {
            if (this.ucLabelEditRCard.Value.Trim().Length == 0)
            {
                this.ucLabelEditRCard.TextFocus(true, true);
                return;
            }

            if (this.ucLabelEditSoftVersion.Value.Trim().Length == 0)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_PleaseChooseSoftVersion"));
                this.ucLabelEditSoftVersion.TextFocus(false, true);
                return;
            }

            if (this.m_MaterialList.Rows.Count == 0)
            {
                this.ucLabelEditRCard.Value = "";
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_PleaseInputFGRcard"));
                this.ucLabelEditRCardFG.TextFocus(false, true);
                return;
            }

            DataView dv = this.m_MaterialList.Copy().DefaultView;
            dv.RowFilter = "Checked='True'";
            if (dv.Count == 0)
            {
                this.ucLabelEditRCard.Value = "";
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_MustChooseOneMItem"));
                return;
            }

            string newVersion = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.ucLabelEditSoftVersion.Value));
            string mItemCode = dv[0].Row["ItemCode"].ToString();
            string rcard = FormatHelper.PKCapitalFormat(this.ucLabelEditRCard.Value.Trim());           

            Messages messages = new Messages();
            ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();

            try
            {
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceRCard = dataCollectFacade.GetSourceCard(rcard.Trim().ToUpper(), string.Empty);

                // Get Product Info
                messages = this.GetProduct(sourceRCard);
                if (!messages.IsSuccess() || product == null || product.LastSimulation == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$NoProductInfo"));
                    this.ucLabelEditRCard.TextFocus(false, true);
                    return;
                }
                DataCollectFacade dcf = new DataCollectFacade(this.DataProvider);
                OnWipSoftVer4Upgrade oldVersion = dcf.GetOnWipSoftVersion2Upgrade(sourceRCard, mItemCode);
                if (oldVersion == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CanNotFindItemSoftVer"));
                    this.ucLabelEditRCard.TextFocus(false, true);
                    return;
                }

                if (string.Compare(oldVersion.SoftwareVersion, newVersion, true) == 0)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_AlreadyUpgraded"));
                    this.ucLabelEditRCard.TextFocus(false, true);
                    return;
                }

                BaseModelFacade bmf = new BaseModelFacade(this.DataProvider);
                Resource resource = (Domain.BaseSetting.Resource)bmf.GetResource(ApplicationService.Current().ResourceCode);

                messages.AddMessages(dcf.UpgradeNewSoftVer(oldVersion, resource, ApplicationService.Current().UserCode, newVersion));
                if (!messages.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().Add(messages);
                    this.ucLabelEditRCard.TextFocus(false, true);
                    return;
                }

                DataRow rowNew = this.m_UpgradeLog.NewRow();
                rowNew["RCard"] = rcard;
                rowNew["ItemDescription"] = oldVersion.ItemDescription;
                rowNew["SemiGoodRCard"] = oldVersion.RunningCard;
                rowNew["OldVersion"] = oldVersion.SoftwareVersion;
                rowNew["NewVersion"] = newVersion;
                this.m_UpgradeLog.Rows.Add(rowNew);
                this.m_UpgradeLog.AcceptChanges();
                this.ucLabelEditCount.Value = this.m_UpgradeLog.Rows.Count.ToString();

                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_UpgradeSuccess"));
                this.ucLabelEditRCard.TextFocus(false, true);
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(ex.Message);
                this.ucLabelEditRCard.TextFocus(false, true);
            }
            finally
            {
                ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                ((SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }
        }

        private void ultraGridMaterialList_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            this.ultraGridMaterialList.UpdateData();
            if (string.Compare(e.Cell.Column.Key, "Checked", true) == 0)
            {
                if (string.Compare(e.Cell.Value.ToString(), "true", true) == 0)
                {
                    for (int i = 0; i < this.ultraGridMaterialList.Rows.Count; i++)
                    {
                        if (this.ultraGridMaterialList.Rows[i] != e.Cell.Row && 
                            string.Compare(this.ultraGridMaterialList.Rows[i].Cells["Checked"].Value.ToString(), "true", true) == 0)
                        {
                            this.ultraGridMaterialList.Rows[i].Cells["Checked"].Value = "false";
                        }
                    }
                }

                this.ultraGridMaterialList.UpdateData();
            }
        }

        private void FSoftVersionUpgrade_FG_Load(object sender, EventArgs e)
        {
            this.InitialUltraGridDataSource();
        }

        
    }
}