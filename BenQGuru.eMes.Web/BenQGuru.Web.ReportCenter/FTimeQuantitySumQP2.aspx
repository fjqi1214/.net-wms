<%@ Page language="c#" Codebehind="FTimeQuantitySumQP2.aspx.cs" AutoEventWireup="True" Inherits="BenQGuru.eMES.Web.WebQuery.FTimeQuantitySumQP2" %>
<%@ Register TagPrefix="igtblexp" Namespace="Infragistics.WebUI.UltraWebGrid.ExcelExport" Assembly="Infragistics35.WebUI.UltraWebGrid.ExcelExport.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="uc1" TagName="eMesTime" Src="~/UserControl/DateTime/DateTime/eMesTime.ascx" %>
<%@ Register TagPrefix="cc2" NameSpace="BenQGuru.eMES.Web.SelectQuery" Assembly="BenQGuru.eMES.Web.SelectQuery" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="cc1" Namespace="BenQGuru.eMES.Web.Helper" Assembly="BenQGuru.eMES.WebUI.Helper" %>
<%@ Register TagPrefix="uc1" TagName="eMesDate" Src="~/UserControl/DateTime/DateTime/eMesDate.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>FTimeQuantitySumQP</title>
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
						<td><table cellpadding="0" cellspacing="0">
								<tr>
									<td><img id="imgTitle" src="../skin/image/ico_arrow.gif" width="15" height="15">
									</td>
									<td><asp:label id="lblTimeQuantitySumQP" runat="server" CssClass="Title2">时间段产量</asp:label></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<table class="query" height="100%" width="100%">
								<TBODY>
									<tr>
										<td class="fieldNameLeft" noWrap><asp:label id="lblSS" runat="server">产线</asp:label></td>
										<td class="fieldValue" noWrap colspan="2"><cc2:selectabletextbox id="txtStepSequence" runat="server" Width="150px" Type="stepsequence"></cc2:selectabletextbox></td>
										<td class="fieldName" noWrap><asp:label id="lblRes" runat="server">资源</asp:label></td>
										<td class="fieldValue" noWrap colspan="2"><cc2:selectabletextbox id="txtRescode" runat="server" Width="150px" Type="resource"></cc2:selectabletextbox></td>
										<td class="fieldValue" noWrap></td>
									</tr>
									<tr>
										<td class="fieldNameLeft" noWrap><asp:label id="lblSDateQuery" runat="server">开始日期</asp:label></td>
										<td class="fieldValue" noWrap>
											<uc1:emesdate id="dateStartDateQuery" runat="server" CssClass="require" width="80"></uc1:emesdate>
										</td>
										<td class="fieldValue" noWrap>
											<uc1:emestime id="dateStartTimeQuery" runat="server" CssClass="require" width="60"></uc1:emestime>
										</td>
										<td class="fieldName" noWrap><asp:label id="lblEDateQuery" runat="server">结束日期</asp:label></td>
										<td class="fieldValue" noWrap>
											<uc1:emesdate id="dateEndDateQuery" runat="server" CssClass="require" width="80"></uc1:emesdate>
										</td>
										<td class="fieldValue" noWrap>
											<uc1:emestime id="dateEndTimeQuery" runat="server" CssClass="require" width="60"></uc1:emestime>
										</td>
										<td width="100%"><INPUT class="submitImgButton" id="cmdQuery" type="submit" value="查 询" name="btnQuery"
												runat="server" onserverclick="cmdQuery_ServerClick"></td>
									</tr>
								</TBODY>
							</table>
						</td>
					</tr>
					<tr height="100%">
						<td class="fieldGrid" id="GridTd" runat="server" vAlign="top" width="100%">
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
							</igtbl:ultrawebgrid></td>
					</tr>
					<tr>
						<TD class="smallImgButton"><INPUT class="gridExportButton" id="cmdGridExport2" type="submit" value="  " name="cmdGridExport"
								runat="server" onserverclick="cmdGridExport2_ServerClick">
						</TD>
					</tr>
				</TBODY>
			</table>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
