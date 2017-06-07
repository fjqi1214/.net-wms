<%@ Page Language="c#" CodeBehind="FHistroyQuantitySummaryQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.Web.ReportCenter.FHistroyQuantitySummaryQP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Src="UserControls/UCDisplayConditions.ascx" TagName="UCDisplayConditions"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/UCLineChartProcess.ascx" TagName="UCLineChartProcess"
    TagPrefix="uc4" %>
<%@ Register Src="UserControls/UCColumnChartProcess.ascx" TagName="UCColumnChartProcess"
    TagPrefix="uc5" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FHistroyQuantitySummaryQP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function judgeSummaryTarget() {
            if (document.all.rblSummaryTarget_3.checked || document.all.rblSummaryTarget_4.checked || document.all.rblSummaryTarget_5.checked || document.all.rblSummaryTarget_6.checked) {
                document.all.tdppm.style.display = "block";
            }
            else {
                document.all.tdppm.style.display = "none";
            }
        }
        function OnInit() {
            judgeSummaryTarget();
        }
        function Grid_Initialize(sender, eventArgs) {

            var colName = document.getElementById('txtColumn').value; //此处要改为需要合并的列的key
            var textTag;
            var indexTag = 0;
            var spanIndex = 0;
            for (var i = 0; i < sender.get_rows().get_length(); i++) {

                var currentCell = sender.get_rows().get_row(i).get_cellByColumnKey(colName);
                if (currentCell == null) {
                    continue;
                }
                if (textTag == currentCell.get_value()) {
                    sender.get_rows().get_row(indexTag).get_cellByColumnKey(colName).get_element().rowSpan = i - indexTag + 1;
                    currentCell.get_element().style.display = 'none';
                }
                else {
                    //重置背景颜色
                    if (spanIndex % 2 == 1) {
                        sender.get_rows().get_row(indexTag).get_cellByColumnKey(colName).get_element().style.backgroundColor = 'white';
                    }
                    else {
                        sender.get_rows().get_row(indexTag).get_cellByColumnKey(colName).get_element().style.backgroundColor = 'rgb(235, 246, 255)';
                    }

                    indexTag = i;
                    textTag = currentCell.get_value();
                    spanIndex++;
                }
            }
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 416px;
        }
    </style>
</head>
<body scroll="yes" ms_positioning="GridLayout" onload="OnInit()">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server" >
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">历史产量统计 </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblModelCodeQuery" runat="server">产品别代码</asp:Label>
                        </td>
                        <td class="fieldValue1">
                            <cc2:selectabletextbox id="txtCondition" runat="server" Type="model">
                            </cc2:selectabletextbox>
                        </td>
                        <td id="tdppm" style="display: none" colspan="6">
                            <table>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblModelQuery" runat="server">产品别代码</asp:Label>
                                    </td>
                                    <td class="fieldValue1">
                                        <cc2:selectabletextbox id="txtModelQuery" runat="server" Type="model">
                                        </cc2:selectabletextbox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblItemQuery" runat="server">产品代码</asp:Label>
                                    </td>
                                    <td class="fieldValue1">
                                        <cc2:selectabletextbox id="txtItemQuery" runat="server" Type="item">
                                        </cc2:selectabletextbox>
                                    </td>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblMoQuery" runat="server">工单代码</asp:Label>
                                    </td>
                                    <td class="fieldValue1">
                                        <cc2:selectabletextbox id="txtMoQuery" runat="server" Type="mo">
                                        </cc2:selectabletextbox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtColumn" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStartDateQuery" runat="server">起始日期</asp:Label>
                        </td>
                        <td class="fieldValue1">
                            <uc1:emesdate id="dateStartDateQuery" runat="server" cssclass="require" width="130"></uc1:emesdate>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEndDateQuery" runat="server">结束日期</asp:Label>
                        </td>
                        <td class="fieldValue1">
                            <uc1:emesdate id="dateEndDateQuery" runat="server" cssclass="require" width="130"></uc1:emesdate>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTimingType" runat="server">时间类型</asp:Label>
                        </td>
                        <td colspan="7">
                            <asp:RadioButtonList ID="rblTimingType" runat="server" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSummaryTarget" runat="server">统计对象</asp:Label>
                        </td>
                        <td colspan="7" nowrap>
                            <asp:RadioButtonList ID="rblSummaryTarget" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rblSummaryTarget_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="7" nowrap>
                        </td>
                        <td width="100%">
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
                        <td class="fieldGrid" >
                            <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                                <ClientEvents Initialize="Grid_Initialize" />
                            </ig:WebDataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" >
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
                <table cellpadding="0" width="100%">
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
