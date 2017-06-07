<%@ Page Language="c#" Codebehind="_StandardMaintainPageDetail.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.BaseSetting._StandardMaintainPageDetail" %>

<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>标准维护界面子页面（以用户组下的用户为例）</title>
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
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">标准维护界面子页面（以用户组下的用户为例）</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="query" style="height: 100%; width: 100%;">
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblUserGroupCodeQuery" runat="server">用户组代码</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtUserGroupCodeQuery" runat="server" Width="150px" CssClass="textbox"
                                    ReadOnly="True"></asp:TextBox></td>
                            <td nowrap="nowrap" width="100%">
                            </td>
                            <td class="fieldName">
                                <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                    runat="server" onserverclick="cmdQuery_ServerClick" visible="false"/></td>
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
                            <td>
                                <asp:CheckBox ID="chbSelectAll" runat="server" Width="124px" AutoPostBack="True"
                                    Text="全选" OnCheckedChanged="chbSelectAll_CheckedChanged"></asp:CheckBox></td>
                            <td class="smallImgButton">
                                <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
                                    runat="server" onserverclick="cmdGridExport_ServerClick" />
                                |
                            </td>
                            <td class="smallImgButton">
                                <input class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete"
                                    runat="server" onserverclick="cmdDelete_ServerClick" />
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
            <tr class="toolBar">
                <td>
                    <table class="toolBar">
                        <tr>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
                                    runat="server" onserverclick="cmdReturn_ServerClick" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
