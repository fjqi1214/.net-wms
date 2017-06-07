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
using System.Xml ;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;

using Infragistics.WebUI.UltraWebGrid ;

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FItem2ConfigMP 的摘要说明。
	/// </summary>
	public partial class FItem2ConfigMP : BasePage 
	{

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
		private System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		private ItemFacade _itemFacade;

		#region Attributes
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
				if( this.ViewState["ITEMCONFIG"] == null )
					return string.Empty;
				else return this.ViewState["ITEMCONFIG"].ToString() ;
			}
			set
			{
				this.ViewState["ITEMCONFIG"] = value ;
			}
		}

		public string ParentCode
		{
			get
			{
				if( this.ViewState["PARENTCODE"] == null )
					return string.Empty;
				else return this.ViewState["PARENTCODE"].ToString() ;
			}
			set
			{
				this.ViewState["PARENTCODE"] = value ;
			}
		}

		public string ParentName
		{
			get
			{
				if( this.ViewState["PARENTNAME"] == null )
					return string.Empty;
				else return this.ViewState["PARENTNAME"].ToString() ;
			}
			set
			{
				this.ViewState["PARENTNAME"] = value ;
			}
		}
		
		public string ConfigCode
		{
			get
			{
				if( this.ViewState["CONFIGCODE"] == null )
					return string.Empty;
				else return this.ViewState["CONFIGCODE"].ToString() ;
			}
			set
			{
				this.ViewState["CONFIGCODE"] = value ;
			}
		}

		public string ConfigName
		{
			get
			{
				if( this.ViewState["CONFIGNAME"] == null )
					return string.Empty;
				else return this.ViewState["CONFIGNAME"].ToString() ;
			}
			set
			{
				this.ViewState["CONFIGNAME"] = value ;
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

		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitPostBack();

			if(!Page.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				this.InitWebGrid();
				this.ItemCode = this.GetRequestParam("ITEMCODE");
				this.ItemConfig = this.GetRequestParam("ITEMCONFIG");
				this.ParentCode = this.GetRequestParam("PARENTCODE");
				this.ParentName = this.GetRequestParam("PARENTNAME");
				this.ConfigCode = this.GetRequestParam("CONFIGCODE");
				this.ConfigName = this.GetRequestParam("CONFIGNAME");
                this.OrgID = int.Parse(this.GetRequestParam("ORGID").ToString());

				this.lblTitle.Text += this.ConfigName;
				
				this.InitDDL();
				this.RequestData();
			}

			
		}

		private void InitPostBack()
		{
			this.buttonHelper = new ButtonHelper(this);
			this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
			this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.pagerToolBar.OnPagerToolBarClick += new EventHandler(this.PagerToolBar_OnPagerToolBarClick);
		}

		private void InitWebGrid()
		{
			//update by crystal chu 2005/04/26
			this.gridHelper.AddColumn("CONFIGVALUE","标准值",null);
			this.gridHelper.AddColumn("NEEDCHECK","是否比对",null);
			this.gridHelper.AddColumn("MUSER","维护人员",null);
			this.gridHelper.AddColumn("MDATE","维护日期",null);
			

			// 2005-04-06
			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
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
			this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
			this.gridWebGrid.ClickCellButton += new Infragistics.WebUI.UltraWebGrid.ClickCellButtonEventHandler(this.gridWebGrid_ClickCellButton);
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
			this.excelExporter.FileExtension = "xls";
			this.excelExporter.LanguageComponent = this.languageComponent1;
			this.excelExporter.Page = this;
			this.excelExporter.RowSplit = "\r\n";

		}
		#endregion

		private object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			return _itemFacade.QueryItem2Config(this.ItemCode,this.ItemConfig,this.ParentCode,this.ConfigCode,"",this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);

		}

		private int GetRowCount()
		{
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			return _itemFacade.QueryItem2ConfigCount(this.ItemCode,this.ItemConfig,this.ParentCode,this.ConfigCode,"");
		}

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			Item2Config item2Config = obj as Item2Config ;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								"false",
								item2Config.ConfigValue,
								this.languageComponent1.GetString( item2Config.NeedCheck==FormatHelper.TRUE_STRING?"trueText":"falseText" ),
								item2Config.MaintainUser,
								FormatHelper.ToDateString( item2Config.MaintainDate ),
								""
							});
		}

		private void SetEditObject(object obj)
		{
			if( obj == null )
			{
				this.txtCompare.SelectedValue = string.Empty;
				this.chbIsNeedCheck.Checked = false ;
				return ;
			}

			this.txtCompare.SelectedValue = ( obj as Item2Config ).ConfigValue;
			this.chbIsNeedCheck.Checked = string.Compare( (obj as Item2Config).NeedCheck,FormatHelper.FALSE_STRING)==1?true:false  ;
		}

		private void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Update )
			{
				
			}
			else if ( pageAction == PageActionType.Save )
			{
				
			}
			else if(pageAction ==PageActionType.Cancel)
			{
				
			}
		}

		private void PagerToolBar_OnPagerToolBarClick(object sender, System.EventArgs e)
		{
			this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize);
		}

		protected void cmdAdd_ServerClick(object sender, EventArgs e)
		{
			if( this.txtCompare.SelectedValue.Length == 0 ) return;
			
			object item2Config = this.GetEditObject();
			if(item2Config != null)
			{
				if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			
				_itemFacade.AddItem2Config( item2Config as Item2Config );
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Add );
			}
			this.ReFlashTreePage();
		}

		protected void cmdSave_ServerClick(object sender, EventArgs e)
		{
			if( this.txtCompare.SelectedValue.Length == 0 ) return;
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			object item2Config = this.GetEditObject();
			if(item2Config != null)
			{
				this._itemFacade.UpdateItem2Config( item2Config as Item2Config ) ;
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Save );
			}
			this.ReFlashTreePage();
		}

		protected void cmdCancel_ServerClick(object sender, EventArgs e)
		{
			this.buttonHelper.PageActionStatusHandle( PageActionType.Cancel );
		}

		protected void cmdDelete_ServerClick(object sender, EventArgs e)
		{
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			ArrayList array = this.gridHelper.GetCheckedRows();
			if( array.Count > 0 )
			{

				ArrayList item2Configs = new ArrayList( array.Count );
			
				foreach (UltraGridRow row in array)
				{
					object item2Config = this.GetEditObject(row);
					if( item2Config != null )
					{
						item2Configs.Add( item2Config as Item2Config );
					}
				}
				
				this._itemFacade.DeleteItem2Config( (Item2Config[])item2Configs.ToArray( typeof(Item2Config) ) );
				
				this.gridHelper.GridBind( this.pagerToolBar.PageIndex, this.pagerToolBar.PageSize );
				this.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
			}
			this.ReFlashTreePage();
		}

		private void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( this.chbSelectAll.Checked )
			{
				this.gridHelper.CheckAllRows( CheckStatus.Checked );
			}
			else
			{
				this.gridHelper.CheckAllRows( CheckStatus.Unchecked );
			}
		}

		private void RequestData()
		{
			this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
			this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
			this.pagerToolBar.RowCount = GetRowCount();
			this.pagerToolBar.InitPager();
		}

		private  object GetEditObject()
		{
			
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			Item2Config item2Config = _itemFacade.CreateNewItem2Config();
			item2Config.ItemCode = this.ItemCode;
			item2Config.ItemConfigration = this.ItemConfig ;
			item2Config.ParentCode = this.ParentCode ;
			item2Config.ParentName = this.ParentName ;
			item2Config.ConfigCode = this.ConfigCode ;
			item2Config.ConfigName = this.ConfigName ;
			item2Config.ConfigValue = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtCompare.SelectedValue,40 ) ) ;
			item2Config.NeedCheck = this.chbIsNeedCheck.Checked?FormatHelper.TRUE_STRING:FormatHelper.FALSE_STRING;
			item2Config.IsLeaf = FormatHelper.TRUE_STRING ;
			item2Config.Level = 3 ;
			item2Config.MaintainUser = this.GetUserCode();
            item2Config.OrganizationID = this.OrgID;

			return item2Config;
		}

		private object GetEditObject( Infragistics.WebUI.UltraWebGrid.UltraGridRow row )
		{
			if(_itemFacade==null){_itemFacade = new FacadeFactory(base.DataProvider).CreateItemFacade();}
			object obj = _itemFacade.GetItem2Config( this.ItemCode,this.ItemConfig,this.ParentCode,this.ConfigCode,
				row.Cells.FromKey("CONFIGVALUE").Text, this.OrgID );
			
			if (obj != null)
			{
				return obj as Item2Config;
			}

			return null;
		}

		private void gridWebGrid_ClickCellButton(object sender, Infragistics.WebUI.UltraWebGrid.CellEventArgs e)
		{
			if(e.Cell.Column.Key =="Edit")
			{
				object obj = this.GetEditObject(e.Cell.Row);

				if ( obj != null )
				{
					this.SetEditObject( obj );

					this.buttonHelper.PageActionStatusHandle( PageActionType.Update );	
				}
			}
		}

		private void InitDDL()
		{
			this.txtCompare.Items.Clear();
			this.txtCompare.Items.Add( new ListItem("","") );

			string path = this.Request.MapPath("")+@"\Item2Config.xml";
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(path);

			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("CheckItem");
			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					XmlNode node = nodeList[i];
					if( node.Attributes["key"].InnerText.ToString() == this.ConfigCode )
					{
						if( node.HasChildNodes )
						{
							for( int j=0; j<node.ChildNodes.Count; j++ )
							{
								string sValue = node.ChildNodes[j].Attributes["value"].InnerText ;
								ListItem listItem = new ListItem(sValue.ToUpper(),sValue.ToUpper());
								this.txtCompare.Items.Add( listItem ) ;
							}
						}
						break ;
					}
				}
			}
		}

		private void ReFlashTreePage()
		{
			string url = this.MakeRedirectUrl ("FConfigTree.aspx",new string[] {"ITEMCODE","ItemConfig","OrgID"},new string[] {this.ItemCode,this.ItemConfig,this.OrgID.ToString()});
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append ("<script language=\"javascript\">");
			stringBuilder.AppendFormat("window.parent.ConfigTreeFrame.location.replace('{0}');",url);
			stringBuilder.Append ("</script>");
			this.RegisterClientScriptBlock (Guid.NewGuid().ToString(), stringBuilder.ToString());
		}
	}
}
