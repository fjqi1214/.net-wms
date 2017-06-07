<%@ Page Language="c#" CodeBehind="FMODownload.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FMODownload" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>MPageMODownload</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
    
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server" >
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">工单导入</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblEnterFile" runat="server">工单导入文件</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <input id="DownLoadPathMO" style="width: 454px" type="file" size="56" name="DownLoadPathMO"
                                class="textbox" runat="server"/>
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
                            <input language="javascript" class="submitImgButton" id="cmdViewMO" type="submit"
                                value="查看工单" name="btnQuery" runat="server" onserverclick="cmdView_ServerClick">
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
        <tr class="toolBar">
            <td>
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdEnter" type="submit"
                                value="导 入" name="cmdAdd" runat="server" onserverclick="cmdDownload_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                runat="server" onserverclick="cmdCancel_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="cmdViewMO"/>
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
