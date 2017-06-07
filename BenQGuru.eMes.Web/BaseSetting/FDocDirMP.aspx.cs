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

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.MutiLanguage;
using Infragistics.WebUI.UltraWebNavigator;
using BenQGuru.eMES.Domain.Document;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FModuleMP 的摘要说明。
	/// </summary>
	public partial class FDocDirMP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Label labeltopic;
		protected System.Web.UI.WebControls.Label lbllModuleStatusEdit;
		
		protected System.Web.UI.WebControls.Label lblHelpeFileNameEdit;
		protected System.Web.UI.WebControls.Label lblModuleTitle;


        protected DocumentFacade _facade = null;


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
			this.treeDocument.NodeClicked += new Infragistics.WebUI.UltraWebNavigator.NodeClickedEventHandler(this.treeDocument_NodeSelectionChanged);
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
			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

                this.txtDocDirQuery.Text = "0";

				// 构建Module树
				this.BuildDocumentTree(false);

				this.treeDocument.ParentNodeImageUrl = string.Format("{0}skin/image/treenode2.gif", this.VirtualHostRoot);
				this.treeDocument.LeafNodeImageUrl   = string.Format("{0}skin/image/treenode2.gif", this.VirtualHostRoot);

   			}
		}
		
		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		#endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new DocumentFacade(base.DataProvider);
            }

            //判断目录顺序不能重复
            if (this._facade.CheckDirSeq(((DocDirForQuery)domainObject).Pdirserial, ((DocDirForQuery)domainObject).Dirseq))
            {
                WebInfoPublish.Publish(this, "$Message_DirSeq_Exist", languageComponent1);
                return;
            }

            this.DataProvider.BeginTransaction();
            try
            {
                DocDir docDir = new DocDir();
                docDir.Dirseq = ((DocDirForQuery)domainObject).Dirseq;
                docDir.Dirname = ((DocDirForQuery)domainObject).Dirname;
                docDir.Dirdesc = ((DocDirForQuery)domainObject).Dirdesc;
                docDir.Pdirserial = ((DocDirForQuery)domainObject).Pdirserial;
                docDir.MaintainUser = ((DocDirForQuery)domainObject).MaintainUser;
                docDir.Mdate = ((DocDirForQuery)domainObject).Mdate;
                docDir.Mtime = ((DocDirForQuery)domainObject).Mtime;
                this._facade.AddDOCDIR(docDir);

                int serial = this._facade.GetMaxSerial();
                Docdir2UserGroup docdir2UserGroup;

                if (!String.IsNullOrEmpty(((DocDirForQuery)domainObject).UploadUsergroupcode))
                {
                    string[] uploadUsergroupcode = ((DocDirForQuery)domainObject).UploadUsergroupcode.Split(',');
                    foreach (string item in uploadUsergroupcode)
                    {
                        docdir2UserGroup = new Docdir2UserGroup();
                        docdir2UserGroup.Dirserial = serial;
                        docdir2UserGroup.Usergroupcode = item;
                        docdir2UserGroup.Dirtype = "UPLOAD";
                        docdir2UserGroup.MaintainUser = ((DocDirForQuery)domainObject).MaintainUser;
                        docdir2UserGroup.Mdate = ((DocDirForQuery)domainObject).Mdate;
                        docdir2UserGroup.Mtime = ((DocDirForQuery)domainObject).Mtime;
                        this._facade.AddDocdir2UserGroup(docdir2UserGroup);
                    }
                }

                if (!String.IsNullOrEmpty(((DocDirForQuery)domainObject).QueryUsergroupcode))
                {
                    string[] queryUsergroupcode = ((DocDirForQuery)domainObject).QueryUsergroupcode.Split(',');
                    foreach (string item in queryUsergroupcode)
                    {
                        docdir2UserGroup = new Docdir2UserGroup();
                        docdir2UserGroup.Dirserial = serial;
                        docdir2UserGroup.Usergroupcode = item;
                        docdir2UserGroup.Dirtype = "QUERY";
                        docdir2UserGroup.MaintainUser = ((DocDirForQuery)domainObject).MaintainUser;
                        docdir2UserGroup.Mdate = ((DocDirForQuery)domainObject).Mdate;
                        docdir2UserGroup.Mtime = ((DocDirForQuery)domainObject).Mtime;
                        this._facade.AddDocdir2UserGroup(docdir2UserGroup);
                    }
                }

                if (!String.IsNullOrEmpty(((DocDirForQuery)domainObject).CheckUsergroupcode))
                {
                    string[] checkUsergroupcode = ((DocDirForQuery)domainObject).CheckUsergroupcode.Split(',');
                    foreach (string item in checkUsergroupcode)
                    {
                        docdir2UserGroup = new Docdir2UserGroup();
                        docdir2UserGroup.Dirserial = serial;
                        docdir2UserGroup.Usergroupcode = item;
                        docdir2UserGroup.Dirtype = "CHECK";
                        docdir2UserGroup.MaintainUser = ((DocDirForQuery)domainObject).MaintainUser;
                        docdir2UserGroup.Mdate = ((DocDirForQuery)domainObject).Mdate;
                        docdir2UserGroup.Mtime = ((DocDirForQuery)domainObject).Mtime;
                        this._facade.AddDocdir2UserGroup(docdir2UserGroup);
                    }
                }


            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }
            this.DataProvider.CommitTransaction();

            BuildDocumentTree(true);


        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new DocumentFacade(base.DataProvider);
            }

            this.DataProvider.BeginTransaction();
            try
            {

                foreach (DocDir obj in domainObjects)
                {
                    if (_facade.CheckHasSubDIR(obj.Dirserial))
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.PublishInfo(this,"$Message_Has_SubDIR", languageComponent1);

                        return;
                    }
                    if (_facade.CheckHasDocuments(obj.Dirserial))
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.PublishInfo(this, "$Message_Has_Documents", languageComponent1);
                        return;
                    }
                
                    this._facade.DeleteDOCDIR(obj);
                    this._facade.DeleteDocdir2UserGroupByDIRSerial(obj.Dirserial);
                    
                    
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }
            this.DataProvider.CommitTransaction();

            BuildDocumentTree(true);

        }

        protected override void UpdateDomainObject(object domainObject)
        {
            this.CheckParentDocDir(this.drpParentDirCodeEdit.SelectedValue);
            if (_facade == null)
            {
                _facade = new DocumentFacade(base.DataProvider);
            }

            //判断目录顺序不能重复
            object obj = this._facade.GetDOCDIR(((DocDirForQuery)domainObject).Dirserial);
            if (((DocDir)obj).Dirseq != ((DocDirForQuery)domainObject).Dirseq || ((DocDir)obj).Pdirserial != ((DocDirForQuery)domainObject).Pdirserial)
            {
                if (this._facade.CheckDirSeq(((DocDirForQuery)domainObject).Pdirserial, ((DocDirForQuery)domainObject).Dirseq))
                {
                    WebInfoPublish.Publish(this, "$Message_DirSeq_Exist", languageComponent1);
                    return;
                }
            }

            this.DataProvider.BeginTransaction();
            try
            {
                DocDir docDir = new DocDir();
                docDir.Dirserial = ((DocDirForQuery)domainObject).Dirserial;
                docDir.Dirseq = ((DocDirForQuery)domainObject).Dirseq;
                docDir.Dirname = ((DocDirForQuery)domainObject).Dirname;
                docDir.Dirdesc = ((DocDirForQuery)domainObject).Dirdesc;
                docDir.Pdirserial = ((DocDirForQuery)domainObject).Pdirserial;
                docDir.MaintainUser = ((DocDirForQuery)domainObject).MaintainUser;
                docDir.Mdate = ((DocDirForQuery)domainObject).Mdate;
                docDir.Mtime = ((DocDirForQuery)domainObject).Mtime;
                this._facade.UpdateDOCDIR(docDir);

                this._facade.DeleteDocdir2UserGroupByDIRSerial(((DocDir)domainObject).Dirserial);

                Docdir2UserGroup docdir2UserGroup;
                if (!String.IsNullOrEmpty(((DocDirForQuery)domainObject).UploadUsergroupcode))
                {
                    string[] uploadUsergroupcode = ((DocDirForQuery)domainObject).UploadUsergroupcode.Split(',');
                    foreach (string item in uploadUsergroupcode)
                    {
                        docdir2UserGroup = new Docdir2UserGroup();
                        docdir2UserGroup.Dirserial = ((DocDirForQuery)domainObject).Dirserial;
                        docdir2UserGroup.Usergroupcode = item;
                        docdir2UserGroup.Dirtype = "UPLOAD";
                        docdir2UserGroup.MaintainUser = ((DocDirForQuery)domainObject).MaintainUser;
                        docdir2UserGroup.Mdate = ((DocDirForQuery)domainObject).Mdate;
                        docdir2UserGroup.Mtime = ((DocDirForQuery)domainObject).Mtime;
                        this._facade.AddDocdir2UserGroup(docdir2UserGroup);
                    }
                }

                if (!String.IsNullOrEmpty(((DocDirForQuery)domainObject).QueryUsergroupcode))
                {
                    string[] queryUsergroupcode = ((DocDirForQuery)domainObject).QueryUsergroupcode.Split(',');
                    foreach (string item in queryUsergroupcode)
                    {
                        docdir2UserGroup = new Docdir2UserGroup();
                        docdir2UserGroup.Dirserial = ((DocDirForQuery)domainObject).Dirserial;
                        docdir2UserGroup.Usergroupcode = item;
                        docdir2UserGroup.Dirtype = "QUERY";
                        docdir2UserGroup.MaintainUser = ((DocDirForQuery)domainObject).MaintainUser;
                        docdir2UserGroup.Mdate = ((DocDirForQuery)domainObject).Mdate;
                        docdir2UserGroup.Mtime = ((DocDirForQuery)domainObject).Mtime;
                        this._facade.AddDocdir2UserGroup(docdir2UserGroup);
                    }
                }

                if (!String.IsNullOrEmpty(((DocDirForQuery)domainObject).CheckUsergroupcode))
                {
                    string[] checkUsergroupcode = ((DocDirForQuery)domainObject).CheckUsergroupcode.Split(',');
                    foreach (string item in checkUsergroupcode)
                    {
                        docdir2UserGroup = new Docdir2UserGroup();
                        docdir2UserGroup.Dirserial = ((DocDirForQuery)domainObject).Dirserial;
                        docdir2UserGroup.Usergroupcode = item;
                        docdir2UserGroup.Dirtype = "CHECK";
                        docdir2UserGroup.MaintainUser = ((DocDirForQuery)domainObject).MaintainUser;
                        docdir2UserGroup.Mdate = ((DocDirForQuery)domainObject).Mdate;
                        docdir2UserGroup.Mtime = ((DocDirForQuery)domainObject).Mtime;
                        this._facade.AddDocdir2UserGroup(docdir2UserGroup);
                    }
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }
            this.DataProvider.CommitTransaction();
            BuildDocumentTree(true);
        }

        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("DirSerial", "目录编号", null);
            this.gridHelper.AddColumn("DocDirSequence", "目录顺序", null);
            this.gridHelper.AddColumn("DocDirName", "目录名称", null);
            this.gridHelper.AddColumn("ParentDir", "父目录", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("UploadUserGroup", "上传权限用户组", null);
            this.gridHelper.AddColumn("QueryUserGroup", "查阅权限用户组", null);
            this.gridHelper.AddColumn("CheckUserGroup", "审核权限用户组", null);

            this.gridWebGrid.Columns.FromKey("DirSerial").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((DocDirForQuery)obj).Dirserial,
            //                    ((DocDirForQuery)obj).Dirseq,
            //                    ((DocDirForQuery)obj).Dirname,
            //                    ((DocDirForQuery)obj).PDirName,
            //                    ((DocDirForQuery)obj).Dirdesc,
            //                    ((DocDirForQuery)obj).UploadUsergroupcode,
            //                    ((DocDirForQuery)obj).QueryUsergroupcode,
            //                    ((DocDirForQuery)obj).CheckUsergroupcode,
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["DirSerial"] = ((DocDirForQuery)obj).Dirserial;
            row["DocDirSequence"] = ((DocDirForQuery)obj).Dirseq;
            row["DocDirName"] = ((DocDirForQuery)obj).Dirname;
            row["ParentDir"] = ((DocDirForQuery)obj).PDirName;
            row["Memo"] = ((DocDirForQuery)obj).Dirdesc;
            row["UploadUserGroup"] = ((DocDirForQuery)obj).UploadUsergroupcode;
            row["QueryUserGroup"] = ((DocDirForQuery)obj).QueryUsergroupcode;
            row["CheckUserGroup"] = ((DocDirForQuery)obj).CheckUsergroupcode;
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new DocumentFacade(base.DataProvider);
            }
            return this._facade.QuerySubDOCDIR(int.Parse(FormatHelper.CleanString(this.txtDocDirQuery.Text)), inclusive, exclusive);



        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new DocumentFacade(base.DataProvider);
            }
            return this._facade.QuerySubDOCDIRCount(int.Parse(FormatHelper.CleanString(this.txtDocDirQuery.Text)));
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
            if (_facade == null)
            {
                _facade = new DocumentFacade(base.DataProvider);
            }
            object obj = _facade.QuerySubDOCDIR(int.Parse(row.Items.FindItemByKey("DirSerial").Text));

            if (obj != null)
            {
                return (DocDirForQuery)obj;
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

            if (_facade == null)
            {
                _facade = new DocumentFacade(base.DataProvider);
            }
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DocDirForQuery docDir = new DocDirForQuery();

            docDir.Dirserial = this.txtDirSerialEdit.Text == "" ? 0 : int.Parse(this.txtDirSerialEdit.Text);
            docDir.Dirseq = int.Parse(this.txtDirSequenceEdit.Text);
            docDir.Dirname = FormatHelper.CleanString(this.txtDirNameEdit.Text,40);
            docDir.Dirdesc = FormatHelper.CleanString(this.txtMemoEdit.Text, 100);
            docDir.Pdirserial = String.IsNullOrEmpty(this.drpParentDirCodeEdit.SelectedValue)?0:int.Parse(this.drpParentDirCodeEdit.SelectedValue);
            docDir.UploadUsergroupcode = FormatHelper.CleanString(this.txtUploadUserGroupEdit.Text, 200);
            docDir.QueryUsergroupcode = FormatHelper.CleanString(this.txtQueryUserGroupEdit.Text, 200);
            docDir.CheckUsergroupcode = FormatHelper.CleanString(this.txtCheckUserGroupEdit.Text, 200);
            docDir.Mdate = currentDateTime.DBDate;
            docDir.Mtime = currentDateTime.DBTime;
            docDir.MaintainUser = this.GetUserCode();


            return docDir;
        }

        /// <summary>
        /// 将DomainObject写入编辑区，如果为null则全部置空
        /// </summary>
        /// <param name="item"></param>
        protected override void SetEditObject(Object obj)
        {
            if (obj == null)
            {
                this.txtDirSerialEdit.Text = "";
                this.txtDirSequenceEdit.Text = "";
                this.txtDirNameEdit.Text = "";
                this.txtMemoEdit.Text = "";
                this.txtUploadUserGroupEdit.Text = "";
                this.txtQueryUserGroupEdit.Text = "";
                this.txtCheckUserGroupEdit.Text = "";

                try
                {
                    this.drpParentDirCodeEdit.SelectedValue = this.txtDocDirQuery.Text;
                }
                catch
                {
                    this.drpParentDirCodeEdit.SelectedIndex = 0;
                }

                return;
            }

            this.txtDirSerialEdit.Text = ((DocDirForQuery)obj).Dirserial.ToString();
            this.txtDirSequenceEdit.Text = ((DocDirForQuery)obj).Dirseq.ToString();
            this.txtDirNameEdit.Text = ((DocDirForQuery)obj).Dirname;
            this.txtMemoEdit.Text = ((DocDirForQuery)obj).Dirdesc;
            this.txtUploadUserGroupEdit.Text = ((DocDirForQuery)obj).UploadUsergroupcode;
            this.txtQueryUserGroupEdit.Text = ((DocDirForQuery)obj).QueryUsergroupcode;
            this.txtCheckUserGroupEdit.Text = ((DocDirForQuery)obj).CheckUsergroupcode;

            try
            {
                this.drpParentDirCodeEdit.SelectedValue = ((DocDirForQuery)obj).Pdirserial.ToString();
            }
            catch
            {
                this.drpParentDirCodeEdit.SelectedIndex = 0;
            }

            
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new NumberCheck(this.lblDirSequenceEdit, this.txtDirSequenceEdit, true));
            manager.Add(new LengthCheck(this.lblDirNameEdit, this.txtDirNameEdit, 40, true));
            manager.Add(new LengthCheck(this.lblMemoEdit, this.txtMemoEdit, 100, false));
            manager.Add(new LengthCheck(this.lblUploadUserGroupEdit, this.txtUploadUserGroupEdit, 40, true));
            manager.Add(new LengthCheck(this.lblQueryUserGroupEdit, this.txtQueryUserGroupEdit, 40, true));
            manager.Add(new LengthCheck(this.lblCheckUserGroupEdit, this.txtCheckUserGroupEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

		#region Tree
		
		private ITreeObjectNode LoadDocumentTreeToApplication()
		{	
			if( _facade == null )
			{
                _facade = new DocumentFacade(base.DataProvider);
			}	
			return this._facade.BuildDocumentTree();
		}

		/// <summary>
        /// 构建Document树
		/// </summary>
        /// <param name="reload">是否重新从数据库中读取Document树</param>
        private void BuildDocumentTree(bool reload)
		{
			this.treeDocument.Nodes.Clear();

			if ( reload )
			{
                this.LoadDocumentTreeToApplication();
			}

            ITreeObjectNode node = LoadDocumentTreeToApplication();

            this.treeDocument.Nodes.Add(BuildTreeNode(node));

			LanguageWord lword  = this.languageComponent1.GetLanguage("documentRoot");

			if ( lword != null )
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

            this.BuildParentDocumentCodeList();
		}

		private Infragistics.WebUI.UltraWebNavigator.Node BuildTreeNode(ITreeObjectNode treeNode)
		{
			Infragistics.WebUI.UltraWebNavigator.Node node = new Node();

			node.Text = treeNode.Text;
			node.Tag = treeNode;

			if ( treeNode.Text == this.txtDirNameEdit.Text )
			{
                this.treeDocument.SelectedNode = node;
			}

			foreach( ITreeObjectNode subNode in treeNode.GetSubLevelChildrenNodes() )
			{
				node.Nodes.Add( BuildTreeNode(subNode) );
			}

			return node;
		}

        protected void treeDocument_NodeSelectionChanged(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                DocumentTreeNode parentNode = (DocumentTreeNode)e.Node.Tag;
                this.txtDocDirQuery.Text = parentNode.docDir.Dirserial.ToString();

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

            this.gridHelper.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);

            try
            {
                this.drpParentDirCodeEdit.SelectedValue = this.txtDocDirQuery.Text;
            }
            catch
            {
                this.drpParentDirCodeEdit.SelectedIndex = 0;
            }
        }

        private bool CheckParentDocDir(string parentDirCode)
		{
            if (parentDirCode == "")
			{
				return true;
			}

			ITreeObjectNode node = LoadDocumentTreeToApplication().GetTreeObjectNodeByID(this.txtDirSerialEdit.Text);

			if ( node == null )
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Node_Lost");
			}
			
			TreeObjectNodeSet set = node.GetAllNodes();

			foreach (ITreeObjectNode childNode in set)
			{
                if (childNode.ID.ToUpper() == parentDirCode.ToUpper())
				{
					ExceptionManager.Raise(this.GetType(),"$Error_Parent_To_Children");
				}
			}
			
			return true;
		}

		#endregion

        #region 数据初始化
        private void BuildParentDocumentCodeList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.drpParentDirCodeEdit);
            if (_facade == null)
            {
                _facade = new DocumentFacade(base.DataProvider);
            }
            builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllDOCDIR);
            builder.Build("Dirname", "Dirserial");

            this.drpParentDirCodeEdit.Items.Insert(0, "");

            try
            {
                this.drpParentDirCodeEdit.SelectedValue = this.txtDocDirQuery.Text;
            }
            catch
            {
                this.drpParentDirCodeEdit.SelectedIndex = 0;
            }
        }

        #endregion

 
        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{   ((DocDirForQuery)obj).Dirseq.ToString(),
                                ((DocDirForQuery)obj).Dirname,
                                ((DocDirForQuery)obj).PDirName,
                                ((DocDirForQuery)obj).Dirdesc,
                                ((DocDirForQuery)obj).UploadUsergroupcode,
                                ((DocDirForQuery)obj).QueryUsergroupcode,
                                ((DocDirForQuery)obj).CheckUsergroupcode};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
                                    "DocDirSequence",
                                    "DocDirName",
                                    "ParentDir",
                                    "Memo",
                                    "UploadUserGroup",
                                    "QueryUserGroup",
                                    "CheckUserGroup"
                                    };
        }
        #endregion

	}
}

