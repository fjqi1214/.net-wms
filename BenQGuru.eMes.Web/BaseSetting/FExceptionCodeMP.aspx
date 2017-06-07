<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>

<%@ Page Language="c#" CodeBehind="FExceptionCodeMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FExceptionCodeMP" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FExceptionCodeMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function OnSelectedDelete() {
            var grid = igtbl_getGridById('gridWebGrid');
            var myDate = new Date();
            var gulid = myDate.getMonth().toString() + myDate.getDate().toString() + myDate.getHours().toString() + myDate.getSeconds().toString();

            for (var i = 0; i < grid.Rows.length; i++) {
                if (grid.Rows.rows[i] != null && grid.Rows.rows[i].getCellFromKey("Check").getValue() == true) {
                    var exceptionCode = encodeURI(grid.Rows.rows[i].getCellFromKey("ExceptionCode").getValue());
                    var result = openPage('<%=Request.ApplicationPath%>/BaseSetting/FDeleteExceptionCodeAndCheckException.ashx?exceptionCode=' + exceptionCode + '&gulID=' + gulid);

                    if (result == "true") {
                        var confirmDialogPath = '<%=Request.ApplicationPath%>/MOModel/ConfirmDialog.htm';
                        // the basic width
                        var rdBasicWidth = "420px";
                        // the basic height
                        var rdBasicHeight = "200px";
                        var _g_privateDialogFeatures = "status=no;center=yes;help=no;dialogWidth=" +
                                                        rdBasicWidth + ";dialogHeight=" +
						                                rdBasicHeight + ";scroll=yes;resize=no";

                        var args = '异常事件中存在该异常代码，是否同时删除异常事件？';
                        var returnValue = window.showModalDialog(confirmDialogPath, args, _g_privateDialogFeatures);

                        if (returnValue == 0) {
                            document.getElementById("txtchecked").value = "NotDelete";

                        }
                    }
                }
            }

            return true;
        }

        function openPage(pageName) {
            var xmlHTTP = new ActiveXObject("Microsoft.XMLHTTP");

            xmlHTTP.open("GET", pageName, false);

            xmlHTTP.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            xmlHTTP.send();

            return xmlHTTP.responseText;
        }

    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">异常事件代码维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblExceptionCodeQuery" runat="server"> 异常事件代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtExceptionCodeQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblExceptionDESCQuery" runat="server"> 异常事件描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtExceptionDESCQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblExceptionTypeQuery" runat="server">异常事件类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpExceptionTypeQuery" runat="server" CssClass="textbox" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblExceptionFlagQuery" runat="server">非生产性损失</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpExceptionFlagQuery" runat="server" CssClass="textbox" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td nowrap>
                        </td>
                        <td nowrap>
                        </td>
                        <td nowrap>
                        </td>
                        <td nowrap>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid">
                <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
                        
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                runat="server">
                            
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete1" type="submit" value="  " name="cmdDelete1"
                                runat="server" onclick=" {return confirm('是否确认删除？')}">
                            <!--if(confirm('是否确认删除？')==true) {return OnSelectedDelete();} else {return false;}-->
                            
                        </td>
                        <td>
                            <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar" runat="server"></cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblExceptionCodeEdit" runat="server"> 异常事件代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtExceptionCodeEdit" runat="server" Width="150px" CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblExceptionNameEdit" runat="server"> 异常事件名称</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtExceptionNameEdit" runat="server" Width="150px" CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblExceptionDESCEdit" runat="server"> 异常事件描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtExceptionDESCEdit" runat="server" Width="150px" CssClass="require"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblExceptionTypeEdit" runat="server">异常事件类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpExceptionTypeEdit" runat="server" CssClass="require" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblExceptionFlagEdit" runat="server">非生产性损失</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpExceptionFlagEdit" runat="server" CssClass="require" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldValue" style="display: none">
                            <asp:TextBox ID="txtchecked" runat="server" Width="150px" CssClass="require"></asp:TextBox>
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
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
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
