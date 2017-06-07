<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FAlertNoticeMP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.Alert.FAlertNoticeMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc3" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>预警信息处理</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table style="height: 100%; width: 100%;" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">预警信息处理</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" style="height: 100%; width: 100%;">
                    <tr>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblAlertItemTypeQuery" runat="server">预警项目</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="ddlAlertTypeQuery" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblStatusQuery" runat="server">状态</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="ddlNoticeStatus" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 40%;" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblAlertStartDate" runat="server">预警日期起</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                         <asp:TextBox type="text" id="datStartDateWhere"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblAlertEndDate" runat="server">预警日期止</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                         <asp:TextBox type="text" id="datEndDateWhere"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td style="width: 40%;">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 100%;">
            <td class="fieldGrid">
                <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table cellpadding="0" style="height: 100%; width: 100%;">
                    <tr>
                       
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" onserverclick="cmdGridExport_ServerClick" />
                            
                        </td>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server" pagesize="20">
                            </cc1:pagertoolbar>
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
                            <input class="submitImgButton" id="cmdQuickDeal" type="submit" value="快速处置" name="cmdQuickDeal"
                                runat="server" onserverclick="cmdQuickDeal_ServerClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
