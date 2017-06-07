<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FsingleExceptionCodeSP.aspx.cs"
    Inherits="BenQGuru.eMES.Web.SelectQuery.FsingleExceptionCodeSP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FsingleExceptionCodeSP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
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
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDateQuery" runat="server">日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox type="text" ID="DateQuery" class='datepicker' runat="server" Width="130px" />
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblShiftCodeQuery" runat="server"> 班次代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtShiftCodeQuery" runat="server" Width="130px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server"> 产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSSQuery" runat="server"> 产线代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSSQuery" runat="server" Width="130px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblExceptionCodeQuery" runat="server"> 异常事件代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtExceptionCodeQuery" runat="server" Width="130px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td nowrap class="fieldName">
                        </td>
                        <td class="fieldNameRight">
                            <input class="submitImgButton" id="cmdQuery" onmouseover="" onmouseout="" type="submit"
                                value="查 询" name="cmdQuery" runat="server">
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
        <tr class="normal">
            <td>
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
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
        <tr style="height: 50px">
            <td nowrap align="center">
                <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                    runat="server">
                &nbsp;
                <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                    runat="server">
            </td>
        </tr>
        <tr>
            <td>
                <div style="display: none">
                    <input id="cmdInit" type="button" value="init" name="cmdInit" runat="server">
                    <textarea id="txtSelected" rows="1" cols="20" runat="server" name="txtSelected"></textarea>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
