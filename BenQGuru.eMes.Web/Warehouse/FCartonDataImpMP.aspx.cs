using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;

using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.Web.WarehouseWeb.ImportData;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FCartonDataImpMP : BaseMPageNew
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        protected System.Web.UI.WebControls.Label lblTitle;
        protected System.Web.UI.WebControls.Label lblBOMDownloadTitle;
        protected System.Web.UI.HtmlControls.HtmlInputButton cmdReturn;
        private System.ComponentModel.IContainer components;
        private BenQGuru.eMES.Material.MaterialFacade _Facade = null;

        // private GridHelperNew gridHelper = null;
        private static string ASN = string.Empty;
        private ButtonHelper buttonHelper = null;
        protected System.Web.UI.WebControls.TextBox ErrorLog;
        private string ImportXMLPath = string.Empty;
        private string InputType = string.Empty;
        private string guid = "";
        private WarehouseFacade _facade = null;
        private MOModel.ItemFacade _itemfacade = null;
        private BenQGuru.eMES.Material.InventoryFacade _invfacade = null;
        private BenQGuru.eMES.Material.InventoryFacade inventoryFacade = null;
        protected void Page_Load(object sender, System.EventArgs e)
        {

            InitHander();
            this.cmdEnter.Attributes.Add("onclick", "return Check();");
            if (!IsPostBack)
            {

                string fileType = "CartonImport";
                if (this.languageComponent1.Language.ToString() == "CHT")
                {
                    fileType = fileType + "_CHT";
                }
                else if (this.languageComponent1.Language.ToString() == "ENU")
                {
                    fileType = fileType + "_ENU";
                }
                aFileDownLoad.HRef = string.Format(@"{0}download\{1}.xls", this.VirtualHostRoot, fileType);
                //// 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                // 初始化界面UI
                ASN = this.GetRequestParam("ASN");
                this.txtASNSTNOQuery.Text = ASN;
                txtPageEdit.Text = this.GetRequestParam("Page");
                if (string.IsNullOrEmpty(txtPageEdit.Text))
                {
                    cmdReturn.Visible = false;
                }
                this.InitUI();
                this.InitWebGrid();

            }
        }

        public string UploadedFileName
        {
            get
            {
                if (this.ViewState["UploadedFileName"] != null)
                {
                    return this.ViewState["UploadedFileName"].ToString();
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["UploadedFileName"] = value;
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
        protected void Page_Init(object sender, System.EventArgs e)
        {
            PostBackTrigger tri = new PostBackTrigger();
            tri.ControlID = this.cmdEnter.ID;
            (this.FindControl("up1") as UpdatePanel).Triggers.Add(tri);
        }
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);


            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.buttonHelper = new ButtonHelper(this);


            //this.gridHelper.Grid.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
        }

        /// <summary>
        /// 初始化Grid的栏位
        /// </summary>
        protected override void InitWebGrid()
        {

            base.InitWebGrid();
            //			this.gridWebGrid.Columns.FromKey("UserPassword").Hidden = true;
            this.gridHelper.AddColumn("BigCartonNO", "大箱号", null);
            this.gridHelper.AddColumn("SmallCartonNO", "小箱号", null);
            this.gridHelper.AddColumn("ISEdit", "能否编辑", null);
            this.gridHelper.AddColumn("CartonNo11", "箱号编码", null);//CartonNo11
            this.gridHelper.AddColumn("DQLotNO", "鼎桥批次号", null);
            this.gridHelper.AddColumn("ASNStatus", "状态", null);
            this.gridHelper.AddColumn("DQMaterialNo", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("DQMaterialNoDesc", "鼎桥物料编码描述", null);
            this.gridHelper.AddColumn("VendorMCODE", "供应商物料编码", null);
            this.gridHelper.AddColumn("VendorMCODEDesc", "供应商物料编码描述", null);
            this.gridHelper.AddColumn("ASNQTY", "来料数量", null);
            this.gridHelper.AddColumn("ReceiveQTY11", "已接收数量", null);//ReceiveQTY11
            this.gridHelper.AddColumn("ImportQTY", "已入库数量", null);
            this.gridHelper.AddColumn("MUOM", "单位", null);
            this.gridHelper.AddColumn("ProDate", "生产日期", null);
            this.gridHelper.AddColumn("VendorLotNo", "供应商批次", null);
            this.gridHelper.AddColumn("MControlType11", "物料管控类型", null);//MControlType11


            this.gridHelper.AddDataColumn("ASNCreateTime", "入库指令创建时间", 20);
            this.gridHelper.AddDataColumn("ReformCount", "现场整改数量", 20);
            this.gridHelper.AddDataColumn("ReturnCount", "退换货数量", 20);
            this.gridHelper.AddDataColumn("RejectCount", "初检拒收数量", 20);


            this.gridHelper.AddColumn("CartonMemo", "箱单备注", null);
            this.gridHelper.AddColumn("stline", "line", null);
            this.gridHelper.AddColumn("stno", "stno", null);
            this.gridHelper.AddLinkColumn("SelectEdit", "编辑", null);
            //this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            //this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridWebGrid.Columns.FromKey("stline").Hidden = true;
            this.gridWebGrid.Columns.FromKey("stno").Hidden = true;

            this.gridHelper.AddDefaultColumn(false, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            if (!string.IsNullOrEmpty(this.txtASNSTNOQuery.Text))
            {
                this.gridHelper.RequestData();
            }
        }
        protected override DataRow GetGridRow(object obj)
        {

            DataRow row = this.DtSource.NewRow();
            row["BigCartonNO"] = ((AsndetailEX)obj).Cartonbigseq;
            row["SmallCartonNO"] = ((AsndetailEX)obj).Cartonseq;


            string stno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNSTNOQuery.Text));
            _facade = new WarehouseFacade(base.DataProvider);

            Asn asn = (Asn)_facade.GetAsn(txtASNSTNOQuery.Text.Trim().ToUpper());
            bool result = CheckImpCondition();
            if (result)
            {
                if (_facade == null)
                {
                    _facade = new WarehouseFacade(base.DataProvider);
                }
                int count = _facade.QueryPODistriQTYcount(stno.ToUpper().Trim(), ((AsndetailEX)obj).Stline.ToString());
                if (count > 1)
                {
                    row["ISEdit"] = "是";
                }
                else
                {
                    row["ISEdit"] = "否";
                }

            }
            else
            {
                row["ISEdit"] = "否";
            }



            row["CartonNo11"] = ((AsndetailEX)obj).Cartonno;
            row["DQLotNO"] = ((AsndetailEX)obj).Lotno;


            //if (((AsndetailEX)obj).Status == ASNDetail_STATUS.ASNDetail_Close)
            //    row["ASNStatus"] = this.languageComponent1.GetLanguage("Close11");
            //else
            row["ASNStatus"] = this.GetStatusName(((AsndetailEX)obj).InitreceiveStatus); //this.languageComponent1.GetLanguage(((AsndetailEX)obj).Status).ControlText;
            row["DQMaterialNo"] = ((AsndetailEX)obj).DqmCode;
            row["DQMaterialNoDesc"] = ((AsndetailEX)obj).MDesc;
            row["VendorMCODE"] = asn.StType == "UB" ? ((AsndetailEX)obj).CustmCode : ((AsndetailEX)obj).VEndormCode;
            row["VendorMCODEDesc"] = ((Asndetail)obj).VEndormCodeDesc;
            row["ASNQTY"] = ((AsndetailEX)obj).Qty.ToString();
            row["ReceiveQTY11"] = ((AsndetailEX)obj).ReceiveQty.ToString();
            row["ImportQTY"] = ((AsndetailEX)obj).ActQty.ToString();
            row["MUOM"] = ((AsndetailEX)obj).Unit;
            row["ProDate"] = FormatHelper.ToDateString(((AsndetailEX)obj).Production_Date);
            row["VendorLotNo"] = ((AsndetailEX)obj).Supplier_lotno;
            if (string.IsNullOrEmpty(((AsndetailEX)obj).MControlType))
                row["MControlType11"] = string.Empty;
            else
                row["MControlType11"] = this.languageComponent1.GetLanguage(((AsndetailEX)obj).MControlType).ControlText;



            if (asn != null)
            {
                string createTime = asn.CDate.ToString() + " " + FormatHelper.ToTimeString(asn.CTime);
                row["ASNCreateTime"] = createTime;

            }

            BenQGuru.eMES.IQC.IQCFacade iqcFacade = new BenQGuru.eMES.IQC.IQCFacade(base.DataProvider);
            row["ReformCount"] = iqcFacade.ReformQtyTotalWithStNoLine(((AsndetailEX)obj).Stno, ((AsndetailEX)obj).Stline);
            row["ReturnCount"] = iqcFacade.ReturnQtyTotalWithStNoLine(((AsndetailEX)obj).Stno, ((AsndetailEX)obj).Stline);
            string status = ((AsndetailEX)obj).Status;

            if (status == ASNHeadStatus.Release || status == ASNHeadStatus.WaitReceive || status == ASNHeadStatus.Receive)
            {
                row["RejectCount"] = 0;

            }
            else
            {

                row["RejectCount"] = ((AsndetailEX)obj).Qty - ((AsndetailEX)obj).ReceiveQty;
            }


            row["CartonMemo"] = ((AsndetailEX)obj).Remark1;
            row["stline"] = ((AsndetailEX)obj).Stline.ToString();

            //row["MaintainUser"] = ((Asn)obj).MaintainUser;
            //row["MaintainDate"] = FormatHelper.ToDateString(((Asn)obj).MaintainDate);






            return row;

        }

        protected override void cmdQuery_Click(object sender, System.EventArgs e)
        {
            #region 入库指令号头
            //string stno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNSTNOQuery.Text));
            //if (_facade == null)
            //    _facade = new WarehouseFacade(base.DataProvider);
            //object asnobj = _facade.GetAsn(stno);
            //if (asnobj == null)
            //{
            //    WebInfoPublish.Publish(this, "缺少入库指令号头！", this.languageComponent1);
            //    return;
            //} 
            bool result = CheckImpCondition();
            if (result)
            {
                return;
            }
            #endregion

            this.gridHelper.RequestData();
            if (this.gridHelper2 != null)
                this.gridHelper2.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new WarehouseFacade(base.DataProvider);
            }
            return this._facade.QueryASNDetailBystno(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNSTNOQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new WarehouseFacade(base.DataProvider);
            }
            return this._facade.ASNDetailBystnoCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNSTNOQuery.Text))

                );
        }


        protected void cmdEnter_ServerClick(object sender, System.EventArgs e)
        {

            if (!CheckImpCondition())
                return;

            this.GetAllItem();



            this.gridHelper.RequestData();


        }
        /// <summary>
        /// 导入前逻辑检查
        /// </summary>
        protected bool CheckImpCondition()
        {
            string stno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNSTNOQuery.Text));
            if (_facade == null)
                _facade = new WarehouseFacade(base.DataProvider);
            object asnobj = _facade.GetAsn(stno);
            if (asnobj != null)
            {

                Asn asn = asnobj as Asn;
                if (asn.Status != ASN_STATUS.ASN_Release)
                {
                    WebInfoPublish.Publish(this, "$Error_ASN_STATUS_NOT_RELEASE", this.languageComponent1);
                    //ExceptionManager.Raise(this.GetType().BaseType, "$Error_ASN_STATUS_NOT_RELEASE");
                    return false;
                }
            }
            else
            {
                WebInfoPublish.Publish(this, "缺少入库指令号头！", this.languageComponent1);
                //ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
                return false;
            }
            return true;

        }
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            string stno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNSTNOQuery.Text));
            object objStline = row.Items.FindItemByKey("stline").Value;
            string strStline = string.Empty;
            string DQMCode = row.Items.FindItemByKey("DQMaterialNo").Value.ToString();
            if (objStline != null)
            {
                strStline = objStline.ToString();
            }
            bool result = CheckImpCondition();
            if (result)
            {
                if (_facade == null)
                {
                    _facade = new WarehouseFacade(base.DataProvider);
                }
                int count = _facade.QueryPODistriQTYcount(stno.ToUpper().Trim(), strStline);
                if (count > 1)
                {
                    this.Response.Redirect(this.MakeRedirectUrl("./FPODistributionQTYMP.aspx", new string[] { "Stno", "Stline", "DQMCode" }, new string[] { stno, strStline, DQMCode }));
                }
                else
                {
                    ExceptionManager.Raise(this.GetType().BaseType, "$Error_Only_One_Data");
                }

            }

        }

        private void GetAllItem()
        {
            //try
            //{

            string fileName = string.Empty;


            #region  new
            #region File
            if (inventoryFacade == null)
            {
                inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(base.DataProvider);
            }
            string upfileName = "";
            BenQGuru.eMES.Domain.Warehouse.InvDoc doc = new BenQGuru.eMES.Domain.Warehouse.InvDoc();
            if (this.DownLoadPathBom.PostedFile != null)
            {
                string stno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNSTNOQuery.Text));
                HttpPostedFile postedFile = this.DownLoadPathBom.PostedFile;
                string path = Server.MapPath(this.VirtualHostRoot + "InvDoc/" + "箱单导入/");
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);
                //string CARINVNO = row.Items.FindItemByKey("CARINVNO").Text;
                doc.InvDocNo = stno;
                doc.InvDocType = "CartonDataImp";
                string[] part = postedFile.FileName.Split(new char[] { '/', '\\' });

                doc.DocType = Path.GetExtension(postedFile.FileName);
                doc.DocName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                doc.DocSize = postedFile.ContentLength / 1024;
                doc.UpUser = GetUserCode();
                doc.UpfileDate = FormatHelper.TODateInt(DateTime.Now);
                doc.Dirname = "箱单导入";
                doc.MaintainUser = this.GetUserCode();
                doc.MaintainDate = dbDateTime.DBDate;
                doc.MaintainTime = dbDateTime.DBTime;
                doc.ServerFileName = doc.DocName + "_" + stno + DateTime.Now.ToString("yyyyMMddhhmmss") + doc.DocType;// +".xlsx";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileName = doc.ServerFileName;

                upfileName = path + fileName;
                this.DownLoadPathBom.PostedFile.SaveAs(upfileName);
                //inventoryFacade.AddInvDoc(doc);
                //WebInfoPublish.Publish(this, "导入成功！", this.languageComponent1);
            }
            else
            {
                WebInfoPublish.PublishInfo(this, "导入文件不能为空", this.languageComponent1);
            }
            #endregion
            this.UploadedFileName = upfileName;
            #endregion




            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (this.ViewState["UploadedFileName"] == null)
            {
                WebInfoPublish.Publish(this, "$Error_UploadFileIsEmpty", this.languageComponent1);
                //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileIsEmpty");
            }
            fileName = this.ViewState["UploadedFileName"].ToString();


            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheet("Sheet1");


            //读取EXCEL格式文件
            System.Data.DataTable dt = GetExcelData(fileName);


            if (dt == null || dt.Rows.Count == 0)
            {
                WebInfoPublish.Publish(this, "工作表无数据！", this.languageComponent1);//add by sam
                return;
            }
            if (_facade == null)
            {
                _facade = new WarehouseFacade(base.DataProvider);
            }
            if (_itemfacade == null)
            {
                _itemfacade = new BenQGuru.eMES.MOModel.ItemFacade(base.DataProvider);
            }
            if (_invfacade == null)
            {
                _invfacade = new InventoryFacade(base.DataProvider);
            }

            object asnobj = _facade.GetAsn(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNSTNOQuery.Text)));
            if (asnobj == null)
            {
                // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
                WebInfoPublish.Publish(this, "缺少入库指令号头1！", this.languageComponent1);
                return;
            }


            Asn asn = asnobj as Asn;

            //_facade.CompareItemPlanQtyAndDetailQTY(asn.Invno);



            #region   检查箱单逻辑
            string stTyle = "0";
            //判断产生条件
            //1,TBLASN.STTYPE)不是：PGIR: PGI退料、SCTR:生产退料时，必须检查箱单中订单号等于入库指令号对应的SAP单据号(TBLASN.INVNO)
            if (!(asn.StType == SAP_ImportType.SAP_PGIR) && !(asn.StType == SAP_ImportType.SAP_SCTR))
            {
                if (asn.Invno != dt.Rows[3][9].ToString().Trim().ToUpper())
                {
                    this.DataProvider.RollbackTransaction();
                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_INVNO_NOT_UNIFORMITY");
                    WebInfoPublish.Publish(this, "$Error_INVNO_NOT_UNIFORMITY", this.languageComponent1);
                    return;
                }
            }
            //2,TBLASN.STTYPE)是：POR: PO入库、YFR:研发入库、PD:盘点时；检查箱中生产日期，供应商批次号，生成鼎桥批次号
            if (asn.StType == SAP_ImportType.SAP_POR || asn.StType == SAP_ImportType.SAP_YFR || asn.StType == SAP_ImportType.SAP_PD)
            {
                stTyle = "1";   //POR: PO入库、YFR:研发入库、PD:盘点时
            }
            else
            {
                stTyle = "2";   //非(POR: PO入库、YFR:研发入库、PD:盘点时)
            }
            #endregion
            #region 更新asn表的装箱单号和出具日期,毛重,体积
            //更新asn表的装箱单号和出具日期,毛重,体积
            decimal weight = 0;
            decimal volume = 0;
            for (int t = 5; t < dt.Rows.Count - 1; t++)
            {
                if (dt.Rows[t][0].ToString().Trim().ToUpper().Length > 4 && dt.Rows[t][0].ToString().Trim().ToUpper().Substring(0, 5) == "TOTAL")
                {
                    break;
                }
                if (!string.IsNullOrEmpty(dt.Rows[t][10].ToString().Trim()))
                {
                    weight += decimal.Parse(dt.Rows[t][10].ToString().Trim());//毛重,

                }
                if (!string.IsNullOrEmpty(dt.Rows[t][13].ToString().Trim()))
                {
                    volume += decimal.Parse(dt.Rows[t][13].ToString().Trim());
                }
            }




            //删除stno下-asndetail，asndetailitem，asndetailsn 数据
            _facade.DeleteSTNoDatabySTNO(asn.Stno);

            //asn.Provide_Date = Helper.FormatHelper.TODateInt(dt.Rows[0][9].ToString().Trim());  /////////////////////////////


            if (string.IsNullOrEmpty(dt.Rows[0][9].ToString().Trim()))
            {
                // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
                WebInfoPublish.Publish(this, "出具日期不能为空", this.languageComponent1);
                return;
            }
            asn.Provide_Date = dt.Rows[0][9].ToString().Trim();//出具日期

            #region Packinglistno
            string Packinglistno = "";
            string str1 = dt.Rows[1][4].ToString().Trim().ToUpper();
            if (!string.IsNullOrEmpty(str1))
            {
                Packinglistno = str1.Substring(5).Trim();//str1.LastIndexOf(":")
            }
            if (string.IsNullOrEmpty(Packinglistno))
            {
                Packinglistno = dt.Rows[1][5].ToString().Trim().ToUpper();
            }
            #endregion


            asn.Packinglistno = Packinglistno;// dt.Rows[1][5].ToString().Trim().ToUpper();//装箱单号

            asn.Gross_weight = weight;
            asn.Volume = volume.ToString();
            asn.MaintainDate = dbTime.DBDate;
            asn.MaintainTime = dbTime.DBTime;
            asn.MaintainUser = this.GetUserCode();
            _facade.UpdateAsn(asn);


            #endregion
            #region   完善dt ，将excel中合并格内容补齐
            //完善dt

            int count = 0;
            if (!string.IsNullOrEmpty(dt.Rows[5][0].ToString().Trim()))
            {
                for (int s = 6; s <= dt.Rows.Count - 1; s++)
                {

                    if (dt.Rows[s][0].ToString().Trim().ToUpper().Length > 4 && dt.Rows[s][0].ToString().Trim().ToUpper().Substring(0, 5) == "TOTAL")
                    {
                        count = s - 1;
                        break;
                    }
                    if (string.IsNullOrEmpty(dt.Rows[s][0].ToString().Trim()))
                    {
                        dt.Rows[s][0] = dt.Rows[s - 1][0].ToString().Trim();

                    }

                }
            }
            else
            {
                this.DataProvider.RollbackTransaction();
                //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
                WebInfoPublish.Publish(this, "Excel表【大箱】第一行不能有空", this.languageComponent1);
                return;
            }

            if (count == 0)
            {
                throw new Exception("未找到结尾！");
            }

            #endregion
            #region 检查excel中需要的栏位是否有值
            for (int s = 5; s < count + 1; s++)
            {
                if (string.IsNullOrEmpty(dt.Rows[s][0].ToString().Trim()))
                {
                    string hang = Convert.ToString(s + 5);
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, hang + "大箱号码为空", this.languageComponent1);
                    return;
                }

                if (string.IsNullOrEmpty(dt.Rows[s][7].ToString().Trim()))
                {
                    string hang = Convert.ToString(s + 5);
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, hang + "数量不能为空", this.languageComponent1);
                    return;
                }


            }
            #endregion

            #region 检查excel中的sn，不能与库存中的sn重复，不能与正在入库中的sn重复，excel中不能有重复
            //excel中不能重复
            //Hashtable htsn = new Hashtable();

            #endregion
            //将数据存入tblasndetail
            string message = string.Empty;
            if (!CheckSNNotRepeat(sheet, asn, count, out message))
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, message, this.languageComponent1);
                return;
            }
            int seq = 0;
            Hashtable htable = new Hashtable();  //记录鼎桥批次号

            object objLot = null;

            try
            {
                this.DataProvider.BeginTransaction();
                for (int h = 5; h < count + 1; h++)
                {

                    string DHYCode = dt.Rows[h][3].ToString().Trim().ToUpper();
                    if (DHYCode.Length >= 2)
                    {
                        string ddd1 = DHYCode.Substring(0, 2);
                        if (ddd1 == "..")
                        {

                            continue;
                        }
                    }

                    Asndetail asnDetail = _facade.CreateNewAsndetail();
                    //根据excel表中大箱号-----同一个号码的小箱号排列顺序
                    asnDetail.Stno = asn.Stno;
                    seq = seq + 1;
                    asnDetail.Stline = seq.ToString();
                    asnDetail.Status = ASNDetail_STATUS.ASNDetail_Release;
                    asnDetail.Cartonno = "";
                    asnDetail.Cartonbigseq = dt.Rows[h][0].ToString().Trim().ToUpper();
                    asnDetail.Cartonseq = dt.Rows[h][2].ToString().Trim().ToUpper();
                    //根据鼎桥物料编码去SAPDETAIL中获取华为物料号，SAP物料号，收货方物料号
                    object str = null;

                    if (!string.IsNullOrEmpty(dt.Rows[h][4].ToString().Trim()))
                    {
                        str = _facade.GetMaterialFromDQMCode(dt.Rows[h][4].ToString().Trim().ToUpper());
                        if (str != null)
                        {
                            asnDetail.CustmCode = string.Empty;
                            asnDetail.MCode = (str as Domain.MOModel.Material).MCode;          //SAP物料号
                            asnDetail.ReceivemCode = string.Empty;
                        }
                        else
                        {
                            this.DataProvider.RollbackTransaction();
                            //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
                            WebInfoPublish.Publish(this, "找不到鼎桥物料号！" + dt.Rows[h][4].ToString().Trim(), this.languageComponent1);
                            return;
                        }
                    }
                    else
                    {
                        this.DataProvider.RollbackTransaction();
                        //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
                        WebInfoPublish.Publish(this, "EXCEL表中鼎桥物料号不能为空", this.languageComponent1);
                        return;
                    }

                    asnDetail.DqmCode = dt.Rows[h][4].ToString().Trim().ToUpper();
                    asnDetail.Qty = int.Parse(dt.Rows[h][7].ToString().Trim().ToUpper());
                    asnDetail.ReceiveQty = 0;
                    asnDetail.ActQty = 0;
                    asnDetail.QcpassQty = 0;

                    #region  检查单件管控料，箱单中sn的个数是否与数量相等
                    object materobj = _itemfacade.GetMaterial(asnDetail.MCode);
                    if (materobj == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
                        WebInfoPublish.Publish(this, "物料表没有物料：" + asnDetail.MCode, this.languageComponent1);
                        return;

                    }
                    if (materobj != null)
                    {
                        Domain.MOModel.Material mmm = (Domain.MOModel.Material)materobj;
                        asnDetail.MDesc = !string.IsNullOrEmpty(mmm.MchshortDesc) ? mmm.MchshortDesc : " ";
                        asnDetail.Unit = !string.IsNullOrEmpty(mmm.Muom) ? mmm.Muom : " ";
                    }
                    else
                    {
                        asnDetail.Unit = " ";
                        asnDetail.MDesc = " ";
                    }

                    if (asn.StType == SAP_ImportType.SAP_JCR)
                    {
                        InvoicesDetail invDetail = (InvoicesDetail)_invfacade.GetInvoicesDetail(asn.Invno);

                        if (!_facade.CompareStorageAvailableQty(invDetail.FromStorageCode, asnDetail.DqmCode, asnDetail.Qty))
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "物料：" + asnDetail.MCode + "在出库库位:" + invDetail.FromStorageCode + "中的可用数量不足！", this.languageComponent1);
                            return;
                        }
                    }
                    Domain.MOModel.Material mater = materobj as Domain.MOModel.Material;
                    if (string.IsNullOrEmpty(mater.MCONTROLTYPE))
                    {
                        this.DataProvider.RollbackTransaction();
                        // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
                        WebInfoPublish.Publish(this, "物料：" + asnDetail.MCode + "没有维护管控类型", this.languageComponent1);
                        return;
                    }
                    IRow row = sheet.GetRow(h + 1);
                    ICell cellSn = row.GetCell(9);

                    string str_SN = cellSn.StringCellValue.Trim().ToUpper();
                    if (!string.IsNullOrEmpty(str_SN))
                    {

                        //string[] strs = str_SN.Split();

                        List<string> strs = new List<string>();
                        MatchCollection matches = Regex.Matches(str_SN, @"(\S+)");
                        for (int i = 0; i < matches.Count; i++)
                        {
                            Match match = matches[i];


                            string sn1 = match.Groups[1].Value;

                            strs.Add(sn1);
                        }


                        if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                        {

                            if (int.Parse(dt.Rows[h][7].ToString().Trim()) != strs.ToArray().Length)
                            {
                                foreach (string s in strs)
                                {
                                    Log.Error("---------------------------------------------------------" + s + "--------------------------------------------"); ;
                                }
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "$Error_SN_QTY_IS_NOT_uniformity", this.languageComponent1);
                                //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_SN_QTY_IS_NOT_uniformity");
                                return;
                            }
                        }
                        // JCR:检测返工入库、BLR:不良品入库、CAR: CLAIM入库时 检查库位
                        //for (int i = 0; i < strs.Length; i++)
                        //{
                        //    if (asn.StType == SAP_ImportType.SAP_JCR)
                        //        break;
                        //    bool check = false;
                        //    if (asn.StType == SAP_ImportType.SAP_BLR || asn.StType == SAP_ImportType.SAP_CAR)
                        //        check = true;
                        //    bool result = _facade.GetLotNOInformationFromSNforcheck(strs[i].Trim(), check, asn.FromstorageCode);
                        //    if (!result)
                        //    {
                        //        this.DataProvider.RollbackTransaction();
                        //        WebInfoPublish.Publish(this, "$Error_SN_NOT_EXIST_INSTORAGE", this.languageComponent1);
                        //        //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_SN_NOT_EXIST_INSTORAGE");
                        //        return;
                        //    }
                        //}
                    }
                    else
                    {
                        //箱单中没有sn
                        if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "$Error_SN_IS_NULL", this.languageComponent1);
                            // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_SN_IS_NULL");
                            return;
                        }
                    }

                    #endregion

                    if (stTyle == "1")      //获取生产日期，供应商批次，鼎桥批次不同(PO入库，研发入库，盘点)
                    {
                        #region 取值

                        if (htable.ContainsKey(dt.Rows[h][4].ToString().Trim().ToUpper()))
                        {
                            //如果已经有鼎桥编码，那么就用原有的批次号
                            asnDetail.Lotno = htable[dt.Rows[h][4].ToString().Trim().ToUpper()].ToString();
                        }
                        else
                        {
                            //如果没有，新生车批次号
                            //根据日期排列鼎桥批次号

                            objLot = null;
                            objLot = _facade.GetNewLotNO("TDLOT", dbTime.DBDate.ToString());
                            Serialbook serbook = _facade.CreateNewSerialbook();
                            if (objLot == null)
                            {

                                asnDetail.Lotno = "TDLOT" + dbTime.DBDate.ToString() + "0001";
                                //塞数据到tblserialbook
                                serbook.SNprefix = "TDLOT" + dbTime.DBDate.ToString();
                                serbook.MAXSerial = "2";
                                serbook.MUser = this.GetUserCode();
                                serbook.MDate = dbTime.DBDate;
                                serbook.MTime = dbTime.DBTime;


                                _facade.AddSerialbook(serbook);


                            }
                            else
                            {
                                string MAXNO = (objLot as Serialbook).MAXSerial;
                                string SNNO = (objLot as Serialbook).SNprefix;
                                asnDetail.Lotno = SNNO + Convert.ToString(MAXNO).PadLeft(4, '0');

                                //更新tblserialbook
                                serbook.SNprefix = SNNO;
                                serbook.MAXSerial = Convert.ToString((int.Parse(MAXNO) + 1));
                                serbook.MUser = this.GetUserCode();
                                serbook.MDate = dbTime.DBDate;
                                serbook.MTime = dbTime.DBTime;
                                _facade.UpdateSerialbook(serbook);

                            }
                            htable.Add(dt.Rows[h][4].ToString().Trim().ToUpper(), asnDetail.Lotno);
                        }
                        if (dt.Columns.Count > 15)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[h][15].ToString()))
                            {
                                if (!Regex.IsMatch(dt.Rows[h][15].ToString(), @"\d{4}/\d{1,2}/\d{1,2}"))
                                {
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, dt.Rows[h][15].ToString() + "格式错误 日期格式必须是####/##/##", this.languageComponent1);
                                    return;
                                }
                                asnDetail.Production_Date = Helper.FormatHelper.TODateInt(dt.Rows[h][15].ToString().Trim().ToUpper().Replace("年", "/").Replace("月", "/").Replace("日", "/"));
                            }
                            else
                            {
                                asnDetail.Production_Date = Helper.FormatHelper.TODateInt(DateTime.Now);
                            }
                        }
                        if (dt.Columns.Count > 16)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[h][16].ToString()))
                            {
                                asnDetail.Supplier_lotno = dt.Rows[h][16].ToString().Trim().ToUpper();
                            }
                        }
                        //asnDetail.StorageageDate = string.Empty;
                        #endregion
                    }
                    if (stTyle == "2")   //不是PO入库，研发入库和盘点
                    {

                        #region 不同类型取值
                        if (asn.StType == SAP_ImportType.SAP_DNR)  //退货入库
                        {
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                            {


                                List<string> strs = new List<string>();
                                MatchCollection matches = Regex.Matches(str_SN, @"(\S+)");
                                for (int i = 0; i < matches.Count; i++)
                                {
                                    Match match = matches[i];


                                    string sn1 = match.Groups[1].Value;

                                    strs.Add(sn1);
                                }

                                object DNRobj = _facade.GetLotProductDateInfoFromSN(strs.ToArray(), asn.Invno);
                                if (DNRobj == null)
                                {

                                    asnDetail.Lotno = CreateLotNo(dbTime); ;
                                    asnDetail.Supplier_lotno = " ";
                                    asnDetail.StorageageDate = FormatHelper.TODateInt(DateTime.Now);
                                }
                                else
                                {
                                    Pickdetailmaterial pikmater = DNRobj as Pickdetailmaterial;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    asnDetail.Production_Date = pikmater.Production_Date;
                                    asnDetail.Supplier_lotno = pikmater.Supplier_lotno;
                                    asnDetail.StorageageDate = pikmater.StorageageDate;
                                }


                            }
                            else
                            {
                                // 批管控
                                object orderobj = _facade.GetFirstCheckInPickMaterialFromDQMCode(asn.Invno, asnDetail.DqmCode);
                                if (orderobj == null)
                                {

                                    asnDetail.Lotno = CreateLotNo(dbTime); ;
                                    asnDetail.Supplier_lotno = " ";
                                    asnDetail.StorageageDate = FormatHelper.TODateInt(DateTime.Now);
                                }
                                else
                                {
                                    Pickdetailmaterial pikmater = orderobj as Pickdetailmaterial;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    asnDetail.Production_Date = pikmater.Production_Date;
                                    asnDetail.Supplier_lotno = pikmater.Supplier_lotno;
                                    asnDetail.StorageageDate = pikmater.StorageageDate;
                                }

                            }

                        }
                        if (asn.StType == SAP_ImportType.SAP_UB)  //调拨
                        {
                            if (string.IsNullOrEmpty(asn.Invno))
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "$Error_INVNO_IS_NULL", this.languageComponent1);
                                //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_INVNO_IS_NULL");
                                return;
                            }
                            object UB_obj = _invfacade.GetPickByInvNo(asn.Invno);
                            if (UB_obj == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "拣货任务令中无数据", this.languageComponent1);
                                //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_PICK_NO_DATA");
                                return;
                            }
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                            {
                                //单件管控


                                List<string> strs = new List<string>();
                                MatchCollection matches = Regex.Matches(str_SN, @"(\S+)");
                                for (int i = 0; i < matches.Count; i++)
                                {
                                    Match match = matches[i];


                                    string sn1 = match.Groups[1].Value;

                                    strs.Add(sn1);
                                }


                                object DNRobj = _facade.GetLotNOInformationFromSN(strs[0].Trim(), ((Pick)UB_obj).PickNo);
                                if (DNRobj == null)
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "SN号" + strs[0].Trim() + "不在" + ((Pick)UB_obj).PickNo + "拣货任务令中！", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_PICKDetail_NO_FIND_INFO");
                                    return;
                                }
                                else
                                {
                                    Pickdetailmaterial pikmater = DNRobj as Pickdetailmaterial;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    asnDetail.Production_Date = pikmater.Production_Date;
                                    asnDetail.Supplier_lotno = pikmater.Supplier_lotno;
                                    asnDetail.StorageageDate = pikmater.StorageageDate;
                                }


                            }
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_LOT || mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_NOCONTROL)
                            {
                                //批管控
                                if (string.IsNullOrEmpty(asnDetail.DqmCode))
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_DQMCODE_IS_NULL", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_DQMCODE_IS_NULL");
                                    return;
                                }
                                object DNRobj = _facade.GetLotNOInformationFromDQMCODE(asnDetail.DqmCode, ((Pick)UB_obj).PickNo);
                                if (DNRobj == null)
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_PICKDetail_NO_FIND_INFO", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_PICKDetail_NO_FIND_INFO");
                                    return;
                                }
                                else
                                {
                                    Pickdetailmaterial pikmater = DNRobj as Pickdetailmaterial;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    asnDetail.Production_Date = pikmater.Production_Date;
                                    asnDetail.Supplier_lotno = pikmater.Supplier_lotno;
                                    asnDetail.StorageageDate = pikmater.StorageageDate;
                                }
                            }

                        }
                        if (asn.StType == SAP_ImportType.SAP_JCR)  //检测返工入库
                        {

                            if (!string.IsNullOrEmpty(dt.Rows[h][15].ToString()))
                            {
                                if (!Regex.IsMatch(dt.Rows[h][15].ToString(), @"\d{4}/\d{1,2}/\d{1,2}"))
                                {
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, dt.Rows[h][15].ToString() + "格式错误 日期格式必须是####/##/##", this.languageComponent1);
                                    return;
                                }
                                asnDetail.Production_Date = Helper.FormatHelper.TODateInt(dt.Rows[h][15].ToString().Trim().ToUpper().Replace("年", "/").Replace("月", "/").Replace("日", "/"));
                            }
                            else
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "检测返工的生产日期必填!", this.languageComponent1);
                                return;
                            }


                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                            {


                                //单件管控
                                string strDNR_SN = dt.Rows[h][9].ToString().Trim().ToUpper();


                                List<string> strs = new List<string>();
                                MatchCollection matches = Regex.Matches(strDNR_SN, @"(\S+)");
                                for (int i = 0; i < matches.Count; i++)
                                {
                                    Match match = matches[i];


                                    string sn1 = match.Groups[1].Value;

                                    strs.Add(sn1);
                                }

                                Invoices invoice = (Invoices)_invfacade.GetInvoices(asn.Invno);
                                InvoicesDetail invoD = (InvoicesDetail)_invfacade.GetInvoicesDetail(asn.Invno);
                                if (invoD == null)
                                {
                                    this.DataProvider.RollbackTransaction();
                                    throw new Exception(asn.Invno + ":" + "单据行项目不存在");
                                }
                                if (invoice == null)
                                {
                                    this.DataProvider.RollbackTransaction();
                                    throw new Exception(asn.Invno + ":" + "单据不存在");
                                }
                                List<string> snss = new List<string>();

                                foreach (string sn in strs)
                                {
                                    snss.Add(sn);
                                }
                                object DNRobj = _facade.GetLotNOInformationFromSNforJCR(snss);

                                if (DNRobj == null)
                                {

                                    //asnDetail.Production_Date = FormatHelper.TODateInt(DateTime.Now);
                                    asnDetail.Lotno = CreateLotNo(dbTime); ;
                                    asnDetail.Supplier_lotno = " ";
                                    asnDetail.StorageageDate = FormatHelper.TODateInt(DateTime.Now);

                                }
                                else
                                {
                                    StorageDetail pikmater = DNRobj as StorageDetail;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    //asnDetail.Production_Date = pikmater.ProductionDate;
                                    asnDetail.Supplier_lotno = pikmater.SupplierLotNo;
                                    asnDetail.StorageageDate = pikmater.StorageAgeDate;
                                }


                            }
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_LOT || mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_NOCONTROL)
                            {
                                if (string.IsNullOrEmpty(asnDetail.DqmCode))
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_DQMCODE_IS_NULL", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_DQMCODE_IS_NULL");
                                    return;
                                }
                                if (string.IsNullOrEmpty(asn.ReworkapplyUser))
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_ReworkapplyUser_IS_NULL", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_ReworkapplyUser_IS_NULL");
                                    return;
                                }
                                InvoicesDetail[] invs = _facade.GetGetInvoicesDetailFromInvNo(asn.Invno);
                                if (invs.Length <= 0)
                                {
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, asn.Invno + "发货单据不存在！", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_StorageCode_IS_NULL");
                                    return;
                                }

                                if (string.IsNullOrEmpty(invs[0].FromStorageCode))
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_StorageCode_IS_NULL", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_StorageCode_IS_NULL");
                                    return;
                                }

                                object DNRobj = _facade.GetLotNOInformationforJCR(asnDetail.DqmCode, asn.ReworkapplyUser, invs[0].FromStorageCode);
                                if (DNRobj == null)
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "返工申请人:" + asn.ReworkapplyUser + " 申请的物料：" + asnDetail.DqmCode + "在库存中不存在！", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_PICKDetail_NO_FIND_INFO");
                                    return;
                                }
                                else
                                {
                                    StorageDetail pikmater = DNRobj as StorageDetail;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    //asnDetail.Production_Date = pikmater.ProductionDate;
                                    asnDetail.Supplier_lotno = pikmater.SupplierLotNo;
                                    asnDetail.StorageageDate = pikmater.StorageAgeDate;
                                }
                            }

                        }
                        if (asn.StType == SAP_ImportType.SAP_BLR || asn.StType == SAP_ImportType.SAP_CAR)  //不良品，CAR,CLAIM入库
                        {
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                            {
                                //单件管控


                                List<string> strs = new List<string>();
                                MatchCollection matches = Regex.Matches(str_SN, @"(\S+)");
                                for (int i = 0; i < matches.Count; i++)
                                {
                                    Match match = matches[i];


                                    string sn1 = match.Groups[1].Value;

                                    strs.Add(sn1);
                                }
                                List<string> snss = new List<string>();

                                foreach (string sn in strs)
                                {
                                    snss.Add(sn);
                                }
                                object DNRobj = _facade.GetLotNOInformationFromSNforJCR(snss);

                                if (DNRobj == null)
                                {


                                    asnDetail.Lotno = CreateLotNo(dbTime); ;
                                    asnDetail.Supplier_lotno = " ";
                                    asnDetail.StorageageDate = FormatHelper.TODateInt(DateTime.Now);

                                }
                                else
                                {
                                    StorageDetail pikmater = DNRobj as StorageDetail;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    asnDetail.Production_Date = pikmater.ProductionDate;
                                    asnDetail.Supplier_lotno = pikmater.SupplierLotNo;
                                    asnDetail.StorageageDate = pikmater.StorageAgeDate;
                                }

                            }
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_LOT || mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_NOCONTROL)
                            {
                                if (string.IsNullOrEmpty(asnDetail.DqmCode))
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_DQMCODE_IS_NULL", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_DQMCODE_IS_NULL");
                                    return;
                                }
                                InvoicesDetail[] invs = _facade.GetGetInvoicesDetailFromInvNo(asn.Invno);
                                if (invs.Length <= 0)
                                {
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, asn.Invno + "发货单据不存在！", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_StorageCode_IS_NULL");
                                    return;
                                }
                                if (string.IsNullOrEmpty(invs[0].FromStorageCode))
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_StorageCode_IS_NULL", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_StorageCode_IS_NULL");
                                    return;
                                }

                                object DNRobj = _facade.GetLotNOInformationforJCR(asnDetail.DqmCode, string.Empty, invs[0].FromStorageCode);
                                if (DNRobj == null)
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_PICKDetail_NO_FIND_INFO", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_PICKDetail_NO_FIND_INFO");
                                    return;
                                }
                                else
                                {
                                    StorageDetail pikmater = DNRobj as StorageDetail;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    asnDetail.Production_Date = pikmater.ProductionDate;
                                    asnDetail.Supplier_lotno = pikmater.SupplierLotNo;
                                    asnDetail.StorageageDate = pikmater.StorageAgeDate;
                                }
                            }

                        }
                        if (asn.StType == SAP_ImportType.SAP_PGIR)  //退料
                        {
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                            {
                                //单件管控
                                if (string.IsNullOrEmpty(asn.Pickno))
                                {
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_PICKNO_IS_NULL", this.languageComponent1);
                                    // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_PICKNO_IS_NULL");
                                    return;
                                }


                                List<string> strs = new List<string>();
                                MatchCollection matches = Regex.Matches(str_SN, @"(\S+)");
                                for (int i = 0; i < matches.Count; i++)
                                {
                                    Match match = matches[i];


                                    string sn1 = match.Groups[1].Value;

                                    strs.Add(sn1);
                                }
                                object DNRobj = _facade.GetLotNOInformationFromSN(strs[0].Trim(), asn.Pickno);
                                if (DNRobj == null)
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_PICKDetail_NO_FIND_INFO", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_PICKDetail_NO_FIND_INFO");
                                    return;
                                }
                                else
                                {
                                    Pickdetailmaterial pikmater = DNRobj as Pickdetailmaterial;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    asnDetail.Production_Date = pikmater.Production_Date;
                                    asnDetail.Supplier_lotno = pikmater.Supplier_lotno;
                                    asnDetail.StorageageDate = pikmater.StorageageDate;
                                }

                            }
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_LOT || mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_NOCONTROL)
                            {
                                if (string.IsNullOrEmpty(asnDetail.DqmCode))
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_DQMCODE_IS_NULL", this.languageComponent1);
                                    // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_DQMCODE_IS_NULL");
                                    return;
                                }
                                if (string.IsNullOrEmpty(asn.Pickno))
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_PICKNO_IS_NULL", this.languageComponent1);
                                    //BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_PICKNO_IS_NULL");
                                    return;
                                }
                                object DNRobj = _facade.GetLotNOInformationFromDQMCODE(asnDetail.DqmCode, asn.Pickno);
                                if (DNRobj == null)
                                {
                                    //没有找到信息
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "$Error_PICKDetail_NO_FIND_INFO", this.languageComponent1);
                                    // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_PICKDetail_NO_FIND_INFO");
                                    return;
                                }
                                else
                                {
                                    Pickdetailmaterial pikmater = DNRobj as Pickdetailmaterial;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    asnDetail.Production_Date = pikmater.Production_Date;
                                    asnDetail.Supplier_lotno = pikmater.Supplier_lotno;
                                    asnDetail.StorageageDate = pikmater.StorageageDate;
                                }
                            }
                        }
                        if (asn.StType == SAP_ImportType.SAP_SCTR)  //生产退料
                        {
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                            {

                                List<string> strs = new List<string>();
                                MatchCollection matches = Regex.Matches(str_SN, @"(\S+)");
                                for (int i = 0; i < matches.Count; i++)
                                {
                                    Match match = matches[i];


                                    string sn1 = match.Groups[1].Value;

                                    strs.Add(sn1);
                                }

                                object DNRobj = _facade.GetLotForSCTR(asn.Stno, strs.ToArray());
                                if (DNRobj == null)
                                {
                                    asnDetail.Lotno = CreateLotNo(dbTime); ;

                                    asnDetail.Supplier_lotno = " ";
                                    asnDetail.StorageageDate = FormatHelper.TODateInt(DateTime.Now);
                                }
                                else
                                {
                                    Pickdetailmaterial pikmater = DNRobj as Pickdetailmaterial;
                                    asnDetail.Lotno = pikmater.Lotno;
                                    asnDetail.Production_Date = pikmater.Production_Date;
                                    asnDetail.Supplier_lotno = pikmater.Supplier_lotno;
                                    asnDetail.StorageageDate = pikmater.StorageageDate;
                                }

                            }
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_LOT || mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_NOCONTROL)
                            {
                                //批管控
                                object sctrobj = _facade.GetLotForSCTRFromDQMCode(asn.Stno, asnDetail.DqmCode);
                                if (sctrobj == null)
                                {
                                    asnDetail.Lotno = CreateLotNo(dbTime); ;

                                    asnDetail.Supplier_lotno = " ";
                                    asnDetail.StorageageDate = FormatHelper.TODateInt(DateTime.Now);
                                }
                                else
                                {
                                    Pickdetailmaterial trans = sctrobj as Pickdetailmaterial;
                                    asnDetail.Lotno = trans.Lotno;
                                    asnDetail.Production_Date = trans.Production_Date;
                                    asnDetail.Supplier_lotno = trans.Supplier_lotno;
                                    asnDetail.StorageageDate = trans.StorageageDate;
                                }
                            }
                            if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_NOCONTROL)
                            {
                                asnDetail.Production_Date = dbTime.DBDate;
                                asnDetail.StorageageDate = dbTime.DBDate;
                            }

                        }
                        #endregion
                    }

                    asnDetail.Remark1 = dt.Rows[h][14].ToString().Trim().ToUpper();
                    asnDetail.CUser = this.GetUserCode();
                    asnDetail.CDate = dbTime.DBDate;
                    asnDetail.CTime = dbTime.DBTime;
                    asnDetail.MaintainDate = dbTime.DBDate;
                    asnDetail.MaintainTime = dbTime.DBTime;
                    asnDetail.MaintainUser = this.GetUserCode();
                    DHYCode = dt.Rows[h][3].ToString().Trim().ToUpper();
                    if (DHYCode.Length >= 2)
                    {
                        string ddd1 = DHYCode.Substring(0, 2);
                        if (ddd1 == "..")
                        {

                            continue;
                        }
                    }
                    asnDetail.VEndormCode = dt.Rows[h][3].ToString().Trim().ToUpper();


                    asnDetail.VEndormCodeDesc = dt.Rows[h][5].ToString().Trim().ToUpper();
                    if (asnDetail.StorageageDate == 0)
                        asnDetail.StorageageDate = dbTime.DBDate;
                    asnDetail.InitreceiveStatus = string.Empty;
                    asnDetail.InitreceiveDesc = string.Empty;

                    _facade.AddAsndetail(asnDetail);

                    #region   插入TBLasndetailSN


                    if (!string.IsNullOrEmpty(str_SN))
                    {
                        List<string> strs = new List<string>();
                        MatchCollection matches = Regex.Matches(str_SN, @"(\S+)");
                        for (int i = 0; i < matches.Count; i++)
                        {
                            Match match = matches[i];


                            string sn1 = match.Groups[1].Value;

                            strs.Add(sn1);
                        }

                        Asn[] asnObjs = _facade.GetASNIncludesThisSNs(strs);
                        foreach (Asn objAsn in asnObjs)
                        {
                            if (objAsn.Status != ASNHeadStatus.ReceiveRejection && objAsn.Status != ASNHeadStatus.IQCRejection && objAsn.Status != ASNHeadStatus.Close)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "此箱单中的SN以存在于" + objAsn.Stno + ",请检查后重新上传！", this.languageComponent1);
                                // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_SAP_NEED_DATA_ERROR");
                                return;
                            }

                        }

                        //如果
                        for (int i = 0; i < strs.ToArray().Length; i++)
                        {


                            Asndetailsn detailsn = _facade.CreateNewAsndetailsn();
                            detailsn.CDate = dbTime.DBDate;
                            detailsn.CTime = dbTime.DBTime;
                            detailsn.CUser = this.GetUserCode();
                            detailsn.MaintainDate = dbTime.DBDate;
                            detailsn.MaintainTime = dbTime.DBTime;
                            detailsn.MaintainUser = this.GetUserCode();
                            detailsn.Cartonno = string.Empty;
                            detailsn.QcStatus = string.Empty;
                            detailsn.Sn = strs[i].Trim();
                            detailsn.Stline = seq.ToString();
                            detailsn.Stno = asn.Stno;
                            _facade.AddAsndetailsn(detailsn);

                        }
                    }

                    #endregion
                    #region   插入tblasndetailITEM
                    Asndetailitem detailitem = _facade.CreateNewAsndetailitem();
                    detailitem.CDate = dbTime.DBDate;
                    detailitem.CTime = dbTime.DBTime;
                    detailitem.CUser = this.GetUserCode();
                    detailitem.MaintainDate = dbTime.DBDate;
                    detailitem.MaintainTime = dbTime.DBTime;
                    detailitem.MaintainUser = this.GetUserCode();
                    detailitem.Stline = seq.ToString();
                    detailitem.Stno = asn.Stno;
                    detailitem.MCode = asnDetail.MCode;
                    detailitem.DqmCode = asnDetail.DqmCode;
                    //detailitem.ActQty = 0;
                    //detailitem.QcpassQty = 0;
                    //detailitem.ReceiveQty = 0;
                    //查找对应的SAP单
                    //有些入库类型没有invoices数据，不能从这里获取数据
                    //if(asn.in)


                    object[] qtyobjs = _facade.GetSAPNOandLinebyMCODE(asn.Invno, asnDetail.MCode, asnDetail.DqmCode);
                    if (asn.StType != SAP_ImportType.SAP_SCTR && asn.StType != SAP_ImportType.SAP_PGIR)
                    {
                        if (qtyobjs == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "入库需求" + asn.Invno + " 没有相关物料" + asnDetail.MCode + "," + asnDetail.DqmCode + "或者此行已被取消！", this.languageComponent1);
                            // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_SAP_NEED_DATA_ERROR");
                            return;
                        }

                        decimal sub = asnDetail.Qty;
                        for (int i = 0; i < qtyobjs.Length; i++)
                        {
                            InvoicesDetail invdetail = qtyobjs[i] as InvoicesDetail;
                            decimal subNeed = 0;
                            object findNeedQTY_old = _facade.GetNeedImportQtyOLD(invdetail.InvNo, invdetail.InvLine, asnDetail.Stno);  //找这个invoice行已经导入了多少，和判退多少
                            Asndetailitem subItemOld = findNeedQTY_old as Asndetailitem;
                            object findNeedQTY_now = _facade.GetNeedImportQtyNow(invdetail.InvNo, invdetail.InvLine, asnDetail.Stno);  //找这个invoice行已经导入了多少，和判退多少
                            Asndetailitem subItemNow = findNeedQTY_now as Asndetailitem;

                            subNeed = invdetail.PlanQty - subItemOld.Qty + (subItemOld.Qty - subItemOld.ReceiveQty) + (subItemOld.ReceiveQty - subItemOld.QcpassQty);
                            subNeed = subNeed - subItemNow.Qty;

                            if (subNeed == 0)
                                continue;

                            //如果箱数量大于需求数量差---进行拆分
                            if (sub > subNeed)
                            {
                                sub = sub - subNeed;  //  sub是剩余的
                                detailitem.Qty = subNeed;
                                detailitem.Invline = invdetail.InvLine.ToString();
                                detailitem.Invno = invdetail.InvNo;
                                detailitem.ActQty = detailitem.Qty;
                                detailitem.QcpassQty = detailitem.Qty;
                                detailitem.ReceiveQty = detailitem.Qty;
                                _facade.AddAsndetailitem(detailitem);
                            }

                            //如果箱单数量小于等于需求数量差--直接填入
                            else
                            {

                                detailitem.Qty = sub;
                                detailitem.Invline = invdetail.InvLine.ToString();
                                detailitem.Invno = invdetail.InvNo;
                                detailitem.ActQty = detailitem.Qty;
                                detailitem.QcpassQty = detailitem.Qty;
                                detailitem.ReceiveQty = detailitem.Qty;
                                _facade.AddAsndetailitem(detailitem);
                                sub = 0;

                            }
                            if (sub == 0)
                            {
                                break;
                            }
                        }
                        //如果sub>0，说明导入数量过多，报错
                        if (sub > 0)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "$Error_SAP_NEED_DATA_ERROR", this.languageComponent1);
                            // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_SAP_NEED_DATA_ERROR");
                            return;
                        }
                    }

                    if (asn.StType == SAP_ImportType.SAP_SCTR)
                    {

                        detailitem.Qty = asnDetail.Qty;
                        detailitem.Invline = asnDetail.Stline;
                        detailitem.Invno = asnDetail.Stno;
                        detailitem.ActQty = detailitem.Qty;
                        detailitem.QcpassQty = detailitem.Qty;
                        detailitem.ReceiveQty = detailitem.Qty;
                        _facade.AddAsndetailitem(detailitem);

                    }


                    #endregion
                }
                #region add by sam
                //this.DownLoadPathBom.PostedFile.SaveAs(upfileName);
                inventoryFacade.AddInvDoc(doc);
                #endregion

                this.DataProvider.CommitTransaction();

                WebInfoPublish.Publish(this, "导入成功！", this.languageComponent1);//add by sam

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
            }



        }

        private bool CheckSNNotRepeat(NPOI.SS.UserModel.ISheet sheet, Asn asn, int count, out string message)
        {



            message = string.Empty;
            for (int h = 5; h < count + 1; h++)
            {

                IRow row = sheet.GetRow(h + 1);
                ICell cellSN = row.GetCell(9);
                Dictionary<string, string> dicSN = new Dictionary<string, string>();


                string str_SN = cellSN.StringCellValue.Trim().ToUpper();

                List<string> strs = new List<string>();
                MatchCollection matches = Regex.Matches(str_SN, @"(\S+)");
                for (int i = 0; i < matches.Count; i++)
                {
                    Match match = matches[i];


                    string sn1 = match.Groups[1].Value;

                    strs.Add(sn1);
                }


                for (int i = 0; i < strs.Count; i++)
                {


                    if (!dicSN.ContainsKey(strs[i].Trim()))
                    {
                        dicSN.Add(strs[i].Trim(), strs[i].Trim());
                    }
                    else
                    {
                        message = "SN号码：" + strs[i] + "重复！";
                        return false;
                    }
                    if (asn.StType == SAP_ImportType.SAP_YFR || asn.StType == SAP_ImportType.SAP_POR || asn.StType == SAP_ImportType.SAP_PD || asn.StType == SAP_ImportType.SAP_DNR || asn.StType == SAP_ImportType.SAP_PGIR)
                    {

                        object[] objs_afterc = _facade.CheckAfterCloseSN(strs[i].Trim());
                        if (objs_afterc != null && objs_afterc.Length > 0)
                        {

                            message = strs[i] + "已经在库存中";
                            return false;

                        }
                    }


                }
            }
            return true;
        }

        private string CreateLotNo(DBDateTime dbTime)
        {
            Serialbook objLot = (Serialbook)_facade.GetNewLotNO("TDLOT", dbTime.DBDate.ToString());
            Serialbook serbook = _facade.CreateNewSerialbook();
            if (objLot == null)
            {


                //塞数据到tblserialbook
                serbook.SNprefix = "TDLOT" + dbTime.DBDate.ToString();
                serbook.MAXSerial = "2";
                serbook.MUser = this.GetUserCode();
                serbook.MDate = dbTime.DBDate;
                serbook.MTime = dbTime.DBTime;


                _facade.AddSerialbook(serbook);
                return "TDLOT" + dbTime.DBDate.ToString() + "0001";

            }
            else
            {
                string MAXNO = (objLot as Serialbook).MAXSerial;
                string SNNO = (objLot as Serialbook).SNprefix;


                //更新tblserialbook
                serbook.SNprefix = SNNO;
                serbook.MAXSerial = Convert.ToString((int.Parse(MAXNO) + 1));
                serbook.MUser = this.GetUserCode();
                serbook.MDate = dbTime.DBDate;
                serbook.MTime = dbTime.DBTime;
                _facade.UpdateSerialbook(serbook);
                return SNNO + Convert.ToString(MAXNO).PadLeft(4, '0');
            }
        }


        #region 读取文件
        /// <summary> 
        /// 获取指定路径、指定工作簿名称的Excel数据:取第一个sheet的数据 
        /// </summary> 
        /// <param name="FilePath">文件存储路径</param> 
        /// <param name="WorkSheetName">工作簿名称</param> 
        /// <returns>如果争取找到了数据会返回一个完整的Table，否则返回异常</returns> 
        public DataTable GetExcelData(string astrFileName)
        {

            return GetExcelData(astrFileName, "Sheet1");
        }

        /// <summary> 
        /// 返回指定文件所包含的工作簿列表;如果有WorkSheet，就返回以工作簿名字命名的ArrayList，否则返回空 
        /// </summary> 
        /// <param name="strFilePath">要获取的Excel</param> 
        /// <returns>如果有WorkSheet，就返回以工作簿名字命名的ArrayList，否则返回空</returns> 
        public ArrayList GetExcelWorkSheets(string strFilePath)
        {
            ArrayList alTables = new ArrayList();
            OleDbConnection odn = new OleDbConnection(GetExcelConnection(strFilePath));
            odn.Open();
            DataTable dt = new DataTable();
            dt = odn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt == null)
            {
                throw new Exception("无法获取指定Excel的架构。");
            }
            foreach (DataRow dr in dt.Rows)
            {
                string tempName = dr["Table_Name"].ToString();
                int iDolarIndex = tempName.IndexOf('$');
                if (iDolarIndex > 0)
                {
                    tempName = tempName.Substring(0, iDolarIndex);
                }
                //修正了Excel2003中某些工作薄名称为汉字的表无法正确识别的BUG。 
                if (tempName[0] == '\'')
                {
                    if (tempName[tempName.Length - 1] == '\'')
                    {
                        tempName = tempName.Substring(1, tempName.Length - 2);
                    }
                    else
                    {
                        tempName = tempName.Substring(1, tempName.Length - 1);
                    }
                }
                if (!alTables.Contains(tempName))
                {
                    alTables.Add(tempName);
                }
            }
            odn.Close();
            if (alTables.Count == 0)
            {
                return null;
            }
            return alTables;
        }


        /// <summary> 
        /// 获取指定路径、指定工作簿名称的Excel数据 
        /// </summary> 
        /// <param name="FilePath">文件存储路径</param> 
        /// <param name="WorkSheetName">工作簿名称</param> 
        /// <returns>如果争取找到了数据会返回一个完整的Table，否则返回异常</returns> 
        public DataTable GetExcelData(string FilePath, string WorkSheetName)
        {
            DataTable dtExcel = new DataTable();
            OleDbConnection con = new OleDbConnection(GetExcelConnection(FilePath));
            OleDbDataAdapter adapter = new OleDbDataAdapter("Select * from [" + WorkSheetName + "$]", con);
            //读取 
            con.Open();
            //adapter.FillSchema(dtExcel, SchemaType.Mapped);
            adapter.Fill(dtExcel);
            con.Close();
            dtExcel.TableName = WorkSheetName;
            //返回 
            return dtExcel;
        }


        /// <summary> 
        /// 获取链接字符串 
        /// </summary> 
        /// <param name="strFilePath"></param> 
        /// <returns></returns> 
        public string GetExcelConnection(string strFilePath)
        {
            if (!File.Exists(strFilePath))
            {
                throw new Exception("指定的Excel文件不存在！");
            }
            // return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + ";Extended properties=\"Excel 8.0;Imex=1;HDR=Yes;\"";
            return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1;\"";

            //@"Provider=Microsoft.Jet.OLEDB.4.0;" + 
            //@"Data Source=" + strFilePath + ";" + 
            //@"Extended Properties=" + Convert.ToChar(34).ToString() + 
            //@"Excel 8.0;" + "Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString(); 
        }
        #endregion

        #region  no use
        //private void RequestData()
        //{
        //    this.InitWebGrid();   
        //    //ImportExcel imXls = new ImportExcel(this.UploadedFileName, this.InputType, this.XmlHelper.GridBuilder, this.XmlHelper.NotAllowNullField);

        //    ImportExcel imXls = new ImportExcel(this.UploadedFileName, this.InputType, this.XmlHelper.GridBuilder, this.XmlHelper.GridBuilder);


        //    DataTable dt = imXls.XlaDataTable;

        //    dt.Columns.Add(this.gridHelper.CheckColumnKey);
        //    dt.Columns.Add("GUID");
        //    dt.Columns.Add("ImportResult");
        //    //foreach (DataRow row in dt.Rows)
        //    //{
        //    //    row[this.gridHelper.CheckColumnKey] =;
        //    //}
        //    //for (int i = 0; i < dt.Rows.Count; i++)
        //    //{

        //    //    //object[] objs = new object[dt.Columns.Count + 1];
        //    //    //Array.Copy(dt.Rows[i].ItemArray, 0, objs, 1, dt.Columns.Count);
        //    //    //Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow(objs);
        //    //    //this.gridHelper.Grid.Rows.Add(row);


        //    //}
        //    dt.AcceptChanges();
        //    this.gridWebGrid.DataSource = dt;
        //    this.gridWebGrid.DataBind();

        //    //this.lblCount.Text = dt.Rows.Count.ToString();
        //}



        protected void cmdAdd_ServerClick(object sender, EventArgs e)
        {

            //this.XmlHelper.SelectedImportType = InputType;
            //cmdQuery_ServerClick(this, e);

            //ArrayList importArray = new ArrayList();
            //foreach (GridRecord row in this.gridWebGrid.Rows)
            //{
            //    importArray.Add(row);
            //}
            //if (importArray.Count == 0)
            //{
            //    return;
            //}

            //DataTable dt = this.GetImportDT(importArray);




            //ImportData.ImportDateEngine importEngine = new BenQGuru.eMES.Web.WarehouseWeb.ImportData.ImportDateEngine(
            //    base.DataProvider, languageComponent1, this.InputType, dt, this.GetUserCode(), importArray);

            //importEngine.fromPage = this.Page;

            //#region 验证数据有效性
            //try
            //{
            //    /*added by jessie lee, 2005/11/30,
            //     * 操作时间过长时添加进度条*/

            //    //this.Page.Response.Write("<div id='mydiv' >");
            //    //this.Page.Response.Write("_");
            //    //this.Page.Response.Write("</div>");
            //    //this.Page.Response.Write("<script>mydiv.innerText = '';</script>");
            //    //this.Page.Response.Write("<script language=javascript>;");
            //    //this.Page.Response.Write("var dots = 0;var dotmax = 10;function ShowWait()");
            //    //this.Page.Response.Write("{var output; output = '正在验证导入数据的有效性,请稍后';dots++;if(dots>=dotmax)dots=1;");
            //    //this.Page.Response.Write("for(var x = 0;x < dots;x++){output += '·';}mydiv.innerText =  output;}");
            //    //this.Page.Response.Write("function StartShowWait(){mydiv.style.visibility = 'visible'; ");
            //    //this.Page.Response.Write("window.setInterval('ShowWait()',1000);}");
            //    //this.Page.Response.Write("function HideWait(){mydiv.style.display = 'none';");
            //    //this.Page.Response.Write("window.clearInterval();}");
            //    //this.Page.Response.Write("StartShowWait();</script>");
            //    //this.Page.Response.Flush();

            //    importEngine.CheckDataValid();

            //    //this.Page.Response.Write("<script language=javascript>HideWait();</script>");
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //finally
            //{
            //    //this.Page.Response.Write("<script language=javascript>HideWait();</script>");
            //}
            //#endregion

            //#region 导入数据
            //try
            //{
            //    //this.Page.Response.Write("<div id='mydiv2' >");
            //    //this.Page.Response.Write("_");
            //    //this.Page.Response.Write("</div>");
            //    //this.Page.Response.Write("<script>mydiv2.innerText = '';</script>");
            //    //this.Page.Response.Write("<script language=javascript>;");
            //    //this.Page.Response.Write("var dots = 0;var dotmax = 10;function ShowWait()");
            //    //this.Page.Response.Write("{var output; output = '正在导入数据,请稍后';dots++;if(dots>=dotmax)dots=1;");
            //    //this.Page.Response.Write("for(var x = 0;x < dots;x++){output += '·';}mydiv2.innerText =  output;}");
            //    //this.Page.Response.Write("function StartShowWait(){mydiv2.style.visibility = 'visible'; ");
            //    //this.Page.Response.Write("window.setInterval('ShowWait()',1000);}");
            //    //this.Page.Response.Write("function HideWait(){mydiv2.style.display = 'none';");
            //    //this.Page.Response.Write("window.clearInterval();}");
            //    //this.Page.Response.Write("StartShowWait();</script>");
            //    //this.Page.Response.Flush();

            //    bool isRollBack = false;
            //    importEngine.Import(isRollBack);

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    //this.Page.Response.Write("<script language=javascript>HideWait();</script>");
            //}
            //#endregion

            //if (importEngine.ErrorArray != null)
            //{
            //    importEngine.ErrorArray = null;
            //}

        }
        /// <summary>
        /// 根据xml加载dropDownlist
        /// </summary>
        //private void BuildDataTypeDDL()
        //{
        //    this.InputTypeDDL.Items.Clear();
        //    this.InputTypeDDL.Items.Add(new ListItem("", ""));

        //    //			ArrayList htable = this.XmlHelper.ImportType;
        //    ArrayList htable = this.XmlHelper.GetImportType(this.languageComponent1);
        //    if (htable.Count > 0)
        //    {
        //        foreach (DictionaryEntry de in htable)
        //        {
        //            this.InputTypeDDL.Items.Add(
        //                new ListItem(de.Value.ToString(), de.Key.ToString()));
        //        }
        //    }
        //}
        //private DataTable GetImportDT(ArrayList array)
        //{
        //    /* 生成表结构 */
        //    DataTable dt = new DataTable();
        //    dt.Rows.Clear();
        //    dt.Columns.Clear();
        //    ArrayList htable = this.XmlHelper.GridBuilder;
        //    if (htable.Count > 0)
        //    {
        //        foreach (DictionaryEntry de in htable)
        //        {
        //            string key = de.Key.ToString();
        //            dt.Columns.Add(key);
        //        }
        //    }

        //    /* 填数据 */
        //    for (int i = 0; i < array.Count; i++)
        //    {
        //        GridRecord gridRow = array[i] as GridRecord;
        //        DataRow row = dt.NewRow();
        //        if (htable.Count > 0)
        //        {
        //            foreach (DictionaryEntry de in htable)
        //            {
        //                string key = de.Key.ToString();
        //                if (XmlHelper.NeedMatchCtoE(key))
        //                {
        //                    string value1 = GetECode(key, gridRow.Items.FindItemByKey(key).Value.ToString());
        //                    if (gridRow.Items.FindItemByKey(key).Value.ToString().Trim().Length != 0
        //                        && value1.Length == 0)
        //                    {
        //                        ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFile_ContentError");
        //                    }
        //                    else
        //                    {
        //                        row[key] = value1;
        //                    }
        //                }
        //                else
        //                {
        //                    string a = gridRow.Items.FindItemByKey(key).Value.ToString();
        //                    row[key] = gridRow.Items.FindItemByKey(key).Value.ToString();
        //                }
        //            }
        //        }
        //        dt.Rows.Add(row);
        //    }

        //    return dt;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="valueWord"></param>
        /// <returns></returns>
        //private string GetECode(string key, string valueWord)
        //{
        //    return XmlHelper.GetMatchKeyWord(valueWord, XmlHelper.MatchType(key));
        //}

        //protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        //{
        //    WebDataGrid Grid2 = null;
        //    Grid2 = this.gridHelper.Grid;
        //    for (int i = Grid2.Rows.Count - 1; i >= 0; i--)
        //    {
        //        GridRecord row = Grid2.Rows[i];
        //        if (bool.Parse(row.Items[0].ToString())
        //            && (row.Items.FindItemByKey("ImportResult").Value.ToString() == "导入成功"))
        //        {
        //            Grid2.Rows.Remove(Grid2.Rows[i]);
        //        }
        //    }

        //    Grid2.Columns.Remove(Grid2.Columns[0]);


        //}

        //public ImportData.ImportXMLHelper XmlHelper
        //{
        //    get
        //    {
        //        if (this.ViewState["XmlHelper"] != null)
        //        {
        //            return this.ViewState["XmlHelper"] as ImportData.ImportXMLHelper;
        //        }
        //        else
        //        {
        //            return new BenQGuru.eMES.Web.WarehouseWeb.ImportData.ImportXMLHelper(ImportXMLPath);
        //        }
        //    }
        //    set
        //    {
        //        this.ViewState["XmlHelper"] = value;
        //    }
        //}

        //public string InputType
        //{
        //    get
        //    {
        //        if (this.ViewState["InputType"] != null)
        //        {
        //            return this.ViewState["InputType"].ToString();
        //        }
        //        return string.Empty;
        //    }
        //    set
        //    {
        //        this.ViewState["InputType"] = value;
        //    }
        //}
        #endregion


        #region 返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            if (txtPageEdit.Text == "FASNForVendorMP.aspx")
            {


                Response.Redirect(this.MakeRedirectUrl("FASNForVendorMP.aspx",
                           new string[] { "StNo" },
                           new string[] { txtASNSTNOQuery.Text.Trim().ToUpper()
                                        
                                    }));

            }
            else
            {

                Response.Redirect(this.MakeRedirectUrl("FASNForBuyerAndLogisticMP.aspx",
                          new string[] { "StNo" },
                          new string[] { txtASNSTNOQuery.Text.Trim().ToUpper()
                                        
                                    }));


            }
        }
        #endregion

    }
}
