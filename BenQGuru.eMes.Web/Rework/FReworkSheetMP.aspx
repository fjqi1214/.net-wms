<%@ Page Language="c#" CodeBehind="FReworkSheetMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.Rework.FReworkSheetMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FReworkSheetMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body scroll="yes" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">返工需求单维护</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblModelQuery" runat="server">产品别</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpModelQuery" runat="server" Width="130px" OnLoad="drpModelQuery_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemQuery" runat="server">产品</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtItemCodeQuery" runat="server" CssClass="textbox" Width="130px"
                                DESIGNTIMEDRAGDROP="181"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOQuery" runat="server">工单</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox ID="txtMOCodeQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldValue" width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReworkCode" runat="server" DESIGNTIMEDRAGDROP="102">需求单号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtReworkSheetCodeQuery" runat="server" CssClass="textbox" Width="130px"
                                DESIGNTIMEDRAGDROP="104"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReworkStatus" runat="server">需求单状态</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpReworkSheetStatusQuery" runat="server" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblLotNum" runat="server">批号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtLotNO" runat="server" type="lot" target=" " cssclass="textbox"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldValue" width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaintainBegdate" runat="server">维护起始日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <asp:TextBox type="text" ID="txtMaintainBeginDate" class='datepicker' runat="server"
                                            Width="130px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaintainEnddate" runat="server">维护结束日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <asp:TextBox type="text" ID="txtMaintainEndDate" class='datepicker' runat="server"
                                            Width="130px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReworkDateEdit" runat="server">判退时间</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <asp:TextBox type="text" ID="txtReworkDateEdit" class='datepicker' runat="server"
                                            Width="130px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldValue" width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblBigSSCodeWhere" runat="server">大线</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtBigSSCodeWhere" runat="server" type="bigline" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMaterialModelCodeWhere" runat="server">机型</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtMaterialModelCodeWhere" runat="server" type="mmodelcode"
                                readonly="false" cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblDutyCodeQuery" runat="server">责任别</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtDutyCodeQuery" runat="server" type="duty" readonly="false"
                                cankeyin="true" width="130px">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldValue" width="100%">
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblReTypeQuery" runat="server">返工类型</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:DropDownList ID="drpReworkType" runat="server" CssClass="textbox" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldValue">
                        </td>
                        <td class="fieldValue">
                        </td>
                        <td class="fieldValue">
                        </td>
                        <td class="fieldValue">
                        </td>
                        <td align="right">
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
                            <cc1:pagertoolbar id="pagerToolBar" runat="server">
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
                            <input class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd"
                                runat="server" onserverclick="cmdAdd_ServerClick">
                        </td>
                        <td class="toolBar" style="display: none">
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdRelease" type="submit" value="下 发" name="cmdOpen"
                                runat="server" onserverclick="cmdOpen_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdInitial" type="submit" value="取消下发" name="cmdCancelOpen"
                                runat="server" onserverclick="cmdInitial_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdWait" type="submit" value="等 待" name="cmdCancel"
                                runat="server" onserverclick="cmdWait_ServerClick">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdBillClose" type="submit" value="结 单" name="cmdAdd"
                                runat="server" onserverclick="cmdMOClose_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
