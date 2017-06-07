<%@ Page Language="c#" CodeBehind="FRptDesignChartMP.aspx.cs" EnableEventValidation="false"
    AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.ReportView.FRptDesignChartMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="ReportSecurity" Src="ReportSecurity.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>DesignChart</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script language="javascript">
        $(function () {

            SetSelectedChartType();
            SetChartLabelStyle();

            $("#tableMain").height($(window).height());
            $("#lstUnSelectColumn").height($(window).height() - 165);
            $("#gridWebGrid").height($(window).height() - 165);
        })

        function lstUnSelectColumn_OnInit() {
            $("#tableMain").height($(window).height());
            $("#lstUnSelectColumn").height($(window).height() - 165);
            $("#gridWebGrid").height($(window).height() - 165);
        }

        function ChartTypeSelect(selectImg) {
            var prevValue = document.getElementById("hidChartType").value;
            if (prevValue != undefined && prevValue != "") {
                var prevValueId = GetImageIdByChartType(prevValue, document.getElementById("hidChartSubType").value);
                var imgSelect = document.getElementById(prevValueId);
                imgSelect.style.borderColor = document.getElementById(selectImg).style.borderColor;
            }
            var imgObj = document.getElementById(selectImg);
            imgObj.style.borderWidth = 1;
            imgObj.style.borderColor = "#ff0000";
            document.getElementById("hidChartType").value = imgObj.getAttribute("ChartType");
            document.getElementById("hidChartSubType").value = imgObj.getAttribute("ChartSubType");
            document.getElementById("spanChartTypeDesc").innerText = imgObj.getAttribute("Description");
            document.getElementById("hidSelectChartTypeId").value = selectImg;
        }
        function GetImageIdByChartType(chartType, chartSubType) {
            var imgList = document.getElementsByTagName("img");
            for (var i = 0; i < imgList.length; i++) {
                if (imgList[i].getAttribute("ChartType") == chartType && imgList[i].getAttribute("ChartSubType") == chartSubType) {
                    return imgList[i].id;
                }
            }
            return "";
        }
        function ChartTypeMouseOver(img) {
            var imgObj = document.getElementById(img);
            imgObj.style.borderWidth = 0;
            document.getElementById("spanChartTypeDesc").innerText = imgObj.getAttribute("Description");
        }
        function ChartTypeMouseOut(img) {
            document.getElementById(img).style.borderWidth = 1;
            document.getElementById("spanChartTypeDesc").innerText = "";
            if (document.getElementById("hidChartType").value != "") {
                document.getElementById("spanChartTypeDesc").innerText = document.getElementById(document.getElementById("hidSelectChartTypeId").value).getAttribute("Description");
            }
        }
        function SetSelectedChartType() {
            var img = GetImageIdByChartType(document.getElementById("hidChartType").value, document.getElementById("hidChartSubType").value);
            if (img == undefined || img == "") {
                return;
            }
            ChartTypeSelect(img);
        }
        function MoveListItem(fromSelectId, toSelectId, isSelect, valId) {
            var i;
            var fromsel = document.getElementById(fromSelectId);
            var tosel = document.getElementById(toSelectId);
            for (i = 0; i < fromsel.options.length; i++) {
                if (fromsel.options[i].selected == true) {
                    var opt = new Option(fromsel.options[i].text, fromsel.options[i].value);
                    tosel.options.add(opt);
                }
            }
            for (i = fromsel.options.length - 1; i >= 0; i--) {
                if (fromsel.options[i].selected == true) {
                    fromsel.options.remove(i);
                }
            }
            var str = "";
            var selVal;
            if (isSelect == 1) {
                selVal = tosel;
            }
            else {
                selVal = fromsel;
            }
            UpdateSelectHiddenValue(selVal, valId);
        }
        function UpdateSelectHiddenValue(sel, hid) {
            var str = "";
            for (i = 0; i < sel.options.length; i++) {
                str = str + sel.options[i].value + ";";
            }
            document.getElementById(hid).value = str;
        }
        function MoveListItemUp(selectId, valId) {
            var sel = document.getElementById(selectId);
            for (var i = 1; i < sel.options.length; i++) {
                if (sel.options[i].selected == true) {
                    SwapListColumn(sel, i, i - 1);
                    sel.selectedIndex = i - 1;
                    break;
                }
            }
            UpdateSelectHiddenValue(sel, valId);
        }
        function MoveListItemDown(selectId, valId) {
            var sel = document.getElementById(selectId);
            for (var i = 0; i < sel.options.length - 1; i++) {
                if (sel.options[i].selected == true) {
                    SwapListColumn(sel, i, i + 1);
                    sel.selectedIndex = i + 1;
                    break;
                }
            }
            UpdateSelectHiddenValue(sel, valId);
        }
        function SwapListColumn(sel, idx1, idx2) {
            var val = sel.options[idx1].value;
            var text = sel.options[idx1].text;
            sel.options[idx1].value = sel.options[idx2].value;
            sel.options[idx1].text = sel.options[idx2].text;
            sel.options[idx2].value = val;
            sel.options[idx2].text = text;
        }
    </script>
    <script language="javascript">
        function SelectColumn() {
            var i;
            var selectValue, selectText;
            var fromsel = document.getElementById("lstUnSelectColumn");
            for (i = 0; i < fromsel.options.length; i++) {
                if (fromsel.options[i].selected == true) {
                    selectValue = fromsel.options[i].value;
                    selectText = fromsel.options[i].text;
                    AddGridRow(selectValue, selectText);
                    fromsel.options.remove(i);
                }
            }
            UpdateSelectedDataColumn();
        }
        function AddGridRow(columnName, columnDesc) {
            var grid = igtbl_getGridById("gridWebGrid");
            var gridRow = igtbl_addNew("gridWebGrid", 0);
            gridRow.getCell(0).setValue(grid.Rows.length);
            gridRow.getCell(1).setValue(columnName);
            gridRow.getCell(2).setValue(columnDesc);
            gridRow.getCell(3).setValue("reporttotaltype_sum");
        }
        function UnSelectColumn() {
            var grid = igtbl_getGridById("gridWebGrid");
            var row = igtbl_getActiveRow("gridWebGrid");
            if (row == null) {
                return;
            }
            var iIdx = row.getIndex();
            for (var i = iIdx + 1; i < grid.Rows.length; i++) {
                grid.Rows.getRow(i).getCell(0).setValue(parseInt(grid.Rows.getRow(i).getCell(0).getValue()) - 1);
            }
            var columnName = row.getCell(1).getValue();
            var columnDesc = row.getCell(2).getValue();
            row.deleteRow();
            var opt = new Option(columnDesc, columnName);
            document.getElementById("lstUnSelectColumn").options.add(opt);
            UpdateSelectedDataColumn();
        }
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
        }
        function SwapRow(grid, row1, row2) {
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
        function OpenFormatWin(url) {
            var url = "FRptTextFormatMP.aspx";
            url = url + "?existvalue=" + escape(document.getElementById("hidLabelFormat").value);
            var ret = window.showModalDialog(url, "", "dialogHeight:500px;dialogWidth:420px");
            if (ret != null && ret != undefined && ret != "") {
                document.getElementById("hidLabelFormat").value = ret;
                SetChartLabelStyle();
            }
        }
        function SetChartLabelStyle() {
            var val = document.getElementById("hidLabelFormat").value;
            if (val == "")
                return;
            var styleList = new Array();
            styleList = val.split(";");
            var lnk = document.getElementById("spanLabelFormat");
            lnk.style.fontFamily = styleList[0];
            lnk.style.fontSize = styleList[1];
            if (styleList[2] == "true")
                lnk.style.fontWeight = "bold";
            else
                lnk.style.fontWeight = "normal";
            if (styleList[3] == "true")
                lnk.style.fontStyle = "italic";
            else
                lnk.style.fontStyle = "normal";
            if (styleList[4] == "true")
                lnk.style.textDecoration = "underline";
            else
                lnk.style.textDecoration = "none";
            lnk.style.color = styleList[5];
            lnk.style.backgroundColor = styleList[6];
            if (styleList[7].indexOf("center") >= 0)
                lnk.parentNode.style.textAlign = "center";
            else if (styleList[7].indexOf("right") >= 0)
                lnk.parentNode.style.textAlign = "right";
            else
                lnk.parentNode.style.textAlign = "left";
            if (styleList[8].indexOf("top") >= 0)
                lnk.parentNode.style.verticalAlign = "top";
            else if (styleList[8].indexOf("bottom") >= 0)
                lnk.parentNode.style.verticalAlign = "bottom";
            else
                lnk.parentNode.style.verticalAlign = "middle";
        }
        function UpdateSelectedDataColumn() {
            var str = "";
            var grid = $find('gridWebGrid');
            for (var i = 0; i < grid.get_columns().get_length(); i++) {
                var columnName = grid.get_rows().get_row(i).get_cell(1).get_value();
                str = str + columnName + ";";
            }
            document.getElementById("hidSelectedDataValue").value = str;
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
    </script>
</head>
<body ms_positioning="GridLayout" scroll="yes">
    <form id="Form1" method="post" runat="server">
    <table height="100%" width="100%" id="tableMain" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">图表样式设定</asp:Label>
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
                <table width="100%">
                    <tr>
                        <td>
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblChartStyle" runat="server">图形样式</asp:Label></legend>
                                <input type="hidden" id="hidChartType" runat="server" />
                                <input type="hidden" id="hidChartSubType" runat="server" />
                                <input type="hidden" id="hidSelectChartTypeId" />
                                <table width="100%">
                                    <tr>
                                        <td width="100%">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <img runat="server" id="imgChartTypeColumn1" onmouseout="ChartTypeMouseOut('imgChartTypeColumn1');"
                                                            onmouseover="ChartTypeMouseOver('imgChartTypeColumn1');" onclick="ChartTypeSelect('imgChartTypeColumn1');"
                                                            src="../Skin/Image/Column1.jpg" charttype="Column" chartsubtype="Plain" description="柱形图"
                                                            border="1" style="border-collapse: collapse" />
                                                    </td>
                                                    <td>
                                                        <img runat="server" id="imgChartTypeColumn2" onmouseout="ChartTypeMouseOut('imgChartTypeColumn2');"
                                                            onmouseover="ChartTypeMouseOver('imgChartTypeColumn2');" onclick="ChartTypeSelect('imgChartTypeColumn2');"
                                                            src="../Skin/Image/Column2.jpg" charttype="Column" chartsubtype="Stacked" description="堆积柱形图"
                                                            border="1" style="border-collapse: collapse" />
                                                    </td>
                                                    <td>
                                                        <img runat="server" id="imgChartTypeColumn3" onmouseout="ChartTypeMouseOut('imgChartTypeColumn3');"
                                                            onmouseover="ChartTypeMouseOver('imgChartTypeColumn3');" onclick="ChartTypeSelect('imgChartTypeColumn3');"
                                                            src="../Skin/Image/Column3.jpg" charttype="Column" chartsubtype="PercentStacked"
                                                            description="百分比堆积柱形图" border="1" style="border-collapse: collapse" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <img runat="server" id="imgChartTypeBar1" onmouseout="ChartTypeMouseOut('imgChartTypeBar1');"
                                                            onmouseover="ChartTypeMouseOver('imgChartTypeBar1');" onclick="ChartTypeSelect('imgChartTypeBar1');"
                                                            src="../Skin/Image/Bar1.jpg" charttype="Bar" chartsubtype="Plain" description="条形图"
                                                            border="1" style="border-collapse: collapse" />
                                                    </td>
                                                    <td>
                                                        <img runat="server" id="imgChartTypeBar2" onmouseout="ChartTypeMouseOut('imgChartTypeBar2');"
                                                            onmouseover="ChartTypeMouseOver('imgChartTypeBar2');" onclick="ChartTypeSelect('imgChartTypeBar2');"
                                                            src="../Skin/Image/Bar2.jpg" charttype="Bar" chartsubtype="Stacked" description="堆积条形图"
                                                            border="1" style="border-collapse: collapse" />
                                                    </td>
                                                    <td>
                                                        <img runat="server" id="imgChartTypeBar3" onmouseout="ChartTypeMouseOut('imgChartTypeBar3');"
                                                            onmouseover="ChartTypeMouseOver('imgChartTypeBar3');" onclick="ChartTypeSelect('imgChartTypeBar3');"
                                                            src="../Skin/Image/Bar3.jpg" charttype="Bar" chartsubtype="PercentStacked" description="百分比堆积条形图"
                                                            border="1" style="border-collapse: collapse" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <img runat="server" id="imgChartTypeLine1" onmouseout="ChartTypeMouseOut('imgChartTypeLine1');"
                                                            onmouseover="ChartTypeMouseOver('imgChartTypeLine1');" onclick="ChartTypeSelect('imgChartTypeLine1');"
                                                            src="../Skin/Image/Line1.jpg" charttype="Line" chartsubtype="Plain" description="折线图"
                                                            border="1" style="border-collapse: collapse" />
                                                    </td>
                                                    <td>
                                                        <img runat="server" id="imgChartTypeLine2" onmouseout="ChartTypeMouseOut('imgChartTypeLine2');"
                                                            onmouseover="ChartTypeMouseOver('imgChartTypeLine2');" onclick="ChartTypeSelect('imgChartTypeLine2');"
                                                            src="../Skin/Image/Line2.jpg" charttype="Line" chartsubtype="Smooth" description="平滑线图"
                                                            border="1" style="border-collapse: collapse" />
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>
                                                        <img runat="server" id="imgChartTypePie1" onmouseout="ChartTypeMouseOut('imgChartTypePie1');"
                                                            onmouseover="ChartTypeMouseOver('imgChartTypePie1');" onclick="ChartTypeSelect('imgChartTypePie1');"
                                                            src="../Skin/Image/Pie1.jpg" charttype="Pie" chartsubtype="Plain" description="饼图"
                                                            border="1" style="border-collapse: collapse" />
                                                    </td>
                                                    <td>
                                                        <img runat="server" id="imgChartTypePie2" onmouseout="ChartTypeMouseOut('imgChartTypePie2');"
                                                            onmouseover="ChartTypeMouseOver('imgChartTypePie2');" onclick="ChartTypeSelect('imgChartTypePie2');"
                                                            src="../Skin/Image/Pie2.jpg" charttype="Pie" chartsubtype="Exploded" description="分离型饼图"
                                                            border="1" style="border-collapse: collapse" />
                                                    </td>
                                                    <td valign="top" width="100%">
                                                        &nbsp;&nbsp;<span id="spanChartTypeDesc"></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
                            <br />
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblSelectGroupSeq" runat="server">选择分组序列</asp:Label></legend>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblUnSelectedSeries">待选择栏位</asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblSelectedSeries">已选择栏位</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox runat="server" ID="lstUnSelectedSeries" SelectionMode="multiple" Height="100px"
                                                Width="200px"></asp:ListBox>
                                        </td>
                                        <td valign="middle">
                                            <img src="../Skin/Image/right_0.gif" runat="server" id="imgSelectSeries" border="0"
                                                onclick="MoveListItem('lstUnSelectedSeries','lstSelectedSeries',1,'hidSelectedSeriesValue');" />
                                            <br />
                                            <br />
                                            <img src="../Skin/Image/left_0.gif" runat="server" id="imgUnSelectSeries" border="0"
                                                onclick="MoveListItem('lstSelectedSeries','lstUnSelectedSeries',0,'hidSelectedSeriesValue');" />
                                        </td>
                                        <td>
                                            <asp:ListBox runat="server" ID="lstSelectedSeries" SelectionMode="multiple" Height="100px"
                                                Width="200px"></asp:ListBox>
                                            <input type="hidden" id="hidSelectedSeriesValue" runat="server" />
                                        </td>
                                        <td valign="middle">
                                            <img src="../Skin/Image/up_0.gif" border="0" onclick="MoveListItemUp('lstSelectedSeries','hidSelectedSeriesValue');" />
                                            <br />
                                            <br />
                                            <img src="../Skin/Image/down_0.gif" border="0" onclick="MoveListItemDown('lstSelectedSeries','hidSelectedSeriesValue');" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
                            <br />
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblSelectStatisticsColumn" runat="server">选择统计栏位</asp:Label></legend>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblUnSelectedCategory">待选择栏位</asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblSelectedCategory">已选择栏位</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox runat="server" ID="lstUnSelectedCategory" SelectionMode="multiple" Height="100px"
                                                Width="200px"></asp:ListBox>
                                        </td>
                                        <td valign="middle">
                                            <img src="../Skin/Image/right_0.gif" runat="server" id="imgSelectCategory" border="0"
                                                onclick="MoveListItem('lstUnSelectedCategory','lstSelectedCategory',1,'hidSelectedCategoryValue');" />
                                            <br />
                                            <br />
                                            <img src="../Skin/Image/left_0.gif" runat="server" id="imgUnSelectCategory" border="0"
                                                onclick="MoveListItem('lstSelectedCategory','lstUnSelectedCategory',0,'hidSelectedCategoryValue');" />
                                        </td>
                                        <td>
                                            <asp:ListBox runat="server" ID="lstSelectedCategory" SelectionMode="multiple" Height="100px"
                                                Width="200px"></asp:ListBox>
                                            <input type="hidden" id="hidSelectedCategoryValue" runat="server" />
                                        </td>
                                        <td valign="middle">
                                            <img src="../Skin/Image/up_0.gif" border="0" onclick="MoveListItemUp('lstSelectedCategory','hidSelectedCategoryValue');" />
                                            <br />
                                            <br />
                                            <img src="../Skin/Image/down_0.gif" border="0" onclick="MoveListItemDown('lstSelectedCategory','hidSelectedCategoryValue');" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
                            <br />
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblSelectDataColumn" runat="server">选择数据栏位</asp:Label></legend>
                                <table width="100%">
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
                                    <tr width="100%">
                                        <td>
                                            <asp:ListBox runat="server" ID="lstUnSelectColumn" Width="200px" Height="100px" SelectionMode="multiple"
                                                OnInit="lstUnSelectColumn_OnInit"></asp:ListBox>
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
                                            <input type="image" src="../Skin/Image/up_0.gif" border="0" runat="server" id="imgUp"
                                                onclick="return up_clickCell()" />
                                            <br />
                                            <br />
                                            <input type="image" src="../Skin/Image/down_0.gif" border="0" runat="server" id="imgDown"
                                                onclick="return down_clickCell()" />
                                            <input type="hidden" runat="server" id="hidSelectedDataValue" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <br />
                            <br />
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblStyleSet" runat="server">样式设置</asp:Label></legend>
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <asp:CheckBox runat="server" ID="chkIsShowLegend" Text="是否显示图例" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkIsShowMarker" Text="是否显示结点标记" />
                                        </td>
                                        <td>
                                            <asp:RadioButtonList runat="server" ID="rdoListMarkerType" RepeatDirection="horizontal"
                                                RepeatColumns="5">
                                                <asp:ListItem Value="Cross" Text="交点"></asp:ListItem>
                                                <asp:ListItem Value="Square" Text="正方形"></asp:ListItem>
                                                <asp:ListItem Value="Diamond" Text="菱形"></asp:ListItem>
                                                <asp:ListItem Value="Circle" Text="圆形"></asp:ListItem>
                                                <asp:ListItem Value="Triangle" Text="三角形"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkIsShowLabel" Text="是否显示结点标签样式" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;<span id="spanLabelFormat"><asp:Label ID="lblSample" runat="server">示例</asp:Label></span><img
                                                src="../skin/Image/go.gif" style="cursor: hand" onclick="OpenFormatWin();" /><input
                                                    type="hidden" id="hidLabelFormat" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
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
                        <td class="toolBar" runat="server" id="tdbuttonBack">
                            <input class="submitImgButton" id="cmdBack" type="submit" value="上一步" name="cmdBack"
                                runat="server">
                        </td>
                        <td class="toolBar" runat="server" id="tdbuttonPreview">
                            <input class="submitImgButton" id="cmdPreview" type="submit" value="预 览" name="cmdPreview"
                                runat="server">
                        </td>
                        <td class="toolBar" runat="server" id="tdbuttonFinish">
                            <input class="submitImgButton" id="cmdFinish" type="submit" value="完 成" name="cmdFinish"
                                runat="server">
                        </td>
                        <td class="toolBar" runat="server" id="tdbuttonPublish">
                            <input class="submitImgButton" id="cmdPublish" type="submit" value="发 布" name="cmdPublish"
                                runat="server">
                        </td>
                        <td class="toolBar" runat="server" id="tdbuttonSave">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
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
