<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FRptDBServer.aspx.cs" Inherits="BenQGuru.eMES.Web.ReportView.FRptDBServer" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FRptDBServer</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">数据库定义</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblNameQuery" runat="server">名称</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtCodeQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblServerNameQuery" runat="server">数据库服务名</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtnameQuery" runat="server" Width="150px" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                            <td class="fieldName" style="width: 213px;">
                                <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                    runat="server">
                            </td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr width="100%">
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
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
                <table class="edit" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblnameEdit" runat="server" Width="49px">名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtNameEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lbldescEdit" runat="server">描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtDescriptEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDataBaseTypeCodeEdit" runat="server">数据库类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpDBTypeEdit" runat="server" CssClass="require" Width="130px">
                                <asp:ListItem Selected="True">oracle</asp:ListItem>
                                <asp:ListItem>sqlserver</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblservername" runat="server" Width="80px">数据库服务名</asp:Label>
                        </td>
                        <td class="fieldValue" >
                            <asp:TextBox ID="txtDBNameEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDefaultDB" runat="server">默认数据库名</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtDBDefaultNameEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td nowrap colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" style="width: 75px;" nowrap>
                            <asp:Label ID="lblconnectusername" runat="server" Width="77px">连接用户名</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtconnectusername" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblpassword" runat="server" Width="44px">密码</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtpassword" runat="server" CssClass="require" Width="130px" TextMode="Password"></asp:TextBox>
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chknopassword" runat="server" Text="空密码" AutoPostBack="True" OnCheckedChanged="chknopassword_CheckedChanged" />
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
                                runat="server"/>
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server"/>
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="删 除" name="cmdCancel"
                                runat="server"/>
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdTest" type="submit" value="测 试" name="cmdTest"
                                runat="server" onserverclick="Submit1_ServerClick"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="display:none">
                            <br/>
                            <asp:Label ID="lblMessage" runat="server" Width="180px" ForeColor="Red"></asp:Label>
                        </td>
                    
                        <td style="display:none">
                            <input id="datasourceid" type="hidden" value="0" name="datasourceid" runat="server"/>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
