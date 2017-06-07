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
using System.IO;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.MutiLanguage;
using BenQGuru.eMES.Domain.Document;
using Infragistics.WebUI.UltraWebNavigator;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FModuleMP 的摘要说明。
    /// </summary>
    public partial class FFileUpLoadMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected SystemSettingFacade _facade = null;//new SystemSettingFacadeFactory().Create();
        protected DocumentFacade _DocumentFacade = null;
        protected UpdatePanel UpdatePanel1;
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
            e.Row.Items.FindItemByKey("DOCNAME").CssClass = "LinkFontBlue";
        }
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
            //this.treeModule.NodeClicked += new Infragistics.WebUI.UltraWebNavigator.NodeClickedEventHandler(this.treeModule_NodeClicked);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

            // 
            // excelExporter
            // 
            //this.excelExporter.FileExtension = "xls";
            //this.excelExporter.LanguageComponent = this.languageComponent1;
            //this.excelExporter.Page = this;
            //this.excelExporter.RowSplit = "\r\n";

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {

            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                // 构建Module树
                this.BuildDocumentTree(false);

                this.treeDocument.ParentNodeImageUrl = string.Format("{0}skin/image/treenode2.gif", this.VirtualHostRoot);
                this.treeDocument.LeafNodeImageUrl = string.Format("{0}skin/image/treenode2.gif", this.VirtualHostRoot);

                BuildParentDocTypeList();
                //this.gridWebGrid.DataBind();
                //this.gridHelper.RequestData();

                this.txtDocDirQuery.Text = "0";
                this.HiddenCheckedstatus.Text = "N";


                BuildValidStatus();

            }
        }

        //protected override void AddParsedSubObject(object obj)
        //{
        //    needUpdatePanel = false;
        //    base.AddParsedSubObject(obj);
        //}

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(base.DataProvider);
            }
            this.DataProvider.BeginTransaction();

            this._DocumentFacade.AddDOC((Doc)domainObject);
            if (UpLoadFile("ADD", (Doc)domainObject))
            {
                this.DataProvider.CommitTransaction();
            }
            else
            {
                this.DataProvider.RollbackTransaction();
            }
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(base.DataProvider);
            }

            try
            {
                this._DocumentFacade.DeleteDOC((Doc[])domainObjects.ToArray(typeof(Doc)));

                string filaPath = "";
                if (_facade == null)
                {
                    _facade = new SystemSettingFacade(this.DataProvider);
                }
                //object parameter = _facade.GetParameter("DOCDIRPATH", "DOCDIRPATHGROUP");

                //if (parameter != null)
                //{
                //服务器目录路径
                filaPath = System.AppDomain.CurrentDomain.BaseDirectory
+ "FileUpload"; //((Domain.BaseSetting.Parameter)parameter).ParameterAlias;
                //}

                DirectoryInfo dir = new DirectoryInfo(filaPath);

                foreach (FileInfo file in dir.GetFiles())
                {
                    foreach (Doc doc in domainObjects)
                    {
                        if (file.Name == doc.ServerFileName)
                        {
                            file.Delete();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                WebInfoPublish.Publish(this, ex.Message, this.languageComponent1);
            }
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            //this.CheckParentModule(this.drpParentModuleCodeEdit.SelectedValue);
            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(base.DataProvider);
            }
            this.DataProvider.BeginTransaction();

            this._DocumentFacade.UpdateDOC((Doc)domainObject);

            if (UpLoadFile("EDIT", (Doc)domainObject))
            {
                this.DataProvider.CommitTransaction();
            }
            else
            {
                this.DataProvider.RollbackTransaction();
            }
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.chbFileCheckedEdit.Checked = true;
                this.ViewState["actionType"] = "ADD";
            }


            if (pageAction == PageActionType.Update)
            {
                this.chbFileCheckedEdit.Checked = false;
                //this.fileUpload.Disabled = false;
                this.chbFileCheckedEdit.Enabled = true;
                this.ViewState["actionType"] = "UPDATE";
            }


            if (pageAction == PageActionType.Cancel)
            {

                this.ViewState["actionType"] = "";
            }
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            try
            {
                if (commandName == "DOCNAME")
                {
                    if (_DocumentFacade == null)
                    {
                        _DocumentFacade = new DocumentFacade(this.DataProvider);
                    }
                    if (_DocumentFacade.GetDocDirRight(int.Parse(this.txtDocDirQuery.Text.Trim()), this.GetUserCode(), "QUERY"))
                    {

                        //下载文件
                        if (_facade == null)
                        {
                            _facade = new SystemSettingFacade(this.DataProvider);
                        }

                        string filePath = System.AppDomain.CurrentDomain.BaseDirectory
+ "FileUpload";
                        if (filePath.LastIndexOf('\\') == filePath.Length - 1)
                        {
                            filePath = filePath.Substring(0, filePath.Length - 1);
                        }


                        #region//下载文件
                        FileInfo currentFile = new FileInfo(filePath + "\\" + row.Items.FindItemByKey("ServerFullName").Value.ToString());
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
                        WebInfoPublish.PublishInfo(this, "$Error_No_WatchFile_Right", this.languageComponent1);
                    }
                }
            }
            catch (Exception ex)
            {
                WebInfoPublish.PublishInfo(this, "$Error_QueryFile_Exception", this.languageComponent1);
            }
        }

        private bool UpLoadFile(string action, Doc doc)
        {


            //如果选中文件
            if (this.chbFileCheckedEdit.Checked)
            {
                //if (this.fileUpLoadEdit.HasFile)
                //{
                try
                {
                    if (_facade == null)
                    {
                        _facade = new SystemSettingFacade(this.DataProvider);
                    }
                    //object parameter = _facade.GetParameter("DOCDIRPATH", "DOCDIRPATHGROUP");
                    //if (parameter != null)
                    //{
                    //服务器目录路径
                    string filePath = System.AppDomain.CurrentDomain.BaseDirectory
+ "FileUpload"; //((Domain.BaseSetting.Parameter)parameter).ParameterAlias;

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    if (filePath.LastIndexOf('\\') == filePath.Length - 1)
                    {
                        filePath = filePath.Substring(0, filePath.Length - 1);
                    }

                    int maxDocSerial = -1;
                    if (action == "ADD")
                    {
                        //文档的序号
                        maxDocSerial = _DocumentFacade.GetDocMaxSerial();
                    }
                    else
                    {
                        maxDocSerial = doc.Docserial;
                    }

                    Doc objDoc = _DocumentFacade.GetDOC(maxDocSerial) as Doc;

                    if (objDoc != null)
                    {
                        DirectoryInfo dir = new DirectoryInfo(filePath);

                        foreach (FileInfo file in dir.GetFiles())
                        {
                            if (file.Name == (objDoc as Doc).ServerFileName)
                            {
                                file.Delete();
                            }
                        }

                        /// '检查文件扩展名字 
                        HttpPostedFile postedFile = fileUpload.PostedFile;
                        string fileName;
                        fileName = System.IO.Path.GetFileName(postedFile.FileName);
                        if (fileName != "")
                        {
                            string serverFullName = fileName.Substring(0, fileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileName.Substring(fileName.LastIndexOf(".")); ;
                            objDoc.ServerFileName = serverFullName;
                            objDoc.Checkedstatus = "N";
                            _DocumentFacade.UpdateDOC(objDoc);
                            string currentPath = filePath + "\\" + serverFullName;
                            postedFile.SaveAs(currentPath);
                        }



                        WebInfoPublish.PublishInfo(this, "$Success_UpLoadFile", this.languageComponent1);

                    }
                    return true;
                    //}
                    //else
                    //{
                    //    WebInfoPublish.PublishInfo(this, "$Error_DocDirPath_NotExist", this.languageComponent1);
                    //    return false;
                    //}
                }
                catch (Exception ex)
                {
                    WebInfoPublish.PublishInfo(this, "$Error_UpLoadFile_Exception", this.languageComponent1);
                    return false;
                }
                //}
            }
            return true;
        }



        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("DOCSERIAL", "文件序号", null);
            this.gridHelper.AddColumn("DIRSERIAL", "目录编号", null);
            this.gridHelper.AddColumn("FileNAME", "文件名称", null);
            this.gridHelper.AddLinkColumn("DOCNAME", "文件名称", null);
            this.gridHelper.AddColumn("DOCNUM", "文件编号", null);
            this.gridHelper.AddColumn("DOCVER", "文件版本", null);
            this.gridHelper.AddColumn("ITEMLIST", "产品编号", null);
            this.gridHelper.AddColumn("OPLIST", "工序代码", null);
            this.gridHelper.AddColumn("DOCCHGNUM", "更改单号", null);
            this.gridHelper.AddColumn("DOCCHGFILE", "更改文件", null);
            this.gridHelper.AddColumn("MEMO", "备注", null);
            this.gridHelper.AddColumn("KEYWORD", "关键字", null);
            this.gridHelper.AddColumn("DOCTYPE", "文件类型", null);
            this.gridHelper.AddColumn("CHECKEDSTATUS", "审核是否通过", null);
            this.gridHelper.AddColumn("VALIDATESTATUS", "是否有效", null);
            this.gridHelper.AddColumn("UPFILEDATE", "上传时间", null);
            this.gridHelper.AddColumn("UPUSER", "上传人", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddColumn("ServerFullName", "服务器文件名", null);
            this.gridHelper.AddDefaultColumn(true, true);

            this.gridWebGrid.Columns.FromKey("DOCSERIAL").Hidden = true;
            this.gridWebGrid.Columns.FromKey("DIRSERIAL").Hidden = true;
            this.gridWebGrid.Columns.FromKey("FileNAME").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ServerFullName").Hidden = true;
            //((BoundDataField)this.gridHelper.Grid.Columns.FromKey("DOCNAME")).HtmlEncode = false;
            ((BoundDataField)this.gridHelper.Grid.Columns.FromKey("DOCNAME")).CssClass = "tdDocument";
            //((BoundDataField)this.gridHelper.Grid.Columns.FromKey("DOCNAME")).HtmlEncode = false;
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();
        }

        protected override DataRow GetGridRow(object obj)
        {

            DataRow row = this.DtSource.NewRow();
            row["DOCSERIAL"] = ((Doc)obj).Docserial;
            row["DIRSERIAL"] = ((Doc)obj).Dirserial;
            row["FileNAME"] = ((Doc)obj).Docname;
            //row["DOCNAME"] = string.Format("<a href='{0}FDownload.aspx?fileName={0}FileUpload/{1}'>{2}</a>", this.VirtualHostRoot, ((Doc)obj).ServerFileName, ((Doc)obj).Docname);
            row["DOCNAME"] = ((Doc)obj).Docname;
            row["DOCNUM"] = ((Doc)obj).Docnum;
            row["DOCVER"] = ((Doc)obj).Docver;
            row["ITEMLIST"] = ((Doc)obj).Itemlist;
            row["OPLIST"] = ((Doc)obj).Oplist;
            row["DOCCHGNUM"] = ((Doc)obj).Docchgnum;
            row["DOCCHGFILE"] = ((Doc)obj).Docchgfile;
            row["MEMO"] = ((Doc)obj).Memo;
            row["KEYWORD"] = ((Doc)obj).Keyword;
            row["DOCTYPE"] = ((Doc)obj).Doctype;
            row["CHECKEDSTATUS"] = languageComponent1.GetString(((Doc)obj).Checkedstatus);
            row["VALIDATESTATUS"] = languageComponent1.GetString(((Doc)obj).Validstatus);
            row["UPFILEDATE"] = FormatHelper.ToDateString(((Doc)obj).Upfiledate);
            row["UPUSER"] = ((Doc)obj).GetDisplayText("Upuser");
            row["MaintainUser"] = ((Doc)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Doc)obj).Mdate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Doc)obj).Mtime);
            row["ServerFullName"] = ((Doc)obj).ServerFileName;
            return row;

        }

        private string GetRCardLink(string no)
        {
            //string url = string.Format("../WebQuery/FOQCLotSampleQP.aspx?reworkrcard={0}", this.Server.UrlEncode(no));
            return string.Format("<a href=#>{0}</a>", no);
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(base.DataProvider);
            }
            return this._DocumentFacade.QueryDocuments(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDocDirQuery.Text)), inclusive, exclusive);

        }

        protected override int GetRowCount()
        {
            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(base.DataProvider);
            }
            return this._DocumentFacade.QueryDocumentsCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtDocDirQuery.Text)));
        }
        #endregion

        #region Object <--> Page
        /// <summary>
        /// 将指定行的记录写入编辑区
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected override object GetEditObject(GridRecord row)
        {
            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(base.DataProvider);
            }
            object obj = _DocumentFacade.GetDOC(int.Parse(row.Items.FindItemByKey("DOCSERIAL").Text.ToString()));

            if (obj != null)
            {
                return (Doc)obj;
            }

            return null;
        }

        /// <summary>
        /// 由编辑区的输入获得DomainObject
        /// </summary>
        /// <returns></returns>
        protected override object GetEditObject()
        {
            //			this.ValidateInput();
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(base.DataProvider);
            }
            Doc doc = this._DocumentFacade.CreateNewDOC();
            if (this.txtDocDirQuery.Text.Trim() != string.Empty)
            {
                doc.Dirserial = int.Parse(this.txtDocDirQuery.Text.Trim());
            }
            else
            {
                doc.Dirserial = 0;
            }
            if (this.txtDocSerialEdit.Text.Trim() == string.Empty)
            {
                doc.Docserial = 0;
            }
            else
            {
                doc.Docserial = int.Parse(this.txtDocSerialEdit.Text.Trim());
            }
            doc.Docname = FormatHelper.CleanString(this.txtDocNameEdit.Text, 100);
            doc.Docnum = FormatHelper.CleanString(this.txtDocNumEdit.Text, 100);
            doc.Doctype = FormatHelper.CleanString(this.drpDocTypeEdit.Text, 40);
            doc.Docver = FormatHelper.CleanString(this.txtDocVerEdit.Text, 100);
            doc.Docchgfile = FormatHelper.CleanString(this.txtDocChgFileEdit.Text, 100);
            doc.Docchgnum = FormatHelper.CleanString(this.txtDocChgNumEdit.Text, 40);
            doc.Keyword = FormatHelper.CleanString(this.txtKeyWordEdit.Text, 2000);
            doc.Itemlist = FormatHelper.CleanString(this.txtItemCodeEdit.Text, 400);
            doc.Validstatus = FormatHelper.CleanString(this.drpValidStatus.SelectedValue, 1);
            doc.Checkedstatus = FormatHelper.CleanString(this.HiddenCheckedstatus.Text, 1);
            doc.Oplist = FormatHelper.CleanString(this.txtOpCodeEdit.Text, 400);
            doc.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 2000);
            doc.MaintainUser = this.GetUserCode();
            doc.Mdate = dbDateTime.DBDate;
            doc.Mtime = dbDateTime.DBTime;

            //如果是编辑，且编辑的时候没有勾选“文件”进行新的文件的上传，则不修改上传时间和上传人
            //也不清空ServerFullName(add by James)
            if (this.ViewState["actionType"].ToString() == "UPDATE")
            {
                doc.Upuser = this.HiddenUpuser.Text;
                doc.Upfiledate = FormatHelper.TODateInt(this.HiddenUpfiledate.Text);
                doc.ServerFileName = FormatHelper.CleanString(this.HiddenServerFullName.Text, 200);
            }
            else
            {
                doc.Upuser = this.GetUserCode();
                doc.Upfiledate = dbDateTime.DBDate;
            }

            return doc;
        }

        /// <summary>
        /// 将DomainObject写入编辑区，如果为null则全部置空
        /// </summary>
        /// <param name="item"></param>
        protected override void SetEditObject(Object obj)
        {
            if (obj == null)
            {
                this.txtDocNameEdit.Text = "";
                this.txtDocNumEdit.Text = "";
                this.drpDocTypeEdit.SelectedIndex = -1;
                this.txtDocVerEdit.Text = "";
                this.txtDocChgFileEdit.Text = "";
                this.txtDocChgNumEdit.Text = "";
                this.txtKeyWordEdit.Text = "";
                this.txtItemCodeEdit.Text = "";
                this.txtOpCodeEdit.Text = "";
                this.txtMemoEdit.Text = "";
                this.txtDocSerialEdit.Text = "";
                this.drpValidStatus.SelectedIndex = -1;
                this.HiddenCheckedstatus.Text = "N";
                this.HiddenUpuser.Text = "";
                this.HiddenUpfiledate.Text = "";
                this.HiddenServerFullName.Text = "";

                this.chbFileCheckedEdit.Enabled = true;
                //this.fileUpload.Disabled = false;
                return;
            }
            this.txtDocNameEdit.Text = ((Doc)obj).Docname.ToString();
            this.txtDocNumEdit.Text = ((Doc)obj).Docnum.ToString();
            this.txtDocVerEdit.Text = ((Doc)obj).Docver.ToString();
            this.txtDocChgFileEdit.Text = ((Doc)obj).Docchgfile.ToString();
            this.txtDocChgNumEdit.Text = ((Doc)obj).Docchgnum.ToString();
            this.txtKeyWordEdit.Text = ((Doc)obj).Keyword.ToString();
            this.txtItemCodeEdit.Text = ((Doc)obj).Itemlist.ToString();
            this.txtOpCodeEdit.Text = ((Doc)obj).Oplist.ToString();
            this.txtMemoEdit.Text = ((Doc)obj).Memo.ToString();
            this.txtDocSerialEdit.Text = ((Doc)obj).Docserial.ToString();
            this.HiddenCheckedstatus.Text = ((Doc)obj).Checkedstatus.ToString();
            this.HiddenUpuser.Text = ((Doc)obj).Upuser.ToString();
            this.HiddenUpfiledate.Text = FormatHelper.ToDateString(((Doc)obj).Upfiledate);
            this.HiddenServerFullName.Text = ((Doc)obj).ServerFileName.ToString();

            //如果文档是已经为审核通过的，则编辑时不允许选择新文件进行更新上传
            if (((Doc)obj).Checkedstatus.ToString() == "Y")
            {
                this.chbFileCheckedEdit.Enabled = false;
                //this.fileUpload.Disabled = true;
            }
            else
            {
                this.chbFileCheckedEdit.Enabled = true;
                //this.fileUpload.Disabled = false;
            }
            try
            {
                this.drpValidStatus.SelectedValue = ((Doc)obj).Validstatus.ToString();
            }
            catch
            {
                this.drpValidStatus.SelectedIndex = -1;
            }
            try
            {
                this.drpDocTypeEdit.SelectedValue = ((Doc)obj).Doctype.ToString();
            }
            catch
            {
                this.drpDocTypeEdit.SelectedIndex = -1;
            }

        }

        protected override bool ValidateInput()
        {

            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(this.DataProvider);
            }

            if (!_DocumentFacade.GetDocDirRight(int.Parse(this.txtDocDirQuery.Text.Trim()), this.GetUserCode(), "UPLOAD"))
            {
                WebInfoPublish.PublishInfo(this, "$Error_No_UploadFile_Right", this.languageComponent1);
                return false;
            }
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblDocNameEdit, txtDocNameEdit, 40, true));
            manager.Add(new LengthCheck(lblDocNumEdit, txtDocNumEdit, 40, true));
            manager.Add(new NumberCheck(lblDocVerEdit, txtDocVerEdit, true));
            manager.Add(new LengthCheck(lblDocVerEdit, txtDocVerEdit, 40, true));
            manager.Add(new LengthCheck(lblMemoEdit, txtMemoEdit, 2000, true));
            manager.Add(new LengthCheck(lblKeyWordEdit, txtKeyWordEdit, 2000, true));
            manager.Add(new LengthCheck(lblFileTypeEdit, drpDocTypeEdit, 40, true));

            //if (this.txtDocVerEdit.Text.Trim().Length != 2)
            //{
            //    WebInfoPublish.PublishInfo(this, "$Error_Version_Length", this.languageComponent1);
            //    return false;
            //}

            if (this.cmdAdd.Disabled)
            {

                if (_DocumentFacade.CheckVertion(txtDocSerialEdit.Text.Trim(), txtDocVerEdit.Text, FormatHelper.CleanString(txtDocNameEdit.Text), FormatHelper.CleanString(txtDocNumEdit.Text)))
                {
                    WebInfoPublish.PublishInfo(this, "$Error_Version_Check", this.languageComponent1);
                    return false;
                }
            }
            else
            {
                if (_DocumentFacade.CheckVertion(txtDocVerEdit.Text, FormatHelper.CleanString(txtDocNameEdit.Text), FormatHelper.CleanString(txtDocNumEdit.Text)))
                {
                    WebInfoPublish.PublishInfo(this, "$Error_Version_Check", this.languageComponent1);
                    return false;
                }
            }


            if (!manager.Check())
            {
                WebInfoPublish.PublishInfo(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (this.chbFileCheckedEdit.Checked)
            {
                HttpPostedFile postedFile =fileUpload.PostedFile;
                if (postedFile.FileName.Trim() == string.Empty)
                {
                    WebInfoPublish.PublishInfo(this, "$Error_UploadFileIsEmpty", this.languageComponent1);
                    return false;
                }
            }


            return true;
        }

        #endregion

        #region Tree

        private ITreeObjectNode LoadDocumentTreeToApplication()
        {
            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(base.DataProvider);
            }
            return this._DocumentFacade.BuildDocumentTree();
        }

        /// <summary>
        /// 构建Document树
        /// </summary>
        /// <param name="reload">是否重新从数据库中读取Document树</param>
        private void BuildDocumentTree(bool reload)
        {
            this.treeDocument.Nodes.Clear();

            if (reload)
            {
                this.LoadDocumentTreeToApplication();
            }

            ITreeObjectNode node = LoadDocumentTreeToApplication();

            this.treeDocument.Nodes.Add(BuildTreeNode(node));

            LanguageWord lword = this.languageComponent1.GetLanguage("documentRoot");

            if (lword != null)
            {
                this.treeDocument.Nodes[0].Text = lword.ControlText;
            }

            //this.treeModule.ExpandAll();
            this.treeDocument.CollapseAll();
            if (this.treeDocument.SelectedNode != null)
            {
                Infragistics.WebUI.UltraWebNavigator.Node nodeParent = this.treeDocument.SelectedNode.Parent;
                while (nodeParent != null)
                {
                    nodeParent.Expand(false);
                    nodeParent = nodeParent.Parent;
                }
            }

            //this.BuildParentDocumentCodeList();
        }

        private Infragistics.WebUI.UltraWebNavigator.Node BuildTreeNode(ITreeObjectNode treeNode)
        {
            Infragistics.WebUI.UltraWebNavigator.Node node = new Node();

            node.Text = treeNode.Text;
            node.DataKey = treeNode.ID;

            node.Tag = treeNode;

            if (treeNode.Text == this.txtDocDirQuery.Text)
            {
                this.treeDocument.SelectedNode = node;
            }

            foreach (ITreeObjectNode subNode in treeNode.GetSubLevelChildrenNodes())
            {
                node.Nodes.Add(BuildTreeNode(subNode));
            }

            return node;
        }

        protected void treeDocument_NodeSelectionChanged(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this.txtDocDirQuery.Text = ((DocumentTreeNode)e.Node.Tag).docDir.Dirserial.ToString();
                DocumentTreeNode parentNode = (DocumentTreeNode)e.Node.Tag;
                string title = "";
                title = parentNode.docDir.Dirname;
                while (parentNode.docDir.Pdirserial != 0)
                {
                    parentNode = (DocumentTreeNode)parentNode.Parent;
                    title = parentNode.docDir.Dirname + "\\" + title;
                }
                if (String.IsNullOrEmpty(title))
                {
                    title = this.languageComponent1.GetLanguage("documentRoot").ControlText;
                }
                else
                {
                    title = this.languageComponent1.GetLanguage("documentRoot").ControlText + "\\" + title;
                }
                this.lblDocDirTitle.Text = title;

            }
            else
            {
                this.txtDocDirQuery.Text = "";
            }
            //if (_DocumentFacade == null)
            //{
            //    _DocumentFacade = new DocumentFacade(this.DataProvider);
            //}
            //if (_DocumentFacade.GetDocDirRight(int.Parse(((DocumentTreeNode)e.Node.Tag).docDir.Dirserial.ToString()), this.GetUserCode(), "CHECK"))
            //{
            //    cmdCheck.Disabled = false;
            //}
            //else
            //{
            //    cmdCheck.Disabled = true;
            //}


            this.gridHelper.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);

        }

        private bool CheckParentModule(string parentModuleCode)
        {
            if (parentModuleCode == "")
            {
                return true;
            }

            ITreeObjectNode node = LoadDocumentTreeToApplication().GetTreeObjectNodeByID(this.txtDocDirQuery.Text);

            if (node == null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Node_Lost");
            }

            TreeObjectNodeSet set = node.GetAllNodes();

            foreach (ITreeObjectNode childNode in set)
            {
                if (childNode.ID.ToUpper() == parentModuleCode.ToUpper())
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Parent_To_Children");
                }
            }

            return true;
        }

        #endregion

        #region 数据初始化
        private void BuildParentDocTypeList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.drpDocTypeEdit);
            if (_facade == null)
            {
                _facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
            }
            builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllDocType);
            builder.Build("ParameterAlias", "ParameterAlias");

            this.drpDocTypeEdit.Items.Insert(0, "");
        }

        private void BuildValidStatus()
        {
            this.drpValidStatus.Items.Add(new ListItem(this.languageComponent1.GetString("Y"), "Y"));
            this.drpValidStatus.Items.Add(new ListItem(this.languageComponent1.GetString("N"), "N"));
        }




        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {

            return new string[]{    
								((Doc)obj).Docname,
								((Doc)obj).Docnum.ToString(),
								((Doc)obj).Docver,
								((Doc)obj).Itemlist,
								((Doc)obj).Oplist,
								((Doc)obj).Docchgnum,
								((Doc)obj).Docchgfile,
                                ((Doc)obj).Memo,
                                ((Doc)obj).Keyword,
                                ((Doc)obj).Doctype,
                                languageComponent1.GetString(((Doc)obj).Checkedstatus),
                                languageComponent1.GetString(((Doc)obj).Validstatus),
                                ((Doc)obj).Upfiledate.ToString(),
                                ((Doc)obj).GetDisplayText("Upuser"),
								((Doc)obj).GetDisplayText("MaintainUser"),
								FormatHelper.ToDateString(((Doc)obj).Mdate),
								FormatHelper.ToTimeString(((Doc)obj).Mtime),
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
									"DocName",
									"DocNum",
									"DocVer",
									"ItemList",
									"OPList",
									"DocChgNum",
									"DocChgFile",
									"MEMO",
									"KeyWord",
									"DocType",
                                    "Checkedstatus",
                                    "Validstatus",
									"UPFileDate",
									"UPUser",
									"MaintainUser",
									"MaintainDate",
                                    "MaintainTime"};
        }
        #endregion
    }
}
