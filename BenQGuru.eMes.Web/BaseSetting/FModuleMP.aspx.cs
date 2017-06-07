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
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.MutiLanguage;
using Infragistics.WebUI.UltraWebNavigator;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FModuleMP 的摘要说明。
	/// </summary>
	public partial class FModuleMP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.WebControls.Label labeltopic;
		protected System.Web.UI.WebControls.Label lbllModuleStatusEdit;
		
		protected System.Web.UI.WebControls.Label lblHelpeFileNameEdit;
		protected System.Web.UI.WebControls.Label lblModuleTitle;


		protected SystemSettingFacade _facade = null ;//new SystemSettingFacadeFactory().Create();


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
			//this.treeModule.NodeClicked += new Infragistics.WebUI.UltraWebNavigator.NodeClickedEventHandler(this.treeModule_NodeClicked);
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

				// 构建Module树
				this.BuildModuleTree(false);
                drpIsActiveEdit_Load();
                drpIsSystemEdit_Load();
                drpIsRestrain_Load();
				this.treeModule.ParentNodeImageUrl = string.Format("{0}skin/image/treenode2.gif", this.VirtualHostRoot);
				this.treeModule.LeafNodeImageUrl   = string.Format("{0}skin/image/treenode3.gif", this.VirtualHostRoot);
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
			if( _facade == null )
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}	
			this._facade.AddModule( (Module)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if( _facade == null )
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}	
			this._facade.DeleteModule( (Module[])domainObjects.ToArray( typeof(Module) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			this.CheckParentModule(this.drpParentModuleCodeEdit.SelectedValue);
			if( _facade == null )
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}	
			this._facade.UpdateModule( (Module)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtModuleCodeEdit.ReadOnly = false;
				this.BuildModuleTree(true);
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtModuleCodeEdit.ReadOnly = true;
			}
		}
		#endregion
		
		#region WebGrid

		protected override void InitWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn("ModuleSequence",	"模块顺序",		null);
			this.gridHelper.AddColumn("ModuleCode",		"模块代码",		null);
			this.gridHelper.AddColumn("ModuleVersion",	"模块版本",		null);
			this.gridHelper.AddColumn("ModuleType",		"模块类型",		null);
			this.gridHelper.AddColumn("ModuleStatus",	"模块状态",		null);
			this.gridHelper.AddColumn("HelpFileName",	"帮助文件",		null);
			this.gridHelper.AddColumn("IsSystem",		"是否系统模块",	null);
			this.gridHelper.AddColumn("IsActive",		"是否可用",		null);
			this.gridHelper.AddColumn("Description",	"模块描述",		null);
			this.gridHelper.AddColumn("FormUrl",		"页面URL",		null);
			this.gridHelper.AddColumn("Restrain",		"抑制显示",		null);

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			base.InitWebGrid();
		}
		
		protected override DataRow GetGridRow( object obj )
		{
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{"false",
            //                    ((Module)obj).ModuleSequence,
            //                    ((Module)obj).ModuleCode,
            //                    ((Module)obj).ModuleVersion,
            //                    ((Module)obj).ModuleType,
            //                    ((Module)obj).ModuleStatus,
            //                    ((Module)obj).ModuleHelpFileName,
            //                    FormatHelper.DisplayBoolean(((Module)obj).IsSystem, this.languageComponent1),
            //                    FormatHelper.DisplayBoolean(((Module)obj).IsActive, this.languageComponent1),
            //                    ((Module)obj).ModuleDescription,
            //                    ((Module)obj).FormUrl,
            //                    FormatHelper.DisplayBoolean(((Module)obj).IsRestrain, this.languageComponent1),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["ModuleSequence"] = ((Module)obj).ModuleSequence;
            row["ModuleCode"] = ((Module)obj).ModuleCode;
            row["ModuleVersion"] = ((Module)obj).ModuleVersion;
            row["ModuleType"] = ((Module)obj).ModuleType;
            row["ModuleStatus"] = ((Module)obj).ModuleStatus;
            row["HelpFileName"] = ((Module)obj).ModuleHelpFileName;
            row["IsSystem"] = FormatHelper.DisplayBoolean(((Module)obj).IsSystem, this.languageComponent1);
            row["IsActive"] = FormatHelper.DisplayBoolean(((Module)obj).IsActive, this.languageComponent1);
            row["Description"] = ((Module)obj).ModuleDescription;
            row["FormUrl"] = ((Module)obj).FormUrl;
            row["Restrain"] = FormatHelper.DisplayBoolean(((Module)obj).IsRestrain, this.languageComponent1);
            return row;

		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if( _facade == null )
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}	
			return this._facade.GetSubModulesByModuleCode( FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModuleCodeQuery.Text)), inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if( _facade == null )
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}	
			return this._facade.GetSubModulesByModuleCodeCount( FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModuleCodeQuery.Text)) );
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
			if( _facade == null )
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}
            object obj = _facade.GetModule(row.Items.FindItemByKey("ModuleCode").Text.ToString());
			
			if (obj != null)
			{
				return (Module)obj;
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

			if( _facade == null )
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}	
			Module module = this._facade.CreateNewModule();

			module.ModuleSequence		= System.Decimal.Parse(this.txtModuleSequenceEdit.Text); 
			module.ModuleCode			= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtModuleCodeEdit.Text,40));
			module.ModuleVersion		= FormatHelper.CleanString(this.txtModuleVersionEdit.Text,40);
			module.ModuleType			= this.drpModuleTypeEdit.SelectedValue;
			module.ModuleStatus			= this.drpModuleStatusEdit.SelectedValue;
			module.ModuleHelpFileName	= FormatHelper.CleanString(this.txtHelpFileNameEdit.Text,100);
			module.IsSystem				= this.drpIsSystemEdit.SelectedValue;
			module.IsActive 			= this.drpIsActiveEdit.SelectedValue;
			module.ModuleDescription	= FormatHelper.CleanString(this.txtModuleDescEdit.Text,100);
			module.FormUrl				= FormatHelper.CleanString(this.txtFormUrlEdit.Text,100);
			module.ParentModuleCode		= this.drpParentModuleCodeEdit.SelectedValue;
			module.IsRestrain           = this.drpIsRestrain.SelectedValue;
			module.MaintainUser			= this.GetUserCode();

			return module;
		}

		/// <summary>
		/// 将DomainObject写入编辑区，如果为null则全部置空
		/// </summary>
		/// <param name="item"></param>
		protected override void SetEditObject(Object obj)
		{
			if (obj == null)
			{
				this.txtModuleSequenceEdit.Text			= "";
				this.txtModuleCodeEdit.Text				= "";
				this.txtModuleVersionEdit.Text			= "";
				this.drpModuleTypeEdit.SelectedIndex	= 0;
				this.drpModuleStatusEdit.SelectedIndex  = 0;
				this.txtHelpFileNameEdit.Text			= "";
				this.drpIsSystemEdit.SelectedIndex		= 0;
				this.drpIsActiveEdit.SelectedIndex		= 0;
				this.drpIsRestrain.SelectedIndex		= 0;
				this.txtModuleDescEdit.Text				= "";
				this.txtFormUrlEdit.Text				= "";
			
				try
				{
					this.drpParentModuleCodeEdit.SelectedValue = this.txtModuleCodeQuery.Text;
				}
				catch
				{
					this.drpParentModuleCodeEdit.SelectedIndex = 0;
				}

				return;
			}

			this.txtModuleSequenceEdit.Text		= ((Module)obj).ModuleSequence.ToString();
			this.txtModuleCodeEdit.Text			= ((Module)obj).ModuleCode;
			this.txtModuleVersionEdit.Text		= ((Module)obj).ModuleVersion;
			this.txtHelpFileNameEdit.Text		= ((Module)obj).ModuleHelpFileName;
			this.txtModuleDescEdit.Text			= ((Module)obj).ModuleDescription;
			this.txtFormUrlEdit.Text			= ((Module)obj).FormUrl;

			try
			{			
				this.drpIsSystemEdit.SelectedValue	= ((Module)obj).IsSystem.ToString();
			}
			catch
			{
				this.drpIsSystemEdit.SelectedIndex = 0;
			}

			try
			{
				this.drpIsActiveEdit.SelectedValue	= ((Module)obj).IsActive.ToString();
			}
			catch
			{
				this.drpIsActiveEdit.SelectedIndex = 0;
			}

			try
			{			
				this.drpIsRestrain.SelectedValue	= ((Module)obj).IsRestrain.ToString();
			}
			catch
			{
				this.drpIsRestrain.SelectedIndex = 0;
			}

			try
			{
				this.drpModuleStatusEdit.SelectedValue	= ((Module)obj).ModuleStatus;
			}
			catch
			{
				this.drpModuleStatusEdit.SelectedIndex = 0;
			}

			try
			{
				this.drpModuleTypeEdit.SelectedValue = ((Module)obj).ModuleType;
			}
			catch
			{
				this.drpParentModuleCodeEdit.SelectedIndex = 0;
			}

			try
			{
				this.drpParentModuleCodeEdit.SelectedValue = ((Module)obj).ParentModuleCode;
			}
			catch
			{
				this.drpParentModuleCodeEdit.SelectedIndex = 0;
			}
			
			//melo zheng 修改于2006.12.31 只有B/S有抑制显示功能
			if (this.drpModuleTypeEdit.SelectedIndex == 1)
			{
				this.drpIsRestrain.Enabled = false;
				this.drpIsRestrain.SelectedIndex = 0;
			}
			else
			{
				this.drpIsRestrain.Enabled = true;
			}
		}	

		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblModuleCodeEdit, txtModuleCodeEdit, 40, true) );
			manager.Add( new NumberCheck(lblModuleSequenceEdit, txtModuleSequenceEdit, true) );
			manager.Add( new LengthCheck(lblModuleTypeEdit, drpModuleTypeEdit, 40, true) );
			manager.Add( new LengthCheck(lblModuleStatusEdit, drpModuleStatusEdit, 40, true) );
			manager.Add( new LengthCheck(lblModuleVersionEdit, txtModuleVersionEdit, 40, false) );
			manager.Add( new LengthCheck(lblFormUrlEdit, txtFormUrlEdit, 100, false) );
			manager.Add( new LengthCheck(lblHelpFileNameEdit, txtHelpFileNameEdit, 100, false) );
			manager.Add( new LengthCheck(lblModuleDescEdit, txtModuleDescEdit, 100, false) );

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}
			return true;
		}

		#endregion

		#region Tree
		
		private ITreeObjectNode LoadModuleTreeToApplication()
		{	
			if( _facade == null )
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}	
			return this._facade.BuildModuleTree();
		}

		/// <summary>
		/// 构建Module树
		/// </summary>
		/// <param name="reload">是否重新从数据库中读取Module树</param>
		private void BuildModuleTree(bool reload)
		{
			this.treeModule.Nodes.Clear();

			if ( reload )
			{
				this.LoadModuleTreeToApplication();
			}

			ITreeObjectNode node = LoadModuleTreeToApplication();

			this.treeModule.Nodes.Add( BuildTreeNode(node) );

			LanguageWord lword  = this.languageComponent1.GetLanguage("moduleRoot");

			if ( lword != null )
			{
				this.treeModule.Nodes[0].Text = lword.ControlText;
			}

			//this.treeModule.ExpandAll();
			this.treeModule.CollapseAll();
			if (this.treeModule.SelectedNode != null)
			{
				Infragistics.WebUI.UltraWebNavigator.Node nodeParent = this.treeModule.SelectedNode.Parent;
				while (nodeParent != null)
				{
					nodeParent.Expand(false);
					nodeParent = nodeParent.Parent;
				}
			}

			this.BuildParentModuleCodeList();
		}

		private Infragistics.WebUI.UltraWebNavigator.Node BuildTreeNode(ITreeObjectNode treeNode)
		{
			Infragistics.WebUI.UltraWebNavigator.Node node = new Node();

			node.Text = treeNode.Text;
			node.Tag = treeNode;

			if ( treeNode.Text == this.txtModuleCodeQuery.Text )
			{
				this.treeModule.SelectedNode = node;
			}

			foreach( ITreeObjectNode subNode in treeNode.GetSubLevelChildrenNodes() )
			{
				node.Nodes.Add( BuildTreeNode(subNode) );
			}

			return node;
		}

        protected void treeModule_NodeSelectionChanged(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                this.txtModuleCodeQuery.Text = ((ModuleTreeNode)e.Node.Tag).Module.ModuleCode;
            }
            else
            {
                this.txtModuleCodeQuery.Text = "";
            }

            this.gridHelper.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);

            try
            {
                this.drpParentModuleCodeEdit.SelectedValue = this.txtModuleCodeQuery.Text;
            }
            catch
            {
                this.drpParentModuleCodeEdit.SelectedIndex = 0;
            }
        }
        /*
		private void treeModule_NodeClicked(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
		{
			if ( e.Node.Tag != null )
			{
				this.txtModuleCodeQuery.Text =  ((ModuleTreeNode)e.Node.Tag).Module.ModuleCode;
			}
			else
			{
				this.txtModuleCodeQuery.Text = "";
			}
			
			this.gridHelper.RequestData();
			this.buttonHelper.PageActionStatusHandle( PageActionType.Query );	
						
			try
			{
				this.drpParentModuleCodeEdit.SelectedValue = this.txtModuleCodeQuery.Text;
			}
			catch
			{
				this.drpParentModuleCodeEdit.SelectedIndex = 0;
			}
		}
        */
		private bool CheckParentModule(string parentModuleCode)
		{
			if ( parentModuleCode == "" )
			{
				return true;
			}

			ITreeObjectNode node = LoadModuleTreeToApplication().GetTreeObjectNodeByID(this.txtModuleCodeEdit.Text);

			if ( node == null )
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Node_Lost");
			}
			
			TreeObjectNodeSet set = node.GetAllNodes();

			foreach (ITreeObjectNode childNode in set)
			{
				if (childNode.ID.ToUpper() == parentModuleCode.ToUpper())
				{
					ExceptionManager.Raise(this.GetType(),"$Error_Parent_To_Children");
				}
			}
			
			return true;
		}

		#endregion

		#region 数据初始化
		private void BuildParentModuleCodeList()
		{
			DropDownListBuilder builder = new DropDownListBuilder(this.drpParentModuleCodeEdit);
			if( _facade == null )
			{
				_facade = new SystemSettingFacadeFactory(base.DataProvider).Create();
			}	
			builder.HandleGetObjectList = new GetObjectListDelegate(this._facade.GetAllModules);
			builder.Build("ModuleCode", "ModuleCode");

			this.drpParentModuleCodeEdit.Items.Insert(0, "");

			try
			{
				this.drpParentModuleCodeEdit.SelectedValue = this.txtModuleCodeQuery.Text;
			}
			catch
			{
				this.drpParentModuleCodeEdit.SelectedIndex = 0;
			}
		}

		protected void drpModuleTypeEdit_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				this.drpModuleTypeEdit.Items.Clear();

				if( InternalSystemVariable.Lookup("ModuleType") == null )
				{
					return;
				}
				
				foreach (string _Items in (InternalSystemVariable.Lookup("ModuleType").Items))
				{
                    drpModuleTypeEdit.Items.Add(_Items);
				}
																							
         	}
		}

		protected void drpModuleStatusEdit_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				this.drpModuleStatusEdit.Items.Clear();

				if((InternalSystemVariable.Lookup("ModuleStatus").Items).Count==0)
				{
					return;
				}
				
				foreach (string _Items in (InternalSystemVariable.Lookup("ModuleStatus").Items))
				{
					drpModuleStatusEdit.Items.Add(_Items);
				}
																							
			}
		}

        public void drpIsActiveEdit_Load()
        {
            this.drpIsActiveEdit.Items.Clear();
            this.drpIsActiveEdit.Items.Add(new ListItem(this.languageComponent1.GetString("1"), "1"));
            this.drpIsActiveEdit.Items.Add(new ListItem(this.languageComponent1.GetString("0"), "0"));
            this.drpIsActiveEdit.SelectedIndex = 0;
        }

        public void drpIsSystemEdit_Load()
        {
            this.drpIsSystemEdit.Items.Clear();
            this.drpIsSystemEdit.Items.Add(new ListItem(this.languageComponent1.GetString("1"), "1"));
            this.drpIsSystemEdit.Items.Add(new ListItem(this.languageComponent1.GetString("0"), "0"));
            this.drpIsSystemEdit.SelectedIndex = 0;
        }

        public void drpIsRestrain_Load()
        {
            this.drpIsRestrain.Items.Clear();
            this.drpIsRestrain.Items.Add(new ListItem(this.languageComponent1.GetString("0"), "0"));
            this.drpIsRestrain.Items.Add(new ListItem(this.languageComponent1.GetString("1"), "1"));
            this.drpIsRestrain.SelectedIndex = 0;
        }
		#endregion

