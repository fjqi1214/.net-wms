<%@ Page Language="c#" CodeBehind="FRptUploadStep2MP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.ReportView.FRptUploadStep2MP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>报表上传 1/4</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function OpenTotalSetWindow(rowIdx) {
            var grid = igtbl_getGridById("gridWebGrid");
            var row = grid.Rows.getRow(rowIdx);
            var columnName = row.getCell(1).getValue();
            var columnDesc = row.getCell(2).getValue();

            var arrFilterColumn = new Array();
            var arrFilterUIType = new Array();
            var strFilterUIType = document.getElementById("hidInputUIType").value;
            var arrTmp = new Array();
            arrTmp = strFilterUIType.split('|');
            for (var i = 0; i < arrTmp.length; i++) {
                var strColumnFilter = arrTmp[i];
                if (strColumnFilter != "") {
                    var arrFilterTmp = new Array();
                    arrFilterTmp = strColumnFilter.split('@');
                    arrFilterColumn[i] = arrFilterTmp[0];
                    arrFilterUIType[i] = arrFilterTmp[1];
                }
            }
            var existSetting = "";
            for (var i = 0; i < arrFilterColumn.length; i++) {
                if (arrFilterColumn[i] == columnName) {
                    existSetting = arrFilterUIType[i]
                    break;
                }
            }

            var url = "FRptDesignStep4FilterUISP.aspx?filtercolumn=" + columnName + "&columndesc=" + escape(columnDesc) + "&existsetting=" + escape(existSetting) + "&reportname=" + escape(document.getElementById("txtReportFile").value);
            var ret = window.showModalDialog(url, "", "dialogHeight:550px;dialogWidth:540px");
            if (ret != undefined && ret != "") {
                var i = -1;
                for (i = 0; i < arrFilterColumn.length; i++) {
                    if (arrFilterColumn[i] == columnName) {
                        arrFilterUIType[i] = ret;
                        break;
                    }
                }
                if (i == arrFilterColumn.length) {
                    arrFilterColumn[i] = columnName;
                    arrFilterUIType[i] = ret;
                }
                var strResult = "";
                for (var i = 0; i < arrFilterColumn.length; i++) {
                    strResult = strResult + arrFilterColumn[i] + "@" + arrFilterUIType[i] + "|";
                }
                document.getElementById("hidInputUIType").value = strResult;
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">报表上传 2/4 －－ 设置报表参数</asp:Label>
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
        <tr class="normal" style="display:none">
            <td>
                <table class="edit" cellpadding="0" width="100%">
                    <tr>
                        <td>
                        <input type="hidden" runat="server" id="hidInputUIType" />
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
