<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="../UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="../UserControl/DateTime/DateTime/eMesDate.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Page language="c#" Codebehind="FReworkRangeSP.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.Rework.FReworkRangeSP" %>
<%@ Register TagPrefix="cc2" Namespace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FShiftMP</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="<%=StyleSheet%>" rel=stylesheet>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table height="100%" width="100%">
				<tr class="moduleTitle">
					<td><asp:label id="lblTitle" runat="server" CssClass="labeltopic"> 维护返工范围</asp:label><asp:textbox id="txtReworkSheetCode" runat="server" CssClass="textbox" Width="120px" Visible="False"
							ReadOnly="True"></asp:textbox></td>
				</tr>
				<tr>
					<td>
						<table class="query" height="100%" width="100%" border="0">
							<tr>
								<td class="fieldNameLeft" noWrap><asp:label id="lblMOQuery" runat="server" DESIGNTIMEDRAGDROP="77"> 工单</asp:label></td>
								<td class="fieldValue"><asp:textbox id="txtMOCodeQuery" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<td class="fieldName" noWrap><asp:label id="lblLotNum" runat="server" DESIGNTIMEDRAGDROP="120"> 批号</asp:label></td>
								<TD class="fieldValue">
										<cc2:SelectableTextBox id="txtLotNoQuery" runat="server" width="100px" Type="lot" Target="lot"></cc2:SelectableTextBox></TD>
								<td class="fieldName" noWrap><asp:label id="lblOperationCodeQuery" runat="server"> 批退工序</asp:label></td>
								<td class="fieldValue"><asp:dropdownlist id="drpOPCodeQuery" runat="server" Width="165px" onload="drpOpCodeQuery_Load"></asp:dropdownlist></td>
								<TD></TD>
								<td class="fieldName"></td>
							</tr>
							<TR>
								<TD class="fieldNameLeft" noWrap><asp:label id="lblRejectTimeQuery" runat="server">判退时间 从</asp:label></TD>
								<td class="fieldValue" nowrap>
									<table border="0" cellspacing="0" cellpadding="0">
										<tr>
											<TD><uc1:emesdate id="dateFrom" runat="server" width="80"></uc1:emesdate></TD>
											<td><uc1:emestime id="timeFrom" runat="server" width="60"></uc1:emestime></td>
										</tr>
									</table>
								</td>
								<td class="fieldName"><asp:label id="lblTo" runat="server">到</asp:label></td>
								<td class="fieldValue">
									<table cellSpacing="0" cellPadding="0">
										<tr>
											<td><uc1:emesdate id="dateTo" runat="server" width="80"></uc1:emesdate></td>
											<td><uc1:emestime id="timeTo" runat="server" width="60"></uc1:emestime></td>
										</tr>
									</table>
								</td>
								<td>
									<asp:label id="lblCartonNoQuery" runat="server">箱号</asp:label></td>
								<td><asp:textbox id="txtBoxNo" runat="server" CssClass="textbox" Width="165px"></asp:textbox></td>
								<TD width="100%">&nbsp;</TD>
								<TD class="fieldName"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
										runat="server"></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr height="100%">
					<td class="fieldGrid"><igtbl:ultrawebgrid id="gridWebGrid" runat="server" Width="100%" Height="100%">
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
								<FrameStyle Width="100%" BorderWidth="0px" Font-Size="12px" Font-Names="Verdana" BorderStyle="Groove"
									Height="100%"></FrameStyle>
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
						</igtbl:ultrawebgrid></td>
				</tr>
				<tr class="normal">
					<td>
						<table height="100%" cellPadding="0" width="100%">
							<tr>
								<td><asp:checkbox id="chbSelectAll" runat="server" Width="124px" Text="全选" AutoPostBack="True"></asp:checkbox></td>
								<TD class="smallImgButton"><INPUT class="deleteButton" id="cmdDelete" type="submit" value="  " name="cmdDelete" runat="server">
									|
								</TD>
								<TD><cc1:pagersizeselector id="pagerSizeSelector" runat="server"></cc1:pagersizeselector></TD>
								<td align="right"><cc1:pagertoolbar id="pagerToolBar" runat="server"></cc1:pagertoolbar></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr class="toolBar">
					<td>
						<table class="toolBar">
							<tr>
								<td class="toolBar" style="display:none;"><INPUT class="submitImgButton" id="cmdAppend" type="submit" value="添 加" name="cmdSelect"
										runat="server" onserverclick="cmdSelect_ServerClick"></td>
								<td class="toolBar"><INPUT class="submitImgButton" id="cmdReturn" type="submit" value="返 回" name="cmdReturn"
										runat="server" onserverclick="cmdReturn_ServerClick"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
