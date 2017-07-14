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
    public partial class FINStorageDetailReport : BaseMPageNew
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
                object[] parameters1 = _SystemSettingFacade.GetParametersByParameterGroup("INVINTYPE");
                this.drpStorageInTypeQuery.Items.Add(new ListItem(string.Empty, string.Empty));
                if (parameters1 != null && parameters1.Length > 0)
                {
                    foreach (Domain.BaseSetting.Parameter parameter in parameters1)
                    {
                        this.drpStorageInTypeQuery.Items.Add(new ListItem(parameter.ParameterDescription, parameter.ParameterAlias));
                    }



                }

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

            this.gridHelper.AddColumn("STNO", "入库指令号", null);
            this.gridHelper.AddColumn("INVNO", "SAP单据号", null);
            this.gridHelper.AddColumn("STTYPE", "入库类型", null);
            this.gridHelper.AddColumn("StorageCode", "库位", null);
            this.gridHelper.AddColumn("VENDORCODE", "供应商代码", null);
            this.gridHelper.AddColumn("VendorName", "供应商名称", null);
            this.gridHelper.AddColumn("ASNCDATE", "ASN创建时间", null);
            this.gridHelper.AddColumn("CARTONNOS", "箱数", null);
            this.gridHelper.AddColumn("GROSSWEIGHT", "重量", null);
            this.gridHelper.AddColumn("VOLUME", "体积", null);
            this.gridHelper.AddColumn("ISSUEDATE1", "下发时间", null);
            this.gridHelper.AddColumn("INSTORAGERange1", "入库执行周期", null);
            this.gridHelper.AddColumn("RECEIVEBEGIN", "到货初检开始时间", null);
            this.gridHelper.AddColumn("RECEIVEEND", "到货初检完成时间", null);
            this.gridHelper.AddColumn("RECEIVERANGE", "到货初检执行周期", null);
            this.gridHelper.AddColumn("IQCCDATE", "IQC开始时间", null);
            this.gridHelper.AddColumn("IQCENDDATE", "IQC结束时间", null);
            this.gridHelper.AddColumn("IQCRANGE", "IQC执行周期", null);
            this.gridHelper.AddColumn("INSTORAGEBEGIN", "上架开始时间", null);
            this.gridHelper.AddColumn("INSTORAGEEND", "上架完成时间", null);
            this.gridHelper.AddColumn("INSTORAGERANGE", "上架执行周期", null);





            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);



        }

        protected override DataRow GetGridRow(object obj)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            DataRow row = this.DtSource.NewRow();
            InStorageDetailRecord s = (InStorageDetailRecord)obj;
            row["STNO"] = s.STNO;
            row["INVNO"] = s.INVNO;
            row["STTYPE"] = s.STTYPE;
            row["StorageCode"] = s.StorageCode;
            row["VENDORCODE"] = s.VENDORCODE;
            row["VendorName"] = s.VendorName;
            row["ASNCDATE"] = FormatHelper.TODateTimeString(s.ASNCDATE, s.ASNCTIME);
            row["CARTONNOS"] = s.CARTONNOCount;
            row["GROSSWEIGHT"] = s.GROSS_WEIGHT;

            row["VOLUME"] = s.VOLUME;

            row["ISSUEDATE1"] = FormatHelper.TODateTimeString(s.IssueDateInt, s.IssueTimeInt);

            decimal receiveRange = facade.Totalday(s.ReceiveEndDateInt, s.ReceiveEndTimeInt, s.ReceiveBeginDateInt, s.ReceiveBeginTimeInt);
            if (receiveRange < 0)
                receiveRange = 0;

            decimal iqcRange = facade.Totalday(s.IQCDateInt, s.IQCTimeInt, s.CDateInt, s.CTimeInt);
            if (iqcRange < 0)
                iqcRange = 0;

            decimal instorageRage = facade.Totalday(s.InStoraeeDateInt, s.InStoraeeTimeInt, s.ReceiveBeginDateInt, s.ReceiveBeginTimeInt);
            if (instorageRage < 0)
                instorageRage = 0;


            decimal instorageRage1 = facade.Totalday(s.InStoraeeDateInt, s.InStoraeeTimeInt, s.IQCDateInt, s.IQCTimeInt);
            if (instorageRage1 < 0)
                instorageRage1 = 0;

            row["INSTORAGERange1"] = instorageRage;


            row["RECEIVEBEGIN"] = FormatHelper.TODateTimeString(s.ReceiveBeginDateInt, s.ReceiveBeginTimeInt);
            row["RECEIVEEND"] = FormatHelper.TODateTimeString(s.ReceiveEndDateInt, s.ReceiveEndTimeInt);


            row["RECEIVERANGE"] = receiveRange;



            row["IQCCDATE"] = FormatHelper.TODateTimeString(s.CDateInt, s.CTimeInt);

            row["IQCENDDATE"] = FormatHelper.TODateTimeString(s.IQCDateInt, s.IQCTimeInt);




            row["IQCRANGE"] = iqcRange;

            row["INSTORAGEBEGIN"] = FormatHelper.TODateTimeString(s.InStoraeeBeginDateInt, s.InStoraeeBeginTimeInt);
            row["INSTORAGEEND"] = FormatHelper.TODateTimeString(s.InStoraeeDateInt, s.InStoraeeTimeInt);






            row["INSTORAGERANGE"] = instorageRage1;



            return row;
        }



        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryInStorageDetails(txtStorageInASNQuery.Text, txtInvNoQuery.Text, this.txtStorageCodeQuery.Text,
                                         this.txtVendorCodeQuery.Text, this.drpStorageInTypeQuery.SelectedValue,
                                         FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text), inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryInStorageDetailsCount(txtStorageInASNQuery.Text, txtInvNoQuery.Text, this.txtStorageCodeQuery.Text,
                                         this.txtVendorCodeQuery.Text, this.drpStorageInTypeQuery.SelectedValue,
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


            xls.CreateSheet("入库明细表");
            xls.NewRow(0);
            xls.Cell(0, "入库指令号", style);
            xls.Cell(1, "SAP单据号", style);
            xls.Cell(2, "入库类型", style);
            xls.Cell(3, "库位", style);
            xls.Cell(4, "供应商代码", style);
            xls.Cell(5, "供应商名称", style);
            xls.Cell(6, "ASN创建时间", style);
            xls.Cell(7, "箱数", style);
            xls.Cell(8, "重量", style);
            xls.Cell(9, "体积", style);
            xls.Cell(10, "下发时间", style);
            xls.Cell(11, "入库执行周期", style);
            xls.Cell(12, "到货初检开始时间", style);
            xls.Cell(13, "到货初检完成时间", style);
            xls.Cell(14, "到货初检执行周期", style);
            xls.Cell(15, "IQC开始时间", style);
            xls.Cell(16, "IQC结束时间", style);
            xls.Cell(17, "IQC执行周期", style);
            xls.Cell(18, "上架开始时间", style);
            xls.Cell(19, "上架完成时间", style);
            xls.Cell(20, "上架执行周期", style);
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            InStorageDetailRecord[] ins = facade.QueryInStorageDetails(txtStorageInASNQuery.Text, txtInvNoQuery.Text, this.txtStorageCodeQuery.Text,
                                           this.txtVendorCodeQuery.Text, this.drpStorageInTypeQuery.SelectedValue,
                                           FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                           FormatHelper.TODateInt(dateInDateToQuery.Text));

            int rowNum = 1;

            for (int i = 0; i < ins.Length; i++)
            {
                xls.NewRow(rowNum);
                string STNO = ins[i].STNO;
                xls.Cell(0, STNO, style);
                string invNo = ins[i].INVNO;
                xls.Cell(1, invNo, style);
                string stType = ins[i].STTYPE;
                xls.Cell(2, stType, style);
                string StorageCode = ins[i].StorageCode;
                xls.Cell(3, StorageCode, style);
                string VENDORCODE = ins[i].VENDORCODE;
                xls.Cell(4, VENDORCODE, style);
                string VendorName = ins[i].VendorName;
                xls.Cell(5, VendorName, style);
                string ASNCDATE = FormatHelper.TODateTimeString(ins[i].ASNCDATE, ins[i].ASNCTIME);
                xls.Cell(6, ASNCDATE, style);
                string CARTONNOS = ins[i].CARTONNOCount.ToString();
                xls.Cell(7, CARTONNOS, style);
                string GROSSWEIGHT = ins[i].GROSS_WEIGHT.ToString();
                xls.Cell(8, GROSSWEIGHT, style);
                string VOLUME = ins[i].VOLUME;
                xls.Cell(9, VOLUME, style);

                string ISSUEDATE1 = FormatHelper.TODateTimeString(ins[i].IssueDateInt, ins[i].IssueTimeInt);
                xls.Cell(10, ISSUEDATE1, style);

                decimal receiveRange = facade.Totalday(ins[i].ReceiveEndDateInt, ins[i].ReceiveEndTimeInt, ins[i].ReceiveBeginDateInt, ins[i].ReceiveBeginTimeInt);
                if (receiveRange < 0)
                    receiveRange = 0;

                decimal iqcRange = facade.Totalday(ins[i].IQCDateInt, ins[i].IQCTimeInt, ins[i].CDateInt, ins[i].CTimeInt);
                if (iqcRange < 0)
                    iqcRange = 0;


                decimal instorageRange = facade.Totalday(ins[i].InStoraeeDateInt, ins[i].InStoraeeTimeInt, ins[i].ReceiveBeginDateInt, ins[i].ReceiveBeginTimeInt);

                if (instorageRange < 0)
                    instorageRange = 0;


                decimal instorageRange1 = facade.Totalday(ins[i].InStoraeeDateInt, ins[i].InStoraeeTimeInt, ins[i].IQCDateInt, ins[i].IQCTimeInt);
                if (instorageRange1 < 0)
                    instorageRange1 = 0;

                string INSTORAGERange = string.Empty;

                INSTORAGERange = (instorageRange).ToString();

                xls.Cell(11, INSTORAGERange, style);


                string RECEIVEBEGIN = FormatHelper.TODateTimeString(ins[i].ReceiveBeginDateInt, ins[i].ReceiveBeginTimeInt);
                xls.Cell(12, RECEIVEBEGIN, style);
                string RECEIVEEND = FormatHelper.TODateTimeString(ins[i].ReceiveEndDateInt, ins[i].ReceiveEndTimeInt);
                xls.Cell(13, RECEIVEEND, style);




                string RECEIVERANGE = string.Empty;


                RECEIVERANGE = receiveRange.ToString();


                xls.Cell(14, RECEIVERANGE, style);


                string IQCCDATE = string.Empty;
                if (ins[i].IQCDate != null)
                    IQCCDATE = FormatHelper.TODateTimeString(ins[i].CDateInt, ins[i].CTimeInt);
                xls.Cell(15, IQCCDATE, style);
                string IQCENDDATE = string.Empty;
                if (ins[i].IQCDate != null)
                    IQCENDDATE = FormatHelper.TODateTimeString(ins[i].IQCDateInt,ins[i].IQCTimeInt);
                xls.Cell(16, IQCENDDATE, style);


                string IQCRANGE = string.Empty;



                IQCRANGE = iqcRange.ToString();

                xls.Cell(17, IQCRANGE, style);

                string INSTORAGEBEGIN = string.Empty;
                string INSTORAGEEND = string.Empty;
                string INSTORAGERANGE = string.Empty;

                INSTORAGEBEGIN = FormatHelper.TODateTimeString(ins[i].InStoraeeBeginDateInt, ins[i].InStoraeeBeginTimeInt);
                INSTORAGEEND = FormatHelper.TODateTimeString(ins[i].InStoraeeDateInt, ins[i].InStoraeeTimeInt);



                INSTORAGERANGE = instorageRange.ToString();





                xls.Cell(18, INSTORAGEBEGIN, style);

                xls.Cell(19, INSTORAGEEND, style);

                xls.Cell(20, instorageRange1.ToString(), style);


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




