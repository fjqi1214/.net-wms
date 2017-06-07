<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FExcelDataImp.aspx.cs"
    Inherits="BenQGuru.eMES.Web.BaseSetting.ImportIndirectManCountDate.FExcelDataImp" %>

<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport"
    Assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns:igtbl>
<head>
    <title>FExcelDataImp</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function Check() {
            if (document.all.DownLoadPathBom.value == "") {
                alert("上传文件不能为空");
                return false;
            }
        }
        
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" style="z-index: 101; left: 8px; position: absolute; top: 8px"
        height="100%" width="100%" runat="server">
        <tbody>
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblDataInmport" runat="server" CssClass="labeltopic">数据导入</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="query" id="Table2" height="100%" width="100%">
                        <tr align="right">
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblImportType" Visible="false" runat="server"> 导入类型</asp:Label>
                            </td>
                            <td style="width: 447px">
                                <asp:DropDownList ID="InputTypeDDL" AutoPostBack="True" Width="0px" runat="server">
                                    <asp:ListItem Value="Item">产品</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblInFile" runat="server"> 导入文件：</asp:Label>
                            </td>
                            <td style="width: 300px">
                                <input id="DownLoadPathBom" style="width: 454px; height: 22px" type="file" size="56"
                                    name="File1" runat="server">
                            </td>
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblInTemplet" runat="server"> 导入模板：</asp:Label>
                            </td>
                            <td nowrap>
                                <a id="aFileDownLoad" style="display: none; color: blue" href="" target="_blank"
                                    runat="server">
                                    <asp:Label ID="lblDown" runat="server">下载</asp:Label></a> <span style="cursor: pointer;
                                        color: blue; text-decoration: underline" onclick="DownLoadFile()">
                                        <asp:Label ID="lblDown1" runat="server">下载</asp:Label></span>
                            </td>
                            <td nowrap width="100%">
                            </td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdEnter" type="submit" value="导入" name="btnAdd"
                                    runat="server" onserverclick="cmdAdd_ServerClick">
                            </td>
                            <td>
                                <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                    runat="server" onserverclick="cmdReturn_ServerClick">
                            </td>
                            <td align="center">
                                <input class="submitImgButton" id="cmdView" onmouseover="document.getElementById('cmdQuery').style.backgroundImage='url(&quot;../../Skin/Image/search_0.gif&quot;)';"
                                    disabled onmouseout="document.getElementById('cmdQuery').style.backgroundImage='url(&quot;../../Skin/Image/search.gif&quot;)';"
                                    type="submit" value="察 看" name="cmdQuery" visible="false" runat="server" onserverclick="cmdQuery_ServerClick">
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td colspan="8">
                                <div id="DIVForDownload" style="width: 1px; position: relative; height: 1px" runat="server"
                                    ms_positioning="GridLayout">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="fieldGrid">
                    <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                    </ig:WebDataGrid>
                </td>
            </tr>
            <tr class="normal">
                <td>
                    <table id="Table3" height="100%" cellpadding="0" width="100%">
                        <tbody>
                            <tr>
                                <td nowrap align="right">
                                    <span>
                                        <asp:Label ID="lblContainNG" runat="server" Visible="false">容错选项：</asp:Label></span>
                                </td>
                                <td nowrap align="left">
                                    <asp:RadioButtonList ID="ImportList" Visible="false" runat="server" RepeatColumns="5">
                                        <asp:ListItem Selected="True" Value="Skip">跳过</asp:ListItem>
                                        <asp:ListItem Value="RoolBack">回滚</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="font-weight: bold; left: 90%; position: absolute" valign="middle">
                                    <span>
                                        <asp:Label ID="lblAll" runat="server">共</asp:Label></span>
                                    <asp:Label ID="lblCount" runat="server"> 0 </asp:Label><span><asp:Label ID="lblNum"
                                        runat="server">笔</asp:Label></span>
                                </td>
                                <tr class="normal">
                                </tr>
                                <tr class="toolBar">
                                    <td colspan="4">
                                        <table class="toolBar" id="Table4">
                                            <tr>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
