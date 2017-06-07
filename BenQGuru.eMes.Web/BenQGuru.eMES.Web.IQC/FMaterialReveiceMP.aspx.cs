using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;

using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.SAPDataTransfer;
using BenQGuru.eMES.SAPDataTransferInterface;
using System.Web.UI.MobileControls;
using System.Collections.Generic;

namespace BenQGuru.eMES.Web.IQC
{
    public partial class FMaterialReveiceMP : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private IQCFacade m_iqcFacade = null;
        private IQCFacade iqcFacade
        {
            get
            {
                if (this.m_iqcFacade == null)
                {
                    this.m_iqcFacade = new IQCFacade(this.DataProvider);
                }
                return this.m_iqcFacade;
            }
        }


        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitUI();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected void drpStatus_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpStatusQuery.Items.Insert(0, new ListItem("", ""));
                this.drpStatusQuery.Items.Insert(1, new ListItem(FlagStatus.FlagStatus_MES, FlagStatus.FlagStatus_MES));
                this.drpStatusQuery.Items.Insert(2, new ListItem(FlagStatus.FlagStatus_POST, FlagStatus.FlagStatus_POST));
                this.drpStatusQuery.Items.Insert(3, new ListItem(FlagStatus.FlagStatus_SAP, FlagStatus.FlagStatus_SAP));
            }
        }

        protected void drpSRMStatusQuery_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpSRMStatusQuery.Items.Insert(0, new ListItem("", ""));
                this.drpSRMStatusQuery.Items.Insert(1, new ListItem(FlagStatus.FlagStatus_MES, FlagStatus.FlagStatus_MES));
                this.drpSRMStatusQuery.Items.Insert(2, new ListItem(FlagStatus.FlagStatus_SRM, FlagStatus.FlagStatus_SRM));
            }
        }

        #region WebGrid

        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("IQCNo", "送检单", null);
            this.gridHelper.AddColumn("STLine", "ASN行", null);
            this.gridHelper.AddColumn("MaterialCode", "物料代码", null);
            this.gridHelper.AddColumn("MaterialDescription", "物料描述", null);
            this.gridHelper.AddColumn("OrderNo", "采购订单", null);
            this.gridHelper.AddColumn("OrderLine", "订单行", null);
            this.gridHelper.AddColumn("ReceiveQty", "收货数量", null);
            this.gridHelper.AddColumn("ReceiveMemo", "抬头文本", null);
            this.gridHelper.AddColumn("AccountDate", "记账日期", null);
            this.gridHelper.AddColumn("VoucherDate", "凭证日期", null);
            this.gridHelper.AddColumn("OrgID", "订单工厂", null);
            this.gridHelper.AddColumn("StorageID", "入库库别", null);
            this.gridHelper.AddColumn("Unit", "单位", null);
            this.gridHelper.AddColumn("Status", "状态", null);
            this.gridHelper.AddColumn("ErrorMessage", "错误信息", null);
            this.gridHelper.AddColumn("MDATE", "维护日期", null);
            this.gridHelper.AddColumn("MTIME", "维护时间", null);
            this.gridHelper.AddColumn("MUSER", "维护人", null);
            this.gridHelper.AddLinkColumn("SyncStatus", "状态同步", null);
            this.gridHelper.AddLinkColumn("ConfirmReceiveAgain", "再次报帐", null);
            this.gridHelper.AddColumn("SRMFlag", "SRM报账状态", null);
            this.gridHelper.AddColumn("SRMErrorMessage", "SRM错误信息", null);
            this.gridHelper.AddLinkColumn("SRMSyncStatus", "SRM状态同步", null);
            this.gridHelper.AddColumn("STNo", "ST单", null);

            this.gridWebGrid.Columns.FromKey("ConfirmReceiveAgain").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MDATE").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MTIME").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MUSER").Hidden = true;
            this.gridWebGrid.Columns.FromKey("STNo").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override void cmdQuery_Click(object sender, EventArgs e)
        {
            PageCheckManager manager = new PageCheckManager();
            if (this.txtFactoryQuery.Text.Trim().Length > 0)
            {
                manager.Add(new NumberCheck(this.lblFactoryQuery, this.txtFactoryQuery, false));
            }
            manager.Add(new DateRangeCheck(this.lblVoucherDateFrom, this.dateVoucherDateFrom.Text, this.dateVoucherDateTo.Text, false));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return;
            }

            base.cmdQuery_Click(sender, e);
        }

        protected override int GetRowCount()
        {
            return this.iqcFacade.QueryMaterialReceiveCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNoQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOrderNoQuery.Text)),
                string.IsNullOrEmpty(this.txtFactoryQuery.Text.Trim()) ? 0 : int.Parse(this.txtFactoryQuery.Text.Trim()),
                FormatHelper.CleanString(this.drpStatusQuery.SelectedValue),
                FormatHelper.CleanString(this.drpSRMStatusQuery.SelectedValue),
                FormatHelper.TODateInt(this.dateVoucherDateFrom.Text),
                FormatHelper.TODateInt(this.dateVoucherDateTo.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtWarehouseType.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVerdorCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtKeepManQuery.Text)));
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            return this.iqcFacade.QueryMaterialReceive(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtIQCNoQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOrderNoQuery.Text)),
                string.IsNullOrEmpty(this.txtFactoryQuery.Text.Trim()) ? 0 : int.Parse(this.txtFactoryQuery.Text.Trim()),
                FormatHelper.CleanString(this.drpStatusQuery.SelectedValue),
                FormatHelper.CleanString(this.drpSRMStatusQuery.SelectedValue),
                FormatHelper.TODateInt(this.dateVoucherDateFrom.Text),
                FormatHelper.TODateInt(this.dateVoucherDateTo.Text),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtWarehouseType.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtVerdorCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtKeepManQuery.Text)),
                inclusive, exclusive);
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            MaterialReceiveExtend mre = obj as MaterialReceiveExtend;
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{
                                mre.IQCNo,
                                mre.STLine.ToString(),
                                mre.ItemCode,
                                mre.MaterialDescription,
                                mre.OrderNo,
                                mre.OrderLine.ToString(),
                                mre.RealReceiveQty.ToString(),
                                mre.ReceiveMemo,
                                FormatHelper.ToDateString(mre.AccountDate),
                                FormatHelper.ToDateString(mre.VoucherDate),
                                mre.OrganizationID.ToString(),
                                mre.StorageID,
                                mre.Unit,
                                mre.Flag,
                                string.Compare(mre.Flag, "SAP", true) == 0 ? "" : mre.ErrorMessage,
                                mre.MaintainDate,
                                mre.MaintainTime,
                                mre.MaintainUser,
                                "",
                                "",
                                mre.SRMFlag == null ? string.Empty : mre.SRMFlag,
                                mre.SRMErrorMessage == null ? string.Empty : mre.SRMErrorMessage,
                                "",
                                mre.STNo
                });
        }

        protected override void Grid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
        {
            string userCode = this.GetUserCode();

            if (this.gridHelper.IsClickColumn("SyncStatus", e))
            {
                if (string.Compare(e.Cell.Row.Cells.FromKey("Status").Value.ToString(), FlagStatus.FlagStatus_MES, true) != 0
                    && string.Compare(e.Cell.Row.Cells.FromKey("Status").Value.ToString(), FlagStatus.FlagStatus_POST, true) != 0)
                {
                    WebInfoPublish.Publish(this, "$Error_OnlyMESAndPOSTStatusCanDo", this.languageComponent1);
                }
                else
                {
                    this.iqcFacade.ManualSyncMaterialReceiveFlag(
                        e.Cell.Row.Cells.FromKey("IQCNo").Value.ToString(),
                        int.Parse(e.Cell.Row.Cells.FromKey("STLine").Value.ToString()),
                        userCode);

                    this.cmdQuery_Click(null, null);
                }
            }

            //if (this.gridHelper.IsClickColumn("ConfirmReceiveAgain", e))
            //{
            //    if (string.Compare(e.Cell.Row.Cells.FromKey("Status").Value.ToString(), FlagStatus.FlagStatus_MES, true) != 0)
            //    {
            //        WebInfoPublish.Publish(this, "$Error_OnlyMESStatusCanDo", this.languageComponent1);
            //    }
            //    else
            //    {
            //        MaterialReceive mr = this.iqcFacade.GetMaterialReceive(e.Cell.Row.Cells.FromKey("IQCNo").Value.ToString(),
            //            int.Parse(e.Cell.Row.Cells.FromKey("STLine").Value.ToString())) as MaterialReceive;

            //        if (mr != null)
            //        {
            //            // 再次报工
            //            DT_MES_SOURCESTOCK_REQLIST po = Post2SAPUtility.GenerateSAPPOInfo(mr);
            //            MaterialPOTransferArgument mpArg = new MaterialPOTransferArgument(this.DataProvider);
            //            mpArg.InventoryList.Add(po);

            //            mr.TransactionCode = mpArg.TransactionCode;
            //            this.iqcFacade.UpdateMaterialReceive(mr);

            //            ServiceResult sr = Post2SAPUtility.CallSAPInterface(mpArg);

            //            if (sr.Result == true)
            //            {

            //            }
            //            else
            //            {
            //                WebInfoPublish.Publish(this, sr.Message + " Transaction Code=" + sr.TransactionCode, this.languageComponent1);
            //            }
            //        }
            //    }

            //}

            else if (this.gridHelper.IsClickColumn("SRMSyncStatus", e))
            {
                if (!this.iqcFacade.IsFromASN(e.Cell.Row.Cells.FromKey("STNo").Value.ToString()))
                {
                    WebInfoPublish.Publish(this, "$Error_OnlyFromASNCanDo", this.languageComponent1);
                }
                else if (string.Compare(e.Cell.Row.Cells.FromKey("SRMFlag").Value.ToString(), FlagStatus.FlagStatus_MES, true) != 0)
                {
                    WebInfoPublish.Publish(this, "$Error_OnlyMESStatusCanDo", this.languageComponent1);
                }
                else
                {
                    this.iqcFacade.ManualSyncIQCDetailSRMFlag(
                        e.Cell.Row.Cells.FromKey("IQCNo").Value.ToString(),
                        int.Parse(e.Cell.Row.Cells.FromKey("STLine").Value.ToString()),
                        userCode);

                    this.cmdQuery_Click(null, null);
                }
            }
        }

        #endregion

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"IQCNo",
                                    "STLine",
                                    "MaterialCode",	
                                    "MaterialDescription",
                                    "OrderNo",
                                    "OrderLine",
                                    "ReceiveQty",
                                    "ReceiveMemo",
                                    "AccountDate",
                                    "VoucherDate",	
                                    "OrgID",
                                    "StorageID",
                                    "Unit",
                                    "Status",	
                                    "ErrorMessage",
                                    "SRMFlag",
                                    "SRMErrorMessage"};
        }

        protected override string[] FormatExportRecord(object obj)
        {
            MaterialReceiveExtend mre = obj as MaterialReceiveExtend;
            return new string[]{
                                mre.IQCNo,
                                mre.STLine.ToString(),
                                mre.ItemCode,
                                mre.MaterialDescription,
                                mre.OrderNo,
                                mre.OrderLine.ToString(),
                                mre.RealReceiveQty.ToString(),
                                mre.ReceiveMemo,
                                FormatHelper.ToDateString(mre.AccountDate),
                                FormatHelper.ToDateString(mre.VoucherDate),
                                mre.OrganizationID.ToString(),
                                mre.StorageID,
                                mre.Unit,
                                mre.Flag,
                                mre.ErrorMessage,
                                mre.SRMFlag == null ? string.Empty : mre.SRMFlag,
                                mre.SRMErrorMessage == null ? string.Empty : mre.SRMErrorMessage
                };
        }


    }
}
