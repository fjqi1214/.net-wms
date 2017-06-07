<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FErrorCode2OPReworkSP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.SelectQuery.FErrorCode2OPReworkSP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<html>
<head>
    <title>FErrorCode2OPReworkSP</title>
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
                <asp:Label ID="lblNGSelectTitle" runat="server">选择不良代码</asp:Label>
            </td>
        </tr>
        <tr>
            <td nowrap>
                <table class="query" id="Table2" height="40" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblNGCodeEdit" runat="server">不良代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 60px">
                            <asp:TextBox ID="txtErrorCodeQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblErrorDescriptionEdit" runat="server">不良代码描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 60px">
                            <asp:TextBox ID="txtErrorCodeDescQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblErrorCodeGroupEdit" runat="server">不良代码组</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 60px">
                            <asp:TextBox ID="txtErrorCodeGroupQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblErrorCodeGroupDescriptionEdit" runat="server">不良代码组描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 60px">
                            <asp:TextBox ID="txtErrorCodeGroupDescQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
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
                <asp:Button ID="cmdSelect" Style="background-image: url(../Skin/Image/down.gif)"
                    runat="server" CssClass="smallbutton"></asp:Button>&nbsp;<asp:Button ID="cmdUnSelect"
                        Style="background-image: url(../Skin/Image/up.gif)" runat="server" CssClass="smallbutton">
                    </asp:Button>
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
                        value="取 消" name="cmdCancel" runat="server"></TEXTAREA>
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: none">
                    <input type="button" value="init" id="cmdInit" runat="server" name="cmdInit">
                    <input id="txtOthers" type="hidden" runat="server" name="txtOthers">
                    <input id="pagePostBackCount" type="hidden" runat="server" name="txtOthers">
                    <textarea id="txtSelected" rows="1" cols="20" runat="server" name="txtSelected"></textarea>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
