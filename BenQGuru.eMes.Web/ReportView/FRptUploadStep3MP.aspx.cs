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
using Infragistics.Web.UI.ListControls;

namespace BenQGuru.eMES.Web.ReportView
{
	/// <summary>
	/// FRptUploadStep3MP 的摘要说明。
	/// </summary>
    public partial class FRptUploadStep3MP : ReportWizardBasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
        //private GridHelper gridHelper = null;
        private object ActiveRowIndex
        {
            get
            {
                if (this.gridWebGrid.Behaviors.CreateBehavior<Activation>().ActiveCell == null)
                    return null;
                ViewState["ActiveRowIndex"] = this.gridWebGrid.Behaviors.Activation.ActiveCell.Row.Index;
                return this.ViewState["ActiveRowIndex"];
            }

            set { this.ViewState["ActiveRowIndex"] = value; }
        }

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
            gridWebGrid.InitializeRow+=new InitializeRowEventHandler(gridWebGrid_InitializeRow);
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

            this.imgSelect.ServerClick += new ImageClickEventHandler(imgSelect_ServerClick);
            this.imgUnSelect.ServerClick += new ImageClickEventHandler(imgUnSelect_ServerClick);
            this.imgUp.ServerClick += new ImageClickEventHandler(SetColumnUp);
            this.imgDown.ServerClick += new ImageClickEventHandler(SetColumnDown);
        }

		#endregion

        private bool bIsSqlDataSource = false;

		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.gridHelper = new GridHelperNew(this.gridWebGrid,DtSource);
            if (this.IsPostBack == false)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                string strFileName = this.designView.UploadFileName;
                strFileName = strFileName.Substring(strFileName.LastIndexOf("\\") + 1);
                this.txtReportFile.Text = strFileName;
                this.txtReportName.Text = this.designView.DesignMain.ReportName;

                this.InitColumnList();
                //InitGrid();
                
            }
            if (this.gridWebGrid.Columns.FromKey("FilterType") != null)
            {
                DropDownProvider dropdownProvider = this.gridWebGrid.EditorProviders.GetProviderById("dropdownProviderFilterType") as DropDownProvider;
                BindDropDownFilterType(dropdownProvider);
                dropdownProvider.EditorControl.DropDownContainerWidth = new Unit(150);
            }
            InitializeBackGroundColor();
        }
        protected void InitializeBackGroundColor()
        {
            foreach (GridRecord row in gridWebGrid.Rows)
            {
                string strScript = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                               row.Index, row.Items.FindItemByKey("DisplayDesc").Column.Index);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript, true);
                if (bIsSqlDataSource == true)
                {
                    string strScript2 = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                                    row.Index, row.Items.FindItemByKey("FilterType").Column.Index);

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript2, true);
                }
            }
        }
        private void BindDropDownFilterType(DropDownProvider dropdownProvider)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FilterTypeValue");
            dt.Columns.Add("FilterTypeText");
            dt.AcceptChanges();
            DataRow rowEqual = dt.NewRow();
            rowEqual["FilterTypeValue"] = ReportFilterType.Equal;
            rowEqual["FilterTypeText"] = this.languageComponent1.GetString(ReportFilterType.Equal);
            DataRow rowGreater = dt.NewRow();
            rowGreater["FilterTypeValue"] = ReportFilterType.Greater;
            rowGreater["FilterTypeText"] = this.languageComponent1.GetString(ReportFilterType.Greater);
            DataRow rowGreaterEqual = dt.NewRow();
            rowGreaterEqual["FilterTypeValue"] = ReportFilterType.GreaterEqual;
            rowGreaterEqual["FilterTypeText"] = this.languageComponent1.GetString(ReportFilterType.GreaterEqual);
            DataRow rowLesser = dt.NewRow();
            rowLesser["FilterTypeValue"] = ReportFilterType.Lesser;
            rowLesser["FilterTypeText"] = this.languageComponent1.GetString(ReportFilterType.Lesser);
            DataRow rowLesserEqual = dt.NewRow();
            rowLesserEqual["FilterTypeValue"] = ReportFilterType.LesserEqual;
            rowLesserEqual["FilterTypeText"] = this.languageComponent1.GetString(ReportFilterType.LesserEqual);
            DataRow rowLeftMatch = dt.NewRow();
            rowLeftMatch["FilterTypeValue"] = ReportFilterType.LeftMatch;
            rowLeftMatch["FilterTypeText"] = this.languageComponent1.GetString(ReportFilterType.LeftMatch);
            DataRow rowRightMatch = dt.NewRow();
            rowRightMatch["FilterTypeValue"] = ReportFilterType.RightMatch;
            rowRightMatch["FilterTypeText"] = this.languageComponent1.GetString(ReportFilterType.RightMatch);
            dt.Rows.Add(rowEqual);
            dt.Rows.Add(rowGreater);
            dt.Rows.Add(rowGreaterEqual);
            dt.Rows.Add(rowLesser);
            dt.Rows.Add(rowLesserEqual);
            dt.Rows.Add(rowLeftMatch);
            dt.Rows.Add(rowRightMatch);
            dropdownProvider.EditorControl.ValueField = "FilterTypeValue";
            dropdownProvider.EditorControl.TextField = "FilterTypeText";
            dropdownProvider.EditorControl.DataSource = dt;
            dropdownProvider.EditorControl.DataBind();
        }
        private void InitColumnList()
        {
            this.lstUnSelectColumn.Items.Clear();
            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            RptViewDataSource dataSource = (RptViewDataSource)rptFacade.GetRptViewDataSource(this.designView.DesignMain.DataSourceID);
            if (dataSource.SourceType == DataSourceType.SQL)
            {
                RptViewDataSourceColumn[] columns = rptFacade.GetRptViewDataSourceColumnByDataSourceId(Convert.ToInt32(this.designView.DesignMain.DataSourceID));
                for (int i = 0; i < columns.Length; i++)
                {
                    if (FormatHelper.StringToBoolean(columns[i].Visible) == true)
                    {
                        this.lstUnSelectColumn.Items.Add(new System.Web.UI.WebControls.ListItem(columns[i].Description, columns[i].ColumnName));
                    }
                }
                bIsSqlDataSource = true;
            }
            else if (dataSource.SourceType == DataSourceType.DLL)
            {
                RptViewDataSourceParam[] parames = rptFacade.GetRptViewDataSourceParamByDataSourceId(Convert.ToInt32(this.designView.DesignMain.DataSourceID));
                for (int i = 0; parames != null && i < parames.Length; i++)
                {
                    this.lstUnSelectColumn.Items.Add(new System.Web.UI.WebControls.ListItem(parames[i].Description, parames[i].ParameterName));
                }
            }
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("Sequence", "次序", null);
            this.gridHelper.AddColumn("ColumnName", "栏位名称", null);
            this.gridHelper.AddColumn("DisplayDesc", "显示提示", null);
            this.gridHelper.AddColumn("FilterType", "过滤方式", null);
            this.gridHelper.AddColumn("InputUIType", "输入方式", null);

            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["DisplayDesc"].ReadOnly = false;
            ((BoundDataField)this.gridWebGrid.Columns.FromKey("InputUIType")).HtmlEncode = false;
            //this.gridWebGrid.Columns.FromKey("DisplayDesc").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
            //this.gridWebGrid.Columns.FromKey("DisplayDesc").CellStyle.BackColor = Color.FromArgb(255, 252, 240);

            DropDownProvider dropdownProvider = new DropDownProvider();
            dropdownProvider.ID = "dropdownProviderFilterType";
            dropdownProvider.EditorControl.ID = "editorControlFilterType";
            dropdownProvider.EditorControl.DisplayMode = DropDownDisplayMode.DropDownList;
            dropdownProvider.EditorControl.TextField = "FilterTypeText";
            dropdownProvider.EditorControl.ValueField = "FilterTypeValue";
            dropdownProvider.EditorControl.DropDownContainerWidth = new Unit(150);
            BindDropDownFilterType(dropdownProvider);
            this.gridWebGrid.EditorProviders.Add(dropdownProvider);
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["FilterType"].EditorID = "dropdownProviderFilterType";
            if (bIsSqlDataSource == true)
            {
                this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["FilterType"].ReadOnly = false;
            }
            
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }
        void gridWebGrid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
        {
            string strScript = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                            e.Row.Index, e.Row.Items.FindItemByKey("DisplayDesc").Column.Index);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript, true);
            if (bIsSqlDataSource == true)
            {
                string strScript2 = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                                e.Row.Index, e.Row.Items.FindItemByKey("FilterType").Column.Index);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript2, true);
            }
        }
        protected override void DisplayDesignData()
        {
            string strFilterUI = "";
            if (this.designView.GridFilters != null)
            {
                for (int i = 0; i < this.designView.GridFilters.Length; i++)
                {
                    string strName = "";
                    string strFilterType = "";
                    if (string.IsNullOrEmpty(this.designView.GridFilters[i].ColumnName) == false)
                    {
                        strName = this.designView.GridFilters[i].ColumnName;
                        strFilterType = this.designView.GridFilters[i].FilterOperation;
                    }
                    else
                    {
                        strName = this.designView.GridFilters[i].ParameterName;
                        strFilterType = ReportFilterType.Equal;
                    }
                    System.Web.UI.WebControls.ListItem item = this.lstUnSelectColumn.Items.FindByValue(strName);
                    if (item == null)
                        continue;
                    DataRow row = DtSource.NewRow();
                    row["GUID"] = Guid.NewGuid().ToString();
                    row["Sequence"] = this.designView.GridFilters[i].FilterSequence;
                    row["ColumnName"] = strName;
                    row["DisplayDesc"] = this.designView.GridFilters[i].Description;
                    row["FilterType"] = strFilterType;
                    row["InputUIType"] = "<a href='#' onclick=\"OpenTotalSetWindow('" + this.gridWebGrid.Rows.Count + "');return false;\">Click</a>";
                    this.DtSource.Rows.Add(row);
                    this.gridWebGrid.DataSource = DtSource;
                    this.gridWebGrid.DataBind();

                    for (int n = 0; this.designView.FiltersUI != null && n < this.designView.FiltersUI.Length; n++)
                    {
                        if (this.designView.FiltersUI[n].InputType == ReportViewerInputType.SqlFilter ||
                            this.designView.FiltersUI[n].InputType == ReportViewerInputType.DllParameter)
                        {
                            if (this.designView.FiltersUI[n].InputName == strName
                                && this.designView.FiltersUI[n].SqlFilterSequence == this.designView.GridFilters[i].FilterSequence)
                            {
                                strFilterUI += strName + "@" + this.designView.FiltersUI[n].UIType + ";";
                                if (this.designView.FiltersUI[n].UIType == ReportFilterUIType.SelectQuery)
                                {
                                    strFilterUI += this.designView.FiltersUI[n].SelectQueryType + ";";
                                }
                                else if (this.designView.FiltersUI[n].UIType == ReportFilterUIType.DropDownList)
                                {
                                    strFilterUI += this.designView.FiltersUI[n].ListDataSourceType + ";";
                                    if (this.designView.FiltersUI[n].ListDataSourceType == "static")
                                    {
                                        strFilterUI += this.designView.FiltersUI[n].ListStaticValue + ";";
                                    }
                                    else
                                    {
                                        strFilterUI += Convert.ToInt32(this.designView.FiltersUI[n].ListDynamicDataSource).ToString() + ";" + this.designView.FiltersUI[n].ListDynamicTextColumn + ";" + this.designView.FiltersUI[n].ListDynamicValueColumn + ";";
                                    }
                                }
                                strFilterUI += "|";
                            }
                        }
                    }
                }
            }
            this.hidInputUIType.Value = strFilterUI;
        }

        protected override bool ValidateInput()
        {
            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                if (this.gridWebGrid.Rows[i].Items.FindItemByKey("DisplayDesc").Text == "")
                {
                    string strHeader = this.gridWebGrid.Columns.FromKey("DisplayDesc").Key;
                    WebInfoPublish.Publish(this, strHeader + " $Error_Input_Empty", this.languageComponent1);
                    return false;
                }
            }
            return true;
        }

        protected override void RedirectToBack()
        {
            this.Response.Redirect("FRptUploadStep2MP.aspx");
        }

        protected override void RedirectToNext()
        {
            this.Response.Redirect("FRptUploadStep4MP.aspx");
        }

        protected override void UpdateReportDesignView()
        {
            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            RptViewDataSource dataSource = (RptViewDataSource)rptFacade.GetRptViewDataSource(this.designView.DesignMain.DataSourceID);
            if (dataSource.SourceType == DataSourceType.SQL)
            {
                bIsSqlDataSource = true;
            }
            ArrayList listFilterUI = new ArrayList();
            Dictionary<string, string> filterUIList = new Dictionary<string, string>();
            string strFilterUI = this.hidInputUIType.Value;
            string[] strFilterUIList = strFilterUI.Split('|');
            for (int i = 0; i < strFilterUIList.Length; i++)
            {
                if (strFilterUIList[i] != "")
                {
                    string[] strTmpList = strFilterUIList[i].Split('@');
                    if (filterUIList.ContainsKey(strTmpList[0]) == false)
                        filterUIList.Add(strTmpList[0], strTmpList[1]);
                }
            }

            RptViewGridFilter[] filters = new RptViewGridFilter[this.gridWebGrid.Rows.Count];
            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                RptViewGridFilter filter = new RptViewGridFilter();
                filter.FilterSequence = i + 1;
                filter.DataSourceID = this.designView.DesignMain.DataSourceID;
                string strName = "";
                if (bIsSqlDataSource == true)
                {
                    filter.ColumnName = this.gridWebGrid.Rows[i].Items.FindItemByKey("ColumnName").Value.ToString();
                    filter.FilterOperation = this.gridWebGrid.Rows[i].Items.FindItemByKey("FilterType").Value.ToString();
                    strName = filter.ColumnName;
                }
                else
                {
                    filter.ParameterName = this.gridWebGrid.Rows[i].Items.FindItemByKey("ColumnName").Value.ToString();
                    strName = filter.ParameterName;
                }
                filter.Description = this.gridWebGrid.Rows[i].Items.FindItemByKey("DisplayDesc").Value.ToString();
                filters[i] = filter;

                if (filterUIList.ContainsKey(strName) == true)
                {
                    RptViewFilterUI filterUI = new RptViewFilterUI();
                    filterUI.Sequence = listFilterUI.Count + 1;
                    if (bIsSqlDataSource == true)
                        filterUI.InputType = ReportViewerInputType.SqlFilter;
                    else
                        filterUI.InputType = ReportViewerInputType.DllParameter;
                    filterUI.InputName = strName;
                    filterUI.SqlFilterSequence = filter.FilterSequence;
                    string[] strUIValList = filterUIList[strName].Split(';');
                    filterUI.UIType = strUIValList[0];
                    if (filterUI.UIType == ReportFilterUIType.SelectQuery)
                        filterUI.SelectQueryType = strUIValList[1];
                    else if (filterUI.UIType == ReportFilterUIType.DropDownList)
                    {
                        filterUI.ListDataSourceType = strUIValList[1];
                        if (filterUI.ListDataSourceType == "static")
                        {
                            string strStaticVal = "";
                            for (int n = 2; n < strUIValList.Length; n++)
                            {
                                if (strUIValList[n] != "" && strUIValList[n].IndexOf(",") >= 0)
                                {
                                    strStaticVal += strUIValList[n] + ";";
                                }
                            }
                            filterUI.ListStaticValue = strStaticVal;
                        }
                        else
                        {
                            filterUI.ListDynamicDataSource = decimal.Parse(strUIValList[2]);
                            filterUI.ListDynamicTextColumn = strUIValList[3];
                            filterUI.ListDynamicValueColumn = strUIValList[4];
                        }
                    }
                    listFilterUI.Add(filterUI);
                }
            }
            this.designView.GridFilters = filters;

            for (int i = 0; this.designView.FiltersUI != null && i < this.designView.FiltersUI.Length; i++)
            {
                if (this.designView.FiltersUI[i].InputType == ReportViewerInputType.FileParameter)
                    listFilterUI.Add(this.designView.FiltersUI[i]);
            }
            RptViewFilterUI[] targetFilterUI = new RptViewFilterUI[listFilterUI.Count];
            listFilterUI.CopyTo(targetFilterUI);
            this.designView.FiltersUI = targetFilterUI;
        }

        #endregion

        void imgSelect_ServerClick(object sender, ImageClickEventArgs e)
        {
            for (int i = this.lstUnSelectColumn.Items.Count - 1; i >= 0; i--)
            {
                if (this.lstUnSelectColumn.Items[i].Selected == true)
                {
                    this.AddRowToGrid(this.lstUnSelectColumn.Items[i].Value, this.lstUnSelectColumn.Items[i].Text);
                    this.lstUnSelectColumn.Items.RemoveAt(i);
                }
            }
        }
        private void AddRowToGrid(string columnName, string columnDesc)
        {
            DataRow row = DtSource.NewRow();
            row["GUID"] = Guid.NewGuid().ToString();
            row["Sequence"] = this.gridWebGrid.Rows.Count + 1;
            row["ColumnName"] = columnName;
            row["DisplayDesc"] = columnDesc;
            row["FilterType"] = "reportfiltertype_equal";
            row["InputUIType"] = "<a href='#' onclick=\"OpenTotalSetWindow('" + this.gridWebGrid.Rows.Count + "');return false;\">Click</a>";
            this.DtSource.Rows.Add(row);
            this.gridWebGrid.DataSource = DtSource;
            this.gridWebGrid.DataBind();
        }

        void imgUnSelect_ServerClick(object sender, ImageClickEventArgs e)
        {
            if (this.gridWebGrid.Behaviors.CreateBehavior<Activation>().ActiveCell == null)
                return;
            int activeRowIndex = this.gridWebGrid.Behaviors.Activation.ActiveCell.Row.Index;
            DataRow row = DtSource.Rows[activeRowIndex];
            string columnName = row["ColumnName"].ToString();
            string columnDesc = row["DisplayDesc"].ToString();
            this.lstUnSelectColumn.Items.Add(new System.Web.UI.WebControls.ListItem(columnDesc, columnName));
            this.DtSource.Rows.Remove(row);
            for (int i = 0; i < this.DtSource.Rows.Count; i++)
            {
                this.DtSource.Rows[i]["Sequence"] = (i + 1).ToString();
            }
            this.gridWebGrid.DataSource = DtSource;
            this.gridWebGrid.DataBind();   
        }

        protected void lstUnSelectColumn_Init(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "lstUnSelectColumn_Init();", true);
        }

        protected void SetColumnDown(object sender, ImageClickEventArgs e)
        {
            if (ActiveRowIndex == null)
                return;
            int activeRowIndex = (int)ActiveRowIndex;
            //if (activeRowIndex == (this.DtSource.Rows.Count - 1))
            //    return;
            DataRow row1 = DtSource.Rows[activeRowIndex];
            DataRow tempRow = DtSource.NewRow();
            tempRow["ColumnName"] = row1["ColumnName"];
            tempRow["DisplayDesc"] = row1["DisplayDesc"];
            tempRow["FilterType"] = row1["FilterType"];
            tempRow["InputUIType"] = row1["InputUIType"];
            row1["ColumnName"] = DtSource.Rows[activeRowIndex - 1]["ColumnName"];
            row1["DisplayDesc"] = DtSource.Rows[activeRowIndex - 1]["DisplayDesc"];
            row1["FilterType"] = DtSource.Rows[activeRowIndex - 1]["FilterType"];
            row1["InputUIType"] = DtSource.Rows[activeRowIndex - 1]["InputUIType"];
            DataRow row2 = DtSource.Rows[activeRowIndex - 1];
            row2["ColumnName"] = tempRow["ColumnName"];
            row2["DisplayDesc"] = tempRow["DisplayDesc"];
            row2["FilterType"] = tempRow["FilterType"];
            row2["InputUIType"] = tempRow["InputUIType"];
            this.gridWebGrid.DataSource = DtSource;
            this.gridWebGrid.DataBind();
        }

        protected void SetColumnUp(object sender, ImageClickEventArgs e)
        {
            if (ActiveRowIndex == null)
                return;
            int activeRowIndex = (int)ActiveRowIndex;
            //if (activeRowIndex == 0)
            //    return;
            DataRow row1 = DtSource.Rows[activeRowIndex];
            DataRow tempRow = DtSource.NewRow();
            tempRow["ColumnName"] = row1["ColumnName"];
            tempRow["DisplayDesc"] = row1["DisplayDesc"];
            tempRow["FilterType"] = row1["FilterType"];
            tempRow["InputUIType"] = row1["InputUIType"];
            row1["ColumnName"] = DtSource.Rows[activeRowIndex + 1]["ColumnName"];
            row1["DisplayDesc"] = DtSource.Rows[activeRowIndex + 1]["DisplayDesc"];
            row1["FilterType"] = DtSource.Rows[activeRowIndex + 1]["FilterType"];
            row1["InputUIType"] = DtSource.Rows[activeRowIndex + 1]["InputUIType"];
            DataRow row2 = DtSource.Rows[activeRowIndex + 1];
            row2["ColumnName"] = tempRow["ColumnName"];
            row2["DisplayDesc"] = tempRow["DisplayDesc"];
            row2["FilterType"] = tempRow["FilterType"];
            row2["InputUIType"] = tempRow["InputUIType"];
            this.gridWebGrid.DataSource = DtSource;
            this.gridWebGrid.DataBind();
        }
    }
}
