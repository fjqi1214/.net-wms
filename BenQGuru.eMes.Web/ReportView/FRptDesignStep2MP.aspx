<%@ Page Language="c#" CodeBehind="FRptDesignStep2MP.aspx.cs" EnableEventValidation="false"
    AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.ReportView.FRptDesignStep2MP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>报表设计 2/5</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script language="javascript">
        $(function () {
            $("#tableMain").height($(window).height());
            $("#lstUnSelectedColumn").height($(window).height() - 150);
            $("#lstSelectedColumn").height($(window).height() - 150);
        })
    </script>
    <script language="javascript">
        function MoveUserGroup(fromSelectId, toSelectId) {
            var i;
            var fromsel = document.getElementById(fromSelectId);
            var tosel = document.getElementById(toSelectId);
            for (i = 0; i < fromsel.options.length; i++) {
                if (fromsel.options[i].selected == true) {
                    var opt = new Option(fromsel.options[i].text, fromsel.options[i].value);
                    tosel.options.add(opt);
                }
            }
            for (i = fromsel.options.length - 1; i >= 0; i--) {
                if (fromsel.options[i].selected == true) {
                    fromsel.options.remove(i);
                }
            }
            var str = "";
            var selVal;
            if (fromSelectId.indexOf("lstSelectedColumn") >= 0) {
                selVal = fromsel;
            }
            else {
                selVal = tosel;
            }
            var strHid = selVal.id.replace("lstSelectedColumn", "hidSelectedValue");
            UpdateHiddenValue(selVal, strHid);
        }
        function UpdateHiddenValue(sel, hid) {
            var str = "";
            for (i = 0; i < sel.options.length; i++) {
                str = str + sel.options[i].value + ";";
            }
            document.getElementById(hid).value = str;
        }
        function SetColumnUp() {
            var sel = document.getElementById("lstSelectedColumn");
            for (var i = 1; i < sel.options.length; i++) {
                if (sel.options[i].selected == true) {
                    SwapListColumn(sel, i, i - 1);
                    sel.selectedIndex = i - 1;
                    break;
                }
            }
            UpdateHiddenValue(sel, "hidSelectedValue");
        }
        function SetColumnDown() {
            var sel = document.getElementById("lstSelectedColumn");
            for (var i = 0; i < sel.options.length - 1; i++) {
                if (sel.options[i].selected == true) {
                    SwapListColumn(sel, i, i + 1);
                    sel.selectedIndex = i + 1;
                    break;
                }
            }
            UpdateHiddenValue(sel, "hidSelectedValue");
        }
        function SwapListColumn(sel, idx1, idx2) {
            var val = sel.options[idx1].value;
            var text = sel.options[idx1].text;
            sel.options[idx1].value = sel.options[idx2].value;
            sel.options[idx1].text = sel.options[idx2].text;
            sel.options[idx2].value = val;
            sel.options[idx2].text = text;
        }
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" id="tableMain" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblDesignStep2Title" runat="server" CssClass="labeltopic">报表设计 2/5 －－ 选择显示栏位</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td nowrap width="100%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid" valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblUnSelectedColumn">待选择栏位</asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblSelectedColumn">已选择栏位</asp:Label>
                        </td>
                    </tr>
                    <tr height="100%">
                        <td height="100%">
                            <asp:ListBox runat="server" ID="lstUnSelectedColumn" SelectionMode="multiple" Height="100%"
                                Width="200px"></asp:ListBox>
                        </td>
                        <td valign="middle">
                            <img src="../Skin/Image/right_0.gif" runat="server" id="imgSelect" border="0" />
                            <br />
                            <br />
                            <img src="../Skin/Image/left_0.gif" runat="server" id="imgUnSelect" border="0" />
                        </td>
                        <td height="100%">
                            <asp:ListBox runat="server" ID="lstSelectedColumn" SelectionMode="multiple" Height="100%"
                                Width="200px"></asp:ListBox>
                            <input type="hidden" id="hidSelectedValue" runat="server" />
                        </td>
                        <td valign="middle">
                            <img src="../Skin/Image/up_0.gif" border="0" onclick="SetColumnUp()" />
                            <br />
                            <br />
                            <img src="../Skin/Image/down_0.gif" border="0" onclick="SetColumnDown()" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;<input type="hidden" runat="server" id="hidGroupTotal" />
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
                            <input class="submitImgButton" id="cmdNext" type="submit" value="下一步" name="cmdNext"
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
    </form>
</body>
</html>
