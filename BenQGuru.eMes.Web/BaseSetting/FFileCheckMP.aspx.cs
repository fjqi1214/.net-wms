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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
using Infragistics.WebUI.UltraWebNavigator;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FModuleMP 的摘要说明。
    /// </summary>
    public partial class FFileCheckMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected SystemSettingFacade _facade = null;//new SystemSettingFacadeFactory().Create();
        protected DocumentFacade _DocumentFacade = null;

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
            //this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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
                this.txtDocDirQuery.Text = "0";

            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region Button

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(base.DataProvider);
            }

            try
            {
                this._DocumentFacade.DeleteDOC((Doc[])domainObjects.ToArray(typeof(Doc)));

                //string filaPath = "";
                //if (_facade == null)
                //{
                //    _facade = new SystemSettingFacade(this.DataProvider);
                //}
                //object parameter = _facade.GetParameter("DOCDIRPATH", "DOCDIRPATHGROUP");

                //if (parameter != null)
                //{
                //    //服务器目录路径
                //    filaPath = ((Domain.BaseSetting.Parameter)parameter).ParameterAlias;
                //}
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "FileUpload";
                DirectoryInfo dir = new DirectoryInfo(filePath);

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
                        //if (_facade == null)
                        //{
                        //    _facade = new SystemSettingFacade(this.DataProvider);
                        //}
                        //object parameter = _facade.GetParameter("DOCDIRPATH", "DOCDIRPATHGROUP");
                        //if (parameter != null)
                        //{
                        //    //服务器目录路径
                        //    string filePath = ((Domain.BaseSetting.Parameter)parameter).ParameterAlias;
                        //    if (filePath.LastIndexOf('\\') == filePath.Length - 1)
                        //    {
                        //        filePath = filePath.Substring(0, filePath.Length - 1);
                        //    }
                        string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "FileUpload";
      
                        #region 下载文件
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
                        WebInfoPublish.PublishInfo(this, "$Error_No_WatchFile_Right", this.languageComponent1);
                    }
                }
            }
            catch (Exception ex)
            {
                WebInfoPublish.PublishInfo(this, "$Error_QueryFile_Exception", this.languageComponent1);
            }
        }

        protected void cmdCheck_ServerClick(object sender, EventArgs e)
        {
            if (_DocumentFacade == null)
            {
                _DocumentFacade = new DocumentFacade(base.DataProvider);
            }


            if (gridHelper == null)
            {
                gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            }

            try
            {

                if (!_DocumentFacade.GetDocDirRight(int.Parse(this.txtDocDirQuery.Text.Trim()), this.GetUserCode(), "CHECK"))
                {
                    WebInfoPublish.PublishInfo(this, "$Error_No_CheckFile_Right", this.languageComponent1);
                    return;
                }
                this.DataProvider.BeginTransaction();
                foreach (object obj in gridHelper.GetCheckedRows())
                {
                    int serial = int.Parse((obj as GridRecord).Items.FindItemByKey("DOCSERIAL").Value.ToString());
                    Doc doc = (Doc)_DocumentFacade.GetDOC(serial);
                    doc.Checkedstatus = "Y";
                    _DocumentFacade.UpdateDOC(doc);
                }
                this.DataProvider.CommitTransaction();
                this.gridHelper.RefreshData();
                WebInfoPublish.PublishInfo(this, "$Success_Check", this.languageComponent1);

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, "$Error_Check_Exception", this.languageComponent1);
            }

        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {

            if (pageAction == PageActionType.Cancel)
            {
                foreach (Infragistics.WebUI.UltraWebGrid.UltraGridRow ultragridRow in this.gridWebGrid.Rows)
                {
                    ultragridRow.Cells[0].Value = false;

                }
                //chbSelectAll.Checked = false;
            }
        }


        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("DOCSERIAL", "文件序号", null);
            this.gridHelper.AddColumn("DIRSERIAL", "目录编号", null);
            this.gridHelper.AddColumn("FileNAME", "文件名称", null);
            this.gridHelper.AddColumn("DOCNAME", "文件名称", null);
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
            this.gridHelper.AddDefaultColumn(true, false);

            this.gridWebGrid.Columns.FromKey("DOCSERIAL").Hidden = true;
            this.gridWebGrid.Columns.FromKey("DIRSERIAL").Hidden = true;
            this.gridWebGrid.Columns.FromKey("FileNAME").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ServerFullName").Hidden = true;

            //gridWebGrid.Columns.FromKey("DOCNAME").CellStyle.BackgroundImage = "";
            //gridWebGrid.Columns.FromKey("DOCNAME").CellStyle.Padding.Bottom = 0;
            //gridWebGrid.Columns.FromKey("DOCNAME").CellStyle.Padding.Left = 0;
            //gridWebGrid.Columns.FromKey("DOCNAME").CellStyle.Padding.Right = 0;
            //gridWebGrid.Columns.FromKey("DOCNAME").CellStyle.Padding.Top = 0;
            //gridWebGrid.Columns.FromKey("DOCNAME").CellButtonStyle.Margin.Top = 0;
            //gridWebGrid.Columns.FromKey("DOCNAME").CellButtonStyle.Margin.Bottom = 0;
            //gridWebGrid.Columns.FromKey("DOCNAME").CellButtonStyle.Margin.Left = 0;
            //gridWebGrid.Columns.FromKey("DOCNAME").CellButtonStyle.Margin.Right = 0;
            //gridWebGrid.Columns.FromKey("DOCNAME").CellButtonDisplay = CellButtonDisplay.Always;

            //gridWebGrid.Columns.FromKey("DOCNAME").CellButtonStyle.Font.Underline = true;
            //gridWebGrid.Columns.FromKey("DOCNAME").CellButtonStyle.ForeColor = Color.Blue;
            ((BoundDataField)this.gridHelper.Grid.Columns.FromKey("DOCNAME")).CssClass = "tdDocument";

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();
        }

        protected override DataRow GetGridRow(object obj)
        {
            //Infragistics.WebUI.UltraWebGrid.UltraGridRow ultragridRow = new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((Doc)obj).Docserial,
            //                    ((Doc)obj).Dirserial,
            //                    ((Doc)obj).Docname,
            //                    ((Doc)obj).Docname,
            //                    ((Doc)obj).Docnum,
            //                    ((Doc)obj).Docver,
            //                    ((Doc)obj).Itemlist,
            //                    ((Doc)obj).Oplist,
            //                    ((Doc)obj).Docchgnum,
            //                    ((Doc)obj).Docchgfile,
            //                    ((Doc)obj).Memo,
            //                    ((Doc)obj).Keyword,
            //                    ((Doc)obj).Doctype,
            //                    languageComponent1.GetString(((Doc)obj).Checkedstatus),
            //                    languageComponent1.GetString(((Doc)obj).Validstatus),
            //                    FormatHelper.ToDateString(((Doc)obj).Upfiledate),
            //                    ((Doc)obj).GetDisplayText("Upuser"),
            //                    ((Doc)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((Doc)obj).Mdate),
            //                    FormatHelper.ToTimeString(((Doc)obj).Mtime),
            //                    ((Doc)obj).ServerFileName,
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["DOCSERIAL"] = ((Doc)obj).Docserial;
            row["DIRSERIAL"] = ((Doc)obj).Dirserial;
            row["FileNAME"] = ((Doc)obj).Docname;
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

            doc.MaintainUser = this.GetUserCode();
            doc.Mdate = dbDateTime.DBDate;
            doc.Mtime = dbDateTime.DBTime;


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

                this.txtDocSerialEdit.Text = "";

                return;
            }

            this.txtDocSerialEdit.Text = ((Doc)obj).Docserial.ToString();

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
