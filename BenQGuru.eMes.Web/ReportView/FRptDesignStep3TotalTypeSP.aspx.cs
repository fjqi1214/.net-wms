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
using Infragistics.Web.UI.ListControls;

namespace BenQGuru.eMES.Web.ReportView
{
    /// <summary>
    /// FRptDesignStep3TotalTypeSP 的摘要说明。
    /// </summary>
    public partial class FRptDesignStep3TotalTypeSP : BaseMPageMinus
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
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.gridHelper = new GridHelperNew(this.gridWebGrid, DtSource);
            if (this.IsPostBack == false)
            {
                //InitWebGrid();
                //InitData();
                this.txtReportName.Text = Server.UrlDecode(this.GetRequestParam("reportname"));

                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.lblTotalTypeTitle.Text = this.languageComponent1.GetString("$PageControl_TotalTypeTitle");
            }
            if (this.gridWebGrid.Columns.FromKey("TotalType") != null)
            {
                DropDownProvider dropdownProvider = this.gridWebGrid.EditorProviders.GetProviderById("dropdownProviderTotalType") as DropDownProvider;
                BindDropDownTotalType(dropdownProvider);
                dropdownProvider.EditorControl.DropDownContainerWidth = new Unit(150);
            }
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("Sequence", "次序", null);
            this.gridHelper.AddColumn("ColumnName", "栏位名称", null);
            this.gridHelper.AddColumn("ColumnDesc", "栏位名称", null);
            this.gridHelper.AddColumn("TotalType", "统计方式", null);

            DropDownProvider dropdownProvider = new DropDownProvider();
            dropdownProvider.ID = "dropdownProviderTotalType";
            dropdownProvider.EditorControl.ID = "editorControlTotalType";
            dropdownProvider.EditorControl.DisplayMode = DropDownDisplayMode.DropDownList;
            dropdownProvider.EditorControl.TextField = "TotalTypeText";
            dropdownProvider.EditorControl.ValueField = "TotalTypeValue";
            dropdownProvider.EditorControl.DropDownContainerWidth = new Unit(150);
            BindDropDownTotalType(dropdownProvider);
            this.gridWebGrid.EditorProviders.Add(dropdownProvider);
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["TotalType"].ReadOnly = false;
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["TotalType"].EditorID = "dropdownProviderTotalType";


            this.gridWebGrid.Columns.FromKey("ColumnName").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            InitData();
        }

        private void BindDropDownTotalType(DropDownProvider dropdownProvider)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TotalTypeValue");
            dt.Columns.Add("TotalTypeText");
            dt.AcceptChanges();
            DataRow rowSum = dt.NewRow();
            rowSum["TotalTypeValue"] = ReportTotalType.Sum;
            rowSum["TotalTypeText"] = this.languageComponent1.GetString(ReportTotalType.Sum);
            DataRow rowCount = dt.NewRow();
            rowCount["TotalTypeValue"] = ReportTotalType.Count;
            rowCount["TotalTypeText"] = this.languageComponent1.GetString(ReportTotalType.Count);
            DataRow rowAvg = dt.NewRow();
            rowAvg["TotalTypeValue"] = ReportTotalType.Avg;
            rowAvg["TotalTypeText"] = this.languageComponent1.GetString(ReportTotalType.Avg);
            DataRow rowEmpty = dt.NewRow();
            rowEmpty["TotalTypeValue"] = ReportTotalType.Empty;
            rowEmpty["TotalTypeText"] = this.languageComponent1.GetString(ReportTotalType.Empty);
            DataRow rowMax = dt.NewRow();
            rowMax["TotalTypeValue"] = ReportTotalType.Max;
            rowMax["TotalTypeText"] = this.languageComponent1.GetString(ReportTotalType.Max);

            dt.Rows.Add(rowSum);
            dt.Rows.Add(rowCount);
            dt.Rows.Add(rowAvg);
            dt.Rows.Add(rowEmpty);
            dt.Rows.Add(rowMax);

            dropdownProvider.EditorControl.ValueField = "TotalTypeValue";
            dropdownProvider.EditorControl.TextField = "TotalTypeText";
            dropdownProvider.EditorControl.DataSource = dt;
            dropdownProvider.EditorControl.DataBind();
        }

        private void InitData()
        {
            string strDataSourceId = this.GetRequestParam("datasourceid");
            string strGroupColumn = this.GetRequestParam("groupcolumn");
            string strColumnList = this.Request.QueryString["columnlist"];
            if (strDataSourceId == "" || strGroupColumn == "" || strColumnList == "")
                throw new Exception("$Error_RequestUrlParameter_Lost");
            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            RptViewDataSourceColumn[] columns = rptFacade.GetRptViewDataSourceColumnByDataSourceId(int.Parse(strDataSourceId));
            Dictionary<string, string> columnMap = new Dictionary<string, string>();
            for (int i = 0; i < columns.Length; i++)
            {
                columnMap.Add(columns[i].ColumnName, columns[i].Description);
            }

            string strExistSet = this.Request.QueryString["existsetting"];
            Dictionary<string, string> existSetting = new Dictionary<string, string>();
            string[] strSelectColumns = strExistSet.Split(';');
            for (int i = 0; i < strSelectColumns.Length; i++)
            {
                if (strSelectColumns[i].Trim() != "")
                {
                    string[] tmpArr = strSelectColumns[i].Trim().Split(',');
                    existSetting.Add(tmpArr[0], tmpArr[1]);
                }
            }
            string[] columnList = strColumnList.Split(',');
            for (int i = 0; i < columnList.Length; i++)
            {
                if (columnList[i] != "" && columnList[i] != strGroupColumn)
                {
                    string strTotalType = ReportTotalType.Empty;
                    if (existSetting.ContainsKey(columnList[i]) == true)
                    {
                        strTotalType = existSetting[columnList[i]];
                    }
                    DataRow row = DtSource.NewRow();
                    if (this.DtSource.Columns.Contains("GUID"))
                    {
                        row["GUID"] = Guid.NewGuid().ToString();
                    }
                    row["Sequence"] = this.DtSource.Rows.Count + 1;
                    row["ColumnName"] = columnList[i];
                    row["ColumnDesc"] = columnMap[columnList[i]];
                    row["TotalType"] = strTotalType;
                    this.DtSource.Rows.Add(row);
                }

            }
            this.gridWebGrid.DataSource = DtSource;
            this.gridWebGrid.DataBind();
            this.txtGroupColumnName.Text = columnMap[strGroupColumn];
        }
        #endregion

        void cmdSave_ServerClick(object sender, EventArgs e)
        {

        }



    }
}
