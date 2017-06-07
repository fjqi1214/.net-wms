<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<%@ Page Language="c#" CodeBehind="FRptStyleDetailMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.ReportView.FRptStyleDetailMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FRptStyleDetailMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script language="javascript">
        $(function () {
            $("#tableMain").height($(window).height());
        })

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
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" id="tableMain" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">报表样式维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStyleNameQuery" runat="server">样式名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtStyleNameQuery" CssClass="textbox" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" visible="false">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid" valign="top">
                <table id="tbFormat" style="border-collapse: collapse;">
                    <tr>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidHeaderRow" type="hidden" id="hidHeaderRow" formattype="headerrow"
                                runat="server" /><a id="linkHeaderRow" onclick="OpenFormatWin('FRptTextFormatMP.aspx?type=headerrow','hidHeaderRow','linkHeaderRow');return false;"
                                    href="#" style="color: Blue"><asp:Label ID="lblTableHeadFormat" runat="server">表头格式</asp:Label></a>
                        </td>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidHeader_0" type="hidden" id="hidHeader_0" formattype="header" columnname="0"
                                runat="server" /><span id="linkHeader_0"><asp:Label ID="lblColumnHead1" runat="server">栏位标题1</asp:Label></span>
                        </td>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidHeader_1" type="hidden" id="hidHeader_1" formattype="header" columnname="1"
                                runat="server" /><span id="linkHeader_1"><asp:Label ID="lblColumnHead2" runat="server">栏位标题2</asp:Label></span>
                        </td>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidHeader_2" type="hidden" id="hidHeader_2" formattype="header" columnname="2"
                                runat="server" /><span id="linkHeader_2"><asp:Label ID="lblColumnHead3" runat="server">栏位标题3</asp:Label></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidGroup_0" type="hidden" id="hidGroup_0" formattype="group" groupseq="0"
                                runat="server" /><a id="linkGroup_0" onclick="OpenFormatWin('FRptTextFormatMP.aspx?type=group&amp;group=0','hidGroup_0','linkGroup_0');return false;"
                                    href="#" style="color: Blue"><asp:Label ID="lblGroupFormat" runat="server">分组格式</asp:Label></a>
                        </td>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidGroupData_0_0" type="hidden" id="hidGroupData_0_0" formattype="groupdata"
                                groupseq="0" columnname="0" runat="server" /><a id="linkGroupData_0_0" onclick="OpenFormatWin('FRptTextFormatMP.aspx?type=groupdata&amp;group=0&amp;column=0','hidGroupData_0_0','linkGroupData_0_0');return false;"
                                    href="#" style="color: Blue"><asp:Label ID="lblGroupColumn" runat="server">分组栏位</asp:Label></a>
                        </td>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidGroupData_0_1" type="hidden" id="hidGroupData_0_1" formattype="groupdata"
                                groupseq="0" columnname="1" runat="server" /><a id="linkGroupData_0_1" onclick="OpenFormatWin('FRptTextFormatMP.aspx?type=groupdata&amp;group=0&amp;column=1','hidGroupData_0_1','linkGroupData_0_1');return false;"
                                    href="#" style="color: Blue"><asp:Label ID="lblUnSumColumn" runat="server">非汇总栏位</asp:Label></a>
                        </td>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidGroupData_0_2" type="hidden" id="hidGroupData_0_2" formattype="groupdata"
                                groupseq="0" columnname="2" runat="server" /><a id="linkGroupData_0_2" onclick="OpenFormatWin('FRptTextFormatMP.aspx?type=groupdata&amp;group=0&amp;column=2','hidGroupData_0_2','linkGroupData_0_2');return false;"
                                    href="#" style="color: Blue"><asp:Label ID="lblSumColumn" runat="server">汇总栏位</asp:Label></a>
                        </td>
                    </tr>
                    <tr>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidItemHeader" type="hidden" id="hidItemHeader" formattype="item" runat="server" /><a
                                id="linkItemHeader" onclick="OpenFormatWin('FRptTextFormatMP.aspx?type=item','hidItemHeader','linkItemHeader');return false;"
                                href="#" style="color: Blue"><asp:Label ID="lblDetailFormat" runat="server">明细格式</asp:Label></a>
                        </td>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidItemData_1" type="hidden" id="hidItemData_0" formattype="itemdata"
                                columnname="0" runat="server" /><span id="linkItemData_0"></span>
                        </td>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidItemData_1" type="hidden" id="hidItemData_1" formattype="itemdata"
                                columnname="1" runat="server" /><span id="linkItemData_1"><asp:Label ID="lblDetailColumn2"
                                    runat="server">明细栏位2</asp:Label></span>
                        </td>
                        <td style="border: solid 1px #000000; width: 2.5cm;">
                            <input name="hidItemData_2" type="hidden" id="hidItemData_2" formattype="itemdata"
                                columnname="2" runat="server" /><span id="linkItemData_2"><asp:Label ID="lblDetailColumn3"
                                    runat="server">明细栏位3</asp:Label></span>
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
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server"/>
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script language="javascript">
        UpdateLinkStyleOnLoad();
    </script>
    </form>
</body>
</html>
