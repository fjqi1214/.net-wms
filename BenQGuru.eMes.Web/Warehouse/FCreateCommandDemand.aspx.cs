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
using System.Collections.Generic;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using BenQGuru.eMES.Material;
using System.Text;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FCreateCommandDemand : BenQGuru.eMES.Web.Helper.BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        BenQGuru.eMES.BaseSetting.SystemSettingFacade _SystemSettingFacade = null;
        private BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = null;
        private BenQGuru.eMES.Material.InventoryFacade inventoryFacade = null;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private Dictionary<string, string> dicStu = new Dictionary<string, string>();

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
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                dicStu.Add("", "");
                dicStu.Add("Release", "初始化");
                dicStu.Add("WaitPick", "待拣料");
                dicStu.Add("Pick", "拣料");
                dicStu.Add("MakePackingList", "制作箱单");
                dicStu.Add("Pack", "包装");
                dicStu.Add("OQC", "OQC检验");
                dicStu.Add("ClosePackingList", "箱单完成");
                dicStu.Add("Close", "已出库");
                dicStu.Add("Cancel", "取消");
                dicStu.Add("Block", "冻结");
                txtPickNoQuery.Text = Request.QueryString["PICKNO"];


                txtInvNoEidt.Text = Request.QueryString["INVNO"];
                InitHander();
                this.InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();
                txtWWPoSerialEdit.TextBox.Style.Add("display", "none");
            }

            txtWWPoSerialEdit.TextBox.TextChanged += new EventHandler(txtWWPoSerialEdit_TextChanged);
            txtWWPoSerialEdit.TextBox.Style.Add("display", "none");

        }



        private string MoldidToSql(string moldid)
        {
            if (moldid.Equals(string.Empty))
            {
                return "";
            }
            string sql = "";
            string[] str = moldid.Split(',');
            foreach (string s in str)
            {
                if (!s.Equals(string.Empty))
                {
                    sql += "'" + s + "',";
                }
            }

            return sql.Remove(sql.LastIndexOf(','));
        }

        protected void txtWWPoSerialEdit_TextChanged(object sender, EventArgs e)
        {
            #region 注释
            //string[] n = txtDQMCodeEdit.Text.Split(',');
            //if (n.Length > 1)
            //{
            //    txtDQMCodeEdit.Text = n[0];
            //    try
            //    {
            //        txtInvLineEidt.Text= n[1];
            //    }
            //    catch (Exception)
            //    {
            //        txtInvLineEidt.Text = "";
            //    }
            //}
            //else
            //{
            //    txtDQMCodeEdit.Text = "";
            //    txtInvLineEidt.Text = "";
            //} 
            #endregion
            if (string.IsNullOrEmpty(txtWWPoSerialEdit.Text))
                return;
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            string serialList = this.MoldidToSql(txtWWPoSerialEdit.Text);
            object[] wwpoList = _WarehouseFacade.QuerySelectedWWpoInvNo(serialList);
            string dqmCode = "";
            string invLine = "";
            string mdesc = "";
            if (wwpoList != null)
            {
                foreach (MesWWPOExc wwpo in wwpoList)
                {
                    dqmCode += wwpo.DQMCode + ",";
                    invLine += wwpo.POLine + ",";
                    mdesc += wwpo.MChLongDesc + ",";
                }
                dqmCode = dqmCode.Remove(dqmCode.LastIndexOf(','));
                invLine = invLine.Remove(invLine.LastIndexOf(','));
                mdesc = mdesc.Remove(mdesc.LastIndexOf(','));
            }
            txtDQMCodeEdit.Text = dqmCode;
            txtInvLineEidt.Text = invLine;
            txtDMESCEdit.Text = mdesc;
        }

        #region 默认查询
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

        private void RequestData()
        {

            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }


        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        protected void cmdDelete_ServerClick(object sender, EventArgs e)
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
            List<BenQGuru.eMES.Domain.Warehouse.PickDetail> ll = new List<BenQGuru.eMES.Domain.Warehouse.PickDetail>();
            foreach (GridRecord row in array)
            {
                BenQGuru.eMES.Domain.Warehouse.PickDetail obj = (BenQGuru.eMES.Domain.Warehouse.PickDetail)_WarehouseFacade.GetPickdetail(txtPickNoQuery.Text, row.Items.FindItemByKey("PICKLINE").Text);

                if (obj.Status != "Release")
                {

                    WebInfoPublish.Publish(this, obj.PickNo + "状态不为初始化！", this.languageComponent1); return;
                }


                if (inventoryFacade.GetPickDetailMaterialsCount(obj.PickNo, obj.PickLine) > 0)
                {
                    WebInfoPublish.Publish(this, obj.PickNo + ":" + obj.PickLine + "正在拣料中，不能删除！", this.languageComponent1);
                    return;
                }

                ll.Add(obj);
            }

            try
            {
                this.DataProvider.BeginTransaction();

                foreach (BenQGuru.eMES.Domain.Warehouse.PickDetail p in ll)
                {
                    _WarehouseFacade.DeletePickToPo(p.PickNo, p.PickLine);  //add by sam
                    _WarehouseFacade.DeletePickdetail(p);
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }

        }





        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("PICKLINE", "行号", null);
            this.gridHelper.AddColumn("STATUS", "状态", null);
            this.gridHelper.AddColumn("MCODE", "物料编码", null);
            this.gridHelper.AddColumn("DQMCODE", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("MDESC", "物料描述", null);
            this.gridHelper.AddColumn("CustMCode", "供应商物料编码", null);
            this.gridHelper.AddColumn("QTY", "数量", false);

            this.gridHelper.AddColumn("UNIT", "单位", null);
            this.gridHelper.AddColumn("OutCStorageCode", "出库库位", null);
            this.gridHelper.AddColumn("SQTY", "已拣货数量", null);
            this.gridHelper.AddColumn("OUTQTY", "已出货数量", null);
            //this.gridHelper.AddColumn("Down_Time", "下发时间", null);
            this.gridHelper.AddColumn("CDATE", "创建日期", null);
            //this.gridHelper.AddColumn("REMARK1", "备注", null);
            this.gridHelper.AddColumn("CTIME", "创建时间", null);
            this.gridHelper.AddColumn("CUSER", "创建人", null);
            this.gridHelper.AddColumn("MDATE", "维护日期", null);
            this.gridHelper.AddColumn("MTIME", "维护时间", null);
            this.gridHelper.AddColumn("MUSER", "维护人", null);


            this.gridHelper.AddLinkColumn("LinkToCartonImport", "已拣明细", null);
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }
        public string DownloadPhysicalPath
        {
            get
            {
                return string.Format(@"{0}\{1}\", Request.PhysicalApplicationPath, _downloadPath.Trim('\\', '/').Replace('/', '\\'));
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

        private string _downloadPath = @"\upload\";
        protected override DataRow GetGridRow(object obj)
        {
            BenQGuru.eMES.Domain.Warehouse.PickDetail pick = (BenQGuru.eMES.Domain.Warehouse.PickDetail)obj;
            BenQGuru.eMES.Domain.Warehouse.Pick s = (BenQGuru.eMES.Domain.Warehouse.Pick)_WarehouseFacade.GetPick(pick.PickNo);

            DataRow row = this.DtSource.NewRow();
            row["PICKLINE"] = pick.PickLine;
            row["STATUS"] = languageComponent1.GetString(pick.Status);//dicStu.ContainsKey(pick.Status) ? dicStu[pick.Status] : string.Empty;
            row["MCODE"] = pick.MCode;
            row["DQMCODE"] = pick.DQMCode;
            row["MDESC"] = pick.MDesc;
            row["CustMCode"] = pick.VEnderItemCode;
            row["QTY"] = pick.QTY;
            row["UNIT"] = pick.Unit;
            row["OutCStorageCode"] = s.StorageCode;// dicStu.ContainsKey(s.StorageCode) ? dicStu[s.StorageCode] : string.Empty;
            row["SQTY"] = pick.SQTY;
            row["OUTQTY"] = pick.OutQTY;
            //row["Down_Time"] = FormatHelper.ToTimeString(pick.DownTime);
            row["CDATE"] = FormatHelper.ToDateString(pick.CDate);
            //row["REMARK1"] = pick.Remark1;
            row["CTIME"] = FormatHelper.ToTimeString(pick.CTime);
            row["CUSER"] = pick.CUser;
            row["MDATE"] = FormatHelper.ToDateString(pick.MaintainDate);
            row["MTIME"] = FormatHelper.ToTimeString(pick.MaintainTime);
            row["MUSER"] = pick.MaintainUser;

            //row["MUSER"] = pick.MaintainUser;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            return _WarehouseFacade.GetPickDetails(txtPickNoQuery.Text, txtDQMCODEQuery.Text, inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }

            return _WarehouseFacade.GetPickDetailsCount(txtPickNoQuery.Text, txtDQMCODEQuery.Text);
        }




        protected override bool ValidateInput()
        {
            return true;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }


        protected void Gener_ServerClick(object sender, EventArgs e)
        {

            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            string dateStr = DateTime.Now.ToString("yyyyMMdd");

            string perfix = "OU" + dateStr;
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

        }


        public string DownloadPath
        {
            get
            {
                return string.Format(@"{0}{1}/", this.PageVirtualHostRoot, _downloadPath.Trim('\\', '/').Replace('\\', '/'));
            }
        }



        #endregion

        #region Button

        protected void cmdLotSave_ServerClick(object sender, EventArgs e)
        {
            try
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(this.DataProvider);
                BenQGuru.eMES.Domain.Warehouse.Pick p = (BenQGuru.eMES.Domain.Warehouse.Pick)_WarehouseFacade.GetPick(txtPickNoQuery.Text);
                if (p.Status != Pick_STATUS.Status_Release)
                {
                    WebInfoPublish.Publish(this, p.PickNo + "拣货任务令的状态必须是初始化才能修改！", this.languageComponent1);
                    return;
                }
                StringBuilder sb = new StringBuilder(300);
                this.DataProvider.BeginTransaction();
                List<string> pickLineGtQty = new List<string>();
                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {
                    string pickLine = this.gridWebGrid.Rows[i].Items.FindItemByKey("PICKLINE").Value.ToString();
                    BenQGuru.eMES.Domain.Warehouse.PickDetail d = (BenQGuru.eMES.Domain.Warehouse.PickDetail)_WarehouseFacade.GetPickdetail(txtPickNoQuery.Text, pickLine);
                    decimal qty = d.QTY;
                    BenQGuru.eMES.Domain.MOModel.Material m = (BenQGuru.eMES.Domain.MOModel.Material)_WarehouseFacade.GetMaterialFromDQMCode(d.DQMCode);

                    if (m == null)
                    {
                        WebInfoPublish.Publish(this, d.DQMCode + "物料号不存在！", this.languageComponent1);
                        this.DataProvider.RollbackTransaction();
                        return;
                    }



                    string qtyStr = this.gridWebGrid.Rows[i].Items.FindItemByKey("QTY").Value.ToString();

                    decimal decqty = 0;
                    if (!decimal.TryParse(qtyStr, out decqty))
                    {
                        WebInfoPublish.Publish(this, txtPickNoQuery.Text + ":" + pickLine + "数量必须是数字！", this.languageComponent1);
                        this.DataProvider.RollbackTransaction();
                        return;

                    }

                    string invno = FormatHelper.CleanString(txtInvNoEidt.Text);
                    decimal pickqty = _WarehouseFacade.GetPickDetailQty(invno, d.InvLine, m.MCode);
                    decimal wwpoqty = _WarehouseFacade.GetWWPOQty(invno, d.InvLine, m.MCode);

                    bool isFalse = (decqty + pickqty - qty > wwpoqty);
                    d.QTY = decqty;
                    if (isFalse)
                    {

                        sb.Append(string.Format(@"拣货任务令号:{0}，SAP单据号:{1}，状态:{2}，下发人:{3}，鼎桥物料编码:{4}，描述:{5}，领取数量:{6},超领数量：{7} \r\n",
                            p.PickNo,
                            txtInvNoEidt.Text,
                            p.Status,
                            GetUserCode(),
                            m.DqmCode,
                            m.MenshortDesc,
                            decqty, decqty + pickqty - qty - wwpoqty));
                        pickLineGtQty.Add(pickLine);
                    }
                    _WarehouseFacade.UpdatePickdetail(d);
                }

                this.DataProvider.CommitTransaction();

                if (pickLineGtQty.Count == 0)
                    WebInfoPublish.Publish(this, "保存成功", this.languageComponent1);
                else
                {
                    GenWWPOMail(sb.ToString(), GetUserCode());
                    WebInfoPublish.Publish(this, "保存成功 - " + txtPickNoQuery.Text + "行项目" + string.Join(",", pickLineGtQty.ToArray()) + " 的领料数大于SAP物料数量", this.languageComponent1);
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            this.gridHelper.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
        }

        private void GenWWPOMail(string mailBody, string user)
        {

            SendMail mail = new SendMail();
            mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
            mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
            mail.CUSER = user;
            mail.MAILCONTENT = mailBody;
            mail.MAILTYPE = ShareLib.ShareKit.MailName.PickingExceptionMail;
            mail.Recipients = _WarehouseFacade.Recipients(mail.MAILTYPE);
            mail.SENDFLAG = "N";
            _WarehouseFacade.AddSendMail(mail);
        }



        protected override void cmdAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtWWPoSerialEdit.Text))
                return;
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }



            BenQGuru.eMES.Domain.Warehouse.Pick p = (BenQGuru.eMES.Domain.Warehouse.Pick)_WarehouseFacade.GetPick(txtPickNoQuery.Text);


            if (p.Status != Pick_STATUS.Status_Release)
            {
                WebInfoPublish.Publish(this, p.PickNo + "拣货任务令的状态必须是初始化才能修改！", this.languageComponent1);
                return;
            }
            BenQGuru.eMES.Domain.Warehouse.PickDetail pick = new BenQGuru.eMES.Domain.Warehouse.PickDetail();

            string serialList = this.MoldidToSql(txtWWPoSerialEdit.Text);
            object[] wwpoList = _WarehouseFacade.QuerySelectedWWpoInvNo(serialList);


            #region 注释
            //pick.PickNo = txtPickNoEdit.Text;
            //pick.PickType = drpPickTypeEdit.SelectedValue;
            //pick.InvNo = txtInvNoEdit.Text;
            //pick.StorageCode = drpStorageCodeEdit.SelectedValue;
            //pick.ReceiverUser = txtReceiverUserEdit.Text;
            //pick.Receiveraddr = txtReceiverAddrEdit.Text;
            //pick.PlanDate = FormatHelper.TODateInt(txtPlanDateEdit.Text);
            //pick.Remark1 = txtREMARKEdit.Text;
            #endregion

            try
            {
                this.DataProvider.BeginTransaction();
                foreach (MesWWPOExc wwpo in wwpoList)
                {
                    //BenQGuru.eMES.Domain.MOModel.Material m = (BenQGuru.eMES.Domain.MOModel.Material)_WarehouseFacade.GetMaterialFromDQMCode(wwpo.DQMCode);
                    //if (m == null)
                    //{
                    //    this.DataProvider.RollbackTransaction();
                    //    WebInfoPublish.Publish(this, "鼎桥物料号不存在", this.languageComponent1);
                    //    return;
                    //}
                    #region add by sam Picktopo

                    PickToPo pickToPo = new PickToPo();
                    pickToPo.PickNo = txtPickNoQuery.Text;
                    pickToPo.DQMCode = wwpo.DQMCode;// txtDQMCodeEdit.Text;
                    pickToPo.PickLine = (_WarehouseFacade.GetMaxLine(txtPickNoQuery.Text) + 1).ToString();
                    pickToPo.MCode = wwpo.MCode;// string.IsNullOrEmpty(m.MCode) ? " " : m.MCode;
                    pickToPo.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    pickToPo.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    pickToPo.MaintainUser = this.GetUserCode();


                    pickToPo.PoNo = FormatHelper.CleanString(txtInvNoEidt.Text);
                    pickToPo.PoLine = wwpo.POLine.ToString();
                    _WarehouseFacade.AddPickToPo(pickToPo);

                    #endregion

                    #region add by sam 检查qty

                    //int invline = 0;
                    //if (!string.IsNullOrEmpty(txtInvLineEidt.Text))
                    //{
                    //    invline = Convert.ToInt32(txtInvLineEidt.Text);
                    //}
                    int invline = wwpo.POLine;
                    string mcode = wwpo.MCode;// string.IsNullOrEmpty(m.MCode) ? " " : m.MCode;
                    string invno = FormatHelper.CleanString(txtInvNoEidt.Text);
                    decimal decqty = 0; // decimal.Parse(txtNumEdit.Text);
                    //decimal pickqty = _WarehouseFacade.GetPickDetailQty(invno, invline, mcode);
                    //decimal wwpoqty = _WarehouseFacade.GetWWPOQty(invno, invline, mcode);
                    //if (decqty + pickqty > wwpoqty)
                    //{
                    //    WebInfoPublish.Publish(this, "领料数量大于库存数量", this.languageComponent1);
                    //}
                    #endregion


                    pick.PickNo = txtPickNoQuery.Text;
                    pick.DQMCode = wwpo.DQMCode;// txtDQMCodeEdit.Text;
                    pick.MDesc = wwpo.MChLongDesc;
                    pick.PickLine = (_WarehouseFacade.GetMaxLine(txtPickNoQuery.Text) + 1).ToString();
                    pick.MCode = wwpo.MCode;// string.IsNullOrEmpty(m.MCode) ? " " : m.MCode;
                    //pick.QTY = 0; // decimal.Parse(txtNumEdit.Text);

                    pick.QTY = 0;

                    pick.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    pick.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    pick.MaintainUser = this.GetUserCode();
                    pick.Status = "Release";
                    pick.VEnderItemCode = wwpo.HWMCode;
                    pick.CDate = FormatHelper.TODateInt(DateTime.Now);
                    pick.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                    pick.CUser = this.GetUserCode();
                    pick.InvLine = invline;
                    _WarehouseFacade.AddPickdetail(pick);
                }



                //if (decqty + pickqty > wwpoqty)
                //{
                //    WebInfoPublish.Publish(this, "领料数量大于库存数量", this.languageComponent1);
                //}
                //else
                //{
                //}
                this.DataProvider.CommitTransaction();
                txtWWPoSerialEdit.Text = string.Empty;
                WebInfoPublish.Publish(this, "添加成功！", this.languageComponent1);
            }
            catch (Exception ex)
            {

                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }


            this.gridHelper.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }

        #endregion

        #region SetEditObject
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {


        }




        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);
                if (obj != null)
                {
                    this.SetEditObject(obj);
                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
            else if (commandName == "LinkToCartonImport")
            {


                Response.Redirect(this.MakeRedirectUrl("FCreateCheckedPickLineMP.aspx",
                                    new string[] { "PickNo" },
                                    new string[] { txtPickNoQuery.Text }));
            }
            //if (commandName == "Edit")
            //{

            //    BenQGuru.eMES.Domain.MOModel.Material m = _WarehouseFacade.GetMaterialFromDQMCode(txtDQMCodeEdit.Text);

            //    BenQGuru.eMES.Domain.Warehouse.PickDetail d = (BenQGuru.eMES.Domain.Warehouse.PickDetail)_WarehouseFacade.GetPickdetail(txtPickNoQuery.Text, row.Items.FindItemByKey("PICKLINE").Text.Trim());
            //    txtPickLineEdit.Text = row.Items.FindItemByKey("PICKLINE").Text.Trim();
            //    txtDMESCEdit.Text = m.MchlongDesc;
            //    txtDQMCodeEdit.Text = d.DQMCode;
            //    txtNumEdit.Text = d.QTY.ToString();



            //}
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            BenQGuru.eMES.Domain.Warehouse.PickDetail d = (BenQGuru.eMES.Domain.Warehouse.PickDetail)_WarehouseFacade.GetPickdetail(txtPickNoQuery.Text, row.Items.FindItemByKey("PICKLINE").Text.Trim());
            if (d != null)
            {
                return d;
            }

            return null;
        }
        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                txtPickLineEdit.Text = "";

                txtDQMCodeEdit.Text = "";
                txtDMESCEdit.Text = "";
                txtInvLineEidt.Text = "";
                return;
            }


            BenQGuru.eMES.Domain.Warehouse.PickDetail d = (BenQGuru.eMES.Domain.Warehouse.PickDetail)obj;
            BenQGuru.eMES.Domain.MOModel.Material m = _WarehouseFacade.GetMaterialFromDQMCode(d.DQMCode);
            txtPickLineEdit.Text = d.PickLine;
            txtDMESCEdit.Text = m.MchlongDesc;
            txtDQMCodeEdit.Text = d.DQMCode;

            txtInvLineEidt.Text = d.InvLine.ToString();

        }

        protected override object GetEditObject()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);
            }
            BenQGuru.eMES.Domain.Warehouse.PickDetail d = (BenQGuru.eMES.Domain.Warehouse.PickDetail)_WarehouseFacade.GetPickdetail(txtPickNoQuery.Text, txtPickLineEdit.Text);

            d.DQMCode = txtDQMCodeEdit.Text;
            return d;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return null;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"CARINVNO",
                                    "PICKNO",
                                    "STATUS",
                                    "INVNO",

	                                "ORDERNO",
                                    "StorageCode",
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

        #region 返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {



            Response.Redirect(this.MakeRedirectUrl("FCreatePickCommand.aspx",
                        new string[] { "PickNo" },
                       new string[] { txtPickNoQuery.Text.Trim().ToUpper()
                                        
                                    }));


        }
        #endregion

        protected void drpStorageInTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
