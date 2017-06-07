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
    public partial class FRecordOQCSumMP : BaseMPageNew
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

            this.gridHelper.AddColumn("CreateOQCQty", "创建OQC个数", null);
            this.gridHelper.AddColumn("QCCheckQty", "QC检验个数", null);
            this.gridHelper.AddColumn("SQEQty", "SQE判定个数", null);

            this.gridHelper.AddColumn("AvgOQCheckDate", "平均QC检验时间", null);
            this.gridHelper.AddColumn("AvgSQEDate", "平均SQE判定时间", null);
            this.gridHelper.AddColumn("AvgOQCDate", "平均OQC执行时间", null);

            this.gridHelper.AddColumn("SumINQTY", "来料总数量", null);
            this.gridHelper.AddColumn("SumSampleSize", "样本总数量", null);
            this.gridHelper.AddColumn("SumNGQty", "缺陷品总数", null);
            this.gridHelper.AddColumn("SumReturnQty", "退换货总数量", null);
            this.gridHelper.AddColumn("SumGiveQTY", "让步放行总数量", null);//

            Dictionary<string, string> eCodelist = GetECodelist();
            if (eCodelist != null)
            {
                foreach (string key in eCodelist.Keys)
                {
                    this.gridHelper.AddColumn(key, eCodelist[key], null);
                }
            }
       

            this.gridHelper.AddDefaultColumn(false, false);



            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            InventoryFacade facade =new InventoryFacade(this.DataProvider);
            OQCQuery oqcQuery = obj as OQCQuery;

            row["CreateOQCQty"] = oqcQuery.OQCQty;

            string outType = FormatHelper.CleanString(this.drpStorageOutTypeQurey.SelectedValue);
            string storageCode = FormatHelper.CleanString(this.drpStorageQuery.SelectedValue);
            int dateFrom = FormatHelper.TODateInt(this.dateInDateFromQuery.Text);
            int dateTo = FormatHelper.TODateInt(this.dateInDateToQuery.Text);
            int QCCheckQty = facade.QueryTransQty(outType, storageCode, dateFrom, dateTo, "OQC");
            int SQEQty = facade.QueryTransQty(outType, storageCode, dateFrom, dateTo, "OQCSQE");
            row["QCCheckQty"] = QCCheckQty;
            row["SQEQty"] = SQEQty;


            object[] oqcQueryList= facade.QueryRecordOQC(
                FormatHelper.CleanString(this.drpStorageOutTypeQurey.SelectedValue),
                FormatHelper.CleanString(this.drpStorageQuery.SelectedValue),
                FormatHelper.TODateInt(this.dateInDateFromQuery.Text),
                FormatHelper.TODateInt(this.dateInDateToQuery.Text),
                0, int.MaxValue);

            #region oqcQueryList
            decimal sumOCCheckDate = 0;
            int sumOCCheck = 0;
            decimal sumSQEexecuteDate = 0;
            int sumSQEexecute = 0;
            foreach (OQCQuery oqcObj in oqcQueryList)
            {
                int ocCheckFinishDate = facade.MaxOutDate(oqcObj.OqcNo, oqcObj.MCode, "OQC");
                int ocCheckFinishTime = facade.MaxOutTime(oqcObj.OqcNo, oqcObj.MCode, "OQC");
                int sqeFinishDate = facade.MaxOutDate(oqcObj.OqcNo, oqcObj.MCode, "OQCSQE");
                int sqeFinishTime = facade.MaxOutTime(oqcObj.OqcNo, oqcObj.MCode, "OQCSQE");
                decimal OCCheckDate = 0;
                if (ocCheckFinishDate != 0)
                {
                    OCCheckDate = Common.Totalday(ocCheckFinishDate, ocCheckFinishTime, oqcObj.CDate, oqcObj.CTime);
                    sumOCCheckDate += OCCheckDate;
                    sumOCCheck++;
                }
                decimal SQEexecuteDate = 0;
                if (SQEexecuteDate != 0)
                {
                    SQEexecuteDate = Common.Totalday(sqeFinishDate, sqeFinishTime, ocCheckFinishDate, ocCheckFinishTime);
                    sumSQEexecuteDate += SQEexecuteDate;
                    sumSQEexecute++;
                }
            } 
            #endregion

            decimal sumOC = 0;
            if (sumOCCheck!=0)
            { sumOC=(sumOCCheckDate / sumOCCheck); }
           decimal sumSQE= 0;
           if (sumSQEexecute != 0)
            {
                sumSQE =  (sumSQEexecuteDate/sumSQEexecute);
            }
            row["AvgOQCheckDate"] = sumOC.ToString("0.0");
            row["AvgSQEDate"] = sumSQE.ToString("0.0");
            row["AvgOQCDate"] = (sumOC + sumSQE).ToString("0.0");
 

            row["SumINQTY"] = oqcQuery.Qty;
            row["SumSampleSize"] = oqcQuery.SampleSize;
            row["SumNGQty"] = oqcQuery.NgQty;
            row["SumReturnQty"] = oqcQuery.ReturnQty;
            row["SumGiveQTY"] = oqcQuery.GiveQty;


            Dictionary<string, string> eCodelist = GetECodelist();
            if (eCodelist != null)
            {
                foreach (string key in eCodelist.Keys)
                {
                    row[key] = facade.GetSumNgQty(outType, storageCode, dateFrom, dateTo, key);
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

            return this._InventoryFacade.QuerySumRecordOQC(
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
            return this._InventoryFacade.QuerySumRecordOQCCount(
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
            int oqcCloseTime= 0;
            int OQCFinishDate = 0;
                 if (oqcQuery.Status == "OQCClose")
            {
                oqcCloseDate = oqcQuery.MaintainDate;
                oqcCloseTime = oqcQuery.MaintainTime;
                      OQCFinishDate=oqcQuery.MaintainDate;//有SQE环节的，等于SQE判定完成时间；无SQE环节的，等于QC检验完成时间
            }
            decimal OCCheckDate = 0;
            if (oqcCloseDate != 0)
            {
                  OCCheckDate = Common.Totalday(ocCheckFinishDate,ocCheckFinishTime,oqcQuery.CDate, oqcQuery.CTime);
            }
            decimal SQEexecuteDate = 0;
            if (SQEexecuteDate != 0)
            {
              SQEexecuteDate= Common.Totalday(sqeFinishDate,sqeFinishTime, ocCheckFinishDate,ocCheckFinishTime);
            }
            decimal OQCEexecuteDate = 0;
            if (oqcCloseDate != 0)
            {
                OQCEexecuteDate = Common.Totalday(oqcCloseDate,oqcCloseTime,oqcQuery.CDate, oqcQuery.CTime);
            }
            return new string[]{
                    oqcQuery.OqcNo,
                    oqcQuery.InvNo,
                          oqcQuery.PickType,
                     oqcQuery.StorageCode,
                        oqcQuery.GfHwItemCode,
                   oqcQuery.DQMCode,
                       oqcQuery.VendorMCode,
                            oqcQuery.Mdesc,
        
                   oqcQuery.OqcType,//检验方式
                    oqcQuery.AQL,
        
                 oqcQuery.Qty.ToString(),//来料数量
                      oqcQuery.SampleSize.ToString(),//样本数量
                 oqcQuery.NgQty.ToString(),
                     oqcQuery.ReturnQty.ToString(),
                   oqcQuery.GiveQty.ToString(),
             FormatHelper.ToDateString(oqcQuery.CDate),
                    FormatHelper.ToDateString(ocCheckFinishDate), // QC检验完成时间
                FormatHelper.ToDateString(sqeFinishDate),//SQE判定完成时间
               FormatHelper.ToDateString(OQCFinishDate),
            OCCheckDate.ToString("0.0"), //QC检验完成时间-创
               SQEexecuteDate.ToString("0.0"),//SQE判定完成
                OQCEexecuteDate.ToString("0.0")//OQC完成时

            };
        }

       

        protected override string[] GetColumnHeaderText()
        {
            string eCode = "";
            Dictionary<string, string> eCodelist = GetECodelist();
            if (eCodelist != null)
            {
                foreach (string key in eCodelist.Keys)
                {
                    eCode += key + ",";
                }
            }
            eCode = eCode.TrimEnd(',');
            return new string[] {	 "OQCSerial",               
            "SAPInvNo",          
            "StorageOutType",    
            "StorageNo",         
            "GFHWItemCode",      
            "DQMCode",           
            "VendorMCODE",       
            "DQMaterialNoDesc",  
            "OQCType",           
            "AQLLevel",          
            "INQTY",             
            "SampleSize",        
            "NGQty",             
            "ReturnQty",         
            "GiveQTY"   ,
              "Ctime",               
            "OCCheckFinishDate",   
            "SQEFinishDate",       
            "OQCFinishDate",       
            "OCCheckDate",         
            "SQEexecuteDate",      
            "OQCEexecuteDate", eCode};
        }

        #endregion

    }
}
