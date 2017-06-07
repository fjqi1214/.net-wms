<%@ Page Language="c#" CodeBehind="FMOSP2.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.SelectQuery.FMOSP2" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FMOSP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" cellspacing="1" cellpadding="1" width="100%" border="0"
        runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblSelectTitle" runat="server">选择</asp:Label>
            </td>
        </tr>
        <tr>
            <td nowrap>
                <table class="query" id="Table2" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblModelCodeQuery" runat="server">产品别代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtModelCodeQuery" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        </td>
                        <td nowrap class="fieldName">
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td nowrap class="fieldValue">
                            <asp:TextBox ID="txtItemCodeQuery" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        </td>
                        <td nowrap class="fieldName">
                            <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                        </td>
                        <td nowrap class="fieldValue">
                            <asp:TextBox ID="txtMOCodeQuery" runat="server" CssClass="textbox" Width="120px"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                            &nbsp;
                        </td>
                        <td class="fieldName">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStartTimeQuery" runat="server">起始时间</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtStartTimeQuery" runat="server" Width="120px" CssClass="textbox"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEndTimeQuery" runat="server">结束时间</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtEndTimeQuery" runat="server" Width="120px" CssClass="textbox"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue" nowrap>
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" onmouseover="" onmouseout="" type="submit"
                                value="查 询" name="cmdQuery" runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridUnSelected" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr>
            <td nowrap>
                <table id="Table3" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td nowrap align="center">
                <asp:Button ID="cmdSelect" Style="background-image: url(/eMes/Skin/Image/down.gif)"
                    runat="server" CssClass="smallbutton"></asp:Button>&nbsp;
                <asp:Button ID="cmdUnSelect" Style="background-image: url(/eMes/Skin/Image/up.gif)"
                    runat="server" CssClass="smallbutton"></asp:Button>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridSelected" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr>
            <td nowrap align="center">
                &nbsp;<input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                    runat="server">&nbsp;<input class="submitImgButton" id="cmdCancel" type="submit"
                        value="取 消" name="cmdCancel" runat="server">
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: none">
                    <input id="cmdInit" type="button" value="init" name="cmdInit" runat="server">
                    <textarea id="txtSelected" name="txtSelected" rows="1" cols="20" runat="server"></textarea>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
