<%@ Register TagPrefix="uc1" TagName="eMESTime" Src="~/UserControl/DateTime/DateTime/eMESTime.ascx" %>
<%@ Page language="c#" Codebehind="FTransTypeMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WarehouseWeb.FTransTypeMP" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>交易单据设置</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 交易单据设置</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblTicketCodeQuery" runat="server"> 单据代码</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px"><asp:textbox id="txtTransTypeCodeQuery" runat="server" Width="150px" CssClass="textbox"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblTransNameQuery" runat="server"> 单据名称</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px"><asp:textbox id="txtTransTypeNameQuery" runat="server" Width="150px" CssClass="textbox"></asp:textbox></td>
								<td noWrap width="100%"></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
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
						</igtbl:ultrawebgrid></td>
				</tr>
				<tr class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<td><asp:checkbox id="chbSelectAll" runat="server" AutoPostBack="True" Text="全选"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									|
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right">
									<cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="normal">
					<td>
						<table class="edit" height="100%" cellPadding="0" width="100%">
							<tr>
								<TD class="fieldName" noWrap><asp:label id="lblTicketTypeEdit" runat="server">单据类型</asp:label></TD>
								<TD class="fieldValue" noWrap><asp:dropdownlist id="drpTransTypeEdit" runat="server" CssClass="require" Width="130px" AutoPostBack="True" onselectedindexchanged="drpTransTypeEdit_SelectedIndexChanged"></asp:dropdownlist></TD>
								<td class="fieldName" style="HEIGHT: 26px" noWrap><asp:label id="lblTicketCodeEdit" runat="server">单据代码</asp:label></td>
								<TD class="fieldValue" style="HEIGHT: 26px"><asp:textbox id="txtTransTypeCodeEdit" runat="server" CssClass="require" Width="130px" Enabled="False"></asp:textbox></TD>
								<td class="fieldNameLeft" style="HEIGHT: 26px" noWrap><asp:label id="lblTransNameEdit" runat="server">单据名称</asp:label></td>
								<td class="fieldValue" style="HEIGHT: 26px"><asp:textbox id="txtTransTypeNameEdit" runat="server" CssClass="require" Width="130px"></asp:textbox></td>
							</tr>
							<tr>
								<td class="fieldName" noWrap><asp:label id="lblTransTypeDescriptionEdit" runat="server">单据描述</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtTransTypeDescriptionEdit" runat="server" Width="130px" CssClass="textbox"></asp:textbox></td>
								<td noWrap colSpan="2"><asp:checkbox id="chbIsByMOControl" runat="server" Text="工单管控"></asp:checkbox></td>
								<td class="fieldName" noWrap><asp:label id="lblTransTypeCategoryEdit" runat="server" Visible="False">单据前缀</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtTransTypeCategoryEdit" runat="server" Width="20px" MaxLength="1" CssClass="textbox"
										Visible="False"></asp:textbox></td>
								<td nowrap width="100%"></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdAdd" type="submit" value="新 增" name="cmdAdd" runat="server"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server"></td>
								<td><INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
										runat="server"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
