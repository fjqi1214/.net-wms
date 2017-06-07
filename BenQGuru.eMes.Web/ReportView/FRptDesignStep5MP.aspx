<%@ Page Language="c#" CodeBehind="FRptDesignStep5MP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.ReportView.FRptDesignStep5MP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>报表设计 5/5</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script language="javascript">
//        $(function () {
//            $("#tableMain").height($(window).height());
//            $("#lstUnSelectColumn").height($(window).height() - 165);
//            $("#gridWebGrid").height($(window).height() - 165);
//        })
        function AdjustHeight() {
            $("#tableMain").height($(window).height());
            $("#lstUnSelectColumn").height($(window).height() - 165);
            $("#gridWebGrid").height($(window).height() - 165);
        }
        function OpenFormatWin(url, valEle, linkEle) {
            url = url + "&existvalue=" + escape(document.getElementById(valEle).value).replace(/\+/g, "%2b");
            var ret = window.showModalDialog(url, "", "dialogHeight:620px;dialogWidth:420px");
            if (ret != null && ret != undefined && ret != "") {
                document.getElementById(valEle).value = ret;
                SetLinkStyle(linkEle, valEle);
                var formatType = document.getElementById(valEle).getAttribute("FormatType");
                var columnName = document.getElementById(valEle).getAttribute("ColumnName");
                var groupSeq = document.getElementById(valEle).getAttribute("GroupSeq");
                SetAliasEleStyle(formatType, columnName, groupSeq, ret);

                SetReportTitleWidth();
            }
        }
        function SetLinkStyle(linkEle, valEle) {
            var val = document.getElementById(valEle).value;
            if (val == "")
                return;
            var styleList = new Array();
            styleList = val.split(";");
            var lnk = document.getElementById(linkEle);
            lnk.style.fontFamily = styleList[0];
            lnk.style.fontSize = styleList[1];
            if (styleList[2] == "true")
                lnk.style.fontWeight = "bold";
            else
                lnk.style.fontWeight = "normal";
            if (styleList[3] == "true")
                lnk.style.fontStyle = "italic";
            else
                lnk.style.fontStyle = "normal";
            if (styleList[4] == "true")
                lnk.style.textDecoration = "underline";
            else
                lnk.style.textDecoration = "none";
            lnk.style.color = styleList[5];
            lnk.parentNode.style.backgroundColor = styleList[6];
            if (styleList[7].indexOf("center") >= 0)
                lnk.parentNode.style.textAlign = "center";
            else if (styleList[7].indexOf("right") >= 0)
                lnk.parentNode.style.textAlign = "right";
            else
                lnk.parentNode.style.textAlign = "left";
            if (styleList[8].indexOf("top") >= 0)
                lnk.parentNode.style.verticalAlign = "top";
            else if (styleList[8].indexOf("bottom") >= 0)
                lnk.parentNode.style.verticalAlign = "bottom";
            else
                lnk.parentNode.style.verticalAlign = "middle";
            if (styleList[9] != "")
                lnk.parentNode.style.width = styleList[9] + "cm";
            if (styleList[10] == "true")
                lnk.parentNode.style.border = "solid 1px #000000";
            else
                lnk.parentNode.style.border = "";
        }
        function SetAliasEleStyle(formatType, columnName, groupSeq, styleValue) {
            if (formatType != "column" && formatType != "headerrow" && formatType != "group" && formatType != "item")
                return;

            var inputs = document.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "hidden") {
                    if (formatType == "column") {
                        if (IsColumnHeaderStyle(inputs[i], columnName) == true) {
                            inputs[i].value = styleValue;
                            var linkEle = "link" + inputs[i].id.substr(3);
                            SetLinkStyle(linkEle, inputs[i].id);
                        }
                    }
                    else if (formatType == "headerrow") {
                        if (IsHeaderRowStyle(inputs[i]) == true) {
                            inputs[i].value = styleValue;
                            var linkEle = "link" + inputs[i].id.substr(3);
                            SetLinkStyle(linkEle, inputs[i].id);
                        }
                    }
                    else if (formatType == "group") {
                        if (IsGroupHeaderStyle(inputs[i], groupSeq) == true) {
                            inputs[i].value = styleValue;
                            var linkEle = "link" + inputs[i].id.substr(3);
                            SetLinkStyle(linkEle, inputs[i].id);
                        }
                    }
                    else if (formatType == "item") {
                        if (IsItemHeaderStyle(inputs[i]) == true) {
                            inputs[i].value = styleValue;
                            var linkEle = "link" + inputs[i].id.substr(3);
                            SetLinkStyle(linkEle, inputs[i].id);
                        }
                    }
                }
            }
        }
        function IsColumnHeaderStyle(input, columnName) {
            var formatType = input.getAttribute("FormatType");
            if (formatType != undefined && formatType != null && formatType != "") {
                var inputColName = input.getAttribute("ColumnName");
                if (inputColName == columnName && (formatType == "column" || formatType == "header" || formatType == "groupdata" || formatType == "itemdata")) {
                    return true;
                }
            }
            return false;
        }
        function IsHeaderRowStyle(input) {
            var formatType = input.getAttribute("FormatType");
            if (formatType != undefined && formatType != null && formatType != "") {
                if (formatType == "header") {
                    return true;
                }
            }
            return false;
        }
        function IsGroupHeaderStyle(input, groupSeq) {
            var formatType = input.getAttribute("FormatType");
            if (formatType != undefined && formatType != null && formatType != "") {
                var grpSeq = input.getAttribute("GroupSeq");
                if (grpSeq == groupSeq && formatType == "groupdata") {
                    return true;
                }
            }
            return false;
        }
        function IsItemHeaderStyle(input) {
            var formatType = input.getAttribute("FormatType");
            if (formatType != undefined && formatType != null && formatType != "") {
                if (formatType == "itemdata") {
                    return true;
                }
            }
            return false;
        }
        function UpdateLinkStyleOnLoad() {
            var inputs = document.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "hidden") {
                    var formatType = inputs[i].getAttribute("FormatType");
                    if (formatType != undefined && formatType != null && formatType != "") {
                        var linkEle = "link" + inputs[i].id.substr(3);
                        SetLinkStyle(linkEle, inputs[i].id);
                    }
                }
            }
        }
        function OpenChartDesignWin() {
            var url = "FRptDesignChartMP.aspx";
            window.open(url, "", "height=500px;width=700px;resizable=no", true);
        }


        function SetReportTitleWidth() {
            //            document.getElementById("tbReportTitle").style.width = document.getElementById("tbFormat").clientWidth;
            $("#tbReportTitle").width($(document.body)[0].clientWidth);
        }
        var lastReportTitleId = 0;
        function AddReportTitle() {
            var url = "FRptTextFormatMP.aspx";
            var ret = window.showModalDialog(url, "", "dialogHeight:620px;dialogWidth:400px");
            if (ret != null && ret != undefined && ret != "") {
                AddReportTitleFromStyle(undefined, ret);
            }
        }
        function AddReportTitleFromStyle(titleId, styleValue) {
            var label = GenerateReportTitleLabelHtml(titleId);
            var tbRptTitle = document.getElementById("tbReportTitle");
            var td0 = document.createElement("td");
            td0.style.width = "100%";
            td0.innerHTML = label;
            var tr0 = document.createElement("tr");
            tr0.appendChild(td0);
            tbRptTitle.childNodes[0].appendChild(tr0);
            if (titleId == undefined || titleId == null || titleId == "")
                UpdateReportTitleStyle(lastReportTitleId, styleValue);
            else
                UpdateReportTitleStyle(titleId, styleValue);
        }
        function EditReportTitle(rptTitleId) {
            var url = "FRptTextFormatMP.aspx?showdelete=1";
            url = url + "&existvalue=" + escape(document.getElementById("hidRptTitle" + rptTitleId).value).replace(/\+/g, "%2b");
            var ret = window.showModalDialog(url, "", "dialogHeight:620px;dialogWidth:420px");
            if (ret != null && ret != undefined && ret != "") {
                if (ret == "delete") {
                    DeleteReportTitle(rptTitleId);
                }
                else {
                    UpdateReportTitleStyle(rptTitleId, ret);
                }
            }
        }
        function UpdateReportTitleStyle(rptTitleId, styleValue) {
            document.getElementById("hidRptTitle" + rptTitleId).value = styleValue;
            var span0 = document.getElementById("spanRptTitle" + rptTitleId);
            var lnk = document.getElementById("lnkRptTitle" + rptTitleId);
            var styleList = new Array();
            styleList = styleValue.split(";");
            lnk.style.fontFamily = styleList[0];
            if (styleList[1] != "")
                lnk.style.fontSize = styleList[1];
            if (styleList[2] == "true")
                lnk.style.fontWeight = "bold";
            else
                lnk.style.fontWeight = "normal";
            if (styleList[3] == "true")
                lnk.style.fontStyle = "italic";
            else
                lnk.style.fontStyle = "normal";
            if (styleList[4] == "true")
                lnk.style.textDecoration = "underline";
            else
                lnk.style.textDecoration = "none";
            if (styleList[5] != "")
                lnk.style.color = styleList[5];
            if (styleList[6] != "")
                lnk.parentNode.style.backgroundColor = styleList[6];
            if (styleList[7].indexOf("center") >= 0)
                lnk.parentNode.parentNode.style.textAlign = "center";
            else if (styleList[7].indexOf("right") >= 0)
                lnk.parentNode.parentNode.style.textAlign = "right";
            else
                lnk.parentNode.parentNode.style.textAlign = "left";
            if (styleList[8].indexOf("top") >= 0)
                lnk.parentNode.parentNode.style.verticalAlign = "top";
            else if (styleList[8].indexOf("bottom") >= 0)
                lnk.parentNode.parentNode.style.verticalAlign = "bottom";
            else
                lnk.parentNode.parentNode.style.verticalAlign = "middle";
            if (styleList[9] != "")
                lnk.parentNode.style.width = styleList[9] + "cm";
            if (styleList[10] == "true")
                lnk.parentNode.style.border = "solid 1px #000000";
            else
                lnk.parentNode.style.border = "";
            lnk.innerText = styleList[12];
        }
        function GenerateReportTitleLabelHtml(titleId) {
            var rndId;
            if (titleId == undefined || titleId == null || titleId == "") {
                lastReportTitleId = lastReportTitleId + 1;
                rndId = lastReportTitleId;
            }
            else {
                rndId = titleId;
            }
            var label = "";
            label = label + "<input type=hidden id='hidRptTitle" + rndId + "' name='hidRptTitle" + rndId + "' ReportTitleId='" + rndId + "' IsReportTitle='1' FormatType='ReportTitle'/>";
            label = label + "<span id='spanRptTitle" + rndId + "'>";
            label = label + "<a href='#' onclick=\"EditReportTitle('" + rndId + "');return false;\" id='lnkRptTitle" + rndId + "'></a>";
            label = label + "</span>";
            return label;
        }
        function DeleteReportTitle(titleId) {
            var hid = document.getElementById("hidRptTitle" + titleId);
            var t = document.getElementById("tbReportTitle");
            t.childNodes[0].removeChild(hid.parentNode.parentNode);
        }
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" id="tableMain" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblDesignStep5Title" runat="server" CssClass="labeltopic">报表设计 5/5 －－ 设置显示格式</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReportName" runat="server">报表名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReportName" CssClass="textbox" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label runat="server" ID="lblDefinedStyle">应用预设样式</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList runat="server" ID="drpDefinedStyle" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldValue">
                            <input class="submitImgButton" id="cmdOK" type="submit" value="确 定" name="cmdOK"
                                runat="server">
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid" valign="top">
                <table width="100%">
                    <tr>
                        <td height="100%">
                            <table id="tbReportTitle" runat="server" border="0">
                                <tr>
                                    <td>
                                        <a href="#" onclick="AddReportTitle();return false;">
                                            <asp:Label ID="lblAddText" runat="server">添加文本</asp:Label></a>
                                    </td>
                                </tr>
                            </table>
                            <table runat="server" id="tbFormat" style="border-collapse: collapse;">
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <asp:HyperLink runat="server" ID="hlnkChartDesign" NavigateUrl="#" onclick="OpenChartDesignWin();return false;"
                    Target="_blank" Text="图表设计"></asp:HyperLink>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdBack" type="submit" value="上一步" name="cmdBack"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdPreview" type="submit" value="预 览" name="cmdPreview"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdFinish" type="submit" value="完 成" name="cmdFinish"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdPublish" type="submit" value="发 布" name="cmdPublish"
                                runat="server">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script language="javascript">
        UpdateLinkStyleOnLoad();
        SetReportTitleWidth();
    </script>
    </form>
</body>
</html>
