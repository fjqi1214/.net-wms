using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using System.Collections.Generic;
using BenQGuru.eMES.Domain.Warehouse;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using NPOI.HSSF.Util;
using System.Text;
using BenQGuru.eMES.SAPRFCService.Domain;
using BenQGuru.eMES.BaseSetting;



namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FSendCaseReceipt : BaseMPageNew
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private XlsPackage xls;
        private BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = null;
        private BenQGuru.eMES.Material.InventoryFacade inventoryFacade = null;
        //private bool is2Sap = false;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        BenQGuru.eMES.BaseSetting.SystemSettingFacade _SystemSettingFacade = null;
        private Dictionary<string, string> dd = new Dictionary<string, string>();

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

        protected void Page_Init(object sender, System.EventArgs e)
        {
            PostBackTrigger tri = new PostBackTrigger();
            tri.ControlID = this.cmdAddImport.ID;
            (this.FindControl("up1") as UpdatePanel).Triggers.Add(tri);
        }

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(base.DataProvider);
            }
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("PICKTYPE");
                this.drpPickTypeQuery.Items.Add(new ListItem("", ""));
                if (parameters != null)
                {
                    foreach (Domain.BaseSetting.Parameter parameter in parameters)
                    {

                        this.drpPickTypeQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
                    }
                }
                this.drpPickTypeQuery.SelectedIndex = 0;




                string pickNo = Request.QueryString["PickNo"];
                if (!string.IsNullOrEmpty(pickNo))
                    txtPickNoQuery.Text = pickNo;

                string carinvno = Request.QueryString["CARINVNO"];
                if (!string.IsNullOrEmpty(carinvno))
                    txtCARINVNOQuery.Text = carinvno;

            }

            object[] parameters1 = _SystemSettingFacade.GetParametersByParameterGroup("CARTONINVOICES");

            if (parameters1 != null)
            {
                foreach (Domain.BaseSetting.Parameter parameter in parameters1)
                {

                    if (!dd.ContainsKey(parameter.ParameterAlias))
                        dd.Add(parameter.ParameterAlias, parameter.ParameterDescription);
                }
            }


        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }




        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("CARINVNO", "发货箱单号", null);
            this.gridHelper.AddColumn("PICKNO", "拣货任务令号", null);
            this.gridHelper.AddColumn("STATUS", "状态", null);
            this.gridHelper.AddColumn("PICKTYPE", "单据类型", null);
            this.gridHelper.AddColumn("INVNO", "SAP单据号", null);

            this.gridHelper.AddColumn("ORDERNO", "订单号", null);
            this.gridHelper.AddColumn("StorageCode123", "出库库位", null);
            this.gridHelper.AddColumn("ReceiverUser", "收货人信息", null);
            this.gridHelper.AddColumn("ReceiverAddr", "收货地址", null);
            this.gridHelper.AddColumn("PlanDate", "计划日期", null);
            this.gridHelper.AddColumn("PLANGIDATE", "计划交货日期", null);
            this.gridHelper.AddColumn("GFCONTRACTNO", "光伏合同号", null);
            this.gridHelper.AddColumn("GFFLAG", "光伏标识", null);
            this.gridHelper.AddColumn("OANO", "OA流水号", null);
            this.gridHelper.AddColumn("PackingListDate", "包装完成日期", null);
            this.gridHelper.AddColumn("PackingListTime", "包装完成时间", null);
            this.gridHelper.AddColumn("ShippingMarkDate", "唛头完成日期", null);
            this.gridHelper.AddColumn("ShippingMarkTime", "唛头完成时间", null);
            this.gridHelper.AddColumn("GROSSWEIGHT", "毛重", null);
            this.gridHelper.AddColumn("VOLUME", "体积", null);

            this.gridHelper.AddLinkColumn("LinkToCartonImport", "详细信息", null);
            this.gridHelper.AddLinkColumn("Packaging", "包装作业详情", null);

            this.gridHelper.AddDefaultColumn(true, true);


            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            if (!string.IsNullOrEmpty(txtPickNoQuery.Text))
            {

                this.gridHelper.RequestData();
            }
        }

        protected override DataRow GetGridRow(object obj)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(base.DataProvider);
            }
            DataRow row = this.DtSource.NewRow();
            PickInfo p = (PickInfo)obj;
            row["CARINVNO"] = p.CARINVNO;
            row["PICKNO"] = p.PICKNO;
            if (p.STATUS != "Close")
                row["STATUS"] = languageComponent1.GetString(p.STATUS); //this.dd.ContainsKey(p.STATUS.ToUpper()) ? this.dd[p.STATUS.ToUpper()] : "";
            else
                row["STATUS"] = "已出库";
            row["PICKTYPE"] = _SystemSettingFacade.GetParameterDescription("PICKTYPE", p.PICKTYPE);
            //GetInvInName(p.PICKTYPE);
            row["INVNO"] = p.INVNO;
            row["ORDERNO"] = p.ORDERNO;
            row["StorageCode123"] = p.StorageCode;
            row["ReceiverUser"] = p.ReceiverUser;
            row["ReceiverAddr"] = p.ReceiverAddr;
            row["PlanDate"] = FormatHelper.ToDateString(p.Plan_Date);
            row["PLANGIDATE"] = p.PLANGIDATE;
            row["GFCONTRACTNO"] = _WarehouseFacade.GetGFCONTRACTNO(p.PICKNO);
            row["GFFLAG"] = p.GFFLAG;
            row["OANO"] = p.OANO;
            row["PackingListDate"] = FormatHelper.ToDateString(p.Packing_List_Date);
            row["PackingListTime"] = FormatHelper.ToTimeString(p.Packing_List_Time);
            row["ShippingMarkDate"] = FormatHelper.ToDateString(p.Shipping_Mark_Date);
            row["ShippingMarkTime"] = FormatHelper.ToTimeString(p.Shipping_Mark_Time);
            row["GROSSWEIGHT"] = p.GROSS_WEIGHT;
            row["VOLUME"] = p.VOLUME;

            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
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
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(GetUserCode());
            string type = this.drpPickTypeQuery.SelectedValue;
            return this._WarehouseFacade.QueryPackageReceipts(
           FormatHelper.CleanString(this.txtCARINVNOQuery.Text),
           FormatHelper.CleanString(this.txtPickNoQuery.Text),
            FormatHelper.CleanString(this.txtCARTONNOQuery.Text),
           FormatHelper.CleanString(type),
           FormatHelper.CleanString(this.txtItemNameQuery.Text),
               FormatHelper.CleanString(this.txtOrderNoQuery.Text),
           FormatHelper.TODateInt(this.txtCBDateQuery.Text),
           FormatHelper.TODateInt(this.txtCEDateQuery.Text),
                     chk, usergroupList,
           inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
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

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(GetUserCode());

            return this._WarehouseFacade.QueryPackageReceiptsCount(
               FormatHelper.CleanString(this.txtCARINVNOQuery.Text),
           FormatHelper.CleanString(this.txtPickNoQuery.Text),
            FormatHelper.CleanString(this.txtCARTONNOQuery.Text),
           FormatHelper.CleanString(this.drpPickTypeQuery.SelectedValue),
           FormatHelper.CleanString(this.txtItemNameQuery.Text),
               FormatHelper.CleanString(this.txtOrderNoQuery.Text),
           FormatHelper.TODateInt(this.txtCBDateQuery.Text),
           FormatHelper.TODateInt(this.txtCEDateQuery.Text),
           chk, usergroupList
           );

        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {


            }

            if (pageAction == PageActionType.Update)
            {
                this.cmdSave.Disabled = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }


            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;
            foreach (GridRecord row in array)
            {
                #region File
                if (this.FileImport.PostedFile != null)
                {
                    try
                    {
                        HttpPostedFile postedFile = this.FileImport.PostedFile;
                        string path = Server.MapPath(this.VirtualHostRoot + "InvDoc/" + "发货箱单/");
                        DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);
                        string CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;

                        string pickNo = row.Items.FindItemByKey("PICKNO").Text;
                        BenQGuru.eMES.Domain.Warehouse.InvDoc doc = new BenQGuru.eMES.Domain.Warehouse.InvDoc();
                        doc.InvDocNo = CARINVNO;
                        doc.InvDocType = "CartonInvoices";
                        string[] part = FileUpload.FileName.Split(new char[] { '/', '\\' });

                        doc.DocType = Path.GetExtension(postedFile.FileName);
                        doc.DocName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                        doc.DocSize = FileUpload.FileBytes.Length / 1024;
                        doc.UpUser = GetUserCode();
                        doc.UpfileDate = FormatHelper.TODateInt(DateTime.Now);
                        doc.Dirname = "发货箱单";
                        doc.MaintainUser = this.GetUserCode();
                        doc.MaintainDate = dbDateTime.DBDate;
                        doc.MaintainTime = dbDateTime.DBTime;
                        doc.PickNo = pickNo;
                        doc.ServerFileName = doc.DocName + "_" + CARINVNO + DateTime.Now.ToString("yyyyMMddhhmmss") + doc.InvDocType + doc.DocType;
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileName = doc.ServerFileName;
                        this.FileImport.PostedFile.SaveAs(path + fileName);
                        inventoryFacade.AddInvDoc(doc);
                        WebInfoPublish.Publish(this, "上传成功！", this.languageComponent1);
                    }
                    catch (Exception ex)
                    {

                        WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
                    }
                }
                else
                {
                    WebInfoPublish.PublishInfo(this, "导入文件不能为空", this.languageComponent1);
                }
                #endregion

                #region File
                //if (FileUpload.HasFile)
                //{
                //    FileUpload.SaveAs(Server.MapPath("~/") + @"\upload");


                //    string CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;

                //    BenQGuru.eMES.Domain.Warehouse.InvDoc doc = new BenQGuru.eMES.Domain.Warehouse.InvDoc();
                //    doc.InvDocNo = CARINVNO;
                //    doc.InvDocType = "CartonInvoices";
                //    string[] part = FileUpload.FileName.Split(new char[] { '/', '\\' });

                //    doc.DocName = FileUpload.FileName;
                //    doc.DocSize = FileUpload.FileBytes.Length / 1024;
                //    doc.UpUser = GetUserCode();
                //    doc.UpfileDate = FormatHelper.TODateInt(DateTime.Now);

                //    doc.ServerFileName = CARINVNO + "_" + doc.InvDocType + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
                //    FileUpload.SaveAs(Server.MapPath("~/") + @"\upload\" + doc.ServerFileName);
                //    WebInfoPublish.Publish(this, "上传成功！", this.languageComponent1);
                //}
                #endregion
            }
        }

        protected void cmdExportLightSendGoodReceipt_click(object sender, EventArgs e)
        {

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (array.Count < 1)
            {
                WebInfoPublish.Publish(this, "至少选择一条记录！", this.languageComponent1); return;
            }

            List<string> ca = new List<string>();

            foreach (GridRecord row in array)
            {
                string GFFLAG = row.Items.FindItemByKey("GFFLAG").Text;

                string CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;
                if (GFFLAG != "X")
                {
                    WebInfoPublish.Publish(this, CARINVNO + "必须为光伏箱单！", this.languageComponent1); return;
                }
                ca.Add(CARINVNO);

            }

            string type = this.drpPickTypeQuery.SelectedValue;
            BenQGuru.eMES.Material.PickInfo1[] objs = this._WarehouseFacade.QueryPackageReceipts1(ca);

            Dictionary<string, List<BenQGuru.eMES.Material.PickInfo1>> dic = new Dictionary<string, List<BenQGuru.eMES.Material.PickInfo1>>();

            foreach (BenQGuru.eMES.Material.PickInfo1 p in objs)
            {
                if (dic.ContainsKey(p.CARTONNO))
                    dic[p.CARTONNO].Add(p);
                else
                {
                    dic[p.CARTONNO] = new List<BenQGuru.eMES.Material.PickInfo1>();
                    dic[p.CARTONNO].Add(p);
                }
            }

            if (!Directory.Exists(this.DownloadPhysicalPath))
            {
                Directory.CreateDirectory(this.DownloadPhysicalPath);
            }




            string filename = string.Format("Export_{0}_{1}.{2}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString(), "xls");
            string filepath = string.Format(@"{0}{1}", this.DownloadPhysicalPath, filename);



            this.xls = new XlsPackage();

            xls.CreateSheet("发货箱单");

            xls.InsertImg(this.Server.MapPath("~/dt.jpg"));
            xls.NewRow(0);
            xls.SetRowHeight(24 * 24);
            xls.Cell(new HorizontalRange(0, 5, 9), "装箱单 PACKING LIST", xls.Title, false);
            xls.NewRow(1);
            xls.SetRowHeight(19 * 19);
            ICellStyle nor = xls.NormalWithBorder;
            nor.Alignment = HorizontalAlignment.Left;
            if (objs != null && objs.Length > 0)
            {
                xls.Cell(new HorizontalRange(1, 5, 6), "使用项目名称：" + ((BenQGuru.eMES.Material.PickInfo1)(objs[0])).CUSBATCHNO, nor, false);

            }
            else
            {
                xls.Cell(new HorizontalRange(1, 5, 6), "使用项目名称：     " + " ", nor, false);
            }


            xls.Cell(new HorizontalRange(1, 7, 9), "出厂日期(Product Date)：", nor, false);
            xls.Cell(new HorizontalRange(1, 10, 11), DateTime.Now.ToString("yyyy/MM/dd"), xls.Warn, false);

            xls.NewRow(2);
            xls.SetRowHeight(19 * 19);
            string caninv = string.Empty;
            if (ca.Count > 0)
                caninv = ca[0];

            xls.Cell(new HorizontalRange(2, 5, 6), "装箱单号P/L No：" + caninv, nor, false);




            xls.Cell(new HorizontalRange(2, 7, 9), "发票号Invoice No：   ", nor, false);

            #region 合同号 add by sam
            string invnolist = "";
            foreach (BenQGuru.eMES.Material.PickInfo1 obj in objs)
            {
                invnolist += "'" + obj.INVNO + "',";
            }
            string orderno = "";
            if (!string.IsNullOrEmpty(invnolist))
            {
                invnolist = invnolist.Substring(0, invnolist.Length - 1);
                object[] list = _WarehouseFacade.GetORDERNOInInvoicesbyInvNo(invnolist);
                if (list != null)
                {
                    foreach (Invoices o in list)
                    {
                        orderno += o.OrderNo + ",";
                    }
                    orderno = orderno.Substring(0, orderno.Length - 1);
                }
            }
            #endregion

            xls.NewRow(3);
            xls.SetRowHeight(19 * 19);
            xls.Cell(new HorizontalRange(3, 5, 6), "合同号Contract No：" + orderno + " ", nor, false);
            //xls.Cell(new HorizontalRange(3, 0, 3), "合同号Contract No：" + string.Join(",", os.ToArray()), xls.Normal, false);
            //xls.Cell(new HorizontalRange(3, 7, 9), "信用证号 L/C No：" + " ", nor, false);
            xls.Cell(new HorizontalRange(3, 7, 9), "SAP单据号 SAP No：" + ((BenQGuru.eMES.Material.PickInfo1)(objs[0])).INVNO, nor, false);
            xls.NewRow(4);
            xls.SetRowHeight(19 * 19);
            //xls.Cell(new HorizontalRange(4, 5, 6), "商业合同号 InnerContract No：               ", nor, false);
            //xls.Cell(new HorizontalRange(4, 7, 9), "订单号 Order No：                   ", nor, false);

            xls.NewRow(5);
            xls.SetRowHeight(30 * 30);
            xls.Cell(0, "大箱CTN NO", xls.NormalWithBorder);
            xls.Cell(1, "材料MATERIAL", xls.NormalWithBorder);
            xls.Cell(2, "小箱CASE NO", xls.NormalWithBorder);
            xls.SetColumnWidth(2, 15);
            xls.Cell(3, "部件编码PART NUMBER", xls.NormalWithBorder);
            xls.SetColumnWidth(3, 15);
            xls.Cell(4, "DHY code", xls.NormalWithBorder);
            xls.SetColumnWidth(4, 25);
            xls.Cell(5, "型号 MODEL NUMBER", xls.NormalWithBorder);
            xls.Cell(6, "描述 DESCRIPTION", xls.NormalWithBorder);
            xls.SetColumnWidth(6, 45);
            xls.Cell(7, "数量 QTY", xls.NormalWithBorder);
            xls.Cell(8, "单位 UOM", xls.NormalWithBorder);
            xls.Cell(9, "物料条码SERIAL NO.", xls.NormalWithBorder);
            xls.SetColumnWidth(9, 19);
            xls.Cell(10, "毛重GW(KG)", xls.NormalWithBorder);
            xls.Cell(11, "净重NW(KG)", xls.NormalWithBorder);
            xls.Cell(12, "尺码SIZE(MM)", xls.NormalWithBorder);
            xls.SetColumnWidth(12, 12);
            xls.Cell(13, "体积VOLUME(CBM)", xls.NormalWithBorder);
            xls.Cell(14, "华为编码(ITEM)", xls.NormalWithBorder);
            xls.Cell(15, "华为描述(DESC)", xls.NormalWithBorder);
            xls.SetColumnWidth(15, 14);
            xls.Cell(16, "B1条码SERIAL NO.", xls.NormalWithBorder);
            xls.SetColumnWidth(15, 24);
            xls.Cell(17, "数量QTY", xls.NormalWithBorder);
            xls.Cell(18, "单位UOM", xls.NormalWithBorder);


            ICellStyle small = xls.NormalWithBorder;
            IFont font = xls.Black;
            font.FontHeightInPoints = 10;
            small.SetFont(font);
            int i = 6;
            foreach (string key in dic.Keys)
            {
                int k = 0;

                foreach (BenQGuru.eMES.Material.PickInfo1 p1 in dic[key])
                {
                    int row = i + k;
                    xls.NewRow(row);
                    ICellStyle rightBorder = xls.Normal;

                    rightBorder.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    rightBorder.RightBorderColor = HSSFColor.Black.Index;

                    xls.Cell(1, string.Empty, rightBorder);

                    //CARTONINVDETAILSN[] sns = _WarehouseFacade.QueryPackagingOperationsSN1(key, p1.CARINVNO, key);
                    object[] objes = _WarehouseFacade.QueryPackagingOperationsSN1(key);

                    List<PackagingOperations> sns = new List<PackagingOperations>();
                    if (objes != null && objes.Length > 0)
                    {

                        foreach (PackagingOperations po in objes)
                        {
                            if (po.DQMCODE == p1.DQMCODE)
                            {
                                sns.Add(po);
                            }

                        }
                    }

                    short rowHeight = (short)(19 * 19 * ((sns.Count == 0) ? 1 : sns.Count));
                    xls.SetRowHeight(rowHeight);
                    if (k == 0)
                    {
                        xls.Cell(new VerticalRegion(row, row + dic[key].Count - 1, 2), key, xls.NormalWithBorder, true);
                    }

                    xls.Cell(3, p1.VENDERITEMCODE, small);
                    xls.Cell(4, p1.DQMCODE, small);
                    xls.Cell(5, p1.HWTYPEINFO, small);
                    BenQGuru.eMES.Domain.MOModel.Material mmmm = _WarehouseFacade.GetMaterialFromDQMCode(p1.DQMCODE);
                    if (mmmm != null)
                    {
                        xls.Cell(6, mmmm.MenshortDesc, small);
                    }
                    xls.Cell(7, p1.QTY.ToString(), small);
                    xls.Cell(8, p1.UNIT, small);


                    string snStr = string.Empty;
                    foreach (PackagingOperations sn in sns)
                    {
                        snStr += sn.SN + "\r\n";
                    }

                    xls.Cell(9, snStr.TrimEnd('\r', '\n'), small);
                    xls.Cell(10, " ", small);
                    xls.Cell(11, " ", small);
                    xls.Cell(12, " ", small);
                    xls.Cell(13, " ", small);
                    xls.Cell(14, p1.GFHWITEMCODE, small);

                    string cusitemdesc = string.Empty;
                    if (p1.PICKTYPE == "DNC" || p1.PICKTYPE == "DNZC")
                        cusitemdesc = _WarehouseFacade.GetGfhwDescFromGfhwmcodeForDN(p1.GFHWITEMCODE, p1.INVNO);
                    else
                        cusitemdesc = _WarehouseFacade.GetGfhwDescFromGfhwmcodeForNotDN(p1.GFHWITEMCODE, p1.INVNO);
                    xls.Cell(15, cusitemdesc, small);
                    //xls.Cell(14, p1.GFHWITEMCODE, small);
                    //xls.Cell(15, p1.CUSITEMDESC, small);
                    xls.Cell(16, " ", small);
                    xls.Cell(17, " ", small);
                    xls.Cell(18, " ", small);
                    k++;
                }

                i = i + k;

            }




            FileStream file = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
            xls.Save(file);
            file.Close();

            DownLoadFile1(filename);

        }

        //导出装箱清单
        protected void cmdExportLoadGoodReceipt_click(object sender, EventArgs e)
        {

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;
            if (array.Count > 1)
            {
                WebInfoPublish.Publish(this, "只能选择一条记录！", this.languageComponent1); return;
            }
            foreach (GridRecord row in array)
            {
                if (!Directory.Exists(this.DownloadPhysicalPath))
                {
                    Directory.CreateDirectory(this.DownloadPhysicalPath);
                }
                string pickNo = row.Items.FindItemByKey("PICKNO").Text;
                string CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;

                List<string> pickNos = new List<string>();
                pickNos.Add(pickNo);
                PickPak[] details = _WarehouseFacade.QueryPicksForLoadCases1(CARINVNO);



                Dictionary<string, List<PickPak>> sCodeGroup = new Dictionary<string, List<PickPak>>();

                foreach (PickPak pickDetail in details)
                {
                    if (sCodeGroup.ContainsKey(pickDetail.CARTONNO))
                    {
                        sCodeGroup[pickDetail.CARTONNO].Add(pickDetail);
                    }
                    else
                    {
                        sCodeGroup[pickDetail.CARTONNO] = new List<PickPak>();
                        sCodeGroup[pickDetail.CARTONNO].Add(pickDetail);
                    }
                }

                string filename = string.Format("Export_{0}_{1}.{2}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString(), "xls");
                string filepath = string.Format(@"{0}{1}", this.DownloadPhysicalPath, filename);



                this.xls = new XlsPackage();

                int n = 1;
                foreach (string cartonno in sCodeGroup.Keys)
                {
                    xls.CreateSheet(cartonno);
                    xls.NewRow(0);
                    xls.SetRowHeight(22 * 22);
                    string sCode = string.Empty;
                    sCode = sCodeGroup[cartonno].Count > 0 ? sCodeGroup[cartonno][0].DQSITEMCODE : string.Empty;
                    xls.Cell(new HorizontalRange(0, 0, 3), "DHY：" + sCode, xls.Normal, false);
                    xls.Cell(new HorizontalRange(0, 4, 5), "(鼎桥S编码)", xls.Warn, false);
                    xls.Cell(new HorizontalRange(0, 6, 12), "装箱清单DHY： " + sCode + " PACKING LIST", xls.Normal, false);

                    xls.NewRow(1);
                    xls.SetRowHeight(24 * 24);
                    xls.Cell(0, "序号NO.", xls.NormalWithBorder);
                    xls.Cell(new HorizontalRange(1, 1, 3), "项目编码 BOM NO.", xls.Normal, true);
                    xls.Cell(new HorizontalRange(1, 4, 7), "项目描述 DESCRIPTION", xls.Normal, true);
                    xls.Cell(8, "数量QTY", xls.NormalWithBorder);
                    xls.Cell(new HorizontalRange(1, 9, 10), "单位 UOM", xls.Normal, true);
                    int i = 1;
                    foreach (PickPak p in sCodeGroup[cartonno])
                    {
                        xls.NewRow(i + 1);
                        xls.SetRowHeight(24 * 24);
                        xls.Cell(0, i.ToString(), xls.NormalWithBorder);
                        xls.Cell(new HorizontalRange(i + 1, 1, 3), p.GFHWITEMCODE, xls.Normal, true);
                        xls.Cell(new HorizontalRange(i + 1, 4, 7), p.CUSITEMDESC, xls.Normal, true);
                        xls.Cell(8, p.QTY.ToString("N2"), xls.NormalWithBorder);
                        xls.Cell(new HorizontalRange(i + 1, 9, 10), p.unit, xls.Normal, true);
                        i++;
                    }

                    n++;
                }


                FileStream file = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
                xls.Save(file);
                file.Close();

                DownLoadFile1(filename);

            }
        }


        protected void cmdExportSendGoodReceipt_click(object sender, EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (array.Count < 1)
            {
                WebInfoPublish.Publish(this, "至少选择一条记录！", this.languageComponent1); return;
            }

            List<string> ca = new List<string>();
            string pickNo = string.Empty;
            foreach (GridRecord row in array)
            {
                string GFFLAG = row.Items.FindItemByKey("GFFLAG").Text;

                string CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;
                if (string.IsNullOrEmpty(pickNo))
                    pickNo = row.Items.FindItemByKey("PICKNO").Text;
                if (!string.IsNullOrEmpty(GFFLAG) && GFFLAG != "N")
                {
                    WebInfoPublish.Publish(this, CARINVNO + "必须为非光伏箱单！", this.languageComponent1); return;
                }
                ca.Add(CARINVNO);

            }

            string type = this.drpPickTypeQuery.SelectedValue;
            BenQGuru.eMES.Material.PickInfo1[] objs = this._WarehouseFacade.QueryPackageReceipts1(ca);

            Dictionary<string, List<BenQGuru.eMES.Material.PickInfo1>> dic = new Dictionary<string, List<BenQGuru.eMES.Material.PickInfo1>>();

            foreach (BenQGuru.eMES.Material.PickInfo1 p in objs)
            {
                if (dic.ContainsKey(p.CARTONNO))
                    dic[p.CARTONNO].Add(p);
                else
                {
                    dic[p.CARTONNO] = new List<BenQGuru.eMES.Material.PickInfo1>();
                    dic[p.CARTONNO].Add(p);
                }
            }

            if (!Directory.Exists(this.DownloadPhysicalPath))
            {
                Directory.CreateDirectory(this.DownloadPhysicalPath);
            }




            string filename = string.Format("Export_{0}_{1}.{2}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString(), "xls");
            string filepath = string.Format(@"{0}{1}", this.DownloadPhysicalPath, filename);



            this.xls = new XlsPackage();

            xls.CreateSheet("发货箱单");

            xls.InsertImg(this.Server.MapPath("~/dt.jpg"));
            xls.NewRow(0);
            xls.SetRowHeight(24 * 24);

            xls.Cell(new HorizontalRange(0, 5, 9), "装箱单 PACKING LIST", xls.Title, false);
            xls.NewRow(1);
            xls.SetRowHeight(19 * 19);
            ICellStyle nor = xls.NormalWithBorder;
            nor.Alignment = HorizontalAlignment.Left;
            IFont font = xls.Black;
            font.FontHeightInPoints = 10;
            nor.SetFont(font);


            xls.Cell(new HorizontalRange(1, 5, 6), "使用项目名称：        ", nor, false);


            xls.Cell(new HorizontalRange(1, 7, 9), "出厂日期(Product Date)：", nor, false);
            xls.Cell(new HorizontalRange(1, 10, 11), DateTime.Now.ToString("yyyy/MM/dd"), xls.Warn, false);

            xls.NewRow(2);
            xls.SetRowHeight(19 * 19);
            string caninv = string.Empty;
            if (ca.Count > 0)
                caninv = ca[0];

            xls.Cell(new HorizontalRange(2, 5, 6), "装箱单号P/L No：" + caninv, nor, false);




            xls.Cell(new HorizontalRange(2, 7, 9), "发票号Invoice No：   ", nor, false);



            xls.NewRow(3);
            xls.SetRowHeight(19 * 19);
            if (objs != null && objs.Length > 0)
                xls.Cell(new HorizontalRange(3, 5, 6), "合同号Contract No： " + ((BenQGuru.eMES.Material.PickInfo1)(objs[0])).CUSBATCHNO, nor, false);
            else
                xls.Cell(new HorizontalRange(3, 5, 6), "合同号Contract No：    ", nor, false);
            //xls.Cell(new HorizontalRange(3, 0, 3), "合同号Contract No：" + string.Join(",", os.ToArray()), xls.Normal, false);
            //xls.Cell(new HorizontalRange(3, 7, 9), "信用证号 L/C No：" + " ", nor, false);

            string INVNO = objs.Length > 0 ? ((BenQGuru.eMES.Material.PickInfo1)(objs[0])).INVNO : string.Empty;

            xls.Cell(new HorizontalRange(3, 7, 9), "SAP单据号 SAP No：" + INVNO, nor, false);
            xls.NewRow(4);
            xls.SetRowHeight(19 * 19);
            //xls.Cell(new HorizontalRange(4, 5, 6), "商业合同号 InnerContract No：               ", nor, false);
            //xls.Cell(new HorizontalRange(4, 7, 9), "订单号 Order No：                   ", nor, false);

            xls.NewRow(5);
            xls.SetRowHeight(30 * 30);
            xls.Cell(0, "大箱CTN NO", nor);
            xls.Cell(1, "材料MATERIAL", nor);
            xls.Cell(2, "小箱CASE NO", nor);
            xls.SetColumnWidth(2, 15);
            xls.Cell(3, "部件编码PART NUMBER", nor);
            xls.SetColumnWidth(3, 15);
            xls.Cell(4, "DHY code", nor);
            xls.SetColumnWidth(4, 25);
            xls.Cell(5, "型号 MODEL NUMBER", nor);
            xls.Cell(6, "描述 DESCRIPTION", nor);
            xls.SetColumnWidth(6, 45);
            xls.Cell(7, "数量 QTY", nor);
            xls.Cell(8, "单位 UOM", nor);
            xls.Cell(9, "物料条码SERIAL NO.", nor);
            xls.SetColumnWidth(9, 19);
            xls.Cell(10, "毛重GW(KG)", nor);
            xls.Cell(11, "净重NW(KG)", nor);
            xls.Cell(12, "尺码SIZE(MM)", nor);
            xls.SetColumnWidth(12, 12);
            xls.Cell(13, "体积VOLUME(CBM)", nor);
            xls.Cell(14, "备注", nor);

            //xls.Cell(14, "华为编码(ITEM)", xls.NormalWithBorder);
            //xls.Cell(15, "华为描述(DESC)", xls.NormalWithBorder);
            //xls.SetColumnWidth(15, 14);
            //xls.Cell(16, "B1条码SERIAL NO.", xls.NormalWithBorder);
            //xls.SetColumnWidth(15, 24);
            //xls.Cell(17, "数量QTY", xls.NormalWithBorder);
            //xls.Cell(18, "单位UOM", xls.NormalWithBorder);


            ICellStyle small = xls.NormalWithBorder;

            small.SetFont(font);
            int i = 6;
            foreach (string key in dic.Keys)
            {
                int k = 0;

                foreach (BenQGuru.eMES.Material.PickInfo1 p1 in dic[key])
                {
                    int row = i + k;
                    xls.NewRow(row);
                    ICellStyle rightBorder = xls.Normal;

                    rightBorder.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    rightBorder.RightBorderColor = HSSFColor.Black.Index;

                    xls.Cell(1, string.Empty, rightBorder);
                    CARTONINVDETAILSN[] sns = _WarehouseFacade.QueryCARTONINVDETAILSNs(key);
                    short rowHeight = (short)(19 * 19 * ((sns.Length == 0) ? 1 : sns.Length));
                    xls.SetRowHeight(rowHeight);
                    if (k == 0)
                    {
                        xls.Cell(new VerticalRegion(row, row + dic[key].Count - 1, 2), key, xls.NormalWithBorder, true);
                    }
                    if (p1.PICKTYPE == "UB")
                        xls.Cell(3, p1.CUSTMCODE, small);
                    else
                        xls.Cell(3, p1.VENDERITEMCODE, small);
                    xls.Cell(4, p1.DQMCODE, small);
                    xls.Cell(5, p1.HWTYPEINFO, small);

                    BenQGuru.eMES.Domain.MOModel.Material mmmm = _WarehouseFacade.GetMaterialFromDQMCode(p1.DQMCODE);
                    if (mmmm != null)
                    {
                        xls.Cell(6, mmmm.MchlongDesc, small);
                    }
                    //xls.Cell(6, p1.MDESC, small);
                    xls.Cell(7, p1.QTY.ToString(), small);
                    xls.Cell(8, p1.UNIT, small);


                    string snStr = string.Empty;
                    object materobj = _WarehouseFacade.GetMaterialFromDQMCode(p1.DQMCODE);
                    Domain.MOModel.Material mater = materobj as Domain.MOModel.Material;
                    if (string.IsNullOrEmpty(mater.MCONTROLTYPE))
                    {
                        this.DataProvider.RollbackTransaction();
                        // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
                        WebInfoPublish.Publish(this, "物料：" + p1.DQMCODE + "没有维护管控类型", this.languageComponent1);
                        return;
                    }
                    if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                    {
                        foreach (CARTONINVDETAILSN sn in sns)
                        {
                            snStr += sn.SN + "\r\n";
                        }

                        xls.Cell(9, snStr.TrimEnd('\r', '\n'), small);
                    }
                    xls.Cell(10, " ", small);
                    xls.Cell(11, " ", small);
                    xls.Cell(12, " ", small);
                    xls.Cell(13, " ", small);
                    xls.Cell(14, " ", small);
                    //xls.Cell(14, p1.GFHWITEMCODE, small);
                    //xls.Cell(15, p1.CUSITEMDESC, small);
                    //xls.Cell(16, " ", small);
                    //xls.Cell(17, " ", small);
                    //xls.Cell(18, " ", small);
                    k++;
                }

                i = i + k;

            }




            FileStream file = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
            xls.Save(file);
            file.Close();

            DownLoadFile1(filename);

        }

        private void DownLoadFile1(string filename)
        {
            string strSript = @" var frameDown =$('<a></a>');
            frameDown.appendTo($('form'));
            //frameDown.attr('target','_blank');
            frameDown.html('<span></span>');
            frameDown.attr('href', '"
            + string.Format(@"{0}FDownload.aspx", this.VirtualHostRoot)
            + "?fileName=" + string.Format(@"{0}{1}", this.DownloadPath, filename)
            + @"');
            frameDown.children().click();
            frameDown.remove();";
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), Guid.NewGuid().ToString(), strSript, true);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strSript, true);
        }


        private string _downloadPath = @"\upload\";
        public string DownloadPath
        {
            get
            {
                return string.Format(@"{0}{1}/", this.PageVirtualHostRoot, _downloadPath.Trim('\\', '/').Replace('\\', '/'));
            }
        }

        public string DownloadPhysicalPath
        {
            get
            {
                return string.Format(@"{0}\{1}\", Request.PhysicalApplicationPath, _downloadPath.Trim('\\', '/').Replace('/', '\\'));
            }
        }


        protected void cmdSave_ServerClick(object sender, EventArgs e)
        {

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }



            double weight;
            double volumn;
            if (!double.TryParse(txtRoughWeight.Text, out weight))
            {
                WebInfoPublish.Publish(this, "必须是数字！", this.languageComponent1);
                return;
            }

            if (!double.TryParse(txtVol.Text, out volumn))
            {
                WebInfoPublish.Publish(this, "必须是数字！", this.languageComponent1);
                return;
            }


            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;
            foreach (GridRecord row in array)
            {
                string CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;
                string pickNo = row.Items.FindItemByKey("PICKNO").Text;
                BenQGuru.eMES.Domain.Warehouse.CARTONINVOICES c = (BenQGuru.eMES.Domain.Warehouse.CARTONINVOICES)_WarehouseFacade.GetTBLCartonInvoices(CARINVNO);
                Pick p = (Pick)_WarehouseFacade.GetPick(pickNo);
                if (c == null)
                {

                    WebInfoPublish.Publish(this, "不存在发货箱单！", this.languageComponent1); return;
                }
                if (p == null)
                {

                    WebInfoPublish.Publish(this, "拣货任务令不存在！", this.languageComponent1); return;
                }

                if (p.PickType == "POC" || p.PickType == "DNC" || p.PickType == "UB" || p.PickType == "PRC" || p.PickType == "BFC")
                {
                    if (c.STATUS != "OQCClose")
                    {
                        WebInfoPublish.Publish(this, "状态必须是OQC检验完成！", this.languageComponent1); return;
                    }
                }

                if (c.STATUS != "OQCClose" && c.STATUS != "ClosePack")
                {
                    WebInfoPublish.Publish(this, "箱单状态必须是OQC检验完成或者包装完成！", this.languageComponent1); return;
                }

                BenQGuru.eMES.Domain.Warehouse.Pick pick = (BenQGuru.eMES.Domain.Warehouse.Pick)inventoryFacade.GetPick(pickNo);
                if (pick == null)
                {
                    WebInfoPublish.Publish(this, "没有关联拣货任务！", this.languageComponent1); return;
                }

                base.DataProvider.BeginTransaction();
                try
                {
                    if (pick.Status != Pick_STATUS.Status_Close)
                    {
                        c.STATUS = "ClosePackingList";
                        pick.Status = "ClosePackingList";
                    }

                    c.GROSS_WEIGHT = Math.Round(weight, 2);
                    c.VOLUME = Math.Round(volumn, 2).ToString();
                    c.FDATE = FormatHelper.TODateInt(DateTime.Now);
                    c.FTIME = FormatHelper.TOTimeInt(DateTime.Now);
                    c.MDATE = FormatHelper.TODateInt(DateTime.Now);
                    c.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                    c.MUSER = this.GetUserCode();

                    pick.PackingListDate = FormatHelper.TODateInt(DateTime.Now);
                    pick.PackingListTime = FormatHelper.TOTimeInt(DateTime.Now);
                    pick.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    pick.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    pick.MaintainUser = this.GetUserCode();
                    _WarehouseFacade.UpdateTBLCartonInvoices(c);
                    inventoryFacade.UpdatePick(pick);

                    #region 在invinouttrans表中增加一条数据 箱单完成日期
                    WarehouseFacade facade = new WarehouseFacade(base.DataProvider);
                    DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    object[] objs = this._WarehouseFacade.GetPickLineByPickNoNotCancel(pickNo);
                    if (objs == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "当前拣货任务令号没有对应的拣货任务令明细信息", this.languageComponent1);
                        return;
                    }
                    foreach (PickDetail pickDetail in objs)
                    {
                        InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = pickDetail.DQMCode;
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = pick.InvNo;//.InvNo;
                        trans.InvType = pick.PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = dbTime1.DBDate;
                        trans.MaintainTime = dbTime1.DBTime;
                        trans.MaintainUser = this.GetUserCode();
                        trans.MCode = pickDetail.MCode;
                        trans.ProductionDate = 0;
                        trans.Qty = pickDetail.QTY;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = string.Empty;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = pickDetail.PickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "ClosePackingList";
                        facade.AddInvInOutTrans(trans);
                    }

                    #endregion

                    this.DataProvider.CommitTransaction();

                    txtVol.Text = string.Empty;
                    txtRoughWeight.Text = string.Empty;
                    this.gridHelper.RequestData();

                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }

            }
        }
        protected void cmdAchieve_ServerClick(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;
            foreach (GridRecord row in array)
            {
                string pickNo = row.Items.FindItemByKey("PICKNO").Text;
                BenQGuru.eMES.Domain.Warehouse.Pick pick = (BenQGuru.eMES.Domain.Warehouse.Pick)inventoryFacade.GetPick(pickNo);
                if (pick == null)
                {
                    WebInfoPublish.Publish(this, "没有关联拣货任务！", this.languageComponent1); return;
                }
                if (pick.GFFlag.ToUpper() != "X")
                {
                    WebInfoPublish.Publish(this, "光伏标志不正确！", this.languageComponent1); return;
                }

                BenQGuru.eMES.Domain.Warehouse.PickDetail[] ds = _WarehouseFacade.GetPickdetails(pick.PickNo);
                foreach (PickDetail ddd in ds)
                {
                    if (ddd.PQTY != ddd.SQTY)
                    {
                        WebInfoPublish.Publish(this, ddd.PickNo + "拣货任务令号未包装完成！", this.languageComponent1); return;
                    }
                }
                try
                {


                    this.DataProvider.BeginTransaction();
                    pick.ShippingMarkDate = FormatHelper.TODateInt(DateTime.Now);
                    pick.ShippingMarkTime = FormatHelper.TOTimeInt(DateTime.Now);
                    pick.ShippingMarkUser = GetUserCode();
                    _WarehouseFacade.UpdatePick(pick);

                    string CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;
                    BenQGuru.eMES.Domain.Warehouse.CARTONINVOICES c = (BenQGuru.eMES.Domain.Warehouse.CARTONINVOICES)_WarehouseFacade.GetTBLCartonInvoices(CARINVNO);
                    c.SHIPPING_MARK_DATE = FormatHelper.TODateInt(DateTime.Now);
                    c.SHIPPING_MARK_TIME = FormatHelper.TOTimeInt(DateTime.Now);
                    c.SHIPPING_MARK_USER = GetUserCode();
                    _WarehouseFacade.UpdateCartonInvoices(c);

                    this.DataProvider.CommitTransaction();
                    WebInfoPublish.Publish(this, "完成唛头成功！", this.languageComponent1);

                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }
            }
        }
        protected void cmdNoPackage_ServerClick(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;
            foreach (GridRecord row in array)
            {
                string pickNo = row.Items.FindItemByKey("PICKNO").Text;
                BenQGuru.eMES.Domain.Warehouse.Pick pick = (BenQGuru.eMES.Domain.Warehouse.Pick)inventoryFacade.GetPick(pickNo);
                if (pick == null)
                {
                    WebInfoPublish.Publish(this, "没有关联拣货任务！", this.languageComponent1); return;
                }
                if (pick.PickType != "JCC" && pick.PickType != "BLC" && pick.PickType != "WWPOC" && pick.PickType != PickType.PickType_PD)
                {
                    WebInfoPublish.Publish(this, "拣货任务令类型必须为检测返工出库 不良品出库 生产领料 盘亏！", this.languageComponent1); return;
                }


                //int sequenceNo = _WarehouseFacade.GetMaxSequence();
                //string caseNo = "CT" + DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0') + (++sequenceNo).ToString().PadLeft(9, '0');

                string CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;
                BenQGuru.eMES.Domain.Warehouse.CARTONINVOICES c = (BenQGuru.eMES.Domain.Warehouse.CARTONINVOICES)_WarehouseFacade.GetTBLCartonInvoices(CARINVNO);


                if (pick.Status != "MakePackingList")
                {
                    WebInfoPublish.Publish(this, pick.PickNo + "必须是待包装！", this.languageComponent1); return;
                }
                if (c.STATUS == "ClosePackingList")
                {
                    WebInfoPublish.Publish(this, c.CARINVNO + "箱单状态是已包装完成，不能重复包装！", this.languageComponent1); return;
                }
                if (c == null)
                {
                    WebInfoPublish.Publish(this, "不存在发货箱单！", this.languageComponent1); return;
                }
                c.STATUS = "ClosePackingList";


                try
                {
                    this.DataProvider.BeginTransaction();

                    _WarehouseFacade.UpdateTBLCartonInvoices(c);



                    Pickdetailmaterial[] mmm = _WarehouseFacade.GetPickMaterials1(pickNo);
                    foreach (Pickdetailmaterial m in mmm)
                    {
                        string caseNo = cmdCreateBarCode();
                        BenQGuru.eMES.Domain.Warehouse.CartonInvDetailMaterial c2d = new BenQGuru.eMES.Domain.Warehouse.CartonInvDetailMaterial();
                        c2d.CARINVNO = c.CARINVNO;
                        c2d.PICKNO = c.PICKNO;

                        PickDetail detail = _WarehouseFacade.GetPickDetail(pickNo, m.DqmCode);
                        c2d.GFHWITEMCODE = detail == null ? " " : detail.GFHWItemCode;
                        c2d.GFPACKINGSEQ = detail == null ? " " : detail.GFPackingSeq;
                        c2d.PICKLINE = m.Pickline;
                        c2d.MCODE = m.MCode;
                        c2d.DQMCODE = m.DqmCode;
                        c2d.QTY = m.Qty;

                        c2d.UNIT = detail.Unit;
                        c2d.CARTONNO = caseNo;
                        c2d.CDATE = FormatHelper.TODateInt(DateTime.Now);
                        c2d.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                        c2d.CUSER = GetUserCode();
                        c2d.MDATE = FormatHelper.TODateInt(DateTime.Now);
                        c2d.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                        c2d.MUSER = GetUserCode();
                        c2d.GFHWITEMCODE = detail.GFHWItemCode;
                        c2d.GFPACKINGSEQ = detail.GFPackingSeq;
                        _WarehouseFacade.AddCartonInvDetailMaterial(c2d);

                        BenQGuru.eMES.Domain.Warehouse.CartonInvDetail c2 = new BenQGuru.eMES.Domain.Warehouse.CartonInvDetail();
                        c2.CARINVNO = c.CARINVNO;
                        c2.STATUS = "ClosePack";
                        c2.PICKNO = c.PICKNO;
                        c2.CARTONNO = caseNo;
                        c2.CUSER = GetUserCode();
                        c2.CDATE = FormatHelper.TODateInt(DateTime.Now);
                        c2.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                        c2.MDATE = FormatHelper.TODateInt(DateTime.Now);
                        c2.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                        c2.MUSER = GetUserCode();
                        _WarehouseFacade.AddCartonInvDetail(c2);

                        Pickdetailmaterialsn[] sns = _WarehouseFacade.GetPickedSNFromDQMCode(m.DqmCode, pickNo);
                        foreach (Pickdetailmaterialsn sn in sns)
                        {
                            BenQGuru.eMES.Domain.Warehouse.CARTONINVDETAILSN csn = new BenQGuru.eMES.Domain.Warehouse.CARTONINVDETAILSN();

                            csn.CARINVNO = CARINVNO;
                            csn.PICKNO = sn.Pickno;
                            csn.PICKLINE = sn.Pickline;
                            csn.CARTONNO = caseNo;
                            csn.SN = sn.Sn;

                            csn.MDATE = FormatHelper.TODateInt(DateTime.Now);
                            csn.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                            csn.MUSER = GetUserCode();
                            _WarehouseFacade.AddCARTONINVDETAILSN(csn);



                        }
                    }
                    object[] objs = _WarehouseFacade.GetPickDeMaterialByPickNo(pickNo);
                    foreach (Pickdetailmaterial o in objs)
                    {
                        o.PQty = o.Qty;
                        _WarehouseFacade.UpdatePickdetailmaterial(o);
                    }


                    pick.Status = "ClosePackingList";
                    _WarehouseFacade.UpdatePick(pick);

                    PickDetail[] ps = _WarehouseFacade.GetPickDetails(pickNo);
                    foreach (PickDetail ddf in ps)
                    {
                        ddf.Status = "ClosePack";
                        ddf.PQTY = ddf.SQTY;
                        _WarehouseFacade.UpdatePickdetail(ddf);
                    }


                    InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                    trans.CartonNO = string.Empty;
                    trans.DqMCode = " ";
                    trans.FacCode = string.Empty;
                    trans.FromFacCode = string.Empty;
                    trans.FromStorageCode = string.Empty;
                    trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                    trans.InvType = (pick as Pick).PickType;
                    trans.LotNo = string.Empty;
                    trans.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    trans.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now); ;
                    trans.MaintainUser = GetUserCode();
                    trans.MCode = " ";
                    trans.ProductionDate = 0;
                    trans.Qty = 0;
                    trans.Serial = 0;
                    trans.StorageAgeDate = 0;
                    trans.StorageCode = pick.StorageCode;
                    trans.SupplierLotNo = string.Empty;
                    trans.TransNO = pickNo;
                    trans.TransType = "OUT";
                    trans.Unit = string.Empty;
                    trans.ProcessType = "ClosePack";
                    _WarehouseFacade.AddInvInOutTrans(trans);

                    this.DataProvider.CommitTransaction();
                    WebInfoPublish.Publish(this, "免包装成功！", this.languageComponent1);
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }
            }
        }

        #region Add barcode

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

        #endregion

        #region CreateSerialNo
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
        #endregion



        protected void cmdApplyIQC_ServerClick(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;
            StringBuilder sbShowMsg = new StringBuilder();
            foreach (GridRecord row in array)
            {
                string CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;
                CARTONINVOICES c1 = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoices(CARINVNO);
                if (c1.STATUS != "ClosePack")
                {
                    WebInfoPublish.Publish(this, c1.CARINVNO + "必须是包装完成才能申请OQC！", this.languageComponent1);
                    return;
                    //sbShowMsg.AppendFormat(c1.CARINVNO + "已经申请OQC,不能重复申请！");
                    //continue;
                }

                string pickNo = row.Items.FindItemByKey("PICKNO").Text;


                BenQGuru.eMES.Domain.Warehouse.Pick pick = (BenQGuru.eMES.Domain.Warehouse.Pick)inventoryFacade.GetPick(pickNo);
                if (pick == null)
                {
                    WebInfoPublish.Publish(this, "没有关联拣货任务！", this.languageComponent1);
                    return;
                }

                if (pick.PickType != "POC" && pick.PickType != "DNC" && pick.PickType != "UB" && pick.PickType != "PRC" && pick.PickType != "BFC")
                {
                    WebInfoPublish.Publish(this, "任务类型必须是Return PO DN出库 UB:调拨 PR领料 报废出库  ！", this.languageComponent1);
                    return;
                }


                BenQGuru.eMES.Domain.Warehouse.PickDetail[] ddd = _WarehouseFacade.GetPickdetails(pick.PickNo);

                foreach (BenQGuru.eMES.Domain.Warehouse.PickDetail d in ddd)
                {
                    if (d.Status != PickDetail_STATUS.Status_Cancel)
                    {
                        if (d.Status != PickDetail_STATUS.Status_ClosePack)
                        {
                            WebInfoPublish.Publish(this, pick.PickNo + "在明细表里状态不是包装完成！", this.languageComponent1);
                            return;
                        }
                    }
                }

                try
                {

                    this.DataProvider.BeginTransaction();



                    if (string.IsNullOrEmpty(pick.GFFlag) || pick.GFFlag == "N")
                    {
                        BenQGuru.eMES.Material.OQCNew[] dqmCodes = _WarehouseFacade.GetOQCDQMCodes(CARINVNO);
                        Dictionary<string, string> dqmCodeOQCNo = new Dictionary<string, string>();

                        foreach (BenQGuru.eMES.Material.OQCNew dqmCode in dqmCodes)
                        {
                            #region 非光伏
                            string perfix = "TDOQC" + DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                            BenQGuru.eMES.Domain.Warehouse.Serialbook s = (BenQGuru.eMES.Domain.Warehouse.Serialbook)_WarehouseFacade.GetSerialbook(perfix);
                            int max = 0;
                            if (s == null)
                            {
                                max = 1;
                                s = new BenQGuru.eMES.Domain.Warehouse.Serialbook();
                                s.MAXSerial = "1";
                                s.MDate = FormatHelper.TODateInt(DateTime.Now);
                                s.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                                s.MUser = GetUserCode();
                                s.SNprefix = perfix;
                                _WarehouseFacade.AddSerialbook(s);
                            }
                            else
                            {
                                max = int.Parse(s.MAXSerial);
                                max++;
                                s = (BenQGuru.eMES.Domain.Warehouse.Serialbook)_WarehouseFacade.GetSerialbook(perfix);
                                s.MAXSerial = max.ToString();
                                s.MDate = FormatHelper.TODateInt(DateTime.Now);
                                s.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                                s.MUser = GetUserCode();
                                _WarehouseFacade.UpdateSerialbook(s);

                            }

                            string oqcNum = perfix + (max).ToString().PadLeft(3, '0');
                            //BenQGuru.eMES.Domain.Warehouse.OQC oqc = new BenQGuru.eMES.Domain.Warehouse.OQC();
                            BenQGuru.eMES.Domain.OQC.OQC oqc = new BenQGuru.eMES.Domain.OQC.OQC();
                            oqc.OqcNo = oqcNum;
                            oqc.AppDate = FormatHelper.TODateInt(DateTime.Now);
                            oqc.AppTime = FormatHelper.TOTimeInt(DateTime.Now);
                            oqc.CarInvNo = CARINVNO;
                            oqc.Status = "Release";
                            oqc.PickNo = pickNo;
                            oqc.CUser = GetUserCode();
                            oqc.CDate = FormatHelper.TODateInt(DateTime.Now);
                            oqc.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                            oqc.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            oqc.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            oqc.MaintainUser = GetUserCode();
                            _WarehouseFacade.AddOQC(oqc);


                            dqmCodeOQCNo.Add(dqmCode.DQMCODE, oqcNum);

                            BenQGuru.eMES.Material.OQCNew[] oqcs = _WarehouseFacade.GetOQCDetailsForN(CARINVNO, dqmCode.DQMCODE);

                            foreach (BenQGuru.eMES.Material.OQCNew o in oqcs)
                            {
                                BenQGuru.eMES.Domain.OQC.OQCDetail d = new BenQGuru.eMES.Domain.OQC.OQCDetail();
                                d.CarInvNo = CARINVNO;
                                d.CartonNo = o.CARTONNO;
                                d.MCode = o.MCODE;
                                d.OqcNo = oqcNum;
                                d.DQMCode = o.DQMCODE;

                                d.Qty = (int)o.QTY;
                                d.Unit = o.UNIT;
                                d.GfHwItemCode = o.GFHWITEMCODE;
                                d.GfPackingSeq = o.GFPACKINGSEQ;
                                d.CUser = GetUserCode();
                                d.CDate = FormatHelper.TODateInt(DateTime.Now);
                                d.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                                d.MaintainUser = GetUserCode();
                                d.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                                d.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                                _WarehouseFacade.AddOQCDetail(d);
                            }
                            #endregion

                        }

                        OQCNew[] oqcNews = _WarehouseFacade.GetOQCDetailSNForN1(CARINVNO);
                        foreach (BenQGuru.eMES.Material.OQCNew o in oqcNews)
                        {
                            BenQGuru.eMES.Domain.OQC.OQCDetailSN dsN = new BenQGuru.eMES.Domain.OQC.OQCDetailSN();
                            dsN.CarInvNo = CARINVNO;
                            dsN.OqcNo = dqmCodeOQCNo[o.DQMCODE];
                            dsN.CartonNo = o.CARTONNO;
                            dsN.SN = o.SN;
                            dsN.DQMCode = string.IsNullOrEmpty(o.DQMCODE) ? " " : o.DQMCODE;
                            dsN.MCode = string.IsNullOrEmpty(o.MCODE) ? " " : o.MCODE;
                            dsN.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            dsN.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            dsN.MaintainUser = GetUserCode();
                            _WarehouseFacade.AddOQCDetailSN(dsN);
                        }


                    }
                    else if (pick.GFFlag.ToLower() == "x")
                    {

                        #region 光伏
                        List<string> ss = _WarehouseFacade.GetGFHWITEMCODE(CARINVNO);

                        foreach (string code in ss)
                        {
                            string perfix = "TDOQC" + DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                            BenQGuru.eMES.Domain.Warehouse.Serialbook s = (BenQGuru.eMES.Domain.Warehouse.Serialbook)_WarehouseFacade.GetSerialbook(perfix);
                            int max = 0;
                            if (s == null)
                            {
                                max = 1;
                                s = new BenQGuru.eMES.Domain.Warehouse.Serialbook();
                                s.MAXSerial = "1";
                                s.MDate = FormatHelper.TODateInt(DateTime.Now);
                                s.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                                s.MUser = GetUserCode();
                                s.SNprefix = perfix;

                                _WarehouseFacade.AddSerialbook(s);
                            }
                            else
                            {

                                max = int.Parse(s.MAXSerial);
                                max++;
                                s = (BenQGuru.eMES.Domain.Warehouse.Serialbook)_WarehouseFacade.GetSerialbook(perfix);
                                s.MAXSerial = max.ToString();
                                s.MDate = FormatHelper.TODateInt(DateTime.Now);
                                s.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                                s.MUser = GetUserCode();
                                _WarehouseFacade.UpdateSerialbook(s);

                            }
                            string oqcNum = perfix + (max).ToString().PadLeft(3, '0');
                            //BenQGuru.eMES.Domain.Warehouse.OQC oqc = new BenQGuru.eMES.Domain.Warehouse.OQC();
                            BenQGuru.eMES.Domain.OQC.OQC oqc = new Domain.OQC.OQC();
                            oqc.OqcNo = oqcNum;
                            oqc.AppDate = FormatHelper.TODateInt(DateTime.Now);
                            oqc.AppTime = FormatHelper.TOTimeInt(DateTime.Now);
                            oqc.CarInvNo = CARINVNO;
                            oqc.Status = "Release";
                            oqc.PickNo = pickNo;
                            oqc.CUser = GetUserCode();
                            oqc.CDate = FormatHelper.TODateInt(DateTime.Now);
                            oqc.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                            oqc.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            oqc.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            oqc.MaintainUser = GetUserCode();
                            _WarehouseFacade.AddOQC(oqc);

                            BenQGuru.eMES.Material.OQCNew[] oqcs = _WarehouseFacade.GetOQCDetailsForY(CARINVNO, code);
                            foreach (BenQGuru.eMES.Material.OQCNew o in oqcs)
                            {
                                BenQGuru.eMES.Domain.OQC.OQCDetail d = new BenQGuru.eMES.Domain.OQC.OQCDetail();
                                d.CarInvNo = CARINVNO;
                                d.CartonNo = o.CARTONNO;
                                d.MCode = o.MCODE;
                                d.OqcNo = oqcNum;
                                d.DQMCode = o.DQMCODE;
                                d.Qty = (int)o.QTY;
                                d.Unit = o.UNIT;
                                d.GfHwItemCode = o.GFHWITEMCODE;
                                d.GfPackingSeq = o.GFPACKINGSEQ;
                                d.CUser = GetUserCode();
                                d.CDate = FormatHelper.TODateInt(DateTime.Now);
                                d.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                                d.MaintainUser = GetUserCode();
                                d.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                                d.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                                _WarehouseFacade.AddOQCDetail(d);

                            }
                            oqcs = _WarehouseFacade.GetOQCDetailSNForY(CARINVNO, code);
                            foreach (BenQGuru.eMES.Material.OQCNew o in oqcs)
                            {
                                BenQGuru.eMES.Domain.OQC.OQCDetailSN dsN = new BenQGuru.eMES.Domain.OQC.OQCDetailSN();
                                dsN.CarInvNo = CARINVNO;
                                dsN.OqcNo = oqcNum;
                                dsN.CartonNo = o.CARTONNO;
                                dsN.SN = o.SN;
                                dsN.DQMCode = string.IsNullOrEmpty(o.DQMCODE) ? " " : o.DQMCODE;
                                dsN.MCode = string.IsNullOrEmpty(o.MCODE) ? " " : o.MCODE;
                                dsN.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                                dsN.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                                dsN.MaintainUser = GetUserCode();
                                _WarehouseFacade.AddOQCDetailSN(dsN);
                            }
                        }

                        #endregion
                    }

                    Pick p = (Pick)_WarehouseFacade.GetPick(pickNo);

                    p.Status = "OQC";
                    _WarehouseFacade.UpdatePick(p);

                    CARTONINVOICES cc = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoices(CARINVNO);
                    cc.STATUS = "OQC";
                    _WarehouseFacade.UpdateCartonInvoices(cc);

                    this.DataProvider.CommitTransaction();
                    WebInfoPublish.Publish(this, "OQC申请成功！", this.languageComponent1);
                    return;
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }

            }
        }




        #endregion

        #region Button
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            string k = row.Items.FindItemByKey("PICKNO").Text.Trim();
            string k1 = row.Items.FindItemByKey("CARINVNO").Text.Trim();

            if (commandName == "LinkToCartonImport")
            {
                Response.Redirect(this.MakeRedirectUrl("FSendCaseReceiptDetails.aspx",
                                    new string[] { "PICKNO", "CARINVNO" },
                                    new string[] { k, k1 }));
            }
            else if (commandName == "Packaging")
            {
                BenQGuru.eMES.Material.InventoryFacade inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
                Pick pick = (Pick)inventoryFacade.GetPick(k);
                if (pick == null)
                {
                    WebInfoPublish.Publish(this, "拣货任务令不存在！", this.languageComponent1);
                    return;
                }
                if (pick.GFFlag.ToUpper() == "X")
                {
                    Response.Redirect(this.MakeRedirectUrl("FGFPackagingOperations.aspx",
                                        new string[] { "PickNo", "CARINVNO", "Page" },
                                        new string[] { k, k1, "FSendCaseReceipt.aspx" }));
                }
                else if (string.IsNullOrEmpty(pick.GFFlag))
                {
                    Response.Redirect(this.MakeRedirectUrl("FPackagingOperations.aspx",
                                          new string[] { "PickNo", "CARINVNO", "Page" },
                                          new string[] { k, k1, "FSendCaseReceipt.aspx" }));

                }
                else
                    throw new Exception("光伏标志不正确！-" + pick.GFFlag);
            }

        }




        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            string k = row.Items.FindItemByKey("PICKNO").Text.Trim();
            string k1 = row.Items.FindItemByKey("CARINVNO").Text.Trim();
            if (commandName == "LinkToCartonImport")
            {

                Response.Redirect(this.MakeRedirectUrl("FSendCaseReceiptDetails.aspx",
                                    new string[] { "PICKNO", "CARINVNO" },
                                    new string[] { k, k1 }));
            }

            else if (commandName == "Packaging")
            {
                BenQGuru.eMES.Material.InventoryFacade inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
                Pick pick = (Pick)inventoryFacade.GetPick(k);
                if (pick == null)
                {
                    WebInfoPublish.Publish(this, "拣货任务令不存在！", this.languageComponent1);
                    return;
                }
                if (pick.GFFlag.ToUpper() == "X")
                {
                    Response.Redirect(this.MakeRedirectUrl("FGFPackagingOperations.aspx",
                                        new string[] { "PickNo", "CARINVNO", "Page" },
                                        new string[] { k, k1, "FSendCaseReceipt.aspx" }));
                }
                else if (string.IsNullOrEmpty(pick.GFFlag))
                {
                    Response.Redirect(this.MakeRedirectUrl("FPackagingOperations.aspx",
                                          new string[] { "PickNo", "CARINVNO", "Page" },
                                          new string[] { k, k1, "FSendCaseReceipt.aspx" }));

                }
                else
                    throw new Exception("光伏标志不正确！-" + pick.GFFlag);
            }

            else if (commandName == "Edit")
            {
                this.cmdSave.Disabled = false;
            }
        }
        protected override object GetEditObject(GridRecord row)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(GetUserCode());
            object[] objs = _WarehouseFacade.QueryPackageReceipts(row.Items.FindItemByKey("CARINVNO").Value.ToString(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty, usergroupList, 1, 50);

            if (objs != null)
            {
                return (PickInfo)objs[0];
            }
            return null;
        }

        protected void cmdSystemOutStorage_ServerClick(object sender, EventArgs e)
        {

            //系统出库
            ArrayList array = this.gridHelper.GetCheckedRows();


            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            #region add by sam
            //is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
            #endregion

            if (array.Count > 0)
            {
                List<PickInfo> picks = new List<PickInfo>();
                ArrayList objList = new ArrayList(array.Count);
                if (_WarehouseFacade == null)
                {
                    _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
                }
                if (inventoryFacade == null)
                {
                    inventoryFacade = new Material.InventoryFacade(base.DataProvider);
                }

                try
                {
                    #region check
                    foreach (GridRecord row in array)
                    {
                        //string picnNo = row.Items.FindItemByKey("PICKNO").Text;


                        //PickInfo pifo = new PickInfo();
                        //pifo.CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;
                        //pifo.PICKNO = picnNo;
                        //pifo.INVNO = row.Items.FindItemByKey("INVNO").Text;
                        //pifo.STATUS = row.Items.FindItemByKey("STATUS").Text;
                        //PickInfo pifo = obj as PickInfo;
                        string picnNo = row.Items.FindItemByKey("PICKNO").Text;

                        Pick pick11 = (Pick)_WarehouseFacade.GetPick(picnNo);
                        PickInfo pifo = new PickInfo();
                        pifo.CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;
                        pifo.PICKNO = picnNo;
                        pifo.INVNO = row.Items.FindItemByKey("INVNO").Text;
                        pifo.STATUS = pick11.Status;

                        if (!CheckDataStatus(pifo.CARINVNO, pifo.INVNO))
                        {
                            return;
                        }
                        object car_obj = _WarehouseFacade.GetCartoninvoices(pifo.CARINVNO);
                        if (car_obj == null)
                        {

                            WebInfoPublish.Publish(this, "发货箱单错误", this.languageComponent1);
                            return;
                        }
                        object[] pima_objs = _WarehouseFacade.GetPickDeMaterialByPickNo(pifo.PICKNO);
                        if (pima_objs == null)
                        {

                            WebInfoPublish.Publish(this, "拣货作业信息错误", this.languageComponent1);
                            return;
                        }
                        object obj_pi = inventoryFacade.GetPick(pifo.PICKNO);
                        if (obj_pi == null)
                        {

                            WebInfoPublish.Publish(this, "拣货任务令错误", this.languageComponent1);
                            return;
                        }
                        object[] pikd_objs = _WarehouseFacade.GetPickLineByPickNo(pifo.PICKNO);
                        if (pikd_objs == null)
                        {

                            WebInfoPublish.Publish(this, "拣货任务令行错误", this.languageComponent1);
                            return;
                        }
                        //if (pifo.STATUS == CartonInvoices_STATUS.Status_Close)
                        //{

                        //    WebInfoPublish.Publish(this, "该发货箱单已经发货", this.languageComponent1);
                        //    return;
                        //}
                        if (pifo.STATUS != PickHeadStatus.PickHeadStatus_ClosePackingList)
                        {

                            WebInfoPublish.Publish(this, "箱单未完成", this.languageComponent1);
                            return;
                        }
                        object[] pikdList = _WarehouseFacade.GetAllLineByPickNo(pifo.PICKNO,
                                                                                 PickDetail_STATUS.Status_ClosePack);
                        if (pikdList != null)
                        {

                            WebInfoPublish.Publish(this, "箱单未完成", this.languageComponent1);
                            return;
                        }
                        picks.Add(pifo);
                    }
                    #endregion
                    string message = string.Empty;
                    #region picks
                    foreach (PickInfo pifo in picks)
                    {

                        string pickNo = pifo.PICKNO;

                        if (_WarehouseFacade == null)
                        {
                            _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
                        }
                        Pick obj = (Pick)_WarehouseFacade.GetPick(pickNo);

                        if (obj == null)
                        {
                            WebInfoPublish.Publish(this, "拣货任务令号对应的拣货任务令不能为空!", this.languageComponent1);
                            return;

                        }
                        Pick pick = (Pick)obj;
                        if (pick.PickType == "BFC")
                        {
                            StockScrapToSAP(pick);

                            this.DataProvider.BeginTransaction();
                            try
                            {
                                DecreaseStorOut(dbTime, pifo);
                                this.DataProvider.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                this.DataProvider.RollbackTransaction();
                                throw new CalStorageQException(ex.Message);
                            }
                        }
                        else if (pick.PickType == "PD")
                        {
                            this.DataProvider.BeginTransaction();
                            try
                            {
                                DecreaseStorOut(dbTime, pifo);
                                this.DataProvider.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                this.DataProvider.RollbackTransaction();
                                throw new CalStorageQException(ex.Message);
                            }
                        }
                        else if (pick.PickType == "DNC" || pick.PickType == "DNZC" || pick.PickType == "UB" || pick.PickType == "JCC" || pick.PickType == "BLC" || pick.PickType == "PRC" || pick.PickType == "GZC" || pick.PickType == "WWPOC" || pick.PickType == "POC")
                        {

                            BenQGuru.eMES.Material.PickDetailDN[] ds = _WarehouseFacade.GetPickDetailsForDN(pickNo);

                            Dictionary<string, decimal> ddd = new Dictionary<string, decimal>();
                            Dictionary<string, decimal> xxx = new Dictionary<string, decimal>();//add by sam

                            foreach (BenQGuru.eMES.Material.PickDetailDN i in ds)
                            {
                                if (i.PQTY >= 0)
                                {
                                    if (ddd.ContainsKey(i.DQMCODE))
                                    {
                                        ddd[i.DQMCODE] += i.PQTY;
                                    }
                                    else
                                    {
                                        ddd.Add(i.DQMCODE, i.PQTY);
                                    }

                                    #region add by sam Support用的变量
                                    if (xxx.ContainsKey(i.DQMCODE))
                                    {
                                        xxx[i.DQMCODE] += i.PQTY;
                                    }
                                    else
                                    {
                                        xxx.Add(i.DQMCODE, i.PQTY);
                                    }
                                }
                                    #endregion
                            }

                            List<string> batchCodes = new List<string>();
                            foreach (BenQGuru.eMES.Material.PickDetailDN i in ds)
                            {
                                if (!batchCodes.Contains(i.BatchCode))
                                {
                                    batchCodes.Add(i.BatchCode);
                                }
                            }
                            foreach (string batchCode in batchCodes)
                            {
                                #region PickType
                                if (pick.PickType == "DNC" || pick.PickType == "DNZC")
                                {//DN出库
                                    BenQGuru.eMES.Material.InvoicesDetailEx[] ins = _WarehouseFacade.GetDNInVoicesDetails(batchCode);

                                    if (ins == null || ins.Length <= 0)
                                        throw new SAPException(batchCode + "发货批号对应的SAP单据号为空！");
                                    DNToSAP(ddd, batchCode, ins);


                                    this.DataProvider.BeginTransaction();
                                    try
                                    {
                                        DecreaseStorOut(dbTime, pifo);
                                        this.DataProvider.CommitTransaction();
                                    }
                                    catch (Exception ex)
                                    {
                                        this.DataProvider.RollbackTransaction();
                                        throw new CalStorageQException(ex.Message);
                                    }
                                }
                                else if (pick.PickType == "UB")//调拨出库
                                {
                                    if (!UBOutOP(dbTime, pifo, ddd, batchCode, out message))
                                    {
                                        WebInfoPublish.Publish(this, message, this.languageComponent1);
                                        return;
                                    }
                                }
                                else if (pick.PickType == "JCC" || pick.PickType == "BLC") //检测返工||不良品出库 
                                {
                                    if (!JCCAndBLCOutOP(dbTime, pifo, pickNo, ddd, batchCode, out message))
                                    {
                                        WebInfoPublish.Publish(this, message, this.languageComponent1);
                                        return;
                                    }
                                }
                                else if (pick.PickType == "PRC" || pick.PickType == "GZC")
                                {
                                    BenQGuru.eMES.Material.InvoicesDetailEx[] ins = _WarehouseFacade.GetInVoicesDetailsForNotDN(batchCode);

                                    if (ins == null || ins.Length <= 0)
                                        throw new SAPException(batchCode + "发货批号对应的SAP单据号为空！");

                                    Invoices inv = (Invoices)inventoryFacade.GetInvoices(pick.InvNo);
                                    if (inv.InvType == "GZC")
                                        RSToSAP(ddd, batchCode, ins, "241", pick.PickNo);
                                    else if (inv.InvType == "PRC")
                                        RSToSAP(ddd, batchCode, ins, "201", pick.PickNo);


                                    this.DataProvider.BeginTransaction();
                                    try
                                    {
                                        DecreaseStorOut(dbTime, pifo);
                                        this.DataProvider.CommitTransaction();
                                    }
                                    catch (Exception ex)
                                    {
                                        this.DataProvider.RollbackTransaction();
                                        throw new CalStorageQException(ex.Message);
                                    }
                                }
                                else if (pick.PickType == "WWPOC")
                                {

                                    //    BenQGuru.eMES.Material.InvoicesDetailEx[] ins = _WarehouseFacade.GetInVoicesDetailsForNotDN(batchCode);
                                    BenQGuru.eMES.Material.InvoicesDetailEx[] ins = _WarehouseFacade.GetWWpoForNotDN(pickNo);
                                    if (ins == null || ins.Length <= 0)
                                        throw new SAPException(batchCode + "发货批号对应的SAP单据号为空！");
                                    WWPoToSAP(ddd, batchCode, ins, pick.StorageCode, pickNo);


                                    this.DataProvider.BeginTransaction();
                                    try
                                    {
                                        DecreaseStorOut(dbTime, pifo);
                                        this.DataProvider.CommitTransaction();
                                    }
                                    catch (Exception ex)
                                    {
                                        this.DataProvider.RollbackTransaction();
                                        throw new CalStorageQException(ex.Message);
                                    }
                                }
                                else if (pick.PickType == "POC")
                                {
                                    BenQGuru.eMES.Material.InvoicesDetailEx[] ins = _WarehouseFacade.GetInVoicesDetailsForNotDN(batchCode);

                                    if (ins == null || ins.Length <= 0)
                                        throw new SAPException(batchCode + "发货批号对应的SAP单据号为空！");
                                    PoToSAP(ddd, batchCode, ins, pickNo);


                                    this.DataProvider.BeginTransaction();
                                    try
                                    {
                                        DecreaseStorOut(dbTime, pifo);
                                        this.DataProvider.CommitTransaction();
                                    }
                                    catch (Exception ex)
                                    {
                                        this.DataProvider.RollbackTransaction();
                                        throw new CalStorageQException(ex.Message);
                                    }
                                }
                                else
                                {
                                    throw new Exception(pick.PickType + ":拣货任务令类型不正确！");
                                }
                                #endregion
                                if (ShareLib.ShareKit.IsReBackSupport)
                                {
                                    #region add by sam webservice support
                                    BackToSupport(pickNo, pick, xxx, batchCode);
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(pick.PickType + ":拣货任务令类型不正确！");
                        }
                    }
                    #endregion

                    foreach (PickInfo pifo in picks)
                    {
                        object[] objs = this._WarehouseFacade.GetPickLineByPickNoNotCancel(pifo.PICKNO);
                        if (objs == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "当前拣货任务令号没有对应的拣货任务令明细信息", this.languageComponent1);
                            return;
                        }

                        #region 在invinouttrans表中增加一条数据

                        WarehouseFacade facade = new WarehouseFacade(base.DataProvider);

                        Pick pick = (Pick)_WarehouseFacade.GetPick(pifo.PICKNO);
                        DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                        foreach (PickDetail pickDetail in objs)
                        {
                            InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = pickDetail.DQMCode;
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = pick.InvNo; //.InvNo;
                            trans.InvType = pick.PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = dbTime1.DBDate;
                            trans.MaintainTime = dbTime1.DBTime;
                            trans.MaintainUser = this.GetUserCode();
                            trans.MCode = pickDetail.MCode;
                            trans.ProductionDate = 0;
                            trans.Qty = pickDetail.QTY;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = string.Empty;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickDetail.PickNo; // asnIqc.IqcNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "SendCase";
                            facade.AddInvInOutTrans(trans);
                        }

                        #endregion
                    }

                    WebInfoPublish.Publish(this, "出库成功！", this.languageComponent1);
                }
                catch (SAPException ex)
                {

                    WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
                }
                catch (CalStorageQException ex)
                {


                    BenQGuru.eMES.Common.Log.Error(ex.Message);
                    BenQGuru.eMES.Common.Log.Error(ex.StackTrace);
                    throw ex;

                }
                catch (Exception ex)
                {
                    BenQGuru.eMES.Common.Log.Error(ex.Message);
                    BenQGuru.eMES.Common.Log.Error(ex.StackTrace);
                    throw ex;

                }

            }
        }

        private void BackToSupport(string pickNo, Pick pick, Dictionary<string, decimal> xxx, string batchCode)
        {
            if (pick.PickType == "DNC" || pick.PickType == "DNZC")
            {
                InvoicesDetailExt[] ins = _WarehouseFacade.GetDNInVoicesDetailExt(batchCode);

                if (ins == null || ins.Length <= 0)
                    throw new SAPException(batchCode + "发货批号对应的SAP单据号为空！");
                PGIPostToXML(xxx, batchCode, ins, pickNo);
            }
        }

        private bool UBOutOP(DBDateTime dbTime, PickInfo pifo, Dictionary<string, decimal> ddd, string batchCode, out string message)
        {
            message = string.Empty;
            BenQGuru.eMES.Material.InvoicesDetailEx[] ins = _WarehouseFacade.GetInVoicesDetailsForNotDN(batchCode);

            if (ins == null || ins.Length <= 0)
            {
                message = batchCode + "发货批号对应的SAP单据号为空！";
                return false;

            }
            Dictionary<string, List<UB>> ubDataGroupInvno = GetUBToSapDomainGroupByInvNo(ddd, batchCode, ins);


            dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;

            if (!is2Sap)
            {
                if (!SendUBToSap(ubDataGroupInvno, "351", out message))
                    return false;
            }

            this.DataProvider.BeginTransaction();
            if (is2Sap)
            {
                foreach (string invNo in ubDataGroupInvno.Keys)
                    LogUB2Sap(ubDataGroupInvno[invNo], "351");
            }
            try
            {
                DecreaseStorOut(dbTime, pifo);
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw new CalStorageQException(ex.Message);
            }
            return true;
        }

        private bool JCCAndBLCOutOP(DBDateTime dbTime, PickInfo pifo, string pickNo, Dictionary<string, decimal> ddd, string batchCode, out string message)
        {
            message = string.Empty;
            BenQGuru.eMES.Material.InvoicesDetailEx[] ins = _WarehouseFacade.GetInVoicesDetailsForNotDN(batchCode);

            if (ins == null || ins.Length <= 0)
                throw new SAPException(batchCode + "发货批号对应的SAP单据号为空！");

            Dictionary<string, List<UB>> ubDataGroupInvno = GetUBToSapDomainGroupByInvNo(ddd, batchCode, ins);

            dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
            Dictionary<string, string> invToStorageCode = new Dictionary<string, string>();
            Dictionary<string, string> invToFromStorageCode = new Dictionary<string, string>();
            foreach (InvoicesDetailEx invs in ins)
            {
                if (!invToStorageCode.ContainsKey(invs.InvNo))
                    invToStorageCode.Add(invs.InvNo, invs.StorageCode);
                if (!invToFromStorageCode.ContainsKey(invs.InvNo))
                    invToFromStorageCode.Add(invs.InvNo, invs.FromStorageCode);

            }
            if (!is2Sap)
            {

                try
                {
                    if (!SendUBToSap(ubDataGroupInvno, "351", out message))
                        return false;
                }
                catch (Exception ex)
                {
                    string str = "{";
                    foreach (string key in ubDataGroupInvno.Keys)
                    {
                        str += key;
                        str += ":[";
                        List<string> strs = new List<string>();
                        foreach (UB ub in ubDataGroupInvno[key])
                        {
                            string strUB = string.Format("|{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}|",
                                ub.UBNO ?? "?? ",
                                ub.UBLine,
                                 ub.Unit ?? "??",
                                 ub.StorageCode ?? "??",
                                 ub.Qty,
                                 ub.MesTransNO ?? "??",
                                 ub.MCode ?? "??",
                                 ub.InOutFlag ?? "??",
                                 ub.FacCode ?? "??",
                                 ub.DocumentDate,
                                 ub.ContactUser ?? "??");
                            strs.Add(strUB);
                        }

                        str += string.Join(",", strs.ToArray());
                        str += " ],";

                    }
                    str += "}";
                    BenQGuru.eMES.Common.Log.Error(str);
                    throw ex;
                }

                foreach (string invno in ubDataGroupInvno.Keys)
                {
                    foreach (UB ub in ubDataGroupInvno[invno])
                        ub.StorageCode = invToStorageCode[ub.UBNO];

                }
                if (!SendUBToSap(ubDataGroupInvno, "101", out message))
                    return false;


            }

            this.DataProvider.BeginTransaction();
            if (is2Sap)
            {
                foreach (string invNo in ubDataGroupInvno.Keys)
                {

                    foreach (UB ub in ubDataGroupInvno[invNo])
                        ub.StorageCode = invToFromStorageCode[invNo];
                    LogUB2Sap(ubDataGroupInvno[invNo], "351");
                }
                foreach (string invNo in ubDataGroupInvno.Keys)
                {
                    foreach (UB ub in ubDataGroupInvno[invNo])
                        ub.StorageCode = invToStorageCode[invNo];
                    LogUB2Sap(ubDataGroupInvno[invNo], "101");
                }
            }

            try
            {
                DecreaseStorOut(dbTime, pifo);
                AddStorIn(ins, pickNo, pifo.CARINVNO);
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw new CalStorageQException(ex.Message);
            }

            return true;

        }

        private bool SendUBToSap(Dictionary<string, List<UB>> ubDataGroupInvno, string flag, out string message)
        {
            message = string.Empty;
            foreach (string invo in ubDataGroupInvno.Keys)
            {
                if (ubDataGroupInvno[invo].Count == 0)
                    continue;


                foreach (UB ub in ubDataGroupInvno[invo])
                    ub.InOutFlag = flag;


                BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = SendUBToSAP(ubDataGroupInvno[invo]);
                List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns =
                    new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();

                foreach (BenQGuru.eMES.SAPRFCService.Domain.UB ub in ubDataGroupInvno[invo])
                {
                    BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                    dn.BatchNO = ub.MesTransNO;
                    dn.DNLine = ub.UBLine;
                    dn.DNNO = ub.UBNO;
                    dn.Qty = ub.Qty;
                    dn.Unit = ub.Unit;
                    dns.Add(dn);
                }
                LogDN(dns, r, "UB");
                if (r == null)
                {
                    message = invo + "SAP回写返回空！";
                    return false;
                }
                if (string.IsNullOrEmpty(r.Result))
                {
                    message = invo + "SAP回写失败 值:" + r.Result + ":" + r.Message;
                    return false;
                }
                if (r.Result.ToUpper().Trim() != "S")
                {
                    message = invo + "SAP回写失败 值:" + r.Result + r.Message;
                    return false;
                }

                return true;
            }
            message = "发送UB单的数据字典为空！";
            return false;
        }

        private void DecreaseStorOut(DBDateTime dbTime, PickInfo pifo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            try
            {
                #region 更新拣货任务令头

                object obj_pi = inventoryFacade.GetPick(pifo.PICKNO);
                Pick pi = obj_pi as Pick;
                pi.Status = Pick_STATUS.Status_Close;
                pi.DeliveryDate = dbTime.DBDate;
                pi.DeliveryTime = dbTime.DBTime;
                pi.MaintainDate = dbTime.DBDate;
                pi.MaintainTime = dbTime.DBTime;
                pi.MaintainUser = this.GetUserCode();
                inventoryFacade.UpdatePick(pi);
                #endregion
                #region 更新拣货任务令行
                object[] pikd_objs = _WarehouseFacade.GetPickLineByPickNo(pifo.PICKNO);
                for (int i = 0; i < pikd_objs.Length; i++)
                {
                    PickDetail pikd = pikd_objs[i] as PickDetail;
                    pikd.OutQTY = pikd.PQTY;
                    pikd.MaintainDate = dbTime.DBDate;
                    pikd.MaintainTime = dbTime.DBTime;
                    pikd.MaintainUser = this.GetUserCode();
                    _WarehouseFacade.UpdatePickdetail(pikd);
                }
                #endregion
                #region 更新发货箱单头
                object car_obj = _WarehouseFacade.GetCartoninvoices(pifo.CARINVNO);
                CARTONINVOICES car = car_obj as CARTONINVOICES;
                car.STATUS = CartonInvoices_STATUS.Status_Close;
                car.MDATE = dbTime.DBDate;
                car.MTIME = dbTime.DBTime;
                car.MUSER = this.GetUserCode();
                _WarehouseFacade.UpdateCartoninvoices(car);
                #endregion
                #region  更新库存
                object[] pima_objs = _WarehouseFacade.GetPickDeMaterialByPickNo(pifo.PICKNO);
                for (int i = 0; i < pima_objs.Length; i++)
                {
                    Pickdetailmaterial pima = pima_objs[i] as Pickdetailmaterial;
                    object stod_obj = inventoryFacade.GetStorageDetail(pima.Cartonno);
                    if (stod_obj == null)
                    {
                        continue;

                    }
                    StorageDetail stod = stod_obj as StorageDetail;
                    //扣库存作业
                    //1，写log，记录当前库存
                    Storagedetaillog log = _WarehouseFacade.CreateNewStoragedetaillog();
                    log.AvailableQty = stod.AvailableQty;
                    log.Cartonno = stod.CartonNo;
                    log.DqmCode = stod.DQMCode;
                    log.FacCode = stod.FacCode;
                    log.FreezeQty = stod.FreezeQty;
                    log.LaststorageageDate = stod.LastStorageAgeDate;
                    log.LocationCode = stod.LocationCode;
                    log.Lotno = stod.Lotno;
                    log.MaintainDate = dbTime.DBDate;
                    log.MaintainTime = dbTime.DBTime;
                    log.MaintainUser = this.GetUserCode();
                    log.MCode = stod.MCode;
                    log.MDesc = stod.MDesc;
                    log.Production_Date = stod.ProductionDate;
                    log.Qty = pima.PQty;
                    log.ReworkapplyUser = stod.ReworkApplyUser;
                    log.Serial = 0;
                    log.Serialno = pifo.PICKNO;
                    log.StorageageDate = stod.StorageAgeDate;
                    log.StorageCode = stod.StorageCode;
                    log.StorageQty = stod.StorageQty;
                    log.Supplier_lotno = stod.SupplierLotNo;
                    log.Type = "OUT";
                    log.Unit = stod.Unit;
                    // log.ValidStartDate=stod.val
                    _WarehouseFacade.AddStoragedetaillog(log);
                    //2，减库存
                    if ((decimal)stod.FreezeQty == pima.PQty)
                    {
                        if (stod.AvailableQty == 0)
                        {
                            inventoryFacade.DeleteStorageDetail(stod);
                        }
                        else
                        {
                            stod.StorageQty = stod.AvailableQty;
                            stod.FreezeQty = 0;
                            stod.MaintainDate = dbTime.DBDate;
                            stod.MaintainTime = dbTime.DBTime;
                            stod.MaintainUser = this.GetUserCode();
                            inventoryFacade.UpdateStorageDetail(stod);
                        }
                    }
                    else
                    {
                        stod.StorageQty = stod.StorageQty - (int)pima.PQty;
                        stod.FreezeQty = stod.FreezeQty - (int)pima.PQty;
                        stod.MaintainDate = dbTime.DBDate;
                        stod.MaintainTime = dbTime.DBTime;
                        stod.MaintainUser = this.GetUserCode();
                        inventoryFacade.UpdateStorageDetail(stod);
                    }
                    //3,删除对应的SN
                    object[] pimsn_objs = _WarehouseFacade.GetPickDeMaterialSNByPickNoAndCartonNo(pifo.PICKNO, pima.Cartonno);
                    if (pimsn_objs != null)
                    {
                        for (int j = 0; j < pimsn_objs.Length; j++)
                        {
                            Pickdetailmaterialsn pimsn = pimsn_objs[j] as Pickdetailmaterialsn;
                            object obj_sn = inventoryFacade.GetStorageDetailSN(pimsn.Sn);
                            if (obj_sn != null)
                            {
                                StorageDetailSN stosn = obj_sn as StorageDetailSN;
                                inventoryFacade.DeleteStorageDetailSN(stosn);
                            }
                        }
                    }
                    //4,写trans
                    InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                    trans.CartonNO = pima.Cartonno;
                    trans.DqMCode = pima.DqmCode;
                    trans.FacCode = string.Empty;
                    trans.FromFacCode = string.Empty;
                    trans.FromStorageCode = stod.StorageCode;
                    trans.InvNO = pi.InvNo;
                    trans.InvType = pi.PickType;
                    trans.LotNo = stod.Lotno;
                    trans.MaintainDate = dbTime.DBDate;
                    trans.MaintainTime = dbTime.DBTime;
                    trans.MaintainUser = this.GetUserCode();
                    trans.MCode = pima.MCode;
                    trans.ProductionDate = stod.ProductionDate;
                    trans.Qty = pima.PQty;
                    trans.Serial = 0;
                    trans.StorageAgeDate = stod.StorageAgeDate;
                    trans.StorageCode = stod.StorageCode;
                    trans.SupplierLotNo = stod.SupplierLotNo;
                    trans.TransNO = pi.PickNo;
                    trans.TransType = "OUT";
                    trans.ProcessType = "DecreaseStorOut";
                    trans.Unit = stod.Unit;
                    _WarehouseFacade.AddInvInOutTrans(trans);
                }
                #endregion
                #region  更新invoicesdetail出库数量
                object[] invd_objs = _WarehouseFacade.GetInvoicesDetailByInvNo(pi.InvNo);
                if (invd_objs != null)
                {
                    //this.DataProvider.RollbackTransaction();
                    //WebInfoPublish.Publish(this, "SAP单据错误", this.languageComponent1);
                    //return;

                    for (int t = 0; t < invd_objs.Length; t++)
                    {
                        InvoicesDetail invd = invd_objs[t] as InvoicesDetail;
                        object qty_obj = _WarehouseFacade.GetQtyByPickNoAndDQMCode(pi.PickNo, invd.DQMCode);
                        PickDetail pidqty = qty_obj as PickDetail;
                        if (pidqty.OutQTY > 0)
                        {
                            invd.OutQty = pidqty.OutQTY;
                            invd.MaintainDate = dbTime.DBDate;
                            invd.MaintainTime = dbTime.DBTime;
                            invd.MaintainUser = this.GetUserCode();
                            inventoryFacade.UpdateInvoicesDetail(invd);
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                throw new CalStorageQException(ex.Message);

            }
        }

        private void AddStorIn(BenQGuru.eMES.Material.InvoicesDetailEx[] ins, string pickNo, string CARINVNO)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            try
            {

                CARTONINVDETAILSN[] sns = _WarehouseFacade.GetCartonInvDetailSn(pickNo);
                Pick pick = (Pick)_WarehouseFacade.GetPick(pickNo);
                foreach (CARTONINVDETAILSN sn in sns)
                {
                    StorageDetailSN sdSN = new StorageDetailSN();



                    sdSN.CartonNo = (pick.PickType == "BLC" || pick.PickType == "JCC") ? sn.CARTONNO + "*" : sn.CARTONNO;
                    sdSN.SN = sn.SN;
                    sdSN.PickBlock = "N";
                    sdSN.CDate = FormatHelper.TODateInt(DateTime.Now);
                    sdSN.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                    sdSN.CUser = GetUserCode();
                    sdSN.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    sdSN.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    sdSN.MaintainUser = GetUserCode();
                    _WarehouseFacade.InsertStorageDetailSN(sdSN);
                }

                foreach (BenQGuru.eMES.Material.InvoicesDetailEx ex in ins)
                {

                    CartonInvDetailMaterial[] ms = _WarehouseFacade.GetCartonInvDetailMaterial(pickNo, ex.DQMCode, CARINVNO);
                    foreach (CartonInvDetailMaterial m1 in ms)
                    {

                        StorageDetail sd = _WarehouseFacade.GetStorageDetailsFromCARTONNOAndStorageCode(m1.CARTONNO, ex.StorageCode);

                        if (sd != null)
                        {
                            sd.StorageQty += (int)m1.QTY;
                            sd.AvailableQty += (int)m1.QTY;
                            _WarehouseFacade.UpdateStorageDetail(sd);
                        }
                        else
                        {

                            StorageDetail stor = new StorageDetail();
                            stor.CartonNo = (pick.PickType == "BLC" || pick.PickType == "JCC") ? m1.CARTONNO + "*" : m1.CARTONNO;
                            stor.StorageQty = (int)m1.QTY;
                            stor.AvailableQty = (int)m1.QTY;
                            stor.DQMCode = m1.DQMCODE;
                            stor.FacCode = ex.FacCode;
                            stor.FreezeQty = 0;
                            Invoices inv = (Invoices)inventoryFacade.GetInvoices(ex.InvNo);
                            if (inv != null)
                                stor.ReworkApplyUser = inv.ReworkApplyUser;

                            stor.MCode = ex.InvNo;
                            stor.CDate = FormatHelper.TODateInt(DateTime.Now);
                            stor.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                            stor.CUser = GetUserCode();
                            stor.LastStorageAgeDate = FormatHelper.TODateInt(DateTime.Now);
                            stor.LocationCode = _WarehouseFacade.GetLocationCode(ex.StorageCode);
                            Pickdetailmaterial p = _WarehouseFacade.GetLotNoAndSupplierLotNO(pickNo);
                            stor.Lotno = p.Lotno;
                            stor.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            stor.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            stor.MaintainUser = GetUserCode();
                            stor.MCode = ex.MCode;
                            BenQGuru.eMES.Domain.MOModel.Material m = _WarehouseFacade.GetMaterial(ex.MCode);
                            stor.MDesc = m.MchshortDesc;
                            stor.ProductionDate = FormatHelper.TOTimeInt(DateTime.Now);
                            stor.SupplierLotNo = p.Supplier_lotno;
                            stor.Unit = ex.Unit;
                            stor.StorageCode = ex.StorageCode;

                            stor.StorageAgeDate = _WarehouseFacade.GetPickDetailMarterialStorageAgeDate(m1.PICKNO, m1.DQMCODE);
                            _WarehouseFacade.InsertStorageDetail(stor);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw new CalStorageQException(ex.Message);
            }

        }
        #region XML
        //        private void REVToXML(Dictionary<string, decimal> ddd, string batchCode, InvoicesDetailExt[] ins, string pickNo)
        //        {
        //            if (_WarehouseFacade == null)
        //            {
        //                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
        //            }
        //            if (inventoryFacade == null)
        //            {
        //                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
        //            }

        //            foreach (InvoicesDetailExt inv in ins)
        //            {
        //                string LFIMG = "";
        //                if (ddd.ContainsKey(inv.DQMCode))
        //                {
        //                    decimal remainCount = ddd[inv.DQMCode];
        //                    remainCount -= inv.PlanQty;
        //                    if (remainCount >= 0)
        //                    {
        //                        //po.Qty = inv.PlanQty;
        //                        LFIMG = inv.PlanQty.ToString();
        //                        ddd[inv.DQMCode] -= inv.PlanQty;
        //                    }
        //                    else
        //                    {
        //                        //po.Qty = ddd[inv.DQMCode];
        //                        LFIMG = ddd[inv.DQMCode].ToString();
        //                        ddd[inv.DQMCode] = 0;
        //                    }
        //                }
        //                else
        //                {
        //                    LFIMG = inv.PlanQty.ToString();
        //                }
        //                PickDetail pikd = (PickDetail)_WarehouseFacade.GetPickLineByDQMcode(pickNo, inv.DQMCode);
        //                if (pikd == null)
        //                {
        //                    this.DataProvider.RollbackTransaction();
        //                    WebInfoPublish.Publish(this, "拣货任务令中没有鼎桥物料号：" + inv.DQMCode, this.languageComponent1);
        //                    return;
        //                }
        //                Pick pik = (Pick)_WarehouseFacade.GetPick(pickNo);
        //                if (pik == null)
        //                {
        //                    this.DataProvider.RollbackTransaction();
        //                    WebInfoPublish.Publish(this, "拣货任务令中没有拣货任务令号：" + inv.DQMCode, this.languageComponent1);
        //                    return;
        //                }
        //                Pickdetailmaterial pickdetailmaterial = (Pickdetailmaterial)_WarehouseFacade.GetPickDetailMaterialFromDQMCODE(inv.DQMCode, pickNo, pikd.PickLine);
        //                if (pickdetailmaterial == null)
        //                {
        //                    this.DataProvider.RollbackTransaction();
        //                    WebInfoPublish.Publish(this, "拣货任务令中没有拣货任务令号：" + inv.DQMCode, this.languageComponent1);
        //                    return;
        //                }
        //                #region
        //                string EBELN = pik.StNo;	//	采购订单号
        //                string EBELP = inv.InvLine.ToString("G0");	//	行项目号
        //                string ZNUMBER = "";	//	流水号
        //                string MATNR = inv.DQMCode;	//	物料号
        //                string MENGE = LFIMG;	//	数量
        //                string MEINS = inv.Unit;	//	单位
        //                string BWART = "101";	//	103/105/104/101状态 
        //                string RETPO = "";//inv.ReturnFlag;	//	退货标识
        //                string SGTXT = pickdetailmaterial.CUser;	//	仓库操作员
        //                string LFSNR = inv.InvNo;	//	供应商送货单号
        //                string LGORT = inv.StorageCode;	//	库位
        //                string BKTXT = "";// inv.DetailRemark;	//	备注
        //                string BUDAT = pickdetailmaterial.MaintainTime.ToString("G0");	//	时间


        //                #endregion

        //                #region xml
        //                int i = 0;
        //                //string xmlStr = "<?xml version='1.0' encoding='utf-8'?> ";
        //                string xmlStr = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        //                xmlStr += string.Format(@"   <inputs><rows>
        //                                 <EBELN>" + EBELN + @" </EBELN>
        //                                <EBELP>" + EBELP + @" </EBELP>
        //                                <ZNUMBER>" + ZNUMBER + @" </ZNUMBER>
        //                                <MATNR>" + MATNR + @" </MATNR>
        //                                <MENGE>" + MENGE + @" </MENGE>
        //                                <MEINS>" + MEINS + @" </MEINS>
        //                                <BWART>" + BWART + @" </BWART>
        //                                <RETPO>" + RETPO + @" </RETPO>
        //                                <SGTXT>" + SGTXT + @" </SGTXT>
        //                                <LFSNR>" + LFSNR + @" </LFSNR>
        //                                <LGORT>" + LGORT + @" </LGORT>
        //                                <BKTXT>" + BKTXT + @" </BKTXT>
        //                                <BUDAT>" + BUDAT + @" </BUDAT>
        //                                      </rows>
        //                                    <userid>test</userid><userkey>DBCA6E1E-8477-4188-A429-C042B395CC64</userkey></inputs>");
        //                #endregion
        //                ryan ryan = new ryan();
        //                try
        //                {
        //                    string returnstr = ryan.ZCHN_MM_PO_GR_REC_ACP_REV(xmlStr);
        //                    BenQGuru.eMES.Common.Log.Error(returnstr + " ======================================== " + xmlStr);
        //                }
        //                catch (Exception ex)
        //                {

        //                    BenQGuru.eMES.Common.Log.Error(ex.Message + "================================" + xmlStr);
        //                }

        //                //}
        //                //else
        //                //{
        //                //    throw new SAPException(batchCode + "发货批不包含单据" + inv.InvNo + "的物料号" + inv.DQMCode);
        //                //}
        //            }
        //        }

        private void PGIPostToXML(Dictionary<string, decimal> ddd, string batchCode, InvoicesDetailExt[] ins, string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            ShareLib.ShareKit kit = new ShareLib.ShareKit();
            string message = string.Empty;
            kit.PostToSupport(ddd, batchCode, ins, pickNo, _WarehouseFacade, base.DataProvider, out message);

        }




        private Dictionary<string, List<UB>> GetUBToSapDomainGroupByInvNo(Dictionary<string, decimal> ddd, string batchCode, BenQGuru.eMES.Material.InvoicesDetailEx[] ins)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.UB>> UBGroupByInvNo = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.UB>>();

            foreach (BenQGuru.eMES.Material.InvoicesDetailEx inv in ins)
            {
                BenQGuru.eMES.SAPRFCService.Domain.UB ub = new BenQGuru.eMES.SAPRFCService.Domain.UB();
                if (ddd.ContainsKey(inv.DQMCode))
                {
                    decimal remaindCount = ddd[inv.DQMCode];
                    remaindCount -= inv.PlanQty;

                    if (remaindCount >= 0)
                    {
                        ub.Qty = inv.PlanQty;
                        ddd[inv.DQMCode] -= inv.PlanQty;
                    }
                    else
                    {
                        ub.Qty = ddd[inv.DQMCode];
                        ddd[inv.DQMCode] = 0;
                    }
                    ub.UBNO = inv.InvNo;

                    ub.Unit = inv.Unit;
                    ub.UBLine = inv.InvLine;

                    ub.MCode = inv.MCode;
                    ub.ContactUser = " ";
                    ub.DocumentDate = FormatHelper.TODateInt(DateTime.Now);
                    ub.FacCode = inv.FacCode;
                    ub.MesTransNO = batchCode;
                    ub.StorageCode = inv.FromStorageCode;

                    if (UBGroupByInvNo.ContainsKey(inv.InvNo))
                    {
                        UBGroupByInvNo[inv.InvNo].Add(ub);
                    }
                    else
                    {
                        UBGroupByInvNo[inv.InvNo] = new List<BenQGuru.eMES.SAPRFCService.Domain.UB>();
                        UBGroupByInvNo[inv.InvNo].Add(ub);
                    }
                }
                //else
                //{
                //    throw new SAPException(batchCode + "发货批不包含单据" + inv.InvNo + "的物料号" + inv.DQMCode);
                //}


            }
            return UBGroupByInvNo;
        }


        #endregion
        //private void UBToSAPForOut(Dictionary<string, decimal> ddd, string batchCode, BenQGuru.eMES.Material.InvoicesDetailEx[] ins)
        //{
        //    if (_WarehouseFacade == null)
        //    {
        //        _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
        //    }
        //    if (inventoryFacade == null)
        //    {
        //        inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
        //    }
        //    Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.UB>> UBGroupByInvNo = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.UB>>();

        //    foreach (BenQGuru.eMES.Material.InvoicesDetailEx inv in ins)
        //    {
        //        BenQGuru.eMES.SAPRFCService.Domain.UB ub = new BenQGuru.eMES.SAPRFCService.Domain.UB();
        //        if (ddd.ContainsKey(inv.DQMCode))
        //        {
        //            decimal remaindCount = ddd[inv.DQMCode];
        //            remaindCount -= inv.PlanQty;

        //            if (remaindCount >= 0)
        //            {
        //                ub.Qty = inv.PlanQty;
        //                ddd[inv.DQMCode] -= inv.PlanQty;
        //            }
        //            else
        //            {
        //                ub.Qty = ddd[inv.DQMCode];
        //                ddd[inv.DQMCode] = 0;
        //            }
        //            ub.UBNO = inv.InvNo;

        //            ub.Unit = inv.Unit;
        //            ub.UBLine = inv.InvLine;

        //            ub.InOutFlag = "351";
        //            ub.MCode = inv.MCode;
        //            ub.ContactUser = " ";
        //            ub.DocumentDate = FormatHelper.TODateInt(DateTime.Now);
        //            ub.FacCode = inv.FacCode;
        //            ub.MesTransNO = batchCode;
        //            ub.StorageCode = inv.FromStorageCode;
        //        }
        //        else
        //        {
        //            throw new SAPException(batchCode + "发货批不包含单据" + inv.InvNo + "的物料号" + inv.DQMCode);
        //        }

        //        if (UBGroupByInvNo.ContainsKey(inv.InvNo))
        //        {
        //            UBGroupByInvNo[inv.InvNo].Add(ub);
        //        }
        //        else
        //        {
        //            UBGroupByInvNo[inv.InvNo] = new List<BenQGuru.eMES.SAPRFCService.Domain.UB>();
        //            UBGroupByInvNo[inv.InvNo].Add(ub);
        //        }
        //    }
        //    #region add by sam
        //    DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
        //    bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
        //    #endregion

        //    foreach (string invo in UBGroupByInvNo.Keys)
        //    {
        //        if (UBGroupByInvNo[invo].Count > 0)
        //        {


        //            if (is2Sap)
        //            {
        //                LogUB2Sap(UBGroupByInvNo[invo]);
        //            }
        //            else
        //            {
        //                BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = SendUBToSAP(UBGroupByInvNo[invo]);
        //                List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns =
        //                    new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();

        //                foreach (BenQGuru.eMES.SAPRFCService.Domain.UB ub in UBGroupByInvNo[invo])
        //                {
        //                    BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
        //                    dn.BatchNO = ub.MesTransNO;
        //                    dn.DNLine = ub.UBLine;
        //                    dn.DNNO = ub.UBNO;
        //                    dn.Qty = ub.Qty;
        //                    dn.Unit = ub.Unit;
        //                    dns.Add(dn);
        //                }
        //                LogDN(dns, r, "UB");
        //                if (r == null)
        //                {
        //                    throw new SAPException(invo + "SAP回写返回空:");
        //                }
        //                if (string.IsNullOrEmpty(r.Result))
        //                {
        //                    throw new SAPException(invo + "SAP回写失败 值:" + r.Result + ":" + r.Message);
        //                }
        //                if (r.Result.ToUpper().Trim() != "S")
        //                {
        //                    throw new SAPException(invo + "SAP回写失败 值:" + r.Result + r.Message);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            throw new SAPException(invo + "单号包含的回写数据为空！");
        //        }
        //    }

        //}



        //private void UBToSAPForIn(Dictionary<string, decimal> ddd, string batchCode, BenQGuru.eMES.Material.InvoicesDetailEx[] ins)
        //{
        //    if (_WarehouseFacade == null)
        //    {
        //        _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
        //    }
        //    if (inventoryFacade == null)
        //    {
        //        inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
        //    }
        //    #region add by sam
        //    DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
        //    bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
        //    #endregion

        //    Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.UB>> UBGroupByInvNo = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.UB>>();

        //    foreach (BenQGuru.eMES.Material.InvoicesDetailEx inv in ins)
        //    {
        //        BenQGuru.eMES.SAPRFCService.Domain.UB ub = new BenQGuru.eMES.SAPRFCService.Domain.UB();
        //        if (ddd.ContainsKey(inv.DQMCode))
        //        {
        //            decimal remaindCount = ddd[inv.DQMCode];
        //            remaindCount -= inv.PlanQty;

        //            if (remaindCount >= 0)
        //            {
        //                ub.Qty = inv.PlanQty;
        //                ddd[inv.DQMCode] -= inv.PlanQty;
        //            }
        //            else
        //            {
        //                ub.Qty = ddd[inv.DQMCode];
        //                ddd[inv.DQMCode] = 0;
        //            }

        //            ub.UBNO = inv.InvNo;

        //            ub.Unit = inv.Unit;
        //            ub.UBLine = inv.InvLine;

        //            ub.InOutFlag = "101";
        //            ub.MCode = inv.MCode;
        //            ub.ContactUser = " ";
        //            ub.DocumentDate = FormatHelper.TODateInt(DateTime.Now);
        //            ub.FacCode = inv.FacCode;
        //            ub.MesTransNO = batchCode;
        //            ub.StorageCode = inv.StorageCode;
        //        }
        //        else
        //        {
        //            throw new SAPException(batchCode + "发货批不包含单据" + inv.InvNo + "的物料号" + inv.DQMCode);
        //        }

        //        if (UBGroupByInvNo.ContainsKey(inv.InvNo))
        //        {
        //            UBGroupByInvNo[inv.InvNo].Add(ub);
        //        }
        //        else
        //        {
        //            UBGroupByInvNo[inv.InvNo] = new List<BenQGuru.eMES.SAPRFCService.Domain.UB>();
        //            UBGroupByInvNo[inv.InvNo].Add(ub);
        //        }
        //    }



        //    foreach (string invo in UBGroupByInvNo.Keys)
        //    {
        //        if (UBGroupByInvNo[invo].Count > 0)
        //        {
        //            if (is2Sap)
        //            {
        //                LogUB2Sap(UBGroupByInvNo[invo]);
        //            }
        //            else
        //            {
        //                BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = SendUBToSAP(UBGroupByInvNo[invo]);
        //                List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns =
        //                    new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();

        //                foreach (BenQGuru.eMES.SAPRFCService.Domain.UB ub in UBGroupByInvNo[invo])
        //                {
        //                    BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
        //                    dn.BatchNO = ub.MesTransNO;
        //                    dn.DNLine = ub.UBLine;
        //                    dn.DNNO = ub.UBNO;
        //                    dn.Qty = ub.Qty;
        //                    dn.Unit = ub.Unit;
        //                    dns.Add(dn);
        //                }
        //                LogDN(dns, r, "UB");
        //                if (r == null)
        //                {
        //                    throw new SAPException(invo + "SAP回写返回空:");
        //                }
        //                if (string.IsNullOrEmpty(r.Result))
        //                {
        //                    throw new SAPException(invo + "SAP回写失败 值:" + r.Result + ":" + r.Message);
        //                }
        //                if (r.Result.ToUpper().Trim() != "S")
        //                {
        //                    throw new SAPException(invo + "SAP回写失败 值:" + r.Result + r.Message);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            throw new SAPException(invo + "单号包含的回写数据为空！");
        //        }
        //    }

        //}
        private void RSToSAP(Dictionary<string, decimal> ddd, string batchCode, BenQGuru.eMES.Material.InvoicesDetailEx[] ins, string inOutFlag, string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.RS>> RSGroupByInvNo = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.RS>>();
            #region add by sam
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
            #endregion

            foreach (BenQGuru.eMES.Material.InvoicesDetailEx inv in ins)
            {
                BenQGuru.eMES.Common.Log.Error("--------------------1-------------------------");
                BenQGuru.eMES.SAPRFCService.Domain.RS rs = new BenQGuru.eMES.SAPRFCService.Domain.RS();
                if (ddd.ContainsKey(inv.DQMCode))
                {
                    decimal remainCount = ddd[inv.DQMCode];
                    BenQGuru.eMES.Common.Log.Error("--------------------ddd[" + inv.DQMCode + "]" + remainCount + "-------------------------");
                    remainCount -= inv.PlanQty;

                    if (remainCount >= 0)
                    {
                        rs.Qty = inv.PlanQty;
                        ddd[inv.DQMCode] -= inv.PlanQty;
                    }
                    else
                    {
                        rs.Qty = ddd[inv.DQMCode];
                        ddd[inv.DQMCode] = 0;
                    }

                    rs.RSNO = inv.InvNo;
                    rs.RSLine = inv.InvLine;
                    rs.Unit = inv.Unit;
                    rs.DocumentDate = FormatHelper.TODateInt(DateTime.Now);
                    rs.FacCode = "10Y2";
                    rs.InOutFlag = inOutFlag;
                    rs.MCode = inv.MCode;
                    rs.MesTransNO = pickNo;
                    rs.StorageCode = inv.StorageCode;

                }
                else
                    continue;


                if (RSGroupByInvNo.ContainsKey(inv.InvNo))
                {
                    RSGroupByInvNo[inv.InvNo].Add(rs);
                }
                else
                {
                    RSGroupByInvNo[inv.InvNo] = new List<BenQGuru.eMES.SAPRFCService.Domain.RS>();
                    RSGroupByInvNo[inv.InvNo].Add(rs);
                }
                BenQGuru.eMES.Common.Log.Error("--------------------end-------------------------");

            }

            foreach (string invo in RSGroupByInvNo.Keys)
            {
                if (RSGroupByInvNo[invo].Count == 0)
                    continue;

                if (is2Sap)
                {
                    LogRS2Sap(RSGroupByInvNo[invo]);
                }
                else
                {
                    #region SAP回写
                    BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = SendRSToSAP(RSGroupByInvNo[invo]);
                    List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();

                    foreach (BenQGuru.eMES.SAPRFCService.Domain.RS rs in RSGroupByInvNo[invo])
                    {
                        BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                        dn.BatchNO = rs.MesTransNO;
                        dn.DNLine = rs.RSLine;
                        dn.DNNO = rs.RSNO;
                        dn.Qty = rs.Qty;
                        dn.Unit = rs.Unit;
                        dns.Add(dn);
                    }
                    LogDN(dns, r, "RS");
                    if (r == null)
                    {
                        throw new SAPException(invo + "SAP回写返回空:");
                    }
                    if (string.IsNullOrEmpty(r.Result))
                    {
                        throw new SAPException(invo + "SAP回写失败 值:" + r.Result + ":" + r.Message);
                    }
                    if (r.Result.ToUpper().Trim() != "S")
                    {
                        throw new SAPException(invo + "SAP回写失败 值:" + r.Result + r.Message);
                    }
                    #endregion
                }


                //else
                //{
                //    throw new SAPException(invo + "单号包含的回写数据为空！");
                //}
            }

        }
        private void WWPoToSAP(Dictionary<string, decimal> ddd, string batchCode, BenQGuru.eMES.Material.InvoicesDetailEx[] ins, string storageCode, string pickno)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.WWPO>> WWGroupByInvNo = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.WWPO>>();

            #region add by sam
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
            #endregion

            foreach (BenQGuru.eMES.Material.InvoicesDetailEx inv in ins)
            {

                PickDetail pdt = _WarehouseFacade.SumPickDetailPQty(inv.InvNo, inv.MCode, pickno);
                if (pdt != null)
                {
                    BenQGuru.eMES.SAPRFCService.Domain.WWPO wwPo = new BenQGuru.eMES.SAPRFCService.Domain.WWPO();
                    wwPo.PONO = inv.InvNo;
                    wwPo.Qty = pdt.QTY;
                    wwPo.Unit = inv.Unit;
                    wwPo.InOutFlag = "541";
                    wwPo.FacCode = "10Y2";//inv.FacCode;
                    wwPo.MCode = inv.MCode;
                    wwPo.MesTransNO = pickno + "-" + pdt.PickLine;
                    wwPo.MesTransDate = FormatHelper.TODateInt(DateTime.Now);
                    wwPo.StorageCode = storageCode;// inv.FromStorageCode;//tblpick
                    wwPo.VendorCode = "";//541 为空


                    if (WWGroupByInvNo.ContainsKey(inv.InvNo))
                    {
                        WWGroupByInvNo[inv.InvNo].Add(wwPo);
                    }
                    else
                    {
                        WWGroupByInvNo[inv.InvNo] = new List<BenQGuru.eMES.SAPRFCService.Domain.WWPO>();
                        WWGroupByInvNo[inv.InvNo].Add(wwPo);
                    }
                }
            }


            foreach (string invo in WWGroupByInvNo.Keys)
            {
                if (WWGroupByInvNo[invo].Count == 0)
                    continue;

                if (is2Sap)
                {
                    LogWWPoSap(WWGroupByInvNo[invo]);
                }
                else
                {
                    #region SAP
                    BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = SendWWPOToSAP(WWGroupByInvNo[invo]);
                    List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();

                    foreach (BenQGuru.eMES.SAPRFCService.Domain.WWPO wwPo in WWGroupByInvNo[invo])
                    {
                        BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                        dn.BatchNO = wwPo.MesTransNO;
                        dn.DNLine = wwPo.POLine;
                        dn.DNNO = wwPo.PONO;
                        dn.Qty = wwPo.Qty;
                        dn.Unit = wwPo.Unit;
                        dns.Add(dn);
                    }
                    LogDN(dns, r, "WWPO");
                    if (r == null)
                    {
                        throw new SAPException(invo + "SAP回写返回空:");
                    }
                    if (string.IsNullOrEmpty(r.Result))
                    {
                        throw new SAPException(invo + "SAP回写失败 值:" + r.Result + ":" + r.Message);
                    }
                    if (r.Result.ToUpper().Trim() != "S")
                    {
                        throw new SAPException(invo + "SAP回写失败 值:" + r.Result + r.Message);
                    }
                    #endregion
                }
                //}
                //else
                //{
                //    throw new SAPException(invo + "单号包含的回写数据为空！");
                //}
            }

        }
        private void StockScrapToSAP(Pick pick)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.StockScrap>> StockScrapGroupByInvNo = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.StockScrap>>();

            PickDetail[] pickDetails = _WarehouseFacade.GetPickdetails(pick.PickNo);
            List<BenQGuru.eMES.SAPRFCService.Domain.StockScrap> ssList = new List<BenQGuru.eMES.SAPRFCService.Domain.StockScrap>();
            if (pickDetails.Length <= 0)
            {
                throw new SAPException("拣货任务令对应的明细为空！");
            }
            foreach (PickDetail d in pickDetails)
            {
                if (d.PQTY > 0)
                {
                    BenQGuru.eMES.SAPRFCService.Domain.StockScrap ss = new BenQGuru.eMES.SAPRFCService.Domain.StockScrap();

                    ss.CC = pick.CC;
                    ss.FacCode = "10Y2";
                    ss.DocumentDate = FormatHelper.TODateInt(DateTime.Now);
                    //ss.FacCode = string.IsNullOrEmpty(pick.FacCode) ? " " : pick.FacCode;
                    ss.MCode = d.MCode;
                    ss.MESScrapNO = pick.PickNo;
                    ss.Operator = GetUserCode();
                    ss.Unit = string.IsNullOrEmpty(d.Unit) ? " " : d.Unit;
                    ss.ScrapCode = "551";
                    ss.Remark = " ";
                    ss.StorageCode = pick.StorageCode;
                    ss.Qty = d.PQTY;
                    ssList.Add(ss);
                }
            }

            List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();

            foreach (BenQGuru.eMES.SAPRFCService.Domain.StockScrap s in ssList)
            {
                BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                dn.BatchNO = s.MESScrapNO;
                dn.DNLine = 0;

                dn.DNNO = " ";
                dn.Qty = s.Qty;
                dn.Unit = s.Unit;
                dns.Add(dn);
            }

            #region add by sam
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
            #endregion
            if (is2Sap)
            {
                LogStockScrap2Sap(ssList);
            }
            else
            {
                BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = SendStockScrapToSAP(ssList);
                LogDN(dns, r, "StockScrap");
                if (r == null)
                {
                    throw new SAPException(pick.PickNo + "SAP回写返回空:");
                }
                if (string.IsNullOrEmpty(r.Result))
                {
                    throw new SAPException(pick.PickNo + "SAP回写失败 值:" + r.Result + ":" + r.Message);
                }
                if (r.Result.ToUpper().Trim() != "S")
                {
                    throw new SAPException(pick.PickNo + "SAP回写失败 值:" + r.Result + r.Message);
                }
            }
        }
        private void PoToSAP(Dictionary<string, decimal> ddd, string batchCode, BenQGuru.eMES.Material.InvoicesDetailEx[] ins, string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.PO>> PoGroupByInvNo = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.PO>>();

            foreach (BenQGuru.eMES.Material.InvoicesDetailEx inv in ins)
            {
                BenQGuru.eMES.SAPRFCService.Domain.PO po = new BenQGuru.eMES.SAPRFCService.Domain.PO();
                if (ddd.ContainsKey(inv.DQMCode))
                {
                    decimal remainCount = ddd[inv.DQMCode];
                    remainCount -= inv.PlanQty;

                    if (remainCount >= 0)
                    {
                        po.Qty = inv.PlanQty;
                        ddd[inv.DQMCode] -= inv.PlanQty;
                    }
                    else
                    {
                        po.Qty = ddd[inv.DQMCode];
                        ddd[inv.DQMCode] = 0;
                    }
                    int serial = inventoryFacade.GetMaxSerialInPoLog() + 1;

                    po.PONO = inv.InvNo;
                    po.POLine = inv.InvLine;
                    po.Unit = inv.Unit;
                    po.FacCode = inv.FacCode;

                    po.MCode = inv.MCode;
                    po.Operator = GetUserCode();
                    po.InvoiceDate = FormatHelper.TODateInt(DateTime.Now);

                    po.Status = "101";//接收
                    po.VendorInvoice = " ";
                    po.StorageCode = inv.StorageCode;
                    po.Remark = " ";
                    po.ZNUMBER = serial.ToString();

                }
                //else
                //{
                //    throw new SAPException(batchCode + "发货批不包含单据" + inv.InvNo + "的物料号" + inv.DQMCode);
                //}

                if (PoGroupByInvNo.ContainsKey(inv.InvNo))
                {
                    PoGroupByInvNo[inv.InvNo].Add(po);
                }
                else
                {
                    PoGroupByInvNo[inv.InvNo] = new List<BenQGuru.eMES.SAPRFCService.Domain.PO>();
                    PoGroupByInvNo[inv.InvNo].Add(po);
                }
            }
            #region add by sam
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
            #endregion

            foreach (string invo in PoGroupByInvNo.Keys)
            {
                if (PoGroupByInvNo[invo].Count == 0)
                    continue;

                if (is2Sap)
                {
                    LogPO2Sap(PoGroupByInvNo[invo]);
                }
                else
                {

                    BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = SendPoToSAP(PoGroupByInvNo[invo]);
                    List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns =
                        new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();

                    foreach (BenQGuru.eMES.SAPRFCService.Domain.PO po in PoGroupByInvNo[invo])
                    {
                        BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                        //dn.BatchNO = PO.;
                        dn.DNLine = po.POLine;
                        dn.DNNO = po.PONO;
                        dn.Qty = po.Qty;
                        dn.Unit = po.Unit;

                        dns.Add(dn);
                    }
                    LogDN(dns, r, "PO");
                    if (r == null)
                    {
                        throw new SAPException(invo + "SAP回写返回空:");
                    }
                    if (string.IsNullOrEmpty(r.Result))
                    {
                        throw new SAPException(invo + "SAP回写失败 值:" + r.Result + ":" + r.Message);
                    }
                    if (r.Result.ToUpper().Trim() != "S")
                    {
                        throw new SAPException(invo + "SAP回写失败 值:" + r.Result + r.Message);
                    }

                    ShareLib.ShareKit.PoToSupport(PoGroupByInvNo[invo], true);
                }
                //}
                //else
                //{
                //    throw new SAPException(invo + "单号包含的回写数据为空！");
                //}
            }

        }
        private void DNToSAP(Dictionary<string, decimal> ddd, string batchCode, BenQGuru.eMES.Material.InvoicesDetailEx[] ins)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            Dictionary<string, bool> calcFlag = new Dictionary<string, bool>();
            Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>> dnsOk = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>>();
            Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>> dnsBad = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>>();
            foreach (BenQGuru.eMES.Material.InvoicesDetailEx inv in ins)
            {
                if (string.IsNullOrEmpty(inv.MovementType))
                {
                    #region
                    BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                    dn.DNNO = inv.InvNo;
                    dn.DNLine = inv.InvLine;
                    dn.Unit = " ";
                    dn.BatchNO = batchCode;

                    //dn.Qty = inv.PlanQty;
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
                    #region




                    if (ddd.ContainsKey(inv.DQMCode))
                    {
                        if (ddd[inv.DQMCode] >= 0)
                        {
                            ddd[inv.DQMCode] -= inv.PlanQty;
                        }
                        BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                        dn.DNNO = inv.InvNo;
                        dn.DNLine = inv.InvLine;
                        dn.Unit = inv.Unit;
                        dn.BatchNO = batchCode;
                        if (ddd[inv.DQMCode] >= 0)
                        {
                            dn.Qty = inv.PlanQty;
                            if (dnsOk.ContainsKey(inv.InvNo))
                                dnsOk[inv.InvNo].Add(dn);
                            else
                            {
                                dnsOk[inv.InvNo] = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();
                                dnsOk[inv.InvNo].Add(dn);
                            }
                        }
                        else
                        {
                            if (calcFlag.ContainsKey(inv.DQMCode))
                                dn.Qty = 0;
                            else
                            {
                                dn.Qty = inv.PlanQty - (Math.Abs(ddd[inv.DQMCode]));
                                calcFlag[inv.DQMCode] = true;
                            }
                            if (dnsBad.ContainsKey(inv.InvNo))
                                dnsBad[inv.InvNo].Add(dn);
                            else
                            {
                                dnsBad[inv.InvNo] = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();
                                dnsBad[inv.InvNo].Add(dn);
                            }
                        }
                    }
                    //else
                    //{


                    //    BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                    //    dn.DNNO = inv.InvNo;
                    //    dn.DNLine = inv.InvLine;
                    //    dn.Unit = inv.Unit;
                    //    dn.BatchNO = batchCode;
                    //    dn.Qty = inv.PlanQty;
                    //    if (dnsOk.ContainsKey(inv.InvNo))
                    //        dnsOk[inv.InvNo].Add(dn);
                    //    else
                    //    {
                    //        dnsOk[inv.InvNo] = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();
                    //        dnsOk[inv.InvNo].Add(dn);
                    //    }


                    //}

                    #endregion
                }


            }

            #region add by sam
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
            #endregion

            foreach (string key in dnsOk.Keys)
            {

                if (dnsOk[key].Count > 0)
                {
                    string flag = "Y";
                    if (dnsBad.ContainsKey(key))
                    {
                        flag = "N";
                        foreach (string badkey in dnsBad.Keys)
                        {
                            if (dnsOk.ContainsKey(badkey))
                                dnsOk[badkey].AddRange(dnsBad[badkey]);
                            else
                                dnsOk.Add(badkey, dnsBad[badkey]);

                        }
                    }
                    if (is2Sap)
                    {


                        LogDN2Sap(dnsOk[key], flag);
                    }
                    else
                    {


                        #region SAP回写
                        BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ok = SendDNToSap(dnsOk[key], !dnsBad.ContainsKey(key));
                        LogDN(dnsOk[key], ok, flag);
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
                        #endregion
                    }

                }
                //else
                //{
                //    throw new SAPException(key + "项目为空！");
                //}
            }

            //BenQGuru.eMES.Common.Log.Error("---------------------------------------------dns.keys----------------------" + dnsBad.Keys.Count + "-----------------------------------------------");
            //foreach (string key in dnsBad.Keys)
            //{


            //    if (dnsBad[key].Count > 0)
            //    {

            //        if (is2Sap)
            //        {
            //            LogDN2Sap(dnsBad[key], "N");
            //        }
            //        else
            //        {
            //            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn bad = SendDNToSap(dnsBad[key], false);

            //            BenQGuru.eMES.Common.Log.Error("---------------------------------------------dns.values---------------------------------------------------------------------");

            //            foreach (string keybad in dnsBad.Keys)
            //            {
            //                BenQGuru.eMES.Common.Log.Error("---------------------------------------------" + keybad + "---------" + dnsBad[keybad] + "------------------------------------------------------------");
            //            }
            //            LogDN(dnsBad[key], bad, "N");
            //            if (bad == null)
            //            {

            //                throw new SAPException(key + "SAP欠料返回为空:");
            //            }
            //            if (string.IsNullOrEmpty(bad.Result))
            //            {

            //                throw new SAPException(key + "SAP欠料回写失败:" + bad.Message);
            //            }
            //            if (bad.Result.ToUpper().Trim() != "S")
            //            {

            //                throw new SAPException(key + "SAP欠料回写失败:" + bad.Message);
            //            }

            //            BenQGuru.eMES.Common.Log.Error(bad.Result + ":" + bad.Message);
            //        }
            //    }
            //    //else
            //    //{
            //    //    throw new SAPException(key + "欠料回写项目为空！");
            //    //}

            //}
        }


        #region add by sam  LogDN2Sap
        private void LogDN2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns, string isAll)
        {
            foreach (BenQGuru.eMES.SAPRFCService.Domain.DN dn in dns)
            {

                DN2Sap log = new DN2Sap();
                log.DNLine = dn.DNLine;
                log.DNNO = dn.DNNO;
                log.IsAll = isAll;
                log.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                log.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                log.MaintainUser = GetUserCode();
                log.Result = string.Empty; ;
                log.Message = string.Empty;
                log.Qty = dn.Qty;
                log.Unit = dn.Unit;
                log.TRANSTYPE = "OUT";
                #region 注释
                //if (ret == null)
                //else
                //    log.RESULT = ret.Result;
                //log.MESSAGE = ret != null ? ret.Message : "null";
                //if (ret != null && string.IsNullOrEmpty(ret.Message))
                //{
                //} 
                #endregion
                log.BatchNO = dn.BatchNO;
                inventoryFacade.AddDN2Sap(log);
            }
        }

        private void LogPO2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.PO> dns)
        {
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            foreach (BenQGuru.eMES.SAPRFCService.Domain.PO po in dns)
            {

                Po2Sap poLog = new Po2Sap();
                poLog.PONO = po.PONO;
                poLog.POLine = po.POLine;
                poLog.SerialNO = " ";
                poLog.Qty = po.Qty; // 
                poLog.Unit = po.Unit;
                poLog.FacCode = po.FacCode;
                poLog.InvoiceDate = po.InvoiceDate; //  
                poLog.MCode = po.MCode;//SAPMcode
                poLog.SAPMaterialInvoice = po.SAPMaterialInvoice;
                poLog.Operator = po.Operator;
                poLog.Status = po.Status;
                poLog.VendorInvoice = po.VendorInvoice;
                poLog.StorageCode = po.StorageCode;
                poLog.Remark = po.Remark;
                poLog.SapDateStamp = dbTime.DBDate;
                poLog.SapTimeStamp = dbTime.DBTime;
                poLog.ZNUMBER = po.ZNUMBER;
                //poLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                //poLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                //poLog.MaintainUser = GetUserCode();
                //poLog.r = "empty";
                //poLog.Message = "empty";
                inventoryFacade.AddPo2Sap(poLog);
            }
        }

        private void LogRS2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.RS> dns)
        {
            foreach (BenQGuru.eMES.SAPRFCService.Domain.RS rs in dns)
            {

                Rs2Sap rsLog = new Rs2Sap();
                rsLog.Qty = rs.Qty;
                rsLog.RSNO = rs.RSNO;
                rsLog.RSLine = rs.RSLine;
                rsLog.Unit = rs.Unit;
                rsLog.DocumentDate = rs.DocumentDate;
                rsLog.FacCode = rs.FacCode;
                rsLog.InOutFlag = rs.InOutFlag;
                rsLog.MCode = rs.MCode;
                rsLog.MesTransNO = rs.MesTransNO;
                rsLog.StorageCode = rs.StorageCode;
                rsLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                rsLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                rsLog.MaintainUser = GetUserCode();
                inventoryFacade.AddRs2Sap(rsLog);
            }
        }

        private void LogStockScrap2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.StockScrap> dns)
        {
            foreach (BenQGuru.eMES.SAPRFCService.Domain.StockScrap ss in dns)
            {

                StockScrap2Sap ssLog = new StockScrap2Sap();
                ssLog.CC = ss.CC;

                ssLog.DocumentDate = ss.DocumentDate;
                //ss.FacCode = string.IsNullOrEmpty(pick.FacCode) ? " " : pick.FacCode;
                ssLog.MCode = ss.MCode;
                ssLog.MESScrapNO = ss.MESScrapNO;
                ssLog.Operator = ss.Operator;
                ssLog.Unit = ss.Unit;
                ssLog.ScrapCode = "551";
                ssLog.Remark = " ";
                ssLog.StorageCode = ss.StorageCode;
                ssLog.Operator = ss.Operator;
                ssLog.Qty = ss.Qty;
                ssLog.FacCode = "10Y2";
                ssLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                ssLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                ssLog.MaintainUser = GetUserCode();
                inventoryFacade.AddStockScrap2Sap(ssLog);
            }
        }

        private void LogWWPoSap(List<BenQGuru.eMES.SAPRFCService.Domain.WWPO> dns)
        {
            foreach (BenQGuru.eMES.SAPRFCService.Domain.WWPO wwPo in dns)
            {

                Wwpo2Sap wwpoLog = new Wwpo2Sap();
                wwpoLog.Qty = wwPo.Qty;
                wwpoLog.PONO = wwPo.PONO;
                wwpoLog.POLine = wwPo.POLine;
                wwpoLog.Unit = wwPo.Unit;
                wwpoLog.InOutFlag = wwPo.InOutFlag;
                wwpoLog.FacCode = wwPo.FacCode;
                wwpoLog.MCode = wwPo.MCode;
                wwpoLog.MesTransNO = wwPo.MesTransNO;
                wwpoLog.MesTransDate = wwPo.MesTransDate;
                wwpoLog.StorageCode = wwPo.StorageCode;// inv.FromStorageCode;//tblpick
                wwpoLog.VendorCode = wwPo.VendorCode;//541 为空
                wwpoLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                wwpoLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                wwpoLog.MaintainUser = GetUserCode();
                inventoryFacade.AddWwpo2Sap(wwpoLog);
            }
        }


        private void LogUB2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.UB> dns, string inOutFlag)
        {
            foreach (BenQGuru.eMES.SAPRFCService.Domain.UB ub in dns)
            {

                Ub2Sap ubLog = new Ub2Sap();
                ubLog.Qty = ub.Qty;
                ubLog.UBNO = ub.UBNO;
                ubLog.Unit = ub.Unit;
                ubLog.UBLine = ub.UBLine;
                ubLog.InOutFlag = ub.InOutFlag;
                ubLog.MCode = ub.MCode;
                ubLog.ContactUser = ub.ContactUser;
                ubLog.DocumentDate = ub.DocumentDate;
                ubLog.FacCode = ub.FacCode;
                ubLog.InOutFlag = inOutFlag;
                ubLog.MesTransNO = ub.MesTransNO;
                ubLog.StorageCode = ub.StorageCode;
                ubLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                ubLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                ubLog.MaintainUser = GetUserCode();
                inventoryFacade.AddUb2Sap(ubLog);
            }
        }

        #endregion

        private void LogDN(List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret, string isAll)
        {
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


        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendPoToSAP(List<BenQGuru.eMES.SAPRFCService.Domain.PO> Pos)
        {
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.POToSAP PoToS = new BenQGuru.eMES.SAPRFCService.POToSAP(this.DataProvider);

            try
            {

                r = PoToS.POReceiveToSAP(Pos);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }

            return r;

        }

        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendStockScrapToSAP(List<BenQGuru.eMES.SAPRFCService.Domain.StockScrap> ss)
        {
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.StockScrapToSAP ssToSap = new BenQGuru.eMES.SAPRFCService.StockScrapToSAP(this.DataProvider);

            try
            {

                r = ssToSap.PostStockScrapToSAP(ss);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }

            return r;

        }

        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendWWPOToSAP(List<BenQGuru.eMES.SAPRFCService.Domain.WWPO> wwPOs)
        {
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.WWPO2SAP wwPoToS = new BenQGuru.eMES.SAPRFCService.WWPO2SAP(this.DataProvider);

            try
            {

                r = wwPoToS.PostWWPOToSAP(wwPOs);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }

            return r;

        }

        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendRSToSAP(List<BenQGuru.eMES.SAPRFCService.Domain.RS> rss)
        {
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.RSToSAP rToS = new BenQGuru.eMES.SAPRFCService.RSToSAP(this.DataProvider);

            try
            {

                r = rToS.PostRSToSAP(rss);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }

            return r;

        }

        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendUBToSAP(List<BenQGuru.eMES.SAPRFCService.Domain.UB> ubs)
        {
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.UBToSAP uToS = new BenQGuru.eMES.SAPRFCService.UBToSAP(this.DataProvider);

            try
            {

                r = uToS.PostUBToSAP(ubs);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }

            return r;

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


        protected bool CheckDataStatus(string CarInvNo, string InvNo)
        {
            //1，检查SAP单据是否被Pedding
            //object[] invline_objs = _WarehouseFacade.GetInvoicesDetailByInvNo(InvNo);
            //if (invline_objs == null)
            //{
            //    WebInfoPublish.Publish(this, "SAP单据号错误", this.languageComponent1);
            //    return false;
            //}

            //for (int i = 0; i < invline_objs.Length; i++)
            //{
            //    InvoicesDetail invline = invline_objs[i] as InvoicesDetail;
            //    if (invline.Status == SAP_STATUS.SAP_Pedding)
            //    {
            //        WebInfoPublish.Publish(this, "SAP单据号被冻结，不能执行操作", this.languageComponent1);
            //        return false;
            //    }
            //}
            //2，判断发货箱单头是否ClosePackingList:箱单完成
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            object carinvno_obj = _WarehouseFacade.GetCartoninvoices(CarInvNo);
            if (carinvno_obj == null)
            {
                WebInfoPublish.Publish(this, "发货箱单头错误", this.languageComponent1);
                return false;
            }
            CARTONINVOICES carinvno = carinvno_obj as CARTONINVOICES;
            if (carinvno.STATUS != CartonInvoices_STATUS.Status_ClosePackingList)
            {
                WebInfoPublish.Publish(this, "发货箱单状态不是箱单完成", this.languageComponent1);
                return false;
            }

            return true;
        }
        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            PickInfo p = (PickInfo)obj;



            return new string[]{
                                 p.CARINVNO,
                                p.PICKNO,
                           this.dd.ContainsKey(p.STATUS) ? this.dd[p.STATUS] : "",
                                 p.INVNO,

                                 p.ORDERNO,
                                p.StorageCode,
                                 p.ReceiverUser,
                                  p.ReceiverAddr,
                               FormatHelper.ToDateString(p.Plan_Date),
                        
                               p.PLANGIDATE,
                               p.GFCONTRACTNO,
                               p.GFFLAG,
                                p.OANO,
                                FormatHelper.ToDateString(p.Packing_List_Date),
                               FormatHelper.ToTimeString(p.Packing_List_Time),
                                FormatHelper.ToDateString(p.Shipping_Mark_Date),
                                FormatHelper.ToTimeString(p.Shipping_Mark_Time),
                               p.GROSS_WEIGHT.ToString(),
                              p.VOLUME
                             
                               };
        }

        private string PageVirtualHostRoot
        {
            get
            {
                return string.Format("{0}{1}"
                    , this.Page.Request.Url.Segments[0]
                    , this.Page.Request.Url.Segments[1]);
            }
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"CARINVNO",
                                    "PICKNO",
                                    "STATUS",
                                    "INVNO",

	                                "ORDERNO",
                                    "StorageCode123",
                                    "ReceiverUser",	
                                    "ReceiverAddr",
                                    "PlanDate",
                                    "PLANGIDATE",
                                    "GFCONTRACTNO",
                                    "GFFLAG",
                                    "OANO",
                                    "PackingListDate",
                                    "PackingListTime",
                                    "ShippingMarkDate",	
                                    "ShippingMarkTime",
                                    "GROSSWEIGHT",
                                    "VOLUME",
                                  };


        }

        #endregion



        protected void drpStorageInTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }

    #region CellRegion,Exception,XlsPackage

    class CalStorageQException : Exception
    {
        public CalStorageQException() { }
        public CalStorageQException(string message)
            : base(message)
        {

        }
    }

    class SAPException : Exception
    {
        public SAPException() { }
        public SAPException(string message)
            : base(message)
        {

        }


    }



    class VerticalRegion : CellRegion
    {
        public VerticalRegion(int beginRow, int endRow, int col)
            : base(beginRow, endRow, col, col)
        {

        }
    }

    class HorizontalRange : CellRegion
    {
        public HorizontalRange(int row, int beginCol, int endCol)
            : base(row, row, beginCol, endCol)
        {
        }
    }

    abstract class CellRegion
    {
        public CellRegion(int beginRow, int endRow, int beginCol, int endCol)
        {
            this.beginRow = beginRow;
            this.endRow = endRow;
            this.beginCol = beginCol;
            this.endCol = endCol;
        }

        public CellRangeAddress GetRegion()
        {

            CellRangeAddress region = new CellRangeAddress(this.beginRow,
                                          this.endRow,
                                          this.beginCol,
                                          this.endCol);
            return region;

        }

        public int Row { get { return beginRow; } }
        public int Col
        {
            get { return beginCol; }
        }

        private int beginRow;
        private int endRow;
        private int beginCol;
        private int endCol;
    }
    class XlsPackage
    {
        private Dictionary<string, ISheet> sheets;
        private HSSFWorkbook hssfworkbook;
        private IRow row;
        private SheetConfig sheetConfig;
        private ISheet sheet;
        public XlsPackage()
        {
            hssfworkbook = new HSSFWorkbook();
            this.sheets = new Dictionary<string, ISheet>();

            this.sheetConfig = new SheetConfig(this.hssfworkbook);
        }


        public void InsertImg(string path)
        {
            HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
            //create the anchor
            HSSFClientAnchor anchor;
            // anchor = new HSSFClientAnchor(0, 0, 0, 255, 0, 2, 4, 4);
            //  anchor = new HSSFClientAnchor(0, 0, 0, 255, 0, 0, 5, 4);
            //anchor = new HSSFClientAnchor(0, 0, 0, 255, 0, 0, 4, 4);
            anchor = new HSSFClientAnchor(0, 0, 0, 255, 0, 0, 3, 3);
            anchor.AnchorType = AnchorType.MoveAndResize;// AnchorType.MoveDontResize|
            //load the picture and get the picture index in the workbook


            HSSFPicture picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(path, hssfworkbook));
            //Reset the image to the original size.

            picture.LineStyle = LineStyle.None;

        }

        public static int LoadImage(string path, HSSFWorkbook wb)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            return wb.AddPicture(buffer, PictureType.JPEG);

        }

        public void CreateSheet(string name)
        {

            if (this.sheets.ContainsKey(name))
                throw new Exception("已存在相同名字的sheet!");
            this.sheets[name] = hssfworkbook.CreateSheet(name);
            this.sheet = sheets[name];
        }

        public void ToSheet(string name)
        {
            this.sheet = sheets[name];
        }

        public ISheet Sheet { get { return sheet; } }
        public void NewRow(int rowNum)
        {
            this.row = this.sheet.CreateRow(rowNum);
        }

        public void Cell(int col, string value, ICellStyle style)
        {
            ICell cell = CreateCell(col, style);
            cell.SetCellValue(value);
        }

        public void Cell(int col, int value, ICellStyle style)
        {
            ICell cell = CreateCell(col, style);
            cell.SetCellValue(value);
        }

        public void Cell(int col, IRichTextString rich)
        {

            ICell cell = CreateCell(col, Normal);
            cell.SetCellValue(rich);
        }


        private ICell CreateCell(int col, ICellStyle style)
        {
            ICell cell = row.CreateCell(col);
            cell.CellStyle = style;
            return cell;
        }

        public void Cell(CellRegion region, string value, ICellStyle style, bool isBorder)
        {
            ICell cell = row.CreateCell(region.Col);
            cell.CellStyle = style;
            CellRangeAddress address = region.GetRegion();
            sheet.AddMergedRegion(address);
            cell.SetCellValue(value);
            if (isBorder)
                SetBorder(address);

        }



        public void Cell(CellRegion region, IRichTextString value, ICellStyle style, bool isBorder)
        {
            ICell cell = row.CreateCell(region.Col);
            CellRangeAddress address = region.GetRegion();
            sheet.AddMergedRegion(address);
            cell.SetCellValue(value);
            if (isBorder)
                SetBorder(address);

        }

        public void SetBorder(CellRangeAddress region)
        {
            ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(region, NPOI.SS.UserModel.BorderStyle.Thin, NPOI.HSSF.Util.HSSFColor.Black.Index);
        }

        public void SetColumnWidth(int col, int width)
        {
            sheet.SetColumnWidth(col, width * 256);
        }

        public void SetRowHeight(short height)
        {
            this.row.Height = height;
        }


        public void Save(Stream s)
        {
            this.hssfworkbook.Write(s);
        }

        public ICellStyle Normal { get { return sheetConfig.StyleNormal; } }
        public ICellStyle Warn { get { return sheetConfig.StyleWarn; } }
        public IFont Red { get { return sheetConfig.Red; } }
        public ICellStyle Title { get { return sheetConfig.StyleTitle; } }
        public IFont Green { get { return sheetConfig.Green; } }
        public IFont Black { get { return sheetConfig.Black; } }

        public ICellStyle WarnWithBorder
        {
            get
            {
                ICellStyle style = sheetConfig.StyleWarn;
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BottomBorderColor = HSSFColor.Black.Index;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.LeftBorderColor = HSSFColor.Black.Index;
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.TopBorderColor = HSSFColor.Black.Index;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.RightBorderColor = HSSFColor.Black.Index;
                return style;

            }
        }
        public ICellStyle NormalWithBorder
        {
            get
            {
                ICellStyle style = sheetConfig.StyleNormal;
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BottomBorderColor = HSSFColor.Black.Index;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.LeftBorderColor = HSSFColor.Black.Index;
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.TopBorderColor = HSSFColor.Black.Index;
                style.WrapText = true;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.RightBorderColor = HSSFColor.Black.Index;
                return style;
            }
        }

        public ICellStyle NormalWithTopBorder
        {
            get
            {
                ICellStyle style = sheetConfig.StyleNormal;

                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.TopBorderColor = HSSFColor.Black.Index;

                return style;
            }
        }

        private class SheetConfig
        {

            private HSSFWorkbook hssfworkbook;

            public SheetConfig(HSSFWorkbook hssfworkbook)
            {
                this.hssfworkbook = hssfworkbook;
            }

            public HSSFWorkbook SheetBook { get { return hssfworkbook; } }

            public readonly ICellStyle Normal;
            public readonly ICellStyle Warn;

            private ICellStyle DefaultNormal()
            {

                ICellStyle style = hssfworkbook.CreateCellStyle();
                style.Alignment = HorizontalAlignment.Center;
                style.VerticalAlignment = VerticalAlignment.Center;
                style.SetFont(Black);
                return style;

            }

            private ICellStyle DefaultWarn()
            {

                ICellStyle style = hssfworkbook.CreateCellStyle();
                style.Alignment = HorizontalAlignment.Center;
                style.VerticalAlignment = VerticalAlignment.Center;

                style.SetFont(Red);
                return style;
            }

            public IFont Green
            {
                get
                {
                    IFont font = hssfworkbook.CreateFont();
                    font.FontHeightInPoints = 10;
                    font.Color = NPOI.HSSF.Util.HSSFColor.Green.Index;
                    return font;
                }
            }

            public IFont Red
            {
                get
                {
                    IFont font = hssfworkbook.CreateFont();
                    font.FontHeightInPoints = 12;
                    font.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;
                    return font;
                }
            }

            public IFont Black
            {
                get
                {
                    IFont font = hssfworkbook.CreateFont();
                    font.FontHeightInPoints = 12;
                    font.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    return font;
                }
            }

            public ICellStyle DefaultTitleStyle()
            {
                IFont font = hssfworkbook.CreateFont();
                font.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
                font.Boldweight = 30;
                font.FontHeightInPoints = 14;
                ICellStyle style = hssfworkbook.CreateCellStyle();
                style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                style.BottomBorderColor = HSSFColor.Black.Index;
                style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                style.LeftBorderColor = HSSFColor.Black.Index;
                style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                style.TopBorderColor = HSSFColor.Black.Index;
                style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                style.RightBorderColor = HSSFColor.Black.Index;

                style.Alignment = HorizontalAlignment.Center;
                style.VerticalAlignment = VerticalAlignment.Center;
                style.SetFont(font);
                return style;
            }


            public ICellStyle StyleTitle
            {
                get
                {

                    return DefaultTitleStyle();
                }
            }

            public ICellStyle StyleNormal
            {
                get
                {
                    return DefaultNormal();
                }
            }

            public ICellStyle StyleWarn { get { return DefaultWarn(); } }
        }
    }
    #endregion
}
