using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.ReportView;
using System.Drawing;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.Domain.ReportView;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.ListControls;
using System.Collections.Generic;

namespace BenQGuru.eMES.Web.ReportView
{
    public partial class FRptViewDataSourceColumn : BaseMPageNew
    {
        #region Web 窗体设计器生成的代码
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private ReportViewFacade _facade = null;
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
            this.gridWebGrid.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid_InitializeRow);
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
            this.languageComponent1.LanguagePackageDir = "D:\\code\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                if (_facade == null)
                {
                    _facade = new ReportViewFacade(base.DataProvider);
                }
                object obj = _facade.GetRptViewDataSource(Convert.ToDecimal(this.GetRequestParam("id")));
                txtDBName.Text = ((RptViewDataSource)obj).Name.ToString();
                txtDllName.Text = ((RptViewDataSource)obj).DllFileName.ToString();
                if (((RptViewDataSource)obj).SourceType == DataSourceType.SQL)
                {
                    txtDllName.Text = ((RptViewDataSource)obj).SQL.ToString();
                }
                datasourceid.Value = this.GetRequestParam("id");
                //_facade.SetColumn(this.MapPath(""), this.txtDllName.Text, decimal.Parse(datasourceid.Value), this.GetUserCode());
            }
            if (this.gridWebGrid.Columns.FromKey("DataType") != null)
            {
                DropDownProvider dropdownProvider = this.gridWebGrid.EditorProviders.GetProviderById("dropdownProviderDataType") as DropDownProvider;
                BindDropDownDataType(dropdownProvider);
                dropdownProvider.EditorControl.DropDownContainerWidth = new Unit(150);
            }
        }
        private void BindDropDownDataType(DropDownProvider dropdownProvider)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DataTypeValue");
            dt.Columns.Add("DataTypeText");
            dt.AcceptChanges();
            DataRow rowDate = dt.NewRow();
            rowDate["DataTypeValue"] = ReportDataType.Date;
            rowDate["DataTypeText"] = this.languageComponent1.GetString(ReportDataType.Date);
            DataRow rowNumeric = dt.NewRow();
            rowNumeric["DataTypeValue"] = ReportDataType.Numeric;
            rowNumeric["DataTypeText"] = this.languageComponent1.GetString(ReportDataType.Numeric);
            DataRow rowString = dt.NewRow();
            rowString["DataTypeValue"] = ReportDataType.String;
            rowString["DataTypeText"] = this.languageComponent1.GetString(ReportDataType.String);
            dt.Rows.Add(rowDate);
            dt.Rows.Add(rowNumeric);
            dt.Rows.Add(rowString);
            dropdownProvider.EditorControl.ValueField = "DataTypeValue";
            dropdownProvider.EditorControl.TextField = "DataTypeText";
            dropdownProvider.EditorControl.DataSource = dt;
            dropdownProvider.EditorControl.DataBind();
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ColumnSequence", "次序", null);
            this.gridHelper.AddColumn("ColumnName", "栏位名称", null);
            this.gridHelper.AddColumn("Description", "栏位描述", null);
            this.gridHelper.AddDataColumn("DataType", "数据类型", 150);
            this.gridHelper.AddCheckBoxColumn("Visible", "是否可见", true, 100);
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["Description"].ReadOnly = false;
            //cell有下拉框
            DropDownProvider dropdownProvider = new DropDownProvider();

            dropdownProvider.ID = "dropdownProviderDataType";
            dropdownProvider.EditorControl.ID = "editorControlDataType";
            dropdownProvider.EditorControl.DisplayMode = DropDownDisplayMode.DropDownList;
            dropdownProvider.EditorControl.TextField = "DataTypeText";
            dropdownProvider.EditorControl.ValueField = "DataTypeValue";
            dropdownProvider.EditorControl.DropDownContainerWidth = new Unit(150);
            BindDropDownDataType(dropdownProvider);
            this.gridWebGrid.EditorProviders.Add(dropdownProvider);
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["DataType"].ReadOnly = false;
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["DataType"].EditorID = "dropdownProviderDataType";
            //下拉框列固定列宽
            ColumnResizeSetting crs = new ColumnResizeSetting(this.gridWebGrid);
            crs.EnableResize = false;
            crs.ColumnKey = "DataType";
            this.gridWebGrid.Behaviors.CreateBehavior<ColumnResizing>().ColumnSettings.Add(crs);
            //是否可见列可编辑
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["Visible"].ReadOnly = false;
            //InitRow();
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            this.cmdQuery_Click(null, null);

        }

        void gridWebGrid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
        {
            string strScript = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                            e.Row.Index, e.Row.Items.FindItemByKey("Description").Column.Index);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript, true);

            string strScript2 = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                            e.Row.Index, e.Row.Items.FindItemByKey("DataType").Column.Index);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript2, true);
        }

        protected override DataRow GetGridRow(object obj)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            //object viewobj = _facade.GetRptViewDataSourceColumn(Convert.ToDecimal(datasourceid.Value), Convert.ToDecimal(((RptViewDataSourceColumn)obj).ColumnSequence), ((RptViewDataSourceColumn)obj).ColumnName);
            DataRow row = DtSource.NewRow();
            row["ColumnSequence"] = ((RptViewDataSourceColumn)obj).ColumnSequence.ToString();
            row["ColumnName"] = ((RptViewDataSourceColumn)obj).ColumnName.ToString();
            row["Description"] = ((RptViewDataSourceColumn)obj).Description.ToString();
            row["DataType"] = ((RptViewDataSourceColumn)obj).DataType.ToString();
            row["Visible"] = (((RptViewDataSourceColumn)obj).Visible.ToString() == "1") ? true : false;
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            return this._facade.GetRptViewDataSourceColumn(Convert.ToInt32(datasourceid.Value),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            return this._facade.GetRptViewDataSourceColumnCountByDataSourceId(Convert.ToInt32(datasourceid.Value));
        }

        #endregion

        #region Button

        protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("../ReportView/FRptViewDataSource.aspx"));
        }
        #endregion

        #region Object <--> Page
        protected void cmdAdd_ServerClick(object sender, EventArgs e)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {

                this._facade.UpdateRptViewDataSourceColumn((RptViewDataSourceColumn)GetEditObject(this.gridWebGrid.Rows[i]));
            }
            this.gridHelper.RequestData();
        }

        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            RptViewDataSourceColumn rptviewdatasourcecolum = this._facade.CreateNewRptViewDataSourceColumn();
            rptviewdatasourcecolum.ColumnSequence = Convert.ToDecimal(FormatHelper.CleanString(row.Items.FindItemByKey("ColumnSequence").Text));
            rptviewdatasourcecolum.ColumnName = FormatHelper.CleanString(row.Items.FindItemByKey("ColumnName").Text);
            rptviewdatasourcecolum.Description = FormatHelper.CleanString(row.Items.FindItemByKey("Description").Text);
            rptviewdatasourcecolum.DataType = FormatHelper.CleanString(row.Items.FindItemByKey("DataType").Value.ToString());
            if (row.Items.FindItemByKey("Visible").Text.ToLower() == "false" || row.Items.FindItemByKey("Visible").Text == "0")
            {
                rptviewdatasourcecolum.Visible = "0";
            }
            else
            {
                rptviewdatasourcecolum.Visible = "1";
            }
            rptviewdatasourcecolum.MaintainUser = this.GetUserCode();
            rptviewdatasourcecolum.DataSourceID = Convert.ToDecimal(datasourceid.Value);
            return rptviewdatasourcecolum;
        }
        protected override bool ValidateInput()
        {
            //PageCheckManager manager = new PageCheckManager();

            //manager.Add(new LengthCheck(lblID, this.txtNameEdit, 40, true));

            //if (!manager.Check())
            //{
            //    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
            //    return false;
            //}
            return true;
        }

        #endregion



        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((RptViewDataSourceColumn)obj).ColumnSequence.ToString(),
                                   ((RptViewDataSourceColumn)obj).ColumnName.ToString(),
                                   ((RptViewDataSourceColumn)obj).Description.ToString(),
                                   ((RptViewDataSourceColumn)obj).DataType.ToString(),
                                   ((RptViewDataSourceColumn)obj).Visible.ToString()};
        }

        protected override string[] GetColumnHeaderText()
        {
            // TODO: 调整字段值的顺序，使之与Grid的列对应
            return new string[] {	
									"ColumnSequence",
									"ColumnName",
                                    "Description",
                                    "DataType",
									"Visible"
									};
        }
        #endregion



    }
}
