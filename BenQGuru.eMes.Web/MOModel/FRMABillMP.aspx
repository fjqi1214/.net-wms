<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>

<%@ Page Language="c#" CodeBehind="FRMABillMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.FRMABillMP" %>

<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FRMABillMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script language="javascript">
        function ShowDateRange() {
            //debugger;
            if (document.all.UseDateCheckBox.checked == true) {
                document.getElementById("DataRange").style.display = "block";
            }
            else document.getElementById("DataRange").style.display = "none";

        }

        function ShowImportDateRange() {
            //debugger;
            if (document.all.ImportDateCheckbox.checked == true) {
                document.getElementById("DateRange").style.display = "block";
            }
            else document.getElementById("DateRange").style.display = "none";

        }

        function Init() {
            ShowDateRange();
            ShowImportDateRange();
        }
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="lblbill" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">RMA单据维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblRMABillNo" runat="server">RMA单号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtRMABillQuery" runat="server" type="rmabill" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblModelCodeQuery" runat="server">产品别代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtModelQuery" runat="server" type="model" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtItemQuery" runat="server" type="item" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblCusCodeQuery" runat="server">客户代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtCusCodeQuery" runat="server" CssClass="textbox" Width="130px"
                                MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSubsidiaryCompanyQuery" runat="server">分公司</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtSubsidiaryCompanyQuery" runat="server" CssClass="textbox" Width="130px"
                                MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                        </td>
                        <td class="fieldValue">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMDateQuery" runat="server">维护日期</asp:Label>
                        </td>
                        <td class="fieldValue"  nowrap>
                            <asp:TextBox type="text" ID="MDateFrom" class='datepicker' runat="server" Width="130px" />
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblTo" runat="server"> 到</asp:Label>
                        </td>
                        <td class="fieldValue"  nowrap>
                            <asp:TextBox type="text" ID="MDateTo" class='datepicker' runat="server" Width="130px" />
                        </td>
                        <td class="fieldName" nowrap>
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
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server" PageSize="50">
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
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblRMABillCode" runat="server">RMA单号</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtRMABillEidt" runat="server" CssClass="require" Width="130px"
                                MaxLength="40"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMemoEdit" runat="server">备注</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtMemoEdit" runat="server" CssClass="textbox" Width="130px" MaxLength="100"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
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
                            <input language="javascript" class="submitImgButton" id="cmdAdd" type="submit" value="新增"
                                name="cmdAdd" runat="server" designtimedragdrop="147" onserverclick="cmdAdd_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdDistribution" type="submit"
                                value="下发" name="cmdDistribution" runat="server" designtimedragdrop="147" onserverclick="cmdDistribution_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input language="javascript" class="submitImgButton" id="cmdClose" type="submit"
                                value="结案" name="cmdAdd" runat="server" designtimedragdrop="168" onserverclick="cmdClose_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
