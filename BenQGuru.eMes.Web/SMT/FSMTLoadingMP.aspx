<%@ Page Language="c#" CodeBehind="FSMTLoadingMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.SMT.FSMTLoadingMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>料站表资料导入</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
      
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="impForm" method="post" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
    <table height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 料站表资料导入</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblInitFileQuery" runat="server"> 文件路径</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <input type="file" runat="server" id="fileInit" style="width: 454px" class="textbox"
                                name="fileInit">
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:CheckBox runat="server" ID="chkReplaceAll" Text="完整导入"></asp:CheckBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label runat="server" ID="lblInTemplet" Text="导入模板下载"></asp:Label>
                        </td>
                        <td nowrap>
                            <a id="aFileDownLoad" runat="server" style="display: none; color: blue">
                                <asp:Label runat="server" ID="lblDown" Text="下载" Style="display: none"></asp:Label></a>
                            <span style="cursor: pointer; color: blue; text-decoration: underline" onclick="DownLoadFile()">
                                <asp:Label ID="lblDown1" runat="server">下载</asp:Label></span>
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdView" type="submit" value="查 看" name="btnQuery"
                                runat="server" onserverclick="cmdView_ServerClick">
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
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdEnter" type="submit" value="导 入" name="cmdSave"
                                runat="server" onserverclick="cmdImport_ServerClick">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                runat="server" onserverclick="cmdReturn_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
      </contenttemplate>
        <triggers>
        <asp:PostBackTrigger ControlID="cmdView"/>
        </triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
