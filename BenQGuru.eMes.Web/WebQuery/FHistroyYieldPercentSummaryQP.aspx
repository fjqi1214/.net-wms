<%@ Page Language="c#" CodeBehind="FHistroyYieldPercentSummaryQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FHistroyYieldPercentSummaryQP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
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
    <title>FHistroyYieldPercentSummaryQP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function judgePPM() {
            if (document.all.rblSummaryTarget_3.checked || document.all.rblSummaryTarget_4.checked || document.all.rblSummaryTarget_5.checked || document.all.rblSummaryTarget_6.checked) {
                document.all.tdppm.style.display = "block";
                if (document.all.rblSummaryTarget_5.checked && document.all.rblYieldCatalog_2.checked) {
                    if (!document.all.rblYieldCatalog_2.checked)
                        document.all.tdppm.style.display = "none";
                }
            }
            else {
                document.all.tdppm.style.display = "none";
            }
        }
        function OnInit() {
            judgePPM();
        }
			
    </script>
</head>
<body scroll="yes" onload="OnInit()" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">历史良率统计 </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblModelCodeQuery" runat="server">产品别代码</asp:Label>
                        </td>
                        <td class="fieldValue1">
                            <cc2:selectabletextbox id="txtCondition" runat="server" Type="model" Width="100px">
                            </cc2:selectabletextbox>
                        </td>
                        <td id="tdppm" style="display: none" colspan="6">
                            <table>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblModelQuery" runat="server">产品别代码</asp:Label>
                                    </td>
                                    <td class="fieldValue1">
                                        <cc2:selectabletextbox id="txtModelQuery" runat="server" Type="model" Width="100px">
                                        </cc2:selectabletextbox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblItemQuery" runat="server">产品代码</asp:Label>
                                    </td>
                                    <td class="fieldValue1">
                                        <cc2:selectabletextbox id="txtItemQuery" runat="server" Type="item" Width="100px">
                                        </cc2:selectabletextbox>
                                    </td>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblMoQuery" runat="server">工单代码</asp:Label>
                                    </td>
                                    <td class="fieldValue1">
                                        <cc2:selectabletextbox id="txtMoQuery" runat="server" Type="mo" Width="100px">
                                        </cc2:selectabletextbox>
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
                        <asp:TextBox  id="dateStartDateQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEndDateQuery" runat="server">结束日期</asp:Label>
                        </td>
                        <td class="fieldValue1">
                        <asp:TextBox  id="dateEndDateQuery"  class='datepicker' runat="server"  Width="130px"/>
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
                        <td colspan="7">
                            <asp:RadioButtonList ID="rblSummaryTarget" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rblSummaryTarget_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblYieldTypeQuery" runat="server">良率类型</asp:Label>
                        </td>
                        <td colspan="7">
                            <asp:RadioButtonList ID="rblYieldCatalog" runat="server" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblVisibleStyle" runat="server">显示样式</asp:Label>
                        </td>
                        <td colspan="7">
                            <asp:RadioButtonList ID="rblVisibleStyle" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rblVisibleStyle_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblChartType" runat="server">图形类型</asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:RadioButtonList ID="rblChartType" runat="server" RepeatDirection="Horizontal"
                                Enabled="False">
                            </asp:RadioButtonList>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick">
                        </td>
                        <td width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td>
                <table height="100%" width="100%">
                    <tr height="100%">
                        <td class="fieldGrid">
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
