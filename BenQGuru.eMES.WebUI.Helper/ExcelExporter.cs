using System;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

using BenQGuru.eMES.Common.Domain;
using System.Data;
using System.Web.UI;

namespace BenQGuru.eMES.Web.Helper
{
    /// <summary>
    /// ExcelExporter 的摘要说明。
    /// </summary>
    /// 
    public delegate object[] LoadExportDataDelegate(int start, int end);
    public delegate object[] LoadExportDataDelegateNoPage();
    public delegate string[] FormatExportRecordDelegate(object record);
    public delegate string[] GetColumnHeaderTextDelegate();

    public class ExcelExporter : System.ComponentModel.Component
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public LoadExportDataDelegate LoadExportDataHandle = null;
        public LoadExportDataDelegateNoPage LoadExportDataNoPageHandle = null;
        public FormatExportRecordDelegate FormatExportRecordHandle = null;
        public GetColumnHeaderTextDelegate GetColumnHeaderTextHandle = null;

        private ExcelHelper excelHelper = new ExcelHelper();
        private DataTable ResultData = new DataTable();
        private ControlLibrary.Web.Language.LanguageComponent _languageComponent;

        [Bindable(true), Category("Action"), DefaultValue(""), Description("LanguageComponent")]
        public ControlLibrary.Web.Language.LanguageComponent LanguageComponent
        {
            get
            {
                return _languageComponent;
            }
            set
            {
                _languageComponent = value;

                if (_languageComponent == null)
                {
                    return;
                }

                string word = string.Empty;

                word = this._languageComponent.GetString("exportWindowTitle");
                if (word != string.Empty)
                {
                    this.exportWindowTitle = word;
                }

                word = this._languageComponent.GetString("exportDownloadText");
                if (word != string.Empty)
                {
                    this.downloadText = word;
                }

                word = this._languageComponent.GetString("exportNoDataAlert");
                if (word != string.Empty)
                {
                    this.noDataAlert = word;
                }
            }
        }

