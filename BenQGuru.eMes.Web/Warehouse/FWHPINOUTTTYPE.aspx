<%@ Page Language="c#" Codebehind="FWHPINOUTTTYPE.aspx.cs" AutoEventWireup="True"
    Inherits="BenQGuru.eMES.Web.WarehouseWeb.FWHPINOUTTTYPE" %>

<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>屏出入库业务类型维护</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=StyleSheet%>" rel="stylesheet" />
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table height="100%" width="100%">
            <tr class="moduleTitle">
                <td>
                    <asp:Label ID="lblTitle" runat="server" CssClass="labeltopic">屏出入库业务类型维护</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table class="query" height="100%" width="100%">
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMaterialBusinessCodeQuery" runat="server">业务代码</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtMaterialBusinessCodeQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                    CssClass="textbox"></asp:TextBox></td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMaterialBusinessDescQuery" runat="server">业务描述</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtMaterialBusinessDescQuery" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                    CssClass="textbox"></asp:TextBox></td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblBusinessTypeQuery" runat="server">业务类型</asp:Label></td>
                            <td class="fieldValue" style="height: 26px">
                                <asp:DropDownList ID="ddlBusinessTypeQuery" runat="server" CssClass="require" Width="120px"
                                    OnLoad="ddlBusinessTypeQuery_Load">
                                </asp:DropDownList></td>
                            <td nowrap width="100%">
                            </td>
                            <td class="fieldName">
                                <input class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
                                    runat="server"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr height="100%">
                <td class="fieldGrid">
                    <igtbl:ultrawebgrid id="gridWebGrid" runat="server" width="100%" height="100%">
							<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
								Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
								BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
								Name="webGrid" TableLayout="Fixed">
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
								<HeaderStyleDefault BorderWidth="1px" Font-Size="12px" Font-Bold="True" BorderColor="White" BorderStyle="Dashed"
									HorizontalAlign="Left" BackColor="#ABABAB">
									<BorderDetails ColorTop="White" WidthLeft="1px" ColorBottom="White" WidthTop="1px" ColorRight="White"
										ColorLeft="White"></BorderDetails>
								</HeaderStyleDefault>
								<RowSelectorStyleDefault BackColor="#EBEBEB"></RowSelectorStyleDefault>
								<FrameStyle Width="100%" BorderWidth="0px" Font-Size="12px" Font-Names="Verdana" BorderColor="#ABABAB"
									BorderStyle="Groove" Height="100%"></FrameStyle>
								<FooterStyleDefault BorderWidth="0px" BorderStyle="Groove" BackColor="LightGray">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<ActivationObject BorderStyle="Dotted"></ActivationObject>
								<EditCellStyleDefault VerticalAlign="Middle" BorderWidth="1px" BorderColor="Black" BorderStyle="None">
									<Padding Bottom="1px"></Padding>
								</EditCellStyleDefault>
								<RowAlternateStyleDefault BackColor="White"></RowAlternateStyleDefault>
								<RowStyleDefault VerticalAlign="Middle" BorderWidth="1px" BorderColor="#D7D8D9" BorderStyle="Solid"
									HorizontalAlign="Left">
									<Padding Left="3px"></Padding>
									<BorderDetails WidthLeft="0px" WidthTop="0px"></BorderDetails>
								</RowStyleDefault>
								<ImageUrls ImageDirectory="/ig_common/WebGrid3/"></ImageUrls>
							</DisplayLayout>
							<Bands>
								<igtbl:UltraGridBand></igtbl:UltraGridBand>
							</Bands>
						</igtbl:ultrawebgrid>
                </td>
            </tr>
            <tr class="normal">
                <td>
                    <table height="100%" cellpadding="0" width="100%">
                        <tr>
                            <td>
                                <asp:CheckBox ID="chbSelectAll" runat="server" Text="全选" AutoPostBack="True"></asp:CheckBox></td>
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
                                <cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector>
                            </td>
                            <td align="right">
                                <cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar>
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
                                <asp:Label ID="lblMaterialBusinessCodeEdit" runat="server">业务代码</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtMaterialBusinessCodeEdit" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                    CssClass="textbox"></asp:TextBox></td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblMaterialBusinessDescEdit" runat="server">业务描述</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:TextBox ID="txtMaterialBusinessDescEdit" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257"
                                    CssClass="textbox"></asp:TextBox></td>
                            <td class="fieldName" nowrap="noWrap">
                                <asp:Label ID="lblOrgEdit" runat="server">组织</asp:Label></td>
                            <td class="fieldValue" nowrap="noWrap">
                                <asp:DropDownList ID="drpOrgEdit" runat="server" CssClass="textbox" Width="130px">
                                </asp:DropDownList></td>
                            <td style="width: 100%">
                            </td>
                        </tr>
                        <tr>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblBusinessTypeEdit" runat="server">业务类型</asp:Label></td>
                            <td>
                                <asp:RadioButtonList ID="rblBussinessType" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rblBussinessType_SelectedIndexChanged">
                                </asp:RadioButtonList></td>
                            <td class="fieldNameLeft" nowrap>
                                <asp:Label ID="lblSAPCode" runat="server">SAP业务代码</asp:Label></td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:DropDownList ID="drpSAPCode" runat="server" CssClass="textbox" Width="130px"></asp:DropDownList></td>
                            <td>
                                &nbsp;</td>
                            <td class="fieldValue" style="width: 159px">
                                <asp:CheckBox ID="chbFIFO" runat="server" Text="FIFO" /></td>
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
                                    runat="server"></td>
                            <td class="toolBar">
                                <input class="submitImgButton" id="cmdSave" type="submit" value="保存" name="cmdSave"
                                    runat="server" /></td>
                            <td>
                                <input class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
                                    runat="server"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
