<%@ Page Language="c#" CodeBehind="FRptViewMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.ReportView.FRptViewMP" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>ReportView</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
  
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="tableMain" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblRptViewTitle" runat="server" CssClass="labeltopic">报表浏览</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" width="100%">
                    <tr>
                        <td nowrap>
                            <table runat="server" id="tbInput">
                                <tr>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldName" valign="bottom">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid" valign="top" style="padding:0px;">
                <span runat="server" id="spanMsg" visible="false" style="font-size: large; color: Red;
                    font-weight: bold;"></span>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%"
                    Font-Names="Verdana" Font-Size="8pt" OnDrillthrough="ReportViewer1_Drillthrough">
                </rsweb:ReportViewer>
            </td>
        </tr>
        <tr>
            <td nowrap style="padding:0px;">
                <table runat="server" id="tableTextBox">
                    <tr>
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldNameLeft" nowrap style="padding:0px;">
                            <asp:Label ID="lblDataCount" runat="server" Width="50px">记录总数</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap style="padding-top:0px;padding-bottom:0px;">
                            <asp:TextBox ID="txtDataCount" runat="server" CssClass="textbox" Width="60px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trHideDropDownList" style="visibility: hidden; display: none">
        </tr>
    </table>
    </form>
</body>
</html>
