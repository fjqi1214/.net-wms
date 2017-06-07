<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<%@ Page Language="c#" CodeBehind="FFileCheckMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting.FFileCheckMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FFileUpLoadMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server" enctype="multipart/form-data">
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
                                        <asp:TextBox ID="txtDocSerialEdit" runat="server" Visible="False"></asp:TextBox>
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
                    <tr class="toolBar">
                        <td>
                            <table class="toolBar" height="100%">
                                <tr>
                                    <td class="toolBar">
                                        <input class="submitImgButton" id="cmdCheck" type="submit" value="审核通过" name="cmdCheck"
                                            runat="server" onserverclick="cmdCheck_ServerClick">
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
