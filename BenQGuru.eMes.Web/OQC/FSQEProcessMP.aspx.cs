using System;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.OQC;
using System.Collections.Generic;
using System.Text;


namespace BenQGuru.eMES.Web.OQC
{
    public partial class FSQEProcessMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        private OQCFacade _OQCFacade = null;
        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;

        /// <summary>
        /// 页面跳转带入OQC检验单号
        /// </summary>
        public string OQCNoQS
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetRequestParam("OQCNo")))
                {
                    return this.GetRequestParam("OQCNo");
                }
                return string.Empty;
            }
            set
            {
                OQCNoQS = value;
            }
        }

        /// <summary>
        /// 页面跳转带入物料控管类型
        /// </summary>
        public string MControlTypeQS
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetRequestParam("MControlType")))
                {
                    return this.GetRequestParam("MControlType");
                }
                return string.Empty;
            }
            set
            {
                MControlTypeQS = value;
            }
        }

        /// <summary>
        /// 页面跳转带入AQL结果
        /// </summary>
        public string AQLResultQS
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetRequestParam("AQLResult")))
                {
                    return this.GetRequestParam("AQLResult");
                }
                return string.Empty;
            }
            set
            {
                AQLResultQS = value;
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

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitProcessWayList();
                this.txtOQCNoQuery.Text = OQCNoQS;
                this.txtAQLResult.Text = AQLResultQS;
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        //初始处理方式下拉框
        /// <summary>
        /// 初始处理方式下拉框
        /// </summary>
        private void InitProcessWayList()
        {
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }

            this.drpProcessWayEdit.Items.Add(new ListItem("", ""));

            this.drpProcessWayEdit.Items.Add(new ListItem("让步接收", SQEStatus.SQEStatus_Give));
            this.drpProcessWayEdit.Items.Add(new ListItem("退换货", SQEStatus.SQEStatus_Return));

            this.drpProcessWayEdit.SelectedIndex = 0;
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("OQCNo", "OQC检验单", null);
            this.gridHelper.AddColumn("STORAGECODE123", "出库库位", null);
            this.gridHelper.AddDataColumn("CarInvNo", "发货箱单号", true);
            this.gridHelper.AddDataColumn("MCode", "SAP物料号", true);
            this.gridHelper.AddColumn("DQMCode", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("DQMCODEDESC", "鼎桥物料描述", null);

            this.gridHelper.AddColumn("CartonNo", "箱号", null);
            this.gridHelper.AddColumn("NGTypeDesc", "缺陷类型", null);
            this.gridHelper.AddColumn("NGType", "缺陷类型", true);
            this.gridHelper.AddColumn("NGDesc", "缺陷描述", null);
            this.gridHelper.AddDataColumn("ECode", "缺陷代码", true);
            this.gridHelper.AddColumn("NGQty", "缺陷品数", null);
            this.gridHelper.AddColumn("SN", "SN", null);
            this.gridHelper.AddColumn("OQCMemo", "OQC备注", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("ProcessWay", "判定结果", null);
            this.gridHelper.AddColumn("SQECUSER", "判定人", null);
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();//页面初始化加载grid数据
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            _InventoryFacade = new InventoryFacade(base.DataProvider);
            _OQCFacade = new OQCFacade(base.DataProvider);
            row["OQCNo"] = ((OQCDetailEC)obj).OqcNo;

            Domain.OQC.OQC oqc = (Domain.OQC.OQC)_OQCFacade.GetOQC(((OQCDetailEC)obj).OqcNo);
            Pick p = (Pick)_InventoryFacade.GetPick(oqc.PickNo);

            row["STORAGECODE123"] = p.StorageCode;

            row["CarInvNo"] = ((OQCDetailEC)obj).CarInvNo;
            row["MCode"] = ((OQCDetailEC)obj).MCode;
            row["DQMCode"] = ((OQCDetailEC)obj).DQMCode;

            Domain.MOModel.Material m = (Domain.MOModel.Material)_InventoryFacade.GetMaterialByDQMCode(((OQCDetailEC)obj).DQMCode);
            if (m != null)
                row["DQMCODEDESC"] = m.MchlongDesc;
            else
                row["DQMCODEDESC"] = string.Empty;

            row["CartonNo"] = ((OQCDetailEC)obj).CartonNo;
            row["NGType"] = ((OQCDetailEC)obj).EcgCode;
            _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            object[] objs_ecg = _SystemSettingFacade.GetErrorGroupcode(((OQCDetailEC)obj).EcgCode);
            if (objs_ecg != null)
                row["NGTypeDesc"] = (objs_ecg[0] as ErrorCodeGroupA).ErrorCodeGroupDescription;
            else
                row["NGTypeDesc"] = ((OQCDetailEC)obj).EcgCode;

            object[] objs_ec = _SystemSettingFacade.GetErrorcodeByEcode(((OQCDetailEC)obj).ECode);
            if (objs_ec != null)
            { row["NGDesc"] = (objs_ec[0] as ErrorCodeA).ErrorDescription; }
            else
            { row["NGDesc"] = ((OQCDetailEC)obj).ECode; }

            //row["NGDesc"] = ((OQCDetailEC)obj).ECode;
            row["ECode"] = ((OQCDetailEC)obj).ECode;
            row["NGQty"] = ((OQCDetailEC)obj).NgQty;
            row["SN"] = ((OQCDetailEC)obj).SN;

            row["OQCMemo"] = ((OQCDetailEC)obj).Remark1;
            row["Memo"] = ((OQCDetailEC)obj).SQERemark1;
            row["ProcessWay"] = this.GetSQEStatusName(((OQCDetailEC)obj).SqeStatus);
            row["SQECUSER"] = ((OQCDetailEC)obj).SQECUser;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            return _OQCFacade.QueryOQCDetailEC(FormatHelper.CleanString(this.txtOQCNoQuery.Text),
                                                FormatHelper.CleanString(this.txtCartonNoQurey.Text),
                                                FormatHelper.CleanString(this.txtSNQuery.Text),
                                                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            return _OQCFacade.QueryOQCDetailECCount(FormatHelper.CleanString(this.txtOQCNoQuery.Text),
                                                    FormatHelper.CleanString(this.txtCartonNoQurey.Text),
                                                    FormatHelper.CleanString(this.txtSNQuery.Text)
                                                );
        }

        #endregion

        #region Button

        //确认
        protected void cmdConfirm_ServerClick(object sender, EventArgs e)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            Domain.OQC.OQC _OQC = _OQCFacade.GetOQC(OQCNoQS) as Domain.OQC.OQC;
            Pick pick1 = null;
            if (_OQC.Status == OQCStatus.OQCStatus_OQCClose)
            {
                WebInfoPublish.PublishInfo(this, "状态已经为判定完成，不能修改", this.languageComponent1);
                return;
            }
            #region add by sam 卡控状态  2016年4月15日

            //Domain.OQC.OQC _oqc = _OQCFacade.GetOQC(OQCNoQS) as Domain.OQC.OQC;
            if (_OQC.Status != OQCStatus.OQCStatus_SQEJudge)
            {
                WebInfoPublish.Publish(this, "状态不是SQE判定，不能判定", this.languageComponent1);
                return;
            }

            #endregion

            if (this.ValidateInput())
            {
                ArrayList array = this.gridHelper.GetCheckedRows();
                if (array.Count > 0)
                {
                    try
                    {
                        this.DataProvider.BeginTransaction();

                        foreach (GridRecord row in array)
                        {
                            string sqeStatus = row.Items.FindItemByKey("ProcessWay").Text.Trim();
                            if (!string.IsNullOrEmpty(sqeStatus))
                            {
                                object obj_value = _OQCFacade.GetParaValueByText(sqeStatus);
                                if (obj_value == null)
                                {
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.PublishInfo(this, "参数错误" + sqeStatus, this.languageComponent1);
                                    //continue;
                                    return;
                                }
                                sqeStatus = (obj_value as BenQGuru.eMES.Domain.BaseSetting.Parameter).ParameterAlias;
                            }

                            object objRow = this.GetEditObject(row);
                            object obj = this.GetEditObject();
                            if (obj != null)
                            {
                                #region 更新TBLOQCDETAILEC，TBLOQCDETAIL
                                OQCDetailEC oqcDeatilEC = (OQCDetailEC)obj;

                                #region add by sam
                                //若为“退换货”，将当前OQC单更新为OQCClose状态同时，tblpcik.status更新为pick ，
                                //tblpickdetail.status更新为pick,tblcartoninvoices.status更新为Release.
                                if (oqcDeatilEC.SqeStatus == SQEStatus.SQEStatus_Return)//退换货
                                {

                                    #region tblcartoninvoices
                                    CARTONINVOICES cartonInvoices = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoicesByOqcNo(OQCNoQS);
                                    if (cartonInvoices != null)
                                    {
                                        cartonInvoices.STATUS = CartonInvoices_STATUS.Status_Release;
                                        _WarehouseFacade.UpdateCartoninvoices(cartonInvoices);
                                        string pickno = cartonInvoices.PICKNO;
                                        _WarehouseFacade.UpdatePickStatusByPickNo(
                                                                                  PickHeadStatus.PickHeadStatus_Pick, pickno);
                                        _WarehouseFacade.UpdatePickDetailToPickingByPickNo(PickHeadStatus.PickHeadStatus_Pick, pickno);
                                    }
                                    #endregion
                                }
                                #endregion

                                //3)当同一箱在送检单明细对应缺陷明细表(TBLOQCDETAILEC)中行记录中NGFLAG=Y也有NGFLAG=N时
                                //if (!CheckNGFlagIsAllYN(oqcDeatilEC.OqcNo, oqcDeatilEC.CartonNo) && MControlTypeQS == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                                if (!CheckNGFlagIsAllYN1(oqcDeatilEC.OqcNo, oqcDeatilEC.CartonNo, oqcDeatilEC.NgFlag))
                                {
                                    //object[] objOqcDetailEc = _OQCFacade.GetOQCDetailEC(oqcDeatilEC.OqcNo, oqcDeatilEC.CartonNo);
                                    //if (objOqcDetailEc != null)
                                    //{
                                    //    if (oqcDeatilEC.NgFlag != ((OQCDetailEC)objOqcDetailEc[0]).NgFlag)
                                    //    {
                                    //        //不能确认
                                    //        WebInfoPublish.PublishInfo(this, "与第一笔缺陷种类不一样，不能确认", this.languageComponent1);
                                    //        continue;
                                    //    }
                                    //}
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.PublishInfo(this, "整箱入库为退货时，不能编辑单件不良", this.languageComponent1);
                                    //continue;
                                    return;
                                }
                                if (string.IsNullOrEmpty(sqeStatus))
                                {
                                    //1,首先判断是否有整箱不良，如果有，必须先维护整箱不良
                                    if (!CheckALLNG(oqcDeatilEC.OqcNo, oqcDeatilEC.CartonNo, oqcDeatilEC.NgFlag))
                                    {
                                        this.DataProvider.RollbackTransaction();
                                        WebInfoPublish.PublishInfo(this, "有整箱不良，必须先维护整箱不良", this.languageComponent1);
                                        //continue;
                                        return;
                                    }
                                    if (CheckALLNGStatus(oqcDeatilEC.OqcNo, oqcDeatilEC.CartonNo, oqcDeatilEC.NgFlag))
                                    {
                                        this.DataProvider.RollbackTransaction();
                                        WebInfoPublish.PublishInfo(this, "整箱不良为判退，不能再维护单件不良", this.languageComponent1);
                                        return;
                                    }

                                }
                                //检查整箱入库时，如果是退货状态，单件不良也更新为退换货
                                if (oqcDeatilEC.NgFlag.ToUpper() == "Y")
                                {
                                    CheckIsUpdateSingleNG(oqcDeatilEC);
                                }
                                _OQCFacade.UpdateOQCDetailEC(oqcDeatilEC);

                                OQCDetail oqcDetail = (OQCDetail)_OQCFacade.GetOQCDetail(oqcDeatilEC.CarInvNo, oqcDeatilEC.OqcNo, oqcDeatilEC.CartonNo, oqcDeatilEC.MCode);
                                if (oqcDetail != null)
                                {
                                    if (!string.IsNullOrEmpty(sqeStatus))
                                    {
                                        //如果不为空，说明是更新
                                        if (oqcDeatilEC.NgFlag == "N")
                                        {
                                            ChangeQtyForSN(sqeStatus, this.drpProcessWayEdit, oqcDetail, oqcDeatilEC.NgQty);
                                        }
                                        else
                                        {
                                            ChangeQtyForSN(sqeStatus, this.drpProcessWayEdit, oqcDetail, oqcDetail.Qty);
                                        }
                                    }
                                    else
                                    {
                                        if (oqcDeatilEC.NgFlag == "N")
                                        {
                                            ChangeQtyForLot(this.drpProcessWayEdit, oqcDetail, oqcDeatilEC.NgQty, "N");
                                        }
                                        else
                                        {
                                            ChangeQtyForLot(this.drpProcessWayEdit, oqcDetail, oqcDetail.Qty);
                                        }
                                    }
                                    #region 注释

                                    #endregion

                                    oqcDetail.QcPassQty = oqcDetail.Qty - oqcDetail.ReturnQty;
                                    _OQCFacade.UpdateOQCDetail(oqcDetail);





                                    #region 在invinouttrans表中增加一条数据
                                    WarehouseFacade facade = new WarehouseFacade(base.DataProvider);
                                    DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                                    Domain.OQC.OQC asnIqcHead = (Domain.OQC.OQC)_OQCFacade.GetOQC(oqcDetail.OqcNo);
                                    InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                                    trans.CartonNO = string.Empty;
                                    trans.DqMCode = oqcDetail.DQMCode;
                                    trans.FacCode = string.Empty;
                                    trans.FromFacCode = string.Empty;
                                    trans.FromStorageCode = string.Empty;
                                    trans.InvNO = oqcDetail.CarInvNo;//.InvNo;
                                    trans.InvType = asnIqcHead.OqcType;
                                    trans.LotNo = string.Empty;
                                    trans.MaintainDate = dbTime1.DBDate;
                                    trans.MaintainTime = dbTime1.DBTime;
                                    trans.MaintainUser = this.GetUserCode();
                                    trans.MCode = oqcDetail.MCode;
                                    trans.ProductionDate = 0;
                                    trans.Qty = oqcDetail.Qty;
                                    trans.Serial = 0;
                                    trans.StorageAgeDate = 0;
                                    trans.StorageCode = string.Empty;
                                    trans.SupplierLotNo = string.Empty;
                                    trans.TransNO = oqcDetail.OqcNo;// asnIqc.IqcNo;
                                    trans.TransType = "OUT";
                                    trans.Unit = string.Empty;
                                    trans.ProcessType = "OQCSQE";
                                    facade.AddInvInOutTrans(trans);

                                    #endregion

                                }
                                #endregion
                            }
                            else
                            {
                                WebInfoPublish.PublishInfo(this, "没有获取有效数据", this.languageComponent1);
                                this.DataProvider.RollbackTransaction();
                                return;
                            }
                        }

                        #region 4)检查当前OQC检验单号下OQC单明细对应缺陷明细表(TBLOQCDETAILEC)中所有NGFLAG，SQESTATUS不为空时
                        //if (CheckNGFlagIsAllYN() && CheckSQEStatusIsNotEmpty())
                        //{
                        if (CheckSQEStatusIsNotEmpty1())
                        {
                            if (CheckSQEStatusHaveReturn())
                            {

                                if (_OQC != null)
                                {
                                    _OQC.Status = OQCStatus.OQCStatus_SQEFail;
                                    _OQCFacade.UpdateOQC(_OQC);
                                }

                                CARTONINVOICES cartonInvoices = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoicesByOqcNo(OQCNoQS);
                                if (cartonInvoices != null)
                                {
                                    cartonInvoices.STATUS = CartonInvoices_STATUS.Status_Release;
                                    _WarehouseFacade.UpdateCartoninvoices(cartonInvoices);
                                }
                            }
                            else
                            {

                                if (_OQC != null)
                                {
                                    _OQC.Status = OQCStatus.OQCStatus_OQCClose;

                                    _OQCFacade.UpdateOQC(_OQC);


                                    Pick pick = (Pick)_WarehouseFacade.GetPick(_OQC.PickNo);
                                    if (pick == null)
                                        throw new Exception(_OQC.PickNo + "拣货任务令不存在！");
                                    pick.Status = PickHeadStatus.PickHeadStatus_PackingListing;
                                    pick1 = pick;
                                    _WarehouseFacade.UpdatePick(pick);

                                }

                                CARTONINVOICES cartonInvoices = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoicesByOqcNo(OQCNoQS);
                                if (cartonInvoices != null)
                                {
                                    cartonInvoices.STATUS = CartonInvoices_STATUS.Status_OQCClose;
                                    _WarehouseFacade.UpdateCartoninvoices(cartonInvoices);
                                }
                            }

                        }
                        #endregion

                        //TODO: 2》	…
                        IQCFacade iqcFacade = new IQCFacade(base.DataProvider);
                        pick1 = (Pick)_WarehouseFacade.GetPick(_OQC.PickNo);
                        SendMail mail1 = ShareLib.ShareKit.OQCRejectThenGenerMail(_OQC, pick1, GetUserCode(), _WarehouseFacade, iqcFacade);
                        if (mail1 != null)
                            _WarehouseFacade.AddSendMail(mail1);

                        SystemSettingFacade baseSetting = new SystemSettingFacade(base.DataProvider);
                        MailOQCEc[] oqcEcs = _WarehouseFacade.GetOQCMailEc(_OQC.OqcNo);
                        SendMail mail2 = ShareLib.ShareKit.OQCSQEFinishThenGenerMail(oqcEcs, pick1, GetUserCode(), _OQC.CUser, baseSetting, _WarehouseFacade);
                        if (mail1 != null)
                            _WarehouseFacade.AddSendMail(mail2);

                        WebInfoPublish.PublishInfo(this, "确认成功", this.languageComponent1);
                        this.gridHelper.RequestData();

                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        WebInfoPublish.PublishInfo(this, "确认失败：" + ex.Message, this.languageComponent1);
                        this.DataProvider.RollbackTransaction();
                    }

                }
            }
        }


        protected void CheckIsUpdateSingleNG(OQCDetailEC ec)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            if (ec.SqeStatus == "Return" || ec.SqeStatus == "Reform")
            {
                object[] objs = _OQCFacade.GetAsnIQCDetailEcByIQCNoAndCartonNo(ec.OqcNo, ec.CartonNo);
                if (objs != null && objs.Length > 1)
                {
                    foreach (OQCDetailEC ee in objs)
                    {
                        ee.SqeStatus = ec.SqeStatus;
                        ee.SQERemark1 = ec.SQERemark1;
                        _OQCFacade.UpdateOQCDetailEC(ee);
                    }
                }
            }
            else
            {
                object[] objs = _OQCFacade.GetAsnIQCDetailEcByIQCNoAndCartonNo(ec.OqcNo, ec.CartonNo);
                if (objs != null && objs.Length > 1)
                {
                    foreach (OQCDetailEC ee in objs)
                    {
                        if (ee.SqeStatus == "Return" || ee.SqeStatus == "Reform")
                        {
                            ee.SqeStatus = string.Empty;
                            ee.SQERemark1 = string.Empty;
                            _OQCFacade.UpdateOQCDetailEC(ee);
                        }
                    }
                }
            }

        }


        protected void cmdReturned2Inspection_ServerClick(object sender, EventArgs e)
        {




            string oqcNo = txtOQCNoQuery.Text;
            if (String.IsNullOrEmpty(oqcNo))
            {
                WebInfoPublish.Publish(this, "OQC检验单号不能为空！", this.languageComponent1);
                return;
            }

            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            Domain.OQC.OQC _OQC = _OQCFacade.GetOQC(OQCNoQS) as Domain.OQC.OQC;
            if (_OQC.Status != OQCStatus.OQCStatus_SQEJudge)
            {
                WebInfoPublish.Publish(this, "OQC检验单状态不是SQE判定！", this.languageComponent1);
                return;


            }
            try
            {
                this.DataProvider.BeginTransaction();

                //退回二次检验
                ToReturned2Inspection(oqcNo);

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {

                this.DataProvider.RollbackTransaction();

                WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
                return;
            }


            WebInfoPublish.Publish(this, "退回二次检验成功", this.languageComponent1);

            this.gridHelper.RequestData();//刷新页面

        }
        private void ToReturned2Inspection(string oqcNo)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }


            Domain.OQC.OQC oqc = (Domain.OQC.OQC)_OQCFacade.GetOQC(oqcNo);
            if (oqc != null)
            {
                //1、更新送检单表 TBLOQC
                oqc.Status = OQCStatus.OQCStatus_Cancel;
                _OQCFacade.UpdateOQC(oqc);

                //2、产生一个新OQC检验单号,内容来源于原 OQC检验单号,涉及表：OQC送检单(TBLOQC)、OQC送检单明细(TBLOQCDETAIL)、OQC送检单明细SN(TBLOQCDETAILSN)
                string newOqcNo = this.CreateNewIqcNo(oqcNo);

                #region 1)送检单TBLOQC
                //1)送检单TBLOQC
                Domain.OQC.OQC newOqc = _OQCFacade.CreateNewOqc();
                newOqc.OqcNo = newOqcNo;
                newOqc.OqcType = OQCType.OQCType_FullCheck;
                newOqc.PickNo = oqc.PickNo;
                newOqc.CarInvNo = oqc.CarInvNo;
                newOqc.Status = OQCStatus.OQCStatus_Release;
                newOqc.AppDate = FormatHelper.GetNowDBDateTime(base.DataProvider).DBDate;
                newOqc.AppTime = FormatHelper.GetNowDBDateTime(base.DataProvider).DBTime;
                newOqc.InspDate = oqc.InspDate;
                newOqc.InspTime = oqc.InspTime;
                newOqc.QcStatus = "";
                newOqc.CUser = oqc.CUser;
                newOqc.CDate = oqc.CDate;
                newOqc.CTime = oqc.CTime;
                newOqc.MaintainUser = this.GetUserCode();
                _OQCFacade.AddOQC(newOqc);
                #endregion

                #region 2)送检单明细TBLOQCDETAIL
                //2)送检单明细TBLOQCDETAIL
                object[] objOqcDetail = _OQCFacade.GetOQCDetailByOqcNo(oqcNo);
                if (objOqcDetail != null && objOqcDetail.Length > 0)
                {
                    foreach (OQCDetail oldOqcDetail in objOqcDetail)
                    {
                        OQCDetail newOqcDetail = _OQCFacade.CreateNewOQCDetail();
                        newOqcDetail.OqcNo = newOqcNo;
                        newOqcDetail.CarInvNo = oldOqcDetail.CarInvNo;
                        newOqcDetail.CartonNo = oldOqcDetail.CartonNo;
                        newOqcDetail.MCode = oldOqcDetail.MCode;
                        newOqcDetail.DQMCode = oldOqcDetail.DQMCode;
                        newOqcDetail.Qty = oldOqcDetail.Qty;
                        newOqcDetail.QcPassQty = 0;
                        newOqcDetail.Unit = oldOqcDetail.Unit;
                        newOqcDetail.NgQty = 0;
                        newOqcDetail.ReturnQty = 0;
                        newOqcDetail.GiveQty = 0;
                        newOqcDetail.QcStatus = "";
                        newOqcDetail.GfHwItemCode = oldOqcDetail.GfHwItemCode;
                        newOqcDetail.GfPackingSeq = oldOqcDetail.GfPackingSeq;
                        newOqcDetail.Remark1 = "";
                        newOqcDetail.CUser = oldOqcDetail.CUser;
                        newOqcDetail.CDate = oldOqcDetail.CDate;
                        newOqcDetail.CTime = oldOqcDetail.CTime;
                        newOqcDetail.MaintainUser = this.GetUserCode();
                        _OQCFacade.AddOQCDetail(newOqcDetail);
                    }
                }


                #endregion

                #region 3)送检单明细SN TBLOQCDETAILSN
                object[] objOqcDetailSN = _OQCFacade.GetOQCDetailSNByOqcNo(oqcNo);
                if (objOqcDetailSN != null && objOqcDetailSN.Length > 0)
                {
                    foreach (OQCDetailSN oldOqcDetailSN in objOqcDetailSN)
                    {
                        OQCDetailSN newOqcDetailSN = _OQCFacade.CreateNewOQCDetailSN();
                        newOqcDetailSN.OqcNo = newOqcNo;
                        newOqcDetailSN.CarInvNo = oldOqcDetailSN.CarInvNo;
                        newOqcDetailSN.CartonNo = oldOqcDetailSN.CartonNo;
                        newOqcDetailSN.SN = oldOqcDetailSN.SN;
                        newOqcDetailSN.MCode = oldOqcDetailSN.MCode;
                        newOqcDetailSN.DQMCode = oldOqcDetailSN.DQMCode;
                        newOqcDetailSN.QcStatus = "";
                        newOqcDetailSN.MaintainUser = this.GetUserCode();
                        _OQCFacade.AddOQCDetailSN(newOqcDetailSN);
                    }
                }

                #endregion
            }
        }
        protected void cmdReturnedOQC_ServerClick(object sender, EventArgs e)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }



            string oqcNo = txtOQCNoQuery.Text;

            if (String.IsNullOrEmpty(oqcNo))
            {
                WebInfoPublish.Publish(this, "OQC检验单号不能为空！", this.languageComponent1);
                return;
            }


            Domain.OQC.OQC _OQC = _OQCFacade.GetOQC(OQCNoQS) as Domain.OQC.OQC;
            if (_OQC.Status != OQCStatus.OQCStatus_SQEJudge)
            {



                WebInfoPublish.Publish(this, "OQC检验单状态不是SQE判定！", this.languageComponent1);
                return;
            }
            try
            {
                this.DataProvider.BeginTransaction();

                Domain.OQC.OQC oqc = (Domain.OQC.OQC)_OQCFacade.GetOQC(oqcNo);
                if (oqc != null)
                {
                    oqc.Status = OQCStatus.OQCStatus_WaitCheck;
                    _OQCFacade.UpdateOQC(oqc);
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {


                this.DataProvider.RollbackTransaction();

                WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
                return;
            }



            WebInfoPublish.Publish(this, "退回OQC重检成功", this.languageComponent1);

            this.gridHelper.RequestData();//刷新页面

        }
        private string CreateNewIqcNo(string oldOqcNo)
        {
            //规则：原OQC检验单号 +_+ 两位流水号，如：OQCASN00000101_01
            WarehouseFacade warehouseFacade = new WarehouseFacade(this.DataProvider);
            string SNPrefix = oldOqcNo + "_";
            object objSerialBook = warehouseFacade.GetSerialBook(SNPrefix);
            if (objSerialBook == null)
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                SERIALBOOK serialBook = new SERIALBOOK();
                serialBook.SNPrefix = SNPrefix;
                serialBook.MaxSerial = "1";
                serialBook.MUser = this.GetUserCode();
                serialBook.MDate = dbDateTime.DBDate;
                serialBook.MTime = dbDateTime.DBTime;
                warehouseFacade.AddSerialBook(serialBook);

                return SNPrefix + "01";
            }
            else
            {
                SERIALBOOK serialBook = (SERIALBOOK)objSerialBook;
                if (serialBook.MaxSerial == "99")
                {
                    return "";
                }
                serialBook.MaxSerial = (Convert.ToInt32(serialBook.MaxSerial) + 1).ToString();
                warehouseFacade.UpdateSerialBook(serialBook);

                return serialBook.SNPrefix + serialBook.MaxSerial.PadLeft(2, '0');

            }
        }



        //返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FSQEJudgeMP.aspx"));
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            //if (pageAction == PageActionType.Query)
            //{
            //    this.cmdConfirm.Disabled = true;
            //}
            //if (pageAction == PageActionType.Add)
            //{
            //    this.cmdConfirm.Disabled = true;
            //}
            //if (pageAction == PageActionType.Update)
            //{
            //    this.cmdConfirm.Disabled = false;
            //}
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            OQCDetailEC oqcDetailEc = (OQCDetailEC)this.ViewState["OQCDetailECEdit"];
            if (oqcDetailEc != null)
            {
                oqcDetailEc.SqeStatus = FormatHelper.CleanString(this.drpProcessWayEdit.SelectedValue, 40);
                oqcDetailEc.SQERemark1 = FormatHelper.CleanString(this.txtMemoEdit.Text, 200);
                oqcDetailEc.SQECUser = this.GetUserName();
            }
            this.ViewState["OQCDetailECEdit"] = null;
            return oqcDetailEc;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            string oqcNo = row.Items.FindItemByKey("OQCNo").Text.Trim();
            string carInvNo = row.Items.FindItemByKey("CarInvNo").Text.Trim();
            string cartonNo = row.Items.FindItemByKey("CartonNo").Text.Trim();
            string dqmcode = row.Items.FindItemByKey("DQMCode").Text.Trim();
            string eCode = row.Items.FindItemByKey("ECode").Text.Trim();
            string sn = row.Items.FindItemByKey("SN").Text.Trim();
            string ECGCode = row.Items.FindItemByKey("NGType").Text.Trim();
            object[] obj = _OQCFacade.GetOQCDetailEC(ECGCode, eCode, carInvNo, oqcNo, cartonNo, dqmcode, sn);

            if (obj != null)
            {
                this.ViewState["OQCDetailECEdit"] = obj[0];//记录行实体
                return (OQCDetailEC)obj[0];
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.drpProcessWayEdit.SelectedIndex = 0;
                this.txtMemoEdit.Text = "";
                return;
            }

            try
            {
                this.drpProcessWayEdit.SelectedValue = ((OQCDetailEC)obj).SqeStatus;
            }
            catch (Exception)
            {

                this.drpProcessWayEdit.SelectedIndex = 0; ;
            }
            this.txtMemoEdit.Text = ((OQCDetailEC)obj).SQERemark1;
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblProcessWayEdit, this.drpProcessWayEdit, 40, true));
            manager.Add(new LengthCheck(this.lblMemoEdit, this.txtMemoEdit, 200, false));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((OQCDetailEC)obj).OqcNo,
                                ((OQCDetailEC)obj).DQMCode,
                                ((OQCDetailEC)obj).CartonNo,
                                ((OQCDetailEC)obj).EcgCode,
                                ((OQCDetailEC)obj).ECode,
                                ((OQCDetailEC)obj).NgQty.ToString(),
                                ((OQCDetailEC)obj).SN,
                                ((OQCDetailEC)obj).SQERemark1,
                                this.GetSQEStatusName(((OQCDetailEC)obj).SqeStatus),
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"OQCNo",
                                    "DQMCode",
                                    "CartonNo",
                                    "NGType",
                                    "NGDesc",
                                    "NGQty",	
                                    "SN",
                                    "Memo",
                                    "ProcessWay"};
        }

        #endregion

        #region Method

        //检查缺陷品数是否为Y/N
        /// <summary>
        /// //检查缺陷品数是否为Y/N
        /// </summary>
        /// <param name="oqcNo">OQC单据号</param>
        /// <param name="cartonNo">箱号</param>
        /// <returns></returns>
        private bool CheckNGFlagIsAllYN(string oqcNo, string cartonNo)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            object[] objOqcDetailEc = _OQCFacade.GetOQCDetailEC(oqcNo, cartonNo);
            if (objOqcDetailEc != null && objOqcDetailEc.Length > 1)
            {
                for (int i = 1; i < objOqcDetailEc.Length; i++)
                {
                    if (((OQCDetailEC)objOqcDetailEc[i - 1]).NgFlag != ((OQCDetailEC)objOqcDetailEc[i]).NgFlag)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool CheckNGFlagIsAllYN1(string iqcNo, string cartonNo, string NGFlag)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            if (NGFlag == "Y")
            {
                return true;
            }
            object[] objAsnIqcDetailEc = _OQCFacade.GetAsnIQCDetailEcByIQCNoAndCartonNo1(iqcNo, cartonNo);
            if (objAsnIqcDetailEc != null && objAsnIqcDetailEc.Length > 0)
            {
                OQCDetailEC ec = objAsnIqcDetailEc[0] as OQCDetailEC;
                if (ec.SqeStatus == "Return" || ec.SqeStatus == "Reform")
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckALLNG(string oqcNo, string cartonNo, string NGFlag)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            return _OQCFacade.GetAsnIQCDetailEc(oqcNo, cartonNo, NGFlag);

        }
        private bool CheckALLNGStatus(string oqcNo, string cartonNo, string NGFlag)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            return _OQCFacade.CheckALLNGStatus(oqcNo, cartonNo, NGFlag);

        }
        //检查缺陷品数是否为Y/N
        /// <summary>
        /// 检查缺陷品数是否为Y/N
        /// </summary>
        /// <returns></returns>
        private bool CheckNGFlagIsAllYN()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            object[] objAsnIqcDetailEc = _OQCFacade.GetOQCDetailECByOqcNo(OQCNoQS);
            if (objAsnIqcDetailEc != null && objAsnIqcDetailEc.Length > 1)
            {
                for (int i = 1; i < objAsnIqcDetailEc.Length; i++)
                {
                    if (((OQCDetailEC)objAsnIqcDetailEc[i - 1]).NgFlag != ((OQCDetailEC)objAsnIqcDetailEc[i]).NgFlag)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //检查SEQ状态是否为空
        /// <summary>
        /// 检查SEQ状态是否为空
        /// </summary>
        /// <returns>空：false;非空：true</returns>
        private bool CheckSQEStatusIsNotEmpty()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            object[] objAsnIqcDetailEc = _OQCFacade.GetOQCDetailECByOqcNo(OQCNoQS);
            if (objAsnIqcDetailEc != null)
            {
                foreach (OQCDetailEC oqcDetailEc in objAsnIqcDetailEc)
                {
                    if (string.IsNullOrEmpty(oqcDetailEc.SqeStatus))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //检查SEQ状态是否有Return记录
        /// <summary>
        /// 检查SEQ状态否有Return记录
        /// </summary>
        /// <returns>有：true;无：false</returns>
        private bool CheckSQEStatusHaveReturn()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            object[] objAsnIqcDetailEc = _OQCFacade.GetOQCDetailECByOqcNo(OQCNoQS);
            if (objAsnIqcDetailEc != null)
            {
                foreach (OQCDetailEC oqcDetailEc in objAsnIqcDetailEc)
                {
                    if (oqcDetailEc.SqeStatus == SQEStatus.SQEStatus_Return)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void ChangeQtyForSN(string sqeStatus, DropDownList drp, OQCDetail OQCDetail, int num)
        {
            switch (sqeStatus)
            {
                case "Return":
                    switch (drp.SelectedValue)
                    {
                        case "Return":
                            //选择本身数量不变
                            break;

                        case "Give":
                            OQCDetail.ReturnQty -= num;
                            OQCDetail.GiveQty += num;
                            break;

                    }
                    break;

                case "Give":
                    switch (drp.SelectedValue)
                    {
                        case "Return":
                            OQCDetail.GiveQty -= num;
                            OQCDetail.ReturnQty += num;
                            break;

                        case "Give":
                            //选择本身数量不变
                            break;

                    }
                    break;

            }
        }
        private void ChangeQtyForLot(DropDownList drp, OQCDetail OQCDetail, int num, string FF)
        {
            _OQCFacade = new OQCFacade(base.DataProvider);
            object[] objs = _OQCFacade.GetAsnIQCDetailEcByIQCNoAndCartonNo1(OQCDetail.OqcNo, OQCDetail.CartonNo);
            if (objs != null)
            {
                OQCDetailEC ec = objs[0] as OQCDetailEC;
                switch (ec.SqeStatus)
                {
                    case "Return":
                        OQCDetail.ReturnQty -= num;

                        break;

                    case "Give":

                        OQCDetail.GiveQty -= num;

                        break;

                }
            }

            switch (drp.SelectedValue)
            {
                case "Return":
                    OQCDetail.ReturnQty += num;

                    break;

                case "Give":

                    OQCDetail.GiveQty += num;

                    break;

            }

        }

        private void ChangeQtyForLot(DropDownList drp, OQCDetail OQCDetail, int num)
        {
            switch (drp.SelectedValue)
            {
                case "Return":
                    OQCDetail.ReturnQty += num;

                    break;

                case "Give":

                    OQCDetail.GiveQty += num;

                    break;

            }
        }
        private bool CheckSQEStatusIsNotEmpty1()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            //bool NGFlg = true;
            //bool SQEVal = false;
            //bool FF = true;
            object[] objAsnIqcDetailEc = _OQCFacade.GetAsnIQCDetailEcByIqcNo(OQCNoQS, "Y");
            if (objAsnIqcDetailEc == null)
            {
                //return true;
                //判断非整箱时，是否每个都填写啦
                object[] objAsnIqcDetailEc1 = _OQCFacade.GetAsnIQCDetailEcByIqcNo(OQCNoQS, "N");
                {
                    if (objAsnIqcDetailEc1 == null)
                        return true;
                    else
                    {
                        foreach (OQCDetailEC ee in objAsnIqcDetailEc1)
                        {
                            if (string.IsNullOrEmpty(ee.SqeStatus))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            else
            {
                foreach (OQCDetailEC ec in objAsnIqcDetailEc)
                {
                    //OQCDetailEC ec = objAsnIqcDetailEc[0] as OQCDetailEC;
                    if (string.IsNullOrEmpty(ec.SqeStatus))
                    {
                        return false;
                    }
                    if (ec.SqeStatus == "Return" || ec.SqeStatus == "Reform")
                    {
                    }
                    else
                    {
                        object[] objAsnIqcDetailEc1 = _OQCFacade.GetAsnIQCDetailEcByIqcNo(OQCNoQS, "N", ec.CartonNo);
                        {
                            if (objAsnIqcDetailEc1 != null)
                            //return true;

                            //else
                            {
                                foreach (OQCDetailEC ee in objAsnIqcDetailEc1)
                                {
                                    if (string.IsNullOrEmpty(ee.SqeStatus))
                                    {
                                        return false;
                                    }
                                }
                                //return true;
                            }
                        }
                    }
                }
                return true;
            }
        }
        #endregion


        //导出OQC异常联络单
        protected void cmdExportOQCACL_ServerClick(object sender, EventArgs e)
        {
            //TODO：未完成，具体值没有获取
            string fileName = "AbnormalContactList.xlsx";
            ExportExcel(fileName);
        }
        #region 导出Excel

        //导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="fileName">模板名称</param>
        private void ExportExcel(string fileName)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            string DQMCode = string.Empty;
            string CustmCode = string.Empty;
            string ApplyDate = string.Empty;
            string InvNo = string.Empty;
            string DQCHLDesc = string.Empty;
            string VendorName = string.Empty;
            string Qty = string.Empty;
            string ecCode = string.Empty;
            string reForm = string.Empty; // "✔";
            string Accept = string.Empty;
            string Give = string.Empty;
            string reTurn = string.Empty;
            string reMark = string.Empty;
            string CUser = string.Empty;
            string _OQCNo = OQCNoQS;//add by sam
            DataSet ds = _OQCFacade.GetOQCandInfoByOQCNo(_OQCNo);
            if (ds != null && ds.Tables.Count > 0)
            {
                //DQMCode = ds.Tables[0].Rows[0]["dqmcode"].ToString();
                //CustmCode = ds.Tables[0].Rows[0]["custmcode"].ToString();
                ApplyDate = FormatHelper.ToDateString(int.Parse(ds.Tables[0].Rows[0]["cdate"].ToString()));
                InvNo = ds.Tables[0].Rows[0]["invno"].ToString() + "/" + _OQCNo;
                //DQCHLDesc = ds.Tables[0].Rows[0]["mchlongdesc"].ToString();
                VendorName = ds.Tables[0].Rows[0]["vendorname"].ToString();

                //Qty = ds.Tables[0].Rows[0]["qty"].ToString();

                DataSet ds1 = _OQCFacade.GetOQCandInfoByOQCNoAndPickNo(_OQCNo, ds.Tables[0].Rows[0]["pickno"].ToString());
                if (ds1 != null && ds1.Tables.Count > 0)
                {
                    for (int v = 0; v < ds1.Tables[0].Rows.Count; v++)
                    {
                        DQMCode += ds1.Tables[0].Rows[v]["dqmcode"].ToString() + ";";
                        CustmCode += ds1.Tables[0].Rows[v]["custmcode"].ToString() + ";";
                        DQCHLDesc += ds1.Tables[0].Rows[v]["mchlongdesc"].ToString() + ";";
                        Qty += Int32.Parse(ds1.Tables[0].Rows[v]["qty"].ToString());
                    }
                }
            }

            object[] objs = _OQCFacade.GetOQCDetailECByOqcNo(_OQCNo);
            if (objs == null)
            {
                WebInfoPublish.PublishInfo(this, "OQC无异常信息", this.languageComponent1);
                return;
            }
            foreach (OQCDetailEC ec in objs)
            {
                switch (ec.SqeStatus)
                {
                    case "Return":
                        reTurn = "✔";
                        reMark += "@" + ec.Remark1;
                        break;
                    case "Accept":
                        Accept = "✔";
                        reMark += "@" + ec.Remark1;
                        break;
                    case "Reform":
                        reForm = "✔";
                        reMark += "@" + ec.Remark1;
                        break;
                    case "Give":
                        Give = "✔";
                        reMark += "@" + ec.Remark1;
                        break;
                }
                CUser = ec.CUser;
            }
            UserFacade userFacade = new UserFacade(this.DataProvider);
            User user = userFacade.GetUser(CUser) as User;
            if (user != null)
            {
                CUser = user.UserName;
            }
            ExportExcelHelper excelHelper = new ExportExcelHelper(this.Page, this.VirtualHostRoot, DownloadPath, fileName);
            int i = 1;
            int j = 1;
            excelHelper.AddCellValue(i + 1, j + 2, _OQCNo);//OQC单号
            excelHelper.AddCellValue(i + 3, j + 2, DQMCode);//鼎桥物料编码
            excelHelper.AddCellValue(i + 3, j + 4, CustmCode);//供应商为华为时，华为物料编码
            excelHelper.AddCellValue(i + 3, j + 6, ApplyDate);//iQC申请日期
            excelHelper.AddCellValue(i + 3, j + 8, InvNo);//SAP单据号/IQC检验单号
            excelHelper.AddCellValue(i + 4, j + 2, DQCHLDesc);//鼎桥物料描述基础数据中文长描述
            excelHelper.AddCellValue(i + 4, j + 4, VendorName);//供应商名称
            excelHelper.AddCellValue(i + 4, j + 6, Qty);//IQC送检总数
            excelHelper.AddCellValue(i + 4, j + 8, CUser);//导出单据人
            excelHelper.AddCellValue(i + 5, j + 6, ecCode);//不良描述
            excelHelper.AddCellValue(i + 10, j + 2, Give);//让步接收
            excelHelper.AddCellValue(i + 10, j + 3, Accept);//特采放行
            excelHelper.AddCellValue(i + 10, j + 4, reForm);//供应商现场整改
            excelHelper.AddCellValue(i + 10, j + 5, reTurn); //退换货
            excelHelper.AddCellValue(i + 10, j + 7, reMark); //具体说明 
            //显示上传图片
            object[] objs_invdoc = _OQCFacade.GetUpLoadFilesByInvDocNo(_OQCNo, "OqcAbnormal");
            if (objs_invdoc != null)
            {
                int t = 13;
                string path = Server.MapPath(this.VirtualHostRoot + "InvDoc/OQC/");
                foreach (InvDoc doc in objs_invdoc)
                {
                    if (doc.DocType == ".jpg" || doc.DocType == ".png" || doc.DocType == ".jpeg" || doc.DocType == ".gif" || doc.DocType == ".bmp")
                    {
                        excelHelper.AddCellPicture(i + 13 + t, j + 1, i + 24 + t, j + 4, path + doc.ServerFileName);
                        t += 13;
                    }
                }
                //excelHelper.AddCellPicture(i + 13, j + 1, i + 24, j + 4, @"C:\Users\Jinger.S.Yan\Desktop\222.jpg");
            }
            //excelHelper.AddCellPicture(i + 13, j + 1, i + 24, j + 4, @"C:\Users\Jinger.S.Yan\Desktop\222.jpg");

            excelHelper.ExportExcel();

            if (!string.IsNullOrEmpty(excelHelper.ErrorMsg))
            {
                WebInfoPublish.PublishInfo(this, excelHelper.ErrorMsg, this.languageComponent1);
            }
        }

        /// <summary>
        /// 下载路径
        /// </summary>
        [Browsable(false)]
        public string DownloadPath
        {
            get
            {
                return string.Format(@"{0}{1}/", this.VirtualHostRoot, _downloadPath.Trim('\\', '/').Replace('\\', '/'));
            }
        }
        private string _downloadPath = @"\download\";//下载文件夹名称 
        #endregion

    }
}
