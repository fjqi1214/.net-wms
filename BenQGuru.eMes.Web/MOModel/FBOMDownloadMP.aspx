<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FBOMDownloadMP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.MOModel.MBOMDownload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>MBOMDownload</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<TBODY>
					<tr class="moduleTitle">
						<td><asp:label id="lblBOMDownloadTitle" runat="server" CssClass="labeltopic">物料清单导入</asp:label></td>
					</tr>
					<tr>
						<td>
							<table class="query" height="100%" width="100%">
								<tr align="right">
									<TD class="fieldNameLeft" noWrap height="20"><asp:label id="lblTitle" runat="server"> 物料清单文件：</asp:label></TD>
									<TD style="WIDTH: 447px"><INPUT id="DownLoadPathBom" style="WIDTH: 454px; HEIGHT: 22px" type="file" size="56" name="File1"
											runat="server"></TD>
									<td noWrap width="100%"></td>
									<td align="center"><input class="submitImgButton" id="cmdView" onmouseover="document.getElementById('cmdQuery').style.backgroundImage='url(&quot;../../Skin/Image/search_0.gif&quot;)';"
											onmouseout="document.getElementById('cmdQuery').style.backgroundImage='url(&quot;../../Skin/Image/search.gif&quot;)';"
											type="submit" value="察 看" name="cmdQuery" runat="server" onserverclick="cmdBomView_ServerClick">
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr height="100%">
						<td class="fieldGrid"><DISPLAYLAYOUT TableLayout="Fixed" Name="webGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
								AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" SelectTypeCellDefault="Single"
								SelectTypeRowDefault="Single" Version="2.00" RowHeightDefault="20px" AllowSortingDefault="Yes" StationaryMargins="Header"
								ColWidthDefault=""><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Height="100%" Width="100%">
									<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
										Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
										BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
										Name="gridWebGrid" TableLayout="Fixed">
										<AddNewBox>
											<Style BorderWidth="1px" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

											</Style>
										</AddNewBox>
										<Pager StyleMode="CustomLabels" AllowPaging="True">
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
									</DisplayLayout>
									<Bands>
										<igtbl:UltraGridBand></igtbl:UltraGridBand>
									</Bands>
								</igtbl:ultrawebgrid>
								<ADDNEWBOX>
									<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
									</STYLE>
								</ADDNEWBOX>
								<PAGER>
									<STYLE BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
									</STYLE>
								</PAGER>
								<HEADERSTYLEDEFAULT BackColor="#ABABAB" BorderStyle="Dashed" BorderWidth="1px" HorizontalAlign="Left"
									BorderColor="White" Font-Bold="True" Font-Size="12px">
									<BORDERDETAILS ColorLeft="White" ColorRight="White" WidthTop="1px" ColorBottom="White" WidthLeft="1px"
										ColorTop="White"></BORDERDETAILS>
								</HEADERSTYLEDEFAULT>
								<ROWSELECTORSTYLEDEFAULT BackColor="#EBEBEB"></ROWSELECTORSTYLEDEFAULT>
								<FRAMESTYLE Height="100%" Width="100%" BorderStyle="Groove" BorderWidth="0px" BorderColor="#ABABAB"
									Font-Size="12px" Font-Names="Verdana"></FRAMESTYLE>
								<FOOTERSTYLEDEFAULT BackColor="LightGray" BorderStyle="Groove" BorderWidth="0px">
									<BORDERDETAILS ColorLeft="White" WidthTop="1px" WidthLeft="1px" ColorTop="White"></BORDERDETAILS>
								</FOOTERSTYLEDEFAULT>
								<ACTIVATIONOBJECT BorderStyle="Dotted"></ACTIVATIONOBJECT>
								<EDITCELLSTYLEDEFAULT BorderStyle="None" BorderWidth="1px" BorderColor="Black" VerticalAlign="Middle">
									<PADDING Bottom="1px"></PADDING>
								</EDITCELLSTYLEDEFAULT>
								<ROWALTERNATESTYLEDEFAULT BackColor="White"></ROWALTERNATESTYLEDEFAULT>
								<ROWSTYLEDEFAULT BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" BorderColor="#D7D8D9"
									VerticalAlign="Middle">
									<PADDING Left="3px"></PADDING>
									<BORDERDETAILS WidthTop="0px" WidthLeft="0px"></BORDERDETAILS>
								</ROWSTYLEDEFAULT>
								<IMAGEURLS ImageDirectory="/ig_grid2_Images/"></IMAGEURLS>
							</DISPLAYLAYOUT><BANDS><IGTBL:ULTRAGRIDBAND></IGTBL:ULTRAGRIDBAND>
							</BANDS></td>
					</tr>
					<tr class="normal">
						<td>
							<table height="100%" cellPadding="0" width="100%">
								<tr>
									<td><asp:checkbox id="chbSelectAll" runat="server" Width="124px" Text="全选" AutoPostBack="True" oncheckedchanged="chbSelectAll_CheckedChanged"></asp:checkbox></td>
								<tr class="normal">
									<td></td>
								</tr>
								<tr class="toolBar">
									<td>
										<table class="toolBar">
											<tr>
												<td class="toolBar"><INPUT class="submitImgButton" id="cmdEnter" type="submit" value="导入" name="btnAdd" runat="server" onserverclick="cmdAdd_ServerClick"></td>
												<td class="toolBar"><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返回" name="btnDelete"
														runat="server" onserverclick="cmdReturn_ServerClick"></td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
		</form>
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>
