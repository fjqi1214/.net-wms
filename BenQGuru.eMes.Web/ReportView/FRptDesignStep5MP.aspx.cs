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

namespace BenQGuru.eMES.Web.ReportView
{
    /// <summary>
    /// FRptDesignStep5MP 的摘要说明。
    /// </summary>
    public partial class FRptDesignStep5MP : ReportWizardBasePage
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
            //InitGridData();
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

            this.cmdPublish.ServerClick += new EventHandler(cmdPublish_ServerClick);
            this.cmdFinish.ServerClick += new EventHandler(cmdFinish_ServerClick);
            this.cmdPreview.ServerClick += new EventHandler(cmdPreview_ServerClick);
            this.cmdOK.ServerClick += new EventHandler(cmdOK_ServerClick);
            this.PreRender += new EventHandler(FRptDesignStep5MP_PreRender);
        }
        #endregion

        #region Init
        void FRptDesignStep5MP_PreRender(object sender, EventArgs e)
        {
            this.InitExtendText();
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "AdjustHeight", "AdjustHeight()", true);
            if (this.IsPostBack == false)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.txtReportName.Text = this.designView.DesignMain.ReportName;
                if (this.designView.DesignMain.DisplayType == ReportDisplayType.GridChart)
                    this.hlnkChartDesign.Visible = true;
                else
                    this.hlnkChartDesign.Visible = false;
                this.InitDefinedStyle();
                
                this.lblDesignStep5Title.Text = this.languageComponent1.GetString("$PageControl_DesignStep5Title");
            }
            InitGridData();
        }
        private void InitGridData()
        {

            ReportViewFacade rptFacade = new ReportViewFacade(this.DataProvider);
            RptViewDataSourceColumn[] sourceColumns = rptFacade.GetRptViewDataSourceColumnByDataSourceId(Convert.ToInt32(this.designView.DesignMain.DataSourceID));
            Dictionary<string, RptViewDataSourceColumn> columnMap = new Dictionary<string, RptViewDataSourceColumn>();
            for (int i = 0; i < sourceColumns.Length; i++)
            {
                columnMap.Add(sourceColumns[i].ColumnName, sourceColumns[i]);
            }

            this.tbFormat.Rows.Clear();
            // 列格式
            HtmlTableRow rowColumn = new HtmlTableRow();
            rowColumn.Cells.Add(new HtmlTableCell());
            for (int i = 0; i < this.designView.GridColumns.Length; i++)
            {
                HtmlTableCell cellHeader = new HtmlTableCell();
                cellHeader.Style.Add("border", "solid 1px #000000");
                cellHeader.Style.Add("width", "2.5cm");
                HtmlInputHidden hidHeader = new HtmlInputHidden();
                hidHeader.ID = "hidColumn_" + this.designView.GridColumns[i].ColumnName;
                hidHeader.Attributes.Add("FormatType", "column");
                hidHeader.Attributes.Add("ColumnName", this.designView.GridColumns[i].ColumnName);
                hidHeader.Value = Request[hidHeader.ID];
                cellHeader.Controls.Add(hidHeader);
                HyperLink linkHeader = new HyperLink();
                linkHeader.NavigateUrl = "#";
                linkHeader.Text = this.languageComponent1.GetString("$PageControl_ColumnFormat");
                linkHeader.ID = "linkColumn_" + this.designView.GridColumns[i].ColumnName;
                linkHeader.Attributes.Add("onclick", "OpenFormatWin('" + "FRptTextFormatMP.aspx?type=column&column=" + this.designView.GridColumns[i].ColumnName + "','" + hidHeader.ClientID + "','" + linkHeader.ClientID + "');return false;");
                cellHeader.Controls.Add(linkHeader);
                rowColumn.Cells.Add(cellHeader);
            }
            this.tbFormat.Rows.Add(rowColumn);

            // Header
            HtmlTableRow rowHeader = new HtmlTableRow();
            HtmlTableCell cellHeaderRow = new HtmlTableCell();
            cellHeaderRow.Style.Add("border", "solid 1px #000000");
            cellHeaderRow.Style.Add("width", "2.5cm");
            HtmlInputHidden hidHeaderRow = new HtmlInputHidden();
            hidHeaderRow.ID = "hidHeaderRow";
            hidHeaderRow.Attributes.Add("FormatType", "headerrow");
            hidHeaderRow.Value = this.Request[hidHeaderRow.ID];
            cellHeaderRow.Controls.Add(hidHeaderRow);
            HyperLink linkHeaderRow = new HyperLink();
            linkHeaderRow.NavigateUrl = "#";
            linkHeaderRow.Text = this.languageComponent1.GetString("$PageControl_TableHeadFormat");
            linkHeaderRow.ID = "linkHeaderRow";
            linkHeaderRow.Attributes.Add("onclick", "OpenFormatWin('" + "FRptTextFormatMP.aspx?type=headerrow','" + hidHeaderRow.ClientID + "','" + linkHeaderRow.ClientID + "');return false;");
            cellHeaderRow.Controls.Add(linkHeaderRow);
            rowHeader.Cells.Add(cellHeaderRow);
            for (int i = 0; i < this.designView.GridColumns.Length; i++)
            {
                HtmlTableCell cellHeader = new HtmlTableCell();
                cellHeader.Style.Add("border", "solid 1px #000000");
                cellHeader.Style.Add("width", "2.5cm");
                HtmlInputHidden hidHeader = new HtmlInputHidden();
                hidHeader.ID = "hidHeader_" + this.designView.GridColumns[i].ColumnName;
                hidHeader.Attributes.Add("FormatType", "header");
                hidHeader.Attributes.Add("ColumnName", this.designView.GridColumns[i].ColumnName);
                hidHeader.Value = this.Request[hidHeader.ID];
                cellHeader.Controls.Add(hidHeader);
                HyperLink linkHeader = new HyperLink();
                linkHeader.NavigateUrl = "#";
                linkHeader.Text = columnMap[this.designView.GridColumns[i].ColumnName].Description;
                linkHeader.ID = "linkHeader_" + this.designView.GridColumns[i].ColumnName;
                linkHeader.Attributes.Add("onclick", "OpenFormatWin('" + "FRptTextFormatMP.aspx?type=header&column=" + this.designView.GridColumns[i].ColumnName + "','" + hidHeader.ClientID + "','" + linkHeader.ClientID + "');return false;");
                cellHeader.Controls.Add(linkHeader);
                rowHeader.Cells.Add(cellHeader);
            }
            this.tbFormat.Rows.Add(rowHeader);

            // 分组
            for (int i = 0; this.designView.GridGroups != null && i < this.designView.GridGroups.Length; i++)
            {
                HtmlTableRow rowGroup = new HtmlTableRow();
                HtmlTableCell cellGroupHeader = new HtmlTableCell();
                cellGroupHeader.Style.Add("border", "solid 1px #000000");
                cellGroupHeader.Style.Add("width", "2.5cm");
                HtmlInputHidden hidGroupHeader = new HtmlInputHidden();
                hidGroupHeader.ID = "hidGroup_" + this.designView.GridGroups[i].GroupSequence;
                hidGroupHeader.Attributes.Add("FormatType", "group");
                hidGroupHeader.Attributes.Add("GroupSeq", this.designView.GridGroups[i].GroupSequence.ToString());
                hidGroupHeader.Value = this.Request[hidGroupHeader.ID];
                cellGroupHeader.Controls.Add(hidGroupHeader);
                HyperLink linkGroupHeader = new HyperLink();
                linkGroupHeader.NavigateUrl = "#";
                linkGroupHeader.Text = this.languageComponent1.GetString("$PageControl_RowFormat");
                linkGroupHeader.ID = "linkGroup_" + this.designView.GridGroups[i].GroupSequence;
                linkGroupHeader.Attributes.Add("onclick", "OpenFormatWin('" + "FRptTextFormatMP.aspx?type=group&group=" + this.designView.GridGroups[i].GroupSequence + "','" + hidGroupHeader.ClientID + "','" + linkGroupHeader.ClientID + "');return false;");
                cellGroupHeader.Controls.Add(linkGroupHeader);
                rowGroup.Cells.Add(cellGroupHeader);
                for (int n = 0; n < this.designView.GridColumns.Length; n++)
                {
                    HtmlTableCell cellGrupData = new HtmlTableCell();
                    cellGrupData.Style.Add("border", "solid 1px #000000");
                    cellGrupData.Style.Add("width", "2.5cm");
                    bool bIsEmpty = false;
                    if (this.designView.GridColumns[n].ColumnName != this.designView.GridGroups[i].ColumnName)
                        bIsEmpty = true;
                    for (int x = 0; x < this.designView.GridGroupTotals.Length; x++)
                    {
                        if (this.designView.GridGroupTotals[x].GroupSequence == this.designView.GridGroups[i].GroupSequence &&
                            this.designView.GridGroupTotals[x].ColumnName == this.designView.GridColumns[n].ColumnName)
                        {
                            if (this.designView.GridGroupTotals[x].TotalType != ReportTotalType.Empty)
                                bIsEmpty = false;
                            break;
                        }
                    }
                    for (int x = 0; x < this.designView.GridGroups.Length; x++)
                    {
                        if (this.designView.GridColumns[n].ColumnName == this.designView.GridGroups[x].ColumnName)
                        {
                            if (x < i)
                                bIsEmpty = true;
                        }
                    }
                    if (bIsEmpty == false)
                    {
                        HtmlInputHidden hidGrupData = new HtmlInputHidden();
                        hidGrupData.ID = "hidGroupData_" + this.designView.GridGroups[i].GroupSequence + "_" + this.designView.GridColumns[n].ColumnName;
                        hidGrupData.Attributes.Add("FormatType", "groupdata");
                        hidGrupData.Attributes.Add("GroupSeq", this.designView.GridGroups[i].GroupSequence.ToString());
                        hidGrupData.Attributes.Add("ColumnName", this.designView.GridColumns[n].ColumnName);
                        hidGrupData.Value = this.Request[hidGrupData.ID];
                        cellGrupData.Controls.Add(hidGrupData);
                        HyperLink linkGroupData = new HyperLink();
                        linkGroupData.NavigateUrl = "#";
                        linkGroupData.Text = columnMap[this.designView.GridColumns[n].ColumnName].Description;
                        linkGroupData.ID = "linkGroupData_" + this.designView.GridGroups[i].GroupSequence + "_" + this.designView.GridColumns[n].ColumnName;
                        linkGroupData.Attributes.Add("onclick", "OpenFormatWin('" + "FRptTextFormatMP.aspx?type=groupdata&group=" + this.designView.GridGroups[i].GroupSequence + "&column=" + this.designView.GridColumns[n].ColumnName + "','" + hidGrupData.ClientID + "','" + linkGroupData.ClientID + "');return false;");
                        cellGrupData.Controls.Add(linkGroupData);
                    }
                    rowGroup.Cells.Add(cellGrupData);
                }
                this.tbFormat.Rows.Add(rowGroup);
            }

            // 详细数据
            HtmlTableRow rowItemData = new HtmlTableRow();
            HtmlTableCell cellItemHeader = new HtmlTableCell();
            cellItemHeader.Style.Add("border", "solid 1px #000000");
            cellItemHeader.Style.Add("width", "2.5cm");
            HtmlInputHidden hidItemHeader = new HtmlInputHidden();
            hidItemHeader.ID = "hidItemHeader";
            hidItemHeader.Attributes.Add("FormatType", "item");
            hidItemHeader.Value = this.Request[hidItemHeader.ID];
            cellItemHeader.Controls.Add(hidItemHeader);
            HyperLink linkItemHeader = new HyperLink();
            linkItemHeader.NavigateUrl = "#";
            linkItemHeader.Text = this.languageComponent1.GetString("$PageControl_RowFormat");
            linkItemHeader.ID = "linkItemHeader";
            linkItemHeader.Attributes.Add("onclick", "OpenFormatWin('" + "FRptTextFormatMP.aspx?type=item" + "','" + hidItemHeader.ClientID + "','" + linkItemHeader.ClientID + "');return false;");
            cellItemHeader.Controls.Add(linkItemHeader);
            rowItemData.Cells.Add(cellItemHeader);
            for (int n = 0; n < this.designView.GridColumns.Length; n++)
            {
                HtmlTableCell cellItemData = new HtmlTableCell();
                cellItemData.Style.Add("border", "solid 1px #000000");
                cellItemData.Style.Add("width", "2.5cm");
                bool bIsEmpty = false;
                for (int x = 0; this.designView.GridGroups != null && x < this.designView.GridGroups.Length; x++)
                {
                    if (this.designView.GridGroups[x].ColumnName == this.designView.GridColumns[n].ColumnName)
                    {
                        bIsEmpty = true;
                        break;
                    }
                }
                if (bIsEmpty == false)
                {
                    HtmlInputHidden hidItemData = new HtmlInputHidden();
                    hidItemData.ID = "hidItemData_" + this.designView.GridColumns[n].ColumnName;
                    hidItemData.Attributes.Add("FormatType", "itemdata");
                    hidItemData.Attributes.Add("ColumnName", this.designView.GridColumns[n].ColumnName);
                    hidItemData.Value = this.Request[hidItemData.ID];
                    cellItemData.Controls.Add(hidItemData);
                    HyperLink linkItemData = new HyperLink();
                    linkItemData.NavigateUrl = "#";
                    linkItemData.Text = columnMap[this.designView.GridColumns[n].ColumnName].Description;
                    linkItemData.ID = "linkItemData_" + this.designView.GridColumns[n].ColumnName;
                    linkItemData.Attributes.Add("onclick", "OpenFormatWin('" + "FRptTextFormatMP.aspx?type=itemdata&column=" + this.designView.GridColumns[n].ColumnName + "','" + hidItemData.ClientID + "','" + linkItemData.ClientID + "');return false;");
                    cellItemData.Controls.Add(linkItemData);
                }
                rowItemData.Cells.Add(cellItemData);
            }
            this.tbFormat.Rows.Add(rowItemData);
        }
        private void InitExtendText()
        {
            if (this.designView.ExtendText == null || this.designView.ExtendText.Length == 0)
            {
                return;
            }
            ReportViewFacade facade = new ReportViewFacade(this.DataProvider);
            string strScript = "";
            for (int i = 0; i < this.designView.ExtendText.Length; i++)
            {
                string strId = Convert.ToInt32(this.designView.ExtendText[i].Sequence).ToString();
                string styleValue = "";
                for (int n = 0; this.designView.DataFormats != null && n < this.designView.DataFormats.Length; n++)
                {
                    if (this.designView.DataFormats[n].FormatID == this.designView.ExtendText[i].FormatID)
                    {
                        styleValue = facade.BuildStyleValueFromDataFormat(this.designView.DataFormats[n]);
                        break;
                    }
                }
                if (styleValue != "")
                {
                    strScript += "AddReportTitleFromStyle('" + strId + "','" + styleValue.Replace("'", "\\'") + "');\r\n";
                }
            }
            if (strScript != "")
            {
                strScript += "lastReportTitleId=" + Convert.ToInt32(this.designView.ExtendText[this.designView.ExtendText.Length - 1].Sequence).ToString() + ";\r\n";
                //this.ClientScript.RegisterStartupScript(typeof(string), "add_report_title", "<script language=javascript>" + strScript + "</script>");
                ScriptManager.RegisterStartupScript(this, GetType(), "add_report_title", strScript, true);
            }
        }

        protected override void DisplayDesignData()
        {
            if (this.designView.DefinedReportStyle != null)
            {
                this.drpDefinedStyle.SelectedValue = this.designView.DefinedReportStyle.StyleID.ToString();
                this.ApplyDefinedStyle(this.designView.DefinedReportStyle.StyleID);
            }

            if (this.designView.GridDataFormats != null)
            {
                Dictionary<string, RptViewDataFormat> formatList = new Dictionary<string, RptViewDataFormat>();
                for (int i = 0; i < this.designView.GridDataFormats.Length; i++)
                {
                    RptViewGridDataFormat dataFormat = this.designView.GridDataFormats[i];
                    string strKey = dataFormat.StyleType + ":" + dataFormat.ColumnName + ":" + dataFormat.GroupSequence.ToString();
                    for (int n = 0; this.designView.DataFormats != null && n < this.designView.DataFormats.Length; n++)
                    {
                        if (this.designView.DataFormats[n].FormatID == dataFormat.FormatID)
                        {
                            formatList.Add(strKey, this.designView.DataFormats[n]);
                            break;
                        }
                    }
                }
                for (int i = 1; i < this.tbFormat.Rows.Count; i++)
                {
                    for (int n = 1; n < this.tbFormat.Rows[i].Cells.Count; n++)
                    {
                        HtmlInputHidden hidVal = this.GetFormatInputField(this.tbFormat.Rows[i].Cells[n]);
                        if (hidVal == null)
                        {
                            continue;
                        }
                        string strFormatType = hidVal.Attributes["FormatType"];
                        if (strFormatType == "header")
                            strFormatType = ReportStyleType.Header;
                        else if (strFormatType == "groupdata")
                            strFormatType = ReportStyleType.SubTotal;
                        else if (strFormatType == "itemdata")
                            strFormatType = ReportStyleType.Item;

                        string strColumnName = hidVal.Attributes["ColumnName"];
                        string strGroupSeq = hidVal.Attributes["GroupSeq"];
                        if (strGroupSeq == null || strGroupSeq == "")
                            strGroupSeq = "0";
                        string strKey = strFormatType + ":" + strColumnName + ":" + strGroupSeq;
                        if (formatList.ContainsKey(strKey) == false)
                            continue;
                        RptViewDataFormat dataFormat = formatList[strKey];
                        string strVal = (new ReportViewFacade(this.DataProvider)).BuildStyleValueFromDataFormat(dataFormat);
                        hidVal.Value = strVal;
                    }
                }
            }
        }

        private void InitDefinedStyle()
        {
            this.drpDefinedStyle.Items.Clear();
            this.drpDefinedStyle.Items.Add(new ListItem("", ""));
            ReportViewFacade facade = new ReportViewFacade(this.DataProvider);
            object[] objs = facade.GetAllRptViewReportStyle();
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    RptViewReportStyle style = (RptViewReportStyle)objs[i];
                    this.drpDefinedStyle.Items.Add(new ListItem(style.Name, style.StyleID.ToString()));
                }
            }
        }

        protected override bool ValidateInput()
        {
            return true;
        }

        protected override void RedirectToBack()
        {
            this.Response.Redirect("FRptDesignStep4MP.aspx");
        }

        protected override void RedirectToNext()
        {
        }

        protected override void UpdateReportDesignView()
        {
            ArrayList listGridFormat = new ArrayList();
            ArrayList listDataFormat = new ArrayList();
            for (int i = 1; i < this.tbFormat.Rows.Count; i++)
            {
                for (int n = 1; n < this.tbFormat.Rows[i].Cells.Count; n++)
                {
                    HtmlInputHidden hidVal = this.GetFormatInputField(this.tbFormat.Rows[i].Cells[n]);
                    if (hidVal == null)
                    {
                        continue;
                    }
                    string strFormatType = hidVal.Attributes["FormatType"];
                    string strColumnName = hidVal.Attributes["ColumnName"];
                    string strGroupSeq = hidVal.Attributes["GroupSeq"];
                    RptViewGridDataFormat gridFormat = null;
                    RptViewDataFormat dataFormat = null;
                    this.BuildDataFormat(out gridFormat, out dataFormat, strFormatType, strColumnName, strGroupSeq, hidVal.Value);
                    listGridFormat.Add(gridFormat);
                    listDataFormat.Add(dataFormat);
                }
            }

            ArrayList listExtText = new ArrayList();
            SortedList<int, int> listRptTitleList = new SortedList<int, int>();
            for (int i = 0; i < Request.Form.AllKeys.Length; i++)
            {
                if (Request.Form.AllKeys[i].StartsWith("hidRptTitle") == true)
                {
                    int iTmp = int.Parse(Request.Form.AllKeys[i].Substring(11));
                    listRptTitleList.Add(iTmp, iTmp);
                }
            }
            decimal iAdjExtId = 0;
            foreach (int iIdx in listRptTitleList.Keys)
            {
                string strVal = Request.Form["hidRptTitle" + iIdx.ToString()];
                iAdjExtId++;
                RptViewExtendText extendText = null;
                RptViewDataFormat dataFormat = null;
                this.BuildExtendTextObject(out extendText, out dataFormat, strVal);
                extendText.Sequence = iAdjExtId;
                listDataFormat.Add(dataFormat);
                listExtText.Add(extendText);
            }

            RptViewGridDataFormat[] gridFormats = new RptViewGridDataFormat[listGridFormat.Count];
            listGridFormat.CopyTo(gridFormats);
            RptViewDataFormat[] dataFormats = new RptViewDataFormat[listDataFormat.Count];
            listDataFormat.CopyTo(dataFormats);
            RptViewExtendText[] extendTexts = new RptViewExtendText[listExtText.Count];
            listExtText.CopyTo(extendTexts);

            this.designView.GridDataFormats = gridFormats;
            this.designView.DataFormats = dataFormats;
            this.designView.ExtendText = extendTexts;
        }
        private HtmlInputHidden GetFormatInputField(HtmlTableCell cell)
        {
            for (int i = 0; i < cell.Controls.Count; i++)
            {
                if (cell.Controls[i] is HtmlInputHidden)
                {
                    HtmlInputHidden hid = (HtmlInputHidden)cell.Controls[i];
                    if (hid.Attributes["FormatType"] != null && hid.Attributes["FormatType"] != "")
                    {
                        return hid;
                    }
                }
            }
            return null;
        }
        private void BuildExtendTextObject(out RptViewExtendText extendText, out RptViewDataFormat dataFormat, string styleValue)
        {
            extendText = new RptViewExtendText();
            dataFormat = new RptViewDataFormat();
            string strId = System.Guid.NewGuid().ToString();
            extendText.FormatID = strId;
            extendText.MaintainUser = this.GetUserCode();

            dataFormat = (new ReportViewFacade(this.DataProvider)).BuildDataFormatByStyle(styleValue);
            dataFormat.FormatID = strId;
            dataFormat.MaintainUser = this.GetUserCode();
        }
        private void BuildDataFormat(out RptViewGridDataFormat gridDataFormat, out RptViewDataFormat dataFormat, string formatType, string columnName, string groupSeq, string styleValue)
        {
            gridDataFormat = new RptViewGridDataFormat();
            dataFormat = new RptViewDataFormat();
            string strId = System.Guid.NewGuid().ToString();
            dataFormat.FormatID = strId;
            gridDataFormat.FormatID = strId;

            if (formatType == "header")
            {
                gridDataFormat.StyleType = ReportStyleType.Header;
                gridDataFormat.ColumnName = columnName;
            }
            else if (formatType == "groupdata")
            {
                gridDataFormat.StyleType = ReportStyleType.SubTotal;
                gridDataFormat.ColumnName = columnName;
                gridDataFormat.GroupSequence = decimal.Parse(groupSeq);
            }
            else if (formatType == "itemdata")
            {
                gridDataFormat.StyleType = ReportStyleType.Item;
                gridDataFormat.ColumnName = columnName;
            }

            dataFormat = (new ReportViewFacade(this.DataProvider)).BuildDataFormatByStyle(styleValue);
            dataFormat.FormatID = strId;
            dataFormat.MaintainUser = this.GetUserCode();

        }

        #endregion


        void cmdOK_ServerClick(object sender, EventArgs e)
        {
            ReportViewFacade facade = new ReportViewFacade(this.DataProvider);
            if (this.drpDefinedStyle.SelectedValue == "" && this.designView.DefinedReportStyle != null)       // 删除样式
            {
                this.designView.DefinedReportStyle = null;
                ClearExistStyle();
            }
            else if (this.drpDefinedStyle.SelectedValue != "" && (this.designView.DefinedReportStyle == null || this.designView.DefinedReportStyle.StyleID.ToString() != this.drpDefinedStyle.SelectedValue))
            {
                this.designView.DefinedReportStyle = (RptViewReportStyle)facade.GetRptViewReportStyle(decimal.Parse(this.drpDefinedStyle.SelectedValue));
                this.ApplyDefinedStyle(this.designView.DefinedReportStyle.StyleID);
            }
        }
        private void ClearExistStyle()
        {
            for (int i = 0; i < this.tbFormat.Rows.Count; i++)
            {
                for (int n = 0; n < this.tbFormat.Rows[i].Cells.Count; n++)
                {
                    HtmlInputHidden hidVal = this.GetFormatInputField(this.tbFormat.Rows[i].Cells[n]);
                    if (hidVal == null)
                    {
                        continue;
                    }
                    hidVal.Value = "";
                }
            }
        }
        private void ApplyDefinedStyle(decimal styleId)
        {
            ReportViewFacade facade = new ReportViewFacade(this.DataProvider);
            RptViewReportStyleDetail[] styleDtls = facade.GetRptViewReportStyleDetailByStyleID(styleId);
            if (styleDtls == null || styleDtls.Length == 0)
            {
                ClearExistStyle();
                return;
            }
            for (int i = 1; i < this.tbFormat.Rows.Count; i++)
            {
                for (int n = 0; n < this.tbFormat.Rows[i].Cells.Count; n++)
                {
                    HtmlInputHidden hidVal = this.GetFormatInputField(this.tbFormat.Rows[i].Cells[n]);
                    if (hidVal == null)
                    {
                        continue;
                    }
                    string strFormatType = hidVal.Attributes["FormatType"];
                    string strStyleType = "";
                    if (strFormatType == "header" || strFormatType == "headerrow")
                        strStyleType = ReportStyleType.Header;
                    else if (strFormatType == "group")
                        strStyleType = ReportStyleType.SubTotal;
                    else if (strFormatType == "groupdata")
                    {
                        strStyleType = "";
                        for (int j = 0; this.designView.GridGroups != null && j < this.designView.GridGroups.Length; j++)
                        {
                            if (this.designView.GridGroups[j].ColumnName == hidVal.Attributes["ColumnName"])
                            {
                                strStyleType = ReportStyleType.SubTotalGroupField;
                                break;
                            }
                        }
                        if (strStyleType == "")
                        {
                            for (int j = 0; this.designView.GridGroupTotals != null && j < this.designView.GridGroupTotals.Length; j++)
                            {
                                if (this.designView.GridGroupTotals[j].GroupSequence == decimal.Parse(hidVal.Attributes["GroupSeq"]) &&
                                    this.designView.GridGroupTotals[j].ColumnName == hidVal.Attributes["ColumnName"])
                                {
                                    if (this.designView.GridGroupTotals[j].TotalType == ReportTotalType.Empty)
                                        strStyleType = ReportStyleType.SubTotalNonCalField;
                                    else
                                        strStyleType = ReportStyleType.SubTotalCalField;
                                }
                            }
                        }
                    }
                    else if (strFormatType == "itemdata" || strFormatType == "item")
                        strStyleType = ReportStyleType.Item;

                    hidVal.Value = "";
                    RptViewReportStyleDetail styleDtl = this.GetRptStyleDetailByType(styleDtls, strStyleType);
                    if (styleDtl != null)
                    {
                        RptViewDataFormat dataFormat = (RptViewDataFormat)facade.GetRptViewDataFormat(styleDtl.FormatID);
                        if (dataFormat != null)
                        {
                            hidVal.Value = facade.BuildStyleValueFromDataFormat(dataFormat);
                        }
                    }
                }
            }
        }
        private RptViewReportStyleDetail GetRptStyleDetailByType(RptViewReportStyleDetail[] styleDtls, string styleType)
        {
            for (int i = 0; i < styleDtls.Length; i++)
            {
                if (styleDtls[i].StyleType == styleType)
                    return styleDtls[i];
            }
            return null;
        }

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
                // this.ClientScript.RegisterStartupScript(GetType(), "OpenPreviewWindow", strScript,true);
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenPreviewWindow", strScript, true);
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
            if (!this.ClientScript.IsClientScriptBlockRegistered("SaveSuccess"))
            {
                // this.ClientScript.RegisterClientScriptBlock(typeof(string), "SaveSuccess", alertInfo);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "SaveSuccess", alertInfo, true);
            }
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



    }
}
