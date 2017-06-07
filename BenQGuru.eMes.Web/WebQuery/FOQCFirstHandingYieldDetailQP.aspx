<%@ Page language="c#" Codebehind="FOQCFirstHandingYieldDetailQP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FOQCFirstHandingYieldDetailQP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FOQCFirstHandingYieldDetailQP</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" width="100%">
				<TR class="moduleTitle">
					<TD>
						<asp:Label id="lblTitle" runat="server">FQC一交合格率明细</asp:Label><INPUT class="textbox" id="txtSeq" style="DISPLAY: none; WIDTH: 120px" readOnly name="textfield"
							runat="server"></TD>
				</TR>
				<TR>
					<TD>
						<TABLE class="query" id="Table2" height="100%" width="100%">
							<TR>
								<TD class="fieldNameLeft" noWrap>
									<asp:Label id="lblModelCodeQuery" runat="server">产品别代码</asp:Label></TD>
								<TD class="fieldValue"><INPUT class="textbox" style="WIDTH: 165px" name="txtModelCode" id="txtModelCode" runat="server"
										readOnly>
								</TD>
								<TD class="fieldNameLeft" noWrap>
									<asp:Label id="lblItemCodeQuery" runat="server">产品代码</asp:Label></TD>
								<TD class="fieldValue"><INPUT class="textbox" style="WIDTH: 120px" name="txtItemCode" id="txtItemCode" runat="server"
										readOnly>
								</TD>
								<TD class="fieldName" noWrap><asp:Label id="lblStartTimeQuery" runat="server">起始时间</asp:Label></TD>
								<TD class="fieldValue"><INPUT class="textbox" style="WIDTH: 120px" name="textfield2" id="txtStartDate" runat="server"
										readOnly></TD>
								<TD class="fieldName" noWrap><asp:Label id="lblEndTimeQuery" runat="server">结束时间</asp:Label></TD>
								<TD class="fieldValue"><INPUT class="textbox" style="WIDTH: 120px" name="textfield2" id="txtEndDate" runat="server"
										readOnly></TD>
								<TD noWrap width="100%">&nbsp;</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR height="100%">
					<TD class="fieldGrid">
						<STYLE type="text/css">.gridWebGrid-ic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; TEXT-ALIGN: left }
	.gridWebGrid-aic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ebebeb; TEXT-ALIGN: left }
	.gridWebGrid-0-ic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; TEXT-ALIGN: left }
	.gridWebGrid-0-aic { BORDER-RIGHT: #d7d8d9 1px solid; BORDER-TOP: #d7d8d9 0px solid; PADDING-LEFT: 3px; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: #d7d8d9 0px solid; BORDER-BOTTOM: #d7d8d9 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ebebeb; TEXT-ALIGN: left }
	.gridWebGrid-0-6-cbc { BACKGROUND-POSITION: center 50%; BACKGROUND-IMAGE: url(./skin/image/detail16.gif); CURSOR: hand; BORDER-TOP-STYLE: none; BACKGROUND-REPEAT: no-repeat; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; TEXT-ALIGN: center; BORDER-BOTTOM-STYLE: none }
	.gridWebGrid-ecc { BORDER-RIGHT: black 1px; BORDER-TOP: black 1px; PADDING-BOTTOM: 1px; VERTICAL-ALIGN: middle; BORDER-LEFT: black 1px; BORDER-BOTTOM: black 1px }
	.gridWebGrid-hc { BORDER-RIGHT: white 1px dashed; BORDER-TOP: white 1px dashed; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: white 1px dashed; BORDER-BOTTOM: white 1px dashed; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ababab; TEXT-ALIGN: left }
	.gridWebGrid-shc { BORDER-RIGHT: white 1px solid; BORDER-TOP: white 0px solid; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 12px; VERTICAL-ALIGN: middle; BORDER-LEFT: white 0px solid; BORDER-BOTTOM: white 1px solid; FONT-FAMILY: Verdana; BACKGROUND-COLOR: #ababab; TEXT-ALIGN: left }
						</STYLE>
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
						</igtbl:ultrawebgrid>
					</TD>
				</TR>
				<TR class="normal">
					<TD>
						<TABLE id="Table3" height="100%" cellPadding="0" width="100%">
							<TR>
								<TD class="smallImgButton" noWrap><INPUT class="gridExportButton" id="cmdGridExport" type="submit" value="  " name="cmdGridExport"
										runat="server">|</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:PagerToolbar id="pagerToolBar" runat="server"></cc1:PagerToolbar></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
										runat="server" onserverclick="cmdReturn_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
