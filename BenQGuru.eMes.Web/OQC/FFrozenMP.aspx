<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>

<%@ Page Language="c#" CodeBehind="FFrozenMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.OQC.FFrozenMP" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>FFrozenMP</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="<%=StyleSheet%>" rel="stylesheet">
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script>
        $(function () {
            var height = $(window).height();
            if (height < $(document).height()) {
                height = $(document).height();
            }
            $("#tableMain").height(height);
        })           
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="tableMain" width="100%">
        <tr class="moduleTitle">
            <td>
                <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic"> 隔离 </asp:Label>
            </td>
        </tr>
        <tr >
            <td  height="1">
                <table id="table1" class="query" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblLotNoQuery" runat="server" Style="width: 60px"> 批号 </asp:Label>
                        </td>
                        <td class="fieldValue" style="width: 159px">
                            <asp:TextBox ID="txtLotNoQuery" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
                        </td>
                        <td nowrap width="100%">
                        </td>
                        <td class="fieldName">
                            <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                runat="server" onserverclick="cmdQuery_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="display: none">
            <td class="fieldGrid">
                <igtbl:UltraWebGrid ID="gridWebGrid" runat="server" Width="100%">
                    <DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes"
                        RowHeightDefault="20px" Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single"
                        HeaderClickActionDefault="SortSingle" BorderCollapseDefault="Separate" AllowColSizingDefault="Free"
                        CellPaddingDefault="4" RowSelectorsDefault="No" Name="webGrid" TableLayout="Fixed">
                        <AddNewBox>
                            <style borderwidth="1px" borderstyle="Solid" backcolor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</style>
                        </AddNewBox>
                        <Pager>
                            <style borderwidth="1px" borderstyle="Solid" backcolor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</style>
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
                            <BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
                            </BorderDetails>
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
                </igtbl:UltraWebGrid>
            </td>
        </tr>
        <tr class="normal" style="display: none">
            <td>
                <table id="table2" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <asp:CheckBox ID="chbSelectAll" runat="server" Width="124px" AutoPostBack="True"
                                Text="全选" OnCheckedChanged="chbSelectAll_CheckedChanged"></asp:CheckBox>
                        </td>
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
                            <cc1:pagertoolbar id="pagerToolBar" runat="server" PageSize="20">
                            </cc1:pagertoolbar>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="normal">
            <td>
                <table id="table3" class="edit" cellpadding="0" width="100%">
                    <tr>
                        <td class="fieldNameLeft" nowrap>
                            <asp:Label ID="lblFrozenCauseEdit" runat="server" Style="width: 60px">隔离原因</asp:Label>
                        </td>
                        <td class="fieldValue" colspan="3">
                            <asp:TextBox ID="txtFrozenCauseEdit" runat="server" CssClass="textbox" Width="500px"
                                Height="200px" MaxLength="50" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td nowrap >
                            <asp:CheckBox ID="chkFrozenLotEdit" runat="server" Text="禁止新的序列号进入"></asp:CheckBox>
                        </td>
                        <td style="width: 100%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr >
            <td valign="bottom">
                <table id="table4" class="toolBar">
                    <tr>
                        <td class="toolBar">
                            <input class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave"
                                runat="server" onserverclick="cmdSave_ServerClick">
                        </td>
                        <td>
                            <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                runat="server" onserverclick="cmdCancel_ServerClick">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
