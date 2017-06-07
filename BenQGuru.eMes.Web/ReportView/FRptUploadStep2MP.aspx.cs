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
    /// FRptUploadStep2MP 的摘要说明。
    /// </summary>
    public partial class FRptUploadStep2MP : ReportWizardBasePage
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
            this.gridHelper = new GridHelperNew(this.gridWebGrid, DtSource);
            if (this.IsPostBack == false)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                string strFileName = this.designView.UploadFileName;
                strFileName = strFileName.Substring(strFileName.LastIndexOf("\\") + 1);
                this.txtReportFile.Text = strFileName;

                InitWebGrid();
                InitParameter();
            }
        }

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("Sequence", "次序", null);
            this.gridHelper.AddColumn("ParameterName", "参数名称", null);
            this.gridHelper.AddColumn("ParameterDesc", "参数描述", null);
            this.gridHelper.AddColumn("DataType", "数据类型", null);
            this.gridHelper.AddColumn("DefaultValue", "默认值", null);
            this.gridHelper.AddColumn("UserInput", "用户输入", null);
            this.gridHelper.AddColumn("InputUIType", "输入方式", null);
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["UserInput"].ReadOnly = false;
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["ParameterDesc"].ReadOnly = false;
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["DefaultValue"].ReadOnly = false;
            //this.gridWebGrid.Columns.FromKey("UserInput").Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
            //this.gridWebGrid.Columns.FromKey("UserInput").Type=ty
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        void gridWebGrid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
        {
            string strScript = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                            e.Row.Index, e.Row.Items.FindItemByKey("ParameterDesc").Column.Index);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript, true);

            string strScript2 = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                            e.Row.Index, e.Row.Items.FindItemByKey("DefaultValue").Column.Index);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript2, true);

            string strScript3 = string.Format(@"
                         $('#gridWebGrid').children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').children('table').children('tbody').children('tr:eq({0})').children('td:eq({1})').css('background-color','#fffcf0');
                               ",
                            e.Row.Index, e.Row.Items.FindItemByKey("UserInput").Column.Index);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), strScript3, true);
        }

        private void InitParameter()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.designView.UploadFileName);
            XmlNamespaceManager manager = new XmlNamespaceManager(xmlDoc.CreateNavigator().NameTable);
            manager.AddNamespace("a", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
            manager.AddNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");

            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("//a:ReportParameters", manager);
            if (node == null)
                return;
            List<RptViewFileParameter> paramList = new List<RptViewFileParameter>();
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                XmlNode nodeParam = node.ChildNodes[i];
                if (nodeParam.Name == "ReportParameter")
                {
                    RptViewFileParameter param = new RptViewFileParameter();
                    param.FileParameterName = nodeParam.Attributes["Name"].FirstChild.Value;
                    param.Description = nodeParam.SelectSingleNode("a:Prompt", manager).FirstChild.Value;
                    param.DataType = nodeParam.SelectSingleNode("a:DataType", manager).FirstChild.Value.ToLower();
                    if (nodeParam.SelectSingleNode("a:DefaultValue/a:Values/a:Value", manager) != null)
                    {
                        param.DefaultValue = nodeParam.SelectSingleNode("a:DefaultValue/a:Values/a:Value", manager).FirstChild.Value;
                    }
                    param.Sequence = paramList.Count + 1;
                    paramList.Add(param);
                }
            }
            for (int i = 0; i < paramList.Count; i++)
            {
                DataRow row = DtSource.NewRow();
                row["Sequence"] = paramList[i].Sequence;
                row["ParameterName"] = paramList[i].FileParameterName;
                row["ParameterDesc"] = paramList[i].Description;
                row["DataType"] = paramList[i].DataType;
                row["DefaultValue"] = paramList[i].DefaultValue;
                row["UserInput"] = true;
                row["InputUIType"] = "<a href='#' onclick=\"OpenTotalSetWindow('" + this.gridWebGrid.Rows.Count + "');return false;\">Click</a>";
                DtSource.Rows.Add(row);
            }
            this.gridWebGrid.DataSource = DtSource;
            this.gridWebGrid.DataBind();
        }

        protected override void DisplayDesignData()
        {
            string strFilterUI = "";
            if (this.designView.FileParameters != null)
            {
                Dictionary<string, RptViewFileParameter> paramMap = new Dictionary<string, RptViewFileParameter>();
                for (int i = 0; i < this.designView.FileParameters.Length; i++)
                {
                    paramMap.Add(this.designView.FileParameters[i].FileParameterName, this.designView.FileParameters[i]);
                }
                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {
                    string strName = this.gridWebGrid.Rows[i].Items.FindItemByKey("ParameterName").Value.ToString();
                    if (paramMap.ContainsKey(strName) == true)
                    {
                        RptViewFileParameter param = paramMap[strName];
                        this.gridWebGrid.Rows[i].Items.FindItemByKey("UserInput").Value = FormatHelper.StringToBoolean(param.ViewerInput);
                        this.gridWebGrid.Rows[i].Items.FindItemByKey("ParameterDesc").Value = param.Description;
                        this.gridWebGrid.Rows[i].Items.FindItemByKey("DefaultValue").Value = param.DefaultValue;
                    }

                    for (int n = 0; this.designView.FiltersUI != null && n < this.designView.FiltersUI.Length; n++)
                    {
                        if (this.designView.FiltersUI[n].InputType == ReportViewerInputType.FileParameter)
                        {
                            if (this.designView.FiltersUI[n].InputName == strName)
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
                if (string.IsNullOrEmpty(this.gridWebGrid.Rows[i].Items.FindItemByKey("ParameterDesc").Text) == true)
                {
                    string strHeader = this.gridWebGrid.Columns.FromKey("ParameterDesc").Key;
                    WebInfoPublish.Publish(this, strHeader + " $Error_Input_Empty", this.languageComponent1);
                    return false;
                }
            }
            return true;
        }

        protected override void RedirectToBack()
        {
            this.Response.Redirect("FRptUploadStep1MP.aspx?isback=1");
        }

        protected override void RedirectToNext()
        {
            this.Response.Redirect("FRptUploadStep3MP.aspx");
        }

        protected override void UpdateReportDesignView()
        {
            ArrayList listFilterUI = new ArrayList();
            Dictionary<string, string> filterUIList = new Dictionary<string, string>();
            string strFilterUI = this.hidInputUIType.Value;
            string[] strFilterUIList = strFilterUI.Split('|');
            for (int i = 0; i < strFilterUIList.Length; i++)
            {
                if (strFilterUIList[i] != "")
                {
                    string[] strTmpList = strFilterUIList[i].Split('@');
                    filterUIList.Add(strTmpList[0], strTmpList[1]);
                }
            }

            RptViewFileParameter[] parames = new RptViewFileParameter[this.gridWebGrid.Rows.Count];
            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                RptViewFileParameter param = new RptViewFileParameter();
                param.Sequence = i + 1;
                param.FileParameterName = this.gridWebGrid.Rows[i].Items.FindItemByKey("ParameterName").Text;
                param.Description = this.gridWebGrid.Rows[i].Items.FindItemByKey("ParameterDesc").Text;
                param.DataType = this.gridWebGrid.Rows[i].Items.FindItemByKey("DataType").Text;
                param.DefaultValue = this.gridWebGrid.Rows[i].Items.FindItemByKey("DefaultValue").Text;
                param.ViewerInput = FormatHelper.BooleanToString(Convert.ToBoolean(this.gridWebGrid.Rows[i].Items.FindItemByKey("UserInput").Value));
                parames[i] = param;

                string strName = param.FileParameterName;
                if (filterUIList.ContainsKey(strName) == true)
                {
                    RptViewFilterUI filterUI = new RptViewFilterUI();
                    filterUI.Sequence = listFilterUI.Count + 1;
                    filterUI.InputType = ReportViewerInputType.FileParameter;
                    filterUI.InputName = strName;
                    filterUI.SqlFilterSequence = 0;
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
            this.designView.FileParameters = parames;

            for (int i = 0; this.designView.FiltersUI != null && i < this.designView.FiltersUI.Length; i++)
            {
                if (this.designView.FiltersUI[i].InputType != ReportViewerInputType.FileParameter)
                    listFilterUI.Add(this.designView.FiltersUI[i]);
            }
            RptViewFilterUI[] targetFilterUI = new RptViewFilterUI[listFilterUI.Count];
            listFilterUI.CopyTo(targetFilterUI);
            this.designView.FiltersUI = targetFilterUI;
        }
        #endregion
    }
}
