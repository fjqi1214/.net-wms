<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FInvReceiptDetail.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FInvReceiptDetail" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
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
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">入库单</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td align="left" class="fieldNameLeft" style="width: 50px">
                            <asp:Label ID="lblReceiptNoQuery" runat="server" Style="width: 50px">入库单号</asp:Label>
                        </td>
                        <td align="left" class="fieldValue">
                            <asp:TextBox ID="txtReceiveNoQuery" runat="server" Width="150px" CssClass="require"></asp:TextBox>
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
        <tr class="normal">
            <td>
                <table class="edit" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblReceiveLineEdit" runat="server">行号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtReceiveLineEdit" runat="server" Width="150px" CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblMaterialCodeEdit" runat="server">物料代码</asp:Label>
                        </td>
                        <td style="height: 26px">
                            <cc2:selectabletextbox id="txtMaterialCodeEdit" runat="server" Width="150px" cankeyin="false"
                                CssClass="require" Type="singlematerial">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblPlanQtyEdit" runat="server">计划数量</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtPlanQtyEdit" runat="server" Width="150px" CssClass="require"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblOrderNoEd" runat="server">采购单号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtOrderNoEdit" runat="server" Width="150px" CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblOrderLineEdit" runat="server">采购单行号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtOrderLineEdit" runat="server" Width="150px" CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblManagerCode" runat="server">保管员代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:SelectableTextBox ID="txtManagerCode" runat="server" CanKeyIn="false" Type="singleuser"
                                Target="">
                            </cc2:SelectableTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fileNameLeft" align="right" style="height: 26px" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue" width="100%" colspan="5">
                            <asp:TextBox ID="txtMemoEdit" runat="server" Width="100%" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldValue" style="width: 150px">
                            <asp:TextBox ID="txtTicketStatus" runat="server" Width="150px" CssClass="textbox"
                                Visible="false"></asp:TextBox>
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
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                runat="server" onserverclick="cmdReturn_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
