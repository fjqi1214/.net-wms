#region system
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.Document;
#endregion

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FModelMP 的摘要说明。
    /// </summary>
    public partial class FDocView : BaseMPageMinus
    {
        //protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        //private GridHelper gridHelper = null;
        private ButtonHelper buttonHelper = null;
        private DocumentFacade _documentFacade;//= FacadeFactory.CreateModelFacade();
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

        SystemSettingFacade _facade = null;

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
            this.gridWebGrid.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid_InitializeRow);
        }
        void gridWebGrid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
        {
            e.Row.Items.FindItemByKey("Docname").CssClass = "LinkFontBlue";
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            // this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            //this.gridWebGrid. += new Infragistics.WebUI.UltraWebGrid.InitializeRowEventHandler(gridWebGrid_InitializeRow);
            // 
            // languageComponent1
            // 
            //this.languageComponent1.Language = "CHS";
            //this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
            //this.languageComponent1.RuntimePage = null;
            //this.languageComponent1.RuntimeUserControl = null;
            //this.languageComponent1.UserControlName = "";
            // 
            // excelExporter
            // 
            this.excelExporter.FileExtension = "xls";
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.Page = this;
            this.excelExporter.RowSplit = "\r\n";

        }
        #endregion

        #region
        private void BuildValidStatus()
        {
            this.drpValidStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString("Y"), "Y"));
            this.drpValidStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString("N"), "N"));
            this.drpValidStatusQuery.Items.Insert(0, "");
            this.drpValidStatusQuery.SelectedIndex = 1;
        }

        private void BuildCheckStatus()
        {
            this.drpCheckedStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString("Y"), "Y"));
            this.drpCheckedStatusQuery.Items.Add(new ListItem(this.languageComponent1.GetString("N"), "N"));
            this.drpCheckedStatusQuery.Items.Insert(0, "");
            this.drpCheckedStatusQuery.SelectedIndex = 1;
        }

        #endregion

        #region form events
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitHander();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 初始化界面UI
                this.InitUI();
                this.InitButton();
                this.InitWebGrid();

                BuildParentDocTypeList();
                BuildValidStatus();
                BuildCheckStatus();

                InitParameters();
            }
        }




        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
        {
            this.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            try
            {
                if (commandName == "Docname")
                {
                    if (_documentFacade == null)
                    {
                        _documentFacade = new DocumentFacade(this.DataProvider);
                    }

                    bool right = _documentFacade.GetDocDirRight(int.Parse(row.Items.FindItemByKey("Dirserial").Value.ToString()), this.GetUserCode(), "QUERY");
                    Doc doc = _documentFacade.GetDOC(int.Parse(row.Items.FindItemByKey("Docserial").Value.ToString())) as Doc;

                    if (right)
                    {
                        if (doc.Checkedstatus == "Y" && doc.Validstatus == "Y")
                        {

                            if (_facade == null)
                            {
                                _facade = new SystemSettingFacade(this.DataProvider);
                            }
                            //object parameter = _facade.GetParameter("DOCDIRPATH", "DOCDIRPATHGROUP");
                            //if (parameter != null)
                            //{
                            //服务器目录路径
                            //string filePath = ((Domain.BaseSetting.Parameter)parameter).ParameterAlias;
                            string filePath = System.AppDomain.CurrentDomain.BaseDirectory
+ "FileUpload";
                            if (filePath.LastIndexOf('\\') == filePath.Length - 1)
                            {
                                filePath = filePath.Substring(0, filePath.Length - 1);
                            }

                            #region//下载文件
                            FileInfo currentFile = new FileInfo(filePath + "/" + row.Items.FindItemByKey("ServerFullName").Value.ToString());
                            if (currentFile.Exists)
                            {
                                this.DownloadFileFull(this.VirtualHostRoot + "FileUpload/" + row.Items.FindItemByKey("ServerFullName").Value.ToString());
                            }
                            else
                            {
                                WebInfoPublish.PublishInfo(this, "$Error_QueryFile_NotExist", this.languageComponent1);
                            }
                            #endregion
                            //}
                            //else
                            //{
                            //    WebInfoPublish.PublishInfo(this, "$Error_DocDirPath_NotExist", this.languageComponent1);
                            //}

                        }
                        else
                        {
                            WebInfoPublish.PublishInfo(this, "$Error_FileIsNotCheckedOrValid", this.languageComponent1);
                        }
                    }
                    else
                    {
                        WebInfoPublish.PublishInfo(this, "$Error_HaveNoRightToDownload", this.languageComponent1);
                    }
                }
            }
            catch (Exception ex)
            {
                WebInfoPublish.PublishInfo(this, "$Error_QueryFile_Exception", this.languageComponent1);
            }
        }

        protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
        {

            this.excelExporter.Export();
        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("../BenQGuru.eMES.Web.IQC/FIQCCheckResultDetailMP.aspx", new string[] { "iqcno" }, new string[] { this.ViewState["IQCNO"].ToString() }));

        }
        #endregion

        #region private method

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
        }

        private int GetRowCount()
        {

            if (_documentFacade == null) { _documentFacade = new DocumentFacade(this.DataProvider); }
            return this._documentFacade.QueryDocumentsCount(FormatHelper.CleanString(this.txtDocnameQuery.Text.ToUpper()),//Jarvis
                                                            FormatHelper.CleanString(this.txtDocnumQuery.Text.ToUpper()),//Jarvis
                                                            FormatHelper.CleanString(this.txtItemlistQuery.Text),
                                                            FormatHelper.CleanString(this.txtOplistQuery.Text),
                                                            FormatHelper.CleanString(this.txtKeywordQuery.Text.ToUpper()),//Jarvis
                                                            FormatHelper.CleanString(this.txtMemoQuery.Text.ToUpper()),//Jarvis
                                                            FormatHelper.CleanString(this.drpDoctypeQuery.SelectedValue),
                                                            FormatHelper.CleanString(this.drpValidStatusQuery.SelectedValue),
                                                            FormatHelper.CleanString(this.drpCheckedStatusQuery.SelectedValue));
        }

        private void InitHander()
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);

            this.buttonHelper = new ButtonHelper(this);

            this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);

            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
        }

        private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
        {
            this.gridHelper.GridBind(this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
        }

        private void InitParameters()
        {
            if (this.Request.Params["MaterialCode"] == null)
            {
                this.ViewState["MaterialCode"] = string.Empty;
                this.ViewState["IQCNO"] = string.Empty;
                this.cmdReturn.Disabled = true;
            }
            else
            {
                this.ViewState["MaterialCode"] = this.Request.Params["MaterialCode"];
                this.ViewState["IQCNO"] = this.Request.Params["IQCNO"];
                this.txtItemlistQuery.Text = this.ViewState["MaterialCode"].ToString();
                this.cmdReturn.Disabled = false;
                this.cmdQuery_ServerClick(null, null);
            }


        }

        public void InitButton()
        {
            this.buttonHelper.AddDeleteConfirm();
        }

        private void InitWebGrid()
        {
            // base.InitWebGrid();
            this.gridHelper.AddColumn("Docserial", "文件序号", null);
            this.gridHelper.AddColumn("Dirserial", "目录序号", null);
            this.gridHelper.AddColumn("Docname", "文件名称", null);
            this.gridHelper.AddColumn("Docnum", "文件编号", null);
            this.gridHelper.AddColumn("Docver", "版本", null);
            this.gridHelper.AddColumn("DirName", "文档目录", null);
            this.gridHelper.AddColumn("ITEMLIST", "产品编号", null);//Jarvis
            //this.gridHelper.AddColumn("Mcodelist", "物料编码", null);
            this.gridHelper.AddColumn("Oplist", "工序", null);
            this.gridHelper.AddColumn("Docchgnum", "更改单号", null);
            this.gridHelper.AddColumn("Docchgfile", "更改文件", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("Keyword", "关键字", null);
            this.gridHelper.AddColumn("Doctype", "文档类型", null);
            this.gridHelper.AddColumn("CHECKEDSTATUS", "审核是否通过", null);
            this.gridHelper.AddColumn("VALIDATESTATUS", "是否有效", null);
            this.gridHelper.AddColumn("Upfiledate", "上传日期", null);
            this.gridHelper.AddColumn("Upuser", "上传人", null);
            this.gridHelper.AddColumn("Mdate", "最后编辑日期", null);
            this.gridHelper.AddColumn("Muser", "最后编辑人", null);
            this.gridHelper.AddColumn("Mtime", "最后编辑时间", null);
            this.gridHelper.AddColumn("ServerFullName", "服务器文件名", null);

            this.gridWebGrid.Columns.FromKey("Docserial").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Dirserial").Hidden = true;
            this.gridWebGrid.Columns.FromKey("Mtime").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ServerFullName").Hidden = true;

            this.gridHelper.AddDefaultColumn(false, false);
            ((BoundDataField)this.gridHelper.Grid.Columns.FromKey("Docname")).CssClass = "tdDocument";

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

        }

        protected DataRow GetGridRow(object obj)
        {
            //Infragistics.WebUI.UltraWebGrid.UltraGridRow row = new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //                    ((DocForQuery)obj).Docserial.ToString(),
            //                    ((DocForQuery)obj).Dirserial.ToString(),
            //                    ((DocForQuery)obj).Docname.ToString(),
            //                    ((DocForQuery)obj).Docnum.ToString(),
            //                    ((DocForQuery)obj).Docver.ToString(),
            //                    ((DocForQuery)obj).DirName.ToString(),
            //                    ((DocForQuery)obj).Itemlist.ToString(),
            //                    ((DocForQuery)obj).Oplist.ToString(),
            //                    ((DocForQuery)obj).Docchgnum.ToString(),
            //                    ((DocForQuery)obj).Docchgfile.ToString(),
            //                    ((DocForQuery)obj).Memo.ToString(),
            //                    ((DocForQuery)obj).Keyword.ToString(),
            //                    ((DocForQuery)obj).Doctype.ToString(),
            //                    languageComponent1.GetString(((Doc)obj).Checkedstatus),
            //                    languageComponent1.GetString(((Doc)obj).Validstatus),
            //                    FormatHelper.ToDateString(((DocForQuery)obj).Upfiledate),
            //                    ((DocForQuery)obj).GetDisplayText("Upuser"),
            //                    FormatHelper.ToDateString(((DocForQuery)obj).Mdate),
            //                    ((DocForQuery)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToTimeString(((DocForQuery)obj).Mtime),
            //                    ((DocForQuery)obj).ServerFileName});

            DataRow row = this.DtSource.NewRow();
            row["Docserial"] = ((DocForQuery)obj).Docserial.ToString();
            row["Dirserial"] = ((DocForQuery)obj).Dirserial.ToString();
            row["Docname"] = ((DocForQuery)obj).Docname.ToString();
            row["Docnum"] = ((DocForQuery)obj).Docnum.ToString();
            row["Docver"] = ((DocForQuery)obj).Docver.ToString();
            row["DirName"] = ((DocForQuery)obj).DirName.ToString();
            row["ITEMLIST"] = ((DocForQuery)obj).Itemlist.ToString();
            row["Oplist"] = ((DocForQuery)obj).Oplist.ToString();
            row["Docchgnum"] = ((DocForQuery)obj).Docchgnum.ToString();
            row["Docchgfile"] = ((DocForQuery)obj).Docchgfile.ToString();
            row["Memo"] = ((DocForQuery)obj).Memo.ToString();
            row["Keyword"] = ((DocForQuery)obj).Keyword.ToString();
            row["Doctype"] = ((DocForQuery)obj).Doctype.ToString();
            row["CHECKEDSTATUS"] = languageComponent1.GetString(((Doc)obj).Checkedstatus);
            row["VALIDATESTATUS"] = languageComponent1.GetString(((Doc)obj).Validstatus);
            row["Upfiledate"] = FormatHelper.ToDateString(((DocForQuery)obj).Upfiledate);
            row["Upuser"] = ((DocForQuery)obj).GetDisplayText("Upuser");
            row["Mdate"] = FormatHelper.ToDateString(((DocForQuery)obj).Mdate);
            row["Muser"] = ((DocForQuery)obj).GetDisplayText("MaintainUser");
            row["Mtime"] = FormatHelper.ToTimeString(((DocForQuery)obj).Mtime);
            row["ServerFullName"] = ((DocForQuery)obj).ServerFileName;
            return row;

        }




        private object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_documentFacade == null) { _documentFacade = new DocumentFacade(this.DataProvider); }
            return this._documentFacade.QueryDocuments(FormatHelper.CleanString(this.txtDocnameQuery.Text.ToUpper()),//Jarvis
                                                        FormatHelper.CleanString(this.txtDocnumQuery.Text.ToUpper()),//Jarvis
                                                        FormatHelper.CleanString(this.txtItemlistQuery.Text),
                                                        FormatHelper.CleanString(this.txtOplistQuery.Text),
                                                        FormatHelper.CleanString(this.txtKeywordQuery.Text.ToUpper()),//Jarvis
                                                        FormatHelper.CleanString(this.txtMemoQuery.Text.ToUpper()),//Jatvis
                                                        FormatHelper.CleanString(this.drpDoctypeQuery.SelectedValue),
                                                        FormatHelper.CleanString(this.drpValidStatusQuery.SelectedValue),
                                                        FormatHelper.CleanString(this.drpCheckedStatusQuery.SelectedValue),
                                                        inclusive,
                                                        exclusive
                                                        );
        }

        private string[] FormatExportRecord(object obj)
        {
            return new string[]{
                ((DocForQuery)obj).Docname.ToString(),
                ((DocForQuery)obj).Docnum.ToString(),
                ((DocForQuery)obj).Docver.ToString(),
                ((DocForQuery)obj).DirName.ToString(),
                ((DocForQuery)obj).Itemlist.ToString(),
                ((DocForQuery)obj).Oplist.ToString(),
                ((DocForQuery)obj).Docchgnum.ToString(),
                ((DocForQuery)obj).Docchgfile.ToString(),
                ((DocForQuery)obj).Memo.ToString(),
                ((DocForQuery)obj).Keyword.ToString(),
                ((DocForQuery)obj).Doctype.ToString(),
                languageComponent1.GetString(((DocForQuery)obj).Checkedstatus),
                languageComponent1.GetString(((DocForQuery)obj).Validstatus),
                ((DocForQuery)obj).Upfiledate.ToString(),
                ((DocForQuery)obj).GetDisplayText("Upuser"),
                ((DocForQuery)obj).Mdate.ToString(),
                ((DocForQuery)obj).GetDisplayText("MaintainUser"),
                ((DocForQuery)obj).Mtime.ToString()
            };
        }

        private string[] GetColumnHeaderText()
        {
            return new string[] {	
                                    "Docname",
                                    "Docnum",
                                    "Docver",
                                    "DirName",
                                    "Itemlist",
                                    "Oplist",
                                    "Docchgnum",
                                    "Docchgfile",
                                    "Memo",
                                    "Keyword", 
                                    "Doctype",
                                    "Checkedstatus",
                                    "Validstatus",
                                    "Upfiledate",
                                    "Upuser",
                                    "Mdate",
                                    "Muser",
                                    "Mtime"
            };
        }

        private object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }

        private void BuildParentDocTypeList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.drpDoctypeQuery);
            if (_facade == null)
            {
                _facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
            }
            builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllDocType);
            builder.Build("ParameterAlias", "ParameterAlias");

            this.drpDoctypeQuery.Items.Insert(0, "");
        }



        #endregion


    }
}
