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
using System.Xml;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using Infragistics.WebUI.UltraWebNavigator;
using Infragistics.WebUI.UltraWebNavigator;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FConfigTree 的摘要说明。
	/// </summary>
	public partial class FConfigTree : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
	
		public string ItemCode
		{
			get
			{
				if( this.ViewState["ITEMCODE"] == null )
					return string.Empty;
				else return this.ViewState["ITEMCODE"].ToString() ;
			}
			set
			{
				this.ViewState["ITEMCODE"] = value ;
			}
		}

		public string ItemConfig
		{
			get
			{
				if( this.ViewState["ItemConfig"] == null )
					return string.Empty;
				else return this.ViewState["ItemConfig"].ToString() ;
			}
			set
			{
				this.ViewState["ItemConfig"] = value ;
			}
		}

        public int OrgID
        {
            get
            {
                if (this.ViewState["OrgID"] == null)
                    return GlobalVariables.CurrentOrganizations.First().OrganizationID;
                else return int.Parse(this.ViewState["OrgID"].ToString());
            }
            set
            {
                this.ViewState["OrgID"] = value;
            }
        }

		protected void Page_Load(object sender, System.EventArgs e)
		{
//			this.ItemCode = this.GetRequestParam("ITEMCODE");
//			this.ItemConfig = this.GetRequestParam("ItemConfig");

			this.ItemCode = (string)Session["ITEMCODE"];
			this.ItemConfig = (string)Session["ItemConfig"];

			this.BuildConfigTree();

			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
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
			this.ConfigTreeView.NodeChecked += new Infragistics.WebUI.UltraWebNavigator.NodeCheckedEventHandler(this.ConfigTreeView_NodeChecked);
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

		private void BuildConfigTree()
		{
			ConfigTreeView.Nodes.Clear();

			//添加根结点
			ConfigTreeView.Nodes.Add (string.Format("{0}:{1}",ItemCode, ItemConfig));
			this.BuildConfigTreeByXML();

			ConfigTreeView.ExpandAll();
		}

		private void BuildConfigTreeByXML()
		{
			BenQGuru.eMES.MOModel.ItemFacade itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();
			string path = this.Request.MapPath("")+@"\Item2Config.xml";

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(path);

			/* 添加第一层 */
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Category");
			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					string text = nodeList[i].Attributes["value"].InnerText;
					string tag = nodeList[i].Attributes["key"].InnerText;
					
					Node node = new Node();
					node.Text = text ;
					node.Tag = tag ;
					ConfigTreeView.Nodes[0].Nodes.Add( node );

					if( nodeList[i].ChildNodes !=null && nodeList[i].ChildNodes.Count > 0 )
					{
						for( int j=0; j<nodeList[i].ChildNodes.Count; j++ )
						{
							string text2 = nodeList[i].ChildNodes[j].Attributes["value"].InnerText;
							string tag2 = nodeList[i].ChildNodes[j].Attributes["key"].InnerText;

							Node node2 = new Node();
							
							node2.Text = text2 ;
							if(itemFacade.IsConfiged( this.ItemCode, this.ItemConfig,tag, tag2))
							{
								node2.Style.Font.Bold = true;

								node2.CheckBox = CheckBoxes.True ;
								node2.Checked = itemFacade.IsMustCheckConfiged( this.ItemCode, this.ItemConfig,tag, tag2) ;
							}
							else
							{
								node2.Style.Font.Bold = false;
								node2.CheckBox = CheckBoxes.False;
							}
							node2.Tag = tag2 ;
							node2.TargetFrame = "ConfigFrame";
							node2.TargetUrl = this.MakeRedirectUrl("FItem2ConfigMP.aspx",
                                new string[] { "ITEMCODE", "ITEMCONFIG", "OrgID", "PARENTCODE", "PARENTNAME", "CONFIGCODE", "CONFIGNAME" },
                                new string[] { ItemCode, ItemConfig, OrgID.ToString(), tag, text, tag2, text2 });

							Item2Config item2Config = itemFacade.CreateNewItem2Config();
							item2Config.ItemCode = this.ItemCode;
							item2Config.ItemConfigration = this.ItemConfig ;
							item2Config.ParentCode = tag;
							item2Config.ParentName = text;
							item2Config.ConfigCode = tag2;
							item2Config.ConfigName = text2;

                            item2Config.OrganizationID = this.OrgID;
							
							node2.DataKey = item2Config;

							node.Nodes.Add( node2 );
						}
					}
				}
			}

			/* 根据Category，加载CheckItem */
		}

		//复选框改变状态时,同步树和右边的明细列表 joe song 20060328
		private void ConfigTreeView_NodeChecked(object sender, Infragistics.WebUI.UltraWebNavigator.WebTreeNodeCheckedEventArgs e)
		{
			Item2Config ic= e.Node.DataKey as Item2Config;
			
			BenQGuru.eMES.MOModel.ItemFacade itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();
			object[] objs = itemFacade.QueryItem2Config(ic.ItemCode,ic.ItemConfigration,ic.ParentCode,ic.ConfigCode,string.Empty,0,int.MaxValue);
			if(objs != null)
			{
				foreach(Item2Config item2Config in objs)
				{  
					if(item2Config != null)
					{
						item2Config.NeedCheck = e.Node.Checked?FormatHelper.TRUE_STRING:FormatHelper.FALSE_STRING;
						item2Config.MaintainUser = this.GetUserCode();

						itemFacade.UpdateItem2Config(item2Config);
					}
				}
			}
			string url =this.MakeRedirectUrl("FItem2ConfigMP.aspx",
							new string[]{ "ITEMCODE","ITEMCONFIG","OrgID","PARENTCODE","PARENTNAME","CONFIGCODE","CONFIGNAME" },
							new string[]{ ic.ItemCode,ic.ItemConfigration,OrgID.ToString(), ic.ParentCode,ic.ParentName,ic.ConfigCode,ic.ConfigName });

			Page.RegisterStartupScript(Guid.NewGuid().ToString(),
										string.Format("<script>window.parent.ConfigFrame.location.replace('{0}');</script>",url)
										);
		}

		
	}
}
