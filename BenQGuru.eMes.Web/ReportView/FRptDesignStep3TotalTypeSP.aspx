<%@ Page Language="c#" CodeBehind="FRptDesignStep3TotalTypeSP.aspx.cs" EnableEventValidation="true"
    AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.ReportView.FRptDesignStep3TotalTypeSP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>TotalType</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <base target="_self"></base>
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script language="javascript">
        $(function () {
            $("#tableMain").height($(window).height());
            $("#gridWebGrid").height($(window).height() - 130);
        })

        function ReturnAndClose() {
            var str = "";
            var grid = $find('gridWebGrid');
            for (var i = 0; i < grid.get_rows().get_length(); i++) {
                var row = grid.get_rows().get_row(i);
                str = str + row.get_cell(2).get_value() + "," + row.get_cell(4).get_value() + ";";
            }
            window.returnValue = str;
            window.close();
        }
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" id="tableMain" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTotalTypeTitle" runat="server" CssClass="labeltopic">报表设计 3/5 －－ 设置分组计算方式</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReportName" runat="server">报表名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReportName" CssClass="textbox" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblGroupColumnName" runat="server">分组栏位名称</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtGroupColumnName" CssClass="textbox" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" cellpadding="0" width="100%">
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
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onclick="ReturnAndClose();return false;">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onclick="window.close();return false;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
