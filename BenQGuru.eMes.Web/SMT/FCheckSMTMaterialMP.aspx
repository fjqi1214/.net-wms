<%@ Page Language="c#" CodeBehind="FCheckSMTMaterialMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.SMT.FCheckSMTMaterialMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FCheckSMTMaterialMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body scroll="yes" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">物料对比</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtMOCodeQuery" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSSQuery" runat="server">产线代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSSCodeQuery" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:CheckBox ID="chbShowErrorOnly" runat="server" Text="只显示错误数据"></asp:CheckBox>
                        </td>
                        <td width="100%">
                            &nbsp;
                        </td>
                        <td nowrap align="right">
                            <asp:Label ID="txtCheckResult" runat="server" Font-Size="16" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdCompare" type="submit" value="对 比" name="cmdCompare"
                                runat="server" onserverclick="cmdCompare_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid" style="padding:0;">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblExceptMaterial" runat="server" CssClass="label">只存在于工单物料清单中的物料</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-right: thin solid; border-top: thin solid; border-left: thin solid;
                border-bottom: thin solid">
                <table>
                    <tr>
                        <td class="fieldGrid">
                            <ig:WebDataGrid ID="gridWebGrid2" runat="server" Width="100%">
                            </ig:WebDataGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table cellpadding="0" width="100%">
                    <tr>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                runat="server">
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
                            <input class="submitImgButton" id="cmdConfirmDifference" type="submit" value="认可差异"
                                name="cmdConfirm" runat="server" disabled="disabled" onserverclick="cmdConfirm_ServerClick"/>
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
