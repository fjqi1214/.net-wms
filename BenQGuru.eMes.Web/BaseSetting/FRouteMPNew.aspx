<%@ Page Language="c#" CodeBehind="FRouteMPNew.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FRouteMPNew" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FRouteMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script>


    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">

    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">生产途程维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblRouteCodeQuery" runat="server">生产途程代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtRouteCodeQuery" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
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
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblPRouteCodeEdit" runat="server">生产途程代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtRouteCodeEdit" CssClass="require" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblRouteDescriptionEdit" runat="server">生产途程描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtRouteDescriptionEdit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="display: none" nowrap>
                            <asp:Label ID="lblRouteTypeEdit" runat="server">生产途程类别</asp:Label>
                        </td>
                        <td class="fieldValue" style="display: none">
                            <asp:DropDownList ID="drpRouteTypeEdit" CssClass="require" runat="server" Width="150px"
                                OnLoad="drpRouteTypeEdit_Load">
                            </asp:DropDownList>
                        </td>
                        <td nowrap width="100%">
                            <asp:CheckBox ID="chbRouteEnabled" runat="server" Text="是否使用" Checked="True"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEffectiveDateEdit" runat="server">生效日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <uc1:eMESDate id="dateEffectiveDateEdit" CssClass="require" width="150" runat="server">
                            </uc1:eMESDate>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblInvalidDateEdit" runat="server">失效日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <uc1:eMESDate id="dateInvalidDateEdit" CssClass="require" width="150" runat="server">
                            </uc1:eMESDate>
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
