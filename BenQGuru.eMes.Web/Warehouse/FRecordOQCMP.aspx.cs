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
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.TSModel;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FRecordOQCMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private InventoryFacade _InventoryFacade = null;
        private SystemSettingFacade _SystemSettingFacade = null;


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
            _SystemSettingFacade = new SystemSettingFacade(this.DataProvider);

            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                InitStorageList();
                DrpPickTypeList();
            }
        }

        #region  单据类型下拉框

        private Dictionary<string, string> GetECodelist()
        {
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            Dictionary<string, string> ECodelist = new Dictionary<string, string>();
            object[] objs = _SystemSettingFacade.GetErrorGroupcode();
            if (objs != null)
            {
                foreach (ErrorCodeGroupA ecg in objs)
                {
                    ECodelist.Add(ecg.ErrorCodeGroup, ecg.ErrorCodeGroupDescription);
                }
            }
            return ECodelist;
        }

        private void InitStorageList()
        {

            InventoryFacade facade = new InventoryFacade(base.DataProvider);

            this.drpStorageQuery.Items.Add(new ListItem("", ""));
            object[] objStorage = facade.GetAllStorage();
            if (objStorage != null && objStorage.Length > 0)
            {
                foreach (Storage storage in objStorage)
                {

                    this.drpStorageQuery.Items.Add(new ListItem(
                         storage.StorageName + "-" + storage.StorageCode, storage.StorageCode)
                        );
                }
            }
            this.drpStorageQuery.SelectedIndex = 0;
        }

        private void DrpPickTypeList()
        {
            if (_SystemSettingFacade == null)
            {
                _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            }
            object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("PICKTYPE");
            this.drpStorageOutTypeQurey.Items.Add(new ListItem("", ""));
            foreach (Domain.BaseSetting.Parameter parameter in parameters)
            {
                this.drpStorageOutTypeQurey.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
            }
            this.drpStorageOutTypeQurey.SelectedIndex = 0;
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
            this.gridHelper.AddColumn("OQCSerial", "OQC单号", null);//
            this.gridHelper.AddColumn("SAPInvNo", "SAP单据号", null);
            this.gridHelper.AddColumn("StorageOutType", "出库类型", null);
            this.gridHelper.AddColumn("StorageNo", "库位", null);
            this.gridHelper.AddColumn("GFHWItemCode", "光伏华为编码", null);
            this.gridHelper.AddColumn("DQMCode", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("VendorMCODE", "供应商物料编码", null);
            this.gridHelper.AddColumn("DQMaterialNoDesc", "鼎桥物料编码描述", null);
            this.gridHelper.AddColumn("OQCType", "检验方式", null);
            this.gridHelper.AddColumn("AQLLevel", "AQL标准", null);
            this.gridHelper.AddColumn("INQTY", "来料数量", null);
            this.gridHelper.AddColumn("SampleSize", "样本数量", null);
            this.gridHelper.AddColumn("NGQty", "缺陷品数", null);
            this.gridHelper.AddColumn("ReturnQty", "退换货数量", null);
            this.gridHelper.AddColumn("GiveQTY", "让步放行数量", null);//

            Dictionary<string, string> eCodelist = GetECodelist();
            if (eCodelist != null)
            {
                foreach (string key in eCodelist.Keys)
                {
                    this.gridHelper.AddColumn(key, eCodelist[key], null);
                }
            }
            this.gridHelper.AddColumn("Ctime", "创建时间", null);
            this.gridHelper.AddColumn("OCCheckFinishDate", "QC检验完成时间", null);
            this.gridHelper.AddColumn("SQEFinishDate", "SQE判定完成时间", null);
            this.gridHelper.AddColumn("OQCFinishDate", "OQC完成时间", null);
            this.gridHelper.AddColumn("OCCheckDate", "QC检验执行时间", null);
            this.gridHelper.AddColumn("SQEexecuteDate", "SQE判定执行时间", null);
            this.gridHelper.AddColumn("OQCEexecuteDate", "OQC执行时间", null);


            this.gridHelper.AddDefaultColumn(false, false);



            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            InventoryFacade facade = new InventoryFacade(this.DataProvider);
            OQCQuery oqcQuery = obj as OQCQuery;

            row["OQCSerial"] = oqcQuery.OqcNo;
            row["SAPInvNo"] = oqcQuery.InvNo;
            row["StorageOutType"] = oqcQuery.PickType;
            row["StorageNo"] = oqcQuery.StorageCode;
            row["GFHWItemCode"] = oqcQuery.GfHwItemCode;
            row["DQMCode"] = oqcQuery.DQMCode;
            row["VendorMCODE"] = oqcQuery.VendorMCode;
            row["DQMaterialNoDesc"] = oqcQuery.Mdesc;

            row["OQCType"] = oqcQuery.OqcType;//检验方式
            row["AQLLevel"] = oqcQuery.AQL;

            row["INQTY"] = oqcQuery.Qty;//来料数量
            row["SampleSize"] = oqcQuery.SampleSize;//样本数量
            row["NGQty"] = oqcQuery.NgQty;
            row["ReturnQty"] = oqcQuery.ReturnQty;
            row["GiveQTY"] = oqcQuery.GiveQty;


            row["Ctime"] = oqcQuery.CDate;
            int ocCheckFinishDate = facade.MaxOutDate(oqcQuery.OqcNo, oqcQuery.MCode, "OQC");
            int ocCheckFinishTime = facade.MaxOutTime(oqcQuery.OqcNo, oqcQuery.MCode, "OQC");
            int sqeFinishDate = facade.MaxOutDate(oqcQuery.OqcNo, oqcQuery.MCode, "OQCSQE");
            int sqeFinishTime = facade.MaxOutTime(oqcQuery.OqcNo, oqcQuery.MCode, "OQCSQE");
            row["OCCheckFinishDate"] = ocCheckFinishDate; // QC检验完成时间
            row["SQEFinishDate"] = sqeFinishDate; //SQE判定完成时间
            int oqcCloseDate = 0;
            int oqcCloseTime = 0;
            if (oqcQuery.Status == "OQCClose")
            {
                oqcCloseDate = oqcQuery.MaintainDate;
                oqcCloseTime = oqcQuery.MaintainTime;
                row["OQCFinishDate"] = oqcQuery.MaintainDate;//有SQE环节的，等于SQE判定完成时间；无SQE环节的，等于QC检验完成时间
            }
            decimal OCCheckDate = 0;
            if (ocCheckFinishDate != 0)
            {
                OCCheckDate = Common.Totalday(ocCheckFinishDate, ocCheckFinishTime, oqcQuery.CDate, oqcQuery.CTime);
            }
            decimal SQEexecuteDate = 0;
            if (SQEexecuteDate != 0)
            {
                SQEexecuteDate = Common.Totalday(sqeFinishDate, sqeFinishTime, ocCheckFinishDate, ocCheckFinishTime);
            }
            decimal OQCEexecuteDate = 0;
            if (oqcCloseDate != 0)
            {
                OQCEexecuteDate = Common.Totalday(oqcCloseDate, oqcCloseTime, oqcQuery.CDate, oqcQuery.CTime);
            }
            row["OCCheckDate"] = OCCheckDate.ToString("0.0"); //QC检验完成时间-创建时间，每天8小时转换成天，未完成检验为空
            row["SQEexecuteDate"] = SQEexecuteDate.ToString("0.0");//SQE判定完成时间-QC检验完成时间，每天按8小时转换成天，未完成SQE判定或无SQE环节则为空
            row["OQCEexecuteDate"] = OQCEexecuteDate.ToString("0.0");//OQC完成时间-创建时间，每天按8小时转换成天，OQC未完成为空


            Dictionary<string, string> eCodelist = GetECodelist();
            if (eCodelist != null)
            {
                foreach (string key in eCodelist.Keys)
                {
                    row[key] = facade.GetNgQty(oqcQuery.OqcNo, oqcQuery.MCode, key);
                }
            }
            return row;

        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }

            return this._InventoryFacade.QueryRecordOQC(
                   FormatHelper.CleanString(this.drpStorageOutTypeQurey.SelectedValue),
                FormatHelper.CleanString(this.drpStorageQuery.SelectedValue),
                       FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                    FormatHelper.TODateInt(this.dateInDateToQuery.Text),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            return this._InventoryFacade.QueryRecordOQCCount(
                FormatHelper.CleanString(this.drpStorageOutTypeQurey.Text),
                FormatHelper.CleanString(this.drpStorageQuery.SelectedValue),
                       FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                    FormatHelper.TODateInt(this.dateInDateToQuery.Text));
        }

        #endregion

        #region Button


        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            InventoryFacade facade = new InventoryFacade(this.DataProvider);
            OQCQuery oqcQuery = obj as OQCQuery;
            if (oqcQuery == null)
            {
                return null;
            }

            int ocCheckFinishDate = facade.MaxOutDate(oqcQuery.PickNo, oqcQuery.MCode, "OQC");
            int ocCheckFinishTime = facade.MaxOutTime(oqcQuery.PickNo, oqcQuery.MCode, "OQC");
            int sqeFinishDate = facade.MaxOutDate(oqcQuery.PickNo, oqcQuery.MCode, "OQCSQE");
            int sqeFinishTime = facade.MaxOutTime(oqcQuery.PickNo, oqcQuery.MCode, "OQCSQE");
            int oqcCloseDate = 0;
            int oqcCloseTime = 0;
            int OQCFinishDate = 0;
            if (oqcQuery.Status == "OQCClose")
            {
                oqcCloseDate = oqcQuery.MaintainDate;
                oqcCloseTime = oqcQuery.MaintainTime;
                OQCFinishDate = oqcQuery.MaintainDate;//有SQE环节的，等于SQE判定完成时间；无SQE环节的，等于QC检验完成时间
            }
            decimal OCCheckDate = 0;
            if (oqcCloseDate != 0)
            {
                OCCheckDate = Common.Totalday(ocCheckFinishDate, ocCheckFinishTime, oqcQuery.CDate, oqcQuery.CTime);
            }
            decimal SQEexecuteDate = 0;
            if (SQEexecuteDate != 0)
            {
                SQEexecuteDate = Common.Totalday(sqeFinishDate, sqeFinishTime, ocCheckFinishDate, ocCheckFinishTime);
            }
            decimal OQCEexecuteDate = 0;
            if (oqcCloseDate != 0)
            {
                OQCEexecuteDate = Common.Totalday(oqcCloseDate, oqcCloseTime, oqcQuery.CDate, oqcQuery.CTime);
            }
            List<string> list = new List<string>();

            list.Add(oqcQuery.OqcNo);
            list.Add(oqcQuery.InvNo);
            list.Add(oqcQuery.PickType);
            list.Add(oqcQuery.StorageCode);
            list.Add(oqcQuery.GfHwItemCode);
            list.Add(oqcQuery.DQMCode);
            list.Add(oqcQuery.VendorMCode);
            list.Add(oqcQuery.Mdesc);

            list.Add(oqcQuery.OqcType);
            list.Add(oqcQuery.AQL);

            list.Add(oqcQuery.Qty.ToString());//来料数量
            list.Add(oqcQuery.SampleSize.ToString());//样本数量
            list.Add(oqcQuery.NgQty.ToString());
            list.Add(oqcQuery.ReturnQty.ToString());
            list.Add(oqcQuery.GiveQty.ToString());
            Dictionary<string, string> eCodelist = GetECodelist();
            if (eCodelist != null)
            {
                foreach (string key in eCodelist.Keys)
                {
                    list.Add(facade.GetNgQty(oqcQuery.OqcNo, oqcQuery.MCode, key).ToString());
                }
            }

            list.Add(FormatHelper.ToDateString(oqcQuery.CDate));
            list.Add(FormatHelper.ToDateString(ocCheckFinishDate)); // QC检验完成时间
            list.Add(FormatHelper.ToDateString(sqeFinishDate));//SQE判定完成时间
            list.Add(FormatHelper.ToDateString(OQCFinishDate));
            list.Add(OCCheckDate.ToString("0.0")); //QC检验完成时间-创
            list.Add(SQEexecuteDate.ToString("0.0"));//SQE判定完成
            list.Add(OQCEexecuteDate.ToString("0.0"));//OQC完成时



           
            return list.ToArray();
        }



        protected override string[] GetColumnHeaderText()
        {
          

            List<string> list = new List<string>();
            list.Add("OQCSerial");
            list.Add("SAPInvNo");
            list.Add("StorageOutType");
            list.Add("StorageNo");
            list.Add("GFHWItemCode");
            list.Add("DQMCode");
            list.Add("VendorMCODE");
            list.Add("DQMaterialNoDesc");
            list.Add("OQCType");
            list.Add("AQLLevel");
            list.Add("INQTY");
            list.Add("SampleSize");
            list.Add("NGQty");
            list.Add("ReturnQty");
            list.Add("GiveQTY");
            Dictionary<string, string> eCodelist = GetECodelist();
            if (eCodelist != null)
            {
                foreach (string key in eCodelist.Keys)
                {
                    list.Add(key);
                }
            }
            list.Add("Ctime");
            list.Add("OCCheckFinishDate");
            list.Add("SQEFinishDate");
            list.Add("OQCFinishDate");
            list.Add("OCCheckDate");
            list.Add("SQEexecuteDate");
            list.Add("OQCEexecuteDate");
         
            return list.ToArray();
        }

        #endregion

    }
}
