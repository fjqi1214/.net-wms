<%@ Page Language="c#" CodeBehind="FTSInfoQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FTSInfoQP" %>

<%@ Register TagPrefix="uc2" TagName="UpDown" Src="~/UserControl/NumericUpDown/UCNumericUpDown.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register Src="UserControls/UCPie3DChartProcess.ascx" TagName="UCPie3DChartProcess"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/UCParetoChartProcess.ascx" TagName="UCParetoChartProcess"
    TagPrefix="uc4" %>
<%@ Register Assembly="Infragistics35.Web.v11.2, Version=11.2.20112.1019, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FTSRecordQP</title>
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
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">维修资料统计</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="query" height="100%" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblGoodSemiGoodGroup" runat="server">成品/半成品</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpFinishSemimanuProductQuery" runat="server" CssClass="textbox"
                                Width="130px" AutoPostBack="False" OnLoad="drpFinishSemimanuProduct_Load">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblItemCodeQuery" runat="server">产品代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtConditionItem" runat="server" Width="130px" Type="item"
                                CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtConditionMo" runat="server" Width="130px" Type="mo"
                                CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSummaryObjectQuery" runat="server">不良代码组</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtErrorCodeGroup" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblNGCodeQuery" runat="server">不良代码</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtErrorCode" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblNGLocationQuery" runat="server">不良位置</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtErrorLocation" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblErrorCauseGroupCodeQuery" runat="server">不良原因组</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtErrorCauseGroup" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblNGReasonQuery" runat="server">不良原因</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtErrorCause" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblNGDutyQuery" runat="server">责任别</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtErrorDuty" runat="server" CssClass="textbox"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblStartDateQuery" runat="server">起始日期</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox type="text" ID="dateStartDateQuery" class='datepicker' runat="server"
                                Width="130px" />
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblEndDateQuery" runat="server">结束日期</asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox type="text" ID="dateEndDateQuery" class='datepicker' runat="server"
                                Width="130px" />
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblOriginQuery" runat="server">来源站</asp:Label>
                        </td>
                        <td>
                            <cc2:selectabletextbox id="txtFromResource" runat="server" Type="resource" Width="130px"
                                CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblFirstClassGroup" runat="server">一级分类</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpFirstClassQuery" runat="server" CssClass="textbox" Width="130px"
                                AutoPostBack="true" OnLoad="drpFirstClass_Load" OnSelectedIndexChanged="drpFirstClass_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblSecondClassGroup" runat="server">二级分类</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpSecondClassQuery" runat="server" CssClass="textbox" Width="130px"
                                AutoPostBack="true" OnSelectedIndexChanged="drpSecondClass_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblThirdClassGroup" runat="server">三级分类</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:DropDownList ID="drpThirdClassQuery" runat="server" CssClass="textbox" Width="130px"
                                AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblLotNoQuery" runat="server">送检批</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <cc2:selectabletextbox id="txtLotNo" runat="server" Width="130px" Type="lot" CanKeyIn="true">
                            </cc2:selectabletextbox>
                        </td>
                        <td class="fieldName" nowrap>
                            <asp:Label ID="lblErrorcomponentQuery" runat="server">不良组件</asp:Label>
                        </td>
                        <td class="fieldValue">
                            <asp:TextBox ID="txtErrorcomponentQuery" runat="server" CssClass="textbox"></asp:TextBox>
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
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblSummaryTarget" runat="server">统计对象</asp:Label>
                        </td>
                        <td colspan="8">
                            <asp:RadioButtonList ID="rblSummaryTargetQuery" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True" OnSelectedIndexChanged="rblSummaryTargetQuery_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblTopQuery" runat="server">排名筛选</asp:Label>
                        </td>
                        <td colspan="6">
                            <uc2:UpDown id="upDown" width="110" runat="server">
                            </uc2:UpDown>
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server">
                        </td>
                        <td>
                            &nbsp;
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
        <tr>
            <td>
                <table>
                    <tr>
                        <td align="center">
                            <uc4:UCParetoChartProcess id="paretoChart" runat="server">
                            </uc4:UCParetoChartProcess>
                        </td>
                        <tr>
                            <td align="center">
                                <uc3:UCPie3DChartProcess id="pie3DChart" runat="server">
                                </uc3:UCPie3DChartProcess>
                            </td>
                        </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
