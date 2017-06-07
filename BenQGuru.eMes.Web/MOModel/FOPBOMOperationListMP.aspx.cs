#region system
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
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FOPBOMOperationListMP 的摘要说明。
	/// </summary>
	public partial class FOPBOMOperationListMP : BasePage
	{
		protected System.Web.UI.WebControls.Label lblSelectBOMItemTitle;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
		//private ModelFacade _modelFacade = FacadeFactory.CreateModelFacade();
		private ItemFacade _itemFacade ;//= FacadeFactory.CreateItemFacade();
		private OPBOMFacade _opBOMFacade;// = FacadeFactory.CreateOPBOMFacade();

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!IsPostBack)
            {
                InitializeComponent();
                this.InitPageLanguage(this.languageComponent1, false);
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.AutoCloseConnection = false;
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.OpenConnection();
                try
                {
                    Initparamters();
                    BuildOPBOM();
                    InitTreeView();
                }
                finally
                {
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.CloseConnection();
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.AutoCloseConnection = true;
                }
			}
		}

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
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
            // 

        }
        #endregion


		#region private method
		private void Initparamters()
		{
			if(Request.Params["itemcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["itemcode"] = Request.Params["itemcode"].ToString();
			}
		}
		public string ItemCode
		{
			get
			{
				return (string)ViewState["itemcode"];
			}
		}

		private void BuildOPBOM()
		{
			if(_opBOMFacade==null){_opBOMFacade = new FacadeFactory(base.DataProvider).CreateOPBOMFacade();}
			this._opBOMFacade.BuildOPBOM(ItemCode);
		}

		private void InitTreeView()
		{

			uwtreeOPBOM.Nodes.Clear();

			Node tmpNode = new Node();
			tmpNode.Text = ItemCode;
			tmpNode.Tag = this.getTreeNodeTag(null,string.Empty);
			tmpNode.TargetFrame ="OPBOMFrame";
			tmpNode.TargetUrl ="";
			Node rootNode = uwtreeOPBOM.Nodes.Add(tmpNode);

			BuildRouteNode(rootNode);

			rootNode.Expand(true);
		}


		private void BuildRouteNode(Node rootNode)
		{
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
            object[] objs = _itemFacade.QueryItem2Route(ItemCode, string.Empty, GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString());
			if(objs != null)
			{
				for(int i=0;i<objs.Length;i++)
				{
					Node tmpRouteNode = new Node();
					tmpRouteNode.Text = ((Item2Route)objs[i]).RouteCode;
					//tmpRouteNode.Tag  = ((Item2Route)objs[i]).RouteCode;
					tmpRouteNode.Tag  = this.getTreeNodeTag(objs[i],string.Empty);
					tmpRouteNode.TargetFrame ="OPBOMFrame";
					tmpRouteNode.TargetUrl = "";
					Node routeNode = rootNode.Nodes.Add(tmpRouteNode);
					BuildComponentLoadingOperationNodes(routeNode,((Item2Route)objs[i]).RouteCode);
				}
			}
		}

		private void BuildComponentLoadingOperationNodes(Node routeNode,string routeCode)
		{
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			object[] Lodingobjs = this._itemFacade.GetComponenetLoadingOperations(ItemCode,routeCode);
			object[] Downobjs = this._itemFacade.GetComponenetDownOperations(ItemCode,routeCode);
			this.AddLoadingNode(routeNode,routeCode,Lodingobjs);
			this.AddDownNode(routeNode,routeCode,Downobjs);
			
		}

		private string getTreeNodeTag(object _nodeobj,string optype)
		{
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			string defaultmsg = "  ";
			if(_nodeobj == null)return defaultmsg;
			if(_nodeobj.GetType() == typeof(Item2Route))
			{
				return defaultmsg;
			}
			else if(_nodeobj.GetType() == typeof(ItemRoute2OP))
			{
				//string optype = "上料";
				string opcode = ((ItemRoute2OP)_nodeobj).OPCode;
				string opcontrol = ((ItemRoute2OP)_nodeobj).OPControl;
//				if(_itemFacade.IsDownOperation(opcontrol))
//				{
//					optype = "下料";
//				}
                return string.Format(@"{0}    {1} {2} {3} {4}", defaultmsg, this.languageComponent1.GetString("$PageControl_CurrentProcess"), opcode, this.languageComponent1.GetString("$PageControl_IS"), optype);
			}
			return defaultmsg;
		}

		//添加上料工序
		private void AddLoadingNode(Node routeNode,string routeCode,object[] objs)
		{
			if(objs == null)return;
			Node tmpRouteNode = new Node();
            tmpRouteNode.Text = this.languageComponent1.GetString("$PageControl_FeedProcess");
			tmpRouteNode.Tag  = this.getTreeNodeTag(null,string.Empty);
			tmpRouteNode.TargetFrame ="OPBOMFrame";
			tmpRouteNode.TargetUrl = "";
			Node LoadingNode = routeNode.Nodes.Add(tmpRouteNode);
			//工序类型 上料0
			string ActionType = "0";
			
			if(objs != null)
			{
				for(int i=0;i<objs.Length;i++)
				{
					Node tmpNode = new Node();//((Model2OP)objs[i]).OPCode,((Model2OP)objs[i]).OPID
					tmpNode.Text = ((ItemRoute2OP)objs[i]).GetDisplayText("OPCode");
					//tmpNode.Tag =  ((ItemRoute2OP)objs[i]).OPID;
					tmpNode.Tag = this.getTreeNodeTag(objs[i],tmpRouteNode.Text);
					tmpNode.TargetFrame = "OPBOMFrame";
					//sammer kong
					tmpNode.TargetUrl = 
						this.MakeRedirectUrl(
						"FOPBOMOperationComponetLoadingMP.aspx",
						new string[]{ "itemCode","opbomcode","opbomversion","routecode","opid" ,"actiontype","OrgID"},
                        new string[] { Server.UrlEncode(ItemCode), Server.UrlEncode(((ItemRoute2OP)objs[i]).RouteCode), OPBOMFacade.OPBOMVERSION_DEFAULT, Server.UrlEncode(((ItemRoute2OP)objs[i]).RouteCode), Server.UrlEncode(((ItemRoute2OP)objs[i]).OPID), ActionType, ((ItemRoute2OP)objs[i]).OrganizationID.ToString() });
					LoadingNode.Nodes.Add(tmpNode);
				}
			}
		}

		//下料工序
		private void AddDownNode(Node routeNode,string routeCode,object[] objs)
		{
			if(objs == null)return;
			Node tmpRouteNode = new Node();
            tmpRouteNode.Text = this.languageComponent1.GetString("$PageControl_CuttingProcess");
			tmpRouteNode.Tag  = this.getTreeNodeTag(null,string.Empty);
			tmpRouteNode.TargetFrame ="OPBOMFrame";
			tmpRouteNode.TargetUrl = "";
			Node DownNode = routeNode.Nodes.Add(tmpRouteNode);

			//工序类型 下料1
			string ActionType = "1";
			if(objs != null)
			{
				for(int i=0;i<objs.Length;i++)
				{
					Node tmpNode = new Node();//((Model2OP)objs[i]).OPCode,((Model2OP)objs[i]).OPID
					tmpNode.Text = ((ItemRoute2OP)objs[i]).OPCode;
					//tmpNode.Tag =  ((ItemRoute2OP)objs[i]).OPID;
					tmpNode.Tag = this.getTreeNodeTag(objs[i],tmpRouteNode.Text);
					tmpNode.TargetFrame = "OPBOMFrame";
					//sammer kong
					tmpNode.TargetUrl = 
						this.MakeRedirectUrl(
						"FOPBOMOperationComponetLoadingMP.aspx",
						new string[]{ "itemCode","opbomcode","opbomversion","routecode","opid" ,"actiontype","OrgID"},
                        new string[] { Server.UrlEncode(ItemCode), Server.UrlEncode(((ItemRoute2OP)objs[i]).RouteCode), OPBOMFacade.OPBOMVERSION_DEFAULT, Server.UrlEncode(((ItemRoute2OP)objs[i]).RouteCode), Server.UrlEncode(((ItemRoute2OP)objs[i]).OPID), ActionType, ((ItemRoute2OP)objs[i]).OrganizationID.ToString() });
					DownNode.Nodes.Add(tmpNode);
				}
			}
		}

		#endregion
	}
}
