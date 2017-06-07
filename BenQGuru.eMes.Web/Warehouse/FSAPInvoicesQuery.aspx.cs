using System;
using System.Collections;
using System.Collections.Generic;
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
using BenQGuru.eMES.Web.WarehouseWeb;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using MOType = BenQGuru.eMES.Web.Helper.MOType;

namespace BenQGuru.eMES.Web.Warehouse
{
    /// <summary>
    /// FCreatePickHeadMP 的摘要说明。
    /// </summary>
    public partial class FSAPInvoicesQuery : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        SystemSettingFacade _SystemSettingFacade = null;
        private WarehouseFacade _TransferFacade;
        private WarehouseFacade _WarehouseFacade;
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
                if (GetRequestParam("InvNo") != null)
                {
                    this.txtInvNoQuery.Text = this.GetRequestParam("InvNo");
                }
                if (GetRequestParam("DnBatchNo") != null)
                {
                    this.txtDnBatchNoQuery.Text = this.GetRequestParam("DnBatchNo");
                }
                string page = Request.QueryString["Page"];
                if (page == "FQueryStoragePickMP.aspx")
                {
                    txtInvNoQuery.Enabled = false;
                    txtDnBatchNoQuery.Enabled = false;
                }
                if (string.IsNullOrEmpty(page))
                    cmdReturn.Visible = false;
                InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
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

        #region 下拉框

