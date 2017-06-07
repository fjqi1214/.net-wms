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
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FPortionStockCheckOp : BenQGuru.eMES.Web.Helper.BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = null;



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
            //PostBackTrigger tri = new PostBackTrigger();
            //tri.ControlID = this.cmdAddImport.ID;
            //(this.FindControl("up1") as UpdatePanel).Triggers.Add(tri);
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


                string checkCode = Request.QueryString["CheckCode"];
                if (!string.IsNullOrEmpty(checkCode))
                {

                    this.cmdReturn.Visible = true;
                    txtStockCheckCodeQuery.Text = checkCode;
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
            this.gridHelper.AddColumn("CheckNo", "盘点单号", null);
            this.gridHelper.AddColumn("StorageCode", "库位", null);
            this.gridHelper.AddColumn("DQMCODE", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("SLocationCode", "库存货位", null);
            this.gridHelper.AddColumn("SCARTONNO", "库存箱号", null);

            this.gridHelper.AddColumn("LocationCode", "实际货位", null);
            this.gridHelper.AddColumn("ACARTONNO", "实际箱号", null);
            this.gridHelper.AddColumn("STORAGEQTY", "库存数量", null);
            //this.gridHelper.AddLinkColumn("LinkToCartonImport", "导入/查看箱单", null);
            this.gridHelper.AddColumn("CheckQty", "盘点数量", null);
            this.gridHelper.AddColumn("CTIME", "盘点时间", null);
            this.gridHelper.AddColumn("DIFFDESC", "差异描述", null);
            //this.gridWebGrid.Columns.FromKey("SCARTONNO").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            if (!string.IsNullOrEmpty(txtStockCheckCodeQuery.Text))
            {

                this.gridHelper.RequestData();
            }

        }

        void drpCheckType_SelectedIndexChanged(object sender, EventArgs e)
        {

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






        protected override DataRow GetGridRow(object obj)
        {

            DataRow row = this.DtSource.NewRow();
            BenQGuru.eMES.Material.StockCheckDetailOp p = (BenQGuru.eMES.Material.StockCheckDetailOp)obj;
            row["CheckNo"] = p.CheckNo;
            row["StorageCode"] = p.StorageCode;
            row["DQMCODE"] = p.DQMCODE;
            row["LocationCode"] = p.LocationCode;
            row["ACARTONNO"] = p.CARTONNO;

            row["SLocationCode"] = p.SLocationCode;
            row["SCARTONNO"] = p.SCARTONNO;

            row["STORAGEQTY"] = p.STORAGEQTY;
            row["CheckQty"] = p.Qty;
            row["CTIME"] = FormatHelper.ToTimeString(p.CTIME);
            row["DIFFDESC"] = p.DiffDesc;
            return row;

        }



        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            return this._WarehouseFacade.GetPortionStockCheckOps(txtStockCheckCodeQuery.Text, inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.GetPortionStockCheckOpsCount(txtStockCheckCodeQuery.Text);
        }

        #endregion



        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            StockCheckDetailOp s = ((StockCheckDetailOp)obj);
            return new string[]{
                                s.CheckNo,
                              s.StorageCode,
                              s.DQMCODE,
                             s.SLocationCode,
                                s.SCARTONNO,
                            s.LocationCode,
                            s.CARTONNO,
                               s.STORAGEQTY.ToString(),
                               s.Qty.ToString(),
                              FormatHelper.ToTimeString(  s.CTIME),
                              s.DiffDesc
                               };
        }

        protected override string[] GetColumnHeaderText()
        {


            return new string[]
                {
                                    "CheckNo",
                                    "StorageCode",
                                    "DQMCODE",
                                    "SLocationCode",
                                    "SCARTONNO",
                                    "LocationCode",	
                                    "ACARTONNO",
                                    "STORAGEQTY",
                                    "CheckQty",
                                    "CTIME",
                                    "DIFFDESC"

                };
        }

        #endregion
        protected void cmdStorageCheckClose_ServerClick(object sender, EventArgs e)
        {

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            BenQGuru.eMES.Material.StockCheckDetailOp[] ops = this._WarehouseFacade.GetPortionStockCheckOpsNoPager(txtStockCheckCodeQuery.Text);

            foreach (BenQGuru.eMES.Material.StockCheckDetailOp op in ops)
            {
                if (op.STORAGEQTY != op.Qty)
                {
                    if (string.IsNullOrEmpty(op.DiffDesc))
                    {
                        WebInfoPublish.Publish(this, "存在数量不一致请填写盘点差异原因！", this.languageComponent1); return;
                    }

                }

            }
            BenQGuru.eMES.Domain.Warehouse.StockCheck s = (BenQGuru.eMES.Domain.Warehouse.StockCheck)_WarehouseFacade.GetStockCheck(txtStockCheckCodeQuery.Text);
            s.STATUS = "Close";
            _WarehouseFacade.UpdateStockCheck(s);
            WebInfoPublish.Publish(this, "盘点关闭成功！", this.languageComponent1); return;
            this.txtStockCheckCodeEdit.Text = string.Empty;
            txtDQMCODEEdit.Text = string.Empty;
            txtStorageCodeEdit.Text = string.Empty;
            txtLocationCodeEdit.Text = string.Empty;
            txtLocationCodeEdit.Text = string.Empty;
            txtCARTONNOEdit.Text = string.Empty;
            txtCheckQtyEdit.Text = string.Empty;
            txtDiffDescEdit.Text = string.Empty;
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {

            }

            if (pageAction == PageActionType.Update)
            {
                this.txtStockCheckCodeEdit.Text = string.Empty;
                txtDQMCODEEdit.Text = string.Empty;
                txtStorageCodeEdit.Text = string.Empty;
                txtsLocationCodeEdit.Text = string.Empty;
                txtLocationCodeEdit.Text = string.Empty;
                txtCARTONNOEdit.Text = string.Empty;
                txtCheckQtyEdit.Text = string.Empty;
            }
        }


        protected void cmdAdd_ServerClick(object sender, EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            string message = string.Empty;
            ShareLib.ShareKit kit = new ShareLib.ShareKit();
            bool result = kit.SubmitPortionCheck(txtStockCheckCodeQuery.Text,
                                    txtCARTONNOEdit.Text,
                                    txtCheckQtyEdit.Text,
                                    txtLocationCodeEdit.Text,
                                    txtDiffDescEdit.Text,
                                    txtDQMCODEEdit.Text,
                                    base.DataProvider, GetUserCode(), out message);


            if (!result)
            {
                WebInfoPublish.Publish(this, message, this.languageComponent1);
                return;
            }
            this.txtStockCheckCodeEdit.Text = string.Empty;
            txtDQMCODEEdit.Text = string.Empty;
            txtStorageCodeEdit.Text = string.Empty;
            txtsLocationCodeEdit.Text = string.Empty;
            txtLocationCodeEdit.Text = string.Empty;
            txtCARTONNOEdit.Text = string.Empty;
            txtCheckQtyEdit.Text = string.Empty;
            txtDiffDescEdit.Text = string.Empty;

            WebInfoPublish.Publish(this, "提交成功！", this.languageComponent1);
            this.cmdQuery_Click(null, null);


        }



        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                string CheckNo = row.Items.FindItemByKey("CheckNo").Text;
                txtStockCheckCodeEdit.Text = CheckNo;
                string DQMCODE = row.Items.FindItemByKey("DQMCODE").Text;
                txtDQMCODEEdit.Text = DQMCODE;


                txtLocationCodeEdit.Text = row.Items.FindItemByKey("LocationCode").Text;//实际货位
                //string CARTONNO = row.Items.FindItemByKey("ACARTONNO").Text;
                txtCARTONNOEdit.Text = row.Items.FindItemByKey("ACARTONNO").Text;//实际箱号
                string CheckQty = row.Items.FindItemByKey("CheckQty").Text;
                txtCheckQtyEdit.Text = CheckQty;

                string StorageCode = row.Items.FindItemByKey("StorageCode").Text;
                txtStorageCodeEdit.Text = StorageCode;

                txtsLocationCodeEdit.Text = row.Items.FindItemByKey("SLocationCode").Text;//库存货位
                txtsCARTONNOEdit.Text = row.Items.FindItemByKey("SCARTONNO").Text;//库存货位


            }
        }

        #region 编辑


        //protected override object GetEditObject()
        //{
        //    if (this.ValidateInput())
        //    {
        //        if (_InventoryFacade == null)
        //        {
        //            _InventoryFacade = new InventoryFacade(base.DataProvider);
        //        }
        //        string pickno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPickNoEdit.Text, 40));
        //        Pick pick = (Pick)_InventoryFacade.GetPick(pickno);
        //        //TBLPICK
        //        pick.StNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtASNCodeEdit.Text, 40));
        //        pick.PlanDate = FormatHelper.TODateInt(this.txtPlanSendDateEdit.Text);
        //        return pick;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        //protected override void SetEditObject(object obj)
        //{
        //    BenQGuru.eMES.Material.StockCheckDetailOp pick = obj as BenQGuru.eMES.Material.StockCheckDetailOp;
        //    if (pick == null)
        //    {
        //        this.txtLocationCodeEdit.Text = string.Empty;
        //        this.txtCARTONNOEdit.Text = string.Empty;
        //        this.txtCheckQtyEdit.Text = string.Empty;
        //        this.txtDQMCODEEdit.Text = string.Empty;
        //        return;
        //    }


        //    this.txtLocationCodeEdit.Text = pick.LocationCode;
        //    this.txtCARTONNOEdit.Text = pick.CARTONNO;
        //    this.txtDQMCODEEdit.Text = pick.DQMCODE;
        //    this.txtCheckQtyEdit.Text = pick.Qty.ToString();
        //}


        #endregion


        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {


            Response.Redirect(this.MakeRedirectUrl("FStockCheck.aspx",
                                new string[] { "CheckCode" },
                                new string[] { txtStockCheckCodeQuery.Text.Trim().ToUpper()
                                        
                                    }));


        }


    }
}
