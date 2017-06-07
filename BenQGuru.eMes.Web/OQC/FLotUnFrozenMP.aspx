<%@ Page Language="c#" CodeBehind="FLotUnfrozenMP.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.OQC.FLotUnfrozenMP" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="../UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FLotUnFrozenMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 批隔离处理 </asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblLotNoQuery" runat="server"> 批号 </asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtLotNoQuery" runat="server" CssClass="textbox" Width="130px"></asp:TextBox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblMaterialModelCodeGroup" runat="server">整机机型</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:selectabletextbox id="txtModelcode" runat="server" CanKeyIn="true" Type="mmodelcode"
                                Target="">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblss" runat="server">产线</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:selectabletextbox id="txtStepsequence" runat="server" CanKeyIn="true" Type="stepsequence"
                                Target="">
                            </cc2:selectabletextbox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" style="height: 26px" nowrap>
                            <asp:Label ID="lblBigSSCodeGroup" runat="server">大线</asp:Label>
                        </td>
                        <td class="fieldValue" style="height: 26px">
                            <cc2:selectabletextbox id="txtBIGLine" runat="server" CanKeyIn="true" Type="bigline"
                                Target="">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStartDateQuery" runat="server">起始日期</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox type="text" ID="txtDateFrom" class='datepicker' runat="server" Width="130px" />
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEndDateQuery" runat="server">结束日期</asp:Label>
                        </td>
                        <td class="fieldValue" nowrap>
                            <asp:TextBox type="text" ID="txtDateTo" class='datepicker' runat="server" Width="130px" />
                        </td>
                        <td width="100%">
                        </td>
                        <td class="fieldName">
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
                                runat="server" onserverclick="cmdGridExport_ServerClick" />
                        </td>
                        <td class="smallImgButton">
                            <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                runat="server" onserverclick="cmdDelete_ServerClick" visible="false" />
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
        <tr class="normal">
            <td>
                <table class="edit" height="100%" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblUnfrozenCauseEdit" runat="server" Style="width: 60px">取消隔离原因</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="3">
                            <asp:TextBox ID="txtUnfrozenCauseEdit" runat="server" CssClass="textbox" Width="500px"
                                Height="80px" MaxLength="50" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </td>
                        <td style="width: 100%">
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
                                runat="server" onserverclick="cmdAdd_ServerClick" visible="false">
                        </td>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="取消隔离" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick" visible="false">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
