<%@ Page Language="c#" Codebehind="_StandardMaintainPage.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.BaseSetting._StandardMaintainPage" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>标准维护界面（以产品别为例）</title>
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
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">标准维护界面（以产品别为例）</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="query" style="height: 100%; width: 100%;">
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblModelCodeQuery" runat="server">产品别代码</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtModelCodeQuery" runat="server" CssClass="textbox" Width="150px"></asp:TextBox></td>
                            <td style="width:100%;">
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
                            <td>
                                <asp:CheckBox ID="chbSelectAll" runat="server" Width="124px" AutoPostBack="True"
                                    Text="全选" OnCheckedChanged="chbSelectAll_CheckedChanged"></asp:CheckBox></td>
                            <td class="smallImgButton">
                                <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdCancel"
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
            <tr class="normal">
                <td>
                    <table class="edit" cellpadding="0" style="height: 100%; width: 100%;">
                        <tr>
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblOrgEdit" runat="server">组织</asp:Label></td>
                            <td class="fieldValue">
                                <asp:DropDownList ID="ddlOrgEdit" runat="server" CssClass="require" Width="150px">
                                </asp:DropDownList></td>                        
                            <td class="fieldNameLeft" nowrap="nowrap">
                                <asp:Label ID="lblModelCodeEdit" runat="server">产品别代码</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtModelCodeEdit" runat="server" CssClass="require" Width="150px"></asp:TextBox></td>
                            <td class="fieldNameLeft" style="height: 26px" nowrap="nowrap">
                                <asp:Label ID="lblModelDescriptionEdit" runat="server">产品别描述</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                <asp:TextBox ID="txtModelDescriptionEdit" runat="server" CssClass="textbox" Width="150px"></asp:TextBox></td>
                            <td>
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
                                    runat="server" onserverclick="cmdAdd_ServerClick" /></td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                    runat="server" onserverclick="cmdSave_ServerClick" /></td>
                            <td>
                                <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                    runat="server" onserverclick="cmdCancel_ServerClick" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
