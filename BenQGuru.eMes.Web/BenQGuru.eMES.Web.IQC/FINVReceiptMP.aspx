<%@ Page Language="c#" CodeBehind="FINVReceiptMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FINVReceiptMP" %>

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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">新增入库单维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReceiptNoQuery" runat="server">入库单号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtReceiveNoQuery" runat="server" Width="130px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblMaterialCodeQuery" runat="server">物料代码</asp:Label>
                        </td>
                        <td style="height: 26px">
                            <cc2:selectabletextbox id="txtMaterialCodeQuery" runat="server" Width="150px" cankeyin="true"
                                CssClass="textbox" Type="material">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblTicketTypeQuery" runat="server">单据类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpTicketTypeQuery" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblVendorCodeQuery" runat="server">供应商代码</asp:Label>
                        </td>
                        <td style="height: 26px">
                            <cc2:selectabletextbox id="txtVendorCodeQuery" runat="server" Width="150px" cankeyin="true"
                                CssClass="textbox" Type="vendor">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCreatDateStartQuery" runat="server">创建日期 从</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                        <asp:TextBox type="text" id="dateCreateDateStart"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCreatDateEndQuery" runat="server">到</asp:Label>
                        </td>
                        <td align="left" class="fieldValue" style="width: 159px">
                       <asp:TextBox type="text" id="dateCreateDateEnd"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblTicketStatus" runat="server">单据状态</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpTicketStatus" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblStorageQuery" runat="server">库别</asp:Label>
                        </td>
                        <td style="height: 26px">
                            <cc2:selectabletextbox id="txtStorageQuery" runat="server" Width="150px" cankeyin="true"
                                CssClass="textbox" Type="storagetype">
                            </cc2:selectabletextbox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td class="fieldName" nowrap>
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
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReceiveNoEdit" runat="server">入库单号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtReceiveNoEdit" runat="server" Width="150px" CssClass="require"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTicketTypeEdit" runat="server">单据类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpTicketTypeEdit" runat="server" CssClass="require" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblVendorCodeEdit" runat="server">供应商代码</asp:Label>
                        </td>
                        <td style="height: 26px">
                            <cc2:selectabletextbox id="txtVendorCodeEdit" runat="server" Width="150px" cankeyin="false"
                                CssClass="require" Type="singlevendor">
                            </cc2:selectabletextbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblStorageEdit" runat="server">库别</asp:Label>
                        </td>
                        <td style="height: 26px">
                            <cc2:selectabletextbox id="txtStorageEdit" runat="server" Width="150px" cankeyin="false"
                                CssClass="require" Type="singlestorage">
                            </cc2:selectabletextbox>
                        </td>
                        <td width="100%" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDescription" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue" width="100%" colspan="5">
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="textbox" Style="width: 100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" class="fieldValue">
                            <asp:TextBox ID="txtTicketStatus" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
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
