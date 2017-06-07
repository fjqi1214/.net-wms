<%@ Page Language="c#" CodeBehind="FShipToStockMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.IQC.FShipToStockMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FShipToStockMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">免检物料维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" style="height: 100%; width: 100%">
                    <tr>
                        <td class="fieldName" nowrap="noWrap">
                            <asp:Label ID="lblDQMCodeQuery" runat="server">鼎桥物料编码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap">
                            <asp:TextBox ID="txtDQMCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                       
                        <td class="fieldName" nowrap="noWrap">
                            <asp:Label ID="lblVendorCodeQuery" runat="server">供应商代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap">
                            <cc2:selectabletextbox id="txtVendorCodeQuery" runat="server" CanKeyIn="true" Type="vendor"
                                Target="" CssClass="textbox">
                            </cc2:selectabletextbox>
                        </td>
                         <td class="fieldName" nowrap="noWrap" visible="false">
                            <asp:Label ID="lblOrgQuery" runat="server" Visible="false">组织编码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" visible="false">
                            <asp:DropDownList ID="drpOrgQuery" runat="server" CssClass="textbox" Width="130px" Visible="false">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap="noWrap">
                            <asp:CheckBox ID="chkActiveQuery" runat="server" Text="有效" />
                        </td>
                        <td style="width: 100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 100%">
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table cellpadding="0" style="height: 100%; width: 100%">
                    <tr>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" onserverclick="cmdGridExport_ServerClick" />
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" onserverclick="cmdDelete_ServerClick" />
                        </td>
                        <td>
                            <cc1:PagerSizeSelector id="pagerSizeSelector" runat="server">
                            </cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar id="pagerToolBar" runat="server" PageSize="20">
                            </cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td style="height: 96px">
                <table class="edit" cellpadding="0" style="height: 100%; width: 100%">
                    <tr>
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                              <asp:Label ID="lblDQMaterialNO" runat="server">鼎桥物料编码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px">
                              <asp:TextBox ID="txtDQMaterialNO" runat="server" BackColor="#D2D2D2" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                       
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                            <asp:Label ID="lblVendorCodeEdit" runat="server"> 供应商代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px">
                            <cc2:selectabletextbox id="txtVendorCodeEdit" runat="server" CanKeyIn="false" Type="singlevendor"
                                Target="" CssClass="require">
                            </cc2:selectabletextbox>
                        </td>
                         <td class="fieldName" nowrap="noWrap" style="height: 26px" visible="false">
                            <asp:Label ID="lblOrgEdit" runat="server" Visible="false">组织</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px" visible="false">
                            <asp:DropDownList ID="drpOrgEdit" runat="server" CssClass="require" Width="130px" Visible="false">
                            </asp:DropDownList>
                        </td>
                       <%-- <td style="width: 100%">
                        </td>
                    </tr>
                    <tr>--%>
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                            <asp:Label ID="lblEffectDateEdit" runat="server">生效日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="width: 159px">
                            <asp:TextBox type="text" ID="datEffectDateEdit" class='datepicker' runat="server"
                                Width="130px" />
                        </td>
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                            <asp:Label ID="lblInvalidDateEdit" runat="server">失效日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px">
                            <asp:TextBox type="text" ID="datInvalidDateEdit" class='datepicker' runat="server"
                                Width="130px" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td style="height: 15px">
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server" onserverclick="cmdAdd_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
