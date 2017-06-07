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
using System.Collections.Generic;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Domain.ReportView;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.ReportView
{
	/// <summary>
	/// FRptDesignStep4FilterUISP 的摘要说明。
	/// </summary>
    public partial class FRptDesignStep4FilterUISP : BaseMPageMinus
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
        //private GridHelper gridHelper = null;
	
		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
            this.gridWebGrid.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid_InitializeRow);
            this.cmdAdd.ServerClick+=new EventHandler(cmdAdd_ServerClick);
            this.cmdDeleteItem.ServerClick += new EventHandler(cmdDeleteItem_ServerClick);
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

            this.cmdSave.ServerClick += new EventHandler(cmdSave_ServerClick);
            this.drpDDLDynamicDataSource.SelectedIndexChanged += new EventHandler(drpDDLDynamicDataSource_SelectedIndexChanged);
		}
		#endregion

		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //PostBackTrigger triggerDataSource = new PostBackTrigger();
            //triggerDataSource.ControlID = this.drpDDLDynamicDataSource.ID;
            //(this.Form1.FindControl("up1") as UpdatePanel).Triggers.Add(triggerDataSource);
            //PostBackTrigger triggerQueryType = new PostBackTrigger();
            //triggerQueryType.ControlID = this.drpSelectQueryType.ID;
            //(this.Form1.FindControl("up1") as UpdatePanel).Triggers.Add(triggerQueryType);

            //PostBackTrigger triggerDDLDataSource = new PostBackTrigger();
            //triggerDDLDataSource.ControlID = this.drpDDLDataSource.ID;
            //(this.Form1.FindControl("up1") as UpdatePanel).Triggers.Add(triggerDDLDataSource);

            //PostBackTrigger triggerAdd = new PostBackTrigger();
            //triggerAdd.ControlID = this.cmdAdd.ID;
            //(this.Form1.FindControl("up1") as UpdatePanel).Triggers.Add(triggerAdd);

            //PostBackTrigger triggerDelete = new PostBackTrigger();
            //triggerDelete.ControlID = this.cmdDeleteItem.ID;
            //(this.Form1.FindControl("up1") as UpdatePanel).Triggers.Add(triggerDelete);

            ScriptManager.RegisterStartupScript(this, GetType(), "AdjustHeight", "AdjustHeight()", true);

            this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);
            this.drpReportFilterType.Attributes["onChange"] = "HideShowPanel();";
            this.drpDDLDataSource.Attributes["onChange"] = "HideShowDropDownListPanel();";
            if (this.IsPostBack == false)
            {  
                InitWebGrid();
               
                InitData();
                BuildDDLDataSource();
                InitSelectData();
                BuildSelectQueryType();
             
                this.txtReportName.Text = Server.UrlDecode(this.GetRequestParam("reportname"));
                this.txtFilterColumnName.Text = Server.UrlDecode(this.GetRequestParam("columndesc"));
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.lblFilterUITypeTitle.Text = this.languageComponent1.GetString("$PageControl_FilterUITypeTitle");
                this.lblProjectDesc.Value = this.languageComponent1.GetString("ProjectDesc");
                this.lblProjectValue.Value = this.languageComponent1.GetString("ProjectValue");
            }
            InitRow();
            this.gridWebGrid.Behaviors.CreateBehavior<Activation>().Enabled = true;
        }
        protected override void AddParsedSubObject(object obj)
        {
            this.needUpdatePanel = false;
            base.AddParsedSubObject(obj);
        }
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("DropDownListText", "显示文本",null);
            this.gridHelper.AddColumn("DropDownListValue", "值", null);
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["DropDownListText"].ReadOnly = false;
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["DropDownListValue"].ReadOnly = false;
            
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }
        void gridWebGrid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
        {
            string strScript = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                            e.Row.Index, e.Row.Items.FindItemByKey("DropDownListText").Column.Index);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript, true);

            string strScript2 = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                            e.Row.Index, e.Row.Items.FindItemByKey("DropDownListValue").Column.Index);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript2, true);
        }
        private void InitRow()
        {
            foreach (GridRecord row in gridWebGrid.Rows)
            {
                string strScript = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                                          row.Index, row.Items.FindItemByKey("DropDownListText").Column.Index);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript, true);

                string strScript2 = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                                row.Index, row.Items.FindItemByKey("DropDownListValue").Column.Index);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript2, true);
            }
        }
        private void InitData()
        {
            this.drpReportFilterType.Items.Add(new ListItem(this.languageComponent1.GetString(ReportFilterUIType.InputText), ReportFilterUIType.InputText));
            this.drpReportFilterType.Items.Add(new ListItem(this.languageComponent1.GetString(ReportFilterUIType.Date), ReportFilterUIType.Date));
            this.drpReportFilterType.Items.Add(new ListItem(this.languageComponent1.GetString(ReportFilterUIType.CheckBox), ReportFilterUIType.CheckBox));
            this.drpReportFilterType.Items.Add(new ListItem(this.languageComponent1.GetString(ReportFilterUIType.DropDownList), ReportFilterUIType.DropDownList));
            this.drpReportFilterType.Items.Add(new ListItem(this.languageComponent1.GetString(ReportFilterUIType.SelectQuery), ReportFilterUIType.SelectQuery));
            this.drpReportFilterType.Items.Add(new ListItem(this.languageComponent1.GetString(ReportFilterUIType.SelectComplex), ReportFilterUIType.SelectComplex));

            this.drpDDLDynamicDataSource.Items.Add(new ListItem("", ""));
            ReportViewFacade facade = new ReportViewFacade(this.DataProvider);
            object[] objs = facade.GetAllRptViewDataSource();
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    this.drpDDLDynamicDataSource.Items.Add(new ListItem(((RptViewDataSource)objs[i]).Name, ((RptViewDataSource)objs[i]).DataSourceID.ToString()));
                }
            }
        }

        private void InitSelectData()
        {
            string existValue = Request.QueryString["existsetting"];
            if (existValue == null || existValue == "")
                return;
            string[] valList = existValue.Split(';');
            this.drpReportFilterType.SelectedValue = valList[0];
            //Add 2008/11/04
            if (valList[valList.Length-1].ToString().ToUpper() == "Y")
            {
                this.cbxCheckMust.Checked = true;
            }
            else
            {
                this.cbxCheckMust.Checked = false;
            }
            //Add End

            if (valList[0] == ReportFilterUIType.SelectQuery)
            {
                this.drpSelectQueryType.SelectedValue = valList[1];
            }
            else if (valList[0] == ReportFilterUIType.DropDownList)
            {
                this.drpDDLDataSource.SelectedValue = valList[1];
                if (valList[1] == "static")
                {
                    for (int i = 2; i < valList.Length; i ++)
                    {
                        if (valList[i] != "" && valList[i].IndexOf(",") >= 0)
                        {
                            string[] strTmp = valList[i].Split(',');
                            DataRow row = DtSource.NewRow();
                            row["GUID"] = Guid.NewGuid().ToString();
                            row["DropDownListText"] = strTmp[0];
                            row["DropDownListValue"] = strTmp[1];
                            DtSource.Rows.Add(row);
                        }
                    }
                    this.gridWebGrid.DataSource = DtSource;
                    this.gridWebGrid.DataBind();
                }
                else
                {
                    this.drpDDLDynamicDataSource.SelectedValue = valList[2];
                    drpDDLDynamicDataSource_SelectedIndexChanged(null, null);
                    if (this.listHideDDLDynamicSourceColumnList.Items.FindByValue(valList[3]) != null)
                    {
                        this.txtHideDDLDynamicSourceText.Value = this.listHideDDLDynamicSourceColumnList.Items.FindByValue(valList[3]).Text;
                        this.hidHideDDLDynamicSourceText.Value = valList[3];
                    }
                    if (this.listHideDDLDynamicSourceColumnList.Items.FindByValue(valList[4]) != null)
                    {
                        this.txtHideDDLDynamicSourceValue.Value = this.listHideDDLDynamicSourceColumnList.Items.FindByValue(valList[4]).Text;
                        this.hidHideDDLDynamicSourceValue.Value = valList[4];
                    }
                }
            }
            //Add  2008/11/04
            else if (valList[0] == ReportFilterUIType.SelectComplex)
            {
                this.drpDDLDataSource.SelectedValue = valList[1];
                if (valList[1] == "static")
                {
                    for (int i = 2; i < valList.Length; i++)
                    {
                        if (valList[i] != "" && valList[i].IndexOf(",") >= 0)
                        {
                            string[] strTmp = valList[i].Split(',');
                            DataRow row = DtSource.NewRow();
                            row["GUID"] = Guid.NewGuid().ToString();
                            row["DropDownListText"] = strTmp[0];
                            row["DropDownListValue"] = strTmp[1];
                            DtSource.Rows.Add(row);
                        }
                        this.gridWebGrid.DataSource = DtSource;
                        this.gridWebGrid.DataBind();
                    }
                }
                else
                {
                    this.drpDDLDynamicDataSource.SelectedValue = valList[2];
                    drpDDLDynamicDataSource_SelectedIndexChanged(null, null);
                    if (this.listHideDDLDynamicSourceColumnList.Items.FindByValue(valList[3]) != null)
                    {
                        this.txtHideDDLDynamicSourceText.Value = this.listHideDDLDynamicSourceColumnList.Items.FindByValue(valList[3]).Text;
                        this.hidHideDDLDynamicSourceText.Value = valList[3];
                    }
                    if (this.listHideDDLDynamicSourceColumnList.Items.FindByValue(valList[4]) != null)
                    {
                        this.txtHideDDLDynamicSourceValue.Value = this.listHideDDLDynamicSourceColumnList.Items.FindByValue(valList[4]).Text;
                        this.hidHideDDLDynamicSourceValue.Value = valList[4];
                    }
                }

            }

        }
        #endregion

        void drpDDLDynamicDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtHideDDLDynamicSourceText.Value = "";
            this.hidHideDDLDynamicSourceText.Value = "";
            this.txtHideDDLDynamicSourceValue.Value = "";
            this.hidHideDDLDynamicSourceValue.Value = "";

            this.listHideDDLDynamicSourceColumnList.Items.Clear();
            if (this.drpDDLDynamicDataSource.SelectedValue == "")
                return;
            ReportViewFacade facade = new ReportViewFacade(this.DataProvider);
            object[] objs = facade.GetRptViewDataSourceColumnByDataSourceId(int.Parse(this.drpDDLDynamicDataSource.SelectedValue));
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    this.listHideDDLDynamicSourceColumnList.Items.Add(new ListItem(((RptViewDataSourceColumn)objs[i]).Description, ((RptViewDataSourceColumn)objs[i]).ColumnName));
                }
            }
        }

        void cmdSave_ServerClick(object sender, EventArgs e)
        {

        }

        protected void cmdAdd_ServerClick(object sender, EventArgs e)
        {
            for(int i=0;i<gridWebGrid.Rows.Count;i++)
            {
                GridRecord record = gridWebGrid.Rows[i];
                if(record.Items.FindItemByKey("GUID").Value.ToString()==DtSource.Rows[i]["GUID"].ToString())
                {
                    DtSource.Rows[i]["DropDownListText"]=record.Items.FindItemByKey("DropDownListText").Value.ToString();
                    DtSource.Rows[i]["DropDownListValue"]=record.Items.FindItemByKey("DropDownListValue").Value.ToString();
                }
            }
            DataRow row = DtSource.NewRow();
            row["GUID"] = Guid.NewGuid().ToString();
            row["DropDownListText"]="";
            row["DropDownListValue"] = "";
            DtSource.Rows.Add(row);
            this.gridWebGrid.DataSource = DtSource;
            this.gridWebGrid.DataBind();
        }

        protected void cmdDeleteItem_ServerClick(object sender, EventArgs e)
        {
            if (this.gridWebGrid.Behaviors.Activation.ActiveCell != null)
            {
                int rowIndex = this.gridWebGrid.Behaviors.Activation.ActiveCell.Row.Index;
                DtSource.Rows[rowIndex].Delete();
                this.gridWebGrid.DataSource = DtSource;
                this.gridWebGrid.DataBind();
            }
        }

        #region private
        private void BuildSelectQueryType()
        {
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_model"), "model"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_item"), "item"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_singleitem"), "singleitem"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_operation"), "operation"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_segment"), "segment"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_stepsequence"), "stepsequence"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_mo"), "mo"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_singlemo"), "singlemo"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_reworkmo"), "reworkmo"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_singlereworkmo"), "singlereworkmo"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_resource"), "resource"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_user"), "user"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_usermail"), "usermail"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_department"), "department"));
            this.drpSelectQueryType.Items.Add(new ListItem(this.languageComponent1.GetString("SelectQueryType_lot"), "lot"));

        }

        private void BuildDDLDataSource()
        {
            this.drpDDLDataSource.Items.Add(new ListItem(this.languageComponent1.GetString("DDLDataSource_static"), "static"));
            this.drpDDLDataSource.Items.Add(new ListItem(this.languageComponent1.GetString("DDLDataSource_datasource"), "datasource"));
        }

        #endregion

    }
}
