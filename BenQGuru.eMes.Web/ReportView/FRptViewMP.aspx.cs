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

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Domain.ReportView;

namespace BenQGuru.eMES.Web.ReportView
{
    /// <summary>
    /// FRptViewMP 的摘要说明。
    /// </summary>
    public partial class FRptViewMP : BaseMPageMinus
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.ReportView.ReportViewFacade rptFacade;
        private RptViewDesignMain designMain;
        private bool existNeedInputField = false;

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {           
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
            this.CreateInputField();
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

            this.cmdQuery.ServerClick += new EventHandler(cmdQuery_ServerClick);
        }
        #endregion

        #region Init
        private string ReportID
        {
            get
            {
                return this.GetRequestParam("reportid");
            }
        }

        private void CreateInputField()
        {
            if (this.ReportID == "")
            {
                return;
            }
            if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
            designMain = (RptViewDesignMain)rptFacade.GetRptViewDesignMain(this.ReportID);
            if (designMain == null)
                return;
            if (this.GetRequestParam("preview") != "1")
            {
                if (designMain.Status != ReportDesignStatus.Publish)
                    return;
            }
            if (this.IsPostBack == false && this.GetRequestParam("preview") != "1")
            {
                if (CheckAccessReportRight() == false)
                {
                    return;
                }
            }

            RptViewUserSubscription[] userSubscr = rptFacade.GetNeedInputByReportId(this.ReportID, this.GetUserCode());

            RptViewFilterUI[] filterUI = rptFacade.GetRptViewFilterUIByReportId(this.ReportID);

            // 添加控件
            this.tbInput.Rows.Clear();
            for (int i = 0; i < userSubscr.Length; )
            {
                HtmlTableRow row = new HtmlTableRow();
                for (int n = 0; n < 3; n++)
                {
                    if (i <= userSubscr.Length - 1)
                    {
                        if (userSubscr[i].InputValue == "")
                            existNeedInputField = true;
                        HtmlTableCell[] cells = GenerateCell(userSubscr[i], filterUI);
                        row.Cells.Add(cells[0]);
                        row.Cells.Add(cells[1]);
                        i++;
                    }
                }
                this.tbInput.Rows.Add(row);
            }

        }
        private HtmlTableCell[] GenerateCell(RptViewUserSubscription userSubs, RptViewFilterUI[] filterUIList)
        {
            HtmlGenericControl title = new HtmlGenericControl();
            title.InnerText = userSubs.EAttribute1;
            HtmlTableCell cellTitle = new HtmlTableCell();
            cellTitle.NoWrap = true;
            cellTitle.Attributes.Add("class", "fieldNameLeft");
            cellTitle.Controls.Add(title);
            RptViewFilterUI filterUI = null;

            if (filterUIList != null)
            {
                for (int i = 0; i < filterUIList.Length; i++)
                {
                    if (filterUIList[i].InputType == userSubs.InputType &&
                        filterUIList[i].InputName == userSubs.InputName &&
                        filterUIList[i].SqlFilterSequence == userSubs.SqlFilterSequence)
                    {
                        filterUI = filterUIList[i];
                        break;
                    }
                }
            }

            if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
            RptViewFilterUI[] objUIs = rptFacade.GetRptViewFilterUIByReportIdAndSeq(this.ReportID, userSubs.InputName, userSubs.SqlFilterSequence);
            bool checkExist = false;
            if (objUIs != null && objUIs.Length > 0)
            {
                foreach (RptViewFilterUI objUI in objUIs)
                {
                    if (objUI.CheckExist == "Y")
                    {
                        checkExist = true;
                        break;
                    }
                }
            }

            Control controlInput = null;
            if (filterUI == null || filterUI.UIType == ReportFilterUIType.InputText)
            {
                TextBox txt = new TextBox();
                txt.ID = "txtViewerInput_" + userSubs.InputType + "_" + userSubs.InputName + "_" + userSubs.SqlFilterSequence.ToString();
                txt.Attributes.Add("IsReportViewerInput", "1");
                txt.Attributes.Add("InputName", userSubs.InputName);
                txt.Attributes.Add("InputType", userSubs.InputType);
                txt.Attributes.Add("InputDesc", userSubs.EAttribute1);
                txt.Attributes.Add("SqlFilterSequence", userSubs.SqlFilterSequence.ToString());
                txt.Text = userSubs.InputValue;
                //Modified 2008/11/07
                if (checkExist)
                {
                    txt.CssClass = "require";
                }
                else
                {
                    txt.CssClass = "textbox";
                }

                controlInput = txt;
            }
            else if (filterUI.UIType == ReportFilterUIType.CheckBox)
            {
                CheckBox chk = new CheckBox();
                chk.ID = "txtViewerInput_" + userSubs.InputType + "_" + userSubs.InputName + "_" + userSubs.SqlFilterSequence.ToString();
                chk.Attributes.Add("IsReportViewerInput", "1");
                chk.Attributes.Add("InputName", userSubs.InputName);
                chk.Attributes.Add("InputType", userSubs.InputType);
                chk.Attributes.Add("InputDesc", userSubs.EAttribute1);
                chk.Attributes.Add("SqlFilterSequence", userSubs.SqlFilterSequence.ToString());
                chk.Checked = FormatHelper.StringToBoolean(userSubs.InputValue);
                if (checkExist)
                {
                    chk.CssClass = "require";
                }
                else
                {
                    chk.CssClass = "textbox";
                }
                controlInput = chk;
            }
            else if (filterUI.UIType == ReportFilterUIType.Date)
            {
                TextBox dateCtl = new TextBox();
                //BenQGuru.eMES.Web.UserControl.eMESDate dateCtl = (BenQGuru.eMES.Web.UserControl.eMESDate)Page.LoadControl("../UserControl/DateTime/DateTime/eMESDate.ascx");
                dateCtl.ID = "txtViewerInput_" + userSubs.InputType + "_" + userSubs.InputName + "_" + userSubs.SqlFilterSequence.ToString();
                dateCtl.Attributes.Add("IsReportViewerInput", "1");
                dateCtl.Attributes.Add("InputName", userSubs.InputName);
                dateCtl.Attributes.Add("InputType", userSubs.InputType);
                dateCtl.Attributes.Add("InputDesc", userSubs.EAttribute1);
                dateCtl.Attributes.Add("SqlFilterSequence", userSubs.SqlFilterSequence.ToString());
                dateCtl.Text = userSubs.InputValue;
                if (checkExist)
                {
                    dateCtl.CssClass = "require datepicker";
                }
                else
                {
                    dateCtl.CssClass = "textbox datepicker";
                }
                controlInput = dateCtl;
            }
            else if (filterUI.UIType == ReportFilterUIType.SelectQuery)
            {
                BenQGuru.eMES.Web.SelectQuery.SelectableTextBox txt = new BenQGuru.eMES.Web.SelectQuery.SelectableTextBox();
                txt.Type = filterUI.SelectQueryType;
                txt.ID = "txtViewerInput_" + userSubs.InputType + "_" + userSubs.InputName + "_" + userSubs.SqlFilterSequence.ToString();
                txt.Attributes.Add("IsReportViewerInput", "1");
                txt.Attributes.Add("InputName", userSubs.InputName);
                txt.Attributes.Add("InputType", userSubs.InputType);
                txt.Attributes.Add("InputDesc", userSubs.EAttribute1);
                txt.Attributes.Add("SqlFilterSequence", userSubs.SqlFilterSequence.ToString());
                txt.Text = userSubs.InputValue;
                txt.CanKeyIn = true;
                if (checkExist)
                {
                    txt.CssClass = "require";
                }
                else
                {
                    txt.CssClass = "textbox";
                }
                controlInput = txt;
            }
            else if (filterUI.UIType == ReportFilterUIType.DropDownList)
            {
                DropDownList drp = new DropDownList();
                drp.ID = "txtViewerInput_" + userSubs.InputType + "_" + userSubs.InputName + "_" + userSubs.SqlFilterSequence.ToString();
                drp.Attributes.Add("IsReportViewerInput", "1");
                drp.Attributes.Add("InputName", userSubs.InputName);
                drp.Attributes.Add("InputType", userSubs.InputType);
                drp.Attributes.Add("InputDesc", userSubs.EAttribute1);
                drp.Attributes.Add("SqlFilterSequence", userSubs.SqlFilterSequence.ToString());
                if (checkExist)
                {
                    drp.CssClass = "require";
                }
                else
                {
                    drp.CssClass = "textbox";
                }
                if (filterUI.ListDataSourceType == "static")
                {
                    string[] strTmpList = filterUI.ListStaticValue.Split(';');
                    for (int i = 0; i < strTmpList.Length; i++)
                    {
                        if (strTmpList[i] != "" && strTmpList[i].IndexOf(",") >= 0)
                        {
                            string[] strTmpVal = strTmpList[i].Split(',');
                            drp.Items.Add(new ListItem(strTmpVal[0], strTmpVal[1]));
                        }
                    }
                    //Add 2008/11/05 如果是必选则不需要加空格选项，否则加空格选项代表是全选
                    //if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
                    //RptViewFilterUI[] objUIs = rptFacade.GetRptViewFilterUIByReportIdAndSeq(this.ReportID, userSubs.InputName);
                    //if (objUIs != null && objUIs.Length > 0)
                    //{
                    //    foreach (RptViewFilterUI objUI in objUIs)
                    //    {
                    //        if (objUI.CheckExist == "N")
                    //        {
                    //            drp.Items.Insert(0,new ListItem("",""));
                    //        }
                    //    }
                    //}
                    if (!checkExist)
                    {
                        drp.Items.Insert(0, new ListItem("", ""));
                    }
                }
                else
                {
                    RptViewDataSource dataSource = (RptViewDataSource)rptFacade.GetRptViewDataSource(filterUI.ListDynamicDataSource);
                    if (dataSource != null)
                    {
                        if (filterUI.ListDynamicTextColumn == "" && filterUI.ListDynamicValueColumn != "")
                            filterUI.ListDynamicTextColumn = filterUI.ListDynamicValueColumn;
                        if (filterUI.ListDynamicValueColumn == "" && filterUI.ListDynamicTextColumn != "")
                            filterUI.ListDynamicValueColumn = filterUI.ListDynamicTextColumn;
                        DataSet ds = rptFacade.ExecuteDataSetFromSource(dataSource, Server.MapPath("").ToString().Substring(0, Server.MapPath("").ToString().LastIndexOf("\\")));
                        string columnName = filterUI.ListDynamicTextColumn;
                        string columnValue = filterUI.ListDynamicValueColumn;
                        ds.Tables[0].DefaultView.Sort = columnName;
                        for (int i = 0; i < ds.Tables[0].DefaultView.Count; i++)
                        {
                            string strOptItemText = "";
                            bool checkRe = false;

                            if (ds.Tables[0].DefaultView[i][columnName] != System.DBNull.Value)
                                strOptItemText = ds.Tables[0].DefaultView[i][columnName].ToString();
                            string strOptItemValue = "";
                            if (ds.Tables[0].DefaultView[i][columnValue] != System.DBNull.Value)
                                strOptItemValue = ds.Tables[0].DefaultView[i][columnValue].ToString();
                            if (drp.Items.Count > 0)
                            {
                                for (int k = 0; k < drp.Items.Count; k++)
                                {
                                    if (strOptItemValue == drp.Items[k].Value && strOptItemText == drp.Items[k].Text)
                                    {
                                        checkRe = true;
                                        break;
                                    }
                                }
                            }
                            if (!checkRe)
                            {
                                drp.Items.Add(new ListItem(strOptItemText, strOptItemValue));
                            }
                        }
                        //Add 2008/11/05 如果是必选则不需要加空格选项，否则加空格选项代表是全选
                        if (!checkExist)
                        {
                            drp.Items.Insert(0, new ListItem("", ""));
                        }
                    }
                }


                if (drp.Items.FindByValue(userSubs.InputValue) != null)
                    drp.SelectedValue = userSubs.InputValue;

                controlInput = drp;
            }
            //Add 2008/11/04 增加了复选框的查询
            else if (filterUI.UIType == ReportFilterUIType.SelectComplex)
            {
                BenQGuru.eMES.Web.SelectQuery.SelectableTextBox txt = new BenQGuru.eMES.Web.SelectQuery.SelectableTextBox();
                string[] strQueryType = filterUI.SelectQueryType.ToString().Split('#');
                txt.Type = strQueryType[0];

                HtmlTableRow row = new HtmlTableRow();
                HtmlTableCell cell = new HtmlTableCell();
                TextBox textBox1 = new TextBox();
                TextBox textBox2 = new TextBox();
                TextBox textBox3 = new TextBox();
                TextBox textBox4 = new TextBox();

                textBox1.ID = "txtDataSource" + userSubs.SqlFilterSequence.ToString("0000");                
                textBox2.ID = "txtDataType" + userSubs.SqlFilterSequence.ToString("0000");
                textBox3.ID = "txtDataCode" + userSubs.SqlFilterSequence.ToString("0000");
                textBox4.ID = "txtDataDesc" + userSubs.SqlFilterSequence.ToString("0000");
                

                textBox2.Text = filterUI.ListDataSourceType;
                if (filterUI.ListDataSourceType.ToString().ToUpper() != "STATIC")
                {
                    textBox3.Text = strQueryType[1];
                    textBox4.Text = strQueryType[2];
                    textBox1.Text = filterUI.ListDynamicDataSource.ToString();
                }
                else
                {
                    textBox1.Text = filterUI.ListStaticValue;
                }

                cell.Controls.Add(textBox1);
                cell.Controls.Add(textBox2);
                cell.Controls.Add(textBox3);
                cell.Controls.Add(textBox4);

                row.Cells.Add(cell);
                row.Attributes.Add("style", "display:none;");
                this.tableTextBox.Rows.Add(row);

                txt.ID = "txtViewerInput_" + userSubs.InputType + "_" + userSubs.InputName + "_" + userSubs.SqlFilterSequence.ToString("0000");
                txt.Attributes.Add("IsReportViewerInput", "1");
                txt.Attributes.Add("InputName", userSubs.InputName);
                txt.Attributes.Add("InputType", userSubs.InputType);
                txt.Attributes.Add("InputDesc", userSubs.EAttribute1);
                txt.Attributes.Add("SqlFilterSequence", userSubs.SqlFilterSequence.ToString());
                txt.Text = userSubs.InputValue;
                txt.CanKeyIn = true;
                if (checkExist)
                {
                    txt.CssClass = "require";
                }
                else
                {
                    txt.CssClass = "textbox";
                }
                controlInput = txt;
            }

            HtmlTableCell cellValue = new HtmlTableCell();
            cellValue.NoWrap = true;
            cellValue.Attributes.Add("class", "fieldValue");
            cellValue.Style.Add("padding-right", "10px");
            cellValue.Controls.Add(controlInput);

            return new HtmlTableCell[] { cellTitle, cellValue };
        }

        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        private bool CheckAccessReportRight()
        {
            SessionHelper sessionHelper = SessionHelper.Current(this.Session);
            if (sessionHelper.UserCode.ToUpper() == "ADMIN" ||
                sessionHelper.IsBelongToAdminGroup == true)
                return true;
            if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
            return rptFacade.CheckUserReportSecurity(this.ReportID, sessionHelper.UserCode);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (this.IsPostBack == false)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false); 
                this.cmdQuery.Visible = false;
                spanMsg.Visible = false;
                this.ReportViewer1.Visible = false;
                if (this.ReportID == "")
                {
                    //throw new Exception("$Error_RequestUrlParameter_Lost");
                    spanMsg.Visible = true;
                    spanMsg.InnerText = this.languageComponent1.GetString("$Error_RequestUrlParameter_Lost");
                    return;
                }
                if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
                designMain = (RptViewDesignMain)rptFacade.GetRptViewDesignMain(this.ReportID);
                if (designMain == null)
                {
                    //throw new Exception("$Error_RequestUrlParameter_Lost");
                    spanMsg.Visible = true;
                    spanMsg.InnerText = this.languageComponent1.GetString("$Error_RequestUrlParameter_Lost");
                    return;
                }
                lblRptViewTitle.Text = designMain.ReportName;
                if (this.GetRequestParam("preview") != "1")
                {
                    if (designMain.Status != ReportDesignStatus.Publish)
                    {
                        //throw new Exception("$ReportView_Status_Error");
                        spanMsg.Visible = true;
                        spanMsg.InnerText = this.languageComponent1.GetString("$ReportView_Status_Error");
                        return;
                    }
                }
                if (this.GetRequestParam("preview") != "1" && CheckAccessReportRight() == false)
                {
                    //throw new Exception("$ReportView_No_Right_ViewReport [" + designMain.ReportName + "]");
                    spanMsg.Visible = true;
                    spanMsg.InnerText = this.languageComponent1.GetString("$ReportView_No_Right_ViewReport") + " [" + designMain.ReportName + "]";
                    return;
                }
                this.txtDataCount.Text = "0";
                this.cmdQuery.Visible = true;
                this.ReportViewer1.Visible = true;
                this.lblRptViewTitle.Text = designMain.ReportName;
                if (existNeedInputField == false)
                {
                    //this.cmdQuery_ServerClick(null, null);
                }
            }
        }

        #endregion


        // 查看报表
        private void cmdQuery_ServerClick(object sender, EventArgs e)
        {
            ArrayList listInput = new ArrayList();
            string strMess = string.Empty;
            // 遍历每个输入项
            for (int i = 0; i < this.tbInput.Rows.Count; i++)
            {
                for (int n = 0; n < this.tbInput.Rows[i].Cells.Count; n++)
                {
                    for (int x = 0; x < this.tbInput.Rows[i].Cells[n].Controls.Count; x++)
                    {
                        Control ctl = this.tbInput.Rows[i].Cells[n].Controls[x];
                        string strType = "", strName = "", strValue = "";
                        decimal dSqlSeq = 0;
                        bool bIsInputControl = false;
                        if (ctl is WebControl || ctl is System.Web.UI.UserControl)
                        {
                            if (ctl is WebControl && ((WebControl)ctl).Attributes["IsReportViewerInput"] == "1")
                            {
                                bIsInputControl = true;
                                WebControl wcontrol = (WebControl)ctl;
                                strType = wcontrol.Attributes["InputType"];
                                strName = wcontrol.Attributes["InputName"];
                                dSqlSeq = decimal.Parse(wcontrol.Attributes["SqlFilterSequence"]);
                            }
                            else if (ctl is System.Web.UI.UserControl && ((System.Web.UI.UserControl)ctl).Attributes["IsReportViewerInput"] == "1")
                            {
                                bIsInputControl = true;
                                System.Web.UI.UserControl wcontrol = (System.Web.UI.UserControl)ctl;
                                strType = wcontrol.Attributes["InputType"];
                                strName = wcontrol.Attributes["InputName"];
                                dSqlSeq = decimal.Parse(wcontrol.Attributes["SqlFilterSequence"]);
                            }
                        }
                        if (bIsInputControl == true)
                        {
                            if (ctl is TextBox)
                                strValue = ((TextBox)ctl).Text.Trim().ToUpper();
                            else if (ctl is CheckBox)
                                strValue = FormatHelper.BooleanToString(((CheckBox)ctl).Checked);
                            else if (ctl is DropDownList)
                                strValue = ((DropDownList)ctl).SelectedValue;
                            else if (ctl is UserControl.eMESDate)
                                strValue = FormatHelper.TODateInt(((UserControl.eMESDate)ctl).Text).ToString();
                            else if (ctl is BenQGuru.eMES.Web.SelectQuery.SelectableTextBox)
                                strValue = ((BenQGuru.eMES.Web.SelectQuery.SelectableTextBox)ctl).Text;

                            RptViewUserSubscription subs = new RptViewUserSubscription();
                            subs.InputType = strType;
                            subs.InputName = strName;
                            //Add  2008/11/04
                            if (string.IsNullOrEmpty(strValue) || (ctl is UserControl.eMESDate && strValue == "0"))
                            {
                                if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
                                RptViewFilterUI[] objUIs = rptFacade.GetRptViewFilterUIByReportIdAndSeq(this.ReportID, strName, dSqlSeq);
                                if (objUIs != null && objUIs.Length > 0)
                                {
                                    foreach (RptViewFilterUI objUI in objUIs)
                                    {
                                        if (objUI.CheckExist == "Y")
                                        {
                                            //添加异常
                                            //throw new Exception( strName + "$ReportDesign_NOT_AllowNull");
                                            //string alertInfo =
                                            // string.Format("<script language=javascript>alert('{0}');</script>",strName + " " +this.languageComponent1.GetString("$ReportDesign_NOT_AllowNull"));
                                            //if (!this.ClientScript.IsClientScriptBlockRegistered("Message"))
                                            //{
                                            //    this.ClientScript.RegisterClientScriptBlock(typeof(string), "Message", alertInfo);
                                            //}
                                            //return;
                                            if (string.IsNullOrEmpty(strMess))
                                            {
                                                if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
                                                RptViewGridFilter objFilter = rptFacade.GetRptViewGridFiltersByReportIdAndName(this.ReportID, strName, dSqlSeq);
                                                if (objFilter != null)
                                                {
                                                    strMess = objFilter.Description;
                                                }
                                                else
                                                {
                                                    strMess = strName;
                                                }
                                            }
                                            else
                                            {
                                                if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
                                                RptViewGridFilter objFilter = rptFacade.GetRptViewGridFiltersByReportIdAndName(this.ReportID, strName, dSqlSeq);
                                                if (objFilter != null)
                                                {
                                                    strMess = strMess + "," + objFilter.Description;
                                                }
                                                else
                                                {
                                                    strMess = strMess + "," + strName;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //Add End 
                            subs.InputValue = strValue;
                            subs.SqlFilterSequence = dSqlSeq;
                            listInput.Add(subs);
                        }
                        /*
                        if (ctl is TextBox)
                        {
                            if (((TextBox)ctl).Attributes["IsReportViewerInput"] == "1")
                            {
                                TextBox txt = (TextBox)ctl;
                                string strType = txt.Attributes["InputType"];
                                string strName = txt.Attributes["InputName"];
                                string strValue = txt.Text.Trim().ToUpper();
                                decimal dSqlSeq = decimal.Parse(txt.Attributes["SqlFilterSequence"]);
                                RptViewUserSubscription subs = new RptViewUserSubscription();
                                subs.InputType = strType;
                                subs.InputName = strName;
                                subs.InputValue = strValue;
                                subs.SqlFilterSequence = dSqlSeq;
                                listInput.Add(subs);
                            }
                        }
                        */

                    }
                }
            }

            //查询所有的输入项，一次性报错
            if (strMess != null && strMess.Length > 0)
            {
                string alertInfo =
                 string.Format("<script language=javascript>alert('{0}');</script>", strMess + "  " + this.languageComponent1.GetString("$ReportDesign_NOT_AllowNull"));
                //if (!this.ClientScript.IsClientScriptBlockRegistered("Message"))
                //{
                   // this.ClientScript.RegisterClientScriptBlock(typeof(string), "Message", alertInfo);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "Message", alertInfo,false);
                //}
                return;
            }
            // 执行查询
            RptViewUserSubscription[] viewerInput = new RptViewUserSubscription[listInput.Count];
            listInput.CopyTo(viewerInput);
            if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
            DataSet dsSource = rptFacade.ExecuteDataSetFromSource(this.ReportID, viewerInput, Server.MapPath("").ToString().Substring(0, Server.MapPath("").ToString().LastIndexOf("\\")));
            this.txtDataCount.Text=dsSource.Tables[0].Rows.Count.ToString();
            // 绑定报表
            DisplayReportHelper rptHelper = new DisplayReportHelper();
            rptHelper.BindReportViewer(rptFacade, designMain, this.ReportViewer1, dsSource, viewerInput);
            this.ReportViewer1.DataBind();

            string[] values = new string[viewerInput.Length];
            for (int i = 0; i < viewerInput.Length; i++)
            {
                values[i] = ((RptViewUserSubscription)viewerInput[i]).InputValue;
            }
            //导出报表数据
            BenQGuru.eMES.DataExp.BaseEngine.ExpData(designMain.ReportName, dsSource.Tables[0], values);
        }

        protected void ReportViewer1_Drillthrough(object sender, Microsoft.Reporting.WebForms.DrillthroughEventArgs e)
        {
            Microsoft.Reporting.WebForms.LocalReport lr = e.Report as Microsoft.Reporting.WebForms.LocalReport;

            if (rptFacade == null) { rptFacade = new ReportViewFacade(this.DataProvider); }
            RptViewDesignMain rptDesign = rptFacade.GetRptViewDesignMainByReportName(e.ReportPath.ToUpper());

            RptViewUserSubscription[] viewerInput = new RptViewUserSubscription[lr.OriginalParametersToDrillthrough.Count];

            RptViewDataSource dataSource = (RptViewDataSource)rptFacade.GetRptViewDataSource(designMain.DataSourceID);

            for (int i = 0; i < lr.OriginalParametersToDrillthrough.Count; i++)
            {
                viewerInput[i] = new RptViewUserSubscription();
                viewerInput[i].InputName = lr.OriginalParametersToDrillthrough[i].Name;

                if (dataSource.SourceType == DataSourceType.SQL)
                {
                    viewerInput[i].InputType = ReportViewerInputType.SqlFilter;
                    viewerInput[i].SqlFilterSequence = i + 1;
                }
                else
                    viewerInput[i].InputType = ReportViewerInputType.DllParameter;

                viewerInput[i].InputValue = lr.OriginalParametersToDrillthrough[i].Values[0];
            }

            DataSet dsSource = rptFacade.ExecuteDataSetFromSource(rptDesign.ReportID, viewerInput, Server.MapPath("").ToString().Substring(0, Server.MapPath("").ToString().LastIndexOf("\\")));
            lr.DataSources.Clear();
            lr.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("MESRPT", dsSource.Tables[0]));
        }
    }
}
