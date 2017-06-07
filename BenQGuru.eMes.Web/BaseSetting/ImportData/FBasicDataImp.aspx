<%@ Page Language="c#" CodeBehind="FBasicDataImp.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting.FBasicDataImp" %>

<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FBasicDataImp</title>
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
<body>
    <form method="post" runat="server" id="impForm">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
    <table id="Table1" runat="server" style="z-index: 101; left: 8px; position: absolute;
        top: 8px" height="100%" width="100%">
        <tbody>
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblBasicDataIn" runat="server" CssClass="labeltopic">基础资料导入</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="query" id="Table2" height="100%" width="100%">
                        <tr align="right">
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblImportType" runat="server"> 导入类型</asp:Label>
                            </td>
                            <td style="width: 447px">
                                <asp:DropDownList ID="InputTypeDDL" AutoPostBack="True" Width="130px" runat="server"
                                    OnSelectedIndexChanged="InputTypeDDL_SelectedIndexChanged">
                                    <asp:ListItem Value="Item">产品</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="fieldNameLeft" nowrap height="20">
                                <asp:Label ID="lblInFile" runat="server"> 导入文件：</asp:Label>
                            </td>
                            <td class="fieldValue">
                                <input id="DownLoadPathBom" style="width: 300px" type="file" size="56" name="DownLoadPathBom"
                                    class="textbox" runat="server" />
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
                            <td align="center">
                                <input class="submitImgButton" id="cmdView" onmouseover="document.getElementById('cmdQuery').style.backgroundImage='url(&quot;../../Skin/Image/search_0.gif&quot;)';"
                                    onmouseout="document.getElementById('cmdQuery').style.backgroundImage='url(&quot;../../Skin/Image/search.gif&quot;)';"
                                    type="submit" value="察 看" name="cmdQuery" runat="server" onserverclick="cmdQuery_ServerClick">
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
                                        <asp:Label ID="lblContainNG" runat="server">容错选项：</asp:Label></span>
                                </td>
                                <td nowrap align="left">
                                    <asp:RadioButtonList ID="ImportList" runat="server" RepeatColumns="5">
                                        <%--<asp:ListItem Selected="True" Value="Skip">跳过</asp:ListItem>
												<asp:ListItem Value="RoolBack">回滚</asp:ListItem> --%>
                                    </asp:RadioButtonList>
                                </td>
                                <td width="70%">
                                    <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                        runat="server" disabled onserverclick="cmdGridExport_ServerClick"><asp:Label ID="lblOutNoIn"
                                            runat="server">导出未导入数据</asp:Label>
                                </td>
                                <td style="font-weight: bold; left: 90%; position: absolute" valign="middle">
                                    <span>
                                        <asp:Label ID="lblAll" runat="server">共</asp:Label></span>
                                    <asp:Label ID="lblCount" runat="server"> 0 </asp:Label><span><asp:Label ID="lblNum"
                                        runat="server">笔</asp:Label></span>
                                </td>
                            </tr>
                            <tr class="normal">
                            </tr>
                            <tr class="toolBar">
                                <td colspan="4">
                                    <table class="toolBar" id="Table4">
                                        <tr>
                                            <td class="toolBar">
                                                <input class="submitImgButton" disabled id="cmdEnter" type="submit" value="导入" name="btnAdd"
                                                    runat="server" onserverclick="cmdAdd_ServerClick">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    </contenttemplate>
        <triggers>
        <asp:PostBackTrigger ControlID="InputTypeDDL"/>
        <asp:PostBackTrigger ControlID="cmdView"/>
        </triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
