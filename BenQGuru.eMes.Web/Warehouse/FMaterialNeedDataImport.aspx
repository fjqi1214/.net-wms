<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FMaterialNeedDataImport.aspx.cs"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FMaterialNeedDataImport" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="ignav" Namespace="Infragistics.WebUI.UltraWebNavigator" Assembly="Infragistics35.WebUI.UltraWebNavigator.v3.2, Version=3.2.20042.26, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>工单导入</title>
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
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">要料导入</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="query" height="100%" width="100%">
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblInitFileQuery" runat="server"> 文件路径</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <input type="file" runat="server" id="fileInit" style="width: 454px" class="textbox"
                                    name="fileInit"></td>
                            <td nowrap width="100%">
                            </td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdEnter" type="submit" value="导 入" name="cmdSave"
                                    runat="server" onserverclick="cmdImport_ServerClick"></td>
                            <td>
                                <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                    runat="server" onserverclick="cmdReturn_ServerClick"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="fieldGrid">
                    <igtbl:UltraWebGrid ID="gridWebGrid" Visible="false" runat="server" Width="100%" Height="100%">
                        <DisplayLayout TableLayout="Fixed" Name="webGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
                            AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle"
                            SelectTypeCellDefault="Single" SelectTypeRowDefault="Single" Version="2.00" RowHeightDefault="20px"
                            AllowSortingDefault="Yes" StationaryMargins="Header" ColWidthDefault="" >
                            <AddNewBox>
                                <Style BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></Style>
                            </AddNewBox>
                            <Pager>
                                <Style BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px"></Style>
                            </Pager>
                            <HeaderStyleDefault BackColor="#ABABAB" BorderStyle="Dashed" BorderWidth="1px" HorizontalAlign="Left"
                                BorderColor="White" Font-Bold="True" Font-Size="12px">
                                <BorderDetails ColorLeft="White" ColorRight="White" WidthTop="1px" ColorBottom="White"
                                    WidthLeft="1px" ColorTop="White"></BorderDetails>
                            </HeaderStyleDefault>
                            <RowSelectorStyleDefault BackColor="#EBEBEB">
                            </RowSelectorStyleDefault>
                            <FrameStyle Height="100%" Width="100%" BorderStyle="Groove" BorderWidth="0px" BorderColor="#ABABAB"
                                Font-Size="12px" Font-Names="Verdana">
                            </FrameStyle>
                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Groove" BorderWidth="0px">
                                <BorderDetails ColorLeft="White" WidthTop="1px" WidthLeft="1px" ColorTop="White"></BorderDetails>
                            </FooterStyleDefault>
                            <ActivationObject BorderStyle="Dotted">
                            </ActivationObject>
                            <EditCellStyleDefault BorderStyle="None" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle">
                                <Padding Bottom="1px"></Padding>
                            </EditCellStyleDefault>
                            <RowAlternateStyleDefault BackColor="White">
                            </RowAlternateStyleDefault>
                            <RowStyleDefault BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" BorderColor="#D7D8D9"
                                VerticalAlign="Middle">
                                <Padding Left="3px"></Padding>
                                <BorderDetails WidthTop="0px" WidthLeft="0px"></BorderDetails>
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
                                <cc1:PagerSizeSelector ID="pagerSizeSelector" runat="server" Visible="false"></cc1:PagerSizeSelector>
                            </td>
                            <td align="right">
                                <cc1:PagerToolBar ID="pagerToolBar" runat="server" Visible="false"></cc1:PagerToolBar>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="toolBar">
                <td>
                    <table class="toolBar">
                        <tr>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
