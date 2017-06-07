using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using MOType = BenQGuru.eMES.Web.Helper.MOType;
using System.Collections.Generic;

namespace BenQGuru.eMES.Web.Warehouse
{
    /// <summary>
    /// FCreatePickHeadMP 的摘要说明。
    /// </summary>
    public partial class FCreatePickHeadMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        SystemSettingFacade _SystemSettingFacade = null;
        private WarehouseFacade _TransferFacade;
        private InventoryFacade facade = null;
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

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitStorageList();
                this.DrpPickTypeList();

                cmdLotSave.Visible = false;
                cmdRelease.Visible = true;
                cmdInitial.Visible = false;

                string pickNo = Request.QueryString["PickNo"];

                this.txtPickNoQuery.Text = pickNo;


                InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
            }
            txtInvNoEdit.TextBox.TextChanged += new EventHandler(txtInvNoEdit_TextChanged);
        }

        protected void txtInvNoEdit_TextChanged(object sender, EventArgs e)
        {
            #region 注释
            string[] n = txtInvNoEdit.Text.Split(',');
            if (n.Length > 1)
            {
                txtInvNoEdit.Text = n[0];
                #region 注释
                //try
                //{
                //    drpStorageOutEdit.SelectedValue = n[1];
                //}
                //catch (Exception)
                //{
                //    drpStorageOutEdit.SelectedIndex = 0;
                //}
                //if (drpStorageOutEdit.Items.Contains(drpStorageOutEdit.Items.FindByValue(n[1])))
                //{
                //    this.drpStorageOutEdit.SelectedValue = n[1];
                //}
                //else
                //{
                //    this.drpStorageOutEdit.SelectedIndex = 0;
                //} 
                #endregion
            }
            else
            {
                txtInvNoEdit.Text = "";
                drpStorageOutEdit.SelectedIndex = 0;
                return;
            }

            #endregion
            if (drpInvNoTypeEdit.SelectedValue != PickType.PickType_PRC)
            {
                return;
            }
            string invno = txtInvNoEdit.Text;
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            Invoices inv = facade.GetInvoices(invno) as Invoices;
            if (inv != null)
            {
                txtCostCenterEdit.Text = inv.Cc;
            }

            object[] invoicesDetailList = facade.GetInvoicesDetailByInvNo(invno);
            if (invoicesDetailList != null)
            {
                #region InvoicesDetail
                InvoicesDetail invoicesDetail = invoicesDetailList[0] as InvoicesDetail;
                if (invoicesDetail != null)
                {
                    if (drpStorageOutEdit.Items.Contains(drpStorageOutEdit.Items.FindByValue(invoicesDetail.StorageCode)))
                    {
                        this.drpStorageOutEdit.SelectedValue = invoicesDetail.StorageCode;
                    }
                    else
                    {
                        this.drpStorageOutEdit.SelectedIndex = 0;
                    }
                    txtReceiverAddrEdit.Text = invoicesDetail.ReceiverAddr;
                    txtReceiverUserEdit.Text = invoicesDetail.ReceiverUser;
                }
                #endregion
            }



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

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtInvNoEdit.Enabled = true;
            }
            if (pageAction == PageActionType.Update)
            {
                this.txtInvNoEdit.Enabled = false;
            }
            if (pageAction == PageActionType.Cancel)
            {
                this.txtInvNoEdit.Enabled = true;
            }
        }

        #region 下拉框
        private void InitStorageList()
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser(GetUserCode());//+TOSTORAGE
            this.drpStorageQuery.Items.Add(new ListItem("", ""));
            this.drpStorageOutEdit.Items.Add(new ListItem("", ""));
            object[] parameters = systemSettingFacade.GetDistinctParaInParameterGroup(usergroupList);
            if (parameters != null)
            {
                foreach (Domain.BaseSetting.Parameter parameter in parameters)
                {
                    drpStorageQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterCode));
                    drpStorageOutEdit.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterCode));
                }
            }
            this.drpStorageQuery.SelectedIndex = 0;
            this.drpStorageOutEdit.SelectedIndex = 0;

        }
        //单据类型下拉框
        /// <summary>
        /// 单据类型下拉框
        /// </summary>
        private void DrpPickTypeList()
        {
            #region 注释
            //if (_SystemSettingFacade == null)
            //{
            //    _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            //}
            //object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("PICKTYPE");
            //foreach (Domain.BaseSetting.Parameter parameter in parameters)
            //{
            //    this.drpPickTypeQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
            //}
            //foreach (Domain.BaseSetting.Parameter parameter in parameters)
            //{
            //    this.drpInvNoTypeEdit.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
            //}
            #endregion
            //PRC:PR领料
            this.drpPickTypeQuery.Items.Add(new ListItem("", ""));
            this.drpPickTypeQuery.Items.Add(new ListItem(GetPickTypeName(PickType.PickType_PRC), PickType.PickType_PRC));
            this.drpPickTypeQuery.Items.Add(new ListItem(GetPickTypeName(PickType.PickType_BFC), PickType.PickType_BFC));
            this.drpPickTypeQuery.SelectedIndex = 0;


            this.drpInvNoTypeEdit.Items.Add(new ListItem("", ""));
            this.drpInvNoTypeEdit.Items.Add(new ListItem(GetPickTypeName(PickType.PickType_PRC), PickType.PickType_PRC));
            this.drpInvNoTypeEdit.Items.Add(new ListItem(GetPickTypeName(PickType.PickType_BFC), PickType.PickType_BFC));
            this.drpInvNoTypeEdit.SelectedIndex = 0;
        }

        #region  单据类型下拉框
        //private void InitStorageList()
        //{
        //if (facade == null)
        //{
        //    facade = new InventoryFacade(base.DataProvider);
        //} 
        //object[] objStorage = facade.GetAllStorage();
        //this.drpStorageQuery.Items.Add(new ListItem("", ""));
        //if (objStorage != null && objStorage.Length > 0)
        //{
        //    foreach (Storage storage in objStorage)
        //    {

        //        this.drpStorageQuery.Items.Add(new ListItem(
        //             storage.StorageName, storage.StorageCode)
        //            );
        //    }
        //}
        //this.drpStorageQuery.SelectedIndex = 0;

        //this.drpStorageOutEdit.Items.Add(new ListItem("", ""));
        //if (objStorage != null && objStorage.Length > 0)
        //{
        //    foreach (Storage storage in objStorage)
        //    {

        //        this.drpStorageOutEdit.Items.Add(new ListItem(
        //             storage.StorageName, storage.StorageCode)
        //            );
        //    }
        //}
        //this.drpStorageOutEdit.SelectedIndex = 0;

        //}
        #endregion

        #endregion

        protected void drpInvNoTypeEdit_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            drpInvNoTypeChange();
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("SERIALNUMBER", "序号", false);
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                this.gridHelper.AddColumn(this.PickHeadViewFieldList[i].FieldName, /*this.languageComponent1.GetString(*/this.PickHeadViewFieldList[i].Description/*)*/, null);
            }
            this.gridHelper.AddDefaultColumn(true, true);
            this.gridHelper.AddLinkColumn("LinkDetail", "详细信息");
            this.gridWebGrid.Columns.FromKey("SERIALNUMBER").Hidden = true;



            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            if (!string.IsNullOrEmpty(this.txtPickNoQuery.Text))
            {
                this.gridHelper.RequestData();
            }
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            Pick pick = obj as Pick;
            Type type = pick.GetType();
            row[1] = pick.SerialNumber.ToString();
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

                //PlanSendDate
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
            if (chbReleaseQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Release + "',"; }
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

            #region picktype
            string picktype = this.drpPickTypeQuery.SelectedValue;
            if (string.IsNullOrEmpty(drpPickTypeQuery.SelectedValue))
            {
                picktype = "'" + PickType.PickType_PRC + "'," + "'" + PickType.PickType_BFC + "',";
            }
            #endregion
            return this.facade.QueryPickHead(
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPickNoQuery.Text)),
                picktype,
                    FormatHelper.CleanString(this.drpStorageQuery.SelectedValue),
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoQuery.Text)),
                 chk, "",
                 FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                    FormatHelper.TODateInt(this.dateInDateToQuery.Text),
                         this.chbIsExpressDelegate.Checked,
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
            if (chbReleaseQuery.Checked)
            { chk += "'" + PickHeadStatus.PickHeadStatus_Release + "',"; }
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

            #region picktype
            string picktype = this.drpPickTypeQuery.SelectedValue;
            if (string.IsNullOrEmpty(drpPickTypeQuery.SelectedValue))
            {
                picktype = "'" + PickType.PickType_PRC + "'," + "'" + PickType.PickType_BFC + "',";
            }
            #endregion

            return this.facade.QueryPickHeadCount(
             FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPickNoQuery.Text)),
                 picktype,
                FormatHelper.CleanString(this.drpStorageQuery.SelectedValue),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoQuery.Text)),
                chk, "",
                FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                FormatHelper.TODateInt(this.dateInDateToQuery.Text),
                   this.chbIsExpressDelegate.Checked,
                   GetUserCode()
               );
        }

        #endregion

        #region Button
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
                //string storageOut = row.Items.FindItemByKey("StorageOut").Text.Trim();
                Response.Redirect(this.MakeRedirectUrl("FCreatePickLineMP.aspx", new string[] { "ACT", "PickNo" }, new string[] { "LinkDetail", pickNo }));
            }
        }



        protected void cmdCreateNo_ServerClick(object sender, EventArgs e)
        {
            //产生单号按钮：点击生成拣货任务令号填入拣货任务令号输入框，规则：OU固定前缀+8位年月日+四位流水码，流水码按照日归零，例：OU201602010001
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int date = dbDateTime.DBDate;
            string serialNo = CreateSerialNo(date);
            txtPickNoEdit.Text = "OU" + date + serialNo;
        }

        #region CreateSerialNo
        private string CreateSerialNo(int stno)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (_TransferFacade == null)
            {
                _TransferFacade = new WarehouseFacade(this.DataProvider);
            }
            string maxserial = _TransferFacade.GetMaxSerial("OU" + stno);
            //如果已是最大值就返回为空
            if (maxserial == "9999")
            {
                return "";
            }
            SERIALBOOK serialbook = new SERIALBOOK();
            if (maxserial == "")
            {
                serialbook.SNPrefix = "OU" + stno;
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _TransferFacade.AddSerialBook(serialbook);
                return string.Format("{0:0000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = "OU" + stno;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _TransferFacade.UpdateSerialBook(serialbook);
                return string.Format("{0:0000}", int.Parse(serialbook.MaxSerial));
            }
        }

        #endregion
        #endregion

        #region ToolButton

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
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (Pick pick in pickList)
                {
                    if (pick.Status == PickHeadStatus.PickHeadStatus_WaitPick)
                    {
                        facade.UpdatePickStatusByPickNo(PickHeadStatus.PickHeadStatus_Release, pick.PickNo);
                        facade.UpdatePickDetailStatusByPickNo(PickHeadStatus.PickHeadStatus_Release, pick.PickNo, PickHeadStatus.PickHeadStatus_WaitPick);
                    }
                    else
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "仅针对待拣料状态的拣货任务令可取消下发", this.languageComponent1);
                        return;
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
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);
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

                    bool isUpdate = facade.QueryPickDetailCount(pick.PickNo, PickDetail_STATUS.Status_Cancel) > 0;
                    if (!isUpdate)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "需求行为空时不可下发", this.languageComponent1);
                        return;
                    }
                    if (pick.Status == PickHeadStatus.PickHeadStatus_Release)
                    {
                        if (pick.PickType == PickType.PickType_DNZC)
                        {
                            #region DNZC
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
                            foreach (PickDetail mCodeObj in pickmcodeList)
                            {
                                int asnqty = facade.GetAsnDetailSumQtyByMCode(stno, mCodeObj.MCode);//DQMCode
                                int pickqty = facade.GetPickDetailSumQtyByMCode(pick.PickNo, mCodeObj.MCode);//DQMCode
                                if (pickqty != asnqty)
                                {
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "对应 ASN号和拣货任务令的需求物料和数量是不一致", this.languageComponent1);
                                    return;
                                }
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


                            #region 注释
                            //CARTONINVOICES CartonH = new CARTONINVOICES();
                            //string date = dbTime.DBDate.ToString().Substring(2, 6);
                            //string serialNo = CreateSerialNo(date);
                            //string carinvno = "K" + date + serialNo;
                            //CartonH.CARINVNO = carinvno;
                            //CartonH.PICKNO = pick.PickNo;
                            //CartonH.STATUS = CartonInvoices_STATUS.Status_ClosePackingList;

                            //CartonH.FDATE = dbTime.DBDate;
                            //CartonH.FTIME = dbTime.DBTime;
                            //CartonH.CDATE = dbTime.DBDate;
                            //CartonH.CTIME = dbTime.DBTime;
                            //CartonH.CUSER = this.GetUserCode();
                            //CartonH.MDATE = dbTime.DBDate;
                            //CartonH.MTIME = dbTime.DBTime;
                            //CartonH.MUSER = this.GetUserCode();
                            //_WarehouseFacade.AddCartoninvoices(CartonH); 
                            #endregion

                            #region add by sam
                            object objLot = null;
                            CARTONINVOICES CartonH = _WarehouseFacade.CreateNewCartoninvoices();
                            objLot = _WarehouseFacade.GetNewLotNO("K", dbTime.DBDate.ToString().Substring(2, 6).ToString());
                            Serialbook serbook = _WarehouseFacade.CreateNewSerialbook();
                            if (objLot == null)
                            {
                                CartonH.CARINVNO = "K" + dbTime.DBDate.ToString().Substring(2, 6).ToString() + "001";
                                CartonH.PICKNO = pick.PickNo;
                                CartonH.STATUS = CartonInvoices_STATUS.Status_ClosePackingList;
                                CartonH.CDATE = dbTime.DBDate;
                                CartonH.CTIME = dbTime.DBTime;
                                CartonH.CUSER = this.GetUserCode();
                                CartonH.MDATE = dbTime.DBDate;
                                CartonH.MTIME = dbTime.DBTime;
                                CartonH.MUSER = this.GetUserCode();
                                _WarehouseFacade.AddCartoninvoices(CartonH);

                                serbook.SNprefix = "K" + dbTime.DBDate.ToString().Substring(2, 6).ToString();
                                serbook.MAXSerial = "2";
                                serbook.MUser = this.GetUserCode();
                                serbook.MDate = dbTime.DBDate;
                                serbook.MTime = dbTime.DBTime;

                                _WarehouseFacade.AddSerialbook(serbook);


                            }
                            else
                            {
                                string MAXNO = (objLot as Serialbook).MAXSerial;
                                string SNNO = (objLot as Serialbook).SNprefix;
                                CartonH.CARINVNO = SNNO + Convert.ToString(MAXNO).PadLeft(3, '0');
                                CartonH.PICKNO = pick.PickNo;
                                CartonH.STATUS = CartonInvoices_STATUS.Status_ClosePackingList;
                                CartonH.CDATE = dbTime.DBDate;
                                CartonH.CTIME = dbTime.DBTime;
                                CartonH.CUSER = this.GetUserCode();
                                CartonH.MDATE = dbTime.DBDate;
                                CartonH.MTIME = dbTime.DBTime;
                                CartonH.MUSER = this.GetUserCode();
                                _WarehouseFacade.AddCartoninvoices(CartonH);

                                //更新tblserialbook
                                serbook.SNprefix = SNNO;
                                serbook.MAXSerial = Convert.ToString((int.Parse(MAXNO) + 1));
                                serbook.MUser = this.GetUserCode();
                                serbook.MDate = dbTime.DBDate;
                                serbook.MTime = dbTime.DBTime;
                                _WarehouseFacade.UpdateSerialbook(serbook);
                            }
                            #endregion




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
                            pick.SerialNumber = Convert.ToInt64(dbTime.DBDate.ToString() + dbTime.DBTime.ToString().PadLeft(6, '0'));

                            _WarehouseFacade.UpdatePickForDNCZ(pick, GetUserCode());
                            #endregion
                        }
                        else
                        {
                            //拣货任务令头下发后为包装状态，拣货任务令行为包装完成状态
                            pick.SerialNumber = Convert.ToInt64(dbTime.DBDate.ToString() + dbTime.DBTime.ToString().PadLeft(6, '0'));

                            facade.UpdatePickStatusByPickNo(PickHeadStatus.PickHeadStatus_WaitPick, pick.PickNo, pick.SerialNumber);
                            facade.UpdatePickDetailStatusByPickNo(PickHeadStatus.PickHeadStatus_WaitPick, pick.PickNo, PickHeadStatus.PickHeadStatus_Release);
                        }

                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = string.Empty;
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = pick.InvNo;//.InvNo;
                        trans.InvType = pick.PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = dbTime.DBDate;
                        trans.MaintainTime = dbTime.DBTime;
                        trans.MaintainUser = this.GetUserCode();
                        trans.MCode = string.Empty;
                        trans.ProductionDate = 0;
                        trans.Qty = 0;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = pick.StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = pick.PickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "ISSUE";
                        _WarehouseFacade.AddInvInOutTrans(trans);

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
                Log.Error(ex.StackTrace);
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }

        }


        protected string cmdCreateBarCode()
        {

            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);

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

        #region 编辑
        protected override void SetEditObject(object obj)
        {
            Pick pick = obj as Pick;
            if (pick == null)
            {
                this.txtPickNoEdit.Text = string.Empty;
                this.drpInvNoTypeEdit.SelectedIndex = 0;
                this.txtInvNoEdit.Text = string.Empty;

                this.drpStorageOutEdit.SelectedIndex = 0;
                this.txtReceiverUserEdit.Text = string.Empty;
                this.txtReceiverAddrEdit.Text = string.Empty;

                this.txtPlanDateEdit.Text = string.Empty;
                this.txtMemoEdit.Text = string.Empty;
                txtCostCenterEdit.Text = string.Empty;
                cmdCreateNo.Disabled = false;
                return;
            }
            cmdCreateNo.Disabled = true;
            this.txtPickNoEdit.Text = pick.PickNo;
            if (drpInvNoTypeEdit.Items.Contains(drpInvNoTypeEdit.Items.FindByValue(pick.PickType)))
            {
                this.drpInvNoTypeEdit.SelectedValue = pick.PickType;
            }
            else
            {
                this.drpInvNoTypeEdit.SelectedIndex = 0;
            }
            drpInvNoTypeChange();

            this.txtInvNoEdit.Text = pick.InvNo;
            txtCostCenterEdit.Text = pick.CC;
            if (drpStorageOutEdit.Items.Contains(drpStorageOutEdit.Items.FindByValue(pick.StorageCode)))
            {
                this.drpStorageOutEdit.SelectedValue = pick.StorageCode;
            }
            else
            {
                this.drpStorageOutEdit.SelectedIndex = 0;
            }
            this.txtReceiverUserEdit.Text = pick.ReceiverUser;
            this.txtReceiverAddrEdit.Text = pick.Receiveraddr;

            this.txtPlanDateEdit.Text = FormatHelper.ToDateString(pick.PlanDate);
            this.txtMemoEdit.Text = pick.Remark1;
        }

        private void drpInvNoTypeChange()
        {
            if (this.drpInvNoTypeEdit.SelectedValue == PickType.PickType_PRC)
            {
                txtInvNoEdit.Enabled = true;
                txtCostCenterEdit.Text = "";
                txtCostCenterEdit.Enabled = false;
            }
            else if (this.drpInvNoTypeEdit.SelectedValue == PickType.PickType_BFC) // "BFC";//报废出库
            {
                txtInvNoEdit.Text = "";
                txtInvNoEdit.Enabled = false;
                txtCostCenterEdit.Enabled = true;
            }
        }

        #endregion

        #region 保存
        protected override object GetEditObject()
        {
            if (this.ValidateInput())
            {
                if (facade == null)
                {
                    facade = new InventoryFacade(base.DataProvider);
                }
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                string pickno = FormatHelper.CleanString(this.txtPickNoEdit.Text, 40);
                Pick pick = (Pick)facade.GetPick(pickno);
                if (pick == null)
                {
                    pick = new Pick();
                    pick.MaintainUser = this.GetUserCode();
                    pick.MaintainDate = dbDateTime.DBDate;
                    pick.MaintainTime = dbDateTime.DBTime;
                    pick.CUser = this.GetUserCode();
                    pick.CDate = dbDateTime.DBDate;
                    pick.CTime = dbDateTime.DBTime;
                    pick.CreatePickDate = dbDateTime.DBDate;
                    pick.CreatePickTime = dbDateTime.DBTime;
                    pick.Status = PickHeadStatus.PickHeadStatus_Release;//Release:初始化
                }
                pick.PickNo = pickno;
                pick.PickType = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpInvNoTypeEdit.SelectedValue, 40));
                pick.InvNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoEdit.Text, 40));//SAP单号
                pick.CC = FormatHelper.CleanString(txtCostCenterEdit.Text);//成本中心
                pick.GFFlag = "";
                //if (pick.PickType == PickType.PickType_BFC)//报废
                //{

                //}
                //else
                //{
                //    pick.GFFlag = "N";
                //}
                //if (!string.IsNullOrEmpty(pick.InvNo))
                //{
                //    Invoices invoices = (Invoices)facade.GetInvoices(pick.InvNo);
                //    if (invoices != null)
                //    {
                //        pick.GFFlag = invoices.GfFlag;
                //    }
                //}

                pick.StorageCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpStorageOutEdit.SelectedValue));
                pick.ReceiverUser = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReceiverUserEdit.Text));
                pick.Receiveraddr = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReceiverAddrEdit.Text));

                pick.PlanDate = FormatHelper.TODateInt(this.txtPlanDateEdit.Text);
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
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            string pickno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPickNoEdit.Text, 40));
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(this.lblPickNoEdit, this.txtPickNoEdit, 40, true));
            manager.Add(new LengthCheck(this.lblInvNoTypeEdit, this.drpInvNoTypeEdit, 40, true));

            if (drpInvNoTypeEdit.SelectedValue == PickType.PickType_PRC) //PR领料时，SAP单号必输
            {
                manager.Add(new LengthCheck(this.lblInvNoEdit, this.txtInvNoEdit, 40, true));
            }


            if (drpInvNoTypeEdit.SelectedValue == PickType.PickType_BFC)
            {
                manager.Add(new LengthCheck(this.lblCostCenterEdit, this.txtCostCenterEdit, 40, true));
            }

            manager.Add(new LengthCheck(this.lblStorageOutEdit, this.drpStorageOutEdit, 40, true));
            manager.Add(new LengthCheck(this.lblReceiverUserEdit, this.txtReceiverUserEdit, 40, false));
            manager.Add(new LengthCheck(this.lblReceiverAddrEdit, this.txtReceiverAddrEdit, 100, false));

            manager.Add(new DateCheck(this.lblPlanDateEdit, this.txtPlanDateEdit.Text, false));
            manager.Add(new LengthCheck(this.lblMemoEdit, this.txtMemoEdit, 200, false));
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int plandate = FormatHelper.TODateInt(txtPlanDateEdit.Text);
            int nowdate = dbDateTime.DBDate;
            if (plandate < nowdate)
            {
                WebInfoPublish.Publish(this, "预计发货时间应该大于等于当天", this.languageComponent1);
                return false;
            }
            return true;
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            Pick pick = (Pick)domainObject;
            if (pick.Status != PickHeadStatus.PickHeadStatus_Release)
            {
                WebInfoPublish.Publish(this, "拣货任务令已经下发不能修改", this.languageComponent1);
                return;
            }
            this.facade.UpdatePick(pick);
        }


        #endregion

        #region 新增

        protected override void AddDomainObject(object domainObject)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            this.facade.AddPick((Pick)domainObject);
        }


        #endregion

        #region 删除
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

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            var picklist = (Pick[])domainObjects.ToArray(typeof(Pick));

            foreach (Pick pick in picklist)
            {
                if (pick.Status != PickHeadStatus.PickHeadStatus_Release)
                {
                    WebInfoPublish.Publish(this, pick.PickNo + "必须是初始化！", this.languageComponent1);
                    return;
                }
                if (facade.GetPickDetailCount(pick.PickNo) > 0)
                {
                    WebInfoPublish.Publish(this, pick.PickNo + "存在拣料行信息，不能删除！", this.languageComponent1);
                    return;
                }
                if (facade.GetPickDetailMaterialCount(pick.PickNo) > 0)
                {
                    WebInfoPublish.Publish(this, pick.PickNo + "存在已拣料信息，不能删除！", this.languageComponent1);
                    return;
                }
            }


            try
            {
                this.DataProvider.BeginTransaction();
                foreach (Pick pick in picklist)
                {
                    if (pick.Status != PickHeadStatus.PickHeadStatus_Release)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "只能删除初始化的拣货任务令", this.languageComponent1);
                        return;
                    }
                    this.facade.DeletePick(pick);
                    this.facade.DeletePickDetailByPickNo(pick.PickNo);
                }
                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "删除成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }
        #endregion

        #endregion


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
                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
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
                    strValue = languageComponent1.GetString(pick.Status);
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

        private ViewField[] viewFieldList = null;
        private ViewField[] PickHeadViewFieldList
        {
            get
            {
                if (viewFieldList == null)
                {
                    if (facade == null)
                    {
                        facade = new InventoryFacade(base.DataProvider);
                    }
                    object[] objs = facade.QueryViewFieldByUserCode(this.GetUserCode(), "TBLPICK");
                    if (objs != null)
                    {
                        viewFieldList = new ViewField[objs.Length];
                        objs.CopyTo(viewFieldList, 0);
                    }
                    else
                    {
                        objs = facade.QueryViewFieldDefault("PickHead_FIELD_LIST_SYSTEM_DEFAULT", "TBLPICK");
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
    }

}
