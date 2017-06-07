<%@ Page language="c#" Codebehind="FResourceSP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FResourceSP" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FMOSP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<tr class="moduleTitle">
					<td>
						<asp:label id="lblTitle" runat="server">选择</asp:label></td>
				</tr>
				<TR>
					<TD noWrap>
							<TABLE class="query" id="Table2" height="100%" width="100%">
								<TR>
									<TD class="fieldNameLeft" noWrap>
										<asp:label id="lblSegmentCodeQuery" runat="server">工段代码</asp:label></TD>
									<TD class="fieldValue" nowrap>
										<asp:textbox id="txtSegmentCodeQuery" runat="server" Width="120px" CssClass="textbox"></asp:textbox></TD>
									<TD noWrap class="fieldName">
										<asp:label id="lblStepSequenceCodeQuery" runat="server">生产线代码</asp:label></TD>
									<TD noWrap class="fieldValue">
										<asp:textbox id="txtStepSequenceCodeQuery" runat="server" Width="120px" CssClass="textbox"></asp:textbox></TD>
									<TD noWrap width="100%">&nbsp;</TD>
									<TD class="fieldName">&nbsp;
									</TD>
								</TR>
								<TR>
									<TD class="fieldNameLeft" noWrap>
										<asp:label id="lblItemOperationCodeQuery" runat="server">工序代码</asp:label></TD>
									<TD class="fieldValue" noWrap>
										<asp:textbox id="txtOPCodeQuery" runat="server" Width="120px" CssClass="textbox"></asp:textbox></TD>
									<TD class="fieldName" noWrap>
										<asp:label id="lblResourceCodeQuery" runat="server">资源代码</asp:label></TD>
									<TD class="fieldValue" noWrap>
										<asp:textbox id="txtResourceCodeQuery" runat="server" Width="120px" CssClass="textbox"></asp:textbox></TD>
									<TD noWrap width="100%"></TD>
									<TD class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" onmouseover="" onmouseout="" type="submit"
											value="查 询" name="cmdQuery" runat="server"></TD>
								</TR>
							</TABLE>
						
					</TD>
				</TR>
				<TR>
					<TD noWrap height="50%"><igtbl:ultrawebgrid id="gridUnselected" runat="server" Width="100%" Height="100%">
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
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand></igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<TD noWrap>
						<TABLE id="Table3" height="100%" cellPadding="0" width="100%">
							<TR>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD noWrap align="center"><asp:button id="cmdUnSelect" style="BACKGROUND-IMAGE: url(/eMes/Skin/Image/up.gif)" runat="server"
							CssClass="smallbutton"></asp:button>&nbsp;
						<asp:button id="cmdSelect" style="BACKGROUND-IMAGE: url(/eMes/Skin/Image/down.gif)" runat="server"
							CssClass="smallbutton"></asp:button></TD>
				</TR>
				<TR>
					<TD noWrap height="50%"><igtbl:ultrawebgrid id="gridSelected" runat="server" Width="100%" Height="100%">
								<DisplayLayout ColWidthDefault="" StationaryMargins="Header" AllowSortingDefault="Yes" RowHeightDefault="20px"
									Version="2.00" SelectTypeRowDefault="Single" SelectTypeCellDefault="Single" HeaderClickActionDefault="SortSingle"
									BorderCollapseDefault="Separate" AllowColSizingDefault="Free" CellPaddingDefault="4" RowSelectorsDefault="No"
									Name="Ultrawebgrid1" TableLayout="Fixed">
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
								</DisplayLayout>
								<Bands>
									<igtbl:UltraGridBand></igtbl:UltraGridBand>
								</Bands>
							</igtbl:ultrawebgrid></TD>
				</TR>
				<TR>
					<TD noWrap align="center">&nbsp;<INPUT class="submitImgButton" id="cmdSave" type="submit" value="保 存" name="cmdSave" runat="server">&nbsp;<INPUT class="submitImgButton" id="cmdCancel" type="submit" value="取 消" name="cmdCancel"
								runat="server"></TD>
				</TR>
			</TABLE>
			<div style="DISPLAY:none">
				<asp:checkbox id="chbSelected" runat="server" AutoPostBack="True"></asp:checkbox>
				<asp:checkbox id="chbUnSelected" runat="server" AutoPostBack="True"></asp:checkbox>
			</div>
			<TEXTAREA id="txtSelected" rows="1" cols="20" runat="server" NAME="txtSelected"></TEXTAREA>
		</form>
	</body>
</HTML>
