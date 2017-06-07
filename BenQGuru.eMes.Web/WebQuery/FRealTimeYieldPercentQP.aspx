<%@ Page Language="c#" CodeBehind="FRealTimeYieldPercentQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FRealTimeYieldPercentQP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Src="UserControls/UCColumnLineChartProcess.ascx" TagName="UCColumnLineChartProcess"
    TagPrefix="uc2" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FRealTimeQuantitySummaryQP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function Format() {
            var original = document.all.txtTargetPercent.value;
            var parts = original.split('.');
            if (parts[1] != null) {
                document.all.txtTargetPercent.value = parts[0] + "." + parts[1].substr(0, 2);
            }
        }
    </script>
</head>
<body scroll="yes" ms_positioning="GridLayout">
<form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">实时良率查询</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="query" height="100%" width="100%">
                        <tr>
                            <td class="fieldNameLeft">
                                <asp:Label ID="lblSegment" runat="server">工段</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <cc2:SelectableTextbox id="txtSegmentCodeQuery" runat="server" CanKeyIn="true" Type="singlesegment"
                                    Target="" AutoPostBack="true">
                                </cc2:SelectableTextbox>
                            </td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblDate" runat="server">日期</asp:Label>
                            </td>
                            <td class="fieldValue">
                               <asp:TextBox  id="eMESDate1"  class='datepicker require' runat="server"  Width="130px"/>
                            </td>
                            <td class="fieldName">
                                <asp:Label ID="lblShip" runat="server">班次</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <asp:DropDownList ID="drpShiftQuery" runat="server" CssClass="require" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <cc1:RefreshController id="RefreshController1" runat="server" Interval="2000">
                                </cc1:RefreshController>
                                <asp:Label ID="lblToday" runat="server" Visible="False">今天</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft">
                                <asp:Label ID="lblStepSequence" runat="server">生产线</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <cc2:selectabletextbox4SS id="txtStepSequence" runat="server" Width="150px" Type="stepsequence">
                                </cc2:selectabletextbox4SS>
                            </td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblModelCodeQuery" runat="server">产品别代码</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <cc2:selectabletextbox id="txtModelQuery" runat="server" Width="150px" Type="model">
                                </cc2:selectabletextbox>
                            </td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <cc2:selectabletextbox id="txtItemQuery" runat="server" Width="150px" Type="item">
                                </cc2:selectabletextbox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <cc2:selectabletextbox id="txtMoQuery" runat="server" Width="150px" Type="mo">
                                </cc2:selectabletextbox>
                            </td>
                            <td class="fieldName" nowrap style="display: none">
                                <asp:Label ID="lblFactory" runat="server">工厂</asp:Label>
                            </td>
                            <td class="fieldValue" style="display: none">
                                <asp:DropDownList ID="drpFactory" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblTargetPercent" runat="server">目标良率</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtTargetPercent" runat="server" CssClass="textbox" Width="150px">95</asp:TextBox>%
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:CheckBox ID="chbRefreshAuto" runat="server" AutoPostBack="True" Text="定时刷新"
                                    OnCheckedChanged="chkRefreshAuto_CheckedChanged"></asp:CheckBox>
                            </td>
                            <td>
                                <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                    runat="server" onserverclick="cmdQuery_ServerClick">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<tr height="100%">
					<td vAlign="top"><cc1:owcchartspace id="OWCChartSpace1" runat="server"></cc1:owcchartspace>                       
                    </td>
				</tr>--%>
            <tr height="100%">
                <td valign="top">
                    <uc2:UCColumnLineChartProcess ID="UCColumnLineChartProcess1" runat="server" />
                </td>
            </tr>
            <tr height="100">
                <td class="fieldGrid">
                    <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                    </ig:WebDataGrid>
                </td>
            </tr>
            <tr class="normal" height="100%">
                <td>
                    <table class="edit">
                        <tr>
                            <td class="labeltopic" nowrap>
                                <asp:Label ID="lblSegmentYieldTitle" runat="server">工段良率</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblSegmentQuantity" runat="server">直通台数</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtSegmentAllGoodQuantity" CssClass="textbox" runat="server" Width="150px"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblSegmentAllQuantity" runat="server">总通过台数</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtSegmentQuantity" CssClass="textbox" runat="server" Width="150px"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblSegmentNotYield" runat="server">工段直通率</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtSegmentNotYieldPercent" CssClass="textbox" runat="server" Width="150px"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td width="100%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
