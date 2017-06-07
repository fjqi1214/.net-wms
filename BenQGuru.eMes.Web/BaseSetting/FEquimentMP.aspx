<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>

<%@ Page Language="c#" CodeBehind="FEquimentMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FEquimentMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FEquimentMP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">设备维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblEQPIDQuery" runat="server">设备编码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:SelectableTextBox ID="txtEQPIDQuery" runat="server" Type="Equiment" Width="100px"
                                CanKeyIn="true" CssClass="textbox"></cc2:SelectableTextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblEQPDESCQuery" runat="server">设备描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtEQPDESQuery" runat="server" Width="100px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="LabEQPTypeEdit" runat="server">设备类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpTypeQuery" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblEQPStatusQuery" runat="server">设备装态</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpEQPStatusQuery" CssClass="require" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                    </tr>
                </table>
    </td> </tr>
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
                        <asp:Label ID="lblPEQPIDEdit" runat="server">设备编码</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="txtEQPIDEdit" CssClass="require" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td class="fieldNameLeft" nowrap>
                        <asp:Label ID="lblEQPNameEdit" runat="server">设备名称</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="txtEQPNameEdit" CssClass="require" runat="server" Width="150px">
                        </asp:TextBox>
                    </td>
                    <td class="fieldName" nowrap>
                        <asp:Label ID="lblEQPModelEdit" runat="server">品牌</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="txtEQPModelEdit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td class="fieldNameLeft" nowrap>
                        <asp:Label ID="lblEQPTypeEdit" runat="server">设备类型</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:DropDownList ID="drpTypeEdit" CssClass="require" runat="server" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="fieldNameLeft" nowrap>
                        <asp:Label ID="lblTypeEdit" runat="server">设备型号</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="txtEQPTypeEdit" CssClass="textbox" runat="server" Width="150px">
                        </asp:TextBox>
                    </td>
                    <td class="fieldName" nowrap>
                        <asp:Label ID="lblEQPDESCEdit" runat="server">设备描述</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="TextBoxEQPDESCEdit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td class="fieldName" nowrap>
                        <asp:Label ID="lblEQPCompanyEdit" runat="server">厂商名称</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="TxtEQPCompanyEdit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td class="fieldName" nowrap>
                        <asp:Label ID="lblContactEdit" runat="server">联系人</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="txtContactEdit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fieldName" nowrap>
                        <asp:Label ID="lblTelPhoneEdit" runat="server">电话</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="txtTelPhoneEdit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td class="fieldName" nowrap>
                        <asp:Label ID="LabelEAttribute1" runat="server">预留1</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="txtEattribute1Edit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td class="fieldName" nowrap>
                        <asp:Label ID="LabelEAttribute2" runat="server">预留2</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="txtEattribute2Edit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                    </td>
                    <td class="fieldName" nowrap>
                        <asp:Label ID="LabelEAttribute3" runat="server">预留3</asp:Label>
                    </td>
                    <td class="fieldValue">
                        <asp:TextBox ID="txtEattribute3Edit" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
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
