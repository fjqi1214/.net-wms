<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FMOTailMP.aspx.cs" Inherits="BenQGuru.eMES.Web.MOModel.FMOTailMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>MOTailMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function cmdSplit_Click() {
            return window.confirm(document.getElementById("lblSplitlbl").value);
        }

        function cmdScrap_Click() {

            return window.confirm(document.getElementById("lblcraplbl").value);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">工单尾数处理</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOCodeQuery" runat="server">工单号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtMoCodeQuery" ReadOnly="true" runat="server" CssClass="textbox"
                                Width="100px"></asp:TextBox>
                        </td>
                        <td style="display: none">
                            <asp:TextBox ID="lblSplitlbl" runat="server" Width="8px"></asp:TextBox>
                        </td>
                        <td style="display: none">
                            <asp:TextBox ID="lblcraplbl" runat="server" Width="8px"></asp:TextBox>
                        </td>
                        <td width="100%">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
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
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolbar id="pagerToolBar" runat="server">
                            </cc1:PagerToolbar>
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
                            <input language="javascript" class="submitImgButton" id="cmdOffMO" type="submit"
                                value="拆 解" name="cmdOffMO" runat="server" onserverclick="cmdSplit_ServerClick"
                                onclick="return cmdSplit_Click();">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdScrap" type="submit"
                                value="报 废" name="cmdScrap" runat="server" onserverclick="cmdScrap_ServerClick"
                                onclick="return cmdScrap_Click();">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                runat="server" onserverclick="cmdCancel_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
