using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Material;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.TSModel;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FoutStorageDetailRecord : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;


        private WarehouseFacade facade = null;
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

            InitHander();
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                InitWebGrid();



                SystemSettingFacade _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);

                object[] parameters = _SystemSettingFacade.GetParametersByParameterGroup("PICKTYPE");
                this.drpPickTypeQuery.Items.Add(new ListItem("", ""));
                foreach (Domain.BaseSetting.Parameter parameter in parameters)
                {
                    this.drpPickTypeQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
                }
                this.drpPickTypeQuery.SelectedIndex = 0;
            }

        }


        #region 默认查询
        private void RequestData()
        {

            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);

            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }



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
        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
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

            this.gridHelper.AddColumn("PICKNO", "拣货任务令号", null);
            this.gridHelper.AddColumn("INVNO", "SAP单据号", null);
            this.gridHelper.AddColumn("PICKTYPE", "出库类型", null);
            this.gridHelper.AddColumn("StorageCode123", "出库库位", null);
            this.gridHelper.AddColumn("CDATE", "创建时间", null);
            this.gridHelper.AddColumn("OUTSTORAGEPERIOD", "出库执行周期", null);
            this.gridHelper.AddColumn("WAREPERIOD", "库房操作周期", null);
            this.gridHelper.AddColumn("DOWNDATE", "下发时间", null);
            this.gridHelper.AddColumn("DOWNPERIOD", "下发执行周期", null);
            this.gridHelper.AddColumn("PICKBEGINTIME", "拣料开始时间", null);
            this.gridHelper.AddColumn("PICKENDTIME", "拣料完成时间", null);
            this.gridHelper.AddColumn("PICKPERIOD", "拣料执行周期", null);
            this.gridHelper.AddColumn("PACKBEGINTIME", "包装开始时间", null);
            this.gridHelper.AddColumn("PACKENDTIME", "包装完成时间", null);
            this.gridHelper.AddColumn("PACKPERIOD", "包装执行周期", null);
            this.gridHelper.AddColumn("OQCBEGINTIME", "OQC开始时间", null);
            this.gridHelper.AddColumn("OQCENDTIME", "OQC完成时间", null);
            this.gridHelper.AddColumn("OQCPERIOD", "OQC执行周期", null);
            this.gridHelper.AddColumn("CARINVNOBEGINTIME", "箱单开始时间", null);
            this.gridHelper.AddColumn("CARINVNOENDTIME", "箱单完成时间", null);
            this.gridHelper.AddColumn("CARINVNOPERIOD", "制作箱单周期", null);
            this.gridHelper.AddColumn("OUTSTORAGETIME", "发货时间", null);
            this.gridHelper.AddColumn("OUTSTORAGEPERIOD1", "发货执行周期", null);
            this.gridHelper.AddColumn("CARTONNOS", "箱数", null);
            this.gridHelper.AddColumn("WEIGHT", "重量", null);
            this.gridHelper.AddColumn("VOLUME", "体积", null);
            this.gridHelper.AddColumn("ORDERLINES", "Orderline行数", null);



            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);



        }

        protected override DataRow GetGridRow(object obj)
        {

            DataRow row = this.DtSource.NewRow();
            OutStorageDetail s = (OutStorageDetail)obj;

            WarehouseFacade _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);

            row["PICKNO"] = s.PICKNO;
            row["INVNO"] = s.INVNO;
            row["PICKTYPE"] = GetPickTypeName(s.PICKTYPE);
            row["StorageCode123"] = s.STORAGECODE;
            row["CDATE"] = FormatHelper.TODateTimeString(s.CDATE, s.CTIME);


            decimal periodOutDec = _WarehouseFacade.Totalday(s.Delivery_Date, s.Delivery_Time, s.CDATE, s.CTIME);
            string outStoragePeriod = string.Empty;
            if (periodOutDec >= 0)
                outStoragePeriod = periodOutDec.ToString();
            row["OUTSTORAGEPERIOD"] = outStoragePeriod;

            DateWithTime pickBeginTime = _WarehouseFacade.StrToDateWithTime(s.MINPICK, '-');
            decimal warePeriod = _WarehouseFacade.Totalday(s.Delivery_Date, s.Delivery_Time, pickBeginTime.Date, pickBeginTime.Time);
            string warePeriodStr = string.Empty;
            if (warePeriod >= 0)
                warePeriodStr = warePeriod.ToString();
            row["WAREPERIOD"] = warePeriodStr;


            row["DOWNDATE"] = FormatHelper.TODateTimeString(s.Down_Date, s.Down_Time);

            string downPeriod = string.Empty;
            decimal downPeriodDec = _WarehouseFacade.Totalday(s.Down_Date, s.Down_Time, s.CDATE, s.CTIME);
            if (downPeriodDec >= 0)
                downPeriod = downPeriodDec.ToString();
            row["DOWNPERIOD"] = downPeriod;


            row["PICKBEGINTIME"] = FormatHelper.TODateTimeString(pickBeginTime.Date, pickBeginTime.Time);


            DateWithTime pickEndTime = _WarehouseFacade.StrToDateWithTime(s.MAXPICK, '-');
            row["PICKENDTIME"] = FormatHelper.TODateTimeString(pickEndTime.Date, pickEndTime.Time);


            decimal pickPeriod = _WarehouseFacade.Totalday(pickEndTime.Date, pickEndTime.Time, pickBeginTime.Date, pickBeginTime.Time);
            string pickPeriodStr = string.Empty;
            if (pickPeriod >= 0)
                pickPeriodStr = pickPeriod.ToString();
            row["PICKPERIOD"] = pickPeriodStr;



            //DateWithTime packBeginTime = _WarehouseFacade.StrToDateWithTime(s.MINPACK, '-');

            DateWithTime packBeginTime = pickEndTime;
            DateWithTime packEndTime = _WarehouseFacade.StrToDateWithTime(s.MAXPACK, '-');
            row["PACKBEGINTIME"] = FormatHelper.TODateTimeString(packBeginTime.Date, packBeginTime.Time);
            row["PACKENDTIME"] = FormatHelper.TODateTimeString(packEndTime.Date, packEndTime.Time);


            decimal packPeriod = _WarehouseFacade.Totalday(packEndTime.Date, packEndTime.Time, packBeginTime.Date, packBeginTime.Time);
            string packPeriodStr = string.Empty;
            if (packPeriod >= 0)
                packPeriodStr = packPeriod.ToString();
            row["PACKPERIOD"] = packPeriodStr;



            //DateWithTime oqcBeginTime = _WarehouseFacade.StrToDateWithTime(s.OQCCDATE, '-');
            DateWithTime oqcBeginTime = packEndTime;
            row["OQCBEGINTIME"] = FormatHelper.TODateTimeString(oqcBeginTime.Date, oqcBeginTime.Time);

            DateWithTime oqcEndTime = _WarehouseFacade.StrToDateWithTime(s.OQCDATE, '-');
            row["OQCENDTIME"] = FormatHelper.TODateTimeString(oqcEndTime.Date, oqcEndTime.Time);


            decimal oqcPeriod = _WarehouseFacade.Totalday(oqcEndTime.Date, oqcEndTime.Time, oqcBeginTime.Date, oqcBeginTime.Time);
            string oqcPeriodStr = string.Empty;
            if (oqcPeriod >= 0)
                oqcPeriodStr = oqcPeriod.ToString();
            row["OQCPERIOD"] = oqcPeriodStr;



            //DateWithTime cartonnoBeginTime = _WarehouseFacade.StrToDateWithTime(s.CDATETIME, '-');

            DateWithTime cartonnoBeginTime = oqcEndTime; 
            row["CARINVNOBEGINTIME"] = FormatHelper.TODateTimeString(cartonnoBeginTime.Date, cartonnoBeginTime.Time);


            row["CARINVNOENDTIME"] = FormatHelper.TODateTimeString(s.Packing_List_Date, s.Packing_List_Time);



            decimal carinvnoPeriod = _WarehouseFacade.Totalday(s.Packing_List_Date, s.Packing_List_Time, cartonnoBeginTime.Date, cartonnoBeginTime.Time);
            string carinvnoPeriodStr = string.Empty;
            if (carinvnoPeriod >= 0)
                carinvnoPeriodStr = carinvnoPeriod.ToString();
            row["CARINVNOPERIOD"] = carinvnoPeriodStr;



            row["OUTSTORAGETIME"] = FormatHelper.TODateTimeString(s.Delivery_Date, s.Delivery_Time);


            decimal outStoragePeriod1 = _WarehouseFacade.Totalday(s.Delivery_Date, s.Delivery_Time, s.Packing_List_Date, s.Packing_List_Time);
            string outStoragePeriod1Str = string.Empty;
            if (outStoragePeriod1 >= 0)
                outStoragePeriod1Str = outStoragePeriod1.ToString();
            row["OUTSTORAGEPERIOD1"] = outStoragePeriod1Str;




            row["CARTONNOS"] = s.CARTONNOS;

            row["WEIGHT"] = s.GROSS_WEIGHT;
            row["VOLUME"] = s.VOLUMN;
            row["ORDERLINES"] = _WarehouseFacade.QueryOrderLines(s.PICKNO);


            return row;
        }



        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryOutStorageDetails(this.txtStorageCodeQuery.Text,
                                             this.drpPickTypeQuery.SelectedValue,
                                         FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text), inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryOutStorageDetailsCount(this.txtStorageCodeQuery.Text,
                                             this.drpPickTypeQuery.SelectedValue,
                                         FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
        }

        #endregion

        #region Button
        //protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        //{
        //    if (commandName == "Edit")
        //    {
        //        object obj = this.GetEditObject(row);
        //        if (obj != null)
        //        {
        //            this.SetEditObject(obj);
        //            this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
        //        }
        //    }
        //    else if (commandName == "LinkDetail")
        //    {
        //        string pickNo = row.Items.FindItemByKey("PickNo").Text.Trim();
        //        //string storageOut = row.Items.FindItemByKey("StorageOut").Text.Trim();
        //        Response.Redirect(this.MakeRedirectUrl("FCreatePickLineMP.aspx", new string[] { "ACT", "PickNo" }, new string[] { "LinkDetail", pickNo }));
        //    }
        //}



        protected override void cmdExport_Click(object sender, EventArgs e)
        {
            BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(base.DataProvider);

            List<string> ca = new List<string>();

            XlsPackage xls = new XlsPackage();
            IFont font = xls.Black;
            font.FontHeightInPoints = 10;
            ICellStyle style = xls.Normal;
            style.SetFont(font);


            xls.CreateSheet("出库明细表");
            xls.NewRow(0);



            xls.Cell(0, "拣货任务令号", style);
            xls.Cell(1, "SAP单据号", style);
            xls.Cell(2, "出库类型", style);
            xls.Cell(3, "库位", style);
            xls.Cell(4, "创建时间", style);
            xls.Cell(5, "出库执行周期", style);
            xls.Cell(6, "库房操作周期", style);
            xls.Cell(7, "下发时间", style);
            xls.Cell(8, "下发执行周期", style);
            xls.Cell(9, "拣料开始时间", style);
            xls.Cell(10, "拣料完成时间", style);
            xls.Cell(11, "拣料执行周期", style);
            xls.Cell(12, "包装开始时间", style);
            xls.Cell(13, "包装完成时间", style);
            xls.Cell(14, "包装执行周期", style);
            xls.Cell(15, "OQC开始时间", style);
            xls.Cell(16, "OQC完成时间", style);
            xls.Cell(17, "OQC执行周期", style);
            xls.Cell(18, "箱单开始时间", style);
            xls.Cell(19, "箱单完成时间", style);
            xls.Cell(20, "制作箱单周期", style);
            xls.Cell(21, "发货时间", style);
            xls.Cell(22, "发货执行周期", style);
            xls.Cell(23, "箱数", style);
            xls.Cell(24, "重量", style);
            xls.Cell(25, "体积", style);
            xls.Cell(26, "Orderline行数", style);

            OutStorageDetail[] details = _WarehouseFacade.QueryOutStorageDetails(this.txtStorageCodeQuery.Text,
                                               this.drpPickTypeQuery.SelectedValue,
                                           FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                           FormatHelper.TODateInt(dateInDateToQuery.Text));

            int rowNum = 1;

            for (int i = 0; i < details.Length; i++)
            {
                xls.NewRow(rowNum);
                string PICKNO = details[i].PICKNO;
                xls.Cell(0, PICKNO, style);
                string INVNO = details[i].INVNO;
                xls.Cell(1, INVNO, style);
                string PICKTYPE = details[i].PICKTYPE;
                xls.Cell(2, PICKTYPE, style);
                string StorageCode = details[i].STORAGECODE;
                xls.Cell(3, StorageCode, style);
                string CDATE = FormatHelper.TODateTimeString(details[i].CDATE, details[i].CTIME);
                xls.Cell(4, CDATE, style);

                decimal periodOutDec = _WarehouseFacade.Totalday(details[i].Delivery_Date, details[i].Delivery_Time, details[i].CDATE, details[i].CTIME);
                string OUTSTORAGEPERIOD = string.Empty;
                if (periodOutDec >= 0)
                    OUTSTORAGEPERIOD = periodOutDec.ToString();
                xls.Cell(5, OUTSTORAGEPERIOD, style);


                DateWithTime pickBeginTime = _WarehouseFacade.StrToDateWithTime(details[i].MINPICK, '-');
                decimal warePeriod = _WarehouseFacade.Totalday(details[i].Delivery_Date, details[i].Delivery_Time, pickBeginTime.Date, pickBeginTime.Time);
                string WAREPERIOD = string.Empty;
                if (warePeriod >= 0)
                    WAREPERIOD = warePeriod.ToString();
                xls.Cell(6, WAREPERIOD, style);



                string DOWNDATE = FormatHelper.ToDateString(details[i].Down_Date);
                xls.Cell(7, DOWNDATE, style);

                string downPeriod = string.Empty;
                decimal downPeriodDec = _WarehouseFacade.Totalday(details[i].Down_Date, details[i].Down_Time, details[i].CDATE, details[i].CTIME);
                if (downPeriodDec >= 0)
                    downPeriod = downPeriodDec.ToString();
                xls.Cell(8, downPeriod, style);


                string PICKBEGINTIME = FormatHelper.TODateTimeString(pickBeginTime.Date, pickBeginTime.Time);
                xls.Cell(9, PICKBEGINTIME, style);


                DateWithTime pickEndTime = _WarehouseFacade.StrToDateWithTime(details[i].MAXPICK, '-');
                string PICKENDTIME = FormatHelper.TODateTimeString(pickEndTime.Date, pickEndTime.Time);
                xls.Cell(10, PICKENDTIME, style);

                decimal pickPeriod = _WarehouseFacade.Totalday(pickEndTime.Date, pickEndTime.Time, pickBeginTime.Date, pickBeginTime.Time);
                string pickPeriodStr = string.Empty;
                if (pickPeriod >= 0)
                    pickPeriodStr = pickPeriod.ToString();
                xls.Cell(11, pickPeriodStr, style);


                //DateWithTime packBeginTime = _WarehouseFacade.StrToDateWithTime(details[i].MINPACK, '-');

                DateWithTime packBeginTime = pickEndTime;
                DateWithTime packEndTime = _WarehouseFacade.StrToDateWithTime(details[i].MAXPACK, '-');
                string PACKBEGINTIME = FormatHelper.TODateTimeString(packBeginTime.Date, packBeginTime.Time);
                xls.Cell(12, PACKBEGINTIME, style);
                string PACKENDTIME = FormatHelper.TODateTimeString(packEndTime.Date, packEndTime.Time);
                xls.Cell(13, PACKENDTIME, style);



                decimal packPeriod = _WarehouseFacade.Totalday(packEndTime.Date, packEndTime.Time, packBeginTime.Date, packBeginTime.Time);
                string packPeriodStr = string.Empty;
                if (packPeriod >= 0)
                    packPeriodStr = packPeriod.ToString();
                xls.Cell(14, packPeriodStr, style);


                //DateWithTime oqcBeginTime = _WarehouseFacade.StrToDateWithTime(details[i].OQCCDATE, '-');

                DateWithTime oqcBeginTime = packEndTime;
                string OQCBEGINTIME = FormatHelper.TODateTimeString(oqcBeginTime.Date, oqcBeginTime.Time);
                xls.Cell(15, OQCBEGINTIME, style);


                DateWithTime oqcEndTime = _WarehouseFacade.StrToDateWithTime(details[i].OQCDATE, '-');
                string OQCENDTIME = FormatHelper.TODateTimeString(oqcEndTime.Date, oqcEndTime.Time);
                xls.Cell(16, OQCENDTIME, style);


                decimal oqcPeriod = _WarehouseFacade.Totalday(oqcEndTime.Date, oqcEndTime.Time, oqcBeginTime.Date, oqcBeginTime.Time);
                string oqcPeriodStr = string.Empty;
                if (oqcPeriod >= 0)
                    oqcPeriodStr = oqcPeriod.ToString();
                xls.Cell(17, oqcPeriodStr, style);


                //DateWithTime cartonnoBeginTime = _WarehouseFacade.StrToDateWithTime(details[i].CDATETIME, '-');

                DateWithTime cartonnoBeginTime = oqcEndTime;
                string CARINVNOBEGINTIME = FormatHelper.TODateTimeString(cartonnoBeginTime.Date, cartonnoBeginTime.Time);
                xls.Cell(18, CARINVNOBEGINTIME, style);


                string CARINVNOENDTIME = FormatHelper.TODateTimeString(details[i].Packing_List_Date, details[i].Packing_List_Time);
                xls.Cell(19, CARINVNOENDTIME, style);


                decimal carinvnoPeriod = _WarehouseFacade.Totalday(details[i].Packing_List_Date, details[i].Packing_List_Time, cartonnoBeginTime.Date, cartonnoBeginTime.Time);
                string carinvnoPeriodStr = string.Empty;
                if (carinvnoPeriod >= 0)
                    carinvnoPeriodStr = carinvnoPeriod.ToString();
                xls.Cell(20, carinvnoPeriodStr, style);



                string OUTSTORAGETIME = FormatHelper.TODateTimeString(details[i].Delivery_Date, details[i].Delivery_Time);
                xls.Cell(21, OUTSTORAGETIME, style);



                decimal outStoragePeriod1 = _WarehouseFacade.Totalday(details[i].Delivery_Date, details[i].Delivery_Time, details[i].Packing_List_Date, details[i].Packing_List_Time);
                string outStoragePeriod1Str = string.Empty;
                if (outStoragePeriod1 >= 0)
                    outStoragePeriod1Str = outStoragePeriod1.ToString();
                xls.Cell(22, outStoragePeriod1Str, style);


                string CARTONNOS = details[i].CARTONNOS.ToString();
                xls.Cell(23, OUTSTORAGEPERIOD, style);





                string WEIGHT = details[i].GROSS_WEIGHT.ToString();
                xls.Cell(24, WEIGHT, style);

                string VOLUME = details[i].VOLUMN.ToString();
                xls.Cell(25, VOLUME, style);

                string ORDERLINES = _WarehouseFacade.QueryOrderLines(details[i].PICKNO).ToString();
                xls.Cell(26, ORDERLINES, style);

                rowNum++;

            }






            string filename = string.Format("Export_{0}_{1}.{2}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString(), "xls");
            string filepath = string.Format(@"{0}{1}", this.DownloadPhysicalPath, filename);



            FileStream file = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite);
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
        #endregion

        private string _downloadPath = @"\upload\";
        public string DownloadPath
        {
            get
            {
                return string.Format(@"{0}{1}/", this.PageVirtualHostRoot, _downloadPath.Trim('\\', '/').Replace('\\', '/'));
            }
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
        class VerticalRegion : CellRegion
        {
            public VerticalRegion(int beginRow, int endRow, int col)
                : base(beginRow, endRow, col, col)
            {

            }
        }

        class HorizontalRange : CellRegion
        {
            public HorizontalRange(int row, int beginCol, int endCol)
                : base(row, row, beginCol, endCol)
            {
            }
        }

        abstract class CellRegion
        {
            public CellRegion(int beginRow, int endRow, int beginCol, int endCol)
            {
                this.beginRow = beginRow;
                this.endRow = endRow;
                this.beginCol = beginCol;
                this.endCol = endCol;
            }

            public CellRangeAddress GetRegion()
            {

                CellRangeAddress region = new CellRangeAddress(this.beginRow,
                                              this.endRow,
                                              this.beginCol,
                                              this.endCol);
                return region;

            }

            public int Row { get { return beginRow; } }
            public int Col
            {
                get { return beginCol; }
            }

            private int beginRow;
            private int endRow;
            private int beginCol;
            private int endCol;
        }
        class XlsPackage
        {
            private Dictionary<string, ISheet> sheets;
            private HSSFWorkbook hssfworkbook;
            private IRow row;
            private SheetConfig sheetConfig;
            private ISheet sheet;
            public XlsPackage()
            {
                hssfworkbook = new HSSFWorkbook();
                this.sheets = new Dictionary<string, ISheet>();

                this.sheetConfig = new SheetConfig(this.hssfworkbook);
            }


            public void InsertImg(string path)
            {
                HSSFPatriarch patriarch = (HSSFPatriarch)sheet.CreateDrawingPatriarch();
                //create the anchor
                HSSFClientAnchor anchor;
                // anchor = new HSSFClientAnchor(0, 0, 0, 255, 0, 2, 4, 4);
                //  anchor = new HSSFClientAnchor(0, 0, 0, 255, 0, 0, 5, 4);
                //anchor = new HSSFClientAnchor(0, 0, 0, 255, 0, 0, 4, 4);
                anchor = new HSSFClientAnchor(0, 0, 0, 255, 0, 0, 3, 3);
                anchor.AnchorType = AnchorType.MoveAndResize;// AnchorType.MoveDontResize|
                //load the picture and get the picture index in the workbook


                HSSFPicture picture = (HSSFPicture)patriarch.CreatePicture(anchor, LoadImage(path, hssfworkbook));
                //Reset the image to the original size.

                picture.LineStyle = LineStyle.None;

            }

            public static int LoadImage(string path, HSSFWorkbook wb)
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, (int)file.Length);
                return wb.AddPicture(buffer, PictureType.JPEG);

            }

            public void CreateSheet(string name)
            {

                if (this.sheets.ContainsKey(name))
                    throw new Exception("已存在相同名字的sheet!");
                this.sheets[name] = hssfworkbook.CreateSheet(name);
                this.sheet = sheets[name];
            }

            public void ToSheet(string name)
            {
                this.sheet = sheets[name];
            }

            public ISheet Sheet { get { return sheet; } }
            public void NewRow(int rowNum)
            {
                this.row = this.sheet.CreateRow(rowNum);
            }

            public void Cell(int col, string value, ICellStyle style)
            {
                ICell cell = CreateCell(col, style);
                cell.SetCellValue(value);
            }

            public void Cell(int col, int value, ICellStyle style)
            {
                ICell cell = CreateCell(col, style);
                cell.SetCellValue(value);
            }

            public void Cell(int col, IRichTextString rich)
            {

                ICell cell = CreateCell(col, Normal);
                cell.SetCellValue(rich);
            }


            private ICell CreateCell(int col, ICellStyle style)
            {
                ICell cell = row.CreateCell(col);
                cell.CellStyle = style;
                return cell;
            }

            public void Cell(CellRegion region, string value, ICellStyle style, bool isBorder)
            {
                ICell cell = row.CreateCell(region.Col);
                cell.CellStyle = style;
                CellRangeAddress address = region.GetRegion();
                sheet.AddMergedRegion(address);
                cell.SetCellValue(value);
                if (isBorder)
                    SetBorder(address);

            }



            public void Cell(CellRegion region, IRichTextString value, ICellStyle style, bool isBorder)
            {
                ICell cell = row.CreateCell(region.Col);
                CellRangeAddress address = region.GetRegion();
                sheet.AddMergedRegion(address);
                cell.SetCellValue(value);
                if (isBorder)
                    SetBorder(address);

            }

            public void SetBorder(CellRangeAddress region)
            {
                ((HSSFSheet)sheet).SetEnclosedBorderOfRegion(region, NPOI.SS.UserModel.BorderStyle.Thin, NPOI.HSSF.Util.HSSFColor.Black.Index);
            }

            public void SetColumnWidth(int col, int width)
            {
                sheet.SetColumnWidth(col, width * 256);
            }

            public void SetRowHeight(short height)
            {
                this.row.Height = height;
            }


            public void Save(Stream s)
            {
                this.hssfworkbook.Write(s);
            }

            public ICellStyle Normal { get { return sheetConfig.StyleNormal; } }
            public ICellStyle Warn { get { return sheetConfig.StyleWarn; } }
            public IFont Red { get { return sheetConfig.Red; } }
            public ICellStyle Title { get { return sheetConfig.StyleTitle; } }
            public IFont Green { get { return sheetConfig.Green; } }
            public IFont Black { get { return sheetConfig.Black; } }

            public ICellStyle WarnWithBorder
            {
                get
                {
                    ICellStyle style = sheetConfig.StyleWarn;
                    style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.BottomBorderColor = HSSFColor.Black.Index;
                    style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.LeftBorderColor = HSSFColor.Black.Index;
                    style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.TopBorderColor = HSSFColor.Black.Index;
                    style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.RightBorderColor = HSSFColor.Black.Index;
                    return style;

                }
            }
            public ICellStyle NormalWithBorder
            {
                get
                {
                    ICellStyle style = sheetConfig.StyleNormal;
                    style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.BottomBorderColor = HSSFColor.Black.Index;
                    style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.LeftBorderColor = HSSFColor.Black.Index;
                    style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.TopBorderColor = HSSFColor.Black.Index;
                    style.WrapText = true;
                    style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.RightBorderColor = HSSFColor.Black.Index;
                    return style;
                }
            }

            public ICellStyle NormalWithTopBorder
            {
                get
                {
                    ICellStyle style = sheetConfig.StyleNormal;

                    style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.TopBorderColor = HSSFColor.Black.Index;

                    return style;
                }
            }

            private class SheetConfig
            {

                private HSSFWorkbook hssfworkbook;

                public SheetConfig(HSSFWorkbook hssfworkbook)
                {
                    this.hssfworkbook = hssfworkbook;
                }

                public HSSFWorkbook SheetBook { get { return hssfworkbook; } }

                public readonly ICellStyle Normal;
                public readonly ICellStyle Warn;

                private ICellStyle DefaultNormal()
                {

                    ICellStyle style = hssfworkbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Center;
                    style.SetFont(Black);
                    return style;

                }

                private ICellStyle DefaultWarn()
                {

                    ICellStyle style = hssfworkbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Center;

                    style.SetFont(Red);
                    return style;
                }

                public IFont Green
                {
                    get
                    {
                        IFont font = hssfworkbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Color = NPOI.HSSF.Util.HSSFColor.Green.Index;
                        return font;
                    }
                }

                public IFont Red
                {
                    get
                    {
                        IFont font = hssfworkbook.CreateFont();
                        font.FontHeightInPoints = 12;
                        font.Color = NPOI.HSSF.Util.HSSFColor.Red.Index;
                        return font;
                    }
                }

                public IFont Black
                {
                    get
                    {
                        IFont font = hssfworkbook.CreateFont();
                        font.FontHeightInPoints = 12;
                        font.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
                        return font;
                    }
                }

                public ICellStyle DefaultTitleStyle()
                {
                    IFont font = hssfworkbook.CreateFont();
                    font.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
                    font.Boldweight = 30;
                    font.FontHeightInPoints = 14;
                    ICellStyle style = hssfworkbook.CreateCellStyle();
                    style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.BottomBorderColor = HSSFColor.Black.Index;
                    style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.LeftBorderColor = HSSFColor.Black.Index;
                    style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.TopBorderColor = HSSFColor.Black.Index;
                    style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                    style.RightBorderColor = HSSFColor.Black.Index;

                    style.Alignment = HorizontalAlignment.Center;
                    style.VerticalAlignment = VerticalAlignment.Center;
                    style.SetFont(font);
                    return style;
                }


                public ICellStyle StyleTitle
                {
                    get
                    {

                        return DefaultTitleStyle();
                    }
                }

                public ICellStyle StyleNormal
                {
                    get
                    {
                        return DefaultNormal();
                    }
                }

                public ICellStyle StyleWarn { get { return DefaultWarn(); } }
            }
        }
    }
}




