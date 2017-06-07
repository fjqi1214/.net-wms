#region system;
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
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FItemRouteOperationListMP 的摘要说明。
	/// </summary>
	public partial class FItemRouteOperationListMP : BasePage
	{
		private ItemFacade _itemFacade ;//= FacadeFactory.CreateItemFacade();
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		//private ModelFacade _modelFacade = FacadeFactory.CreateModelFacade();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				InitParameters();
				//BuildItemRoute();
				BuildTreeView();
			}
		}

		private void InitParameters()
		{
			if(this.Request.Params["itemcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["itemcode"] = this.Request.Params["itemcode"];
			}
		}

		public void BuildItemRoute()
		{
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			_itemFacade.BuildItemRoute(ItemCode);
		}

		public void BuildTreeView()
		{
		
			this.uwtreeItemRoute.Nodes.Clear();
			//build root node
			Node tmpNode = new Node();
            tmpNode.Text = ItemDisplayText;
			tmpNode.Tag = ItemCode;
			tmpNode.TargetFrame = "ItemRouteFrame";
			//tmpNode.TargetUrl = "FItemRouteSP.aspx?itemcode="+ItemCode;
			tmpNode.TargetUrl = this.MakeRedirectUrl("FItemRouteSP.aspx",new string[] {"itemcode","rp"},new string[] {Server.UrlEncode(ItemCode),Guid.NewGuid().ToString()});
			Node rootNode = uwtreeItemRoute.Nodes.Add(tmpNode);

			//build root node for route
			BuildRouteNode(rootNode);
			rootNode.Expand(true);
		}

		private void BuildRouteNode(Node rootNode)
		{
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			object[] objs = _itemFacade.QueryItem2Route(ItemCode,string.Empty,GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString());
			//object[] objso = null;
			if(objs != null)
			{
				for(int i=0;i<objs.Length;i++)
				{
					Node tmpRouteNode = new Node();
					tmpRouteNode.Text = ((Item2Route)objs[i]).RouteCode;
					tmpRouteNode.Tag  = ((Item2Route)objs[i]).RouteCode;
					tmpRouteNode.TargetFrame ="ItemRouteFrame";
					tmpRouteNode.TargetUrl = "";
					Node routeNode = rootNode.Nodes.Add(tmpRouteNode);
					BuildOperationNode(routeNode,((Item2Route)objs[i]).RouteCode);
				}
			}
		}

		private void BuildOperationNode(Node routeNode,string routeCode)
		{
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			object[] objs = _itemFacade.QueryItem2Operation(ItemCode,routeCode);
			if(objs != null)
			{
				for(int i=0;i<objs.Length;i++)
				{
					Node tmpOperationNode = new Node();
					tmpOperationNode.Text = ((ItemRoute2OP)objs[i]).OPCode;
					tmpOperationNode.Tag  = ((ItemRoute2OP)objs[i]).OPID;
					tmpOperationNode.TargetFrame = "ItemRouteFrame";
					tmpOperationNode.TargetUrl = 
						this.MakeRedirectUrl(
						"FItemRouteOperationEP.aspx",
						new string[]{ "OPID" },
						new string[]{ Server.UrlEncode(((ItemRoute2OP)objs[i]).OPID) } );
					routeNode.Nodes.Add(tmpOperationNode);
				}
			}
		}


		public string ItemCode
		{
			get 
			{
				return (string) this.ViewState["itemcode"];
			}
		}

        public string ItemDisplayText
        {
            get
            {
                if (_itemFacade == null) 
                {
                    _itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade(); 
                }
                Domain.MOModel.Item item = (Domain.MOModel.Item)_itemFacade.GetItem((string)this.ViewState["itemcode"], GlobalVariables.CurrentOrganizations.First().OrganizationID);
                if (item == null)
                {
                    return (string)this.ViewState["itemcode"];
                }
                else
                {
                    return item.GetDisplayText("ItemCode");
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

	
	}
}
