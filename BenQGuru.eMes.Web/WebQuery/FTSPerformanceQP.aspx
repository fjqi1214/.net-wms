<%@ Page Language="c#" CodeBehind="FTSPerformanceQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FTSPerformanceQP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="uc2" TagName="UpDown" Src="~/UserControl/NumericUpDown/UCNumericUpDown.ascx" %>
<%@ Register Src="UserControls/UCColumnChartProcess.ascx" TagName="UCColumnChartProcess"
    TagPrefix="uc3" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FTSRecordQP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body scroll="yes" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">维修绩效查询</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblModelCodeQuery" runat="server">产品别代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtConditionModel" runat="server" Width="130px" Type="model"
                                CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtConditionItem" runat="server" Width="130px" Type="item"
                                CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtConditionMo" runat="server" Width="130px" Type="mo"
                                CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap style="height: 23px">
                            <asp:Label ID="lblRepaireOperationQuery" runat="server">维修站</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 23px">
                            <cc2:selectabletextbox id="txtConditionResource" runat="server" Width="130px" Type="resource"
                                CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap style="height: 23px">
                            <asp:Label ID="lblOperatorQuery" runat="server">维修工</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 23px">
                            <cc2:selectabletextbox id="txtConditionOperator" runat="server" Width="130px" Type="user"
                                CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td style="height: 23px">
                        </td>
                        <td style="height: 23px">
                        </td>
                        <td style="height: 23px">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblRepairStartDateQuery" runat="server">维修起始日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                        <asp:TextBox  id="dateStartDateQuery"  class='datepicker' runat="server"  Width="110px"/>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEDateQuery" runat="server">结束日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox  id="dateEndDateQuery"  class='datepicker' runat="server"  Width="110px"/>
                        </td>
                        <td>
                        </td>
                        <td width="100%">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSummaryTarget" runat="server">统计对象</asp:Label>
                        </td>
                        <td colspan="6">
                            <asp:RadioButtonList ID="rblSummaryTargetQuery" runat="server">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTopQuery" runat="server">排名筛选</asp:Label>
                        </td>
                        <td colspan="4">
                            <uc2:UpDown id="upDown" width="110" runat="server">
                            </uc2:UpDown>
                        </td>
                        <td>
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
        <tr>
            <td>
                <table>
                    <%--<tr>
								<td colspan="8">
									<cc1:OWCChartSpace id="OWCChartSpace1" runat="server"></cc1:OWCChartSpace>
								</td>
							</tr>--%>
                    <tr class="normal">
                        <td align="center">
                            <uc3:UCColumnChartProcess ID="UCColumnChartProcess1" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
