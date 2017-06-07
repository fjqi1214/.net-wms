<%@ Page Language="c#" CodeBehind="FTSInfoListQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FTSInfoListQP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FINNOInfoQP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form2" method="post" runat="server">
    <table height="100%" width="100%" id="Table1" runat="server">
        <tr class="moduleTitle">
            <td style="height: 19px">
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">维修品清单</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%" id="Table2">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSummaryObjectQuery" runat="server">不良代码组</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSummaryObjectQuery" runat="server" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblNGCodeQuery" runat="server" Visible="False">不良代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSummaryObject1Query" runat="server" CssClass="textbox" Visible="False"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td width="100%">
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
                <table height="100%" cellpadding="0" width="100%" id="Table3">
                    <tr>
                        <td width="140">
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                runat="server">
                        </td>
                        <td>
                            <asp:CheckBox ID="chbRepairDetail" runat="server" Text="导出维修明细"></asp:CheckBox>
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
                <table class="toolBar" id="Table4">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                runat="server" onserverclick="cmdReturn_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
