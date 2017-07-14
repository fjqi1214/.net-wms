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
    public partial class FoutstorageSummaryReport : BaseMPageNew
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

            this.gridHelper.AddColumn("StorageCode123", "库位", null);
            this.gridHelper.AddColumn("PICKCCOUNT", "创建拣货任务令个数", null);
            this.gridHelper.AddColumn("PICKDOWNCOUNT", "下发拣货任务令个数", null);
            this.gridHelper.AddColumn("AVEROUTSTORAGEPERIOD", "平均出库执行周期", null);
            this.gridHelper.AddColumn("AVGERWAREPERIOD", "平均库房操作周期", null);
            this.gridHelper.AddColumn("AVERDOWNPERIOD", "平均下发执行周期", null);
            this.gridHelper.AddColumn("CLOSEPICKCOUNT", "完成拣料个数", null);
            this.gridHelper.AddColumn("AVERPICKPERIOD", "平均每个拣货任务令的拣料周期", null);
            this.gridHelper.AddColumn("AVERPICKORDERLINEPERIOD", "平均每个ORDERLINE的拣料周期", null);
            this.gridHelper.AddColumn("CLOSEPPACKCOUNT", "完成包装个数", null);
            this.gridHelper.AddColumn("AVERPACKPERIOD", "平均每个拣货任务令的包装周期", null);
            this.gridHelper.AddColumn("AVERPACKORDERLINEPERIOD", "平均每个ORDERLINE的包装周期", null);
            this.gridHelper.AddColumn("AVEROQCPERIOD", "平均每个拣货任务令的OQC周期", null);
            this.gridHelper.AddColumn("CLOSEPACKLISTCOUNT", "完成箱单个数", null);
            this.gridHelper.AddColumn("AVERCLOSEPACKLISTPERIOD", "平均每个拣货任务令的箱单制作周期", null);
            this.gridHelper.AddColumn("AVERORDERLINECLOSEPACKLISTPERIOD", "平均每个ORDERLINE的箱单制作周期", null);
            this.gridHelper.AddColumn("FINISHCARTONNOCOUNT", "完成发货个数", null);
            this.gridHelper.AddColumn("AVERFINISHCARTONNOPERIOD", "平均发货周期", null);
            this.gridHelper.AddColumn("CARTONNOS", "总箱数", null);
            this.gridHelper.AddColumn("WEIGHT", "总重量", null);
            this.gridHelper.AddColumn("VOLUME", "总体积", null);
            this.gridHelper.AddColumn("ORDERLINES", "总Orderline行数", null);




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
            OutStorageSummary s = (OutStorageSummary)obj;


            row["StorageCode123"] = s.StorageCode;
            row["PICKCCOUNT"] = s.picktotal;
            row["PICKDOWNCOUNT"] = s.ISSUECOUNT;
            decimal averPick = facade.averPickPeriod(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            decimal averPack = facade.averPackPeriod(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));

            decimal averOQC = facade.averOQCPeriod(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            decimal averClosePackList = facade.averCARTONNOPeriod(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            decimal averWare = facade.averWarePeriod(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            decimal averDown = facade.averDownPeriod(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            decimal averDeliery = facade.averDeliveryPeriod(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            decimal averDeliery2 = facade.averDeliveryPeriod2(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            row["AVEROUTSTORAGEPERIOD"] = averDeliery;
            row["AVGERWAREPERIOD"] = averWare;
            row["AVERDOWNPERIOD"] = averDown;
            row["CLOSEPICKCOUNT"] = s.PICKCOUNT;
            row["AVERPICKPERIOD"] = averPick;
            row["AVERPICKORDERLINEPERIOD"] = facade.averPickOrderLinePeriod(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            row["CLOSEPPACKCOUNT"] = s.PACKCOUNT;

            row["AVERPACKPERIOD"] = averPack;
            row["AVERPACKORDERLINEPERIOD"] = facade.averPackOrderLinePeriod(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            row["AVEROQCPERIOD"] = averOQC;
            row["CLOSEPACKLISTCOUNT"] = s.FINISHCARTONNOCOUNT;
            row["AVERCLOSEPACKLISTPERIOD"] = averClosePackList;
            row["AVERORDERLINECLOSEPACKLISTPERIOD"] = facade.averCARTONNOOrderLinesPeriod(s.StorageCode, drpPickTypeQuery.SelectedValue == "DNZC" ? "DNC" : drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            row["FINISHCARTONNOCOUNT"] = s.FINISHCARTONNOCOUNT;



            row["AVERFINISHCARTONNOPERIOD"] = averDeliery2;
            row["CARTONNOS"] = s.CARTONNOS;
            row["WEIGHT"] = s.GROSS_WEIGHT;
            row["VOLUME"] = s.VOLUME;
            row["ORDERLINES"] = facade.SumOrderLines(s.StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));


            return row;
        }



        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryOutStorageSummarys(txtStorageCodeQuery.Text, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text), inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryOutStorageSummarysCount(txtStorageCodeQuery.Text, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
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
            OutStorageSummary[] summarys = _WarehouseFacade.QueryOutStorageSummarys(txtStorageCodeQuery.Text, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                         FormatHelper.TODateInt(dateInDateToQuery.Text));
            XlsPackage xls = new XlsPackage();
            IFont font = xls.Black;
            font.FontHeightInPoints = 10;
            ICellStyle style = xls.Normal;
            style.SetFont(font);






            xls.CreateSheet("出库统计表");
            xls.NewRow(0);
            xls.Cell(0, "库位", style);
            xls.Cell(1, "创建拣货任务令个数", style);
            xls.Cell(2, "下发拣货任务令个数", style);
            xls.Cell(3, "平均出库执行周期", style);
            xls.Cell(4, "平均库房操作周期", style);
            xls.Cell(5, "平均下发执行周期", style);
            xls.Cell(6, "完成拣料个数", style);
            xls.Cell(7, "平均每个拣货任务令的拣料周期", style);
            xls.Cell(8, "平均每个ORDERLINE的拣料周期", style);
            xls.Cell(9, "完成包装个数", style);

            xls.Cell(10, "平均每个ORDERLINE的包装周期", style);
            xls.Cell(11, "平均每个拣货任务令的包装周期", style);
            xls.Cell(12, "平均每个拣货任务令的OQC周期", style);
            xls.Cell(13, "完成箱单个数", style);
            xls.Cell(14, "平均每个拣货任务令的箱单制作周期", style);
            xls.Cell(15, "平均每个ORDERLINE的箱单制作周期", style);
            xls.Cell(16, "完成发货个数", style);
            xls.Cell(17, "平均发货周期", style);


            xls.Cell(18, "总箱数", style);


            xls.Cell(19, "总重量", style);
            xls.Cell(20, "总体积", style);
            xls.Cell(21, "总Orderline行数", style);

            int rowNum = 1;





            for (int i = 0; i < summarys.Length; i++)
            {
                xls.NewRow(rowNum);


                xls.Cell(0, summarys[i].StorageCode, style);
                xls.Cell(1, summarys[i].picktotal.ToString(), style);
                xls.Cell(2, summarys[i].ISSUECOUNT.ToString(), style);


                decimal averPick = _WarehouseFacade.averPickPeriod(summarys[i].StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                             FormatHelper.TODateInt(dateInDateToQuery.Text));
                decimal averPack = _WarehouseFacade.averPackPeriod(summarys[i].StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                             FormatHelper.TODateInt(dateInDateToQuery.Text));

                decimal averOQC = _WarehouseFacade.averOQCPeriod(summarys[i].StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                             FormatHelper.TODateInt(dateInDateToQuery.Text));

                decimal averClosePackList = _WarehouseFacade.averCARTONNOPeriod(summarys[i].StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                             FormatHelper.TODateInt(dateInDateToQuery.Text));
                decimal averWare = (averPick + averPack + averOQC + averClosePackList);

                decimal averDown = _WarehouseFacade.averDownPeriod(summarys[i].StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                           FormatHelper.TODateInt(dateInDateToQuery.Text));

                decimal averDeliery = _WarehouseFacade.averDeliveryPeriod(summarys[i].StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                             FormatHelper.TODateInt(dateInDateToQuery.Text));

                xls.Cell(3, (averWare + averDown + averDeliery).ToString(), style);
                xls.Cell(4, averWare.ToString(), style);

                xls.Cell(5, averDown.ToString(), style);

                xls.Cell(6, summarys[i].PICKCOUNT, style);

                xls.Cell(7, averPick.ToString(), style);

                xls.Cell(8, _WarehouseFacade.averPickOrderLinePeriod(summarys[i].StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                             FormatHelper.TODateInt(dateInDateToQuery.Text)).ToString(), style);


                xls.Cell(9, summarys[i].PACKCOUNT, style);

                xls.Cell(10, _WarehouseFacade.averPackOrderLinePeriod(summarys[i].StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                             FormatHelper.TODateInt(dateInDateToQuery.Text)).ToString(), style);

                xls.Cell(11, averPack.ToString(), style);


                xls.Cell(12, averOQC.ToString(), style);



                xls.Cell(13, summarys[i].FINISHCARTONNOCOUNT.ToString(), style);

                xls.Cell(14, averClosePackList.ToString(), style);

                xls.Cell(15, _WarehouseFacade.averCARTONNOOrderLinesPeriod(summarys[i].StorageCode, drpPickTypeQuery.SelectedValue == "DNZC" ? "DNC" : drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                             FormatHelper.TODateInt(dateInDateToQuery.Text)).ToString(), style);

                xls.Cell(16, summarys[i].FINISHCARTONNOCOUNT.ToString(), style);


                xls.Cell(17, averDeliery.ToString(), style);


                xls.Cell(18, summarys[i].CARTONNOS, style);

                xls.Cell(19, summarys[i].GROSS_WEIGHT.ToString(), style);

                xls.Cell(20, summarys[i].VOLUME.ToString(), style);

                xls.Cell(21, _WarehouseFacade.SumOrderLines(summarys[i].StorageCode, drpPickTypeQuery.SelectedValue, FormatHelper.TODateInt(dateInDateFromQuery.Text),
                                             FormatHelper.TODateInt(dateInDateToQuery.Text)).ToString(), style);

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




