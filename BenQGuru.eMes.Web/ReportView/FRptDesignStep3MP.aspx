<%@ Page Language="c#" CodeBehind="FRptDesignStep3MP.aspx.cs" EnableEventValidation="true"
    AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.ReportView.FRptDesignStep3MP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>报表设计 3/5</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
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
        gridRow.getCell(3).setValue("<a href=''>abc</a.");
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
        var columnName = row.getCell(1).getValue();
        var columnDesc = row.getCell(2).getValue();
        var opt = new Option(columnDesc, columnName);
        var fromsel = document.getElementById("lstUnSelectColumn");
        fromsel.options.add(opt);
		        
        row.deleteRow();
        }
        */

        //        var selectedRowIndex = 0;
        //        function WebDataGridView_RowSelectionChanged(webDataGrid, evntArgs) {
        //            selectedRowIndex=evntArgs.getSelectedRows().getItem(0).get_index();
        //        }


        function SetColumnUp() {

            var currentRow = $find('gridWebGrid')._behaviors.get_selection().get_selectedRows().getItem(0);
            if (currentRow == null) {
                return;
            }
            var currentIndex = $find('gridWebGrid')._behaviors.get_selection().get_selectedRows().getItem(0).get_index();
            if (currentIndex == 0) {
                return;
            }
            var preRow = $find('gridWebGrid').get_rows().get_row(currentIndex - 1);
            SwapRow($find('gridWebGrid'), currentRow, preRow);
            //            $find('gridWebGrid').get_rows().get_row(currentIndex - 1)._index = currentIndex;
            //            currentRowUltra._index = currentIndex - 1;
            //            
            //        var currentRow = $(currentRowUltra.get_element());
            //        var currentRowClone = currentRow.clone();
            //        var preRow = currentRow.prev();
            //        currentRow.remove();
            //        preRow.before(currentRowClone);
            //        alert($find('gridWebGrid')._behaviors.get_selection().get_selectedRows().getItem(0).get_index());
            //        currentRowClone.set_index(selectedRowIndex - 1);
            //        preRow.set_index(selectedRowIndex);
            //            var grid = $("#gridWebGrid");
            //            var row = igtbl_getActiveRow("gridWebGrid");
            //            if (row == null) {
            //                return;
            //            }
            //            var iIdx = row.getIndex();
            //            if (iIdx <= 0) {
            //                return;
            //            }
            //            SwapRow(grid, iIdx, iIdx - 1);
            //            row = grid.Rows.getRow(iIdx - 1);
            //            grid.setActiveRow(row);
        }
        function SetColumnDown() {
            var currentRow = $find('gridWebGrid')._behaviors.get_selection().get_selectedRows().getItem(0);
            if (currentRow == null) {
                return;
            }
            var currentIndex = $find('gridWebGrid')._behaviors.get_selection().get_selectedRows().getItem(0).get_index();

            if ($find('gridWebGrid').get_rows().get_row(currentIndex + 1) == null) {
                return;
            }
            var nextRow = $find('gridWebGrid').get_rows().get_row(currentIndex + 1);
            SwapRow($find('gridWebGrid'), currentRow, nextRow);

            //            var grid = igtbl_getGridById("gridWebGrid");
            //            var row = igtbl_getActiveRow("gridWebGrid");
            //            if (row == null) {
            //                return;
            //            }
            //            var iIdx = row.getIndex();
            //            if (iIdx >= grid.Rows.length - 1) {
            //                return;
            //            }
            //            SwapRow(grid, iIdx, iIdx + 1);
            //            row = grid.Rows.getRow(iIdx + 1);
            //            grid.setActiveRow(row);
        }
        function SwapRow(grid, row1, row2) {
            //            var row1, row2;
            //            row1 = grid.Rows.getRow(r1);
            //            row2 = grid.Rows.getRow(r2);
            //            var valTmp;
            //            for (var i = 1; i < grid.Bands[0].Columns.length; i++) {
            //                valTmp = row1.getCell(i).getValue();
            //                row1.getCell(i).setValue(row2.getCell(i).getValue());
            //                row2.getCell(i).setValue(valTmp);
            //            }
            var valTmp;
            for (var i = 0; i < grid.get_columns().get_length(); i++) {
                var colKey = grid.get_columns().get_column(i).get_key();
                if (colKey == 'Sequence') {
                    continue;
                }
                valTmp = row1.get_cellByColumnKey(colKey).get_value();
                row1.get_cellByColumnKey(colKey).set_value(row2.get_cellByColumnKey(colKey).get_value());
                row2.get_cellByColumnKey(colKey).set_value(valTmp);
            }
        }
        function OpenTotalSetWindow(rowIdx) {
            var grid = $("#gridWebGrid");
            var row = $find('gridWebGrid').get_rows().get_row(rowIdx);
            var columnName = row.get_cell(1).get_value();

            var arrGroupColumn = new Array();
            var arrGroupTotal = new Array();
            var strGType = document.getElementById("hidGroupTotal").value;
            var arrTmp = new Array();
            arrTmp = strGType.split('|');
            for (var i = 0; i < arrTmp.length; i++) {
                var strColumnGroupType = arrTmp[i];
                if (strColumnGroupType != "") {
                    var arrGroupListTmp = new Array();
                    arrGroupListTmp = strColumnGroupType.split('@');
                    arrGroupColumn[i] = arrGroupListTmp[0];
                    arrGroupTotal[i] = arrGroupListTmp[1];
                }
            }
            var existSetting = "";
            for (var i = 0; i < arrGroupColumn.length; i++) {
                if (arrGroupColumn[i] == columnName) {
                    existSetting = arrGroupTotal[i]
                    break;
                }
            }

            var url = "FRptDesignStep3TotalTypeSP.aspx?datasourceid=" + DATA_SOURCE_ID + "&groupcolumn=" + columnName + "&columnlist=" + DISPLAY_COLUMN_LIST + "&existsetting=" + existSetting + "&reportname=" + escape(document.getElementById("txtReportName").value);
            var ret = window.showModalDialog(url, "", "dialogHeight:400px;dialogWidth:800px");
            if (ret != undefined && ret != "") {
                var i = -1;
                for (i = 0; i < arrGroupColumn.length; i++) {
                    if (arrGroupColumn[i] == columnName) {
                        arrGroupTotal[i] = ret;
                        break;
                    }
                }
                if (i == arrGroupColumn.length) {
                    arrGroupColumn[i] = columnName;
                    arrGroupTotal[i] = ret;
                }
                var strResult = "";
                for (var i = 0; i < arrGroupColumn.length; i++) {
                    strResult = strResult + arrGroupColumn[i] + "@" + arrGroupTotal[i] + "|";
                }
                document.getElementById("hidGroupTotal").value = strResult;
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" id="tableMain" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblDesignStep3Title" runat="server" CssClass="labeltopic">报表设计 3/5 －－ 设置分组栏位</asp:Label>
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
                        <td>
                            <asp:ListBox runat="server" ID="lstUnSelectColumn" Width="300px" SelectionMode="multiple"
                                OnInit="lstUnSelectColumn_Init"></asp:ListBox>
                        </td>
                        <td valign="middle">
                            <input type="image" src="../Skin/Image/right_0.gif" id="imgSelect" runat="server" />
                            <br />
                            <br />
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
                <table class="edit" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;<input type="hidden" runat="server" id="hidGroupTotal" />
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
                                runat="server" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdNext" type="submit" value="下一步" name="cmdNext"
                                runat="server" />
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
