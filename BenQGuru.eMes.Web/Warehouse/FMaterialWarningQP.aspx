<%@ Page Language="c#" Codebehind="FMaterialWarningQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FMaterialWarningQP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FMaterialWarningQP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table style="height: 100%; width: 100%;">
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">FMaterialWarningQP</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="query" style="height: 100%; width: 100%;">
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblBigSSCodeQuery" runat="server">大线代码</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <cc2:selectabletextbox id="txtBigSSCodeQuery" runat="server" Type="bigline" Readonly="false" CanKeyIn="true" Width="131px"></cc2:selectabletextbox></td>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblDateQuery" runat="server">日期</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <uc1:eMESDate id="datDateQuery" CssClass="textbox" width="90" runat="server"></uc1:eMESDate>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblStatusQuery" runat="server">状态</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:DropDownList ID="ddlStatusQuery" runat="server" CssClass="textbox" Width="90px"></asp:DropDownList></td>                                                                
                            <td style="width:100%;">
                            </td>
							<td noWrap style="width:100px;text-align:right">
								<asp:CheckBox id="chbRefreshAuto" runat="server" AutoPostBack="True" Text="定时刷新" oncheckedchanged="chkRefreshAuto_CheckedChanged"></asp:CheckBox>
								<cc1:RefreshController id="RefreshController1" runat="server" Interval="30000"></cc1:RefreshController>
							</td>                            
                            <td class="fieldName">
                                <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                    runat="server" onserverclick="cmdQuery_ServerClick" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 100%;">
                <td class="fieldGrid">
                    <igtbl:UltraWebGrid ID="gridWebGrid" runat="server" Height="100%" Width="100%">
                        <DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes"
                            RowHeightDefault="20px" Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single"
                            HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                            CellPaddingDefault="4" RowSelectorsDefault="No" Name="webGrid" TableLayout="Fixed">
                            <AddNewBox>
                                <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray" />
                            </AddNewBox>
                            <Pager>
                                <Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray" />
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
                    <table cellpadding="0" style="height: 100%; width: 100%;">
                        <tr>
                            <td class="smallImgButton">
                                <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
                                    runat="server" onserverclick="cmdGridExport_ServerClick" />
                                |
                            </td>
                            <td>
                                <cc1:pagersizeselector id="pagerSizeSelector" runat="server">
                                </cc1:pagersizeselector></td>
                            <td align="right">
                                <cc1:pagertoolbar id="pagerToolBar" runat="server" pagesize="20">
                                </cc1:pagertoolbar></td>
                        </tr>
                    </table>
                </td>
            </tr>            
        </table>
    </form>
</body>
</html>
