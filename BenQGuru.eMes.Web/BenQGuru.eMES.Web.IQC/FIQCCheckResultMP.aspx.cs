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
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using System.IO;
using System.Text;
using BenQGuru.eMES.Domain.TSModel;

namespace BenQGuru.eMES.Web.IQC
{
    public partial class FIQCCheckResultMP : BaseMPageForIQC
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        #region Field
        private IQCFacade _IQCFacade = null;
        private OQCFacade _OQCFacade = null;
        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;
        private WarehouseFacade _WarehouseFacade = null;
        private string _IQCNo = string.Empty;//IQC检验单号（页面跳转带入）
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
        /// IQC检验单号
        /// </summary>
        public string IqcNo
        {
            get
            {
                if (this.ViewState["iqcNo"] != null)
                {
                    return this.ViewState["iqcNo"].ToString();
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["iqcNo"] = value;
            }
        }

        /// <summary>
        /// ASN单据号
        /// </summary>
        public string StNo
        {
            get
            {
                if (this.ViewState["stNo"] != null)
                {
                    return this.ViewState["stNo"].ToString();
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["stNo"] = value;
            }
        }

        /// <summary>
        /// ASN单行项目
        /// </summary>
        public string StLine
        {
            get
            {
                if (this.ViewState["stLine"] != null)
                {
                    return this.ViewState["stLine"].ToString();
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["stLine"] = value;
            }
        }

        /// <summary>
        /// 箱号
        /// </summary>
        public string CartonNo
        {
            get
            {
                if (this.ViewState["cartonNo"] != null)
                {
                    return this.ViewState["cartonNo"].ToString();
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["cartonNo"] = value;
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
            set
            {
                this.ViewState["mControlType"] = value;
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
            _IQCNo = this.GetRequestParam("IQCNo");
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                //InitDrpAQLStandard();
                InitDrpNGType();
                InitDrpNGDesc();
                this.txtCartonNoEdit.Enabled = false;
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
                this.ViewState["iqcNo"] = _IQCNo;
            }
            _IQCNo = this.GetRequestParam("IQCNo");

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
                //InitAQLStandard();
                //object obj_iqc = _OQCFacade.GetOQC(_IQCNo);
                //AsnIQC iqc = obj_iqc as AsnIQC;
                //object obj_sample=_IQCFacade.GetSampleQTYByIqcQTY(iqc.Qty);
                //if (obj_sample != null)
                //{
                //    AQL sample = obj_sample as AQL;
                //    this.drpAQLStandardQuery.SelectedIndex = this.drpAQLStandardQuery.Items.IndexOf(this.drpAQLStandardQuery.Items.FindByText(sample.AqlLevel));
                //}

            }

        }

        protected void RadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblType.SelectedIndex == 0)//"抽检"
            {
                drpAQLStandardQuery.Enabled = true;
                txtAQLDesc.Enabled = true;
                txtSamplesNum.Enabled = true;
                txtRejectionNum.Enabled = true;
            }
            else
            {
                drpAQLStandardQuery.Enabled = false;
                txtAQLDesc.Enabled = false;
                txtSamplesNum.Enabled = false;
                txtRejectionNum.Enabled = false;
            }

        }

        //AQL标准下拉框索引改变
        protected void drpAQLStandardQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitPageInfoByDropDownList(this.drpAQLStandardQuery);
        }
        //protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.rblType.Items[1].Selected)
        //    {
        //        this.lblAQLStandardQuery.Enabled = false;
        //    }
        //    else
        //    {
        //        this.lblAQLStandardQuery.Enabled = true;
        //        InitAQLStandard();
        //    }
        //}
        protected void InitAQLStandard()
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            object obj_iqc = _IQCFacade.GetAsnIQC(_IQCNo);
            AsnIQC iqc = obj_iqc as AsnIQC;
            object[] obj_sample = _IQCFacade.GetSampleQTYByIqcQTY1(iqc.Qty);
            if (obj_sample != null)
            {
                this.drpAQLStandardQuery.Items.Clear();

                foreach (AQL aql in obj_sample)
                {
                    this.drpAQLStandardQuery.Items.Add(new ListItem(aql.AqlLevel, string.Format("{0},{1}", aql.AqlLevel, aql.AQLSeq)));
                }
                if (!string.IsNullOrEmpty(iqc.AQLLevel))
                    this.drpAQLStandardQuery.SelectedValue = iqc.AQLLevel;
                else
                    this.drpAQLStandardQuery.SelectedIndex = 0;

                InitPageInfoByDropDownList(this.drpAQLStandardQuery);

                //object obj_iqc = _OQCFacade.GetOQC(_IQCNo);
                //AsnIQC iqc = obj_iqc as AsnIQC;
                //object obj_sample=_IQCFacade.GetSampleQTYByIqcQTY(iqc.Qty);
                //if (obj_sample != null)
                //{
                //    AQL sample = obj_sample as AQL;
                //    this.drpAQLStandardQuery.SelectedIndex = this.drpAQLStandardQuery.Items.IndexOf(this.drpAQLStandardQuery.Items.FindByText(sample.AqlLevel));
                //}




                //AQL sample = obj_sample as AQL;
                //this.drpAQLStandardQuery.SelectedIndex = this.drpAQLStandardQuery.Items.IndexOf(this.drpAQLStandardQuery.Items.FindByText(sample.AqlLevel));
            }
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
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            this.drpNGTypeEdit.Items.Clear();
            this.drpNGTypeEdit.Items.Add(new ListItem("", ""));
            //this.drpNGTypeEdit.Items.Add(new ListItem("缺陷类型1", "缺陷类型1"));
            //this.drpNGTypeEdit.Items.Add(new ListItem("缺陷类型2", "缺陷类型2"));
            //this.drpNGTypeEdit.Items.Add(new ListItem("缺陷类型3", "缺陷类型3"));
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
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            this.drpNGDescEdit.Items.Clear();
            this.drpNGDescEdit.Items.Add(new ListItem("", ""));
            //this.drpNGDescEdit.Items.Add(new ListItem("缺陷描述1", "缺陷描述1"));
            //this.drpNGDescEdit.Items.Add(new ListItem("缺陷描述2", "缺陷描述2"));
            //this.drpNGDescEdit.Items.Add(new ListItem("缺陷描述3", "缺陷描述3"));
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
            this.gridHelper2.AddColumn("IQCNo", "IQC检验单号", null);
            this.gridHelper2.AddColumn("STORAGECODE1234", "入库库位", null);
            this.gridHelper2.AddColumn("CartonNo", "箱号", null);
            this.gridHelper2.AddDataColumn("StNo", "ASN单号", true);
            this.gridHelper2.AddDataColumn("StLine", "ASN单行项目", true);
            this.gridHelper2.AddColumn("DQMCode", "鼎桥物料编码", null);
            this.gridHelper2.AddColumn("DQMCODEDESC", "鼎桥物料描述", null);


            this.gridHelper2.AddColumn("VMCode", "供应商物料编码", null);
            this.gridHelper2.AddColumn("IQCWay", "管控类型", null);
            this.gridHelper2.AddDataColumn("mControlType", "管控类型", true);
            this.gridHelper2.AddColumn("Status", "状态", null);
            this.gridHelper2.AddDataColumn("IQCStatus", "状态", true);
            this.gridHelper2.AddColumn("IQCType", "检验方式", null);
            this.gridHelper2.AddColumn("AQLResult", "AQL结果", null);
            this.gridHelper2.AddColumn("AppQty", "送检数量", null);
            this.gridHelper2.AddColumn("NGQty", "缺陷品数", null);
            this.gridHelper2.AddColumn("ReturnQty", "退换货数量", null);
            this.gridHelper2.AddColumn("ReformQty", "现场整改数量", null);
            this.gridHelper2.AddColumn("GiveQty", "让步接收数量", null);
            this.gridHelper2.AddColumn("AcceptQty", "特采数量", null);
            this.gridHelper2.AddColumn("Memo", "备注", null);
            this.gridHelper2.AddEditColumn("btnRecordNG2", "记录缺陷");

            this.gridHelper2.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper2.ApplyLanguage(this.languageComponent1);
            this.gridHelper2.RequestData();
            this.ViewState["iqcNo"] = _IQCNo;
            this.gridHelper.RefreshData();
        }

