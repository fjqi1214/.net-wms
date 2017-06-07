<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FSAPStorageInDemandQuery.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FSAPStorageInDemandQuery" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FStorageMP</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript" type="text/javascript">

        function SelectViewField() {
            var result = window.showModalDialog("FViewFieldEP.aspx?defaultUserCode=INVOICESEX_FIELD_LIST_SYSTEM_DEFAULT&table=TBLINVOICESEX", "", "dialogWidth:800px;dialogHeight:600px;scroll:none;help:no;status:no");
            if (result == "OK") {
                window.location.href = window.location.href;
            }
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">SAP入库需求管理</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStorageInTypeQuery" runat="server">入库类型</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="drpStorageInTypeQuery" runat="server" CssClass="textbox" Width="150px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblInvNoQuery" runat="server">SAP单据号</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtInvNoQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCompleteQuery" runat="server">完成</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:DropDownList ID="drpCompleteQuery" runat="server" CssClass="textbox" Width="150px"
                                DESIGNTIMEDRAGDROP="94">
                            </asp:DropDownList>
                        </td>
                        <td nowrap width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblVendorCodeQuery" runat="server">供应商代码</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtVendorCodeQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCreateUserQuery" runat="server">创建人</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="3"  style="width: 159px">
                            <asp:TextBox ID="txtCreateUserQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                CssClass="textbox"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCBDateQuery" runat="server">创建日期开始</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                             <asp:TextBox type="text" id="txtCBDateQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblCEDateQuery" runat="server">创建日期结束</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                             <asp:TextBox type="text" id="txtCEDateQuery"  class='datepicker' runat="server"  Width="130px"/>
                        </td>
                        <td class="fieldValue" nowrap>
                            <a href="#" onclick="SelectViewField(); return false;">
                                <asp:Label runat="server" ID="lblMOSelectMOViewField">选择栏位</asp:Label></a>
                        </td>
                       <td colspan="1" nowrap  width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
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
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                runat="server">
                        </td>
                       <%-- <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server">
                        </td>--%>
                        <td>
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td align="right">
                            <cc1:PagerToolbar id="pagerToolBar" runat="server">
                            </cc1:PagerToolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
