<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FPODistributionQTYMP.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FPODistributionQTYMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FStackMP</title>
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
                <asp:Label ID="lblPOdistriTitle" runat="server" CssClass="labeltopic">制定PO行来料数量</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                         <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="invno" runat="server" CssClass="require" Width="130px" Visible="false"></asp:TextBox>
                        </td>
                         <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="stno" runat="server" CssClass="require" Width="130px" Visible="false"></asp:TextBox>
                        </td>
                         <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="stline" runat="server" CssClass="require" Width="130px" Visible="false"></asp:TextBox>
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
       
      <%--  <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblITEMNOEdit" runat="server">ITEM号</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtITEMNOEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 26px" nowrap>
                            <asp:Label ID="lblINQtyEdit" runat="server">来料数量</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:TextBox ID="txtINQtyEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        
                         <td style="width:100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                       
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_click">
                        </td>
                      <%--  <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server">
                        </td>--%>
                         <td>
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                runat="server" onserverclick="cmdReturn_Click">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
