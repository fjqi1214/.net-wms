<%@ Page Language="c#" CodeBehind="FTimePeriodMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FTimePeriodMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESTime" Src="~/UserControl/DateTime/DateTime/eMESTime.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FTimePeriodMP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 时段维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTimePeriodCodeQuery" runat="server"> 时段代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtTimePeriodCodeQuery" runat="server" CssClass="textbox" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblShiftCodeQuery" runat="server"> 班次代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <cc2:SelectableTextBox ID="txtShiftCodeQuery" runat="server" Type="singleshiftcode"
                                Target="" CanKeyIn="True"></cc2:SelectableTextBox>
                        </td>
                        <td nowrap width="100%">
                            <td class="fieldName">
                                <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                    runat="server">
                            </td>
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
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblTimePeriodCodeEdit" runat="server">时段代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtTimePeriodCodeEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblTimePeriodDescriptionEdit" runat="server">时段描述</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTimePeriodDescriptionEdit" runat="server" CssClass="textbox"
                                Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblTimePeriodTypeEdit" runat="server">时段类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpTimePeriodTypeEdit" runat="server" CssClass="require" Width="130px"
                                OnLoad="drpTimePeriodTypeEdit_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblShiftCEdit" runat="server">所属班次</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpShiftCodeEdit" runat="server" CssClass="require" Width="130px"
                                AutoPostBack="True" OnLoad="drpShiftCodeEdit_Load" OnSelectedIndexChanged="drpShiftCodeEdit_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblStartTimeQuery" runat="server">起始时间</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <uc1:emestime id="timeTimePeriodBeginTimeEdit" runat="server" CssClass="require"
                                width="130">
                            </uc1:emestime>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEndTimeQuery" runat="server">结束时间</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <uc1:emestime id="timeTimePeriodEndTimeEdit" runat="server" CssClass="require" width="130">
                            </uc1:emestime>
                        </td>
                        <td nowrap>
                            <asp:CheckBox ID="chbIsOverDateEdit" runat="server" Text="是否跨日期"></asp:CheckBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td width="100%">
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblShiftTypeCEdit" runat="server">所属班制</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtShiftTypeCodeEdit" runat="server" CssClass="textbox" Width="130px"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblStartTimeEdit" runat="server">起始时间</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtShiftBeginTime" runat="server" CssClass="textbox" Width="130px"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEndTimeEdit" runat="server">结束时间</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtShiftEndTime" runat="server" CssClass="textbox" Width="130px"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblTimePeriodSequenceEdit" runat="server">时段序列</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtTimePeriodSequenceEdit" runat="server" CssClass="require" Width="130px"
                                ReadOnly="True">0</asp:TextBox>
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
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
