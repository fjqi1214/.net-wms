<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRptViewDataSourceParam.aspx.cs"
    Inherits="BenQGuru.eMES.Web.ReportView.FRptViewDataSourceParam" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FRptViewDataSource</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" type="text/css" rel="stylesheet" />
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">数据源参数定义</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDBName" runat="server">数据源名称</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtDBName" runat="server" Width="150px" CssClass="textbox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDllName" runat="server">DLL文件名</asp:Label>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:TextBox ID="txtDllName" runat="server" CssClass="textbox" ReadOnly="True" Width="150px"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%" width="100%">
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
                        <td class="fieldName" style="height: 31px" nowrap>
                            <asp:Label ID="lblID" runat="server" Width="49px">次序</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 31px">
                            <asp:TextBox ID="txtNameEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" style="height: 31px" nowrap>
                            <asp:Label ID="lblParamEdit" runat="server">参数名称</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 31px">
                            <asp:TextBox ID="txtParamNameEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap style="height: 31px">
                            <asp:Label ID="lblparmdescEdit" runat="server">参数描述</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 31px; width: 2px;">
                            <asp:TextBox ID="txtparmdescEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="height: 26px; width: 75px;" nowrap>
                            <asp:Label ID="lblDataTypeEdit" runat="server" Width="80px">数据类型</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <asp:DropDownList ID="drpdbtype" runat="server" CssClass="require" Width="130px"
                                OnLoad="drpDBTypeEdit_Load">
                                <asp:ListItem>bool</asp:ListItem>
                                <asp:ListItem>date</asp:ListItem>
                                <asp:ListItem>numeric</asp:ListItem>
                                <asp:ListItem>string</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblDefaultNameEdit" runat="server">默认值</asp:Label>
                        </td>
                        <td colspan="3" class="fieldValue">
                            <asp:TextBox ID="txtDefaultNameEdit" runat="server" CssClass="require" Width="365px"></asp:TextBox>
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
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="删 除" name="cmdCancel"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                runat="server" onserverclick="cmdReturn_ServerClick">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input id="datasourceid" type="hidden" value="0" name="datasourceid" runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
