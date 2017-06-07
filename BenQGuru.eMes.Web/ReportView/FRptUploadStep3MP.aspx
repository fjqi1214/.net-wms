<%@ Page Language="c#" CodeBehind="FRptUploadStep3MP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.ReportView.FRptUploadStep3MP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>报表上传 3/4</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script language="javascript">
        $(function () {
            $("#tableMain").height($(window).height());
            $("#lstUnSelectColumn").height($(window).height() - 165);
            $("#gridWebGrid").height($(window).height() - 165);
        })
        function lstUnSelectColumn_Init() {
            $("#tableMain").height($(window).height());
            $("#lstUnSelectColumn").height($(window).height() - 165);
            $("#gridWebGrid").height($(window).height() - 165);
        }
        function down_clickCell() {
            var index = $find('gridWebGrid')._behaviors.get_activation().get_activeCell().get_row().get_index() + 1;
            if ($find('gridWebGrid').get_rows().get_row(index) == null) {
                return false;
            }
            $(document.getElementById("x:782293072.17:adr:" + index + ":tag:")).find('th').click();
        }
        function up_clickCell() {
            var index = $find('gridWebGrid')._behaviors.get_activation().get_activeCell().get_row().get_index() - 1;
            if (index == -1) {
                return false;
            }
            $(document.getElementById("x:782293072.17:adr:" + index + ":tag:")).find('th').click();
        }
        /*
        function SelectColumn()
        {
        var i;
        var selectValue, selectText;
        var fromsel = document.getElementById("lstUnSelectColumn");
        for (i = 0; i < fromsel.options.length; i++)
        {
        if (fromsel.options[i].selected == true)
        {
        selectValue = fromsel.options[i].value;
        selectText = fromsel.options[i].text;
        AddGridRow(selectValue, selectText);
        fromsel.options[i].selected = false;
        }
        }
        }
        function AddGridRow(columnName, columnDesc)
        {
        var grid = igtbl_getGridById("gridWebGrid");
        var gridRow = igtbl_addNew("gridWebGrid", 0);
        gridRow.getCell(0).setValue(grid.Rows.length);
        gridRow.getCell(1).setValue(columnName);
        gridRow.getCell(2).setValue(columnDesc);
        gridRow.getCell(3).setValue("reportfiltertype_equal");
        }
        function UnSelectColumn()
        {
        var grid = igtbl_getGridById("gridWebGrid");
        var row = igtbl_getActiveRow("gridWebGrid");
        if (row == null)
        {
        return;
        }
        var iIdx = row.getIndex();
        for (var i = iIdx + 1; i < grid.Rows.length; i++)
        {
        grid.Rows.getRow(i).getCell(0).setValue(parseInt(grid.Rows.getRow(i).getCell(0).getValue()) - 1);
        }
        row.deleteRow();
        }
        function SetColumnUp()
        {
        var grid = igtbl_getGridById("gridWebGrid");
        var row = igtbl_getActiveRow("gridWebGrid");
        if (row == null)
        {
        return;
        }
        var iIdx = row.getIndex();
        if (iIdx <= 0)
        {
        return;
        }
        SwapRow(grid, iIdx, iIdx - 1);
        row = grid.Rows.getRow(iIdx - 1);
        grid.setActiveRow(row);
        }
        function SetColumnDown()
        {
        var grid = igtbl_getGridById("gridWebGrid");
        var row = igtbl_getActiveRow("gridWebGrid");
        if (row == null)
        {
        return;
        }
        var iIdx = row.getIndex();
        if (iIdx >= grid.Rows.length - 1)
        {
        return;
        }
        SwapRow(grid, iIdx, iIdx + 1);
        row = grid.Rows.getRow(iIdx + 1);
        grid.setActiveRow(row);
        }
        function SwapRow(grid, r1, r2)
        {
        var row1, row2;
        row1 = grid.Rows.getRow(r1);
        row2 = grid.Rows.getRow(r2);
        var valTmp;
        for (var i=1; i < grid.Bands[0].Columns.length; i++)
        {
        valTmp = row1.getCell(i).getValue();
        row1.getCell(i).setValue(row2.getCell(i).getValue());
        row2.getCell(i).setValue(valTmp);
        }
        }
        */
    </script>
    <script language="javascript">
        function SetColumnUp() {
            var grid = igtbl_getGridById("gridWebGrid");
            var row = igtbl_getActiveRow("gridWebGrid");
            if (row == null) {
                return;
            }
            var iIdx = row.getIndex();
            if (iIdx <= 0) {
                return;
            }
            SwapRow(grid, iIdx, iIdx - 1);
            row = grid.Rows.getRow(iIdx - 1);
            grid.setActiveRow(row);
        }
        function SetColumnDown() {
            var grid = igtbl_getGridById("gridWebGrid");
            var row = igtbl_getActiveRow("gridWebGrid");
            if (row == null) {
                return;
            }
            var iIdx = row.getIndex();
            if (iIdx >= grid.Rows.length - 1) {
                return;
            }
            SwapRow(grid, iIdx, iIdx + 1);
            row = grid.Rows.getRow(iIdx + 1);
            grid.setActiveRow(row);
        }
        function SwapRow(grid, r1, r2) {
            var row1, row2;
            row1 = grid.Rows.getRow(r1);
            row2 = grid.Rows.getRow(r2);
            var valTmp;
            for (var i = 1; i < grid.Bands[0].Columns.length; i++) {
                valTmp = row1.getCell(i).getValue();
                row1.getCell(i).setValue(row2.getCell(i).getValue());
                row2.getCell(i).setValue(valTmp);
            }
        }
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

            var url = "FRptDesignStep4FilterUISP.aspx?filtercolumn=" + columnName + "&columndesc=" + escape(columnDesc) + "&existsetting=" + escape(existSetting) + "&reportname=" + escape(document.getElementById("txtReportName").value);
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
    <table height="100%" width="100%" id="tableMain" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">报表上传 3/4 －－ 设置过滤栏位</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
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
        <tr height="100%">
            <td class="fieldGrid" valign="top">
                <table height="100%">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblUnSelectedColumn">待选择栏位</asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblSelectedColumn">已选择栏位</asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td height="100%">
                            <asp:ListBox runat="server" ID="lstUnSelectColumn" Width="300px" Height="100%" SelectionMode="multiple" OnInit="lstUnSelectColumn_Init">
                            </asp:ListBox>
                        </td>
                        <td valign="middle">
                            <input type="image" src="../Skin/Image/right_0.gif" id="imgSelect" runat="server" />
                            <br /><br />
                            <input type="image" src="../Skin/Image/left_0.gif" id="imgUnSelect" runat="server" />
                        </td>
                        <td class="fieldGrid">
                            <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                            </ig:WebDataGrid>
                        </td>
                        <td valign="middle">
                             <input type="image" src="../Skin/Image/up_0.gif" border="0" runat="server" id="imgUp" onclick="return up_clickCell()" />
                            <br />
                            <br />
                            <input type="image" src="../Skin/Image/down_0.gif" border="0" runat="server" id="imgDown" onclick="return down_clickCell()"/>
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
                            &nbsp;<input type="hidden" runat="server" id="hidInputUIType" />
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