        public ExcelExporter(System.ComponentModel.IContainer container)
        {
            ///
            /// Windows.Forms 类撰写设计器支持所必需的
            ///
            container.Add(this);
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        public ExcelExporter()
        {
            ///
            /// Windows.Forms 类撰写设计器支持所必需的
            ///
            InitializeComponent();

            //
            // TODO: 在 InitializeComponent 调用后添加任何构造函数代码
            //
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion

        private string _downloadPath = @"\upload\";
        private string _fileExt = "xls";
        private string _cellSplit = "\t";
        private string _rowSplit = "\r\n";
        private System.Web.UI.Page _page = null;
        private object[] _exportData = null;
        private string exportWindowTitle = "Export Window";
        private string downloadText = "Download";
        private string noDataAlert = "No Data To Export!";
        private bool isPageExport = true;
        //		private ArrayList _downloadFileList = new ArrayList();

        [Bindable(true), Category("Action"), DefaultValue(@"\upload\"), Description("文件下载的相对路径")]
        public string DownloadRelativePath
        {
            get
            {
                return this._downloadPath;
            }
            set
            {
                this._downloadPath = value;
            }
        }

        [Browsable(false)]
        public string DownloadPhysicalPath
        {
            get
            {
                return string.Format(@"{0}\{1}\", _page.Request.PhysicalApplicationPath, _downloadPath.Trim('\\', '/').Replace('/', '\\'));
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

        [Browsable(false)]
        public string DownloadPath
        {
            get
            {
                return string.Format(@"{0}{1}/", this.PageVirtualHostRoot, _downloadPath.Trim('\\', '/').Replace('\\', '/'));
            }
        }

        [Bindable(true), Category("Action"), DefaultValue("\t"), Description("单元格之间的分隔符")]
        public string CellSplit
        {
            get
            {
                return this._cellSplit;
            }
            set
            {
                if (value.Trim().Length != 0)
                {
                    this._cellSplit = value;
                }
            }
        }

        [Bindable(true), Category("Action"), DefaultValue("\t"), Description("")]
        public string FileExtension
        {
            get
            {
                return this._fileExt;
            }
            set
            {
                this._fileExt = value;
            }
        }

        [Bindable(true), Category("Action"), DefaultValue("\n"), Description("每行之间的分割符")]
        public string RowSplit
        {
            get
            {
                return this._rowSplit;
            }
            set
            {
                if (value.Trim().Length != 0)
                {
                    this._rowSplit = value;
                }
            }
        }


        [Bindable(true), Category("Action"), DefaultValue(""), Description("ExcelExporter所属的页面")]
        public System.Web.UI.Page Page
        {
            get
            {
                return this._page;
            }
            set
            {
                this._page = value;
            }
        }

        private string[] FormatRecord(object record)
        {
            if (this.FormatExportRecordHandle != null)
            {
                string[] formatRecords = FormatExportRecordHandle(record);
                // Added by Icyer 2006/09/06
                if (formatRecords == null)
                    return formatRecords;
                // Added end
                for (int i = 0; i < formatRecords.Length; i++)
                {
                    if (formatRecords[i] == null) continue;
                    formatRecords[i] = formatRecords[i].Replace("\r\n", " ");
                    formatRecords[i] = formatRecords[i].Replace("\r", " ");
                    formatRecords[i] = formatRecords[i].Replace("\n", " ");
                }

                return formatRecords;
            }

            if (record is DomainObject)
            {
                ArrayList array = DomainObjectUtility.GetDomainObjectValues((DomainObject)record);

                ArrayList strArray = new ArrayList();

                foreach (object obj in array)
                {
                    if (obj == null)
                    {
                        strArray.Add("");
                    }
                    else
                    {
                        strArray.Add(obj.ToString());
                    }
                }

                return (string[])strArray.ToArray(typeof(string));
            }

            return new string[] { "" };
        }

        private string[] GetColumnHeaderText()
        {
            if (this.GetColumnHeaderTextHandle != null)
            {
                return this.GetColumnHeaderTextHandle();
            }

            if (this._exportData != null && this._exportData.Length != 0)
            {
                return (string[])DomainObjectUtility.GetDomainObjectScheme(this._exportData[0].GetType()).ToArray(typeof(string));
            }

            return new string[] { "" };
        }

        private object[] GetExportData(int start, int end)
        {
            if (this.LoadExportDataHandle != null)
            {
                this.isPageExport = true;
                return this.LoadExportDataHandle(start, end);
            }
            else if (this.LoadExportDataNoPageHandle != null)
            {
                this.isPageExport = false;
                return this.LoadExportDataNoPageHandle();
            }

            return null;
        }


        private StringBuilder builder = null;
        public string FormatExportString()
        {
            builder = new StringBuilder();

            string[] headerTexts = this.GetColumnHeaderText();
            string word = string.Empty;

            if (this.LanguageComponent != null)
            {
                for (int i = 0; i < headerTexts.Length; i++)
                {
                    word = this.LanguageComponent.GetString(headerTexts[i]);

                    if (word != string.Empty)
                    {
                        builder.Append(word);
                    }
                    else
                    {
                        builder.Append(headerTexts[i]);
                    }

                    if (i != headerTexts.Length - 1)
                    {
                        builder.Append(this.CellSplit);
                    }
                    else
                    {
                        builder.Append(this.RowSplit);
                    }
                }
            }
            else
            {
                builder.Append(string.Join(this.CellSplit, headerTexts) + this.RowSplit);
            }

            if (this._exportData != null)
            {
                foreach (object obj in this._exportData)
                {
                    // Added by Icyer 2006/09/06
                    string[] objRow = FormatRecord(obj);
                    if (objRow == null)
                        continue;
                    // Added end
                    builder.Append(string.Join(this.CellSplit, objRow) + this.RowSplit);
                }
            }

            return builder.ToString();
        }

        public string FormatExportString(bool buildHeaderText)
        {
            builder = new StringBuilder();

            string[] headerTexts;
            if (buildHeaderText)
            {
                headerTexts = this.GetColumnHeaderText();
            }
            else
            {
                headerTexts = null;
            }
            string word = string.Empty;
            if (buildHeaderText)
            {
                if (this.LanguageComponent != null)
                {
                    for (int i = 0; i < headerTexts.Length; i++)
                    {
                        word = this.LanguageComponent.GetString(headerTexts[i]);

                        if (word != string.Empty)
                        {
                            builder.Append(word);
                        }
                        else
                        {
                            builder.Append(headerTexts[i]);
                        }

                        if (i != headerTexts.Length - 1)
                        {
                            builder.Append(this.CellSplit);
                        }
                        else
                        {
                            builder.Append(this.RowSplit);
                        }
                    }
                }
                else
                {
                    builder.Append(string.Join(this.CellSplit, headerTexts) + this.RowSplit);
                }
            }

            if (this._exportData != null)
            {
                //foreach ( object obj in this._exportData )
                for (int i = 0; i < this._exportData.Length; i++)
                {
                    object obj = this._exportData[i];
                    // Added by Icyer 2006/09/06
                    string[] objRow = FormatRecord(obj);
                    if (objRow == null)
                        continue;
                    // Added end
                    builder.Append(string.Join(this.CellSplit, objRow) + this.RowSplit);
                }
            }

            return builder.ToString();
        }

        public void FormatExportStringForNew(bool buildHeaderText)
        {
            StringBuilder columnBuilder = new StringBuilder();
            string[] headerTexts;

            if (buildHeaderText)
            {
                headerTexts = this.GetColumnHeaderText();
                for (int i = 0; i < headerTexts.Length; i++)
                {
                    ResultData.Columns.Add(headerTexts[i], typeof(string));
                }
                //每列的关键key
                excelHelper.ColumnFields = this.GetColumnHeaderText();

            }
            else
            {
                headerTexts = null;
            }
            string word = string.Empty;
            if (buildHeaderText)
            {
                if (this.LanguageComponent != null)
                {
                    for (int i = 0; i < headerTexts.Length; i++)
                    {
                        word = this.LanguageComponent.GetString(headerTexts[i]);

                        if (word != string.Empty)
                        {
                            columnBuilder.Append(word);
                        }
                        else
                        {
                            columnBuilder.Append(headerTexts[i]);
                        }

                        if (i != headerTexts.Length - 1)
                        {
                            columnBuilder.Append(",");
                        }
                    }
                }

                string[] colunmSCaption = columnBuilder.ToString().Split(',');
                //表头的Caption
                excelHelper.ColumnNames = colunmSCaption;
            }

            //excel的数据源
            if (this._exportData != null)
            {
                foreach (object obj in this._exportData)
                {
                    string[] objRow = FormatRecord(obj);
                    if (objRow == null)
                    {
                        continue;
                    }

                    DataRow rowLine = ResultData.NewRow();
                    for (int i = 0; i < objRow.Length; i++)
                    {
                        rowLine[i] = objRow[i];
                    }
                    ResultData.Rows.Add(rowLine);
                }
            }
        }

        // Added by Icyer 2006/09/06
        public void AppendRow(string[] row)
        {
            if (builder == null)
                return;
            builder.Append(string.Join(this.CellSplit, row) + this.RowSplit);
        }
        // Added end

        private bool _big5 = false;


        public void Export()
        {
            /*added by jessie lee, 2005/12/14
                 * 操作时间过长时添加进度条*/
            //_page.Response.Write("<script language=javascript src=" + this.PageVirtualHostRoot + "Skin/JS/selectall.js></script>\n");
            //_page.Response.Write("<div id='mydiv' >");
            //_page.Response.Write("_");
            //_page.Response.Write("</div>");
            //_page.Response.Write("<script>mydiv.innerText = '';</script>");
            //_page.Response.Write("<script language=javascript>;");
            //_page.Response.Write("var dots = 0;var dotmax = 10;function ShowWait()");
            //_page.Response.Write("{var output; output = '" + LanguageComponent.GetString("$Message_Loading") + "';dots++;if(dots>=dotmax)dots=1;");
            //_page.Response.Write("for(var x = 0;x < dots;x++){output += '·';}mydiv.innerText =  output;}");
            //_page.Response.Write("function StartShowWait(){mydiv.style.visibility = 'visible'; ");
            //_page.Response.Write("window.setInterval('ShowWait()',1000);}");
            //_page.Response.Write("function HideWait(){mydiv.style.display = 'none';");
            //_page.Response.Write("window.clearInterval();}");
            //_page.Response.Write("StartShowWait();</script>");
            //_page.Response.Flush();
            this.LanguageComponent = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            this.LanguageComponent.Language = "CHS";
           
            try
            {
                this.XlsExport();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //_page.Response.Write("<script language=javascript>HideWait();</script>");
            }
        }

        public void XlsExport()
        {
            if (!Directory.Exists(this.DownloadPhysicalPath))
            {
                Directory.CreateDirectory(this.DownloadPhysicalPath);
            }

            string filename = string.Format("Export_{0}_{1}.{2}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString(), this.FileExtension);
            string filepath = string.Format(@"{0}{1}", this.DownloadPhysicalPath, filename);

            while (File.Exists(filepath))
            {
                filename = string.Format("{0}_{1}.{2}", filename, "0", ".xls");
                filepath = string.Format(@"{0}{1}", this.DownloadPhysicalPath, filename);
            }

            int startRow = 1;
            int endRow = 100000;
            object[] exportDatas = null;
            int exportDataCount = 0;

            do
            {
                exportDatas = this.GetExportData(startRow, endRow);
                this._exportData = exportDatas;
                if (startRow == 1)
                {
                    this.FormatExportStringForNew(true);
                }
                else
                {
                    this.FormatExportStringForNew(false);
                }
                if (exportDatas != null)
                {
                    exportDataCount = exportDatas.Length;
                }
                else
                {
                    exportDataCount = 0;
                }
                this._exportData = null;
                exportDatas = null;
                startRow += 100000;
                endRow += 100000;
            } while (this.isPageExport == true && exportDataCount == 100000);

            excelHelper.DataSource = ResultData;
            excelHelper.SaveAs(filepath);

            string strSript = @" var frameDown =$('<a></a>');
            frameDown.appendTo($('form'));
            //frameDown.attr('target','_blank');
            frameDown.html('<span></span>');
            frameDown.attr('href', '"
             + string.Format(@"{0}FDownload.aspx", this.PageVirtualHostRoot)
             + "?fileName=" + string.Format(@"{0}{1}", this.DownloadPath, filename)
             + @"');
            frameDown.children().click();
            frameDown.remove();";
            this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), Guid.NewGuid().ToString(), strSript, true);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strSript, true);
        }

        public void Export(object[] exportData)
        {
            this._exportData = exportData;

            if (_exportData == null)
            {
                _page.Response.Write(string.Format("<script language=javascript>alert('{0}');window.name+='[back]';history.back();</script>", this.noDataAlert));

                return;
            }

            if (!Directory.Exists(this.DownloadPhysicalPath))
            {
                Directory.CreateDirectory(this.DownloadPhysicalPath);
            }

            string filename = string.Format("Export_{0}_{1}.{2}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString(), this.FileExtension);
            string filepath = string.Format(@"{0}{1}", this.DownloadPhysicalPath, filename);

            while (File.Exists(filepath))
            {
                filename = string.Format("{0}_{1}.{2}", filename, "0", ".xls");
                filepath = string.Format(@"{0}{1}", this.DownloadPhysicalPath, filename);
            }
            //			this._downloadFileList.Add( filepath );

            StreamWriter writer = new StreamWriter(filepath, false, System.Text.Encoding.Unicode);
            writer.Write(this.FormatExportString());
            writer.Flush();
            writer.Close();

            //_page.Response.Write(@"<script language=javascript>window.top.document.getElementById('iframeDownload').src='" 
            //    + string.Format(@"{0}FDownload.aspx", this.PageVirtualHostRoot)
            //    + "?fileName=" + string.Format(@"{0}{1}", this.DownloadPath, filename)
            //    + @"';</script>");

            _page.Response.Write(@"<iframe width='0' height='0' src="
                + string.Format(@"{0}FDownload.aspx", this.PageVirtualHostRoot)
                + "?fileName=" + string.Format(@"{0}{1}", this.DownloadPath, filename)
                + "></iframe>"
                );
        }
    }
}
