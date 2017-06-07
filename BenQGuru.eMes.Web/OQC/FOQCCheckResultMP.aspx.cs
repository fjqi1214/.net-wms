#region system
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
using System.Web.UI.WebControls.WebParts;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
#endregion

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using System.IO;
using BenQGuru.eMES.IQC;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using BenQGuru.eMES.Domain.TSModel;


namespace BenQGuru.eMES.Web.OQC
{
    public partial class FOQCCheckResultMP : BaseMPageForIQC
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        private MOModel.ItemFacade _itemfacade = null;
        #region Field
        private OQCFacade _OQCFacade = null;
        private InventoryFacade _InventoryFacade = null;
        private WarehouseFacade _WarehouseFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;
        private string _OQCNo = string.Empty;//OQC检验单号（页面跳转带入）
        private string _PickNo = string.Empty;//拣货任务令号（页面跳转带入）
        private string _PickType = string.Empty;//出库类型（页面跳转带入）
        private string _OQCStatus = string.Empty;//OQC状态（页面跳转带入）
        private string _downloadPath = @"\download\";//下载文件夹名称 
        #endregion

        #region Property

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

        /// <summary>
        /// OQC检验单号
        /// </summary>
        public string OqcNo
        {
            get
            {
                if (this.ViewState["OQCNo"] != null)
                {
                    return this.ViewState["OQCNo"].ToString();
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 发货箱单号
        /// </summary>
        public string CarInvNo
        {
            get
            {
                if (this.ViewState["CarInvNo"] != null)
                {
                    return this.ViewState["CarInvNo"].ToString();
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 鼎桥物料号
        /// </summary>
        public string DQMCode
        {
            get
            {
                if (this.ViewState["DQMCode"] != null)
                {
                    return this.ViewState["DQMCode"].ToString();
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 箱号
        /// </summary>
        public string CartonNo
        {
            get
            {
                if (this.ViewState["CartonNo"] != null)
                {
                    return this.ViewState["CartonNo"].ToString();
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 管控类型
        /// </summary>
        public string MControlType
        {
            get
            {
                if (this.ViewState["mControlType"] != null)
                {
                    return this.ViewState["mControlType"].ToString();
                }
                return string.Empty;
            }
        }
        #endregion

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
        protected void Page_Init(object sender, System.EventArgs e)
        {
            PostBackTrigger tri = new PostBackTrigger();
            tri.ControlID = this.cmdEnter.ID;
            (this.FindControl("up1") as UpdatePanel).Triggers.Add(tri);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            _OQCNo = this.GetRequestParam("OQCNo");
            _PickNo = this.GetRequestParam("PickNo");
            _PickType = this.GetRequestParam("PickType");
            _OQCStatus = this.GetRequestParam("OQCStatus");
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                InitDrpAQLStandard();
                InitDrpNGType();
                InitDrpNGDesc();
                this.txtPickNo.Text = _PickNo;
                this.txtStorageOutTypeQurey.Text = _PickType;

                this.txtPickNo.Enabled = false;
                this.txtStorageOutTypeQurey.Enabled = false;
                this.txtCartonNoEdit.Enabled = false;
                JudgeIsFullCheck();
                if (this.rblType.Items[1].Selected)
                {
                    this.drpAQLStandardQuery.SelectedIndex = 0;
                    this.drpAQLStandardQuery.Enabled = false;
                }
                else
                {
                    this.drpAQLStandardQuery.Enabled = true;
                    InitAQLStandard();
                }
            }
        }
        protected void JudgeIsFullCheck()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            object obj_oqc = _OQCFacade.GetOQC(_OQCNo);
            if (obj_oqc == null)
            {
                WebInfoPublish.Publish(this, "OQC单据错误", this.languageComponent1);
                return;
            }
            Domain.OQC.OQC oqc = obj_oqc as Domain.OQC.OQC;
            if (!string.IsNullOrEmpty(oqc.OqcType))
            {
                if (oqc.OqcType == OQCType.OQCType_FullCheck)
                {
                    this.rblType.Items[1].Selected = true;
                    this.rblType.Enabled = false;
                }
                else if (oqc.OqcType == OQCType.OQCType_SpotCheck)
                {
                    this.rblType.Items[0].Selected = true;
                    this.rblType.Enabled = false;
                }
            }
            else
            {
                //amy 加:如果是TBLPICK. GFFLAG=X时默认为加严全检，其他为抽检
                if (_InventoryFacade == null)
                {
                    _InventoryFacade = new InventoryFacade(base.DataProvider);
                }
                object obj_pick = _InventoryFacade.GetPick(oqc.PickNo);
                if (obj_pick == null)
                {
                    WebInfoPublish.Publish(this, "OQC单查询捡令任务号错误", this.languageComponent1);
                    return;
                }
                Pick pick = obj_pick as Pick;
                if (pick.GFFlag.ToUpper() == "X")
                {
                    this.rblType.Items[1].Selected = true;
                    this.rblType.Enabled = true;
                }
                else
                {
                    this.rblType.Items[0].Selected = true;
                    this.rblType.Enabled = true;
                }
                //end
            }
        }
        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        //初始化AQL标准下拉框
        /// <summary>
        /// 初始化AQL标准下拉框
        /// </summary>
        protected void InitDrpAQLStandard()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            object[] objAQL = _OQCFacade.GetAllAQL();
            this.drpAQLStandardQuery.Items.Clear();
            if (objAQL != null)
            {
                foreach (AQL aql in objAQL)
                {
                    this.drpAQLStandardQuery.Items.Add(new ListItem(aql.AqlLevel, string.Format("{0},{1}", aql.AqlLevel, aql.AQLSeq)));
                }
                this.drpAQLStandardQuery.SelectedIndex = 0;

                InitPageInfoByDropDownList(this.drpAQLStandardQuery);

            }

        }
        protected void InitAQLStandard()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            object[] objs_oqcDetail = _OQCFacade.GetOQCDetailByOqcNo(_OQCNo);
            int sumQty = 0;

            if (objs_oqcDetail != null)
            {
                foreach (OQCDetail oqcDetail in objs_oqcDetail)
                {
                    sumQty += oqcDetail.Qty;
                }
            }
            object[] obj_sample = _OQCFacade.GetSampleQTYByIqcQTY1(sumQty);
            if (obj_sample != null)
            {
                this.drpAQLStandardQuery.Items.Clear();
                foreach (AQL aql in obj_sample)
                {
                    this.drpAQLStandardQuery.Items.Add(new ListItem(aql.AqlLevel, string.Format("{0},{1}", aql.AqlLevel, aql.AQLSeq)));
                }
                this.drpAQLStandardQuery.SelectedIndex = 0;

            }
            InitPageInfoByDropDownList(this.drpAQLStandardQuery);
        }
        //AQL标准下拉框索引改变
        protected void drpAQLStandardQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitPageInfoByDropDownList(this.drpAQLStandardQuery);
        }

        //根据AQL标准下拉框值带出页面相关信息
        /// <summary>
        /// 根据下拉框值带出页面相关信息
        /// </summary>
        /// <param name="drp">下拉框DropDownList</param>
        private void InitPageInfoByDropDownList(DropDownList drp)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            if (drp.SelectedIndex >= 0)
            {
                this.txtAQLDesc.Text = "";
                this.txtSamplesNum.Text = "";
                this.txtRejectionNum.Text = "";

                string aqlLevel = drp.SelectedValue.ToString().Split(',')[0];
                string aqlSeq = drp.SelectedValue.ToString().Split(',')[1];
                AQL aql = (AQL)_OQCFacade.GetAQL(Convert.ToInt32(aqlSeq), aqlLevel);
                if (aql != null)
                {
                    this.txtAQLDesc.Text = aql.AqlLevelDesc;
                    this.txtSamplesNum.Text = aql.SampleSize.ToString();
                    this.txtRejectionNum.Text = aql.RejectSize.ToString();
                }
            }
        }

        //缺陷类型下拉框
        protected void InitDrpNGType()
        {
            //if (_SystemSettingFacade == null)
            //{
            //    _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            //}
            //this.drpNGTypeEdit.Items.Add(new ListItem("", ""));
            //this.drpNGTypeEdit.Items.Add(new ListItem("errorcode1", "缺陷类型1"));
            //this.drpNGTypeEdit.Items.Add(new ListItem("errorcode2", "缺陷类型2"));
            //this.drpNGTypeEdit.Items.Add(new ListItem("errorcode3", "缺陷类型3"));

            //if (_SystemSettingFacade == null)
            //{
            //    _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            //}
            //this.drpNGTypeEdit.Items.Add(new ListItem("",""));
            //object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("NGTYPE");
            //foreach (Domain.BaseSetting.Parameter parameter in parameters)
            //{
            //    this.drpNGTypeEdit.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
            //}
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            this.drpNGTypeEdit.Items.Clear();
            this.drpNGTypeEdit.Items.Add(new ListItem("", ""));
            object[] objs = _SystemSettingFacade.GetErrorGroupcode();
            if (objs != null)
            {
                foreach (ErrorCodeGroupA ecg in objs)
                {
                    this.drpNGTypeEdit.Items.Add(new ListItem(ecg.ErrorCodeGroupDescription, ecg.ErrorCodeGroup));
                }
            }
        }

        //缺陷描述下拉框
        protected void InitDrpNGDesc()
        {
            //if (_SystemSettingFacade == null)
            //{
            //    _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            //}
            //this.drpNGDescEdit.Items.Add(new ListItem("", ""));
            //this.drpNGDescEdit.Items.Add(new ListItem("errorcode1", "缺陷描述1"));
            //this.drpNGDescEdit.Items.Add(new ListItem("errorcode2", "缺陷描述2"));
            //this.drpNGDescEdit.Items.Add(new ListItem("errorcode3", "缺陷描述3"));

            //if (_SystemSettingFacade == null)
            //{
            //    _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            //}
            //this.drpNGDescEdit.Items.Add(new ListItem("", ""));
            //object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("NGDESC");
            //foreach (Domain.BaseSetting.Parameter parameter in parameters)
            //{
            //    this.drpNGDescEdit.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
            //}
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            this.drpNGDescEdit.Items.Clear();
            this.drpNGDescEdit.Items.Add(new ListItem("", ""));
            object[] objs = _SystemSettingFacade.GetErrorcode(this.drpNGTypeEdit.SelectedValue);
            if (objs != null)
            {
                foreach (ErrorCodeA ec in objs)
                {
                    this.drpNGDescEdit.Items.Add(new ListItem(ec.ErrorDescription, ec.ErrorCode));
                }
            }

        }
        #endregion

        #region WebGrid

        protected override void InitWebGrid2()
        {
            base.InitWebGrid2();
            this.gridHelper2.AddColumn("OQCNo", "OQC检验单号", null);
            this.gridHelper2.AddColumn("STORAGECODE123", "出库库位", null);
            this.gridHelper2.AddColumn("CarInvNo", "发货箱单号", null);
            this.gridHelper2.AddColumn("CartonNo", "包装箱号", null);
            this.gridHelper2.AddColumn("DQMCode", "鼎桥物料编码", null);
            this.gridHelper2.AddColumn("DQMCODEDESC", "鼎桥物料描述", null);

            this.gridHelper2.AddColumn("OQCType", "检验方式", null);
            this.gridHelper2.AddColumn("MContorlType", "管控类型", null);
            this.gridHelper2.AddColumn("Status", "状态", null);
            this.gridHelper2.AddColumn("AppQty", "送检数量", null);
            this.gridHelper2.AddColumn("NGQty", "缺陷品数", null);
            this.gridHelper2.AddColumn("GiveQty", "让步放行数量", null);
            this.gridHelper2.AddColumn("ReturnQty", "退换货数量", null);
            this.gridHelper2.AddColumn("GfFlag", "光伏标识", null);
            this.gridHelper2.AddDataColumn("DQSItmeCode", "鼎桥S编码", true);

            this.gridHelper2.AddColumn("HWItemCode", "华为物料编码", null);
            this.gridHelper2.AddColumn("GFHWItemCode", "华为物料编码", null);
            this.gridHelper2.AddDataColumn("HWCodeQty", "华为编码数量", true);
            this.gridHelper2.AddColumn("GFPackingSeq", "光伏序号", null);
            this.gridHelper2.AddColumn("Memo", "备注", null);
            this.gridHelper2.AddEditColumn("btnRecordNG2", "记录缺陷");
            this.gridHelper2.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper2.ApplyLanguage(this.languageComponent1);
            this.gridHelper2.RequestData();
            this.ViewState["OQCNo"] = _OQCNo;
            this.gridHelper.RefreshData();
        }

        protected override DataRow GetGridRow2(object obj)
        {
            DataRow row = this.DtSource2.NewRow();
            _InventoryFacade = new InventoryFacade(base.DataProvider);
            _OQCFacade = new OQCFacade(base.DataProvider);


            row["OQCNo"] = ((OQCDetailExt)obj).OqcNo;

            Domain.OQC.OQC o = (Domain.OQC.OQC)_OQCFacade.GetOQC(((OQCDetailExt)obj).OqcNo);


            Pick p = (Pick)_InventoryFacade.GetPick(o.PickNo);
            if (p != null)
                row["STORAGECODE123"] = p.StorageCode;
            else
                row["STORAGECODE123"] = string.Empty;


            row["CarInvNo"] = ((OQCDetailExt)obj).CarInvNo;
            row["CartonNo"] = ((OQCDetailExt)obj).CartonNo;
            row["DQMCode"] = ((OQCDetailExt)obj).DQMCode;
            Domain.MOModel.Material m = (Domain.MOModel.Material)_InventoryFacade.GetMaterialByDQMCode(((OQCDetailExt)obj).DQMCode);
            if (m != null)
                row["DQMCODEDESC"] = m.MchlongDesc;
            else
                row["DQMCODEDESC"] = string.Empty;

            row["OQCType"] = FormatHelper.GetChName(((OQCDetailExt)obj).OqcType);
            row["MContorlType"] = FormatHelper.GetChName(((OQCDetailExt)obj).MControlType);
            row["Status"] = FormatHelper.GetChName(((OQCDetailExt)obj).QcStatus);
            row["AppQty"] = ((OQCDetailExt)obj).Qty;
            row["NGQty"] = ((OQCDetailExt)obj).NgQty;
            row["GiveQty"] = ((OQCDetailExt)obj).GiveQty;
            row["ReturnQty"] = ((OQCDetailExt)obj).ReturnQty;
            row["GfFlag"] = ((OQCDetailExt)obj).GFFlag;
            row["DQSItmeCode"] = ((OQCDetailExt)obj).DQSItemCode;
            row["HWItemCode"] = ((OQCDetailExt)obj).HwItemCode;
            row["GFHWItemCode"] = ((OQCDetailExt)obj).GfHwItemCode;
            row["HWCodeQty"] = ((OQCDetailExt)obj).HWCodeQTY;
            row["GFPackingSeq"] = ((OQCDetailExt)obj).GfPackingSeq;
            row["Memo"] = ((OQCDetailExt)obj).Remark1;

            return row;
        }

        protected override object[] LoadDataSource2(int inclusive, int exclusive)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            return this._OQCFacade.QueryOQCDetail(_OQCNo,
                                                    FormatHelper.CleanString(this.txtPickNo.Text),
                                                    FormatHelper.CleanString(this.txtStorageOutTypeQurey.Text),
                                                    FormatHelper.CleanString(this.txtCartonNoQurey.Text),
                                                    FormatHelper.CleanString(this.txtCarInvNoQurey.Text),
                                                    inclusive, exclusive);
        }

        protected override int GetRowCount2()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            return this._OQCFacade.QueryOQCDetailCount(_OQCNo,
                                                        FormatHelper.CleanString(this.txtPickNo.Text),
                                                        FormatHelper.CleanString(this.txtStorageOutTypeQurey.Text),
                                                        FormatHelper.CleanString(this.txtCartonNoQurey.Text),
                                                        FormatHelper.CleanString(this.txtCarInvNoQurey.Text)
                                                        );
        }




        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("CartonNo", "箱号", null);
            this.gridHelper.AddColumn("DQMCode", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("DQSN", "鼎桥SN", null);
            this.gridHelper.AddColumn("NGQty", "缺陷品数", null);
            this.gridHelper.AddColumn("NGType", "缺陷类型", null);
            this.gridHelper.AddColumn("NGDesc", "缺陷描述", null);
            //add by sam 2016年7月14日
            this.gridHelper.AddColumn("CommitUser", "检验提交人", null);
            this.gridHelper.AddColumn("SQEUser", "SQE判定人", null);
            this.gridHelper.AddColumn("ProcessWay", "处理方式", null);//add by sam SQE判定结果
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddDataColumn("ECode", "缺陷代码", true);
            this.gridHelper.AddDataColumn("NGType1", "缺陷类型", true);

            this.gridHelper.AddDefaultColumn(true, true);
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            DataRow row = this.DtSource.NewRow();
            OQCDetailEC asnec = obj as OQCDetailEC;
            object[] objs_ecg = _SystemSettingFacade.GetErrorGroupcode(asnec.EcgCode);
            object[] objs_ec = _SystemSettingFacade.GetErrorcodeByEcode(asnec.ECode);
            row["CartonNo"] = asnec.CartonNo;
            row["DQMCode"] = asnec.DQMCode;
            row["DQSN"] = asnec.SN;
            row["NGQty"] = asnec.NgQty;
            if (objs_ecg != null)
                row["NGType"] = (objs_ecg[0] as ErrorCodeGroupA).ErrorCodeGroupDescription;
            else
                row["NGType"] = asnec.EcgCode;
            if (objs_ec != null)
                row["NGDesc"] = (objs_ec[0] as ErrorCodeA).ErrorDescription;
            else
                row["NGDesc"] = asnec.ECode;
            //row["NGType"] = ((OQCDetailEC)obj).EcgCode;
            //row["NGDesc"] = ((OQCDetailEC)obj).ECode;
            #region  add by sam
            row["CommitUser"] = asnec.CUser;// EC的CUser
            row["SQEUser"] = asnec.SQECUser;
            row["ProcessWay"] = this.GetSQEStatusName(asnec.SqeStatus);
            #endregion

            row["Memo"] = asnec.Remark1;

            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }

            return _OQCFacade.QueryOQCDetailEC(OqcNo, CarInvNo, CartonNo, DQMCode, inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            return _OQCFacade.QueryOQCDetailECCount(OqcNo, CarInvNo, CartonNo, DQMCode);
        }

        #endregion

        #region Button
        protected void drpNGTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitDrpNGDesc();
        }
        //点击Grid中按钮
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "btnRecordNG2")
            {
                string oqcNo = row.Items.FindItemByKey("OQCNo").Text.Trim();
                string carInvNo = row.Items.FindItemByKey("CarInvNo").Text.Trim();
                string cartonNo = row.Items.FindItemByKey("CartonNo").Text.Trim();
                string dqMCode = row.Items.FindItemByKey("DQMCode").Text.Trim();
                string mControlType = row.Items.FindItemByKey("MContorlType").Text.Trim();

                this.txtCartonNoEdit.Text = cartonNo;
                this.txtDQMCODEEdit.Text = dqMCode;
                this.txtCartonINVEdit.Text = carInvNo;
                //Grid(下)显示数据
                this.ViewState["OQCNo"] = oqcNo;
                this.ViewState["CarInvNo"] = carInvNo;
                this.ViewState["CartonNo"] = cartonNo;
                this.ViewState["DQMCode"] = dqMCode;
                this.ViewState["mControlType"] = FormatHelper.GetChName(mControlType);
                this.ViewState["IsAdd"] = true;
                this.gridHelper.RequestData();
                if (_WarehouseFacade == null)
                {
                    _WarehouseFacade = new WarehouseFacade(base.DataProvider);
                }
                object[] materobj = _WarehouseFacade.GetMaterialInfoByQDMCode(dqMCode);
                if (materobj == null)
                {
                    this.DataProvider.RollbackTransaction();
                    // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
                    WebInfoPublish.Publish(this, "物料表没有物料：" + this.txtDQMCODEEdit.Text, this.languageComponent1);
                    return;

                }
                Domain.MOModel.Material mater = materobj[0] as Domain.MOModel.Material;
                if (mater.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                {
                    this.txtSNEdit.Enabled = true;
                    this.txtNGQtyEdit.Enabled = false;
                    this.txtSNEdit.Focus();
                }
                else
                {
                    this.txtSNEdit.Enabled = false;
                    this.txtNGQtyEdit.Enabled = true;
                    this.txtNGQtyEdit.Focus();

                }
            }
        }

        //查询按钮
        protected override void cmdQuery_Click(object sender, EventArgs e)
        {
            //初始化字段 Grid(下)显示数据清空
            this.ViewState["OQCNo"] = 0;
            this.ViewState["CarInvNo"] = 0;
            this.ViewState["CartonNo"] = 0;
            this.ViewState["DQMCode"] = 0;
            base.cmdQuery_Click(sender, e);
        }

        //保存
        protected override void UpdateDomainObject(object domainObject)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            Domain.OQC.OQC OQC = _OQCFacade.GetOQC(_OQCNo) as Domain.OQC.OQC;
            if (OQC.Status == OQCStatus.OQCStatus_SQEJudge)
            {
                WebInfoPublish.PublishInfo(this, "SQE判定状态不能修改", this.languageComponent1);
                return;
            }
            if (OQC.Status == OQCStatus.OQCStatus_OQCClose)
            {
                WebInfoPublish.PublishInfo(this, _OQCNo + "已经关闭，不能修改", this.languageComponent1);
                return;
            }
            try
            {
                OQCDetailEC oqcDetailEC = (OQCDetailEC)domainObject;
                if ((bool)this.ViewState["IsAdd"])
                {
                    #region  判断录入的SN是否在包装内
                    if (!string.IsNullOrEmpty(this.txtSNEdit.Text.Trim()))
                    {

                        object[] cartoninvmar_obj = _WarehouseFacade.GetOqcdetailsn(this.txtCartonINVEdit.Text.Trim(), oqcDetailEC.OqcNo, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNEdit.Text)), oqcDetailEC.CartonNo);
                        if (cartoninvmar_obj == null)
                        {
                            WebInfoPublish.Publish(this, "SN不在包装内", this.languageComponent1);
                            return;
                        }
                    }
                    if (!string.IsNullOrEmpty(this.txtNGQtyEdit.Text))
                    {
                        object objs_carqty = _OQCFacade.GetOQCDetailByIQCNoAndCartonNo(oqcDetailEC.OqcNo, oqcDetailEC.CartonNo);
                        if (objs_carqty != null)
                        {
                            OQCDetail carqty = objs_carqty as OQCDetail;
                            int num = 0;
                            try
                            {
                                num = int.Parse(this.txtNGQtyEdit.Text);
                            }
                            catch
                            {
                                WebInfoPublish.Publish(this, "不良数必须是数字格式", this.languageComponent1);
                                return;
                            }
                            if (num > carqty.Qty)
                            {
                                WebInfoPublish.Publish(this, "不良数量不能大于送检数量", this.languageComponent1);
                                return;
                            }
                        }
                    }
                    #endregion
                    this.DataProvider.BeginTransaction();


                    #region CheckData
                    //1》	同一箱号NGFLAG=Y的记录只能存在一笔
                    //2》	单件管控：SN记录NGFLAG=N的记录只能存在一笔
                    //3》	批管控：同一箱号NGFLAG=N记录的SUM(NGQTY)不能大于箱号送检数量
                    int oqcDetailEcCount = _OQCFacade.GetOQCDetailEcCount(oqcDetailEC.OqcNo, oqcDetailEC.CartonNo, "Y");
                    if (oqcDetailEcCount > 0)
                    {
                        if (oqcDetailEC.NgFlag == "Y")
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.PublishInfo(this, "同一箱号NGFLAG=Y的记录只能存在一笔", this.languageComponent1);
                            return;
                        }
                    }
                    if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                    {
                        int asnIQCDetailECCount = _OQCFacade.GetOQCDetailECCount(oqcDetailEC.OqcNo, oqcDetailEC.SN, "N");
                        if (asnIQCDetailECCount > 0)
                        {
                            if (oqcDetailEC.NgFlag == "N")
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "SN记录NGFLAG=N的记录只能存在一笔", this.languageComponent1);
                                return;
                            }
                        }

                    }
                    if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_LOT)
                    {
                        Domain.OQC.OQC oqc = (Domain.OQC.OQC)_OQCFacade.GetOQC(_OQCNo);
                        if (oqc != null)
                        {
                            int ngQtyCount = _OQCFacade.GetSumNgQtyFromOQCDetailEc(oqcDetailEC.OqcNo, oqcDetailEC.CartonNo, "N", DQMCode);
                            object CartonInvDetailMaterial_obj = _WarehouseFacade.QueryCartonInvDetailMaterial(this.txtCartonINVEdit.Text.Trim(), oqcDetailEC.CartonNo, DQMCode);
                            if (CartonInvDetailMaterial_obj == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "包装中找不到信息", this.languageComponent1);
                                return;
                            }
                            CartonInvDetailMaterial CartonInvDetailMaterial = CartonInvDetailMaterial_obj as CartonInvDetailMaterial;
                            //PIS描述不清楚  同一鼎桥物料送检数量？？
                            if (ngQtyCount > CartonInvDetailMaterial.QTY)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "大于箱号送检数量", this.languageComponent1);
                                return;
                            }
                        }
                    }

                    #endregion


                    this.ViewState["IsAdd"] = false;//新增状态更改为false

                    //object[] objOqcDetaileEc = _OQCFacade.GetOQCDetailEC(oqcDetailEC.ECode, oqcDetailEC.CarInvNo,
                    //                                                      oqcDetailEC.OqcNo, oqcDetailEC.CartonNo,
                    //                                                      oqcDetailEC.MCode, oqcDetailEC.SN);
                    object[] objOqcDetaileEc = _OQCFacade.GetOQCDetailEC(oqcDetailEC.EcgCode, oqcDetailEC.ECode, oqcDetailEC.CarInvNo,
                                                                         oqcDetailEC.OqcNo, oqcDetailEC.CartonNo,
                                                                         oqcDetailEC.DQMCode, oqcDetailEC.SN);
                    if (objOqcDetaileEc != null)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.PublishInfo(this, "重复添加", this.languageComponent1);
                        return;
                    }
                    this._OQCFacade.AddOQCDetailEC((OQCDetailEC)domainObject);
                    #region UpdateTable
                    OQCDetail oqcDetail = (OQCDetail)_OQCFacade.GetOQCDetail(oqcDetailEC.CarInvNo,
                                                                                oqcDetailEC.OqcNo,
                                                                                oqcDetailEC.CartonNo,
                                                                                oqcDetailEC.MCode);
                    if (oqcDetail != null)
                    {
                        oqcDetail.NgQty = _OQCFacade.GetSumNgQtyFromOQCDetailEc2(oqcDetailEC.OqcNo, "", oqcDetailEC.CartonNo);
                        oqcDetail.QcStatus = "N";
                        _OQCFacade.UpdateOQCDetail(oqcDetail);
                    }
                    OQCDetailSN oqcDetailSN = (OQCDetailSN)_OQCFacade.GetOQCDetailSN(
                                                                                        oqcDetailEC.CarInvNo,
                                                                                        oqcDetailEC.OqcNo,
                                                                                        oqcDetailEC.SN);
                    if (oqcDetailSN != null)
                    {
                        oqcDetailSN.QcStatus = "N";
                        _OQCFacade.UpdateOQCDetailSN(oqcDetailSN);
                    }
                    if (string.IsNullOrEmpty(txtSNEdit.Text) && string.IsNullOrEmpty(txtNGQtyEdit.Text))
                    {
                        object[] objs_asnIqcDetailSN1 = _OQCFacade.GetAsnIqcDetailSNByIqcNoAndCartonNo(oqcDetailEC.OqcNo, oqcDetailEC.CartonNo);
                        if (objs_asnIqcDetailSN1 != null && objs_asnIqcDetailSN1.Length > 0)
                        {
                            foreach (OQCDetailSN asnIqcDetailSN1 in objs_asnIqcDetailSN1)
                            {
                                asnIqcDetailSN1.QcStatus = "N";
                                _OQCFacade.UpdateOQCDetailSN(asnIqcDetailSN1);
                            }
                        }

                    }
                    #endregion

                    this.DataProvider.CommitTransaction();
                }
                else
                {
                    this._OQCFacade.UpdateOQCDetailEC((OQCDetailEC)domainObject);
                    this.DataProvider.CommitTransaction();
                    WebInfoPublish.Publish(this, "保存成功！", this.languageComponent1);
                }
                this.ViewState["OQCNo"] = oqcDetailEC.OqcNo;
                this.ViewState["CarInvNo"] = oqcDetailEC.CarInvNo;
                this.ViewState["CartonNo"] = oqcDetailEC.CartonNo;
                this.ViewState["DQMCode"] = oqcDetailEC.DQMCode;
                this.gridHelper.RequestData();
                this.gridHelper2.RequestData();
            }
            catch (Exception ex)
            {

                WebInfoPublish.Publish(this, "保存失败：" + ex.Message, this.languageComponent1);
                this.DataProvider.RollbackTransaction();
            }

        }

        //删除
        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            this.DataProvider.BeginTransaction();
            try
            {
                this._OQCFacade.DeleteOQCDetailEC((OQCDetailEC[])domainObjects.ToArray(typeof(OQCDetailEC)));

                //并修改当前箱号对应的TBLASNIQCDETAIL. NGQTY= TBLASNIQCDETAIL. NGQTY- TBLASNIQCDETAILEC. NGQTY
                OQCDetailEC asnIqcDetailEc0 = domainObjects.ToArray()[0] as OQCDetailEC;
                Domain.OQC.OQC OQC = _OQCFacade.GetOQC(asnIqcDetailEc0.OqcNo) as Domain.OQC.OQC;
                if (OQC.Status == IQCStatus.IQCStatus_SQEJudge)
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.PublishInfo(this, "状态是SQE判定状态，不能删除", this.languageComponent1);
                    return;
                }
                foreach (OQCDetailEC asnIqcDetailEc in (OQCDetailEC[])domainObjects.ToArray(typeof(OQCDetailEC)))
                {
                    OQCDetail asnIqcDetail = (OQCDetail)_OQCFacade.GetOQCDetail(asnIqcDetailEc.CarInvNo, asnIqcDetailEc.OqcNo, asnIqcDetailEc.CartonNo, asnIqcDetailEc.MCode);
                    {
                        asnIqcDetail.NgQty = _OQCFacade.GetSumNgQtyFromAsnIQCDetailEc1(asnIqcDetailEc.OqcNo, asnIqcDetailEc.CartonNo, "");
                        //asnIqcDetail.NgQty = asnIqcDetail.NgQty - asnIqcDetailEc.NgQty;
                        if (asnIqcDetail.NgQty == 0)
                        {
                            asnIqcDetail.QcStatus = string.Empty;
                        }
                        _OQCFacade.UpdateOQCDetail(asnIqcDetail);
                    }
                }
                this.DataProvider.CommitTransaction();
                this.gridHelper2.RequestData();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, "删除失败：" + ex.Message, this.languageComponent1);
            }
        }

        //导入
        protected void cmdEnter_ServerClick(object sender, EventArgs e)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            if (this.FileImport.PostedFile != null)
            {
                try
                {
                    HttpPostedFile postedFile = this.FileImport.PostedFile;

                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);
                    InvDoc invDoc = _InventoryFacade.CreateNewInvDoc();

                    invDoc.InvDocNo = _OQCNo;
                    invDoc.InvDocType = "OqcAbnormal";
                    invDoc.DocType = Path.GetExtension(postedFile.FileName);
                    invDoc.DocName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    invDoc.DocSize = postedFile.ContentLength / 1024;
                    invDoc.UpUser = this.GetUserCode();
                    invDoc.UpfileDate = dbDateTime.DBDate;
                    invDoc.MaintainUser = this.GetUserCode();
                    invDoc.MaintainDate = dbDateTime.DBDate;
                    invDoc.MaintainTime = dbDateTime.DBTime;

                    string path = Server.MapPath(this.VirtualHostRoot + "InvDoc/" + "OQC/");
                    string fileName = string.Format("{0}_OqcAbnormal_{1}{2}{3}",
                        _OQCNo, dbDateTime.DBDate, dbDateTime.DBTime, invDoc.DocType);

                    invDoc.ServerFileName = fileName;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    this.FileImport.PostedFile.SaveAs(path + fileName);
                    invDoc.Dirname = "OQC";
                    _InventoryFacade.AddInvDoc(invDoc);
                    WebInfoPublish.PublishInfo(this, "$Success_UpLoadFile", this.languageComponent1);
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
        }

        //返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FOQCCheckListQuery.aspx"));
        }

        //提交
        protected void cmdCommit_ServerClick(object sender, EventArgs e)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);
            _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            Domain.OQC.OQC OQC = _OQCFacade.GetOQC(_OQCNo) as Domain.OQC.OQC;
            if (OQC.Status == OQCStatus.OQCStatus_SQEJudge)
            {
                WebInfoPublish.PublishInfo(this, "SQE判定状态不能再提交", this.languageComponent1);
                return;
            }
            if (OQC.Status == OQCStatus.OQCStatus_OQCClose)
            {
                WebInfoPublish.PublishInfo(this, _OQCNo + "已经关闭，不能再提交", this.languageComponent1);
                return;
            }
            try
            {
                this.DataProvider.BeginTransaction();

                int ngQty = _OQCFacade.GetSumNgQtyFromOQCDetail(_OQCNo);
                //1)	OQC检验单下所有行没有缺陷品数SUM(TBLOQCDETAIL.NGQTY)=0，则按免检按钮功能处理
                //注：不检查OQC检验单状态为：Release:初始化
                //TBLOQC.OQCTYPE：更新为介面单选选择的检验方式：SpotCheck:抽检；FullCheck:全检
                Pick pick = (Pick)_InventoryFacade.GetPickByOqcNo(OQC.OqcNo);
                if (pick == null)
                    throw new Exception(OQC.OqcNo + "对应的拣货任务令不存在！");

                if (ngQty == 0)
                {
                    this.ToSTS(_OQCNo);
                    if (_OQCFacade == null)
                    {
                        _OQCFacade = new OQCFacade(base.DataProvider);
                    }
                    Domain.OQC.OQC oqc = (Domain.OQC.OQC)_OQCFacade.GetOQC(_OQCNo);
                    if (this.rblType.Items[0].Selected)
                        oqc.OqcType = OQCType.OQCType_SpotCheck;
                    else if (this.rblType.Items[1].Selected)
                        oqc.OqcType = OQCType.OQCType_FullCheck;
                    _OQCFacade.UpdateOQC(oqc);

                }
                else if (this.rblType.Items[1].Selected && ngQty > 0)
                {
                    #region 更新表 TBLOQC,TBLOQCDETAIL,TBLOQCDETAILSN

                    _OQCFacade.UpdateOQC(OQCType.OQCType_FullCheck, OQCStatus.OQCStatus_SQEJudge, "N", _OQCNo);
                    _OQCFacade.UpdateOQCDetail("Y", _OQCNo, "N");
                    _OQCFacade.UpdateOQCDetailSN("Y", _OQCNo, "N");

                    DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);

                    InvInOutTrans trans1 = _WarehouseFacade.CreateNewInvInOutTrans();
                    trans1.CartonNO = string.Empty;
                    trans1.DqMCode = string.Empty;
                    trans1.FacCode = string.Empty;
                    trans1.FromFacCode = string.Empty;
                    trans1.FromStorageCode = string.Empty;
                    trans1.InvNO = pick.InvNo;//.InvNo;
                    trans1.InvType = pick.PickType;
                    trans1.LotNo = string.Empty;
                    trans1.MaintainDate = dbTime1.DBDate;
                    trans1.MaintainTime = dbTime1.DBTime;
                    trans1.MaintainUser = this.GetUserCode();
                    trans1.MCode = string.Empty;
                    trans1.ProductionDate = 0;
                    trans1.Qty = 0;
                    trans1.Serial = 0;
                    trans1.StorageAgeDate = 0;
                    trans1.StorageCode = string.Empty;
                    trans1.SupplierLotNo = string.Empty;
                    trans1.TransNO = pick.PickNo;// asnIqc.IqcNo;
                    trans1.TransType = "OUT";
                    trans1.Unit = string.Empty;
                    trans1.ProcessType = "OQC";
                    _WarehouseFacade.AddInvInOutTrans(trans1);
                    #endregion
                }
                else if (this.rblType.Items[0].Selected && ngQty > 0)
                {
                    if (string.IsNullOrEmpty(this.txtRejectionNum.Text))
                    {
                        WebInfoPublish.PublishInfo(this, "请选择AQL标准", this.languageComponent1);
                        this.drpAQLStandardQuery.Focus();
                        return;
                    }
                    int rejectSize = Convert.ToInt32(this.txtRejectionNum.Text);//页面拒收数量
                    #region ngQty < rejectSize
                    if (ngQty < rejectSize)
                    {
                        // #region 更新表 TBLOQC,TBLOQCDETAIL,TBLOQCDETAILSN,TBLCartonInvoices

                        //1》通过OQC检验单号更新送检单表(TBLOQC)数据
                        _OQCFacade.UpdateOQC(OQCType.OQCType_SpotCheck, OQCStatus.OQCStatus_SQEJudge, "Y", _OQCNo);//OQCStatus.OQCStatus_OQCClose

                        //2》通过OQC检验单号更新检单明细表(TBLOQCDETAIL)
                        _OQCFacade.UpdateOQCDetail("Y", _OQCNo, "N");

                        //3》通过OQC检验单号更新检单明细SN表
                        _OQCFacade.UpdateOQCDetailSN("Y", _OQCNo, "N");

                        //4》更新发货箱单头信息表(TBLCartonInvoices）

                        if (pick.GFFlag.ToUpper() == "X")
                        {
                            if (CheckAllOQCStatusIsOQCClose(_OQCNo))
                            {
                                CARTONINVOICES cartonInvoices = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoicesByOqcNo(_OQCNo);
                                if (cartonInvoices != null)
                                {
                                    cartonInvoices.STATUS = CartonInvoices_STATUS.Status_OQCClose;
                                    _WarehouseFacade.UpdateCartoninvoices(cartonInvoices);
                                }
                            }
                            else
                            {
                                //WebInfoPublish.PublishInfo(this, "提交失败：OQC单号"+_OQCNo+"没有全部检验完成", this.languageComponent1);
                                //this.DataProvider.RollbackTransaction();
                            }
                        }
                        else
                        {
                            CARTONINVOICES cartonInvoices = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoicesByOqcNo(_OQCNo);
                            if (cartonInvoices != null)
                            {
                                cartonInvoices.STATUS = CartonInvoices_STATUS.Status_OQCClose;
                                _WarehouseFacade.UpdateCartoninvoices(cartonInvoices);
                            }
                        }

                        DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);

                        InvInOutTrans trans1 = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans1.CartonNO = string.Empty;
                        trans1.DqMCode = string.Empty;
                        trans1.FacCode = string.Empty;
                        trans1.FromFacCode = string.Empty;
                        trans1.FromStorageCode = string.Empty;
                        trans1.InvNO = pick.InvNo;//.InvNo;
                        trans1.InvType = pick.PickType;
                        trans1.LotNo = string.Empty;
                        trans1.MaintainDate = dbTime1.DBDate;
                        trans1.MaintainTime = dbTime1.DBTime;
                        trans1.MaintainUser = this.GetUserCode();
                        trans1.MCode = string.Empty;
                        trans1.ProductionDate = 0;
                        trans1.Qty = 0;
                        trans1.Serial = 0;
                        trans1.StorageAgeDate = 0;
                        trans1.StorageCode = string.Empty;
                        trans1.SupplierLotNo = string.Empty;
                        trans1.TransNO = pick.PickNo;// asnIqc.IqcNo;
                        trans1.TransType = "OUT";
                        trans1.Unit = string.Empty;
                        trans1.ProcessType = "OQC";
                        _WarehouseFacade.AddInvInOutTrans(trans1);
                        //  #endregion
                    }
                    #endregion
                    #region ngQty>rejectSize
                    else
                    {
                        // #region 更新表 TBLOQC,TBLOQCDETAIL,TBLOQCDETAILSN

                        _OQCFacade.UpdateOQC(OQCType.OQCType_SpotCheck, OQCStatus.OQCStatus_SQEJudge, "N", _OQCNo);
                        _OQCFacade.UpdateOQCDetail("Y", _OQCNo, "N");
                        _OQCFacade.UpdateOQCDetailSN("Y", _OQCNo, "N");

                        DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);

                        InvInOutTrans trans1 = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans1.CartonNO = string.Empty;
                        trans1.DqMCode = string.Empty;
                        trans1.FacCode = string.Empty;
                        trans1.FromFacCode = string.Empty;
                        trans1.FromStorageCode = string.Empty;
                        trans1.InvNO = pick.InvNo;//.InvNo;
                        trans1.InvType = pick.PickType;
                        trans1.LotNo = string.Empty;
                        trans1.MaintainDate = dbTime1.DBDate;
                        trans1.MaintainTime = dbTime1.DBTime;
                        trans1.MaintainUser = this.GetUserCode();
                        trans1.MCode = string.Empty;
                        trans1.ProductionDate = 0;
                        trans1.Qty = 0;
                        trans1.Serial = 0;
                        trans1.StorageAgeDate = 0;
                        trans1.StorageCode = string.Empty;
                        trans1.SupplierLotNo = string.Empty;
                        trans1.TransNO = pick.PickNo;// asnIqc.IqcNo;
                        trans1.TransType = "OUT";
                        trans1.Unit = string.Empty;
                        trans1.ProcessType = "OQC";
                        _WarehouseFacade.AddInvInOutTrans(trans1);
                        // #endregion
                    }
                    #endregion

                }

                #region add by sam 2016年8月17日
                string aqlLevel = drpAQLStandardQuery.SelectedValue.ToString().Split(',')[0];
                //string aqlSeq = drp.SelectedValue.ToString().Split(',')[1];
                if (!string.IsNullOrEmpty(aqlLevel))
                {
                    Domain.OQC.OQC oqc = (Domain.OQC.OQC)_OQCFacade.GetOQC(_OQCNo);
                    oqc.AQL = aqlLevel;
                    _OQCFacade.UpdateOQC(oqc);
                }
                #endregion
                if (_OQCFacade.IsOQCFinish(pick.PickNo))
                {
                    pick.Status = PickHeadStatus.PickHeadStatus_PackingListing;
                    _WarehouseFacade.UpdatePick(pick);
                }


                WarehouseFacade ware = new WarehouseFacade(base.DataProvider);
                IQC.IQCFacade _IQCFacade = new IQC.IQCFacade(base.DataProvider);
                SendMail mail = ShareLib.ShareKit.OQCExceptionThenGenerMail(OQC, GetUserCode(), _IQCFacade, ware);
                if (mail != null)
                    ware.AddSendMail(mail);

                WebInfoPublish.PublishInfo(this, "提交成功", this.languageComponent1);
                this.DataProvider.CommitTransaction();
                this.gridHelper2.RequestData();
            }
            catch (Exception ex)
            {
                WebInfoPublish.PublishInfo(this, "提交失败：" + ex.Message, this.languageComponent1);
                this.DataProvider.RollbackTransaction();
            }

        }

        //免检
        protected void cmdStatusSTS_ServerClick(object sender, EventArgs e)
        {
            if (_OQCStatus != IQCStatus.IQCStatus_Release)
            {
                //OQC检验单号: {0} 状态不是初始化
                WebInfoPublish.Publish(this, string.Format("OQC检验单号: {0} 状态不是初始化，不能免检 ", _OQCNo), this.languageComponent1);
                return;
            }
            //免检
            try
            {
                this.DataProvider.BeginTransaction();

                ToSTS(_OQCNo);

                this.DataProvider.CommitTransaction();
                WebInfoPublish.Publish(this, "免检成功", this.languageComponent1);
                this.gridHelper2.RequestData();//刷新页面


            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, "免检失败：" + ex.Message, this.languageComponent1);
                this.gridHelper2.RequestData();//刷新页面

            }
        }

        //导出OQC异常联络单
        protected void cmdExportOQCACL_ServerClick(object sender, EventArgs e)
        {
            //TODO：未完成，具体值没有获取
            string fileName = "AbnormalContactList.xlsx";
            ExportExcel(fileName);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Query)
            {
                this.drpNGTypeEdit.Enabled = true;
                this.drpNGDescEdit.Enabled = true;
                //this.txtSNEdit.Enabled = true;
                if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                {
                    this.txtSNEdit.Enabled = true;
                }
                else
                {
                    this.txtSNEdit.Enabled = false;
                }
                this.txtNGQtyEdit.Enabled = true;
                this.cmdSave.Disabled = false;
            }
            if (pageAction == PageActionType.Add)
            {
                this.drpNGTypeEdit.Enabled = true;
                this.drpNGDescEdit.Enabled = true;
                this.txtSNEdit.Enabled = true;
                this.txtNGQtyEdit.Enabled = true;
                this.cmdSave.Disabled = false;
            }
            if (pageAction == PageActionType.Update)
            {
                this.drpNGTypeEdit.Enabled = false;
                this.drpNGDescEdit.Enabled = false;
                this.txtSNEdit.Enabled = false;
                this.txtNGQtyEdit.Enabled = false;

            }
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }

            OQCDetailEC oqcDetailEC = (OQCDetailEC)this.ViewState["OQCDetailECEdit"];
            if (oqcDetailEC == null)
            {
                this.ViewState["IsAdd"] = true;
                oqcDetailEC = _OQCFacade.CreateNewOQCDetailEC();
            }

            if ((bool)this.ViewState["IsAdd"])
            {
                oqcDetailEC.OqcNo = OqcNo;
                oqcDetailEC.CarInvNo = CarInvNo;
                oqcDetailEC.CartonNo = FormatHelper.CleanString(this.txtCartonNoEdit.Text, 40);
                object str = _WarehouseFacade.GetMaterialFromDQMCode(DQMCode);
                if (str != null)
                {
                    oqcDetailEC.MCode = (str as Domain.MOModel.Material).MCode;          //SAP物料号
                }
                else
                {
                    oqcDetailEC.MCode = string.Empty;
                }
                //oqcDetailEC.MCode = DQMCode;
                oqcDetailEC.DQMCode = DQMCode;
                oqcDetailEC.EcgCode = FormatHelper.CleanString(this.drpNGTypeEdit.SelectedValue, 40);
                oqcDetailEC.ECode = FormatHelper.CleanString(this.drpNGDescEdit.SelectedValue, 40);
                //oqcDetailEC.NgFlag = this.txtNGQtyEdit.Text.Trim() == null ? "Y" : "N";
                if (string.IsNullOrEmpty(this.txtSNEdit.Text) && string.IsNullOrEmpty(this.txtNGQtyEdit.Text.Trim()))
                {
                    oqcDetailEC.NgFlag = "Y";
                }
                else
                {
                    oqcDetailEC.NgFlag = "N";
                }
                oqcDetailEC.CUser = this.GetUserName();
                oqcDetailEC.CDate = FormatHelper.GetNowDBDateTime(base.DataProvider).DBDate;
                oqcDetailEC.CTime = FormatHelper.GetNowDBDateTime(base.DataProvider).DBTime;

            }
            oqcDetailEC.MaintainUser = this.GetUserName();
            oqcDetailEC.SN = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNEdit.Text, 40));
            //oqcDetailEC.NgQty = Convert.ToInt32(this.txtNGQtyEdit.Text.Trim() ?? "1");
            oqcDetailEC.NgQty = string.IsNullOrEmpty(this.txtNGQtyEdit.Text.Trim()) ? 1 : Convert.ToInt32(this.txtNGQtyEdit.Text.Trim());
            oqcDetailEC.Remark1 = FormatHelper.CleanString(this.txtMemoEdit.Text, 200);
            decimal sampleSize = 0;
            if (!string.IsNullOrEmpty(txtSamplesSize1.Text) && decimal.TryParse(txtSamplesSize1.Text, out sampleSize))
                oqcDetailEC.SAMPLESIZE = (int)sampleSize;
            this.ViewState["OQCDetailECEdit"] = null;
            return oqcDetailEC;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            string oqcNo = _OQCNo;
            string carInvNo = CarInvNo;
            //string dqMCode = DQMCode;
            //string cartonNo = CartonNo;
            string dqMCode = row.Items.FindItemByKey("DQMCode").Text.Trim();
            string cartonNo = row.Items.FindItemByKey("CartonNo").Text.Trim();
            string eCode = row.Items.FindItemByKey("ECode").Text.Trim();
            string egCode = row.Items.FindItemByKey("NGType1").Text.Trim();
            string sn = row.Items.FindItemByKey("DQSN").Text.Trim();
            object[] obj = _OQCFacade.GetOQCDetailEC(egCode, eCode, carInvNo, oqcNo, cartonNo, dqMCode, sn);

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
                this.drpNGTypeEdit.SelectedIndex = 0;
                this.drpNGDescEdit.SelectedIndex = 0;
                this.txtSNEdit.Text = "";
                this.txtNGQtyEdit.Text = "";
                this.txtCartonNoEdit.Text = "";
                this.txtMemoEdit.Text = "";
                this.txtSamplesSize1.Text = string.Empty;
                return;
            }

            try
            {
                this.drpNGTypeEdit.SelectedValue = ((OQCDetailEC)obj).EcgCode;
            }
            catch (Exception)
            {

                this.drpNGTypeEdit.SelectedIndex = 0; ;
            }
            try
            {
                this.drpNGDescEdit.SelectedValue = ((OQCDetailEC)obj).ECode;
            }
            catch (Exception)
            {

                this.drpNGDescEdit.SelectedIndex = 0; ;
            }
            this.txtSNEdit.Text = ((OQCDetailEC)obj).SN;
            this.txtNGQtyEdit.Text = ((OQCDetailEC)obj).NgQty.ToString();
            this.txtCartonNoEdit.Text = ((OQCDetailEC)obj).CartonNo;
            this.txtMemoEdit.Text = ((OQCDetailEC)obj).Remark1;
            this.ViewState["IsAdd"] = false;
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblNGTypeEdit, this.drpNGTypeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblNGDescEdit, this.drpNGDescEdit, 40, true));
            manager.Add(new LengthCheck(this.lblCartonNoEdit, this.txtCartonNoEdit, 40, true));

            if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
            {
                manager.Add(new LengthCheck(this.lblSNEdit, this.txtSNEdit, 40, false));
            }
            else
            {
                manager.Add(new LengthCheck(this.lblSNEdit, this.txtSNEdit, 40, false));
            }

            //if (string.IsNullOrEmpty(this.txtNGQtyEdit.Text))
            //{
            //    this.txtNGQtyEdit.Text = "1";
            //}
            if (!string.IsNullOrEmpty(this.txtNGQtyEdit.Text))
            {
                if (Convert.ToInt32(this.txtNGQtyEdit.Text.Trim()) == 0)
                {
                    WebInfoPublish.Publish(this, "不良数不能为0", this.languageComponent1);
                    return false;
                }
                manager.Add(new NumberCheck(this.lblNGQtyEdit, this.txtNGQtyEdit, false));
            }
            //manager.Add(new NumberCheck(this.lblNGQtyEdit, this.txtNGQtyEdit, false));

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
            return new string[]{
                                ((OQCDetailEC)obj).CartonNo,
                                ((OQCDetailEC)obj).EcgCode,
                                ((OQCDetailEC)obj).ECode,
                                ((OQCDetailEC)obj).SN,
                                ((OQCDetailEC)obj).NgQty.ToString(),
                                ((OQCDetailEC)obj).Remark1
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"CartonNo",
                                    "DQMCode",
                                    "DQSN",
	                                "NGQty",
                                    "NGType",
                                    "NGDesc",
                                    "Memo"};
        }

        protected override string[] FormatExportRecord2(object obj)
        {
            return new string[]{
                                ((OQCDetailExt)obj).OqcNo,
                                ((OQCDetailExt)obj).CarInvNo,
                                ((OQCDetailExt)obj).CartonNo,
                                ((OQCDetailExt)obj).DQMCode,
                                FormatHelper.GetChName(((OQCDetailExt)obj).OqcType),
                                FormatHelper.GetChName(((OQCDetailExt)obj).MControlType),
                                FormatHelper.GetChName(((OQCDetailExt)obj).QcStatus),
                                ((OQCDetailExt)obj).Qty.ToString(),
                                ((OQCDetailExt)obj).NgQty.ToString(),
                                ((OQCDetailExt)obj).GiveQty.ToString(),
                                ((OQCDetailExt)obj).GFFlag,
                                ((OQCDetailExt)obj).DQSItemCode,
                                ((OQCDetailExt)obj).GfHwItemCode,
                                ((OQCDetailExt)obj).HWCodeQTY,
                                ((OQCDetailExt)obj).GfPackingSeq,
                                ((OQCDetailExt)obj).Remark1
                               };
        }

        protected override string[] GetColumnHeaderText2()
        {
            return new string[] {	"OQCNo",
                                    "CarInvNo",
                                    "CartonNo",
                                    "DQMCode",
	                                "OQCType",
                                    "MContorlType",
                                    "Status",	
                                    "AppQty",
                                    "NGQty",
                                    "GiveQty",
                                    "GfFlag",
                                    "DQSItmeCode",
                                    "GFHWItemCode",
                                    "HWCodeQty",
                                    "GFPackingSeq",
                                    "Memo"};
        }

        #endregion

        #region Method

        //免检
        /// <summary>
        /// 免检
        /// </summary>
        /// <param name="iqcNo">OQC检验单号</param>
        private void ToSTS(string oqcNo)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);
            _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            //1、更新OQC单表(TBLOQC)
            _OQCFacade.UpdateOQC(OQCType.OQCType_ExemptCheck, OQCStatus.OQCStatus_OQCClose, "Y", oqcNo);

            //2、更新OQC单明细表(TBLOQCDETAIL)
            _OQCFacade.UpdateOQCDetail("Y", oqcNo);

            //3、更新OQC单明细SN信息表(TBLOQCDETAILSN)
            _OQCFacade.UpdateOQCDetailSN("Y", oqcNo);

            //4、更新发货箱单头信息表(TBLCartonInvoices)
            Pick pick = (Pick)_InventoryFacade.GetPickByOqcNo(oqcNo);
            if (pick != null)
            {
                //if (pick.GFFlag == "X")
                //{
                //    if (CheckAllOQCStatusIsOQCClose(oqcNo))
                //    {
                //        CARTONINVOICES cartonInvoices = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoicesByOqcNo(oqcNo);
                //        if (cartonInvoices != null)
                //        {
                //            cartonInvoices.STATUS = CartonInvoices_STATUS.Status_OQCClose;
                //            _WarehouseFacade.UpdateCartoninvoices(cartonInvoices);
                //        }
                //    }
                //    else
                //    {
                //        throw new Exception("OQC单号" + oqcNo + "没有全部检验完成");
                //    }
                //}
                //else
                //{

                if (_OQCFacade.IsOQCFinish(pick.PickNo))
                {
                    pick.Status = PickHeadStatus.PickHeadStatus_PackingListing;
                    _WarehouseFacade.UpdatePick(pick);
                }
                CARTONINVOICES cartonInvoices = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoicesByOqcNo(oqcNo);
                if (cartonInvoices != null)
                {
                    cartonInvoices.STATUS = CartonInvoices_STATUS.Status_OQCClose;
                    _WarehouseFacade.UpdateCartoninvoices(cartonInvoices);
                }

                //}

                #region 在invinouttrans表中增加一条数据
                WarehouseFacade facade = new WarehouseFacade(base.DataProvider);
                DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);

                InvInOutTrans trans1 = facade.CreateNewInvInOutTrans();
                trans1.CartonNO = string.Empty;
                trans1.DqMCode = string.Empty;
                trans1.FacCode = string.Empty;
                trans1.FromFacCode = string.Empty;
                trans1.FromStorageCode = string.Empty;
                trans1.InvNO = pick.InvNo;//.InvNo;
                trans1.InvType = pick.PickType;
                trans1.LotNo = string.Empty;
                trans1.MaintainDate = dbTime1.DBDate;
                trans1.MaintainTime = dbTime1.DBTime;
                trans1.MaintainUser = this.GetUserCode();
                trans1.MCode = string.Empty;
                trans1.ProductionDate = 0;
                trans1.Qty = 0;
                trans1.Serial = 0;
                trans1.StorageAgeDate = 0;
                trans1.StorageCode = string.Empty;
                trans1.SupplierLotNo = string.Empty;
                trans1.TransNO = pick.PickNo;// asnIqc.IqcNo;
                trans1.TransType = "OUT";
                trans1.Unit = string.Empty;
                trans1.ProcessType = "OQC";
                facade.AddInvInOutTrans(trans1);


                Domain.OQC.OQC asnIqcHead = (Domain.OQC.OQC)_OQCFacade.GetOQC(oqcNo);
                object[] objs_oqcDetail = _OQCFacade.GetOQCDetailByOqcNo(_OQCNo);
                if (objs_oqcDetail != null)
                {
                    foreach (OQCDetail asnIqc in objs_oqcDetail)
                    {

                        InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = asnIqc.DQMCode;
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = asnIqc.CarInvNo;//.InvNo;
                        trans.InvType = asnIqcHead.OqcType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = dbTime1.DBDate;
                        trans.MaintainTime = dbTime1.DBTime;
                        trans.MaintainUser = this.GetUserCode();
                        trans.MCode = asnIqc.MCode;
                        trans.ProductionDate = 0;
                        trans.Qty = asnIqc.Qty;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = string.Empty;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = oqcNo;// asnIqc.IqcNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "OQC";
                        facade.AddInvInOutTrans(trans);
                    }
                }
                #endregion


            }
        }

        //检查OQC检验单所有行状态为OQCClose
        /// <summary>
        /// 检查OQC检验单所有行状态为OQCClose
        /// </summary>
        /// <param name="iqcNo">OQC检验单号</param>
        /// <returns>全部是OQCClose：true;否则：false</returns>
        private bool CheckAllOQCStatusIsOQCClose(string oqcNo)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(base.DataProvider);
            }
            object[] objOQC = _OQCFacade.GetOQCByOqcNo(oqcNo);
            if (objOQC != null)
            {
                foreach (Domain.OQC.OQC oqc in objOQC)
                {
                    if (oqc.Status != OQCStatus.OQCStatus_OQCClose)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

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

        #endregion
    }
}
