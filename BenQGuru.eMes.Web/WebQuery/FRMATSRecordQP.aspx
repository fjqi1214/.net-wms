<%@ Page Language="c#" CodeBehind="FRMATSRecordQP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WebQuery.FRMATSRecordQP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="~/UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
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
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">RMA维修记录查询</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblModelCodeQuery" runat="server">产品别代码</asp:Label>
                        </td>
                        <td class="fieldValue" width="100">
                            <cc2:selectabletextbox id="txtConditionModel" runat="server" Type="model" Width="120">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue" width="100">
                            <cc2:selectabletextbox id="txtConditionItem" runat="server" Type="item" Width="120">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblConditionMoQuery" runat="server">RMA返工工单</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtReworkMo" runat="server" Type="remo" Width="120">
                            </cc2:selectabletextbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReceiveBeginDate" runat="server">接收起始日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table>
                                <tr>
                                    <td>
                                     <asp:TextBox  id="txtReceiveBeginDate"  class='datepicker' runat="server"  Width="70"/>
                                    </td>
                                    <td>
                                        <uc1:emestime id="txtReceiveBeginTime" runat="server" width="60">
                                        </uc1:emestime>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReceiveEndDate" runat="server">接收结束日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox  id="txtReceiveEndDate"  class='datepicker' runat="server"  Width="70"/>
                                    </td>
                                    <td>
                                        <uc1:emestime id="txtReceiveEndTime" runat="server" width="60">
                                        </uc1:emestime>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblRMABillCode" runat="server">RMA单号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtRMABillCode" runat="server" Type="rmabill" Width="120">
                            </cc2:selectabletextbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblTSStateQuery" runat="server">维修状态</asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:CheckBoxList ID="chkTSStateList" runat="server" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
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
                        <td width="140">
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td>
                            <asp:CheckBox ID="chbRepairDetail" runat="server" Text="导出维修明细" Visible="False">
                            </asp:CheckBox>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
