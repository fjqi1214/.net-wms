<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FIndexMORelatedQP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WebQuery.FIndexMORelatedQP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Src="UserControls/UCLineChartProcess.ascx" TagName="UCLineChartProcess"
    TagPrefix="uc4" %>
<%@ Register Src="UserControls/UCColumnChartProcess.ascx" TagName="UCColumnChartProcess"
    TagPrefix="uc5" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FIndexMORelatedQP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">工单相关指标</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblStartDateQuery" runat="server" Text="开始日期"></asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox type="text" ID="datStartDateQuery" class='datepicker' runat="server"
                                Width="130px" />
                        </td>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblEndDateQuery" runat="server" Text="结束日期"></asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox type="text" ID="datEndDateQuery" class='datepicker' runat="server" Width="130px" />
                        </td>
                        <td colspan="2" />
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap="nowrap">
                            <asp:Label ID="lblGoodSemiGoodQuery" runat="server" Text="成品/半成品"></asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="ddlGoodSemiGoodQuery" runat="server" Width="131px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblByTimeTypeQuery" runat="server" Text="按时间"></asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 150px">
                            <asp:RadioButtonList ID="rblByTimeTypeQuery" runat="server" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                        <td nowrap width="40%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="title" nowrap>
                <asp:Label ID="lblMOCloseRate" runat="server" Text="工单关单率"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table cellpadding="0" width="100%">
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
        <tr>
            <td>
                <table>
                    <tr>
                        <td align="center">
                            <uc4:uclinechartprocess id="lineChart" runat="server">
                            </uc4:uclinechartprocess>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="title" style="display: none;">
                <asp:Label ID="lblMOPeriodDistribute" runat="server" Text="未关工单时长分布"></asp:Label>
            </td>
        </tr>
        <%--此处删了一个grid，根据后台逻辑应该没用，by kathy @20131030--%>
        <tr>
            <td>
                <table>
                    <tr>
                        <td align="center">
                            <uc5:uccolumnchartprocess id="columnChart" runat="server">
                            </uc5:uccolumnchartprocess>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
