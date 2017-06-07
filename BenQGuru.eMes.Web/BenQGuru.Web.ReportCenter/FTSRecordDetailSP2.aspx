<%@ Page language="c#" Codebehind="FTSRecordDetailSP2.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FTSRecordDetailSP2" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMESTime" Src="~/UserControl/DateTime/DateTime/eMESTime.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FTSRecordDetailSP</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="FORM1" runat="server">
			<table width="100%" height="100%">
				<tr>
					<td>
						<table cellpadding="0" cellspacing="0">
							<tr>
								<td><img id="imgTitle" src="../skin/image/ico_arrow.gif" width="15" height="15">
								</td>
								<td><asp:label id="lblRepairQuery" runat="server" CssClass="Title2">维修记录查询明细</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table class="query" width="100%">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:Label id="lblModelQuery" Runat="server">产品别</asp:Label></td>
								<td class="fieldValue" noWrap><asp:TextBox id="txtModel" Runat="server" width="120px"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:Label id="lblItemQuery" Runat="server">产品</asp:Label></td>
								<td class="fieldValue" noWrap><asp:TextBox id="txtItem" Runat="server" width="120px"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:Label id="lblMOQuery" Runat="server">工单</asp:Label></td>
								<td class="fieldValue" noWrap><asp:TextBox id="txtMO" Runat="server" width="120px"></asp:TextBox></td>
							</tr>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:Label id="lblSNQuery" Runat="server">序列号</asp:Label></td>
								<td class="fieldValue" noWrap><asp:TextBox id="txtSN" Runat="server" width="165px"></asp:TextBox></td>
								<td class="fieldName" noWrap><asp:Label id="lblTSStateQuery" Runat="server">维修状态</asp:Label></td>
								<td class="fieldValue" noWrap><asp:TextBox id="txtTSStatus" Runat="server" width="120px"></asp:TextBox></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr height="100%" width="100%">
					<td>
						<table width="100%" height="100%">
							<TBODY>
								<tr>
					</td>
				</tr>
			</table>
			<igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
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
			</igtbl:ultrawebgrid></TD></TR>
			<tr class="normal">
				<td>
					<table height="100%" cellPadding="0" width="100%">
						<tr>
							<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
									runat="server"> |
							</TD>
							<TD><cc1:pagersizeselector id="Pagersizeselector1" runat="server" PageSize="10"></cc1:pagersizeselector></TD>
							<td align="right"><cc1:PagerToolBar id="pagerToolBar" runat="server"></cc1:PagerToolBar></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr class="toolbar">
				<td>
					<table class="toolbar" width="100%" height="100%">
						<tr>
							<td>
								<input type="submit" class="submitImgButton" value="返 回" style="BACKGROUND-IMAGE: url(./Skin/Image/ButtonGray.gif)"
									onmouseover="this.style.backgroundImage='url(./Skin/Image/ButtonBlue.gif)';" onmouseout="this.style.backgroundImage='url(./Skin/Image/ButtonGray.gif)';"
									onclick="history.back()" ID="cmdReturn" NAME="Submit1">
							</td>
						</tr>
					</table>
				</td>
			</tr>
			</TBODY></TABLE></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
		</form>
	</body>
</HTML>
