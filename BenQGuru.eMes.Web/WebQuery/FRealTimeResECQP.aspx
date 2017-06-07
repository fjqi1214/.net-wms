<%@ Page Language="c#" CodeBehind="FRealTimeResECQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FRealTimeResECQP" %>

<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport"
    Assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FRealTimeResECQP</title>
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
                <asp:Label ID="lblRealTimeNGTitle" runat="server" CssClass="labeltopic">实时工位不良统计</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft">
                            <asp:Label ID="lblRes" runat="server">资源</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtResCode" runat="server" Width="150px" Type="resource">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDate" runat="server">日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox  id="eMESDate1"  class='datepicker require' runat="server"  Width="150px"/>
                        </td>
                        <td class="fieldName">
                            <asp:Label ID="lblShip" runat="server">班次</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpShiftQuery" runat="server" Width="150px" CssClass="require"
                                OnLoad="drpShiftQuery_Load">
                            </asp:DropDownList>
                        </td>
                        <td width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft">
                            <asp:Label ID="lblStepSequence" runat="server">生产线</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtStepSequence" runat="server" Width="150px" Type="stepsequence">
                            </cc2:selectabletextbox>
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
                        <td width="100%">
                        </td>
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
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
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
        <tr>
            <td class="smallImgButton">
                <input class="gridExportButton" id="cmdGridExport2" type="submit" value="  " name="cmdGridExport"
                    runat="server" onserverclick="cmdGridExport2_ServerClick">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
