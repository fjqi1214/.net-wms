<%@ Page Language="c#" CodeBehind="FRptDesignStep4FilterUISP.aspx.cs" EnableEventValidation="true"
    AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.ReportView.FRptDesignStep4FilterUISP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FilterUIType</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <base target="_self"></base>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script language="javascript">
        $(function () {
            HideShowPanel();
        })
        function AdjustHeight() {
            //            if (/msie/.test(navigator.userAgent.toLowerCase())) {
            //                $("#tableMain").height($(window).height());
            //            }
            //            if (/chrome/.test(navigator.userAgent.toLowerCase())) {
            //                $("#tableMain").height($(window).height());
            //            }
            $("#tableMain").height($(window).height());

        }
        function ReturnAndClose() {
            var sel = document.getElementById("drpReportFilterType");
            var stype = sel.options[sel.selectedIndex].value;

            var str = "";
            if (stype == "reportfilteruitype_checkbox" || stype == "reportfilteruitype_date" || stype == "reportfilteruitype_inputtext") {
                str = stype;
            }
            else if (stype == "reportfilteruitype_selectquery") {
                sel = document.getElementById("drpSelectQueryType");
                str = stype + ";" + sel.options[sel.selectedIndex].value;
            }
            else if (stype == "reportfilteruitype_dropdownlist") {
                sel = document.getElementById("drpDDLDataSource");
                str = stype + ";" + sel.options[sel.selectedIndex].value + ";";
                if (sel.options[sel.selectedIndex].value == "static") {
                    var grid = $find('gridWebGrid');
                    for (var i = 0; i < grid.get_rows().get_length(); i++) {
                        var row = grid.get_rows().get_row(i);
                        if (row.get_cell(1).get_value() != null && row.get_cell(1).get_value() != "") {
                            str = str + row.get_cell(1).get_value() + "," + row.get_cell(2).get_value() + ";";
                        }
                    }
                }
                else {
                    sel = document.getElementById("drpDDLDynamicDataSource");
                    if (sel.options[sel.selectedIndex].value == "") {
                        alert("请选择数据源");
                        return;
                    }
                    if (document.getElementById("hidHideDDLDynamicSourceValue").value == "" && document.getElementById("hidHideDDLDynamicSourceText").value == "") {
                        alert("请选择显示栏位和值栏位");
                        return;
                    }
                    str = str + sel.options[sel.selectedIndex].value + ";";
                    str = str + document.getElementById("hidHideDDLDynamicSourceText").value + ";";
                    str = str + document.getElementById("hidHideDDLDynamicSourceValue").value;
                }
            }
            else if (stype == "reportfilteuitype_selectcomplex") {
                sel = document.getElementById("drpDDLDataSource");

                str = stype + ";" + sel.options[sel.selectedIndex].value + ";";

                if (sel.options[sel.selectedIndex].value == "static") {
                    var grid = $find('gridWebGrid');
                    for (var i = 0; i < grid.get_rows().get_length(); i++) {
                        var row = grid.get_rows().get_row(i);
                        if (row.get_cell(1).get_value() != null && row.get_cell(1).get_value() != "") {
                            str = str + row.get_cell(1).get_value() + "," + row.get_cell(2).get_value() + ";";

                        }
                    }
                    str = str + "selectcomplex#";
                }
                else {
                    sel = document.getElementById("drpDDLDynamicDataSource");
                    if (sel.options[sel.selectedIndex].value == "") {
                        alert("请选择数据源");
                        return;
                    }
                    if (document.getElementById("hidHideDDLDynamicSourceValue").value == "" && document.getElementById("hidHideDDLDynamicSourceText").value == "") {
                        alert("请选择显示栏位和值栏位");
                        return;
                    }

                    str = str + sel.options[sel.selectedIndex].value + ";";
                    str = str + document.getElementById("hidHideDDLDynamicSourceText").value + ";";
                    str = str + document.getElementById("hidHideDDLDynamicSourceValue").value;

                    str = str + ";selectcomplex#";
                    str = str + document.getElementById("hidHideDDLDynamicSourceText").value + "#";
                    str = str + document.getElementById("hidHideDDLDynamicSourceValue").value;

                }
            }



            //Modified  2008/11/04
            if (document.all.cbxCheckMust.checked == false) {
                str = str + ";N";
            }
            else {
                str = str + ";Y";
            }

            //for chrome  
            if (window.opener != undefined) {
                window.opener.returnValue = str;
            }
            window.returnValue = str;
            window.close();
        }
        function HideShowPanel() {
            var tb = document.getElementById("tbDataPanel");
            var i;
            for (i = 1; i < tb.childNodes[0].childNodes.length; i++) {
                HideObject(tb.childNodes[0].childNodes[i]);
            }

            var sel = document.getElementById("drpReportFilterType");
            var stype = sel.options[sel.selectedIndex].value;
            if (stype == "reportfilteruitype_selectquery") {
                ShowObject(document.getElementById("trHideSelectQuery"));
                HideObject(document.getElementById("trHideDropDownList"));
                HideObject(document.getElementById("trHideDDLStaticValue"));
                HideObject(document.getElementById("trHideDDLDynamicValue"));
                HideObject(document.getElementById("trHideDDLDynamicValueColumn"));
            }
            else if (stype == "reportfilteruitype_dropdownlist") {
                ShowObject(document.getElementById("trHideDropDownList"));
                HideShowDropDownListPanel();
                HideObject(document.getElementById("trHideSelectQuery"));
            }
            //Add 2008/10/04 其显示同下拉框类似
            else if (stype == "reportfilteuitype_selectcomplex") {
                ShowObject(document.getElementById("trHideDropDownList"));
                HideShowDropDownListPanel();
                HideObject(document.getElementById("trHideSelectQuery"));
            }
            else {
                HideObject(document.getElementById("trHideSelectQuery"));
                HideObject(document.getElementById("trHideDropDownList"));
                HideObject(document.getElementById("trHideDDLStaticValue"));
                HideObject(document.getElementById("trHideDDLDynamicValue"));
                HideObject(document.getElementById("trHideDDLDynamicValueColumn"));
            }
        }
        function HideShowDropDownListPanel() {
            var sel = document.getElementById("drpDDLDataSource");
            var stype = sel.options[sel.selectedIndex].value;
            if (stype == "static") {
                ShowObject(document.getElementById("trHideDDLStaticValue"));
                HideObject(document.getElementById("trHideDDLDynamicValue"));
                HideObject(document.getElementById("trHideDDLDynamicValueColumn"));
                contentOtherHeight = 0;
                SetContentOtherHeight($('#gridWebGrid'));
                setGridHeight();
            }
            else {
                ShowObject(document.getElementById("trHideDDLDynamicValue"));
                ShowObject(document.getElementById("trHideDDLDynamicValueColumn"));
                HideObject(document.getElementById("trHideDDLStaticValue"));
                contentOtherHeight = 0;
                SetContentOtherHeight($('#gridWebGrid'));
                setGridHeight();
            }
        }
        function ShowObject(obj) {
            obj.style.visibility = "visible";
            obj.style.display = "block";
        }
        function HideObject(obj) {
            obj.style.visibility = "hidden";
            obj.style.display = "none";
        }
        //        function AddGridRow() {
        //            var gridRow = igtbl_addNew("gridWebGrid", 0);
        //            gridRow.getCell(0).setValue("");
        //            gridRow.getCell(1).setValue("");
        //        }
        //        function RemoveGridRow() {
        //            var row = igtbl_getActiveRow("gridWebGrid");
        //            if (row == null) {
        //                return;
        //            }
        //            row.deleteRow();
        //        }
        function SelectColumnToText(txtObj, valObj) {
            var sel = document.getElementById("listHideDDLDynamicSourceColumnList");
            for (var i = 0; i < sel.options.length; i++) {
                if (sel.options[i].selected == true) {
                    document.getElementById(txtObj).value = sel.options[i].text;
                    document.getElementById(valObj).value = sel.options[i].value;
                    return;
                }
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table width="100%" id="tableMain" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblFilterUITypeTitle" runat="server" CssClass="labeltopic">报表设计 4/5 －－ 过滤栏位输入方式</asp:Label>
            </td>
        </tr>
        <tr height="40px">
            <td>
                <table class="query" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReportName" runat="server">报表名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReportName" CssClass="textbox" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblFilterColumnName" runat="server">过滤栏位名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtFilterColumnName" CssClass="textbox" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="10px">
            <td>
                <table width="100%" id="tbDataPanel" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label runat="server" ID="lblReportFilterType">选择类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList runat="server" ID="drpReportFilterType" Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:CheckBox runat="server" ID="cbxCheckMust" Text="必选"></asp:CheckBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="10px">
            <td>
                <table width="100%" id="Table1" cellpadding="0" cellspacing="0">
                    <tr id="trHideSelectQuery" style="visibility: hidden; display: none">
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label runat="server" ID="lblSelectQueryType">数据选择类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList runat="server" ID="drpSelectQueryType" Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td nowrap width="100%" colspan="2">
                        </td>
                    </tr>
                    <tr id="trHideDropDownList" style="visibility: hidden; display: none">
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label runat="server" ID="lblDDLDataSource">数据来源</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList runat="server" ID="drpDDLDataSource" Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td nowrap width="100%" colspan="2">
                        </td>
                    </tr>
                    <tr id="trHideDDLDynamicValue" style="visibility: hidden; display: none">
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label runat="server" ID="lblDDLDynamicDataSource">数据源</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList runat="server" AutoPostBack="true" ID="drpDDLDynamicDataSource"
                                Width="160px">
                            </asp:DropDownList>
                        </td>
                        <td nowrap width="100%" colspan="2">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tbGrid" width="100%" cellpadding="0" cellspacing="0">
                    <tr id="trHideDDLStaticValue" style="visibility: hidden; display: none">
                        <td colspan="3">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="fieldGrid">
                                        <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="510px" DefaultColumnWidth="218px">
                                        </ig:WebDataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="toolbar">
                                        <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                            runat="server" />
                                        <input class="submitImgButton" id="cmdDeleteItem" type="submit" value="删 除" name="cmdDeleteItem"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trHideDDLDynamicValueColumn" style="visibility: hidden; display: none">
                        <td colspan="3">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:ListBox runat="server" ID="listHideDDLDynamicSourceColumnList" Width="150px"
                                            Height="200px"></asp:ListBox>
                                    </td>
                                    <td valign="middle">
                                        <table>
                                            <tr>
                                                <td>
                                                    <input type="button" value="ProjectValue>>" runat="server" id="lblProjectValue" style="border: Solid 0px #000000"
                                                        onclick="SelectColumnToText('txtHideDDLDynamicSourceText','hidHideDDLDynamicSourceText');" /><br />
                                                    <input type="text" value="" class="textbox" id="txtHideDDLDynamicSourceText" style="width: 100px"
                                                        runat="server" /><input type="hidden" style="width: 0px" id="hidHideDDLDynamicSourceText"
                                                            runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="button" value="ProjectDesc>>" runat="server" id="lblProjectDesc" style="border: Solid 0px #000000"
                                                        onclick="SelectColumnToText('txtHideDDLDynamicSourceValue','hidHideDDLDynamicSourceValue');" /><br />
                                                    <input type="text" value="" class="textbox" id="txtHideDDLDynamicSourceValue" style="width: 100px"
                                                        runat="server" /><input type="hidden" style="width: 0px" id="hidHideDDLDynamicSourceValue"
                                                            runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal" cellpadding="0" cellspacing="0">
            <td>
                <table class="edit" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar" id="toolBar" cellpadding="0" cellspacing="0">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onclick="ReturnAndClose();return false;" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onclick="window.close();return false;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