        protected override DataRow GetGridRow2(object obj)
        {
            DataRow row = this.DtSource2.NewRow();

            row["IQCNo"] = ((AsnIQCDetailExt)obj).IqcNo;

            _InventoryFacade = new InventoryFacade(base.DataProvider);
            ASN a = (ASN)_InventoryFacade.GetASN(((AsnIQCDetailExt)obj).StNo);
            if (a != null)
                row["STORAGECODE1234"] = a.StorageCode;
            else
                row["STORAGECODE1234"] = string.Empty;


            row["CartonNo"] = ((AsnIQCDetailExt)obj).CartonNo;
            row["StNo"] = ((AsnIQCDetailExt)obj).StNo;
            row["StLine"] = ((AsnIQCDetailExt)obj).StLine;
            row["DQMCode"] = ((AsnIQCDetailExt)obj).DQMCode;

            Domain.MOModel.Material m = (Domain.MOModel.Material)_InventoryFacade.GetMaterialByDQMCode(((AsnIQCDetailExt)obj).DQMCode);
            if (m != null)
                row["DQMCODEDESC"] = m.MchlongDesc;
            else
                row["DQMCODEDESC"] = string.Empty;

            row["VMCode"] = ((AsnIQCDetailExt)obj).VendorMCode;
            row["IQCWay"] = FormatHelper.GetChName(((AsnIQCDetailExt)obj).MControlType);
            row["mControlType"] = ((AsnIQCDetailExt)obj).MControlType;
            row["Status"] = this.GetStatusName(((AsnIQCDetailExt)obj).Status);
            row["IQCStatus"] = ((AsnIQCDetailExt)obj).Status;
            row["IQCType"] = FormatHelper.GetChName(((AsnIQCDetailExt)obj).IqcType);

            if (((AsnIQCDetailExt)obj).IqcType == "SpotCheck")
            {
                row["AQLResult"] = FormatHelper.GetChName(((AsnIQCDetailExt)obj).QcStatus);

            }


            row["AppQty"] = ((AsnIQCDetailExt)obj).Qty;
            row["NGQty"] = ((AsnIQCDetailExt)obj).NgQty;
            row["ReturnQty"] = ((AsnIQCDetailExt)obj).ReturnQty;
            row["ReformQty"] = ((AsnIQCDetailExt)obj).ReformQty;
            row["GiveQty"] = ((AsnIQCDetailExt)obj).GiveQty;
            row["AcceptQty"] = ((AsnIQCDetailExt)obj).AcceptQty;
            row["Memo"] = ((AsnIQCDetailExt)obj).Remark1;

            return row;
        }

