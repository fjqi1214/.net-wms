<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FNewReportDutyAnalysisQP.aspx.cs"
    Inherits="BenQGuru.Web.ReportCenter.FNewReportDutyAnalysisQP" %>

<%@ Register Src="UserControls/UCGroupConditions.ascx" TagName="UCGroupConditions"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/UCWhereConditions.ascx" TagName="UCWhereConditions"
    TagPrefix="uc2" %>
<%@ Register Src="UserControls/UCDisplayConditions.ascx" TagName="UCDisplayConditions"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/UCQueryDataType.ascx" TagName="UCQueryDataType" TagPrefix="uc4" %>
<%@ Register Src="UserControls/UCPieChartProcess.ascx" TagName="UCPieChartProcess"
    TagPrefix="uc5" %>
<%@ Register Src="UserControls/UCColumnChartProcess.ascx" TagName="UCColumnChartProcess"
    TagPrefix="uc6" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="<%=StyleSheet%>" rel="stylesheet">
<head runat="server">
    <title>FNewReportDutyAnalysisQP</title>
</head>
<body scroll="yes" ms_positioning="GridLayout" onload="if (typeof(scrollToBottom)!='undefined' && scrollToBottom) scrollTo(document.body.scrollLeft,document.body.scrollHeight-document.body.clientHeight-60);">
    <form id="form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 产量报表 </asp:Label>
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
                        <td style="text-align: left; width: 400px">
                            <uc3:UCDisplayConditions id="UCDisplayConditions1" runat="server">
                            </uc3:UCDisplayConditions>
                        </td>
                        <td style="text-align: center; width: 300px; display: none">
                            <uc4:UCQueryDataType id="UCQueryDataType1" runat="server">
                            </uc4:UCQueryDataType>
                        </td>
                        <td nowrap style="width: 100px; text-align: right">
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
                <table height="100%" width="100%">
                    <tr height="100%">
                        <td class="fieldGrid">
                            <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                            </ig:webdatagrid>
                        </td>
                    </tr>
                    <%--	<tr>
							<td colspan="8">
								<cc1:OWCPivotTable id="OWCPivotTable1" runat="server"></cc1:OWCPivotTable></td>
						</tr>
						<tr>
							<td colspan="8" style="text-align:center;">
								<cc1:OWCChartSpace id="OWCChartSpace1" runat="server"></cc1:OWCChartSpace>
							</td>
						</tr>--%>
                    <tr>
                        <td colspan="8" align="center">
                            <uc6:uccolumnchartprocess id="columnChart" runat="server">
                            </uc6:uccolumnchartprocess>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" align="center">
                            <uc5:UCPieChartProcess ID="pieChart" runat="server">
                            </uc5:UCPieChartProcess>
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
