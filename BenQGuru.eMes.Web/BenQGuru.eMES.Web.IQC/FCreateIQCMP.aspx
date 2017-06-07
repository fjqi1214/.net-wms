<%@ Page Language="c#" CodeBehind="FCreateIQCMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.IQC.FCreateIQCMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FCreateIQCMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
    <script type="text/javascript">
        function InitPageWhenLoad() {
            document.getElementById("cmdCreateIQCFromASN").disabled = false;
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 65px;
        }
        .style2
        {
            width: 180px;
        }
        .style3
        {
            width: 138px;
        }
    </style>
</head>
<body onload="InitPageWhenLoad()">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">生成打印IQC送检单</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" style="height: 100%; width: 100%">
                    <tr>
                        <td class="style1" nowrap="noWrap">
                            <asp:Label ID="lblReceiptNOEdit" runat="server">入库单号</asp:Label>
                        </td>
                        <td class="style2" nowrap="noWrap">
                            &nbsp&nbsp&nbsp<asp:TextBox ID="txtASNEdit" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="style3">
                            <input  class="submitImgButtonSmallerFont" id="cmdCreateIQCFromASN" type="submit" value="产生IQC送检单"
                                name="btnCreateIQCFromASN" runat="server" onserverclick="cmdCreateIQCFromASN_ServerClick" />
                        </td>
                        <td style="width: 10%">
                        </td>
                        <td class="toolBar">
                        </td>
                        <td style="width: 10%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" style="height: 100%; width: 100%">
                    <tr>
                        <td class="fieldName" nowrap="noWrap">
                            <asp:Label ID="lblIQCNoQuery" runat="server">IQC送检单号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap">
                            <asp:TextBox ID="txtIQCNoQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap="noWrap">
                            <asp:Label ID="lblReceiptNOEditQuery" runat="server">入库单号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap">
                            <asp:TextBox ID="txtASNPOQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap="noWrap">
                            <asp:Label ID="lblIQCStatusQuery" runat="server">状态</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap">
                            <asp:DropDownList ID="drpIQCStatusQuery" runat="server" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap="noWrap">
                            <asp:Label ID="lblVendorCodeQuery" runat="server">供应商代码</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap">
                            <asp:TextBox ID="txtVendorCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap="noWrap">
                            <asp:Label ID="lblROHSQuery" runat="server">ROHS</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap">
                            <asp:DropDownList ID="drpROHSQuery" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap="noWrap">
                            <asp:Label ID="lblShipToStockQuery" runat="server">免检</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap">
                            <asp:DropDownList ID="drpShipToStockQuery" runat="server" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                            <asp:Label ID="lblAppDateFromQuery" runat="server">送检日期从</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px">                  
  <asp:TextBox type="text" id="datAppDateFromQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                            <asp:Label ID="lblAppDateToQuery" runat="server">到</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px">
                        
  <asp:TextBox type="text" id="datAppDateToQuery"  class='datepicker' runat="server"  Width="130px"/>

                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: 100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 100%">
            <td class="fieldGrid">
                <ig:WebDataGrid ID="gridWebGrid" runat="server" Width="100%">
                </ig:WebDataGrid>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table cellpadding="0" style="height: 100%; width: 100%">
                    <tr>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                runat="server" onserverclick="cmdGridExport_ServerClick" />
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" onserverclick="cmdDelete_ServerClick" />
                        </td>
                        <td>
                            <cc1:PagerSizeSelector id="pagerSizeSelector" runat="server">
                            </cc1:PagerSizeSelector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolBar id="pagerToolBar" runat="server" PageSize="20">
                            </cc1:PagerToolBar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table class="edit" cellpadding="0" style="height: 100%; width: 100%">
                    <tr>
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                            <asp:Label ID="lblIQCNoEdit" runat="server">IQC送检单号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px">
                            <asp:TextBox ID="txtIQCNoEdit" runat="server" CssClass="require" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap="noWrap" style="height: 26px">
                            <asp:Label ID="lblIQCHeadAttributeEdit" runat="server">类型</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap="noWrap" style="height: 26px">
                            <asp:DropDownList ID="drpIQCHeadAttributeEdit" runat="server" CssClass="require"
                                Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 92%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="toolBar">
            <td style="height: 15px">
                <table class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSendCheck" type="submit" value="送 检" name="cmdSendCheck"
                                runat="server" onserverclick="cmdSendCheck_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdBack" type="submit" value="撤 回" name="cmdBack"
                                runat="server" onserverclick="cmdBack_ServerClick" />
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdIQCPrint" type="submit" value="打 印" name="cmdIQCPrint"
                                runat="server" onserverclick="cmdIQCPrint_ServerClick" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
