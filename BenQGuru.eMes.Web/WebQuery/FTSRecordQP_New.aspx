<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FTSRecordQP_New.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WebQuery.FTSRecordQP_New" %>

<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FTSRecordQP_New</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" height="100%" width="100%" runat="server">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">维修记录查询</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblGoodSemiGoodWhere" runat="server">成品/半成品</asp:Label>
                        </td>
                        <td class="fieldValue" width="150">
                            <asp:DropDownList runat="server" ID="drpFGorSemiFG" Width="150" OnLoad="drpFGorSemiFG_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue" width="150">
                            <cc2:selectabletextbox id="txtConditionItem" runat="server" Type="item" Width="150"
                                CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                        </td>
                        <td class="fieldValue" width="150">
                            <cc2:selectabletextbox id="txtConditionMo" runat="server" Type="mo" Width="150" CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStartSnQuery" runat="server">起始序列号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtStartSnQuery" runat="server" CssClass="require" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblEndSnQuery" runat="server">结束序列号</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtEndSnQuery" runat="server" CssClass="require" Width="150px"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblReworkMo" runat="server">返工需求单代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtReworkMo" runat="server" Type="reworkmo" Width="150"
                                CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSegment" runat="server">工段</asp:Label>
                        </td>
                        <td class="fieldValue" width="150">
                            <cc2:SelectableTextbox id="txtSegmentCodeQuery" runat="server" width="150" Type="singlesegment"
                                Target="" autopostback="true" CanKeyIn="true">
                            </cc2:SelectableTextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStepSequence" runat="server">生产线</asp:Label>
                        </td>
                        <td class="fieldValue" width="150">
                            <cc2:selectabletextbox4SS id="txtStepSequence" runat="server" Width="150px" Type="stepsequence"
                                CanKeyIn="true">
                            </cc2:selectabletextbox4SS>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblOPCodeEdit" runat="server">工序代码</asp:Label>
                        </td>
                        <td class="fieldValue" width="150">
                            <cc2:selectabletextbox id="txtOPCodeEdit" runat="server" Width="150px" Type="singleopwithoutroute"
                                Target="" CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblNGStartDateQuery" runat="server">不良起始日期</asp:Label>
                        </td>
                        <td class="fieldValue" width="150" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                    <asp:TextBox  id="dateStartDateQuery"  class='datepicker' runat="server"  Width="110px"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblNGEndDateQuery" runat="server">不良结束日期</asp:Label>
                        </td>
                        <td class="fieldValue" width="150" nowrap>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                 <asp:TextBox  id="dateEndDateQuery"  class='datepicker' runat="server"  Width="110px"/>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblBigSSCodeGroup" runat="server">大线</asp:Label>
                        </td>
                        <td class="fieldValue" width="150">
                            <cc2:selectabletextbox id="txtBigSSCodeGroupQuery" runat="server" Width="150px" Type="bigline"
                                Target="" CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaterialModelCodeQuery" runat="server">整机机型</asp:Label>
                        </td>
                        <td class="fieldValue" width="150">
                            <cc2:SelectableTextbox id="txtMaterialModelCodeQuery" runat="server" width="150"
                                Type="mmodelcode" Target="" autopostback="true" CanKeyIn="true">
                            </cc2:SelectableTextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblMaterialMachineTypeGroup" runat="server">整机机芯</asp:Label>
                        </td>
                        <td class="fieldValue" width="150">
                            <cc2:SelectableTextbox id="txtMaterialMachineTypeGroup" runat="server" Width="150px"
                                Type="mmachinetype" CanKeyIn="true">
                            </cc2:SelectableTextbox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                        </td>
                        <td class="fieldValue">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTSStateQuery" runat="server">维修状态</asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:CheckBoxList ID="chkTSStateList" runat="server" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                        <td>
                        </td>
                        <td class="fieldNameLeft">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="cmdQuery"
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
                        <td width="140">
                            <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                            </cc1:pagersizeselector>
                        </td>
                        <td class="smallImgButton">
                            <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                runat="server">
                        </td>
                        <td>
                            <asp:CheckBox ID="chbRepairDetail" runat="server" Text="导出维修明细"></asp:CheckBox>
                        </td>
                        <td align="right">
                            <cc1:pagertoolbar id="pagerToolBar" runat="server">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
