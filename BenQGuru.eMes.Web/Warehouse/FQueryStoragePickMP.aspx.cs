using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using Infragistics.WebUI.UltraWebGrid;
using System.Collections.Generic;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FQueryStoragePickMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private WarehouseFacade _WarehouseFacade = null;
        private WarehouseFacade _TransferFacade;
        SystemSettingFacade _SystemSettingFacade = null;
        private InventoryFacade facade = null;
        private UserFacade _UserFacade = null;
        private InventoryFacade _InventoryFacade = null;
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
            //this.gridWebGrid.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid_InitializeRow);
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

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言

                string pickNo = Request.QueryString["PickNo"];

                this.InitPageLanguage(this.languageComponent1, false);
                this.DrpPickTypeList();
                InitStorageList();
                txtPickNoQuery.Text = pickNo;


                InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
            }

        }



        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }


        #region 默认查询
        private void RequestData()
        {

            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }
        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);


            #region Exporter
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this.excelExporter.Page = this;
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
            #endregion

        }
        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        #endregion

        #region 下拉框
        /// <summary>
        /// 初始化库位
        /// </summary>
        //private void InitStorageList()
        //{
        //    if (facade == null)
        //    {
        //        facade = new InventoryFacade(base.DataProvider);
        //    }
        //    this.drpOutStackListQuery.Items.Add(new ListItem("", ""));
        //    object[] objStorage = facade.GetAllStorage();
        //    if (objStorage != null && objStorage.Length > 0)
        //    {
        //        foreach (Storage storage in objStorage)
        //        {

        //            this.drpOutStackListQuery.Items.Add(new ListItem(
        //                 storage.StorageName, storage.StorageCode)
        //                );
        //        }
        //    }
        //    this.drpOutStackListQuery.SelectedIndex = 0;
        //}
        private void InitStorageList()
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser(GetUserCode());//+TOSTORAGE
            this.drpOutStackListQuery.Items.Add(new ListItem("", ""));
            object[] parameters = systemSettingFacade.GetDistinctParaInParameterGroup(usergroupList);
            if (parameters != null)
            {
                foreach (Domain.BaseSetting.Parameter parameter in parameters)
                {
                    drpOutStackListQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterCode));
                }
            }
            this.drpOutStackListQuery.SelectedIndex = 0;

        }



        //单据类型下拉框
        /// <summary>
        /// 单据类型下拉框
        /// </summary>
        private void DrpPickTypeList()
        {
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("PICKTYPE");
            this.drpPickTypeQuery.Items.Add(new ListItem("", ""));
            foreach (Domain.BaseSetting.Parameter parameter in parameters)
            {
                this.drpPickTypeQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
            }
            this.drpPickTypeQuery.SelectedIndex = 0;
        }
        #endregion


        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("SERIALNUMBER", "序号", false);
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                this.gridHelper.AddColumn(this.PickHeadViewFieldList[i].FieldName, this.PickHeadViewFieldList[i].Description, null);
            }
            //this.gridHelper.AddLinkColumn("LinkDetail", "详细信息");
            this.gridHelper.AddLinkColumn("SAPLinkDetail", "SAP详细信息");
            this.gridHelper.AddLinkColumn("PickLinkDetail", "拣料作业");
            this.gridWebGrid.Columns.FromKey("SERIALNUMBER").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, false);
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            if (!string.IsNullOrEmpty(txtPickNoQuery.Text))
            {
                this.gridHelper.RequestData();
            }
        }



        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);
                if (obj != null)
                {
                    this.SetEditObject(obj);
                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
            else if (commandName == "LinkDetail")
            {
                string pickNo = row.Items.FindItemByKey("PickNo").Text.Trim();
                Response.Redirect(this.MakeRedirectUrl("FQueryPickLineMP.aspx", new string[] { "ACT", "PickNo" }, new string[] { "LinkDetail", pickNo }));
            }
            else if (commandName == "PickLinkDetail")
            {
                string pickNo = row.Items.FindItemByKey("PickNo").Text.Trim();
                Response.Redirect(this.MakeRedirectUrl("FPickDoneMP.aspx", new string[] { "ACT", "PickNo" }, new string[] { "LinkDetail", pickNo }));
            }
            else if (commandName == "SAPLinkDetail")
            {
                if (facade == null)
                {
                    facade = new InventoryFacade(base.DataProvider);
                }
                string pickNo = row.Items.FindItemByKey("PickNo").Text.Trim();
                Pick pick = (Pick)facade.GetPick(pickNo);
                if (pick != null)
                {
                    string pickType = pick.PickType;
                    string dnBatchNo = "";
                    string invNo = "";
                    if (pickType == "DNC" || pickType == "DNZC")
                    {
                        dnBatchNo = pick.InvNo;
                    }
                    else
                    {
                        invNo = pick.InvNo;
                    }
                    Response.Redirect(this.MakeRedirectUrl("FSAPInvoicesQuery.aspx", new string[] { "ACT", "InvNo", "DnBatchNo", "Page", "PickNo" }, new string[] { "SAPLinkDetail", invNo, dnBatchNo, "FQueryStoragePickMP.aspx", pick.PickNo }));

                }
            }
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            Pick pick = obj as Pick;
            Type type = pick.GetType();
            row[1] = pick.SerialNumber.ToString();
            BenQGuru.eMES.MOModel.ItemFacade itemFacade = new BenQGuru.eMES.MOModel.ItemFacade(this.DataProvider);
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                ViewField field = this.PickHeadViewFieldList[i];
                string strValue = string.Empty;
                System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
                if (fieldInfo != null)
                {
                    strValue = fieldInfo.GetValue(pick).ToString();
                }
                if (field.FieldName == "Status")
                {
                    if (pick.Status == PickHeadStatus.PickHeadStatus_Close)
                    {
                        strValue = "已出库";
                    }
                    else
                    {
                        strValue = languageComponent1.GetString(pick.Status);
                    }
                }
                else if (field.FieldName == "PickType")
                {
                    strValue = GetPickTypeName(pick.PickType);
                }
                else if (field.FieldName == "StorageOut")
                {
                    strValue = pick.StorageCode;
                }
                else if (field.FieldName == "PlanSendDate")
                {
                    strValue = GetDate(pick.PlanDate);
                }
                else if (field.FieldName == "PickFinishDate")
                {
                    strValue = GetDate(pick.FinishDate);
                }
                else if (field.FieldName == "VenderName")
                {
                    string venderCode = pick.VenderCode;
                    Vendor vendor = (Vendor)itemFacade.GetVender(venderCode);
                    if (vendor != null)
                    {
                        strValue = vendor.VendorName;
                    }
                }
                row[i + 2] = strValue;
            }
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            #region
            string chk = "";
            //if (chbReleaseQuery.Checked)
            //{ chk += "'" + PickHeadStatus.PickHeadStatus_Release + "',"; }
            if (chbWaitPickQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_WaitPick + "',"; }
            if (chbPickQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Pick + "',"; }
            if (chbMakePackingListQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_MakePackingList + "',"; }
            if (chbPackQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Pack + "',"; }
            if (chbOQCQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_OQC + "',"; }
            if (chbClosePackingListQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_ClosePackingList + "',"; }
            if (chbCloseQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Close + "',"; }
            if (chbCancelQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Cancel + "',"; }
            if (chbBlockQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Block + "',"; }
            if (chbPackingListingQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_PackingListing + "',"; }
            #endregion
            return this.facade.QueryStoragePick(
           FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPickNoQuery.Text)),
           this.drpPickTypeQuery.SelectedValue,
           FormatHelper.CleanString(this.drpOutStackListQuery.SelectedValue),
           FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoQuery.Text)),
          chk,

           FormatHelper.CleanString(this.txtCusBatchNo.Text),
           FormatHelper.TODateInt(this.txtCBDateQuery.Text),
           FormatHelper.TODateInt(this.txtCEDateQuery.Text),
           GetUserCode(),
           inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            #region
            string chk = "";
            //if (chbReleaseQuery.Checked)
            //{ chk += "'" + PickHeadStatus.PickHeadStatus_Release + "',"; }
            if (chbWaitPickQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_WaitPick + "',"; }
            if (chbPickQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Pick + "',"; }
            if (chbMakePackingListQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_MakePackingList + "',"; }
            if (chbPackQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Pack + "',"; }
            if (chbOQCQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_OQC + "',"; }
            if (chbClosePackingListQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_ClosePackingList + "',"; }
            if (chbCloseQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Close + "',"; }
            if (chbCancelQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Cancel + "',"; }
            if (chbBlockQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Block + "',"; }
            #endregion
            return this.facade.QueryStoragePickCount(
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPickNoQuery.Text)),
           this.drpPickTypeQuery.SelectedValue,
           FormatHelper.CleanString(this.drpOutStackListQuery.SelectedValue),
           FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoQuery.Text)),
          chk,

           FormatHelper.CleanString(this.txtCusBatchNo.Text),
           FormatHelper.TODateInt(this.txtCBDateQuery.Text),
           FormatHelper.TODateInt(this.txtCEDateQuery.Text), GetUserCode()
                  );
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            string[] objs = new string[this.PickHeadViewFieldList.Length + 1];
            Pick pick = obj as Pick;
            Type type = pick.GetType();
            objs[0] = pick.SerialNumber.ToString();
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                ViewField field = this.PickHeadViewFieldList[i];
                string strValue = string.Empty;
                System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName);
                if (fieldInfo != null)
                {
                    strValue = fieldInfo.GetValue(pick).ToString();
                }
                if (field.FieldName == "Status")
                {
                    if (pick.Status == PickHeadStatus.PickHeadStatus_Close)
                    {
                        strValue = "已出库";
                    }
                    else
                    {
                        strValue = languageComponent1.GetString(pick.Status);
                    }
                }
                else if (field.FieldName == "PickType")
                {
                    strValue = GetPickTypeName(pick.PickType);
                }
                else if (field.FieldName == "StorageOut")
                {
                    strValue = pick.StorageCode;
                }
                else if (field.FieldName == "PlanSendDate")
                {
                    strValue = GetDate(pick.PlanDate); //FormatHelper.ToDateString(pick.PlanDate);
                }
                else if (field.FieldName == "PickFinishDate")
                {
                    strValue = GetDate(pick.FinishDate);
                }

                objs[i + 1] = strValue;
            }
            return objs;
        }
        private string GetDate(int date)
        {

            return date.ToString();
        }

        protected override string[] GetColumnHeaderText()
        {
            string[] strHeader = new string[this.PickHeadViewFieldList.Length + 1];
            strHeader[0] = "序号";
            for (int i = 0; i < strHeader.Length - 1; i++)
            {
                strHeader[i + 1] = this.PickHeadViewFieldList[i].Description;
            }
            return strHeader;
        }

        #endregion

        #region 编辑

        protected override object GetEditObject(GridRecord row)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            object obj = facade.GetPick(row.Items.FindItemByKey("PickNo").Text);
            if (obj != null)
            {
                return obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            Pick pick = obj as Pick;
            if (pick == null)
            {
                this.txtPickNoEdit.Text = string.Empty;
                this.txtASNCodeEdit.Text = string.Empty;
                this.txtPlanSendDateEdit.Text = "";
                txtMemoEdit.Text = string.Empty;
                return;
            }
            this.txtPickNoEdit.Text = pick.PickNo;
            this.txtASNCodeEdit.Text = pick.StNo.ToString();
            this.txtPlanSendDateEdit.Text = FormatHelper.ToDateString(pick.PlanDate);
            txtMemoEdit.Text = pick.Remark1;

        }
        #endregion

        #region Button

        #region 批量保存

        protected void cmdLotSave_ServerClick(object sender, EventArgs e)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {
                    string pickno = this.gridWebGrid.Rows[i].Items.FindItemByKey("PickNo").Value.ToString();
                    Pick pick = (Pick)facade.GetPick(pickno);
                    if (pick != null)
                    {
                        pick.SerialNumber = int.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("SERIALNUMBER").Value.ToString());
                        facade.UpdatePick(pick);
                    }
                }
                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "批量保存成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }

        #endregion

        #region 取消下发
        protected void cmdInitial_ServerClick(object sender, EventArgs e)
        {
            GetServerClick("cmdInitial_ServerClick");
        }

        protected void CmdInitialObjects(object[] pickList)
        {
            //取消下发按钮：将拣货任务令从状态为：WaitPick:待拣料更新为：Release:初始化，仅针状态为：WaitPick:待拣料的拣货任务令可取消下发
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            _WarehouseFacade=new WarehouseFacade(base.DataProvider);
            
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (Pick pick in pickList)
                {
                    if (pick.Status == PickHeadStatus.PickHeadStatus_Close)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "拣货任务令已出库的不能取消下发", this.languageComponent1);
                        return;
                    }
                    else
                    {
                        Pick oldpick = (Pick)facade.GetPick(pick.PickNo);
                        if (oldpick != null)
                        {
                            oldpick.Status = PickHeadStatus.PickHeadStatus_Release;
                            facade.UpdatePick(oldpick);
                        }
                        //，将拣货任务令的头状态、行状态都变为初始化；OQC单据状态变为取消；
                        //facade.UpdatePickDetailStatusByPickNo(PickHeadStatus.PickHeadStatus_Release, pick.PickNo, PickHeadStatus.PickHeadStatus_WaitPick);
                        facade.UpdatePickDetailStatusByPickNoNotCancel(PickHeadStatus.PickHeadStatus_Release, pick.PickNo);
                        facade.UpdateOQCStatusByPickno("Cancel", pick.PickNo);



                        object[] pickObj = _WarehouseFacade.GetCartoninvoicesByPickNo(pick.PickNo);
                        if (pickObj != null && pickObj.Length > 0)
                        {
                            foreach (CARTONINVOICES c in pickObj)
                            {
                                c.STATUS = "Pack";
                                _WarehouseFacade.UpdateCartoninvoices(c);
                            }
                        
                        }
                        WarehouseFacade ware = new WarehouseFacade(base.DataProvider);
                        DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                        
                        InvInOutTrans trans = ware.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = string.Empty;
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = pick.InvNo;//.InvNo;
                        trans.InvType = pick.PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = dbTime1.DBDate;
                        trans.MaintainTime = dbTime1.DBTime;
                        trans.MaintainUser = this.GetUserCode();
                        trans.MCode = string.Empty;
                        trans.ProductionDate = 0;
                        trans.Qty = 0;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = string.Empty;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = pick.PickNo;// asnIqc.IqcNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "CANCELISSUE";
                        ware.AddInvInOutTrans(trans);
                    }


                }
                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "取消下发成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }

        }
        #endregion

        #region 下发
        protected void cmdRelease_ServerClick(object sender, EventArgs e)
        {
            GetServerClick("cmdRelease_ServerClick");
        }

        protected void CmdReleaseObjects(object[] pickList)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (Pick pick in pickList)
                {
                    if (string.IsNullOrEmpty(pick.StorageCode))
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, pick.PickNo + "拣货任务令出库库位不能空", this.languageComponent1);
                        return;
                    }

                    if (pick.Status == PickHeadStatus.PickHeadStatus_Release)
                    {
                        if (pick.PickType == PickType.PickType_DNZC)
                        {
                            PickDetail[] pickmcodeList = facade.GetPickDetailMCodeByPickNo1(pick.PickNo);
                            string stno = GetStNo(pick.StNo);
                            ASNDetail[] asnmCodeList = facade.GetAsnDetailMCodeByStNo1(stno);

                            #region 需求物料和数量不一致

                            if (pickmcodeList.Length != asnmCodeList.Length)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "对应 ASN号和拣货任务令的需求物料和数量不一致", this.languageComponent1);
                                return;
                            }

                            #endregion


                            object[] cartonNoList = facade.QueryAsnDetailCartonNo(stno);

                            Dictionary<string, List<CartonInvDetailMaterial>> cartonsGroupByDQMCode = new Dictionary<string, List<CartonInvDetailMaterial>>();
                            if (cartonNoList == null || cartonNoList.Length <= 0)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, pick.StNo + "对应的入库指令明细不存在！", this.languageComponent1);
                                return;
                            }


                            CARTONINVOICES CartonH = new CARTONINVOICES();
                            string date = dbTime.DBDate.ToString().Substring(2, 6);
                            string serialNo = CreateSerialNo(date);
                            string carinvno = "K" + date + serialNo;
                            CartonH.CARINVNO = carinvno;
                            CartonH.PICKNO = pick.PickNo;
                            CartonH.STATUS = CartonInvoices_STATUS.Status_ClosePackingList;

                            CartonH.FDATE = dbTime.DBDate;
                            CartonH.FTIME = dbTime.DBTime;
                            CartonH.CDATE = dbTime.DBDate;
                            CartonH.CTIME = dbTime.DBTime;
                            CartonH.CUSER = this.GetUserCode();
                            CartonH.MDATE = dbTime.DBDate;
                            CartonH.MTIME = dbTime.DBTime;
                            CartonH.MUSER = this.GetUserCode();
                            _WarehouseFacade.AddCartoninvoices(CartonH);




                            foreach (Asndetail asnDetail in cartonNoList)
                            {
                                //如果一致，则将ASN中的物料SN条码 匹配到拣货任务令的已拣物料中。自动产生发货箱单，
                                #region 自动产生发货箱单，

                                BenQGuru.eMES.Domain.Warehouse.CartonInvDetail c2 = new BenQGuru.eMES.Domain.Warehouse.CartonInvDetail();
                                string caseNo = cmdCreateBarCode();
                                c2.CARINVNO = CartonH.CARINVNO;
                                c2.STATUS = "ClosePack";
                                c2.PICKNO = CartonH.PICKNO;
                                c2.CARTONNO = asnDetail.Cartonno;
                                c2.CUSER = GetUserCode();
                                c2.CDATE = FormatHelper.TODateInt(DateTime.Now);
                                c2.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                                c2.MDATE = FormatHelper.TODateInt(DateTime.Now);
                                c2.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                                c2.MUSER = GetUserCode();
                                _WarehouseFacade.AddCartonInvDetail(c2);

                                Pickdetailmaterial pikm = new Pickdetailmaterial();
                                pikm.Cartonno = asnDetail.Cartonno;
                                pikm.CDate = dbTime.DBDate;
                                pikm.CTime = dbTime.DBTime;
                                pikm.CUser = this.GetUserCode();
                                pikm.CustmCode = asnDetail.CustmCode;
                                pikm.DqmCode = asnDetail.DqmCode;
                                pikm.LocationCode = "";// stor.LocationCode;
                                pikm.Lotno = asnDetail.Lotno;
                                pikm.MaintainDate = dbTime.DBDate;
                                pikm.MaintainTime = dbTime.DBTime;
                                pikm.MaintainUser = this.GetUserCode();
                                pikm.MCode = asnDetail.MCode;
                                pikm.Pickline = asnDetail.Stline;
                                pikm.Pickno = pick.PickNo;
                                pikm.Production_Date = asnDetail.Production_Date;
                                StorageDetail sd = (StorageDetail)_WarehouseFacade.GetStorageDetail(asnDetail.Cartonno);
                                pikm.PQty = asnDetail.Qty;
                                pikm.LocationCode = sd.LocationCode;
                                //pikm.QcStatus = string.Empty;
                                pikm.Qty = asnDetail.Qty;
                                //pikm.Status = string.Empty;   ////xu yao xiu gai 
                                pikm.StorageageDate = asnDetail.StorageageDate;
                                pikm.Supplier_lotno = asnDetail.Supplier_lotno;
                                pikm.Unit = asnDetail.Unit;
                                _WarehouseFacade.AddPickdetailmaterial(pikm);

                                CartonInvDetailMaterial cm = new CartonInvDetailMaterial();
                                cm.CARINVNO = CartonH.CARINVNO;
                                cm.CARTONNO = asnDetail.Cartonno;
                                cm.CDATE = FormatHelper.TODateInt(DateTime.Now);
                                cm.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                                cm.CUSER = GetUserCode();
                                cm.DQMCODE = asnDetail.DqmCode;
                                cm.MCODE = asnDetail.MCode;
                                cm.MDATE = FormatHelper.TODateInt(DateTime.Now);
                                cm.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                                cm.MUSER = GetUserCode();
                                cm.PICKLINE = asnDetail.Stline;
                                cm.PICKNO = pick.PickNo;
                                cm.QTY = asnDetail.Qty;
                                cm.UNIT = asnDetail.Unit;
                                if (cartonsGroupByDQMCode.ContainsKey(cm.DQMCODE))
                                    cartonsGroupByDQMCode[cm.DQMCODE].Add(cm);
                                else
                                {
                                    cartonsGroupByDQMCode[cm.DQMCODE] = new List<CartonInvDetailMaterial>();
                                    cartonsGroupByDQMCode[cm.DQMCODE].Add(cm);
                                }

                                _WarehouseFacade.AddCartonInvDetailMaterial(cm);




                                Asndetailsn[] sns = _WarehouseFacade.GetAsnDetailSNs(asnDetail.Stno, asnDetail.Stline);

                                foreach (Asndetailsn snn in sns)
                                {
                                    CARTONINVDETAILSN sn = new CARTONINVDETAILSN();
                                    sn.CARINVNO = CartonH.CARINVNO;
                                    sn.CARTONNO = cm.CARTONNO;
                                    sn.MDATE = FormatHelper.TODateInt(DateTime.Now);
                                    sn.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                                    sn.MUSER = GetUserCode();
                                    sn.PICKNO = pick.PickNo;
                                    sn.PICKLINE = snn.Stline;
                                    sn.SN = snn.Sn;
                                    _WarehouseFacade.AddCARTONINVDETAILSN(sn);


                                    PickDetailMaterialSn pickmsn = new PickDetailMaterialSn();
                                    pickmsn.CartonNo = asnDetail.Cartonno;
                                    pickmsn.PickNo = pick.PickNo;
                                    pickmsn.PickLine = asnDetail.Stline;
                                    pickmsn.Sn = snn.Sn;

                                    // pickmsn.QcStatus = asnObj.QcStatus;
                                    pickmsn.MaintainUser = this.GetUserCode();
                                    pickmsn.MaintainDate = dbTime.DBDate;
                                    pickmsn.MaintainTime = dbTime.DBTime;
                                    facade.AddPickDetailMaterialSn(pickmsn);
                                }
                            }
                            foreach (string DQMCode in cartonsGroupByDQMCode.Keys)
                            {
                                PickDetail pd = _WarehouseFacade.GetPickDetail(pick.PickNo, DQMCode);
                                int PQTY = 0;
                                foreach (CartonInvDetailMaterial c in cartonsGroupByDQMCode[DQMCode])
                                {
                                    PQTY += (int)c.QTY;
                                }

                                pd.PQTY = PQTY;
                                pd.Status = "ClosePack";
                                _WarehouseFacade.UpdatePickDetailForDNCZ(pd);

                            }

                                #endregion
                            //拣货任务令头下发后为包装状态，拣货任务令行为包装完成状态
                            pick.Status = "ClosePackingList";
                            _WarehouseFacade.UpdatePickForDNCZ(pick,GetUserCode());

                        }
                        else
                        {
                            //拣货任务令头下发后为包装状态，拣货任务令行为包装完成状态
                            facade.UpdatePickStatusByPickNo(PickHeadStatus.PickHeadStatus_WaitPick, pick.PickNo);
                            facade.UpdatePickDetailStatusByPickNo(PickHeadStatus.PickHeadStatus_WaitPick, pick.PickNo, PickHeadStatus.PickHeadStatus_Release);
                        }

                    }
                    else
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "非初始化的拣货任务令不可下发", this.languageComponent1);
                        return;
                    }
                }

                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "下发成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }

        }
        private string CreateSerialNo(int stno)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            }
            string maxserial = _WarehouseFacade.GetMaxSerial("CT" + stno);

            //如果已是最大值就返回为空
            if (maxserial == "999999999")
            {
                return "";
            }

            SERIALBOOK serialbook = new SERIALBOOK();
            if (maxserial == "")
            {
                serialbook.SNPrefix = "CT" + stno;
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _WarehouseFacade.AddSerialBook(serialbook);
                return string.Format("{0:000000000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = "CT" + stno;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _WarehouseFacade.UpdateSerialBook(serialbook);
                return string.Format("{0:000000000}", int.Parse(serialbook.MaxSerial));
            }
        }
        protected string cmdCreateBarCode()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            //点击生成条码按钮，根据数量输入框输入的数量生成箱号数，显示在Grid中并保存在TBLBARCODE表中
            //4.	鼎桥箱号编码规则：CT+年月日+九位流水码：如：CT20160131000000001，流水码不归零(流水码创建对应的Sequences累加)

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int date = dbDateTime.DBDate;
            //try
            //{
            //    this.DataProvider.BeginTransaction();

            BarCode bar = new BarCode();
            string serialNo = CreateSerialNo(date);
            bar.BarCodeNo = "CT" + date + serialNo;
            bar.Type = "CARTONNO";
            bar.MCode = "";
            bar.EnCode = "";
            bar.SpanYear = date.ToString().Substring(0, 4);
            bar.SpanDate = date;
            if (!string.IsNullOrEmpty(serialNo))
            {
                bar.SerialNo = int.Parse(serialNo);
            }
            bar.PrintTimes = 0;
            bar.CUser = this.GetUserCode();	//	CUSER
            bar.CDate = dbDateTime.DBDate;	//	CDATE
            bar.CTime = dbDateTime.DBTime;//	CTIME
            bar.MaintainDate = dbDateTime.DBDate;	//	MDATE
            bar.MaintainTime = dbDateTime.DBTime;	//	MTIME
            bar.MaintainUser = this.GetUserCode();		//	MUSER
            _WarehouseFacade.AddBarCode(bar);
            return "CT" + date + serialNo;
            //    this.DataProvider.CommitTransaction();
            //}
            //catch (Exception ex)
            //{
            //    this.DataProvider.RollbackTransaction();
            //    WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            //}
        }
        # endregion

        #region GetServerClick

        private void GetServerClick(string clickName)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            object obj = null;

            if (array.Count > 0)
            {
                ArrayList objList = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    obj = this.GetEditObject(row);

                    if (obj != null)
                    {
                        objList.Add(obj);
                    }
                }
                object[] asnList = ((Pick[])objList.ToArray(typeof(Pick)));
                if (clickName == "cmdRelease_ServerClick")
                {
                    this.CmdReleaseObjects(asnList);
                }
                else if (clickName == "cmdInitial_ServerClick")
                {
                    CmdInitialObjects(asnList);
                }
                else if (clickName == "cmdForceClose_ServerClick")
                {
                    cmdForceCloseObjects(asnList);
                }
                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }
        #endregion

        #region 强制关单 ForceClose
        protected void cmdForceClose_ServerClick(object sender, EventArgs e)
        {
            //以应对不能完结的单据。例如，光伏需要欠料发货时，
            //  业务上是不允许的。如果遇到这种情况，则需要重新获取新的拣货任务令，此时老的拣货任务令需要关闭掉。
            GetServerClick("cmdForceClose_ServerClick");
        }
        protected void cmdForceCloseObjects(object[] pickList)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (Pick pick in pickList)
                {
                }

                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "强制关单成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }
        #endregion

        #region 保存
        protected override object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (_InventoryFacade == null)
                {
                    _InventoryFacade = new InventoryFacade(base.DataProvider);
                }
                string pickno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPickNoEdit.Text, 40));
                Pick pick = (Pick)_InventoryFacade.GetPick(pickno);
                //TBLPICK
                pick.StNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNCodeEdit.Text, 40));
                pick.PlanDate = FormatHelper.TODateInt(this.txtPlanSendDateEdit.Text);
                pick.Remark1 = FormatHelper.CleanString(this.txtMemoEdit.Text);

                return pick;
            }
            else
            {
                return null;
            }
        }

        protected override bool ValidateInput()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            string pickno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPickNoEdit.Text, 40));
            Pick pick = (Pick)_InventoryFacade.GetPick(pickno);
            if (pick == null)
            {
                WebInfoPublish.Publish(this, "拣货任务令号不存在", this.languageComponent1);
                return false;
            }
            if (!string.IsNullOrEmpty(txtASNCodeEdit.Text))
            {
                if (pick.PickType != PickType.PickType_DNZC)
                {
                    WebInfoPublish.Publish(this, "必须为DN直发才能填写入库指令号！", this.languageComponent1);
                    return false;
                }
            }

            PageCheckManager manager = new PageCheckManager();
            //manager.Add(new DateCheck(this.lblPlanSendDateEdit, this.txtPlanSendDateEdit.Text, true));
            //manager.Add(new LengthCheck(this.lblASNCodeEdit, this.txtASNCodeEdit, 200, true));


            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            this._InventoryFacade.UpdatePick((Pick)domainObject);
        }
        #endregion
        #endregion

        #region CreateSerialNo
        private string CreateSerialNo(string stno)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (_TransferFacade == null)
            {
                _TransferFacade = new WarehouseFacade(this.DataProvider);
            }
            string maxserial = _TransferFacade.GetMaxSerial("K" + stno);
            //如果已是最大值就返回为空
            if (maxserial == "999")
            {
                return "";
            }
            SERIALBOOK serialbook = new SERIALBOOK();
            if (maxserial == "")
            {
                serialbook.SNPrefix = "K" + stno;
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _TransferFacade.AddSerialBook(serialbook);
                return string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = "K" + stno;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _TransferFacade.UpdateSerialBook(serialbook);
                return string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }
        }

        #endregion

        #region 列名
        private ViewField[] viewFieldList = null;
        private ViewField[] PickHeadViewFieldList
        {
            get
            {
                if (viewFieldList == null)
                {
                    if (_InventoryFacade == null)
                    {
                        _InventoryFacade = new InventoryFacade(base.DataProvider);
                    }
                    object[] objs = _InventoryFacade.QueryViewFieldByUserCode(this.GetUserCode(), "TBLPICK");
                    if (objs != null)
                    {
                        viewFieldList = new ViewField[objs.Length];
                        objs.CopyTo(viewFieldList, 0);
                    }
                    else
                    {
                        objs = _InventoryFacade.QueryViewFieldDefault("PickHead_FIELD_LIST_SYSTEM_DEFAULT", "TBLPICK");
                        if (objs != null)
                        {
                            ArrayList list = new ArrayList();
                            for (int i = 0; i < objs.Length; i++)
                            {
                                ViewField field = (ViewField)objs[i];
                                if (FormatHelper.StringToBoolean(field.IsDefault) == true)
                                {
                                    list.Add(field);
                                }
                            }
                            viewFieldList = new ViewField[list.Count];
                            list.CopyTo(viewFieldList, 0);
                        }
                    }
                    if (viewFieldList != null)
                    {
                        bool bExistPickNo = false;
                        for (int i = 0; i < viewFieldList.Length; i++)
                        {
                            if (viewFieldList[i].FieldName == "PickNo")
                            {
                                bExistPickNo = true;
                                break;
                            }
                        }
                        if (bExistPickNo == false)
                        {
                            ViewField field = new ViewField();
                            field.FieldName = "PickNo";
                            field.Description = "拣货任务令号";
                            ArrayList list = new ArrayList();
                            list.Add(field);
                            list.AddRange(viewFieldList);
                            viewFieldList = new ViewField[list.Count];
                            list.CopyTo(viewFieldList, 0);
                        }
                    }
                }
                return viewFieldList;
            }
        }
        #endregion
    }
}
