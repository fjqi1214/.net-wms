<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FMaterialWorkPlan.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FMaterialWorkPlan" %>

<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>生产计划信息维护</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table height="100%" width="100%">
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">生产计划信息维护</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="query" height="100%" width="100%">
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblPlanDateFrom" runat="server">计划日期从</asp:Label></td>
                            <td class="fieldValue" style="width: 132px">
                                <uc1:eMESDate ID="dateVoucherDateFrom" CssClass="textbox" Width="130" runat="server">
                                </uc1:eMESDate>
                            </td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblZ" runat="server">到</asp:Label></td>
                            <td class="fieldValue" style="width: 132px">
                                <uc1:eMESDate ID="dateVoucherDateTo" CssClass="textbox" Width="130" runat="server"></uc1:eMESDate>
                            </td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblBigSSCodeWhere" runat="server">大线</asp:Label></td>
                            <td class="fieldValue">
                                <cc2:SelectableTextBox ID="txtBigSSCodeWhere" runat="server" Type="bigline" Readonly="false"
                                    CanKeyIn="true" Width="130px"></cc2:SelectableTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblMOIDQuery" runat="server">工单代码</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtConditionMo" runat="server" Width="130px" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblAactionStatus" runat="server">执行状态</asp:Label></td>
                            <td class="fieldValue">
                                <asp:DropDownList ID="drpAactionStatus" runat="server" Width="130px">
                                </asp:DropDownList></td>
                            <td class="fieldName" nowrap>
                                <asp:Label ID="lblMaterialStatus" runat="server">配料状态</asp:Label></td>
                            <td class="fieldValue">
                                <asp:DropDownList ID="drpMaterialStatus" runat="server" Width="130px">
                                </asp:DropDownList></td>
                            <td align="center" colspan="2">
                                <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                    tabindex="0" runat="server"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="fieldGrid">
                    <igtbl:UltraWebGrid ID="gridWebGrid" runat="server" Width="100%" Height="100%">
                        <DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes"
                            RowHeightDefault="20px" Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single"
                            HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                            CellPaddingDefault="4" RowSelectorsDefault="No" Name="webGrid" TableLayout="Fixed">
                            <AddNewBox>
                                <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
                            </AddNewBox>
                            <Pager>
                                <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
                            </Pager>
                            <HeaderStyleDefault BorderWidth="1px" Font-Size="12px" Font-Bold="True" BorderColor="White"
                                BorderStyle="Dashed" HorizontalAlign="Left" BackColor="#ABABAB">
                                <BorderDetails ColorTop="White" WidthLeft="1px" ColorBottom="White" WidthTop="1px"
                                    ColorRight="White" ColorLeft="White"></BorderDetails>
                            </HeaderStyleDefault>
                            <RowSelectorStyleDefault BackColor="#EBEBEB">
                            </RowSelectorStyleDefault>
                            <FrameStyle Width="100%" BorderWidth="0px" Font-Size="12px" Font-Names="Verdana"
                                BorderColor="#ABABAB" BorderStyle="Groove" Height="100%">
                            </FrameStyle>
                            <FooterStyleDefault BorderWidth="0px" BorderStyle="Groove" BackColor="LightGray">
                                <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
                            </FooterStyleDefault>
                            <ActivationObject BorderStyle="Dotted">
                            </ActivationObject>
                            <EditCellStyleDefault VerticalAlign="Middle" BorderWidth="1px" BorderColor="Black"
                                BorderStyle="None">
                                <Padding Bottom="1px"></Padding>
                            </EditCellStyleDefault>
                            <RowAlternateStyleDefault BackColor="White">
                            </RowAlternateStyleDefault>
                            <RowStyleDefault VerticalAlign="Middle" BorderWidth="1px" BorderColor="#D7D8D9" BorderStyle="Solid"
                                HorizontalAlign="Left">
                                <Padding Left="3px"></Padding>
                                <BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
                            </RowStyleDefault>
                            <ImageUrls ImageDirectory="/ig_common/WebGrid3/"></ImageUrls>
                        </DisplayLayout>
                        <Bands>
                            <igtbl:UltraGridBand>
                            </igtbl:UltraGridBand>
                        </Bands>
                    </igtbl:UltraWebGrid></td>
            </tr>
            <tr class="normal">
                <td>
                    <table height="100%" cellpadding="0" width="100%">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chbSelectAll" runat="server" AutoPostBack="True" Text="全选"></asp:CheckBox></td>
                            <td class="smallImgButton">
                                <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                    runat="server">
                                |
                            </td>
                            <td class="smallImgButton">
                                <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                    runat="server">
                                |
                            </td>
                            <td>
                                <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server"></cc1:PagerSizeSelector>
                            </td>
                            <td align="right">
                                <cc1:PagerToolBar ID="pagerToolBar" runat="server"></cc1:PagerToolBar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="normal">
                <td align="center">
                    <table class="edit" height="100%" cellpadding="0" width="50%">
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblDate" runat="server">日期</asp:Label></td>
                            <td class="fieldValue" style="width: 132px">
                                <uc1:eMESDate ID="dateDateFrom" CssClass="require" Width="130" runat="server"></uc1:eMESDate>
                            </td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblBigSSCodeGroup" runat="server">大线</asp:Label></td>
                            <td class="fieldValue">
                                <cc2:SelectableTextBox ID="txtBigSSCodeGroup" runat="server" CssClass="require" Type="singlebigss" Readonly="false"
                                    CanKeyIn="false" Width="130px"></cc2:SelectableTextBox>
                            </td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMactureSeq" runat="server">生产顺序</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtMactureSeq" MaxLength="40" runat="server" Width="120px" CssClass="require"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMOEdit" runat="server">工单</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtMOEdit" MaxLength="40" runat="server" Width="130px" CssClass="require"></asp:TextBox></td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMOSeqEdit" runat="server">工单项次</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtMOSeqEdit" MaxLength="40" runat="server" Width="130px" CssClass="require"></asp:TextBox></td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblPlanInQTYEdit" runat="server">计划投入数量</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtPlanInQTYEdit" MaxLength="40" runat="server" Width="120px" CssClass="require"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblPlanInStartDateEdit" runat="server">计划开始时间</asp:Label></td>
                            <td class="fieldValue">
                               <uc1:emestime id="timeFrom" runat="server" CssClass="require" width="130"></uc1:emestime></td>
                             <td> <asp:TextBox ID="txtAct" MaxLength="40" runat="server" Width="0px" CssClass="require"></asp:TextBox></td>
                             <td> <asp:TextBox ID="txtMat" MaxLength="40" runat="server" Width="0px" CssClass="require"></asp:TextBox></td>
                              <td> <asp:TextBox ID="txtActQty" MaxLength="40" runat="server" Width="0px" CssClass="require"></asp:TextBox></td>
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
                                    runat="server"></td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                    runat="server"></td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                    runat="server"></td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdEnter" type="submit" value="导 入" name="cmdEnter"
                                    runat="server" onserverclick="cmdImport_ServerClick"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
