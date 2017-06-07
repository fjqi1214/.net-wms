<%@ Page Language="c#" CodeBehind="FRealTimeInputOutputQTY.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FRealTimeInputOutputQTY" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FRealTimeInputOutputQTY</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">实时工单投入产出查询</asp:Label>
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
                            <asp:DropDownList ID="drpSegmentQuery" runat="server" Width="150px" CssClass="require"
                                AutoPostBack="True" OnSelectedIndexChanged="drpSegmentQuery_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDate" runat="server">日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                        <asp:TextBox  id="eMESDate1"  class='datepicker require' runat="server"  Width="150px"/>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblShip" runat="server">班次</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpShiftQuery" runat="server" Width="150px" CssClass="require">
                            </asp:DropDownList>
                        </td>
                        <td width="100%">
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
                        <td colspan="2">
                            <asp:CheckBox ID="chbRefreshAuto" runat="server" AutoPostBack="True" Text="定时刷新"
                                OnCheckedChanged="chkRefreshAuto_CheckedChanged"></asp:CheckBox>
                        </td>
                        <td>
                            <cc1:RefreshController id="RefreshController1" runat="server" Interval="2000">
                            </cc1:RefreshController>
                            <asp:Label ID="lblToday" runat="server" Visible="False">今天</asp:Label>
                        </td>
                        <td>
                        </td>
                        <td align="right">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick">
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
    </table>
    </form>
</body>
</html>