//		private void treeModule_NodeCollapsed(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
//		{
//			e.Node.ImageUrl = string.Format("{0}skin/image/treenode1.gif", this.VirtualHostRoot);
//		}
//
//		private void treeModule_NodeExpanded(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeEventArgs e)
//		{
//			e.Node.ImageUrl = string.Format("{0}skin/image/treenode2.gif", this.VirtualHostRoot);		
//		}

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{   ((Module)obj).ModuleSequence.ToString(),
								   ((Module)obj).ModuleCode,
								   ((Module)obj).ModuleVersion,
								   ((Module)obj).ModuleType,
								   ((Module)obj).ModuleStatus,
								   ((Module)obj).ParentModuleCode,
								   ((Module)obj).ModuleHelpFileName,
								   languageComponent1.GetString(((Module)obj).IsSystem),
								   languageComponent1.GetString(((Module)obj).IsActive),
								   ((Module)obj).ModuleDescription,
								   ((Module)obj).FormUrl,
								   languageComponent1.GetString(((Module)obj).IsRestrain),
								   ((Module)obj).MaintainUser.ToString(),
								   FormatHelper.ToDateString(((Module)obj).MaintainDate)};
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"ModuleSequence",
									"ModuleCode",
									//"ModuleName",
									"ModuleVersion",
									"ModuleType",
									"ModuleStatus",
									"ParentModuleCode",
									"ModuleHelpFileName",
									"IsSystem",
									"IsActive",
									"ModuleDescription",
									"FormUrl",
									"IsRestrain",
									"MaintainUser",
									"MaintainDate"};
		}
		#endregion

		//melo zheng 修改于2006.12.29 只有B/S有抑制显示功能
		protected void drpModuleTypeEdit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.drpModuleTypeEdit.SelectedIndex == 1)
			{
				this.drpIsRestrain.Enabled = false;
				this.drpIsRestrain.SelectedIndex = 0;
			}
			else
			{
				this.drpIsRestrain.Enabled = true;
			}
		}
	}
}
