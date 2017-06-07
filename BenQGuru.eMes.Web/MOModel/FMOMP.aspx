<%@ Page Language="c#" CodeBehind="FMOMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FMOMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FErrorCodeMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function ShowDateRange() {
            //debugger;
            if (document.all.chbUseDate.checked == true) {
                document.getElementById("DataRange").style.display = "block";
            }
            else document.getElementById("DataRange").style.display = "none";

        }

        function ShowImportDateRange() {
            //debugger;
            if (document.all.chbImportDate.checked == true) {
                document.getElementById("DateRange").style.display = "block";
            }
            else document.getElementById("DateRange").style.display = "none";

        }

        function Init() {
            ShowDateRange();
            ShowImportDateRange();
        }
        function SelectMOViewField() {
            var result = window.showModalDialog("FMOViewFieldEP.aspx", "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");
            if (result == "OK") {
                window.location.href = window.location.href;
            }
        }
    </script>
</head>
<body onload="Init()">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">工单维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblFactory" runat="server">工厂</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtFactoryQuery" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOCodeQuery" runat="server">工单号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtMoCodeQuery" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtItemCodeQuery" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemNameEditSRM" runat="server">物料描述</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtItemDescription" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOTypeQuery" runat="server">工单类型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpMoTypeQuery" runat="server" Width="100px" CssClass="textbox">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMOStatusQuery" runat="server">生产状态</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpMoStatusQuery" runat="server" Width="100px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblActstarDateFrom" runat="server">已生产天数</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtActstarDateFrom" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblActstarDateTo" runat="server">到</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtActstarDateTo" runat="server" CssClass="textbox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" nowrap onclick="ShowImportDateRange()" align="center">
                            <asp:CheckBox ID="chbImportDate" runat="server" Text="启用导入日期过滤"></asp:CheckBox>
                        </td>
                        <td colspan="4">
                            <table style="display: none" id="DateRange" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblEnterDate" runat="server">导入日期</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox type="text" ID="ImportDateFrom" class='datepicker' runat="server" Width="130px" />
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblTo" runat="server"> 到</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox type="text" ID="ImportDateTo" class='datepicker' runat="server" Width="130px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" nowrap onclick="ShowDateRange()" align="center">
                            <asp:CheckBox ID="chbUseDate" runat="server" Text="启用下发日期过滤"></asp:CheckBox>
                        </td>
                        <td colspan="4">
                            <table style="display: none" id="DataRange" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="fieldNameLeft" nowrap>
                                        <asp:Label ID="lblInDateFromQuery" runat="server">下发日期</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox type="text" ID="dateInDateFromQuery" class='datepicker' runat="server"
                                            Width="130px" />
                                    </td>
                                    <td class="fieldName" nowrap>
                                        <asp:Label ID="lblInDateToQuery" runat="server"> 到</asp:Label>
                                    </td>
                                    <td class="fieldValue">
                                        <asp:TextBox type="text" ID="dateInDateToQuery" class='datepicker' runat="server"
                                            Width="130px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <a href="#" onclick="SelectMOViewField(); return false;">
                                <asp:Label runat="server" ID="lblMOSelectMOViewField">选择栏位</asp:Label></a>
                        </td>
                        <td style="padding-right: 8px" align="right">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick">
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
                <table height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" onserverclick="cmdGridExport_ServerClick">
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" onserverclick="cmdDelete_ServerClick">
                        </td>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server" PageSize="20">
                            </cc1:pagertoolbar>
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
                            <input language="javascript" class="submitImgButton" id="cmdAddImport" type="submit"
                                value="新 增" name="cmdAdd" runat="server" designtimedragdrop="147" onserverclick="cmdDownload_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdRelease" type="submit"
                                value="下 发" name="cmdAdd" runat="server" designtimedragdrop="168" onserverclick="cmdRelease_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdInitial" type="submit"
                                value="取消下发" name="cmdAdd" runat="server" onserverclick="cmdInitial_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdPending" type="submit"
                                value="暂 停" name="cmdAdd" runat="server" designtimedragdrop="228" onserverclick="cmdPending_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdAnnulPending" type="submit"
                                value="取消暂停" name="cmdAdd" runat="server" designtimedragdrop="170" onserverclick="cmdMOOpen_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdCloseMO" type="submit"
                                value="关 单" name="cmdAdd" runat="server" onserverclick="cmdMOClose_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdExport" type="submit" value="导出" name="cmdExport"
                                runat="server" onserverclick="cmdMOExport_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdEnter" type="submit" value="导 入" name="cmdEnter"
                                runat="server" onserverclick="cmdImport_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
