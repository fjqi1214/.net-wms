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

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Domain.ReportView;

namespace BenQGuru.eMES.Web.ReportView
{
	/// <summary>
	/// FRptDesignStep2MP 的摘要说明。
	/// </summary>
    public partial class FRptDesignStep2MP : ReportWizardBasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
	
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
		
		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // 初始化页面语言
            this.InitPageLanguage(this.languageComponent1, false);       

            this.imgSelect.Attributes.Add("onclick", "MoveUserGroup('" + this.lstUnSelectedColumn.ClientID + "','" + this.lstSelectedColumn.ClientID + "')");
            this.imgUnSelect.Attributes.Add("onclick", "MoveUserGroup('" + this.lstSelectedColumn.ClientID + "','" + this.lstUnSelectedColumn.ClientID + "')");
            if (this.IsPostBack == false)
            {
                ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
                RptViewDataSourceColumn[] columns = rptFacade.GetRptViewDataSourceColumnByDataSourceId(Convert.ToInt32(this.designView.DesignMain.DataSourceID));
                for (int i = 0; columns != null && i < columns.Length; i++)
                {
                    if (FormatHelper.StringToBoolean(columns[i].Visible) == true)
                    {
                        this.lstUnSelectedColumn.Items.Add(new ListItem(columns[i].Description, columns[i].ColumnName));
                    }
                }
                this.lblDesignStep2Title.Text = this.languageComponent1.GetString("$PageControl_DesignStep2Title");
            }
		}

        protected override void DisplayDesignData()
        {
            string strSelectedValue = "";
            if (this.designView.GridColumns != null)
            {
                for (int i = 0; i < this.designView.GridColumns.Length; i++)
                {
                    ListItem item = this.lstUnSelectedColumn.Items.FindByValue(this.designView.GridColumns[i].ColumnName);
                    if (item != null)
                    {
                        strSelectedValue += item.Value + ";";
                        this.lstSelectedColumn.Items.Add(new ListItem(item.Text, item.Value));
                        this.lstUnSelectedColumn.Items.Remove(item);
                    }
                }
                this.hidSelectedValue.Value = strSelectedValue;
            }
        }

        protected override bool ValidateInput()
        {
            return true;
        }

        protected override void RedirectToBack()
        {
            this.Response.Redirect("FRptDesignStep1MP.aspx?isback=1");
        }

        protected override void RedirectToNext()
        {
            this.Response.Redirect("FRptDesignStep3MP.aspx");
        }

        protected override void UpdateReportDesignView()
        {
            string[] strSelected = this.hidSelectedValue.Value.Split(';');
            ArrayList list = new ArrayList();
            for (int i = 0; i < strSelected.Length; i++)
            {
                if (strSelected[i].Trim() != "")
                {
                    RptViewGridColumn column = new RptViewGridColumn();
                    column.DataSourceID = this.designView.DesignMain.DataSourceID;
                    column.DisplaySequence = list.Count + 1;
                    column.ColumnName = strSelected[i].Trim();
                    list.Add(column);
                }
            }
            if (list.Count == 0)
                throw new Exception("$ReportDesign_Select_ColumnName");
            RptViewGridColumn[] columns = new RptViewGridColumn[list.Count];
            list.CopyTo(columns);
            this.designView.GridColumns = columns;
        }

        #endregion




    }
}
