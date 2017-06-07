<%@ Page Language="c#" CodeBehind="FRptEntryMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.ReportView.FRptEntryMP" %>

<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FRptEntryMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function ViewReport(url) {
            //window.top.document.getElementById("content").src = url;
            window.top.document.getElementById('frmWorkSpace').contentWindow.location = url;
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td colspan="2">
                <asp:Label ID="lblRptEntryTitle" runat="server" CssClass="labeltopic">报表结构维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td style="border-right: #ababab 1px solid; border-top: #ababab 1px solid; border-left: #ababab 1px solid;
                border-bottom: #ababab 1px solid; background-color: #d7d8d9; vertical-align: top;
                width: 200px;">
                <ignav:UltraWebTree ID="treeMenu" runat="server" ExpandImage="ig_treePlus.gif" CollapseImage="ig_treeMinus.gif"
                    TargetFrame="ifPage" ImageDirectory="/ig_common/WebNavigator31/" Indentation="20"
                    Cursor="Hand" Font-Size="12px" WebTreeTarget="ClassicTree" Height="100%" Width="200px">
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
                            <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                            </ig:WebDataGrid>
                        </td>
                    </tr>
                    <tr class="normal">
                        <td>
                            <table height="100%" cellpadding="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtEntryCodeQuery" runat="server" Visible="False"></asp:TextBox>
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
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblRptEntrySequence" runat="server">次序</asp:Label>
                                    </td>
                                    <td class="fieldValue" style="width: 64px">
                                        <asp:TextBox ID="txtRptEntrySequence" CssClass="require" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblRptEntryCode" runat="server">代码</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtRptEntryCode" CssClass="require" runat="server" Width="120px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblRptEntryName" runat="server">名称</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtRptEntryName" runat="server" Width="120px" CssClass="textbox"></asp:TextBox>
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblRptEntryDesc" runat="server">描述</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox ID="txtRptEntryDesc" runat="server" Width="120px" CssClass="textbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblRptIsVisible" runat="server">是否可见</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:CheckBox runat="server" ID="chkRptIsVisible" />
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