        #endregion


        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            #region
            //this.gridHelper.AddColumn("SERIALNUMBER", "序号",false);
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {

                this.gridHelper.AddColumn(this.PickHeadViewFieldList[i].FieldName, /*this.languageComponent1.GetString(*/this.PickHeadViewFieldList[i].Description/*)*/, null);
                if (PickHeadViewFieldList[i].FieldName == "DQMCODE")
                {
                    this.gridHelper.AddColumn("DQMCHLONGDESC", "鼎桥物料长描述", null);
                }
            }
            this.gridHelper.AddDefaultColumn(false, false);
            ////this.gridHelper.AddLinkColumn("LinkDetail", "详细信息");
            //this.gridWebGrid.Columns.FromKey("EATTRIBUTE1").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("EATTRIBUTE2").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("EATTRIBUTE3").Hidden = true;
            //多语言
            #endregion


        }

        #region  注释
        protected DataRow GetGridRow1(object obj)
        {
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            DataRow row = this.DtSource.NewRow();
            row["INVNO"] = ((SAPInvoicesQuery)obj).INVNO;
            row["DNBatchNo"] = ((SAPInvoicesQuery)obj).DNNO;
            row["INVSTATUS"] = ((SAPInvoicesQuery)obj).INVSTATUS;
            row["INVLINE"] = ((SAPInvoicesQuery)obj).INVLINE;
            row["INVLINESTATUS"] = ((SAPInvoicesQuery)obj).INVLINESTATUS;
            row["PRNO"] = ((SAPInvoicesQuery)obj).PRNO;
            row["DQMCODE"] = ((SAPInvoicesQuery)obj).DQMCODE;

         

            row["PLANQTY"] = ((SAPInvoicesQuery)obj).PLANQTY;
            row["UNIT"] = ((SAPInvoicesQuery)obj).UNIT;
            row["CUSBATCHNO"] = ((SAPInvoicesQuery)obj).CUSBATCHNO;
            row["ORDERNO"] = ((SAPInvoicesQuery)obj).ORDERNO;
            row["CUSORDERNO"] = ((SAPInvoicesQuery)obj).CUSORDERNO;
            row["OANO"] = ((SAPInvoicesQuery)obj).OANO;
            row["REWORKAPPLYUSER"] = ((SAPInvoicesQuery)obj).REWORKAPPLYUSER;
            row["CC"] = ((SAPInvoicesQuery)obj).CC;
            row["INVENTORYNO"] = ((SAPInvoicesQuery)obj).INVENTORYNO;
            row["RECEIVERUSER"] = ((SAPInvoicesQuery)obj).RECEIVERUSER;
            //row["RECEIVERUSER"] = ((SAPInvoicesQuery)obj).RECEIVERUSER;
            row["SHIPPINGLOCATION"] = ((SAPInvoicesQuery)obj).SHIPPINGLOCATION;
            row["RECEIVERADDR"] = ((SAPInvoicesQuery)obj).RECEIVERADDR;
            //row["RECEIVERADDR"] = ((SAPInvoicesQuery)obj).RECEIVERADDR;
            row["REMARK1"] = ((SAPInvoicesQuery)obj).REMARK1;
            //row["REMARK1"] = ((SAPInvoicesQuery)obj).REMARK1;
            row["GFFLAG"] = ((SAPInvoicesQuery)obj).GFFLAG;
            row["MOVEMENTTYPE"] = ((SAPInvoicesQuery)obj).MOVEMENTTYPE;
            row["FACCODE"] = ((SAPInvoicesQuery)obj).FACCODE;
            row["FROMSTORAGECODE"] = ((SAPInvoicesQuery)obj).FROMSTORAGECODE;
            row["STORAGECODE"] = ((SAPInvoicesQuery)obj).STORAGECODE;
            row["GFHWMCODE"] = ((SAPInvoicesQuery)obj).GFHWMCODE;
            row["GFPACKINGSEQ"] = ((SAPInvoicesQuery)obj).GFPACKINGSEQ;
            row["CUSMCODE"] = ((SAPInvoicesQuery)obj).CUSMCODE;
            row["CUSITEMSPEC"] = ((SAPInvoicesQuery)obj).CUSITEMSPEC;
            row["CUSITEMDESC"] = ((SAPInvoicesQuery)obj).CUSITEMDESC;
            row["VENDERMCODE"] = ((SAPInvoicesQuery)obj).VENDERMCODE;
            row["GFHWDESC"] = ((SAPInvoicesQuery)obj).GFHWDESC;
            row["HWCODEQTY"] = ((SAPInvoicesQuery)obj).HWCODEQTY;
            row["HWCODEUNIT"] = ((SAPInvoicesQuery)obj).HWCODEUNIT;
            row["HWTYPEINFO"] = ((SAPInvoicesQuery)obj).HWTYPEINFO;
            row["CUSER"] = ((SAPInvoicesQuery)obj).CUSER;
            row["CTIME"] = ((SAPInvoicesQuery)obj).CTIME;
            row["MUSER"] = ((SAPInvoicesQuery)obj).MUSER;
            row["MTIME"] = ((SAPInvoicesQuery)obj).MTIME;
            row["HIGNLEVELITEM"] = ((SAPInvoicesQuery)obj).HIGNLEVELITEM;
            row["PACKINGWAY"] = ((SAPInvoicesQuery)obj).PACKINGWAY;
            row["PACKINGNO"] = ((SAPInvoicesQuery)obj).PACKINGNO;
            row["PACKINGSPEC"] = ((SAPInvoicesQuery)obj).PACKINGSPEC;
            row["PACKINGWAYNO"] = ((SAPInvoicesQuery)obj).PACKINGWAYNO;
            row["PLANGIDATE"] = ((SAPInvoicesQuery)obj).PLANGIDATE;
            row["NEEDDATE"] = ((SAPInvoicesQuery)obj).NEEDDATE;
            row["DEMANDARRIVALDATE"] = ((SAPInvoicesQuery)obj).DEMANDARRIVALDATE;
            row["VENDORMCODE"] = ((SAPInvoicesQuery)obj).VENDORMCODE;
            row["CUSTMCODE"] = ((SAPInvoicesQuery)obj).CUSTMCODE;
            row["RECEIVEMCODE"] = ((SAPInvoicesQuery)obj).RECEIVEMCODE;
            row["GFCONTRACTNO"] = ((SAPInvoicesQuery)obj).GFCONTRACTNO;
            row["CUSORDERNOTYPE"] = ((SAPInvoicesQuery)obj).CUSORDERNOTYPE;
            row["ORDERREASON"] = ((SAPInvoicesQuery)obj).ORDERREASON;
            row["POSTWAY"] = ((SAPInvoicesQuery)obj).POSTWAY;
            row["PICKCONDITION"] = ((SAPInvoicesQuery)obj).PICKCONDITION;
            row["DQSMCODE"] = ((SAPInvoicesQuery)obj).DQSMCODE;

            return row;
        }
        #endregion

        #region
        protected override DataRow GetGridRow(object obj)
        {
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            DataRow row = this.DtSource.NewRow();
            SAPInvoicesQuery pick = obj as SAPInvoicesQuery;
            Type type = pick.GetType();
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                ViewField field = this.PickHeadViewFieldList[i];
                string strValue = string.Empty;
                System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName.ToUpper());
                if (fieldInfo != null)
                {
                    if (field.FieldName == "DNBatchNo")
                    {
                        strValue = pick.DNNO;
                    }
                    else if (field.FieldName == "REMARK1")
                    {
                        strValue = pick.REMARK1;
                    }
                    else if (field.FieldName == "FROMSTORAGECODE")
                    {
                        strValue = pick.FROMSTORAGECODE;
                        if (pick.INVTYPE == "PRC" || pick.INVTYPE == "YFR" || pick.INVTYPE == "GZC")
                        {
                            strValue = pick.STORAGECODE;
                        }
                    }
                    else if (field.FieldName == "STORAGECODE")
                    {
                        strValue = pick.STORAGECODE;
                        if (pick.INVTYPE == "PRC" || pick.INVTYPE == "YFR" || pick.INVTYPE == "GZC")
                        {
                            strValue = "";
                        }
                    }
                  

                    #region SAP

                    //else if (field.FieldName == "CREATEUSER")
                    //{

                    //    if (!string.IsNullOrEmpty(pick.CREATEUSER))
                    //    {
                    //        strValue = pick.CREATEUSER;
                    //    }
                    //    else
                    //    {
                    //        strValue = pick.DNMUSER;
                    //    }

                    //}
                    //else if (field.FieldName == "POUPDATEDATE")
                    //{
                    //    if (pick.POUPDATEDATE != 0)
                    //    {
                    //        strValue = FormatHelper.ToDateString(pick.POUPDATEDATE);
                    //    }
                    //    else
                    //    {
                    //        strValue = FormatHelper.ToDateString(pick.DNMDATE);
                    //    }
                    //}
                    //else if (field.FieldName == "POUPDATETIME")
                    //{
                    //    if (pick.POUPDATEDATE != 0)
                    //    {
                    //        strValue = FormatHelper.ToTimeString(pick.POUPDATETIME);
                    //    }
                    //    else
                    //    {
                    //        strValue = FormatHelper.ToTimeString(pick.DNMTIME);
                    //    }
                    //}

                    #endregion

                    #region MES

                    //else if (field.FieldName == "MESCDate")
                    //{
                    //    strValue = FormatHelper.ToDateString(pick.CDate);
                    //}
                    else if (field.FieldName == "MESCTIME")
                    {
                        strValue = FormatHelper.ToTimeString(pick.CTIME);
                    }
                    else if (field.FieldName == "MESCUSER")
                    {
                        strValue = pick.CUSER;
                    }
                    else if (field.FieldName == "MESMUSER")
                    {
                        strValue = pick.MUSER;
                    }
                    //else if (field.FieldName == "MESMTIME")
                    //{
                    //    strValue = FormatHelper.ToDateString(pick.MaintainDate);
                    //}
                    else if (field.FieldName == "MESMTIME")
                    {
                        strValue = FormatHelper.ToTimeString(pick.MTIME);
                    }

                    else if (field.FieldName == "NOTOUTCHECKFLAG")
                    {
                        if (string.IsNullOrEmpty(pick.NOTOUTCHECKFLAG))
                        {
                            strValue = "否";
                        }
                        else if (pick.NOTOUTCHECKFLAG.ToUpper() == "X")
                        {
                            strValue = "是";
                        }
                        else
                        {
                            strValue = pick.NOTOUTCHECKFLAG;
                        }
                    }
                    #endregion

                    else
                    {
                        strValue = fieldInfo.GetValue(pick).ToString();
                    }
                }
               

                row[this.PickHeadViewFieldList[i].FieldName] = strValue;

                if (this.PickHeadViewFieldList[i].FieldName == "DQMCODE")
                {

                    BenQGuru.eMES.Domain.MOModel.Material m = (BenQGuru.eMES.Domain.MOModel.Material)_WarehouseFacade.GetMaterialFromDQMCode(((SAPInvoicesQuery)obj).DQMCODE);
                    if (m != null)
                        row["DQMCHLONGDESC"] = m.MchlongDesc;
                    else
                        row["DQMCHLONGDESC"] = string.Empty;
                }
            }
            return row;
        }
        #endregion


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            return this.facade.QuerySAPInvoicesQuery1(
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoQuery.Text)),
                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDnBatchNoQuery.Text)),
                    false,
                 inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            return this.facade.QuerySAPInvoicesQueryCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInvNoQuery.Text)),
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDnBatchNoQuery.Text)),
                  false
               );
        }

        #endregion

        #region Button
        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            if (commandName == "LinkDetail")
            {
                string invno = row.Items.FindItemByKey("INVNO").Text.Trim();
                Response.Redirect(this.MakeRedirectUrl("FSAPInvoicesDetailMP.aspx", new string[] { "INVNO" }, new string[] { invno }));
            }
        }

        #endregion

        #region ToolButton
        //软件出库
        protected void cmdSoftwareOut_ServerClick(object sender, EventArgs e)
        {
            if (facade == null)
            {
                facade = new InventoryFacade(base.DataProvider);
            }
            if (gridWebGrid.Rows.Count <= 0)
            {
                WebInfoPublish.Publish(this, "Gird数据不能为空", this.languageComponent1);
                return;
            }
            Dictionary<string, string> dnBatchNoList = new Dictionary<string, string>();
            try
            {
                this.DataProvider.BeginTransaction();

                #region 出库
                #region dnBatchNo
                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {
                    //string InvNo = this.gridWebGrid.Rows[i].Items.FindItemByKey("INVNO").Value.ToString();
                    //string InvLine = this.gridWebGrid.Rows[i].Items.FindItemByKey("INVLINE").Value.ToString();
                    string dnBatchNo = this.gridWebGrid.Rows[i].Items.FindItemByKey("DNBatchNo").Value.ToString();
                    string movementtype = this.gridWebGrid.Rows[i].Items.FindItemByKey("MOVEMENTTYPE").Value.ToString();

                    //判断发货批号不能为空。
                    #region check
                    if (string.IsNullOrEmpty(dnBatchNo))
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "发货批号不能为空", this.languageComponent1);
                        return;
                    }
                    if (!string.IsNullOrEmpty(movementtype))
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.PublishInfo(this, "所有物料移动类型必须全为空", this.languageComponent1);
                        return;
                    }
                    #endregion
                    if (!dnBatchNoList.ContainsKey(dnBatchNo))
                    {
                        dnBatchNoList.Add(dnBatchNo, dnBatchNo);
                    }
                    //dnBatchNoList.Add(dnBatchNo);
                }
                #endregion
                if (dnBatchNoList.Count > 1)
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.PublishInfo(this, "软件不允许多个DN批次号出库", this.languageComponent1);
                    return;
                }
                //回写当前整个发货批中的所有DN
                if (dnBatchNoList.Count > 0)
                {

                    foreach (string dnbatchNo in dnBatchNoList.Keys)
                    {
                        BenQGuru.eMES.Material.InvoicesDetailEx[] ins = facade.GetDNInVoicesDetails(dnbatchNo);
                        Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>> dnsOk = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>>();

                        if (ins.Length == 0)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.PublishInfo(this, "此单已被取消", this.languageComponent1);
                            return;
                        }
                        #region dnsOk
                        foreach (BenQGuru.eMES.Material.InvoicesDetailEx inv in ins)
                        {
                            if (string.IsNullOrEmpty(inv.MovementType))
                            {
                                #region
                                BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                                dn.DNNO = inv.InvNo;
                                dn.DNLine = inv.InvLine;
                                dn.BatchNO = dnbatchNo;
                                dn.Unit = inv.Unit;
                                if (dnsOk.ContainsKey(inv.InvNo))
                                    dnsOk[inv.InvNo].Add(dn);
                                else
                                {
                                    dnsOk[inv.InvNo] = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();
                                    dnsOk[inv.InvNo].Add(dn);
                                }
                                #endregion
                            }
                            else
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "所有物料移动类型必须全为空", this.languageComponent1);
                                return;
                            }
                        }
                        #endregion

                        #region SAP回写
                        foreach (string key in dnsOk.Keys)
                        {

                            if (dnsOk[key].Count > 0)
                            {
                                BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ok = SendDNToSap(dnsOk[key], true);
                                LogDN(dnsOk[key], ok, "Y");
                                if (ok == null)
                                {
                                    throw new SAPException(key + "SAP回写返回空:");
                                }
                                if (string.IsNullOrEmpty(ok.Result))
                                {
                                    throw new SAPException(key + "SAP回写失败 值:" + ok.Result + ":" + ok.Message);
                                }
                                if (ok.Result.ToUpper().Trim() != "S")
                                {
                                    throw new SAPException(key + "SAP回写失败 值:" + ok.Result + ok.Message);
                                }

                                BenQGuru.eMES.Common.Log.Error("---------------------" + ok.Result + ":" + ok.Message + "---------------");
                            }
                            else
                            {
                                throw new SAPException(key + "项目为空！");
                            }
                        }
                        #endregion

                    }
                }


                #endregion
                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "软件出库成功", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }

        #endregion



        private void LogDN(List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret, string isAll)
        {
            _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            foreach (BenQGuru.eMES.SAPRFCService.Domain.DN dn in dns)
            {

                DNLOG log = new DNLOG();
                log.DNLINE = dn.DNLine;
                log.DNNO = dn.DNNO;
                log.ISALL = isAll;
                if (ret == null)
                    log.RESULT = "empty";
                else
                    log.RESULT = ret.Result;


                log.MDATE = FormatHelper.TODateInt(DateTime.Now);
                log.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                log.MUSER = GetUserCode();
                log.Qty = dn.Qty;
                log.Unit = dn.Unit;
                log.MESSAGE = ret != null ? ret.Message : "null";
                if (ret != null && string.IsNullOrEmpty(ret.Message))
                {
                    log.MESSAGE = "empty";
                }
                log.DNBATCHNO = dn.BatchNO;
                _WarehouseFacade.InsertDNLOG(log);
            }
        }


        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendDNToSap(List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns, bool isALL)
        {

            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.DNToSAP d = new BenQGuru.eMES.SAPRFCService.DNToSAP(this.DataProvider);

            try
            {

                r = d.DNPGIToSAP(dns, isALL);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }



            return r;
        }

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            string[] objs = new string[this.PickHeadViewFieldList.Length];
            SAPInvoicesQuery pick = obj as SAPInvoicesQuery;
            Type type = pick.GetType();
            for (int i = 0; i < this.PickHeadViewFieldList.Length; i++)
            {
                ViewField field = this.PickHeadViewFieldList[i];
                string strValue = string.Empty;
                System.Reflection.FieldInfo fieldInfo = type.GetField(field.FieldName.ToUpper());
                if (fieldInfo != null)
                {

                    if (field.FieldName == "DNBatchNo")
                    {
                        strValue = pick.DNNO;
                    }
                    else if (field.FieldName == "REMARK1")
                    {
                        strValue = pick.REMARK1;
                    }
                    else if (field.FieldName == "FROMSTORAGECODE")
                    {
                        strValue = pick.FROMSTORAGECODE;
                        if (pick.INVTYPE == "PRC" || pick.INVTYPE == "YFR" || pick.INVTYPE == "GZC")
                        {
                            strValue = pick.STORAGECODE;
                        }
                    }
                    else if (field.FieldName == "STORAGECODE")
                    {
                        strValue = pick.STORAGECODE;
                        if (pick.INVTYPE == "PRC" || pick.INVTYPE == "YFR" || pick.INVTYPE == "GZC")
                        {
                            strValue = "";
                        }
                    }
                    #region SAP

                    //else if (field.FieldName == "CREATEUSER")
                    //{

                    //    if (!string.IsNullOrEmpty(pick.CREATEUSER))
                    //    {
                    //        strValue = pick.CREATEUSER;
                    //    }
                    //    else
                    //    {
                    //        strValue = pick.DNMUSER;
                    //    }

                    //}
                    //else if (field.FieldName == "POUPDATEDATE")
                    //{
                    //    if (pick.POUPDATEDATE != 0)
                    //    {
                    //        strValue = FormatHelper.ToDateString(pick.POUPDATEDATE);
                    //    }
                    //    else
                    //    {
                    //        strValue = FormatHelper.ToDateString(pick.DNMDATE);
                    //    }
                    //}
                    //else if (field.FieldName == "POUPDATETIME")
                    //{
                    //    if (pick.POUPDATEDATE != 0)
                    //    {
                    //        strValue = FormatHelper.ToTimeString(pick.POUPDATETIME);
                    //    }
                    //    else
                    //    {
                    //        strValue = FormatHelper.ToTimeString(pick.DNMTIME);
                    //    }
                    //}
                    else if (field.FieldName == "POCREATEDATE")
                    {
                        strValue = FormatHelper.ToDateString(pick.POCREATEDATE);
                    }
                    else if (field.FieldName == "POUPDATEDATE")
                    {
                        strValue = FormatHelper.ToDateString(pick.POUPDATEDATE);
                    }
                    else if (field.FieldName == "POUPDATETIME")
                    {
                        strValue = FormatHelper.ToTimeString(pick.POUPDATETIME);
                    }
                    #endregion

                    #region MES

                    //else if (field.FieldName == "MESCDate")
                    //{
                    //    strValue = FormatHelper.ToDateString(pick.CDate);
                    //}
                    else if (field.FieldName == "MESCTIME")
                    {
                        strValue = FormatHelper.ToTimeString(pick.CTIME);
                    }
                    else if (field.FieldName == "MESCUSER")
                    {
                        strValue = pick.CUSER;
                    }
                    else if (field.FieldName == "MESMUSER")
                    {
                        strValue = pick.MUSER;
                    }
                    //else if (field.FieldName == "MESMTIME")
                    //{
                    //    strValue = FormatHelper.ToDateString(pick.MaintainDate);
                    //}
                    else if (field.FieldName == "MESMTIME")
                    {
                        strValue = FormatHelper.ToTimeString(pick.MTIME);
                    }

                    #endregion

                    else
                    {
                        strValue = fieldInfo.GetValue(pick).ToString();
                    }
                }

                objs[i] = strValue;
            }
            return objs;
        }
        private string GetDate(int date)
        {

            return date.ToString();
        }
        protected override string[] GetColumnHeaderText()
        {
            string[] strHeader = new string[this.PickHeadViewFieldList.Length];// + 1
            //strHeader[0] = "序号";
            for (int i = 0; i < strHeader.Length; i++)
            {
                strHeader[i] = this.PickHeadViewFieldList[i].Description; //+ 1
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
                    object[] objs = facade.QueryViewFieldByUserCode(this.GetUserCode(), "TBLINVOICES");
                    if (objs != null)
                    {
                        viewFieldList = new ViewField[objs.Length];
                        objs.CopyTo(viewFieldList, 0);
                    }
                    else
                    {
                        objs = facade.QueryViewFieldDefault("INVOICES_FIELD_LIST_SYSTEM_DEFAULT", "TBLINVOICES");
                        if (objs != null)
                        {
                            ArrayList list = new ArrayList();
                            for (int i = 0; i < objs.Length; i++)
                            {
                                ViewField field = (ViewField)objs[i];
                                //if (FormatHelper.StringToBoolean(field.IsDefault) == true)
                                //{
                                list.Add(field);
                                //}
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
                            if (viewFieldList[i].FieldName == "INVNO")
                            {
                                bExistPickNo = true;
                                break;
                            }
                        }
                        if (bExistPickNo == false)
                        {
                            ViewField field = new ViewField();
                            field.FieldName = "INVNO";
                            field.Description = "SAP单据号";
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


        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            string pickNo = Request.QueryString["PickNo"];
            string page = Request.QueryString["Page"];
            Response.Redirect(this.MakeRedirectUrl(page,
                                 new string[] { "PickNo" },
                                 new string[] { pickNo
                                        
                                    }));

        }
    }

}
