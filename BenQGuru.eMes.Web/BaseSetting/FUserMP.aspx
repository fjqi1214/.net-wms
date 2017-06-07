<%@ Page Language="c#" CodeBehind="FUserMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FUserMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FUserMP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">系统用户维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblUserSN" runat="server">用户代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtUserCodeQuery" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
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
                <ig:webdatagrid id="gridWebGrid" runat="server" width="100%">
                </ig:webdatagrid>
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
                            <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar ID="pagerToolBar" runat="server"></cc1:PagerToolBar>
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
                            <asp:Label ID="lblUserSNEdit" runat="server">用户代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtUserCodeEdit" CssClass="require" runat="server" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblUserNameEdit" runat="server">用户名</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtUserNameEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblUserPasswordEdit" runat="server">密码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtUserPasswordEdit" CssClass="require" runat="server" TextMode="Password"
                                Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblUserPasswordValidateEdit" runat="server">密码验证</asp:Label>
                        </td>
                        <td class="">
                            <asp:TextBox ID="txtUserPasswordMatchEdit" CssClass="require" runat="server" TextMode="Password"
                                Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblPasswordCache" runat="server" Visible="False">密码页面暂存</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtPasswordCache" runat="server" Visible="False" Width="130px"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblUserDepartmentEdit" runat="server">部门</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpDepartmentEdit" CssClass="require" runat="server" Width="130px"
                                Height="18px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblUserTelephoneEdit" runat="server">电话号码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtUserTelephoneEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblUserEmailEdit" runat="server">电子信箱</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtUserEmailEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblOrgEdit" runat="server">组织</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:SelectableTextBox ID="SelectableTextboxOrg" runat="server" CssClass="require"
                                Width="130px" Type="org" Target="FOrgSP.aspx"></cc2:SelectableTextBox>
                        </td>
                        <td nowrap width="100%">
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
                            <input class="submitImgButton" id="cmdUnLock" type="submit" value="激活" name="cmdUnLock"
                                runat="server" onserverclick="cmdUnLock_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdConfined" type="submit" value="限制" name="cmdConfined"
                                runat="server" onserverclick="cmdConfined_ServerClick">
                        </td>
                        <td class="toolBar">
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
