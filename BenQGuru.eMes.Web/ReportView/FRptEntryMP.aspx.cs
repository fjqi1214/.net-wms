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
using Infragistics.WebUI.UltraWebNavigator;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Domain.ReportView;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.ReportView
{
    /// <summary>
    /// FRptEntryMP 的摘要说明。
    /// </summary>
    public partial class FRptEntryMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.ReportView.ReportViewFacade _facade;

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
            this.treeMenu.NodeClicked += new Infragistics.WebUI.UltraWebNavigator.NodeClickedEventHandler(this.treeMenu_NodeClicked);
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.BuildMenuTree();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("RptEntrySequence", "次序", null);
            this.gridHelper.AddColumn("RptEntryCode", "代码", null);
            this.gridHelper.AddColumn("RptEntryName", "名称", null);
            this.gridHelper.AddColumn("RptIsVisible", "是否可见", null);
            this.gridHelper.AddColumn("RptEntryType", "类型", null);
            this.gridHelper.AddColumn("RptEntryTypeDesc", "类型", null);
            this.gridHelper.AddColumn("ReportID", "ReportID", null);
            this.gridHelper.AddLinkColumn("RptAccessRight", "访问权限", null);
            this.gridHelper.AddLinkColumn("RptView", "浏览", null);
            this.gridHelper.AddLinkColumn("RptDownloadFile", "下载", null);
            this.gridHelper.AddLinkColumn("RptDesign", "设计", null);
            this.gridHelper.AddLinkColumn("RptPublish", "发布", null);

            this.gridWebGrid.Columns.FromKey("RptEntryType").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ReportID").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, true);

            //this.gridWebGrid.Columns.FromKey("RptEntrySequence").Width = Unit.Pixel(30);
            //this.gridWebGrid.Columns.FromKey("RptEntryCode").Width = Unit.Pixel(80);
            //this.gridWebGrid.Columns.FromKey("RptIsVisible").Width = Unit.Pixel(30);
            //this.gridWebGrid.Columns.FromKey("RptEntryTypeDesc").Width = Unit.Pixel(50);
            for (int i = 6; i <= 11; i++)
                this.gridWebGrid.Columns[i].Width = Unit.Pixel(30);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            RptViewEntry entry = (RptViewEntry)obj;
            string strEntryTypeDesc = "";
            RptViewDesignMain rptMain = null;
            if (entry.EntryType == ReportEntryType.Folder)
                strEntryTypeDesc = this.languageComponent1.GetString(ReportEntryType.Folder);
            else
            {
                if (this._facade == null) { this._facade = new ReportViewFacade(this.DataProvider); }
                rptMain = (RptViewDesignMain)this._facade.GetRptViewDesignMain(entry.ReportID);
                if (rptMain != null)
                {
                    entry.EntryName = rptMain.ReportName;
                    entry.Description = rptMain.Description;
                    if (rptMain.ReportBuilder == ReportBuilder.OnLine)
                        strEntryTypeDesc = this.languageComponent1.GetString(ReportEntryType.Report + "_online");
                    else
                        strEntryTypeDesc = this.languageComponent1.GetString(ReportEntryType.Report + "_offline");
                }
            }

            DataRow row = DtSource.NewRow();
            row["RptEntrySequence"] = entry.Sequence;
            row["RptEntryCode"] = entry.EntryCode;
            row["RptEntryName"] = entry.EntryName;
            row["RptIsVisible"] = entry.Visible;
            row["RptEntryType"] = entry.EntryType;
            row["RptEntryTypeDesc"] = strEntryTypeDesc;
            row["ReportID"] = entry.ReportID;
            row["RptAccessRight"] = "";
            row["RptView"] = "";
            row["RptDownloadFile"] = "";
            row["RptDesign"] = "";
            row["RptPublish"] = "";
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (this._facade == null) { this._facade = new ReportViewFacade(this.DataProvider); }
            return this._facade.QueryRptViewEntryByParent(this.txtEntryCodeQuery.Text, inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (this._facade == null) { this._facade = new ReportViewFacade(this.DataProvider); }
            return this._facade.QueryRptViewEntryByParentCount(this.txtEntryCodeQuery.Text);
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (this._facade == null) { this._facade = new ReportViewFacade(this.DataProvider); }
            RptViewEntry entry = (RptViewEntry)domainObject;
            entry.EntryType = ReportEntryType.Folder;
            entry.ParentEntryCode = this.txtEntryCodeQuery.Text;
            this._facade.AddRptViewEntry(entry);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (this._facade == null) { this._facade = new ReportViewFacade(this.DataProvider); }
            this._facade.DeleteRptViewEntryWithReport((RptViewEntry[])domainObjects.ToArray(typeof(RptViewEntry)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (this._facade == null) { this._facade = new ReportViewFacade(this.DataProvider); }
            this._facade.UpdateRptViewEntry((RptViewEntry)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtRptEntryCode.ReadOnly = false;
                this.BuildMenuTree();
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtRptEntryCode.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (this._facade == null) { this._facade = new ReportViewFacade(this.DataProvider); }
            RptViewEntry entry = this._facade.CreateNewRptViewEntry();
            if (this.txtRptEntryCode.ReadOnly == true)
            {
                entry = (RptViewEntry)this._facade.GetRptViewEntry(this.txtRptEntryCode.Text);
            }

            entry.Sequence = decimal.Parse(this.txtRptEntrySequence.Text);
            entry.EntryCode = this.txtRptEntryCode.Text.Trim().ToUpper();
            entry.EntryName = this.txtRptEntryName.Text.Trim().ToUpper();
            entry.Description = this.txtRptEntryDesc.Text;
            entry.Visible = FormatHelper.BooleanToString(this.chkRptIsVisible.Checked);
            entry.MaintainUser = this.GetUserCode();

            return entry;
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (this._facade == null) { this._facade = new ReportViewFacade(this.DataProvider); }
            object obj = _facade.GetRptViewEntry(row.Items.FindItemByKey("RptEntryCode").Text.ToString());

            if (obj != null)
            {
                return (RptViewEntry)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtRptEntrySequence.Text = "0";
                this.txtRptEntryCode.Text = "";
                this.txtRptEntryName.Text = "";
                this.txtRptEntryDesc.Text = "";
                this.chkRptIsVisible.Checked = true;

                return;
            }

            RptViewEntry entry = (RptViewEntry)obj;
            this.txtRptEntrySequence.Text = entry.Sequence.ToString();
            this.txtRptEntryCode.Text = entry.EntryCode;
            this.txtRptEntryName.Text = entry.EntryName;
            this.txtRptEntryDesc.Text = entry.Description;
            this.chkRptIsVisible.Checked = FormatHelper.StringToBoolean(entry.Visible);
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblRptEntryCode, txtRptEntryCode, 40, true));
            manager.Add(new NumberCheck(lblRptEntrySequence, txtRptEntrySequence, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }

        #endregion

        #region Tree
        /// <summary>
        /// 构建Menu树
        /// </summary>
        private void BuildMenuTree()
        {
            this.treeMenu.Nodes.Clear();

            if (this._facade == null) { this._facade = new ReportViewFacade(this.DataProvider); }
            object[] objs = this._facade.GetAllRptViewEntry();
            if (objs == null || objs.Length == 0)
                return;
            RptViewEntry[] entryList = new RptViewEntry[objs.Length];
            objs.CopyTo(entryList, 0);
            Node node = new Node();
            node.Text = this.languageComponent1.GetString("ReportFolderRootNode");
            this.treeMenu.Nodes.Add(node);
            BuildTreeSubNode(entryList, node, "");

            this.treeMenu.ExpandAll();

        }
        private void BuildTreeSubNode(RptViewEntry[] entryList, Node currentNode, string parentEntryCode)
        {
            string strParentCode = "";
            if (currentNode.Tag != null && currentNode.Tag is RptViewEntry)
            {
                strParentCode = ((RptViewEntry)currentNode.Tag).EntryCode;
            }
            for (int i = 0; i < entryList.Length; i++)
            {
                if (entryList[i].ParentEntryCode == strParentCode && entryList[i].EntryType == ReportEntryType.Folder)
                {
                    Node node = new Node();
                    node.Text = entryList[i].EntryName;
                    node.Tag = entryList[i];
                    currentNode.Nodes.Add(node);

                    BuildTreeSubNode(entryList, node, entryList[i].EntryCode);
                }
            }

        }

        private void treeMenu_NodeClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
        {
            if (e.Node.Tag != null && e.Node.Tag is RptViewEntry)
                this.txtEntryCodeQuery.Text = ((RptViewEntry)e.Node.Tag).EntryCode;
            else
                this.txtEntryCodeQuery.Text = "";
            this.gridHelper.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);

        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            string strType = row.Items.FindItemByKey("RptEntryType").Value.ToString();
            if (strType == ReportEntryType.Folder)
                return;
            string strRptId = row.Items.FindItemByKey("ReportID").Value.ToString();
            if (commandName == "RptAccessRight")
            {
                string strBackUrl = "FRptEntryMP.aspx";
                string strUrl = "FRptAccessRightMP.aspx?reportid=" + strRptId + "&backurl=" + strBackUrl;
                this.Response.Redirect(strUrl);
            }
            else if (commandName == "RptView")
            {
                ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
                RptViewDesignMain designMain = (RptViewDesignMain)rptFacade.GetRptViewDesignMain(strRptId);
                if (designMain == null)
                {
                    WebInfoPublish.Publish(this, "$Error_RequestUrlParameter_Lost", this.languageComponent1);
                    return;
                }
                if (designMain.Status != ReportDesignStatus.Publish)
                {
                    WebInfoPublish.Publish(this, "$ReportView_Status_Error", this.languageComponent1);
                    return;
                }

                string strScript = "ViewReport('" + this.VirtualHostRoot + "ReportView/FRptViewMP.aspx?reportid=" + strRptId + "');";
                //ClientScriptManager scriptManager = this.Page.ClientScript;
                //scriptManager.RegisterClientScriptBlock(typeof(string), "RptView", strScript);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "RptView", strScript,true);
            }
            else if (commandName == "RptDownloadFile")
            {
                ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
                RptViewDesignMain designMain = (RptViewDesignMain)rptFacade.GetRptViewDesignMain(strRptId);
                string strFile = designMain.ReportFileName.Replace("\\", "/"); ;
                if (strFile != "")
                {
//                    Response.Write(string.Format(@"<script language=javascript> 
//										window.top.document.getElementById('iframeDownload').src='" + string.Format(@"{0}FDownload.aspx", this.VirtualHostRoot)
//                        + "?fileName="
//                        + string.Format(@"{0}", strFile)
//                        + @"';</script>"));
                    string script = string.Format(@"
										window.top.document.getElementById('iframeDownload').src='" + string.Format(@"{0}FDownload.aspx", this.VirtualHostRoot)
                        + "?fileName="
                        + string.Format(@"{0}", strFile)
                        + @"';");
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "DownLoadFile", script, true);
                }
            }
            else if (commandName == "RptDesign")
            {
                ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
                RptViewDesignMain designMain = (RptViewDesignMain)rptFacade.GetRptViewDesignMain(strRptId);
                string strUrl = "";
                if (designMain.ReportBuilder == ReportBuilder.OnLine)
                    strUrl = "FRptDesignStep1MP.aspx?reportid=" + strRptId;
                else
                    strUrl = "FRptUploadStep1MP.aspx?reportid=" + strRptId;
                Response.Redirect(strUrl);
            }
            else if (commandName == "RptPublish")
            {
                ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
                RptViewDesignMain designMain = (RptViewDesignMain)rptFacade.GetRptViewDesignMain(strRptId);
                if (designMain.Status == ReportDesignStatus.Initial ||
                    designMain.Status == ReportDesignStatus.ReDesign
                    || designMain.Status == ReportDesignStatus.Publish)
                {
                    string strUrl = "FRptPublishDesignMP.aspx?reportid=" + strRptId;
                    this.Response.Redirect(strUrl);
                }
            }
        }
        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            RptViewEntry entry = (RptViewEntry)obj;
            string strEntryTypeDesc = "";
            RptViewDesignMain rptMain = null;
            if (entry.EntryType == ReportEntryType.Folder)
                strEntryTypeDesc = this.languageComponent1.GetString(ReportEntryType.Folder);
            else
            {
                if (this._facade == null) { this._facade = new ReportViewFacade(this.DataProvider); }
                rptMain = (RptViewDesignMain)this._facade.GetRptViewDesignMain(entry.ReportID);
                if (rptMain != null)
                {
                    entry.EntryName = rptMain.ReportName;
                    entry.Description = rptMain.Description;
                    if (rptMain.ReportBuilder == ReportBuilder.OnLine)
                        strEntryTypeDesc = this.languageComponent1.GetString(ReportEntryType.Report + "_online");
                    else
                        strEntryTypeDesc = this.languageComponent1.GetString(ReportEntryType.Report + "_offline");
                }
            }

            string[] objRow = new string[]{
                entry.Sequence.ToString(),
                entry.EntryCode,
                entry.EntryName,
                entry.Visible,
                strEntryTypeDesc
                };
            return objRow;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[]{
                "RptEntrySequence",
                "RptEntryCode",
                "RptEntryName",
                "RptIsVisible",
                "RptEntryTypeDesc"
            };
        }
        #endregion

    }
}
