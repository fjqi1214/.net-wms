<%@ Page language="c#" Codebehind="FFTQuery.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FFTQuery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v9.1, Version=9.1.20091.2101, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FINNOInfoQP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
		<script language="javascript">
			
		</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table id="Table1" height="100%" width="100%">
				<tr class="moduleTitle">
					<td style="HEIGHT: 19px"><asp:label id="lblTitle" runat="server" CssClass="labeltopic">FT测试结果查询</asp:label></td>
				</tr>
				<tr>
					<td>
						<table class="query" id="Table2" height="100%" width="100%">
							<TR>
								<td class="fieldNameLeft"><asp:label id="lblSS" runat="server">产线</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtStepSequence" runat="server" Width="150px" Type="stepsequence"></cc2:selectabletextbox></td>
								<td class="fieldName" noWrap><asp:label id="lblItemCodeQuery" runat="server">产品代码</asp:label></td>
								<td class="fieldValue"><cc2:selectabletextbox id="txtItemQuery" runat="server" Width="150px" Type="item" CssClass="require"></cc2:selectabletextbox></td>
								<td class="fieldNameLeft"><asp:label id="lblRes" runat="server">资源</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtResQuery" runat="server" CssClass="textbox" Width="130"></asp:textbox></td>
							</TR>
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblStartRCardQuery" runat="server">起始产品序列号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtStartSnQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblEndRCardQuery" runat="server">截止产品序列号</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtEndSnQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<td class="fieldNameLeft" noWrap style="DISPLAY:none"><asp:label id="lblMoQuery" runat="server">工单代码</asp:label></td>
								<td class="fieldValue" style="DISPLAY:none"><cc2:selectabletextbox id="txtMoQuery" runat="server" Width="150px" Type="mo"></cc2:selectabletextbox></td>
								<td></td>
								<td class="fieldName">&nbsp;
								</td>
							</tr>
							<TR>
								<TD class="fieldNameLeft" noWrap>
										<asp:label id="lblStartDateQuery" runat="server">起始日期</asp:label></TD>
								<TD class="fieldValue">
									<uc1:emesdate id="txtDateFrom" runat="server" CssClass="textbox" width="130"></uc1:emesdate></TD>
								<TD class="fieldName" noWrap>
									<asp:label id="lblEndDateQuery" runat="server">结束日期</asp:label></TD>
								<TD class="fieldValue" noWrap>
									<uc1:emesdate id="txDateTo" runat="server" CssClass="textbox" width="130"></uc1:emesdate></TD>
								<TD></TD>
								<TD><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></TD>
								<TD></TD>
								<TD class="fieldName"></TD>
							</TR>
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
							</igtbl:ultrawebgrid><DISPLAYLAYOUT TableLayout="Fixed" Name="webGrid" RowSelectorsDefault="No" CellPaddingDefault="4"
							AllowColSizingDefault="Free" BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" SelectTypeCellDefault="Single"
							SelectTypeRowDefault="Single" Version="2.00" RowHeightDefault="20px" AllowSortingDefault="Yes" StationaryMargins="Header" ColWidthDefault=""><ADDNEWBOX>
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
							<FRAMESTYLE Width="100%" Height="100%" BorderStyle="Groove" BorderWidth="0px" BorderColor="#ABABAB"
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
							<IMAGEURLS ImageDirectory="/ig_common/WebGrid3/"></IMAGEURLS>
						</DISPLAYLAYOUT><BANDS><IGTBL:ULTRAGRIDBAND></IGTBL:ULTRAGRIDBAND>
						</BANDS></td>
				</tr>
				<tr class="normal">
					<td>
						<table id="Table3" height="100%" cellPadding="0" width="100%">
							<tr>
								<TD width="140"><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server"> |
								</TD>
								<td><asp:checkbox id="chbTestDetail" runat="server" Text="导出测试明细"></asp:checkbox></td>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
