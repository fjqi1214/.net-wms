using System;
using System.Collections.Generic;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.SAPRFCService;

using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using System.IO;
using BenQGuru.eMES.SAPRFCService.Domain;
using BenQGuru.eMES.Common;



namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FASNReceiveMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade _InventoryFacade = null;
        private WarehouseFacade _WarehouseFacade = null;
        private bool _RedirectFlag = false;//页面跳转标识

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
            this.gridWebGrid2.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid2_InitializeRow);
        }
        void gridWebGrid2_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
        {
            e.Row.Items.FindItemByKey("DocName2").CssClass = "LinkFontBlue";
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
        protected void Page_Init(object sender, System.EventArgs e)
        {
            PostBackTrigger tri = new PostBackTrigger();
            tri.ControlID = this.cmdPicUpLoad.ID;
            (this.FindControl("up1") as UpdatePanel).Triggers.Add(tri);
        }
        //private void InitHander()
        //{
        //    this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
        //    this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
        //    this.gridHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount);
        //    this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

        //    this.buttonHelper = new ButtonHelper(this);

        //    //this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);


        //    #region Exporter
        //    this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
        //    this.excelExporter.Page = this;
        //    this.excelExporter.LanguageComponent = this.languageComponent1;
        //    this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
        //    this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
        //    this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        //    #endregion

        //}


        //private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        //{
        //    //this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        //}



        protected void Page_Load(object sender, System.EventArgs e)
        {
            string stNo = this.GetRequestParam("StNo");//接收入库指令号

            if (!string.IsNullOrEmpty(stNo))
            {
                _RedirectFlag = true;
                this.txtStorageInASNQuery.Text = stNo;

            }
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            _WarehouseFacade = new WarehouseFacade(base.DataProvider);

            if (!this.IsPostBack)
            {
                InitHander();
                this.InitWebGrid();
                this.cmdQuery_Click(null, null);
                this.RequestData();

                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                InitDrpDownList();

                if (_RedirectFlag)
                {
                    ASN asn = (ASN)_InventoryFacade.GetASN(stNo);

                    this.txtStorageInASNQuery.Enabled = false;

                    cmdReturn.Visible = true;


                    //if (asn.Status != ASNHeadStatus.Receive)
                    //{
                    //    cmdQueryASN.Disabled = true;
                    //    cmdCheckSN.Disabled = true;
                    //    cmdSubmitCarton.Disabled = true;//提 交
                    //    cmdClear.Disabled = true;
                    //    cmdRejectbt.Disabled = true;
                    //    cmdReceivebt.Disabled = true;
                    //    cmdGiveinbt.Disabled = true;
                    //    cmdPicUpLoad.Disabled = true;
                    //    cmdPicDelete.Disabled = true;
                    //}
                    if (asn.ExigencyFlag == "Y")
                    {
                        this.chkEmergency.Visible = true;
                        this.cmdGiveinbt.Visible = true;
                        this.drpWaitDesc.Visible = true;
                        this.lblWaitDesc.Visible = true;

                    }
                    else
                    {
                        this.chkEmergency.Visible = false;
                        this.cmdGiveinbt.Visible = false;
                        this.drpWaitDesc.Visible = false;
                        this.lblWaitDesc.Visible = false;
                    }
                    txtRejectQty.Text = asn.InitRejectQty.ToString();
                    Asndetail[] ds = _WarehouseFacade.GetAsnDetails(stNo);
                    string giveInDesc = string.Empty;
                    if (ds != null && ds.Length != 0)
                    {
                        foreach (Asndetail d in ds)
                        {
                            if (!string.IsNullOrEmpty(d.Remark2))
                                txtMemoEdit.Text = d.Remark2;
                            if (!string.IsNullOrEmpty(d.InitGIVEINDESC))
                                giveInDesc = d.InitGIVEINDESC;
                        }
                    }

                    foreach (ListItem item in drpRejectDesc.Items)
                    {
                        if (item.Value == asn.InitReceiveDesc)
                        {
                            drpRejectDesc.SelectedValue = asn.InitReceiveDesc;
                            break;
                        }

                    }


                    foreach (ListItem item in drpWaitDesc.Items)
                    {
                        if (item.Value == giveInDesc)
                        {
                            drpWaitDesc.SelectedValue = giveInDesc;
                            break;
                        }

                    }

                    RefreshQty();
                }
                else
                {
                    this.txtStorageInASNQuery.Enabled = true;
                    cmdReturn.Visible = false;

                }

            }
        }



        #region 默认查询
        private void RequestData()
        {

            //this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            //this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            //this.pagerToolBar.RowCount = GetRowCount();
            //this.pagerToolBar.InitPager();
        }
        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);

            //this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);


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
            this.gridHelper.GridBind(0, int.MaxValue);
            //this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        #endregion
        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }


        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ASN", "入库指令号", null);
            this.gridHelper.AddColumn("ASNline", "入库指令号行", null);
            this.gridHelper.AddColumn("StorageInType", "入库类型", null);
            this.gridHelper.AddColumn("SAPInvNo", "SAP单据号", null);
            this.gridHelper.AddColumn("CartonBigSeq", "大箱号", null);
            this.gridHelper.AddColumn("CartonSeq", "小箱号", null);
            this.gridHelper.AddColumn("CartonNo11", "箱号编码", null);
            this.gridHelper.AddColumn("DQLotNO", "鼎桥批次号", null);
            this.gridHelper.AddColumn("Status", "状态", null);
            this.gridHelper.AddColumn("InitReceiveStatus", "初检结果", null);
            this.gridHelper.AddColumn("DQMCODE", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("DQMDESC", "鼎桥物料编码描述", null);
            this.gridHelper.AddColumn("VENDORMCODE", "供应商物料编码", null);
            this.gridHelper.AddColumn("VENDORMCODEDESC", "供应商物料编码描述", null);
            this.gridHelper.AddColumn("DQQTY", "来料数量", null);
            this.gridHelper.AddColumn("DQReceiveQTY", "已接收", null);
            this.gridHelper.AddColumn("DQACTQTY", "已入库", null);
            this.gridHelper.AddColumn("UNIT", "单位", null);
            this.gridHelper.AddColumn("Production_date", "生产日期", null);
            this.gridHelper.AddColumn("Supplier_LotNo", "供应商批次号", null);
            this.gridHelper.AddColumn("MControlType", "物料管控类型", null);
            this.gridHelper.AddColumn("CartonRemark1", "箱单备注", null);

            this.gridWebGrid.Columns.FromKey("ASNline").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, false);
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);


        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ASN"] = ((AsndetailEX)obj).Stno;
            row["ASNline"] = ((AsndetailEX)obj).Stline;
            row["StorageInType"] = this.GetInvInName(((AsndetailEX)obj).StType);//FormatHelper.ToDateString(((ASN)obj).CDate);
            row["SAPInvNo"] = ((AsndetailEX)obj).Invno;
            row["CartonBigSeq"] = ((AsndetailEX)obj).Cartonbigseq;
            row["CartonSeq"] = ((AsndetailEX)obj).Cartonseq;
            row["CartonNo11"] = ((AsndetailEX)obj).Cartonno;
            row["DQLotNO"] = ((AsndetailEX)obj).Lotno;
            row["Status"] = this.GetStatusName(((AsndetailEX)obj).Status);
            row["InitReceiveStatus"] = this.GetLineStatusName(((AsndetailEX)obj).InitreceiveStatus);
            row["DQMCODE"] = ((AsndetailEX)obj).DqmCode;
            row["DQMDESC"] = ((AsndetailEX)obj).MDesc;

            AsndetailEX detail = ((AsndetailEX)obj);
            row["VENDORMCODE"] = detail.StType == "UB" ? ((AsndetailEX)obj).CustmCode : detail.VEndormCode;
            row["VENDORMCODEDESC"] = ((AsndetailEX)obj).VEndormCodeDesc;
            row["DQQTY"] = ((AsndetailEX)obj).Qty;
            row["DQReceiveQTY"] = ((AsndetailEX)obj).ReceiveQty;
            row["DQACTQTY"] = ((AsndetailEX)obj).ActQty;
            row["UNIT"] = ((AsndetailEX)obj).Unit;
            row["Production_date"] = FormatHelper.ToDateString(((AsndetailEX)obj).Production_Date);
            row["Supplier_LotNo"] = ((AsndetailEX)obj).Supplier_lotno;
            row["MControlType"] = this.languageComponent1.GetString(((AsndetailEX)obj).MControlType);
            row["CartonRemark1"] = ((AsndetailEX)obj).Remark1;

            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }




            return this._InventoryFacade.QueryASNDetail(
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageInASNQuery.Text)), "",
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryASNDetailCount(
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageInASNQuery.Text)), ""
               );
        }

        protected override void InitWebGrid2()
        {
            base.InitWebGrid2();
            this.gridHelper2.AddColumn("DocSerial", "序号", null);
            this.gridHelper2.AddColumn("InvDocNo", "单据号", null);
            this.gridHelper2.AddColumn("DocName2", "文件名", null);
            this.gridHelper2.AddColumn("DocType", "文件类型", null);
            this.gridHelper2.AddColumn("DocSize", "文件大小", null);
            this.gridHelper2.AddColumn("PicType", "图片类型", null);
            this.gridHelper2.AddColumn("UpUser", "上传者", null);
            this.gridHelper2.AddColumn("UpDate", "上传时间", null);

            this.gridWebGrid2.Columns.FromKey("DocSerial").Hidden = true;
            this.gridWebGrid2.Columns.FromKey("InvDocNo").Hidden = true;

            this.gridHelper2.AddDefaultColumn(true, false);
            ((BoundDataField)this.gridHelper2.Grid.Columns.FromKey("DocName2")).CssClass = "tdDocument";
            //多语言
            this.gridHelper2.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow2(object obj)
        {
            DataRow row = this.DtSource2.NewRow();
            row["DocSerial"] = ((InvDoc)obj).DocSerial.ToString();
            row["InvDocNo"] = ((InvDoc)obj).InvDocNo;
            row["DocName2"] = ((InvDoc)obj).DocName;
            row["DocType"] = ((InvDoc)obj).DocType.Replace(".", "");
            row["DocSize"] = ((InvDoc)obj).DocSize + "KB";
            row["PicType"] = this.languageComponent1.GetString(((InvDoc)obj).InvDocType);//显示中文
            row["UpUser"] = ((InvDoc)obj).UpUser;
            row["UpDate"] = FormatHelper.ToDateString(((InvDoc)obj).UpfileDate);
            return row;
        }

        protected override object[] LoadDataSource2(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return _InventoryFacade.QueryInvDoc(FormatHelper.PKCapitalFormat(this.txtStorageInASNQuery.Text.Trim()), inclusive, exclusive);
        }

        protected override int GetRowCount2()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return _InventoryFacade.QueryInvDocCount(FormatHelper.PKCapitalFormat(this.txtStorageInASNQuery.Text.Trim()));
        }


        #endregion


        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "DocName2")
            {
                string InvDocNo = row.Items.FindItemByKey("InvDocNo").Text.Trim();

                if (!string.IsNullOrEmpty(InvDocNo))
                {
                    Response.Redirect(this.MakeRedirectUrl("../BaseSetting/FInDocView.aspx", new string[] { "ACT", "InvDocNo" }, new string[] { "LinkDetail", InvDocNo }));
                }
                //string DocSerial =row.Items.FindItemByKey("DocSerial").Text.Trim();
                //string storageOut = row.Items.FindItemByKey("StorageOut").Text.Trim();
            }
        }


        #region Button

        //单选按钮 选择改变
        protected void rbtReject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkRejectpic.Checked)
            {
                this.chkGiveinpic.Checked = false;
            }
        }

        //单选按钮 选择改变
        protected void rbtGivein_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkGiveinpic.Checked)
            {
                this.chkRejectpic.Checked = false;
            }
        }

        protected void btnASNNOEnter_Click(object sender, EventArgs e)
        {
            this.cmdQuery_ServerClick(sender, e);
        }
        protected void btnSNEnter_Click(object sender, EventArgs e)
        {
            this.cmdCheck_ServerClick(sender, e);
        }
        protected void btnCartonEnter_Click(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            if (!CheckGrid())
            {
                return;
            }
            //判断箱号有没有跟其他入库单的行关联
            //

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                foreach (GridRecord row in array)
                {
                    ASNDetail asnDetail = (ASNDetail)GetEditObject(row);
                    if (!string.IsNullOrEmpty(asnDetail.CartonNo))
                    {
                        WebInfoPublish.PublishInfo(this, "该行数据已有关联箱号：" + asnDetail.CartonNo, this.languageComponent1);
                        return;
                    }
                    else
                    {
                        asnDetail.CartonNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonCode.Text.Trim()));
                        _InventoryFacade.UpdateASNDetail(asnDetail);
                        WebInfoPublish.PublishInfo(this, "关联箱号成功", this.languageComponent1);
                    }
                }
            }
            this.gridHelper.RequestData();
        }

        //查询
        /// <summary>
        /// 根据入库指令号查询行数据信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdQuery_ServerClick(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            if (!string.IsNullOrEmpty(this.txtStorageInASNQuery.Text.Trim()))
            {
                ASN asn = (ASN)_InventoryFacade.GetASN(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageInASNQuery.Text.Trim())));
                if (asn != null)
                {

                    txtRejectQty.Text = asn.InitRejectQty.ToString();


                    foreach (ListItem item in drpRejectDesc.Items)
                    {
                        if (item.Value == asn.InitReceiveDesc)
                        {
                            drpRejectDesc.SelectedValue = asn.InitReceiveDesc;
                            break;
                        }

                    }

                    string giveInDesc = string.Empty;

                    Asndetail[] ds = _WarehouseFacade.GetAsnDetails(asn.StNo);
                    if (ds != null && ds.Length != 0)
                    {
                        foreach (Asndetail d in ds)
                        {
                            if (!string.IsNullOrEmpty(d.Remark2))
                                txtMemoEdit.Text = d.Remark2;
                            if (!string.IsNullOrEmpty(d.InitGIVEINDESC))
                                giveInDesc = d.InitGIVEINDESC;
                        }
                    }

                    foreach (ListItem item in drpWaitDesc.Items)
                    {
                        if (item.Value == giveInDesc)
                        {
                            drpWaitDesc.SelectedValue = giveInDesc;
                            break;
                        }

                    }

                    if (asn.ExigencyFlag == "Y")
                    {
                        this.chkEmergency.Visible = true;
                        this.cmdGiveinbt.Visible = true;
                        this.drpWaitDesc.Visible = true;
                        this.lblWaitDesc.Visible = true;
                    }
                    else
                    {
                        this.chkEmergency.Visible = false;
                        this.cmdGiveinbt.Visible = false;
                        this.drpWaitDesc.Visible = false;
                        this.lblWaitDesc.Visible = false;

                    }
                }
                else
                {
                    this.chkEmergency.Visible = false;
                    this.cmdGiveinbt.Visible = false;
                    this.drpWaitDesc.Visible = false;
                    this.lblWaitDesc.Visible = false;
                    WebInfoPublish.PublishInfo(this, "入库指令号不存在", this.languageComponent1);
                    return;
                }
            }
            else
            {
                this.chkEmergency.Visible = false;
                this.cmdGiveinbt.Visible = false;
                this.drpWaitDesc.Visible = false;
                this.lblWaitDesc.Visible = false;
                WebInfoPublish.PublishInfo(this, "请输入入库指令号", this.languageComponent1);
                return;
            }
            this.gridHelper.RequestData();
            if (this.gridHelper2 != null)
                this.gridHelper2.RequestData();
            RefreshQty();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);

        }

        //检索
        /// <summary>
        /// 根据SN匹配到入库指令号具体的行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdCheck_ServerClick(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            if (this.gridWebGrid.Rows.Count == 0)
            {
                //提示错误
                WebInfoPublish.PublishInfo(this, "Grid中没有数据", this.languageComponent1);
                return;
            }

            if (string.IsNullOrEmpty(this.txtSNOrCodeEdit.Text.Trim()))
            {
                //提示错误
                WebInfoPublish.PublishInfo(this, "必须提供数据！", this.languageComponent1);
                return;
            }
            

            object[] obj = _InventoryFacade.QueryASNDetailSNOrCode(this.txtSNOrCodeEdit.Text.Trim().ToUpper(), txtStorageInASNQuery.Text);
            if (obj == null || obj.Length == 0)
            {
                WebInfoPublish.PublishInfo(this, this.txtSNOrCodeEdit.Text + "没有检索出行！", this.languageComponent1);
                return;
            }
            bool exist = false;


            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                string stNO = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASN").Text;
                string stLine = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASNline").Text;

                foreach (Asndetailsn snCode in obj)
                {
                    if (snCode.Stno == stNO && snCode.Stline == stLine)
                    {
                        this.gridWebGrid.Rows[i].Items.FindItemByKey("Check").Value = true;
                        exist = true;
                    }


                }

            }
            if (!exist)
                WebInfoPublish.PublishInfo(this, this.txtSNOrCodeEdit.Text + "没有检索出行！", this.languageComponent1);


        }

        //提交
        /// <summary>
        /// 给勾选的入库指令号行数据匹配箱号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdSubmit_ServerClick(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            WarehouseFacade facade = new WarehouseFacade(base.DataProvider);
            if (!_InventoryFacade.CartonnoIsNotRepeat(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonCode.Text.Trim().ToUpper()))))
            {
                WebInfoPublish.PublishInfo(this, this.txtCartonCode.Text.Trim() + "箱号已经使用！", this.languageComponent1);
                return;
            }                             
            int result = _InventoryFacade.GetCartonNoByStnoAndCartonNo(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStorageInASNQuery.Text.Trim())), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonCode.Text.Trim())));
            switch (result)
            {
                case 1:
                    WebInfoPublish.PublishInfo(this, "当前STNO 箱号重复", this.languageComponent1);
                    return;
                case 2:
                    WebInfoPublish.PublishInfo(this, "此箱号在其他的STNO中", this.languageComponent1);
                    return;
                case 3:
                    WebInfoPublish.PublishInfo(this, "此箱号在库存中", this.languageComponent1);
                    return;

            }
            if (!CheckGrid())
            {
                return;
            }
            //判断箱号有没有跟其他入库单的行关联
            //

            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                foreach (GridRecord row in array)
                {
                    ASNDetail asnDetail = (ASNDetail)GetEditObject(row);
                    if (!string.IsNullOrEmpty(asnDetail.CartonNo))
                    {
                        WebInfoPublish.PublishInfo(this, "该行数据已有关联箱号：" + asnDetail.CartonNo, this.languageComponent1);
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(this.txtCartonCode.Text))
                        {
                            WebInfoPublish.PublishInfo(this, "箱号不能为空" + asnDetail.CartonNo, this.languageComponent1);
                            return;
                        }
                        ASN asn = (ASN)_InventoryFacade.GetASN(this.txtStorageInASNQuery.Text.Trim().ToUpper());
                        int num = _InventoryFacade.GetASNDetailCountCartonNoNutNull(this.txtStorageInASNQuery.Text.Trim().ToUpper());
                        if (num <= asn.InitRejectQty)
                        {
                            WebInfoPublish.PublishInfo(this, "已经到拒收箱数，不用关联箱号", this.languageComponent1);
                            return;
                        }
                        this.DataProvider.BeginTransaction();
                        try
                        {
                            asnDetail.CartonNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonCode.Text.Trim()));
                            _InventoryFacade.UpdateASNDetail(asnDetail);
                            object[] objs = facade.GetASNDetailSNbyStnoandStline((asnDetail as ASNDetail).StNo, (asnDetail as ASNDetail).StLine);
                            if (objs != null)
                            {
                                for (int i = 0; i < objs.Length; i++)
                                {
                                    Asndetailsn a_sn = objs[i] as Asndetailsn;
                                    a_sn.Cartonno = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonCode.Text.Trim()));
                                    facade.UpdateAsndetailsn(a_sn);
                                }
                            }
                            this.DataProvider.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.PublishInfo(this, "关联失败" + ex, this.languageComponent1);
                            return;
                        }

                        row.Items.FindItemByKey("Check").Value = false;
                        WebInfoPublish.PublishInfo(this, "关联箱号成功", this.languageComponent1);
                    }
                }
            }
            this.txtCartonCode.Text = string.Empty;
            this.gridHelper.RequestData();
        }

        protected void cmdCancel_ServerClick(object sender, EventArgs e)
        {

            WarehouseFacade facade = new WarehouseFacade(base.DataProvider);
            _InventoryFacade = new InventoryFacade(base.DataProvider);
            string stno = this.txtStorageInASNQuery.Text.Trim().ToUpper();


            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count <= 0)
            {
                WebInfoPublish.PublishInfo(this, "请勾选数据！", this.languageComponent1);
                return;
            }
            List<ASNDetail> asnds = new List<ASNDetail>();
            foreach (GridRecord row in array)
            {

                ASNDetail asnDetail = (ASNDetail)GetEditObject(row);

                asnds.Add(asnDetail);

            }
            ShareLib.ShareKit kit = new ShareLib.ShareKit();
            string message = kit.ReceiveCancelCartonno(stno, asnds, facade, _InventoryFacade, this.DataProvider);
            WebInfoPublish.PublishInfo(this, message, this.languageComponent1);
            this.gridHelper.RequestData();
        }


        //清空
        /// <summary>
        /// 清除关联箱号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdClear_ServerClick(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            if (this.gridWebGrid.Rows.Count == 0)
            {
                //提示错误
                WebInfoPublish.PublishInfo(this, "Grid中没有数据", this.languageComponent1);
                return;
            }
            ASNDetail asnDetail = null;
            bool isClear = false;

            string stno = this.txtStorageInASNQuery.Text.Trim().ToUpper();
            ASN asn = (ASN)_InventoryFacade.GetASN(stno);
            if (asn.Status != ASNHeadStatus.Receive)
            {
                WebInfoPublish.PublishInfo(this, "状态必须是" + languageComponent1.GetString(ASNHeadStatus.Receive) + "！", this.languageComponent1);
                return;
            }

            bool hasDetail = _InventoryFacade.CheckASNHasDetail(this.txtStorageInASNQuery.Text.Trim().ToUpper(), ASNLineStatus.ReceiveClose);
            if (!hasDetail)
            {
                WebInfoPublish.PublishInfo(this, "初检已完成不能清空！", this.languageComponent1);
                return;

            }
            WarehouseFacade facade = new WarehouseFacade(base.DataProvider);

            try
            {
                this.DataProvider.BeginTransaction();

                _InventoryFacade.UpdateAsnDetailToRec(this.txtStorageInASNQuery.Text.Trim().ToUpper());
                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {


                    string stNO = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASN").Text;
                    string stLine = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASNline").Text;
                    asnDetail = (ASNDetail)this._InventoryFacade.GetASNDetail(int.Parse(stLine), stNO);


                    asnDetail.CartonNo = string.Empty;
                    asnDetail.InitReceiveStatus = string.Empty;
                    asnDetail.InitReceiveDesc = string.Empty;
                    asnDetail.ReceiveQty = 0;
                    asnDetail.Remark2 = string.Empty;
                    asnDetail.Status = ASNHeadStatus.Receive;
                    asnDetail.InitGIVEINDESC = string.Empty;
                    _InventoryFacade.UpdateASNDetail(asnDetail);

                    Asndetailsn[] sns = facade.GetAsnDetailSN(stNO, stLine);
                    foreach (Asndetailsn sn in sns)
                    {
                        sn.Cartonno = string.Empty;
                        facade.UpdateAsndetailsn(sn);
                    }
                }

                txtRejectQty.Text = "0";
                drpRejectDesc.SelectedValue = string.Empty;
                txtMemoEdit.Text = string.Empty;

                isClear = true;
                this.DataProvider.CommitTransaction();

            }
            catch (Exception ex)
            {

                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
                throw ex;

            }

            if (isClear)
            {
                WebInfoPublish.PublishInfo(this, "清除成功", this.languageComponent1);
                RefreshQty();
            }



            this.gridHelper.RequestData();
        }

        //拒收
        /// <summary>
        /// 拒收实际箱数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdReject_ServerClick(object sender, EventArgs e)
        {
            if (this.gridWebGrid.Rows.Count == 0)
            {
                WebInfoPublish.PublishInfo(this, "初检行为零，请先查询", this.languageComponent1);
                return;
            }
            if (string.IsNullOrEmpty(this.txtRejectQty.Text.Trim()))
            {
                WebInfoPublish.PublishInfo(this, "拒收时必须填写拒收数量和选择拒收原因", this.languageComponent1);
                return;
            }

            if (string.IsNullOrEmpty(this.drpRejectDesc.SelectedValue))
            {
                WebInfoPublish.PublishInfo(this, "拒收时必须填写拒收数量和选择拒收原因", this.languageComponent1);
                return;
            }

            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new NumberCheck(this.lblRejectQty, this.txtRejectQty, false));
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return;
            }
            ASN asn1 = (ASN)_InventoryFacade.GetASN(txtStorageInASNQuery.Text);
            if (asn1 == null)
                throw new Exception(asn1.StNo + "入库指令号不存在！");
            if (asn1.StType == InInvType.PGIR)
            {
                if (int.Parse(txtRejectQty.Text) != this.gridWebGrid.Rows.Count)
                {
                    WebInfoPublish.Publish(this, "生产退料只能全部拒收！", this.languageComponent1);
                    return;
                }
            }
            if (asn1.Status != ASN_STATUS.ASN_ReceiveRejection && asn1.Status != ASN_STATUS.ASN_Receive)
            {
                WebInfoPublish.Publish(this, "入库指令状态必须是初检阶段！", this.languageComponent1);
                return;

            }

            this.DataProvider.BeginTransaction();
            try
            {
                ASNDetail asnDetail = null;

                //如果拒收数量等于剩余待检行数量，表示剩余全部拒收
                if (int.Parse(this.txtRejectQty.Text.Trim()) <= this.gridWebGrid.Rows.Count)//否则部分拒收
                {
                    if (!string.IsNullOrEmpty(txtMemoEdit.Text))
                    {
                        for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                        {
                            string stNO = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASN").Text;
                            string stLine = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASNline").Text;
                            asnDetail = (ASNDetail)this._InventoryFacade.GetASNDetail(int.Parse(stLine), stNO);
                            //检查剩余待检的是否已全部勾选
                            asnDetail.Remark2 = txtMemoEdit.Text;
                            _InventoryFacade.UpdateASNDetail(asnDetail);
                        }
                    }
                    int rejectCount = int.Parse(this.txtRejectQty.Text.Trim());

                    //更新主表初检拒收数量和拒收描述
                    ASN asn = (ASN)_InventoryFacade.GetASN(this.txtStorageInASNQuery.Text.Trim().ToUpper());

                    if (rejectCount > 0)
                        asn.InitReceiveDesc = this.drpRejectDesc.SelectedValue;
                    asn.InitRejectQty = rejectCount;

                    _InventoryFacade.UpdateASN(asn);
                }
                else
                {
                    WebInfoPublish.PublishInfo(this, "拒收数量不能大于剩余待检行记录数", this.languageComponent1);
                    return;
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }


            WebInfoPublish.PublishInfo(this, "拒收成功！", this.languageComponent1);
            RefreshQty();
            this.gridHelper.RequestData();

        }

        //接收
        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdReceive_ServerClick(object sender, EventArgs e)
        {

            _InventoryFacade = new InventoryFacade(base.DataProvider);

            _WarehouseFacade = new WarehouseFacade(base.DataProvider);

            if (this.gridWebGrid.Rows.Count == 0)
            {
                //提示错误
                WebInfoPublish.PublishInfo(this, "Grid中没有数据", this.languageComponent1);
                return;
            }
            List<Asndetail> asnds = new List<Asndetail>();
            string stno = this.txtStorageInASNQuery.Text.Trim().ToUpper();


            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {

                string stNO = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASN").Text;
                string stLine = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASNline").Text;
                if (stno != stNO)
                {
                    WebInfoPublish.PublishInfo(this, "入库指令号和行项目的不一致！", this.languageComponent1);
                    return;
                }
                Asndetail asnd = new Asndetail { Stno = stNO, Stline = stLine };
                asnds.Add(asnd);

            }
            ShareLib.ShareKit kit = new ShareLib.ShareKit();
            string message = kit.Receive(stno,
                                        asnds,
                                        drpRejectDesc.SelectedValue,
                                        GetUserCode(),
                                        Server.MapPath(this.VirtualHostRoot + "InvDoc/" + "箱单导入/"),
                                        _InventoryFacade,
                                        _WarehouseFacade,
                                        base.DataProvider);
            WebInfoPublish.PublishInfo(this, message, this.languageComponent1);
        }

        //让步接收
        /// <summary>
        /// 让步接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdGivein_ServerClick(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            if (string.IsNullOrEmpty(this.drpWaitDesc.SelectedValue))
            {
                WebInfoPublish.PublishInfo(this, "让步接收时必须填写原因", this.languageComponent1);
                return;
            }
            if (this.gridWebGrid.Rows.Count == 0)
            {
                //提示错误
                WebInfoPublish.PublishInfo(this, "Grid中没有数据", this.languageComponent1);
                return;
            }
            bool isGivein = false;
            ASNDetail asnDetail = null;

            try
            {

                ASN asn = (ASN)_InventoryFacade.GetASN(this.txtStorageInASNQuery.Text.Trim().ToUpper());


                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {
                    if (this.gridWebGrid.Rows[i].Items.FindItemByKey("Check").Text.ToUpper() == "TRUE")
                    {
                        string stNO = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASN").Text;
                        string stLine = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASNline").Text;
                        ASNDetail asnDetail1 = (ASNDetail)this._InventoryFacade.GetASNDetail(int.Parse(stLine), stNO);
                        if (!_WarehouseFacade.CheckAlterIncludeEQ(asn.InvNo, asnDetail1.DQMCode))
                        {


                            //提示错误
                            WebInfoPublish.PublishInfo(this, asn.InvNo + ":" + asnDetail.StLine + ":" + asnDetail.DQMCode + "数量已超出SAP计划数量！", this.languageComponent1);
                            return;
                        }
                        if (string.IsNullOrEmpty(asnDetail1.CartonNo))
                        {
                            WebInfoPublish.PublishInfo(this, "让步接收必须填写箱号！", this.languageComponent1);
                            return;

                        }
                    }
                }

                this.DataProvider.BeginTransaction();
                //算出剩余要接收的数量（入库指令行记录数减去拒收数量）

                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {

                    if (this.gridWebGrid.Rows[i].Items.FindItemByKey("Check").Text.ToUpper() == "TRUE")
                    {
                        string stNO = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASN").Text;
                        string stLine = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASNline").Text;

                        asnDetail = (ASNDetail)this._InventoryFacade.GetASNDetail(int.Parse(stLine), stNO);

                        asn = (ASN)_InventoryFacade.GetASN(stNO);

                        if (asnDetail.Status != ASNDetail_STATUS.ASNDetail_ReceiveClose)
                        {


                            asnDetail.InitReceiveStatus = SAP_LineStatus.SAP_LINE_GIVEIN;
                            asnDetail.ReceiveQty = asnDetail.Qty;
                            asnDetail.Status = ASNDetail_STATUS.ASNDetail_ReceiveClose;
                            asnDetail.InitGIVEINDESC = this.drpWaitDesc.SelectedValue;

                            if (!string.IsNullOrEmpty(txtMemoEdit.Text))
                            {
                                asnDetail.Remark2 = txtMemoEdit.Text;
                            }
                            _InventoryFacade.UpdateASNDetail(asnDetail);

                            //接收数量(TBLASNDETAILITEM.ReceiveQTY)更新为：等于需求数量(TBLASNDETAILITEM.QTY)
                            _InventoryFacade.UpdateASNItem(stNO, stLine);

                            asn.InitGiveInQty += 1;
                            _InventoryFacade.UpdateASN(asn);
                            isGivein = true;
                        }
                    }
                }

                this.DataProvider.CommitTransaction();


                if (isGivein)
                {
                    WebInfoPublish.PublishInfo(this, "让步接收成功！", this.languageComponent1);
                }
                else
                {

                    WebInfoPublish.PublishInfo(this, "让步接收失败没有条目需要让步接收！", this.languageComponent1);
                }

                RefreshQty();
                this.gridHelper.RequestData();

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, "让步接收出错！", this.languageComponent1);
                throw ex;

            }


        }

        //图片上传
        protected void cmdUpLoad_ServerClick(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            string asnNo = FormatHelper.CleanString(this.txtStorageInASNQuery.Text);
            if (string.IsNullOrEmpty(asnNo))
            {
                WebInfoPublish.PublishInfo(this, "入库指令号为空", this.languageComponent1);
                return;
            }
            if (this.FileImport.PostedFile != null)
            {
                try
                {
                    HttpPostedFile postedFile = this.FileImport.PostedFile;
                    string img = Path.GetExtension(postedFile.FileName);
                    //if (!(img.ToLower() == "png" || img.ToLower() == "jpg" || img.ToLower() == "jpeg" || img.ToLower() == "gif"))
                    //{
                    //    WebInfoPublish.PublishInfo(this, "上传文件必须是png,jpg,jpeg,gif类型", this.languageComponent1);
                    //    return;
                    //}
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);
                    InvDoc invDoc = _InventoryFacade.CreateNewInvDoc();

                    invDoc.InvDocNo = asnNo;
                    invDoc.InvDocType = this.chkRejectpic.Checked ? "InitReject" : "InitGivein";

                    invDoc.DocType = Path.GetExtension(postedFile.FileName);
                    invDoc.DocName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    invDoc.DocSize = postedFile.ContentLength / 1024;
                    invDoc.UpUser = this.GetUserCode();
                    invDoc.UpfileDate = dbDateTime.DBDate;
                    invDoc.MaintainUser = this.GetUserCode();
                    invDoc.MaintainDate = dbDateTime.DBDate;
                    invDoc.MaintainTime = dbDateTime.DBTime;

                    string path = Server.MapPath(this.VirtualHostRoot + "InvDoc/" + "初检签收/");
                    string fileName = this.chkRejectpic.Checked ? string.Format("{0}_InitReject_{1}{2}{3}",
                        this.txtStorageInASNQuery.Text.ToUpper(), dbDateTime.DBDate, dbDateTime.DBTime, invDoc.DocType) : string.Format("{0}_InitGivein_{1}{2}{3}",
                        this.txtStorageInASNQuery.Text.ToUpper(), dbDateTime.DBDate, dbDateTime.DBTime, invDoc.DocType);

                    invDoc.ServerFileName = fileName;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    this.FileImport.PostedFile.SaveAs(path + fileName);
                    invDoc.Dirname = "初检签收";
                    _InventoryFacade.AddInvDoc(invDoc);
                    WebInfoPublish.PublishInfo(this, "$Success_UpLoadPic", this.languageComponent1);
                    this.gridHelper2.RequestData();
                }
                catch (Exception ex)
                {

                    WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
                }

            }
            else
            {
                WebInfoPublish.PublishInfo(this, "导入图片不能为空", this.languageComponent1);
            }
        }

        //删除
        /// <summary>
        /// 删除上传的图片信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdDeletePic_ServerClick(object sender, EventArgs e)
        {
            if (!CheckGrid2())
            {
                return;
            }

            ArrayList array = this.gridHelper2.GetCheckedRows();
            if (array.Count > 0)
            {
                try
                {
                    this.DataProvider.CommitTransaction();
                    InvDoc invDoc = null;
                    foreach (GridRecord row in array)
                    {
                        invDoc = (InvDoc)_InventoryFacade.GetInvDoc(int.Parse(row.Items.FindItemByKey("DocSerial").Text));
                        if (invDoc != null)
                        {
                            _InventoryFacade.DeleteInvDoc(invDoc);
                        }
                    }
                    this.DataProvider.CommitTransaction();
                    WebInfoPublish.PublishInfo(this, "删除成功", this.languageComponent1);
                    this.gridHelper2.RequestData();
                }
                catch (Exception ex)
                {
                    WebInfoPublish.PublishInfo(this, "删除出错", this.languageComponent1);
                    this.DataProvider.RollbackTransaction();
                }
            }

        }


        #region Object <--> Page

        protected override object GetEditObject(GridRecord row)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            object obj = _InventoryFacade.GetASNDetail(int.Parse(row.Items.FindItemByKey("ASNline").Value.ToString()), row.Items.FindItemByKey("ASN").Value.ToString());

            if (obj != null)
            {
                return (ASNDetail)obj;
            }
            return null;
        }

        #endregion

        #region 自定义

        protected bool CheckGrid()
        {
            if (this.gridWebGrid.Rows.Count == 0)
            {
                //提示错误
                WebInfoPublish.PublishInfo(this, "Grid没有数据", this.languageComponent1);
                return false;
            }
            int count = 0;
            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                if (this.gridWebGrid.Rows[i].Items.FindItemByKey("Check").Text.ToUpper() == "TRUE")
                {
                    count++;
                }
            }
            if (count == 0)
            {
                WebInfoPublish.PublishInfo(this, "请勾选且只勾选一条数据", this.languageComponent1);
                return false;
            }
            else if (count > 1)
            {
                WebInfoPublish.PublishInfo(this, "只能勾选一条数据", this.languageComponent1);
                return false;
            }
            return true;
        }

        protected bool CheckGrid2()
        {
            if (this.gridWebGrid2.Rows.Count == 0)
            {
                //提示错误
                WebInfoPublish.PublishInfo(this, "Grid没有数据", this.languageComponent1);
                return false;
            }
            int count = 0;
            for (int i = 0; i < this.gridWebGrid2.Rows.Count; i++)
            {
                if (this.gridWebGrid2.Rows[i].Items.FindItemByKey("Check").Text.ToUpper() == "TRUE")
                {
                    count++;
                }
            }
            if (count == 0)
            {
                WebInfoPublish.PublishInfo(this, "请勾选要操作的数据", this.languageComponent1);
                return false;
            }
            return true;
        }



        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {

            Response.Redirect(this.MakeRedirectUrl("FExecuteASNMP.aspx",
                                 new string[] { "StNo" },
                                 new string[] { txtStorageInASNQuery.Text.Trim().ToUpper()
                                        
                                    }));

        }


        //刷新lbl纪录
        protected void RefreshQty()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            ASN asn = (ASN)_InventoryFacade.GetASN(this.txtStorageInASNQuery.Text.Trim().ToUpper());
            this.lblBoxCountEdit.Text = this.gridWebGrid.Rows.Count.ToString();
            this.lblRejectQtyEdit.Text = asn.InitRejectQty.ToString();
            this.lblActQty.Text = (this.gridWebGrid.Rows.Count - asn.InitRejectQty).ToString();
            this.lblReceivedQty.Text = (asn.InitReceiveQty).ToString();
            this.lblGiveinQty.Text = asn.InitGiveInQty.ToString();
        }

        protected void InitDrpDownList()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            //拒收原因
            this.drpRejectDesc.Items.Add(new ListItem("", ""));
            object[] obj = _InventoryFacade.GetDrpDesc("REJECTRESULT");
            if (obj != null && obj.Length > 0)
            {
                foreach (Domain.BaseSetting.Parameter param in obj)
                {
                    this.drpRejectDesc.Items.Add(new ListItem(param.ParameterDescription, param.ParameterCode));
                }
            }
            this.drpRejectDesc.SelectedIndex = 0;

            //让步接收问题
            obj = _InventoryFacade.GetDrpDesc("GIVEINRESULT");
            this.drpWaitDesc.Items.Add(new ListItem("", ""));
            if (obj != null && obj.Length > 0)
            {
                foreach (Domain.BaseSetting.Parameter param in obj)
                {
                    this.drpWaitDesc.Items.Add(new ListItem(param.ParameterDescription, param.ParameterCode));
                }
            }
            this.drpWaitDesc.SelectedIndex = 0;

        }
        #endregion
    }
}
        #endregion