        protected override object[] LoadDataSource2(int inclusive, int exclusive)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }

            return this._IQCFacade.QueryAsnIQCDetail(_IQCNo,
                                                    FormatHelper.CleanString(this.txtCartonNoQurey.Text),
                                                    inclusive, exclusive);
        }

        protected override int GetRowCount2()
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            return this._IQCFacade.QueryAsnIQCDetailCount(_IQCNo, FormatHelper.CleanString(this.txtCartonNoQurey.Text));
        }




        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("CartonNo", "箱号", null);
            this.gridHelper.AddColumn("NGType", "缺陷类型", null);
            this.gridHelper.AddColumn("NGDesc", "缺陷描述", null);
            this.gridHelper.AddDataColumn("ECode", "缺陷代码", true);
            this.gridHelper.AddDataColumn("NGType1", "缺陷类型", true);
            this.gridHelper.AddColumn("SN", "SN", null);
            this.gridHelper.AddColumn("NGQty", "缺陷品数", null);
            //add by sam 2016年7月14日
            this.gridHelper.AddColumn("CommitUser", "检验提交人", null);
            this.gridHelper.AddColumn("SQEUser", "SQE判定人", null);
            this.gridHelper.AddColumn("ProcessWay", "处理方式", null);//add by sam SQE判定结果
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("stline", "stline", null);
            this.gridHelper.AddDefaultColumn(true, true);
            this.gridWebGrid.Columns.FromKey("stline").Hidden = true;
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.gridHelper.RequestData();
        }

        protected override DataRow GetGridRow(object obj)
        {
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            DataRow row = this.DtSource.NewRow();
            AsnIQCDetailEc asnec = obj as AsnIQCDetailEc;
            object[] objs_ecg = _SystemSettingFacade.GetErrorGroupcode(asnec.EcgCode);
            object[] objs_ec = _SystemSettingFacade.GetErrorcodeByEcode(asnec.ECode);
            row["CartonNo"] = asnec.CartonNo;
            if (objs_ecg != null)
                row["NGType"] = (objs_ecg[0] as ErrorCodeGroupA).ErrorCodeGroupDescription;
            else
                row["NGType"] = asnec.EcgCode;
            if (objs_ec != null)
                row["NGDesc"] = (objs_ec[0] as ErrorCodeA).ErrorDescription;
            else
                row["NGDesc"] = asnec.ECode;
            row["ECode"] = asnec.ECode;
            row["NGType1"] = asnec.EcgCode;
            row["SN"] = asnec.SN;
            row["NGQty"] = asnec.NgQty;

            #region  add by sam
            row["CommitUser"] = asnec.CUser;// EC的CUser
            row["SQEUser"] = asnec.SQECUser;
            row["ProcessWay"] = this.GetSQEStatusName(asnec.SqeStatus);
            #endregion

            row["Memo"] = asnec.Remark1;
            row["stline"] = asnec.StLine;



            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }

            return _IQCFacade.QueryAsnIQCDetailEc(IqcNo, StNo, StLine, CartonNo, inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            return _IQCFacade.QueryAsnIQCDetailEcCount(IqcNo, StNo, StLine, CartonNo);
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
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            if (commandName == "btnRecordNG2")
            {
                string iqcNo = row.Items.FindItemByKey("IQCNo").Text.Trim();
                string stNo = row.Items.FindItemByKey("StNo").Text.Trim();
                string stLine = row.Items.FindItemByKey("StLine").Text.Trim();
                string cartonNo = row.Items.FindItemByKey("CartonNo").Text.Trim();
                string mControlType = row.Items.FindItemByKey("mControlType").Text.Trim();

                this.txtCartonNoEdit.Text = cartonNo;
                //Grid(下)显示数据
                this.ViewState["iqcNo"] = iqcNo;
                this.ViewState["stNo"] = stNo;
                this.ViewState["stLine"] = stLine;
                this.ViewState["cartonNo"] = cartonNo;
                this.ViewState["mControlType"] = mControlType;
                this.ViewState["IsAdd"] = true;

                this.gridHelper.RequestData();

                if (mControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
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
            this.ViewState["iqcNo"] = 0;
            this.ViewState["stNo"] = 0;
            this.ViewState["stLine"] = 0;
            this.ViewState["cartonNo"] = 0;
            //base.cmdQuery_Click(sender, e);

            for (int i = 0; i < this.gridWebGrid2.Rows.Count; i++)
            {
                if (FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonNoQurey.Text.Trim())) == this.gridWebGrid2.Rows[i].Items.FindItemByKey("CartonNo").Text)
                //if (FormatHelper.CleanString(this.txtCartonNoQurey.Text.Trim()) == this.gridWebGrid2.Rows[i].Items.FindItemByKey("CartonNo").Text)
                {
                    this.gridWebGrid2.Rows[i].Items.FindItemByKey("Check").Value = true;
                }
                else
                {
                    this.gridWebGrid2.Rows[i].Items.FindItemByKey("Check").Value = false;
                }
            }
        }

        //保存
        protected override void UpdateDomainObject(object domainObject)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            AsnIQC asnIQC = _IQCFacade.GetAsnIQC(_IQCNo) as AsnIQC;
            if (asnIQC.Status == IQCStatus.IQCStatus_SQEJudge)
            {
                WebInfoPublish.PublishInfo(this, "SQE判定状态不能再修改", this.languageComponent1);
                return;
            }
            if (asnIQC.Status == IQCStatus.IQCStatus_IQCClose)
            {
                WebInfoPublish.PublishInfo(this, _IQCNo + "已经关闭，不能再修改", this.languageComponent1);
                return;
            }
            try
            {
                this.DataProvider.BeginTransaction();
                AsnIQCDetailEc asnIQCDetailEc = (AsnIQCDetailEc)domainObject;
                if ((bool)this.ViewState["IsAdd"])
                {
                    #region  判断录入的SN是否在包装内
                    if (!string.IsNullOrEmpty(this.txtSNEdit.Text.Trim()))
                    {

                        object[] cartoninvmar_obj = _WarehouseFacade.GetIqcdetailsn(asnIQCDetailEc.IqcNo, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNEdit.Text)), asnIQCDetailEc.CartonNo);
                        if (cartoninvmar_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.Publish(this, "SN不在包装内", this.languageComponent1);
                            return;
                        }
                    }
                    if (!string.IsNullOrEmpty(this.txtNGQtyEdit.Text))
                    {
                        object objs_carqty = _IQCFacade.GetASNIQCDetailByIQCNoAndCartonNo(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo);
                        if (objs_carqty != null)
                        {
                            AsnIQCDetail carqty = objs_carqty as AsnIQCDetail;
                            int num = 0;
                            try
                            {
                                num = int.Parse(this.txtNGQtyEdit.Text);
                            }
                            catch
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "不良数必须是数字格式", this.languageComponent1);
                                return;
                            }
                            if (num > carqty.Qty)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "不良数量不能大于送检数量", this.languageComponent1);
                                return;
                            }
                        }
                    }
                    #endregion

                    #region CheckData
                    //1》	同一箱号NGFLAG=Y的记录只能存在一笔
                    //2》	单件管控：SN记录NGFLAG=N的记录只能存在一笔
                    //3》	批管控：同一箱号NGFLAG=N记录的SUM(NGQTY)不能大于箱号送检数量
                    int asnIQCDetailEcCount = _IQCFacade.GetAsnIQCDetailEcCount(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, "Y");
                    if (asnIQCDetailEcCount > 0)
                    {
                        if (asnIQCDetailEc.NgFlag == "Y")
                        {
                            this.DataProvider.RollbackTransaction();
                            WebInfoPublish.PublishInfo(this, "同一箱号NGFLAG=Y的记录只能存在一笔", this.languageComponent1);
                            return;
                        }
                    }
                    if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                    {
                        int asnIQCDetailECCount = _IQCFacade.GetAsnIQCDetailECCount(asnIQCDetailEc.IqcNo, asnIQCDetailEc.SN, "N");
                        if (asnIQCDetailECCount > 0)
                        {
                            if (asnIQCDetailEc.NgFlag == "N")
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "SN记录NGFLAG=N的记录只能存在一笔", this.languageComponent1);
                                return;
                            }
                        }

                    }
                    if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_LOT)
                    {
                        AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(_IQCNo);
                        if (asnIqc != null)
                        {
                            int ngQtyCount = _IQCFacade.GetSumNgQtyFromAsnIQCDetailEc(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, "N");
                            if (ngQtyCount > asnIqc.Qty)
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.PublishInfo(this, "大于箱号送检数量", this.languageComponent1);
                                return;
                            }
                        }
                    }

                    #endregion


                    this.ViewState["IsAdd"] = false;//新增状态更改为false

                    //object[] objAsnIqcDetaileEc = _IQCFacade.GetAsnIQCDetailEc(asnIQCDetailEc.ECode, asnIQCDetailEc.StLine,
                    //asnIQCDetailEc.IqcNo, asnIQCDetailEc.StNo,
                    //asnIQCDetailEc.SN);
                    object[] objAsnIqcDetaileEc = _IQCFacade.GetAsnIQCDetailEc(asnIQCDetailEc.EcgCode, asnIQCDetailEc.ECode, asnIQCDetailEc.StLine,
                                                                         asnIQCDetailEc.IqcNo, asnIQCDetailEc.StNo,
                                                                         asnIQCDetailEc.SN);
                    if (objAsnIqcDetaileEc != null)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.PublishInfo(this, "重复添加", this.languageComponent1);
                        return;
                    }
                    this._IQCFacade.AddAsnIQCDetailEc(asnIQCDetailEc);

                    AsnIQCDetail asnIqcDetail = (AsnIQCDetail)_IQCFacade.GetAsnIQCDetail(Convert.ToInt32(asnIQCDetailEc.StLine),
                                                                                        asnIQCDetailEc.IqcNo,
                                                                                        asnIQCDetailEc.StNo);
                    #region UpdateTable
                    if (asnIqcDetail != null)
                    {
                        //asnIqcDetail.NgQty = _IQCFacade.GetSumNgQtyFromAsnIQCDetailEc(asnIQCDetailEc.IqcNo, "");
                        asnIqcDetail.NgQty = _IQCFacade.GetSumNgQtyFromAsnIQCDetailEc1(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, "");
                        asnIqcDetail.QcStatus = "N";
                        _IQCFacade.UpdateAsnIQCDetail(asnIqcDetail);
                    }
                    AsnIqcDetailSN asnIqcDetailSN = (AsnIqcDetailSN)_IQCFacade.GetAsnIqcDetailSN(Convert.ToInt32(asnIQCDetailEc.StLine),
                                                                                                    asnIQCDetailEc.IqcNo,
                                                                                                    asnIQCDetailEc.SN,
                                                                                                    asnIQCDetailEc.StNo);
                    if (asnIqcDetailSN != null)
                    {
                        asnIqcDetailSN.QcStatus = "N";
                        _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN);
                    }
                    if (string.IsNullOrEmpty(txtSNEdit.Text) && string.IsNullOrEmpty(txtNGQtyEdit.Text))
                    {
                        object[] objs_asnIqcDetailSN1 = _IQCFacade.GetAsnIqcDetailSNByIqcNoAndCartonNo(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo);
                        if (objs_asnIqcDetailSN1 != null && objs_asnIqcDetailSN1.Length > 0)
                        {
                            foreach (AsnIqcDetailSN asnIqcDetailSN1 in objs_asnIqcDetailSN1)
                            {
                                asnIqcDetailSN1.QcStatus = "N";
                                _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN1);
                            }
                        }

                    }
                    #endregion

                    //#region add by sam
                    //if (this.rblType.Items[0].Selected)//抽检
                    //{
                    //    AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(_IQCNo);
                    //    if (asnIqc != null)
                    //    {
                    //        _IQCFacade.UpdateAsnIQCDetailByIqcno(_IQCNo, asnIqc.QcStatus);
                    //    }
                    //}
                    //#endregion
                    this.DataProvider.CommitTransaction();
                }
                else
                {
                    this._IQCFacade.UpdateAsnIQCDetailEc(asnIQCDetailEc);
                    this.DataProvider.CommitTransaction();
                    WebInfoPublish.Publish(this, "保存成功！", this.languageComponent1);
                }
                this.ViewState["iqcNo"] = asnIQCDetailEc.IqcNo;
                this.ViewState["stNo"] = asnIQCDetailEc.StNo;
                this.ViewState["stLine"] = asnIQCDetailEc.StLine;
                this.ViewState["cartonNo"] = asnIQCDetailEc.CartonNo;
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
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            this.DataProvider.BeginTransaction();
            try
            {

              

                //并修改当前箱号对应的TBLASNIQCDETAIL. NGQTY= TBLASNIQCDETAIL. NGQTY- TBLASNIQCDETAILEC. NGQTY
                AsnIQCDetailEc asnIqcDetailEc0 = domainObjects.ToArray()[0] as AsnIQCDetailEc;
                AsnIQC IQC = _IQCFacade.GetAsnIQC(asnIqcDetailEc0.IqcNo) as AsnIQC;
                if (IQC.Status == IQCStatus.IQCStatus_SQEJudge)
                {
                    this.DataProvider.RollbackTransaction();
                    WebInfoPublish.PublishInfo(this, "状态是SQE判定状态，不能删除", this.languageComponent1);
                    return;
                }

                this._IQCFacade.DeleteAsnIQCDetailEc((AsnIQCDetailEc[])domainObjects.ToArray(typeof(AsnIQCDetailEc)));
                foreach (AsnIQCDetailEc asnIqcDetailEc in (AsnIQCDetailEc[])domainObjects.ToArray(typeof(AsnIQCDetailEc)))
                {
                    AsnIQCDetail asnIqcDetail = (AsnIQCDetail)_IQCFacade.GetAsnIQCDetail(Convert.ToInt32(asnIqcDetailEc.StLine), asnIqcDetailEc.IqcNo, asnIqcDetailEc.StNo);
                    if (asnIqcDetail != null)
                    {
                        asnIqcDetail.NgQty = _IQCFacade.GetSumNgQtyFromAsnIQCDetailEc1(asnIqcDetailEc.IqcNo, asnIqcDetailEc.CartonNo, "");
                        //asnIqcDetail.NgQty = asnIqcDetail.NgQty - asnIqcDetailEc.NgQty;
                        //asnIqcDetail.ReturnQty
                        if (asnIqcDetail.NgQty == 0)
                        {
                            asnIqcDetail.QcStatus = string.Empty;
                        }
                        _IQCFacade.UpdateAsnIQCDetail(asnIqcDetail);
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

        //提交
        protected void cmdCommit_ServerClick(object sender, EventArgs e)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);
            AsnIQC iqc = _IQCFacade.GetAsnIQC(_IQCNo) as AsnIQC;

            if (iqc == null)
            {
                WebInfoPublish.PublishInfo(this, _IQCNo + "单号不存在！", this.languageComponent1);
                return;

            }
            if (iqc.Status == IQCStatus.IQCStatus_SQEJudge)
            {
                WebInfoPublish.PublishInfo(this, "SQE判定状态不能再提交", this.languageComponent1);
                return;
            }
            if (iqc.Status != IQCStatus.IQCStatus_WaitCheck && iqc.Status != IQCStatus.IQCStatus_Release)
            {

                WebInfoPublish.PublishInfo(this, _IQCNo + "必须是待检验或者是初始化才能提交！", this.languageComponent1);
                return;
            }
            if (iqc.Status == IQCStatus.IQCStatus_IQCClose)
            {
                WebInfoPublish.PublishInfo(this, _IQCNo + "已经关闭，不能再提交", this.languageComponent1);
                return;
            }
            try
            {
                this.DataProvider.BeginTransaction();

                int ngQty = _IQCFacade.GetSumNgQtyFromAsnIQCDetail(_IQCNo);
                if (ngQty == 0)
                {
                    this.ToSTS(_IQCNo);

                    if (this.rblType.Items[0].Selected)
                        iqc.IqcType = OQCType.OQCType_SpotCheck;
                    else if (this.rblType.Items[1].Selected)
                        iqc.IqcType = OQCType.OQCType_FullCheck;
                    iqc.AQLLevel = drpAQLStandardQuery.SelectedValue;
                    iqc.Status = "IQCClose";
                    iqc.QcStatus = "Y";
                    _IQCFacade.UpdateAsnIQC(iqc);

                }
                else if (this.rblType.Items[1].Selected && ngQty > 0)
                {
                    #region 更新表 TBLASNIQC,TBLASNIQCDETAIL,TBLASNIQCDETAILSN

                    if (iqc != null)
                    {
                        iqc.IqcType = IQCType.IQCType_FullCheck;
                        iqc.Status = IQCStatus.IQCStatus_SQEJudge;
                        iqc.AQLLevel = drpAQLStandardQuery.SelectedValue;
                        iqc.QcStatus = "N";

                        _IQCFacade.UpdateAsnIQC(iqc);
                        ToSTS1(IqcNo);
                        object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(_IQCNo);
                        if (objAsnIqcDetail != null)
                        {
                            foreach (AsnIQCDetail asnIQCDetail in objAsnIqcDetail)
                            {

                                asnIQCDetail.QcStatus = "Y";
                                asnIQCDetail.QcPassQty = asnIQCDetail.Qty;
                                _IQCFacade.UpdateAsnIQCDetail(asnIQCDetail);

                            }
                        }

                        object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(_IQCNo);
                        if (objAsnIqcDetailSN != null)
                        {
                            foreach (AsnIqcDetailSN asnIqcDetailSN in objAsnIqcDetailSN)
                            {

                                asnIqcDetailSN.QcStatus = "Y";
                                _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN);


                                Asndetailsn asnDetailSn = (Asndetailsn)_InventoryFacade.GetAsndetailsn(asnIqcDetailSN.Sn, asnIqcDetailSN.StNo, Convert.ToInt32(asnIqcDetailSN.StLine));

                                if (asnDetailSn != null)
                                {
                                    asnDetailSn.QcStatus = "Y";
                                    _InventoryFacade.UpdateAsndetailsn(asnDetailSn);
                                }
                            }
                        }
                    }
                    ToSTS1(IqcNo);
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
                    if (ngQty < rejectSize)
                    {
                        #region 更新表 TBLASNIQC,TBLASNIQCDETAIL,TBLASNIQCDETAILSN,TBLASNDETAILITEM,TBLASNDETAIL

                        //1》通过IQC检验单号更新送检单表(TBLASNIQC)数据

                        if (iqc != null)
                        {
                            iqc.IqcType = IQCType.IQCType_SpotCheck;
                            iqc.Status = IQCStatus.IQCStatus_SQEJudge;//IQCStatus.IQCStatus_IQCClose;
                            iqc.QcStatus = "Y";


                            //2》	通过IQC检验单号更新检单明细表(TBLASNIQCDETAIL)
                            object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(_IQCNo);
                            if (objAsnIqcDetail != null)
                            {
                                foreach (AsnIQCDetail asnIQCDetail in objAsnIqcDetail)
                                {

                                    asnIQCDetail.QcStatus = "Y";
                                    asnIQCDetail.QcPassQty = asnIQCDetail.Qty;
                                    _IQCFacade.UpdateAsnIQCDetail(asnIQCDetail);

                                    //更新ASN明细表(TBLASNDETAIL)
                                    ASNDetail asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(Convert.ToInt32(asnIQCDetail.StLine), asnIQCDetail.StNo);
                                    if (asnDetail != null)
                                    {

                                        asnDetail.QcPassQty = asnDetail.ReceiveQty;
                                        asnDetail.Status = ASNLineStatus.IQCClose;

                                        _InventoryFacade.UpdateASNDetail(asnDetail);
                                    }

                                    //更新ASN明细对应单据行明细表(TBLASNDETAILITEM)
                                    //object[] objAsnDetailItem = _InventoryFacade.GetAsnDetailItem(asnIQCDetail.StNo, Convert.ToInt32(asnIQCDetail.StLine));
                                    //if (objAsnDetailItem != null)
                                    //{
                                    //    foreach (Asndetailitem asnDetailItem in objAsnDetailItem)
                                    //    {
                                    //        asnDetailItem.QcpassQty = asnDetailItem.ReceiveQty;
                                    //        asnDetailItem.ActQty = asnDetailItem.QcpassQty;
                                    //        _InventoryFacade.UpdateAsndetailitem(asnDetailItem);
                                    //    }
                                    //}
                                }
                            }

                            //3》通过IQC检验单号更新检单明细SN表
                            object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(_IQCNo);
                            if (objAsnIqcDetailSN != null)
                            {
                                foreach (AsnIqcDetailSN asnIqcDetailSN in objAsnIqcDetailSN)
                                {

                                    asnIqcDetailSN.QcStatus = "Y";
                                    _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN);


                                    Asndetailsn asnDetailSn = (Asndetailsn)_InventoryFacade.GetAsndetailsn(asnIqcDetailSN.Sn, asnIqcDetailSN.StNo, Convert.ToInt32(asnIqcDetailSN.StLine));
                                    if (asnDetailSn != null)
                                    {
                                        asnDetailSn.QcStatus = "Y";
                                        _InventoryFacade.UpdateAsndetailsn(asnDetailSn);
                                    }
                                }
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region 更新表 TBLASNIQC,TBLASNIQCDETAIL,TBLASNIQCDETAILSN

                        if (iqc != null)
                        {
                            iqc.IqcType = IQCType.IQCType_SpotCheck;
                            iqc.Status = IQCStatus.IQCStatus_SQEJudge;
                            iqc.QcStatus = "N";

                            object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(_IQCNo);
                            if (objAsnIqcDetail != null)
                            {
                                foreach (AsnIQCDetail asnIQCDetail in objAsnIqcDetail)
                                {

                                    asnIQCDetail.QcStatus = "N";
                                    asnIQCDetail.QcPassQty = 0;
                                    _IQCFacade.UpdateAsnIQCDetail(asnIQCDetail);

                                }
                            }

                            object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(_IQCNo);
                            if (objAsnIqcDetailSN != null)
                            {
                                foreach (AsnIqcDetailSN sn in objAsnIqcDetailSN)
                                {
                                    sn.QcStatus = "N";
                                    _IQCFacade.UpdateAsnIqcDetailSN(sn);
                                    Asndetailsn asnDetailSn = (Asndetailsn)_InventoryFacade.GetAsndetailsn(sn.Sn, sn.StNo, Convert.ToInt32(sn.StLine));

                                    if (asnDetailSn != null)
                                    {
                                        asnDetailSn.QcStatus = "N";
                                        _InventoryFacade.UpdateAsndetailsn(asnDetailSn);
                                    }
                                }
                            }

                        }
                        #endregion
                    }
                    iqc.AQLLevel = drpAQLStandardQuery.SelectedValue;
                    _IQCFacade.UpdateAsnIQC(iqc);
                    ToSTS1(IqcNo);
                }

                ASN asn = (ASN)_InventoryFacade.GetASN(iqc.StNo);
                WarehouseFacade ware = new WarehouseFacade(DataProvider);
                SendMail mail = ShareLib.ShareKit.IQCExceptionThenGenerMail(iqc, asn, GetUserCode(), _IQCFacade, ware);
                if (mail != null)
                    ware.AddSendMail(mail);

                this.DataProvider.CommitTransaction();
                WebInfoPublish.PublishInfo(this, "提交成功", this.languageComponent1);

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, "提交失败：" + ex.Message, this.languageComponent1);

            }

        }

        //免检
        protected void cmdStatusSTS_ServerClick(object sender, EventArgs e)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            ArrayList array = this.gridHelper2.GetCheckedRows();
            if (array.Count > 0)
            {
                StringBuilder sbShowMsg = new StringBuilder();
                foreach (GridRecord row in array)
                {
                    string iqcNo = row.Items.FindItemByKey("IQCNo").Value.ToString();

                    AsnIQC iqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);


                    if (iqc.Status != "IQC" && iqc.Status != "Release" && iqc.Status != "WaitCheck")
                    {
                        //IQC检验单号: {0} 状态不是初始化
                        sbShowMsg.AppendFormat("IQC检验单号: {0} 状态不是检验中，不能免检 ", iqcNo);
                        continue;
                    }

                    //免检
                    try
                    {
                        this.DataProvider.BeginTransaction();
                        ToSTS(iqcNo);
                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {

                        sbShowMsg.AppendFormat("IQC检验单号: {0} {1}", iqcNo, ex.Message);
                        this.DataProvider.RollbackTransaction();
                        continue;
                    }

                }
                if (sbShowMsg.Length > 0)
                {
                    string showMsg = sbShowMsg.ToString();
                    WebInfoPublish.Publish(this, showMsg, this.languageComponent1);
                }
                else
                {
                    WebInfoPublish.Publish(this, "免检成功", this.languageComponent1);
                }
                this.gridHelper2.RequestData();//刷新页面
            }

        }

        //返回
        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FIQCCheckListQuery.aspx"));
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

                    invDoc.InvDocNo = _IQCNo;
                    invDoc.InvDocType = "IqcAbnormal";
                    invDoc.DocType = Path.GetExtension(postedFile.FileName);
                    invDoc.DocName = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    invDoc.DocSize = postedFile.ContentLength / 1024;
                    invDoc.UpUser = this.GetUserCode();
                    invDoc.UpfileDate = dbDateTime.DBDate;
                    invDoc.MaintainUser = this.GetUserCode();
                    invDoc.MaintainDate = dbDateTime.DBDate;
                    invDoc.MaintainTime = dbDateTime.DBTime;

                    string path = Server.MapPath(this.VirtualHostRoot + "InvDoc/" + "IQC/");
                    string fileName = string.Format("{0}_IqcAbnormal_{1}{2}{3}",
                        _IQCNo, dbDateTime.DBDate, dbDateTime.DBTime, invDoc.DocType);

                    invDoc.ServerFileName = fileName;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    this.FileImport.PostedFile.SaveAs(path + fileName);
                    invDoc.Dirname = "IQC";
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

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Query)
            {
                this.drpNGTypeEdit.Enabled = true;
                this.drpNGDescEdit.Enabled = true;

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
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }

            AsnIQCDetailEc asnIqcDetailEc = (AsnIQCDetailEc)this.ViewState["AsnIQCDetailEcEdit"];
            if (asnIqcDetailEc == null)
            {
                this.ViewState["IsAdd"] = true;
                asnIqcDetailEc = _IQCFacade.CreateNewAsnIQCDetailEc();
            }

            if ((bool)this.ViewState["IsAdd"])
            {
                asnIqcDetailEc.IqcNo = IqcNo;
                asnIqcDetailEc.StNo = StNo;
                asnIqcDetailEc.StLine = StLine;
                asnIqcDetailEc.CartonNo = FormatHelper.CleanString(this.txtCartonNoEdit.Text, 40);
                asnIqcDetailEc.EcgCode = FormatHelper.CleanString(this.drpNGTypeEdit.SelectedValue, 40);
                asnIqcDetailEc.ECode = FormatHelper.CleanString(this.drpNGDescEdit.SelectedValue, 40);
                if (string.IsNullOrEmpty(this.txtSNEdit.Text) && string.IsNullOrEmpty(this.txtNGQtyEdit.Text.Trim()))
                {
                    asnIqcDetailEc.NgFlag = "Y";
                }
                else
                {
                    asnIqcDetailEc.NgFlag = "N";
                }
                asnIqcDetailEc.CUser = this.GetUserName();
                asnIqcDetailEc.CDate = FormatHelper.GetNowDBDateTime(base.DataProvider).DBDate;
                asnIqcDetailEc.CTime = FormatHelper.GetNowDBDateTime(base.DataProvider).DBTime;

            }
            decimal sampleSize = 0;
            if (!string.IsNullOrEmpty(txtSamplesSize1.Text) && decimal.TryParse(txtSamplesSize1.Text, out sampleSize))
            {
                asnIqcDetailEc.SAMPLESIZE = (int)sampleSize;
            }
            asnIqcDetailEc.MaintainUser = this.GetUserName();
            asnIqcDetailEc.SN = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNEdit.Text, 40));
            asnIqcDetailEc.NgQty = string.IsNullOrEmpty(this.txtNGQtyEdit.Text.Trim()) ? 1 : Convert.ToInt32(this.txtNGQtyEdit.Text.Trim());
            asnIqcDetailEc.Remark1 = FormatHelper.CleanString(this.txtMemoEdit.Text, 200);
            this.ViewState["AsnIQCDetailEcEdit"] = null;
            return asnIqcDetailEc;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            string iqcNo = _IQCNo;
            string stNo = StNo;
            //string stLine = StLine;
            //string cartonNo = CartonNo;
            string stLine = row.Items.FindItemByKey("stline").Text.Trim();
            string cartonNo = row.Items.FindItemByKey("CartonNo").Text.Trim();
            string eCode = row.Items.FindItemByKey("ECode").Text.Trim();
            string egCode = row.Items.FindItemByKey("NGType1").Text.Trim();
            string sn = row.Items.FindItemByKey("SN").Text.Trim();
            object[] obj = _IQCFacade.GetAsnIQCDetailEc(egCode, eCode, stLine, iqcNo, stNo, sn);

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
                this.drpNGTypeEdit.SelectedValue = ((AsnIQCDetailEc)obj).EcgCode;
            }
            catch (Exception)
            {

                this.drpNGTypeEdit.SelectedIndex = 0; ;
            }
            try
            {
                this.drpNGDescEdit.SelectedValue = ((AsnIQCDetailEc)obj).ECode;
            }
            catch (Exception)
            {

                this.drpNGDescEdit.SelectedIndex = 0; ;
            }

            this.txtSNEdit.Text = ((AsnIQCDetailEc)obj).SN;
            this.txtNGQtyEdit.Text = ((AsnIQCDetailEc)obj).NgQty.ToString();
            this.txtCartonNoEdit.Text = ((AsnIQCDetailEc)obj).CartonNo;
            this.txtMemoEdit.Text = ((AsnIQCDetailEc)obj).Remark1;
            txtSamplesSize1.Text = ((AsnIQCDetailEc)obj).SAMPLESIZE.ToString();
            this.ViewState["IsAdd"] = false;
        }

        protected override bool ValidateInput()
        {

            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblNGTypeEdit, this.drpNGTypeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblNGDescEdit, this.drpNGDescEdit, 40, true));
            manager.Add(new LengthCheck(this.lblCartonNoEdit, this.txtCartonNoEdit, 40, true));
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
            if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
            {
                manager.Add(new LengthCheck(this.lblSNEdit, this.txtSNEdit, 40, true));
            }
            else
            {
                manager.Add(new LengthCheck(this.lblSNEdit, this.txtSNEdit, 40, false));
            }


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
                                ((AsnIQCDetailEc)obj).CartonNo,
                                ((AsnIQCDetailEc)obj).EcgCode,
                                ((AsnIQCDetailEc)obj).ECode,
                                ((AsnIQCDetailEc)obj).SN,
                                ((AsnIQCDetailEc)obj).NgQty.ToString(),
                                ((AsnIQCDetailEc)obj).Remark1
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"CartonNo",
                                    "NGType",
                                    "NGDesc",
	                                "SN",
                                    "NGQty",
                                    "Memo"};
        }

        protected override string[] FormatExportRecord2(object obj)
        {
            return new string[]{
                                ((AsnIQCDetailExt)obj).IqcNo,
                                ((AsnIQCDetailExt)obj).CartonNo,
                                ((AsnIQCDetailExt)obj).DQMCode,
                                ((AsnIQCDetailExt)obj).VendorMCode,
                                ((AsnIQCDetailExt)obj).MControlType,
                                this.GetStatusName(((AsnIQCDetailExt)obj).Status),
                                ((AsnIQCDetailExt)obj).IqcType,
                                ((AsnIQCDetailExt)obj).QcStatus,
                                ((AsnIQCDetailExt)obj).Qty.ToString(),
                                ((AsnIQCDetailExt)obj).NgQty.ToString(),
                                ((AsnIQCDetailExt)obj).ReturnQty.ToString(),
                                ((AsnIQCDetailExt)obj).ReformQty.ToString(),
                                ((AsnIQCDetailExt)obj).GiveQty.ToString(),
                                ((AsnIQCDetailExt)obj).AcceptQty.ToString(),
                                ((AsnIQCDetailExt)obj).Remark1
                               };
        }

        protected override string[] GetColumnHeaderText2()
        {
            return new string[] {	"IQCNo",
                                    "CartonNo",
                                    "DQMCode",
                                    "VMCode",
	                                "IQCWay",
                                    "Status",
                                    "IQCType",	
                                    "AQLResult",
                                    "AppQty",
                                    "NGQty",
                                    "ReturnQty",
                                    "ReformQty",
                                    "GiveQty",
                                    "AcceptQty",
                                    "Memo"};
        }

        #endregion

        #region Mothed

        //免检
        /// <summary>
        /// 免检
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param> 
        private void ToSTS(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);

            //1、更新送检单TBLASNIQC
            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                asnIqc.IqcType = IQCType.IQCType_ExemptCheck;//this.rblType.SelectedItem.Text;
                asnIqc.Status = IQCStatus.IQCStatus_IQCClose;
                asnIqc.QcStatus = "Y";
                _IQCFacade.UpdateAsnIQC(asnIqc);
                #region 在invinouttrans表中增加一条数据
                WarehouseFacade facade = new WarehouseFacade(base.DataProvider);

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
                trans.MaintainUser = this.GetUserCode();
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

            object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(iqcNo);
            if (objAsnIqcDetail != null)
            {
                foreach (AsnIQCDetail asnIqcDetail in objAsnIqcDetail)
                {
                    //2、更新送检单明细TBLASNIQCDETAIL
                    asnIqcDetail.QcPassQty = asnIqcDetail.Qty;
                    asnIqcDetail.QcStatus = "Y";
                    _IQCFacade.UpdateAsnIQCDetail(asnIqcDetail);


                    //4、更新ASN明细TBLASNDETAIL
                    ASNDetail asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(Convert.ToInt32(asnIqcDetail.StLine), asnIqcDetail.StNo);
                    if (asnDetail != null)
                    {
                        asnDetail.QcPassQty = asnDetail.ReceiveQty;
                        asnDetail.Status = IQCStatus.IQCStatus_IQCClose;
                        _InventoryFacade.UpdateASNDetail(asnDetail);
                    }

                    //5、更新ASN明细对应单据行明细TBLASNDETAILITEM
                    object[] objAsnDetaileItem = _InventoryFacade.GetAsnDetailItem(asnIqcDetail.StNo, Convert.ToInt32(asnIqcDetail.StLine));
                    if (objAsnDetaileItem != null)
                    {
                        foreach (Asndetailitem asnDetaileItem in objAsnDetaileItem)
                        {
                            asnDetaileItem.QcpassQty = asnDetaileItem.ReceiveQty;
                            asnDetaileItem.ActQty = asnDetaileItem.QcpassQty;
                            _InventoryFacade.UpdateAsndetailitem(asnDetaileItem);
                        }
                    }

                }
            }

            object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(iqcNo);
            if (objAsnIqcDetailSN != null)
            {
                foreach (AsnIqcDetailSN asnIqcDetailSN in objAsnIqcDetailSN)
                {
                    //3、更新送检单明细SNTBLASNIQCDETAILSN
                    asnIqcDetailSN.QcStatus = "Y";
                    _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN);

                    //6、更新ASN明细SN TBLASNDETAILSN
                    Asndetailsn asnDetailSn = (Asndetailsn)_InventoryFacade.GetAsndetailsn(asnIqcDetailSN.Sn, asnIqcDetailSN.StNo, Convert.ToInt32(asnIqcDetailSN.StLine));
                    if (asnDetailSn != null)
                    {
                        asnDetailSn.QcStatus = "Y";
                        _InventoryFacade.UpdateAsndetailsn(asnDetailSn);
                    }
                }
            }

            if (_IQCFacade.CanToOnlocationStaus(asnIqc.StNo))
            {

                ASN asn = (ASN)_InventoryFacade.GetASN(asnIqc.StNo);
                asn.Status = ASNHeadStatus.OnLocation;
                _InventoryFacade.UpdateASN(asn);
            }
        }


        private void ToSTS1(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);

            //1、更新送检单TBLASNIQC
            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            Log.Error("----------------------------------------------------------------------------------------------");
            if (asnIqc != null)
            {

                #region 在invinouttrans表中增加一条数据
                WarehouseFacade facade = new WarehouseFacade(base.DataProvider);

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
                trans.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                trans.MaintainUser = this.GetUserCode();
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

        }

        //检查ASN明细所有行状态为IQCClose
        /// <summary>
        /// 检查ASN明细所有行状态为IQCClose
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是IQCClose：true;否则：false</returns>
        private bool CheckAllASNDetailIsIQCClose(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.IQCClose)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //检查ASN明细所有行状态为OnLocation
        /// <summary>
        /// 检查ASN明细所有行状态为OnLocation
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是OnLocation：true;否则：false</returns>
        private bool CheckAllASNDetailIsOnLocation(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.OnLocation)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //检查ASN明细所有行状态为Close
        /// <summary>
        /// 检查ASN明细所有行状态为Close
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是Close：true;否则：false</returns>
        private bool CheckAllASNDetailIsClose(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.Close)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //检查ASN明细所有行状态为Cancel
        /// <summary>
        /// 检查ASN明细所有行状态为Cancel
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是Cancel：true;否则：false</returns>
        private bool CheckAllASNDetailIsCancel(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(base.DataProvider);
            }
            _InventoryFacade = new InventoryFacade(base.DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.Cancel)
                        {
                            return false;
                        }
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
        #endregion




    }
}
