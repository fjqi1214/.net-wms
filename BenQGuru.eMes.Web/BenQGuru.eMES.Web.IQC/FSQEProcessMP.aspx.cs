using System;
using System.Collections.Generic;
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
using BenQGuru.eMES.SAPRFCService;
using BenQGuru.eMES.SAPRFCService.Domain;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Domain.IQC;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Common;
using System.ComponentModel;
using BenQGuru.eMES.Domain.OQC;
using System.Text;


namespace BenQGuru.eMES.Web.IQC
{
    public partial class FSQEProcessMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        private IQCFacade _IQCFacade = null;
        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;
        private WarehouseFacade facade = null;
        private InventoryFacade _Invenfacade = null;
        /// <summary>
        /// SN
        /// </summary>
        public string SN { get; set; }

        /// <summary>
        /// 页面跳转带入IQC检验单号
        /// </summary>
        public string IQCNoQS
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetRequestParam("IQCNo")))
                {
                    return this.GetRequestParam("IQCNo");
                }
                return string.Empty;
            }
            set
            {
                IQCNoQS = value;
            }
        }

        /// <summary>
        /// 页面跳转带入IQC检验单号
        /// </summary>
        public string IQCNoQSLine
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetRequestParam("IQCNoLine")))
                {
                    return this.GetRequestParam("IQCNoLine");
                }
                return string.Empty;
            }
            set
            {
                IQCNoQSLine = value;
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
        /// 页面跳转带入入库类型
        /// </summary>
        public string StTypeQS
        {
            get
            {
                if (!string.IsNullOrEmpty(this.GetRequestParam("StType")))
                {
                    return this.GetRequestParam("StType");
                }
                return string.Empty;
            }
            set
            {
                StTypeQS = value;
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
                this.txtIQCNoQuery.Text = IQCNoQS;
                if (MControlTypeQS == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_LOT)
                {
                    this.cmdCheckSN.Disabled = true;
                }
                else
                {
                    this.cmdCheckSN.Disabled = false;
                }
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
            if (StTypeQS == InInvType.PGIR)
            {
                this.drpProcessWayEdit.Items.Add(new ListItem("让步接收", SQEStatus.SQEStatus_Give));
                this.drpProcessWayEdit.Items.Add(new ListItem("特采", SQEStatus.SQEStatus_Accept));
            }
            else
            {
                object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("SQESTATUS");
                if (parameters != null && parameters.Length > 0)
                {
                    foreach (Domain.BaseSetting.Parameter parameter in parameters)
                    {
                        this.drpProcessWayEdit.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
                    }
                }
            }
            this.drpProcessWayEdit.SelectedIndex = 0;
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("IQCNo", "IQC检验单", null);
            this.gridHelper.AddDataColumn("StNo", "ASN单号", true);
            this.gridHelper.AddDataColumn("StLine", "ASN单行项目", true);

            this.gridHelper.AddColumn("STORAGECODE1234", "入库库位", null);
            this.gridHelper.AddColumn("DQMCODE", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("DQMCODEMDESC", "鼎桥物料描述", null);

            this.gridHelper.AddColumn("CartonNo", "箱号", null);
            this.gridHelper.AddColumn("NGTypeDesc", "缺陷类型", null);
            this.gridHelper.AddColumn("NGType", "缺陷类型", true);
            this.gridHelper.AddColumn("NGDesc", "缺陷描述", null);
            this.gridHelper.AddDataColumn("ECode", "缺陷代码", true);
            this.gridHelper.AddColumn("NGQty", "缺陷品数", null);
            this.gridHelper.AddColumn("SN", "SN", null);
            this.gridHelper.AddColumn("IQCMemo", "IQC备注", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("ProcessWay", "处理方式", null);
            this.gridHelper.AddDataColumn("NGFlag", "缺陷种类", true);
            this.gridHelper.AddColumn("SQECUSER", "判定人", null);
            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();//页面初始化加载grid数据
        }

        protected override DataRow GetGridRow(object obj)
        {

            DataRow row = this.DtSource.NewRow();
            _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            row["IQCNo"] = ((AsnIQCDetailEc)obj).IqcNo;
            row["StNo"] = ((AsnIQCDetailEc)obj).StNo;
            row["StLine"] = ((AsnIQCDetailEc)obj).StLine;

            _InventoryFacade = new InventoryFacade(base.DataProvider);
            _IQCFacade = new IQCFacade(base.DataProvider);
            ASN a = (ASN)_InventoryFacade.GetASN(((AsnIQCDetailEc)obj).StNo);
            if (a != null)
                row["STORAGECODE1234"] = a.StorageCode;
            else
                row["STORAGECODE1234"] = string.Empty;

            AsnIQC iqc = (AsnIQC)_IQCFacade.GetAsnIQC(((AsnIQCDetailEc)obj).IqcNo);
            if (iqc != null)
                row["DQMCODE"] = iqc.DQMCode;
            else
                row["DQMCODE"] = string.Empty;


            Domain.MOModel.Material m = (Domain.MOModel.Material)_InventoryFacade.GetMaterialByDQMCode(iqc.DQMCode);
            if (m != null)
                row["DQMCODEMDESC"] = m.MchlongDesc;
            else
                row["DQMCODEMDESC"] = string.Empty;

            row["CartonNo"] = ((AsnIQCDetailEc)obj).CartonNo;
            row["NGType"] = ((AsnIQCDetailEc)obj).EcgCode;


            object[] objs_ecg = _SystemSettingFacade.GetErrorGroupcode(((AsnIQCDetailEc)obj).EcgCode);
            if (objs_ecg != null)
                row["NGTypeDesc"] = (objs_ecg[0] as ErrorCodeGroupA).ErrorCodeGroupDescription;
            else
                row["NGTypeDesc"] = ((AsnIQCDetailEc)obj).EcgCode;


            object[] objs_ec = _SystemSettingFacade.GetErrorcodeByEcode(((AsnIQCDetailEc)obj).ECode);
            if (objs_ec != null)
            { row["NGDesc"] = (objs_ec[0] as ErrorCodeA).ErrorDescription; }
            else
            { row["NGDesc"] = ((AsnIQCDetailEc)obj).ECode; }
            //row["NGDesc"] = ((AsnIQCDetailEc)obj).ECode;
            row["ECode"] = ((AsnIQCDetailEc)obj).ECode;
            row["NGQty"] = ((AsnIQCDetailEc)obj).NgQty;
            row["SN"] = ((AsnIQCDetailEc)obj).SN;
            row["IQCMemo"] = ((AsnIQCDetailEc)obj).Remark1;
            row["Memo"] = ((AsnIQCDetailEc)obj).SQERemark1;
            row["ProcessWay"] = this.GetSQEStatusName(((AsnIQCDetailEc)obj).SqeStatus);
            row["NGFlag"] = ((AsnIQCDetailEc)obj).NgFlag;// Remark1;
            row["SQECUSER"] = ((AsnIQCDetailEc)obj).SQECUser;// Remark1;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }

            return _IQCFacade.QueryAsnIQCDetailEc(IQCNoQS, SN, inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            return _IQCFacade.QueryAsnIQCDetailEcCount(IQCNoQS, SN);
        }

        #endregion

        #region Button
        //检索
        protected void cmdCheckSN_ServerClick(object sender, EventArgs e)
        {
            this.SN = FormatHelper.CleanString(this.txtSNQuery.Text);
            this.gridHelper.RequestData();
        }

        //确认
        protected void cmdConfirm_ServerClick(object sender, EventArgs e)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            AsnIQC _iqc = _IQCFacade.GetAsnIQC(IQCNoQS) as AsnIQC;
            if (_iqc.Status == ASNLineStatus.IQCClose)
            {
                WebInfoPublish.PublishInfo(this, "状态已经为判定完成，不能修改", this.languageComponent1);
                return;
            }

            #region add by sam 卡控状态  2016年4月15日
            if (_iqc.Status != IQCStatus.IQCStatus_SQEJudge)
            {
                WebInfoPublish.Publish(this, "状态不是SQE判定，不能判定", this.languageComponent1);
                return;
            }
            #endregion

            //bool isReceive = false;
            //List<string> iqclist = new List<string>();
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
                                object obj_value = _IQCFacade.GetParaValueByText(sqeStatus);
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
                                #region 更新TBLASNIQCDETAILEC,TBLASNIQCDETAIL
                                AsnIQCDetailEc asnIQCDetailEc = (AsnIQCDetailEc)obj;

                                //3)当同一箱在送检单明细对应缺陷明细表(TBLASNIQCDETAILEC)中行记录中NGFLAG=Y也有NGFLAG=N时
                                //if (!CheckNGFlagIsAllYN(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo) && MControlTypeQS == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                                if (!CheckNGFlagIsAllYN1(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, asnIQCDetailEc.NgFlag))
                                {
                                    //object[] objAsnIqcDetailEc = _IQCFacade.GetAsnIQCDetailEc(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo);
                                    //if (objAsnIqcDetailEc != null)
                                    //{
                                    //    if (asnIQCDetailEc.NgFlag != ((AsnIQCDetailEc)objAsnIqcDetailEc[0]).NgFlag)
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
                                    if (!CheckALLNG(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, asnIQCDetailEc.NgFlag))
                                    {
                                        this.DataProvider.RollbackTransaction();
                                        WebInfoPublish.PublishInfo(this, "有整箱不良，必须先维护整箱不良", this.languageComponent1);
                                        //continue;
                                        return;
                                    }
                                    if (CheckALLNGStatus(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, asnIQCDetailEc.NgFlag))
                                    {
                                        this.DataProvider.RollbackTransaction();
                                        WebInfoPublish.PublishInfo(this, "整箱不良为判退，不能再维护单件不良", this.languageComponent1);
                                        return;
                                    }

                                }
                                //检查整箱入库时，如果是退货状态，单件不良也更新为退换货
                                if (asnIQCDetailEc.NgFlag.ToUpper() == "Y")
                                {
                                    CheckIsUpdateSingleNG(asnIQCDetailEc);
                                }
                                asnIQCDetailEc.MaintainUser = GetUserCode();
                                asnIQCDetailEc.SQECUser = GetUserCode();
                                _IQCFacade.UpdateAsnIQCDetailEc(asnIQCDetailEc);

                                AsnIQCDetail asnIQCDetail = (AsnIQCDetail)_IQCFacade.GetAsnIQCDetail(Convert.ToInt32(asnIQCDetailEc.StLine), asnIQCDetailEc.IqcNo, asnIQCDetailEc.StNo);
                                if (asnIQCDetail != null)
                                {
                                    if (!string.IsNullOrEmpty(sqeStatus))
                                    {
                                        //如果不为空，说明是更新
                                        if (asnIQCDetailEc.NgFlag == "N")
                                        {
                                            ChangeQtyForSN(sqeStatus, this.drpProcessWayEdit, asnIQCDetail, asnIQCDetailEc.NgQty);
                                        }
                                        else
                                        {
                                            ChangeQtyForSN(sqeStatus, this.drpProcessWayEdit, asnIQCDetail, asnIQCDetail.Qty);
                                        }
                                    }
                                    else
                                    {

                                        if (asnIQCDetailEc.NgFlag == "N")
                                        {
                                            ChangeQtyForLot(this.drpProcessWayEdit, asnIQCDetail, asnIQCDetailEc.NgQty, "N");
                                        }
                                        else
                                        {
                                            ChangeQtyForLot(this.drpProcessWayEdit, asnIQCDetail, asnIQCDetail.Qty);
                                        }
                                    }
                                    asnIQCDetail.QcPassQty = asnIQCDetail.Qty - asnIQCDetail.ReturnQty - asnIQCDetail.ReformQty;
                                    _IQCFacade.UpdateAsnIQCDetail(asnIQCDetail);
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

                        WebInfoPublish.PublishInfo(this, "确认成功", this.languageComponent1);
                        this.gridHelper.RequestData();

                        this.DataProvider.CommitTransaction();

                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.PublishInfo(this, "确认失败：" + ex.Message, this.languageComponent1);

                        Log.Error(ex.StackTrace);

                    }
                }
            }

            #region SAP回传 add by sam 2016年3月21日
            //if (isReceive)
            //{
            //    PoToSap(iqclist.Distinct().ToArray());
            //}
            #endregion
        }
        protected void CheckIsUpdateSingleNG(AsnIQCDetailEc ec)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            if (ec.SqeStatus == "Return" || ec.SqeStatus == "Reform")
            {
                object[] objs = _IQCFacade.GetAsnIQCDetailEcByIQCNoAndCartonNo(ec.IqcNo, ec.CartonNo);
                if (objs != null && objs.Length > 1)
                {
                    foreach (AsnIQCDetailEc ee in objs)
                    {
                        ee.SqeStatus = ec.SqeStatus;
                        //ee.Remark1 = ec.Remark1;
                        ee.SQERemark1 = ec.SQERemark1;
                        ee.MaintainUser = GetUserCode();
                        _IQCFacade.UpdateAsnIQCDetailEc(ee);
                    }
                }
            }
            else
            {
                object[] objs = _IQCFacade.GetAsnIQCDetailEcByIQCNoAndCartonNo(ec.IqcNo, ec.CartonNo);
                if (objs != null && objs.Length > 1)
                {
                    foreach (AsnIQCDetailEc ee in objs)
                    {
                        if (ee.SqeStatus == "Return" || ee.SqeStatus == "Reform")
                        {
                            ee.SqeStatus = string.Empty;
                            ee.Remark1 = string.Empty;
                            ee.MaintainUser = GetUserCode();
                            _IQCFacade.UpdateAsnIQCDetailEc(ee);
                        }
                    }
                }
            }

        }


        protected void cmdRejectbt_ServerClick(object sender, EventArgs e)
        {





            if (string.IsNullOrEmpty(txtIQCNoQuery.Text))
            {
                WebInfoPublish.PublishInfo(this, "IQC单号不能为空！", this.languageComponent1);
                return;

            }


            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            string iqcNo = txtIQCNoQuery.Text;
            AsnIQC iqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);

            if (iqc.Status != IQCStatus.IQCStatus_SQEJudge)
            {
                WebInfoPublish.PublishInfo(this, "检验单状态必须是判断！", this.languageComponent1);
                return;


            }
            if (iqc.StType == InInvType.PGIR)
            {
                WebInfoPublish.PublishInfo(this, "PGI退料不能拒收！", this.languageComponent1);
                return;

            }
            try
            {


                //拒收
                bool result = ToRejectbt(iqcNo);
                if (!result)
                {
                    WebInfoPublish.PublishInfo(this, "上传SAP失败 ！", this.languageComponent1);
                    return;

                }
            }
            catch (Exception ex)
            {

                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);

                return;

            }

            WebInfoPublish.Publish(this, "拒收成功", this.languageComponent1);


            this.gridHelper.RequestData();//刷新页面


            //#region SAP回传 add by sam 2016年3月21日

            //if (isReceive)
            //{
            //    PoToSap(iqclist.ToArray());
            //}
            //#endregion
        }
        private bool ToRejectbt(string iqcNo)
        {
            try
            {
                if (_IQCFacade == null)
                {
                    _IQCFacade = new IQCFacade(base.DataProvider);
                }
                if (facade == null)
                {
                    facade = new WarehouseFacade(base.DataProvider);
                }
                _InventoryFacade = new InventoryFacade(base.DataProvider);
                //1、更新送检单 TBLASNIQC
                //更改增加TBLASNIQC的状态，IQCRejection:IQC拒收
                AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);

                base.DataProvider.BeginTransaction();
                if (asnIqc != null)
                {
                    asnIqc.Status = IQCStatus.IQCStatus_IQCRejection;
                    _IQCFacade.UpdateAsnIQC(asnIqc);

                    #region 在invinouttrans表中增加一条数据
                    //WarehouseFacade facade = new WarehouseFacade(this.DataProvider);

                    //ASN asn = (ASN)domainObject;
                    DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                    trans.CartonNO = string.Empty;
                    trans.DqMCode = asnIqc.DQMCode;
                    trans.FacCode = string.Empty;
                    trans.FromFacCode = string.Empty;
                    trans.FromStorageCode = string.Empty;
                    trans.InvNO = asnIqc.InvNo;
                    trans.InvType = asnIqc.IqcType;
                    trans.LotNo = string.Empty;
                    trans.MaintainDate = dbTime1.DBDate;
                    trans.MaintainTime = dbTime1.DBTime;
                    trans.MaintainUser = "PDA";
                    trans.MCode = asnIqc.MCode;
                    trans.ProductionDate = 0;
                    trans.Qty = asnIqc.Qty;
                    trans.Serial = 0;
                    trans.StorageAgeDate = 0;
                    trans.StorageCode = string.Empty;
                    trans.SupplierLotNo = string.Empty;
                    trans.TransNO = asnIqc.IqcNo;
                    trans.TransType = "IN";
                    trans.Unit = string.Empty;
                    trans.ProcessType = "IQC";
                    facade.AddInvInOutTrans(trans);
                    #endregion
                }
                //2、更新ASN明细表 TBLASNDETAIL
                //更改增加TBLASNIQC的状态，IQCRejection:IQC拒收
                ASN asn = null;
                List<ASNDetail> asnDetailList = new List<ASNDetail>();
                List<AsnIQCDetail> iqcDetails = new List<AsnIQCDetail>();
                object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(iqcNo);


                Hashtable ht = new Hashtable();
                if (objAsnIqcDetail != null)
                {
                    asn = _InventoryFacade.GetASN((objAsnIqcDetail[0] as AsnIQCDetail).StNo) as ASN;
                    foreach (AsnIQCDetail asnIqcDetail in objAsnIqcDetail)
                    {

                        ASNDetail asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(Convert.ToInt32(asnIqcDetail.StLine), asnIqcDetail.StNo);
                        if (asnDetail != null)
                        {
                            asnDetailList.Add(asnDetail);
                            if (!ht.ContainsKey(asnIqcDetail.StNo))
                            {
                                ht.Add(asnIqcDetail.StNo, asnIqcDetail.StNo);
                            }
                            asnDetail.Status = ASNDetail_STATUS.ASNDetail_IQCRejection;
                            _InventoryFacade.UpdateASNDetail(asnDetail);

                            object[] objs_item = facade.GetASNDetailItembyStnoAndStline(asnDetail.StNo, asnDetail.StLine);
                            if (objs_item != null)
                            {
                                foreach (Asndetailitem item in objs_item)
                                {
                                    item.QcpassQty = 0;
                                    item.ActQty = 0;
                                    facade.UpdateAsndetailitem(item);
                                }
                            }
                        }
                        iqcDetails.Add(asnIqcDetail);
                    }
                }

                //更新 asn状态（如果所有的IQC状态都是拒收，则改状态为拒收） 为IQCRejection:IQC拒收
                foreach (DictionaryEntry d in ht)
                {
                    bool FFlag = true;
                    object[] objs_asnds = _InventoryFacade.GetASNDetailByStNo(d.Key.ToString());
                    if (objs_asnds != null)
                    {
                        foreach (ASNDetail asnds in objs_asnds)
                        {
                            if (asnds.InitReceiveStatus != "Reject")
                            {
                                if (asnds.Status != ASNDetail_STATUS.ASNDetail_IQCRejection)
                                {
                                    FFlag = false;
                                    break;
                                }
                            }
                        }
                    }
                    if (FFlag)
                    {
                        object obj_asn = _InventoryFacade.GetASN(d.Key.ToString());
                        if (obj_asn != null)
                        {
                            asn = obj_asn as ASN;
                            asn.Status = ASN_STATUS.ASN_IQCRejection;
                            _InventoryFacade.UpdateASN(asn);
                        }
                    }
                }
                //TODO: 逻辑未提供
                //3、入库类型：TBLASNIQC.STTYPE为：POR:PO入库时SAP过帐处理.....
                if (_IQCFacade.CanToOnlocationStaus(asnIqc.StNo))
                {
                    if (!_IQCFacade.BeIQCReject(asnIqc.StNo))
                    {
                        asn.Status = ASNHeadStatus.OnLocation;

                    }
                    else
                    {

                        asn.Status = ASNHeadStatus.IQCRejection;
                    }
                    _InventoryFacade.UpdateASN(asn);
                }




                if (asn.StType == SAP_ImportType.SAP_POR)
                {
                    bool result = PoToSap(asnDetailList.ToArray());//asnDetailList.Distinct<string>().ToArray());
                    if (!result)
                    {
                        this.DataProvider.RollbackTransaction();
                        return false;
                    }


                }

                WarehouseFacade ware = new WarehouseFacade(base.DataProvider);
                ShareLib.ShareKit.IQCRejectThenGenerMail(asnIqc, asn, GetUserCode(), ware, _IQCFacade);
                this.DataProvider.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        //退回二次检验
        protected void cmdReturned2Inspection_ServerClick(object sender, EventArgs e)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            string iqcNo = txtIQCNoQuery.Text;

            if (string.IsNullOrEmpty(txtIQCNoQuery.Text))
            {
                WebInfoPublish.PublishInfo(this, "IQC单号不能为空！", this.languageComponent1);
                return;

            }
            AsnIQC iqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);

            if (iqc.Status != IQCStatus.IQCStatus_SQEJudge)
            {
                WebInfoPublish.PublishInfo(this, "IQC单状态不是SQE判定！", this.languageComponent1);
                return;


            }



            try
            {

                if (iqc.IqcType == OQCType.OQCType_FullCheck)
                {
                    WebInfoPublish.PublishInfo(this, "检验方式为[全检]，不能申请[退回二次检验]！", this.languageComponent1);
                    return;


                }
                this.DataProvider.BeginTransaction();

                //退回二次检验
                ToReturned2Inspection(iqcNo);

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
                return;


            }


            WebInfoPublish.Publish(this, "退回二次检验成功", this.languageComponent1);

            this.gridHelper.RequestData();//刷新页面

        }
        private void ToReturned2Inspection(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }


            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                //1、更新送检单表 TBLASNIQC
                asnIqc.Status = IQCStatus.IQCStatus_Cancel;  //原IQC变成关单的状态
                _IQCFacade.UpdateAsnIQC(asnIqc);

                //2、产生一个新IQC检验单号,内容来源于原IQC检验单号,涉及表：送检单(TBLASNIQC)、送检单明细(TBLASNIQCDETAIL)、送检单明细SN(TBLASNIQCDETAILSN)
                string newIqcNo = this.CreateNewIqcNo(iqcNo);

                #region 1)送检单TBLASNIQC
                //1)送检单TBLASNIQC
                AsnIQC newAsnIqc = _IQCFacade.CreateNewAsnIQC();
                newAsnIqc.IqcNo = newIqcNo;
                newAsnIqc.IqcType = IQCType.IQCType_FullCheck;
                newAsnIqc.StNo = asnIqc.StNo;
                newAsnIqc.InvNo = asnIqc.InvNo;
                newAsnIqc.StType = asnIqc.StType;
                newAsnIqc.Status = IQCStatus.IQCStatus_Release;
                newAsnIqc.AppDate = FormatHelper.GetNowDBDateTime(base.DataProvider).DBDate;
                newAsnIqc.AppTime = FormatHelper.GetNowDBDateTime(base.DataProvider).DBTime;
                newAsnIqc.InspDate = asnIqc.InspDate;
                newAsnIqc.InspTime = asnIqc.InspTime;
                newAsnIqc.CustmCode = asnIqc.CustmCode;
                newAsnIqc.MCode = asnIqc.MCode;
                newAsnIqc.DQMCode = asnIqc.DQMCode;
                newAsnIqc.MDesc = asnIqc.MDesc;
                newAsnIqc.Qty = asnIqc.Qty;
                newAsnIqc.QcStatus = "";
                newAsnIqc.VendorCode = asnIqc.VendorCode;
                newAsnIqc.VendorMCode = asnIqc.VendorMCode;
                newAsnIqc.Remark1 = "";
                newAsnIqc.CUser = asnIqc.CUser;
                newAsnIqc.CDate = asnIqc.CDate;
                newAsnIqc.CTime = asnIqc.CTime;
                newAsnIqc.MaintainUser = this.GetUserCode();
                _IQCFacade.AddAsnIQC(newAsnIqc);
                #endregion

                #region 2)送检单明细TBLASNIQCDETAIL
                //2)送检单明细TBLASNIQCDETAIL
                object[] objAsnIQCDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(iqcNo);
                if (objAsnIQCDetail != null && objAsnIQCDetail.Length > 0)
                {
                    foreach (AsnIQCDetail oldAsnIQCDetail in objAsnIQCDetail)
                    {
                        AsnIQCDetail newAsnIqcDetail = _IQCFacade.CreateNewAsnIQCDetail();
                        newAsnIqcDetail.IqcNo = newIqcNo;
                        newAsnIqcDetail.StNo = oldAsnIQCDetail.StNo;
                        newAsnIqcDetail.StLine = oldAsnIQCDetail.StLine;
                        newAsnIqcDetail.CartonNo = oldAsnIQCDetail.CartonNo;
                        newAsnIqcDetail.Qty = oldAsnIQCDetail.Qty;
                        newAsnIqcDetail.QcPassQty = 0;
                        newAsnIqcDetail.Unit = oldAsnIQCDetail.Unit;
                        newAsnIqcDetail.NgQty = 0;
                        newAsnIqcDetail.ReturnQty = 0;
                        newAsnIqcDetail.ReformQty = 0;
                        newAsnIqcDetail.GiveQty = 0;
                        newAsnIqcDetail.AcceptQty = 0;
                        newAsnIqcDetail.QcStatus = "";
                        newAsnIqcDetail.Remark1 = "";
                        newAsnIqcDetail.CUser = oldAsnIQCDetail.CUser;
                        newAsnIqcDetail.CDate = oldAsnIQCDetail.CDate;
                        newAsnIqcDetail.CTime = oldAsnIQCDetail.CTime;
                        newAsnIqcDetail.MaintainUser = this.GetUserCode();
                        _IQCFacade.AddAsnIQCDetail(newAsnIqcDetail);
                    }
                }


                #endregion

                #region 3)送检单明细SN TBLASNIQCDETAILSN
                object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(iqcNo);
                if (objAsnIqcDetailSN != null && objAsnIqcDetailSN.Length > 0)
                {
                    foreach (AsnIqcDetailSN oldAsnIqcDetailSN in objAsnIqcDetailSN)
                    {
                        AsnIqcDetailSN newAsnIqcDetailSN = _IQCFacade.CreateNewAsnIqcDetailSN();
                        newAsnIqcDetailSN.IqcNo = newIqcNo;
                        newAsnIqcDetailSN.StNo = oldAsnIqcDetailSN.StNo;
                        newAsnIqcDetailSN.StLine = oldAsnIqcDetailSN.StLine;
                        newAsnIqcDetailSN.CartonNo = oldAsnIqcDetailSN.CartonNo;
                        newAsnIqcDetailSN.Sn = oldAsnIqcDetailSN.Sn;
                        newAsnIqcDetailSN.QcStatus = "";
                        newAsnIqcDetailSN.Remark1 = "";
                        newAsnIqcDetailSN.CUser = oldAsnIqcDetailSN.CUser;
                        newAsnIqcDetailSN.CDate = oldAsnIqcDetailSN.CDate;
                        newAsnIqcDetailSN.CTime = oldAsnIqcDetailSN.CTime;
                        newAsnIqcDetailSN.MaintainUser = this.GetUserCode();
                        _IQCFacade.AddAsnIqcDetailSN(newAsnIqcDetailSN);
                    }
                }

                #endregion
            }
        }

        private string CreateNewIqcNo(string oldIqcNo)
        {
            //规则：原IQC检验单号+_+两位流水号，如：IQCASN00000101_01
            WarehouseFacade warehouseFacade = new WarehouseFacade(this.DataProvider);
            string newIqcNo = string.Empty;
            string SNPrefix = oldIqcNo + "_";
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

        //退回IQC重检
        protected void cmdReturnedQC_ServerClick(object sender, EventArgs e)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }



            string iqcNo = txtIQCNoQuery.Text;
            if (String.IsNullOrEmpty(iqcNo))
            {
                WebInfoPublish.Publish(this, "IQC检验单号不能为空！", this.languageComponent1);
                return;
            }

            AsnIQC iqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (iqc.Status != IQCStatus.IQCStatus_SQEJudge)
            {
                WebInfoPublish.Publish(this, "IQC检验单状态不是SQE判定！", this.languageComponent1);
                return;

            }
            try
            {

                if (iqc.IqcType == OQCType.OQCType_FullCheck)
                {
                    WebInfoPublish.Publish(this, "检验方式为[全检]，不能申请[退回IQC重检]！", this.languageComponent1);
                    return;

                }
                this.DataProvider.BeginTransaction();

                AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
                if (asnIqc != null)
                {
                    asnIqc.Status = IQCStatus.IQCStatus_Release;
                    _IQCFacade.UpdateAsnIQC(asnIqc);
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);

                return;
            }



            WebInfoPublish.Publish(this, "退回QC重检成功", this.languageComponent1);

            this.gridHelper.RequestData();//刷新页面

        }
        private void LogPO2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.PO> dns)
        {
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            _InventoryFacade = new InventoryFacade(this.DataProvider);
            foreach (BenQGuru.eMES.SAPRFCService.Domain.PO po in dns)
            {

                Po2Sap poLog = new Po2Sap();
                poLog.PONO = po.PONO;
                poLog.POLine = po.POLine;
                poLog.SerialNO = po.SerialNO;
                poLog.STNO = po.SerialNO;
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
                _InventoryFacade.AddPo2Sap(poLog);
            }
        }

        private bool PoToSap(object[] asnDetailList)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);

            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            #region add by sam
            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
            #endregion

            string stno = "";
            string dqmcode = "";
            string stline = "";
            bool isSuccess = true;

            foreach (ASNDetail asndetail in asnDetailList)
            {
                stno = asndetail.StNo;
                dqmcode = asndetail.DQMCode;
                stline = asndetail.StLine;
                //取在detailitem中，此箱分配到哪些po行
                object[] objs_item = _InventoryFacade.GetInvoiceLineFromASNDetailItem(stno, stline);
                if (objs_item == null)
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.Publish(this, "asndetailitem无数据", this.languageComponent1);
                    return false;
                }
                ASN asn = (ASN)_InventoryFacade.GetASN(stno);
                int count = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime);
                BenQGuru.eMES.SAPRFCService.POToSAP poToSAP = new POToSAP(this.DataProvider);
                List<PO> list = new List<PO>();
                List<PoLog> logList = new List<PoLog>();
                // item中每个invline进行操作
                foreach (Asndetailitem item in objs_item)
                {
                    //取PO行的信息
                    object invoicesDetaillist = _InventoryFacade.GetInvoicesDetail(item.Invno, int.Parse(item.Invline));
                    if (invoicesDetaillist == null)
                    {
                        WebInfoPublish.Publish(this, "SAP单据号查找所有的项目行数据不存在", this.languageComponent1);
                        this.DataProvider.CommitTransaction();
                        return false;
                    }
                    InvoicesDetail invdetail = invoicesDetaillist as InvoicesDetail;
                    object obj_item = _InventoryFacade.GetQcRejectQtyFromASNDetailItem(item.Stno, item.Invno, item.Invline, item.Stline);
                    if (obj_item == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "SAP单据号查找所有的项目行数在item中不存在", this.languageComponent1);
                        return false;
                    }
                    //decimal actQTY = (obj_item as Asndetailitem).ActQty;
                    decimal receiveQty = (obj_item as Asndetailitem).ReceiveQty;
                    decimal qcPassQty = (obj_item as Asndetailitem).QcpassQty;
                    int ngqty = (int)receiveQty - (int)qcPassQty;
                    if (ngqty > 0)
                    {

                        PoLog poLog = new PoLog();
                        int serial = _InventoryFacade.GetMaxSerialInPoLog() + 1;
                        poLog.Serial = serial;
                        poLog.PONO = item.Invno;
                        poLog.ZNUMBER = poLog.Serial.ToString();
                        poLog.PoLine = item.Invline.ToString();
                        poLog.FacCode = asn.FacCode;
                        poLog.SerialNO = asn.StNo; // asndetail.s;
                        poLog.MCode = item.MCode;//SAPMcode

                        poLog.Qty = ngqty; // 
                        poLog.Unit = invdetail.Unit; //asndetailObj.Unit;
                        poLog.Status = "104"; // 
                        poLog.Operator = asn.CUser; // asndetail.;
                        poLog.VendorInvoice = asn.InvNo;
                        poLog.StorageCode = asn.StorageCode;
                        poLog.Remark = asn.Remark1;
                        poLog.InvoiceDate = asn.MaintainDate;
                        poLog.SapDateStamp = dbTime.DBDate;
                        poLog.SapTimeStamp = dbTime.DBTime;
                        if (count > 0) //P回传
                        {
                            poLog.SAPMaterialInvoice = "";
                            poLog.IsPBack = "";
                            poLog.SapReturn = "";
                        }
                        else
                        {
                            poLog.IsPBack = "Actual";
                        }
                        logList.Add(poLog);


                        PO po = new PO();
                        po.PONO = item.Invno;
                        po.POLine = int.Parse(item.Invline);
                        po.FacCode = asn.FacCode;
                        po.SerialNO = asn.StNo; // asndetail.s;
                        po.MCode = invdetail.MCode;//SAPMcode
                        po.Qty = ngqty; //初检 接收数量
                        po.Unit = invdetail.Unit; //asndetailObj.Unit;
                        po.Status = "104"; //接收
                        po.ZNUMBER = poLog.ZNUMBER;

                        string invoice103 = string.Empty;
                        PoLog oldPoLogs =
                            (PoLog)
                            _InventoryFacade.GetPoLog(po.PONO, po.POLine.ToString(), po.SerialNO, "103");
                        if (oldPoLogs != null)
                            invoice103 = oldPoLogs.SAPMaterialInvoice;
                        else
                            invoice103 = _InventoryFacade.GetPo103Invoices(po.PONO, po.POLine.ToString(), po.SerialNO);

                        po.SAPMaterialInvoice = oldPoLogs.SAPMaterialInvoice;

                        po.Operator = asn.CUser;
                        po.VendorInvoice = asn.InvNo;
                        po.StorageCode = asn.StorageCode;
                        po.Remark = asn.Remark1;
                        po.InvoiceDate = asn.MaintainDate;
                        list.Add(po);

                    }
                }
                if (list.Count > 0)
                {
                    if (is2Sap)
                    {
                        LogPO2Sap(list);
                    }
                    else
                    {
                        #region SAP回写

                        SAPRfcReturn msg = poToSAP.POReceiveToSAP(list);

                        #region 如果错了返回false

                        if (msg.Result.Trim().ToUpper() == "E")
                        {
                            isSuccess = false;
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "SAP回写错误" + msg.Message, this.languageComponent1);
                        }

                        #endregion

                        #region 写log

                        foreach (PoLog poLog in logList)
                        {

                            if (count > 0) //P回传
                            {
                                poLog.SAPMaterialInvoice = ""; //初检时放，P就为空
                                poLog.SapReturn = "";
                            }
                            else
                            {
                                poLog.SAPMaterialInvoice = Getstring(msg.MaterialDocument); //
                                poLog.SapReturn = Getstring(msg.Result); // msg.Result;//(S表示成功，E表示失败)
                                poLog.Message = Getstring(msg.Message);
                            }
                            _InventoryFacade.AddPoLog(poLog);

                        }

                        #endregion

                        if (!isSuccess)
                        {
                            return false;
                        }

                        #endregion
                    }

                    ShareLib.ShareKit.PoToSupport(list, false);

                }
            }
            return true;

        }

        //提交
        protected void cmdCommit_ServerClick(object sender, EventArgs e)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            #region 4)检查当前检验单号下送检单明细对应缺陷明细表(TBLASNIQCDETAILEC)中所有NGFLAG,SQESTATUS 不为空时
            try
            {

                AsnIQC _iqc = _IQCFacade.GetAsnIQC(IQCNoQS) as AsnIQC;
                if (_iqc.Status != IQCStatus.IQCStatus_SQEJudge)
                {
                    WebInfoPublish.Publish(this, "状态不是SQE判定，不能判定", this.languageComponent1);
                    return;
                }

                AsnIQCDetail[] iqcDetails = _IQCFacade.GetAsnIQCDetails(_iqc.StNo, _iqc.IqcNo);


                if (iqcDetails.Length == 0)
                {
                    WebInfoPublish.PublishInfo(this, _iqc.IqcNo + "检验单不存在入库的箱！", this.languageComponent1);
                    return;
                }
                this.DataProvider.BeginTransaction();

                if (CheckSQEStatusIsNotEmpty1())
                {


                    AsnIQCDetailEc[] ecs = _IQCFacade.GetRetunOrReformEcs(_iqc.IqcNo);
                    ;

                    if (_iqc.IqcType == IQCType.IQCType_SpotCheck)
                    {
                        string[] sss = _iqc.AQLLevel.Split(',');

                        if (sss.Length != 2)
                        {
                            WebInfoPublish.PublishInfo(this, _iqc.IqcNo + " AQL标准格式不正确！必须是序号+检验标准", this.languageComponent1);
                            return;
                        }
                        AQL aql = _IQCFacade.GetAQL(int.Parse(sss[1]), sss[0]);

                        if (aql == null)
                        {
                            WebInfoPublish.PublishInfo(this, _iqc.IqcNo + "AQL不存在！", this.languageComponent1);
                            return;
                        }


                        int sum = _IQCFacade.GetReformAndReturnNgQtyFromIQCNO(_iqc.IqcNo);

                        if (sum < aql.RejectSize)
                        {
                            _iqc.QcStatus = "Y";

                            object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(_iqc.IqcNo);
                            if (objAsnIqcDetail != null)
                            {
                                foreach (AsnIQCDetail asnIQCDetail in objAsnIqcDetail)
                                {

                                    asnIQCDetail.QcStatus = "Y";
                                    asnIQCDetail.QcPassQty = asnIQCDetail.Qty - asnIQCDetail.ReturnQty - asnIQCDetail.ReformQty;
                                    _IQCFacade.UpdateAsnIQCDetail(asnIQCDetail);

                                }
                            }

                            object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(_iqc.IqcNo);
                            if (objAsnIqcDetailSN != null)
                            {
                                foreach (AsnIqcDetailSN sn in objAsnIqcDetailSN)
                                {
                                    sn.QcStatus = "Y";
                                    _IQCFacade.UpdateAsnIqcDetailSN(sn);
                                    Asndetailsn asnDetailSn = (Asndetailsn)_InventoryFacade.GetAsndetailsn(sn.Sn, sn.StNo, Convert.ToInt32(sn.StLine));

                                    if (asnDetailSn != null)
                                    {
                                        asnDetailSn.QcStatus = "Y";
                                        _InventoryFacade.UpdateAsndetailsn(asnDetailSn);
                                    }
                                }
                            }

                        }
                        else
                        {
                            foreach (AsnIQCDetail iqcDetail in iqcDetails)
                            {
                                iqcDetail.QcPassQty = 0;
                                _IQCFacade.UpdateAsnIQCDetail(iqcDetail);
                            }
                        }

                    }



                    _iqc.Status = "IQCClose";
                    _IQCFacade.UpdateAsnIQC(_iqc);

                    foreach (AsnIQCDetailEc ece in ecs)
                    {
                        if (!string.IsNullOrEmpty(ece.SN))
                        {
                            AsnIqcDetailSN iqcSn = (AsnIqcDetailSN)_IQCFacade.GetIQCSN(ece.IqcNo, ece.SN);
                            if (iqcSn != null)
                            {
                                iqcSn.QcStatus = "N";
                                _IQCFacade.UpdateAsnIqcDetailSN(iqcSn);

                            }


                            Asndetailsn detailSn = _IQCFacade.GetASNDetailSN(ece.StNo, ece.SN);
                            if (detailSn != null)
                            {
                                detailSn.QcStatus = "N";
                                _InventoryFacade.UpdateAsndetailsn(detailSn);
                            }
                        }
                    }
                    int ngQty = _IQCFacade.GetSumNgQtyFromAsnIQCDetail(_iqc.IqcNo);


                    #region 在invinouttrans表中增加一条数据

                    DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);



                    object[] ooo = _IQCFacade.GetAsnIQCDetailEcByIqcNo(_iqc.IqcNo);
                    if (ooo != null && ooo.Length > 0)
                    {
                        foreach (AsnIQCDetailEc ec in ooo)
                        {
                            InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                            trans.CartonNO = ec.CartonNo;
                            trans.DqMCode = _iqc.DQMCode;
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = _iqc.InvNo;
                            trans.InvType = _iqc.IqcType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = ec.MaintainDate;
                            trans.MaintainTime = ec.MaintainTime;
                            trans.MaintainUser = ec.MaintainUser;
                            trans.MCode = _iqc.MCode;
                            trans.ProductionDate = 0;
                            trans.Qty = _iqc.Qty;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = string.Empty;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = _iqc.IqcNo;
                            trans.TransType = "IN";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "IQCSQE";
                            facade.AddInvInOutTrans(trans);

                        }
                    }
                    #endregion

                    object[] objAsnIQCDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(IQCNoQS);
                    List<AsnIQCDetail> iqcDetailList = new List<AsnIQCDetail>();
                    if (objAsnIQCDetail != null)
                    {

                        foreach (AsnIQCDetail asnIQCDetail in objAsnIQCDetail)
                        {

                            ASNDetail asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(Convert.ToInt32(asnIQCDetail.StLine), asnIQCDetail.StNo);
                            if (asnDetail != null)
                            {
                                asnDetail.QcPassQty = asnIQCDetail.QcPassQty;
                                asnDetail.Status = ASNLineStatus.IQCClose;
                                if (asnDetail.QcPassQty == 0)
                                    asnDetail.Status = "IQCRejection";

                                asnDetail.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                                asnDetail.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                                asnDetail.MaintainUser = GetUserCode();
                                _InventoryFacade.UpdateASNDetail(asnDetail);
                            }

                            #region 更新asndetailitem
                            object[] objs_item = facade.GetASNDetailItembyStnoAndStline(asnIQCDetail.StNo, asnIQCDetail.StLine);
                            int sub = asnIQCDetail.QcPassQty;
                            if (sub == 0)
                            {
                                foreach (Asndetailitem item in objs_item)
                                {
                                    item.QcpassQty = 0;
                                    item.ActQty = 0;
                                    facade.UpdateAsndetailitem(item);
                                }
                            }
                            else
                            {
                                if (objs_item != null)
                                {
                                    if (objs_item.Length == 1)
                                    {
                                        Asndetailitem item = objs_item[0] as Asndetailitem;
                                        item.QcpassQty = (decimal)sub;
                                        item.ActQty = item.QcpassQty;
                                        facade.UpdateAsndetailitem(item);
                                    }
                                    else
                                    {
                                        object[] objs_invdetail = facade.GetInvoicesDetailByStnoAndStline(asnIQCDetail.StNo, asnIQCDetail.StLine);
                                        if (objs_invdetail != null)
                                        {
                                            foreach (InvoicesDetail invd in objs_invdetail)
                                            {
                                                foreach (Asndetailitem item in objs_item)
                                                {
                                                    if (invd.InvNo == item.Invno && invd.InvLine.ToString() == item.Invline)
                                                    {
                                                        if (item.ReceiveQty >= sub)
                                                        {
                                                            item.QcpassQty = sub;
                                                            item.ActQty = item.QcpassQty;
                                                            sub = 0;
                                                            facade.UpdateAsndetailitem(item);
                                                        }
                                                        else
                                                        {
                                                            sub = sub - (int)item.ReceiveQty;
                                                            item.QcpassQty = item.ReceiveQty;
                                                            item.ActQty = item.QcpassQty;
                                                            facade.UpdateAsndetailitem(item);
                                                        }
                                                        if (sub == 0)
                                                        {
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            iqcDetailList.Add(asnIQCDetail);

                        }
                    }

                    AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(IQCNoQS);


                    ASN asn = null;
                    if (asnIqc != null)
                    {
                        asn = (ASN)_InventoryFacade.GetASN(asnIqc.StNo);

                        if (_IQCFacade.CanToOnlocationStaus(asnIqc.StNo))
                        {
                            string status = _IQCFacade.GetAsnNewStatus(asnIqc.StNo);


                            asn.Status = status;
                            if (status == ASNHeadStatus.IQCRejection)
                                asnIqc.Status = status;
                        }

                        _IQCFacade.UpdateAsnIQC(asnIqc);
                        _InventoryFacade.UpdateASN(asn);
                    }

                    #region


                    if ((_iqc.Status == ASNLineStatus.IQCClose || _iqc.Status == "IQCRejection") && _iqc.StType == SAP_ImportType.SAP_POR)
                    {
                        List<string> iqclist = new List<string>();
                        iqclist.Add(IQCNoQS);
                        bool result = PoToSap(iqclist.ToArray());
                        if (!result)
                        {
                            return;
                        }
                    }
                    #endregion
                    WarehouseFacade ware = new WarehouseFacade(base.DataProvider);
                    SendMail mail1 = ShareLib.ShareKit.IQCRejectThenGenerMail(_iqc, asn, GetUserCode(), ware, _IQCFacade);
                    if (mail1 != null)
                        ware.AddSendMail(mail1);

                    SystemSettingFacade baseSetting = new SystemSettingFacade(base.DataProvider);
                    MailIQCEc[] mailEcs = ware.GetIQCMailEc(_iqc.IqcNo);
                    List<SendMail> mail2 = ShareLib.ShareKit.IQCSQEFinishThenGenerMail(mailEcs, asn, GetUserCode(), _iqc.CUser, baseSetting, ware);
                    foreach (SendMail s in mail2)
                        ware.AddSendMail(s);

                }
                else
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.PublishInfo(this, "SQE判定没有完成", this.languageComponent1);
                    return;
                }
            #endregion



                this.DataProvider.CommitTransaction();
                WebInfoPublish.PublishInfo(this, "提交成功", this.languageComponent1);
                this.gridHelper.RequestData();
            }
            catch (Exception ex)
            {
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
                this.DataProvider.RollbackTransaction();
            }
        }

        #region SAP回传 add by sam 2016年3月21日
        private bool PoToSap(string[] iqcNoList)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            IQCFacade _iqcFacade = new IQCFacade(base.DataProvider);
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            string stno = "";
            string dqmcode = "";
            string stline = "";
            bool isSuccess = true;

            foreach (string iqcno in iqcNoList)
            {
                AsnIQC iqc = _iqcFacade.GetAsnIQC(iqcno) as AsnIQC;
                dqmcode = iqc.DQMCode;
                object[] objs_iqcdetails = _iqcFacade.GetAsnIQCDetailByIqcNo(iqcno);
                if (objs_iqcdetails != null)
                {
                    foreach (AsnIQCDetail iqcdetails in objs_iqcdetails)
                    {
                        stno = iqcdetails.StNo;
                        stline = iqcdetails.StLine;

                        //取在detailitem中，此箱分配到哪些po行
                        object[] objs_item = _InventoryFacade.GetInvoiceLineFromASNDetailItem(stno, stline);
                        if (objs_item == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "asndetailitem无数据", this.languageComponent1);
                            return false;
                        }
                        ASN asn = (ASN)_InventoryFacade.GetASN(stno);
                        int count = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime);
                        BenQGuru.eMES.SAPRFCService.POToSAP poToSAP = new POToSAP(this.DataProvider);
                        List<PO> list = new List<PO>();
                        List<PoLog> logList = new List<PoLog>();
                        // item中每个invline进行操作
                        foreach (Asndetailitem item in objs_item)
                        {
                            //取PO行的信息
                            object invoicesDetaillist = _InventoryFacade.GetInvoicesDetail(item.Invno, int.Parse(item.Invline));
                            if (invoicesDetaillist == null)
                            {
                                WebInfoPublish.Publish(this, "SAP单据号查找所有的项目行数据不存在", this.languageComponent1);
                                this.DataProvider.CommitTransaction();
                                return false;
                            }
                            InvoicesDetail invdetail = invoicesDetaillist as InvoicesDetail;
                            object obj_item = _InventoryFacade.GetQcRejectQtyFromASNDetailItem(item.Stno, item.Invno, item.Invline, item.Stline);
                            if (obj_item == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "SAP单据号查找所有的项目行数在item中不存在", this.languageComponent1);
                                return false;
                            }
                            //decimal actQTY = (obj_item as Asndetailitem).ActQty;
                            decimal receiveQty = (obj_item as Asndetailitem).ReceiveQty;
                            decimal qcPassQty = (obj_item as Asndetailitem).QcpassQty;
                            int ngqty = (int)receiveQty - (int)qcPassQty;
                            if (ngqty > 0)
                            {
                                //Domain.MOModel.Material material =
                                //    (Domain.MOModel.Material)_InventoryFacade.GetMaterialByDQMCode(detail.DQMCode);
                                #region 记录log
                                PoLog poLog = new PoLog();
                                int serial = _InventoryFacade.GetMaxSerialInPoLog() + 1;
                                poLog.Serial = serial;
                                poLog.ZNUMBER = serial.ToString();
                                poLog.PONO = item.Invno;
                                poLog.PoLine = item.Invline.ToString();
                                poLog.FacCode = asn.FacCode;
                                poLog.SerialNO = asn.StNo; // asndetail.s;
                                poLog.MCode = item.MCode;//SAPMcode
                                poLog.Qty = ngqty; // 
                                poLog.Unit = invdetail.Unit; //asndetailObj.Unit;
                                poLog.Status = "104"; // 
                                poLog.Operator = GetUserCode(); // asndetail.;
                                poLog.VendorInvoice = asn.InvNo;
                                poLog.StorageCode = asn.StorageCode;
                                poLog.Remark = asn.Remark1;
                                poLog.InvoiceDate = asn.MaintainDate;
                                poLog.SapDateStamp = dbTime.DBDate;
                                poLog.SapTimeStamp = dbTime.DBTime;
                                if (count > 0) //P回传
                                {
                                    poLog.SAPMaterialInvoice = "";
                                    poLog.IsPBack = "";
                                    poLog.SapReturn = "";
                                }
                                else
                                {
                                    poLog.IsPBack = "Actual";
                                }
                                logList.Add(poLog);
                                #endregion

                                #region 回传接口

                                PO po = new PO();
                                po.PONO = item.Invno;
                                po.POLine = int.Parse(item.Invline);
                                po.FacCode = asn.FacCode;
                                po.SerialNO = asn.StNo; // asndetail.s;
                                po.MCode = invdetail.MCode;//SAPMcode
                                po.Qty = ngqty; //初检 接收数量
                                po.Unit = invdetail.Unit; //asndetailObj.Unit;
                                po.Status = "104"; //接收
                                po.ZNUMBER = poLog.ZNUMBER;


                                string invoice103 = string.Empty;
                                PoLog oldPoLogs =
                                    (PoLog)
                                    _InventoryFacade.GetPoLog(po.PONO, po.POLine.ToString(), po.SerialNO, "103");
                                if (oldPoLogs != null)
                                    invoice103 = oldPoLogs.SAPMaterialInvoice;
                                else
                                    invoice103 = _InventoryFacade.GetPo103Invoices(po.PONO, po.POLine.ToString(), po.SerialNO);

                                po.SAPMaterialInvoice = invoice103;
                                po.Operator = poLog.Operator;
                                po.VendorInvoice = asn.InvNo;
                                po.StorageCode = asn.StorageCode;
                                po.Remark = asn.Remark1;
                                po.InvoiceDate = asn.MaintainDate;
                                list.Add(po);
                                #endregion
                            }

                        }
                        if (list.Count > 0)
                        {
                            bool is2Sap = _WarehouseFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;
                            if (is2Sap)
                            {
                                LogPO2Sap(dbTime, list, _InventoryFacade);

                            }
                            else
                            {

                                SAPRfcReturn msg = poToSAP.POReceiveToSAP(list);
                                #region 如果错了返回false
                                if (msg.Result.Trim().ToUpper() == "E")
                                {
                                    isSuccess = false;
                                    this.DataProvider.RollbackTransaction();
                                    WebInfoPublish.Publish(this, "SAP回写错误 " + msg.Message, this.languageComponent1);
                                }
                                #endregion
                                #region 写log
                                foreach (PoLog poLog in logList)
                                {

                                    if (count > 0)//P回传
                                    {
                                        poLog.SAPMaterialInvoice = "";//初检时放，P就为空
                                        poLog.SapReturn = "";
                                    }
                                    else
                                    {
                                        poLog.SAPMaterialInvoice = Getstring(msg.MaterialDocument); //
                                        poLog.SapReturn = Getstring(msg.Result);  // msg.Result;//(S表示成功，E表示失败)
                                        poLog.Message = Getstring(msg.Message);
                                    }
                                    _InventoryFacade.AddPoLog(poLog);

                                }
                            }


                                #endregion
                            if (!isSuccess)
                                return false;
                            ShareLib.ShareKit.PoToSupport(list, false);

                        }

                    }
                }


            }
            return true;

        }


        private void LogPO2Sap(DBDateTime dbTime, List<BenQGuru.eMES.SAPRFCService.Domain.PO> dns, InventoryFacade _InventoryFacade)
        {


            foreach (BenQGuru.eMES.SAPRFCService.Domain.PO po in dns)
            {

                Po2Sap poLog = new Po2Sap();
                poLog.PONO = po.PONO;
                poLog.POLine = po.POLine;
                poLog.STNO = po.SerialNO;
                poLog.SerialNO = po.SerialNO;
                poLog.Qty = po.Qty; // 
                poLog.Unit = po.Unit;
                poLog.STNO = po.SerialNO;
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
                poLog.SapReturn = string.Empty;
                poLog.ZNUMBER = po.ZNUMBER;
                //poLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                //poLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                //poLog.MaintainUser = GetUserCode();
                //poLog.r = "empty";
                //poLog.Message = "empty";
                _InventoryFacade.AddPo2Sap(poLog);
            }
        }
        #endregion

        private string Getstring(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                return message;
            }
            return "";
        }


        #region SAP回传 add by sam 2016年3月21日
        private void PoToSap2(ArrayList array)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            if (_Invenfacade == null)
            {
                _Invenfacade = new InventoryFacade(base.DataProvider);
            }
            foreach (GridRecord row in array)
            {
                string sqeStatus = row.Items.FindItemByKey("ProcessWay").Text.Trim();
                object objRow = this.GetEditObject(row);
                object obj = this.GetEditObject();
                if (obj != null)
                {
                    AsnIQCDetailEc asnIQCDetailEc = (AsnIQCDetailEc)obj;
                    //Return:退换货、Reform:现场整改 回写
                    if (asnIQCDetailEc.SqeStatus == SQEStatus.SQEStatus_Return || asnIQCDetailEc.SqeStatus == SQEStatus.SQEStatus_Reform)
                    {
                        if (asnIQCDetailEc.NgFlag != "Y")//Y:整箱缺陷,，不用回写
                        {
                            string iqcNo = asnIQCDetailEc.IqcNo;
                            string cartonCode = asnIQCDetailEc.CartonNo;
                            POToSAP poToSAP = new POToSAP();
                            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                            int count = facade.GetRecordCount(dbTime.DBDate, dbTime.DBTime);
                            string datetime = dbTime.DBDate.ToString() + dbTime.DBTime.ToString().PadLeft(6, '0');
                            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
                            //Invoices invoices = (Invoices)_Invenfacade.GetInvoices(asnIqc.InvNo);
                            ASN asnObj = (ASN)_Invenfacade.GetASN(asnIqc.StNo);

                            #region check
                            // object[] allObjList = _Invenfacade.QueryASNDetailBySTNoCheckStatus(asnIqc.StNo, "Reject");
                            Asndetail asndetailObj = (Asndetail)_Invenfacade.QueryAsnDetailByStNoAndcartons(asnIqc.StNo, cartonCode);
                            if (asndetailObj == null)
                            {
                                return;
                            }
                            #endregion
                            //int receiveQty = _Invenfacade.GetQcPassQtyByStatus(Stno, asndetailObj.DqmCode, "Reject");
                            object[] invoicesDetaillist = _Invenfacade.GetInvoicesDetailByInvNo(asnIqc.InvNo, asndetailObj.DqmCode);
                            Domain.MOModel.Material material = (Domain.MOModel.Material)_Invenfacade.GetMaterialByDQMCode(asndetailObj.DqmCode);
                            #region POList
                            List<PO> list = new List<PO>();
                            foreach (InvoicesDetail detail in invoicesDetaillist)
                            {
                                #region  PO回传
                                PO po = new PO();
                                po.PONO = detail.InvNo;
                                po.POLine = detail.InvLine;
                                po.FacCode = asnObj.FacCode;
                                po.SerialNO = asnObj.StNo;// asndetailObj.s;
                                po.MCode = asndetailObj.DqmCode; //txtMCode.Text;
                                po.Qty = asnIQCDetailEc.NgQty;//detailSN. 
                                if (material != null)
                                {
                                    po.Unit = material.Muom;//asndetailObj.Unit;
                                }
                                po.Status = "104";//IQC
                                PoLog oldPoLog = (PoLog)_Invenfacade.GetPoLog(po.PONO, po.POLine.ToString(), po.SerialNO, "103");
                                if (oldPoLog != null)
                                {
                                    po.SAPMaterialInvoice = oldPoLog.SAPMaterialInvoice;
                                }
                                po.Operator = asndetailObj.CUser;// asndetailObj.;
                                po.VendorInvoice = asnObj.InvNo;
                                po.StorageCode = asnObj.StorageCode;
                                po.Remark = asndetailObj.Remark1;
                                po.InvoiceDate = asndetailObj.MaintainDate;
                                list.Add(po);
                                #endregion

                                #region poLog
                                PoLog poLog = new PoLog();
                                poLog.PONO = po.PONO;
                                poLog.PoLine = po.POLine.ToString();
                                poLog.FacCode = po.FacCode; // asndetail.;
                                poLog.SerialNO = po.SerialNO;// asndetail.s;
                                poLog.MCode = po.MCode; //txtMCode.Text;
                                poLog.Qty = po.Qty;//detailSN. //Convert.ToDecimal(txtQty.Text);
                                poLog.Unit = po.Unit;
                                poLog.Status = po.Status;// 初检

                                poLog.Operator = po.Operator;// asndetail.;
                                poLog.VendorInvoice = po.VendorInvoice;
                                poLog.StorageCode = po.StorageCode;
                                poLog.Remark = po.Remark;
                                poLog.InvoiceDate = po.InvoiceDate;
                                if (count > 0)//P回传
                                {
                                    poLog.IsPBack = "";
                                    poLog.SapReturn = "";
                                    poLog.SapTimeStamp = int.Parse(datetime);
                                    _Invenfacade.AddPoLog(poLog);
                                }
                                else
                                {
                                    SAPRfcReturn msg = poToSAP.POReceiveToSAP(list);
                                    poLog.IsPBack = "Actual";
                                    poLog.SapReturn = msg.Result;//(S表示成功，E表示失败)
                                    poLog.SapTimeStamp = int.Parse(datetime);
                                    _Invenfacade.AddPoLog(poLog);
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                }
            }
        }
        #endregion




        //返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FSQEJudgeMP.aspx"));
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Query)
            {
                this.cmdConfirm.Disabled = true;
            }
            if (pageAction == PageActionType.Add)
            {
                this.cmdConfirm.Disabled = true;
            }
            if (pageAction == PageActionType.Update)
            {
                this.cmdConfirm.Disabled = false;
            }
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }

            AsnIQCDetailEc asnIqcDetailEc = (AsnIQCDetailEc)this.ViewState["AsnIQCDetailEcEdit"];
            if (asnIqcDetailEc != null)
            {
                asnIqcDetailEc.SqeStatus = FormatHelper.CleanString(this.drpProcessWayEdit.SelectedValue, 40);
                asnIqcDetailEc.SQERemark1 = FormatHelper.CleanString(this.txtMemoEdit.Text, 200);
            }
            this.ViewState["AsnIQCDetailEcEdit"] = null;
            return asnIqcDetailEc;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            string iqcNo = row.Items.FindItemByKey("IQCNo").Text.Trim();
            string stNo = row.Items.FindItemByKey("StNo").Text.Trim();
            string stLine = row.Items.FindItemByKey("StLine").Text.Trim();
            string eCode = row.Items.FindItemByKey("ECode").Text.Trim();
            string sn = row.Items.FindItemByKey("SN").Text.Trim();
            string ECGCode = row.Items.FindItemByKey("NGType").Text.Trim();
            object[] obj = _IQCFacade.GetAsnIQCDetailEc(ECGCode, eCode, stLine, iqcNo, stNo, sn);

            if (obj != null)
            {
                this.ViewState["AsnIQCDetailEcEdit"] = obj[0];//记录行实体
                return (AsnIQCDetailEc)obj[0];
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
                this.drpProcessWayEdit.SelectedValue = ((AsnIQCDetailEc)obj).SqeStatus;
            }
            catch (Exception)
            {

                this.drpProcessWayEdit.SelectedIndex = 0; ;
            }
            this.txtMemoEdit.Text = ((AsnIQCDetailEc)obj).SQERemark1;
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
            return new string[]{((AsnIQCDetailEc)obj).IqcNo,
                                ((AsnIQCDetailEc)obj).CartonNo,
                                ((AsnIQCDetailEc)obj).EcgCode,
                                ((AsnIQCDetailEc)obj).ECode,
                                ((AsnIQCDetailEc)obj).NgQty.ToString(),
                                ((AsnIQCDetailEc)obj).SN,
                                ((AsnIQCDetailEc)obj).SQERemark1,
                                ((AsnIQCDetailEc)obj).SqeStatus,
                                ((AsnIQCDetailEc)obj).SQERemark1
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"IQCNo",
                                    "CartonNo",
                                    "NGType",
                                    "NGDesc",
                                    "NGQty",
                                    "SN",	
                                    "Memo",
                                    "ProcessWay",
                                    "Memo"};
        }

        #endregion

        #region Method
        //批量管控时更改送检单明细处理方式数量
        /// <summary>
        /// 批量管控时更改送检单明细处理方式数量
        /// </summary>
        /// <param name="drp">DropDownList（选择的处理方式）</param>
        /// <param name="asnIQCDetail">送检单明细实体</param>
        private void ChangeQtyForLot(DropDownList drp, AsnIQCDetail asnIQCDetail)
        {
            switch (drp.SelectedValue)
            {
                case "Return":
                    asnIQCDetail.ReturnQty += 1;
                    //asnIQCDetail.ReformQty = 0;
                    //asnIQCDetail.GiveQty = 0;
                    //asnIQCDetail.AcceptQty = 0;
                    break;
                case "Reform":
                    //asnIQCDetail.ReturnQty = 0;
                    asnIQCDetail.ReformQty += 1;
                    //asnIQCDetail.GiveQty = 0;
                    //asnIQCDetail.AcceptQty = 0;
                    break;
                case "Give":
                    //asnIQCDetail.ReturnQty = 0;
                    //asnIQCDetail.ReformQty = 0;
                    asnIQCDetail.GiveQty += 1;
                    //asnIQCDetail.AcceptQty = 0;
                    break;
                case "Accept":
                    //asnIQCDetail.ReturnQty = 0;
                    //asnIQCDetail.ReformQty = 0;
                    //asnIQCDetail.GiveQty = 0;
                    asnIQCDetail.AcceptQty += 1;
                    break;
            }
        }
        private void ChangeQtyForLot(DropDownList drp, AsnIQCDetail asnIQCDetail, int num)
        {
            switch (drp.SelectedValue)
            {
                case "Return":
                    asnIQCDetail.ReturnQty += num;
                    //asnIQCDetail.ReformQty = 0;
                    //asnIQCDetail.GiveQty = 0;
                    //asnIQCDetail.AcceptQty = 0;
                    break;
                case "Reform":
                    //asnIQCDetail.ReturnQty = 0;
                    asnIQCDetail.ReformQty += num;
                    //asnIQCDetail.GiveQty = 0;
                    //asnIQCDetail.AcceptQty = 0;
                    break;
                case "Give":
                    //asnIQCDetail.ReturnQty = 0;
                    //asnIQCDetail.ReformQty = 0;
                    asnIQCDetail.GiveQty += num;
                    //asnIQCDetail.AcceptQty = 0;
                    break;
                case "Accept":
                    //asnIQCDetail.ReturnQty = 0;
                    //asnIQCDetail.ReformQty = 0;
                    //asnIQCDetail.GiveQty = 0;
                    asnIQCDetail.AcceptQty += num;
                    break;
            }
        }
        private void ChangeQtyForLot(DropDownList drp, AsnIQCDetail asnIQCDetail, int num, string FF)
        {
            _IQCFacade = new IQCFacade(base.DataProvider);
            object[] objs = _IQCFacade.GetAsnIQCDetailEcByIQCNoAndCartonNo1(asnIQCDetail.IqcNo, asnIQCDetail.CartonNo);
            if (objs != null)
            {
                AsnIQCDetailEc ec = objs[0] as AsnIQCDetailEc;
                switch (ec.SqeStatus)
                {
                    case "Return":
                        asnIQCDetail.ReturnQty -= num;
                        //asnIQCDetail.ReformQty = 0;
                        //asnIQCDetail.GiveQty = 0;
                        //asnIQCDetail.AcceptQty = 0;
                        break;
                    case "Reform":
                        //asnIQCDetail.ReturnQty = 0;
                        asnIQCDetail.ReformQty -= num;
                        //asnIQCDetail.GiveQty = 0;
                        //asnIQCDetail.AcceptQty = 0;
                        break;
                    case "Give":
                        //asnIQCDetail.ReturnQty = 0;
                        //asnIQCDetail.ReformQty = 0;
                        asnIQCDetail.GiveQty -= num;
                        //asnIQCDetail.AcceptQty = 0;
                        break;
                    case "Accept":
                        //asnIQCDetail.ReturnQty = 0;
                        //asnIQCDetail.ReformQty = 0;
                        //asnIQCDetail.GiveQty = 0;
                        asnIQCDetail.AcceptQty -= num;
                        break;
                }
            }

            switch (drp.SelectedValue)
            {
                case "Return":
                    asnIQCDetail.ReturnQty += num;
                    //asnIQCDetail.ReformQty = 0;
                    //asnIQCDetail.GiveQty = 0;
                    //asnIQCDetail.AcceptQty = 0;
                    break;
                case "Reform":
                    //asnIQCDetail.ReturnQty = 0;
                    asnIQCDetail.ReformQty += num;
                    //asnIQCDetail.GiveQty = 0;
                    //asnIQCDetail.AcceptQty = 0;
                    break;
                case "Give":
                    //asnIQCDetail.ReturnQty = 0;
                    //asnIQCDetail.ReformQty = 0;
                    asnIQCDetail.GiveQty += num;
                    //asnIQCDetail.AcceptQty = 0;
                    break;
                case "Accept":
                    //asnIQCDetail.ReturnQty = 0;
                    //asnIQCDetail.ReformQty = 0;
                    //asnIQCDetail.GiveQty = 0;
                    asnIQCDetail.AcceptQty += num;
                    break;
            }

        }
        //单件管控时更改送检单明细处理方式数量
        /// <summary>
        /// 单件管控时更改送检单明细处理方式数量
        /// </summary>
        /// <param name="sqeStatus">原处理方式</param>
        /// <param name="drp">DropDownList（选择的处理方式）</param>
        /// <param name="asnIQCDetail">送检单明细实体</param>
        private void ChangeQtyForSN(string sqeStatus, DropDownList drp, AsnIQCDetail asnIQCDetail)
        {
            switch (sqeStatus)
            {
                case "Return":
                    switch (drp.SelectedValue)
                    {
                        case "Return":
                            //选择本身数量不变
                            break;
                        case "Reform":
                            asnIQCDetail.ReturnQty -= 1;
                            asnIQCDetail.ReformQty += 1;
                            break;
                        case "Give":
                            asnIQCDetail.ReturnQty -= 1;
                            asnIQCDetail.GiveQty += 1;
                            break;
                        case "Accept":
                            asnIQCDetail.ReturnQty -= 1;
                            asnIQCDetail.AcceptQty += 1;
                            break;
                    }
                    break;
                case "Reform":
                    switch (drp.SelectedValue)
                    {
                        case "Return":
                            asnIQCDetail.ReformQty -= 1;
                            asnIQCDetail.ReturnQty += 1;
                            break;
                        case "Reform":
                            //选择本身数量不变
                            break;
                        case "Give":
                            asnIQCDetail.ReformQty -= 1;
                            asnIQCDetail.GiveQty += 1;
                            break;
                        case "Accept":
                            asnIQCDetail.ReformQty -= 1;
                            asnIQCDetail.AcceptQty += 1;
                            break;
                    }
                    break;
                case "Give":
                    switch (drp.SelectedValue)
                    {
                        case "Return":
                            asnIQCDetail.GiveQty -= 1;
                            asnIQCDetail.ReturnQty += 1;
                            break;
                        case "Reform":
                            asnIQCDetail.GiveQty -= 1;
                            asnIQCDetail.ReformQty += 1;
                            break;
                        case "Give":
                            //选择本身数量不变
                            break;
                        case "Accept":
                            asnIQCDetail.GiveQty -= 1;
                            asnIQCDetail.AcceptQty += 1;
                            break;
                    }
                    break;
                case "Accept":
                    switch (drp.SelectedValue)
                    {
                        case "Return":
                            asnIQCDetail.AcceptQty -= 1;
                            asnIQCDetail.ReturnQty += 1;
                            break;
                        case "Reform":
                            asnIQCDetail.AcceptQty -= 1;
                            asnIQCDetail.ReformQty += 1;
                            break;
                        case "Give":
                            asnIQCDetail.AcceptQty -= 1;
                            asnIQCDetail.GiveQty += 1;
                            break;
                        case "Accept":
                            //选择本身数量不变
                            break;
                    }
                    break;
            }
        }

        private void ChangeQtyForSN(string sqeStatus, DropDownList drp, AsnIQCDetail asnIQCDetail, int num)
        {
            switch (sqeStatus)
            {
                case "Return":
                    switch (drp.SelectedValue)
                    {
                        case "Return":
                            //选择本身数量不变
                            break;
                        case "Reform":
                            asnIQCDetail.ReturnQty -= num;
                            asnIQCDetail.ReformQty += num;
                            break;
                        case "Give":
                            asnIQCDetail.ReturnQty -= num;
                            asnIQCDetail.GiveQty += num;
                            break;
                        case "Accept":
                            asnIQCDetail.ReturnQty -= num;
                            asnIQCDetail.AcceptQty += num;
                            break;
                    }
                    break;
                case "Reform":
                    switch (drp.SelectedValue)
                    {
                        case "Return":
                            asnIQCDetail.ReformQty -= num;
                            asnIQCDetail.ReturnQty += num;
                            break;
                        case "Reform":
                            //选择本身数量不变
                            break;
                        case "Give":
                            asnIQCDetail.ReformQty -= num;
                            asnIQCDetail.GiveQty += num;
                            break;
                        case "Accept":
                            asnIQCDetail.ReformQty -= num;
                            asnIQCDetail.AcceptQty += num;
                            break;
                    }
                    break;
                case "Give":
                    switch (drp.SelectedValue)
                    {
                        case "Return":
                            asnIQCDetail.GiveQty -= num;
                            asnIQCDetail.ReturnQty += num;
                            break;
                        case "Reform":
                            asnIQCDetail.GiveQty -= num;
                            asnIQCDetail.ReformQty += num;
                            break;
                        case "Give":
                            //选择本身数量不变
                            break;
                        case "Accept":
                            asnIQCDetail.GiveQty -= num;
                            asnIQCDetail.AcceptQty += num;
                            break;
                    }
                    break;
                case "Accept":
                    switch (drp.SelectedValue)
                    {
                        case "Return":
                            asnIQCDetail.AcceptQty -= num;
                            asnIQCDetail.ReturnQty += num;
                            break;
                        case "Reform":
                            asnIQCDetail.AcceptQty -= num;
                            asnIQCDetail.ReformQty += num;
                            break;
                        case "Give":
                            asnIQCDetail.AcceptQty -= num;
                            asnIQCDetail.GiveQty += num;
                            break;
                        case "Accept":
                            //选择本身数量不变
                            break;
                    }
                    break;
            }
        }
        //检查缺陷品数是否为Y/N
        /// <summary>
        /// 检查缺陷品数是否为Y/N
        /// </summary>
        /// <returns></returns>
        private bool CheckNGFlagIsAllYN()
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            object[] objAsnIqcDetailEc = _IQCFacade.GetAsnIQCDetailEcByIqcNo(IQCNoQS);
            if (objAsnIqcDetailEc != null && objAsnIqcDetailEc.Length > 1)
            {
                for (int i = 1; i < objAsnIqcDetailEc.Length; i++)
                {
                    if (((AsnIQCDetailEc)objAsnIqcDetailEc[i - 1]).NgFlag != ((AsnIQCDetailEc)objAsnIqcDetailEc[i]).NgFlag)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //检查缺陷品数是否为Y/N
        /// <summary>
        /// 根据IQC送检单号和箱号检查缺陷品数是否为Y/N
        /// </summary>
        /// <param name="iqcNo">IQC送检单号</param>
        /// <param name="cartonNo">箱号</param>
        /// <returns></returns>
        private bool CheckNGFlagIsAllYN(string iqcNo, string cartonNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            object[] objAsnIqcDetailEc = _IQCFacade.GetAsnIQCDetailEc(iqcNo, cartonNo);
            if (objAsnIqcDetailEc != null && objAsnIqcDetailEc.Length > 1)
            {
                for (int i = 1; i < objAsnIqcDetailEc.Length; i++)
                {
                    if (((AsnIQCDetailEc)objAsnIqcDetailEc[i - 1]).NgFlag != ((AsnIQCDetailEc)objAsnIqcDetailEc[i]).NgFlag)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool CheckNGFlagIsAllYN1(string iqcNo, string cartonNo, string NGFlag)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            if (NGFlag == "Y")
            {
                return true;
            }
            object[] objAsnIqcDetailEc = _IQCFacade.GetAsnIQCDetailEcByIQCNoAndCartonNo1(iqcNo, cartonNo);
            if (objAsnIqcDetailEc != null && objAsnIqcDetailEc.Length > 0)
            {
                AsnIQCDetailEc ec = objAsnIqcDetailEc[0] as AsnIQCDetailEc;
                if (ec.SqeStatus == "Return" || ec.SqeStatus == "Reform")
                {
                    return false;
                }
            }
            return true;
        }
        private bool CheckALLNG(string iqcNo, string cartonNo, string NGFlag)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            return _IQCFacade.GetAsnIQCDetailEc(iqcNo, cartonNo, NGFlag);

        }
        private bool CheckALLNGStatus(string iqcNo, string cartonNo, string NGFlag)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            return _IQCFacade.CheckALLNGStatus(iqcNo, cartonNo, NGFlag);

        }
        //检查SQE状态是否都有值
        /// <summary>
        /// 检查SQE状态是否都有值
        /// </summary>
        /// <returns></returns>
        private bool CheckSQEStatusIsNotEmpty()
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            object[] objAsnIqcDetailEc = _IQCFacade.GetAsnIQCDetailEcByIqcNo(IQCNoQS);
            if (objAsnIqcDetailEc != null)
            {
                foreach (AsnIQCDetailEc asnIQCDetailEc in objAsnIqcDetailEc)
                {
                    if (string.IsNullOrEmpty(asnIQCDetailEc.SqeStatus))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool CheckSQEStatusIsNotEmpty1()
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            //bool NGFlg = true;
            //bool SQEVal = false;
            //bool FF = true;
            object[] objAsnIqcDetailEc = _IQCFacade.GetAsnIQCDetailEcByIqcNo(IQCNoQS, "Y");
            if (objAsnIqcDetailEc == null)
            {
                //return true;
                object[] objAsnIqcDetailEc1 = _IQCFacade.GetAsnIQCDetailEcByIqcNo(IQCNoQS, "N");
                {
                    if (objAsnIqcDetailEc1 == null)
                        return true;
                    else
                    {
                        foreach (AsnIQCDetailEc ee in objAsnIqcDetailEc1)
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
                foreach (AsnIQCDetailEc ec in objAsnIqcDetailEc)
                {
                    //AsnIQCDetailEc ec = objAsnIqcDetailEc[0] as AsnIQCDetailEc;
                    if (string.IsNullOrEmpty(ec.SqeStatus))
                    {
                        return false;
                    }
                    if (ec.SqeStatus == "Return" || ec.SqeStatus == "Reform")
                    {
                        //return true;
                    }
                    else
                    {
                        object[] objAsnIqcDetailEc1 = _IQCFacade.GetAsnIQCDetailEcByIqcNo(IQCNoQS, "N", ec.CartonNo);
                        {
                            if (objAsnIqcDetailEc1 != null)
                            //    return true;
                            //else
                            {
                                foreach (AsnIQCDetailEc ee in objAsnIqcDetailEc1)
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

        // 根据ASN单号检查ASN明细所有行状态
        /// <summary>
        /// 根据ASN单号检查ASN明细所有行状态
        /// </summary>
        /// <param name="stNo">ASN单号</param>
        /// <returns></returns>
        private bool CheckAllAsnDetailStatus(string stNo)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(stNo);
            if (objAsnDetail != null && objAsnDetail.Length > 0)
            {
                string fistStatus = ((ASNDetail)objAsnDetail[0]).Status;
                switch (fistStatus)
                {
                    case "IQCClose":
                        for (int i = 1; i < objAsnDetail.Length; i++)
                        {
                            if (((ASNDetail)objAsnDetail[i]).Status != fistStatus)
                            {
                                return false;
                            }
                        }
                        break;
                    case "OnLocation":
                        for (int i = 1; i < objAsnDetail.Length; i++)
                        {
                            if (((ASNDetail)objAsnDetail[i]).Status != fistStatus)
                            {
                                return false;
                            }
                        }
                        break;
                    case "Close":
                        for (int i = 1; i < objAsnDetail.Length; i++)
                        {
                            if (((ASNDetail)objAsnDetail[i]).Status != fistStatus)
                            {
                                return false;
                            }
                        }
                        break;
                    case "Cancel":
                        for (int i = 1; i < objAsnDetail.Length; i++)
                        {
                            if (((ASNDetail)objAsnDetail[i]).Status != fistStatus)
                            {
                                return false;
                            }
                        }
                        break;
                    default:
                        return false;
                }
            }
            return true;
        }


        //导出IQC异常联络单
        protected void cmdExportIQCACL_ServerClick(object sender, EventArgs e)
        {
            try
            {
                //TODO：未完成，具体值没有获取
                string fileName = "AbnormalContactList.xlsx";
                ExportExcel(fileName);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw ex;
            }
        }


        //导出Excel
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="fileName">模板名称</param>
        private void ExportExcel(string fileName)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            _InventoryFacade = new InventoryFacade(base.DataProvider);
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
            string _IQCNo = IQCNoQS;//add by sam
            DataSet ds = _IQCFacade.GetIQCandMaterialInfoByIQCNo(_IQCNo);
            if (ds != null && ds.Tables.Count > 0)
            {
                DQMCode = ds.Tables[0].Rows[0]["dqmcode"].ToString();
                CustmCode = ds.Tables[0].Rows[0]["custmcode"].ToString();
                ApplyDate = FormatHelper.ToDateString(int.Parse(ds.Tables[0].Rows[0]["cdate"].ToString()));
                InvNo = ds.Tables[0].Rows[0]["invno"].ToString() + "/" + _IQCNo;

                Invoices inv = (Invoices)_InventoryFacade.GetInvoices(ds.Tables[0].Rows[0]["invno"].ToString());
                if (inv != null)
                {
                    BenQGuru.eMES.Domain.MOModel.Vendor ve = _IQCFacade.GetVendor(inv.VendorCode);
                    if (ve != null)
                    {
                        CustmCode = ve.VendorCode;
                        VendorName = ve.VendorName;
                    }
                }

                DQCHLDesc = ds.Tables[0].Rows[0]["mchlongdesc"].ToString();

                Qty = _IQCFacade.GetAsnIqcDetailSum(_IQCNo).ToString();
            }
            object[] objs = _IQCFacade.GetAsnIQCDetailEcByIqcNo(_IQCNo);
            if (objs == null || objs.Length <= 0)
            {
                WebInfoPublish.PublishInfo(this, "无IQC异常信息", this.languageComponent1);
                return;
            }
            foreach (AsnIQCDetailEc ec in objs)
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
            //WebInfoPublish.PublishInfo(this, "走到这里" + "||" + this.Page + "||" + this.VirtualHostRoot + "||" + DownloadPath + "||" + fileName, this.languageComponent1);
            //return;
            excelHelper.AddCellValue(i + 1, j + 2, _IQCNo);//IQC单号
            excelHelper.AddCellValue(i + 3, j + 2, DQMCode);//鼎桥物料编码
            excelHelper.AddCellValue(i + 3, j + 4, CustmCode);//供应商为华为时，华为物料编码
            excelHelper.AddCellValue(i + 3, j + 6, ApplyDate);//iQC申请日期
            excelHelper.AddCellValue(i + 3, j + 8, InvNo);//SAP单据号/IQC检验单号
            excelHelper.AddCellValue(i + 4, j + 2, DQCHLDesc);//鼎桥物料描述基础数据中文长描述
            excelHelper.AddCellValue(i + 4, j + 4, VendorName);//供应商名称
            excelHelper.AddCellValue(i + 4, j + 6, Qty);//IQC送检总数
            excelHelper.AddCellValue(i + 4, j + 8, CUser);//this.GetUserCode());//导出单据人
            excelHelper.AddCellValue(i + 5, j + 6, ecCode);//不良描述
            excelHelper.AddCellValue(i + 10, j + 2, Give);//让步接收
            excelHelper.AddCellValue(i + 10, j + 3, Accept);//特采放行
            excelHelper.AddCellValue(i + 10, j + 4, reForm);//供应商现场整改
            excelHelper.AddCellValue(i + 10, j + 5, reTurn); //退换货
            excelHelper.AddCellValue(i + 10, j + 7, reMark); //具体说明 
            //显示上传图片
            object[] objs_invdoc = _IQCFacade.GetUpLoadFilesByInvDocNo(_IQCNo, "IqcAbnormal");
            if (objs_invdoc != null)
            {
                int t = 13;
                string path = Server.MapPath(this.VirtualHostRoot + "InvDoc/IQC/");
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
