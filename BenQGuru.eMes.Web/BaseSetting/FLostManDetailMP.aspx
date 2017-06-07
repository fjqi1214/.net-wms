<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>

<%@ Page Language="c#" CodeBehind="FLostManDetailMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FLostManDetailMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FLostManDetailMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">损失工时分析</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDateQuery" runat="server">日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                         <asp:TextBox type="text" id="DateQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblShiftCodeQuery" runat="server"> 班次代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtShiftCodeQuery" runat="server" ReadOnly="true" Width="150px"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server"> 产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemCodeQuery" runat="server" ReadOnly="true" CssClass="textbox"
                                Width="150px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSSQuery" runat="server"> 产线代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSSQuery" runat="server" ReadOnly="true" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblLostManHourQuery" runat="server"> 损失工时</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtLostManHourQuery" runat="server" ReadOnly="true" Width="150px"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldValue" style="display: none;">
                            <asp:TextBox ID="txtitemCode" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid">
                <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
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
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server">
                        </td>
                        <td>
                            <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar" runat="server"></cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblLostManHourEdit" runat="server"> 损失工时</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtLostManHourEdit" runat="server" Width="130px" CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblExceptionCodeEdit" runat="server"> 异常事件代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:SelectableTextBox ID="txtExceptionCodeEdit" runat="server" CssClass="require"
                                Type="singleexceptionCode" CanKeyIn="True"></cc2:SelectableTextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblRealExceptionCodeEdit" runat="server"> 实际异常事件</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:SelectableTextBox ID="txtRealExceptionCodeEdit" runat="server" CssClass="textbox"
                                Type="singlerealexceptionCode" CanKeyIn="True"></cc2:SelectableTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDutyCodeEdit" runat="server"> 责任别代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <cc2:SelectableTextBox ID="txtDutyCodeEdit" runat="server" CssClass="require" Type="singleduty"
                                CanKeyIn="True"></cc2:SelectableTextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMemoEdit" runat="server" CssClass="textbox" TextMode="MultiLine"
                                Width="200px" Height="60px" MaxLength="500"></asp:TextBox>
                        </td>
                        <td class="fieldValue" style="display: none;">
                            <asp:TextBox ID="txtSeq" runat="server" Width="130px" CssClass="require"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                    runat="server">
                            </td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                    runat="server">
                            </td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                    runat="server">
                            </td>
                            <td>
                                <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                    runat="server" onserverclick="cmdReturn_ServerClick">
                            </td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
