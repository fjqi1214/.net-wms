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
using BenQGuru.eMES.Domain.Warehouse;
using Infragistics.Web.UI.GridControls;
using BenQGuru.eMES.Web.Helper;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FStockCheck : BenQGuru.eMES.Web.Helper.BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = null;
        private BenQGuru.eMES.Material.InventoryFacade _InventoryFacade = null;


        bool isVendor = false;//判断当前用户是否为供应商

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
            tri.ControlID = this.cmdAddImport.ID;
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
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                BenQGuru.eMES.Domain.Warehouse.Storage[] storages = _WarehouseFacade.GetStorageCode();
                this.drpStorageCodeQuery.Items.Add(new ListItem("", ""));
                this.drpStorageCodeEdit.Items.Add(new ListItem("", ""));
                foreach (BenQGuru.eMES.Domain.Warehouse.Storage p in storages)
                {

                    this.drpStorageCodeQuery.Items.Add(new ListItem(p.StorageCode + "-" + p.StorageName, p.StorageCode));
                    this.drpStorageCodeEdit.Items.Add(new ListItem(p.StorageCode + "-" + p.StorageName, p.StorageCode));
                }


                this.drpCheckType.Items.Add(new ListItem("", ""));
                this.drpCheckType.Items.Add(new ListItem("全盘", "All"));
                this.drpCheckType.Items.Add(new ListItem("动盘", "Portion"));
                this.drpCheckTypeEdit.Items.Add(new ListItem("", ""));
                this.drpCheckTypeEdit.Items.Add(new ListItem("全盘", "All"));
                this.drpCheckTypeEdit.Items.Add(new ListItem("动盘", "Portion"));

                string CheckNo = Request.QueryString["CheckCode"];
                string StorageCode = Request.QueryString["StorageCode"];
                if (!string.IsNullOrEmpty(CheckNo))
                    txtStockCheckCodeQuery.Text = CheckNo;

                if (!string.IsNullOrEmpty(StorageCode))
                    drpStorageCodeEdit.SelectedValue = StorageCode;


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
            this.gridHelper.AddColumn("CheckNo", "盘点单号", null);
            this.gridHelper.AddColumn("CheckType", "检查类型", null);
            this.gridHelper.AddColumn("StorageCode", "库位", null);
            this.gridHelper.AddColumn("CDATE", "盘点日期", null);
            this.gridHelper.AddColumn("Status", "状态", null);
            this.gridHelper.AddColumn("CUSER", "创建人", null);
            this.gridHelper.AddColumn("CDATE", "创建日期", null);
            //this.gridHelper.AddLinkColumn("LinkToCartonImport", "导入/查看箱单", null);
            this.gridHelper.AddColumn("CTIME", "创建时间", null);
            this.gridHelper.AddColumn("REMARK1", "备注", null);

            this.gridHelper.AddLinkColumn("LinkToCartonImport", "详细信息", null);
            this.gridHelper.AddLinkColumn("StockCheckOp", "盘点作业", null);
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);





            if (!string.IsNullOrEmpty(txtStockCheckCodeQuery.Text) || !string.IsNullOrEmpty(drpStorageCodeEdit.SelectedValue))
            {
                this.gridHelper.RequestData();

            }
        }

        void drpCheckType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //导入按钮
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;

            if (array.Count != 1)
            {
                WebInfoPublish.Publish(this, "只能选择一行！", this.languageComponent1); return;
            }
            try
            {

                GridRecord row1 = (GridRecord)array[0];
                string checkNo = row1.Items.FindItemByKey("CheckNo").Text;

                BenQGuru.eMES.Domain.Warehouse.StockCheck ssss = (BenQGuru.eMES.Domain.Warehouse.StockCheck)_WarehouseFacade.GetStockCheck(checkNo);
                if (ssss == null)
                {
                    WebInfoPublish.Publish(this, "只有盘点单不存在！", this.languageComponent1); return;
                }
                if (ssss.CheckType != "All")
                {
                    WebInfoPublish.Publish(this, "只有盘点类型为全盘时可用！", this.languageComponent1); return;
                }
                if (ssss.STATUS == "Close")
                {
                    WebInfoPublish.Publish(this, "盘点单已经关闭！", this.languageComponent1); return;
                }


                System.IO.Stream ss = FileUpload1.PostedFile.InputStream;
                string fileName = FileUpload1.PostedFile.FileName;
                IWorkbook workbook = null;




                if (string.IsNullOrEmpty(fileName) || System.IO.Path.GetExtension(fileName).ToLower() != ".xls") // 2007版本
                {
                    WebInfoPublish.Publish(this, "必须是.xls文件！", this.languageComponent1); return;
                }

                try
                {
                    workbook = new HSSFWorkbook(ss);
                }
                catch (Exception ex)
                {
                    WebInfoPublish.Publish(this, "未处理的异常！" + ex.Message, this.languageComponent1); return;
                }
                ISheet sheet = workbook.GetSheetAt(0);
                if (sheet == null)
                {
                    WebInfoPublish.Publish(this, "Excel文件为空！", this.languageComponent1); return;
                }
                IRow row = null;
                row = sheet.GetRow(0);
                ICell cellFirst = row.GetCell(1);
                string cellCheckNo = cellFirst.ToString();
                if (string.IsNullOrEmpty(cellCheckNo) || cellCheckNo != checkNo)
                {
                    WebInfoPublish.Publish(this, "xls文件中的盘点单号必须与所选的盘点单号一致！", this.languageComponent1); return;
                }



                int rowNum = 2;
                int lastRowNum = sheet.LastRowNum;
                List<string> list = new List<string>();
                for (row = sheet.GetRow(2); rowNum <= lastRowNum; rowNum++, row = sheet.GetRow(rowNum))
                {
                    ICell cell2 = row.GetCell(1);
                    string dqmCode = cell2.StringCellValue.Trim();

                    if (string.IsNullOrEmpty(dqmCode))
                    {
                        WebInfoPublish.Publish(this, "xls文件中的鼎桥物料编号不能为空！", this.languageComponent1); return;
                    }
                    if (list.Contains(dqmCode))
                    {
                        WebInfoPublish.Publish(this, "xls文件中的鼎桥物料编号" + dqmCode + "不能重复！", this.languageComponent1); return;
                    }
                    list.Add(dqmCode);

                }

                rowNum = 2;
                lastRowNum = sheet.LastRowNum;

                this.DataProvider.BeginTransaction();

                int date = FormatHelper.TODateInt(DateTime.Now);
                int time = FormatHelper.TOTimeInt(DateTime.Now);
                for (row = sheet.GetRow(2); rowNum <= lastRowNum; rowNum++, row = sheet.GetRow(rowNum))
                {

                    ICell cell1 = row.GetCell(0);
                    ICell cell2 = row.GetCell(1);
                    ICell cell3 = row.GetCell(2);
                    if (cell1 != null && cell2 != null && cell3 != null)
                    {
                        string storageCode = cell1.StringCellValue;
                        string dqmCode = cell2.StringCellValue;
                        decimal checkQty = 0;
                        decimal.TryParse(cell3.ToString(), out checkQty);




                        _WarehouseFacade.UpdateStockCheckDetailIfTypeIsCheckALL(checkNo, dqmCode, (int)checkQty, date, time);

                    }


                }
                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "导入成功！", this.languageComponent1); return;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, "未处理的异常！" + ex.Message, this.languageComponent1); return;
            }
        }

        protected void cmdLoadCheckResult_click(object sender, EventArgs e)
        {

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array == null)
                return;

            if (array.Count != 1)
            {
                WebInfoPublish.Publish(this, "只能选择一行！", this.languageComponent1); return;
            }
            string time = DateTime.Now.ToString("yyyyMMdd_HHmmss");//")+ ""+DateTime.Now.ToString("




            XlsPackage xls = new XlsPackage();
            string filename = string.Format("Export_{0}_{1}.{2}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString(), "xls");
            string filepath = string.Format(@"{0}{1}", this.DownloadPhysicalPath, filename);
            int n = 1;
            foreach (GridRecord row1 in array)
            {
                string checkNo = row1.Items.FindItemByKey("CheckNo").Text;
                xls.CreateSheet("sheet" + n);
                xls.NewRow(0);
                xls.Cell(0, "盘点单号：", xls.Normal);
                xls.Cell(1, checkNo, xls.Normal);

                xls.NewRow(1);
                xls.Cell(0, "库位", xls.Normal);
                xls.Cell(1, "鼎桥物料编码", xls.Normal);
                xls.Cell(2, "盘点数量", xls.Normal);
                BenQGuru.eMES.Domain.Warehouse.StockCheck ssss = (BenQGuru.eMES.Domain.Warehouse.StockCheck)_WarehouseFacade.GetStockCheck(checkNo);

                if (ssss.CheckType != "All")
                {
                    WebInfoPublish.Publish(this, "必须是全盘才可用！", this.languageComponent1); return;
                }
                int i = 1;
                BenQGuru.eMES.Domain.Warehouse.StockCheckDetail[] sdddd = _WarehouseFacade.GetDisStockCheckDetails(checkNo);
                foreach (BenQGuru.eMES.Domain.Warehouse.StockCheckDetail d in sdddd)
                {
                    xls.NewRow(1 + i);
                    xls.Cell(0, d.StorageCode, xls.Normal);
                    xls.Cell(1, d.DQMCODE, xls.Normal);
                    xls.Cell(2, d.CheckQty.ToString(), xls.Normal);
                    i++;
                }

                n++;
            }

            System.IO.FileStream file = new System.IO.FileStream(filepath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
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

        private string PageVirtualHostRoot
        {
            get
            {
                return string.Format("{0}{1}"
                    , this.Page.Request.Url.Segments[0]
                    , this.Page.Request.Url.Segments[1]);
            }
        }

        public string DownloadPhysicalPath
        {
            get
            {
                return string.Format(@"{0}\{1}\", Request.PhysicalApplicationPath, _downloadPath.Trim('\\', '/').Replace('/', '\\'));
            }
        }

        private void DownLoad(XlsPackage xls, string fileName)
        {
            using (System.IO.MemoryStream memoryStram = new System.IO.MemoryStream())
            {
                //把工作簿写入到内存流中
                xls.Save(memoryStram);
                //设置输出编码格式
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                //设置输出流
                Response.ContentType = "application/octet-stream";
                //防止中文乱码
                fileName = HttpUtility.UrlEncode(fileName);
                //设置输出文件名
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
                //输出
                Response.BinaryWrite(memoryStram.GetBuffer());
            }
        }




        protected void cmdAdd_ServerClick(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (string.IsNullOrEmpty(txtStockCheckEdit.Text))
            {
                WebInfoPublish.Publish(this, "盘点单号不能为空！", this.languageComponent1); return;
            }
            if (string.IsNullOrEmpty(drpStorageCodeEdit.SelectedValue))
            {
                WebInfoPublish.Publish(this, "库位不能为空！", this.languageComponent1); return;
            }
            if (string.IsNullOrEmpty(drpCheckTypeEdit.SelectedValue))
            {
                WebInfoPublish.Publish(this, "盘点类型不能为空！", this.languageComponent1); return;
            }
            if (drpCheckTypeEdit.SelectedValue != "Portion")
            {
                if (!string.IsNullOrEmpty(txtBDateEdit.Text))
                {
                    WebInfoPublish.Publish(this, "盘点日期动盘时才可用！", this.languageComponent1); return;
                }
                if (!string.IsNullOrEmpty(txtEDateEdit.Text))
                {
                    WebInfoPublish.Publish(this, "盘点日期动盘时才可用！", this.languageComponent1); return;
                }
            }
            if (drpCheckTypeEdit.SelectedValue == "Portion")
            {
                if (string.IsNullOrEmpty(txtBDateEdit.Text))
                {
                    WebInfoPublish.Publish(this, "必须输入盘点日期！", this.languageComponent1); return;
                }
                if (string.IsNullOrEmpty(txtEDateEdit.Text))
                {
                    WebInfoPublish.Publish(this, "必须输入盘点日期！", this.languageComponent1); return;
                }
            }

            try
            {
                this.DataProvider.BeginTransaction();
                BenQGuru.eMES.Domain.Warehouse.StockCheck s = new BenQGuru.eMES.Domain.Warehouse.StockCheck();
                s.CheckNo = txtStockCheckEdit.Text;
                s.CheckType = drpCheckTypeEdit.SelectedValue;
                s.StorageCode = drpStorageCodeEdit.Text;
                s.REMARK1 = txtREMARKEdit.Text;
                s.STATUS = "WaitCheck";
                s.FACCODE = " ";
                s.SDATE = FormatHelper.TODateInt(txtBDateEdit.Text);
                s.EDATE = FormatHelper.TODateInt(txtEDateEdit.Text);
                s.MDATE = FormatHelper.TODateInt(DateTime.Now);
                s.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                s.MUSER = GetUserCode();
                s.CDATE = FormatHelper.TODateInt(DateTime.Now);
                s.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                s.CUSER = GetUserCode();
                _WarehouseFacade.AddStockCheck(s);

                BenQGuru.eMES.Material.Do[] ds;
                bool isPortion = false;
                if (drpCheckTypeEdit.SelectedValue == "Portion")
                {
                    ds = _WarehouseFacade.GetPortionStorageQty(drpStorageCodeEdit.Text, s.SDATE, s.EDATE);
                    isPortion = true;
                }
                else
                    ds = _WarehouseFacade.GetStorageQTY123(drpStorageCodeEdit.Text);
                foreach (BenQGuru.eMES.Material.Do d in ds)
                {

                    BenQGuru.eMES.Domain.Warehouse.StockCheckDetail ss = new BenQGuru.eMES.Domain.Warehouse.StockCheckDetail();
                    ss.CheckNo = txtStockCheckEdit.Text;
                    ss.StorageCode = drpStorageCodeEdit.SelectedValue;
                    ss.DQMCODE = d.DQMCODE;

                    BenQGuru.eMES.Domain.MOModel.Material m = _WarehouseFacade.GetMaterialFromDQMCode(d.DQMCODE);
                    if (m == null)
                        throw new Exception(d.DQMCODE + "物料在物料表中不存在！");
                    ss.MDESC = m.MchshortDesc;
                    ss.UNIT = m.Muom;

                    ss.STORAGEQTY = d.sum;
                    ss.LocationCode = " ";
                    ss.CARTONNO = string.IsNullOrEmpty(d.CARTONNO) ? " " : d.CARTONNO;
                    if (isPortion)
                    {
                        ss.LocationCode = d.LOCATIONCODE;
                        //StorageDetail storageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(ss.CARTONNO);
                        //if (storageDetail != null)
                        //{
                        //    ss.LocationCode = storageDetail.LocationCode;
                        //}
                    }
                    ss.MDATE = FormatHelper.TODateInt(DateTime.Now);
                    ss.MTIME = FormatHelper.TOTimeInt(DateTime.Now);
                    ss.MUSER = GetUserCode();
                    ss.CDATE = FormatHelper.TODateInt(DateTime.Now);
                    ss.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                    ss.CUSER = GetUserCode();
                    _WarehouseFacade.AddStockCheckDetails(ss);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {

                this.DataProvider.RollbackTransaction();
                throw ex;
            }



            WebInfoPublish.Publish(this, "添加成功！", this.languageComponent1); return;
        }


        protected override DataRow GetGridRow(object obj)
        {

            DataRow row = this.DtSource.NewRow();
            BenQGuru.eMES.Domain.Warehouse.StockCheck p = (BenQGuru.eMES.Domain.Warehouse.StockCheck)obj;
            row["CheckNo"] = p.CheckNo;
            row["CheckType"] = languageComponent1.GetString("STOCK" + p.CheckType);
            row["StorageCode"] = p.StorageCode;
            row["CDATE"] = FormatHelper.ToDateString(p.CDATE);
            row["Status"] = p.STATUS;
            row["CUSER"] = p.CUSER;
            row["CDATE"] = FormatHelper.ToDateString(p.CDATE);
            row["CTIME"] = FormatHelper.ToTimeString(p.CTIME);
            row["REMARK1"] = p.REMARK1;

            return row;



        }

        protected void Gener_ServerClick(object sender, EventArgs e)
        {

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            string dateStr = DateTime.Now.ToString("yyyyMMdd");

            string perfix = "TDPD" + dateStr;
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

            txtStockCheckEdit.Text = perfix + max.ToString().PadLeft(3, '0');
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(GetUserCode());


            return this._WarehouseFacade.QueryStockCheck2(usergroupList,txtStockCheckCodeQuery.Text, drpStorageCodeQuery.SelectedValue, drpCheckType.SelectedValue,
           FormatHelper.TODateInt(this.txtCBDateQuery.Text),
           FormatHelper.TODateInt(this.txtCEDateQuery.Text),
           inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(GetUserCode());


            return this._WarehouseFacade.QueryStockCheckCount2(usergroupList,txtStockCheckCodeQuery.Text, drpStorageCodeQuery.SelectedValue, drpCheckType.SelectedValue,
           FormatHelper.TODateInt(this.txtCBDateQuery.Text),
           FormatHelper.TODateInt(this.txtCEDateQuery.Text)
                  );
        }

        #endregion



        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            BenQGuru.eMES.Domain.Warehouse.StockCheck s = ((BenQGuru.eMES.Domain.Warehouse.StockCheck)obj);
            return new string[]{
                                s.CheckNo,
                              s.StorageCode,
                               FormatHelper.ToDateString(s.CDATE),
                             s.STATUS,
                                s.CUSER,
                             FormatHelper.ToDateString(s.CDATE),
                                FormatHelper.ToTimeString(s.CTIME),
                               
                         
                                s.REMARK1
                               };
        }

        protected override string[] GetColumnHeaderText()
        {

            return new string[]
                {
                                    "CheckNo",
                                    "StorageCode",
                                    "CDATE",
                                    "Status",
                                    "CUSER",
                                    "CDATE",	
                                    "CTIME",
                                    "REMARK1"
                                   
                };
        }

        #endregion



        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {

            if (commandName == "LinkToCartonImport")
            {

                string CheckNo = row.Items.FindItemByKey("CheckNo").Text.Trim();
                string StorageCode = row.Items.FindItemByKey("StorageCode").Text.Trim();
                BenQGuru.eMES.Domain.Warehouse.StockCheck ssss = (BenQGuru.eMES.Domain.Warehouse.StockCheck)_WarehouseFacade.GetStockCheck(CheckNo);
                if (ssss.CheckType == "All")
                {
                    Response.Redirect(this.MakeRedirectUrl("FStockCheckDetails.aspx", new string[] { "CheckNo", "StorageCode" }, new string[] { CheckNo, StorageCode }));
                }
                else if (ssss.CheckType == "Portion")
                {
                    Response.Redirect(this.MakeRedirectUrl("FStockPortionCheckDetails.aspx", new string[] { "CheckNo", "StorageCode" }, new string[] { CheckNo, StorageCode }));
                }
            }
            else if (commandName == "StockCheckOp")
            {


                string checkNo = row.Items.FindItemByKey("CheckNo").Text.Trim();
                BenQGuru.eMES.Domain.Warehouse.StockCheck ssss = (BenQGuru.eMES.Domain.Warehouse.StockCheck)_WarehouseFacade.GetStockCheck(checkNo);
                if (ssss.CheckType == "Portion")
                    Response.Redirect(this.MakeRedirectUrl("FPortionStockCheckOp.aspx",
                                        new string[] { "CheckCode" },
                                        new string[] { checkNo
                                        
                                    }));
                else
                {

                    WebInfoPublish.Publish(this, "盘点类型必须是动盘！", this.languageComponent1);
                }
            }
        }

        protected void cmdStockCheckOp_ServerClick(object sender, EventArgs e)
        {





        }

        protected override object GetEditObject(GridRecord row)
        {

            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(this.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            string checkNo = row.Items.FindItemByKey("CheckNo").Text;

            StockCheck check = (StockCheck)_WarehouseFacade.GetStockCheck(checkNo);

            if (check != null)
            {
                return check;
            }

            return null;
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(this.DataProvider);
            }
            StockCheck[] stockChecks = ((StockCheck[])domainObjects.ToArray(typeof(StockCheck)));


            foreach (StockCheck s in stockChecks)
            {
                if (s.STATUS != "WaitCheck")
                {
                    WebInfoPublish.Publish(this, s.CheckNo + "状态必须是待盘点才能删除！", this.languageComponent1); return;
                }


            }
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (StockCheck s in stockChecks)
                {
                    _WarehouseFacade.DeleteStockCheck(s);

                }
                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "删除成功！", this.languageComponent1);
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }


        }
    }





}
