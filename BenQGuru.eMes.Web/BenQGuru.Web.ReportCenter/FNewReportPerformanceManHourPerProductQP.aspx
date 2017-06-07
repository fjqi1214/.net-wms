<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FNewReportPerformanceManHourPerProductQP.aspx.cs"
    Inherits="BenQGuru.Web.ReportCenter.FNewReportPerformanceManHourPerProductQP" %>

<%@ Register Src="UserControls/UCGroupConditions.ascx" TagName="UCGroupConditions"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/UCWhereConditions.ascx" TagName="UCWhereConditions"
    TagPrefix="uc2" %>
<%@ Register Src="UserControls/UCDisplayConditions.ascx" TagName="UCDisplayConditions"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/UCLineChartProcess.ascx" TagName="UCLineChartProcess"
    TagPrefix="uc4" %>
<%@ Register Src="UserControls/UCColumnChartProcess.ascx" TagName="UCColumnChartProcess"
    TagPrefix="uc5" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="<%=StyleSheet%>" rel="stylesheet">
<head runat="server">
    <title>FNewReportPerformanceManHourPerProductQP</title>
</head>
<body scroll="yes" ms_positioning="GridLayout" onload="if (typeof(scrollToBottom)!='undefined' && scrollToBottom) scrollTo(document.body.scrollLeft,document.body.scrollHeight-document.body.clientHeight-60);">
    <form id="form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 台耗工时 </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td>
                            <uc2:ucwhereconditions id="UCWhereConditions1" runat="server">
                            </uc2:ucwhereconditions>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td>
                            <uc1:ucgroupconditions id="UCGroupConditions1" runat="server">
                            </uc1:ucgroupconditions>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td style="text-align: left; width: 700px">
                            <uc3:UCDisplayConditions id="UCDisplayConditions1" runat="server">
                            </uc3:UCDisplayConditions>
                        </td>
                        <td nowrap style="width: 100px; text-align: center">
                            <asp:CheckBox ID="chbRefreshAuto" runat="server" AutoPostBack="True" Text="定时刷新"
                                OnCheckedChanged="chkRefreshAuto_CheckedChanged"></asp:CheckBox>
                            <cc1:RefreshController id="RefreshController1" runat="server" Interval="2000">
                            </cc1:RefreshController>
                        </td>
                        <td nowrap style="width: 100px; text-align: center">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick">
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="fieldGrid" style="padding: 0px;">
                            <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                            </ig:WebDataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <uc4:uclinechartprocess id="lineChart" runat="server">
                            </uc4:uclinechartprocess>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <uc5:uccolumnchartprocess id="columnChart" runat="server">
                            </uc5:uccolumnchartprocess>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                runat="server" onserverclick="cmdGridExport_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
