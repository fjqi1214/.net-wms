<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<%@ Page Language="c#" CodeBehind="FMenuMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FMenuMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FMenuMP</title>
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
            <td colspan="2">
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">菜单维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tree" >
                <ignav:UltraWebTree ID="treeMenu" OnNodeSelectionChanged="treeMenu_NodeSelectionChanged"
                    runat="server" ExpandImage="ig_treePlus.gif" CollapseImage="ig_treeMinus.gif"
                    TargetFrame="ifPage" ImageDirectory="/ig_common/WebNavigator31/" Indentation="20"
                    Cursor="Hand" Font-Size="12px" WebTreeTarget="ClassicTree" Height="100%" Width="250px">
                    <SelectedNodeStyle ForeColor="White" BackColor="Navy"></SelectedNodeStyle>
                    <Levels>
                        <ignav:Level Index="0"></ignav:Level>
                        <ignav:Level Index="1"></ignav:Level>
                    </Levels>
                </ignav:UltraWebTree>
            </td>
            <td>
                <table height="100%" width="100%">
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
                                        <asp:TextBox ID="txtMenuCodeQuery" runat="server" Visible="False">nothing</asp:TextBox>
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
                                        <asp:Label ID="lblMenuCodeEdit" runat="server">菜单代码</asp:Label>
                                    </td>
                                    <td class="fieldValue" >
                                        <asp:TextBox ID="txtMenuCodeEdit" CssClass="require" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblParentMenuCodeEdit" runat="server">父菜单代码</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpParentMenuCodeEdit" runat="server" Width="120px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblMenuType" runat="server">菜单类型</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="width: 64px">
                                        <asp:DropDownList ID="drpMenuTypeEdit" runat="server" Width="120px" DESIGNTIMEDRAGDROP="566"
                                            OnLoad="drpMenuType_Load">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblModuleCodeEdit" runat="server">模块代码</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpModuleCodeEdit" runat="server" Width="120px" DESIGNTIMEDRAGDROP="566"
                                            OnLoad="drpModuleCodeEdit_Load">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblMenuSequenceEdit" runat="server">菜单顺序</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtMenuSequenceEdit" CssClass="require" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblMenuDescriptionEdit" runat="server">菜单描述</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtMenuDescriptionEdit" runat="server" Width="120px" CssClass="textbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblRestrain" runat="server">抑制显示</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:DropDownList ID="drpIsRestrain" runat="server" Width="130px" DESIGNTIMEDRAGDROP="252">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="66%" colspan="4">
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
