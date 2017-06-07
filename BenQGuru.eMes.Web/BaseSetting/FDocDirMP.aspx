<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<%@ Page Language="c#" CodeBehind="FDocDirMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FDocDirMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FDocDirMP</title>
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
            <td colspan="2">
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">文档目录维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tree">
                <ignav:UltraWebTree ID="treeDocument" OnNodeSelectionChanged="treeDocument_NodeSelectionChanged"
                    runat="server" Width="250px" Height="100%" WebTreeTarget="ClassicTree" Font-Size="12px"
                    Cursor="Hand" Indentation="20" ImageDirectory="/ig_common/WebNavigator31/" TargetFrame="ifPage"
                    CollapseImage="ig_treeMinus.gif" ExpandImage="ig_treePlus.gif">
                    <SelectedNodeStyle ForeColor="White" BackColor="Navy"></SelectedNodeStyle>
                    <Levels>
                        <ignav:Level Index="0"></ignav:Level>
                        <ignav:Level Index="1"></ignav:Level>
                    </Levels>
                </ignav:UltraWebTree>
            </td>
            <td>
                <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <asp:Label ID="lblDocDirTitle" runat="server" CssClass="labeltopic">文档目录维护路径</asp:Label>
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
                                    <td>
                                        <asp:TextBox
                                            ID="txtDocDirQuery" runat="server" Visible="False">nothing</asp:TextBox>
                                    </td>
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
                                        <asp:Label ID="lblDirSequenceEdit" runat="server">目录顺序</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtDirSequenceEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblDirNameEdit" runat="server">目录名称</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtDirNameEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtMemoEdit" runat="server" CssClass="textbox" Width="90px"></asp:TextBox>
                                        <asp:TextBox ID="txtDirSerialEdit" runat="server" CssClass="require" Width="130px"
                                            Visible="false"></asp:TextBox>
                                    </td>
                                    <td width="100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap="noWrap">
                                        <asp:Label ID="lblUploadUserGroupEdit" runat="server">上传权限用户组</asp:Label>
                                    </td>
                                    <td class="fieldValue" nowrap="noWrap">
                                        <cc2:SelectableTextBox ID="txtUploadUserGroupEdit" runat="server" CanKeyIn="false"
                                            Type="usergroup" Target="" CssClass="require"></cc2:SelectableTextBox>
                                    </td>
                                    <td class="fieldName" nowrap="noWrap">
                                        <asp:Label ID="lblQueryUserGroupEdit" runat="server">查阅权限用户组</asp:Label>
                                    </td>
                                    <td class="fieldValue" nowrap="noWrap">
                                        <cc2:SelectableTextBox ID="txtQueryUserGroupEdit" runat="server" CanKeyIn="false"
                                            Type="usergroup" Target="" CssClass="require"></cc2:SelectableTextBox>
                                    </td>
                                    <td colspan="3" width="100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap="noWrap">
                                        <asp:Label ID="lblCheckUserGroupEdit" runat="server">审核权限用户组</asp:Label>
                                    </td>
                                    <td class="fieldValue" nowrap="noWrap">
                                        <cc2:SelectableTextBox ID="txtCheckUserGroupEdit" runat="server" CanKeyIn="false"
                                            Type="usergroup" Target="" CssClass="require"></cc2:SelectableTextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblParentDirCodeEdit" runat="server" Visible="false">父目录</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpParentDirCodeEdit" runat="server" CssClass="textbox" Width="130px"
                                            Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="3" width="100%">
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
                                    <td>
                                        <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                            runat="server">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
