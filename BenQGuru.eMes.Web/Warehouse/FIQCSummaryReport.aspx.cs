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
    public partial class FIQCSummaryReport : BaseMPageNew
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

            this.gridHelper.AddColumn("IQCCCOUNT", "创建IQC个数", null);
            this.gridHelper.AddColumn("QCCOUNT", "QC检验个数", null);
            this.gridHelper.AddColumn("SQECOUNT", "SQE判定个数", null);
            this.gridHelper.AddColumn("AVERQCTIME", "平均QC检验时间", null);
            this.gridHelper.AddColumn("AVERSQETIME", "平均SQE判定时间", null);
            this.gridHelper.AddColumn("AVERIQCTIME", "平均IQC执行时间", null);
            this.gridHelper.AddColumn("QTY", "来料总数", null);
            this.gridHelper.AddColumn("SAMPLECOUNT", "样本总数", null);
            this.gridHelper.AddColumn("NGQTY", "缺陷品总数", null);
            this.gridHelper.AddColumn("RETURNQTY", "退换货数量", null);
            this.gridHelper.AddColumn("REFORMQTY", "现场整改数量", null);
            this.gridHelper.AddColumn("GIVEQTY", "让步接收数量", null);
            this.gridHelper.AddColumn("ACCEPTQTY", "特采放行数量", null);


            SystemSettingFacade _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            object[] objs = _SystemSettingFacade.GetErrorGroupcode();
            if (objs != null && objs.Length > 0)
            {
                foreach (ErrorCodeGroupA ecg in objs)
                {
                    this.gridHelper.AddColumn(ecg.ErrorCodeGroup, ecg.ErrorCodeGroupDescription, null);

                }
            }


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
            IQCSUMMARY s = (IQCSUMMARY)obj;
            row["IQCCCOUNT"] = s.IQCCCOUNT;
            row["QCCOUNT"] = s.QCCOUNT;
            row["SQECOUNT"] = s.SQECOUNT;

            row["AVERQCTIME"] = facade.QCAverPeriod(txtStorageCodeQuery.Text, drpStorageInTypeQuery.SelectedValue, txtVendorCodeQuery.Text, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            row["AVERSQETIME"] = facade.SQEAverPeriod(txtStorageCodeQuery.Text, drpStorageInTypeQuery.SelectedValue, txtVendorCodeQuery.Text, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            row["AVERIQCTIME"] = facade.IQCAverPeriod(txtStorageCodeQuery.Text, drpStorageInTypeQuery.SelectedValue, txtVendorCodeQuery.Text, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));


            row["QTY"] = s.QTY;
            row["SAMPLECOUNT"] = s.SAMPLECOUNT;
            row["NGQTY"] = s.NGQTY;
            row["RETURNQTY"] = s.RETURNQTY;
            row["REFORMQTY"] = s.REFORMQTY;
            row["GIVEQTY"] = s.GIVEQTY;
            row["ACCEPTQTY"] = s.ACCEPTQTY;


            SystemSettingFacade _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            object[] objs = _SystemSettingFacade.GetErrorGroupcode();
            if (objs != null && objs.Length > 0)
            {
                foreach (ErrorCodeGroupA ecg in objs)
                {
                    IQCSUMMARY[] summary = facade.EGGroupQty(ecg.ErrorCodeGroup,
                        txtStorageCodeQuery.Text, drpStorageInTypeQuery.SelectedValue, txtVendorCodeQuery.Text,
                        FormatHelper.TODateInt(dateInDateFromQuery.Text),
                        FormatHelper.TODateInt(dateInDateToQuery.Text));

                    int sum = 0;
                    foreach (IQCSUMMARY o in summary)
                    {
                        sum += o.NGQTY;
                    }
                    row[ecg.ErrorCodeGroup] = sum;
                }
            }



            return row;
        }



        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryIQCSummarys(FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text),this.txtStorageCodeQuery.Text,
                                         this.drpStorageInTypeQuery.SelectedValue,
                                         this.txtVendorCodeQuery.Text
                                        );
        }

        protected override int GetRowCount()
        {
            int rowCount = 0;
            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                rowCount++;
            }
            return rowCount;
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


           


            xls.CreateSheet("IQC统计表");
            xls.NewRow(0);
            xls.Cell(0, "创建OQC个数", style);
            xls.Cell(1, "QC检验个数", style);
            xls.Cell(2, "SQE判定个数", style);
            xls.Cell(3, "平均QC检验时间", style);
            xls.Cell(4, "平均SQE判定时间", style);

            xls.Cell(5, "平均IQC执行时间", style);
            xls.Cell(6, "来料总数", style);
            xls.Cell(7, "样本总数", style);
            xls.Cell(8, "缺陷品总数", style);
            xls.Cell(9, "退换货数量", style);
            xls.Cell(10, "现场整改数量", style);
            
            xls.Cell(11, "让步接收数量", style);
            xls.Cell(12, "特采放行数量", style);
        
            int cellNum = 13;
            SystemSettingFacade _SystemSettingFacade = new SystemSettingFacade(base.DataProvider);
            object[] objs = _SystemSettingFacade.GetErrorGroupcode();
            if (objs != null && objs.Length > 0)
            {

                foreach (ErrorCodeGroupA ecg in objs)
                {

                    xls.Cell(cellNum, ecg.ErrorCodeGroupDescription, style);
                    cellNum++;

                }
            }

        


            int rowNum = 1;

            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                xls.NewRow(rowNum);
                string IQCCCOUNT = this.gridWebGrid.Rows[i].Items.FindItemByKey("IQCCCOUNT").Text;
                xls.Cell(0, IQCCCOUNT, style);
                string QCCOUNT = this.gridWebGrid.Rows[i].Items.FindItemByKey("QCCOUNT").Text;
                xls.Cell(1, QCCOUNT, style);
                string SQECOUNT = this.gridWebGrid.Rows[i].Items.FindItemByKey("SQECOUNT").Text;
                xls.Cell(2, SQECOUNT, style);
                string AVERQCTIME = this.gridWebGrid.Rows[i].Items.FindItemByKey("AVERQCTIME").Text;
                xls.Cell(3, AVERQCTIME, style);
                string AVERSQETIME = this.gridWebGrid.Rows[i].Items.FindItemByKey("AVERSQETIME").Text;
                xls.Cell(4, AVERSQETIME, style);
                string AVERIQCTIME = this.gridWebGrid.Rows[i].Items.FindItemByKey("AVERIQCTIME").Text;
                xls.Cell(5, AVERIQCTIME, style);
                string QTY = this.gridWebGrid.Rows[i].Items.FindItemByKey("QTY").Text;
                xls.Cell(6, QTY, style);
                string SAMPLECOUNT = this.gridWebGrid.Rows[i].Items.FindItemByKey("SAMPLECOUNT").Text;
                xls.Cell(7, SAMPLECOUNT, style);
                string NGQTY = this.gridWebGrid.Rows[i].Items.FindItemByKey("NGQTY").Text;
                xls.Cell(8, NGQTY, style);
                string RETURNQTY = this.gridWebGrid.Rows[i].Items.FindItemByKey("RETURNQTY").Text;
                xls.Cell(9, RETURNQTY, style);
                string REFORMQTY = this.gridWebGrid.Rows[i].Items.FindItemByKey("REFORMQTY").Text;
                xls.Cell(10, REFORMQTY, style);
                string GIVEQTY = this.gridWebGrid.Rows[i].Items.FindItemByKey("GIVEQTY").Text;
                xls.Cell(11, GIVEQTY, style);
                string ACCEPTQTY = this.gridWebGrid.Rows[i].Items.FindItemByKey("ACCEPTQTY").Text;
                xls.Cell(12, ACCEPTQTY, style);
               
                int cellNum2 = 13;
                if (objs != null && objs.Length > 0)
                {
                    foreach (ErrorCodeGroupA ecg in objs)
                    {
                        string ngStr = this.gridWebGrid.Rows[i].Items.FindItemByKey(ecg.ErrorCodeGroup).Text;
                        xls.Cell(cellNum2, ngStr, style);
                        cellNum2++;
                    }
                }
                

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
