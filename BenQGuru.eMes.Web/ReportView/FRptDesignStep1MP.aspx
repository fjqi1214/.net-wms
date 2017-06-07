<%@ Page Language="c#" CodeBehind="FRptDesignStep1MP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.ReportView.FRptDesignStep1MP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>报表设计 1/5</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script language="javascript">
        $(function () {
            $("#tableMain").height($(window).height());
        })
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" id="tableMain">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblDesignStep1Title" runat="server" CssClass="labeltopic">报表设计 1/5 －－ 指定数据源</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td nowrap width="100%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr height="100%">
            <td class="fieldGrid" valign="top">
                <table>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblRptDataSource" runat="server">数据源</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList runat="server" ID="drpDataSource">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label runat="server" ID="lblUploadRptName">报表名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox runat="server" ID="txtUploadRptName" CssClass="require"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label runat="server" ID="lblUploadRptDesc">报表描述</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox runat="server" ID="txtUploadRptDesc" CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="100%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;
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
                            <input class="submitImgButton" id="cmdNext" type="submit" value="下一步" name="cmdNext"
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
    </form>
</body>
</html>
