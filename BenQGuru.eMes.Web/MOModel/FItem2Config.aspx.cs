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
using Infragistics.WebUI.UltraWebNavigator;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FItem2Config 的摘要说明。
	/// </summary>
	public partial class FItem2Config : BasePage
	{
		protected Infragistics.WebUI.UltraWebNavigator.UltraWebTree ConfigTreeView;
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
			this.ItemCode = this.GetRequestParam("ITEMCODE");
			this.ItemConfig = this.GetRequestParam("ItemConfig");

			Session["ITEMCODE"] = this.ItemCode;
			Session["ItemConfig"] = this.ItemConfig;

			//this.BuildConfigTree();

			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
		}

		
		/// <summary>
		/// 执行客户端的函数
		/// </summary>
		/// <param name="FunctionName">函数名</param>
		/// <param name="FunctionParam">参数</param>
		/// <param name="Page">当前页面的引用</param>
		public  void ExecuteClientFunction(string FunctionName,string FunctionParam)
		{
			try
			{
				string _msg = string.Empty;
				if(FunctionParam != string.Empty)
					_msg = string.Format("<script language='JavaScript'>  {0}('{1}');</script>",FunctionName,FunctionParam);
				else
					_msg = string.Format("<script language='JavaScript'>  {0}();</script>",FunctionName);

				//将Key值设为随机数,防止脚本重复
				Page.RegisterStartupScript(Guid.NewGuid().ToString(),_msg);
			}
			catch(Exception ex)
			{
				throw ex;
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
							node2.CheckBox = CheckBoxes.True ;
							node2.Checked = itemFacade.IsConfiged( this.ItemCode, this.ItemConfig,tag, tag2) ;
							node2.Text = text2 ;
							if(itemFacade.IsMustCheckConfiged( this.ItemCode, this.ItemConfig,tag, tag2))
							{
								node2.Style.Font.Bold = true;
							}
							node2.Tag = tag2 ;
							node2.TargetFrame = "ConfigFrame";
							node2.TargetUrl = this.MakeRedirectUrl("FItem2ConfigMP.aspx",
								new string[]{ "ITEMCODE","ITEMCONFIG","OrgID","PARENTCODE","PARENTNAME","CONFIGCODE","CONFIGNAME" },
                                new string[] { ItemCode, ItemConfig, OrgID.ToString(), tag, text, tag2, text2 });
							node.Nodes.Add( node2 );
						}
					}
				}
			}

			/* 根据Category，加载CheckItem */
		}
	}
}
