<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FMaterialSendMP.aspx.cs" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FMaterialSendMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="~/UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>FMaterialReveiceMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">

    <script language="javascript">
	    function gridWebGrid_ClickCellButtonHandler(gridName, cellId)
	    {
	        var msg = "";
	        var column = igtbl_getColumnById(cellId);
            if(column.Key == "SyncStatus")
            {
                msg = "确定要同步状态吗？";
            }
            
            if(column.Key == "ConfirmReceiveAgain")
            {
                msg = "确定要再次报帐吗？";
            }
            var result = false;
            if(msg != "")
            {
               result = !window.confirm(msg);
            }
            
            return result;
	    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table height="100%" width="100%">
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">物料收料状态</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="query" height="100%" width="100%">
                        <tr>
                        <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblItemIDQuery" runat="server">物料代码</asp:Label></td>
                            <td class="fieldValue" style="width: 132px">
                                <cc2:SelectableTextBox ID="txtMaterialCodeQuery"  CanKeyIn="true" runat="server" Width="120px"
                                    Type="material"></cc2:SelectableTextBox></td>
                                    
                              <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblVendorCodeQuery" runat="server">供应商代码</asp:Label></td>
                            <td class="fieldValue">
                                <asp:TextBox ID="txtVerdorCodeQuery" MaxLength="40" runat="server" Width="120px"
                                    CssClass="textbox"></asp:TextBox></td>
                            
                           
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblSendStatus2" runat="server">报帐状态</asp:Label></td>
                            <td class="fieldValue" >
                                <asp:DropDownList ID="drpStatusQuery" runat="server" CssClass="textbox" Width="70px"
                                    OnLoad="drpStatus_Load">
                                </asp:DropDownList></td>
                            <td width="100%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblVoucherDateFrom" runat="server">凭证日期从</asp:Label></td>
                            <td class="fieldValue" style="width: 132px">
                                <uc1:eMesDate ID="dateVoucherDateFrom" CssClass="textbox" Width="130" runat="server">
                                </uc1:eMesDate>
                            </td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblZ" runat="server">到</asp:Label></td>
                            <td class="fieldValue" style="width: 132px">
                                <uc1:eMesDate ID="dateVoucherDateTo" CssClass="textbox" Width="120" runat="server"></uc1:eMesDate>
                            </td>
                            <td  width="100%" nowrap colspan="2" class="fieldName">
                                <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                    runat="server"></td>
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
                            <ClientSideEvents ClickCellButtonHandler="gridWebGrid_ClickCellButtonHandler" />
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
                            <td class="smallImgButton">
                                <input class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
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
        </table>
    </form>
</body>
</html>
