using System;
using System.Collections;
using System.Collections.Generic;
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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.ListControls;

namespace BenQGuru.eMES.Web.ReportView
{
    /// <summary>
    /// FRptDesignChartMP 的摘要说明。
    /// </summary>
    public partial class FRptDesignChartMP : ReportWizardBasePage
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
            this.cmdPublish.ServerClick += new EventHandler(cmdPublish_ServerClick);
            this.cmdFinish.ServerClick += new EventHandler(cmdFinish_ServerClick);
            this.cmdPreview.ServerClick += new EventHandler(cmdPreview_ServerClick);
            this.cmdSave.ServerClick += new EventHandler(cmdSave_ServerClick);
            this.imgUp.ServerClick += new ImageClickEventHandler(SetColumnUp);
            this.imgDown.ServerClick += new ImageClickEventHandler(SetColumnDown);
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            needVScroll = true;
            this.gridHelper = new GridHelperNew(this.gridWebGrid, DtSource);
            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            RptViewDataSourceColumn[] columns = rptFacade.GetRptViewDataSourceColumnByDataSourceId(Convert.ToInt32(this.designView.DesignMain.DataSourceID));
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                InitButtonVisible();

                this.txtReportName.Text = this.designView.DesignMain.ReportName;
                //InitGridData();
                this.InitChartDescLanguage();
                this.lstUnSelectColumn.Items.Clear();
                for (int i = 0; columns != null && i < columns.Length; i++)
                {
                    this.lstUnSelectColumn.Items.Add(new System.Web.UI.WebControls.ListItem(columns[i].Description, columns[i].ColumnName));
                }
            }
            this.lstUnSelectedSeries.Items.Clear();
            this.lstUnSelectedCategory.Items.Clear();
            for (int i = 0; columns != null && i < columns.Length; i++)
            {
                this.lstUnSelectedSeries.Items.Add(new System.Web.UI.WebControls.ListItem(columns[i].Description, columns[i].ColumnName));
                this.lstUnSelectedCategory.Items.Add(new System.Web.UI.WebControls.ListItem(columns[i].Description, columns[i].ColumnName));
            }
            this.lstSelectedSeries.Items.Clear();
            this.lstSelectedCategory.Items.Clear();
            if (this.hidSelectedSeriesValue.Value != "")
            {
                string[] strArrTmp = this.hidSelectedSeriesValue.Value.Split(';');
                for (int i = 0; i < strArrTmp.Length; i++)
                {
                    if (strArrTmp[i] != "")
                    {
                        System.Web.UI.WebControls.ListItem item = this.lstUnSelectedSeries.Items.FindByValue(strArrTmp[i]);
                        if (item != null)
                        {
                            this.lstSelectedSeries.Items.Add(new System.Web.UI.WebControls.ListItem(item.Text, item.Value));
                            this.lstUnSelectedSeries.Items.Remove(item);
                        }
                    }
                }
            }
            if (this.hidSelectedCategoryValue.Value != "")
            {
                string[] strArrTmp = this.hidSelectedCategoryValue.Value.Split(';');
                for (int i = 0; i < strArrTmp.Length; i++)
                {
                    if (strArrTmp[i] != "")
                    {
                        System.Web.UI.WebControls.ListItem item = this.lstUnSelectedCategory.Items.FindByValue(strArrTmp[i]);
                        if (item != null)
                        {
                            this.lstSelectedCategory.Items.Add(new System.Web.UI.WebControls.ListItem(item.Text, item.Value));
                            this.lstUnSelectedCategory.Items.Remove(item);
                        }
                    }
                }
            }
            if (this.hidSelectedDataValue.Value != "")
            {
                string[] strArrTmp = this.hidSelectedDataValue.Value.Split(';');
                for (int i = 0; i < strArrTmp.Length; i++)
                {
                    if (strArrTmp[i] != "")
                    {
                        System.Web.UI.WebControls.ListItem item = this.lstUnSelectColumn.Items.FindByValue(strArrTmp[i]);
                        if (item != null)
                        {
                            this.lstUnSelectColumn.Items.Remove(item);
                        }
                    }
                }
            }
            if (this.gridWebGrid.Columns.FromKey("TotalType") != null)
            {
                DropDownProvider dropdownProvider = this.gridWebGrid.EditorProviders.GetProviderById("dropdownProviderTotalType") as DropDownProvider;
                BindDropDownTotalType(dropdownProvider);
                dropdownProvider.EditorControl.DropDownContainerWidth = new Unit(150);
            }
            this.gridWebGrid.Behaviors.CreateBehavior<Activation>().Enabled = true;
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

            dt.Rows.Add(rowSum);
            dt.Rows.Add(rowCount);
            dt.Rows.Add(rowAvg);

            dropdownProvider.EditorControl.ValueField = "TotalTypeValue";
            dropdownProvider.EditorControl.TextField = "TotalTypeText";
            dropdownProvider.EditorControl.DataSource = dt;
            dropdownProvider.EditorControl.DataBind();
        }
        private void InitChartDescLanguage()
        {
            this.imgChartTypeColumn1.Attributes["Description"] = this.languageComponent1.GetString("$PageControl_ChartTypeColumn1");
            this.imgChartTypeColumn2.Attributes["Description"] = this.languageComponent1.GetString("$PageControl_ChartTypeColumn2");
            this.imgChartTypeColumn3.Attributes["Description"] = this.languageComponent1.GetString("$PageControl_ChartTypeColumn3");
            this.imgChartTypeBar1.Attributes["Description"] = this.languageComponent1.GetString("$PageControl_ChartTypeBar1");
            this.imgChartTypeBar2.Attributes["Description"] = this.languageComponent1.GetString("$PageControl_ChartTypeBar2");
            this.imgChartTypeBar3.Attributes["Description"] = this.languageComponent1.GetString("$PageControl_ChartTypeBar3");
            this.imgChartTypeLine1.Attributes["Description"] = this.languageComponent1.GetString("$PageControl_ChartTypeLine1");
            this.imgChartTypeLine2.Attributes["Description"] = this.languageComponent1.GetString("$PageControl_ChartTypeLine2");
            this.imgChartTypePie1.Attributes["Description"] = this.languageComponent1.GetString("$PageControl_ChartTypePie1");
            this.imgChartTypePie2.Attributes["Description"] = this.languageComponent1.GetString("$PageControl_ChartTypePie2");
        }

        private void InitButtonVisible()
        {
            bool bVisible = (this.GetRequestParam("displaytype") == "chart");
            this.tdbuttonBack.Visible = bVisible;
            this.tdbuttonPreview.Visible = bVisible;
            this.tdbuttonPublish.Visible = bVisible;
            this.tdbuttonFinish.Visible = bVisible;
            this.tdbuttonSave.Visible = !bVisible;
            if (bVisible == false)
            {
                this.cmdCancel.Attributes.Add("onclick", "window.close();return false;");
            }
        }
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("Sequence", "次序", null);
            this.gridHelper.AddColumn("ColumnName", "栏位名称", null);
            this.gridHelper.AddColumn("DisplayDesc", "显示提示", null);
            this.gridHelper.AddColumn("TotalType", "汇总方式", null);
            this.gridWebGrid.Behaviors.CreateBehavior<EditingCore>().Behaviors.CreateBehavior<CellEditing>().ColumnSettings["DisplayDesc"].ReadOnly = false;
            //this.gridWebGrid.Columns.FromKey("DisplayDesc").AllowUpdate = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
            //this.gridWebGrid.Columns.FromKey("DisplayDesc").CellStyle.BackColor = Color.FromArgb(255, 252, 240);

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
        }

        protected override void DisplayDesignData()
        {
            if (this.designView.ChartMains != null && this.designView.ChartMains.Length > 0)
            {
                this.hidChartType.Value = this.designView.ChartMains[0].ChartType;
                this.hidChartSubType.Value = this.designView.ChartMains[0].ChartSubType;
            }
            if (this.designView.ChartSeries != null)
            {
                string strVal = "";
                for (int i = 0; i < this.designView.ChartSeries.Length; i++)
                {
                    System.Web.UI.WebControls.ListItem item = this.lstUnSelectedSeries.Items.FindByValue(this.designView.ChartSeries[i].ColumnName);
                    if (item == null)
                        continue;
                    this.lstSelectedSeries.Items.Add(new System.Web.UI.WebControls.ListItem(item.Text, item.Value));
                    strVal += item.Value + ";";
                    this.lstUnSelectedSeries.Items.Remove(item);
                }
                this.hidSelectedSeriesValue.Value = strVal;
            }
            if (this.designView.ChartCategories != null)
            {
                string strVal = "";
                for (int i = 0; i < this.designView.ChartCategories.Length; i++)
                {
                    System.Web.UI.WebControls.ListItem item = this.lstUnSelectedCategory.Items.FindByValue(this.designView.ChartCategories[i].ColumnName);
                    if (item == null)
                        continue;
                    this.lstSelectedCategory.Items.Add(new System.Web.UI.WebControls.ListItem(item.Text, item.Value));
                    strVal += item.Value + ";";
                    this.lstUnSelectedCategory.Items.Remove(item);
                }
                this.hidSelectedCategoryValue.Value = strVal;
            }
            if (this.designView.ChartDatas != null)
            {
                for (int i = 0; i < this.designView.ChartDatas.Length; i++)
                {
                    System.Web.UI.WebControls.ListItem item = this.lstUnSelectColumn.Items.FindByValue(this.designView.ChartDatas[i].ColumnName);
                    if (item == null)
                        continue;
                    DataRow row = DtSource.NewRow();
                    row["GUID"] = Guid.NewGuid().ToString();
                    row["Sequence"] = this.designView.ChartDatas[i].DataSequence;
                    row["ColumnName"] = item.Value;
                    row["DisplayDesc"] = this.designView.ChartDatas[i].Description;
                    row["TotalType"] = this.designView.ChartDatas[i].TotalType;
                    this.DtSource.Rows.Add(row);
                    this.gridWebGrid.DataSource = DtSource;
                    this.gridWebGrid.DataBind();

                    this.lstUnSelectColumn.Items.Remove(item);
                }
            }
            if (this.designView.ChartMains != null)
            {
                this.chkIsShowLegend.Checked = FormatHelper.StringToBoolean(this.designView.ChartMains[0].ShowLegend);
                this.chkIsShowMarker.Checked = FormatHelper.StringToBoolean(this.designView.ChartMains[0].ShowMarker);
                if (this.chkIsShowMarker.Checked == true)
                {
                    this.rdoListMarkerType.SelectedValue = this.designView.ChartMains[0].MarkerType;
                }
                this.chkIsShowLabel.Checked = FormatHelper.StringToBoolean(this.designView.ChartMains[0].ShowLabel);
                if (this.chkIsShowLabel.Checked == true)
                {
                    string strFormatId = this.designView.ChartMains[0].LabelFormatID;
                    ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
                    RptViewDataFormat dataFormat = (RptViewDataFormat)rptFacade.GetRptViewDataFormat(strFormatId);
                    string strVal = "";
                    if (dataFormat != null)
                    {
                        strVal += dataFormat.FontFamily + ";";
                        strVal += dataFormat.FontSize.ToString() + ";";
                        strVal += (dataFormat.FontWeight == "Bold" ? "true" : "false") + ";";
                        strVal += (dataFormat.FontStyle == "Italic" ? "true" : "false") + ";";
                        strVal += (dataFormat.TextDecoration == "Underline" ? "true" : "false") + ";";
                        strVal += dataFormat.Color + ";";
                        strVal += dataFormat.BackgroundColor + ";";
                        if (dataFormat.TextAlign == "Center")
                            strVal += BenQGuru.eMES.Web.Helper.TextAlign.Center + ";";
                        else if (dataFormat.TextAlign == "Right")
                            strVal += BenQGuru.eMES.Web.Helper.TextAlign.Right + ";";
                        else
                            strVal += BenQGuru.eMES.Web.Helper.TextAlign.Left + ";";
                        if (dataFormat.VerticalAlign == "Top")
                            strVal += BenQGuru.eMES.Web.Helper.VerticalAlign.Top + ";";
                        else if (dataFormat.VerticalAlign == "Bottom")
                            strVal += BenQGuru.eMES.Web.Helper.VerticalAlign.Bottom + ";";
                        else
                            strVal += BenQGuru.eMES.Web.Helper.VerticalAlign.Middle + ";";
                        strVal += dataFormat.TextFormat;
                    }
                    this.hidLabelFormat.Value = strVal;
                }
            }
        }

        protected override bool ValidateInput()
        {
            if (this.hidChartType.Value == "" || this.hidChartSubType.Value == "")
                throw new Exception("$ReportDesign_Chart_No_ChartType");
            if (this.gridWebGrid.Rows.Count == 0)
                throw new Exception("$ReportDesign_Chart_No_DataColumn");
            if (this.chkIsShowMarker.Checked == true)
            {
                if (this.rdoListMarkerType.SelectedItem == null)
                    throw new Exception("$ReportDesign_Chart_No_MarkerType");
            }
            return true;
        }

        protected override void RedirectToBack()
        {
            this.Response.Redirect("FRptDesignStep1MP.aspx?isback=1");
        }

        protected override void RedirectToNext()
        {
        }

        protected override void UpdateReportDesignView()
        {
            RptViewChartMain chartMain = null;
            if (this.designView.ChartMains != null && this.designView.ChartMains.Length > 0)
                chartMain = this.designView.ChartMains[0];
            else
                chartMain = new RptViewChartMain();
            chartMain.ChartSequence = 1;
            chartMain.DataSourceID = this.designView.DesignMain.DataSourceID;
            chartMain.ChartType = this.hidChartType.Value;
            chartMain.ChartSubType = this.hidChartSubType.Value;
            chartMain.ShowLegend = FormatHelper.BooleanToString(this.chkIsShowLegend.Checked);
            chartMain.ShowMarker = FormatHelper.BooleanToString(this.chkIsShowMarker.Checked);
            if (this.chkIsShowMarker.Checked == false)
                chartMain.MarkerType = "";
            else
                chartMain.MarkerType = this.rdoListMarkerType.SelectedValue;
            chartMain.ShowLabel = FormatHelper.BooleanToString(this.chkIsShowLabel.Checked);
            if (this.chkIsShowLabel.Checked == false)
            {
                chartMain.LabelFormatID = "";
                this.designView.ChartDataFormats = null;
            }
            else
            {
                RptViewDataFormat dataFormat = null;
                this.BuildDataFormat(out dataFormat, this.hidLabelFormat.Value);
                chartMain.LabelFormatID = dataFormat.FormatID;
                this.designView.ChartDataFormats = new RptViewDataFormat[] { dataFormat };
            }
            this.designView.ChartMains = new RptViewChartMain[] { chartMain };

            ArrayList list = new ArrayList();
            string[] strArrTmp = this.hidSelectedSeriesValue.Value.Split(';');
            for (int i = 0; i < strArrTmp.Length; i++)
            {
                if (strArrTmp[i] == "")
                    continue;
                RptViewChartSeries series = new RptViewChartSeries();
                series.ChartSequence = 1;
                series.SeriesSequence = i + 1;
                series.DataSourceID = this.designView.DesignMain.DataSourceID;
                series.ColumnName = strArrTmp[i];
                series.Description = this.lstSelectedSeries.Items.FindByValue(series.ColumnName).Text;
                list.Add(series);
            }
            this.designView.ChartSeries = new RptViewChartSeries[list.Count];
            list.CopyTo(this.designView.ChartSeries);

            list = new ArrayList();
            strArrTmp = this.hidSelectedCategoryValue.Value.Split(';');
            for (int i = 0; i < strArrTmp.Length; i++)
            {
                if (strArrTmp[i] == "")
                    continue;
                RptViewChartCategory cate = new RptViewChartCategory();
                cate.ChartSequence = 1;
                cate.CategorySequence = i + 1;
                cate.DataSourceID = this.designView.DesignMain.DataSourceID;
                cate.ColumnName = strArrTmp[i];
                list.Add(cate);
            }
            this.designView.ChartCategories = new RptViewChartCategory[list.Count];
            list.CopyTo(this.designView.ChartCategories);

            list = new ArrayList();
            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                RptViewChartData data = new RptViewChartData();
                data.ChartSequence = 1;
                data.DataSequence = i + 1;
                data.DataSourceID = this.designView.DesignMain.DataSourceID;
                data.ColumnName = this.gridWebGrid.Rows[i].Items.FindItemByKey("ColumnName").Value.ToString();
                data.Description = this.gridWebGrid.Rows[i].Items.FindItemByKey("DisplayDesc").Value.ToString();
                data.TotalType = this.gridWebGrid.Rows[i].Items.FindItemByKey("TotalType").Value.ToString();
                list.Add(data);
            }
            this.designView.ChartDatas = new RptViewChartData[list.Count];
            list.CopyTo(this.designView.ChartDatas);
        }
        private void BuildDataFormat(out RptViewDataFormat dataFormat, string styleValue)
        {
            dataFormat = new RptViewDataFormat();
            string strId = System.Guid.NewGuid().ToString();
            dataFormat.FormatID = strId;

            dataFormat.FontFamily = "Arial";
            dataFormat.FontSize = 12;
            dataFormat.FontWeight = "Normal";
            dataFormat.FontStyle = "Normal";
            dataFormat.TextDecoration = "None";
            dataFormat.Color = "Black";
            dataFormat.BackgroundColor = "White";
            dataFormat.TextAlign = "Left";
            dataFormat.VerticalAlign = "Middle";
            string[] styleList = styleValue.Split(';');
            if (styleList.Length >= 10)
            {
                dataFormat.FontFamily = styleList[0];   // Font Family
                if (styleList[1] != "")                 // Font Size
                    dataFormat.FontSize = decimal.Parse(styleList[1]);
                if (styleList[2] == "true")             // Font Weight
                    dataFormat.FontWeight = "Bold";
                if (styleList[3] == "true")             // Font Style
                    dataFormat.FontStyle = "Italic";
                if (styleList[4] == "true")             // Font Decoration
                    dataFormat.TextDecoration = "Underline";
                if (styleList[5] != "")                 // Fore Color
                    dataFormat.Color = styleList[5];
                if (styleList[6] != "")                 // Back Color
                    dataFormat.BackgroundColor = styleList[6];
                if (styleList[7] == BenQGuru.eMES.Web.Helper.TextAlign.Center)        // Text Align
                    dataFormat.TextAlign = "Center";
                else if (styleList[7] == BenQGuru.eMES.Web.Helper.TextAlign.Right)
                    dataFormat.TextAlign = "Right";
                else
                    dataFormat.TextAlign = "Left";
                if (styleList[8] == BenQGuru.eMES.Web.Helper.VerticalAlign.Top)     // Vertical Align
                    dataFormat.VerticalAlign = "Top";
                else if (styleList[8] == BenQGuru.eMES.Web.Helper.VerticalAlign.Bottom)
                    dataFormat.VerticalAlign = "Bottom";
                else
                    dataFormat.VerticalAlign = "Middle";
                dataFormat.TextFormat = styleList[9];   // Text Format
            }

        }

        #endregion


        void cmdPreview_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInput() == false)
                return;
            UpdateReportDesignView();
            this.SaveDesignView(designView);

            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
            this.DataProvider.BeginTransaction();
            try
            {
                // 保存
                rptFacade.SaveDesignReportData(this.designView, this.GetUserCode());

                // 生成报表文件
                ReportGenerater rptGenerater = new ReportGenerater(this.DataProvider);
                string strFormatFile = Server.MapPath("ReportFormat.xml");
                string strReportFile = Server.MapPath("../ReportFiles");
                if (System.IO.Directory.Exists(strReportFile) == false)
                    System.IO.Directory.CreateDirectory(strReportFile);
                strReportFile += "\\" + this.designView.ReportID + ".rdlc";
                rptGenerater.Generate(this.designView, strFormatFile, strReportFile);
                this.designView.UploadFileName = strReportFile;

                string strRptFile = strReportFile.Substring(strReportFile.LastIndexOf("\\", strReportFile.LastIndexOf("\\") - 1) + 1);
                this.designView.DesignMain.ReportFileName = strRptFile;
                rptFacade.UpdateRptViewDesignMain(this.designView.DesignMain);

                // 脚本
                string strScript = "window.open('FRptViewMP.aspx?reportid=" + this.designView.DesignMain.ReportID + "&preview=1');";
                //this.ClientScript.RegisterStartupScript(typeof(string), "OpenPreviewWindow", strScript);
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenPreviewWindow", strScript,true);
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }
            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
            {
                string strName = this.gridWebGrid.Rows[i].Items.FindItemByKey("ColumnName").Value.ToString();
                System.Web.UI.WebControls.ListItem item = this.lstUnSelectColumn.Items.FindByValue(strName);
                if (item != null)
                    this.lstUnSelectColumn.Items.Remove(item);
            }
        }

        void cmdFinish_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInput() == false)
                return;
            UpdateReportDesignView();
            this.SaveDesignView(designView);

            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
            this.DataProvider.BeginTransaction();
            try
            {
                rptFacade.SaveDesignReportData(this.designView, this.GetUserCode());

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }

            string alertInfo =
                string.Format("alert('{0}');", this.languageComponent1.GetString("$ReportDesign_Save_Success"));
            //if (!this.ClientScript.IsClientScriptBlockRegistered("SaveSuccess"))
            //{
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "SaveSuccess", alertInfo,true);
               // this.ClientScript.RegisterClientScriptBlock(typeof(string), "SaveSuccess", alertInfo);
            //}
        }

        void cmdPublish_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInput() == false)
                return;
            UpdateReportDesignView();
            this.SaveDesignView(designView);

            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.OpenConnection();
            this.DataProvider.BeginTransaction();
            try
            {
                rptFacade.SaveDesignReportData(this.designView, this.GetUserCode());

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }

            string strRptId = this.designView.ReportID;
            this.designView = null;
            this.SaveDesignView(null);

            string strUrl = "FRptPublishDesignMP.aspx?reportid=" + strRptId;
            this.Response.Redirect(strUrl);
        }

        void cmdSave_ServerClick(object sender, EventArgs e)
        {
            if (ValidateInput() == false)
                return;
            UpdateReportDesignView();
            this.SaveDesignView(designView);

            string strScript = "window.close();";
            //this.ClientScript.RegisterClientScriptBlock(typeof(string), "CloseWindows", strScript);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "CloseWindows", strScript,true);
        }

        protected void lstUnSelectColumn_OnInit(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "lstUnSelectColumn_OnInit();", true);
        }

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

        private void AddRowToGrid(string columnName, string DisplayDesc)
        {
            DataRow row = DtSource.NewRow();
            row["GUID"] = Guid.NewGuid().ToString();
            row["Sequence"] = this.gridWebGrid.Rows.Count + 1;
            row["ColumnName"] = columnName;
            row["DisplayDesc"] = DisplayDesc;
            row["TotalType"] = "reporttotaltype_sum";
            DtSource.Rows.Add(row);
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

        protected void SetColumnUp(object sender, ImageClickEventArgs e)
        {
            if (ActiveRowIndex == null)
                return;
            int activeRowIndex = (int)ActiveRowIndex;

            DataRow row1 = DtSource.Rows[activeRowIndex];
            DataRow tempRow = DtSource.NewRow();
            tempRow["ColumnName"] = row1["ColumnName"];
            tempRow["DisplayDesc"] = row1["DisplayDesc"];
            tempRow["TotalType"] = row1["TotalType"];
            row1["ColumnName"] = DtSource.Rows[activeRowIndex + 1]["ColumnName"];
            row1["DisplayDesc"] = DtSource.Rows[activeRowIndex + 1]["DisplayDesc"];
            row1["TotalType"] = DtSource.Rows[activeRowIndex + 1]["TotalType"];
            DataRow row2 = DtSource.Rows[activeRowIndex + 1];
            row2["ColumnName"] = tempRow["ColumnName"];
            row2["DisplayDesc"] = tempRow["DisplayDesc"];
            row2["TotalType"] = tempRow["TotalType"];
            this.gridWebGrid.DataSource = DtSource;
            this.gridWebGrid.DataBind();
        }

        protected void SetColumnDown(object sender, ImageClickEventArgs e)
        {
            if (ActiveRowIndex == null)
                return;
            int activeRowIndex = (int)ActiveRowIndex;

            DataRow row1 = DtSource.Rows[activeRowIndex - 1];
            DataRow tempRow = DtSource.NewRow();
            tempRow["ColumnName"] = row1["ColumnName"];
            tempRow["DisplayDesc"] = row1["DisplayDesc"];
            tempRow["TotalType"] = row1["TotalType"];
            row1["ColumnName"] = DtSource.Rows[activeRowIndex]["ColumnName"];
            row1["DisplayDesc"] = DtSource.Rows[activeRowIndex]["DisplayDesc"];
            row1["TotalType"] = DtSource.Rows[activeRowIndex]["TotalType"];
            DataRow row2 = DtSource.Rows[activeRowIndex];
            row2["ColumnName"] = tempRow["ColumnName"];
            row2["DisplayDesc"] = tempRow["DisplayDesc"];
            row2["TotalType"] = tempRow["TotalType"];
            this.gridWebGrid.DataSource = DtSource;
            this.gridWebGrid.DataBind();
        }
    }
}
