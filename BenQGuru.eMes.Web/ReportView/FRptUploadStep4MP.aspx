<%@ Page Language="c#" CodeBehind="FRptUploadStep4MP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.ReportView.FRptUploadStep4MP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>报表上传 4/4</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script language="javascript">
        $(function () {
            $("#tableMain").height($(window).height());
            $("#rptSecuritySelect_lstUnSelectedUserGroup").height($(window).height() - 210);
            $("#rptSecuritySelect_lstSelectedUserGroup").height($(window).height() - 210);
        })
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" id="tableMain">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">报表上传 4/4 －－ 发布报表</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReportFile" runat="server">报表文件</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReportFile" CssClass="textbox" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReportName" runat="server">报表名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReportName" CssClass="textbox" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid" valign="top">
                <table>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblPublishReportFolder" runat="server">报表目录</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpReportFolder" runat="server" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReportSecurity" runat="server">报表权限</asp:Label>
                        </td>
                        <td class="fieldValue" height="100%">
                            <uc1:ReportSecurity runat="server" id="rptSecuritySelect">
                            </uc1:ReportSecurity>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" cellpadding="0" width="100%">
                    <tr>
                        <td> &nbsp;
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
                            <input class="submitImgButton" id="cmdBack" type="submit" value="上一步" name="cmdBack"
                                runat="server">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdPublish" type="submit" value="发 布" name="cmdPublish"
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
