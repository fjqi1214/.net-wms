<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<%@ Page Language="c#" CodeBehind="FModuleMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FModuleMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FModuleMP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">模块维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tree">
                <ignav:UltraWebTree ID="treeModule" OnNodeSelectionChanged="treeModule_NodeSelectionChanged"
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
                                            ID="txtModuleCodeQuery" runat="server" Visible="False">nothing</asp:TextBox>
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
                                        <asp:Label ID="lblModuleCodeEdit" runat="server">模块代码</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtModuleCodeEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblModuleVersionEdit" runat="server">模块版本</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtModuleVersionEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblParentModuleCodeEdit" runat="server">父模块代码</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpParentModuleCodeEdit" runat="server" CssClass="textbox"
                                            Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblModuleSequenceEdit" runat="server">模块顺序</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtModuleSequenceEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblHelpFileNameEdit" runat="server">帮助文件</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtHelpFileNameEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblIsActiveEdit" runat="server">是否可用</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpIsActiveEdit" runat="server" CssClass="require" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblIsSystemEdit" runat="server">系统参数</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpIsSystemEdit" runat="server" CssClass="require" Width="130px"
                                            DESIGNTIMEDRAGDROP="252">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblModuleTypeEdit" runat="server">模块类型</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpModuleTypeEdit" runat="server" CssClass="require" Width="130px"
                                            AutoPostBack="True" OnLoad="drpModuleTypeEdit_Load" OnSelectedIndexChanged="drpModuleTypeEdit_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblModuleStatusEdit" runat="server">模块状态</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpModuleStatusEdit" runat="server" Width="130px" CssClass="require"
                                            OnLoad="drpModuleStatusEdit_Load">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblModuleDescEdit" runat="server">模块描述</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtModuleDescEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblFormUrlEdit" runat="server">页面地址</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtFormUrlEdit" runat="server" CssClass="textbox" Width="100%"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblRestrain" runat="server">抑制显示</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpIsRestrain" runat="server" CssClass="require" Width="130px"
                                            DESIGNTIMEDRAGDROP="252">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="100%">
                                    </td>
                                    <td width="100%">
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
