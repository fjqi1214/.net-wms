<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>

<%@ Page Language="c#" CodeBehind="FITOPResultPackingQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FITOPResultPackingQP" %>

<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FITProductionProcessQP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server">产品追溯管理</asp:Label><input class="textbox"
                    id="txtSeq" style="display: none; width: 120px" readonly name="textfield" runat="server">
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" id="Table2" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSNQuery" runat="server">序列号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <input class="textbox" style="width: 165px" name="textfield" id="txtSN" runat="server"
                                readonly>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblModelQuery" runat="server">产品别</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <input class="textbox" style="width: 120px" name="textfield2" id="txtModel" runat="server"
                                readonly>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemQuery" runat="server">产品</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <input class="textbox" style="width: 120px" name="textfield2" id="txtItem" runat="server"
                                readonly>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOQuery" runat="server">工单</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <input class="textbox" style="width: 120px" name="textfield2" id="txtMO" runat="server"
                                readonly>
                        </td>
                        <td nowrap width="100%">
                            &nbsp;
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
        <tr>
            <td>
                <asp:Label ID="lblPackTitle" runat="server" CssClass="labeltopic">包装信息</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid2" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPackingDetailTitle" runat="server" CssClass="labeltopic">包装明细</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid3" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table id="Table3" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="smallImgButton" nowrap>
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
