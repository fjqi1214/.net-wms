<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FStockAgeQuery.aspx.cs" Inherits="BenQGuru.eMES.Web.WebQuery.FStockAgeQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMESDate" Src="~/UserControl/DateTime/DateTime/eMESDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FPauseHistoryQuery</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic">停发历史查询</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblStorageTypeEdit" runat="server" >库别</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px"><cc2:selectabletextbox id="txtStorageTypeEdit" cankeyin="true" runat="server" Width="150px" Type="storagetype"></cc2:selectabletextbox></td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblPeiodGroupEdit" runat="server">帐龄组</asp:label></td>
								<td  style="WIDTH: 159px">
									<asp:DropDownList runat="server" ID="drpPeiodGroupEdit" Width="150px" ></asp:DropDownList>
								</td>
								<td noWrap width="100%"></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px">
									<cc2:selectabletextbox id="txtItemCodeEdit" runat="server" Width="150px" Type="item" cankeyin="true"></cc2:selectabletextbox></td>
								</td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblOrderNoEdit" runat="server">合同号</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px">
									<asp:textbox id="txtOrderNoEdit" runat="server" Width="150px" DESIGNTIMEDRAGDROP="257" CssClass="textbox"></asp:textbox>
								</td>
								<td noWrap width="100%"></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblMmodelcode" runat="server">机型</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px">
									<cc2:selectabletextbox id="txtMmodelcode" cankeyin="true" runat="server" Width="150px" Type="mmodelcode"></cc2:selectabletextbox>
								</td>
								<td class="fieldNameLeft" noWrap><asp:label id="lblFirstClassGroup" runat="server">分类</asp:label></td>
								<td class="fieldValue" style="WIDTH: 159px">
									<asp:DropDownList runat="server" ID="drpFirstClassGroup" Width="150px"  ></asp:DropDownList>
								</td>
								<td noWrap width="100%"><asp:CheckBox id="chbSAPCompelete" runat="server" Text="SAP完工"></asp:CheckBox></td>
								<td class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery" runat="server" ></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid" id="GridTd" runat="server"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
							<DisplayLayout ColWidthDefault="" StationaryMargins="Header" RowHeightDefault="20px" Version="2.00"
								SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" BorderCollapseDefault="Separate"
								AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No" Name="webGrid"
								TableLayout="Fixed" AllowSortingDefault="Yes" HeaderClickActionDefault="SortSingle">
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
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"  onserverclick="cmdGridExport_ServerClick"> |
								</TD>
								<TD><div style="display:none;"><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></div></TD>
								<td align="right"><div style="display:none;"><cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></div>
									</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

