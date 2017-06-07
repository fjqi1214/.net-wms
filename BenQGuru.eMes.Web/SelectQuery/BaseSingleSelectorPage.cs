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

using Infragistics.WebUI.UltraWebGrid ;

using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Web.Helper ;
using BenQGuru.eMES.Web.UserControl ;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// Selector 的摘要说明。
	/// </summary>
	public class BaseSingleSelectorPage : BasePage
	{
		protected System.ComponentModel.IContainer components;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		protected GridHelper gridSelectedHelper = null ;
		protected GridHelper gridUnSelectedHelper = null ;

		// 分隔符
		protected const string DATA_SPLITER = "," ;
		protected bool writerOutted = false ;



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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Init
		private void Page_Load(object sender, System.EventArgs e)
		{
			Control control ;
			control = this.FindControl("cmdUnSelect") ;
			if(control != null)
			{
				((System.Web.UI.WebControls.Button)control).Click += new System.EventHandler(this.cmdUnSelect_Click);
			}
			control = this.FindControl("cmdSelect") ;
			if(control != null)
			{
				((System.Web.UI.WebControls.Button)control).Click += new System.EventHandler(this.cmdSelect_Click);
			}
			control = this.FindControl("cmdQuery") ;
			if(control != null)
			{
				((System.Web.UI.HtmlControls.HtmlInputButton)control).ServerClick += new System.EventHandler(this.cmdQuery_ServerClick);
			}

			control = this.FindControl("chbSelected") ;
			if(control==null)
			{
				this.gridSelectedHelper = new GridHelper( this.GetGridSelected() ) ;
			}
			else
			{
				this.gridSelectedHelper = new GridHelper( this.GetGridSelected() ,(System.Web.UI.WebControls.CheckBox) control ) ;
			}

			control = this.FindControl("chbUnSelected") ;
			if(control==null)
			{
				this.gridUnSelectedHelper = new GridHelper( this.GetGridUnSelected()  ) ;
			}
			else
			{
				this.gridUnSelectedHelper = new GridHelper( this.GetGridUnSelected()  ,(System.Web.UI.WebControls.CheckBox) control ) ;
			}


			this.gridSelectedHelper.BuildGridRowhandle = new BuildGridRowDelegate( this.GetSelectedGridRow ) ;
			this.gridSelectedHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadSelectedDataSource) ;

			this.gridUnSelectedHelper.BuildGridRowhandle = new BuildGridRowDelegate( this.GetUnSelectedGridRow ) ;
			this.gridUnSelectedHelper.GetRowCountHandle = new GetRowCountDelegate( this.GetUnSelectedRowCount ) ;
			this.gridUnSelectedHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadUnSelectedDataSource) ;

			control = this.FindControl("cmdSave") ;
			if(control!=null)
			{
				((System.Web.UI.HtmlControls.HtmlInputButton)control).Attributes.Add("OnClick","try{window.returnValue=document.getElementById('txtSelected').innerText;window.close();return false ;}catch(e){}") ;
			}
			control = this.FindControl("cmdCancel") ;
			if(control!=null)
			{
				((System.Web.UI.HtmlControls.HtmlInputButton)control).Attributes.Add("OnClick","window.close();return false ;") ;
			}

			control = this.FindControl("cmdInit") ;
			if(control!=null)
			{
				((System.Web.UI.HtmlControls.HtmlInputButton)control).ServerClick +=  new System.EventHandler(this.cmdInit_ServerClick);
			}

			if(! this.IsPostBack )
			{
				this.InitWebGrid() ;
				this.cmdQuery_ServerClick( null, null);
			}

			SelectableTextBox_Load(null,null);

		}

		#endregion

		#region WebGrid
        
		protected virtual void InitWebGrid()
		{
			this.gridSelectedHelper.AddColumn( "Selector_SelectedCode", "已选择的项目",	null);
			this.gridSelectedHelper.AddColumn( "Selector_SelectedDesc", "描述",	null);
			this.gridSelectedHelper.AddDefaultColumn(true,false) ;
			this.gridSelectedHelper.ApplyLanguage( this.languageComponent1 );

			this.gridUnSelectedHelper.AddColumn( "Selector_UnselectedCode", "未选择的项目",	null);
			this.gridUnSelectedHelper.AddColumn( "Selector_UnSelectedDesc", "描述",	null);
			this.gridUnSelectedHelper.AddDefaultColumn(true,false) ;
			this.gridUnSelectedHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected virtual Infragistics.WebUI.UltraWebGrid.UltraGridRow GetSelectedGridRow(object obj)
		{
			return null ;
		}

		protected virtual Infragistics.WebUI.UltraWebGrid.UltraGridRow GetUnSelectedGridRow(object obj)
		{
			return null ;
		}

		protected virtual object[] LoadSelectedDataSource(int inclusive, int exclusive)
		{
			return null ;
		}

		protected virtual object[] LoadUnSelectedDataSource(int inclusive, int exclusive)
		{
			return null ;
		}

		protected virtual int GetUnSelectedRowCount()
		{
			return 0 ;
		}

        


		#endregion

		#region Misc
		protected void cmdUnSelect_Click(object sender, System.EventArgs e)
		{
			ArrayList rows = this.gridSelectedHelper.GetCheckedRows() ;
			foreach( UltraGridRow row in rows )
			{
				this.gridSelectedHelper.Grid.Rows.Remove( row ) ;
			}


			this.RequestData() ;

		}

		protected void cmdSelect_Click(object sender, System.EventArgs e)
		{
			ArrayList rows = this.gridUnSelectedHelper.GetCheckedRows() ;
			foreach( UltraGridRow row in rows )
			{
				UltraGridRow newRow = new UltraGridRow(
					new object[]{"",row.Cells[1].Text,row.Cells[2].Text}
					) ;
				this.gridSelectedHelper.Grid.Rows.Add( newRow ) ;
			}
            

			this.RequestData() ;
		}


		protected string[] GetSelectedCodes()
		{
			return new string[0] ;
            //Control control = this.FindControl("txtSelected");
            //if (control == null)
            //{
            //    return new string[0];
            //}

            //else
            //{
            //    if (((System.Web.UI.HtmlControls.HtmlTextArea)control).Value.Trim().Length == 0)
            //    {
            //        return new string[0];
            //    }
            //    return ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value.Split(DATA_SPLITER.ToCharArray());

            //}
		}

		private void SetSelectedCodes()
		{
			return;
            //string[] codes = new string[this.gridSelectedHelper.Grid.Rows.Count];
            //for (int i = 0; i < codes.Length; i++)
            //{
            //    codes[i] = this.gridSelectedHelper.Grid.Rows[i].Cells[1].Text;
            //}

            //Control control = this.FindControl("txtSelected");
            //if (control == null)
            //{
            //    return;
            //}

            //else
            //{
            //    ((System.Web.UI.HtmlControls.HtmlTextArea)control).Value = string.Join(DATA_SPLITER, codes);
            //}
		}


		protected override void Render(HtmlTextWriter writer)
		{
			base.Render (writer);
			if(!writerOutted)
			{
				//writer.Write("<script language=javascript>try{ResetSelectAllPosition('chbUnSelected','gridUnSelected');ResetSelectAllPosition('chbSelected','gridSelected');}catch(e){};</script>");
				//writer.Write("<script language=javascript>try{if(window.top.valueLoaded != true){document.getElementById('txtSelected').innerText = window.top.dialogArguments.Codes;document.getElementById('cmdInit').click();window.top.valueLoaded = true ;}}catch(e){};</script>");
			}
		}

		protected void cmdQuery_ServerClick(object sender, System.EventArgs e)
		{
			RequestData();
		}

		private void RequestData()
		{
			SetSelectedCodes() ;

			this.gridUnSelectedHelper.RequestData() ;
			this.gridSelectedHelper.RequestData();

		}

		protected virtual Infragistics.WebUI.UltraWebGrid.UltraWebGrid GetGridUnSelected()
		{
			Control control = this.FindControl("gridUnselected") ;
			if(control == null)
			{
				return null ;
			}
			return (Infragistics.WebUI.UltraWebGrid.UltraWebGrid) control ;
		}

		protected virtual Infragistics.WebUI.UltraWebGrid.UltraWebGrid GetGridSelected()
		{
			Control control = this.FindControl("gridSelected") ;
			if(control == null)
			{
				return null ;
			}
			return (Infragistics.WebUI.UltraWebGrid.UltraWebGrid) control ;
		}


		private void cmdInit_ServerClick(object sender, System.EventArgs e)
		{
			this.gridSelectedHelper.RequestData() ;
			SetSelectedCodes() ;

		}
		#endregion

		#region 注册客户端事件

		private void SelectableTextBox_Load(object sender, EventArgs e)
		{
			if(!this.Page.IsStartupScriptRegistered("SelectableTextBox_Startup_js"))
			{
				string scriptString = string.Format("<script>var STB_Virtual_Path = \"{0}\";</script><script src='{0}SelectQuery/selectableTextBox.js'></script>",this.VirtualHostRoot ) ;
                
				this.Page.RegisterStartupScript("SelectableTextBox_Startup_js", scriptString);
			}

		}

		#endregion
	}
}

