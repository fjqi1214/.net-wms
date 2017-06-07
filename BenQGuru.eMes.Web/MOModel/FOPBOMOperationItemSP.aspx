<%@ Page Language="c#" CodeBehind="FOPBOMOperationItemSP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.MOModel.FOPBOMOperationItemSP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FOPBOMOperationItemSP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 选择料品</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSBOMVersionQuery" runat="server"> SBOM Version </asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="TextboxSBOMVersionQuery" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSBOMItemCodeQuery" runat="server"> 子阶料料号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSBOMItemCodeQuery" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSBOMItemNameQuery" runat="server"> 子阶料名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSBOMItemNameQuery" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSBOMSourceItemCodeQuuery" runat="server"> 首选料</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSBOMSourceItemCodeQuery" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                        </td>
                        <td class="fieldValue" nowrap style="display: NONE">
                            <asp:CheckBox ID="cbOPBOMEdit" runat="server" Width="70px" Text="不存在于工序BOM"></asp:CheckBox>
                        </td>
                        <td class="fieldNameRight">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick">
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
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server" PageSize="20">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
            </td>
        </tr>
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="选 择" name="cmdAdd"
                                runat="server" language="javascript" onserverclick="cmdAdd_ServerClick">
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
